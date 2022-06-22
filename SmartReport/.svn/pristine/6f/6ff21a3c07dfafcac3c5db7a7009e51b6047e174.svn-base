(function () {
    "use strict";

    //#region private field

    var searchOptions, _resources;

    _resources = egov.resources.searchBar;
    searchOptions = [
        {
            id: 0,
            value: "s:",
            iconClass: "icon-search",
            isAutoComplete: false,
            //name: _resources.searchDocumentOption.name,
            //label: _resources.searchDocumentOption.label,
            //desc: _resources.searchDocumentOption.desc,
            //defaultLabel: _resources.searchDocumentOption.defaultLabel
        },
        {
            id: 1,
            value: "sm:",
            iconClass: "icon-search",
            isAutoComplete: false,
            //name: _resources.searchMailOption.name,
            //label: _resources.searchMailOption.label,
            //desc: _resources.searchMailOption.desc,
            //defaultLabel: _resources.searchMailOption.defaultLabel
        },
        {
            id: 2,
            value: "sc:",
            iconClass: "icon-search",
            isAutoComplete: false,
            //name: _resources.searchChatOption.name,
            //label: _resources.searchChatOption.label,
            //desc: _resources.searchChatOption.desc,
            //defaultLabel: _resources.searchChatOption.defaultLabel
        },
        {
            id: 3,
            value: "sf:",
            iconClass: "icon-search",
            isAutoComplete: false,
            //name: _resources.searchFileOption.name,
            //label: _resources.searchFileOption.label,
            //desc: _resources.searchFileOption.desc,
            //defaultLabel: _resources.searchFileOption.defaultLabel
        },
        {
            id: 4,
            value: "m:",
            iconClass: "icon-mail5",
            isAutoComplete: true,
            isAccountAutoComplete: true,
            //name: _resources.createMailOption.name,
            //label: _resources.createMailOption.label,
            //desc: _resources.createMailOption.desc,
            //defaultLabel: _resources.createMailOption.defaultLabel
        },
        {
            id: 5,
            value: "c:",
            iconClass: "icon-chat",
            isAutoComplete: true,
            isAccountAutoComplete: true,
            //name: _resources.createChatOption.name,
            //label: _resources.createChatOption.label,
            //desc: _resources.createChatOption.desc,
            //defaultLabel: _resources.createChatOption.defaultLabel
        },
        {
            id: 6,
            value: "d:",
            iconClass: "icon-folder-open",
            isAutoComplete: true,
            isAccountAutoComplete: false,
            isDocumentAutoComplete: true,
            name: "Khởi tạo hồ sơ, văn bản",
            label: "Khởi tạo hồ sơ {0}",
            desc: "Khởi tạo hồ sơ {0}",
            defaultLabel: "Khởi tạo hồ sơ {0}"
        },
    ];

    //#endregion

    $(function () {
        $.widget("egov.searchBar", {
            template: $("#searchBarTemplate"),

            // Contructor
            _create: function () {
                var that;

                if (navigator.isMobile && window.innerWidth < 768) {
                    // Trường hợp mobile bỏ qua
                    return;
                }

                that = this;
                this.searchOption;
                this.doctypeSelected;
                this.searchQuery;

                this.element.append(this.template.html());

                this.searchInput = this.element.find(".search-bar-input")
                this.searchHelper = this.element.find(".search-bar-helper");
                this.searchTerm = this.element.find(".search-term");
                this.searchDesc = this.element.find(".search-desc");
                this.isOtherOption = false;
                this.searchQueryIsValid = true;

                this._initialize(function () {
                    that.searchInput.removeAttr("disabled");
                });

                this.searchInput.bind("keyup", function (event) {
                    that._searchQuery(event);
                });

                this.searchInput.bind("click", function (event) {
                    that._focusAndBindEvent(event);
                    event.preventDefault();
                });

                this.searchInput.focus(function () {
                    // that._focus();
                    that._focusAndBindEvent(event);
                });

                this.searchInput.blur(function () {
                    that._blur();
                });
            },

            _focusAndBindEvent: function (event) {
                var length = this.searchInput.val().length;
                this.searchInput[0].setSelectionRange(0, length);
                this._focus();
                this.searchOption = this._findSearchOption();
                // this._searchQuery(event);
            },

            _searchQuery: function (event) {
                var keyCode;

                keyCode = event.keyCode || event.which;

                this.searchOption = this._findSearchOption();

                switch (keyCode) {
                    case 32: // space
                        if (!this.isOtherOption) {
                            this.isOtherOption = this.searchOption.value != searchOptions[0].value;
                            if (this.isOtherOption && this.searchOption.isDocumentAutoComplete) {
                                this._hideHelperElement();
                            }
                        }
                        break;
                    case 8: // backspace
                        this.isOtherOption = this.searchOption.value != searchOptions[0].value;
                        break;
                    case 9: // tab
                        if ($(this).autocomplete("instance").menu.active) {
                            event.preventDefault();
                        }
                        break;
                    case 13: // enter
                        this._fireSearchAction();
                        break;
                    case 46: // del
                        break;
                    default:
                        this.searchOption = this._findSearchOption();
                        break;
                }

                this._showSearchTermHelper();
                this._bindAutoCompleteData();
                this._clientDocumentFilter();
            },

            _initialize: function (callback) {
                var i, option, helperKey, $li;

                for (i = 0; i < searchOptions.length; i++) {
                    option = searchOptions[i];
                    helperKey = $("<span>").addClass("search-helper-key").html("Gõ " + option.desc);
                    $li = $("<div>").addClass("list-group-item").append("<span class='icon " + option.iconClass + "'></span>").append(option.name + ":").append(helperKey);
                    this.searchHelper.find(".list-group").append($li);
                }

                // Hàm ngoài  main.js
                getDocTypeList(function (result) {
                    // Gán currentDoctypes để lúc tạo từ nút (+) không phải load lại nữa.
                    currentDoctypes = result == null ? [] : result;
                    callback();
                    // Tạm bỏ autocomplete user
                    //getUsers(function () {
                    //    callback();
                    //});
                });
            },

            _findSearchOption: function () {
                /// <summary>
                /// Trả về kiểu searchOption từ nội dung gõ
                /// </summary>
                var result;
                var term = this.searchInput.val();

                if (!String.isNullOrEmpty(term)) {
                    result = _.find(searchOptions, function (itm) {
                        return term.toLowerCase().trimStart().indexOf(itm.value + " ") == 0;
                    });
                }

                if (result == undefined) {
                    result = term.startWith("@") ? searchOptions[5] : this._defaultSearchOption();

                    if (this._autocompleteSearchInputIsLoaded()) {
                        this.searchInput.autocomplete("destroy");
                    }

                    this.doctypeSelected = null;
                }

                return result;
            },

            _fireSearchAction: function () {
                /// <summary>
                /// Phân tích cú pháp tìm kiếm và thực thi công việc tương ứng
                /// </summary>
                this.searchQuery = this._parseSearchQuery(this.searchOption);
                if (!this.searchQueryIsValid) {
                    return;
                }

                this.searchHelper.hide();
                this.searchInput.blur();

                switch (this.searchOption.value) {
                    case "d:": // khởi tạo văn bản
                        this._addDocument();
                        break;
                    case "c:": // Tạo chat
                        this._addChat();
                        break;
                    case "m:": // Tạo mail
                        this._addMail();
                        break;
                    case "sm:": // Tìm kiếm mail
                        this._searchMail();
                        break;
                    case "s:": // Tìm kiếm văn bản
                        this._searchDocument();
                        break;
                    case "sf:": // Tìm kiếm file
                        this._searchFile();
                        break;
                    case "sc:": // Tìm kiếm nội dung chat
                        this._searchChat();
                        break;
                    default:
                        break;
                }
            },

            _parseSearchQuery: function () {
                /// <summary>
                /// Phân tích cú pháp từ ô tìm kiếm và trả về kết quả
                /// </summary>
                var result = [];
                var searchVal = this.searchInput.val();
                searchVal = searchVal.replace(this.searchOption.value, "").trimStart();

                switch (this.searchOption.value) {
                    case "d:":
                        result[1] = this.doctypeSelected;
                        result[0] = searchVal;
                        break;
                    case "s:", "sm:", "sc:", "sf:":
                        result[0] = searchVal;
                        break;
                    case "c:":
                        var firstSpace = searchVal.indexOf(" ");
                        result[0] = searchVal.substr(0, firstSpace);
                        if (searchVal.length > firstSpace + 1) {
                            result[1] = searchVal.replace(result[0], "").trim();
                        }
                        break;
                    case "m:":
                        var firstSpace = searchVal.indexOf(" ");
                        result[0] = searchVal.substr(0, firstSpace);
                        if (searchVal.length > firstSpace + 1) {
                            var content = searchVal.replace(result[0], "").trim().split("@");
                            result[1] = content[0];
                            if (content.length > 1) {
                                result[2] = content[1];
                            }
                        }
                        break;
                    default:
                        if (searchVal.startWith("@")) {
                            var firstSpace = searchVal.indexOf(" ");
                            result[0] = searchVal.substr(0, firstSpace).replace("@", "");
                            result[1] = searchVal.replace(result[0], "").trim();
                        } else {
                            result[0] = searchVal;
                        }
                        break;
                }

                this.searchQueryIsValid = true;

                // Hiển thị gợi ý mặc định khi người dùng chưa nhập gì
                if (String.isNullOrEmpty(result[0])) {
                    this.searchQueryIsValid = false;
                    result = this.searchOption.defaultLabel;
                } else {
                    result = $.extend([], this.searchOption.defaultLabel, result);
                }

                return result;
            },

            _showHelperElement: function () {
                this.searchHelper.find("div.list-group-item:not('.active')").show();
            },

            _hideHelperElement: function () {
                this.searchHelper.find("div.list-group-item:not('.active')").hide();
            },

            _focus: function () {
                this.searchHelper.show();
                this._showHelperElement();
            },

            _blur: function () {
                this.searchHelper.hide();
                this._hideHelperElement();
            },

            _addDocument: function () {
                /// <summary>
                /// Khởi tạo văn bản
                /// </summary>
                var query = this.searchQuery;

                // Hàm ngoài main.js - line 24
                addDocument(query[1], query[0]);
            },

            _addMail: function () {
                /// <summary>
                /// Khởi tạo mail
                /// </summary>
                var searchQuery = this.searchQuery[0];
                helper.displayApp("bmail", function () {
                    alert("Comming soon");
                });
            },

            _addChat: function () {
                /// <summary>
                /// Tạo chát
                /// </summary>
                var searchQuery = this.searchQuery[0];
                helper.displayApp("chat", function () {
                    alert("Comming soon");
                    return false;
                });
            },

            _searchMail: function () {
                /// <summary>
                /// Tìm kiếm mail
                /// </summary>
                var searchQuery, that;

                searchQuery = this.searchQuery[0];
                that = this;

                helper.displayApp("bmail", function () {
                    _searchMailAfterLoadFrame(searchQuery);
                    return false;
                });
            },

            _searchMailAfterLoadFrame: function (searchQuery) {
                var waitTimeOut;
                try {
                    var bmailFr = helper.getContentWindow("bmail");
                    if (bmailFr.tabView != undefined) {
                        bmailFr.EgovLibrary.doSearch(searchQuery);
                        clearTimeout(waitTimeOut);
                    }
                }
                catch (err) {
                    waitTimeOut = setTimeout(function () {
                        that._searchMailAfterLoadFrame(searchQuery);
                    }, 300);
                }
            },

            _searchChat: function () {
                /// <summary>
                /// Tìm kiếm theo nội dung hội thoại
                /// </summary>
                var searchQuery = this.searchQuery[0];
                helper.displayApp("chat", function () {
                    alert("Comming soon");
                    return false;
                });
            },

            _searchDocument: function (isSearchFile) {
                /// <summary>
                /// Tìm kiếm văn bản
                /// </summary>
                /// <param name="isSearchFile">Giá trị xác định có tìm kiếm nội dung file không</param>
                var that = this;
                var searchQuery = this.searchQuery[0];

                isSearchFile = isSearchFile || false;

                helper.displayApp("documents", function () {
                    var documentFrame = helper.getContentWindow("documents");
                    documentFrame.egov.message.notification('Đang tìm...');

                    documentFrame.egov.views.home.tab.addSearch(searchQuery, isSearchFile ? 2 : 1);
                });
            },

            _searchFile: function () {
                /// <summary>
                /// Tìm kiếm nội dung file
                /// </summary>
                this._searchDocument(true);
            },

            _bindAutoCompleteData: function () {
                /// <summary>
                /// Hiển thị autocomplete
                /// </summary>
                var searchOption = this.searchOption;
                var that = this;
                var resource = [];

                if (!searchOption.isAutoComplete) {
                    return;
                }

                if (searchOption.isDocumentAutoComplete) {
                    resource = _.map(currentDoctypes, function (itm) {
                        return {
                            label: itm.DocTypeName,
                            nameUnsign: itm.DocTypeName.toLowerCase().removeVietnamChars(),
                            value: itm.DocTypeId
                        };
                    });
                }
                // TienBV: tạm bỏ phần này, sau có nhu cầu xem lại
                //else {
                //    resource = _.map(allUser, function (itm) {
                //        return {
                //            label: itm.username,
                //            nameUnsign: itm.username.toLowerCase(),
                //            value: itm.username
                //        };
                //    });
                //};

                if (this._autocompleteSearchInputIsLoaded()) {
                    return;
                }

                this.searchInput.autocomplete({
                    minLength: 0,
                    appendTo: ".search-bar-autocomplete",
                    source: function (request, response) {
                        var term = request.term;
                        term = term.replace(searchOption.value + " ", "").removeVietnamChars();
                        response(_.filter(resource, function (itm) {
                            return itm.nameUnsign.startWith(term);
                        }));
                    },
                    focus: function (event, ui) {
                        return false;
                    },
                    select: function (event, ui) {
                        var terms = this.value;
                        var selected = ui.item.label;
                        this.value = searchOption.value + " " + selected;

                        // todo: hiện đang chỉ dùng cho khởi tạo văn bản => khi thêm autocomplete user cần thêm điều kiện
                        that.doctypeSelected = ui.item.value;
                        return false;
                    },
                    open: function () {
                        that._hideHelperElement();

                        $(".search-bar-autocomplete").show();
                        $(this).autocomplete("widget").addClass("list-group").attr("style", "");
                        $(this).autocomplete("widget").find("li").addClass("list-group-item");
                    },
                    close: function () {
                        $(".search-bar-autocomplete").hide();
                        that.searchHelper.show();
                        that._showHelperElement();
                    },
                    create: function () {
                        $(".search-bar-autocomplete").show();
                    }
                });
            },

            _showSearchTermHelper: function () {
                /// <summary>
                /// Hiển thị gợi ý khi người dùng gõ lệnh
                /// </summary>
                var searchQuery = this._parseSearchQuery();
                this.searchDesc.text(String.format(this.searchOption.label, searchQuery));
            },

            _clientDocumentFilter: function () {
                var searchQuery = this.searchInput.val();
                var $appSelected = $('.menu-items a.active');

                if ($appSelected.length > 0) {
                    var app = helper.bkavElements[$appSelected.attr('data-ng-app')];
                    if (app.iframeName === "documents") {
                        var documentFrame = helper.getContentWindow(app.iframeName);
                        var egov = documentFrame.egov;

                        if (this.searchOption.value !== "s:") {
                            egov.views.home.documents.clientQuickSearch("");
                            return;
                        }

                        if (searchQuery.startWith("s:")) {
                            egov.views.home.documents.clientQuickSearch("");
                            return;
                        }

                        egov.views.home.documents.clientQuickSearch(searchQuery);
                    }
                }
            },

            _defaultSearchOption: function () {
                var $appSelected = $('.menu-items a.active');
                if ($appSelected.length > 0) {
                    var app = helper.bkavElements[$appSelected.attr('data-ng-app')];
                    if (app.iframeName === "bmail") {
                        return searchOptions[1];
                    } else if (app.iframeName === "chat") {
                        return searchOptions[2];
                    } else {
                        return searchOptions[0];
                    }
                }
            },

            _autocompleteSearchInputIsLoaded: function () {
                return this.searchInput.hasClass(".ui-autocomplete-input");
            },

            _autocompleteSearchInputIsShowing: function () {
                return $(this.searchInput.autocomplete('widget')).is(':visible')
            }
        });

        $("#formSearch").searchBar();
    });

    function onSearchDocumentBegin(searchQuery) {
        // var searchQuery = $("#MainSearchQuery").val();
        var searchType = parseInt($("#MainSearchType").val());
        if (searchQuery == '') {
            return false;
        }
        var $appSelected = $('.menu-items a.active');
        if ($appSelected.length > 0) {
            var app = helper.bkavElements[$appSelected.attr('data-ng-app')];
            if (app.iframeName === "bmail") {
                var bmailFr = helper.getContentWindow(app.iframeName);
                if (navigator.isMobile) {
                    bmailFr.searchMail(0, searchQuery, 1000);
                }
                else {
                    bmailFr.EgovLibrary.doSearch(searchQuery);
                }
                return false;
            }
            else if (app.iframeName === "documents") {
                helper.displayApp("documents", function () {
                    var documentFrame = helper.getContentWindow(app.iframeName);
                    documentFrame.egov.message.notification('Đang tìm...');
                    if (!navigator.isMobile) {
                        documentFrame.egov.views.home.tab.addSearch(searchQuery, searchType);
                    }
                    else {
                        documentFrame.egov.message.notification('Đang tìm...');

                        if (!documentFrame.egov.views.home.tab) {
                            documentFrame.egov.require(['tabView'], function (TabView) {
                                var tabDocument = new TabView({});
                                documentFrame.egov.views.home.tab.addSearchMobile(searchQuery);
                                documentFrame.egov.mobile.$backToListButton.click(function () {
                                    $("#MainSearchQuery").val('');
                                });
                            });
                        }
                        else {
                            documentFrame.egov.views.home.tab.addSearchMobile(searchQuery);
                        }
                    }
                });
            }
        }
    }

    function getUsers(callback) {
        var waitTimeout;
        var egovFrame = helper.getContentWindow(helper.bkavElements["documents"]["iframeName"]);
        try {
            if (egovFrame.egov) {
                egovFrame.egov.dataManager.getAllUsers({
                    success: function (data) {
                        allUser = data;
                        callback();
                    }
                });
            }
            clearTimeout(waitTimeout);
        }
        catch (err) {
            if (!waitTimeout) {
                waitTimeout = setTimeout(function () {
                    getUsers(callback);
                }, 300);
            }
        }
    }
})()