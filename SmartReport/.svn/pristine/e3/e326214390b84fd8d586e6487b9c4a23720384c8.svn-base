(function ($, $dialog, _, egov) {
    if (typeof ($.fn.serializeObject) === 'undefined') {
        throw 'Thư viện này sử dụng bkav.utilities.js, hãy tải thư viện bkav.utilities.js trước khi sử dụng';
    }
    if (typeof (_) === 'undefined') {
        throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    }

    // ===================================================================================================================
    /// <summary> Class hỗ trợ trả kết quả</summary>
    /// <param name="docId" type="Guid"> Document Id của hồ sơ tương ứng</param>
    /// <param name="frame" type="String"> Frame đang thao tác</param>
    egov.document.returns = function (docCopyId, frame) {
        this.docCopyId = docCopyId;
        this.frame = frame;
    };

    /// <summary> Mở form trả kết quả</summary>
    egov.document.returns.prototype.open = function () {
        var settings = {}; // dialog setting
        settings.width = 650;
        settings.title = "Trả kết quả";
        $.ajax({
            url: '/Return/Index',
            data: { docCopyId: this.docCopyId },
            beforeSend: function () {
                var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                $dialog.open($loading, settings);
            },
            success: function (result) {
                $dialog.close();
                $dialog.open(result, settings);
            }
        });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ chức năng Yêu cầu bổ sung và tiếp nhận bổ sung</summary>
    egov.document.supplementary = function (docCopyId, frame, actionId, workflowId) {
        this.docCopyId = docCopyId;
        this.actionId = actionId;
        this.workFlowId = workflowId;
        this.frame = frame;
        this.tranferFrame = "supplementaryForm";
        this.transferButtonId = "saveAndTransfer";
    };

    /// <summary>Mở form yêu cầu bổ sung</summary>
    egov.document.supplementary.prototype.openRequire = function () {
        var $this = this;
        var settings = {};
        settings.title = "Yêu cầu bổ sung";
        settings.width = 450;
        settings.height = 250;
        settings.buttons = [
            {
                text: "Lưu",
                click: function () {
                    $this.sendRequire();
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        $.ajax({
            url: '/Supplementary/Index',
            data: { docCopyId: $this.docCopyId },
            type: "GET",
            success: function (data) {
                $dialog.open(data, settings,
                    function () { // open callback
                        egov.cshtml.transfer.supplementary.init();   // bkav.cshtml.transfer.js
                    });
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
                $dialog.close();
            }
        });
    };

    /// <summary> Gửi ý kiến yêu cầu bổ sung.</summary>
    egov.document.supplementary.prototype.sendRequire = function () {
        var $currentTab = egov.cshtml.home.tab.getActiveTab();              // bkav.cshtml.home.js
        var suppModel = egov.cshtml.transfer.supplementary.get();           // bkav.cshtml.transfer.js
        if (suppModel == undefined) {
            return;
        }
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#SupplementarySendRequire").val();
        $.ajax({
            type: "POST",
            url: '/Supplementary/SendRequire',
            dataType: "json",
            traditional: true,
            data: {
                "model": JSON.stringify(suppModel),
                __RequestVerificationToken: token
            },
            success: function (data) {
                if (data.success) {
                    $dialog.close();
                    var frame = document.getElementById(frame).contentWindow;
                    frame.location.reload();
                } else {
                    eGovMessage.show(data.error);
                }
            },
            error: function () {
                eGovMessage.notification("Có lỗi khi gửi yêu cầu bổ sung.", eGovMessage.messageTypes.error);
            }
        });
    };

    /// <summary> Mở form Tiếp nhận kết quả dừng xử lý</summary>
    egov.document.supplementary.prototype.openReceive = function () {
        var $this = this;
        var settings = {};
        settings.width = 700;
        settings.title = "Cập nhật kết quả dừng xử lý";
        settings.buttons = [
            {
                text: "Bàn giao",
                click: function () {
                    $this.updateResultProcess();
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        $.ajax({
            url: '/Supplementary/ReceiveSupplementary',
            data: { docCopyId: $this.docCopyId },
            beforeSend: function () {
                var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                $dialog.open($loading, settings);
            },
            success: function (result) {
                $dialog.open(result, settings);
            },
            error: function () {
                eGovMessage.notification("Có lỗi xảy ra khi lấy dữ liệu!", eGovMessage.messageTypes.error);
                $dialog.close();
            }
        });
    };

    /// <summary>Mở form tiếp nhận bổ sung</summary>
    egov.document.supplementary.prototype.openReceiveSupplement = function () {
        var settings = {};
        settings.width = 700;
        settings.title = "Tiếp nhận bổ sung";
        settings.buttons = [
            {
                text: "Cập nhật",
                click: function () {
                    $this.updateResultProcess();
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        $.ajax({
            url: '/Supplementary/ReceiveSupplementary',
            data: { docCopyId: $this.docCopyId },
            success: function (result) {
                $dialog.open(result, settings,
                    function () {
                        $("#suppComment").focus();
                    });
            },
            error: function () {
                eGovMessage.notification("Có lỗi xảy ra khi lấy dữ liệu!", eGovMessage.messageTypes.error);
                $dialog.close();
            }
        });
    };

    /// <summary>Cập nhật kết quả tiếp nhận bổ sung</summary>
    egov.document.supplementary.prototype.updateResultProcess = function () {
        var data = egov.cshtml.transfer.supplementary.getForReceive();
        $.ajax({
            url: "/Supplementary/Receive",
            data: data,
            success: function (result) {
                if (result.success) {
                    $dialog.close();
                    document.location.reload();
                } else {
                    eGovMessage.show(result.error);
                }
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
            }
        });
    };

    /// <summary> Mở form Tiếp tục xử lý</summary>
    egov.document.supplementary.prototype.openContinueProcess = function () {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 800;
        settings.title = "Xác nhận tiếp tục xử lý";
        settings.buttons = [
            {
                id: "saveAndTransfer",
                text: "Bàn giao",
                click: function () {
                    $this.continueProcess();
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        $.ajax({
            url: '/Supplementary/ContinueProcess',
            data: { docCopyId: $this.docCopyId },
            beforeSend: function () {
                var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                $dialog.open($loading, settings);
            },
            success: function (result) {
                $dialog.open(result, settings);
            },
            error: function () {
                eGovMessage.notification("Có lỗi xảy ra khi lấy dữ liệu!", eGovMessage.messageTypes.error);
                $dialog.close();
            }
        });
    };

    /// <summary> Tiếp tục xử lý và bàn giao</summary>
    egov.document.supplementary.prototype.continueProcess = function () {
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#SupplementaryConfirmContinueProcess").val();

        var updateModel = window.getUpdateModel();
        if (updateModel == null) {
            return;
        }
        updateModel.__RequestVerificationToken = token;
        updateModel.docCopyId = $this.docCopyId;
        $.ajax({
            url: '/Supplementary/ConfirmContinueProcess',
            typ: "POST",
            data: updateModel,
            success: function (result) {
                if (result.success) {
                    var transferObj = new egov.document.transfer($this.docCopyId, $this.frame, $this.token); //$this.docId
                    transferObj.showActions('#' + $this.transferButtonId);
                }
                else {
                    eGovMessage.show(result.error);
                }
            },
            error: function () {
                eGovMessage.notification("Có lỗi xảy ra khi tiếp tục xử lý, vui lòng thử lại!", eGovMessage.messageTypes.error);
            }
        });
    };
})(window.jQuery, new dialogAdapter(), window._, window.egov = window.egov || {})