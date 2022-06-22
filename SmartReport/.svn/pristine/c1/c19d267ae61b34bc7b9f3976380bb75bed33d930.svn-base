(function (eG) {
    var RECORDPERPAGE = 50;
    eG.initialize = function () {
        this.$el = $(".overview");
        this.officeId = 1;
        this.$status = $(".alert");
        this.isLoading = false;
        this.loaded = 0;
        this.bindEvents();
        this.count = 1;
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
            that.isPaging = target.attr("ispaging");
            that.storeId = target.attr("data-storeId");
            that.statisticName = target.attr("statisticName");
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
            $("#backTree").hide()
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
            var settings = {
                type: type,
                content: content,
                name: that.reportName
            }

            if (that.isPaging) {
                settings = $.extend(that.settingExport, settings);
                settings.isPaging = that.isPaging;
                settings.statisticName = that.statisticName;
                settings.RecordPerPage = 100000;
                settings.IsGetAll = true;
                settings.StoreId = that.storeId;
            }
           
            
            $.ajax({
                type: "post",
                url: "/Statistics/ExportXlvb",
                data: settings,
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
        that.currentURL = url;
        office.error = false;
        //that.$status.fadeIn("fast");
        waitingDialog.show();
        var settings = that.getReportCondition();
        settings.IsFirstLoad = true;
        
        that.settingExport = settings;

        $.ajax({
            url: url,
            type: "GET",
            data: settings,
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
                    });
                    var totalNum = !isNaN(data.total) ? data.total : (data.data ? data.data.length - countGroup.length : 0)
                    that.$el.find(".total").show().text("Tổng số văn bản " + totalNum);
                } else {
                    that.$el.find(".total").hide().text("");
                }
                if (that.isPaging) {
                    that.bindAll(data.template, data);
                } else {
                    that.bindAllNormal(data.template, data);
                }
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
        this.count++;
        var that = this;
        idPage = 'page' + this.count;
        var templatePage = '<tr><td colspan=20><nav aria-label="Page navigation" class="pager">\
            <div class="col-md-10">\
            <ul class="pagination pull-right" id="' + idPage + '">\
            </ul></div>\
            <div class="col-md-6"><div style="width: 100px;"><div class="input-group go-to-page" style="margin: 21px 0px;"><input type="text" class="form-control input-sm"><span class="input-group-btn"><a class="btn btn-default btn-sm" type="button">Đi đến</a></span></div></div></div></nav></td></tr>'
        if (template.length === 0 || model.data.length === 0) {
            return;
        }
        var $container = this.container.find("table tbody");
        if (model.data[0].IsGroup) {
            $container.html($.tmpl(template, model.data));
        } else {
            $container.append(templatePage);
            that.Paging(false, idPage, model.total);
        }

        this.container.find("table tbody .isParent").click(function (e) {
            var groupBy = $(e.target).closest(".isParent").attr("group-by");
            var totalRecord = $(e.target).closest(".isParent").data("count");
            var isNotLoad = $(e.target).closest(".isParent").attr("isnotload");
            if (!isNotLoad) {
                that.count++;
                idPage = 'page' + that.count;
                templatePage = '<tr page-group="' + groupBy + '"><td colspan=20><nav aria-label="Page navigation" class="pager">\
                <div class="col-md-10">\
                <ul class="pagination pull-right" id="' + idPage + '">\
                </ul></div>\
                <div class="col-md-6"><div style="width: 100px;"><div class="input-group go-to-page" style="margin: 21px 0px;"><input type="text" class="form-control input-sm"><span class="input-group-btn"><a class="btn btn-default btn-sm" type="button">Đi đến</a></span></div></div></div></nav></td></tr>';
                $(this).closest("tr").after(templatePage);
                that.Paging(groupBy, idPage, totalRecord);
                $(e.target).closest(".isParent").attr("isnotload", true);
            } else {
                that._toggleGroupby(groupBy);
            }
            
        });
    }


    eG.bindAllNormal = function (templateId, model) {
        var template = $(templateId);
        var that = this;

        if (template.length === 0) {
            return;
        }

        this.container.find("table tbody").html($.tmpl(template, model.data));
        this.bindEvent();
        this.container.find("table tbody .isParent").click(function (e) {
            var groupBy = $(e.target).closest(".isParent").attr("group-by");
            that._toggleGroupby(groupBy);
            var over = $(e.target).closest(".isParent").attr("data-id");

            if (over == "overview") {
                var array = $("[group-by='" + groupBy + "']").siblings();
                _.each(array, function (item) {
                    var name = $(item).attr("group-by") ? $(item).attr("group-by") : $(item).attr("group-data");
                    if (name != groupBy) {
                        $(item).remove();
                    }
                });
                that.backCallBack = function () {
                    $("#menuReport a.active").click();
                }
                $("#backTree").show();
            }
        });
    }

    eG.bindEvent = function () {
        var that = this;
        $("[data-click='department']").click(function (e) {
            var type = $(this).attr("data-type");
            that.typeBack = 1;
            if (type && type == 1) {
                var deptName = $(this).attr("data-dept");
                $("[data-dept='" + deptName + "']").show();
                var array = $("[data-dept='" + deptName + "']").siblings();
                _.each(array, function (item) {
                    var name = $(item).attr("data-dept")
                    if (name != deptName) {
                        $(item).remove();
                    }
                });
                that.backCallBack = function () {
                    $("#th_vbden").click();
                }
                $("#backTree").show();
            };
           
        });
        $("[data-click='user']").click(function (e) {
            var type = $(this).attr("data-type");
            that.typeBack = 2;

            if (type && type == 2) {
                var userId = $(this).attr("data-userid");
                that.reportUrl = "/Statistics/Xlvb_den_all_normal?UserId=" + userId;
                that.container = $("#tonghop_vbden_detail");
                that.container.show();
                that.container.siblings().hide();
                that.GetDataReport();
                that.backCallBack = function () {
                    that.container = $("#tonghop_vbden_normal_basic");
                    that.container.show();
                    that.container.siblings().hide();
                    that.backCallBack = function () {
                        $("#th_vbden").click();
                    }
                }
                $("#backTree").show();
            }
        });

        $("[data-click='vbdi']").click(function (e) {
            var type = $(this).attr("data-type");
            that.typeBack = 1;
            if (type && type == 1) {
                var deptName = $(this).attr("data-dept");
                $("[data-dept='" + deptName + "']").show();
                var array = $("[data-dept='" + deptName + "']").siblings();
                _.each(array, function (item) {
                    var name = $(item).attr("data-dept")
                    if (name != deptName) {
                        $(item).remove();
                    }
                })
                that.backCallBack = function () {
                    $("#th_vbdi").click();
                }
                $("#backTree").show();
            };
        });

        $("[data-click='vbdiuser']").click(function (e) {
            var type = $(this).attr("data-type");
            that.typeBack = 2;

            if (type && type == 2) {
                var userId = $(this).attr("data-userid");
                that.reportUrl = "/Statistics/Xlvb_di_user?UserId=" + userId;
                that.container = $("#tonghop_vbdi");
                that.container.show();
                that.container.siblings().hide();
                that.GetDataReport();
                that.backCallBack = function () {
                    that.container = $("#tonghop_vbdihb");
                    that.container.show();
                    that.container.siblings().hide();
                    that.backCallBack = function () {
                        $("#th_vbdi").click();
                    }
                }
            }
        });

        $("[data-click='hoibao']").click(function (e) {
            var type = $(this).attr("data-type");
            that.typeBack = 2;

            if (type && type == 1) {
                var departExt = $(this).attr("data-dept");
                that.reportUrl = "/Statistics/Xlvb_all_hoibao?GroupName=" + departExt;
                that.container = $("#tonghop_hoibao");
                that.container.show();
                that.container.siblings().hide();
                that.GetDataReport();
            }
            that.backCallBack = function () {
                $("#vb_hb").click();
            }
            $("#backTree").show();
        });
        $("[data-click='addresshoibao']").click(function (e) {
            var type = $(this).attr("data-type");
            that.typeBack = 2;

            if (type && type == 1) {
                var departExt = $(this).attr("data-addressname");
                that.reportUrl = "/Statistics/Xlvb_di_hoibao_addressname?GroupName=" + departExt;
                that.container = $("#tonghop_hoibao_cqbh");
                that.container.show();
                that.container.siblings().hide();
                that.GetDataReport();
            }
            that.backCallBack = function () {
                $("#vb_hb_cqbh").click();
            }
            $("#backTree").show();
        });

        $("[data-click='document']").click(function (e) {
            var docCopyId = $(this).attr("data-doccopyid");
            if (top) {
                top.openDocument(docCopyId, "");
            }
        });

        $("#backTree").click(function (e) {
            that.backCallBack()
        });
    }

    eG.getReportCondition = function () {
        return {
            IsFirstLoad: true,
            hasOldDocument: $("#HasTonKyTruoc").is(":checked"),
            ViewType: this.viewType || "department",
            GroupBy: this.groupBy || "",
            from: $("#from").datepicker("getDate").startOf("day").format("yyyy-MM-ddTHH:mm:ss"),
            to: $("#to").datepicker("getDate").endOf("day").format("yyyy-MM-ddTHH:mm:ss")
        };
    },

    eG.Paging = function (groupBy, el, total) {
        var that = this;
        $("#" + el).paging(total, {
            format: '[< ncnnn >]',
            onSelect: function (page) {
                var setting = that.getReportCondition();
                setting.RecordPerPage = RECORDPERPAGE;
                setting.Page = page;
                setting.IsFirstLoad = false;
                setting.GroupName = groupBy ? groupBy : null;

                that.settingExport = setting;

                $.ajax({
                    url: that.currentURL,
                    type: "GET",
                    data: setting,
                    timeout: 90 * 1000, // 30s
                    success: function (data) {
                        var rowPage = $("#" + el).closest("tr");
                        if (!groupBy) {
                            rowPage.prevAll().remove();
                            rowPage.before($.tmpl($(data.template), data.data));
                        } else {
                            that.$el.find("tr[group-data='" + groupBy + "']").remove();
                            rowPage.before($.tmpl($(data.template), data.data));
                        }
                        if (data.total <= RECORDPERPAGE) {
                            rowPage.hide();
                        }
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
            },
            onFormat: function (type) {
                switch (type) {
                    case 'block': // n and c
                        if (this.value == this.page)
                            return '<li class="active"><a href="#">' + this.value + '</a></li>';
                        else 
                            return '<li class=""><a href="#">' + this.value + '</a></li>';
                    case 'next': // >
                        return '<li class=""><a href="#">&gt;</a></li>';
                    case 'prev': // <
                        return '<li class=""><a href="#">&lt;</a></li>';
                    case 'first': // [
                        return '<li class="btn-prev"><a href="#" value="1">&lt;&lt;</a></li>';
                    case 'last': // ]
                        return '<li class="btn-next"><a href="#" value="4">&gt;&gt;</a></li>';
                }
            },
            perpage: RECORDPERPAGE,
        });
    }

    eG._toggleGroupby = function (groupBy) {
        this.$el.find("tr[group-data='" + groupBy + "']").toggleClass("hidden");
        this.$el.find("tr[page-group='" + groupBy + "']").toggleClass("hidden");
        
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
