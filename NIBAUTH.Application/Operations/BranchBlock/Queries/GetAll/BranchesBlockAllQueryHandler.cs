using AutoMapper;
using NIBAUTH.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Operations.BranchBlock.Queries.GetAll
{
    public class BranchesBlockAllQueryHandler : IRequestHandler<BranchesBlockAllQuery, BranchesBlockAllVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public BranchesBlockAllQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<BranchesBlockAllVm> Handle(BranchesBlockAllQuery request, CancellationToken cancellationToken)
        {
            var branchBlocksQuery = _context.BrancheBlocks
                .Include(x => x.RegionBranche)
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                branchBlocksQuery = branchBlocksQuery.Where(x =>
                    x.Name.Contains(request.Search) ||
                    (x.Description != null && x.Description.Contains(request.Search)));
            }

            if (request.RegionBranchId.HasValue)
            {
                branchBlocksQuery = branchBlocksQuery.Where(x => x.RegionBranche.Id == request.RegionBranchId.Value);
            }

            var branchBloks = await branchBlocksQuery
                .Select(x => new BranchesBlockAllDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    RegionBranchId = x.RegionBranche.Id
                })
                .ToListAsync(cancellationToken);

            return new BranchesBlockAllVm { List = branchBloks };
        }
    }
}