using System.Text.Json;
using Microsoft.Maui.Storage;

public class TokenService
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";
    private const string RememberMeKey = "remember_me";

    private string _accessToken;
    private string _refreshToken;
    private bool _rememberMe;

    public async Task SaveTokensAsync(string accessToken, string refreshToken, bool rememberMe)
    {
        _accessToken = accessToken;
        _refreshToken = refreshToken;
        _rememberMe = rememberMe;

        if (rememberMe)
        {
            await SecureStorage.SetAsync(AccessTokenKey, accessToken);
            await SecureStorage.SetAsync(RefreshTokenKey, refreshToken);
            await SecureStorage.SetAsync(RememberMeKey, "true");
        }
    }

    public async Task LoadTokensAsync()
    {
        if (await SecureStorage.GetAsync(RememberMeKey) == "true")
        {
            _accessToken = await SecureStorage.GetAsync(AccessTokenKey);
            _refreshToken = await SecureStorage.GetAsync(RefreshTokenKey);
            _rememberMe = true;
        }
    }

    public string GetAccessToken() => _accessToken;
    public string GetRefreshToken() => _refreshToken;
    public bool RememberMe => _rememberMe;

    public async Task ClearAsync()
    {
        _accessToken = null;
        _refreshToken = null;
        _rememberMe = false;

        SecureStorage.Remove(AccessTokenKey);
        SecureStorage.Remove(RefreshTokenKey);
        SecureStorage.Remove(RememberMeKey);
    }
}
