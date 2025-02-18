namespace Fanex.NAP.Home.Sites.Middleware;

using Fanex.Logging;
using Fanex.Logging.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Fanex.NAP.Home.Sites.Models;
using System.Net;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var logMessage = new LogMessage(contextFeature.Error.Message)
                        .AddSection("Server Info", CommonInfoGetters.GetServerInfo())
                        .AddSection("Exception Info", CommonInfoGetters.GetExceptionInfo(contextFeature.Error.InnerException))
                        .ToString();
                    await LogService.ErrorAsync(logMessage);
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}
