using FluentValidation;

namespace NIBAUTH.Application.Operations.Cameras.Commands.Create
{
    public class CreateCameraValidator : AbstractValidator<CreateCameraCommand>
    {
        public CreateCameraValidator()
        {
            RuleFor(createCameraCommand => createCameraCommand.Name).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(createCameraCommand => createCameraCommand.Description).MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(createCameraCommand => createCameraCommand.IpAddress).NotNull().NotEmpty().MaximumLength(45);
            RuleFor(createCameraCommand => createCameraCommand.Port).NotNull().InclusiveBetween(1, 65535);
            RuleFor(createCameraCommand => createCameraCommand.Username).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(createCameraCommand => createCameraCommand.Password).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(createCameraCommand => createCameraCommand.Type).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(createCameraCommand => createCameraCommand.BranchBlockId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(createCameraCommand => createCameraCommand.CreatedByUserId).NotNull().NotEqual(Guid.Empty);
          
        }
    }
}
