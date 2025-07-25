namespace OKR.WEB.Components.Login;

public partial class Login
{
  private readonly LoginModel _loginModel = new();
  private string? _errorMessage;

  private async Task HandleLogin()
  {
    bool success = await LoginService.Login(email: _loginModel.Email, senha: _loginModel.Password);
    if (success)
    {
      _errorMessage = "Sucesso!";
      NavigationManager.Refresh();
    }
    else
    {
      _errorMessage = "E-mail ou senha incorretos!";
    }
  }

  public class LoginModel
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
}
