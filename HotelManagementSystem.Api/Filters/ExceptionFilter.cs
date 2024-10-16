using HotelManagementSystem.Interfaces.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagementSystem.Api.Filters
{
    public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger = logger;

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An error occurred during processing the request.");

            context.Result = context.Exception switch
            {
                ArgumentNullException => new ObjectResult(new { message = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                },
                NotFoundException => new NotFoundObjectResult(context.Exception.Message),
                ValidationException => new UnprocessableEntityObjectResult(context.Exception.Message),
                _ => new StatusCodeResult(500)
            };

            context.ExceptionHandled = true;
        }
    }
}
