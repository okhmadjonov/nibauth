using AutoMapper;
using MediatR;
using NIBAUTH.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Application.Operations.Cameras.Queries.GetAll
{
    public class CamerasAllQueryHandler : IRequestHandler<CamerasAllQuery, CamerasAllVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public CamerasAllQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<CamerasAllVm> Handle(CamerasAllQuery request, CancellationToken cancellationToken)
        {
            var camerasQuery = _context.Cameras
                .Include(x => x.BrancheBlock)
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                camerasQuery = camerasQuery.Where(x =>
                    x.Name.Contains(request.Search) ||
                    (x.Description != null && x.Description.Contains(request.Search)) ||
                    x.IpAddress.Contains(request.Search) 
                   );
            }

            if (request.BranchBlockId.HasValue)
            {
                camerasQuery = camerasQuery.Where(x => x.BrancheBlock.Id == request.BranchBlockId.Value);
            }



            var cameras = await camerasQuery
                .Select(x => new CamerasAllDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IpAddress = x.IpAddress,
                    Port = x.Port,
                    Username = x.Username,
                    Password = x.Password,
                    Type = x.Type,
                    BranchBlockId = x.BrancheBlock.Id
                })
                .ToListAsync(cancellationToken);

            return new CamerasAllVm { List = cameras };
        }
    }
}
