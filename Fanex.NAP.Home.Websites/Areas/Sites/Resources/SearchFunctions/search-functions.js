/* File Created: July 28, 2014 */
/* global Namespace, common */
'use strict';

$(document).ready(function () {
    var searchFunctions = new Fanex.NAP.Home.Sites.SearchFunctions();

    searchFunctions.Initialize();
});

Namespace.Register("Fanex.NAP.Home.Sites.SearchFunctions");
Fanex.NAP.Home.Sites.SearchFunctions = function () {
    var $this = this;

    var domElements = {
        spanStarIcons: null
    };
    var urls = {
        MarkFavoriteFunction: common.ResolveRootUrl('Sites/SearchFunctions/MarkFavoriteFunction')
    };

    // Cache DOM elements.
    var cacheDOMElements = function () {
        domElements.spanStarIcons = $('span.icon-star');
    };

    // Handle events of elements.
    var handleEvents = function () {
        domElements.spanStarIcons.tooltip();
        domElements.spanStarIcons.on('click.nap-home', function () {
            var $icon = $(this);
            var siteId = $icon.attr('data-site-id');
            var functionId = $icon.attr('data-function-id');

            markUnmarkFavoriteFunction({
                SiteId: siteId,
                FunctionId: functionId,
                IsMarked: true
            }, $icon);
        });
    };

    // Get Top Favorite Functions.
    var markUnmarkFavoriteFunction = function (parameters, $icon) {
        common.DisableInput();
        $.ajax({
            type: 'POST',
            url: urls.MarkFavoriteFunction,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(parameters),
            success: function (result) {
                if (result.Code === 0) {
                    common.ShowInfoMessage(result.Message);
                    $icon.toggleClass('icon-favorite-medium').toggleClass('icon-noneFavorite-medium');
                } else {
                    common.ShowErrorMessage(result.Message);
                }
            },
            error: function (xhr) {
                common.HandleAjaxError(xhr);
            },
            complete: function () {
                common.EnableInput();
            }
        });
    };

    // Intialize page.
    $this.Initialize = function () {
        cacheDOMElements();
        handleEvents();
    };
};