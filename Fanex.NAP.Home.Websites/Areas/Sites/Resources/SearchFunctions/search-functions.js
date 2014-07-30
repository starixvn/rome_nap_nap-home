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
        divDomainButtons: null,
        divFuntionButtons: null
    };
    var urls = {
        MarkFavoriteFunction: common.ResolveRootUrl('Sites/SearchFunctions/MarkFavoriteFunction')
    };
    var favoriteUpdated = false;

    // Cache DOM elements.
    var cacheDOMElements = function () {
        domElements.divFuntionButtons = $('div.function-button');
        domElements.divDomainButtons = $('div.domain-button');
    };

    // Handle events of elements.
    var handleEvents = function () {
    };

    // Get Top Favorite Functions.
    var markUnmarkFavoriteFunction = function (parameters) {
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