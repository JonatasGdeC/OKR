namespace OKR.Domain.Entities;

public class Objective
{
  public Guid Id { get; init; }
  public required string Title { get; init; }
  public int Year { get; init; }
  public int Quarter { get; init; }
  public DateTime CreateDate { get; init; }

  public Objective()
  {
    CreateDate = DateTime.UtcNow;
  }
}
