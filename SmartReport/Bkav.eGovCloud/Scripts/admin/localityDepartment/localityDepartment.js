$(function () {
    var modelLocalityView = Backbone.View.extend({
        el: "#modalLocality",
        events: {
            "click #btnSaveLocality": "saveLocality"
        },

        initialize: function (options) {
            var that = this;
            that.App = options.App;
        },
        render: function (departmentId) {
            var that = this;
            that.departmentId = departmentId;
            $.ajax({
                url: "/Admin/LocalityDepartment/GetLocality",
                data: {
                    departmentId: that.departmentId
                },
                type: 'Get',
                error: function (a, b, c) { },
                success: function (response) {
                    var templateTree = '<div id="chooseLocalityTree">Chọn địa bàn</div>';
                    that.$el.find("#bodyTree").html(templateTree);

                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-line-chart"
                        return kpi
                    });

                    $("#chooseLocalityTree").jstree({
                        "checkbox": {
                            "keep_selected_style": false,
                            "three_state": false
                        },
                        "plugins": ["wholerow", "checkbox", "search"],
                        'core': {
                            'data': kpis,
                            "multiple": true
                        }
                    }).bind("loaded.jstree", function (event, data) {
                        $(this).jstree("open_all");
                    });

                    var to = false;

                    $('#searchChooseLocality').keyup(function () {
                        that.$el.find("#chooseLocalityTree").jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#searchChooseLocality').val();
                            if (!v || v == "" || v == undefined) {
                                that.$el.find("#chooseLocalityTree").jstree(true).clear_search();
                                that.$el.find("#chooseLocalityTree").jstree(true).open_all();
                            } else {
                                that.$el.find("#chooseLocalityTree").jstree(true).search(v);
                            }
                        }, 250);
                    });
                }
            });
            $("#modalLocality").modal("show");
        },

        saveLocality: function () {
            var that = this;
            var data = that.$el.find("#chooseLocalityTree").jstree("get_selected");
            $.ajax({
                url: "/Admin/LocalityDepartment/saveLocality",
                data: {
                    departmentId: that.departmentId,
                    dataIds: JSON.stringify(data)
                },
                type: 'Post',
                error: function (a, b, c) { },
                success: function (response) {
                    $("#modalLocality").modal("hide");
                    that.alert();
                    that.App.renderLocality();
                }
            });
        },

        alert: function () {
            swal("Thành công!", "Cập nhật thành công", "success");
        }
    });

    var appView = Backbone.View.extend({
        el: ".content-wrapper",
        events: {
            "click #ChooseLocality": "renderModelLocality"
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

        renderLocality: function () {
            var that = this;
            if (that.departmentId != null || that.departmentId != undefined) {
                $.ajax({
                    url: "/Admin/LocalityDepartment/GetLocality",
                    type: 'Get',
                    data: {
                        departmentId: that.departmentId,
                    },
                    success: function (response) {
                        var templateTree = '<div id="localityTree">Chọn địa bàn</div>';
                        that.$el.find("#locality").html(templateTree);

                        var kpis = _.map(response, function (kpi) {
                            kpi["icon"] = "fa fa-line-chart"
                            return kpi
                        });

                        kpis = _.filter(kpis, function (kpi) {
                            return kpi.localityDepartCheck == true
                        })

                        for (var i = 0; i < kpis.length; i++) {
                            var check = false;
                            for (var j = 0; j < kpis.length; j++) {
                                if (kpis[i].parent == kpis[j].id) {
                                    check = true;
                                    break;
                                }
                            }
                            if (!check)
                                kpis[i].parent = "#";
                        }

                        $("#localityTree").jstree({
                            'core': {
                                'data': kpis
                            },
                            "plugins": ["state", "wholerow", "search"]
                        }).bind("loaded.jstree", function (event, data) {
                            $(this).jstree("open_all");
                        });

                        var to = false;
                        $('#searchLocality').keyup(function () {
                            $('#localityTree').jstree(true).close_all();
                            if (to) { clearTimeout(to); }
                            to = setTimeout(function () {
                                var v = $('#searchLocality').val();
                                if (!v || v == null || v == undefined) {
                                    $('#localityTree').jstree(true).clear_search();
                                    $('#localityTree').jstree(true).open_all();
                                }
                                $('#localityTree').jstree(true).search(v);
                            }, 250);
                        });
                    }
                });
            }

        },

        renderDepartment: function () {
            var that = this;
            $.ajax({
                url: "/Admin/LocalityDepartment/GetAllDepartment",
                dataType: "json",
                type: 'Get',
                error: function (a, b, c) {
                },
                success: function (response) {
                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-line-chart"
                        return kpi
                    });

                    kpis[0].state = {
                        selected: true
                    }

                    $("#departmentTree").jstree({
                        'core': {
                            'data': kpis
                        },
                        "plugins": ["state", "wholerow", "search"]
                    }).bind("loaded.jstree", function (event, data) {
                        $(this).jstree("open_all");
                    });

                    var to = false;
                    $('#searchDepartment').keyup(function () {
                        $('#departmentTree').jstree(true).close_all();
                        if (to) { clearTimeout(to); }
                        to = setTimeout(function () {
                            var v = $('#searchDepartment').val();
                            if (!v || v == "" || v == undefined) {
                                $('#departmentTree').jstree(true).clear_search();
                                $('#departmentTree').jstree(true).open_all();
                            }else
                                $('#departmentTree').jstree(true).search(v);
                        }, 250);
                    });

                    $("#departmentTree").on('changed.jstree', function (e, data) {

                        that.departmentId = data.selected[0];
                        that.renderLocality();
                    });
                }
            });
        },

        renderModelLocality: function () {
            var that = this;
            if (!that.modalDepartment) {
                that.modalDepartment = new modelLocalityView({ App: that });
            }
            that.modalDepartment.render(this.departmentId);
        }

    });
    var appview = new appView();
});