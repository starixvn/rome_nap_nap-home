// --------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.7.29">Create</change>
// </history>

namespace Fanex.NAP.Home.Core.Helpers
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Base service.
    /// </summary>
    public class BaseService
    {
        public BaseService()
        {
            this.Repository = new DynamicRepository();
        }

        protected IDynamicRepository Repository { get; set; }
    }
}