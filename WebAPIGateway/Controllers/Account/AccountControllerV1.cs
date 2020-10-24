using Domain.Models.Account;
using InternalServices.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Account
{
    [Route("identity/Account")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountControllerV1 : IdentityControllersBase
    {
        private readonly IAccountService _service;


        public AccountControllerV1(IAccountService service)
        {
            _service = service;
        }
        /// <summary>
        /// Action for the user to login into the system
        /// </summary>
        /// <param name="loginInfo">Login Data</param>
        /// <param name="key">Application Secret</param>
        /// <returns>User Secret Token</returns>
        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult Login([FromBody] LoginModel loginInfo, [FromQuery] string key)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                string userSecret = _service.Login(loginInfo, key);
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
        /// Action to reset forgotten password
        /// </summary>
        /// <param name="info"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel info, [FromQuery] string key)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                string otp = _service.ChangePassword(info, key);
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
        /// Checks the authentication for the user according to the roles
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="roles"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        [ProducesResponseType(200, Type = typeof(AuthenticationResult))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult Authenticate(string userKey, string[] roles, string appKey)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                AuthenticationResult result = _service.Authenticate(userKey, roles.ToList(), appKey);
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
        ///// <summary>
        ///// Action for the confirmation of chamged password
        ///// </summary>
        ///// <param name="info"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //[HttpPost("ForgetPasswordConfirmation")]
        //[ProducesResponseType(200, Type = typeof(bool))]
        //[ProducesErrorResponseType(typeof(APIErrorResponse))]
        //public IActionResult ForgetPasswordConfirmation(ForgetPasswordConfirmationModel info, string key)
        //{
        //    var model = Try(() =>
        //    {
        //        var status = true;
        //        return status;
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
