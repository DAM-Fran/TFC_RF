namespace TFC_RelevosFamiliares.Pages;

public partial class MisReservasVacio : ContentPage
{
    public MisReservasVacio()
    {
        InitializeComponent();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///home");
    }

    private async void OnReservarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///calendario");
    }
}
