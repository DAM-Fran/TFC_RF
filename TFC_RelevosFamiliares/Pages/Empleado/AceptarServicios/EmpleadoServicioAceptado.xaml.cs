namespace TFC_RelevosFamiliares.Pages.Empleado.AceptarServicios;

public partial class EmpleadoServicioAceptado : ContentPage
{
    public EmpleadoServicioAceptado()
    {
        InitializeComponent();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///empleadoinicio");
    }
}
