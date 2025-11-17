//using NIBAUTH.Application.Common.Exceptions;
//using NIBAUTH.Application.Interfaces;
//using NIBAUTH.Domain.Entities;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using NIBAUTH.Application.Common.Utilities;
//using Microsoft.AspNetCore.Hosting;

//namespace NIBAUTH.Application.Operations.Auth.Login
//{
//    public class LoginHandler : IRequestHandler<LoginCommand, LoginVm>
//    {
//        private readonly INIBAUTHDbContext _context;
//        private readonly UserManager<User> _userManager;
//        private readonly IConfiguration _configuration;
//        private readonly IWebHostEnvironment _webHostEnvironment;


//        public LoginHandler(
//            INIBAUTHDbContext context,
//            UserManager<User> userManager,
//            IConfiguration configuration,
//            IWebHostEnvironment webHostEnvironment

//            )
//        {
//            this._context = context;
//            this._userManager = userManager;
//            this._configuration = configuration;
//            this._webHostEnvironment = webHostEnvironment;

//        }
//        public async Task<LoginVm> Handle(LoginCommand request, CancellationToken cancellationToken)
//        {
//            var user = await _userManager.FindByNameAsync(request.Username);
//            if (user == null)
//            {
//                throw new NotFoundException(nameof(User), request.Username);
//            }



//            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

//            if (!isPasswordCorrect)
//            {
//                throw new CredentialException("Password or username is wrong");
//            }

//            var authClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                };

//            var tokenManager = new TokenManager(_configuration);



//            var token = tokenManager.CreateToken(authClaims);
//            var refreshToken = tokenManager.GenerateRefreshToken();

//            _ = int.TryParse(_configuration["JwtSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
//            var dbUser = await _context.Users.Include(x => x.DefaultRole).FirstOrDefaultAsync(x => x.Id == user.Id, cancellationToken);

//            user.RefreshToken = refreshToken;
//            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

//            await _userManager.UpdateAsync(user);
//            await _context.SaveChangesAsync(cancellationToken);

//            return new LoginVm
//            {
//                Expiration = token.ValidTo,
//                RefreshToken = refreshToken,
//                Token = new JwtSecurityTokenHandler().WriteToken(token),
//            };

//        }
//    }
//}


using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NIBAUTH.Application.Common.Utilities;
using Microsoft.AspNetCore.Hosting;

namespace NIBAUTH.Application.Operations.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TokenManager _tokenManager;

        public LoginHandler(
            INIBAUTHDbContext context,
            UserManager<User> userManager,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _tokenManager = new TokenManager(configuration);
        }

        public async Task<LoginVm> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new CredentialException("Username and password are required");
            }

            var user = await _context.Users
                .Include(u => u.DefaultRole)
                .Include(u => u.Region)
                .FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Username);
            }

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordCorrect)
            {
                throw new CredentialException("Invalid username or password");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = BuildUserClaims(user, userRoles);

            var token = _tokenManager.CreateToken(authClaims);
            var refreshToken = _tokenManager.GenerateRefreshToken();

            await UpdateUserRefreshToken(user, refreshToken, cancellationToken);

            return CreateLoginResponse(token, refreshToken, user, userRoles);
        }

        private List<Claim> BuildUserClaims(User user, IList<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.Id.ToString())
            };

            if ( user.Region != null)
            {
                claims.Add(new Claim("Region", user.Region.Name));
                claims.Add(new Claim("RegionCode", user.Region.Code ?? ""));
            }

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            if (user.DefaultRole != null)
            {
                claims.Add(new Claim("DefaultRole", user.DefaultRole.Name!));
            }

            return claims;
        }

        private async Task UpdateUserRefreshToken(User user, string refreshToken, CancellationToken cancellationToken)
        {
            _ = int.TryParse(_configuration["JwtSettings:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private LoginVm CreateLoginResponse(JwtSecurityToken token, string refreshToken, User user, IList<string> userRoles)
        {
            return new LoginVm
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo,
                RegionName = user.Region?.Name,
                RegionCode = user.Region?.Code,
                DefaultRole = user.DefaultRole?.Name,
                Roles = userRoles.ToList(),
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}