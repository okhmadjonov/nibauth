using MediatR;

namespace NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll
{
    public class RegionBranchesAllQuery : IRequest<RegionBranchesAllVm>
    {
        public string? Search { get; set; } = "";
        public Guid? RegionId { get; set; }

    }
}
