using System.Globalization;

namespace TFC_RelevosFamiliares.Pages
{
    [QueryProperty(nameof(FechaSeleccionada), "fecha")]
    public partial class HorasSueltasNocturno : ContentPage
    {
        public string FechaSeleccionada { get; set; }

        public HorasSueltasNocturno()
        {
            InitializeComponent();
            GenerarHoras();
        }

        private void GenerarHoras()
        {
            // 22:00 → 24:00
            for (int hora = 22; hora < 24; hora++)
            {
                string inicio = hora.ToString("00") + ":00";
                string fin = ((hora + 1) % 24).ToString("00") + ":00";
                string texto = $"{inicio} - {fin}";

                CrearCheckbox(texto);
            }

            // 00:00 → 08:00
            for (int hora = 0; hora < 8; hora++)
            {
                string inicio = hora.ToString("00") + ":00";
                string fin = (hora + 1).ToString("00") + ":00";
                string texto = $"{inicio} - {fin}";

                CrearCheckbox(texto);
            }
        }

        private void CrearCheckbox(string texto)
        {
            var checkbox = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
                BindingContext = texto
            };

            var label = new Label
            {
                Text = texto,
                VerticalOptions = LayoutOptions.Center
            };

            var layout = new HorizontalStackLayout
            {
                Spacing = 10,
                Children = { checkbox, label }
            };

            HorasContainer.Children.Add(layout);
        }

        private async void OnContinuarClicked(object sender, EventArgs e)
        {
            var horasSeleccionadas = new List<string>();

            foreach (var item in HorasContainer.Children)
            {
                if (item is HorizontalStackLayout hsl &&
                    hsl.Children[0] is CheckBox cb &&
                    cb.IsChecked)
                {
                    horasSeleccionadas.Add(cb.BindingContext.ToString());
                }
            }

            if (horasSeleccionadas.Count == 0)
            {
                await DisplayAlert("Aviso", "Debes seleccionar al menos una hora.", "OK");
                return;
            }

            string horas = string.Join("|", horasSeleccionadas);
            string tipo = "Servicio nocturno (horas sueltas)";

            await Shell.Current.GoToAsync(
                $"///confirmarReserva?fecha={Uri.EscapeDataString(FechaSeleccionada)}&hora={Uri.EscapeDataString(horas)}&tipo={Uri.EscapeDataString(tipo)}");
        }
    }
}
