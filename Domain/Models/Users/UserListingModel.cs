using Domain.ValueObjects;

namespace Domain.Models.Users
{
    /// <summary>
    /// Model to hold data to show user listing
    /// </summary>
    public class UserListingModel
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// User's Full Name
        /// </summary>
        public Name UserFullName { get; set; }
        /// <summary>
        /// User's mobile number
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// Name of the user's country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Email of user
        /// </summary>
        public string Email { get; set; }
    }
}
