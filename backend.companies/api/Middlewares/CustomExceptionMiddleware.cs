using application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace api.Middlewares
{
    public class CustomExceptionMiddleware
    {
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private readonly RequestDelegate _next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await Handle(context, e);
            }
        }

        private Task Handle(HttpContext context, Exception occured)
        {
            var responseCode = HttpStatusCode.InternalServerError;
            var responseMessage = string.Empty;

            switch (occured)
            {
                case ValidationException validationException:
                    responseCode = HttpStatusCode.BadRequest;
                    responseMessage = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case CustomerNotFoundException:
                    responseCode = HttpStatusCode.NotFound;
                    break;
                case CustomerWithSameNameException:
                    responseCode = HttpStatusCode.Conflict;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;

            if (responseMessage == string.Empty)
            {
                responseMessage = JsonSerializer.Serialize(new { midlewareHandledErrorMessage = occured.Message });
            }

            return context.Response.WriteAsync(responseMessage);
        }
    }
}
