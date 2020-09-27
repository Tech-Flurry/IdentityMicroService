using Domain.Models.Users;
using System.Collections.Generic;

namespace DataAcess.Abstractions
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Returns User ID (in the database) by searching with username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        long GetUserId(string username, int appId);
        /// <summary>
        /// Change disable flag for a user which is responsible for enabling/disabling user activity
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isDisabled"></param>
        /// <returns></returns>
        bool ChangeDisableFlag(long userId, bool isDisabled);
        /// <summary>
        /// Returns a list of registered users in an application
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<UserListingModel> GetRegisteredUsers(int appId);
        /// <summary>
        /// Creates a new user for the application into the database
        /// </summary>
        /// <param name="model"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool CreateUser(CreateUserModel model, int appId);
        bool IsExists(string username, int appId);

        /// <summary>
        /// Update phone number of a user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool UpdatePhoneNumber(UserPhoneNumberParam model, int appId);
        /// <summary>
        /// Update user info into the database
        /// </summary>
        /// <param name="model"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool UpdateUser(UpdateUserModel model, int appId);
        bool AddRoles(UserRolesModel userRoles, int appId);
        bool RemoveRoles(UserRolesModel userRoles, int appId);
    }
}
