// --------------------------------------------------------------------------
// <copyright file="ForwardResult.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.8.8">Create</change>
// </history>

namespace Fanex.NAP.Home.Sites.ViewModels.UserSites
{
    using Fanex.NAP.Framework.Command;

    /// <summary>
    /// Object indicates that user can access a specific function or not and contains a valid SSO link.
    /// </summary>
    public class ForwardResult
    {
        public ForwardResult()
        {
        }

        public ForwardResult(ICommandResult result)
        {
            if (result != null)
            {
                this.ErrorCode = result.Code;
                this.ErrorMessage = result.Message;
            }
        }

        public string Link { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}