namespace TFC_RelevosFamiliares.Pages;

[QueryProperty(nameof(FechaSeleccionada), "fecha")]
public partial class HorasAcompanamiento : ContentPage
{
    public string FechaSeleccionada { get; set; }

    public HorasAcompanamiento()
    {
        InitializeComponent();
    }

    private async void OnHoraClicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var hora = btn.Text; // ya viene en formato "08:00 - 09:00"

            await Shell.Current.GoToAsync(
                $"///confirmarReserva?fecha={Uri.EscapeDataString(FechaSeleccionada)}&hora={Uri.EscapeDataString(hora)}&tipo={Uri.EscapeDataString("Acompañamiento")}");
        }
    }
}
