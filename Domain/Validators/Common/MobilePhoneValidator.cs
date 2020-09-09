using FluentValidation;

namespace Domain.Validators.Common
{
    public class MobilePhoneValidator : AbstractValidator<string>
    {
        public MobilePhoneValidator()
        {
            //regex for mobile number
            RuleFor(x => x).Matches(@"^\+[0-9]{2}\s+[0-9]{2}\s+[0-9]{8}$ ").WithMessage("Enter a valid number");
        }
    }
}
