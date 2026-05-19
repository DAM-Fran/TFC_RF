namespace TFC_RelevosFamiliares.Pages
{
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Esperar 3 segundos y luego navegar a Login
            await Task.Delay(3000);

            string token = await SecureStorage.GetAsync("accessToken");
            string remember = await SecureStorage.GetAsync("remember");

            if (!string.IsNullOrEmpty(token) && remember == "1")
            {
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await Shell.Current.GoToAsync("///login");
            }

            //await Shell.Current.GoToAsync("///login");
        }
    }
}
