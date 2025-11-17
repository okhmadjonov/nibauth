using MediatR;

namespace NIBAUTH.Application.Operations.BranchBlock.Commands.CreateBrancheBlock
{
    public class CreateBrancheBlockCommand : IRequest<CreateBrancheBlockVm>
    {
        public Guid CreatedByUserId { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid BranchId { get; set; }
    }
}
