using FluentValidation;

namespace NIBAUTH.Application.Operations.Cameras.Queries.GetAll
{
    public class CamerasAllQueryValidator : AbstractValidator<CamerasAllQuery>
    {
        public CamerasAllQueryValidator()
        {
            // Optional validation rules can be added here
            // For example:
            // RuleFor(x => x.Search).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Search));
            // RuleFor(x => x.Type).MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Type));
        }
    }
}
