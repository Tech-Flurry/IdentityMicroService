using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models.Users;
using Domain.Validators.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Users
{
    [Route("identity/Users")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersControllerV1 : IdentityControllersBase
    {
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
                List<UserListingModel> model = new List<UserListingModel>();
                model.Add(new UserListingModel
                {
                    Country = "Pakstan",
                    Email = "abc@gmail.com",
                    Id = 1,
                    MobileNumber = "+923214302360",
                    UserFullName = new Domain.ValueObjects.Name
                    {
                        FirstName = "Umair",
                        LastName = "Tahir"
                    }
                });
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
                var userSecret = "AezaIwxfyupoossnjnjnlmmllm";
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
        public IActionResult UpdateUser([FromBody] UpdateUserModel userInfo)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                var userSecret = "AezaIwxfyupoossnjnjnlmmllm";
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
                var result = true;
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
                var otp = "otp";
                return otp;
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
        /// Checks the OTP and confirm phone number update
        /// </summary>
        /// <param name="userInfo">Data for the user to be updated</param>
        /// <param name="key">Application's secret key</param>
        /// <returns></returns>
        [HttpPost("UpdatePhoneNumberConfirmation")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult UpdatePhoneNumberConfirmation([FromBody] UpdatePhoneNumberConfirmationModel userInfo, [FromQuery] string key)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                var result = true;
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
    }
}
