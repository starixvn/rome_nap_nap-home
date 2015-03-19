// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web.Context;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// Base controller.
    /// </summary>
    public class BaseController : Controller
    {
        private readonly string _environment = NapSettings.Settings.Environment;
        private readonly int _siteId = NapSettings.Settings.SiteId;
        private readonly ICurrentContext _currentContext = new CurrentContext();
        private readonly IServiceContext _serviceContext = new ServiceContext();
        private readonly ICurrentSession _currentSession = new CurrentSession();

        public BaseController()
        {
            ViewBag.IsReCheckin = NapSettings.Settings.EnableReCheckIn;
            this.ServiceContext.UserId = this.CurrentSession.UserId;
            this.ServiceContext.SiteId = this._siteId;
            this.ServiceContext.Ip = this.CurrentContext.IP;
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

        public IServiceContext ServiceContext
        {
            get
            {
                return this._serviceContext;
            }
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this.CurrentSession.UserId == 0 && !Request.Url.ToString().ToLower().Contains("user/login"))
            {
                Response.Redirect(FormsAuthentication.LoginUrl);
                Response.End();
            }
        }
    }
}