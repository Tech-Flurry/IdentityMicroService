namespace Domain.Models.Users
{
    /// <summary>
    /// Sends OTP response to the user
    /// </summary>
    public class OTPResponseModel
    {
        /// <summary>
        /// Generted OTP
        /// </summary>
        public string OTP { get; set; }
        /// <summary>
        /// Seconds to expire the OTP
        /// </summary>
        public short ExpiresInSeconds { get; set; }
    }
}
