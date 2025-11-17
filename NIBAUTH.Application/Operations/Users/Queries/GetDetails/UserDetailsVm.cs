using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Operations.Users.Queries.GetDetails
{
    public class UserDetailsVm
    {
        public Guid Id { get; set; }
        public string? Role { get; set; } 
        public string? UserName { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
