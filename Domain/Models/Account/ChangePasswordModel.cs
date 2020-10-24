namespace Domain.Models.Account
{
    public class ChangePasswordModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
