using FluentValidation;

namespace NIBAUTH.Application.Operations.Users.Queries.GetDetails
{
    public class UserDetailsQueryValidator:AbstractValidator<UserDetailsQuery>
    {
        public UserDetailsQueryValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
}
