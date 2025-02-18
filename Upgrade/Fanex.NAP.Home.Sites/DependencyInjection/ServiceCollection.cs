namespace Fanex.NAP.Home.Sites.DependencyInjection;

using Fanex.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Fanex.NAP.Home.Sites.Services;


public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IPHelper, IPHelperImpl>();
        services.AddTransient<IDynamicRepository, DynamicRepository>();
        services.AddSingleton<ConfigurationService, ConfigurationServiceImp>();        
        services.AddTransient<IUserSitesService, UserSitesService>();
    }

    public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = configuration.GetSection("AuthSettings:LoginPath").Value;
                options.LogoutPath = configuration.GetSection("AuthSettings:LogoutPath").Value;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:CookieTimeOut").Value));
                options.Cookie = new CookieBuilder
                {
                    Name = "Nap-Home.Cookie",
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };

                options.Events.OnSigningOut = context =>
                {
                    context.Response.Redirect(options.LoginPath);
                    return Task.CompletedTask;
                };
            });

        services.AddSession(options =>
        {
            options.Cookie.IsEssential = true;
            options.IdleTimeout = TimeSpan.FromMinutes(int.Parse(configuration.GetSection("AuthSettings:SessionTimeOut").Value));
            options.Cookie.Name = "Nap-Home.Session";
        });
    }

    public static void ProcessWebOptimizer(this IServiceCollection services)
    {
        services.AddWebOptimizer(pipeline =>
        {
            pipeline.AddCssBundle("~/style/common.css",
                "/Content/Bootstrap/CSS/bootstrap.css",
                "/Content/Bootstrap/CSS/bootstrap-theme.css",
                "/Content/MessageBox/msgbox-0.0.1.css",
                "/Content/Theme/style.css");

            pipeline.AddJavaScriptBundle("~/script/common.js",
                 "/Scripts/jQuery/jquery-1.8.2.min.js",
                "/Scripts/jQuery/jquery.blockUI.2.66.min.js",
                "/Scripts/jQuery/jquery.nicescroll.min.js",
                "/Scripts/Bootstrap/bootstrap.js",
                "/Scripts/Bootstrap/bootstrap-tooltip-extension.js",
                "/Scripts/Modernizr/modernizr.30715.min.js",
                "/Scripts/MessageBox/msgbox-0.0.1.js",
                "/Scripts/Others/namespace.js",
                "/Scripts/Resources/viewLang.js",
                "/Scripts/common.js");

            pipeline.AddJavaScriptBundle("/nap-auth/login.js",
                "/nap-auth/jquery-1.8.0.min.js",
                "/nap-auth/cryptojs.js");
        });
    }
}
