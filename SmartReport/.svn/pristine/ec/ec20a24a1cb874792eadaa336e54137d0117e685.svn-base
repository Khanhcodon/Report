(function (window, $, egov, hashBase64) {
    if (typeof $ === 'undefined') {
        throw 'Chưa có thư viện jquery!.';
    }

    //if (typeof hashBase64 === 'undefined') {
    //    throw 'Chưa có thư viện mã hóa';
    //}

    function _hasSupportLocalStorage() {
        /// <summary>
        /// Kiểm tra trình duyệt có hỗ trợ localStorage hay không?
        /// </summary>
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

    var Locache = function () {
        /// <summary>
        /// Lớp database lưu các dữ liệu dùng chung cho hệ thống.
        /// </summary>
        this.hasSupportLocalStorage = _hasSupportLocalStorage();
        if (this.hasSupportLocalStorage) {
            this.database = window['localStorage'];
        } else {
            console.log("not support localStorage");
            this.database = {};
        }

        this.init();
    };

    Locache.prototype.init = function () {
        /// <summary>
        /// Khởi tạo dữ liệu
        /// </summary>
        /// <summary>
        /// Gán các dữ liệu lưu cache ở client;
        /// </summary>

        // Nghiệp vụ xử lý
        if (!this.hasKey('categoryBusiness')) {
            this.set('categoryBusiness', {
                vbden: 1,
                vbdi: 2,
                hsmc: 4
            });
        }

        // Độ khẩn
        if (!this.hasKey('urgents')) {
            this.set('urgents',
                [
                    { name: "Thường", value: 1 },
                    { name: "Khẩn", value: 2 },
                    { name: "Hỏa tốc", value: 3 }
                ]
            );
        }

        // Độ mật
        if (!this.hasKey('securityLevel')) {
            this.set('securityLevel', [
                { name: "Thường", value: 1 },
                { name: "Mật", value: 2 },
                { name: "Tối mật", value: 3 }
            ]);
        }

        // Kiểu bàn giao
        if (!this.hasKey('transferType')) {
            this.set('transferType', {
                xulychinh: 1,
                dongxuly: 2,
                thongbao: 3
            });
        }

        // Các loại bàn giao
        if (!this.hasKey('documentTransferType')) {
            this.set('documentTransferType', {
                // 1 là khi tạo mới thông thường,
                taoMoiThongThuong: 1,
                // 2 là bàn giao thông thường, 
                banGiaoThongThuong: 2,
                // 4 là bàn giao khi trả lời, 
                banGiaoKhiTraLoi: 4,
                // 8 là bàn giao khi phân loại
                banGiaoKhiPhanLoai: 8
            });
        }

        // Các kiểu xem kích cỡ danh sách văn bản
        if (!this.hasKey('documentListSize')) {
            this.set('documentListSize', {
                small: 0, // chế độ xem nhỏ nhất: padding: 2
                medium: 1, // Chế độ xem bình thường: padding: 5
                large: 2 // chế độ xem lớn: padding: 8 (default)
            });
        }
        // Danh sách các loại hồ sơ cán bộ hiện tại được phép khởi tạo.
        if (!this.hasKey('currentDoctypes')) {
            this.setQuery('currentDoctypes', '/Doctype/GetDoctypes');
        }

        // Thể loại văn bản.
        if (!this.hasKey('categories')) {
            this.setQuery('categories', '/Common/GetCategories');
        }

        // Danh sách các phòng ban người dùng hiện tại thuộc vào.
        if (!this.hasKey('currentDepartments')) {
            this.setQuery('currentDepartments', '/Common/GetDepartmentsByUser');
        }

        // Danh sách tất cả các phòng ban hệ thống.
        if (!this.hasKey('allDept')) {
            this.setQuery('allDept', '/Common/GetAllDepartment');
        }

        // Danh sách tất cả các chức danh hệ thống.
        if (!this.hasKey('allJobtitle')) {
            this.setQuery('allJobtitle', '/Common/GetAllJobTitlies');
        }
        // Danh sách tất cả các chức vụ hệ thống.
        if (!this.hasKey('allPosition')) {
            this.setQuery('allPosition', '/Common/GetAllPosition');
        }
        // Danh sách tất cả các user hệ thống.
        if (!this.hasKey('allUsers')) {
            this.setQuery('allUsers', '/Common/GetAllUsers');
        }

        // Danh sách tất cả các quan hệ user - dept - position hệ thống.
        if (!this.hasKey('allUserDeptPosition')) {
            this.setQuery('allUserDeptPosition', '/Common/GetAllUserDepartmentJobTitlesPosition');
        }

        // Danh sách tất cả các cơ quan bên ngoài.
        if (!this.hasKey('allAddress')) {
            this.setQuery('allAddress', '/Common/GetAllAddress');
        }

        // Danh sách tất cả các từ khóa hệ thống.
        if (!this.hasKey('allKeyword')) {
            this.setQuery('allKeyword', '/Common/GetKeywords');
        }

        // Danh sách hinh thuc.
        if (!this.hasKey('allDocField')) {
            this.setQuery('allDocField', '/Common/GetDocField');
        }

        // Danh sách linh vuc văn bản đến
        if (!this.hasKey('getDocfieldsVbDen')) {
            this.setQuery('getDocfieldsVbDen', '/Document/GetDocfieldsVbDen');
        }

        //Danh sách list hình thức gửi
        if (!this.hasKey('allSendType')) {
            this.setQuery('allSendType', '/Common/GetSendTypes');
        }

        // Danh sách các Comment thường dùng
        //if (!this.hasKey('allComments')) {
        //    this.setQuery('allComments', '/Document/GetCommonComments');
        //}

        if (!this.hasKey('getDocumentByNode')) {
            this.setQuery('getDocumentByNode', '/Document/GetCommonComments');
        }

        return this;
    },

    Locache.prototype.hasKey = function (key) {
        /// <summary>
        /// Kiểm tra key có tồn tại hay chưa
        /// </summary>
        /// <param name="key">Tên key của dữ liệu</param>
        return this.database[key] ? true : false;
    }

    Locache.prototype.set = function (key, value) {
        /// <summary>
        /// Set thêm dữ liệu cố định
        /// </summary>
        /// <param name="key">Tên key của dữ liệu</param>
        /// <param name="value">Dữ liệu</param>
        if (this.hasSupportLocalStorage) {
            var obj = {
                query: '',
                isQuery: false,
                value: value
            }
            var jsonBase64 = window.hashBase64.toBase64(JSON.stringify(obj));
            this.database.setItem(key, jsonBase64);
        }
        else {
            this.database[key] = {
                query: '',
                isQuery: false,
                value: value
            };
        }
    },

    Locache.prototype.setDefault = function (key, value, hash) {
        /// <summary>
        /// Set thêm dữ liệu cố định
        /// </summary>
        /// <param name="key" type="string">Tên key của dữ liệu</param>
        /// <param name="value" type="string>Dữ liệu</param>
        /// <param name="hash" type="bool>Dữ liệu</param>
        if (hash && value != null) {
            value = window.hashBase64.toBase64(value);
        }

        if (this.hasSupportLocalStorage) {
            this.database.setItem(key, value);
        }
        else {
            this.database[key] = value;
        }
    },

    Locache.prototype.setQuery = function (key, query, params, value) {
        /// <summary>
        /// Set dữ liệu lấy từ server.
        /// </summary>
        /// <param name="key">Tên dữ liệu</param>
        /// <param name="query">Url lấy dữ liệu</param>
        /// <param name="params">Param cho url</param>
        /// <param name="value">Giá trị set cho key</param>
        if (params === undefined) {
            params = null;
        }

        if (this.hasSupportLocalStorage) {
            var obj = {
                query: query,
                params: params,
                isQuery: true,
                value: value
            };

            var jsonBase64 = window.hashBase64.toBase64(JSON.stringify(obj));
            this.database.setItem(key, jsonBase64);
        }
        else {
            this.database[key] = {
                query: query,
                params: params,
                isQuery: true,
                value: value
            };
        }
    }

    Locache.prototype.get = function (key, callback) {
        /// <summary>
        /// Trả về dữ liệu theo tên
        /// </summary>
        /// <param name="key">Tên dữ liệu</param>
        /// <param name="callback">Hàm thực thi sau khi tải xong dữ liệu nếu có (sử dụng khi dữ liệu dạng query).</param>
        /// <returns type="json object">Dữ liệu theo tên yêu cầu</returns>
        var _this = this;
        var cache = this.database[key];
        if (cache) {
            if (this.hasSupportLocalStorage) {
                cache = JSON.parse(window.hashBase64.fromBase64(cache));
            }

            if (cache.isQuery && (cache.value === undefined || cache.value == null)) {
                $.get(cache.query, cache.params)
                .done(function (result) {
                    if (result.error) {
                        return null;
                    }
                    if (typeof result === 'string') {
                        cache.value = JSON.parse(result);
                    }
                    else {
                        cache.value = result;
                    }
                    ///Gán lại giá trị cho phần tử có key là key
                    _this.setQuery(key, cache.query, cache.params, cache.value);

                    if (typeof callback === 'function') {
                        callback(cache.value);
                    }
                    else {
                        return cache.value;
                    }
                });
            }
            else {
                var value = cache.value ? cache.value : null;
                if (typeof callback === 'function') {
                    callback(value);
                }
                else {
                    return value;
                }
            }
        }
        else {
            if (typeof callback === 'function') {
                callback(value);
            }
            else {
                return null;
            }
            //alert('Không lấy được dữ liệu!.Vui lòng load lại trang.');
            ///Todo: chưa rõ nếu không tồn tại thì lên làm j,nến tam thời reload lại trang để lấy key rồi get lại
            //window.location.reload(true);
        }
    }

    Locache.prototype.getDefault = function (key, deHash, callback) {
        /// <summary>
        /// Trả về dữ liệu theo tên
        /// </summary>
        /// <param name="key">Tên dữ liệu</param>
        /// <param name="deHash">Có giải mã không</param>
        /// <param name="callback">Hàm thực thi sau khi tải xong dữ liệu nếu có (sử dụng khi dữ liệu dạng query).</param>
        /// <returns type="json object">Dữ liệu theo tên yêu cầu</returns>
        var _this = this;
        var cache = this.database[key];
        if (cache) {
            if (deHash) {
                cache = window.hashBase64.fromBase64(cache);
            }
            if (typeof callback === 'function') {
                callback(cache);
            }
            else {
                return cache;
            }
        }
        else {
            if (typeof callback === 'function') {
                callback(cache);
            }
            else {
                return null;
            }
        }
    }

    Locache.prototype.gets = function (keys, callback) {
        /// <summary>
        /// Lấy dữ liệu của 1 loạt các key
        /// </summary>
        /// <param name="keys" type="Array">Mảng các key</param>
        /// <param name="callback">Hàm thực thi sau khi tải xong dữ liệu.</param>
        var that = this;
        if (keys instanceof Array) {
            if (keys.length > 0) {
                var key = keys[0];
                this.get(key, function () {
                    keys.shift();
                    that.gets(keys, callback);
                });
            }
            else {
                if (typeof callback === 'function') {
                    callback();
                }
            }
        }
    }

    Locache.prototype.update = function (key, value) {
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="key">Tên key của dữ liệu</param>
        /// <param name="value">Dữ liệu</param>

        var data = this.database[key];
        if (!data)
            return;

        if (this.hasSupportLocalStorage) {
            data = JSON.parse(window.hashBase64.fromBase64(data));
            var obj = {
                query: data.query,
                params: data.params ? data.params : null,
                isQuery: data.isQuery,
                value: value
            }
            var jsonBase64 = window.hashBase64.toBase64(JSON.stringify(obj));
            this.database.setItem(key, jsonBase64);
        }
        else {
            this.database[key] = {
                query: data.query,
                params: data.params ? data.params : null,
                isQuery: data.isQuery,
                value: value
            };
        }
    }

    Locache.prototype.remove = function (key, callback) {
        /// <summary>
        /// Xóa bỏ 1 phần tử
        /// </summary>
        /// <param name="key">key để xóa</param>
        /// <param name="callback">hàm callback sau khi xóa</param>
        if (this.hasSupportLocalStorage) {
            this.database.removeItem(key);
        }
        else {
            delete this.database[key];
        }

        if (typeof callback === 'function') {
            callback();
        }
    }

    Locache.prototype.removes = function (keys, callback) {
        /// <summary>
        /// Xóa bỏ nhiều phần tử trong cache
        /// </summary>
        /// <param name="key">danh sách các key để xóa</param>
        /// <param name="callback">hàm callback</param>
        var _this = this;
        if (keys instanceof Array && keys.length > 0) {
            var leng = keys.length;
            for (var i = 0 ; i < leng; i++) {
                var key = keys[i];
                _this.remove(key);
            }
        }

        if (typeof callback === 'function') {
            callback();
        }
    }

    Locache.prototype.clear = function (callback) {
        /// <summary>
        /// Xóa toàn bộ cache trong hệ thống
        /// </summary>
        /// <param name="callback">hàm callback</param>
        if (this.hasSupportLocalStorage) {
            this.database.clear();
        } else {
            this.database = {};
        }

        if (typeof callback === 'function') {
            callback();
        }
    }

    egov.locache = new Locache();

})(window, window.jQuery, egov = window.egov || {}, window.hashBase64);
