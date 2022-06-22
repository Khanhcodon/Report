(function (egov, _) {

    "use strict";

    var _entities = egov.entities;

    var DataManager = function () {
        /// <summary>
        /// Lớp quản lý truy cập dữ liệu:
        ///     - Xử lý logic dữ liệu cung cấp cho các Widget;
        /// </summary>

        this._dataAccess = egov.DataAccess;

        // Thiết lập ajax option mặc định cho toàn hệ thống
        this._requestDefault = {
            // Bỏ comment để thiết lập mặc định cho các option
            // beforeSent: function () { },
            // success: function (result) { },
            // error: function (xhr) {},
            // complete: function () { }
        };

        // Thiết lập ajax option mặc định cho toàn hệ thống
        this._syncDefault = {
            success: function (entity, result) {

            }

            // Bỏ comment để thiết lập mặc định cho các option
            // beforeSent: function () { },
            // error: function (xhr) { },
            // complete: function () { }
        };

        return this;
    }

    DataManager.prototype.sendRequest = function (request, options) {
        /// <summary>
        /// Gửi request
        /// </summary>
        /// <param name="request">egov.entities request</param>
        /// <param name="options" type="object">jQuery ajax option</param>

        // Override ajax option theo thứ tự: mặc định cho toàn hệ thống <= mặc định cho từng request <= tùy chỉnh cua nguoi su dung.
        request.option = $.extend({}, this._requestDefault, request.option, options);
        this._dataAccess.get(request);
    }

    DataManager.prototype.getCache = function (request, options) {
        /// <summary>
        ///  HopCV 
        /// Lấy dữ liệu từ cache ra (không call lên server)
        /// </summary>
        /// <param name="request">egov.entities request</param>
        /// <param name="options" type="object">jQuery ajax option</param>

        // Override ajax option theo thứ tự: mặc định cho toàn hệ thống <= mặc định cho từng request <= tùy chỉnh cua nguoi su dung.
        request.option = $.extend({}, this._requestDefault, request.option, options);
        this._dataAccess.getCache(request);
    }

    DataManager.prototype.getLastUpdate = function (entity, options) {
        /// <summary>
        /// Trả về thời điểm đồng bộ gần 
        /// </summary>
        /// <param name="entity" type="egov.entities">Entity cần lấy lastupdate tương ứng.</param>
        entity.option = $.extend({}, this._requestDefault, entity.option, options);
        this._dataAccess.getLastUpdate(entity);
    }

    DataManager.prototype.reset = function (options) {
        /// <summary>
        /// Xóa hết tất cả cache hệ thống.
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        this._dataAccess.reset(options ? options.success : undefined);
    }

    DataManager.prototype.deletePrivateCache = function (options) {
        /// <summary>
        /// Xóa hết các cache chỉ người dùng hiện tại mới thao tac được
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        if (typeof _entities === undefined) {
            return;
        }

        var i = 0, arrName = [];
        for (var item in _entities) {
            var obj = _entities[item];
            if (obj && obj.hasCache && obj.isPrivate) {
                arrName.push(obj.name);
            }
        }

        if (arrName.length > 0) {
            var leng = arrName.length;
            for (var j = 0; j < leng; j++) {
                i++;
                this._dataAccess.delete(arrName[j], (i == leng - 1 && options) ? options.success : undefined);
            }
        }
    }

    //#region Sử dụng chung cho toàn hệ thống

    DataManager.prototype.getCurrentDoctypes = function (options) {
        /// <summary>
        /// Trả về danh sách các Loại văn bản người dùng hiện tại được phép khởi tạo
        /// </summary>
        /// <param name="options" type="object">jQuery ajax option</param>

        if (egov && egov.doctypes) {
            egov.callback(options.success, egov.doctypes);
            return;
        }

        var request = _entities.currentDoctypes;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getCategories = function (options) {
        /// <summary>
        /// Trả về danh sách các thể loại văn bản.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request = _entities.categories;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getCurrentDepartments = function (options) {
        /// <summary>
        /// Trả về danh sách các phòng ban hiện tại người dùng thuộc vào.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.currentDepts) {
            egov.callback(options.success, egov.currentDepts);
            return;
        }

        var request = _entities.currentDepartments;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getDepartmentsCurrent = function (options) {
        /// <summary>
        /// Trả về danh sách các phòng ban hiện tại người dùng thuộc vào.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.getDepartmentsCurrent) {
            egov.callback(options.success, egov.getDepartmentsCurrent);
            return;
        }

        var request = _entities.getDepartmentsCurrent;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getAllDept = function (options) {
        /// <summary>
        /// Trả về danh sách các tất cả phòng ban của hệ thống.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.allDeps) {
            egov.callback(options.success, egov.allDeps);
            return;
        }

        var request = _entities.allDept;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getAllJobtitle = function (options) {
        /// <summary>
        /// Trả về danh sách các tất cả chức danh của hệ thống.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.allJobtitles) {
            egov.callback(options.success, egov.allJobtitles);
            return;
        }

        var request = _entities.allJobtitle;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getAllPosition = function (options) {
        /// <summary>
        /// Trả về danh sách các tất cả chức vụ của hệ thống.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.allPositions) {
            egov.callback(options.success, egov.allPositions);
            return;
        }

        var request = _entities.allPosition;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getAllUsers = function (options) {
        /// <summary>
        /// Trả về danh sách các tất cả người dùng trong hệ thống.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.allUsers) {
            egov.callback(options.success, egov.allUsers);
            return;
        }

        var request = _entities.allUsers;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getAllUserDeptPosition = function (options) {
        /// <summary>
        /// Trả về danh sách các tất cả quan hệ người dùng - phòng ban - chức vụ.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        if (egov && egov.allUserDeps) {
            egov.callback(options.success, egov.allUserDeps);
            return;
        }

        var request = _entities.allUserDeptPosition;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getAllAddress = function (options) {
        /// <summary>
        /// Trả về danh sách các địa chỉ.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request = _entities.allAddress;
        this.sendRequest(request, options);
    }

    DataManager.prototype.clearAddress = function () {
        /// <summary>
        /// Clear danh sách địac hỉ
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>
        this._dataAccess.delete("allAddress", function () {

        });
    }

    DataManager.prototype.getSendTypes = function (options) {
        /// <summary>
        /// Trả về danh sách allSendType
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request = _entities.allSendType;
        this.sendRequest(request, options);
    }

    //#endregion

    //#region Current User

    DataManager.prototype.getCommonConfigs = function (options) {
        var request, callback;

        request = _entities.getCommonConfigs;
        callback = options.success;
        options.success = function (result) {
            // Xử lý dữ liệu trước khi trả về cho Widget
            callback(result);
        }
        this.sendRequest(request, options);
    },

    DataManager.prototype.deletePrivateData = function (options) {
        /// <summary>
        /// Trả về danh sách thiết lập của user.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request, callback;

        request = _entities.currentUserId;
        callback = options.success;
        options.success = function (result) {
            // Xử lý dữ liệu trước khi trả về cho Widget
            callback(result);
        }
        this.sendRequest(request, options);
    },

    DataManager.prototype.getUserConfig = function (options) {
        /// <summary>
        /// Trả về danh sách thiết lập của user.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request, callback;

        request = _entities.userConfig;
        callback = options.success;
        options.success = function (result) {
            // Xử lý dữ liệu trước khi trả về cho Widget

            //Lưu lại giá trị quickview vào cookie
            egov.cookie.setQuickView(result.userSetting.quickView);
            egov.cookie.viewSize(result.userSetting.fontSize);

            egov.cookie.setMudimMethod(result.userSetting.MudimMethod);

            result.acceptFileTypes = new RegExp(result.acceptFileTypes, 'i');

            callback(result);
        }
        this.sendRequest(request, options);
    },

    DataManager.prototype.setPopUpSize = function (width, height, options) {
        /// <summary>
        /// Trả về danh sách thiết lập của user.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request, callback;

        request = _entities.setPopUpSize;
        request.option.data = { width: width, height: height };
        options = request.option;
        options.success = function (result) {
            if (result.result === "success") {
                window.opener.egov.setting.userSetting.PopUpHeight = height;
                window.opener.egov.setting.userSetting.PopUpWidth = width;
            }
        }
        this.sendRequest(request, options);
    },

    //#endregion

    egov.dataManager = new DataManager();
})
(this.egov = this.egov || {}, _);