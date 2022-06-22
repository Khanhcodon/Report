(function ($) {
    $(".ui-autocomplete-input").bind("autocompleteopen", function () {
        var autocomplete = $(this).data("autocomplete"), menu = autocomplete.menu;

        if (!autocomplete.options.selectFirst) {
            return;
        }
        menu.activate($.Event({ type: "mousehover" }), menu.element.children().first());
    });

} (jQuery));