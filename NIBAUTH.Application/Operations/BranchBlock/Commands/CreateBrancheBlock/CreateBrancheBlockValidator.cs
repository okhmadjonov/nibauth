using FluentValidation;

namespace NIBAUTH.Application.Operations.BranchBlock.Commands.CreateBrancheBlock
{
    public class CreateBrancheBlockValidator : AbstractValidator<CreateBrancheBlockCommand>
    {

        public CreateBrancheBlockValidator()
        {
        RuleFor(createNoteCommand => createNoteCommand.Name).NotNull().NotEmpty().MaximumLength(250);
        RuleFor(createNoteCommand => createNoteCommand.Description).NotNull().NotEmpty();
        RuleFor(createNoteCommand => createNoteCommand.BranchId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(createNoteCommand => createNoteCommand.CreatedByUserId).NotNull().NotEqual(Guid.Empty);
            
        }
    }
}
