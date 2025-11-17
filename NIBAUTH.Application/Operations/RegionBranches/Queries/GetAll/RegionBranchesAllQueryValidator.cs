using FluentValidation;

namespace NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll
{
    public class RegionBranchesAllQueryValidator : AbstractValidator<RegionBranchesAllQuery>
    {
        public RegionBranchesAllQueryValidator()
        {
            //RuleFor(x => x.OrderColumn).NotEqual(string.Empty);
            //RuleFor(x => x.OrderType).NotEqual(string.Empty);
            //RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
            //RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
