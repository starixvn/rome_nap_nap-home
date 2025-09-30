using Fanex.Data;
using Fanex.Logging.Sentry;
using Fanex.NAP.Client.Attributes;
using Microsoft.AspNetCore.Mvc;
using Fanex.NAP.Home.Sites.DependencyInjection;
using Fanex.NAP.Home.Sites.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ONEbook.UM.DependencyInjection;
using ONEbook.UM.Filters;
using Fanex.NAP.Client;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.RegisterAuthentication(configuration);
services.RegisterServices();
services.AddOneBookUmForWeb(configuration);
services.ProcessWebOptimizer();

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
services.AddFanexData(configuration);
services.AddNapClient(configuration);
services.AddFanexLogSentry(configuration);
services.AddHealthChecks();

var app = builder.Build();

// PRODUCTION READY: Debug features are properly conditional
// UseDeveloperExceptionPage() is only enabled in Development environment
// In Production, UseExceptionHandler("/Home/Error") is used instead
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseWebOptimizer();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserSites}/{action=Index}/{id?}");

app.MapHealthChecks("/health").AllowAnonymous();
app.ConfigureExceptionHandler();

app.Run();