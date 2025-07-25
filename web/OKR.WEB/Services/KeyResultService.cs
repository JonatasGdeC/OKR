using System.Net.Http.Json;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.WEB.Services;

public class KeyResultService(HttpClient httpClient)
{
  private Dictionary<Guid, List<ResponseKeyResultJson>> _keyResults = new();
  public List<ResponseKeyResultJson> ListKeyResults(Guid objectiveId) =>  _keyResults[objectiveId];
  public event Action? OnKeyResultChanged;
  private void NotifyChange() => OnKeyResultChanged?.Invoke();

  public async Task GetKeyResultByIdAsync(Guid objectivId)
  {
    var response = await httpClient.GetFromJsonAsync<ResponseListKeyResultJson>($"api/keyresult/{objectivId}");
    _keyResults[objectivId] = response?.ListKeyResults!;
    NotifyChange();
  }

  public async Task RegisterKeyResultAsync(RequestRegisterKeyResultJson request)
  {
    var response = await httpClient.PostAsJsonAsync("api/keyresult", request);
    var content = await response.Content.ReadFromJsonAsync<ResponseKeyResultJson>();
    _keyResults[request.ObjectiveId].Add(content!);
    NotifyChange();
  }

  public async Task UpdateKeyResultAsync(ResponseKeyResultJson keyResult, RequestRegisterKeyResultJson request)
  {
    var response = await httpClient.PutAsJsonAsync($"api/keyresult/{keyResult.Id}", request);
    response.EnsureSuccessStatusCode();
    _keyResults[keyResult.ObjectiveId].First(responseKeyResult => responseKeyResult.Id == keyResult.Id).Title = request.Title;
    NotifyChange();
  }

  public async Task DeleteKeyResultAsync(ResponseKeyResultJson keyResult)
  {
    var response = await httpClient.DeleteAsync($"api/keyresult/{keyResult.Id}");
    response.EnsureSuccessStatusCode();
    var responseKeyResult = _keyResults[keyResult.ObjectiveId].FirstOrDefault(kr => kr.Id == keyResult.Id);
    if (responseKeyResult != null)
    {
      _keyResults[keyResult.ObjectiveId].Remove(keyResult);
    }
    NotifyChange();
  }
}
