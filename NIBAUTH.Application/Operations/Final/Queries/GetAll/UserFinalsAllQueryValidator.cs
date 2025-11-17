using FluentValidation;
using NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIBAUTH.Application.Operations.Final.Queries.GetAll
{
    public class UserFinalsAllQueryValidator : AbstractValidator<UserFinalsAllQuery>
    {
        public UserFinalsAllQueryValidator()
        {
            //RuleFor(x => x.OrderColumn).NotEqual(string.Empty);
            //RuleFor(x => x.OrderType).NotEqual(string.Empty);
            //RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
            //RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
