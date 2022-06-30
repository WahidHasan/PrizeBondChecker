using Application.Shared.Models;
using System.Net;
using System.Text.Json;

namespace PrizeBondChecker.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //var response = context.Response;

            var errorResponse = new CommonApiResponses
            {
                IsSuccess = false
            };
            switch (exception)
            {
                case ApplicationException:
                    if (exception.Message.Contains("Invalid token"))
                    {
                        //response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.StatusDetails = "Forbidden";
                        errorResponse.Message = exception.Message;
                        break;
                    }
                    //response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.StatusDetails = "Bad Request";
                    errorResponse.Message = exception.Message;
                    break;
                case KeyNotFoundException:
                    //response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.StatusDetails = "Not Found";
                    errorResponse.Message = exception.Message;
                    break;
                default:
                    //response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusDetails = "Internal Server Error";
                    errorResponse.Message = exception.Message;
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);

        }
    }
}
