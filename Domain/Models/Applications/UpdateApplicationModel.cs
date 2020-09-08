using Domain.Models.Data;

namespace Domain.Models.Applications
{
    /// <summary>
    /// Model to hold the data to update an application
    /// </summary>
    public class UpdateApplicationModel : DbUpdateEntry
    {
        /// <summary>
        /// Primary Key of the application
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// Name of the application
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// Identity configurtion for the applcation
        /// </summary>
        public ApplicationConfigurationModel Configuration { get; set; }
    }
}
