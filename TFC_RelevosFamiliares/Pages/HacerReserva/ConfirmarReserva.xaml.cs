using TFC_RelevosFamiliares.Models.Appointment;

namespace TFC_RelevosFamiliares.Pages;

[QueryProperty(nameof(Fecha), "fecha")]
[QueryProperty(nameof(Hora), "hora")]
[QueryProperty(nameof(Tipo), "tipo")]
public partial class ConfirmarReserva : ContentPage
{
    public string Fecha { get; set; }
    public string Hora { get; set; }
    public string Tipo { get; set; }

    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public ConfirmarReserva()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _tokenService.LoadTokensAsync();

        // FECHA
        if (!DateTime.TryParse(Fecha, out var fechaParsed))
            FechaLabel.Text = "Fecha: (no recibida)";
        else
            FechaLabel.Text = "Fecha: " + fechaParsed.ToString("dd-MM-yyyy");

        // SERVICIO
        TipoLabel.Text = $"Servicio: {Tipo}";

        // HORAS
        if (string.IsNullOrEmpty(Hora))
        {
            HoraLabel.Text = "Hora: (no recibida)";
            return;
        }

        if (Hora.Contains("|"))
        {
            var lista = Hora.Split('|');
            HoraLabel.Text = "Horas:\n\n" + string.Join("\n\n", lista);
        }
        else
        {
            HoraLabel.Text = $"Hora: {Hora}";
        }
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        // VALIDAR FECHA
        if (!DateTime.TryParse(Fecha, out var fechaParsed))
        {
            await DisplayAlert("Error", "Fecha inválida", "OK");
            return;
        }

        // PROCESAR HORAS
        List<(DateTime inicio, DateTime fin)> rangos = new();

        if (Hora.Contains("|"))
        {
            foreach (var h in Hora.Split('|'))
            {
                if (TryParseHora(h, fechaParsed, out var ini, out var fin))
                    rangos.Add((ini, fin));
            }
        }
        else
        {
            if (TryParseHora(Hora, fechaParsed, out var ini, out var fin))
                rangos.Add((ini, fin));
        }

        if (rangos.Count == 0)
        {
            await DisplayAlert("Error", "No se pudieron interpretar las horas seleccionadas.", "OK");
            return;
        }

        // CREAR RESERVAS UNA A UNA
        foreach (var r in rangos)
        {
            var req = new AppointmentCreateRequest
            {
                ClientId = null, // el backend lo obtiene del token
                CaregiverId = null, // aún no hay selección de cuidador
                StartsAt = r.inicio,
                EndsAt = r.fin,
                Description = Tipo
            };

            bool ok = await _api.CreateAppointmentAsync(req);

            if (!ok)
            {
                await DisplayAlert("Error", "No se pudo crear la reserva.", "OK");
                return;
            }
        }

        // TODO OK → navegar
        await Shell.Current.GoToAsync(
            $"///reservaSolicitada?fecha={Fecha}&hora={Hora}&tipo={Tipo}");
    }

    private bool TryParseHora(string texto, DateTime fecha, out DateTime inicio, out DateTime fin)
    {
        inicio = fin = default;

        // formato esperado: "08:00 - 09:00"
        var partes = texto.Split('-');
        if (partes.Length != 2)
            return false;

        if (!TimeSpan.TryParse(partes[0].Trim(), out var tsInicio))
            return false;

        if (!TimeSpan.TryParse(partes[1].Trim(), out var tsFin))
            return false;

        inicio = fecha.Date + tsInicio;
        fin = fecha.Date + tsFin;

        return true;
    }
}
