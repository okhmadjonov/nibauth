using NIBAUTH.Domain.Entities.Base;

namespace NIBAUTH.Domain.Entities
{
    public class Language : EmptyTable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }
        public bool IsDefault { get; set; }

    }
}
