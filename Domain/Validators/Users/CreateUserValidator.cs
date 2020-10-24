using Domain.Models.Users;
using Domain.Validators.Common;
using Domain.Validators.ValueObjects;
using FluentValidation;

namespace Domain.Validators.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName).NotNull().WithMessage("Full Name is required")
                .SetValidator(new NameValidator());
            RuleFor(x => x.AppKey).NotNull().NotEmpty().WithMessage("Application key is required");
            RuleFor(x => x.Country).NotNull().GreaterThan(0).WithMessage("Country is required");
            RuleFor(x => x.Email).NotNull().WithMessage("Email address is required")
                                .EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(x => x.MobileNumber).NotNull().WithMessage("Phone number is required");
            //.SetValidator(new MobilePhoneValidator());
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required")
                                    .SetValidator(PasswordValidator.GetValidator());
            RuleFor(x => x.PasswordConfirm).NotNull().Equal(x => x.Password).WithMessage("Passwords do not match");
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username is required")
                                    .SetValidator(UsernameValidator.GetValidator());
        }
    }
}
