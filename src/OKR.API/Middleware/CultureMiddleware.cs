using System.Globalization;

namespace OKR.API.Middleware;

public class CultureMiddleware
{
  private readonly RequestDelegate _next;

  public CultureMiddleware(RequestDelegate next)
  {
    _next = next;
  }
  public async Task Invoke(HttpContext context)
  {
    var supportedCultures = CultureInfo.GetCultures(types: CultureTypes.AllCultures).ToList();

    var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

    var cultureInfo = new CultureInfo(name: "en");

    if (!string.IsNullOrWhiteSpace(value: requestedCulture) && supportedCultures.Exists(match: language => language.Name.Equals(value: requestedCulture)))
    {
      cultureInfo = new CultureInfo(name: requestedCulture);
    }
    CultureInfo.CurrentCulture = cultureInfo;
    CultureInfo.CurrentUICulture = cultureInfo;

    await _next(context: context);
  }}
