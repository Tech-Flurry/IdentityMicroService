namespace Domain.Models.Roles
{
    /// <summary>
    /// Model to hold the data for role in a list
    /// </summary>
    public class RolesListModel
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Name of the role
        /// </summary>
        public string RoleName { get; set; }
    }
}
