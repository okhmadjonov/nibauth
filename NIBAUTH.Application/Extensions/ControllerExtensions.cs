using Microsoft.AspNetCore.Mvc;
using NIBAUTH.Application.Common.Models;

namespace NIBAUTH.Application.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult<ApiResponse<T>> OkResponse<T>(this ControllerBase controller, T data, string message = "Success")
        {
            return controller.Ok(new ApiResponse<T> { Success = true, Message = message, Data = data });
        }

        public static ActionResult<ApiResponse<T>> BadRequestResponse<T>(this ControllerBase controller, string message)
        {
            return controller.BadRequest(new ApiResponse<T> { Success = false, Message = message });
        }

        public static ActionResult<ApiResponse<T>> NotFoundResponse<T>(this ControllerBase controller, string message)
        {
            return controller.NotFound(new ApiResponse<T> { Success = false, Message = message });
        }

        public static ActionResult<ApiResponse<T>> UnauthorizedResponse<T>(this ControllerBase controller, string message = "Unauthorized")
        {
            return controller.Unauthorized(new ApiResponse<T> { Success = false, Message = message });
        }
    }
}