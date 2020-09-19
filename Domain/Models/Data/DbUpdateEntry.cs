using System;

namespace Domain.Models.Data
{
    /// <summary>
    /// Model to hold the data for update into the Db
    /// </summary>
    public abstract class DbUpdateEntry
    {
        /// <summary>
        /// Name of the person updating the data
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Date and Time of changing the record
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
