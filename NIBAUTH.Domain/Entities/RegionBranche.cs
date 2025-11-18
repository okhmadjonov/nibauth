using NIBAUTH.Domain.Entities.Base;

namespace NIBAUTH.Domain.Entities
{
    public class RegionBranche : DefaultTable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public ICollection<BrancheBlock> BrancheBlocks { get; set; } = new List<BrancheBlock>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}