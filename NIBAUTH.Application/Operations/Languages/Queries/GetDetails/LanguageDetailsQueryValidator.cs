using FluentValidation;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetDetails
{
    public class LanguageDetailsQueryValidator:AbstractValidator<LanguageDetailsQuery>
    {
        public LanguageDetailsQueryValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
}
