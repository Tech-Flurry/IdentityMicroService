using System;

namespace Domain.Models.Data
{
    /// <summary>
    /// Model to hold audit data
    /// </summary>
    public abstract class DbCreationEntry
    {
        /// <summary>
        /// Data created by in the Db
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Date and time of data cretion into the Db
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
