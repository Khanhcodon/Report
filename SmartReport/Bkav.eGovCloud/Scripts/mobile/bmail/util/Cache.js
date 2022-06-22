(function (bmail) {
    if (!window.localStorage) {
        console.log("localStorage not support");
    }

    if (!window.indexedDB) {
        console.log("indexedDB not support");
    }

    var cacheName = {
        folderList: "folderList",
        mailList: "mailList",
        folder: "mailFolder",
        notify: "notifyData",
    };

    //Các kiểu giao tiếp với indexedDb
    var indexedDbMode = {
        READ_WRITE: "readwrite",
        READ_ONLY: "readonly",
    };

    var BCache = function () { }

    //#region Notify

    BCache.prototype.readAllNotify = function (name) {
        /// <summary>
        /// Đọc tất cả notify
        /// </summary>
        var notifies = this.readCache(name);
        if (notifies == null || notifies == undefined) {
            notifies = JSON.stringify([]);
        }

        return JSON.parse(notifies);
    }

    BCache.prototype.readNotify = function (id) {
        /// <summary>
        /// Đọc notify theo mail Id
        /// </summary>

    }

    BCache.prototype.removeNotify = function (name, id) {
        /// <summary>
        /// Xóa notify
        /// </summary>
        var notifies = this.readAllNotify(name);

        var notify = notifies.find(function (item) {
            return item.id == id;
        });

        notifies.splice(notifies.indexOf(notify), 1);

        this.setCache(name, JSON.stringify(notifies));
    }

    BCache.prototype.removeAllNotify = function (name) {
        /// <summary>
        /// Xóa toàn bộ notify
        /// </summary>
        this.updateNotifies(name, []);
    }

    BCache.prototype.addNotify = function (name, value, notPush) {
        /// <summary>
        /// Thêm notify
        /// </summary>
        /// <param name="value"></param>
        var notifies = this.readAllNotify();
        if (notPush) {
            notifies.unshift(value);
        }
        else {
            notifies.push(value);
        }
        this.setCache(name, JSON.stringify(notifies));
    }

    BCache.prototype.updateNotifies = function (name, notifies) {
        /// <summary>
        /// Cập nhật tất cả notify
        /// </summary>
        /// <param name="value"></param>
        this.setCache(name, JSON.stringify(notifies));
    }

    //#endregion

    BCache.prototype.getFolderList = function () {
        return this.readCache(cacheName.folderList);
    }

    BCache.prototype.setFolderList = function (value) {
        this.setCache(cacheName.folderList, value);
    }

    BCache.prototype.getLastMailList = function () {
        return this.readCache(cacheName.mailList);
    }

    BCache.prototype.setLastMailList = function (value) {
        this.setCache(cacheName.mailList, value);
    }

    BCache.prototype.getLastFolderIndex = function () {
        /// <summary>
        /// Đọc index folder cuối
        /// </summary>
        var index = this.readCache(cacheName.folder);
        if (index && !isNaN(index)) {
            return index;
        }
        return 0;
    }

    BCache.prototype.setLastFolderIndex = function (value) {
        /// <summary>
        /// Ghi cache cho last folder
        /// </summary>
        /// <param name="value"></param>
        this.setCache(cacheName.folder, value);
    }

    BCache.prototype.setCache = function (name, value) {
        /// <summary>
        /// Ghi cache
        /// </summary>
        /// <param name="name">Tên</param>
        /// <param name="value">Giá trị</param>
        value = hashBase64.encode(value.toString());
        if (window.localStorage) {
            window.localStorage.setItem(name, value);
        }
        else if (window.indexedDB) {
            objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
            request = objectStore.add({ "key": name, "value": value, "dateModified": new Date() });
        }
    }

    BCache.prototype.readCache = function (name) {
        /// <summary>
        /// Đọc cache
        /// </summary>
        /// <param name="name">Tên</param>
        var result;
        if (window.localStorage) {
            result = window.localStorage.getItem(name);
        }
        else if (window.indexedDB) {
            var objectStore = this._getObjectStore(indexedDbMode.READ_ONLY);
            result = objectStore.get(name);
        }
        if (result) {
            return hashBase64.decode(result);
        }
    }

    BCache.prototype.removeCache = function (name) {
        /// <summary>
        /// Xóa cache
        /// </summary>
        /// <param name="name">Tên</param>
        if (window.localStorage) {
            window.localStorage.removeItem(name);
        }
        else if (window.indexedDB) {
            objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
            request = objectStore.delete(key);
        }
    }

    BCache.prototype.reset = function () {

        if (window.localStorage) {
            window.localStorage.clear();
        }
        else if (window.indexedDB) {
            objectStore = this._getObjectStore(indexedDbMode.READ_WRITE);
            objectStore.clear();
        }
    }

    BCache.prototype._getObjectStore = function (mode) {
        /// <summary>
        /// Trả về store object trong indexeddb
        /// </summary>
        /// <param name="mode">Chế độ đọc "readwrite" hoặc "readonly"</param>
        /// <returns type="objectStore">objectStore</returns>
        mode = mode ? mode : indexedDbMode.READ_WRITE;
        var transaction, storage;
        //do {
        //    storage = this._storage;
        //}
        //while (!storage || storage === undefined);
        transaction = this._storage.transaction([this._tableName], mode);
        return transaction.objectStore(this._tableName);
    }

    bmail.locache = new BCache();

})
(bmail = window.bmail || {})