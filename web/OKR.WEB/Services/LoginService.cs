using System.Net.Http.Json;
using Blazored.SessionStorage;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.WEB.Services;

public class LoginService
{
  private readonly HttpClient _httpClient;
  private readonly ISessionStorageService _sessionStorage;
  private readonly AuthStatesProvider _authStatesProvider;

  public LoginService(HttpClient httpClient, ISessionStorageService sessionStorage, AuthStatesProvider authStatesProvider)
  {
    _httpClient = httpClient;
    _sessionStorage = sessionStorage;
    _authStatesProvider = authStatesProvider;
  }

  public async Task<bool> Login(string email, string senha)
  {
    RequestLoginJson request = new RequestLoginJson
    {
      Email = email,
      Password = senha
    };

    var response = await _httpClient.PostAsJsonAsync("api/login", request);

    if (!response.IsSuccessStatusCode) return false;

    var content = await response.Content.ReadFromJsonAsync<ResponseRegisteredUserJson>();
    var token = content?.Token;

    if (!string.IsNullOrEmpty(token))
    {
      await _sessionStorage.SetItemAsync("authToken", token);
      _authStatesProvider.NotifyUserAuthentication(token);
      _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
      return true;
    }

    return false;
  }
}
