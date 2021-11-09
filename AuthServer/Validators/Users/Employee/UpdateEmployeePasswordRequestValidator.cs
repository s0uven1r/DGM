using AuthServer.Models.Users.Employee.Request;
using FluentValidation;

namespace AuthServer.Validators.Users.Employee
{
    public class UpdateEmployeePasswordRequestValidator : AbstractValidator<UpdateEmployeePasswordRequest>
    {
        public UpdateEmployeePasswordRequestValidator()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotNull()
              .NotEmpty();

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty();


            RuleFor(x => x.NewPassword).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty();

            RuleFor(x => x.NewConfirmPassword).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty();

        }
    }
}
