using Domain.Models.Roles;
using System.Collections.Generic;

namespace InternalServices.Abstractions
{
    public interface IRolesService
    {
        /// <summary>
        /// Returns a list of roles in an application
        /// </summary>
        /// <param name="appSecret">secret key for the application</param>
        /// <returns></returns>
        List<RolesListModel> GetApplicationRoles(string appSecret);
        /// <summary>
        /// Add new role into the application
        /// </summary>
        /// <param name="roleData"></param>
        /// <returns></returns>
        bool AddNewApplicationRole(CreateRoleModel roleData);
        /// <summary>
        /// Remove a role from the application
        /// </summary>
        /// <param name="role">name of the role to be removed</param>
        /// <param name="appSecret">secret key for the application</param>
        /// <returns></returns>
        bool RemoveRole(string role, string appSecret);
        /// <summary>
        /// Disable the activity of a role in the application
        /// </summary>
        /// <param name="role">name of the role to be removed</param>
        /// <param name="appSecret">secret key for the application</param>
        /// <returns></returns>
        bool DisableRole(string role, string appSecret);
        /// <summary>
        /// Enable the activity of a role in the application
        /// </summary>
        /// <param name="role">name of the role to be removed</param>
        /// <param name="appSecret">secret key for the application</param>
        /// <returns></returns>
        bool EnableRole(string role, string key);
    }
}
