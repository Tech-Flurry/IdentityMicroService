using Domain.Models.Users;
using Domain.Validators.Common;
using FluentValidation;

namespace Domain.Validators.Users
{
    public class UpdatePhoneNumberValidator : AbstractValidator<UpdatePhoneNumberModel>
    {
        public UpdatePhoneNumberValidator()
        {
            RuleFor(x => x.MobileNumber).NotEmpty().NotNull().WithMessage("MobileNumber is required")
                                        .SetValidator(MobilePhoneValidator.GetValidator());
        }
    }
}
