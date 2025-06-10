namespace OKR.Communication.Requests;

public class RequestRegisterFeedbackJson
{
  public Guid ActionId { get; set; }
  public required string Description { get; set; }
  public DateTime Date { get; set; }
}
