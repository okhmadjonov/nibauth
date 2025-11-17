using AutoMapper;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NIBAUTH.Application.Operations.Auth.GetMe;
using Microsoft.AspNetCore.Hosting;
using NIBAUTH.Application.Interfaces;

namespace NIBAUTH.Application.Operations.Auth.GetMe
{
    public class GetMeHandler : IRequestHandler<GetMeQuery, GetMeVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public GetMeHandler(
            INIBAUTHDbContext context,
            IMapper mapper,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment

            )
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<GetMeVm> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.DefaultRole).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

       
        
            return new GetMeVm
            {
                Id = user.Id,
                Role = user.DefaultRole.Name,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl,
              
            };
        }
    }
}
