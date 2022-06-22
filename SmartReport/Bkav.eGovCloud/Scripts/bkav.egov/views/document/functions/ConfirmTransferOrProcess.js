define([
'jquery',
'backbone',

], function ($, Backbone) {

    "use strict";

    var ConfirmTransferOrProcessView = Backbone.View.extend({
        initialize: function (options) {
            this.frame = options.frame;
            this.docCopyId = options.docCopyId;
            this.isTransfer = options.isTransfer;

            this.open();
        },

        open: function () {
            var _this = this;
            if (!_this.isTransfer) {
                var settings = {}; // dialog setting
                var _dialog = egov.views.base.dialog;
                settings.width = 450;
                settings.title = "Đồng ý xác nhận xử lý cho hồ sơ này?";
                settings.buttons = [// danh sách các nút chức năng trên form xin ý kiến
                        {
                            text: "Đồng ý",
                            click: function () {
                                _this.save();
                            }
                        },
                        {
                            text: "Bỏ qua",
                            click: function () {
                                _dialog.close();
                            }
                        }
                ];

                egov.request.getUsersProcess({
                    data: { documentCopyId: _this.docCopyId },
                    success: function (result) {
                        if (result.error) {
                            egov.message.show(result.error);
                            return;
                        }
                        if (result.data) {
                            // TODO: Xu ly mo form thong bao co scroll giong xac nhan chuyen van ban lien quan.
                            var strHtml = "<span>Danh sách User đã tham gia xử lý.</span></br>";
                            for (var i = 0; i < result.data.length; i++) {
                                var users = _.sortBy(result.data, function (num) { return num.IsViewed; });
                                var daXem = users[i].IsViewed == true ? 'Đã xem' : 'Chưa xem';
                                strHtml += "<div>" + users[i].FullName + "(" + users[i].Username + ")" + " - " + daXem + "</div>";
                            }
                            _dialog.open(strHtml, settings);
                        }
                    }
                });
            } else {
                egov.message.show(
                    'Bạn có đồng ý xác nhận bàn giao cho hồ sơ này không?',
                    null,
                    egov.message.messageButtons.YesNo,
                    function () {
                        _this.save();
                    }
                );
            }
        },

        save: function () {
            var url, msg, tokenId;
            var _this = this;
            if (_this.isTransfer) {
                url = 'confirmTransfer';
                msg = 'Xác nhận bàn giao bị lỗi.';
            }
            else {
                url = 'confirmTransfer';
                msg = 'Xác nhận xử lý bị lỗi.';
            }

            egov.request[url]({
                data: { documentCopyId: _this.docCopyId },
                success: function (data) {
                    if (data.error) {
                        egov.message.notification(data.error, egov.message.messageTypes.error);
                    } else {
                        egov.message.notification(data.success, egov.message.messageTypes.success);
                        egov.message.notification(data.success, egov.message.messageTypes.success);
                        egov.views.base.dialog.close();
                        //Todo:xem lại hàm reload
                        //  egov.cshtml.home.reloadData();
                    }
                },
                error: function () {
                    egov.message.notification(msg, egov.message.messageTypes.error);
                }
            });
        },
    });

    return ConfirmTransferOrProcessView;
});