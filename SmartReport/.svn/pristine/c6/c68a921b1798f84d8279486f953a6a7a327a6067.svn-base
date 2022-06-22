define([
    'bmailCustomToolbar',
],
function () {

    "use strict";

    var _template = egov.template.bmail;

    var BmailViewFolder = Backbone.View.extend({

        el: "#folderlist",
        $mailListWrap: $("#mailListWrap"),

        cache: bmail.locache,

        childs: [],

        events: {
            'click .mail-folder-item': "_selectFolder"
        },

        initialize: function (models) {
            this.model = new bmail.models.folderList(models);
            this.render();
        },

        render: function () {
            var that = this;
            var folders = this.model;

            require([_template.folderItem], function (FolderTemplate) {
                that.$el.html($.tmpl(FolderTemplate, that.model.toJSON()));
            });
        },

        _selectFolder: function (e) {
            var target = $(e.target).closest(".mail-folder-item");
            var folderId = target.attr("id");

            var currentFolder = this.model.detect(function (f) {
                return f.get("id") === folderId;
            });

            if (currentFolder == undefined) {
                return;
            }

            egov.bmailView.CurrentFolder = currentFolder;

            egov.mobile.showProcessBar();
            var that = this;
            that.isSentBox = false;
            var path = currentFolder.get("path");
            bmail.request.getFolderMailList(0, path, 25, that.isSentBox, function (mails) {
                that._renderMailList(mails);
            });

            return;

            require(["bmailViewMailList"], function (MailListView) {
                var model;

                var mailList = new MailListView({
                    folder: that,
                    model: model,
                    id: mailListId,
                    folderPath: that.model.get("path"),
                    folderId: that.model.get("id")
                });

                that.mailList = mailList;

                that.$mailListWrap.append(mailList.$el);

                that.removeAllSelected();
                that.addSelected(e);

                mailList.render(model, false, true, function () {
                    bmail.request.getFolderMailList(0, that.model.get("path"), firstLoadMailNumber,
                        that.isSentBox, function (data) {
                            mailList.reRender(data);
                        });
                });
            });

            this.parent.cache.setLastFolderIndex(that.index);
            var folderId = parseInt(that.model.get("folderId"));
            if (!folderId || folderId > 6) {
                egov.mobile.setNofifyFolder(that.model.get("path"));
            }
            if (!egov.mobile.elements.mainPage.hasClass("showNotify")) {
                egov.commonFn.event.changeTitleForMobile("bmail", that.model.get("pathName"));
            }
        },

        _renderMailList: function () {

        },

        changeFolderStructure: function (folder) {
            if (folder && this.model.get(folder.id)) {
                this.model.get(folder.id).set("isOpen", folder.get("isOpen"))
            }
            this.cache.setFolderList(JSON.stringify(this.model.toJSON()));

            bmail.makeRequest.changeFolderStructure(this.model, function () {

            });
        },

        getPath: function (id) {
            var result = this.model.models.find(function (model) {
                return model.get("id") == id;
            });

            if (result) {
                return result.get("path");
            }
        },

        isCommonBox: function (id) {
            var path = this.getPath(id);
            if (path != null) {
                return ["drafts", "sent", "junk", "trash"].indexOf(path.indexOf("/") > 0 ? path.substr(0, path.indexOf("/")) : path) > -1;
            }
            return false;
        },

        rebindTotalUnread: function () {
            _.each(bmail.views.folderlist.childs, function (item) {
                var folderInfo = getFolderInfo(item.folderId);
                if (folderInfo) {
                    item.model.set("totalUnread", folderInfo.unread);
                }
            });

            console.log("folder's struct has updated!");
        }
    });

    var FolderItem = Backbone.View.extend({

        className: "mdl-list",

        tagName: 'ul',

        $mailListWrap: $("#mailListWrap"),

        idForMailList: "withFolderId_",

        maillistClass: ".mail-list",

        domId: "foldermail_",

        folderId: 0,

        events: {
            "click": "select",
            "click .collapsableMinus": "close",
            "click .collapsablePlus": "open",
        },

        initialize: function (options) {
            this.parent = options.parent;
            this.index = options.folderIndex;
            this.isSentBox = options.model.isSentBox;
            this.initData(options.model);
            this.initToolbar(true);
            this.setEventOfModel();
            this.render();
        },

        initData: function (folder) {
            var that = this;
            var path = folder.path;
            var level = folder.path.split("/").length;
            this.folderId = folder.id;

            this.model = new bmail.models.folder({
                path: path,
                pathName: folder.name, //translate(path.lastIndexOf("/") < 0 ? path : path.substr(path.lastIndexOf("/") + 1)),
                id: folder.id,
                visible: folder.visible,//0 || 1
                link: folder.link,
                parentid: folder.parentid,
                totalUnread: folder.totalUnread,
                isPublicFolder: folder.isPublicFolder,
                hasChildren: folder.hasChildren,
                isOpen: 0,
                isSentBox: folder.isSentBox,
                perm: folder.perm,
                className: getClassName(folder.hasChildren, level, folder.isOpen, folder.visible, folder.isPublicFolder),
                domId: that.domId + folder.id,
                folderId: folder.id.replace(/:/gi, ""),
                level: level
            });
        },

        setEventOfModel: function () {
            var that = this;
            this.model.on("change:totalUnread", function (model, totalUnread) {
                if (totalUnread > 0) {
                    that.$(".mdl-badge").first().attr("data-badge", egov.mobile.getTotalUnreadLabel(totalUnread));
                }
                else {
                    that.$(".mdl-badge").first().removeAttr("data-badge");
                }
            })
        },

        initToolbar: function (isTextOnly) {
            var that = this,
                model;

            if (this.model.get("level") == 1) {
                var items = [], isMain = [], notMain = [];
                var path = this.model.get("path").split("/")[0];
                this.isTextOnly = isTextOnly == true;

                var itemsName = bmail.customToolbar.getToolbar(path).split(",");

                for (var i = 0; i < itemsName.length; i++) {
                    items.push(itemsName[i].trim());
                }
                model = items;
            }
            else {
                model = bmail.toolbarList.filter(function (toolbar) {
                    return toolbar.folderId == that.model.get("parentid")
                })[0].model;
            }

            that.toolbar = {
                folderId: this.model.get("id"),
                folderPath: this.model.get("path"),
                model: model
            };

            bmail.toolbarList.push(that.toolbar);
        },

        render: function () {
            var that = this;
            this.$el.attr("folderId", this.model.get("id"));
            this.$el.attr("id", this.model.get("domId"));
            this.$el.attr("parentId", this.model.get("parentid"));
            this.$el.addClass(this.model.get("className"));
            var $item = $.tmpl(bmail.templates.store.folder, that.model.toJSON());
            var totalUnread = this.model.get("totalUnread");
            if (totalUnread > 0) {
                $item.find(".mdl-badge").attr("data-badge", egov.mobile.getTotalUnreadLabel(totalUnread));
            }
            //$item.find(".mdl-button").materialButton();
            //$item.find(".mdl-js-ripple-effect").materialRipple();
            this.$el.html($item);

        },

        select: function (e) {
            egov.helper.destroyClickEvent(e);
            egov.mobile.hidePanel(e);
            egov.mobile.hideMainMenu();

            var that = this;

            bmail.current.folder = this;
            bmail.current.folder.isPublicFolder = this.model.get("isPublicFolder");
            bmail.current.folder.folderId = this.model.get("folderId");

            if (!this.hasBind) {
                that.$mailListWrap.children().hide();
                egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

                require(["bmailViewMailList"], function (MailList) {
                    var model;
                    var locacheData = that.parent.cache.getLastMailList();
                    if (locacheData) {
                        locacheData = JSON.parse(locacheData);
                        //Kiểm tra xem dữ liệu mail locache có phải là của thư mục đang chọn hiện tại hay không?
                        //Nếu không phải thì không gán vào model
                        if (locacheData && locacheData instanceof Array && locacheData.length > 0 && locacheData[0].l == that.folderId) {
                            model = locacheData;
                        }
                    }
                    var mailListId = that.idForMailList + that.model.get("folderId");
                    //if (that.$mailListWrap.children("#mailListId").length == 0) {
                    var mailList = new MailList({
                        folder: that,
                        model: model,
                        id: mailListId,
                        folderPath: that.model.get("path"),
                        folderId: that.model.get("id")
                    });
                    that.mailList = mailList;
                    that.$mailListWrap.append(mailList.$el);
                    that.removeAllSelected();
                    that.addSelected(e);
                    $("#btnNewMail").show();
                    mailList.render(model, false, true, function () {
                        bmail.makeRequest.getFolderMailList(0, that.model.get("path"), firstLoadMailNumber,
                            that.isSentBox, function (data) {
                                mailList.reRender(data);
                                that.parent.cache.setLastMailList(JSON.stringify(data));
                                egov.pubsub.publish(egov.events.status.destroy);
                            });
                    })

                    //}
                });
                that.hasBind = true;
            } else {
                this.mailList.folderRefresh();
                if (!this.$el.hasClass("select")) {
                    that.removeAllSelected();
                    that.addSelected(e);
                }
                else {
                    this.showMailList(e);
                }
                egov.mobile.hideMainMenu();
            }

            this.parent.cache.setLastFolderIndex(that.index);
            var folderId = parseInt(that.model.get("folderId"));
            if (!folderId || folderId > 6) {
                egov.mobile.setNofifyFolder(that.model.get("path"));
            }
            if (!egov.mobile.elements.mainPage.hasClass("showNotify")) {
                egov.commonFn.event.changeTitleForMobile("bmail", that.model.get("pathName"));
            }
        },

        changeTitleApp: function (title) {
            egov.commonFn.event.changeTitleForMobile("bmail", title);
        },

        open: function (e) {
            egov.helper.destroyClickEvent(e);
            var $folder = $(e.currentTarget).closest(".parentFolder");
            $folder.addClass("showChildren");
            this.model.set("isOpen", "1");
            bmail.views.folderlist.changeFolderStructure(this.model);
        },

        close: function (e) {
            egov.helper.destroyClickEvent(e);
            var $folder = $(e.currentTarget).closest(".parentFolder");
            $folder.removeClass("showChildren");
            this.model.set("isOpen", "0");
            bmail.views.folderlist.changeFolderStructure(this.model);
        },

        removeAllSelected: function () {
            $(this.maillistClass).hide();
            this.parent.$(".list-group-item").removeClass("select");
        },

        addSelected: function (e) {
            this.showMailList(e);
            $("#" + this.idForMailList + this.model.get("id")).show();
            if (egov.mobile.notification) {
                egov.mobile.notification.hideNotification();
            }
            this.$el.addClass("select");
        },

        showMailList: function (e) {
            bmail.views.main.showListMail(e);
            this.mailList.$el.show();
        },

        emptyFolder: function () {
            this.model.set("totalUnread", 0);
        },

    });

    function getRandomTrueFalse() {
        return Math.floor(Math.random() * 100) % 2 == 1;
    }

    return BmailViewFolder;
});
