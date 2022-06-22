(function ($) {

    var $menuItems = $(".menu-items");
    var $divApps = $(".site-content");
    var qtip = function ($selector) {
        $selector.qtip({
            position: {
                at: "center right",
                my: "left center"
            }
        });
    }

    var runCallback = function (callback) {
        if (typeof callback == "function") {
            callback();
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

    qtip($("#btnNewApp"));

    var menuAppTemplate = '<a href="#" class="list-group-item{{if isCustomApp}} customapp{{/if}}" id="${name}" data-ng-app="${name}" title="${title}"><div class="btnDelete" appid="${id}">x</div><img src="${icon}" /></a>';

    window.mainApps = {
        init: function (config) {
            if (config instanceof Array) {
                for (var i = 0; i < config.length; i++) {
                    mainApps.initApp(config[i]);
                }
            }
            else {
                mainApps.initApp(config);
            }
        },

        isTool: isTool(),

        initApp: function (config) {
            var defaultConfig = {
                id: "",
                name: "",
                title: "",
                appUrl: "",
                icon: "",
                isActive: true,
                isMainApp: false,
                isBackgroundApp: false,
                isCustomApp: false
            };

            var thisConfig = $.extend(defaultConfig, config);

            var $thisApp;
            if (thisConfig.isActive) {
                $thisApp = $menuItems.children("[data-ng-app='" + thisConfig.name + "']");
                if ($thisApp.length == 0) {
                    $thisApp = $.tmpl(menuAppTemplate, thisConfig);
                    $menuItems.children().last().before($thisApp);
                } else {
                    $thisApp.removeClass("hidden");
                }
                thisConfig.$elItem = $thisApp;
                qtip($thisApp);
                mainApps.setConfig(thisConfig);

                if (thisConfig.isMainApp) {
                    mainApps.openApp(thisConfig);
                }

                if (thisConfig.isBackgroundApp) {
                    setTimeout(function () {
                        mainApps.addDivApp(thisConfig.name, thisConfig.appUrl);
                    }, 5 * 1000);
                }

                var that = mainApps;
                $thisApp.on("click", function (e) {
                    that.openApp(thisConfig);
                });
            }
            return $thisApp;
        },

        getConfig: function (name) {
            return mainApps[name];
        },

        setConfig: function (config) {
            mainApps[config.name] = config
        },

        deleteAllLocalStorage: function () {
            ///<summary>
            /// Xóa toàn bộ localstorage
            ///</summary>
            if ('localStorage' in window && window['localStorage'] !== null) {
                window['localStorage'].clear();
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

        openApp: function (config, callback) {
            var that = this, divApp, openAppSuccess;
            if (typeof config == "string") {
                config = mainApps.getConfig(config);
                if (!config) {
                    return;
                }
            }

            openAppSuccess = function () {
                try {
                    window.currentApp = that.getContentWindow(config.name);
                    window.currentApp.name = config.name;
                    mainApps.changeSearchApp(config.name);
                    localStorage.setItem("appCurrentOpen", config.name);
                } catch (ex) {
                    // log(ex);
                }
                runCallback(callback);
            };

            $menuItems.children().removeClass("active");
            $menuItems.children("." + config.name).addClass("active");

            if (config.$el != undefined && config.$el.length > 0) {
                divApp = config.$el;
                openAppSuccess();
            } else {
                divApp = mainApps.addDivApp(config.name, config.appUrl, openAppSuccess);
            }

            // Ẩn hiện chat
            if (egov.chatDesktop) {
                egov.pubsub && egov.pubsub.publish('chatdesktop.active', config.hasShowChat);
            }

            divApp.siblings().hide();
            divApp.show();
        },

        getContentWindow: function (name) {
            try {
                return $divApps.find("iframe#" + name)[0].contentWindow;
            } catch (e) {

            }
        },

        addDivApp: function (name, appUrl, callback) {
            var divApp = $divApps.children("." + name);
            if (divApp.length == 0) {
                divApp = $("<div class='" + name + "' style='display: none;' id='div-" + name + "' />");
                var frame = $('<iframe>').attr('name', name).attr('id', name);
                frame.attr("src", appUrl);
                divApp.append(frame);
                $divApps.append(divApp);
                $(frame).ready(function () {
                    runCallback(callback);
                });
            } else {
                runCallback(callback);
            }

            mainApps[name].$el = divApp;

            return divApp;
        },

        changeSearchApp: function (name) {
            switch (name) {
                case "documents":
                    $('.form-search').show();
                    $(".search-type-btn").show();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchDocument"));
                    break;
                case "bmail":
                    $('.form-search').show();
                    $(".search-type-btn").hide();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchMail"));
                    break;
                default:
                    $('.form-search').hide();
                    break;
            }
        },
    };

    var mailType =
    {
        Bmail: 1,
        Gmail: 2,
        Yahoo: 3,
        MailExchange: 4,
        MDaemon: 5
    };

    var getSettingFromServer = function (callback) {
        $.ajax({
            url: '/Home/GetConnectionSettings'
        }).success(function (result) {
            if (localStorage) {
                localStorage.setItem("connectionSettings", JSON.stringify(result));
            }
            callback(result);
        });
    };

    var loadMenu = function (connection) {
        if (mailType.Bmail == connection.MailType) {
            connection.BmailLink = connection.BmailLink + "?egov=1" + "&domain=" + connection.ParentDomain;
            connection.CalendarLink = connection.BmailLink + "?egov=1&calendar=1" + "&domain=" + connection.ParentDomain;
        }
        else {
            var mailUrl = localStorage.getItem("mdaemonUrl");
            if (mailUrl != undefined) {
                connection.BmailLink = mailUrl;
                connection.CalendarLink = mailUrl;
            }
        }

        systemApps = systemApps.sort(function (a, b) { return a.Order - b.Order; });
        for (var i in systemApps) {
            var app = systemApps[i];
            var appName = app.Name;
            if (appName === "bmail" || appName === "chat") {
                if (app.AppUrl) {
                    if (app.AppUrl.indexOf("?") > 0) {
                        app.AppUrl += "&domain=" + connection.ParentDomain;
                    } else {
                        app.AppUrl += "?domain=" + connection.ParentDomain;
                    }
                }
            }

            new mainApps.init({
                id: app.Id,
                name: app.Name,
                title: app.Title,
                icon: app.IconUrl,
                appUrl: app.AppUrl,
                isBackgroundApp: app.IsBackgroundApp,
                isMainApp: app.IsDefaultApp,
                isActive: app.IsActived,
                hasShowChat: (appName === "documents" || appName === "bmail")
            });
        }

        //document.domain = connection.ParentDomain;
    }

    if (localStorage) {
        egov = egov || {};
        var localConnection = localStorage.getItem("connectionSettings");
        if (!localConnection) {
            getSettingFromServer(function (connection) {
                egov.connections = connection;
                loadMenu(connection);
            })
        } else {
            loadMenu(JSON.parse(localConnection));
            egov.connections = JSON.parse(localConnection);
        }
    }

})(window.jQuery);

