using FluentValidation;

namespace NIBAUTH.Application.Operations.Final.Commands.CreateFinal
{
    public class CreateUserFinalValidator : AbstractValidator<CreateUserFinalCommand>
    {
        public CreateUserFinalValidator()
        {
            RuleFor(command => command.Fullname).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(command => command.Pinfl).NotNull().NotEmpty().Length(14);
            RuleFor(command => command.WeaponType).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(command => command.Distance).NotNull().NotEmpty().MaximumLength(10);
            RuleFor(command => command.Amount).NotNull().GreaterThan(0);
            RuleFor(command => command.Test).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(command => command.Result).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(command => command.Rating).NotNull().NotEmpty().MaximumLength(20);
        }
    }
}