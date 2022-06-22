define(
['jquery', 'backbone', egov.template.document.appoint],
function ($, Backbone, Template) {

    var _resource = egov.resources.document.appoint;

    var AppointView = Backbone.View.extend({
        doccode: "",
        that: null,
        isCreate: true,
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
                url: "https://kntc.bkav.com/TalkingPeople/GetAppointInfo",
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
                remind: "",
                talkingDate: "",
                number: 0
            }
            if (info && info.hasBook) {
                var data = info.data;
                this.model = {
                    talkingid: data.id,
                    talkingDate: data.talkingDate,
                    remind: data.remind,
                    number: data.number
                };
                this.isCreate = false;
            }
            this.$el.html($.tmpl(this.template, this.model));
            this.$el.bindResources();

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
                                if (that.isCreate) {
                                    that._createAppointment();
                                }
                                else {
                                    that._updateAppointment();
                                }
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
            that.$(".remind").focus();
        },

        _createAppointment: function () {
            var that = this;
            var dateAppoint = that.$(".dateAppoint").val();
            var remind = that.$(".remind").val();

            var ajaxOptions = {
                type: "POST",
                dataType: "JSON",
                url: "https://kntc.bkav.com/TalkingPeople/CreateAppoint",
                data: {
                    doccode: that.doccode,
                    remind: remind,
                    dateAppoint: dateAppoint
                },
                success: function (data) {
                    if (data.success) {
                        that.$el.dialog("hide");
                        egov.pubsub.publish(egov.events.status.success, _resource.createAppointSuccess);
                        return;
                    }
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.error);
                },
                error: function (xhr, error) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.error);
                }
            }
            $.ajax(ajaxOptions);
        },
        _updateAppointment: function () {
            var that = this;
            var talkingid = that.model.talkingid;
            var dateAppoint = that.$(".dateAppoint").val();
            var remind = that.$(".remind").val();

            var ajaxOptions = {
                type: "POST",
                dataType: "JSON",
                url: "https://kntc.bkav.com/TalkingPeople/UpdateAppoint",
                data: {
                    id: talkingid,
                    remind: remind,
                    dateAppoint: dateAppoint
                },
                success: function (data) {
                    if (data.success) {
                        that.$el.dialog("hide");
                        egov.pubsub.publish(egov.events.status.success, _resource.updateAppointSuccess);
                        return;
                    }
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.error);
                },
                error: function (xhr, error) {
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.error);
                }
            }
            $.ajax(ajaxOptions);
        },
        _validInfo: function () {
            if (this.$(".remind").val() == "") {
                this.$(".remind").focus();
                return false;
            }
            else if (this.$(".dateAppoint").val() == "") {
                this.$(".dateAppoint").focus();
                return false;
            }
            return true;
        },
    });

    return AppointView;
});