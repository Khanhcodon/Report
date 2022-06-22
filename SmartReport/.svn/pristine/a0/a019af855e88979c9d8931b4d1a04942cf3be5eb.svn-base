(function (egov) {

    "use strict";

    var _entities, DataManager;

    if (egov.dataManager === undefined) {
        egov.log("Chưa khởi tạo data manager.");
        return;
    }

    _entities = egov.entities;
    DataManager = egov.dataManager;

    DataManager.getDocumentTemplate = function (isCreate, docTypeId, categoryBusinessId, currentNodeId, options) {
        /// <summary>
        /// Trả về template cấu hình form văn bản
        /// </summary>
        /// <param name="isCreate">Giá trị xác định văn bản là đang tạo mới hay mở</param>
        /// <param name="docTypeId">Id loại văn bản</param>
        /// <param name="categoryBusinessId">categoryBusinessId (truyền vào khi mở văn bản)</param>
        /// <param name="currentNodeId">Node hiện tại (truyền vào khi mở văn bản)</param>

        var request,
            callback,
            result,
            that = this;

        // egov.entities.documentTemplate
        request = _entities.documentTemplate;

        request.option.data = {
            isCreate: isCreate,
            docTypeId: docTypeId,
            categoryBusinessId: categoryBusinessId
        };

        callback = options.success;

        options.success = function (data) {
            // Xử lý dữ liệu trước khi trả về.

            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    }

    DataManager.getDocument = function (documentCopyId, options) {
        /// <summary>
        /// Trả về văn bản theo id
        /// </summary>
        /// <param name="documentCopyId">Document Copy Id</param>
        /// <param name="options">jQuery ajax option</param>

        var that,
            result,
            callback,
            storeId,
            storeEntity,
            document_StoreEntity;

        that = this;
        callback = options.success;
        document_StoreEntity = _entities.document_Store;

        // Xác định kho văn bản mà document thuộc vào
        document_StoreEntity.option.success = function (data) {
            storeId = _.find(data, function (document_store) {
                return document_store["documentCopyId"] === documentCopyId;
            });

            if (storeId) {
                // Lấy document từ kho văn bản
                that.getDocumentStore(storeId, {
                    success: function (documents) {
                        result = _.find(documents, function (doc) {
                            return doc.DocumentCopyId === documentCopyId;
                        });

                        egov.callback(callback, result);
                    }
                });
            }
            else {
                // Lấy document trên server

            }
        };

        that._dataAccess.get(_entities.document_Store);
    }

    DataManager.getTemplateComments = function (options) {
        /// <summary>
        /// Trả về danh sách các ý kiến thường dùng
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var that,
            entity;

        that = this;
        entity = _entities.allTemplateComments;

        that.sendRequest(entity, options);
    }

    DataManager.updateTemplateComments = function (templateComments, isReplace, options) {
        /// <summary>
        /// Trả về danh sách các ý kiến thường dùng
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var that,
            entity,
            exist,
            tmpcomment,
            arr;

        that = this;
        entity = _entities.allTemplateComments;

        this.getTemplateComments({
            success: function (data) {
                if (typeof templateComments === "string") {
                    exist = false;
                    tmpcomment = templateComments.toLowerCase();
                    arr = data;
                    if (arr && arr.length > 0) {
                        for (var j = 0; j < arr.length; j++) {
                            var item = arr[j];
                            if (item.toLowerCase() == tmpcomment) {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist) {
                            if (isReplace) {
                                arr = [templateComments];
                            } else {
                                arr.push(tmpcomment);
                            }
                            data = arr;
                        }
                    }
                }
                else if (templateComments instanceof Array) {
                    if (isReplace) {
                        data = templateComments;
                    } else {
                        data = data.concat(templateComments);
                    }
                }
                else {
                    if (isReplace) {
                        data = [templateComments];
                    } else {
                        data.push(templateComments);
                    }
                }

                //Set lại success
                entity.option.success = function () {
                    if (options && options.success) {
                        egov.callback(options.success);
                    }
                };

                that._dataAccess.update(entity, data, true);
            }
        });
    }

    DataManager.getCommonComment = function (options) {
        /// <summary>
        /// Trả về danh sách các ý kiến thường dùng
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var that,
            entity;

        that = this;
        entity = _entities.allCommonComments;

        that.sendRequest(entity, options);
    }

    DataManager.addCommonComment = function (comments, isReplace, options) {
        /// <summary>
        /// Trả về danh sách các ý kiến thường dùng
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var that,
            entity,
            exist,
            hasChange;

        that = this;
        entity = _entities.allCommonComments;
        //Lấy danh sách comment đã có, đẩy comment mới vào và cập nhật lại
        this.getCommonComment({
            success: function (data) {
                if (typeof comments === "string") {
                    if (data && data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].toLowerCase() == comments.toLowerCase()) {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist) {
                            data.push(comments);
                            hasChange = true;
                        }
                    }
                } else if (comments instanceof Array && comments.length > 0) {
                    if (isReplace) {
                        data = comments;
                    } else {
                        data = data.concat(comments);
                    }
                    hasChange = true;
                }

                if (hasChange) {
                    //Set lại success
                    entity.option.success = function () {
                        if (options && options.success) {
                            egov.callback(options.success);
                        }
                    };
                    that._dataAccess.update(entity, data, true);
                }
            }
        })
    }

    DataManager.getDocumentEditInfo = function (documentCopyId, storePrivateId, options) {
        /// <summary>
        /// Lấy các thông tin văn bản khi edit
        /// </summary>
        /// <param name="documentCopyId">Documentcopy id</param>
        /// <param name="options">jQuery ajax options</param>

        var that,
            entity;

        that = this;
        entity = _entities.documentEdit;
        entity.option.data = {
            id: documentCopyId,
            storePrivateId: storePrivateId
        };

        that.sendRequest(entity, options);
    }

    DataManager.getDocumentCreateInfo = function (doctypeId, relationId, options) {
        /// <summary>
        /// Trả về thông tin văn bản khi tạo mới
        /// </summary>
        /// <param name="doctypeId">Loại văn bản</param>
        /// <param name="relationId">Văn bản đính kèm, dùng trong chức năng trả lời</param>
        /// <param name="options">jQuery ajax options</param>

        var that,
            entity;

        that = this;
        entity = _entities.documentCreate;
        entity.option.data = { id: doctypeId, documentCopyRelationId: relationId };

        that.sendRequest(entity, options);
    }

    DataManager.getDocumentCreateActions = function (doctypeId, hasChangingDoctype, options) {
        /// <summary>
        /// Trả về danh sách các hướng chuyển khi tiếp nhận hoặc phân loại văn bản
        /// </summary>
        /// <param name="doctypeId">Loại văn bản tạo mới hoặc loại văn bản được phân loại</param>
        /// <param name="hasChangingDoctype">Giá trị xác định văn bản hiện tại đang phân loại hay không</param>
        /// <param name="options">jQuery ajax action</param>

        var that,
            entity;

        that = this;
        entity = _entities.documentCreateAction;
        entity.option.data = { documentTypeId: doctypeId, isPhanloai: hasChangingDoctype };

        that.sendRequest(entity, options);
    }

    DataManager.getDocumentEditActions = function (documentCopyId, options) {
        /// <summary>
        /// Trả về danh sách các hướng chuyển khi bàn giao văn bản
        /// </summary>
        /// <param name="documentCopyId">Document copy id</param>
        /// <param name="options">jQuery ajax action</param>

        var that,
            entity;

        that = this;
        entity = _entities.documentEditAction;
        entity.option.data = { documentCopyId: documentCopyId };

        that.sendRequest(entity, options);
    }

    DataManager.getUserByAction = function (actionId, workflowId, documentCopyId, options) {
        /// <summary>
        /// Trả về danh sách người nhận theo hướng chuyển
        /// </summary>
        /// <param name="actionId">Id hướng chuyển</param>
        /// <param name="workflowId">Id quy trình</param>
        /// <param name="documentCopyId">Document copy id</param>
        /// <param name="options">jquery ajax option</param>
        var that,
            entity;

        that = this;
        entity = _entities.getUserByAction;
        entity.option.data = {
            actionId: actionId,
            workflowId: workflowId,
            documentCopyId: documentCopyId,
        };

        that.sendRequest(entity, options);
    }

    DataManager.transfer = function (doc, destination, files, modifiedFiles, removeAttachmentIds, storePrivateId, destinationPlan, options) {
        /// <summary>
        /// Bàn giao văn bản
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="destination">Danh sách người nhận</param>
        /// <param name="files">Danh sách các file đính kèm lên</param>
        /// <param name="modifiedFiles">Danh sách các file chỉnh sửa</param>
        /// <param name="removeAttachmentIds">Danh sách các file đã xóa</param>
        /// <param name="storePrivateId">Id hồ sơ cá nhân</param>
        /// <param name="destinationPlan">Dự kiến chuyển</param>
        /// <param name="options">jquery ajax option</param>
        var that,
            document,
            entity;

        that = this;
        entity = _entities.transfer;
        entity.option.data = {
            doc: doc,
            destination: destination,
            files: files,
            modifiedFiles: modifiedFiles,
            removeAttachmentIds: removeAttachmentIds,
            storePrivateId: storePrivateId,
            destinationPlan: destinationPlan
        };

        that.sendRequest(entity, options);
    }

    DataManager.getDocumentDetail = function (id, options) {
        /// <summary>
        /// Trả về template cấu hình form văn bản
        /// </summary>
        /// <param name="isCreate">Giá trị xác định văn bản là đang tạo mới hay mở</param>
        /// <param name="id">DocumentCopy id khi mở văn bản hoặc doctype id khi tạo mới.</param>
        /// <param name="options">jquery ajax option</param>

        var request,
            callback,
            that = this;

        request = _entities.getDocumentDetail;
        request.id = id;
        request.option.data = { id: id };
        callback = options.success;

        options.success = function (data) {
            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    }

    DataManager.filterCitizen = function (name, idCardNumber, phoneNumber, email, options) {
        /// <summary>
        /// Trả về template cấu hình form văn bản
        /// </summary>
        /// <param name="isCreate">Giá trị xác định văn bản là đang tạo mới hay mở</param>
        /// <param name="id">DocumentCopy id khi mở văn bản hoặc doctype id khi tạo mới.</param>
        /// <param name="options">jquery ajax option</param>

        var request,
            callback,
            that = this;

        request = _entities.filterCitizen;
        request.option.data = {
            name: name,
            idCardNumber: idCardNumber,
            phoneNumber: phoneNumber,
            email: email
        };
        callback = options.success;

        options.success = function (data) {
            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    }

    //#region Private Methods

    //#endregion
})
(this.egov = this.egov || {});