using System;

namespace Domain.Models.Applications
{
    /// <summary>
    /// Model to keep the application info for a list
    /// </summary>
    public class ApplicationsListModel
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int ApplicationId { get; set; }
        /// <summary>
        /// Name of the application
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// Date of creation/registeration
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
