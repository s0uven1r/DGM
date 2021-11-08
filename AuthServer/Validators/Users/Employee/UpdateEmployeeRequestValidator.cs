using AuthServer.Models;
using AuthServer.Models.Users.Employee.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Validators.Users.Employee
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty();
            
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty()
                .Length(2, 20);

            RuleFor(x => x.MiddleName).Cascade(CascadeMode.Stop)
                .MaximumLength(20);

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty()
                .Length(2, 20);
         
            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop).NotNull()
                            .NotEmpty();

            RuleFor(x => x.RoleId).Cascade(CascadeMode.Stop).NotNull()
                          .NotEmpty();

        }
    }
}
