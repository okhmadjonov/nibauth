using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NIBAUTH.Application.Common.Extensions;
using NIBAUTH.Application.Common.Models;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Operations.Auth.GetMe;
using NIBAUTH.Application.Operations.Auth.Login;
using NIBAUTH.Application.Operations.Auth.RefreshToken;
using NIBAUTH.Application.Operations.Users.Commands.RegisterUser;
using NIBAUTH.Domain.Entities;
using NIBAUTH.WebApi.Controllers.Base;

namespace NIBAUTH.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly INIBAUTHDbContext _context;

        public AuthController(
            UserManager<User> userManager,
            IMapper mapper,
            INIBAUTHDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginVm>>> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return this.BadRequestResponse<LoginVm>("form_fields_not_correct");

            var command = _mapper.Map<LoginCommand>(loginDto);
            if (command == null)
                return this.BadRequestResponse<LoginVm>("form_fields_not_correct");

            var vm = await Mediator.Send(command);
            return this.OkResponse(vm, "success");
        }

        [HttpPost("register")]
        [Authorize]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        [RequestSizeLimit(int.MaxValue)]
        public async Task<ActionResult<ApiResponse<RegisterUserVm>>> Register(
          [FromForm] RegisterUserDto registerDto,
          IFormFile? photo)
        {
            if (registerDto == null)
                return this.BadRequestResponse<RegisterUserVm>("form_fields_not_correct");

            var command = _mapper.Map<RegisterUserCommand>(registerDto);
            if (command == null)
                return this.BadRequestResponse<RegisterUserVm>("form_fields_not_correct");

            command.Photo = photo;
            command.CreatedByUserId = UserId;
            var result = await Mediator.Send(command);
            return this.OkResponse(result, "User registered successfully");
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<ApiResponse<RefreshTokenVm>>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null)
                return this.BadRequestResponse<RefreshTokenVm>("form_fields_not_correct");

            var command = _mapper.Map<RefreshTokenCommand>(refreshTokenDto);
            if (command == null)
                return this.BadRequestResponse<RefreshTokenVm>("form_fields_not_correct");

            var vm = await Mediator.Send(command);
            return this.OkResponse(vm, "Token refreshed successfully");
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<GetMeVm>>> GetMe()
        {
            if (UserId == Guid.Empty)
                return this.UnauthorizedResponse<GetMeVm>("User not authenticated");

            var query = new GetMeQuery { UserId = UserId };
            var vm = await Mediator.Send(query);
            return this.OkResponse(vm, "User data retrieved successfully");
        }
    }
}