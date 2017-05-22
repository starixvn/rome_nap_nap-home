// --------------------------------------------------------------------------
// <copyright file="FunctionGroup.cs">
// Copyright (c). All rights reserved.
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
    /// View model using to represent a group of functions.
    /// </summary>
    [Serializable]
    public class FunctionGroup : FunctionItem
    {
        public FunctionGroup()
        {
            this.FunctionList = new List<FunctionItem>();
        }

        public FunctionGroup(MenuInfo menu)
            : base(menu)
        {
            this.FunctionList = new List<FunctionItem>();
        }

        public FunctionGroup(FunctionItem item)
            : base(item)
        {
            this.FunctionList = new List<FunctionItem>();
        }

        public List<FunctionItem> FunctionList { get; set; }
    }
}