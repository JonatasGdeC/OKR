using OKR.Communication.Enums;

namespace OKR.Communication.Requests;

public class RequestRegisterGuideline
{
  public required string Title { get; set; }
  public GuidelineType Type { get; set; }
  public required string Description { get; set; }
}
