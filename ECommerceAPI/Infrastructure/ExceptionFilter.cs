using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceAPI.Infrastructure;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILog _logger;

    public ExceptionFilter()
    {
        _logger = LogManager.GetLogger(typeof(ExceptionFilter));
    }

    public void OnException(ExceptionContext context)
    {

        if (context.Exception is NotFoundException notFoundEx)
        {
            _logger.Warn("Resource not found.", notFoundEx);

            context.Result = new JsonResult(new { message = notFoundEx.Message })
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            return;
        }

        else if (context.Exception is BadRequestException badRequestEx)
        {
            _logger.Warn("Bad request.", badRequestEx);

            context.Result = new JsonResult(new { message = badRequestEx.Message })
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            return;
        }

        else if (context.Exception is UnauthorizedException unauthEx)
        {
            _logger.Warn("Invalid credentials.", unauthEx);

            context.Result = new JsonResult(new { message = unauthEx.Message })
            {
                StatusCode = StatusCodes.Status404NotFound
            };
            return;
        }

        else
        {
            _logger.Error("An unhandled exception occurred.", context.Exception);

            context.Result = new JsonResult(new { message = "Oops, something went wrong." })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
