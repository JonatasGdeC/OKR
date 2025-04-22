using OKR.Communication.Enums;

namespace OKR.Communication.Response;

public class ResponseRegisterGuidelineJson
{
  public Guid Id { get; init; }
  public string Title { get; init; } = string.Empty;
  public GuidelineType Type { get; init; }
  public string Description { get; init; } = string.Empty;
  public DateTime CreatedAt { get; init; }
}
