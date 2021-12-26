using EMS.Auth.Validators;
using EMS.Configurations;
using EMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EMS.Extensions;

public static class StartupExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var generalConfiguration = new GeneralConfiguration();
        configuration.Bind(nameof(GeneralConfiguration), generalConfiguration);
        services.AddOptions<GeneralConfiguration>().BindConfiguration(nameof(GeneralConfiguration));

        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(new JwtBearerValidator(generalConfiguration.JwtBearerConfiguration));
        });
        
        services.AddControllersWithViews();
        services.AddTransient<AuthService>();
        services.AddSingleton<EmployeeService>();
        services.AddSingleton<PositionService>();
    }

    public static void ConfigureApplication(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseStatusCodePages();
        
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
