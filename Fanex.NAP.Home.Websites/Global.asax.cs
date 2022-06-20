// --------------------------------------------------------------------------
// <copyright file="Global.asax.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.NAP.Home.Websites
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using Core.Web;
    using Fanex.Caching;
    using Fanex.Data.Configuration;
    using Logging.Extensions.Database;
    using Logging.Extensions.Web;
    using NAP.Core.Auth;
    using NAP.Core.Service.Impl;
    using NAP.Core.Web.Auth;
    using NAP.Core.Web.Session;
    using ONEbook.UM;
    using ONEbook.UM.AspNetMVC.Filters;
    using ONEbook.UM.Configurations;
    using ONEbook.UM.Helpers;
    using ONEbook.UM.NETFramework;
    using ONEbook.UM.Services;
    using ONEbook.UM.Transport;

    /// <summary>
    /// MVC application configuration.
    /// </summary>
    [System.Runtime.InteropServices.GuidAttribute("A695642F-3A07-4718-AAEC-AB470117E10B")]
    public class MvcApplication : System.Web.HttpApplication
    {
#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
        /// <summary>
        /// Sets all needed configuration when the application starts.
        /// </summary>
        protected void Application_Start()
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
        {
            LoggingConfig.Init(
               new[] { "password", "OldUserPwd", "UserPwd", "ConfirmUserPwd" },
               getDbExceptionInfo: DatabaseInfoGetters.GetDbExceptionInfo);

            DbSettingProviderManager.Current.Start();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var umConfig = new UMClientConfiguration().Initialize();
            UMClientManager
                .UseConfig(umConfig)
                .UseCacheService(new UMCacheService(new CacheService()))
                .UseRestClient(new UMRestClient(umConfig))
                .UseHttpContext(new HttpContextHelper());

            GlobalFilters.Filters.Add(new CheckUMFilterAttribute(
                new MaintenanceService(UMClientManager.RestClient, UMClientManager.CacheService, UMClientManager.UMClientConfigs, UMClientManager.HttpContext), null));
        }

        /// <summary>
        /// Handles the Error event of the Application.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            if (ex.GetType() != typeof(HttpException))
            {
                ExceptionHandler.Handle(ex);
            }
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var user = HttpContext.Current.User;

            if (user != null &&
                user.Identity is FormsIdentity &&
                user.Identity.IsAuthenticated)
            {
                HttpCookie httpCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (httpCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpCookie.Value);

                    if (ticket == null || ticket.Expired || string.IsNullOrEmpty(ticket.UserData))
                    {
                        return;
                    }

                    var identity = new FormsIdentity(ticket);
                    var udata = AuthInfo.CreateUserData(ticket.UserData);

                    Context.User = new NapPrincipal(identity, udata);
                }
            }
        }

#pragma warning disable S2325 // Methods and properties that don't access instance data should be static
        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_End(object sender, EventArgs e)
#pragma warning restore S2325 // Methods and properties that don't access instance data should be static
        {
            var currentSession = new CurrentSession();
            var securityService = new SecurityService();

            securityService.Logout(currentSession.OnUserId);
            currentSession.ClearAll();
        }
    }
}