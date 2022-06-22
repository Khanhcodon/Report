define(function () {

    "use strict";

    var MultiSelection = Backbone.View.extend({

        el: "#multiselectionbar",

        $documentList: $("#documentList"),

        totalUnread: 0,
        totalRead: 0,
        selectedList: [],

        events: {
            //"tap .chkCheckAll": "checkAllDocument",
            "tap #btnhidemultiselection": "hideSelectionBar",
            "tap #btnmarkread": "markRead",
            "tap #btnmarkunread": "markUnRead",
        },

        initialize: function (options) {
            this.documentList = options.documentList;
            this.render();
        },

        render: function () {
            var that = this;
            if (!this.hasRender) {
                require([egov.template.document.multiselectionbar], function (Template) {
                    that.$el.html(Template);
                    that.$("#btnhidemultiselection").materialButton();
                    that.$(".mdl-menu").materialMenu();
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

        hideSelectionBar: function (e) {
            egov.helper.destroyClickEvent(e);
            
            this.selectedList = [];
            this.totalRead = 0;
            this.totalUnread = 0;

            var mailSelectedList = this.documentList.model.filter(function (item) {
                return item.get("Selected") == true
            });

            _.each(mailSelectedList, function (item) {
                item.set("Selected", false);
            })

            if (!egov.mobile.isTablet) {
                egov.commonFn.event.showNavbar();
            }

            //this.$documentList.find(".chkCheck").addClass("hidden-checkbox");
            $("#multiselectionbar").removeClass("display");
            $("#main-page").removeClass("showMultiSelectionBar");
            this.$documentList.find(".showMultiselectCheckBox").removeClass("showMultiselectCheckBox");
            egov.views.home.documents.isMultiSelecting = false;
            this.documentList.removeAllSelected()
        },

        showMultiSelectionBar: function () {
            if (!egov.mobile.isTablet) {
                egov.commonFn.event.hideNavbar();
            }
            //this.$documentList.find(".chkCheck").removeClass("hidden-checkbox");
            $("#multiselectionbar").addClass("display");
            $("#main-page").addClass("showMultiSelectionBar");
        },

        markRead: function (e) {
            egov.helper.destroyClickEvent(e);
            this.documentList.setViewed(this.selectedList, true);
            this.hideSelectionBar();
        },

        markUnRead: function (e) {
            egov.helper.destroyClickEvent(e);
            this.documentList.setViewed(this.selectedList, false);
            this.hideSelectionBar();
        },

        add: function (document, isAdd, textCount) {
            var isViewed = document.get("IsViewed") == 1 || document.get("IsViewed") == true;
            if (isAdd) {
                if (isViewed) {
                    this.totalRead++;
                }
                else {
                    this.totalUnread++;
                }
                this.selectedList.push(document);
            }
            else {
                if (isViewed) {
                    this.totalRead--;
                }
                else {
                    this.totalUnread--;
                }
                this.selectedList.splice(this.selectedList.indexOf(document), 1);
            }

            if (this.totalUnread == this.selectedList.length) {
                this.$("#btnmarkread").show();
                this.$("#btnmarkunread").hide();
            }
            else if (this.totalRead == this.selectedList.length) {
                this.$("#btnmarkread").hide();
                this.$("#btnmarkunread").show();
            }
            else {
                this.$("#btnmarkread").show();
                this.$("#btnmarkunread").show();
            }

            this.$("#selectionCount").text(textCount);
        },
    });

    return MultiSelection;
});
