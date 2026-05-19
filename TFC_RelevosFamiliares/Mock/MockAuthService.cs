using TFC_RelevosFamiliares.Interfaces;

namespace TFC_RelevosFamiliares.Mock;

public class MockAuthService : IAuthService
{
    public Task<bool> LoginAsync(string email, string password)
    {
        // Aquí lógica mock (por ahora, algo simple)
        var ok = email == "test@demo.com" && password == "1234";
        return Task.FromResult(ok);
    }

    public Task<bool> RegisterAsync(string email, string password, string nombre)
    {
        // Simular registro correcto siempre, por ahora
        return Task.FromResult(true);
    }

    public Task LogoutAsync()
    {
        // Nada especial por ahora
        return Task.CompletedTask;
    }
}
