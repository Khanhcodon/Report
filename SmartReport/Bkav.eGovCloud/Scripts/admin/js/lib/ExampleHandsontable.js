/// <reference path="Data3.js" />
$(function () {
    $.ajax({
        url: "./Scripts/admin/js/lib/Data3.js",
        dataType: 'json',
        type: 'GET',
        success: function (response) {
            //var dataClassCell = [];
            var example = document.getElementById('example');
            var headerNested = response.extra.mergedCells.length ? response.extra.mergedCells : response.headerNested;
            var colwidths = response.colWidths ? response.colWidths : 100;
            var headerSetting = response.extra.headerSetting;
            //var rowHeader = headerSetting.length;
            var a = _.keys(response.header);
            var classcell = response.classCells;
            var i = 0;
            var hiddenCol = [];
            _.each(a, function (element) {
                if (response.extra.columnSetting[element]["Hidden"])
                    hiddenCol.push(i);
                i++;
            });

            var header = _.map(a, function (element) {
                return element.split("!!", 1);
            });
            var data = [];
            _.each(response.data, function (item) {
                var temp = [];
                _.each(header, function (element) {
                    temp.push(item[element]);
                });
                data.push(temp);
            });

            var selectRange, selectRangeArr = [], toggleSwitch = true, Ccell, dataAlias = [], colwidth = [], colCount = 0;
            var styleColors = [];
            for (var i = 0; i < data.length; i++) {
                var styleColor = [];
                for (var j = 0; j < header.length; j++) {
                    styleColor.unshift({ "color": '#000000', "background": '#FFFFFF', "border": '' });
                }
                styleColors.unshift(styleColor);
            }

            var settings = setting();

            var hot = new Handsontable(example, settings);
            hot.updateSettings({ width: colCount + 68 });

            for (var i = 0; i < headerSetting.length; i++) {
                var string = "";
                for (var j = 0; j < headerSetting[i].length; j++) {
                    if (j == 0 && i == 0) {
                        string = " <th style='width: 50px' rowspan='" + headerSetting.length + "'></th>";
                    }
                    if (headerSetting[i][j] != " " && headerSetting[i][j] != "") {
                        _.each(headerNested, function (element) {
                            if (element.row == i && element.col == j)
                                if (i == headerSetting.length - 1 && element.colspan)
                                    for (var k = 0; k < element.colspan - 1; k++) {
                                        if (response.extra.columnSetting[a[j]]["Hidden"])
                                            string = string + "<th class='cot" + j + "' style ='width:" + colwidth[j] + "px;' hidden>" + headerSetting[i][j] + "</th >";
                                        else string = string + "<th class='cot" + j + "' style ='width:" + colwidth[j] + "px;'>" + headerSetting[i][j] + "</th >";
                                    }
                                else {
                                    if (response.extra.columnSetting[a[j]]["Hidden"])
                                        string = string + "<th class='cot" + j + "'style ='width:" + colwidth[j] + "px;' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' hidden>" + headerSetting[i][j] + "</th >";
                                    else string = string + "<th class='cot" + j + "'style ='width:" + colwidth[j] + "px;' colspan = '" + element.colspan + "' rowspan ='" + element.rowspan + "' >" + headerSetting[i][j] + "</th >";
                                }
                        });
                        if (i == headerSetting.length - 1)
                            if (response.extra.columnSetting[a[j]]["Hidden"])
                                string = string + "<th class='cot" + j + "' style ='width:" + colwidth[j] + "px;' hidden>" + headerSetting[i][j] + "</th >";
                            else string = string + "<th class='cot" + j + "' style ='width:" + colwidth[j] + "px;'>" + headerSetting[i][j] + "</th >";
                    }
                }
                $('#head').append("<tr>" + string + "</tr>");
            }

            hot.addHook('afterAutofill', function (start, end, Data) {
                var cellMeta = hot.getSourceDataAtCell(start.row - 1, start.col);
                if (start.row != selectRange[0][0] + 1)
                    cellMeta = hot.getSourceDataAtCell(end.row + 1, start.col);
                var array1 = [], array2 = [], indexCells = [], str = [];
                for (var i = 0; i < cellMeta.length; i++) {
                    if (cellMeta[i] == ")")
                        array2.push(i);
                    else if (cellMeta[i] == "(")
                        array1.push(i + 1);
                }

                for (var i = 0; i < array1.length; i++) {
                    var str1 = cellMeta.slice(array1[i], array2[i]).replace(/[\*\+\-\/\\]/g, '@').split('@');
                    _.each(str1, function (element) {
                        str.push(element);
                    })
                }

                var dataSheet = Handsontable.helper.createSpreadsheetData(hot.countRows(), hot.countCols());
                _.each(str, function (item) {
                    for (var i = 0; i < hot.countRows(); i++)
                        for (var j = 0; j < hot.countCols(); j++) {
                            if (dataSheet[i][j] === item) {
                                var d = { row: i, col: j };
                                indexCells.push(d);
                            }
                        }
                });
                if (start.row == selectRange[0][0] + 1) {
                    for (var i = start.row; i <= end.row; i++) {
                        var cells = cellMeta, r = 0;
                        _.each(str, function (item) {
                            cells = cells.replace(item, dataSheet[indexCells[r].row + i - start.row + 1][indexCells[r].col]);
                            r++;
                        })
                        hot.setDataAtCell(i, start.col, cells);
                    }
                } else {
                    for (var i = start.row; i <= end.row; i++) {
                        var cells = cellMeta, r = 0;
                        _.each(str, function (item) {
                            cells = cells.replace(item, dataSheet[indexCells[r].row - (end.row - i + 1)][indexCells[r].col]);
                            r++;
                        })
                        hot.setDataAtCell(i, start.col, cells);
                    }
                }
            });

            hot.addHook('afterColumnResize', function (newSize, col, isDoubleClick) {

                colCount = 0;
                if (isDoubleClick) {
                    for (var i = 0; i < hot.countCols(); i++)
                        colCount += hot.getColWidth(i);
                    $('#dataTable').css({ "width": colCount + 50 });
                    $('.cot' + col).css("width", hot.getColWidth(col));
                    hot.updateSettings({ width: colCount + 68 });
                } else {
                    for (var i = 0; i < hot.countCols(); i++)
                        colCount += hot.getColWidth(i);
                    $('#dataTable').css({ "width": colCount + 50 });
                    $('.cot' + col).css("width", newSize - 1);
                    hot.updateSettings({ width: colCount + 68 });
                }

            })

            hot.addHook('afterSelectionEnd', function (row, col, row2, col2) {
                var Crow = row;
                var Ccol = col;
                if (Crow < 0)
                    Crow = 0;
                if (Ccol < 0)
                    Ccol = 0;
                Ccell = hot.getCell(Crow, Ccol);
                selectRangeArr = []; // Tất cả vùng đã chọn Mảng 
                selectRange = hot.getSelected();
                checkClass("#btnCenter", "htCenter", Ccell);
                checkClass("#btnRight", "htRight", Ccell);
                checkClass("#btnLeft", "htLeft", Ccell);
                checkClass("#btnJustify", "htJustify", Ccell);
                checkClass("#btnItalic", "htItalic", Ccell);
                checkClass("#btnBold", "htBold", Ccell);
                checkClass("#btnUnderline", "htUnderline", Ccell);

                var rangeRowArr = []; // hàng đã chọn của vùng mảng
                var rangeColArr = []; // the Mảng cột phạm vi lựa chọn         
                for (var i = Crow; i < selectRange[0][2] + 1; i++) {
                    rangeRowArr.push(i);
                }
                for (var i = Ccol; i < selectRange[0][3] + 1; i++) {
                    rangeColArr.push(i);
                }
                for (var i = 0; i < rangeRowArr.length; i++) {
                    for (var n = 0; n < rangeColArr.length; n++) {
                        var selectRangeCell = { row: rangeRowArr[i], col: rangeColArr[n] };
                        selectRangeArr.push(selectRangeCell);
                    }
                }
            });

            hot.addHook('beforeCreateRow', function (index, amount, source) {
                var string = classcell[selectRange[0][0]];
                var color = styleColors[selectRange[0][0]];
                classcell.splice(index, 0, string);
                styleColors.splice(index, 0, color);
                if (source === "ContextMenu.rowAbove") {
                    for (var i = 0; i < hot.countCols(); i++) {
                        var cellMeta = hot.getCellMeta(selectRange[0][0], i);
                        hot.setCellMeta(index, i, "alias", cellMeta.alias);
                    }
                } else {
                    for (var i = 0; i < header.length; i++) {
                        var cellMeta = hot.getCellMeta(selectRange[0][0], i);
                        hot.setCellMeta(index, i, "alias", cellMeta.alias);
                    }
                }

            });

            hot.addHook('beforeCreateCol', function (index, amount, source) {
                for (var i = 0; i < hot.countRows(); i++) {
                    var string = classcell[i][selectRange[0][1]];
                    var color = styleColors[i][selectRange[0][1]];
                    classcell[i].splice(index, 0, string);
                    styleColors[i].splice(index, 0, color);
                }
            });

            hot.addHook('beforeRemoveCol', function (index) {
                for (var i = 0; i < classcell.length; i++) {
                    classcell[i].splice(index, 1);
                    styleColors[i].splice(index, 1);
                }
            });

            hot.addHook('beforeRemoveRow', function (index) {
                classcell.splice(index, 1);
                styleColors.splice(index, 1);
            });

            hot.addHook('afterOnCellMouseDown', function (event, coords) {
                if (event.buttons == 1) {
                    if (coords.row < 0 || coords.col < 0)
                        return;
                    var cellMeta = hot.getCellMeta(coords.row, coords.col);
                    if (cellMeta.alias != null && cellMeta.alias != undefined) {
                        if ($(Ccell) != undefined) {
                            AliasDialog(cellMeta.alias, coords.row, coords.col);
                        } else {
                            alert("Please select the cell first...");
                        }
                    }
                }
            });

            layui.use('colorpicker', function () {
                var colorpicker = layui.colorpicker;
                colorpicker.render({
                    elem: '#mauNen'
                    , size: 'lg'
                    , color: ''
                    , predefine: true
                    , done: function (color) {
                        changeColor(color, 0);
                    }
                });

                colorpicker.render({
                    elem: '#mauChu'
                    , size: 'lg'
                    , color: '#000000'
                    , predefine: true
                    , done: function (color) {
                        changeColor(color, 1);
                    }
                });

                colorpicker.render({
                    elem: '#mauVien'
                    , size: 'lg'
                    , color: ''
                    , predefine: true
                    , done: function (color) {
                        changeColor(color, 2);
                    }
                });
            });

            function changeColor(val, _index) {
                // Xác định phương thức thay đổi kiểu 
                var changeCellStyle = function () {
                    if (_index == 0) {
                        $(rangeCell).css({ "background": val });
                        styleColors[selectRangeArr[i].row][selectRangeArr[i].col].background = val;
                    }
                    if (_index == 1) {
                        $(rangeCell).css({ "color": val });
                        styleColors[selectRangeArr[i].row][selectRangeArr[i].col].color = val;
                    }
                    if (_index == 2) {
                        $(rangeCell).css({ "border": "solid 1px " + val });
                        styleColors[selectRangeArr[i].row][selectRangeArr[i].col].border = val;
                    }
                };
                for (var i = 0; i < selectRangeArr.length; i++) {
                    var rangeCell = hot.getCell(selectRangeArr[i].row, selectRangeArr[i].col);
                    var checkMergeCell = $(rangeCell).attr("rowspan");
                    if (checkMergeCell != undefined) {
                        if (toggleSwitch) {
                            changeCellStyle();
                        } else {
                            continue;
                        }
                    } else {
                        changeCellStyle();
                    }
                }
            };

            $('#btnBold').click(function () {
                $('#btnBold').toggleClass("btn-default");
                setClassNameByRange(hot.getSelectedRange(), 'htBold', classcell);
                hot.render();
            });

            $('#btnItalic').click(function () {
                $("#btnItalic").toggleClass("btn-default");
                setClassNameByRange(hot.getSelectedRange(), 'htItalic', classcell);
                hot.render();
            });

            $('#btnUnderline').click(function () {
                $("#btnUnderline").toggleClass("btn-default");
                setClassNameByRange(hot.getSelectedRange(), 'htUnderline', classcell);
                hot.render();
            });

            $('#btnJustify').click(function () {
                $("#btnJustify").toggleClass("btn-default");
                $("#btnRight").addClass("btn-default");
                $("#btnCenter").addClass("btn-default");
                $("#btnLeft").addClass("btn-default");
                setClassNameByRange2(hot.getSelectedRange(), 'htJustify', classcell);
                hot.render();
            });

            $('#btnLeft').click(function () {
                $("#btnLeft").toggleClass("btn-default");
                $("#btnRight").addClass("btn-default");
                $("#btnCenter").addClass("btn-default");
                $("#btnJustify").addClass("btn-default");
                setClassNameByRange2(hot.getSelectedRange(), 'htLeft', classcell);
                hot.render();
            });

            $('#btnRight').click(function () {
                $("#btnRight").toggleClass("btn-default");
                $("#btnLeft").addClass("btn-default");
                $("#btnCenter").addClass("btn-default");
                $("#btnJustify").addClass("btn-default");
                setClassNameByRange2(hot.getSelectedRange(), 'htRight', classcell);
                hot.render();
            });

            $('#btnCenter').click(function () {
                $("#btnCenter").toggleClass("btn-default");
                $("#btnLeft").addClass("btn-default");
                $("#btnRight").addClass("btn-default");
                $("#btnJustify").addClass("btn-default");
                setClassNameByRange2(hot.getSelectedRange(), 'htCenter', classcell);
                hot.render();
            });

            $('#btnMergecells').click(function () {
                //$("#btnMergecells").toggleClass("btn-default");
                mergeCellss(hot, settings, hot.getSelectedRange(), classcell);
                hot.render();
            });

            $('#btnUnMergecells').click(function () {
                //$("#btnUnMergecells").toggleClass("btn-default");
                UnMergeCellss(hot, settings, hot.getSelectedRange(), classcell);
                hot.render();
            });

            function addAliasDialog() {
                var html = '<div class="alias" style="text-align:center;margin-top:20px;"><label>Mời nhâp tên alias:<input type="text" id="aliasVal" /></label></div>';
                layer.open({
                    type: 1,
                    btn: ['Confirm', 'Cancel'],
                    skin: 'layui-layer-molv',
                    title: "Set alias",
                    area: [' 420px', '240px'],  //nội dung chiều rộng và chiều cao  
                    content: html,
                    yes: function (index, layero) {
                        var val = $("#aliasVal").val();
                        //var check = _.filter(_.keys(dataAlias), function (item) {
                        //    if (item == val)
                        //        return true;
                        //});
                        //if (!check.length) {
                        if (val != "" && val != null) {
                            for (var i = 0; i < selectRangeArr.length; i++) {
                                var cellMeta = hot.getCellMeta(selectRangeArr[i].row, selectRangeArr[i].col);
                                val = $("#aliasVal").val();
                                if (cellMeta.alias != null && cellMeta.alias != undefined)
                                    val = val + " " + cellMeta.alias;
                                hot.setCellMeta(selectRangeArr[i].row, selectRangeArr[i].col, "alias", val);

                            };

                            dataAlias[val] = [];
                            for (var row = 0; row < hot.countRows(); row++) {
                                var dataAlia = [];
                                for (var col = 0; col < hot.countCols(); col++) {
                                    var cellMeta = hot.getCellMeta(row, col);
                                    if (cellMeta.alias != undefined && cellMeta.alias.indexOf(val) > -1 && hot.getDataAtCell(row, col)) {
                                        dataAlia.push(hot.getDataAtCell(row, col));
                                    }
                                }

                                for (var i = 0; i < dataAlias[val].length; i++) {
                                    if (dataAlias[val][i].head == dataAlia[0]) {
                                        dataAlia = [];
                                        break;
                                    }
                                };

                                if (dataAlia.length > 0) {
                                    dataAlias[val].push({ 'head': dataAlia, 'done': false });
                                }
                            }
                            layer.msg('Set successfully', {
                                icon: 1,
                                time: 1000 // Đóng sau 1 giây (nếu chưa cấu hình, mặc định là 3 giây) 
                            }, function () {
                                layer.close(index);
                            });
                        } else {
                            alert("The alias cannot be empty!");
                        }
                    },
                    cancel: function (index, layero) {
                        layer.close(index);
                    }, btn2: function (index, layero) {
                        layer.confirm('Are you sure to cancel the alias setting? ', { icon: 3, title: 'Thông báo', btn: ['Yes', 'No'] }, function (index) {
                            layer.close(index);
                        }, function (index) {
                            addAliasDialog();
                        });
                    }
                });
            };

            function AliasDialog(val, Row, Col) {
                var html = '<div class="alias" style="text-align:center;margin-top:20px;"><div id="example2" class="head-gap handsontable htRowHeaders htColumnHeaders" style="height: 272px; overflow: hidden; width: 650px;" data-originalstyle="height: 272px; overflow: hidden; width: 650px;"></div></div>';
                layer.open({
                    type: 1,
                    btn: ['Confirm', 'Cancel'],
                    title: val,
                    skin: 'layui-layer-molv',
                    area: [' 420px', '400px'],  //nội dung chiều rộng và chiều cao  
                    content: html,
                    success: function (index, layero) {
                        var dataCell = hot.getDataAtCell(Row, Col);
                        var example1 = document.getElementById('example2');
                        if (val != "" && val != null) {
                            _.each(dataAlias[val], function (item) {
                                if (item.head == dataCell)
                                    item.done = true;
                                else item.done = false;
                            });

                            hot2 = new Handsontable(example1, {
                                data: dataAlias[val],
                                width: '100%',
                                rowHeaders: false,
                                colHeaders: false,
                                columns: [
                                    { data: 'head', type: 'text' },
                                    { data: 'done', type: 'checkbox' }
                                ],
                                outsideClickDeselects: false,
                                licenseKey: 'non-commercial-and-evaluation'
                            });
                            hot2.addHook('afterOnCellMouseDown', function (event, coords) {
                                if (event.buttons == 1) {
                                    if (coords.col == 1) {
                                        dataCell = hot2.getDataAtCell(coords.row, coords.col);
                                        if (coords.row == 0) {
                                            for (var i = 0; i < dataAlias[val].length; i++) {
                                                dataAlias[val][i].done = !dataCell;
                                                hot2.setDataAtCell(i, coords.col, !dataCell);
                                            }
                                        } else {
                                            for (var i = 0; i < dataAlias[val].length; i++) {
                                                if (dataAlias[val][i].done) {
                                                    dataAlias[val][i].done = false;
                                                    hot2.setDataAtCell(i, coords.col, false);
                                                }
                                            }
                                            dataAlias[val][coords.row].done = !dataCell;
                                            hot2.setDataAtCell(coords.row, coords.col, !dataCell);
                                        }
                                    }
                                }
                            });
                        }
                        else {
                            alert("The alias cannot be empty!");
                        }
                    },
                    yes: function (index, layero) {
                        var dataCell = _.filter(dataAlias[val], function (item) {
                            return item.done == true;
                        });
                        if (!dataCell.length)
                            for (var j = 0; j < header.length; j++)
                                hot.setDataAtCell(Row, j, '');
                        else {
                            for (var i = 0; i < data.length; i++) {
                                if (data[i][Col] == dataCell[0].head[0]) {
                                    for (var j = 0; j < header.length; j++)
                                        hot.setDataAtCell(Row, j, data[i][j]);
                                    break;
                                }
                            }
                        }
                        layer.msg('Get successfully', {
                            icon: 1,
                            time: 1000 // Đóng sau 1 giây (nếu chưa cấu hình, mặc định là 3 giây) 
                        }, function () { layer.close(index) })
                    },
                    btn2: function (index, layero) {
                        layer.confirm('Are you sure to cancel the alias setting? ', { icon: 3, title: 'Prompt', btn: ['Yes', 'No'] }, function (index) {
                            layer.close(index);
                        }, function (index) {
                            AliasDialog(val, Row);
                        });
                    }
                });
            };

            function setting() {
                return {
                    data: data,
                    afterLoadData: function () {
                        for (var i = 0; i < this.countCols(); i++) {
                            colwidth.push(this.getColWidth(i));
                            colCount += this.getColWidth(i);
                        }
                        $('#dataTable').css({ "width": colCount + 50 });
                    },
                    width: '100%',
                    //height: 500,
                    colWidths: colwidths,
                    stretchH: 'all',
                    rowHeaders: true,
                    colHeaders: true,
                    cells: function (row, col, prop) {
                        var cellProperties = {};
                        cellProperties.renderer = function negativeValueRenderer(instance, td, row, col, prop, value, cellProperties) {
                            Handsontable.renderers.TextRenderer.apply(this, arguments);
                            if (styleColors[row][col]) {
                                td.style.color = styleColors[row][col].color;
                                td.style.background = styleColors[row][col].background;
                                if (styleColors[row][col].border) {
                                    td.style.border = "1px solid " + styleColors[row][col].border;
                                }
                            }
                            if (classcell)
                                td.className = classcell[row][col];
                        }
                        return cellProperties;
                    },
                    hiddenColumns: {
                        columns: hiddenCol,
                        indicators: true
                    },
                    outsideClickDeselects: false,
                    contextMenu: {
                        items: {
                            'row_above': { name: 'Thêm một hàng ở trên', },
                            'row_below': { name: 'Thêm một hàng ở dưới', },
                            'col_left': { name: 'Thêm một cột vào bên trái', },
                            'col_right': { name: 'Thêm một cột vào bên phải', },
                            'remove_row': { name: 'Xóa hàng này', },
                            'remove_col': { name: 'Xóa cột này', },
                            'copy': { name: 'copy', },
                            'cut': { name: 'cut', },
                            'undo': { name: 'Hoàn tác', },
                            'redo': { name: 'Cải tạo', },
                            'setAlias': {
                                name: 'Set alias',
                                callback: function () {
                                    if ($(Ccell) != undefined) {
                                        addAliasDialog();
                                    } else {
                                        alert("Please select the cell first...");
                                    }
                                }
                            },
                        }
                    },
                    selectionMode: 'range',         // 'single', 'range' or 'multiple'
                    mergeCells: [],//headerNested,
                    formulas: true,
                    bindRowsWithHeaders: 'strict',
                    manualColumnResize: true,
                    manualRowResize: true,
                    licenseKey: 'non-commercial-and-evaluation'
                };
            }
        }
    });
});

function checkClass(value, key, style) {
    if (style != null) {
        if (style.className.indexOf(key) > -1) {
            $(value).removeClass("btn-default");
        }
        else {
            $(value).addClass("btn-default");
        }
    }
}

function setClassNameByRange(selection, className, classcell) {

    var startRow = Math.min(selection[0].from.row, selection[0].to.row);
    var endRow = Math.max(selection[0].from.row, selection[0].to.row);
    var startCol = Math.min(selection[0].from.col, selection[0].to.col);
    var endCol = Math.max(selection[0].from.col, selection[0].to.col);
    startRow = check(startRow);
    startCol = check(startCol);

    for (var rowIndex = startRow; rowIndex <= endRow; rowIndex++) {
        for (var columnIndex = startCol; columnIndex <= endCol; columnIndex++) {
            var cellMeta = classcell[rowIndex][columnIndex];
            tempClassName = '';
            if (!cellMeta || (cellMeta && cellMeta.indexOf(className) < 0)) {
                tempClassName = cellMeta + " " + className;
            } else {
                var replace = className;
                var re = new RegExp(replace, "g");

                tempClassName = cellMeta.replace(re, "");
            }

            classcell[rowIndex][columnIndex] = tempClassName;
            //thisthis.setCellMeta(rowIndex, columnIndex, 'className', tempClassName);
        }
    }
}

function setClassNameByRange2(selection, className, classcell) {

    var startRow = Math.min(selection[0].from.row, selection[0].to.row);
    var endRow = Math.max(selection[0].from.row, selection[0].to.row);
    var startCol = Math.min(selection[0].from.col, selection[0].to.col);
    var endCol = Math.max(selection[0].from.col, selection[0].to.col);
    startRow = check(startRow);
    startCol = check(startCol);

    for (var rowIndex = startRow; rowIndex <= endRow; rowIndex++) {
        for (var columnIndex = startCol; columnIndex <= endCol; columnIndex++) {
            var cellMeta = classcell[rowIndex][columnIndex];
            tempClassName = '';
            if (!cellMeta)
                tempClassName = className;
            else if (cellMeta.indexOf('htJustify') >= 0)
                tempClassName = cellMeta.replace('htJustify', className);
            else if (cellMeta.indexOf('htRight') >= 0)
                tempClassName = cellMeta.replace('htRight', className);
            else if (cellMeta.indexOf('htCenter') >= 0)
                tempClassName = cellMeta.replace('htCenter', className);
            else if (cellMeta.indexOf('htLeft') >= 0)
                tempClassName = cellMeta.replace('htLeft', className);
            else tempClassName = cellMeta + " " + className;

            classcell[rowIndex][columnIndex] = tempClassName;
        }
    }
}

function mergeCellss(thisthis, settings, selection) {

    var startRow = Math.min(selection[0].from.row, selection[0].to.row);
    var endRow = Math.max(selection[0].from.row, selection[0].to.row);
    var startCol = Math.min(selection[0].from.col, selection[0].to.col);
    var endCol = Math.max(selection[0].from.col, selection[0].to.col);
    startRow = check(startRow);
    startCol = check(startCol);

    var valueAdd = { row: startRow, col: startCol, rowspan: endRow - startRow + 1, colspan: endCol - startCol + 1, removed: false };
    if (checkItem(settings.mergeCells, valueAdd) == -1) {
        settings.mergeCells.push(valueAdd);
        thisthis.updateSettings({ mergeCells: settings.mergeCells });
    }
}

function UnMergeCellss(thisthis, settings, selection) {
    var startRow = Math.min(selection[0].from.row, selection[0].to.row);
    var endRow = Math.max(selection[0].from.row, selection[0].to.row);
    var startCol = Math.min(selection[0].from.col, selection[0].to.col);
    var endCol = Math.max(selection[0].from.col, selection[0].to.col);
    startRow = check(startRow);
    startCol = check(startCol);

    var valueRemove = { row: startRow, col: startCol, rowspan: endRow - startRow + 1, colspan: endCol - startCol + 1, removed: false };
    if (checkItem(settings.mergeCells, valueRemove) > -1) {
        settings.mergeCells.splice(checkItem(settings.mergeCells, valueRemove), 1);
        thisthis.updateSettings({ mergeCells: settings.mergeCells });
    }

}

function check(number) {
    if (number < 0)
        return 0;
    return number;
}

function checkItem(arr, key) {
    for (var i = 0; i < arr.length; i++)
        if (_.isEqual(arr[i], key))
            return i;
    return - 1;
}
