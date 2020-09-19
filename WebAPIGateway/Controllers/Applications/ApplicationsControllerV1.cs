using Domain.Models.Applications;
using InternalServices.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Applications
{
    /// <summary>
    /// Controls the requests related to the applications
    /// </summary>
    [Route("identity/Applications")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ApplicationsControllerV1 : IdentityControllersBase
    {
        private readonly IApplicationsService _service;

        public ApplicationsControllerV1(IApplicationsService service)
        {
            _service = service;
        }
        /// <summary>
        /// Returns a list of applications registered in the microservice
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetApplicationsList")]
        [ProducesResponseType(200, Type = typeof(List<ApplicationsListModel>))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult GetApplicationsList()
        {
            var model = Try(() =>
             {
                 List<ApplicationsListModel> model = _service.GetApplicationsList();

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
        /// Validates a secret key for the application according to the criteria
        /// </summary>
        /// <param name="key">Secret key for the applcation</param>
        /// <returns></returns>
        [HttpGet("ValidateApplicationSecret")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult ValidateApplicationSecret(string key)
        {
            var model = Try(() =>
            {
                bool isValidated = _service.ValidateApplicationSecret(key);
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
        /// <summary>
        /// Generates an application secret key 
        /// </summary>
        /// <param name="appId">Primary Key of the application</param>
        /// <returns></returns>
        [HttpGet("GenerateApplicationSecret")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult GenerateApplicationSecret(int appId)
        {
            var model = Try(() =>
            {
                string secret = _service.GenerateApplicationSecret(appId);
                return secret;
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
        /// Creates new Application
        /// </summary>
        /// <param name="appInfo">Model for the new Applcation data</param>
        /// <returns></returns>
        [HttpPost("CreateNewApplication")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult CreateNewApplication([FromBody] CreateApplicationModel appInfo)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                appInfo.CreatedDate = DateTime.Now;
                var secret = _service.CreateNewApplication(appInfo);
                return secret;
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
        /// Updates the application info
        /// </summary>
        /// <param name="appInfo">Model for the new Applcation data</param>
        /// <returns></returns>
        [HttpPost("UpdateApplication")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult UpdateApplication([FromBody] UpdateApplicationModel appInfo)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                appInfo.ModifiedDate = DateTime.Now;
                bool status = _service.UpdateApplicationInfo(appInfo);
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
