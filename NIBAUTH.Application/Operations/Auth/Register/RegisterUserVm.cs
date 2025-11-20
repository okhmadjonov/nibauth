namespace NIBAUTH.Application.Operations.Users.Commands.RegisterUser
{
    public class RegisterUserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public Guid RegionId { get; set; }
        public Guid? RegionBranchId { get; set; }
        public string? PhotoUrl { get; set; }
    }
}