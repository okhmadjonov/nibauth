using AutoMapper;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Application.Operations.Users.Queries.GetDetails
{
    public class UserDetailsQueryHandler : IRequestHandler<UserDetailsQuery, UserDetailsVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public UserDetailsQueryHandler(INIBAUTHDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDetailsVm> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var role = await _context.Roles
                                     .Where(r => r.Id == user.DefaultRoleId)
                                     .Select(r => r.Name)
                                     .FirstOrDefaultAsync(cancellationToken);


       

            var userDetails = new UserDetailsVm
            {
                Id = request.Id,
                Role = role,
                UserName = user.UserName,
                PhotoUrl = user.PhotoUrl,
            
            };

            return userDetails;
        }
    }
}
