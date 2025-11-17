using FluentValidation;

namespace NIBAUTH.Application.Operations.Users.Queries.GetList
{
    public class UsersListQueryValidator:AbstractValidator<UsersListQuery>
    {

        public UsersListQueryValidator()
        {
            //RuleFor(x => x.OrderType).NotEqual(string.Empty);
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
