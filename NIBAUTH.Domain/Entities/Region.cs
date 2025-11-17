using NIBAUTH.Domain.Entities.Base;

namespace NIBAUTH.Domain.Entities
{
    public class Region:EmptyTable
    {
        public string? Code { get; set; }
        public string Name { get; set; }

        public ICollection<RegionBranche> RegionBranches { get; set; } = new List<RegionBranche>();
        public ICollection<User> Users { get; set; } = new List<User>();


    }
}
