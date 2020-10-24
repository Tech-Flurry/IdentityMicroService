using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.Models.Users
{
    public class UserInfoModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public long UserId { get; set; }
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
        /// Roles to add user in for an application
        /// </summary>
        public List<Roles> Roles { get; set; }
    }
}
