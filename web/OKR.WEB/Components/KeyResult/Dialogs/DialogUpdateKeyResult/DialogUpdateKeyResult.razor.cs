using Microsoft.AspNetCore.Components;
using MudBlazor;
using OKR.Communication.Requests;
using OKR.Communication.Response;

namespace OKR.WEB.Components.KeyResult.Dialogs.DialogUpdateKeyResult;

public partial class DialogUpdateKeyResult
{
  [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }
  [Parameter] public required ResponseKeyResultJson KeyResult { get; set; }

  private string _keyResultTitle = string.Empty;

  protected override void OnInitialized()
  {
    _keyResultTitle = KeyResult.Title;
  }

  private async Task Submit()
  {
    await KeyResultService.UpdateKeyResultAsync(keyResult: KeyResult, new RequestRegisterKeyResultJson
    {
      ObjectiveId = KeyResult.ObjectiveId,
      NumberKr = KeyResult.NumberKr,
      Title = _keyResultTitle
    });

    MudDialog.Close();
  }

  private void Cancel() => MudDialog.Cancel();
}
