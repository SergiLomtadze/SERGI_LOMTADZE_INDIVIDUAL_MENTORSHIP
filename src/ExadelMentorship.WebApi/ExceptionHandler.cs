using ExadelMentorship.BusinessLogic.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

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
                    if (exception is not null)
                    {
                        int statusCode;
                        string message = string.Empty;
                        switch (exception?.Error)
                        {
                            case NotFoundException:
                                statusCode = StatusCodes.Status404NotFound;
                                message = exception.Error.Message;
                                break;
                            case ValidationException:
                                statusCode = StatusCodes.Status400BadRequest;
                                var validationException = (ValidationException)exception.Error;
                                var err = validationException.Errors;
                                await c.Response.WriteAsync(err.ToString());
                                break;
                            default:
                                statusCode = StatusCodes.Status500InternalServerError;
                                message = exception.Error.Message;
                                break;
                        };
                        c.Response.StatusCode = statusCode;
                        await c.Response.WriteAsync(message);
                    }
                }
            };
        }
    }
}
