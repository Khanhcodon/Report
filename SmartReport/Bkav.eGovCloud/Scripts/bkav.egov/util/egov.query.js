define([],
    function () {
        "use strict";

        //#region Models

        var QueryModel = Backbone.Model.extend({
            defaults: {
                name: '',
                url: '',
                type: 'Get',
                traditional: false,
                async: true
            }
        });

        /// <summary>Đối tượng thể hiện danh sách các query</summary>
        var QueryCollection = Backbone.Collection.extend({
            model: QueryModel
        });

        //#endregion

        // Danh sách tất cả các query từ client lên server
        //note:Các query nào có method là post thì nên set thêm tokenId: 'id của tocken muốn lấy'
        ///     Có những hàm có method là post nhưng chỉ là lấy dữ liệu về thì không cần thiết phải set tokenid
        ///     Vì vậy cần lưu ý để set thêm tokenId.
        /// Quy ước đặt tockenId : lấy url bỏ các dấu'/' đi.
        //                       type='post' &&  url:'/Transfer/Transfer' => tokenId:'TransferTransfer'
        var _queries = [

	        // Lấy danh sách các hướng chuyển với văn bản đang edit
            { name: 'getEditTransferActions', url: '/Transfer/GetActionsEdit', type: 'Get' },

            // Lấy danh sách các hướng chuyển với văn bản tạo mới
            { name: 'getCreateTransferActions', url: '/Transfer/GetActionsCreate', type: 'Get' },

            // Lấy danh sách người nhận theo hướng chuyển
            { name: 'getUserByAction', url: '/Transfer/GetUserByAction', type: 'Get' },

             // Lấy các hướng chuyển của người dùng
            { name: 'getActionsTransferPlan', url: '/Transfer/GetActionsTransferPlan', type: 'Get' },

            // Tải về tệp đính kèm mới tải lên.
            { name: 'downloadAttachmentTemp', url: '/Attachment/DownloadAttachmentTempBase64', type: 'Get' },

            // Tạo ảnh từ file PDf, trả về url ảnh
            { name: 'createImagesFromBeginAndLastPdfPages', url: '/Document/CreateImagesFromBeginAndLastPdfPages', type: 'Get' },


            // Tải về tệp đính kèm có sẵn
            { name: 'downloadAttachment', url: '/Attachment/DownloadAttachmentBase64', type: 'Get' },

            // Tải về tệp đính kèm có sẵn
            { name: 'downloadFile', url: '/Attachment/DownloadFile', type: 'Get' },

            // Tải về tệp đính kèm để ký
            { name: 'downloadAttachmentForSignBase64', url: '/Attachment/DownloadAttachmentForSignBase64', type: 'Get', traditional: true },

            // Lấy cấu hình chữ ký
            { name: 'getSignConfig', url: '/Attachment/GetSignConfig', type: 'Get', traditional: true },

            // Chuyển văn bản
            { name: 'transfer', url: '/Transfer/Transfer', type: 'Post', traditional: true, tokenId: 'TransferTransfer' },

            // Chuyển văn bản
            { name: 'lightTransfer', url: '/Transfer/LightTransfer', type: 'Post' },

            // Chuyển ý kiến đóng góp: cho văn bản xin ý kiến, đồng xử lý.
            { name: 'TransferYKienDongGop', url: '/Transfer/TransferYKienDongGop', type: 'Post', traditional: true, tokenId: 'TransferTransferYKienDongGop' },

            // Tiếp nhận hồ sơ.
            { name: 'TransferTiepNhan', url: '/Transfer/TransferTiepNhan', type: 'Post', traditional: true, tokenId: 'TransferTransferTiepNhan' },

            // Thông báo
            { name: 'TransferAnnouncement', url: '/Transfer/TransferThongBao', type: 'Post', traditional: true, tokenId: 'TransferTransferThongBao' },

            // Xin ý kiến
            { name: 'TransferConsult', url: '/Transfer/TransferXinYKienToolbar', type: 'Post', traditional: true, tokenId: 'TransferTransferXinYKienToolbar' },

            // Xin ý kiến trên contextmenu trang chính
            { name: 'TransferConsultContext', url: '/Transfer/TransferXinYKienContext', type: 'Post', traditional: true, tokenId: 'TransferTransferXinYKienContext' },

             // Chuyen theo lo
            { name: 'transferTheoLo', url: '/Transfer/TransferTheoLo', type: 'Post', traditional: true, tokenId: 'TransferTransferTheoLo' },

            // Lấy danh sách Sổ văn bản
            { name: 'GetStores', url: '/Transfer/GetStores', type: 'Get' },

            // Lấy danh sách bảng mã.
            { name: 'GetCodes', url: '/Transfer/GetCodes', type: 'Get' },

            // Lưu sổ và phát hành văn bản
            { name: 'publish', url: '/Transfer/TransferPublish', type: 'Post', traditional: true, tokenId: 'TransferTransferPublish' },
             // Lưu sổ và phát hành văn bản theo lô
            { name: 'publishTheoLo', url: '/Transfer/TransferPublishTheoLo', type: 'Post', traditional: true, tokenId: 'TransferTransferPublishTheoLo' },

            // Lưu sổ và phát hành nội bộ
            { name: 'privatePublish', url: '/Transfer/TransferPrivatePublish', type: 'Post', traditional: true, tokenId: 'TransferTransferPrivatePublish' },

             // Lưu sổ và phát hành nội bộ theo lô
            { name: 'privatePublishTheoLo', url: '/Transfer/TransferPrivatePublishTheoLo', type: 'Post', traditional: true, tokenId: 'TransferTransferPrivatePublishTheoLo' },

           // Dự kiến phát hành
            { name: 'publishmentPlan', url: '/Transfer/PublishmentPlan', type: 'Post' },

            // Gửi ý kiến
            { name: 'sendComment', url: '/Document/SendComment', type: 'Post', tokenId: 'DocumentSendComment' },

            // Lấy văn bản tạo mới
            { name: 'getCreateDocument', url: '/Document/CreateNew', type: 'Get' },

            // Lấy loại hồ sơ, văn bản
            { name: 'getDoctype', url: '/Document/GetDocType', type: 'Get' },

            // Lấy nội dung form
            { name: 'getFormContent', url: '/Document/GetFormContent', type: 'Get' },

            // Lấy nội dung form
            { name: 'getFormUrl', url: '/Document/GetFormUrl', type: 'Get' },


            ///Lấy những người dùng thàm gia xử lý
            { name: 'getUsersProcess', url: '/Document/GetUsersProcess', type: 'Get' },

            ///Xác nhận bàn giao
             { name: 'confirmTransfer', url: '/Document/ConfirmTransfer', type: 'Post', tokenId: 'DocumentConfirmTransfer' },

           ///Xác nhận xử lý
            { name: 'confirmProcess', url: '/Document/ConfirmProcess', type: 'Post', tokenId: 'DocumentConfirmProcess' },

            ///Ký duyệt
            { name: 'approverSend', url: '/Approver/Send', type: 'Post', tokenId: 'ApproverSend' },

            ///Gia hạn xử lý
            { name: 'indexAddTime', url: '/Document/IndexAddTime', type: 'Post', tokenId: 'DocumentIndexAddTime' },

          ///Cập nhật gia hạn xử lý
             { name: 'updateDateAppointed', url: '/Document/UpdateDateAppointed', type: 'Post', tokenId: 'DocumentUpdateDateAppointed' },

            //Main
            ///Lấy các cấu hình của người dùng
            { name: 'getCommonConfigs', url: '/Home/GetCommonConfigs', type: 'Get' },

           //tree
            //Lấy danh sách cây văn bản
            { name: 'getDocumentTree', url: '/Home/GetFunctionByParentId', type: 'get' },

            ///Lấy ra tổng số các văn bản hồ sơ chưa đọc
            { name: 'getTotalDocumentUnreadMultiFunction', url: '/Home/GetTotalDocumentUnreadMultiFunction', type: 'Post' },

            { name: 'getTotalDocumentUnread', url: '/Home/GetTotalDocumentUnread', type: 'Post' },

            //GetTotalUnread
           //srore private
           //Lấy danh sách hồ sơ cá nhân, hồ sơ chia sẻ
            { name: 'getStorePrivate', url: '/StorePrivate/Gets', type: 'Get' },

            //Tạo mới hồ sơ cá nhân,hồ sơ chia sẻ
            { name: 'createStorePrivate', url: '/StorePrivate/Create', type: 'Post', tokenId: 'StorePrivateCreate', traditional: true },

            //Cập nhật hồ sơ cá nhân. hồ sơ chia sẻ
            { name: 'updateStorePrivate', url: '/StorePrivate/Update', type: 'Post', tokenId: 'StorePrivateUpdate', traditional: true },

            { name: 'anycStoreShare', url: '/StorePrivate/AnycStoreShare', type: 'Get' },
            //Mở hồ sơ cá nhân. hồ sơ chia sẻ
            { name: 'openStorePrivate', url: '/StorePrivate/Open', type: 'Post', tokenId: 'StorePrivateOpen' },

            //Đóng hồ sơ cá nhân. hồ sơ chia sẻ
            { name: 'closeStorePrivate', url: '/StorePrivate/Close', type: 'Post', tokenId: 'StorePrivateClose' },

            //Xóa hồ sơ cá nhân. hồ sơ chia sẻ
            { name: 'deleteStorePrivate', url: '/StorePrivate/Delete', type: 'Post', tokenId: 'StorePrivateDelete' },

            //Lấy danh sách văn bản hồ sơ trong hồ sơ cá nhân, hồ sơ chia sẻ
            { name: 'getStorePrivateDocuments', url: '/StorePrivate/GetDocuments', type: 'Post' },

            //Lấy danh sách người dùng trong hồ sơ cá nhân. hồ sơ chia sẻ
            { name: 'getUserJoined', url: '/StorePrivate/GetUserJoined', type: 'Get' },

             { name: 'storePrivateRemoveFile', url: '/StorePrivate/RemoveAttachment', type: 'post', tokenId: 'StorePrivateRemoveAttachment' },

              { name: 'storePrivateDownloadFile', url: '/StorePrivate/DownloadAttachment', type: 'Get' },

              { name: 'storePrivateOpenFile', url: '/StorePrivate/DownloadAttachmentBase64', type: 'Get' },

            //Thêm văn bản vào hồ sơ cá nhân/chia sẻ
            { name: 'SaveDocumentToStorePrivate', url: '/Document/SaveDocumentToStorePrivate', type: 'Post', tokenId: 'DocumentSaveDocumentToStorePrivate' },

            //Documents
            ///Lấy danh sách hồ sơ, văn bản
            { name: 'getDocuments', url: '/Home/GetDocuments', type: 'Post' },

            ///Lấy toàn bộ danh sách văn bản của node hiện tại
             { name: 'getAllDocument', url: '/Home/GetAllDocument', type: 'Post' },

            ///Lấy danh sách hồ sơ văn bản theo phân trang
            { name: 'getDocumentPaging', url: '/Home/GetDocumentPaging', type: 'Post' },

            ///Lấy danh sách hồ sơ, văn bản và phân trang mobile-tablet
            { name: 'getDocumentPagingForTabletAndMobile', url: '/Home/GetDocumentPagingForTabletAndMobile', type: 'Post' },

            //Lấy danh sách hồ sơ, văn bản mới được thêm vào node và những văn bản xóa khỏi node
            { name: 'getLastestDocuments', url: '/Home/GetLastestDocuments', type: 'Post', traditional: true },

            ///Lấy ngày hết hạn
            { name: 'getDateAppointed', url: '/Home/GetDateAppointed', type: 'Post' },

            ///Lấy quyền thao tác xử lý văn bản hồ sơ
            { name: 'getDocumentPermission', url: '/Document/GetDocumentPermission', type: 'Get', traditional: true },

            ///Xem trước thông tin văn bản hồ sơ
            { name: 'quickViewDocument', url: '/Home/QuickViewDocument', type: 'Get' },

              ///Lấy thông tin chi tiết văn bản hồ sơ
            { name: 'getDocumentDetail', url: '/Document/GetDocumentDetail', type: 'Get' },

            ///Thiết lập văn bản quan trong hay không quan trọng
            { name: 'setDocumentImportant', url: '/Home/SetDocumentImportant', type: 'Post' },

            //Document
            //Sửa văn bản
            { name: 'editNew', url: '/Document/EditNew/', type: 'Get' },

            //contextmenu
            ///Lấy lại văn bản ở các node
            { name: 'getContextItemForUndoTransfering', url: '/Document/GetContextItemForUndoTransfering/', type: 'Get' },

            // Kết thúc xử lý văn bản
            { name: 'finish', url: '/Finish/UpdateFinish', type: 'Post', tokenId: 'FinishUpdateFinish' },

           ///Xóa văn bản/hồ sơ
            { name: 'removeDocument', url: '/Document/RemoveDocument', type: 'Post', tokenId: 'DocumentRemoveDocument', traditional: true, },

            ///Lấy lại văn bản
            { name: 'undoTransfering', url: '/Document/UndoTransfering', type: 'Post', tokenId: 'DocumentUndoTransfering' },

            ///Lấy những người nhận văn bản để undo lại
            { name: 'getUsersForUndoTransfering', url: '/Document/GetUsersForUndoTransfering', type: 'Get' },

            ///Set trang thái đọc văn bản
            { name: 'setViewed', url: '/Document/SetViewed/', type: 'Post' },

            //Sửa hồ sơ đăng ký trực tuyến
            { name: 'editDocumentOnline', url: '/Document/EditDocumentOnline/', type: 'Get', },

            ///Thiết lập các config của người dùng
            { name: 'setUserConfig', url: '/Account/SetUserConfig/', type: 'Post' },

            ///Thiết lập các config của người dùng cho Mobile
            { name: 'setMobileUserConfig', url: '/Account/SetMobileUserConfig/', type: 'Post', tokenId: 'SetMobileUserConfig' },

            //Sửa nội dung hồ sơ
            { name: 'editDocumentContent', url: '/Document/EditContent/', type: 'Post' },

            //Lay y kien thuong dung cua nguoi dung
             { name: 'getCommonComments', url: '/Document/GetCommonComments/', type: 'Get', },

             //Tim kiem van ban
            { name: 'searchDocuments', url: '/Document/SearchDocuments', type: 'Post', traditional: true },

            //Lay loai van ban
             { name: 'getDocTypes', url: '/Document/GetDocTypes', type: 'Get', },

              //Lay hinh thuc
             { name: 'getDocField', url: '/Document/GetDocField', type: 'Get', },

             // Lấy danh sách văn bản liên quan
             { name: 'getDocumentRelations', url: '/document/GetDocumentRelations', type: 'Get' },

             //Tìm kiếm nhanh
             { name: 'quickSearch', url: '/Home/QuickSearch', type: 'Get', },

             //lấy dữ liệu cho form tìm kiếm nâng cao
             { name: 'getSearchAdvanceForm', url: '/Document/GetSearchAdvanceForm', type: 'Get', },

             //Tìm kiếm nâng cao
             { name: 'searchAdvance', url: '/Home/SearchAdvance', type: 'Get', },

             { name: 'getFileUploadSettings', url: '/Attachment/GetFileUploadSettings', type: 'Get', },

             { name: 'setPopUpSize', url: '/Account/SetPopUpSize', type: 'Post' },

             //Upload file scan
             { name: 'uploadTempScan', url: '/Attachment/UploadTempScan', type: 'Post' },

             //Upload file
             { name: 'uploadTemp', url: '/Attachment/UploadTemp', type: 'Post', contentType: false, processData: false },

             //Lấy danh sách các mẫu template ý kiến mà người dùng đã soạn mẫu trước
             { name: 'getTemplateComments', url: '/Document/GetTemplateComments', type: 'get' },

             //Tao moi mẫu template ý kiến mà người dùng đã soạn mẫu trước
             { name: 'createTemplateComments', url: '/Document/CreateTemplateComments', type: 'post' },

               //Cap nhat mẫu template ý kiến mà người dùng đã soạn mẫu trước
             { name: 'updateTemplateComments', url: '/Document/UpdateTemplateComments', type: 'post' },

               //Tao moi mẫu template ý kiến mà người dùng đã soạn mẫu trước
             { name: 'deleteTemplateComments', url: '/Document/DeleteTemplateComments', type: 'post' },

              // Lấy danh sách người nhận theo hướng chuyển theo lo
             { name: 'getUserByActionTheoLo', url: '/Transfer/GetUserByActionTheoLo', type: 'post', traditional: true },

             // Lấy danh sách hướng chuyển theo lo
             { name: 'getActionTheoLoVanBan', url: '/Transfer/GetActionTheoLoVanBan', type: 'post', traditional: true, async: false },

             // Trả về yêu cầu bổ sung của hồ sơ
             { name: 'getRequiredSupplementary', url: '/Supplementary/GetRequiredSupplementary', type: 'get' },

             // Trả về yêu cầu bổ sung của hồ sơ
             { name: 'GetRequiredSupplementaryInDocument', url: '/Supplementary/GetRequiredSupplementaryInDocument', type: 'get' },

             // Tạo yêu cầu bổ sung hồ sơ
             { name: 'createRequiredSupplementary', url: '/Supplementary/CreateRequiredSupplementary', type: 'Post' },

             // Trả về các phiên bản của nội dung văn bản
             { name: 'getDocumentContentVersion', url: '/Document/getDocumentContentVersion', type: 'Get' },

             // Tiếp nhận bổ sung
             { name: 'receiveSupplementary', url: '/Supplementary/receiveSupplementary', type: 'Get' },

             // Trả về ngày hẹn trả mới khi tiếp nhận bổ sung
             { name: 'getNewDateAppointed', url: '/Supplementary/GetDateAppointed', type: 'Get' },

             // Tiếp nhận bổ sung
             { name: 'updateSupplementary', url: '/Supplementary/Receive', type: 'Post' },

             // Trả về nội dung phiếu in khi tiếp nhận bổ sung
             { name: 'getSuppPrintContent', url: '/Supplementary/GetPrintContent', type: 'Get' },

             // Trang in
             { name: 'print', url: '/Print/Index', type: 'Get' },

             { name: 'printReceipt', url: '/Print/PrintReceipt', type: 'Get' },

             // Trả về danh sách các phiếu in của hồ sơ 
             { name: 'getPrints', url: '/Print/GetPrints', type: 'Get' },

             // Trả về danh sách các mẫu phiếu in theo nghiệp vụ
             { name: 'getPrintTemplates', url: '/Print/GetPrintTemplates', type: 'Get' },

             // Trả về danh sách các mẫu phiếu in theo danh sách hồ sơ
             { name: 'getPrintByDocCopys', url: '/Print/GetPrintByDocCopys', type: 'Get' },

             // Trả về danh sách các mẫu phiếu in theo nghiệp vụ
             { name: 'renewals', url: '/Document/Renewals', type: 'Post' },

             // Cập nhật kết quả xử lý cuối cùng
             { name: 'updateLastResult', url: '/Document/UpdateLastResult', type: 'Post' },

             // Trả về dữ liệu cho form trả kết quả
             { name: 'getReturnResult', url: '/Return/GetReturnResult', type: 'Get' },

             // Trả về dữ liệu cho form trả kết quả
             { name: 'updateReturn', url: '/Return/UpdateReturn', type: 'Post' },

             // Cập nhật văn bản
             { name: 'saveDoc', url: '/Transfer/SaveDoc', type: 'Post', tokenId: "TransferSaveDoc" },

             // Lưu văn bản dự thảo
             { name: 'saveDocDraft', url: '/Transfer/SaveDocDraft', type: 'Post', tokenId: "TransferSaveDocDraft" },
        ];

        var Query = function () {
            /// <summary>
            /// Đối tượng quản lý các query từ client lên server.
            /// </summary>

            // Kiểm tra có url trùng nhau
            // Khi thêm mới 1 query nếu trùng sẽ bị báo ngay.
            var check = _.uniq(_.map(_queries, function (q) {
                return q !== undefined && q.url.trim();
            }));
            if (check.length !== _queries.length) {
                throw "Lặp url, kiểm tra lại để đảm bảo không bị lặp";
            }

            // Kiểm tra tên trùng nhau
            check = _.uniq(_.map(_queries, function (q) {
                return q !== undefined && q.name.trim();
            }));

            if (check.length !== _queries.length) {
                throw "Lặp tên query, kiểm tra lại để đảm bảo không bị lặp";
            }

            this.model = new QueryCollection(_queries);
            this.init();
        };

        Query.prototype.init = function () {
            /// <summary>
            /// Render các function theo cấu hình
            /// </summary>
            /// <returns type="this"></returns>
            var that = this;

            this.stops = [];//chưa danh sách ajax property khi run ajax call dữ liêu => để có thể phương thức ajax 

            // Render các function theo tên các query trong danh sách       
            this.model.each(function (query) {
                var name = query.get('name');

                that[name] = function (param, successFunc, errorFunc, completeFunc) {
                    /// <summary>
                    /// Tự động tạo hàm theo tên của query.
                    /// </summary>
                    /// <param name="param" type="object">Tham số cho url.</param>
                    /// <param name="successFunc">Hàm callback khi query thành công.</param>
                    /// <param name="errorFunc">Hàm callback khi query lỗi.</param>
                    /// <param name="completeFunc">Hàm callback sau khi kết thúc query.</param>
                    that._exeQuery(query, param, name, successFunc, errorFunc, completeFunc);
                }
            });

            return this;
        };

        Query.prototype._exeQuery = function (query, param, name, successFunc, errorFunc, completeFunc, beforeSendFunc) {
            /// <summary>Private: hàm thực thi một quẻry</summary>
            /// <param name="query" type="QueryModel">Query cần thực thi</param>
            /// <param name="param" type="object">Tham số cho query</param>
            /// <param name="successFunc" type="function">Hàm thực thi sau khi query thành công. Kết quả query được truyền vào hàm này.</param>
            /// <param name="errorFunc" type="function">Hàm thực thi nếu query không thành công. Đối tượng xhr sẽ được truyền vào hàm này.</param>
            /// <param name="completeFunc" type="function">Hàm thực thi sau khi kết thúc query</param>
            /// <param name="beforeSendFunc" type="function">Hàm thực thi truoc khi goi ve server</param>

            var type, processName, that, setting;
            that = this;

            ///Kiểm tra type có phải la kiểu string hay không và có khác null
            if (typeof query.get('type') !== 'string' || query.get('type') === '') {
                type = 'get';
            } else {
                type = query.get('type');
            }

            processName = name + JSON.stringify(param) + "processing";
            //Kiểm tra type có phải là method post và tockenId là kiểu string và có giá trị
            if (type.toLowerCase() === 'post'
                && (typeof query.get('tokenId') === 'string')
                && query.get('tokenId') !== '') {
                var tokenId = "#" + query.get('tokenId');
                param['__RequestVerificationToken'] = $("input[name='__RequestVerificationToken']", tokenId).val();
            }

            setting = {
                url: query.get('url'),
                type: type,
                async: query.get('async'),
                data: param,
                beforeSend: function () {
                    that[processName] = true;
                    egov.callback(beforeSendFunc);
                },
                success: function (result) {
                    egov.callback(successFunc, result);
                },
                error: function (xhr) {
                    egov.callback(errorFunc, xhr);
                },
                complete: function () {
                    delete that[processName];
                    egov.callback(completeFunc);
                }
            };

            if (query.get('traditional')) {
                setting.traditional = query.get('traditional');
                setting.dataType = 'json';
            }

            this.stops[query.get('name')] = $.ajax(setting);
        };

        return Query;
    });