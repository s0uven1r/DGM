using AuthServer.Models.Roles.Request;
using FluentValidation;

namespace AuthServer.Validators.Roles
{
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotNull()
              .NotEmpty();

            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotNull()
                .NotEmpty()
                .Length(2, 20)
                .Matches(@"^(?!.*<[^>]+>).*");
        }
    }
}
