using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace OKR.WEB.Components.Objectives.Dialogs.DialogDeleteObjective;

public partial class DialogDeleteObjective
{
  [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }
  [Parameter] public required Guid ObjectiveId { get; set; }

  private async Task Submit()
  {
    await ObjectiveService.DeleteObjectiveAsync(ObjectiveId);
    MudDialog.Close();
  }

  private void Cancel() => MudDialog.Cancel();
}
