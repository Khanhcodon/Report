/// <reference path="../chat/chat_desktop.js" />

(function () {
    var documentFrame,
        isLoadingDoctypes = false,
        _bmailAppName = "bmail",
        _chatAppName = "chat",
        _documentAppName = "documents",
        _mainApp = window.mainApps,
        _keyCode = {
            enter: 13,
            f5: 116
        };

    var pinedDoctypeTemplate = '<li class="newDocument" id="${DocTypeId}" name="${DocTypeName}"><a href="#" class="create-new-document" id="${DocTypeId}" name="${DocTypeName}"><img src="${iconName}" style="width: 25px;"> ${DocTypeName} <img class="pinDocType isPined" src="/Content/Images/Icons/unpin.png" title="Bỏ gắn lên đầu danh sách" id="${DocTypeId}"></a></li>';
    var unpinDoctypeTemplate = '<li class="newDocument" id="${DocTypeId}" name="${DocTypeName}"><a href="#" class="create-new-document" id="${DocTypeId}" name="${DocTypeName}"><img src="${iconName}" style="width: 25px;"> ${DocTypeName} <img class="pinDocType" src="/Content/Images/Icons/pin.png" title="Gắn lên trên đầu danh sách" id="${DocTypeId}"></a></li>';
    var docfieldTemplate = '<li class="docfield-groups"><a href="#"><span class="icon-arrow icon-arrow-right7"></span><span>Lĩnh vực ${DocFieldName}</span><span class="docfieldCount"> (${Count})</span></a><ul class="subDoctype" data-docfieldid="${DocFieldName}" style=""></ul></li>';

    var egovMain = {
        eGovSso: window.eGovSso,
        currentUserName: window.currentUserName,

        init: function () {
            _layoutMain();

            // Đăng xuất
            $('#logout, .btnlogout').on("click", function (e) {
                _mainApp.destroyEvent(e);
                _logout();
            });

            $(".avatar").error(function () {
                $(this).attr("src", "/AvatarProfile/noavatar.jpg");
            });

            $('.app-size').click(function () {
                var appSize = $(this).attr("data-value");
                var appSizeValue = appSize === "small-size" ? 0 : (appSize === "medium-size" ? 1 : 2);
                _changeAppSize(appSizeValue);
            });

            // Thay đổi xem preview
            $('li > a.preview').click(function () {
                var preview = $(this).attr("data-value");
                var documentFrame = mainApps.getContentWindow(_documentAppName);
                if (documentFrame) {
                    documentFrame.egov.views.home.showPreview(preview, true);
                }
            });

            $('.create-new').click(function () {
                _createNewMail();
            });

            $("#resetSystem").on("click", function (e) {
                //_saveChangeBeforeUnload(e, function () {
                //    _resetCache(e);
                //});
                _resetCache(e);
            });

            $("#MainSearchQuery").on("click", function () {
                // Bôi đen chuỗi tìm kiếm khi click vào để tiện xóa.
                $(this)[0].setSelectionRange(0, $(this).val().length);
            });

            $('.form-group .search-btn').click(function () {
                _doSearch();
            });

            $("#installPlugin").on("click", function () {
                _showInstallPlugin();
            });

            $(".search-type-btn a[role=menuitem]").click(function () {
                $("#MainSearchType").val(parseInt($(this).data("value")));
                $("#MainSearchQuery").focus();
            });

            $('#absentDialog .datetime').datepicker({
                dateFormat: "dd/mm",
                onSelect: function (dateText, inst) {
                    $(inst.input).val(new Date().format('HH:mm ') + dateText);
                }
            });

            $("#saveAbsent").click(function () {
                _saveAbsent();
            });

            $("#cancelAbsent").click(function () {
                $('#absentDialog .datetime').val('');
            });
        },

        initKeyPress: function () {
            $(document).keyup(function (e) {
                /*
                 * Lọc danh sách văn bản khi gõ vào ô tìm kiếm.
                 */
                var TextBoxSearchIsFocusing = $("#MainSearchQuery").is(':focus');
                if (TextBoxSearchIsFocusing) {
                    if (e.keyCode === _keyCode.enter) {
                        _doSearch();
                    }
                    _filterDocumentInClient($("#MainSearchQuery").val());
                }
            });

            $(window.document).keydown(function (e) {
                var keycode = e.keyCode;
                if (keycode === _keyCode.f5) {
                    _saveChangeBeforeUnload(e, function () {
                        window.location.reload();
                    });
                }
            });
        },

        initDoctype: function (doctypes) {
            _renderDoctypes(doctypes);
            _bindDoctypesEvent();
        },

        initChat: function (settings) {
            return;
            egov = egov || {};
            egov.setting = settings;

            require(['/scripts/bkav.egov/views/chat/chat_desktop.js'], function (DesktopChat) {
                new DesktopChat();
            });

            $('#startAbsent').val(settings.userSetting.StartAbsent);
            $('#endAbsent').val(settings.userSetting.EndAbsent);
        },

        initNotifications: function (settings) {
            if (!eGovNotification) {
                return;
            }

            egov.eGovNotification = new eGovNotification({ settings: settings });
        },

        registerToGlobal: function () {

            window.readNewMail = function (mailData) {
                /*
                 * Hàm mở mail khi notify sử dụng cho Bmail. Lưu ý, ko đổi tên hàm này.
                 */

                var mailId, folderId;
                if (typeof mailData === "string") {
                    mailData = JSON.parse(mailData);
                }

                mailId = mailData.MailId;
                folderId = mailData.FolderId;

                _mainApp.openApp(_bmailAppName, function () {
                    var frameApp = $("iframe#bmail")[0].contentWindow;
                    if (_.isFunction(frameApp.readNewMail)) {
                        frameApp.readNewMail(mailId, folderId);
                    }
                });
            }

            window.createNewMail = function () {
                _createNewMail();
            }

            window.openChat = function (chatData) {
                /*
                 * Hàm mở notify chat dùng cho chat bmail. Lưu ý, không đổi tên.
                 */
                if (egov.chatDesktop) {
                    // nếu có kích hoạt chat desktop thì bỏ qua xử lý mở menu chat
                    return;
                }

                var chatterJid;
                if (typeof chatData === "string") {
                    chatData = JSON.parse(chatData);
                }

                chatterJid = chatData.ChatterJid;

                _mainApp.openApp(_chatAppName, function () {
                    var frameApp = $("iframe#chat")[0].contentWindow;
                    if (frameApp.btalk && frameApp.btalk.APPVIEW) {
                        frameApp.btalk.APPVIEW.notificationClick({ chatterJid: chatterJid });
                    }
                });
            }

            window.addDocument = function (id, name, contentHTML, attachmentLinks) {
                _createNewDocument(id, name, contentHTML, attachmentLinks);
            }

            window.openDocument = function (id, name) {
                mainApps.openApp(_documentAppName, function () {
                    setTimeout(function () {
                        var egov = currentApp.egov;
                        egov.views.home.tab.openDocument(id, name, true);
                    }, 400)
                });
            }

            window.getDeptAndUsers = function (success) {
                /*
                 * Trả về danh sách cấu trúc phòng ban - người dùng, sử dụng cho các bên bmail, chat, calendar.
                 */
                if (!_.isFunction(success)) {
                    return;
                }

                var egovFrame = mainApps.getContentWindow(_documentAppName);
                if (!egovFrame || !egovFrame.egov || !egovFrame.egov.dataManager) {
                    success([]);
                    return;
                }

                var parseResult = function (users, depts, userDeptPoses) {
                    var results = [];
                    _.each(depts, function (dept) {
                        var userDepts = _.filter(userDeptPoses, function (userDeptPos) {
                            return userDeptPos.departmentid === dept.value;
                        });
                        var usersInDept = _.filter(users, function (user) {
                            return _.pluck(userDepts, "userid").indexOf(user.value) > -1;
                        });
                        var usersForm = [];
                        _.each(usersInDept, function (u) {
                            usersForm.push(
                                {
                                    Username: u.username,
                                    FullName: u.fullname,
                                });
                        });
                        results.push({
                            DepartmentId: dept.value,
                            ParentId: dept.parentid,
                            DepartmentName: dept.data,
                            DepartmentIdExt: dept.idext,
                            DepartmentPath: dept.label,
                            Order: dept.order,
                            Level: dept.level,
                            Users: usersForm
                        });
                    });

                    success(JSON.stringify(results));
                };

                egovFrame.egov.dataManager.getAllUsers({
                    success: function (users) {
                        egovFrame.egov.dataManager.getAllDept({
                            success: function (depts) {
                                egovFrame.egov.dataManager.getAllUserDeptPosition({
                                    success: function (userDeptPoses) {
                                        parseResult(users, depts, userDeptPoses);
                                    }
                                })
                            }
                        });
                    }
                });
            }

            window.logoutDesktop = function () {
                if (!window.external || !_.isFunction(window.external.CB_LoginSuccess)) {
                    return;
                }

                var data = [];
                var loginObj = {
                    user: window.currentUserName,
                    password: "",
                    remember: "true"
                }

                data.push(loginObj);

                var myJsonString = JSON.stringify(data);
                window.external.CB_LoginSuccess(myJsonString);
            }

            window.onSearchDocumentBegin = function () {
                _doSearch();
            }

            window.saveChangeBeforeUnload = function (e) {
                _saveChangeBeforeUnload(e, function () {
                    window.location.reload();
                });
            }

            window.changeBmailResource = function () { }

            window.resetApplication = function () {
                _resetCache();
            }
        },

        showTooltip: function () {
            var qtip = function ($selector) {
                $selector.qtip({
                    position: {
                        at: "center right",
                        my: "left center"
                    }
                });
            }

            var $settingItems = $(".setting-items > a");
            $settingItems.each(function () {
                qtip($(this));
            });

        }
    };

    $(function () {
        egovMain.init();

        egovMain.registerToGlobal();

        egovMain.initKeyPress();

        egovMain.showTooltip();

        window.egovMain = egovMain;
    });

    // #region _private methods

    function _logout() {
        var cookies = document.cookie.split(";");

        for (var i = 0; i < cookies.length; i++) {
            var eqPos = cookies[i].indexOf("=");
            var name = eqPos > -1 ? cookies[i].substr(0, eqPos) : cookies[i];
            name = name.trim();
            if (name === "bkavAuthen") {
                $.cookie(name, "", { domain: document.domain, path: "/", expires: -1 });
                $.cookie(name, "", { expires: -1 });
            }
        }

        logoutDesktop();

        // Tạm bỏ vào để demo trên nhiều tài khoản
        $(window).on('unload', function (e) {
            var egovFrame = _mainApp.getContentWindow(_documentAppName);
            if (egovFrame && egovFrame.egov && egovFrame.egov.dataManager) {
                egovFrame.egov.dataManager.reset({
                    success: function () {
                        window.location.reload(true);
                    }
                });
            }

            _mainApp.deleteAllLocalStorage();
        });

        if (egovMain.currentUserName != undefined) {
            $.get(egovMain.eGovSso + '/User/Logout?userName=' + egovMain.currentUserName, {});
        }

        window.document.location.href = "/account/logout";
    }

    function _layoutMain() {
        var contentLayout = $('.panel-container').layout({
            resizable: false,
            closable: true,
            spacing_closed: 0,
            spacing_open: 0,
            west__spacing_open: 0,
            west__size: 40,
            west__zIndex: 1,
            west__paneSelector: ".west-panel",
            center__paneSelector: ".center-panel"
        });

        //$(".west-panel").hide();

        $('.center-panel').layout({
            resizable: false,
            closable: true,
            spacing_closed: 0,
            spacing_open: 0,
            north__spacing_open: 0,
            north__size: 45,
            north__zIndex: 1,
            north__paneSelector: ".site-header",
            center__paneSelector: ".site-center"
        });
    }

    function _createNewMail() {
        _mainApp.openApp(_bmailAppName, function () {
            window.currentApp.$("#composeMailTab").click();
        });
    }

    function _createNewDocument(id, name, contentHTML, attachmentLinks) {
        /// <summary>
        /// Tạo mới công văn
        /// </summary>
        /// <param name="id">Id công văn</param>
        /// <param name="name">Tên công văn</param>
        /// <param name="contentHTML">nội dung HTML muốn truyền</param>
        _mainApp.openApp(_documentAppName, function () {
            var egovFrame = currentApp,
                userCurrent = egovFrame.egov.setting.userName,
                att = [];

            if (attachmentLinks == undefined || attachmentLinks.length === 0) {
                egovFrame.egov.views.home.tab.addDocument(id, name, null, false, null, contentHTML);
                return;
            }

            $.each(attachmentLinks, function (index, item) {
                att.push({ FileName: item.filename, Url: item.filepart });
            });

            $.ajax({
                type: "POST",
                url: "Attachment/UploadAttachmentInLink",
                data: {
                    urls: JSON.stringify(att)
                },
                success: function (result) {
                    var attachments = [];
                    $.each(result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.message.error(file.name + ": " + file.error);
                        } else {
                            var newAttachment = {
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: file.extension,
                                Versions: [{
                                    Version: 1,
                                    User: userCurrent
                                }],
                                isNew: true
                            };
                            attachments.push(newAttachment);
                        }
                    });
                    egovFrame.egov.views.home.tab.addDocument(id, name, null, false, attachments, contentHTML);
                },
                error: function () {
                }
            });
        });
    }

    function _resetCache(e) {
        e && _mainApp.destroyEvent(e);
        var egovFrame = _mainApp.getContentWindow(_documentAppName);
        $(window).on('unload', function (e) {
            _mainApp.deleteAllLocalStorage();
        });

        if (egovFrame && egovFrame.egov && egovFrame.egov.dataManager) {
            egovFrame.egov.dataManager.reset({
                success: function () {
                    window.location.reload(true);
                }
            });
        } else {
            window.location.reload(true);
        }
    }

    function _filterDocumentInClient(filterValue) {
        ///<summary>
        /// Lọc danh sách văn bản hiện tại khi gõ vào ô tìm kiếm.
        ///</summary>

        var $appSelected = $('.menu-items a.active');
        if ($appSelected.length === 0 || $appSelected.attr('data-ng-app') !== _documentAppName) {
            return;
        }

        if (!currentApp || !currentApp.egov) {
            return;
        }

        currentApp.egov.views.home.documents.clientQuickSearch(filterValue);
    }

    function _doSearch() {
        var searchQuery = $("#MainSearchQuery").val();
        var searchType = parseInt($("#MainSearchType").val());
        var $appSelected = $('.menu-items a.active');
        if ($appSelected.length === 0) {
            return;
        }

        var ngApp = $appSelected.attr('data-ng-app');
        if (ngApp === _documentAppName) {
            if (!currentApp || !currentApp.egov) {
                return;
            }

            currentApp.egov.views.home.tab.addSearch(searchQuery, searchType);
        }
        else if (ngApp.indexOf(_bmailAppName) >= 0) {
            currentApp.EgovLibrary.doSearch(searchQuery);
        }
    }

    function _saveChangeBeforeUnload(e, callback) {
        var hasChange = false;
        var documentframe = mainApps.getContentWindow(_documentAppName);
        if (documentframe) {
            hasChange = documentframe.egov.views.home.tab.hasChangeContent();
            if (hasChange) {
                documentframe.egov.views.home.tab.closeAll(function () {
                    callback();
                });
                e.preventDefault();
                return;
            } else {
                callback();
            }
        } else {
            callback();
        }
    }

    function _showInstallPlugin() {
        var eGdocument = document.getElementById(_documentAppName).contentWindow;
        eGdocument.showInstallPlugin();
    }

    function _saveAbsent() {
        var hasAbsent = false;
        var start = $("#startAbsent").val();
        var end = $("#endAbsent").val();

        if (start != '' && end != '') {
            hasAbsent = true;
        }

        $.ajax({
            url: 'Account/SaveAbsent',
            type: "post",
            data: {
                hasAbsent: hasAbsent,
                startAbsent: start,
                endAbsent: end
            },
            success: function (result) {
                var status = result.hasAbsent ? String.format("Tôi sẽ vắng mặt từ: {0} đến {1}", result.data.start, result.data.end)
                                : "";
                egov.pubsub.publish("chat.changestatus", status);
            }
        })
    }

    //#region Hiển thị loại văn bản cho khởi tạo

    function _renderDoctypes(doctypes) {
        var $doctypeList = $('.new-doctypes'),
           pinnedDocTypes,
           commonDoctypes,
           $pinnedDocTypes,
           $commonDoctypes,
           docFieldIds;

        $doctypeList.find(".newDocument").remove();
        $pinnedDocTypes = $(".pinnedDocTypes");
        $commonDoctypes = $(".commonDocTypes");

        if (doctypes === null || doctypes.length === 0) {
            return;
        }

        pinnedDocTypes = _.filter(doctypes, function (item) {
            return item.Pinned;
        });
        _renderDoctypeGroup(pinnedDocTypes, $pinnedDocTypes, true);
        $pinnedDocTypes.append("<li class='divider'></li>");

        // TienBV: pin những cái được đánh dấu lên lên, còn lại vẫn xử lý bt
        commonDoctypes = doctypes;

        //Hiển thị theo lĩnh vực
        docFieldIds = _.uniq(_.pluck(commonDoctypes, "DocFieldId"));
        
        var docFieldGroups = _.groupBy(commonDoctypes, function (doctype) {
            return doctype.DocFieldName;
        });
        _.each(docFieldGroups, function (doctypes, docfieldName) {
            var docFieldElement = $.tmpl(docfieldTemplate, { DocFieldName: docfieldName, Count: doctypes.length });
            $commonDoctypes.append(docFieldElement);

            var $subDoctype = docFieldElement.find(".subDoctype");
            _renderDoctypeGroup(doctypes, $subDoctype, false);
            $subDoctype.append("<li class='divider'></li>");
            $subDoctype.hide();
        });

        _displayDivider();
    }

    function _renderDoctypeGroup(doctypes, groupElement, isPined) {
        groupElement.empty();
        if (doctypes.length === 0) {
            return;
        }

        doctypes = _.sortBy(doctypes, function (dt) {
            return dt.DocTypeName;
        });

        groupElement.html(_parseDoctypeElement(doctypes, isPined));
    }

    function _bindDoctypesEvent() {
        var $commonDoctypes = $(".commonDocTypes");

        $commonDoctypes.find(".docfield-groups > a").click(function () {
            $(this).siblings("ul").toggle();
            if ($(this).find(".icon-arrow-down7").length > 0) {
                $(this).find(".icon-arrow").removeClass("icon-arrow-down7").addClass("icon-arrow-right7");
            } else {
                $(this).find(".icon-arrow").removeClass("icon-arrow-right7").addClass("icon-arrow-down7");
            }

            return false;
        });

        $('.li-create-new').hover(function () {
            $('.li-create-new').removeClass('unactive');
        }, function () {
            $('.li-create-new').addClass('unactive');
        });

        $(".doctypesFilter").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            return false;
        });

        $("#doctypesSearch").keyup(function () {
            var key = $(this).val();
            if (key.length > 0) {
                $(".newDocument[name *= '" + key + "']").removeClass("hidden").show();
                $(".newDocument").not("[name *= '" + key + "']").hide().addClass("hidden");
            } else {
                $(".newDocument").show().removeClass("hidden");
            }

            $(".docfieldCount").each(function () {
                var $docfield = $(this).parents("li.docfield-groups");
                var doctypesLength = $docfield.find("ul.subDoctype li.newDocument:not('.hidden')").length;
                $(this).text(" (" + doctypesLength + ")");
            });
        });
    }

    function _parseDoctypeElement(doctypes, isPined) {
        doctypes = _.each(doctypes, function (doctype) {
            var iconName = setIconDoctype(doctype.ActionLevel)
            doctype["iconName"] = iconName;
            return doctype;
        })
        var template = isPined ? pinedDoctypeTemplate : unpinDoctypeTemplate;
        var result = $.tmpl(template, doctypes);

        result.find(".create-new-document").click(function () {
            var doctypeId = $(this).attr('id');
            var doctypeName = $(this).attr('name');
            _createNewDocument(doctypeId, doctypeName);

            // ẩn tạo mới
            $('.li-create-new').addClass("unactive");
        });

        result.find(".pinDocType").click(function (e) {
            e.stopPropagation();
            var doctypeId = $(this).attr('id');
            var doctype = _.find(egovMain.doctypes, function (dt) {
                return dt.DocTypeId == doctypeId;
            });
            if (doctype == undefined) {
                return;
            }

            if (!$(this).is(".isPined") && doctype.Pinned) {
                // Chọn Pin nhưng đã tồn tại trên danh sách đã pin.
                return;
            }

            _setDoctypePinedToUser(doctypeId);

            doctype.Pinned = !doctype.Pinned;
            if (doctype.Pinned) {
                var pined = _parseDoctypeElement(doctype, true);
                $(".pinnedDocTypes").prepend(pined);
            } else {
                $(this).parents(".newDocument").remove();
            }
        });

        return result;
    }

    function _setDoctypePinedToUser(doctypeId) {
        /// <summary>
        /// Pin/bỏ pin loại văn bản
        /// </summary>
        if (doctypeId == undefined || doctypeId === "") {
            return;
        }

        $.ajax({
            url: '/Account/PinDocType',
            type: "Post",
            data: {
                docTypeId: doctypeId
            }
        });
    }

    function _displayDivider() {
        /// <summary>
        /// Hiển thị pinned divider
        /// </summary>
        var dividers = $('.new-doctypes').find(".divider");
        dividers.removeClass("hidden");
        _.each(dividers, function (item) {
            if ($(item).siblings("li").length === 0) {
                $(item).addClass("hidden");
            } else {
                $(item).removeClass("hidden");
            }
        });
        dividers.last().addClass("hidden");
    }

    // #endregion

    //#region Thay đổi font

    function _changeAppSize(appSizeValue) {
        var documentFrame = _mainApp.getContentWindow(_documentAppName);
        if (documentFrame) {
            documentFrame.egov.views.home.changeSize(appSizeValue);
        }

        _setCookie("ViewSize", appSizeValue, 365);

        var bmailFrame = _mainApp.getContentWindow(_bmailAppName);
        if (bmailFrame) {
            if (appSizeValue === 0) {
                $('#layout-doc', bmailFrame.document).removeClass("medium-size large-size");
            }
            else {
                $('#layout-doc', bmailFrame.document).removeClass("medium-size large-size");
                $('#layout-doc', bmailFrame.document).addClass(appSizeValue == 1 ? "medium-size" : "large-size");
            }
        }
    }

    function _setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires;
    }

    function setIconDoctype(level) {
        switch (level) {
            case 1:
                return "/Content/bkav.egov/times/ico-nam.png";
            case 2:
                return "/Content/bkav.egov/times/ico-nuanam.png";
            case 3:
                return "/Content/bkav.egov/times/ico-quy.png";
            case 4:
                return "/Content/bkav.egov/times/ico-month.png";
            case 5:
                return "/Content/bkav.egov/times/ico-week.png";
            case 6:
                return "/Content/bkav.egov/times/ico-day.png";
            default:
                return "/Content/bkav.egov/times/ico-khancap.png";
        }
    }

    //#endregion

    // #endregion

    jQuery.fn['editor'] = function (appendTo, callback) {
        /// <summary>
        /// Bind editor
        /// TODO: Hiện tại đang dùng editor ở frame bên dưới, nếu vừa clear cache + thao tác sửa trước khi load xong editor ở frame bên dưới thì sẽ bị lỗi => Nghiên cứu cách tối ưu hơn
        /// </summary>
        /// <param name="appendTo"></param>
        /// <param name="callback"></param>
        if (this) {
            var that = this,
                egovFrame = mainApps.getContentWindow("documents"),
                isLoaded = false;

            if (appendTo == "destroy") {
                return;
            }

            //Gán toolbar đã load trước vào toolbar của document mới
            if (egovFrame.Aloha && egovFrame.Aloha.$toolbar) {
                isLoaded = true;
                if (appendTo.find(egovFrame.Aloha.$toolbar).length == 0) {
                    appendTo.html(egovFrame.Aloha.$toolbar);
                }
            }

            egovFrame.Aloha.ready(function () {
                egovFrame.Aloha.jQuery(that).aloha();
                $(that).attr("spellcheck", false);
                if (!isLoaded) {
                    egovFrame.Aloha.trigger('aloha-editable-activated', {
                        'oldactive': undefined,
                        'editable': egovFrame.Aloha.editables[0]
                    });
                    appendTo.html(egovFrame.Aloha.$toolbar);
                }
                if (typeof callback === "function") {
                    callback();
                }
            });
        }
    }

    //function openLink(url) {
    //    $('.linked').parents('.qtip').hide();
    //    window.open(url, '_blank');
    //}

    //function _getContentWindow(frameName) {
    //    if (frameName === undefined) {
    //        return null;
    //    }
    //    var _el = $(".site-content iframe#" + frameName);
    //    if (_el === undefined || _el.length <= 0) {
    //        return null;
    //    }
    //    return _el[0].contentWindow;
    //}

})();