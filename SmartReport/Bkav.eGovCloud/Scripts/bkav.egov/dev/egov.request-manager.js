
(function (egov, Offline) {

    "use strict";

    var RequestManager = function () {
        /// <summary>
        /// Đối tượng quản lý các ajax http request từ client lên server.
        /// </summary>
        var check;

        this.profilers = [];

        this.dataDefaults = {
            type: 'GET',
            async: true,
            traditional: false,
            //global: false
        }

        // Kiểm tra có url trùng nhau
        // Khi thêm mới 1 query nếu trùng sẽ bị báo ngay.
        check = _.find(_.groupBy(_queries, function (q) { return q.url; }), function (g) {
            return g.length > 1;
        });

        if (check != null) {
            console.log("Lặp url, kiểm tra lại để đảm bảo không bị lặp: ");
            console.log(check);
        }

        // Kiểm tra tên trùng nhau
        check = _.find(_.groupBy(_queries, function (q) { return q.name; }), function (g) {
            return g.length > 1;
        });

        if (check != null) {
            console.log("Lặp tên query, kiểm tra lại để đảm bảo không bị lặp");
            console.log(check);
        }

        this.model = _queries;

        this.setDefaultAjaxSetting();
        this.init();

        this._profilers();
    }

    RequestManager.prototype.setDefaultAjaxSetting = function () {
        /// <summary>
        /// Xử lý các thiết lập chung cho mỗi ajax http request
        /// </summary>

        // Xử lý lỗi mặc định
        //$(document).ajaxError(function (e, jqXHR) {
        //    //if (jqXHR.status === 200) {

        //    //    // Xử lý các mã lỗi ở đây
        //    //    // window.location.replace(egov.getRelativeEndpointUrl('/Error.html'));
        //    //    egov.log(jqXHR);
        //    //}
        //});
    }

    RequestManager.prototype.init = function () {
        /// <summary>
        /// Render các function theo cấu hình
        /// </summary>
        /// <returns type="this"></returns>
        var that = this, name;

        //chưa danh sách ajax property khi run ajax call dữ liêu => để có thể phương thức ajax
        this.aborts = {};

        // Render các function theo tên các query trong danh sách
        _.each(this.model, function (query) {
            name = query['name'];
            that[name] = function (ajaxOption) {
                /// <summary>
                /// Tự động tạo hàm theo tên của query.
                /// </summary>
                /// <param name="ajaxOption" type="object">jQuery ajax option</param>

                // closure: query
                var processName = query.name + JSON.stringify(ajaxOption.data) + "processing";
                that._exeQuery(query, ajaxOption);
                //if (!that[processName]) {
                //    that._exeQuery(query, ajaxOption);
                //} else {
                //    egov.log("Gọi lặp chức năng " + query.name);
                //}
            }
        });

        return this;
    }

    RequestManager.prototype._exeQuery = function (query, ajaxOption) {
        /// <summary>Private: hàm thực thi một query</summary>
        /// <param name="query" type="QueryModel">Query cần thực thi</param>
        /// <param name="ajaxOption" type="object">Ajax option</param>
        var type,
            callerOptions,
            defaultOptions,
            tokenId,
            processName,
            beforeSendOption,
            completeOption,
            successOption,
            that = this;

        processName = query.name + JSON.stringify(ajaxOption.data);
        defaultOptions = $.extend({}, that.dataDefaults);

        if (query['async'] !== undefined) {
            defaultOptions.async = query['async'];
        }
        defaultOptions.async = true;

        if (query['traditional'] !== undefined) {
            defaultOptions.dataType = 'json';
            defaultOptions.traditional = query['traditional'];
        }

        if (query['hasToken'] !== undefined) {
            tokenId = '#' + query['url'].replace(/\//g, '');
            ajaxOption.data['__RequestVerificationToken'] = $("input[name='__RequestVerificationToken']", tokenId).val();
        }

        beforeSendOption = ajaxOption.beforeSend;
        ajaxOption.beforeSend = function (xhr, settings) {
            that[processName] = true;
            that.profilers.push({
                processName: processName,
                Name: settings.url,
                Started: new Date
            });

            if (typeof beforeSendOption === 'function')
                beforeSendOption();
        };

        successOption = ajaxOption.success;
        ajaxOption.success = function (result) {
            var profiler = _.find(that.profilers, function (p) { return p.processName === processName });
            profiler && (profiler.durations = new Date - profiler.Started);

            delete that[processName];
            if (typeof successOption === 'function')
                successOption(result);
        };

        completeOption = ajaxOption.complete;
        ajaxOption.complete = function () {
            console.log(that.profilers);
            delete that[processName];

            if (typeof completeOption === 'function')
                completeOption();
        };

        ajaxOption.data = ajaxOption.data || {};
        ajaxOption.data.puid = egov.userid;
        callerOptions = $.extend({}, defaultOptions, query, ajaxOption);

        //Overide lại XmlhttpRequest để lấy trạng thái kết nối của request đấy được trả về để đánh trạng thái kết nối
        if (Offline && !egov.isMobile) {
            Offline.options.checks = {
                xhr: callerOptions
            };
        }

        this.aborts[query['name']] = $.ajax(callerOptions);
    }

    //#region Log Profilers

    RequestManager.prototype._profilers = function () {
        setInterval(function () {
            if (this.profilers.length === 0) return;

            var data = _.map(this.profilers, function (p) {
                return {
                    Name: p.Name,
                    Started: p.Started.toServerString(),
                    DurationMilliseconds: p.durations
                }
            });

            this.profilers = [];

            $.ajax({
                url: '/profiler/updateclient',
                type: 'Post',
                data: { json: JSON.stringify(data) },
                complete: function () {

                }
            });
        }.bind(this), 5 * 1000);
    }

    //#endregion

    //#region Private Fields

    // Danh sách tất cả các query từ client lên server
    var _queries = [

        //#region Common
        
        { name: 'getDataCompile', url: '/Document/GetDataCompile' },

        // Lấy danh sách tất cả user trong hệ thống
        { name: 'getAllUsers', url: '/Common/GetAllUsers' },

        // Lấy danh sách tất cả category trong hệ thống
        { name: 'getCategories', url: '/Common/GetCategories' },

        // Lấy danh sách tất cả department theo user trong hệ thống
        { name: 'getDepartmentsByUser', url: '/Common/GetDepartmentsByUser' },

        // Lấy danh sách tất cả department theo user trong hệ thống
        { name: 'getDepartmentsCurrent', url: '/Common/GetCurrentDepartments' },

        // Lấy danh sách tất cả department trong hệ thống
        { name: 'getAllDepartment', url: '/Common/GetAllDepartment' },

        // Lấy danh sách tất cả jobtitle trong hệ thống
        { name: 'getAllJobTitlies', url: '/Common/getAllJobTitlies' },

        { name: 'getAllUserDepartmentJobTitlesPosition', url: '/Common/GetAllUserDepartmentJobTitlesPosition' },

        // Lấy ra danh sách tất cả các chức vụ trong hệ thống
        { name: 'getAllPosition', url: '/Common/GetAllPosition' },

        { name: 'getAllAddress', url: '/Common/GetAllAddress' },

        // Lấy danh sách tất cả user trong hệ thống
        { name: 'getKeywords', url: '/Common/GetKeywords' },

        // Lấy danh sách tất cả user trong hệ thống
        { name: 'getDocField', url: '/Common/GetDocField' },

        { name: 'getSendTypes', url: '/Common/GetSendTypes' },

        { name: 'getDeptAndUsers', url: '/Common/GetDeptAndUsers' },

        // Lấy danh sách bảng mã.
        { name: 'GetCodes', url: '/Common/GetCodes', type: 'Get' },

        // Lấy danh sách cơ quan ban hành của đơn vị hiện tại.
        { name: 'getOrganizations', url: '/Common/GetOrganizations', type: 'Get' },

        //#endregion

        //#region Văn bản

        //Sửa văn bản
        { name: 'editNew', url: '/Document/EditNew/', type: 'Get' },

        // Lấy nội dung form
        { name: 'getFormContent', url: '/Document/GetFormContent' },

        // Lấy nội dung form
        { name: 'getFormUrl', url: '/Document/GetFormUrl' },

        // Xác nhận bàn giao
        { name: 'confirmTransfer', url: '/Document/ConfirmTransfer', type: 'Post', hasToken: true },

        // Xác nhận xử lý
        { name: 'confirmProcess', url: '/Document/ConfirmProcess', type: 'Post', hasToken: true },

        // Lấy danh sách văn bản liên quan
        { name: 'getDocumentRelations', url: '/document/GetDocumentRelations', type: 'Get' },

        // Gia hạn xử lý hồ sơ
        { name: 'renewals', url: '/Document/Renewals', type: 'Post' },

        // Lấy quyền thao tác xử lý văn bản hồ sơ
        { name: 'getDocumentPermission', url: '/Document/GetDocumentPermission', traditional: true, async: true },

        // Sửa văn bản
        { name: 'getDocumentInfoForEdit', url: '/Document/GetDocumentDetail' },

        // Sửa văn bản
        { name: 'getMultiDocument', url: '/Document/GetDocumentDetails', traditional: true, },

        // Mở văn bản mobile
        { name: 'getDocumentInfoForMobile', url: '/Document/getDocumentInfoForMobile' },

         // Tạo văn bản mobile
        { name: 'getDocumentInfoForCreateMobile', url: '/Document/GetDocumentInfoForCreateMobile' },

        // Tạo văn bản
        { name: 'getDocumentInfoForCreate', url: '/Document/GetDocumentInfoForCreate' },

        // Lấy lại văn bản ở các node
        { name: 'getContextItemForUndoTransfering', url: '/Document/GetContextItemForUndoTransfering/' },

        // Xóa văn bản/hồ sơ
        { name: 'removeDocument', url: '/Document/RemoveDocument', type: 'Post', hasToken: true, traditional: true, },

        // Lấy lại văn bản
        { name: 'undoTransfering', url: '/Document/UndoTransfering', type: 'Post', hasToken: true },

        // Lấy những người nhận văn bản để undo lại
        { name: 'getUsersForUndoTransfering', url: '/Document/GetUsersForUndoTransfering' },

        // Set trang thái đọc văn bản
        { name: 'setDocumentViewed', url: '/Document/SetViewed/', type: 'Post' },

        // Sửa hồ sơ đăng ký trực tuyến
        { name: 'editDocumentOnline', url: '/Document/EditDocumentOnline/' },

        // Sửa nội dung hồ sơ
        { name: 'editDocumentContent', url: '/Document/EditContent/', type: 'Post' },

        // Trả về các phiên bản của nội dung văn bản
        { name: 'getDocumentContentVersion', url: '/Document/getDocumentContentVersion', type: 'Get' },

        // Lay y kien thuong dung cua nguoi dung
        { name: 'getCommonComments', url: '/Document/GetCommonComments/' },

        // Lay y kien thuong dung cua nguoi dung
        { name: 'getTemplateComments', url: '/Document/GetTemplateComments/' },

        { name: 'createTemplateComments', url: '/Document/CreateTemplateComments/', type: 'Post' },

        //Cap nhat mẫu template ý kiến mà người dùng đã soạn mẫu trước
        { name: 'updateTemplateComments', url: '/Document/UpdateTemplateComments', type: 'post' },

        //Tao moi mẫu template ý kiến mà người dùng đã soạn mẫu trước
        { name: 'deleteTemplateComments', url: '/Document/DeleteTemplateComments', type: 'post' },

        // Tim kiem van ban
        { name: 'searchDocuments', url: '/Document/SearchDocuments', type: 'Post', traditional: true },

        // Thiết lập văn bản quan trong hay không quan trọng
        { name: 'setDocumentImportant', url: '/Document/SetDocumentImportant', type: 'Post' },

        // Xem trước thông tin văn bản hồ sơ
        { name: 'quickViewDocument', url: '/Home/QuickViewDocument', type: 'Get' },

        // Lấy file cấu hình form văn bản
        { name: 'getDocumentTemplate', url: '/Document/GetDocumentTemplate', type: 'Get' },

        
        // Lấy file cấu hình form văn bản
        { name: 'getCatalog', url: '/Document/GetCatalog', type: 'Get' },

        // Cập nhật kết quả xử lý cuối cùng
        { name: 'updateLastResult', url: '/Document/UpdateLastResult', type: 'Post' },

        // Tạo ảnh từ file PDf, trả về url ảnh
        { name: 'createImagesFromBeginAndLastPdfPages', url: '/Document/CreateImagesFromBeginAndLastPdfPages', type: 'Get' },

        { name: 'GetDocumentInfoFromScan', url: '/Parallel/GetDocumentInfoFromScan', type: 'Get', traditional: true },

         // trả về url ảnh
        { name: 'getImageTemp', url: '/Document/GetImageTemp', type: 'Get' },

        // Hủy số đã cấp
        { name: 'cancelCode', url: '/Document/CancelCode', type: 'Get' },

        // Lấy danh sách số ký hiêu, mã hồ sơ
        { name: 'GetDocCodes', url: '/Document/GetDocCodes', type: 'Get' },

        // Lấy danh sách số đến đi
        { name: 'GetInOutCode', url: '/Document/GetInOutCode', type: 'Get' },

        // Lấy trạng thái phát hành
        { name: 'GetIsTransferPublish', url: '/Document/GetIsTransferPublish', type: 'Get' },

        // Xóa doc paper
        { name: 'deleteDocPaper', url: '/Document/DeleteDocPaper', type: 'Post' },

        // Xóa doc fee
        { name: 'deleteDocFee', url: '/Document/DeleteDocFee', type: 'Post' },

        // Thu hồi văn bản
        { name: 'acceptThuHoi', url: '/Document/AcceptThuHoi', type: 'Post' },

        // Lấy loại hồ sơ, văn bản
        { name: 'getDoctype', url: '/Doctype/GetDocType' },

        //Lay loai van ban
        { name: 'getDocTypes', url: '/Doctype/GetDocTypes' },

        // Trả về giấy tờ và lệ phí của doctype
        { name: 'getDoctypePaperAndFees', url: '/Doctype/GetPaperAndFees' },

        // Cập nhật giấy tờ và lệ phí của loại hồ sơ
        { name: 'updateDoctypePaperAndFees', url: '/Doctype/UpdatePaperAndFees', type: 'Post' },

        // Xóa doctype paper
        { name: 'deleteDoctypePaper', url: '/Document/DeleteDoctypePaper', type: 'Post' },

        // Xóa doctype paper
        { name: 'deleteDoctypeFee', url: '/Document/DeleteDoctypeFee', type: 'Post' },

        // Kiểm tra số ký hiệu đã được dùng
        { name: 'checkDocCodeIsUsed', url: '/Document/CheckDocCodeIsUsed' },

        //#endregion

         //#region Mission (Tạo nhiệm vụ)

        // Lấy thông tin user
        { name: 'GetListUser', url: '/Mission/GetListUser', type: 'Get' },

        // Tạo nhiệm vụ
        { name: 'CreateMission', url: '/Mission/CreateMission', type: 'post', traditional: true, async: true },

        // Lấy thông tin user
        { name: 'LinkDetailMission', url: '/Mission/LinkDetailMission', type: 'post', traditional: true, async: true },

         //#endregion Mission (Tạo nhiệm vụ)

        //#region Đính kèm

        // Tải về tệp đính kèm mới tải lên.
        { name: 'downloadAttachmentTemp', url: '/Attachment/DownloadAttachmentTempBase64', type: 'Get' },

        // Tải về tệp đính kèm có sẵn
        { name: 'downloadAttachment', url: '/Attachment/DownloadAttachmentBase64', type: 'Get' },

        // Tải về tệp đính kèm để ký
        { name: 'downloadAttachmentForSignBase64', url: '/Attachment/DownloadAttachmentForSignBase64', type: 'Get', traditional: true },

        //Upload file scan
        { name: 'uploadTempScan', url: '/Attachment/UploadTempScan', type: 'Post' },

        //#endregion

        //#region Workflow

         // Lấy danh sách các hướng chuyển với văn bản đang edit
        { name: 'getAction', url: '/Workflow/GetActions', type: 'Get' },
        // Lấy danh sách các hướng chuyển với văn bản đang edit
        { name: 'getDocumentEditAction', url: '/Workflow/GetActionsEdit', type: 'Get' },

        // Lấy danh sách các hướng chuyển với văn bản tạo mới
        { name: 'getDocumentCreateAction', url: '/Workflow/GetActionsCreate', type: 'Get' },

        // Lấy danh sách người nhận theo hướng chuyển
        { name: 'getUserByAction', url: '/Workflow/GetUserByAction', type: 'Get' },

        // Lấy các hướng chuyển của người dùng theo duwj kieen
        { name: 'getActionsTransferPlan', url: '/Workflow/GetActionsTransferPlan', type: 'Get' },

         // Lấy danh sách người nhận theo hướng chuyển theo lo
        { name: 'getUserByActionTheoLo', url: '/Workflow/GetUserByActionTheoLo', type: 'post', traditional: true },

        // Lấy danh sách hướng chuyển theo lo
        { name: 'getActionTheoLoVanBan', url: '/Workflow/GetActionTheoLoVanBan', type: 'post', traditional: true, async: true },

        //#endregion

        //#region Xử lý văn bản

        // Phat hang va ket thuc
        { name: 'publishAndFinish', url: '/Transfer/PublishAndFinish', type: 'Post', traditional: true, hasToken: true },
        // Chuyển văn bản
        { name: 'transfer', url: '/Transfer/TransferDocument', type: 'Post', traditional: true, hasToken: true },
        //phát hành phiếu khảo sát
        { name: 'surveyRelease', url: '/Transfer/SurveyReleased', type: 'Post', traditional: true, hasToken: true },
        // hoan thành phiếu khảo sát
        { name: 'surveyComplete', url: '/Transfer/SurveyComplete', type: 'Post', traditional: true, hasToken: true },
        // Chỉnh sửa cấu hình báo cáo
        { name: 'surveySaveReport', url: '/Transfer/SurveySaveReport', type: 'Post', traditional: true, hasToken: true },
        // Chuyen theo lo
        { name: 'transferTheoLo', url: '/Transfer/TransferMultiple', type: 'Post', traditional: true, hasToken: true },

        // Chuyển văn bản
        { name: 'lightTransfer', url: '/Transfer/LightTransfer', type: 'Post' },

        // Chuyển ý kiến đóng góp: cho văn bản xin ý kiến, đồng xử lý.
        { name: 'TransferYKienDongGop', url: '/Transfer/TransferAnswer', type: 'Post', traditional: true, hasToken: true },

        // Tiếp nhận hồ sơ.
        { name: 'TransferTiepNhan', url: '/Transfer/TransferTiepNhan', type: 'Post', traditional: true, hasToken: true },

        // Thông báo
        { name: 'TransferAnnouncement', url: '/Transfer/TransferAnnouncement', type: 'Post', traditional: true, hasToken: true },

        // Xin ý kiến
        { name: 'TransferConsult', url: '/Transfer/TransferConsult', type: 'Post', traditional: true, hasToken: true },

        // Phát hành báo cáo lên VP chính phủ
        { name: 'publishGov', url: '/Publish/TransferPublishGov', type: 'Post', traditional: true, hasToken: true },

        // Lưu sổ và phát hành văn bản
        { name: 'publish', url: '/Publish/TransferPublish', type: 'Post', traditional: true, hasToken: true },

        // Lưu sổ và phát hành nội bộ
        { name: 'privatePublish', url: '/Publish/TransferPrivatePublish', type: 'Post', traditional: true, hasToken: true },

        // Lưu sổ và phát hành nội bộ theo lô
        { name: 'privatePublishTheoLo', url: '/Publish/TransferPrivatePublishTheoLo', type: 'Post', traditional: true, },

        // Lưu sổ và phát hành văn bản theo lô
        { name: 'publishTheoLo', url: '/Publish/TransferPublishTheoLo', type: 'Post', traditional: true, hasToken: true },

        // Dự kiến phát hành
        { name: 'publishmentPlan', url: '/Publish/PublishmentPlan', type: 'Post' },

        // Phát hành tiếp
        { name: 'rePublish', url: '/Publish/RePublish', type: 'Post', traditional: true },

        // Cập nhật văn bản
        { name: 'saveDoc', url: '/Transfer/SaveDoc', type: 'Post', hasToken: true, traditional: true },

        // Lưu văn bản dự thảo
        { name: 'saveDocDraft', url: '/Transfer/SaveDocDraft', type: 'Post', hasToken: true },

        // Lưu văn bản dự thảo
        { name: 'transferLienThong', url: '/Publish/transferLienThong', type: 'Post', hasToken: true, traditional: true },

        // Thu hồi văn bản liên thông
        { name: 'recalledLienThong', url: '/Publish/RecalledLienThong', type: 'Post', hasToken: true, traditional: true },

        // Gửi liên thông lại
        { name: 'resendLienThong', url: '/Transfer/ResendLienThong', type: 'Post', hasToken: true, traditional: true },

        // Gửi ý kiến
        { name: 'sendComment', url: '/Document/SendComment', type: 'Post', hasToken: true },

         // Đính chính văn bản
        { name: 'dinhchinh', url: '/Finish/DinhChinh', type: 'Post' },
         // Kết thúc xử lý văn bản
        { name: 'fiAndPub', url: '/Finish/FinishAndPublish', type: 'Post', hasToken: true },

        // Kết thúc xử lý văn bản
        { name: 'finish', url: '/Finish/UpdateFinish', type: 'Post', hasToken: true },

         // Lấy lại văn bản đã kết thúc
        { name: 'undoFinish', url: '/Finish/UndoFinish', type: 'Post' },

        // Ký duyệt
        { name: 'approverSend', url: '/Approver/Send', type: 'Post', hasToken: true },

        { name: 'deleteApprover', url: '/Approver/deleteApprover', type: 'Post', hasToken: true },

        { name: 'deleteResult', url: '/Document/DeleteResult', type: 'Post', hasToken: true },

        //#endregion

        //#region hỏi đáp

        { name: 'getNodeQuestion', url: '/Question/GetNode', type: 'Get' },

        { name: 'getsQuestion', url: '/Question/GetQuestions', type: 'Get' },

        { name: 'answerQuestion', url: '/Question/Answer', type: 'POST' },

        { name: 'rejectQuestion', url: '/Question/Reject', type: 'POST' },

        { name: 'forwardQuestion', url: '/Question/ForwardQuestion', type: 'POST' },

        { name: 'rejectAnswer', url: '/Question/RejectAnswer', type: 'POST' },

        { name: 'getForwardList', url: '/Question/GetForwardList', type: 'Get' },

        { name: 'getsHolderList', url: '/Question/GetsHolderList', type: 'Get' },

        //#endregion

        //#region Hồ sơ cá nhân

        // Lấy danh sách Sổ văn bản
        { name: 'GetStores', url: '/Common/GetStores', type: 'Get' },

        // Lấy danh sách hồ sơ cá nhân, hồ sơ chia sẻ
        { name: 'getStorePrivate', url: '/StorePrivate/Gets', type: 'Get' },

        // Tạo mới hồ sơ cá nhân,hồ sơ chia sẻ
        { name: 'createStorePrivate', url: '/StorePrivate/Create', type: 'Post', hasToken: true, traditional: true },

        // Cập nhật hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'updateStorePrivate', url: '/StorePrivate/Update', type: 'Post', hasToken: true, traditional: true },

        { name: 'anycStoreShare', url: '/StorePrivate/AnycStoreShare', type: 'Get' },

        // Mở hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'openStorePrivate', url: '/StorePrivate/Open', type: 'Post', hasToken: true },

        // Đóng hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'closeStorePrivate', url: '/StorePrivate/Close', type: 'Post', hasToken: true },

        // Xóa hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'deleteStorePrivate', url: '/StorePrivate/Delete', type: 'Post', hasToken: true },

        // Lấy danh sách văn bản hồ sơ trong hồ sơ cá nhân, hồ sơ chia sẻ
        { name: 'getStorePrivateDocuments', url: '/StorePrivate/GetDocuments', type: 'Post' },

        // Xóa văn bản ra khỏi hồ sơ
        { name: 'removeStorePrivateDocument', url: '/StorePrivate/RemoveDocuments', type: 'Post', traditional: true },

        //Lấy danh sách người dùng trong hồ sơ cá nhân. hồ sơ chia sẻ
        { name: 'getUserJoined', url: '/StorePrivate/GetUserJoined', type: 'Get' },

        //Thêm văn bản vào hồ sơ cá nhân/chia sẻ
        { name: 'SaveDocumentToStorePrivate', url: '/Document/SaveDocumentToStorePrivate', type: 'Post', hasToken: true },

        // Mở file từ hồ sơ cá nhân, hồ sơ chia sẻ
        { name: 'storePrivateOpenFile', url: '/StorePrivate/DownloadAttachmentBase64', type: 'Get' },

        // Xoá file trong hồ sơ
        { name: 'storePrivateRemoveFile', url: '/StorePrivate/RemoveAttachment', type: 'post', hasToken: true },

        // Loại hồ sơ khỏi hồ sơ thông báo
        { name: 'removeDocumentAnnouncement', url: '/Document/RemoveDocumentAnnouncement', type: 'post' },

        //#endregion

        //#region Danh sách văn bản

        // Lấy danh sách văn bản theo kho
        { name: 'getDocumentStore', url: '/Home/GetDocumentStore' },

        //#endregion

        //#region Cây văn bản

        //Lấy danh sách cây văn bản
        { name: 'getDocumentTree', url: '/Home/GetFunctionByParentId', type: 'get' },

        //Lấy danh sách cây văn bản có hướng chuyển theo lô
        { name: 'getDocumentTreeHasTransferTheoLo', url: '/Home/GetFunctionHasTransferTheoLoByParentId', type: 'get' },

        // Lấy danh sách cây văn bản
        { name: 'syncDocumentStore', url: '/Home/SyncDocumentStore' },

        // Lấy danh sách các kho văn bản
        { name: 'getFunctionGroups', url: '/Home/GetFunctionGroups' },

        //#endregion

        //#region Home

        // Lấy các cấu hình của người dùng
        { name: 'getCommonConfigs', url: '/Home/GetCommonConfigs', type: 'Get' },

        // Lấy các cấu hình bỏ lưu sổ
        { name: 'hasHideSaveStore', url: '/Home/HasHideSaveStore', type: 'Post' },

        // Lấy ngày hết hạn
        { name: 'getDateAppointed', url: '/Document/GetDateAppointed', type: 'Post' },

        // Thiết lập các config của người dùng
        { name: 'setUserConfig', url: '/Account/SetUserConfig/', type: 'Post' },

        // Thiết lập các config của người dùng
        { name: 'setPopUpSize', url: '/Account/setPopUpSize/', type: 'Post' },

        { name: 'filterCitizen', url: '/Document/FilterCitizen/', type: 'Get' },

        // Trang in
        { name: 'print', url: '/Print/Index', type: 'Get' },

        { name: 'previewPrint', url: '/Print/PreviewPrint', type: 'Get' },

        // Trả về danh sách các phiếu in của hồ sơ 
        { name: 'getPrints', url: '/Print/GetPrints', type: 'Get' },

        // Trả về danh sách các mẫu phiếu in theo nghiệp vụ
        { name: 'getPrintTemplates', url: '/Print/GetPrintTemplates', type: 'Get' },

        // Trả về danh sách các mẫu phiếu in theo danh sách hồ sơ
        { name: 'getPrintByDocCopys', url: '/Print/GetPrintByDocCopys', type: 'Get' },

        // In phiếu biên nhận
        { name: 'quickPrint', url: '/Print/QuickPrint', type: 'Get' },

        { name: 'getPrinters', url: '/Print/GetActivePrinters', type: 'Get' },

        { name: 'printTransferHistory', url: '/Print/PrintTransferHistory', type: 'Get' },

        // Trả về dữ liệu cho form trả kết quả
        { name: 'getReturnResult', url: '/Return/GetReturnResult', type: 'Get' },

        // Trả về dữ liệu cho form trả kết quả
        { name: 'updateReturn', url: '/Return/UpdateReturn', type: 'Post' },

        // Form yêu cầu bổ sung mới
        { name: 'createSupplementary', url: '/Supplementary/CreateSupplementary', type: 'Get' },

        // Tiếp nhận bổ sung
        { name: 'receiveSupplementary', url: '/Supplementary/GetDetails', type: 'Get' },

        // Tiếp nhận bổ sung - Posts
        { name: 'supplementaryReceive', url: '/Supplementary/Receive', type: 'Post' },

        // Trả về ngày hẹn trả mới khi tiếp nhận bổ sung
        { name: 'getNewDateAppointed', url: '/Supplementary/GetDateAppointed', type: 'Get' },

        // Tạo yêu cầu bổ sung hồ sơ
        { name: 'sendRequiredSupplementary', url: '/Supplementary/SendRequire', type: 'Post', hasToken: true },

        // Hủy yêu cầu bổ sung
        { name: 'cancelReceiveSupplementary', url: '/Supplementary/CancelReceive', type: 'Post', hasToken: true },

        // Tiếp tục xử lý
        { name: 'continueProcess', url: '/Supplementary/continueProcess', type: 'Post' },

        //Tìm kiếm nhanh
        { name: 'quickSearch', url: '/Search/QuickSearch', type: 'Get', traditional: true },

        { name: 'getMailTemplates', url: '/Document/GetMailTemplates', type: 'Get', },

        { name: 'getSmsTemplates', url: '/Document/GetSmsTemplates', type: 'Get', },

        { name: 'sendMailToPeople', url: '/Document/SendMailToPeople', type: 'Post', },

        { name: 'sendSmsToPeople', url: '/Document/SendSmsToPeople', type: 'Post', },

        { name: 'getVersionValue', url: '/Home/GetVersionValue', type: 'Get' },


        //#endregion

        //#region Khác - kiểm tra lại nếu không dùng thì bỏ

        // Lấy ra tổng số các văn bản hồ sơ chưa đọc
        { name: 'getTotalDocumentUnreadMultiFunction', url: '/Parallel/GetTotalDocumentUnreadMultiFunction', type: 'Post' },

        { name: 'getTotalDocumentUnread', url: '/Home/GetTotalDocumentUnread', type: 'Post' },

        // Lấy danh sách hồ sơ, văn bản
        { name: 'getDocuments', url: '/home/GetDocuments', type: 'Post' },

        // Lấy toàn bộ danh sách văn bản của node hiện tại
        { name: 'getAllDocument', url: '/Home/GetAllDocument', type: 'Post' },

        // Lấy danh sách hồ sơ văn bản theo phân trang
        { name: 'getDocumentPaging', url: '/Home/GetDocumentPaging', type: 'Post' },

        // Lấy danh sách hồ sơ, văn bản mới được thêm vào node và những văn bản xóa khỏi node
        { name: 'getLastestDocuments', url: '/Home/GetLastestDocuments', type: 'Post', traditional: true },

          // Lấy danh sách hồ sơ, văn bản mới được thêm vào node và những văn bản xóa khỏi node
        { name: 'getLastestReports', url: '/HomeSMReport/GetLastestDocuments', type: 'Post', traditional: true },

         { name: 'getReports', url: '/HomeSMReport/GetDocuments', type: 'Post', traditional: true },

        //Tìm kiếm nâng cao
        { name: 'searchAdvance', url: '/Search/SearchAdvance', type: 'Get', traditional: true },

        //lấy form tìm kiếm nâng cao
        { name: 'getSearchAdvanceForm', url: '/Search/GetSearchAdvanceForm', type: 'Get', },

        //lấy form tìm kiếm nâng cao
        { name: 'getDiffVersionTrees', url: '/Home/DiffVersionTree', type: 'Get', },

        //#endregion

        //#region Đăng ký qua mạng

        //tiep nhan
        { name: 'acceptOnline', url: '/DocumentOnline/AcceptOnline', type: 'Post' },

        //tu choi
        { name: 'rejectOnline', url: '/DocumentOnline/RejectOnline', type: 'Post' },

        //tu choi
        { name: 'onlineSupplementary', url: '/DocumentOnline/OnlineSupplementary', type: 'Post' },

        { name: 'additionalRequirements', url: '/DocumentOnline/AdditionalRequirements', type: 'Post' },

        //Tong so van ban dang ky qua mang
        { name: 'getTotalOnlineRegistration', url: '/DocumentOnline/GetTotalOnlineRegistration', type: 'Get' },

         //Danh sach van ban dang ky qua mang
        { name: 'getDocumentOnlineRegistration', url: '/DocumentOnline/GetDocumentOnlineRegistration', type: 'Get' },

        //Tong so van ban dang ky qua mang bị hủy bỏ
        { name: 'getTotalOnlineCancel', url: '/DocumentOnline/GetTotalOnlineCancel', type: 'Get' },

         //Danh sach van ban dang ky qua mang bị hủy bỏ
        { name: 'getDocumentOnlineCancel', url: '/DocumentOnline/GetDocumentOnlineCancel', type: 'Get' },

        //chi tiet hos dang ky qua mang
        { name: 'getDocumentDetailOnlineRegistration', url: '/DocumentOnline/GetDocumentDetailOnlineRegistration', type: 'Get' },

        //Kiểm tra hồ sơ công dân đang đăng ký trực tuyến
        { name: 'checkDocumentOnline', url: '/DocumentOnline/CheckDocumentOnline/', type: 'Get' },

        //Kiểm tra danh sách hồ sơ đang có của công dân trên hệ thống
        { name: 'checkDocument', url: '/DocumentOnline/CheckDocument/', type: 'Get' },

        //Mở lại công văn kết thúc nhầm
        { name: 'reOpenDocument', url: '/Document/ReOpenDocument', type: 'Get' },

        //Xuat danh sach ra file
        { name: 'exportToFile', url: '/Home/ExportToFile', type: 'Post' },

          //Lấy mẫu phôi của mail, sms
        { name: 'editTemplate', url: '/Document/EditTemplate', type: 'Get' },

        //#endregion

        //#region Mobile

        // Lấy tổng số văn bản thông báo
        { name: 'notificationsCount', url: '/Mobile/GetNotificationsCount', type: 'Get' },

        // Thiết lập các config của người dùng cho Mobile
        { name: 'setMobileUserConfig', url: '/Account/SetMobileUserConfig/', type: 'Post', hasToken: true },

        //#endregion

        //Lấy Xóa tệp đính kèm
        { name: 'removeAttachment', url: '/Attachment/RemoveAttachment', type: 'Post' },

        // lấy tổng sô câu hỏi chung
        { name: 'getTotalGeneralQuestion', url: '/Question/GetTotal?isGetGeneral=true', type: 'Get' },

        // lấy tổng sô câu hỏi theo hồ sơ
        { name: 'getTotalDocumentQuestion', url: '/Question/GetTotal?isGetGeneral=false', type: 'Get' },

         //#region Giấy phép doanh nghiệp
        { name: 'getBusinessLicense', url: '/BusinessLicense/BusinessLicenses', type: 'Get' },

         //#region Giấy phép doanh nghiệp
        { name: 'removeBusinessLicense', url: '/BusinessLicense/RemoveLicenses', type: 'Post', traditional: true },

           //#region Giấy phép doanh nghiệp
        { name: 'createCitizen', url: '/BusinessLicense/CreateCitizen', type: 'Post', traditional: true },

            //#region Giấy phép doanh nghiệp
        { name: 'createLicense', url: '/BusinessLicense/CreateLicense', type: 'Post', traditional: true },
        //#endregion

        // Tạo mới hóa đơn
        { name: 'createInvoice', url: '/DocumentInvoice/ImportInvoice', type: 'Post' },

        // lấy hóa đơn
        { name: 'getInvoice', url: '/DocumentInvoice/LookupInvoice', type: 'Get' },

        // chi tiết hóa đơn
        { name: 'getDetailInvoice', url: '/DocumentInvoice/DetailInvoice', type: 'Get' },

        // chi tiết hóa đơn
        { name: 'removeInvoice', url: '/DocumentInvoice/RemoveInvoice', type: 'Post' },

        //#region Mobile

        // Lấy tổng số văn bản thông báo
      { name: 'createVote', url: '/Referendum/Vote', type: 'Post', traditional: true },
      { name: 'updateVote', url: '/Referendum/VoteUpdate', type: 'Post', traditional: true },
      { name: 'deleteVote', url: '/Referendum/DeleteVote', type: 'Post', traditional: true },
      // Lấy tổng số các vote cảu người dùng hiện tại
      { name: 'getVotes', url: '/Referendum/GetVotes', type: 'Get' },
      // Lấy tổng số các vote cảu người dùng hiện tại
      { name: 'getVoteDetail', url: '/Referendum/GetVoteDetail', type: 'Get' },
      // DeleteVote
      { name: 'createCommentDiff', url: '/Referendum/CreateCommentDiff', type: 'Post', traditional: true },
      // 
      { name: 'checkVote', url: '/Referendum/CheckVote', type: 'Post', traditional: true },

      // Gui
      { name: 'checkVoteResult', url: '/Referendum/CheckVoteResult', type: 'Post', traditional: true },
        // 
      { name: 'uncheckVote', url: '/Referendum/UncheckVote', type: 'Post', traditional: true },
      { name: 'getUserInfos', url: '/Referendum/GetUserInfos', type: 'get' },
      { name: 'getVoteDetailReload', url: '/Referendum/GetVoteDetailReload', type: 'get' },


        //#endregion
        //#endregion

    ];

    //#endregion

    egov.request = new RequestManager();
})
(this.egov = this.egov || {}, window.Offline);