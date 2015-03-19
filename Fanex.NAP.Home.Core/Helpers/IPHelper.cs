// --------------------------------------------------------------------------
// <copyright file="IPHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.NAP.Home.Core.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    /// <summary>
    /// IP Helper.
    /// </summary>
    public static class IPHelper
    {
        public static bool IsWebVpnAccess()
        {
            return IsLocalOrVpnRequest(HttpContext.Current.Request.Headers["Host"]);
        }
        
        private static bool IsLocalOrVpnRequest(string ip)
        {
            var ipReturn = string.IsNullOrWhiteSpace(ip) ? string.Empty : ip;
            return ipReturn.Split('.').Length == 4 &&
                (ipReturn.StartsWith("10.", StringComparison.OrdinalIgnoreCase) ||
                ipReturn.StartsWith("172.16.", StringComparison.OrdinalIgnoreCase) ||
                ipReturn.StartsWith("192.168.", StringComparison.OrdinalIgnoreCase));
        }
    }
}
