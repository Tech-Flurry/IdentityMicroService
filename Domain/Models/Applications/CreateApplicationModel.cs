using Domain.Models.Data;

namespace Domain.Models.Applications
{
    /// <summary>
    /// Model to hold the data for create applcation
    /// </summary>
    public class CreateApplicationModel : DbCreationEntry
    {
        /// <summary>
        /// Application Name
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Configuration for Identity
        /// </summary>
        public ApplicationConfigurationModel Configuration { get; set; }
    }
}
