using Microsoft.AspNetCore.Components;
using MudBlazor;
using OKR.Communication.Response;
using OKR.WEB.Components.KeyResult.Dialogs.DialogDeleteKeyResult;
using OKR.WEB.Components.KeyResult.Dialogs.DialogRegisterKeyResult;
using OKR.WEB.Components.KeyResult.Dialogs.DialogUpdateKeyResult;

namespace OKR.WEB.Components.KeyResult.ListKeyResult;

public partial class ListKeyResult
{
  [Parameter] public required Guid ObjectiveId { get; set; }

  private List<ResponseKeyResultJson> ListAllKeyResult => KeyResultService.ListKeyResults(ObjectiveId);
  private bool _isLoading = true;

  protected override async Task OnInitializedAsync()
  {
    KeyResultService.OnKeyResultChanged += OnKeyResultChanged;
    await KeyResultService.GetKeyResultByIdAsync(objectivId: ObjectiveId);
    _isLoading = false;
    StateHasChanged();
  }

  private async Task OpenDialogRegisterKeyResult()
  {
    var parameters = new DialogParameters { { "ObjectiveId", ObjectiveId } };
    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
    await DialogService.ShowAsync<DialogRegisterKeyResult>(String.Empty, parameters, options);
  }

  private async Task OpenDialogDeleteKeyResult(ResponseKeyResultJson keyResult)
  {
    var parameters = new DialogParameters { { "KeyResult", keyResult } };
    var options = new DialogOptions { CloseOnEscapeKey = true };
    await DialogService.ShowAsync<DialogDeleteKeyResult>(String.Empty, parameters, options);
  }

  private async Task OpenDialogUpdateKeyResult(ResponseKeyResultJson keyResult)
  {
    var parameters = new DialogParameters { { "KeyResult", keyResult } };
    var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
    await DialogService.ShowAsync<DialogUpdateKeyResult>(String.Empty, parameters, options);
  }

  private void OnKeyResultChanged() => InvokeAsync(StateHasChanged);
  public void Dispose() => KeyResultService.OnKeyResultChanged -= OnKeyResultChanged;
}
