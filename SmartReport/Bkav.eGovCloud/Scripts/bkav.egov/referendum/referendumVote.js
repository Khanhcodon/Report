define([
   egov.template.voteReferendum,
   egov.template.voteItemReferendum,
   egov.template.voteOnlyItemReferendum,
   egov.template.viewOnlyItemReferendum,
   "referendumModel"
],
function (template, templateItem, templateOnlyItem, templateViewOnlyItem) {
    window.intervalVote = function () {

    }
    var ReferendumVote = Backbone.View.extend({

        template: template,
        // view : 2 giao diện xem và vote
        // view : 3 giao diện xem
        // view : 4 giao diện vote
        view: 3,
        events: {
            "keydown .oppinionDiff": "commentDiff"
        },

        initialize: function (options) {
            this.listenTo(listVoteDetail, 'add', this.addVote);
            this.listenTo(listVoteDetail, 'reset', this.resetVote);
            //this.listenTo(listVoteDetail, 'all', this.renderData);
            var listUserVote = this.model.get("UsersVote").split(";");
            var listUserVoted =[]
            if (this.model.get("UsersVoted") != null) {
                listUserVoted = this.model.get("UsersVoted").split(";");
            }
            listUserVote = cleanArray(listUserVote);
            listUserVoted = cleanArray(listUserVoted);
            this.model.set("TimeBeginFormat", setTime(this.model.get("TimeBegin")))
            this.model.set("CountUserVote", listUserVote.length)
            this.model.set("CountUserVoted", listUserVoted.length)

            this.model = options.model;
            this.view = options.view
            this.checkView();

            return this;
        },

        checkView: function () {
            var now = new Date();
            var that = this;
            var timeEndStr = that.model.get("TimeEnd");
            var timeBeginStr = that.model.get("TimeBegin");
            if (timeEndStr) {
                timeEndStr = timeEndStr.match(/\d+/)[0]
            }
            if (timeBeginStr) {
                timeBeginStr = timeBeginStr.match(/\d+/)[0]
            }

            if (that.model.get("IsView") && !that.model.get("IsVote") && that.view == 4) {
                that.view = 3 // nếu chỉ được xem ko được vote thì chuyển sang giao diện xem
                return;
            }

            if (Number(timeEndStr) < now.getTime() || Number(timeBeginStr) > now.getTime()) {
                that.view = 3;// hết hạn hoặc chưa đến hạn trưng cầu thì sẽ cho vào dao diện xem
                return;
            } else {
                //that.view = 4;
            }
        },

        renderData: function () {
            listVoteDetail.reset();
            var that = this;
            egov.request.getVoteDetail({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    this.model = new VoteModel(result);
                    var listUserVote = result.UsersVote.split(";");
                    listUserVote = cleanArray(listUserVote);
                    var opinion = JSON.parse(this.model.get("ListOpinion"));
                    currentUserId = this.model.get("CurrentUserId")
                    opinion = that.voteDetailCount(opinion, currentUserId, listUserVote.length)
                    opinion["IsMultiSelect"] = that.model.get("IsMultiSelect");
                    for (var i = 0; i < opinion.length; i++) {
                        opinion[i]["Option"] = {
                            IsMultiSelect: that.model.get("IsMultiSelect"),
                            IsPublic: that.model.get("IsPublic"),
                            IsCommentDiff: that.model.get("IsCommentDiff"),
                            IsViewResultImmediately: that.model.get("IsViewResultImmediately"),
                            IsNotify: that.model.get("IsNotify")
                        }
                        var vote = new VoteDetailModel(opinion[i]);
                        listVoteDetail.add(vote);
                    }
                },
                error: function (error) {}
            });
        },

        voteDetailCount: function (opinion, currentUserId, maxVote) {
            for (var i = 0; i < opinion.length; i++) {
                if (opinion[i].UserIdsVote) {
                    var listUser = opinion[i].UserIdsVote.split(";");
                    listUser = cleanArray(listUser);
                    opinion[i]["TotalVote"] = listUser.length;
                    if (listUser.includes(currentUserId.toString())) {
                        opinion[i]["IsChecked"] = true;
                    }
                } else {
                    opinion[i]["TotalVote"] = 0;
                }
                
            }
            //var maxVote = _.max(opinion, function (opi) { return opi.TotalVote; });
            for (var i = 0; i < opinion.length; i++) {
                opinion[i]["MaxVote"] = maxVote;
            }
            return opinion;
        },

        addVote: function (vote) {
            var view = new ReferendumVoteItem({ model: vote, view: this.view });
            this.$(".list-vote-detail").append(view.render().el);
            if (this.view != 4) {
                this.$el.find('.progress .progress-bar').css("width",
                       function () {
                           return $(this).attr("aria-valuenow") + "%";
                       }
               )
            }
        },

        resetVote: function (vote) {
            this.$(".list-vote-detail").html("");
        },

        render: function () {
            var that = this;
            this.$el.html($.tmpl(this.template, that.model.toJSON()));
            var height = 700;
            if (that.view == 4) {
                height = 600;
            }
            if (that.view == 3) {
                if (that.$el.find(".notifyContent").length > 0) {
                    that.$el.find(".notifyContent").hide();
                }
            }
            var buttons = [];
            if (that.model.get("IsView") && that.view == 4) {
                var textTitle = "Gửi kết quả"
                var voted = that.model.get("IsVoted");
                if (voted) {
                    textTitle = "Xem kết quả";
                }
                buttons.push({
                    text: textTitle,
                    className: "btn-success",
                    click: function () {
                        var list = listVoteDetail.where({ IsChecked: true });
                        var voteDetailIds = [];
                        list.forEach(function (model, index) {
                            voteDetailIds.push(model.get("VoteDetailId"))
                        });
                        
                        if (that.model.get("IsVoted")) {
                            that.view = 3;
                            that.$el.closest(".modal-content").find("h4.modal-title").text("Xem kết quả trưng cầu");
                            that._viewResult();
                            return;
                        }
                        egov.request.checkVoteResult({
                            data: { voteId: that.model.get("VoteId"), voteDetailIds: JSON.stringify(voteDetailIds) },
                            success: function (resultData) {
                                if (resultData == true) {
                                    that.view = 3;
                                    that.$el.closest(".modal-content").find("h4.modal-title").text("Xem kết quả trưng cầu");
                                    that._viewResult();
                                } else {
                                    that.$el.find(".errorContent").text(resultData);
                                }
                            },
                            error: function (error) {

                            }
                        });
                    }
                });
            }

            buttons.push({
                text: "Đóng",
                click: function () {
                   // clearInterval(window.intervalVote);
                    that.$el.dialog("hide");
                }
            });
            var btnView = {
                text: "Xem kết quả",
                className: "btn-success",
                click: function () {
                    that.view = 3;
                    that._viewResult()
                }
            }
            var that = this;
            var dialogSetting = {
                width: height,
                height: "auto",
                draggable: true,
                title: "Thực hiện trưng cầu",
                buttons: buttons
            };
            if (that.view == 3) {
                that.$el.find(".oppinionDiff").hide();
            }
            that.$el.dialog(dialogSetting);
            that.renderData();
            if (that.view == 3) {
                that.$el.find("h4.modal-title").text("Xem kết quả trưng cầu");
                //window.intervalVote = setInterval(function () {
                //    that.reloadData();
                //}, 3000)
            }

            return that;
        },

        reloadData: function () {
            var that = this;
            var count = listVoteDetail.length;
            egov.request.getVoteDetailReload({
                data: { voteId: that.model.get("VoteId"), voteDetailCount: count },
                success: function (resultData) {
                    if (resultData.IsDiff) {
                        that.renderData();
                        return;
                    }
                    var result = resultData.List;
                    listVoteDetail.forEach(function (model, index) {
                        for (var i = 0; i < result.length; i++) {
                            if (model.get("VoteDetailId") == result[i].VoteDetailId) {
                                if (model.get("TotalVote") != result[i].TotalVote) {
                                    model.set("TotalVote", result[i].TotalVote);
                                }
                            }
                        }
                    });
                },
                error: function (error) {

                }
            });
            
        },

        commentDiff: function (e) {
            if (!this.model.get("IsCommentDiff")) {
                return false;
            }

            if (e.keyCode == 13 && commentDiff != "") {
                var that = this;
                var commentDiff = that.$el.find(".oppinionDiff").val();
                egov.request.createCommentDiff({
                    data: { voteId: that.model.get("VoteId"), commentDiff: commentDiff },
                    success: function (result) {
                        if (result.error) {
                            $(".errorContent").text(result.content)
                            return;
                        }

                        listVoteDetail.reset();
                        that.renderData();
                        that.$el.find(".oppinionDiff").val("");
                    },
                    error: function (error) {
                        $(".errorContent").text("Lỗi kết nối đến server");
                    }
                });
            }
        },

        _viewResult: function () {
            this.render();
        }
    });

    var ReferendumVoteItem = Backbone.View.extend({
        tagName: "div",

        template: templateItem,

        events: {
            "click .checkbox": "chooseOpinion"
        },

        model: VoteDetailModel,

        initialize: function (options) {
            this.listenTo(this.model, 'add', this.render);
            this.listenTo(this.model, 'change:IsChecked', this.renderAfterChange);
            this.listenTo(this.model, 'change:TotalVote', this.renderAfterChange);
            this.listenTo(this.model, 'change:Percent', this.renderAfterChange);
            this.listenTo(this.model, 'change:UserIdsVote', this.renderAfterChange);
            var view = options.view;
            if (view == 4) {
                this.template = templateOnlyItem;
            }
            if (view == 3) {
                this.template = templateViewOnlyItem;
            }
        },

        chooseOpinion: function (e) {
            e.preventDefault();
            e.stopPropagation();
            var that = this;
            var isCheck = that.model.get("IsChecked");
            if (that.model.get("Option").IsMultiSelect) {
                if (isCheck) {
                    that.model.set("IsChecked", false);
                } else {
                    that.model.set("IsChecked", true);
                }
            } else {
                if (isCheck) {
                    return;
                }
                listVoteDetail.forEach(function (model, index) {
                    if (model.get("IsChecked") == true) {
                        model.set("IsChecked", false);
                    }
                });
                that.model.set("IsChecked", true);
            }
        },

        render: function () {
            var that = this;
            if (that.model.get("MaxVote") == 0) {
                that.model.set("Percent", 0);
            } else {
                var percent = parseInt(parseFloat(that.model.get("TotalVote") / that.model.get("MaxVote")).toFixed(2) * 100);
                that.model.set("Percent", percent);
            }
            that.$el.addClass("row");
            that.$el.html($.tmpl(that.template, that.model.toJSON()));
            that.renderUserInfo();
            
            return that;
        },

        renderAfterChange: function () {
            var width = this.model.get("Percent");
            this.render();
            this.$el.find('.progress .progress-bar').css("width",
                        function () {
                            return width + "%";
                        });
        },

        renderUserInfo: function () {
            var that = this;
            that.$el.find(".btnUserVote").customDropdown({
                css: {
                    width: 300,
                    height: 300
                },
                callback: function () {
                    if (that.model.get("UserIdsVote") != ";;" && that.model.get("UserIdsVote")) {
                        egov.request.getUserInfos({
                            data: {
                                userIds: that.model.get("UserIdsVote")
                            },
                            success: function (result) {
                                var template = '<li><a href="#">${UserName}</a></li>';
                                $("#ListUserVote").html($.tmpl(template, result))
                            },
                            error: function (error) {
                            }
                        });
                    } else {
                        $("#ListUserVote").html("");
                    }
                }
            });
           
        },

        getTotalVote: function (userVotes) {
            var listUser = userVotes.split(";");
            listUser = cleanArray(listUser);
            return listUser.length;
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
    var setTime = function (timeStr) {
        if (timeStr instanceof Date) { } else {
            timeStr = timeStr.match(/\d+/)[0];
        }
        var time = new Date(Number(timeStr));
        var day = "";
        switch (time.getDay()) {
            case 0:
                day = "Chủ nhật";
                break;
            case 1:
                day = "Thứ hai";
                break;
            case 2:
                day = "Thứ ba";
                break;
            case 3:
                day = "Thứ tư";
                break;
            case 4:
                day = "Thứ năm";
                break;
            case 5:
                day = "Thứ sáu";
                break;
            case 6:
                day = "Thứ bảy";
        }
        return day + "," + ("0" + time.getDate()).slice(-2) + "-" + ("0" + Number(time.getMonth() + 1)).slice(-2) + "-" + time.getFullYear() + " " + time.getHours() + ":" + ("0" + time.getMinutes()).slice(-2)
    }
    return ReferendumVote;
});