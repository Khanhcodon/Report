(function (egov) {

    "use strict";

    var _entities, DataManager, viewModel;

    if (egov.dataManager === undefined) {
        egov.log("Chưa khởi tạo data manager.");
        return;
    }

    if (egov.viewModels.documentList === undefined) {
        egov.log("Chưa khởi tạo models.");
        return;
    }
    viewModel = egov.viewModels.documentList;
    _entities = egov.entities;
    DataManager = egov.dataManager;
    DataManager._defaultSortBy = "DateReceived";

    DataManager.getDocuments2 = function (id, filters, options) {
        /// <summary>
        /// Trả về danh sách văn bản theo kho và các bộ lọc
        /// </summary>
        /// <param name="id">Id kho văn bản.</param>
        /// <param name=" filters">Các bộ lọc của node tương ứng.
        ///         DataField: "UserCurrentId"
        ///         FilterExpression: 2
        ///         IsAutoGenNodeName: false
        ///         IsSqlValue: false
        ///         Name: "Văn bản chờ xử lý"
        ///         ProcessFunctionFilterId: 1
        ///         Value: "2551"
        /// </param>
        /// <remarks>
        ///     Trước khi gọi vào hàm này cần xử lý các bộ lọc của node có FilterExpression == 1 về hết FilterExpression = 2.
        ///     Xem trong egov.data-manager.tree.js;
        /// </remarks>
        /// <param name=" options">jQuery ajax option.</param>

        var request,
            callback,
            result,
            expression = [],
            expressionStr,
            filter,
            that = this;

        that.getDocumentStore(id, {
            success: function (data) {
                result = filterDocuments(data, filters, that._defaultSortBy);
                viewModel.reset();
                viewModel.add(result);
                egov.callback(options.success, viewModel);
            }
        });
    }

    DataManager.syncDocuments = function (storeId, filters) {
        /// <summary>
        /// Đồng bộ danh sách văn bản.
        /// </summary>
        /// <param name="storeId" type="int">Id kho văn bản tương ứng</param>
        /// <param name=" filters">Các bộ lọc của node tương ứng.
        ///         DataField: "UserCurrentId"
        ///         FilterExpression: 2
        ///         IsAutoGenNodeName: false
        ///         IsSqlValue: false
        ///         Name: "Văn bản chờ xử lý"
        ///         ProcessFunctionFilterId: 1
        ///         Value: "2551"
        /// </param>

        var entity,
            that,
            model,
            result,
            option = {};

        that = this;
        model = viewModel;
        entity = _entities.documentStore;
        entity.id = storeId;
        option.success = function (data) {
            result = filterDocuments(data, filters, that._defaultSortBy);
            viewModel.reset();
            viewModel.add(result);
        }

        that.getLastUpdate(entity, {
            success: function (lastUpdate) {
                that._syncDocumentStore(storeId, lastUpdate, option);
            }
        });
    }

    DataManager.getDocumentStore = function (id, options) {
        /// <summary>
        /// Trả về danh sách văn bản theo kho
        /// </summary>
        /// <param name="id">Id kho văn bản</param>
        /// <param name="options">jQuery ajax option</param>

        var request,
            callback,
            result,
            documentCopyIds,
            isMerge,
            that = this;

        // egov.entities.documentStore
        request = _entities.documentStore;

        request.id = id;
        request.option.data = { id: id };
        callback = options.success;

        options.success = function (data) {
            // Xử lý nghiệp vụ business ở đây.

            // Xử lý lưu mapping giữa document copy id với function Store id
            documentCopyIds = _.map(data, function (doc) {
                return {
                    documentCopyId: doc["DocumentCopyId"],
                    functionStoreId: id
                };
            });

            isMerge = true;
            that._dataAccess.insert(_entities.document_Store, documentCopyIds, isMerge, "documentCopyId");

            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    }

    DataManager.getDocumentPermission = function (documentCopyIds, options) {
        /// <summary>
        /// Trả về danh sách các permission của danh sách văn bản được chọn.
        /// </summary>
        /// <param name="documentCopyIds">Danh sách văn bản cần lấy permission</param>
        /// <param name="options">jQuery ajax option</param>

        var request,
            callback;

        request = $.extend({}, _entities.getDocumentPermission);

        request.option.data = { documentCopyIds: documentCopyIds };
        //callback = options.success;
        //options.success = function (data, lastUpdate) {
        //    // Xử lý kho văn bản trước khi trả về.

        //    egov.callback(callback, data);
        //};

        this.sendRequest(request, options);
    }

    DataManager.getFunctionStores = function (options) {
        /// <summary>
        /// Trả về danh sách tất cả FunctionStore trong hệ thống
        /// </summary>
        /// <param name="options" type="object">Jquery ajax option</param>

        var request,
            callback;

        // egov.entities.functionGroups
        request = _entities.functionGroups;
        callback = options.success;

        options.success = function (data) {
            // Xử lý nghiệp vụ business ở đây

            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    },

    DataManager.getFunctionStoreFromDocument = function (document, options) {
        /// <summary>
        /// Trả về kho văn bản chứa văn bản tương ứng
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="document">Văn bản</param>
        /// <param name="options">jQuery ajax option</param>

        var storeExpression,
            callback,
            doc,
            that;

        that = this;
        callback = options.success;
        doc = document;

        that.getFunctionStores({
            success: function (data) {
                if (data.length > 0) {
                    _.each(data, function (functionGroup) {
                        storeExpression = functionGroup["ClientExpression"];
                        if (eval(storeExpression)) {
                            // Trả về kho văn bản đầu tiên tìm được
                            egov.callback(callback, functionGroup[FunctionGroupId]);
                            return;
                        }
                    });
                }
            }
        })
    }

    DataManager.updateDocumentToStore = function (document) {
        /// <summary>
        /// Cập nhật document vào kho tương ứng dựa trên các bộ lọc của kho.
        /// </summary>
        /// <param name="document"></param>
        var that;

        that = this;
        that.getFunctionStoreFromDocument(document, {
            success: function (storeId) {
                // Todo: lamf sau
            }
        })
    }

    DataManager.getDocumentsCache = function (options) {
        /// <summary>
        /// Lấy danh sách văn bản ở trong cache 
        /// </summary>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.documents);
        entity.option.success = options.success;
        this.getCache(entity, options);
    }

    DataManager.getDocumentsReportCache = function (options) {
        /// <summary>
        /// Lấy danh sách văn bản ở trong cache 
        /// </summary>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.documents);
        entity.option.success = options.success;
        this.getCache(entity, options);
    }

    DataManager.getDocuments = function (data, options) {
        /// <summary>
        /// Lấy danh sách văn bản từ server về (không lưu cache)
        /// </summary>
        /// <param name="data">Các tham số khi post lên lấy dữ liệu</param>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.tempDocuments);
        entity.option.data = data;

        this.sendRequest(entity, options);
    }

    DataManager.updateDocuments = function (documents, callback) {
        /// <summary>
        /// Cập nhật cây văn bản ở trạng thái hiện tại vào cache
        /// </summary>
        /// <param name="documents">Danh sách văn bản</param>
        /// <param name="callback">Hàm thục thi gọi lại khi thành công</param>
        var entity = $.extend({}, _entities.documents);
        var that = this;

        entity.option.success = function (result) {
            entity.option.success = callback;
            if (result) {
                that._dataAccess.update(entity, documents, true);
            } else {
                that._dataAccess.insert(entity, documents);
            }
        };

        this.getCache(entity, {});
    }

    DataManager.getStorePrivateDocuments = function (data, options) {
        /// <summary>
        /// Lấy danh sách văn bản từ server về (không lưu cache)
        /// </summary>
        /// <param name="data">Các tham số khi post lên lấy dữ liệu</param>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.getStorePrivateDocuments);
        entity.option.data = data;

        this.sendRequest(entity, options);
    }

    //#region Private Methods

    DataManager._syncDocumentStore = function (id, lastUpdate, options) {
        /// <summary>
        /// Đồng bộ kho văn bản với server
        /// </summary>
        /// <param name="id">Id kho văn bản</param>
        /// <param name="options">jQuery ajax option</param>

        var request,
            callback,
            result,
            isReplaceUpdate,
            entity,
            that = this;

        // egov.entities.documentStore
        request = _entities.sync.documentStore;
        isReplaceUpdate = false;

        request.id = id;
        options.data = { id: id, lastUpdate: egov.toServerDate(lastUpdate) };
        callback = options.success;

        options.success = function (data) {
            // Xử lý cache kho văn bản trước khi trả về.
            entity = _entities.documentStore;
            entity.option.success = function (documentList) {
                egov.callback(callback, documentList);
            };

            that._dataAccess.update(_entities.documentStore, data, isReplaceUpdate);
        };

        egov.callback(request.request, options);
    }

    function filterDocuments(data, filters, defaultSortBy) {
        /// <summary>
        /// Lọc danh sách văn bản từ kho qua các bộ lọc của node tương ứng
        /// </summary>
        /// <param name="data">Danh sách văn bản từ kho</param>
        /// <param name="filters">Các bộ lọc tương ứng</param>
        /// <param name="defaultSortBy">Sắp xếp</param>
        var expressionStr,
            result,
            expression = [];
        // Lọc danh sách văn bản theo các bộ lọc
        _.each(filters, function (filter) {
            if (filter.FilterExpression === egov.enum.processFilterType.equal) {
                expression.push("doc['" + filter.DataField + "'] == " + filter.Value);
            }
            if (filter.FilterExpression === egov.enum.processFilterType.custom) {
                expression.push(filter.Value);
            }
        });

        // Join các bộ lọc lại
        expressionStr = expression.join(" && ");
        result = _.filter(data, function (doc) {
            return eval(expressionStr);
        });

        // Sắp xếp mặc định theo ngày thay đổi
        result = _.sortBy(result, function (doc) {
            return doc[defaultSortBy];
        });

        return result;
    }

    //#endregion
})
(this.egov = this.egov || {});