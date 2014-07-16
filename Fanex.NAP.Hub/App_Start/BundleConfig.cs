// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.16">Created</change>
// </history>

namespace Fanex.NAP.Hub
{
    using System.Web.Optimization;

    /// <summary>
    /// Bundle Configuration.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundle collections.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bundles JavaScript and LESS files for Login page.
            bundles.AddAndInclude(
                "~/styles/login",
                "~/Areas/UserLogin/themes/login.less");

            bundles.Add(new ScriptBundle("~/scripts/login").Include(
                "~/Areas/UserLogin/scripts/lib/crypto/CryptoJS.js",
                "~/Areas/UserLogin/scripts/Login.js"));

            // Bundles JavaScript and LESS files for Site page.
            bundles.AddAndInclude(
               "~/styles/site",
               "~/Areas/UserLogin/themes/select-site.less",
               "~/Areas/UserLogin/scripts/lib/msgbox/msgbox-0.0.1.css");

            bundles.Add(new ScriptBundle("~/scripts/site").Include(
                "~/Areas/UserLogin/scripts/lib/jquery-1.8.0.min.js",
                "~/Areas/UserLogin/scripts/lib/msgbox/msgbox-0.0.1.js"));
        }
    }
}