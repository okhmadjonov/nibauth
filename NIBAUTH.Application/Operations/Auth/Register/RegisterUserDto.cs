using AutoMapper;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.Users.Commands.RegisterUser
{
    public class RegisterUserDto : IMapWith<RegisterUserCommand>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public Guid? RoleId { get; set; }
        public Guid RegionId { get; set; }
        public Guid? RegionBranchId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterUserDto, RegisterUserCommand>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId))
                .ForMember(dest => dest.RegionBranchId, opt => opt.MapFrom(src => src.RegionBranchId));
        }
    }
}