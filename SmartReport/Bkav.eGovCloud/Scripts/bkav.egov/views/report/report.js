define(['egovCore'
], function () {
    "use strict";

    var reportView = Backbone.View.extend({

        initialize: function (options) {
            this.el = options.el;
            this.tab = options.tab;
            this.url = options.tab.model.get("url")
            this.render();

            return this;
        },

        render: function () {
            var _this = this;
            var iframe = $("<iframe width='100%' height='100%' frameBorder='0' src='" + this.url + "'> </iframe>")
            this.$el.html(iframe);
        }
    });

    return reportView;
});