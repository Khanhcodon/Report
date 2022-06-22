
define([egov.template.search.document,
        egov.template.search.documentResult],
function (SearchFormTemplate, SearchItemTemplate) {

    var DocumentSearch = Backbone.View.extend({
        el: '#document-search',
        events: {
            'click #btnSearch': "_search",
            //'click .search-item': "_openDocument",
            'click .search-item': "_openDetail",
            'click #btnBackSearch': "_hideSearch",
            'click #search-show-hide-advance': '_searchShowHideAdvance',
        },

        currentPage: 1,
        totalPage: 1,
        //taida hõ trợ ẩn hiện tìm kiếm nâng cao
        isShowingAdvanceSearch: false,
        advanceSearchHeight: 86,


        initialize: function () {
            this.render();
        },

        /**
         * mở ra page hiển thị chi tiết của document từ mh tìm kiếm
         * phân biệt 2 mh iphone và ipad
         * @param {any} e
         */
        _openDetail: function (e) {
            egov.mobile.showDocumentDetail(e.currentTarget.getAttribute('data-id'));
        },

        /**
         * Tìm kiếm mặc định chỉ tìm bằng 'trích yếu'
         * function hỗ trợ ẩn hiện 'số ký hiệu' và 'số đến'
         * @param {any} e
         */
        _searchShowHideAdvance: function (e) {
            this.isShowingAdvanceSearch = !this.isShowingAdvanceSearch;
            if (this.isShowingAdvanceSearch) {
                this.$('#search-input-skh').show();
                this.$('#search-input-sd').show();
            } else {
                this.$('#search-input-skh').hide();
                this.$('#search-input-sd').hide();
            }
            this.$('#search-show-hide-advance').children().html(this.isShowingAdvanceSearch ? 'keyboard_arrow_up' : 'keyboard_arrow_down');

            this.$("#searchResult").height(egov.screenSize.contentH - 202 - 48 + (this.isShowingAdvanceSearch ? 0 : this.advanceSearchHeight));
        },

        render: function () {
            this.$el.html(SearchFormTemplate);
            this._layout();
            egov.mobile.upgradeMaterial(".mdl-textfield, .mdl-button");
        },

        _layout: function () {
            this.$("#searchResult").height(egov.screenSize.contentH - 202 - 48 + this.advanceSearchHeight/*taida support less/more search*/);
        },

        _openDocument: function (e) {
            var target = $(e.target).closest('.search-item');
            var documentCopyId = target.attr('data-id');

            egov.pubsub.publish('documents.open', documentCopyId);
        },

        _search: function () {
            if (!this._isValid()) {
                return;
            }

            if (this.currentPage > this.totalPage) {
                return;
            }

            var that = this;
            var data = this._serialize();

            $.ajax({
                url: '/Search/SearchAdvance',
                data: data,
                beforeSend: function () {
                    egov.mobile.showStatus("Đang tìm kiếm...");
                },
                success: function (result) {
                    that.totalPage = result.TotalPage;
                    that._showResult(result);
                },
                complete: function () {
                    egov.mobile.hideStatus();
                }
            });
        },

        _hideSearch: function () {
            egov.mobile.hideSearchPage();
        },

        _serialize: function () {
            return {
                Compendium: this.$("#compendium").val(),
                DocCode: this.$("#doccode").val(),
                InOutCode: this.$("#inoutcode").val(),
                CategoryId: "Tất cả",
                UrgentId: "Tất cả",
                CategoryBusinessId: "Tất cả",
                Page: this.currentPage++,  // Tăng để lấy trang tiếp theo khi cuộn xuống
                IsMainProcess: false,
                IsUseCached: false
            };
        },

        _isValid: function () {
            var keyword = this.$("#compendium").val() + this.$("#doccode").val() + this.$("#inoutcode").val();
            return !String.isNUllOrWhiteSpace(keyword);
        },

        _showResult: function (result) {
            this.$(".total-result").text(String.format('{0} kết quả', result.TotalResult));
            var docs = result.Items;
            _.each(docs, function (d) {
                d.Status = docStatus[d.ExtendInfo.Status];
                d.InOutCode = d.InOutCode || "";
            });

            this.$('#searchResult ul').append($.tmpl(SearchItemTemplate, result.Items));

            this._loadMoreHandler();
        },

        _loadMoreHandler: function () {
            return this.totalPage > 1 && this.$("#searchResult").scrollLoadMore('next', this._search.bind(this));
        }
    });

    var docStatus = {
        1: "Đang dự thảo",
        2: "Đang xử lý",
        4: "Đã kết thúc",
        8: "Đã hủy",
        16: "Đang dừng xử lý"
    };

    return DocumentSearch;
});