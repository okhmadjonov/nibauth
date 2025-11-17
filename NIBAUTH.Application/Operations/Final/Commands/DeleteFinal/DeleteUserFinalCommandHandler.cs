using Abp.EntityFrameworkCore;
using MediatR;
using NIBAUTH.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIBAUTH.Application.Interfaces;


using System.Threading.Tasks;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Operations.Final.Commands.DeleteFinal
{
    public class DeleteUserFinalCommandHandler : IRequestHandler<DeleteUserFinalCommand>
    {
        private readonly INIBAUTHDbContext _context;
        public DeleteUserFinalCommandHandler(INIBAUTHDbContext context) => this._context = context;


        public async Task Handle(DeleteUserFinalCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserFinals.FindAsync(new object[] { request.Id }, cancellationToken);
            var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(UserFinal), request.Id);
            }
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            if (user != null)
            {
                entity.UpdatedBy = user;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
