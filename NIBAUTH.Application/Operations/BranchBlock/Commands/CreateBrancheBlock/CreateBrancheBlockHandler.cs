using Abp.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using NIBAUTH.Application.Common.Exceptions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NIBAUTH.Application.Operations.BranchBlock.Commands.CreateBrancheBlock
{
    public class CreateBrancheBlockHandler : IRequestHandler<CreateBrancheBlockCommand, CreateBrancheBlockVm>
    {

        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CreateBrancheBlockHandler(
         INIBAUTHDbContext context,
        IMapper mapper,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._mapper = mapper;
            this._configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;

        }

        public async Task<CreateBrancheBlockVm> Handle(CreateBrancheBlockCommand request, CancellationToken cancellationToken)
        {

            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.CreatedByUserId, cancellationToken);

            var branch = await _context.RegionBranches.FirstOrDefaultAsync(rg => rg.Id == request.BranchId, cancellationToken);


            if (branch == null)
            {
                throw new NotFoundException(nameof(RegionBranche), request.BranchId);
            }


            var id = Guid.NewGuid();

            var branchBlock = new BrancheBlock
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                IsDeleted = false,
                RegionBranche = branch,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.BrancheBlocks.AddAsync(branchBlock, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await (from bb in _context.BrancheBlocks
                               from r in _context.RegionBranches
                               where bb.Id == branchBlock.Id && r.Id == branchBlock.RegionBranche.Id
                               select new CreateBrancheBlockVm()
                               {
                                   Id = bb.Id,
                                   Name = bb.Name,
                                   Description = bb.Description,
                                   BranchId = r.Id,

                               })
                      .FirstOrDefaultAsync();
            return query;

        }

    }
}
