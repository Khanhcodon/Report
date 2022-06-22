
(function () {
    requirejs.config({
        // Tienbv: thay đổi chút về cách đánh phiên bản
        //     - file main.js luôn luôn tải về bản mới nhất.
        //     - các file khác phụ thuộc vào phiên bản được đánh này, khi sửa file js thì đánh lại phiên bản cho nó là dc. Như thế ko phải lúc nào f5 cũng load về cái mới nhất
        // Todos: về lâu dài 456sẽ tính cho từng file, file nào thay đổi thì đánh version lại cho file đó thôi
        urlArgs: new Date().getTime()
    });

    /// <summary>Khai báo namespace chung cho toàn hệ thống</summary>
    (function (egov, bmail) {
        "use strict";

        // Danh sách các view
        egov.views = {
            // Danh sách các view dùng chung: tree, contextmenu, toolbar, dialog, message
            base: {},
        };

        // Danh sách các view
        bmail = {
            views: {},
            folders: []
        };

        //Lưu trữ các option của tree hiện tại
        egov.currentTree = {
            document: {}
        };

        // Danh sách các thiết lập cá nhân, thiết lập hệ thống ứng với user hiện tại.
        egov.setting = {};

        // Danh sách các thư viện bổ trợ: ultilities, grid,...
        egov.utils = egov.utils || {};

        egov.callback = function (func, params) {
            if (typeof func === 'function')
                func(params);
        }

        egov.isMobile = true;
        navigator.isMobile = true;
        navigator.isTablet = window.innerWidth > 1024;

    })(window.egov = window.egov || {}, window.bmail = window.bmail || {})

    require(["config"], function () {
        require(
            ['vendors'],
            function () {
                initMobile();
            });
    });

    function initMobile() {
        egov.require = require;

        //Lớp quản lý các biến dùng chung
        egov.common = {};

        bmail.authenCookie = $.cookie("bkavAuthen");
        bmail.userCookie = $.cookie("bkavUsername");

        // Trước tiên cần tải về danh sách các thiết lập cho user hiện tại.
        egov.request.getCommonConfigs({
            success: function (result) {
                egov.setting = result;
                egov.setting.noavatar = "../AvatarProfile/noavatar2.jpg";

                // test
                //egov.setting.noavatar = "https://danhba.bkav.com/avatars/TienBV.bmp";

                // Set domain
                // document.domain = egov.setting.parentDomain;

                egov.userId = egov.setting.userId;
                egov.usernameEmailDomain = $.cookie("bkavUsername");

                egov.setting.acceptFileTypes = new RegExp(result.acceptFileTypes, 'i');

                showUserProfile(egov.setting.user);

                require(["main"]);

                $(document.body).bindResources();
            }
        });

        registerApisToGlobal();
    }

    function showUserProfile(userSettings) {
        $(".useroption .avatar").attr("src", userSettings.avatar);
        $(".useroption .userinfo > div:first-child").text(userSettings.fullName);
        $(".useroption .fullmail").text(userSettings.userDomainName);
        $(".useroption .fullmail").dotdotdot();

        $('#menu-username').html(userSettings.fullName);
        $('#menu-email').html(userSettings.userDomainName);
    }

    function registerApisToGlobal() {
        /*
         * Đăng ký các api ra global.
         * 
         */

        /*
         * Xử lý khi nhấn Back trên thiết bị android
         */
        window.onBackPressed = function () {
            require(['main'], function (Jsmobile) {
                Jsmobile.$("#btnbacktolist").trigger("click");
                Jsmobile.autoHideMainMemu(false);
                Jsmobile._hideSearchClick();
            });
        }
    }

})();