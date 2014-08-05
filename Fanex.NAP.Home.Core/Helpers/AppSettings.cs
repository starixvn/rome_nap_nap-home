// --------------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
        private static int? searchPageSize;
        private static int? topFavorites;
        private static int? favoritePageSize;
        private static int? favoriteLinesOfColumn;

        public static int SearchPageSize
        {
            get
            {
                if (searchPageSize == null)
                {
                    searchPageSize = Converter.ToInt(GetValue("SearchPageSize"));
                }

                return searchPageSize.Value;
            }
        }

        public static int TopFavorites
        {
            get
            {
                if (topFavorites == null)
                {
                    topFavorites = Converter.ToInt(GetValue("TopFavorites"));
                }

                return topFavorites.Value;
            }
        }

        public static int FavoritePageSize
        {
            get
            {
                if (favoritePageSize == null)
                {
                    favoritePageSize = Converter.ToInt(GetValue("FavoritePageSize"));
                }

                return favoritePageSize.Value;
            }
        }

        public static int FavoriteLinesOfColumn
        {
            get
            {
                if (favoriteLinesOfColumn == null)
                {
                    favoriteLinesOfColumn = Converter.ToInt(GetValue("FavoriteLinesOfColumn"));
                }

                return favoriteLinesOfColumn.Value;
            }
        }

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