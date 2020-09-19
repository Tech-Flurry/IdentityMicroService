using Domain.Models.Users;
using Domain.Validators.Common;
using FluentValidation;

namespace Domain.Validators.Users
{
    public class UpdatePhoneNumberConfirmationValidator : AbstractValidator<UpdatePhoneNumberConfirmationModel>
    {
        public UpdatePhoneNumberConfirmationValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull().WithMessage("Username is required");
            RuleFor(x => x.MobileNumber).NotEmpty().NotNull().WithMessage("MobileNumber is required")
                                        .SetValidator(new MobilePhoneValidator());
            RuleFor(x => x.OTP).NotEmpty().NotNull().WithMessage("OTP is required");
        }
    }
}
