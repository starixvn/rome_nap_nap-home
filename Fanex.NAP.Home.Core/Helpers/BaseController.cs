// --------------------------------------------------------------------------
// <copyright file="BaseController.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.24">Created</change>
// </history>

namespace Fanex.NAP.Home.Core.Helpers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.NAP.Core.Configuration;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;
    using NAP.Core.Context;

    /// <summary>
    /// Base controller.
    /// </summary>
    public class BaseController : Controller
    {
        private readonly string _environment = NapSettings.Settings.Environment;
        private readonly int _siteId = NapSettings.Settings.SiteId;
        private readonly ICurrentContext _currentContext = new CurrentContext();
        private readonly ICurrentSession _currentSession = new CurrentSession();

        public BaseController()
        {
            ViewBag.IsReCheckin = NapSettings.Settings.EnableReCheckIn;
        }

        public string Environment
        {
            get
            {
                return this._environment;
            }
        }

        public int SiteId
        {
            get
            {
                return this._siteId;
            }
        }

        public ICurrentContext CurrentContext
        {
            get
            {
                return this._currentContext;
            }
        }

        public ICurrentSession CurrentSession
        {
            get
            {
                return this._currentSession;
            }
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this.CurrentSession.UserId == 0 &&
                !Request.Url.ToString().ToLowerInvariant().Contains("user/login") &&
                !Response.IsRequestBeingRedirected)
            {
                Response.Redirect(FormsAuthentication.LoginUrl);
                Response.End();
            }
        }
    }
}
