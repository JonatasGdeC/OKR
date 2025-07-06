using System.Net;

namespace OKR.Exception.ExceptionBase;

public class ErrorOnValidationException : OkrException
{
  private readonly List<string> _errors;
  public ErrorOnValidationException(List<string> errorsMessages) : base(message: string.Empty)
  {
    _errors = errorsMessages;
  }

  public override int StatusCode => (int)HttpStatusCode.BadRequest;

  public override List<string> GetErrors()
  {
    return _errors;
  }
}
