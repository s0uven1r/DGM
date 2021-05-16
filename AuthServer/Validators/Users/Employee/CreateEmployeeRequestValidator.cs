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
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(2, 20);

            RuleFor(x => x.MiddleName)
                .Length(2, 20);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 20);

            RuleFor(x => x.Password)
                            .NotEmpty()
                            .Length(4, 100);

            RuleFor(x => x.UserName)
               .NotEmpty()
               .Length(4, 100);

            RuleFor(x => x.UserName)
                           .NotEmpty()
                           .EmailAddress();

            RuleFor(x => x.Phone1)
                            .NotEmpty();

            //RuleFor(x => x.Phone2)
            //                .NotEmpty();


            RuleFor(x => x.RoleId)
                          .NotEmpty();

        }
    }
}
