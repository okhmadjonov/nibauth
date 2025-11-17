using MediatR;

namespace NIBAUTH.Application.Operations.Users.Queries.GetDetails
{
    public class UserDetailsQuery:IRequest<UserDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
