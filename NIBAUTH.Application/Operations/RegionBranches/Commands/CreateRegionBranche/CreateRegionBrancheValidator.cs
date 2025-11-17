using FluentValidation;

namespace NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche
{
    public class CreateRegionBrancheValidator : AbstractValidator<CreateRegionBrancheCommand>
    {

        public CreateRegionBrancheValidator()
        {
        RuleFor(createNoteCommand => createNoteCommand.Name).NotNull().NotEmpty().MaximumLength(250);
        RuleFor(createNoteCommand => createNoteCommand.Description).NotNull().NotEmpty();
        RuleFor(createNoteCommand => createNoteCommand.RegionId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(createNoteCommand => createNoteCommand.CreatedByUserId).NotNull().NotEqual(Guid.Empty);
            
        }
    }
}
