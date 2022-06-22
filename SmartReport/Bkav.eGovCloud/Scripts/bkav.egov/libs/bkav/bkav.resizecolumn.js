(function () {
    jQuery.fn['resizecolumn'] = function (settings) {
        settings = $.extend({
            isUsingCustomScroll: true,
            isResizeColumn: true,
            isFixHeightContent: true,
            isAddHoverRow: true,
            prefix: "",
            isUseCookie: true,
            onresizefinish: function () { }
        }, settings);
        var isResizing = null;
        var element = null;
        var index = null;
        var tableHeader = null;
        var tableContent = null;
        var tableHeaderCol = null;
        var tableContentCol = null;
        var clientX = null;
        var width = null;
        var widthTable = null;

        this.each(function (i, el) {
            var elQ = $(el);
            var id = $(elQ).attr('id');
            $(this).find(".grid-content tr:odd").addClass("trodd");
            if (settings.isAddHoverRow) {
                var tr = $('#' + id + ' .grid-content table tbody tr');
                if (tr.length > 0) {
                    tr.hover(function () {
                        $(this).addClass("hover-row");
                    }, function () {
                        $(this).removeClass("hover-row");
                    });

                    tr.click(function () {
                        tr.removeClass("highlight-row");
                        $(this).addClass("highlight-row");
                    });

                    tr.mousedown(function (e) {
                        if (e.which === 3) {
                            tr.removeClass("highlight-row");
                            $(this).addClass("highlight-row");
                        }
                    });


                    $('#' + id + ' .grid-content table tbody tr input, #' + id + ' .grid-content table tbody tr a, #' + id + ' tbody tr button').click(function (e) {
                        e.stopPropagation();
                        return true;
                    });
                }
            }
            if (settings.isFixHeightContent) {
                $(this).find('.grid-content').css({ 'overflow-y': 'scroll' });
            } else {
                $(this).find('.grid-content').css({ 'overflow-y': 'visible', 'height': '' });
            }

            if (settings.isResizeColumn) {
                if (settings.isFixHeightContent && !settings.isUsingCustomScroll) {
                    $('.grid-header').css('padding-right', $.layout.scrollbarWidth() + 'px');
                }
                $(this).find('.grid-content').css({ 'overflow-x': 'auto' });

                var uniqueKey = id + "_" + i + "_" + settings.prefix;
                var columns = $(elQ).find("table thead tr:first th");
                $('.grid-content').unbind('scroll');
                $('.grid-content').scroll(function () {
                    $('.grid-header-wrap').scrollLeft($(this).scrollLeft());
                });
                var widthColumn = settings.isUseCookie ? $.cookie(uniqueKey) : null;
                tableHeader = $('#' + id + ' .grid-header table');
                tableHeaderCol = $('#' + id + ' .grid-header table colgroup col');
                tableContent = $('#' + id + ' .grid-content table');
                tableContentCol = $('#' + id + ' .grid-content table colgroup col');
                tableHeader.width('auto');
                tableContent.width('auto');
                if (widthColumn) {
                    var arrayWidthColumn = widthColumn.split(',');
                    if (arrayWidthColumn.length == tableHeaderCol.length) {
                        widthTable = 0;
                        for (var j = 0; j < arrayWidthColumn.length; j++) {
                            if (tableHeaderCol.eq(j)) {
                                tableHeaderCol.eq(j).width(arrayWidthColumn[j] + 'px');
                                tableContentCol.eq(j).width(arrayWidthColumn[j] + 'px');
                                widthTable += parseInt(arrayWidthColumn[j]);
                            }
                        }
                        tableHeader.width(widthTable + 'px');
                        tableContent.width(widthTable + 'px');
                    }
                } else {
                    var columnNoWidth = [];
                    var totalWidth = 0;
                    tableHeaderCol.each(function (idx, item) {
                        if ($(this).width() == 0 || $(this).inlineStyle('width') == null) {
                            columnNoWidth.push(idx);
                        } else {
                            totalWidth += $(this).width();
                        }
                    });
                    if (columnNoWidth.length > 0) {
                        widthTable = $(elQ).width() - 1;
                        if (settings.isFixHeightContent) {
                            widthTable = widthTable - $.layout.scrollbarWidth();
                        }
                        var allWidth = widthTable - totalWidth;
                        var widthCol = allWidth / columnNoWidth.length;
                        for (var g = 0; g < columnNoWidth.length; g++) {
                            tableHeaderCol.eq(columnNoWidth[g]).width(widthCol);
                            tableContentCol.eq(columnNoWidth[g]).width(widthCol);
                        }
                    } else {
                        widthTable = 0;
                        tableHeaderCol.each(function (idx, item) {
                            widthTable += $(item).width();
                        });
                    }
                    tableHeader.width(widthTable + 'px');
                    tableContent.width(widthTable + 'px');
                }
                if (settings.isUsingCustomScroll) {
                    $('.grid-content').niceScroll();
                }
                columns.each(function (k, column) {
                    $(column).unbind('mousedown');
                    $(column).mousedown(function (e) {
                        if ($(e.target).css('cursor') == 'col-resize') {
                            e.preventDefault();
                            $('body').addClass('unselectable');
                            isResizing = true;
                            element = e.target;
                            index = $(element).index();
                            clientX = e.clientX;
                            width = $(element).outerWidth();
                            widthTable = tableContent.width();
                            var layer = $('<div id="resize-column-layer" style="position:fixed; z-index:100; top:0; left:0; opacity: 0; filter: alpha(opacity=0); background-color: #000;cursor: col-resize !important;"></div>')
                                .css({ height: $(document).height(), width: $(document).width(), display: 'block' })
                                .mousemove(function (ev) {
                                    if (isResizing) {
                                        var difference = clientX - ev.clientX;
                                        if ((width - difference) > 10) {
                                            var newWidth = width - difference;
                                            tableHeaderCol.eq(index).width(newWidth);
                                            tableContentCol.eq(index).width(newWidth);
                                            tableHeader.width(widthTable - difference);
                                            tableContent.width(widthTable - difference);
                                        }
                                    }
                                })
                                .mouseup(function () {
                                    if (isResizing) {
                                        isResizing = false;
                                        element = null;
                                        $('body').css({ cursor: '' });
                                        $('body').removeClass('unselectable');
                                        if (settings.isUseCookie) {
                                            var str = [];
                                            var columnNoWidth = [];
                                            var totalWidth = 0;
                                            tableHeaderCol.each(function (idx) {
                                                if ($(this).width() == 0 || $(this).inlineStyle('width') == null) {
                                                    columnNoWidth.push(idx);
                                                } else {
                                                    totalWidth += $(this).width();
                                                }
                                                str.push($(this).width());
                                            });
                                            if (columnNoWidth.length > 0) {
                                                var allWidth = tableContent.width() - totalWidth;
                                                var widthCol = allWidth / columnNoWidth.length;
                                                for (var g = 0; g < columnNoWidth.length; g++) {
                                                    str[columnNoWidth[g]] = widthCol;
                                                }
                                            }
                                            $.cookie(uniqueKey, str.join(','), { expires: 7, path: "/", secure: true });
                                        }
                                        if (settings.onresizefinish && $.isFunction(settings.onresizefinish)) {
                                            settings.onresizefinish();
                                        }
                                        if (settings.isUsingCustomScroll) {
                                            $('.grid-content').getNiceScroll().resize();
                                        }
                                        $(this).remove();
                                    }
                                }).appendTo('body');
                            if (!$.support.fixedPosition) {
                                layer.css({
                                    'position': 'absolute',
                                    'height': $(document).height()
                                });
                            }
                        }
                    });
                    $(column).unbind('mousemove');
                    $(column).mousemove(function (e) {
                        if ($(e.target).attr('class') != 'grid-header-wrap' && $(e.target).attr('class') == 'header') {
                            var bounds = $(e.target).bounds();
                            if (Math.abs((bounds.left + bounds.width + 6) - (e.clientX)) <= 5) {
                                $(e.target).css({ cursor: 'col-resize' });
                            }
                            else {
                                $(e.target).css({ cursor: '' });
                            }
                        }
                    });
                });
            } else {
                $(this).find('.grid-content').css({ 'overflow-x': 'visible' });
                $(this).find('.grid-header table').css('width', $(this).width() - 1);
                $(this).find('.grid-content table').css('width', $(this).width() - 2);
            }
        });
    };
})();

(function () {
    jQuery.fn['bounds'] = function () {
        var bounds = {
            left: Number.POSITIVE_INFINITY,
            top: Number.POSITIVE_INFINITY,
            right: Number.NEGATIVE_INFINITY,
            bottom: Number.NEGATIVE_INFINITY,
            width: Number.NaN,
            height: Number.NaN
        };

        this.each(function (i, el) {
            var elQ = $(el);
            var off = elQ.offset();
            off.right = off.left + $(elQ).width();
            off.bottom = off.top + $(elQ).height();

            if (off.left < bounds.left)
                bounds.left = off.left;

            if (off.top < bounds.top)
                bounds.top = off.top;

            if (off.right > bounds.right)
                bounds.right = off.right;

            if (off.bottom > bounds.bottom)
                bounds.bottom = off.bottom;
        });

        bounds.width = bounds.right - bounds.left;
        bounds.height = bounds.bottom - bounds.top;
        return bounds;
    };
})();

(function () {
    $.fn['inlineStyle'] = function (prop) {
        var styles = $(this).attr("style"),
             value = null;
        styles && styles.split(";").forEach(function (e) {
            var style = e.split(":");
            if ($.trim(style[0]) === prop) {
                value = style[1];
            }
        });
        return value;
    };
})();

(function () {
    jQuery.fn['normalGrid'] = function() {
        var width = $(this).width();
        $(this).find('.table-main').css('width', width);
        $(this).find('.grid-content').css({height: "", overflow: 'visible', 'overflow-x': 'visible'});
        $(this).find(".grid-header").css({ 'border-bottom-style': 'none' });
        var columns = $(this).find(".grid-header table thead tr:first th");
        var columns2 = $(this).find(".grid-content table tbody tr:first td");
        columns.each(function(k, column) {
            $(column).width(columns2[k].width());
        });
    };
})();

// ~ resizecolumn
(function () {
    jQuery.fn['resizableGrid'] = function () {
        
    };
})();