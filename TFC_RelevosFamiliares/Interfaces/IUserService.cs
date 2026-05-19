using TFC_RelevosFamiliares.Models.User;

namespace TFC_RelevosFamiliares.Interfaces;

public interface IUserService
{
    Task<User> GetCurrentUserAsync();
    Task SetCurrentUserAsync(User user);
    Task ClearCurrentUserAsync();
}
