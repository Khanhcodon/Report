(function ($, eGovNotify, helper) {
    "use strict";

    // Tên class có kế thừa từ : Hub tương ứng trên server.
    // var hubs = $.connection.notificationHubs;
    var hubs = $.connection.hubs;

    var frameName = "documents";

    $.connection.hub.logging = false;

    $.connection.hub.start(function () {

    });

    $(window).off('unload').on('unload', function (e) {
        $.connection.hub.disconnected(function () {
            console.log('disconnected!');
        });

        if (helper)
            helper.destroyEvent(e);
    });

    $.connection.hub.reconnected(function (e) {
        try {
            console.log('reconnected!');
        }
        catch (ex) {
            console.log("Reconnected error!");
        }
    });

    $.connection.hub.error(function (error) {
        ///<summary>
        /// Hàm gọi khi hub xảy ra lỗi
        ///</summary>
        if (error) {
            try {
                console.log(error);
               // helper.getContentWindow(frameName).location.reload(true);
            }
            catch (ex) {
                console.log(ex.message);
            }
        }
    });

    //Nhận thông báo từ server
    hubs.client.notification = function (data) {
        ///<summary>
        /// Truyền đối tượng notify từ server xuống cho client
        ///</summary>
        ///<param name = "data">Đối tượng notify trả về cho client</param>
        try {
            if (!egov.isMobile && currentApp && currentApp.name == "mail" && document.hasFocus()) {
                return;
            }

            if (typeof window.pushNotifyFromServer === "function") {
                window.pushNotifyFromServer(data);
            }
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    hubs.client.mailNotification = function (data) {
        ///<summary>
        /// Truyền đối tượng notify từ server xuống cho client
        ///</summary>
        ///<param name = "data">Đối tượng notify trả về cho client</param>

        try {
            window.bmailNotify.addToModel(JSON.parse(data.JsonData));
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    //Cập nhật trạng thái notify của các connections khác của người dung
    hubs.client.updateViewAllNotify = function () {
        ///<summary>
        ///Cập nhật trạng thái xem tất cả văn bản
        ///</summary>
        try {
            window.eGovNotify.setViewAll();
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    //Cập nhật trạng thái notify và các notify của connections khác
    hubs.client.updateViewNotify = function (notifyId) {
        ///<summary>
        ///Cập nhật trạng thái xem của văn bản
        ///</summary>
        try {
            eGovNotify.reBindNotify(notifyId);
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    hubs.client.reloadPage = function () {
        ///<summary>
        /// Reload lại trang
        ///</summary>
        try {
            document.location.reload(true);
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

})(window.jQuery, window.eGovNotify, window.helper);