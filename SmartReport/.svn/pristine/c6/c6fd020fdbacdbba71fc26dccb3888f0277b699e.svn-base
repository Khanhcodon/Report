define([
    'bmailmakeRequest',
    'bmailModel',
    'bmailUtil',
    'bmailMsgUtil'
],
function () {

    "use strict";

    var _template = egov.template.bmail;

    var BmailView = Backbone.View.extend({

        el: "#bmailFrame",

        displayClass: "display",

        activeFolder: "inbox",
        firstLoad: 50,
        autoCompleteResult: [],

        currentPage: 1,

        events: {
            "swipeup .dataList": "swipeup",
            "swipedown .dataList": "swipedown",
            "click #btnNewMail": "_createMail",
            "click #btnSearchMail": "_openSearch",
            "click #btnCreatePersonMail": "sendPersonMail",
            "click #btnCreateBoxMail": "sendBoxMail",
            "click .btnEmptyFolder": "emptyFolder",
            "click .btnMarkFolderRead": "markFolderRead",

            'click .mdl-list__item--data': "_selectFolder",
            'click .list-mails .mdl-list__item': '_openMailHandler'
        },

        initialize: function () {
            var that = this;
            that.mails = {};
            that.allMail = [];

            bmail.request.preAuthen(function () {
                //bmail.authenCookie = $.cookie("bkavAuthen");
                that.render();
            });
        },

        render: function () {
            var that = this;

            egov.mobile.showProcessBar();

            bmail.request.getUserInfo(function (folderList, userInfo) {
                bmail.user = userInfo;
                $.cookie("user", userInfo.email);

                egov.mobile.hideProcessBar();

                that._renderFolderMenu(folderList);

                that._registerApiGlobal();

            });
        },

        _registerApiGlobal: function () {
            var that = this;

            egov.pubsub.subscribe("bmail.open", that._openMail, that);
            egov.pubsub.subscribe("bmail.moveToTrash", that.moveToTrash, that);

            egov.pubsub.subscribe("maillist.refresh", that.refreshMailList, that);
            egov.pubsub.subscribe("maillist.show", that.showListMail, that);
        },

        //#region Hiển thị cây thư mục

        _renderFolderMenu: function (folderList) {
            var that = this;
            folderList = this._sortBy(folderList);
            this.folders = new bmail.models.folderList(folderList);

            require([_template.folderItem], function (FolderTemplate) {
                var privateFolders = _.where(folderList, { "isPublicFolder": false });
                var publishFolders = _.sortBy(_.where(folderList, { "isPublicFolder": true }), function (f) {
                    return f.name;
                });

                that.$("#folderlist").html($.tmpl(FolderTemplate, privateFolders));
                if (publishFolders.length > 0) {
                    that.$("#folderlist .mail-folder-item:last-child").addClass("mdl-menu__item--full-bleed-divider");
                    that.$("#folderlist").append($.tmpl(FolderTemplate, publishFolders));
                }

                that._focusFisrtFolder();
            });
        },

        _focusFisrtFolder: function () {
            this.$("#folderlist .mdl-list__item--data").first().click();
        },

        _sortBy: function (folderList) {
            return _.sortBy(folderList, function (folder) {
                return folder.id;
            });
        },

        _selectFolder: function (e) {
            egov.mobile.autoHideMainMemu();

            var target = $(e.target).closest(".mdl-list__item--data");
            $('.mdl-list__item--data.active').removeClass("active");
            target.addClass("active");

            var parentId = target.is(".isChildren") ? target.parents("ul.children").attr("parent-id") : 0;
            var folderId = target.attr("id");
            var folderName = target.find(".node-name").text();
            this.$(".header-title").text(folderName);

            var parent = parentId != 0 && this.folders.detect(function (f) {
                return f.get("id") == parentId;
            });

            var currentFolder;
            if (parent) {
                currentFolder = _.find(parent.get("children"), function (child) {
                    return child.id == folderId;
                });

                currentFolder && (currentFolder = new bmail.models.folder(currentFolder));
            } else {
                currentFolder = this.folders.detect(function (f) {
                    return f.get("id") == folderId;
                });
            }

            if (currentFolder == undefined) {
                return;
            }

            this.currentFolder = currentFolder;
            this.currentPage = 1;
            this.$("#mailList").empty();
            this.refreshMailList(false);
        },

        //#endregion

        //#region Hiển thị danh sách mail

        _renderMailList: function (folderId, template) {
            var currentMails = this.mails[folderId];
            var that = this;
            that._hideNoElementPage();

            var mailGroups = _.groupBy(currentMails, "groupDate");
            var length = _.keys(mailGroups).length;
            var idx = 0;
            _.each(mailGroups, function (mails, groupName) {
                that.$("#mailList").append('<div class="group-label" data-group="important"><span>' + groupName + '</span></div>');
                that.$('#mailList').append($('<ul>').addClass('mdl-list').append($.tmpl(template, mails)));
                idx++;
                if (length === idx) {
                    that.hasMore && that._loadMoreHandler();
                }
            });
        },

        _openMailHandler: function (e) {
            var target = $(e.target).closest("li.mdl-list__item");
            var domid = target.attr("id");
            var id = domid.replace("maillist_", "");
            egov.pubsub.publish("bmail.open", { mailId: id });
        },

        _openMail: function (option) {
            var mailId = option.mailId;
            egov.mobile.showDetailPage("bmail");
            var mailInfo = _.find(this.allMail, function (mail) {
                return mail.id === mailId;
            });

            egov.mobile.showProcessBar();
            require(["bmailViewDetail"], function (MailDetail) {
                var mailDetail = new MailDetail({
                    model: mailInfo,
                    id: mailId,
                });
            });

            return;
        },

        //#endregion

        //#region Private

        _showNoElementPage: function (icon, message) {
            this.$(".no-element-content").removeClass("hidden");
            this.$(".no-element-content .material-icons").text(icon);
            this.$(".no-element-content .message-info").text(message);
            this.$(".page-content").addClass("hidden");
        },

        _hideNoElementPage: function () {
            this.$(".no-element-content").addClass("hidden");
            this.$(".page-content").removeClass("hidden");
        },

        //#endregion

        //#region Khác

        swipedown: function (e) {
            this.showNewBtn();
        },

        swipeup: function (e) {
            this.hideNewBtn();
        },

        hideNewBtn: function () {
            this.$btnNewMail.removeClass("display");
        },

        showNewBtn: function () {
            this.$btnNewMail.addClass("display");
        },

        refreshMailList: function (isLoadMore) {
            var that = this;
            isLoadMore = isLoadMore || false;
            var currentFolder = this.currentFolder;
            var isSentBox = currentFolder.get("path") === "";
            var take = 25;
            var skip = (this.currentPage - 1) * take;
            var path = currentFolder.get("path");
            var folderId = currentFolder.get('id');
            var name = currentFolder.get("name");
            var target = this.$("[id='" + folderId + "']");

            egov.mobile.showProcessBar();
            egov.mobile.hideDetailPage();

            bmail.request.getFolderMailList(skip, path, take, isSentBox, function (result) {
                that.mails[folderId] = result.mails;
                that.hasMore = result.more;
                that.hasMore && (that.currentPage++);
                that.allMail = _.union(that.allMail, result.mails);

                egov.mobile.hideProcessBar();

                if (result.mails.length === 0) {
                    that._showNoElementPage(target.find(".material-icons").text(), name);
                    return;
                }

                require([_template.listItem], function (ListItemTemplate) {
                    that._renderMailList(folderId, ListItemTemplate, isLoadMore);
                });
            });
        },

        _loadMoreHandler: function () {
            return this.$(".egov-maincontent").scrollLoadMore('next', this.refreshMailList.bind(this));
        },

        showDetailMail: function (e) {
            this.isShowingDetailMail = true;
            this.$detailWrap.addClass("display");
            if (!egov.mobile.isTablet) {
                this.$mailListWrap.removeClass("display");
                egov.commonFn.event.hideNavbar();
            }
        },

        showListMail: function (e) {
            if (!e && this.isShowingDetailMail) {
                return;
            }

            egov.mobile.hideDetailPage();

            this.isShowingDetailMail = false;
        },

        sendPersonMail: function () {
            this._createMail({});
        },

        _createMail: function () {
            var data = {};
            var that = this;
            if (this.currentFolder.get("isPublicFolder") == true) {
                data = {
                    to: this.currentFolder.get("path"),
                    folderId: this.currentFolder.get("id")
                };
            }

            egov.mobile.showDetailPage("compose");
            if (that.ComposeMailView) {
                that.ComposeMailView.render(data);
            } else {
                require(["bmailCreateOrReply"], function (CreateOrReply) {
                    that.ComposeMailView = new CreateOrReply(data);
                });
            }
        },

        createMail: function (e) {
            var that = this;
            if (bmail.current.folder && bmail.current.folder.isPublicFolder) {
                this.changeCreateIcon();
            }
            else {
                this.sendPersonMail();
            }
        },

        moveToTrash: function (listMail) {
            /// <summary>
            /// Xóa mail (cho vào thùng rác)
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.request.msgAction("trash", listMail, function () {
                var mailsId = listMail.split(",");

                egov.pubsub.publish("maillist.show", { fuck: "doannx" });
                egov.pubsub.publish("maillist.refresh");
            });
        },

        deleteMailForever: function (listMail) {
            /// <summary>
            /// Xóa mail vĩnh viễn
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.request.msgAction("delete", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    that.model.remove(that.model.get(mailId));
                })
            });
        },

        changeCreateIcon: function () {
            if ($("#ddlnewmail").hasClass(this.displayClass)) {
                $("#btnNewMail .material-icons").text("create");
            }
            else {
                $("#btnNewMail .material-icons").text("close");
                egov.mobile.showModal();
                $(document).one("click", function (e) {
                    if ($(e.target).closest("#ddlnewmail").length == 0) {
                        egov.helper.destroyClickEvent(e);
                    }
                    $("#btnNewMail .material-icons").text("create");
                    egov.mobile.hideModal();
                    $("#ddlnewmail").toggleClass(this.displayClass);
                });
            }
            $("#ddlnewmail").toggleClass(this.displayClass)
        },

        emptyFolder: function () {
            var folderId = bmail.current.folder.folderId;
            bmail.makeRequest.setEmptyFolder(folderId, function () {
                bmail.current.folder.emptyFolder();
                bmail.current.maillist.refresh();
            });
        },

        markFolderRead: function () {
            var folderId = bmail.current.folder.folderId;
            bmail.makeRequest.setFolderRead(folderId, function () {
                bmail.current.maillist.refresh();
            });
        },

        _openSearch: function (e) {
            egov.mobile.showSearchPage("bmail");
            require(['searchBmail'], function (BmailSearchView) {
                new BmailSearchView();
            });

            e.preventDefault();
        },

        //#endregion
    });

    return BmailView;
});
