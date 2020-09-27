using System.Collections.Generic;

namespace Domain.Models.Users
{
    public class UserRolesModel
    {
        public string Username { get; set; }
        public List<Roles> Roles { get; set; }
    }
}
