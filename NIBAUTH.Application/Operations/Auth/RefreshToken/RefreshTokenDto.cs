using AutoMapper;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.Auth.RefreshToken
{
    public class RefreshTokenDto : IMapWith<RefreshTokenCommand>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<RefreshTokenDto, RefreshTokenCommand>()
                .ForMember(command => command.AccessToken, opt => opt.MapFrom(dto => dto.AccessToken))
                .ForMember(command => command.RefreshToken, opt => opt.MapFrom(dto => dto.RefreshToken))
                ;
        }

    }
}
