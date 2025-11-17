using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NIBAUTH.Domain.Entities;

namespace NIBAUTH.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDescriptor != null)
            {
                var hasAllowAnonymous =
                    actionDescriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>() != null ||
                    actionDescriptor.ControllerTypeInfo.GetCustomAttribute<AllowAnonymousAttribute>() != null;

                if (hasAllowAnonymous)
                    return;
            }

            var user = context.HttpContext.Items["User"] as User;

            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

                context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            }
        }
    }
}
