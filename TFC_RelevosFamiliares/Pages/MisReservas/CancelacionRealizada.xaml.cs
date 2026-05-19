namespace TFC_RelevosFamiliares.Pages;

public partial class CancelacionRealizada : ContentPage
{
    public CancelacionRealizada()
    {
        InitializeComponent();
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///home");
    }
}
