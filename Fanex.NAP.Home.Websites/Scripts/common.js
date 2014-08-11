/* File Created: July 25, 2014 */
/* global Namespace, appSettings, viewLang */
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
        divMsgBox: null
    };
    var cachedValues = {
        RootURL: '',
        SearchSitesUrl: 'Sites/SearchFunctions?FunctionName={0}&PageIndex={1}',
        ForwardUrl: 'Sites/SiteBase/Forward'
    };

    $this.HandleAjaxError = function (xhr) {
        var message = '';

        if (xhr.status !== 0) {
            if (xhr.status === 403) {
                message = messages.AccessDeniedError;
            } else if (xhr.status === 404) {
                message = messages.NotFoundError;
            } else {
                message = messages.InternalError;
            }

            $this.ShowErrorMessage(message);
        }
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

    $this.OpenNewTab = function (url) {
        window.open(url, '_blank');
    };

    // Check permission and forward to a specific link.
    $this.Forward = function (parameters) {
        $this.DisableInput();
        $.ajax({
            type: 'POST',
            async: false,
            url: $this.ResolveRootUrl(cachedValues.ForwardUrl),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(parameters),
            success: function (forwardResult) {
                if (forwardResult.ErrorCode === 0) {
                    $this.OpenNewTab(forwardResult.Link);
                } else {
                    $this.ShowErrorMessage(forwardResult.ErrorMessage);
                }
            },
            error: function (xhr) {
                $this.HandleAjaxError(xhr);
            },
            complete: function () {
                $this.EnableInput();
            }
        });
    };

    $this.Initialize = function () {
        domElements.txtSearchedFunctionName = $('#txtSearchedFunctionName');
        domElements.divMsgBox = $.msgbox({
            message: '',
            type: 'err',
            modal: false,
            position: ['top:80', 'center'],
            theme: '',
            zIndex: 9999,
            useAnimation: false
        });

        messages.AccessDeniedError = viewLang.AccessDeniedError;
        messages.InternalError = viewLang.InternalError;
        messages.NotFoundError = viewLang.NotFoundError;

        cachedValues.RootURL = appSettings.RootUrl;

        if (domElements.txtSearchedFunctionName.length > 0) {
            domElements.hdnCurrentPageIndex = $('#hdnCurrentPageIndex');
            domElements.btnSearch = $('#btnSearch');

            $(document).on('keydown.nap-home', function () {
                if (!domElements.txtSearchedFunctionName.is(':focus')) {
                    domElements.txtSearchedFunctionName.val('').focus();
                }
            });

            domElements.txtSearchedFunctionName.on('keyup.nap-home', function (event) {
                if (event.keyCode === 13) {
                    domElements.btnSearch.click();
                }

                if (domElements.txtSearchedFunctionName.val().length >= 2) {
                    if (!domElements.btnSearch.hasClass('button-green')) {
                        domElements.btnSearch.removeClass('button-disabled').addClass('button-green');
                    }
                } else {
                    domElements.btnSearch.removeClass('button-green').addClass('button-disabled');
                }
            });

            domElements.btnSearch.on('click.nap-home', function () {
                if (!domElements.btnSearch.hasClass('button-disabled')) {
                    var pageIndex = domElements.hdnCurrentPageIndex.val();
                    var functionName = domElements.txtSearchedFunctionName.val();

                    $this.NavigateToRoot(cachedValues.SearchSitesUrl.format($.trim(functionName), pageIndex));
                }
            });
        }

        if ($.fn.niceScroll) {
            $('html').niceScroll({
                autohidemode: false,
                cursorcolor: "#8C8C8C",
                cursorwidth: '10px',
                background: '#eee',
                cursorborderradius: '3px',
                zindex: 1031
            });
        }

        $('body').on('click.nap-home', '[data-strict-link]', function () {
            var $link = $(this);
            var parameters = null;

            try {
                parameters = $.parseJSON($link.attr('data-strict-link'));

                if (parameters) {
                    $this.Forward(parameters);
                }
            } catch (error) {
                $this.ShowErrorMessage(viewLang.InvalidLink);
            }
        });
    };
};

var common = new Fanex.NAP.Home.Common();

$(document).ready(function () {
    common.Initialize();
});