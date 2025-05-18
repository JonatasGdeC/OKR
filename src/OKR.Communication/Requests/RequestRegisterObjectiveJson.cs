namespace OKR.Communication.Requests;

public class RequestRegisterObjectiveJson
{
  public required string Title { get; set; }
  public required int Year { get; set; }
  public required int Quarter { get; set; }
}
