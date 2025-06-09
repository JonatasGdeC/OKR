namespace OKR.Communication.Requests;

public class RequestRegisterActionJson
{
  public Guid KeyResultId { get; set; }
  public required string Description { get; set; }
  public string? ActionWeek { get; set; }
  public string? Notes { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }
  public int CurrentProgress { get; set; }
}
