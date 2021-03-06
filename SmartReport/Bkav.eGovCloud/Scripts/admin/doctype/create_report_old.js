(function () {
    window.tree_indicator = {};
    window.tree_indicator.renderIndicator = function () {
        var that = this;
        $.ajax({
            url: "/Admin/IndicatorDepartment/GetCatalog",
            data: {},
            type: 'Get',
            error: function (a, b, c) { },
            success: function (response) {
                var templateTree = '<div id="chooseIndicatorTree">Chọn chỉ tiêu</div>';
                $("#treeIndicator").html(templateTree)

                var kpis = _.map(response, function (kpi) {
                    kpi["icon"] = "icon icon-stats"
                    return kpi
                });

                $("#chooseIndicatorTree").jstree({
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
                    $("#chooseIndicatorTree").jstree(true).close_all();
                    if (to) { clearTimeout(to); }
                    to = setTimeout(function () {
                        var v = $('#inputIndicatorSearch').val();
                        if (!v) {
                            $("#chooseIndicatorTree").jstree(true).open_all();
                        }
                        $("#chooseIndicatorTree").jstree(true).search(v);
                    }, 250);
                });
            }
        });

        window.tree_indicator.loadByIndicator = function () {
            var indexIndcatorCode = null;
            var indexIndcatorUnit = null;
            var indexIndcatorName = null;

            typeTableData.forEach(function (val) {
                if (val.length > 13 + fieldRowsCount) {
                    switch (val[15 + fieldRowsCount]) {
                        case "Kỳ trước":
                            val[15 + fieldRowsCount] = "KT";
                            break;
                        case "Cùng kỳ":
                            val[15 + fieldRowsCount] = "CK";
                            break;
                        case "Tự chọn":
                            val[15 + fieldRowsCount] = "TC";
                            break;
                        default:
                            break;
                    }

                    var _dataLocalitys = JSON.parse($('#dataLocalitys').val());
                    var _dataLocalityDataIds = JSON.parse($('#dataLocalityDataIds').val());

                    var _categoryDisaggregationNames = JSON.parse($("#categoryDisaggregationNames").val());
                    var _categoryDisaggregationIds = JSON.parse($("#categoryDisaggregationIds").val());

                    _dataLocalitys.forEach(function (value, index) {
                        if (val[16 + fieldRowsCount] !== undefined) {
                            if (val[16 + fieldRowsCount] == value) {
                                val[16 + fieldRowsCount] = _dataLocalityDataIds[index];
                            }
                        }                   
                    });

                    _categoryDisaggregationNames.forEach(function (value, index) {
                        if (val[17 + fieldRowsCount] !== undefined) {
                            if (val[17 + fieldRowsCount] == value) {
                                val[17 + fieldRowsCount] = _categoryDisaggregationIds[index];
                            }
                        }
                    });

                    if (val[13 + fieldRowsCount] == "Chỉ tiêu") {
                        val[13 + fieldRowsCount] = "Indicator";
                        switch (val[14 + fieldRowsCount]) {
                            case "Mã chỉ tiêu":
                                val[14 + fieldRowsCount] = "IndicatorCode";
                                break;
                            case "Tên chỉ tiêu":
                                val[14 + fieldRowsCount] = "IndicatorName";
                                break;
                            case "Đơn vị chỉ tiêu":
                                val[14 + fieldRowsCount] = "IndicatorUnit"
                                break;
                            default:
                                break;
                        }
                    }
                    else if (val[13 + fieldRowsCount] == "Loại số liệu") {
                        val[13 + fieldRowsCount] = "TypeData"
                        var dataTypeNamesArr = JSON.parse($('#dataTypeNames').val());
                        var dataTypesArr = JSON.parse($('#dataTypes').val());
                        dataTypeNamesArr.forEach(function (value, index) {
                            if (val[14 + fieldRowsCount] == value) {
                                val[14 + fieldRowsCount] = dataTypesArr[index];
                            }
                        })
                    } else {
                    }
                }
            })

            for (var i = 0; i < typeTableData.length; i++) {
                var type = typeTableData[i][fieldRowsCount + 13];
                var typeData = typeTableData[i][fieldRowsCount + 14];
                if (type == "Indicator" && typeData == "IndicatorCode") {
                    indexIndcatorCode = i
                }
                if (type == "Indicator" && typeData == "IndicatorUnit") {
                    indexIndcatorUnit = i
                }
                if (type == "Indicator" && typeData == "IndicatorName") {
                    indexIndcatorName = i
                }
            }

            var selected = JSON.parse(localStorage.getItem("selected"));
            var data = dataTable.getData();
            if (!selected) {
                selected = {
                    row: 0,
                    col: 0
                };
            };
            var row = selected.row;
            var column = selected.col;
            var indicators = $("#chooseIndicatorTree").jstree("get_selected");
            for (var i = row; i < row + indicators.length; i++) {
                var node = $("#chooseIndicatorTree").jstree(true).get_node(indicators[i - row], false);
                if (node && node.original) {
                    data[i][indexIndcatorCode] = node.original.code;
                    data[i][indexIndcatorName] = node.original.name;
                    data[i][indexIndcatorUnit] = node.original.unit;
                }
            }
            dataTable.getInstance().loadData(data);
            dataTable.getInstance().render();
        }
    }

    $("#dataRightModel").click(function (e) {
        e.preventDefault();
        renderFunctionDataModel(true);
    });
    $("#dataBottomModel").click(function (e) {
        e.preventDefault();
        renderFunctionDataModel(false);
    });
    var renderLocalities = function () {
        $.ajax({
            type: "GET",
            url: "/Admin/Ad_Locality/GetLocalities",
            traditional: true,
            data: {},
            success: function (response) {
                var result = response;
                $("#localityModel").html('<option value="TC">Tự chọn</option>');
                var templateData = $.tmpl('<option value="${Id}">${LocalityName}</option>', result.data);
                $("#localityModel").append(templateData);
            }
        });
    }

    var renderTypeDatas = function () {
        $.ajax({
            type: "GET",
            url: '/Admin/dataType/GetDataTypes',
            traditional: true,
            data: { },
            success: function (response) {
                var result = response;

                var templateData = $.tmpl('<option value="${nameID}">${dataTypeName}</option>', result.data);
                $("#typeDataModel").html(templateData);
            }
        });
    }

    var renderDisaggregation = function () {
        $.ajax({
            type: "GET",
            url: '/Admin/CategoryDisaggregations/GetDisaggregations',
            traditional: true,
            data: {},
            success: function (response) {
                var result = response;

                var templateData = $.tmpl('<option value="${CategoryDisaggregationCode}">${CategoryDisaggregationName}</option>', result.data);
                $("#typeDisaggregation").append(templateData);
                $("#typeDisaggregation").select2({
                    dropdownAutoWidth: true,
                    width: '100%'
                });
            }
        });
    }

    renderLocalities();
    renderTypeDatas();
    renderDisaggregation();
    var renderFunctionDataModel = function (isRight) {
        var selected = JSON.parse(localStorage.getItem("selected"));
        var data = dataTable.getData();
        if (!selected) {
            selected = {
                row: 0,
                col: 0
            };
        };
        var row = selected.row;
        var column = selected.col;
        var propModel = {};
        propModel.Func = $("#functionModel").val();
        propModel.Period = $("#periodModel").val();
        
        //begin select Option
        var numberoptionSelect = $('#optionSelect').val();
        if (numberoptionSelect == 1) {
            //nam
            var numberYear = parseInt($("#numberYearKey").val() + "0101")
            propModel.PeriodData = numberYear;
        } else if (numberoptionSelect == 2) {
            // quy
            var numberYearQuarter = $("#numberQuater").val();
            if (numberYearQuarter == "1") {
                numberYearQuarter = parseInt($("#numberYearKey").val() + "0101");
                propModel.PeriodData = numberYearQuarter;
            } else if (numberYearQuarter == "2") {
                numberYearQuarter = parseInt($("#numberYearKey").val() + "0401");
                propModel.PeriodData = numberYearQuarter;
            } else if (numberYearQuarter == "3") {
                numberYearQuarter = parseInt($("#numberYearKey").val() + "0701");
                propModel.PeriodData = numberYearQuarter;
            } else if (numberYearQuarter == "4") {
                numberYearQuarter = parseInt($("#numberYearKey").val() + "1001");
                propModel.PeriodData = numberYearQuarter;
            }

        } else if (numberoptionSelect == 3) {
            //thang
            var numberYearMonth = parseInt($("#numberMonth").val());
            if (numberYearMonth < 10) {
                var str = "0" + $("#numberMonth").val() + "01";
                numberYearQuarter = parseInt($("#numberYearKey").val() + str);
                propModel.PeriodData = numberYearQuarter;
            } else {
                numberYearQuarter = parseInt($("#numberYearKey").val() + $("#numberMonth").val() + "01");
                propModel.PeriodData = numberYearQuarter;
            }
        }


        //end select OPtion

        
        propModel.PeriodKey = "monthkey";
        propModel.Locality = $("#localityModel").val();
        propModel.TypeData = $("#typeDataModel").val();
        propModel.Disaggregation = $('#typeDisaggregation').val();
        var localityKey = $("#localityModel").val();
        var period = $("#periodModel").val();
        if (period != "KTC") {
            propModel.PeriodData = "@timekey";
        }
        
        if (localityKey == "TC") {
            propModel.Locality = "@organizationkey"
        }
        var queryFunctionTemplate = propModel.Func + "('${InCatalogValueCode}', '${Locality}', ${PeriodData}, '${Locality}', '${PeriodKey}', '${Period}','${Disaggregation}', '${TypeData}')";
        var indicators = $("#chooseIndicatorTree").jstree("get_selected");

        if (isRight) {
            for (var i = column; i < column + indicators.length; i++) {
                var node = $("#chooseIndicatorTree").jstree(true).get_node(indicators[i - column], false);
                if (node && node.original) {
                    propModel.InCatalogValueCode = node.original.code;
                    var valueFunction = $.tmpl(queryFunctionTemplate, propModel)[0].wholeText;
                    data[row][i] = valueFunction;
                }
            }
        } else {
            for (var i = row; i < row + indicators.length; i++) {
                var node = $("#chooseIndicatorTree").jstree(true).get_node(indicators[i - row], false);
                if (node && node.original) {
                    propModel.InCatalogValueCode = node.original.code;
                    var valueFunction = $.tmpl(queryFunctionTemplate, propModel)[0].wholeText;
                    data[i][column] = valueFunction;
                }
            }
        }

        dataTable.getInstance().loadData(data);
        dataTable.getInstance().render();
    }

    var renderToolbar = function () {
        $("#paragraphLeft").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            bindingAlginClassCells(dataTable, selected, 'htLeft');
            dataTable.render();
        });
        $("#paragraphCenter").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            bindingAlginClassCells(dataTable, selected, 'htCenter');
            dataTable.render();
        });
        $("#paragraphRight").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            bindingAlginClassCells(dataTable, selected, 'htRight');
            dataTable.render();
        });
        $("#paragraphJustify").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            bindingAlginClassCells(dataTable, selected, 'htCenter');
            dataTable.render();
        });
        $("#makeBold").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            setClassNameByRange(dataTable, selected, 'htBold');
            dataTable.render();
        });
        $("#makeItalic").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            setClassNameByRange(dataTable, selected, 'htItalic');
            dataTable.render();
        });
        $("#makeUnderline").click(function (e) {
            e.preventDefault();
            var selected = JSON.parse(localStorage.getItem("selectedRange"));
            setClassNameByRange(dataTable, selected, 'htStrike');
            dataTable.render();
        });
        $("#mergeCell").click(function (e) {
            e.preventDefault();
        });
        $("#unMergeCell").click(function (e) {
            e.preventDefault();
        });
        $("#readOnlyCell").click(function (e) {
            e.preventDefault();
        });
    }
    renderToolbar();
    window.tree_indicator.renderIndicator();
})();