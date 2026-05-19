namespace TFC_RelevosFamiliares.Pages
{
    [QueryProperty(nameof(FechaSeleccionada), "fecha")]
    public partial class HorasNocturno : ContentPage
    {
        public string FechaSeleccionada { get; set; }

        public HorasNocturno()
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
                        $"///horasSueltasNocturno?fecha={Uri.EscapeDataString(FechaSeleccionada)}");
                    return;
                }

                // Caso normal: rangos fijos → convertir a formato válido
                var tipo = "Servicio nocturno";

                string hora = texto switch
                {
                    "De 22:00 a 08:00" => "22:00 - 08:00",
                    _ => texto
                };

                await Shell.Current.GoToAsync(
                    $"///confirmarReserva?fecha={Uri.EscapeDataString(FechaSeleccionada)}&hora={Uri.EscapeDataString(hora)}&tipo={Uri.EscapeDataString(tipo)}");
            }
        }
    }
}
