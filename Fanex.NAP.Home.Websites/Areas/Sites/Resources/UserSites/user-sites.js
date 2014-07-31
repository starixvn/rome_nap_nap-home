/* File Created: July 29, 2014 */
/* global Namespace, common */
'use strict';

$(document).ready(function () {
    var userSites = new Fanex.NAP.Home.Sites.UserSites();

    userSites.Initialize();
});

Namespace.Register("Fanex.NAP.Home.Sites.UserSites");
Fanex.NAP.Home.Sites.UserSites = function () {
    var $this = this;

    var domElements = {
        divDomainButtons: null,
        divFuntionButtons: null
    };
    var urls = {
        FunctionList: common.ResolveRootUrl('Sites/UserSites/FunctionList'),
        TopFavoriteFunctions: common.ResolveRootUrl('Sites/UserSites/TopFavoriteFunctions'),
        UpdatingFavoriteFunction: common.ResolveRootUrl('Sites/UserSites/UpdatingFavoriteFunction')
    };
    var favoriteUpdated = false;

    // Cache DOM elements.
    var cacheDOMElements = function () {
        domElements.divFuntionButtons = $('div.function-button');
        domElements.divDomainButtons = $('div.domain-button');
    };

    // Handle events of elements.
    var handleEvents = function () {
        // Click on Domains icon.
        domElements.divDomainButtons.on('click.nap-home', function () {
            var $button = $(this);
            var $icon = $button.children('span');
            var $menu = $button.next();

            if ($menu.is(':hidden')) {
                hideMenus();
                $button.addClass('active');
                $icon.addClass('active');
                $menu.show().children().niceScroll({
                    cursorcolor: "#8C8C8C",
                    cursorwidth: '7px',
                    cursorborderradius: '3px'
                });
            } else {
                $button.removeClass('active');
                $icon.removeClass('active');
                $menu.hide();
            }

            return false;
        });

        // Click on Function icon.
        domElements.divFuntionButtons.on('click.nap-home', function () {
            var $button = $(this);
            var $tile = $button.closest('div.description');
            var $icon = $button.children('span');
            var $menu = $button.next();
            var siteId = $tile.attr('data-site-id');

            if ($menu.is(':hidden')) {
                favoriteUpdated = false;
                getFunctionsOfSite($menu, siteId);

                hideMenus();
                $button.addClass('active');
                $icon.addClass('active');
            } else {
                $button.removeClass('active');
                $icon.removeClass('active');
                $tile.trigger('reload.nap-home');
                $menu.hide();
            }

            return false;
        });

        // Collapse/expand function tree.
        $('div.function-menu-wrapper').on('click.nap-home', 'span.group-node', function () {
            var $groupNode = $(this);

            $groupNode.next().slideToggle();
            $groupNode.toggleClass('expand-node').children('span').toggleClass('icon-expand').toggleClass('icon-collapse');
            $groupNode.closest('ul.menu-content').getNiceScroll().resize();

            return false;
        });

        // Mark/unmark favorite function.
        $('div.function-menu-wrapper').on('click.nap-home', 'span.icon-star', function () {
            var $starIcon = $(this);
            var $tile = $starIcon.closest('div.description');
            var siteId = $tile.attr('data-site-id');
            var functionId = $starIcon.attr('data-function-id');
            var isMarked = $starIcon.hasClass('icon-noneFavourite');

            favoriteUpdated = true;
            $starIcon.toggleClass('icon-favourite').toggleClass('icon-noneFavourite');
            markUnmarkFavoriteFunction({
                SiteId: siteId,
                FunctionId: functionId,
                IsMarked: isMarked
            });
        });

        // Reload tiles.
        $('div.description').on('reload.nap-home', function () {
            var $tile = $(this);
            var $bgImage = null;
            var siteId = $tile.attr('data-site-id');

            if (favoriteUpdated) {
                favoriteUpdated = false;
                $bgImage = $tile.children('img.bg-img');
                getTopFavoriteFunctions($bgImage, siteId);
            }
        });

        // Hide menus when clicking on page.
        $('html').on('click.nap-home', function (event) {
            if ($(event.target).closest('.menu-wrapper').length === 0) {
                hideMenus();
            }
        });
    };

    // Hide all menus.
    var hideMenus = function () {
        // Hide URL menu.
        domElements.divDomainButtons.filter('.active').each(function () {
            var $button = $(this);
            var $icon = $button.children('span');
            var $menu = $button.next();

            $button.removeClass('active');
            $icon.removeClass('active');
            $menu.hide();
        });

        // Hide function menu.
        domElements.divFuntionButtons.filter('.active').each(function () {
            var $button = $(this);
            var $tile = $button.closest('div.description');
            var $icon = $button.children('span');
            var $menu = $button.next();

            $button.removeClass('active');
            $icon.removeClass('active');
            $tile.trigger('reload.nap-home');
            $menu.hide();
        });
    };

    // Get Function menu content.
    var getFunctionsOfSite = function ($menu, siteId) {
        $menu.html('');
        common.DisableInput();
        $.ajax({
            type: 'POST',
            url: urls.FunctionList,
            contentType: 'application/json; charset=utf-8',
            dataType: 'html',
            data: JSON.stringify({ SiteId: siteId }),
            success: function (functionList) {
                var $menuContent = null;

                if (functionList && functionList !== '') {
                    $menu.html(functionList).show();
                    $menuContent = $menu.children('ul.menu-content');
                    $menuContent.find('span.icon-star').tooltip({
                        placement: 'left'
                    });

                    if ($menuContent.length > 0 && $menuContent.getNiceScroll().length === 0) {
                        $menuContent.niceScroll({
                            cursorcolor: "#8C8C8C",
                            cursorwidth: '7px',
                            cursorborderradius: '3px'
                        });
                    }
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

    // Get Top Favorite Functions.
    var getTopFavoriteFunctions = function ($bgImage, siteId) {
        var bgImageUrl = $bgImage.attr('src');

        common.DisableInput();
        $.ajax({
            type: 'POST',
            url: urls.TopFavoriteFunctions,
            contentType: 'application/json; charset=utf-8',
            dataType: 'html',
            data: JSON.stringify({ SiteId: siteId }),
            success: function (favorites) {
                
                $bgImage.next('.link').remove(); // Remove old favorite list.

                if (favorites && favorites !== '') {
                    $bgImage.after(favorites); // Append new favorite list.

                    if (bgImageUrl.indexOf('NonFavorite') > -1) {
                        bgImageUrl = bgImageUrl.replace('NonFavorite', 'Favorite');
                        $bgImage.attr('src', bgImageUrl);
                    }
                } else if (bgImageUrl.indexOf('NonFavorite') < 0 && bgImageUrl.indexOf('Favorite') > -1) {
                    bgImageUrl = bgImageUrl.replace('Favorite', 'NonFavorite');
                    $bgImage.attr('src', bgImageUrl);
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

    // Get Top Favorite Functions.
    var markUnmarkFavoriteFunction = function (parameters) {
        common.DisableInput();
        $.ajax({
            type: 'POST',
            url: urls.UpdatingFavoriteFunction,
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