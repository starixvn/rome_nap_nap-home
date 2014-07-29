/* File Created: July 29, 2014 */
/* global Namespace */
'use strict';

$(document).ready(function () {
    var userSites = new Fanex.NAP.Home.Sites.UserSites();

    userSites.Initialize();
});

Namespace.Register("Fanex.NAP.Home.Sites.UserSites");
Fanex.NAP.Home.Sites.UserSites = function () {
    var $this = this;

    var domElements = {
        divSiteUrls: null,
        divFuntions: null
    };

    // Cache DOM elements.
    var cacheDOMElements = function () {
        domElements.divFuntions = $('div.favorite');
        domElements.divSiteUrls = $('div.url-site');
    };

    // Handle events of elements.
    var handleEvents = function () {
        domElements.divSiteUrls.on('click.nap-home-menu', function () {
            var $button = $(this);
            var $icon = $button.find('span');
            var $menu = $button.find('ul');

            if ($menu.is(':hidden')) {
                hideMenus();
                $button.addClass('active');
                $icon.addClass('active');
                $menu.show();
            } else {
                $button.removeClass('active');
                $icon.removeClass('active');
                $menu.hide();
            }

            return false;
        });

        // Hide menus when clicking on page.
        $('html').on('click.nap-home-menu', function (event) {
            if (!$(event.target).hasClass('expand menu-node')) {
                hideMenus();
            }
        });
    };

    // Hide all menus.
    var hideMenus = function () {
        // Hide URL menu.
        domElements.divSiteUrls.filter('.active').each(function () {
            var $button = $(this);
            var $icon = $button.find('span');
            var $menu = $button.find('ul');

            $button.removeClass('active');
            $icon.removeClass('active');
            $menu.hide();
        });

        // Hide function menu.
        domElements.divFuntions.filter('.active').each(function () {
            var $button = $(this);
            var $tile = $button.closest('div.description');
            var $icon = $button.find('span');
            var $menu = $button.find('ul');

            $button.removeClass('active');
            $icon.removeClass('active');

            if ($menu) {
                $menu.hide();
            }

            $tile.trigger('close.nap-home-menu');
        });
    };

    // Intialize page.
    $this.Initialize = function () {
        cacheDOMElements();
        handleEvents();
    };
};