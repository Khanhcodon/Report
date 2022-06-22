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
                url: "/Admin/Indatatype/GetIndicator",
                data: {
                    departmentId: that.departmentId
                },
                type: 'Get',
                error: function (a, b, c) {},
                success: function (response) {
                    var templateTree = '<div id="chooseIndicatorTree">Chọn chỉ tiêu</div>';
                    that.$el.find("#bodyTree").html(templateTree)

                    var kpis = _.map(response, function (kpi) {
                        kpi["icon"] = "fa fa-folder"
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
                url: "/Admin/Indatatype/SaveIndicator",
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
            if (that.departmentId != null || that.departmentId != undefined) {
                $.ajax({
                    url: "/Admin/Indatatype/GetIndicator",
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
                            kpi["icon"] = "fa fa-folder"
                            return kpi
                        });

                        kpis = _.filter(kpis, function (kpi) {
                            return kpi.indicatorDepartCheck == true
                        })
                        
                        if (kpis.length > 0) {
                            kpis.parent = "#";
                        }

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
            }
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
                            //type: "tree",
                           //icon:"fa fa-home m--font-accent"
                        }
                    });
                    departments[0].state = {
                        selected: true
                    }
                    $("#department").jstree({
                        "plugins": ["search","types"],
                        'core': {
                            'data': departments
                        },
                        "types": {
                            "default": {
                                "icon": "fa fa-home"
                            },
                            "demo": {
                                "icon": "fa fa-file"
                            }
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