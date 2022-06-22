define(function () {
    "use strict";

    var PrintView = Backbone.View.extend({
        editFieldClass: "field-edit",
        editButtonId: "editPrint",
        printButtonId: "printContent",
        saveButtonId: "savePrint",

        initialize: function (options) {
            this.docCopyId = options.docCopyId;
            this.docId = options.docId;
            this.paperAddIds = options.paperAddIds ? options.paperAddIds : "";
            this.feeAddIds = options.feeAddIds ? options.feeAddIds : "";
            this.suppId = options.suppId || 0;
            this.displayPreview = options.displayPreview;
            this.printId = options.printId ? options.printId : 0;
            this.commonTemplate = options.commonTemplate ? options.commonTemplate : 0;

            if (this.displayPreview) {
                this.open();
                return;
            }
            this._print();
        },

        events: {
            "click #quickPrint": "_quickPrint",
            "click #printContent": "_printOnClient",
            "click .btn-close": "_close"
        },

        _quickPrint: function () {
            var printerId,
                copies,
                landscape;

            printerId = parseInt(this.$(".printerId").val());
            copies = this.$(".copies").val();
            landscape = this.$(".landscape").val();
            if (egov.setting.userSetting.PrinterId !== printerId) {
                egov.setting.userSetting.PrinterId = printerId;
                egov.request.setUserConfig({ data: { PrinterId: printerId } });
            }
            this._print(printerId, copies, landscape);
        },

        _close: function () {
            this.$el.dialog("destroy");
        },

        _print: function (printerId, copies, landscape) {
            debugger
            var that = this;
            egov.request.quickPrint({
                data: {
                    docCopyIds: JSON.stringify(that.docCopyId),
                    templateId: that.printId ? that.printId : 0,
                    printerId: printerId ? printerId : 0,
                    copies: copies ? copies : 1,
                    landscape: landscape == false ? landscape : true,
                    commonTemplate: that.commonTemplate
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.print.error + result.message);
                        return;
                    }
                    egov.pubsub.publish(egov.events.status.success, egov.resources.document.print.success + result.printerName);
                },
                error: function (err) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.print.error);
                }
            });
        },

        _getPrinters: function (callback) {
            /// <summary>
            /// Lấy danh sách máy in trên server
            /// </summary>
            debugger
            var that = this;
            egov.request.getPrinters({
                success: function (result) {
                    egov.printers = result;
                    egov.callback(callback);
                }
            });
        },

        getPrints: function (success) {
            /// <summary>
            /// Trả về danh sách các phiếu in có thể xuất ở thời điểm hiện tại
            /// </summary>
            var that = this;
            if (that.docCopyId == null) {
                return [];
            }
            var contextItems = {};
            egov.request.getPrints({
                data: { docCopyId: that.docCopyId },
                success: function (result) {
                    var prints = JSON.parse(result.success);
                    var max = prints.length;
                    for (var i = 0; i < max; i++) {
                        contextItems[prints[i].TemplateId] = {
                            name: prints[i].Name,
                            icon: 'print'
                        };
                    }

                    egov.callback(success, contextItems);
                }
            });
        },

        /// <summary> Mở form in</summary>
        open: function () {
            var that,
                dialog,
                settings,
                success,
                data,
                $loading,
                $leftPanel,
                $rightPanel;
            that = this;
            dialog = egov.views.base.dialog;

            settings = {
                width: '95%',
                height: 550,
                title: "eGov Printer",
                draggable: true,
            };

            that.$el.addClass("printDialog")
            egov.log(JSON.stringify(that.docCopyId));
            data = { id: that.printId, docCopyIds: JSON.stringify(that.docCopyId), suppId: that.suppId };
            // $loading = $('<img src="../../Content/Images/ajax-loader.gif" />');
            $leftPanel = $("<div class='col-md-3 printLeftPanel' style='display:none'></div>").appendTo(that.$el);
            $rightPanel = $("<div class='col-md-16 printRightPanel' style='padding-right:0;'></div>").appendTo(that.$el); //.html($loading)

            success = function () {
                that.$el.dialog(settings);
                require([egov.template.document.printLeftPanel], function (printLeftPanelTemp) {
                    $leftPanel.html($.tmpl(printLeftPanelTemp, {
                        printers: egov.printers
                    }));

                    var url = "/Print/PreviewPrint?docCopyIds=" + (JSON.stringify(that.docCopyId)) + "&templateId=" + (that.printId ? that.printId : 0) + "&suppId=" + that.suppId;
                    $rightPanel.html('<embed id="pdfviewer" width="100%" height="510px" name="plugin" src="' + url + '" type="application/pdf" internalinstanceid="224">');
                    that.$el.dialog(settings);
                });
            };

            //data = {
            //    id: that.printId,
            //    docCopyIds: JSON.stringify(that.docCopyId),
            //    suppId: that.suppId,
            //    paperAddIds: that.paperAddIds,
            //    feeAddIds: that.feeAddIds
            //};

            if (!egov.printers) {
                that._getPrinters(function () {
                    success();
                });
            } else {
                success();
            }
        },

        /// <summary> Mở form in dạng cho phép chỉnh sửa nội dung trước khi in</summary>
        edit: function () {
            var that = this;
            var editFields = $('.' + that.editFieldClass);
            if (editFields.length === 0) {
                egov.message.show("Mẫu phiếu in này không cho phép sửa nội dung nào.");
                return;
            }
            editFields.each(function () {
                var val = $(this).text();
                var id = $(this).attr("id");
                var onchange = $(this).attr("onkeyup");
                $(this).replaceWith("<input type='text' id = '" + id + "' class='" + that.editFieldClass + "' value='" + val + "' onkeyup='" + onchange + "'/>");
            });
            $("#" + that.editButtonId).hide();
            $("#" + that.printButtonId).hide();
            $("#" + that.saveButtonId).show();
        },

        /// <summary> Lưu lại giá trị của các key đã dc cập nhật</summary>
        save: function (printId) {
            var that = this;
            var editFields = $('.' + that.editFieldClass);
            var keys = {};
            var max = editFields.length;
            for (var i = 0; i < max; i++) {
                var key = editFields[i];
                keys[$(key).attr("id")] = $(key).val();
            }
            $.ajax({
                url: '/Print/SaveChange',
                data: { docId: that.docId, changes: JSON.stringify(keys) },
                success: function () {
                    that.open(printId);
                },
                error: function (xhr) {
                    egov.message.notification(xhr.statusText, egov.message.messageTypes.error);
                }
            });
        },

        /// <summary> In</summary>
        _printOnClient: function () {
            $(".pdfviewer").jqprint();
        },

        /// <summary> Mở form in</summary>
        openPrintEmbryonicForm: function (embryonicFormId) {
            var dialog = egov.views.base.dialog;
            var that = this;
            var settings = { // dialog setting
                width: 350,
                height: 150,
                title: "eGov Printer",
                buttons: [
                {
                    text: "In",
                    click: function () {
                        that.printEmbryonicForm(that.docId, embryonicFormId);
                    }
                },
                {
                    text: "Đóng",
                    click: function () {
                        dialog.close();
                    }
                }
                ]
            };

            $.ajax({
                url: '/Print/GetPrinters',
                beforeSend: function () {
                    var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                    dialog.open($loading, settings);
                },
                success: function (result) {
                    dialog.close();
                    dialog.open(result, settings);
                }
            });
        },

        /// <summary> In</summary>
        printEmbryonicForm: function (docID, embryonicFormId) {
            var dialog = egov.views.base.dialog;
            var printerName = $('#printer').val();
            if (printerName == null) {
                window.parent.egov.message.show("Bạn chưa chọn máy in!", null, null, null, null);
            }
            else {
                $.ajax({
                    url: '/Print/PrintReport',
                    data: { embryonicId: embryonicFormId, documentId: docID, printerName: printerName },
                    success: function (result) {
                        egov.message.show(result.message, null, null, null, null);
                        if (result.success) {
                            dialog.close();
                        }
                    }
                });
            }
        },

        /// <summary> Xem trước</summary>
        preViewEmbryonicForm: function (docID, embryonicFormId) {
            window.open('/Print/PreView?id=' + embryonicFormId + '&&docId=' + docID, 'mywindow', 'fullscreen=yes, scrollbars=auto');
        },
    });

    return PrintView;
});