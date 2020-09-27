using InternalServices.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.OTPs
{
    [Route("identity/OTPs")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OTPsControllerV1 : IdentityControllersBase
    {
        private readonly IOTPService _service;

        public OTPsControllerV1(IOTPService service)
        {
            _service = service;
        }
        /// <summary>
        /// Action to Validate an OTP
        /// </summary>
        /// <param name="otp">One Time Password</param>
        /// <param name="key">Applcation Secret key</param>
        /// <returns></returns>
        [HttpGet("ValidateOTP")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult ValidateOTP(string otp, string key)
        {
            var model = Try(() =>
            {
                var message = _service.ValidateOTP(otp, key);
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
    }
}
