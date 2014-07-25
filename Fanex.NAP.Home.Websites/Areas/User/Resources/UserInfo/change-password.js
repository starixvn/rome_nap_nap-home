/* File Created: July 24, 2014 */
/* global Namespace, CryptoJS */
'use strict';

$(document).ready(function () {
    var changePassword = new Fanex.NAP.Home.ChangePassword();

    changePassword.Initialize();
});

Namespace.Register("Fanex.NAP.Home.ChangePassword");
Fanex.NAP.Home.ChangePassword = function () {
    var $this = this;
    var domElements = {
        frmMainForm: null,
        txtOldUserPwd: null,
        txtUserPwd: null,
        txtConfirmUserPwd: null,
        frmHiddenForm: null,
        hdnToken: null,
        hdnOldPassword: null,
        hdnNewPassword: null,
        hdnConfirmPassword: null
    };

    var cacheDOM = function () {
        domElements.frmMainForm = $('#mainForm');
        domElements.txtOldUserPwd = $('#OldUserPwd');
        domElements.txtUserPwd = $('#UserPwd');
        domElements.txtConfirmUserPwd = $('#ConfirmUserPwd');
        domElements.frmHiddenForm = $('#hiddenForm');
        domElements.hdnToken = $('#token');
        domElements.hdnOldPassword = $('#oldPassword');
        domElements.hdnNewPassword = $('#newPassword');
        domElements.hdnConfirmPassword = $('#confirmPassword');
    };

    var handleEvents = function () {
        domElements.frmMainForm.on('submit', function () {
            if ($(this).valid()) {
                var encryptPass = '';
                var token = domElements.hdnToken.val();

                encryptPass = CryptoJS.CfsEnCode(domElements.txtOldUserPwd.val());
                domElements.hdnOldPassword.val(CryptoJS.SHA256(encryptPass + token));
                encryptPass = CryptoJS.CfsEnCode(domElements.txtUserPwd.val());
                domElements.hdnNewPassword.val(encryptPass);
                encryptPass = CryptoJS.CfsEnCode(domElements.txtConfirmUserPwd.val());
                domElements.hdnConfirmPassword.val(encryptPass);

                domElements.frmHiddenForm.submit();

                return false;
            }
        });
    };

    $this.Initialize = function () {
        cacheDOM();
        handleEvents();
    };
};