using Domain.Models.Roles;
using System.Collections.Generic;

namespace DataAcess.Abstractions
{
    public interface IRolesRepository
    {
        /// <summary>
        /// Checks in the db whether the prticular role is present or not
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="appId"></param>
        /// <returns>true if the role is already present</returns>
        bool IsExists(string roleName, int appId);
        /// <summary>
        /// Adds a new role into the db
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true is the record added successfully</returns>
        bool AddNewApplicationRole(AddRoleModel model);
        /// <summary>
        /// Turn the disable flag to on 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool DisableRole(string role, int appId);
        /// <summary>
        /// Returns a list of role for a particular application
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<RolesListModel> GetApplicationRoles(int appId);
        /// <summary>
        /// Checks whether the particular role is assigned to some user
        /// </summary>
        /// <param name="role"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool IsRoleAssigned(string role, int appId);
        /// <summary>
        /// Deletes the role from the db
        /// </summary>
        /// <param name="role"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool DeleteRole(string role, int appId);
        /// <summary>
        /// Turn the enable flag to on 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool EnableRole(string role, int appId);
    }
}
