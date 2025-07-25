namespace OKR.WEB.Layout.MainLayout;

public partial class MainLayout
{
  private bool _isAuthenticated;
  private bool _drawerOpen = true;

  protected override async Task OnInitializedAsync()
  {
    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    _isAuthenticated = authState.User.Identity?.IsAuthenticated == true;

    AuthenticationStateProvider.AuthenticationStateChanged += async (task) =>
    {
      var state = await task;
      _isAuthenticated = state.User.Identity?.IsAuthenticated == true;
      StateHasChanged();
    };
  }

  private void DrawerToggle() => _drawerOpen = !_drawerOpen;
}
