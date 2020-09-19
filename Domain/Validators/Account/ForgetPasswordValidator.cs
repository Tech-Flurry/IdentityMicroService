using Domain.Models.Account;
using Domain.Validators.Common;
using FluentValidation;

namespace Domain.Validators.Account
{
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordModel>
    {
        public ForgetPasswordValidator()
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required")
                                    .SetValidator(new PasswordValidator());
            RuleFor(x => x.ConfirmPassword).NotNull().Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
