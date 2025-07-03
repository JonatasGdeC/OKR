namespace OKR.Communication.Response;

public class ResponseRegisteredUserJson
{
  public required string Name { get; set; }
  public required string Email { get; set; }
  public string Token { get; set; } = String.Empty;
}
