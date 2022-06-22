(function (window, egov, Modernizr) {

    'use strict';

    //if (typeof Modernizr === 'undefined') {
    //    throw 'Chưa có thư viện Modernizr';
    //}

    //Biến tạm, dùng trong trường hợp trình duyệt không hỗ trợ localStorage lẫn indexDb
    var TempStorage = function () {
        return this._initialize();
    };

    TempStorage.prototype._initialize = function () {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        this._storage = {};
        this.hasSupport = true;
        return this;
    }

    TempStorage.prototype.get = function (key, callback) {
        var that = this,
            result;
        result = this._storage[key];
        if (result) {
            try {
                egov.callback(callback, JSON.parse(result));
            } catch (e) {
                var resultNew = JSON.parse(window.hashBase64.fromBase64(result));
                if (resultNew.value == null || resultNew.value == undefined) {
                    egov.locache.get(key, function (value) {
                        resultNew.value = value;
                        egov.locache.update(key, resultNew);
                        egov.callback(callback, resultNew);
                    });
                }
                else {
                    egov.callback(callback, resultNew);
                }
            }
        } else {
            egov.callback(callback);
        }
    }

    TempStorage.prototype.insert = function (key, value, callback) {
        this._storage[key] = JSON.stringify({ key: key, value: value, dateModified: new Date() });
        egov.callback(callback, value);
    }

    TempStorage.prototype.update = function (key, value, callback) {
        this._storage[key] = JSON.stringify({ key: key, value: value, dateModified: new Date() });

        egov.callback(callback, value);
    }

    TempStorage.prototype.delete = function (key, callback) {
        if (this._storage[key]) {
            this._storage[key] = null;
        }

        egov.callback(callback, key);
    }

    TempStorage.prototype.reset = function (callback) {
        this._storage = {};

        egov.callback(callback);
    }
    
    //#region LocalStorage

    var LocalStorage = function () {
        /// <summary>
        /// Lớp quản lý dữ liệu trong localStorage.
        /// localStorage: 
        ///     5MB
        ///     No Expire Date
        ///     Firefox 3.5+
        ///     Chrome 4+
        ///     IE 8+
        ///     Safari 4+
        ///     Opera 10.5+
        ///     Android 2.1+
        /// </summary>

        if (!Modernizr.localstorage) {
            return;
        }

        this._storage;
        return this._initialize();
    };

    LocalStorage.prototype._initialize = function () {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        this.hasSupport = this._hasSupport();
        this._storage = window.localStorage;

        return this;
    }

    LocalStorage.prototype.get = function (key, callback) {
        var that = this,
            result;
        result = this._storage[key];
        if (result) {
            try {
                egov.callback(callback, JSON.parse(result));
            } catch (e) {
                var resultNew = JSON.parse(window.hashBase64.fromBase64(result));
                if (resultNew.value == null || resultNew.value == undefined) {
                    egov.locache.get(key, function (value) {
                        resultNew.value = value;
                        egov.locache.update(key, resultNew);
                        egov.callback(callback, resultNew);
                    });
                }
                else {
                    egov.callback(callback, resultNew);
                }
            }
        } else {
            egov.callback(callback);
        }
    }

    LocalStorage.prototype.insert = function (key, value, callback) {
        this._storage.setItem(key, JSON.stringify({ key: key, value: value, dateModified: new Date() }));
        egov.callback(callback, value);
    }

    LocalStorage.prototype.update = function (key, value, callback) {
        this._storage.setItem(key, JSON.stringify({ key: key, value: value, dateModified: new Date() }));

        egov.callback(callback, value);
    }

    LocalStorage.prototype.delete = function (key, callback) {
        if (this._storage[key]) {
            this._storage.removeItem(key);
        }

        egov.callback(callback, key);
    }

    LocalStorage.prototype.reset = function (callback) {
        this._storage.clear();

        egov.callback(callback);
    }

    LocalStorage.prototype._hasSupport = function () {
        if ('localStorage' in window && window['localStorage'] !== null) {
            //Kiểm tra trình duyêt có cho phép thao tác với localStorage
            try {
                window.localStorage.setItem('egov', 'Bkav Corporation');
                window.localStorage.removeItem('egov');
                return true;
            }
            catch (ex) {
                return false;
            }
        } else {
            return false;
        }
    }

    //#endregion

    //#region IndexedDb

    //Các kiểu giao tiếp với indexedDb
    var indexedDbMode = {
        READ_WRITE: "readwrite",
        READ_ONLY: "readonly",
    };

    var IndexedDb = function () {
        /// <summary>
        /// Lớp quản lý dữ liệu trong localStorage.
        /// indexedDb: 
        ///     At least 20MB
        ///     No Expire Date
        ///     Firefox 10+
        ///     Chrome 23+
        ///     Opera 15+
        ///     Android 4.4+
        ///     IE 10+
        /// </summary>
        var request,
            that;

        if (!Modernizr.indexeddb) {
            return;
        }

        that = this;
        that._storage;
        that._storageName = "eGovStore";
        that._tableName = "eGovObjectStore";
        that.version = 1;
        that.hasSupport = true;    //Biến kiểm tra có hỗ trợ hay không

        return that._initialize();

        // HopCV:
        // todo: Chỗ này quá dư thừa, đã mở db lên rồi trong hàm init lại mở tiếp
        //       mà mục đích chỗ này check xem trình duyệt có hỗ trợ hay không khi mở kết db lên
        ////Open trước indexDb, nếu gặp lỗi sẽ xác định trình duyệt không support(như private mode in firefox)
        //request = window.indexedDB.open(that._storageName, 1);
        //request.onsuccess = function (event) {
        //    return that._initialize();
        //};

        //request.onerror = function () {
        //    that.isNotSupportIndexedDb = true;
        //}
    };

    IndexedDb.prototype._initialize = function () {
        /// <summary>
        /// Khởi tạo
        /// </summary>

        var request,
            db,
            that = this;

        request = window.indexedDB.open(that._storageName, that.version);

        request.onerror = function (event) {
            that.hasSupport = false;
            console.log("Database error: " + event.target.errorCode);
        };

        request.onupgradeneeded = function (event) {
            db = event.target.result;
            db.createObjectStore(that._tableName, { keyPath: "key" });
        }

        request.onsuccess = function (event) {
            that._storage = request.result;
        };

        return this;
    }

    IndexedDb.prototype.get = function (key, callback) {
        var objectStore,
            request,
            that = this;

        objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.get(key);

        request.onerror = function () {
            console.log("Database error: " + this.error.message);
        };

        request.onsuccess = function (e) {
            if (!request.result) {
                //console.log("Key không tồn tại: " + key);
            }
            egov.callback(callback, request.result);
        };
    }

    IndexedDb.prototype.insert = function (key, value, callback) {
        var objectStore,
            request,
            that = this;

        objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.add({ "key": key, "value": value, "dateModified": new Date() });

        request.onsuccess = function () {
            console.log("Add success: " + key);
            egov.callback(callback, value);
        };

        request.onerror = function () {
            console.log("Add error! " + this.error.message);
        }
    }

    IndexedDb.prototype.update = function (key, value, callback) {
        var objectStore,
            request,
            that = this;

        objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.get(key);

        request.onsuccess = function () {
            if (request.result) {
                request.result.value = value;
                request.result.dateModified = new Date();
                objectStore.put(request.result);
            }

            console.log("Update success." + key);
            egov.callback(callback, value);
        };

        request.onerror = function () {
            console.log("Update error! " + this.error.message);
        }
    }

    IndexedDb.prototype.delete = function (key, callback) {
        var objectStore,
            request;

        objectStore = this._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.delete(key);

        request.onerror = function () {
            console.log("delete error! " + this.error.message);
        }

        request.onsuccess = function () {
            console.log("delete success.");
            egov.callback(callback);
        };
    }

    IndexedDb.prototype.reset = function (callback) {
        var objectStore,
            request;

        objectStore = this._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.clear();

        request.onerror = function () {
            console.log("reset error! " + this.error.message);
        }

        request.onsuccess = function () {
            console.log("Reset success.");
            egov.callback(callback);
        };
    }

    IndexedDb.prototype._getObjectStore = function (mode) {
        /// <summary>
        /// Trả về store object trong indexeddb
        /// </summary>
        /// <param name="mode">Chế độ đọc "readwrite" hoặc "readonly"</param>
        /// <returns type="objectStore">objectStore</returns>
        mode = mode ? mode : indexedDbMode.READ_WRITE;
        var transaction, storage;

        if (this._storage == null) {
            console.log("Cache not loaded.");
            window.location.reload();
            return;
        }

        transaction = this._storage.transaction([this._tableName], mode);
        return transaction.objectStore(this._tableName);
    }

    //#endregion

    //#region File System

    //#endregion

    //#region Locache

    var Locache = function (cacheName) {
        /// <summary>
        /// Local Cache: Xử lý cache của hệ thống.
        /// Chỉ lưu những thông tin không quan trọng, được dùng thường xuyên dưới client để giảm tải cho server.
        /// </summary>
        /// <param name="cacheName" type="String">
        ///     - Giá trị chỉ định sử dụng cache nào: "storage","indexedDb", "fileApi".
        ///     - Để null: tự động chỉ định cache theo trình duyệt.
        /// </param>
        this._localStorage = Modernizr.localstorage;
        this._indexeddb = Modernizr.indexeddb;
        this._fileReader = Modernizr.filereader;

        // giá trị xác định có hỗ trợ Cache hay không.
        this._hasSupportCache;

        // cache data
        this._storage;

        // Giá trị xác định dung lượng cache client.
        this._cacheSize;
        this._cacheName = cacheName;

        return this._initialize();
    };

    Locache.prototype._initialize = function () {

        /// <summary>
        /// Thiết lập chế độ cache client
        /// </summary>

        // Kiểm tra trình duyệt hỗ trợ lưu cache
        this._hasSupportCache = this._localStorage || this._indexeddb || this._fileReader;
        if (!this._hasSupportCache) {
            return new TempStorage();
            return;
        }
        if (!this._fileReader && this._cacheName == "fileApi") {
            this._cacheName = "indexedDb";
        }
        else if (!this._indexeddb && this._cacheName == "indexedDb") {
            this._cacheName = "storage";
        }
        if (this._cacheName) {
            switch (this._cacheName) {
                case "storage":
                    return new LocalStorage();
                case "indexedDb":
                    return new IndexedDb();
                case "fileApi":
                    return; // undefined
                default:
                    console.log("Giá trị khởi tạo không đúng");
                    return; // undefined
            }
        }

        if (this._indexeddb) {
            return new IndexedDb();
        }
        else if (this._localStorage) {
            return new LocalStorage();
        }
        else if (this._fileReader) {
            return;  // undefined
        }

        return this;
    }

    Locache.prototype.get = function (key, callback) {
        /// <summary>
        /// Trả về giá trị theo key
        /// </summary>
        /// <param name="key">Key cần lấy giá trị</param>
        /// <param name="callback">Hàm thực thi sau khi lấy giá trị thành công</param>
        /// <returns type="Object">Json object: {key: "", value: "", dateModified: ""}</returns>
        egov.callback(callback);
    }

    Locache.prototype.insert = function (key, value, callback) {
        /// <summary>
        /// Thêm mới giá trị vào cache
        /// </summary>
        /// <param name="key">Tên giá trị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="callback">Hàm thực thi sau thêm giá trị thành công</param>

        console.log('Locache chưa được khởi tạo');
    }

    Locache.prototype.update = function (key, value, callback) {
        /// <summary>
        /// Cập nhật giá trị vào cache
        /// </summary>
        /// <param name="key">Tên giá trị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="callback">Hàm thực thi sau thêm giá trị thành công</param>
        console.log('Locache chưa được khởi tạo');
    }

    Locache.prototype.delete = function (key, callback) {
        /// <summary>
        /// Xóa giá trị khỏi cache theo key
        /// </summary>
        /// <param name="key">Tên key</param>
        /// <param name="callback">Hàm thực thi sau xóa giá trị thành công</param>

        console.log('Locache chưa được khởi tạo');
    }

    Locache.prototype.reset = function (callback) {
        /// <summary>
        /// Xóa tất cả dữ liệu trong cache
        /// </summary>
        /// <param name="callback">Hàm thực thi sau xóa dữ liệu thành công</param>

        console.log('Locache chưa được khởi tạo');
    }

    //#endregion

    egov.CacheManager = Locache;

})(this, this.egov = this.egov || {}, this.Modernizr);