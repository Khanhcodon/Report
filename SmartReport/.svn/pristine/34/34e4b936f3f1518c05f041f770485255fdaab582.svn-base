define([
   egov.template.referendum, "referendumModel"
],
function (template) {
    var Referendum = Backbone.View.extend({

        template: template,

        listVote: new ListVote(),

        events: {

        },

        initialize: function (options) {
            this.listenTo(this.listVote, 'add', this.addVote);
            this.listenTo(this.listVote, 'reset', this.resetVote);
            this.render();
            this.getVotes();
            return this;
        },

        render: function () {
            var that = this;
            $("#ListVoteBody").remove();
            installElement = $("<div id='ListVoteBody'>");
            installElement.appendTo("body");
            this.$el = $("#ListVoteBody");
            this.$el.html($.tmpl(this.template));

            var that = this;
            // that.$("#listRefendum").html(that.$el.ht)
            var dialogSetting = {
                width: 900,
                height: "auto",
                draggable: true,
                title: "Danh sách các cuộc thăm dò ý kiến",
                buttons: [{
                    text: "Tạo mới",
                    className: "btn-success",
                    click: function () {
                        that._create();
                    }
                },
                             {
                                 text: "Đóng",
                                 click: function () {
                                     that.$el.dialog("hide");
                                     that.$el.remove()
                                 }
                             }]
            };

            that.$el.dialog(dialogSetting);

            return that;
        },

        addVote: function (vote) {
            if (this.empty) {
                this.listVote.reset();
                this.empty = false;
            }
            var view = new ReferendumItem({ model: vote });
            this.$(".listVote").append(view.render().el);
        },

        getVotes: function () {
            var that = this;
            that.listVote.reset()
            egov.request.getVotes({
                data: {},
                success: function (result) {
                    if (result.length == 0) {
                        that.empty = true;
                        this.$(".listVote").html("<tr><td colspan='4'>Không có cuộc trưng cầu nào</td></tr>");
                        return;
                    }
                    for (var i = 0; i < result.length; i++) {
                        var vote = new VoteModel(result[i]);
                        that.listVote.add(vote);
                    }

                    that.listVote.comparator = function (model) {
                        return model.get('IsNow');
                    }
                    that.listVote.sort();
                },
                error: function (error) { }
            });
        },

        resetVote: function () {
            this.$(".listVote").html("");
        },

        _create: function () {
            that = this;
            require(['referendumCreate'], function (referendumCreate) {
                var create = new referendumCreate({ vote: that });
            });
        }
    });

    var ReferendumItem = Backbone.View.extend({
        tagName: "tr",

        events: {
            "dblclick td": "getShowView",
            "click .editVote": "editVote",
            "click .deleteVote": "deleteVote",
            "click .viewVote": "getShowView"
        },

        template:
            '<td class="wraptext">' +
                        '${Title}' +
                       '</td>' +
                       '<td class="second-color time-begin-vote" style="text-align:right;width: 150px"> ${TimeBeginFormat} </td>' +
                       '<td class="second-color wraptext" style="width: 100px">' +
                       '${UsernameCreate}' + '</td>' +
            '<td style="text-align:center;width: 100px">      <label class="checkbox document-color">          <input name="checkbox[]" value="2378" type="checkbox"{{if IsNow}}checked{{/if}} disabled>          <span class="document-color-1"><i class="icon-check"></i></span>      </label>  </td>'
            + '<td class="second-color wraptext" style="width: 100px">' +
                       '{{if IsCreate}}<a class="editVote"href="#" style="color: blue">Chi tiết</a> <a href="#" class="deleteVote" style="color: red">Xóa</a>{{else}}<a class="viewVote"href="#">Xem</a>{{/if}}' + '</td>',

        model: VoteModel,

        initialize: function (options) {
            var setTime = function (timeStr) {
                if (timeStr instanceof Date) { } else {
                    timeStr = timeStr.match(/\d+/)[0];
                }
                var time = new Date(Number(timeStr));
                return time.getHours() + ":" + ("0" + time.getMinutes()).slice(-2) + "   " + time.getDate() + "-" + Number(time.getMonth() + 1) + "-" + time.getFullYear()
            }
            var convertTime = function (timeStr) {
                if (timeStr instanceof Date) { } else {
                    timeStr = timeStr.match(/\d+/)[0];
                }
                var time = new Date(Number(timeStr));
                return time;
            }
            this.listenTo(this.model, 'add', this.render);
            this.listenTo(this.model, 'change', this.render);
            this.model.set({ "TimeBeginFormat": setTime(this.model.get("TimeBegin")) });
            if (new Date().getTime() > convertTime(this.model.get("TimeBegin")).getTime() && new Date().getTime() < convertTime(this.model.get("TimeEnd")).getTime()) {
                this.model.set({ "IsNow": true })
            }

        },

        render: function () {
            var that = this;
            that.$el.html($.tmpl(that.template, that.model.toJSON()));

            return that;
        },
        getShowView: function () {
            this.showView(4);
        },

        editVote: function () {
            var that = this;
            egov.request.getVoteDetail({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    require(['referendumCreate'], function (referendumCreate) {
                        var create = new referendumCreate({ vote: result, isEdit: true });
                    });
                },
                error: function (error) { }
            });
        },

        deleteVote: function () {
            var that = this;
            var r = confirm("Bạn có muốn xóa cuộc trưng cầu này");
            if (r == false) {
                return false;
            }
            egov.request.deleteVote({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    that.model.destroy();
                    that.$el.remove();
                },
                error: function (error) { }
            });
        },

        showView: function (view) {
            var that = this;
            egov.request.getVoteDetail({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    require(['referendumVote'], function (referendumVote) {
                        var model = new VoteModel(result);
                        var vote = new referendumVote({ model: model, view: view });
                        vote.render();
                    });
                },
                error: function (error) { }
            });
        },

        _create: function () {
        }
    });

    return Referendum;
});