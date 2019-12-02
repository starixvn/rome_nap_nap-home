// --------------------------------------------------------------------------
// <copyright file="AppSettings.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Created</change>
// </history>

namespace Fanex.NAP.Home.Core.Helpers
{
    using System.Configuration;
    using Fanex.Common;

    /// <summary>
    /// Application settings.
    /// </summary>
    public static class AppSettings
    {
        public static string LoggingDefaultCategory => GetValue("Fanex.Logging:DefaultCategory");

        public static string LogLibs => GetValue("LogLibs");

#pragma warning disable S100 // Methods and properties should be named in camel case
        public static string RabbitMQConnectionStrings => GetValue("Fanex.Logging:RabbitMQConnectionStrings");
#pragma warning restore S100 // Methods and properties should be named in camel case

        public static string SentryBackendUrl => GetValue("Fanex.Logging:SentryBackendUrl");

        public static string SentryUrl => GetValue("Fanex.Logging:SentryUrl");

        public static string RavenLibPath => GetValue("RavenLibPath");

        private static int? numberOfSitesPerRow;

        public static int NumberOfSitesPerRow
        {
            get
            {
                if (numberOfSitesPerRow == null)
                {
                    numberOfSitesPerRow = Converter.ToInt(GetValue("NumberOfSitesPerRow"));
                }

                return numberOfSitesPerRow.Value;
            }
        }

        public static string UMToken => GetValue("UMToken");

        /// <summary>
        /// Gets the value from web configuration.
        /// </summary>
        /// <param name="key">The key which is used to identify the value.</param>
        /// <returns>Associated value of passed key.</returns>
        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
