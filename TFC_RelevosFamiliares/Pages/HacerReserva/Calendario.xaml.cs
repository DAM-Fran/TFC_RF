namespace TFC_RelevosFamiliares.Pages;

public partial class Calendario : ContentPage
{
	public Calendario()
	{
		InitializeComponent();
	}

    private async void OnContinuarClicked(object sender, EventArgs e)
    {
        DateTime fecha = (DateTime)FechaPicker.Date;

        string fechaFormateada = fecha.ToString("yyyy-MM-dd");

        await Shell.Current.GoToAsync(
            $"///tipoReserva?fecha={Uri.EscapeDataString(fechaFormateada)}");
    }


}