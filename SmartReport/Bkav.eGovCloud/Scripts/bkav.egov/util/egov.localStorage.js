(function () {
    "use strict";

    // Danh sách các key trong localStorage
    var localStorageKey = {
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

    /// <summary>Lớp quản lý tất cả các localStorage</summary>
    var Storage = {

        // Lưu tab đang mở vào localStorage
        addRecentTab: function (user, tabModel) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (!recentTab) {
                recentTab = {};
            }

            var recentUserTab = recentTab[user];
            if (recentUserTab === undefined) {
                recentUserTab = [];
            }

            recentUserTab.push(tabModel);
            recentTab[user] = recentUserTab;

            egov.locache.setDefault(localStorageKey.RecentTabs, JSON.stringify(recentTab));
        },

        // Trả về danh sách tất cả các tab đã lưu vào localStorage
        getRecentTab: function (user) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (recentTab) {
                return recentTab[user];
            }

            return [];
        },

        // Xóa một tab khỏi localStorage
        deleteRecentTab: function (user, tabModel) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (recentTab) {
                recentTab[user] = _.reject(recentTab[user], function (itm) {
                    return itm.id === tabModel.id;
                });

                egov.locache.setDefault(localStorageKey.RecentTabs, JSON.stringify(recentTab));
            }
        },

        // Lưu thông tin các cột và độ rộng các cột vào localStorage
        addDocumentHeaderWidth: function (functionId, value) {
            egov.locache.setDefault(localStorageKey.DocumentHeaderWidth + functionId, JSON.stringify(value));
        },

        // Lấy ra danh sách các cột và độ rộng đã được lưu localStorage
        getDocumentHeaderWidth: function (functionId) {
            return this.getLocalStorage(localStorageKey.DocumentHeaderWidth + functionId);
        },

        // Thêm localStorage pagesize
        /// <param name="pageSize" type="int">PageSize</param>
        addPageSize: function (pageSize) {
            if (pageSize && typeof pageSize === 'number') {
                egov.locache.setDefault(localStorageKey.PageSize, pageSize);
            }
        },

        // Trả về localStorage pageSize
        getPageSize: function () {
            var value = egov.locache.getDefault(localStorageKey.PageSize);
            return parseInt(value);
        },

        // private: Lấy ra localStorage theo tên
        getLocalStorage: function (name) {
            var value = egov.locache.getDefault(name);
            return JSON.parse(value);
        },

        // Thiết lập bỏ qua confirm văn bản liên quan khi bàn giao
        setIgnoreConfirmRelation: function (value) {
            egov.locache.setDefault(localStorageKey.IgnoreConfirmRelations, value);
        },

        // Trả về giá trị thiết lập confirm văn bản liên quan
        getIgnoreConfirmRelation: function () {
            var value = egov.locache.getDefault(localStorageKey.IgnoreConfirmRelations);
            return value;
        },

        // Kích thước trang chủ
        viewSize: function (value) {
            if (value === undefined) {
                return parseInt(egov.locache.getDefault(localStorageKey.viewSize));
            } else {
                egov.locache.setDefault(localStorageKey.viewSize, value);
            }
        },

        ///Thiết lập hiển thị tóm tắt văn bản
        setQuickView: function (value) {
            egov.locache.setDefault(localStorageKey.quickView, value);
        },

        ///Lấy thiết lập hiển thị tóm tắt văn bản
        getQuickView: function () {
            var value = parseInt(egov.locache.getDefault(localStorageKey.quickView));
            return value;
        },

        ///Thiết lập ứng dụng chạy sau cùng
        setLastApp: function (value) {
            egov.locache.setDefault(localStorageKey.lastApp, value);
        },

        ///Lấy thiết lập ứng dụng chạy sau cùng
        getLastApp: function () {
            return egov.locache.getDefault(localStorageKey.lastApp);
        },

        ///Thiết lập gõ tiếng việt
        setMudimMethod: function (value) {
            egov.locache.setDefault(localStorageKey.mudimMethod, value);
        },

        //Lấy thiết lập gõ tiếng việt
        getMudimMethod: function () {
            return parseInt(egov.locache.getDefault(localStorageKey.mudimMethod));
        },

        ///Lấy thiết lập gõ tiếng việt
        getUseVietKey: function () {
            return parseInt(egov.locache.getDefault(localStorageKey.useVietKey));
        },

        //lấy hoặc thiết lập trạng thái có hiển thị popup cho ý kiến khi chuyển theo lô
        displayPopupTransferTheoLo: function (value) {
            if (value === undefined) {
                var val = egov.locache.getDefault(localStorageKey.displayPopupTransferTheoLo);
                return val === "true" || val == 1;
            } else {
                egov.locache.setDefault(localStorageKey.displayPopupTransferTheoLo, value);
            }
        },
    };

    window.eGovLocalStorage = Storage;
})();