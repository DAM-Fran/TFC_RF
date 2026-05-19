using TFC_RelevosFamiliares.Models.User;

namespace TFC_RelevosFamiliares.Pages;

public partial class Register : ContentPage
{
    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public Register()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        string phone = PhoneEntry.Text?.Trim();
        string email = EmailEntry.Text?.Trim();
        string password = PasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        // VALIDACIONES
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(phone) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
            return;
        }

        if (password.Length < 6)
        {
            await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
            return;
        }

        // 1) REGISTRO
        bool registerOk = await _api.RegisterAsync(email, password);
        if (!registerOk)
        {
            await DisplayAlert("Error", "No se pudo crear la cuenta", "OK");
            return;
        }

        // 2) LOGIN AUTOMÁTICO
        bool loginOk = await _api.LoginAsync(email, password, rememberMe: true);
        if (!loginOk)
        {
            await DisplayAlert("Error", "Error al iniciar sesión tras el registro", "OK");
            return;
        }

        // 3) PATCH /users/me → guardar nombre + teléfono
        var update = new UserInfoUpdate
        {
            Name = name,
            Phone = phone
        };

        bool updateOk = await _api.UpdateMeAsync(update);
        if (!updateOk)
        {
            await DisplayAlert("Error", "No se pudo guardar la información del perfil", "OK");
            return;
        }

        // 4) HOME
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
    }
}
