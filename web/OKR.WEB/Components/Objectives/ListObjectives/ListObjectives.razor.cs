using OKR.Communication.Response;

namespace OKR.WEB.Components.Objectives.ListObjectives;

public partial class ListObjectives
{
  private List<ResponseObjectiveJson> _listObjectives = [];
  private bool _isLoading = true;

  protected override async Task OnInitializedAsync()
  {
    var response = await ObjectiveService.GetAllAsync();
    if (response != null)
    {
      _listObjectives = response.ListObjectives;
    }

    _isLoading = false;
    StateHasChanged();
  }
}
