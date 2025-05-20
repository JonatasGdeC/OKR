namespace OKR.Communication.Requests;

public class RequestRegisterKeyResultJson
{
  public required Guid ObjectiveId { get; set; }
  public int NumberKr { get; set; }
  public required string Title { get; set; }
}
