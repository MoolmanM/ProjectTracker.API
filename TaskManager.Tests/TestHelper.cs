using System.Net.Http.Json;
using TaskManager.Dtos.Auth;

namespace TaskManager.Tests;

public static class TestHelper
{
    public static async Task<string> RegisterAndLoginUser(HttpClient client, string email, string password)
    {
        var registerResponse = await client.PostAsJsonAsync("/api/auth/register", new RegisterDto
        {
            Email = email,
            Password = password,
            ConfirmPassword = password,
            FirstName = "Test",
            LastName = "User"
        });
        registerResponse.EnsureSuccessStatusCode();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new LoginDto
        {
            Email = email,
            Password = password
        });
        loginResponse.EnsureSuccessStatusCode();

        var authResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponseDto>();
        return authResult!.AccessToken;
    }
}