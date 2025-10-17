using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TaskManager.Tests
{
    public abstract class IntegrationTestBase(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
    {
        protected readonly HttpClient _client = factory.CreateClient();
        protected readonly TestWebApplicationFactory _factory = factory;

        protected void SetBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}