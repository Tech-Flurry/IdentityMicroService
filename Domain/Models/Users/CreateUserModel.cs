using Domain.Models.Data;
using Domain.ValueObjects;

namespace Domain.Models.Users
{
    /// <summary>
    /// Model to hold data to create a user
    /// </summary>
    public class CreateUserModel : DbCreationEntry
    {
        /// <summary>
        /// Full name of the user
        /// </summary>
        public Name FullName { get; set; }
        /// <summary>
        /// Country Id to which the user belongs
        /// </summary>
        public int Country { get; set; }
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mobile of the user (at which the otp will be sent)
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// Unique username for the user account
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password for the user account
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password confirmation (must match the password)
        /// </summary>
        public string PasswordConfirm { get; set; }
        /// <summary>
        /// Secret key for the application
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// Roles to add user in for an application
        /// </summary>
        public string[] Roles { get; set; }
    }
}
