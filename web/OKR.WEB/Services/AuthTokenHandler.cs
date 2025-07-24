using System.Net.Http.Headers;
using Blazored.SessionStorage;

public class AuthTokenHandler : DelegatingHandler
{
  private readonly ISessionStorageService _sessionStorage;

  public AuthTokenHandler(ISessionStorageService sessionStorage)
  {
    _sessionStorage = sessionStorage;
  }

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    var token = await _sessionStorage.GetItemAsync<string>("authToken");

    if (!string.IsNullOrWhiteSpace(token)) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

    return await base.SendAsync(request, cancellationToken);
  }
}
