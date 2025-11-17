using FluentValidation;

namespace NIBAUTH.Application.Operations.Auth.RefreshToken
{
    public class RefreshTokenvalidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenvalidator()
        {
            RuleFor(x => x.AccessToken).NotNull().NotEqual(string.Empty);
            RuleFor(x => x.RefreshToken).NotNull().NotEqual(string.Empty);
        }
    }
}
