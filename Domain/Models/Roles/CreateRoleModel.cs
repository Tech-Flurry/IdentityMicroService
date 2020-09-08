using Domain.Models.Data;

namespace Domain.Models.Roles
{
    /// <summary>
    /// Model for holding create role data
    /// </summary>
    public class CreateRoleModel : DbCreationEntry
    {
        /// <summary>
        /// Name for the role
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Secret Key of the application
        /// </summary>
        public string Key { get; set; }
    }
}
