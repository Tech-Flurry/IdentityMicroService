using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPIGateway.Infrastructure.InternalModels;

namespace WebAPIGateway.Infrastructure
{
    public abstract class IdentityControllersBase : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            return base.Ok(value);
        }
        public override UnauthorizedResult Unauthorized()
        {
            return base.Unauthorized();
        }
        public override BadRequestResult BadRequest()
        {
            return base.BadRequest();
        }
        protected T Try<T>(Func<T> func, out bool isSuccessful)
        {
            try
            {
                var obj = func.Invoke();
                isSuccessful = true;
                return obj;
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                throw ex;
            }
        }
        protected IActionResult Throw(string message)
        {
            return BadRequest(new APIErrorResponse { ErrorMessage = message });
        }
        protected IActionResult Throw(Exception ex)
        {
            return BadRequest(new APIErrorResponse { ErrorMessage = ex.Message });
        }
        protected IActionResult Throw(List<string> messages)
        {
            return BadRequest(new APIErrorResponse { ErrorMessage = string.Join(", ", messages) });
        }
        protected IActionResult ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                return Throw(errors);
            }
            return null;
        }
    }
}
