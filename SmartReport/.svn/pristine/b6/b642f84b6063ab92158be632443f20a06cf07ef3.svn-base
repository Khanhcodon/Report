(function () {
    var AppView = Backbone.View.extend({
        el: "#reportCreate",
        events: {
        },
        initialize: function (options) {
            var that = this;
            that.render();
        },
        render: function () {
            var that = this;
            $('#sectionDetail').slimScroll({
                height: '400px'
            });
            $('#treeIndicator').slimScroll({
                height: '200px'
            });
            
            that.renderIndicator();
        },

        renderIndicator: function () {
            var that = this;
            $.ajax({
                url: "/Admin/IndicatorDepartment/GetCatalog",
                data: {},
                type: 'Get',
                error: function (a, b, c) { },
                success: function (response) {
                    var templateTree = '<div id="chooseIndicatorTree">Chọn chỉ tiêu</div>';
                    that.$el.find("#treeIndicator").html(templateTree)

                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-line-chart"
                        return kpi
                    });

                    that.$el.find("#chooseIndicatorTree").jstree({
                        "checkbox": {
                            "keep_selected_style": false,
                            "three_state": false
                        },
                        "plugins": ["checkbox", "search"],
                        'core': {
                            'data': kpis,
                            "multiple": true
                        }
                    }).bind("loaded.jstree", function (event, data) {
                        $(this).jstree("open_all");
                    });

                    var to = false;

                    $('#inputIndicatorSearch').keyup(function () {
                        that.$el.find("#chooseIndicatorTree").jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#inputIndicatorSearch').val();
                            if (!v) {
                                that.$el.find("#chooseIndicatorTree").jstree(true).open_all();
                            }
                            that.$el.find("#chooseIndicatorTree").jstree(true).search(v);
                        }, 250);
                    });
                }
            });
        }
    });

    var appView = new AppView
})();