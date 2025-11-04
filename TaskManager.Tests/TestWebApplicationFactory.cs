using System.Linq;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Data;
using TaskManager.Models;
using Testcontainers.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManager.Tests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithName($"postgres-test-{Guid.NewGuid()}")
            .WithDatabase("testdb")
            .WithUsername("testuser")
            .WithPassword("testpass")
            .WithPortBinding(0, true)
            .Build();

        async System.Threading.Tasks.Task IAsyncLifetime.InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        async System.Threading.Tasks.Task IAsyncLifetime.DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }

        public override async ValueTask DisposeAsync()
        {
            if (_dbContainer != null)
            {
                await _dbContainer.DisposeAsync();
            }
            await base.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    var port = _dbContainer.GetMappedPublicPort(5432);
                    var connectionString = $"Host=localhost;Port={port};Database=testdb;Username=testuser;Password=testpass";
                    options.UseNpgsql(connectionString);
                });

                services.Configure<JwtSettings>(options =>
                {
                    options.Key = "Supersecretsuperlongtestkeythatisquitelongatleastthirtytwocharacters";
                    options.Issuer = "test-issuer";
                    options.Audience = "test-audience";
                    options.ExpiryInMinutes = 60;
                });

                services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var jwtSettings = services.BuildServiceProvider().GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtSettings>>();
                    options.TokenValidationParameters.ValidIssuer = jwtSettings.Value.Issuer;
                    options.TokenValidationParameters.ValidAudience = jwtSettings.Value.Audience;
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Value.Key!));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreatedAsync().GetAwaiter().GetResult();
            });
        }
    }
}
