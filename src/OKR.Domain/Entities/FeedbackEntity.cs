namespace OKR.Domain.Entities;

public class FeedbackEntity
{
  public Guid Id { get; init; }
  public required string Description { get; init; }
  public DateTime Date { get; init; }
  public Guid UserId { get; set; }
  public required User User { get; set; }
  public Guid ActionId { get; init; }
  public required ActionEntity Action { get; init; }
}
