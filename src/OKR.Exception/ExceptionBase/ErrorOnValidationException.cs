namespace OKR.Exception.ExceptionBase;

public class ErrorOnValidationException : OkrException
{
  public List<string> Errors { get; set; }
  public ErrorOnValidationException(List<string> errorsMessages)
  {
    Errors = errorsMessages;
  }
}
