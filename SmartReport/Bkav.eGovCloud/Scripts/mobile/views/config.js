define(function () {

    "use strict";

    var MobileConfig = Backbone.View.extend({
        el: 'body',
        setting: {},
        avatarTheme: "troll1", //Chủ đề Avatar (dùng k người dùng chọn thiết lập không tải avatar thật) troll, icon
        isAutoSaveConfig: true, //Tự động lưu thiết lập người dùng
        maxUnreadLabel: 99,
        settingResources: egov.resources.mobile.usersetting,

        startApp: "documents", //Ứng dụng khởi động
        pageSize: 50, //Số mail tải cho 1 lần
        fontFamily: 1, //Font hệ thống
        fontSize: 1, //Cỡ chữ: 0, 1, 2
        notifyType: 1,//0:Không hiện notify, 1: Chỉ hiện notify cuối, 2: Hiện toàn bộ
        language: "Vietnamese", //Ngôn ngữ
        showUserAvatar: true, //Xem avatar của người dùng
        showPageInPopup: false,

        initialize: function () {
            this.initSetting();
            this.userConfigRender();

            egov.mobile.config = this;
        },

        events: {
            "click .btnconfig": "showUserConfig",

            "change #showAvatar": "userAvatar",
            "change .ddlApps": "changeStartApp",
            "change .ddlFontFamily": "changeFontFamily",
            "change .ddlNotifyType": "changeNotifyType",
            "change .ddlFontSize": "changeFontSize",
            "change .ddlPageSize": "changePageSize",
            "change .ddlLanguage": "changeLanguage",
        },

        initSetting: function () {
            egov.mobile.startApp = this.startApp;

            this.setting = egov.setting;
            this.userSetting = this.setting.user.userSetting;

            //this.pageSize = this.userSetting.MPageSize || this.pageSize;
            this.fontFamily = this.userSetting.MFontFamily || this.fontFamily;
            this.notifyType = this.userSetting.MNotifyType || this.notifyType;
            this.fontSize = this.userSetting.MFontSize;
            this.language = this.userSetting.Language;
            this.showUserAvatar = this.userSetting.MUseAvatar;
            this.showPageInPopup = this.userSetting.MUsePopup;

            this.setUserSetting();
        },

        setUserSetting: function () {
            egov.userConfig = {
                startApp: this.startApp,
                pageSize: this.pageSize,
                fontFamily: this.fontFamily,
                notifyType: this.notifyType,
                fontSize: this.fontSize,
                showAvatar: true, // this.showUserAvatar, TienBV set mac dinh
                usePopUp: this.showPageInPopup,
                language: this.language
            };

            if (this.showUserAvatar) {
                egov.mobile.$egov.addClass("showRealAvatar");
            }
        },

        //#endregion

        //#region UserConfig

        userConfigRender: function () {
            /// <summary>
            /// Load lại trang theo setting của người dùng.
            /// </summary>
            //this._changeFontFamily(this.fontFamily);
            this._changeFontSize(this.fontSize);
        },

        render: function () {
            var that = this,
                mobileSetting = {};
            mobileSetting.userConfig = egov.userConfig;

            mobileSetting.apps = [
                { value: appType.documents, text: this.settingResources.appType.documents },
                { value: appType.bmail, text: this.settingResources.appType.bmail },
                { value: appType.chat, text: this.settingResources.appType.chat },
                { value: appType.calendar, text: this.settingResources.appType.calendar },
                { value: appType.contacts, text: this.settingResources.appType.contacts },
            ];
            mobileSetting.fontSizes = [
                { value: "0", text: this.settingResources.fontsizeType.small },
                { value: "1", text: this.settingResources.fontsizeType.normal },
                { value: "2", text: this.settingResources.fontsizeType.large }];

            if (!that.configDialog) {
                require([egov.template.mobile.userConfig], function (Template) {

                    var configDialog = $.tmpl(Template, mobileSetting);

                    var dialogSetting = {
                        title: egov.resources.main.config,
                        keyboard: true
                    };

                    dialogSetting.height = "auto"; // 250px

                    dialogSetting.buttons = [{
                        id: "close",
                        text: egov.resources.common.closeButton,
                        className: 'btn-close',
                        click: function () {
                            configDialog.dialog('hide');
                        }
                    }];
                    if (!that.isAutoSaveConfig) {
                        dialogSetting.buttons.unshift({
                            id: "submit",
                            text: egov.resources.common.confirmButton,
                            className: 'btn-primary',
                            click: function () {
                                that.saveUserSetting(function (data) {
                                    if (data.result == "success") {
                                        egov.pubsub.publish(egov.events.status.success, egov.resources.userConfig.saveSuccess);
                                    }
                                    else {
                                        egov.pubsub.publish(egov.events.status.error, egov.resources.userConfig.saveError);
                                    }
                                    configDialog.dialog('hide');
                                });
                            }
                        });
                    }
                    configDialog.dialogSetting = dialogSetting;
                    configDialog.dialog(dialogSetting);
                    configDialog.parent().parent().css("top", "150px");
                    that.configDialog = configDialog;

                    componentHandler.upgradeDom();
                })
            }
            else {
                that.configDialog.dialog("show");
            }
        },

        showUserConfig: function (e) {
            egov.helper.destroyClickEvent(e);
            this.render();
            egov.mobile.hidePanel();
        },

        userAvatar: function (e) {
            egov.helper.destroyClickEvent(e);
            //$(e.currentTarget).toggleClass("checked");
            //this.showUserAvatar = $(e.currentTarget).hasClass("checked");
            this.showUserAvatar = $(e.currentTarget).is(":checked");

            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        changeStartApp: function (e) {
            egov.helper.destroyClickEvent(e);
            this.startApp = $(e.currentTarget).find("option:selected").val();
            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        changeFontFamily: function (e) {
            egov.helper.destroyClickEvent(e);
            this.fontFamily = $(e.currentTarget).find("option:selected").val();
            //this._changeFontFamily(this.fontFamily);
            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        changeNotifyType: function (e) {
            egov.helper.destroyClickEvent(e);
            this.notifyType = $(e.currentTarget).find("option:selected").val();
            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        changeFontSize: function (e) {
            egov.helper.destroyClickEvent(e);
            this.fontSize = $(e.currentTarget).find("option:selected").val();
            this._changeFontSize(this.fontSize);
            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        changePageSize: function (e) {
            egov.helper.destroyClickEvent(e);
            this.pageSize = $(e.currentTarget).find("option:selected").val();
            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        changeLanguage: function (e) {
            egov.helper.destroyClickEvent(e);
            this.language = $(e.currentTarget).find("option:selected").val();
            if (this.isAutoSaveConfig) {
                this.autoSaveSetting();
            }
        },

        autoSaveSetting: function () {
            this.saveUserSetting(function (data) {
                if (data.result != "success") {
                    console.log(data);
                }
            })
        },

        saveUserSetting: function (callback) {
            /// <summary>
            /// Gửi thông tin thiết lập lên Server
            /// </summary>
            /// <param name="callback"></param>
            var that = this;
            var data = {
                startApp: this.startApp,
                pageSize: this.pageSize,
                fontFamily: this.fontFamily,
                notifyType: this.notifyType,
                fontSize: this.fontSize,
                showAvatar: true, // this.showUserAvatar,
                usePopup: this.showPageInPopup,
                language: this.language
            };

            egov.request.setMobileUserConfig({
                data: data,
                success: function (result) {
                    that.setUserSetting();
                    egov.callback(callback(result));
                },
                error: function (error) {
                    egov.callback(callback({ result: "error", info: error }));
                }
            });
        },
        
        _changeFontSize: function (size) {
            switch (parseInt(size)) {
                case 0:
                    $("body").attr("font-size", "small");
                    break;
                case 2:
                    $("body").attr("font-size", "large");
                    break;
                default:
                    $("body").attr("font-size", "");
                    break;
            }
        },

        //#endregion
    });

    return new MobileConfig();
});
