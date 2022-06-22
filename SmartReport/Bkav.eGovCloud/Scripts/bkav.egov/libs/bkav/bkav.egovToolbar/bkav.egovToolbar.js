(function ($) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    if (typeof ($.contextMenu) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery ContextMenu, hãy tải thư viện jQuery ContextMenu trước khi sử dụng';
    }
    if (typeof ($.fn.blockpanel) === 'undefined') {
        throw 'Thư viện này sử dụng bkav.blockpanel.js, hãy tải thư viện bkav.blockpanel.js trước khi sử dụng';
    }

    jQuery.fn['egovToolbar'] = function (settings) {
        this.each(function (i, el) {
            var $el = $(el);
            if (!$el.hasClass('toolbar')) {
                $el.addClass('toolbar');
            }

            $outer = $(el);

            $.each(settings.items, function (key, value) {
                if (typeof value === 'string') {
                    $outer.append('<span class="divider"></span>');
                } else if (value != null && typeof value === 'object') {
                    var datakey = key + '' + i;
                    var $item = $('<span data-key="' + datakey + '" class="toolbar-btn"></span>');
                    var selector = 'span[data-key=' + datakey + ']';
                    if (value.class && typeof value.class === 'string' && value.class !== '') {
                        $item.addClass(value.class);
                    }
                    if (value.disable && typeof value.disable === 'boolean') {
                        $item.addClass('disabled');
                    }
                    if (value.id && typeof value.id === 'string' && value.id !== '') {
                        $item.attr('id', value.id);
                    }
                    if (value.title && typeof value.title === 'string' && value.id !== '') {
                        $item.attr('title', value.title);
                    }
                    var $a = $('<a href="javascript:void(0)"></a>');
                    $a.append('<img src="../../Content/Images/clear.gif" alt="">');
                    $a.append("<em>" + value.name + "</em>");

                    $item.append($a);
                    $outer.append($item);

                    if (value.dropdownItems && value.dropdownItems.items) {
                        $a.append('<b></b>');
                        $.contextMenu({
                            selector: selector,
                            trigger: 'left',
                            position: function (opt, x, y) {
                                opt.$menu.position({
                                    my: 'left top',
                                    at: 'left bottom',
                                    of: opt.$trigger,
                                    offset: "0 6"
                                });
                            },
                            build: function () {
                                if ($(selector).hasClass('disabled')) {
                                    return null;
                                }
                                return value.dropdownItems;
                            }
                        });
                    } else if (value.dropdownItemsLazyLoading) {
                        $a.append('<b></b>');
                        var offsetX, offsetY;
                        $item.mouseup(function (e) {
                            offsetX = e.clientX;
                            offsetY = e.clientY;
                        });
                        $item.click(function (e) {
                            var self = this;
                            if ($item.hasClass('disabled')) {
                                $(selector).contextMenu(false);
                                return;
                            }
                            if (value.callback && typeof value.callback === 'function') {
                                if (value.callback() === false) {
                                    $(selector).contextMenu(false);
                                    return;
                                }
                            }
                            $(selector).contextMenu(true);
                            if ($item.attr('data-loadedDropdownItems')) {
                                return;
                            }
                            $.contextMenu('destroy', selector);
                            $.ajax({
                                type: value.dropdownItemsLazyLoading.type,
                                data: value.dropdownItemsLazyLoading.data,
                                url: value.dropdownItemsLazyLoading.url,
                                beforeSend: function (xhr, setting) {
                                    if (value.dropdownItemsLazyLoading.beforeSend && typeof value.dropdownItemsLazyLoading.beforeSend === 'function') {
                                        value.dropdownItemsLazyLoading.beforeSend(setting);
                                    }
                                    $(selector).blockpanel({ backgroundColor: 'transparent', icon: { width: 24, height: 24 } });
                                },
                                success: function (data) {
                                    var options = value.dropdownItemsLazyLoading.onBuildDropdownItems(data);
                                    if (options != null) {
                                        if (options.items) {
                                            var existItem = false;
                                            for (var item in options.items) {
                                                existItem = true;
                                                break;
                                            }
                                            if (existItem) {
                                                $.contextMenu({
                                                    selector: selector,
                                                    trigger: 'left',
                                                    build: function () {
                                                        return value.dropdownItemsLazyLoading.onBuildDropdownItems(data);
                                                    },
                                                    position: function (opt, x, y) {
                                                        opt.$menu.position({
                                                            my: 'left top',
                                                            at: 'left bottom',
                                                            of: opt.$trigger,
                                                            offset: "0 0"
                                                        });
                                                    },
                                                });
                                                e.preventDefault();
                                                e.stopImmediatePropagation();
                                                $(self).trigger($.Event("contextmenu", { data: e.data })); //, pageX: e.pageX, pageY: e.pageY 
                                                if (!options.items.notfound) {
                                                    $item.attr('data-loadedDropdownItems', 'true');
                                                }
                                            }
                                        }
                                    }
                                },
                                complete: function () {
                                    $(selector).unblockpanel();
                                },
                                error: function () {
                                    value.dropdownItemsLazyLoading.onError();
                                }
                            });
                        });
                    } else {
                        if (value.callback && typeof value.callback === 'function') {
                            $item.click(function () {
                                if ($item.hasClass('disabled')) {
                                    return;
                                }
                                value.callback();
                            });
                        }
                    }
                }
            });
        });
        return settings;
    };

})(window.jQuery);