(function ($) {
    if (typeof $ === undefined)
        throw getResource("egov.resources.notJqueryAlert");

    var mailType =
    {
        Bmail: 1,
        Gmail: 2,
        Yahoo: 3,
        MailExchange: 4,
        MDaemon: 5
    };

    window.helper = {
        timeShowSoundNotify: 60000,//Thiết lập thời gian để bật âm thanh khi hiển thị notify, tránh khi nhận notify liên tục hiện thị âm thanh liên tục

        hasHideAfterDisplayNotify: false,//Thiết lập có ẩn notify sau khi hiển thị hay không

        timeOutHideNotify: null,// Thiết lập timout khi ẩn notify

        timeHideAfterDisplayNotify: 0,//Thiết lập thời gian ẩn notify khi cấu hình ẩn notify.

        change: true,// Thay đổi title

        isLoad: false,//Thiết lập trạng thái load lấy những notify cũ hơn của người dùng

        isAllData: false,//Thiết lập trạng thái đã load toàn bộ notify của người dùng ra

        isPlayAudio: false,//Thiết lập trạng thái để bật nhạc khi hiển thị notify tránh khi nhận nhiều notify cung lúc thì hiển thị nhiều.

        clearChangeTitle: null,

        connection: {
            isLoaded: false,
            BmailLink: '?egov=1',
            KNTCLink: '/TalkingPeople',
            CalendarLink: '?egov=1&calendar=1',
            ChatLink: '',
            CssLinks: [], // '/mail/Chat/css/ChatEgov.css'
            ContactMobile: '/mobile/#ContactPage',
            JsLinks: [],
            MailType: undefined,
            //[
            //    '/mail/ChatHangout/PopupChat.js',
            //    '/mail/ChatHangout/ChatHangout.js',
            //    '/mail/ChatHangout/MenuButton.js',
            //    '/mail/ChatHangout/WindowChat.js',
            //]
        },

        // TienBV: tạm chuyển hết về eGov Theme
        bkavElements: {
            bmail: { divContainId: "#div-bmail", iframeName: "bmail", tab: "#tab-bmail", name: getResource("egov.resources.bmail"), color: "egov-theme", hasShowChat: true },
            documents: { divContainId: "#div-document", iframeName: "documents", tab: "#tab-document", name: getResource("egov.resources.documentslabel"), color: "egov-theme", hasShowChat: true },
            chat: { divContainId: "#div-chat", iframeName: "chat", tab: "#tab-chat", name: getResource("egov.resources.chat"), color: "egov-theme", hasShowChat: false },
            calendar: { divContainId: "#div-calendar", iframeName: "calendar", tab: "#tab-calendar", name: getResource("egov.resources.calendar"), color: "egov-theme", hasShowChat: true },
            report: { divContainId: "#div-report", iframeName: "report", tab: "#tab-report", name: getResource("egov.resources.reportLabel"), color: "egov-theme", hasShowChat: false },
            kntc: { divContainId: "#div-kntc", iframeName: "kntc", tab: "#tab-kntc", name: getResource("egov.resources.kntc"), color: "egov-theme" },
            links: { divContainId: "#div-links", tab: "#tab-links", name: getResource("egov.resources.links"), color: "egov-theme" },
            statistics: { divContainId: "#div-statistics", iframeName: "statistics", tab: "#tab-statistics", name: getResource("egov.resources.statictisLabel"), color: "egov-theme", hasShowChat: false },
            overall: { divContainId: "#div-overall", iframeName: "overall", tab: "#tab-overall", name: getResource("egov.resources.overall"), color: "egov-theme", hasShowChat: false },
            bwss: { divContainId: "#div-bwss", iframeName: "bwss", tab: "#tab-bwss", name: "bwss", color: "egov-theme" },
            cbcl: { divContainId: "#div-cbcl", iframeName: "cbcl", tab: "#tab-cbcl", name: getResource("egov.resources.overall"), color: "egov-theme" },
            calendarregiter: { divContainId: "#div-calendarregiter", iframeName: "calendarregiter", tab: "#tab-calendarregiter", name: getResource("egov.resources.overall"), color: "egov-theme" }
        },

        isTool: isTool(),//Thiết lập xem có dùng bản desktop hay không

        getContentWindow: function (frameName) {
            if (frameName === undefined) {
                return null;
            }
            var _el = document.getElementsByName(frameName);
            if (_el === undefined || _el.length <= 0) {
                return null;
            }

            return _el[0].contentWindow;
        },

        compareObject: function (aObj, bObj, checkLengthPropery) {
            ///<summary> So sánh 2 giá trị với nhau
            /// Todo: mục đích viết là cho thêm trường checkLengthPropery để kiểm tra vào
            /// Vì dữ liệu model trả về từ server nhiều lúc không có các thuộc tính như  model ở client
            /// nên việc so sánh nhiều khi không đúng lên viết thêm hàm này.
            /// Nếu bỏ qua checkLengthPropery có thể dung _.isEquals của underscore  hoặc, compare của jQuery...
            ///</summary>
            ///<param name="aObj">Giá trị thứ 1</param>
            ///<param name="aObj">Giá trị thứ 2</param>
            ///<param name="checkLengthPropery" type="bool">Có kiểm tra độ dài, số lượng giá trị con hay không nếu là kiểu Object hay Array</param>
            if (typeof aObj !== "object" && typeof bObj !== "object") {
                if (aObj != bObj) {
                    return false;
                }

                return true;
            }
            else {
                if (aObj instanceof Array && bObj instanceof Array) {
                    if (aObj.length !== bObj.length) {
                        return false;
                    }

                    if (aObj.length > 0) {
                        for (var i = 0; i < aObj.length; i++) {
                            if (!this.compareObject(aObj[i], bObj[i], checkLengthPropery, true)) {
                                return false;
                            }
                        }
                    }

                    return true;
                }
                else {
                    try {
                        // Lấy các thuộc tính của các đối tượng
                        var aProps = Object.getOwnPropertyNames(aObj);
                        var bProps = Object.getOwnPropertyNames(bObj);

                        ///Kiểm tra số lượng thuộc tính của object
                        if (checkLengthPropery) {
                            if (aProps.length !== bProps.length) {
                                return false;
                            }
                        }

                        if (aProps.length > 0) {
                            for (var i = 0; i < aProps.length; i++) {
                                var propName = aProps[i];
                                if (!this.compareObject(aObj[propName], bObj[propName], true)) {
                                    return false;
                                }
                            }
                        }

                        return true;

                    } catch (ex) {
                        return false
                    }
                }
            }
        },

        checkExistInArray: function (item, objArr, checkLengthPropery) {
            ///<summary> So sánh 2 giá trị với nhau</summary>
            ///<param name="aObj">Giá trị thứ 1</param>
            ///<param name="objArr" type="Array">Mảng chứa giá trị so sánh</param>
            ///<param name="checkLengthPropery" type="bool">Có kiểm tra độ dài, số lượng giá trị con hay không nếu là kiểu Object hay Array</param>
            if (objArr instanceof Array && objArr.length > 0 && item) {
                for (var i = 0; i < objArr.length; i++) {
                    if (this.compareObject(item, objArr[i], checkLengthPropery)) {
                        return true;
                    }
                }
            }

            return false;
        },

        playAudio: function (url) {
            try {
                var _this = this;
                if (!_this.isPlayAudio) {
                    if (!url) {
                        url = '../Content/Sound/notify.wav';
                    }

                    var audio = new Audio(url);
                    audio.play();

                    _this.isPlayAudio = true;
                    window.setTimeout(function () {
                        _this.isPlayAudio = false;
                    }, _this.timeShowSoundNotify);
                }
            }
            catch (ex) { }
        },

        loadFrame: function (name, onLoadedSuccess) {
            var app = this.bkavElements[name];
            if (!app) {
                return;
            }

            // Load app
            var appFrame = document.getElementsByName(name);

            if (appFrame.length === 0) {
                // Chưa load frame lần nào
                var frame = $('<iframe>').attr('name', name).attr('id', name);
                var that = this;
                if (!that.connection.isLoaded) {
                    $.ajax({
                        url: '/Home/GetConnectionSettings'
                    }).success(function (result) {
                        //Set lại các link liên kết với bmail

                        that.connection.ParentDomain = result.ParentDomain;
                        that.connection.KNTCLink = result.KNTCLink + that.connection.KNTCLink;
                        that.connection.ChatLink = result.ChatLink;
                        that.connection.MailType = result.MailType;

                        if (result.MailType == mailType.Bmail) {
                            that.connection.BmailLink = result.BmailLink + that.connection.BmailLink;
                            that.connection.BmailLink += "&domain" + that.connection.ParentDomain;

                        }
                        else {
                            var mailUrl = localStorage.getItem("mdaemonUrl");
                            if (mailUrl != undefined) {
                                that.connection.BmailLink = mailUrl;
                                that.connection.CalendarLink = mailUrl;
                            }
                            else {
                                that.connection.BmailLink = result.BmailLink;
                                that.connection.CalendarLink = result.BmailLink;
                            }
                        }


                        //for (var i = 0; i < that.connection.JsLinks.length; i++) {
                        that.connection.isLoaded = true;
                        //    that.connection.JsLinks[i] = result.BmailLink + that.connection.JsLinks[i];
                        //}

                        document.domain = that.connection.ParentDomain;

                        var appUrl = that.getAppUrl(name);

                        frame.attr("src", appUrl);

                        $(app.divContainId).append(frame);
                        $(frame).ready(function () {
                            if (typeof onLoadedSuccess === 'function') {
                                onLoadedSuccess();
                            }
                        });

                        that.showHideApp(result);
                    });
                } else {
                    var appUrl = that.getAppUrl(name);
                    $(app.divContainId).append(frame);
                    frame.attr("src", appUrl);
                    $(frame).ready(function () {
                        if (typeof onLoadedSuccess === 'function') {
                            onLoadedSuccess();
                        }
                    });
                }
            }
            else {
                //Refresh trang
                if (name === "kntc") {
                    appFrame[0].src = this.connection.KNTCLink;
                }

                if (typeof onLoadedSuccess === 'function') {
                    onLoadedSuccess();
                }
            }
        },

        displayApp: function (name, onLoadedSuccess) {
            /// <summary>
            /// Hiển thị ứng dụng tương ứng theo tên.
            /// </summary>
            /// <param name="name">Tên ứng dụng: bmail, egov, chat, calendar</param>
            var app,
                menuItem;

            app = this.bkavElements[name];
            if (!app) {
                return;
            }

            menuItem = $('.menu-items a[data-ng-app = "' + name + '"]');

            // Active menu tương ứng
            if (!menuItem.hasClass('active')) {
                $('.menu-items .active').removeClass('active');
                menuItem.addClass('active');
            }
            
            // Show container
            $(app.divContainId).siblings().hide();
            $(app.divContainId).show();

            if (app.iframeName) {
                $('#colorTheme').attr("class", app.color);

                // Load app
                this.loadFrame(name, onLoadedSuccess);
                this.changeSearchApp(name);
            }

            if (name === "links" && $(app.divContainId).is(":empty")) {
                $(app.divContainId).load("/Home/Linkeds");
            }

        },

        isCurrentFrame: function (name) {
            var app,
                menuItem;

            app = this.bkavElements[name];

            if (!app) {
                return false;
            }

            menuItem = $('.menu-items a[data-ng-app = "' + name + '"]');
            return menuItem.hasClass('active');
        },

        getAppUrl: function (name) {
            var result;

            switch (name) {
                case "documents":
                    return "/Home/Index";
                case "chat":
                    return this.connection.ChatLink;
                case "calendar":
                    return this.connection.CalendarLink;
                case "contact":
                    return this.connection.ContactMobile;
                case "report":
                    return "/ReportViewer/Index";
                case "statistics":
                    return "/statistics/Index";
                case "overall":
                    return "/Statistics/Index";
                case "kntc":
                    return this.connection.KNTCLink;
                case "cbcl":
                    return "/EvaluationCriteria/Index";
                case "calendarregiter":
                    return "/Calendar/Index";
                default:
                    result = this.connection.BmailLink;
                    if (this.isTool) {
                        result += '&client=tool'
                    }
                    return result;
            }

        },

        changeCreateButtonName: function (name) {
            switch (name) {
                case "documents":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblDocument"));
                    break;
                case "chat":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewConversion"));
                    break;
                case "calendar":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewWorkTime"));
                    break;
                case "bmail":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewMail"));
                    break;
                default:
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewMail"));
                    break;
            }
        },

        changeSearchApp: function (name) {
            switch (name) {
                case "documents":
                    $('.form-search').show();
                    $(".search-type-btn").show();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchDocument"));
                    break;
                case "chat":
                    $('.form-search').hide();
                    break;
                case "calendar":
                    $('.form-search').hide();
                    break;
                default:
                    $('.form-search').show();
                    $(".search-type-btn").hide();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchMail"));
                    break;
            }
        },

        destroyEvent: function (e) {
            if (!e) {
                return;
            }
            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                // fix cho ie 8
                e.returnValue = false;
            }
            e.stopPropagation();
        },

        changeTitle: function (newTitle, oldTile) {
            ///<summary>
            /// Set thời gian thay đổi title
            ///</summary>
            ///<param name="newTitle" type="string"> Title mới</param>
            ///<param name="oldTile" type="string"> Title cũ</param>
            window.document.title = this.change ? newTitle : oldTile;
            this.change = !this.change;
        },

        timer: function (number) {
            var defaultTitle = "eGovCloud";
            var _this = this;

            if (helper.clearChangeTitle) {
                window.clearInterval(helper.clearChangeTitle);
                window.document.title = defaultTitle;
            }

            if (number > 0) {
                helper.clearChangeTitle = window.setInterval(function () {
                    _this.changeTitle(getResource("egov.resources.youHave") + " " + number + " " + getResource("egov.resources.unreadDocuments"), defaultTitle);
                }, 1000);
            }
        },

        deleteAllLocalStorage: function () {
            ///<summary>
            /// Xóa toàn bộ localstorage
            ///</summary>
            if ('localStorage' in window && window['localStorage'] !== null) {
                window['localStorage'].clear();
            }
        },

        deleteAllCookies: function () {
            ///<summary>
            /// Xóa toàn bộ cookie của trang 
            ///</summary>
            var cookies = document.cookie.split(";");
            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i];
                var eqPos = cookie.indexOf("=");
                var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            }
        },

        setCookie: function (cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        },

        getCookie: function (cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1);
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        },

        loadCss: function (href) {
            var link = $("<link>");
            $("head").append(link);
            link.attr({
                rel: "stylesheet",
                type: "text/css",
                href: href
            });
        },

        loadJs: function (href) {
            $.getScript(href);
        },

        loadBmailResources: function () {
            var that = this;
            $.each(this.connection.CssLinks, function (index, value) {
                that.loadCss(value);
            });
            $.each(this.connection.JsLinks, function (index, value) {
                that.loadJs(value);
            });
        },

        showHideApp: function (connection) {
            if (connection.BmailLink == undefined || connection.BmailLink == "") {
                $(".bmail, .calendar").remove();
            }
            if (connection.ChatLink == undefined || connection.ChatLink == "") {
                $(".chat").remove();
            }
        }
    };

    function getResource(resourceKey) {
        /// <summary>
        /// Trả về ResourceValue theo Key, nếu không tồn tại, trả về ResourceKey
        /// </summary>
        /// <param name="resourceKey"></param>
        try {
            return eval(resourceKey);
        }
        catch (e) {
            return resourceKey;
        }
    }

    function isTool() {
        if (!egov.utilities) {
            require(["util/bkav.utilities"], function () {
                return egov.utilities.url.getQueryStringValue('client') === 'tool';
            });
        }
        else {
            return egov.utilities.url.getQueryStringValue('client') === 'tool'
        }
    }

})(window.jQuery);