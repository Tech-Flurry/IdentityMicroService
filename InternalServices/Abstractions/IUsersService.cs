using Domain.Models.Users;
using System.Collections.Generic;

namespace InternalServices.Abstractions
{
    public interface IUsersService
    {
        /// <summary>
        /// Returns a list of users registered in the application
        /// </summary>
        /// <param name="key">appliction secret key</param>
        /// <returns></returns>
        List<UserListingModel> GetRegisteredUsersList(string key);
        /// <summary>
        /// Creates a new user for the application and sends OTP for confirmation 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        string CreateNewUser(CreateUserModel userInfo);
        /// <summary>
        /// Updates the existing user for the application and sends OTP for confirmation
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        string UpdateUser(UpdateUserModel userInfo, string appSecret);
        /// <summary>
        /// Disables a user's activity in the application
        /// </summary>
        /// <param name="username">unique usernam</param>
        /// <param name="key">application secret key</param>
        /// <returns></returns>
        bool DisableUser(string username, string key);
        /// <summary>
        /// update user's phone number
        /// </summary>
        /// <param name="usersPhoneNumberInfo"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string UpdatePhoneNumber(UpdatePhoneNumberModel usersPhoneNumberInfo, string key);
        /// <summary>
        /// Enables a user's activity in the application
        /// </summary>
        /// <param name="username">unique usernam</param>
        /// <param name="key">application secret key</param>
        /// <returns></returns>
        bool EnableUser(string username, string key);
        bool IsUsernameAvailable(string username, string appKey);
        string AddUserRoles(UserRolesModel userRoles, string appKey);
        string RemoveUserRoles(UserRolesModel userRoles, string appKey);
    }
}
