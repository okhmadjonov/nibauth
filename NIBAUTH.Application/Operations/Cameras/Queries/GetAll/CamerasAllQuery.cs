using MediatR;

namespace NIBAUTH.Application.Operations.Cameras.Queries.GetAll
{
    public class CamerasAllQuery : IRequest<CamerasAllVm>
    {
        public string? Search { get; set; } = "";
        public Guid? BranchBlockId { get; set; }
    }
}
