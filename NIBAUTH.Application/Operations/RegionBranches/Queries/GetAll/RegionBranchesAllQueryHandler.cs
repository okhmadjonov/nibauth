using AutoMapper;
using NIBAUTH.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Operations.RegionBranches.Queries.GetAll
{
    public class RegionBranchesAllQueryHandler : IRequestHandler<RegionBranchesAllQuery, RegionBranchesAllVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public RegionBranchesAllQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<RegionBranchesAllVm> Handle(RegionBranchesAllQuery request, CancellationToken cancellationToken)
        {
            var regionBranchesQuery = _context.RegionBranches
                .Include(x => x.Region)
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                regionBranchesQuery = regionBranchesQuery.Where(x =>
                    x.Name.Contains(request.Search) ||
                    (x.Description != null && x.Description.Contains(request.Search)));
            }

            if (request.RegionId.HasValue)
            {
                regionBranchesQuery = regionBranchesQuery.Where(x => x.Region.Id == request.RegionId.Value);
            }

            var regionBranches = await regionBranchesQuery
                .Select(x => new RegionBranchesAllDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    RegionId = x.Region.Id
                })
                .ToListAsync(cancellationToken);

            return new RegionBranchesAllVm { List = regionBranches };
        }
    }
}