namespace TFC_RelevosFamiliares.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string password, string nombre);
    Task LogoutAsync();
}
