document.addEventListener("DOMContentLoaded", function () {
    //jscolor.presets.default = {
    //    format: 'hex'
    //};
    $.ajax({
        url: "./Scripts/lib/Data2.js",
        dataType: 'json',
        type: 'GET',
        success: function (response) {
            var dataClassCell = [];
            var example = document.getElementById('example');
            var headerNested = response.extra.mergedCells.length ? response.extra.mergedCells : response.headerNested;
            var colwidths = response.colWidths ? response.colWidths : 100;
            var data = response.extra.headerSetting;
            var rowHeader = data.length;
            var a = _.keys(response.header);
            //classcell = response.extra.headerSetting;
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

            _.each(response.data, function (item) {
                var temp = [];
                _.each(header, function (element) {
                    temp.push(item[element]);
                });
                data.push(temp);
            });

            for (let j = 0; j < header.length; j++)
                dataClassCell[j] = "htBold htCenter";
            for (var i = 0; i < rowHeader; i++)
                classcell.unshift(dataClassCell);

            var selectRange, selectRangeArr = [], toggleSwitch = true;
            var styleColors = [];
            for (var i = 0; i < data.length; i++) {
                var styleColor = [];
                for (var j = 0; j < header.length; j++) {
                    styleColor.unshift({ "color": '#000000', "background": '#FFFFFF', "border": '' });
                }
                styleColors.unshift(styleColor);
            }

            var settings = setting(data, headerNested, colwidths, hiddenCol, classcell, styleColors);
     
            var hot = new Handsontable(example, settings);

            hot.addHook('afterAutofill', function (start, end, Data) {
                console.log(start);
                console.log(end);
                console.log(Data);
            });

            hot.addHook('afterSelectionEnd', function (row, col, row2, col2) {
                var Crow = row;
                var Ccol = col;
                if (Crow < 0)
                    Crow = 0;
                if (Ccol < 0)
                    Ccol = 0;
                selectRangeArr = []; // Tất cả vùng đã chọn Mảng 
                var style = hot.getCell(Crow, Ccol);//td
                selectRange = hot.getSelected();
                checkClass("#btnCenter", "htCenter", style);
                checkClass("#btnRight", "htRight", style);
                checkClass("#btnLeft", "htLeft", style);
                checkClass("#btnJustify", "htJustify", style);
                checkClass("#btnItalic", "htItalic", style);
                checkClass("#btnBold", "htBold", style);
                checkClass("#btnUnderline", "htUnderline", style);

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
            hot.addHook('afterCreateRow', function (index, amount, source) {
                selectRange = hot.getSelected();
                var string = classcell[selectRange[0][0]];
                var color = styleColors[selectRange[0][0]];
                classcell.splice(index, 0, string);
                styleColors.splice(index, 0, color);
            });

            hot.addHook('afterCreateCol', function (index, amount, source) {
                selectRange = hot.getSelected();
                for (var i = 0; i < classcell.length; i++) {
                    var string = classcell[i][selectRange[0][1]];
                    var color = styleColors[i][selectRange[0][1]];
                    classcell[i].splice(index, 0, string);
                    styleColors[i].splice(index, 0, color);
                }
            });

            hot.addHook('afterRemoveCol', function (index) {
                for (var i = 0; i < classcell.length; i++) {
                    classcell[i].splice(index, 1);
                    styleColors[i].splice(index, 1);
                }
            });
            hot.addHook('afterRemoveRow', function (index) {
                classcell.splice(index, 1);
                styleColors.splice(index, 1);
            });

            $(".ColorStyle input.color").blur(function () {
                var val = $(this).val();
                var _index = $(this).index();
                $(this).css("cssText", "background:" + val + "!important;color:" + val + "!important;");
                // Xác định phương thức thay đổi kiểu 
                var changeCellStyle = function () {
                    if (_index == 0) {
                        $(rangeCell).css({ "background": val });
                        hot.setCellMeta(selectRangeArr[i].row, selectRangeArr[i].col, "bkColor", val);
                        styleColors[selectRangeArr[i].row][selectRangeArr[i].col].background = val;
                    }
                    if (_index == 1) {
                        $(rangeCell).css({ "color": val });
                        hot.setCellMeta(selectRangeArr[i].row, selectRangeArr[i].col, "ftColor", val);
                        styleColors[selectRangeArr[i].row][selectRangeArr[i].col].color = val;
                    }
                    if (_index == 2) {
                        $(rangeCell).css({ "border": "solid 1px " + val });
                        hot.setCellMeta(selectRangeArr[i].row, selectRangeArr[i].col, "bdColor", val);
                        styleColors[selectRangeArr[i].row][selectRangeArr[i].col].border = val;
                    }
                };
                for (var i = 0; i < selectRangeArr.length; i++) {
                    var rangeCell = hot.getCell(selectRangeArr[i].row, selectRangeArr[i].col);
                    var checkMergeCell = $(rangeCell).attr("rowspan");
                    if (checkMergeCell != undefined) {
                        if (toggleSwitch) {
                            changeCellStyle();
                            toggleSwitch = false;
                        } else {
                            continue;
                        }
                    } else {
                        changeCellStyle();
                    }
                }
            });

            $('#btnBold').click(function () {
                $("#btnBold").toggleClass("btn-secondary");
                setClassNameByRange(hot.getSelectedRange(), 'htBold', classcell);
                hot.render();
            });

            $('#btnItalic').click(function () {
                $("#btnItalic").toggleClass("btn-secondary");
                setClassNameByRange(hot.getSelectedRange(), 'htItalic', classcell);
                hot.render();
            });

            $('#btnUnderline').click(function () {
                $("#btnUnderline").toggleClass("btn-secondary");
                setClassNameByRange(hot.getSelectedRange(), 'htUnderline', classcell);
                hot.render();
            });

            $('#btnJustify').click(function () {
                $("#btnJustify").addClass("btn-secondary");
                $("#btnRight").removeClass("btn-secondary");
                $("#btnCenter").removeClass("btn-secondary");
                $("#btnLeft").removeClass("btn-secondary");
                setClassNameByRange2(hot.getSelectedRange(), 'htJustify', classcell);
                hot.render();
            });

            $('#btnLeft').click(function () {
                $("#btnLeft").addClass("btn-secondary");
                $("#btnRight").removeClass("btn-secondary");
                $("#btnCenter").removeClass("btn-secondary");
                $("#btnJustify").removeClass("btn-secondary");
                setClassNameByRange2(hot.getSelectedRange(), 'htLeft', classcell);
                hot.render();
            });

            $('#btnRight').click(function () {
                $("#btnRight").addClass("btn-secondary");
                $("#btnLeft").removeClass("btn-secondary");
                $("#btnCenter").removeClass("btn-secondary");
                $("#btnJustify").removeClass("btn-secondary");
                setClassNameByRange2(hot.getSelectedRange(), 'htRight', classcell);
                hot.render();
            });

            $('#btnCenter').click(function () {
                $("#btnCenter").addClass("btn-secondary");
                $("#btnLeft").removeClass("btn-secondary");
                $("#btnRight").removeClass("btn-secondary");
                $("#btnJustify").removeClass("btn-secondary");
                setClassNameByRange2(hot.getSelectedRange(), 'htCenter', classcell);
                hot.render();
            });

            $('#btnMergecells').click(function () {
                //$("#btnMergecells").toggleClass("btn-secondary");
                mergeCellss(hot, settings, hot.getSelectedRange(), classcell);
                hot.render();
            });

            $('#btnUnMergecells').click(function () {
                //$("#btnUnMergecells").toggleClass("btn-secondary");
                UnMergeCellss(hot, settings, hot.getSelectedRange(), classcell);
                hot.render();
            });
        }
    });

});

function checkClass(value, key, style) {
    if (style.className.indexOf(key) > -1) {
        $(value).addClass("btn-secondary");
    }
    else $(value).removeClass("btn-secondary");
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
            //thisthis.setCellMeta(rowIndex, columnIndex, 'className', tempClassName);
        }
    }
}

function mergeCellss( thisthis , settings, selection) {

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

function setting(data, headerNested, colwidths, hiddenCol, classCell, styleColors) {
    return {
        data: data,
        width: '100%',
        height: 500,
        colWidths: colwidths,
        //rowHeights: 23,
        rowHeaders: true,
        colHeaders: true,
        cells: function (row, col, prop) {
            var cellProperties = {};

            cellProperties.renderer = function negativeValueRenderer(instance, td, row, col, prop, value, cellProperties) {
                Handsontable.renderers.TextRenderer.apply(this, arguments);
                if (styleColors[row][col]) {
                    td.style.color = styleColors[row][col].color;
                    td.style.background = styleColors[row][col].background;
                    if (styleColors[row][col].border)
                        td.style.border = "1px solid " + styleColors[row][col].border;
                }
                td.className = classCell[row][col];
            } 
            return cellProperties;
        },
        hiddenColumns: {
            columns: hiddenCol,
            indicators: true
        },
        //fixedRowsTop: rowHeader,
        //nestedHeaders:[],
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
                'make_read_only': { name: 'Prohibit editing the selected item', },
                'alignment': {},
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
                }
            }
        },
        selectionMode: 'range',         // 'single', 'range' or 'multiple'
        mergeCells: headerNested,
        formulas: true,
        bindRowsWithHeaders: 'strict'
    };
}

