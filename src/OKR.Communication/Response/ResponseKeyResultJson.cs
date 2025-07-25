namespace OKR.Communication.Response;

public class ResponseKeyResultJson
{
  public Guid Id { get; init; }
  public Guid ObjectiveId { get; init; }
  public required int NumberKr { get; init; }
  public required string Title { get; set; }
  public DateTime CreateDate { get; init; }
}
