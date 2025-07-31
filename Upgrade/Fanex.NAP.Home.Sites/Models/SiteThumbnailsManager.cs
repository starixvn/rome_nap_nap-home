namespace Fanex.NAP.Home.Sites.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public static class SiteThumbnailsManager
{
    public const string SiteThumbnailsFilePath = "Areas/Sites/Configurations/SiteThumbnails.xml";

    static SiteThumbnailsManager()
    {
        SiteThumbnailsConfigData = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}/{SiteThumbnailsFilePath}");
        LoadSiteThumbnails();
    }

    public static XDocument SiteThumbnailsConfigData { get; set; }

    public static List<SiteThumbnail> Thumbnails { get; set; }

    public static string GetImageName(int siteId)
    {
        var thumbnail = Thumbnails.Find(obj => obj.SiteId == siteId);
        thumbnail ??= Thumbnails.Find(obj => obj.SiteId == 0);
        return thumbnail?.ImageName;
    }

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
                        SiteId = int.Parse(thumbnail.Attribute("siteId").Value),
                        Name = thumbnail.Attribute("name").Value,
                        ImageName = thumbnail.Attribute("imageName").Value
                    };

                    Thumbnails.Add(siteThumbnail);
                }
            }
        }
    }
}
