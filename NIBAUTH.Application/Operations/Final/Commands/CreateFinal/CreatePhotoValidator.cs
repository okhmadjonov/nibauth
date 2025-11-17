using FluentValidation;
using Microsoft.AspNetCore.Http;
using NIBAUTH.Domain;

namespace NIBAUTH.Application.Operations.Final.Commands.CreateFinal
{
    public class CreatePhotoValidator : AbstractValidator<IFormFile>
    {
        public CreatePhotoValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(5 * 1024 * 1024)
                .WithMessage("File size is larger than 5 MB");

            RuleFor(x => x.FileName).NotNull().Must(x => FileExtensions.IMAGE_EXTENSIONS.Contains(Path.GetExtension(x.ToLower())))
                .WithMessage("Image extension not supported");
        }
    }
}