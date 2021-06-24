﻿using AuthServer.Models.Users.Employee.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Validators.Users.Employee
{
    public class UpdateEmployeePasswordRequestValidator : AbstractValidator<UpdateEmployeePasswordRequest>
    {
        public UpdateEmployeePasswordRequestValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();


            RuleFor(x => x.NewPassword)
                .NotEmpty();

            RuleFor(x => x.NewConfirmPassword)
                .NotEmpty();

        }
    }
}