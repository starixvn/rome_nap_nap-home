using Fanex.Data;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.NAP.Client.Attributes;
using Microsoft.AspNetCore.Mvc;
using Nap.Home.Sites;
using Nap.Home.Sites.App_Start;
using Nap.Home.Sites.DependencyInjection;
using Nap.Home.Sites.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ONEbook.UM.AspNetCore.Filters;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.RegisterAuthentication(configuration);
        //services.RegisterGlobalServices();
        OnebookUMInjection.RegisterServices(services, configuration);        
        services.RegisterMediAtR();
        //services.ProcessWebOptimizer();

        // Add services to the container.
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new ServiceFilterAttribute(typeof(CheckUMFilterAttribute)));
            options.Filters.Add(new SessionAuthorizeAttribute());
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        services.AddHttpContextAccessor();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddFanexData(configuration);
        services.AddNapClient(configuration);
        services.AddFanexLogSentry(configuration);
       
        var app = builder.Build();        
        
        app.UseDeveloperExceptionPage();
        //app.UseWebOptimizer();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseSession();
        app.UseRouting();
       
        app.UseAuthentication();
        app.UseAuthorization();

        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.ConfigureExceptionHandler();

        app.Run();
    }
}