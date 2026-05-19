namespace TFC_RelevosFamiliares.Helpers;

public static class ServiceHelper
{
    public static T Get<T>() =>
        Current.GetService<T>();

    public static IServiceProvider Current =>
        IPlatformApplication.Current.Services;
}
