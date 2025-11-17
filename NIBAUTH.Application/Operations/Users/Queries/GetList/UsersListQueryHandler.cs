using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Paginators;
using NIBAUTH.Domain.Entities;
using System.ComponentModel;
using System.Linq.Dynamic.Core;

namespace NIBAUTH.Application.Operations.Users.Queries.GetList
{
    public class UsersListQueryHandler : IRequestHandler<UsersListQuery, UsersListVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public UsersListQueryHandler(INIBAUTHDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersListVm> Handle(UsersListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Users
                .Include(u => u.DefaultRole)
                .AsQueryable();

            ValidateSortingParameters(request);

            query = ApplyFilters(query, request);
            query = ApplySorting(query, request);

            var totalCount = await query.CountAsync(cancellationToken);

            if (totalCount == 0)
            {
                return CreateEmptyResponse(request);
            }

            var pagedUsers = await GetPagedUsers(query, request, cancellationToken);
            //var userDtos = _mapper.Map<List<UserLookupDto>>(pagedUsers);
            var userDtos = pagedUsers.Select(u => new UserLookupDto
            {
                Id = u.Id,
                UserName = u.UserName,
                //PhotoUrl = u.PhotoUrl,
                //Role = u.DefaultRole != null ? u.DefaultRole.Name : null
            }).ToList();

            return CreatePaginatedResponse(userDtos, totalCount, request);
        }

        private void ValidateSortingParameters(UsersListQuery request)
        {
            var prop = TypeDescriptor.GetProperties(typeof(User)).Find(request.OrderColumn, true);
            if (prop == null)
            {
                request.OrderColumn = "CreatedAt";
            }

            request.OrderType = request.OrderType.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "desc" : "asc";
        }

        private IQueryable<User> ApplyFilters(IQueryable<User> query, UsersListQuery request)
        {
            if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(u =>
                    u.UserName.ToUpper().Contains(request.UserName.ToUpper()));
            }

        

            return query;
        }

        private IQueryable<User> ApplySorting(IQueryable<User> query, UsersListQuery request)
        {
            return query.OrderBy($"{request.OrderColumn} {request.OrderType}");
        }

        private async Task<List<User>> GetPagedUsers(IQueryable<User> query, UsersListQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize > PaginatedList.MaxPageSize)
            {
                request.PageSize = PaginatedList.MaxPageSize;
            }

            return await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
        }

        private UsersListVm CreateEmptyResponse(UsersListQuery request)
        {
            return new UsersListVm
            {
                CurrentPage = request.PageIndex,
                Items = new List<UserLookupDto>(),
                PageSize = request.PageSize,
                TotalItems = 0,
                TotalPages = 0
            };
        }

        private UsersListVm CreatePaginatedResponse(List<UserLookupDto> items, int totalCount, UsersListQuery request)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new UsersListVm
            {
                CurrentPage = request.PageIndex,
                Items = items,
                PageSize = request.PageSize,
                TotalItems = totalCount,
                TotalPages = totalPages
            };
        }
    }
}