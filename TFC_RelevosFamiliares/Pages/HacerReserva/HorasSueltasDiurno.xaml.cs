using System.Globalization;

namespace TFC_RelevosFamiliares.Pages
{
    [QueryProperty(nameof(FechaSeleccionada), "fecha")]
    public partial class HorasSueltasDiurno : ContentPage
    {
        public string FechaSeleccionada { get; set; }

        public HorasSueltasDiurno()
        {
            InitializeComponent();
            GenerarHoras();
        }

        private void GenerarHoras()
        {
            // Rango 8:00 → 22:00
            for (int hora = 8; hora < 22; hora++)
            {
                string inicio = hora.ToString("00") + ":00";
                string fin = (hora + 1).ToString("00") + ":00";
                string texto = $"{inicio} - {fin}";

                var checkbox = new CheckBox
                {
                    VerticalOptions = LayoutOptions.Center
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

                // Guardamos el texto de la hora en el CheckBox
                checkbox.BindingContext = texto;

                HorasContainer.Children.Add(layout);
            }
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

            // Unimos todas las horas con |
            string horas = string.Join("|", horasSeleccionadas);

            string tipo = "Servicio diurno (horas sueltas)";

            await Shell.Current.GoToAsync(
                $"///confirmarReserva?fecha={Uri.EscapeDataString(FechaSeleccionada)}&hora={Uri.EscapeDataString(horas)}&tipo={Uri.EscapeDataString(tipo)}");
        }
    }
}
