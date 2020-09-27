namespace Domain.Models.Users
{
    public class UpdatePhoneNumberModel
    {
        /// <summary>
        /// Mobile of the user (at which the otp will be sent)
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// Unique UserSecret for the user account
        /// </summary>
        public string UserSecret { get; set; }
    }
}
