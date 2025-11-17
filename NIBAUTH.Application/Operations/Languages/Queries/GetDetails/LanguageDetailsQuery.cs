using MediatR;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetDetails
{
    public class LanguageDetailsQuery:IRequest<LanguageDetailsVm>
    {
        public Guid Id { get; set; }
        public string LangCode { get; set; }
    }
}
