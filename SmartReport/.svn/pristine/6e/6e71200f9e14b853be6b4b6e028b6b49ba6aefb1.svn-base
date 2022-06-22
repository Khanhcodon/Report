(function ($, $dialog, _, egov) {
    if (typeof ($.fn.serializeObject) === 'undefined') {
        throw 'Thư viện này sử dụng bkav.utilities.js, hãy tải thư viện bkav.utilities.js trước khi sử dụng';
    }
    if (typeof (_) === 'undefined') {
        throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    }

    var itemsDg = {
        jobtitlesItem: "jobtitlesItem",//chức vụ
        jobtitlesDeptItem: "jobtitlesDeptItem",//phòng ban - chức vụ
        allUsers: "allUsers", //tất cả người dùng
        deptItem: "deptItem",//phong ban
        userItem: 'userItem', // Cán bộ
    };

    var detailRetrieve = function (documentCopyId, dateCreated) {
        $.ajax({
            type: "GET",
            url: '/Document/GetUsersForUndoTransfering',
            data: { documentCopyId: parseInt(documentCopyId), dateCreated: dateCreated },
            success: function (result) {
                $('#viewDetailRetrieve').html('');
                if (result.error) {
                    eGovMessage.show(result.error);
                    return;
                }
                if (result.data) {
                    // TODO: Xu ly mo form thong bao co scroll giong xac nhan chuyen van ban lien quan.
                    var strHtml = "";
                    var users = _.sortBy(result.data, function (num) { return num.IsViewed; });
                    for (var i = 0; i < result.data.length; i++) {
                        var daXem = users[i].IsViewed == true ? 'Đã xem' : 'Chưa xem';
                        var stt = i + 1;
                        strHtml += "<div>" + stt + '. ' + users[i].FullName + "(" + users[i].Username + ")" + " - " + daXem + "</div>";
                    }
                    $('#viewDetailRetrieve').html(strHtml);
                }
            }
        });
    };

    var checkOtherProgramOpenFile = function (plugin, allFileOpened) {
        var regex = /(.doc|.docx|.xls|.xlsx|.ppt|.pptx)$/i;
        for (var i = 0; i < allFileOpened.length; i++) {
            if (regex.test(allFileOpened[i].name) && plugin.readFileBase64(allFileOpened[i].name, 0, -1) == '') {
                eGovMessage.show('Bản phải đóng các chương trình đang mở tệp đính kèm trước khi lưu');
                return false;
            }
        }
        return true;
    };

    var confirmSaveAttachment = function (array, modifiedFiles, plugin) {
        if (!modifiedFiles) {
            modifiedFiles = {};
        }
        if (array.length === 0) {
            $(document).trigger('checkModifySuccess', modifiedFiles);
        } else {
            for (var i = 0; i < array.length; i++) {
                if (plugin.isChangeContent(array[i].name)) {
                    var fileName = array[i].value;
                    var newArray = array.splice(i + 1, array.length - (i + 1));
                    var content = plugin.readFileBase64(array[i].name, 0, -1);
                    eGovMessage.show('Tệp ' + fileName + ' có sự thay đổi<br/>Bạn có muốn lưu lại không?', null, eGovMessage.messageButtons.YesNo,
                                function () {
                                    var j = i;
                                    modifiedFiles[array[j].id] = content;
                                    confirmSaveAttachment(newArray, modifiedFiles, plugin);
                                },
                                function () {
                                    confirmSaveAttachment(newArray, modifiedFiles, plugin);
                                });
                    break;
                } else {
                    if (i == array.length - 1) {
                        $(document).trigger('checkModifySuccess', modifiedFiles);
                    }
                }
            }
        }
    };

    egov.document = { isLoadedDgView: false, isLoadedFindRelation: false };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ ký duyệt</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.approver = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    /// <summary> Lưu ý kiến ký duyệt</summary>
    /// <param name="isSuccess" type="Boolean">set True nếu là đồng ý, false nếu là từ chối</param>
    egov.document.approver.prototype.send = function (isSuccess) {
        var $this = this;
        var token = $("input[name='__RequestVerificationToken']", '#ApproverSend').val();
        $.ajax({
            url: "/Approver/Send",
            type: "POST",
            data: {
                docCopyId: $this.docCopyId,
                isSuccess: isSuccess,
                __RequestVerificationToken: token
            },
            success: function (result) {
                if (result.success) {
                    //Todo: Sửa lại nghiệp vụ chổ này hiển thị form bàn giao theo hướng chuyển mặc định sau khi làm xong chức năng cấu hình hướng chuyển mặc định
                    $dialog.close();
                    document.getElementById($this.frame).contentWindow.location.reload();
                } else {
                    eGovMessage.show(result.error);
                }
            }
        });
    };

    //#region HSMC

    //// ===================================================================================================================
    ///// <summary> Class hỗ trợ trả kết quả</summary>
    ///// <param name="docId" type="Guid"> Document Id của hồ sơ tương ứng</param>
    ///// <param name="frame" type="String"> Frame đang thao tác</param>
    //egov.document.returns = function (docCopyId, frame, token) {
    //    this.docCopyId = docCopyId;
    //    this.frame = frame;
    //    this.token= token;
    //};

    ///// <summary> Mở form trả kết quả</summary>
    //egov.document.returns.prototype.open = function () {
    //    var settings = {}; // dialog setting
    //    settings.width = 650;
    //    settings.title = "Trả kết quả";
    //    $.ajax({
    //        url: 'Return/Index',
    //        data: { docCopyId: this.docCopyId },
    //        beforeSend: function () {
    //            var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
    //            $dialog.open($loading, settings);
    //        },
    //        success: function (result) {
    //            $dialog.close();
    //            $dialog.open(result, settings);
    //        }
    //    });
    //};

    //// ===================================================================================================================
    ///// <summary> Class hỗ trợ chức năng Yêu cầu bổ sung và tiếp nhận bổ sung</summary>
    //egov.document.supplementary = function (docCopyId, frame, actionId, workflowId, token) {
    //    this.docCopyId = docCopyId;
    //    this.actionId = actionId;
    //    this.workFlowId = workflowId;
    //    this.frame = frame;
    //    this.tranferFrame = "supplementaryForm";
    //    this.transferButtonId = "saveAndTransfer";
    //    this.token= token;
    //};

    ///// <summary>Mở form yêu cầu bổ sung</summary>
    //egov.document.supplementary.prototype.openRequire = function () {
    //    var $this = this;
    //    var settings = {};
    //    settings.title = "Yêu cầu bổ sung";
    //    settings.width = 450;
    //    settings.height = 250;
    //    settings.buttons = [
    //        {
    //            text: "Lưu",
    //            click: function () {
    //                $this.sendRequire();
    //            }
    //        },
    //        {
    //            text: "Đóng",
    //            click: function () {
    //                $dialog.close();
    //            }
    //        }
    //    ];
    //    $.ajax({
    //        url: 'Supplementary/Index',
    //        data: { docCopyId: $this.docCopyId },
    //        type: "GET",
    //        success: function (data) {
    //            $dialog.open(data, settings,
    //                function () { // open callback
    //                    egov.cshtml.transfer.supplementary.init();   // bkav.cshtml.transfer.js
    //                });
    //        },
    //        error: function (xhr) {
    //            eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
    //            $dialog.close();
    //        }
    //    });
    //};

    ///// <summary> Gửi ý kiến yêu cầu bổ sung.</summary>
    //egov.document.supplementary.prototype.sendRequire = function () {
    //    var $currentTab = egov.cshtml.home.tab.getActiveTab();              // bkav.cshtml.home.js
    //    var suppModel = egov.cshtml.transfer.supplementary.get();           // bkav.cshtml.transfer.js
    //    if (suppModel == undefined) {
    //        return;
    //    }
    //    var frameName = this.frame;
    //    $.ajax({
    //        type: "POST",
    //        url: '/Supplementary/SendRequire',
    //        dataType: "json",
    //        traditional: true,
    //        data: {
    //            "model": JSON.stringify(suppModel),
    //            __RequestVerificationToken: $this.token
    //        },
    //        success: function (data) {
    //            if (data.success) {
    //                $dialog.close();
    //                var frame = document.getElementById(frameName).contentWindow;
    //                frame.location.reload();
    //            } else {
    //                eGovMessage.show(data.error);
    //            }
    //        },
    //        error: function () {
    //            eGovMessage.notification("Có lỗi khi gửi yêu cầu bổ sung.", eGovMessage.messageTypes.error);
    //        }
    //    });
    //};

    ///// <summary> Mở form Tiếp nhận kết quả dừng xử lý</summary>
    //egov.document.supplementary.prototype.openReceive = function () {
    //    var $this = this;
    //    var settings = {};
    //    settings.width = 700;
    //    settings.title = "Cập nhật kết quả dừng xử lý";
    //    settings.buttons = [
    //        {
    //            text: "Bàn giao",
    //            click: function () {
    //                $this.updateResultProcess();
    //            }
    //        },
    //        {
    //            text: "Đóng",
    //            click: function () {
    //                $dialog.close();
    //            }
    //        }
    //    ];
    //    $.ajax({
    //        url: 'Supplementary/ReceiveSupplementary',
    //        data: { docCopyId: $this.docCopyId },
    //        beforeSend: function () {
    //            var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
    //            $dialog.open($loading, settings);
    //        },
    //        success: function (result) {
    //            $dialog.open(result, settings);
    //        },
    //        error: function () {
    //            eGovMessage.notification("Có lỗi xảy ra khi lấy dữ liệu!", eGovMessage.messageTypes.error);
    //            $dialog.close();
    //        }
    //    });
    //};

    ///// <summary>Mở form tiếp nhận bổ sung</summary>
    //egov.document.supplementary.prototype.openReceiveSupplement = function () {
    //    var $this = this;
    //    var settings = {};
    //    settings.width = 700;
    //    settings.title = "Tiếp nhận bổ sung";
    //    settings.buttons = [
    //        {
    //            text: "Cập nhật",
    //            click: function () {
    //                $this.updateResultProcess();
    //            }
    //        },
    //        {
    //            text: "Đóng",
    //            click: function () {
    //                $dialog.close();
    //            }
    //        }
    //    ];
    //    $.ajax({
    //        url: 'Supplementary/ReceiveSupplementary',
    //        data: { docCopyId: $this.docCopyId },
    //        success: function (result) {
    //            $dialog.open(result, settings,
    //                function () {
    //                    $("#suppComment").focus();
    //                });
    //        },
    //        error: function () {
    //            eGovMessage.notification("Có lỗi xảy ra khi lấy dữ liệu!", eGovMessage.messageTypes.error);
    //            $dialog.close();
    //        }
    //    });
    //};

    ///// <summary>Cập nhật kết quả tiếp nhận bổ sung</summary>
    //egov.document.supplementary.prototype.updateResultProcess = function () {
    //    var data = egov.cshtml.transfer.supplementary.getForReceive();
    //    $.ajax({
    //        url: "Supplementary/Receive",
    //        data: data,
    //        success: function (result) {
    //            if (result.success) {
    //                $dialog.close();
    //                document.location.reload();
    //            } else {
    //                eGovMessage.show(result.error);
    //            }
    //        },
    //        error: function (xhr) {
    //            eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
    //        }
    //    });
    //};

    ///// <summary> Mở form Tiếp tục xử lý</summary>
    //egov.document.supplementary.prototype.openContinueProcess = function () {
    //    var $this = this;
    //    var settings = {}; // dialog setting
    //    settings.width = 800;
    //    settings.title = "Xác nhận tiếp tục xử lý";
    //    settings.buttons = [
    //        {
    //            id: "saveAndTransfer",
    //            text: "Bàn giao",
    //            click: function () {
    //                $this.continueProcess();
    //            }
    //        },
    //        {
    //            text: "Đóng",
    //            click: function () {
    //                $dialog.close();
    //            }
    //        }
    //    ];
    //    $.ajax({
    //        url: 'Supplementary/ContinueProcess',
    //        data: { docCopyId: $this.docCopyId },
    //        beforeSend: function () {
    //            var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
    //            $dialog.open($loading, settings);
    //        },
    //        success: function (result) {
    //            $dialog.open(result, settings);
    //        },
    //        error: function () {
    //            eGovMessage.notification("Có lỗi xảy ra khi lấy dữ liệu!", eGovMessage.messageTypes.error);
    //            $dialog.close();
    //        }
    //    });
    //};

    ///// <summary> Tiếp tục xử lý và bàn giao</summary>
    //egov.document.supplementary.prototype.continueProcess = function () {
    //    var $this = this;
    //    var updateModel = window.getUpdateModel();
    //    if (updateModel == null) {
    //        return;
    //    }
    //    updateModel.docCopyId = this.docCopyId;
    //    $.ajax({
    //        url: 'Supplementary/ConfirmContinueProcess',
    //        typ: "POST",
    //        data: updateModel,
    //        success: function (result) {
    //            if (result.success) {
    //                var transferObj = new egov.document.transfer($this.docCopyId, $this.frame, $this.token); //$this.docId
    //                transferObj.showActions('#' + $this.transferButtonId);
    //            }
    //            else {
    //                eGovMessage.show(result.error);
    //            }
    //        },
    //        error: function () {
    //            eGovMessage.notification("Có lỗi xảy ra khi tiếp tục xử lý, vui lòng thử lại!", eGovMessage.messageTypes.error);
    //        }
    //    });
    //};

    //#endregion

    // ===================================================================================================================
    /// <summary> Class hỗ trợ in</summary>
    egov.document.egovprint = function (docId, docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
        this.docId = docId;
        this.editFieldClass = "field-edit";
        this.editButtonId = "editPrint";
        this.printButtonId = "printContent";
        this.saveButtonId = "savePrint";
    };

    /// <summary> Trả về danh sách các phiếu in có thể xuất ở thời điểm hiện tại dạng context items (dùng cho context menu) </summary>
    egov.document.egovprint.prototype.getPrints = function () {
        if (this.docCopyId == null) {
            return [];
        }
        var contextItems = {};

        $.ajax({
            url: '/Print/GetPrints',
            data: { docCopyId: this.docCopyId },
            type: 'GET',
            async: false,
            success: function (result) {
                var prints = JSON.parse(result.success);
                var max;
                for (var i = 0; max = prints.length, i < max; i++) {
                    contextItems[prints[i].TemplateId] = {
                        name: prints[i].Name,
                        icon: 'print'
                    };
                }
            }
        });
        return contextItems;
    };

    /// <summary> Mở form in</summary>
    egov.document.egovprint.prototype.open = function ($printId) {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 1000;
        settings.height = 600;
        settings.title = "eGov Printer";
        settings.buttons = [
            {
                id: $this.editButtonId,
                text: "Sửa",
                click: function () {
                    $this.edit($printId);
                }
            },
            {
                id: $this.saveButtonId,
                text: "Lưu lại",
                style: "display: none",
                click: function () {
                    $this.save($printId);
                }
            },
            {
                id: this.printButtonId,
                text: "In",
                click: function () {
                    $this.print();
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
            url: '/Print/Index',
            data: { id: $printId, docCopyIds: JSON.stringify(this.docCopyIds) },
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

    /// <summary> Mở form in dạng cho phép chỉnh sửa nội dung trước khi in</summary>
    egov.document.egovprint.prototype.edit = function () {
        var $this = this;
        var editFields = $('.' + this.editFieldClass);
        if (editFields.length === 0) {
            eGovMessage.show("Mẫu phiếu in này không cho phép sửa nội dung nào.");
            return;
        }
        editFields.each(function () {
            var val = $(this).text();
            var id = $(this).attr("id");
            var onchange = $(this).attr("onkeyup");
            $(this).replaceWith("<input type='text' id = '" + id + "' class='" + $this.editFieldClass + "' value='" + val + "' onkeyup='" + onchange + "'/>");
        });
        $("#" + $this.editButtonId).hide();
        $("#" + $this.printButtonId).hide();
        $("#" + $this.saveButtonId).show();
    };

    /// <summary> Lưu lại giá trị của các key đã dc cập nhật</summary>
    egov.document.egovprint.prototype.save = function ($printId) {
        var $this = this;
        var editFields = $('.' + this.editFieldClass);
        var keys = {};
        var max;
        for (var i = 0; max = editFields.length, i < max; i++) {
            var key = editFields[i];
            keys[$(key).attr("id")] = $(key).val();
        }
        $.ajax({
            url: '/Print/SaveChange',
            data: { docId: $this.docId, changes: JSON.stringify(keys) },
            success: function () {
                $this.open($printId);
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
            }
        });
    };

    /// <summary> In</summary>
    egov.document.egovprint.prototype.print = function () {
        $("#print").jqprint();
    };

    /// <summary> Mở form in</summary>
    egov.document.egovprint.prototype.openPrintEmbryonicForm = function (embryonicFormId) {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 350;
        settings.height = 150;
        settings.title = "eGov Printer";
        settings.buttons = [
                     {
                         text: "In",
                         click: function () {
                             // $this.preViewEmbryonicForm($this.docId, embryonicFormId);
                             $this.printEmbryonicForm($this.docId, embryonicFormId);
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
            url: '/Print/GetPrinters',
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

    /// <summary> In</summary>
    egov.document.egovprint.prototype.printEmbryonicForm = function (docID, embryonicFormId) {
        var printerName = $('#printer').val();
        if (printerName == null) {
            window.parent.eGovMessage.show("Bạn chưa chọn máy in!", null, null, null, null);
        }
        else {
            $.ajax({
                url: '/Print/PrintReport',
                data: { embryonicId: embryonicFormId, documentId: docID, printerName: printerName },
                success: function (result) {
                    window.parent.eGovMessage.show(result.message, null, null, null, null);
                    if (result.success) {
                        $dialog.close();
                    }
                }
            });
        }
    };

    /// <summary> Xem trước</summary>
    egov.document.egovprint.prototype.preViewEmbryonicForm = function (docID, embryonicFormId) {
        window.open('/Print/PreView?id=' + embryonicFormId + '&&docId=' + docID, 'mywindow', 'fullscreen=yes, scrollbars=auto');
    };

    //--------------------

    egov.document.transferplan = function (doctypeId, documentCopyId, frame) {
        this.doctypeId = doctypeId;
        this.documentCopyId = documentCopyId || 0;
        this.isCreatingDocument = this.documentCopyId <= 0;
        this.destination = null;
        this.frame = frame;
    };

    egov.document.transferplan.prototype.open = function () {
        var self = this;
        var actions = [];
        var actionsPlan = [];
        var settings = {};
        settings.width = 810;
        settings.title = "eGovCloud - Dự kiến bàn giao hồ sơ, văn bản";
        settings.buttons = [ // danh sách các nút chức năng trên form bàn giao
                {
                    text: "Đồng ý",
                    click: function () {
                        if ($('#dropdownAction').val() !== '' && $("#dropdownUserTransfer").val() !== '' && $("#dropdownActionPlan").val() !== '') {
                            var destination = egov.cshtml.transfer.getDestination();
                            if (destination) {
                                var action = _.find(actions, function (item) { return item.id === $('#dropdownAction').val(); });
                                var frame = document.getElementById(self.frame).contentWindow;
                                frame.egov.cshtml.document.newtransferplan = {
                                    transferTo: {
                                        actionid: action.id,
                                        workflowid: action.workflowid,
                                        nextnodeid: action.nextnodeid,
                                        currentnodeid: action.currentnodeid,
                                        userid: parseInt($("#dropdownUserTransfer").val()),
                                        isAllowSign: action.isAllowSign
                                    },
                                    transferPlan: destination
                                };
                                settings.close();
                            }
                        } else {
                            eGovMessage.show("Bạn phải chọn hướng chuyển dự kiến");
                        }
                    }
                },
                {
                    text: "Bỏ qua",
                    click: function () {
                        settings.close();
                    }
                }
        ];

        var $transfer = $('<div></div>').append($('#transferPlanTemplate').tmpl()).appendTo($('body'));
        $('#dialogTransferPlan #divTransfer').append($('#transferTemplate').tmpl());
        $dialog.openexist($transfer, settings, function () {
            egov.cshtml.transfer.initLayout();
            $('.dialog-center').css({ border: 'none' });
            $('.dialog-south').css({ border: 'none' });
            $('.dialog-center-south').css({ border: 'none' });
            $('#dropdownAction').mousedown(function (e) {
                e.preventDefault();
                var data = self.isCreatingDocument ? { documentTypeId: self.doctypeId, isPhanloai: false } : { documentCopyId: self.documentCopyId };
                var url = self.isCreatingDocument ? "/Transfer/GetActionsCreate" : "/Transfer/GetActionsEdit";
                $.get(url, data, function (result) {
                    if (result) {
                        if (result.error) {
                            eGovMessage.notification(result.error, eGovMessage.messageTypes.error);
                        } else {
                            actions = _.filter(result, function (item) { return !item.isspecial; });
                            if (actions.length > 0) {
                                $.each(actions, function (i, item) {
                                    $('#dropdownAction').append('<option value="' + item.id + '">' + item.name + '</option>');
                                });
                                $("#dropdownAction").unbind('mousedown');
                                $("#dropdownAction").change(function () {
                                    var id = $(this).val();
                                    if (id) {
                                        var action = _.find(actions, function (item) { return item.id === id; });
                                        if (action) {
                                            $.get('/Transfer/GetUserByAction',
                                                {
                                                    actionId: $(this).val(),
                                                    workflowId: action.workflowid,
                                                    documentCopyId: self.documentCopyId
                                                },
                                                function (resultUser) {
                                                    if (resultUser) {
                                                        $("#dropdownUserTransfer").html($("#dropdownUserTransfer option:first"));
                                                        $.each(resultUser, function (i, item) {
                                                            $("#dropdownUserTransfer").append('<option value="' + item.value + '">' + item.label + '</option>');
                                                        });
                                                    } else {
                                                        eGovMessage.show('Bạn không có quyền xử lý văn bản!');
                                                    }
                                                }
                                            );
                                        }
                                    } else {
                                        $("#dropdownUserTransfer").html($("#dropdownUserTransfer option:first"));
                                        $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                                    }
                                });
                                $("#dropdownUserTransfer").change(function () {
                                    var id = $(this).val();
                                    if (id) {
                                        var action = _.find(actions, function (item) { return item.id === $("#dropdownAction").val(); });
                                        if (action) {
                                            $.get('/Transfer/GetActionsTransferPlan',
                                                {
                                                    workflowId: action.workflowid,
                                                    userId: id,
                                                    currentNodeId: action.nextnodeid
                                                },
                                                function (resultActionPlan) {
                                                    if (resultActionPlan) {
                                                        actionsPlan = resultActionPlan;
                                                        $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                                                        $.each(resultActionPlan, function (i, item) {
                                                            $("#dropdownActionPlan").append('<option value="' + item.id + '">' + item.name + '</option>');
                                                        });
                                                    } else {
                                                        eGovMessage.show('Bạn không có quyền xử lý văn bản!');
                                                    }
                                                }
                                             );
                                        }
                                    } else {
                                        $("#dropdownActionPlan").html($("#dropdownActionPlan option:first"));
                                    }
                                });
                                $("#dropdownActionPlan").change(function () {
                                    var id = $(this).val();
                                    if (id) {
                                        var action = _.find(actionsPlan, function (item) { return item.id === id; });
                                        if (action) {
                                            $.get('/Transfer/GetUserByAction', { actionId: id, workflowId: action.workflowid, documentCopyId: 0, userId: $("#dropdownUserTransfer").val() }, function (resultUser) {
                                                if (resultUser) {
                                                    egov.cshtml.transfer.databind(resultUser, action.nextnodeid, action.workflowid, action.currentnodeid, id);
                                                    $('.dialog-center').css({ border: 'none' });
                                                    $('.dialog-south').css({ border: 'none' });
                                                    $('.dialog-center-south').css({ border: 'none' });
                                                } else {
                                                    eGovMessage.show('Bạn không có quyền xử lý văn bản!');
                                                }
                                            });
                                        }
                                    } else {

                                    }
                                });
                                //$("#dropdownAction").simulate('mousedown');
                            }
                        }
                    }
                });
            });
        }, function () {
            if (egov.cshtml.transfer.dialogLayout) {
                egov.cshtml.transfer.dialogLayout = null;
            }
        });
    };

    // ===================================================================================================================
    /// <summary>Class hỗ trợ chuyển hs, vb thuoc huong xu ly documentCopyId</summary>
    /// <param name="docCopyId" type="int">DocumentCopyId</param>
    /// <param name="frame" type="String"> Frame đang thao tác</param>
    egov.document.transfer = function (documentCopyId, frame) {
        this.frame = frame;
        this.docCopyId = documentCopyId || 0;
        this.isCreating = this.docCopyId <= 0;
        this.isTranferring = false;
        this.isComfirmedRelations = true; // bỏ confirm cho BIDV
    };

    /// <summary>Enum danh sách các hướng chuyển đặc biệt</summary>
    egov.document.transfer.prototype.actionSpecial = {
        LuuSoVaPhatHanhNoiBo: 'LuuSoVaPhatHanhNoiBo',
        LuuSoNoiBo: 'LuuSoNoiBo',
        LuuSoVaPhatHanhRaNgoai: 'LuuSoVaPhatHanhRaNgoai',
        ChuyenNguoiKhoiTao: 'ChuyenNguoiKhoiTao',
        ChuyenYKienDongGopVbDxl: 'ChuyenYKienDongGopVbDxl',
        TiepTucXuLy: 'TiepTucXuLy',
        ChuyenNguoiCoQuyenDongGopYKien: 'ChuyenNguoiCoQuyenDongGopYKien',
        TiepNhanHoSo: 'TiepNhanHoSo',
        TiepNhanHoSoVaTiepTuc: 'TiepNhanHoSoVaTiepTuc',
        CapNhatKetQuaDungXuLy: 'CapNhatKetQuaDungXuLy',
        ChuyenYKienDongGopVbXinYKien: 'ChuyenYKienDongGopVbXinYKien',
        ChuyenNguoiGui: 'ChuyenNguoiGui'
    };

    /// <summary>Mở form chuyển theo dự kiến</summary>
    egov.document.transfer.prototype.openTransferPlan = function () {
        var $this = this;
        var frame = document.getElementById($this.frame).contentWindow;
        if (frame.egov.cshtml.document.transferplan) {
            var plugin = frame.egov.cshtml.document.getPlugin();
            var allFileOpened = frame.egov.cshtml.document.getFileOpened();
            var settings = {};
            $(document).off('checkModifySuccess');
            $(document).on('checkModifySuccess', function (e, modifiedFiles) {
                settings.width = 810;
                settings.autoResize = true;
                settings.title = "eGovCloud - Bàn giao hồ sơ, văn bản";
                settings.buttons = [ // danh sách các nút chức năng trên form bàn giao
                        {
                            text: "Chuyển",
                            click: function () {
                                var transferplan = egov.cshtml.transfer.getDestination();
                                $this.transferNormal(transferplan, modifiedFiles);
                            }
                        },
                        {
                            text: "Bỏ qua",
                            click: function () {
                                settings.close();
                            }
                        }
                ];
                var transferplan = frame.egov.cshtml.document.transferplan;
                // Mở dialog form bàn giao.
                $.get('/Transfer/GetUserByAction',
                    {
                        actionId: transferplan.ActionId,
                        workflowId: transferplan.WorkflowId,
                        documentCopyId: $this.docCopyId
                    }, function (data) {
                        if (data) {
                            if ($.isArray(data)) {
                                var $transfer = $('<div id="transferDialog"></div>').append($('#transferTemplate').tmpl()).appendTo($('body'));
                                $dialog.openexist($transfer, settings, function () {
                                    egov.cshtml.transfer.initLayout();
                                    egov.cshtml.transfer.databind(data, transferplan.NextNodeId, transferplan.WorkflowId, transferplan.currentNodeId, transferplan.ActionId, transferplan.UserIdXlc, transferplan.UserIdsDxl);
                                    egov.cshtml.transfer.showDG(transferplan.TargetComment, transferplan.IsThongbao, transferplan.IsDxl, transferplan.IsAttachYk);

                                    $('.dialog-center').css({ border: 'none' });
                                    $('.dialog-south').css({ border: 'none' });
                                    $('.dialog-center-south').css({ border: 'none' });
                                }, function () {
                                    if (egov.cshtml.transfer.dialogLayout) {
                                        egov.cshtml.transfer.dialogLayout = null;
                                    }
                                });
                            } else {
                                if (data.error) {
                                    eGovMessage.show(data.error);
                                }
                            }
                        } else {
                            eGovMessage.show('Bạn không có quyền xử lý văn bản!');
                        }
                    });
            });

            if (allFileOpened.length > 0) {
                plugin.closeAllOfficeDocuments();
                if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                    confirmSaveAttachment(allFileOpened, {}, plugin);
                }
            } else {
                $(document).trigger('checkModifySuccess', {});
            }
        }
    };

    /// <summary> Mở form bàn giao</summary>
    /// <param name="actionId" type="String"> Id hướng chuyển</param>
    /// <param name="workflowId" type="String"> Id quy trình</param>
    /// <param name="nextNodeId" type="Int">Id Node nhận</param>
    /// <param name="currentnodeid" type="Int">Id Node hiện tại</param>
    /// <param name="userIdXlc" type="Int">Id người xử lý</param>
    /// <param name="isAllowSign" type="Bool">Hướng chuyển cho phép ký hay không</param>
    egov.document.transfer.prototype.open = function (actionId, workflowId, nextNodeId, currentnodeid, userIdXlc, isAllowSign) {// CuongNT@bkav.com - 040713: bo nextnodeid, documentCopyId vi da truyen khi khoi tao transfer roi.
        var $this = this;
        var frame = document.getElementById($this.frame).contentWindow;
        if (!$this.isCreating && !$this.isComfirmedRelations && frame.egov.cshtml.document.relations.length > 0) {
            new egov.document.docRelations($this.frame).confirm(frame.egov.cshtml.document.relations, actionId, workflowId, $this.docCopyId, nextNodeId, currentnodeid, isAllowSign); // CuongNT@bkav.com - 040713: bo $this.docId, nextnodeid, $this.docCopyId
            return;
        }
        window.targetForComments = [];
        var settings = {}; // dialog setting
        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();
        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            settings.width = 810;
            settings.autoResize = true;
            settings.title = "eGovCloud - Bàn giao hồ sơ, văn bản";
            settings.buttons = [
                    {
                        text: "Chuyển",
                        "class": "btn-submit",
                        click: function () {
                            if (!$this.isTransferring) {
                                $this.isTransferring = true;
                                deActiveDialogButton();
                                eGovMessage.notification("Đang chuyển...", eGovMessage.messageTypes.processing, false);
                                $this.send(modifiedFiles, isAllowSign);
                            }
                        }
                    },
                    {
                        text: "Bỏ qua",
                        "class": "btn-cancel",
                        click: function () {
                            settings.close();
                        }
                    }
            ];
            // Mở dialog form bàn giao.
            $.get('/Transfer/GetUserByAction',
                {
                    actionId: actionId,
                    workflowId: workflowId,
                    documentCopyId: $this.docCopyId
                }, function (data) {
                    if (data) {
                        if ($.isArray(data)) {
                            var $transfer = $('<div id="transferDialog"></div>').append($('#transferTemplate').tmpl()).appendTo($('body'));
                            $dialog.openexist($transfer, settings, function () {
                                egov.cshtml.transfer.initLayout();
                                egov.cshtml.transfer.databind(data, nextNodeId, workflowId, currentnodeid, actionId, userIdXlc);

                                //hopcv
                                //double click người dung thì mặc định là người xử lý chính
                                var selectUserXLC = $('#tblUsersTransfer tbody tr');
                                $(selectUserXLC).each(function (i, item) {
                                    $(item).bind("dblclick", function () {
                                        $(selectUserXLC).find('.xulychinhItem').attr('checked', false);
                                        $(this).find('.xulychinhItem').attr('checked', true);
                                        if (!$this.isTransferring) {
                                            $this.isTransferring = true;
                                            deActiveDialogButton();
                                            eGovMessage.notification("Đang chuyển...", eGovMessage.messageTypes.processing, false);
                                            $this.send(modifiedFiles, isAllowSign);
                                            settings.close();
                                        }
                                    });
                                });
                                //end

                                $('.dialog-center').css({ border: 'none' });
                                $('.dialog-south').css({ border: 'none' });
                                $('.dialog-center-south').css({ border: 'none' });
                            }, function () {
                                if (egov.cshtml.transfer.dialogLayout) {
                                    egov.cshtml.transfer.dialogLayout = null;
                                }
                            });
                        } else {
                            if (data.error) {
                                eGovMessage.show(data.error);
                            }
                        }
                    } else {
                        eGovMessage.show('Bạn không có quyền xử lý văn bản!');
                    }
                });
        });


        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary> Bàn giao</summary>
    /// <param name=" transferFrame" type="String"> Tên frame chứa form bàn giao (xem hàm open() để biết) </param>
    egov.document.transfer.prototype.send = function (modifiedFiles, isAllowSign) {
        var destination = egov.cshtml.transfer.getDestination(); // lấy danh sách chuyển.
        if (destination == null) {
            activeDialogButton();
            this.isTransferring = false;
            return;
        }

        // Hiển thị lưu sổ cá nhân
        var $this = this;
        if ($this.isCreating) {
            $.get('/StorePrivate/GetStoreActive',
                {},
                function (result) {
                    if (result) {
                        if (result.storePrivate.length > 0 || result.storeShare.length > 0) {
                            egov.cshtml.home.tree.storeprivate.openDialogSave(result, function (selectedId) {
                                if (isAllowSign) {
                                    $this.signDocument(document.getElementById($this.frame).contentWindow, destination, modifiedFiles, null, null, selectedId);
                                } else {
                                    $this.transferNormal(destination, modifiedFiles, null, null, selectedId); // bàn giao
                                }
                            }, function () {
                                if (isAllowSign) {
                                    $this.signDocument(document.getElementById($this.frame).contentWindow, destination, modifiedFiles);
                                } else {
                                    $this.transferNormal(destination, modifiedFiles); // bàn giao
                                }
                            });
                        } else {
                            if (isAllowSign) {
                                $this.signDocument(document.getElementById($this.frame).contentWindow, destination, modifiedFiles);
                            } else {
                                $this.transferNormal(destination, modifiedFiles); // bàn giao
                            }
                        }
                    }
                }
            )
            .fail(function () {
                eGovMessage.show('Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân. Bạn có muốn chuyển tiếp?', 'Thông báo',
                    eGovMessage.messageButtons.YesNo,
                    function () {
                        if (isAllowSign) {
                            $this.signDocument(document.getElementById($this.frame).contentWindow, destination, modifiedFiles);
                        } else {
                            $this.transferNormal(destination, modifiedFiles); // bàn giao
                        }
                    },
                    function () {
                        $this.isTransferring = false;
                        activeDialogButton();
                        eGovMessage.notification('hide');
                    }
                );
            });
        } else {
            if (isAllowSign) {
                $this.signDocument(document.getElementById($this.frame).contentWindow, destination, modifiedFiles);
            } else {
                $this.transferNormal(destination, modifiedFiles); // bàn giao
            }
        }
    };

    /// <summary>Hiển thị chọn ký đính kèm</summary>
    egov.document.transfer.prototype.signDocument = function (frame, destination, modifiedFiles, key, currentTab, storePrivateId) {
        var $this = this;
        var plugin = egov.cshtml.home.plugin.getPlugin();
        var pluginName = 'eOfficePlus';
        var detectBrowser = new browserDetect();
        detectBrowser.detect();
        var os = detectBrowser.getOS();
        var isMobile = (os === 'iPhone' || os === 'iPad' || os == 'Android' || os === 'Windows Phone');
        if (!plugin && $("#plugin").length === 0 && !isMobile) {
            if (FireBreath.isPluginInstalled(pluginName)) {
                $("body").append(FireBreath.injectPlugin(pluginName, "plugin", function () {
                    plugin = document.getElementById('plugin');
                    egov.cshtml.home.plugin.getPlugin(plugin);
                    $this.signDocument(frame, destination, modifiedFiles, key, currentTab, storePrivateId);
                }));
            } else {
                egov.cshtml.home.plugin.showDialogDownloadPlugin(function () { $this.signDocument(frame, destination, modifiedFiles, key, currentTab, storePrivateId); });
            }
        } else {
            var oldFiles = frame.egov.cshtml.document.getOldFiles();
            var newFiles = frame.egov.cshtml.document.getSelectedFiles();
            var removeFiles = frame.egov.cshtml.document.getRemoveFiles();
            var openedFiles = frame.egov.cshtml.document.getFileOpened();
            var openedFileIds = _.pluck(openedFiles, 'id');
            var regex = /(.doc|.docx|.pdf)$/i;

            oldFiles = _.filter(oldFiles, function (item) {
                return !_.contains(removeFiles, item.Id);
            });
            var confirmSignFiles = [];
            $.each(newFiles, function (key1, value) {
                if (regex.test(value.name.toLowerCase())) {
                    confirmSignFiles.push({ id: key1, name: value.name, ext: value.name.split('.').pop(), isNewFile: true });
                }
            });
            $.each(oldFiles, function (i, item) {
                if (regex.test(item.Name.toLowerCase())) {
                    confirmSignFiles.push({ id: item.Id, name: item.Name, ext: item.Name.split('.').pop(), isNewFile: false });
                }
            });
            if (confirmSignFiles.length > 0) {
                var $confirmDialog = $('<div></div>');
                var $table = $('<table><colgroup><col style="width:30px"/><col /></colgroup><thead><tr><td></td><td>Tên tệp</td></tr></thead><tbody></tbody></table>');
                $.each(confirmSignFiles, function (i, item) {
                    $table.children('tbody').append('<tr><td><input data-id="' + item.id + '" type="checkbox" checked="checked" /></td><td>' + item.name + '</td></tr>');
                });
                $confirmDialog.append($table).appendTo('body');
                var settings = {};
                settings.width = 500;
                settings.height = 300;
                settings.autoResize = true;
                settings.title = "eGovCloud - Chọn tệp cần ký";
                settings.buttons = [
                    {
                        text: "Chuyển",
                        "class": "btn-submit",
                        click: function () {
                            var $selected = $table.find('input:checked');
                            var selectedId = [];
                            if ($selected.length > 0) {
                                $selected.each(function (i, item) {
                                    selectedId.push($(item).attr('data-id'));
                                });
                                var selectedFiles = _.filter(confirmSignFiles, function (item) {
                                    return _.contains(selectedId, item.id.toString());
                                });
                                var fileMustDownload = _.filter(selectedFiles, function (item) {
                                    return !_.contains(openedFileIds, item.id);
                                });
                                if (fileMustDownload.length > 0) {
                                    var fileTempIds = [];
                                    var fileIds = [];
                                    $.each(fileMustDownload, function (i, item) {
                                        if (item.isNewFile) {
                                            if (isMobile) {
                                                fileTempIds.push(item.id + '.' + item.ext);
                                            } else {
                                                fileTempIds.push(item.id);
                                            }
                                        } else {
                                            fileIds.push(item.id);
                                        }
                                    });

                                    $.ajax({
                                        type: "GET",
                                        url: '/Attachment/DownloadAttachmentForSignBase64',
                                        traditional: true,
                                        data: {
                                            fileIds: fileIds,
                                            fileTempIds: fileTempIds,
                                            convertWordTopdf: isMobile
                                        },
                                        success: function (data) {
                                            if (data) {
                                                var result = JSON.parse(data);
                                                if (result.signatureConfig && !isMobile) {
                                                    $.each(result.signatureConfig, function (i, item) {
                                                        if (item.ImagePath && item.SignType === 0) {
                                                            var filename = 'ImageSignatureTemp_' + i + item.Ext;
                                                            delete item.Ext;
                                                            var filesize = plugin.writeFileBase64(filename, item.ImagePath, false);
                                                            item.ImagePath = plugin.getTempFolder() + filename;
                                                        } else {
                                                            item.ImagePath = '';
                                                        }
                                                    });
                                                }
                                                if (result.files) {
                                                    var downloaded = result.files;
                                                    var filePdfAddNew = [];
                                                    if (isMobile) {
                                                        $.each(selectedFiles, function (i, item) {
                                                            var content;
                                                            if (downloaded['' + item.id]) {
                                                                content = TOMICA.doSignPDF(downloaded['' + item.id], egov.cshtml.home.fullnameAndEmail, '');
                                                                if (content != '') {
                                                                    if (item.ext === 'doc' || item.ext === 'docx') {
                                                                        filePdfAddNew.push({ id: '' + i, name: item.name.substring(0, item.name.indexOf('.' + item.ext)) + '.pdf', value: content });
                                                                    } else if (item.ext === 'pdf') {
                                                                        modifiedFiles[item.id] = content;
                                                                    }
                                                                }
                                                                // Đoạn này để test giả lập
                                                                //var filename = 'FileTemp_' + item.id + '.pdf';
                                                                //var filesize = plugin.writeFileBase64(filename, downloaded['' + item.id], false);
                                                                //item.filename = filename;
                                                                //var cert = plugin.getCertIndex();
                                                                //var config1 = { ConfigSignPDFList: result.signatureConfig };
                                                                //if (item.ext === 'doc' || item.ext === 'docx') {
                                                                //    content = plugin.signPDF(filename, '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config1), cert);
                                                                //    if (content != '') {
                                                                //        filePdfAddNew.push({ id: '' + i, name: item.name.substring(0, item.name.indexOf('.' + item.ext)) + '.pdf', value: content });
                                                                //    }
                                                                //} else if (item.ext === 'pdf') {
                                                                //    content = plugin.signPDF(filename, '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config1), cert);
                                                                //    if (content != '') {
                                                                //        modifiedFiles[item.id] = content;
                                                                //    }
                                                                //}
                                                            }
                                                        });
                                                    } else {
                                                        var config = { ConfigSignPDFList: result.signatureConfig };
                                                        var idxCert = plugin.getCertIndex();
                                                        if (idxCert > -1) {
                                                            $.each(selectedFiles, function (i, item) {
                                                                var content;
                                                                if (downloaded['' + item.id]) {
                                                                    var filename = 'FileTemp_' + item.id + '.' + item.ext;
                                                                    var filesize = plugin.writeFileBase64(filename, downloaded['' + item.id], false);
                                                                    item.filename = filename;
                                                                    if (item.ext === 'doc' || item.ext === 'docx') {
                                                                        content = plugin.signWord(filename, 'FileTemp_' + item.id + '_sign.pdf', '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config), idxCert);
                                                                        if (content != '') {
                                                                            filePdfAddNew.push({ id: '' + i, name: item.name.substring(0, item.name.indexOf('.' + item.ext)) + '.pdf', value: content });
                                                                        }
                                                                    } else if (item.ext === 'pdf') {
                                                                        content = plugin.signPDF(filename, '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config), idxCert);
                                                                        if (content != '') {
                                                                            modifiedFiles[item.id] = content;
                                                                        }
                                                                    }
                                                                } else {
                                                                    var opened = _.find(openedFiles, function (file) {
                                                                        return file.id == item.id;
                                                                    });
                                                                    if (opened) {
                                                                        item.filename = opened.name;
                                                                        if (item.ext === 'doc' || item.ext === 'docx') {
                                                                            content = plugin.signWord(opened.name, 'FileTemp_' + item.id + '_sign.pdf', '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config), idxCert);
                                                                            if (content != '') {
                                                                                filePdfAddNew.push({ id: '' + i, name: item.name.substring(0, item.name.indexOf('.' + item.ext)) + '.pdf', value: content });
                                                                            }
                                                                        } else if (item.ext === 'pdf') {
                                                                            content = plugin.signPDF(opened.name, '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config), idxCert);
                                                                            if (content != '') {
                                                                                modifiedFiles[item.id] = content;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            });
                                                        } else {
                                                            $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
                                                        }
                                                    }
                                                    if (filePdfAddNew.length > 0) {
                                                        $.post('/Attachment/UploadTempScan',
                                                            {
                                                                files: JSON.stringify(filePdfAddNew)
                                                            }, function (resultUpload) {
                                                                if (resultUpload) {
                                                                    $.each(resultUpload, function (index, file) {
                                                                        if (!file.error) {
                                                                            newFiles[file.key] = { name: file.name };
                                                                        }
                                                                    });
                                                                    $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
                                                                }
                                                            }).fail(function () {
                                                                eGovMessage.notification("Có lỗi xảy ra", eGovMessage.messageTypes.error);
                                                            });
                                                    } else {
                                                        $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
                                                    }
                                                }
                                            }
                                        },
                                        error: function () {
                                            eGovMessage.notification("Có lỗi trong quá trình lưu dự thảo.", eGovMessage.messageTypes.error);
                                        }
                                    });
                                } else {
                                    $.ajax({
                                        type: "GET",
                                        url: '/Attachment/GetSignConfig',
                                        data: {},
                                        success: function (signatureConfig) {
                                            if (signatureConfig) {
                                                $.each(signatureConfig, function (i, item) {
                                                    if (item.ImagePath && item.SignType === 0) {
                                                        var filename = 'ImageSignatureTemp_' + i + item.Ext;
                                                        delete item.Ext;
                                                        var filesize = plugin.writeFileBase64(filename, item.ImagePath, false);
                                                        item.ImagePath = plugin.getTempFolder() + filename;
                                                    } else {
                                                        item.ImagePath = '';
                                                    }
                                                });
                                                var filePdfAddNew = [];
                                                var config = { ConfigSignPDFList: signatureConfig };
                                                var idxCert = plugin.getCertIndex();
                                                if (idxCert > -1) {
                                                    $.each(selectedFiles, function (i, item) {
                                                        var content;
                                                        var opened = _.find(openedFiles, function (file) {
                                                            return file.id == item.id;
                                                        });
                                                        if (opened) {
                                                            item.filename = opened.name;
                                                            if (item.ext === 'doc' || item.ext === 'docx') {
                                                                content = plugin.signWord(opened.name, 'FileTemp_' + item.id + '_sign.pdf', '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config), idxCert);
                                                                if (content != '') {
                                                                    filePdfAddNew.push({ id: '' + i, name: item.name.substring(0, item.name.indexOf('.' + item.ext)) + '.pdf', value: content });
                                                                }
                                                            } else if (item.ext === 'pdf') {
                                                                content = plugin.signPDF(opened.name, '1', egov.cshtml.home.fullnameAndEmail, '1', JSON.stringify(config), idxCert);
                                                                if (content != '') {
                                                                    modifiedFiles[item.id] = content;
                                                                }
                                                            }
                                                        }
                                                    });
                                                    if (filePdfAddNew.length > 0) {
                                                        $.post('/Attachment/UploadTempScan',
                                                            {
                                                                files: JSON.stringify(filePdfAddNew)
                                                            },
                                                            function (resultUpload) {
                                                                if (resultUpload) {
                                                                    $.each(resultUpload, function (index, file) {
                                                                        if (!file.error) {
                                                                            newFiles[file.key] = { name: file.name };
                                                                        }
                                                                    });
                                                                    $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
                                                                }
                                                            }).fail(function () {
                                                                eGovMessage.notification("Có lỗi xảy ra", eGovMessage.messageTypes.error);
                                                            });
                                                    } else {
                                                        $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
                                                    }
                                                } else {
                                                    $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
                                                }
                                            }
                                        },
                                        error: function () {
                                            eGovMessage.notification("Có lỗi trong quá trình lưu dự thảo.", eGovMessage.messageTypes.error);
                                        }
                                    });
                                }
                            } else {
                                $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId);
                            }
                        }
                    },
                    {
                        text: "Bỏ qua",
                        "class": "btn-cancel",
                        click: function () {
                            $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId);
                        }
                    }
                ];
                $dialog.close();
                $dialog.openexist($confirmDialog, settings, function () {
                    $table.grid({
                        isResizeColumn: false,
                        isFixHeightContent: true,
                        height: 175
                    });
                }, function () {

                });
            } else {
                $this.transferNormal(destination, modifiedFiles, key, currentTab, storePrivateId); // bàn giao
            }
        }
    };

    // TODO: Chuyen 3 ham sau vao window.egov.document cho hop ly hon
    /// <summary> Lưu văn bản dự thảo: dùng khi đóng tab mà hồ sơ đã có thay đổi về dữ liệu => hỏi có lưu hay ko?</summary>
    egov.document.transfer.prototype.saveDocDraft = function (currentTab) {
        var $this = this;
        var frame = document.getElementById($this.frame).contentWindow;
        var token = $("input[name='__RequestVerificationToken']", '#TransferSaveDocDraft').val();
        var doc = frame.egov.cshtml.document.getDoc(); // lấy nội dung văn bản.
        if (doc == null) {
            return;
        }
        var selectedFiles = frame.egov.cshtml.document.getSelectedFiles(); // lấy danh sách các file
        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();
        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });
            $.ajax({
                type: "POST",
                url: '/Transfer/SaveDocDraft',
                dataType: "json",
                traditional: true,
                data: {
                    "doc": JSON.stringify(doc),
                    "files": JSON.stringify(selectedFiles),
                    __RequestVerificationToken: token
                },
                success: function (data) {
                    if (data.success) {
                        egov.cshtml.home.tab.close(currentTab);
                        egov.cshtml.home.reloadData();
                    } else {
                        eGovMessage.show(data.error);
                    }
                },
                error: function () {
                    eGovMessage.notification("Có lỗi trong quá trình lưu dự thảo.", eGovMessage.messageTypes.error);
                }
            });
        });
        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary> Lưu hồ sơ, văn bản</summary>
    egov.document.transfer.prototype.saveDoc = function (currentTab) {
        var $this = this;
        var frame = document.getElementById($this.frame).contentWindow;
        var token = $("input[name='__RequestVerificationToken']", "#TransferSaveDoc").val();

        var doc = frame.egov.cshtml.document.getDoc(); // lấy nội dung văn bản.
        if (doc == null) {
            return;
        }
        var selectedFiles = frame.egov.cshtml.document.getSelectedFiles(); // lấy danh sách các file
        var removeFiles = frame.egov.cshtml.document.getRemoveFiles(); // lấy danh sách các file đã bị loại bỏ
        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();
        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });
            $.ajax({
                type: "POST",
                url: '/Transfer/SaveDoc',
                dataType: "json",
                traditional: true,
                data: {
                    "doc": JSON.stringify(doc),
                    "files": JSON.stringify(selectedFiles),
                    'removeAttachmentIds': removeFiles,
                    'modifiedFiles': JSON.stringify(modifiedFiles),
                    __RequestVerificationToken: token
                },
                success: function (data) {
                    if (data.success) {
                        eGovMessage.notification('Lưu văn bản thành công', eGovMessage.messageTypes.success);
                        egov.cshtml.home.tab.close(currentTab);
                    } else {
                        eGovMessage.notification(data.error, eGovMessage.messageTypes.error);
                    }
                },
                error: function () {
                    eGovMessage.notification("Lưu văn bản bị lỗi.", eGovMessage.messageTypes.error);
                }
            });
        });
        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary> Tiếp nhận văn bản/hồ sơ</summary>
    /// <param name="useridnext" type="int">Id người nhận</param>
    /// <param name="key" type="String">Description</param>
    /// <param name="nextNodeId" type="int">Id node nhạn</param>
    /// <param name="workflowId" type="int">Id quy trinh</param>
    egov.document.transfer.prototype.transferSpecialCreate = function (key, currentTab) {
        var $this = this;
        var frame = document.getElementById($this.frame).contentWindow;
        var token = $("input[name='__RequestVerificationToken']", "#TransferTransferTiepNhan").val();

        var doc = frame.egov.cshtml.document.getDoc(); // lấy nội dung văn bản.
        if (doc == null) {
            return;
        }
        var selectedFiles = frame.egov.cshtml.document.getSelectedFiles();
        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();
        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });
            $.ajax({
                type: "POST",
                url: '/Transfer/TransferTiepNhan',
                traditional: true,
                data: {
                    "doc": JSON.stringify(doc),
                    "files": JSON.stringify(selectedFiles),
                    __RequestVerificationToken: token
                },
                success: function (data) {
                    if (data.success) {
                        // Nếu tiếp nhận và tiếp tục thì không close tab văn bản
                        if (key == 'TiepNhanHoSoVaTiepTuc') {
                            egov.cshtml.home.reloadData();
                            frame.location.reload(true);
                            eGovMessage.notification('Tiếp nhận hồ sơ thành công.', eGovMessage.messageTypes.success);
                        } else {
                            if (currentTab) {
                                egov.cshtml.home.tab.close(currentTab);
                            } else {
                                egov.cshtml.home.tab.closeActiveTab();
                            }
                            egov.cshtml.home.reloadData();
                        }
                        eGovMessage.notification("Tiếp nhận hồ sơ thành công.", eGovMessage.messageTypes.success);
                    } else {
                        eGovMessage.show(data.error);
                    }
                },
                error: function () {
                    eGovMessage.notification("Lưu văn bản bị lỗi.", eGovMessage.messageTypes.error);
                }
            });
        });
        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary> Ban giao van ban fix cung</summary>
    /// <param name="useridnext" type="int">Id người nhận</param>
    /// <param name="key" type="String">Description</param>
    /// <param name="nextNodeId" type="int">Id node nhạn</param>
    /// <param name="workflowId" type="int">Id quy trinh</param>
    egov.document.transfer.prototype.transferSpecialEdit = function (useridnext, key, nextNodeId, workflowId) {
        var $this = this;
        if (useridnext == undefined || useridnext <= 0 ||
            nextNodeId == undefined || nextNodeId <= 0 ||
            workflowId == undefined || workflowId <= 0) {
            throw "ArgumentNullException(useridnext, nextNodeId, workflowId khong hop le.)";
        }

        // this.frame --> frame Chi tiết văn bản
        var frame = document.getElementById($this.frame).contentWindow;

        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();

        // CuongNT@bkav.com - 060713: Thêm destination thay cho nextNodeId, useridnext, workflowId
        var destination = {};
        destination.UserIdXlc = useridnext;
        destination.UserIdsDxl = [];
        destination.IsThongbao = false;
        destination.IsDxl = false;
        destination.IsAttachYk = false;
        destination.UserIdsDg = [];
        destination.NextNodeId = nextNodeId;
        destination.WorkflowId = workflowId;
        destination.TargetComment = JSON.stringify([]);

        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            $this.transferNormal(destination, modifiedFiles, key);
        });
        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary>Bàn giao van ban theo config quy trinh</summary>
    /// <param name="destination" type="json">Hướng chuyển</param>
    egov.document.transfer.prototype.transferNormal = function (destination, modifiedFiles, key, currentTab, storePrivateId) {
        key = key || "Normal";
        var frame = document.getElementById(this.frame).contentWindow;
        var doc = frame.egov.cshtml.document.getDoc(); // lấy nội dung văn bản.
        if (doc == null) {
            activeDialogButton();
            this.isTransferring = false;
            return;
        }
        var selectedFiles = frame.egov.cshtml.document.getSelectedFiles(); // lấy danh sách các file
        var removeFiles = frame.egov.cshtml.document.getRemoveFiles(); // lấy danh sách các file đã bị loại bỏ
        var hasDestination = false;
        var destinationPlan;
        var $this = this;
        var token = $("input[name='__RequestVerificationToken']", "#TransferTransfer").val();
        if (destination) {
            hasDestination = true;
            if (key === 'Normal' && frame.egov.cshtml.document.newtransferplan) {
                if (destination.UserIdXlc) {
                    if (frame.egov.cshtml.document.newtransferplan.transferTo.userid === destination.UserIdXlc) {
                        destinationPlan = frame.egov.cshtml.document.newtransferplan.transferPlan;
                    }
                } else {
                    if (destination.UserIdsDxl.length === 1) {
                        if (frame.egov.cshtml.document.newtransferplan.transferTo.userid === destination.UserIdsDxl[0]) {
                            destinationPlan = frame.egov.cshtml.document.newtransferplan.transferPlan;
                        }
                    }
                }
            }
        }
        $.each(selectedFiles, function (keyname, value) {
            if (modifiedFiles[keyname]) {
                value.content = modifiedFiles[keyname];
                delete modifiedFiles[keyname];
            }
        });

        $.ajax({
            type: "POST",
            url: '/Transfer/Transfer',
            dataType: "json",
            traditional: true,
            data: {
                __RequestVerificationToken: token,
                "doc": JSON.stringify(doc),
                "destination": hasDestination ? JSON.stringify(destination) : "",
                "files": JSON.stringify(selectedFiles),
                "modifiedFiles": JSON.stringify(modifiedFiles),
                "removeAttachmentIds": removeFiles,
                //"actionSpecial": key,
                "storePrivateId": storePrivateId,
                "destinationPlan": destinationPlan ? JSON.stringify(destinationPlan) : ""
            },
            success: function (data) {
                if (data.success) {
                    $dialog.close();
                    // Nếu tiếp nhận và tiếp tục thì không close tab văn bản
                    if (key == 'TiepNhanHoSoVaTiepTuc') {
                        egov.cshtml.home.reloadData();
                        frame.location.reload(true);
                        eGovMessage.notification('Tiếp nhận hồ sơ thành công.', eGovMessage.messageTypes.success);
                    } else {
                        if (currentTab) {
                            egov.cshtml.home.tab.close(currentTab);
                        } else {
                            egov.cshtml.home.tab.closeActiveTab();
                        }
                        egov.cshtml.home.reloadData();
                    }
                    eGovMessage.notification("Chuyển hồ sơ thành công.", eGovMessage.messageTypes.success);
                } else {
                    eGovMessage.notification('hide');
                    eGovMessage.show(data.error);
                }
            },
            error: function () {
                activeDialogButton();
                $this.isTransferring = false;
                eGovMessage.notification("Có lỗi trong quá trình bàn giao.", eGovMessage.messageTypes.error);
            }
        });
    };

    /// <summary>Chuyển ý kiến đóng góp Văn bản ĐXL, Văn bản xin ý kiến</summary>
    /// <param name="destination" type="json">Hướng chuyển</param>
    egov.document.transfer.prototype.transferChuyenYKien = function (documentCopyId, actionSpecial, comment) {
        var $this = this;
        var frame = document.getElementById($this.frame).contentWindow;
        var token = $("input[name='__RequestVerificationToken']", "#TransferTransferYKienDongGop").val();

        if (!$this.isCreating && !$this.isComfirmedRelations && frame.egov.cshtml.document.relations.length > 0) {
            new egov.document.docRelations($this.frame).confirm(frame.egov.cshtml.document.relations, actionId, workflowId, $this.docCopyId, nextNodeId, currentnodeid); // CuongNT@bkav.com - 040713: bo $this.docId, nextnodeid, $this.docCopyId
            return;
        }
        var doc = frame.egov.cshtml.document.getDoc(); // lấy nội dung văn bản.
        if (doc == null) {
            activeDialogButton();
            this.isTransferring = false;
            return;
        }
        var selectedFiles = frame.egov.cshtml.document.getSelectedFiles(); // lấy danh sách các file
        var removeFiles = frame.egov.cshtml.document.getRemoveFiles(); // lấy danh sách các file đã bị loại bỏ
        var $currentTab = egov.cshtml.home.tab.getActiveTab();
        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();
        var settings = {};
        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            $.ajax({
                type: "POST",
                url: '/Transfer/TransferYKienDongGop',
                dataType: "json",
                traditional: true,
                data: {
                    "doc": JSON.stringify(doc),
                    "files": JSON.stringify(selectedFiles),
                    "modifiedFiles": JSON.stringify(modifiedFiles),
                    removeAttachmentIds: removeFiles,
                    "documentCopyId": documentCopyId,
                    "actionSpecial": actionSpecial,
                    "comment": comment,
                    __RequestVerificationToken: token
                },
                success: function (data) {
                    if (data.success) {
                        eGovMessage.notification('Trả lời thành công.', eGovMessage.messageTypes.success);
                        $dialog.close();
                        egov.cshtml.home.tab.close($currentTab);
                        egov.cshtml.home.reloadData();
                    } else {
                        eGovMessage.show(data.error);
                    }
                },
                error: function () {
                    eGovMessage.notification("Có lỗi trong quá trình trả lời ý kiến.", eGovMessage.messageTypes.error);
                }
            });
        });
        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary>Bàn giao văn bản để Xin ý kiến</summary>
    /// <param name="destination" type="json">Hướng chuyển</param>
    egov.document.transfer.prototype.transferXinYKien = function () {

    };

    /// <summary>Bàn giao văn bản để Thông báo</summary>
    /// <param name="destination" type="json">Hướng chuyển</param>
    egov.document.transfer.prototype.transferThongbao = function () {

    };

    egov.document.transfer.prototype.showActions = function (selector, event) {
        var $this = this;
        var frame = document.getElementById(this.frame).contentWindow;
        var $selector = $(selector);
        $selector.unbind('mouseup');
        if (frame.egov.cshtml.document.validateForm() === false) {
            $selector.contextMenu(false);
            return;
        }
        $(selector).contextMenu(true);
        if ($selector.attr('data-loadedDropdownItems')) {
            return;
        }
        $.contextMenu('destroy', selector);
        $.ajax({
            type: 'GET',
            data: frame.egov.cshtml.document.isCreatingDocument ? { documentTypeId: frame.egov.cshtml.document.doctypeId, isPhanloai: false } : { documentCopyId: frame.egov.cshtml.document.documentCopyId },
            url: frame.egov.cshtml.document.isCreatingDocument ? "/Transfer/GetActionsCreate" : "/Transfer/GetActionsEdit",
            beforeSend: function () {
                $selector.blockpanel({ backgroundColor: 'transparent', icon: { width: 24, height: 24 } });
            },
            success: function (data) {
                var options = null;
                if (data.error) {
                    eGovMessage.notification(data.error, eGovMessage.messageTypes.error);
                }
                else {
                    var itemsContext = {};
                    data = _.sortBy(data, function (num) { return num.priority; });
                    for (var i = 0; i < data.length; i++) {
                        itemsContext[data[i].id.toString()] = {
                            name: data[i].name,
                            icon: data[i].isspecial == true ? data[i].id : 'transferdocument',
                            disabled: !data[i].isAllow
                        };
                        if (data[i + 1] != undefined && data[i].priority != data[i + 1].priority) {
                            itemsContext['sep' + i] = "------------";
                        }
                    }
                    var transferOptions = data;
                    options = {
                        callback: function (key) {
                            var action = _.find(transferOptions, function (num) { return num.id === key; });
                            if (key === frame.egov.document.permission.actionSpecial.tiepTucXuLy.name ||
                                key === frame.egov.document.permission.actionSpecial.capNhatKetQuaDungXuLy.name ||
                                key === frame.egov.document.permission.actionSpecial.tiepNhanBoSung.name) {
                                if (isCreating) return;

                                var supplementary = new egov.document.supplementary($this.docCopyId, frame, null, null);
                                if (key === frame.egov.document.permission.actionSpecial.tiepTucXuLy.name) {
                                    supplementary.openContinueProcess();
                                } else if (key === frame.egov.document.permission.actionSpecial.capNhatKetQuaDungXuLy.name) {
                                    supplementary.openReceive();
                                } else if (key === frame.egov.document.permission.actionSpecial.tiepNhanBoSung.name) {
                                    supplementary.openReceiveSupplement();
                                }
                            } else if (key === frame.egov.document.permission.actionSpecial.tiepNhanHoSo.name ||
                                key === frame.egov.document.permission.actionSpecial.tiepNhanHoSoVaTiepTuc.name ||
                                key === frame.egov.document.permission.actionSpecial.chuyenNguoiCoQuyenDongGopYKien.name ||
                                key === frame.egov.document.permission.actionSpecial.chuyenNguoiGui.name ||
                                key === frame.egov.document.permission.actionSpecial.chuyenNguoiKhoiTao.name) {
                                if (key === frame.egov.document.permission.actionSpecial.tiepNhanHoSo.name || key === frame.egov.document.permission.actionSpecial.tiepNhanHoSoVaTiepTuc.name) {
                                    $this.transferSpecialCreate(key);
                                } else {
                                    $this.transferSpecialEdit(action.useridnext, action.id.toString(), action.nextnodeid, action.workflowid); //key --> action.Id
                                }
                            } else if (key === frame.egov.document.permission.actionSpecial.luuSoNoiBo.name ||
                                key === frame.egov.document.permission.actionSpecial.luuSoVaPhatHanhNoiBo.name ||
                                key === frame.egov.document.permission.actionSpecial.luuSoVaPhatHanhRaNgoai.name) {
                                var luusophathanh = new egov.document.luusophathanh($this.docCopyId, frame);
                                luusophathanh.open(true);
                            } else if (key === frame.egov.document.permission.actionSpecial.chuyenYKienDongGopVbDxl.name ||
                            key === frame.egov.document.permission.actionSpecial.chuyenYKienDongGopVbXinYKien.name) {
                                var comment = frame.egov.cshtml.document.getComment();
                                $this.transferChuyenYKien($this.docCopyId, key, comment);
                            } else if (!action.isspecial) {
                                $this.open(action.id.toString(), action.workflowid);
                            }
                        },
                        items: itemsContext
                    };
                }
                if (options) {
                    if (options.items) {
                        var existItem = false;
                        for (var item in options.items) {
                            existItem = true;
                            break;
                        }
                        if (existItem) {
                            $.contextMenu({
                                selector: selector,
                                trigger: 'left',
                                build: function () {
                                    return options;
                                }
                            });
                            $selector.trigger($.Event("contextmenu", { data: event.data, pageX: event.pageX, pageY: event.pageY }));
                            $selector.attr('data-loadedDropdownItems', 'true');
                        }
                    }
                }
            },
            complete: function () {
                $selector.unblockpanel();
            },
            error: function () {
                eGovMessage.notification('Có lỗi xảy ra khi tải danh sách hướng chuyển', eGovMessage.messageTypes.error);
            }
        });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ phát hành văn bản</summary>
    /// <param name="docCopyId" type="int">Id văn bản phát hành</param>
    /// <param name="frame" type="String">Frame Id của tab mở văn bản</param>
    egov.document.publishment = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    /// <summary>Mở form phát hành</summary>
    egov.document.publishment.prototype.open = function () {
        var $this = this;
        var settings = {}; // dialog setting
        var frame = document.getElementById($this.frame).contentWindow;
        var plugin = frame.egov.cshtml.document.getPlugin();
        var allFileOpened = frame.egov.cshtml.document.getFileOpened();
        $(document).off('checkModifySuccess');
        $(document).on('checkModifySuccess', function (e, modifiedFiles) {
            settings.width = 790;
            settings.height = 615;
            settings.title = "Lưu sổ phát hành";
            settings.buttons = [
                {
                    text: "Lưu",
                    click: function () {
                        $this.publish(modifiedFiles);
                    }
                },
                {
                    text: "Bỏ qua",
                    click: function () {
                        $dialog.close();
                    }
                }
            ];
            var publishmentDialog = $('<div id="publishDialog">');
            $.ajax({
                url: '/Transfer/GetPublish',
                data: { docCopyId: $this.docCopyId },
                beforeSend: function () {
                    var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                    publishmentDialog.html($loading);
                    $dialog.openexist(publishmentDialog, settings);
                },
                success: function (result) {
                    publishmentDialog.html(result);
                    $dialog.openexist(publishmentDialog, settings);
                }
            });
        });
        if (allFileOpened.length > 0) {
            plugin.closeAllOfficeDocuments();
            if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                confirmSaveAttachment(allFileOpened, {}, plugin);
            }
        } else {
            $(document).trigger('checkModifySuccess', {});
        }
    };

    /// <summary> Thuc hien phat hanh van ban</summary>
    egov.document.publishment.prototype.publish = function (modifiedFiles) {
        var usersConsult = egov.document.getUsersConsult(window.targetForComments, window.allUserDepartments);
        var $this = this;
        var token = $("input[name='__RequestVerificationToken']", "#TransferTransferPublish").val();

        var publish = $("#DocumentPublish").find(":input,select").serializeObject();
        if (publish.hdfaddressIds == "") {
            eGovMessage.show('Bạn chưa chọn nơi phát hành văn bản.', 'Phát hành văn bản');
        }
        var frame = document.getElementById($this.frame).contentWindow;
        var doc = frame.egov.cshtml.document.getDoc();
        if (doc == null) {
            return;
        }
        var selectedFiles = frame.egov.cshtml.document.getSelectedFiles(); // lấy danh sách các file
        $.each(selectedFiles, function (keyname, value) {
            if (modifiedFiles[keyname]) {
                value.content = modifiedFiles[keyname];
                delete modifiedFiles[keyname];
            }
        });
        var removeFiles = frame.egov.cshtml.document.getRemoveFiles(); // lấy danh sách các file đã bị loại bỏ
        var data = {
            documentCopyId: $this.docCopyId,
            doc: JSON.stringify(doc),
            files: JSON.stringify(selectedFiles),
            modifiedFiles: JSON.stringify(modifiedFiles),
            removeAttachmentIds: removeFiles,
            publishinfo: JSON.stringify(publish),
            usersConsult: usersConsult,
            __RequestVerificationToken: token
        };
        $.ajax({
            type: "POST",
            url: '/Transfer/TransferPublish',
            dataType: "json",
            traditional: true,
            data: data,
            success: function (data) {
                $dialog.close();
                egov.cshtml.home.tab.closeActiveTab();
                // CuongNT - 20042014: Thêm để load lại danh sách văn bản
                egov.cshtml.home.reloadData();
            },
            error: function ()
            { }
        });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ thêm hồ sơ, văn bản liên quan </summary>
    egov.document.docRelations = function (frame) {
        this.frame = frame;
        this.relationFrame = "relationFrame";
    };

    /// <summary> Mở form thêm hồ sơ liên quan</summary>
    egov.document.docRelations.prototype.open = function () {
        var $this = this;
        var $dialogFindRelation = $('#dialogFindRelation');
        var settings = {};
        settings.title = "Thêm hồ sơ, văn bản liên quan";
        settings.width = 900;
        settings.height = 500;
        settings.buttons = [
            {
                text: "Thêm",
                click: function () {
                    $this.add();
                    $dialogFindRelation.dialog('close');
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialogFindRelation.dialog('close');
                }
            }
        ];
        if (!egov.document.isLoadedFindRelation) {
            $dialog.openexist($dialogFindRelation, settings, null, null, false);
            $.ajax({
                url: '/Document/FindRelations',
                success: function (result) {
                    $dialogFindRelation.html(result);
                    egov.document.isLoadedFindRelation = true;
                }
            });
        } else {
            $('.search-advance').hide();
            $('#checkAdvance').prop('checked', false);
            $dialogFindRelation.dialog('open');
        }
    };

    /// <summary> Thêm các văn bản được chọn vào danh sách văn bản liên quan.</summary>
    egov.document.docRelations.prototype.add = function () {
        var docs = $("#listRelationsAdded tr");
        var frame = document.getElementById(this.frame).contentWindow;
        frame.egov.cshtml.document.addRelations(docs);
    };

    /// <summary>Xác nhận các văn bản liên quan sẽ được gửi kèm</summary>
    egov.document.docRelations.prototype.confirm = function (relations, actionId, workflowId, docCopyId, nextNodeId, currentNodeId, isAllowSign) {
        var $this = this;
        var settings = {};
        settings.title = "Xác nhận văn bản liên quan được xem";
        settings.width = 800;
        settings.height = 300;
        settings.buttons = [
            {
                text: "Ok",
                click: function () {
                    $this.sendConfirm();
                    $dialog.close();
                    var transferObj = new egov.document.transfer(docCopyId, $this.frame);
                    transferObj.isComfirmedRelations = true;
                    transferObj.open(actionId, workflowId, nextNodeId, currentNodeId, isAllowSign);
                }
            }
        ];
        var data = { "docCopyIds": JSON.stringify(_.pluck(relations, "RelationCopyId")) };

        $.ajax({
            url: '/Document/ConfirmRelation',
            data: data,
            type: "GET",
            success: function (result) {
                $dialog.open(result, settings);
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
                $dialog.close();
            }
        });
    };

    egov.document.docRelations.prototype.sendConfirm = function () {
        var docCopyConfirmed = $("#confirmRelations .itm-check:checked");
        var docCopyIds = [];
        if (docCopyConfirmed.length > 0) {
            docCopyConfirmed.each(function () {
                docCopyIds.push($(this).val());
            });
        }
        var frame = document.getElementById(this.frame).contentWindow;
        frame.egov.cshtml.document.confirmRelations(docCopyIds);
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ xin ý kiến</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.consult = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    var openConsult = function (settings) {
        var $consult = $('<div style="display:none"></div>');
        if (!egov.document.isLoadedDgView) {
            $.ajax({
                url: '/Transfer/GetDgView',
                success: function (result) {
                    $('body').append(result);
                    $consult.append($("#consultTemplate").tmpl());
                    $consult.find('#divDg').append($("#dgViewTemplate").tmpl());
                    $('body').append($consult);
                    window.strContent = '';
                    $('body').delegate('#txtContent', 'keyup change', function () {
                        window.strContent = $('#txtContent').val();
                    });
                    $('#userDeptPopup').height(240);
                    $('.tbl-position-for-all').height(243);
                    $dialog.openexist($consult, settings);
                    window.dataBind();
                    egov.document.isLoadedDgView = true;
                }
            });
        } else {
            $consult.append($("#consultTemplate").tmpl());
            $consult.find('#divDg').append($("#dgViewTemplate").tmpl());
            $('body').append($consult);
            window.strContent = '';
            $('body').delegate('#txtContent', 'keyup change', function () {
                window.strContent = $('#txtContent').val();
            });
            $('#userDeptPopup').height(240);
            $('.tbl-position-for-all').height(243);
            $dialog.openexist($consult, settings);
            window.dataBind();
        }
    };

    /// <summary> Mở form xin ý kiến</summary>
    /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
    egov.document.consult.prototype.open = function (isFromToolbar) {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 830;
        settings.height = 510;
        settings.title = "Xin ý kiến";
        settings.buttons = [// danh sách các nút chức năng trên form xin ý kiến
            {
                text: "Gửi",
                click: function () {
                    $this.save($this.docCopyId, isFromToolbar);
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                }
            }
        ];

        if (isFromToolbar) {
            var frame = document.getElementById($this.frame).contentWindow;
            var plugin = frame.egov.cshtml.document.getPlugin();
            var allFileOpened = frame.egov.cshtml.document.getFileOpened();
            $(document).off('checkModifySuccess');
            $(document).on('checkModifySuccess', function (e, modifiedFiles) {
                settings.buttons[0].click = function () {
                    $this.save($this.docCopyId, isFromToolbar, modifiedFiles);
                };
                openConsult(settings);
            });
            if (allFileOpened.length > 0) {
                plugin.closeAllOfficeDocuments();
                if (checkOtherProgramOpenFile(plugin, allFileOpened)) {
                    confirmSaveAttachment(allFileOpened, {}, plugin);
                }
            } else {
                $(document).trigger('checkModifySuccess', {});
            }
        } else {
            openConsult(settings);
        }
    };

    /// <summary> Gửi xin ý kiến - tương đương với phần chuyển văn bản toàn bộ đồng xử lý</summary>
    egov.document.consult.prototype.save = function (documentCopyId, isFromToolbar, modifiedFiles) {
        var $this = this;
        var usersConsult = egov.document.getUsersConsult(window.targetForComments, window.allUserDepartments);
        var targetForComments = egov.cshtml.transfer.consult.getTargetForComments(); // lấy danh sách chuyển.
        if (usersConsult.length == 0 || targetForComments == undefined) {
            eGovMessage.show('Bạn chưa chọn người xin ý kiến.');
            return;
        }
        var url = isFromToolbar == true ? '/Transfer/TransferXinYKienToolbar' : '/Transfer/TransferXinYKienContext';
        var tokenId = isFromToolbar == true ? '#TransferTransferXinYKienToolbar' : '#TransferTransferXinYKienContext';
        var data = {};
        var token = $("input[name='__RequestVerificationToken']", tokenId).val();

        if (isFromToolbar) {
            var frame = document.getElementById(this.frame).contentWindow;
            var doc = frame.egov.cshtml.document.getDoc();
            if (doc == null) {
                return;
            }
            var strContent = doc.Comments.Content;
            var selectedFiles = frame.egov.cshtml.document.getSelectedFiles(); // lấy danh sách các file
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });
            var removeFiles = frame.egov.cshtml.document.getRemoveFiles(); // lấy danh sách các file đã bị loại bỏ

            data = {
                "doc": JSON.stringify(doc),
                "files": JSON.stringify(selectedFiles),
                "modifiedFiles": JSON.stringify(modifiedFiles),
                removeAttachmentIds: removeFiles,
                usersConsult: usersConsult,
                contentRequest: strContent,
                targetForComments: targetForComments,
                __RequestVerificationToken: token
            };

        } else {
            data.documentCopyId = documentCopyId;
            data.usersConsult = usersConsult;
            data.contentRequest = strContent;
            data.targetForComments = targetForComments;
            data.__RequestVerificationToken = token
        }

        $.ajax({
            type: "POST",
            url: url,
            //  dataType: "json",
            traditional: true,
            data: data,
            success: function (result) {
                if (result.success) {
                    eGovMessage.notification(result.success, eGovMessage.messageTypes.success);
                    $dialog.close();
                } else {
                    eGovMessage.show(result.error);
                }
            },
            error: function () {
                eGovMessage.notification("Có lỗi trong quá trình xin ý kiến.", eGovMessage.messageTypes.error);
            }
        });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ gửi thông báo</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.announcement = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    /// <summary> Mở form xin ý kiến</summary>
    /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
    egov.document.announcement.prototype.open = function (isFromToolbar) {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 830;
        settings.height = 495;
        settings.title = "Gửi thông báo";
        settings.buttons = [// danh sách các nút chức năng trên form xin ý kiến
            {
                text: "Gửi",
                click: function () {
                    $this.save($this.docCopyId, isFromToolbar);
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        var $announcement = $('<div style="display:none"></div>');
        if (!egov.document.isLoadedDgView) {
            $.ajax({
                url: '/Transfer/GetDgView',
                success: function (result) {
                    $('body').append(result);
                    //$announcement.append($("#announcementTemplate").tmpl());
                    //$announcement.find('#divDg').append($("#dgViewTemplate").tmpl());
                    //$('body').append($announcement);
                    //$('#userDeptPopup').height(240);
                    //$('.tbl-position-for-all').height(242);
                    //$dialog.openexist($announcement, settings);
                    //window.dataBind();   
                    //  var a = new dgView;
                    egov.document.isLoadedDgView = true;
                }
            });
        } else {

            //$announcement.append($("#announcementTemplate").tmpl());
            //$announcement.find('#divDg').append($("#dgViewTemplate").tmpl());
            //$('body').append($announcement);
            //$('#userDeptPopup').height(240);
            //$('.tbl-position-for-all').height(242);
            //$dialog.openexist($announcement, settings);
            //window.dataBind();
        }
    };

    /// <summary> Gửi thong bao - tương đương với phần chuyển văn bản toàn bộ đồng xử lý</summary>
    egov.document.announcement.prototype.save = function (documentCopyId, isFromToolbar) {
        var $this = this;
        var usersConsult = egov.document.getUsersConsult(window.targetForComments, window.allUserDepartments);
        var targetForComments = egov.cshtml.transfer.consult.getTargetForComments(); // lấy danh sách chuyển.

        if (usersConsult.length == 0 || targetForComments == undefined) {
            eGovMessage.show('Bạn chưa chọn người gửi thông báo.');
            return;
        }
        var token = $("input[name='__RequestVerificationToken']", "#TransferTransferThongBao").val();

        $.ajax({
            type: "POST",
            url: '/transfer/TransferThongBao',
            dataType: "json",
            traditional: true,
            data: {
                documentCopyId: documentCopyId,
                ccUsers: usersConsult,
                targetForComments: targetForComments,
                __RequestVerificationToken: token
            },
            success: function (data) {
                if (data.success) {
                    eGovMessage.notification(data.success, eGovMessage.messageTypes.success);
                    $dialog.close();
                } else {
                    eGovMessage.notification(data.error, eGovMessage.messageTypes.error);
                }
            },
            error: function () {
                eGovMessage.notification("Có lỗi trong quá trình gửi thông báo.", eGovMessage.messageTypes.error);
            }
        });
    };

    // ===================================================================================================================
    ///<summary> Hàm lấy danh sách user đồng gửi - xin ý kiến</summary>
    egov.document.getUsersConsult = function (listItemsDg, allUserDept) {
        var result = [];
        if (listItemsDg != undefined) {
            if (listItemsDg.length > 0) {
                for (var i = 0; i < listItemsDg.length; i++) {
                    var objDg = listItemsDg[i];
                    var listUser;
                    if (objDg.value == itemsDg.allUsers) {
                        return _.pluck(allUserDept, 'userid');
                    }
                    else if (objDg.value == itemsDg.userItem) {
                        result.push(objDg.item.userid);
                    }
                    else if (objDg.value == itemsDg.jobtitlesItem) {
                        listUser = _.filter(allUserDept, function (num) { return num.jobtitleid == objDg.item.jobtitleid; });
                        if (listUser.length > 0) {
                            listUser = _.pluck(listUser, 'userid');
                            $.extend(result, listUser);
                        }
                    }
                    else if (objDg.value == itemsDg.deptItem) {
                        // Tim phong ban hien tai va phong ban con
                        listUser = _.filter(allUserDept, function (num) { return num.departmentid == objDg.item.deptid || num.idext.indexOf('.' + objDg.item.deptid + '.') > 0; }); //num.departmentid == objDg.item;
                        if (listUser.length > 0) {
                            listUser = _.pluck(listUser, 'userid');
                            $.extend(result, listUser);
                        }
                    }
                    else if (objDg.value == itemsDg.jobtitlesDeptItem) {
                        // Tim phong ban hien tai va phong ban con
                        listUser = _.filter(allUserDept, function (num) { return num.jobtitleid == objDg.item.jobtitleid && (num.departmentid == objDg.item.deptid || num.idext.indexOf('.' + objDg.item.deptid + '.') > 0); }); //num.departmentid == objDg.item;
                        if (listUser.length > 0) {
                            listUser = _.pluck(listUser, 'userid');
                            $.extend(result, listUser);
                        }
                    }
                }
            }
        }
        // Distinct array.
        result = _.uniq(result, false);
        return result;
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ xác nhận bàn giao và xác nhận xử lý</summary>
    /// <param name="docCopyId" type="String"></param>
    /// <param name="isTransfer" type="Bool">isTransfer - True là gọi hàm xác nhận bàn giao, false - là gọi hàm xác nhận xử lý</param>
    egov.document.confirmTransferOrProcess = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    egov.document.confirmTransferOrProcess.prototype.open = function (isTransfer) {
        var $this = this;
        if (!isTransfer) {
            var settings = {}; // dialog setting
            settings.width = 450;
            settings.title = "Đồng ý xác nhận xử lý cho hồ sơ này?";
            settings.buttons = [// danh sách các nút chức năng trên form xin ý kiến
                    {
                        text: "Đồng ý",
                        click: function () {
                            $this.save(isTransfer);
                        }
                    },
                    {
                        text: "Bỏ qua",
                        click: function () {
                            $dialog.close();
                        }
                    }
            ];

            $.ajax({
                url: '/Document/GetUsersProcess',
                data: { documentCopyId: $this.docCopyId },
                success: function (result) {
                    if (result.error) {
                        eGovMessage.show(result.error);
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
                        $dialog.open(strHtml, settings);
                    }
                }
            });
        } else {
            eGovMessage.show(
                'Bạn có đồng ý xác nhận bàn giao cho hồ sơ này không?',
                null,
                eGovMessage.messageButtons.YesNo,
                function () {
                    $this.save(isTransfer);
                }
            );
        }
    };

    egov.document.confirmTransferOrProcess.prototype.save = function (isTransfer) {
        var url, msg, tokenId;
        if (isTransfer) {
            url = '/Document/ConfirmTransfer';
            msg = 'Xác nhận bàn giao bị lỗi.';
            tokenId = '#DocumentConfirmTransfer';
        }
        else {
            url = '/Document/ConfirmProcess';
            msg = 'Xác nhận xử lý bị lỗi.';
            tokenId = '#DocumentConfirmProcess';
        }
        var token = $("input[name='__RequestVerificationToken']", tokenId).val();
        $.post(
        	url,
        	{
        	    documentCopyId: $this.docCopyId,
        	    __RequestVerificationToken: token
        	},
        	function (data) {
        	    if (data.error) {
        	        eGovMessage.notification(data.error, eGovMessage.messageTypes.error);
        	    } else {
        	        eGovMessage.notification(data.success, eGovMessage.messageTypes.success);
        	        eGovMessage.notification(data.success, eGovMessage.messageTypes.success);
        	        $dialog.close();
        	        egov.cshtml.home.reloadData();
        	    }
        	})
            .fail(function () {
                eGovMessage.notification(msg, eGovMessage.messageTypes.error);
            });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ loại bỏ văn bản khỏi hồ sơ</summary>
    /// <param name="docCopyId" type="String">Id văn bản copy</param>
    egov.document.removeDocument = function (documentCopyIds) {
        eGovMessage.show(
            'Bạn có đồng ý loại bỏ văn bản/hồ sơ này không?',
            null,
            eGovMessage.messageButtons.YesNo,
            function () {
                var token = $("input[name='__RequestVerificationToken']", "#DocumentRemoveDocument").val();
                $.ajax({
                    type: "POST",
                    url: '/Document/RemoveDocument',
                    traditional: true,
                    data: {
                        documentCopyIds: documentCopyIds,
                        __RequestVerificationToken: token
                    },
                    success: function (data) {
                        if (data.success) {
                            eGovMessage.notification(data.success, eGovMessage.messageTypes.success);
                            $dialog.close();
                            egov.cshtml.home.reloadData();
                        } else {
                            eGovMessage.show(data.error);
                        }
                    },
                    error: function () {
                        eGovMessage.notification("Có lỗi trong quá trình loại bỏ văn bản/hồ sơ.", eGovMessage.messageTypes.error);
                    }
                });
            }
        );
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ chức năng kết thúc xử lý</summary>
    /// <param name="docCopyId" type="String">Id văn bản copy</param>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.endProcess = function (docCopyId, isHsmc, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
        this.isHsmc = isHsmc;
    };

    egov.document.endProcess.prototype.open = function () {
        var $this = this;
        if ($this.isHsmc) {
            eGovMessage.show(
                'Bạn có đồng ý kết thúc hồ sơ này không?',
                null,
                eGovMessage.messageButtons.YesNo,
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
                                $this.save();
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
                        url: '/Finish/Index',
                        data: { docCopyId: $this.docCopyId },
                        beforeSend: function () {
                            //var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                            //$dialog.open($loading, settings);
                        },
                        success: function (result) {
                            $dialog.open(result, settings);
                        }
                    });
                }
            );
        } else {
            eGovMessage.show(
                'Bạn có đồng ý kết thúc văn bản này không?',
                null,
                eGovMessage.messageButtons.YesNo,
                function () {
                    $.get('/StorePrivate/GetStoreActive',
                        {},
                        function (result) {
                            if (result) {
                                if (result.storePrivate.length > 0 || result.storeShare.length > 0) {
                                    egov.cshtml.home.tree.storeprivate.openDialogSave(result, function (selectedId) {
                                        updateFinish($this.docCopyId, selectedId);
                                    }, function () {
                                        updateFinish($this.docCopyId);
                                    });
                                } else {
                                    updateFinish($this.docCopyId, null, $this.frame);
                                }
                            }
                        }
                    )
                    .fail(function () {
                        eGovMessage.notification('Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân', eGovMessage.messageTypes.error);
                    });
                }
            );
        }
    };

    var updateFinish = function (documentCopyId, storePrivateId, frame) {
        var token = $("input[name='__RequestVerificationToken']", "#FinishUpdateFinish").val();

        var data = { documentCopyId: documentCopyId, __RequestVerificationToken: token };
        if (storePrivateId) {
            data.storePrivateId = storePrivateId;
        }
        $.post('/Finish/UpdateFinish', data, function (result) {
            if (result) {
                if (result.error) {
                    eGovMessage.notification(result.message, eGovMessage.messageTypes.error);
                } else {
                    eGovMessage.notification('Kết thúc xử lý thành công', eGovMessage.messageTypes.success);
                    $dialog.close();
                    if (frame) {
                        egov.cshtml.home.tab.closeActiveTab();
                    }
                    egov.cshtml.home.reloadData();
                }
            }
        });
    };

    // ===================================================================================================================
    ///<summary>Hàm xử lý chức năng gia hạn xử lý</summary>
    ///<param name="hsmc" type="bool">True - là hồ sơ một cửa, False - là văn bản</param>
    egov.document.addTime = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    /// <summary> Mở form gia hạn</summary>
    /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
    egov.document.addTime.prototype.open = function () {
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#DocumentIndexAddTime").val();

        var settings = {}; // dialog setting
        settings.width = 830;
        //settings.height = 500;
        settings.title = "Gia hạn xử lý";
        settings.buttons = [
            {
                text: "Đồng ý",
                click: function () {
                    var extendedDays = parseInt($('#ExtendedDays').val());
                    if (!isNaN(extendedDays)) {
                        $this.save();
                    } else {
                        eGovMessage.show('Thời gian gia hạn phải là số');
                    }
                }
            },
            {
                text: "Bỏ qua",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        $.ajax({
            type: "POST",
            url: '/Document/IndexAddTime',
            data: { docCopyId: parseInt($this.docCopyId), __RequestVerificationToken: token },
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

    ///<summary>Hàm xử lý nút đồng ý gia hạn</summary>
    egov.document.addTime.prototype.save = function () {
        var addTimes = window.getAddTimeMode(); // lấy nội dung form gia hạn.
        //  var token = $("input[name='__RequestVerificationToken']", "#DocumentUpdateDateAppointed").val();
        //  addTimes.__RequestVerificationToken = token;
        $.ajax({
            type: "POST",
            data: addTimes,
            url: '/Document/UpdateDateAppointed',
            success: function (data) {
                if (data.error) {
                    eGovMessage.notification(data.error, eGovMessage.messageTypes.error);
                }
                else {
                    eGovMessage.notification(data.success, eGovMessage.messageTypes.success);
                    $dialog.close();
                }
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
            }
        });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ lấy lại văn bản</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.retrieve = function (docCopyId, dateCreated) {//frame, 
        //this.frame = frame;
        this.docCopyId = docCopyId;
        this.dateCreated = dateCreated;
    };

    /// <summary> Mở form lấy lại văn bản</summary>
    /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
    egov.document.retrieve.prototype.open = function () {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 500;
        settings.title = "Lấy lại văn bản";
        settings.buttons = [
            {
                text: "Đồng ý",
                click: function () {
                    $this.save($this.docCopyId);
                }
            },
            {
                text: "Bỏ qua",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        var htmlAdd = "<span>Bạn có đồng ý lấy lại văn bản/hồ sơ này không?</span></br><div><a id='xemChiTietLayLaiVanBan' href='#' style='font-weight:bold;'>Xem chi tiết</a></div><div id='viewDetailRetrieve' style='height: 80px; overflow-y:scroll;'></div>";

        $dialog.open(htmlAdd, settings);
        $('#xemChiTietLayLaiVanBan').click(function () {
            detailRetrieve($this.docCopyId, $this.dateCreated);
        });
    };

    ///<summary>Hàm xử lý chức năng lấy lại văn bản(trên contextmenu)</summary>
    egov.document.retrieve.prototype.save = function (documentCopyId) {
        var $this = this;
        var dateCreated = $this.dateCreated;
        var token = $("input[name='__RequestVerificationToken']", "#DocumentUndoTransfering").val();

        $.ajax({
            type: "POST",
            data: { documentCopyId: documentCopyId, dateCreated: dateCreated, __RequestVerificationToken: token },
            url: '/Document/UndoTransfering',
            success: function (data) {
                if (data.error) {
                    eGovMessage.show(data.error);
                }
                else {
                    eGovMessage.notification(data.success, eGovMessage.messageTypes.success);
                    $dialog.close();
                    egov.cshtml.home.reloadData();
                }
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
            }
        });
    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ gửi thông báo</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.sendComment = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    /// <summary> Mở form gửi ý kiến</summary>
    egov.document.sendComment.prototype.open = function (isToolbar) {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 500;
        settings.title = "Gửi ý kiến";
        settings.buttons = [
            {
                text: "Đồng ý",
                click: function () {
                    $this.send($this.docCopyId, isToolbar);
                }
            },
            {
                text: "Bỏ qua",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        var htmlAdd = "<textarea tabindex='1' style='min-height: 0px;width:98%' rows='5' id='newComment' cols='20'></textarea>";
        $dialog.open(htmlAdd, settings);

    };

    ///<summary>Hàm xử lý chức năng gửi ý kiến xử lý</summary>
    egov.document.sendComment.prototype.send = function (documentCopyId, isToolbar) {
        var comment = $("#newComment").val();
        var frameDocument;
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#DocumentSendComment").val();


        if ($this.frame) {
            frameDocument = document.getElementById($this.frame).contentWindow;
        }
        if (comment == '') {
            eGovMessage.show('Bạn chưa nhập nội dung ý kiến!');
            return;
        }
        $.ajax({
            type: "POST",
            data: { documentCopyId: documentCopyId, comment: comment, isToolbar: isToolbar, __RequestVerificationToken: token },
            url: '/Document/SendComment',
            beforeSend: function () {
                eGovMessage.notification('Đang gửi...', eGovMessage.messageTypes.processing, false);
            },
            success: function (data) {
                if (data.error) {
                    eGovMessage.show(data.error);
                }
                else {
                    //Xử lý việc load lại danh sách comment khi gọi chức năng gửi ý kiến từ toolbar(xem chi tiết vb)
                    if (isToolbar && frameDocument) {
                        frameDocument.insertCommentView(data.Data);
                    }
                    eGovMessage.notification('Gửi ý kiến thành công.', eGovMessage.messageTypes.success);
                    $dialog.close();
                }
            },
            error: function (xhr) {
                eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
            }
        });
    };


    ///=========================================================
    // <summary> Class  chỉnh sửa HTML documentcontent </summary>
    egov.document.editHTMLDocContent = function (frame, contentid, doctypeid) {
        this.frame = frame;
        this.contentid = contentid;
        this.doctypeid = doctypeid;
        if (editor)
            editor.destroy();
    };
    // <summary> Mở form chỉnh sủa html</summary>
    var editor;
    egov.document.editHTMLDocContent.prototype.open = function () {
        var $this = this;

        var $content = $('.content[data-contentid=' + $this.contentid + ']', $('#' + $this.frame).contents());
        var settings = {};
        settings.height = 500;
        settings.width = 1000;
        settings.title = "Chỉnh sửa HTML";
        settings.buttons = [
                {
                    text: "Cập nhật",
                    click: function () {
                        $this.update($this.frame, $content);
                        $dialogEdit.dialog('close');
                    }
                },
                {
                    text: "Đóng",
                    click: function () {
                        if (editor)
                            editor.destroy();
                        $dialogEdit.dialog('close');
                    }
                }
        ];
        var $dialogEdit = $('<div id="editHTML"></div>');
        if ($content.html() == null || $content.html() == "") {
            var $html;
            var frame = $this.frame;
            var token = $("input[name='__RequestVerificationToken']", "#DocumentGetFormContent").val();

            $.ajax({
                url: "/Document/GetFormContent",
                type: "POST",
                data: { contentId: $this.contentid, isCreate: false, doctypeId: $this.doctypeid, __RequestVerificationToken: token },
                success: function (result) {
                    $dialogEdit.html(result).appendTo('body');
                    $dialog.openexist($dialogEdit, settings, null, null, true);
                    editor = CKEDITOR.replace(
                        $dialogEdit[0],
                        {
                            height: 356,
                            toolbar: "Basic"
                        }
                    );
                }
            });
        } else {
            $dialogEdit.html($content.html()).appendTo('body');
            $dialog.openexist($dialogEdit, settings, null, null, true);
            editor = CKEDITOR.replace(
                $dialogEdit[0],
                {
                    height: 356,
                    toolbar: "Basic"
                }
            );
        }

        //load nội dung của form chỉnh sửa
    };

    ///summary> Cập nhật Hml đã chỉnh sửa</summary>
    egov.document.editHTMLDocContent.prototype.update = function (frame, $content) {
        var $this = $(this);
        var newContentHTML = CKEDITOR.instances.editHTML.getData();
        $content.html(newContentHTML);
        eGovMessage.notification('Chỉnh sửa nội dung thành công!.', eGovMessage.messageTypes.success);

        //huy editor
        if (editor)
            editor.destroy();
    };

    egov.document.editForm = function (frame, contentId, doctypeId, contentName, isMain) {
        this.frame = frame;
        this.contentId = contentId;
        this.doctypeId = doctypeId;
        this.contentName = contentName;
        this.isMain = isMain;
        if (editor)
            editor.destroy();
    };

    egov.document.editForm.prototype.openEditHtmlForm = function () {

    };

    egov.document.editForm.prototype.open = function () {
        var $this = this;
        var frame = $this.frame;
        var token;
        var $content = $('.content[data-contentid=' + $this.contentId + ']', $('#' + $this.frame).contents());
        var formModel = $content.siblings("#Contents").val();
        if (formModel == null || formModel == "") {
            token = $("input[name='__RequestVerificationToken']", "#DocumentGetFormContent").val();
            $.ajax({
                url: "/Document/GetFormContent",
                type: "POST",
                data: { contentId: $this.contentId, isCreate: false, doctypeId: $this.doctypeId, __RequestVerificationToken: token },
                success: function (result) {
                    var form = JSON.parse(result);
                    $this.formTmp = result;
                    var itemModel = {
                        collections: form.JssCatalog,
                        schema: form.JssForm,
                        formid: form.FormId,
                        maxRow: form.MaxRow
                    };
                    $this.model = itemModel;
                    loadForm(itemModel, $this, false);
                }
            });
        }
        else {
            token = $("input[name='__RequestVerificationToken']", "#DocumentParseDynamicFormContent").val();

            $.ajax({
                url: "/Document/ParseDynamicFormContent",
                type: "POST",
                data: { json: formModel, isFormContent: false, __RequestVerificationToken: token },
                success: function (result) {
                    var form = JSON.parse(result);
                    $this.formTmp = result;
                    var itemModel = {
                        collections: form.JssCatalog,
                        schema: form.JssForm,
                        formid: form.FormId,
                        maxRow: form.MaxRow
                    };
                    $this.model = itemModel;
                    loadForm(itemModel, $this, false);
                }
            });
        }
    };

    egov.document.editForm.prototype.updateDynamicForm = function () {
        if (!validateAll()) {
            return "";
        }

        var formTmp = this.formTmp;
        if (formTmp != "") {
            var json = JSON.parse(formTmp);
            var formJson = {};
            var docFieldJson = eForm.database.JsonSerialize3(json.FormId);
            formJson.FormId = json.FormId;
            formJson.GlobalCode = json.GlobalCode;
            formJson.Description = json.Description;
            formJson.DocFieldJson = JSON.parse(docFieldJson);

            var form = {};
            form.Content = JSON.stringify(formJson);
            form.ContentName = this.contentName;
            form.FormTypeId = 2;
            form.IsMain = this.isMain;
            var docContentId = parseInt(this.contentId) != this.contentId ? 0 : this.contentId;
            form.DocumentContentId = docContentId;
            var $content = $('.content[data-contentid=' + this.contentId + ']', $('#' + this.frame).contents());
            $content.siblings("#Contents").val(JSON.stringify(form));
            return JSON.stringify(form);
        }
        return "{}";
    };

    egov.document.editForm.prototype.openViewDynamicForm = function (json) {
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#DocumentParseDynamicFormContent").val();

        $.ajax({
            url: "/Document/ParseDynamicFormContent",
            type: "POST",
            data: { json: json, isFormContent: true, __RequestVerificationToken: token },
            success: function (result) {
                var form = JSON.parse(result);
                $this.formTmp = result;
                var itemModel = {
                    collections: form.JssCatalog,
                    schema: form.JssForm,
                    formid: form.FormId,
                    maxRow: form.MaxRow
                };
                $this.model = itemModel;
                loadForm(itemModel, $this, true);
            }
        });
    };

    var loadForm = function (formModel, editFormObject, isView) {
        if (isView == undefined) {
            isView = false;
        }
        var settings = {};
        settings.height = 500;
        settings.width = 850;
        settings.title = editFormObject.contentName;
        if (!isView) {
            settings.buttons = [
                {
                    text: "Cập nhật",
                    click: function () {
                        var form = editFormObject.updateDynamicForm();
                        if (form != "") {
                            $dialogEdit.dialog('close');
                            var frame = document.getElementById(editFormObject.frame).contentWindow;
                            frame.egov.cshtml.document.rebindForm(form);
                        }
                    }
                },
                {
                    text: "Đóng",
                    click: function () {
                        $dialogEdit.dialog('close');
                    }
                }
            ];
        }
        var $dialogEdit = $('<div id="editHTML"></div>');
        $dialogEdit.addClass("pnl_root sub_pnl_root").attr("id", "div" + formModel.formid);
        eForm.Lib.Init();
        $dialog.openexist($dialogEdit, settings, null, null, true);
        bindForm(formModel, "div" + formModel.formid, isView);
    };

    var bindForm = function (model, divRoot, isView) {
        // Khởi tạo efTools
        eForm.efTools.init(null, divRoot, model.formid);
        // Tạo danh mục catatalog
        fformModel.fromCatalog(model.collections); // egate.compiler.js
        // Chuẩn bị view cho form động + tạo database cho form động từ model.schema
        if (isView) {
            eForm.efTools.ViewForm(model.schema, model.maxRow); // eForm.Tool.js
        }
        else {
            eForm.efTools.LoadForm(model.schema, model.maxRow); // eForm.Tool.js
        }

        // Chuẩn bị model cho form động
        var partModel = fformModel.fromSchema(eForm.database.GetAll(model.formid), model.formid); // egate.compiler.js
        $.extend(fformModel, partModel);
        // Dùng knockoutjs gắn view + model vào với nhau để hiển thị lên web.
        ko.applyBindings(fformModel, document.getElementById(divRoot));
    };

    // ===================================================================================================================
    // Xử lý các ý kiến thường dùng
    egov.document.commonComments = function () {
        this.model = [];
        this.commonComments = [];
        this.tmp = $("<div><span onclick='egov.cshtml.document.setCommonSelected(this)' ondblclick='egov.cshtml.document.insertCommonContent($(this))' style='display: inline-table; width: 504px;'>${Content}</span><span title='Xóa' onclick='egov.cshtml.document.deleteCommonComment(${CommonCommentId}, this)' style='color: red; cursor: pointer; display: none'>X</span></div>");
    };

    egov.document.commonComments.prototype.open = function () {
        var $this = this;
        var token = $("input[name='__RequestVerificationToken']", "#GetCommonComments").val();
        var $addCommentForm = $("<div>");
        var htmlDialog = "<div id='divComments' style='padding:5px !important;'>";
        $.ajax({
            url: '../GetCommonComments',
            data: { __RequestVerificationToken: token },
            type: "Post",
            success: function (data) {
                $this.commonComments = data;
                tmp.tmpl($this.commonComments).appendTo($addCommentForm);
                htmlDialog += "<div class='addComment' style='overflow:auto;height:100px'>" + $addCommentForm.html() + "</div></br>";
                htmlDialog += "<fieldset><legend>Thêm ý kiến</legend><textarea tabindex='1' style='min-height: 0px;width:100%' rows='2' id='newCommonComment' cols='20'></textarea><input type='button' id='addCommonComment' value='Thêm ý kiến thường dùng' onclick='egov.cshtml.document.addCommonComment(this)' /></fieldset></div>";
                $(htmlDialog).dialog({
                    title: "Các ý kiến thường dùng",
                    modal: true,
                    resizable: false,
                    draggable: false,
                    width: 540
                });

                $('.addComment').niceScroll();
            }
        });
    };

    egov.document.commonComments.prototype.addCommon = function () {

    };


    // ===================================================================================================================
    /// <summary> Class hỗ trợ cấp phép</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.license = function (docCopyId, businessName, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
        this.businessName = businessName;
    };

    /// <summary> Mở form cấp phép</summary>
    /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
    egov.document.license.prototype.open = function () {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 510;
        settings.height = 474;
        settings.title = "Thông tin giấy phép";
        settings.buttons = [
            {
                text: "Cập nhật",
                click: function () {
                    $this.add();
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
            url: '/BusinessLicense/Create',
            data: { docCopyId: $this.docCopyId, businessName: $this.businessName },
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

    /// <summary> Thêm mới giấy phép</summary>
    egov.document.license.prototype.add = function () {
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#BusinessLicenseCreateLicense").val();

        var license = $("#addbusinesslicense").find(":input,select").serializeObject();
        if (license.LicenseCode == "") {
            eGovMessage.show('Bạn chưa nhập mã giấy phép.', 'Cấp phép');
            return;
        }
        if (license.LicenseNumber == "") {
            eGovMessage.show('Bạn chưa nhập số giấy phép.', 'Cấp phép');
            return;
        }

        if (license.BusinessId == 0) {
            eGovMessage.show('Doanh nghiệp chưa tồn tại trên hệ thống. Vui lòng thêm trước khi cấp phép.');
            $(".add-business").show();
            return;
        }

        $.ajax({
            type: "POST",
            url: '/BusinessLicense/CreateLicense',
            dataType: "json",
            traditional: true,
            data: {
                docCopyId: $this.docCopyId,
                "licenseinfo": JSON.stringify(license),
                __RequestVerificationToken: token
            },
            success: function (data) {
                $dialog.close();
            },
            error: function (xhr) {
                messageTemp({
                    message: xhr.statusText, type: 'error'
                });
            }
        });

    };

    egov.document.business = function (docCopyId, businessTypeId, businessName, frame) {
        this.docCopyId = docCopyId;
        this.businessName = businessName;
        this.businessTypeId = businessTypeId;
        this.frame = frame;
    };

    egov.document.business.prototype.open = function () {
        $dialog.close();
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#BusinessesCreateBusiness").val();

        var settings = {}; // dialog setting
        settings.width = 550;
        settings.height = 700;
        settings.title = "Thêm mới doanh nghiệp";
        settings.buttons = [
            {
                text: "Thêm mới",
                click: function () {
                    $this.add();
                }
            },
            {
                text: "Đóng",
                click: function () {
                    $dialog.close();
                    var docCopyId = $this.docCopyId;
                    var license = new egov.document.license(docCopyId, null, null, token);
                    license.open(true);
                }
            }
        ];
        $.ajax({
            url: '/Businesses/CreateBusiness',
            data: { businessName: $this.businessName, businessTypeId: $this.businessTypeId },
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

    /// <summary> Thêm mới doanh nghiệp</summary>
    egov.document.business.prototype.add = function () {
        var $this = this;
        var frame = $this.frame;
        var token = $("input[name='__RequestVerificationToken']", "#BusinessesCreate").val();

        var bus = $("#addbusiness").find(":input,select").serializeObject();
        if (bus.BusinessCode == "") {
            eGovMessage.show('Bạn chưa nhập số ĐKKD.', 'Doanh nghiệp');
        }
        if (bus.IssueCodeby == "") {
            eGovMessage.show('Bạn chưa nhập nơi cấp số ĐKKD.', 'Doanh nghiệp');
        }
        if (bus.Address == "") {
            eGovMessage.show('Bạn chưa nhập địa chỉ doanh nghiệp.', 'Doanh nghiệp');
        }
        if (bus.UserName == "") {
            eGovMessage.show('Bạn chưa nhập tên người đại diện doanh nghiệp.', 'Doanh nghiệp');
        }

        $.ajax({
            type: "POST",
            url: '/Businesses/Create',
            dataType: "json",
            traditional: true,
            data: {
                "businessinfo": JSON.stringify(bus),
                __RequestVerificationToken: token
            },
            success: function (data) {
                $dialog.close();
                var docCopyId = $this.docCopyId;
                var businessName = $this.businessName;
                var license = new egov.document.license(docCopyId, businessName, null, token);
                license.open(true);
            },
            error: function (xhr) {
                messageTemp({
                    message: xhr.statusText, type: 'error'
                });
            }
        });

    };

    // ===================================================================================================================
    /// <summary> Class hỗ trợ lưu sổ phát hành nội bộ</summary>
    /// <param name="frame" type="String"> Tên frame (form view hs) đang thao tác</param>
    egov.document.luusophathanhnoibo = function (docCopyId, frame) {
        this.frame = frame;
        this.docCopyId = docCopyId;
    };

    /// <summary> Mở form lưu sổ phát hành nội bộ</summary>
    /// <param name="isContext" type="bool">True - là mở form trên contextmenu, False - là mở form trên toolbar</param>
    egov.document.luusophathanhnoibo.prototype.open = function (isFromToolbar) {
        var $this = this;
        var settings = {}; // dialog setting
        settings.width = 420;
        settings.height = 435;
        settings.title = "Lưu sổ phát hành nội bộ";
        settings.buttons = [
            {
                text: "Lưu",
                click: function () {
                    $this.add();
                }
            },
            {
                text: "Bỏ qua",
                click: function () {
                    $dialog.close();
                }
            }
        ];
        $.ajax({
            url: '/Transfer/GetPublishPrivate',
            data: { docCopyId: $this.docCopyId },
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

    /// <summary> Lưu sổ phát hành nội bộ</summary>
    egov.document.luusophathanhnoibo.prototype.add = function () {
        getPublishPrivateConsult();
        var usersConsult = egov.document.getUsersConsult(window.targetForComments, window.allUserDepartments);
        var $this = this;
        var publish = { StoreId: 0, CodeId: 0, Code: '', TotalPage: 1, SecurityId: 1, TotalCopy: 1 };
        publish.StoreId = $('#dialogpublish #StoreId').val();
        publish.CodeId = $('#dialogpublish #CodeId').val();
        publish.Code = $('#dialogpublish #Code').val();

        var data = {
            documentCopyId: $this.docCopyId,
            publishinfo: JSON.stringify(publish),
            usersConsult: usersConsult
        };
        $.ajax({
            type: "POST",
            url: '/Transfer/TransferPrivatePublish',
            dataType: "json",
            traditional: true,
            data: data,
            success: function (data) {
                $dialog.close();
                egov.cshtml.home.tab.closeActiveTab();
                if (data.error) {
                    eGovMessage.show(data.error);
                }
                else {
                    // CuongNT - 20042014: Thêm để load lại danh sách văn bản
                    egov.cshtml.home.reloadData();
                }
            },
            error: function () {

            }
        });
    };

    //#region Private Methods

    function deActiveDialogButton() {
        $(".ui-dialog .ui-dialog-buttonpane .btn-submit").addClass("disabled");
    }

    function activeDialogButton() {
        $(".ui-dialog .ui-dialog-buttonpane .btn-submit").removeClass("disabled");
    }

    // Kiểm tra dữ liệu nhập đầu vào
    var validateAll = function () {
        var isValid = true;
        var invalidObj = null;
        // Form động
        $('.ffield').each(function () {
            $(this).blur();
        });

        $('.cssErr').each(function () {
            if ($(this).css('display') != 'none' && isValid) {
                invalidObj = $(this);
                isValid = false;
            }
        });
        if (!isValid) {
            $('#' + invalidObj.attr('controlValidate')).focus();
            return false;
        }
        return true;
    };

    // Lấy toàn bộ dữ liệu đã nhập trên form động
    var jsonSerializeAll = function (frame, contentId) {
        var $content = $('.content[data-contentid=' + contentId + ']', $('#' + frame).contents());
        var formTmp = formObj.find("#ContentFields").val();
        if (formTmp != "") {
            var json = JSON.parse(formTmp);
            var formJson = "{";
            var docFieldJson = eForm.database.JsonSerialize3(json.FormId);
            formJson += "\"FormId\": \"" + json.FormId + "\",";
            formJson += "\"GlobalCode\":\"" + json.GlobalCode + "\",";
            formJson += "\"Description\":\"" + json.Description + "\",";
            formJson += "\"DocFieldJson\":" + docFieldJson;
            formJson += "}";

            return JSON.stringify(formJson);
        }
    };
    //#endregion
})(window.jQuery, new dialogAdapter(), window._, window.egov = window.egov || {})