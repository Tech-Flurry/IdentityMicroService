using FluentValidation;

namespace Domain.Validators.Common
{
    public static class MobilePhoneValidator
    {
        public static IValidator<string> GetValidator()
        {
            return new ConcreteValidator();
        }
        class ConcreteValidator : AbstractValidator<string>
        {
            public ConcreteValidator()
            {
                //regex for mobile number
                RuleFor(x => x).Matches(@"^\+[0-9]{2}\s+[0-9]{2}\s+[0-9]{8}$ ").WithMessage("Enter a valid number");
            }
        }
    }
}
