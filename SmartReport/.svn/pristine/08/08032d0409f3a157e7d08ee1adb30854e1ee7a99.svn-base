define([
    egov.template.transfer.transferMobile,
    egov.template.transfer.transferItemMobile
],
function (Template, UserItem) {
    "use strict";

    var _resource = egov.resources.document.transfer;

    //#region View

    /// <summary>Đối tượng thể hiện form bàn giao văn bản</summary>
    var Transfer = Backbone.View.extend({
        el: "#tranfer",
        className: 'transfer-form row',
        template: Template,
        model: egov.viewModels.actionUserList,
        events: {
            "change .mdl-checkbox__input": "_selectUser",
            "click .result-view": "_focusToFilter",
            "click #btn_transfer_extends": "toggle_transferExt",
            "click .btn-clear-dg-tb": "clearUserExt",
        },

        initialize: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            var that = this;
            that.allUser = []; //danh sach tat ca user
            // Đối tượng tổng hợp danh sách nhận bàn giao
            that.destination = {};
            that.$(".mdl-dialog__content").append(that.template);

            that.$listUser = that.$('.listUsers');
            that.$result = that.$('.result-view');
            that.$filter = that.$('#searchUser');

            that.model.on("reset", function (model) {
                that._clear();

                if (model.length === 0) {
                    that.$listUser.append($('<li>').addClass('mdl-list__item').text(_resource.noUserByAction));
                    return;
                }

                that.$listUser.append($.tmpl(UserItem, model.toJSON()));
                egov.mobile.upgradeMaterial(that.$(".mdl-button, .mdl-checkbox"));

                if (model.length === 1) {
                    that.$listUser.find(".mdl-checkbox").addClass("is-checked");
                    model.models[0].set("isSelected", true);
                    model.models[0].set("isMainProcess", true);
                }

                that._filterUser();
            });

            that.model.on("change:isMainProcess", function (changedModel) {
                var isMainProcess = changedModel.get("isMainProcess") == true;

                if (isMainProcess) {
                    that.mainProcess && that.mainProcess.get("value") != changedModel.get("value") && that.mainProcess.set("isMainProcess", 'false');
                    that.mainProcess = changedModel;
                    changedModel.set("isCoProcess", false);
                    that.$(".searchUser").before("<span class='user-result' id='" + changedModel.get("value") + "'>" + changedModel.get("fullname") + "</span>");

                    that.$('.user-xlc').text("Xử lý chính: " + changedModel.get('fullname'));
                } else {
                    that.mainProcess = null;
                    that.$result.find(".user-result#" + changedModel.get("value")).remove();
                }
            });

            that.model.on("change:isCoProcess", function (changedModel) {
                var isCoProcess = changedModel.get("isCoProcess") == true;
                if (isCoProcess) {
                    changedModel.set("isMainProcess", false);
                    that.$(".searchUser").before("<span class='user-result' id='" + changedModel.get("value") + "'>" + changedModel.get("fullname") + "</span>");
                } else {
                    that.$result.find(".user-result#" + changedModel.get("value")).remove();
                }
            });

            return this;
        },

        /// <summary>Hiển thị form bàn giao</summary>
        render: function (option) {
            this.UserIdDxl = [];
            this.UserIdDg = [];
            this.UserIdTb = [];
            this.$("#user_transfer_extends").hide();
            this._clear();

            if (!(option.action instanceof egov.models.action)) {
                option.action = new egov.models.action(option.action);
            }

            this.action = option.action;
            this.document = option.document;
            this.callback = option.callback;

            // Chuyển cho người nhận theo hướng chuyển đặc biệt hoặc mở form chuyển
            if (this.action.get('isSpecial')) {
                this._transferSpecial();
            }
            else {
                this._open();
            }
        },

        serialize: function () {
            /// <summary>
            /// Serialize form bàn giao để lấy ra destination
            /// </summary>
            var mainProcess = this.mainProcess;

            var coProcess = this.model.select(function (user) {
                return user.get('isCoProcess') && !user.get('isMainProcess');
            });

            if (mainProcess === undefined && coProcess.length === 1) {
                mainProcess = coProcess;
                coProcess = [];
            }

            if (mainProcess === undefined) {
                // trên mobile bỏ qua trường hợp toàn xử lý chính.
                return null;
            }

            // Lấy destination
            var transferType = egov.enum.transferType,
                targetComments = [],
                userIdsDxl = [],
                isDxl = false,
                isThongbao = false;

            this.destination.UserIdXlc = mainProcess.get('id');

            targetComments.push({
                label: mainProcess.get('fullname'),
                department: mainProcess.get("department"),
                value: '',
                type: transferType.xulychinh,
                isMobile: true
            });

            //Lấy danh sách đồng xử lý
            userIdsDxl = _(coProcess).chain().pluck('id').value();

            //Thiết lập hiển thị danh sách ngưởi đồng xử lý
            //Nếu danh sách đxl >0 mới thêm vào, nếu ko thì bỏ qua
            if (coProcess.length > 0) {
                var coProcessFullname = _.map(coProcess, function (p) { return p.get('fullname'); });
                var coProcessDeparments = _.map(coProcess, function (p) { return p.get('department'); });
                coProcessDeparments = _.uniq(coProcessDeparments, function (p) { return p; });

                targetComments.push({
                    label: coProcessFullname.join(', '),
                    department: coProcessDeparments.join(', '),
                    value: '',
                    type: transferType.dongxuly,
                    isMobile: true
                });
            }

            this.destination.UserIdsDxl = userIdsDxl;
            this.destination.CurrentNodeId = this.action.get('currentNodeId');
            //this.destination.IsThongbao = isThongbao;
            //this.destination.IsDxl = isDxl;
            this.destination.IsAttachYk = false;
            //this.destination.UserIdsDg = []; // Xử lý thông báo sau nếu cần
            //this.destination.UserGs = [];
            this.destination.NextNodeId = this.action.get('nextNodeId');
            this.destination.CurrentNodeId = this.action.get('currentNodeId');
            this.destination.WorkflowId = this.action.get('workflowId');
            this.destination.NewDocTypeId = undefined;
            this.destination.TargetComment = JSON.stringify(targetComments);
            this.destination.ActionId = this.action.get('id');

            this.destination.IsThongbao = (this.UserIdTb.length > 0) ? true : false;
            this.destination.IsDxl = (this.UserIdDg.length > 0) ? true : false;
            this.destination.UserTb = _.pluck(this.UserIdTb, "value");
            this.destination.UserIdsDxl = _.pluck(this.UserIdDxl, "value");
            this.destination.UserIdsDg = _.pluck(this.UserIdDg, "value");

            return this.destination;
        },

        //#region Private Methods

        _clear: function () {
            /// <summary>
            /// Chuyển các giá trị về mặc định
            ///</summary>
            this.model = egov.viewModels.actionUserList;
            this.$el.css("display", "block");
            this.$listUser.empty();
            this.destination = {};

            //reset lai nguoi xu ly chinh
            if (this.mainProcess) {
                this.mainProcess = undefined;
            }
        },

        _open: function () {
            /// <summary>
            /// Mở form chuyển
            /// </summary>
            var that,
                actionId,
                workflowId,
                documentCopyId;

            that = this;
            actionId = that.action.get('id');
            workflowId = that.action.get('workflowId');
            documentCopyId = that.document.model.get('DocumentCopyId');

            that._renderDialog();
            that._focusToFilter();

            egov.request.getUserByAction({
                data: { actionId: actionId, workflowId: workflowId, documentCopyId: documentCopyId },
                success: function (result) {
                    that._sortUsersByAction(result, function (userActions) {
                        that.model.reset(userActions);
                    });
                }
            });
        },

        _sortUsersByAction: function (userByActions, success) {
            /// <summary>
            /// Săp xếp danh sách người nhận theo hướng chuyển:
            /// - Người vừa gửi.
            /// - Người khởi tạo.
            /// - Người cùng phòng ban.
            /// - Người còn lại.
            /// Những người còn lại sort theo abc.
            /// </summary>
            /// <param name="userByActions">Danh sách người nhận theo action.</param>
            /// <returns type=""></returns>
            var that,
                result,
                currentUser;

            if (userByActions.error) {
                egov.callback(success, []);
                return;
            }

            if (userByActions.length === 1) {
                egov.callback(success, userByActions);
                return;
            }

            that = this;
            result = [];

            currentUser = _.find(userByActions, function (user) {
                return user.value === egov.setting.userId;
            });

            userByActions = _.reject(userByActions, function (u) {
                return u.value === egov.setting.userId;
            });

            result = userByActions;
            result = _.sortBy(result, function (user) {
                return user.name;
            });

            if (currentUser) {
                result.push(currentUser);
            }

            egov.callback(success, result);
        },

        _filterUser: function () {
            var that = this;
            that.$filter.on("change keyup", function (event) {
                var tearm = that.$filter.val().toLowerCase();
                if (tearm === "") {
                    that.$listUser.find("li").show();
                    return;
                }

                that.$listUser.find("li").each(function () {
                    var userInfo = $(this).attr("data").toLowerCase();
                    if (userInfo.indexOf(tearm) < 0) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
            });
        },

        _renderDialog: function () {
            var that = this;

            dialogPolyfill.registerDialog(that.el);
            that.el.showModal();
            $(that.el).css({ "width": "75%",'padding-top': "20px" })
            $(that.el).find(".mdl-dialog__title").text("Chuyển báo cáo");

            that.$(".user-receiveds .user-result").remove();

            that.$(".btnSend").off("click").click(function (e) {
                e.preventDefault();
                that.$("button").attr("disabled", "disabled");
                egov.mobile.showProcessBar();
                that._send(function () {
                    that._transferComplete();
                });

                // -- New Code 02/01 -- Dùng để clear các account đã chọn -- //
                $(".user-result").remove();

                return;
            });

            that.$(".close").click(function () {
                that.$el.css("display", "none");
                $(".user-result").remove();
                that.el.close();
                return;
            });

            egov.mobile.upgradeMaterial(that.$(".mdl-checkbox, .mdl-radio"));

            return;
        },

        _transferSpecial: function () {
            /// <summary>
            /// Chuyển văn bản theo hướng chuyển đặc biệt
            /// </summary>
            var that = this;
            this.destination.UserIdXlc = this.action.get('userIdNext');
            this.destination.UserIdsDxl = [];
            this.destination.IsThongbao = false;
            this.destination.IsDxl = false;
            this.destination.IsAttachYk = false;
            this.destination.UserIdsDg = [];
            this.destination.NextNodeId = this.action.get('nextNodeId');
            this.destination.WorkflowId = this.action.get('workflowId');
            this.destination.TargetComment = "[]";

            this._transferEdit(function () {
                that._transferComplete();
            });
        },

        _send: function (completeCallback) {
            /// <summary>
            /// Bàn giao văn bản
            /// </summary>
            var that = this;
            var destination = that.serialize();
            if (!destination) {
                egov.mobile.showStatus(_resource.noUser);
                completeCallback();
                return;
            }

            if (that.document.isCreate) {
                that._transferCreate(completeCallback);
            }
            else {
                that._transferEdit(completeCallback);
            }
        },

        _transferCreate: function (completeCallback) {
            var doc = this.document.serialize();
            var that = this;
            if (this.storeId && this.code) {
                doc.StoreId = this.storeId;
                doc.DocCode = this.code;
                doc.CodeId = this.codeId;
            }
            doc.TransferType = 1;
            doc.CategoryBusinessId = 4;
            doc.CategoryId = 1;
            var selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });
            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });
            removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });
            var modifiedFiles = this.document.attachments.modifiedFiles;

            // Cập nhật nội dung file đã sửa với những file vừa upload lên.
            // Đồng thời xóa file đó trong danh sách file đang chỉnh sửa.
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });

            //Dự kiến chuyển
            var docContentMobile = this.document.$formTemplate.serializeForm();

            // egov.message.processing(egov.resources.common.transfering, false);
            egov.request.transfer({
                data: {
                    "doc": JSON.stringify(doc),
                    "destination": this.destination ? JSON.stringify(this.destination) : "",
                    "files": JSON.stringify(selectedFiles),
                    "modifiedFiles": JSON.stringify(modifiedFiles),
                    "removeAttachmentIds": removeFiles,
                    "storePrivateId": 0,
                    "destinationPlan": "",
                    "jsonFile": JSON.stringify(docContentMobile)
                },
                success: function (data) {
                    // transferSuccess(data);
                },
                error: function () {
                    // transferError();
                },
                complete: function () {
                    completeCallback();
                }
            });
        },

        _transferEdit: function (completeCallback) {
            /// <summary>
            /// Bàn giao thông thường
            /// </summary>
           
            var docContentMobile = this.document.$formTemplate.serializeForm();
            var that = this,
                key = "Normal",
                doc,
                destinationPlan,
                selectedFiles,
                docCopyId = that.document.model.get("DocumentCopyId"),
                isOnlyDxls,
                newContent,
                newCompendium;
            var modifiedFiles = this.document.attachments.modifiedFiles;
            var transferSuccess = function (data) {
                if (data.success) {
                    egov.mobile.showStatus(_resource.transferSuccess);
                    that._reloadDocumentList();
                } else {
                    egov.mobile.showStatus(data.error);
                }

                completeCallback();
            };

            var transferError = function () {
                egov.mobile.showStatus(_resource.transferError);
            };

            isOnlyDxls = false;
            selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });

            egov.request.lightTransfer({
                data: {
                    "documentCopyId": docCopyId,
                    "destination": this.destination ? JSON.stringify(this.destination) : "",
                    "comment": this.document.$("textarea[name=Comment]").val(),
                    "files": selectedFiles ? JSON.stringify(selectedFiles) : "",
                    "newContent": "",
                    "newCompendium": "",
                    "modifiedFiles": _.isEmpty(modifiedFiles) ? "" : JSON.stringify(modifiedFiles),
                    "jsonFile": JSON.stringify(docContentMobile)
                },
                success: function (data) {
                    if (data.error) {
                        transferError();
                        return;
                    }
                    transferSuccess(data);
                },
                error: function (xhr) {
                    console.log(xhr);
                    transferError();
                },
                complete: function () {
                    completeCallback();
                }
            });
        },

        _transferComplete: function () {
            var that = this;
            that.$("button").removeAttr("disabled");

            egov.mobile.hideDetailPage();
            egov.mobile.hideProcessBar();

            that.document.$el.remove();

            if (that.el) {
                // xử lý dialog
                that.$el.css("display", "none");
                that.el.close();
            }
        },

        _reloadDocumentList: function () {
            /// <summary>
            /// Đồng bộ danh sách văn bản hiện tại
            /// </summary>
            /// <param name="removeDocumentCopyIds">Danh sách các documentCopyId đã chuyển khỏi danh sách hiện tại</param>
            egov.pubsub.publish("tree.reloadSelected");
            // egov.mobile.reloadDocumentList();
        },

        _selectUser: function (e) {
            var target = $(e.target).closest(".mdl-checkbox__input");
            var tranferItemCheckbox = target.parent();
            var userId = target.parents('.user-received-item').attr("value");
            var isMainProcess = target.is(".isXlc");
            var isChecked = target.prop("checked") == true;

            var userReceived = this.model.detect(function (u) { return u.get("value") == userId; });

            if (!userReceived) {
                return;
            }

            // Nếu checked hiện tại là xlc thì bỏ các xử lý chính khác.
            if (isMainProcess && this.mainProcess && (this.mainProcess.get("value") != userId)) {
                this.$('#list-checkbox-xlc-' + this.mainProcess.get("value")).attr('checked', false);
                this.$('#list-checkbox-xlc-' + this.mainProcess.get("value")).parent().removeClass('is-checked');
            }

            isMainProcess ? userReceived.set('isMainProcess', isChecked) : userReceived.set('isCoProcess', isChecked);

            // loại bỏ checkbox còn lại khi chuyển dxl, xlc trên cùng user.
            isChecked && tranferItemCheckbox.siblings().removeClass("is-checked")
                        && tranferItemCheckbox.siblings().find('.mdl-checkbox__input').attr('checked', false);

            this._focusToFilter();
            e.preventDefault();
        },

        _focusToFilter: function (e) {
            if (e == undefined) {
                this.$filter.focus();
                return;
            }

            var target = $(e.target).find("input");
            target.focus();
        },

        toggle_transferExt: function () {
            var that = this;
            if (that.allUser.length < 1) {
                that.allUser = egov.setting.allUsers;
                that._binUserAdvance();
            }

            $("#user_transfer_extends").toggle();
        },

        _binUserAdvance: function () {
            var that = this;
            var $userDg = that.$("#txt_searchdg");
            var $usrTb = that.$("#txt_searchtb");
            var $filterUsr = that.$(".txt_filter_allUser");
            if (typeof (eGovApp) !== 'undefined') {
                $filterUsr.focus(function () {  //dieu chinh kich thuoc dialog hop ly de khi focus ban phim k che
                    $(".transfer-dialog").prop("style", "display:block; bottom:0;top:inherit;");
                });
                $("#searchUser").focus(function () {
                    $(".transfer-dialog").prop("style", "display:block;");
                });
            }
            $userDg.autocomplete({
                appendTo: ".box_donggui",
                source: that.allUser,
                select: function (event, ui) {
                    $(this).val('');
                    var userSelect = _.findWhere(that.allUser, { value: ui.item.value });
                    if (userSelect) {
                        if ((that.UserIdDxl.length + that.UserIdDg.length) < 1) {
                            $(".box_donggui .btn-clear-dg-tb").show();
                        }
                        var user = that.model.detect(function (u) {
                            return u.id === userSelect.value;
                        });
                        if (user) {
                            var itemExist = _.findWhere(that.UserIdDxl, { value: ui.item.value });
                            if (!itemExist) {
                                that.UserIdDxl.push(userSelect);
                                $(this).before("<span class='user-result' id='" + ui.item.value + "'>" + ui.item.fullname + ",</span>");
                            }
                        } else {
                            var itemExist = _.findWhere(that.UserIdDg, { value: ui.item.value });
                            if (!itemExist) {
                                that.UserIdDg.push(userSelect);
                                $(this).before("<span class='user-result' id='" + ui.item.value + "'>" + ui.item.fullname + ",</span>");

                            }
                        }
                    }

                    return false;
                    // xác định các phòng ban user thuộc vào
                },
                open: function (event, ui) {
                    var autocomplete = $("#user_transfer_extends .ui-autocomplete");
                    autocomplete.css({ "height": "100px", "top": ($(this).position().top + $(this).innerHeight()) + "px", "overflow-y": "scroll" });
                }
            }).data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu').css("zIndex", "1060");
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };

            $usrTb.autocomplete({
                appendTo: ".box_thongbao",
                source: that.allUser,
                select: function (event, ui) {
                    $(this).val('');
                    var userSelect = _.findWhere(that.allUser, { value: ui.item.value });
                    if (userSelect) {
                        if ((that.UserIdTb.length) < 1) {
                            $(".box_thongbao .btn-clear-dg-tb").show();
                        }
                        var itemExist = _.findWhere(that.UserIdTb, { value: ui.item.value });
                        if (!itemExist) {
                            that.UserIdTb.push(userSelect);
                            $(this).before("<span class='user-result' id='" + ui.item.value + "'>" + ui.item.fullname + ",</span>");
                        }
                    }
                    return false;
                    // xác định các phòng ban user thuộc vào
                },
                open: function (event, ui) {
                    var autocomplete = $("#user_transfer_extends .ui-autocomplete");
                    autocomplete.css({ "height": "100px", "top": "-100px", "overflow": "scroll" });
                }
            }).data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu').css("zIndex", "1060");
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };

        },

        //clear lai user
        clearUserExt: function (e) {
            var target = $(e.target).closest(".btn-clear-dg-tb");
            var typeClear = target.data("target");
            if (typeClear === 'searchUsrDg') {
                $(".searchUsrDg").find("span").remove();
                $(".box_donggui .btn-clear-dg-tb").hide();
                this.UserIdDg = [];
                this.UserIdDxl = [];
            } else if (typeClear === 'searchUsrTb') {
                $(".box_thongbao .btn-clear-dg-tb").hide();
                $(".searchUsrTb").find("span").remove();
                this.UserIdTb = [];
            }
        }

        //#endregion
    });

    //#endregion

    return Transfer;
});