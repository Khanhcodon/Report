/**
 * Copyright 2012 Tsvetan Tsvetkov
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Author: Tsvetan Tsvetkov (tsekach@gmail.com)
 */
(function (win) {
    var PERMISSION_DEFAULT = "default",
    PERMISSION_GRANTED = "granted",
    PERMISSION_DENIED = "denied",
    PERMISSION = [PERMISSION_GRANTED, PERMISSION_DEFAULT, PERMISSION_DENIED],
    defaultSetting = {
        pageVisibility: false,
        autoClose: 0
    },
    enumStyle = ["basic", "image", "list", "progress"],
    empty = {},
    emptyString = "",
    isSupported = (function () {
        var isSupported = false;
        try {
            isSupported = !!(/* Safari, Chrome */win.Notification || /* Chrome & ff-html5notifications plugin */win.webkitNotifications || /* Firefox Mobile */navigator.mozNotification || /* IE9+ */(win.external && win.external.msIsSiteMode() !== undefined));
        } catch (e) { }
        return isSupported;
    }()),
    ieVerification = Math.floor((Math.random() * 10) + 1),
    isFunction = function (value) { return (value && (value).constructor === Function); },
    isString = function (value) { return (value && (value).constructor === String); },
    isObject = function (value) { return (value && (value).constructor === Object); },
    mixin = function (target, source) {
        var name, s;
        for (name in source) {
            s = source[name];
            if (!(name in target) || (target[name] !== s && (!(name in empty) || empty[name] !== s))) {
                target[name] = s;
            }
        }
        return target; // Object
    },
    noop = function (PERMISSION_GRANTED) {
        if (Notification) {
            Notification.permission = PERMISSION_GRANTED;
        }
    },
    settings = defaultSetting;

    function getNotification(options) {
        //dcm
        var notification;
        if (win.external && win.external.msIsSiteMode()) { /* IE9+ */
            //Clear any previous notifications
            win.external.msSiteModeClearIconOverlay();
            win.external.msSiteModeSetIconOverlay((isString(options.icon) ? options.icon : options.icon.x16), options.title);
            win.external.msSiteModeActivate();
            notification = { "ieVerification": ieVerification + 1 };
        }
        else if (win.Notification) {
            notification = new win.Notification(options.title, {
                icon: isString(options.icon) ? options.icon : options.icon.x32,
                body: options.body || emptyString,
                tag: options.tag || emptyString
            });

        } else if (win.webkitNotifications) { /* FF with html5Notifications plugin installed */
            notification = win.webkitNotifications.createNotification(options.icon, options.title, options.body);
            notification.show();
        } else if (navigator.mozNotification) { /* Firefox Mobile */
            notification = navigator.mozNotification.createNotification(options.title, options.body, options.icon);
            notification.show();
        }
        return notification;
    }

    function getWrapper(notification) {
        return {
            close: function () {
                if (notification) {
                    if (notification.close) {
                        notification.onshow(function () { });
                    } else if (win.external && win.external.msIsSiteMode()) {
                        if (notification.ieVerification === ieVerification) {
                            win.external.msSiteModeClearIconOverlay();
                        }
                    }
                }
            }
        };
    }

    function requestPermission() {
        if (!isSupported) { return; }
        var callbackFunction = noop;
        if (win.webkitNotifications && win.webkitNotifications.checkPermission) {
            win.webkitNotifications.requestPermission(callbackFunction);
        } else if (win.Notification && win.Notification.requestPermission) {
            win.Notification.requestPermission(callbackFunction);
        }
    }

    function permissionLevel() {
        var permission;
        if (!isSupported) { return; }
        if (win.Notification && win.Notification.permissionLevel) {
            //Safari 6
            permission = win.Notification.permissionLevel();
        } else if (win.webkitNotifications && win.webkitNotifications.checkPermission) {
            //Chrome & Firefox with html5-notifications plugin installed
            permission = PERMISSION[win.webkitNotifications.checkPermission()];
        } else if (navigator.mozNotification) {
            //Firefox Mobile
            permission = PERMISSION_GRANTED;
        } else if (win.Notification && win.Notification.permission) {
            // Firefox 23+
            permission = win.Notification.permission;
        } else if (win.external && (win.external.msIsSiteMode() !== undefined)) { /* keep last  && (win.external.msIsSiteMode() !== undefined)*/
            //IE9+
            permission = win.external.msIsSiteMode() !== undefined ? PERMISSION_GRANTED : PERMISSION_DEFAULT;
        }
        return permission;
    }

    /**
     *  
     */
    function config(params) {
        if (params && isObject(params)) {
            mixin(settings, params);
        }
        return settings;
    }

    function isDocumentHidden() {
        return settings.pageVisibility ? (document.hidden || document.msHidden || document.mozHidden || document.webkitHidden) : true;
    }

    //Tạo và hiển thị notification
    function createNotification(options) {
        var notification, notificationWrapper;
        if (isString(options.title) && (isString(options.icon) || isobject(options.icon))) {
            notification = getNotification(options);
        }
        var clickFunc = isFunction(options.click) ? options.click : function () { };
        notification.addEventListener("click", clickFunc);
        notificationWrapper = getWrapper(notification);

        return notificationWrapper;
    }

    //Kiểm tra trình duyệt: Có hỗ trợ notification hay không?.
    //Khác Ie => ie chưa hỗ trợ notification nhưng các hàm kiểm tra vẫn đúng lên dùng hàm isIE() để loại bỏ ie.
    function hasNotify() {
        return ((!isIE() && isSupported) && (isDocumentHidden() && (permissionLevel() === PERMISSION_GRANTED)))
    }

    //Kiểm tra trình duyệt đang chạy có phải là ie hay không
    function isIE() {
        return navigator.userAgent.indexOf('MSIE') > -1;
    }

    win.notify = {
        PERMISSION_DEFAULT: PERMISSION_DEFAULT,
        PERMISSION_GRANTED: PERMISSION_GRANTED,
        PERMISSION_DENIED: PERMISSION_DENIED,
        isSupported: isSupported,
        config: config,
        createNotification: createNotification,
        permissionLevel: permissionLevel,
        requestPermission: requestPermission,
        hasNotify: hasNotify
    };

    if (isFunction(Object.seal)) {
        //Object.seal(win.notify);
        win.PERMISSION_DEFAULT = PERMISSION_DEFAULT;
        win.PERMISSION_GRANTED = PERMISSION_GRANTED;
        win.PERMISSION_DENIED = PERMISSION_DENIED;
        win.isSupported = isSupported;
        win.config = config;
        win.createNotification = createNotification;
        win.permissionLevel = permissionLevel;
        win.requestPermission = requestPermission;
        win.hasNotify = hasNotify;
    }
}(window));
