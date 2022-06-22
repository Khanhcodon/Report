(function (eG) {
    eG.initialize = function () {
        this.$el = $(".overview");
        this.officeId = 1;
        this.$status = $(".alert");
        this.isLoading = false;
        this.loaded = 0;
        this.bindEvents();

        this.$el.find("#menuReport a").eq(1).click();
    };

    eG.bindEvents = function () {
        var $el = this.$el;
        var that = this;
        that.type = "3"; // mặc định hiển thị tháng hiện tại

        formatDateAndShowDatepicker(that.type);

        $el.find("#menuReport a").click(function (e) {
            var target = $(e.target).closest("a");
            that.reportUrl = target.attr("href");
            if (!that.reportUrl || that.reportUrl == "") {
                e.preventDefault();
                return false;
            }

            that.reportName = target.text();
            that.container = $(target.attr("tabs"));

            target.addClass("active");
            target.siblings().removeClass("active");

            that.container.show();
            that.container.siblings().hide();

            $el.find("#btnReport").click();

            e.preventDefault();
            return false;
        });

        $el.find("#btnReport").click(function () {
            that.loaded = 0;
            that.showStatistic();
        });

        $el.find("#clearCache").click(function () {
            that.clearCache();
        });

        $el.find("#dateConditions li a").click(function () {
            var type = $(this).attr("type");
            that.type = type;
            formatDateAndShowDatepicker(type);

            $("#dateConditionCurrent").text($(this).text());
        });

        $(".statistics-time select").change(function () {
            formatDateAndShowDatepicker(that.type);
        });

        $el.find("#btnExportExcel, #btnExportWord, #btnExportPdf").click(function () {
            var type = $(this).is("#btnExportPdf") ? 1 :
                       $(this).is("#btnExportWord") ? 2 :
                       3
            var content = that.container.html();
            content += that._exportStyle();
            $.ajax({
                type: "post",
                url: "/Statistics/ExportXlvb",
                data: {
                    type: type,
                    content: content,
                    name: that.reportName
                },
                success: function (result) {
                    if (result) {

                        $("<a>").attr("href", result).attr("target", "_blank")[0].click();
                        // window.open(result, "_blank");
                    }
                }
            });
        });
        $el.find(".label-group").click(function (e) {
            var $target = $(e.target).closest("a");
            var groups = $el.find("[group='" + $target.attr("groups") + "']");
            groups.toggle();
            var collapse = $target.find("i");
            collapse.eq(0).toggle();
            collapse.eq(1).toggle();
        })
    }

    eG.showStatistic = function () {
        if (this.isLoading) {
            return;
        }

        this.isLoading = true;

        this.conditions = this.getReportCondition();

        $(".processingDocs").show();

        this.GetDataReport();
    }

    eG.clearCache = function () {
        var getResportUrl = "/webapi/StatisticApi/ClearCache";
        var that = this;
        var idx = 0;
        eG.offices.forEach(function (office) {
            $.get(office.ServiceUrl + getResportUrl).always(function () {
                idx++;
                if (idx === eG.offices.length) {
                    that.$el.find("#btnReport").click();
                }
            });
        });
    },

    eG.GetDataReport = function () {
        var getResportUrl = this.reportUrl;
        var that = this;

        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId || o.OfficeId == 1;
        });

        if (office == null) {
            return;
        }

        var url = parseServiceUrl(office.ServiceUrl + getResportUrl);

        office.error = false;
        //that.$status.fadeIn("fast");
        waitingDialog.show();
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            timeout: 90 * 1000, // 30s
            success: function (data) {
                if (data.error) {
                    return;
                }

                if (data.hasGetOldDocument) {
                    that.$el.find("#HasTonKyTruoc").parents("li").show();
                } else {
                    that.$el.find("#HasTonKyTruoc").parents("li").hide();
                }

                if (data.groups) {
                    that._bindGroups(data.groups);
                }

                if (data.hasShowTotal) {
                    var countGroup = _.filter(data.data, function (item) {
                        if (item.IsGroup) {
                            return item;
                        }
                    })
                    that.$el.find(".total").show().text("Tổng số văn bản " + (data.data ? data.data.length - countGroup.length : 0));
                } else {
                    that.$el.find(".total").hide().text("");
                }

                that.bindAll(data.template, data.data);
            },
            error: function () {
                // office.error = true;
            },
            complete: function () {
                that.isBindAll = false;
                that.isLoading = false;
                waitingDialog.hide();
                //that.$status.fadeOut("fast");
            }
        });
    }

    eG.bindAll = function (templateId, model) {
        var template = $(templateId);
        var that = this;

        if (template.length === 0) {
            return;
        }

        this.container.find("table tbody").html($.tmpl(template, model));

        this.container.find("table tbody .isParent").click(function (e) {
            var groupBy = $(e.target).closest(".isParent").attr("group-by");
            that._toggleGroupby(groupBy);
        });
    }

    eG.getReportCondition = function () {
        return {
            hasOldDocument: $("#HasTonKyTruoc").is(":checked"),
            ViewType: this.viewType || "department",
            GroupBy: this.groupBy || "",
            from: $("#from").datepicker("getDate").startOf("day").format("yyyy-MM-ddTHH:mm:ss"),
            to: $("#to").datepicker("getDate").endOf("day").format("yyyy-MM-ddTHH:mm:ss")
        };
    },

    eG._toggleGroupby = function (groupBy) {
        this.$el.find("tr[group-data='" + groupBy + "']").toggleClass("hidden");
    },

    eG._bindGroups = function (groups) {
        var that = this;
        that.$el.find(".overdue-group ul").empty();
        _.each(_.keys(groups), function (key) {
            that.$el.find(".overdue-group ul").append('<li><a href="#" value="' + key + '">' + groups[key] + '</a></li>');
        });

        that.$el.find(".overdue-group ul a").click(function (e) {
            that.groupBy = $(e.target).closest("a").attr("value");
            that.viewType = $(e.target).closest("a").attr("value");
            that.$el.find("#btnReport").click();
        });

    },

    eG._exportStyle = function () {
        return '<style type="text/css"> table { border-collapse: collapse; } table td { border: 1px solid; } </style>';
    }
})
(this.eGovOverview = this.eGovOverview || {});

(function () {
    bindDatepicker();
    bindYearSelect();
    bindDefaultSelect();

    eGovOverview.initialize();
})
();

function bindDatepicker() {

    $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);

    $("#from").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 2,
        dateFormat: "d/mm/yy",
        onClose: function (selectedDate) {
            $("#to").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#to").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 2,
        dateFormat: "d/mm/yy",
        onClose: function (selectedDate) {
            $("#from").datepicker("option", "maxDate", selectedDate);
        }
    });

    formatDateAndShowDatepicker("3");
}

function formatDateAndShowDatepicker(type) {
    var from, to;
    $(".statistics-time select").addClass("hidden");
    switch (type) {
        case "1": // trong ngày
            from = new Date().startOf("day");
            to = new Date().endOf("day");
            break;
        case "2": // trong tuần
            from = new Date().startOf("week");
            to = new Date().endOf("week");
            break;
        case "3": // Trong tháng
            $(".statistics-time #monthSelect").removeClass("hidden");
            var currentMonth = $(".statistics-time #monthSelect").val();
            from = new Date().month(currentMonth).startOf("month");
            to = new Date().month(currentMonth).endOf("month");
            break;
        case "4": // Trong quý
            var quarter = $(".statistics-time #quarterSelect").val();
            var firstMonthInQuarter = (quarter - 1) * 3 + 1;
            from = new Date().date(1).month(firstMonthInQuarter).startOf("quarter");
            to = new Date().date(1).month(firstMonthInQuarter).endOf("quarter");
            $(".statistics-time #quarterSelect").removeClass("hidden");
            break;
        case "5": // Trong năm
            $(".statistics-time #yearSelect").removeClass("hidden");
            var currentYear = $(".statistics-time #yearSelect").val();
            from = new Date().year(currentYear).startOf("year");
            to = new Date().year(currentYear).endOf("year");
            break;
        case "6":
        default:
            from = to = new Date();
            break;
    }

    from = from.format("dd/MM/yyyy");
    to = to.format("dd/MM/yyyy");
    $("#from").val(from);
    $("#to").val(to);
}

function bindYearSelect() {
    var currentYear = new Date().year();
    var yearSelect = $("#yearSelect");
    var from = currentYear - 4;
    for (var i = from ; i <= currentYear; i++) {
        yearSelect.append($("<option>").val(i).text("Năm " + i));
    }
}

function bindDefaultSelect() {
    var currentMonth = new Date().month();
    var currentQuarter = new Date().quarter();
    var currentYear = new Date().year();

    $("#monthSelect option[value='" + currentMonth + "']").attr("selected", "selected");
    $("#quarterSelect option[value='" + currentQuarter + "']").attr("selected", "selected");
    $("#yearSelect option[value='" + currentYear + "']").attr("selected", "selected");
}

function imageError(that) {
    $(that).attr("src", "/AvatarProfile/noavatar.jpg");
}

function parseServiceUrl(url) {
    if (location.protocol === "https:") {
        url = url.replace("http:", "https:");
    }

    return url;
}
