using MediatR;

namespace NIBAUTH.Application.Operations.Auth.GetProfile
{
    public class GetProfileQuery : IRequest<GetProfileVm>
    {
        public Guid UserId { get; set; }
    }
}
