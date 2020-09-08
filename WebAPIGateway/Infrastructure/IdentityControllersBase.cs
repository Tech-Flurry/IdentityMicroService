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
            Response.Headers.Add("api-devloped-by", new Microsoft.Extensions.Primitives.StringValues("techflurry.co"));
            return base.Ok(value);
        }
        public override UnauthorizedResult Unauthorized()
        {
            Response.Headers.Add("api-devloped-by", new Microsoft.Extensions.Primitives.StringValues("techflurry.co"));
            return base.Unauthorized();
        }
        public override BadRequestResult BadRequest()
        {
            Response.Headers.Add("api-devloped-by", new Microsoft.Extensions.Primitives.StringValues("techflurry.co"));
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
            Response.Headers.Add("api-devloped-by", new Microsoft.Extensions.Primitives.StringValues("techflurry.co"));
            return BadRequest(new APIErrorResponse { ErrorMessage = message });
        }
        protected IActionResult Throw(Exception ex)
        {
            Response.Headers.Add("api-devloped-by", new Microsoft.Extensions.Primitives.StringValues("techflurry.co"));
            return BadRequest(new APIErrorResponse { ErrorMessage = ex.Message });
        }
        protected IActionResult Throw(List<string> messages)
        {
            Response.Headers.Add("api-devloped-by", new Microsoft.Extensions.Primitives.StringValues("techflurry.co"));
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
