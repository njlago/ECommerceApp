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

        _logger.Error("An unhandled exception occurred.", context.Exception);

        context.Result = new JsonResult(new { message = "Oops, something went wrong." })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
