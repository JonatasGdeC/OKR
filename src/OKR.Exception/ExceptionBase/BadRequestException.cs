using System.Net;

namespace OKR.Exception.ExceptionBase;

public class BadRequestException : OkrException
{
  public BadRequestException(string message) : base(message: message)
  {
  }

  public override int StatusCode => (int)HttpStatusCode.BadRequest;

  public override List<string> GetErrors()
  {
    return [Message];
  }
}
