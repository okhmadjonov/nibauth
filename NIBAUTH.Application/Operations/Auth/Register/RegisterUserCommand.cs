using MediatR;
using Microsoft.AspNetCore.Http;

namespace NIBAUTH.Application.Operations.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserVm>
    {
        public Guid CreatedByUserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public Guid RegionId { get; set; }
        public Guid? RegionBranchId { get; set; }
        public IFormFile? Photo { get; set; }
    }
}