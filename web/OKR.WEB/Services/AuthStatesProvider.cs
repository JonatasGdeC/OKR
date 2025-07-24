using System.Security.Claims;
using System.Text.Json;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;

public class AuthStatesProvider : AuthenticationStateProvider
{
  private readonly ISessionStorageService _sessionStorage;
  private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

  public AuthStatesProvider(ISessionStorageService sessionStorage)
  {
    _sessionStorage = sessionStorage;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    var token = await _sessionStorage.GetItemAsync<string>("authToken");

    if (string.IsNullOrWhiteSpace(token))
      return new AuthenticationState(_anonymous);

    var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    var user = new ClaimsPrincipal(identity);

    return new AuthenticationState(user);
  }

  public void NotifyUserAuthentication(string token)
  {
    var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    var user = new ClaimsPrincipal(identity);
    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
  }

  public void NotifyUserLogout()
  {
    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
  }

  private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
  {
    var payload = jwt.Split('.')[1];
    var jsonBytes = ParseBase64WithoutPadding(payload);
    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)!;

    return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
  }

  private byte[] ParseBase64WithoutPadding(string base64)
  {
    switch (base64.Length % 4)
    {
      case 2: base64 += "=="; break;
      case 3: base64 += "="; break;
    }
    return Convert.FromBase64String(base64);
  }
}
