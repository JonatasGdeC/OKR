using Microsoft.AspNetCore.Components;
using MudBlazor;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.WEB.Components.Objectives.Dialogs.DialogUpdateObjective;

public partial class DialogUpdateObjective
{
  [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }
  [Parameter] public required ResponseObjectiveJson Objective { get; set; }

  private string _objectiveTitle = string.Empty;

  protected override void OnInitialized()
  {
    _objectiveTitle = Objective.Title;
  }

  private async Task Submit()
  {
    await ObjectiveService.UpdateObjectiveAsync(objectiveId: Objective.Id, new RequestUpdateObjectiveJson
    {
      Title = _objectiveTitle
    });

    MudDialog.Close();
  }

  private void Cancel() => MudDialog.Cancel();
}
