using NIBAUTH.Domain.Entities.Base;
using System.Threading.Tasks.Dataflow;

namespace NIBAUTH.Domain.Entities
{
    public class RegionBranche: DefaultTable
    {

        public string Name { get; set; }
        public string? Description { get; set; }

        public Region Region { get; set; }

        public ICollection<BrancheBlock> BrancheBlocks { get; set; } = new List<BrancheBlock>();

    }
}
