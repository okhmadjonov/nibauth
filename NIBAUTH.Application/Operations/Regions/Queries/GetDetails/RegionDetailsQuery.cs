using MediatR;

namespace NIBAUTH.Application.Operations.Regions.Queries.GetDetails
{
    public class RegionDetailsQuery : IRequest<RegionDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
