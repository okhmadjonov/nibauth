using AutoMapper;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Operations.Regions.Queries.GetDetails
{
    public class RegionDetailsQueryHandler : IRequestHandler<RegionDetailsQuery, RegionDetailsVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public RegionDetailsQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);


        public async Task<RegionDetailsVm> Handle(RegionDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Regions
                .FirstOrDefaultAsync(x=>x.Id==request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Region), request.Id);
            }

            var rd = new RegionDetailsVm
            {
                Id = entity.Id,
                Name = entity?.Name,
            };

            return rd;
        }
    }
}
