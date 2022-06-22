define([
'jquery',
'backbone'
], function ($, Backbone) {
    "use strict";

    /// <summary> Class hỗ trợ chức năng kết thúc xử lý</summary>
    /// <param name="docCopyId" type="String">Id văn bản copy</param>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    var EndProcessView = Backbone.View.extend({
        initialize: function (options) {
            this.frame = options.frame;
            this.docCopyId = options.docCopyId;
            this.isHsmc = options.isHsmc;
            this.open();
        },

        open: function () {
            var _this = this;
            var _dialog = egov.views.base.dialog;
            if (_this.isHsmc) {
                egov.message.show(
                    'Bạn có đồng ý kết thúc hồ sơ này không?',
                    null,
                    egov.message.messageButtons.YesNo,
                    function () {
                        var settings = {}; // dialog setting
                        settings.width = 600;
                        //settings.height = 500;
                        settings.title = "Kết thúc xử lý";
                        settings.buttons = [// danh sách các nút chức năng trên form xin ý kiến
                            {
                                text: "Gửi",
                                click: function () {
                                    //TODO: Hàm này không tìm thấy đâu
                                    _this.save();
                                }
                            },
                            {
                                text: "Đóng",
                                click: function () {
                                    _dialog.close();
                                }
                            }
                        ];
                        $.ajax({
                            url: '/Finish/Index',
                            data: { docCopyId: _this.docCopyId },
                            beforeSend: function () {
                                var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                                _dialog.open($loading, settings);
                            },
                            success: function (result) {
                                _dialog.close();
                                _dialog.open(result, settings);
                            }
                        });
                    }
                );
            } else {
                egov.message.show(
                    'Bạn có đồng ý kết thúc văn bản này không?',
                    null,
                    egov.message.messageButtons.YesNo,
                    function () {
                        $.get('/StorePrivate/GetStoreActive', {},
                            function (result) {
                                if (result) {
                                    if (result.storePrivate.length > 0 || result.storeShare.length > 0) {
                                        ////todo:xem lại class StorePrivateView
                                        //var storePrivate = new StorePrivateView;
                                        //storePrivate.openDialogSave(result, function (selectedId) {
                                        //    updateFinish(_this.docCopyId, selectedId);
                                        //}, function () {
                                        //    updateFinish(_this.docCopyId);
                                        //});

                                    } else {
                                        updateFinish(_this.docCopyId, null, _this.frame);
                                    }
                                }
                            }
                        )
                        .fail(function () {
                            egov.message.notification('Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân', egov.message.messageTypes.error);
                        });
                    }
                );
            }
        },
    });

    var updateFinish = function (documentCopyId, storePrivateId, frame) {
        var token = $("input[name='__RequestVerificationToken']", "#FinishUpdateFinish").val();
        var _dialog = egov.views.base.dialog;
        var data = { documentCopyId: documentCopyId, __RequestVerificationToken: token };
        if (storePrivateId) {
            data.storePrivateId = storePrivateId;
        }
        $.post('/Finish/UpdateFinish', data, function (result) {
            if (result) {
                if (result.error) {
                    egov.message.notification(result.message, egov.message.messageTypes.error);
                } else {
                    egov.message.notification('Kết thúc xử lý thành công', egov.message.messageTypes.success);
                    _dialog.close();
                    if (frame) {
                        //todo:xem lại hàm đong âtb
                        egov.cshtml.home.tab.closeActiveTab();
                    }
                    var documentCopyIds = [documentCopyId];
                    if (egov.currentDocumentObject && typeof egov.currentDocumentObject !== 'undefined') {
                        egov.currentDocumentObject.clientRemove(documentCopyIds);
                    }
                }
            }
        });
    };

    return EndProcessView;
});