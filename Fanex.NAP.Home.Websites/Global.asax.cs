// --------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    using Fanex.Data.Configuration;
    using Fanex.Module.Logs.Services;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Web.Auth;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// MVC application configuration.
    /// </summary>
    [System.Runtime.InteropServices.GuidAttribute("A695642F-3A07-4718-AAEC-AB470117E10B")]
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Sets all needed configuration when the application starts.
        /// </summary>
        protected void Application_Start()
        {
            DbSettingProviderManager.Current.Start();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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
                ex.Write();
            }
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity
                    && HttpContext.Current.User.Identity.IsAuthenticated)
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
        }

        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_End(object sender, EventArgs e)
        {
            var currentSession = new CurrentSession();
            var userService = new UserService();
            var logOutUserCriteria = new LogOutUserCriteria();

            logOutUserCriteria.OnUserId = currentSession.OnUserId;
            userService.UserLogOut(logOutUserCriteria);
            currentSession.ClearAll();
        }
    }
}