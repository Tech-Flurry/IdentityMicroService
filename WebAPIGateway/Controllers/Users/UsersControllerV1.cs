using Domain.Models.Users;
using InternalServices.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Users
{
    [Route("identity/Users")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersControllerV1 : IdentityControllersBase
    {
        private readonly IUsersService _service;

        public UsersControllerV1(IUsersService service)
        {
            _service = service;
        }
        /// <summary>
        /// Action to get list of registered users with this micro-service
        /// </summary>
        /// <param name="key">Application Secret Key</param>
        /// <returns></returns>
        [HttpGet("GetRegisteredUsersList")]
        [ProducesResponseType(200, Type = typeof(List<UserListingModel>))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult GetRegisteredUsersList(string key)
        {
            var model = Try(() =>
            {
                List<UserListingModel> model = _service.GetRegisteredUsersList(key);
                return model;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        [HttpGet("IsUsernameAvailable")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult IsUsernameAvailable(string username,string key)
        {
            var model = Try(() =>
            {
                bool model = _service.IsUsernameAvailable(username, key);
                return model;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Action to create a new user for an application
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("CreateNewUser")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult CreateNewUser([FromBody] CreateUserModel userInfo)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                userInfo.CreatedDate = DateTime.Now;
                string userSecret = _service.CreateNewUser(userInfo);
                return userSecret;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        /// <summary>
        /// Updates a user for an application
        /// </summary>
        /// <param name="userInfo">user's information to be updated</param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult UpdateUser([FromBody] UpdateUserModel userInfo, string key)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                userInfo.ModifiedDate = DateTime.Now;
                string userSecret = _service.UpdateUser(userInfo, key);
                return userSecret;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        /// <summary>
        /// Disables a user to perform activity
        /// </summary>
        /// <param name="username">Unqiue username</param>
        /// <param name="key">Application's secret key</param>
        /// <returns></returns>
        [HttpGet("DisableUser")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult DisableUser(string username, string key)
        {
            var model = Try(() =>
            {
                bool result = _service.DisableUser(username, key);
                return result;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        /// <summary>
        /// Enables a user to perform activity
        /// </summary>
        /// <param name="username">Unqiue username</param>
        /// <param name="key">Application's secret key</param>
        /// <returns></returns>
        [HttpGet("EnableUser")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult EnableUser(string username, string key)
        {
            var model = Try(() =>
            {
                bool result = _service.EnableUser(username, key);
                return result;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        /// <summary>
        /// Action used to update the user's phone number
        /// </summary>
        /// <param name="userInfo">Data for the user to be updated</param>
        /// <param name="key">Application's secret key</param>
        /// <returns>String type OTP</returns>
        [HttpPost("UpdatePhoneNumber")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult UpdatePhoneNumber([FromBody] UpdatePhoneNumberModel userInfo, [FromQuery] string key)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                var message = _service.UpdatePhoneNumber(userInfo, key);
                return message;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        /// <summary>
        /// Adds new roles to the user
        /// </summary>
        /// <param name="userRoles"></param>
        /// <param name="key">applcation secret key</param>
        /// <returns></returns>
        [HttpPost("AddRoles")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult AddRoles([FromBody] UserRolesModel userRoles, [FromQuery] string key)
        {
            //var validation = ValidateModel();
            //if (validation != null)
            //{
            //    return validation;
            //}
            var model = Try(() =>
            {
                string message = _service.AddUserRoles(userRoles, key);
                return message;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        /// <summary>
        /// Removes roles to the user
        /// </summary>
        /// <param name="userRoles"></param>
        /// <param name="key">applcation secret key</param>
        /// <returns></returns>
        [HttpPost("RemoveRoles")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult RemoveRoles([FromBody] UserRolesModel userRoles, [FromQuery] string key)
        {
            //var validation = ValidateModel();
            //if (validation != null)
            //{
            //    return validation;
            //}
            var model = Try(() =>
            {
                string message = _service.RemoveUserRoles(userRoles, key);
                return message;
            }, out bool isSuccessfull);
            if (isSuccessfull)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
            }
        }
        ///// <summary>
        ///// Checks the OTP and confirm phone number update
        ///// </summary>
        ///// <param name="userInfo">Data for the user to be updated</param>
        ///// <param name="key">Application's secret key</param>
        ///// <returns></returns>
        //[HttpPost("UpdatePhoneNumberConfirmation")]
        //[ProducesResponseType(200, Type = typeof(bool))]
        //[ProducesErrorResponseType(typeof(APIErrorResponse))]
        //public IActionResult UpdatePhoneNumberConfirmation([FromBody] UpdatePhoneNumberConfirmationModel userInfo, [FromQuery] string key)
        //{
        //    var validation = ValidateModel();
        //    if (validation != null)
        //    {
        //        return validation;
        //    }
        //    var model = Try(() =>
        //    {
        //        bool result = true;
        //        return result;
        //    }, out bool isSuccessfull);
        //    if (isSuccessfull)
        //    {
        //        return Ok(model);
        //    }
        //    else
        //    {
        //        return BadRequest(new APIErrorResponse { ErrorMessage = "Internal Server Error" });
        //    }
        //}
    }
}
