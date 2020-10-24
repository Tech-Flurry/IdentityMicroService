namespace Domain.Models.Account
{
    /// <summary>
    /// Data holding model to login 
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Unique username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
