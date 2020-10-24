using System.Collections.Generic;

namespace Domain.Models.Account
{
    public class UserToken
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public int AppId { get; set; }
    }
}
