using AuthServer.Models;
using AuthServer.Models.Users.Employee.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Validators.Users.Employee
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty()
                .Length(2, 20);

            RuleFor(x => x.MiddleName).Cascade(CascadeMode.Stop)
                .MaximumLength(20);

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty()
                .Length(2, 20);

            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotNull()
               .NotEmpty()
               .Length(4, 100);

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotNull()
                           .NotEmpty()
                           .EmailAddress();

            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop).NotNull()
                            .NotEmpty();

            RuleFor(x => x.RoleId).Cascade(CascadeMode.Stop).NotNull()
                          .NotEmpty();

        }
    }
}
