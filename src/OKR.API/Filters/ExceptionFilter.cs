using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OKR.Communication.Response;
using OKR.Exception;
using OKR.Exception.ExceptionBase;

namespace OKR.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is OkrException)
    {
      HandleProjectException(context);
    }
    else
    {
      ThrowUnknowError(context);
    }
  }

  private void HandleProjectException(ExceptionContext context)
  {
    var okrException = context.Exception as OkrException;
    var errorResponse = new ResponseErrorJson(okrException!.GetErrors());

    context.HttpContext.Response.StatusCode = okrException.StatusCode;
    context.Result = new ObjectResult(errorResponse);
  }

  private void ThrowUnknowError(ExceptionContext context)
  {
    var errorResponse = new ResponseErrorJson(ResourceErrorMessage.UNKNOWN_ERROR);
    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Result = new ObjectResult(errorResponse);
  }
}
