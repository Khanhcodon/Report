
define([egov.template.search.bmail,
        egov.template.bmail.listItem],
function (SearchFormTemplate, SearchItemTemplate) {

    var DocumentSearch = Backbone.View.extend({
        el: '#mail-search',
        events: {
            'click #btnSearch': "_doSearch",
            'click #searchResult li': "_openMail",
            'click #btnBackSearch': "_hideSearch"
        },

        currentPage: 1,
        totalPage: 1,
        hasMore: false,

        initialize: function () {
            this.render();
        },

        render: function () {
            this.$el.html(SearchFormTemplate);
            this._layout();
            this.$('.datetime').datepicker();
            egov.mobile.upgradeMaterial(".mdl-textfield, .mdl-button");
        },

        _layout: function () {
            this.$("#searchResult").height(egov.screenSize.contentH - 202 - 48);
        },

        _openMail: function (e) {
            var target = $(e.target).closest('.mail-list-item');
            var mailId = target.attr('data-id');

            egov.pubsub.publish('bmail.open', { mailId: mailId });
        },

        _doSearch: function () {
            this.$('#searchResult').find('.mdl-list').empty();
            this._search();
        },

        _search: function () {
            var that = this;
            var from = this.$("#from").val();
            var subject = this.$("#subject").val();
            var fromDate = this.$("#fromDate").val();
            var toDate = this.$("#toDate").val();

            fromDate = !fromDate ? '' : new Date(fromDate).format('MM/dd/yy');
            toDate = !toDate ? '' : new Date(toDate).format('MM/dd/yy');

            bmail.request.search(from, subject, fromDate, toDate, (this.currentPage - 1) * 25, 25, function (result) {
                that.currentPage++;
                that._showResult(result.mails, result.more);
            });
        },

        _hideSearch: function () {
            egov.mobile.hideSearchPage();
        },

        _showResult: function (mails, hasMore) {
            if (mails && mails.length === 0) {
                this.$('#searchResult').find('.mdl-list').html("<li class='mdl-list__item'>Không có kết quả.</li>");
                return;
            }
            $.tmpl(SearchItemTemplate, mails).appendTo(this.$('#searchResult'));
            hasMore && this._loadMoreHandler();
        },

        _loadMoreHandler: function () {
            return this.$("#searchResult").scrollLoadMore('next', this._search.bind(this));
        }
    });

    return DocumentSearch;
});