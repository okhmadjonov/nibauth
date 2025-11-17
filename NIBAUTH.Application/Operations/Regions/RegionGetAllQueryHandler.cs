using AutoMapper;
using NIBAUTH.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Application.Operations.Regions
{
    internal class RegionGetAllQueryHandler : IRequestHandler<RegionGetAllQuery, RegionGetAllQueryVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public RegionGetAllQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<RegionGetAllQueryVm> Handle(RegionGetAllQuery request, CancellationToken cancellationToken)
        {
            var query = await (from p in _context.Regions
                               orderby p.Name
                               select new RegionItem
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Code = p.Code,
                               })
                    .ToListAsync();
            return new RegionGetAllQueryVm { List = query };
        }
    }
}
