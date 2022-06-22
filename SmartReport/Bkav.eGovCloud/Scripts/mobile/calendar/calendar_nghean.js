

define([egov.template.calendar.item, egov.template.calendar.detail], function (ItemTemplate, DetailTemplate) {
    var request = {
        monthly: '/Calendar/GetMobile',
    };

    var Calendar = Backbone.View.extend({
        el: "#calendarFrame",
        calendarList: $("#calendarFrame .page-content"),
        detailEl: $(".calendar-detail"),
        template: ItemTemplate,
        detailTemplate: DetailTemplate,
        events: {
            'click #menu-calendar li': '_changeCalendarNode',
            'click #btnAddCalendar': '_addNew'
        },

        initialize: function () {
            this.render();
        },

        render: function () {
            var that = this;
            that._viewCalendar();
        },

        _addNew: function () {

        },

        _changeCalendarNode: function (e) {
            var target = $(e.target).closest('li');
            if (target.is(".active")) {
                // Load văn bản mới

                return;
            }

            target.siblings().removeClass("active");
            target.addClass("active");

            egov.mobile.autoHideMainMemu();
            this._viewCalendar();
        },

        _viewCalendar: function () {
            var that = this;
            var url = "";

            var menuActive = this.$("#menu-calendar li.active");
            var nodeName = menuActive.find(".node-name").text();
            this.$(".header-title").text(nodeName);

            var dataUri = menuActive.attr("data-url");
            this.calendarList.empty();

            this._getData(dataUri, function (result, from, to) {
                that.model = result;
                if (result.length === 0) {
                    that._showNoElementPage("perm_contact_calendar", nodeName);
                    return;
                }

                that._showCalendarList();
            });
        },

        _showCalendarList: function () {
            var that = this;
            var model = that._parseGroup(that.model);
            that._hideNoElementPage();

            that.calendarList.html($.tmpl(this.template, model));

            that.$el.find("li.mdl-list__item").click(function (e) {
                var calendarId = $(e.target).closest("li").attr("data-id");
                that._showDetail(calendarId);
            });

            that.calendarList.find("li.mdl-list__item .mdl-list__item-text-body").dotdotdot();
        },

        _showNoElementPage: function (icon, message) {
            this.$(".no-element-content").removeClass("hidden");
            this.$(".no-element-content .material-icons").text(icon);
            this.$(".no-element-content .message-info").text(message);
            this.$(".page-content").addClass("hidden");
        },

        _hideNoElementPage: function () {
            this.$(".no-element-content").addClass("hidden");
            this.$(".page-content").removeClass("hidden");
        },

        _showDetail: function (calendarId) {
            var calendar = _.find(this.model, function (c) {
                return c.CalendarId == calendarId;
            });

            if (!calendar) {
                return;
            }

            egov.mobile.showDetailPage("calendar");

            this.detailEl.html($.tmpl(this.detailTemplate, calendar));
            this.detailEl.show();
            this.detailEl.find(".removeUser").remove();
        },

        _parseGroup: function (data) {
            if (!data || data.length === 0) {
                return [];
            }

            var groups = _.groupBy(data, 'Date');
            var that = this;
            var result = [];

            _.each(groups, function (group, key) {
                var hasMorning = false;
                _.each(group, function (calendar) {
                    var a = calendar.BeginTime.split(/[^0-9]/);
                    var beginDate = new Date(a[0], a[1] - 1, a[2], a[3], a[4], a[5]); // Date.parse(calendar.BeginTime + '.000Z');
                    calendar.Date = beginDate.getDate();
                    calendar.Month = beginDate.getMonth() + 1;
                    calendar.Hour = beginDate.getHours();

                    var minutes = beginDate.getMinutes();
                    if (minutes < 10) {
                        minutes = "0" + minutes;
                    }
                    calendar.Minutes = minutes;
                    calendar.key = key;
                    if (calendar.Hour < 12) {
                        hasMorning = true;
                    }
                });

                // Nếu hiển thị ngyaf vào buổi sáng thì ko hiển thị ngày vào buổi chiều nữa
                var isAfternoon = hasMorning ? new Date(group[0].BeginTime).getHours() > 12 : false;
                result.push({
                    BeginTime: group[0].BeginTime,
                    isAfternoon: isAfternoon,
                    Date: key,
                    Calendars: group
                });
            });

            result = _.sortBy(result, "BeginTime");

            return result;
        },

        _getData: function (url, success) {
            var that = this;
            $.ajax({
                url: url,
                type: "Get",
                beforeSend: function () {
                    // that._showStatus();
                },
                success: function (result) {
                    success(result.data, result.from, result.to);
                },
                error: function (xhr) {
                },
                complete: function () {
                }
            });
        }
    });

    return Calendar;
});