using Domain.Models.Applications;
using FluentValidation;

namespace Domain.Validators.Applications
{
    public class ApplicationConfigurationValidator : AbstractValidator<ApplicationConfigurationModel>
    {
        public ApplicationConfigurationValidator()
        {
            RuleFor(x => x.MaxAttemptsAllowed).GreaterThanOrEqualTo(0).WithMessage("Cannot use negative value for Max. Attempts");
        }
    }
}
