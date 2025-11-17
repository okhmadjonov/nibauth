using Microsoft.AspNetCore.Identity;

namespace NIBAUTH.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
