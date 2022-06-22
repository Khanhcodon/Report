(function () {

    var ModalIndicatorView = Backbone.View.extend({
        el: "#modalIndicator",
        events: {
            "click #btnSaveIndicator": "saveIndicator"
        },
        initialize: function (options) {
            var that = this;
            that.App = options.App;
        },
        render: function (departmentId) {
            var that = this;
            that.departmentId = departmentId;
            $.ajax({
                url: "/Admin/IndicatorDepartment/GetIndicator",
                data: {
                    departmentId: that.departmentId
                },
                type: 'Get',
                error: function (a, b, c) {},
                success: function (response) {
                    var templateTree = '<div id="chooseIndicatorTree">Chọn chỉ tiêu</div>';
                    that.$el.find("#bodyTree").html(templateTree)

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

                    $('#searchChooseIndicator').keyup(function () {
                        that.$el.find("#chooseIndicatorTree").jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#searchChooseIndicator').val();
                            if (!v) {
                                that.$el.find("#chooseIndicatorTree").jstree(true).open_all();
                            }
                            that.$el.find("#chooseIndicatorTree").jstree(true).search(v);
                        }, 250);
                    });
                }
            });
            
            $("#modalIndicator").modal("show");
        },

        saveIndicator: function () {
            var that = this;
            var data = that.$el.find("#chooseIndicatorTree").jstree("get_selected");
            $.ajax({
                url: "/Admin/IndicatorDepartment/SaveIndicator",
                data: {
                    departmentId: that.departmentId,
                    dataIds: JSON.stringify(data)
                },
                type: 'Post',
                error: function (a, b, c) { },
                success: function (response) {
                    $("#modalIndicator").modal("hide");
                    that.alert();
                    that.App.renderIndicatorDepart();
                }
            });
        },
        alert: function () {
            swal("Thành công!", "Thêm mới thành công", "success");
        }
    })

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
        },

        renderIndicatorDepart: function () {
            var that = this;
            $.ajax({
                url: "/Admin/IndicatorDepartment/GetIndicator",
                type: 'Get',
                data: {
                    departmentId: that.departmentId
                },

                error: function (a, b, c) {
                },

                success: function (response) {

                    var templateTree = '<div id="indicatorTree">Chọn chỉ tiêu</div>';
                    that.$el.find("#indicatorBodyTree").html(templateTree);

                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-line-chart"
                        return kpi
                    });

                    kpis = _.filter(kpis, function (kpi) {
                        return kpi.indicatorDepartCheck == true
                    })

                    $("#indicatorTree").jstree({
                        "plugins": ["search"],
                        'core': {
                            'data': kpis
                        }
                    }).bind("loaded.jstree", function (event, data) {
                        $(this).jstree("open_all");
                    });

                    var to = false;
                    $('#searchIndicator').keyup(function () {
                        $('#indicatorTree').jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#searchIndicator').val();
                            if (!v) {
                                $('#indicatorTree').jstree(true).open_all();
                            }
                            $('#indicatorTree').jstree(true).search(v);
                        }, 250);
                    });
                }
            });
        },
        renderDepartment: function () {
            var that = this;
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
                    });
                    departments[0].state = {
                        selected: true
                    }
                    $("#department").jstree({
                        "plugins": ["search"],
                        'core': {
                            'data': departments
                        },
                        "types": {
                            "tree": { "icon": "mdi mdi-email" }
                        },
                    }).bind("loaded.jstree", function (event, data) {
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
                    $("#department").on('changed.jstree', function (e, data) {
                        var dataid = data.selected[0];
                        that.departmentId = dataid;
                        that.renderIndicatorDepart();
                    });
                }
            });
        },

        renderModalIndicators: function () {
            var that = this;
            if (!that.modalIndicator) {
                that.modalIndicator = new ModalIndicatorView({App: that});
            }
            
            that.modalIndicator.render(that.departmentId);
        }
    });

    var appview = new AppView();
})();