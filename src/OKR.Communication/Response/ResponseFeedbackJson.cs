namespace OKR.Communication.Response;

public class ResponseFeedbackJson
{
  public Guid Id { get; init; }
  public Guid ActionId { get; init; }
  public required string Description { get; init; }
  public DateTime Date { get; init; }
}
