namespace Fanex.NAP.Home.Sites.Services;
using System;

public interface IPHelper
{
    bool IsWebVpnAccess();

    bool IsLocalLink(string siteLink);
}

public class IPHelperImpl(IHttpContextAccessor httpContextAccessor) : IPHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public bool IsWebVpnAccess()
    => IsLocalOrVpnRequest(_httpContextAccessor.HttpContext.Request.Headers["Host"]);

    public bool IsLocalLink(string siteLink)
    {
        var uri = new Uri(siteLink);
        return IsLocalOrVpnRequest(uri.Host);
    }

    private static bool IsLocalOrVpnRequest(string ip)
    {
        const int noPartOfIP = 4;

        var ipReturn = string.IsNullOrWhiteSpace(ip) ? string.Empty : ip;
        return ipReturn.Split('.').Length == noPartOfIP &&
            (ipReturn.StartsWith("10.", StringComparison.OrdinalIgnoreCase) ||
            ipReturn.StartsWith("172.16.", StringComparison.OrdinalIgnoreCase) ||
            ipReturn.StartsWith("192.168.", StringComparison.OrdinalIgnoreCase));
    }
}
