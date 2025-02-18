namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;
using System;
using Fanex.NAP.Home.Core.Helpers;
using Fanex.NAP.Home.Sites.Models.UserSites;

[Serializable]
public class FunctionItem
{
    public FunctionItem(MenuInfo menu)
    {
        if (menu != null && menu.MenuItemId.HasValue)
        {
            FunctionId = menu.MenuItemId.Value;
            FunctionName = menu.MenuItemName;
            FunctionLink = StringExtensions.TrimOrEmpty(menu.MenuLink);
        }
    }

    public FunctionItem(FunctionItem item)
    {
        if (item != null)
        {
            FunctionId = item.FunctionId;
            FunctionName = item.FunctionName;
            FunctionLink = item.FunctionLink;
        }
    }

    public int FunctionId { get; set; }

    public string FunctionName { get; set; }

    public string FunctionLink { get; set; }

    public int DisplayOrder { get; set; }
}
