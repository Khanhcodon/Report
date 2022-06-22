(function ($, _, Backbone, desktopNotify, helper) {
    "use strict";
    if (typeof $ === undefined) {
        throw "Chưa có thư viện jquery.";
    }

    if (typeof _ === undefined) {
        throw "Chưa có thư viện underscore.";
    }

    if (typeof Backbone === undefined) {
        throw "Chưa có thư viện backbone.";
    }

    // Kiểm tra thư viện desktop notify.
    if (typeof desktopNotify === undefined) {
        throw "Chưa có thư viện desktop notify.";
    }

    if (typeof helper === undefined) {
        throw "Chưa có thư viện helper.";
    }

    var types = {
        eGov: 0,     // Notify bên xử lý văn bản
        bmail: 1,    // Notify bên bmail
        chat: 2,     // Notify của bên chat
        calendar: 3, // Notify bên lịch
        eTask: 4     // Notify bên eTask
    };

    ///Đối tượng notify model
    var notifyModel = Backbone.Model.extend({
        defaults: {
            title: "",
            content: "",
            date: "",
            avatar: "",
            userName: "",
            fullName: "",
            type: 0,

            //egov
            docCopyId: 0,

            //mail
            mailId: 0,
            folderId: 0,

            //chat
            chatterJid: "",
            messageId: ""
        }
    });

    ///Danh sách đối tượng notify model
    var notifyList = Backbone.Collection.extend({
        model: notifyModel
    });

    var notifyView = Backbone.View.extend({
        events: {
            'click .ripple-ef': '_setNumberViewEmpty',
            'click .notify-command .notify-openAll': 'openAll',
            'click .notify-command .notify-closeAll': 'closeAll'
        },

        hasAlertNotify: false,//Check có hiển thị notify trên khung dưới màn hình hay không

        initialize: function (options) {
            this.el = options.el;
            this.type = options.type || types.eGov;
            this.callback = options.callback;
            this.model = new notifyList;

            if (this.model.length <= 0) {
                this.$('.notify-empty, .notify-command').remove();
                var htmlEmpty = '<li class="notify-empty"><b>' + this._getMessageNotifyEmpty() + '<b></li>';
                this.$('.notification').empty().append(htmlEmpty);
            }

            this._eventProps();

            if (typeof this.callback === 'function') {
                this.callback();
            }

            return this;
        },

        _eventProps: function () {
            var that = this;

            this.model.on('add', function (newNotify) {
                var view = new notifyItemView({
                    model: newNotify,
                    parent: that
                });

                that.$('.notify-empty, .notify-command').remove();
                that.$('.notification').prepend(view.$el);

                if (!navigator.isMobile) {
                    var openAllHtml = '<div class="notify-command"><a href="#" class="notify-openAll"><b>'
                        + egov.resources.notify.openall + '</b></a><a href="#" class="notify-closeAll"><b>'
                        + egov.resources.notify.closeall + '</b></a></div>';
                    that.$('.dropdown-menu').prepend(openAllHtml);
                }

                that._showNumberNotify();

                if (that.hasAlertNotify) {
                    that.alertNotify(newNotify);
                }
            });

            this.model.on("remove", function (model) {
                if (that.model.length <= 0) {
                    that.$('.notify-empty, .notify-command').remove();
                    var htmlEmpty = '<li class="notify-empty"><b>' + that._getMessageNotifyEmpty() + '</b></li>';
                    that.$('.notification').empty().append(htmlEmpty);
                }

                model.view.remove();

                that._showNumberNotify();
            });
        },

        _showNumberNotify: function () {
            if (this.model && this.model.length > 0) {
                var numberNotify = this.model.select(function (item) {
                    return item.get('hasInBell');
                }).length;

                if (numberNotify > 0) {
                    this.$('.notify-count').text(numberNotify > 99 ? '99+' : numberNotify)
                        .addClass('notify-count-avaiable').attr('data-count', numberNotify);
                } else {
                    this.$('.notify-count').text('').attr('data-count', 0);
                }

            } else {
                this.$('.notify-count').text('').attr('data-count', 0);
            }
        },

        _setNumberViewEmpty: function () {
            if (this.model && this.model.length > 0) {
                this._showNumberNotify();
            }
        },

        addToModel: function (model) {
            if (!model) {
                return;
            }

            this.model.add(model);
        },

        checkExist: function (model) {
            if (!model) {
                throw egov.resources.notify.valuemodelnotnull;
            }

            if (!(model instanceof notifyModel)) {
                model = new notifyModel(model);
            }

            var propName = this._getPropKey();
            var exist;

            if (this.model && this.model.length > 0) {
                exist = this.model.select(function (item) {
                    return item.get(propName) == model.get(propName);
                });
            }

            if (!exist || exist.length === 0) {
                return false;
            }

            return true;
        },

        removeModel: function (model) {
            if (!model) {
                return;
            }

            this.model.remove(model);
        },

        removeModelById: function (id, hasRemoveServer) {
            if (!this.model || this.model.length <= 0) {
                return;
            }

            var propName = this._getPropKey();
            var model = this.model.select(function (item) {
                return item.get(propName) == id;
            });

            if (!model)
                return;

            this.removeModel(model);
            if (hasRemoveServer) {
                removeNotify(id);
            }
        },

        setViewAll: function () {
            if (this.model && this.model.length > 0) {
                this.model.each(function (item) {
                    if (item.get('hasInBell')) {
                        item.set('hasInBell', false);
                    }
                });

                this._showNumberNotify();
            }
        },

        alertNotify: function (model) {
            ///<summary>
            /// Tạo notify trên góc màn hình
            ///</summary>
            ///<param name="data">Đối tượng để tạo notify</param>
            if (model === undefined) {
                return;
            }

            helper.playAudio();
            var that = this;
            try {
                var id, title, body, icon, onclick, onclick_param;

                if (this.type == types.eGov) {
                    id = model.get('docCopyId');
                    title = model.get('fullName') + " - " + model.get('userName');
                    body = model.get('content');
                    icon = model.get('avatar')
                    onclick = "openDocumentAlertInNotifyeGovDesktop",
                    onclick_param = JSON.stringify({
                        id: id
                    });
                }
                else if (this.type == types.bmail) {
                    id = model.get('mailId');
                    title = model.get('userName');
                    body = model.get('content');
                    icon = model.get('avatar');
                    onclick = "openMailAlertInNotifyeGovDesktop",
                    onclick_param = JSON.stringify({
                        id: id
                    });
                } else if (this.type == types.chat) {
                    id = model.get('chatterJid');
                    title = model.get('userName');
                    body = model.get('content');
                    icon = model.get('avatar');
                    onclick = "openChatAlertInNotifyeGovDesktop",
                    onclick_param = JSON.stringify({
                        id: id
                    });
                }

                //kiểm tra trên bản desktop
                if (window.Notification
                     && (window.external
                     && typeof window.external.createNotification === 'function')) {
                    if (this.type == types.bmail)
                        return;

                    var objectData = [{
                        title: title,
                        body: body,
                        icon: icon,
                        onclick: onclick,
                        onclick_praram: onclick_param
                    }];

                    window.external.createNotification(JSON.stringify(objectData));
                }
                else {
                    //kiểm tra trình duyệt có hỗ trợ và người dùng có bật chức năng thông báo của trình duyệt lên hay không
                    if (desktopNotify.hasNotify()) {
                        desktopNotify.requestPermission();
                        if (!this.arrAlertNotify) {
                            this.arrAlertNotify = {};
                        }

                        if (this.type == types.chat) {
                            if (this.arrAlertNotify["egovchat_cuongnt"]) {
                                this.arrAlertNotify["egovchat_cuongnt"].close();
                                delete this.arrAlertNotify["egovchat_cuongnt"];
                            }
                            this.arrAlertNotify["egovchat_cuongnt"] = desktopNotify.createNotification({
                                title: title,
                                body: body,
                                icon: icon,
                                click: function () {
                                    that.arrAlertNotify["egovchat_cuongnt"].close();
                                    that.openNotifyById(id);
                                }
                            });
                        } else {
                            this.arrAlertNotify[id] = desktopNotify.createNotification({
                                title: title,
                                body: body,
                                icon: icon,
                                click: function () {
                                    that.openNotifyById(id);
                                }
                            });
                        }
                    }
                }
            }
            catch (ex) {
                console.log(ex.message);
            }
        },

        rebindAlertNotify: function () {
            if (window.Notification
               && (window.external
               && typeof window.external.createNotification === 'function')) {
                return;
            }

            if (this.model && this.model.length > 0) {
                this.alertNotify(this.model.at(0));
            }
        },

        openNotifyById: function (id) {
            ///<summary>
            /// Mở notify theo id
            ///</summary>
            ///<param name="id"> <param>
            if (this.model && this.model.length > 0) {
                var propName = this._getPropKey();
                var models = this.model.select(function (item) {
                    return item.get(propName) == id;
                });

                if (models && models.length > 0) {
                    for (var i = 0; i < models.length; i++) {
                        var model = models[i];
                        var value = model.get(propName);
                        model.view.open();

                        if ((this.arrAlertNotify && value) && this.arrAlertNotify[value]) {
                            this.arrAlertNotify[value].close();
                            delete this.arrAlertNotify[value];
                        }
                    }
                }
            }
        },

        openAlertInNotifyeGovDesktop: function (data) {
            ///<summary>
            ///  Mở notify trên bản desktop
            ///</summary>
            var obj = JSON.parse(data);
            this.openNotifyById(obj.id);
        },

        setAlert: function (value) {
            ///<summary>
            /// Thiết lập có hiển thị notify ở khung màn hình hay không
            ///</summary>
            ///<param name="value" type="bool">Giá trị cần thiết lập co hiển thị 1 thống báo ở khung màn hình hay không</param>
            this.hasAlertNotify = value;
        },

        _getPropKey: function () {
            ///</ummary>
            /// Lấy thuộc tính chính theo từng loại notify
            ///</summary>
            var propKeyname = null;

            switch (this.type) {
                case types.eGov:
                    propKeyname = "docCopyId";
                    break;
                case types.bmail:
                    propKeyname = "mailId";
                    break;
                case types.eTask:

                    break;
                case types.chat:
                    propKeyname = "chatterJid";
                    break;

                default:
                    break;
            }

            return propKeyname;
        },

        _getMessageNotifyEmpty: function () {
            var message = null;

            switch (this.type) {
                case types.eGov:
                    // message = egov.resources.emptyDocumentNotifications;
                    message = egov.resources.notify.nodocumentnotify;
                    break;

                case types.bmail:
                    // message = egov.resources.emptyMailNotifications;
                    message = egov.resources.notify.nomailnotify;
                    break;

                case types.eTask:

                    break;

                case types.chat:
                    // message = egov.resources.emptyChatNotifications;
                    message = egov.resources.notify.nochatnotify;
                    break;

                default:
                    break;
            }

            return message;
        },

        _getStringOpenAll: function () {
            var str = null;

            switch (this.type) {
                case types.eGov:
                    str = egov.resources.openAllDocument;
                    // str = "Mở tất cả văn bản được thông báo";
                    break;

                case types.bmail:
                    str = egov.resources.openAllMail;
                    // str = "Mở tất cả mail nhận được";
                    break;

                case types.eTask:
                    break;

                case types.chat:
                    str = egov.resources.openAllChat;
                    // str = "Mở tất cả tin nhắn nhận được";
                    break;

                default:
                    break;
            }

            return str;
        },

        openAll: function () {
            if (!this.model || this.model.length <= 0) {
                return;
            }

            this.model.at(0).view.open(this.model.length === 1);
            this.openAll();
        },

        closeAll: function () {
            if (!this.model || this.model.length <= 0) {
                return;
            }

            this.model.at(0).view.remove(true);
            this.closeAll();
        }
    });

    var notifyItemView = Backbone.View.extend({
        tagName: 'li',
        events: {
            'click .open-notify': 'open',
            'click .close-notify': 'close'
        },

        initialize: function (option) {
            this.parent = option.parent;
            this.render();
            this.model.view = this;

            return this;
        },

        render: function () {
            this.$el.html(this._parseHtml(this.model));
        },

        open: function () {
            ///<summary>
            /// Mở văn bản hoặc mail
            ///</summary> 
            window.focus();
            var type = this.model.get("type");
            this.remove();
            switch (type) {
                case types.eGov:
                    //Mở văn bản
                    removeNotify(this.model.get("docCopyId"));
                    this._openDocument(true);
                    break;

                case types.bmail:
                    //Mở mail
                    this._openMail();
                    break;

                case types.calendar:
                    break;

                case types.chat:
                    this._openChat();
                    break;

                case types.eTask:
                    break;

                default:
                    console.log("Notification type not exist!.");
                    break;
            }
        },

        close: function (e) {
            helper.destroyEvent(e);
            this.remove(true);
            helper.destroyEvent(e);
        },

        _parseHtml: function (model) {
            ///<summary>
            /// Giao diện cuả từng model
            ///</summary>  
            if (!model)
                return null;

            if (model instanceof notifyModel) {
                model = model.toJSON();
            }

            model.defaultAvatar = "'../../AvatarProfile/noavatar.jpg'";
            return $.tmpl(templateInBell, model);
        },

        _openDocument: function (hasLoadContent) {
            ///<summary>
            /// Mở văn bản
            ///</summary>
            var that = this
                , documentsFrame = "documents"
                , docCopyId = this.model.get("docCopyId");

            if (hasLoadContent == null || hasLoadContent === undefined) {
                hasLoadContent = true;
            }

            helper.displayApp(documentsFrame, function () {
                try {
                    var content = helper.getContentWindow(documentsFrame);
                    var egov = content.egov;
                    //TrinhNVd - 5/5/15: Mở văn bản Notify trên Tablet
                    if (!navigator.isMobile) {
                        egov.views.home.tab.openDocument(docCopyId, that.model.get('title'), hasLoadContent);
                    }
                    else {
                        if (egov.views && egov.views.home && egov.views.home.tab) {
                            egov.views.home.tab.removeDocument();
                        }

                        egov.require(['tabView'], function (TabView) {
                            var tabDocument = new TabView({
                                model: {
                                    id: docCopyId,
                                    name: that.model.get('title'),
                                    title: that.model.get('title'),
                                    href: 'tabDocument_' + docCopyId
                                }
                            });
                        });
                    }
                }
                catch (ex) {
                    var data = {
                        DocumentCopyId: docCopyId,
                        title: that.model.get('title')
                    };

                    $.cookie('documentNotify', JSON.stringify(data),
                        {
                            expires: 1
                            , domain: window.document.domain
                            , path: "/"
                            , secure: true
                        }
                    );
                }
            });
        },

        _openMail: function () {
            ///<summary>
            /// Mở mail
            ///</summary>
            var that = this;
            helper.displayApp("bmail", function () {
                var content = helper.getContentWindow("bmail");
                content.readNewMail(that.model.get("mailId"), that.model.get("folderId"));
            });
        },

        _openChat: function () {
            ///<summary>
            /// Mở chat
            ///</summary>
            var that = this;
            helper.displayApp("chat", function () {
                var content = helper.getContentWindow("chat");
                content.btalk.APPVIEW.notificationClick({ chatterJid: that.model.get("chatterJid") });
            });
        },

        _removeView: function () {
            ///<summary>
            /// Xóa trên giao diện và trong model
            ///</summary>
            this.$el.remove();
            this.parent.model.remove(this.model);
        },

        _closeAlertNotify: function () {
            if (this.parent && this.parent.arrAlertNotify) {
                var parent = this.parent;
                var propKey = parent._getPropKey();
                if (parent.arrAlertNotify[this.model.get(propKey)]) {
                    parent.arrAlertNotify[this.model.get(propKey)].close();
                    delete parent.arrAlertNotify[this.model.get(propKey)];
                }
            }
        },

        remove: function (hasRemoveServer) {
            this._removeView();
            this._closeAlertNotify();
            if (hasRemoveServer) {
                if (this.model.get("type") == types.eGov) {
                    removeNotify(this.model.get("docCopyId"));
                }
            }
        }
    });

    function removeNotify(docCopyId, callback) {
        ///<summary>
        /// Xóa các notify liên quan đến văn bản
        ///</summary>
        ///<param name="docCopyId"> id của văn bản muốn xóa notify</param>
        ///<param name ="callback">Hàm gọi lại khi post ajax thành công</param>
        $.post("/Home/RemoveNotify", { docCopyId: docCopyId }, function () {
            if (typeof callback === 'function') {
                callback();
            }
        });
    }

    function setNumberViewEmpty(callback) {
        $.post("/Home/SetAllNotificationViewed").done(function () {
            if (typeof callback === 'function') {
                callback();
            }
        });
    }

    //#region các template của notify

    var templateInBell = '<a href="#" class="open-notify" title="${content}">'
                     + '    <div class="col-xs-2 col-md-2 col-sm-3">'
                     + '         <img class="img-circle" src="${avatar}" onerror="this.src=${defaultAvatar}">'
                     + '    </div>'
                     + '    <div class="col-xs-14 col-md-14 col-sm-13 textalert">'
                     + '         <div class="col-xs-15 col-md-15 col-sm-15" style="padding:0px">'
                     + '            <div class="notify-usersend wraptext">{{if fullName != null}} ${fullName}-{{/if}} ${userName}</div>'
                     + '            <div class="notify-content wraptext"> ${content}</div>'
                     + '            <div class="notify-date">${date}</div>'
                     + '         </div>'
                     + '       <div class="close-notify col-xs-1 col-md-1 col-sm-1">X</div>'
                     + '    </div>'
                     + '</a>';

    var templateAlert = '<div class="notification-alert">'
                 + '  <div class="alert-opacity"></div>'
                 + '   <img src="${avatar}" alt="" class="alert-avatar">'
                 + '   <div class="alert-container">'
                 + '     <div>'
                 + '        <div class="alert-contain"> </div>'
                 + '        <div class="alert-username wraptext">${title}</div>'
                 + '     </div>'
                 + '     <a data-doccopyid="${docId}" data-title="${title2}" data-id="${id}" class="open-notify alert-content">'
                 + '      <div> <b>${date} : </b>${content}</div>'
                 + '     </a>'
                 + '  </div>'
                 + '  <img src="@Url.Content("~/Content/Images/notification_close.png")" class="alert-close" title="Đóng lại" alt="close" />'
                 + ' </div>';

    var templateAlertList = '<div>'
                        + '  <span class="show-list-notify"> << </span>'
                        + '  <ul class="list-notify">'
                        + '     {{each results}}'
                        + '       <li title=" ${content}">'
                        + '         <a class="wraptext open-notify" href="#" data-doccopyid="${docId}" data-title="${title2}" data-id="${id}" title="${content}">'
                        + '            <img src="${avatar}" alt="">'
                        + '            <span class="alert-content">'
                        + '               <b>${date} : </b>&nbsp; ${content} - ${fullName}'
                        + '             </span>'
                        + '         </a>'
                        + '       </li>'
                        + '    {{/each}}'
                        + '    <li><a class="openAll wraptext"><span>Mở tất cả văn</span></a></li>'
                        + '    <li><a class="closeAll wraptext"><span data-res="egov.resources.closeAll">Đóng tất cả</span><span class="count-notify"></span></a></li>'
                        + '   </ul>'
                        + '</div>';

    // #endregion

    //#region khởi tạo notify

    //Notify bên egov
    window.eGovNotify = new notifyView({
        el: ".div-eGov-notify",
        type: types.eGov
    });

    //Notify bên mail
    window.bmailNotify = new notifyView({
        el: ".div-bmail-notify",
        type: types.bmail
    });

    //Notify bên mail
    window.chatNotify = new notifyView({
        el: ".div-chat-notify",
        type: types.chat
    });

    //#endregion 

})(window.jQuery, window._, window.Backbone, window.notify, window.helper);