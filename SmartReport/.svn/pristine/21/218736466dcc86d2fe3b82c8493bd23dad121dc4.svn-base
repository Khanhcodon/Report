define([], function () {
    "use strict";

    /// <summary> Class hỗ trợ ký duyệt</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    var ApproverView = Backbone.View.extend({
        initialize: function (options) {
            this.frame = options.frame;
            this.docCopyIds = options.docCopyIds;
            this.isSuccess = options.isSuccess;
            this.open();
        },

        /// <summary>Mở dialog lưu ý kiến ký duyệt</summary>
        /// <param name="isSuccess" type="Boolean">set True nếu là đồng ý, false nếu là từ chối</param>
        open: function (isSuccess) {
            var _this = this;
            var _dialog = egov.views.base.dialog;
            var settings = {}; // dialog setting
            settings.width = 350;
            settings.height = 120;
            settings.title = "Ký duyệt";
            settings.buttons = [
                {
                    text: "Đồng ý",
                    click: function () {
                        _this.send(true);
                        _dialog.close();
                    }
                },
                {
                    text: "Không đồng ý",
                    click: function () {
                        _this.send(false);
                        _dialog.close();
                    }
                },
                {
                    text: "Đóng",
                    click: function () {
                        _dialog.close();
                    }
                }
            ];
            _dialog.open("<div><b>Bạn muốn ký duyệt văn bản này?.</b></div>", settings);
        },

        /// <summary> Lưu ý kiến ký duyệt</summary>
        /// <param name="isSuccess" type="Boolean">set True nếu là đồng ý, false nếu là từ chối</param>
        send: function (isSuccess) {
            var _this = this;
            egov.request.approverSend({
                data: {
                    docCopyIds: JSON.stringify(_this.docCopyIds),
                    isSuccess: isSuccess
                },
                success: function (result) {
                    if (result.success) {
                        egov.pubsub.publish(egov.events.status.success, result.success);
                        //Todo: Sửa lại nghiệp vụ chổ này hiển thị form bàn giao theo hướng chuyển mặc định sau khi làm xong chức năng cấu hình hướng chuyển mặc định
                        if (_this.frame && typeof _this.frame !== 'undefined') {
                            document.getElementById(_this.frame).contentWindow.location.reload();
                        }
                    } else {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                }
            });
        },
    });

    return ApproverView;
});