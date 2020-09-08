using Domain.Models.Applications;
using FluentValidation;

namespace Domain.Validators.Applications
{
    public class UpdateApplicationValidator : AbstractValidator<UpdateApplicationModel>
    {
        public UpdateApplicationValidator()
        {
            RuleFor(x => x.AppId).GreaterThan(0).WithMessage("Id is required");
            RuleFor(x => x.AppName).NotEmpty().WithMessage("App Name is required");
            RuleFor(x => x.Configuration).NotNull().WithMessage("Configurtion is required")
                .SetValidator(new ApplicationConfigurationValidator());
        }
    }
}
