using MediatR;

namespace NIBAUTH.Application.Operations.Cameras.Commands.Create
{
    public class CreateCameraCommand : IRequest<CreateCameraVm>
    {
        public Guid CreatedByUserId { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public Guid BranchBlockId { get; set; }
    }
}
