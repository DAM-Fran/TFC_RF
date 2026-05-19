namespace TFC_RelevosFamiliares.Pages.Empleado.AceptarServicios;

[QueryProperty(nameof(Fecha), "fecha")]
[QueryProperty(nameof(Hora), "hora")]
[QueryProperty(nameof(Tipo), "tipo")]
public partial class EmpleadoConfirmarServicio : ContentPage
{
    public string Fecha { get; set; }
    public string Hora { get; set; }
    public string Tipo { get; set; }

    public EmpleadoConfirmarServicio()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        FechaLabel.Text = $"Fecha: {Fecha}";
        HoraLabel.Text = $"Hora: {Hora}";
        TipoLabel.Text = $"Servicio: {Tipo}";
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///empleadoservicioaceptado");
    }
}
