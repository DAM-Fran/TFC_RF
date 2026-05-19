namespace TFC_RelevosFamiliares.Pages
{
    public partial class ReservaSolicitada : ContentPage
    {
        public ReservaSolicitada()
        {
            InitializeComponent();
        }

        private async void OnVolverClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///home");
        }
    }
}
