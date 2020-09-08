using Domain.Models.Data;
using System;

namespace Domain.Models.Applications
{
    public class CreateApplicationModel : DbCreationEntry
    {
        public string ApplicationName { get; set; }
        public ApplicationConfigurationModel Configuration { get; set; }
    }
}
