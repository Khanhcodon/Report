(function ($) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    jQuery.fn['blockpanel'] = function (settings) {
        eGovMessage.notification(settings.text, eGovMessage.messageTypes.processing);
        return;
        settings = $.extend({
            text: '',
            icon: { width: 32, height: 32 },
            borderWidth: 0,
            backgroundColor: 'transparent'//'#fff'
        }, settings);
        if (settings.icon.width <= 0) {
            settings.icon.width = 32;
        }
        if (settings.icon.height <= 0) {
            settings.icon.height = 32;
        }
        var border = 'none';
        if (settings.borderWidth > 0) {
            border = settings.borderWidth + 'px solid #aaa';
        }
        var padding = "0";
        if (settings.backgroundColor != 'transparent') {
            padding = '10px';
        }
        var message = '<img src="/Content/Images/ajax-loader.gif" width="' + settings.icon.width + '" height="' + settings.icon.height + '" />';
        if (settings.text != '') {
            message += '<br /><strong>' + settings.text + '</strong>';
        }

        this.each(function (i, el) {
            if (el != window) {
                $(el).block({
                    message: message,
                    css: {
                        width: 'auto',
                        height: 'auto',
                        padding: padding,
                        border: 'none',//border,
                        backgroundColor: "transparent"
                    }
                });
            } else {
                $.blockUI({
                    message: message,
                    css: {
                        width: 'auto',
                        height: 'auto',
                        padding: padding,
                        border: 'none',//border,
                        backgroundColor: "transparent"
                    }
                });
            }
        });
    };

    jQuery.fn['unblockpanel'] = function () {
        eGovMessage.notification("hide");
        return;
        this.each(function (i, el) {
            if (el != window) {
                $(el).unblock();
            } else {
                $.unblockUI();
            }
        });
    };
})(window.jQuery);