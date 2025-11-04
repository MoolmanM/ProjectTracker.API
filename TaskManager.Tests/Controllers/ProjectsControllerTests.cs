using System.Net;
using System.Net.Http.Json;
using TaskManager.Dtos.Projects;
using Xunit.Abstractions;

namespace TaskManager.Tests.Controllers
{
    public class ProjectsControllerTests(TestWebApplicationFactory factory, ITestOutputHelper output)
        : IntegrationTestBase(factory), IAsyncLifetime
    {
        private readonly ITestOutputHelper _output = output;

        public async Task InitializeAsync() => await Task.CompletedTask;
        public async Task DisposeAsync() => await Task.CompletedTask;

        [Fact]
        public async Task CreateProject_ShouldReturnSuccess_WhenValidRequest()
        {
            var token = await TestHelper.RegisterAndLoginUser(_client, $"{Guid.NewGuid()}@example.com", "Password123!", _output);
            SetBearerToken(token);
            _output.WriteLine(token);

            var createDto = new CreateProjectDto { Name = "Test Project", Description = "Test description" };
            var response = await _client.PostAsJsonAsync("api/projects", createDto);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetProject_ShouldReturnEmptyList_WhenNoProjects()
        {
            var token = await TestHelper.RegisterAndLoginUser(_client, $"{Guid.NewGuid()}@example.com", "Password123!", _output);
            SetBearerToken(token);

            _output.WriteLine($"Using token: {token}");

            var response = await _client.GetAsync("api/projects");
            _output.WriteLine($"Response Status: {response.StatusCode}");
            //response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _output.WriteLine($"Response content: {content}");

            if (response.IsSuccessStatusCode)
            {
                var projects = await response.Content.ReadFromJsonAsync<List<ProjectSummaryDto>>();
                Assert.NotNull(projects);
                Assert.Empty(projects);
            }
            else
                Assert.True(response.IsSuccessStatusCode, $"Expected 200 but got {response.StatusCode}: {content}");
        }

        [Fact]
        public async Task CreateProject_ShouldReturnUnauthorized_WhenNoToken()
        {
            var createDto = new CreateProjectDto { Name = "Test Project" };
            var response = await _client.PostAsJsonAsync("api/projects", createDto);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
