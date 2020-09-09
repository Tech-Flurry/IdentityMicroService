using Domain.ValueObjects;

namespace Domain.Models.Users
{
    /// <summary>
    /// Model to hold data to update a user
    /// </summary>
    public class UpdateUserModel
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
        /// Application Secret Key
        /// </summary>
        public string Key { get; set; }
    }
}
