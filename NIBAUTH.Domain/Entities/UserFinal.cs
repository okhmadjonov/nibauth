using NIBAUTH.Domain.Entities.Base;

namespace NIBAUTH.Domain.Entities
{
    public class UserFinal : DefaultTable
    {
        public string? Image { get; set; }
        public string? Fullname { get; set; } 
        public string Pinfl { get; set; }
        public string? WeaponType { get; set; }
        public string? Distance { get; set; }
        public int? Amount { get; set; }
        public string? Test { get; set; }
        public string? Result { get; set; }
        public string? Rating { get; set; }
    }
}