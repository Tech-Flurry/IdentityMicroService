using Domain.Models.Users;
using Domain.Validators.ValueObjects;
using FluentValidation;

namespace Domain.Validators.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User Id is reqiured");
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Full Name required")
                                    .SetValidator(new NameValidator());
            RuleFor(x => x.Key).NotEmpty().NotNull().WithMessage("Application Key is required");
        }
    }
}
