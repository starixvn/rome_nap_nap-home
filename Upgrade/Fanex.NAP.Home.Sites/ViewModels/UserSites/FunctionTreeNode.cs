namespace Fanex.NAP.Home.Sites.ViewModels.UserSites;
using System;
using System.Collections.Generic;
using Fanex.NAP.Home.Sites.Models.UserSites;

[Serializable]
public class FunctionTreeNode : FunctionItem
{

    public FunctionTreeNode(MenuInfo menu)
        : base(menu)
    {
        ChildFunctionNodeList = new List<FunctionTreeNode>();
    }

    public FunctionTreeNode(FunctionItem item)
        : base(item)
    {
        ChildFunctionNodeList = new List<FunctionTreeNode>();
    }

    public List<FunctionTreeNode> ChildFunctionNodeList { get; set; }
}
