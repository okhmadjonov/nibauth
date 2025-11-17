using AutoMapper;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche
{
    public class CreateRegionBrancheDto : IMapWith<CreateRegionBrancheCommand>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? RegionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateRegionBrancheDto, CreateRegionBrancheCommand>()
                .ForMember(productCommand => productCommand.Name, opt => opt.MapFrom(productDto => productDto.Name))
                .ForMember(productCommand => productCommand.Description, opt => opt.MapFrom(productDto => productDto.Description))
                .ForMember(productCommand => productCommand.RegionId, opt => opt.MapFrom(productDto => productDto.RegionId))
                ;
        }
    }
}
