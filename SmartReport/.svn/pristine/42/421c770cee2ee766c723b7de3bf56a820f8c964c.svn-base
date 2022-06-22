/// <reference path="../views/home/contextmenu.js" />
(function (egov, _, undefined) {
    "use strict";

    var cacheType = {
        indexedDb: "indexedDb",  //Lưu trữ vào indexDB
        storage: "storage",     // Lưu trữ localStorage
        fileApi: "fileApi"      //Lưu trữ vào file
    }

    var DataAccess = function () {
        /// <summary>
        /// Lớp cung cấp dữ liệu cho Business:
        ///    - Quản lý lưu cache dữ liệu.
        ///    - Quản lý dữ liệu ở client.
        ///    - Quản lý đồng bộ dữ liệu với server;
        /// </summary>

        this._cacheManager = new egov.CacheManager(cacheType.storage);
        if (egov.isMobile || this._cacheManager == undefined || !this._cacheManager.hasSupport) {
            this._cacheManager = new egov.CacheManager(cacheType.storage);
        }

        this._dataBase = {};
        this._hasCache = (this._cacheManager !== undefined && this._cacheManager.hasSupport);
        this._expriedDay = 10;

        return this;
    }

    DataAccess.prototype.get = function (entity) {
        /// <summary>
        /// Trả về giá trị theo key
        /// </summary>
        /// <param name="entity">egov.entity cần lấy giá trị</param>
        var result,
            requestOptions,
            that = this,
            callback,
            key = egov.getKeyName(entity),
            keyInDb;

        if (entity == undefined) {
            egov.log("DataAccess.get: Entity is null.");
            return;
        }

        callback = entity.option.success;

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        // Trường hợp ko lưu cache lại dưới client thì query lên lấy dữ liệu trên server
        //Trường hợp trình duyệt không mở được indexedDb(private mode firefox) thì gọi server luôn
        if (!entity.hasSessionCache) {
            that._sendRequestToServer(entity);
            return;
        }
        //Gán success để kiểm tra dữ liệu outDate chưa
        entity.option.success = function (hasReset) {
            //Sau khi kiểm tra, gán lại success ban đầu
            entity.option.success = callback;

            //Nếu reset hoặc chưa có dữ liệu => gọi lên server luôn
            if (hasReset) {
                that._sendRequestToServer(entity);
                return;
            }

            key = egov.getKeyName(entity)

            // Nếu không reset
            // Lấy trong database client trước
            result = that._dataBase[key];
            if (result) {
                //Nếu entity chỉ dành cho currentUser, người khác đăng nhập trên máy đó sẽ bị xóa đi
                if (entity.isInsertToCurrentCache) {
                    keyInDb = JSON.stringify(entity.option.data);

                    result = result.filter(function (e) {
                        return e.key == keyInDb;
                    });

                    if (result.length > 0) {
                        egov.callback(callback, result[0].value);
                        return;
                    }
                } else {
                    egov.callback(callback, result);
                    return;
                }
            }

            // Dữ liệu chưa tồn tại ở client
            // Kiểm tra nếu có cache thì get từ cache
            that._getFromCache(entity, function (data) {
                if (data) {
                    result = data.value;
                    //Nếu entity chỉ dành cho currentUser, người khác đăng nhập trên máy đó data sẽ bị xóa đi
                    if (entity.isInsertToCurrentCache) {
                        keyInDb = JSON.stringify(entity.option.data);

                        result = result.filter(function (e) {
                            return e.key == keyInDb;
                        });

                        if (result.length > 0) {
                            egov.callback(callback, result[0].value);
                            return;
                        }
                    } else {
                        that._dataBase[key] = result;
                        egov.callback(callback, result);
                        return;
                    }
                }

                // Nếu dữ liệu vẫn chưa có trong cache (hoặc không hỗ trợ Cache) thì query lên server để lấy về
                if (_.isFunction(entity.request)) {
                    that._sendRequestToServer(entity);
                }
                else {
                    egov.callback(callback, undefined);
                }
            });
        };

        this.resetOutofDate(entity);
    },

    DataAccess.prototype.getCache = function (entity) {
        /// <summary>
        /// HopCV
        /// Todo: Hàm này mục đích chỉ lấy dữ liệu ở client ra thôi, không giao tiếp j với server
        /// Trả về giá trị theo key
        /// </summary>
        /// <param name="entity">egov.entity cần lấy giá trị</param>
        var result,
            that = this,
            callback,
            key,
            keyInDb;
        if (entity == undefined) {
            egov.log("DataAccess.get: Entity is null.");
            return;
        }

        key = egov.getKeyName(entity),
        callback = entity.option.success;
        this._cacheManager.get(key, function (data) {
            if (data) {
                result = data.value;
                //Nếu entity chỉ dành cho currentUser, người khác đăng nhập trên máy đó data sẽ bị xóa đi
                if (entity.isInsertToCurrentCache) {
                    keyInDb = JSON.stringify(entity.option.data);

                    result = result.filter(function (e) {
                        return e.key == keyInDb;
                    });

                    if (result.length > 0) {
                        egov.callback(callback, result[0].value);
                        return;
                    }
                } else {
                    that._dataBase[key] = result;
                    egov.callback(callback, result);
                    return;
                }
            }
            egov.callback(callback, undefined);
        });
    },

    DataAccess.prototype.resetOutofDate = function (entity) {
        /// <summary>
        /// Kiểm tra nếu entity đã lâu không cập nhật thì xóa đi, khoảng thời gian được báo bởi biến this._expriedDay
        /// </summary>
        /// <param name="entity"></param>
        var that, key, callback;

        that = this;
        key = egov.getKeyName(entity);
        callback = entity.option.success;

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        entity.option.success = function (lastUpdate) {
            //Kiểm tra lastUpdate đã quá experiedDay => Xóa hết dữ liệu trong cache
            if (lastUpdate && (Date.now() - lastUpdate > that._expriedDay * 86400000)) {
                that._dataBase[key] = null;
                that.delete(key, function () {
                    egov.callback(callback, true);
                });
                return;
            }

            //Nếu lastUpdate == undefined => chưa từng có cache => trả về true coi như đã reset để gọi lên server luôn
            egov.callback(callback, lastUpdate ? false : true);
        };

        this.getLastUpdate(entity);
    };

    DataAccess.prototype.getLastUpdate = function (entity) {
        /// <summary>
        /// Trả về thời điểm đồng bộ gần nhất với server.
        /// </summary>
        var result,
            callback;

        if (entity == undefined) {
            egov.log("DataAccess.getLastUpdate: Entity is null.");
            return;
        }

        callback = entity.option.success;

        // Trường hợp ko lưu cache lại dưới client
        if (!entity.hasSessionCache) {
            return;
        }

        // Kiểm tra nếu có cache thì get từ cache
        this._getFromCache(entity, function (data) {
            egov.callback(callback, data ? data.dateModified : undefined);
        });
    }

    DataAccess.prototype.insert = function (entity, value, property) {
        /// <summary>
        /// Thêm giá trị vào cache
        /// </summary>
        /// <param name="key">Tên giá trị</param>
        /// <param name="value">Giá trị</param>

        var key,
            callback,
            cacheValue,
            that;

        that = this;
        key = egov.getKeyName(entity);
        callback = entity.option.success;

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        that._dataBase[key] = value;
        if (entity.hasCache && that._hasCache) {
            that._cacheManager.insert(key, value, callback);
        }
        else {
            egov.callback(callback, value);
        }
    }

    DataAccess.prototype.update = function (entity, value, isReplace) {
        /// <summary>
        /// Cập giá trị vào cache
        /// </summary>
        /// <param name="key">Tên giá trị</param>
        /// <param name="isReplace">Giá trị xác định có replace toàn bộ dữ liệu cũ hay không</param>
        /// <param name="value">Giá trị</param>

        var cacheValue,
            key,
            that,
            result,
            callback,
            keyProperty;

        that = this;
        key = egov.getKeyName(entity);
        isReplace = isReplace || false;
        keyProperty = "id";
        callback = entity.option.success;

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        cacheValue = that._dataBase[key] || [];
        if (!isReplace && (value === undefined || value.length === 0)) {
            egov.callback(callback, cacheValue);
            return;
        }

        result = isReplace ? value : egov.mergeArraysByProperty(cacheValue, value, keyProperty);

        that._dataBase[key] = result;
        if (entity.hasCache && that._hasCache) {
            that._cacheManager.update(key, result, callback);
        }
        else {
            egov.callback(callback, result);
        }
    }

    DataAccess.prototype.delete = function (key, callback) {
        /// <summary>
        /// Xóa giá trị khỏi cache theo key
        /// </summary>
        /// <param name="key">Tên key</param>
        /// <param name="callback">Hàm thực thi sau xóa giá trị thành công</param>

        var entity,
            that = this;

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        entity = egov.entities[key];
        if (entity === undefined) {
            egov.log("DataAccess.delete: entity not found");
            return;
        }

        key = egov.getKeyName(entity);
        this._dataBase[key] = null;
        if (entity.hasCache && that._hasCache) {
            this._cacheManager.delete(key, callback);
        } else {
            egov.callback(callback, key);
        }
    }

    DataAccess.prototype.reset = function (callback) {
        /// <summary>
        /// Xóa tất cả dữ liệu trong cache
        /// </summary>
        /// <param name="callback">Hàm thực thi sau xóa dữ liệu thành công</param>
        this._dataBase = {};

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        if (this._hasCache) {
            this._cacheManager.reset(callback);
        }
        else {
            egov.callback(callback);
        }
    }

    DataAccess.prototype._saveToCache = function (key, value) {
        /// <summary>
        /// Lưu cache client
        /// </summary>
        /// <param name="key" type="String">Key</param>
        /// <param name="value" type="dynamic">Giá trị</param>

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        this._cacheManager.insert(key, value);
    }

    DataAccess.prototype._getFromCache = function (entity, callback) {
        /// <summary>
        /// Trả về giá trị từ cache
        /// </summary>
        /// <param name="key">Tên key cần lấy giá trị</param>

        //HopCV
        //toddo; Chỗ này check làm j nhi?
        // Cái set có hỗ trợ trên trình duyệt hay không thì chỉ cần gọi trên thằng khỏi tạo là được??
        //that._checkBrowsersNotSupport();

        if (this._hasCache && entity.hasCache) {
            this._cacheManager.get(egov.getKeyName(entity), callback);
        }
        else {
            egov.callback(callback, undefined);
        }
    }

    DataAccess.prototype._sendRequestToServer = function (entity) {
        /// <summary>
        /// Gửi request lên server
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="callback"></param>
        var requestOptions,
            that = this,
            key,
            existDb,
            keyInDb,
            flag,
            callback = entity.option.success;

        key = egov.getKeyName(entity);
        requestOptions = $.extend({ cache: true }, entity.option);
        requestOptions.success = function (result) {
            //Kiểm tra xem entity query có option insert vào current cache hay không
            //True: insert bản ghi mới query vào db dưới client
            //False: thêm mới
            if (entity.isInsertToCurrentCache) {
                that._getFromCache(entity, function (dataInDb) {
                    if (dataInDb) {
                        existDb = dataInDb.value;
                        flag = true;
                    }
                    keyInDb = JSON.stringify(entity.option.data);

                    if (!existDb) {
                        //nếu chưa tồn tại dataBase, thêm mới
                        existDb = [{ key: keyInDb, value: result }];
                    } else {
                        existDb.push({ key: keyInDb, value: result });
                    }

                    that._dataBase[key] = existDb;

                    // Save to cache
                    if (entity.hasCache) {
                        //override lại hàm success của entity để khi gọi insert hoặc update sẽ tự động callback
                        entity.option.success = function () {
                            callback(result);
                        }
                        if (flag) {
                            that.update(entity, existDb, true);
                        } else {
                            that.insert(entity, existDb);
                        }
                    }
                    return;
                });
            } else {
                that._dataBase[key] = result;

                //override lại hàm success của entity để khi gọi insert sẽ tự động callback
                //success mới sẽ = hàm cũ kèm thêm giá trị trả về.
                entity.option.success = function () {
                    egov.callback(callback, result);
                }

                // Save to cache
                if (entity.hasCache) {
                    that.insert(entity, result);
                } else {
                    //Nếu entity ko lưu vào cache, callback trả về giá trị
                    egov.callback(entity.option.success);
                }
            }
        }
        requestOptions.error = function (xhr) {
            //Nếu response error có header IsAuthenticated=false thì logout
            if (xhr.getResponseHeader("IsAuthenticated") === "false") {
                parent.logout();
            }
        }
        entity.request(requestOptions);
    }

    DataAccess.prototype._checkBrowsersNotSupport = function () {
        if (this._cacheManager.isNotSupportIndexedDb) {
            this._hasCache = false;
        }
    }

    DataAccess.prototype.version = function (callback) {
        //this._cacheManager = new egov.CacheManager(cacheType.storage);
        //this._cacheManager.get("version", function (data) {
        //    if (data) {
        //        var result = data.value;
        //        callback(result);
        //    } else {
        //        callback(false);
        //    }
        //});
    }

    DataAccess.prototype.setVersion = function (value) {
        //this._cacheManager = new egov.CacheManager(cacheType.storage);
        //this._cacheManager.insert("version", value);
    }

    egov.DataAccess = new DataAccess();
})
(this.egov = this.egov || {}, _, undefined);