using Microsoft.AspNetCore.Components;
using MudBlazor;
using OKR.Communication.Requests;

namespace OKR.WEB.Components.Objectives.Dialogs.DialogRegisterObjective;

public partial class DialogRegisterObjective
{
  [CascadingParameter] public required IMudDialogInstance MudDialog { get; set; }

  private RequestRegisterObjectiveJson _request =  new RequestRegisterObjectiveJson
  {
    Title = string.Empty,
    Year = DateTime.Now.Year,
    Quarter = (DateTime.Now.Month - 1) / 3 + 1
  };

  private async Task Submit()
  {
    await ObjectiveService.RegisterObjectiveAsync(_request);
    MudDialog.Close();
  }

  private void Cancel() => MudDialog.Cancel();
}
