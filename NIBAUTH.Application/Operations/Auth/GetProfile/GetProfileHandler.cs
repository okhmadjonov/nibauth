using AutoMapper;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Application.Operations.Auth.GetProfile
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, GetProfileVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;
        public GetProfileHandler(INIBAUTHDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProfileVm> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            var regionId = Guid.Empty;
            var regionName = string.Empty;

        

            return new GetProfileVm
            {
                Name = user.UserName,
                PhotoUrl = user.PhotoUrl,
                PhoneNumber = user.PhoneNumber,
             
            }; 
        }
    }
}
