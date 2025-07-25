namespace OKR.Communication.Response;

public class ResponseObjectiveJson
{
  public Guid Id { get; init; }
  public required string Title { get; set; }
  public required int Year { get; init; }
  public required int Quarter { get; init; }
  public DateTime CreateDate { get; init; }
}
