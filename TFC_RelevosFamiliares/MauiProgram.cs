using Microsoft.Extensions.Logging;
using TFC_RelevosFamiliares.Interfaces;
using TFC_RelevosFamiliares.Mock;

namespace TFC_RelevosFamiliares
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Páginas
            builder.Services.AddTransient<Pages.SplashScreen>();
            builder.Services.AddTransient<Pages.Login>();
            builder.Services.AddTransient<Pages.Register>();
            builder.Services.AddTransient<Pages.Home>();
            builder.Services.AddTransient<Pages.Calendario>();
            builder.Services.AddTransient<Pages.MisReservas>();
            builder.Services.AddTransient<Pages.Contacto>();

            // Servicios (mock)
            builder.Services.AddSingleton<IAuthService, MockAuthService>();
            builder.Services.AddSingleton<IReservationService, MockReservationService>();
            builder.Services.AddSingleton<IUserService, MockUserService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
