using Domain.Models.Roles;
using InternalServices.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Roles
{
    /// <summary>
    /// Controls the requests for identity roles
    /// </summary>
    [Route("identity/Roles")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RolesControllerV1 : IdentityControllersBase
    {
        private readonly IRolesService _service;

        public RolesControllerV1(IRolesService service)
        {
            _service = service;
        }
        /// <summary>
        /// Returns a list of roles associated to an application
        /// </summary>
        /// <param name="key">Application's secret key</param>
        /// <returns></returns>
        [HttpGet("GetRolesList")]
        [ProducesResponseType(200, Type = typeof(List<RolesListModel>))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult GetRolesList(string key)
        {
            var model = Try(() =>
            {
                List<RolesListModel> model = _service.GetApplicationRoles(key);
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
        /// Creates a new role for the applcation
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddNewRole")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult AddNewRole([FromBody] CreateRoleModel roleData)
        {
            var validation = ValidateModel();
            if (validation != null)
            {
                return validation;
            }
            var model = Try(() =>
            {
                roleData.CreatedDate = DateTime.Now;
                bool status = _service.AddNewApplicationRole(roleData);
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
        /// <summary>
        /// Removes a role from the application (only if it's not being used by anyone)
        /// </summary>
        /// <param name="role">Role Nameparam>
        /// <param name="key">Application's secret key</param>
        /// <returns></returns>
        [HttpGet("RemoveRole")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult RemoveRole(string role, string key)
        {
            var model = Try(() =>
            {
                bool status = _service.RemoveRole(role, key);
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
        /// <summary>
        /// Disables the activity of all actions under this role in an application
        /// </summary>
        /// <param name="role">Role Nameparam>
        /// <param name="key">Application's secret key</param>
        [HttpGet("DisableRole")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult DisableRole(string role, string key)
        {
            var model = Try(() =>
            {
                bool status = _service.DisableRole(role, key);
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
        /// <summary>
        /// Enables the activity of all actions under this role in an application
        /// </summary>
        /// <param name="role">Role Name</param>
        /// <param name="key">Application's secret key</param>
        [HttpGet("EnableRole")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult EnableRole(string role, string key)
        {
            var model = Try(() =>
            {
                bool status = _service.EnableRole(role, key);
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
