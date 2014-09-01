// --------------------------------------------------------------------------
// <copyright file="FunctionTreeNode.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.9.1">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.Models.UserSites;

    /// <summary>
    /// View model using to represent a node of function tree.
    /// </summary>
    [Serializable]
    public class FunctionTreeNode : FunctionItem
    {
        public FunctionTreeNode()
            : base()
        {
            this.ChildFunctionNodeList = new List<FunctionTreeNode>();
        }

        public FunctionTreeNode(MenuInfo menu)
            : base(menu)
        {
            this.ChildFunctionNodeList = new List<FunctionTreeNode>();
        }

        public FunctionTreeNode(FunctionItem item)
            : base(item)
        {
            this.ChildFunctionNodeList = new List<FunctionTreeNode>();
        }

        public List<FunctionTreeNode> ChildFunctionNodeList { get; set; }
    }
}