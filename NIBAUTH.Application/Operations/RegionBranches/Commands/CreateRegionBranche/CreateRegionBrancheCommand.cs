using MediatR;

namespace NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche
{
    public class CreateRegionBrancheCommand : IRequest<CreateRegionBrancheVm>
    {
        public Guid CreatedByUserId { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid RegionId { get; set; }
    }
}
