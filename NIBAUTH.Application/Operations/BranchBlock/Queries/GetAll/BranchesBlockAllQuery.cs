using MediatR;

namespace NIBAUTH.Application.Operations.BranchBlock.Queries.GetAll
{
    public class BranchesBlockAllQuery : IRequest<BranchesBlockAllVm>
    {
        public string? Search { get; set; } = "";
        public Guid? RegionBranchId { get; set; }

    }
}
