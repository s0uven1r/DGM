using Dgm.Common.Models.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AuthServer.Validators.Settings
{
    public class DescriptiveImageUploadModelValidator : AbstractValidator<DescriptiveImageUploadModel>
    {
        public DescriptiveImageUploadModelValidator()
        {
            RuleForEach(x => x.Images).SetValidator(new DescriptiveImageFileValidator());
        }
    }

    public class DescriptiveImageFileValidator : AbstractValidator<IFormFile>
    {
        public DescriptiveImageFileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(1000000)
                .WithMessage("File size is larger than allowed size 10MB");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("File type not allowed");
        }
    }
}
