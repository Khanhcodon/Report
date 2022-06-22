define(
[egov.template.publicResult],
function (Template) {

    var _resource = egov.resources.document.publicResult;

    var PublicResultView = Backbone.View.extend({
        doccode: "",
        that: null,

        template: Template,

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.document = options.document;
            this.doccode = this.document.model.get("DocCode")
            that = this;
            var ajaxOptions = {
                type: "GET",
                dataType: "JSON",
                url: "https://kntc.bkav.com/TalkingPeople/GetBookInfo",
                data: {
                    doccode: that.doccode,
                },
                success: function (data) {
                    that.render(data);
                },
                error: function (xhr, error, result) {
                    console.log({ error: { xhr: xhr, e: error, info: result } });
                    that.render();
                }
            }
            $.ajax(ajaxOptions);
            return that;
        },

        render: function (info) {
            /// <summary>
            /// Page Load
            /// </summary>
            this.model = {
                result: "",
                talkingDate: "",
                isPublic: false,
                finishDate: null
            }
            if (info && info.hasBook) {
                var data = info.data;
                this.model = {
                    result: data.finalResult,
                    talkingDate: data.talkingDate,
                    isPublic: data.isPublic,
                    finishDate: data.finishDate
                }
            }
            this.$el.html($.tmpl(this.template, this.model));
            this.$el.bindResources();
            //if (!info) {
            //    if (info.hasBook) {
            //        var data = info.data;
            //        this.$(".result").val(data.finalResult);
            //        this.$(".talkingDate").val(data.talkingDate);
            //        if (data.isPublic) {
            //            $("#isAllowPublic").attr("checked", "checked");
            //        }
            //        this.$el.prepend("<div class=''></div>")
            //        data.finishDate

            //    }
            //    var dateAppoint = that.$(".dateAppoint").val();
            //    var result = that.$(".result").val();
            //    var isAllowPublic = that.$("#isAllowPublic").is(":checked");
            //}
            this.$(".datepicker").datepicker();
            var that = this;
            var dialogSetting = {
                width: 800,
                height: 270,
                draggable: true,
                keyboard: true,
                modal: true,
                title: _resource.titleDialog,
                buttons: [
                    {
                        text: _resource.updateButton,
                        className: "btn-primary",
                        click: function () {
                            if (that._validInfo()) {
                                that._updateResult();

                            }
                        }
                    },
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog("hide");
                        }
                    },
                ]
            };
            that.$el.dialog(dialogSetting);
            that.$(".result").focus();
        },

        _updateResult: function () {
            var isFinish = that.$("#finishProcess").is(":checked");
            var dateAppoint = that.$(".dateAppoint").val();
            var result = that.$(".result").val();
            var isAllowPublic = that.$("#isAllowPublic").is(":checked");

            var ajaxOptions = {
                type: "POST",
                dataType: "JSON",
                url: "https://kntc.bkav.com/Document/FinishDocument",
                data: {
                    id: that.document.model.get("DocCode"),
                    result: result,
                    appointDate: dateAppoint,
                    isPublic: isAllowPublic
                },
                success: function (data) {
                    if (isFinish) {
                        that.document.finish();
                    }
                    that.$el.dialog("hide");
                },
                error: function (xhr, error) {
                    debugger;
                }
            }
            $.ajax(ajaxOptions);
        },

        _validInfo: function () {
            if (this.$(".result").val() == "") {
                this.$(".result").focus();
                return false;
            }
            else if (this.$(".dateAppoint").val() == "") {
                this.$(".dateAppoint").focus();
                return false;
            }
            return true;
        },
    });

    return PublicResultView;
});