using Domain.Validators.Applications;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Infrastucture
{
    public static class Setup
    {
        public static void SetupValidations(this IServiceCollection service)
        {
            service.AddMvc().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<CreateApplicationValidator>();
            });
            
        }
    }
}
