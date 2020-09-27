using System;

namespace Domain.Models.Users
{
    public class UserGenerateModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? SessionExpire { get; set; }
    }
}
