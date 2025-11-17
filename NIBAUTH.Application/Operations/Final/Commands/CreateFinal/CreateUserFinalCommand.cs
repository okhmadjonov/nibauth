using MediatR;
using Microsoft.AspNetCore.Http;

namespace NIBAUTH.Application.Operations.Final.Commands.CreateFinal
{
    public class CreateUserFinalCommand : IRequest<CreateUserFinalVm>
    {
        public Guid CreatedByUserId { get; set; }
        public IFormFile? Image { get; set; }
        public string Fullname { get; set; }
        public string Pinfl { get; set; }
        public string WeaponType { get; set; }
        public string Distance { get; set; }
        public int Amount { get; set; }
        public string Test { get; set; }
        public string Result { get; set; }
        public string Rating { get; set; }
    }
}