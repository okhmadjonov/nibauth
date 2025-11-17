using FluentValidation;

namespace NIBAUTH.Application.Operations.Auth.GetProfile
{
    public class GetProfileValidator : AbstractValidator<GetProfileQuery>
    {
        public GetProfileValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
        }
    }
}
