using Microsoft.AspNetCore.Components;
using MudBlazor;
using OKR.Communication.Requests;

namespace OKR.WEB.Components.KeyResult.Dialogs.DialogRegisterKeyResult;

public partial class DialogRegisterKeyResult
{
  [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }
  [Parameter] public Guid ObjectiveId { get; set; }

  private string _title = string.Empty;

  private async Task Submit()
  {
    await KeyResultService.RegisterKeyResultAsync(new RequestRegisterKeyResultJson
    {
      ObjectiveId = ObjectiveId,
      NumberKr = KeyResultService.ListKeyResults(ObjectiveId).Count + 1,
      Title = _title
    });
    MudDialog.Close();
  }

  private void Cancel() => MudDialog.Cancel();
}
