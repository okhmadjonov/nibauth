using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NIBAUTH.Application.Operations.Final.Queries.GetAll
{
    public class UserFinalsAllQueryHandler : IRequestHandler<UserFinalsAllQuery, UserFinalsAllQueryVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public UserFinalsAllQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<UserFinalsAllQueryVm> Handle(UserFinalsAllQuery request, CancellationToken cancellationToken)
        {
            var userFinalsQuery = _context.UserFinals
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                userFinalsQuery = userFinalsQuery.Where(x =>
                    x.Fullname.Contains(request.Search) ||
                    x.Pinfl.Contains(request.Search) ||
                    (x.WeaponType != null && x.WeaponType.Contains(request.Search)) ||
                    (x.Rating != null && x.Rating.Contains(request.Search)));
            }

            var userFinals = await userFinalsQuery
                .Select(x => new UserFinalsAllDto
                {
                    Id = x.Id,
                    Image = x.Image,
                 
                    Fullname = x.Fullname,
                    Pinfl = x.Pinfl,
                    WeaponType = x.WeaponType,
                    Distance = x.Distance,
                    Amount = x.Amount,
                    Test = x.Test,
                    Result = x.Result,
                    Rating = x.Rating
                })
                .ToListAsync(cancellationToken);

            return new UserFinalsAllQueryVm { List = userFinals };
        }
    }
}