define([
    'jquery',
], function ($) {
    var PublishmentView = Backbone.View.extend({
        initialize: function (options) {
            this.frame = options.frame;
            this.docCopyId = options.docCopyId;
            this.open();
        },
        open: function () {
            var _this = this;
            var settings = {}; // dialog setting
            var dialog = new dialogAdapter();
            //var frame = document.getElementById(_this.frame).contentWindow;
            //var plugin = frame.egov.cshtml.document.getPlugin();
            var allFileOpened = getFileOpened();
            $(document).off('checkModifySuccess');
            $(document).on('checkModifySuccess', function (e, modifiedFiles) {
                settings.width = 790;
                settings.height = 615;
                settings.title = "Lưu sổ phát hành";
                settings.buttons = [
                    {
                        text: "Lưu",
                        click: function () {
                            _this.publish(modifiedFiles);
                        }
                    },
                    {
                        text: "Bỏ qua",
                        click: function () {
                            dialog.close();
                        }
                    }
                ];
                var publishmentDialog = $('<div id="publishDialog"></div>');
                publishmentDialog.appendTo('body');
                $.ajax({
                    url: '/Transfer/GetPublish',
                    data: { docCopyId: _this.docCopyId },
                    beforeSend: function () {
                        var $loading = '<img src="../../Content/Images/ajax-loader.gif" />';
                        publishmentDialog.html($loading);
                        dialog.openexist(publishmentDialog, settings);
                    },
                    success: function (result) {
                        publishmentDialog.html(result);
                        dialog.openexist(publishmentDialog, settings);
                    }
                });
            });
            if (allFileOpened.length > 0) {

                egov.plugin.closeAllOfficeDocuments();
                if (checkOtherProgramOpenFile(egov.plugin, allFileOpened)) {
                    confirmSaveAttachment(allFileOpened, {}, egov.plugin);
                }
            } else {
                $(document).trigger('checkModifySuccess', {});
            }
        },

        publish: function (modifiedFiles) {
            var usersConsult = getUsersConsult(egov.common.targetForComments, egov.database.allUserDepartments);
            var $this = this;
            var token = $("input[name='__RequestVerificationToken']", "#TransferTransferPublish").val();
            var dialog = egov.views.base.dialog;
            var publish = $("#DocumentPublish").find(":input,select").serializeObject();
            if (publish.hdfaddressIds == "") {
                egov.message.show('Bạn chưa chọn nơi phát hành văn bản.', 'Phát hành văn bản');
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
                    dialog.close();
                    egov.cshtml.home.tab.closeActiveTab();
                    // CuongNT - 20042014: Thêm để load lại danh sách văn bản
                    egov.cshtml.home.reloadData();
                },
                error: function ()
                { }
            });
        },

    });


    var itemsDg = {
        jobtitlesItem: "jobtitlesItem",//chức vụ
        jobtitlesDeptItem: "jobtitlesDeptItem",//phòng ban - chức vụ
        allUsers: "allUsers", //tất cả người dùng
        deptItem: "deptItem",//phong ban
        userItem: 'userItem', // Cán bộ
    };
    var getUsersConsult = function (listItemsDg, allUserDept) {
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

    var checkOtherProgramOpenFile = function (plugin, allFileOpened) {
        var regex = /(.doc|.docx|.xls|.xlsx|.ppt|.pptx)$/i;
        for (var i = 0; i < allFileOpened.length; i++) {
            if (regex.test(allFileOpened[i].name) && plugin.readFileBase64(allFileOpened[i].name, 0, -1) == '') {
                egov.message.show('Bản phải đóng các chương trình đang mở tệp đính kèm trước khi lưu');
                return false;
            }
        }
        return true;
    };

    var getFileOpened = function () {
        var allFiles = [];
        $("#tblFiles tbody tr").each(function (i, item) {
            // Nếu không phải là các file đã bị loại bỏ
            var $item = $(item);
            if (!$item.hasClass('disabletemp-attachment')) {
                // Và là các file đã được mở bằng plugin để chỉnh sửa --> sẽ có attr('data-file')
                if ($item.attr('data-file')) {
                    var id = $item.hasClass('temp-attachment') ? $item.attr("id").replace("temp", "") : $item.attr("id").replace("attachment", "");
                    allFiles.push({ id: id, name: $item.attr('data-file'), value: $item.attr('data-filename') });
                }
            }
        });
        return allFiles;
    };

    return PublishmentView;
});