namespace TFC_RelevosFamiliares.Pages
{
    public partial class Contacto : ContentPage
    {
        public Contacto()
        {
            InitializeComponent();
        }

        private async void OnVolverTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///home");
        }

    }
}
