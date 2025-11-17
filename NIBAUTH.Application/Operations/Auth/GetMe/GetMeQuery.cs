using MediatR;

namespace NIBAUTH.Application.Operations.Auth.GetMe
{
    public class GetMeQuery : IRequest<GetMeVm>
    {
        public Guid? UserId { get; set; }
    }
}
