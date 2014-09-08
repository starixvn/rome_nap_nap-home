// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.22">Created</change>
// </history>

namespace Fanex.NAP.Home.Websites
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
            RegisterLoginBundles(bundles);
            RegisterCommonBundles(bundles);
            RegisterUserBundles(bundles);
            RegisterUserSitesBundles(bundles);
        }

        /// <summary>
        /// Registers the login bundles.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        public static void RegisterLoginBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/styles/login",
                "~/Areas/User/Resources/Login/login.less");

            bundles.Add(new ScriptBundle("~/scripts/login").Include(
                "~/Scripts/CryptoJs/CryptoJS.js",
                "~/Areas/User/Resources/Login/login.js"));
        }

        /// <summary>
        /// Registers the common bundles.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        public static void RegisterCommonBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/styles/common",
                "~/Content/Bootstrap/CSS/bootstrap.css",
                "~/Content/Bootstrap/CSS/bootstrap-theme.css",
                "~/Content/MessageBox/msgbox-0.0.1.css",
                "~/Content/Theme/style.less");

            bundles.Add(new ScriptBundle("~/scripts/html5hacks").Include(
                "~/Scripts/Modernizr/modernizr.30715.min.js",
                "~/Scripts/RespondJs/respond.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/common").Include(
                "~/Scripts/jQuery/jquery-1.11.1.min.js",
                "~/Scripts/jQuery/jquery.blockUI.2.66.min.js",
                "~/Scripts/jQuery/jquery.nicescroll.min.js",
                "~/Scripts/Bootstrap/bootstrap.js",
                "~/Scripts/Bootstrap/bootstrap-tooltip-extension.js",
                "~/Scripts/Underscore/underscore-min.js",
                "~/Scripts/MessageBox/msgbox-0.0.1.js",
                "~/Scripts/Others/namespace.js",
                "~/Scripts/Resources/viewLang.js",
                "~/Scripts/common.js",
                "~/Areas/User/Resources/Login/recheckIn.js"));
        }

        /// <summary>
        /// Bundles the JavaScript files for User area.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        public static void RegisterUserBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/scripts/profile").Include(
                "~/Scripts/jQuery/jquery.validate.min.js",
                "~/Scripts/jQuery/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/changePassword").Include(
                "~/Scripts/CryptoJs/CryptoJS.js",
                "~/Scripts/jQuery/jquery.validate.min.js",
                "~/Scripts/jQuery/jquery.validate.unobtrusive.min.js",
                "~/Areas/User/Resources/UserInfo/change-password.js"));
        }

        /// <summary>
        /// Bundles the JavaScript files for User Sites area.
        /// </summary>
        /// <param name="bundles">The bundle collection.</param>
        public static void RegisterUserSitesBundles(BundleCollection bundles)
        {
            bundles.AddAndInclude(
                "~/styles/userSites",
                "~/Areas/Sites/Resources/UserSites/user-sites.less");
        }
    }
}