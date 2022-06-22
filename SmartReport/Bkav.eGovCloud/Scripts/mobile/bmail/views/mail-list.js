define(function () {

    "use strict";

    var BmailViewMailList = Backbone.View.extend({

        folderId: "",
        folderPath: "",
        el: 'mailList',

        className: "mail-list mdl-list ",

        idName: "mail-list-",

        searchClass: "search-list-item",

        isLoading: false,

        isSwipeDown: false,

        loadCount: 0,

        events: {
            //"swipedown": "swipeRefresh",
            "swipeup": "swipeUp",
            "scroll": "loadMoreEvent",
            "click #btnHideSearchResult": "hideSearchResult"
        },

        initialize: function (options) {
            this.folderNode = options.folder;
            this.folderId = options.folderId;
            this.folderPath = options.folderPath;

            bmail.views.maillist = this;
            this.$el.empty();
            
            this._registerGlobalApi();
        },

        _registerGlobalApi: function () {
            egov.pubsub.subscribe("maillist.refresh", this.folderRefresh.bind(this));

            egov.pubsub.subscribe("maillist.delete", this.deleteMail.bind(this));
        },

        initPropForModel: function () {
            var that = this;
            this.model.on("remove", function (mail) {
                if (!egov.mobile.isTablet) {
                    egov.commonFn.event.showNavbar();
                    bmail.current.folder.showMailList();
                }
            })
        },

        render: function (mails, callback) {
            bmail.current.maillist = this;
            var that = this;
            if (mails && mails.length > 0) {
                that.$el.empty();
                require([bmail.templates.mailList], function (Template) {
                    bmail.templates.store.maillist = Template;
                    that.model = new bmail.models.mailList(that.renderData(mails));
                    that.initPropForModel();
                    egov.callback(callback);
                });

            } else {
                require([bmail.templates.mailListNoData], function (Template) {
                    bmail.templates.store.maillistnodata = Template;
                    var message = "Bạn không có thư nào trong mục này.";                  
                    that.$el.html($.tmpl(Template, {
                        message: message
                    }));
                    that.isLoading = false;
                    egov.callback(callback);
                });
            }
        },

        reRender: function (mails) {
            this.$el.empty();
            this.render(mails);
        },

        renderData: function (mails, isLoadMore) {
            var that = this;
            var listModel = [];
            console.log(mails);
            if (mails instanceof Array) {
                $.each(mails, function (idx) {
                    var mail = new MailInList({
                        parent: that,
                        model: this,
                        id: that.idName + this.id,
                        mailId: this.id
                    });
                    listModel[idx] = mail.model;
                    if (!isLoadMore && idx === 0 && egov.mobile.isTablet) {
                        // mail.select();
                    }

                    that.$el.append(mail.$el);
                });

            }
            else {
                egov.pubsub.publish(egov.events.status.error, mails);
            }

            that.isLoading = false;
            return listModel;
        },

        swipeUp: function () {
            this.isSwipeDown = false;
        },

        swipeRefresh: function (e) {
            this.isSwipeDown = true;
            egov.helper.destroyClickEvent(e);
            if ($(e.currentTarget).scrollTop() == 0) {
                this.refresh(e);
            }
        },

        folderRefresh: function () {
            egov.mobile.hideDetailPage();
            this.refresh();
        },

        refresh: function (e) {
            egov.helper.destroyClickEvent(e);
            var that = this;
            if (bmail.views.maillist.isShowingMultiSelectionBar) {
                return;
            }
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

            bmail.makeRequest.getFolderMailList(0, this.folderPath, firstLoadMailNumber, that.folderNode.isSentBox,
                function (data) {
                that.$el.empty();
                that.hasMore = data.more;
                that.render(data.mails);
                egov.pubsub.publish(egov.events.status.destroy);
            });
        },

        loadMore: function (callback) {
            var that = this;
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
            bmail.makeRequest.getFolderMailList(this.model.length + 1, this.folderPath, firstLoadMailNumber, that.folderNode.isSentBox,
                function (data) {
                that.hasMore = data.more;
                that.model.add(that.renderData(data.mails, true));
                egov.callback(callback);
                egov.pubsub.publish(egov.events.status.destroy);
            });
        },

        loadMoreEvent: function (e) {
            if (window.innerHeight + document.body.scrollTop + 300 > document.body.scrollHeight && !this.isLoading && !this.isSwipeDown) {
                this.loadMore();
            }
        },

        hideSearchResult: function (e) {
            egov.helper.destroyClickEvent(e);
            if (!egov.mobile.isTablet) {
                egov.mobile.removeSearch();
            }
            this.$el.remove();
            bmail.current.folder.select();
        },

        markAsRead: function (listMail) {
            /// <summary>
            /// Đánh dấu là đã đọc
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("read", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    var toltalUnread = bmail.current.folder.model.get("totalUnread");
                    if (toltalUnread > 0) {
                        bmail.current.folder.model.set("totalUnread", toltalUnread - 1);
                    }
                    that.model.get(mailId).set("unread", false)
                })
            });
        },

        markUnRead: function (listMail) {
            /// <summary>
            /// Đánh dấu là chưa đọc
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("!read", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    bmail.current.folder.model.set("totalUnread", bmail.current.folder.model.get("totalUnread") + 1);
                    that.model.get(mailId).set("unread", true);
                })
            });
        },

        markSpam: function (listMail) {
            /// <summary>
            /// Đánh dấu là thư rác
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("spam", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    that.model.remove(that.model.get(mailId));
                })
            });
        },

        unMarkSpam: function (listMail) {
            /// <summary>
            /// Bỏ đánh dấu là thư rác
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("!spam", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    that.model.remove(that.model.get(mailId));
                })
            });
        },

        deleteMail: function (listMail) {
            /// <summary>
            /// Xóa mail (cho vào thùng rác)
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("trash", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    that.model.remove(that.model.get(mailId));
                })
            });
        },

        deleteMailPer: function (listMail) {
            /// <summary>
            /// Xóa mail vĩnh viễn
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("delete", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    that.model.remove(that.model.get(mailId));
                })
            });
        },

        restore: function (listMail) {
            /// <summary>
            /// Khôi phục mail đã xóa
            /// </summary>
            /// <param name="listMail">list Id của mail: Dạng id, id, id, ...</param>
            var that = this;
            bmail.makeRequest.msgAction("!trash", listMail, function () {
                var mailsId = listMail.split(",");
                _.each(mailsId, function (mailId) {
                    that.model.remove(that.model.get(mailId));
                })
            });
        },

        removeAllSelected: function () {
            _.each(this.model.models, function (model) {
                if (model.get("isSelected")) {
                    model.set("isSelected", false);
                }
            })
        },

    });

    var MailInList = Backbone.View.extend({

        mailId: "",

        tagName: "li",

        className: "mdl-list__item mdl-list__item--two-line",

        mailDetailClass: ".mail-detail",

        mailDetailId: "mailDetail_",

        $detailWrap: $("#mail-detail"),

        numberSelected: 0,

        events: {
            "click": "select",
            "taphold": "multiSelection"
        },

        initialize: function (options) {
            this.parent = options.parent;
            this.isSentBox = this.parent.folderNode.isSentBox;
            this.mailId = options.mailId;
            this.initData(options.model);
            this.setEventOfModel();
            this.render();
        },

        initData: function (mail) {
            var that = this;
            var email = {
                a: "unknown",
                d: "unknown",
                p: "unknown",
            }, trimId, unread = false;

            if (mail.e) {
                var filter = 'f';
                if (this.isSentBox) {
                    filter = 't'
                }
                email = mail.e.filter(function (obj) {
                    return obj.t == filter;
                })[0];
            }

            if (mail.f && mail.f == "u") {
                unread = true;
            }

            trimId = mail.id.replace(/:/gi, "");
            var userName = email.a ? (email.a.indexOf("@") == -1 ? email.a : email.a.substr(0, email.a.indexOf("@"))) : "?";
            var user = GetUser(userName);
            that.model = new bmail.models.mail({
                d: mail.d,
                date: egov.commonFn.util.getCommonTime(mail.d),
                detailDate: new Date(mail.d).format("HH:mm dd/MM/yyyy"),
                location: mail.l,
                conversationId: mail.cid,
                id: mail.id,
                trimId: trimId,
                domId: "maillist_" + trimId,
                mailDetailId: that.mailDetailId + trimId,
                sender: {
                    fulladdress: email.a,
                    address: userName,
                    name: user == null ? userName : user.fullname,
                    fullname: user == null ? email.a : user.fullname
                },
                hasAttach: mail.f == undefined ? false : mail.f.indexOf("a") != -1,
                subject: Util.getSubject(mail.su),
                unread: unread,
                isSelected: false
            });

            that.model.set("avatar", user == null ? "../AvatarProfile/noavatar.jpg" : user.avatar);
            that.model.set("alphabet", that.model.get("sender").address[0]);
            that.model.set("alphabetCode", getColorCode(that.model.get("sender").address[0]));
        },

        setEventOfModel: function () {
            var that = this;

            this.model.on("remove", function (model) {
                that.$el.remove();
            });

            this.model.on("change:unread", function (model, unread) {
                that.$el.toggleClass("unread");
            });

            this.model.on("change:isSelected", function (model, isSelected) {
                if (isSelected) {
                    that.$el.addClass("rowSelected");
                }
                else {
                    that.$el.removeClass("rowSelected");
                }
                if (bmail.views.maillist.isShowingMultiSelectionBar) {
                    var isSelect;
                    that.$el.toggleClass("showMultiselectCheckBox");
                    if (that.$el.hasClass("showMultiselectCheckBox")) {
                        isSelect = true;
                        that.$el.find('.checkbox').addClass('checked');
                    }
                    else {
                        isSelect = false;
                        that.$el.find('.checkbox').removeClass('checked');
                    }
                    var selectedLength = that.parent.$el.find(".showMultiselectCheckBox").length;
                    if (that.parent.multiselectionbar) {
                        if (selectedLength == 0) {
                            that.parent.multiselectionbar.hideMultiSelectionBar();
                        }
                        that.parent.multiselectionbar.add(isSelect, model, selectedLength + "/" + that.parent.model.length);
                    }
                }
            });

        },

        render: function () {
            var that = this;
            var $mailItem = $.tmpl(bmail.templates.store.maillist, this.model.toJSON());
            this.$el.append($mailItem);
            if (this.model.get("unread")) {
                this.$el.addClass("unread");
            }
        },

        select: function (e) {
            egov.helper.destroyClickEvent(e);
            var that = this;

            if (bmail.views.maillist.isShowingMultiSelectionBar) {
                if (this.parent.multiselectionbar) {
                    this.parent.multiselectionbar.hideDropDown();
                }
                this.toggleSelected();
                return;
            }

            if (egov.mobile.showingSearchBox && !egov.mobile.isTablet) {
                egov.mobile.hideSearchBox();

                return;
            }

            if (egov.mobile.isTablet) {
                $(".dataDetail").removeClass("display");
            }

            if (!this.model.hasBind) {
                require(["bmailViewDetail"], function (MailDetail) {
                    var mailDetail = new MailDetail({
                        mailItem: that,
                        model: that.model,
                        folderId: that.folderId,
                        id: that.model.get("mailDetailId"),
                    });

                    that.$detailWrap.empty();
                    that.$detailWrap.append(mailDetail.$el);
                    egov.mobile.showDetailPage();
                    that.removeAllSelected();
                    that.markAsRead();
                    // that.model.hasBind = true;
                });
            }
            else {
                that.removeAllSelected();
                that.addSelected();
                egov.mobile.showDetailPage();
            }
            bmail.current.mail = this;
        },

        multiSelection: function (e) {
            var that = this;
            egov.helper.destroyClickEvent(e);
            this.removeAllSelected();
            if (!bmail.views.maillist.isShowingMultiSelectionBar) {
                if (!bmail.views.multiSelectionBar) {
                    require(["bmailViewMultiSelectionBar"], function (MultiSelectionBar) {
                        that.parent.multiselectionbar = new MultiSelectionBar({
                            mailList: that.parent
                        });
                    });
                }
                else {
                    that.parent.multiselectionbar.showMultiSelectionBar();
                }
                bmail.views.maillist.isShowingMultiSelectionBar = true;
            }
            //this.toggleSelected();
        },

        removeAllSelected: function () {
            $(this.mailDetailClass).hide();
            this.parent.removeAllSelected();
        },

        addSelected: function () {
            $("#" + this.model.get("mailDetailId")).show();
            this.model.set("isSelected", true);
        },

        toggleSelected: function () {
            if (this.model.get("isSelected")) {
                this.model.set("isSelected", false);
            }
            else {
                this.model.set("isSelected", true);
            }
        },

        markAsRead: function () {
            /// <summary>
            /// Đánh dấu là đã đọc
            /// </summary>
            if (this.model.get("unread")) {
                this.parent.markAsRead(this.mailId);
                //this.model.set("unread", true);
            }
        },

        markUnRead: function () {
            /// <summary>
            /// Đánh dấu là chưa đọc
            /// </summary>
            if (!this.model.get("unread")) {
                this.parent.markUnRead(this.mailId);
                this.model.set("unread", false);
            }
        },

        markSpam: function () {
            /// <summary>
            /// Đánh dấu là thư rác
            /// </summary>
            this.parent.folderRefresh();
            this.parent.markSpam(this.mailId);
        },

        unMarkSpam: function () {
            /// <summary>
            /// Bỏ đánh dấu là thư rác
            /// </summary>
            this.parent.folderRefresh();
            this.parent.unMarkSpam(this.mailId);
        },

        deleteMail: function () {
            /// <summary>
            /// Bỏ đánh dấu là thư rác
            /// </summary>
            this.parent.folderRefresh();
            this.parent.deleteMail(this.mailId);
        },

        deleteMailPer: function () {
            /// <summary>
            /// Bỏ đánh dấu là thư rác
            /// </summary>
            this.parent.folderRefresh();
            this.parent.deleteMailPer(this.mailId);
        },

        restore: function () {
            /// <summary>
            /// Khôi phục mail đã xóa
            /// </summary>
            this.parent.restore(this.mailId);
        }

    });

    function GetUser(userName) {
        var user = _.find(egov.setting.allUsers, function (u) {
            return u.username === userName;
        });

        return user;
    }

    return BmailViewMailList;
});
