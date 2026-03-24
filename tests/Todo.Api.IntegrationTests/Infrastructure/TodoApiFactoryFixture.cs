using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure.Persistence;

namespace Todo.Api.IntegrationTests.Infrastructure;

public sealed class TodoApiFactoryFixture: WebApplicationFactory<Program>, IAsyncLifetime
{
    private string _databasePath = null!;

    public HttpClient Client { get; private set; } = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            _databasePath = Path.Combine(context.HostingEnvironment.ContentRootPath, "Database", "test.sqlite");

            configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                // The API reads DefaultConnection, so point it at the dedicated test database.
                ["ConnectionStrings:DefaultConnection"] = $"Data Source={_databasePath};Cache=Shared"
            });
        });
    }

    async Task IAsyncLifetime.InitializeAsync()
    {
        Client = CreateClient();

        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureDeletedAsync();
        await db.Database.MigrateAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        Client.Dispose();

        if (File.Exists(_databasePath))
        {
            File.Delete(_databasePath);
        }

        return Task.CompletedTask;
    }
}
