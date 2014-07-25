// --------------------------------------------------------------------------
// <copyright file="UserInfoController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Domain;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Framework.Command;
    using Fanex.NAP.Home.Core.Helpers;

    /// <summary>
    /// User controller.
    /// </summary>
    public class UserInfoController : BaseController
    {
        private const string FormTokenKey = "_SecureChangePasswordFormToken";
        private readonly IUserService _userService;
        private HashAlgorithm _hashAlgorithm;

        public UserInfoController()
            : this(null)
        {
        }

        public UserInfoController(IUserService userService)
        {
            _userService = userService ?? new UserService { ServiceContext = new ServiceContext() };
            _hashAlgorithm = new SHA256Managed();

            ViewBag.HasSearchBar = false;
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
            GenerateToken();

            return View(new UpdatePasswordCustCriteria { Username = CurrentSession.Username });
        }

        /// <summary>
        /// Change password action.
        /// </summary>
        /// <param name="criteria">Criteria using to update password.</param>
        /// <param name="token">Changing password token.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ChangePassword(UpdatePasswordCustCriteria criteria, string token)
        {
            // The password values are encoded at client side by calling CryptoJS.CfsEnCode.
            // The result of this action is the encrypted strings which totally break the password valiadtion rules.
            // They also cannot be decoded to the original ones since CryptoJS.CfsEnCode is one-way encryption algorithm.
            // Because of those reasons, so we will validate the form at client side only and not check the model state here.
            ////if (ModelState.IsValid)

            ModelState.Clear();

            if (CheckValidToken(token))
            {
                IUser user = _userService.GetById(CurrentSession.UserId);
                string hashPassword = GetHash(user.Password + token);

                if (hashPassword == criteria.OldUserPwd)
                {
                    criteria.CustId = user.Id;

                    ICommandResult result = this._userService.InsertOrUpdate(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }
                }
                else
                {
                    ModelState.AddModelError("OldUserPwd", ViewResources.Wrong_password);
                }
            }

            GenerateToken();

            return View(criteria);
        }

        #endregion Change Password

        #region Profile

        /// <summary>
        /// Redirect to profile page.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult UserProfile()
        {
            var criteria = new UpdateCustCriteria();
            var user = this._userService.GetById(CurrentSession.UserId);

            criteria.CustId = user.Id;
            criteria.Username = user.Username;
            criteria.FirstName = user.FirstName;
            criteria.LastName = user.LastName;
            criteria.Email = user.Email;

            return View(criteria);
        }

        /// <summary>
        /// Update user profile.
        /// </summary>
        /// <param name="criteria">Criteria using to update user profile.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UserProfile(UpdateCustCriteria criteria)
        {
            if (ModelState.IsValid)
            {
                criteria.CustId = CurrentSession.UserId;
                criteria.Username = CurrentSession.Username;
                criteria.FirstName = criteria.FirstName ?? string.Empty;
                criteria.LastName = criteria.LastName ?? string.Empty;
                criteria.Email = criteria.Email ?? string.Empty;

                ICommandResult result = this._userService.InsertOrUpdate(criteria);

                if (result != null && result.IsSuccessful)
                {
                    ViewBag.Success = true;
                }
            }

            return View(criteria);
        }

        #endregion Profile

        #region Helpers

        /// <summary>
        /// Generates the token for login form.
        /// </summary>
        private void GenerateToken()
        {
            string token = Guid.NewGuid().ToString("N");

            FormToken = token;
            ViewBag.ChangePasswordToken = token;
            AddTokenToCookie(token);
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

                encrypted = BitConverter.ToString(hashData).Replace("-", string.Empty).ToLower();
            }

            return encrypted;
        }

        /// <summary>
        /// Clears the token.
        /// </summary>
        private void ClearToken()
        {
            Session.Remove(FormTokenKey);

            if (Request.Cookies[FormTokenKey] != null)
            {
                HttpCookie cookie = new HttpCookie(FormTokenKey);

                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
        }

        #endregion Helpers
    }
}