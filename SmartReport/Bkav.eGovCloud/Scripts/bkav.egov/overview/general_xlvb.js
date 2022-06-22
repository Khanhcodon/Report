(function (eG) {

    var defaultStatistic = {
        Name: "",
        PreExtisting: 0,
        NewReception: 0,
        Total: 0,
        Resolved: 0,
        SolvedInTime: 0,
        SolvedInTimePercent: 0,
        SolvedLate: 0,
        SolvedLatePercent: 0,
        UnResolved: 0,

        Pending: 0,
        PendingPercent: 0,
        PendingLate: 0,
        PendingLatePercent: 0
    };

    var color = {
        dunghan: "#48B9DD",
        quahan: "#FF5F11",
        toihan: "#FF5F11",
        chuadenhan: "#42CC80"
    };

    var varGlobal = {
        index: 0
    };

    function addComment(model) {
        var getResponseUrl = "/Common/GetAllUsers";
        eG.offices.forEach(function (office) {
            var url = office.ServiceUrl + getResponseUrl;
            office.error = false;
            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (allUsers) {
                    var tempCommentList = model.CommentList;
                    var comments = parseComment(tempCommentList, allUsers);
                    var CommentTemplate = $("#commentTemp").html();
                    $('#commentList').html(parseCommentNew(CommentTemplate, comments.Processor));

                    _.each(comments.Processor, function (item) {
                        var children = item.Children;
                        $('.commentChirdren .child' + item.CommentId).html(parseCommentNew(CommentTemplate, children));
                    });

                    if (comments.CoProcessor.length > 0) {
                        $('#coCommentList').html(parseCommentNew(CommentTemplate, comments.CoProcessor));
                        $('#coCommentList').addClass("open");
                        _.each(comments.CoProcessor, function (item) {
                            var children = item.Children;
                            $('.commentChirdren .child' + item.CommentId).html(parseCommentNew(CommentTemplate, children));
                        });
                    }
                    else {
                        $('#coCommentList').parent().hide();
                    }

                    //openCommentEvent();
                },
                error: function () {
                    console.log("No data return");
                }
            });
        });
    }

    var openCommentEvent = function () {
        var divHeight = 0;
        $('#commentList, #coCommentList').bindResources();
        if ($('.comment-detail .wraptext').length > 0) {
            require(['dotdotdot'], function () {
                that.$('.comment-detail .wraptext').dotdotdot({
                    watch: true,
                    height: 30
                });
            });
        }

        if (!egov.isMobile) {
            that.$('#commentList .comment-item, #coCommentList .comment-item').on("click", function () {
                // var commentItem = $(this).parent(".comment-item");
                $(this).toggleClass("open");
            });
        }
        else {
            that.$('.wrapCommentList .list-group-item').on("click", function () {
                $(this).toggleClass("selected");
            });
        }

        that.$(".displayAllComment").on("click", function (e) {
            if (egov.isMobile) {
                if (egov.mobile.showAllCommentInPopup) {
                    that._renderCommentDialog();
                } else {
                    that._renderCommentInPage();
                }
            }
            else {
                that.$('#commentList, #coCommentList').find(".dynamicComment").show();
                $(this).parents("li.list-group-item").remove();
            }

            e.stopPropagation();
        });
    }

    // Các loại ý kiến xử lý
    var commentType = { Common: 1, Consulted: 2, Contribution: 3, Supplementary: 4, Signed: 5, Success: 6, Finished: 7, Reopen: 8 };

    // Xử lý tách các loại ý kiến xử lý từ danh sách ý kiến server trả về
    function parseComment(comments, allUsers) {
        var result = { CoProcessor: [], Processor: [] };
        var content;
        var commentTypeEnum = commentType;
        for (var i = 0; i < comments.length; i++) {
            var comment = comments[i];
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
                    comment.Description = String.format("Chuyển tới <span class='comment-received'>{0}</span>", transfers[0].label);
                    if (transfers.length > 1) {
                        var otherNumber = 0;
                        for (var j = 1; j < transfers.length; j++) {
                            var peoples = transfers[j].label;
                            otherNumber += peoples.split(",").length;
                        }
                        comment.Description += String.format(" và <span class='comment-received'>{0} người khác</span>", otherNumber);
                    }

                    //if (content.DateOverdue != null) {
                    //    comment.Description += String.format(" hạn xử lý <span class='comment-received'>{0}</span>", Globalize.parseDate(content.DateOverdue).format("dd/MM/yyyy"));
                    //}
                } else {
                    comment.Description = "Gửi ý kiến";
                }
                result.Processor.push(comment);
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


            //comment.avatar = String.format(egov.setting.avatarPath, comment.UserSend.username);

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
                    //child.avatar = String.format(egov.setting.avatarPath, child.UserSend.username);
                });
            }
        }

        return result;
    }

    //Parse lại comment do break line bị mất
    function parseCommentNew(commentTemplate, processor) {
        var commentsHtml = $.tmpl(commentTemplate, processor);
        for (var i = 0; i < commentsHtml.length; i++) {
            commentsHtml.eq(i).html(_.unescape(commentsHtml[i].innerHTML));
        }

        return commentsHtml;
    }

    eG.initialize = function () {
        this.totalElement = $("#statisticTotal");
        this.detailElement = $("#statisticDetail");
        //this.documentCopyTemplate = $("#documentcopy-detail");
        this.template = $("#statisticTemp");
        this.totalTemplate = $("#statisticSumTemp");
        this.overdueTemplate = $("#statisticOverdueTemp");
        this.$el = $(".overview");
        this.officeId = 0;
        this.$status = $(".alert");

        this.otherOverduesElement = $("#docOtherOverdue tbody");
        this.otherOverdueTemp = $("#otherOverduesTemp");

        this.loaded = 0;
        var that = this;

        that.filterStatus = $("#ddlFilterStatus");
        that.filterDoctype = $("#ddlFilterDocTypes");
        that.filterDept = $("#ddlFilterDepartments");
        that.filterUser = $("#ddlFilterUsers");

        that.groupBy = $("#ddlGroupBy");

        if (eG.offices == undefined || eG.offices.length == 0) {
            return;
        }

        eG.offices.forEach(function (office) {
            office.ServiceUrl = parseServiceUrl(office.ServiceUrl);
        });

        this.officeLenght = eG.offices.length;
        if (this.officeLenght == 1) {
            this.officeId = eG.offices[0].OfficeId;
        }

        this.bindEvents();

        this.$el.find("#btnReport").click();
    };

    eG.bindEvents = function () {
        var $el = this.$el;
        var that = this;
        that.type = "3"; // mặc định hiển thị tháng hiện tại
        formatDateAndShowDatepicker(that.type);

        $el.find("#btnReport").click(function () {
            that.loaded = 0;
            that.showStatistic();
        });

        $el.find("#clearCache").click(function () {
            that.clearCache();
        });

        $el.find(".overview-total .panel-body.office-overview").click(function () {
            if (that.officeId == 0) {
                // return;
            }
            var app = $(this).attr("data-app");
            $el.find(".overview-total .panel-body.selected").removeClass("selected");
            $(this).addClass("selected");
            that.showDocumentStatisticDetail(app);
        });

        $el.find("#officeConditions li a").click(function () {
            that.officeId = $(this).attr("value");

            $("#officeConditionCurrent").text($(this).text());
        });

        $el.find("#officeConditions li :checkbox").change(function () {
            that.officeId = $(this).attr("id");
            //$("#officeConditionCurrent").text($(this).text());
            if ($(this).is(":checked"))
            {
                if (varGlobal.index == 0)
                {
                    $("#officeConditionCurrent").text($(this).attr("value"));
                }
                else
                {
                    $("#officeConditionCurrent").text($("#officeConditionCurrent").text() + "," + $(this).attr("value"));
                }
                varGlobal.index++;
            }
            else
            {
                varGlobal.index--;
                if(varGlobal.index == 0)
                {
                    $("#officeConditionCurrent").text("Tất cả cơ quan");
                }
                else
                {
                    $("#officeConditionCurrent").text($("#officeConditionCurrent").text().replace("," + $(this).attr("value"),""));
                }
            }
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

        $el.find(".overview-filters select").change(function () {
            if ($(this).is("#ddlGroupBy")) {
                if (that.officeId != 0) {

                    that.clearData();
                    // Giám sát cơ quan
                    that.showStatisticOffice();
                }
            }
            that._showOverdueByFilterAndGroup();
        });

        $el.find("#btnExportExcel, #btnExportWord, #btnExportPdf,#btnExportXml").click(function () {
            var type = $(this).is("#btnExportPdf") ? 1 :
                       $(this).is("#btnExportWord") ? 2 :
                       3

            var app = $(this).parent().closest("ul").attr("data-app");
            if (app == "total" || app == "dkqm") {
                that.exportTotal(type, app == "dkqm");
                return;
            }

            if (app == "lienthong") {
                that.exportLT(type);
                return;
            }

            if (app == "quahannd") {
                that.exportOverdueByWorkflow(type);
                return;
            }

            if (app == "customerinfo") {
                that.exportCustomerInfo(type);
                return;
            }

            that.exportDetail(type, app);
        });

        $el.find("#overdueRange").change(function () {
            that.bindOverdue();
        });
    }

    eG.bindViewDocument = function () {
        $(".btn-viewDocument").click(function (e) {
            var element = $(e.target).closest("a");
            var id = element.attr("value");
            var getResportUrl = "/webapi/StatisticApi/GetProcessDocumentDetail";
            var dialogElement = $("<div>");
            var documentCopyTemplate = $("#documentcopy-detail-process").html();
            //var compendium = element.attr("content");
            eG.offices.forEach(function (office) {
                var url = office.ServiceUrl + getResportUrl;
                office.error = false;
                $.ajax({
                    type: "GET",
                    url: url,
                    data: { docCopyId: id },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (model) {
                        //Thiết lập dialog
                        $("#detailModal").html($.tmpl(documentCopyTemplate, model));
                        $("#detailModal").modal("show");
                    },
                    error: function () {
                        console.log("No data return");
                    }
                });
            });
        });
    }

    ///Main
    eG.showStatistic = function () {
        if (this.isLoading) {
            return;
        }
        this.isLoading = true;

        this.$el.find(".overview-total .panel-body.office-overview").first().click();

        this.conditions = this.getReportCondition();

        this.clearData();

        $(".processingDocs").show();
        if (this.officeId == 0) {
            // Giám sát quản trị tập trung
            this.showStatisticAll();
        } else {
            // Giám sát cơ quan
            this.showStatisticOffice();
            this.showDetailOffice();
        }

        //this.showDKQMList();
    }

    eG.clearData = function () {
        this.data = {
            total: {
                TotalCustomer: 0,
                docOnlines: 0,
                docPublisheds: 0,
                unduePercen: 0,
                Name: "Tổng",
                PreExtisting: 0,
                NewReception: 0,
                Total: 0,
                Resolved: 0,
                SolvedInTime: 0,
                SolvedInTimePercent: 0,
                SolvedLate: 0,
                SolvedLatePercent: 0,
                UnResolved: 0,
                Pending: 0,
                PendingPercent: 0,
                PendingLate: 0,
                PendingLatePercent: 0
            },
            column: [['Cơ quan', 'Đúng hẹn', 'Trễ hẹn']],
            table: []
        };
    }

    eG.bindOverdue = function () {
        var range = $("#overdueRange").val()
        range = range || 10;
        var getOverdueUrl = "/webapi/StatisticApi/GetOverdue?range=" + range;
        var that = this;
        that.$overdueElement = that.$el.find("#docOverdue tbody");
        that.$overdueElement.empty();

        eG.offices.forEach(function (office) {
            var url = office.ServiceUrl + getOverdueUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.conditions,
                timeout: 60 * 1000, // 30s
                success: function (result) {
                    that.$overdueElement.append("<tr><td colspan='8'><b>" + office.OfficeName + "</b> (" + result.length + " hồ sơ)</td></tr>");
                    that.$overdueElement.append(that.overdueTemplate.tmpl(result, {
                        dataArrayIndex: function (item) {
                            return $.inArray(item, result) + 1;
                        }
                    }));
                },
                error: function () {

                },
                complete: function () {

                }
            });
        });
    }

    eG.showStatisticAll = function () {
        var getResportUrl = "/webapi/StatisticApi/GetStatistic";
        var that = this;
        var idx = 1;
        that.$status.fadeIn("fast");
        eG.offices.forEach(function (office) {
            var url = office.ServiceUrl + getResportUrl;
            office.error = false;
            $.ajax({
                url: url,
                type: "GET",
                data: that.conditions,
                timeout: 60 * 1000, // 30s
                success: function (data) {
                    var result = data.docs;
                    if (result.length == 0) {
                        that.data.column.push([office.Name, 0, 0]);
                        return;
                    }
                    that.data.total.TotalCustomer += data.customerCount;
                    that.data.total.docPublisheds += data.docPublisheds;
                    that.data.total.docOnlines += data.docOnlines;

                    result.Total = result.NewReception + result.PreExtisting;
                    result.Resolved = result.SolvedInTime + result.SolvedLate;
                    result.UnResolved = result.Pending + result.PendingLate;
                    result.SolvedInTimePercent = Math.round(result.SolvedInTimePercent);
                    result.SolvedLatePercent = Math.round(result.SolvedLatePercent);
                    result.PendingPercent = Math.round(result.PendingPercent);
                    result.PendingLatePercent = Math.round(result.PendingLatePercent);

                    result.Name = office.OfficeName;
                    result.Stt = idx++;

                    that.addToTotal(result, that.data.total);

                    that.data.column.push([result.Name, result.SolvedInTime, result.SolvedLate + result.PendingLate]);

                    that.data.table.push(result);
                },
                error: function () {
                    that.data.column.push([office.OfficeName, 0, 0]);
                    var _default = jQuery.extend({}, defaultStatistic);
                    _default.Name = office.OfficeName;
                    _default.Stt = idx++;
                    that.data.table.push(_default);
                },
                complete: function () {
                    that.loaded += 1;
                    if (that.loaded === that.officeLenght) {
                        that.isLoading = false;
                    }
                    that.$status.fadeOut("fast");
                }
            });
        });

        var statisticInterval = setInterval(function () {
            if (that.loaded === that.officeLenght) {
                clearInterval(statisticInterval);

                that.isBindAll = true;
                that.$status.fadeOut("fast");
                that.bindAll();
            }
        }, 500);
    }

    eG.showDetailOffice = function () {
        var that = this;
        var data = that.getReportCondition();
        var getResportUrl = "/webapi/StatisticApi/GetAllDocumentOverdue";
        var that = this;
        var idx = 1;

        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        var url = office.ServiceUrl + getResportUrl;
        $.ajax({
            url: url,
            data: data,
            timeout: 60 * 1000, // 30s
            success: function (result) {
                debugger
                that.$el.find(".otherOverdue-total").text(result == undefined ? 0 : result.length);
                that.otherOverdues = that._parseDocColor(result);

                that._showOtherOverdueFilterAndGroup();

                that._showOverdueByFilterAndGroup();
            }
        });
    }

    eG.showStatisticOffice = function () {
        var getResportUrl = "/webapi/StatisticApi/GetStatisticDetail";
        var that = this;
        that.data.table = [];

        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        if (office == null) {
            return;
        }

        var url = office.ServiceUrl + getResportUrl;
        office.error = false;
        that.$status.fadeIn("fast");
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            timeout: 30 * 1000, // 30s
            success: function (data) {
                that.$el.find(".doc-lienthong-total, .lienthong-total").text(data.docPublisheds);
                that.$el.find(".doc-dkqm-total, .dkqm-total").text(data.docOnlines);
                that.data.total.docOnlines = data.docOnlines;
                that.data.total.docPublisheds = data.docPublisheds;
                var docs = data.docs;

                if (docs.length == 0) {
                    that.data.column.push([office.Name, 0, 0]);
                    return;
                }

                that.data.total.TotalCustomer = data.customerCount;

                for (var i = 0; i < docs.length; i++) {
                    var result = docs[i];
                    result.Total = result.NewReception + result.PreExtisting;
                    result.Resolved = result.SolvedInTime + result.SolvedLate;
                    result.UnResolved = result.Pending + result.PendingLate;
                    result.SolvedInTimePercent = Math.round(result.SolvedInTimePercent);
                    result.SolvedLatePercent = Math.round(result.SolvedLatePercent);
                    result.PendingPercent = Math.round(result.PendingPercent);
                    result.PendingLatePercent = Math.round(result.PendingLatePercent);
                    if (result.SolvedInTimePercent == 0 && result.SolvedLatePercent == 0) {
                        result.SolvedInTimePercent = 100;
                    }
                    result.Stt = i + 1;

                    that.addToTotal(result, that.data.total);

                    that.data.column.push([result.Name, result.SolvedInTime, result.SolvedLate + result.PendingLate]);

                    that.data.table.push(result);
                }
            },
            error: function () {
                // office.error = true;
                that.data.column.push([office.OfficeName, 0, 0]);
            },
            complete: function () {
                that.isBindAll = false;
                that.isLoading = false;
                that.bindAll();
                that.$status.fadeOut("fast");
            }
        });
    }

    eG.showLienThongStatisticOffice = function () {
        var getResportUrl = "/webapi/StatisticApi/GetLienThongs";
        var that = this;
        that.docLienThong = $("#docLienThong tbody");
        that.lienthongTemplate = $("#lienThongTemp");
        that.lienThongData = [];

        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        if (office == null) {
            return;
        }

        var url = office.ServiceUrl + getResportUrl;
        that.docLienThong.empty();

        $.ajax({
            url: url,
            type: "GET",
            data: that.conditions,
            timeout: 30 * 1000, // 30s
            success: function (data) {
                // that.$el.find(".doc-lienthong-total, .lienthong-total").text(data.length);
                if (data.length == 0) {
                    return;
                }
                that.lienThongData = data;
                that.docLienThong.append($.tmpl(that.lienthongTemplate, data));
            },
            error: function () {

            },
            complete: function () {
            }
        });
    }

    eG.showLienThongAll = function () {
        var getResportUrl = "/webapi/StatisticApi/GetLienThongs";
        var that = this;
        that.lienThongData = [];

        that.docLienThong = $("#docLienThong tbody");
        that.lienthongTemplate = $("#lienThongTemp");

        that.docLienThong.empty();

        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            that.docLienThong.append(officeRow);
            var url = office.ServiceUrl + getResportUrl;

            $.ajax({
                url: url,
                type: "GET",
                timeout: 30 * 1000, // 30s
                data: that.conditions,
                success: function (data) {
                    that.lienThongData = _.union(that.lienThongData, data);
                    that.$el.find(".doc-lienthong-total, .lienthong-total").text(that.lienThongData.length);

                    officeRow.find(".officeTotal").text(data.length);

                    if (data.length == 0) {
                        return;
                    }

                    officeRow.after($.tmpl(that.lienthongTemplate, data));
                },
                error: function () {

                },
                complete: function () {
                }
            });
        });
    },

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

    eG.addToTotal = function (result, total) {
        total.SolvedInTime += result.SolvedInTime;
        total.Total += result.Total;
        total.PreExtisting += result.PreExtisting,
        total.NewReception += result.NewReception,
        total.Resolved += result.Resolved,
        total.SolvedInTimePercent = Math.round(total.Resolved == 0 ? 0 : (total.SolvedInTime * 100 / total.Resolved)),
        total.SolvedLate += result.SolvedLate,
        total.SolvedLatePercent = Math.round(total.Resolved == 0 ? 0 : (total.SolvedLate * 100 / total.Resolved)),
        total.UnResolved += result.UnResolved,
        total.Pending += result.Pending,
        total.PendingPercent = Math.round(total.UnResolved == 0 ? 0 : (total.Pending * 100 / total.UnResolved)),
        total.PendingLate += result.PendingLate,
        total.PendingLatePercent = Math.round(total.UnResolved == 0 ? 0 : (total.PendingLate * 100 / total.UnResolved)),

        total.unduePercen = Math.round(total.Total == 0 ? 0 : (total.SolvedInTime + total.Pending) * 100 / total.Total);
    }

    eG.bindAll = function () {
        this.officeName = this.isBindAll ? "Đơn vị" : "Loại hồ sơ";
        $(".statistic-detail-officeName").text(this.officeName);

        this.$el.find(".doc-total-total").text(this.data.total.Total);
        this.$el.find(".doc-resolved-total").text(this.data.total.SolvedInTime);
        this.$el.find(".doc-resolvedLate-total").text(this.data.total.SolvedLate);
        this.$el.find(".doc-pending-total").text(this.data.total.Pending);
        this.$el.find(".doc-overdue-total").text(this.data.total.PendingLate);

        this.$el.find(".doc-customerinfo-total").text(this.data.total.TotalCustomer);
        this.$el.find(".doc-lienthong-total, .lienthong-total").text(this.data.total.docPublisheds);
        this.$el.find(".doc-dkqm-total, .dkqm-total").text(this.data.total.docOnlines);

        this.bindPieChart();
        this.bindColumnChart();
        this.bindTableChart();
    }

    eG.bindPieChart = function () {
        this.$el.find(".doughnutChar-result-numb").text(this.data.total.unduePercen + "%");
        var that = this;

        var data = [
            {
                value: that.data.total.SolvedInTime,
                color: "#48B9DD",
                highlight: "#48B9DD",
                label: "Đúng hạn"
            },
            {
                value: that.data.total.SolvedLate + that.data.total.PendingLate,
                color: color.quahan,
                highlight: color.quahan,
                label: "Quá hạn"
            },
            {
                value: that.data.total.Pending,
                color: color.chuadenhan,
                highlight: color.chuadenhan,
                label: "Chưa đến hạn"
            }
        ];

        showDoughnutChart(data);
    };

    eG.bindColumnChart = function () {
        var that = this;
        this.$el.find(".statistic-column-label").text(this.timeTitle);

        var data = {
            labels: [],
            datasets: [
                {
                    label: "Đúng hẹn",
                    fillColor: color.dunghan,
                    strokeColor: color.dunghan,
                    highlightFill: color.dunghan,
                    highlightStroke: color.dunghan,
                    data: []
                },
                {
                    label: "Quá hạn",
                    fillColor: color.quahan,
                    strokeColor: color.quahan,
                    highlightFill: color.quahan,
                    highlightStroke: color.quahan,
                    data: []
                },
                {
                    label: "Chưa đến hạn",
                    fillColor: color.chuadenhan,
                    strokeColor: color.chuadenhan,
                    highlightFill: color.chuadenhan,
                    highlightStroke: color.chuadenhan,
                    data: []
                }
            ]
        };

        _.each(this.data.table, function (result) {
            data.labels.push(result.Name);
            data.datasets[0].data.push(result.SolvedInTime);
            data.datasets[1].data.push(result.SolvedLate + result.PendingLate);
            data.datasets[2].data.push(result.Pending);
        });

        showBarChart(data);
    }

    eG.bindTableChart = function () {
        this.detailElement.find("tbody").empty();
        this.detailElement.find("tbody").append($.tmpl(this.template, this.data.table));
        this.detailElement.find("tbody").append($.tmpl(this.totalTemplate, this.data.total));
    }

    eG.getReportCondition = function () {
        return {
            hasOldDocument: $("#HasTonKyTruoc").is(":checked"),
            hasXlvb: $("#IsXlvb").is(":checked"),
            hasHsmc: $("#IsHsmc").is(":checked"),
            groupBy: $("#ddlGroupBy").val(),
            from: $("#from").datepicker("getDate").startOf("day").format("yyyy-MM-ddTHH:mm:ss"),
            to: $("#to").datepicker("getDate").endOf("day").format("yyyy-MM-ddTHH:mm:ss")
        };
    }

    eG.getTimeLineCustomerInfo = function () {
        return {
            from: $("#from").datepicker("getDate").startOf("day").format("yyyy-MM-ddTHH:mm:ss"),
            to: $("#to").datepicker("getDate").endOf("day").format("yyyy-MM-ddTHH:mm:ss")
        };
    }

    eG.getTimeTitle = function (condition) {
        if (condition.type == 1) {
            return "Năm " + condition.year;
        }

        if (condition.type == 2) {
            return $(".statistic-time-detail .haftYear :selected").text() + " " + condition.year;
        }

        if (condition.type == 3) {
            return $(".statistic-time-detail .quarter :selected").text() + " " + condition.year;
        }

        return $(".statistic-time-detail .month :selected").text() + "/" + condition.year;
    }

    eG.exportTotal = function (type, isDkqm) {
        var data = [];
        var that = this;
        var dataTable = isDkqm ? this.onlineDataTable.table : this.data.table;
        dataTable.forEach(function (statistic) {
            data.push({
                LoaiHoSo: statistic.Name,
                TonKyTruoc: statistic.PreExtisting,
                NhanTrongKy: statistic.NewReception,
                Tong: statistic.Total,
                DaGiaiQuyet: statistic.Resolved,
                DungHen: statistic.SolvedInTime,
                TiLeDungHen: statistic.SolvedInTimePercent,
                TreHen: statistic.SolvedLate,
                TiLeTreHen: statistic.SolvedLatePercent,
                ChuaGiaiQuyet: statistic.UnResolved,
                TrongHan: statistic.Pending,
                TileTrongHan: statistic.PendingPercent,
                QuaHan: statistic.PendingLate,
                TiLeQuaHan: statistic.PendingLatePercent,
                GhiChu: ""
            });
        });

        var reportName = isDkqm ? "BÁO CÁO HỒ SƠ ĐĂNG KÝ QUA MẠNG" : "BÁO CÁO TÌNH TRẠNG XỬ LÝ HỒ SƠ";

        var fromDate = $("#from").val();
        var toDate = $("#to").val();

        $.ajax({
            url: "/Statistics/export",
            data: { data: JSON.stringify(data), reportName: reportName, fromDate: fromDate, toDate: toDate },
            type: "POST",
            success: function (result) {
                window.open("/Statistics/ExportFile?isDetail=" + !that.isBindAll + "&type=" + type, "_blank");
            }
        });
    },

    eG.exportDetail = function (type, app) {
        var data = [];
        var that = this;
        var reportName = "";

        switch (app) {
            case "dunghan":
                data = that.dungHenData;
                reportName = "BÁO CÁO HỒ SƠ ĐÚNG HẠN";
                break;
            case "trehen":
                data = that.treHenData;
                reportName = "BÁO CÁO HỒ SƠ TRỄ HẸN";
                break;
            case "chuadenhan":
                data = that.chuaDenHanData;
                reportName = "BÁO CÁO HỒ SƠ CHƯA ĐẾN HẠN";
                break;
            case "quahan":
                data = that.quaHanData;
                reportName = "BÁO CÁO HỒ SƠ QUÁ HẠN";
                break;
            case "quahannd":
                data = that.quaHanDataND;
                reportName = "BÁO CÁO HỒ SƠ QUÁ HẠN THEO QUY TRÌNH";
                break;
        }
        var fromDate = $("#from").val();
        var toDate = $("#to").val();

        // Tạm fix ko export được file của BRVT
        data.forEach(function (obj) {
            obj.CurrentUser = obj.CurrentUserName ? obj.CurrentUserName : "";
            obj.CitizenName = obj.CategoryBusinessId == 4 ? obj.CitizenName : obj.Compendium
        });

        $.ajax({
            url: "/Statistics/exportDetail",
            data: { data: JSON.stringify(data), reportName: reportName, fromDate: fromDate, toDate: toDate },
            type: "POST",
            success: function (result) {
                window.open("/Statistics/ExportFileDetail?type=" + type, "_blank");
            }
        });
    }

    eG.exportLT = function (type) {
        var data = this.lienThongData;
        var that = this;

        var fromDate = $("#from").val();
        var toDate = $("#to").val();

        $.ajax({
            url: "/Statistics/ExportLienThong",
            data: { data: JSON.stringify(data), fromDate: fromDate, toDate: toDate, reportName: "BÁO CÁO QUÁ HẠN XỬ LÝ VĂN BẢN" },
            type: "POST",
            success: function (result) {
                window.open("/Statistics/ExportFileLT?type=" + type, "_blank");
            }
        });
    }

    eG.exportOverdueByWorkflow = function (type) {
        var data = this.quaHanDataND;
        var that = this;

        var fromDate = $("#from").val();
        var toDate = $("#to").val();

        $.ajax({
            url: "/Statistics/ExportOverdueByWorkflow",
            data: { data: JSON.stringify(data), fromDate: fromDate, toDate: toDate },
            type: "POST",
            success: function (result) {
                window.open("/Statistics/ExportOverdueByWorkflow?type=" + type, "_blank");
            }
        });
    }

    eG.exportCustomerInfo = function (type) {
        var data = this.customerInfoData;
        var that = this;

        var fromDate = $("#from").val();
        var toDate = $("#to").val();

        $.ajax({
            url: "/Statistics/ExportCustomerInfo",
            data: { data: JSON.stringify(data), fromDate: fromDate, toDate: toDate },
            type: "POST",
            success: function (result) {
                window.open("/Statistics/ExportFileCustomerInfoDetail?type=" + type, "_blank");
            }
        });
    },

    eG.showDocumentStatisticDetail = function (app) {
        var contentElement = this.$el.find(".overview-result .overview-" + app);
        contentElement.siblings().hide();
        contentElement.show();

        switch (app) {
            case "dunghan":
                this.showDungHan();
                break;
            case "trehen":
                this.showTreHan();
                break;
            case "chuadenhan":
                this.showChuaDenHan();
                break;
            case "quahan":
                this.showQuaHan();
                break;
            case "quahannd":
                this.showQuaHanNd();
                break;
            case "customerinfo":
                this.showCustomerInfo();
                break;
            case "lienthong":
                this.showLienThongStatisticOffice();
                break;
            case "dkqm":
                this.showDKQM();
                this.showDKQMList();
                break;
            default:
                break;
        }
    },

    eG.showCustomerInfo = function () {
        if (this.officeId == 0) {
            return this.showCustomerInfoAll();
        }

        var customerInfoElement = this.$el.find("#customerInfoList tbody"),
            customerInfoUrl = "/webapi/StatisticApi/GetAllCustomerInfo",
            that = this,
            office, url, groupType;

        customerInfoElement.empty();
        that.$status.fadeIn("fast");
        office = _.find(eG.offices, function (o) {
            return o.OfficeId = that.officeId;
        });

        url = office.ServiceUrl + customerInfoUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getTimeLineCustomerInfo(),
            success: function (result) {
                that.customerInfoData = result;
                $(".customerinfo-total").text(result.length);
                customerInfoElement.append($.tmpl($("#customerInfoTemp"), result));
            },
            error: function () {

            },
            complete: function () {
                that.$status.fadeOut("fast");
            }
        });
    },

    eG.showCustomerInfoAll = function () {
        var customerInfoElement = this.$el.find("#customerInfoList tbody");
        var customerInfoUrl = "/webapi/StatisticApi/GetAllCustomerInfo";
        var that = this;
        that.customerInfoData = [];
        customerInfoElement.empty();
        that.$status.fadeIn("fast");
        $(".overview-customerinfo .overdue-group").addClass("hidden");

        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            customerInfoElement.append(officeRow);
            var url = office.ServiceUrl + customerInfoUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.getTimeLineCustomerInfo(),
                success: function (result) {
                    if (result) {
                        _.each(result, function (item) {
                            item.GroupName = office.OfficeName + " - " + result.length;
                        });
                    }

                    that.customerInfoData = _.union(that.customerInfoData, result);
                    $(".customerinfo-total").text(that.customerInfoData.length);
                    officeRow.find(".officeTotal").text(result.length);
                    officeRow.after($.tmpl($("#customerInfoTemp"), result));
                },
                error: function () {

                },
                complete: function () {
                    that.$status.fadeOut("fast");
                }
            });
        });
    },

    eG.showDungHan = function () {
        if (this.officeId == 0) {
            return this.showDungHanAll();
        }

        var dungHanElement = this.$el.find("#dungHanList tbody"),
            dunghanUrl = "/webapi/StatisticApi/GetAllDocumentDungHan",
            that = this,
            office, url, groupType, groups, groupByColumn;

        dungHanElement.empty();
        that.$status.fadeIn("fast");
        office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        url = office.ServiceUrl + dunghanUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            success: function (result) {
                that.dungHenData = result;
                $(".dunghan-total").text(result.length);
                dungHanElement.append($.tmpl($("#dunghanTemp"), result));

                that.bindViewDocument();
            },
            error: function () {

            },
            complete: function () {
                that.$status.fadeOut("fast");
            }
        });

        $(".overview-dunghan .overdue-group").removeClass("hidden");
        $(".overview-dunghan .overdue-group li").click(function (e) {
            dungHanElement.empty();
            groupByColumn = $(e.target).closest("a").attr("value");
            if (groupByColumn === "") {
                _.each(that.dungHenData, function (doc) {
                    doc.GroupName = "";
                });
                dungHanElement.append($.tmpl($("#dunghanTemp"), that.dungHenData));
                return;
            }

            groups = _.groupBy(_.sortBy(that.dungHenData, groupByColumn), groupByColumn);

            for (var group in groups) {
                var data = groups[group];
                var groupName = group + " - " + data.length + " Văn bản (Hồ sơ)";
                _.each(data, function (doc) {
                    doc.GroupName = groupName;
                });

                dungHanElement.append("<tr class='overdue-group'><td colspan='7'>" + groupName + "</td></tr>");
                dungHanElement.append($.tmpl($("#dunghanTemp"), data));
            }
            that.bindViewDocument();
        });
    },

    eG.showDungHanAll = function () {
        var dungHanElement = this.$el.find("#dungHanList tbody");
        var dunghanUrl = "/webapi/StatisticApi/GetAllDocumentDungHan";
        var that = this;
        that.dungHenData = [];
        dungHanElement.empty();
        that.$status.fadeIn("fast");
        $(".overview-dunghan .overdue-group").addClass("hidden");

        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            dungHanElement.append(officeRow);
            var url = office.ServiceUrl + dunghanUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.getReportCondition(),
                success: function (result) {
                    if (result) {
                        _.each(result, function (item) {
                            item.GroupName = office.OfficeName + " - " + result.length;
                        });
                    }

                    that.dungHenData = _.union(that.dungHenData, result);
                    $(".dunghan-total").text(that.dungHenData.length);
                    officeRow.find(".officeTotal").text(result.length);
                    officeRow.after($.tmpl($("#dunghanTemp"), result));
                    that.bindViewDocument();
                },
                error: function () {

                },
                complete: function () {
                    that.$status.fadeOut("fast");
                }
            });
        });
    },

    eG.showTreHan = function () {

        if (this.officeId == 0) {
            return this.showTreHanAll();
        }
        var treHanElement = this.$el.find("#treHanList tbody");
        var trehanUrl = "/webapi/StatisticApi/GetAllDocumentTreHan";
        var that = this,
            office, url, groupType, groups, groupByColumn;
        treHanElement.empty();
        that.$status.fadeIn("fast");
        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        var url = office.ServiceUrl + trehanUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            success: function (result) {
                that.treHenData = result;
                $(".trehan-total").text(result.length);
                treHanElement.append($.tmpl($("#dunghanTemp"), result));

                that.bindViewDocument();
            },
            error: function () {

            },
            complete: function () {
                that.$status.fadeOut("fast");
            }
        });

        $(".overview-trehen .overdue-group").removeClass("hidden");
        $(".overview-trehen .overdue-group li").click(function (e) {
            treHanElement.empty();
            groupByColumn = $(e.target).closest("a").attr("value");
            if (groupByColumn === "") {
                treHanElement.append($.tmpl($("#dunghanTemp"), that.treHenData));
                return;
            }

            groups = _.groupBy(_.sortBy(that.treHenData, groupByColumn), groupByColumn);

            for (var group in groups) {
                var data = groups[group];
                var groupName = group + " - " + data.length + " Văn bản (Hồ sơ)";
                _.each(data, function (doc) {
                    doc.GroupName = groupName;
                });

                treHanElement.append("<tr class='overdue-group'><td colspan='7'>" + groupName + "</td></tr>");
                treHanElement.append($.tmpl($("#dunghanTemp"), data));
            }
            that.bindViewDocument();
        });
    },

    eG.showTreHanAll = function () {
        var treHanElement = this.$el.find("#treHanList tbody");
        var trehanUrl = "/webapi/StatisticApi/GetAllDocumentTreHan";
        var that = this;
        that.treHenData = [];

        treHanElement.empty();
        that.$status.fadeIn("fast");
        $(".overview-trehen .overdue-group").addClass("hidden");

        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            treHanElement.append(officeRow);

            var url = office.ServiceUrl + trehanUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.getReportCondition(),
                success: function (result) {
                    if (result) {
                        _.each(result, function (item) {
                            item.GroupName = office.OfficeName + " - " + result.length;
                        });
                    }

                    that.treHenData = _.union(that.treHenData, result);
                    $(".trehan-total").text(that.treHenData.length);
                    officeRow.find(".officeTotal").text(result.length);
                    officeRow.after($.tmpl($("#dunghanTemp"), result));
                },
                error: function () {

                },
                complete: function () {
                    that.$status.fadeIn("fast");
                }
            });
        });
    },

    eG.showChuaDenHan = function () {
        if (this.officeId == 0) {
            return this.showChuaDenHanAll();
        }
        var chuadenhanElement = this.$el.find("#chuaDenHanList tbody");
        var chuadenhanUrl = "/webapi/StatisticApi/GetAllDocumentChuaDenHan";
        var that = this,
            office, url, groupType, groups, groupByColumn;

        chuadenhanElement.empty();
        that.$status.fadeIn("fast");
        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        var url = office.ServiceUrl + chuadenhanUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            success: function (result) {
                that.chuaDenHanData = result;
                $(".chuadenhan-total").text(result.length);
                chuadenhanElement.append($.tmpl($("#dunghanTemp"), result));

                that.bindViewDocument();
            },
            error: function () {

            },
            complete: function () {
                that.$status.fadeOut("fast");
            }
        });

        $(".overview-chuadenhan .overdue-group").removeClass("hidden");
        $(".overview-chuadenhan .overdue-group li").click(function (e) {
            chuadenhanElement.empty();
            groupByColumn = $(e.target).closest("a").attr("value");
            if (groupByColumn === "") {
                chuadenhanElement.append($.tmpl($("#dunghanTemp"), that.chuaDenHanData));
                return;
            }

            groups = _.groupBy(_.sortBy(that.chuaDenHanData, groupByColumn), groupByColumn);

            for (var group in groups) {
                var data = groups[group];
                var groupName = group + " - " + data.length + " Văn bản (Hồ sơ)";
                _.each(data, function (doc) {
                    doc.GroupName = groupName;
                });
                chuadenhanElement.append("<tr class='overdue-group'><td colspan='7'>" + groupName + "</td></tr>");
                chuadenhanElement.append($.tmpl($("#dunghanTemp"), data));
            }
            that.bindViewDocument();
        });
    },

    eG.showChuaDenHanAll = function () {
        var chuadenhanElement = this.$el.find("#chuaDenHanList tbody");
        var chuadenhanUrl = "/webapi/StatisticApi/GetAllDocumentChuaDenHan";
        var that = this;
        that.chuaDenHanData = [];

        chuadenhanElement.empty();
        that.$status.fadeIn("fast");
        $(".overview-chuadenhan .overdue-group").addClass("hidden");
        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            chuadenhanElement.append(officeRow);

            var url = office.ServiceUrl + chuadenhanUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.getReportCondition(),
                success: function (result) {
                    if (result) {
                        _.each(result, function (item) {
                            item.GroupName = office.OfficeName + " - " + result.length;
                        });
                    }

                    that.chuaDenHanData = _.union(that.chuaDenHanData, result);
                    $(".chuadenhan-total").text(that.chuaDenHanData.length);
                    officeRow.find(".officeTotal").text(result.length);
                    officeRow.after($.tmpl($("#dunghanTemp"), result));
                    that.bindViewDocument();
                },
                error: function () {

                },
                complete: function () {
                    that.$status.fadeOut("fast");
                }
            });
        });
    },

    eG.showQuaHan = function () {
        if (this.officeId == 0) {
            return this.showQuaHanAll();
        }

        var quahanElement = this.$el.find("#quaHanList tbody");
        var quahanUrl = "/webapi/StatisticApi/GetAllDocumentQuaHan";
        var that = this,
            office, url, groupType, groups, groupByColumn;

        quahanElement.empty();
        that.$status.fadeIn("fast");
        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        var url = office.ServiceUrl + quahanUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            success: function (result) {
                that.quaHanData = result;
                $(".quahan-total").text(result.length);
                quahanElement.append($.tmpl($("#dunghanTemp"), result));

                that.bindViewDocument();
            },
            error: function () {

            },
            complete: function () {
                that.$status.fadeOut("fast");
            }
        });

        $(".overview-quahan .overdue-group").removeClass("hidden");
        $(".overview-quahan .overdue-group li").click(function (e) {
            quahanElement.empty();
            groupByColumn = $(e.target).closest("a").attr("value");
            if (groupByColumn === "") {
                quahanElement.append($.tmpl($("#dunghanTemp"), that.quaHanData));
                return;
            }

            groups = _.groupBy(_.sortBy(that.quaHanData, groupByColumn), groupByColumn);

            for (var group in groups) {
                var data = groups[group];
                var groupName = group + " - " + data.length + " Văn bản (Hồ sơ)";
                _.each(data, function (doc) {
                    doc.GroupName = groupName;
                });
                quahanElement.append("<tr class='overdue-group'><td colspan='7'>" + groupName + "</td></tr>");
                quahanElement.append($.tmpl($("#dunghanTemp"), data));
            }
            that.bindViewDocument();
        });
    },

    eG.showQuaHanAll = function () {
        var quahanElement = this.$el.find("#quaHanList tbody");
        var quahanUrl = "/webapi/StatisticApi/GetAllDocumentQuaHan";
        var that = this;
        that.quaHanData = [];

        quahanElement.empty();
        that.$status.fadeIn("fast");
        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            quahanElement.append(officeRow);

            var url = office.ServiceUrl + quahanUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.getReportCondition(),
                success: function (result) {
                    if (result) {
                        _.each(result, function (item) {
                            item.GroupName = office.OfficeName + " - " + result.length;
                        });
                    }

                    that.quaHanData = _.union(that.quaHanData, result);
                    $(".quahan-total").text(that.quaHanData.length);
                    officeRow.find(".officeTotal").text(result.length);
                    officeRow.after($.tmpl($("#dunghanTemp"), result));
                    that.bindViewDocument();
                },
                error: function () {

                },
                complete: function () {
                    that.$status.fadeOut("fast");
                }
            });
        });

        $(".overview-quahan .overdue-group").addClass("hidden");
    },

    eG.showQuaHanNd = function () {
        if (this.officeId == 0) {
            return;
        }

        var quahanElement = this.$el.find("#quaHanNDList tbody");
        var quahanUrl = "/webapi/StatisticApi/GetOverdueByWorkflow";
        var that = this,
            office, url, groupType, groups, groupByColumn;

        quahanElement.empty();
        that.$status.fadeIn("fast");
        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        var url = office.ServiceUrl + quahanUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            success: function (result) {
                that.quaHanDataND = result;
                $(".quahannd-total, .doc-overduend-total").text(result.length);
                quahanElement.append($.tmpl($("#quahanndTemp"), result));

                that.bindViewDocument();
            },
            error: function () {

            },
            complete: function () {
                that.$status.fadeOut("fast");
            }
        });

        $(".overview-quahannd .overdue-group").removeClass("hidden");
        $(".overview-quahannd .overdue-group li").click(function (e) {
            quahanElement.empty();
            groupType = $(e.target).closest("a").attr("value");
            if (groupType == 0) {
                quahanElement.append($.tmpl($("#quahanndTemp"), that.quaHanDataND));
                return;
            }

            groupByColumn = groupType == 1 ? "DoctypeName" : (groupType == 2 ? "CurrentDepartmentExt" : "CurrentUser");
            groups = _.groupBy(_.sortBy(that.quaHanDataND, groupByColumn), groupByColumn);

            for (var group in groups) {
                var data = groups[group];
                var groupName = group + " - " + data.length + " Văn bản (Hồ sơ)";
                _.each(data, function (doc) {
                    doc.GroupName = groupName;
                });
                quahanElement.append("<tr class='overdue-group'><td colspan='7'>" + groupName + "</td></tr>");
                quahanElement.append($.tmpl($("#quahanndTemp"), data));
            }
            that.bindViewDocument();
        });
    },

    eG.showDKQM = function () {
        var getResportUrl = "/webapi/StatisticApi/GetDocumentOnlineDetails";
        var that = this;
        that.onlineTableElement = $("#dkqmDetail");

        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        if (office == null) {
            return;
        }

        var url = office.ServiceUrl + getResportUrl;
        office.error = false;
        that.$status.fadeIn("fast");

        that.onlineDataTable = {
            total: {
                unduePercen: 0,
                Name: "Tổng",
                PreExtisting: 0,
                NewReception: 0,
                Total: 0,
                Resolved: 0,
                SolvedInTime: 0,
                SolvedInTimePercent: 0,
                SolvedLate: 0,
                SolvedLatePercent: 0,
                UnResolved: 0,
                Pending: 0,
                PendingPercent: 0,
                PendingLate: 0,
                PendingLatePercent: 0
            },
            table: []
        };

        $.ajax({
            url: url,
            type: "GET",
            data: that.conditions,
            success: function (data) {
                if (data.length == 0) {
                    that.data.column.push([office.Name, 0, 0]);
                    return;
                }
                for (var i = 0; i < data.length; i++) {
                    var result = data[i];
                    result.Total = result.NewReception + result.PreExtisting;
                    result.Resolved = result.SolvedInTime + result.SolvedLate;
                    result.UnResolved = result.Pending + result.PendingLate;
                    result.SolvedInTimePercent = Math.round(result.SolvedInTimePercent);
                    result.SolvedLatePercent = Math.round(result.SolvedLatePercent);
                    result.PendingPercent = Math.round(result.PendingPercent);
                    result.PendingLatePercent = Math.round(result.PendingLatePercent);
                    if (result.SolvedInTimePercent == 0 && result.SolvedLatePercent == 0) {
                        result.SolvedInTimePercent = 100;
                    }
                    result.Stt = i + 1;
                    that.addToTotal(result, that.onlineDataTable);
                    that.onlineDataTable.table.push(result);
                }
            },
            error: function () {
                // office.error = true;
                that.onlineDataTable.table.push([office.OfficeName, 0, 0]);
            },
            complete: function () {
                that.isBindAll = false;

                that.$status.fadeOut("fast");

                that.onlineTableElement.find("tbody").empty();
                that.onlineTableElement.find("tbody").append($.tmpl(that.template, that.onlineDataTable.table));
                that.onlineTableElement.find("tbody").append($.tmpl(that.totalTemplate, that.onlineDataTable.total));
            }
        });
    },

    eG.showDKQMList = function () {
        if (this.officeId == 0) {
            return this.showDKQMListAll();
        }

        var onlineElement = this.$el.find("#onlineList tbody");
        var onlineUrl = "/webapi/StatisticApi/GetAllDocumentOnline";
        var that = this,
            office, url, groupType, groups, groupByColumn;

        onlineElement.empty();

        var office = _.find(eG.offices, function (o) {
            return o.OfficeId == that.officeId;
        });

        var url = office.ServiceUrl + onlineUrl;
        $.ajax({
            url: url,
            type: "GET",
            data: that.getReportCondition(),
            success: function (result) {
                that.onlineData = result;
                $(".online-total").text(result.length);
                onlineElement.append($.tmpl($("#dunghanTemp"), result));
            },
            error: function () {

            },
            complete: function () {

            }
        });

    },

    eG.showDKQMListAll = function () {
        var onlineElement = this.$el.find("#onlineList tbody");
        var onlineUrl = "/webapi/StatisticApi/GetAllDocumentOnline";
        var that = this,
            office, url, groupType, groups, groupByColumn;

        onlineElement.empty();
        $(".processingDocs").show();
        that.onlineData = [];

        that.offices.forEach(function (office) {
            var officeRow = $("<tr style='color: blue'>").addClass("office" + office.OfficeId).html("<td colspan='6'>" + office.OfficeName + " - <span class='officeTotal'>0</span></td>");
            onlineElement.append(officeRow);

            var url = office.ServiceUrl + onlineUrl;
            $.ajax({
                url: url,
                type: "GET",
                data: that.getReportCondition(),
                success: function (result) {
                    if (result) {
                        _.each(result, function (item) {
                            item.GroupName = office.OfficeName + " - " + result.length;
                        });
                    }
                    that.onlineData = _.union(that.onlineData, result)

                    $(".doc-dkqm-total, .dkqm-total").text(that.onlineData.length);

                    officeRow.find(".officeTotal").text(result.length);

                    officeRow.after($.tmpl($("#dunghanTemp"), result));
                },
                error: function () {

                },
                complete: function () {

                }
            });
        });
    },

    eG._showOverdueByFilterAndGroup = function () {
        var docTypeFilterValue, deptFilterValue, userFilterValue, groupBy, that,
            groupByColumn, sortByColumn, overdues;

        that = this;

        // Cảnh báo nhân viên
        statusFilterValue = that.filterStatus.val();
        docTypeFilterValue = that.filterDoctype.val();
        deptFilterValue = that.filterDept.val();
        userFilterValue = that.filterUser.val();

        groupByColumn = that.groupBy.val();

        overdues = _.filter(that.otherOverdues, function (doc) {
            var result = true;
            if (statusFilterValue == 1) {
                result &= doc.IsProcessing == true;
            }

            if (docTypeFilterValue != 0) {
                result &= doc.DoctypeId == docTypeFilterValue;
            }

            if (deptFilterValue != 0) {
                result &= doc.CurrentDepartmentExt == deptFilterValue;
            }

            if (userFilterValue != 0) {
                result &= doc.CurrentUserId == userFilterValue;
            }

            return result;
        });
        groupByColumn = groupByColumn.trim();
        var overduesSort = _.sortBy(overdues, groupByColumn);
        overdues = _.groupBy(overduesSort, groupByColumn);

        that.otherOverduesElement.empty();

        _.each(overdues, function (groupDocs, group) {
            var data = _.groupBy(groupDocs, "UserCurrentName");
            var overdueCount = _.where(groupDocs, { color: "doc-overdue" }).length;
            var totalOverdueCount = _.where(groupDocs, { color: "doc-overdueTotal" }).length;
            var groupText = String.format("{0}", group);
            // var groupText = String.format("{0}: {1} văn bản ( <span class='doc-overdue'>{2}</span> quá hạn giữ, <span class='doc-overdueTotal'>{3}</span> quá hạn tổng, {4} còn lại )", group, groupDocs.length, overdueCount, totalOverdueCount, groupDocs.length - overdueCount - totalOverdueCount);

            var groupElement = $("<tr class='overdue-group'><td colspan='8'>" + groupText + "</td></tr>");
            var groupItems = $.tmpl(that.otherOverdueTemp, { data: data, groupText: group });
            groupElement.click(function () {
                that.otherOverduesElement.find("tr[group='" + group + "']").toggle();
            });

            that.otherOverduesElement.append(groupElement);
            that.otherOverduesElement.append(groupItems);
        });

        if (groupByColumn === "DocTypeName") {
            $(".doctype-col").hide();
            $(".dept-col").show();
        } else {
            $(".doctype-col").show();
            $(".dept-col").hide();
        }
    },

    eG._showOtherOverdueFilterAndGroup = function () {
        var that = this;

        $(".overview-filters select option.newitem").remove();

        if (that.otherOverdues == undefined || that.otherOverdues.length == 0) {
            $(".overview-filters").hide();
        } else {
            $(".overview-filters").show();
        }

        that.users = _.map(that.otherOverdues, function (i) {
            var obj = {
                CurrentUserId: i.CurrentUserId,
                CurrentUser: i.CurrentUser
            };
            return obj;
        });
        that.users = _.uniq(that.users, function (item) {
            return item.CurrentUserId;
        });

        that.depts = _.map(that.otherOverdues, function (i) {
            var obj = {
                CurrentDepartmentId: i.CurrentDepartmentExt,
                CurrentDepartmentExt: i.CurrentDepartmentExt
            };
            return obj;
        });
        that.depts = _.uniq(that.depts, function (item) {
            return item.CurrentDepartmentId;
        });

        that.doctypes = _.map(that.otherOverdues, function (i) {
            var obj = {
                DoctypeId: i.DoctypeId,
                DoctypeName: i.DoctypeName
            };
            return obj;
        });
        that.doctypes = _.uniq(that.doctypes, function (item) {
            return item.DoctypeId;
        });

        that.filterDoctype.append($.tmpl("<option value='${DoctypeId}' class='newitem'>${DoctypeName}</option>", that.doctypes));
        that.filterDept.append($.tmpl("<option value='${CurrentDepartmentId}' class='newitem'>${CurrentDepartmentExt}</option>", that.depts));
        that.filterUser.append($.tmpl("<option value='${CurrentUserId}' class='newitem'>${CurrentUser}</option>", that.users));
    },

    eG._parseDocColor = function (docs) {
        var overdueDay = eG.overdueDay || 0; // hạn tổng
        var overdueCurrentDay = eG.overdueCurrentDay || 0;  // hạn giữ

        _.each(docs, function (doc) {
            var deadline = doc.Deadline;
            var deadlineCurrent = doc.DeadlineCurrent;

            if (deadlineCurrent > 0 && deadlineCurrent > overdueCurrentDay) {
                doc.color = "doc-overdue";
            }
            if (deadline > 0 && deadline > overdueDay) {
                doc.color = "doc-overdueTotal"
            }
        });

        return docs;
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

function showBarChart(data) {
    for (var i = 0; i < data.labels.length; i++) {
        var label = data.labels[i];
        if (label && label.length > 12) {
            data.labels[i] = label.substring(0, 12) + "...";
        }
    }

    var barChart = $("#barOverview");

    barChart[0].width = barChart.parent()[0].clientWidth - 30;

    var ctx = barChart[0].getContext("2d");

    var options = {
        //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
        scaleBeginAtZero: true,

        //Boolean - Whether grid lines are shown across the chart
        scaleShowGridLines: true,

        //String - Colour of the grid lines
        scaleGridLineColor: "#e0e0e0",// "rgba(0,0,0,.05)",

        //Number - Width of the grid lines
        scaleGridLineWidth: 1,

        //Boolean - Whether to show horizontal lines (except X axis)
        scaleShowHorizontalLines: true,

        //Boolean - Whether to show vertical lines (except Y axis)
        scaleShowVerticalLines: true,

        //Boolean - If there is a stroke on each bar
        barShowStroke: true,

        //Number - Pixel width of the bar stroke
        barStrokeWidth: 5,

        //Number - Spacing between each of the X value sets
        barValueSpacing: 40,

        //Number - Spacing between data sets within X values
        barDatasetSpacing: 5,

        //String - A legend template
        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"

    };

    var myBarChart = new Chart(ctx).Bar(data, options);
}

function showDoughnutChart(data) {

    // And for a doughnut chart
    var doughnutChar = $("#DoughnutChar");
    doughnutChar[0].width = doughnutChar.parent()[0].clientWidth;
    var ctx1 = doughnutChar[0].getContext("2d");
    var options = {
        //Boolean - Whether we should show a stroke on each segment
        segmentShowStroke: true,

        //String - The colour of each segment stroke
        segmentStrokeColor: "#fff",

        //Number - The width of each segment stroke
        segmentStrokeWidth: 2,

        //Number - The percentage of the chart that we cut out of the middle
        percentageInnerCutout: 75, // This is 0 for Pie charts

        //Number - Amount of animation steps
        animationSteps: 100,

        //String - Animation easing effect
        animationEasing: "easeOutBounce",

        //Boolean - Whether we animate the rotation of the Doughnut
        animateRotate: true,

        //Boolean - Whether we animate scaling the Doughnut from the centre
        animateScale: false,

        //String - A legend template
        legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"

    }
    var myDoughnutChart = new Chart(ctx1).Doughnut(data, options);
}

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
