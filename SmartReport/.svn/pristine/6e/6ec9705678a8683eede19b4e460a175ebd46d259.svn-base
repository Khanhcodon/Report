(function () {
    "use strict";

    //#region Private fields

    var messageButtons = {
        Ok: 1,
        YesNo: 2,
        OkCancel: 3
    };

    var messageResult = {
        Ok: 1,
        Yes: 2,
        No: 3,
        Cancel: 4
    };

    var messageTypes = {
        success: 1,
        error: 2,
        warning: 3,
        processing: 4
    };

    //#endregion

    /// <summary>Đối tượng View quản lý thông báo của eGov</summary>
    var eGovMessage = Backbone.View.extend({

        el: '.alert',

        // Khởi tạo
        initialize: function () {
            this.messageButtons = messageButtons;
            this.messageResult = messageResult;
            this.messageTypes = messageTypes;
            this.autoHide = false;
        },

        // Hiển thị thông báo dạng alert
        show: function (message, title, messageButton, okCallBackFunction, cancelCallBackFunction, settings) {
            this.hide();
            require(['dialog'], function () {
                var defaultSettings = _getDialogSetting();
                if (settings) {
                    defaultSettings = $.extend(defaultSettings, settings);
                }

                var dialogObj = _getDialogObj();
                if (typeof message !== 'string') {
                    message = '';
                }
                dialogObj.html(message);

                if (typeof title === 'string' && title !== '') {
                    defaultSettings.title = title;
                }

                defaultSettings.buttons = _getDialogButtons(messageButton, okCallBackFunction, cancelCallBackFunction, dialogObj);
                dialogObj.dialog(defaultSettings);
            });
        },

        prompt: function (title, confirmButtonName, callback, isCheckNullOrEmpty, valueDefault) {
            /// <summary>
            /// Hiển thị Prompt giống như javascript
            /// </summary>
            /// <param name="title">Tiêu đề</param>
            /// <param name="callback">Hàm thực thi sau khi comfirm.</param>
            /// <param name="isCheckNullOrEmpty">Có kiểm tra nếu là null hoặc rỗng thì validate</param>
            /// <param name="valueDefault">Giá trị mặc định khi bật dialog</param>
            var $el = $('<div><textarea class="form-control" row="4" style="height: 80px"></textarea></div>');
            $el.dialog({
                title: title,
                width: '400px',
                height: '120px',
                draggable: true,
                keyboard: true,
                buttons: [
                     {
                         text: confirmButtonName,
                         className: 'btn-primary',
                         click: function () {
                             var value = $el.find("textarea").val();
                             if (isCheckNullOrEmpty && (value === '' || value == null)) {
                                 $el.find("textarea").addClass('input-validation-error').first().focus();
                                 return;
                             }

                             if (typeof callback === 'function') {
                                 callback(value);
                             }

                             $el.dialog('destroy');
                         }
                     }
                    , {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            $el.dialog('destroy');
                        }
                    }
                ]
            });

            var $textarea = $el.find('textarea');
            if (valueDefault) {
                $textarea.focus().val(valueDefault);
            }
            else {
                $textarea.focus();
            }
        },

        promptHasConfirmDisplay: function (options) {
            //title, confirmButtonName, callback,
            //isCheckNullOrEmpty, callbackConfirm,
            //hasAutoComplete, addTemplateButtonName,
            //callbackCloseForm) {
            /// <summary>
            /// Hiển thị Prompt giống như javascript
            /// </summary>
            /// <param name="title">Tiêu đề</param>
            /// <param name="callback">Hàm thực thi sau khi comfirm.</param>
            /// <param name="isCheckNullOrEmpty">Có kiểm tra nếu là null hoặc rỗng thì validate</param>

            var that = this;
            var $el = $('<div><textarea class="form-control" row="5" style="height: 120px"></textarea></div>');
            var textArea = $el.find('textarea');
            $el.dialog({
                title: options.title,
                width: '500px',
                height: '150px',
                draggable: true,
                keyboard: true,
                confirm: {
                    id: "hasDisplay",
                    text: options.confirmTitleName,
                    style: {
                        'float': 'left',
                        'display': 'inline-block',
                        'font-size': '13px',
                        'font-weight': 'normal'
                    },
                    callback: function (value) {
                        if (typeof options.callbackConfirm === 'function') {
                            options.callbackConfirm(value);
                        }
                    }
                },
                buttons: [
                            {
                                text: options.confirmButtonName,
                                className: 'btn-primary',
                                click: function () {
                                    var value = $el.find("textarea").val();
                                    if (options.isCheckNullOrEmpty && (value === '' || value == null)) {
                                        $el.find("textarea").addClass('input-validation-error').first().focus();
                                        return;
                                    }

                                    if (typeof options.callback === 'function') {
                                        options.callback(value);
                                    }

                                    $el.dialog('destroy');
                                }
                            }
                            , {
                                text: egov.resources.common.closeButton,
                                className: 'btn-close',
                                click: function () {
                                    if (typeof options.callbackCloseForm === 'function') {
                                        options.callbackCloseForm();
                                    }
                                    $el.dialog('destroy');
                                }
                            }
                ]
            });

            textArea.focus();
        },

        // Hiển thị thông báo dạng notify
        notification: function (message, messageType, autoHide) {
            if (message === "hide") {
                this.hide();
                return;
            }

            if (autoHide == undefined) {
                autoHide = true;
            }
            if (messageType == undefined) {
                messageType = this.messageTypes.success;
            }
            this.$el.removeClass();
            this.$el.addClass(_getAlertTypeClass(messageType));
            this.$el.html(message);

            if (autoHide) {
                var that = this;
                this.$el.fadeIn();
                setTimeout(function () {
                    that.hide();
                }, 10000);
            }
            else {
                this.$el.fadeIn();
            }
        },

        /// <summary>Hiển thị message đang xử lý</summary>
        processing: function (message, autoHide) {
            this.notification(message, this.messageTypes.processing, autoHide);
        },

        /// <summary>Hiển thị message thông báo lỗi</summary>
        error: function (message, autoHide) {
            this.notification(message, this.messageTypes.error, autoHide);
        },

        /// <summary>Hiển thị message thông báo thành công</summary>
        success: function (message, autoHide) {
            this.notification(message, this.messageTypes.success, autoHide);
        },

        /// <summary>Hiển thị message cảnh báo</summary>
        warning: function (message, autoHide) {
            this.notification(message, this.messageTypes.warning, autoHide);
        },

        hide: function () {
            this.$el.fadeOut('slow');
        }
    });

    var _getAlertTypeClass = function (type) {
        switch (type) {
            case messageTypes.success:
                return "alert alert-success";
            case messageTypes.error:
                return "alert alert-danger";
            case messageTypes.warning:
                return "alert alert-warning";
            case messageTypes.processing:
                return "alert alert-info";
            default:
                return "";
        }
    };

    var _getDialogSetting = function () {
        return {
            height: 'auto',
            width: '600px',
            title: 'Thông báo',
            draggable: true,
            buttons: [],
            close: function () {
                //$(this).remove();
            }
        };
    };

    var _getDialogObj = function () {
        var result = $("<div>");
        result.css("min-width", "200px");
        return result;
    };

    var _getDialogButtons = function (button, okCallBackFunction, cancelCallBackFunction, that) {
        if (typeof button !== 'number') {
            return [];
        }
        var result = [];
        var btnText = {
            Yes: egov.resources.common.messageYesBtn ? egov.resources.common.messageYesBtn : "Yes",
            No: egov.resources.common.messageNoBtn ? egov.resources.common.messageNoBtn : "No",
            Cancel: egov.resources.common.messageCancelBtn ? egov.resources.common.messageCancelBtn : "Cancel",
            Ok: egov.resources.common.messageOkBtn ? egov.resources.common.messageOkBtn : "Ok"
        }
        switch (button) {
            case messageButtons.YesNo:
                result.push({
                    text: btnText.Yes,
                    className: 'btn-primary',
                    click: function () {
                        that.dialog('destroy');
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        return messageResult.Yes;
                    }
                });
                result.push({
                    text: btnText.No,
                    click: function () {
                        that.dialog('destroy');
                        if (typeof cancelCallBackFunction === "function") {
                            cancelCallBackFunction();
                        }
                        return messageResult.No;
                    }
                });

                break;
            case messageButtons.OkCancel:
                result.push({
                    text: btnText.Ok,
                    className: 'btn-primary',
                    click: function () {
                        that.dialog('destroy');
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        return messageResult.OK;
                    }
                });
                result.push({
                    text: btnText.Cancel,
                    click: function () {
                        that.dialog('destroy');
                        if (typeof cancelCallBackFunction === "function") {
                            cancelCallBackFunction();
                        }
                        return messageResult.Cancel;
                    }
                });

                break;
            default:
                result.push({
                    text: btnText.Ok,
                    className: 'btn-primary',
                    click: function () {
                        that.dialog('destroy');
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        return messageResult.OK;
                    }
                });
                break;
        }
        return result;
    };

    window.eGovMessage = eGovMessage;

    return eGovMessage;
})();