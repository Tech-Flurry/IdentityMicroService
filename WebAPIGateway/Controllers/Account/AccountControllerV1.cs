using Domain.Models.Account;
using Microsoft.AspNetCore.Mvc;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Account
{
    [Route("identity/Account")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountControllerV1 : IdentityControllersBase
    {
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
        /// Action to reset forgotten password
        /// </summary>
        /// <param name="info"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost("ForgetPassword")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult ForgetPassword([FromBody] ForgetPasswordModel info, [FromQuery] string key)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                var otp = "";
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
        /// Action for the confirmation of chamged password
        /// </summary>
        /// <param name="info"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost("ForgetPasswordConfirmation")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult ForgetPasswordConfirmation(ForgetPasswordConfirmationModel info, string key)
        {
            var model = Try(() =>
            {
                var status = true;
                return status;
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
