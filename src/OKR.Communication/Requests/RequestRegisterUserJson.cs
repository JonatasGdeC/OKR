namespace OKR.Communication.Requests;

public class RequestRegisterUserJson
{
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Password { get; set; }
}
