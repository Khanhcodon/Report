(function (egov) {

    "use strict";

    var _entities, DataManager, viewModel;

    if (egov.dataManager === undefined) {
        egov.log("Chưa khởi tạo data manager.");
        return;
    }

    if (egov.viewModels.tree === undefined) {
        egov.log("Chưa khởi tạo models.");
        return;
    }

    viewModel = egov.viewModels.tree;
    _entities = egov.entities;
    DataManager = egov.dataManager;

    DataManager.getTree = function (id, options) {
        /// <summary>
        /// Trả về danh sách cây văn bản
        /// </summary>
        /// /// <param name="id" type="int">Id node cha. Đặt = 0 để lấy danh sách các node root.</param>
        /// <param name="options">jQuery ajax option</param>

        var request,
            callback,
            result,
            syncRequest,
            parentNode,
            that = this;

        // egov.entities.documentTree
        request = _entities.documentTree;

        callback = options.success;
        options.success = function (data) {
            /// <summary>
            /// Xử lý danh sách các node lấy ra từ database (server, cache);
            /// </summary>
            /// <param name="data">Danh sách tất các node.</param>

            // Trả về danh sách node theo node cha. Trường hợp id == 0 lấy ra danh sách các node root.
            result = _.filter(data, function (node) {
                return (id === 0 && node.parentid == null) || (id !== 0 && node.parentid == id);
            });

            // Sort theo thứ tự hiển thị
            result = _.sortBy(result, function (node) {
                return node.order;
            });

            // Parse chuỗi json config hiển thị các cột cho danh sách văn bản ra json object.
            _.each(result, function (node) {
                node.columnSetting = egov.toJSON(node.columnSetting);
            });

            // Xử lý sinh node nếu bộ lọc là kết quả câu sql.
            result = _generateNodeFromFilter(result);

            if (id === 0) {
                viewModel.reset();
                viewModel.add(result);
            }
            else {
                parentNode = viewModel.detect(function (node) {
                    return node.get("functionId") === id;
                });
                parentNode.set("children", new egov.models.TreeList(result));
            }
            egov.callback(callback, viewModel);
        };

        this.sendRequest(request, options);
    }

    DataManager.getNodeFromStoreId = function (storeId, options) {
        /// <summary>
        /// Trả về node trên cây văn bản theo kho văn bản
        /// </summary>
        /// <param name="storeId">Id kho văn bản</param>
        /// <param name="options">jQuery ajax option</param>

        var callback,
            result,
            that;

        that = this;
        callback = options.success;

        result = viewModel.detect(function (node) {
            return node.get("group") === storeId;
        });

        egov.callback(callback, result);
    }

    //#region Question 

    DataManager.getNodeQuestion = function (options) {
        /// <summary>
        /// Trả về danh sách câu hỏi
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var entity = _entities.getNodeQuestion;
        this.sendRequest(entity, options);
    }

    //#endregion

    //#store private

    DataManager.getStorePrivate = function (options) {
        /// <summary>
        /// Trả về danh sách các hồ sơ cá nhân
        /// </summary>
        /// <param name="options">jquery ajax option</param>

        if (egov && egov.storePrivate) {
            egov.callback(options.success, egov.storePrivate);
            return;
        }

        var entity = _entities.getStorePrivate;
        this.sendRequest(entity, options);
    }

    //#endregion

    //#region document tree

    DataManager.getTreesByRoot = function (parentId, options) {
        /// <summary>
        /// Trả về danh sách cây văn bản
        /// </summary>
        /// <param name="parentId" type="int">Id node cha. Đặt = 0 để lấy danh sách các node root.</param>
        /// <param name="options">jQuery ajax option</param>

        var entity = $.extend({}, _entities.trees);
        parentId = parentId || 0;
        entity.option.data = { id: parentId };

        this.sendRequest(entity, options);
    }

    DataManager.updateTree = function (treeData) {
        /// <summary>
        /// Cập nhật cây văn bản ở trạng thái hiện tại vào cache
        /// </summary>
        /// <param name="treeData">Dữ liệu json của cây văn bản</param>

        var entity = $.extend({}, _entities.trees);
        this._dataAccess.update(entity, treeData, true);
    }

    DataManager.updatePropInTreeModel = function (nodeId, propName, propValue) {
        /// <summary>
        /// Cập nhật lại giá trị theo thuộc tính theo id node truyền vào
        /// </summary>
        /// <param name="nodeId" type="int">node id truyền vào để cập nhật</param>
        /// <param name="propName" type="string">Tên thuộc tính muốn cập nhật</param>
        /// <param name="propValue">Giá trị gán vào cho thuộc tính</param>

        var entity = _entities.trees;
        var that = this;
        this.getTreesByRoot(0, {
            success: function (data) {
                if (!data || data.length <= 0) {
                    return;
                }

                for (var j = 0; j < data.length; j++) {
                    if (data[j].id === nodeId) {
                        data[j][propName] = propValue;
                        break;
                    }
                }

                entity.option.success = null;
                that._dataAccess.update(entity, data, true);
            }
        });
    }

    //endregion

    //#region private methods

    function _generateNodeFromFilter(nodes) {
        /// <summary>
        /// Xử lý sinh các node nếu node hiện tại có bộ lọc cho phép tự sinh các node theo sql.
        /// </summary>
        /// <param name="nodes">Danh sách node</param>

        var result = [],
            filters,
            filterValues,
            newNodes = [],
            newFilter;

        _.each(nodes, function (node) {
            filters = node.filter;
            if (filters.length === 0) {
                result.push(node);
                return; // continue
            }

            _.each(filters, function (filter) {
                if (!filter.IsAutoGenNodeName) {
                    result.push(node);
                    return; // continue
                }

                filterValues = egov.toJSON(filter.Value);
                // Trường hợp câu SQL không có kết quả.
                if (filterValues === undefined && filterValues.length === 0) {
                    filter = null; // Clear filter theo sql
                    result.push(node);
                    return; // continue
                }

                _.each(filterValues, function (expression) {
                    var newNode = _.clone(node);
                    newNode.name = expression.TextField;

                    newFilter = _.clone(filter);
                    newFilter.Value = expression.DataField;
                    newFilter.IsSqlValue = false;
                    newFilter.IsAutoGenNodeName = false;

                    newNode.filter = [];
                    newNode.filter.push(newFilter);
                    newNodes.push(newNode);
                });

                // Sắp xếp lại danh sách các node tự sinh theo tên
                newNodes = _.sortBy(newNodes, function (itm) {
                    return itm.name;
                });

                result = result.concat(newNodes);
            });
        });

        return result;
    }

    //#endregion
})
(this.egov = this.egov || {});