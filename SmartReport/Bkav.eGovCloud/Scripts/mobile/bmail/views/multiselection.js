define(function () {

    "use strict";

    var MultiSelection = Backbone.View.extend({

        el: "#multiselectionbar",

        buttons: {
            isShowDelete: false,
            isShowDeletePer: false,
            isShowSpam: false,
            isShowUnSpam: false,
            isShowUnread: false,
            isShowRestore: false,
        },

        totalSelected: 0,

        totalRead: 0,

        totalUnread: 0,

        selectedMailsId: [],

        events: {
            //"tap .chkCheckAll": "checkAllDocument",
            "click #btnhidemultiselection": "hideMultiSelectionBar",
            "click #btndelete": "deleteMail",
            "click #btndeleteper": "deleteMailPer",
            "click #btnspam": "markSpam",
            "click #btnunspam": "unMarkSpam",
            "click #btnunread": "markunread",
            "click #btnread": "markRead",
            "click #btnrestore": "restore",
        },

        initialize: function (options) {
            this.mailList = options.mailList;
            this.initData();
            this.render();
            bmail.views.multiSelectionBar = this;
        },

        initData: function () {
            this.model = bmail.current.folder.toolbar.model;
            this.buttons.isShowDelete = this.model.indexOf("delete") > -1;
            this.buttons.isShowDeletePer = this.model.indexOf("deletePer") > -1;
            this.buttons.isShowSpam = this.model.indexOf("spam") > -1;
            this.buttons.isShowUnSpam = this.model.indexOf("unspam") > -1;
            this.buttons.isShowUnread = this.model.indexOf("unread") > -1;
            this.buttons.isShowRestore = this.model.indexOf("restore") > -1;
        },

        render: function () {
            var that = this;
            if (!this.hasRender) {
                require([bmail.templates.multiSelectionBar], function (Template) {
                    that.$el.html($.tmpl(Template, that.buttons));
                    that.$(".mdl-button").materialButton();
                    that.$(".mdl-menu").materialMenu();
                    that.$(".mdl-js-ripple-effect").materialRipple();
                    that.$el.show();
                    that.hasRender = true;
                });
            }

            this.showMultiSelectionBar();
        },

        checkAllDocument: function (e) {
            egov.helper.destroyClickEvent(e);
            var $newtarget = $(e.currentTarget).find('.checkbox');
            if ($newtarget.hasClass("checked")) {
                $newtarget.removeClass("checked");
                egov.views.home.documents.removeAllSelected();
            }
            else {
                $newtarget.addClass("checked");
                egov.views.home.documents.setAllSelected();
            }
            egov.views.home.documents.countDocumentsMark();
        },

        hideMultiSelectionBar: function (e) {
            egov.helper.destroyClickEvent(e);
            this.totalSelected = 0;
            this.totalRead = 0;
            this.totalUnread = 0;
            this.selectedMailsId = [];
            if (!egov.mobile.isTablet) {
                egov.commonFn.event.showNavbar();
            }

            //this.documentList.find(".chkCheck").addClass("hidden-checkbox");
            $("#multiselectionbar").removeClass("display");

            $("#mailListWrap .mail-list").children().removeClass("showMultiselectCheckBox");
            this.hideDropDown();
            bmail.views.main.showNewBtn();
            bmail.views.maillist.isShowingMultiSelectionBar = false;
            this.mailList.removeAllSelected();
        },

        showMultiSelectionBar: function () {
            if (!egov.mobile.isTablet) {
                egov.commonFn.event.hideNavbar();
            }
            //this.documentList.find(".chkCheck").removeClass("hidden-checkbox");
            bmail.views.main.hideNewBtn();
            $("#multiselectionbar").addClass("display");
        },

        hideDropDown: function () {
            this.$el.find(".mdl-menu__container").removeClass("is-visible");
        },

        add: function (isAdd, mail, text) {
            if (isAdd) {
                if (mail.get("unread")) {
                    this.totalUnread++;
                }
                else {
                    this.totalRead++;
                }
                this.selectedMailsId.push(mail.id);
                this.totalSelected++;
            }
            else {
                if (mail.get("unread")) {
                    this.totalUnread--;
                }
                else {
                    this.totalRead--;
                }
                this.selectedMailsId.splice(this.selectedMailsId.indexOf(mail.id), 1);
                this.totalSelected--;
            }

            if (this.totalUnread == this.totalSelected) {
                this.$("#btnread").show();
                this.$("#btnunread").hide();
            }
            else if (this.totalRead == this.totalSelected) {
                this.$("#btnread").hide();
                this.$("#btnunread").show();
            }
            else {
                this.$("#btnread").show();
                this.$("#btnunread").show();
            }
            this.$("#selectionCount").text(text);
        },

        deleteMail: function (e) {
            egov.helper.destroyClickEvent(e);
            this.mailList.deleteMail(this.selectedMailsId.join(","));
            this.hideMultiSelectionBar();
        },

        deleteMailPer: function (e) {
            egov.helper.destroyClickEvent(e);
            this.mailList.deleteMailPer(this.selectedMailsId.join(","));
            this.hideMultiSelectionBar();
        },

        markSpam: function (e) {
            egov.helper.destroyClickEvent(e);
            this.mailList.markSpam(this.selectedMailsId.join(","));
            this.hideMultiSelectionBar();
        },

        unMarkSpam: function (e) {
            egov.helper.destroyClickEvent(e);
            this.mailList.unMarkSpam(this.selectedMailsId.join(","));
            this.hideMultiSelectionBar();
        },

        markRead: function (e) {
            egov.helper.destroyClickEvent(e);
            this.mailList.markAsRead(this.selectedMailsId.join(","));
            this.$("#btnread").parent().hide();
            this.$("#btnunread").parent().show();
            this.hideMultiSelectionBar();
        },

        markunread: function (e) {
            egov.helper.destroyClickEvent(e);
            this.mailList.markUnRead(this.selectedMailsId.join(","));
            this.$("#btnunread").parent().hide();
            this.$("#btnread").parent().show();
            this.hideMultiSelectionBar();
        },

        restore: function (e) {
            this.mailList.restore(this.selectedMailsId.join(","));
            this.hideMultiSelectionBar();
        },

    });

    return MultiSelection;
});
