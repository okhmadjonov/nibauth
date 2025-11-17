using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NIBAUTH.Application.Interfaces;
using NIBAUTH.Application.Common.Exceptions;
using NIBAUTH.Domain.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NIBAUTH.Application.Operations.Final.Commands.CreateFinal
{
    public class CreateUserFinalHandler : IRequestHandler<CreateUserFinalCommand, CreateUserFinalVm>
    {
        private readonly INIBAUTHDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateUserFinalHandler(
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

        public async Task<CreateUserFinalVm> Handle(CreateUserFinalCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.CreatedByUserId, cancellationToken);

            string imageUrl = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, _configuration["MediaPaths:MEDIA_ROOT"], _configuration["MediaPaths:FINAL_ROOT"]);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var id = Guid.NewGuid();
                var extension = Path.GetExtension(request.Image.FileName);
                var filename = id + extension;

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream, cancellationToken);
                }

                imageUrl = _configuration["BASE_URL"]
                    + "/" + _configuration["MediaPaths:MEDIA_ROOT"]
                    + "/" + _configuration["MediaPaths:FINAL_ROOT"]
                    + "/" + filename;
            }

            var final = new UserFinal
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                IsDeleted = false,
                Image = imageUrl,
                Fullname = request.Fullname,
                Pinfl = request.Pinfl,
                WeaponType = request.WeaponType,
                Distance = request.Distance,
                Amount = request.Amount,
                Test = request.Test,
                Result = request.Result,
                Rating = request.Rating
            };

            await _context.UserFinals.AddAsync(final, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var query = await _context.UserFinals
                .Where(f => f.Id == final.Id)
                .Select(f => new CreateUserFinalVm()
                {
                    Id = f.Id,
                    Image = f.Image,
                    Fullname = f.Fullname,
                    Pinfl = f.Pinfl,
                    WeaponType = f.WeaponType,
                    Distance = f.Distance,
                    Amount = f.Amount,
                    Test = f.Test,
                    Result = f.Result,
                    Rating = f.Rating
                })
                .FirstOrDefaultAsync(cancellationToken);

            return query;
        }
    }
}