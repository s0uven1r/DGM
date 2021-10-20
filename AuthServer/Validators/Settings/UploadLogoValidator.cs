using Dgm.Common.Models.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AuthServer.Validators.Settings
{
    public class UploadLogoValidator: AbstractValidator<LogoUploadModel>
    {
        public UploadLogoValidator()
        {
            RuleFor(x => x.Logo).SetValidator(new FileValidator());
        }
    }

    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(200000)
                .WithMessage("File size is larger than allowed size 2MB");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("File type not allowed");
        }
    }
}
