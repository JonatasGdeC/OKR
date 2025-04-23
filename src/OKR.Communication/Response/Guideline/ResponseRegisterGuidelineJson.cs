using OKR.Communication.Enums;

namespace OKR.Communication.Response.Guideline;

public class ResponseRegisterGuidelineJson
{
  public Guid Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public GuidelineType Type { get; set; }
  public string Description { get; set; } = string.Empty;
  public DateTime CreatedAt { get; set; }
}
