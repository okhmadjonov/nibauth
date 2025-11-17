using FluentValidation;

namespace NIBAUTH.Application.Operations.BranchBlock.Queries.GetAll
{
    public class BranchesBlockAllQueryValidator : AbstractValidator<BranchesBlockAllQuery>
    {
        public BranchesBlockAllQueryValidator()
        {
            //RuleFor(x => x.OrderColumn).NotEqual(string.Empty);
            //RuleFor(x => x.OrderType).NotEqual(string.Empty);
            //RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
            //RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
