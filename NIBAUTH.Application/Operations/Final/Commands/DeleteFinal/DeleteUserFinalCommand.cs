using MediatR;

namespace NIBAUTH.Application.Operations.Final.Commands.DeleteFinal
{
    public class DeleteUserFinalCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}

