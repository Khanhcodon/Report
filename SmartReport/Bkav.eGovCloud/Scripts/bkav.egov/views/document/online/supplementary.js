define([
    'jquery',
    egov.template.document.supplementaryOnline
],
function ($, SuppTemplate) {
    "use strict";

    var _resource = egov.resources.document.supplementary;
    //#region Relation View

    var SupplementaryView = Backbone.View.extend({

        $txtRequire: undefined,

        docId: 0,

        id: "supplementaryOnline",

        initialize: function (options) {
            var that = this;
            that.docId = options.docId;
            that.template = SuppTemplate;
            that.onlineModel = options.onlineModel;
            that.document = options.document;

            that.render();
        },

        events: {
            "blur #addRequire": "addRequire_Blur",
            "keyup #addRequire": "addRequire_KeyUp",
            "click .removeItem": "removeRequire"
        },

        render: function () {
            var that = this;
            var dialogSetting = {
                title: _resource.name,
                width: '800px',
                draggable: true,
                keyboard: true,
                height: "auto",
                buttons: [
                    {
                        id: "btnSendSuplementary",
                        className: 'btn-primary',
                        text: _resource.sendSuplementRequire,
                        click: function () {
                            that.sendSuplementRequire(function () {
                                that.document.tab.close3();
                                that.$el.dialog('destroy');
                            });
                        }
                    },
                    {
                        id: "btnHideSupForm",
                        className: 'btn-close',
                        text: egov.resources.common.closeButton,
                        click: function () {
                            that.$el.dialog('destroy');
                        }
                    }
                ]
            };
            this.$el.html($.tmpl(this.template, {}));
            this.$txtRequire = this.$("#addRequire");
            this.$el.dialog(dialogSetting);
        },

        addRequire_KeyUp: function (e) {
            /// <summary>
            /// Thêm yêu cầu bằng phím Enter
            /// </summary>
            /// <param name="e"></param>
            if (e.keyCode == 13) {
                this.addRequire();
            }
        },

        addRequire_Blur: function (e) {
            egov.helper.destroyClickEvent(e);

            this.addRequire();
        },

        addRequire: function () {
            var requirement = this.$txtRequire.val();
            if (requirement.length > 0) {
                var number = this.$("#supItemZone").children(".suppItem").length + 1;
                var suppItem = "<div class='row suppItem'><div class='col-md-1 sttrow'>"
                    + number
                    + ".</div><div class='col-md-14 wraptext'>"
                    + requirement
                    + "</div><div class='col-md-1 removeItem'><span class='icon icon-close'></span></div></div>";
                this.$("#supItemZone").append(suppItem);
                this.$txtRequire.val("");
            }
        },

        removeRequire: function (e) {
            egov.helper.destroyClickEvent(e);
            var $currentRow = $(e.currentTarget).closest(".row");
            var index = $currentRow.index();
            var $rowAfter = $currentRow.next(".row");
            while ($rowAfter.length > 0) {
                $rowAfter.find(".sttrow").html(++index + ".");
                $rowAfter = $rowAfter.next(".row");
            }
            $currentRow.remove();
        },

        sendSuplementRequire: function (callback) {
            var that = this;
            var requireItems = this.$("#supItemZone").children(".suppItem");
            if (requireItems.length == 0) {
                this.$txtRequire.focus();
                return;
            }
            var comment = this.$("#txtComment").val();
            if (comment.length == 0) {
                this.$("#txtComment").focus();
                return;
            }

            var expireDate = this.$("#expireDate").val();
            if (expireDate.length == 0 || parseInt(expireDate) <= 0) {
                this.$("#expireDate").focus();
                return;
            }

            var supplement = "";
            _.each(requireItems, function (item) {
                supplement += $(item).find(".wraptext").text() + "-----";
            });

            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

            egov.request.additionalRequirements({
                data: {
                    'docId': that.docId,
                    'phone': that.onlineModel.Phone,
                    'mail': that.onlineModel.Email,
                    'comment': comment,
                    'expireDate': expireDate,
                    'supplement': supplement,
                    'compendium': that.onlineModel.Compendium,
                    'doctypeId': that.onlineModel.DoctypeId,
                    'docFieldId': that.onlineModel.DocfieldId,
                    'docCode': that.onlineModel.DocCode,
                    'citizenName': that.onlineModel.FullName,
                    'dateRegister': that.onlineModel.DateRecieved,
                    'token': that.onlineModel.Token
                },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }
                    if (result.success) {
                        egov.pubsub.publish(egov.events.status.success, result.success);
                        if (that.tab) {
                            that.tab.close3();
                        }
                        egov.views.home.tree.reloadOnlineRegistration();
                    }
                    egov.callback(callback);
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                    egov.callback(callback);
                }
            });
        }
    });
    //#endregion

    return SupplementaryView;
});