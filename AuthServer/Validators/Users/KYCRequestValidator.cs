using AuthServer.Models.Users;
using FluentValidation;

namespace AuthServer.Validators.Users
{
    public class KYCRequestValidator : AbstractValidator<UserKYCRequestModel>
    {
        public KYCRequestValidator()
        {
            RuleFor(x => x.FullName)
                    .NotEmpty();
            RuleFor(x => x.PermanentAddress)
                    .NotEmpty();
            RuleFor(x => x.TemporaryAddress)
                   .NotEmpty();
            RuleFor(x => x.CitizenshipNumber)
                   .NotEmpty();
            RuleFor(x => x.ContactNumber)
                   .NotEmpty();
            RuleFor(x => x.Gender)
                   .NotEmpty();
            RuleFor(x => x.BloodGroup)
                   .NotEmpty();
        }
    }
}
