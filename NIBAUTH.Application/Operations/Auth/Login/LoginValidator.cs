using FluentValidation;

namespace NIBAUTH.Application.Operations.Auth.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEqual(string.Empty);
            RuleFor(x => x.Password).NotNull().NotEqual(string.Empty);
        }
    }
}
