// --------------------------------------------------------------------------
// <copyright file="CommonController.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Created</change>
// </history>

using System.Web.Mvc;
using System.Web.Security;
using ONEbook.UM;
using ONEbook.UM.Services;

namespace Fanex.NAP.Home.User.Controllers
{
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
            _maintenanceService = maintenanceService ?? new MaintenanceService(
               UMClientManager.RestClient,
               UMClientManager.CacheService,
               UMClientManager.UMClientConfigs,
               UMClientManager.HttpContext);
        }

        /// <summary>
        /// Offline this instance.
        /// </summary>
        /// <returns>Return status of maintenance.</returns>
        public ActionResult Offline()
        {
            if (!_maintenanceService.IsUnderMaintenance())
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            var timeInfo = _maintenanceService.GetUnderMaintenanceTime();
            ViewBag.UmtFrom = timeInfo.From;
            ViewBag.UmtTo = timeInfo.To;

            return View();
        }
    }
}