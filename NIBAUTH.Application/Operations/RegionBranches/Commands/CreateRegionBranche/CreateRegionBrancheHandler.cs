using Abp.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using NIBAUTH.Application.Common.Exceptions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NIBAUTH.Application.Operations.RegionBranches.Commands.CreateRegionBranche
{
    public class CreateRegionBrancheHandler : IRequestHandler<CreateRegionBrancheCommand, CreateRegionBrancheVm>
    {

        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CreateRegionBrancheHandler(
      INIBAUTHDbContext context,
      IMapper mapper,
      IConfiguration configuration,
      IWebHostEnvironment webHostEnvironment

      )
        {
            this._context = context;
            this._mapper = mapper;
            this._configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;

        }

        public async Task<CreateRegionBrancheVm> Handle(CreateRegionBrancheCommand request, CancellationToken cancellationToken)
        {

            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.CreatedByUserId, cancellationToken);

            var region = await _context.Regions.FirstOrDefaultAsync(rg => rg.Id == request.RegionId, cancellationToken);


            if (region == null)
            {
                throw new NotFoundException(nameof(Region), request.RegionId);
            }


            var id = Guid.NewGuid();

            var regionBranche = new RegionBranche
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                IsDeleted = false,
                Region = region,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.RegionBranches.AddAsync(regionBranche, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await (from rb in _context.RegionBranches
                               from r in _context.Regions
                               where rb.Id == regionBranche.Id && r.Id == regionBranche.Region.Id
                               select new CreateRegionBrancheVm()
                               {
                                   Id = rb.Id,
                                   Name = rb.Name,
                                   Description = rb.Description,
                                   RegionId = r.Id,

                               })
                      .FirstOrDefaultAsync();
            return query;

        }

    }
}
