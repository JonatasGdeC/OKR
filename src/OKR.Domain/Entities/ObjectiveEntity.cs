namespace OKR.Domain.Entities;

public class ObjectiveEntity
{
  public Guid Id { get; init; }
  public required string Title { get; init; }
  public int Year { get; init; }
  public int Quarter { get; init; }
  public DateTime CreateDate = DateTime.UtcNow;
  public Guid UserId { get; set; }
  public required User User { get; set; }
}
