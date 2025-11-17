using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NIBAUTH.Application.Common.Utilities;

namespace NIBAUTH.Application.Operations.Auth.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        public RefreshTokenHandler(
            INIBAUTHDbContext context,
            UserManager<User> userManager,
        IConfiguration configuration
            )
        {
            this._context = context;
            this._userManager = userManager;
            this._configuration = configuration;
        }
        public async Task<RefreshTokenVm> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenManager = new TokenManager(_configuration);

            var principal = tokenManager.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                throw new NotFoundException(nameof(ClaimsPrincipal), request.AccessToken);
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), username);
            }

            if (user.RefreshToken != request.RefreshToken)
            {
                throw new ConflictException("Your refresh token is wrong");
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new ExpiredException("refresh token");
            }

         
            var newAccessToken = tokenManager.CreateToken(principal.Claims.ToList());
            var newRefreshToken = tokenManager.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
            return new RefreshTokenVm
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }
    }
}
