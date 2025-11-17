using FluentValidation;

namespace NIBAUTH.Application.Operations.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid role");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+998\d{9}$")
                .WithMessage("Phone number must be a valid Uzbekistan number (e.g., +998901234567)");
        }
    }
}
