using FluentValidation;
using System.Linq;

namespace Domain.Validators.Common
{
    public static class PasswordValidator
    {
        public static IValidator<string> GetValidator()
        {
            return new ConcreteValidator();
        }

        class ConcreteValidator : AbstractValidator<string>
        {
            public ConcreteValidator()
            {
                RuleFor(x => x).MinimumLength(6).WithMessage("Password cannot be less than 6 letters")
                            .MaximumLength(50).WithMessage("Password cannot be greater than 50")
                            .Must(x => ContainsUppercase(x)).WithMessage("Password must have an uppercase letter")
                            .Must(x => ContainsLowercase(x)).WithMessage("Password must have a lowercase letter")
                            .Must(x => ContainsDigit(x)).WithMessage("Password must have a digit");
            }
            private bool ContainsUppercase(string s)
            {
                var result = s.Any(x =>
                {
                    var ascii = (int)x;
                    var result = (ascii >= 65 && ascii <= 90);
                    return result;
                });
                return result;
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
