namespace TFC_RelevosFamiliares
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        /*private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Presionado {count} vez";
            else
                CounterBtn.Text = $"Presionado {count} veces";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }*/


        private async void CuandoHaceClick(object sender, EventArgs e)
        {
            // Botón de pruebas - no hacer nada de momento
        }
    }
}
