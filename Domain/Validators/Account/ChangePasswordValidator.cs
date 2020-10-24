using Domain.Models.Account;
using Domain.Validators.Common;
using FluentValidation;

namespace Domain.Validators.Account
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required")
                                    .SetValidator(PasswordValidator.GetValidator());
            RuleFor(x => x.NewPassword).NotNull().NotEmpty().WithMessage("New Password is required")
                                    .SetValidator(PasswordValidator.GetValidator());
            RuleFor(x => x.ConfirmPassword).NotNull().Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
