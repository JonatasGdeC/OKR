using MudBlazor;
using OKR.Communication.Response;
using OKR.WEB.Components.Objectives.Dialogs.DialogDeleteObjective;
using OKR.WEB.Components.Objectives.Dialogs.DialogRegisterObjective;
using OKR.WEB.Components.Objectives.Dialogs.DialogUpdateObjective;

namespace OKR.WEB.Components.Objectives.ListObjectives;

public partial class ListObjectives
{
  private List<ResponseObjectiveJson> ListAllObjectives => ObjectiveService.ListObjectives;
  private bool _isLoading = true;

  protected override async Task OnInitializedAsync()
  {
    ObjectiveService.OnObjectivesChanged += OnObjectivesChanged;
    await ObjectiveService.GetAllObjetivesAsync();
    _isLoading = false;
    StateHasChanged();
  }

  private async Task OpenDialogRegisterObjective()
  {
    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
    await DialogService.ShowAsync<DialogRegisterObjective>(String.Empty, options);
  }

  private async Task OpenDialogUpdateObjective(ResponseObjectiveJson objective)
  {
    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
    var parameters = new DialogParameters { { "Objective", objective } };
    await DialogService.ShowAsync<DialogUpdateObjective>(String.Empty, parameters, options);
  }

  private async Task OpenDialogDeleteObjective(Guid objectiveId)
  {
    var options = new DialogOptions { CloseOnEscapeKey = true };
    var parameters = new DialogParameters { { "ObjectiveId", objectiveId } };
    await DialogService.ShowAsync<DialogDeleteObjective>(String.Empty, parameters, options);
  }


  private void OnObjectivesChanged() => InvokeAsync(StateHasChanged);
  public void Dispose() => ObjectiveService.OnObjectivesChanged -= OnObjectivesChanged;
}
