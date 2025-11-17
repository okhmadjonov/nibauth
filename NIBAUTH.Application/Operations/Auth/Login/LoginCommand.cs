using MediatR;

namespace NIBAUTH.Application.Operations.Auth.Login
{
    public class LoginCommand : IRequest<LoginVm>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
