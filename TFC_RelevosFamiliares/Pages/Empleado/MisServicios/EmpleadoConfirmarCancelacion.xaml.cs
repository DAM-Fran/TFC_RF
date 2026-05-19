namespace TFC_RelevosFamiliares.Pages.Empleado.MisServicios;

[QueryProperty(nameof(ServicioId), "id")]
public partial class EmpleadoConfirmarCancelacion : ContentPage
{
    public string ServicioId { get; set; }

    public EmpleadoConfirmarCancelacion()
    {
        InitializeComponent();
    }

    private async void OnCancelarDefinitivo(object sender, EventArgs e)
    {
        // ⚠️ No hay endpoint para cancelar como caretaker → no llamamos al backend
        // En backend real sería: await _api.DeleteAppointmentAsync(ServicioId);

        await Shell.Current.GoToAsync("//empleadocancelacionrealizada");
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//empleadomisservicios");
    }
}
