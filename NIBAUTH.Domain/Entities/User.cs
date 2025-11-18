using Microsoft.AspNetCore.Identity;

namespace NIBAUTH.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid? DefaultRoleId { get; set; }
        public Role? DefaultRole { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Guid? RegionId { get; set; }
        public Region? Region { get; set; }
        public Guid? RegionBranchId { get; set; }
        public RegionBranche? RegionBranch { get; set; }
    }
}