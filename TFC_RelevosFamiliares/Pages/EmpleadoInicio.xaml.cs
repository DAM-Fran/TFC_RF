namespace TFC_RelevosFamiliares.Pages;

public partial class EmpleadoInicio : ContentPage
{
    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public EmpleadoInicio()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _tokenService.LoadTokensAsync();

        var user = await _api.GetMeAsync();

        if (user?.UserInfo?.Name == null)
        {
            WelcomeLabel.Text = "¡Hola!";
            return;
        }

        string firstName = user.UserInfo.Name.Split(' ')[0];
        WelcomeLabel.Text = $"¡Hola, {firstName}!";
    }

    private async void OnAceptarServicioClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//empleadoaceptarservicio");
    }

    private async void OnMisServiciosClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//empleadomisservicios");
    }

    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        await _tokenService.ClearAsync();
        await Shell.Current.GoToAsync("//login");
    }
}
