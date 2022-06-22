// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk) {
    'use strict';

    if (btalk.cache) {
        return;
    }

    // Viet du lieu cache ra Storage
    function writeCacheStorage(namekey, valuekey) {
        if (typeof (Storage) !== "undefined") {
            localStorage.setItem(namekey, valuekey);
        }
    }

    // Doc du lieu cache tu Storage
    function readCacheStorage(namekey) {
        var valuekey = null;
        if (typeof (Storage) !== "undefined") {
            valuekey = localStorage.getItem(namekey);
        }
        return valuekey;
    }

    // Added DamBV - 23/02/2017 : Doi tuong setting notification.
    function SettingNotifyWithChatter(idChatter, value) {
        this.account = idChatter;
        this.value = value;
        var that = this;

        this.canShow = function () {
            if (that.value == "active" || that.value == null || that.value == "") return true;
            else if (that.value == "noactive") return false;
            else {
                var valueTimeNoteRunNotify = parseInt(that.value);
                var timeNow = new Date().valueOf();
                if (timeNow < valueTimeNoteRunNotify) {
                    return false;
                }
                return true;
            }
        };

        this.delete = function () {
            if (that.value == "active" || that.value == null || that.value == "") {
                return true;
            }
            else {
                var valueTimeNoteRunNotify = parseInt(that.value);
                var timeNow = new Date().valueOf();
                if (timeNow > valueTimeNoteRunNotify) {
                    return true;
                }
                return false;
            }
        }
    }

    /* TamDN - 30/6/2017
    ViewedCache - Lưu thông tin về cache đã xem của 1 chatter, tương ứng
    với một record trong storage
    */
    function ViewedCache(chatterId, strValue) {
        this.chatterId = chatterId;
        this.viewedCache = JSON.parse(strValue);

        if(!this.viewedCache || this.viewedCache[0])
            this.viewedCache = {};

        //Lấy danh sách jids đã xem 1 message dựa vào msgId
        this.getViewedJidsByMsgId = function(msgId) {
            var result = [];
            var keys = Object.keys(this.viewedCache);
            for(var i = 0; i<keys.length; i++) {
                if(this.viewedCache[keys[i]].msgId == msgId)
                    result.push(keys[i]);
            }
            return result;
        }

        //Lưu thông tin vào cache
        this.saveToCache = function(viewedByJid, msgId, secs) {
            //Nếu msgId của message đã xem lớn hơn đang lưu
            //hoặc chưa có thông tin trong cache thì mới cập nhật

            /*if(!this.viewedCache[viewedByJid] || !this.viewedCache[viewedByJid].secs
                    || this.viewedCache[viewedByJid].secs == "undefined" || (this.viewedCache[viewedByJid].secs < secs)) {*/
                var newCacheInfo = {'msgId': msgId, 'secs': secs};
                this.viewedCache[viewedByJid] = newCacheInfo;
                btalk.cache.writeCacheStorage(this.chatterId, JSON.stringify(this.viewedCache));

        }
    }

    /* TamDN - 30/6/2017
    Quản lý cache đã xem trong storage
    */
    function CacheManager() {
        var viewedCacheList = {};

        //Load cache từ storage
        function loadCacheByChatterId(chatterId) {
            var strValue = btalk.cache.readCacheStorage(chatterId);
            var newCache = new btalk.cache.ViewedCache(chatterId, strValue);
            viewedCacheList[chatterId] = newCache;
        }

        //Lấy cache đã xem dựa vào chatterId, nếu chưa load thì load sau đó trả về
        function getViewedCacheByChatterId(chatterId) {
            if(!viewedCacheList[chatterId])
                loadCacheByChatterId(chatterId);
            return viewedCacheList[chatterId];
        }

        //Lấy danh sách các tài khoản đã xem 1 message trong 1 chatter
        this.getViewedListByMsgId = function(chatterId, msgId) {
            var cache = getViewedCacheByChatterId(chatterId);
            return cache.getViewedJidsByMsgId(msgId);
        }

        //Lưu trạng thái đã xem của 1 tài khoản đối với 1 message trong 1 chatter
        this.updateAndSaveToCache = function(chatterId, viewedByJid, msgId, secs) {
            //Nếu là tin của chính mình thì không xử lý
            if(btalk && btalk.auth && typeof btalk.auth.getJID == "function" &&
                    btalk.auth.getJID() == viewedByJid)
                return;

            var cache = getViewedCacheByChatterId(chatterId);
            var oldMsgId = null;
            if(cache.viewedCache[viewedByJid])
                oldMsgId = cache.viewedCache[viewedByJid].msgId;
            cache.saveToCache(viewedByJid, msgId, secs);
            if(btalk.APPVIEW && btalk.APPVIEW.CURRENTCHATTER &&
                    typeof btalk.APPVIEW.CURRENTCHATTER.get == "function" &&
                    btalk.APPVIEW.CURRENTCHATTER.get("jid") == chatterId) {
                btalk.CONVERSATIONDAYS.updateModelsViewed(oldMsgId, msgId, viewedByJid);
            }
            //Cập nhật cache, set lại status các tin khác trong chatter
            btalk.APPVIEW.viewedAllMessage(chatterId);
        }
    }

    btalk.cache = {
        // Cac ham va doi tuong
        ViewedCache: ViewedCache,
        CacheManager: CacheManager,

        SettingNotifyWithChatter: SettingNotifyWithChatter,
        // Cac ham
        writeCacheStorage: writeCacheStorage,
        readCacheStorage: readCacheStorage,

        // Cac bien cau hinh
        statusSizeChatter: "default",
        heightDefaultChatter: 40,
        heightTabHistory: null,

        Name_OpenChatterInfo: "OpenChatterInfo",
        Value_OpenChatterInfo: false,

        Name_SoundMessage: "soundMessage",
        Key_SoundMessage: "active",

        Name_SettingNotification: "SettingNotification",
        ManagerSettingNotification: [],
    }
})(window.jQuery, window.btalk);