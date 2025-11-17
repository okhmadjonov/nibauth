using AutoMapper;
using NIBAUTH.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetAll
{
    public class LanguagesAllQueryHandler:IRequestHandler<LanguagesAllQuery, LanguagesAllVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public LanguagesAllQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<LanguagesAllVm> Handle(LanguagesAllQuery request, CancellationToken cancellationToken)
        {
            var query = await (from l in _context.Languages
            select new LanguagesAllDto{
                Id=l.Id,
                Name=l.Name,
                Code=l.Code,
                Flag=l.Flag,
                IsDefault=l.IsDefault
            }).ToListAsync(cancellationToken);

            return new LanguagesAllVm { List = query };
        }
    }
}
