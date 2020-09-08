using Domain.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPIGateway.Infrastructure;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Controllers.Roles
{
    /// <summary>
    /// Controls the requests for identity roles
    /// </summary>
    [Route("identity/ApplicationsController")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RolesControllerV1 : IdentityControllersBase
    {
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
                List<RolesListModel> model = new List<RolesListModel>();
                model.Add(new RolesListModel
                {
                    RoleId = 1,
                    RoleName = "Admin"
                });
                model.Add(new RolesListModel
                {
                    RoleId = 2,
                    RoleName = "User"
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
        /// Creates a new role for the applcation
        /// </summary>
        /// <param name="role">Role Name</param>
        /// <param name="key">Application's secret key</param>
        /// <returns></returns>
        [HttpGet("AddNewRole")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesErrorResponseType(typeof(APIErrorResponse))]
        public IActionResult AddNewRole(string role, string key)
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
