using Core.Plugin.Interface;
using CorePlugin.Plugin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CorePlugin.Plugin;

public class Plugin : ICorePlugin
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<PlannerService>();
        builder.Services.AddTransient<WebuntisService>();
        builder.Services.AddControllers();
    }

    public void Configure(WebApplication app) => app.MapControllers();
}
