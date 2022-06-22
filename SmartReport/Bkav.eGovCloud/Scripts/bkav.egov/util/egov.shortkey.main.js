var _mainSearch = "#MainSearchQuery",
    _alertNotifier = ".bell",    // bell
    apps = {
        bmail: "bmail",
        egov: "documents",
        chat: "chat",
        calendar: "calendar",
        report: "report",
        links: "links"
    };

$(document).on("keydown", function (e) {
    if (e.ctrlKey) {
        switch (e.keyCode) {
            case 49: // Ctrl 1: mở tab bmail
                //_showAlertNotifier();
                e.preventDefault();
                _displayApp(apps.bmail)
                break;
            case 50: // Ctrl 2: mở tab egov
                e.preventDefault();
                _displayApp(apps.egov)
                break;
            case 51: // Ctrl 3: mở tab chat
                e.preventDefault();
                _displayApp(apps.chat)
                break;
            case 52: // Ctrl 4: mở tab calendar
                e.preventDefault();
                _displayApp(apps.calendar)
                break;
            case 53: // Ctrl 5: mở tab report
                e.preventDefault();
                _displayApp(apps.report)
                break;
            case 54: // Ctrl 6: mở tab links
                e.preventDefault();
                _displayApp(apps.links)
                break;
            case 69: //Ctr shift E: Create chat
                if (e.shiftKey) {
                    createChat();
                }
                break;
            case 70: // Ctrl F: tìm kiếm
                if (helper.isCurrentFrame("documents")) {
                    e.preventDefault();
                    _focusToMainSearch();
                }
                break;
            case 79: // Ctrl O: mở panel cầu hình
                e.preventDefault();
                _openOptionPanel();
                break;

                //Những nút dưới là của egov bản cũ, hiện tại ko sử dụng
            case 66: // Ctrl B: tạo mới văn bản theo loại hồ sơ vừa tiếp nhận gần nhất (nếu ko có tạo theo loại đầu tiên)
                //Phần này hiện tại không chính xác nên không xử lý gì cả
                var isHs = false;
                //_createNewDocument(isHs);
                break;
            case 71: // Ctrl G: focus vào ô tìm kiếm trên danh sách hồ sơ
                //Phần này hiện tại không chính xác nên không xử lý gì cả
                //_focusToListSearch();
                break;
            case 72: // Ctrl H: tạo mới hồ sơ theo loại hồ sơ vừa tiếp nhận gần nhất
                var isHs = true;
                //Phần này hiện tại không chính xác nên không xử lý gì cả
                //_createNewDocument(isHs);
                break;
            case 73: // Ctrl I: mở tab in nhanh
                //_openPrintTab();
                break;

            case 82: // Ctrl R: mở tab báo cáo thống kê
                //var tabIdx = egov.home.tab.getActiveTab().index();
                //if (tabIdx === 0) {
                //    _openReportTab();
                //}
                break;
            default:
                break;
        }
    }
});


//#region CommonShortKey

var _displayApp = function (appName) {
    parent.helper.displayApp(appName);
}

var _selectFocused = function () {
    if (focusObj.length == 1) {
        focusObj.click();
    }
};

var _focusToMainSearch = function () {
    parent.focusToMainSearch();
};

var _showAlertNotifier = function () {
    var bell = window.parent.$(_alertNotifier);
    bell.hasClass("open") ? bell.removeClass("open") : bell.addClass("open");
};

var _showFirstNodeInDocumentTree = function () {
    egov.views.home.tree.showFirstNodeInDocumentTree();
};

var _selectFirstDocumentInList = function () {
    egov.views.home.documents.selectFirstDocumentInList();
};

var _overrideBrowserEvent = function (e) {
    e.preventDefault();
}

var _focusToListSearch = function () {
    $(_listSearch).show();
    $(_listSearch).focus();
};

var _createNewDocument = function (isHs) {
    //if (isHs) {
    //    $(".btn-create-hs").click();
    //}
    //else {
    //    $(".btn-create-vb").click();
    //}
};

var _openPrintTab = function () { };

var _openOptionPanel = function () {
    parent.openOptionPanel();
};

var _openReportTab = function () {
    egov.home.tab.addReport();
};

var _showHomeTab = function () {
    egov.views.home.tab.activeTab(0);
};

var _showTab = function (tabIdx) {
    egov.views.home.tab.activeTab(tabIdx);
};

var _open = function (e) {
    var area = _tabIndex[tabArea];
    if (area.isDocuments) {
        return;
        //Do mở văn bản bằng hàm dbclick đã modify nên phải dùng click() 2 lần
        focusObj.click().click();
    } else {
        if (e.target !== document.body) {
            e.target.click();
        } else {
            if (focusObj) {
                focusObj.click();
                focusObj.children("a:first").click();
                focusObj.find("input[type=checkbox]").click();
            }
        }
    }
}

//#endregion CommonShortKey
