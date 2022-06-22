(function () {
    "use strict";

    // Danh sách các cookie name
    var cookies = {
        RecentTabs: 'RecentTabs',
        DocumentHeaderWidth: 'DocumentHeaderWidth_',
        PageSize: 'PageSize',
        IgnoreConfirmRelations: "ConfirmRelation",
        viewSize: "ViewSize",
        quickView: "QuickView",
        mudimMethod: "MudimMethod",
        useVietKey: "UseVietKey",
        displayPopupTransferTheoLo: "displayPopupTransferTheoLo",
        lastApp: "lastApp",
    };

    /// <summary>Lớp quản lý tất cả các cookie</summary>
    var Cookie = {

        // Lưu tab đang mở vào cookie
        addRecentTab: function (user, tabModel) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (!recentTab) {
                recentTab = {};
            }
            var recentUserTab = recentTab[user];
            if (recentUserTab === undefined) {
                recentUserTab = [];
            }
            recentUserTab.push(tabModel);
            recentTab[user] = recentUserTab;
            $.cookie(cookies.RecentTabs, JSON.stringify(recentTab), { secure: true });
        },

        // Trả về danh sách tất cả các tab đã lưu vào cookie
        getRecentTab: function (user) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (recentTab) {
                return recentTab[user];
            }
            return [];
        },

        // Xóa một tab khỏi cookie
        deleteRecentTab: function (user, tabModel) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (recentTab) {
                recentTab[user] = _.reject(recentTab[user], function (itm) {
                    return itm.id === tabModel.id;
                });

                $.cookie(cookies.RecentTabs, JSON.stringify(recentTab), { secure: true });
            }
        },

        // Lưu thông tin các cột và độ rộng các cột vào cookie
        addDocumentHeaderWidth: function (functionId, value) {
            $.cookie(cookies.DocumentHeaderWidth + functionId, JSON.stringify(value), { expires: 7, path: "/", secure: true });
        },

        // Lấy ra danh sách các cột và độ rộng đã được lưu cookie
        getDocumentHeaderWidth: function (functionId) {
            return this.getCookie(cookies.DocumentHeaderWidth + functionId);
        },

        // Thêm cookie pagesize
        /// <param name="pageSize" type="int">PageSize</param>
        addPageSize: function (pageSize) {
            if (pageSize && typeof pageSize === 'number') {
                $.cookie(cookies.PageSize, pageSize, { expires: 7, path: "/", secure: true });
            }
        },

        // Trả về cookie pageSize
        getPageSize: function () {
            var value = $.cookie(cookies.PageSize);
            return parseInt(value);
        },

        // private: Lấy ra cookie theo tên
        getCookie: function (name) {
            var value = $.cookie(name);
            return JSON.parse(value);
        },

        // Thiết lập bỏ qua confirm văn bản liên quan khi bàn giao
        setIgnoreConfirmRelation: function (value) {
            $.cookie(cookies.IgnoreConfirmRelations, value, { secure: true });
        },

        // Trả về giá trị thiết lập confirm văn bản liên quan
        getIgnoreConfirmRelation: function () {
            var value = $.cookie(cookies.IgnoreConfirmRelations);
            return value;
        },

        // Kích thước trang chủ
        viewSize: function (value) {
            if (value === undefined) {
                return parseInt($.cookie(cookies.viewSize));
            } else {
                $.cookie(cookies.viewSize, value, { expires: 30, secure: true });
            }
        },

        ///Thiết lập hiển thị tóm tắt văn bản
        setQuickView: function (value) {
            $.cookie(cookies.quickView, value, { secure: true });
        },

        ///Lấy thiết lập hiển thị tóm tắt văn bản
        getQuickView: function () {
            return parseInt($.cookie(cookies.quickView));
        },

        ///Thiết lập ứng dụng chạy sau cùng
        setLastApp: function (value) {
            $.cookie(cookies.lastApp, value, { secure: true });
        },

        ///Lấy thiết lập ứng dụng chạy sau cùng
        getLastApp: function () {
            return $.cookie(cookies.lastApp);
        },

        ///Thiết lập gõ tiếng việt
        setMudimMethod: function (value) {
            $.cookie(cookies.mudimMethod, value, { path: '/', secure: true });
        },

        //Lấy thiết lập gõ tiếng việt
        getMudimMethod: function () {
            return parseInt($.cookie(cookies.mudimMethod));
        },

        ///Lấy thiết lập gõ tiếng việt
        getUseVietKey: function () {
            return parseInt($.cookie(cookies.useVietKey));
        },

        //lấy hoặc thiết lập trạng thái có hiển thị popup cho ý kiến khi chuyển theo lô
        displayPopupTransferTheoLo: function (value) {
            if (value === undefined) {
                var val = $.cookie(cookies.displayPopupTransferTheoLo);
                return val === "true" || val == 1;
            } else {
                $.cookie(cookies.displayPopupTransferTheoLo, value, { path: '/', secure: true });
            }
        },


    };

    window.eGovCookie = Cookie;
})();