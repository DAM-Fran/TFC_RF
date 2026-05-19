namespace TFC_RelevosFamiliares.Pages;

[QueryProperty(nameof(ReservaId), "id")]
public partial class ConfirmarCancelacion : ContentPage
{
    public string ReservaId { get; set; }

    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public ConfirmarCancelacion()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    private async void OnCancelarDefinitivo(object sender, EventArgs e)
    {
        await _tokenService.LoadTokensAsync();

        if (string.IsNullOrWhiteSpace(ReservaId))
        {
            await DisplayAlert("Error", "No se recibió el ID de la reserva.", "OK");
            return;
        }

        bool ok = await _api.DeleteAppointmentAsync(ReservaId);

        if (!ok)
        {
            await DisplayAlert("Error", "No se pudo cancelar la reserva.", "OK");
            return;
        }

        await Shell.Current.GoToAsync("//cancelacionrealizada");
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//misreservas");
    }
}
