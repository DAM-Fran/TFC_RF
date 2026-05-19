using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TFC_RelevosFamiliares.Models.Appointment;
using TFC_RelevosFamiliares.Models.Auth;
using TFC_RelevosFamiliares.Models.User;

public class ApiService
{
    private readonly HttpClient _client;
    private readonly TokenService _tokenService;

    public ApiService(TokenService tokenService)
    {
        _tokenService = tokenService;

        _client = new HttpClient
        {
            BaseAddress = new Uri("http://vms.iesluisvives.org:25015/")
        };
    }

    // ---------------------------
    // INTERNAL AUTH HEADER
    // ---------------------------
    private void SetAuthHeader(string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    // ---------------------------
    // REFRESH TOKEN
    // ---------------------------
    private async Task<bool> RefreshTokenAsync()
    {
        var refreshToken = _tokenService.GetRefreshToken();
        if (string.IsNullOrEmpty(refreshToken))
            return false;

        var body = new RefreshRequest { RefreshToken = refreshToken };

        var response = await _client.PostAsJsonAsync("auth/refresh", body);
        if (!response.IsSuccessStatusCode)
            return false;

        var data = await response.Content.ReadFromJsonAsync<RefreshResponse>();

        await _tokenService.SaveTokensAsync(
            data.AccessToken,
            data.RefreshToken,
            _tokenService.RememberMe
        );

        return true;
    }

    // ---------------------------
    // SEND WITH AUTO-REFRESH
    // ---------------------------
    private async Task<HttpResponseMessage> SendAuthorizedAsync(Func<Task<HttpResponseMessage>> action)
    {
        SetAuthHeader(_tokenService.GetAccessToken());

        var response = await action();

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            bool refreshed = await RefreshTokenAsync();
            if (!refreshed)
                return response;

            SetAuthHeader(_tokenService.GetAccessToken());
            response = await action();
        }

        return response;
    }

    // ---------------------------
    // LOGIN
    // ---------------------------
    public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
    {
        try
        {
            var body = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var response = await _client.PostAsJsonAsync("auth/login", body);

            var status = (int)response.StatusCode;
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"LOGIN STATUS: {status}");
            Console.WriteLine($"LOGIN BODY: {content}");

            if (!response.IsSuccessStatusCode)
                return false;

            var data = await response.Content.ReadFromJsonAsync<LoginResponse>();

            await _tokenService.SaveTokensAsync(
                data.AccessToken,
                data.RefreshToken,
                rememberMe
            );

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("LOGIN EXCEPTION: " + ex.Message);
            return false;
        }
    }


    // ---------------------------
    // REGISTER
    // ---------------------------
    public async Task<bool> RegisterAsync(string email, string password)
    {
        try
        {
            var body = new RegisterRequest
            {
                Email = email,
                Password = password
            };

            var response = await _client.PostAsJsonAsync("auth/register", body);

            string raw = await response.Content.ReadAsStringAsync();
            Console.WriteLine("REGISTER STATUS: " + (int)response.StatusCode);
            Console.WriteLine("REGISTER RAW RESPONSE: " + raw);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("REGISTER ERROR RAW: " + raw);
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine("REGISTER EXCEPTION: " + ex.Message);
            return false;
        }
    }



    // ---------------------------
    // GET ME
    // ---------------------------
    public async Task<User> GetMeAsync()
    {
        var response = await SendAuthorizedAsync(() => _client.GetAsync("auth/me"));
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<User>();
    }

    // ---------------------------
    // UPDATE ME
    // ---------------------------
    public async Task<bool> UpdateMeAsync(UserInfoUpdate update)
    {
        var body = new
        {
            active = true,
            userInfo = new
            {
                name = update.Name,
                phone = update.Phone,
                description = "",
                profilepictureuri = "",
                location = new
                {
                    longitude = 0.0,
                    latitude = 0.0
                }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Patch, "users/me")
        {
            Content = JsonContent.Create(body)
        };

        var response = await SendAuthorizedAsync(() => _client.SendAsync(request));
        return response.IsSuccessStatusCode;
    }



    // ---------------------------
    // GET MY APPOINTMENTS
    // ---------------------------
    public async Task<List<Appointment>> GetMyAppointmentsAsync()
    {
        var response = await SendAuthorizedAsync(() => _client.GetAsync("appointments/MyAppointments"));
        if (!response.IsSuccessStatusCode)
            return new List<Appointment>();

        return await response.Content.ReadFromJsonAsync<List<Appointment>>();
    }

    // ---------------------------
    // CREATE APPOINTMENT
    // ---------------------------
    public async Task<bool> CreateAppointmentAsync(AppointmentCreateRequest req)
    {
        var response = await SendAuthorizedAsync(() =>
            _client.PostAsJsonAsync("appointments", req)
        );

        return response.IsSuccessStatusCode;
    }

    // ---------------------------
    // DELETE APPOINTMENT
    // ---------------------------
    public async Task<bool> DeleteAppointmentAsync(string id)
    {
        var response = await SendAuthorizedAsync(() =>
            _client.DeleteAsync($"appointments/{id}")
        );

        return response.IsSuccessStatusCode;
    }

    // ---------------------------
    // LOGOUT
    // ---------------------------
    public async Task LogoutAsync()
    {
        await _tokenService.ClearAsync();
    }
}
