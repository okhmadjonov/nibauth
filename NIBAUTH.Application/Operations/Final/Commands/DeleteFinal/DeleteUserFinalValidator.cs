using FluentValidation;

namespace NIBAUTH.Application.Operations.Final.Commands.DeleteFinal
{
    public class DeleteUserFinalValidator : AbstractValidator<DeleteUserFinalCommand>
    {
        public DeleteUserFinalValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty);

        }
    }
}
