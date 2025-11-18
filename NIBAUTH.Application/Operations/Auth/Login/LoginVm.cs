namespace NIBAUTH.Application.Operations.Auth.Login
{
    public class LoginVm
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? RegionName { get; set; }
        public string? RegionBranchName { get; set; }

        // Remove these properties since you're not setting them
        // public DateTime Expiration { get; set; }
        // public Guid RegionId { get; set; }
        // public string? RegionCode { get; set; }
        // public Guid? RegionBranchId { get; set; }
        // public string? DefaultRole { get; set; }
        // public List<string> Roles { get; set; } = new List<string>();
        // public string? UserName { get; set; }
        // public string? Email { get; set; }
    }
}