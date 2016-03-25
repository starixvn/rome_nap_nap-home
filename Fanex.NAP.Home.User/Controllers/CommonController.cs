// --------------------------------------------------------------------------
// <copyright file="CommonController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Created</change>
// </history>

namespace Fanex.NAP.Home.User.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;

    /// <summary>
    /// Common controller.
    /// </summary>
    public class CommonController : Controller
    {
        private readonly IMaintenanceService _maintenanceService;

        public CommonController()
            : this(null)
        {
        }

        public CommonController(IMaintenanceService maintenanceService)
        {
            this._maintenanceService = maintenanceService ?? new MaintenanceService();
        }

        /// <summary>
        /// Offline this instance.
        /// </summary>
        /// <returns>Return status of maintenance.</returns>
        public ActionResult Offline()
        {
            var umservice = new ONEbook.UM.Services.Impl.MaintenanceService();

            if (!umservice.IsUnderMaintenance() && !umservice.IsPassUnderMaintenance())
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            var timeInfo = umservice.GetUnderMaintenanceTime();
            ViewBag.UmtFrom = timeInfo.From;
            ViewBag.UmtTo = timeInfo.To;

            return this.View();
        }
    }
}