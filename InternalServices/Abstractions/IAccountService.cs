using Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternalServices.Abstractions
{
    public interface IAccountService
    {
        /// <summary>
        /// Enter the login credentials and get the user token
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        string Login(LoginModel loginInfo, string appKey);
        /// <summary>
        /// Changes a password for the user
        /// </summary>
        /// <param name="info"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string ChangePassword(ChangePasswordModel info, string appKey);
        /// <summary>
        /// Gives the authentication to the user according to the roles
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        AuthenticationResult Authenticate(string userKey, List<string> roles, string appKey);
    }
}
