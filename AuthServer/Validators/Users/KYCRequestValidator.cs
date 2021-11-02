using AuthServer.Models.Users;
using FluentValidation;

namespace AuthServer.Validators.Users
{
    public class KYCRequestValidator : AbstractValidator<UserKYCRequestModel>
    {
        public KYCRequestValidator()
        {
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop).NotNull()
                    .NotEmpty();
            RuleFor(x => x.PermanentAddress).Cascade(CascadeMode.Stop).NotNull()
                    .NotEmpty();
            RuleFor(x => x.TemporaryAddress).Cascade(CascadeMode.Stop).NotNull()
                   .NotEmpty();
            RuleFor(x => x.CitizenshipNumber).Cascade(CascadeMode.Stop).NotNull()
                   .NotEmpty();
            RuleFor(x => x.ContactNumber).Cascade(CascadeMode.Stop).NotNull()
                   .NotEmpty();
            RuleFor(x => x.Gender).Cascade(CascadeMode.Stop).NotNull()
                   .NotEmpty();
            RuleFor(x => x.BloodGroup).Cascade(CascadeMode.Stop).NotNull()
                   .NotEmpty();
        }
    }
}
