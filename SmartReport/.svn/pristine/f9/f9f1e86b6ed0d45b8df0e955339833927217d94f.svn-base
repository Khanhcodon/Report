function messageBoxAlert(settings) {
    eGovMessage.show(getResourceText(egov.resources.bkavmessagebox.useshowtoreplacealert), eGovMessage.messageTypes.error);
    return;
    var options = {
        modal: true,
        minHeight: 30,
        width: 'auto',
        title: getResourceText(egov.resources.bkavmessagebox.notify),
        resizable: false,
        buttons: {},
        close: function () {
            $(this).remove();
        }
    };
    var $div;
    if (typeof settings === 'string') {
        options.buttons[getResourceText(egov.resources.bkavmessagebox.closebutton)] = function () {
            $(this).dialog('close');
        };
        $div = $('<div>' + settings + '</div>');
    } else if (settings && typeof settings === 'object') {
        if (settings.title && typeof settings.title === 'string' && settings.title != '') {
            options.title = settings.title;
        }
        if (!settings.buttonOkName || typeof settings.buttonOkName !== 'string' || settings.buttonOkName == '') {
            settings.buttonOkName = getResourceText(egov.resources.bkavmessagebox.closebutton);
        }
        options.buttons[settings.buttonOkName] = function () {
            if ($.isFunction(settings.buttonOkFunction)) {
                settings.buttonOkFunction();
            }
            $(this).dialog('close');
        };

        if (settings.autoClose) {
            options.modal = false;
            setInterval(function () {
                $div.remove();
            }, 2000);
        }
        $div = $('<div>' + settings.message + '</div>');
    }
    options.create = function () {
        $(".ui-dialog-titlebar").find("a").text(getResourceText(egov.resources.bkavmessagebox.closebutton));
    };
    if ($div) {
        $div.dialog(options);
    }
}

function messageBoxConfirm(settings) {
    eGovMessage.show(getResourceText(egov.resources.bkavmessagebox.useshowtoreplaceconfirm), eGovMessage.messageTypes.error);
    return;
    var options = {
        modal: true,
        minHeight: 30,
        width: 'auto',
        title: getResourceText(egov.resources.bkavmessagebox.ok),
        resizable: false,
        buttons: {},
        close: function () {
            $(this).remove();
        }
    };
    if (settings.title && typeof settings.title === 'string' && settings.title != '') {
        options.title = settings.title;
    }
    if (!settings.buttonOkName || typeof settings.buttonOkName !== 'string' || settings.buttonOkName == '') {
        settings.buttonOkName = getResourceText(egov.resources.bkavmessagebox.ok);
    }
    options.buttons[settings.buttonOkName] = function () {
        if ($.isFunction(settings.buttonOkFunction)) {
            settings.buttonOkFunction();
        }
        $(this).dialog('close');
    };
    if (!settings.buttonCancelName || typeof settings.buttonCancelName !== 'string' || settings.buttonCancelName == '') {
        settings.buttonCancelName = getResourceText(egov.resources.bkavmessagebox.cancel);
    }
    options.buttons[settings.buttonCancelName] = function () {
        if ($.isFunction(settings.buttonCancelFunction)) {
            settings.buttonCancelFunction();
        }
        $(this).dialog('close');
    };
    options.create = function () {
        $(".ui-dialog-titlebar").find("a").text(getResourceText(egov.resources.bkavmessagebox.closebutton));
    };
    $('<div>' + settings.message + '</div>').dialog(options);
}

function messageTemp(settings) {
    eGovMessage.notification(getResourceText(egov.resources.bkavmessagebox.usenotificationtoreplacetemp), eGovMessage.messageTypes.error);
    return;
    settings = $.extend({
        message: '',
        timeout: 3000,
        width: 'auto',
        type: 'success',
        center: false
    }, settings);
    var $div = $('<div style="background: url(/Content/Images/' + (settings.type.toLowerCase() == 'success' ? 'check_32x32.png' : 'forbidden_32x32.png') + ') no-repeat 5px 5px;min-height: 42px"><div style="color: white; padding: 5px 5px 5px 45px; text-align: left;">' + settings.message + '</div></div>');
    var obj = {
        message: $div,
        fadeIn: 700,
        fadeOut: 700,
        timeout: settings.timeout,
        showOverlay: false,
        css: {
            width: settings.width,
            border: 'none',
            padding: '5px',
            backgroundColor: '#000',
            'font-size': '18px',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            'border-radius': '10px',
            opacity: 1,
            color: '#fff'
        }
    };
    if (!settings.center) {
        obj.css.top = '10px';
        obj.css.left = '';
        obj.css.right = '10px';
    }
    $.blockUI(obj);
}

(function (window, $) {
    /// <summary>Lớp quản lý hiển thị message cho egov</summary>
    var eGovMessage = function () {
        this.messageButtons = {
            Ok: 1,
            YesNo: 2,
            OkCancel: 3
        };
        this.messageResult = {
            Ok: 1,
            Yes: 2,
            No: 3,
            Cancel: 4
        };
        this.messageTypes = {
            success: 1,
            error: 2,
            warning: 3,
            processing: 4
        };
        this.autoHide = false;
    };

    if (!window.eGovMessage) {
        window.eGovMessage = new eGovMessage();
    }

    /// <summary>Hiển thị message box</summary>
    eGovMessage.prototype.show = function (message, title, messageButton, okCallBackFunction, cancelCallBackFunction, autoHide) {
        //this.notification('hide');
        if (autoHide == null) {
            autoHide = false;
        }
        this.autoHide = autoHide;
        var defaultSettings = _getDialogSetting();
        var dialogObj = _getDialogObj();
        if (typeof message !== 'string') {
            message = '';
        }
        dialogObj.html(message);

        if (typeof title === 'string' && title !== '') {
            defaultSettings.title = title;
        }

        defaultSettings.buttons = _getDialogButtons(messageButton, okCallBackFunction, cancelCallBackFunction);

        if (this.autoHide) {
            defaultSettings.modal = false;
            setInterval(function () {
                dialogObj.remove();
            }, 2000);
        }

        dialogObj.dialog(defaultSettings);
    };

    eGovMessage.prototype.notification = function (message, messageType, autoHide) {
        if (autoHide == undefined) {
            autoHide = true;
        }
        if (messageType == undefined) {
            messageType = this.messageTypes.success;
        }
        if (message === "hide") {
            $.unblockUI();
            return;
        }
        var options = _getNotifiSetting();
        var notifyObj = $("<div>").html(message).css({
            color: _getNotifiColor(messageType),
            textAlign: "left",
            height: "22px",
            lineHeight: "22px",
            backgroundColor: "#F3ECBA"
        });
        notifyObj.append($("<img>").attr("src", _getNotifiIcon(messageType)).css({
            width: "16px",
            float: "left",
            marginRight: "5px",
            marginTop: "3px"
        }));
        options.message = notifyObj;
        if (autoHide) {
            options.timeout = 2000;
        }
        $.blockUI(options);
    };

    var _getDialogSetting = function () {
        return {
            modal: true,
            minHeight: 30,
            minWidth: 200,
            width: 'auto',
            title: getResourceText(egov.resources.bkavmessagebox.notify),
            resizable: false,
            buttons: [],
            close: function () {
                $(this).remove();
            },
            create: function () {
                $(".ui-dialog-titlebar").find("a").text(getResourceText(egov.resources.bkavmessagebox.closebutton));
            }
        };
    };

    var _getNotifiSetting = function () {
        return {
            message: '',
            fadeIn: 400,
            fadeOut: 700,
            timeout: 0,
            showOverlay: false,
            css: {
                width: 'auto',
                //minWidth: '100px',
                border: 'none',
                padding: '2px 5px',
                backgroundColor: '#F3ECBA',
                fontSize: '11px',
                borderRadius: '3px',
                opacity: 1,
                top: '6px',
                left: '800px',
                cursor: 'default'
            }
        };
    };

    var _getDialogObj = function () {
        var result = $("<div>");
        result.css("min-width", "200px");
        return result;
    };

    var _getNotifiIcon = function (type) {
        switch (type) {
            case window.eGovMessage.messageTypes.success:
                return "/Content/Images/notification_success.png";
            case window.eGovMessage.messageTypes.error:
                return "/Content/Images/notification_error.png";
            case window.eGovMessage.messageTypes.warning:
                return "/Content/Images/notification_warning.png";
            case window.eGovMessage.messageTypes.processing:
                return "/Content/Images/notification_processing.gif";
            default:
                return "/Content/Images/forbidden_32x32.png";
        }
    };

    var _getNotifiColor = function (type) {
        switch (type) {
            case window.eGovMessage.messageTypes.success:
                return "#0DBD68";
            case window.eGovMessage.messageTypes.error:
                return "#EB6565";
            case window.eGovMessage.messageTypes.warning:
                return "#DDAB4E";
            case window.eGovMessage.messageTypes.processing:
                return "#4E9444";
            default:
                return "#b7b7b7";
        }
    }

    var _getDialogButtons = function (button, okCallBackFunction, cancelCallBackFunction) {
        if (typeof button !== 'number') {
            return [];
        }
        var result = [];

        switch (button) {
            case window.eGovMessage.messageButtons.YesNo:
                result.push({
                    text: getResourceText(egov.resources.bkavmessagebox.yes),
                    click: function () {
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        $(this).dialog('close');
                        return window.eGovMessage.messageResult.Yes;
                    }
                });
                result.push({
                    text: getResourceText(egov.resources.bkavmessagebox.no),
                    click: function () {
                        if (typeof cancelCallBackFunction === "function") {
                            cancelCallBackFunction();
                        }
                        $(this).dialog('close');
                        return window.eGovMessage.messageResult.No;
                    }
                });
                break;
            case window.eGovMessage.messageButtons.OkCancel:
                result.push({
                    text: getResourceText(egov.resources.bkavmessagebox.ok),
                    click: function () {
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        $(this).dialog('close');
                        return window.eGovMessage.messageResult.OK;
                    }
                });
                result.push({
                    text: getResourceText(egov.resources.bkavmessagebox.cancel),
                    click: function () {
                        if (typeof cancelCallBackFunction === "function") {
                            cancelCallBackFunction();
                        }
                        $(this).dialog('close');
                        return window.eGovMessage.messageResult.Cancel;
                    }
                });
                break;
            default:
                result.push({
                    text: getResourceText(egov.resources.bkavmessagebox.ok),
                    click: function () {
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        $(this).dialog('close');
                        return window.eGovMessage.messageResult.OK;
                    }
                });
                break;
        }
        return result;
    };

    function getResourceText(resourceKey) {
        try {
            return resourceKey;
        } catch (e) {
            return JSON.stringify(resourceKey);
        }
    }
})
(window, window.jQuery, undefined)