(function ($) {
    if ($.validator) {
        $.each($.validator.unobtrusive.adapters, function () {
            var baseAdapt = this.adapt;
            if (this.name === "number") {
                this.adapt = function (options) {
                    var fieldName = new RegExp("The field (.+) must be a number").exec(options.message)[1];
                    options.message = $.validator.format($.validator.messages.number, fieldName);
                    baseAdapt(options);
                };
            }
        });
    }
} (jQuery));