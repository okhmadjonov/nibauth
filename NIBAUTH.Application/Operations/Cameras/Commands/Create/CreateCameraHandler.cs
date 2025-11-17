using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace NIBAUTH.Application.Operations.Cameras.Commands.Create
{
    public class CreateCameraHandler : IRequestHandler<CreateCameraCommand, CreateCameraVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateCameraHandler(
            INIBAUTHDbContext context,
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CreateCameraVm> Handle(CreateCameraCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.CreatedByUserId, cancellationToken);

            var branchBlock = await _context.BrancheBlocks.FirstOrDefaultAsync(bb => bb.Id == request.BranchBlockId, cancellationToken);

            if (branchBlock == null)
            {
                throw new NotFoundException(nameof(BrancheBlock), request.BranchBlockId);
            }

            var id = Guid.NewGuid();

            var camera = new Camera
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                IpAddress = request.IpAddress,
                Port = request.Port,
                Username = request.Username,
                Password = request.Password,
               
                Type = request.Type,
                BrancheBlock = branchBlock,
                IsDeleted = false,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
            };

            await _context.Cameras.AddAsync(camera, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await (from c in _context.Cameras
                               from bb in _context.BrancheBlocks
                               where c.Id == camera.Id && bb.Id == camera.BrancheBlock.Id
                               select new CreateCameraVm()
                               {
                                   Id = c.Id,
                                   Name = c.Name,
                                   Description = c.Description,
                                   IpAddress = c.IpAddress,
                                   Port = c.Port,
                                   Username = c.Username,
                                   Password = c.Password,
                                  
                                   Type = c.Type,
                                   BranchBlockId = bb.Id,
                               })
                      .FirstOrDefaultAsync(cancellationToken);

            return query;
        }
    }
}
