using AuthServer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestViewModel>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                //.Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2, 50);

            RuleFor(x => x.MiddleName)
                //.Cascade(CascadeMode.Stop)
                .Length(2, 50);

            RuleFor(x => x.LastName)
                //.Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(2, 50);

            RuleFor(x => x.Email)
                //.Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                //.Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(4, 100);

        }

    }
}
