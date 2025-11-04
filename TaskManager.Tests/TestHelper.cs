using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskManager.Dtos.Auth;
using Xunit.Abstractions;

namespace TaskManager.Tests;

public static class TestHelper
{
    public static async Task<string> RegisterAndLoginUser(HttpClient client, string email, string password, ITestOutputHelper output)
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

        var checkLoginResponse = await client.PostAsJsonAsync("api/auth/login", new LoginDto
        {
            Email = email,
            Password = "wrong"
        });

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new LoginDto
        {
            Email = email,
            Password = password
        });

        var loginContent = await loginResponse.Content.ReadAsStringAsync();

        if (loginResponse.IsSuccessStatusCode)
        {
            var authResponse = await loginResponse.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (authResponse?.AccessToken != null)
            {
                output.WriteLine($"Login successful, token: {authResponse.AccessToken[..20]}");
                return authResponse.AccessToken;
            }
        }

        var errorContent = await loginResponse.Content.ReadAsStringAsync();

        throw new InvalidOperationException(
            $"Failed to login user {email}. Status: {loginResponse.StatusCode}. Responses: {errorContent}");
    }
}