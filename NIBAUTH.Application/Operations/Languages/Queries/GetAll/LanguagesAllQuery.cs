using MediatR;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetAll
{
    public class LanguagesAllQuery:IRequest<LanguagesAllVm>
    {
        public string LangCode { get; set; }
    }
}
