namespace OKR.Domain.Entities;

public class KeyResult
{
  public Guid Id { get; init; }
  public Guid ObjectiveId { get; init; }
  public int NumberKr { get; init; }
  public required string Title { get; init; }
  public DateTime CreateDate = DateTime.UtcNow;
}
