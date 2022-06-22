var typeContainer = document.getElementById('typeTable');
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

    colHeaders.push('Kiểu dữ liệu', 'Chi tiết', 'Giá trị mặc định', 'Bắt buộc', 'Chỉ đọc', 'Phạm vi', 'Ghi đè', "Ẩn cột desktop", "Xoay", "Giá trị", "Key", "Value", "Ẩn cột mobile", "Inline", "Auto size");
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

        // Key
        typeColumsSetting.push({});

        // Value
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
    if (typeColumsSetting.length)
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
                            // cũ
                            //type: "dropdown", source: null,
                            //readOnly: true,
                            //className: 'gray htCenter'
                            type: "dropdown", source: datatype_dropdown,
                            readOnly: false
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