define(function () {
    "use strict";

    var _resource,
        DocumentModel,
        _template;

    _resource = egov.resources.document;
    DocumentModel = egov.models.document;
    _template = egov.template.document;

    //#region Document View

    var Document = Backbone.View.extend({
        //Kiểm tra có phải tạo mơi văn bản hồ sơ hay không
        isCreate: false,

        //Kiểm tra id của categoryBusiness
        categoryBusinessId: 0,

        events: {
            'click .btnFinish': '_finish',
            'click .btnUndoTransfer': '_undoTransfer',
            'click .btn-transfer': "confirmTransfer",
            'click .target-comment-desc': '_toggleTargetCommentDetail',
            'click .viewAllComment': '_viewAllComment',
            'click .btnTransfer': '_actionTranfer',
        },

        initialize: function (option) {
            /// <summary>
            /// Khởi tạo
            /// </summary>
            var that = this;
            this.el = option.el;
            this.id = option.id;
            this.documentInfo = option.documentInfo;
            if (this.documentInfo) {
                if (!(this.documentInfo instanceof Backbone.Model)) {
                    this.documentInfo = new Backbone.Model(this.documentInfo);
                }
                this.model = new DocumentModel(this.documentInfo.toJSON());
            } else {
                this.model = new DocumentModel;
            }

            that.isCreate = that.isCreate || option.isCreate;

            //this.registerToGlobal();

            var template = that.isCreate ? _template.mobileCreate : _template.mobileInfo;
            require([template], function (DocumentTemplate) {
                that.template = DocumentTemplate;

                that.$el.html($.tmpl(DocumentTemplate, that.model.toJSON()));

                that.showProcessing();
                that.isCreate ? that._openCreateDocument() : that._openEditDocument(that.id);
            });
        },

        //tiennvg them check transfer
        confirmTransfer: function (e) {
            if (window.eGovApp) {
                var itemIds = [];
                var dsAttachments = this.model.get('Attachments');
                $.each(dsAttachments, function (i, items) {
                    itemIds.push(items.Id);
                });
                //link attachment mobile has format: /Attachment/DownloadAttachment/7695
                var response = eGovApp.confirmAttachment(itemIds.toString());
                var that = this;
                if (response) {
                    egov.callback(function () {
                        var objectResponse = JSON.parse(response);
                        $.each(objectResponse, function (i, item) {
                            that.attachments.modifiedFiles[item.Id] = item.base64;
                        });
                    });
                }
            }
        },
        _actionTranfer: function () {
            if ($(".mdl-menu__container").hasClass('is-visible')) {
                $(".borcustom").addClass('is-visible');
            } else {
                $(".borcustom").removeClass('is-visible');
            }
        },
        _finish: function (e) {
            /// <summary>
            /// Kết thúc xử lý
            /// </summary>
            if (this.checkDisable(e)) {
                return;
            }
            e.preventDefault();
            var that = this;

            egov.dialog.confirm({
                title: "Kết thúc VB",
                message: "Bạn có chắc muốn kết thúc văn bản này không?",
                confirm: function () {
                    if (that.model.get("DocumentCopyId") != 0) {
                        that._doFinish();
                        return;
                    }
                }
            });
        },

        _doFinish: function () {
            /// <summary>
            /// Kết thúc xử lý
            /// </summary>
            var that = this;
            var comment = "(Kết thúc văn bản từ thiết bị di động)";
            var token = $("input[name='__RequestVerificationToken']", "#FinishUpdateFinish").val();
            var id = that.model.get("DocumentCopyId");
            $.ajax({
                url: '/Finish/UpdateFinish',
                type: "Post",
                data: { documentCopyId: id, comment: comment, __RequestVerificationToken: token },
                success: function (response) {
                    if (response.success) {
                        // Bỏ văn bản ở box hiện tại
                        that._removeDocumentInList();
                        that._backToList();
                    }
                }
            });
        },

        _undoTransfer: function (e) {
            var that = this;
            if (this.checkDisable(e)) {
                return;
            }

            e.preventDefault();
            var that = this;

            egov.dialog.confirm({
                title: "Lấy lại VB",
                message: "Bạn có chắc muốn lấy lại văn bản này không?",
                confirm: function () {
                    if (that.model.get("DocumentCopyId") != 0) {
                        that.undoTransfer();
                        return;
                    }
                }
            });
        },

        undoTransfer: function () {
            var that = this;
            var dateCreated = that.documentInfo.get("DateReceived");

            egov.request.undoTransfering({
                data: { documentCopyId: that.id, dateCreated: dateCreated },
                beforeSend: function () {
                    egov.mobile.showProcessBar();
                },
                success: function (data) {
                    if (data.error) {
                        egov.mobile.showStatus("Lấy lại văn bản lỗi.");
                        egov.mobile.hideProcessBar();
                        return;
                    }

                    that._removeDocumentInList();
                    egov.mobile.showStatus(data.success);
                },
                error: function (xhr) {
                    egov.mobile.showStatus("Lấy lại văn bản lỗi.");
                },
                complete: function () {
                    egov.mobile.hideDetailPage();
                    egov.mobile.hideProcessBar();
                }
            });
        },

        _removeDocumentInList: function () {
            var id = this.model.get("DocumentCopyId");
            $("#documentFrame .list-document li[data-id='" + id + "']").remove();
        },

        // ------------------------------------------------------------------------------------------- //

        _openEditDocument: function (docCopyId) {
            /// <summary>
            /// Mở văn bản theo id
            /// </summary>
            ///<param name="docCopyId">Id của văn bản</param>
            var that = this;
            var storePrivateId = that.model == null ? null : that.model.get("StorePrivateId");
            egov.request.getDocumentInfoForMobile({
                data: {
                    id: docCopyId,
                    storePrivateId: storePrivateId
                },
                success: function (result) {
                    if (result) {
                        if (result.error) {
                            require([egov.template.document.mobileError], function (ErrorTemp) {
                                var $error = $.tmpl(ErrorTemp, result);
                                that.$el.html($error);
                            });

                            return;
                        }
                        that.model = new DocumentModel(result);
                        that.model.set("StorePrivateId", storePrivateId);
                        var eform = result.Note? result.Note : result.JsonForm;
                        var document = eform ? JSON.parse(eform) : [];
                        that.jsonForm = document;
                        that.configForm = result.configForm ? JSON.parse(result.configForm) : {};
                        that.formCode = result.FormCode;
                        that._reRenderInfo();
                    }
                },
                error: function (xhr) {
                    $(egov.views.home.tree.documentList).addClass('display');
                    that._showStatus(egov.resources.document.openError);
                },
                complete: function () {
                    that.hideProcessing();
                }
            });
        },

        _openCreateDocument: function () {
            /// <summary>
            /// Mở văn bản theo id
            /// </summary>
            ///<param name="docCopyId">Id của văn bản</param>
            var that = this;
            var doctypeId = that.model == null ? null : that.model.get("DocTypeId");
            egov.request.getDocumentInfoForCreateMobile({
                data: {
                    id: doctypeId
                },
                success: function (result) {
                    if (result) {
                        if (result.error) {
                            require([egov.template.document.mobileError], function (ErrorTemp) {
                                var $error = $.tmpl(ErrorTemp, result);
                                that.$el.html($error);
                            });

                            return;
                        }

                        that.model = new DocumentModel(result);
                        var eform = result.JsonForm;
                        var document = eform ? JSON.parse(eform) : [];
                        that.jsonForm = document
                        that.configForm = result.configForm ? JSON.parse(result.configForm) : {};
                        that.formCode = result.formCode;

                        that._reRenderInfo();
                    }
                },
                error: function (xhr) {
                    $(egov.views.home.tree.documentList).addClass('display');
                    that._showStatus(egov.resources.document.openError);
                },
                complete: function () {
                    that.hideProcessing();
                }
            });
        },

        // Hiển thị phòng ban
        _renderInOutPlace: function (allDepts) {
            var that = this;
            egov.request.getAllDepartment({
                success: function (data) {
                    allDepts = _.pluck(data, "label");
                    egov.request.getDepartmentsByUser({
                        success: function (currentDepts) {
                            var currentInOutPlace = that.model.get('InOutPlace');

                            if (!currentInOutPlace || currentInOutPlace == "") {
                                currentInOutPlace = currentDepts ? currentDepts[0] : allDepts[0];
                            }

                            $.tmpl("<option value='${$data}'>${}</option>", allDepts).appendTo(that.$("#chooseInOutPlace"));

                            that.$("#InOutPlace option").each(function () {
                                if ($(this).text() == currentInOutPlace) {
                                    $(this).attr("selected", "selected");
                                }
                            });

                            that.model.set('InOutPlace', currentInOutPlace);
                        }
                    });
                }
            });
            require(["mobiScroll"], function (mobiscroll) {
                that.$("#chooseInOutPlace").mobiscroll().select({
                    placeholder: 'Please Select...',
                    display: 'bottom',
                    filter: true
                });
                that.$('#formEdit').mobiscroll().form();
                that.$("#formEdit").trigger('mbsc-enhance');

                // 20200226 VuHQ bị ẩn btn [Thêm nội dung] khi đóng và reclick [Tạo mới báo cáo]
                that.$('#addItemFormDiv').trigger('mbsc-enhance');
            });
        },

        _reRenderInfo: function () {
            var that = this;
            this.$(".compendium").text(this.model.get("Compendium"));
            this.$("form.document-info").attr("data-id", this.model.get("DocumentCopyId"));
            this.$Comment = this.$("#Comment");

            this._initDocument();

            this.$("#Comment").on("changeContent", function () {
                that._renderLayout();
            });

            this.$("#Comment").on("change", function () {
                that.$("#Comment").trigger("changeContent");
            });

            $(document).on('resize', function (e) {
                this._renderLayout();
            });

            egov.mobile.upgradeMaterial(that.$(".mdl-progress, .mdl-button, .mdl-menu"));
            that.hideProcessing();
        },

        _initDocument: function () {

            var that = this;

            this.$el.parent().addClass("display");

            this._renderPermission(this.model.get("DocumentPermissions"));

            this._renderLayout();

            this._renderComments();

            this._renderAttachment();

            //this._renderForm();

            //this._renderFormMobile() ;
            this._renderFormInMobile()

            this._renderInOutPlace();

            this.$el.bindResources();

            this._renderCommonComments();

            this.$('form').attr('id', this.isCreate ? "CreateForm" : "EditForm");

            egov.mobile.current.document = that;
        },

        registerToGlobal: function () {
            var top = window.top;
            if (_.isFunction(top.openFilePreview)) {
                return;
            }

            //top.openFilePreview = function (documentId, attachmentId) {
            //    window.open(
            //     'AttachmentPreview/Index?id=' + documentId + '&currentId=' + attachmentId,
            //     '_blank'
            //   );
            //}
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

            // Lấy giá trị các thuộc tính từ control tương ứng.
            // Mặc định đặt Id của Control = tên thuộc tính để lấy.
            for (var attr in that.model.attributes) {
                control = that.$el.find('[name="' + attr + '"]');
                if (control.length > 0 && (control.is("input") || control.is("select") || control.is("textarea"))) {
                    val = control.val();
                    if (val === '' && that.model.get(attr) == null) {
                        continue;
                    }
                    that.model.set(attr, val);
                }
            }

            that.model.set('IsSuccess', null);
            that.model.set('IsAcknowledged', false);

            result = that.model.toJSON();
            result.CommentList = [];
            result.Contents = [];

            // 20200113 VuHQ START comment vì không dùng theo DungNT
            //_.each(result.DocumentContents, function (content) {
            //    if (content.FormTypeId === egov.enum.formType.html) {
            //        content.Content = escape(that.$formContainer.val());
            //        result.Contents.push(JSON.stringify(content));
            //    }
            //});
            // 20200113 VuHQ END comment vì không dùng theo DungNT

            // 20200113 VuHQ START I'm here
            var keys = Object.keys(that.configForm);
            var cellData = '';

            var datas = null;
            var controls = null;

            for (var i = 0; i < keys.length; i++) {
                var item = that.configForm[keys[i]];
                var fieldNameAscii = keys[i].split("!!")[0];
                controls = that.$el.find('[name="' + fieldNameAscii + '"]');
                if (i == 0 && controls.length > 0) {
                    datas = new Array(controls.length);
                    for (var j = 0; j < controls.length; j++) {
                        datas[j] = {};
                    }
                }
                    
                for (var j = 0; j < controls.length; j++) {
                    control = controls[j];
                    if (control.tagName.toLowerCase() == "input" || control.tagName.toLowerCase() == "select") {
                        val = control.value;
                        if (control.type.toLowerCase() == "checkbox") {
                            datas[j][fieldNameAscii] = control.checked;
                        }
                        else
                            datas[j][fieldNameAscii] = val;
                    }
                }
            }

            var allCatalogValues = _.uniq(that.$formTemplate.allCatalogValues, function (x) {
                return x.CatalogValueId;
            });

            var j;
            _.each(datas, function (dataFormRow) {
                j = 0;
                Object.keys(dataFormRow).forEach(function (key) {
                    var catalogValues = allCatalogValues.filter(d => d.Value == dataFormRow[key]);
                    if (catalogValues.length > 0 && catalogValues[0].CatalogKey != null)
                        dataFormRow[key] = catalogValues[0].CatalogKey;
                    j++;
                });
            });

            result.Note = JSON.stringify(datas);

            result.NoteCSV = keys.join(",") + "\n";
            datas.forEach(item => {
                for (var i = 0; i < keys.length; i++) {
                    if (i > 0) {
                        result.NoteCSV += ",";
                    }

                    result.NoteCSV += typeof item[keys[i]] === "string" && item[keys[i]].includes(",") ? `"${item[keys[i]]}"` : item[keys[i]];
                }
                result.NoteCSV += "\n";
            });

            result.ProcessInfo = that.formCode;

            _.each(result.DocumentContents, function (content) {
                content.Content = JSON.stringify(datas);
                content.ContentUrl = String.format("{0}{1}_saved.xlsx", egov.setting.eform.forderReport, content.Url);
                result.Contents.push(JSON.stringify(content));
            });

            result.DocumentContents = [];
            // 20200113 VuHQ END I'm here

            result.DocumentContents = [];
            result.RelationModels = result.Relations;
            result.Compendium = escape(result.Compendium);
            result.Comments.Content = result.Comment;

            result.Fees = [];
            result.Papers = [];
            result.Supplementary = "";
            result.HasDateOverdue = false;
            result.DateOverdue = null;
            result.DateReceived = null;
            result.DateModified = null;
            result.DateResult = null;
            result.Attachments = null;
            return result;
        },

        hasChange: function () {
            return false;
        },

        showProcessing: function () {
            egov.mobile.showProcessBar();
        },

        hideProcessing: function () {
            egov.mobile.hideProcessBar();
        },

        //#region Document functions

        _transfer: function (actionId) {
            /// <summary>
            /// Bàn giao văn bản theo hướng chuyển.
            /// </summary>
            /// <param name="action" type="object">Hướng chuyển.</param>
            var that = this,
                comment,
                transferAction;

            comment = this.$Comment.val();
            transferAction = _.find(this.actionList, function (action) {
                return action.id === actionId;
            });

            if (!transferAction) {
                return;
            }

            // Mở form bàn giao
            require(['transferMobile'], function (Transfer) {
                if (egov.transferForm === undefined) {
                    egov.transferForm = new Transfer;
                }
                egov.transferForm.render({
                    action: transferAction,
                    document: that
                });
            });
        },

        //#endregion

        //#region Private Methods

        _reloadDocumentTree: function (documentCopyId) {
            egov.views.home.tree.removeDocuments(documentCopyId);

            //// reload lại cây văn bản
            egov.views.home.tree.reloadSelectedNode();
        },

        _renderComments: function () {
            /// <summary>
            /// Hiển thị các ý kiến của document.
            /// </summary>
            var that = this;
            if (that.isCreate) {
                that.$('#wrapComments').hide();
                return;
            }

            var comments = parseComment(that.model.get('CommentList'), egov.setting.allUsers);

            require([egov.template.document.commentMobile], function (CommentMobileTemplate) {
                that.$('#commentList').html($.tmpl(CommentMobileTemplate, comments));

                // Hiển thị nút click xem tất cả
                if (comments.length > 3) {
                    that.$('.viewAllComment').removeClass("hidden");
                }

                // Tính width của comment
                that.$('#commentList .comment-content-wrap').css("maxWidth", egov.screenSize.contentMaxW);
            });
        },

        _toggleTargetCommentDetail: function (e) {
            var target = $(e.target).closest('.target-comment-desc');
            target.next().toggleClass('hidden');
            e.preventDefault();
        },

        _viewAllComment: function (e) {
            this.$('#commentList .comment-item').removeClass('hidden');
            this.$('.viewAllComment').remove();
            e.preventDefault();
        },

        _renderPermission: function (permission) {
            /// <summary>
            /// Hiển thị toolbar của document
            /// </summary>
            var egovPermission = egov.enum.permission,
                that = this;

            var userCurrentId = that.documentInfo.get("UserCurrentId");
            var status = that.documentInfo.get("Status");
            var isViewed = that.documentInfo.get("Viewed") == 0;

            that.hasLayLaiVanBan = that.documentInfo && userCurrentId !== egov.setting.user.userId && status == 2;

            if (that.hasLayLaiVanBan) {
                that.$(".btnUndoTransfer").removeClass("hidden");
            };

            if (that.documentInfo && status == 2 && userCurrentId == egov.setting.user.userId) {
                that.$(".btnFinish").removeClass("hidden");
            }

            //Comment mobile
            that.hasBanGiao = that.isCreate || containFlags(permission, egovPermission.bangiao);
            if (that.hasBanGiao) {
                that.$(".document-transfer").show();
                that._showTransferActions();
                return;
            }
        },

        // --------------------------- // 

        _renderLayout: function () {
            var screenHeight = window.innerHeight; // screen.height;
            var headerHeight = 65; //header:64
            var footer = 75;

            $(".documents-detail").height(screenHeight - headerHeight);

            var $documentContent = this.$(".document-content");
            $documentContent.height(screenHeight - headerHeight - footer);
            

            var $documentForm = this.$(".document-form");
            var $documentTranfer = this.$(".document-transfer");

            //if (this.$Comment[0]) {
            //    this.$Comment.height(this.$Comment[0].scrollHeight);
            //}

            if ($documentTranfer.is(":hidden")) {
                $documentForm.height("100%");
            } else {
                $documentForm.height(screenHeight - headerHeight - $documentTranfer.height());
            }
        },

        renderTemplate: function () {
            var config = {
                hovatenchuho: { type: "string", name: "Tên chủ hộ" },
                hongheo: { type: "bool", name: "Là hộ nghèo" },
                chatluongnuocsinhhoatdangsudung_nuochopvesinh: { type: "bool", name: "Sử dụng nước hợp vệ sinh" },
                chatluongnuocsinhhoatdangsudung_nuocsach: { type: "bool", name: " Sử dụng nước sạch" },
                nguoncapnuoc_congtrinhcnnl: { type: "bool", name: "Nguồn cấp nước nhỏ lẻ" },
                nguoncapnuoc_congtrinhcntt: { type: "bool", name: " Nguồn cấp nước tập trung" },
            }
            var that = this;
            var jsonForm = this.jsonForm;
            var tmpl = '<div class="mbsc-form-group form-group-template">'

            var keys = Object.keys(config);

            for (var i = 0; i < keys.length; i++) {
                var item = config[keys[i]];
                if (item.type == "bool") {
                    tmpl += ' <label class="mbsc-switch-success">' + item.name +
                                    '<input type="checkbox" data-role="switch" name="' + keys[i] + '" {{if ' + keys[i] + ' == "1"}} checked {{/if}}></label>'
                } else if (item.type == "string") {
                    tmpl += '<label for="' + keys[i] + '">' + item.name +
                                   '<input id="' + keys[i] + '" type="text" placeholder="' + item.name + '" value="${' + keys[i] + '}" />\
                               </label>'
                }
            }
            tmpl += "</div>"
            var template = tmpl;
            for (var i = 0; i < jsonForm.length; i++) {
                $.template("FormContentTemplate", template);
                that.$('#formContentmbsc').append($.tmpl("FormContentTemplate", jsonForm[i]));
            }
        },

        _renderFormInMobile: function () {
            // VuHQ marked
            var that = this;
            var model = this.documentInfo.attributes;
            if (model && model.CategoryBusinessId == 8) {
                if (model.DocumentId != undefined) {
                    $.ajax({
                        type: "POST",
                        //async: false,
                        url: "/Document/GetContentDocument",
                        data: { 'documentId': model.DocumentId },
                        success: function (response) {
                            if (response.Success) {
                                var content = response.Content || "";
                                var input = document.createElement("textarea");
                                var id = `${that.id}_documentContent`;
                                input.style = "width: 100%";
                                input.id = id;
                                input.rows = "800";
                                var div = $(`#${that.id} #divContentmobile #formContentmbsc`);
                                div.append(input);
                                CKEDITOR.editorConfig = function (config) {
                                    config.language = 'vi-vn';
                                    config.uiColor = '#F7B42C';
                                    config.height = 200;
                                    config.toolbarCanCollapse = true;
                                    config.removePlugins = 'image,forms';
                                    config.extraPlugins = 'showborders';
                                };
                                CKEDITOR.replace(id, {
                                    height: 400
                                });
                                CKEDITOR.instances[id].setData(content);
                            }
                        }
                    });
                }
        } else {
                require(["formTemplate"], function (formTemplate) {
                    that.$formTemplate = new formTemplate({
                        models: that.jsonForm,
                        allCatalogValues: [],
                        config: that.configForm,
                        document: that,
                        isCreate: that.isCreate,
                        callback: that._renderThemeMobile
                    });
                });
               
            }
            require(["mobiScroll"], function (mobiscroll) {
                mobiscroll.settings = {
                    theme: 'mobiscroll-dark'
                    //theme: 'ios'
                };

                that.$('#formContentmbsc').trigger('mbsc-enhance');
            });
           
        },

        _renderThemeMobile: function () {
            var that = this;
            require(["mobiScroll"], function (mobiscroll) {
                mobiscroll.settings = {
                    theme: 'mobiscroll-dark'
                };

                that.$('#formContentmbsc').trigger('mbsc-enhance');
            });
        },

        _renderFormMobile: function () {
            var that = this;
            var formObject = {};
            var configKey = 1;
            var configValue = 3;


            var formJson = this.jsonForm;
            var conf = []
            for (var i = 0; i < formJson.length; i++) {
                var keys = Object.keys(formJson[i]);

                var name = "row" + i;
                formObject[name] = {
                    type: 'string',
                    title: formJson[i][keys[configKey]],
                    "default": formJson[i][keys[configValue]],
                    "enum": ["0", "1"],
                    required: true
                }
                var mapkey = {
                    "key": name,
                    "titleMap": {
                        "0": "Không",
                        "1": "Nước hợp vệ sinh",
                    }
                }
                conf.push(mapkey)
            }

            require(["jsonform"], function () {
                that.formBuildJson = that.$('#divContentmobile').jsonForm({
                    schema: formObject,
                    "form": conf
                });

                that.$divContentmobile = that.$('#divContentmobile');
            });
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
                    "key": form.Url,
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
                "type": "mobile",
                "width": "100%",
                "lang": "vi_VN",
                "documentType": "spreadsheet",
                "editorConfig": {
                    "mode": "edit",
                    "callbackUrl": egov.setting.eform.domain + "webeditor.ashx",
                    "customization": {
                        "compactToolbar": true,
                        "autosave": false,
                        "forcesave": false,
                        "chat": false,
                        "logo": {
                            "image": "/Content/logo.png"
                        },
                        "customer": {
                            "address": "My City, 123a-45",
                            "info": "Some additional information",
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
                    hasPermission: that.model.get("UserCurrentId") == egov.setting.userId,// Chỉ có quyền remove, phục hồi khi là người đang giữ công văn
                    model: attacts,
                    el: that.$('#wrapAttachment'),
                    documentId: that.model.get("DocumentId"),
                    documentCopyId: that.model.get("DocumentCopyId"),
                    storePrivateId: that.model == undefined ? null : that.model.get("StorePrivateId")
                });
            });

            that._insertAttachment();
        },

        _renderCommonComments: function () {
            /// <summary>
            /// Hiển thị autocomplete ý kiến thường dùng
            /// </summary>
            var that = this;
            var comments = egov.setting.commentComments;
            if (comments.length === 0) {
                return;
            }

            _.each(comments, function (c) {
                c.Content = unescape(c.Content);
            });

            var savedComments = that.$("#commonCommentList");
            savedComments.empty();
            savedComments.append($.tmpl('<li class="mdl-menu__item wraptext">${Content}</li>', comments));
            savedComments.find("li").click(function (e) {
                that.$("#Comment").val($(this).text());
                that.$("#Comment").trigger("changeContent");
            });
        },

        _showTransferActions: function () {
            var that = this;
            that.actionElement = that.$('.transferActions');
            var success = function (result) {
                if (result) {
                    if (result.error) {
                        return null;
                    }

                    that._showActionResult(result);
                }
            };

            if (that.isCreate) {
                egov.request.getDocumentCreateAction({
                    data: {
                        documentTypeId: that.model.get("DocTypeId"),
                    },
                    success: function (result) {
                        success(result);
                    },
                    complete: function () {
                        that.actionElement.find('.loading').parent().remove();
                    }
                });
            } else {
                egov.request.getDocumentEditAction({
                    data: { documentCopyId: that.id },
                    success: function (result) {
                        success(result);
                    },
                    complete: function () {
                        that.actionElement.find('.loading').parent().remove();
                    }
                });
            }
        },

        _showActionResult: function (result) {
            if (result.length === 0) {
                this.actionElement.html($('<li>').text(egov.resources.document.toolbar.noaction));
                return;
            }
            this.actionList = _.sortBy(result, function (action) { return action.priority; });
            this._insertTransferActions();
        },

        _insertTransferActions: function () {
            /// <summary>
            /// Hiển thị danh sách các hướng chuyển đối với văn bản hiện tại.
            /// </summary>
            var that = this,
                leng,
                action,
                transferIcon,
                actionEle,
                actionSpecials,
                nextAction,
                transferPlan;

            this.actionElement.empty();

            actionSpecials = egov.enum.actionSpecial;
            leng = this.actionList.length;

            // Thêm các hướng chuyển của văn bản.
            for (var i = 0; i < leng; i++) {
                action = this.actionList[i];

                actionEle = this.actionElement;
                actionEle.append(parseListItem(action.name, action.id, transferIcon, function (key, e) {
                    if (key === actionSpecials.tiepNhanHoSo.name
                        || key === actionSpecials.tiepNhanHoSoVaTiepTuc.name
                        || key === actionSpecials.chuyenNguoiCoQuyenDongGopYKien.name
                        || key === actionSpecials.chuyenNguoiGui.name
                        || key === actionSpecials.chuyenNguoiKhoiTao.name) {
                        // Hướng chuyển đặc biệt
                        if (key === actionSpecials.tiepNhanHoSo.name
                            || key === actionSpecials.tiepNhanHoSoVaTiepTuc.name) {
                            // Chuyển văn bản khởi tạo
                            that._transferSpecialCreate(key);
                        } else {
                            // Chuyển văn bản chỉnh sửa
                            that._transfer(key);
                        }
                    } else if (key === actionSpecials.luuSoVaPhatHanhNoiBo.name) {
                        // Phát hành nội bộ
                        that._privatePublish(true);
                    } else if (key === actionSpecials.luuSoVaPhatHanhRaNgoai.name) {
                        // Phát hành
                        that._publishment();
                    } else if (key === actionSpecials.chuyenYKienDongGopVbDxl.name
                              || key === actionSpecials.chuyenYKienDongGopVbXinYKien.name) {
                        // Chuyển ý kiến đóng góp
                        that._transferChuyenYKien(key);
                    } else {
                        // bàn giao văn bản theo hướng chuyển thông thường
                        that._transfer(key);
                    }

                    e.preventDefault();
                    return;
                }, action.isAllow));
            }

            egov.mobile.upgradeMaterial(that.$(".mdl-progress, .mdl-button, .mdl-menu"));
        },

        _insertAttachment: function () {
            /// <summary>
            /// Thêm tài liệu đính kèm
            /// </summary>
            var that = this;
            var newAttachment;
            var existFile;

            this.$('.fileupload').fileupload({
                dataType: 'json',
                add: function (e, data) {
                    var file = data.files[0];
                    var msg = "";

                    if (egov.setting.maxFileSize && file.size > egov.setting.maxFileSize) {
                        msg += file.name + ": " + _resource.file.lenghtIsNotAllow + _resource.file.maxLength + egov.setting.maxFileSize / 1048576 + "Mb";
                    } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test('.' + file.name.split('.').pop()))) {
                        msg += file.name + ": " + _resource.file.typeIsNotAllow;
                    } else {
                        newAttachment = new egov.models.attachment({
                            Id: 0,
                            Name: file.name,
                            Size: file.size,
                            Extension: '.' + file.name.split('.').pop(),
                            Versions: [{
                                Version: 1,
                                User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                            }],
                            isNew: true
                        });

                        that.attachments.model.add(newAttachment);
                    }
                    if (msg !== "") {
                        that._showStatus(msg);
                    } else {
                        data.submit();
                    }
                },
                start: function () {
                    that.showProcessing();
                },
                stop: function () {
                    that.hideProcessing();
                },
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();

                            that._showStatus(file.name + ": " + file.error);
                        } else {
                            var newFile = that.attachments.model.detect(function (f) {
                                return f.get('isNew') && f.get('Name') === file.name;
                            });
                            if (newFile) {
                                newFile.set('Id', file.key);
                            }
                        }
                    });
                },
                fail: function (e, data) {
                    that._showStatus(_resource.file.errorUpload);
                    $(data.id).remove();
                }
            });
        },

        _changeStoreId: function () {
            // <summary>
            // Thay đổi sổ hồ sơ
            //</summary>

            if (this.model.get("CategoryBusinessId") != 1) {
                this.isVanBanDen = false;
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

        _getInOutCode: function (callback) {
            this._showDocCodes(true);
        },

        _getDocCodes: function () {
            this._showDocCodes(false);
        },

        checkDisable: function (event) {
            /// <summary>
            /// Kiểm tra xem target click có click được không.
            /// </summary>
            /// <returns type="bool"></returns>
            return $(event.target).hasClass("disabled") || $(event.target).parents().hasClass("disabled");
        },

        _showDocCodes: function (isVbDen) {
            var that = this,
                storeId = that.$("#StoreId").val(),
                categoryId = that.$("#CategoryId").val(),
                codeElement,
                codeRequest;

            codeElement = isVbDen ? that.$("input[name='InOutCode']") : that.$("input[name='DocCode']");
            codeRequest = isVbDen ? egov.request.GetInOutCode : egov.request.GetDocCodes;
            that.$codeElement = codeElement;

            if (storeId == "" || storeId == null) {
                return;
            }

            if (that.stores) {
                var existStore = _.find(that.stores, function (item) {
                    return item.StoreId == storeId && item.CategoryId == categoryId;
                });

                if (existStore && existStore.Codes != null && existStore.Codes.length > 0) {
                    codeElement.val(existStore.Codes[0]);
                    return;
                }
            }

            codeRequest({
                data: {
                    storeId: storeId,
                    categoryId: categoryId
                },
                success: function (data) {
                    if (!that.stores) {
                        that.stores = [];
                    }

                    that.stores.push({
                        StoreId: storeId,
                        CategoryId: categoryId,
                        Codes: data
                    });

                    codeElement.val(data[0]);
                }
            });
        },

        _showStatus: function (message) {
            egov.mobile.showStatus(message);
        },

        _backToList: function () {
            this.$el.remove();
            egov.mobile.hideDetailPage();
            egov.mobile.hideProcessBar();
        }

        //#endregion
    });

    //#endregion

    //#region Private Methods

    // Các loại ý kiến xử lý
    var commentType = { Common: 1, Consulted: 2, Contribution: 3, Supplementary: 4, Signed: 5, Success: 6, Finished: 7, Reopen: 8 };

    function parseComment(comments, allUsers) {
        var result = [];
        var content;
        var commentTypeEnum = commentType;
        var idx = 1;
        _.each(comments, function (comment) {
            if (typeof comment.Content == "string") {
                comment.Content = comment.Content.split('\\n').join('<br />');
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
                    comment.Description = String.format("Tới {0}", transfers[0].label);
                    if (transfers.length > 1) {
                        var otherNumber = 0;
                        for (var j = 1; j < transfers.length; j++) {
                            var peoples = transfers[j].label;
                            otherNumber += peoples.split(",").length;
                        }

                        comment.Description += String.format(" và {0} ĐXL", otherNumber);
                    }
                } else {
                    comment.Description = "Hệ thống gửi";
                }
            }

            if (comment.CommentType == commentTypeEnum.Consulted) {
                content = JSON.parse(comment.Content);
                comment.Content = content;
                comment.Content.Transfers = [];
                comment.Description = "Trả lời đồng xử lý";
            }

            if (comment.CommentType == commentTypeEnum.Contribution) {
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
                comment.Description = "Trả lời xin ý kiến";
            }

            if (comment.CommentType == commentTypeEnum.Finished || comment.CommentType == commentTypeEnum.Reopen) {
                comment.Content = JSON.parse(comment.Content);
                comment.Content.Transfers = [];
            }

            comment.UserSend = _.find(allUsers, function (user) {
                return user.value === comment.UserSendId;
            }) || {};
            comment.avatar = comment.UserSend.avatar;

            comment.Date = Date.parseFromIsoString(comment.DateCreated).relativeDate();
            comment.stt = idx++;
            result.push(comment);
        });
        console.log(result);
        return result;
    }

    function containFlags(combined, checkagainst) {
        ///<summary> Kiểm tra xem phần tử combined có chứa checkagainst hay không</summary>
        return ((combined & checkagainst) === checkagainst);
    }

    function parseListItem(name, value, icon, callback, isAllow) {
        var result;
        if (typeof value == "string" && "luusonoiboluusovaphathanhnoiboluusovaphathanhrangoai".indexOf(value.toString().toLowerCase()) > -1) {
            return;
        }
        result = $.tmpl('<li class="mdl-menu__item" value="${value}">${name}</li>', { value: value, name: name, icon: icon });

        if (!isAllow) {
            result.attr('disabled', 'disabled');
        }

        result.on("click", function (e) {
            if (typeof callback === "function") {
                callback(value, e);
            }
        });

        return result;
    }

    function remove_unicode(str) {
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");

        str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1- 
        str = str.replace(/^\-+|\-+$/g, "");

        return str;
    }

    //#endregion

    return Document;
});