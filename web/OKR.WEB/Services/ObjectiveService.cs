using System.Net.Http.Json;
using OKR.Communication.Response;

namespace OKR.WEB.Services;

public class ObjectiveService
{
  private readonly HttpClient _httpClient;

  public ObjectiveService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<ResponseListObjectiveJson?> GetAllAsync()
  {
    return await _httpClient.GetFromJsonAsync<ResponseListObjectiveJson>("api/objective");
  }
}
