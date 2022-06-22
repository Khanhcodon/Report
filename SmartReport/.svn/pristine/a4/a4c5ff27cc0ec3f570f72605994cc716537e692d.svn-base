define([
   egov.template.createReferendum, "referendumModel"
],
function (template) {

    var ReferendumCreate = Backbone.View.extend({

        template: template,

        model: VoteModel,

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.IsEdit = options.isEdit ? true : false;
            this.vote = options.vote;
            this.model = new VoteModel();
            var that = this
            if (this.IsEdit) {
                that.listDetail = JSON.parse(that.vote.ListOpinion);
                that.model = new VoteModel(that.vote);
            }
            this.templateVoteDetail = '<tr><td> <input name="text" value="${TitleDetail}" type="text" class="form-control vote-detail-title" style="border:none" placeholder="+ Thêm ý kiến"> </td></tr>';
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            if ($("#Referendum").length === 0) {
                var installElementReferendum = $("<div id='Referendum'>");
                installElementReferendum.appendTo("body");
            }
            this.$el = $("#Referendum");
            this.$el.html($.tmpl(this.template, this.model.toJSON()));
            this.$dg = this.$('.dg-view');
            this.$dg1 = this.$('.dg-view1');
            this.$privateAnounc = this.$('.private-anoun ul:first');
            this.$privateAnounc1 = this.$('.private-anoun1 ul:first');
            var buttons = [];
            var title = "Thêm cuộc trưng cầu ý kiến";
            var titleButton = "Tạo mới";


            if (this.IsEdit) {
                title = "Sửa cuộc trưng cầu";
                titleButton = "Sửa";
                var timeBegin = convertTime(this.model.get("TimeBegin"));
                if (timeBegin.getTime() < new Date().getTime()) {
                    $("#validateError").html("Đã hết thời gian sửa");
                } else {
                    var btn = {
                        text: titleButton,
                        className: "btn-success",
                        click: function () {
                            that._create();
                        }
                    }
                    buttons.push(btn);
                }
            } else {
                var btn = {
                    text: titleButton,
                    className: "btn-success",
                    click: function () {
                        that._create();
                    }
                }
                buttons.push(btn);
            }
            buttons.push({
                text: "Đóng",
                click: function () {
                    that.$el.dialog("hide");
                }
            })
            var that = this;
            var dialogSetting = {
                width: 900,
                height: "auto",
                draggable: true,
                title: title,
                buttons: buttons
            };

            that.$el.dialog(dialogSetting);
            that._destroyDg();
            that._destroyDg1();
            that.renderTitleDetail(that.listDetail);
            that.renderDatePicker();
            that._showDg1();
            that.renderEdit();
            $("#IsSyncUser").click(function () {
                var IsSyncUser = that.$el.find("#IsSyncUser").is(":checked");
                
                if (IsSyncUser) {
                    that._destroyDg();
                    that.$el.find(".private-anoun > ul").html("");
                    that.$el.find(".private-anoun").parent().hide();
                    that.$el.find("#titleVote").text("Danh sách tham gia bỏ phiếu và xem kết quả");
                } else {
                    that.$el.find("#titleVote").text("Danh sách tham gia bỏ phiếu");
                    that.$el.find(".private-anoun").parent().show();
                    that._showDg();
                }
            })
            return that;
        },

        renderEdit: function () {
            var that = this;
            if (that.IsEdit) {
                that._showDg();
                setTimeout(function () {
                        var listUserVote = that.vote.UsersVote.split(";");
                        var listUserView = that.vote.UsersView.split(";");
                        listUserVote = cleanArray(listUserVote);
                        listUserView = cleanArray(listUserView);
                        var list = listUserVote.map(function (item) {
                            return parseInt(item, 10);
                        });
                        var listView = listUserView.map(function (item) {
                            return parseInt(item, 10);
                        });
                        if (list.length == listView.length) {
                            egov.views.dg1._showUsers(list);
                            return;
                        }
                        $("#IsSyncUser").click();
                        that.$el.find("#titleVote").text("Danh sách tham gia bỏ phiếu");
                        that.$el.find(".private-anoun").parent().show();
                        egov.views.dg1._showUsers(list);
                        egov.views.dg._showUsers(listView);
                }, 1000)
            }
            
        },

        renderTitleDetail: function (data) {
            var that = this;
            var listVoteDetail = [];
            if (data == null) {
                for (var i = 0; i < 5; i++) {
                    var voteDetail = new VoteDetailModel();
                    listVoteDetail.push(voteDetail.toJSON())
                }
            } else {
                for (var i = 0; i < data.length; i++) {
                    var voteDetail = new VoteDetailModel(data[i]);
                    listVoteDetail.push(voteDetail.toJSON())
                }
                var voteDetail = new VoteDetailModel();
                listVoteDetail.push(voteDetail.toJSON())
            }
            that.$el.find(".listVoteDetail").html($.tmpl(that.templateVoteDetail, listVoteDetail))
            $(document).on("blur", ".vote-detail-title", function () {
                var count = 0;
                $(".vote-detail-title").each(function () {
                    var value = $(this).val();
                    if (value == "") {
                        count++;
                    }
                });
                if (count < 2) {
                    var voteDetail = new VoteDetailModel();
                    that.$el.find(".listVoteDetail").append($.tmpl(that.templateVoteDetail, voteDetail.toJSON()));
                }
            })
            $(document).on("keyup", ".vote-detail-title", function (e) {
                var target = $(e.target).closest("tr");
                if (e.keyCode == 40) {
                    var trNext = target.next();
                    trNext.find(".vote-detail-title").focus();
                }
                if (e.keyCode == 38) {
                    var trPreview = target.prev();
                    trPreview.find(".vote-detail-title").focus();
                }
            })

        },

        renderDatePicker: function () {
            var that = this;
            $.datepicker.regional["vi-VN"] =
                     {
                         closeText: "Đóng",
                         prevText: "Trước",
                         nextText: "Sau",
                         currentText: "Hôm nay",
                         monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
                         monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
                         dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
                         dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
                         dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
                         weekHeader: "Tuần",
                         dateFormat: "dd/mm/yy",
                         firstDay: 1,
                         isRTL: false,
                         showMonthAfterYear: false,
                         yearSuffix: ""
                     };

            $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
            var dates = that.$el.find("#beginDate,#endDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                defaultDate: new Date(),
                onSelect: function (selectedDate) {
                    var dateObject = $(this).datepicker('getDate');
                    if ($(this).attr("id") == "beginDate") {
                        that.model.set({ TimeBegin: dateObject })
                    }
                    if ($(this).attr("id") == "endDate") {
                        that.model.set({ TimeEnd: dateObject })
                    }
                }
            });

            var formatTime = function (time) {
                return time.getHours() + ":" + ("0" + time.getMinutes()).slice(-2);
            }
            if (that.IsEdit) {
                var timeBegin = convertTime(that.model.get("TimeBegin"));
                var timeEnd = convertTime(that.model.get("TimeEnd"));

                $("#beginDate").datepicker("setDate", timeBegin);
                $("#endDate").datepicker("setDate", timeEnd);
                $("#timeBegin").val(formatTime(timeBegin))
                $("#timeEnd").val(formatTime(timeEnd))
            } else {
                $("#beginDate,#endDate").datepicker("setDate", new Date());
            }
            var times = that.$el.find("#timeBegin,#timeEnd").timepicker({
                timeFormat: 'H:i',
                durationTime: new Date()
            });
        },

        uncheckPrivateAnoun: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            egov.views.dg.uncheckPrivateAnoun(e);
            this._selectDg();
        },
        uncheckPrivateAnoun1: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            egov.views.dg1.uncheckPrivateAnoun(e);
            this._selectDg1();
        },
        _showDg: function () {
            /// <summary>
            /// Hiển thị form đồng gửi
            /// </summary>
            var that = this;
            if (!that.$dg.is(':not(:empty)')) {
                if (!egov.views.dg) {
                    require(['referendumDropdownUserView'], function (deptUser) {
                        egov.views.dg = new deptUser;
                        egov.views.dg.render(false, false, function () {
                            that._selectDg();
                        }, function () {
                            that.$dg.html(egov.views.dg.$el);
                        });
                    });
                }
                else {
                    egov.views.dg.render(false, false, function () {
                        that._selectDg();
                    });
                    that.$dg.html(egov.views.dg.$el);
                };
            }
        },

        _showDg1: function () {
            var that = this;
            if (!that.$dg1.is(':not(:empty)')) {
                if (!egov.views.dg1) {
                    require(['referendumDropdownUser'], function (deptUser) {
                        egov.views.dg1 = new deptUser;
                        egov.views.dg1.render(false, false, function () {
                            that._selectDg1();
                        }, function () {
                            that.$dg1.html(egov.views.dg1.$el);
                        }, true);
                    });
                }
                else {
                    egov.views.dg1.render(false, false, function () {
                        that._selectDg1();
                    });
                    that.$dg1.html(egov.views.dg1.$el);
                };

            }
        },

        _selectDg: function () {
            /// <summary>
            /// Hiển thị select đồng gửi
            /// </summary>
            var that = this;
            that.$privateAnounc.empty();
            egov.views.dg.selectDg(this.$privateAnounc);
            that.$privateAnounc.find(".checkbox").on("click", function (e) {
                that.uncheckPrivateAnoun(e);
            })
        },

        _selectDg1: function () {
            /// <summary>
            /// Hiển thị select đồng gửi
            /// </summary>
            var that = this;
            that.$privateAnounc1.empty();
            egov.views.dg1.selectDg(this.$privateAnounc1);
            that.$privateAnounc1.find(".checkbox").on("click", function (e) {
                that.uncheckPrivateAnoun1(e);
            })
        },

        _destroyDg: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        },
        _destroyDg1: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg1) {
                egov.views.dg1.destroy();
            }
        },

        setTime: function (id, attrModel) {
            var that = this;
            var time = $(id).val();
            var nameId = $(id).attr("id");
            if (time) {
                var hour = time.split(":")[0];
                var minute = time.split(":")[1];
                var dateBegin = that.model.get(attrModel);
                if (dateBegin instanceof Date) {
                } else {
                    dateBegin = convertTime(dateBegin)
                }
                dateBegin.setHours(Number(hour));
                dateBegin.setMinutes(Number(hour));
                var dateObj = {};
                dateObj[attrModel] = dateBegin
                that.model.set(dateObj);
            }
        },

        _validate: function () {
            var that = this;
            var IsSyncUser = that.$el.find("#IsSyncUser").is(":checked");
            var title = that.$el.find(".comment").val();
            var listDetail = [];
            $(".vote-detail-title").each(function () {
                var value = $(this).val();
                if (value != "") {
                    listDetail.push(value)
                }
            });
            if (title == "") {
                return "Chưa có tiêu đề";
            }
            if (listDetail.length == 0) {
                return "Chưa có ý kiến nào được ghi";
            }
            if (egov.views.dg1.getUserConsults().length == 0) {
                return "Chưa có người nào được tham gia trưng cầu";
            }

            if ((egov.views.dg && egov.views.dg.getUserConsults().length) == 0 && !IsSyncUser) {
                return "Chưa có người nào được xem kết quả";
            }

            if (that.model.get("TimeEnd").getTime() < that.model.get("TimeBegin").getTime()) {
                return "Thời gian bắt đầu không được lơn hơn thời gian kết thúc";
            }
            return false;
        },

        _create: function () {
            var that = this;
            that.setTime("#timeBegin", "TimeBegin");
            that.setTime("#timeEnd", "TimeEnd");
            var validate = that._validate();
            if (validate) {
                $("#validateError").html(validate);
                return false;
            }
            var IsSyncUser = that.$el.find("#IsSyncUser").is(":checked");

            var userVote = egov.views.dg1.getUserConsults();
            var userView = [];
            if (IsSyncUser) {
                userView = userVote;
            } else {
                userView = userView = egov.views.dg.getUserConsults();
            }

            var title = that.$el.find(".comment").val();
            var isMultiSelect = that.$el.find("#IsMultiSelect").is(":checked");
            var isPublic = that.$el.find("#IsPublic").is(":checked");
            var isCommentDiff = that.$el.find("#IsCommentDiff").is(":checked");
            var isViewResultImmediately = that.$el.find("#IsViewResultImmediately").is(":checked");
            var isNotify = that.$el.find("#IsNotify").is(":checked");
            var listDetail = [];
            $(".vote-detail-title").each(function () {
                var value = $(this).val();
                if (value != "") {
                    var voteDetail = new VoteDetailModel({
                        VoteId: 0,
                        VoteDetailId: 0,
                        UserIdCreate: 0,
                        UserIdVote: "",
                        TitleDetail: value,
                    });
                    listDetail.push(voteDetail.toJSON())
                }
            });
            var strUserVote = JSON.stringify(userVote).replace(/[^a-zA-Z0-9]/g, ';');
            var strUserView = JSON.stringify(userView).replace(/[^a-zA-Z0-9]/g, ';');
            that.model.set({
                Title: title,
                UsersView: strUserView,
                UsersVote: strUserVote,
                IsMultiSelect: isMultiSelect,
                IsPublic: isPublic,
                IsCommentDiff: isCommentDiff,
                IsViewResultImmediately: isViewResultImmediately,
                IsNotify: isNotify,
                VoteDetailId: JSON.stringify(listDetail)
            });
            if (that.IsEdit) {
                egov.request.updateVote({
                    data: {
                        voteStr: JSON.stringify(that.model.toJSON())
                    },
                    success: function (result) {
                        if (result.error) {
                            $("#validateError").html(result.content);
                            return;
                        }
                        var vote = new VoteModel(result.content);
                        that.$el.dialog("hide");
                    },
                    error: function (error) {
                        $("#validateError").html("Có lỗi khi gửi lên server");
                    }
                });
                return;
            }
            egov.request.createVote({
                data: {
                    vote: JSON.stringify(that.model.toJSON())
                },
                success: function (result) {
                    if (result.error) {
                        $("#validateError").html(result.content);
                        return;
                    }
                    var vote = new VoteModel(result.content);
                    vote.set("IsCreate", true);
                    that.vote.listVote.add(vote)
                    that.$el.dialog("hide");
                },
                error: function (error) {
                    $("#validateError").html("Có lỗi khi gửi lên server");
                }
            });
        }
    });

    function cleanArray(actual) {
        var newArray = new Array();
        for (var i = 0; i < actual.length; i++) {
            if (actual[i]) {
                newArray.push(actual[i]);
            }
        }
        return newArray;
    }

    var convertTime = function (timeStr) {
        if (timeStr instanceof Date) { } else {
            timeStr = timeStr.match(/\d+/)[0];
        }
        var time = new Date(Number(timeStr));
        return time;
    }

    return ReferendumCreate;
    //if ($("#Referendum").length === 0) {
    //    var installElementReferendum = $("<div id='Referendum'>");
    //    installElementReferendum.appendTo("body");
    //}

    //installElementReferendum = $("#Referendum");

    //var html = template;
    //installElementReferendum.html(html);

    //installElementReferendum.dialog({
    //    width: 900,
    //    height: "auto",
    //    draggable: true,
    //    title: "Thêm cuộc trưng cầu ý kiến",
    //    buttons: [
    //                 {
    //                     text: "Tạo mới",
    //                     className: "btn-success",
    //                     click: function () {
    //                     }
    //                 },
    //                 {
    //                      text: egov.resources.common.closeButton,
    //                      click: function () {

    //                      }
    //                 },
    //    ]
    //});
});