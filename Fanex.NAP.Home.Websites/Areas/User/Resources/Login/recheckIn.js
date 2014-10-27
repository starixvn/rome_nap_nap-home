/* File Created: July 22, 2014 */
/* global Namespace, appSettings */
// 'use strict';
/* jshint strict: false */

$(document).ready(function () {
    var reCheckIn = new Fanex.NAP.Home.RecheckIn();

    reCheckIn.Initialize();
});

Namespace.Register("Fanex.NAP.Home.RecheckIn");
Fanex.NAP.Home.RecheckIn = function () {
    var $this = this;
    var cachedElements = {
        AllowReCheckIn: false,
        HeadlineMsgBox: null,
        RootURL: '',
        KickedOutMessage: 'You have logged in from another session or your login session has expired!',
        ReCheckInterval: 5000
    };

    var initializeData = function () {
        cachedElements.AllowReCheckIn = appSettings.AllowReCheckIn;
        cachedElements.RootURL = location.protocol + '//' + location.host + appSettings.RootUrl;
        cachedElements.HeadlineMsgBox = $.msgbox({
            message: '',
            type: 'err',
            modal: false,
            position: ['top:130', 'center'],
            theme: '',
            zIndex: 9999,
            duration: 2000,
            useAnimation: false,
            beforeClose: function () {
                window.location.href = cachedElements.RootURL + 'User/Login/LogOut';
            }
        });
    };

    var reCheckIn = function () {
        $.ajax({
            url: cachedElements.RootURL + 'User/Login/RecheckLogin',
            type: 'POST',
            dataType: 'json',
            success: function (result) {
                if (result === '1') {
                    cachedElements.HeadlineMsgBox.showInfo(cachedElements.KickedOutMessage, { closeBtn: false });
                }
                if (result === '-1') {
                    window.location.href = cachedElements.RootURL + 'User/Common/Offline';
                }
            }
        });
    };

    $this.Initialize = function () {
        initializeData();

        if (cachedElements.AllowReCheckIn) {
            setInterval(reCheckIn, cachedElements.ReCheckInterval);
        }
    };
};