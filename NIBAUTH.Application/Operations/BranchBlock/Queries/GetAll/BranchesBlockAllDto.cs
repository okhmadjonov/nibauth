namespace NIBAUTH.Application.Operations.BranchBlock.Queries.GetAll
{
    public class BranchesBlockAllDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid RegionBranchId { get; set; }
    }
}
