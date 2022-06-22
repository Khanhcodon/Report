(function () {
    var AppView = Backbone.View.extend({
        el: ".content-wrapper",
        events: {
            "click #ChooseIndicator": "renderModalIndicators"
        },
        initialize: function (options) {
            //$.jstree.defaults.core.dblclick_toggle = true;
            var that = this;
            that.render();
        },
        render: function () {
            var that = this;
            that.renderDepartment();
            that.renderIndicatorDepart();
        },

        renderDepartment: function () {
            $.ajax({
                url: "/Admin/IndicatorDepartment/GetCatalog",
                type: 'Get',
                error: function (a, b, c) {
                },
                success: function (response) {
                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-check-square-o"
                        return kpi
                    });
                    $("#indicatorTree").jstree({
                        'core': {
                            'data': kpis
                        },
                        "types": {
                            "tree": { "icon": "mdi mdi-email" }
                        }
                    });
                }
            });
        },
        renderIndicatorDepart: function () {
            $.ajax({
                url: "/webapi/user/GetAllDepartment",
                type: 'Get',
                error: function (a, b, c) {
                },
                success: function (response) {
                    var departments = _.map(response, function (dp) {
                        return {
                            id: dp.value,
                            text: dp.data,
                            parent: dp.parentid == 0 ? "#" : dp.parentid,
                            type: "tree",
                            icon: "fa fa-home"
                        }
                    })
                    $("#department").jstree({
                        "plugins": ["search"],
                        'core': {
                            'data': departments
                        },
                        "types": {
                            "tree": { "icon": "mdi mdi-email" }
                        },
                    }).bind("loaded.jstree", function (event, data) {
                        // you get two params - event & data - check the core docs for a detailed description
                        $(this).jstree("open_all");
                    });
                    var to = false;
                    $('#searchDepartment').keyup(function () {
                        $('#department').jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#searchDepartment').val();
                            if (!v) {
                                $('#department').jstree(true).open_all();
                            }
                            $('#department').jstree(true).search(v);
                        }, 250);
                    });
                }
            });
        },

        renderModalIndicators: function () {
           
            $.ajax({
                url: "/Admin/IndicatorDepartment/GetCatalog",
                type: 'Get',
                error: function (a, b, c) {
                },
                success: function (response) {
                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-line-chart"
                        return kpi
                    });
                    $("#chooseIndicatorTree").jstree({
                        "checkbox" : {
                            "keep_selected_style" : false
                        },
                        "plugins" : [ "checkbox" ],
                        'core': {
                            'data': kpis
                        }
                    }).bind("loaded.jstree", function (event, data) {
                        // you get two params - event & data - check the core docs for a detailed description
                        $(this).jstree("open_all");
                    });;
                }
            });
            $("#modalIndicator").modal("show");
        }
    });

    var appview = new AppView();
})();