using AutoMapper;
using NIBAUTH.Application.Common.Mappings;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetDetails
{
    public class LanguageDetailsVm:IMapWith<Language>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }
        public bool IsDefault { get; set; }

        public void Mapping(Profile profile) {

            profile.CreateMap<Language, LanguageDetailsVm>()
                .ForMember(ldVm=>ldVm.Id, opt=> opt.MapFrom(language=> language.Id))
                .ForMember(ldVm=>ldVm.Name, opt=> opt.MapFrom(language=> language.Name))
                .ForMember(ldVm=>ldVm.Code, opt=> opt.MapFrom(language=> language.Code))
                .ForMember(ldVm=>ldVm.Flag, opt=> opt.MapFrom(language=> language.Flag))
                .ForMember(ldVm=>ldVm.IsDefault, opt=> opt.MapFrom(language=> language.IsDefault))
                ;
        }
    }
}
