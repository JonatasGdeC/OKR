using OKR.Domain.Secury.Tokens;

namespace OKR.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public string TokenOnRequest()
  {
    string autohrization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
    return autohrization.Split(separator: " ")[1];
  }
}
