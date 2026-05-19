namespace TFC_RelevosFamiliares.Pages;

public partial class MisReservas : ContentPage
{
    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public MisReservas()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _tokenService.LoadTokensAsync();

        var reservas = await _api.GetMyAppointmentsAsync();

        if (reservas == null || reservas.Count == 0)
        {
            await Shell.Current.GoToAsync("//misreservasvacio");
            return;
        }

        // Adaptar datos al formato visual que espera tu XAML
        var lista = reservas.Select(r => new
        {
            Fecha = r.StartsAt.ToString("dd-MM-yyyy"),
            Hora = $"{r.StartsAt:HH:mm} - {r.EndsAt:HH:mm}",
            Tipo = r.Description,
            Id = r.Id
        }).ToList();

        ListaReservas.ItemsSource = lista;
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is not null)
        {
            var idProp = btn.BindingContext.GetType().GetProperty("Id");
            if (idProp == null) return;

            var id = idProp.GetValue(btn.BindingContext)?.ToString();
            if (id == null) return;

            await Shell.Current.GoToAsync($"///confirmarCancelacion?id={id}");
        }
    }



    /*private async void OnCancelarClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is dynamic reserva)
        {
            await Shell.Current.GoToAsync(
                $"///confirmarCancelacion?id={reserva.Id}");
        }
    }*/
}
