namespace TFC_RelevosFamiliares.Pages.Empleado;

public partial class EmpleadoSinServicios : ContentPage
{
    public EmpleadoSinServicios()
    {
        InitializeComponent();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///empleadoinicio");
    }
}
