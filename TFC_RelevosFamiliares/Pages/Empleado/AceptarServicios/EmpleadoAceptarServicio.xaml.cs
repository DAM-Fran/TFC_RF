namespace TFC_RelevosFamiliares.Pages.Empleado.AceptarServicios;

public partial class EmpleadoAceptarServicio : ContentPage
{
    private readonly ApiService _api;
    private readonly TokenService _tokenService;

    public EmpleadoAceptarServicio()
    {
        InitializeComponent();

        _tokenService = new TokenService();
        _api = new ApiService(_tokenService);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _tokenService.LoadTokensAsync();

        // ⚠️ Aquí NO hay endpoint real → usamos lista vacía o mock
        var servicios = new List<dynamic>(); // ← backend no implementado

        if (servicios == null || servicios.Count == 0)
        {
            await Shell.Current.GoToAsync("//empleadosinservicios");
            return;
        }

        ListaServicios.ItemsSource = servicios;
    }

    private async void OnAceptarClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is not null)
        {
            var idProp = btn.BindingContext.GetType().GetProperty("Id");
            if (idProp == null) return;

            var id = idProp.GetValue(btn.BindingContext)?.ToString();
            if (id == null) return;

            await Shell.Current.GoToAsync($"///empleadoconfirmarservicio?id={id}");
        }
    }
}
