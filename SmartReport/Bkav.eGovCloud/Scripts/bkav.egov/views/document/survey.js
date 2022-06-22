define([egov.template.document.surveyContent], function (Template) {
    "use strict";

    //#region Models

    //#endregion

    var _resource = egov.resources.document.transfer;
    var ID_REGEXP = /[a-zA-Z_0-9{\*\/\<\>\=\!\$\.\-\u00A2-\uFFFF]/;
    var operations = [
        {
            value: "and",
            title: "logical 'and' operator",
        },
        {
            value: "&&",
            title: "logical 'and' operator",
        },
        {
            value: "or",
            title: "logical 'or' operator",
        },
        {
            value: "||",
            title: "logical 'or' operator",
        },
        {
            value: "empty",
            title: "returns true if the left operand is empty	{questionName} empty",
        },
        {
            value: "notempty",
            title: "returns true if the left operand is not empty	{questionName} notempty",
        },
        {
            value: "=",
            title: "returns true if two values are equal	{questionName} = 5, {questionName} == 'abc', {questionName} equal 124",
        },
        {
            value: "==",
            title: "returns true if two values are equal	{questionName} = 5, {questionName} == 'abc', {questionName} equal 124",
        },
        {
            value: "equal",
            title: "returns true if two values are equal	{questionName} = 5, {questionName} == 'abc', {questionName} equal 124",
        },
        {
            value: "<>",
            title: "returns true if two values are not equal	{questionName} <> 5, {questionName} != 'abc', {questionName} notequal 124",
        },
        {
            value: "!=",
            title: "returns true if two values are not equal	{questionName} <> 5, {questionName} != 'abc', {questionName} notequal 124",
        },
        {
            value: "notequal",
            title: "returns true if two values are not equal	{questionName} <> 5, {questionName} != 'abc', {questionName} notequal 124",
        },
        {
            value: ">",
            title: "returns true if the left operand greater then the second operand	{questionName} > 2, {questionName} greater 'a'",
        },
        {
            value: "greater",
            title: "returns true if the left operand greater then the second operand	{questionName} > 2, {questionName} greater 'a'",
        },
        {
            value: "<",
            title: "returns true if the left operand less than the second operand	{questionName} < 2, {questionName} less 'a'",
        },
        {
            value: "less",
            title: "returns true if the left operand less than the second operand	{questionName} < 2, {questionName} less 'a'",
        },
        {
            value: ">=",
            title: "returns true if the left operand equal or greater then the second operand	{questionName} >= 2, {questionName} greaterorequal 'a'",
        },
        {
            value: "greaterorequal",
            title: "returns true if the left operand equal or greater then the second operand	{questionName} >= 2, {questionName} greaterorequal 'a'",
        },
        {
            value: "<=",
            title: "returns true if the left operand equal or less than the second operand	{questionName} <= 2, {questionName} lessorequal 'a'",
        },
        {
            value: "lessorequal",
            title: "returns true if the left operand equal or less than the second operand	{questionName} <= 2, {questionName} lessorequal 'a'",
        },
        {
            value: "contains",
            title: "return true if the left operand is an array and it contains a value of the second operand	{questionName} contains 'a'",
        },
        {
            value: "notcontains",
            title: "return true if the left operand is an array and it does not contain a value of the second operand",
        },
        {
            value: "anyof",
            title: "return true if the left operand is an array and it contains any value of the second array operand",
        },
        {
            value: "allof",
            title: "return true if the left operand is an array and it contains all values of the second array operand",
        }
    ];
    var max, max1;

    var DocumentSurvey = Backbone.View.extend({
        className: 'survey-content',
        template: Template,

        events: {
            'click .svd-logic-add-condition-button': '_addCriteria',
            'change input[name=alias]': '_onAliasChange',
            'change #criteria-name': '_onCriteriaNameChange',
            'submit .criteria-advance form': '_addCriteriaAdvance',
            'click .svd-criteria-save-button': '_saveCriteria',
            'click .svd-propertyeditor-condition-item-delete-icon': '_deleteCriteria',
            'click .co-process-user .checkbox': '_uncheckPrivateAnoun',
            'change select[name=operator]': '_onOperatorChange'
        },

        initialize: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>

            var that = this;
            require(['text!templates/document/survey/criteriaRow.html'], function (criteriaRow) { that.criteriaRow = criteriaRow; });
            require(['text!templates/document/survey/textTemplate.html'], function (text) { that.textTemplate = text; });
            require(['text!templates/document/survey/dropdownTemplate.html'], function (dropdown) { that.dropdownTemplate = dropdown; });
            require(['text!templates/document/survey/checkboxTemplate.html'], function (checkbox) { that.checkboxTemplate = checkbox; });
            this.questions = [];
            this.allDepts = egov.allDeps;
            this.allUsers = egov.allUsers;
            this.allPositions = egov.allPositions;
            this.allUserDeps = egov.allUserDeps;

            return this;
        },
        render: function () {
            /// <summary>
            /// Hiển thị form
            /// </summary>
            /// <returns type=""></returns>
            var html = $.tmpl(this.template, { cid: this.cid });
            this.$el.html(html);
            this.$el.bindResources();
            return this;
        },
        destroy: function () {
            /// <summary>
            /// Hủy bỏ các cây văn người dung- phòng ban- chức danh
            /// </summary>
            this.$('#dgUsers, #dgJobtitle, #dgDeptJob').each(function () {
                $(this).customDropdown("destroy");
            });

            this.$el.remove();
        },
        serialize: function () {
            /// <summary>
            /// Trả về không gian user được chọn
            /// </summary>
            this.result = {
                isAllUser: false,
                isAllDept: false,
                isAllJobtitle: false,
                users: [],
                depts: [],
                jobDepts: [],
                jobtitlies: []
            };
            this._getUsersDept();
            this._getJobtitlesDept();
            return this._processResult();
        },
        getDestination: function () {
            /// <summary>
            /// Trả về các không gian user nhận đồng gửi.
            /// </summary>
            /// <returns type=""></returns>
            var that = this;
            var selected = this.serialize();
            var result = [];
            var type = egov.enum.transferType.dongxuly;

            // Trường hợp chọn tất cả người dùng
            if (selected.isAllUser || (selected.isAllDept && selected.isAllJobtitle)) {
                result.push({
                    label: _resource.sendAll,
                    value: 'all',
                    type: type
                });

                return result;
            }

            // Hiển thị các lựa chọn ở chọn user - phòng ban: chọn phòng ban
            selected.depts.forEach(function (dept) {
                result.push({
                    label: dept.data,
                    value: "FilterDepartment-" + dept.value,
                    type: type
                });
            });

            // Hiển thị các lựa chọn ở chọn user - phòng ban: chọn người dùng
            selected.users.forEach(function (user) {
                result.push({
                    label: user.fullname + " - " + user.username,
                    value: "FilterDepartment-user_" + user.value,
                    type: type
                });
            });

            // Chọn tất cả phòng ban
            if (selected.isAllDept) {
                // Trường hợp chọn tất cả chức vụ đã set ở trên
                if (selected.jobtitlies.length > 0) {
                    // Không chọn tất cả chức vụ thì hiển thị danh sách các chức vụ thuộc phòng ban root
                    var dept = " thuộc " + selected.jobDepts[0].data;
                    var text = _.pluck(selected.jobtitlies, 'label').join(', ') + dept;
                    var jobtitleIds = _.pluck(selected.jobtitlies, 'value').join(',');
                    result.push({
                        label: text,
                        value: String.format('JobtitleForDept-{0}_{1}', selected.jobDepts[0].idext, jobtitleIds),
                        type: type
                    });
                }
            }
            else if (selected.isAllJobtitle) {// Chọn tất cả chức vụ
                // Trường hợp chọn tất cả phòng ban đã set ở trên
                if (!selected.isAllDept) {
                    selected.jobDepts.forEach(function (dept) {
                        result.push({
                            label: dept.data,
                            value: String.format('JobtitleForDept-{0}_{1}', dept.idext, 0),
                            type: type
                        });
                    });
                }
            }
            else if (selected.jobtitlies.length > 0) {
                var jobtitleIds = _.pluck(selected.jobtitlies, 'value').join(',');
                selected.jobDepts.forEach(function (dept) {
                    var text = _.pluck(selected.jobtitlies, 'label').join(', ') + " thuộc " + dept.data;
                    result.push({
                        label: text,
                        value: String.format('DeptForJobtitle-{0}_{1}', dept.idext, jobtitleIds),
                        type: type
                    });
                });
            }

            return result;
        },
        renderWarningCompilation: function (document, timeKey) {
            var that = this;
            if (that.warningCompilation) {
                that.warningCompilation.reload(timeKey)
            } else {
                require(["surveyWarningCompilation"], function (returnView) {
                    if (!returnView) {
                        return;
                    }
                    that.warningCompilation = new returnView({
                        document: document,
                        TimeKey: timeKey
                    });
                });
            }
        },
        renderFormSurvey: function (surveyConfig, surveyCriteria, donViNhan, doc, surveyReport) {
            var that = this;
            localStorage.setItem("cid", that.cid);
            this.criteriaArr = surveyCriteria;
            this.document = doc;
            var desLogic = this.$('#' + this.cid + "2a");
            var input = document.createElement("div");
            input.id = this.cid + "_surveyDesLogic";
            desLogic.append(input);
            var logic = this.$('#' + this.cid + "3a");
            input = document.createElement("div");
            input.id = this.cid + "_surveyLogic";
            logic.append(input);
            var preview = this.$('#' + this.cid + "4a");
            input = document.createElement("div");
            input.id = this.cid + "_surveyPreview";
            preview.append(input);
            var mainColor = "#5677fc";
            var mainHoverColor = "#3f51b5";
            var defaultThemeColorsEditor = SurveyCreator
                .StylesManager
                .ThemeColors["default"];
            defaultThemeColorsEditor["$primary-color"] = mainColor;
            defaultThemeColorsEditor["$secondary-color"] = mainColor;
            defaultThemeColorsEditor["$primary-hover-color"] = mainHoverColor;
            var optionsDesLogic = {
                showDesignerTab: true,
                showJSONEditorTab: false,
                showLogicTab: true,
                showTestSurveyTab: false,
                showTranslationTab: false,
                showEmbededSurveyTab: false
            };
            that.surveyDesLogic = new SurveyCreator.SurveyCreator(this.cid + "_surveyDesLogic", optionsDesLogic);
            that.surveyDesLogic.showToolbox = "right";
            that.surveyDesLogic.showPropertyGrid = "right";
            that.surveyDesLogic.rightContainerActiveItem("toolbox");
            var _dataSurveyDesLogic = {};
            if (localStorage.getItem("linkSurvey") === null) {
                _dataSurveyDesLogic = surveyConfig;
            } else {
                var linkSurvey = localStorage.getItem("linkSurvey");
                var objLinkSurvey = JSON.parse(linkSurvey);
                _dataSurveyDesLogic = objLinkSurvey;
            }
            that.surveyDesLogic.JSON = _dataSurveyDesLogic;
            var form = $(that.surveyDesLogic.renderedElement).find('form')[0];
            if (form != undefined) {
                var validator = new $.validator({}, form);
                $.data(form, 'validator', validator);
            }

            var $surveyAnalytics = document.createElement('div');
            this.$('#' + that.cid + '8a').append($surveyAnalytics);
            var q = this.surveyDesLogic.getAllQuestions().filter(x => x.getType() !== 'html');
            var receiveUnits = that.document.model.get("ReceiveUnits");
            var data = [];
            if (receiveUnits) {
                var data = receiveUnits.filter(user => user.UserStatus === 4).map(user => JSON.parse(user.UserNote));
            }
            var visPanel = new SurveyAnalytics.VisualizationPanel($surveyAnalytics, q, data);
            visPanel.showHeader = true;
            visPanel.render();
            this.$("#tab-" + this.cid + "3a").on("click", function () {
                that.questions = that.surveyDesLogic.getAllQuestionsName();
                that._onCriteriaTabActive();
            });

            that.survey = null;
            $(document).ready(function() {
                $(document).on("click",".btnsurveyComplete",function() {
                    that.survey.completeLastPage();
                    });
            });
            this.$("#tab-" + this.cid + "4a").on("click", function () {
                var _data = {};
                if (localStorage.getItem("linkSurvey") === null) {
                    _data = that.surveyDesLogic.JSON;
                } else {
                    var linkSurvey = localStorage.getItem("linkSurvey");
                    var objLinkSurvey = JSON.parse(linkSurvey);
                    _data = objLinkSurvey;
                }
                that.survey = new Survey.Model(_data);
                var r = JSON.parse(that.document.model.get("Note") || null);
                if (r) {
                    that.survey.data = r;
                }
                that.survey.render(that.cid + "_surveyPreview");
                if (that.document.model.get("ParentId") != undefined &&
                    that.document.model.get("ParentId") != 0 &&
                    that.document.model.get("UserCurrentId") == egov.userId && that.document.model.get("DocCopyStatus") == 2) {
                    $(`#${that.cid}_surveyPreview .sv_complete_btn`).removeClass("hidden");
                    $(`#tabContentEdit_${that.document.id} .surveyComplete`).removeClass("hidden");
                } else {
                    $(`#${that.cid}_surveyPreview .sv_complete_btn`).addClass("hidden");
                    $(`#tabContentEdit_${that.document.id} .surveyComplete`).addClass("hidden");
                }

               
                that.survey.onComplete.add(function (result) {
                    var docCopyId = that.document.model.get("DocumentCopyId");
                    egov.request.surveyComplete({
                        data: {
                            "docId": docCopyId,
                            "data": JSON.stringify(result.data)
                        },
                        success: function (data) {
                            if (data.success) {
                                egov.pubsub.publish(egov.events.status.success, data.success);
                                that._closeTab([docCopyId]);
                            } else {
                                egov.pubsub.publish(egov.events.status.error, data.error);
                            }
                        },
                        error: function (data) {
                            that.document.tab.display(true);
                            that.document.tab.errorTab();

                            var message = _resource.transferError;
                            egov.pubsub.publish(egov.events.status.error, message);
                        }
                    });
                });
            });
            this.$("#tab-" + this.cid + "6a").on("click", function () {
                var questions = that.criteriaArr || surveyCriteria || [];
                var obj = {
                    "data": {
                        "title": "Các tiêu chí",
                        "icon": "/Content/Images/doctype_activate.png"
                    },
                    "metadata": { id: `${that.cid}_0` },
                    "state": "open",
                    "attr": {
                        "id": `${that.cid}_0`,
                        "label": "Các tiêu chí"
                    },
                    "children": []
                };
                var data = [];
                _.each(questions, function (c) {
                    obj.children.push({
                        "data": {
                            "title": c.alias,
                            "icon": "/Content/Images/doctype_key.png"
                        },
                        "metadata": { id: c.alias },
                        "attr": {
                            "id": c.alias,
                            "label": c.alias
                        }
                    });
                });
                data.push(obj);
                $(`#${that.cid}tree`).jstree("destroy");
                $(`#${that.cid}tree`).on("click.jstree", `#${that.cid}_0 li`,
                    function (evt) {
                        if (evt.currentTarget.id != `${that.cid}_0`) {
                            var value = evt.currentTarget.innerText.substr(2); // bỏ 2 dấu cách ở đầu
                            CKEDITOR.instances[`${that.cid}SurveyReport`].insertHtml(`&#64;&#64;${value}&#64;&#64;`);
                        }
                    }).jstree({
                        "json_data": {
                            "data": data
                        },
                        "plugins": ["json_data", "themes"]
                    });
            });
            this.$("#tab-" + this.cid + "8a").on("click", function () {
                !!window && window.dispatchEvent(new UIEvent("resize"));
            });
            this.criteriaTabEl = this.$('#' + this.cid + "3a");
            this.tbody = this.criteriaTabEl.find('.svd-propertyeditor-condition-wide-table tbody');
            var langTools = ace.require("ace/ext/language_tools");
            if ($(`#${this.cid}3a #criteria-expression`).length > 0) {
                this.criteriaEditor = ace.edit('criteria-expression');
                var completer = {
                    identifierRegexps: [ID_REGEXP],
                    insertMatch: _insertMatch,
                    getCompletions: function (editor, session, pos, prefix, callback) {
                        var completions = _doGetCompletions(that.questions, prefix, completer);
                        callback(null, completions);
                    },
                    getDocTooltip: function (item) {
                        item.docHTML = "<div style='max-width: 300px; white-space: normal;'>" + item.meta + "</div>";
                    },
                };
                langTools.setCompleters([completer]);
                this.criteriaEditor.setOptions({
                    enableBasicAutocompletion: true,
                    enableLiveAutocompletion: true,
                });
            }
            that._bind(donViNhan);

            $.ajax({
                url: '/Admin/SurveyCatalog/GetAllData',
                method: 'POST'
            }).then(function (result) {
                if (result.success) {
                    var choices = result.data.map(function (d) {
                        return {
                            value: d.CatalogId,
                            text: d.CatalogName
                        };
                    });
                    Survey.Serializer.addProperty("selectBase", {
                        name: "Tiêu chí",
                        category: "choices",
                        visibleIndex: 0,
                        choices: choices,
                        onSetValue: function (obj, value) {
                            $.ajax('/SurveyCatalog/GetSurveyCatalogValues?catalogId=' + value)
                                .then(function (result) {
                                    obj.choices = result.map(function (r) { return { value: r.CatalogKey, text: r.Value }; });
                                });
                        }
                    });
                    Survey.Serializer.addProperty("matrixBase", {
                        name: "Tiêu chí 1",
                        category: "columns",
                        visibleIndex: 0,
                        choices: choices,
                        onSetValue: function (obj, value) {
                            $.ajax('/SurveyCatalog/GetSurveyCatalogValues?catalogId=' + value)
                                .then(function (result) {
                                    if (obj.getType() === 'matrixdropdown') {
                                        obj.columns.splice(0, obj.columns.length);
                                        for (var i = 0; i < result.length; i++) {
                                            obj.addColumn(result[i].CatalogKey, result[i].Value);
                                        }
                                    } else {
                                        obj.columns = result.map(function (r) { return { value: r.CatalogKey, text: r.Value }; });
                                    }
                                });
                        }
                    });
                    Survey.Serializer.addProperty("matrixBase", {
                        name: "Tiêu chí 2",
                        category: "rows",
                        visibleIndex: 0,
                        choices: choices,
                        onSetValue: function (obj, value) {
                            $.ajax('/SurveyCatalog/GetSurveyCatalogValues?catalogId=' + value)
                                .then(function (result) {
                                    obj.rows = result.map(function (r) { return { value: r.CatalogKey, text: r.Value }; });
                                });
                        }
                    });
                    Survey.Serializer.addProperty("matrixDropdown", {
                        name: "Tiêu chí 3",
                        category: "choices",
                        visibleIndex: 0,
                        choices: choices,
                        onSetValue: function (obj, value) {
                            $.ajax('/SurveyCatalog/GetSurveyCatalogValues?catalogId=' + value)
                                .then(function (result) {
                                    obj.choices = result.map(function (r) { return { value: r.CatalogKey, text: r.Value }; });
                                });
                        }
                    });
                }
            });
            var CkEditor_ModalEditor = {
                afterRender: function (modalEditor, htmlElement) {
                    var editor = CKEDITOR.replace(htmlElement, {
                        toolbar: [
                            ['Source', '-', 'Bold', 'Italic', 'Underline', 'Strike', '-'],
                            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'],
                            ['NumberedList', 'BulletedList', 'Table', '-'],
                            ['Styles', 'Format', 'Font', 'FontSize', '-'],
                            ['TextColor', 'BGColor', '-']
                        ],
                        on: {
                            instanceReady: function (ev) {
                                ev.editor.dataProcessor.htmlFilter.addRules({
                                    elements: {
                                        p: function (e) {
                                            if (e.attributes.style)
                                                e.attributes.style += ';font-family:Times New Roman,Times,serif';
                                            else e.attributes.style = 'font-family:Times New Roman,Times,serif';
                                        },
                                        span: function (e) {
                                            e.attributes.style = e.attributes.style.replace(/\"(Times New Roman)\"/, '$1');
                                        }
                                    }
                                });
                            }
                        }
                    });
                    var isUpdating = false;
                    editor.on("change", function () {
                        isUpdating = true;
                        modalEditor.editingValue = editor.getData();
                        isUpdating = false;
                    });
                    editor.setData(modalEditor.editingValue);
                    modalEditor.onValueUpdated = function (newValue) {
                        if (!isUpdating) {
                            editor.setData(newValue);
                        }
                    };
                },
                destroy: function (modalEditor, htmlElement) {
                    var instance = CKEDITOR.instances[htmlElement.id];
                    if (instance) {
                        instance.removeAllListeners();
                        CKEDITOR.remove(instance);
                    }
                }
            };
            SurveyCreator
                .SurveyPropertyModalEditor
                .registerCustomWidget("html", CkEditor_ModalEditor);
            // preview
            if (that.document.model.get("ParentId") != undefined &&
                that.document.model.get("ParentId") != 0 &&
                that.document.model.get("UserCurrentId") == egov.userId) {
                this.$("#tab-" + this.cid + "2a").addClass("hidden");
                this.$("#tab-" + this.cid + "3a").addClass("hidden");
                this.$("#tab-" + this.cid + "5a").addClass("hidden");
                this.$("#tab-" + this.cid + "6a").addClass("hidden");
                this.$("#tab-" + this.cid + "4a").find('a').trigger('click');
            }
            CKEDITOR.plugins.registered['save'] =
            {
                init: function (editor) {
                    var command = editor.addCommand('save',
                        {
                            modes: { wysiwyg: 1, source: 1 },
                            exec: function (editor) {
                                CKEDITOR.instances[`${that.cid}SurveyReport`].updateElement();
                                var surveyReportOld = that.document.model.get("SurveyReport");
                                var surveyReportNew = $(`textarea#${that.cid}SurveyReport`).val().replace(/&quot;/g, "'"); 
                                eGovMessage.show("Bạn có chắc muốn lưu cấu hình báo cáo?", null, eGovMessage.messageButtons.YesNo,
                                    function () {
                                        var data = {
                                            SurveyConfig: that.document.model.get("SurveyConfig") || "",
                                            SurveyCriteria: that.document.model.get("SurveyCriteria") || "",
                                            SurveyReport: surveyReportNew || "",
                                            DonViNhan: donViNhan
                                        };
                                        egov.request.surveySaveReport({
                                            data: {
                                                "docId": that.document.model.get("DocumentCopyId"),
                                                "data": JSON.stringify(data)
                                            },
                                            success: function (data) {
                                                if (data.success) {
                                                    egov.pubsub.publish(egov.events.status.success, "Chỉnh sửa cấu hình báo cáo thành công!");
                                                    that._viewReport(surveyCriteria, surveyReportNew);
                                                    that.document.model.set("SurveyReport", surveyReportNew);
                                                } else {
                                                    egov.pubsub.publish(egov.events.status.error, data.error);
                                                }
                                            },
                                            error: function (data) {
                                            }
                                        });

                                    }, function () {
                                        CKEDITOR.instances[`${that.cid}SurveyReport`].setData(surveyReportOld);
                                        CKEDITOR.instances[`${that.cid}SurveyReport`].updateElement();
                                    }
                                );
                            }
                        }
                    );
                    editor.ui.addButton('Save', { label: 'Lưu', command: 'save' });
                }
            }
            CKEDITOR.editorConfig = function (config) {
                config.language = 'es';
                config.uiColor = '#F7B42C';
                config.height = 200;
                config.toolbarCanCollapse = true;
                config.removePlugins = 'image,forms';
            };
            CKEDITOR.replace(`${that.cid}SurveyReport`, {
                height: 500,
                allowedContent: true,
                extraPlugins: 'highcharts'
            });

            CKEDITOR.editorConfig = function (config) {
                config.language = 'es';
                config.uiColor = '#F7B42C';
                config.height = 200;
                config.toolbarCanCollapse = true;
                config.removePlugins = 'image,forms,save';
            };
            CKEDITOR.replace(`${that.cid}SurveyReportView`, {
                height: 500,
                allowedContent: true,
                extraPlugins: 'highcharts',
                removePlugins: 'save'
            });
            CKEDITOR.instances[`${that.cid}SurveyReport`].setData(surveyReport);
            CKEDITOR.instances[`${that.cid}SurveyReport`].updateElement();
            that._viewReportCk(surveyCriteria, surveyReport);
        },
        _viewReportCk: function (surveyCriteria, surveyReport) {
            var that = this;
            // tab view báo cáo
            var objView = {
                "data": {
                    "title": "Đơn vị nhận",
                },
                "metadata": { id: `${that.cid}_view0` },
                "state": "open",
                "attr": {
                    "id": `${that.cid}_view0`,
                    "label": "Đơn vị nhận",
                    "rel": "dept"
                },
                "children": []
            };
            var dataView = [];
            var allUnit = that.document.model.get("ReceiveUnits") || [];
            this.allUserDeps.filter(function (userDept) {
                return allUnit.some(function (unit) {
                    return unit.UserUnit === userDept.userid;
                });
            }).forEach(function (filteredUserDept) {
                var children = objView.children;
                filteredUserDept.idext.split('.').map(function (id) {
                    return +id;
                }).forEach(function (id) {
                    var currentDept = that.allDepts.filter(function (dept) {
                        return dept.value === id;
                    })[0];
                    if (currentDept) {
                        var currentNode = children.filter(function (child) {
                            return child.metadata.id === id;
                        });
                        if (currentNode.length === 0) {
                            currentNode = {
                                "data": {
                                    "title": currentDept.label,
                                },
                                "metadata": { id: id },
                                "state": "open",
                                "attr": {
                                    "id": id,
                                    "label": currentDept.label,
                                    "rel": "dept"
                                },
                                "children": []
                            };
                            children.push(currentNode);
                            children = currentNode.children;
                        } else {
                            children = currentNode[0].children;
                        }
                    }
                });
                var currentUnit = allUnit.filter(unit => unit.UserUnit === filteredUserDept.userid)[0];
                children.push({
                    "data": {
                        "title": filteredUserDept.fullname,
                    },
                    "metadata": { id: filteredUserDept.userid },
                    "attr": {
                        "id": filteredUserDept.userid,
                        "label": filteredUserDept.fullname,
                        "class": currentUnit && currentUnit.UserStatus === 4 ? "jstree-checked" : "",
                        "rel": "people"
                    }
                });
            });
            dataView.push(objView);
            this.selectedUnits = allUnit.filter(function (unit) { return unit.UserStatus === 4; }).map(function (unit) { return unit.UserUnit; });
            $(`#${that.cid}treeView`).jstree("destroy");
            $(`#${that.cid}treeView`).jstree({
                "json_data": {
                    "data": dataView
                },
                "plugins": ["json_data", "themes", "checkbox"]
            }).bind("change_state.jstree", function (e, data) {
                that.selectedUnits = [];
                $(`#${that.cid}treeView .jstree-checked`).each(function () {
                    var node = $(this);
                    var nodeId = node.attr("id");
                    var unit = _.find(allUnit, function (i) { return i.UserUnit == nodeId; });
                    if (unit) {
                        that.selectedUnits.push(unit.UserUnit);
                    }

                });
                var surveyReportNew = $(`textarea#${that.cid}SurveyReport`).val();
                that._viewReport(surveyCriteria, surveyReportNew);
            });
            that._viewReport(surveyCriteria, surveyReport);
        },

        _viewReport: function (surveyCriteria, surveyReport) {
            var that = this;
            that.viewSurveyReport = surveyReport;
            var allKeys = that.viewSurveyReport.match(/@@(.*?)@@/gm) || [];
            var value, checkCount = false;
            var allUnit = that.document.model.get("ReceiveUnits") || [];
            allUnit = _.filter(allUnit,
                function (a) {
                    return that.selectedUnits.indexOf(a.UserUnit) >= 0;
                });
            _.each(allKeys, function (criteriaKey) {
                checkCount = false;
                var decode = decodeHTML(criteriaKey);
                var criteria = _.find(surveyCriteria, function (i) { return i.alias == decode.replaceAll("@@", ""); });
                var resultUser, resultDk;
                if (criteria) {
                    // get đơn vị hoàn thành khảo sát
                    var users = _.filter(allUnit, function (itm) {
                        return itm.UserNote != undefined;//  && itm.UserNote.includes(criteria.name);
                    });
                    try {
                        var cValue = criteria.value;
                        var choices = criteria.choices || [];
                        if (criteria.type == 'radiogroup' || criteria.type == 'dropdown') {
                            var cValues = _.find(choices,
                                function (itm) {
                                    return itm.value != undefined && itm.value == criteria.value;
                                });
                            cValue = cValues != undefined ? cValues.text : "";
                        }
                        switch (criteria.operator) {
                            case 'empty': //trống
                                value = _.filter(users,
                                    function (itm) {
                                        return itm.UserNote != undefined && 
                                            JSON.parse(itm.UserNote)[criteria.name] == undefined;
                                    });
                                break;
                            case 'notempty': // ko trống
                                value = _.filter(users,
                                    function (itm) {
                                        return itm.UserNote != undefined &&
                                            JSON.parse(itm.UserNote)[criteria.name] != undefined;
                                    });
                                break;
                            case 'equal': // bằng
                                if (criteria.type == 'checkbox') {
                                    value = _.filter(users,
                                        function (itm) {

                                            if (!(JSON.parse(itm.UserNote)[criteria.name] instanceof Array) ||
                                                !(criteria.value instanceof Array))
                                                return false;
                                            resultUser = JSON.parse(itm.UserNote)[criteria.name].sort();
                                            resultDk = criteria.value.sort();
                                            return itm.UserNote != undefined && resultUser.toString() == resultDk.toString();
                                        });
                                }
                                else
                                    value = _.filter(users,
                                        function (itm) {
                                            return itm.UserNote != undefined && JSON.parse(itm.UserNote)[criteria.name] != undefined &&
                                                JSON.parse(itm.UserNote)[criteria.name].toString() == (criteria.value.toString() || "");
                                        });
                                break;
                            case 'notequal': // ko bằng
                                if (criteria.type == 'checkbox') {
                                    value = _.filter(users,
                                        function (itm) {

                                            if (!(JSON.parse(itm.UserNote)[criteria.name] instanceof Array) ||
                                                !(criteria.value instanceof Array))
                                                return false;
                                            resultUser = JSON.parse(itm.UserNote)[criteria.name].sort();
                                            resultDk = criteria.value.sort();
                                            return itm.UserNote != undefined && resultUser.toString() != resultDk.toString();
                                        });
                                }
                                else
                                    value = _.filter(users,
                                        function (itm) {
                                            return itm.UserNote != undefined && JSON.parse(itm.UserNote)[criteria.name] != undefined &&
                                                JSON.parse(itm.UserNote)[criteria.name].toString() != (criteria.value.toString() || "");
                                        });
                                break;
                            case 'contains': // chứa
                                if (criteria.type == 'checkbox') {
                                    value = _.filter(users,
                                        function (itm) {
                                            return itm.UserNote != undefined && JSON.parse(itm.UserNote)[criteria.name] != undefined &&
                                                JSON.parse(itm.UserNote)[criteria.name].some(
                                                    r => criteria.value.indexOf(r) >= 0);
                                        });
                                }
                                else
                                    value = _.filter(users,
                                        function (itm) {
                                            return itm.UserNote != undefined && JSON.parse(itm.UserNote)[criteria.name] != undefined &&
                                                JSON.parse(itm.UserNote)[criteria.name].includes(criteria.value || "");
                                        });
                                break;
                            case 'notcontains': // không chứa
                                if (criteria.type == 'checkbox') {
                                    value = _.filter(users,
                                        function (itm) {
                                            return itm.UserNote != undefined && JSON.parse(itm.UserNote)[criteria.name] != undefined &&
                                                !JSON.parse(itm.UserNote)[criteria.name].some(
                                                    r => criteria.value.indexOf(r) >= 0);
                                        });
                                }
                                else
                                    value = _.filter(users,
                                        function (itm) {
                                            return itm.UserNote != undefined && JSON.parse(itm.UserNote)[criteria.name] != undefined &&
                                                !JSON.parse(itm.UserNote)[criteria.name].includes(criteria.value || "");
                                        });
                                break;
                            case 'anyof': // bất kỳ
                                if (criteria.type == 'checkbox') {
                                    value = _.filter(users,
                                        function (itm) {
                                            resultUser = JSON.parse(itm.UserNote)[criteria.name];
                                            resultDk = criteria.value;
                                            if (!(JSON.parse(itm.UserNote)[criteria.name] instanceof Array) ||
                                                !(criteria.value instanceof Array))
                                                return false;
                                            var arr = resultUser.filter(x => resultDk.includes(x));
                                            return itm.UserNote != undefined && arr.length > 0;
                                        });
                                }
                                else if (criteria.type == 'radiogroup' || criteria.type == 'dropdown') {
                                    value = _.filter(users,
                                        function (itm) {
                                            if (!(criteria.value instanceof Array))
                                                return false;
                                            return itm.UserNote != undefined && criteria.value.includes(JSON.parse(itm.UserNote)[criteria.name]);
                                        });
                                }
                                else
                                    value = [];
                                break;
                            case 'allof': // tất cả
                                if (criteria.type == 'checkbox') {
                                    value = _.filter(users,
                                        function (itm) {
                                            resultUser = JSON.parse(itm.UserNote)[criteria.name];
                                            resultDk = criteria.value;
                                            if (!(JSON.parse(itm.UserNote)[criteria.name] instanceof Array) ||
                                                !(criteria.value instanceof Array))
                                                return false;
                                            var arr = resultUser.filter(x => resultDk.includes(x));
                                            return itm.UserNote != undefined && arr.length == resultDk.length;
                                        });
                                }
                                else
                                    value = [];
                                break;
                            case 'greater': // lớn hơn
                                value = _.filter(users,
                                    function (itm) {
                                        var uValue = JSON.parse(itm.UserNote)[criteria.name];
                                        if (criteria.type == 'radiogroup' || criteria.type == 'dropdown') {
                                            var uValues = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == uValue;
                                                });
                                            uValue = uValues != undefined ? uValues.text : uValue;
                                        }
                                        if (isNaN(cValue) || uValue != undefined && isNaN(uValue))
                                            checkCount = true;
                                        return itm.UserNote != undefined && uValue != undefined && (isNaN(cValue) || isNaN(uValue) || Number(uValue) > Number(cValue));
                                    });
                                break;
                            case 'less': // nhỏ hơn
                                value = _.filter(users,
                                    function (itm) {
                                        var uValue = JSON.parse(itm.UserNote)[criteria.name];
                                        if (criteria.type == 'radiogroup' || criteria.type == 'dropdown') {
                                            var uValues = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == uValue;
                                                });
                                            uValue = uValues != undefined ? uValues.text : uValue;
                                        }
                                        if (isNaN(cValue) || uValue != undefined && isNaN(uValue))
                                            checkCount = true;
                                        return itm.UserNote != undefined && uValue != undefined && (isNaN(cValue) || isNaN(uValue) || Number(uValue) < Number(cValue));
                                    });
                                break;
                            case 'greaterorequal': // lớn hơn hoặc bằng
                                value = _.filter(users,
                                    function (itm) {
                                        var uValue = JSON.parse(itm.UserNote)[criteria.name];
                                        if (criteria.type == 'radiogroup' || criteria.type == 'dropdown') {
                                            var uValues = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == uValue;
                                                });
                                            uValue = uValues != undefined ? uValues.text : uValue;
                                        }
                                        if (isNaN(cValue) || uValue != undefined && isNaN(uValue))
                                            checkCount = true;
                                        return itm.UserNote != undefined && uValue != undefined && (isNaN(cValue) || isNaN(uValue) || Number(uValue) >= Number(cValue));
                                    });
                                break;
                            case 'lessorequal': // nhỏ hơn hoặc bằng
                                value = _.filter(users,
                                    function (itm) {
                                        var uValue = JSON.parse(itm.UserNote)[criteria.name];
                                        if (criteria.type == 'radiogroup' || criteria.type == 'dropdown') {
                                            var uValues = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == uValue;
                                                });
                                            uValue = uValues != undefined ? uValues.text : uValue;
                                        }
                                        if (isNaN(cValue) || uValue != undefined && isNaN(uValue))
                                            checkCount = true;
                                        return itm.UserNote != undefined && uValue != undefined && (isNaN(cValue) || isNaN(uValue) || Number(uValue) <= Number(cValue));
                                    });
                                break;
                            default:
                                value = users;
                                break;

                        }
                        var result = 0, i = 0, len = value.length, v = 0, minS, maxS, rValue, rValues, val;
                        if (criteria.formula == 'sum' ||
                            criteria.formula == 'avg' ||
                            criteria.formula == 'min' ||
                            criteria.formula == 'max') {
                            if (criteria.operator == 'empty') {
                                that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, result);
                            }
                            else if (len > 0) {
                                switch (criteria.type) {
                                    case 'text':
                                        if (isNaN(criteria.value) && criteria.operator != 'notempty')
                                            result = NaN;
                                        else {

                                            switch (criteria.formula) {
                                                case 'sum': //Tổng
                                                    for (i = 0; i < len; i++) {
                                                        result += Number(JSON.parse(value[i].UserNote)[criteria.name]);
                                                    }
                                                    break;
                                                case 'avg': //Trung bình
                                                    for (i = 0; i < len; i++) {
                                                        result += Number(JSON.parse(value[i].UserNote)[criteria.name]);
                                                    }
                                                    result = Math.round((result / len) * 100) / 100;
                                                    break;
                                                case 'min': //Nhỏ nhất
                                                    minS = Number(JSON.parse(value[0].UserNote)[criteria.name]);
                                                    for (i = 1; i < len; i++) {
                                                        v = Number(JSON.parse(value[i].UserNote)[criteria.name]);
                                                        minS = (v < minS) ? v : minS;
                                                    }
                                                    result = minS;
                                                    break;
                                                case 'max': //Lớn nhất
                                                    maxS = Number(JSON.parse(value[0].UserNote)[criteria.name]);
                                                    for (i = 1; i < len; i++) {
                                                        v = Number(JSON.parse(value[i].UserNote)[criteria.name]);
                                                        maxS = (v > maxS) ? v : maxS;
                                                    }
                                                    result = maxS;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 'radiogroup': case "dropdown":
                                        var check = _.filter(criteria.value, function (x) {
                                            var choice = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == x;
                                                });
                                            var checkV = choice != undefined ? choice.text : "";
                                            return isNaN(checkV);
                                        });
                                        if (check.length > 0 && criteria.operator != 'notempty') {
                                            result = NaN;
                                        }
                                        else {
                                            switch (criteria.formula) {
                                                case 'sum': //Tổng
                                                    for (i = 0; i < len; i++) {
                                                        rValues = _.find(choices,
                                                            function (itm) {
                                                                return itm.value != undefined && itm.value == JSON.parse(value[i].UserNote)[criteria.name];
                                                            });
                                                        rValue = rValues != undefined ? rValues.text : NaN;
                                                        result += Number(rValue);
                                                    }
                                                    break;
                                                case 'avg': //Trung bình
                                                    for (i = 0; i < len; i++) {
                                                        rValues = _.find(choices,
                                                            function (itm) {
                                                                return itm.value != undefined && itm.value == JSON.parse(value[i].UserNote)[criteria.name];
                                                            });
                                                        rValue = rValues != undefined ? rValues.text : NaN;
                                                        result += Number(rValue);
                                                    }
                                                    result = Math.round((result / len) * 100) / 100;
                                                    break;
                                                case 'min': //Nhỏ nhất
                                                    var minTmp = _.find(choices,
                                                        function (itm) {
                                                            return itm.value != undefined && itm.value == JSON.parse(value[0].UserNote)[criteria.name];
                                                        });
                                                    minS = minTmp != undefined ? Number(minTmp.text) : NaN;
                                                    for (i = 0; i < len; i++) {
                                                        rValues = _.find(choices,
                                                            function (itm) {
                                                                return itm.value != undefined && itm.value == JSON.parse(value[i].UserNote)[criteria.name];
                                                            });
                                                        rValue = rValues != undefined ? rValues.text : 0;
                                                        v = Number(rValue);
                                                        minS = (v < minS) ? v : minS;
                                                    }
                                                    result = minS;
                                                    break;
                                                case 'max': //Lớn nhất
                                                    var maxTmp = _.find(choices,
                                                        function (itm) {
                                                            return itm.value != undefined && itm.value == JSON.parse(value[0].UserNote)[criteria.name];
                                                        });
                                                    maxS = maxTmp != undefined ? Number(maxTmp.text) : NaN;
                                                    for (i = 0; i < len; i++) {
                                                        rValues = _.find(choices,
                                                            function (itm) {
                                                                return itm.value != undefined && itm.value == JSON.parse(value[i].UserNote)[criteria.name];
                                                            });
                                                        rValue = rValues != undefined ? rValues.text : NaN;
                                                        v = Number(rValue);
                                                        maxS = (v > maxS) ? v : maxS;
                                                    }
                                                    result = maxS;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 'checkbox':
                                        var check = _.filter(criteria.value, function (x) {
                                            var choice = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == x;
                                                });
                                            var checkV = choice != undefined ? choice.text : "";
                                            return isNaN(checkV);
                                        });
                                        if (check.length > 0 && criteria.operator != 'notempty') {
                                            result = NaN;
                                        }
                                        else
                                        switch (criteria.formula) {
                                            case 'sum': //Tổng
                                                for (i = 0; i < len; i++) {
                                                    _.each(JSON.parse(value[i].UserNote)[criteria.name],
                                                        function (x) {
                                                            rValues = _.find(choices,
                                                                function (itm) {
                                                                    return itm.value != undefined && itm.value == x;
                                                                });
                                                            rValue = rValues != undefined ? rValues.text : 0;
                                                            result += Number(rValue);
                                                        });
                                                }
                                                break;
                                            case 'avg': //Trung bình
                                                var c = 0;
                                                for (i = 0; i < len; i++) {
                                                    _.each(JSON.parse(value[i].UserNote)[criteria.name],
                                                        function (x) {
                                                            rValues = _.find(choices,
                                                                function (itm) {
                                                                    return itm.value != undefined &&
                                                                        itm.value == x;
                                                                });
                                                            rValue = rValues != undefined ? rValues.text : 0;
                                                            result += Number(rValue);
                                                            c++;
                                                        });
                                                }
                                                result = Math.round((result / c) * 100) / 100;
                                                break;
                                            case 'min': //Nhỏ nhất
                                                var minTmp = JSON.parse(value[0].UserNote)[criteria.name];
                                                minTmp = minTmp != undefined && minTmp.length > 0 ? minTmp[0] : "";
                                                rValues = _.find(choices,
                                                    function (itm) {
                                                        return itm.value != undefined &&
                                                            itm.value == minTmp;
                                                    });
                                                minS = rValues != undefined ? Number(rValues.text) : 0;
                                                for (i = 0; i < len; i++) {
                                                    _.each(JSON.parse(value[i].UserNote)[criteria.name],
                                                        function (x) {
                                                            rValues = _.find(choices,
                                                                function (itm) {
                                                                    return itm.value != undefined &&
                                                                        itm.value == x;
                                                                });
                                                            rValue = rValues != undefined ? rValues.text : 0;
                                                            v = Number(rValue);
                                                            minS = (v < minS) ? v : minS;
                                                        });
                                                }
                                                result = minS;
                                                break;
                                            case 'max': //Lớn nhất
                                                var maxTmp = JSON.parse(value[0].UserNote)[criteria.name];
                                                maxTmp = maxTmp != undefined && maxTmp.length > 0 ? maxTmp[0] : ""
                                                rValues = _.find(choices,
                                                    function (itm) {
                                                        return itm.value != undefined &&
                                                            itm.value == maxTmp;
                                                    });
                                                maxS = rValues != undefined ? Number(rValues.text) : 0;
                                                for (i = 0; i < len; i++) {
                                                    _.each(JSON.parse(value[i].UserNote)[criteria.name],
                                                        function (x) {
                                                            rValues = _.find(choices,
                                                                function (itm) {
                                                                    return itm.value != undefined &&
                                                                        itm.value == x;
                                                                });
                                                            rValue = rValues != undefined ? rValues.text : 0;
                                                            v = Number(rValue);
                                                            maxS = (v > maxS) ? v : maxS;
                                                        });
                                                }
                                                result = maxS;
                                                break;
                                        }
                                        break;
                                }
                                that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, result);
                            }
                            else {
                                switch (criteria.type) {
                                    case 'checkbox': case 'radiogroup': case "dropdown":
                                        var check = _.filter(criteria.value, function (x) {
                                            var choice = _.find(choices,
                                                function (itm) {
                                                    return itm.value != undefined && itm.value == x;
                                                });
                                            var checkV = choice != undefined ? choice.text : "";
                                            return isNaN(checkV);
                                        });
                                        that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, check.length > 0 ? "NaN" : result);
                                        break;
                                     case 'text':
                                        var check = isNaN(cValue);
                                        that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, check ? "NaN" : result);
                                        break;
                                }

                            }
                        }
                        else if (criteria.formula == 'count') //Đếm
                        {
                            that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, checkCount ? "NaN" : value.length);
                        }
                        else {
                            that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, "<b>Chưa chọn công thức</b>");
                        }
                    } catch (err) {
                        eGovMessage.notification(err, eGovMessage.messageTypes.error, true);
                    }
                }
                else
                    that.viewSurveyReport = that.viewSurveyReport.replace(criteriaKey, "");
            });
            CKEDITOR.instances[`${that.cid}SurveyReportView`].setData(that.viewSurveyReport);
        },
        _onCriteriaTabActive: function (criteriaArr) {
            var that = this;
            that.criteriaArr = that.criteriaArr || criteriaArr;
            this.tbody.empty();
            if (this.criteriaArr != null && this.criteriaArr.length > 0) {
                _.each(that.questions, function (q, idx) {
                    _.each(that.criteriaArr, function (e, i) {
                        if (e.name == q.name)
                            that.criteriaArr[i].choices = q.choices;
                    });
                });
            }
            var rows = $.tmpl(this.criteriaRow, that.criteriaArr || that.questions);
            rows.appendTo(this.tbody);
        },
        _checkDuplicateAlias: function () {
            var hasDuplicate = false;
            var aliasElArr = this.criteriaTabEl.find('input[name=alias]').toArray();
            aliasElArr.forEach(function (el) { el.setCustomValidity(''); });
            for (var i = 0; i < aliasElArr.length; i++) {
                var currentValue = aliasElArr[i].value;
                for (var j = i + 1; j < aliasElArr.length; j++) {
                    if (!aliasElArr[j].checkValidity())
                        continue;
                    if (currentValue != aliasElArr[j].value)
                        continue;
                    hasDuplicate = true;
                    aliasElArr[i].setCustomValidity('Tên tiêu chí đã tồn tại. Hãy nhập giá trị khác.');
                    aliasElArr[j].setCustomValidity('Tên tiêu chí đã tồn tại. Hãy nhập giá trị khác.');
                }
            }
            return hasDuplicate;
        },
        _markInputAsVisited: function () {
            this.criteriaTabEl.find('input[name=alias]').addClass('visited');
            this.criteriaTabEl.find('select[name=name]').attr('required', '');
        },
        _addCriteria: function () {
            var that = this;
            var row = $.tmpl(this.criteriaRow, { addRow: true, questions: this.questions, type: '' });
            var selectQuestion = row.find('.question-name select');
            selectQuestion.on('change', function (e) {
                var questionName = this.value;
                var question = that.questions.filter(function (q) { return q.name == questionName; })[0] || { addRow: true, type: '' };;
                row.find('.question-value').remove();
                row.find('.sv_row + .sv_row').remove();

                row.find('input[name=type]').val(question.type);
                row.find('select[name=formula], select[name=operator]').val('').attr('disabled', !!question.addRow);

                var operatorOption = row.find('select[name=operator] option');
                operatorOption.attr('disabled', false);

                var sv_row = row.find('.sv_row');
                if (question.type === 'text' || question.type === '') {
                    $.tmpl(that.textTemplate, question).appendTo(sv_row);
                    operatorOption.filter(function () { return this.value === 'allof' || this.value === 'anyof' }).attr('disabled', true);
                } else if (question.type === 'checkbox') {
                    $.tmpl(that.checkboxTemplate, question).insertAfter(sv_row);
                    operatorOption.filter(function () { return this.value === 'greater' || this.value === 'less' || this.value === 'greaterorequal' || this.value === 'lessorequal' }).attr('disabled', true);
                } else {
                    $.tmpl(that.dropdownTemplate, question).appendTo(sv_row);
                    operatorOption.filter(function () { return this.value === 'contains' || this.value === 'notcontains' || this.value === 'allof' }).attr('disabled', true);
                }
            });
            row.appendTo(this.tbody);
        },
        _onAliasChange: function (e) {
            this._checkDuplicateAlias();
            $(e.target).addClass('visited');
            $(e.target).closest('form').find('input:submit').click();
        },
        _onCriteriaNameChange: function (e) {
            e.target.setCustomValidity('');
        },
        _onOperatorChange: function (e) {
            var that = this;
            var sv_row = $(e.target).closest('.sv_row');

            var questionName = $(e.target).closest('form').find('[name=name]').val();
            var questionType = $(e.target).closest('form').find('input[name=type]').val();
            var question = this.questions.filter(q => q.name === questionName)[0];
            if (questionType === 'checkbox') {
                if ((e.target.value === 'contains' || e.target.value === 'notcontains')) {
                    if (sv_row.find('.question-value').length === 0) {
                        sv_row.next().remove();
                        $.tmpl(that.dropdownTemplate, question).appendTo(sv_row);
                    }
                } else {
                    if (sv_row.next().length === 0) {
                        sv_row.find('.question-value').remove();
                        $.tmpl(that.checkboxTemplate, question).insertAfter(sv_row);
                    }
                }
            } else if (questionType === 'radiogroup' || questionType === 'dropdown') {
                if ((e.target.value === 'anyof')) {
                    if (sv_row.next().length === 0) {
                        sv_row.find('.question-value').remove();
                        $.tmpl(that.checkboxTemplate, question).insertAfter(sv_row);
                    }
                } else {
                    if (sv_row.find('.question-value').length === 0) {
                        sv_row.next().remove();
                        $.tmpl(that.dropdownTemplate, question).appendTo(sv_row);
                    }
                }
            }

            sv_row.find('[name=value]').attr('disabled', e.target.value === 'empty' || e.target.value === 'notempty');
            sv_row.next().find('input:checkbox').attr('disabled', e.target.value === 'empty' || e.target.value === 'notempty');
        },
        _addCriteriaAdvance: function () {
            var criteriaNameEl = this.criteriaTabEl.find('#criteria-name')[0];
            criteriaNameEl.setCustomValidity('');
            var aliasArr = this.criteriaTabEl.find('input[name=alias]').toArray().map(function (el) { return el.value; });
            if (aliasArr.indexOf(criteriaNameEl.value) >= 0) {
                criteriaNameEl.setCustomValidity('Tên tiêu chí đã tồn tại. Hãy nhập giá trị khác.');
                return;
            }
            $.tmpl(this.criteriaRow, {
                alias: criteriaNameEl.value,
                name: criteriaNameEl.value,
                value: this.criteriaEditor.getValue(),
                type: 'advance'
            }).appendTo(this.tbody);
            criteriaNameEl.value = '';
            this.criteriaEditor.setValue('');
        },
        _saveCriteria: function () {
            var that = this;
            this._markInputAsVisited();
            this._checkDuplicateAlias();
            var lastEl = this.criteriaTabEl.find('input[name=alias].visited').toArray().filter(function (el) { return !el.checkValidity(); }).pop();
            if (lastEl) {
                $(lastEl).closest('form').find('input:submit').click();
                return;
            }
            this.criteriaArr = [];
            var rows = this.tbody.find('form');
            rows.each(function (index) {
                var arr = $(this).serializeArray();
                var criteriaObj = {};
                arr.forEach(function (prop) {
                    criteriaObj[prop.name] = prop.value;
                });
                var checkboxes = $(this).find('.checkbox input:checkbox');
                if (checkboxes.length) {
                    criteriaObj.value = [];
                    checkboxes.each(function () {
                        if (this.checked) {
                            criteriaObj.value.push(this.name.split('_')[1]);
                            delete criteriaObj[this.name];
                        }
                    });
                }
                if (criteriaObj.type === 'checkbox' && (criteriaObj.operator === 'contains' || criteriaObj.operator === 'notcontains')) {
                    criteriaObj.value = [criteriaObj.value];
                }
                var question = that.questions.filter(function (q) { return q.name === criteriaObj.name; })[0];
                if (question)
                    criteriaObj.choices = question.choices;
                if (criteriaObj.name)
                    that.criteriaArr.push(criteriaObj);
            });

            if (that.document.isCreate) {
                this.document.model.set('SurveyCriteria', JSON.stringify(this.criteriaArr));
                this.document.$el.find('[name="SurveyCriteria"]').val(JSON.stringify(this.criteriaArr));
                eGovMessage.notification('Lưu tạm tiêu chí thành công!', eGovMessage.messageTypes.success, true);
            }
            else {
                eGovMessage.show("Bạn có chắc muốn lưu cấu hình tiêu chí?", null, eGovMessage.messageButtons.YesNo,
                    function () {
                        that.document.model.set('SurveyCriteria', JSON.stringify(that.criteriaArr));
                        that.document.$el.find('[name="SurveyCriteria"]').val(JSON.stringify(that.criteriaArr));
                        var data = {
                            SurveyConfig: that.document.model.get("SurveyConfig") || "",
                            SurveyCriteria: that.document.model.get("SurveyCriteria") || "",
                            SurveyReport: that.document.model.get("SurveyReport") || "",
                            DonViNhan: that.document.model.get('DonViNhan')
                        };
                        egov.request.surveySaveReport({
                            data: {
                                "docId": that.document.model.get("DocumentCopyId"),
                                "data": JSON.stringify(data)
                            },
                            success: function (data) {
                                if (data.success) {
                                    egov.pubsub.publish(egov.events.status.success, "Chỉnh sửa cấu hình tiêu chí thành công!");
                                    that._viewReport(JSON.parse(that.document.model.get("SurveyCriteria") || []), $(`textarea#${that.cid}SurveyReport`).html($(`textarea#${that.cid}SurveyReport`).val()).text());

                                } else {
                                    egov.pubsub.publish(egov.events.status.error, data.error);
                                }
                            },
                            error: function (data) {
                            }
                        });

                    }, function () {
                        that.criteriaArr = JSON.parse(that.document.model.get("SurveyCriteria") || "");
                    }
                );
            }
        },
        _deleteCriteria: function (e) {
            $(e.target).closest('tr').remove();
        },

        _bind: function (donViNhan) {
            var that = this;

            var $userDg = this.$('#dgUser');
            var $filterDept = $('.sv_FilterDeparment#' + this.cid + '_FilterDepartment');
            that._bindUsers($userDg, $filterDept);

            that._bindDepartments(function () {
                that._bindJobtitlies(function () {
                    that._show();
                    that._bindCallback(donViNhan);
                    egov.callback(that.onloaded);
                    that.$el.bindResources();
                    // that.$('#dgUser').focus();
                });
            });
        },
        _bindUsers: function ($user, $filterDept) {
            /// <summary>
            /// Hiển thị dữ liệu user để tìm kiếm và tree dept
            /// </summary>
            var that = this;
            $user.autocomplete({
                source: this.allUsers,
                focus: function (event, ui) {
                    $user.val(ui.item.username);
                    return false;
                },
                select: function (event, ui) {
                    _selectUser($filterDept, ui.item.value, true, that.allDepts, that.allUsers, that.allUserDeps);
                    $user.val('');
                    return false;
                }
            })
                .data("autocomplete")._renderItem = function (ul, item) {
                    ul.addClass('dropdown-menu').css("zIndex", "1060");
                    return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append("<a>" + item.label + "</a>")
                        .appendTo(ul);
                };
        },
        _bindDepartments: function (callback) {
            var that = this;
            require(['jstree'], function () {
                // Bind search user
                _bindJsTree($('#' + that.cid + '_FilterDepartment'), true, true, false,
                    that.allDepts, that.allUsers,
                    that.allUserDeps, null, []);

                // Phòng ban, chức danh
                _bindJsTree($('#' + that.cid + '_DeptForJobtitle'), false, true, false,
                    that.allDepts, [], [], null, []);

                if (typeof callback === 'function') {
                    setTimeout(function () { callback(); }, 100);
                }
            });
        },
        _bindJobtitlies: function (callback) {
            /// <summary>
            /// Bind danh sách các chức danh
            /// </summary>
            this.$jobtitlies = this.$('.dgJobtitlies');

            var max;
            for (var i = 0; max = this.allPositions.length, i < max; i++) {
                var jobtitle = this.allPositions[i];
                var jobItem = $('<li>').addClass('list-group-item');
                jobItem.append(String.format('<label><input type="checkbox" value="{0}"> {1}</label>', jobtitle.value, jobtitle.label));
                this.$jobtitlies.append(jobItem);
            }
            if (egov.isMobile) {
                this.$jobtitlies.find(".mdl-checkbox").materialCheckbox();
            }
            $('#' + this.cid + '_JobtitleForDept #sv_AllJobsForDept').checkAll($('#' + this.cid + '_JobtitleForDept :checkbox').not(":first"));
            this.$('.jobtitlies #allJobs').checkAll(this.$('.jobtitlies :checkbox').not(":first"));

            if (typeof callback === 'function') {
                callback();
            }
        },

        _getUsersDept: function () {
            ///<summary>
            /// Trả về kết quả chọn user - phòng ban
            /// </summary>
            var result = this.result,
                that = this,
                allDepts = that.allDepts,
                allUsers = that.allUsers;

            // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức danh
            $("#" + this.cid + "_FilterDepartment").jstree("get_checked", null, false).each(function () {
                var node = $(this);
                var nodeId = this.id;
                // Xác định node đang chọn là node phòng ban hay node user
                var isDeptNode = node.attr('rel') === 'dept';
                if (isDeptNode) {
                    var dept = _.find(allDepts, function (i) {
                        return i.value == nodeId;
                    });
                    if (dept) {
                        // Xác định node root: là node không có extend .
                        if (dept.idext.indexOf('.') < 0) {
                            result.isAllUser = true;
                            return;
                        }
                        // Không thì push dept hiện tại vào danh sách được chọn
                        result.depts.push(dept);
                    }
                }
                else {
                    // Node là user
                    nodeId = nodeId.replace('user_', '');
                    var user = _.find(allUsers, function (i) {
                        return i.value == nodeId;
                    });

                    if (user) {
                        ///Kiểm tra xem tồn tai hay chưa,không có thì add
                        if (!_.contains(result.users, user))
                            result.users.push(user);
                    }
                }
            });
        },
        _getJobtitlesDept: function () {
            ///<summary>
            /// Trả về kết quả chọn Jobtitle - phòng ban
            /// </summary>
            var result = this.result;
            // Lấy ra danh sách các chức vụ đang được chọn.
            var isAllJobtitle = false;
            var jobtitleSelected = [];
            var that = this;
            $('#' + this.cid + '_JobtitleForDept :checked').each(function (checkbox) {
                var jobId = $(this).val();
                if (jobId === '0') {
                    isAllJobtitle = true;
                    return;
                }
                var jobtitle = _.find(that.allPositions, function (i) {
                    return i.value == jobId;
                });
                if (jobtitle) {
                    jobtitleSelected.push(jobtitle);
                }
            });

            var that = this;
            var allDepts = that.allDepts;
            var selecteds = $("#" + this.cid + "_DeptForJobtitle").jstree("get_checked", null, false);
            if (selecteds.length === 0) {
                // Trường hợp chưa chọn phòng ban nào mặc định là gửi cho tất cả
                result.isAllDept = true;
                result.isAllJobtitle = isAllJobtitle;
                result.jobtitlies = jobtitleSelected;
                var rootDept = _.find(allDepts, function (i) {
                    return i.idext.indexOf('.') < 0;
                });
                result.jobDepts.push(rootDept);
                return;
            }
            selecteds.each(function () {
                var node = $(this);
                var nodeId = this.id;

                var dept = _.find(allDepts, function (i) {
                    return i.value == nodeId;
                });
                if (dept) {
                    // Xác định node root: là node không có extend .
                    var isAllDept = dept.idext.indexOf('.') < 0;
                    // Chọn tất cả phòng ban
                    if (isAllDept) {
                        // Trường hợp chọn tất cả phòng ban và chức danh = chọn tất cả user
                        if (isAllJobtitle) {
                            result.isAllDept = true;
                            result.isAllJobtitle = true;
                            return;
                        }
                        else {
                            result.isAllDept = true;
                            result.jobDepts.push(dept);
                            result.jobtitlies = jobtitleSelected;
                            return;
                        }
                    }
                    else {
                        // Chọn tất cả chức danh và không chọn tất cả phòng ban
                        if (isAllJobtitle) {
                            result.isAllJobtitle = true;
                            result.jobDepts.push(dept);
                        }
                        else {
                            result.jobtitlies = jobtitleSelected;
                            result.jobDepts.push(dept);
                        }
                    }
                }
            });
        },
        _processResult: function () {
            /// <summary>
            /// Xử lý danh sách nhận đồng gửi.
            /// </summary>
            /// <returns type=""></returns>

            var that = this;
            // Loại bỏ phòng ban hiện tại và các phòng ban của nó ở những lựa chọn khác.
            // Ví dụ ở lựa chọn User - Phòng ban: chọn gửi cho BDA,
            // Xong lại chọn ở Chức vụ - phòng ban: gửi Manager của BDA, Gửi tất cả nhân viên BDA/Phòng 4
            // => Lựa chọn ở trên bao gồm lựa chọn ở dưới => cần remove cái thừa đi
            this.result.depts.forEach(function (d) {
                that.result.jobDepts = _.reject(that.result.jobDepts, function (i) {
                    return i.idext.split('.').indexOf('' + d.value) >= 0; // điều kiện này tính cả trường hợp =
                });
            });

            return this.result;
        },

        _resetForm: function () {
            $("#" + this.cid + "_FilterDepartment").jstree("uncheck_node", $("#" + this.cid + "_FilterDepartment").find("li"));
            $("#" + this.cid + "_DeptForJobtitle").jstree("uncheck_node", $("#" + this.cid + "_DeptForJobtitle").find("li"));
            $("#" + this.cid + "_JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
        },
        _show: function () {
            var that = this;
            that.$('#dgUsers, #dgJobtitle, #dgDeptJob').each(function () {
                $(this).customDropdown({
                    css: {
                        width: 300,
                        height: 'auto'
                    }
                });
            });
            that._resetForm();
        },
        _bindCallback: function (donViNhan) {
            // Bind các sự kiện change tree và change jobtitle
            // Gọi đến hàm callback cho các sự kiện này
            var that = this;

            $('#' + this.cid + '_FilterDepartment, #' + this.cid + '_DeptForJobtitle').bind('change_state.jstree', function () {
                that._onSelectDvn();
            });

            $('#' + this.cid + '_JobtitleForDept :checkbox').on("click", function () {
                that._onSelectDvn();
            });

            var isCheckedPosition;
            var donViNhanArr = JSON.parse(donViNhan || '[]');
            donViNhanArr.forEach(function (donVi) {
                if (donVi.value === 'all') {
                    var $div = $('#' + that.cid + '_FilterDepartment');
                    var root = $div.find('li[rel=dept]:not([idext*="."])');
                    if (root.length) $div.jstree('check_node', root);
                    return;
                }
                var tree = donVi.value.split('-')[0];
                var value = donVi.value.split('-')[1];

                if (tree === 'FilterDepartment') {
                    var userId;
                    if (value.indexOf('_') >= 0) {
                        userId = +value.split('_')[1];
                    } else {
                        userId = +value;
                    }
                    var $div = $('#' + that.cid + '_FilterDepartment');
                    _selectUser($div, userId, true, that.allDepts, that.allUsers, that.allUserDeps);
                } else if (tree === 'DeptForJobtitle' || tree === 'JobtitleForDept') {
                    var deptIdExt = value.split("_")[0];

                    var $div = $('#' + that.cid + '_DeptForJobtitle');
                    _selectUser($div, deptIdExt, false, that.allDepts, [], that.allUserDeps);

                    if (!isCheckedPosition) {
                        var positionIds = value.split("_")[1].split(',');
                        _.each(positionIds, function (positionId) {
                            $('#' + that.cid + '_JobtitleForDept').find('input:checkbox' + (positionId == 0 ? '' : ('[value="' + positionId + '"]'))).click();
                        });
                        isCheckedPosition = true;
                    }
                }
            });
        },
        _showListDvn: function (node) {
            /// <summary>
            /// Hiển thị danh sách người dùng, phòng ban, chức danh- phòng ban được chọn lên
            ///<param name="node">Node chứa dach sách hiển thị</param>
            /// </summary>
            var destination = this.getDestination();
            if (!destination || destination.length <= 0) {
                return;
            }

            for (var i = 0; i < destination.length; i++) {
                var value = destination[i].value;

                var cssClass = "user";
                if (value === 'all') {
                    cssClass = 'all';
                }
                else if (value === '') {
                    cssClass = 'allJobtitle';
                }
                else if (value.indexOf('FilterDepartment-user_') !== -1) {
                    cssClass = 'dept-user';
                }
                else if (value.indexOf('FilterDepartment-') !== -1) {
                    cssClass = 'dept';
                }
                else if (value === 'JobtitleForDept') {
                    cssClass = 'jobtitleForDept';
                }
                else if (value.indexOf('DeptForJobtitle-') !== -1) {
                    cssClass = 'deptForJobtitle';
                }

                node.append(_parseUserItem(value, destination[i].label, cssClass));
            }
        },
        _onSelectDvn: function () {
            var containner = this.$('.co-process-user > ul');
            containner.empty();
            this._showListDvn(containner);
        },
        _onReleased: function (document, action) {
            var that = this;
            var docCopyId = document.model.get("DocumentCopyId");
            var transferSuccess = function (data) {
                /// <summary>
                /// tạo function sử dụng khi tranfer thành công, dùng cho cả 2 trường hợp văn bản có sự thay đổi hay ko
                /// </summary>
                /// <param name="data"></param>
                /// <param name="data"></param>
                /// <summary>
                if (data.success) {
                    egov.pubsub.publish(egov.events.status.success, data.success);
                    that._closeTab([docCopyId]);
                } else {
                    //if (!egov.isMobile) {
                    //    that.document.tab.display(true);
                    //}
                    egov.pubsub.publish(egov.events.status.error, data.error);
                    //egov.callback(callback);
                }
            };
            var transferError = function (message) {
                /// <summary>
                /// tạo function sử dụng khi tranfer lỗi, dùng cho cả 2 trường hợp văn bản có sự thay đổi hay ko
                /// </summary>
                /// <param name="data"></param>
                /// <summary>
                that.document.tab.display(true);
                that.document.tab.errorTab();

                message = message || _resource.transferError;
                egov.pubsub.publish(egov.events.status.error, message);
            };
            var doc = document.serialize();
            that.destination = {};
            var targetComments = that.getDestination();
            if (targetComments.length == 0) {
                egov.pubsub.publish(egov.events.status.error, "Bạn chưa chọn đơn vị nhận . Vui lòng thử lại");
                return;
            }
            that.destination.NextNodeId = action.nextNodeId || 0;
            that.destination.CurrentNodeId = action.currentNodeId || 0;
            that.destination.WorkflowId = action.workflowId || 0;
            that.destination.NewDocTypeId = document ? document.newDoctype : undefined;
            that.destination.TargetComment = JSON.stringify(targetComments);
            that.destination.ActionId = action.id || 0;
            var selectedFiles = {};
            document.attachments.model.each(function (file) {
                if (file.get('isNew') && !file.get('isRemoved')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });

            var removeFiles = document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });
            removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

            var modifiedFiles = document.attachments.modifiedFiles;

            // Cập nhật nội dung file đã sửa với những file vừa upload lên.
            // Đồng thời xóa file đó trong danh sách file đang chỉnh sửa.
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });

            eGovMessage.show(window.surveyMessReleased || "Bạn có chắc muốn phát hành phiếu khảo sát này?", null, eGovMessage.messageButtons.YesNo,
                function () {
                    egov.request.surveyRelease({
                        data: {
                            "doc": JSON.stringify(doc),
                            "destination": that.destination ? JSON.stringify(that.destination) : "",
                            "files": JSON.stringify(selectedFiles),
                            "modifiedFiles": JSON.stringify(modifiedFiles),
                            "removeAttachmentIds": removeFiles,
                            "storePrivateId": 0,
                            "destinationPlan": ""
                        },
                        success: function (data) {
                            transferSuccess(data);
                        },
                        error: function (data) {
                            transferError(data.statusMessage);
                        }
                    });
                }
            );

        },
        _saveSurveyToPdf: function (filename, data) {
            var that = this;
            var options = {
                fontSize: 14,
                margins: {
                    top: 18
                },
                format: [that.surveyDesLogic.pdfWidth || 210, that.surveyDesLogic.pdfHeight || 297]
            };
            var surveyPDF = new SurveyPDF.SurveyPDF(that.surveyDesLogic.JSON, options);
            surveyPDF.mode = "display";
            surveyPDF.data = data;
            surveyPDF.save(filename);
        },
        _closeTab: function (documentCopyIds) {
            ///<summay>
            /// Đóng tab văn bản khi bàn giao thành công
            ///<para name="documentCopyIds"> Có thể là mảng chứa danh sách id văn bản hoặc id văn bản</para>
            ///</summay>
            if (this.document) {
                this.document.isSaveChanged = true;
                if (this.document.tab) {
                    this.document.tab.close2(false);
                }
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
        _uncheckPrivateAnoun: function (e) {
            /// <summary>
            /// Bỏ check đơn vị nhận
            /// </summary>
            /// <param name="e">event</param>
            var that = this;
            var target = $(e.target).closest(':checkbox');
            if (target.length === 0) return;

            var value = e.target.value.split("-");
            var isAllUserSelected = value[0] === "all";
            var isUserOrDeptSelected = value[0] === "FilterDepartment";
            var isPosDeptSelected = value[0] === "DeptForJobtitle" || value[0] === "JobtitleForDept";

            if (isAllUserSelected) {
                $("#" + this.cid + "_JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
                $("#" + this.cid + "_DeptForJobtitle").jstree("uncheck_node", $("#" + this.cid + "_DeptForJobtitle").find("li"));
                $("#" + this.cid + "_FilterDepartment").jstree("uncheck_node", $("#" + this.cid + "_FilterDepartment").find("li"));
                return;
            }

            if (isUserOrDeptSelected) {
                $("#" + this.cid + "_" + value[0]).jstree("uncheck_node", $("#" + this.cid + "_" + value[0]).find("li#" + value[1]));
                return;
            }

            if (isPosDeptSelected) {
                var positionIds = value[1].split("_")[1].split(',');
                var deptIdExt = value[1].split("_")[0];

                $("#" + this.cid + "_DeptForJobtitle").jstree("uncheck_node", $("#" + this.cid + "_DeptForJobtitle").find("li[idext='" + deptIdExt + "']"));
                var selecteds = $("#" + this.cid + "_DeptForJobtitle").jstree("get_checked", null, false);
                if (selecteds.length === 0) {
                    _.each(positionIds, function (positionId) {
                        positionId == 0
                            ? $("#" + that.cid + "_JobtitleForDept").find("input:checkbox").removeAttr("checked")
                            : $("#" + that.cid + "_JobtitleForDept").find("input:checkbox[value='" + positionId + "']").removeAttr("checked");
                    });
                }
            }
            this._onSelectDvn();
        }
    });

    //#region Private Methods

    var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" hasReceiveDocument="${attr.hasReceiveDocument}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewCheckboxTemplate += '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var plugins = ["themes", "json_data", "ui", "crrm"];
    function decodeHTML(text) {
        var textArea = document.createElement('textarea');
        textArea.innerHTML = text;
        return textArea.value;
    }
    var _doGetCompletions = function (questions, prefix, completer) {
        if (completer === void 0) { completer = null; }
        var completions = [];
        if (!!questions) {
            var operationsFiltered = operations.filter(function (op) { return !prefix || op.value.indexOf(prefix) !== -1; });
            var questionsFiltered = questions.filter(function (op) { return !prefix || op.name.indexOf(prefix) !== -1; });
            completions = completions
                .concat(questionsFiltered.map(function (q) {
                    return {
                        completer: completer,
                        name: "",
                        value: "{" + q.name + "}",
                        some: "",
                        meta: q.title,
                        identifierRegex: ID_REGEXP,
                    };
                }))
                .concat(operationsFiltered.map(function (op) {
                    return {
                        name: "",
                        value: op.value,
                        some: "",
                        meta: op.title,
                        identifierRegex: ID_REGEXP,
                    };
                }));
        }
        return completions;
    }

    var _insertMatch = function (editor, data) {
        if (editor.completer.completions.filterText) {
            var allRanges = editor.selection.getAllRanges();
            for (var rangeIndex = 0, range; (range = allRanges[rangeIndex]); rangeIndex++) {
                range.start.column -= editor.completer.completions.filterText.length;
                var rangeText = editor.session.getTextRange(range);
                if (rangeText.indexOf("{") !== 0) {
                    var extRange = range.clone();
                    extRange.start.column--;
                    if (editor.session.getTextRange(extRange).indexOf("{") === 0) {
                        range = extRange;
                    }
                }
                editor.session.remove(range);
            }
        }
        editor.execCommand("insertstring", data.value || data);
    }

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

    var _parseUserItem = function (value, name, cssClass) {
        var result;
        var template = '<li class="list-group-item {2}">\
                                <div class="row">\
                                    <label class="mdl-checkbox mdl-js-checkbox checkbox document-color">\
                                       <input class="mdl-checkbox__input" name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                        <span class="document-color-1"><i class="icon-check"></i></span>\
                                    </label>\
                                    <span style="margin-left: 15px;">{1}</span>\
                                </div>\
                            </li>';
        result = $(String.format(template, value, name, cssClass));
        if (egov.isMobile) {
            $(result).find(".mdl-checkbox").materialCheckbox();
        }
        return result;
    };

    var _selectUser = function ($filterDept, value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
        // xác định các phòng ban user thuộc vào
        var departments = _.filter(arrDeptUserJobtitles, function (u) {
            return u.userid === value;
        });

        var isDept = false;
        if (departments.length === 0) {
            departments = _.filter(arrDept, function (u) {
                return hasUser ? u.value === value : u.attr.idext === value;
            });
            if (departments.length > 0) {
                isDept = true;
            }
        }

        if (departments) {
            for (var i = 0; max = departments.length, i < max; i++) {
                var parents = departments[i].idext.split('.');
                if (parents) {
                    if (!hasUser) value = departments[i].value;
                    // Tìm đến node có id là node đang được chọn.
                    var userItem = 'li#' + (isDept ? '' : 'user_') + value;
                    var userItems = $filterDept.find(userItem);
                    if (typeof userItems !== 'undefined' && userItems.length > 0) {
                        // Nếu tồn tại thì check cho node đó
                        $filterDept.jstree("check_node", userItems);
                        // return;
                    }

                    for (var j = 0; max1 = parents.length, j < max1; j++) {
                        // Xác định node cha
                        var parent = parents[j];
                        var parentItem = $filterDept.find('li[id=' + parent + ']');

                        // Nếu node cha tồn tại.
                        if (parentItem.length > 0) {
                            // và chưa mở thì thêm các node con của node cha tương ứng.
                            if (parentItem.hasClass('jstree-closed') && parentItem.children('ul').length === 0) {
                                _appendChild(parentItem, parseInt(parent),
                                    hasUser, true, arrDept, arrUsers, arrDeptUserJobtitles);
                            }
                            continue;
                        }
                        // Không thì thêm vào
                        var $parent = $('');
                        var parentId = 0;
                        if (j > 0) {
                            $parent = $filterDept.find('li[id=' + parents[j - 1] + ']');
                            parentId = parseInt(parents[j - 1]);
                        }
                        _appendChild($parent, parentId, hasUser, true,
                            arrDept, arrUsers, arrDeptUserJobtitles);

                        if (j === max1 - 1) {
                            _appendChild($filterDept.find('li[id=' + parent + ']'),
                                parseInt(parent), hasUser, true,
                                arrDept, arrUsers, arrDeptUserJobtitles);
                        }
                    }
                }
            }

            $filterDept.jstree("check_node", $filterDept
                .jstree("get_unchecked", null, true)
                .find(isDept ? 'li[rel=dept][id=' + value + ']' : 'li[rel!=dept][id=user_' + value + ']'));
        }
    };

    //#endregion

    return DocumentSurvey;
});
