namespace TFC_RelevosFamiliares.Pages;

[QueryProperty(nameof(FechaSeleccionada), "fecha")]
public partial class TipoReserva : ContentPage
{
    public string FechaSeleccionada { get; set; }

    public TipoReserva()
    {
        InitializeComponent();
    }

    private async void OnDiurnoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(
            $"///horasDiurno?fecha={Uri.EscapeDataString(FechaSeleccionada)}");
    }

    private async void OnNocturnoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(
            $"///horasNocturno?fecha={Uri.EscapeDataString(FechaSeleccionada)}");
    }

    private async void OnConsultaClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(
            $"///horasConsulta?fecha={Uri.EscapeDataString(FechaSeleccionada)}");
    }

}
