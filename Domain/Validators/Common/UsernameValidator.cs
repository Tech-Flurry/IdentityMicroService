using FluentValidation;
using System.Linq;

namespace Domain.Validators.Common
{
    public static class UsernameValidator
    {

        public static IValidator<string> GetValidator()
        {
            return new ConcreteValidator();
        }

        class ConcreteValidator : AbstractValidator<string>
        {
            public ConcreteValidator()
            {
                RuleFor(x => x).MinimumLength(4).WithMessage("Username cannot be less than 6 letters")
                           .MaximumLength(50).WithMessage("Username cannot be greater than 50")
                           .Must(x => ContainsLowercase(x)).WithMessage("Username must have a lowercase letter")
                           .Must(x => ContainsDigit(x)).WithMessage("Username must have a digit");
            }

            private bool ContainsLowercase(string s)
            {
                var result = s.Any(x =>
                {
                    var ascii = (int)x;
                    var result = (ascii >= 97 && ascii <= 122);
                    return result;
                });
                return result;
            }
            private bool ContainsDigit(string s)
            {
                var result = s.Any(x =>
                {
                    var ascii = (int)x;
                    var result = (ascii >= 43 && ascii <= 57);
                    return result;
                });
                return result;
            }
        }
    }
}
