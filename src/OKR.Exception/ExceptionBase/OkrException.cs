namespace OKR.Exception.ExceptionBase;

public abstract class OkrException : System.Exception
{
  protected OkrException(string messagr) : base(messagr) { }

  public abstract int StatusCode { get; }
  public abstract List<string> GetErrors();
}
