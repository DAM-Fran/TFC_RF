namespace TFC_RelevosFamiliares;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        var route = Shell.Current.CurrentState.Location.OriginalString;

        // LOGIN o HOME → cerrar app
        if (route.Contains("login") || route.Contains("home"))
            return base.OnBackButtonPressed();

        // REGISTER → volver a login
        if (route.Contains("register"))
        {
            Shell.Current.GoToAsync("///login");
            return true;
        }

        // Cualquier otra pantalla → volver atrás
        Shell.Current.GoToAsync("..");
        return true;
    }




    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        Application.Current.UserAppTheme = e.Value
            ? AppTheme.Dark
            : AppTheme.Light;
    }

    private void OnThemeClicked(object sender, EventArgs e)
    {
        Application.Current.UserAppTheme =
            Application.Current.UserAppTheme == AppTheme.Dark
                ? AppTheme.Light
                : AppTheme.Dark;
    }



}
