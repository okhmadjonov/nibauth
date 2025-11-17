using AutoMapper;
using Microsoft.AspNetCore.Http;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.Final.Commands.CreateFinal
{
    public class CreateUserFinalDto : IMapWith<CreateUserFinalCommand>
    {
        public IFormFile? Image { get; set; }
        public string? Fullname { get; set; }
        public string? Pinfl { get; set; }
        public string? WeaponType { get; set; }
        public string? Distance { get; set; }
        public int? Amount { get; set; }
        public string? Test { get; set; }
        public string? Result { get; set; }
        public string? Rating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserFinalDto, CreateUserFinalCommand>()
                .ForMember(finalCommand => finalCommand.Image, opt => opt.MapFrom(finalDto => finalDto.Image))
                .ForMember(finalCommand => finalCommand.Fullname, opt => opt.MapFrom(finalDto => finalDto.Fullname))
                .ForMember(finalCommand => finalCommand.Pinfl, opt => opt.MapFrom(finalDto => finalDto.Pinfl))
                .ForMember(finalCommand => finalCommand.WeaponType, opt => opt.MapFrom(finalDto => finalDto.WeaponType))
                .ForMember(finalCommand => finalCommand.Distance, opt => opt.MapFrom(finalDto => finalDto.Distance))
                .ForMember(finalCommand => finalCommand.Amount, opt => opt.MapFrom(finalDto => finalDto.Amount))
                .ForMember(finalCommand => finalCommand.Test, opt => opt.MapFrom(finalDto => finalDto.Test))
                .ForMember(finalCommand => finalCommand.Result, opt => opt.MapFrom(finalDto => finalDto.Result))
                .ForMember(finalCommand => finalCommand.Rating, opt => opt.MapFrom(finalDto => finalDto.Rating));
        }
    }
}