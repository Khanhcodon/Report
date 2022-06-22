

function loadTab3() {
    var dataContainer = document.getElementById('dataTable');
    //var fieldColumnsCount = fieldTableData[0].length;
    //for (var i = 0; i < fieldColumnsCount; i++) {
    //    fieldColumsSetting.push({
    //        validator: fieldTableValidator,
    //        allowInvalid: true
    //    })
    //}

    //var dataTableSetting = {
    //    data: fieldTableData,
    //    columns: fieldColumsSetting,
    //    colWidths: fieldTableColWidths,
    //    rowHeaders: false,
    //    colHeaders: true,
    //    filters: true,
    //    dropdownMenu: false,
    //    autoColumnSize: true,
    //    manualColumnResize: true,
    //    //allowRemoveColumn: true,
    //    className: "htMiddle htLeft htBold",
    //    licenseKey: 'non-commercial-and-evaluation',
    //    selectionMode: 'range',
    //    formulas: true,
    //    mergeCells: fieldTableMergedCells
    //}

    //fieldTable = new Handsontable(dataContainer, dataTableSetting);
    const data = [
  ['', 'Tesla', 'Volvo', 'Toyota', 'Ford'],
  ['2019', 10, 11, 12, 13],
  ['2020', 20, 11, 14, 13],
  ['2021', 30, 15, 12, 13]
    ];

    const hot = new Handsontable(dataContainer, {
        data: data,
        rowHeaders: true,
        colHeaders: true
    });
}