using FluentValidation;

namespace NIBAUTH.Application.Operations.Regions.Queries.GetDetails
{
    public class RegionDetailsQueryValidator : AbstractValidator<RegionDetailsQuery>
    {
        public RegionDetailsQueryValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
}
