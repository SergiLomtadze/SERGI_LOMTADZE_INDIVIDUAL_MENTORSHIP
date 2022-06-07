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
                        
                        switch (exception?.Error)
                        {
                            case NotFoundException:
                                statusCode = StatusCodes.Status404NotFound;
                                await c.Response.WriteAsync(exception.Error.Message);
                                break;
                            case ValidationException:
                                statusCode = StatusCodes.Status400BadRequest;
                                var validationException = (ValidationException)exception.Error;
                                await c.Response.WriteAsJsonAsync(validationException.Errors);
                                break;
                            default:
                                statusCode = StatusCodes.Status500InternalServerError;
                                var message = exception?.Error.Message;
                                await c.Response.WriteAsync(message??string.Empty);
                                break;
                        };

                        c.Response.StatusCode = statusCode;
                    }
                }
            };
        }
    }
}
