using Domain.Models.Roles;
using FluentValidation;

namespace Domain.Validators.Roles
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleModel>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().NotNull().WithMessage("Role Name is Required")
                                                        .Length(5, 50).WithMessage("Role Name's length must between 5 to 50");
            RuleFor(x => x.Key).NotEmpty().NotNull().WithMessage("Application Key is required");
        }
    }
}
