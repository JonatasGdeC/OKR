using System.Net.Http.Json;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.WEB.Services;

public class ObjectiveService(HttpClient httpClient)
{
  private List<ResponseObjectiveJson> _objectives = [];
  public List<ResponseObjectiveJson> ListObjectives => _objectives;
  public event Action? OnObjectivesChanged;
  private void NotifyChange() => OnObjectivesChanged?.Invoke();

  public async Task GetAllObjetivesAsync()
  {
    var response = await httpClient.GetFromJsonAsync<ResponseListObjectiveJson>("api/objective");
    _objectives = response?.ListObjectives!;
    NotifyChange();
  }

  public async Task RegisterObjectiveAsync(RequestRegisterObjectiveJson request)
  {
    var response = await httpClient.PostAsJsonAsync("api/objective", request);
    var content = await response.Content.ReadFromJsonAsync<ResponseObjectiveJson>();
    _objectives.Add(content!);
    NotifyChange();
  }

  public async Task UpdateObjectiveAsync(Guid objectiveId, RequestUpdateObjectiveJson request)
  {
    var response = await httpClient.PutAsJsonAsync($"api/objective/{objectiveId}", request);
    response.EnsureSuccessStatusCode();
    _objectives.First(objective => objective.Id == objectiveId).Title = request.Title;
    NotifyChange();
  }

  public async Task DeleteObjectiveAsync(Guid objectiveId)
  {
    var response = await httpClient.DeleteAsync($"api/objective/{objectiveId}");
    response.EnsureSuccessStatusCode();
    var objective = _objectives.FirstOrDefault(o => o.Id == objectiveId);
    if (objective != null)
    {
      _objectives.Remove(objective);
    }
    NotifyChange();
  }
}
