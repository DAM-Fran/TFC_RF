namespace TFC_RelevosFamiliares.Pages;

public partial class Home : ContentPage
{
    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public Home()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Cargar tokens si existen
        await _tokenService.LoadTokensAsync();

        var user = await _api.GetMeAsync();

        if (user == null || user.UserInfo == null || string.IsNullOrWhiteSpace(user.UserInfo.Name))
        {
            WelcomeLabel.Text = "¡Hola!";
            return;
        }

        // Primer nombre
        string firstName = user.UserInfo.Name.Split(' ')[0];
        WelcomeLabel.Text = $"¡Hola, {firstName}!";
    }

    private async void OnButton1Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//calendario");
    }

    private async void OnButton2Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//misreservas");
    }

    private async void OnButton3Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//contacto");
    }

    private async void OnLogoutTapped(object sender, EventArgs e)
    {
        await _tokenService.ClearAsync();
        await Shell.Current.GoToAsync("//login");
    }
}
