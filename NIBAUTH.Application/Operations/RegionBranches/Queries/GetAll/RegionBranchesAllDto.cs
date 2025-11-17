namespace NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll
{
    public class RegionBranchesAllDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid RegionId { get; set; }
    }
}
