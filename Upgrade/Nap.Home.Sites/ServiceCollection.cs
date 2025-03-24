using Microsoft.AspNetCore.Authentication.Cookies;
//using Nap.Home.Sites.DependencyInjection;

namespace Nap.Home.Sites
{
    public static class ServiceCollection
    {
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
            /*services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/style/fanex.result.css", 
                    "/Content/Fanex.Result/fanex.result.css", 
                    "/Content/Fanex.Result/fanex.result-customized.css");                
               
                pipeline.AddJavaScriptBundle("/scripts/nap-report-common.js",
                    "/scripts/_lib/jquery-1.8.2.js",
                    "/scripts/_lib/jquery-ui-1.9.1.js",
                    "/scripts/_lib/BlockUI/jquery.blockUI.1.33.js",
                    "/scripts/_lib/FormatLibs/NumberFormat/numeral.js",
                    "/scripts/_lib/Fanex.Grid/Fanex.Grid.js",
                    "/scripts/_lib/Fanex.Grid/Fanex.Pagination.js",
                    "/scripts/_lib/Tooltip/jquery.tipsy.js",
                    "/scripts/NapAccReports.js");
            });*/
        }
    }
}
