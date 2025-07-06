using System.Net;

namespace OKR.Exception.ExceptionBase;

public class NotFoundException : OkrException
{
  public NotFoundException(string message) : base(message: message)
  {
  }

  public override int StatusCode => (int)HttpStatusCode.NotFound;

  public override List<string> GetErrors()
  {
    return [Message];
  }
}
