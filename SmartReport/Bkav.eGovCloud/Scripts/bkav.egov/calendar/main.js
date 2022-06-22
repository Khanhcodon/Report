
/// <summary>Khai báo namespace chung cho toàn hệ thống</summary>
(function (egov) {
    "use strict";
    // Phiên bản dùng nội bộ công ty
    // Todo: cần chuyển config này trả về từ server
    egov.isPrivateVersion = false;

    // Danh sách các view
    egov.views = {
        // Danh sách các view dùng chung: tree, contextmenu, toolbar, dialog, message
        base: {},
    };

    //Lưu trữ các option của tree hiện tại
    egov.currentTree = {
        document: {}
    };

    // Danh sách các thiết lập cá nhân, thiết lập hệ thống ứng với user hiện tại.
    egov.setting = {};

    // Helper
    egov.helper = {
        loading: '<img class="loading" src="../Content/bkav.egov/images/ajax-loader.gif" alt="loading"/>',

        destroyClickEvent: function (e) {
            if (e) {
                if (e.preventDefault) {
                    e.preventDefault();
                }
                else {
                    // fix cho ie 8
                    e.returnValue = false;
                }
                e.stopPropagation();
            }
        },

        hideAllContext: function () {
            /// <summary>An cac contextmenu</summary>
            if (egov.views.home && egov.views.home.documents) {
                egov.views.home.documents.hideAllContextmenu();
            }
        }
    };

    // Danh sách các thư viện bổ trợ: ultilities, grid,...
    egov.utils = egov.utils || {};

    egov.isMobile = false;

    window.Aloha = window.Aloha || {};
})(window.egov = window.egov || {})

require(["../config"], function () {
        require(
            ['jquery', 'jqueryBrowser', 'jqueryUI', 'layout', 'hashBase64', 'underscore','egovEvents', 'egovPubSub'],
            function ($) {
                init();
            });
});

function init() {
    require(['calendarDeptUser', 'datetimeFunction','serverCalendar'], function () {


    })
}