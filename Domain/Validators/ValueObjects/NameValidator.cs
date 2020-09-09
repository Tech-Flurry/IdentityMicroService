using Domain.ValueObjects;
using FluentValidation;

namespace Domain.Validators.ValueObjects
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("First Name is required")
                .Matches("/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u").WithMessage("First name is not a correct name");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Last Name is required")
                .Matches("/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u").WithMessage("Last name is not a correct name");
        }
    }
}
