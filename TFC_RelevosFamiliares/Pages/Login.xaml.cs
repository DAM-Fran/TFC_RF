namespace TFC_RelevosFamiliares.Pages;

public partial class Login : ContentPage
{
    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public Login()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _tokenService.LoadTokensAsync();

        if (_tokenService.RememberMe)
        {
            await Shell.Current.GoToAsync("//home");
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim();
        string password = PasswordEntry.Text;
        bool remember = RecordarmeCheck.IsChecked;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos", "OK");
            return;
        }

        bool ok = await _api.LoginAsync(email, password, remember);

        if (!ok)
        {
            await DisplayAlert("Error", "Credenciales incorrectas", "OK");
            return;
        }

        await Shell.Current.GoToAsync("//home");
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//register");
    }
}
