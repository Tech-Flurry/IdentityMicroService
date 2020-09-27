using Domain.Models.Data;
using Domain.ValueObjects;

namespace Domain.Models.Users
{
    /// <summary>
    /// Model to hold data to update a user
    /// </summary>
    public class UpdateUserModel : DbUpdateEntry
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Full name of the user
        /// </summary>
        public Name FullName { get; set; }
        /// <summary>
        /// User Secret Token
        /// </summary>
        public string Key { get; set; }
    }
}
