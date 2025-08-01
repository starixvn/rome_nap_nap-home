namespace Fanex.NAP.Home.Sites.Controllers;
using System.Linq;
using Fanex.NAP.Client;
using Fanex.NAP.Home.Sites.Services;
using Fanex.NAP.Home.Sites.ViewModels.UserSites;
using Microsoft.AspNetCore.Mvc;
using Fanex.NAP.Home.Sites.Models;
using Fanex.Common;
using Fanex.NAP.Client.Services;
using Fanex.NAP.Client.Configuration;
using Microsoft.AspNetCore.Http;

public class UserSitesController(
    IUserSitesService siteService,
    INapSession napSession,
    ConfigurationService configurationService,
    INapSettings napSettings,
    ISecurityService securityService) : Controller
{
    private const string ForwardLink = "{0}/userlogin/sso?userid={1}&onuserid={2}&time={3}&redirectUrl={4}";

    private readonly IUserSitesService _siteService = siteService;
    private readonly INapSession napSession = napSession;
    private readonly INapSettings napSettings = napSettings;
    private readonly ConfigurationService _configurationService = configurationService;
    private readonly ISecurityService _securityService = securityService;

    public ActionResult Index()
    => RedirectToAction("Sites", "UserSites");

    [HttpGet]
    public ActionResult Sites(string searchFunction)
    {
        var filter = new SearchFunctionsFilter
        {
            UserId = napSession.UserId,
            SearchedFunctionName = searchFunction ?? string.Empty,
            Environment = string.Empty
        };

        var sites = _siteService.LoadUserSitesAndFunctions(filter).ToList();

        ViewBag.SearchedFunctionName = searchFunction;
        ViewBag.IsHomePage = true;
        ViewBag.Environment = _configurationService.Environment;
        if (HttpContext.Request.Query.ContainsKey(_configurationService.UMToken))
        {
            HttpContext.Session.SetString(_configurationService.UMToken, HttpContext.Request.Query[_configurationService.UMToken]);
        }


        return View(new UserSiteViewModel(sites, _configurationService.NumberOfSitesPerRow));
    }

    [HttpPost]
    public JsonResult Forward([FromBody] ForwardFilter filter)
    {
        string time = _securityService.GetTime();
        CommandResult checkPermissionResult;

        filter.UserId = napSession.UserId;
        filter.Environment = napSettings.Environment;
        filter.SiteLink ??= string.Empty;
        filter.FunctionLink = string.IsNullOrEmpty(filter.FunctionLink) ? string.Empty : "~" + filter.FunctionLink;

        if (filter.FunctionId.HasValue && filter.FunctionId > 0)
        {
            checkPermissionResult = _siteService.CheckUserPermissionOnFunction(filter);
        }
        else
        {
            checkPermissionResult = _siteService.CheckUserPermissionOnSite(filter);
            filter.FunctionLink = string.Empty;
        }
        var token = HttpContext.Session.GetString(_configurationService.UMToken);
        var forwardResult = new ForwardResult(checkPermissionResult);

        if (checkPermissionResult.IsSuccessful)
        {
            forwardResult.Link = string.Format(
                                     ForwardLink,
                                     filter.SiteLink.Trim(),
                                     Cryptography.CfsEnCrypt(napSession.UserId.ToString()),
                                     Cryptography.CfsEnCrypt(napSession.OnUserId.ToString()),
                                     Cryptography.CfsEnCrypt(time),
                                     filter.FunctionLink);
            forwardResult.Link += $"&{_configurationService.UMToken}={token}";
        }

        return Json(forwardResult);
    }
}
