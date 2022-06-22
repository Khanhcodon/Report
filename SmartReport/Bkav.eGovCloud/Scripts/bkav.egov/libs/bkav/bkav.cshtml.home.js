(function (egov, $, _, etip, $dialog, undefined) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    if (typeof (_) === 'undefined') {
        throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    }
    var listDocumentWaitTransfer = {};

    /// Các biến dùng cho xử lý tab</summary>
    var tabIdentity = 0;
    var preFixTabContentCreate = 'tabContentPanel_Create_';
    var preFexTabContentEdit = 'tabContentPanel_Edit_';
    var urlCreateDocument = '/Document/Create';
    var urlEditDocument = '/Document/Edit/';
    var urlReport = '/ReportViewer/Index';
    var urlPrint = '/Print/ExpressPrint';
    var urlSetting = '/Home/Setting';

    //
    var $mainTab, $mainContent;
    var $ulmain;
    var recentTabs = [];
    var recentTabsCookieName = "RecentTabs";
    var recentTabsCookie = {};
    userId

    // Các biến dùng cho xử lý tree, danh sách 
    var idTableDocuments = '#tblListDocument';
    var $treeDocument, $treeStorePrivate, $doctype;
    var cacheDocuments = {};
    var cacheQuickViewDocuments = {};
    var currentQuickSearchResult = [];
    var currentPageListDocuments, currentPageSizeListDocuments, totalRecordsListDocuments;
    var rowIndex = 0;
    var isPaging = false;
    var regexUtcDate = /\d{4}-[0-1]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-6]\d$/;
    var rowIdSelected = [];
    var allUsers;
    var userIdJoined = [];
    var isListStoreDocument = false;
    var isOverdueFilter = false;
    var isDateFilter = false;
    var dateFilterView = "";
    // Các biến dùng cho quét tài liệu
    var plugin;
    var pluginName = "eOfficePlus";
    var currentUrl = "";
    var currentExt = "";
    var currentWidth = 0;
    var currentHeight = 0;
    var currentPage = 0;
    var allImages = [];
    var fitWidth = 250;
    var jcropApi;
    var x, y, x2, y2, w, h;
    var scanDialogTemplate;
    var mapImageFormat = { "2": '.jpg', "13": '.png', "25": '.gif', "18": '.tif', "0": '.bmp', "PDF": '.pdf', "DOC": '.doc' };
    var tabIcons = {
        home: "home.png",
        report: "report.png",
        option: "settings.png",
        newDocument: "new-document.png",
        document: "document.png",
        search: "search.png",
        print: "print.png"
    };
    var niceScrollOption = { cursorborderradius: 0, cursorcolor: "#555", cursorborder: 'none' };

    var documentViews = [];

    var setJcrop = function () {
        jcropApi = $.Jcrop('#imageScan', {
            onChange: setPosition,
            onSelect: setPosition,
            bgColor: 'black',
            bgOpacity: .9
        });
    };

    var setPosition = function (c) {
        x = c.x;
        y = c.y;
        x2 = c.x2;
        y2 = c.y2;
        w = c.w;
        h = c.h;
    };

    var showImageByCurrentPage = function () {
        var image = allImages[currentPage];
        currentUrl = image.url;
        currentExt = image.ext;
        currentWidth = image.width;
        currentHeight = image.height;
        jcropApi.destroy();
        var fitHeight = (fitWidth / image.width) * image.height;
        var $img = $('<img width="' + fitWidth + 'px" height="' + fitHeight + 'px" id="imageScan" src="data:image/' + image.ext + ';base64,' + image.data + '" />');
        $("#imagePreviewPanel").html($img);
        setJcrop();
        $("#addToContent").prop('checked', image.addToContent);
        $("#pagePosition").text('Trang: ' + (currentPage + 1) + '/' + allImages.length);
    };

    /// <summary>Hàm kiểm tra xem tab có phải là tab tạo mới không hay là tab chi tiết văn bản dựa vào id</summary>
    var isCreateDocument = function (id) {
        return /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/.test(id);
    };

    /// <summary> Gán title cho tab</summary>
    var addTabTitle = function (title, href, hasCloseButton, index, icon, attributes) {

        // Mẫu tab
        //<li title="Màn hình tác nghiệp">
        //                <div class="hover"></div>
        //                <a href="#DocumentPanel" class="shortkey" title="2">
        //                    <img src="../../Content/Tabs/home.png" />
        //                    Tác nghiệp
        //                    <span class="ui-icon ui-icon-close"></span>
        //                </a></li>

        $ulmain.children("li.tabs-active").removeClass("tabs-active");

        var $newTab = $('<li title="' + title + '" class="tabs-active qtooltip"></li>');
        if (attributes != undefined && attributes.class) {
            $newTab.addClass(attributes.class);
            delete attributes.class;
        }
        $newTab.append("<div class='hover'></div>");

        var $newTabLink = $('<a href="#' + href + '"></a>');
        //$newTabLink.append("<img src='../../Content/Tabs/" + icon + "' />");
        $newTabLink.append(title);
        $newTab.append($newTabLink);

        if (hasCloseButton) {
            $newTab.append('<span class="ui-icon ui-icon-close">x</span>');
        }

        if (attributes) {
            $newTab.attr(attributes);
        }

        $newTab.click(function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            if (hasCloseButton) {
                if (e.button === 0 || e.button == undefined) {
                    showTabContent($(this));
                } else if (e.button === 1) {
                    var close = $(this).find("span.ui-icon.ui-icon-close");
                    if (close.length > 0) {
                        close.trigger('click');
                    }
                }
            } else {
                showTabContent($(this));
            }

            return false;
        });
        if (index != null && index > -1) {
            var totalTab = $ulmain.children('li').length;
            if (index > totalTab - 1) {
                $ulmain.append($newTab);
            } else {
                var $beforeTab = $ulmain.children('li:eq(' + (index - 1) + ')');
                if ($beforeTab.length > 0) {
                    $beforeTab.after($newTab);
                }
            }
        } else {
            $ulmain.append($newTab);
        }
        reSizeTab();
        initTooltip($newTab);
        return $newTab;
    };

    /// <summary> Gán nội dung cho tab</summary>
    var addTabContent = function (url, contentId, onloaded) {
        var $newContent = $('<div id="' + contentId + '" style="display:block; height: 100%"></div>');
        $mainContent.append($newContent);
        $newContent.siblings("div:visible").hide();
        eGovMessage.notification("Đang tải...", eGovMessage.messageTypes.processing, false);
        var $newIframe = $('<iframe name="iframe_' + contentId + '" id="iframe_' + contentId + '" scr="" height="100%" width="100%" style="border:none"></iframe>');
        $newContent.append($newIframe);
        var frame = $newIframe[0];
        frame.src = url;
        $(frame).load(function () {
            if (onloaded && typeof onloaded === 'function') {
                onloaded($newIframe.attr('id'));
            }
            eGovMessage.notification("hide");
        });
        return $newIframe;
    };

    /// <summary> Thêm nội dung tab html</summary>
    var addTabContentHtml = function (contentId, html) {
        var $newContent = $('<div id="' + contentId + '" style="display:block; height: 100%"></div>');
        $mainContent.append($newContent);
        $newContent.siblings("div:visible").hide();
        if (html) {
            $newContent.append(html);
        }
    };

    /// <summary> Gán sự kiện đóng tab</summary>
    var addTabCloseEvent = function (isCreate, $newTab, $newIframe) {
        var frameName = $newIframe ? $newIframe[0].name : null;
        var iconClose = $newTab.find("span.ui-icon.ui-icon-close");
        $(iconClose).on('close', function () {
            closeTab($newTab);
        });
        $(iconClose).click(function () {
            if (frameName && document.getElementById(frameName)) {
                var frame = document.getElementById(frameName).contentWindow;
                if (frame.egov) {
                    var transfer;
                    if (isCreate) {
                        transfer = new egov.document.transfer(0, frameName);
                        if (frame.egov.cshtml.document.isHoso) {
                            eGovMessage.show(
                                "Bạn có đồng ý tiếp nhận hồ sơ này không?", null, eGovMessage.messageButtons.YesNo,
                                function () {
                                    // Gọi khi đóng tab tạo mới văn bản.
                                    transfer.transferSpecialCreate(transfer.actionSpecial.TiepNhanHoSo, $newTab);
                                },
                                function () {
                                    // TODO: Xóa file mới upload tạm lên server
                                    closeTab($newTab);
                                });
                        } else {
                            eGovMessage.show(
                                "Bạn có đồng ý lưu dự thảo?", null, eGovMessage.messageButtons.YesNo,
                                function () {
                                    transfer.saveDocDraft($newTab);
                                },
                                function () {
                                    // TODO: Xóa file mới upload tạm lên server
                                    closeTab($newTab);
                                });
                        }
                    } else {
                        transfer = new egov.document.transfer(frame.egov.cshtml.document.documentCopyId, frameName);
                        if (frame.egov.cshtml.document.luuvanbanPermission) {
                            if (frame.egov.cshtml.document.isFormChange) {
                                eGovMessage.show("Văn bản/hồ sơ có sự thay đổi, bạn có muốn lưu lại không?", null, eGovMessage.messageButtons.YesNo,
                                function () {
                                    transfer.saveDoc($newTab);
                                },
                                function () {
                                    // TODO: Cancel thì không xử lý việc lưu file đã đính kèm --> Cần xóa file tạm plugin đang giữ.
                                    // TODO: Xóa file mới upload tạm lên server
                                    closeTab($newTab);
                                });
                            } else {
                                //TODO: Cancel thì không xử lý việc lưu file đã đính kèm --> Cần xóa file tạm plugin đang giữ.
                                // TODO: Xóa file mới upload tạm lên server
                                closeTab($newTab);
                            }
                        } else {
                            closeTab($newTab);
                        }
                    }
                } else {
                    closeTab($newTab);
                }
            } else {
                closeTab($newTab);
            }
            return false;
        });
    };

    /// <summary> Hiển thị nội dung tab</summary>
    var showTabContent = function ($tab) {
        var activeClass = "tabs-active";
        if (!$tab.hasClass(activeClass)) {
            var a = $tab.children("a[href]")[0];
            if (a) {
                $tab.addClass(activeClass);
                $tab.siblings("li." + activeClass).removeClass(activeClass);
                var id = $(a).attr("href");
                var url;
                if ($(id).length == 0) {
                    url = urlEditDocument + id.replace('#' + preFexTabContentEdit, '');
                    addTabContent(url, id.replace('#', ''));
                }

                $(id).show();

                if ($tab.is('[egovtab="print"]')) {
                    egov.cshtml.home.tab.reloadActiveTab($tab);
                }

                $(id).siblings("div:visible").hide();
            }
        }
    };

    /// <summary> Đóng tab</summary>
    var closeTab = function ($tab) {
        var idx = $tab.index();
        var $a = $tab.children("a");
        if (idx == $ulmain.children("li.tabs-active").index()) {
            //var $prevLi = $ulmain.children('li[egovtab=document]');
            //var $searchTab = $ulmain.children('li[egovtab=search]');
            //if ($searchTab.length === 1 && $tab.attr('egovtab') !== 'search') {
            //    $prevLi = $searchTab;
            //}
            var $prevLi = $tab.prev();
            if ($prevLi.length === 0) {
                $prevLi = $tab.next();
            }
            $prevLi.addClass("tabs-active");
            var $prevA = $prevLi.children("a[href]");
            if ($prevA) {
                var id = $prevA.attr("href");
                $(id).show();
                $(id).siblings("div:visible").hide();
            }
        }
        var href = $a.attr("href");
        if (egov.cshtml.home.tab.config.isSaveOpenTab) {
            if (recentTabs.length > 0) {
                recentTabs = _.reject(recentTabs, function (item) {
                    return item.id == href.replace('#' + preFexTabContentEdit, '');
                });
                recentTabsCookie[userId] = recentTabs;
                $.cookie(recentTabsCookieName, JSON.stringify(recentTabsCookie), { secure: true });
            }
        }
        if ($tab.data('onclosed')) {
            $tab.data('onclosed')();
        }
        $($tab).qtip("destroy");
        $tab.remove();
        $(href).remove();
        if ($('.tabs-ul li').length === 0) {
            egov.cshtml.home.tab.activeRootTab();
        }
        reSizeTab();
    };

    /// <summary> Thay đổi kích thước tất cả các tab</summary>
    var reSizeTab = function () {
        var documentWidth = $(document).outerWidth();
        var $allTabs = $ulmain.find("li");
        var count = $allTabs.length;
        var parentPading = 10; // padding của div tab.
        var marginBetweenTab = count - 1;
        var parentWidth = documentWidth - parentPading - marginBetweenTab;
        // TienBV: comment vì totalWidthTab hiện chưa dùng ở đâu
        //var totalWidthTab = 0;
        //$allTabs.each(function (index, item) {
        //    totalWidthTab += $(item).outerWidth(true);
        //});

        var paddingTab = 12;
        var newMaxWidth = Math.floor(parentWidth / count) - paddingTab;
        if (newMaxWidth > 95) {
            newMaxWidth = 95;
        }
        $allTabs.each(function (index, item) {
            $(item).css("width", newMaxWidth + "px");
        });
    };

    var getSelectedNode = function () {
        return $($(egov.cshtml.home.idPanelTreeDocument).jstree('get_selected'));
    };

    /// <summary>Lọc danh sách văn bản theo Loại hồ so</summary>
    var filterByDocType = function (id) {
        $doctype = $('#dtypeSelect');
        if (cacheDocuments[id].isFilterByDocFieldDocType) {
            $("#DTypeSelect").show();
            var allDoctypeId = [];
            $.each(cacheDocuments[id].documentsObject, function (index, item) {
                allDoctypeId.push(item.DocTypeId);
            });

            allDoctypeId = egov.utilities.array.distinct(allDoctypeId);
            var filterDoctypes = _.filter(egov.cshtml.home.doctypes, function (item) {
                return _.contains(allDoctypeId, item.DocTypeId);
            });
            if (filterDoctypes.length > 0) {
                $doctype.html('').append($('#doctypeTemplate').tmpl(filterDoctypes));
                $doctype.find("span.doctype-item").bind("click", function () {
                    $(this).siblings(".selected").removeClass("selected");
                    $(this).addClass("selected");
                    $("#DTypeSelect a.edropdown").qtip('hide');
                    var value = $(this).attr("value");
                    $(".grid-content").scrollTop(0);
                    if (value == '') {
                        reloadListDocuments(id, cacheDocuments[id].columnSetting, cacheDocuments[id].documentsObject);
                    } else {
                        var filter = value === ''
                            ? cacheDocuments[id].documentsObject
                            : _.filter(cacheDocuments[id].documentsObject, function (item) {
                                return item.DocTypeId === value;
                            });
                        if (cacheDocuments[id].doctypeColumnSettings) {
                            if (cacheDocuments[id].doctypeColumnSettings[value]) {
                                reloadListDocuments(id + value, cacheDocuments[id].doctypeColumnSettings[value], filter);
                            } else {
                                $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[id].columnSetting));
                                $(idTableDocuments + " tr:odd").addClass("trodd");
                                bindContextShortCut();
                            }
                        } else {
                            $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[id].columnSetting));
                            $(idTableDocuments + " tr:odd").addClass("trodd");
                            bindContextShortCut();
                        }
                    }
                });
            } else {
                $("#DTypeSelect").hide();
            }
        } else {
            $("#DTypeSelect").hide();
        }
    };

    /// <summary> Lấy ra danh sách văn bản hồ sơ</summary>
    /// <param name="id" type="Int">Id của node trên cây văn bản</param>
    /// <param name="url" type="String">Url lấy ra danh sách văn bản theo node, xem trong attr của node</param>
    /// <param name="reloadFunctionIds" type="Array">Danh sách các node đang mở</param>
    /// <param name="showLoading" type="Bool">Hiển thị load ding</param>
    var getDocuments = function (id, url, reloadFunctionIds, showLoading) {
        var obj = {};
        var lastDocument;
        if (reloadFunctionIds && reloadFunctionIds.length > 0) {
            obj.reloadFunctionIds = reloadFunctionIds;
        }
        if (cacheDocuments[id]) {
            if (cacheDocuments[id].documentsObject.length > 0) {
                lastDocument = _.max(cacheDocuments[id].documentsObject, function (item) { return Date.parse(item.DateReceived); });
                obj.lastDate = lastDocument.DateReceived;
                obj.currentDocCopyIds = _.pluck(cacheDocuments[id].documentsObject, 'DocumentCopyId');
            } else {
                obj.lastDate = null;
            }
        }
        if (!egov.cshtml.home.pagingByScroll) {
            obj.pagesize = egov.cshtml.home.listdocuments.pageSize();
        }
        if (showLoading) {
            eGovMessage.notification("Đang lấy dữ liệu...", eGovMessage.messageTypes.processing);
        }
        $.ajaxSetup({ traditional: true });
        $.post(url, obj, function (result) {
            isOverdueFilter = result.listDocuments.isOverdueFilter;
            isDateFilter = result.listDocuments.isDateFilter;
            dateFilterView = result.listDocuments.dateFilterView;
            if (isOverdueFilter) {
                $("#DateOverdueSelect").show();
            }
            else {
                $("#DateOverdueSelect").hide();
            }
            if (isDateFilter) {
                $("#GotoDateSelect").hide();
            }
            else {
                $("#GotoDateSelect").show();
                if (!$("#GotoDateSelect a").text().indexOf(dateFilterView)) {
                    $("#GotoDateSelect a").prepend(dateFilterView);
                }
            }
            currentPageListDocuments = result.listDocuments.page;
            currentPageSizeListDocuments = result.listDocuments.pageSize;
            totalRecordsListDocuments = result.listDocuments.totalDocuments;
            if (result.listDocuments.totalUnread > 0) {
                $("#" + id).children('a:first').children('span:first').html(result.listDocuments.totalUnread).attr('data-totalunread', '' + result.listDocuments.totalUnread);
            } else {
                $("#" + id).children('a:first').children('span:first').html(' ').attr('data-totalunread', '' + result.listDocuments.totalUnread);
            }
            if (result.reloadFunctions) {
                var reloadFunctions;
                try {
                    reloadFunctions = JSON.parse(result.reloadFunctions);
                } catch (ex) {
                    reloadFunctions = [];
                }
                $.each(reloadFunctions, function (index, item) {
                    var nodeId = item.id + (item.param ? "_" + item.param : "");
                    if (item.totalDocumentUnread > 0) {
                        $("#" + nodeId).children('a:first').children('span:first').html(item.totalDocumentUnread).attr('data-totalunread', '' + item.totalDocumentUnread);
                    } else {
                        $("#" + nodeId).children('a:first').children('span:first').html(' ').attr('data-totalunread', '' + item.totalDocumentUnread);
                    }
                });
            }
            var documents;
            try {
                documents = JSON.parse(result.listDocuments.documents);
            } catch (ex) {
                documents = [];
            }
            if (cacheDocuments[id] && cacheDocuments[id].documentsObject.length > 0 && egov.cshtml.home.pagingByScroll) {
                if (result.listDocuments.removeDocumentCopyIds && result.listDocuments.removeDocumentCopyIds.length > 0) {
                    $.each(result.listDocuments.removeDocumentCopyIds, function (idx, item) {
                        $(idTableDocuments).find('tbody tr[id=documentCopy_' + item + ']').remove();
                    });
                    cacheDocuments[id].documentsObject = _.reject(cacheDocuments[id].documentsObject, function (item) {
                        return _.contains(result.listDocuments.removeDocumentCopyIds, item.DocumentCopyId);
                    });
                }
                documents = _.reject(documents, function (item) {
                    return item.DocumentCopyId == lastDocument.DocumentCopyId;
                });
                var columnSettings = cacheDocuments[id].columnSetting;
                if (!columnSettings) {
                    columnSettings = [];
                }

                if (!_.find(columnSettings, function (item) { return item.ColumnName == "STT"; })) {
                    // TienBV tạm bỏ cột sTT nàydđi
                    columnSettings.push({ ColumnName: "STT", DisplayName: "STT", Width: 30, EnableSort: false, Order: -1 });
                }
                if (!_.find(columnSettings, function (item) { return item.ColumnName == "Checkmask"; })) {
                    //columnSettings.push({ ColumnName: "Checkmask", DisplayName: "", Width: 30, EnableSort: false, Order: -1 });
                }
                if (!_.find(columnSettings, function (item) { return item.ColumnName == "Color"; })) {
                    columnSettings.push({ ColumnName: "Color", DisplayName: "", Width: 50, EnableSort: false, Order: -1 });
                }

                columnSettings = _.sortBy(columnSettings, function (item) { return item.Order; });
                var appendRows = bindDocumentRows(documents, columnSettings);
                $(idTableDocuments).find('tbody tr:first').before(appendRows);
                $(idTableDocuments).find('tbody tr').each(function (idx, item) {
                    if ($(item).attr('id') !== 'rowGetDocumentOlder') {
                        $(item).children('td:first').html((idx + 1) + '');
                    }
                });
                bindContextShortCut();
                $.each(documents, function (idx, item) {
                    cacheDocuments[id].documentsObject.push(item);
                });
                cacheDocuments[id].documentsObject = _.sortBy(cacheDocuments[id].documentsObject, function (item) { return Date.parse(item.DateReceived); }).reverse();
            } else {
                cacheDocuments[id] = result.listDocuments;
                cacheDocuments[id].reload = false;
                cacheDocuments[id].documentsObject = documents;
                cacheDocuments[id].isOverdueFilter = isOverdueFilter;
                cacheDocuments[id].isDateFilter = isDateFilter;
                filterByDocType(id);
                reloadListDocuments(id, cacheDocuments[id].columnSetting, cacheDocuments[id].documentsObject);
                bindPaging(id);
            }
            isListStoreDocument = false;
            //$.each(rowIdSelected, function (index, item) {
            //    addClassInRow($("#" + item));
            //});
        })
        .fail(function () {
            eGovMessage.notification("Có lỗi xảy ra", eGovMessage.messageTypes.error);
        })
        .complete(function () {
            if (showLoading) {
                eGovMessage.notification('hide');
            }
            $.ajaxSetup({ traditional: false });
        });

    };

    var bindListDocuments = function (columnSettings, documents) {
        var result = { cols: '', headers: '', rows: '' };

        //columnSettings.push({ ColumnName: "Checkmask", DisplayName: "", Width: 30, EnableSort: false, Order: -1 });
        //columnSettings.push({ ColumnName: "Color", DisplayName: "", Width: 50, EnableSort: false, Order: -1 });

        if (documents.length === 0) {
            columnSettings = _.sortBy(columnSettings, function (item) { return item.Order; });
            $.each(columnSettings, function (index, setting) {
                if (setting.Width) {
                    result.cols += '<col style="width: ' + setting.Width + 'px"/>';
                } else {
                    result.cols += '<col/>';
                }
                result.headers += '<th data-column="' + setting.ColumnName + '">' + setting.DisplayName + '</th>';
            });
            result.rows = '';
        } else {
            columnSettings = _.sortBy(columnSettings, function (item) { return item.Order; });
            $.each(columnSettings, function (index, setting) {
                if (setting.Width) {
                    result.cols += '<col style="width: ' + setting.Width + 'px"/>';
                } else {
                    result.cols += '<col/>';
                }
                if (setting.EnableSort) {
                    result.headers += '<th data-column="' + setting.ColumnName + '"><a id="' + (setting.SortName ? setting.SortName : setting.ColumnName) + '" href="javascript:egov.cshtml.home.listdocuments.sort(\'' + (setting.SortName ? setting.SortName : setting.ColumnName) + '\')">' + setting.DisplayName + '</a></th>';
                } else {
                    result.headers += '<th data-column="' + setting.ColumnName + '">' + setting.DisplayName + '</th>';
                }
            });
            result.rows = bindDocumentRows(documents, columnSettings);
        }

        $(egov.cshtml.home.idPanelListDocument).html($("#listDocumentTemplate").tmpl(result));

        // Thêm phần phân trang nếu không chọn paging by scroll
        if (!egov.cshtml.home.pagingByScroll) {
            $('#tblListDocument').append($("#pagingTemplate").tmpl());
            var totalPages = Math.ceil(totalRecordsListDocuments / currentPageSizeListDocuments);
            $('#tblListDocument tfoot li:last').text('Trang ' + currentPageListDocuments + ' / ' + totalPages);
        }
    };

    /// <summary> Bind dữ liệu lên thành danh sách</summary>
    var reloadListDocuments = function (id, columnSettings, documents) {
        if (!columnSettings) {
            columnSettings = [];
        }
        if (!_.find(columnSettings, function (item) { return item.ColumnName == "STT"; })) {
            columnSettings.push({ ColumnName: "STT", DisplayName: "STT", Width: 30, EnableSort: false, Order: -1 });
        }
        if (!_.find(columnSettings, function (item) { return item.ColumnName == "Checkmask"; })) {
            //columnSettings.push({ ColumnName: "Checkmask", DisplayName: "", Width: 30, EnableSort: false, Order: -1 });
        }
        if (!_.find(columnSettings, function (item) { return item.ColumnName == "Color"; })) {
            columnSettings.push({ ColumnName: "Color", DisplayName: "", Width: 50, EnableSort: false, Order: -1 });
        }
        var cookieName = "function" + id;
        var columnWidthCookieContent = $.cookie(cookieName);
        if (columnWidthCookieContent) {
            var columnWidth;
            try {
                columnWidth = JSON.parse(columnWidthCookieContent);
            } catch (e) {
                columnWidth = null;
            }
            $.each(columnWidth, function (key, value) {
                var exist = _.find(columnSettings, function (item) {
                    return item.ColumnName == key;
                });
                if (exist) {
                    exist.Width = value;
                }
            });
        }
        isOverdueFilter = cacheDocuments[id].isOverdueFilter;
        isDateFilter = cacheDocuments[id].isDateFilter;
        if (isOverdueFilter) {
            $("#DateOverdueSelect").show();
        }
        else {
            $("#DateOverdueSelect").hide();
        }
        if (isDateFilter) {
            $("#GotoDateSelect").hide();
        }
        else {
            $("#GotoDateSelect").show();
        }
        bindListDocuments(columnSettings, documents);
        $(idTableDocuments).grid({
            isUsingCustomScroll: false,
            isResizeColumn: true,
            isFixHeightContent: true,
            isAddHoverRow: false,
            isUseCookie: false,
            isRenderPanelGrid: false,
            onresizefinish: function () {
                var data = {};
                var columnNoWidth = [];
                var totalWidth = 0;
                var tableHeader = $(egov.cshtml.home.idPanelListDocument + ' .grid-header table th');
                var tableHeaderCol = $(egov.cshtml.home.idPanelListDocument + ' .grid-header table colgroup col');
                var tableContent = $(egov.cshtml.home.idPanelListDocument + ' .grid-content table');
                tableHeaderCol.each(function (idx, item) {
                    var column = tableHeader.eq(idx);
                    if ($(item).width() == 0) {
                        columnNoWidth.push(column.attr('data-column'));
                    } else {
                        totalWidth += $(item).width();
                    }
                    data[column.attr('data-column')] = $(item).width();
                });
                if (columnNoWidth.length > 0) {
                    var allWidth = tableContent.width() - totalWidth;
                    var widthCol = allWidth / columnNoWidth.length;
                    for (var g = 0; g < columnNoWidth.length; g++) {
                        data[columnNoWidth[g]] = widthCol;
                    }
                }
                $.cookie(cookieName, JSON.stringify(data), { expires: 7, path: "/", secure: true });
            }
        });
        bindContextShortCut();
        etip.initTooltip();
        etip.initNotifier();
        if (egov.cshtml.home.pagingByScroll) {
            var $rowGetDocumentOlder = $('<tr id="rowGetDocumentOlder"><td colspan="' + columnSettings.length + '" style="text-align:center"><img id="imgGetDocumentOlder" src="/Content/Images/ajax-loader.gif" width="24px" height="24px" style="display:none" /><a id="linkGetDocumentOlder" href="javascript:void(0)">Lấy thêm các văn bản, hồ sơ</a></td></tr>');
            $rowGetDocumentOlder.click(function () {
                if (!isPaging && !cacheDocuments[id].isLoadAllData) {
                    getDocumentOlder(id);
                }
            });
            $rowGetDocumentOlder.appendTo(idTableDocuments + " tbody");
        }
        //$('.grid-content').getNiceScroll().remove();
        egov.cshtml.home.documentProcessingLayout.initContent('center');
        //$('.grid-content').niceScroll(niceScrollOption);
    };

    /// <summary> Phân trang</summary>
    var bindPaging = function (id) {
        if (cacheDocuments[id].enablePaging) {
            if (egov.cshtml.home.pagingByScroll) {
                $(".grid-content").unbind('scroll');
                $(".grid-content").scroll(function () {
                    $('.grid-header-wrap').scrollLeft($(this).scrollLeft());
                    if (!isPaging && !cacheDocuments[id].isLoadAllData) {
                        if (this.offsetHeight + this.scrollTop >= this.scrollHeight - 50) {
                            getDocumentOlder(id);
                        }
                    }
                });
            }
        }
    };

    var getDocumentOlder = function (id) {
        isPaging = true;
        $('#imgGetDocumentOlder').show();
        $('#linkGetDocumentOlder').hide();
        var $selected = $("#" + id);
        var quicksearch = $('#SearchQuery').val();
        var lastDocument = _.min(quicksearch === '' ? cacheDocuments[id].documentsObject : currentQuickSearchResult, function (item) { return Date.parse(item.DateReceived); });
        var url = $selected.children('a').attr('href').replace('/Home/GetDocument/', '/Home/GetDocumentPaging/');

        $.post(url,
            {
                lastDate: lastDocument.DateReceived,
                quickSearch: quicksearch
            },
            function (data) {
                var stt = cacheDocuments[id].documentsObject.length;
                var newdocuments;
                try {
                    newdocuments = JSON.parse(data.documents);
                } catch (ex) {
                    newdocuments = [];
                }
                newdocuments = _.reject(newdocuments, function (item) {
                    return item.DocumentCopyId == lastDocument.DocumentCopyId;
                });
                if (newdocuments.length > 0) {
                    cacheDocuments[id].isLoadAllData = false;
                    $.each(newdocuments, function (index, item) {
                        cacheDocuments[id].documentsObject.push(item);
                    });
                } else {
                    cacheDocuments[id].isLoadAllData = true;
                }

                $('#imgGetDocumentOlder').hide();
                $('#linkGetDocumentOlder').show();
                $('#rowGetDocumentOlder').before(bindDocumentRows(newdocuments, cacheDocuments[id].columnSetting, stt));
                etip.initTooltip();
                $(idTableDocuments + " tr:odd").addClass("trodd");
                $('#rowGetDocumentOlder').removeClass('trodd');
                bindContextShortCut();
                var $dropdownDoctype = $('#doctypeid:visible');
                if ($dropdownDoctype.length > 0) {
                    var allDoctypeId = [];
                    $.each(cacheDocuments[id].documentsObject, function (index, item) {
                        allDoctypeId.push(item.DocTypeId);
                    });
                    allDoctypeId = egov.utilities.array.distinct(allDoctypeId);
                    var filterDoctypes = _.filter(doctypes, function (item) {
                        return _.contains(allDoctypeId, item.DocTypeId);
                    });
                    $.each(filterDoctypes, function (index, item) {
                        if ($dropdownDoctype.children('option[value=' + item.DocTypeId + ']').length === 0) {
                            $('#doctypeTemplate').tmpl(item).appendTo($dropdownDoctype);
                        }
                    });
                }
                isPaging = false;
            });
    };

    /// <summary> Bind dữ liệu thành từng dòng</summary>
    var bindDocumentRows = function (documents, columnSettings, stt) {
        if (!stt) {
            stt = 0;
        }
        var rows = $("<tbody>");
        var newcolumnSettings = _.filter(columnSettings, function (item) {
            return (item.ColumnName != "STT") && (item.ColumnName != "Checkmask") && (item.ColumnName != "Color");
        });

        for (var i = 0; i < documents.length; i++) {
            //rows += '<tr id="documentCopy_' + documents[i].DocumentCopyId + '" data-color="' + (documents[i].Color ? documents[i].Color : '') + '" class="' + (!documents[i].IsViewed ? 'documentUnread' : '') + '' + (documents[i].Color ? ' documentColor' + documents[i].Color : '') + '" ondblclick="openDocument(' + documents[i].DocumentCopyId + ', \'' + documents[i].Compendium + '\')"><td style="text-align:center">' + (stt + 1) + '</td>';
            var row = $("<tr></tr>");
            if (!documents[i].IsFile) {
                row.attr("id", "documentCopy_" + documents[i].DocumentCopyId);
                row.attr("data-color", (documents[i].Color ? documents[i].Color : ''));
                row.addClass(!documents[i].IsViewed ? 'documentUnread' : '');
                row.attr("ondblclick", "openDocument('" + documents[i].DocumentCopyId + "','" + documents[i].Compendium + "')");
            } else {
                row.attr("id", "attachment_" + documents[i].DocumentCopyId);
                row.attr("ondblclick", "egov.cshtml.home.plugin.openAttachment(" + documents[i].DocumentCopyId + ")");
            }

            // thêm cột checkbox
            //row.append($("<td style='text-align: center;'>").append("<input type='checkbox' />"));
            row.append($("<td style='text-align: center;'>").text(stt + 1));

            // thêm cột màu văn bản
            var colorCol = $("<td style='text-align: center'></td>");
            if (!documents[i].IsFile) {
                if (documents[i].IsDocumentImportant == "0") {
                    colorCol.append('<span class="document-important qtooltip" index="' + i + '" title="Gắn quan trọng cho văn bản này"  onclick="documentImportant(this,' + documents[i].DocumentCopyId + ');" ></span>');
                }
                else {
                    colorCol.append('<span class="document-important important qtooltip" index="' + i + '" title="Bỏ gắn quan trọng văn bản này" onclick="documentImportant(this,' + documents[i].DocumentCopyId + ');" ></span>');
                }
            }
            //
            var colorTitle = getColorTitle(documents[i].Color);
            if (!documents[i].IsFile) {
                colorCol.append('<span class="document-color ' + (documents[i].Color ? 'documentColor' + documents[i].Color : '') + ' qtooltip" title="' + colorTitle + '"></span>');
            } else {
                var ext = documents[i].Compendium ? documents[i].Compendium.split('.').pop() : '';
                if (ext === '7z') {
                    ext = 'sevenz';
                }
                colorCol.append('<span class="icon-attachment ' + ext + '"></span>');
            }
            row.append(colorCol);

            // Hiển thị các cột theo cấu hình
            $.each(newcolumnSettings, function (index, setting) {
                var data = documents[i][setting.ColumnName];
                if (regexUtcDate.test(data)) {
                    var array = data.split(/[^0-9]/);
                    var date = new Date(array[0], array[1] - 1, array[2], array[3], array[4], array[5]);
                    row.append('<td>' + Globalize.format(new Date(date), "dd/MM/yyyy hh:mm tt") + '</td>');
                } else {
                    row.append('<td>' + (data ? data : '') + '</td>');
                }
            });
            stt++;
            row.appendTo(rows);
        }
        return rows.html();
    };

    var updateTotalUnread = function (item) {
        if (item.id != 0) {
            var $selectedNode = $(item);
            var querystring = $selectedNode.children("a:first").attr("href").split("/");
            $.get("/Home/GetTotalDocumentUnread/" + querystring[querystring.length - 1],
                {},
                function (unread) {
                    if (unread) {
                        if (unread > 0) {
                            $selectedNode.find("span:first").text(unread);
                        } else {
                            $selectedNode.find("span:first").text(" ");
                        }
                        $selectedNode.find("span:first").attr("data-totalunread", unread);
                    }
                });
        }
    };

    var bindContextShortCut = function () {
        var selector = idTableDocuments + ' tbody tr:not(tr#rowGetDocumentOlder)';
        $(selector).unbind('mouseup');
        $(selector).mouseup(function (e) {
            if (e.button === 2) {
                $.contextMenu('destroy', selector);
                var $row = $(this);
                if (!$row.hasClass("rowSelected")) {
                    changeColor($row);
                }
                var rowId = $row.attr("id");
                var clickId, name;
                if (rowId.indexOf('documentCopy_') === 0) {
                    clickId = rowId.replace('documentCopy_', '');
                    name = 'văn bản';
                } else {
                    clickId = rowId.replace('attachment_', '');
                    name = 'tài liệu';
                }
                var arrPermission = [];
                var listIds = [];
                var isShowContextMenuForAttachment;
                $(idTableDocuments + ' tbody tr.rowSelected').each(function (i, item) {
                    if ($(item).attr("id").indexOf('attachment_') === 0) {
                        isShowContextMenuForAttachment = true;
                        return false;
                    }
                    var id = $(item).attr("id").replace('documentCopy_', '');
                    listIds.push(parseInt(id));
                });
                if (isShowContextMenuForAttachment) {
                    $.contextMenu({
                        selector: selector,
                        trigger: 'right',
                        build: function () {
                            var items = {
                                open: {
                                    name: "Xem chi tiết " + name,
                                    icon: "openattach",
                                    callback: function () {
                                        var $self = $(this);
                                        if ($self.attr("id").indexOf('attachment_') === 0) {
                                            egov.cshtml.home.plugin.openAttachment(clickId);
                                        } else {
                                            $self.dblclick();
                                        }
                                    }
                                },
                            };
                            if (rowId.indexOf('attachment_') === 0) {
                                items.download = {
                                    name: "Tải " + name,
                                    icon: "download",
                                    callback: function () {
                                        var link = '/StorePrivate/DownloadAttachment/' + clickId;
                                        $.fileDownload(link, {
                                            failCallback: function (response) {
                                                var html = $(response);
                                                try {
                                                    var json = JSON.parse(html.text());
                                                    eGovMessage.show(json.error);
                                                } catch (e) {
                                                    eGovMessage.show('"@Localizer("Common.Error")" khi tải file');
                                                }
                                            }
                                        });
                                    }
                                };
                            }
                            items.speperator = '--------',
                            items.removeDocumentInStorePrivate = {
                                name: "Loại " + name + " khỏi hồ sơ",
                                icon: "remove",
                                callback: function () {
                                    var $self = $(this);
                                    var storeSelected = $($(egov.cshtml.home.idPanelTreeStorePrivate).jstree('get_selected'));
                                    if (storeSelected.hasClass('storeitem')) {
                                        eGovMessage.show(
                                            'Bạn chắc chắn muốn loại ' + name + ' này khỏi hồ sơ chứ?',
                                            null,
                                            eGovMessage.messageButtons.YesNo,
                                            function () {
                                                var url = '/Document/RemoveDocumentInStorePrivate';
                                                var token = $("input[name='__RequestVerificationToken']", "#DocumentRemoveDocumentInStorePrivate").val();
                                                var data = {
                                                    storePrivateId: storeSelected.attr('data-storeprivateid'),
                                                    documentCopyId: clickId,
                                                    __RequestVerificationToken: token
                                                };
                                                if ($self.attr("id").indexOf('attachment_') === 0) {
                                                    var token = $("input[name='__RequestVerificationToken']", "#StorePrivateRemoveAttachment").val();
                                                    url = '/StorePrivate/RemoveAttachment';
                                                    data = { id: clickId, __RequestVerificationToken: token };
                                                }
                                                $.post(url,
                                                    data,
                                                    function (result) {
                                                        if (result) {
                                                            if (result.error) {
                                                                eGovMessage.notification(result.message, eGovMessage.messageTypes.error);
                                                            } else {
                                                                eGovMessage.notification("Loại " + name + " khỏi hồ sơ thành công!", eGovMessage.messageTypes.success);
                                                                $self.remove();
                                                            }
                                                        }
                                                    });
                                            }
                                        );
                                    }
                                    else {
                                        eGovMessage.show("Bạn không có quyền xóa tài liệu này");
                                    }
                                }
                            }
                            return {
                                items: items
                            };
                        }
                    });
                    $(selector).contextMenu({
                        x: e.clientX,
                        y: e.clientY
                    });
                    return;
                }

                var loading = $('<img src="/Content/Images/ajax-loader.gif" width="20" height="20" style="position:fixed; z-index:100; top:' + (e.clientY - 10) + 'px; left: ' + (e.clientX - 10) + 'px;" />');
                if (!$.support.fixedPosition) {
                    loading.css({
                        'position': 'absolute'
                    });
                }
                loading.appendTo('body');
                $.ajax({
                    type: "GET",
                    data: { documentCopyIds: listIds },
                    url: '/Document/GetDocumentPermission',
                    traditional: true,
                    success: function (data) {
                        if (data.error) {
                            eGovMessage.notification(data.error, eGovMessage.messageTypes.error);
                        } else {
                            arrPermission = _.uniq(data);
                            var items = egov.document.permission.getContextmenu(arrPermission, listIds, clickId);
                            if (isListStoreDocument) {
                                items.speperator = "---------";
                                items.removeDocumentInStorePrivate = {
                                    name: "Loại văn bản khỏi hồ sơ",
                                    icon: "remove",
                                    callback: function () {
                                        var self = this;
                                        var storeSelected = $($(egov.cshtml.home.idPanelTreeStorePrivate).jstree('get_selected'));
                                        if (storeSelected.hasClass('storeitem')) {
                                            eGovMessage.show(
                                                'Bạn chắc chắn muốn loại văn bản này khỏi hồ sơ chứ?',
                                                null,
                                                eGovMessage.messageButtons.YesNo,
                                                function () {
                                                    var token = $("input[name='__RequestVerificationToken']", "#DocumentRemoveDocumentInStorePrivate").val();
                                                    $.post('/Document/RemoveDocumentInStorePrivate',
                                                        {
                                                            storePrivateId: storeSelected.attr('data-storeprivateid'),
                                                            documentCopyId: clickId,
                                                            __RequestVerificationToken: token
                                                        },
                                                        function (result) {
                                                            if (result) {
                                                                if (result.error) {
                                                                    eGovMessage.notification(result.message, eGovMessage.messageTypes.error);
                                                                } else {
                                                                    eGovMessage.notification("Loại văn bản khỏi hồ sơ thành công!", eGovMessage.messageTypes.success);
                                                                    $(self).remove();
                                                                }
                                                            }
                                                        });
                                                }
                                            );
                                        }
                                    }
                                };
                            }
                            $.contextMenu({
                                selector: selector,
                                trigger: 'right',
                                build: function () {
                                    return {
                                        items: items
                                    };
                                }
                            });
                            $(selector).contextMenu({
                                x: e.clientX,
                                y: e.clientY
                            });
                        }
                    },
                    error: function (xhr) {
                        eGovMessage.notification(xhr.statusText, eGovMessage.messageTypes.error);
                    },
                    complete: function () {
                        loading.remove();
                    }
                });
            }

        });
        $("body").unbind('keydown');
        $("body").keydown(function (e) {
            if (e.ctrlKey && (e.keyCode == 65 || e.keyCode == 97)) {
                e.preventDefault();
                if ($(idTableDocuments + ' tbody tr').hasClass('rowSelected') && egov.cshtml.home.tab.getActiveTab().attr('egovtab') == 'document') {
                    $(idTableDocuments + ' tbody tr').each(function (i, item) {
                        var $row = $(this);
                        if ($row.attr("data-color") != '') {
                            var color = $row.attr("data-color");
                            $row.addClass('documentBgColor' + color + ' rowSelected');
                        } else {
                            $(item).addClass("rowSelected");
                        }
                    });
                } else if ($('#listRelations').length > 0 && $('#listRelationsAdded').length > 0) {
                    if ($('#listRelations tbody tr').hasClass('rowSelected')) {
                        $('#listRelations tbody tr').removeClass('rowSelected').addClass('rowSelected');
                    }
                    if ($('#listRelationsAdded tbody tr').hasClass('rowSelected')) {
                        $('#listRelationsAdded tbody tr').removeClass('rowSelected').addClass('rowSelected');
                    }
                }
            }
        });
        bindRowClickAndShortcut();
    };

    var bindRowClickAndShortcut = function () {
        var selector = idTableDocuments + ' tbody tr:not(tr#rowGetDocumentOlder)';
        if (egov.cshtml.home.showQuickView) {
            quickView($(idTableDocuments + ' tbody tr:first').attr('id'));
            if (!$(idTableDocuments + ' tbody tr:first').hasClass('rowSelected')) {
                $(idTableDocuments + ' tbody tr:first').addClass('rowSelected');
            }
        }
        $(selector).unbind('click');
        $(selector).click(function (e) {
            e.preventDefault();
            var $row = $(this);
            if (e.shiftKey) {
                rowIdSelected = [];
                //Shift-Click
                var currentIndex = $row.index();
                var beginRow = rowIndex <= currentIndex ? rowIndex : currentIndex;
                var endRow = rowIndex <= currentIndex ? currentIndex : rowIndex;

                for (var i = beginRow; i <= endRow; i++) {
                    var $rowShift = $(idTableDocuments + ' tbody tr:eq(' + i + ')');
                    setRowSelected($rowShift);
                    rowIdSelected.push($rowShift.attr('id'));
                }

                $(selector).each(function (idx, item) {
                    var index = $(item).index();
                    if (index > endRow || index < beginRow) {
                        removeRowSelected($(item));
                    }
                });
                if (egov.cshtml.home.showQuickView) {
                    quickView($(idTableDocuments + ' tbody tr:eq(' + currentIndex + ')').attr('id'));
                }
                return;
            } else if (e.ctrlKey) {
                //Ctrl+Click
                rowIndex = $row.index();
                if ($row.hasClass("rowSelected")) {
                    removeRowSelected($row);
                    rowIdSelected = _.reject(rowIdSelected, function (item) {
                        return item == $row.attr('id');
                    });
                    if (egov.cshtml.home.showQuickView) {
                        quickView(rowIdSelected[rowIdSelected.length - 1]);
                    }
                } else {
                    setRowSelected($row);
                    rowIdSelected.push($row.attr('id'));
                    if (egov.cshtml.home.showQuickView) {
                        quickView($row.attr('id'));
                    }
                }
                return;
            } else {
                rowIndex = $row.index();
                rowIdSelected = [];
                rowIdSelected.push($row.attr('id'));
                changeColor(this);
                if (egov.cshtml.home.showQuickView) {
                    quickView($row.attr('id'));
                }
                var detectBrowser = new browserDetect();
                detectBrowser.detect();
                var os = detectBrowser.getOS();
                if (os === 'iPhone' || os === 'iPad' || os == 'Android' || os === 'Windows Phone') {
                    eval($row.attr('ondblclick'));
                }
            }
        });
    };

    var quickView = function (id) {
        if (id.indexOf('documentCopy_') === 0) {
            id = id.replace("documentCopy_", "");
            var functionId = getSelectedNode().attr('id');
            var document = cacheDocuments[functionId] ? _.find(cacheDocuments[functionId].documentsObject, function (item) {
                return item.DocumentCopyId == id;
            }) : null;
            var isreload = false;
            if (document) {
                if (cacheQuickViewDocuments[id]) {
                    $(".document-preview").html($("#quickviewTemplate").tmpl(cacheQuickViewDocuments[id].document));
                    if (document.DateReceived != cacheQuickViewDocuments[id].date) {
                        isreload = true;
                    }
                } else {
                    isreload = true;
                }
            } else {
                isreload = true;
            }
            // TienBV: xét kiểu này thì những văn bản phát hành ko cập nhậtdc 
            //if (isreload) {
            $.get('/Home/QuickViewDocument/' + id, function (data) {
                if (data) {
                    $(".document-preview").html($("#quickviewTemplate").tmpl(data));
                    cacheQuickViewDocuments[id] = { document: data, date: document ? document.DateReceived : '' };
                }
            }).fail(function () {
                eGovMessage.notification('@Localizer("Common.Error")', eGovMessage.messageTypes.error);
            });
            //}
        } else {
            id = id.replace("attachment_", "");
            $.get('/StorePrivate/GetAttachment/' + id, function (data) {
                if (data) {
                    $(".document-preview").html($("#quickviewAttachmentTemplate").tmpl(data));
                }
            }).fail(function () {
                eGovMessage.notification('@Localizer("Common.Error")', eGovMessage.messageTypes.error);
            });
        }
    };

    var paging = function (page, pagesize) {
        var quicksearch = $('#SearchQuery').val();
        var $selected = getSelectedNode();
        var id = $selected.attr('id');
        var url = $selected.children('a').attr('href').replace('/Home/GetDocument/', '/Home/GetDocumentPaging/');
        var token = $("input[name='__RequestVerificationToken']", "#HomeGetDocumentPaging").val();
        $.post(url, {
            quickSearch: quicksearch,
            page: page,
            pagesize: pagesize,
            __RequestVerificationToken: token
        },
        function (data) {
            var documents;
            try {
                documents = JSON.parse(data.documents);
                currentPageListDocuments = data.page;
                currentPageSizeListDocuments = data.pageSize;
                totalRecordsListDocuments = data.totalDocuments;
            } catch (ex) {
                documents = [];
            }
            if (currentPageListDocuments === 1) {
                cacheDocuments[id].documentsObject = documents;
            }
            $('#tblListDocument tbody').html(bindDocumentRows(documents, cacheDocuments[id].columnSetting, (page - 1) * pagesize));
            $(idTableDocuments + " tr:odd").addClass("trodd");
            bindContextShortCut();
            $('.grid-content').scrollTop(0);
            var totalPages = Math.ceil(totalRecordsListDocuments / currentPageSizeListDocuments);
            $('.grid-pager li:first').text('Trang ' + currentPageListDocuments + ' / ' + totalPages);
            if (currentPageListDocuments > 1) {
                $('#firstPage img').removeClass('disabled');
                $('#prevPage img').removeClass('disabled');
            } else {
                $('#firstPage img').addClass('disabled');
                $('#prevPage img').addClass('disabled');
            }
            if (currentPageListDocuments < totalPages) {
                $('#lastPage img').removeClass('disabled');
                $('#nextPage img').removeClass('disabled');
            } else {
                $('#lastPage img').addClass('disabled');
                $('#nextPage img').addClass('disabled');
            }
            egov.cshtml.home.listdocuments.pageSize(pagesize);
        });
    };

    var removeRowSelected = function (row) {
        row.removeClass("rowSelected");
    };

    var setRowSelected = function (row) {
        $(row).addClass("rowSelected");
    };

    var changeColor = function (target) {
        var row = $(target);
        setRowSelected(row);
        row.siblings('tr').removeClass("rowSelected");
    };

    var bindSearchUserJoined = function (listUser) {
        $("#userJoined").autocomplete({
            minLength: 1,
            source: listUser,
            focus: function () {
                return false;
            },
            selectFirst: true,
            select: function (event, ui) {
                $("#userJoined").val('');
                var exist = _.find(userIdJoined, function (id) {
                    return id === ui.item.value;
                });
                if (!exist) {
                    userIdJoined.push(ui.item.value);
                    addUserJoined(ui.item);
                }
                return false;
            }
        })
        .data("autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>")
                .data("item.autocomplete", item)
                .append("<a>" + item.label + "</a>")
                .appendTo(ul);
        };
        $(".ui-autocomplete-input").bind("autocompleteopen", function () {
            var autocomplete = $(this).data("autocomplete"), menu = autocomplete.menu;

            if (!autocomplete.options.selectFirst) {
                return;
            }
            menu.activate($.Event({ type: "mousehover" }), menu.element.children().first());
        });
    };

    var addUserJoined = function (item) {
        var $newtr = $('<tr></tr>');
        $newtr.append('<td>' + item.label + '</td>');
        var $newtdDelete = $('<td style="width:30px"></td>');
        var $a = $('<a href="javascript:void(0)">Xóa</a>');
        $a.click(function () {
            $(this).parents('tr:first').remove();
            userIdJoined = _.reject(userIdJoined, function (id) {
                return id === item.value;
            });
        });
        $newtdDelete.append($a);
        $newtr.append($newtdDelete);
        $('#tblUserJoined').append($newtr);
        $("#panelUserJoined").getNiceScroll().resize();
    };

    var addStorePrivateAttachment = function (id) {
        var settings = {}; // dialog setting
        settings.width = 425;
        settings.height = 230;
        settings.title = "Thêm tài liệu";
        settings.buttons = [
        {
            text: "Thêm",
            click: function () {
                if ($('#attachmentName').val() !== '') {
                    $('#uploadStorePrivateAttachment').click();
                }
            }
        },
        {
            text: "Bỏ qua",
            click: function () {
                $("#addStorePrivateAttachment").dialog('close');
            }
        }
        ];
        $('#attachmentName').val('');
        $('#descStorePrivateAttachment').val('');
        $("#addStorePrivateAttachment .fileinput-button").html('Chọn tệp <input id="storePrivateAttachment" type="file" name="file" class="file" data-url="/StorePrivate/AddAttachment">');
        $('#uploadStorePrivateAttachment').unbind('click');
        initFileUploadStorePrivateAttachment(id);
        var dialog = new dialogAdapter();
        dialog.openexist($("#addStorePrivateAttachment"), settings, null, null, false);
    };

    var initFileUploadStorePrivateAttachment = function (id) {
        $('#storePrivateAttachment').fileupload({
            dataType: 'json',
            autoUpload: false,
            add: function (e, data) {
                var file = data.files[0];
                $('#attachmentName').val(file.name);
                $('#uploadStorePrivateAttachment').unbind('click');
                $('#uploadStorePrivateAttachment').click(function () {
                    data.submit();
                });
            },
            start: function () {
                $("#addStorePrivateAttachment").blockpanel({ text: "Đang tải...", borderWidth: 1 });
                $('#toolbar').block({
                    message: '',
                    css: {
                        border: 'none'
                    }
                });
            },
            stop: function () {
                $('#addStorePrivateAttachment').unblockpanel();
                $("#addStorePrivateAttachment").dialog('close');
            },
            done: function () {
                $('li[data-storeprivateid=' + id + '] a').click();
            },
            fail: function () {
                eGovMessage.show('"@Localizer("Common.Error")" khi tải tệp lên');
            }
        }).bind('fileuploadsubmit', function (e, data) {
            data.formData = { id: id, desc: $('#descStorePrivateAttachment').val() };
        });
    };

    var createStorePrivate = function (parentId) {
        var settings = {}; // dialog setting

        settings.width = 425;
        settings.height = 400;
        settings.title = "Tạo mới hồ sơ";
        settings.buttons = [
        {
            text: "Tạo mới",
            click: function () {
                if ($.trim($('#storePrivateName').val()) === '') {
                    $('#storePrivateName').addClass('input-validation-error').focus().siblings('span').show();
                } else {
                    $('#storePrivateName').removeClass('input-validation-error').siblings('span').hide();
                    var token = $("input[name='__RequestVerificationToken']", "#StorePrivateCreate").val();
                    var par
                    $.ajax({
                        type: "POST",
                        url: '/StorePrivate/Create',
                        traditional: true,
                        data: {
                            storePrivateName: $('#storePrivateName').val(),
                            descStorePrivate: $('#descStorePrivate').val(),
                            parentId: parentId,
                            userIdJoined: userIdJoined,
                            __RequestVerificationToken: token
                        },
                        success: function (data) {
                            if (data) {
                                if (data.error) {
                                    eGovMessage.notification(data.message, eGovMessage.messageTypes.error);
                                } else if (data.validateMessage) {
                                    $('#storePrivateName').addClass('input-validation-error').focus().siblings('span').show();
                                    eGovMessage.notification(data.validateMessage, eGovMessage.messageTypes.error);
                                } else {
                                    $(egov.cshtml.home.idPanelTreeStorePrivate)
                                        .jstree(
                                            'create',
                                            parentId ? $('#storePrivate li[data-storeprivateid=' + parentId + ']') : $('#storePrivate'),
                                            'last',
                                            {
                                                'attr': {
                                                    'data-storeprivateid': data.id,
                                                    'class': 'storeitem active'
                                                },
                                                'data': {
                                                    'title': $('#storePrivateName').val(),
                                                    'attr': {
                                                        //'onclick': 'getWorkflow',
                                                        'href': '#'
                                                    }
                                                }
                                            },
                                            null,
                                            true
                                        );
                                    $('#treeStorePrivateSave')
                                        .jstree(
                                            'create',
                                            parentId ? $('#storePrivateSave li[data-storeprivateid=' + parentId + ']') : $('#storePrivateSave'),
                                            'last',
                                            {
                                                'attr': {
                                                    'data-storeprivateid': data.id,
                                                },
                                                'data': {
                                                    'title': $('#storePrivateName').val(),
                                                    'attr': {
                                                        //'onclick': 'getWorkflow',
                                                        'href': '#'
                                                    }
                                                }
                                            },
                                            null,
                                            true
                                        );
                                    $("#createStorePrivate").dialog('close');
                                    createContextMenuForStorePrivate();
                                }
                            }
                        },
                        error: function () {
                            eGovMessage.notification("Có lỗi trong quá trình tạo mới hồ sơ cá nhân.", eGovMessage.messageTypes.error);
                        }
                    });
                }
            }
        },
        {
            text: "Bỏ qua",
            click: function () {
                $("#createStorePrivate").dialog('close');
            }
        }
        ];
        userIdJoined = [];
        $('#storePrivateName').val('');
        $('#descStorePrivate').val('');
        if (parentId) {
            $('#storePrivateParentId').val(parentId);
        } else {
            $('#storePrivateParentId').val('');
        }
        $('#tblUserJoined tr').remove();
        var dialog = new dialogAdapter();
        if (!allUsers) {
            $.get('/Transfer/GetAllUsers', {}, function (data) {
                if (data) {
                    allUsers = JSON.parse(data);
                    bindSearchUserJoined(_.filter(allUsers, function (user) { return user.value != userId; }));
                    dialog.openexist($("#createStorePrivate"), settings, null, null, false);
                    $('#panelUserJoined').niceScroll(niceScrollOption);
                }
            });
        } else {
            dialog.openexist($("#createStorePrivate"), settings, null, null, false);
        }
    };

    var getStorePrivate = function (id) {
        $.get('/StorePrivate/Get', { id: id }, function (result) {
            if (result) {
                if (result.error) {
                    eGovMessage.notification(result.message, eGovMessage.messageTypes.error);
                } else {
                    $('#storePrivateName').val(result.storePrivateName);
                    $('#descStorePrivate').val(result.descStorePrivate);
                    $('#tblUserJoined tr').remove();
                    userIdJoined = result.userIdJoined;
                    var users = _.filter(allUsers, function (u) {
                        return _.contains(userIdJoined, u.value);
                    });
                    $.each(users, function (i, item) {
                        addUserJoined(item);
                    });
                    var dialog = new dialogAdapter();
                    var settings = {}; // dialog setting
                    settings.width = 425;
                    settings.height = 370;
                    settings.title = "Cập nhật hồ sơ";
                    settings.buttons = [
                    {
                        text: "Cập nhật",
                        click: function () {
                            if ($.trim($('#storePrivateName').val()) === '') {
                                $('#storePrivateName').addClass('input-validation-error').focus().siblings('span').show();
                            } else {
                                $('#storePrivateName').removeClass('input-validation-error').siblings('span').hide();
                                var token = $("input[name='__RequestVerificationToken']", "#StorePrivateUpdate").val();
                                $.ajax({
                                    type: "POST",
                                    url: '/StorePrivate/Update',
                                    traditional: true,
                                    data: {
                                        id: id,
                                        storePrivateName: $('#storePrivateName').val(),
                                        descStorePrivate: $('#descStorePrivate').val(),
                                        userIdJoined: userIdJoined,
                                        __RequestVerificationToken: token
                                    },
                                    success: function (data) {
                                        if (data) {
                                            if (data.error) {
                                                eGovMessage.notification(data.message, eGovMessage.messageTypes.error);
                                            } else if (data.validateMessage) {
                                                $('#storePrivateName').addClass('input-validation-error').focus().siblings('span').show();
                                                eGovMessage.notification(data.validateMessage, eGovMessage.messageTypes.error);
                                            } else {
                                                $('li[data-storeprivateid=' + id + ']').children('a').html('<ins class="jstree-icon">&nbsp;</ins>' + $('#storePrivateName').val());
                                                $("#createStorePrivate").dialog('close');
                                            }
                                        }
                                    },
                                    error: function () {
                                        eGovMessage.notification("Có lỗi trong quá trình cập nhật hồ sơ cá nhân.", eGovMessage.messageTypes.error);
                                    }
                                });
                            }
                        }
                    },
                    {
                        text: "Bỏ qua",
                        click: function () {
                            $("#createStorePrivate").dialog('close');
                        }
                    }
                    ];
                    dialog.openexist($("#createStorePrivate"), settings, null, null, false);
                    $('#panelUserJoined').getNiceScroll().remove();
                    $('#panelUserJoined').niceScroll(niceScrollOption);
                }
            }
        });
    };

    var createContextMenuForStoreFolder = function () {
        $.contextMenu('destroy', '#treeStorePrivate #storePrivate');
        $.contextMenu({
            selector: '#treeStorePrivate #storePrivate',
            zIndex: 3,
            build: function () {
                var options = {
                    items: {
                        "addStorePrivate": {
                            name: "Tạo hồ sơ mới",
                            icon: "add2",
                            callback: function () {
                                createStorePrivate();
                            }
                        }
                    }
                };
                return options;
            }
        });
    };

    var createContextMenuForStorePrivate = function () {
        $.contextMenu('destroy', '#treeStorePrivate li.storeitem');
        $.contextMenu({
            selector: '#treeStorePrivate li.storeitem',
            zIndex: 3,
            build: function ($trigger) {
                var isActivated = $trigger.hasClass('active');
                var options = {
                    items: {
                        "addStorePrivate": {
                            name: "Tạo hồ sơ mới",
                            icon: "add2",
                            callback: function () {
                                var id = $trigger.attr('data-storeprivateid');
                                createStorePrivate(id);
                            }
                        },
                        "addFile": {
                            name: "Thêm tài liệu",
                            icon: "add",
                            callback: function () {
                                var id = $trigger.attr('data-storeprivateid');
                                addStorePrivateAttachment(id);
                            }
                        },
                        "closeStorePrivate": {
                            name: isActivated ? "Đóng hồ sơ" : "Mở hồ sơ",
                            icon: isActivated ? "cancel" : "accept",
                            callback: function () {
                                var self = this;
                                var url = isActivated ? "/StorePrivate/Close" : "/StorePrivate/Open";
                                var tokenId = isActivated ? "#StorePrivateClose" : "#StorePrivateOpen";
                                var token = $("input[name='__RequestVerificationToken']", tokenId).val();
                                eGovMessage.show(
                                    'Bạn có chắc muốn ' + (isActivated ? "đóng" : "mở") + ' hồ sơ này không',
                                    null,
                                    eGovMessage.messageButtons.YesNo,
                                    function () {
                                        $.post(url, {
                                            id: $(self).attr('data-storeprivateid'),
                                            __RequestVerificationToken: token
                                        }, function (data) {
                                            if (data) {
                                                if (data.error) {
                                                    eGovMessage.notification(data.message, eGovMessage.messageTypes.error);
                                                } else {
                                                    if (isActivated) {
                                                        $(self).removeClass('active');
                                                    } else {
                                                        $(self).addClass('active');
                                                    }
                                                    eGovMessage.notification((isActivated ? "Đóng" : "Mở") + ' hồ sơ thành công', eGovMessage.messageTypes.success);
                                                }
                                            }
                                        }).fail(function () {
                                            eGovMessage.notification('@Localizer("Common.Error")', eGovMessage.messageTypes.error);
                                        });
                                    }
                                );
                            }
                        },
                        "editStorePrivate": {
                            name: "Cập nhật hồ sơ",
                            icon: "edit",
                            callback: function () {
                                var self = this;
                                if (!allUsers) {
                                    $.get('/Transfer/GetAllUsers', {}, function (data) {
                                        if (data) {
                                            allUsers = JSON.parse(data);
                                            bindSearchUserJoined(_.filter(allUsers, function (user) { return user.value != userId; }));
                                            getStorePrivate($(self).attr('data-storeprivateid'));
                                        }
                                    });
                                } else {
                                    getStorePrivate($(self).attr('data-storeprivateid'));
                                }
                            }
                        },
                        "sep1": "---------",
                        "deleteStorePrivate": {
                            name: "Xóa hồ sơ",
                            icon: "remove",
                            callback: function () {
                                var self = this;
                                eGovMessage.show(
                                    'Bạn có chắc muốn xóa hồ sơ này không',
                                    null,
                                    eGovMessage.messageButtons.YesNo,
                                    function () {
                                        var token = $("input[name='__RequestVerificationToken']", "#StorePrivateDelete").val();
                                        $.post('/StorePrivate/Delete', {
                                            id: $(self).attr('data-storeprivateid'),
                                            __RequestVerificationToken: token
                                        }, function (data) {
                                            if (data) {
                                                if (data.error) {
                                                    eGovMessage.notification(data.message, eGovMessage.messageTypes.error);
                                                } else {
                                                    eGovMessage.notification('Xóa hồ sơ thành công', eGovMessage.messageTypes.success);
                                                    $(egov.cshtml.home.idPanelTreeStorePrivate).jstree('remove', $(self));
                                                    $(egov.cshtml.home.idPanelTreeStorePrivate).jstree("deselect_node");
                                                }
                                            }
                                        }).fail(function () {
                                            eGovMessage.notification('"@Localizer("Common.Error")" khi xóa hồ sơ', eGovMessage.messageTypes.error);
                                        });
                                    }
                                );
                            }
                        }
                    }
                };
                return options;
            }
        });
    };

    var createContextMenuForStoreShare = function () {
        $.contextMenu('destroy', '#treeStorePrivate li.storeshareitem');
        $.contextMenu({
            selector: '#treeStorePrivate li.storeshareitem',
            zIndex: 3,
            build: function ($trigger) {
                var options = {
                    items: {
                        "addFile": {
                            name: "Thêm tài liệu",
                            icon: "add",
                            callback: function () {
                                var id = $trigger.attr('data-storeprivateid');
                                addStorePrivateAttachment(id);
                            }
                        }
                    }
                };
                return options;
            }
        });
    };

    var getKeys = function (obj) {
        var keys = [];
        for (var key in obj) {
            keys.push(key);
        }
        return keys;
    };

    var addDocumentContinue = function (dtype, title, key, acceptFileTypes, fullname, username) {
        egov.cshtml.home.tab.add(dtype, title, 0, egov.cshtml.home.tabIcons.newDocument, function (frameName) {
            onloadDocumentContinue(frameName, dtype, key, acceptFileTypes, fullname, username);
        }, function () {
            listDocumentWaitTransfer[dtype][key].splice(0, 1);
            if (listDocumentWaitTransfer[dtype][key].length > 0) {
                addDocumentContinue(dtype, title, key, acceptFileTypes, fullname, username);
            }
        });
    };

    var onloadDocumentContinue = function (frameName, dtype, key, acceptFileTypes, fullname, username) {
        var fileName = listDocumentWaitTransfer[dtype][key][0];
        var file, jsonFile;
        if (fileName.indexOf('.pdf', fileName.length - 4) !== -1) {
            jsonFile = plugin.readPdfToImage(fileName, egov.cshtml.home.transferMultiDocument.startImage, egov.cshtml.home.transferMultiDocument.endImage, egov.cshtml.home.transferMultiDocument.bitcount, egov.cshtml.home.transferMultiDocument.quality, egov.cshtml.home.transferMultiDocument.grayscale);
        } else {
            jsonFile = plugin.readImage(fileName, egov.cshtml.home.transferMultiDocument.startImage, egov.cshtml.home.transferMultiDocument.endImage, egov.cshtml.home.transferMultiDocument.bitcount, egov.cshtml.home.transferMultiDocument.quality, egov.cshtml.home.transferMultiDocument.grayscale);
        }
        if (jsonFile) {
            file = JSON.parse(jsonFile);
            var frame = document.getElementById(frameName).contentWindow;
            frame.egov.cshtml.document.allowTransferMultiDocument = false;
            var split = fileName.split('\\');
            frame.egov.cshtml.document.uploadImageScan([{ name: split[split.length - 1], value: file.attachment }], acceptFileTypes, fullname, username);
            if (!frame.CKEDITOR.instances.editor1) {
                frame.egov.cshtml.document.enableEditor($('#' + frameName).contents().find('#mainForm .content')[0], function () {
                    insertImageToContent(frame, file.images, 'jpg');
                });
            } else {
                insertImageToContent(frame, file.images, 'jpg');
            }
        }
    };

    var createObjectTree = function (list) {
        list = _.sortBy(list, function (o) { return o.Level; });
        for (var i = list.length - 1; i >= 0; i--) {
            var parent = _.find(list, function (o) { return o.StorePrivateId == list[i].ParentId; });
            if (parent) {
                if (!parent.Children) {
                    parent.Children = [];
                }
                parent.Children.push(list[i]);
            } else {
                list[i].root = true;
            }
        }
        list = _.filter(list, function (o) { return o.root; });
        return list;
    };

    if (!egov.cshtml) {
        egov.cshtml = {};
    }

    egov.cshtml.home = {
        documentProcessingLayout: null,
        searchDocumentLayout: null,
        contentLayout: null,
        showQuickView: true,
        pagingByScroll: true,
        idPanelListDocument: '',
        idPanelTreeDocument: '#treeDocument',
        idPanelTreeStorePrivate: '#treeStorePrivate',
        doctypes: [],
        tabIcons: tabIcons,
        userid: null,
        fullnameAndEmail: ''
    };

    /// <summary> Class xu ly Tab tren giao dien trang chu</summary>
    egov.cshtml.home.tab = {
        config: {
            documentTabContentId: 'documentProcessing',
            reportTabContentId: 'report',
            searchTabContentId: 'searchDocument',
            printTabContentId: 'printTab',
            profileConfigTabContentId: 'profileConfig',
            settingTabContentId: 'setting',
            changePasswordTabContentId: 'changePassword',
            isSaveOpenTab: true,
            userId: null
        },
        /// <summary> Khởi tạo tab ()</summary>
        /// <param name="mainTabId">Id của thẻ chứa tab</param>
        /// <param name="mainContentId">Id của thẻ chứa nội dung tab</param>
        /// <param name="option">Cấu hình tùy chọn</param>
        init: function (mainTabId, mainContentId, option) {
            $mainTab = $(mainTabId);
            $mainContent = $(mainContentId);
            userId = option.userId;

            $.extend(this.config, option || {});

            $ulmain = $('<ul class="tabs-ul"></ul>');
            $ulmain.attr('class', 'tabs-ul');

            //addTabTitle('Tác nghiệp', this.config.documentTabContentId, false, null, tabIcons.home, { egovtab: 'document' });

            if (this.config.isSaveOpenTab) {
                if (!userId) {
                    throw 'Nếu bạn sử dụng chức năng lưu lại các tab đang mở, bạn phải gán giá trị cho tham số userId';
                }
                try {
                    recentTabsCookie = JSON.parse($.cookie(recentTabsCookieName));
                    recentTabs = recentTabsCookie[userId] ? recentTabsCookie[userId] : [];
                } catch (err) {
                    recentTabsCookie = {};
                    recentTabs = [];
                }
                $.each(recentTabs, function (index, item) {
                    var $newTab = addTabTitle(item.name, preFexTabContentEdit + item.id, true, null, item.icon);
                    addTabCloseEvent(false, $newTab);
                });
            }
            //this.activeTab(0);
            $ulmain.appendTo($mainTab);
        },

        /// <summary> Thêm một tab</summary>
        /// <param name="id">Id doctype nếu là tạo mới, Id của doccopy nếu là chi tiết</param>
        /// <param name="title">Tiêu đề của tab</param>
        /// <param name="docCopyId">Id của doccopy liên quan</param>
        /// <param name="icon">icon hiển thiị</param>
        /// <param name="onloaded">Hàm callback sau khi load xong tab</param>
        add: function (id, title, docCopyId, icon, onloaded, onclosed) {
            var contentId;
            var url;
            var isCreate = isCreateDocument(id);
            if (isCreate) {
                contentId = preFixTabContentCreate + tabIdentity;
                if (docCopyId) {
                    url = urlCreateDocument + '?id=' + id + '&documentCopyRelationId=' + docCopyId;
                } else {
                    url = urlCreateDocument + '/' + id;
                }
                tabIdentity++;
            } else {
                contentId = preFexTabContentEdit + id;
                var $a = $ulmain.find("li a[href=#" + preFexTabContentEdit + id + "]");
                if ($a.length > 0) {
                    this.activeTab($a.parent());
                    return;
                }
                url = urlEditDocument + id;
            }
            var $newTab = addTabTitle(title, contentId, true, null, icon);
            if (onclosed && typeof onclosed === 'function') {
                $newTab.data('onclosed', onclosed);
            }
            var $newIframe = addTabContent(url, contentId, onloaded);
            addTabCloseEvent(isCreate, $newTab, $newIframe);
            if (this.config.isSaveOpenTab && !isCreate) {
                var exist = _.find(recentTabs, function (item) {
                    return item.id == id;
                });
                if (!exist) {
                    recentTabs.push({ id: id, name: title, icon: icon });
                    recentTabsCookie[userId] = recentTabs;
                    $.cookie(recentTabsCookieName, JSON.stringify(recentTabsCookie), { secure: true });
                }
            }
        },

        /// <summary> Mở tab document</summary>
        /// <param name="id">Id doctype nếu là tạo mới, Id của doccopy nếu là chi tiết</param>
        /// <param name="title">Tiêu đề của tab</param>
        addDocument: function (id, title) {
            this.add(id, title, null, egov.cshtml.home.tabIcons.document, function () {
                egov.cshtml.home.listdocuments.updateIsViewed(id);
            });
        },

        /// <summary> Mở tab report</summary>
        addReport: function () {
            var $a = $ulmain.find("li a[href=#" + this.config.reportTabContentId + "]");
            if ($a.length > 0) {
                this.activeTab($a.parent());
                return;
            }
            var $newTab = addTabTitle('Báo cáo thống kê', this.config.reportTabContentId, true, null, tabIcons.report, { egovtab: 'report' });
            var $newIframe = addTabContent(urlReport, this.config.reportTabContentId);
            addTabCloseEvent(false, $newTab);
        },

        /// <summary> Mở tab ProfileConfig </summary>
        addTabSetting: function () {
            var $a = $ulmain.find("li a[href=#" + this.config.settingTabContentId + "]");
            if ($a.length > 0) {
                this.activeTab($a.parent());
                return;
            }
            var $newTab = addTabTitle('Thiết lập', this.config.settingTabContentId, true, null, tabIcons.option, { egovtab: 'setting' });
            var $newIframe = addTabContent(urlSetting, this.config.settingTabContentId);
            addTabCloseEvent(false, $newTab);
        },

        /// <summary> Mở tab ProfileConfig </summary>
        addTabProfileConfig: function () {
            var $a = $ulmain.find("li a[href=#" + this.config.profileConfigTabContentId + "]");
            if ($a.length > 0) {
                this.activeTab($a.parent());
                return;
            }
            var $newTab = addTabTitle('Thiết lập thông tin', this.config.profileConfigTabContentId, true, null, tabIcons.option, { egovtab: 'profileConfig' });
            var $newIframe = addTabContent(urlProfileConfig, this.config.profileConfigTabContentId);
            addTabCloseEvent(false, $newTab);
        },

        /// <summary> Mở tab changepaw </summary>
        addTabChangePassWord: function () {
            var $a = $ulmain.find("li a[href=#" + this.config.changePasswordTabContentId + "]");
            if ($a.length > 0) {
                this.activeTab($a.parent());
                return;
            }
            var $newTab = addTabTitle('Đổi mật khẩu', this.config.changePasswordTabContentId, true, null, tabIcons.option, { egovtab: 'changePassword' });
            var $newIframe = addTabContent(urlChangePassword, this.config.changePasswordTabContentId);
            addTabCloseEvent(false, $newTab);
        },
        /// <summary> Thêm tab search</summary>
        /// <param name="html">Nội dung tab</param>
        addTabSearch: function (html) {
            var $a = $ulmain.find("li a[href=#" + this.config.searchTabContentId + "]");
            if ($a.length > 0) {
                this.activeTab($a.parent());
                $('#' + this.config.searchTabContentId).remove();
                addTabContentHtml(this.config.searchTabContentId, html);
                return;
            }
            var $tabSearch = addTabTitle('Tìm kiếm', this.config.searchTabContentId, true, 2, tabIcons.search, { egovtab: 'search' });
            addTabContentHtml(this.config.searchTabContentId, html);
            addTabCloseEvent(false, $tabSearch);
        },
        /// <summary> Thêm tab print</summary>
        addPrint: function () {
            var $a = $ulmain.find("li a[href=#" + this.config.printTabContentId + "]");
            if ($a.length > 0) {
                this.activeTab($a.parent());
                return;
            }
            var $newTab = addTabTitle('Trang in', this.config.printTabContentId, true, null, tabIcons.print, { egovtab: 'print' });
            var $newIframe = addTabContent(urlPrint, this.config.printTabContentId);
            addTabCloseEvent(false, $newTab);
        },
        /// <summary> Lấy ra tab đang active</summary>
        getActiveTab: function () {
            return $ulmain.children("li.tabs-active");
        },
        /// <summary> Active một tab</summary>
        /// <param name="tab">Tab (có thể là object jquery, có thể là index, có thể là tên tab đặc biệt)</param>
        /// <remarks>Trường hợp là active theo index: nếu index = 0 thì active tabs root,  </remarks>
        activeTab: function (tab) {
            if (tab != null) {
                var $tab;
                if (tab instanceof jQuery) {
                    if (tab.length > 0) {
                        if (tab[0].tagName === 'LI' && tab.parent()[0].tagName === 'UL') {
                            if (!tab.hasClass('tabs-active')) {
                                tab.trigger('click');
                            }
                        }
                    }
                } else if (typeof tab === 'number') {
                    if (tab === 0) {
                        this.activeRootTab();
                        return;
                    }
                    $tab = $ulmain.children('li').eq(tab - 1);
                    if ($tab.length > 0) {
                        if (!$tab.hasClass('tabs-active')) {
                            $tab.trigger('click');
                        }
                    }
                } else if (typeof tab === 'string') {
                    $tab = $ulmain.children('li[egovtab=' + tab + ']');
                    if ($tab.length > 0) {
                        if (!$tab.hasClass('tabs-active')) {
                            $tab.trigger('click');
                        }
                    }
                }

            }
        },

        reloadActiveTab: function (tab) {
            if (tab != null) {
                var id = $(tab).find('a').attr('href');
                if ($(id).length === 0) {
                    return;
                }
                window.document.getElementById("iframe_" + id.replace("#", '')).contentWindow.location.reload();
            }
        },

        activeRootTab: function () {
            if ($('#documentProcessing').is(':visible')) {
                return;
            }
            var tabActive = this.getActiveTab();
            if (tabActive != undefined) {
                tabActive.removeClass('tabs-active');
                $($(tabActive.children("a[href]")[0]).attr('href')).hide();
            }
            $('#documentProcessing').show();
        },

        /// <summary> Đóng một tab</summary>
        /// <param name="tab">Tab (có thể là object jquery, có thể là index, có thể là tên tab đặc biệt)</param>
        close: function (tab) {
            if (tab != null) {
                var $tab;
                if (tab instanceof jQuery) {
                    if (tab.length > 0) {
                        if (tab[0].tagName === 'LI' && tab.parent()[0].tagName === 'UL') {
                            closeTab(tab);
                        }
                    }
                } else if (typeof tab === 'number') {
                    $tab = $ulmain.children('li').eq(tab);
                    if ($tab.length > 0) {
                        closeTab($tab);
                    }
                } else if (typeof tab === 'string') {
                    $tab = $ulmain.children('li[egovtab=' + tab + ']');
                    if ($tab.length > 0) {
                        closeTab($tab);
                    }
                }
            }
        },

        /// <summary> Đóng tab đang active</summary>
        closeActiveTab: function () {
            var $li = $ulmain.children('li.tabs-active');
            closeTab($li);
        }
    };

    egov.cshtml.home.tree = {
        document: {
            /// <summary> Khởi tạo cây văn bản</summary>
            /// <param name="processfunctions">Danh sách các cây văn bản</param>
            init: function (processfunctions) {
                var objDefineTree = {
                    plugins: ["themes", "html_data", "ui", "types", "crrm"],
                    ui: { select_multiple_modifier: false },
                    "types": {
                        "valid_children": ["root"],
                        "types": {
                            "root": {
                                "hover_node": false,
                                "select_node": function () { return false; }
                            }
                        }
                    }
                };

                $treeDocument = $('<ul></ul>');
                $treeDocument.appendTo($(egov.cshtml.home.idPanelTreeDocument));
                $('#treeTemplate').tmpl(processfunctions).appendTo($treeDocument);

                $(egov.cshtml.home.idPanelTreeDocument).jstree(objDefineTree)
                .bind('loaded.jstree', function (e, data) {
                    var $firstFunction = $(egov.cshtml.home.idPanelTreeDocument + ' ul:first ul:first li:first a:first');
                    if ($firstFunction.length > 0) {
                        $firstFunction.addClass('jstree-clicked');
                        $(egov.cshtml.home.idPanelTreeDocument + ' ul:first').children('li:first').attr('rel', 'root');
                        data.inst.get_container().find('li').each(function () {
                            if (data.inst.get_path($(this)).length <= 1) {
                                data.inst.open_node($(this));
                                return false;
                            }
                        });
                        $firstFunction.trigger('click');
                    }
                })
                .bind('open_node.jstree', function (event, data) {
                    if (data.inst._get_children(data.rslt.obj).length == 0 && data.rslt.obj.attr("id") > 0) {
                        data.rslt.obj.children('a:first').addClass('jstree-loading');
                        var id = parseInt(data.rslt.obj.attr("id"));
                        $.get('/Home/GetFunctionByParentId',
                            { id: id },
                            function (result) {
                                if (result) {
                                    var functions = _.sortBy(JSON.parse(result), function (item) { return item.order; });
                                    var orderFunctions = [];
                                    var order = 0;
                                    for (var i = 0; i < functions.length; i++) {
                                        var fuctionsParam = _.filter(functions, function (item) {
                                            return item.order == functions[i].order;
                                        });
                                        fuctionsParam = _.sortBy(fuctionsParam, function (item) { return egov.utilities.string.stripVietnameseChars(item.name); });
                                        for (var j = 0; j < fuctionsParam.length; j++) {
                                            var clone = _.clone(fuctionsParam[j]);
                                            clone.order = order;
                                            orderFunctions.push(clone);
                                            order++;
                                            i = order - 1;
                                        }
                                    }
                                    $('<ul></ul>').appendTo(data.rslt.obj);
                                    $('#treeItemTemplate').tmpl(orderFunctions).appendTo(data.rslt.obj.children('ul:first'));
                                    data.rslt.obj.children('ul:first').children('li:last-child').addClass('jstree-last');
                                    data.rslt.obj.children('ul:first').parents('li').each(function (index, item) {
                                        updateTotalUnread(item);
                                    });
                                    $('#SystemTree').getNiceScroll().resize();
                                }
                            }
                        )
                        .complete(function () {
                            data.rslt.obj.children('a:first').removeClass('jstree-loading');
                        })
                        .fail(function () {
                            eGovMessage.notification('@Localizer("Common.Error")', eGovMessage.messageTypes.error);
                        });
                    }
                })
                .bind('select_node.jstree', function (e, data) {
                    $(egov.cshtml.home.idPanelTreeStorePrivate).jstree("deselect_node");
                    var selected = $(data.rslt.obj[0]);
                    var functionId = selected.attr('id');
                    $('.grid-content').scrollTop(0);

                    //var pagingUrl = url.replace('/Home/GetDocument/', '/Home/GetDocumentPaging/');
                    //if (documentViews[functionId]) {
                    //    documentViews[functionId].rebind();
                    //    documentViews[functionId].loadNewerDocuments();
                    //}
                    //else {
                    //    documentViews[functionId] = new DocumentsView(functionId, url, pagingUrl, egov.cshtml.home.pagingByScroll, egov.cshtml.home.showQuickView);
                    //}

                    //   egov.cshtml.home.currentDocuments = cacheDocuments[functionId].documentsObject;
                    if (cacheDocuments[functionId]) {
                        reloadListDocuments(functionId, cacheDocuments[functionId].columnSetting, cacheDocuments[functionId].documentsObject);
                        if (cacheDocuments[functionId].reload == true) {
                            getDocuments(functionId, selected.children('a').attr('href'), null, true);
                        } else {
                            filterByDocType(functionId);
                            bindPaging(functionId);
                            isListStoreDocument = false;
                        }
                    } else {
                        getDocuments(functionId, selected.children('a').attr('href'), null, true);
                    }
                });
            }
        },
        storeprivate: {
            init: function () {
                var objDefineTree = {
                    json_data: {
                        data: [
                            {
                                data: 'Hồ sơ công việc',
                                state: 'closed',
                                attr: { rel: 'root', "class": 'storefolder' }
                            }
                        ]
                    },
                    plugins: ["themes", "json_data", "ui", "types", "crrm"],
                    ui: { select_multiple_modifier: false },
                    "types": {
                        "valid_children": ["root"],
                        "types": {
                            "root": {
                                "hover_node": false,
                                "select_node": function () { return false; }
                            }
                        }
                    }
                };

                $treeStorePrivate = $('<ul></ul>');
                $treeStorePrivate.appendTo($(egov.cshtml.home.idPanelTreeStorePrivate));

                $(egov.cshtml.home.idPanelTreeStorePrivate).jstree(objDefineTree)
                .bind('loaded.jstree', function () {
                    $(egov.cshtml.home.idPanelTreeStorePrivate).find("a").attr("rel", "root");
                })
                .bind('open_node.jstree', function (event, data) {
                    if (data.inst._get_children(data.rslt.obj).length == 0) {
                        data.rslt.obj.children('a:first').addClass('jstree-loading');
                        $.get('/StorePrivate/Gets',
                            {},
                            function (result) {
                                if (result) {
                                    var stores = [
                                        { id: 'storePrivate', name: 'Hồ sơ cá nhân', className: 'storeitem', list: createObjectTree(result.storePrivate) },
                                        { id: 'storeShare', name: 'Hồ sơ chia sẻ', className: 'storeshareitem', list: createObjectTree(result.storeShare) }
                                    ];
                                    var $newUl = $('<ul></ul>');
                                    $newUl.appendTo(data.rslt.obj);
                                    $('#treeItemStorePrivateTemplate').tmpl(stores).appendTo(data.rslt.obj.children('ul:first'));
                                    $newUl.find('li:last-child').addClass('jstree-last');
                                    $newUl.find('#storePrivate li').addClass('storeitem');
                                    $newUl.find('#storeShare li').addClass('storeshareitem');
                                }
                            }
                        )
                        .complete(function () {
                            data.rslt.obj.children('a:first').removeClass('jstree-loading');
                            createContextMenuForStoreFolder();
                            createContextMenuForStorePrivate();
                            createContextMenuForStoreShare();
                        })
                        .fail(function () {
                            eGovMessage.notification('@Localizer("Common.Error")', eGovMessage.messageTypes.error);
                        });
                    }
                })
                .bind('select_node.jstree', function (e, data) {
                    var id = parseInt(data.rslt.obj.attr("data-storeprivateid"));
                    $.get('/StorePrivate/GetDocuments', { id: id }, function (result) {
                        var columnSettings = [{ "ColumnName": "STT", "DisplayName": "STT", "Width": "50", "EnableSort": false, "Order": -1 }, { "ColumnName": "Color", "DisplayName": "", "Width": "50", "EnableSort": false, "Order": 0 }, { "ColumnName": "Compendium", "DisplayName": "Trích yếu", "SortName": "", "Width": "300", "EnableSort": true, "Order": 2 }, { "ColumnName": "Email", "DisplayName": "Người soạn thảo", "SortName": null, "Width": 100, "EnableSort": false, "Order": 3 }, { "ColumnName": "DateCreated", "DisplayName": "Ngày tạo", "SortName": "", "Width": "150", "EnableSort": true, "Order": 4 }, { "ColumnName": "DateAppointed", "DisplayName": "Ngày hết hạn", "Width": "150", "EnableSort": true, "Order": 5 }, { "ColumnName": "LastComment", "DisplayName": "Ý kiến xử lý cuối", "SortName": null, "Width": null, "EnableSort": false, "Order": 6 }];
                        bindListDocuments(columnSettings, JSON.parse(result));
                        $(idTableDocuments).grid({
                            isUsingCustomScroll: true,
                            isResizeColumn: true,
                            isFixHeightContent: true,
                            isAddHoverRow: false,
                            isUseCookie: true,
                            isRenderPanelGrid: false
                        });
                        if (data.rslt.obj.hasClass('storeitem')) {
                            isListStoreDocument = true;
                        } else {
                            isListStoreDocument = false;
                        }
                        bindContextShortCut();
                        $('.grid-content').getNiceScroll().remove();
                        egov.cshtml.home.documentProcessingLayout.initContent('center');
                        $('.grid-content').niceScroll(niceScrollOption);
                    });

                    $(egov.cshtml.home.idPanelTreeDocument).jstree("deselect_node");
                });
            },
            openDialogSave: function (data, saveCallback, cancelCallback) {
                var stores = [
                    { id: 'storePrivateSave', name: 'Hồ sơ cá nhân', className: 'storeitem', list: createObjectTree(data.storePrivate) },
                    { id: 'storeShareSave', name: 'Hồ sơ chia sẻ', className: 'storeshareitem', list: createObjectTree(data.storeShare) }
                ];
                var selectedId;
                var $tree = $('<div id="treeStorePrivateSave"></div>');
                var $newUl = $('<ul></ul>');
                $newUl.appendTo($tree);
                $('#treeSaveStorePrivateTemplate').tmpl(stores).appendTo($newUl);
                $newUl.find('li:last-child').addClass('jstree-last');
                var objDefineTree = {
                    "core": { "initially_open": ["storePrivateSave", "storeShareSave"] },
                    plugins: ["themes", "html_data", "ui", "types", "crrm"],
                    ui: { select_multiple_modifier: false },
                    "types": {
                        "valid_children": ["root"],
                        "types": {
                            "root": {
                                "hover_node": false,
                                "select_node": function () { return false; }
                            }
                        }
                    }
                };
                $tree.appendTo($('body'));
                $tree.jstree(objDefineTree)
                    .bind('select_node.jstree', function (e, dataTree) {
                        selectedId = parseInt($(dataTree.rslt.obj[0]).attr('data-storeprivateid'));
                    });
                var settings = {}; // dialog setting
                settings.height = 350;
                settings.title = "Chọn hồ sơ để lưu văn bản";
                settings.buttons = [
                    {
                        text: "Tạo mới",
                        click: function () {
                            createStorePrivate(selectedId);
                        }
                    },
                    {
                        text: "Lưu",
                        click: function () {
                            if (!selectedId) {
                                eGovMessage.show('Bạn phải chọn hồ sơ để lưu');
                            } else {
                                if (saveCallback && typeof saveCallback === 'function') {
                                    saveCallback(selectedId);
                                }
                            }
                        }
                    },
                    {
                        text: "Không lưu",
                        click: function () {
                            if (cancelCallback && typeof cancelCallback === 'function') {
                                cancelCallback();
                            }
                        }
                    }
                ];
                var dialog = new dialogAdapter();
                dialog.close();
                dialog.openexist($tree, settings);
            }
        }
    };

    /// <summary> Gửi nhận</summary>
    /// <param name="showLoading">Hiển thị loading</param>
    egov.cshtml.home.reloadData = function (showLoading) {
        var functionId = getSelectedNode().attr('id');
        var $selected = $('#' + functionId);
        var allIds = [];
        $treeDocument.find('li').each(function (idx, item) {
            var dataid = parseInt($(item).attr('data-functionid'));
            if (dataid != 0) {
                if (!isNaN(dataid)) {
                    allIds.push(dataid);
                }
            }
        });
        allIds = egov.utilities.array.distinct(allIds);

        $.each(cacheDocuments, function (key, value) {
            if (key !== functionId) {
                value.reload = true;
            }
        });
        getDocuments(functionId, $selected.children('a').attr('href'), allIds, showLoading);
    };

    egov.cshtml.home.listdocuments = {};

    /// <summary>
    /// <para>Tìm kiếm nhanh trên danh sách văn bản</para>
    /// <para>TrungVH@bkav.com - 1/5/2013</para>
    /// </summary>
    egov.cshtml.home.listdocuments.quickSearch = function (target) {
        clearTimeout(this.searching);
        this.searching = setTimeout(function () {
            var $selected = getSelectedNode();
            var id = $selected.attr('id');
            var quicksearch = $(target).val();
            if (quicksearch === '') {
                reloadListDocuments(id, cacheDocuments[id].columnSetting, cacheDocuments[id].documentsObject);
                bindPaging(id);
            } else {
                var url = $selected.children('a').attr('href').replace('/Home/GetDocument/', '/Home/GetDocumentPaging/');
                var token = $("input[name='__RequestVerificationToken']", "#HomeGetDocumentPaging").val();
                $.post(url, {
                    quickSearch: $(target).val(),
                    __RequestVerificationToken: token
                },
                    function (data) {
                        var documents;
                        try {
                            documents = JSON.parse(data.documents);
                        } catch (ex) {
                            documents = [];
                        }
                        currentQuickSearchResult = documents;
                        reloadListDocuments(id, cacheDocuments[id].columnSetting, documents);
                        bindPaging(id);
                    });
            }
        }, 300);
        //var value = egov.utilities.string.stripVietnameseChars($(target).val().toLowerCase());
        //var id = getSelectedNode().attr('id');
        //if (id) {
        //    if (cacheDocuments[id]) {
        //        var filter = value === ''
        //            ? cacheDocuments[id].documentsObject
        //            : _.filter(cacheDocuments[id].documentsObject, function (item) {
        //                return egov.utilities.string.stripVietnameseChars(item.Compendium.toLowerCase()).search(value) >= 0;
        //            });
        //        $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[id].columnSetting));
        //        $(idTableDocuments + " tr:odd").addClass("trodd");
        //        bindContextShortCut();
        //    }
        //}
    };

    /// <summary> Sắp xếp danh sách văn bản hồ sơ</summary>
    /// <param name="columnName">Tên column</param>
    egov.cshtml.home.listdocuments.sort = function (columnName) {
        var $target = $(".grid-header table th a[id=" + columnName + "]");
        var $selected = getSelectedNode();
        var $docImportant = $('#DocumentImportantSelect .selected');
        var $docView = $('#DocumentViewSelect .selected');
        var functionId = $selected.attr("id");
        //console.log(cacheDocuments[functionId].documentsObject);
        if (cacheDocuments[functionId]) {
            var doclength = cacheDocuments[functionId].documentsObject.length;
            var filter = cacheDocuments[functionId].documentsObject;

            filter = $docImportant.hasClass('important')
                       ? _.filter(filter, function (item) {
                           return item.IsDocumentImportant == 1;
                       })
                       : (!$docImportant.hasClass('important')
                       ? _.filter(filter, function (item) { return item.IsDocumentImportant == 0; })
                       : filter);

            filter = $docView.hasClass('docIsView')
                        ? _.filter(filter, function (item) { return item.IsViewed == 1; })
                        : ($docView.hasClass('docNotView') ? _.filter(filter, function (item) { return item.IsViewed == 0; }) : filter);

            if (doclength > 0) {
                var sortDocuments = _.sortBy(filter, function (item) {
                    if (regexUtcDate.test(item[columnName])) {
                        var date = Date.parse(item[columnName]);
                        if (isNaN(date)) {
                            if (typeof item[columnName] == 'string') {
                                return egov.utilities.string.stripVietnameseChars(item[columnName]);
                            }
                            return item[columnName];
                        }
                        return date;
                    } else {
                        if (typeof item[columnName] == 'string') {
                            return egov.utilities.string.stripVietnameseChars(item[columnName]);
                        }
                        return item[columnName];
                    }
                });
                if ($(egov.cshtml.home.idPanelListDocument + " .grid-header table th a.sort").attr('id') != columnName) {
                    $(egov.cshtml.home.idPanelListDocument + " .grid-header table th a.sort").removeClass('sort').removeClass('asc').removeClass('desc');
                }
                if ($target.hasClass('sort')) {
                    if ($target.hasClass('asc')) {
                        $(idTableDocuments + " tbody").html(bindDocumentRows(sortDocuments.reverse(), cacheDocuments[functionId].columnSetting));
                        $target.removeClass('asc').addClass('desc');
                    } else if ($target.hasClass('desc')) {
                        $(idTableDocuments + " tbody").html(bindDocumentRows(sortDocuments, cacheDocuments[functionId].columnSetting));
                        $target.removeClass('desc').addClass('asc');
                    }
                } else {
                    $(idTableDocuments + " tbody").html(bindDocumentRows(sortDocuments, cacheDocuments[functionId].columnSetting));
                    $target.addClass('sort asc');
                }
                $(idTableDocuments + " tr:odd").addClass("trodd");
                bindContextShortCut();
            }
        }
    };

    /// <summary> Sắp xếp danh sách văn bản hồ sơ theo loại và theo trang thái xem</summary>
    egov.cshtml.home.listdocuments.sortByTypeAndIsView = function () {
        var $selected = getSelectedNode();
        var $docImportant = $('#DocumentImportantSelect1 .document-important.selected').length > 0;
        var $docView = $('#DocumentImportantSelect1 .document-unread.selected').length > 0;
        var functionId = $selected.attr("id");
        if (cacheDocuments[functionId]) {
            var doclength = cacheDocuments[functionId].documentsObject.length;
            var filter = cacheDocuments[functionId].documentsObject;

            //filter = $docImportant.hasClass('document-important')
            //           ? _.filter(filter, function (item) { return item.IsDocumentImportant == 1; })
            //           : ($docImportant.hasClass('document-notimportant')
            //           ? _.filter(filter, function (item) { return item.IsDocumentImportant == 0; })
            //           : filter);

            //filter = $docView.hasClass('docIsView')
            //            ? _.filter(filter, function (item) { return item.IsViewed == 1; })
            //            : ($docView.hasClass('docNotView')
            //            ? _.filter(filter, function (item) { return item.IsViewed == 0; })
            //            : filter);

            filter = _.filter(cacheDocuments[functionId].documentsObject, function (item) {
                if ($docImportant) {
                    return item.IsDocumentImportant == 1;
                }
                if ($docView) {
                    return item.IsViewed == 0;
                }
                return true;
            });

            if (doclength > 0) {
                var tableDocument = $(egov.cshtml.home.idPanelListDocument + " .grid-header table th a");
                if (tableDocument.hasClass('sort')) {
                    var columnName = tableDocument.attr('id');

                    filter = _.sortBy(filter, function (item) {
                        if (regexUtcDate.test(item[columnName])) {
                            var date = Date.parse(item[columnName]);
                            if (isNaN(date)) {
                                if (typeof item[columnName] == 'string') {
                                    return egov.utilities.string.stripVietnameseChars(item[columnName]);
                                }
                                return item[columnName];
                            }
                            return date;
                        } else {
                            if (typeof item[columnName] == 'string') {
                                return egov.utilities.string.stripVietnameseChars(item[columnName]);
                            }
                            return item[columnName];
                        }
                    });

                    if (tableDocument.hasClass('asc')) {
                        $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[functionId].columnSetting));
                    } else if (tableDocument.hasClass('desc')) {
                        $(idTableDocuments + " tbody").html(bindDocumentRows(filter.reverse(), cacheDocuments[functionId].columnSetting));
                    }
                }
                else { $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[functionId].columnSetting)); }

                $(idTableDocuments + " tr:odd").addClass("trodd");
                bindContextShortCut();
            }
        }
    };

    ///<summary>thiết lập văn bản quan trọng hay không</summary>
    ///<param name="item">phần tử chứa</param>
    ///<param name="documentCopyId">id của văn bản copy</param>
    egov.cshtml.home.listdocuments.setDoccumentImportant = function documentImportant(item, documentCopyId) {
        var $selected = getSelectedNode();
        var functionId = $selected.attr("id");
        var $item = $(item);
        var important = $item.hasClass('important') ? true : false;
        if (important) {
            $item.removeClass('important');
            $item.attr('title', 'Gắn quan trọng cho văn bản này');
        }
        else {
            $item.addClass('important');
            $item.attr('title', 'Bỏ gắn quan trọng văn bản này');
        }
        $.ajax({
            dataType: 'json',
            data: {
                documentCopyId: documentCopyId,
                isImportant: !important
            },
            url: "/Home/SetDocumentImportant",
            success: function (result) {
                if (result.success) {
                    if (cacheDocuments[functionId]) {
                        var doclength = cacheDocuments[functionId].documentsObject.length;
                        for (var i = 0; i < doclength; i++) {
                            if (cacheDocuments[functionId].documentsObject[i].DocumentCopyId == documentCopyId) {
                                if (important) {
                                    cacheDocuments[functionId].documentsObject[i].IsDocumentImportant = 0;
                                } else cacheDocuments[functionId].documentsObject[i].IsDocumentImportant = 1;
                            }

                        }
                    }
                }
                else {
                    $item.addClass('important');
                    $item.attr('title', 'Bỏ gắn quan trọng văn bản này');
                    eGovMessage.notification(result.message, eGovMessage.messageTypes.error);
                }
            },
            error: function () {
                $item.addClass('important');
                $item.attr('title', 'Bỏ gắn quan trọng văn bản này');
                eGovMessage.notification(""@Localizer("Common.Error")" khi gắn quan trọn cho văn bản!", eGovMessage.messageTypes.error);
            }
        });
    }

    egov.cshtml.home.listdocuments.nextpage = function () {
        if (!$('#nextPage').hasClass('disabled')) {
            var totalPages = Math.ceil(totalRecordsListDocuments / currentPageSizeListDocuments);
            if (currentPageListDocuments < totalPages) {
                paging(currentPageListDocuments + 1, currentPageSizeListDocuments);
            }
        }
    };

    egov.cshtml.home.listdocuments.lastpage = function () {
        if (!$('#lastPage').hasClass('disabled')) {
            var totalPages = Math.ceil(totalRecordsListDocuments / currentPageSizeListDocuments);
            if (currentPageListDocuments < totalPages) {
                paging(totalPages, currentPageSizeListDocuments);
            }
        }
    };

    egov.cshtml.home.listdocuments.prevpage = function () {
        if (!$('#prevPage').hasClass('disabled')) {
            if (currentPageListDocuments > 1) {
                paging(currentPageListDocuments - 1, currentPageSizeListDocuments);
            }
        }
    };

    egov.cshtml.home.listdocuments.firstpage = function () {
        if (!$('#firstPage').hasClass('disabled')) {
            if (currentPageListDocuments > 1) {
                paging(1, currentPageSizeListDocuments);
            }
        }
    };

    egov.cshtml.home.listdocuments.changePageSize = function (pagesize) {
        paging(1, pagesize);
    };

    egov.cshtml.home.listdocuments.pageSize = function (pagesize) {
        if (pagesize && typeof pagesize === 'number') {
            $.cookie('PageSizeHome_' + egov.cshtml.home.userid, pagesize, { secure: true });
            return pagesize;
        }
        return $.cookie('PageSizeHome_' + egov.cshtml.home.userid) === null ? null : parseInt($.cookie('PageSizeHome_' + egov.cshtml.home.userid));
    };

    /// <summary> Cập nhật trạng thái đã đọc</summary>
    /// <param name="id">Id doccopy</param>
    egov.cshtml.home.listdocuments.updateIsViewed = function (id) {
        $("#documentCopy_" + id).removeClass('documentUnread');
        $.each(cacheDocuments, function (key, value) {
            var exist = _.find(value.documentsObject, function (item) {
                return item.DocumentCopyId == id;
            });
            if (exist) {
                exist.IsViewed = 1;
            }
        });
    };

    egov.cshtml.home.plugin = {};

    egov.cshtml.home.plugin.getPlugin = function (set) {
        if (set) {
            plugin = set;
        }
        return plugin;
    };

    egov.cshtml.home.plugin.showDialogDownloadPlugin = function (callback) {
        var options = {
            modal: true,
            minHeight: 30,
            width: 'auto',
            resizable: false,
            open: function () {
                var self = this;
                $(".ui-widget-overlay").css("background", "transparent none");
                $(".ui-widget-overlay").click(function () {
                    $(self).dialog("close");
                });
            },
            title: "Thông báo",
            close: function () {
                $(this).remove();
            }
        };

        var $div = $('<div><p style="font-size:16px;font-weight:bold;">Bạn chưa cài đặt plugin</p><p>Bạn cần tải về và cài đặt plugin này để sử dụng chức năng mở tệp đính kèm và quét ảnh</p><p style="color:red">Nếu bạn vẫn thấy thông báo này sau khi cài đặt plugin, hãy khởi động lại trình duyệt</p><center><div style="text-align:center;width:180px"><input type="button" value="Tải về và cài đặt" /><div id="imgDownloadingPlugin" style="float:left;display:none"><img src="/Content/Images/ajax-loader.gif" width="24px" height="24px" /></div><div id="msgDowloadingPlugin" style="float:left;padding-top:5px;display:none">&nbsp;Đang chờ cài đặt plugin...</div></div></center></div>');
        $div.find('input').click(function () {
            var self = this;
            document.location = "/Download/EOfficePlusPlugin";
            $(self).hide();
            $(self).siblings('div').show();
            $div.find('#imgDownloadingPlugin').show();
            $div.find('#msgDowloadingPlugin').show();
            FireBreath.waitForInstall(pluginName, function () {
                $(self).parents('div.ui-dialog-content').dialog('close');
                if (callback && typeof (callback) === 'function') {
                    callback();
                }
            });
        });
        $div.dialog(options);
    };

    egov.cshtml.home.plugin.openAttachment = function (id) {
        var self = this;
        if (!plugin && $("#plugin").length === 0) {
            if (FireBreath.isPluginInstalled(pluginName)) {
                $("body").append(FireBreath.injectPlugin(pluginName, "plugin", function () {
                    plugin = document.getElementById('plugin');
                    self.openAttachment(id);
                }));
            } else {
                self.showDialogDownloadPlugin(function () { self.openAttachment(id); });
            }
        } else {
            $("#center").blockpanel({ text: "Đang tải...", borderWidth: 1 });
            $.get("/StorePrivate/DownloadAttachmentBase64/" + id, {}, function (data) {
                var result = JSON.parse(data);
                if (result) {
                    if (result.error) {
                        eGovMessage.show(result.error);
                    } else {
                        var filesize = plugin.writeFileBase64(result.fileName, result.content, false);
                        plugin.openFile(result.fileName, false);
                    }
                }
            })
            .fail(function () {
                eGovMessage.show('"@Localizer("Common.Error")" khi tải tài liệu!');
            })
            .complete(function () {
                $("#center").unblockpanel();
            });
        }
    };

    egov.cshtml.home.scan = { isLoadedListScaner: false };

    egov.cshtml.home.scan.showDialogScan = function (frameName, imageName, acceptFileTypes, fullname, username) {
        var self = this;
        if (!plugin && $("#pluginScan").length === 0) {
            if (FireBreath.isPluginInstalled(pluginName)) {
                $("body").append(FireBreath.injectPlugin(pluginName, "pluginScan", function () {
                    plugin = document.getElementById('pluginScan');
                    self.reloadListScanner(true);
                    self.isLoadedListScaner = true;
                    self.showDialogScan(frameName, imageName, acceptFileTypes, fullname, username);
                }));
            } else {
                egov.cshtml.home.plugin.showDialogDownloadPlugin(function () { self.showDialogScan(frameName, imageName, acceptFileTypes, fullname, username); });
            }
        } else {
            if (self.isLoadedListScaner) {
                self.reloadListScanner(false);
            } else {
                self.reloadListScanner(true);
                self.isLoadedListScaner = true;
            }
            currentUrl = "";
            currentExt = "";
            currentWidth = 0;
            currentHeight = 0;
            currentPage = 0;
            allImages = [];
            if (!imageName || imageName == '') {
                $("#filename").val('image');
            } else {
                $("#filename").val(imageName);
            }
            $("#pagePosition").text('Trang: 0/0');
            $("#addToContent").prop('checked', false);
            $("#preImage, #nextImage, #addToContent, #removeImageScan, #removeAllImageScan").attr("disabled", "disabled");

            var setting = {};
            setting.title = "eGovCloud - Quét tài liệu";
            setting.width = 595;
            setting.height = 570;
            setting.modal = true;
            setting.draggable = true;
            setting.resizable = true;
            setting.open = function () {
                $(".ui-widget-overlay").css("background", "transparent none");
                $(".ui-widget-overlay").click(function () {
                    $("#scanPanel").dialog("close");
                });
            };
            setting.close = function () {
                $.each(allImages, function (index, item) {
                    plugin.cancelTransferImage(item.url);
                });
            };
            setting.buttons = [
                {
                    text: "Chuyển ảnh",
                    click: function () {
                        var dialog = this;
                        var frame = document.getElementById(frameName).contentWindow;
                        if (allImages && allImages.length > 0) {
                            var images = _.filter(allImages, function (item) {
                                return item.addToContent;
                            });
                            if (images.length > 0) {
                                if ($('#' + frameName).contents().find('#mainForm').length > 0 && frame.CKEDITOR) {

                                    if (!frame.CKEDITOR.instances.editor1) {
                                        frame.egov.cshtml.document.enableEditor($('#' + frameName).contents().find('#mainForm .content')[0], function () {
                                            insertImageToContent(frame, images);
                                            uploadImageScan(frame, dialog, acceptFileTypes, fullname, username);
                                        });
                                    } else {
                                        insertImageToContent(frame, images);
                                        uploadImageScan(frame, dialog, acceptFileTypes, fullname, username);
                                    }
                                }
                            } else {
                                uploadImageScan(frame, dialog, acceptFileTypes, fullname, username);
                            }
                        } else {
                            $(dialog).dialog("close");
                        }
                    }
                },
                {
                    text: "Đóng",
                    click: function () {
                        $(this).dialog("close");
                    }
                }];
            if ($("#scanPanel").length > 0) {
                scanDialogTemplate = _.clone($("#scanPanel"));
            } else {
                $('body').append(scanDialogTemplate);
            }
            if (jcropApi) {
                jcropApi.destroy();
                $("#imageScan").remove();
            }
            $("#scanPanel").dialog(setting);
        }
    };

    var resizeImageBase64ForInsertToContent = function (base64, type) {
        var tmpImg = new Image();
        tmpImg.src = "data:image/" + type + ";base64," + base64;
        var fitWidthResize = 768;
        var newWidth = tmpImg.width;
        var newHeight = tmpImg.height;
        if (tmpImg.width >= fitWidthResize) {
            newWidth = fitWidthResize;
            newHeight = (fitWidthResize / tmpImg.width) * tmpImg.height;
        }
        return '<img width="' + newWidth + 'px" height="' + newHeight + 'px" src="data:image/' + type + ';base64,' + base64 + '" />';
    };

    var insertImageToContent = function (frame, images, type) {
        var editor = frame.CKEDITOR.instances.editor1;
        var focusManager = new frame.CKEDITOR.focusManager(editor);
        focusManager.focus();
        if (editor.getData() === '') {
            $.each(images, function (index, item) {
                if (typeof item === 'object') {
                    if (item.addToContent) {
                        editor.insertHtml('<p></p>');
                        editor.insertHtml('<p>' + resizeImageBase64ForInsertToContent(item.data, (item.url.indexOf(".bmp", item.url.length - 4) !== -1 ? "bmp" : "jpg")) + '</p>');
                    }
                } else if (typeof item === 'string') {
                    var tmp = new Image();
                    tmp.src = "data:image/" + type + ";base64," + item;
                    editor.insertHtml('<p>' + resizeImageBase64ForInsertToContent(item, type) + '</p>');
                }
            });
        } else {
            var selection = editor.getSelection();
            var range = selection.getRanges()[0];
            var pCon = range.startContainer.getAscendant({ div: 1, b: 1, p: 1, span: 1, em: 1, u: 1 }, true);
            var newRange = new frame.CKEDITOR.dom.range(range.document);
            newRange.moveToPosition(pCon, frame.CKEDITOR.POSITION_BEFORE_START);
            newRange.select();
            $.each(images, function (index, item) {
                if (typeof item === 'object') {
                    if (item.addToContent) {
                        editor.insertHtml('<p></p>');
                        editor.insertHtml('<p>' + resizeImageBase64ForInsertToContent(item.data, (item.url.indexOf(".bmp", item.url.length - 4) !== -1 ? "bmp" : "jpg")) + '</p>');
                    }
                } else if (typeof item === 'string') {
                    var tmp = new Image();
                    tmp.src = "data:image/" + type + ";base64," + item;
                    editor.insertHtml('<p>' + resizeImageBase64ForInsertToContent(item, type) + '</p>');
                }
            });
        }
    };

    var uploadImageScan = function (frame, dialog, acceptFileTypes, fullname, username) {
        var allImagesTransfer = [];
        if ($("#imageformat").val() == 'PDF') {
            if (allImages.length > 0) {
                var filenamePdf = $("#filename").val() + mapImageFormat[$("#imageformat").val()];
                var base64Pdf = plugin.transferPdf(_.pluck(allImages, 'url'));
                if (base64Pdf !== '') {
                    allImagesTransfer.push({ name: filenamePdf, value: base64Pdf });
                }
            }
        } else if ($("#imageformat").val() == 'DOC') {
            if (allImages.length > 0) {
                if (plugin.isWordInstalled()) {
                    var filenameDoc = $("#filename").val() + mapImageFormat[$("#imageformat").val()];
                    var base64Doc = plugin.transferDoc(_.pluck(allImages, 'url'));
                    if (base64Doc !== '') {
                        allImagesTransfer.push({ name: filenameDoc, value: base64Doc });
                    }
                } else {
                    eGovMessage.show("Bạn cần cài đặt Microsoft Word để sử dụng chức năng này!");
                }
            }
        } else {
            $.each(allImages, function (index, item) {
                var filename = $("#filename").val() + '_' + index + mapImageFormat[$("#imageformat").val()];
                var base64 = plugin.transferImage(item.url, $("#imageformat").val());
                if (base64 !== '') {
                    allImagesTransfer.push({ name: filename, value: base64 });
                }
            });
        }
        if (allImagesTransfer.length > 0) {
            frame.egov.cshtml.document.uploadImageScan(allImagesTransfer, acceptFileTypes, fullname, username);
        }
        allImages = [];
        $(dialog).dialog("close");
    };

    egov.cshtml.home.scan.reloadListScanner = function (reload) {
        $("#listScanner").html('');
        $("#acquire").attr('disabled', 'disabled');
        var listScanner = JSON.parse(plugin.getAllScanner(reload));

        if (listScanner && listScanner.length > 0) {
            $("#acquire").removeAttr('disabled');
            $.each(listScanner, function (index, item) {
                $("#listScanner").append("<option value='" + index + "'>" + item + "</option>");
            });
        }
    };

    egov.cshtml.home.scan.acquire = function () {
        plugin.acquire($("#showui").prop("checked"), $("#listScanner").val(), $("#pixeltype").val(), $("#resolution").val(), $("#duplex").prop("checked"), egov.cshtml.home.scan.showImage);
    };

    egov.cshtml.home.scan.showImage = function (url, width, height) {
        currentUrl = url;
        currentWidth = width;
        currentHeight = height;
        var fitHeight = (fitWidth / width) * height;
        var base64 = plugin.readFileScanBase64(currentUrl, 0, -1);
        currentExt = currentUrl.indexOf(".bmp", currentUrl.length - 4) !== -1 ? "bmp" : "jpg";
        var $img = $('<img width="' + fitWidth + 'px" height="' + fitHeight + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
        $("#imagePreviewPanel").html($img);
        setJcrop();

        allImages.push({ url: url, data: base64, ext: currentExt, width: width, height: height, addToContent: false });
        currentPage = allImages.length - 1;
        $("#pagePosition").text('Trang: ' + (currentPage + 1) + '/' + allImages.length);
        if (allImages.length > 1) {
            $("#preImage").removeAttr("disabled");
            $("#nextImage").attr("disabled", "disabled");
        } else if (allImages.length === 1) {
            $("#addToContent, #removeImageScan, #removeAllImageScan").removeAttr("disabled");
        }
    };

    egov.cshtml.home.scan.setRotate = function (angle) {
        if (currentUrl !== '') {
            if (plugin.imageRotate(currentUrl, angle)) {
                jcropApi.destroy();
                var width = $("#imageScan").width();
                var height = $("#imageScan").height();
                var base64 = plugin.readFileScanBase64(currentUrl, 0, -1);
                var $img = $('<img width="' + height + 'px" height="' + width + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                $("#imageScan").remove();
                $("#imagePreviewPanel").html($img);
                setJcrop();
                var newCurrentWidth = currentHeight;
                currentHeight = currentWidth;
                currentWidth = newCurrentWidth;
                var image = allImages[currentPage];
                image.width = currentWidth;
                image.height = currentHeight;
                image.data = base64;
            }
        }
    };

    egov.cshtml.home.scan.setZoom = function (value) {
        if (currentUrl !== '') {
            var width = $("#imageScan").width();
            var height = $("#imageScan").height();
            var zoomInWidth = width + value;
            var zoomInHeight = zoomInWidth / (width / height);
            if (zoomInWidth > 0 && zoomInHeight > 0) {
                jcropApi.destroy();
                $("#imageScan").css({
                    'width': zoomInWidth + "px",
                    'height': zoomInHeight + "px"
                });
                $("#imageScan").attr("width", zoomInWidth + "px").attr("height", zoomInHeight + "px");
                setJcrop();
            }
        }
    };

    egov.cshtml.home.scan.setActualSize = function () {
        if (currentUrl !== '') {
            jcropApi.destroy();
            $("#imageScan").css({
                'width': currentWidth + "px",
                'height': currentHeight + "px"
            });
            $("#imageScan").attr("width", currentWidth + "px").attr("height", currentHeight + "px");
            setJcrop();
        }
    };

    egov.cshtml.home.scan.crop = function () {
        if (currentUrl !== '') {
            var width = $("#imageScan").width();
            var height = $("#imageScan").height();
            if (plugin.imageCrop(currentUrl, x, y, x2, y2, width, height)) {
                jcropApi.destroy();
                var base64 = plugin.readFileScanBase64(currentUrl, 0, -1);
                var $img = $('<img width="' + fitWidth + 'px" height="' + ((fitWidth / w) * h) + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                $("#imagePreviewPanel").html($img);
                setJcrop();
                currentWidth = (w / width) * currentWidth;
                currentHeight = (h / height) * currentHeight;
                var image = allImages[currentPage];
                image.width = currentWidth;
                image.height = currentHeight;
                image.data = base64;
            }
        }
    };

    egov.cshtml.home.scan.setBrightness = function (value) {
        if (currentUrl !== '') {
            if (plugin.imageAdjustBrightness(currentUrl, value)) {
                var width = $("#imageScan").width();
                var height = $("#imageScan").height();
                jcropApi.destroy();
                var base64 = plugin.readFileScanBase64(currentUrl, 0, -1);
                var $img = $('<img width="' + width + '" height="' + height + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                $("#imagePreviewPanel").html($img);
                setJcrop();
                allImages[currentPage].data = base64;
            }
        }
    };

    egov.cshtml.home.scan.setContrast = function (value) {
        if (currentUrl !== '') {
            if (plugin.imageAdjustContrast(currentUrl, value)) {
                var width = $("#imageScan").width();
                var height = $("#imageScan").height();
                jcropApi.destroy();
                var base64 = plugin.readFileScanBase64(currentUrl, 0, -1);
                var $img = $('<img width="' + width + '" height="' + height + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                $("#imagePreviewPanel").html($img);
                setJcrop();
                allImages[currentPage].data = base64;
            }
        }
    };

    egov.cshtml.home.scan.preImage = function () {
        if (currentPage >= 1) {
            currentPage--;
            showImageByCurrentPage();
            $("#nextImage").removeAttr("disabled");
            if (currentPage === 0) {
                $("#preImage").attr("disabled", "disabled");
            }
        }
    };

    egov.cshtml.home.scan.nextImage = function () {
        if (currentPage <= allImages.length - 2) {
            currentPage++;
            showImageByCurrentPage();
            $("#preImage").removeAttr("disabled");
            if (currentPage === allImages.length - 1) {
                $("#nextImage").attr("disabled", "disabled");
            }
        }
    };

    egov.cshtml.home.scan.removeImageScan = function () {
        if (allImages.length > 0) {
            var oldLength = allImages.length;
            plugin.cancelTransferImage(allImages[currentPage].url);
            allImages.splice(currentPage, 1);
            if (currentPage === oldLength - 1) {
                if (currentPage > 0) {
                    currentPage--;
                    showImageByCurrentPage();
                } else {
                    jcropApi.destroy();
                    $("#imageScan").remove();
                    $("#pagePosition").text('Trang: 0/0');
                }
            } else {
                showImageByCurrentPage();
            }
            if (currentPage === 0) {
                $("#preImage").attr("disabled", "disabled");
                if (allImages.length <= 1) {
                    $("#nextImage").attr("disabled", "disabled");
                }
            } else if (currentPage === allImages.length - 1) {
                $("#nextImage").attr("disabled", "disabled");
            }
            if (allImages.length === 0) {
                currentUrl = "";
                currentExt = "";
                currentWidth = 0;
                currentHeight = 0;
                currentPage = 0;
                $("#removeImageScan, #removeAllImageScan, #addToContent").attr("disabled", "disabled");
            }
        }
    };

    egov.cshtml.home.scan.removeAllImageScan = function () {
        if (allImages.length > 0) {
            jcropApi.destroy();
            $("#imageScan").remove();
            $("#pagePosition").text('Trang: 0/0');
            currentUrl = "";
            currentExt = "";
            currentWidth = 0;
            currentHeight = 0;
            currentPage = 0;
            $.each(allImages, function (index, item) {
                plugin.cancelTransferImage(item.url);
            });
            allImages = [];
            $("#removeImageScan, #removeAllImageScan, #nextImage, #preImage, #addToContent").attr("disabled", "disabled");
        }
    };

    egov.cshtml.home.scan.addToContent = function () {
        var image = allImages[currentPage];
        image.addToContent = $("#addToContent").prop('checked');
    };

    egov.cshtml.home.transferMultiDocument = {
        startImage: 1,
        endImage: 0,
        bitcount: 24,
        quality: 50,
        grayscale: true
    };

    egov.cshtml.home.transferMultiDocument.add = function (frameName, dtype, isHsmc, acceptFileTypes, fullname, username) {
        var self = this;
        if (!plugin && $("#pluginScan").length === 0) {
            if (FireBreath.isPluginInstalled(pluginName)) {
                $("body").append(FireBreath.injectPlugin(pluginName, "pluginScan", function () {
                    plugin = document.getElementById('pluginScan');
                    self.add(frameName, dtype, isHsmc, acceptFileTypes, fullname, username);
                }));
            } else {
                egov.cshtml.home.plugin.showDialogDownloadPlugin(function () { self.add(frameName, dtype, isHsmc, acceptFileTypes, fullname, username); });
            }
        } else {
            var title = isHsmc ? 'Hồ sơ mới' : 'Văn bản mới';
            var key;
            plugin.openfiledialog(function (files) {
                if (files) {
                    var listFile = JSON.parse(files);
                    if (listFile.length > 0) {
                        if (listDocumentWaitTransfer[dtype]) {
                            var keys = getKeys(listDocumentWaitTransfer[dtype]);
                            var max = _.max(keys, function (k) { return parseInt(k); });
                            key = (max + 1) + '';
                            listDocumentWaitTransfer[dtype][key] = listFile;
                        } else {
                            key = '0';
                            listDocumentWaitTransfer[dtype] = {};
                            listDocumentWaitTransfer[dtype][key] = listFile;
                        }
                        var $activeTab = egov.cshtml.home.tab.getActiveTab();
                        $activeTab.data('onclosed', function () {
                            listDocumentWaitTransfer[dtype][key].splice(0, 1);
                            if (listDocumentWaitTransfer[dtype][key].length > 0) {
                                addDocumentContinue(dtype, title, key, acceptFileTypes, fullname, username);
                            }
                        });
                        onloadDocumentContinue(frameName, dtype, key, acceptFileTypes, fullname, username);
                    }
                }
            });
        }
    };

    egov.cshtml.home.expressPrinter = {};

    egov.cshtml.home.expressPrinter.index = function (printUrl) {
        var printDialog = $("<div id='expressPrinter'>");
        printDialog.load(printUrl);
        var setting = {
            width: 700,
            height: 500,
            title: "Trang in",
            buttons: [
                {
                    text: "In",
                    title: "In danh sách được chọn",
                    click: function () {
                        egov.cshtml.home.expressPrinter.print(printDialog);
                    }
                },
                {
                    text: "Đóng",
                    click: function () {
                        printDialog.dialog("destroy");
                        printDialog.remove();
                    }
                }
            ],
            open: function () {
                $(".ui-dialog-buttonset").prepend("<span style='margin-right: 16px;' checked> Xem trước khi in</span>");
                $(".ui-dialog-buttonset").prepend("<input type='checkbox'>");
            },
            close: function () {
                printDialog.remove();
            }
        };
        printDialog.dialog(setting);
    };

    egov.cshtml.home.expressPrinter.print = function (printDialog) {
        var docCopyIds = [];
        printDialog.find(".print-list-item :checked").each(function () {
            docCopyIds.push($(this).val());
        });
        if (docCopyIds.length === 0) {
            eGovMessage.show("Bạn chưa chọn hồ muốn in.");
            return;
        }
        var templateId = printDialog.find(".printTemplateList").val();
        var printer = new egov.document.egovprint("", docCopyIds, "");
        printer.open(templateId);
    };

    egov.cshtml.home.feedback = function () {
        var option = {
            // default properties
            label: "Gửi phản hồi",
            header: "Gửi phản hồi",
            url: "/Feedback/Send",

            nextLabel: "Tiếp tục",
            reviewLabel: "Xem trước",
            sendLabel: "Gửi",
            closeLabel: "Đóng",

            messageSuccess: "Gửi thành công! Cảm ơn bạn đã góp ý!",
            messageError: "Gửi không thành công! Chúng tôi rất tiếc về sự cố này, Bạn vui lòng thử lại!",

            h2cPath: 'scripts/feedback/html2canvas.js'
        };
        Feedback(option).open();
    };

    var allDoctypeShortKey = [];
    egov.cshtml.home.setting = {
        addDoctypeShortkey: function (frame, existDocType) {
            if (allDoctypeShortKey.length === 0) {
                $.ajax({
                    url: '/Account/GetDocDefaultByUser',
                    type: "GET",
                    success: function (result) {
                        if (result) {
                            allDoctypeShortKey = result;
                            openDialogAddDoctypeShortKey(frame, result, existDocType);
                        }
                    }
                });
            } else {
                openDialogAddDoctypeShortKey(frame, allDoctypeShortKey, existDocType);
            }
        }
    };

    var openDialogAddDoctypeShortKey = function (frame, allDoctype, existDoctype) {
        var $dialogEdit;
        var settings = {};
        settings.width = 440;
        settings.height = 300;
        settings.title = "Danh sách các văn bản ,hồ sơ mặc định";
        settings.buttons = [
                  {
                      text: "Thêm",
                      click: function () {
                          var doctypeAdded = []
                          $('#listDocDefaults tbody input[type=checkbox]').each(function () {
                              var $this = $(this);
                              if ($this.prop('checked')) {
                                  doctypeAdded.push({ argument: $this.attr('data-id'), functionName: $this.parent().siblings().text(), shortKey: '', keyName: '' })
                              }
                          });
                          $('#tbDocDefaultSetting', $('#' + frame).contents()).append($('#tmpRowDocDefault', $('#' + frame).contents()).tmpl(doctypeAdded));
                          settings.close();
                      }
                  },
                  {
                      text: "Bỏ qua",
                      click: function () {
                          settings.close();
                      }
                  }
        ];
        var filter = _.filter(allDoctype, function (item) { return !_.contains(existDoctype, item.DocTypeId); });

        $dialogEdit = $('<div id="addDocDefault"></div>').html($('#tmpListRowDocDefault', $('#' + frame).contents()).tmpl({ items: filter }));

        $dialog.open($dialogEdit, settings, function () {
            $('#listDocDefaults #checkAll').change(function () {
                $('#listDocDefaults tbody input[type=checkbox]').prop('checked', $(this).prop('checked'));
            });
            $("#listDocDefaults").grid({
                isResizeColumn: false,
                isFixHeightContent: false,
                isAutoHideScroll: false
            });
        }, null);
    };

    var getColorTitle = function (color) {
        if (color === 2) {
            return "Văn bản đồng xử lý";
        }
        if (color === 3) {
            return "Văn bản sắp hết hạn (còn 1 ngày)";
        }
        if (color === 4) {
            return "Văn bản khẩn hoặc quá hạn xử lý";
        }
        if (color === 5) {
            return "Văn bản quá hạn hồi báo";
        }
        if (color === 6) {
            return "Văn bản hỏa tốc";
        }
        return "Văn bản bình thường";
    };

    var initTooltip = function (obj) {
        etip.bind(obj);
    };

    /// <summary> Lọc danh sách hồ sơ, văn bản theo ngày</summary>
    /// <param name="target">Ngày được chọn</param>
    egov.cshtml.home.datefilter = function (target) {
        var $selected = getSelectedNode();
        var id = $selected.attr('id');
        $(".grid-content").scrollTop(0);
        if (target === '') {
            reloadListDocuments(id, cacheDocuments[id].columnSetting, cacheDocuments[id].documentsObject);
            bindPaging(id);
        } else {
            var datefilter = cacheDocuments[id].dateFilter;
            if (datefilter != "") {
                var filter = _.filter(cacheDocuments[id].documentsObject, function (item) {
                    return item[datefilter].indexOf(target);
                });
                $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[id].columnSetting));
                $(idTableDocuments + " tr:odd").addClass("trodd");
                bindContextShortCut();
            }
        }
    };

    /// <summary> Lọc danh sách hồ sơ, văn bản theo hạn xử lý</summary>
    /// <param name="target">Số ngày sắp hết hạn</param>
    egov.cshtml.home.overduefilter = function (target) {
        var $selected = getSelectedNode();
        var id = $selected.attr('id');
        $(".grid-content").scrollTop(0);
        if (target === '') {
            reloadListDocuments(id, cacheDocuments[id].columnSetting, cacheDocuments[id].documentsObject);
            bindPaging(id);
        } else {
            target = Globalize.parseDate(target, "yyyy-MM-dd");
            var filter = _.filter(cacheDocuments[id].documentsObject, function (item) {
                var dateFiltered = Globalize.parseDate(item["DateAppointed"], "yyyy-MM-dd'T'HH:mm:ss");
                if (dateFiltered != null) {
                    return (dateFiltered.getDate() == target.getDate()) && (dateFiltered.getMonth() == target.getMonth()) && (dateFiltered.getYear() == target.getYear());
                }
                return false;
            });
            $(idTableDocuments + " tbody").html(bindDocumentRows(filter, cacheDocuments[id].columnSetting));
            $(idTableDocuments + " tr:odd").addClass("trodd");
            bindContextShortCut();
        }
    };

})(window.egov = window.egov || {}, window.jQuery, window._, window.etip, new dialogAdapter())