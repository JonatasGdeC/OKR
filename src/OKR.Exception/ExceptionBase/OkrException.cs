namespace OKR.Exception.ExceptionBase;

public abstract class OkrException : System.Exception
{
  protected OkrException(string message) : base(message: message) { }

  public abstract int StatusCode { get; }
  public abstract List<string> GetErrors();
}
