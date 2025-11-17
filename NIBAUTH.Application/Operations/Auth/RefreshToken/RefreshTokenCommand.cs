using MediatR;

namespace NIBAUTH.Application.Operations.Auth.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenVm>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
