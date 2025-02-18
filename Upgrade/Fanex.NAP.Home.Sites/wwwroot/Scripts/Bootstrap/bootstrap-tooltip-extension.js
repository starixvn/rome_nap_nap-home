(function ($) {
    var old,
        TooltipExtension = function (element, options) {
            this.init('tooltip', element, options);
        };

    TooltipExtension.prototype = $.extend({}, $.fn.tooltip.Constructor.prototype, {
        constructor: TooltipExtension,
        show: function () {
            var that = this,
                $tip,
                tipId,
                pos,
                actualWidth,
                actualHeight,
                placement,
                autoToken,
                autoPlace,
                calculatedOffset,
                e = $.Event('show.bs.' + this.type),
                inDom = $.contains(document.documentElement, this.$element[0]);

            if (this.hasContent() && this.enabled) {
                this.$element.trigger(e);

                if (e.isDefaultPrevented() || !inDom) {
                    return;
                }

                $tip = this.tip();
                tipId = this.getUID(this.type);
                this.setContent();
                $tip.attr('id', tipId);
                this.$element.attr('aria-describedby', tipId);

                if (this.options.animation) {
                    $tip.addClass('fade');
                }

                placement = typeof this.options.placement === 'function' ?
                            this.options.placement.call(this, $tip[0], this.$element[0]) :
                            this.options.placement;
                autoToken = /\s?auto?\s?/i;
                autoPlace = autoToken.test(placement);

                if (autoPlace) {
                    placement = placement.replace(autoToken, '') || 'top';
                }

                $tip
                .detach()
                .css({ top: 0, left: 0, display: 'block' })
                .addClass(placement)
                .data('bs.' + this.type, this);

                this.options.container ? $tip.appendTo(this.options.container) : $tip.insertAfter(this.$element);
                pos = this.getPosition();
                actualWidth = $tip[0].offsetWidth;
                actualHeight = $tip[0].offsetHeight;

                if (autoPlace) {
                    var orgPlacement = placement;
                    var $parent = this.$element.parent();
                    var parentDim = this.getPosition($parent);

                    placement = placement === 'bottom' && pos.top + pos.height + actualHeight - parentDim.scroll > parentDim.height ? 'top' :
                                placement === 'top' && pos.top - parentDim.scroll - actualHeight < 0 ? 'bottom' :
                                placement === 'right' && pos.right + actualWidth > parentDim.width ? 'left' :
                                placement === 'left' && pos.left - actualWidth < parentDim.left ? 'right' :
                                placement;

                    $tip.removeClass(orgPlacement).addClass(placement);
                }

                calculatedOffset = this.getCalculatedOffset(placement, pos, actualWidth, actualHeight);
                this.applyPlacement(calculatedOffset, placement);

                var complete = function () {
                    that.$element.trigger('shown.bs.' + that.type);
                    that.hoverState = null;
                };

                $.support.transition && this.$tip.hasClass('fade') ?
                $tip.one('bsTransitionEnd', complete).emulateTransitionEnd(150) :
                complete();
            }
        },
        getCalculatedOffset: function (placement, pos, actualWidth, actualHeight) {
            var tp;

            switch (placement) {
                case 'bottom':
                    tp = { top: pos.top + pos.height, left: pos.left + pos.width / 2 - actualWidth / 2 };
                    break;
                case 'top':
                    tp = { top: pos.top - actualHeight, left: pos.left + pos.width / 2 - actualWidth / 2 };
                    break;
                case 'left':
                    tp = { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left - actualWidth };
                    break;
                case 'right':
                    tp = { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left + pos.width };
                    break;

                case 'bottom-left':
                    tp = { top: pos.top + pos.height + 5, left: pos.left };

                    if (this.$element.outerWidth() <= 18) { // if button is small move tooltip left
                        tp.left -= 4;
                    }

                    break;
                case 'bottom-right':
                    tp = { top: pos.top + pos.height + 5, left: pos.left + pos.width - actualWidth };

                    if (this.$element.outerWidth() <= 18) { // if button is small move tooltip left
                        tp.left += 4;
                    }

                    break;
                case 'top-left':
                    tp = { top: pos.top - actualHeight - 10, left: pos.left };

                    if (this.$element.outerWidth() <= 18) { // if button is small move tooltip left
                        tp.left -= 4;
                    }

                    break;
                case 'top-right':
                    tp = { top: pos.top - actualHeight - 10, left: pos.left + pos.width - actualWidth };

                    if (this.$element.outerWidth() <= 18) { // if button is small move tooltip left
                        tp.left += 4;
                    }

                    break;
            }

            return tp;
        }
    });

    old = $.fn.tooltip;

    $.fn.tooltip = function (option) {
        return this.each(function () {
            var $this = $(this),
                data = $this.data('bs.tooltip'),
                options = typeof option === 'object' && option;

            if (!data && option === 'destroy') {
                return;
            }

            if (!data) {
                $this.data('bs.tooltip', (data = new TooltipExtension(this, options)));
            }

            if (typeof option === 'string') {
                data[option]();
            }
        });
    };

    $.fn.tooltip.Constructor = TooltipExtension;

    $.fn.tooltip.defaults = {
        animation: true,
        placement: 'top',
        selector: false,
        template: '<div class="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',
        trigger: 'hover focus',
        title: '',
        delay: 0,
        html: false,
        container: false
    };

    $.fn.tooltip.noConflict = function () {
        $.fn.tooltip = old;

        return this;
    };
})(window.jQuery);