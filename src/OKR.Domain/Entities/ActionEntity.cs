namespace OKR.Domain.Entities;

public class ActionEntity
{
  public Guid Id { get; init; }
  public required string Description { get; init; }
  public string? ActionWeek { get; init; }
  public string? Notes { get; init; }
  public DateTime StartDate { get; init; }
  public DateTime EndDate { get; init; }
  public int CurrentProgress { get; set; }
  public Guid UserId { get; set; }
  public required User User { get; set; }
  public Guid KeyResultId { get; init; }
  public required KeyResultEntity KeyResult { get; init; }
}
