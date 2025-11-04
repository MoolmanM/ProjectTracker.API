using System.Net;
using System.Net.Http.Json;
using TaskManager.Dtos.Auth;
using Xunit.Abstractions;

namespace TaskManager.Tests.Controllers
{
    public class AuthControllerTests(TestWebApplicationFactory factory, ITestOutputHelper output)
        : IntegrationTestBase(factory), IAsyncLifetime
    {
        private readonly ITestOutputHelper _output = output;

        public async Task InitializeAsync() => await Task.CompletedTask;
        public async Task DisposeAsync() => await Task.CompletedTask;

        private RegisterDto validRegisterDto = new()
        {
            Email = "test@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            FirstName = "First",
            LastName = "Last"
        };

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenValidCredential()
        {
            var response = await _client.PostAsJsonAsync("api/auth/register", validRegisterDto);

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
            Assert.NotEmpty(result.AccessToken);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidEmail()
        {
            var newDto = validRegisterDto with { Email = "testexample.com" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenEmptyEmail()
        {
            var newDto = validRegisterDto with { Email = "" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPasswordLength()
        {
            var newDto = validRegisterDto with { Password = "Pas123!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPassword_NoDigit()
        {
            var newDto = validRegisterDto with { Password = "Password!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPassword_NoLowercase()
        {
            var newDto = validRegisterDto with { Password = "PASSWORD123!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPassword_NoUppercase()
        {
            var newDto = validRegisterDto with { Password = "password123!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPassword_NoAlphanumeric()
        {
            var newDto = validRegisterDto with { Password = "Password123" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidConfirmPassword()
        {
            var newDto = validRegisterDto with { ConfirmPassword = "Different123!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess_WhenValidCredentials()
        {
            try
            {
                var registerDto = new RegisterDto
                {
                    Email = "test@example.com",
                    Password = "Password123!",
                    ConfirmPassword = "Password123!",
                    FirstName = "Test",
                    LastName = "User"
                };

                var regResponse = await _client.PostAsJsonAsync("api/auth/register", registerDto);

                if (regResponse.StatusCode != HttpStatusCode.OK)
                {
                    var regContent = await regResponse.Content.ReadAsStringAsync();
                    _output.WriteLine($"Registration Response: {regContent}");
                }

                var loginDto = new LoginDto
                {
                    Email = "test@example.com",
                    Password = "Password123!",
                };

                var response = await _client.PostAsJsonAsync("api/auth/login", loginDto);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                }

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            catch (Exception ex)
            {
                _output.WriteLine($"TEST EXCEPTION: {ex.Message}");
                _output.WriteLine($"STACK TRACE: {ex.StackTrace}");
                throw;
            }
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenInvalidCredentials()
        {
            var loginDto = new LoginDto { Email = "nonexistent@example.com", Password = "wrong" };
            var response = await _client.PostAsJsonAsync("api/auth/login", loginDto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
