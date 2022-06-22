
(function () {
    var request = {
        create: '/Calendar/Create',
        update: '/Calendar/Update',
        get: '/Calendar/Get',
        gets: '/Calendar/GetCalendars',
        daily: '/Calendar/GetDaily',
        weekly: '/Calendar/GetWeekly',
        monthly: '/Calendar/GetMonthly',
        manager: '/Calendar/GetManager',
        'delete': '/Calendar/Delete',
        'accept': '/Calendar/Accept',
        createResource: '/Calendar/AddResource',
        'deleteResource': '/calendar/DeleteResource'
    };

    var Calendar = Backbone.View.extend({
        el: "#egovCalendar",

        template: $("#calendarTemplate"),
        adminTemplate: $("#calendarManagerTemplate"),

        events: {
            "click #daily": "_viewDailyCalendar",
            "click #monthly": "_viewMonthlyCalendar",
            "click #weekly": "_viewWeeklyCalendar",
            "click #manager": "_showManager",
            "click #btnSubmit": "_submit",
            "click #btnAddNew": "_showCreateOrEdit",
            "click .newContent a": "_createContent",
            "click .deleteContent": "_removeContent",
            "click .delete": "_delete",
            "click .accept": "_accept",
            "click .removeUser": "_removeUserJoin",
            "click .resource a": "_selectResource",
            "click .viewDetail": "_edit",
            "click #printerPage": "_print",
            'click #export': "_export",
            'click #nextTime': 'next',
            'click #prevTime': 'prev',
            'change .hasPublish': '_toggleTitlePublish'
        },

        initialize: function () {
            this.isAdmin = window.hasPermision;
            this.isFromDocument = window.isFromDocument;
            this.render();
        },

        render: function () {
            setConfigDefaults();
            var that = this;
            this._keyboard();
            this._showDefault();

            if (this.isFromDocument) {
                this._renderManager();

                window.createFromDocument = function (complete) {
                    that._submit(complete);
                }
            }
        },

        next: function () {
           this._actionPage(1) 
        },

        prev: function () {
            this._actionPage(-1)
        },

        _actionPage: function (day) {
            var dateJson, that = this, dateCurrent = that.types.firstDay;
            if (that.types.type == "daily") {
                dateJson = that._dateTimeUtil.addDays(dateCurrent, day);
            } else if (that.types.type == "weekly") {
                dateJson = that._dateTimeUtil.addWeeks(dateCurrent, day);
            } else {
                dateJson = that._dateTimeUtil.addMonths(dateCurrent, day);
            }
           
            var from = dateJson.firstDay
            var to = dateJson.lastDay
            that.types.firstDay = from;
            that.types.lastDay = to;

            this._getData(request.gets, function (result, fr, t) {
                $("#stringDate").text(dateJson.stringDate);
                that.$(".calendarTime").text("Lịch từ " + fr + " đến " + t);
                that._showCalendarList(that.template, result, that.$("#tbody_calendar"));
            }, { from: from.toISOString(), to: to.toISOString() });
            console.log(JSON.stringify(dateJson))
        },
        _keyboard: function () {
            // Xử lý phím tắt
            // - Tổ hợp phím tắt
            // - Phím di chuyển trong form tạo, sửa
            var that = this;

            this.$("#CreateForm .title").keyup(function () {
                that.$(".content").first().attr("placeholder", $(this).val());
                that.$(".titlePublish").attr("placeholder", $(this).val());
            });

            this.$el.on("keydown", function (e) {
                if (e.ctrlKey) {
                    // Ctrl + S
                    if (e.keyCode === KeyCode.s) {
                        e.preventDefault();
                        that._createNew();
                    }

                    // Ctrl + T
                    if (e.keyCode === KeyCode.d) {
                        e.preventDefault();
                        that._createContent(e);
                    }
                }
            });

            this.$(".userPrimary, .userSecondary").on("keypress", function (e) {
                if (e.keyCode == KeyCode.enter) {
                    $(e.target).siblings(".userPrimaryList, .userSecondaryList").append($.tmpl("<div value='${value}'>${label} <span class='pull-right removeUser'>xóa</span></div>", {
                        value: 0,
                        label: $(this).val()
                    }));
                }
            });
        },

        _showDefault: function () {
            var type = location.href.split('#')[1];
            if (type !== undefined && type != "" && this.$("#" + type).length === 1) {
                this.$("#" + type).click();
                return;
            }

            this.$("#weekly").click();
            //this.types = {
            //    type: "weekly"
            //}
        },

        _serialize: function () {
            if (!this._isValid()) {
                return null;
            }

            var result = {};
            result.BeginTime = this._getDateBegin();

            result.CalendarId = this.$("#CreateForm .calendarId").val();
            result.Title = this.$("#CreateForm .title").val();

            result.TitlePublish = this.$("#CreateForm .titlePublish").val();
            if (result.TitlePublish == "") {
                result.TitlePublish = result.Title;
            }
            result.PlacePublish = this.$("#CreateForm .placePublish").val();
            if (result.PlacePublish == "") {
                result.PlacePublish = result.Place;
            }

            result.Place = this.$("#CreateForm .place").val();
            result.DepartmentIdExt = this.$("#CreateForm  .office").val()? this.$("#CreateForm  .office").val(): "khong co";
            result.IsPrivate = this.$(".isPrimary").prop("checked");
            result.HasPublish = this.$(".hasPublish").prop("checked");
            result.Order = this.$(".order").val();
            result.Contents = [];

            result.UserPrimaryPublish = this.$("#CreateForm .userPrimaryPublish").val();
            this.$(".tblContents tbody tr").not(".newContent").each(function (i, contentRow) {
                if (i > 0 && $(contentRow).find(".content").val() == "") {
                    return false;
                }

                if (result.UserPrimaryPublish == "" && i == 0) {
                    result.UserPrimaryPublish = $(contentRow).find(".userPrimaryList").text().replace("xóa", "");
                }

                var newContent = {
                    CalendarDetailId: $(contentRow).find(".calendarDetailId").val(),
                    Content: $(contentRow).find(".content").val(),
                    UserPrimary: escape($(contentRow).find(".userPrimaryList").html()),
                    UserSecondary: escape($(contentRow).find(".userSecondaryList").html()),
                    Department: $(contentRow).find(".department").val(),
                    Joined: $(contentRow).find(".joined").val(),
                    Note: $(contentRow).find(".note").val(),
                    Prepare: $(contentRow).find(".prepare").val(),
                };

                result.Contents.push(newContent);
            });

            return result;
        },

        _isValid: function () {
            // Todo: validate ở đây
            if (this.$("#CreateForm .title").val() === '') {
                return false;
            }

            //var dateBegin = this._getDateBegin();
            //if (dateBegin == null) {
            //    return false;
            //}

            return true;
        },

        _getDateBegin: function () {
            var time = this.$("#CreateForm .timepicker").val();
            if (time === '') {
                return null;
            }

            var date = this.$("#CreateForm .datepicker").datepicker("getDate");

            var timeSplit = time.split(":");
            date.hours(timeSplit[0]);
            date.minutes(timeSplit[1]);

            return date.toServerString();
        },

        _submit: function (complete) {
            var that = this;
            var model = this._serialize();
            if (model == null) {
                alert("Dữ liệu nhập chưa đúng");
                return;
            }

            var url = model.CalendarId == 0 ? request.create : request.update;

            $.ajax({
                url: url,
                type: "POST",
                traditional: true,
                data: { model: JSON.stringify(model) },
                beforeSend: function () {
                    that._showStatus();
                },
                success: function (result) {
                    if (result.error) {
                        alert(result.message);
                        return;
                    }

                    that._reload();
                },
                error: function () {

                },
                complete: function () {
                    if (typeof complete === "function") {
                        complete();
                    }

                    that._hideStatus();
                }
            });
        },

        _showCreateOrEdit: function (e) {
            this.$(".create-form, #btnSubmit").removeClass("hidden");
            this.$("#btnAddNew").addClass("hidden");
        },

        _createContent: function (contentModel) {
            contentModel = contentModel || {};

            var createContentRow = this.$(".newContent");
            var template = '<tr><td><input type="hidden" id="" value="${CalendarDetailId}" class="calendarDetailId"/><div>' +
                                   '<textarea class="form-control content" rows="2" tabindex="5">${Content}</textarea></div></td>' +
                               '<td><textarea class="form-control department" rows="2" tabindex="6">${Department}</textarea></td>' +
                               '<td><input type="text" class="form-control input-sm userPrimary user" tabindex="5" placeholder="Cán bộ"/>' +
                                    '<input type="text" class="form-control input-sm userPrimary jobtitle" tabindex="5" placeholder="Chức danh"/><input type="hidden" value="[]" class="userSelected" />' +
                                    '<div class="userPrimaryList list-group"></div></td>' +
                               '<td><textarea class="form-control joined" rows="2" tabindex="8">${Joined}</textarea></td>' +
                               '<td><input type="text" class="form-control input-sm userSecondary user" tabindex="5" placeholder="Cán bộ"/>' +
                                    '<input type="text" class="form-control input-sm userSecondary jobtitle" tabindex="5" placeholder="Chức danh"/><input type="hidden" value="[]" class="userSelected" />' +
                                    '<div class="userSecondaryList list-group"></div></td>' +
                               '<td><textarea class="form-control prepare" rows="2" tabindex="5">${Prepare}</textarea></td>' +
                               '<td><textarea class="form-control note" rows="2" tabindex="9">${Note}</textarea></td><td><a href="#" class="deleteContent">xóa</a></td></tr>';

            var newContent = $($.tmpl(template, contentModel));
            createContentRow.before(newContent);
            newContent.find(".userPrimaryList").html(contentModel.UserPrimary);
            newContent.find(".userSecondaryList").html(contentModel.UserSecondary);
            newContent.find(".content").focus();
            this._bindAutoCompleteUser();
            this._indexForInput();
        },

        _delete: function (e) {
            var that = this;
            var target = $(e.target).closest(".delete");
            var id = target.attr("data-id");

            if (id == '') {
                return;
            }

            if (!confirm("Bạn có chắc muốn xóa lịch này?")) {
                return;
            }

            $.ajax({
                url: request.delete,
                type: "post",
                data: { calendarId: id },
                beforeSend: function () {
                    that._showStatus();
                },
                success: function (result) {
                    if (result) {
                        that._reload();
                    }
                },
                complete: function () {
                    that._hideStatus();
                }
            });
        },

        _edit: function (e) {
            var that = this;
            var target = $(e.target).closest(".viewDetail");
            var id = target.attr("data-id");

            $.ajax({
                url: request.get,
                type: "get",
                data: { calendarId: id },
                beforeSend: function () {
                    that._showStatus();
                },
                success: function (result) {
                    if (!result.data) {
                        return;
                    }

                    that._bindUpdateForm(result.data);
                },
                complete: function () {
                    that._hideStatus();
                }
            });
        },

        _bindUpdateForm: function (calendar) {
            if (!calendar) {
                return;
            }
            var that = this;

            this.$("#CreateForm .calendarId").val(calendar.CalendarId);
            this.$("#CreateForm .title").val(calendar.Title);
            this.$("#CreateForm .titlePublish").val(calendar.TitlePublish);
            this.$("#CreateForm .placePublish").val(calendar.PlacePublish);
            this.$("#CreateForm .userPrimaryPublish").val(calendar.UserPrimaryPublish);
            this.$("#CreateForm .place").val(calendar.Place);
            this.$("#CreateForm  .office").val(calendar.DepartmentIdExt);

            if (calendar.IsPrivate) {
                this.$("#CreateForm .isPrivate").prop("checked", "checked");
            }

            if (calendar.HasPublish) {
                this.$("#CreateForm .hasPublish").prop("checked", "checked");
                this._toggleTitlePublish();
            }

            var beginTime = Date.parse(calendar.BeginTime);

            if (beginTime) {
                this.$("#CreateForm .timepicker").val(beginTime.hours() + ":" + beginTime.minutes());
                this.$("#CreateForm .datepicker").datepicker("setDate", beginTime);
            }

            this.$(".tblContents tbody tr").not(".newContent").remove();
            var createContentRow = this.$(".newContent");

            _.each(calendar.Contents, function (content) {
                that._createContent(content);
            });
            this._createContent({}); // them dong trong

            this._showCreateOrEdit();
            this._bindAutoCompleteUser();
            this._indexForInput();
        },

        _accept: function (e) {
            var that = this;
            var target = $(e.target).closest(".accept");
            var id = target.attr("data-id");
            var isAccept = target.attr("data-accept") == "true";

            if (isAccept) {
                if (this.$(".timestart").text() == '00 giờ 00') {
                    alert("Chưa nhập ngày họp;");
                    return false;
                }
            }

            $.ajax({
                url: request.accept,
                type: "post",
                data: { calendarId: id, isAccept: isAccept },
                beforeSend: function () {
                    that._showStatus();
                },
                success: function (result) {
                    if (result) {
                        that._reload();
                    }
                },
                complete: function () {
                    that._hideStatus();
                }
            });
        },

        _removeContent: function (e) {
            var contentRow = $(e.target).parents("tr");
            contentRow.remove();
        },

        _indexForInput: function () {
            var startIndex = 5; // Index vào dòng đầu tiên của nội dung cuộc họp
            this.$(".tblContents tbody tr").not(".newContent").each(function (i, contentRow) {
                $(contentRow).find("input, textarea").attr("tabindex", startIndex++);
            });
        },

        _viewDailyCalendar: function (e) {
            var that = this;
            that.tabName = "daily";
            that.types = {
                type:  that.tabName,
                firstDay: new Date(),
                lastDay: new Date()
            }
            this.$("#spinTime").show();
            this._switchForm(false);
            this._activeMenuItem(e);

            this._getData(request.daily, function (result) {
                $("#stringDate").text("Hôm nay");
                that.$(".calendarTime").text("Lịch Hôm nay");
                that._showCalendarList(that.template, result, that.$("#tbody_calendar"));
            });
        },

        _viewWeeklyCalendar: function (e) {
            var that = this;
            that.tabName = "weekly";
            that.types = {
                type:  that.tabName,
                firstDay: new Date(),
                lastDay: new Date()
            }
            
            this.$("#spinTime").show();
            this._switchForm(false);
            this._activeMenuItem(e);

            this._getData(request.weekly, function (result, from, to) {
                $("#stringDate").text("Tuần này");
                that.$(".calendarTime").text("Lịch Tuần từ " + from + " đến " + to);
                that._showCalendarList(that.template, result, that.$("#tbody_calendar"));
            });
        },

        _viewMonthlyCalendar: function (e) {
            var that = this;
            that.tabName = "monthly";
            that.types = {
                type:  that.tabName,
                firstDay: new Date(),
                lastDay: new Date()
            }

            this.$("#spinTime").show();
            this._switchForm(false);
            this._activeMenuItem(e);

            this._getData(request.monthly, function (result, from, to) {
                $("#stringDate").text("Tháng này");
                that.$(".calendarTime").text("Lịch Tháng từ " + from + " đến " + to);
                that._showCalendarList(that.template, result, that.$("#tbody_calendar"));
            });
        },

        _showManager: function (e) {
            var that = this;
            that.tabName = "manager";
            this.$("#spinTime").hide();
            this._switchForm(true);
            this._activeMenuItem(e);
            this._renderManager(false);
        },

        _renderManager: function (isShowingDialog) {
            var that = this;
            that._getData(request.manager, function (result) {
                that.resource = result.resource;
                that.users = result.users;
                that.jobtitles = result.jobtitles;
                that.offices = result.offices;
                that.userId = result.userId;

                that._renderCreateForm();

                if (!isShowingDialog) {
                    result.privates.length > 0 ? that.$(".privateCalendar").show() : that.$(".privateCalendar").hide();
                    result.notAccepteds.length > 0 ? that.$(".notAcceptCalendar").show() : that.$(".notAcceptCalendar").hide();

                    that._showCalendarList(that.adminTemplate, result.privates, that.$("#tbody_calendar_private"));
                    that._showCalendarList(that.adminTemplate, result.accepteds, that.$("#tbody_calendar_accepted"));
                    that._showCalendarList(that.adminTemplate, result.notAccepteds, that.$("#tbody_calendar_notAccept"));
                }
            });
        },

        _renderCreateForm: function () {
            var that = this;

            this.$('.datepicker').datepicker();
            this.$('.datepicker').datepicker('setDate', new Date);

            this.$(".timepicker").timepicker({
                'scrollDefault': 'now',
                'timeFormat': 'H:i',
                scrollDefaultNow: true,
                minTime: '7:00',
                maxTime: '22:00pm',
            });

            if (that.resource) {
                that.$(".resource .createResource").parent().siblings().remove();
                that.$(".resource").append($.tmpl('<li><a href="#">${label} <span class="removeResource" data-id="${id}">xóa</span></a></li>', that.resource));

                that.$("#CreateForm .place").on("focus", function () {
                    that._bindAutocomplete(that.$("#CreateForm .place"), that.resource, function (e, ui) {
                        that.$("#CreateForm .place").val(ui.item.label);
                    });
                });

                that.$(".removeResource").click(function (e) {
                    var id = $(e.target).closest('.removeResource').attr("data-id");
                    $(e.target).parents("li").remove();

                    that.resource = _.reject(that.resource, function (r) {
                        return r.id == id;
                    });

                    that._removeResource(id);
                });
            }

            if (that.users) {
                that._bindAutoCompleteUser();
            }

            if (that.offices) {
                that.$(".office .isPrimary").siblings().remove();
                that.$(".office").append($.tmpl('<option value="${departmentIdExt}" class="isPrimary" {{if selected}}selected{{/if}}>${name}</option>', that.offices));
            }
        },

        _bindAutoCompleteUser: function () {
            var that = this;
            var userSelectedList;

            that.$("#CreateForm .userPrimary.user, #CreateForm .userSecondary.user").on("focus", function () {
                that._bindAutocomplete($(this), that.users, function (e, ui) {
                    var userId = ui.item.value;
                    if ($(e.target).siblings(".userPrimaryList, .userSecondaryList").find("li[value='" + userId + "'][isJobtitle='False']").length > 0) {
                        return;
                    }

                    userSelectedList = JSON.parse(that._getUserSelectedElement(e.target).val());
                    userSelectedList.push(ui.item);

                    that._getUserSelectedElement(e.target).val(JSON.stringify(userSelectedList));

                    that._showUserPrimary(userSelectedList, $(e.target).siblings(".userPrimaryList, .userSecondaryList"));
                    $(e.target).val("");
                });
            });


            that.$("#CreateForm .userPrimary.jobtitle").on("focus", function () {
                that._bindAutocomplete($(this), that.jobtitles, function (e, ui) {
                    var jobId = ui.item.value;
                    if ($(e.target).siblings(".userPrimaryList").find("li[value='" + jobId + "'][isJobtitle='True']").length > 0) {
                        return;
                    }

                    userSelectedList = JSON.parse(that._getUserSelectedElement(e.target).val());
                    userSelectedList.push(ui.item);

                    that._getUserSelectedElement(e.target).val(JSON.stringify(userSelectedList));

                    that._showUserPrimary(userSelectedList, $(e.target).siblings(".userPrimaryList"));
                    $(".userPrimary").val("");
                });
            });
        },

        _showUserPrimary: function (selectedUsers, $userList) {
            $userList.empty();
            selectedUsers = _.sortBy(selectedUsers, "positionLevel");
            $userList.append($.tmpl("<div value='${id}' isJobtitle='${isJobtitle}'>{{if isJobtitle && (value != 'Chủ tịch' || value != 'Giám đốc')}}Các {{else}}Đc {{/if}}${value} <span class='pull-right removeUser'>xóa</span></div>", selectedUsers));
        },

        _getUserSelectedElement: function (target) {
            return $(target).parents("td").find(".userSelected");
        },

        _createResource: function () {
            var newResource = prompt("Nhập tên địa điểm mới", "");
            var that = this;

            if (newResource && newResource != '') {
                $.ajax({
                    url: request.createResource,
                    data: { resource: newResource },
                    type: 'Post',
                    beforeSend: function () {
                        that.$(".resource").append('<li><a href="#">' + newResource + '</a></li>');
                    },
                    success: function () {
                        that.resource.push({
                            value: newResource,
                            label: newResource
                        });
                    }
                });
            }
        },

        _removeResource: function (id) {
            $.ajax({
                url: request.deleteResource,
                data: { id: id },
                type: 'Post',
                beforeSend: function () {
                    // that.$(".resource").append('<li><a href="#">' + newResource + '</a></li>');
                },
                success: function () {

                }
            });
        },

        _selectResource: function (e) {
            var target = $(e.target).closest("a");
            if (target.is(".createResource")) {
                this._createResource();
                return;
            }

            this.$("#CreateForm .place").val(target.text().replace('xóa', '').trim());
        },

        _removeUserJoin: function (e) {
            var userId = $(e.target).closest('li').attr("value");

            var userSelectedList = JSON.parse(this._getUserSelectedElement(e.target).val());
            userSelectedList = _.reject(userSelectedList, function (u) {
                return u.id == userId;
            });
            this._getUserSelectedElement(e.target).val(JSON.stringify(userSelectedList));

            this._showUserPrimary(userSelectedList, $(e.target).parents('tr').find('.userPrimaryList'));
        },

        _showCalendarList: function (template, data, container) {
            var data = this._parseGroup(data);
            container.html('<tr><td colspan="8">Không có Lịch</td></tr>');

            if (data.length > 0) {
                container.html($.tmpl(template, data));

                container.find(".removeUser").remove();
                container.find(".userPrimaryList  li[isGroup]").remove();
            }
        },

        _switchForm: function (isManager) {
            if (isManager) {
                this.$(".managerCalendar").show();
                this.$(".viewCalendar").hide();
            } else {
                this.$(".managerCalendar").hide();
                this.$(".viewCalendar").show();
            }
        },

        _activeMenuItem: function (e) {
            var target = $(e.target).closest(".list-group-item");
            target.siblings().removeClass("active");
            target.addClass("active");
        },

        _getData: function (url, success, data) {
            var that = this;
            dataAjax = data || {};
            $.ajax({
                url: url,
                type: "Get",
                data: dataAjax,
                dataType: 'json',
                beforeSend: function () {
                    that._showStatus();
                },
                success: function (result) {
                    success(result.data, result.from, result.to);
                },
                error: function (xhr) {
                    console.log(xhr);
                },
                complete: function () {
                    that._hideStatus();
                }
            });
        },

        _parseGroup: function (data) {
            if (!data || data.length === 0) {
                return [];
            }

            var groups = _.groupBy(data, 'Date');
            var that = this;
            var result = [];

            _.each(groups, function (group, key) {
                var rowSpan = group.length + 1; // 1 là row mặc định của group
                _.each(group, function (calendar) {
                    calendar.IsMe = calendar.UserCreatedId == that.userId;

                    if (calendar.Contents.length > 1) {
                        rowSpan += calendar.Contents.length;
                    }
                });

                result.push({
                    Date: key,
                    BeginTime: group[0].BeginTime,
                    IsAdmin: that.isAdmin,
                    Count: rowSpan,
                    Calendars: group
                });
            });

            result = _.sortBy(result, "BeginTime");

            return result;
        },

        _bindAutocomplete: function (target, source, onSelected) {
            target.autocomplete({
                source: function (request, response) {
                    // Tìm kiếm không dấu
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    // Tìm kiếm có dấu
                    var matcherStrip = new RegExp($.ui.autocomplete.escapeRegex(egov.utilities.string.stripVietnameseChars(request.term)), "i");
                    response($.grep(source, function (value) {
                        value = egov.utilities.string.stripVietnameseChars(value.label);
                        return matcher.test(value) || matcher.test(value) && matcherStrip.test(value) || matcherStrip.test(value);
                    }));
                },

                minLength: 2,
                select: function (e, ui) {
                    if (onSelected) {
                        onSelected(e, ui);
                    }
                    return false;
                }
            }).data("autocomplete")._renderItem = function (ul, item) {
                ul.addClass('dropdown-menu');
                return $("<li>")
                    .data("item.autocomplete", item)
                    .append("<a href='#' style='white-space: initial;'>" + item.label + "</a>")
                    .appendTo(ul);
            };
        },

        _showStatus: function () {
            this.$(".status").show();
        },

        _hideStatus: function () {
            this.$(".status").hide();
        },

        _reload: function () {
            window.location.href = '/calendar/index#manager';
            window.location.reload();
        },

        _export: function () {
            this._parseExportStyle();
            var content = $(".viewCalendar").html();
            if (this.tabName == "manager") {
                var $magager = $('.managerCalendar');
                $magager.find('#CreateForm').remove();
                $magager.find("a.viewDetail").remove();
                $magager.find("a.delete").remove();
                content = $magager.html();
            }
            $.ajax({
                type: "post",
                url: "/Download/Export",
                data: {
                    type: 2,
                    content: content,
                    name: $(".calendarTime").text()
                },
                success: function (result) {
                    if (result) {
                        $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                    }

                    window.location.reload();
                }
            });
        },

        _toggleTitlePublish: function () {
            this.$(".calendarPublish").toggleClass("hidden");
        },

        _parseExportStyle: function () {
            $(".viewCalendar *").css({ 'fontFamily': 'TimeNewRoman', 'fontSize': 12 }); //Tahoma, Arial, Helvetica, sans-serif
            $(".viewCalendar th:last-child, .viewCalendar td:last-child").remove();
            $(".managerCalendar *").css({ 'fontFamily': 'TimeNewRoman', 'fontSize': 12 }); //Tahoma, Arial, Helvetica, sans-serif
            $(".managerCalendar th:last-child, .managerCalendar td:last-child").remove();
        },

        _print: function () {
            $(".viewCalendar").jqprint();
        },
        _dateTimeUtil:  {
            formatDate: function (d) {
                function pad(s) { return (s < 10) ? '0' + s : s; }
                return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
            },
            getCurrentWeek: function () {
                var date = new Date();
                var dt = new Date(date.getFullYear(), 0, 1);
                return Math.ceil((((date - dt) / 86400000) + dt.getDay() + 1) / 7);
            },
            getWeek: function (date) {
                var dt = new Date(date.getFullYear(), 0, 1);
                return Math.ceil((((date - dt) / 86400000) + dt.getDay() + 1) / 7);
            },
            getWeek: function (date) {
                var dt = new Date(date.getFullYear(), 0, 1);
                return Math.ceil((((date - dt) / 86400000) + dt.getDay() + 1) / 7);
            },
            getFirstDayOfWeek: function (year, wn) {
                return this.w2date(year, wn, 0);
            },
            getLastDayOfWeek: function (year, wn) {
                return this.w2date(year, wn, 6);
            },
            w2date: function (year, wn, dayNb) {
                var j10 = new Date(year, 0, 10, 12, 0, 0),
                   j4 = new Date(year, 0, 4, 12, 0, 0),
                   mon1 = j4.getTime() - j10.getDay() * 86400000;
                var date = new Date(mon1 + ((wn - 1) * 7 + dayNb) * 86400000);
                date = new Date(date.toDateString())
                return date;
            },
          
            addDays: function (date, days) {
                var that = this;
                var result = new Date(date);
                result.setDate(result.getDate() + days);
                var dateDiff = this.dateDiffInDays(new Date(), result)
                var stringDate = this.getFormatDay(dateDiff, that.formatDate(result));
                return {
                    firstDay: new Date(result.setHours(0)),
                    lastDay: new Date(result.setHours(23)),
                    stringDate: stringDate
                }
            },

            addWeeks: function (date, weeks) {
                var days = weeks * 7;
                date.setDate(date.getDate() + days);
                var firstOfWeek = date.setDate(date.getDate() - date.getDay())
                var lastOfWeek = date.setDate(date.getDate() + (7 - date.getDay()))
                var weekCurrent = this.getCurrentWeek();
                var weekAction = this.getWeek(new Date(firstOfWeek));
                var diffWeekNumber = weekAction - weekCurrent;
                var stringDate = this.getFormatTime(diffWeekNumber, weekAction, "Tuần");
                return {
                    firstDay: new Date(firstOfWeek),
                    lastDay: new Date(lastOfWeek),
                    stringDate: stringDate
                };
            },

            addMonths: function (date, months) {
                var dateMonth = date.setMonth(date.getMonth() + months);
                var dateFirstMonth = date.setDate(1);
                date.setMonth(date.getMonth() + 1);
                var dateLastMonth = date.setDate(date.getDate() - 1);
                var monthCurent = new Date().getMonth();
                var monthAction = date.getMonth();
                var diffMonthNumber = monthAction - monthCurent;
                var stringDate = this.getFormatTime(diffMonthNumber, monthAction + 1, "Tháng");
                return {
                    firstDay: new Date(dateFirstMonth),
                    lastDay: new Date(dateLastMonth),
                    stringDate: stringDate
                }
            },

            dateDiffInDays: function (a, b) {
                // Discard the time and time-zone information.
                const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
                const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

                return Math.floor((utc2 - utc1) / (1000 * 60 * 60 * 24));
            },
            getFormatTime: function (diff, time, strFirst) {
                switch (diff) {
                    case -1:
                        return strFirst + " trước"
                        break;
                    case 0:
                        return strFirst + " này"
                        break;
                    case 1:
                        return strFirst + " sau"
                        break;
                    default:
                        return strFirst + " " + time
                }
            },
            getFormatDay: function (diff, time) {
                switch (diff) {
                    case -2:
                        return "Hôm kia"
                        break;
                    case -1:
                        return "Hôm qua"
                        break;
                    case 0:
                        return "Hôm nay"
                        break;
                    case 1:
                        return "Ngày mai"
                        break;
                    case 2:
                        return "Ngày kia"
                        break;
                    default:
                        return "Ngày " + time
                }
            }
        }
    });

    function setConfigDefaults() {
        $.datepicker.setDefaults({
            dateFormat: "dd/mm/yy",
            gotoCurrent: true,
            changeMonth: true,
            changeYear: true
        });
        $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
    }

    window.eGovCalendar = new Calendar;
})();