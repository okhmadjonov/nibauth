using NIBAUTH.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace NIBAUTH.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.ExpectationFailed;
            var result = string.Empty;
            switch (ex)
            {
                case FluentValidation.ValidationException validationException:
                    {
                        code = HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize(validationException.Errors);
                    }
                    break;
                case NotFoundException:
                    {
                        code = HttpStatusCode.NotFound;
                    }
                    break;
                case ConflictException:
                    {
                        code = HttpStatusCode.Conflict;
                    }
                    ; break;

            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }

            return context.Response.WriteAsync(result);

        }


    }

}
