
(function () {
    require(["/scripts/bkav.egov/config.js?v=1rttr"]);
    egov.screenSize = {
        w: 215,
        h: 600,
        footerH: 49,
        contentMaxW: Math.round(215 * 0.7), // độ rộng tối đa nội dung trao đổi, comment, ...
        contentH: 600 - 65,
        contentHWithoutFooter: 600 - 65 - 49
    };

    $.fn.doubleTap = function (doubleTapCallback) {
        return this.each(function () {
            var elm = this;
            var lastTap = 0;
            $(elm).bind('vmousedown', function (e) {
                var now = (new Date()).valueOf();
                var diff = (now - lastTap);
                lastTap = now;
                if (diff < 250) {
                    if ($.isFunction(doubleTapCallback)) {
                        doubleTapCallback.call(elm);
                    }
                }
            });
        });
    }

    $.fn.scrollToBottom = function () {
        var objDiv = this.get(0);
        objDiv.scrollTop = objDiv.scrollHeight;
    }

    $.fn.scrollLoadMore = function (direction, callback) {
        if (typeof callback !== 'function') {
            return;
        }

        direction = direction || 'next';

        ScrollLoadMore && new ScrollLoadMore(this.get(0), {
            offset: 70,
            direction: direction,
            callback: function () {
                typeof callback === 'function' && callback();
            }
        });
    }

})();