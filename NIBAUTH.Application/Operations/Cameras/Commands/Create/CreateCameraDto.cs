using AutoMapper;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.Cameras.Commands.Create
{
    public class CreateCameraDto : IMapWith<CreateCameraCommand>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IpAddress { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Type { get; set; }
        public Guid? BranchBlockId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCameraDto, CreateCameraCommand>()
                .ForMember(cameraCommand => cameraCommand.Name, opt => opt.MapFrom(cameraDto => cameraDto.Name))
                .ForMember(cameraCommand => cameraCommand.Description, opt => opt.MapFrom(cameraDto => cameraDto.Description))
                .ForMember(cameraCommand => cameraCommand.IpAddress, opt => opt.MapFrom(cameraDto => cameraDto.IpAddress))
                .ForMember(cameraCommand => cameraCommand.Port, opt => opt.MapFrom(cameraDto => cameraDto.Port))
                .ForMember(cameraCommand => cameraCommand.Username, opt => opt.MapFrom(cameraDto => cameraDto.Username))
                .ForMember(cameraCommand => cameraCommand.Password, opt => opt.MapFrom(cameraDto => cameraDto.Password))
                .ForMember(cameraCommand => cameraCommand.Type, opt => opt.MapFrom(cameraDto => cameraDto.Type))
                .ForMember(cameraCommand => cameraCommand.BranchBlockId, opt => opt.MapFrom(cameraDto => cameraDto.BranchBlockId));

                
        }
    }
}
