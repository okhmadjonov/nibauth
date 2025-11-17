using MediatR;

namespace NIBAUTH.Application.Operations.Final.Queries.GetAll
{
    public class UserFinalsAllQuery : IRequest<UserFinalsAllQueryVm>
    {
        public string? Search { get; set; } = "";
    }
}
