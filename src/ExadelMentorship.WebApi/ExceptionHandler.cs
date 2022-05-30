using Microsoft.AspNetCore.Diagnostics;

namespace ExadelMentorship.WebApi
{
    public static class ExceptionHandler
    {
        public static ExceptionHandlerOptions GetExceptionHandlerOptions()
        {
            return new ExceptionHandlerOptions()
            {
                ExceptionHandler = (c) =>
                {
                    var exception = c.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exception?.Error.GetType().Name switch
                    {
                        "NotFoundException" => StatusCodes.Status404NotFound,
                        "FormatException" => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    c.Response.StatusCode = statusCode;
                    var message = exception is not null ? exception.Error.Message : String.Empty;
                    c.Response.WriteAsync(message);

                    return Task.CompletedTask;
                }
            };
        }
    }
}
