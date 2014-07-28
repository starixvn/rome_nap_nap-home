/* File Created: July 28, 2014 */
$(function () {
    var urlSite = $('.url-site'),
        favorite = $('.favorite');

    urlSite.click(function (event) {
        var $this = $(this),
            URL = $this.children('.list-url'),
            icon_site = $this.children('.icon-arrow-site');

        if (URL.is(':hidden')) {
            hideUrlFunction();
            icon_site.addClass('active');
            URL.show();
        } else {
            icon_site.removeClass('active');
            URL.hide();
        }
        event.stopPropagation();
    });

    favorite.click(function (event) {
        var $this = $(this),
            favoriteList = $this.children('.function'),
            icon_current_function = $this.children('.icon-function');

        if (favoriteList.is(':hidden')) {
            hideUrlFunction();
            icon_current_function.addClass('active');
            favoriteList.show();
        } else {
            icon_current_function.removeClass('active');
            favoriteList.hide();
        }
        event.stopPropagation();
    });

    $('body').click(function () {
        hideUrlFunction();
    });
});

function hideUrlFunction() {
    var listUrl = $('.list-url'),
        icon_arrow_site = $('.icon-arrow-site'),
        icon_function = $('.icon-function'),
        functionList = $('.function');

    listUrl.hide();
    icon_arrow_site.removeClass('active');
    functionList.hide();
    icon_function.removeClass('active');
}