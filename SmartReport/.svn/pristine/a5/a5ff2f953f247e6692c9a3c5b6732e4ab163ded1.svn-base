(function ($) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    jQuery.fn['blockpanel'] = function (settings) {
        egov.message.notification(settings.text, egov.message.messageTypes.processing);
        return;
    };

    jQuery.fn['unblockpanel'] = function () {
        egov.message.notification("hide");
        return;
    };
})(window.jQuery);