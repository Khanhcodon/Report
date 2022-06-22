var fieldContainer = document.getElementById('fieldTable');
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
var countExtraCols = 0; xoayHeaders

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
var fieldTableData = [['', '', '']];

var fieldTableMergedCells = []
var fieldColumsSetting = [];
var fieldTableColWidths = [];

var fieldColumnsCount = fieldTableData[0].length;
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
            "remove_col": {
                name: "Xoa cot",
            },
            "remove_row": {
                name: "Xoa dong",
            },
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
    //allowRemoveColumn: true,
    className: "htMiddle htLeft htBold",
    licenseKey: 'non-commercial-and-evaluation',
    selectionMode: 'range',
    formulas: true,
    mergeCells: fieldTableMergedCells
}

fieldTable = new Handsontable(fieldContainer, fieldTableSetting);

var fieldTableValidator = function (value, callback) {
    var isValid = true
    if (value == '' || value == ' ')
        isValid = false
    callback(isValid);
};

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