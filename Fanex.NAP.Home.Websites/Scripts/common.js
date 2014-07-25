/* File Created: July 25, 2014 */
/* global Namespace, appSettings */
'use strict';

/* jshint freeze: false */
// String Format
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] !== 'undefined' ? args[number] : match;
    });
};
/* jshint freeze: true */

Namespace.Register("Fanex.NAP.Home.Common");
Fanex.NAP.Home.Common = function () {
    var $this = this;
    var messages = {
        NotFoundError: null,
        InternalError: null,
        AccessDeniedError: null
    };
    var domElements = {
        // For _SitesLayout.cshtml.
        txtSearchedFunctionName: null,
        hdnCurrentPageIndex: null,
        btnSearch: null,

        // For _Layout.cshtml.
        divMsgBox: null,
        divPopup: null
    };
    var cachedValues = {
        RootURL: '',
        SearchSitesUrl: 'Sites/SearchSites?functionName={0}&pageIndex={1}'
    };

    $this.HandleAjaxError = function (xhr) {
        var message = messages.InternalError;

        if (xhr.status === 403) {
            message = messages.AccessDeniedError;
        } else if (xhr.status === 404) {
            message = messages.NotFoundError;
        }

        $this.ShowErrorMessage(message);
    };

    $this.ShowErrorMessage = function (message) {
        domElements.divMsgBox.showErr(message, { sticky: true });
    };

    $this.ShowInfoMessage = function (message) {
        domElements.divMsgBox.showInfo(message, { duration: 3000 });
    };

    $this.CloseMsgBox = function () {
        domElements.divMsgBox.close();
    };

    $this.DisableInput = function () {
        try { $.blockUI({ message: '' }); } catch (e) { }
    };

    $this.EnableInput = function () {
        try { $.unblockUI(); } catch (e) { }
    };

    $this.ResolveRootUrl = function (relativeUrl) {
        return cachedValues.RootURL + relativeUrl;
    };

    $this.NavigateToRoot = function (url) {
        window.location.replace($this.ResolveRootUrl(url));
    };

    $this.Initialize = function () {
        domElements.txtSearchedFunctionName = $('#txtSearchedFunctionName');
        domElements.divPopup = $('#divPopup');
        domElements.divMsgBox = $.msgbox({
            message: '',
            type: 'err',
            modal: false,
            position: ['top:80', 'center'],
            theme: '',
            zIndex: 9999,
            useAnimation: false
        });

        messages.AccessDeniedError = $('#AccessDeniedError').val();
        messages.InternalError = $('#InternalError').val();
        messages.NotFoundError = $('#NotFoundError').val();

        cachedValues.RootURL = appSettings.RootUrl;

        if (domElements.txtSearchedFunctionName.length > 0) {
            domElements.hdnCurrentPageIndex = $('#hdnCurrentPageIndex');
            domElements.btnSearch = $('#btnSearch');

            $(document).keydown(function () {
                domElements.txtSearchedFunctionName.focus();
            });

            domElements.txtSearchedFunctionName.on('keyup', function (event) {
                if (event.keyCode === 13) {
                    domElements.btnSearch.click();
                }
            });

            domElements.btnSearch.on('click', function () {
                var pageIndex = domElements.hdnCurrentPageIndex.val();
                var functionName = domElements.txtSearchedFunctionName.val();

                $this.NavigateToRoot(cachedValues.SearchSitesUrl.format(functionName, pageIndex));
            });
        }
    };
};

var common = new Fanex.NAP.Home.Common();

$(document).ready(function () {
    common.Initialize();
});