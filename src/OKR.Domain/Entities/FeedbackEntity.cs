namespace OKR.Domain.Entities;

public class FeedbackEntity
{
  public Guid Id { get; init; }
  public Guid ActionId { get; init; }
  public required string Description { get; init; }
  public DateTime Date { get; init; }
  public Guid UserId { get; set; }
  public required User User { get; set; }
}
