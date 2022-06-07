using ExadelMentorship.BusinessLogic.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace ExadelMentorship.WebApi
{
    public static class ExceptionHandler
    {
        public static ExceptionHandlerOptions GetExceptionHandlerOptions()
        {
            return new ExceptionHandlerOptions()
            {
                ExceptionHandler = async (c) =>
                {
                    var exception = c.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exception?.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        ValidationException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    c.Response.StatusCode = statusCode;
                    var message = exception is not null ? exception.Error.Message : String.Empty;
                    await c.Response.WriteAsync(message);
                }
            };
        }
    }
}
