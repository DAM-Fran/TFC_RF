namespace TFC_RelevosFamiliares.Pages.Empleado.MisServicios;

public partial class EmpleadoCancelacionRealizada : ContentPage
{
    public EmpleadoCancelacionRealizada()
    {
        InitializeComponent();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///empleadoinicio");
    }
}
