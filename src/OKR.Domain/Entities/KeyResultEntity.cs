namespace OKR.Domain.Entities;

public class KeyResultEntity
{
  public Guid Id { get; init; }
  public int NumberKr { get; init; }
  public required string Title { get; init; }
  public DateTime CreateDate = DateTime.UtcNow;
  public Guid UserId { get; set; }
  public required User User { get; set; }
  public Guid ObjectiveId { get; init; }
  public required ObjectiveEntity Objective { get; init; }
}
