// Lich
var Calendar = Backbone.Model.extend({
    defaults: function () {
        return {
            stt: "",
            CalendarId: "-1",
            BeginTime: "",
            EndTime: "",
            Title: "",
            DepartmentPrimary: "",
            UserJoin: "",
            Location: "",
            Note: "",
            IsSms: false,
            Cause:""
        };
    }
});

// Danh sach lich
var ListCalendar = Backbone.Collection.extend({
    model: Calendar,
    sort_key: 'BeginTime', // default sort key

    comparator: function (item) {
        return !item.get(this.sort_key);
    },

    sortByField: function (fieldName) {
        this.sort_key = fieldName;
        this.sort();
    },
});
(function (calendar, $, undefined) {
    calendar.model = {};

    calendar.model.emptyModel = function () {
        return {
            CalendarId: "-1",
            BeginTime: "",
            EndTime: "",
            Title: "",
            DepartmentPrimary: "",
            UserJoin: "",
            Location: "",
            Note: "",
            IsSms: false
        };
    }

    calendar.model.setModel = function (model) {
        return {
            CalendarId: model.get('CalendarId'),
            BeginTime: model.get("BeginTime"),
            EndTime: model.get("EndTime"),
            Title: model.get("Title"),
            DepartmentPrimary: model.get("DepartmentPrimary"),
            UserJoin: model.get("UserJoin"),
            Location: model.get("Location"),
            Note: model.get("Note"),
            IsSms: model.get("IsSms")
        };
    }

    calendar.model.setModelConformRow = function (row) {
        return {
            BeginTime: row.children().eq(1).children().attr("data-time"),
            EndTime: row.children().eq(2).children().attr("data-time"),
            Title: row.children().eq(3).children().val(),
            DepartmentPrimary: row.children().eq(4).children().val(),
            UserJoin: row.children().eq(5).children().val(),
            Location: row.children().eq(6).children().val(),
            Note: row.children().eq(7).children().val()
        };
    }
})(window.calendar = window.calendar || {}, window.jQuery);


