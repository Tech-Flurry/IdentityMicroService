using Domain.Models.Applications;
using FluentValidation;

namespace Domain.Validators.Applications
{
    public class CreateApplicationValidator : AbstractValidator<CreateApplicationModel>
    {
        public CreateApplicationValidator()
        {
            RuleFor(x => x.ApplicationName).NotEmpty().NotNull().WithMessage("Application Name is required.");
            RuleFor(x => x.Configuration).NotNull().WithMessage("Configuration is required.")
                .SetValidator(new ApplicationConfigurationValidator());
        }
    }
}
