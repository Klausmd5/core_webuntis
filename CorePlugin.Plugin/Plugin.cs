using Core.Plugin.Interface;
using CorePlugin.DbLib;
using CorePlugin.Plugin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CorePlugin.Plugin;

public class Plugin : ICorePlugin
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PlannerContext>(x =>
        {
            var connectionString = builder.Configuration.GetConnectionString("PlannerDb");
            x.UseSqlite(connectionString);
        });
        builder.Services.AddTransient<PlannerService>();
        builder.Services.AddTransient<WebuntisService>();
        builder.Services.AddControllers();
    }

    public void Configure(WebApplication app)
    {
        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PlannerContext>();
        db.Database.EnsureCreated();
    }
}
