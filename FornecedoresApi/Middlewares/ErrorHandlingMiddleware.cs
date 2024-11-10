using FornecedoresApi.Domain.Exceptions;
using System.Net.Mime;
using System.Text;

namespace FornecedoresApi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NoContentException ex)
            {
                HandleNoContent(context, ex);
            }
            catch (ArgumentException ex)
            {
                await HandleBadRequest(context, ex);
            }
        }

        private static void HandleNoContent(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status204NoContent;
            context.Response.Headers.Add("X-Error-Message", GetStringMessage(exception.Message));
            context.Response.Headers.Add("X-Display-Error-Message", GetStringMessage(exception.Message));
        }

        private static Task HandleBadRequest(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status400BadRequest;
            return SendResponse(context, exception, statusCode, exception.Message, "");
        }

        private static void HandleBadRequestMessage(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.Headers.Add("X-Error-Message", exception.Message);
            context.Response.Headers.Add("X-Display-Error-Message", exception.Message);
        }

        private static Task SendResponse(HttpContext context, Exception exception, int statusCode, string message, string displayMessage)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = MediaTypeNames.Text.Plain;
            context.Response.Headers.Add("X-Error-Message", GetStringMessage(message));
            context.Response.Headers.Add("X-Display-Error-Message", GetStringMessage(displayMessage));
            return context.Response.WriteAsync(exception.GetBaseException().StackTrace ?? string.Empty);
        }

        private static string GetStringMessage(string message)
        {
            if (message == null)
                return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(message);
            return System.Net.WebUtility.UrlEncode(Encoding.UTF8.GetString(bytes)).Replace("+", "%20");
        }
    }
}
