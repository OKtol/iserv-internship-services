using IservInternship.Commons.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IservInternship.BFF.Web.Filters;

public class HttpResponseExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundObjectResult(new { error = context.Exception.Message });
            context.ExceptionHandled = true;
        }
        if (context.Exception is DeleteConstraintException)
        {
            context.Result = new ConflictObjectResult(new { error = context.Exception.Message });
            context.ExceptionHandled = true;
        }
        // Optionally handle other exceptions
    }
}