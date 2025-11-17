using AutoMapper;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Common.Utilities;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NIBAUTH.Application.Operations.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly TokenManager _tokenManager;

        public RegisterUserCommandHandler(
            INIBAUTHDbContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            TokenManager tokenManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _tokenManager = tokenManager;
        }

        public async Task<RegisterUserVm> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var region = await _context.Regions.FindAsync(new object[] { request.RegionId }, cancellationToken);
            if (region == null)
                throw new NotFoundException(nameof(Region), request.RegionId);

            Role? role;
            if (request.RoleId.HasValue && request.RoleId.Value != Guid.Empty)
            {
                role = await _roleManager.FindByIdAsync(request.RoleId.Value.ToString());
                if (role == null)
                    throw new NotFoundException(nameof(Role), request.RoleId);
            }
            else
            {
                role = await _roleManager.FindByNameAsync(Roles.User);
                if (role == null)
                    throw new NotFoundException(nameof(Role), Roles.User);
            }

            string? photoUrl = null;
            if (request.Photo != null)
            {
                var uploadPath = Path.Combine(
                    _webHostEnvironment.ContentRootPath,
                    _configuration["MediaPaths:MEDIA_ROOT"],
                    _configuration["MediaPaths:USER_ROOT"]);

                Directory.CreateDirectory(uploadPath);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Photo.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await request.Photo.CopyToAsync(stream, cancellationToken);

                photoUrl = $"{_configuration["BASE_URL"]}/{_configuration["MediaPaths:MEDIA_ROOT"]}/{_configuration["MediaPaths:USER_ROOT"]}/{fileName}";
            }

            var user = new User
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                PhotoUrl = photoUrl,
                DefaultRoleId = role.Id,
                RegionId = request.RegionId,
                CreatedAt = DateTime.UtcNow,
                Email = null,
                RefreshToken = _tokenManager.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                        int.TryParse(_configuration["JwtSettings:RefreshTokenValidityInDays"], out var days) ? days : 1)
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                throw new DecrypException(string.Join(", ", createResult.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new CredentialException(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            return new RegisterUserVm
            {
                Id = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                RoleId = user.DefaultRoleId ?? Guid.Empty,
                RegionId = user.RegionId ?? Guid.Empty,
                PhotoUrl = user.PhotoUrl
            };
        }
    }
}