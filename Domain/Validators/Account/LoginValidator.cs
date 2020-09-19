using Domain.Models.Account;
using FluentValidation;

namespace Domain.Validators.Account
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().WithMessage("Username is required");
            RuleFor(x => x.PasswordHash).NotEmpty().NotNull().WithMessage("Password is required");
        }
    }
}
