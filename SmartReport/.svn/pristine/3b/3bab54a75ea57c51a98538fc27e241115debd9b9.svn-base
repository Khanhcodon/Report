(function ($) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }

    var refreshGrid = function ($gridContent, isAddHoverRow) {
        $gridContent.find(".trodd").removeClass("trodd");
        $gridContent.find("tr:odd").addClass("trodd");
        if (isAddHoverRow) {
            var tr = $gridContent.find("tr");
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
                $('.grid-content table tbody tr input, .grid-content table tbody tr a, .grid-content table tbody tr button').unbind('click').click(function (e) {
                    e.stopPropagation();
                    return true;
                });
            }
        }
    };

    var _hasSupportLocalStorage = function () {
        /// <summary>
        /// Kiểm tra trình duyệt có hỗ trợ localStorage hay không?
        /// </summary>
        if ('localStorage' in window && window['localStorage'] !== null) {
            //Kiểm tra trình duyêt có cho phép thao tác với localStorage
            try {
                window.localStorage.setItem('egov', 'Bkav Corporation');
                window.localStorage.removeItem('egov');
                return true;
            }
            catch (ex) {
                return false;
            }
        } else {
            return false;
        }
    };

    jQuery.fn['grid'] = function (settings) {
        if (typeof settings === 'string') {
            if (settings.toLowerCase() === 'refresh') {
                this.each(function (i, el) {
                    refreshGrid($(el).parent(), $(el).data()['bkav.grid'].isAddHoverRow);
                });
                return;
            }
        }

        settings = $.extend({
            isUsingCustomScroll: false,
            isAutoHideScroll: true,
            isResizeColumn: true,
            isFixHeightContent: true,
            isAddHoverRow: true,
            isUseCookie: true,
            isRenderPanelGrid: true,
            onresizefinish: function () { }
        }, settings);

        this.each(function (i, el) {
            var isResizing = null;
            var element = null;
            var index = null;
            var clientX = null;
            var width = null;
            var widthTable = null;
            var $elQ = $(el);
            if ($elQ.length === 0) {
                return false;
            }
            if ($elQ[0].tagName !== 'TABLE') {
                return false;
            }

            $elQ.data('bkav.grid', settings);
            var $grid;
            if (settings.isRenderPanelGrid) {
                $grid = $('<div></div>');
                $elQ.after($grid);
            } else {
                $grid = $elQ.parent();
            }
            $grid.attr('data-key', $elQ.attr('id'));
            if (!$grid.hasClass('grid')) {
                $grid.addClass('grid');
            }

            if (settings.width) {
                if (typeof settings.width === 'number') {
                    $grid.width(settings.width + 'px');
                } else if (typeof settings.width === 'string') {
                    $grid.width(settings.width);
                }
            }
            if (!$elQ.hasClass('table-main')) {
                $elQ.addClass('table-main');
            }
            //Build header
            var $gridHeader = $('<div class="grid-header"></div>');
            var $gridHeaderWrap = $('<div class="grid-header-wrap"></div>');
            var $tableHeader = $('<table class="table table-hover table-main"></table>');
            var countColumn = $elQ.find('thead tr:first th').length;
            var $colgroupHeader = $('<colgroup></colgroup>');

            //Tạo header cho table header
            $tableHeader.append($colgroupHeader);
            if ($elQ.find('col').length === 0) {
                var $colgroupContent = $('<colgroup></colgroup>');
                $elQ.children('tbody').before($colgroupContent);
                for (var j = 0; j < countColumn; j++) {
                    $colgroupHeader.append('<col />');
                    $colgroupContent.append('<col />');
                }
            } else {
                $colgroupHeader.append($elQ.find('colgroup').html());
            }

            $elQ.find('thead tr:first th').each(function (idx, item) {
                if (!$(item).hasClass('header')) {
                    $(item).addClass('header');
                }
            });

            $tableHeader.append($elQ.find('thead'));
            $gridHeaderWrap.append($tableHeader);
            $gridHeader.append($gridHeaderWrap);
            $grid.append($gridHeader);
            // $gridHeaderWrap.width($tableHeader.width());

            //Build pager
            if ($elQ.find('tfoot tr td').length > 0) {
                var $gridPager = $('<div class="grid-pager grid-pager-wrap grid-pager-bottom"></div>');
                $gridPager.append($elQ.find('tfoot tr td:first').html());
                $grid.append($gridPager);
                $elQ.find('tfoot').remove();
            }

            //Build content
            var $gridContent = $('<div class="grid-content"></div>');
            $gridContent.append($elQ);
            $gridHeader.after($gridContent);
            refreshGrid($gridContent, settings.isAddHoverRow);

            if (settings.isFixHeightContent) {
                //$gridContent.css({ 'overflow-y': 'auto', 'min-height': '300px' });
                $gridContent.css({ 'overflow-y': 'auto' });//bỏ min-height do bản desktop khi rezisie không nhận scroll ngang
                if (settings.height) {
                    if (typeof settings.height === 'number') {
                        $gridContent.height(settings.height + 'px');
                    } else if (typeof settings.height === 'string') {
                        $gridContent.height(settings.height);
                    }
                }
            } else {
                $gridContent.css({ 'overflow-y': 'visible', 'height': '' });
                if (settings.height) {
                    if (typeof settings.height === 'number') {
                        $grid.height(settings.height + 'px');
                    } else if (typeof settings.height === 'string') {
                        $grid.height(settings.height);
                    }
                }
            }

            // resize chiều rộng các cột
            if (settings.isResizeColumn) {

                // $gridContent.css({ 'overflow-x': 'auto' });
                var uniqueKey = i + "_" + $elQ.attr('id') + "_" + window.location.pathname;
                var columns = $tableHeader.find("thead tr:first th");

                //Bắt các sự kiện để khi scroll lesft thì các cột trên table header cũng thay đổi vị trí theo
                $gridContent.bind('mousedown, mouseup, mousemove', function () {
                    var value = $(this).scrollLeft();
                    $gridHeaderWrap.css({ "margin-left": -value })
                });

                //Lấy thiết lập các cột
                var widthColumn = null;
                if (settings.isUseCookie) {
                    if (_hasSupportLocalStorage()) {
                        widthColumn = window.localStorage.getItem(uniqueKey);
                    }
                    else {
                        widthColumn = $.cookie(uniqueKey);
                    }
                }

                //Bind lại các thiết lập trước đấy cho table
                var $tableHeaderCol = $gridHeaderWrap.find('col');
                var $tableContent = $gridContent.children('table');
                var $tableContentCol = $gridContent.find('col');

                $tableHeader.css({ minWidth: '1024px' });
                $tableContent.css({ minWidth: '100%' });
                // $gridContent.width($tableContent.width());

                if (settings.isFixHeightContent && !settings.isUsingCustomScroll) {
                    if ($gridContent.height() < $tableContent.height()) {
                        $gridHeader.css('padding-right', $.layout.scrollbarWidth() + 'px');
                        $tableContent.css('padding-right', $.layout.scrollbarWidth() + 'px');
                    }
                }

                if (widthColumn) {
                    var arrayWidthColumn = widthColumn.split(',');
                    if (arrayWidthColumn.length == $tableHeaderCol.length) {
                        widthTable = 0;
                        for (var l = 0; l < arrayWidthColumn.length; l++) {
                            if ($tableHeaderCol.eq(l)) {
                                $tableHeaderCol.eq(l).width(arrayWidthColumn[l] + 'px');
                                $tableContentCol.eq(l).width(arrayWidthColumn[l] + 'px');
                                widthTable += parseInt(arrayWidthColumn[l]);
                            }
                        }

                        $tableHeader.width(widthTable + 'px');
                        $tableContent.width(widthTable + 'px');
                    }
                } else {
                    var columnNoWidth = [];
                    var totalWidth = 0;
                    $tableHeaderCol.each(function (idx) {
                        if ($(this).width() == 0 || $(this).inlineStyle('width') == null) {
                            columnNoWidth.push(idx);
                        } else {
                            totalWidth += $(this).width();
                        }
                    });

                    if (columnNoWidth.length > 0) {
                        widthTable = $grid.width();
                        if (settings.isFixHeightContent && !settings.isUsingCustomScroll) {
                            widthTable = widthTable - $.layout.scrollbarWidth();
                        }
                        var allWidth = widthTable - totalWidth;
                        var widthCol = allWidth / columnNoWidth.length;
                        for (var g = 0; g < columnNoWidth.length; g++) {
                            if (g == columnNoWidth.length - 1) {
                                widthCol = widthCol - $.layout.scrollbarWidth();
                            }

                            $tableHeaderCol.eq(columnNoWidth[g]).width(widthCol);
                            $tableContentCol.eq(columnNoWidth[g]).width(widthCol);
                        }
                    } else {
                        widthTable = 0;
                        $tableHeaderCol.each(function (idx, item) {
                            widthTable += $(item).width();
                        });
                    }
                }

                if (settings.isUsingCustomScroll) {
                    $gridContent.niceScroll({ autohidemode: settings.isAutoHideScroll });
                }

                columns.each(function (k, column) {
                    $(column).unbind('mousedown')
                        .mousedown(function (e) {
                            if ($(e.target).css('cursor') == 'w-resize') {//'col-resize'
                                e.preventDefault();
                                $('body').addClass('unselectable');
                                isResizing = true;
                                element = e.target;
                                index = $(element).index();
                                clientX = e.clientX;
                                width = $(element).outerWidth();
                                widthTable = $tableContent.width();

                                var layer = $('<div id="resize-column-layer" style="position:fixed; z-index:10000; top:0; left:0; opacity: 0; filter: alpha(opacity=0); background-color: #000;cursor: w-resize !important;border-right:1px solid gray"></div>')
                                    .css({ height: $(document).height(), width: $(document).width(), display: 'block' })
                                    .mousemove(function (ev) {
                                        if (isResizing) {
                                            var difference = clientX - ev.clientX;
                                            if ((width - difference) > 10) {
                                                var newWidth = width - difference;

                                                $tableHeaderCol.eq(index).width(newWidth);
                                                $tableContentCol.eq(index).width(newWidth);

                                                $tableHeader.width(widthTable - difference);
                                                $gridHeaderWrap.width(widthTable - difference);

                                                $tableContent.width(widthTable - difference);
                                                $gridContent.width(widthTable - difference);
                                            }
                                        }

                                        $gridHeaderWrap.css({ "margin-left": -$gridContent.scrollLeft() })

                                    })
                                    .mouseup(function (ev) {
                                        if (isResizing) {
                                            isResizing = false;
                                            element = null;
                                            $('body').css({ cursor: '' });
                                            $('body').removeClass('unselectable');

                                            if (settings.isUseCookie) {
                                                var str = [];
                                                var columnNoWidth1 = [];
                                                var totalWidth1 = 0;
                                                $tableHeaderCol.each(function (idx) {
                                                    if ($(this).width() == 0 || $(this).inlineStyle('width') == null) {
                                                        columnNoWidth1.push(idx);
                                                    } else {
                                                        totalWidth1 += $(this).width();
                                                    }
                                                    str.push($(this).width());
                                                });

                                                if (columnNoWidth1.length > 0) {
                                                    var allWidth1 = $tableContent.width() - totalWidth1;
                                                    var widthCol1 = allWidth1 / columnNoWidth1.length;
                                                    for (var h = 0; h < columnNoWidth1.length; h++) {
                                                        str[columnNoWidth1[h]] = widthCol1;
                                                    }
                                                }

                                                if (_hasSupportLocalStorage()) {
                                                    window.localStorage.setItem(uniqueKey, str.join(','));
                                                }
                                                else {
                                                    $.cookie(uniqueKey, str.join(','), { expires: 7, path: "/", secure: true });
                                                }
                                            }

                                            if (settings.onresizefinish && $.isFunction(settings.onresizefinish)) {
                                                settings.onresizefinish();
                                            }

                                            if (settings.isUsingCustomScroll) {
                                                $gridContent.getNiceScroll().resize();
                                            }

                                            $gridHeaderWrap.css({ "margin-left": -$gridContent.scrollLeft() });

                                            $(ev.target).css({ cursor: 'w-resize', "border-right": "" });
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
                    $(column).unbind('mousemove')
                        .mousemove(function (e) {
                            if ($(e.target).attr('class') != 'grid-header-wrap' && $(e.target).attr('class') == 'header') {
                                var bounds = $(e.target).bounds();
                                $gridHeaderWrap.css({ "margin-left": -$gridContent.scrollLeft() });
                                if (Math.abs((bounds.left + bounds.width + 10) - (e.clientX)) <= 10) {
                                    $(e.target).css({ cursor: 'w-resize' });
                                }
                                else {
                                    $(e.target).css({ cursor: '' });
                                }
                            }
                        });
                });

            } else {
                $gridContent.css({ 'overflow-x': 'visible' });
                $gridHeaderWrap.css('width', $grid.width() - 1).children('table').css('width', $grid.width() - 1);
                $gridContent.css('width', $grid.width() - 2).children('table').css('width', $grid.width() - 2);
            }
        });
    };
})(window.jQuery);

(function ($) {
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
})(window.jQuery);

(function ($) {
    $.fn['inlineStyle'] = function (prop) {
        var styles = $(this).attr("style");
        var value;
        if (styles == null || styles == undefined)
            return null;

        var stylies = styles.split(";");
        for (var i = 0; i < stylies.length; i++) {
            var style = stylies[i].split(":");
            if ($.trim(style[0]) === prop) {
                value = style[1];
            }
        }

        //styles && styles.split(";").forEach(function (e) {
        //	var style = e.split(":");
        //	if ($.trim(style[0]) === prop) {
        //		value = style[1];
        //	}
        //});
        return value;
    };
})(window.jQuery);

(function ($) {
    $.fn.tableSelect = function (options) {
        var defaultOptions = {
            clickedClass: "selected",
            clickCallback: null,
            contextCallback: null,
            tableCallback: null
        };

        defaultOptions = $.extend(defaultOptions, options);

        var table = $(this),
            lastSelected,
            originalRow,
            currentRow;

        if (table[0].tagName !== 'TABLE') {
            return false;
        }
        var tableRows = $(table).find('tr');

        //Xóa toàn bộ các hàng được chọn
        var tableRemoveAllSelected = function () {
            $.each(tableRows, function () {
                $(this).removeClass(defaultOptions.clickedClass);
            });
        };

        $.each(tableRows, function () {
            var _this = $(this);
            _this.children('td').attr('class', '');
            _this.children('td').attr('unselectable', 'on');

            //Bắt sự kiên click chuột trái
            _this.on('click', function (ev) {
                var that = $(this);
                //phím shift
                if (ev.shiftKey) {
                    var last = tableRows.index(lastSelected);
                    var first = tableRows.index(this);
                    var start = Math.min(first, last);
                    var end = Math.max(first, last) + 1;
                    tableRemoveAllSelected();

                    for (var i = start; i < end; i++) {
                        if (!$(tableRows[i]).hasClass(defaultOptions.clickedClass)) {
                            $(tableRows[i]).addClass(defaultOptions.clickedClass);
                        }
                    }

                    originalRow = lastSelected;
                    //  currentRow = this;
                }
                    //phím ctrl
                else if (ev.ctrlKey) {
                    if (this.className.search(defaultOptions.clickedClass) > -1) {
                        that.removeClass(defaultOptions.clickedClass);
                    } else {
                        if (!that.hasClass(defaultOptions.clickedClass)) {
                            that.addClass(defaultOptions.clickedClass);
                        }
                    }

                    lastSelected = this;
                    //  currentRow = this;
                    originalRow = this;
                }
                else {
                    tableRemoveAllSelected();
                    if (!that.hasClass(defaultOptions.clickedClass)) {
                        that.addClass(defaultOptions.clickedClass);
                        lastSelected = this;
                        originalRow = this;
                        // currentRow = this;
                    }
                }

                if (typeof defaultOptions.clickCallback === 'function') {
                    defaultOptions.clickCallback();
                }
            });

            //Bắt sự kiên click chuột phải
            if (!_this.hasClass(defaultOptions.clickedClass)) {
                //_this.on('contextmenu', function (ev) {
                //    if (lastSelected === currentRow) {
                //        tableRemoveAllSelected();
                //    }
                //    _this.addClass(defaultOptions.clickedClass);
                //    lastSelected = this;
                //    originalRow = this;
                //    currentRow = this;
                //    if (typeof defaultOptions.contextCallback === 'function') {
                //        defaultOptions.contextCallback();
                //    }
                //});
            }

            if (defaultOptions.tableCallback && typeof defaultOptions.tableCallback === 'function') {
                defaultOptions.tableCallback();
            }
        });
    };

    $.fn.openLink = function (options) {
        var defaultOptions = {
            event: 'dblclick',
            urlOpen: ''
        }
        , table = $(this);

        defaultOptions = $.extend(defaultOptions, options);

        if (table[0].tagName !== 'TABLE'
            || ((defaultOptions.urlOpen == null || defaultOptions.urlOpen == '')
            || (defaultOptions.event == null || defaultOptions.event == ''))) {
            return false;
        }

        var tableRows = $(table).find('tr');

        $.each(tableRows, function () {
            var _this = $(this);
            _this.on(defaultOptions.event, function (ev) {
                var that = $(this);
                var id = that.attr("id") || that.attr("data-id");
                urlOpen = getUrl(defaultOptions.urlOpen);

                if (id && urlOpen != null && urlOpen != '') {
                    document.location.href = urlOpen + '/' + id;
                }
            });
        });

        if (!String.prototype.endsWith) {
            String.prototype.endsWith = function (searchString, position) {
                var subjectString = this.toString();
                if (typeof position !== 'number' || !isFinite(position) || Math.floor(position) !== position || position > subjectString.length) {
                    position = subjectString.length;
                }
                position -= searchString.length;
                var lastIndex = subjectString.indexOf(searchString, position);
                return lastIndex !== -1 && lastIndex === position;
            };
        }

        var getUrl = function (inUrl) {
            if (inUrl == '' || inUrl == null)
                return null;

            if (inUrl.endsWith("/")) {
                inUrl = inUrl.substring(0, inUrl.lastIndexOf("/"));
            }
            return inUrl;
        }
    };
})(jQuery);