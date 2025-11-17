using FluentValidation;

namespace NIBAUTH.Application.Operations.Auth.GetMe
{
    public class GetMeValidator : AbstractValidator<GetMeQuery>
    {
        public GetMeValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
        }
    }
}
