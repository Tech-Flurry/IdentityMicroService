using Domain.Models.Applications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Applications
{
    [Route("identity/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ApplicationsControllerV1 : IdentityControllersBase
    {
        [HttpGet("GetApplicationsList")]
        [ProducesResponseType(200, Type = typeof(List<ApplicationsListModel>))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult GetApplicationsList()
        {
            var model = Try(() =>
             {
                 List<ApplicationsListModel> model = new List<ApplicationsListModel>();
                 model.Add(new ApplicationsListModel
                 {
                     ApplicationId = 1,
                     ApplicationName = "E-Farmer.pk",
                     CreatedDate = DateTime.Now
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
        [HttpGet("ValidateApplicationSecerate")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult ValidateApplicationSecerate(string key)
        {
            var model = Try(() =>
            {
                var isValidated = true;
                return isValidated;
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
        [HttpGet("GenerateApplicationSecerate")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult GenerateApplicationSecerate(int appId)
        {
            var model = Try(() =>
            {
                var secerate = "AezaIwxfyupoossnjnjnlmmllm";
                return secerate;
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
        [HttpPost("CreateNewApplication")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult CreateNewApplication([FromBody] CreateApplicationModel appInfo)
        {
            var model = Try(() =>
            {
                var secerate = "AezaIwxfyupoossnjnjnlmmllm";
                return secerate;
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
        [HttpPost("UpdateApplication")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult UpdateApplication([FromBody] UpdateApplicationModel appInfo)
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
