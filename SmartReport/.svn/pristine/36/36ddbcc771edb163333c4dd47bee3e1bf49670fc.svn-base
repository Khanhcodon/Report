(function () {
    var AppView = Backbone.View.extend({
        el: ".rightIncatalog",
        events: {
            "click #ChooseIndicator": "renderModalIndicators",
            "change #IncataLogIdTree": "renderIndicatorValues",
            "click #DeleteIndicatroTree": "deleteInCataValue",
            "click .jstree-anchor": "renderClickTree",
            "click #EditDivRight": "renderClickEditDivRight",
            "click #tagFormInCatalogValue": "renderModalShow",
            "keyup #indicatorTree_search": "renderSearchStatitic",
        },
        initialize: function (options) {
            var that = this;
            that.render();
        },
        render: function () {
            var that = this;
            that.renderInCatalogValue();
            that.renderSelect2();
            that.renderTree();
        },
        renderTree: function () {
            $.ajax({
                url: "/Admin/IndicatorTree/GetTree",
                type: "Get",
                error: function (a, b, c) {

                },
                success: function (result) {
                    //treeIncatalog
                    var kpis = _.map(result, function (kpi) {
                        kpi["icon"] = "fa fa-home"
                        return kpi
                    });
                    $("#treeIncatalog").jstree({
                        'core': {
                            "mulitple": false,
                            "animation": 100,
                            "check_callback": true,
                            "themes": {
                                "variant": "medium",
                                "dots": true
                            },
                            'data': kpis
                        },
                        "types": {
                            "root": {
                                "icon": "glyphicon glyphicon-plus"
                            },
                            "child": {
                                "icon": "glyphicon glyphicon-leaf"
                            },
                            "default": {
                            }
                        },
                        "checkbox": {
                            "keep_selected_style": false,
                            "three_state": true,
                            "whole_node": true
                        },
                        "conditionalselect": function (node, event) {
                            return false;
                        },
                        "plugins": [
                             "contextmenu",
                              "dnd",
                             "types",
                             "unique",
                             "changed",
                             "html_data",
                             "themes",
                             "ui",
                             "crrm",
                             "search"
                        ],
                        "contextmenu": {
                            "items": function ($node) {
                                var tree = $("#treeIncatalog").jstree(true);
                                return {
                                    "Create": {
                                        "separator_before": false,
                                        "separator_after": false,
                                        "label": "Thêm mới chỉ tiêu đơn vị",
                                        "action": function (obj) {
                                            var inCatalogValueId = obj.reference.prevObject[0].id;
                                            //call ajax

                                            $.ajax({
                                                url: '/IndicatorTree/Edit',
                                                data: { id: inCatalogValueId },
                                                success: function (result) {

                                                    $('.btnAddsValue').removeAttr('id');
                                                    $('.btnAddsValue').attr('id', 'btnAddIncatalogValue');

                                                    var IncataLogIds_, ParentId_, InCatalogValueId_;
                                                    IncataLogIds_ = result.InCatalogIds;
                                                    InCatalogValueId_ = result.InCatalogValueId;
                                                    if (typeof result.ParentId != "undefined") {
                                                        ParentId_ = result.ParentId;
                                                    } else {
                                                        ParentId_ = "00000000-0000-0000-0000-000000000000";
                                                    }

                                                    $('#InCatalogIdReplace').select2().val(JSON.parse(IncataLogIds_)).trigger("change");
                                                    $("#IncataLogIds").val(IncataLogIds_);
                                                    $("#ParentId").select2().val(InCatalogValueId_).trigger("change");

                                                    $('#txtCode_FIELD_IncataLogValue').val('');
                                                    $('#txtNAME_FIELD_IncataLogValue').val('');
                                                    $('#InCatalogValueId').val('');
                                                    $('#TypeSelect').select2().val("").trigger("change");
                                                    $('#UnitSelect').select2().val("").trigger("change");
                                                    $('#Description').val('');
                                                    $('#IsActivated_In').prop('checked', false);
                                                    $('#Threshold_min').val('');
                                                    $('#Threshold_max').val('');
                                                    $('#AllowAggregation').prop('checked', false);
                                                    $('#AggregationFormula').select2().val("").trigger("change");
                                                    $('#NumberPeriodReplace').val('');
                                                    $('#AllowAggregationByPeriod').prop('checked', false);
                                                    $('#tagFormInCatalogValue').click();

                                                },
                                                error: function (xhr) { },
                                                complete: function () {
                                                }
                                            });
                                        }
                                    },
                                    "Rename": {
                                        "separator_before": false,
                                        "separator_after": false,
                                        "label": "Sửa chỉ tiêu đơn vị",
                                        "action": function (obj) {
                                            var inCatalogValueId = obj.reference.prevObject[0].id;
                                            //call ajax

                                            $.ajax({
                                                url: '/IndicatorTree/Edit',
                                                data: { id: inCatalogValueId },
                                                success: function (result) {
                                                    $('.btnAddsValue').removeAttr('id');
                                                    $('.btnAddsValue').attr('id', 'editCatalogValue');


                                                    var InCatalogValueCode_, InCatalogValueName_,
                                                        ParentId_, Unit_, IncataLogIds_, InCatalogValueId_,
                                                        Level_, Type_, Unit_, Description_, Active_,
                                                            Threshold_min_, Threshold_max_, AllowAggregation_, AggregationFormula_,
                                                            NumberPeriodReplace_, AllowAggregationByPeriod_;

                                                    IncataLogIds_ = result.InCatalogIds;
                                                    InCatalogValueId_ = result.InCatalogValueId;
                                                    InCatalogValueCode_ = result.InCatalogValueCode;
                                                    InCatalogValueName_ = result.InCatalogValueName;
                                                    Type_ = result.Type; // loại số liệu
                                                    Unit_ = result.Unit;// đơn vị tính
                                                    Description_ = result.Description; // mô tả
                                                    // kỳ công bố
                                                    Active_ = result.Active; // sử dụng
                                                    Threshold_min_ = result.Threshold_min; // giới hạn nhỏ nhất
                                                    Threshold_max_ = result.Threshold_max; // giới hạn lớn nhất
                                                    AllowAggregation_ = result.AllowAggregation; // cho phép tổng hợp theo địa bàn đơn vị
                                                    AggregationFormula_ = result.AggregationFormula; // ham tổng hợp
                                                    NumberPeriodReplace_ = result.NumberPeriodReplace; // so ky
                                                    AllowAggregationByPeriod_ = result.AllowAggregationByPeriod; // cho phep tong hop theo ky


                                                    if (typeof result.ParentId != "undefined") {
                                                        ParentId_ = result.ParentId;
                                                    } else {
                                                        ParentId_ = "00000000-0000-0000-0000-000000000000";
                                                    }

                                                    if (typeof result.Unit != "undefined") {
                                                        Unit_ = result.Unit;
                                                    }
                                                    $('#txtCode_FIELD_IncataLogValue').val(InCatalogValueCode_);
                                                    $('#txtNAME_FIELD_IncataLogValue').val(InCatalogValueName_);
                                                    //$('') Unit
                                                    $('#InCatalogValueId').val(InCatalogValueId_);
                                                    $('#TypeSelect').select2().val(Type_).trigger("change");
                                                    $('#UnitSelect').select2().val(Unit_).trigger("change");
                                                    $('#Description').val(Description_);
                                                    $("#InCatalogIdReplace").select2().val(JSON.parse(IncataLogIds_)).trigger("change");
                                                    $("#ParentId").select2().val(ParentId_).trigger("change");
                                                    $("#IncataLogIds").val(IncataLogIds_);
                                                    $('#IsActivated_In').prop('checked', Active_);
                                                    $('#Threshold_min').val(Threshold_min_);
                                                    $('#Threshold_max').val(Threshold_max_);
                                                    $('#AllowAggregation').prop('checked', AllowAggregation_);
                                                    $('#AggregationFormula').select2().val(AggregationFormula_).trigger("change");
                                                    $('#NumberPeriodReplace').val(NumberPeriodReplace_);
                                                    $('#AllowAggregationByPeriod').prop('checked', AllowAggregationByPeriod_);
                                                    $('#tagFormInCatalogValue').click();

                                                },
                                                error: function (xhr) { },
                                                complete: function () {
                                                }
                                            });
                                        }
                                    },
                                    "Delete": {
                                        "separator_before": false,
                                        "separator_after": false,
                                        "label": "Xóa chỉ tiêu đơn vị",
                                        "action": function (obj) {

                                            swal({
                                                title: "Bạn có chắc?",
                                                text: "Xóa tiêu thức phân tổ này không?",
                                                type: "warning",
                                                showCancelButton: true,
                                                confirmButtonColor: "#DD6B55",
                                                confirmButtonText: "Đồng ý!",
                                                cancelButtonText: "Hủy bỏ!",
                                                closeOnConfirm: false,
                                                closeOnCancel: false
                                            },
                                           function (isConfirm) {
                                               if (isConfirm) {
                                                   var data;
                                                   //xu ly
                                                   //var inCatalogValueId = obj.reference.prevObject[0].id;
                                                   var children_ = $('#treeIncatalog').jstree('get_selected', true)[0].children;
                                                   var children_d_ = $('#treeIncatalog').jstree('get_selected', true)[0].children;
                                                   if (children_.length > 0 && children_d_.length > 0) {
                                                       var countArray = $('#treeIncatalog').jstree('get_selected', true)[0].children_d;
                                                       countArray.push($('#treeIncatalog').jstree('get_selected', true)[0].id);
                                                       var data_to_send = JSON.stringify(countArray);
                                                       data = { model: JSON.parse(data_to_send) };
                                                   } else {
                                                       var countArray = $('#treeIncatalog').jstree('get_selected', true)[0].id;
                                                       var data_to_send = JSON.stringify(countArray);
                                                       data = { model: JSON.parse(data_to_send) };
                                                   }
                                                   //call ajax
                                                   $.ajax({
                                                       url: '/IndicatorTree/Delete',
                                                       type: 'POST',
                                                       data: data,
                                                       success: function (result) {
                                                       },
                                                       traditional: true,
                                                       error: function (xhr) { },
                                                       complete: function () {
                                                           swal("Thành công!", "Xóa chỉ tiêu thành công", "success");
                                                           setTimeout(function () {
                                                               window.location.reload();
                                                           }, 1000)
                                                       }
                                                   });

                                               } else {
                                                   swal("Hủy bỏ", "Hủy xóa thành công!", "warning");
                                               }
                                           });
                                        }
                                    }
                                };
                            }
                        }
                    }).bind("loaded.jstree", function (event, data) {
                        $(this).jstree("open_all");
                    });
                }
            });
        },
        renderSelect2: function () {
            $('#IncataLogId').select2();
            $('#IncataLogIdTree').select2();
            $('#ParentId').select2();
            $('#UnitSelect').select2();
            $('#TypeSelect').select2();
            $('#AggregationFormula').select2();
            //test mul

            $('#InCatalogIdReplace').select2({
                multiple: "multiple"
            });
        },
        renderInCatalogValue: function () {
            $.ajax({
                url: "/Admin/IndicatorTree/GetCatalog",
                type: 'Get',
                error: function (a, b, c) {
                },
                success: function (response) {
                }
            });
        },
        renderIndicatorValues: function () {
            var that = this;
            $('#appendDiv').empty();
            $('#treeIncatalog').jstree("destroy").empty();
            var that = this;
            var valueCatalogID = that.$('#IncataLogIdTree').val();
            if (valueCatalogID != "00000000-0000-0000-0000-000000000000") {
                $.ajax({
                    url: "/Admin/IndicatorTree/GetCatalogs",
                    type: "Get",
                    data: { inCatalogId: valueCatalogID },
                    error: function (a, b, c) {

                    },
                    success: function (result) {
                        //treeIncatalog
                        var kpis = _.map(result, function (kpi) {
                            kpi["icon"] = "fa fa-home"
                            return kpi
                        });
                        $("#treeIncatalog").jstree({
                            'core': {
                                "mulitple": false,
                                "animation": 100,
                                "check_callback": true,
                                "themes": {
                                    "variant": "medium",
                                    "dots": true
                                },
                                'data': kpis
                            },
                            "types": {
                                "root": {
                                    "icon": "glyphicon glyphicon-plus"
                                },
                                "child": {
                                    "icon": "glyphicon glyphicon-leaf"
                                },
                                "default": {
                                }
                            },
                            "checkbox": {
                                "keep_selected_style": false,
                                "three_state": true,
                                "whole_node": true
                            },
                            "conditionalselect": function (node, event) {
                                return false;
                            },
                            "plugins": [
                                 "contextmenu",
                                  "dnd",
                                 "types",
                                 "unique",
                                 "changed",
                                 "html_data",
                                 "themes",
                                 "ui",
                                 "crrm",
                                 "search"
                            ],
                            "contextmenu": {
                                "items": function ($node) {
                                    var tree = $("#treeIncatalog").jstree(true);
                                    return {
                                        "Create": {
                                            "separator_before": false,
                                            "separator_after": false,
                                            "label": "Thêm mới chỉ tiêu đơn vị",
                                            "action": function (obj) {
                                                var inCatalogValueId = obj.reference.prevObject[0].id;
                                                //call ajax

                                                $.ajax({
                                                    url: '/IndicatorTree/Edit',
                                                    data: { id: inCatalogValueId },
                                                    success: function (result) {

                                                        $('.btnAddsValue').removeAttr('id');
                                                        $('.btnAddsValue').attr('id', 'btnAddIncatalogValue');

                                                        var IncataLogIds_, ParentId_, InCatalogValueId_;
                                                        IncataLogIds_ = result.InCatalogIds;
                                                        InCatalogValueId_ = result.InCatalogValueId;
                                                        if (typeof result.ParentId != "undefined") {
                                                            ParentId_ = result.ParentId;
                                                        } else {
                                                            ParentId_ = "00000000-0000-0000-0000-000000000000";
                                                        }

                                                        $('#InCatalogIdReplace').select2().val(JSON.parse(IncataLogIds_)).trigger("change");
                                                        $("#IncataLogIds").val(IncataLogIds_);
                                                        $("#ParentId").select2().val(InCatalogValueId_).trigger("change");

                                                        $('#txtCode_FIELD_IncataLogValue').val('');
                                                        $('#txtNAME_FIELD_IncataLogValue').val('');
                                                        $('#InCatalogValueId').val('');
                                                        $('#TypeSelect').select2().val("").trigger("change");
                                                        $('#UnitSelect').select2().val("").trigger("change");
                                                        $('#Description').val('');
                                                        $('#IsActivated_In').prop('checked', false);
                                                        $('#Threshold_min').val('');
                                                        $('#Threshold_max').val('');
                                                        $('#AllowAggregation').prop('checked', false);
                                                        $('#AggregationFormula').select2().val("").trigger("change");
                                                        $('#NumberPeriodReplace').val('');
                                                        $('#AllowAggregationByPeriod').prop('checked', false);
                                                        $('#tagFormInCatalogValue').click();

                                                    },
                                                    error: function (xhr) { },
                                                    complete: function () {
                                                    }
                                                });
                                            }
                                        },
                                        "Rename": {
                                            "separator_before": false,
                                            "separator_after": false,
                                            "label": "Sửa chỉ tiêu đơn vị",
                                            "action": function (obj) {
                                                var inCatalogValueId = obj.reference.prevObject[0].id;
                                                //call ajax

                                                $.ajax({
                                                    url: '/IndicatorTree/Edit',
                                                    data: { id: inCatalogValueId },
                                                    success: function (result) {
                                                        $('.btnAddsValue').removeAttr('id');
                                                        $('.btnAddsValue').attr('id', 'editCatalogValue');


                                                        var InCatalogValueCode_, InCatalogValueName_,
                                                            ParentId_, Unit_, IncataLogIds_, InCatalogValueId_,
                                                            Level_, Type_, Unit_, Description_, Active_,
                                                                Threshold_min_, Threshold_max_, AllowAggregation_, AggregationFormula_,
                                                                NumberPeriodReplace_, AllowAggregationByPeriod_;

                                                        IncataLogIds_ = result.InCatalogIds;
                                                        InCatalogValueId_ = result.InCatalogValueId;
                                                        InCatalogValueCode_ = result.InCatalogValueCode;
                                                        InCatalogValueName_ = result.InCatalogValueName;
                                                        Type_ = result.Type; // loại số liệu
                                                        Unit_ = result.Unit;// đơn vị tính
                                                        Description_ = result.Description; // mô tả
                                                        // kỳ công bố
                                                        Active_ = result.Active; // sử dụng
                                                        Threshold_min_ = result.Threshold_min; // giới hạn nhỏ nhất
                                                        Threshold_max_ = result.Threshold_max; // giới hạn lớn nhất
                                                        AllowAggregation_ = result.AllowAggregation; // cho phép tổng hợp theo địa bàn đơn vị
                                                        AggregationFormula_ = result.AggregationFormula; // ham tổng hợp
                                                        NumberPeriodReplace_ = result.NumberPeriodReplace; // so ky
                                                        AllowAggregationByPeriod_ = result.AllowAggregationByPeriod; // cho phep tong hop theo ky


                                                        if (typeof result.ParentId != "undefined") {
                                                            ParentId_ = result.ParentId;
                                                        } else {
                                                            ParentId_ = "00000000-0000-0000-0000-000000000000";
                                                        }

                                                        if (typeof result.Unit != "undefined") {
                                                            Unit_ = result.Unit;
                                                        }
                                                        $('#txtCode_FIELD_IncataLogValue').val(InCatalogValueCode_);
                                                        $('#txtNAME_FIELD_IncataLogValue').val(InCatalogValueName_);
                                                        //$('') Unit
                                                        $('#InCatalogValueId').val(InCatalogValueId_);
                                                        $('#TypeSelect').select2().val(Type_).trigger("change");
                                                        $('#UnitSelect').select2().val(Unit_).trigger("change");
                                                        $('#Description').val(Description_);
                                                        $("#InCatalogIdReplace").select2().val(JSON.parse(IncataLogIds_)).trigger("change");
                                                        $("#ParentId").select2().val(ParentId_).trigger("change");
                                                        $("#IncataLogIds").val(IncataLogIds_);
                                                        $('#IsActivated_In').prop('checked', Active_);
                                                        $('#Threshold_min').val(Threshold_min_);
                                                        $('#Threshold_max').val(Threshold_max_);
                                                        $('#AllowAggregation').prop('checked', AllowAggregation_);
                                                        $('#AggregationFormula').select2().val(AggregationFormula_).trigger("change");
                                                        $('#NumberPeriodReplace').val(NumberPeriodReplace_);
                                                        $('#AllowAggregationByPeriod').prop('checked', AllowAggregationByPeriod_);
                                                        $('#tagFormInCatalogValue').click();

                                                    },
                                                    error: function (xhr) { },
                                                    complete: function () {
                                                    }
                                                });
                                            }
                                        },
                                        "Delete": {
                                            "separator_before": false,
                                            "separator_after": false,
                                            "label": "Xóa chỉ tiêu đơn vị",
                                            "action": function (obj) {

                                                swal({
                                                    title: "Bạn có chắc?",
                                                    text: "Xóa chỉ tiêu này không ?",
                                                    type: "warning",
                                                    showCancelButton: true,
                                                    confirmButtonColor: "#DD6B55",
                                                    confirmButtonText: "Đồng ý!",
                                                    cancelButtonText: "Hủy bỏ!",
                                                    closeOnConfirm: false,
                                                    closeOnCancel: false
                                                },
                                               function (isConfirm) {
                                                   if (isConfirm) {
                                                       var data;
                                                       //xu ly
                                                       //var inCatalogValueId = obj.reference.prevObject[0].id;
                                                       var children_ = $('#treeIncatalog').jstree('get_selected', true)[0].children;
                                                       var children_d_ = $('#treeIncatalog').jstree('get_selected', true)[0].children;
                                                       if (children_.length > 0 && children_d_.length > 0) {
                                                           var countArray = $('#treeIncatalog').jstree('get_selected', true)[0].children_d;
                                                           var data_to_send = JSON.stringify(countArray);
                                                           data = { model: JSON.parse(data_to_send) };
                                                       } else {
                                                           var countArray = $('#treeIncatalog').jstree('get_selected', true)[0].id;
                                                           var data_to_send = JSON.stringify(countArray);
                                                           data = { model: JSON.parse(data_to_send) };
                                                       }               
                                                       //call ajax
                                                       $.ajax({
                                                           url: '/IndicatorTree/Delete',
                                                           type: 'POST',
                                                           data: data,
                                                           success: function (result) {
                                                           },
                                                           traditional: true,
                                                           error: function (xhr) { },
                                                           complete: function () {
                                                               swal("Thành công!", "Xóa chỉ tiêu thành công", "success");
                                                               setTimeout(function () {
                                                                   window.location.reload();
                                                               }, 1000)
                                                           }
                                                       });

                                                   } else {
                                                       swal("Hủy bỏ", "Hủy xóa thành công!", "warning");
                                                   }
                                               });
                                            }
                                        }
                                    };
                                }
                            }
                        }).bind("loaded.jstree", function (event, data) {
                            $(this).jstree("open_all");
                        });
                    }
                });
            } else {
                that.renderTree();
            }

        },
        deleteInCataValue: function () {

            swal({
                title: "Bạn có chắc?",
                text: "Xóa chỉ tiêu này không?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Đồng ý!",
                cancelButtonText: "Hủy bỏ!",
                closeOnConfirm: false,
                closeOnCancel: false
            },
           function (isConfirm) {
               if (isConfirm) {

                   //xu ly
                   var jsTree = $('#treeIncatalog').find('ul');
                   for (var i = 0 ; i < jsTree.length; i++) {
                       var el = jsTree[i];
                       var els = $(el).find('a');
                       _.each(els, function (item, index) {
                           if ($(item).hasClass('jstree-clicked')) {
                               var closestLi = $(item).closest('li')
                               var valueID = $(closestLi).attr('id');

                               $.ajax({
                                   url: '/IndicatorTree/Delete',
                                   type: 'POST',
                                   data: { id: valueID },
                                   success: function (result) {
                                   },
                                   error: function (xhr) { },
                                   complete: function () {
                                       swal("Thành công!", "Xóa chỉ tiêu thành công", "success");
                                       setTimeout(function () {
                                           window.location.reload();
                                       }, 1000)
                                   }
                               });
                           }
                       });
                       if (i == 0) {
                           break;
                       }
                   }

               } else {
                   swal("Hủy bỏ", "Hủy xóa thành công!", "warning");
               }
           });
        },
        renderClickTree: function (e) {
            var currentTarget = e.currentTarget;
            var closestLi = $(currentTarget).closest('li')
            var valueID = $(closestLi).attr('id');

            $.ajax({
                url: '/IndicatorTree/RenderDetail',
                data: { id: valueID },
                success: function (result) {
                    $('#appendDiv').empty();
                    var template = $("#templaceDetailRight");
                    var html = $.tmpl(template, result)
                    $('#appendDiv').append(html);
                },
                error: function (xhr) { },
                complete: function () {
                }
            });
        },
        renderClickEditDivRight: function (e) {
            var jsTree = $('#treeIncatalog').find('ul');
            for (var i = 0 ; i < jsTree.length; i++) {
                var el = jsTree[i];
                var els = $(el).find('a');
                _.each(els, function (item, index) {
                    if ($(item).hasClass('jstree-clicked')) {
                        var closestLi = $(item).closest('li')
                        var valueID = $(closestLi).attr('id');
                        $.ajax({
                            url: '/IndicatorTree/Edit',
                            data: { id: valueID },
                            success: function (result) {
                                $('.btnAddsValue').removeAttr('id');
                                $('.btnAddsValue').attr('id', 'editCatalogValue');


                                var InCatalogValueCode_, InCatalogValueName_,
                                                            ParentId_, Unit_, IncataLogIds_, InCatalogValueId_,
                                                            Level_, Type_, Unit_, Description_, Active_,
                                                                Threshold_min_, Threshold_max_, AllowAggregation_, AggregationFormula_,
                                                                NumberPeriodReplace_, AllowAggregationByPeriod_;

                                IncataLogIds_ = result.InCatalogIds;
                                InCatalogValueId_ = result.InCatalogValueId;
                                InCatalogValueCode_ = result.InCatalogValueCode;
                                InCatalogValueName_ = result.InCatalogValueName;
                                Type_ = result.Type; // loại số liệu
                                Unit_ = result.Unit;// đơn vị tính
                                Description_ = result.Description; // mô tả
                                // kỳ công bố
                                Active_ = result.Active; // sử dụng
                                Threshold_min_ = result.Threshold_min; // giới hạn nhỏ nhất
                                Threshold_max_ = result.Threshold_max; // giới hạn lớn nhất
                                AllowAggregation_ = result.AllowAggregation; // cho phép tổng hợp theo địa bàn đơn vị
                                AggregationFormula_ = result.AggregationFormula; // ham tổng hợp
                                NumberPeriodReplace_ = result.NumberPeriodReplace; // so ky
                                AllowAggregationByPeriod_ = result.AllowAggregationByPeriod; // cho phep tong hop theo ky


                                if (typeof result.ParentId != "undefined") {
                                    ParentId_ = result.ParentId;
                                } else {
                                    ParentId_ = "00000000-0000-0000-0000-000000000000";
                                }

                                if (typeof result.Unit != "undefined") {
                                    Unit_ = result.Unit;
                                }
                                $('#txtCode_FIELD_IncataLogValue').val(InCatalogValueCode_);
                                $('#txtNAME_FIELD_IncataLogValue').val(InCatalogValueName_);
                                //$('') Unit
                                $('#InCatalogValueId').val(InCatalogValueId_);
                                $('#TypeSelect').select2().val(Type_).trigger("change");
                                $('#UnitSelect').select2().val(Unit_).trigger("change");
                                $('#Description').val(Description_);
                                $("#InCatalogIdReplace").select2().val(JSON.parse(IncataLogIds_)).trigger("change");
                                $("#ParentId").select2().val(ParentId_).trigger("change");
                                $("#IncataLogIds").val(IncataLogIds_);
                                $('#IsActivated_In').prop('checked', Active_);
                                $('#Threshold_min').val(Threshold_min_);
                                $('#Threshold_max').val(Threshold_max_);
                                $('#AllowAggregation').prop('checked', AllowAggregation_);
                                $('#AggregationFormula').select2().val(AggregationFormula_).trigger("change");
                                $('#NumberPeriodReplace').val(NumberPeriodReplace_);
                                $('#AllowAggregationByPeriod').prop('checked', AllowAggregationByPeriod_);
                                $('#tagFormInCatalogValue').click();

                            },
                            error: function (xhr) { },
                            complete: function () {
                            }
                        });

                    }
                });
                if (i == 0) {
                    break;
                }
            }
        },
        renderModalShow: function (e) {

        },
        renderSearchStatitic: function () {;
            var to = false;
            $('#treeIncatalog').jstree(true).close_all();
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#indicatorTree_search').val();
                if (!v) {
                    $('#treeIncatalog').jstree(true).open_all();
                }
                $('#treeIncatalog').jstree(true).search(v);
            }, 250);
        }
    });

    var appview = new AppView();
})();