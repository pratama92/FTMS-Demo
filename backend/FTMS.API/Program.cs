using FTMS.API.Extensions;
using FTMS.Application;
using FTMS.Application.Common.Settings;
using FTMS.Infrastructure;
using FTMS.Infrastructure.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCorsPolicy();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();


var app = builder.Build();

app.ApplyDatabaseMigrations();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsEnvironment("Development.docker") ||
    app.Environment.IsEnvironment("Development.local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AngularClient");

app.UseCustomMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/health-db", async (ApplicationDbContext db) =>
{
    return await db.Database.CanConnectAsync();
});



app.Run();