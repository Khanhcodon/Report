(function ($) {
    "use strict";

    if (typeof $.connection === undefined)
        throw "Chưa có signalR. Vui lòng thêm signalR vào project.";

    var hubs = $.connection.notificationHubs;

    $.connection.hub.logging = false;

    $.connection.hub.start(function () {

    });

    $(window).off('unload').on('unload', function (e) {
        $.connection.hub.disconnected(function () {
            console.log('disconnected!');
        });
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
        //  notify.eGovNotify(data);
        try {
            if (egov.mobile.notification && egov.mobile.notification.eGovNotify) {
                egov.mobile.notification.eGovNotify.addToModel(data);
            }
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
        if (egov.mobile.notification) {
            egov.mobile.notification.clearNotifies();
        }
    }

    //Cập nhật trạng thái notify và các notify của connections khác
    hubs.client.updateViewNotify = function (notifyId) {
        ///<summary>
        ///Cập nhật trạng thái xem của văn bản
        ///</summary>
        try {
            //if (egov.mobile.notification) {
            //    egov.mobile.notification.clear(notifyId);
            //}
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    hubs.client.reloadPage = function () {
        ///<summary>
        /// Reload lại trang
        ///</summary>
    }

    hubs.client.transferSync = function (docCopyId) {
        /// <summary>
        /// Xóa văn bản trên danh sách
        /// </summary>
        /// <param name="docCopyId"></param>
        try {
            egov.mobile.removeItem(docCopyId);
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

})(window.jQuery);