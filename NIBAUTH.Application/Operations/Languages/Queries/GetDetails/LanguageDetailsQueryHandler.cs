using AutoMapper;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace NIBAUTH.Application.Operations.Languages.Queries.GetDetails
{
    public class LanguageDetailsQueryHandler:IRequestHandler<LanguageDetailsQuery, LanguageDetailsVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;

        public LanguageDetailsQueryHandler(INIBAUTHDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

        public async Task<LanguageDetailsVm> Handle(LanguageDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Languages.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Language), request.Id);
            }

            return _mapper.Map<LanguageDetailsVm>(entity);
        }

    }
}
