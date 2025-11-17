using FluentValidation;
using MediatR;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetAll
{
    public class LanguagesAllQueryValidator:AbstractValidator<LanguagesAllQuery>
    {
        public LanguagesAllQueryValidator()
        {
            //RuleFor(x => x.OrderColumn).NotEqual(string.Empty);
            //RuleFor(x => x.OrderType).NotEqual(string.Empty);
            //RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
            //RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
