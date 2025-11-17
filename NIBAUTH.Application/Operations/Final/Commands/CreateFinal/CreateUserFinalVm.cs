using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIBAUTH.Application.Operations.Final.Commands.CreateFinal
{
    public class CreateUserFinalVm
    {
        public Guid Id { get; set; }
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
