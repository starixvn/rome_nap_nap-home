// --------------------------------------------------------------------------
// <copyright file="CreateSiteItemCriteria.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.NAP.Home.Sites.Criteria.UserSites
{
    using System.Collections.Generic;
    using Fanex.NAP.Home.Sites.Models.UserSites;
    using Fanex.NAP.Home.Sites.ViewModels.UserSites;

    /// <summary>
    /// Create Site Item Criteria.
    /// </summary>
    public class CreateSiteItemCriteria
    {
        public SiteUrlInfo SiteInfo { get; set; }

        public List<SiteUrlInfo> SiteLinks { get; set; }

        public List<FunctionGroup> FunctionGroups { get; set; }

        public bool IsWebVpnAccess { get; set; }
    }
}
