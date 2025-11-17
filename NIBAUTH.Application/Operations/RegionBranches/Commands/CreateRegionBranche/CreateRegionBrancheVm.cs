namespace NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche
{
    public class CreateRegionBrancheVm
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid RegionId { get; set; }
    }
}
