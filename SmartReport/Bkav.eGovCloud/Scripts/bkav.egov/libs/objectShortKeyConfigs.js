(function ($, egov) {

    if (!egov.shortKey) {
        egov.shortKey = {};
    }

    egov.shortKey.configs = [
                { displayName: "Mở các thông báo", functionName: "_showAlertNotifier()", isAlt: false, isCtrl: true, isShift: false, keyCode: "49", keyName: "1", shortKey: "Ctrl" },
                { displayName: "Chọn node xử lý đầu tiên trên cây văn bản", functionName: "_showFirstNodeInDocumentTree()", isAlt: false, isCtrl: true, isShift: false, keyCode: "51", keyName: "3", shortKey: "Ctrl" },
                { displayName: "Chọn văn bản đầu tiên trong danh sách đang xem", functionName: "_selectFirstDocumentInList()", isAlt: false, isCtrl: true, isShift: false, keyCode: "52", keyName: "4", shortKey: "Ctrl" },
                { displayName: "Tạo mới văn bản theo loại hồ sơ vừa tiếp nhận gần nhất", functionName: "_createNewDocument(isHs)", isAlt: false, isCtrl: true, isShift: false, keyCode: "98", keyName: "b", shortKey: "Ctrl" },
                { displayName: "Focus vào ô tìm kiếm trên trang chủ", functionName: "_focusToMainSearch()", isAlt: false, isCtrl: true, isShift: false, keyCode: "102", keyName: "f", shortKey: "Ctrl" },
                { displayName: "Focus vào ô tìm kiếm trên danh sách hồ sơ", functionName: "_focusToListSearch()", isAlt: false, isCtrl: true, isShift: false, keyCode: "103", keyName: "g", shortKey: "Ctrl" },
                { displayName: "Tạo mới hồ sơ theo loại hồ sơ vừa tiếp nhận gần nhất", functionName: "_createNewDocument(isHs)", isAlt: false, isCtrl: true, isShift: false, keyCode: "104", keyName: "h", shortKey: "Ctrl" },
                { displayName: "Mở tab in nhanh", functionName: "_openPrintTab()", isAlt: false, isCtrl: true, isShift: false, keyCode: "105", keyName: "i", shortKey: "Ctrl" },
                { displayName: "Mở panel cầu hình", functionName: "_openOptionPanel()", isAlt: false, isCtrl: true, isShift: false, keyCode: "111", keyName: "o", shortKey: "Ctrl" },
                { displayName: "Mở tab báo cáo thống kê", functionName: "_openReportTab()", isAlt: false, isCtrl: true, isShift: false, keyCode: "114", keyName: "r", shortKey: "Ctrl" }
           //    { displayName: "demo", functionName: "111", isAlt: false, isCtrl: true, isShift: false, keyCode: "114", keyName: "r", shortKey: "Ctrl" }
    ];
})(window.jQuery, window.egov = window.egov || {})