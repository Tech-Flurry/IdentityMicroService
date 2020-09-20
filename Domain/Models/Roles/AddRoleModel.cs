using Domain.Models.Data;

namespace Domain.Models.Roles
{
    public class AddRoleModel : DbCreationEntry
    {
        public int AppId { get; set; }
        public string RoleName { get; set; }
    }
}
