using AutoMapper;
using NIBAUTH.Application.Common.Mappings;

namespace NIBAUTH.Application.Operations.BranchBlock.Commands.CreateBrancheBlock
{
    public class CreateBrancheBlockDto : IMapWith<CreateBrancheBlockCommand>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? BranchId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBrancheBlockDto, CreateBrancheBlockCommand>()
                .ForMember(productCommand => productCommand.Name, opt => opt.MapFrom(productDto => productDto.Name))
                .ForMember(productCommand => productCommand.Description, opt => opt.MapFrom(productDto => productDto.Description))
                .ForMember(productCommand => productCommand.BranchId, opt => opt.MapFrom(productDto => productDto.BranchId))
                ;
        }
    }
}
