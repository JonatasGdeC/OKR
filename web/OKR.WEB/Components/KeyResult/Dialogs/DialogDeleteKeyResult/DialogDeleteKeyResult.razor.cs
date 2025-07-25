using Microsoft.AspNetCore.Components;
using MudBlazor;
using OKR.Communication.Response;

namespace OKR.WEB.Components.KeyResult.Dialogs.DialogDeleteKeyResult;

public partial class DialogDeleteKeyResult
{
  [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }
  [Parameter] public required ResponseKeyResultJson KeyResult { get; set; }

  private async Task Submit()
  {
    await KeyResultService.DeleteKeyResultAsync(KeyResult);
    MudDialog.Close();
  }

  private void Cancel() => MudDialog.Cancel();
}
