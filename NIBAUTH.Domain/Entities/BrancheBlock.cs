using NIBAUTH.Domain.Entities.Base;

namespace NIBAUTH.Domain.Entities
{
    public class BrancheBlock: DefaultTable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public RegionBranche RegionBranche { get; set; }
        public  ICollection<Camera> Cameras { get; set; } = new List<Camera>();

    }
}
