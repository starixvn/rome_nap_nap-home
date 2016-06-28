// --------------------------------------------------------------------------
// <copyright file="SiteThumbnailsManager.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.Configurations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;
    using Fanex.Common;

    /// <summary>
    /// Getting the configurations of login themes.
    /// </summary>
    public static class SiteThumbnailsManager
    {
        public const string SiteThumbnailsFilePath = "~/Areas/Sites/Configurations/SiteThumbnails.xml";

        static SiteThumbnailsManager()
        {
            SiteThumbnailsConfigData = XDocument.Load(HttpContext.Current.Server.MapPath(SiteThumbnailsFilePath));
            LoadSiteThumbnails();
        }

        public static XDocument SiteThumbnailsConfigData { get; set; }

        public static List<SiteThumbnail> Thumbnails { get; set; }

        /// <summary>
        /// Gets the thumbnail of a specific site.
        /// </summary>
        /// <param name="siteId">Site identification.</param>
        /// <returns>Returns the thumbnail of site if it is configured, otherwise returns the default thumbnail.</returns>
        public static string GetImageName(int siteId)
        {
            SiteThumbnail thumbnail = Thumbnails.FirstOrDefault(obj => obj.SiteId == siteId);

            thumbnail = thumbnail ?? Thumbnails.FirstOrDefault(obj => obj.SiteId == 0);

            return thumbnail.ImageName;
        }

        /// <summary>
        /// Loads all thumbnails configuration.
        /// </summary>
        private static void LoadSiteThumbnails()
        {
            XElement root = SiteThumbnailsConfigData.Root;
            IEnumerable<XElement> thumbnails = root.Elements();

            Thumbnails = new List<SiteThumbnail>();

            if (thumbnails.Any())
            {
                foreach (XElement thumbnail in thumbnails)
                {
                    if (thumbnail.HasAttributes)
                    {
                        var siteThumbnail = new SiteThumbnail
                        {
                            SiteId = Converter.ToInt(thumbnail.Attribute("siteId").Value),
                            Name = thumbnail.Attribute("name").Value,
                            ImageName = thumbnail.Attribute("imageName").Value
                        };

                        Thumbnails.Add(siteThumbnail);
                    }
                }
            }
        }
    }
}