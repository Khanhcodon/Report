define(function () {
    "use strict";

    //#region Private Fields

    var _resource,
        DocumentModel,
        _template;

    _resource = egov.resources.document;
    DocumentModel = egov.models.document;
    _template = egov.template.document;

    var documentTemplates = egov.documentTemplates;
    var allDoctypes = egov.doctypes;

    //#endregion

    //#region Document View

    var Document = Backbone.View.extend({

        ///Loại hiển thị văn bản hồ sơ
        // Default : Chế độ hiển thị mở tab bình thườnghas
        // preView: hiển thị văn bản hồ, sơ ở khung xem trước văn bản
        // dialog: hiển thị xử lý văn bản trên dialog
        documentViewType: egov.enum.documentViewType.default,

        //Kiểm tra có phải tạo mơi văn bản hồ sơ hay không
        isCreate: false,

        //Kiểm tra id của categoryBusiness
        categoryBusinessId: 0,

        //Height thay đổi của textarea trích yếu khi nhập trích yếu quá dài. phục vụ cho việc hiển thị toolbar.
        heightChange: 0,

        // Lấy giá trị lĩnh vực văn bản đến
        docFieldIds: null,

        //Kiem tra hoi bao
        isAcknowledged: false,

        // Lấy giá trị từ khóa văn bản đến, tờ trình liên quan với hsmc
        keyword: null,

        // 20191205 VuHQ REQ-5
        dataFormTable: null,
        xoayObject: null,
        globalColumns: null,
        events: {
            'click #openDialogCommon': '_openDialogCommon',
            'change [name="cbDetail"]': 'checkDetail',
            'click': "removeRowSelected",
            'change .new-paper .paper-name': "_changePaper",
            'input input.paper-name': '_addPaper',
            'input input.fee-name': '_addFee',
            'input input.fee-price,input.fee-name': '_calPrice',
            'change input.fee-id': '_calPrice',
            "change #ddlDateAppointRange": "_changeDateAppoint",
            "click .changeDateCreated": "_changeDateCreated",
            'click .attachment-download-all': '_downloadAllAttachment',
            "click .changeWorkflowType": "_changeWorkflowType",
            'click #viewDocFee': "_openDocFee",
            'click #viewDocPaper': "_openDocPaper",
            'click .deletePaper': "_deleteDocPaper",
            'click .deleteFee': "_deleteDocFee",
            'click .delete-paper': "_deleteDocTypePaper",
            'click .delete-fee': "_deleteDocTypeFee",
            "click .doc-online-btn": "_docOnlineActions",
            "click .btnCheckCitizenInfo": "checkCitizenInfo",
            "click .file_attachments .attachment-download": "dowloadDocOnlineAttachment",
            "click .file_attachments .attachment-open": "openDocOnlineAttachment",
            "click .delApprover": "_delApprover",
            "click .delUpdateResult": "_delUpdateResult",
            "click .deleteDelayReason": "_deleteDelayReason",
            "click .btnResendLt": "_ReSendLienThong",
            "click .btnRecalled": "_recalledLienThong",
            "click .recalledList li": "_recalledAllLienThong",
            "click #btnToggleForm": "_changeDocumentFormView",

            // Code
            'change #StoreId': '_changeStoreId',
            "change #CategoryId": "_changeCategory",
            "click .ddlDocCodes a": "_selectDocCode",
            "blur #DocCode": "_checkDocCodeIsUsed",
            "keyup #DocCode": "suggesOrgan",

            "click #checkDocCode": "_seachDocCode",
            "change #IsCustomCode": "_allowUsingCustomCode",

            "click .docPaperManager": "_managerDoctypePaperFee",
            "click .changeDateAppointed": "renewals",
            "click .btnAcceptThuHoi": "_acceptThuHoi",

            // Handsontable
            "click #btnAddRow": "addRow_HT",
            "click #btnMerge": "merge_HT",
            "click #btnViewBCTH": "viewBCTH_HT",

            //viewLeaf 
            "click #btnViewLeaf": "showLeaf",
            "click #btnUploadImage": "showUploadImage",
            // 20200225 VUHQ
            "change #ddlYearReport, #bctuan, #bcthang, #bcquy, #bcnuanam, .bcngayplus, #InOutPlace": "changePeriod",
            "change #slTruong": "changeTruong",
            "change #slLop": "changeLop",
            "click #btnImportData": "_importDataFromExcel",
            "click .compare": "_showCompareData"
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            /// <param name="option" type="object">Các đối tượng {el: container} </param>
            var that = this,
                template;

            this.el = option.el;
            this.id = option.id;
            this.isOnlineRegistration = option.isOnlineRegistration;//dăng ký qua mang
            this.question = option.question;
            this.callback = option.callback;
            this.documentInfo = option.documentInfo;

            if (this.documentInfo) {
                if (!(this.documentInfo instanceof Backbone.Model)) {
                    this.documentInfo = new Backbone.Model(this.documentInfo);
                }

                this.model = new DocumentModel(this.documentInfo.toJSON());
            } else {
                this.model = new DocumentModel();
            }

            this.isPopUp = option.isPopUp;
            this.contentHTML = option.contentHTML;
            if (option.tab) {
                that.tab = option.tab;
                // Id văn bản gốc (dành cho chức năng trả lời).
                that.relationAnswerId = that.tab.model.get('relationId');
                that.relationType = that.tab.model.get('relationType') || 1;
            }

            that.isCreate = that.isCreate || option.isCreate;
            that.documentViewType = option.documentViewType || egov.enum.documentViewType.default;
            that.isPreView = that.isPreView || option.isPreView;
            that.isShowingPreview = false; // that.documentViewType === egov.enum.documentViewType.preView && egov.setting.userSetting.quickView === egov.enum.quickViewType.right;
            that.hasMinTemplate = false;
            that.survey = option.survey || that.model.get("CategoryBusinessId") == 32;
            // Danh sách các yêu cầu bổ sung
            that.supplements = [];

            // Hiển thị giao diện đầy đủ: khi mở văn bản hoặc khi xem preview ở dưới.
            that.isFullView = that.documentViewType !== egov.enum.documentViewType.preView ||
                egov.setting.userSetting.quickView === egov.enum.quickViewType.below;
            template = that.survey ? _template.desktopSurvey : _template.desktop;
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing, 2);

            if (option.copiedDocument) {
                this.copiedDocument = true;
                this.model = option.copiedDocument;
                var attachments = this.model.get('Attachments');
                if (attachments) {
                    _.each(attachments, function (item) {
                        item.isNew = true;
                    });
                }
            }

            this.registerToGlobal();

            require([template], function (DocumentTemplate) {
                that.template = DocumentTemplate;
                if (that.isOnlineRegistration) {
                    //mở đăng ký qua mạnng
                    that._openOnlineRegistration(option.id);
                    return;
                }

                if (that.question) {
                    //mở câu hỏi
                    that._openQuestion();
                    return;
                }

                // Tạm bỏ mở popup
                that.isPopUp = false;
                if (that.isPopUp) {
                    that._openDocumentInPopUp(option.model, option.templateCfg);
                    return;
                }

                if (that.isCreate) {
                    if (that.survey)
                        that._openCreateSurveyDocument(option.id);
                    else
                        that._openCreateDocument(option.id);
                    return;
                }
                if (that.survey) {
                    that._openEditSurveyDocument(option.id);
                }
                else
                    that._openEditDocument(option.id);
            });
        },

        registerToGlobal: function () {
            var previewDialog;
            var dialogBtn;
            var top = window.parent;
            if (_.isFunction(top.openFilePreview)) {
                return;
            }

            top.openFilePreview = function (documentId, attachmentId) {

                previewDialog = $("#filePreviewDialog", top.document);
                dialogBtn = $("#openFilePreview", top.document);

                if (previewDialog.length === 0) {
                    return;
                }

                if (previewDialog.find("iframe").length > 0) {
                    previewDialog.find("iframe").remove();
                }
                previewDialog.find(".modal-content").append('<iframe src="/AttachmentPreview/Index?id=' + documentId + '&currentId=' + attachmentId + '" style="width: 100%; height: 100%; border: none;"></iframe>');
                dialogBtn.click();
            }
        },
        checkDetail: function (e) {
            var thisCheck = $(e.target);
            if (thisCheck.is(':checked')) {
                $('#wrapUrgent, #wrapDateArrived, #wrapDocField, #wrapKeyword, #wrapDatePublished, #wrapSecurity').show();
            }
            else {
                $('#wrapUrgent, #wrapDateArrived, #wrapDocField, #wrapKeyword, #wrapDatePublished, #wrapSecurity').hide();
            }
        },

        selected: function (e) {
            var that = this,
                contextItems,
                contextModel,
                $target = that.$(e.target);
            require(["contextMenuView"], function () {
                contextModel = new egov.models.contextMenu({
                    trigger: 'right',
                    data: null,
                    style: {},
                    position: {
                        my: 'left top',
                        at: 'left bottom',
                        of: 'event'
                    },
                    hasCache: false,
                    isShowLoading: false
                });
                contextItems = new egov.models.contextMenuList();
                contextItems.add({
                    text: _resource.contextmenu.copyText,
                    value: 'copyText',
                    iconClass: 'icon-eye3',
                    preFunction: function (target) {
                        ZeroClipboard.setMoviePath('/Scripts/bkav.egov/libs/zeroclipboard/ZeroClipboard.swf');
                        var clip = new ZeroClipboard.Client();
                        clip.setText($target.val());
                        clip.glue(target);
                    }
                });
                contextItems.add({
                    text: _resource.contextmenu.selectAll,
                    value: 'selectAll',
                    iconClass: 'icon-eye3',
                    callback: function () {
                        $target.select();
                    }
                });
                contextModel.set('data', contextItems);
                $target.contextmenu(contextModel, e);
            });
        },

        removeRowSelected: function (e) {
            if (this.$(".document-relation").find(e.target).length == 0) {
                this.$(".document-relation").find(".rowSelected").removeClass('rowSelected');
            }
            if (this.$(".document-attachment").find(e.target).length == 0) {
                this.$(".document-attachment").find(".rowSelected").removeClass('rowSelected');
            }
        },
        _changeStoreId: function () {
            // <summary>
            // Thay đổi sổ hồ sơ
            //</summary>

            // - Khi khởi tạo hồ sơ một cửa
            var hasChangeDocCode = this.isHsmc && this.isCreate;

            if (hasChangeDocCode) {
                this._getDocCodes();
            }
            else {
                this._getInOutCode();
            }
        },

        _changeCategory: function (e) {
            var categoryEl = $(e.target).closest("#CategoryId");
            var cateId = categoryEl.val();
            this.model.set("CategoryId", cateId);
            if (this.model.get("CategoryBusinessId") != 1) {
                this._getDocCodes();
            }
            else {
                this._getInOutCode();
            }
        },

        _selectDocCode: function (e) {
            var docCodeElement = $(e.target).closest("a");
            this.$codeElement = this.isVanBanDen ? this.$("input[name='InOutCode']") : this.$("input[name='DocCode']");
            this.$codeElement.val(docCodeElement.text());
            var codeId = docCodeElement.attr("codeid");
            this.model.set("CodeId", codeId);
        },

        _checkDocCodeIsUsed: function () {
            var docCode = this.$("#DocCode").val();
            if (docCode == "") {
                return;
            }

            var that = this;
            var docId = that.isCreate ? null : that.model.get("DocumentId");

            // TienBV: thay đổi kịch bản xử lý chổ báo trùng số.
            // Mặc định ở client sẽ chỉ báo theo skh mà không check theo cqbh, để người dùng có thể so sánh, tránh trường hợp tên cơ quan người ta nhập không đồng bộ.
            egov.request.checkDocCodeIsUsed({
                data: {
                    doccode: docCode,
                    organization: "",
                    documentId: docId
                },
                success: function (result) {
                    if (result.isUsed) {
                        that.$("label.warning[for='DocCode']").show();
                        that.$("#checkDocCode").show();
                        that.$("#codeUsedList tbody").html($.tmpl('<tr><td>${DateCreated}</td><td>${Organization}</td><td>${Compendium}</td></tr>', result.codes));
                    } else {
                        that.$("label.warning[for='DocCode']").hide();
                        that.$("#checkDocCode").hide();
                    }
                }
            });
        },

        sucgesOrgan: function () {

        },

        _seachDocCode: function (e) {
            var docCode = this.$("#DocCode").val();
            if (docCode == "") {
                return;
            }

            egov.views.home.tab.addSearch(docCode, 1);
        },

        _allowUsingCustomCode: function (e) {
            var target = $(e.target).closest("#IsCustomCode");
            if (target.is(":checked")) {
                this.$("#InOutCode").removeAttr("readonly");
                this.model.set("IsCustomCode", true);
            } else {
                this.$("#InOutCode").attr("readonly", "readonly");
                this.model.set("IsCustomCode", false);
            }
        },

        _getInOutCode: function (callback) {
            this._showDocCodes(true);
        },

        _getDocCodes: function () {
            this._showDocCodes(false);
        },

        _showDocCodes: function (isVbDen) {
            var that = this,
                storeId = that.$("#StoreId").val(),
                categoryId = that.$("#CategoryId").val(),
                codeElement, codeSelectList,
                codeRequest;

            codeElement = isVbDen ? that.$("input[name='InOutCode']") : that.$("input[name='DocCode']");
            codeRequest = isVbDen ? egov.request.GetInOutCode : egov.request.GetDocCodes;
            codeSelectList = that.$(".ddlDocCodes");
            that.$codeElement = codeElement;

            if (storeId == "" || storeId == null) {
                return;
            }

            //if (isVbDen) {
            //    that.model.set("StoreId", storeId)
            //}

            codeSelectList.empty();

            if (that.stores) {
                var existStore = _.find(that.stores, function (item) {
                    return item.StoreId == storeId && item.CategoryId == categoryId;
                });

                if (existStore && existStore.Codes != null && existStore.Codes.length > 0) {
                    codeElement.val(existStore.Codes[0]);
                    _.each(existStore.Codes, function (item) {
                        codeSelectList.append($('<li><a href="#">' + item + '</a></li>'));
                    });
                    return;
                }
            }

            codeSelectList.html(egov.helper.loading);
            codeRequest({
                data: {
                    storeId: storeId,
                    categoryId: categoryId
                },
                success: function (codes) {
                    if (!that.stores) {
                        that.stores = [];
                    }

                    that.stores.push({
                        StoreId: storeId,
                        CategoryId: categoryId,
                        Codes: codes
                    });

                    codeSelectList.empty();
                    // codeElement.val(codes[0]);
                    var i = 0;
                    _.each(_.keys(codes), function (key) {
                        if (i == 0) {
                            codeElement.val(codes[key]);
                            that.model.set("CodeId", key);
                            i = 1;
                        }
                        codeSelectList.append($('<li><a href="#" codeid="' + key + '">' + codes[key] + '</a></li>'));
                    });
                }
            });
        },

        reload: function () {
            this.tab.reloadContent();
        },

        editContent: function (contentId) {
            /// <summary>
            /// Sửa nội dung văn bản
            /// </summary>
            /// <param name="ContentId" type="int">Nội dung văn bản cần sửa</param>
            /// <returns type=""></returns>
            var content = _.find(this.model.get('DocumentContents'), function (content) {
                return content.DocumentContentId === contentId;
            });

            if (content && this.forms[content.DocumentContentId]) {
                this.forms[content.DocumentContentId].renderDialog();
            }
        },

        isValid: function () {
            /// <summary>
            /// Validate form nhập
            /// </summary>
            /// <returns type="bool">Giá trị xác định trạng thái form có được valid hay không.</returns>
            var that = this,
                strKeyword,
                strDocfieldIds;

            this.$("label.error[for='DocCode']").hide();

            if (that.$("#ListDocField").val()) {
                strDocfieldIds = "";
                //Lấy chuỗi string lĩnh vực đã chọn
                $("#tblDocField input[type='checkbox']:checked").each(function () {
                    strDocfieldIds += $(that).attr("value") + ",";
                });
                strDocfieldIds = strDocfieldIds.substr(0, strDocfieldIds.length - 1);
                that.$("#DocFieldIds").val(strDocfieldIds);
            } else {
                that.$("#DocFieldIds").val('');
            }

            //    Kiểm tra check hồi báo gán giá trị true,false gán vào biến isAcknowledged
            if (that.$("#IsAcknowledged").is(':checked')) {
                that.isAcknowledged = true;
            }
            else {
                that.isAcknowledged = false;
            }

            //Yêu cầu dự thảo phát hành khi khởi tạo văn bản đi
            if (egov.setting && egov.setting.requirePublishPlan && this.model.get("CategoryBusinessId") == egov.enum.categoryBusiness.vbdi) {
                if (that.publishPlan == null) {
                    egov.pubsub.publish(egov.events.status.error, "Văn bản yêu cầu dự kiến phát hành trước khi chuyển.");
                    return;
                }
            }
            var checkWarningCompilation = that.warningCompilation == undefined ? true : false;
            if (!true) {
                if (!that.warningCompilation.ValidCompilation) {
                    egov.pubsub.publish(egov.events.status.warning, "Các đơn vị cấp dưới chưa báo cáo lên đủ, cần đôn đốc để tổng hợp dữ liệu");
                    return;
                }
            }
            // 20191204 VuHQ START REQ-5
            if (that.model.attributes["CategoryBusinessId"] == 4) {
                //that.dataFormTable.validateCells(function (valid) {
                //    if (!valid) {
                //        egov.pubsub.publish(egov.events.status.error, "Dữ liệu không hợp lệ, vui lòng kiểm tra lại thông tin.");
                //        return;
                //    }
                //})
            }

            if (this.isVanBanDen && (this.$("#InOutCode").val() === "" || this.$("#StoreId").val() === "")) {
                egov.pubsub.publish(egov.events.status.error, "Bạn chưa nhập Sổ hoặc Số đến cho văn bản.");
                return;
            }
            if (that.$("#CreateForm").length > 0) {
                return that.$("#CreateForm").valid();
            }

            if (that.$("#EditForm").length > 0) {
                return that.$("#EditForm").valid();
            }
        },

        setOriginContent: function () {
            var docContents = this.model.get("DocumentContents"),
                spellErrors;

            _.each(docContents, function (item) {
                var $temp = $("<div></div>");
                $temp.html(item.Content);
                spellErrors = $temp.find(".errorSpell");
                $.each(spellErrors, function (err) {
                    $(this)[0].outerHTML = $(this).html()
                });
                item.Content = $temp.html();
            });
        },

        setSigned: function () {
            this.model.set("HasCA", true);
        },

        DataAnalysisHandson: function (dataObject, xoayObject) {
            var newDataObject = [];
            $.each(dataObject, function (i, val) {
                var index = 0;
                var newDataObjectDetail = {};

                Object.keys(val).forEach(function (key) {
                    if (index != xoayObject.GiaTriIndex) {
                        if (index == xoayObject.CatalogIndex) {
                            for (var i = 0; i < xoayObject.CatalogCount; i++) {
                                newDataObjectDetail[xoayObject.CatalogValuesAscii[i]] = val[xoayObject.GiaTriNameAscii];
                            }
                        }
                        else {
                            if (!_.contains([xoayObject.GiaTriNameAscii, xoayObject.CatalogNameAscii, "pos"], key)) {
                                newDataObjectDetail[key] = val[key];
                            }
                        }
                    }
                    index++;
                });

                newDataObject.push(newDataObjectDetail);
            });
            return newDataObject;
        },

        renderDataModel: function (config, data, options) {
            var dataModel = [];

            if (config && config.extra && config.extra.columnSetting) {
                var listKey = config.extra.columnSetting;
                var arrayKey = Object.entries(listKey);
                var listkeyModel = []
                for (var i = 0; i < arrayKey.length; i++) {
                    var obj = arrayKey[i];
                    obj = arrayKey[i];
                    var key = obj[0].split("!!")[0];
                    var valueObj = obj[1];
                    listkeyModel.push({
                        key: key,
                        DataFieldModel: valueObj.DataFieldModel,
                        FieldModel: valueObj.FieldModel,
                        PeriodModel: valueObj.PeriodModel,
                        LocalityModel: valueObj.LocalityModel,
                        DiaggregationModel: valueObj.DiaggregationModel
                    });
                }
                _.each(data, function (item) {
                    var IndicatorKey = null

                    for (var i = 0; i < listkeyModel.length; i++) {
                        if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorCode") {
                            IndicatorKey = item[listkeyModel[i].key];
                        }
                    }
                    if (IndicatorKey) {
                        for (var i = 0; i < listkeyModel.length; i++) {
                            if (listkeyModel[i].FieldModel == "TypeData") {
                                var model = {
                                    IndicatorKey: IndicatorKey, LocalityKey: options.LocalityKey, TimeKey: options.TimeKey, TimeType: options.TimeType,
                                    OrganizationKey: options.OrganizationKey, TypeData: "", Measure: "", Disaggregation: listkeyModel[i].DiaggregationModel
                                };
                                model.Measure = item[listkeyModel[i].key];
                                if (listkeyModel[i].PeriodModel != "TC") {
                                    if (options.TimeType == "monthkey") {
                                        if (model.TimeKey % 10000 == 101) {
                                            model.TimeKey = model.TimeKey - 10000 + 1100;
                                        } else {
                                            model.TimeKey = options.TimeKey - 100;
                                        }
                                    }
                                }

                                if (listkeyModel[i].LocalityModel != "TC" && listkeyModel[i].LocalityModel) {
                                    model.LocalityKey = listkeyModel[i].LocalityModel;
                                }

                                model.TypeData = listkeyModel[i].DataFieldModel;
                                dataModel.push(model);
                            }
                        }
                    }

                });
            }
            return dataModel;
        },

        renderDataAddIndicator: function (config, data, dataAdd, indexStart, length, isDepart) {
            if (config && config.extra && config.extra.columnSetting) {
                if (isDepart) {
                    for (var j = indexStart; j < indexStart + length; j++) {
                        var key1 = _.keys(data[j])[1];
                        var key2 = _.keys(data[j])[2];
                        data[j][key1] = dataAdd[j - indexStart].code;
                        data[j][key2] = dataAdd[j - indexStart].name;
                    }
                    return data;
                }
                var listKey = config.extra.columnSetting;
                var arrayKey = Object.entries(listKey);
                var listkeyModel = []
                for (var i = 0; i < arrayKey.length; i++) {
                    var obj = arrayKey[i];
                    obj = arrayKey[i];
                    var key = obj[0].split("!!")[0];
                    var valueObj = obj[1];
                    listkeyModel.push({
                        key: key,
                        DataFieldModel: valueObj.DataFieldModel,
                        FieldModel: valueObj.FieldModel
                    });
                }
                for (var j = indexStart; j < indexStart + length; j++) {
                    for (var i = 0; i < listkeyModel.length; i++) {
                        if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorCode") {
                            data[j][listkeyModel[i].key] = dataAdd[j - indexStart].code;
                        }

                        if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorName") {
                            data[j][listkeyModel[i].key] = dataAdd[j - indexStart].name;
                        }

                        if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorUnit") {
                            data[j][listkeyModel[i].key] = dataAdd[j - indexStart].unit;
                        }
                    }
                }

            }
            return data;
        },

        getIndicatorData: function (config, data, dataAdd, indexStart, length) {
            var that = this;
            if (config && config.extra && config.extra.columnSetting) {
                var listKey = config.extra.columnSetting;
                var arrayKey = Object.entries(listKey);
                var listkeyModel = []
                for (var i = 0; i < arrayKey.length; i++) {
                    var obj = arrayKey[i];
                    obj = arrayKey[i];
                    var key = obj[0].split("!!")[0];
                    var valueObj = obj[1];
                    listkeyModel.push({
                        key: key,
                        DataFieldModel: valueObj.DataFieldModel,
                        FieldModel: valueObj.FieldModel
                    });
                }
                var code, name, unit
                for (var i = 0; i < listkeyModel.length; i++) {
                    if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorCode") {
                        code = data[indexStart][listkeyModel[i].key];
                    }

                    if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorName") {
                        name = data[indexStart][listkeyModel[i].key];
                    }

                    if (listkeyModel[i].FieldModel == "Indicator" && listkeyModel[i].DataFieldModel == "IndicatorUnit") {
                        unit = data[indexStart][listkeyModel[i].key];
                    }
                }
            }
            return {
                code: code,
                name: name,
                unit: unit,
                timekey: that.TimeKeyName,
                organization: that.$el.find("#InOutPlace").val()
            };
        },

        addIndicator: function (isdepart) {
            var that = this;
            require(['addIndicator'], function (addIndicator) {
                var aI = new addIndicator({ document: that, isdepart: isdepart });
            });
        },

        editDataRow: function () {
            var that = this;
            require(['editDataRow'], function (editDataRow) {
                var aI = new editDataRow({ document: that });
            });
        },

        thresHold: function () {
            var that = this;
            require(['thresHold'], function (thresHold) {
                var aI = new thresHold({ document: that });
            });
        },

        buildQuery: function () {
            var that = this;
            if (that.model.attributes["CategoryBusinessId"] == 64 && that.isCreate) {
                require(['buildQuery'], function (buildQuery) {
                    var buildQueryCompilation = new buildQuery({ document: that });
                });
            }
        },

        serialize: function () {
            /// <summary>
            /// Serialize form văn bản sang json
            /// </summary>
            /// <returns type=""></returns>
            var that = this,
                control,
                val,
                result,
                fees = [],
                papers = [],
                checked,
                paperName,
                feeName;
            var kybaocao = that.findKyBaoCao(that.model.get("DocTypeId"));
            // Lấy giá trị các thuộc tính từ control tương ứng.
            // Mặc định đặt Id của Control = tên thuộc tính để lấy.
            for (var attr in that.model.attributes) {
                if (attr === "IsCustomCode" || attr === "IsActivated") {
                    continue;
                }

                control = that.$el.find('[name="' + attr + '"]');
                if (control.parents('.formTmp').length > 0) {
                    continue;
                }

                if (control.length > 0 && (control.is("input") || control.is("select") || control.is("textarea"))) {
                    val = control.val();
                    if (val === '' && that.model.get(attr) == null) {
                        continue;
                    }
                    that.model.set(attr, stripHtml(val));
                }
            }

            that.model.set("TypeReturned", Number(that.$el.find('[name="TypeReturned"]:checked').val()));

            var isSuccess = that.model.get('IsSuccess');
            if (isSuccess !== true && isSuccess !== false) {
                that.model.set('IsSuccess', null);
            }

            //Gán tạm check box hồi báo do trả về : on
            that.model.set('IsAcknowledged', this.isAcknowledged);
            result = that.model.toJSON();
            result.CommentList = [];

            result.Contents = [];

            if (that.model.attributes["CategoryBusinessId"] == 4 || that.model.attributes["CategoryBusinessId"] == 64) {
                var dataForm = JSON.parse(JSON.stringify(that.dataFormTable.getSourceData()));
                var dataFormOrigin = that.dataFormTable.getData();
                var defaultData = JSON.parse(JSON.stringify(that.dataFormTable.getSourceData()));
                if (that.xoayObject) {
                    dataForm = that.DataAnalysisHandson(dataForm, that.xoayObject)
                }
                // day du lieu
                dataForm = $.extend(JSON.parse(JSON.stringify(that.dataFormTable.getSourceData())), that.dataFormTable.getSourceData());
                // 20191226 VuHQ START Cù Trọng Xoay
                var formCode = JSON.parse(that.configHandsontable);

                var header = [];
                var data = [];
                var nestedHeaders = [];

                // chuyen du lieu formula sang data neu co START
                for (var i = 0; i < dataForm.length; i++) {
                    var row = dataForm[i];
                    var j = 0;

                    Object.keys(row).forEach(function (key) {
                        if (dataFormOrigin[i] != undefined) {
                            if (dataForm[i][key] != dataFormOrigin[i][j])
                                dataForm[i][key] = dataFormOrigin[i][j];
                        }
                        j++;

                    });
                }

                // chuyen du lieu formula sang data neu co END

                if (that.xoayObject != null) {
                    var xoayObject = that.xoayObject;
                    //var xoayInfo = formCode.extra.xoayInfo;
                    //var xoayObject = formCode.extra.xoayObject;
                    //var xoayHeadersAscii = formCode.extra.xoayHeadersAscii
                    var xoayDataForm = [];
                    var xoayDataFormDetail = [];
                    var tempCurrentCatalogValue = [];

                    for (var i = 0; i < dataForm.length; i++) {
                        var row = dataForm[i];

                        $.each(xoayObject.CatalogValues, function (index, catalogValue) {
                            xoayDataFormDetail = {};

                            Object.keys(row).forEach(function (key) {
                                if (!xoayObject.CatalogValuesAscii.includes(key))
                                    xoayDataFormDetail[key] = row[key];
                            });

                            tempCurrentCatalogValue = that.allCatalogValues.filter(function (el) { return el.Value == catalogValue; });

                            if (tempCurrentCatalogValue == undefined || tempCurrentCatalogValue.length == 0)
                                xoayDataFormDetail[xoayObject.CatalogNameAscii] = catalogValue;
                            else
                                xoayDataFormDetail[xoayObject.CatalogNameAscii] = tempCurrentCatalogValue[0].CatalogKey;

                            xoayDataFormDetail[xoayObject.GiaTriNameAscii] = row[xoayObject.CatalogValuesAscii[index]];
                            xoayDataForm.push(xoayDataFormDetail);
                        });
                    }

                    dataForm = xoayDataForm;
                }
                // 20191226 VuHQ END Cù Trọng Xoay

                for (var i = 0; i < dataForm.length; i++) {
                    var row = dataForm[i];
                    dataForm[i]["pos"] = i + 1;
                    var keys = Object.keys(row)
                    for (var j = 0; j < keys.length; j++) {
                        //if (!row[keys[j]]) {
                        //    dataForm[i][keys[j]] = 0;
                        //}

                        //if (row[keys[j]] === true) {
                        //    dataForm[i][keys[j]] = 1;
                        //}

                        //if (row[keys[j]] == "false") {
                        //    dataForm[i][keys[j]] = 0;
                        //}
                    }
                }

                // 20200227 Lấy CatalogKey thay cho CatalogName START
                var j;
                _.each(dataForm, function (dataFormRow) {
                    j = 0;
                    Object.keys(dataFormRow).forEach(function (key) {
                        if (that.dataFormTable.getDataType(0, j, 0, j) == 'dropdown') {
                            var catalogValues = that.allCatalogValues.filter(d => d.Value == dataFormRow[key]);
                            if (catalogValues.length > 0 && catalogValues[0].CatalogKey != null && catalogValues[0].CatalogKey != '')
                                dataFormRow[key] = catalogValues[0].CatalogKey;
                        }
                        j++;
                    });
                });
                // 20200227 Lấy CatalogKey thay cho CatalogName END
                var datekey = that.serializeDatePublishedStandard(kybaocao);

                var keyLocality = that.$el.find("#InOutPlace").val();
                var timekey = that.formatDateTimeKey(datekey);
                var dataModel = that.renderDataModel(formCode, dataForm, { OrganizationKey: keyLocality, LocalityKey: keyLocality, TimeKey: timekey, TimeType: that.TimeKeyName })
                result.CompilationData = JSON.stringify(dataModel);
                result.Note = JSON.stringify(dataForm);
                if (dataForm.length > 0) {
                    var keys = Object.keys(dataForm[0]);
                    result.NoteCSV = keys.join(",") + "\n";
                    dataForm.forEach(item => {
                        for (var i = 0; i < keys.length; i++) {
                            if (i > 0) {
                                result.NoteCSV += ",";
                            }

                            result.NoteCSV += typeof item[keys[i]] === "string" && item[keys[i]].includes(",") ? `"${item[keys[i]]}"` : item[keys[i]];
                        }
                        result.NoteCSV += "\n";
                    });
                }
                var config = that.configHandsontable ? JSON.parse(that.configHandsontable) : {};
                config["xoayData"] = JSON.stringify(defaultData);
                config["xoayObject"] = that.xoayObject;
                var process = that.model.get("ProcessInfo");
                var obj = {};
                if (process != undefined && JSON.parse(process).headerFooter != undefined) {
                    var dataHeader = JSON.parse(JSON.parse(process).headerFooter);
                    obj.FormHeader = dataHeader.FormHeader;
                    obj.FormFooter = dataHeader.FormFooter;
                }
                else if (that.model.attributes != null && that.model.attributes.DocumentContents.length > 0) {

                    var form = that.model.attributes.DocumentContents[0];
                    if (form != undefined) {
                        var formHeader = form.FormHeader;
                        var formFooter = form.FormFooter;
                        var templateKeyCodes = [];
                        if (formHeader != undefined) {
                            templateKeyCodes = formHeader.match(/@@(.*?)@@/gm) ? formHeader.match(/@@(.*?)@@/gm) : [];
                            _.each(formHeader.match(/##(.*?)##/gm),
                                function (v) {
                                    templateKeyCodes.push(v);
                                });
                        }
                        var templateKeyCodesFooter = [];
                        if (formFooter != undefined) {
                            templateKeyCodesFooter = formFooter.match(/@@(.*?)@@/gm) ? formFooter.match(/@@(.*?)@@/gm) : [];
                            _.each(formFooter.match(/##(.*?)##/gm),
                                function (v) {
                                    templateKeyCodesFooter.push(v);
                                });
                        }
                        var timeKey = $("#ddlYearReport option:selected").val();
                        if (templateKeyCodes.length > 0) {
                            _.each(templateKeyCodes, function (templateKeyCode) {
                                $.ajax({
                                    type: "POST",
                                    async: false,
                                    //contentType: "application/json; charset=utf-8",
                                    //dataType: "json",
                                    url: '/Document/GetDataTemplateKeys',
                                    traditional: true,
                                    data: { 'templateKeyCode': templateKeyCode, 'formId': that.FormId, 'timeKey': timeKey },
                                    success: function (response) {
                                        if (response.Success) {
                                            if (response.Type && (response.Type == 4 || response.Type == "default")) {
                                                formHeader = formHeader.replace(templateKeyCode, response.NewValue || "");
                                            } else
                                                formHeader = formHeader.replace(templateKeyCode, "");
                                        }
                                        else {
                                            formHeader = formHeader.replace(templateKeyCode, "");
                                        }
                                    }
                                });
                            });
                        }
                        if (templateKeyCodesFooter.length > 0) {
                            _.each(templateKeyCodesFooter, function (templateKeyCode) {
                                $.ajax({
                                    type: "POST",
                                    async: false,
                                    //contentType: "application/json; charset=utf-8",
                                    //dataType: "json",
                                    url: '/Document/GetDataTemplateKeys',
                                    traditional: true,
                                    data: { 'templateKeyCode': templateKeyCode, 'formId': that.FormId, 'timeKey': timeKey },
                                    success: function (response) {
                                        if (response.Success) {
                                            if (response.Type && (response.Type == 4 || response.Type == "default")) {
                                                formFooter = formFooter.replace(templateKeyCode, response.NewValue || "");
                                            } else
                                                formFooter = formFooter.replace(templateKeyCode, "");
                                        }
                                        else {
                                            formFooter = formFooter.replace(templateKeyCode, "");
                                        }
                                    }
                                });
                            });
                        }

                        obj.FormHeader = formHeader;
                        obj.FormFooter = formFooter;
                    }
                }
                config["headerFooter"] = JSON.stringify(obj);
                result.ProcessInfo = JSON.stringify(config);
            }
            else if (that.model.attributes["CategoryBusinessId"] == 8) {
                var data = CKEDITOR.instances[that.cid + '_explicit_template'].getData();
                result.Note = data;
            }
            else if (that.model.get("CategoryBusinessId") == 32) {
                var donvi;
                var ins = that.$el.find('textarea[id$=SurveyReport]').attr("id");
                if (ins) {
                    that.model.set("SurveyReport", CKEDITOR.instances[ins].getData());
                }

                if (egov.views.survey) {
                    //Lấy danh sách đơn vị nhận
                    donvi = egov.views.survey.getDestination();
                }

                var obj = {
                    SurveyConfig: JSON.stringify(egov.views.survey.surveyDesLogic.JSON).replace(/&quot;/g, '\\"') || "",
                    SurveyCriteria: that.model.get("SurveyCriteria") || "",
                    SurveyReport: that.model.get("SurveyReport") || "",
                    DonViNhan: JSON.stringify(donvi)
                };
                result.ProcessInfo = JSON.stringify(obj);
            }

            //if (that.CompilationId) {
            //    var config = JSON.parse(that.ConfigFunction)
            //    var dlth = that._compilationData(dataForm, config, result.InOutPlace);
            //    result.CompilationData = JSON.stringify(dlth);
            //}

            result.OrganizationCode = that.$el.find("#InOutPlace").val();
            result.InOutPlace = that.$el.find("#InOutPlace > option:selected").text().trim();



            //var tesst = that._compilationData(dataForm)
            _.each(result.DocumentContents, function (content) {
                //content.Content = content.ContentName;
                if (that.model.attributes["CategoryBusinessId"] == 4 || that.model.attributes["CategoryBusinessId"] == 64) {
                    content.Content = JSON.stringify(dataForm);
                }
                else {
                    var data = CKEDITOR.instances[that.cid + '_explicit_template'].getData();
                    content.Content = data;
                }

                content.ContentUrl = String.format("{0}{1}_saved.xlsx", egov.setting.eform.forderReport, content.Url);
                result.Contents.push(JSON.stringify(content));
            });

            result.DocumentContents = [];
            result.RelationModels = result.Relations;
            if (this.relationType == 4 && result.RelationModels && result.RelationModels.length > 0) {
                // thu hồi
                _.each(result.RelationModels, function (r) {
                    r.RelationType = 4;
                });
            }

            result.Compendium = escape(result.Compendium);
            result.Comments.Content = result.Comment;

            // 20200325 TaiDA
            if (that.model.attributes["CategoryBusinessId"] == 4 || that.model.attributes["CategoryBusinessId"] == 64) {
                result.Comments.Diff = { Data: JSON.stringify(that.dataFormTable.getSourceData()) };
            }
            else if (that.model.attributes["CategoryBusinessId"] == 8) {
                result.Comments.Diff = {
                    Data: CKEDITOR.instances[that.cid + '_explicit_template'].getData()
                };
            }

            result.DocPapers = [];
            result.DocFees = [];
            this.$(".papers .doc-paper").each(function (paper) {
                checked = $(this).find(":checked");
                if (checked.length === 1) {
                    paperName = checked.siblings(".paper-name").is("input") ? checked.siblings(".paper-name").val() : checked.siblings(".paper-name").text();
                    if (!paperName) {
                        return;
                    }
                    result.DocPapers.push({
                        PaperName: paperName,
                        Amount: checked.siblings(".paper-amount").val(),
                        Type: egov.enum.paperType.TiepNhan
                    });
                }
            });
            this.$(".fees .doc-fee").each(function (paper) {
                checked = $(this).find(":checked");
                if (checked.length === 1) {
                    feeName = checked.siblings(".fee-name").is("input") ? checked.siblings(".fee-name").val() : checked.siblings(".fee-name").text();
                    if (!feeName) {
                        return;
                    }
                    result.DocFees.push({
                        FeeName: feeName,
                        Price: checked.siblings(".fee-price").val().split(".").join(""),
                        Type: egov.enum.feeType.TiepNhan
                    });
                }
            });

            result.Fees = fees;
            result.Papers = papers;

            if (this.relationAnswerId) {
                result.relationAnswerId = this.relationAnswerId;
                result.TransferTypeInEnum = this.relationType == 4 ? "ThuHoi" : "TraLoi";
            }

            result.Supplementary = this.supplements.join("\n");
            result.HasDateOverdue = this.$("#HasDateOverdue").prop("checked");
            result.HasDateAppointed = this.$("#HasDateAppointed").prop("checked");
            result.IsCustomCode = this.$("#IsCustomCode").prop("checked");
            result.ExpireProcess = this.$("#ddlDateAppointRange").val();

            result.DateOverdue = result.HasDateOverdue ? this._getDate("DateOverdue") : null;
            result.DateReceived = null;
            result.DatePublished = that.serializeDatePublished(kybaocao);
            result.TimeKey = that.TimeKey;
            result.DateResponse = this._getDate("DateResponse");
            result.DateArrived = this._getDate("DateArrived");
            result.DateAppointed = (this.isHsmc || result.HasDateAppointed) ? this._getDate("DateAppointed") : null;
            result.DateCreated = this._getDate("DateCreated");

            result.FormId = that.FormId;

            if (result.DateModified == "") {
                result.DateModified = null;
            }
            if (result.DateResult) {
                result.DateResult = null;
            }

            //result.Attachments = null;
            if (result.DocTypeId == "") {
                result.DocTypeId = null;
            }
            var catalogs = {};
            var $catalogValue = $(".catalog-name");
            $catalogValue.each(function (item) {
                var value = $(this).val();
                var key = $(this).attr("data-name");
                var catalog = {};
                catalogs[key] = value;
            })


            if (that.model.get("CategoryBusinessId") == 32) {
                result.CategoryId = 5;
            }
            result.Address = JSON.stringify(catalogs);
            return result;
        },

        _compilationData: function (data, configFormNew, organ) {
            var that = this;
            var options =
            {
                'huyenthixathanhpho': {
                    'name': 'huyenthixathanhpho',
                    'type': 'None',
                    'query': '',
                    'data': ""
                },
                'sohongheo': {
                    'name': 'sohongheo',
                    'type': 'Sum',
                    'query': '$.sohongheo',
                },
                'sohocanngheo': {
                    'name': 'sohocanngheo',
                    'type': 'Sum',
                    'query': '($.sohocanngheo / $.tongsohodancu) * 100',
                },
                'tongsohodancu': {
                    'name': 'tongsohodancu',
                    'type': 'Sum',
                    'query': '$.tongsohodancu',
                }
            };
            var result = {};
            result = _.mapObject(configFormNew, function (val, key) {
                var value = that.funtioncompilationData(data, organ, val);
                return value;
            });

            return result;
            //var data = Enumerable.From(data).Sum("$.b")
        },
        funtioncompilationData: function (data, organ, option) {
            if (option.type == 'Sum') {
                var data = Enumerable.from(data).sum(option.query);
                return data;
            }
            if (option.type == 'Average') {
                var data = Enumerable.from(data).average(option.query);
                return data;
            }
            if (option.type == 'Count') {
                var data = Enumerable.from(data).count(option.query);
                return data;
            }
            if (option.type == 'Max') {
                var data = Enumerable.from(data).max(option.query);
                return data;
            }
            if (option.type == 'Min') {
                var data = Enumerable.from(data).min(option.query);
                return data;
            }
            if (option.type == 'None') {
                return organ;
            }
            return null;
        },

        _serializeDynamicForm: function (form) {
            var formJson = "{";
            var formIdStr = form.FormId.replace(/[\-&]+/g, '');
            var docFieldJson = eForm.database.JsonSerialize3(formIdStr);
            formJson += "\"FormId\": \"" + formIdStr + "\",";
            formJson += "\"GlobalCode\":\"" + form.GlobalCode + "\",";
            formJson += "\"Description\":\"" + form.Description + "\",";
            formJson += "\"DocFieldJson\":" + docFieldJson;
            formJson += "}";
            form.Content = formJson;
            return form;
        },

        _getDate: function (dateNameId) {
            var dateElement = this.$("[name='" + dateNameId + "']");
            var result = Date.parse(dateElement.val(), "HH:mm dd/MM/yyyy");
            if (result != undefined) {
                return result.toServerString();
            }

            var date = dateElement.datepicker("getDate");
            if (date == undefined) {
                // Ko xác định được từ datepicker thì lấy giá trị trong textbox
                if (dateElement.val() != '') {
                    date = Date.parse(dateElement.val());
                }
            }

            if (date == undefined || date.length === 0) {
                // Ko xác định dc nữa thì lấy từ model (khi ko hiển thị lên giao diện)
                var dateInModel = this.model.get(dateNameId);
                return (dateInModel == null || dateInModel == "") ? null : new Date(dateInModel).toServerString();
            }

            date.hours(new Date().hours());
            date.minutes(new Date().minutes());

            if (dateNameId == "DateAppointed" && this.isHsmc) {
                date.hours(this.defaultHour == null ? new Date().hours() : this.defaultHour);
                date.minutes(this.defaultMinute == null ? new Date().minutes() : this.defaultMinute);
            }

            return date.toServerString();
        },

        _getReturnResultTimeDefault: function () {
            // Lấy giờ trả kết quả mặc định cho hsmc
            var dateAppointed = this.model.get("DateAppointed");
            var date = Date.parse(dateAppointed);
            if (date) {
                this.defaultHour = date.hours();
                this.defaultMinute = date.minutes();
            }
        },

        hasInputChange: function () {
            //Kiểm tra xem thông tin văn bản có gì thay đổi không

            if (this.hasChangeInfo) {
                return;
            }

            if (this.supplements && this.supplements.length > 0) {
                this.hasChangeInfo = true;
                return;
            }

            var control,
                val;

            for (var attr in this.model.attributes) {
                control = this.$el.find('[name="' + attr + '"]');
                if (control.length === 0) {
                    continue;
                }

                if (control.is("input[type=checkbox]")) {
                    //Nếu là checkbox => kiểm tra giá trị trước và sau
                    if ((control.is(":checked") && this.model.get(attr) == "0")
                        || (!control.is(":checked") && this.model.get(attr) == "1")) {
                        this.hasChangeInfo = true;
                        return;
                    }

                    continue;
                }

                if (control.is("input[type=text]:not([readonly])") || control.is("select:not([disabled])")
                    || control.is("textarea:not([readonly])") || control.is("input[type=radio]")) {
                    val = control.val();

                    if (val !== null && this.model.get(attr) !== null) {
                        //Nếu chỉ thêm ý kiến xử lý => bỏ qua
                        if ((val === '') || attr === "Comment") {
                            continue;
                        }

                        if (attr === "DateArrived" || attr === "DatePublished" || attr === "DateAppointed") {
                            continue;
                        }

                        //nếu thông tin ban đầu của văn bản khác với giá trị lấy trên form
                        if (this.model.get(attr).toString() !== val.toString()) {
                            this.hasChangeInfo = true;
                            return;
                        }
                    }
                }
            }
        },

        hasChange: function (success) {
            /// <summary>
            /// Giá trị xác định văn bản đang mở có thay đổi hay không.
            /// </summary>
            /// <returns type=""></returns>

            var that = this,
                selectedFiles,
                removeFiles,
                modifiedFiles,
                changedFile,
                result = false;
            if (typeof success === 'undefined') return;

            if (!this.model) {
                return success(false);
            }

            if (this.isSaveChanged) {
                return success(false);
            }

            //Không phải văn bản đang xử lý
            if ((this.model.get("Status") !== 2 && this.model.get("Status") !== 1) || this.model.get("UserCurrentId") !== egov.setting.user.userId) {
                return success(false);
            }

            if (this.isCreate) {
                return success(true);
            }

            this.hasInputChange();

            if (this.hasChangeInfo) {
                return success(true);
            }

            // Kiểm tra thay đổi nội dung công văn
            if (that.hasChangeContent) {
                return success(true);
            }

            // Kiểm tra thay đổi văn bản liên quan
            if (that.relations && that.relations.hasChangeRelation) {
                return success(true);
            }

            if (!that.attachments) {
                return success(false);
            }

            that.attachments.confirmAttachments(function () {
                //Do những file đã từng remove vẫn còn trong model nên việc kiểm tra có remove file hay không được set bên attachments
                removeFiles = that.attachments.model.select(function (file) {
                    return file.get('hasRemoved');
                });

                modifiedFiles = that.attachments.modifiedFiles;

                if (removeFiles.length > 0 || Object.getOwnPropertyNames(modifiedFiles).length > 0) {
                    return success(true);
                }

                return success(false);
            });
        },

        //#region Document functions
        transfer: function (action) {
            /// <summary>
            /// Bàn giao văn bản theo hướng chuyển.
            /// </summary>
            /// <param name="action" type="object">Hướng chuyển.</param>
            var that = this,
                comment,
                transferAction;

            this.$Comment = this.$Comment || $();
            comment = this.$Comment.val();
            transferAction = action;

            egov.pubsub.publish("status.processing", "Đang lưu báo cáo");
            //this.DocEditor.destroyEditor();
            setTimeout(function () {
                require(['transfer'], function (Transfer) {
                    if (egov.transferForm === undefined) {
                        egov.transferForm = new Transfer;
                    }
                    egov.transferForm.render({
                        action: transferAction,
                        document: that,
                        callback: function () {
                            //add comment vao cache
                            that._addCommentCommon(comment);
                        }
                    });
                });
            }, 100);
        },

        transferSpecialCreate: function (action) {
            /// <summary>
            /// Bàn giao hướng chuyển đặc biệt khi tiếp nhận: tiếp nhận hồ sơ, tiếp nhận và tiếp tục
            /// </summary>
            /// <param name="action">Hướng chuyển</param>
            var that = this;
            var comment = this.$Comment.val();
            // Không cần hỏi chấp nhận văn bản liên quan vì hướng chuyển này là chuyển trực tiếp cho mình.

            // Lưu các file đang được mở để chỉnh sửa
            that.attachments.confirmAttachments(function () {
                // Mở form bàn giao
                require(['transfer'], function (Transfer) {
                    if (egov.transferForm === undefined) {
                        egov.transferForm = new Transfer;
                    }
                    egov.transferForm.render({
                        action: action,
                        document: that,
                        callback: function () {
                            //add comment vao cache
                            that._addCommentCommon(comment);
                        }
                    });
                });
            });
        },

        cacheCodeNotation: function () {
            var organ = this.model.get("Organization");
            var doccode = this.model.get("DocCode");
            if (doccode == '') return;

            var codeNotation = _.last(doccode.split('/'));
            var notations = egov.setting.codeNotations[organ] || [];
            notations.push(codeNotation);
        },

        sendAnswer: function (actionKey) {
            /// <summary>
            /// Chuyển ý kiến đóng góp: cho văn bản xin ý kiến và văn bản đồng xử lý
            /// </summary>
            /// <param name="actionKey">Tên hướng chuyển đặc biệt</param>

            var that = this,
                comment = this.$Comment.val(),
                doc,
                selectedFiles,
                removeFiles,
                documentCopyId;

            var saveStoreFunction = function (storeId) {
                if (storeId) {
                    egov.pubsub.publish(egov.events.status.processing, _resource.docStore.processing);

                    egov.request.SaveDocumentToStorePrivate({
                        data: {
                            storePrivateId: storeId,
                            documentCopyId: that.model.get('DocumentCopyId')
                        },
                        success: function (result) {
                            egov.pubsub.publish(egov.events.status.success, _resource.docStore.success);

                            that.storePrivate.$el.dialog('hide');
                        },
                        error: function (error) {
                            egov.pubsub.publish(egov.events.status.error, _resource.docStore.error);
                        }
                    });
                }
            };

            // Xác nhận lại có cho phép hướng chuyển tiếp theo xem được các văn bản liên quan đã có hay không.
            that.relations.confirmRelations(function () {
                // Lưu các file đang được mở để chỉnh sửa
                that.attachments.confirmAttachments(function () {
                    doc = that.serialize();
                    if (doc == undefined) {
                        return;
                    }

                    selectedFiles = {};
                    that.attachments.model.each(function (file) {
                        if (file.get('isNew')) {
                            selectedFiles[file.get('Id')] = { name: file.get('Name') }
                        }
                    });

                    removeFiles = that.attachments.model.select(function (file) {
                        return file.get('isRemoved');
                    });
                    documentCopyId = that.model.get("DocumentCopyId");
                    documentCopyId = parseInt(documentCopyId);
                    if (isNaN(documentCopyId)) {
                        return;
                    }

                    // egov.message.processing(egov.resources.common.transfering, false);
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                    egov.request.TransferYKienDongGop({
                        data: {
                            "doc": JSON.stringify(doc),
                            "files": JSON.stringify(selectedFiles),
                            "modifiedFiles": JSON.stringify(that.attachments.modifiedFiles),
                            "removeAttachmentIds": removeFiles,
                            "documentCopyId": documentCopyId,
                            "actionSpecial": actionKey
                        },
                        success: function (result) {
                            if (result.success) {
                                // egov.message.success(_resource.transfer.answerSuccess);
                                egov.pubsub.publish(egov.events.status.success, _resource.transfer.answerSuccess);
                                egov.views.home.tree.storeTree.renderDialog(function (storeId) {
                                    if (storeId) {
                                        saveStoreFunction(storeId);
                                    }
                                    that._reloadDocumentTree();
                                    that.tab.close();
                                });

                                that._addCommentCommon(comment);
                            } else {
                                egov.pubsub.publish(egov.events.status.error, result.error);
                            }
                        }
                    });
                });
            });
        },

        publishment: function () {
            /// <summary>
            /// Lưu sổ phát hành văn bản
            /// </summary>
            var that = this;
            // Lưu các file đang được mở để chỉnh sửa
            that.attachments.confirmAttachments(function () {
                // Mở form phát hành
                require(['publishmentView'], function (publishView) {
                    if (egov.publishment === undefined) {
                        egov.publishment = new publishView;
                    }

                    egov.publishment.render({
                        document: that,
                        docTypeId: that.newDoctype
                    });
                });
            }, true);
        },
        publishmentGov: function () {
            /// <summary>
            /// Lưu sổ phát hành văn bản
            /// </summary>
            var that = this;
            // Lưu các file đang được mở để chỉnh sửa
            that.attachments.confirmAttachments(function () {
                // Mở form phát hành
                require(['publishmentGovView'], function (publishView) {
                    if (egov.publishmentGov === undefined) {
                        egov.publishmentGov = new publishView;
                    }

                    egov.publishmentGov.render({
                        document: that,
                        docTypeId: that.newDoctype
                    });
                });
            }, true);
        },
        transferSurvey: function (action) {
            /// <summary>
            /// Bàn giao văn bản theo hướng chuyển.
            /// </summary>
            /// <param name="action" type="object">Hướng chuyển.</param>
            var that = this,
                comment,
                transferAction;

            this.$Comment = this.$Comment || $();
            comment = this.$Comment.val();
            transferAction = action;

            egov.pubsub.publish("status.processing", "Đang lưu báo cáo");
            //this.DocEditor.destroyEditor();
            setTimeout(function () {
                require(['surveyView'], function (surveyView) {
                    if (egov.surveyForm === undefined) {
                        egov.surveyForm = new surveyView;
                    }
                    egov.surveyForm.render({
                        action: transferAction,
                        document: that,
                        callback: function () {
                            //add comment vao cache
                            that._addCommentCommon(comment);
                        }
                    });
                });
            }, 100);
        },
        publishmentSurvey: function () {
            /// <summary>
            /// phát hành khảo sát
            /// </summary>
            var that = this;
            // Lưu các file đang được mở để chỉnh sửa
            that.attachments.confirmAttachments(function () {
                // Mở form phát hành
                require(['surveyView'], function (surveyView) {
                    if (egov.surveyView === undefined) {
                        egov.surveyView = new surveyView;
                    }

                    egov.surveyView.render({
                        document: that,
                        docTypeId: that.newDoctype
                    });
                });
            }, true);
        },
        rePublish: function (docId) {
            var that = this;
            egov.request.GetIsTransferPublish({
                data: { documentCopyId: that.model.get("DocumentCopyId") },
                success: function (data) {
                    if (data.error) {
                        return;
                    }
                    var isTransferPublish = data.data;
                    if (isTransferPublish) {
                        require(['publishmentView'], function (publishView) {
                            if (egov.publishment === undefined) {
                                egov.publishment = new publishView;
                            }

                            egov.publishment.render({
                                document: that,
                                isRePublish: true
                            });
                        });
                        return;
                    }

                    require(['privatePublishmentView'], function (privatePublishView) {
                        if (egov.privatePublishment === undefined) {
                            egov.privatePublishment = new privatePublishView;
                        }

                        egov.privatePublishment.render({
                            document: that,
                            isRePublish: true
                        });
                    });
                }
            })

        },

        privatePublishment: function () {
            /// <summary>
            /// Lưu sổ phát hành nội bộ
            /// </summary>

            var that = this;
            // Lưu các file đang được mở để chỉnh sửa
            that.attachments.confirmAttachments(function () {
                // Mở form phát hành
                require(['privatePublishmentView'], function (privatePublishView) {
                    if (egov.privatePublishment === undefined) {
                        egov.privatePublishment = new privatePublishView;
                    }

                    egov.privatePublishment.render({
                        document: that,
                        docTypeId: that.newDoctype
                    });
                });
            });
        },

        sendComment: function (title) {
            /// <summary>
            /// Gửi ý kiến
            /// </summary>

            if (title === undefined) {
                title = _resource.sendComment.dialogTitle;
            }
            var that = this,
                newComment;

            egov.message.prompt(title, _resource.sendComment.dialogButton, function (comment) {
                if (comment === '') {
                    // Chưa nhập ý kiến bắt nhập lại
                    that.sendComment(_resource.sendComment.enterComment);
                    return;
                }

                // egov.message.processing(egov.resources.common.transfering, false);
                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                egov.request.sendComment({
                    data: {
                        documentCopyId: that.model.get('DocumentCopyId'),
                        comment: comment
                    },
                    success: function (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.error);
                        } else {
                            egov.pubsub.publish(egov.events.status.success, _resource.sendComment.sendSuccess);
                            egov.dataManager.getAllUsers({
                                success: function (allUsers) {
                                    require([egov.template.document.comment], function (CommentTemplate) {
                                        newComment = parseComment(result, allUsers);

                                        // Thêm ý kiến xử lý mới thêm vào danh sách ý kiến
                                        that.$('#commentList').prepend($.tmpl(CommentTemplate, newComment.Processor));
                                    });
                                }
                            });
                        }
                    },
                    error: function () {
                        egov.pubsub.publish(egov.events.status.error, _resource.sendComment.sendFail);
                    }
                });
            });
        },

        announcement: function () {
            /// <summary>
            /// Thông báo
            /// </summary>

            var that = this;
            require(['announcementView'], function (announcementView) {
                if (egov.announcementForm === undefined) {
                    egov.announcementForm = new announcementView;
                }
                egov.announcementForm.render(that.model.get('DocumentCopyId'));
            });
        },

        sendQuestion: function () {
            /// <summary>
            /// Xin ý kiến
            /// </summary>
            var that = this;
            // Lưu các file đang được mở để chỉnh sửa
            that.attachments.confirmAttachments(function () {
                // Mở form phát hành
                require(['consultView'], function (consultView) {
                    if (egov.consultView === undefined) {
                        egov.consultView = new consultView();
                    }
                    egov.consultView.render({ document: that });
                });
            });
        },

        answer: function (doctypeId, relationType) {
            /// <summary>
            /// Trả lời văn bản
            /// </summary>
            /// <param name="doctypeId">Loại hồ sơ, văn bản trả lời</param>
            var relationId = this.model.get('DocumentCopyId');
            relationType = relationType || 2;
            egov.dataManager.getCurrentDoctypes({
                success: function (doctypes) {
                    var doctype = _.find(doctypes, function (dt) {
                        return dt.DocTypeId === doctypeId;
                    });

                    egov.views.home.tab.addDocument(doctypeId, doctype.DocTypeName, relationId, null, null, null, null, null, relationType);
                }
            });
        },

        answerHSMC: function (doctypeId, attachment) {
            /// <summary>
            /// Trả lời văn bản hồ sơ một cửa(dung trong trường hợp thực hiện gửi văn bản kí tài liệu qua bên khác rồi gửi trả lại hồ sơ môt của)
            /// </summary>
            /// <param name="doctypeId">Loại hồ sơ, văn bản trả lời</param>
            var relationId = this.model.get('DocumentCopyId');
            egov.dataManager.getCurrentDoctypes({
                success: function (doctypes) {
                    var doctype = _.find(doctypes, function (dt) {
                        return dt.DocTypeId === doctypeId;
                    });
                    egov.views.home.tab.addDocument(doctypeId, doctype.DocTypeName, relationId, false, attachment);
                }
            });
        },

        _businessLicense: function () {
            if (!this.isHsmc) {
                return;
            }
            var that = this;
            require(['BusinessLicenseView'], function (returnView) {
                new returnView({
                    document: that,
                    callback: function (result) {
                    }
                });
            });
        },

        _importInvoice: function () {
            if (!this.isHsmc) {
                return;
            }
            var that = this;
            require(['InvoiceView'], function (returnView) {
                new returnView({
                    document: that,
                    callback: function (result) {
                    }
                });
            });
        },
        _exportExcel: function () {
            var that = this;
            var configHandsontable = that.configHandsontable;
            var parseConfigHandsontable = JSON.parse(configHandsontable);
            var headerNested = JSON.stringify(parseConfigHandsontable.headerNested);
            var arrHeaderSetting = parseConfigHandsontable.extra.headerSetting;
            var headerSetting = JSON.stringify(parseConfigHandsontable.extra.headerSetting);
            var hiddenArr = parseConfigHandsontable.extra.hiddenColumns;
            var hiddenColumns = JSON.stringify(hiddenArr);

            // 5 nam
            var actionLevel = that.model.get("ActionLevel");
            var categoryBusinessId = that.model.get("CategoryBusinessId");
            var year = that.$el.find("#ddlYearReport option:selected").val();
            if (actionLevel == 1 && (categoryBusinessId == 4 || categoryBusinessId == 64)) {
                var countYear = 5;
                var dem = 0;
                var index = 0;
                arrHeaderSetting[0].forEach(function (key) {
                    if (key != null) {
                        if (key.charAt(0) == '$') {
                            var yearPlus = "Năm " + (year - countYear + dem);
                            arrHeaderSetting[0][index] = yearPlus;
                            dem++;
                        }
                    }
                    index++;
                })
                headerSetting = JSON.stringify(arrHeaderSetting);
            }
            //end 5 nam
            var arrnew = [];
            _.map(parseConfigHandsontable.headerNested, function (num, index) {
                if (!_.contains(parseConfigHandsontable.extra.hiddenColumns, num.col)) {
                    arrnew.push(num);
                }
            });
            var dataString = null;
            var rows = configHandsontable == undefined ? rows = that.nestedHeaders.slice() : arrHeaderSetting
            //var rows = that.nestedHeaders.slice();
            //var rows = arrHeaderSetting;
            // ajax để export ra
            that.dataFormTable.getData().forEach(function (infoArray, index) {
                rows.push(infoArray);
            });

            var arr = [];
            for (var i = 0; i < rows.length; i++) {
                arr.push(Object.assign({}, rows[i]));
            }
            var arr22 = [];
            arr.forEach(function (item, index) {
                arr22.push(item);
            });

            var hiddenCols = that.dataFormTable.getPlugin("hiddenColumns")
            var countHiddenCol = hiddenCols.hiddenColumns
            var arrContent = [];
            arr.forEach(function (item, index) {
                for (var i = 0; i < countHiddenCol.length; i++) {
                    var indexDelete = countHiddenCol[i];
                    delete item[indexDelete];
                }
                arrContent.push(item);
            });

            var arr0 = arrContent[0];
            var arrIndex = Object.keys(arr0);
            var ct = arrIndex[0];
            var posTextBold = 2;
            Object.keys(arr0).forEach(function (key) {
                if (arr0[key] == "STT" || arr0[key] == "TT" || arr0[key] == "Đơn vị hành chính") {
                    posTextBold = key;
                }
            });

            var screenWidth = screen.width;
            var right1, left2, right2, left3, right3, right4;
            screenWidth == 1920 ? right1 = 1000 : right1 = 600;
            screenWidth == 1920 ? right2 = 1500 : right2 = 900;
            screenWidth == 1920 ? right3 = 1920 : right3 = 1300;
            screenWidth == 1920 ? right4 = 1600 : right4 = 1000;
            screenWidth == 1920 ? left2 = 500 : left2 = 300;
            screenWidth == 1920 ? left3 = 700 : left3 = 500;

            var arrLeftHeader = [];
            var arrRightHeader = [];
            var arrMiddleHeader = [];
            var arrCenterHeader = [];

            var lstHeader1 = that.$el.find("#CreateForm .scrollPart .formHeader p");
            var lstHeader2 = that.$el.find("#EditForm .scrollPart .formHeader p");
            var lstHeader = lstHeader1.length > 0 ? lstHeader1 : lstHeader2;
            for (var i = 0; i < lstHeader.length; i++) {
                var tagHTML = lstHeader[i].getBoundingClientRect();
                var txtHTML = lstHeader[i].textContent;
                if (txtHTML != " ") {
                    if (txtHTML != "") {
                        var objLeft = {};
                        var objRight = {};
                        var objMiddle = {};
                        var objCenter = {};
                        var _leftTag = tagHTML.left;
                        var _rightTag = tagHTML.right;
                        var _topTag = tagHTML.top;
                        var _nameTag = txtHTML;
                        var tb = (_leftTag + _rightTag) / 2;

                        if (_leftTag >= 0 && _rightTag < right1) {
                            objLeft.leftTag = _leftTag;
                            objLeft.rightTag = _rightTag;
                            objLeft.topTag = _topTag;
                            objLeft.nameTag = _nameTag;
                            arrLeftHeader.push(objLeft);
                        } else if (_leftTag > left2 && _rightTag < right2) {
                            objMiddle.leftTag = _leftTag;
                            objMiddle.rightTag = _rightTag;
                            objMiddle.topTag = _topTag;
                            objMiddle.nameTag = _nameTag;
                            arrMiddleHeader.push(objMiddle);
                        } else if (_leftTag > left3 && _rightTag < right3) {
                            objRight.leftTag = _leftTag;
                            objRight.rightTag = _rightTag;
                            objRight.topTag = _topTag;
                            objRight.nameTag = _nameTag;
                            arrRightHeader.push(objRight);
                        } else if (_leftTag > 0 && _rightTag > right4) {
                            objCenter.leftTag = _leftTag;
                            objCenter.rightTag = _rightTag;
                            objCenter.topTag = _topTag;
                            objCenter.nameTag = _nameTag;
                            arrCenterHeader.push(objCenter);
                        }
                    }
                }
            }

            var arrLeftFooter = [];
            var arrRightFooter = [];
            var arrMiddleFooter = [];
            var arrCenterFooter = [];
            var lstFooter1 = that.$el.find("#CreateForm .scrollPart .formFooter p");
            var lstFooter2 = that.$el.find("#EditForm  .scrollPart .formFooter p");
            var lstFooter = lstFooter1.length > 0 ? lstFooter1 : lstFooter2;

            for (var i = 0; i < lstFooter.length; i++) {
                var tagHTML = lstFooter[i].getBoundingClientRect();
                var txtHTML = lstFooter[i].textContent;
                if (txtHTML != " ") {
                    if (txtHTML != "") {
                        var objLeft = {};
                        var objRight = {};
                        var objMiddle = {};
                        var objCenter = {};
                        var _leftTag = tagHTML.left;
                        var _rightTag = tagHTML.right;
                        var _topTag = tagHTML.top;
                        var _nameTag = txtHTML;
                        var tb = (_leftTag + _rightTag) / 2;

                        if (_leftTag >= 0 && _rightTag < right1) {
                            objLeft.leftTag = _leftTag;
                            objLeft.rightTag = _rightTag;
                            objLeft.topTag = _topTag;
                            objLeft.nameTag = _nameTag;
                            arrLeftFooter.push(objLeft);
                        } else if (_leftTag > left2 && _rightTag < right2) {
                            objMiddle.leftTag = _leftTag;
                            objMiddle.rightTag = _rightTag;
                            objMiddle.topTag = _topTag;
                            objMiddle.nameTag = _nameTag;
                            arrMiddleFooter.push(objMiddle);
                        } else if (_leftTag > left3 && _rightTag < right3) {
                            objRight.leftTag = _leftTag;
                            objRight.rightTag = _rightTag;
                            objRight.topTag = _topTag;
                            objRight.nameTag = _nameTag;
                            arrRightFooter.push(objRight);
                        } else if (_leftTag > 0 && _rightTag > right4) {
                            objCenter.leftTag = _leftTag;
                            objCenter.rightTag = _rightTag;
                            objCenter.topTag = _topTag;
                            objCenter.nameTag = _nameTag;
                            arrCenterFooter.push(objCenter);
                        }
                    }
                }
            }

            var fileName = that.$("textarea[name=Compendium]").text();
            $.ajax({
                type: "POST",
                url: '/HomeSMReport/ExportFileExcel',
                data: {
                    stringRows: JSON.stringify(arrContent),
                    stringFileName: fileName,
                    leftHeader: JSON.stringify(arrLeftHeader),
                    rightHeader: JSON.stringify(arrRightHeader),
                    middleHeader: JSON.stringify(arrMiddleHeader),
                    centerHeader: JSON.stringify(arrCenterHeader),
                    leftFooter: JSON.stringify(arrLeftFooter),
                    rightFooter: JSON.stringify(arrRightFooter),
                    middleFooter: JSON.stringify(arrMiddleFooter),
                    centerFooter: JSON.stringify(arrCenterFooter),
                    ct: JSON.stringify(arrIndex),
                    posTextBold: posTextBold,
                    headerNested: JSON.stringify(arrnew),
                    headerSetting: headerSetting,
                    hiddenColumns: hiddenColumns
                },
                success: function (result) {
                    $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                    // window.location = '/HomeSMReport/DownloadExcel';
                }
            });
            /*
            var fileName = $("textarea[name=Compendium]").text();
            if (fileName == '')
                fileName = "Báo cáo_[YYYY]-[MM]-[DD].csv";
            else
                fileName += ".csv";

            var asUtf16, downloadExcelCsv, makeExcelCsvBlob, rows, toTsv;

            asUtf16 = function (str) {
                var buffer, bufferView, i, val, _i, _ref;
                buffer = new ArrayBuffer(str.length * 2);
                bufferView = new Uint16Array(buffer);
                bufferView[0] = 0xfeff;
                for (i = _i = 0, _ref = str.length; 0 <= _ref ? _i <= _ref : _i >= _ref; i = 0 <= _ref ? ++_i : --_i) {
                    val = str.charCodeAt(i);
                    bufferView[i + 1] = val;
                }
                return bufferView;
            };

            makeExcelCsvBlob = function (rows) {
                return new Blob([asUtf16(toTsv(rows)).buffer], {
                    type: "text/csv;charset=UTF-16"
                });
            };

            toTsv = function (rows) {
                var escapeValue;
                escapeValue = function (val) {
                    if (typeof val === 'string') {
                        return '"' + val.replace(/"/g, '""') + '"';
                    } else if (val != null) {
                        return val;
                    } else {
                        return '';
                    }
                };
                return rows.map(function (row) {
                    return row.map(escapeValue).join('\t');
                }).join('\n') + '\n';
            };
            */
            //downloadExcelCsv = function (rows, attachmentFilename) {
            //    var a, blob;
            //    blob = makeExcelCsvBlob(rows);
            //    a = document.createElement('a');
            //    a.style.display = 'none';
            //    a.download = attachmentFilename;
            //    document.body.appendChild(a);
            //    a.href = URL.createObjectURL(blob);
            //    a.click();
            //    URL.revokeObjectURL(a.href);
            //    a.remove();
            //};

            //downloadExcelCsv(rows, fileName);
        },
        _exportPdf: function () {
            var that = this;
            var content = CKEDITOR.instances[that.cid + '_explicit_template'].getData();
            if (content.trim().length > 0) {
                var fileName = $(`${that.el} textarea[name=Compendium]`).text();
                if (fileName == '')
                    fileName = "Báo cáo";
                var settings = {
                    content: content,
                    name: fileName
                }
                $.ajax({
                    type: "post",
                    url: "/ReportViewer/ExportPdfHtml",
                    data: settings,
                    success: function (result) {
                        $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                    }
                });
            }
        },
        _exportDoc: function () {
            var that = this;

            var content = CKEDITOR.instances[that.cid + '_explicit_template'].getData();
            if (content.trim().length > 0) {
                var fileName = $(`${that.el} textarea[name=Compendium]`).text();
                if (fileName == '')
                    fileName = "Báo cáo";
                var settings = {
                    content: content,
                    name: fileName
                }
                $.ajax({
                    type: "post",
                    url: "/ReportViewer/ExportDocxHtml",
                    data: settings,
                    success: function (result) {
                        $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                    }
                });
            }
        },

        _exportDocToSign: function (callback) {
            var that = this;
            var content = ""
            require(["exportdata"], function (exprt) {
                if (that.model.get("CategoryBusinessId") == 4 || that.model.get("CategoryBusinessId") == 64) {
                    var exportData = new exprt({ document: that });
                    content = exportData.getFormSolieu();
                }
                if (that.model.get("CategoryBusinessId") == 8) {
                    content = CKEDITOR.instances[that.cid + '_explicit_template'].getData();
                }

                if (content.trim().length > 0) {
                    var fileName = $(`${that.el} textarea[name=Compendium]`).text();
                    if (fileName == '')
                        fileName = "Báo cáo";
                    var settings = {
                        content: content,
                        name: fileName
                    }
                    $.ajax({
                        type: "post",
                        url: "/ReportViewer/ExportDocxHtmlSign",
                        data: settings,
                        success: function (result) {
                            callback({ fileName: result })
                        }
                    });
                }
            });



        },

        _showCodeTemp: function () {
            var template = egov.template.document.codetemp;
            var dialogElement = $("<div>");
            var that = this;
            $.ajax({
                type: "GET",
                url: "/document/GetCodeTemp",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //Thiết lập dialog
                    var settings = {
                        width: 780,
                        height: 400,
                        title: "Thông tin các mã đang lưu tạm",
                        position: ['center', 100],
                        buttons: [
                            {
                                text: "Xóa tất cả",
                                className: 'btn-danger',
                                click: function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/document/DeleteAllCodeTemp",
                                        dataType: "json",
                                        contentType: 'application/json; charset=utf-8',
                                        success: function (model) {
                                            if (model.success) {
                                                that._reloadCodeTemp(template, dialogElement);
                                            }
                                            else {

                                            }
                                        },
                                        error: function () {
                                        }
                                    });
                                }
                            },
                            {
                                text: "Đóng",
                                className: 'btn-default',
                                click: function () {
                                    dialogElement.dialog('destroy');
                                    dialogElement.remove();
                                }
                            },
                        ],
                        close: function () {
                            dialogElement.remove();
                        },

                        draggable: true,
                        keyboard: true,
                    };

                    require([template], function (CodeTempTemplate) {
                        var temp = { templateCode: result }
                        dialogElement.html($.tmpl(CodeTempTemplate, temp));
                        that._deleteCodeTemp(template, dialogElement);
                    });

                    dialogElement.dialog(settings);

                },
                error: function () {
                    console.log("No data return");
                }
            });
        },

        _deleteCodeTemp: function (template, dialogElement) {
            var that = this;
            $(".btn-DeleteCode").click(function (e) {
                var element = $(e.target).closest("a");
                var id = element.attr("id");
                $.ajax({
                    type: "POST",
                    url: "/document/DeleteCodeTemp",
                    data: { 'codeTempId': parseInt(id) },
                    success: function (model) {
                        if (model.success) {
                            that._reloadCodeTemp(template, dialogElement);
                        }
                        else {

                        }
                    },
                    error: function () {
                    }
                });
            });
        },

        _reloadCodeTemp: function (template, dialogElement) {
            var that = this;
            $.ajax({
                type: "GET",
                url: "/document/GetCodeTemp",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    require([template], function (CodeTempTemplate) {
                        var temp = { templateCode: result }
                        dialogElement.html($.tmpl(CodeTempTemplate, temp));
                        that._deleteCodeTemp(template, dialogElement);
                    });
                }
            });
        },

        finish: function () {
            /// <summary>
            /// Kết thúc xử lý
            /// </summary>
            var that = this,
                comment = this.$("#Comment").val();

            var documentCopyId = that.model.get("DocumentCopyId");
            var docInTree = egov.views.home.documents.model.detect(function (d) {
                return d.get('DocumentCopyId') === documentCopyId;
            });

            var isThongBao = docInTree != undefined ? docInTree.get('isThongBao') == 1 : false;
            this.model.set("isThongBao", isThongBao);

            egov.views.home.documents.finishs([this.model], comment, function () {
                that._reloadDocumentTree();
                that.tab.close();
            }, this);
        },

        finishNotification: function (callback) {
            /// <summary>
            /// kết thúc hồ sơ khi là văn bản thông báo(bỏ user hiện tại khỏi thông báo)
            /// </summary>
            /// <param name="callback"></param>
            /// <returns type=""></returns>
            var that = this;
            var removeNotice = function () {
                egov.request.removeDocumentAnnouncement({
                    data: {
                        documentCopyId: that.model.get('DocumentCopyId')
                    },
                    success: function (result) {

                    },
                    error: function (error) {
                    }
                });
            };

            var saveStoreFunction = function (storeId) {
                if (storeId) {
                    egov.pubsub.publish(egov.events.status.processing, _resource.docStore.processing);

                    egov.request.SaveDocumentToStorePrivate({
                        data: {
                            storePrivateId: storeId,
                            documentCopyId: that.model.get('DocumentCopyId')
                        },
                        success: function (result) {
                            removeNotice();
                            egov.pubsub.publish(egov.events.status.success, _resource.docStore.success);

                            that.storePrivate.$el.dialog('hide');
                        },
                        error: function (error) {
                            egov.pubsub.publish(egov.events.status.error, _resource.docStore.error);
                        }
                    });
                }
            };


            egov.views.home.tree.storeTree.renderDialog(function (storeId) {
                if (storeId) {
                    saveStoreFunction(storeId);
                } else {
                    removeNotice();
                }

                that._reloadDocumentTree();
                that.tab.close();
            });
        },

        saveStore: function (callback) {
            /// <summary>
            /// Lưu sổ
            /// </summary>
            /// <param name="callback"></param>
            /// <returns type=""></returns>
            var that = this;
            var saveStoreFunction = function (storeId) {
                if (storeId) {
                    egov.pubsub.publish(egov.events.status.processing, _resource.docStore.processing);

                    egov.request.SaveDocumentToStorePrivate({
                        data: {
                            storePrivateId: storeId,
                            documentCopyId: that.model.get('DocumentCopyId')
                        },
                        success: function (result) {
                            egov.pubsub.publish(egov.events.status.success, _resource.docStore.success);
                            that.storePrivate.$el.dialog('hide');
                        },
                        error: function (error) {
                            egov.pubsub.publish(egov.events.status.error, _resource.docStore.error);
                        }
                    });
                }
            };

            egov.views.home.tree.storeTree.renderDialog(function (storeId) {
                saveStoreFunction(storeId);
            });
        },

        printReceiptShortKey: function (displayPreview, printId) {
            var that = this,
                id,
                status,
                _print;

            id = that.model.get("DocumentCopyId");
            status = that.model.get("Status");

            if (!that.isHsmc || (status !== 0 && status !== 1)) {
                egov.pubsub.publish(egov.events.status.error, _resource.print.isNotCreated);
                return;
            }

            egov.pubsub.publish(egov.events.status.processing, _resource.print.processing);

            //Hàm in
            _print = function () {
                require(["print"], function (printView) {
                    new printView({
                        docCopyId: [id],
                        displayPreview: displayPreview,
                        printId: printId
                    });
                });
            }

            //Nếu đã lưu dự thảo
            if (id != 0) {
                _print();
                return;
            }

            //Nếu chưa lưu dự thảo
            that.saveDocument(function (result) {
                if (result.success) {
                    id = result.documentCopyId;
                    that.model.set("DocumentCopyId", id);
                    _print();
                } else {
                    egov.pubsub.publish(egov.events.status.error, _resource.print.error);
                }
            }, true);

        },

        receiveSupplementary: function (supplementaryId, suppItem, message, success) {
            /// <summary>
            ///
            /// </summary>
            /// <param name="message"></param>
            /// <param name="success"></param>
            /// <returns type=""></returns>
            if (!this.isHsmc) {
                return;
            }

            var that = this;

            supplementaryId |= 0;
            if (supplementaryId === 0) {
                var suppModel = that.model.get("Supplementary");
                if (suppModel != undefined) {
                    var unsuccess = suppModel.detect(function (supp) {
                        return !supp.get("IsSuccess");
                    });
                    if (unsuccess != undefined) {
                        supplementaryId = unsuccess.get("SupplementaryId");
                        suppItem = unsuccess.view;
                    }
                }
            }

            if (that.supplementaryView) {
                that.supplementaryView.open(suppItem, success, message);
                return;
            }

            require(['supplementary'], function (supplementaryView) {
                that.supplementaryView = new supplementaryView({
                    document: that,
                    message: message,
                    suppItem: suppItem,
                    callback: success
                });
            });
        },

        insertReceiveSupplementary: function (supp, papers, fees) {
            /// <summary>
            /// Chèn yêu cầu bổ sung mới vào danh sách
            /// </summary>
            /// <param name="supp"></param>
            /// <returns type=""></returns>
            // this.supplementaryView.model.add(supp);
            this.model.get("Supplementary").add(supp);
        },

        removeCurrentRequiredSupplementary: function (suppItem) {
            /// <summary>
            /// Xóa bỏ yêu cầu bổ sung hiện tại
            /// </summary>
            /// <returns type=""></returns>
            this.model.get("Supplementary").remove(suppItem.model);
        },

        renewals: function () {
            /// <summary>
            /// Gia hạn
            /// </summary>
            /// <returns type=""></returns>
            var that = this;
            require(['renewals'], function (renewalsView) {
                new renewalsView({
                    document: that
                });
            });
        },

        _acceptThuHoi: function (e) {
            var isAccept = $(e.target).closest(".btnAcceptThuHoi").is(".accept");
            var that = this;

            this.$("#Comment").next(".error").remove();
            var comment = this.$("#Comment").val();

            if (!isAccept && comment === '') {
                this.$("#Comment").after('<label class="error">Bạn cần nhập lý do từ chối thu hồi.</span>');
                return;
            }

            var documentCopyId = this.model.get("DocumentCopyId");
            egov.request.acceptThuHoi({
                data: { documentCopyId: documentCopyId, isAccept: isAccept, comment: comment },
                success: function (response) {
                    if (response.success) {
                        egov.pubsub.publish("status.success", "Đã thu hồi văn bản thành công.");
                        that.closeTabAndReloadTreeNode();
                        return;
                    }

                    egov.pubsub.publish("status.error", "Lỗi: không thu hồi được văn bản.");
                }
            });
        },

        changeDateAppointed: function (newDate) {
            /// <summary>
            /// Thay đổi hạn xử lý khi gia hạn
            /// </summary>
            /// <param name="newDate"></param>
            this.model.set("DateAppointed", newDate);

            var date = new Date(newDate);
            this.$("[name='DateAppointed']").datepicker("setDate", date);
        },

        returnResult: function () {
            /// <summary>
            /// Trả kết quả
            /// </summary>

            if (!this.isHsmc) {
                return;
            }

            var that = this,
                hasSupplemntary,
                isSuccess,
                success,
                docCopyId;

            hasSupplemntary = (this.model.get("IsSupplemented") == false) && this.model.get("Status") == egov.enum.documentStatus.DungXuLy;
            var doctypePermission = this.model.get("DocTypePermission");
            var isNeedSuccess = ((doctypePermission & 0x00000002) === 0x00000002);
            if (isNeedSuccess) {
                isSuccess = this.model.get("IsSuccess") != null;
            } else {
                isSuccess = true
            }
            success = function () {
                require(['returnResult'], function (returnView) {
                    new returnView({
                        document: that,
                        callback: function (result) {
                            docCopyId = parseInt(that.model.get("DocumentCopyId"));
                            if (result.isFinish) {

                                that._reloadDocumentTree();

                                that.tab.close();
                                return;
                            }
                            that.returnPapers = result.papers;
                            that.returnFees = result.fees;
                            that._bindSuppPaperAndFee(that.returnPapers, that.returnFees, true);
                        }
                    });
                });
                return;
            }

            if (hasSupplemntary) {
                this.receiveSupplementary(0, null, _resource.returnResult.needToUpdateSupplementary,
                    function () {
                        if (!isSuccess) {
                            that.updateLastResult(_resource.returnResult.needToUpdateLastResult, success, true);
                            return;
                        }
                        success();
                    });
                return;
            }

            if (!isSuccess) {
                that.updateLastResult(_resource.returnResult.needToUpdateLastResult, success, true);
                return;
            }

            success();
        },

        publicResult: function () {
            /// <summary>
            /// Công bố kết quả KNTC
            /// </summary>

            if (!this.isKNTC) {
                return;
            }
            var that = this;
            require(['publicResult'], function (publicResultView) {
                new publicResultView({
                    document: that
                });
            });
        },

        createNewAppoint: function () {
            /// <summary>
            /// Đặt lịch hẹn tiếp công dân
            /// </summary>
            var that = this;
            require(['appoint'], function (Appoint) {
                new Appoint({
                    document: that
                });
            });
        },

        updateLastResult: function (message, success, isSign) {
            /// <summary>
            /// Cập nhật kết quả xử lý cuối
            /// </summary>
            var that = this;
            var isNotNeedSign = isSign ? false : true;
            require(['updateLastResult'], function (updateLastResultView) {
                new updateLastResultView({
                    document: that,
                    message: message,
                    callback: success,
                    IsNotNeedSign: isNotNeedSign
                });
            });
        },

        saveDocument: function (success, isSecretSave) {
            /// <summary>
            /// Lưu văn bản
            /// </summary>
            var that,
                doc,
                selectedFiles,
                removeFiles,
                modifiedFiles,
                query,
                callback,
                data;

            that = this;

            that.attachments.confirmAttachments(function () {
                doc = that.serialize();
                selectedFiles = {};
                query = that.isCreate ? egov.request.saveDocDraft : egov.request.saveDoc;

                that.attachments.model.each(function (file) {
                    if (file.get('isNew')) {
                        selectedFiles[file.get('Id')] = { name: file.get('Name') }
                    }
                });

                callback = function (result) {

                    if (window.top._tempKey) {
                        $.ajax({
                            url: "/homesmreport/updateconfigkey",
                            data: {
                                documentcopyid: result.documentCopyId,
                                keyconfig: JSON.stringify(window.top._tempKey)
                            },
                            method: 'post',
                            success: function (res) {
                                //if (res) {
                                //    alert("cấu hình key thành công")
                                //} else {
                                //    alert("cấu hình key không thành công")
                                //}
                            }
                        })
                        window.top._tempKey = null;
                    }

                    if (result.error && !isSecretSave) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                    }
                    else if (result.success && !isSecretSave) {
                        egov.pubsub.publish(egov.events.status.success, result.success);
                    }

                    if (that.isCreate) {
                        //that.$el.find("input[name=DocumentCopyId]").val(result.documentCopyId);
                        //that.model.set("DocumentCopyId", result.documentCopyId)
                        //that.model.set("TransferType", 2)
                        //that.model.set("TransferTypeInEnum", "CapNhat")
                        that.tab.close();
                    }

                    that.isCreate = false;
                    egov.callback(success, result);
                };

                data = {
                    doc: JSON.stringify(doc),
                    files: JSON.stringify(selectedFiles)
                };

                if (!that.isCreate) {
                    removeFiles = that.attachments.model.select(function (file) {
                        return file.get('isRemoved');
                    });
                    removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

                    modifiedFiles = that.attachments.modifiedFiles;
                    $.each(selectedFiles, function (keyname, value) {
                        if (modifiedFiles[keyname]) {
                            value.content = modifiedFiles[keyname];
                            delete modifiedFiles[keyname];
                        }
                    });

                    data.modifiedFiles = JSON.stringify(modifiedFiles);
                    data.removeAttachmentIds = removeFiles;
                }

                query({ data: data, success: callback });
            });
        },

        updateDocument: function (success) {
            var that,
                doc,
                selectedFiles,
                removeFiles,
                modifiedFiles,
                query,
                callback,
                data;

            that = this;

            that.attachments.confirmAttachments(function () {
                doc = that.serialize();

                selectedFiles = {};
                query = egov.request.saveDoc;

                that.attachments.model.each(function (file) {
                    if (file.get('isNew')) {
                        selectedFiles[file.get('Id')] = { name: file.get('Name') }
                    }
                });

                callback = function (result) {
                    egov.pubsub.publish(egov.events.status.success, result.success);
                    egov.callback(success, result);
                };

                if (that.transferType == egov.enum.documentTransferType.banGiaoKhiPhanLoai) {
                    doc.DocTypeId = that.newDoctype;
                }

                data = {
                    doc: JSON.stringify(doc),
                    files: JSON.stringify(selectedFiles)
                };

                removeFiles = that.attachments.model.select(function (file) {
                    return file.get('isRemoved');
                });
                removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

                modifiedFiles = that.attachments.modifiedFiles;
                $.each(selectedFiles, function (keyname, value) {
                    if (modifiedFiles[keyname]) {
                        value.content = modifiedFiles[keyname];
                        delete modifiedFiles[keyname];
                    }
                });

                data.modifiedFiles = JSON.stringify(modifiedFiles);
                data.removeAttachmentIds = removeFiles;

                query({ data: data, success: callback });
            });
        },

        RemoveDocument: function () {
            /// <summary>
            /// Hủy văn bản
            /// </summary>
            var that,
                documentCopyIds;
            that = this;
            documentCopyIds = parseInt(that.model.get("DocumentCopyId"));
            egov.request.removeDocument({
                data: {
                    documentCopyIds: [documentCopyIds]
                },
                success: function (result) {
                    that._reloadDocumentTree();
                    that.tab.close();
                }
            });
        },

        closeTabAndReloadTreeNode: function () {
            // close tab
            this.tab.close();

            // reload lại cây văn bản
            this._reloadDocumentTree(this.model.get("DocumentCopyId"));
        },

        getAttachmentsForSign: function () {
            var that = this;
            var docAttachments = this.attachments.model.models;
            var result = [];

            if (docAttachments.length > 0) {
                $.each(docAttachments, function (i, item) {
                    if (egov.fileExtension.isForSign(item.get("Name")) && item.get("hasRemoved") !== true && item.get("isRemoved") !== true) {
                        item.set("DocumentCopyId", that.model.get('DocumentCopyId'));
                        item.set("Compendium", that.model.get('Compendium'));
                        result.push(item);
                    }
                });
            }

            return result;
        },

        uploadSignFiles: function (signedFiles, success) {
            var that = this, newSignFiles = [];

            // Thêm phiên bản vào file cũ nếu file cũ không phải là word, excel.
            _.each(signedFiles, function (signedFile) {
                var oldAttachment = that.attachments.model.find(function (item) {
                    return String(item.get("Id")) === String(signedFile.id);
                });

                var fileName = oldAttachment.get("Name");

                var isSignedFile = fileName.endWith("_Signed.pdf", true);
                if (!isSignedFile || oldAttachment.get("isNew")) {
                    // Nếu mớới thêm hoặc file gốc là doc thì thêm mới file ký
                    newSignFiles.push(signedFile);
                } else {
                    // Nếu file gốc là pdf thì tạo phiên bản mới.
                    that.attachments.modifiedFiles[signedFile.id] = signedFile.value;
                }
            });

            if (newSignFiles.length === 0) {
                egov.callback(success);
                return;
            }

            egov.request.uploadTempScan({
                data: {
                    files: JSON.stringify(newSignFiles)
                },
                success: function (result) {
                    $.each(result, function (index, file) {
                        if (file.error !== "") {
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            var newAttachment = new egov.models.attachment({
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: file.extension,
                                Versions: [{
                                    Version: 1,
                                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                                }],
                                isNew: true,
                            });

                            that.attachments.model.add(newAttachment);
                        }
                    });
                    egov.callback(success);
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, "Lỗi trong quá trình upload file đã kí lên server! Vui lòng thử lại.");
                }
            });
        },

        //#endregion

        //#region Private Methods

        _renderLienthong: function () {
            var that = this,
                lienthongs = that.model.get("LienThongs");

            if (lienthongs == null || lienthongs.length === 0) {
                that.$("#wrapLienThong").remove();
            } else {
            }
        },

        _bindSuppPaperAndFee: function (papers, fees, isReturn) {
            var paperTemplate = '<li class="doc-paper list-group-item"><input class="paper-id" type="checkbox" value="${DocPaperId}" checked/><span class="paper-name">${PaperName}</span><input class="paper-amount form-control pull-right" type="text" value="${Amount}" /></li>';
            var feeTemplate = '<li class="doc-fee list-group-item"><input class="fee-id" type="checkbox" value="${DocFeeId}" checked/><span class="fee-name">${FeeName}</span><span class="pull-right currencyUnit" data-res="egov.resources.common.currencyUnit"></span><input class="fee-price pull-right form-control" type="text" value="${Price}" /></li>';
            this.$(".add-paper, .add-fee").remove();
            this.$(".doc-paper-supp, .doc-fee-supp").remove();
            this.$(".papers").append($.tmpl(paperTemplate, papers));
            this.$(".ul-papers").append(this.$newPaper.clone(true));

            this.$(".fees").append($.tmpl(feeTemplate, fees));
            this.$(".ul-fees").append(this.$newFee.clone(true));
        },

        _reloadDocumentTree: function (documentCopyId) {
            if (this.isOnlineRegistration || this.question) {
                egov.views.home.tree.reloadSelectedNode();
            } else {
                egov.views.home.tree.removeDocuments(documentCopyId);
            }

            //// reload lại cây văn bản
            // egov.views.home.tree.reloadSelectedNode();
        },

        _unescapeModel: function () {
            /// <summary>
            /// Un escape các giá trị trong model
            /// </summary>
            var that = this;
            _.each(that.model.attributes, function (key) {
                if (typeof that.model.get(key) === 'string') {
                    that.model.set(key, unescape(that.model.get(key)));
                    return;
                }

                if (key !== 'CommentList') {
                    return;
                }

                var commentList = that.model.get(key);
                _.each(commentList, function (comment) {
                    comment.Content = unescape(comment.Content);
                    if (typeof comment.Content == "string") {
                        comment.Content = comment.replace(/[\n]/gi, "").Content.split('\\n').join('<br />');
                    }
                });
                that.model.set(key, commentList);
            });
        },

        _openDocumentInPopUp: function (model, templateConfig) {
            /// <summary>
            /// Mở văn bản trên window.open
            /// </summary>
            var modelTemp,
                that;

            that = this;
            //Giá trị lấy về từ view có vài biến = null, lúc convert sang DocumentModel để transfer gặp lỗi, remove hết những biến này và để theo mặc định
            modelTemp = removeAllBlankOrNull(model);

            that.model = new DocumentModel(modelTemp);
            that.templateCfg = templateConfig;
            //that.$el.html(that.template);
            that.render();
            require(['egovPopup'], function () {
                egov.popup.autoSaveSize();
            });
        },

        _openCreateDocument: function (doctypeId) {
            /// <summary>
            /// Mở văn bản trong form khởi tạo
            /// </summary>
            var that = this;
            var doctype = _.find(egov.setting.allDoctypes, function (dt) {
                return dt.DocTypeId == doctypeId;
            });


            that.isHsmc = doctype.CategoryBusinessId === egov.enum.categoryBusiness.hsmc;
            that.isVanBanDen = doctype.CategoryBusinessId == egov.enum.categoryBusiness.vbden;

            egov.dataManager.getDocumentCreateInfo(that.id, that.relationAnswerId, {
                success: function (data) {
                    if (data) {
                        if (data.error) {
                            that.tab.close(true);
                            egov.pubsub.publish(egov.events.status.error, data.error);
                            return;
                        }

                        if (that.copiedDocument) {
                            var arr = ["DocCode", "InOutCode", "DocumentCopyId", "DocumentPermissions", "DateAppointed", "DateCreated", "DateResponse", "DateModified", "DateResult", "TransferType", "TransferTypeInEnum"];
                            _.each(arr, function (item) {
                                that.model.set(item, data[item]);
                            })

                        } else {
                            var organization = "";
                            if (that.model.get("Organization")) {
                                if (that.model.get("CategoryBusinessId") == 1) {
                                    that.model.set("InOutCode", data.InOutCode);
                                    that.model.set("DocCode", "");
                                    that.model.set("DocumentContents", []);
                                    that.model.set("Compendium", "");
                                    that.model.set("Attachments", []);
                                    that.model.set("CategoryId", data.CategoryId)
                                    that.model.set("StoreId", data.StoreId)
                                    that._getInOutCode();
                                }
                            } else {
                                that.model.set(data);
                            }

                        }

                        if (data.Stores && data.Stores.length > 0) {
                            if (!that.stores) {
                                that.stores = [];
                            }
                            that.stores.push({
                                StoreId: data.Stores[0].StoreId,
                                CategoryId: data.CategoryId,
                                Codes: data.CategoryBusinessId == 1 ? data.InOutCodes : data.DocCodes
                            });
                        }
                        that.render();
                    }
                }, error: function (xhr) {
                    that.tab.close();
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }, complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },

        _openEditDocument: function (documentCopyId) {
            /// <summary>
            /// Mở văn bản
            /// </summary>
            /// <summary>
            /// Ham lấy thồng tin cấu hình khi bàn giao văn bản
            /// </summary>
            var that = this,
                doc = that.documentInfo,
                docTypeId = doc ? doc.get("DocTypeId") : null,
                categoryBusinessId = doc ? doc.get("CategoryBusinessId") : null,
                currentNodeId = doc ? doc.get("NodeCurrentId") : null;
            var storePrivateId = that.model == undefined ? null : that.model.get("StorePrivateId");

            egov.dataManager.getDocumentEditInfo(that.id, storePrivateId, {
                success: function (data) {
                    if (data.error) {
                        that.tab.close();
                        egov.pubsub.publish(egov.events.status.error, data.error);
                        return;
                    }

                    that.isHsmc = data.document.CategoryBusinessId === egov.enum.categoryBusiness.hsmc;
                    that.isVanBanDen = data.document.CategoryBusinessId == egov.enum.categoryBusiness.vbden;

                    // đưa ra trạng thái người dùng mở văn bản đến không phải văn thư sẽ hiển thị không đầy đủ
                    that.hasMinTemplate = !egov.setting.userSetting.AlwaysDisplayFullDocumentInfo && (data.hasKhoiTao != null ? !data.hasKhoiTao : false);

                    //đưa những giá trị lấy được trên server vào model
                    that.model.set(data.document);

                    // Quyền đánh lại số đến
                    that.model.set("HasPrivateSaveToStore", data.hasPrivateSaveToStore);
                    that.model.set("DocumentPermissions", data.documentPermission);
                    that.model.set("LienThongs", data.lienThongs);

                    that.hasUpdatePermission = data.hasUpdatePermission;

                    that.publishPlan = data.plan.publish;

                    that._renderInfo();
                    that._initDocument();
                }, error: function (xhr) {
                    that.tab.close();
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }, complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },

        getDocumentTemplate: function () {
            var result = "";

            if (this.isHsmc) {
                result = "hsmc";
            }
            else if (this.isVanBanDen) {
                result = "vbden";
            } else {
                result = "vbdi";
            }

            this.fullTemplate = this.minTemplate = JSON.parse(documentTemplates[result]);

            if (!this.isHsmc) {
                result += "_min";
                this.minTemplate = documentTemplates[result];
            }
        },

        renderWarningCompilation: function () {
            var that = this;
            // get thoi gian bao cao theo filter
            var timeKey = that.$el.find("#ddlYearReport option:selected").val();
            // neu bao cao theo ngay
            if (that.findKyBaoCao(that.model.get("DocTypeId")) != 6) {
                const abElements = that.$el.find('.timeKey').filter(':not(.hidden)');

                _.each(abElements, function (item) {
                    const zeroPad = (num, places) => String(num).padStart(places, '0');
                    // nếu là monthkey thì phải + 1 với value, 2,3 la quy voi 6thang thi du nguyen
                    // thang voi tuan neu < 10 se them so 0
                    timeKey += that.findKyBaoCao(that.model.get("DocTypeId")) == 2 || that.findKyBaoCao(that.model.get("DocTypeId")) == 3 ? item.value : zeroPad((that.findKyBaoCao(that.model.get("DocTypeId")) == 4 ? parseInt(item.value) + 1 : item.value), 2);
                });
            } else {
                timeKey = that.formatDateTimeKey(that._getDate("DatePublished"));
            }
            if (that.warningCompilation) {
                that.warningCompilation.reload(timeKey)
            } else {
                require(["warningCompilation"], function (returnView) {
                    if (!returnView) {
                        return;
                    }
                    that.warningCompilation = new returnView({
                        document: that,
                        TimeKey: timeKey
                    });
                });
            }
        },

        _renderInfo: function () {
            //Không thể Render văn bản hồ sơ một cửa. Bắt try catch để tạm.
            var that = this;
            if (!this.model) {
                return;
            }

            if (this.isHsmc) {
                if (egov.views.document && this.isHsmc) {
                    this.model.set("DateCreated", egov.views.document.dateCreated);
                    this.model.set("DelayReason", egov.views.document.delayReason);
                }

                if (that.model.get("Approver") && that.model.get("Approver").length > 0) {
                    // Cho phép sửa ký duyệt nếu người ký là chính mình
                    _.each(that.model.get("Approver"), function (item) {
                        if (item.UserSendId == egov.setting.userId) {
                            item.hasPermission = true;
                        }
                    });
                } else {
                    that.model.set("Approver", undefined);
                }

                if (this.model.get("DocFees")) {
                    var docfees = this.model.get("DocFees");
                    var totalFee = 0;
                    for (var i = 0; i < docfees.length; i++) {
                        var price = docfees[i].Price;
                        totalFee += price;
                    }

                    totalFee = totalFee / 1000;

                    // this.model.set("TotalFee", totalFee + egov.setting.moneyFormat);
                    this.model.set("MoneyFormat", egov.setting.moneyFormat);
                }

                if (!this.model.get("HasJustCreated")) {
                    this.model.set("HasJustCreated", false);
                }

                this._getReturnResultTimeDefault();
            }

            if (!this.model.get("ExpireProcess")) {
                this.model.set("ExpireProcess", 1);
            }
            this.model.attributes.cid = this.model.cid;
            this.$el.html($.tmpl(this.template, this.model.toJSON()));

            this._showDocumentFormByTemplate();
            this.$("#ddlDateAppointRange option[value='" + this.model.get("ExpireProcess") + "']").attr("selected", "selected");
        },

        undoFinish: function (docCopyId) {
            /// <summary>
            /// Mở văn bản theo id
            /// </summary>
            ///<param name="docCopyId">Id của văn bản</param>
            var that = this;
            egov.request.undoFinish({
                data: {
                    documentCopyId: docCopyId,
                },
                success: function (result) {
                    that.closeTabAndReloadTreeNode();
                },
                error: function (xhr) {
                    $(egov.views.home.tree.documentList).addClass('display');
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }
            });
        },

        render: function () {
            /// <summary>
            /// Load giao diện
            /// </summary>

            this._unescapeModel();

            //Nếu không phải là giao diện mobile-tablet và đang hiên thị thông tin tóm tắt thì ghi đè html  không thì append html
            if (this.isShowingPreview) {
                var tempModel = this.model;
                tempModel.set("Compendium", tempModel.get("Compendium").split('\n').join('<br />'));
                this.$el.html(parseCommentNew(this.template, tempModel.toJSON()));
                this._initDocument();
            }
            else {
                this._renderInfo();
                this._initDocument();
            }
            this.renderBusinessLicense();
        },

        renderDocumentInfo: function (callback) {
            /// <summary>
            /// Render những thông tin có sẵn trên client
            /// </summary>
            /// <param name="callback"></param>
            /// <returns type=""></returns>
            var that = this,
                template;
            template = egov.template.document.desktopInfo;
            require([template], function (DocumentInfoTemplate) {
                that.$(".document-info-template").html($.tmpl(DocumentInfoTemplate, that.model.toJSON()));
                that._showDocumentFormByTemplate();
                egov.callback(callback);
            })
        },

        renderBusinessLicense: function () {
            return;
            var that = this;
            if (!that.isHsmc || that.isCreate || egov.isMobile) {
                return;
            }

            require(["BusinessLicenseShowView"], function (returnView) {
                if (!returnView) {
                    return;
                }

                egov.request.getBusinessLicense({
                    data: {
                        docCopyId: this.model.get('DocumentCopyId')
                    },
                    success: function (result) {
                        new returnView({
                            model: result,
                            document: that
                        });
                    },
                    error: function (error) {
                    }
                });
            });
        },

        renderInvoice: function () {
            var that = this;
            if (!that.isHsmc || that.isCreate || egov.isMobile) {
                return;
            }

            require(["InvoiceShowView"], function (returnView) {
                if (!returnView) {
                    return;
                }

                egov.request.getInvoice({
                    data: {
                        fkey: that.model.get('DocCode')
                    },
                    success: function (result) {
                        var doc = JSON.parse(result.data)
                        var item = doc.Data.Item;
                        if (!item) return;
                        if (item.status != 5) {
                            new returnView({
                                model: item,
                                document: that
                            });
                        }
                    },
                    error: function (error) {
                    }
                });
            });
        },

        reRenderInfo: function () {
            this.$("[name='Compendium']").val(this.model.get("Compendium"));
            this.$("[name='DocCode']").val(this.model.get("DocCode"));
            this.$("[name='Organization']").val(this.model.get("Organization"));
            if (this.model.get("DatePublished")) {
                var datePublished = Date.parse(this.model.get("DatePublished"));
                this.$("[name='DatePublished']").datepicker("setDate", datePublished);
            }

            this._checkDocCodeIsUsed();
        },

        renderStoreWhenChangeDocType: function (docTypeId) {
            var that = this;
            that.$("#StoreId").empty();

            var isDanhLaiSoDen = that.model.get("HasChangeInoutCode") || that.model.get("DocTypeId") == null || that.model.get("DocTypeId") == "00000000-0000-0000-0000-000000000000";

            if (!isDanhLaiSoDen) {
                return;
            }

            var doctype = _.find(egov.doctypes, function (dt) {
                return dt.DocTypeId == docTypeId;
            });

            if (!doctype) {
                return;
            }

            var stores = doctype.Stores;

            $.tmpl("<option value='${StoreId}'>${StoreName}</option>", stores).appendTo(that.$("#StoreId"));

            if (that.hasMinTemplate) {
                that._changeDocumentFormView();
            }

            that._changeStoreId();
        },

        _initDocument: function () {

            var that = this;

            that.hasUserCreate = that.isCreate || that.model.get("UserCreatedId") == egov.userId ? true : false;
            that.hasCompilation = that.isCreate || that.model.get("Status") == 1 && that.model.get("StatusReport") == 1 ? true : false;

            this.$el.parent().addClass("display");

            this._renderToolbar();

            this._parseValues();

            this._unescapeDocument();

            this._renderDateOverdue();

            if (!this.isCreate) {
                this._renderComments();
            } else {
                this.$('.document-extend').hide();
            }

            this._renderAttachment();

            //this._renderForm();

            //this._renderFormHandson();
            this._renderRelation();

            this._parseDateControl();

            this.$el.bindResources();

            this._renderCommonComments();

            this._autoResizeCompendium();

            this.$('form').attr('id', this.isCreate ? "CreateForm" : "EditForm");

            // this._initHsmc();

            require(["documentShortkey"], function (shortKey) {
                shortKey.init(that);
            });

            egov.pubsub.publish(egov.events.status.destroy);
        },

        _renderQuestionList: function () {
            var that = this;
            if (that.isCreate) {
                return;
            }
            require(['documentQuestions'], function (DocumentQuestions) {
                new DocumentQuestions({
                    documentId: that.model.get("DocumentId"),
                    document: that
                });
            })
        },

        _autocompleteCitizen: function () {
            /// <summary>
            /// Autocomplelte cho danh sách người dân, dùng trong việc tìm kiếm người dân ở hồ sơ 1 cửa
            /// </summary>
            var that = this,
                setSetting,
                targets,
                autocompleteCitizen,
                $name,
                $identityCard,
                $phone,
                $email,
                $address,
                $compendium;

            //Danh sách các input cho autocomplete
            targets = {
                citizenName: "CitizenName",
                identityCard: "IdentityCard",
                phone: "Phone",
                email: "Email",
                address: "Address"
            };

            $name = that.$('input[name="CitizenName"]');
            $identityCard = that.$('input[name="IdentityCard"]');
            $phone = that.$('input[name="Phone"]');
            $email = that.$('input[name="Email"]');
            $address = that.$('input[name="Address"]');
            $compendium = that.$("textarea[name=Compendium]")

            setSetting = function (target) {
                /// <summary>
                /// Set settings cho input
                /// </summary>
                /// <param name="target">input target</param>
                return {
                    minLength: 2,
                    source: function (request, response) {
                        $.ajax({
                            url: "/document/FilterCitizen/",
                            data: {
                                name: $name.val(),
                                identityCard: $identityCard.val(),
                                phone: $phone.val(),
                                email: $email.val(),
                                address: $address.val()
                            },
                            dataType: "json",
                            type: "POST",
                            success: function (data) {
                                response($.map(data, function (obj) {
                                    return {
                                        label: obj[target],
                                        value: obj[target],
                                        citizenName: obj.CitizenName,
                                        email: obj.Email,
                                        identityCard: obj.IdentityCard,
                                        phone: obj.Phone,
                                        address: obj.Address
                                    };
                                }));
                            }
                        });
                    },
                    //delay: 300,
                    autoFocus: true,
                    autoSelectFirst: true,
                    select: function (event, ui) {
                        /// <summary>
                        /// Set các input khác theo dữ liệu lấy về
                        /// </summary>
                        /// <param name="event"></param>
                        /// <param name="ui"></param>
                        $name.val(ui.item.citizenName);
                        $identityCard.val(ui.item.identityCard);
                        $phone.val(ui.item.phone);
                        $email.val(ui.item.email);
                        $address.val(ui.item.address);
                        $compendium.focus().val($compendium.val() + ' ');
                    }
                };
            }

            autocompleteCitizen = function (target) {
                /// <summary>
                /// Autocomplete 1 target
                /// </summary>
                /// <param name="target"></param>
                var settings,
                    citizenName,
                    email,
                    identityCard;

                settings = setSetting(target);
                if (that.$('input[name="' + target + '"]').length === 0) {
                    return;
                }
                that.$('input[name="' + target + '"]').autocomplete(settings).data("autocomplete")._renderItem = function (ul, item) {
                    ul.addClass('dropdown-menu');
                    citizenName = item.citizenName ? item.citizenName : _resource.noCitizenFullName;
                    email = item.email ? item.email : _resource.noCitizenEmail;
                    identityCard = item.identityCard ? item.identityCard : _resource.noCitizenIdCardNumber;

                    return $("<li>")
                        .data("item.autocomplete", item)
                        .append("<a href='#'>" + citizenName + " | " + email + " | " + identityCard + "</a>")
                        .appendTo(ul);
                };
            }

            //Khởi tạo autocomplte cho tất cả các input người dân
            for (var property in targets) {
                autocompleteCitizen(targets[property]);
            }
        },

        _unescapeDocument: function () {
            var key;
            for (key in this.model.attributes) {
                if (typeof this.model.get(key) === 'string') {
                    this.model.set(key, unescape(this.model.get(key)));
                } if (key === 'CommentList') {
                    var commentList = this.model.get(key);
                    for (var i = 0; i < commentList.length; i++) {
                        commentList[i].Content = unescape(commentList[i].Content);
                        commentList[i].Content = commentList[i].Content.replace(/[\n]/gi, "").split('\\n').join('<br />');
                    }
                    this.model.set(key, commentList);
                }
            }
        },

        _parseValues: function () {
            var that = this,
                inOutCodes,
                inOutCode;

            egov.dataManager.getCategories({
                success: function (data) {

                    // Lấy tên thể loại văn bản từ cache
                    var documentBusinessId = parseInt(that.model.get('CategoryBusinessId'));

                    var categoryForDoctype = _.filter(data, function (itm) {
                        return (itm.CategoryBusinessId & documentBusinessId) === documentBusinessId;
                    });

                    categoryForDoctype = _.sortBy(categoryForDoctype, function (c) { return c.CategoryName });

                    that.$('#CategoryId').append($.tmpl('<option value="${CategoryId}">${CategoryName}</option>', categoryForDoctype));
                    var category = _.find(data, function (itm) {
                        return itm.CategoryId === that.model.get('CategoryId');
                    });

                    if (category) {
                        that.model.set('CategoryName', category.CategoryName);
                        that.$('#CategoryId option[value="' + category.CategoryId + '"]').attr('selected', 'selected');
                    }
                }
            });

            egov.dataManager.getSendTypes({
                success: function (data) {
                    that.$('#SendTypeId').append($.tmpl('<option value="${Value}">${Text}</option>', data));
                    var sendType = _.find(data, function (itm) {
                        return itm.Value == that.model.get('SendTypeId');
                    });
                    if (sendType) {
                        that.model.set('SendTypeId', parseInt(sendType.Value));
                        that.$('#SendTypeId option[value="' + parseInt(sendType.Value) + '"]').attr('selected', 'selected');
                    }
                }
            });

            egov.dataManager.getCurrentDoctypes({
                success: function (data) {
                    var doctype = _.find(data, function (itm) {
                        return itm.DocTypeId === that.model.get('DocTypeId');
                    });
                    var doctypeName = "";

                    if (doctype === undefined) {
                        // Trường hợp mở văn bản không thuộc quy trình được khởi tạo: thông báo, xin ý kiến.
                        //TODO: update vào datamanager nếu không có
                        doctypeName = that.model.get('DocTypeName');
                    }
                    else {
                        doctypeName = doctype.DocTypeName;
                        that.model.set('DocTypeName', doctype.DocTypeName);
                    }

                    var $docTypeName = that.$('#DocTypeId');
                    if ($docTypeName.is("label")) {
                        $docTypeName.append(doctypeName);
                    } else if ($docTypeName.is("input")) {
                        $docTypeName.val(doctypeName);
                    }
                }
            });

            // Hiển thị phòng ban
            var showDepts = function (allDepts) {
                egov.dataManager.getDepartmentsCurrent({
                    success: function (currentDepts) {
                        var currentInOutPlace = that.model.get('InOutPlace');
                        var organizationCode = that.model.get('OrganizationCode');

                        if (!currentInOutPlace || currentInOutPlace == "") {

                            currentInOutPlace = currentDepts ? currentDepts[0].DepartmentName : allDepts[0];
                            organizationCode = currentDepts ? currentDepts[0].EdocId : "";
                        } else {
                            if (!that.isCreate) {
                                currentDepts = _.find(currentDepts, function (dept) {
                                    return dept.EdocId = organizationCode;
                                })
                            }
                        }
                        if (that.hasUserCreate) {
                            $.tmpl("<option value='${EdocId}'>${DepartmentName}</option>", currentDepts).appendTo(that.$("#InOutPlace"));
                            $.ajax({
                                type: "Get",
                                url: '/IndicatorCatalogValueView/GetLocality',
                                traditional: true,
                                data: {},
                                success: function (response) {
                                    $.tmpl("<option value='${code}'>${text}</option>", response).appendTo(that.$("#InOutPlace"));
                                    that.$("#InOutPlace option").each(function () {
                                        if ($(this).val() == organizationCode) {
                                            $(this).attr("selected", "selected");
                                        }
                                    });
                                }
                            });
                        }

                        that.model.set('InOutPlace', currentInOutPlace);
                    }
                });
            };

            egov.request.getAllDepartment({
                success: function (data) {
                    that.departmentCached = data;
                    var depts = _.pluck(data, "label");
                    showDepts(depts);
                }
            });

            inOutCodes = that.model.get("InOutCodes");

            if (inOutCodes) {
                _.each(_.keys(inOutCodes), function (key) {
                    that.$(".ddlDocCodes").append($('<li><a href="#" codeid="' + key + '">' + inOutCodes[key] + '</a></li>'));
                });
            }

            //inOutCode = that.model.get("InOutCode");
            //if (inOutCode) {
            //    that.$(".ddlDocCodes").append($('<li><a href="#">' + inOutCode + '</a></li>'));
            //}
        },

        _renderDateOverdue: function () {
            var that = this;

            this.$("#HasDateAppointed").click(function () {
                if ($(this).is(":checked")) {
                    that.$("[name='DateAppointed']").removeAttr("readonly");
                } else {
                    that.$("[name='DateAppointed']").attr("readonly", "readonly");
                }
            });

            this.$("#HasDateOverdue").click(function () {
                if ($(this).is(":checked")) {
                    that.$("[name='DateOverdue']").removeAttr("readonly");
                } else {
                    that.$("[name='DateOverdue']").attr("readonly", "readonly");
                }
            });
        },

        _autoResizeCompendium: function () {
            /// <summary>
            /// Tự động resize height của trích yếu.
            /// </summary>

            var compendiumElement,
                comHeight,
                that;

            that = this;
            compendiumElement = that.$("#wrapCompendium textarea");
            if (compendiumElement && compendiumElement.length > 0) {
                comHeight = compendiumElement[0].scrollHeight;
                compendiumElement.css({
                    "min-height": "36px",
                    "height": comHeight + "px",
                    "max-height": "none"
                });

                compendiumElement.keyup(function () {
                    compendiumElement.height('0px');
                    compendiumElement.height((comHeight + 15) + 'px');
                    that.heightChange = compendiumElement.height() - comHeight;
                });
            }
        },

        _showDocumentFormByTemplate: function () {
            /// <summary>
            /// Hiển thị form văn bản theo cấu hình
            /// </summary>
            var that = this;
            require(["formtemplate"], function () {
                that._renderTemplateByConfig();
                that.$('.formTmp').hide();
                return;
            });
        },

        _renderComments: function () {
            /// <summary>
            /// Hiển thị các ý kiến của document.
            /// </summary>
            var that = this;
            if (that.isCreate) {
                that.$('.document-extend').hide();
                return;
            }

            $.ajax({
                type: "GET",
                url: "/Document/GetTrangThaiLienThong",
                data: { id: that.model.attributes.DocumentCopyId },
                contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (response) {
                    var tinhtrangLienThong = that.$("#tinhTrangLienThongArea");
                    if (response == null || response.data == null) {
                        tinhtrangLienThong.hide();
                    }
                    else {
                        var doc_publish = response.data
                        if (doc_publish.IsPending) {
                            that.$("#tinhTrangLienThong").html("Đang liên thông");
                        }
                        else if (!doc_publish.IsPending) {
                            if (doc_publish.Traces) {
                                var Traces = JSON.parse(doc_publish.Traces);
                                if (Traces && Traces.length > 0) {
                                    that.$("#tinhTrangLienThong").html(Traces[0].description);
                                }
                                else {
                                    that.$("#tinhTrangLienThong").hide();
                                }

                            }
                            else {
                                that.$("#tinhTrangLienThong").hide();
                            }
                        }
                    }
                    //console.log(response);
                }


            })
            this.$('#commentList, #coCommentList').enableTextSelect();

            var elShowHide = this.$('.comment-title, #countComment');
            elShowHide.each(function () {
                $(this).on('click', function (e) {
                    egov.helper.destroyClickEvent(e);
                    var result = elShowHide.attr('data-open') === 'true' ? true : false;
                    if (result) {
                        that.$('#commentList').find('.comment-item').addClass("open");
                        that.$('#commentList').find('.list-group-item[data-display="false"]').show();
                        that.$('#coCommentList, .synthesis-comments').show();
                    }
                    else {
                        that.$('#commentList').find('.comment-item').removeClass("open");
                        that.$('#coCommentList,.synthesis-comments').hide();
                    }

                    elShowHide.attr('data-open', !result);
                    that.$(".synthesis-comments-title").attr('data-open', !result);
                });
            });

            this.$(".synthesis-comments-title").on("click", function (e) {
                egov.helper.destroyClickEvent(e);
                var result = $(this).attr('data-open') === 'true' ? true : false;
                if (result) {
                    that.$('#coCommentList,.synthesis-comments').show();
                } else {
                    that.$('#coCommentList,.synthesis-comments').hide();
                }

                $(this).attr('data-open', !result);
            });

            var openCommentEvent = function () {
                var divHeight = 0;
                that.$('#commentList, #coCommentList').bindResources();
                if (that.$('.comment-detail .wraptext').length > 0) {
                    that.$('.comment-detail .wraptext').dotdotdot({
                        watch: true,
                        height: 30
                    });
                }

                that.$('#commentList .comment-item, #coCommentList .comment-item').on("click", function () {
                    // var commentItem = $(this).parent(".comment-item");
                    $(this).toggleClass("open");
                });

                that.$(".displayAllComment").on("click", function (e) {
                    that.$('#commentList, #coCommentList').find(".dynamicComment").show();
                    $(this).parents("li.list-group-item").remove();

                    e.stopPropagation();
                });
            }

            // Do đã cache lại toàn bộ danh sách user, phòng ban rồi nên trên server không cần lấy ra user phòng ban nữa.
            // Mặc định trả xuống client và load ra từ cache.
            egov.dataManager.getAllUsers({
                success: function (allUsers) {

                    var tempCommentList = that.model.get('CommentList');
                    var comments = parseComment(tempCommentList, allUsers, that.model.get("CategoryBusinessId") == 32);

                    require([egov.template.document.comment], function (CommentTemplate) {

                        that.$('#commentList').html(parseCommentNew(CommentTemplate, comments.Processor));

                        if (comments.Processor.length > 3) {
                            // Thêm hiển thị tất cả ý kiến
                            that.$('#commentList').append(
                                $(String.format('<li class="list-group-item"><div class="comment-item"><span class="displayAllComment">{0}</span></div></li>', egov.resources.document.displayAllComment)));
                        }

                        _.each(comments.Processor, function (item) {
                            var children = item.Children;
                            that.$('.commentChirdren .child' + item.CommentId).html(parseCommentNew(CommentTemplate, children));
                        });

                        if (comments.CoProcessor.length > 0) {
                            that.$('#coCommentList').html(parseCommentNew(CommentTemplate, comments.CoProcessor));

                            that.$('#coCommentList .comment-item').addClass("open");

                            var first = _.filter(comments.CoProcessor, function (item) {
                                return item.CommentType == commentType.Contribution && item.DocumentCopyTargetId;
                            })[0];

                            if (first) {
                                that.$('.synthesis-comments').text(first.Content.ContentConSult);
                            }

                            _.each(comments.CoProcessor, function (item) {
                                var children = item.Children;
                                that.$('.commentChirdren .child' + item.CommentId).html(parseCommentNew(CommentTemplate, children));
                            });
                        }
                        else {
                            that.$('#coCommentList').parent().hide();
                        }

                        if (comments.CoProcessor.length > 3 || comments.Processor.length > 3) {
                            that.$(".displayAllComment").removeClass("hide");
                        }

                        openCommentEvent();
                    });
                }
            })
        },

        _renderToolbar: function (defaultToolbarType) {
            /// <summary>
            /// Hiển thị toolbar của document
            /// </summary>

            if (this.documentViewType === egov.enum.documentViewType.default) {
                this.$toolbar = this.$('.toolbar');
                //Xét toolbar của document phía bên phải
            } else if (egov.setting.userSetting.quickView === egov.enum.quickViewType.right) {
                this.$toolbar = this.$('.toolbarQuickView');
            }
                //Xét toolbar của document bên dưới
            else if (egov.setting.userSetting.quickView === egov.enum.quickViewType.below) {
                this.$toolbar = this.$('.toolbar');
            }
            var that = this;

            require(['documentToolbar'], function (Toolbar) {
                //if (that.toolbar) {
                //    that.toolbar.reRender(defaultToolbarType, that.model.get('DocumentPermissions'));
                //    return;
                //}

                that.toolbar = new Toolbar({
                    el: that.$toolbar,
                    model: that.model.get('DocumentPermissions'),
                    document: that,
                    defaultToolbarType: defaultToolbarType
                });

                egov.toolbar = that.toolbar;
            });
            setTimeout(function () {
                that.renderBangiaoName();
            }, 500)
        },

        renderBangiaoName: function () {
            var that = this;
            if (that.model.get("DocumentContents").length > 0 && that.model.get("DocumentContents")[0].FormCategoryId == 2) {
                that.$el.find(".btnTransfer span").eq(1).text("Giao kế hoạch");
            } else {
                if (that.isCreate) {
                    that.$el.find(".btnTransfer span").eq(1).text("Gửi báo cáo");
                } else {
                    that.$el.find(".btnTransfer span").eq(1).text("Chuyển");
                }
            }
        },

        _renderForm: function () {
            var form = this.model.get("DocumentContents")[0];
            var hasEdit = this.model.get("DocumentCopyId") === 0 || (this.model.get("UserCreatedId") == egov.userId);
            function makeid(length) {
                var result = '';
                var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
                var charactersLength = characters.length;
                for (var i = 0; i < length; i++) {
                    result += characters.charAt(Math.floor(Math.random() * charactersLength));
                }
                return result;
            }
            var config = {
                "document": {
                    "fileType": "xlsx",
                    "key": makeid(10),
                    "title": form.ContentName,
                    "url": egov.setting.eform.domain + form.ContentUrl,
                    "permissions": {
                        "comment": false,
                        "download": false,
                        "edit": hasEdit,
                        "fillForms": false,
                        "print": false,
                        "review": false
                    }
                },
                "height": "500px",
                "width": "100%",
                "lang": "vi_VN",
                "documentType": "spreadsheet",
                "editorConfig": {
                    "mode": "edit",
                    "callbackUrl": egov.setting.eform.domain + "webeditor.ashx?name=" + form.Url,
                    "customization": {
                        "compactToolbar": true,
                        "autosave": false,
                        "forcesave": false,
                        "chat": false,
                        "logo": {
                            "image": "/Content/logo.png"
                        },
                        "customer": {
                            "address": "Dương Đình Nghệ, Yên Hoà, Cầu Giấy, Hà Nội",
                            "info": "Công ty Cổ phần Bkav",
                            "logo": "https://example.com/logo-big.png",
                            "mail": "john@example.com",
                            "name": "Bkav Corp",
                            "www": "bkav.com"
                        },
                        "help": false
                    },
                    "plugins": {
                        "pluginsData": []
                    }
                }
            };

            this.DocEditor = new DocsAPI.DocEditor("divContent", config);
        },

        _renderTHDL: function () {
            var that = this;
            var kybc = this.findKyBaoCao(this.model.get("DocTypeId"))
            var datepublished = this.serializeDatePublished(kybc);
            var timekey = this.TimeKey;
            egov.request.getDataCompile({
                data: { timekey: timekey, doctypeId: this.model.get("DocTypeId") },
                success: function (result) {
                    var data = result.compile
                    if (data) {
                        var dt = [];
                        for (var i = 0; i < data.length; i++) {
                            dt.push(JSON.parse(data[i].CompilationData))
                        }
                        if (dt.length) {
                            var document = that.dataFormTable;
                            document.loadData(dt);
                        }
                    }
                }
            })
        },

        renderFormTranspose: function () {
            var that = this;
            var form = this.model.get("DocumentContents")[0];
            var formCode = JSON.parse(form.FormCode);
            var dataHeader = formCode.dataHeader;
            var dataHeaderTranspose = transpose(dataHeader);
            var countCol = dataHeaderTranspose[0].length
            var countRow = dataHeaderTranspose.length
            var colHeaders = [];

            for (var i = 0; i < countCol - 1; i++)
                colHeaders.push('')
            colHeaders.push('Đơn vị', 'Chỉ tiêu');

            // thêm giá trị mặc định cho chỉ tiêu
            for (var i = 0; i < countRow; i++)
                dataHeaderTranspose[i].push(null)

            var columsSetting = []
            for (var i = 0; i < countCol; i++)
                columsSetting.push({ readOnly: true })

            columsSetting.push({})

            var $hotElement = that.$('#divContent');
            var hotElementContainer = $hotElement.parentNode;
            that.hotSettings = {
                data: dataHeaderTranspose,
                stretchH: 'all',
                width: "100%",
                colHeaders: colHeaders,
                columns: columsSetting,
                autoWrapRow: false,
                manualColumnResize: true,
                manualRowResize: true,
                className: "htMiddle",
                dropdownMenu: true,
                rowHeaders: true,
                contextMenu: true,
                licenseKey: 'non-commercial-and-evaluation',
            };
            that.dataFormTable = new Handsontable($hotElement.get(0), that.hotSettings);

            var mergedCellsData = JSON.parse(formCode.headerMerge);
            var mergeCellsArray = [];
            if (mergedCellsData.length > 0) {
                mergeCellsArray = []
                mergedCellsData.forEach(function (e) {
                    mergeCellsArray.push({
                        row: e.col,
                        col: e.row,
                        rowspan: e.colspan,
                        colspan: e.rowspan
                    })

                });
                that.dataFormTable.updateSettings({
                    mergeCells: mergeCellsArray,
                })
            }
        },

        _renderFormHandson: function () {
            var that = this;
            //if (true) {
            //    that.renderFormTranspose();
            //    return;
            //}
            var validateInteger = function (value, callback) {
                setTimeout(function () {
                    if (isNaN(value)) {
                        callback(false);
                    }
                    else {
                        callback(true);
                    }
                }, 100);
            };

            var form = this.model.get("DocumentContents")[0];

            that.CompilationId = form.CompilationId
            that.FormId = form.FormId
            that.ChildCompilationId = form.ChildCompilationId
            that.ConfigFunction = form.ConfigFunction

            var formCode = this.model.get("ProcessInfo");
            if (that.isCreate) {
                formCode = form.FormCode;
            }
            // var formCode = form.FormCode;
            that.configHandsontable = formCode;
            var configExcel = JSON.parse(formCode);
            var header = configExcel.header;
            var data = configExcel.data;
            var headerNested = JSON.parse(configExcel.headerNested);

            var dataObject = that.isCreate ? data : JSON.parse(this.model.get("Note"));
            var widths = [];
            var columnObjs = _.mapObject(header, function (val, key) {
                var listName = key.split("!!");
                if (val == "string") {
                    widths.push(300)
                    val = 'text'
                }
                if (val == "double" || val == "float") {
                    widths.push(80)
                    val = 'numeric'
                }

                if (val == "int") {
                    widths.push(80)

                    val = 'numeric'
                    return {
                        data: listName[0],
                        type: val,
                        allowInvalid: true,
                        validator: validateInteger
                    };
                }
                return {
                    data: listName[0],
                    type: val,
                    allowInvalid: true,
                    width: 80
                };
            });

            var columnObjNames = _.mapObject(header, function (val, key) {
                var listName = key.split("!!");
                return listName[1]
            });

            var columns = _.values(columnObjs);
            var columnNames = _.values(columnObjNames);
            that.fisrt = true;

            var $hotElement = that.$('#divContent');
            var hotElementContainer = $hotElement.parentNode;
            that.hotSettings = {
                data: dataObject,
                columns: columns,
                stretchH: 'all',
                width: "100%",
                autoWrapRow: false,
                manualColumnResize: true,
                manualRowResize: true,
                height: 500,
                //dropdownMenu: true,
                rowHeaders: true,
                contextMenu: true,
                colHeaders: columnNames,
                nestedHeaders: headerNested,
                licenseKey: 'non-commercial-and-evaluation',
                colWidths: widths,
                beforePaste: function (data, coords) {
                    for (var i = 0; i < data.length; i++) {
                        for (var j = 0; j < data[i].length; j++) {
                            var valueCell = data[i][j];
                            data[i][j] = _.unescape(valueCell)
                            //var cell = that.dataFormTable.getCell(i + coords[0].startRow, j + coords[0].startCol)
                            var number = valueCell.replace(/,/g, '');
                            if (_.isNumber(number)) {
                                data[i][j] = number;
                            }
                        }
                    }

                    return data;
                },
                afterChange: function (changes, source) {
                    if (arguments[1] == "edit") {
                        that.dataFormTable.updateSettings({
                            height: $(`${that.el} div#divContent .ht_master .wtHider`).height()
                        });
                        $(`${that.el} div#divContent`).css("height", $(`${that.el} div#divContent .ht_master .wtHider`).height());
                        $(`${that.el} div#divContent .ht_master .wtHolder`)
                            .css("height", $(`${that.el} div#divContent .ht_master .wtHider`).height() + ($(`${that.el} div#divContent .ht_master .wtHider`).width() > $(`${that.el} div#divContent`).width() ? 17 : 0));
                    }
                }
            };
            that.dataFormTable = new Handsontable($hotElement.get(0), that.hotSettings);
            that.dataFormTable.updateSettings({
                height: $(`${that.el} div#divContent .ht_master .wtHider .htCore`).height()
            });
            $(`${that.el} div#divContent`).css("height", $(`${that.el} div#divContent .ht_master .wtHider .htCore`).height());
            //$(`${that.el} div#divContent .ht_master .wtHolder`)
            //    .css("height", $(`${that.el} div#divContent .ht_master .wtHider .htCore`).height() + ($(`${that.el} div#divContent .ht_master .wtHider .htCore`).width() > $(`${that.el} div#divContent`).width() ? 17 : 0));;
            ////có được tổng hợp từ cấp dưới
            //if (that.ChildCompilationId) {
            //    that._renderTHDL();
            //}
        },

        // 20191120 VuHQ REQ-5 START
        _renderHeaderFooterForm: function () {
            var that = this;
            var formCode = this.model.get("ProcessInfo");
            if (that.isCreate || that.model.get("Note") == "") {
                var form = this.model.get("DocumentContents")[0];
                if (form != undefined) {
                    that.FormId = form.FormId;
                    var formHeader = form.FormHeader;
                    var formFooter = form.FormFooter;
                    var templateKeyCodes = [];
                    if (formHeader != undefined) {
                        templateKeyCodes = formHeader.match(/@@(.*?)@@/gm) ? formHeader.match(/@@(.*?)@@/gm) : [];
                        _.each(formHeader.match(/##(.*?)##/gm),
                            function (v) {
                                templateKeyCodes.push(v);
                            });
                    }
                    var templateKeyCodesFooter = [];
                    if (formFooter != undefined) {
                        templateKeyCodesFooter = formFooter.match(/@@(.*?)@@/gm) ? formFooter.match(/@@(.*?)@@/gm) : [];
                        _.each(formFooter.match(/##(.*?)##/gm),
                            function (v) {
                                templateKeyCodesFooter.push(v);
                            });
                    }
                    var timeKey = $("#ddlYearReport option:selected").val();
                    if (templateKeyCodes.length > 0) {
                        _.each(templateKeyCodes, function (templateKeyCode) {
                            $.ajax({
                                type: "POST",
                                async: false,
                                //contentType: "application/json; charset=utf-8",
                                //dataType: "json",
                                url: '/Document/GetDataTemplateKeys',
                                traditional: true,
                                data: { 'templateKeyCode': templateKeyCode, 'formId': that.FormId, 'timeKey': timeKey },
                                success: function (response) {
                                    if (response.Success) {
                                        if (response.Type && (response.Type == 4 || response.Type == "default")) {
                                            formHeader = formHeader.replace(templateKeyCode, response.NewValue || "");
                                        } else
                                            formHeader = formHeader.replace(templateKeyCode, "");
                                    }
                                    else {
                                        formHeader = formHeader.replace(templateKeyCode, "");
                                    }
                                }
                            });
                        });
                    }
                    if (templateKeyCodesFooter.length > 0) {
                        _.each(templateKeyCodesFooter, function (templateKeyCode) {
                            $.ajax({
                                type: "POST",
                                async: false,
                                //contentType: "application/json; charset=utf-8",
                                //dataType: "json",
                                url: '/Document/GetDataTemplateKeys',
                                traditional: true,
                                data: { 'templateKeyCode': templateKeyCode, 'formId': that.FormId, 'timeKey': timeKey },
                                success: function (response) {
                                    if (response.Success) {
                                        if (response.Type && (response.Type == 4 || response.Type == "default")) {
                                            formFooter = formFooter.replace(templateKeyCode, response.NewValue || "");
                                        } else
                                            formFooter = formFooter.replace(templateKeyCode, "");
                                    }
                                    else {
                                        formFooter = formFooter.replace(templateKeyCode, "");
                                    }
                                }
                            });
                        });
                    }
                    $(`${that.el} .formHeader`).html(formHeader);
                    $(`${that.el} .formFooter`).html(formFooter);

                }
            }
            else {
                if (JSON.parse(formCode).headerFooter != undefined) {
                    var dataHeader = JSON.parse(JSON.parse(formCode).headerFooter);
                    $(`${that.el} .formHeader`).html(dataHeader.FormHeader);
                    $(`${that.el} .formFooter`).html(dataHeader.FormFooter);
                }
            }
        },
        _myFormat1: function (instance, td, row, col, prop, value, cellProperties) {
            var that = this;
            Handsontable.renderers.TextRenderer.apply(this, arguments);
            if (value && value != "") {
                td.innerHTML = new Intl.NumberFormat('de-DE', { currency: 'EUR' }).format(Number.parseFloat(value).toFixed(1));
                //td.innerHTML = value.toLocaleString('en-US', {
                //    currency: 'VND',
                //    minimumFractionDigits: 2,
                //    maximumFractionDigits: 2
                //});
            }

        },
        _myFormat2: function (instance, td, row, col, prop, value, cellProperties) {
            var that = this;
            Handsontable.renderers.TextRenderer.apply(this, arguments);
            if (value && value != "") {
                td.innerHTML = new Intl.NumberFormat('de-DE', { currency: 'EUR' }).format(Number.parseFloat(value).toFixed(2));
            }

        },
        _myFormat3: function (instance, td, row, col, prop, value, cellProperties) {
            var that = this;
            Handsontable.renderers.TextRenderer.apply(this, arguments);
            if (value && value != "") {
                td.innerHTML = new Intl.NumberFormat('de-DE', { currency: 'EUR' }).format(Number.parseFloat(value).toFixed(3));
            }

        },
        _myFormat4: function (instance, td, row, col, prop, value, cellProperties) {
            var that = this;
            Handsontable.renderers.TextRenderer.apply(this, arguments);
            if (value && value != "") {
                td.innerHTML = new Intl.NumberFormat('de-DE', { currency: 'EUR' }).format(Number.parseFloat(value).toFixed(4));
            }

        },
        _coverRenderer: function (instance, td, row, col, prop, value, cellProperties) {
            var escaped = Handsontable.helper.stringify(value), img;
            if (escaped.indexOf('http') === 0) {
                img = document.createElement('IMG');
                img.src = value;

                Handsontable.dom.addEvent(img, 'mousedown', function (e) {
                    e.preventDefault(); // prevent selection quirk
                });

                Handsontable.dom.empty(td);
                td.appendChild(img);
            }
            else {
                // render as text
                Handsontable.renderers.TextRenderer.apply(this, arguments);
            }

            return td;
        },
        _formatDate: function (date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [day, month, year].join('/');
        },
        firstDayOfWeek: function (year, week) {
            // Jan 1 of 'year'
            var d = new Date(year, 0, 1),
                offset = d.getTimezoneOffset();

            // ISO: week 1 is the one with the year's first Thursday 
            // so nearest Thursday: current date + 4 - current day number
            // Sunday is converted from 0 to 7
            d.setDate(d.getDate() + 4 - (d.getDay() || 7));

            // 7 days * (week - overlapping first week)
            d.setTime(d.getTime() + 7 * 24 * 60 * 60 * 1000
                * (week + (year == d.getFullYear() ? -1 : 0)));

            // daylight savings fix
            d.setTime(d.getTime()
                + (d.getTimezoneOffset() - offset) * 60 * 1000);

            // back to Monday (from Thursday)
            d.setDate(d.getDate() - 3);

            return d;
        },
        addDays: function (date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        },
        removeVietnameseTones: function (str) {
            str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
            str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
            str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
            str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
            str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
            str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
            str = str.replace(/đ/g, "d");
            str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
            str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
            str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
            str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
            str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
            str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
            str = str.replace(/Đ/g, "D");
            // Some system encode vietnamese combining accent as individual utf-8 characters
            // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
            str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
            str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
            // Remove extra spaces
            // Bỏ các khoảng trắng liền nhau
            str = str.replace(/ + /g, " ");
            str = str.trim();
            // Remove punctuations
            // Bỏ dấu câu, kí tự đặc biệt
            str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
            str = str.replace(/\s/g, '');
            str = str.toLowerCase();
            return str;
        },
        _renderFormHandsonPlus: function () {
            var form = this.model.get("DocumentContents")[0];
            //font hue
            $('textarea[name="Compendium"]').attr("disabled", true);
            $('textarea[name="Compendium"]').css("text-align", "center");
            $('textarea[name="Compendium"]').css("font-weight", "bold");
            $('textarea[name="Compendium"]').css("line-height", "25px");
            $('textarea[name="Compendium"]').attr('style', function (i, s) { return (s || '') + 'font-size: 20px !important;' });
            $('textarea[name="Compendium"]').attr('style', function (i, s) { return (s || '') + 'height: auto !important;' });
            //end font hue
            if (form == undefined) return;

            var that = this;

            that.CompilationId = form.CompilationId
            that.FormId = form.FormId
            that.ChildCompilationId = form.ChildCompilationId
            that.ConfigFunction = form.ConfigFunction

            var formCode = that.model.get("ProcessInfo");
            if (that.isCreate) {
                formCode = form.FormCode;
            }
            // var formCode = form.FormCode;
            that.configHandsontable = formCode;
            formCode = JSON.parse(formCode);
            var indexCkb = [];
            Object.keys(formCode.header).forEach(function (val, index) {
                if (formCode.header[val] == "bit") {
                    indexCkb.push(index)
                }
            })
            formCode.data.forEach(function (values, index) {
                Object.keys(values).forEach(function (_value, _index) {
                    if (indexCkb.indexOf(_index) !== -1) {
                        values[_value] = "false"
                    }
                })
            });

            var header = [];
            var data = [];
            var nestedHeaders = [];

            header = formCode.extra.columnSetting;
            nestedHeaders = formCode.extra.headerSetting;
            data = formCode.data;

            // 5 nam
            var actionLevel = that.model.get("ActionLevel");
            var categoryBusinessId = that.model.get("CategoryBusinessId");
            var year = that.$el.find("#ddlYearReport option:selected").val();

            if (actionLevel == 1 && (categoryBusinessId == 4 || categoryBusinessId == 64)) {
                var form = this.model.get("DocumentContents")[0];
                if (form == undefined) return;

                var formCode = that.model.get("ProcessInfo");
                if (that.isCreate) {
                    formCode = form.FormCode;
                }
                // var formCode = form.FormCode;
                that.configHandsontable = formCode;
                formCode = JSON.parse(formCode);
                nestedHeaders = formCode.extra.headerSetting;
                var countYear = 5;
                var dem = 0;
                var index = 0;
                nestedHeaders[0].forEach(function (key) {
                    if (key != null) {
                        if (key.charAt(0) == '$') {
                            var yearPlus = "Năm " + (year - countYear + dem);
                            nestedHeaders[0][index] = yearPlus;
                            dem++;
                        }
                    }
                    index++;
                })
            } else if ((categoryBusinessId == 4 || categoryBusinessId == 64) && actionLevel != 1) {
                nestedHeaders = formCode.extra.headerSetting;
            }
            //end 5 nam
            var colWidths = formCode.colWidths;
            var hiddenColumns = [];
            if (formCode.extra.hiddenColumns != undefined)
                hiddenColumns = formCode.extra.hiddenColumns;

            for (var i = 0; i < nestedHeaders.length; i++)
                for (var j = 0; j < nestedHeaders[0].length; j++)
                    if (nestedHeaders[i][j] == null)
                        nestedHeaders[i][j] = ''

            // Là form xoay
            var xoayObject = null;

            var dataObject = null;
            if (that.isCreate || that.model.get("Note") == "") {
                dataObject = data;
                if (formCode.extra.xoayObject != null) {
                    that.xoayObject = formCode.extra.xoayObject;
                    xoayObject = that.xoayObject;
                }
            }
            else {
                if (formCode.xoayData != null) {
                    dataObject = JSON.parse(formCode.xoayData);
                }
                else {
                    dataObject = JSON.parse(this.model.get("Note"));
                    //vien10
                    //var lengthDataObject = dataObject.length;
                    //var listMaBN = [];

                    //var docTypeId = that.model.get("DocTypeId");
                    //var timeKey = that.model.get("TimeKey");
                    //$.ajax({
                    //    url: "/Document/GetCompilationData",
                    //    async: false,
                    //    data: {
                    //        docTypeId: docTypeId,
                    //        timeKey: timeKey
                    //    },
                    //    type: "GET",
                    //    success: function (data) {
                    //        var dataArr = JSON.parse(data.data);
                    //        var lengthData = dataArr.length;
                    //        //if (lengthData > lengthDataObject) {
                    //        //    dataObject.forEach(function (val) {
                    //        //        var maBN = val.mabenhnhan;
                    //        //        listMaBN.push(maBN);
                    //        //    })
                    //        //    dataArr.forEach(function (_val, index) {
                    //        //        var maBN2 = _val.MaBN;
                    //        //        if (listMaBN.indexOf(maBN2) === -1) {
                    //        //            var obj = {};
                    //        //            obj.baohiem = dataArr[index].BHTD;
                    //        //            obj.chandoan = dataArr[index].ChanDoanTD;
                    //        //            obj.checkbox = false;
                    //        //            obj.diachi = dataArr[index].DiaChiTD;
                    //        //            obj.dienbienbenhvaxutridieutri = "";
                    //        //            obj.huongxutritieptheo = dataArr[index].HuongXTTTTD;
                    //        //            obj.khoa = dataArr[index].KhoaTD;
                    //        //            obj.mabenhnhan = dataArr[index].MaBN;
                    //        //            obj.ngayvaovien = dataArr[index].NgayVVTD;
                    //        //            obj.ten = dataArr[index].HoTenTDKN;
                    //        //            obj.tinhtranghientai = dataArr[index].TTHienTaiTD;
                    //        //            obj.tuoi = dataArr[index].TuoiTD;
                    //        //            dataObject.push(obj);
                    //        //        }
                    //        //    })

                    //        //}
                    //    }
                    //});
                }

                if (formCode.xoayObject != null) {
                    that.xoayObject = formCode.xoayObject;
                    xoayObject = that.xoayObject;
                }
            }
            //var dataObject = that.isCreate ? data : JSON.parse(this.model.get("Note"));

            var widths = [];

            if (xoayObject != null) {
                // generate lại nestedHeaders
                var nestedHeadersGenerated = [];
                var source = null;

                for (var i = 0; i < nestedHeaders.length; i++) {
                    var nestedHeadersGeneratedDetail = [];
                    for (var j = 0; j < nestedHeaders[0].length; j++) {
                        if (xoayObject.GiaTriIndex != j) {
                            if (xoayObject.CatalogIndex == j) {
                                if (xoayObject.CatalogValues == null || xoayObject.CatalogValues.length == 0) {
                                    $.ajaxSetup({ async: false });
                                    $.getJSON('/Admin/Form/GetCatalogValues',
                                        { catalogName: xoayObject.CatalogType, row: j, col: (nestedHeaders.length + 1), formId: form.FormId, isXoay: true },
                                        function (data, textStatus, jqXHR) {
                                            source = JSON.parse(data.catalogValues);
                                            xoayObject.CatalogValuesAscii = JSON.parse(data.catalogValuesAscii);
                                            xoayObject.CatalogValues = source
                                        });
                                    $.ajaxSetup({ async: true });
                                }
                                else if (xoayObject["CatalogValuesAscii"] == undefined) {
                                    // Bản mới thay đổi logic, nên với dữ liệu cũ chưa có catalogvaluesAscii thì phải generate lại
                                    $.ajaxSetup({ async: false });
                                    $.getJSON('/Admin/Form/GetCatalogValuesAscii',
                                        { strCatalogValues: JSON.stringify(xoayObject.CatalogValues) },
                                        function (data, textStatus, jqXHR) {
                                            source = xoayObject.CatalogValues;
                                            xoayObject["CatalogValuesAscii"] = JSON.parse(data.catalogValuesAscii);
                                        });
                                    $.ajaxSetup({ async: true });
                                }
                                else
                                    source = xoayObject.CatalogValues;

                                _.each(source, function (item) {
                                    nestedHeadersGeneratedDetail.push(item);
                                });
                                xoayObject.CatalogCount = source.length;

                            } else {
                                nestedHeadersGeneratedDetail.push(nestedHeaders[i][j]);
                            }
                        }
                    }

                    nestedHeadersGenerated.push(nestedHeadersGeneratedDetail);
                }

                nestedHeaders = nestedHeadersGenerated;
            }

            that.nestedHeaders = nestedHeaders

            var columnObjs = null;
            var newColumnObjs = [];
            var newColWidths = [];
            var newDataObject = [];
            var index = 0;
            if (xoayObject != null) {
                // 20200512 START đối ứng CATALOGNAME ==> CATALOGKEY cho trường hợp XOAY
                that.allCatalogValues = [];
                columnObjs = _.mapObject(header, function (val, key) {
                    var source = null;
                    var listName = key.split("!!");
                    if (val.TypeHandson == "dropdown") {
                        $.ajaxSetup({ async: false });
                        $.getJSON('/Admin/Form/GetCatalogValues', { catalogName: val.CatalogName, row: 1, col: 1, formId: form.FormId, isXoay: false },
                            function (data, textStatus, jqXHR) {
                                source = JSON.parse(data.catalogValues);
                                var catalogInfos = JSON.parse(data.catalogInfos);
                                jQuery.each(catalogInfos, function (i, val) {
                                    that.allCatalogValues.push(val);
                                });
                                that.allCatalogValues = _.uniq(that.allCatalogValues);
                            });
                        $.ajaxSetup({ async: true });
                    }
                });
                // 20200512 END
                $.each(header, function (key, val) {
                    var listName = key.split("!!");

                    if (index != xoayObject.GiaTriIndex) {
                        if (index == xoayObject.CatalogIndex) {
                            var giaTriKey = xoayObject.GiaTriNameAscii + "!!" + xoayObject.GiaTriName;
                            for (var i = 0; i < xoayObject.CatalogCount; i++) {
                                var propertyColumn = {};
                                propertyColumn.data = xoayObject.CatalogValuesAscii[i];
                                propertyColumn.type = header[giaTriKey].TypeHandson;
                                propertyColumn.readOnly = header[giaTriKey].ReadOnly;
                                //propertyColumn.allowInvalid = header[giaTriKey];
                                propertyColumn.allowInvalid = true;
                                propertyColumn.validator = new RegExp(header[giaTriKey].RegEx);
                                propertyColumn.source = null;
                                propertyColumn.className = header[giaTriKey].ReadOnly ? 'htLeft gray' : 'htLeft'

                                if (header[giaTriKey].TypeName == 'Số thực' && header[giaTriKey].CatalogName != '') {
                                    switch (header[giaTriKey].CatalogName.split(".")[1].length) {
                                        case 1:
                                            propertyColumn.renderer = that._myFormat1;
                                            //propertyColumn.numericFormat = {
                                            //    pattern: header[giaTriKey].CatalogName,
                                            //    //pattern: '0,0.0',
                                            //    culture: 'de-GB'
                                            //};
                                            break;
                                        case 2:
                                            propertyColumn.renderer = that._myFormat2;
                                            break;
                                        case 3:
                                            propertyColumn.renderer = that._myFormat3;
                                            break;
                                        case 4:
                                            propertyColumn.renderer = that._myFormat4;
                                            break;
                                    }
                                }
                                else if (header[giaTriKey].TypeName == 'Hình ảnh') {
                                    propertyColumn.renderer = that._coverRenderer;
                                }

                                newColumnObjs.push(propertyColumn);
                                newColWidths.push(colWidths[xoayObject.CatalogIndex]);
                            }
                        }
                        else {
                            var propertyColumn = {};
                            propertyColumn.data = listName[0];
                            propertyColumn.type = val.TypeHandson;
                            propertyColumn.readOnly = val.ReadOnly;
                            //propertyColumn.allowInvalid = val.Required;
                            propertyColumn.allowInvalid = true;
                            propertyColumn.validator = new RegExp(val.RegEx);
                            propertyColumn.source = source;
                            propertyColumn.className = val.ReadOnly ? 'htLeft gray' : 'htLeft'

                            if (val.TypeName == 'Số thực' && val.CatalogName != '') {
                                switch (val.CatalogName.split(".")[1].length) {
                                    case 1:
                                        propertyColumn.renderer = that._myFormat1;
                                        //propertyColumn.numericFormat = {
                                        //    pattern: val.CatalogName,
                                        //    //pattern: '0,0.0',
                                        //    culture: 'de-GB'
                                        //};
                                        break;
                                    case 2:
                                        propertyColumn.renderer = that._myFormat2;
                                        break;
                                    case 3:
                                        propertyColumn.renderer = that._myFormat3;
                                        break;
                                    case 4:
                                        propertyColumn.renderer = that._myFormat4;
                                        break;
                                }
                            }
                            else if (val.TypeName == 'Hình ảnh') {
                                propertyColumn.renderer = that._coverRenderer;
                            }

                            newColumnObjs.push(propertyColumn);
                            newColWidths.push(colWidths[index]);
                        }
                    }

                    index++;
                });

                if (this.isCreate) {
                    $.each(dataObject, function (i, val) {
                        index = 0;
                        var newDataObjectDetail = {};

                        Object.keys(val).forEach(function (key) {
                            if (index != xoayObject.GiaTriIndex) {
                                if (index == xoayObject.CatalogIndex) {
                                    for (var i = 0; i < xoayObject.CatalogCount; i++) {
                                        newDataObjectDetail[xoayObject.CatalogValuesAscii[i]] = val[xoayObject.GiaTriNameAscii];
                                    }
                                }
                                else {
                                    if (!_.contains([xoayObject.GiaTriNameAscii, xoayObject.CatalogNameAscii, "pos"], key)) {
                                        newDataObjectDetail[key] = val[key];
                                    }
                                }
                            }
                            index++;
                        });

                        newDataObject.push(newDataObjectDetail);
                    });
                }

                if (newDataObject.length > 0)
                    dataObject = newDataObject;
            }
            else {
                that.allCatalogValues = [];
                columnObjs = _.mapObject(header, function (val, key) {
                    var source = null;
                    var listName = key.split("!!");
                    if (val.TypeHandson == "dropdown") {
                        $.ajaxSetup({ async: false });
                        $.getJSON('/Admin/Form/GetCatalogValues', { catalogName: val.CatalogName, row: 1, col: 1, formId: form.FormId, isXoay: false },
                            function (data, textStatus, jqXHR) {
                                source = JSON.parse(data.catalogValues);
                                var catalogInfos = JSON.parse(data.catalogInfos);
                                jQuery.each(catalogInfos, function (i, val) {
                                    that.allCatalogValues.push(val);
                                });
                                that.allCatalogValues = _.uniq(that.allCatalogValues);
                            });
                        $.ajaxSetup({ async: true });
                    }

                    index++;

                    var propertyColumn = {};
                    propertyColumn.data = listName[0];
                    propertyColumn.type = val.TypeHandson;
                    propertyColumn.readOnly = val.ReadOnly;
                    // propertyColumn.allowInvalid = val.Required;
                    propertyColumn.allowInvalid = true;
                    propertyColumn.validator = new RegExp(val.RegEx);
                    propertyColumn.source = source;
                    propertyColumn.className = val.ReadOnly ? 'htLeft gray' : 'htLeft';

                    if (val.TypeName == 'Số thực' && val.CatalogName != '') {
                        switch (val.CatalogName.split(".")[1].length) {
                            case 1:
                                propertyColumn.renderer = that._myFormat1;
                                //propertyColumn.numericFormat = {
                                //    pattern: val.CatalogName,
                                //    //pattern: '0,0.0',
                                //    culture: 'de-GB'
                                //};
                                break;
                            case 2:
                                propertyColumn.renderer = that._myFormat2;
                                break;
                            case 3:
                                propertyColumn.renderer = that._myFormat3;
                                break;
                            case 4:
                                propertyColumn.renderer = that._myFormat4;
                                break;
                        }
                    }
                    else if (val.TypeName == 'Hình ảnh') {
                        propertyColumn.renderer = that._coverRenderer;
                    }

                    return propertyColumn;
                });
            }

            if (newColumnObjs.length > 0)
                columnObjs = newColumnObjs;

            if (newColWidths.length > 0)
                colWidths = newColWidths;

            var columnObjNames = _.mapObject(header, function (val, key) {
                var listName = key.split("!!");
                return listName[1]
            });
            var columns = _.values(columnObjs);
            that.globalColumns = columns;
            that.columnsSetting = columns;
            //var columnNames = _.values(columnObjNames);
            that.fisrt = true;

            // 20200213 VuHQ Phase 2 - REQ-0 Báo cáo chỉ tiêu START
            if (form.Compilation != null && that.hasCompilation) {
                var compilation = JSON.parse(form.Compilation);
                var dataObjectCompilation = JSON.parse(compilation.data);
                var codeObjectCompilation = undefined;
                var dataObjectMatchOff = [];
                if (that.model.get("DocumentContents")[0].FormCategoryId == 3) {
                    codeObjectCompilation = JSON.parse(compilation.code).summaryConfigJsonForm;
                    if (dataObjectCompilation != undefined && codeObjectCompilation != undefined) {
                        dataObject.forEach(function (dataObjectDetail, index) {
                            dataObjectCompilation.forEach(function (dataObjectCompilationDetail, indexCompilation) {
                                if (codeObjectCompilation.schema.Form_Compilation_Match_Off == undefined) {
                                    if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] != ""
                                        && dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Target_Match.default.trim()]) {
                                        var keyFields = Object.keys(codeObjectCompilation.schema);
                                        for (var i = 3; i < keyFields.length; i++) {
                                            var keyField = keyFields[i].replace("Form_", "");
                                            if (codeObjectCompilation.schema[keyFields[i]].default != "") {
                                                var value = dataObjectCompilationDetail[codeObjectCompilation.schema[keyFields[i]].default];
                                                dataObjectDetail[keyField] = value;
                                            }
                                        }
                                    }
                                }
                                else if (codeObjectCompilation.schema.Form_Compilation_Match_Off.default == 1) {
                                    if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] != undefined &&
                                        dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default.trim()] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Target_Match.default.trim()]) {
                                        if (that.model.get("DocumentContents")[0].FormCategoryId == 3) {
                                            var keyFields = Object.keys(codeObjectCompilation.schema);
                                            for (var i = 4; i < keyFields.length; i++) {
                                                var keyField = keyFields[i].replace("Form_", "");
                                                if (codeObjectCompilation.schema[keyFields[i]].default != "") {
                                                    var value =
                                                        dataObjectCompilationDetail[codeObjectCompilation.schema[
                                                            keyFields[i]].default];
                                                    dataObjectDetail[keyField] = value;
                                                }
                                            }
                                        } else {
                                            dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display
                                                .default] = dataObjectCompilationDetail[codeObjectCompilation.schema
                                                .Form_Compilation_Select.default];
                                        }
                                    }
                                } else {
                                    if (that.model.get("DocumentContents")[0].FormCategoryId == 3) {
                                        var keyFields = Object.keys(codeObjectCompilation.schema);
                                        var obj = JSON.parse(JSON.stringify(dataObjectDetail));
                                        for (var i = 4; i < keyFields.length; i++) {
                                            var keyField = keyFields[i].replace("Form_", "");
                                            if (codeObjectCompilation.schema[keyFields[i]].default != "") {
                                                var value =
                                                    dataObjectCompilationDetail[codeObjectCompilation.schema[
                                                        keyFields[i]].default];
                                                obj[keyField] = value;
                                            }
                                        }
                                        dataObjectMatchOff.push(obj);
                                    } else {
                                        dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display
                                            .default] = dataObjectCompilationDetail[codeObjectCompilation.schema
                                            .Form_Compilation_Select.default];
                                    }
                                }
                                //if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] != undefined
                                //    && dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Target_Match.default]) {
                                //    if (that.model.get("DocumentContents")[0].FormCategoryId == 3) {
                                //        var keyFields = Object.keys(codeObjectCompilation.schema);
                                //        for (var i = 3; i < keyFields.length; i++) {
                                //            var keyField = keyFields[i].replace("Form_", "");
                                //            if (codeObjectCompilation.schema[keyFields[i]].default != "") {
                                //                var value = dataObjectCompilationDetail[codeObjectCompilation.schema[keyFields[i]].default];
                                //                dataObjectDetail[keyField] = value;
                                //            }
                                //        }
                                //    }
                                //    else {
                                //        dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display.default] = dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Select.default];
                                //    }
                                //}
                            });
                        });
                    }
                }
                else if (that.model.get("DocumentContents")[0].FormCategoryId == 2) {
                    codeObjectCompilation = JSON.parse(compilation.code).targetConfigJsonForm;
                    if (dataObjectCompilation != undefined && codeObjectCompilation != undefined) {
                        dataObject.forEach(function (dataObjectDetail, index) {
                            dataObjectCompilation.forEach(function (dataObjectCompilationDetail, indexCompilation) {
                                if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Match.default] != undefined && dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Match.default] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Match.default]) {
                                    dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display.default] = dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Select.default];
                                }
                            });
                        });
                    }
                }
                if (dataObjectMatchOff.length > 0) {
                    dataObject = dataObjectMatchOff;
                }
            }
            // 20200213 VuHQ Phase 2 - REQ-0 Báo cáo chỉ tiêu END
            var $hotElement = that.$('#divContent');

            // START Autosize
            if (formCode.extra.autoSizeColumns != undefined) {
                var autoSizeColumns = formCode.extra.autoSizeColumns;
                var totalOtherWidths = 0;
                var index = 0;
                _.each(colWidths, function (colWidth) {
                    if (autoSizeColumns[0] != index && jQuery.inArray(index, hiddenColumns) == -1)
                        totalOtherWidths += colWidth;
                    index++;
                });

                // trừ 50 là trừ đi width của rowHeader (cột đầu)
                if (colWidths != null) {
                    colWidths[autoSizeColumns[0]] = $(`${that.el} div.formHeader`).width() - totalOtherWidths - 50 - 15;
                    //colWidths[autoSizeColumns[0]] = $(`${that.el} div#divContent`).width() - totalOtherWidths - 50;

                }
            }
            // END Autosize

            // 20200227 START Chuyển CatalogKey thành CatalogName để hiển thị
            var j;
            _.each(dataObject, function (dataFormRow) {
                j = 0;
                Object.keys(dataFormRow).forEach(function (key) {
                    if (columns[j] != undefined && columns[j].type == 'dropdown') {
                        var catalogValues = that.allCatalogValues.filter(d => d.CatalogKey == dataFormRow[key] && d.CatalogKey != "");
                        if (catalogValues.length > 0 && catalogValues[0].Value != null)
                            dataFormRow[key] = catalogValues[0].Value;
                    }
                    j++;
                });
            });

            //excel with data mysql
            var dataNew = [];
            if (form.FormCode === undefined) {
                dataNew = dataObject;
                var dataNormal = dataNew;

                var countExcel = 0;

                dataNormal.forEach((nor, index) => {
                    var count = 0;
                    var dataa = dataObject[index];
                    var objnew = {};
                    Object.entries(nor).forEach(function (key, index) {
                        var keynor = key[0];
                        var valnor = key[1];
                        var indexnor = index;
                        if (valnor && valnor != "" && valnor.length > 1 && valnor.charAt(0) == "=") {
                            if (valnor.charAt(0) == "=") {
                                countExcel++;
                            }
                        }
                    });
                });
                if (countExcel > 0) {
                    dataNew = JSON.parse(this.model.get("Note"))
                } else {
                    dataNew = dataObject;
                }
            } else {
                var dataNormal = JSON.parse(form.FormCode).data;
                var countExcel = 0;

                dataNormal.forEach((nor, index) => {
                    var count = 0;
                    var dataa = dataObject[index];
                    var objnew = {};
                    Object.entries(nor).forEach(function (key, index) {
                        var keynor = key[0];
                        var valnor = key[1];
                        var indexnor = index;
                        if (valnor != "" && valnor.length > 1 && valnor.charAt(0) == "=") {
                            if (valnor.charAt(0) == "=") {
                                countExcel++;
                            }
                        }
                    });
                });
                if (countExcel > 0) {
                    dataNormal.forEach((nor, index) => {
                        var count = 0;
                        var dataa = dataObject[index];
                        var objnew = {};
                        Object.entries(nor).forEach(function (key, index) {
                            var keynor = key[0];
                            var valnor = key[1];
                            var indexnor = index;
                            if (valnor != "" && valnor.length > 1 && valnor.charAt(0) == "=") {
                                if (valnor.charAt(0) == "=") {
                                    objnew[keynor] = valnor;
                                } else {
                                    objnew[keynor] = dataa[keynor];
                                }
                            } else {
                                objnew[keynor] = dataa[keynor];
                            }
                        });
                        dataNew.push(objnew);
                    });
                } else {
                    dataNew = dataObject;
                }
            }
            // end

            //handle checkbox
            if (form.DefineConfigJson != undefined) {
                var countCheckbox = 0;
                var arrCheckbox = [];
                var dataConfig = JSON.parse(form.DefineConfigJson).data;
                for (var cb = 0; cb < dataConfig.length; cb++) {
                    dataConfig[cb].forEach(function (cbVal) {
                        if (cbVal != "" && cbVal != null && cbVal == "Checkbox" && dataConfig[cb][0] != null) {
                            countCheckbox++;
                            var dataConvert = that.removeVietnameseTones(dataConfig[cb][0]);
                            arrCheckbox.push(dataConvert);
                        }
                    })
                }
                if (countCheckbox > 0) {
                    dataNew.forEach(function (val) {
                        for (var arrcb = 0; arrcb < arrCheckbox.length; arrcb++) {
                            var colData = val[arrCheckbox[arrcb]];
                            colData == 1 ? val[arrCheckbox[arrcb]] = "true" : val[arrCheckbox[arrcb]] = "false";
                        }
                    })
                }
            }
            //end handle checkbox
            $.each(dataNew, function (index, value) {
                $.each(value, function (key, val) {
                    if (val === undefined) {
                        dataNew[index][key] = null;
                    }
                })
            })
            // 20200227 END Chuyển CatalogKey thành CatalogName để hiển thị
            that.hotSettings = {
                data: dataNew,
                mergeCells: formCode.extra.mergedCells.length == 0 ? true : formCode.extra.mergedCells,
                colHeaders: true,
                rowHeaders: true,
                columns: columns,
                colWidths: colWidths,
                //nestedHeaders: nestedHeaders,
                filters: true,
                dropdownMenu: {
                    items: {
                        "filter_by_condition": {
                            name: 'Lọc theo điều kiện'
                        },
                        //"filter_by_value": {
                        //   name: "Lọc theo giá trị"
                        //},
                        "filter_action_bar": { name: "okeee" }
                    }
                },
                className: "htLeft",
                //columnHeaderHeight: 35,
                stretchH: 'none',
                //width: '100%',
                height: '100%',
                hiddenColumns: {
                    columns: hiddenColumns,
                    indicators: false
                },
                comments: true,
                //afterSelection: function (r, c, r2, c2, preventScrolling, selectionLayerLevel) {
                //    preventScrolling.value = true;
                //},
                outsideClickDeselects: false,
                contextMenu: {
                    items: {
                        'remove_row': {
                            name: "Xóa dòng"
                        },
                        'row_above': {
                            name: "Thêm dòng bên trên"
                        },
                        'row_below': {
                            name: "Thêm dòng bên dưới"
                        },
                        "make_threshold": {
                            name: 'Kiểm tra ngưỡng chỉ tiêu',
                            callback: function (key, options) {
                                that.thresHold();
                            }
                        },
                        "add_locality": {
                            name: 'Thêm mới đia bàn',
                            callback: function (key, options) {
                                that.addIndicator(true);
                            }
                        },
                        "edit_rowdata": {
                            name: 'Sửa dữ liệu',
                            callback: function (key, options) {
                                that.editDataRow(true);
                            }
                        }
                    }
                },
                manualColumnResize: true,
                //viewportColumnRenderingOffset: 500,
                //viewportRowRenderingOffset: 1000,
                formulas: true,
                //wordWrap: true,
                licenseKey: 'non-commercial-and-evaluation',
                //afterChange: function (changes, source) {
                //    if (arguments[1] == "edit") {
                //        var rowCount = that.dataFormTable.getData().length;
                //        if (rowCount != that.lengthRow) {
                //            that.dataFormTable.updateSettings({
                //                height: $(`${that.el} div#divContent .ht_master .wtHider`).height()
                //            });
                //            that.lengthRow = rowCount;
                //        } 
                //        //$(`${that.el} div#divContent`).css("height", $(`${that.el} div#divContent .ht_master .wtHider`).height());
                //        //$(`${that.el} div#divContent .ht_master .wtHolder`)
                //        //    .css("height", $(`${that.el} div#divContent .ht_master .wtHider`).height() + ($(`${that.el} div#divContent .ht_master .wtHider`).width() > $(`${that.el} div#divContent`).width() ? 17 : 0));
                //    }
                //}
            };

            that.renderHeaderHandson(formCode, colWidths)

            if (typeof ($hotElement.get(0)) == 'undefined') return;
            $hotElement.get(0).innerHTML = "";
            if (that.dataFormTable != null)
                that.dataFormTable.destroy();
            that.dataFormTable = new Handsontable($hotElement.get(0), that.hotSettings);
            that.dataFormTable.updateSettings({
                //height: $(`${that.el} div#divContent .ht_master .wtHider .htCore`).height(),
                //vien 10
                height: 500
            });
            // that.dataFormTable.loadData(dataNew);

            //$(`${that.el} div#divContent`).css("height", $(`${that.el} div#divContent .ht_master .wtHider .htCore`).height());
            //$(`${that.el} div#divContent .ht_master .wtHolder`)
            //    .css("height", $(`${that.el} div#divContent .ht_master .wtHider .htCore`).height() + ($(`${that.el} div#divContent .ht_master .wtHider .htCore`).width() > $(`${that.el} div#divContent`).width() ? 17 : 0));


            //// 20200212 Phase 2 START REQ-0 thêm tính năng export csv
            //var r = $('<div class="custom-file"><label class="custom-file-label" for="customFile"></label><form id="uploader" enctype="multipart/form-data"><input type="file" id="fileImportData" class= "custom-file-input" accept = "*.xls, *.xlsx" id = "customFile"><input type="button" id="btnImportData" style="margin-top:5px" class="btn btn-primary" value="Import dữ liệu từ Excel"/><div style="display: -webkit-inline-box; margin: 10px 0px 0px 10px; padding-left: 5px"><input style="width: 200px" type="text" id="strHeaderAI" title="[vị trí index bắt đầu header], [vị trí index kết thúc header] (ví dụ: 1,2)"></div></form></div>');
            ////var r = $('<a href="#" data-restitle="egov.resources.document.download" title="Import từ excel" class="import-file btn btn-primary"><span class="icon-download2">  Import từ excel</span></a>');
            ////var r = $('<input class="btn" type="file" name="files" id="upload" style="display: none;" accept = "*.xlsx" />');
            //$(".divImportExcel").append(r);


            that.dataFormTable.addHook('beforePaste', function (data, coords) {
                for (var i = 0; i < data.length; i++) {
                    for (var j = 0; j < data[i].length; j++) {
                        data[i][j] = data[i][j].trim();
                        var valueCell = data[i][j];
                        if (valueCell != undefined) {
                            data[i][j] = _.unescape(valueCell)
                            var number = valueCell.replace(/./g, '');
                            var number = valueCell.replace(/,/g, '.');
                            if (!isNaN(number)) {
                                data[i][j] = number;
                            }
                        }
                    }
                }
                return data;
            });
            that.dataFormTable.addHook("afterChange", function (changes, source) {
                if (source === 'init' || source === 'edit') {
                    var valueOld = changes[0][2];
                    var valueNew = changes[0][3];
                    if (valueOld != valueNew) {
                        localStorage.setItem("isChangeForm", "true");
                    }
                }
            });
            //that.dataFormTable.addHook('afterRender', (isForced) => {
            //    var h0 = that.$('#divContent')[0];
            //    var width = 0.0;
            //    for (var j = 1; j <= that.dataFormTable.countCols(); j++) {

            //        var st1 = '.ht_clone_top  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')'
            //        var st2 = '.ht_master  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')';

            //        if (h0.querySelectorAll(st1)[0] != undefined) {
            //            if (jQuery.inArray((j - 1), hiddenColumns) == -1) {
            //                width += colWidths[j - 1];
            //                h0.querySelectorAll(st1)[0].setAttribute('style', 'width:' + colWidths[j - 1] + "px; height: unset !important");

            //                if (h0.querySelectorAll(st2)[0] != undefined) {
            //                    h0.querySelectorAll(st2)[0].setAttribute('style', 'width:' + colWidths[j - 1] + "px; height: unset !important");
            //                }
            //            }
            //        }
            //    }

            //    setTimeout(function () {
            //        $(`${that.el} div#divContent .ht_master.handsontable .wtHolder .wtHider`).css("width", width);
            //        $(`${that.el} div#divContent .ht_master.handsontable .wtHolder`).css("overflow-y", "unset");
            //        $(`${that.el} div#divContent .ht_master.handsontable .wtHolder`).css("overflow-x", "auto");
            //    }, 10);

            //    //$(`${that.el} div#divContent`).css("height", $(`${that.el} div#divContent .ht_master .wtHider`).height());
            //   // $(`${that.el} div#divContent .ht_master .wtHolder`)
            //       // .css("height", $(`${that.el} div#divContent .ht_master .wtHider`).height() + ($(`${that.el} div#divContent .ht_master .wtHider`).width() > $(`${that.el} div#divContent`).width() ? 17 : 0));

            //    var _formCode = JSON.parse(that.configHandsontable);
            //    var fieldTableMergedCells = _formCode.headerNested;
            //    if (fieldTableMergedCells.length > 0) {
            //        var delarray = [];
            //        delarray = _formCode.extra.headerSetting;

            //        var row = nestedHeaders.length;
            //        var col = nestedHeaders[0].length;

            //        for (var i = 0; i < row; i++)
            //            for (var j = 0; j < col; j++)
            //                delarray[i][j] = false;

            //        var h0 = $hotElement.get(0);

            //        fieldTableMergedCells.forEach((e) => {
            //            for (var i = e.row + e.rowspan; i > e.row; i--)
            //                for (var j = e.col + e.colspan; j > e.col; j--) {

            //                    var st1 = '.ht_clone_top  tr:nth-child(' + i + ') th:nth-child(' + (j + 1) + ')'

            //                    if ((i != e.row + 1) || (j != e.col + 1)) {
            //                        //h0.querySelectorAll(st1)[0].setAttribute('style', 'background : black;')
            //                        if (h0.querySelectorAll(st1)[0] != undefined) {
            //                            //h0.querySelectorAll(st1)[0].remove()
            //                            delarray[i - 1][j - 1] = true
            //                        }
            //                    } else {
            //                        if (h0.querySelectorAll(st1)[0] != undefined) {
            //                            //h0.querySelectorAll(st1)[0].setAttribute('style', 'background : red;')
            //                            h0.querySelectorAll(st1)[0].setAttribute('rowspan', e.rowspan);
            //                            h0.querySelectorAll(st1)[0].setAttribute('colspan', e.colspan);
            //                            h0.querySelectorAll(st1)[0].setAttribute('style', 'vertical-align: middle');
            //                        }
            //                    }
            //                }

            //        })

            //        for (var i = row; i > 0; i--) {
            //            for (var j = col; j > 0; j--) {

            //                var st1 = '.ht_clone_top  tr:nth-child(' + i + ') th:nth-child(' + (j + 1) + ')'
            //                if (delarray[i - 1][j - 1] && h0.querySelectorAll(st1)[0] != undefined)
            //                    //h0.querySelectorAll(st1)[0].setAttribute('style', 'background : black;')
            //                    h0.querySelectorAll(st1)[0].remove()
            //            }
            //        }
            //    }
            //});
            var commentsPlugin = that.dataFormTable.getPlugin('comments');

            //that.dataFormTable.addHook('afterValidate', (isValid, value, row, prop, source, s) => {
            //    if (!isValid) {
            //        var indexColumn = that.dataFormTable.propToCol(prop);
            //        // Manage comments programmatically:
            //        commentsPlugin.setCommentAtCell(row, indexColumn, 'Lỗi định dạng');
            //        commentsPlugin.showAtCell(row, indexColumn);
            //    }
            //});

            //Handsontable.hooks.add('beforeOnCellMouseDown',
            //    handleHotBeforeOnCellMouseDown, that.dataFormTable);

            var classCells = formCode.classCells;
            var newClassCells = [];
            var newClassCellsDetail = null;

            if (xoayObject != null) {
                $.each(classCells, function (rowIndex, val) {
                    newClassCellsDetail = [];
                    $.each(val, function (columnIndex, val) {
                        if (columnIndex != xoayObject.GiaTriIndex) {
                            if (columnIndex == xoayObject.CatalogIndex) {
                                var giaTriKey = xoayObject.GiaTriNameAscii + "!!" + xoayObject.GiaTriName;
                                for (var i = 0; i < xoayObject.CatalogCount; i++) {
                                    newClassCellsDetail.push(classCells[rowIndex][xoayObject.GiaTriIndex]);
                                }
                            }
                            else {
                                newClassCellsDetail.push(classCells[rowIndex][columnIndex]);
                            }
                        }
                    });
                    newClassCells.push(newClassCellsDetail);
                });

                if (newClassCells.length > 0)
                    classCells = newClassCells;
            }

            that.dataFormTable.updateSettings({
                cells: function (row, col) {
                    var cellProperties = {};
                    if (formCode.readOnlys) {
                        var arrayCellReadOnly = formCode.readOnlys;
                        for (var i = 0; i < arrayCellReadOnly.length; i++) {
                            if (arrayCellReadOnly[i].row == row && arrayCellReadOnly[i].col == col) {
                                cellProperties.readOnly = true;
                            }
                        }
                    }

                    if (classCells) {
                        //console.log("row:" + row + ", col:" + col + ", formCode.classCells.length:" + formCode.classCells.length);
                        if (classCells == undefined || classCells.length == 0 || classCells.length <= row )
                            return cellProperties;

                        cellProperties.className = classCells[row][col]; 
                        return cellProperties;
                    }

                    return cellProperties;
                }
            });
            if (that.isCreate || that.model.get("Note") == "") {
                that.dataFormTable.validateCells(function (a, b, c) { });
            }
            that.xoayObject = xoayObject;
        },
        //TienQD
        _importWord: function () {
            var that = this;
            require(['ImportWordView'], function (ImportWordView) {
                var importWord = new ImportWordView({ document: that, cid: that.cid });
            });
        },
        // 20191120 VuHQ REQ-5 END

        _importExcel: function () {
            var that = this;
            require(['ImportExcelView'], function (ImportExcelView) {
                var importExcel = new ImportExcelView({ document: that });
            });
        },
        // 20200203 VuHQ START import data từ file excel
        _importDataFromExcel: function (option) {
            var that = this;

            var configHandsontable = that.configHandsontable;
            var parseConfigHandsontable = JSON.parse(configHandsontable);
            var headerNested = JSON.stringify(parseConfigHandsontable.headerNested);

            var fileUpload = option.$uploadFile.get(0);
            var files = fileUpload.files;

            if (files.length == 1) {
                var importFile = files[0];
                var filename = importFile.name;
                if (filename.indexOf(".xlsx", filename - ".xlsx".length) !== -1 && filename.indexOf(".xls", filename - ".xls".length) !== -1) {
                    // Create FormData object  
                    var fileData = new FormData();
                    // Looping over all files and add it to FormData object  
                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                    }

                    fileData.append("key", $("#ddlYearReport option:selected").val());

                    // strHeaderAI
                    fileData.append("HeaderAI", option.headerAI);

                    $.ajax({
                        url: '/Form/GetDataFromExcel',
                        type: "POST",
                        contentType: false, // Not to set any content header  
                        processData: false, // Not to process data  
                        data: fileData,
                        success: function (result) {
                            var countDataForm = that.hotSettings.data.length;
                            //var countDataForm = that.dataFormTable.getData().length;
                            var countDataFormNew = JSON.parse(result.data).length;
                            var isChange = false;
                            var isConfirm = false;
                            if (countDataFormNew < countDataForm || countDataFormNew > countDataForm) {
                                isChange = true;
                            }

                            if (isChange) {
                                var r = confirm("Dữ liệu nhập vào đang bị thiếu hoặc thừa dòng bạn có muốn tiếp tục nhập file không?");
                                if (r == true) {  
                                    var data = JSON.parse(result.data);
                                    if (data.length > 0 && data[0].length > 0 && data[0][0] != undefined) {
                                        var dataObjects = [];
                                        var index = 0;
                                        _.each(data, function (item) {
                                            index = 0;
                                            var keyValue = {};
                                            _.each(that.globalColumns, function (globalColumn) {
                                                keyValue[globalColumn.data] = item[index];
                                                index++;
                                            });
                                            dataObjects.push(keyValue);
                                        });
                                        data = dataObjects;
                                    }
                                    that.dataFormTable.updateSettings({
                                        columns: that.columnsSetting,
                                        data: data
                                    });
                                    if (countDataFormNew < countDataForm) {
                                        var notifi = "Dữ liệu nhập vào thiếu " + (countDataForm - countDataFormNew) + " dòng so với dữ liệu ban đầu";
                                        egov.pubsub.publish(egov.events.status.error, notifi);
                                    }

                                    if (countDataFormNew > countDataForm) {
                                        that.dataFormTable.updateSettings({
                                            cells: function (row, col, prop) {
                                                if (row >= countDataForm) {
                                                    var cell = that.dataFormTable.getCell(row, col);   // get the cell for the row and column                                                                                                                
                                                    cell.style.backgroundColor = "#709fe4";  // set the background color
                                                }     
                                            }
                                        });
                                        var notifi = "Dữ liệu nhập vào thừa " + (countDataFormNew - countDataForm) + " dòng so với dữ liệu ban đầu";
                                        egov.pubsub.publish(egov.events.status.error, notifi);
                                    }
                                    that.dataFormTable.validateCells(function (a, b, c) { });
                                }
                            }
                            if (countDataFormNew == countDataForm) {
                                var notifi = "Đã nhập dữ liệu xong.";
                                egov.pubsub.publish(egov.events.status.success, notifi);
                                var data = JSON.parse(result.data);
                                if (data.length > 0 && data[0].length > 0 && data[0][0] != undefined) {
                                    var dataObjects = [];
                                    var index = 0;
                                    _.each(data, function (item) {
                                        index = 0;
                                        var keyValue = {};
                                        _.each(that.globalColumns, function (globalColumn) {
                                            keyValue[globalColumn.data] = item[index];
                                            index++;
                                        });
                                        dataObjects.push(keyValue);
                                    });
                                    data = dataObjects;
                                }
                                that.dataFormTable.updateSettings({
                                    columns: that.columnsSetting,
                                    data: data
                                });

                                that.dataFormTable.validateCells(function (a, b, c) { });
                            }

                        },
                        error: function (err) {
                            alert(err.statusText);
                        },
                        complete: function () {
                            localStorage.removeItem("isChangeForm");
                        }
                    });
                }
                else {
                    eGovMessage.show("Chỉ được up file xls, xlsx",
                        "", eGovMessage.messageButtons.Ok);
                }
            }
        },
        //TienQD-25/08/2020
        _importDataFromWord: function (option) {
            var that = this;

            var fileUpload = option.$uploadFile.get(0);
            var files = fileUpload.files;

            if (files.length == 1) {
                var importFile = files[0];
                var filename = importFile.name;
                if (filename.indexOf(".docx", filename - ".docx".length) !== -1 && filename.indexOf(".doc", filename - ".doc".length) !== -1) {
                    // Create FormData object  
                    var fileData = new FormData();
                    // Looping over all files and add it to FormData object  
                    for (var i = 0; i < files.length; i++) {
                        fileData.append(files[i].name, files[i]);
                    }

                    fileData.append("key", $("#ddlYearReport option:selected").val());

                    // strHeaderAI
                    fileData.append("HeaderAI", option.headerAI);



                    console.time();
                    var reader = new FileReader();
                    reader.onloadend = function (event) {
                        var arrayBuffer = reader.result;
                        // debugger

                        mammoth.convertToHtml({ arrayBuffer: arrayBuffer }).then(function (resultObject) {
                            var id_ckeditor = that.cid + "_explicit_template";
                            CKEDITOR.instances[id_ckeditor].setData(resultObject.value);
                            console.log(resultObject.value)
                        })
                        console.timeEnd();
                    };
                    reader.readAsArrayBuffer(importFile);
                    //$.ajax({
                    //    url: '/Form/GetDataToWord',
                    //    type: "POST",
                    //    contentType: false, // Not to set any content header  
                    //    processData: false, // Not to process data  
                    //    data: fileData,
                    //    success: function (result) {
                    //            var id_ckeditor = that.cid + "_explicit_template";
                    //            CKEDITOR.instances[id_ckeditor].setData(result.data);
                    //    },
                    //    error: function (err) {
                    //        alert(err.statusText);
                    //    }
                    //});
                }
                else {
                    eGovMessage.show("Chỉ được up file doc, docx",
                        "", eGovMessage.messageButtons.Ok);
                }
            }
        },
        // 20200310 START Compare data
        _decodeEntities: function (encodedString) {
            var textArea = document.createElement('textarea');
            textArea.innerHTML = encodedString;
            return textArea.value;
        },


        _showCompareData: function (e) {
            var that = this;
            var commentList = that.model.get("CommentList");
            var commentId = $(e.target).closest("img").data("commentid");
            var prevCommentId = $(e.target).closest("img").data("prev-commentid");

            if (commentList.length > 1) {
                var currentComments = commentList.filter(function (obj) {
                    return (obj.CommentId === commentId);
                });
                var currentComment = currentComments[0];

                var compareComment = null;
                if (prevCommentId == 0)
                    compareComment = commentList[commentList.length - 1];
                else {
                    var compareComments = commentList.filter(function (obj) {
                        return (obj.CommentId === prevCommentId);
                    });
                    compareComment = compareComments[0];
                }

                if (currentComments.length > 0 && currentComment.Diff != undefined && compareComment.Diff.length > 0 && compareComment.Diff != undefined) {
                    var $afterElement = document.getElementById('afterData');
                    var $beforeElement = document.getElementById('beforeData');
                    var compareSourceData = JSON.parse(compareComment.Diff).Data;
                    var currentSourceData = JSON.parse(currentComment.Diff).Data;

                    if (that.model.attributes.CategoryBusinessId == 8) {
                        //CKEDITOR.replace('beforeData', {
                        //    height: 400,
                        //    readOnly: true
                        //});
                        //CKEDITOR.instances['beforeData'].setData(compareSourceData);

                        //CKEDITOR.replace('afterData', {
                        //    height: 400,
                        //    readOnly: true
                        //});
                        //CKEDITOR.instances['afterData'].setData(currentSourceData);

                        // var dmp = new diff_match_patch();
                        // dmp.Diff_Timeout = 0;

                        // No warmup loop since it risks triggering an 'unresponsive script' dialog
                        // in client-side JavaScript

                        //var plainCompareData = that._decodeEntities(compareSourceData.replace(/<[^<|>]+?>/gi, ''));
                        //var plainCurrentData = that._decodeEntities(currentSourceData.replace(/<[^<|>]+?>/gi, ''));

                        //var d = dmp.diff_main(plainCompareData, plainCurrentData, false);
                        //var ds = dmp.diff_prettyHtml(d);

                        var ds = htmldiff(compareSourceData, currentSourceData);
                        document.getElementById('outputdiv').innerHTML = ds;
                    }
                    else {
                        $('.lblBeforeData').html("{" + compareComment.CommentId + "} " + compareComment.UserSend.fullname + " " + compareComment.Description);
                        $('.lblAfterData').html("{" + currentComment.CommentId + "} " + currentComment.UserSend.fullname + " " + currentComment.Description);
                        var _settings = that.hotSettings;
                        _settings.height = '300px';
                        _settings.cells = function (row, col, prop) {
                            var cellProperties = {};
                            cellProperties.editor = false;
                            return cellProperties;
                        };

                        setTimeout(function () {
                            _settings.data = JSON.parse(compareSourceData);
                            that.compareFormTable = new Handsontable($beforeElement, that.hotSettings);

                            _settings.data = JSON.parse(currentSourceData);
                            that.currentFormTable = new Handsontable($afterElement, that.hotSettings);

                            // class style với rule
                            // 1. current có data, compare không có ==> xanh
                            // 2. current có data, compare có ==> none
                            // 3. current không có data, compare có ==> đỏ
                            var compareData = that.compareFormTable.getData();
                            var currentData = that.currentFormTable.getData();
                            var compareClass = [];
                            var currentClass = [];

                            var rowCount4Check = currentData.length > compareData.length ? currentData.length : compareData.length;

                            for (var i = 0; i < rowCount4Check; i++) {
                                for (var j = 0; j < currentData[0].length; j++) {
                                    if (compareData[i] == undefined) {
                                        currentClass.push([i, j, 'htGreen']);
                                    }
                                    else if (currentData[i] == undefined) {
                                        currentClass.push([i, j, 'htRed']);
                                    }
                                    else {
                                        if (that.isEmptyValue(compareData[i][j]) && !that.isEmptyValue(currentData[i][j])) {
                                            currentClass.push([i, j, 'htGreen']);
                                        }
                                        else if (!that.isEmptyValue(compareData[i][j]) && that.isEmptyValue(currentData[i][j])) {
                                            currentClass.push([i, j, 'htRed']);
                                        }
                                        else if (!that.isEmptyValue(compareData[i][j]) && !that.isEmptyValue(currentData[i][j]) && compareData[i][j] != currentData[i][j]) {
                                            currentClass.push([i, j, 'htYellow']);
                                            compareClass.push([i, j, 'htYellow']);
                                        }
                                    }
                                }
                            }

                            _.each(compareClass, function (item) {
                                that.compareFormTable.setCellMeta(item[0], item[1], "className", that.compareFormTable.getCellMeta(item[0], item[1]).className + " " + item[2]);
                            });

                            _.each(currentClass, function (item) {
                                that.currentFormTable.setCellMeta(item[0], item[1], "className", that.currentFormTable.getCellMeta(item[0], item[1]).className + " " + item[2]);
                            });

                            that.compareFormTable.render();
                            that.currentFormTable.render();

                            $("#left-content").css("height", Math.max($("#beforeData .ht_master .wtHider .htCore").height(), $("#afterData .ht_master .wtHider .htCore").height()));
                            $("#scroll-left").css("height", 300);
                            $("#c2-content").css("width", Math.max($("#beforeData .ht_master .wtHider .htCore").width(), $("#afterData .ht_master .wtHider .htCore").width()));
                            $("#scroll-com").css("width", $("#beforeData").width());
                            $("#scroll-com").on("scroll", function () {
                                $("#afterData .ht_master .wtHolder").scrollLeft($(this).scrollLeft());
                                $("#beforeData .ht_master .wtHolder").scrollLeft($(this).scrollLeft());
                            });
                            $("#scroll-left").on("scroll", function () {
                                $("#afterData .ht_master .wtHolder").scrollTop($(this).scrollTop());
                                $("#beforeData .ht_master .wtHolder").scrollTop($(this).scrollTop());
                            });
                        }, 500);
                    }
                    $('#compareModal').modal('toggle');
                }
            }
        },

        renderHeaderHandson: function (config, colwidth) {
            var that = this;
            var hiddenCol = [];
            var a = _.keys(config.header);
            i = 0;
            _.each(a, function (element) {
                if (config.extra.columnSetting[element] && config.extra.columnSetting[element]["Hidden"])
                    hiddenCol.push(i);
                i++;
            });
            var header = _.map(a, function (element) {
                return element.split("!!", 1);
            });
            var widthHeader = 50;
            for (var i = 0; i < colwidth.length; i++) {
                if (_.contains(hiddenCol, i)) {
                } else {
                    widthHeader += Number(colwidth[i]);
                }
            }

            that.$el.find(".headerHandson").css({ "width": widthHeader });
            that.$el.find("#divContent").css({ "width": widthHeader });
            that.$el.find('#headHandson').html("")
            var headerSetting = config.extra.headerSetting;
            for (var i = 0; i < headerSetting.length; i++) {
                var string = "";
                for (var j = 0; j < headerSetting[i].length; j++) {
                    if (j == 0 && i == 0) {
                        string = " <td style='width: 50px' rowspan='" + headerSetting.length + "'></td>";
                    }

                    if (headerSetting[i][j] != " " && headerSetting[i][j] != "") {
                        _.each(config.headerNested, function (element) {
                            if (element.row == i && element.col == j)
                                if (i == headerSetting.length - 1 && element.colspan)
                                    for (var k = 0; k < element.colspan - 1; k++) {
                                        if (config.extra.columnSetting[a[j]]["Hidden"])
                                            string = string + "<td class='cot" + j + "' style ='width:" + colwidth[j] + "px;' hidden>" + headerSetting[i][j] + "</td >";
                                        else string = string + "<td class='cot" + j + "' style ='width:" + colwidth[j] + "px;'>" + headerSetting[i][j] + "</td >";
                                    }
                                else {
                                    if (config.extra.columnSetting[a[j]]["Hidden"])
                                        string = string + "<td class='cot" + j + "'style ='width:" + colwidth[j] + "px;' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' hidden>" + headerSetting[i][j] + "</td >";
                                    else string = string + "<td class='cot" + j + "'style ='width:" + colwidth[j] + "px;' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' >" + headerSetting[i][j] + "</td >";
                                }
                        });
                        if (i == headerSetting.length - 1)
                            if (config.extra && config.extra.columnSetting && config.extra.columnSetting[a[j]] && config.extra.columnSetting[a[j]]["Hidden"])
                                string = string + "<td class='cot" + j + "' style ='width:" + colwidth[j] + "px;' hidden>" + headerSetting[i][j] + "</td >";
                            else string = string + "<td class='cot" + j + "' style ='width:" + colwidth[j] + "px;'>" + headerSetting[i][j] + "</td >";
                    }
                }
                that.$el.find('#headHandson').append("<tr>" + string + "</tr>");
            }
        },

        isEmptyValue: function (val) {
            if (val == " " || val == undefined) {
                return true;
            }
            return false;
        },
        // 20200310 END Compare data
        // Danh sach bao cao
        _renderFormReportList: function () {
            var pivotTable;
            var form = this.model.get("DocumentContents")[0];
            if (form == undefined) return;
            var formHeader = form.FormHeader;
            var formFooter = form.FormFooter;
            var templateHeader = formHeader.match(/@@(.*?)@@/gm);
            var templateFooter = formFooter.match(/@@(.*?)@@/gm);
            var timeKey = $("#ddlYearReport option:selected").val();
            var id = $("a", $("ul#ulTabs li.active")).attr('href');
            if (templateHeader != null) {
                _.each(templateHeader, function (templateKeyCode) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        //contentType: "application/json; charset=utf-8",
                        //dataType: "json",
                        url: '/Document/GetDataTemplateKeys',
                        traditional: true,
                        data: { 'templateKeyCode': templateKeyCode, 'formId': form.FormId, 'timeKey': timeKey },
                        success: function (response) {
                            if (response.Success) {
                                if (response.Type && response.Type == 4) {
                                    formHeader = formHeader.replace(value, response.NewValue || "");
                                }
                                else if (response.Type && response.Type == 8 && response.result) {
                                    var dataTmp = {};
                                    dataTmp.data = [];
                                    _.each(response.result,
                                        function (v) {
                                            dataTmp.data.push(v);
                                        });
                                    if (dataTmp.data.length > 0) {
                                        var tmpHtml = $($.tmpl(`<div> ${response.HtmlTemplate} </div>`, dataTmp));
                                        if (tmpHtml != null && tmpHtml.length > 0)
                                            formHeader = formHeader.replace(templateKeyCode, tmpHtml.html());
                                    } else {
                                        formHeader = formHeader.replace(templateKeyCode, "");
                                    }
                                }

                            }
                        }
                    });
                });
                $(`${id} .formHeader`).html(formHeader);
            }
            if (templateFooter != null) {
                _.each(templateFooter, function (templateKeyCode) {
                    $.ajax({
                        type: "POST",
                        async: false,
                        //contentType: "application/json; charset=utf-8",
                        //dataType: "json",
                        url: '/Document/GetDataTemplateKeys',
                        traditional: true,
                        data: { 'templateKeyCode': templateKeyCode, 'formId': form.FormId, 'timeKey': timeKey },
                        success: function (response) {
                            if (response.Success) {
                                if (response.Type && response.Type == 4) {
                                    formFooter = formFooter.replace(value, response.NewValue || "");
                                }
                                else if (response.Type && response.Type == 8 && response.result) {
                                    var dataTmp = {};
                                    dataTmp.data = [];
                                    _.each(response.result,
                                        function (v) {
                                            dataTmp.data.push(v);
                                        });
                                    if (dataTmp.data.length > 0) {
                                        var tmpHtml = $($.tmpl(`<div> ${response.HtmlTemplate} </div>`, dataTmp));
                                        if (tmpHtml != null && tmpHtml.length > 0)
                                            formFooter = formFooter.replace(templateKeyCode, tmpHtml.html());
                                    }
                                    else {
                                        formFooter = formFooter.replace(templateKeyCode, "");
                                    }
                                }
                            }
                        }
                    });
                });
                $(`${id} .formFooter`).html(formFooter);
            }
            if (form.ExplicitTemplate && form.ExplicitTemplate.length > 2) {
                this.initPivot(JSON.parse(form.ExplicitTemplate), id);
            }
        },
        initPivot: function (reportId, id) {

            _.each(reportId, function (v) {
                $(`${id} #divContent`).append(`<div id='${id.replace("#", "")}_formPivot-${v}'></div>`);
                $(`${id}_formPivot-${v}`).css("margin-bottom", "20px");
                //var pivotConfig = JSON.parse("{}");
                $.ajax({
                    url: "/ReportViewer/GetReportKeyData",
                    async: false,
                    beforeSend: function () {
                    },
                    data: {
                        ReportId: v,
                        Time: "TuyChon",
                        FromDate: "2017-01-01T00:00:00",
                        ToDate: "2018-03-01T00:00:00",
                        TreeGroupValue: null,
                        TreeGroupName: null,
                        GroupId: 0,
                        sortBy: "",
                        isDesc: false,
                        Page: 1,
                        PageSize: 500
                    },
                    success: function (result) {
                        var cf = {
                            "dataSource": { data: result },
                            "options": {
                                "grid": {
                                    "showTotals": "off",
                                    "showGrandTotals": "off"
                                }
                            },
                            "tableSizes": {
                                "columns": [
                                    {
                                        "idx": 0,
                                        "width": 468
                                    }
                                ]
                            }
                        }
                        var pi = new WebDataRocks({
                            container: `${id}_formPivot-${v}`,
                            toolbar: false,
                            global: {
                                // replace this path with the path to your own translated file
                                //localization: "http://smartnation.bkav.com/Scripts/bkav.egov/libs/webdatarocks-1.2.0/en.json"
                            },
                            report: cf
                        });
                    }
                });
            });


        },
        _decodeBase64: function (s) {
            var e = {}, i, b = 0, c, x, l = 0, a, r = '', w = String.fromCharCode, L = s.length;
            var A = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            for (i = 0; i < 64; i++) { e[A.charAt(i)] = i; }
            for (x = 0; x < L; x++) {
                c = e[s.charAt(x)]; b = (b << 6) + c; l += 6;
                while (l >= 8) { ((a = (b >>> (l -= 8)) & 0xff) || (x < (L - 2))) && (r += w(a)); }
            }
            return r;
        },
        // 20200211 VuHQ Phase 2 - REQ-0 START
        _renderFormExplicit: function () {
            var form = this.model.get("DocumentContents")[0];
            if (form == undefined) return;

            var that = this;

            var explicitTemplate = form.ExplicitTemplate;
            that.FormId = form.FormId;

            if (!that.isCreate) {
                explicitTemplate = that.model.get("DocumentContents")[0].Content;
            }
            else {
                // get thoi gian bao cao theo filter
                var timeKey = that.$el.find("#ddlYearReport option:selected").val();
                // neu bao cao theo ngay
                if (that.findKyBaoCao(that.model.get("DocTypeId")) != 6) {
                    const abElements = that.$el.find('.timeKey').filter(':not(.hidden)');

                    _.each(abElements, function (item) {
                        const zeroPad = (num, places) => String(num).padStart(places, '0');
                        // nếu là monthkey thì phải + 1 với value, 2,3 la quy voi 6thang thi du nguyen
                        // thang voi tuan neu < 10 se them so 0
                        timeKey += that.findKyBaoCao(that.model.get("DocTypeId")) == 2 || that.findKyBaoCao(that.model.get("DocTypeId")) == 3 ? item.value : zeroPad((that.findKyBaoCao(that.model.get("DocTypeId")) == 4 ? parseInt(item.value) + 1 : item.value), 2);
                    });
                } else {
                    timeKey = that.formatDateTimeKey(that._getDate("DatePublished"));
                }

                var templateKeyCodes = explicitTemplate.match(/@@(.*?)@@/gm) ? explicitTemplate.match(/@@(.*?)@@/gm) : [];
                templateKeyCodes = templateKeyCodes || [];
                _.each(explicitTemplate.match(/##(.*?)##/gm),
                    function (v) {
                        templateKeyCodes.push(v);
                    });
                //var type = 8;
                if (templateKeyCodes.length > 0) {
                    //if (type === 8) {
                    _.each(templateKeyCodes, function (templateKeyCode) {
                        $.ajax({
                            type: "POST",
                            async: false,
                            //contentType: "application/json; charset=utf-8",
                            //dataType: "json",
                            url: '/Document/GetDataTemplateKeys',
                            traditional: true,
                            data: { 'templateKeyCode': templateKeyCode, 'formId': that.FormId, 'timeKey': timeKey },
                            success: function (response) {
                                if (response.Success) {
                                    if (response.Type && (response.Type == 4 || response.Type == "default")) {
                                        explicitTemplate =
                                            explicitTemplate.replace(templateKeyCode, response.NewValue || "");
                                    } else if (response.Type && response.Type == 8 && response.result) {
                                        var dataTmp = {};
                                        dataTmp.data = [];
                                        _.each(response.result,
                                            function (v) {
                                                dataTmp.data.push(v);
                                            });
                                        if (dataTmp.data.length > 0) {
                                            var tmpHtml = $($.tmpl('<div>' + response.HtmlTemplate + '</div>',
                                                dataTmp));
                                            if (tmpHtml != null && tmpHtml.length > 0)
                                                explicitTemplate =
                                                    explicitTemplate.replace(templateKeyCode, that._decodeEntities(tmpHtml.html()));
                                        } else
                                            explicitTemplate = explicitTemplate.replace(templateKeyCode, "");
                                    }
                                }
                                else {
                                    explicitTemplate = explicitTemplate.replace(templateKeyCode, "");
                                }
                            }
                        });
                    });
                }
            }

            var div = that.$('#divContent')[0];
            var input = document.createElement("textarea");

            that.$toolbar.find('#btnKey').attr('data-id', that.cid);

            input.style = "width: 100%";
            input.id = that.cid + "_explicit_template";
            input.rows = "800";
            div.appendChild(input);
            CKEDITOR.editorConfig = function (config) {
                config.language = 'vi-vn';
                config.uiColor = '#F7B42C';
                config.height = 200;
                config.toolbarCanCollapse = true;
                config.removePlugins = 'image,forms';
                config.extraPlugins = 'showborders, ruler, highcharts';
            };
            CKEDITOR.replace(that.cid + '_explicit_template', {
                height: 400,
                allowedContent: true,
                extraPlugins: 'ruler, showborders, highcharts'
            });
            CKEDITOR.config.ruler = {
                values: 21,     // segment number of the ruler
                step: 0.25,     // accuracy of sliders
                sliders: {
                    left: 2,    // left slider value
                    right: 19   // right slider value (21-19 = 2)
                },
                padding: {
                    top: 20,    // top 'canvas' padding (px)
                    bottom: 20  // bottom 'canvas' padding (px)
                }
            };
            CKEDITOR.instances[that.cid + '_explicit_template'].setData(explicitTemplate);

            $(that.el + ' .document-navigation').show();
            $(that.el + ' .toggle-navigation').css('display', 'flex');
            $(that.el + ' #divContent').css('min-width', 'unset');
            CKEDITOR.instances[that.cid + '_explicit_template'].on('instanceReady', function (ev) {
                var nav = buildNav();
                nav.appendTo($(that.el + ' .document-navigation-content'));
                ev.editor.dataProcessor.htmlFilter.addRules({
                    elements: {
                        p: function (e) {
                            var fontFamily = 'font-family:Times New Roman,Times,serif';
                            if (e.attributes.style) {
                                if (e.attributes.style.indexOf('font-family') < 0) e.attributes.style += ';' + fontFamily;
                                else e.attributes.style = e.attributes.style.replace(/font-family:[^;]*(;?)/, `${fontFamily}$1`);
                            } else e.attributes.style = fontFamily;
                        }
                    }
                });
            });


            CKEDITOR.instances[that.cid + '_explicit_template'].on('change', function () {
                var nav = buildNav();
                nav.appendTo($(that.el + ' .document-navigation-content').empty());
            });

            $(that.el + ' .toggle-navigation-btn').on('click', function (e) {
                if ($(that.el + ' .toggle-navigation').hasClass('show-nav')) {
                    $(that.el + ' .toggle-navigation').removeClass('show-nav').addClass('hide-nav');
                    $(that.el + ' .document-navigation').css('max-width', 0);
                    setTimeout(function () { $(that.el + ' .document-navigation').hide(); }, 500);
                } else {
                    $(that.el + ' .toggle-navigation').removeClass('hide-nav').addClass('show-nav');
                    $(that.el + ' .document-navigation').show();
                    setTimeout(function () { $(that.el + ' .document-navigation').css('max-width', '100%'); }, 0);
                }
            });

            function buildNav() {
                var element = $('<div>'),
                    container = $('<ol>');
                container.appendTo(element);

                var editor = CKEDITOR.instances[that.cid + '_explicit_template'];
                var headings = editor.editable().find('h1,h2,h3,h4,h5,h6'),
                    parentLevel = 1,
                    length = headings.count();

                //get each heading
                for (var i = 0; i < length; ++i) {

                    var currentHeading = headings.getItem(i),
                        text = currentHeading.getText(),
                        newLevel = parseInt(currentHeading.getName().substr(1, 1));
                    var diff = (newLevel - parentLevel);

                    //set the start level incase it is not h1
                    if (i === 0) { diff = 0; parentLevel = newLevel; }

                    //we need a new ul if the new level has a higher number than its parents number

                    if (diff > 0) {
                        var containerLiNode = container.children().last();


                        var olNode = $('<ol>');
                        olNode.appendTo(containerLiNode);
                        container = olNode;
                        parentLevel = newLevel;
                    }


                    //we need to get a previous ul if the new level has a lower number than its parents number
                    if (diff < 0) {
                        while (0 !== diff++) {
                            parent = container.parent().parent();
                            var tagName = parent.prop('tagName');
                            container = (tagName != null && tagName.toLowerCase() === 'ol' ? parent : container);
                        }
                        parentLevel = newLevel;
                    }

                    //we can add the list item if there is no difference

                    //if(text === ''){text = 'empty'}


                    if (text == null || text.trim() === '') {
                        text = '&nbsp;'
                    }

                    var aNode = $('<a href="#" data-index="' + i + '">' + text + '</a>');
                    aNode.on('click', function (e) {
                        e.preventDefault();
                        var index = this.dataset.index;
                        currentHeading = headings.getItem(index);
                        currentHeading.scrollIntoView();

                        var editor = CKEDITOR.instances[that.cid + '_explicit_template'];
                        var selection = editor.getSelection();
                        selection.selectElement(currentHeading);
                        var range = selection.getRanges()[0];

                        var newRange = new CKEDITOR.dom.range(range.document);
                        newRange.moveToPosition(currentHeading, CKEDITOR.POSITION_BEFORE_START);
                        editor.focus();
                    });
                    var liNode = $('<li>');
                    aNode.appendTo(liNode);
                    liNode.appendTo(container);
                }

                return element;
            }
        },
        // 20200211 VuHQ Phase 2 - REQ-0 END

        // Handsontable
        addRow_HT: function () {
            // 20191120 VuHQ REQ - 5 START
            var that = this;
            var row = that.dataFormTable.countRows();

            // 20200225 VuHQ START Fil dữ liệu default
            var data = that.dataFormTable.getData();
            var form = that.model.get("DocumentContents")[0];
            var rowData = [];
            var defineConfigJson = JSON.parse(form.DefineConfigJson);
            var defineFieldJson = JSON.parse(form.DefineFieldJson);
            var fieldRowsCount = defineFieldJson.data.length;

            var prop = { readOnly: false, className: 'htLeft', source: '', type: 'text' }

            that.dataFormTable.alter('insert_row', row, 1);

            for (var j = 0; j < that.dataFormTable.countCols() ; j++) {
                prop.source = that.dataFormTable.getCellMeta(row - 1, j).source;
                prop.type = that.dataFormTable.getCellMeta(row - 1, j).type;

                that.dataFormTable.setDataAtCell(row, j, defineConfigJson.data[j][fieldRowsCount + 2].toString());

                that.dataFormTable.setCellMetaObject(row, j, prop);
            }

            that.dataFormTable.render();
        },
        showLeaf: function () {
            var that = this;
            that._viewLeaf();
        },
        _viewLeaf: function (docId) {
            var that = this;
            var viewLeaf = $("#viewLeaf");

            var documentId = this.model.get("DocumentId");
            if (docId) {
                documentId = docId;
            }

            require(['TreeView'], function (TreeView) {
                console.log("doc2" + documentId);
                var treeview = new TreeView({ DocumentId: documentId, document: that });
            });
        },

        showUploadImage: function () {
            var that = this;
            require(['ShowUploadImage'], function (ShowUploadImage) {
                var showUploadImage = new ShowUploadImage({ document: that });
            });
        },
        merge_HT: function () {
            var that = this;
            var doc = that.dataFormTable.getSelected();
        },
        exportCSV_HT: function () {

        },
        viewBCTH_HT: function () {
            var that = this;
            egov.views.home.tab.addReportBCTH();
        },
        changeLop: function () {
            var that = this;
            var maLop = that.$el.find("#slLop").val();
            localStorage.removeItem("linkSurvey");
            var link = 'https://eform.hcm.edu.vn:8443/webapi/departmentapi/GetsAllStudents?maLop=' + maLop;
            var doctype = egov.setting.allDoctypes.find(function (item) {
                return item.DocTypeId == that.model.get("DocTypeId")
            });

            $.ajax({
                url: "/Admin/Doctype/GetSurveyConfig",
                type: "GET",
                data: {
                    doctypeId: doctype.DocTypeId
                },
                success: function (res) {
                    var surveyConfig = JSON.parse(res.SurveyConfig);
                    surveyConfig.pages.forEach(function (_val) {
                        _val.elements.forEach(
                        function (val) {
                            if (val.type == "radiogroup") {
                                val["choicesByUrl"] = {
                                    url: link,
                                    valueName: "Name"
                                }
                            }
                        })
                    })
                    var _link = JSON.stringify(surveyConfig);
                    localStorage.setItem("linkSurvey", _link);
                    $.ajax({
                        url: "/Admin/Doctype/Update",
                        type: "POST",
                        dataType: 'json',
                        data: {
                            doctypeId: doctype.DocTypeId,
                            Link: _link
                        },
                        success: function (res) {
                            egov.pubsub.publish(egov.events.status.success, "Đã thay đổi lớp học");
                        },
                        complete: function () {
                            var cid = "";
                            if (localStorage.getItem("cid") !== null) {
                                cid = localStorage.getItem("cid");
                            }

                            var idTab1 = '#' + cid + '4a';
                            $('a[href="' + idTab1 + '"]').click();
                        }
                    })

                },
                complete: function () {

                }
            })
        },
        changeTruong: function () {
            var that = this;
            var maTruong = that.$el.find("#slTruong").val();
            var selectLop = that.$("#slLop");
            $.ajax({
                url: 'https://eform.hcm.edu.vn:8443/webapi/departmentapi/GetsAllClass',
                type: "GET",
                dataType: 'json',
                data: {
                    MaTruong: maTruong
                },
                async: false,
                success: function (res) {
                    res.forEach(function (value) {
                        var subOption = '<option value="' + value.IdCard + '">' + value.Name + '</option> ';
                        selectLop.append(subOption);
                    })
                },
                complete: function () {
                    $('.js-example-basic-single2').select2();
                    selectLop.css("display", "inline-block");
                }
            })
        },
        changePeriod: function () {
            var that = this;
            if (this.model.attributes.CategoryBusinessId == 64) {
                that.buildQuery();
                return;
            }
            var _listOption = that.$el.find("#datebykybaocao option");
            for (var i = 0; i < _listOption.length; i++) {
                _listOption[i].disabled = false;
            }
            var organizeKey = that.$el.find("#InOutPlace").val();
            var timeKey = '';
            var docTypeId = that.model.get("DocTypeId");
            var actionLevel = that.model.get("ActionLevel");
            var categoryBusinessId = that.model.get("CategoryBusinessId");
            var year = that.$el.find("#ddlYearReport option:selected").val();
            if (actionLevel == 5) {
                var _year = parseInt(year);
                var week = parseInt(that.$el.find("#bctuan option:selected").val());

                var date1 = that._formatDate(that.firstDayOfWeek(year, week));
                var date2 = that._formatDate(that.addDays(that.firstDayOfWeek(year, week), 6).format("yyyy-MM-dd"));
                that.$el.find("#week_date").text("Từ " + date1 + " đến " + date2);
                that.$el.find("#week_date").css("display", "block");
            }

            //5 nam
            var nestedHeaders = [];
            var changeHeader = 0;
            if (actionLevel == 1 && (categoryBusinessId == 4 || categoryBusinessId == 64)) {
                var form = this.model.get("DocumentContents")[0];
                if (form == undefined) return;

                var formCode = that.model.get("ProcessInfo");
                if (that.isCreate) {
                    formCode = form.FormCode;
                }
                // var formCode = form.FormCode;
                that.configHandsontable = formCode;
                formCode = JSON.parse(formCode);
                nestedHeaders = formCode.extra.headerSetting;
                var countYear = 5;
                var dem = 0;
                var index = 0;
                nestedHeaders[0].forEach(function (key) {
                    if (key.charAt(0) == '$') {
                        var yearPlus = "Năm " + (year - countYear + dem);
                        nestedHeaders[0][index] = yearPlus;
                        changeHeader++;
                        dem++;
                    }
                    index++;
                })
            } else if ((categoryBusinessId == 4 || categoryBusinessId == 64) && actionLevel != 1) {
                var form = this.model.get("DocumentContents")[0];
                if (form == undefined) return;

                var formCode = that.model.get("ProcessInfo");
                if (that.isCreate) {
                    formCode = form.FormCode;
                }
                // var formCode = form.FormCode;
                that.configHandsontable = formCode;
                formCode = JSON.parse(formCode);
                nestedHeaders = formCode.extra.headerSetting;
            }
            //end 5 nam
            if (egov.setting.transfer.iscreatedform == true) {
                switch (actionLevel) {
                    case 1:
                        timeKey = year;
                        year = "2";
                        break;
                    case 2:
                        var nuanam = that.$el.find("#bcnuanam").val();
                        timeKey = year + "" + nuanam;
                        break;
                    case 3:
                        var quy = that.$el.find("#bcquy").val();
                        timeKey = year + "" + quy;
                        break;
                    case 4:
                        var month = that.$el.find("#bcthang").val();
                        var monthKey = Number(month) + 1;
                        if (monthKey < 10)
                            monthKey = '0' + monthKey;
                        timeKey = year + "" + monthKey;
                        break;
                    default:
                        var month_9 = that.$el.find("#bc9thang").val();
                        var monthKey = Number(month_9);
                        if (monthKey < 10)
                            monthKey = '0' + monthKey;
                        timeKey = year + "" + monthKey;
                        break;
                }
                var organizationCode = "";


                //get ra organizationCode
                egov.dataManager.getDepartmentsCurrent({
                    success: function (currentDepts) {
                        var currentInOutPlace = that.model.get('InOutPlace');
                        organizationCode = currentDepts[0].EdocId;
                    }
                });
                //get ra organizationCode

                //get list document to organizationCode, docTypeId, timekey
                $.ajax({
                    url: "/Document/GetListToDoctype",
                    type: "GET",
                    async: false,
                    data: {
                        doctypeId: docTypeId,
                        organizationCode: organizationCode,
                        timeKey: timeKey,
                        year: year
                    },
                    success: function (res) {
                        if (res != null) {
                            var year = that.$el.find("#ddlYearReport option:selected").val();
                            switch (actionLevel) {
                                case 1:
                                    var year = that.$el.find("#ddlYearReport option");
                                    for (var i = 0; i < year.length; i++) {
                                        for (var j = 0; j < res.length; j++) {
                                            if (year[i].value == res[j].timekey) {
                                                year[i].disabled = true;
                                            }
                                        }
                                    }
                                    break;
                                case 2:
                                    var nuanam = that.$el.find("#bcnuanam option");
                                    for (var i = 0; i < nuanam.length; i++) {
                                        for (var j = 0; j < res.length; j++) {
                                            if ((year + "" + nuanam[i].value) == res[j].timekey) {
                                                nuanam[i].disabled = true;
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    var quy = that.$el.find("#bcquy option");
                                    for (var i = 0; i < quy.length; i++) {
                                        for (var j = 0; j < res.length; j++) {
                                            if ((year + "" + quy[i].value) == res[j].timekey) {
                                                quy[i].disabled = true;
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    var month = that.$el.find("#bcthang option");
                                    for (var i = 0; i < month.length; i++) {
                                        for (var j = 0; j < res.length; j++) {
                                            var monthKey = Number(month[i].value) + 1;
                                            if (monthKey < 10)
                                                monthKey = '0' + monthKey;
                                            var _month = year + "" + monthKey;
                                            if (_month == res[j].timekey) {
                                                month[i].disabled = true;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    var month_9 = that.$el.find("#bc9thang option");
                                    for (var i = 0; i < month_9.length; i++) {
                                        for (var j = 0; j < res.length; j++) {
                                            var monthKey = Number(month_9[i].value) + 1;
                                            if (monthKey < 10)
                                                monthKey = '0' + monthKey;
                                            var _month_9 = year + "" + monthKey;
                                            if (_month_9 == res[j].timekey) {
                                                month_9[i].disabled = true;
                                            }
                                        }
                                    }
                                    break;
                            }
                            console.log(JSON.stringify(res));
                        } else {
                            var _listOption = that.$el.find("#datebykybaocao option");
                            for (var i = 0; i < _listOption.length; i++) {
                                _listOption[i].disabled = false;
                            }
                            console.log("Khong co du lieu");
                        }
                    }
                });
                //end get list document to organizationCode, docTypeId, timekey
            }

            if (actionLevel == 6) {
                // trường hợp changeperiod ngày #bcngay
                timeKey = that.$el.find(".bcngayplus").first().val().replace(/(\d{2})\/(\d{2})\/(\d{4})/, '$3$2$1');;
            }
            else {
                timeKey = that.$el.find("#ddlYearReport option:selected").val();

                const abElements = that.$el.find('.timeKey').filter(':not(.hidden)');

                _.each(abElements, function (item) {
                    const zeroPad = (num, places) => String(num).padStart(places, '0');

                    // nếu là monthkey thì phải + 1 với value
                    timeKey += that.findKyBaoCao(that.model.get("DocTypeId")) == 2 || that.findKyBaoCao(that.model.get("DocTypeId")) == 3 ? item.value : zeroPad((that.findKyBaoCao(that.model.get("DocTypeId")) == 4 ? parseInt(item.value) + 1 : item.value), 2);
                });
            }

            if (that.model.attributes.CategoryBusinessId == 8) {
                var form = this.model.get("DocumentContents")[0];
                if (form == undefined) return;
                var explicitTemplate = form.ExplicitTemplate;
                var templateKeyCodes = explicitTemplate.match(/@@(.*?)@@/gm) ? explicitTemplate.match(/@@(.*?)@@/gm) : [];
                templateKeyCodes = templateKeyCodes || [];
                _.each(explicitTemplate.match(/##(.*?)##/gm),
                    function (v) {
                        templateKeyCodes.push(v);
                    });
                if (templateKeyCodes.length > 0) {
                    _.each(templateKeyCodes, function (templateKeyCode) {
                        $.ajax({
                            type: "POST",
                            async: false,
                            url: '/Document/GetDataTemplateKeys',
                            data: { 'templateKeyCode': templateKeyCode, 'formId': that.FormId, 'timeKey': timeKey },
                            success: function (response) {
                                if (response.Success) {
                                    if (response.Type && (response.Type == 4 || response.Type == "default")) {
                                        explicitTemplate = explicitTemplate.replace(templateKeyCode, response.NewValue || "");
                                    }
                                    else if (response.Type && response.Type == 8 && response.result) {
                                        var dataTmp = {};
                                        dataTmp.data = [];
                                        _.each(response.result,
                                            function (v) {
                                                dataTmp.data.push(v);
                                            });
                                        var tmpHtml = $($.tmpl('<div>' + response.HtmlTemplate + '</div>', dataTmp));
                                        if (tmpHtml != null && tmpHtml.length > 0)
                                            explicitTemplate = explicitTemplate.replace(templateKeyCode, that._decodeEntities(tmpHtml.html()));
                                    }

                                }
                                else {
                                    explicitTemplate = explicitTemplate.replace(templateKeyCode, "");
                                }
                            }
                        });
                    });
                    CKEDITOR.instances[that.cid + '_explicit_template'].setData(explicitTemplate, function () {
                        this.checkDirty();
                    });
                }
            } else {
                if (that.hasCompilation)
                    $.ajax({
                        url: "/Document/GetCompilationData",
                        data: {
                            docTypeId: docTypeId,
                            timeKey: timeKey,
                            organizeKey: organizeKey
                        },
                        type: "GET",
                        success: function (data) {
                            if (data.success) {
                                var form = that.model.get("DocumentContents")[0];
                                // 20200213 VuHQ Phase 2 - REQ-0 Báo cáo chỉ tiêu START
                                if (form.Compilation != null) {
                                    // var dataObject = that.dataFormTable.getSourceData();
                                    var dataObject = null;

                                    var form = that.model.get("DocumentContents")[0];
                                    var formCode = form.FormCode;

                                    formCode = JSON.parse(formCode);
                                    var indexCkb = [];
                                    Object.keys(formCode.header).forEach(function (val, index) {
                                        if (formCode.header[val] == "bit") {
                                            indexCkb.push(index)
                                        }
                                    })
                                    formCode.data.forEach(function (values, index) {
                                        Object.keys(values).forEach(function (_value, _index) {
                                            if (indexCkb.indexOf(_index) !== -1) {
                                                values[_value] = "false"
                                            }
                                        })
                                    });
                                    dataObject = formCode.data;

                                    var compilation = JSON.parse(form.Compilation);
                                    var dataObjectCompilation = JSON.parse(data.data);
                                    var codeObjectCompilation = undefined;
                                    var dataObjectMatchOff = [];
                                    if (that.model.get("DocumentContents")[0].FormCategoryId == 3) {
                                        codeObjectCompilation = JSON.parse(compilation.code).summaryConfigJsonForm;
                                        if (dataObjectCompilation != undefined && codeObjectCompilation != undefined) {
                                            if (codeObjectCompilation.schema.Form_Compilation_Match_Off != undefined && codeObjectCompilation.schema.Form_Compilation_Match_Off.default != 1) {
                                                dataObjectCompilation.forEach(function (dataObjectCompilationDetail, indexCompilation) {
                                                    var keyFields = Object.keys(formCode.header);
                                                    var schemaCompilations = JSON.parse(JSON.stringify(codeObjectCompilation.schema));
                                                    // Bỏ 4 key đầu : sql, match_off, target_match, display_match
                                                    var indexNotIncludes = ['Form_Compilation_Display_Match', 'Form_Compilation_Match_Off', 'Form_Compilation_Target_Match', 'Form_sql'];
                                                    const filteredSchemaCompilations = Object.keys(schemaCompilations)
                                                        .filter(key => !indexNotIncludes.includes(key))
                                                        .reduce((obj, key) => {
                                                            obj[key] = schemaCompilations[key];
                                                            return obj;
                                                        }, {});
                                                    var itemDetail = {};
                                                    for (var i = 0; i < keyFields.length; i++) {
                                                        const keyMatchs = Object.keys(schemaCompilations)
                                                            .filter(key => key == "Form_" + keyFields[i].split('!!')[0]);
                                                        if (keyMatchs.length > 0) {
                                                            itemDetail[keyFields[i].split('!!')[0]] = dataObjectCompilationDetail[schemaCompilations[keyMatchs[0]].default];
                                                        }
                                                        else
                                                            itemDetail[keyFields[i].split('!!')[0]] = '';
                                                    }
                                                    dataObjectMatchOff.push(itemDetail);
                                                });
                                            }
                                            else {
                                                dataObject.forEach(function (dataObjectDetail, index) {
                                                    if (dataObjectCompilation.length > 0) {
                                                        dataObjectCompilation.forEach(function (dataObjectCompilationDetail, indexCompilation) {
                                                            if (codeObjectCompilation.schema.Form_Compilation_Match_Off == undefined) {
                                                                if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] != ""
                                                                    && dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default.trim()] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Target_Match.default.trim()]) {
                                                                    var keyFields = Object.keys(codeObjectCompilation.schema);
                                                                    for (var i = 3; i < keyFields.length; i++) {
                                                                        var keyField = keyFields[i].replace("Form_", "");
                                                                        if (codeObjectCompilation.schema[keyFields[i]].default != "") {
                                                                            var value = dataObjectCompilationDetail[codeObjectCompilation.schema[keyFields[i]].default];
                                                                            dataObjectDetail[keyField] = value;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else if (codeObjectCompilation.schema.Form_Compilation_Match_Off.default == 1) {
                                                                if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default] != "" &&
                                                                    dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display_Match.default.trim()] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Target_Match.default.trim()]) {
                                                                    var keyFields = Object.keys(codeObjectCompilation.schema);
                                                                    for (var i = 4; i < keyFields.length; i++) {
                                                                        var keyField = keyFields[i].replace("Form_", "");
                                                                        if (codeObjectCompilation.schema[keyFields[i]].default != "") {
                                                                            var value = dataObjectCompilationDetail[codeObjectCompilation.schema[keyFields[i]].default];
                                                                            dataObjectDetail[keyField] = value;
                                                                        }
                                                                    }
                                                                }
                                                            } else {
                                                                // nothing
                                                            }

                                                        });
                                                    } else {
                                                        var keyFields = Object.keys(codeObjectCompilation.schema);
                                                        for (var i = 3; i < keyFields.length; i++) {
                                                            var keyField = keyFields[i].replace("Form_", "");
                                                            if (codeObjectCompilation.schema[keyFields[i]].default != "" && dataObjectDetail[keyField] != undefined && !dataObjectDetail[keyField].toString().startWith('=')) {
                                                                dataObjectDetail[keyField] = 0;
                                                            }
                                                        }
                                                    }
                                                });
                                            }
                                        }
                                    } else if (that.model.get("DocumentContents")[0].FormCategoryId == 2) {
                                        codeObjectCompilation = JSON.parse(compilation.code).targetConfigJsonForm;
                                        if (dataObjectCompilation != undefined && codeObjectCompilation != undefined) {
                                            dataObject.forEach(function (dataObjectDetail, index) {
                                                if (dataObjectCompilation.length > 0) {
                                                    dataObjectCompilation.forEach(function (dataObjectCompilationDetail, indexCompilation) {
                                                        if (dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Match.default] != undefined && dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Match.default] == dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Match.default]) {
                                                            dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display.default] = dataObjectCompilationDetail[codeObjectCompilation.schema.Form_Compilation_Select.default];
                                                        }
                                                    });
                                                } else {
                                                    dataObjectDetail[codeObjectCompilation.schema.Form_Compilation_Display.default] = 0;
                                                }
                                            });
                                        }
                                    }

                                    if (dataObjectMatchOff.length > 0) {
                                        dataObject = dataObjectMatchOff;
                                    }

                                    //excel with mysql
                                    var dataNew = [];
                                    if (form.FormCode === undefined) {
                                        dataNew = dataObject;
                                    } else {
                                        var dataNormal = JSON.parse(form.FormCode).data;
                                        var countExcel = 0;

                                        dataNormal.forEach((nor, index) => {
                                            var count = 0;
                                            var dataa = dataObject[index];
                                            var objnew = {};
                                            Object.entries(nor).forEach(function (key, index) {
                                                var keynor = key[0];
                                                var valnor = key[1];
                                                var indexnor = index;
                                                if (valnor && valnor != "" && valnor.length > 1 && valnor.charAt(0) == "=") {
                                                    if (valnor.charAt(0) == "=") {
                                                        countExcel++;
                                                    }
                                                }
                                            });
                                        });
                                        if (countExcel > 0) {
                                            dataNormal.forEach((nor, index) => {
                                                var count = 0;
                                                var dataa = dataObject[index];
                                                var objnew = {};
                                                Object.entries(nor).forEach(function (key, index) {
                                                    var keynor = key[0];
                                                    var valnor = key[1];
                                                    var indexnor = index;
                                                    if (valnor && valnor != "" && valnor.length > 1 && valnor.charAt(0) == "=") {
                                                        if (valnor.charAt(0) == "=") {
                                                            objnew[keynor] = valnor;
                                                        } else {
                                                            objnew[keynor] = dataa[keynor];
                                                        }
                                                    } else {
                                                        objnew[keynor] = dataa[keynor];
                                                    }
                                                });
                                                dataNew.push(objnew);
                                            });
                                        } else {
                                            dataNew = dataObject;
                                        }
                                    }
                                    //end excel with mysql

                                    //handle checkbox
                                    if (form.DefineConfigJson != undefined) {
                                        var countCheckbox = 0;
                                        var arrCheckbox = [];
                                        var dataConfig = JSON.parse(form.DefineConfigJson).data;
                                        for (var cb = 0; cb < dataConfig.length; cb++) {
                                            dataConfig[cb].forEach(function (cbVal) {
                                                if (cbVal != "" && cbVal != null && cbVal == "Checkbox" && dataConfig[cb][0] != null) {
                                                    countCheckbox++;
                                                    var dataConvert = that.removeVietnameseTones(dataConfig[cb][0]);
                                                    arrCheckbox.push(dataConvert);
                                                }
                                            })
                                        }
                                        if (countCheckbox > 0) {
                                            dataNew.forEach(function (val) {
                                                for (var arrcb = 0; arrcb < arrCheckbox.length; arrcb++) {
                                                    var colData = val[arrCheckbox[arrcb]];
                                                    if (colData == "true" || colData == 1) {
                                                        val[arrCheckbox[arrcb]] = "true"
                                                    } else{
                                                        val[arrCheckbox[arrcb]] = "false"
                                                    }
                                                }
                                            })
                                        }
                                    }
                                    //end handle checkbox

                                    $.each(dataNew, function (index, value) {
                                        $.each(value, function (key, val) {
                                            if (val === undefined) {
                                                dataNew[index][key] = null;
                                            }
                                        })
                                    })
                                    if (changeHeader > 0) {
                                        that.dataFormTable.updateSettings({
                                            nestedHeaders: nestedHeaders,
                                            data: dataNew,
                                            formulas: true
                                        });
                                    } else {
                                        that.dataFormTable.updateSettings({
                                            data: dataNew,
                                            formulas: true
                                        });
                                    }

                                    //that.dataFormTable.validateCells(function (a, b, c) { });
                                }
                                // 20200213 VuHQ Phase 2 - REQ-0 Báo cáo chỉ tiêu END
                            }
                        }
                    });
            }
            that.renderWarningCompilation();
        },

        _renderAttachment: function () {
            /// <summary>
            /// Hiển thị các file đính kèm của document
            /// </summary>
            var that = this;
            if (this.isCreate || this.isOnlineRegistration) {
                this.$('.attachment-download-all').hide();
            }
            require(['docAttachment'], function (Attachment) {
                var attacts = new egov.models.attachmentCollection(that.model.get('Attachments'));
                that.attachments = new Attachment({
                    hasPermission: that.isCreate || that.model.get("UserCurrentId") == egov.userId || that.hasUpdatePermission,// Chỉ có quyền remove, phục hồi khi là người đang giữ công văn
                    model: attacts,
                    el: that.$('#wrapAttachment'),
                    documentId: that.model.get("DocumentId"),
                    documentCopyId: that.model.get("DocumentCopyId"),
                    storePrivateId: that.model == undefined ? null : that.model.get("StorePrivateId")
                });
            });

        },

        _downloadAllAttachment: function (e) {
            if (!e || this.isCreate || this.isOnlineRegistration) {
                return;
            }
            egov.helper.destroyClickEvent(e);
            if (this.attachments) {
                this.attachments.downloadAllAttachment();
            }
        },

        _renderSupplementary: function () {
            /// <summary>
            /// Hiển thị danh sách yêu cầu bổ sung
            /// </summary>
            var that = this;
            require(['supplementaryList'], function (supplementaryList) {
                that.supplementaryList = new supplementaryList({
                    supplementary: that.model.get('SupplementaryModel'),
                    document: that,
                    el: that.$('#wrapSupplementary')
                });
            });
        },

        _renderRelation: function () {
            /// <summary>
            /// Hiển thị các văn bản liên quan của document
            /// </summary>
            var that = this;
            require(['docRelation'], function (Relation) {
                var relations = new egov.models.relationList(that.model.get('RelationModels'));
                that.model.set('Relations', relations);
                var hasRemovePermission = that.isCreate || (that.model.get("UserCurrentId") === egov.userId && that.model.get("Status") === 2);
                that.relations = new Relation({
                    document: that.model,
                    hasPermission: hasRemovePermission,
                    model: relations,
                    el: that.$('#wrapDocumentRelation')
                });
            });
        },

        _approve: function (approve) {
            /// <summary>
            /// Ký duyệt
            /// </summary>
            /// <param name="approve">Trạng thái yes or no</param>
            var approveText = approve ? egov.resources.document.hsmc.resultOk : egov.resources.document.hsmc.resultDeny;
            this.model.set({
                IsSuccess: approve ? true : false
            })
        },

        _removeApprove: function (e) {
            this.$(".documentResult").text(egov.resources.document.hsmc.noResult);
            this.$(".documentResult").removeClass("yes no");
            this.$("input[name='IsSuccess']").val(null);
            this.$('.removeApprove').hide();
        },

        _changeDocumentFormView: function (e) {
            this.hasMinTemplate = !this.hasMinTemplate;
            this.$("#btnToggleForm").find("span").last().text(this.hasMinTemplate ? "Đầy đủ" : "Thu gọn");

            var currentTemplate = this.hasMinTemplate ? this.minTemplate : this.fullTemplate;

            egov.formtemplate.view.load(this.$('.document-info-template'), currentTemplate);

            this._parseDateControl();
            return;
        },

        _renderTemplateByConfig: function () {
            /// <summary>Hiển thị form văn bản theo cấu hình giao diện</summary>
            var that = this;
            this.getDocumentTemplate();

            this.$("#btnToggleForm").find("span").last().text(this.hasMinTemplate ? "Đầy đủ" : "Thu gọn");
            var currentTemplate = this.isHsmc ? this.fullTemplate : (this.hasMinTemplate ? this.minTemplate : this.fullTemplate);

            egov.formtemplate.view.load(this.$('.document-info-template'), currentTemplate);
            var doctype = egov.setting.allDoctypes.find(function (item) {
                return item.DocTypeId == that.model.get("DocTypeId")
            });
            if (doctype) {
                egov.request.getCatalog({
                    data: { docfieldId: doctype.DocFieldId },
                    success: function (result) {
                        if (result.error) {
                            return;
                        }

                        var template = $.tmpl('<label class="control-label">${CatalogName}</label><div class="control-value"><select class="form-control catalog-name" data-name="${CatalogKey}" name="catalog_${CatalogKey}" id="">{{each CatalogValues}}<option value="${Value}">${Value}</option>{{/each}}</select></div>', result.data);
                        that.$("#catalogContent").html(template);

                        var catalog = that.model.get("Address");
                        if (catalog) {
                            var keyCatalog = JSON.parse(catalog)
                            var keys = _.keys(keyCatalog)
                            _.each(keys, function (keyC) {
                                that.$("#catalogContent").find("[data-name='" + keyC + "']").val(keyCatalog[keyC])
                            })
                        }
                    }
                })
            }
        },

        _renderCatalog: function () {

        },

        _setFocus: function () {
            // Gọi trong form/html.js
            var that = this;
            setTimeout(function () {

                if (that.categoryBusinessId === 4) {
                    // Hồ sơ một cửa focus vào Tên công dân
                    that.$("#wrapCitizenName input").focus();
                    return;
                }

                if (that.isCreate) {
                    var firstElement = that.$('table #wrapCompendium textarea, table input[name="Organization"]').first();
                    if (firstElement) {
                        firstElement.focus();
                        firstElement.prop("selectionStart", firstElement.val().length);
                        firstElement[0].setSelectionRange(0, firstElement.val().length);
                    }

                    //if (that.categoryBusinessId == 2) {
                    //    that.$('#wrapCompendium textarea, input[name="Organization').focus();
                    //    // set con trỏ chuột ở cuối nội dung mặc định của trích yếu
                    //    that.$('#wrapCompendium textarea').prop("selectionStart", that.$('#wrapCompendium textarea').val().length);
                    //} else {
                    //    that.$('input[name="Organization"]').focus();
                    //}
                } else {
                    that.$('#wrapComment textarea').focus();
                }
            }, 300);
        },

        _renderCommonComments: function () {
            /// <summary>
            /// Hiển thị autocomplete ý kiến thường dùng
            /// </summary>
            var that = this;
            egov.dataManager.getCommonComment({
                success: function (comments) {
                    if (egov.isMobile) {
                        var savedComments = that.$("#commonCommentList");
                        savedComments.empty();
                        savedComments.append($.tmpl('<li class="mdl-menu__item">${$data}</li>', comments));
                        savedComments.find("li").click(function (e) {
                            that.$("#Comment").val($(this).text());
                        });
                        return;
                    }

                    egov.dataManager.getTemplateComments({
                        success: function (templateComments) {
                            templateComments = _.pluck(templateComments, "Content");
                            templateComments = _.filter(templateComments, function (item) {
                                return comments.indexOf(item) == -1
                            });

                            if (templateComments.length > 0) {
                                _.each(templateComments, function (item) {
                                    comments.push(item)
                                });
                                egov.dataManager.addCommonComment(comments, true)
                            }
                            that._renderAutocomplete(comments);
                        }
                    });

                    if (egov.isMobile) {
                        egov.mobile.materialReset();
                    }
                }
            });
        },

        _renderAutocomplete: function (sources) {
            /// <summary>
            /// Tạo autocomplete ý kiến thường dùng
            /// </summary>
            //<param name="sources" type="Array">Mảng danh sách giá trị trong kho tìm kiếm</param>
            this.$Comment = this.$("[name='Comment']");
            var that = this,
                commentWidth;

            if (this.$Comment.length > 0) {
                commentWidth = this.$Comment[0].offsetWidth;
                that.$Comment.autocomplete({
                    source: function (request, response) {
                        var value = egov.utilities.string.stripVietnameseChars(request.term).toLowerCase();
                        var data = [];
                        for (var i = 0; i < sources.length; i++) {
                            var item = egov.utilities.string.stripVietnameseChars(sources[i]).toLowerCase();
                            if (item.indexOf(value) != -1) {
                                data.push(sources[i]);
                            }
                        }

                        response(data);
                    },
                    autoFocus: true,
                    autoSelectFirst: true
                }).data("autocomplete")._renderItem = function (ul, item) {
                    ul.addClass('dropdown-menu');
                    return $("<li>")
                        .data("item.autocomplete", item)
                        .append("<a href='#' style='white-space: initial;width:" + commentWidth + "px'>" + item.value + "</a>")
                        .appendTo(ul);
                };
            };

            if (that.$("[name='Organization']").length > 0) {
                egov.dataManager.getAllAddress({
                    success: function (addresses) {
                        var source = _.map(addresses, function (item) {
                            return item.Name;
                        });
                        that.$("[name='Organization']").autocomplete({
                            source: source,
                            minLength: 2,
                            autoFocus: true,
                            focus: function () {
                                return false;
                            },
                            select: function (e, ui) {
                                that.$("[name='Organization']").val(ui.item.label);
                                e.preventDefault();
                            }

                        }).data("autocomplete")._renderItem = function (ul, item) {
                            ul.addClass('dropdown-menu');
                            return $("<li>")
                                .data("item.autocomplete", item)
                                .append("<a href='#' style='white-space: initial;'>" + item.value + "</a>")
                                .appendTo(ul);
                        };
                    }
                });
            }

            if (that.$("[name='DocCode']").length > 0) {
                that.$("[name='DocCode']")
                    .on("keydown", function (event) {
                        if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                            event.preventDefault();
                        }
                    })
                    .autocomplete({
                        minLength: 0,
                        source: function (request, response) {
                            var term = _.last(request.term.split("/"));
                            var organ = that.$("[name='Organization']").val();
                            var notations = egov.setting.codeNotations[organ];
                            response($.ui.autocomplete.filter(notations, term));
                        },
                        focus: function () {
                            return false;
                        },
                        select: function (event, ui) {
                            var result = this.value.split('/');
                            result.pop();
                            result.push(ui.item.value);
                            this.value = result.join('/');
                            return false;
                        }
                    }).data("autocomplete")._renderItem = function (ul, item) {
                        ul.addClass('dropdown-menu');
                        return $("<li>")
                            .data("item.autocomplete", item)
                            .append("<a href='#' style='white-space: initial;'>" + item.value + "</a>")
                            .appendTo(ul);
                    };
            }
        },

        ///Render các hàm xử lý tab mở xem thông tin văn bản,
        ///các comment ở khung quick view khi người dùng cấu hình hiển thị xem trước văn bản là xem đầy đủ thông tin
        _renderEventQuickView: function () {
            this.$tabQuickView = this.$('.tabs-quickview');
            this.$documentInfo = this.$('.document-info');
            var $hiddenElement = this.$documentInfo.find('form .detail-content>ul>li:not(.display)');
            $hiddenElement.hide();
            var $tab = this.$documentInfo.find('ul:first').attr('data-tabs');
            this.$('.' + $tab).show();
        },

        ///Hiển thị danh sách tiêu đề các form của văn bản/hồ sơ khi hiển thị trên quick view
        _renderTitleContent: function () {
            this.$titleContent = this.$('#titleContent tbody');
            var contents = this.model.get('DocumentContents');
            var leng = contents.length;
            var _this = this;
            for (var i = 0; i < leng; i++) {
                var titleContent = bindTitleQuickView(contents[i].DocumentContentId
                    , contents[i].ContentName
                    , function (id) {
                        _this.editContent(id)
                    });
                _this.$titleContent.append(titleContent);
            }
        },

        _addCommentCommon: function (comment, callback) {
            ///<summary>
            /// Thêm ý kiến thường dung vào localstorage
            ///</summary>
            if (!comment) {
                return;
            }

            if (typeof comment === "string") {
                if (comment == null || comment == "") {
                    return;
                }
            }
            else {
                if (comment.length <= 0) {
                    return;
                }
            }
            if (typeof comment === "string") {
                comment = splitComment(comment);
            }
            egov.dataManager.addCommonComment(comment, false, {
                success: function () {
                    egov.callback(callback);
                }
            });
        },

        _openDialogCommon: function (e) {
            ///<summary>
            /// Mở dialog danh sách mẫu ý kiến thường dùng cảu người dùng đã soạn trước đó
            ///</summary>
            if (!e) {
                return;
            }

            var target = $(e.target),
                that = this;

            target.attr("disabled", "disabled");
            this.$Comment = this.$("[name='Comment']");
            require(["templateComment"], function (TemplateComment) {
                new TemplateComment({
                    callbackInsertComments: function (commentText) {
                        var val = that.$Comment.val();
                        if (val) {
                            val += '\n' + commentText;
                        } else {
                            val = commentText;
                        }

                        that.$Comment.val(val);
                    },
                    callbackEnableButton: function () {
                        target.removeAttr("disabled");
                    }
                });
            });
        },

        //Hiển thị cấu hình dự kiến chuyển
        anticipate: function () {
            /// <summary>
            /// Mo form du kien chuyen
            /// </summary>
            var that = this;
            require(['anticipate'], function (Anticipate) {
                egov.anticipate = new Anticipate;
                egov.anticipate.render({
                    parent: that,
                    documentCopyId: that.model.get("DocumentCopyId"),
                    doctypeId: that.model.get("DocTypeId")
                });
            });
        },

        // Cấu hình phát hành báo cáo và kết thúc
        finishAndPublishData: function () {

        },

        //Hiển thị dự kiến chuyển
        transferPlan: function (action, plan) {
            /// <summary>
            /// Bàn giao văn bản theo hướng chuyển.
            /// </summary>
            /// <param name="action" type="object">Hướng chuyển.</param>
            var that = this,
                comment,
                transferAction;

            this.$Comment = this.$Comment || $();
            comment = this.$Comment.val();
            transferAction = action;
            // Xác nhận lại có cho phép hướng chuyển tiếp theo xem được các văn bản liên quan đã có hay không.
            that.relations.confirmRelations(function () {
                // Lưu các file đang được mở để chỉnh sửa
                that.attachments.confirmAttachments(function () {
                    //// Mở form bàn giao
                    //if (egov.transferForm === undefined) {
                    require(['transfer'], function (Transfer) {
                        egov.transferForm = new Transfer;
                        //}
                        egov.transferForm.render({
                            action: transferAction,
                            document: that,
                            plan: plan,
                            callback: function () {
                                //add comment vao cache
                                that._addCommentCommon(comment);
                            }
                        });
                    });
                });
            });
        },

        //Lấy dự kiến chuyển
        getDestinationPlan: function () {
            var destinationPlan = "";
            if (egov.anticipate) {
                destinationPlan = egov.anticipate.getDestinationPlan();
            }

            return destinationPlan;
        },

        //Cấu hình dự kiến phát hành
        comfigPublishmentPlan: function () {
            /// <summary>
            /// Cấu hình dự kiến phát hành
            /// </summary>
            var that = this;
            require(['publishmentView'], function (publishView) {
                egov.publishmentPlan = new publishView;
                egov.publishmentPlan.render({
                    document: that,
                    isPlan: true
                });
            });
        },

        //Hiển thị theo cấu hình dự kiến phát hành
        publishmentPlan: function (plan, isplan) {
            /// <summary>
            /// Hiển thị theo cấu hình dự kiến phát hành
            ///<param name= "plan" type="object">Cấu hình phát hành theo dự kiến </param>
            /// </summary>
            if (!plan || typeof plan !== "object") {
                egov.pubsub.publish(egov.events.status.error, egov.resources.document.configError);
            }

            var that = this;
            this.attachments.confirmAttachments(function () {
                require(['publishmentView'], function (publishView) {
                    egov.publishmentPlan = new publishView;
                    egov.publishmentPlan.render({
                        document: that,
                        plan: plan,
                        isPlan: isplan
                    });
                });
            });
        },

        _initHsmc: function () {
            this.isHsmc = this.model.get("CategoryBusinessId") === egov.enum.categoryBusiness.hsmc;

            if (!this.isHsmc) {
                return;
            }

            if (!this.isCreate || !this.model.get("WorkflowTypes") || this.model.get("WorkflowTypes").length <= 1) {
                this.$(".changeWorkflowType").hide();
            }


            this.$("[name='DateAppointed']").datepicker({
                onSelect: function (date) {
                    that.$("[name='DateAppointed']").val(date.format("HH:mm dd/MM/yyyy"));
                }
            });

            this.$newPaper = this.$(".papers.ul-papers>li:last-child").clone(true);
            this.$newFee = this.$(".fees.ul-fees>li:last-child").clone(true);
            this._calPrice();
            if (!egov.isMobile) {
                this._autocompleteCitizen();
                if (egov.setting.onlineRegistration
                    && egov.setting.onlineRegistration.Active) {
                    this._renderQuestionList();
                }
            }

        },

        _parseDateControl: function () {
            /// <summary>
            /// Load dữ liệu cho văn bản đến
            /// </summary>
            var that = this;

            setTimeout(function () {
                this.$(".datepicker").not("[name='DateAppointed']").datepicker().datepicker("setDate", null);

                var dateCreated = this.isCreate ? new Date : Date.parse(this.model.get("DateCreated"));
                this.$("#tableLayout [name='DateCreated']").datepicker("setDate", dateCreated);

                var dateArrived = this.isCreate ? new Date : Date.parse(this.model.get("DateArrived"));
                this.$("#tableLayout [name='DateArrived']").datepicker("setDate", dateArrived);

                var datePublished = this.isCreate ? new Date : Date.parse(this.model.get("DatePublished"));
                this.$("#tableLayout [name='DatePublished']").datepicker("setDate", datePublished);

                var dateResponse = this.isCreate ? new Date : Date.parse(this.model.get("DateResponse"));
                this.$("#tableLayout [name='DateResponse']").datepicker("setDate", dateResponse);

                this.$("#tableLayout [name='DateAppointed']").datepicker({
                    "onSelect": function (dateText, inst) {
                        inst.input.val(new Date().format('HH:mm ') + dateText);
                    }
                });
                var dateAppointed = (this.model.get("DateAppointed") == '' || this.model.get("DateAppointed") == null) ? "" : Date.parseFromIsoString(this.model.get("DateAppointed")).format('HH:mm dd/MM/yyyy');
                this.$("#tableLayout [name='DateAppointed']").val(dateAppointed);

                var dateOverdue = Date.parse(this.model.get("DateOverdue"));
                this.$("#tableLayout [name='DateOverdue']").datepicker("setDate", dateOverdue);
                this.renderDatePublished(datePublished);
                this.renderInOutPlace();

                // 20191120 VuHQ REQ-5
                // 20191120 VuHQ REQ-5
                var form = this.model.get("DocumentContents") != undefined ? this.model.get("DocumentContents")[0] : {};
                var formCode = this.model.get("ProcessInfo");
                if (that.isCreate) {
                    if (this.model.get("DocumentContents").length != 0)
                        formCode = form.FormCode;
                }
                else {
                    if (this.model.get("DocumentContents").length != 0)
                        form.ExplicitTemplate = this.model.get("DocumentContents")[0].Content;
                }

                if (this.model.attributes.CategoryBusinessId == 16) {
                    this._renderFormReportList();
                }
                else if (this.model.attributes.CategoryBusinessId == 8) {
                    if (this.model.get("DocumentContents").length != 0 && form.ExplicitTemplate != undefined)
                        this._renderFormExplicit();
                } else if (this.model.attributes.CategoryBusinessId == 4 || this.model.attributes.CategoryBusinessId == 64) {
                    this._renderHeaderFooterForm();
                    this._renderFormHandsonPlus();
                    this.buildQuery();
                    this.changePeriod();
                } else if (this.model.attributes.CategoryBusinessId == 32) {
                    this._renderSurveyContent();
                }
                else {
                    this._renderFormHandson();
                }
                // Kiểm tra điều kiện tổng hợp

                this.renderWarningCompilation();
            }.bind(this), 500);
        },

        findKyBaoCao: function (doctypeId) {
            var that = this;
            var doctypeId = this.model.get("DocTypeId")
            var doctypes = egov.setting.allDoctypes;
            var doctype = _.find(doctypes, function (dt) {
                return dt.DocTypeId == doctypeId;
            });
            if (doctype) {
                return doctype.ActionLevel
            }
            return 1;
        },

        renderInOutPlace: function myfunction() {
            var that = this;
            require(['/scripts/bkav.egov/libs/select2/select2.min.js'], function (object) {
                that.$("#InOutPlace").select2()
            });
        },

        renderDatePublished: function (datepublish) {
            var that = this;
            var kybaocao = that.findKyBaoCao();
            that.showDatePublished(kybaocao, datepublish);

            if (!that.isCreate) {
                that.$el.find("#kybaocao .timeKey").attr("disabled", "disabled");
                that.$el.find("#ddlYearReport").attr("disabled", "disabled");

            }
        },

        showDatePublished: function (kybaocao, datepublish) {
            var that = this;
            // là báo cáo ngày hiển thị bình thường
            if (kybaocao == 6) {
                return;
            }
            var template = '<option value="${value}">${name}</option>';
            this.$("#tableLayout [name='DatePublished']").hide();
            this.$("#datebykybaocao").removeClass("hidden");
            this.$("#ddlYearReport").val(datepublish.getFullYear());
            if (kybaocao == 1) {
                this.$("#ddlYearReport").css({ "width": "100%" })
            } else {
                this.$("#kybaocao").removeClass("hidden");
                if (kybaocao == 2) {
                    this.$("#bcnuanam").removeClass("hidden");
                    var month = datepublish.getMonth();
                    var value = Math.floor(month / 6 + 1);
                    this.$("#bcnuanam").val(value);
                }

                if (kybaocao == 3) {
                    this.$("#bcquy").removeClass("hidden");
                    var month = datepublish.getMonth();
                    var value = Math.floor(month / 3 + 1);
                    this.$("#bcquy").val(value);
                }

                if (kybaocao == 4) {
                    this.$("#bcthang").removeClass("hidden");
                    var month = datepublish.getMonth();
                    this.$("#bcthang").val(month);
                }

                if (kybaocao == 5) {
                    this.$("#bctuan").removeClass("hidden");
                    var week = datepublish.weekOfYear();
                    this.$("#bctuan").val(week);
                }

                if (kybaocao == 8) {
                    this.$("#bc9thang").removeClass("hidden");
                    this.$("#bc9thang").val(9);
                }
            }
        },

        serializeDatePublished: function (kybaocao) {
            var that = this;
            // là báo cáo ngày hiển thị bình thường
            if (kybaocao == 6) {
                that.TimeKey = that.formatDateTimeKey(that._getDate("DatePublished"));
                return that._getDate("DatePublished");
            }
            this.$("#tableLayout [name='DatePublished']").hide();
            var year = this.$("#ddlYearReport").val();

            if (kybaocao == 1) {
                that.TimeKey = year + "";
                return new Date(year, 1, 1).toServerString();
            } else {
                if (kybaocao == 2) {
                    var kybc = this.$("#bcnuanam").val();
                    that.TimeKey = year + "" + kybc;
                    var month = (Number(kybc) - 1) * 6 + 1;
                    return new Date(year, month, 1).toServerString();
                }

                if (kybaocao == 3) {
                    var kybc = this.$("#bcquy").val();
                    that.TimeKey = year + "" + kybc;

                    var month = (Number(kybc) - 1) * 3 + 1;
                    return new Date(year, month, 1).toServerString();
                }

                if (kybaocao == 4) {
                    this.$("#bcthang").val();
                    var month = this.$("#bcthang").val();
                    var monthKey = Number(month) + 1;
                    if (monthKey < 10)
                        monthKey = '0' + monthKey;

                    that.TimeKey = year + "" + monthKey;

                    return new Date(year, Number(month), 1).toServerString();
                }

                if (kybaocao == 5) {
                    var week = this.$("#bctuan").val();
                    var days = Number(week) * 7 + 1;

                    var weekKey = Number(week);
                    if (weekKey < 10)
                        weekKey = '0' + weekKey;

                    that.TimeKey = year + "" + weekKey;

                    // VuHQ START
                    var d = (1 + (weekKey - 1) * 7); // 1st of January + 7 days for each week
                    return new Date(year, 0, d).toServerString();

                    // result.setDate(new Date(year, 0, d));
                    //var result = new Date(year, 0, 1 + (weekKey - 1) * 7, 12);
                    //var dow = result.getDay();
                    //if (dow <= 4)
                    //    result.setDate(result.getDate() - result.getDay() + 1);
                    //else
                    //    result.setDate(result.getDate() + 8 - result.getDay());

                    // var result = new Date(year, 0, 1);
                    // result.setDate(result.getDate() + days);
                    // VuHQ END

                    return result;
                }

                if (kybaocao == 8) {
                    that.TimeKey = year + "0" + 9;

                    var result = new Date(year, 9, 1);
                    return result;
                }
            }
        },

        getTimeKeyStandard: function () {
            var that = this;
            var kybaocao = that.findKyBaoCao(that.model.get("DocTypeId"));
            var date = that.serializeDatePublishedStandard(kybaocao);
            return that.formatDateTimeKey(date)
        },

        serializeDatePublishedStandard: function (kybaocao) {
            var that = this;
            // là báo cáo ngày hiển thị bình thường
            if (kybaocao == 6) {
                that.TimeKeyName = "datekey"
                return that._getDate("DatePublished");
            }
            this.$("#tableLayout [name='DatePublished']").hide();
            var year = this.$("#ddlYearReport").val();

            if (kybaocao == 1) {
                that.TimeKeyName = "yearkey"
                return new Date(year, 0, 1).toServerString();
            } else {
                if (kybaocao == 2) {
                    var kybc = this.$("#bcnuanam").val();
                    that.TimeKeyName = "halfkey"

                    var month = (Number(kybc) - 1) * 6;
                    return new Date(year, month, 1).toServerString();
                }

                if (kybaocao == 3) {
                    var kybc = this.$("#bcquy").val();
                    that.TimeKeyName = "quarterkey"
                    var month = (Number(kybc) - 1) * 3;
                    return new Date(year, month, 1).toServerString();
                }

                if (kybaocao == 4) {
                    this.$("#bcthang").val();
                    var month = this.$("#bcthang").val();
                    var monthKey = Number(month);
                    if (monthKey < 10)
                        monthKey = '0' + monthKey;

                    that.TimeKeyName = "monthkey"

                    return new Date(year, Number(month), 1).toServerString();
                }

                if (kybaocao == 5) {
                    var week = this.$("#bctuan").val();
                    var days = Number(week) * 7 + 1;

                    var weekKey = Number(week);
                    if (weekKey < 10)
                        weekKey = '0' + weekKey;

                    that.TimeKeyName = "weekkey"
                    var d = (1 + (weekKey - 1) * 7); // 1st of January + 7 days for each week
                    return new Date(year, 0, d).toServerString();

                    return result;
                }

                if (kybaocao == 8) {
                    that.TimeKeyName = "ninekey"

                    var result = new Date(year, 9, 1);
                    return result;
                }
            }
        },
        formatDate: function (date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [year, month, day].join('-');
        },

        formatDateTimeKey: function (date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [year, month, day].join('');
        },

        _changeDateAppoint: function (e) {
            var target = $(e.target).closest("#ddlDateAppointRange"),
                dateRange = target.val();

            this._getDateAppointed(dateRange, this.model.get("DateCreated"));
        },

        //#endregion

        //#region HCMC functions

        _openDocFee: function () {

            this.$(".ul-fees").toggleClass("hidden");
            return;

            this.docFees = this.model.get("DocFees");
            var fees = this.model.get("DocFees");
            var supplementaries = this.model.get("Supplementary");
            var that = this;

            var listFeeElement = $('<div>').addClass("list-group");
            var feeElement = "<a hreg='#' class='list-group-item' value='${DocFeeId}'>${FeeName} <span class='pull-right deleteFee' style='margin-left:10px'>Xóa</span><span class='pull-right'>${Price}</span></a>";

            var groupTypes = _.groupBy(fees, function (p) { return p.Type });
            var types = Object.getOwnPropertyNames(groupTypes);
            var feeType = egov.enum.feeType;
            for (var i = 0; i < types.length; i++) {
                var type = parseInt(types[i]);
                var docFees = groupTypes[type];
                _.each(docFees, function (item) {
                    item.Price = item.Price + egov.setting.moneyFormat;
                });
                switch (type) {
                    case feeType.TiepNhan:
                        $.tmpl(feeElement, { FeeName: "Lệ phí tiếp nhận", Price: "" }).addClass("bold").appendTo(listFeeElement);
                        $.tmpl(feeElement, docFees).appendTo(listFeeElement);
                        break;
                    case feeType.ThuongBosung:
                        var groupFees = _.groupBy(docFees, function (p) { return p.SupplementaryId });
                        var supplementIds = Object.getOwnPropertyNames(groupFees);
                        for (var j = 0; j < supplementIds.length; j++) {
                            var suplementaryId = supplementIds[j];
                            var supp = supplementaries.detect(function (s) {
                                return s.get("SupplementaryId") == suplementaryId;
                            });
                            if (supp) {
                                $.tmpl(feeElement, { FeeName: "Lệ phí bổ sung lần " + supp.get("Order"), Price: "" }).addClass("bold").appendTo(listFeeElement);
                            }
                            $.tmpl(feeElement, groupFees[suplementaryId]).appendTo(listFeeElement);
                        }
                        break;
                    case feeType.TraCongDan:
                        $.tmpl(feeElement, { FeeName: "Lệ phí thu trả kết quả", Price: "" }).addClass("bold").appendTo(listFeeElement);
                        $.tmpl(feeElement, docFees).appendTo(listFeeElement);
                        break;
                }
            }

            listFeeElement.find(".deleteFee").click(function (e) {
                that._deleteDocFee(e);
            });

            listFeeElement.dialog({
                title: "Lệ phí đã thu",
                width: '450px',
                draggable: true,
                keyboard: true,
                height: "auto",
                buttons: [
                    {
                        text: egov.resources.common.closeButton,
                        className: 'btn-close',
                        click: function () {
                            var total = 0;
                            _.each(that.docFees, function (fee) {
                                total += parseInt(fee.Price);
                            });

                            that.$(".docFeeCount").text(total + ".000 đ");
                            listFeeElement.dialog('hide');
                        }
                    }
                ]
            });
        },

        _openDocPaper: function () {

            this.$(".ul-papers").toggleClass("hidden");
            return;
            this.docPapers = this.model.get("DocPapers");
            var papers = this.docPapers;
            var supplementaries = this.model.get("Supplementary");
            var that = this;

            var listPaperElement = $('<div>').addClass("list-group");
            var paperElement = "<a hreg='#' class='list-group-item' value='${DocPaperId}'>- ${PaperName} <span class='pull-right deletePaper' style='margin-left:10px'>Xóa</span><span class='pull-right'>${Amount}</span></a>";

            var groupTypes = _.groupBy(papers, function (p) { return p.Type });
            var types = Object.getOwnPropertyNames(groupTypes);
            var paperType = egov.enum.paperType;
            for (var i = 0; i < types.length; i++) {
                var type = parseInt(types[i]);
                var docPapers = groupTypes[type];
                switch (type) {
                    case paperType.TiepNhan:
                        $.tmpl(paperElement, { PaperName: "Giấy tờ tiếp nhận", Amount: docPapers.length }).addClass("bold").appendTo(listPaperElement);
                        $.tmpl(paperElement, docPapers).appendTo(listPaperElement);
                        break;
                    case paperType.ThuongBosung:
                        var groupPapers = _.groupBy(docPapers, function (p) { return p.SupplementaryId });
                        var supplementIds = Object.getOwnPropertyNames(groupPapers);
                        for (var j = 0; j < supplementIds.length; j++) {
                            var suplementaryId = supplementIds[j];
                            var supp = supplementaries.detect(function (s) {
                                return s.get("SupplementaryId") == suplementaryId;
                            });
                            if (supp) {
                                $.tmpl(paperElement, { PaperName: "Giấy tờ bổ sung lần " + supp.get("Order"), Amount: docPapers.length }).addClass("bold").appendTo(listPaperElement);
                            }
                            $.tmpl(paperElement, groupPapers[suplementaryId]).appendTo(listPaperElement);
                        }
                        break;
                    case paperType.TraCongDan:
                        $.tmpl(paperElement, { PaperName: "Giấy tờ thu trả kết quả", Amount: docPapers.length }).addClass("bold").appendTo(listPaperElement);
                        $.tmpl(paperElement, docPapers).appendTo(listPaperElement);
                        break;
                }
            }
            listPaperElement.find(".deletePaper").click(function (e) {
                that._deleteDocPaper(e);
            });

            listPaperElement.dialog({
                title: "Giấy tờ đã thu",
                width: '450px',
                height: '300px',
                draggable: true,
                keyboard: true,
                buttons: [
                    {
                        text: egov.resources.common.closeButton,
                        className: 'btn-close',
                        click: function () {
                            that.$(".docPaperCount").text(that.docPapers.length);
                            listPaperElement.dialog('hide');
                        }
                    }
                ]
            });
        },

        _deleteDocPaper: function (e) {
            var target = $(e.target).closest("a");
            var docPaperId = target.attr("value");
            var that = this;
            egov.request.deleteDocPaper({
                data: { id: docPaperId },
                beforeSend: function () {
                    target.remove();
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.deleteDocPaperError);
                        return;
                    }

                    that.docPapers = _.reject(that.docPapers, function (dp) {
                        return dp.DocPaperId == docPaperId;
                    });

                    that.model.set("DocPapers", that.docPapers);
                }
            });
        },

        _deleteDocTypePaper: function (e) {
            var target = $(e.target).closest("span.delete-paper");
            var paperId = target.attr("value");
            var doctypeId = this.model.get("DocTypeId");
            egov.request.deleteDoctypePaper({
                data: { doctypeId: doctypeId, paperId: paperId },
                beforeSend: function () {
                    target.parents("li.doc-paper").remove();
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.deleteDocPaperError);
                        return;
                    }
                }
            });
        },

        _deleteDocFee: function (e) {
            var target = $(e.target).closest("a");
            var docFeeId = target.attr("value");
            var that = this;

            egov.request.deleteDocFee({
                data: { id: docFeeId },
                beforeSend: function () {
                    target.remove();
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.deleteDocFeeError);
                        return;
                    }

                    that.docFees = _.reject(that.docFees, function (dp) {
                        return dp.DocFeeId == docFeeId;
                    });

                    that.model.set("DocFees", that.docFees);
                }
            });
        },

        _deleteDocTypeFee: function (e) {
            var target = $(e.target).closest("span.delete-fee");
            var paperId = target.attr("value");
            var doctypeId = this.model.get("DocTypeId");
            egov.request.deleteDoctypeFee({
                data: { doctypeId: doctypeId, feeId: paperId },
                beforeSend: function () {
                    target.parents("li.doc-fee").remove();
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.deleteDocFeeError);
                        return;
                    }
                }
            });
        },

        _changePaper: function (e) {
            if (!this.isHsmc) {
                return;
            }

            var target = $(e.target).closest(".paper-name"),
                parent = target.parents(".new-paper"),
                newPaper = target.val();
            if (String.isNullOrEmpty(newPaper)) {
                $(".new-paper").not(newPaper).remove();
            }
        },

        _addPaper: function (e) {
            /// <summary>
            /// Thêm giấy tờ
            /// </summary>
            /// <param name="e"></param>
            if (!this.isHsmc) {
                return;
            }

            var target = this.$(e.target),
                parent = target.parent("li"),
                currentText;

            if (parent.is(":last-child")) {
                this.$(".ul-papers").append(this.$newPaper.clone(true));
                return;
            }

            if (!target.val()) {
                parent.siblings(":last-child").remove();
            }
        },

        _addFee: function (e) {
            /// <summary>
            /// Thêm lệ phí
            /// </summary>
            /// <param name="e"></param>
            if (!this.isHsmc) {
                return;
            }

            var target = this.$(e.target),
                parent = target.parent("li");

            if (parent.is(":last-child")) {
                this.$(".ul-fees").append(this.$newFee.clone(true));
                return;
            }

            if (!target.val()) {
                parent.siblings(":last-child").remove();
            }
        },

        _calPrice: function (e) {
            /// <summary>
            /// Tính phí tổng
            /// </summary>
            /// <param name="e"></param>
            if (!this.isHsmc) {
                return;
            }
            var checked, hasName,
                total = 0,
                prices,
                target,
                targetVal = 0;

            prices = this.$(".fee-price");
            prices.each(function () {
                checked = $(this).siblings(".fee-id").is(":checked");
                hasName = $(this).siblings(".fee-name").val() || $(this).siblings(".fee-name").text();
                targetVal = $(this).val();
                if (targetVal) {
                    total = total + (checked && hasName ? parseInt(targetVal) : 0);
                }
            });

            total = total + egov.setting.moneyFormat;
            this.$("#totalFee").text(total);
        },

        _changeDateCreated: function (e) {
            var that = this,
                content,
                dialogSetting,
                currentDateCreated,
                currentDelayReason,
                newDateCreated,
                newDelayReason,
                dateRange;

            currentDateCreated = new Date(that.model.get("DateCreated"));
            currentDelayReason = that.model.get("DelayReason");
            dateRange = that.$("#ddlDateAppointRange").val();
            dialogSetting = {
                title: egov.resources.document.changeDateCreatedDialog.title,
                width: '500px',
                draggable: true,
                keyboard: true,
                height: "auto",
                buttons: [
                    {
                        id: "changeDateCreated",
                        text: egov.resources.document.changeDateCreatedDialog.submitBtn,
                        className: 'btn-info',
                        click: function () {
                            newDateCreated = $(".newDateCreatedDate").val() + "T" + $(".newDateCreatedTime").val();
                            newDelayReason = $(".newDelayReason").val();
                            //if ($(".applyAll").is(":checked")) {
                            egov.views.document = {
                                dateCreated: newDateCreated,
                                delayReason: newDelayReason
                            }
                            //}
                            that.$("input[name=DateCreated]").val(newDateCreated);
                            that._getDateAppointed(dateRange, newDateCreated);
                            that.model.set({
                                "DateCreated": newDateCreated,
                                "DelayReason": newDelayReason
                            });
                            that.$(".wrapDelayReason").removeClass("hidden");
                            that.$(".delayReason").text(newDelayReason);
                            content.dialog('destroy');
                        }
                    },
                    {
                        id: "closeChangeDateCreated",
                        className: 'btn-close',
                        text: egov.resources.document.changeDateCreatedDialog.closeBtn,
                        click: function () {
                            content.dialog('destroy');
                        }
                    }
                ]
            };

            require([egov.template.document.changeDateCreated], function (changeDateCreatedTemplate) {
                content = $.tmpl(changeDateCreatedTemplate,
                    {
                        dateCreatedDate: currentDateCreated.format("yyyy-MM-dd"),
                        dateCreatedTime: currentDateCreated.format("HH:mm:ss"),
                        delayReason: currentDelayReason
                    });
                content.dialog(dialogSetting);
                content.find(".newDelayReason").focus();
                content.find(".datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
                content.find(".timepicker").timepicker({ 'timeFormat': 'H:i:s' });
            })
        },

        _deleteDelayReason: function () {
            delete egov.views.document;
            var dateCreated = new Date;
            this.$("input[name=DateCreated]").val(dateCreated.format("yyyy-MM-ddTHH:mm:ss"));
            this.model.set({
                "DateCreated": dateCreated.format("yyyy-MM-ddTHH:mm:ss"),
                "DelayReason": ""
            });
            this.$(".wrapDelayReason").addClass("hidden");
        },

        _changeWorkflowType: function () {
            var that,
                workflowTypes,
                dialogSetting,
                selectedType,
                content,
                row,
                _selected;

            that = this;
            workflowTypes = that.model.get("WorkflowTypes");
            _selected = function () {
                selectedType = _.find(workflowTypes, function (item) {
                    return item.Id == content.find("input[type=radio]:checked").val();
                });
                that.model.set("WorkflowTypeId", selectedType.Id);
                that.$("input[name='WorkflowTypeId']").val(selectedType.Id);
                that._getDateAppointed(selectedType.ExpireProcess);
                content.dialog('destroy');
            };
            dialogSetting = {
                title: egov.resources.document.changeWorkflowTypesDialog.title,
                width: '500px',
                draggable: true,
                keyboard: true,
                height: "auto",
                buttons: [
                    {
                        id: "changeWorkflowTypes",
                        text: egov.resources.document.changeWorkflowTypesDialog.submitBtn,
                        className: 'btn-info',
                        click: function () {
                            _selected();
                        }
                    },
                    {
                        id: "closeWorkflowTypes",
                        className: 'btn-close',
                        text: egov.resources.document.changeWorkflowTypesDialog.closeBtn,
                        click: function () {
                            content.dialog('destroy');
                        }
                    }
                ]
            };

            require([egov.template.document.changeworkflowTypes], function (template) {
                content = $.tmpl(template, { workflowTypes: workflowTypes });
                content.dialog(dialogSetting);
                row = content.find(".workflowTypeRow");
                row.click(function (e) {
                    $(this).find("input[type=radio]").prop("checked", true);
                });
                row.dblclick(function (e) {
                    _selected();
                });
                content.find(".datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
                content.find(".timepicker").timepicker({ 'timeFormat': 'H:i:s' });
            })
        },

        _getDateAppointed: function (range, dateCreated) {
            var that = this;
            egov.request.getDateAppointed({
                data: {
                    range: range,
                    dateCreated: dateCreated
                },
                success: function (result) {
                    var newDate = Date.parse(result);
                    that.$("[name='DateAppointed']").datepicker("setDate", newDate);
                    that.$("#ddlDateAppointRange").val(range);
                }
            });
        },

        _openOnlineRegistration: function (id) {
            /// <summary>
            /// Mở văn bản đăng ký qua mạng
            /// </summary>
            var that = this;
            egov.request.getDocumentDetailOnlineRegistration({
                data: { id: id },
                success: function (result) {
                    if (result) {
                        that.model = result;
                        that.renderDocumentOnlineRegistration();
                    } else {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                    }
                },
                error: function (xhr) {
                    that.$el.remove();
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }, complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },

        _openQuestion: function () {
            /// <summary>
            /// Mở câu hỏi
            /// </summary>
            var that = this;
            var questionId = that.question.get("QuestionId")
            require(['question'], function (Question) {
                var question = new Question({
                    id: "tabQuestionId_" + questionId,
                    questionId: questionId,
                    question: that.question,
                    tab: that.tab
                })
                that.$el.append(question.$el);
                egov.pubsub.publish(egov.events.status.destroy);
            })
        },

        transferLienThong: function (comment, addressIds, date) {
            var that = this,
                doc,
                selectedFiles,
                removeFiles,
                documentCopyId;

            that.attachments.confirmAttachments(function () {
                doc = that.serialize();
                if (doc == undefined) {
                    return;
                }

                selectedFiles = {};
                that.attachments.model.each(function (file) {
                    if (file.get('isNew')) {
                        selectedFiles[file.get('Id')] = { name: file.get('Name') }
                    }
                });

                removeFiles = that.attachments.model.select(function (file) {
                    return file.get('isRemoved');
                });
                documentCopyId = that.model.get("DocumentCopyId");
                documentCopyId = parseInt(documentCopyId);
                if (isNaN(documentCopyId)) {
                    return;
                }

                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);

                egov.request.transferLienThong({
                    data: {
                        "doc": JSON.stringify(doc),
                        "files": JSON.stringify(selectedFiles),
                        "modifiedFiles": JSON.stringify(that.attachments.modifiedFiles),
                        "removeAttachmentIds": removeFiles,
                        "documentCopyId": documentCopyId,
                        "addressIds": addressIds,
                        "comment": comment,
                        "dateAppointed": date
                    },
                    success: function (result) {
                        if (result.success) {
                            egov.pubsub.publish(egov.events.status.success, _resource.lienthong.sendSuccess);
                            // close tab
                            that.tab.close();

                            // reload lại cây văn bản
                            that._reloadDocumentTree(documentCopyId);
                        } else {
                            // egov.message.error(result.error);
                            egov.pubsub.publish(egov.events.status.error, result.error);
                        }
                    },
                    error: function () {
                        egov.pubsub.publish(egov.events.status.error, _resource.lienthong.sendFail);
                    }
                });
            });
        },

        _bindEventMobile: function () {
            this.$(".document-info, #commentListCopy").scroll(function (e) {
                if ($(e.currentTarget).scrollTop() > 200) {
                    $("#btndocumentuptotop").addClass("display");
                }
                else {
                    $("#btndocumentuptotop").removeClass("display");
                }
            });

            //egov.materialReset();
        },

        //Map status

        documentOnlineStatus: [
            {
                value: 1,
                text: egov.resources.document.documentOnlineStatus.choDuyet
            },
            {
                value: 2,
                text: egov.resources.document.documentOnlineStatus.dangXuLy
            },
            {
                value: 3,
                text: egov.resources.document.documentOnlineStatus.choBoSung
            },
            {
                value: 4,
                text: egov.resources.document.documentOnlineStatus.choThanhToan
            },
            {
                value: 5,
                text: egov.resources.document.documentOnlineStatus.choTraKetQua
            },
            {
                value: 6,
                text: egov.resources.document.documentOnlineStatus.daTraKetQua
            },
            {
                value: 7,
                text: egov.resources.document.documentOnlineStatus.biTuChoi
            }
        ],

        renderDocumentOnlineRegistration: function () {
            /// <summary>
            /// Hiển thị văn bản đăng ký qua mạng.
            /// </summary>
            var that = this,
                currentStatus,
                $target,
                jsonModel,
                itemModel;

            require([egov.template.document.onlineRegistration], function (onlineRegistration) {
                currentStatus = _.find(that.documentOnlineStatus, function (item) {
                    return item.value = that.model.Status;
                });
                that.model.StatusText = currentStatus.text;
                that.$el.append($.tmpl(onlineRegistration, that.model));
                that.$("#comment").focus();
                that.$('.attachment-download-all').hide();
                jsonModel = jQuery.parseJSON(that.model.FormJson);
                if (!(jsonModel instanceof Array)) {
                    var tmpModel = [];
                    tmpModel.push(jsonModel);
                    jsonModel = $.extend([], tmpModel);
                }

                $target = that.$("#forms-area");
                if (jsonModel && jsonModel.length > 0) {
                    // Để tạm, cần đưa vào dynamic
                    require([
                        'knockout',
                        'eFormJqueryGlobal',
                        'eFormJsutilt',
                        'eFormLibdata',
                        'eFormControls',
                        'eFormLib',
                        'eFormDB',
                        'eFormTool',
                        'eFormResize',
                        'eFormBkavEgateApplet',
                        'eFormBkavEgate',
                        'eFormBkavEgateCompiler'
                    ], function () {
                        $("head").append("<link href='../Scripts/bkav.egov/libs/eForm/css/eformTn.css' rel='stylesheet' /><style>.icontainer:hover {background: none !important;}#pnl_root, .pnl_root {background-color: transparent;}</style>");
                        require(['knockout'], function (ko) {
                            window.ko = ko; // ko hiểu chổ nào ghi đè cái "ko" nên phải gán lại ở đây
                            for (var i = 0; i < jsonModel.length; i++) {
                                if (jsonModel[i] == null) {
                                    continue;
                                }
                                itemModel = {
                                    collections: jsonModel[i].JssCatalog,
                                    schema: jsonModel[i].JssForm,
                                    formid: jsonModel[i].FormId.replace(/-/gi, ''),
                                    maxRow: jsonModel[i].MaxRow
                                };
                                $target.addClass("pnl_root sub_pnl_root").append("<div id='div" + itemModel.formid + "'></div>")

                                eForm.Lib.Init();
                                eForm.efTools.init(null, "div" + itemModel.formid, itemModel.formid);
                                // eForm.efTools.LoadForm(model.schema, model.maxRow);

                                // Tạo danh mục catatalog
                                fformModel.fromCatalog(itemModel.collections); // egate.compiler.js

                                // Chuẩn bị view cho form động + tạo database cho form động từ model.schema
                                eForm.efTools.LoadForm(itemModel.schema, itemModel.maxRow); // eForm.Tool.js

                                // Chuẩn bị model cho form động
                                var partModel = fformModel.fromSchema(eForm.database.GetAll(itemModel.formid), itemModel.formid); // egate.compiler.js
                                $.extend(fformModel, partModel);
                                // Dùng knockoutjs gắn view + model vào với nhau để hiển thị lên web.
                                ko.applyBindings(fformModel, document.getElementById("div" + itemModel.formid));
                            }
                        });
                    });
                }

                egov.callback(that.callback);
            })
        },

        _docOnlineActions: function (e) {
            var that = this,
                status,
                model,
                files,
                doc,
                data;

            this.$("#wrapComment").removeClass("isrequired");
            status = that.$(e.target).closest(".doc-online-btn").data("status");
            model = $.extend({}, that.model);
            files = JSON.stringify(model.Files)
            model.Files = undefined;
            model.Id = undefined;
            model.DocTypeId = model.DoctypeId;
            model.Doctype = {};
            model.Comment = that.$("#comment").val();

            if (status == 'Accept') {
                if (model.Comment == "") {
                    this.$("#wrapComment").addClass("isrequired");
                    return;
                }
                doc = JSON.stringify(model);
                data = {
                    docId: that.model.Id,
                    doc: doc,
                    files: files
                };

                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                egov.request.acceptOnline({
                    data: data,
                    success: function (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.error);
                            return;
                        }
                        if (result.success) {
                            egov.pubsub.publish(egov.events.status.success, result.success);
                            if (that.tab) {
                                that.tab.close3();
                            }
                            egov.views.home.tree.reloadOnlineRegistration();
                        }
                    }
                });

            } else if (status == 'Reject') {
                var $rejectForm = $("<textarea id='rejectComment' />");
                $rejectForm.addClass("form-control");

                var dialogSetting = {
                    title: egov.resources.document.documentOnline.insertRejectComment,
                    width: '1200px',
                    height: "auto",
                    top: '120px',
                    draggable: true,
                    keyboard: true,
                    buttons: [
                        {
                            id: "btnConfirm",
                            className: 'btn-primary',
                            text: egov.resources.common.confirmButton,
                            click: function () {
                                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                                egov.request.rejectOnline({
                                    data: {
                                        'docId': that.model.Id,
                                        'phone': model.Phone,
                                        'compendium': model.Compendium,
                                        'comment': $rejectForm.val(),
                                        'mail': model.Email,
                                        'doctypeId': model.DocTypeId,
                                        'docFieldId': model.DocfieldId,
                                        //HopCV: Bổ sung thêm thông tin để gửi mail, sms thông báo
                                        'docCode': model.DocCode,
                                        'citizenName': model.FullName,
                                        'dateRegister': model.DateRecieved
                                    },
                                    success: function (result) {
                                        if (result.error) {
                                            egov.pubsub.publish(egov.events.status.error, result.error);
                                            return;
                                        }
                                        if (result.success) {
                                            egov.pubsub.publish(egov.events.status.success, result.success);
                                            if (that.tab) {
                                                that.tab.close3();
                                            }
                                            egov.views.home.tree.reloadOnlineRegistration();
                                        }
                                    }
                                });
                                $rejectForm.dialog('destroy');
                            }
                        },
                        {
                            id: "closeCheckCitizenInfo",
                            className: 'btn-close',
                            text: egov.resources.common.closeButton,
                            click: function () {
                                $rejectForm.dialog('destroy');
                            }
                        }
                    ]
                };

                $rejectForm.dialog(dialogSetting);
            } else if (status == "AdditionalRequirements") {
                require(['supplementaryOnline'], function (SupplementaryOnline) {
                    new SupplementaryOnline({
                        docId: that.model.Id,
                        onlineModel: model,
                        document: that
                    });
                });
            }

            egov.callback(that.callback);
        },

        checkCitizenInfo: function () {
            var that = this, model = this.model, $formInfo = this.$(".staticPart"), data;

            data = {
                citizenName: $formInfo.find("[name='CitizenName']").val(),
                address: $formInfo.find("[name='Address']").val(),
                idCard: $formInfo.find("[name='IdentityCard']").val(),
                phone: $formInfo.find("[name='Phone']").val(),
                email: $formInfo.find("[name='Email']").val(),
                isCheckExistOnly: !model.IdCard
            };

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.loading);
            require(['checkCitizenInfo'], function (CheckCitizenInfo) {
                new CheckCitizenInfo({
                    model: data,
                    document: that
                });
            })
        },

        dowloadDocOnlineAttachment: function (e) {
            var target,
                id,
                fileName,
                link,
                html;

            target = this.$(e.target).closest("li.list-group-item");
            id = target.data("id");
            //window.open(egov.setting.onlineRegistration.onlineApiUrl + "/Document/DownLoad/" + id);
            fileName = target.data("name");
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
            require(['filedownload'], function () {
                link = "/DocumentOnline/DownLoadFileOnlineRegistration?fileId=" + id + "&fileName=" + fileName;
                $.fileDownload(link, {
                    done: function () {
                        egov.pubsub.publish(egov.events.status.destroy);
                    },
                    fail: function (response) {
                        html = $(response);
                        try {
                            var json = JSON.parse(html.text());
                            egov.pubsub.publish(egov.events.status.error, json.error);
                        } catch (e) {
                            egov.pubsub.publish(egov.events.status.error, egov.resources.file.errorDownload);
                        }
                    }
                });
            });
        },

        openDocOnlineAttachment: function (e) {
            var target,
                id,
                fileName,
                link,
                win;

            target = this.$(e.target).closest("li.list-group-item");

            id = target.data("id");
            fileName = target.data("name");
            $.ajax({
                url: "/DocumentOnline/OpenFileOnlineRegistration",
                data: {
                    fileId: id,
                    fileName: fileName
                },
                type: "GET",
                success: function (base64String) {
                    if (base64String != "") {
                        win = window.open(base64String, '_blank');
                        win.focus();
                    }
                    else {
                        window.open("/DocumentOnline/DownLoadFileOnlineRegistration?fileId=" + id + "&fileName=" + fileName);
                    }
                }
            });
        },

        _delApprover: function (e) {
            var that = this,
                id = $(e.target).data("approverid"),
                approvers = this.model.get("Approver"),
                parent = $(e.target).parent(".approverItem");

            egov.request.deleteApprover({
                data: {
                    appId: id
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }
                    else {
                        approvers = _.reject(approvers, function (item) {
                            return item.ApproverId == id;
                        });
                        that.model.set("Approver", approvers);
                        parent.remove();
                        if (approvers.length == 0) {
                            that.model.set({
                                IsSuccess: null,
                                UserSuccessId: null,
                                DateSuccess: null,
                            });
                        }
                    }
                }
            });
        },

        _delUpdateResult: function (e) {
            var that = this;
            var id = that.model.get("DocumentCopyId"),
                parent = $(e.target).parents(".resultItem");;
            egov.request.deleteResult({
                data: {
                    documentCopyId: id
                },
                success: function (result) {
                    parent.remove();
                }
            });
        },

        _scrollDocument: function (e) {
        },

        _ReSendLienThong: function (e) {
            var target = $(e.target).closest("a.btnResendLt");
            var publishId = parseInt(target.attr("data-id"));
            if (!publishId || publishId === 0) {
                return;
            }

            egov.request.resendLienThong({
                data: { publishId: publishId },
                beforeSend: function () {
                    egov.pubsub.publish(egov.events.status.processing, "Đang gửi lại");
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.message);
                        return;
                    }

                    egov.pubsub.publish(egov.events.status.success, "Gửi lại thành công");
                    // Xử lý hiển thị lên giao diện
                },
                error: function () {

                },
                complete: function () {
                    egov.pubsub.publish(egov.events.status.hide);
                }
            })
        },

        _recalledLienThong: function (e) {
            // Thu hồi văn bản liên thông
            var target = $(e.target).closest("a.btnRecalled");
            var publishId = parseInt(target.attr("data-id"));
            if (!publishId || publishId === 0) {
                return;
            }

            egov.message.prompt("Vui lòng nhập lý do lấy lại", "Lấy lại", function (comment) {
                egov.request.recalledLienThong({
                    data: { publishId: publishId, comment: comment },
                    beforeSend: function () {
                        egov.pubsub.publish(egov.events.status.processing, "Đang thu hồi văn bản.");
                    },
                    success: function (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.message);
                            return;
                        }
                        target.hide();
                        var status = result.status;
                        if (status == 15) {
                            target.parents('tr').find(".lt-status span").text("Đã thu hồi");
                        }
                        if (status == 13) {
                            target.parents('tr').find(".lt-status span").text("Đã gửi y/c lấy lại");
                        }

                        egov.pubsub.publish(egov.events.status.success, "Gửi thu hồi thành công");
                    },
                    complete: function () {
                        egov.pubsub.publish(egov.events.status.hide);
                    }
                });
            });
        },

        _recalledAllLienThong: function (e) {
            var target = $(e.target).closest('li');
            var doctypeid = target.attr("data-id");
            var relationType = 4; // liên quan thu hồi
            this.answer(doctypeid, relationType);
        },

        //#endregion

        getTempMailOrSms: function (templateId, templateName, isMail) {
            ///<summary>
            ///Lấy mẫu gủi mail hoặc sms cho người đăng ký trên hồ sơ
            ///</summary>
            ///<param name="templateId">ID mau phoi<param>
            ///<param name="templateName">ten mau phoi<param>
            ///<param name="isMail">True: gui mail, False:Gui sms<param>
            if (!this.isHsmc || (templateId == undefined || templateId == null || templateId <= 0))
                return;

            var docNull = "00000000-0000-0000-0000-000000000000";
            var documentId = this.model.get("DocumentId") || docNull;
            if (documentId === docNull) {
                egov.pubsub.publish(egov.events.status.error, egov.resources.document.notExist);
                return;
            }

            if (isMail === undefined || isMail === null) {
                isMail = true;
            }

            var that = this;
            var data = {
                isMail: isMail,
                documentId: this.model.get("DocumentId"),
                templateId: templateId
            }

            egov.request.editTemplate({
                data: data,
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }

                    var tempModel = {
                        templateId: templateId,
                        documentId: documentId,
                        isMail: isMail,
                        templateName: templateName,
                        content: result.content,
                        email: that.model.get("Email"),
                        phone: that.model.get("Phone"),
                        userNameRecive: that.model.get("CitizenName")
                    };

                    require(['tempMailOrSms'], function (EditTempMailOrSms) {
                        if (egov.editTempMailOrSms === undefined) {
                            egov.editTempMailOrSms = new EditTempMailOrSms;
                        }

                        egov.editTempMailOrSms.render({
                            model: tempModel
                        });
                    });
                },
                error: function (xhr) {
                    egov.pubsub.publish(egov.events.status.error, xhr.message);
                }
            });
        },

        _managerDoctypePaperFee: function () {
            var doctypeId = this.model.get("DocTypeId");
            var doctypeName = this.model.get("DocTypeName");
            var type = 1;
            var that = this;

            var success = function (result) {
                egov.pubsub.publish(egov.events.status.success, "Cập nhật thành công, vui lòng tiếp nhận lại.");
            };

            require(["doctypePaperFee"], function (doctypePaperFeeView) {
                var doctypePaperAndFeeView = new doctypePaperFeeView({
                    doctypeId: doctypeId,
                    doctypeName: doctypeName,
                    type: type,
                    callback: success
                });
            });
        },
        //Survey
        _openEditSurveyDocument: function (documentCopyId) {
            /// <summary>
            /// Mở văn bản
            /// </summary>
            /// <summary>
            /// Ham lấy thồng tin cấu hình khi bàn giao văn bản
            /// </summary>
            var that = this,
                doc = that.documentInfo,
                docTypeId = doc ? doc.get("DocTypeId") : null,
                categoryBusinessId = doc ? doc.get("CategoryBusinessId") : null,
                currentNodeId = doc ? doc.get("NodeCurrentId") : null;
            var storePrivateId = that.model == undefined ? null : that.model.get("StorePrivateId");

            egov.dataManager.getDocumentEditInfo(that.id, storePrivateId, {
                success: function (data) {
                    if (data.error) {
                        that.tab.close();
                        egov.pubsub.publish(egov.events.status.error, data.error);
                        return;
                    }

                    that.isHsmc = data.document.CategoryBusinessId === egov.enum.categoryBusiness.hsmc;
                    that.isVanBanDen = data.document.CategoryBusinessId == egov.enum.categoryBusiness.vbden;

                    // đưa ra trạng thái người dùng mở văn bản đến không phải văn thư sẽ hiển thị không đầy đủ
                    that.hasMinTemplate = !egov.setting.userSetting.AlwaysDisplayFullDocumentInfo && (data.hasKhoiTao != null ? !data.hasKhoiTao : false);

                    //đưa những giá trị lấy được trên server vào model
                    that.model.set(data.document);

                    // Quyền đánh lại số đến
                    that.model.set("HasPrivateSaveToStore", data.hasPrivateSaveToStore);
                    that.model.set("DocumentPermissions", data.documentPermission);
                    that.model.set("LienThongs", data.lienThongs);
                    var process = JSON.parse(data.document.ProcessInfo || null);
                    if (process != null) {
                        that.model.set("SurveyConfig", process.SurveyConfig || "");
                        that.model.set("SurveyCriteria", process.SurveyCriteria || "");
                        that.model.set("SurveyReport", process.SurveyReport || "");
                        that.model.set("DonViNhan", process.DonViNhan || "");
                    } else {
                        that.model.set("SurveyConfig", "");
                        that.model.set("SurveyCriteria", "");
                        that.model.set("SurveyReport", "");
                        that.model.set("DonViNhan", "");
                    }

                    that.hasUpdatePermission = data.hasUpdatePermission;

                    that.publishPlan = data.plan.publish;

                    that._renderInfo();
                    that._initDocument();
                }, error: function (xhr) {
                    that.tab.close();
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }, complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },
        _openCreateSurveyDocument: function (doctypeId) {
            /// <summary>
            /// Mở văn bản trong form khởi tạo
            /// </summary>
            var that = this;
            var doctype = _.find(egov.setting.allDoctypes, function (dt) {
                return dt.DocTypeId == doctypeId;
            });


            //that.isHsmc = doctype.CategoryBusinessId === egov.enum.categoryBusiness.hsmc;
            //that.isVanBanDen = doctype.CategoryBusinessId == egov.enum.categoryBusiness.vbden;

            egov.dataManager.getDocumentCreateInfo(that.id, that.relationAnswerId, {
                success: function (data) {
                    if (data) {
                        if (data.error) {
                            that.tab.close(true);
                            egov.pubsub.publish(egov.events.status.error, data.error);
                            return;
                        }

                        if (that.copiedDocument) {
                            var arr = ["DocCode", "InOutCode", "DocumentCopyId", "DocumentPermissions", "DateAppointed", "DateCreated", "DateResponse", "DateModified", "DateResult", "TransferType", "TransferTypeInEnum"];
                            _.each(arr,
                                function (item) {
                                    that.model.set(item, data[item]);
                                });

                        } else {
                            var organization = "";
                            if (that.model.get("Organization")) {
                                if (that.model.get("CategoryBusinessId") == 1) {
                                    that.model.set("InOutCode", data.InOutCode);
                                    that.model.set("DocCode", "");
                                    that.model.set("DocumentContents", []);
                                    that.model.set("Compendium", "");
                                    that.model.set("Attachments", []);
                                    that.model.set("CategoryId", data.CategoryId)
                                    that.model.set("StoreId", data.StoreId)
                                    that._getInOutCode();
                                }
                            } else {
                                that.model.set(data);
                            }

                        }
                        //if (data.Stores && data.Stores.length > 0) {
                        //    if (!that.stores) {
                        //        that.stores = [];
                        //    }
                        //    that.stores.push({
                        //        StoreId: data.Stores[0].StoreId,
                        //        CategoryId: data.CategoryId,
                        //        Codes: data.CategoryBusinessId == 1 ? data.InOutCodes : data.DocCodes
                        //    });
                        //}

                        if (!that.model.get("CategoryId")) {
                            that.model.set("CategoryId", 5)
                        }
                        that._renderSurvey();

                    }
                }, error: function (xhr) {
                    that.tab.close();
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.openError);
                }, complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },
        _renderSurvey: function () {
            /// <summary>
            /// Load giao diện
            /// </summary>

            var that = this;
            that._unescapeModel();
            //Nếu không phải là giao diện mobile-tablet và đang hiên thị thông tin tóm tắt thì ghi đè html  không thì append html
            if (that.isShowingPreview) {
                var tempModel = that.model;
                that.model.attributes.cid = that.model.cid;
                tempModel.set("Compendium", tempModel.get("Compendium").split('\n').join('<br />'));
                that.$el.html(parseCommentNew(that.template, tempModel.toJSON()));
                that._initDocument();
            }
            else {
                that._renderInfo();
                that._initDocument();
            }
            //if (that.isShowingPreview) {
            //    var tempModel = that.model;
            //    tempModel.set("Compendium", tempModel.get("Compendium").split('\n').join('<br />'));
            //    that.$el.html(parseCommentNew(that.template, tempModel.toJSON()));
            //    that._initDocument();
            //}
            //else {
            //    that._renderInfo();
            //    that._initDocument();
            //}
            that.renderBusinessLicense();
        },
        _renderSurveyContent: function () {
            var that = this;
            require(['surveyContentView'], function (survey) {
                egov.views.survey = new survey;
                egov.views.survey.render();
                that.$el.find('#exTab').append(egov.views.survey.$el);
                var surveyConfig = JSON.parse(that.$el.find('input[name=SurveyConfig]').val() || "[]"),
                    surveyCriteria = JSON.parse(that.$el.find('input[name=SurveyCriteria]').val() || "[]"),
                    donViNhan = that.model.get('DonViNhan'),
                    surveyReport = that.$el.find('input[name=SurveyReport]').val() || "";
                // get thoi gian bao cao theo filter
                var timeKey = that.$el.find("#ddlYearReport option:selected").val();
                egov.views.survey.renderWarningCompilation(that, timeKey);
                egov.views.survey.renderFormSurvey(surveyConfig, surveyCriteria, donViNhan, that, surveyReport);

            });
        }
    });

    //#endregion

    //#region Private Methods

    function toggleFormStaticResize($el, template, isShowFull) {
        egov.formtemplate.view.reviewToggle($el, template, isShowFull);
    }

    function formatCurrency(number, ext) {
        var a, b, ext, newExt;

        if (!ext) {
            ext = ".000";
        }
        ext = "000";

        if (!parseFloat(number)) {
            return "";
        };

        if (number == 0) {
            return "";
        }

        number = number.split(".").join("");

        if (number.length == 1 || getIndicesOf(ext, number).length == 1) {
            a = number.split(ext).join("");
        }

        if (occurrences(number, ext) > 1) {
            var x = getIndicesOf(ext, number);
            var y = x[x.length - 1];
            a = number.substr(0, y) + number.substr(y + ext.length);
        }

        if (!a) {
            newExt = ext.slice(0, -1);
            a = number.split(newExt).join("");
            a = a.slice(0, -1);
        }

        if (!a) {
            return "";
        }

        a = a.split("").reverse().join("");
        a = a.split(".").join("");
        b = a.replace(/\d\d\d(?!$)/g, "$&.");
        return b.split('').reverse().join('') + "." + ext;
    }

    function occurrences(string, subString, allowOverlapping) {

        string += "";
        subString += "";
        if (subString.length <= 0) return (string.length + 1);

        var n = 0,
            pos = 0,
            step = allowOverlapping ? 1 : subString.length;

        while (true) {
            pos = string.indexOf(subString, pos);
            if (pos >= 0) {
                ++n;
                pos += step;
            } else break;
        }
        return n;
    }

    function getIndicesOf(searchStr, str, caseSensitive) {
        var startIndex = 0, searchStrLen = searchStr.length;
        var index, indices = [];
        if (!caseSensitive) {
            str = str.toLowerCase();
            searchStr = searchStr.toLowerCase();
        }
        while ((index = str.indexOf(searchStr, startIndex)) > -1) {
            indices.push(index);
            startIndex = index + searchStrLen;
        }
        return indices;
    }

    // Các loại ý kiến xử lý
    var commentType = { Common: 1, Consulted: 2, Contribution: 3, Supplementary: 4, Signed: 5, Success: 6, Finished: 7, Reopen: 8, ReleasedSurvey: 9, CompletedSurvey: 10 };

    function isScrolledIntoView($elem, $parent, heightChange) {
        var docViewTop = $parent.scrollTop();
        var elemTop = $elem.offset().top + heightChange;
        return (elemTop >= docViewTop);
    }

    // Xử lý tách các loại ý kiến xử lý từ danh sách ý kiến server trả về
    function parseComment(comments, allUsers, unit) {
        var result = { CoProcessor: [], Processor: [] };
        var content;
        var commentTypeEnum = commentType;
        for (var i = 0; i < comments.length; i++) {
            var comment = comments[i];
            if (typeof comment.Content == "string") {
                comment.Content = comment.Content.replace(/[\n]/gi, "").split('\\n').join('<br />');
                content = JSON.parse(comment.Content);
            }
            comment.Description = "";

            if (comment.CommentType == commentTypeEnum.Common) {
                var transfers = content.Transfers;

                transfers = _.sortBy(transfers, function (item) { return item.type; });
                comment.Content = content;
                if (comment.Content.Transfers.length == 1 && comment.Content.Transfers[0].type == "2") {
                    comment.Content.Transfers[0].type = "1"; // gửi 1 đồng xử lý thì hiển thị là xử lý chính
                }

                if (comment.Content.Transfers.length > 0) {
                    comment.Description = String.format("Chuyển tới <span class='comment-received'>{0}</span>", transfers[0].label);
                    if (transfers.length > 1) {
                        var otherNumber = 0;
                        for (var j = 1; j < transfers.length; j++) {
                            var peoples = transfers[j].label;
                            otherNumber += peoples.split(",").length;
                            comment.Content.Transfers[j].type = unit ? commentTypeEnum.ReleasedSurvey : comment.Content.Transfers[j].type;
                        }
                        comment.Description += String.format(" và <span class='comment-received'>{0} người khác</span>", otherNumber);
                    }
                } else {
                    comment.Description = "Gửi ý kiến";
                }
                result.Processor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.ReleasedSurvey) {
                var transfers = content.Transfers;
                transfers = _.sortBy(transfers, function (item) { return item.type; });
                comment.Content = content;
                comment.Content.Transfers[0].type = commentTypeEnum.ReleasedSurvey;
                if (comment.Content.Transfers.length > 0) {
                    comment.Description = String.format("Phát hành tới <span class='comment-received'>{0}</span>", transfers[0].label);
                    if (transfers.length > 1) {
                        var otherNumber = 0;
                        for (var j = 1; j < transfers.length; j++) {
                            var peoples = transfers[j].label;
                            otherNumber += peoples.split(",").length;
                            comment.Content.Transfers[j].type = commentTypeEnum.ReleasedSurvey;
                        }
                        comment.Description += String.format(" và <span class='comment-received'>{0} người khác</span>", otherNumber);
                    }
                }
                result.Processor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.CompletedSurvey) {
                content = JSON.parse(comment.Content);
                comment.Content = content;
                comment.Content.Transfers = [];
                comment.Description = "Hoàn thành phiếu khảo sát";
                result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Consulted) {
                content = JSON.parse(comment.Content);
                comment.Content = content;
                comment.Content.Transfers = [];
                comment.Description = "Gửi ý kiến đồng xử lý";
                result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Contribution) {
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
                comment.Description = "";
                result.CoProcessor.push(comment);
            }
            if (comment.CommentType == commentTypeEnum.Finished || comment.CommentType == commentTypeEnum.Reopen) {
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
                result.Processor.push(comment);
            }

            comment.UserSend = _.find(allUsers, function (user) {
                return user.value === comment.UserSendId;
            });

            if (comment.UserSend == undefined) {
                comment.UserSend = {};
            }

            comment.avatar = comment.UserSend.avatar;

            if (comment.Children) {
                _.each(comment.Children, function (child) {
                    child.UserSend = _.find(allUsers, function (user) {
                        return user.value === child.UserSendId;
                    });

                    if (child.UserSend == undefined) {
                        child.UserSend = {};
                    }
                    child.Content = JSON.parse(child.Content);
                    child.Content.Transfers = [];
                    child.avatar = comment.UserSend.avatar;
                });
            }

            if (i != comments.length - 1)
                comments[i].prev_commentId = comments[i + 1].CommentId;
            else
                comments[i].prev_commentId = 0;

            // 20200310 START compare data
            comment.compare = "../AvatarProfile/compare.png";
            comment.compare_first = "../AvatarProfile/compare_first.png";
            // 20200310 END compare data
        }

        return result;
    }

    //Parse lại comment do break line bị mất
    function parseCommentNew(commentTemplate, processor) {
        var commentsHtml = $.tmpl(commentTemplate, processor);
        for (var i = 0; i < commentsHtml.length; i++) {
            commentsHtml.eq(i).html(_.unescape(commentsHtml[i].innerHTML));
            if (i <= 2) {
                continue;
            }

            if (egov.isMobile) {
                commentsHtml.eq(i).hide();
            }
            else {
                //Nếu có nhiều hơn 3 comment, add thêm vào 1 class dynamicComment làm flag để dễ gọi lúc muốn hiện lên và ẩn đi
                commentsHtml.eq(i).addClass("dynamicComment").hide();
            }
        }

        return commentsHtml;
    }

    /// Tạo danh sách các link để show dialog ở phần quickview
    //  Cho người dùng xem và có thể sửa nội dung
    /// <param name='value'> id của form nội dung văn bản<param>
    /// <param name='name'>Tên của form <param>
    /// <param name='callback'>Hàm call back lại khi click vào tiêu đề<param>
    function bindTitleQuickView(value, name, callback) {
        var template = "<tr><td class='wraptext' nowrap><span>${name}</span></td><td class='wraptext' title='Xem'><a href='#' value='${value}' style='color:blue;'><span class='icon-file-openoffice'></span></a></td></tr>";
        //var template = "<li><a href='#' value='${value}'>${name}</a></li>";
        var result = $.tmpl(template, { value: value, name: name });

        result.find("a").click(function () {
            if (typeof callback === 'function') {
                callback(value);
            }
        });

        return result;
    }

    var commentLength = 100;

    function splitComment(comment) {
        if (comment.length <= commentLength) {
            return comment;
        }

        var char = comment.charAt(commentLength);
        var str = comment.substring(0, commentLength);
        if (char == " ") {
            return str;
        }

        var lastSpaceStr = commentLength - str.lastIndexOf(" ");
        var str2 = comment.substring(commentLength, comment.length);
        var firstSpaceStr2 = str2.indexOf(" ");

        if (firstSpaceStr2 < lastSpaceStr) {
            return comment.substring(0, commentLength + firstSpaceStr2);
        }
        else {
            return comment.substring(0, commentLength - lastSpaceStr);
        }
    };

    //lay gia tri
    function stringTime() {
        var d = new Date();

        var nhour = d.getHours();

        var nmin = d.getMinutes();

        var nsec = d.getSeconds();

        if (nmin <= 9) {
            nmin = "0" + nmin;
        }

        if (nsec <= 9) {
            nsec = "0" + nsec;
        }

        if (nhour <= 9) {
            nhour = "0" + nhour;
        }

        var stringT = " " + nhour + ":" + nmin + ":" + nsec;

        return stringT;
    }

    function removeAllBlankOrNull(JsonObj) {
        $.each(JsonObj, function (key, value) {
            if (value === "" || value === null) {
                delete JsonObj[key];
            } else if (typeof (value) === "object") {
                JsonObj[key] = removeAllBlankOrNull(value);
            }
        });
        return JsonObj;
    }

    function finish(documentCopyId, storeId, callbackSuccess, callbackError) {
        /// <summary>
        ///  Ket thuc văn bản
        ///</summary>
        /// <param name="documentCopyId" type="int">Id document copy.</param>
        /// <param name="callbackSuccess" type="function">Ham thuc thi khi thanh cong</param>
        /// <param name="callbackError" type="function">Ham thuc thi khi loi</param>
        egov.request.finish({
            data: {
                documentCopyId: documentCopyId,
                storePrivateId: storeId
            },
            success: function (result) {
                if (result.error) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.document.finish.error);
                    egov.callback(callbackError);
                } else {
                    egov.callback(callbackSuccess);
                }
            },
            error: function () {
                egov.callback(callbackError);
            }
        });
    }

    function stripHtml(html) {
        var tmp = document.createElement("DIV");
        tmp.innerHTML = html;
        return tmp.textContent || tmp.innerText || "";
    }

    function transpose(a) {

        // Calculate the width and height of the Array
        var w = a.length || 0;
        var h = a[0] instanceof Array ? a[0].length : 0;

        // In case it is a zero matrix, no transpose routine needed.
        if (h === 0 || w === 0) {
            return [];
        }

        /**
         * @var {Number} i Counter
         * @var {Number} j Counter
         * @var {Array} t Transposed data is stored in this array.
         */
        var i, j, t = [];

        // Loop through every item in the outer array (height)
        for (i = 0; i < h; i++) {

            // Insert a new row (array)
            t[i] = [];

            // Loop through every item per item in outer array (width)
            for (j = 0; j < w; j++) {

                // Save transposed data.
                t[i][j] = a[j][i];
            }
        }

        return t;
    }

    //#endregion

    return Document;
});
var handleHotBeforeOnCellMouseDown = function (event, coords, element) {
    if (coords.row < 0) {
        event.stopImmediatePropagation();
    }
};
handleHotAfterChange = function (formTable, id) {
    formTable.updateSettings({
        height: $(`${id} div#divContent .ht_master .wtHider`).height()
    });
    $(`${that.el} div#divContent .ht_master .wtHolder`)
        .css("height", $(`${id} div#divContent .ht_master .wtHider`).height() + 20);
}
