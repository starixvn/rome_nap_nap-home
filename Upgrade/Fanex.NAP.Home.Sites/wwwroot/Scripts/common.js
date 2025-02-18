/* File Created: July 25, 2014 */
/* global Namespace, appSettings, viewLang */
// 'use strict';
/* jshint strict: false */

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
        txtSearchedFunctionName: null,
        ulMenus: null,
        spanSearch: null,
        divContent: null,
        divFeatureLists: null,

        // For _Layout.cshtml.
        divMsgBox: null
    };
    var cachedValues = {
        RootURL: '',
        SearchSitesUrl: 'UserSites/Sites?searchFunction={0}',
        ForwardUrl: 'UserSites/Forward'
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
        $this.CloseMsgBox();
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

    // Truncate sites' names and top favorites' names.
    $this.TruncateTileTexts = function () {
        $('[data-truncate-offset]').each(function () {
            var $link = $(this);
            var $parent = $link.closest('[data-truncate-parent]');
            var text = $link.attr('title');
            var offset = parseInt($link.attr('data-truncate-offset'));
            var width = $parent.width() - offset;
            var noOfChars = parseInt(width / 6);

            if (text.length > noOfChars) {
                text = text.substring(0, noOfChars - 3) + '...';
            }

            $link.text(text);
        });
    };

    // Reset thumbnails' heights when resizing the window.
    $this.RefreshThumbnailHeight = function () {
        var windowHeight = $(window).height();

        if (windowHeight <= 768) {
            domElements.divContent.addClass('max-height-768');
        } else {
            domElements.divContent.removeClass('max-height-768');
        }

        domElements.divFeatureLists.getNiceScroll().resize();
    };

    // Check and resize container's width for IE8.
    $this.RefreshContainerWidth = function () {
        var windowWidth = 0;

        if (!Modernizr.mq('only all')) {
            windowWidth = $(window).width();

            if (windowWidth < 1024) {
                domElements.divContent.addClass('container-fluid-max-width-1024');
            } else {
                domElements.divContent.removeClass('container-fluid-max-width-1024');
            }
        }
    };

    $this.Initialize = function () {
        domElements.txtSearchedFunctionName = $('#txtSearchedFunctionName');
        domElements.divContent = $('.content');
        domElements.ulMenus = $('ul.site-domain');
        domElements.divFeatureLists = $('div.feature-list');
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

        // Handle resizing event every 0.1s.
        $(window).on('resize', $this.Debounce(onResizeWindow, 100));
        onResizeWindow();

        if (domElements.txtSearchedFunctionName.length > 0) {
            domElements.spanSearch = $('#spanSearch');

            $(document).on('keydown.nap-home', function (event) {
                if (!domElements.txtSearchedFunctionName.is(':focus') && !$(event.target).is('input')) {
                    domElements.txtSearchedFunctionName.val('').focus();
                }
            });

            domElements.txtSearchedFunctionName.on('focusout.nap-home', function () {
                var errorMessage = domElements.txtSearchedFunctionName.attr('data-original-title');

                if (errorMessage && errorMessage.length > 0) {
                    domElements.txtSearchedFunctionName.attr('data-original-title', '');
                    domElements.txtSearchedFunctionName.tooltip('hide');

                    return false;
                }
            });

            domElements.txtSearchedFunctionName.on('keyup.nap-home', function (event) {
                if (event.keyCode === 13) {
                    domElements.spanSearch.click();

                    return false;
                }
            });

            domElements.spanSearch.on('click.nap-home', function () {
                setTimeout(function () {
                    var functionName = domElements.txtSearchedFunctionName.val();
                    var errorMessage = '';

                    if (functionName.length === 0) {
                        errorMessage = viewLang.RequiredFieldError;
                    } else if (functionName.length < 2) {
                        errorMessage = viewLang.MinLengthSearchFieldError;
                    } else {
                        $this.NavigateToRoot(cachedValues.SearchSitesUrl.format($.trim(functionName)));
                    }

                    if (errorMessage.length > 0) {
                        domElements.txtSearchedFunctionName.attr('data-original-title', errorMessage).tooltip({
                            placement: 'bottom',
                            trigger: 'manual'
                        }).focus().tooltip('show');
                    }
                }, 50);
            });
        }

        // Customize default browsers' scrollbars with NiceScroll.
        if ($.fn.niceScroll) {
            domElements.divFeatureLists.niceScroll({
                autohidemode: false,
                horizrailenabled: false,
                cursorcolor: "#ccc",
                cursorwidth: '8px',
                background: '#fff',
                cursorborderradius: '3px'
            });
            $('html').niceScroll({
                autohidemode: false,
                cursorcolor: "#8C8C8C",
                cursorwidth: '10px',
                background: '#eee',
                cursorborderradius: '3px',
                zindex: 1032
            });
        }

        // Handle domain menus behaviors.
        $('span.icon-down').on('click.nap-home', function () {
            var $icon = $(this);
            var $menu = $icon.next('ul.site-domain');

            if ($menu && !$icon.hasClass('disabled')) {
                if ($menu.is(':hidden')) {
                    domElements.ulMenus.hide();
                    $menu.show();
                } else {
                    $menu.hide();
                }

                return false;
            }
        });

        // Force all strict link must call Forward service.
        $('html').on('click.nap-home', '[data-strict-link]', function () {
            var $link = $(this);
            var parameters = null;
            var invalid = false;

            try {
                parameters = $.parseJSON($link.attr('data-strict-link'));

                for (var key in parameters) {
                    invalid = invalid || !parameters[key];
                }

                if (parameters && !invalid) {
                    $this.Forward(parameters);
                } else {
                    $this.ShowErrorMessage(viewLang.InvalidLink);
                }
            } catch (error) {
                $this.ShowErrorMessage(viewLang.InvalidLink);
            }
        }).on('click.nap-home', function () {
            domElements.ulMenus.hide();
        });

        // Hack for IE8 that does not set the exact position for thumbnails' scrollbar.
        setTimeout(function () {
            domElements.divFeatureLists.getNiceScroll().resize();
        }, 200);
    };

    // Execute function only once in a period of time.
    $this.Debounce = function (func, wait, immediate) {
        var timeout, args, context, timestamp, result;

        var later = function () {
            var last = now() - timestamp;

            if (last < wait && last >= 0) {
                timeout = setTimeout(later, wait - last);
            } else {
                timeout = null;

                if (!immediate) {
                    result = func.apply(context, args);

                    if (!timeout) {
                        context = args = null;
                    }
                }
            }
        };

        return function () {
            context = this;
            args = arguments;
            timestamp = now();

            var callNow = immediate && !timeout;

            if (!timeout) {
                timeout = setTimeout(later, wait);
            }

            if (callNow) {
                result = func.apply(context, args);
                context = args = null;
            }

            return result;
        };
    };

    // Call once the browser's window is resizing.
    var onResizeWindow = function () {
        $this.TruncateTileTexts();
        $this.RefreshThumbnailHeight();
        $this.RefreshContainerWidth();
        domElements.divFeatureLists.getNiceScroll().resize();
    };

    // A (possibly faster) way to get the current timestamp as an integer.
    var now = Date.now || function () {
        return new Date().getTime();
    };
};

$(document).ready(function () {
    var common = new Fanex.NAP.Home.Common();

    common.Initialize();
});