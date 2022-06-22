define([
egov.template.document.publishmentGov,
egov.template.document.addressItem
],
function (PublishmentTemplate, AddressItemTemplate) {
    "use strict";

    var _resource = egov.resources.document.publishment;

    //#region Views

    /// <summary>Đối tượng form Lưu sổ phát hành</summary>
    var Publishment = Backbone.View.extend({
        // Load cùng với trang home (trong _Publishment.cshtml)
        tagName: 'div',//'#Publishment',

        events: {
            'click .main-address .checkbox': 'uncheckAddress',
            'click .private-dept .checkbox': 'uncheckDept',
            'click .private-anoun .checkbox': 'uncheckPrivateAnoun',
            'blur #Code': '_checkCodeUsed',
            'click .code-list > .list-group-item': '_selectCode',
            'keydown #searchAddress': "filterAddress",
            'keydown #searchEmail': "filterEmail",
            "click #addressDateResponse": "addressResponse",
            'click .ddlApprovers > li': '_selectApprover',
        },

        groups: {},
        noneGroup: "Chưa phân nhóm",
        allGroup: "Tất cả",

        filterAddress: function (e) {
            var address,
                $address = this.$(e.target);
            var that = this;
            if (e.keyCode === 13) {
                address = $address.val();
                if (address === '') {
                    return;
                }
                that.$address.append("<tr class='tempAddress'><td></td><td>" + address + "</td><td></td></tr>");

                var tempAddress = _parseUserItem(0, address);
                tempAddress.find(":checkbox").click(function () {
                    tempAddress.remove();
                    $(".tempAddress").remove();
                    that.$("#tempAddress").val("");
                });

                that.$("#tempAddress").val(address);

                that.$mainAddress.append(tempAddress);

                $address.val("");
            }
        },

        filterEmail: function (e) {
            var email,
                $email = this.$(e.target);
            var that = this;
            if (e.keyCode === 13) {
                email = $email.val();
                var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

                if (email === '' || !re.test(String(email).toLowerCase())) {
                    return;
                }
                that.$address.append("<tr class='tempEmail'><td></td><td>" + email + "</td><td></td></tr>");

                var tempEmail = _parseUserItem("mail", email);
                tempEmail.find(":checkbox").click(function () {
                    tempEmail.remove();
                    $(".tempEmail").remove();
                    that.$("#tempEmail").val("");
                });

                that.$("#tempEmail").val(email);

                that.$mailAddress.append(tempEmail);

                $email.val("");
            }
        },

        initialize: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            this._clear();
            this.$el.attr("id", "Publishment").addClass("publishment").html(PublishmentTemplate);//.appendTo('body');
            this.$el.bindResources();
            this.result = new egov.models.publish;

            return this;
        },

        render: function (option) {
            /// <summary>
            /// Hiển thị form phát hành
            /// </summary>
            /// <param name="option">Các tham số khởi tạo</param>
            this._clear();
            this.theoLo = false;
            this.document = option.document;
            this.docTypeId = option.docTypeId;
            this.categoryId = this.document.model.get("CategoryId");
            this.documentId = this.document.model.get("DocumentId");
            this.plan = option.plan;
            this.isPlanning = option.isPlan;
            this.$el.show();
            // Danh sách các sổ hồ sơ.
            this.stores = [];

            this.isRePublish = false;

            if (option.isRePublish) {
                this.isRePublish = option.isRePublish;
            }

            this._renderDialog();

            return this;
        },

        renderTheoLo: function (option) {
            /// <summary>
            /// Hiển thị form phát hành theo lô
            /// </summary>
            /// <param name="option">Các tham số khởi tạo</param>
            this._clear();
            this.docTypeId = option.docTypeId;
            this.documentCopyIds = option.documentCopyIds;
            this.parent = option.parent;
            this.theoLo = true;
            this.categoryId = option.categoryId;
            this.callback = option.callback;
            this.$el.show();
            // Danh sách các sổ hồ sơ.
            this.stores = [];
            this._renderDialog();

            return this;
        },

        _clear: function () {
            /// <summary>
            /// Xoa bo
            /// </summary>
            // this.$el.attr("id", "Publishment").addClass("publishment").html(PublishmentTemplate);
            this.$el.bindResources();

            this.$address = this.$('.listAddress');
            this.$stores = this.$('#StoreId');
            this.$codeId = this.$('#Code');
            //this.$code = this.$('.code-list');
            this.$code = this.$('#Code');
            this.$approvers = this.$('.ddlApprovers');
            this.$depts = this.$('#InPlace');
            this.$keywords = this.$('#KeyWordId');
            this.$securities = this.$('#SecurityId');
            this.$datePublish = this.$('#DatePublished1');
            this.$dg = this.$('.dg-view');
            this.$search = this.$('#searchAddress');
            this.$mainAddress = this.$('.main-address ul:first');
            this.$mailAddress = this.$('.main-mail ul:first');
            this.$privateAnounc = this.$('.private-anoun ul:first');
            this.$mainAddress.empty();
            this.$mailAddress.empty();
            this.$privateAnounc.empty();
            this.$(".private-dept ul").empty();
            $("#privateDepartments").html("");
            this.$code.val("");
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        },

        _renderDialog: function () {
            var that = this;
            var buttons = [];
            if (this.isRePublish) {
                buttons = [
                    {
                        text: "Phát hành tiếp",
                        className: 'btn-primary',
                        disableProcess: true,
                        click: function () {
                            that._rePublish();
                        }
                    }
                ];
            }
            else {
                var that = this,
                    buttons = [
                        {
                            text: that.isPlanning ? _resource.addpublishment : _resource.publishButton,
                            className: 'btn-primary',
                            disableProcess: true,
                            click: function (enableButtonsCalback) {
                                if (that.isPlanning) {
                                    //Hiển thị theo dự kiến phát hành
                                    //that._publishPlan();
                                } else {
                                    if (that.theoLo) {
                                        //that._publishTheoLo();
                                    }
                                    else {
                                        //Phát hành văn bản
                                        that._publish();
                                    }
                                }
                                enableButtonsCalback();
                            }
                        }
                        //,
                        //{
                        //    text: "Ký và Phát hành",
                        //    id: "signAndPublish",
                        //    className: 'btn-info ' + (!that.isPlanning ? "" : "hidden"),
                        //    disableProcess: true,
                        //    click: function (enableButtonsCalback) {
                        //        if (that.theoLo) {
                        //            return;
                        //        }
                        //        else {
                        //            //Phát hành văn bản
                        //            that._signAndPublish();
                        //        }
                        //        enableButtonsCalback();
                        //    }
                        //}
                    ];
            }

            buttons.push({
                text: egov.resources.common.closeButton,
                className: 'btn-close',
                click: function () {
                    var status = that.isPlan ? "destroy" : "hide";
                    egov.callback(that.callback);
                    that.$el.dialog(status);
                }
            });
            this.$el.attr("help-content-page", "publishment")
            this.$el.dialog({
                title: _resource.dialogTitle,
                width: "850px",
                height: "450px",
                draggable: true,
                buttons: buttons
            });

            if (!egov.setting.publish.showPlaceInOffice) {
                this.$('#privateDepts').parents('.form-group').hide();
                this.$('#privateDepts').parents('.form-group').prev().hide();
                this.$('.private-dept').parent().hide();
                this.$('.listAddress').height(158);
            }

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

        uncheckDept: function (e) {
            var target = $(e.target);
            if (target.is(':checkbox')) {
                var addressId = target.val();
                $("#privateDepartments").jstree("uncheck_node", $("#privateDepartments").find("li#" + addressId));
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

            var datePublish = this.$("#DatePublished1").datepicker("getDate");
            var dateResponse = this.$("#DateResponse").datepicker("getDate");

            if (datePublish) {
                datePublish.hours(new Date().hours());
                datePublish.minutes(new Date().minutes());
            }
            //if (dateResponse) {
            //    dateResponse.hours(new Date().hours());
            //    dateResponse.minutes(new Date().minutes());
            //}

            this.result.set("DatePublished", datePublish == null ? null : datePublish.toServerString());
            //this.result.set("DateResponse", dateResponse == null ? null : dateResponse.toServerString());

            var codeId = this.result.get('CodeId');
            if (codeId == 0) {
                codeId = this.$("#CodeId li[text='" + this.result.get("Code") + "']").attr("value");
            }

            // Kiểm tra có custom mã, số ký hiệu hay không.
            var code = _.find(this.codies, function (code) {
                return code.CodeId == codeId;
            });

            if (code && code.Template.trim() != this.result.get('Code').trim()) {
                // this.result.set('IsCustomCode', true);
            }

            // Lấy danh sách các địa chỉ nhận
            var addressIds = _.map(this.model.where({ isSelected: true }), function (item) {
                return item.get("AddressId");
            });
            var addressIdUniqs = _.uniq(addressIds)
            this.result.set('Address', addressIdUniqs);
            this.result.set('Organization', this.$("#Organization :selected").text());
            this.result.set('OrganizationCode', "000.00.00.H49");

            //if (this.isChangeDateResponse) {
            //    var customDateResponse = {};
            //    _.each(this.model.where({ isSelected: true }), function (a) {
            //        customDateResponse[a.get("AddressId")] = a.get("DateResponse");
            //    });

            //    this.result.set("DateResponeAddress", customDateResponse);
            //}
            return this.result;
        },

        addressResponse: function () {
            var that = this;
            var addressSelected = this.model.select({ isSelected: true });
            var addressSelectedUniq = _.uniq(addressSelected, function (x) {
                return x.get("AddressId");
            });

            this.result.set("DateResponse", this.$("#DateResponse").datepicker("getDate"));

            if (addressSelectedUniq.length > 0) {
                require(['DateResponseAddresses'], function (returnView) {
                    new returnView({
                        listAddress: addressSelectedUniq,
                        dateResponse: that.result.get("DateResponse"),
                        callback: function (data) {
                            that.isChangeDateResponse = true;
                        }
                    });
                });
            }
        },

        //#region Private Methods

        _renderData: function () {
            /// <summary>
            /// Hiển thị dữ liệu cho form phát hành
            /// </summary>
            var that = this,
                datePublished;

            //#region Danh sách cơ quan ban hành

            egov.dataManager.getAllAddress({
                success: function (address) {
                    that.model = new egov.models.addressCollection(address);
                    that._bindAddress();
                    return;
                }
            });

            //#endregion

            //#region Nơi lưu bản gốc

            egov.dataManager.getAllDept({
                success: function (allDepts) {
                    egov.dataManager.getCurrentDepartments({
                        success: function (currentDepts) {
                            var currentInOutPlace = currentDepts ? currentDepts[0] : allDepts[0];

                            var leng = allDepts.length;
                            that.$depts.empty();

                            for (var i = 0; i < leng; i++) {
                                var temp = '<option value="${label}" >${label}</option>';

                                if (currentInOutPlace && currentInOutPlace == allDepts[i].label) {
                                    temp = '<option value="${label}"selected="selected">${label}</option>';
                                }

                                //Hiển thị theo dự kiến phát hành
                                if (that.plan && that.plan.publishInfo && that.plan.publishInfo.InPlace) {
                                    if (allDepts[i].label == that.plan.publishInfo.InPlace) {
                                        temp = '<option value="${label}"selected="selected">${label}</option>';
                                    }
                                }

                                that.$depts.append($.tmpl(temp, allDepts[i]));
                            }
                        }
                    });
                }
            });

            //#endregion

            //#region Người ký

            var showSigners = function (allUsers, allJobtitle, allUserDeptPosition) {
                that.allUsers = allUsers;
                that.allJobtitle = allJobtitle;
                that.allUserDeptPosition = allUserDeptPosition;

                that.approvers = that._getApprovers();

                that.$approvers.empty();

                // that.$approvers.append($.tmpl('<option value="${value}">${fullname} - ${username}</option>', approvers));
                if (that.approvers.length > 0) {
                    that.$approvers.append($.tmpl('<li class="list-group-item" value="${value}">${fullname} - ${username}</li>', that.approvers));

                    that.$("#ApproverName").val(that.approvers[0].fullname + " - " + that.approvers[0].username);
                    that.$("#Approvers").val(that.approvers[0].value);
                }
                

                if (that.plan && that.plan.publishInfo) {//Dự kiến phát hành
                    that.$approvers.find('option[value="' + that.plan.publishInfo.Approvers + '"]').attr('selected', 'selected');
                }
                else {
                    // Hiển thị người ký mặc định là người duyệt văn bản
                    if (that.document && that.document.model) {
                        var userSuccessId = that.document.model.get('UserSuccessId');
                        if (userSuccessId) {
                            that.$approvers.find('option[value="' + userSuccessId + '"]').attr('selected', 'selected');
                        }
                    }
                }
                if (that.approvers.length > 0) {
                    that._autoCompleteApprover();
                }
            };

            egov.dataManager.getAllUsers({
                success: function (allUsers) {
                    var type_position = egov.setting.typePositionTitleJob;
                    if (type_position === undefined) {
                        type_position = 0;
                    }
                    if (type_position === 0) {
                        egov.dataManager.getAllJobtitle({
                            success: function (allJobtitle) {
                                egov.dataManager.getAllUserDeptPosition({
                                    success: function (allUserDeptPosition) {
                                        showSigners(allUsers, allJobtitle, allUserDeptPosition);
                                    }
                                });
                            }
                        });
                    } else {
                        egov.dataManager.getAllPosition({
                            success: function (allJobtitle) {
                                egov.dataManager.getAllUserDeptPosition({
                                    success: function (allUserDeptPosition) {
                                        showSigners(allUsers, allJobtitle, allUserDeptPosition);
                                    }
                                });
                            }
                        });
                    }
                }
            });

            //#endregion

            //#region Độ mật

            var securities = [
                { name: "Thường", value: 1 },
                { name: "Mật", value: 2 },
                { name: "Tối mật", value: 3 },
                { name: "Tuyệt mật", value: 4 }
            ];

            that.$securities.empty();
            that.$securities.append($.tmpl('<option value="${value}">${name}</option>', securities));

            //#endregion

            //#region Cơ quan ban hành

            egov.request.getOrganizations({
                success: function (data) {
                    data = _.uniq(data, function (x) {
                        return x.OrganId;
                    });
                    that.$("#Organization").html($.tmpl('<option value="${OrganId}">${OrganName}</option>', data));
                }
            });

            //#endregion

            //#region Ngày tháng

            that.$datePublish.datepicker({
                onSelect: function (dateText) {
                    $(inst.input).val(dateText);
                }
            });
            that.$("#DateResponse").datepicker({
                onSelect: function (dateText) {
                    $(inst.input).val(dateText);
                }
            });

            //#endregion

            // Hiển thị nhận văn bản nội bộ
            that._bindPrivateDept();

            // Hiển thị nhận thông báo nội bộ
            that._showDg();

            //#region Hiển thị sổ

            if (!this.isPlanning) {
                // Hiển thị sổ văn bản
                var storeId = this.document.model.get("StoreId");
                var docTypeId = this.docTypeId ? this.docTypeId : this.document.model.get('DocTypeId');
                var categoryId = this.document.model.get('CategoryId');

                egov.request.GetStores({
                    data: { docTypeId: docTypeId, categoryId: categoryId },
                    success: function (stores) {
                        that.stores = stores;
                        if (stores.length > 0) {
                            that.$stores.empty();
                            that.$stores.append($.tmpl('<option value="${StoreId}">${StoreName}</option>', stores));
                            that.$stores.find("option[value='" + storeId + "']").attr("selected", "selected");

                            // Hiển thị danh sách bảng mã của Sổ văn bản đầu tiên
                            storeId |= stores[0].StoreId;
                            that._showCode(storeId);
                        }
                    }
                });
            }

            //#endregion

            if (that.plan) {
                that._renderPublishPlan();
            } else {
                that._renderInfo();
            }
        },

        _bindAddress: function () {
            this.addressGroups = this._getAddressGroups();
            this.$address.empty();

            var that = this;
            _.each(this.addressGroups, function (groupName) {
                that.$address.append(_parseGroupName(groupName.trim()));

                var addressByGroups = that.model.select(function (address) {
                    if (groupName.equals(that.noneGroup) || groupName.equals(that.allGroup)) {
                        return String.isNullOrEmpty(address.get("GroupName"));
                    }

                    var addressGroup = String.format(";{0};", address.get("GroupName"));
                    return addressGroup.contains(String.format(";{0};", groupName));
                });

                if (addressByGroups.length === 0) {
                    return;
                }

                that.groups[groupName.trim()] = addressByGroups;
            });

            this._bindAddressToGroup();

            // Xử lý parentId ở đây

            this._bindGroupEvents();

            // Search nhanh danh sách cơ quan nhận
            //this._searchAddress();

            this.$address.find(".toggle-address").first().click();

            this._showAddressByPlan();
        },

        _bindAddressToGroup: function () {
            var that = this;
            var drawItem = function (itm, g) {
                var addressItem = new AddressItm({
                    model: itm,
                    groupName: g,
                    parent: that
                });

                //Check hiển thị theo dự kiến phát hành
                if (that.plan && that.plan.publishInfo) {
                    if (that.plan.publishInfo.Address && that.plan.publishInfo.Address.length > 0) {
                        var address = that.plan.publishInfo.Address;
                        var leng = address.length;
                        for (var i = 0; i < leng; i++) {
                            if (itm.get('value') === address[i]) {
                                addressItem.model.set('isSelected', true)
                            }
                        }
                    }
                }

                that.$address.find("tr[group-name='" + g + "']").after(addressItem.$el);
                itm.view = addressItem;
            }

            _.each(this.groups, function (addressGroups, groupName) {
                _.each(addressGroups, function (address) {
                    drawItem(address, groupName);
                });
            });
        },

        _bindGroupEvents: function () {
            var that = this;

            that.$address.find(".group-name").change(function (e) {
                var groupName = $(this).val();
                var checked = $(this).is(":checked")

                var addressGroup = that.groups[groupName];

                that.groupSelected = groupName;
                _.each(addressGroup, function (address) {
                    address.set({ isSelected: checked });
                });
            });

            that.$address.find(".toggle-address").on("click", function (e) {
                var status = $(this).data("show");
                var name = $(this).data("name");
                $(this).data("show", !status);

                var htmlHide = '<i class="icon icon-arrow-left8">';
                var htmlShow = '<i class="icon icon-arrow-down8">';

                if (status) {
                    $(this).addClass("icon-arrow-left8").removeClass("icon-arrow-down8");
                } else {
                    $(this).addClass("icon-arrow-down8").removeClass("icon-arrow-left8");
                }

                var addressGroup = that.groups[name];

                _.each(addressGroup, function (address) {
                    address.set({ IsShow: !status });
                });
            });
        },

        _renderPublishPlan: function () {
            var that = this;
            var publishPlan = that.plan.publishInfo;
            if (!publishPlan) {
                return;
            }

            var dateResponse = publishPlan.DateResponse == null ?
                                        Date.parse(that.document.model.get("DateAppointed")) :
                                        Date.parse(publishPlan.DateResponse);

            if (dateResponse) {
                that.$("#DateResponse").datepicker("setDate", dateResponse);
            }

            // Độ mật
            var securityId = publishPlan.SecurityId;
            that.$securities.find('option[value="' + securityId + '"]').attr('selected', 'selected');


            // Hiển thị ngày ban hành
            var datePublished = Date.parse(publishPlan.DatePublished);
            if (!datePublished) {
                datePublished = new Date();
            }
            that.$datePublish.datepicker("setDate", datePublished);

            // Người ký
            var signerId = publishPlan.Approvers;
            var signer = _.find(that.allUsers, function (u) {
                return u.value == signerId;
            });

            if (signer) {
                that.$("#ApproverName").val(signer.fullname + " - " + signer.username);
            }
            that.$("#Approvers").val(signerId);

            //hiển thị các thông tin  khác
            that.$("#TotalCopy").val(that.plan.publishInfo.TotalCopy);
            that.$("#TotalPage").val(that.plan.publishInfo.TotalPage);
        },

        _showAddressByPlan: function () {
            var that = this;
            var publishPlan = that.plan ? that.plan.publishInfo : null;
            if (!publishPlan) {
                return;
            }

            // Hạn hồi báo
            var dateResponeAddress = publishPlan.DateResponeAddress;
            if (dateResponeAddress) {
                that.isChangeDateResponse = true;
                _.each(_.keys(dateResponeAddress), function (addressId) {
                    if (dateResponeAddress[addressId] == null) {
                        return;
                    }

                    var address = that.model.detect(function (a) {
                        return a.get("AddressId") == addressId;
                    });

                    var date = new Date(dateResponeAddress[addressId]);
                    if (address) {
                        address.set("DateResponse", date);
                        address.set("DateResponseFormat", date.format("dd/MM/yyyy"));
                    }
                });
            }
        },

        _renderInfo: function () {
            var that = this;
            var documentDateAppointed = Date.parse(that.document.model.get("DateAppointed"));
            if (documentDateAppointed) {
                that.$("#DateResponse").datepicker("setDate", documentDateAppointed);
            }

            var datePublished = new Date();
            that.$datePublish.datepicker("setDate", datePublished);

            that.$search.focus();
        },

        _getAddressGroups: function () {
            var groupNames = this.model.pluck("GroupName");
            var result = [];
            _.each(groupNames, function (groupName) {
                if (!groupName) {
                    return;
                }

                var groupList = groupName.split(";");
                result = _.union(result, groupList);
            });

            result = _.uniq(result);
            if (result.length === 0) {
                result.push(this.allGroup);
            } else {
                result.push(this.noneGroup);
            }

            return result;
        },

        _dataProcess: function (addresses) {
            var adressAfter = [];
            _.each(addresses, function (address) {
                if (!address.GroupName) {
                    return;
                }

                var isMultiGroup = false;
                if (address.GroupName) {
                    isMultiGroup = address.GroupName.indexOf(";") > -1 ? true : false;
                }

                if (isMultiGroup) {
                    var groups = address.GroupName.split(";");
                    _.each(groups, function (subAddress) {
                        var addresTmp = $.extend(true, [], address);
                        addresTmp.GroupName = subAddress.trim();
                        adressAfter.push(addresTmp);
                    })
                } else {
                    adressAfter.push(address);
                }
            });
            return adressAfter
        },

        _showCode: function (storeId) {
            /// <summary>
            /// Hiển thị số ký hiệu (template)
            /// </summary>
            var that = this;

            var docCode = that.document.model.get("DocCode");
            var codeId = that.document.model.get("CodeId");
            if (docCode != "") {
                that.$codeId.val(docCode);
            } else {
                that.$codeId.val('');
            }

            that.$code.empty('');

            var data = { storeId: storeId };
            if (this.theoLo) {
                if (!this.categoryId) {
                    return;
                }
                data.categoryId = this.categoryId;
            } else {
                data.categoryId = this.categoryId;
                data.documentId = this.documentId;
            }

            if (docCode != "") {
                return;
            }

            egov.request.GetCodes({
                data: data,
                success: function (codies) {
                    that.codies = codies;
                    if (codies && codies.length > 0) {
                        that.$code.append($.tmpl('<div class="list-group-item" value="${CodeId}" text="${Template}">${Template}</div>', codies));

                        if (docCode === "") {
                            that.$codeId.val(codies[0].Template);
                        }

                        if (codeId == 0) {
                            codeId = codies[0].CodeId;
                        }

                        //Dự kiến phát hành
                        if (that.plan && that.plan.publishInfo) {
                            codeId = that.plan.publishInfo.CodeId;
                        }
                        that.result.set('CodeId', codeId);
                    }
                }
            });
        },

        _selectCode: function (e) {
            /// <summary>
            /// Thay đổi mã hồ sơ
            /// </summary>
            /// <param name="e"></param>
            var code = $(e.target).closest(".list-group-item").text();
            var codeId = $(e.target).attr('value');

            this.result.set('CodeId', parseInt(codeId));
            this.$codeId.val(code);
        },

        _checkCodeUsed: function () {
            var docCode = this.$('#Code').val();
            var that = this;
            if (docCode === '') {
                return;
            }
            return;
            var organization = this.$("#Organization").text();

            // TienBV: thay đổi kịch bản xử lý chổ báo trùng số.
            // Mặc định ở client sẽ chỉ báo theo skh mà không check theo cqbh, để người dùng có thể so sánh, tránh trường hợp tên cơ quan người ta nhập không đồng bộ.
            egov.request.checkDocCodeIsUsed({
                data: {
                    doccode: docCode,
                    organization: ""
                },
                success: function (result) {
                    if (result.isUsed) {
                        that.$("label.warning[for='Code']").show();
                    } else {
                        that.$("label.warning[for='Code']").hide();
                    }
                }
            });
        },

        _selectApprover: function (e) {
            var target = $(e.target).closest("li");
            var approverId = target.attr("value");
            this.$("#ApproverName").val(target.text());
            this.$("#Approvers").val(approverId);
        },

        _autoCompleteApprover: function () {
            var that = this;
            var source = that.approvers;
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

        _showDepartment: function (deptId) {
            /// <summary>
            /// Hiển thị nơi lưu bản gốc mặc định.
            /// </summary>
            /// <param name="store">Sổ văn bản</param>

            // Kiểm tra nếu Sổ văn bản có thiết lập mặc định đơn vị quản lý thì mặc định chọn Đơn vị đó.
            // Không thì tìm đơn vị của người phát hành hiện tại
            if (deptId !== undefined) {
                this.$depts.find('option[value="' + deptId + '"]').attr('selected', 'selected');
                return;
            }

            var that = this;
            egov.dataManager.getCurrentDepartments({
                success: function (result) {
                    if (result.length > 0) {
                        deptId = result[0].departmentid;
                        that.$depts.find('option[value="' + deptId + '"]').attr('selected', 'selected');
                    }
                }
            });
        },

        _searchAddress: function () {
            /// <summary>
            /// Chèn nhanh address
            /// </summary>
            var that = this;
            var source = this.model.collect(function (a) {
                return { value: a.get('id'), label: a.get('label') };
            });
            this.$search.autocomplete({
                source: source,
                autoFocus: true,
                select: function (event, ui) {
                    var address = that.model.detect(function (u) {
                        return u.get('AddressId') === ui.item.value;
                    });

                    if (!address) {
                        return;
                    }

                    if (address.view == null) {
                        var addressItem = new AddressItm({
                            model: address,
                            parent: that
                        });

                        that.$address.prepend(addressItem.$el);
                        address.view = addressItem;
                    }

                    address.set('isSelected', !address.get('isSelected'));
                    that.$search.val('');
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
            if (!that.$dg.is(':not(:empty)')) {
                require(['transferExtend'], function (transferExtend) {
                    egov.views.dg = new transferExtend;
                    egov.views.dg.render(false, false, function () {
                        that._dongGui();
                    }, function () {
                        if (that.plan) {
                            window.setTimeout(function () {
                                that._bindData();
                            }, 300);
                        }
                        that.$dg.append(egov.views.dg.$el);
                    });
                });
            }
        },

        _getApprovers: function () {
            /// <summary>
            /// Trả về danh sách người ký duyệt
            /// </summary>
            var approvers = [];

            var approverIds = [];
            var allUsers = this.allUsers;
            var jobtitlies = this.allJobtitle;
            var jobDepts = this.allUserDeptPosition;

            var type_position = egov.setting.typePositionTitleJob;
            if (type_position === undefined) {
                type_position = 0;
            }

            var userPrimaryDepartment = egov.setting.user.primaryDepartment;

            if (type_position === 0) {
                jobDepts.forEach(function (jobDept) {
                    var jobId = jobDept.jobtitleid;
                    var isApprover = _.find(jobtitlies, function (job) {
                        return job.value === jobId && job.isApprover;
                    });

                    if (isApprover && egov.setting.transfer.showApproverByDepartment) {
                        isApprover = userPrimaryDepartment.indexOf(jobDept.idext) === 0;
                    }

                    if (isApprover) {
                        approvers.push({
                            value: jobDept.userid,
                            username: jobDept.username,
                            fullname: jobDept.fullname,
                            label: jobDept.fullname + " - " + jobDept.username,
                            isMain: true
                        });
                    }
                });
            } else {
                jobDepts.forEach(function (jobDept) {
                    var jobId = jobDept.positionid;
                    var isApprover = _.find(jobtitlies, function (job) {
                        return job.value === jobId && job.isApprover;
                    });

                    if (isApprover && egov.setting.transfer.showApproverByDepartment) {
                        isApprover = userPrimaryDepartment.indexOf(jobDept.idext) === 0;
                    }

                    if (isApprover) {
                        approvers.push({
                            value: jobDept.userid,
                            username: jobDept.username,
                            fullname: jobDept.fullname,
                            label: jobDept.fullname + " - " + jobDept.username,
                            isMain: true
                        });
                    }
                });
            }

            return approvers;
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

        _rePublish: function () {
            var that = this;
            var model = this.serialize();
            var doc = this.document.serialize();
            var searchAddr = this.$("#tempAddress").val();

            if (model.get("Address").length === 0 && searchAddr === "") {
                // egov.message.warning(_resource.noAddressSelected);
                egov.pubsub.publish(egov.events.status.warning, _resource.noAddressSelected);
            }
            var documentCopyId = that.document.model.get('DocumentCopyId');
            var addressResponse = that.result.get("DateResponeAddress");
            if (addressResponse) { } else {
                var addresses = that.result.get("Address");
                addressResponse = {};
                _.each(addresses, function (address) {
                    addressResponse[address] = that.result.get("DateResponse");
                });
            }

            if (addressResponse.length == 0) {
                egov.pubsub.publish(egov.events.status.error, "Chưa chọn đơn vị nhận");

                return;
            }

            // Lấy danh sách đồng gửi.
            var usersConsult = egov.views.dg.getUserConsults();

            // Danh sách user nhận văn bản đến
            var userHasReceiveDocuments = this._getUserReceiveVBDen();

            // Loại bỏ user nhận thông báo khi đã nhận văn bản đến.
            usersConsult = _.difference(usersConsult, _.keys(userHasReceiveDocuments));

            var destination = egov.views.dg.getDestination();

            // Lấy danh sách file được thêm vào
            var selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew') && !file.get("isRemoved")) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
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

            // Lấy danh sách file được xóa đi.
            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });
            removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

            var that = this;
            var documentCopyId = parseInt(this.document.model.get('DocumentCopyId'));

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            var targetComments = [];
            _.each(this.model.where({ isSelected: true }), function (addr) {
                targetComments.push({
                    label: addr.get("Name"),
                    value: '',
                    type: 0
                });
            });

            that.$(".address_0").each(function (idx, customAddr) {
                targetComments.push({
                    label: $(customAddr).find(".address-name").text(),
                    value: '',
                    type: 0
                });
            });
            targetComments = targetComments.concat(destination);

            egov.request.rePublish({
                data: {
                    documentCopyId: documentCopyId,
                    addressRepublish: JSON.stringify(addressResponse),
                    doc: JSON.stringify(doc),
                    files: JSON.stringify(selectedFiles),
                    modifiedFiles: JSON.stringify(modifiedFiles),
                    removeAttachmentIds: removeFiles,
                    userHasReceiveDocuments: JSON.stringify(userHasReceiveDocuments),
                    usersConsult: usersConsult,
                    searchAddr: searchAddr,
                    targetForComments: JSON.stringify(targetComments)
                },
                success: function (result) {
                    // Success
                    if (!result.error) {
                        that.$el.dialog('hide');
                        egov.pubsub.publish(egov.events.status.success, "Phát hành tiếp văn bản thành công!");
                        // close tab
                        that._closeTab([documentCopyId]);

                        if (model.get("Address").length === 0 && searchAddr !== "") {
                            egov.dataManager.clearAddress();
                        }
                    }
                    else {
                        // egov.message.error(result.error);
                        egov.pubsub.publish(egov.events.status.error, result.message);
                    }
                },
                error: function () {
                    // egov.message.error(_resource.error);
                    egov.pubsub.publish(egov.events.status.error, _resource.error);
                }
            });
        },

        _publish: function () {
            /// <summary>
            /// Phát hành văn bản.
            /// </summary>
            var model = this.serialize();
            var doc = this.document.serialize();
            var searchAddr = this.$("#tempAddress").val();

            if (model.get("Address").length === 0 && searchAddr === "") {
                // egov.message.warning(_resource.noAddressSelected);
                egov.pubsub.publish(egov.events.status.warning, _resource.noAddressSelected);
                return;
            }

            // Lấy Document.
            var doc = this.document.serialize();

            // Lấy danh sách đồng gửi.
            var usersConsult = egov.views.dg.getUserConsults();

            // Danh sách user nhận văn bản đến
            var userHasReceiveDocuments = this._getUserReceiveVBDen();

            // Loại bỏ user nhận thông báo khi đã nhận văn bản đến.
            usersConsult = _.difference(usersConsult, _.keys(userHasReceiveDocuments));
            usersConsult = usersConsult.length == 0 ? [] : usersConsult;

            var destination = egov.views.dg.getDestination();

            // Lấy danh sách file được thêm vào
            var selectedFiles = {};
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew') && !file.get("isRemoved")) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
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

            // Lấy danh sách file được xóa đi.
            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });
            removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });
            removeFiles = removeFiles.length == 0 ? [] : removeFiles;
            var that = this;
            var documentCopyId = parseInt(this.document.model.get('DocumentCopyId'));

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            var targetComments = [];
            _.each(this.model.where({ isSelected: true }), function (addr) {
                targetComments.push({
                    label: addr.get("Name"),
                    value: '',
                    type: 0
                });
            });

            that.$(".address_0").each(function (idx, customAddr) {
                targetComments.push({
                    label: $(customAddr).find(".address-name").text(),
                    value: '',
                    type: 0
                });
            });

            that.$(".address_mail").each(function (idx, customAddr) {
                targetComments.push({
                    label: $(customAddr).find(".address-name").text(),
                    value: '',
                    type: 4
                });
            });

            if (this.targetComments != null) {
                targetComments = targetComments.concat(this.targetComments);
            }

            targetComments = targetComments.concat(destination);

            egov.request.publishGov({
                data: {
                    documentCopyId: documentCopyId,
                    doc: JSON.stringify(doc),
                    files: JSON.stringify(selectedFiles),
                    modifiedFiles: JSON.stringify(modifiedFiles),
                    removeAttachmentIds: removeFiles,
                    publishinfo: JSON.stringify(model),
                    usersConsult: usersConsult,
                    userHasReceiveDocuments: JSON.stringify(userHasReceiveDocuments),
                    searchAddr: searchAddr,
                    targetForComments: JSON.stringify(targetComments)
                },
                success: function (result) {
                    // Success
                    if (result.success) {
                        that.$el.dialog('hide');
                        egov.pubsub.publish(egov.events.status.success, "Phát hành văn bản thành công!");
                        // close tab
                        that._closeTab([documentCopyId]);

                        if (model.get("Address").length === 0 && searchAddr !== "") {
                            egov.dataManager.clearAddress();
                        }
                    }
                    else {
                        // egov.message.error(result.error);
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                },
                error: function () {
                    // egov.message.error(_resource.error);
                    egov.pubsub.publish(egov.events.status.error, _resource.error);
                }
            });
        },

        _publishPlan: function () {
            /// <summary>
            /// Tạo mới dự kiến phát hành
            /// </summary>
            var that = this,
                model = this.serialize();

            var searchAddr = that.$("#tempAddress").val();
            if (model.get("Address").length === 0 && searchAddr === "") {
                egov.pubsub.publish(egov.events.status.warning, _resource.noAddressSelected);
            }
            else {
                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                var usersConsult = egov.views.dg.getUserConsults();
                var extInfo = egov.views.dg.getExtInfo();
                var documentCopyId = parseInt(this.document.model.get('DocumentCopyId'));
                var destination = JSON.stringify({
                    publishInfo: model,
                    usersConsult: usersConsult,
                    searchAddress: searchAddr,
                    extInfo: extInfo
                });

                egov.request.publishmentPlan({
                    data: {
                        documentCopyId: documentCopyId,
                        destination: destination
                    },
                    success: function (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.error);
                        } else {
                            egov.pubsub.publish(egov.events.status.success, result.success);

                            that.document.publishPlan = destination;
                            that.document.publishPlanId = result.id;
                            that.$el.dialog('destroy');
                        }
                    },
                    error: function (xhr) {
                        console.log(xhr.message);
                    }
                });
            }
        },

        _publishTheoLo: function () {
            if (!this.documentCopyIds || this.documentCopyIds.length <= 0) {
                return;
            }

            var model = this.serialize();
            var searchAddr = this.$("#tempAddress").val();
            if (model.get("Address").length === 0 && searchAddr === "") {

                // egov.message.warning(_resource.noAddressSelected);
                egov.pubsub.publish(egov.events.status.warning, _resource.noAddressSelected);
                return;
            }

            // Lấy danh sách đồng gửi.
            var usersConsult = _.pluck(egov.views.dg.getUserConsults(), "value");

            // Danh sách user nhận văn bản đến
            var userHasReceiveDocuments = this._getUserReceiveVBDen();

            // Loại bỏ user nhận thông báo khi đã nhận văn bản đến.
            usersConsult = _.difference(usersConsult, _.keys(userHasReceiveDocuments));

            var that = this;
            // Phát hành
            // egov.message.processing(egov.resources.common.transfering);
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

            egov.request.publishTheoLo({
                data: {
                    documentCopyIds: this.documentCopyIds,
                    publishinfo: JSON.stringify(model),
                    usersConsult: usersConsult,
                    userHasReceiveDocuments: JSON.stringify(userHasReceiveDocuments),
                    searchAddr: searchAddr
                },
                success: function (result) {
                    // Success
                    if (result.success) {
                        that.$el.dialog('hide');
                        egov.pubsub.publish(egov.events.status.success, result.success);
                        //var documentCopyIds = [];
                        //for (var i = 0; i < that.documents.length; i++) {
                        //    documentCopyIds.push(that.documents[i].DocumentCopyId);
                        //}
                        // close tab
                        that._closeTab(that.documentCopyIds);

                        // reload lại cây văn bản
                        that.parent.removeDocumentByIdsAndSetSelected(that.documentCopyIds, function () {
                            //  that.parent.loadNewerDocuments();
                        });

                        egov.callback(that.callback);
                    }
                    else {
                        // egov.message.error(result.error);
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.error);
                }
            });
        },

        _signAndPublish: function () {
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
                        that._publish();
                    }
                });
            });
        },

        _bindData: function (callback) {
            /// <summary>
            ///  bind dữ liệu đã cấu hình dự kiến vào form phát hành
            /// </summary>
            if (this.plan && this.plan.extInfo && egov.views.dg) {
                egov.views.dg.bindDataView(this.plan.extInfo);
            }

            if (typeof callback === 'function') {
                callback();
            }
        },

        _closeTab: function (documentCopyIds) {
            ///<summay>
            /// Đóng tab văn bản khi bàn giao thành công
            ///<para name="documentCopyIds"> Có thể là mảng chứa danh sách id văn bản hoặc id văn bản</para>
            ///</summay>
            if (this.document && this.document.tab) {
                this.document.tab.close2();
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
        },

        _getUserReceiveVBDen: function () {
            var result = {};

            var allUserDept = this.allUserDeptPosition;
            var allUsers = this.allUsers;
            var allDepts = this.allDepts;

            // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức vụ
            $("#privateDepartments").jstree("get_checked", null, false).each(function () {
                var node = $(this);
                var nodeId = this.id;
                var hasReceiveDocument = node.attr("hasreceivedocument") === "true";

                // Xác định node đang chọn là node phòng ban hay node user
                var isDeptNode = node.attr('rel') === 'dept';
                if (isDeptNode) {
                    var dept = _.find(allDepts, function (i) {
                        return i.value == nodeId;
                    });

                    if (dept) {
                        // Xác định node root: là node không có extend .
                        var isRoot = dept.idext.indexOf('.') < 0;
                        var userInDepts;
                        if (isRoot) {
                            userInDepts = _.filter(allUserDept, function (u) {
                                return u.departmentid == dept.idext && u.hasReceiveDocument == true;
                            });

                        } else {
                            userInDepts = _.filter(allUserDept, function (u) {
                                return u.idext.indexOf(dept.idext) == 0 && u.hasReceiveDocument == true;
                            });
                        }

                        _.each(userInDepts, function (u) {
                            result[u.userid] = dept.label;
                        });
                    }
                }
            });

            return result;
        },

        _bindPrivateDept: function () {
            var that = this;
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
        },

        _bindDepartments: function () {
            var that = this;
            var allDepts = that.allDepts;

            that.$('#privateDepts').customDropdown({
                css: {
                    width: 300,
                    height: 'auto'
                }
            });

            _.each(allDepts, function (dept) {
                var deptId = dept.value;
                var hasReceiver = _.find(that.allUserDepts, function (ud) {
                    return ud.departmentid === deptId && ud.hasReceiveDocument;
                });

                if (!hasReceiver) {
                    dept.state = "disabled";
                }
            });

            egov.utils.treeUtil.bindJsTree($('#privateDepartments'), false, true, false,
                              allDepts, [], [], null, []);

            $('#privateDepartments').bind('change_state.jstree', function (e, data) {
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
            that.$(".private-dept ul").empty();
            that.selectedDeptIds = [];
            that.targetComments = [];
            $("#privateDepartments .jstree-checked").each(function () {
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
                        that.selectedDeptIds.push(dept.idext);
                        var label = disabled ? dept.data + " (không có người nhận)" : dept.data;
                        that.$(".private-dept ul").append(_parseUserItem(dept.value, label));
                        that.targetComments.push({
                            label: label,
                            value: '',
                            disabled: disabled,
                            type: 0
                        });
                    }
                }
            });
        }
        //#endregion
    });

    /// <summary>Đối tượng thể hiện 1 cơ quan nhận</summary>
    var AddressItm = Backbone.View.extend({
        tagName: 'tr',
        events: {
            'click .checkbox': 'selected'
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            this.parent = option.parent;
            this.groupName = option.groupName;
            this.$mainAddress = this.parent.$mainAddress;

            this.model.set({ "DateResponse": null });
            this.model.set({ "DateResponseFormat": "" });

            this.model.on('change:isSelected', function (model, selected) {
                // var groupSelected = this.parent.groupSelected;

                //if (groupSelected && this.groupName !== groupSelected) {
                //    return;
                //}

                if (selected) {
                    this.$(':checkbox').prop('checked', true);
                    var dateResponse = model.get('DateResponse') != null ? "- " + model.get('DateResponseFormat') : "";
                    if (this.$mainAddress.find(':checkbox[value="' + model.get('AddressId') + '"]').length === 0) {
                        this.$mainAddress.append(_parseUserItem(model.get('AddressId'), model.get('Name'), dateResponse, this.groupName));
                    }
                } else {
                    this.$(':checkbox').prop('checked', false);
                    this.$mainAddress.find(':checkbox[value="' + model.get('AddressId') + '"]').parents('li.list-group-item').remove();
                }
            }, this);

            this.model.on('change:DateResponseFormat', function (model, selected) {
                var selector = "[id='dateItem" + model.get("AddressId") + "']";

                this.parent.$mainAddress.find(selector).text("- " + model.get("DateResponseFormat"));
            }, this);

            this.model.on('change:IsShow', this.toggle, this);

            this.render();
        },

        toggle: function () {
            if (!this.model.get("IsShow")) {
                this.$el.hide();
            } else {
                this.$el.show();
            }
        },

        render: function () {
            /// <summary>
            /// Hiển thị giao diện
            /// </summary>
            /// <returns type="">This</returns>
            this.$el.append($.tmpl(AddressItemTemplate, this.model.toJSON()));
            this.$el.attr("group-by", this.groupName);
            this.toggle();

            return this;
        },

        selected: function () {
            /// <summary>
            /// Chọn cơ quan
            /// </summary>
            var checked = this.$el.find(":checked").length > 0;
            this.model.set('isSelected', checked);
        }
    });

    //#endregion

    //#region Private Methods

    var _parseUserItem = function (value, name, dateResponse, groupName) {
        var template = '<li class="list-group-item address_{0}" group-by="' + groupName + '">\
                                <div class="row">\
                                    <label class="checkbox document-color">\
                                       <input name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                        <span class="document-color-1"><i class="icon-check"></i></span>\
                                    </label>\
                                    <span class="address-name" style="margin-left: 15px;">{1}</span>\
                                     <span id="dateItem{0}" style="color: red">{2}</span>\
                                </div>\
                            </li>';
        return $(String.format(template, value, name, dateResponse));
    };

    var _parseGroupName = function (name) {
        var template = '<tr group-name="{0}"><td> ' +
            '     <label class="checkbox document-color">' +
            '          <input class="group-name" name="checkbox[]" value="{0}" type="checkbox">' +
            '          <span class="document-color-1"><i class="icon-check"></i></span>' +
            '      </label>' +
            '  </td>  <td class="wraptext " style="font-weight: bold; width: 300px;" >' +
            '      {0} <i class="icon icon-arrow-left8 toggle-address pull-right" data-show="false" data-name="{0}"></i></td>' +
            '  </tr>';
        return $(String.format(template, name));
    };

    //#endregion

    return Publishment;
});