// --------------------------------------------------------------------------
// <copyright file="UserInfoController.cs">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.4.2">Add to NAP.Home project.</change>
// </history>

namespace Fanex.NAP.Home.User.Controllers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Home.Core.Helpers;
    using NAP.Core.Configuration;
    using ViewModels;

    /// <summary>
    /// User controller.
    /// </summary>
    public class UserInfoController : BaseController
    {
        private const string FormTokenKey = "_SecureChangePasswordFormToken";
        private readonly ISecurityService _securityService;
        private readonly IUserService _userService;
        private readonly HashAlgorithm _hashAlgorithm;

        public UserInfoController()
            : this(null, null)
        {
        }

        public UserInfoController(IUserService userService, ISecurityService securityService)
        {
            _userService = userService ?? new UserService();
            _securityService = securityService ?? new SecurityService();
            _hashAlgorithm = new SHA256Managed();
        }

        public string FormToken
        {
            get
            {
                if (Session[FormTokenKey] == null)
                {
                    return string.Empty;
                }

                return Session[FormTokenKey].ToString();
            }

            set
            {
                Session[FormTokenKey] = value;
            }
        }

        #region Change Password

        /// <summary>
        /// Redirect to changing password form.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var viewModel = new ChangePasswordViewModel { Token = GenerateToken() };

            return View(viewModel);
        }

        /// <summary>
        /// Change password action.
        /// </summary>
        /// <param name="criteria">Criteria using to update password.</param>
        /// <param name="token">Changing password token.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            // The password values are encoded at client side by calling CryptoJS.CfsEnCode.
            // The result of this action is the encrypted strings which totally break the password valiadtion rules.
            // They also cannot be decoded to the original ones since CryptoJS.CfsEnCode is one-way encryption algorithm.
            // Because of those reasons, so we will validate the form at client side only and not check the model state here.
            ////if (ModelState.IsValid)

            ModelState.Clear();

            if (CheckValidToken(viewModel.Token))
            {
                var user = _userService.Get(CurrentSession.UserId);
                string hashPassword = GetHash(user.Password + viewModel.Token);

                if (hashPassword == viewModel.OldUserPwd)
                {
                    ViewBag.Success = _securityService.UpdatePassword(NapSettings.Settings.SiteId, user.Id, viewModel.UserPwd);
                }
                else
                {
                    ModelState.AddModelError("OldUserPwd", ViewResources.Wrong_password);
                }
            }

            viewModel.Token = GenerateToken();

            return View(viewModel);
        }

        #endregion Change Password

        #region Helpers

        /// <summary>
        /// Generates the token for login form.
        /// </summary>
        private string GenerateToken()
        {
            string token = Guid.NewGuid().ToString("N");

            FormToken = token;
            AddTokenToCookie(token);

            return token;
        }

        /// <summary>
        /// Adds the login token to cookie.
        /// </summary>
        /// <param name="token">The changing password token.</param>
        private void AddTokenToCookie(string token)
        {
            var responseCookie = new HttpCookie(FormTokenKey)
            {
                HttpOnly = true,
                Value = token
            };

            // If we are using SSL, the cookie should be set to secure to
            // prevent it from being sent over HTTP connections
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }

            // Add the cookie to the response
            Response.Cookies.Set(responseCookie);
        }

        /// <summary>
        /// Checks if the token is valid or not.
        /// </summary>
        /// <param name="token">The changing password token.</param>
        /// <returns>Boolean value indicates if the login token is valid or not.</returns>
        private bool CheckValidToken(string token)
        {
            return !string.IsNullOrWhiteSpace(token)
                && FormToken == token
                && Request.Cookies[FormTokenKey] != null
                && token == Request.Cookies[FormTokenKey].Value;
        }

        /// <summary>
        /// Gets the hash value of input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Hash value of input.</returns>
        private string GetHash(string input)
        {
            var encrypted = string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var hashData = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                encrypted = BitConverter.ToString(hashData).Replace("-", string.Empty).ToLowerInvariant();
            }

            return encrypted;
        }

        #endregion Helpers
    }
}