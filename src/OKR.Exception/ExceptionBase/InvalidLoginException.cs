using System.Net;

namespace OKR.Exception.ExceptionBase;

public class InvalidLoginException : OkrException
{
  public InvalidLoginException() : base(message: "Email or password is incorrect!")
  {
  }

  public override int StatusCode => (int)HttpStatusCode.Unauthorized;
  public override List<string> GetErrors()
  {
    return [Message];
  }
}
