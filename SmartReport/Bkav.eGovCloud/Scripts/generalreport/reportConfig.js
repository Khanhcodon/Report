var reportConfig = reportConfig ? reportConfig : {};

(function () {
    var reportUrls = {
        gets: "/ReportViewer/GetAll",
        getTables: "/ReportViewer/GetAllTableName",
    };
    reportConfig.App = Backbone.View.extend({
        el: "#reportContent",
        reportTreeEl: $("#reportTree"),
        events: {

        },

        initialize: function () {
            var tableRelation = new reportConfig.TableRelation;
        },

        render: function () {
           
        }
    });

    reportConfig.TableRelation = Backbone.View.extend({
        el: "#tableRelation",
        events: {
            "click #addTables": "addTables"
        },

        initialize: function () {
            this.render();
        },

        render: function () {
            var that = this;

           
        },
        addTables: function () {
            var that = this;
            this._getTable(function (allTable) {
                that.model = allTable;
            });
        },

        _getTable: function (complete) {
            $.ajax({
                url: reportUrls.getTables,
                success: function (result) {
                    if (isFunc(complete)) {
                        complete(result);
                    }
                }
            });
        },

        _renderReportTree: function () {
            var that = this;
            this.reportTreeEl.append($.tmpl($(this.template), this.model));
        },
    });

    var Tables = Backbone.View.extend({
        el: "#reportContent",
        reportTreeEl: $("#reportTree"),
        events: {

        },

        initialize: function () {
            this.render();
        },

        render: function () {
            var that = this;
            this.currentPage = 1;

            this._getTables(function (allReport) {
                that.model = allReport;
                //that._renderReportTree();
            });
        },

        _getTables: function (complete) {
            $.ajax({
                url: reportUrls.gets,
                success: function (result) {
                    if (isFunc(complete)) {
                        complete(result);
                    }
                }
            });
        },

        _renderReportTree: function () {
            var that = this;
            this.reportTreeEl.append($.tmpl($(this.template), this.model));
        },
    });

    var isFunc = function (func) {
        return typeof func === "function";
    };

    reportConfig.App = new reportConfig.App;

})();