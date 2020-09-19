namespace Domain.Models.Users
{
    public class UpdatePhoneNumberConfirmationModel : UpdatePhoneNumberModel
    {
        /// <summary>
        /// One time password use to authorize a password change
        /// </summary>
        public string OTP { get; set; }
    }
}
