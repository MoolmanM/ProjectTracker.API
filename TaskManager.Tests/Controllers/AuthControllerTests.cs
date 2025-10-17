using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskManager.Dtos.Auth;
using Xunit;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http.Connections;
using Xunit.Abstractions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TaskManager.Tests.Controllers
{
    public class AuthControllerTests(TestWebApplicationFactory factory, ITestOutputHelper output) : IntegrationTestBase(factory)
    {
        private readonly ITestOutputHelper _output = output;

        private RegisterDto validRegisterDto = new()
        {
            Email = "test@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            FirstName = "Test",
            LastName = "User"
        };

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenValidCredential()
        {
            var response = await _client.PostAsJsonAsync("api/auth/register", validRegisterDto);

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPasswordDigit()
        {
            var newDto = validRegisterDto with { Password = "Password!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPasswordLowercase()
        {
            var newDto = validRegisterDto with { Password = "PASSWORD123!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPasswordUppercase()
        {
            var newDto = validRegisterDto with { Password = "password123!" };
            var response = await _client.PostAsJsonAsync("api/auth/register", newDto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenInvalidPasswordAlphanumeric()
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
            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "Password123!",
            };

            var response = await _client.PostAsJsonAsync("api/auth/login", loginDto);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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