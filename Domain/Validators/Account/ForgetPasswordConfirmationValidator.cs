using Domain.Models.Account;
using FluentValidation;

namespace Domain.Validators.Account
{
    public class ForgetPasswordConfirmationValidator : AbstractValidator<ForgetPasswordConfirmationModel>
    {
        public ForgetPasswordConfirmationValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().WithMessage("Username is required");
            RuleFor(x => x.OTP).NotNull().NotEmpty().WithMessage("OTP is required");
        }
    }
}
