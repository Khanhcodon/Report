define([
'libs/jquery/jquery.timepicker/jquery-ui-timepicker-addon',
], function () {
    "use strict";
    ///<summary>Hàm xử lý chức năng gia hạn xử lý</summary>
    ///<param name="hsmc" type="bool">True - là hồ sơ một cửa, False - là văn bản</param>
    var AddTimeView = Backbone.View.extend({
        initialize: function (options) {
            this.frame = options.frame;
            this.docCopyId = options.docCopyId;
            this.open();
        },

        /// <summary> Mở form gia hạn</summary>
        /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
        open: function () {
            var _this = this;
            var _dialog = egov.views.base.dialog;
            var settings = {}; // dialog setting
            settings.width = 830;
            //settings.height = 500;
            settings.title = egov.resources.document.renewals.dialogTitle;
            settings.buttons = [
                {
                    text: egov.resources.common.messageOkBtn,
                    click: function () {
                        var extendedDays = parseInt($('#ExtendedDays').val());
                        if (!isNaN(extendedDays)) {
                            _this.save();
                        } else {
                            egov.message.show(egov.resources.document.addtime.numberonly);
                        }
                    }
                },
                {
                    text: egov.resources.common.messageCancelBtn,
                    click: function () {
                        _dialog.close();
                    }
                }
            ];
            egov.query.indexAddTime(
                { docCopyId: parseInt(_this.docCopyId) },
                 function (result) {
                     _dialog.close();
                     if (result.error) {
                         egov.message.show(result.error);
                     }
                     else {
                         _dialog.open(result, settings);
                     }
                 });
        },

        ///<summary>Hàm xử lý nút đồng ý gia hạn</summary>
        save: function () {
            var dialog = egov.views.base.dialog;
            var addTimes = getAddTimeMode(); // lấy nội dung form gia hạn.
            egov.query.updateDateAppointed(
                addTimes,
                function (data) {
                    if (data.error) {
                        egov.message.notification(data.error, egov.message.messageTypes.error);
                    }
                    else {
                        egov.message.notification(data.success, egov.message.messageTypes.success);
                        dialog.close();
                    }
                }, function (xhr) {
                    egov.message.notification(xhr.statusText, egov.message.messageTypes.error);
                });
        },
    });

    function getAddTimeMode() {
        return $("#dialogAddTime").find(":input,textarea").serializeObject();
    }

    return AddTimeView;
});