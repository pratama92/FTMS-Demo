using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.API.Extensions;

public static class DatabaseMigrationExtensions
{
    public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        try
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            db.Database.Migrate();

            app.Logger.LogInformation("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "Database migration failed.");

            throw;
        }

        return app;
    }
}