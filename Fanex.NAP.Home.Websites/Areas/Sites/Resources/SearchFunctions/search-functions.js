/* File Created: July 28, 2014 */
/* global Namespace, common, instructions */
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
        UpdatingFavoriteFunction: common.ResolveRootUrl('Sites/SearchFunctions/UpdatingFavoriteFunction')
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
            var isMarked = $icon.hasClass('icon-noneFavorite-medium');

            markUnmarkFavoriteFunction({
                SiteId: siteId,
                FunctionId: functionId,
                IsMarked: isMarked
            }, $icon);
        });
    };

    // Get Top Favorite Functions.
    var markUnmarkFavoriteFunction = function (parameters, $icon) {
        common.DisableInput();
        $.ajax({
            type: 'POST',
            url: urls.UpdatingFavoriteFunction,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(parameters),
            success: function (result) {
                var instructionMessage = '';

                if (result.Code === 0) {
                    $icon.toggleClass('icon-favorite-medium').toggleClass('icon-noneFavorite-medium');
                    instructionMessage = $icon.hasClass('icon-noneFavorite-medium') ?
                                         instructions.MarkingFavoriteInstruction :
                                         instructions.UnmarkingFavoriteInstruction;
                    $icon.attr('title', instructionMessage);
                    $icon.attr('data-original-title', instructionMessage);
                    common.ShowInfoMessage(result.Message);
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