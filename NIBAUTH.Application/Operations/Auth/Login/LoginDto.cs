using AutoMapper;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.Auth.Login
{
    public class LoginDto : IMapWith<LoginCommand>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<LoginDto, LoginCommand>()
                .ForMember(chuchCommand => chuchCommand.Username, opt => opt.MapFrom(channelDto => channelDto.Username))
                .ForMember(chuchCommand => chuchCommand.Password, opt => opt.MapFrom(channelDto => channelDto.Password));
        }

    }
}
