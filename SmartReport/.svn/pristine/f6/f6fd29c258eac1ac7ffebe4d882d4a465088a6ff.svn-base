
define([], function () {

    var eGovPaging = Backbone.View.extend({
        tagName: "nav",
        template: '<ul class="pagination pagination-sm"></ul>',
        pageTemplate: '<li><a href="#" page="{0}" class="page">{0}</a></li>',

        disabledClass: "disabled",
        activeClass: "active",
        events: {
            'click a.page': '_selectPage',
            'click a.next': '_nextPage',
            'click a.previour': '_prevPage',
            'click .btnGotoPage': '_gotoInput'
        },

        option: {
            pageSizeOption: [50, 100, 150, 200],
            pageSize: 50,
            total: 0,
            totalPage: 0,
            currentPage: 1,
            displayPageCount: 4, // Hiển thị 5 trang gần trang hiện tại
            showPrevious: true,
            showNext: true,
            shopChangePageSize: true,
            showGoInput: true,
            autoHidePrevious: true,
            autoHideNext: true,
            showNavigator: true,
            showPageNumber: true,
            onload: null,
            select: null
        },

        initialize: function (option) {
            this.$el.attr("aria-label", "Page navigation");

            this.option = $.extend({}, this.option, option);

            // Todo: xử lý trường hợp bằng vừa đúng thì mới
            this.option.totalPage = Math.ceil(this.option.total / this.option.pageSize);

            if (!this.option.showPageNumber) {
                this.option.showGoInput = false;
            }

            this.render();
        },

        render: function () {
            this.$el.html(this.template);
            this.$pager = this.$(".pagination");

            // Không hiển thị khi chỉ có 1 trang hoặc không có trang nào
            if (this.option.totalPage === 0 || this.option.totalPage === 1) {
                return this;
            }

            // Trang trước
            if (this.option.showPrevious) {
                this.$pager.append('<li><a href="#" aria-label="Previous" class="previour"><span aria-hidden="true">&laquo;</span></a></li>');
            }

            if (this.option.showPageNumber) {
                // các trang đang xem
                this._renderPages(this.option.currentPage);
            }

            // Trang tiếp theo
            if (this.option.showNext) {
                this.$pager.append('<li><a href="#" aria-label="Next" class="next"><span aria-hidden="true">&raquo;</span></a></li>');
            }

            // Goto page
            if (this.option.showGoInput) {
                var gotoInput = this._bindGotoInput();
                this.$el.append(gotoInput);
            }

            this._activeCurrent();

            // Onloaded
            if (this.option.onload && typeof this.option.onload === "function") {
                this.option.onload(this);
            }
        },

        show: function () {
            this.$el.show();
        },

        hide: function () {
            this.$el.hide();
        },

        getSelectedPage: function () {
            return this.option.currentPage;
        },

        nextPage: function () {
            this._nextPage();
        },

        previourPage: function () {
            this._prevPage();
        },

        _renderPages: function (currentPage) {
            var startPage, toPage;
            
            if (this.option.totalPage <= this.option.displayPageCount) {
                startPage = 1; toPage = this.option.totalPage;

                for (var i = startPage; i <= toPage ; i++) {
                    this.$pager.append(String.format(this.pageTemplate, i));
                }

                return;
            }

            var haftPageCount = Math.floor(this.option.displayPageCount / 2);
            startPage = currentPage - haftPageCount;
            if (startPage <= 0) {
                startPage = 1;
            }

            toPage = startPage + this.option.displayPageCount;
            // Trường hợp chọn vào các trang gần cuối => toPage > totalPage => Tính lại
            if (toPage > this.option.totalPage) {
                toPage = this.option.totalPage;
                startPage = toPage - this.option.displayPageCount;
            }

            var firstPage = 1;
            // Hiển thị trang 1
            if (startPage > firstPage) {
                this.$pager.append(String.format(this.pageTemplate, firstPage));

                if (startPage > haftPageCount + firstPage) {
                    // Khi startPage > 1 nhiều thì mới hiển thị ...
                    this.$pager.append($(String.format(this.pageTemplate, '...')).addClass(this.disabledClass));
                } else {
                    // Hiển thị luôn nếu gần 1 ví dụ: 1  2  3  4  5  6  7
                    for (var i = firstPage + 1; i < startPage; i++) {
                        this.$pager.append(String.format(this.pageTemplate, i));
                    }
                }
            }

            for (var i = startPage; i <= toPage ; i++) {
                this.$pager.append(String.format(this.pageTemplate, i));
            }

            if (toPage < this.option.totalPage) {
                // Hiển thị trang cuối nếu toPage nhỏ hơn totalPage nhiều
                if (this.option.totalPage > haftPageCount + toPage) {
                    this.$pager.append($(String.format(this.pageTemplate, '...')).addClass(this.disabledClass));
                } else {
                    for (var i = toPage + 1; i < this.option.totalPage; i++) {
                        this.$pager.append(String.format(this.pageTemplate, i));
                    }
                }
                this.$pager.append(String.format(this.pageTemplate, this.option.totalPage));
            }
        },

        _nextPage: function () {
            var page = this.option.currentPage + 1;
            this._gotoPage(page);
        },

        _prevPage: function () {
            var page = this.option.currentPage - 1;
            this._gotoPage(page);
        },

        _selectPage: function (e) {
            var target = $(e.target).closest("a");
            if (target.parent().is(".disabled")) {
                return;
            }

            var page = target.attr("page");
            this._gotoPage(page);
        },

        _gotoInput: function (e) {
            var page = this.$(".gotopage").val();
            this._gotoPage(page);
        },

        _gotoPage: function (page) {
            page = parseInt(page);
            if (page > this.option.totalPage) {
                page = this.option.totalPage;
            }

            this.option.currentPage = page;
            this.render();
            this._callback();
        },

        _activeCurrent: function () {
            var currentPage = this.option.currentPage;
            var element = this.$('a[page="' + currentPage + '"]');
            if (element.length === 0) {
                return;
            }

            var parentLi = element.parent();
            if (parentLi) {
                parentLi.siblings().removeClass(this.activeClass);
                parentLi.addClass(this.activeClass);
            }

            if (this.option.showGoInput) {
                this.$(".gotopage").val(currentPage);
            }
        },

        _bindGotoInput: function () {
            var result = $("<ul class='pagination pagination-sm paging-input' style='margin-left:15px;'>");
            result.append("<li style='border: none'><a>Xem trang: <input type='text' class='gotopage' style='width: 20px;border: none;padding: 0;'/></a></li>");
            result.append("<li style='border: none'><a><span href='#' class='btnGotoPage'>xem</span></a></li>");

            return result;
        },

        _callback: function () {
            if (this.option.select && typeof this.option.select === 'function') {
                this.option.select(this.option.currentPage);
            }
        }
    });

    return eGovPaging;
});