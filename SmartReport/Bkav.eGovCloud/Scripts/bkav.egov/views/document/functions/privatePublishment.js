define(["jstree"],
function () {
    "use strict";

    var _resource = egov.resources.document.publishment;

    /// <summary>Đối tượng form Lưu sổ phát hành</summary>
    var PrivatePublishment = Backbone.View.extend({
        // Load cùng với trang home (trong _Publishment.cshtml)
        el: '#PrivatePublishment',

        events: {
            'click .code-list > li': '_selectCode',
            'click .ddlApprovers > li': '_selectApprover',
            'click .private-anoun .checkbox': 'uncheckPrivateAnoun',
            'blur #Code': '_checkCodeUsed',
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>

            // Danh sách các sổ hồ sơ.
            this.stores = [];
            this.targetComments = [];
            this.selectedDeptIds = [];
            this.$stores = this.$('#StoreId');
            this.$codeId = this.$('#Code');
            this.$code = this.$('.code-list');
            this.$dg = this.$('.dg-view');
            this.$approvers = this.$('.ddlApprovers');
            this.$depts = this.$('#InPlace');
            this.$securities = this.$('#SecurityId');
            this.$datePublish = this.$('#DatePublished');
            this.$privateAnounc = this.$('.private-anoun ul:first');
            return this;
        },

        render: function (option) {
            /// <summary>
            /// Hiển thị form phát hành
            /// </summary>
            /// <param name="option">Các tham số khởi tạo</param>
            /// <returns type=""></returns>
            this._destroyDg();
            this.$privateAnounc.empty();
            this.$(".private-receiveds .table-depts tbody").empty();
            this.$(".main-address ul").empty();
            this.theoLo = false;
            this.document = option.document;
            this.docTypeId = option.docTypeId;
            this.hasDg = option.hasDg;
            this.hasPrivateSaveToStore = option.HasPrivateSaveToStore;
            if (this.hasDg == undefined) {
                this.hasDg = true;
            }
            this.callback = option.callback;
            this.categoryId = this.document.model.get("CategoryId");
            this.documentId = this.document.model.get("DocumentId");
            this.result = new egov.models.publish;

            this.$el.show();
            this.$stores.off("change");
            this.isRePublish = false;

            if (option.isRePublish) {
                this.isRePublish = option.isRePublish;
            }
            this._openDialog();
            return this;
        },

        renderTheoLo: function (option) {
            /// <summary>
            /// Hiển thị form phát hành theo lô
            /// </summary>
            /// <param name="option">Các tham số khởi tạo</param>
            /// <returns type=""></returns>
            this._destroyDg();
            this.$privateAnounc.empty();
            this.$(".private-receiveds .table-depts tbody").empty();
            this.documentCopyIds = option.documentCopyIds;
            this.docTypeId = option.docTypeId;
            this.parent = option.parent;
            this.categoryId = option.categoryId;
            this.hasDg = option.hasDg;
            if (this.hasDg == undefined) {
                this.hasDg = true;
            }
            this.theoLo = true;
            this.callback = option.callback;
            this.result = new egov.models.publish;
            this.$el.empty().show();
            this.$stores.off("change");
            this._openDialog();
            return this;
        },

        _openDialog: function () {
            var that = this;
            var buttons = [];
            if (that.isRePublish) {
                buttons = [{
                    text: "Phát hành tiếp",
                    className: 'btn-primary',
                    disableProcess: true,
                    click: function () {
                        that._rePublish();
                    }
                },
                {
                    text: egov.resources.common.closeButton,
                    className: 'btn-close',
                    click: function () {
                        egov.callback(that.callback);
                        that.$el.dialog('hide');
                    }
                }];
            } else {
                buttons = [{
                    text: _resource.publishButton,
                    className: 'btn-primary',
                    disableProcess: true,
                    click: function (enableButtonsCalback) {
                        if (!that.hasDg) {
                            var storeId = that.$stores.val();
                            var code = that.$codeId.val();

                            that.$el.dialog('hide');
                            egov.callback(that.callback, { storeId: storeId, code: code, codeId: that.result.get('CodeId') });
                        }
                        else if (that.theoLo) {
                            that._publishPrivateTheoLo();
                        } else {
                            that._publishPrivate();
                        }
                        enableButtonsCalback();
                    }
                },
                {
                    text: "Ký và phát hành",
                    className: 'btn-info',
                    disableProcess: true,
                    click: function (enableButtonsCalback) {
                        that._signAndPublishPrivate();
                        enableButtonsCalback();
                    }
                },
                {
                    text: egov.resources.common.closeButton,
                    className: 'btn-close',
                    click: function () {
                        egov.callback(that.callback);
                        that.$el.dialog('hide');
                    }
                }]
            }
            this.$el.attr("help-content-page", "privatePublishment")
            this.$el.dialog({
                title: _resource.privateDialogTitle,
                width: that.hasDg ? "780px" : "450px",
                height: that.hasDg ? "360px" : "180px",
                draggable: true,
                onclose: function (e) {
                    that.$el.dialog('hide');
                    return;
                },
                buttons: buttons
            });

            this._renderData();
            this._changeStore();
        },

        uncheckAddress: function (e) {
            var target = $(e.target);
            if (target.is(':checkbox')) {
                var addressId = target.val();
                var add = this.model.detect(function (address) {
                    return address.get('AddressId') == addressId;
                });

                if (add) {
                    add.set('isSelected', false);
                }

                egov.helper.destroyClickEvent(e);
            }
        },

        uncheckPrivateAnoun: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            if (egov.views.dg) {
                var target = $(e.target).parent().find(':checkbox');
                if (target.length === 0) return;

                egov.views.dg.uncheckPrivateAnoun(target.val());
                this._dongGui();
            }
        },

        serialize: function () {
            /// <summary>
            /// Trả về serialize dữ liệu của form
            /// </summary>
            /// <returns type="">Json object</returns>

            // Lấy các giá trị của các input
            for (var attr in this.result.attributes) {
                // CodeId được set riêng khi chọn sổ hoặc code
                if (attr !== 'CodeId' && this.$('#' + attr).length > 0) {
                    this.result.set(attr, this.$('#' + attr).val());
                }
            }

            var datePublish = this.$("#DatePublished").datepicker("getDate");
            if (datePublish) {
                datePublish.hours(new Date().hours());
                datePublish.minutes(new Date().minutes());
            }
            this.result.set("DatePublished", datePublish == null ? null : datePublish.toServerString());

            var codeId = this.result.get('CodeId');
            // Kiểm tra có custom mã, số ký hiệu hay không.
            var code = _.find(this.codies, function (code) {
                return code.CodeId == codeId;
            });
            if (code && code.Template.trim() != this.result.get('Code').trim()) {
                this.result.set('IsCustomCode', true);
            }

            var inPlace = this.result.get('InPlace');
            this.result.set('Organization', inPlace);

            return this.result;
        },

        //#region Private Methods

        _renderData: function () {
            /// <summary>
            /// Hiển thị dữ liệu cho form phát hành
            /// </summary>
            var that = this;
            var docTypeId = this.docTypeId ? this.docTypeId : this.document.model.get('DocTypeId');

            egov.dataManager.getAllUserDeptPosition({
                success: function (allUserDept) {
                    that.allUserDepts = allUserDept;

                    egov.dataManager.getAllDept({
                        success: function (allDepts) {
                            that.allDepts = allDepts;

                            that._bindDepartments();
                        }
                    });
                }
            });

            var that = this,
                datePublished;

            // Hiển thị sổ văn bản
            var storeId = this.document.model.get("StoreId");
            var categoryId = this.document.model.get("CategoryId");
            var docTypeId = this.docTypeId ? this.docTypeId : this.document.model.get('DocTypeId');

            egov.request.GetStores({
                data: { docTypeId: docTypeId, categoryId: categoryId },
                success: function (stores) {
                    that.stores = stores;
                    if (stores.length > 0) {
                        that.$stores.empty();
                        that.$stores.append($.tmpl('<option value="${StoreId}">${StoreName}</option>', stores));
                        that.$stores.find("option[value='" + storeId + "']").attr("selected", "selected");
                        if (that.plan) {
                            that._showCode(that.plan.publishInfo.StoreId);
                        } else {
                            // Hiển thị danh sách bảng mã của Sổ văn bản đầu tiên
                            storeId |= stores[0].StoreId;
                            that._showCode(storeId);
                        }
                    }
                }
            });

            var showSigners = function (allUsers, allJobtitle, allUserDeptPosition) {
                that.allUsers = allUsers;
                that.allJobtitle = allJobtitle;
                that.allUserDeptPosition = allUserDeptPosition;

                that.approvers = that._getApprovers();

                that.$approvers.empty();
                // that.$approvers.append($.tmpl('<option value="${value}">${fullname} - ${username}</option>', approvers));
                that.$approvers.append($.tmpl('<li class="list-group-item" value="${value}">${fullname} - ${username}</li>', that.approvers));
                that.$("#ApproverName").val(that.approvers[0].fullname + " - " + that.approvers[0].username);
                that.$("#Approvers").val(that.approvers[0].value);

                // Hiển thị người ký mặc định là người duyệt văn bản
                if (that.document && that.document.model) {
                    var userSuccessId = that.document.model.get('UserSuccessId');
                    if (userSuccessId) {
                        that.$approvers.find('option[value="' + userSuccessId + '"]').attr('selected', 'selected');
                    }
                }

                that._autoCompleteApprover();
            };

            egov.dataManager.getAllUsers({
                success: function (allUsers) {
                    egov.dataManager.getAllJobtitle({
                        success: function (allJobtitle) {
                            egov.dataManager.getAllUserDeptPosition({
                                success: function (allUserDeptPosition) {
                                    showSigners(allUsers, allJobtitle, allUserDeptPosition);
                                }
                            });
                        }
                    });
                }
            });

            // Hiển thị độ mật
            var securities = [
                { name: "Thường", value: 1 },
                { name: "Mật", value: 2 },
                { name: "Tối mật", value: 3 },
                { name: "Tuyệt mật", value: 4 }
            ];
            that.$securities.empty();
            that.$securities.append($.tmpl('<option value="${value}">${name}</option>', securities));

            datePublished = Globalize.format(new Date(), "dd/MM/yyyy")
            that.$datePublish.val(datePublished);

            that.$datePublish.datepicker({ gotoCurrent: true, prevText: "Trước", nextText: "Tiếp", dateFormat: 'dd/mm/yy' });
            that.$("#DateResponse").datepicker({ gotoCurrent: true, prevText: "Trước", nextText: "Tiếp", dateFormat: 'dd/mm/yy' });

            // Nơi lưu bản gốc
            egov.dataManager.getAllDept({
                success: function (depts) {
                    var leng = depts.length;
                    that.$depts.empty();
                    var currentDept = egov.setting.currentDepts ? egov.setting.currentDepts[0] : "";

                    for (var i = 0; i < leng; i++) {
                        var temp = '<option value="${label}" >${label}</option>';

                        if (currentDept !== "" && currentDept === depts[i].label) {
                            temp = '<option value="${label}"selected="selected">${label}</option>';
                        }

                        that.$depts.append($.tmpl(temp, depts[i]));
                    }
                }
            });

            // Hiển thị nhận thông báo nội bộ
            that._showDg();
        },

        _showCode: function (storeId) {
            /// <summary>
            /// Hiển thị số ký hiệu (template)
            /// </summary>
            var that = this;
            that.$code.empty();
            that.$codeId.val('');

            //HopCV
            // Bổ sung thêm lấy theo cả lĩnh vực (categoryId)
            var data = {
                storeId: storeId
            };

            if (this.theoLo) {
                if (!this.categoryId) {
                    return;
                }
                data.categoryId = this.categoryId;
            } else {
                data.categoryId = this.categoryId;
                data.documentId = this.documentId;
            }
            egov.request.GetCodes({
                data: data,
                success: function (codies) {
                    that.codies = codies;
                    if (codies && codies.length > 0) {
                        that.$code.append($.tmpl('<li class="list-group-item" value="${CodeId}">${Template}</li>', codies));
                        that.$codeId.val(codies[0].Template);
                        that.result.set('CodeId', codies[0].CodeId);
                    }
                }
            });
        },

        _selectCode: function (e) {
            /// <summary>
            /// Thay đổi mã hồ sơ
            /// </summary>
            /// <param name="e"></param>
            var code = $(e.target).text();
            var codeId = $(e.target).attr('value');
            this.result.set('CodeId', parseInt(codeId));
            this.$codeId.val(code);
        },

        _selectApprover: function (e) {
            var target = $(e.target).closest("li");
            var approverId = target.attr("value");
            this.$("#ApproverName").val(target.text());
            this.$("#Approvers").val(approverId);
        },

        _autoCompleteApprover: function () {
            var source = this.approvers;
            var that = this;
            this.$("#ApproverName").autocomplete({
                source: source,
                focus: function (event, ui) {
                    return false;
                },
                select: function (event, ui) {
                    var itm = ui.item;
                    that.$("#ApproverName").val(itm.fullname + " - " + itm.username);
                    that.$("#Approvers").val(itm.value);
                    return false;
                }
            })
            .data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu');
                ul.css("zIndex", "2000");
                return $("<li></li>")
                    .data("item.autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };
        },

        _showDg: function () {
            /// <summary>
            /// Hiển thị nơi nhận trong đơn vị.
            /// </summary>
            var that = this;
            if (!that.$dg.is(':not(:empty)') && this.hasDg) {
                require(['transferExtend'], function (transferExtend) {
                    egov.views.dg = new transferExtend;
                    egov.views.dg.render(false, false, function () {
                        that._dongGui();
                    }, function () {
                        that.$dg.append(egov.views.dg.$el);
                    });
                });
            }

            if (!this.hasDg) {
                this.$(".private-receiver").hide();
            }
        },

        _dongGui: function () {
            this.$privateAnounc.empty();
            egov.views.dg.selectDg(this.$privateAnounc);
        },

        _changeStore: function () {
            var that = this;
            this.$stores.change(function () {
                var storeId = $(this).val();
                that._showCode(storeId);
            });
        },

        _signAndPublishPrivate: function () {
            if (!Plugin) {
                return;
            }

            var that = this;

            that.document.model.set("DocCode", that.$("#Code").val());

            Plugin.appendPlugin(function () {
                Plugin.sign(that.document, function (isSigned, signMessage) {
                    isSigned = isSigned || false;
                    // Nếu là đóng cửa sổ chọn file kí thì show lại trạng thái nút trên cửa sổ bàn giao
                    if (!isSigned) {
                        return;
                    } else {
                        that.document.model.set("HasCA", true);
                        that._publishPrivate();
                    }
                });
            });
        },

        _rePublish: function () {
            /// <summary>
            /// Phát hành văn bản.
            /// </summary>
            var model = this.serialize();

            // Lấy Document.
            var doc = this.document.serialize();

            // Lấy danh sách đồng gửi.
            var usersConsult = egov.views.dg.getUserConsults();

            // Danh sách user nhận văn bản đến
            var userHasReceiveDocuments = this._getUserReceiveDocument();

            // Loại bỏ user nhận thông báo khi đã nhận văn bản đến.
            usersConsult = _.difference(usersConsult, _.keys(userHasReceiveDocuments));

            // Lấy danh sách file được thêm vào
            var selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });

            // Lấy danh sách file được xóa đi.
            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });

            var that = this;
            // Phát hành

            var documentCopyId = parseInt(that.document.model.get('DocumentCopyId'));

            var destination = egov.views.dg.getDestination();

            var targetComments = this.targetComments;
            targetComments = targetComments.concat(destination);

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            egov.request.rePublish({
                data: {
                    documentCopyId: documentCopyId,
                    doc: JSON.stringify(doc),
                    files: JSON.stringify(selectedFiles),
                    modifiedFiles: JSON.stringify(this.document.attachments.modifiedFiles),
                    removeAttachmentIds: removeFiles,
                    publishinfo: JSON.stringify(model),
                    usersConsult: usersConsult,
                    userHasReceiveDocuments: JSON.stringify(userHasReceiveDocuments),
                    targetForComments: JSON.stringify(targetComments)
                },
                success: function (result) {
                    if (!result.error) {
                        that._destroyDg();

                        // Gan de close ko hoi lai
                        that.document.isSaveChanged = true;

                        that.$el.dialog('hide');

                        that._closeTab([documentCopyId]);
                    }
                    else {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.error);
                }
            });
        },

        _publishPrivate: function () {
            /// <summary>
            /// Phát hành văn bản.
            /// </summary>
            var model = this.serialize();

            // Lấy Document.
            var doc = this.document.serialize();

            // Lấy danh sách đồng gửi.
            var usersConsult = egov.views.dg.getUserConsults();

            // Danh sách user nhận văn bản đến
            var userHasReceiveDocuments = this._getUserReceiveDocument();

            // Loại bỏ user nhận thông báo khi đã nhận văn bản đến.
            usersConsult = _.difference(usersConsult, _.keys(userHasReceiveDocuments));

            // Lấy danh sách file được thêm vào
            var selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });

            // Lấy danh sách file được xóa đi.
            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });

            var modifiedFiles = this.document.attachments.modifiedFiles;

            // Cập nhật nội dung file đã sửa với những file vừa upload lên.
            // Đồng thời xóa file đó trong danh sách file đang chỉnh sửa.
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });

            var that = this;
            // Phát hành

            var documentCopyId = parseInt(that.document.model.get('DocumentCopyId'));

            var destination = egov.views.dg.getDestination();

            var targetComments = this.targetComments;
            targetComments = targetComments.concat(destination);

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            egov.request.privatePublish({
                data: {
                    documentCopyId: documentCopyId,
                    doc: JSON.stringify(doc),
                    files: JSON.stringify(selectedFiles),
                    modifiedFiles: JSON.stringify(modifiedFiles),
                    removeAttachmentIds: removeFiles,
                    publishinfo: JSON.stringify(model),
                    usersConsult: usersConsult,
                    userHasReceiveDocuments: JSON.stringify(userHasReceiveDocuments),
                    targetForComments: JSON.stringify(targetComments)
                },
                success: function (result) {
                    if (result.success) {
                        that._destroyDg();

                        // Gan de close ko hoi lai
                        that.document.isSaveChanged = true;
                        // close tab
                        that._closeTab([documentCopyId]);

                        // Reload lại node hiện tại
                        if (that.document) {
                            that.document._reloadDocumentTree([documentCopyId]);
                        }

                        that.$el.dialog('hide');
                    }
                    else {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.error);
                }
            });
        },

        _publishPrivateTheoLo: function () {
            /// <summary>
            /// Phát hành văn bản.
            /// </summary>
            var model = this.serialize();
            // Lấy danh sách đồng gửi.
            var usersConsult = _.pluck(egov.views.dg.getUserConsults(), "value");

            // Danh sách user nhận văn bản đến
            var userHasReceiveDocuments = egov.views.dg.getUserReceiveDocument();

            // Loại bỏ user nhận thông báo khi đã nhận văn bản đến.
            usersConsult = _.difference(usersConsult, userHasReceiveDocuments);

            var that = this;

            // egov.message.processing(egov.resources.common.transfering);
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            egov.request.privatePublishTheoLo({
                data: {
                    documentCopyIds: this.documentCopyIds,
                    publishinfo: JSON.stringify(model),
                    usersConsult: usersConsult,
                    userHasReceiveDocuments: userHasReceiveDocuments
                },
                success: function (result) {
                    if (result.success) {
                        that._destroyDg();

                        that.document.isSaveChanged = true;
                        // close tab
                        that._closeTab(that.documentCopyIds);

                        //Reload lại node hiện tại
                        that.parent.removeDocumentByIdsAndSetSelected(that.documentCopyIds, function () {
                            //  that.parent.loadNewerDocuments();
                        });

                        egov.callback(that.callback);
                        that.$el.dialog('hide');
                    }
                    else {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.error);
                }
            });
        },

        _destroyDg: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        },

        _closeTab: function (documentCopyIds) {
            ///<summay>
            /// Đóng tab văn bản khi bàn giao thành công
            ///<para name="documentCopyIds"> Có thể là mảng chứa danh sách id văn bản hoặc id văn bản</para>
            ///</summay>
            if (this.document && this.document.tab) {
                this.document.tab.close();
            }
            else if (egov.views.home && egov.views.home.tab) {
                if (!documentCopyIds) {
                    return;
                }

                if (!(documentCopyIds instanceof Array)) {
                    documentCopyIds = [documentCopyIds];
                }

                egov.views.home.tab.closeTab(documentCopyIds);
            }

            //// reload lại cây văn bản
            //egov.views.home.tree.removeDocuments(documentCopyIds);
            //egov.views.home.tree.selectedNode.documentsView.loadNewerDocuments();
        },

        _getUserReceiveDocument: function () {
            var selectedDeptIds = this.selectedDeptIds, result = {},
                allUserDept = this.allUserDepts, allDepts = this.allDepts,
                isRoot, userInDepts, deptName;

            if (_.isEmpty(selectedDeptIds)) {
                return result;
            }

            _.each(_.keys(selectedDeptIds), function (deptId) {
                deptName = selectedDeptIds[deptId];
                userInDepts = _.filter(allUserDept, function (u) {
                    isRoot = deptId.indexOf('.') < 0;
                    if (isRoot) {
                        // root
                        return u.idext === deptId && u.hasReceiveDocument == true;
                    }

                    return u.idext.indexOf(deptId) == 0 && u.hasReceiveDocument == true;
                });

                _.each(userInDepts, function (u) {
                    result[u.userid] = deptName;
                });

                // result.push(_.pluck(userInDepts, "userid"));
            });

            return result;
        },

        _getApprovers: function () {
            /// <summary>
            /// Trả về danh sách người ký duyệt
            /// </summary>
            var approvers = [];
            if (!approvers || approvers.length <= 0) {
                var approverIds = [];
                var allUsers = this.allUsers;
                var jobtitlies = this.allJobtitle;
                var jobDepts = this.allUserDeptPosition;
                jobDepts.forEach(function (jobDept) {
                    var jobId = jobDept.jobtitleid;
                    var isApprover = _.find(jobtitlies, function (job) {
                        return job.value === jobId && job.isApprover;
                    });

                    var userPrimaryDepartment = egov.setting.user.primaryDepartment;

                    if (isApprover && egov.setting.transfer.showApproverByDepartment) {
                        isApprover = userPrimaryDepartment.indexOf(jobDept.idext) === 0;
                    }

                    if (isApprover) {
                        approvers.push({
                            value: jobDept.userid,
                            username: jobDept.username,
                            fullname: jobDept.fullname,
                            isMain: true,
                            label: jobDept.fullname + " - " + jobDept.username
                        });

                        // approverIds.push(jobDept.userid);
                    }
                });

                //  approverIds = _.uniq(approverIds);
                //approvers = _.filter(allUsers, function (user) {
                //    return approverIds.indexOf(user.value) >= 0;
                //});
            }

            return approvers;
        },

        _bindDepartments: function (callback) {
            var that = this;
            var allDepts = that.allDepts, allUserDept = this.allUserDepts;

            _.each(allDepts, function (dept) {
                var deptId = dept.value;
                var hasReceiver = _.find(allUserDept, function (ud) {
                    return ud.departmentid === deptId && ud.hasReceiveDocument;
                });

                dept.hasReceiveDocument = hasReceiver;

                if (!hasReceiver) {
                    dept.state = "disabled";
                }
            });

            _bindJsTree(that.$('#listDepartments'), false, true, false,
                              allDepts, [], [], null, []);

            that.$('#listDepartments').bind('change_state.jstree', function (e, data) {
                var isRoot = data.rslt.attr("idext").indexOf(".") < 0;

                if (isRoot && data.rslt.is(".jstree-checked")) {
                    data.rslt.find(".jstree-checked").removeClass("jstree-checked").addClass("jstree-unchecked");
                }

                that._changeDepartmentReceive();
            });
        },

        _changeDepartmentReceive: function () {
            var that = this;
            var allDepts = this.allDepts;
            that.$(".main-address ul").empty();
            that.targetComments = [];

            that.selectedDeptIds = {};// Đổi lại thành dạng "{Id: name} để cập nhật đơn vị nhận văn bản
            this.$("#listDepartments  .jstree-checked").each(function () {
                var node = $(this);
                var nodeId = node.attr("id");
                var disabled = node.is(".jstree-disabled");

                // Xác định node đang chọn là node phòng ban hay node user
                var isDeptNode = node.attr('rel') === 'dept';
                if (isDeptNode) {
                    var dept = _.find(allDepts, function (i) {
                        return i.value == nodeId;
                    });

                    if (dept) {
                        var label = disabled ? dept.label + " (không có người nhận)" : dept.label;
                        that.targetComments.push({
                            label: label,
                            value: '',
                            type: 0
                        });

                        that.selectedDeptIds[dept.idext] = dept.label;
                        that.$(".main-address ul").append(_parseUserItem(dept.value, label));
                    }
                }
            });
        },

        //#endregion
    });

    var _parseUserItem = function (value, name) {
        var template = '<li class="list-group-item">\
                                <div class="row">\
                                    <label class="checkbox document-color">\
                                       <input name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                        <span class="document-color-1"><i class="icon-check"></i></span>\
                                    </label>\
                                    <span style="margin-left: 15px;">{1}</span>\
                                </div>\
                            </li>';
        return $(String.format(template, value, name));
    };

    var _getChildrens = function (parentid, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
        var children = _.filter(arrDept, function (num) { return num.parentid == parentid; });
        var deptUsers = _.filter(arrDeptUserJobtitles, function (dept) {
            return dept.departmentid === parentid;
        });

        if (children.length > 0) {
            for (var j = 0; j < children.length; j++) {
                if (_getChildrens(children[j].value, false, arrDept, [], []).length > 0 || (hasUser && deptUsers.length > 0)) {
                    children[j].state = "closed";
                }
            }
        }

        if (hasUser && deptUsers.length > 0) {
            for (var i = 0; i < deptUsers.length; i++) {
                var userindept = _.find(arrUsers, function (user) {
                    return user.value === deptUsers[i].userid;
                });

                if (userindept) {
                    var selected = {
                        "value": "user_" + userindept.value,
                        "data": userindept.fullname,
                        "parentid": parentid,
                        "state": "leaf",
                        "metadata": { "id": "user_" + userindept.value },
                        "attr": {
                            "id": "user_" + userindept.value,
                            "rel": "people",
                            "idext": deptUsers[i].idext,
                            hasReceiveDocument: deptUsers[i].hasReceiveDocument
                        }
                    };
                    children.push(selected);
                }
            }
        }

        return children;
    };

    var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" hasReceiveDocument="${attr.hasReceiveDocument}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewCheckboxTemplate += '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var plugins = ["themes", "json_data", "ui", "crrm"];

    var _bindJsTree = function (divTree, hasUser, hasCheckbox,
        hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind) {
        var deptRoot = _.find(arrDept, function (node) {
            return node.parentid === 0;
        });
        if (hasCheckbox) {
            plugins.push("checkbox");
        }
        if (hasDnD) {
            plugins.push("dnd");
        }
        if (deptRoot) {
            var children = _getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
            divTree.jstree({
                "json_data": {
                    "data": [
                        {
                            "data": deptRoot.data.toString(),
                            "metadata": { id: deptRoot.value },
                            "state": "closed",
                            "attr": {
                                "id": deptRoot.value, "rel": "dept",
                                "idext": deptRoot.idext, "label": deptRoot.label
                            },
                            "children": children
                        }
                    ]
                },
                "crrm": hasDnD == false ? {} : {
                    "move": {
                        "check_move": function (m) {
                            var dept = _.find(arrDept, function (de) {
                                return de.value === parseInt(m.o.attr('id'));
                            });
                            if (!dept) return false;
                            if (dept.level != 1) return false;
                            var p = this._get_parent(m.o);
                            if (!p) return false;
                            p = p == -1 ? this.get_container() : p;
                            if (p === m.np) return true;
                            if (p[0] && m.np[0] && p[0] === m.np[0]) return true;
                            return false;
                        }
                    }
                },
                "dnd": hasDnD == false ? {} : {
                    "drop_target": false,
                    "drag_target": false
                },
                "plugins": plugins
            }).bind("loaded.jstree", function (e, dataLoad) {
                var depth = 1;
                dataLoad.inst.get_container().find('li').each(function () {
                    if (dataLoad.inst.get_path($(this)).length <= depth) {
                        dataLoad.inst.open_node($(this));
                    }
                });
                divTree.bind("open_node.jstree", function (event, data) {
                    if (data.inst._get_children(data.rslt.obj).length == 0) {
                        _appendChild(data.rslt.obj, parseInt(data.rslt.obj.attr("id")), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles);
                    }
                });
            });
        }
    };

    var _appendChild = function ($parent, parentId, hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles) {
        var child = _getChildrens(parentId, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
        if (child.length > 0) {
            var $newChild = $('<ul></ul>');
            $newChild.appendTo($parent);
            if (hasCheckbox) {
                // $.template("checkboxTemplate", itemTreeviewCheckboxTemplate);
                $.tmpl(itemTreeviewCheckboxTemplate, child).appendTo($newChild);
                $($parent).find("li").each(function (idx, listItem) {
                    $(listItem).addClass($parent.hasClass("jstree-checked") ? "jstree-checked" : "jstree-unchecked");
                });
            } else {
                // $.template("itemTreeviewTemplate", itemTreeviewTemplate);
                $.tmpl(itemTreeviewTemplate, child).appendTo($newChild);
            }
            $newChild.children("li:last").addClass("jstree-last");
        }
    };

    return PrivatePublishment;
});