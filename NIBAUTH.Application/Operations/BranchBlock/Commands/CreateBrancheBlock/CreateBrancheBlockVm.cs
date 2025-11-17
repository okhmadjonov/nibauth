namespace NIBAUTH.Application.Operations.BranchBlock.Commands.CreateBrancheBlock
{
    public class CreateBrancheBlockVm
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid BranchId { get; set; }
    }
}
