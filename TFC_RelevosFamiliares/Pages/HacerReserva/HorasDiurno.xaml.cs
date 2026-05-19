namespace TFC_RelevosFamiliares.Pages
{
    [QueryProperty(nameof(FechaSeleccionada), "fecha")]
    public partial class HorasDiurno : ContentPage
    {
        public string FechaSeleccionada { get; set; }

        public HorasDiurno()
        {
            InitializeComponent();
        }

        private async void OnHoraClicked(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                var texto = btn.Text;

                // Caso especial: Horas sueltas → pantalla nueva
                if (texto == "Horas sueltas")
                {
                    await Shell.Current.GoToAsync(
                        $"///horasSueltasDiurno?fecha={Uri.EscapeDataString(FechaSeleccionada)}");
                    return;
                }

                // Caso normal: rangos fijos → convertir a formato válido
                var tipo = "Servicio diurno";

                string hora = texto switch
                {
                    "De 8:00 a 15:00" => "08:00 - 15:00",
                    "De 15:00 a 22:00" => "15:00 - 22:00",
                    _ => texto
                };

                await Shell.Current.GoToAsync(
                    $"///confirmarReserva?fecha={Uri.EscapeDataString(FechaSeleccionada)}&hora={Uri.EscapeDataString(hora)}&tipo={Uri.EscapeDataString(tipo)}");
            }
        }
    }
}
