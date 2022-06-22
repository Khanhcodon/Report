define([
	'qtip',
],
    function ($, Backbone, Qtip) {
        "use strict";
        if (typeof $ != "function") {
            throw "Trang chưa có Jquery";
        }

        var _helperClass = ".shortkey",
        _mainSearch = "#MainSearchQuery",
        _listSearch = "#SearchQuery",
        _alertNotifier = "#AlertNotifier",          // Alert Notifi
        _userSetting = "#MenuSettings",             // User Profile
        _tabFocus = ".tab-focus",                   // Class hiển thị focus khi di chuyển qua các control
        _tabFocusName = "tab-focus",
        _tabIndex = [                               // Cấu hình tab
            {
                object: "#newDocuments",
                children: ["button"],
                isDropdownList: true
            },
            {
                object: "#menu-document",
                children: ["li"],
                isLevel: true,
                isVerticalAlign: true,
                isClickWhenFocus: true
            },
            {
                object: "#ulTabs",
                children: ["li"],
                isClickWhenFocus: true
            },
            {
                object: "ul.toolbar",
                children: ["li"],
                isMultiTab: true,
                isLevel: true,
            },
            {
                object: "table.table-main",
                children: ["th"],
                isTableHeader: true,
            },
            {
                object: ".grid-content",
                children: ["tr"],
                isVerticalAlign: true,
                isDocuments: true,
                isClickWhenFocus: true,
            }
        ],
        tabArea = 0,                // index vùng tab đang thao tác theo tab index trên
        focusObj,                   // Control đang được focus hiện tại
        childrenIdx = 0,            // index của phần tử đang được thao tác trong children của tabArea
        menuElements,               // danh sách các phần tử trong menu xổ xuống
        isMoveInMenu = false        // xác định đang di chuyển trong menu
        ;

        var eGovHomeShortKey = Backbone.View.extend({
            el:window,
            events: {
                'keyup': 'keyUp',
                // 'mousedown': 'clickEvent',
            },

            clickEvent: function (e) {
                var $target = this.$(e.target);
                for (var i = 0; i < _tabIndex.length; i++) {
                    var $child = $(_tabIndex[i].object).find(_tabIndex[i].children[0]);
                    for (var j = 0; j < $child.length; j++) {
                        if ($child.eq(j).find(e.target).length > 0) {
                            tabArea = i;
                            focusObj = _getObjectInTabArea(i, 0).eq(j);
                        }
                    }
                }
            },
            
            keyUp: function (e) {
                if (e.ctrlKey) {
                    _showShortKeyHelper();
                    switch (e.keyCode) {
                        case 49: // Ctrl 1: mở các thông báo
                            e.preventDefault();
                            _showAlertNotifier();
                            break;
                        case 51: // Ctrl 3: chọn node xử lý đầu tiên trên cây văn bản
                            e.preventDefault();
                            _showFirstNodeInDocumentTree();
                            break;
                        case 52: // Ctrl 4: chọn văn bản đầu tiên trong danh sách đang xem
                            e.preventDefault();
                            _selectFirstDocumentInList();
                            break;
                        case 66: // Ctrl B: tạo mới văn bản theo loại hồ sơ vừa tiếp nhận gần nhất (nếu ko có tạo theo loại đầu tiên)
                            e.preventDefault();
                            var isHs = false;
                            _createNewDocument(isHs);
                            break;
                        case 70: // Ctrl F: tìm kiếm
                            e.preventDefault();
                            _focusToMainSearch();
                            break;
                        case 71: // Ctrl G: focus vào ô tìm kiếm trên danh sách hồ sơ
                            e.preventDefault();
                            _focusToListSearch();
                            break;
                        case 72: // Ctrl H: tạo mới hồ sơ theo loại hồ sơ vừa tiếp nhận gần nhất
                            e.preventDefault();
                            var isHs = true;
                            _createNewDocument(isHs);
                            break;
                        case 73: // Ctrl I: mở tab in nhanh
                            e.preventDefault();
                            _openPrintTab();
                            break;
                        case 79: // Ctrl O: mở panel cầu hình
                            e.preventDefault();
                            _openOptionPanel();
                            break;
                        case 82: // Ctrl R: mở tab báo cáo thống kê
                            var tabIdx = egov.home.tab.getActiveTab().index();
                            if (tabIdx === 0) {
                                e.preventDefault();
                                _openReportTab();
                            }
                            break;
                        default:
                            break;
                    }
                }
                var tabIdx = egov.home.tab.getActiveTab().index();
                if (tabIdx === 0 || $(":focus").length === 0) {
                    switch (e.keyCode) {
                        case 9: // tab
                            e.preventDefault();
                            if (tabIdx !== 0 && tabArea === 3) {
                                focusObj.parents(".tab-content").find("input[type=text],textarea,select,input[type=checkbox]").eq(0).focus();
                                focusObj.blur();
                            } else {
                                _moveTab();
                            }
                            break;
                        case 13: // enter
                            e.preventDefault();
                            _selectFocused();
                            break;
                        case 27: // enter
                            e.preventDefault();
                            _open(e);
                            break;
                        case 32: // spacebar
                            //e.preventDefault();
                            //_open(e);
                            break;
                        case 36: // Home: mở tab trang chủ
                            _showHomeTab();
                            break;
                        case 37: // left
                            _moveLeft();
                            break;
                        case 38: // up
                            _moveUp();
                            break;
                        case 39: // right
                            _moveRight();
                            break;
                        case 40: // down
                            _moveDown();
                            break;
                            // Mở tab với số thứ tự tương ứng
                        case 49: // 1
                        case 50:
                        case 51:
                        case 52:
                        case 53:
                        case 54:
                        case 55:
                        case 56:
                        case 57: //9
                            if ($("*:focus").length > 0) {
                                break;
                            };
                            _showTab(e.keyCode - 49); // bắt đầu từ index 0
                            break;
                        default:
                            break;
                    }
                }
            },
            focusFirstTab: function () {
                // Gán cho thằng đầu tiên
                focusObj = _getObjectInTabArea(0, 0).first();
                tabArea = 0;
                childrenIdx = 0;
                _setFocusAndClick();
            }
        });

        //#region CommonShortKey
        var _selectFocused = function () {
            if (focusObj.length == 1) {
                focusObj.click();
            }
        };

        var _focusToMainSearch = function () {
            parent.window.postMessage("focusToMainSearch", "*");
            //$(_mainSearch).focus();
        };

        var isShowHelper = false;

        var _hideShortKeyHelper = function () {
            if (isShowHelper) {
                $(_helperClass).qtip('hide');
                isShowHelper = false;
            }
        };

        var _showShortKeyHelper = function () {
            if (!isShowHelper) {
                isShowHelper = true;
                $(_helperClass).qtip('show');
            }
        };

        var _showAlertNotifier = function () {
            $(_alertNotifier).click();
        };

        var _showFirstNodeInDocumentTree = function () {
            $("#child0 li:first > a").click();
        };

        var _selectFirstDocumentInList = function () {
            $("#tblListDocument tbody tr:first").click();
        };

        var _overrideBrowserEvent = function (e) {
            e.preventDefault();
        }

        var _focusToListSearch = function () {
            $(_listSearch).show();
            $(_listSearch).focus();
        };

        var _createNewDocument = function (isHs) {
            if (isHs) {
                $(".btn-create-hs").click();
            }
            else {
                $(".btn-create-vb").click();
            }
        };

        var _openPrintTab = function () { };

        var _openOptionPanel = function () {
            parent.window.postMessage("openOptionPanel", "*");
            //$(_userSetting).click();
        };

        var _openReportTab = function () {
            egov.home.tab.addReport();
        };

        var _showHomeTab = function () {
            egov.home.tab.activeTab(0);
        };

        var _showTab = function (tabIdx) {
            egov.home.tab.activeTab(tabIdx);
        };

        var _open = function (e) {
            var area = _tabIndex[tabArea];
            if (area.isDocuments) {
                focusObj.dblclick();
            } else {
                if (e.target !== document.body) {
                    e.target.click();
                } else {
                    focusObj.click();
                    focusObj.children("a:first").click();
                    focusObj.find("input[type=checkbox]").click();
                }
            }
        }

        //#endregion CommonShortKey

        //#region MoveTab
        /// <summary>Di chuyển qua các vùng index được định nghĩa ở trên</summary>
        /// <remarks>Di chuyển đến control đầu tiên trong các vùng</remarks>

        //#region Common

        var _setFocusAndClick = function () {
            if (focusObj.is("input")) {
                focusObj.focus();
            }
            $(_tabFocus).removeClass(_tabFocusName);
            focusObj.addClass(_tabFocusName);
            if (_tabIndex[tabArea].isClickWhenFocus) {
                focusObj.click();
            }
        };

        var _hideMenuBeforeMove = function () {
            isMoveInMenu = false;
            if (focusObj) {
                focusObj.qtip('hide');
            }
        };

        var _setFocusAndClick = function () {
            if (focusObj.is("input")) {
                focusObj.focus();
            }
            $(_tabFocus).removeClass(_tabFocusName);
            focusObj.addClass(_tabFocusName);
            if (_tabIndex[tabArea].isClickWhenFocus) {
                focusObj.click();
            }
        };

        var _getChildren = function (idx) {
            var child = _tabIndex[tabArea].children[idx];
            if (child == undefined) {
                return { object: undefined };
            }
            var result = {};
            if (typeof child === "object") {
                result = child;
            }
            else {
                result.object = child;
                result.isTip = false;
            }
            return result;
        };

        var _openChildren = function () {
            var icon = focusObj.children("a").children("span.menu-icon");
            if (icon.hasClass("closed")) {
                icon.click();
            } else if (icon.parent("a").hasClass("collapsed")) {
                icon.click();
            }
        };

        var _closeChildren = function () {
            var icon = focusObj.children("a").children("span.menu-icon");
            if (icon.hasClass("open")) {
                icon.click();
            } else if (!icon.parent("a").hasClass("collapsed") && !icon.hasClass("closed")) {
                icon.click();
            }
        };

        var _selectFocused = function () {
            if (focusObj.length == 1) {
                focusObj.click();
            }
        };

        var _gotoFirstElementNextArea = function () {
            if (tabArea == _tabIndex.length - 1) {
                tabArea = 0;
            }
            else {
                tabArea++;
            }
            var area = _tabIndex[tabArea];
            // gán focus là phần tử đầu tiên của vùng tiếp theo
            if (area.isMultiTab) {
                var href = focusObj.find("a").attr("href");
                focusObj = $(href).find("ul.toolbar").children("li").first();
            } else {
                childrenIdx = 0;
                focusObj = _getObjectInTabArea(tabArea, childrenIdx).first();
            }
            var tblListDocuments = focusObj.parents("div#documentList").children("[data-key=tblListDocument]");
            if (tblListDocuments.length > 0) {
                var count = 0;
                for (var j = 0; j < tblListDocuments.length; j++) {
                    if (tblListDocuments.eq(j).css("display") == "none") {
                        if (area.children[0] == "tr") {
                            count = count + tblListDocuments.eq(j).find(area.children[0]).length - 1;//bỏ 1 tr ở header
                        } else {
                            count = count + tblListDocuments.eq(j).find(area.children[0]).length;
                        }
                    }
                    if (tblListDocuments.eq(j).css("display") != "none") {
                        break;
                    }
                }
                focusObj = _getObjectInTabArea(tabArea, childrenIdx).eq(count);
            }
            _setFocusAndClick();
        };

        var _gotoLastElementPrevArea = function () {
            if (tabArea != 0) {
                tabArea--;
            }
            var area = _tabIndex[tabArea];
            childrenIdx = _tabIndex[tabArea].children.length - 1;
            // gán focus là phần tử cuối cùng của vùng trước đó
            focusObj = _getObjectInTabArea(tabArea, childrenIdx).last();

            var tblListDocuments = focusObj.parents("div").children("[data-key=tblListDocument]");
            var count = 0;
            for (var j = 0; j < tblListDocuments.length; j++) {
                if (tblListDocuments.eq(j).css("display") == "none") {
                    if (area.children[0] == "tr") {
                        count = count + tblListDocuments.eq(j).find(area.children[0]).length - 1;//bỏ 1 tr ở header
                    } else {
                        count = count + tblListDocuments.eq(j).find(area.children[0]).length;
                    }
                }
            }
            if (count !== 0) {
                focusObj = _getObjectInTabArea(tabArea, childrenIdx).eq(count);
            }
            _setFocusAndClick();
        };

        var _getObjectInTabArea = function (area, idx) {
            return $(_tabIndex[area].object + " " + _getChildren(idx).object);
        }

        //#endregion Common

        //#region Move in menu

        var _gotoNextElementInMenu = function () {
            _getNextFocusElementInListObject(menuElements);
            _setFocusAndClick();
        }

        var _gotoPrevElementInMenu = function () {
            _getPrevFocusElementInListObject(menuElements);
            _setFocusAndClick();
        }

        var _getNextFocusElementInListObject = function (childObject) {
            for (var i = 0; i < childObject.length; i++) {
                if ($(childObject[i]).is(focusObj)) {
                    if (i == childObject.length - 1) {
                        focusObj = $(childObject[0]);
                    }
                    else {
                        var area = _tabIndex[tabArea];
                        //Kiểm tra nếu trong focusobj có ul và không phải đang trong trạng thái mở => nhảy qua tất cả li con
                        if (area.isVerticalAlign && (focusObj.children("ul").length > 0) && !focusObj.children("ul").hasClass("in") && !focusObj.children("ul").hasClass("open")) {
                            focusObj = $(childObject[i + focusObj.children("ul").find("li").length + 1]);
                        } else if (!area.isVerticalAlign && (focusObj.children("ul").length > 0) && !focusObj.children("ul").hasClass("open") && !focusObj.children("ul").hasClass("in")) {
                            focusObj = $(childObject[i + focusObj.children("ul").find("li").length + 1]);
                        } else {
                            focusObj = $(childObject[i + 1]);
                        }
                    }
                    if (focusObj.length === 0) {
                        _moveTab();
                    }
                    //Nếu focusobj đang bị ẩn
                    var parents = focusObj.parents();
                    for (var j = parents.length - 1; j >= 0; j--) {
                        if (parents.eq(j).css("display") == "none") {
                            _moveTab();
                            break;
                        }
                    }
                    return;
                }
            }
            focusObj = childObject.first(); // nếu chưa có phần tử đang focus thì chọn cái đầu tien.
        }

        var _getPrevFocusElementInListObject = function (childObject) {
            for (var i = 0; i < childObject.length; i++) {
                if ($(childObject[i]).is(focusObj)) {
                    if (i == 0) {
                        //focusObj = $(childObject[0]);
                    }
                    else {
                        var area = _tabIndex[tabArea];
                        if (area.isLevel) {
                            var parents = $(childObject[i - 1]).parents("ul");
                            var parent = parents.eq(parents.length);
                            var count = 0;
                            if (parents.length > 0) {
                                for (var j = parents.length - 2; j >= 0; j--) {
                                    if (!(parents.eq(j).hasClass("in"))) {
                                        count = count + parents.eq(j).find("li").length;
                                        break;
                                    }
                                }
                            }
                            focusObj = $(childObject[i - 1 - count]);
                        } else {
                            focusObj = $(childObject[i - 1]);
                        }
                    }
                    //Nếu focusobj đang bị ẩn
                    var parents = focusObj.parents();
                    for (var j = parents.length - 1; j >= 0; j--) {
                        if (parents.eq(j).css("display") == "none") {
                            _gotoLastElementPrevArea();
                            break;
                        }
                    }
                    return;
                }
            }
            focusObj = childObject.last(); // nếu chưa có phần tử đang focus thì chọn cái cuối cùng.
        }

        //#endregion Move in menu

        //#region Move

        var _moveTab = function () {
            if (focusObj == undefined) {
                // Gán cho thằng đầu tiên
                focusObj = _getObjectInTabArea(0, 0).first();
                tabArea = 0;
                childrenIdx = 0;
                _setFocusAndClick();
            }
            else {
                _hideMenuBeforeMove();
                _gotoFirstElementNextArea();
            }
        };

        var _moveRight = function () {
            _hideMenuBeforeMove();
            var area = _tabIndex[tabArea];
            if (area.isVerticalAlign) {
                if (area.isLevel) {
                    _openChildren();
                }
            }
            else {
                _moveNext();
            }
        };

        var _moveLeft = function () {
            _hideMenuBeforeMove();
            var area = _tabIndex[tabArea];
            if (area.isVerticalAlign) {
                if (area.isLevel) {
                    _closeChildren();
                }
            }
            else {
                _movePrev();
            }
        };

        var _moveUp = function () {
            var area = _tabIndex[tabArea];
            var child = _getChildren(childrenIdx);
            // Nếu vùng hiện tại đang hiển thị dạng danh sách dọc thì di chuyển đến phần tử trước đó
            if (area.isVerticalAlign) {
                _movePrev();
                return;
            }
            else if (child.isTip) { // nếu focus có hiển thị qtip: option, notification, ...
                if (isMoveInMenu) {
                    _gotoPrevElementInMenu();
                    return;
                }
                var qtipId = focusObj.attr("data-hasqtip");
                if (qtipId != undefined) {
                    var tipObj = $("#qtip-" + qtipId);
                    if (tipObj.is(":visible") && !isMoveInMenu) // nếu đang hiển thị menu nhưng chưa di chuyển vào menu thì ẩn nó đi
                    {
                        focusObj.qtip('hide');
                    }
                    else {
                        if (menuElements != undefined) {
                            menuElements = $(child.tipElement);
                        }
                        _gotoPrevElementInMenu();
                    }
                    return;
                }
            }
            _hideMenuBeforeMove();
            _gotoLastElementPrevArea();
        };

        var _moveDown = function () {
            var area = _tabIndex[tabArea];
            var child = _getChildren(childrenIdx);
            // Nếu vùng hiện tại đang hiển thị dạng danh sách dọc thì di chuyển đến phần tử tiếp theo
            if (area.isVerticalAlign) {
                _moveNext();
                return;
            }
            else if (child.isTip) { // nếu focus có hiển thị qtip: option, notification, ...
                if (isMoveInMenu) {
                    _gotoNextElementInMenu();
                    return;
                }
                var qtipId = focusObj.attr("data-hasqtip");
                if (qtipId != undefined) {
                    var tipObj = $("#qtip-" + qtipId);
                    if (!tipObj.is(":visible")) // nếu đang ẩn hoặc chưa từng hiển thị thì hiển thị nó lên
                    {
                        focusObj.qtip('show');
                    }
                    else {
                        // nếu đang hiển thị thì di chuyển đến các phần tử trong menu xổ
                        menuElements = $(child.tipElement);
                        isMoveInMenu = true;
                        _gotoNextElementInMenu();
                    }
                    return;
                }
            }
            if (focusObj !== undefined) {
                _hideMenuBeforeMove();
                _gotoFirstElementNextArea();
            } else {
                _moveTab();
            }
        };

        var _moveNext = function () {
            if (tabArea == undefined || childrenIdx == undefined) {
                return;
            }
            var childObject = _getObjectInTabArea(tabArea, childrenIdx);
            if (focusObj) {
                if (focusObj.is(childObject.last())) {
                    if (childrenIdx !== _tabIndex[tabArea].children.length - 1) {
                        // Tăng childrenIdx
                        childrenIdx++;
                        // Lấy phần tử đầu tiên;
                        focusObj = _getObjectInTabArea(tabArea, childrenIdx).first();
                    }
                    else {
                        _moveTab();
                    }
                }
                else {
                    // Lấy phần tử tiếp theo trong 1 chuỗi phần tử của childrenIdx
                    _getNextFocusElementInListObject(childObject);
                }
                _setFocusAndClick();
            }
        }

        var _movePrev = function () {
            if (tabArea == undefined || childrenIdx == undefined) {
                return;
            }
            var childObject = _getObjectInTabArea(tabArea, childrenIdx);
            if (focusObj) {
                if (focusObj.is(childObject.first())) {
                    if (childrenIdx !== 0) {
                        // Giảm childrenIdx
                        childrenIdx--;
                        // Lấy phần tử cuối cùng;
                        focusObj = _getObjectInTabArea(tabArea, childrenIdx).last();
                    }
                    else {
                        _gotoLastElementPrevArea();
                    }
                }
                else {
                    // Lấy phần tử trước đó trong 1 chuỗi phần tử của childrenIdx
                    _getPrevFocusElementInListObject(childObject);
                }

                _setFocusAndClick();
            }
        };

        //#endregion Move

        //#endregion MoveTab

        return eGovHomeShortKey;
    });