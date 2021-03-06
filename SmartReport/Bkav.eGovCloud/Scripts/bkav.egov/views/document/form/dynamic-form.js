// 20191106 VuHQ START
// Generate Field Table
var fieldContainer = document.getElementById('fieldTable');
var typeContainer = document.getElementById('typeTable');
var dataContainer = document.getElementById('dataTable');
var indexDataContainer = document.getElementById('indexDataTable');

var fieldTable = undefined
var typeTable = undefined
var dataTable = undefined
var indexDataTable = undefined;

var fieldColumsSetting = []
var typeColumsSetting = []
var dataColumsSetting = []

var fieldTableColWidths = [];

var fieldTableData = []
var typeTableData = []
var dataTableData = []
var isRenderDone = false

var fieldTableMergedCells = []

var dataTableReadOnlys = [];
var dataTableMergedCells = true;
var dataTableClassCells = [];

var chiTieuSources = [];
var isEdit = false;
var countCols = 0;
var countExtraCols = 0;xoayHeaders

// 20191225 VuHQ START Cù Trọng Xoay
var gia_tri_prop = null;
var isXoay = false;
var xoayHeaders = [];
// 20191225 VuHQ END Cù Trọng Xoay

// 20200106 VuHQ START cache catalog detail/ default
var catalogDetails = [];
// 20200106 VuHQ END cache catalog detail/ default

var defineFieldJson = null;
var defineConfigJson = null;
var defineValueJson = null;
var fieldRowsCount = 0;
var fieldColumnsCount = 0;
var isNeedReload = true;
var newColumns = [];

function loadData() {
    defineFieldJson = $('#DefineFieldJson').val() == undefined ? $('#Form_DefineFieldJson').val() : $('#DefineFieldJson').val();
    if (defineFieldJson !== '' && defineFieldJson != undefined) {
        fieldTableData = JSON.parse(defineFieldJson)["data"];
        //fieldColumsSetting = JSON.parse(defineFieldJson)["columns"];
        fieldTableMergedCells = JSON.parse(defineFieldJson)["mergedCells"];
        fieldTableColWidths = JSON.parse(defineFieldJson)["colWidths"];
        countCols = JSON.parse(defineFieldJson)["countCols"];

        isEdit = true;
    }
    else {
        fieldTableData = [['', '', '']]
    }

    if (isEdit)
        $('#btnAddRowField').addClass("hidden");
    else
        $('#btnAddRowField').removeClass("hidden");

    defineConfigJson = $('#DefineConfigJson').val() == undefined ? $('#Form_DefineConfigJson').val() : $('#DefineConfigJson').val();
    if (defineConfigJson !== '' && defineConfigJson != undefined) {
        typeTableData = JSON.parse(defineConfigJson)["data"];
        typeColumsSetting = JSON.parse(defineConfigJson)["columns"];
        if (JSON.parse(defineConfigJson)["catalogDetails"] != undefined)
            catalogDetails = JSON.parse(defineConfigJson)["catalogDetails"];
    }

    defineValueJson = $('#DefineValueJson').val() == undefined ? $('#Form_DefineValueJson').val() : $('#DefineValueJson').val();
    if (defineValueJson !== '' && defineValueJson != undefined) {
        dataTableData = JSON.parse(defineValueJson)["data"];
        dataColumsSetting = JSON.parse(defineValueJson)["columns"];
        if (JSON.parse(defineValueJson)["readOnlys"] != undefined)
            dataTableReadOnlys = JSON.parse(defineValueJson)["readOnlys"];
        if (JSON.parse(defineValueJson)["mergedCells"] != undefined)
            dataTableMergedCells = JSON.parse(defineValueJson)["mergedCells"];
        if (JSON.parse(defineValueJson)["classCells"] != undefined)
            dataTableClassCells = JSON.parse(defineValueJson)["classCells"];
        if (JSON.parse(defineValueJson)["chiTieuSources"] != undefined)
            chiTieuSources = JSON.parse(defineValueJson)["chiTieuSources"];
    }

    fieldRowsCount = fieldTableData.length;
    fieldColumnsCount = fieldTableData[0].length;
}

loadData();

// Cấu hình form động ở Biểu mẫu - Quản lý mẫu báo cáo & Danh mục - Loại báo cáo
var fieldTableValidator = function (value, callback) {
    var isValid = true
    if (value == '' || value == ' ')
        isValid = false
    callback(isValid);
};

function init() {
    loadData();
    isNeedReload = true;
}
function _init() {
    //if (typeof fieldTable == "undefined") {
    if (isNeedReload) {
        generateFieldTable();
        if (defineConfigJson !== '') {
            btnSaveFieldConfig(true);
        }
        isNeedReload = false;
    }
}

$('#defaultModal').on('shown.bs.modal', function(e) {
    _init();
});

//#region Event Handler
function generateFieldTable() {
    for (var i = 0; i < fieldColumnsCount; i++) {
        fieldColumsSetting.push({
            validator: fieldTableValidator,
            allowInvalid: true
        })
    }

    var fieldTableSetting = {
        data: fieldTableData,
        columns: fieldColumsSetting,
        colWidths: fieldTableColWidths,
        rowHeaders: false,
        colHeaders: true,
        filters: true,
        dropdownMenu: false,
        autoColumnSize: true,
        manualColumnResize: true,
        contextMenu: {
            items: {
                "mergeCells": {
                    name: 'Mergel Cells',
                    callback: function (key, options) {
                        if (options[0].end.col < countCols) return;
                        var cellMerg = {
                            row: options[0].start.row,
                            col: options[0].start.col,
                            rowspan: options[0].end.row - options[0].start.row + 1,
                            colspan: options[0].end.col - options[0].start.col + 1,
                            removed: false
                        }
                        var abc = fieldTable.getPlugin('mergeCells').mergedCellsCollection.mergedCells;
                        fieldTableMergedCells = [];
                        $.each(abc, function (index, item) {
                            fieldTableMergedCells.push(item);
                        });
                        //fieldTableMergedCells = fieldTable.getPlugin('mergeCells').mergedCellsCollection.mergedCells;
                        fieldTableMergedCells.push(cellMerg);
                        fieldTable.updateSettings({
                            mergeCells: fieldTableMergedCells
                        });
                    }
                },
                "unMergeCells": {
                    name: 'UnMergel Cells',
                    callback: function (key, options) {
                        if (options[0].end.col == options[0].start.col && options[0].end.row == options[0].start.row) return;
                        var cellMerg = {
                            row: options[0].start.row,
                            col: options[0].start.col,
                            rowspan: options[0].end.row - options[0].start.row + 1,
                            colspan: options[0].end.col - options[0].start.col + 1,
                            removed: false
                        }
                        var tempMergedCell = fieldTable.getPlugin('mergeCells').mergedCellsCollection.mergedCells;
                        if (tempMergedCell.length > 0) {
                            tempMergedCell = jQuery.grep(tempMergedCell, function (value) {
                                return JSON.stringify(value) != JSON.stringify(cellMerg);
                            });
                            fieldTable.updateSettings({
                                mergeCells: tempMergedCell
                            });
                        }
                    }
                },
                "Add Left Column": {
                    name: 'Add Left Column',
                    callback: function (key, options) {
                        AddColumn(options, true);
                    }
                },
                "Add Right Column": {
                    name: 'Add Right Column',
                    callback: function (key, options) {
                        AddColumn(options, false);
                    }
                }
            }
        },
        allowRemoveColumn: true,
        className: "htMiddle htLeft htBold",
        licenseKey: 'non-commercial-and-evaluation',
        mergeCells: fieldTableMergedCells
    }

    fieldTable = new Handsontable(fieldContainer, fieldTableSetting);

    fieldTable.validateCells();

    fieldTable.addHook('beforePaste', function (changes, coords) {
        // Nếu là edit thì không cho paste dữ liệu mới ( phá vỡ cấu trúc hiện tại)
        if (isEdit) return false;

        fieldColumsSetting = []
        fieldTableData = [];

        // remove null row
        changes.filter(function (arr) {
            var isNull = true;
            arr.filter(function (subArr) {
                if (subArr != null)
                    isNull = false;
            });
            if (!isNull)
                fieldTableData.push(arr);
        });

        calculateMergeCells(fieldTableData);

        for (var i = 0; i < fieldTableData[0].length; i++) {
            fieldColumsSetting.push({
                validator: fieldTableValidator,
                allowInvalid: true
            })
        }

        fieldRowsCount = fieldTableData.length;
        fieldColumnsCount = fieldTableData[0].length;

        fieldTable.updateSettings({
            data: fieldTableData,
            columns: fieldColumsSetting,
            mergeCells: fieldTableMergedCells
        });

        fieldTable.validateCells();
        return (false)
    })

    fieldTable.render();
}

function AddColumn(options, isLeft) {
    if (options.length > 0) {
        var dataBefore = fieldTable.getData();
        var getCol = fieldTable.getSelectedLast()[1];
        var getColSet = fieldColumsSetting.length;


        fieldColumsSetting.push({
            validator: fieldTableValidator,
            allowInvalid: true,
            allowEmpty: false
        });

        if (isLeft) {
            arraymove(fieldColumsSetting, getColSet, getCol);
            newColumns.push(getCol);
        }
        else {
            arraymove(fieldColumsSetting, getColSet, getCol + 1);
            newColumns.push(getCol + 1);
        }


        dataBefore.forEach((row) => {
            row.push(null);
            if (isLeft)
                arraymove(row, getColSet, getCol);
            else
                arraymove(row, getColSet, getCol + 1);

        });

        // lấy mergeCell của column click để clone cho column được add
        var newColumnMergedCell = [];
        fieldTableMergedCells.forEach((mergedCell) => {
            if (mergedCell.col == getCol) {
                newColumnMergedCell.push(mergedCell);
                if (isLeft)
                    dataBefore[mergedCell.row][mergedCell.col] = '';
                else
                    dataBefore[mergedCell.row][mergedCell.col + 1] = '';
            }
        });

        fieldTable.updateSettings({
            data: dataBefore,
            columns: fieldColumsSetting
        });

        newColumnMergedCell = JSON.parse(JSON.stringify(newColumnMergedCell));

        // tăng +1 với những cột phía sau cột add thêm
        fieldTableMergedCells.forEach((mergedCell) => {
            if (mergedCell.col >= getCol)
                mergedCell.col += 1;
        });

        // nếu cột dc add lại ở trong 1 phạm vi col được merge
        fieldTableMergedCells.forEach((mergedCell) => {
            if (getCol > mergedCell.col && getCol < mergedCell.col + mergedCell.colspan)
                mergedCell.colspan += 1;
        });

        if (newColumnMergedCell.length > 0)
            fieldTableMergedCells.push(newColumnMergedCell[0]);
        fieldTable.updateSettings({
            mergeCells: fieldTableMergedCells
        })
    }
}


function btnSaveFieldConfig(isOnLoad) {
    // Nếu field Table vẫn còn cell đỏ thì không generate setting table
    fieldTable.validateCells(function (valid) {
        if (!valid && !isOnLoad) {
            if (typeTable != undefined && !isOnLoad)
                typeTable.destroy();
        }
        else {
            if (isOnLoad) {
                generateTypeTable(isOnLoad);
                if (defineValueJson !== '') {
                    btnSaveTypeConfig(true);
                }
            }
            else {
                if (isEdit) {
                    addRowType();
                    updateTypeTableData();
                }
                else {
                    generateTypeTable(isOnLoad);
                    if (defineValueJson !== '') {
                        dataTableClassCells = [];
                        btnSaveTypeConfig(true);
                    }
                }
            }
        }
    })
}

function btnSaveTypeConfig(isOnLoad) {
    if (typeTable == undefined || fieldTableData == undefined)
        return;

    var fieldRowsCount = fieldTableData.length;
    var fieldColumnsCount = fieldTableData[0].length;
    typeTableData = typeTable.getData();
    fieldTableData = fieldTable.getData();

    isRenderDone = false;

    var isEmpty = document.getElementById('dataTable').innerHTML === " "

    if (!isEmpty && dataTable != undefined) {
        document.getElementById('dataTable').innerHTML = "";
        //dataTable.destroy()
    }

    var nestedHeaders = fieldTableData;

    // 20191225 VuHQ START Cù Trọng Xoay
    // cùng nhào nặn lại header cho table 3 (dataTable) nào
    var nestedHeadersTemp = [];
    var nestedHeader = [];
    var catalogValues = [];
    var gia_tri_data = null;
    var dataColumnsCount = 0;

    // set default
    gia_tri_prop = {
        source: '', type: 'text', default: 0, data: '',
        index_catalog: null, index_gia_tri: null,
        readOnly: false, required: true,
        TypeName: '',
        CatalogType: '',
        CatalogValues: []
    };
    isXoay = false;

    if (fieldRowsCount == 1) {
        $.each(typeTableData, function (index, item) {
            if (item[fieldRowsCount] == 'Catalog' && item[fieldRowsCount + 1] != '' && item[fieldRowsCount + 8] && item[fieldRowsCount + 9] != '') {
                isXoay = true;
                var value_after = item[fieldRowsCount + 1];
                gia_tri_prop.data = item[fieldRowsCount + 9];
                gia_tri_prop.required = item[fieldRowsCount + 3];
                gia_tri_prop.readOnly = item[fieldRowsCount + 4];
                gia_tri_prop.CatalogType = item[fieldRowsCount + 1];

                gia_tri_prop.index_catalog = index;
                return false;
            }
        });

        if (isXoay) {
            $.each(typeTableData, function (index, item) {
                if (gia_tri_prop.index_gia_tri == index) {
               
                }
                else if (item[fieldRowsCount - 1] == gia_tri_prop.data) {
                    gia_tri_prop.type = item[fieldRowsCount] == "Kí tự" ? "text" : "numeric";
                    gia_tri_prop.TypeName = item[fieldRowsCount];
                    gia_tri_prop.default = item[fieldRowsCount + 2];
                    gia_tri_prop.index_gia_tri = index;
                }
                else {
                }
            });
        }
    }

    dataColumnsCount = fieldColumnsCount;
    if (!isOnLoad) {
        dataTableData = [];
        dataColumsSetting = [];
        for (var i = 0; i < dataColumnsCount; i++) {
            dataColumsSetting.push({
                validator: dataTableValidator,
                allowInvalid: true
            })
        }
    }
    // 20191225 VuHQ END Cù Trọng Xoay

    for (i = 0; i < fieldRowsCount; i++)
        for (j = 0; j < fieldColumnsCount; j++)
            if (nestedHeaders[i][j] == null)
                nestedHeaders[i][j] = '';

    var table1_col_size = [];
    var width = 0;

    for (var i = 0; i < fieldTable.countCols(); i++) {
        width = fieldTable.getColWidth(i);
        table1_col_size.push(width);
    }

    dataTable = new Handsontable(dataContainer, {
        data: dataTableData,
        colHeaders: true,
        rowHeaders: true,
        columns: dataColumsSetting,
        nestedHeaders: nestedHeaders,
        filters: true,
        dropdownMenu: false,
        className: "htLeft",   
        colWidths: table1_col_size,
        //columnHeaderHeight: 40,
        manualColumnResize: true,
        viewportColumnRenderingOffset: 500,
        viewportRowRenderingOffset: 1000,
        mergeCells: dataTableMergedCells.length == 0 ? true : dataTableMergedCells,
        afterSelection: function (r, c, r2, c2, preventScrolling, selectionLayerLevel) {
            preventScrolling.value = true;
        },
        contextMenu: {
            items: {
                //"Load chỉ tiêu phụ thuộc": {
                //    name: 'Load chỉ tiêu phụ thuộc',
                //    callback: function (key, options) {
                //        var selection = this.getSelectedRange();
                //        var fromRow = Math.min(selection[0].from.row, selection[0].to.row);
                //        var toRow = Math.max(selection[0].from.row, selection[0].to.row);
                //        var fromCol = Math.min(selection[0].from.col, selection[0].to.col);
                //        var toCol = Math.max(selection[0].from.col, selection[0].to.col);

                //        if (fromRow == toRow && fromCol == toCol && dataTable.getDataAtCell(fromRow, fromCol) != '') {
                //            var inCatalogValueName = dataTable.getDataAtCell(fromRow, fromCol);
                //            if (typeTable.getDataAtCell(fromCol, fieldRowsCount) == 'Chỉ tiêu') {
                //                $.getJSON('/Admin/DocType/LoadSubInCatalogValues', { inCatalogValueName: inCatalogValueName },
                //                    function (data, textStatus, jqXHR) {
                //                        var catalogValues = JSON.parse(data.inCatalogValues);
                //                        var catalogValueNames = jQuery.map(catalogValues, function (n, i) {
                //                            return n.name;
                //                        });

                //                        var row = fromRow + 1
                //                        catalogValues.forEach(function (item, index, arr) {
                //                            dataTable.alter('insert_row', row + index, 1);
                //                            for (var dataColumnIndex = 0; dataColumnIndex < dataTable.countCols(); dataColumnIndex++) {
                //                                var prop = { readOnly: false, className: 'htCenter', source: '', type: 'text' }
                //                                var typeTableIndex = dataColumnIndex

                //                                if (typeTable.getDataAtCell(fromCol, fieldRowsCount) == 'Chỉ tiêu' && dataColumnIndex == fromCol) {
                //                                    prop.source = catalogValueNames;
                //                                    chiTieuSources.push({ row: row + index, col: dataColumnIndex, source: catalogValueNames });
                //                                }
                //                                else
                //                                    prop.source = typeTable.getCellMeta(typeTableIndex, fieldRowsCount + 2).source;

                //                                prop.type = typeTable.getCellMeta(typeTableIndex, fieldRowsCount + 2).type;

                //                                if (typeTable.getDataAtCell(fromCol, fieldRowsCount) == 'Chỉ tiêu' && dataColumnIndex == fromCol)
                //                                    prop.className = "htLeft level-" + item.level;

                //                                dataTable.setCellMetaObject(row + index, dataColumnIndex, prop);

                //                                var cellText = typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 2);

                //                                if (cellText == undefined)
                //                                    cellText = '';
                //                                else {
                //                                    if (typeTable.getDataAtCell(fromCol, fieldRowsCount) == 'Chỉ tiêu' && dataColumnIndex == fromCol)
                //                                        cellText = item.name;
                //                                    else
                //                                        cellText = typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 2).toString().trim();
                //                                }

                //                                if (typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 2) != null) {
                //                                    dataTable.setDataAtCell(row + index, dataColumnIndex, cellText);
                //                                }
                //                                else if (typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 3)) {
                //                                    dataTable.setDataAtCell(row + index, dataColumnIndex, cellText);
                //                                }

                //                                if (typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 0) == 'Hình ảnh') {
                //                                    dataTable.setCellMeta(row + index, dataColumnIndex, "renderer", this.coverRenderer);
                //                                }
                //                            }
                //                        });
                //                    });
                //            }
                //        }
                //    }
                //},
                'remove_row': {
                    name: "Remove Row"
                },
                'row_above': {
                    name: "Row Above"
                },
                'row_below': {
                    name: "Row below"
                },
                'make_read_only': {
                    name: "Make Read Only"
                },
                'mergeCells': {
                    name: "MergeCells"
                },
                "alignment": {
                    name: "Alignment",
                    submenu: {
                        items: [
                            {
                                name: "Left",
                                key: "alignment:Left",
                                callback: function (key, options) {
                                    bindingAlginClassCells(this, this.getSelectedRange(), ' htLeft');
                                    this.render();
                                }
                            },
                            {
                                name: "Right",
                                key: "alignment:Right",
                                callback: function (key, options) {
                                    bindingAlginClassCells(this, this.getSelectedRange(), ' htRight');
                                    this.render();
                                }
                            },
                            {
                                name: "Center",
                                key: "alignment:Center",
                                callback: function (key, options) {
                                    bindingAlginClassCells(this, this.getSelectedRange(), ' htCenter');
                                    this.render();
                                }
                            },
                        ]
                    }
                },
                "make_bold": {
                    name: 'Make it bold',
                    callback: function (key, options) {
                        setClassNameByRange(this, this.getSelectedRange(), 'htBold');
                        this.render();
                    }
                },
                "make_italic": {
                    name: 'Make it italic',
                    callback: function (key, options) {
                        setClassNameByRange(this, this.getSelectedRange(), 'htItalic');
                        this.render();
                    }
                },
                "make_strike": {
                    name: 'Make it strike through',
                    callback: function (key, options) {
                        setClassNameByRange(this, this.getSelectedRange(), 'htStrike');
                        this.render();
                    }
                }
            }
        },
        cells: function (crow, ccol, prop) {
            var cellProperties = {}
            var typeName = typeTable.getDataAtCell(ccol, fieldRowsCount);
            if (typeName == 'Thời gian') {
                switch (fieldTable.getDataAtCell(ccol, fieldRowsCount + 1)) {
                    case date_dropdown[0]:
                        cellProperties.dateFormat = setting_json['Thời gian'].Details[date_dropdown[0]].Displaycode;
                        cellProperties.correctFormat = true;
                        break;
                    case date_dropdown[1]:
                        cellProperties.dateFormat = setting_json['Thời gian'].Details[date_dropdown[1]].Displaycode;
                        cellProperties.correctFormat = true;
                        break;
                    default:
                        break;
                }
            }
            else if (typeName == 'Catalog' && fieldTable.getDataAtCell(ccol, fieldRowsCount + 1) != null) {
                var t = setting_json['Catalog'].Details;
                cellProperties.type = 'dropdown';
                if (t[fieldTable.getDataAtCell(ccol, fieldRowsCount + 1)] != undefined) {
                    cellProperties.source = Object.keys(t[fieldTable.getDataAtCell(ccol, fieldRowsCount + 1)].Details);
                    cellProperties.readOnly = false;
                }
                
            }
            //else if (typeName == 'Chỉ tiêu' && chiTieuSources.length != 0) {
            //    $.each(chiTieuSources, function (index, item) {
            //        if (item.col == ccol && item.row == crow)
            //            cellProperties.source = item.source;
            //    });
            //}
            else if (typeName == 'Hình ảnh')
                cellProperties.renderer = coverRenderer;
            else
                cellProperties.source = typeTable.getCellMeta(ccol, fieldRowsCount + 2).source;

            cellProperties.type = typeTable.getCellMeta(ccol, fieldRowsCount + 2).type;

            var data = dataTableClassCells;
            if (data == undefined || data.length == 0 || data.length <= crow)
                return cellProperties;
            // dataTable.setCellMeta(crow, ccol, 'className', data[crow][ccol]);
            cellProperties.className = data[crow][ccol];

            return cellProperties;
        },
        licenseKey: 'non-commercial-and-evaluation'
    });

    dataTable.render();
    dataTable.validateCells();

    dataTable.addHook('afterRender', (isForced) => {
        fieldTableMergedCells = fieldTable.getPlugin('mergeCells').mergedCellsCollection.mergedCells;
        if (fieldTableMergedCells.length > 0) {
            var delarray = [];
            delarray = fieldTable.getData();

            var row = fieldRowsCount;
            var col = fieldColumnsCount;

            for (i = 0; i < row; i++)
                for (j = 0; j < col; j++)
                    delarray[i][j] = false

            var h0 = document.getElementById('dataTable');

            fieldTableMergedCells.forEach((e) => {
                for (var i = e.row + e.rowspan; i > e.row; i--)
                    for (var j = e.col + e.colspan; j > e.col; j--) {

                        var st1 = '.ht_clone_top  tr:nth-child(' + i + ') th:nth-child(' + (j + 1) + ')'

                        if ((i != e.row + 1) || (j != e.col + 1)) {
                            // h0.querySelectorAll(st1)[0].setAttribute('style', 'background : black;')
                            // h0.querySelectorAll(st1)[0].remove()
                            delarray[i - 1][j - 1] = true
                        } else {
                            if (h0.querySelectorAll(st1)[0] != undefined) {
                                //h0.querySelectorAll(st1)[0].setAttribute('style', 'background : red;')
                                h0.querySelectorAll(st1)[0].setAttribute('rowspan', e.rowspan);
                                h0.querySelectorAll(st1)[0].setAttribute('colspan', e.colspan);
                                h0.querySelectorAll(st1)[0].setAttribute('style', 'vertical-align: middle');
                            }
                        }
                    }
            })

            for (var i = row; i > 0; i--) {
                for (var j = col; j > 0; j--) {
                    var st1 = '.ht_clone_top  tr:nth-child(' + i + ') th:nth-child(' + (j + 1) + ')'
                    if (delarray[i - 1][j - 1] && h0.querySelectorAll(st1)[0] != undefined)
                        //h0.querySelectorAll(st1)[0].setAttribute('style', 'background : black;')
                        h0.querySelectorAll(st1)[0].remove()
                }
            }
        }

        var table1_col_size = [];
        for (var i = 0; i < fieldTable.countCols(); i++)
            table1_col_size.push(fieldTable.getColWidth(i));

        var h0 = document.getElementById('dataTable');

        for (var j = 1; j <= dataTable.countCols(); j++) {

            var st1 = '.ht_clone_top  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')';
            var st2 = '.ht_master  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')';

            if (h0.querySelectorAll(st1) != undefined && h0.querySelectorAll(st1)[0] != undefined) {
                h0.querySelectorAll(st1)[0].setAttribute('style', 'width:' + table1_col_size[j - 1] + "px; height: unset !important");
            }

            if (h0.querySelectorAll(st2) != undefined && h0.querySelectorAll(st2)[0] != undefined) {
                h0.querySelectorAll(st2)[0].setAttribute('style', 'width:' + table1_col_size[j - 1] + "px; height: unset !important");
            }
        }
    });

    Handsontable.hooks.add('beforeOnCellMouseDown',
        handleHotBeforeOnCellMouseDown, dataTable);

    var isEmptyIndexDataTable = document.getElementById('dataTable').innerHTML === " "

    if (!isEmptyIndexDataTable && indexDataTable != undefined) {
        document.getElementById('indexDataTable').innerHTML = "";
        //dataTable.destroy()
    }

    indexDataTable = new Handsontable(indexDataContainer, {
        columns: dataColumsSetting,
        rowHeaders: true,
        colHeaders: true,
        filters: true,
        height: 25,
        dropdownMenu: false,
        className: "htLeft",
        columnHeaderHeight: 40,
        manualColumnResize: true,
        viewportColumnRenderingOffset: 500,
        viewportRowRenderingOffset: 1000,
        colWidths: table1_col_size,
        licenseKey: 'non-commercial-and-evaluation'
    });

    // set ReadOnly by cell từ cấu hình DefineValueJson
    var propReadOnly = { readOnly: true, className: "" };
    dataTableReadOnlys.forEach(function (item, index, arr) {
        propReadOnly.className = "gray" + " " + dataTableClassCells[item.row][item.col];
        dataTable.setCellMetaObject(item.row, item.col, propReadOnly);
    });

    var h0 = document.getElementById('dataTable');

    for (var j = 1; j <= dataTable.countCols(); j++) {

        var st1 = '.ht_clone_top  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')'

        if (h0.querySelectorAll(st1)[0] != undefined) {
            h0.querySelectorAll(st1)[0].setAttribute('style', 'width:' + table1_col_size[j - 1] + "px; height: unset !important");
        }
    }
}

/**
 * update dữ liệu thay đổi của fieldTable tới typeTable
 * */
function updateTypeTableData() {
    fieldRowsCount = fieldTable.countRows();
    fieldColumnsCount = fieldTable.countCols();

    for (var i = 0; i < fieldRowsCount; i++) {
        for (var j = 0; j < fieldColumnsCount; j++) {
            if (typeTable.getDataAtCell(j, i) != '' && typeTable.getDataAtCell(j, i) != null)
                typeTable.setDataAtCell(j, i, fieldTable.getDataAtCell(i, j));
        }
    }

    fieldTableData = fieldTable.getData();
}

/**
* @param {MouseEvent} event
* @param {WalkontableCellCoords} coords
* @param {Element} element
*/
function addRowType() {
    var prop = { readOnly: false, className: 'htLeft', source: '', type: 'text' }
    var row = typeTable.countRows();

    var colAddTotal = fieldTable.countCols() - typeTable.countRows();
    for (var i = 0; i < colAddTotal; i++) {
        typeTable.alter('insert_row', row, 1);
        prop.source = typeTable.getCellMeta(fieldTable.countCols() - 1, fieldRowsCount + 1).source;
        prop.type = typeTable.getCellMeta(i, fieldRowsCount + 1).type;

        typeTable.setDataAtCell(row + i, fieldRowsCount, 'Số thực');
        typeTable.setDataAtCell(row + i, fieldRowsCount + 1, '');
        typeTable.setDataAtCell(row + i, fieldRowsCount + 2, '0');
        typeTable.setDataAtCell(row + i, fieldRowsCount + 3, true);
        typeTable.setDataAtCell(row + i, fieldRowsCount + 4, false);
    }

    var typeTableDataOfFieldTable = [];
    typeTableDataOfFieldTable = transpose(fieldTable.getData());

    if (typeTableDataOfFieldTable.length == 0) return;

    typeTableData = typeTable.getData();
    for (var i = typeTable.countRows() - 1; i >= typeTable.countRows() - colAddTotal; i--) {
        for (var j = 0; j < fieldRowsCount; j++) {
            typeTableData[i][j] = typeTableDataOfFieldTable[i][j];
        }
    }

    mergeCellsArray = []
    var mergedCellsCollection = fieldTable.getPlugin('mergeCells').mergedCellsCollection;
    if (mergedCellsCollection != null) {
        mergedCells = mergedCellsCollection.mergedCells;
        if (mergedCells.length > 0) {
            mergedCells.forEach(function (e) {
                mergeCellsArray.push({
                    row: e.col,
                    col: e.row,
                    rowspan: e.colspan,
                    colspan: e.rowspan
                })
            });
        }
    }

    typeTable.updateSettings({
        mergeCells: mergeCellsArray,
        data: typeTableData,
    });
}

function coverRenderer(instance, td, row, col, prop, value, cellProperties) {
    var escaped = Handsontable.helper.stringify(value),
        img;

    if (escaped.indexOf('http') === 0) {
        img = document.createElement('IMG');
        img.src = value;
        

        Handsontable.dom.addEvent(img, 'mousedown', function (e) {
            e.preventDefault(); // prevent selection quirk
        });

        Handsontable.dom.empty(td);
        td.appendChild(img);
    }
    else {
        // render as text
        Handsontable.renderers.TextRenderer.apply(this, arguments);
    }

    return td;
}

function btnAddRowData() {
    var prop = { readOnly: false, className: 'htLeft', source: '', type: 'text' }
    var row = dataTable.countRows();

    dataTable.alter('insert_row', row, 1);
    for (var j = 0; j < dataTable.countCols(); j++) {
        var typeTableIndex = j
        var k = 0;

        prop.source = typeTable.getCellMeta(typeTableIndex, fieldRowsCount + 2).source;
        prop.type = typeTable.getCellMeta(typeTableIndex, fieldRowsCount + 2).type;

        dataTable.setCellMetaObject(row, j, prop);

        var cellText = typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 2);
        if (cellText == undefined)
            cellText = '';
        else
            cellText = typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 2).toString().trim().split("{{index}}").join(row + 1);
        if (typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 2) != null) {
            dataTable.setDataAtCell(row, j, cellText);
        }
        else if (typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 3)) {
            dataTable.setDataAtCell(row, j, cellText);
        }

        if (typeTable.getDataAtCell(typeTableIndex, fieldRowsCount + 0) == 'Hình ảnh') {
            dataTable.setCellMeta(row, j, "renderer", this.coverRenderer);
        }
    }

    var table1_col_size = [];
    for (var i = 0; i < fieldTable.countCols(); i++)
        table1_col_size.push(fieldTable.getColWidth(i));

    dataTable.updateSettings({
        colWidths: table1_col_size
    })

    var h0 = document.getElementById('dataTable');

    for (var j = 1; j <= dataTable.countCols(); j++) {

        var st1 = '.ht_clone_top  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')';
        var st2 = '.ht_master  .wtHolder .wtHider .wtSpreader .htCore >colgroup:nth-child(1) col:nth-child(' + (j + 1) + ')';

        if (h0.querySelectorAll(st1) != undefined && h0.querySelectorAll(st1)[0] != undefined) {
            h0.querySelectorAll(st1)[0].setAttribute('style', 'width:' + table1_col_size[j - 1] + "px; height: unset !important");
        }

        if (h0.querySelectorAll(st2) != undefined && h0.querySelectorAll(st2)[0] != undefined) {
            h0.querySelectorAll(st2)[0].setAttribute('style', 'width:' + table1_col_size[j - 1] + "px; height: unset !important");
        }
    }

    refreshClassCells();
}

function btnAddColField() {
    fieldTableData = fieldTable.getData();
    fieldRowsCount = fieldTableData.length;
    fieldColumnsCount = fieldTableData[0].length;
    fieldColumsSetting = [];

    for (var i = 0; i <= fieldColumnsCount; i++) {
        fieldColumsSetting.push({
            validator: fieldTableValidator,
            allowInvalid: true
        })

        for (var j = 0; j < fieldRowsCount; j++) {
            fieldTableData[j][fieldColumnsCount] = '';
        }
    }

    fieldTable.updateSettings({
        data: fieldTableData,
        columns: fieldColumsSetting
    });

    fieldTable.validateCells();
    // cách inset này không sử dụng được khi áp dụng validate của fieldColumnSetting
    //fieldTable.alter('insert_col', fieldTable.countCols(), 1);
}

function addRowField() {
    fieldTableData = fieldTable.getData();
    fieldRowsCount = fieldTableData.length;
    fieldColumnsCount = fieldTableData[0].length;
    var rowItem = [];

    for (var i = 0; i < fieldColumnsCount; i++) {
        rowItem.push('')
    }
    fieldTableData.push(rowItem);

    fieldTable.updateSettings({
        data: fieldTableData
    });

    fieldTable.validateCells();
}
//#endregion

//#region private function
function generateTypeTable(isOnLoad) {
    fieldTableData = fieldTable.getData();
    fieldColumnsCount = fieldTableData[0].length;
    fieldRowsCount = fieldTableData.length;

    // Clear type table trước khi generate lại
    var isEmpty = document.getElementById('typeTable').innerHTML === "" || document.getElementById('typeTable').innerHTML === " "
    if (!isEmpty) {
        typeTable.destroy()
    }

    if (!isOnLoad)
        typeTableData = transpose(fieldTable.getData());

    if (typeTableData.length == 0) return;

    var colHeaders = [];
    var typeColCount = fieldRowsCount;
    var typeRowCount = fieldColumnsCount;

    for (var i = 0; i < typeColCount; i++)
        colHeaders.push('')

    colHeaders.push('Kiểu dữ liệu', 'Chi tiết', 'Giá trị mặc định', 'Bắt buộc', 'Chỉ đọc', 'Phạm vi', 'Ghi đè', "Ẩn cột desktop", "Xoay", "Giá trị", "Ẩn cột mobile", "Inline", "Auto size");
    var columnCount = colHeaders.length - typeColCount;
    for (var i = 0; i < typeRowCount; i++) {
        for (var j = 0; j < columnCount; j++) {
            typeTableData[i].push(null);
        }
    }

    if (typeColumsSetting.length == 0) {
        for (var i = 0; i < typeColCount; i++) {
            typeColumsSetting.push({
                readOnly: true
            })
        }

        //kieu du lieu/chi tiet
        typeColumsSetting.push({})

        typeColumsSetting.push({
            validator: typeTableValidator,
            allowInvalid: true
        })
        //gia tri mac dinh
        typeColumsSetting.push({
            validator: typeTableValidator,
            allowInvalid: true
        })
        //bat buoc/chi doc, phạm vi
        typeColumsSetting.push({}, {}, { className: "inputMasked", editor: inputMaskedEditor })

        // Ghi đè
        typeColumsSetting.push({});

        // Ẩn cột
        typeColumsSetting.push({});

        // Xoay
        typeColumsSetting.push({});

        // Giá trị
        typeColumsSetting.push({});

        // Ẩn cột mobile
        typeColumsSetting.push({});

        // Inline
        typeColumsSetting.push({});

        // Autosize
        typeColumsSetting.push({});
    }
    else {
        typeColumsSetting[fieldRowsCount + 5] = { className: "inputMasked", editor: inputMaskedEditor };
    }

    // tính toán thêm column setting của các cột mới vào columnsetting cũ
    var countNewCols = typeColCount + columnCount - typeColumsSetting.length;
    for (var i = 0; i < countNewCols; i++) {
        typeColumsSetting.push({});
    }
    if (typeColumsSetting.length )
        mergeCellsArray = []

    var mergedCellsCollection = fieldTable.getPlugin('mergeCells').mergedCellsCollection;
    if (mergedCellsCollection != null) {
        mergedCells = mergedCellsCollection.mergedCells;
        if (mergedCells.length > 0) {
            mergedCells.forEach(function (e) {
                mergeCellsArray.push({
                    row: e.col,
                    col: e.row,
                    rowspan: e.colspan,
                    colspan: e.rowspan
                })
            });
        }
    }

    typeTableSetting = {
        data: typeTableData,
        stretchH: 'all',
        filters: false,
        rowHeaders: false,
        colHeaders: colHeaders,
        columns: typeColumsSetting,
        autoColumnSize: true,
        autoWrapCol: false,
        autoWrapRow: false,
        autoRowSize: { syncLimit: '80%' },
        dropdownMenu: false,
        manualColumnResize: false,
        className: "htLeft",
        contextMenu: true,
        licenseKey: 'non-commercial-and-evaluation'
    }

    typeTable = new Handsontable(typeContainer, typeTableSetting);

    typeTable.addHook('beforeChange', function (changes, source) {
        if (source === 'loadData' || source === 'internal' || changes.length > 1) {
            return;
        }
        var row = changes[0][0];
        var col = changes[0][1];
        var value_before = changes[0][2];
        var value_after = changes[0][3];
        var propDetail = {}
        var propDefault = {};

        if (typeTable.getColHeader(col) == 'Kiểu dữ liệu') {

            if ((value_after != value_before) && (value_before) &&
                (typeTable.getDataAtCell(row, col + 1) || typeTable.getDataAtCell(row, col + 2) || typeTable.getDataAtCell(row, col + 3) || typeTable.getDataAtCell(row, col + 3))) {

                typeTable.setDataAtCell(row, col + 1, '')
                typeTable.setDataAtCell(row, col + 2, '')
                typeTable.setDataAtCell(row, col + 3, true)

                typeTable.setCellMetaObject(row, col + 1, '');
                typeTable.setCellMetaObject(row, col + 2, '');
            }

            switch (value_after) {
                case 'Kí tự':
                    propDetail = { source: char_dropdown, type: 'dropdown', readOnly: false }
                    propDefault = { source: '' }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);

                    typeTable.setCellMeta(row, col + 1, "renderer", "dropdown");
                    typeTable.setCellMeta(row, col + 2, "renderer", "text");
                    break;
                case 'Thời gian':
                    propDetail = { source: date_dropdown, type: 'dropdown', readOnly: false }
                    propDefault = { source: '', dateFormat: setting_json['Thời gian'].Details[date_dropdown[0]].Displaycode }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);

                    typeTable.setCellMeta(row, col + 1, "renderer", "dropdown");
                    typeTable.setCellMeta(row, col + 2, "renderer", "date");
                    break;
                case 'Catalog':
                    cate_dropdown = JSON.parse($('#catalogs').val());
                    propDetail = { source: cate_dropdown, type: 'dropdown', readOnly: false }
                    propDefault = { source: '', type: 'dropdown', readOnly: false }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);

                    typeTable.setCellMeta(row, col + 1, "renderer", "dropdown");
                    typeTable.setCellMeta(row, col + 2, "renderer", "dropdown");
                    break;
                case 'Chỉ tiêu':
                    cate_dropdown = JSON.parse($('#inCatalogs').val());
                    propDetail = { source: cate_dropdown, type: 'dropdown', readOnly: false }
                    propDefault = { source: '', type: 'dropdown', readOnly: false }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);

                    typeTable.setCellMeta(row, col + 1, "renderer", "dropdown");
                    typeTable.setCellMeta(row, col + 2, "renderer", "dropdown");
                    break;
                case 'Số nguyên':
                    propDetail = { readOnly: false, source: '' }
                    propDefault = { source: '' }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);
                    typeTable.setDataAtCell(row, col + 2, 0);

                    typeTable.setCellMeta(row, col + 1, "renderer", "text");
                    typeTable.setCellMeta(row, col + 2, "renderer", "text");

                    break;
                case 'Số thực':
                    propDetail = { readOnly: false }
                    propDefault = { source: '' }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);
                    typeTable.setDataAtCell(row, col + 2, 0);

                    typeTable.setCellMeta(row, col + 1, "renderer", "text");
                    typeTable.setCellMeta(row, col + 2, "renderer", "text");
                    break;
                case 'Checkbox':
                    propDetail = { source: '', readOnly: false }
                    propDefault = { source: '', type: 'checkbox', checkedTemplate: true, uncheckedTemplate: false }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);

                    typeTable.setCellMeta(row, col + 1, "renderer", "text");
                    typeTable.setCellMeta(row, col + 2, "renderer", "checkbox");
                    typeTable.setDataAtCell(row, col + 2, false);
                    break;
                case 'Hình ảnh':
                    propDetail = { readOnly: true, source: '' }
                    propDefault = { source: '' }

                    typeTable.setCellMetaObject(row, col + 1, propDetail);
                    typeTable.setCellMetaObject(row, col + 2, propDefault);
                    typeTable.setDataAtCell(row, col + 2, '');

                    typeTable.setCellMeta(row, col + 1, "renderer", "text");
                    typeTable.setCellMeta(row, col + 2, "renderer", "text");
                    break;
                default:
                    // statements_def
                    break;
            }
        }

        if (typeTable.getColHeader(col) == 'Chi tiết') {
            switch (value_after) {
                case date_dropdown[0]:
                    propDefault = { type: 'date', dateFormat: setting_json['Thời gian'].Details[date_dropdown[0]].Displaycode, correctFormat: true, readOnly: false }
                    typeTable.setCellMetaObject(row, col + 1, propDefault);
                    break;
                case date_dropdown[1]:
                    propDefault = { type: 'date', dateFormat: setting_json['Thời gian'].Details[date_dropdown[1]].Displaycode, correctFormat: true, readOnly: false }
                    typeTable.setCellMetaObject(row, col + 1, propDefault);
                    break;
                default:
                    if (typeTable.getDataAtCell(row, col - 1) == 'Catalog' && value_after != '') {
                        var formId = $('#Form_FormId').val() != null ? $('#Form_FormId').val() : $('#FormId').val();
                        $.getJSON('/Admin/Form/GetCatalogValues', { catalogName: value_after, row: row, col: col, formId: formId, isXoay: false },
                            function (data, textStatus, jqXHR) {
                                var catalogValues = JSON.parse(data.catalogValues);
                                propDefault = { type: 'dropdown', source: catalogValues, readOnly: false }
                                typeTable.setDataAtCell(row, col + 1, '');
                                typeTable.setCellMetaObject(row, col + 1, propDefault);
                            });
                    }
                    else if (typeTable.getDataAtCell(row, col - 1) == 'Chỉ tiêu' && value_after != '') {
                        var formId = $('#Form_FormId').val() != null ? $('#Form_FormId').val() : $('#FormId').val();
                        $.getJSON('/Admin/DocType/GetInCatalogValues', { inCatalogName: value_after, row: row, col: col, formId: formId, isXoay: false },
                            function (data, textStatus, jqXHR) {
                                var catalogValues = JSON.parse(data.inCatalogValues);
                                propDefault = { type: 'dropdown', source: catalogValues, readOnly: false }
                                typeTable.setDataAtCell(row, col + 1, '');
                                typeTable.setCellMetaObject(row, col + 1, propDefault);
                            });
                    }
                    break;
            }
        }
    });

    typeTable.updateSettings({
        cells: function (crow, ccol, prop) {
            var cellProperties = {}
            if (ccol == fieldRowsCount) {
                if (isEdit) {
                    var typeFiltered = datatype_dropdown;

                    if (["Số nguyên"].includes(typeTable.getDataAtCell(crow, ccol))) {
                        typeFiltered = datatype_dropdown.filter(function (el) {
                            return !["Chỉ tiêu", "Catalog", "Checkbox", "Thời gian", "Kí tự"].includes(el);
                        });

                        cellProperties = {
                            type: "dropdown", source: typeFiltered,
                            readOnly: false
                        }
                    }
                    else if (crow >= countCols) {
                        // newColumns.includes(crow)
                        // edit với trường hợp add thêm cột
                        cellProperties = {
                            type: "dropdown",
                            source: datatype_dropdown
                        }
                    }
                    else {
                        cellProperties = {
                            type: "dropdown", source: datatype_dropdown,
                            className: 'gray htCenter'
                        }
                    }
                }
                else {
                    cellProperties = {
                        type: "dropdown", source: datatype_dropdown
                    }
                }
            }
            else if (ccol == fieldRowsCount + 1) {
                if (["Kí tự ngắn"].includes(typeTable.getDataAtCell(crow, ccol))) {
                    cellProperties = {
                        type: "dropdown", source: char_dropdown,
                        readOnly: false
                    }
                }
            }
            else if ((ccol == fieldRowsCount + 2 && typeTable.getDataAtCell(crow, ccol - 2) == 'Checkbox') || (ccol == fieldRowsCount + 3) || (ccol == fieldRowsCount + 4)
                || (ccol == fieldRowsCount + 7) || (ccol == fieldRowsCount + 8 && typeTable.getDataAtCell(crow, ccol - 8) == 'Catalog')
                || (ccol == fieldRowsCount + 10) || (ccol == fieldRowsCount + 11) || (ccol == fieldRowsCount + 12)) {
                cellProperties = { type: 'checkbox' }
            }
            // Ghi đè
            else if ((ccol == fieldRowsCount + 6) || (ccol == fieldRowsCount + 9)) {
                if (typeTable.getDataAtCell(crow, ccol - 6) == 'Catalog') {
                    cellProperties = { type: 'checkbox' }
                }
                else if (typeTable.getDataAtCell(crow, ccol - 9) == 'Catalog') {
                    var value_dropdown = [];
                    typeTableData = typeTable.getData();
                    // get data
                    typeTableData.forEach(function (item, index, arr) {
                        if (index != crow) {
                            value_dropdown.push(item[fieldRowsCount - 1])
                        }
                    });
                    cellProperties = {
                        type: "dropdown", source: value_dropdown,
                        readOnly: false
                    }
                }
                else
                    cellProperties = { readOnly: true }
            }

            if (ccol < fieldRowsCount) {
                cellProperties = {
                    readOnly: true
                }
            }
            return cellProperties;
        },
        mergeCells: mergeCellsArray,
    });

    // set default value là số thực, bắt buộc là: true
    var propDetail = [];
    var propDefault = [];

    for (var i = 0; i < fieldColumnsCount; i++) {
        if (!isOnLoad) {
            typeTable.setDataAtCell(i, fieldRowsCount, "Số thực");
            typeTable.setDataAtCell(i, fieldRowsCount + 3, true);
            typeTable.setDataAtCell(i, fieldRowsCount + 2, 0);
        }
        else {
            if (typeTableData[i][fieldRowsCount] == "Catalog") {
                cate_dropdown = JSON.parse($('#catalogs').val());
                propDetail = { source: cate_dropdown, type: 'dropdown', readOnly: false }
                typeTable.setCellMetaObject(i, fieldRowsCount + 1, propDetail);

                var formId = $('#Form_FormId').val() != null ? $('#Form_FormId').val() : $('#FormId').val();
                $.getJSON('/Admin/Form/GetCatalogValues', { catalogName: typeTableData[i][fieldRowsCount + 1], row: i, col: (fieldRowsCount + 1), formId: formId, isXoay: false },
                    function (data, textStatus, jqXHR) {
                        var catalogValues = JSON.parse(data.catalogValues);
                        propDefault = { type: 'dropdown', source: catalogValues, readOnly: false }
                        typeTable.setCellMetaObject(data.row, fieldRowsCount + 2, propDefault);
                    });
            }
            else if (typeTableData[i][fieldRowsCount] == "Chỉ tiêu") {
                cate_dropdown = JSON.parse($('#inCatalogs').val());
                propDetail = { source: cate_dropdown, type: 'dropdown', readOnly: false }
                typeTable.setCellMetaObject(i, fieldRowsCount + 1, propDetail);

                var formId = $('#Form_FormId').val() != null ? $('#Form_FormId').val() : $('#FormId').val();
                $.getJSON('/Admin/DocType/GetInCatalogValues', { inCatalogName: typeTableData[i][fieldRowsCount + 1], row: i, col: (fieldRowsCount + 1), formId: formId, isXoay: false },
                    function (data, textStatus, jqXHR) {
                        var catalogValues = JSON.parse(data.inCatalogValues);
                        propDefault = { type: 'dropdown', source: catalogValues, readOnly: false }
                        typeTable.setCellMetaObject(data.row, fieldRowsCount + 2, propDefault);
                    });
            }
            else if (typeTableData[i][fieldRowsCount] == "Checkbox") {
                var data;
                data = typeTableData[i][fieldRowsCount + 2];
                propDefault = { type: 'checkbox', checkedTemplate: true, uncheckedTemplate: false }
                typeTable.setDataAtCell(i, fieldRowsCount + 2, '');
                typeTable.setCellMetaObject(i, fieldRowsCount + 2, propDefault);
                typeTable.setDataAtCell(i, fieldRowsCount + 2, data);
            }
            else if (typeTableData[i][fieldRowsCount] == "Thời gian") {
                propDetail = { source: date_dropdown, type: 'dropdown', readOnly: false }
                if (typeTableData[i][fieldRowsCount + 1] == date_dropdown[0]) {
                    propDefault = { type: 'date', dateFormat: setting_json['Thời gian'].Details[date_dropdown[0]].Displaycode, correctFormat: true, readOnly: false }
                }
                else if (typeTableData[i][fieldRowsCount + 1] == date_dropdown[1]) {
                    propDefault = { type: 'date', dateFormat: setting_json['Thời gian'].Details[date_dropdown[1]].Displaycode, correctFormat: true, readOnly: false }
                }
                typeTable.setCellMetaObject(i, fieldRowsCount + 1, propDetail);
                typeTable.setCellMetaObject(i, fieldRowsCount + 2, propDefault);
            }
        }
    }

    // 20200107 VuHQ START binding source [Giá trị mặc định]
    $.each(catalogDetails, function (index, catalogDetail) {
        typeTable.setCellMeta(catalogDetail.row, catalogDetail.col, 'source', catalogDetail.source);
    });
    // 20200107 VuHQ ENDinding source [Giá trị mặc định]
}

function calculateMergeCells(changes) {
    fieldRowsCount = changes.length;
    fieldColumnsCount = changes[0].length;
    var isCellMergeEnd = false;
    var colSpan = 1;
    var rowSpan = 1;
    fieldTableMergedCells = [];
    var isMerged = false;

    for (var i = 0; i < fieldRowsCount; i++) {
        for (var j = 0; j < fieldColumnsCount; j++) {
            if (j + 1 < fieldColumnsCount) {
                // check cell này đã được merged hay chưa
                isMerged = false;
                fieldTableMergedCells.forEach(function (item, index, arr) {
                    if (item.col == j + 1 && item.rowspan != 1 && i - item.row < item.rowspan) {
                        isMerged = true;
                    }
                });

                if (changes[i][j + 1] != null) {
                    isCellMergeEnd = true;
                }
                else if (!isMerged) {
                    isCellMergeEnd = false;
                    colSpan++;
                }
                else {
                    isCellMergeEnd = true;
                }
            }
            else {
                isCellMergeEnd = true;
            }

            if (colSpan == 1) {
                for (var k = i; k < fieldRowsCount; k++) {
                    if (k + 1 < fieldRowsCount) {
                        if (changes[k + 1][j] != null) {
                            isCellMergeEnd = true;
                            break;
                        }
                        else {
                            isCellMergeEnd = false;
                            rowSpan++;
                        }
                    }
                    else {
                        isCellMergeEnd = true;
                    }
                }
            }

            if ((colSpan != 1 || rowSpan != 1) && isCellMergeEnd) {
                fieldTableMergedCells.push({ row: i, col: (j + 1) - colSpan, rowspan: rowSpan, colspan: colSpan })
                colSpan = 1;
                rowSpan = 1;
            }
        }
    }
}

var handleHotBeforeOnCellMouseDown = function (event, coords, element) {
    if (coords.row < 0) {
        event.stopImmediatePropagation();
    }
};

function bindingAlginClassCells(thiss, selections, alignClass) {
    selections.forEach((selection) => {
        var fromRow = Math.min(selection.from.row, selection.to.row);
        var toRow = Math.max(selection.from.row, selection.to.row);
        var fromCol = Math.min(selection.from.col, selection.to.col);
        var toCol = Math.max(selection.from.col, selection.to.col);
        var className = '';

        for (var row = fromRow; row <= toRow; row++) {
            for (var col = fromCol; col <= toCol; col++) {
                var cellMeta = thiss.getCellMeta(row, col);

                if (cellMeta.className != undefined) {
                    className = cellMeta.className.replace(/htCenter/g, "").replace(/htRight/g, "").replace(/htLeft/g, "");
                    className = className + alignClass;
                    dataTableClassCells[row][col] = className;
                }
            }
        }
    });
}


function refreshClassCells() {
    // get classname of datatable
    dataTableClassCells = [];
    var h0 = document.getElementById('dataTable');
    var st1 = '.ht_master tbody > tr';

    $.each(h0.querySelectorAll(st1), function (rowIndex, rowValue) {
        var row = [];
        $.each(rowValue.cells, function (columnIndex, cellValue) {
            if (columnIndex == 0) return;
            row.push(cellValue.className);
        });
        dataTableClassCells.push(row);
    });
}

function transpose(a) {

    // Calculate the width and height of the Array
    var w = a.length || 0;
    var h = a[0] instanceof Array ? a[0].length : 0;

    // In case it is a zero matrix, no transpose routine needed.
    if (h === 0 || w === 0) {
        return [];
    }

    /**
     * @var {Number} i Counter
     * @var {Number} j Counter
     * @var {Array} t Transposed data is stored in this array.
     */
    var i, j, t = [];

    // Loop through every item in the outer array (height)
    for (i = 0; i < h; i++) {

        // Insert a new row (array)
        t[i] = [];

        // Loop through every item per item in outer array (width)
        for (j = 0; j < w; j++) {

            // Save transposed data.
            t[i][j] = a[j][i];
        }
    }

    return t;
}

var dataTableValidator = function (value, callback) {
    var dataType = typeTable.getDataAtCell(this.col, fieldRowsCount)
    var dataTypeDetail = typeTable.getDataAtCell(this.col, fieldRowsCount + 1)

    if (value == null || value == undefined) {
        callback(true)
        return;
    }
    switch (dataType) {
        case 'Số nguyên':
            callback(value.match(setting_json['Số nguyên'].PatternCode) != null)
            break;
        case 'Số thực':
            callback(value.match(setting_json['Số thực'].PatternCode) != null)
            break;
        case 'Thời gian':
            switch (dataTypeDetail) {
                case date_dropdown[0]:
                    callback(moment(value, setting_json['Thời gian'].Details[date_dropdown[0]].Displaycode, true).isValid())
                    break;
                case date_dropdown[1]:
                    callback(moment(value, setting_json['Thời gian'].Details[date_dropdown[1]].Displaycode, true).isValid())
                    break;
                default:
                    callback(true)
                    break;
            }
            break;
        default:
            callback(true)
            break;
    }
};

var typeTableValidator = function (value, callback) {
    if (fieldTable.getColHeader(this.col) == 'Giá trị mặc định') {
        var dataType = fieldTable.getDataAtCell(this.row, this.col - 2);
        var dataTypeDetail = fieldTable.getDataAtCell(this.row, this.col - 1);

        switch (dataType) {
            case 'Số nguyên':
                callback(value.match(setting_json['Số nguyên'].PatternCode) != null)
                break;
            case 'Số thực':
                callback(value.match(setting_json['Số thực'].PatternCode) != null)
                break;
            case 'Thời gian':
                switch (dataTypeDetail) {
                    case date_dropdown[0]:
                        callback(moment(value, setting_json['Thời gian'].Details[date_dropdown[0]].Displaycode, true).isValid())
                        break;
                    case date_dropdown[1]:
                        callback(moment(value, setting_json['Thời gian'].Details[date_dropdown[1]].Displaycode, true).isValid())
                        break;
                    default:
                        callback(true)
                        break;
                }
                break;
            default:
                callback(true)
                break;
        }
    }
    if (fieldTable.getColHeader(this.col) == 'Chi tiết') {
        callback(false)
    }

    callback(true)
};
//#endregion
// 20191106 VuHQ END

var setting_json = {
    "Số nguyên": {
        "TypeCode": "int",
        "Displaycode": null,
        "PatternCode": /^\d*$/,
        "Details": {}
    },
    "Số thực": {
        "TypeCode": "double",
        "Displaycode": null,
        "PatternCode": new RegExp("^[-+]?[0-9]*\.?[0-9]*$"),
        "Details": {}
    },
    "Kí tự": {
        "TypeCode": "char",
        "Displaycode": null,
        "PatternCode": null,
        "Details": {
            "Kí tự ngắn": {
                "TypeCode": "int",
                "Displaycode": null,
                "PatternCode": null
            },
            "Kí tự dài": {
                "TypeCode": "int",
                "Displaycode": null,
                "PatternCode": null
            }
        }
    },
    "Thời gian": {
        "TypeCode": "int",
        "Displaycode": null,
        "PatternCode": null,
        "Details": {
            "Ngày tháng năm": {
                "TypeCode": "int",
                "Displaycode": 'DD/MM/YYYY',
                "PatternCode": null
            },
            "Năm tháng ngày": {
                "TypeCode": "int",
                "Displaycode": 'YYYY/MM/DD',
                "PatternCode": null
            }
        }
    },
    "Checkbox": {
        "TypeCode": "int",
        "Displaycode": null,
        "PatternCode": null,
        "Details": {}
    },
    "Catalog": {
        "TypeCode": "cat",
        "Displaycode": null,
        "PatternCode": null,
        "Details": {
            "Cate1": {
                "Details": {
                    "Cate1.1": {
                        "TypeCode": "int",
                        "Displaycode": null,
                        "PatternCode": null
                    },
                    "Cate1.2": {
                        "TypeCode": "int",
                        "Displaycode": null,
                        "PatternCode": null
                    }
                }
            },
            "Cate2": {
                "Details": {
                    "Cate2.1": {
                        "TypeCode": "int",
                        "Displaycode": null,
                        "PatternCode": null
                    },
                    "Cate2.2": {
                        "TypeCode": "int",
                        "Displaycode": null,
                        "PatternCode": null
                    }
                }
            }
        }
    },
    "Chỉ tiêu": {
        "TypeCode": "cat",
        "Displaycode": null,
        "PatternCode": null,
        "Details": {}
    },
    "Hình ảnh": {
        "TypeCode": "int",
        "Displaycode": null,
        "PatternCode": null,
        "Details": {}
    },
}

var datatype_dropdown = Object.keys(setting_json)
var date_dropdown = Object.keys(setting_json['Thời gian'].Details)
var char_dropdown = Object.keys(setting_json['Kí tự'].Details)
var cate_dropdown = Object.keys(setting_json['Catalog'].Details)

class inputMaskedEditor extends Handsontable.editors.TextEditor {
    createElements() {
        super.createElements();

        var im = new Inputmask('Độ dài: (9, 999)');

        this.TEXTAREA = this.hot.rootDocument.createElement('input');
        this.TEXTAREA.setAttribute('type', 'text');
        this.TEXTAREA.className = 'handsontableInput';
        im.mask(this.TEXTAREA);
        this.textareaStyle = this.TEXTAREA.style;
        this.textareaStyle.width = 0;
        this.textareaStyle.height = 0;

        Handsontable.dom.empty(this.TEXTAREA_PARENT);
        this.TEXTAREA_PARENT.appendChild(this.TEXTAREA);
    }
}

function setClassNameByRange(thisthis, selection, className) {
    var fromRow = Math.min(selection[0].from.row, selection[0].to.row);
    var toRow = Math.max(selection[0].from.row, selection[0].to.row);
    var fromCol = Math.min(selection[0].from.col, selection[0].to.col);
    var toCol = Math.max(selection[0].from.col, selection[0].to.col);
    var tempClassName = '';
    for (var row = fromRow; row <= toRow; row++) {
        for (var col = fromCol; col <= toCol; col++) {
            var cellMeta = thisthis.getCellMeta(row, col);
            tempClassName = '';
            if (!cellMeta.className || (cellMeta.className && cellMeta.className.indexOf(className) < 0)) {
                tempClassName = cellMeta.className + " " + className;
            } else {
                var replace = className;
                var re = new RegExp(replace, "g");
                // tempClassName = cellMeta.className.replace(className, "");
                tempClassName = cellMeta.className.replace(re, "");
            }
            dataTableClassCells[row][col] = tempClassName;
        }
    }
}

function arraymove(arr, fromIndex, toIndex) {
    var element = arr[fromIndex];
    arr.splice(fromIndex, 1);
    arr.splice(toIndex, 0, element);
}