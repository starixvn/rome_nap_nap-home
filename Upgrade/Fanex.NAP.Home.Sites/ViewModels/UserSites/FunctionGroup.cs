namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;

using System;
using System.Collections.Generic;
using Fanex.NAP.Home.Sites.Models.UserSites;

[Serializable]
public class FunctionGroup : FunctionItem
{
    public FunctionGroup(MenuInfo menu)
        : base(menu)
    {
        FunctionList = [];
    }

    public FunctionGroup(FunctionItem item)
        : base(item)
    {
        FunctionList = new List<FunctionItem>();
    }

    public List<FunctionItem> FunctionList { get; set; }
}
