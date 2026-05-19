using TFC_RelevosFamiliares.Interfaces;
using TFC_RelevosFamiliares.Models.User;

namespace TFC_RelevosFamiliares.Mock;

public class MockUserService : IUserService
{
    private User _currentUser;

    public Task<User> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }

    public Task SetCurrentUserAsync(User user)
    {
        _currentUser = user;
        return Task.CompletedTask;
    }

    public Task ClearCurrentUserAsync()
    {
        _currentUser = null;
        return Task.CompletedTask;
    }
}
