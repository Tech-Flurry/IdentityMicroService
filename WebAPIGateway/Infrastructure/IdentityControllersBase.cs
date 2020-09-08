using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace WebAPIGateway.Infrastructure
{
    public class IdentityControllersBase : ControllerBase
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
        protected static T Try<T>(Func<T> func, out bool isSuccessful)
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
    }
}
