/*
 * ChromeNativeApp V1.0.2016.0801
 * - Tương thích với BkaveGov_FFPlugin_ChromNativeApp-1.0.0+.exe
 * - Tương thích với BkaveGovExtension-4.6+
 *  
 * Lịch sử:
 * 
 * ChromeNativeApp V1.0.0
 * - Tương thích với BkaveGov_FFPlugin_ChromNativeApp-1.0.0.exe 
 * - Tương thích với BkaveGovExtension-4.6+
 * 
 */
function ChromeNativeApp() {
    Base.call(this);
}
ChromeNativeApp.version = "1.0.2016.0801";
ChromeNativeApp.prototype.version = ChromeNativeApp.version;

ChromeNativeApp.prototype = new Base();
ChromeNativeApp.prototype.constructor = ChromeNativeApp;


//ChromeNativeApp.PREFIX_RESP_EVENT = "Resp_";
ChromeNativeApp.EXTENSION_ID = "cngndmpehahbhkdhihanbpobckmkdkkk";
ChromeNativeApp.EXTENSION_URL = "";

ChromeNativeApp.Egov_Event = {
    // Xử lý file: đọc, ghi, mở, đóng, lưu file. Xử lý folder: xóa folder...
    GetMd5: "Egov_GetMd5",
    OpenFile: "Egov_OpenFile",
    CloseFile: "Egov_CloseFile",
    WriteFileBase64: "Egov_WriteFileBase64",
    ReadFileBase64: "Egov_ReadFileBase64",

    DeleteFile: "Egov_DeleteFile",
    DeleteFolder: "Egov_DeleteFolder",

    // Ký điện tử
    GetCertIndex: "Egov_GetCertIndex",
    SignFile: "Egov_SignFile",
    SignFileByPoint: "Egov_SignFileByPoint",

    HasAppendModeVersion: "HasAppendModeVersion",

    // Scan văn bản
    ScanFile: "Egov_ScanFile",
    TransferPdf: "Egov_TransferPdf",
    TransferDoc: "Egov_TransferDoc",
    CancelTransferImage: "Egov_CancelTransferImage",
    IsWordInstalled: "Egov_IsWordInstalled",
    IsChangeContent: "Egov_isChangeContent",
    GetAllScanner: "Egov_GetAllScanner",
    ImageCrop: "Egov_ImageCrop",
    ReadFileScanBase64: "Egov_ReadFileScanBase64",
    ImageRotate: "Egov_ImageRotate",
    ImageAdjustBrightness: "Egov_ImageAdjustBrightness",
    ImageAdjustContrast: "Egov_ImageAdjustContrast",
    TransferImage: "Egov_TransferImage",
    Acquire: "Egov_Acquire",
    GetTempFolder: "Egov_GetTempFolder",
    IsExistExtension: "IsExistExtension",

    Convert: "Egov_Convert",

    GetMAC: "Egov_GetMAC",

    OpenUrl: "Egov_OpenUrl"
};

ChromeNativeApp.getInstance = function () {
    if (!this._instance)
        this._instance = new ChromeNativeApp();

    return this._instance;
};

ChromeNativeApp.callbacks = {};
ChromeNativeApp.responseEventReady = false;

ChromeNativeApp.sendToContent = function (req_message, callback) {
    // Khoi tao su kien handle cac message response tu content.js
    if (!ChromeNativeApp.responseEventReady) {
        // Cho vao day de tranh bi chanh bao Cross domain
        var handleResponse = function (e) {
            var resp_message = e.detail;
            var callbackId = resp_message.sender == null ? null : resp_message.sender.callbackid;
            if (!callbackId) {
                return;
                // throw new Exception("Không tìm được callbackid trong thông tin response.");
            }
            var callback = ChromeNativeApp.callbacks[callbackId];
            if (!callback) {
                return;
                // throw new Exception("Không tìm được callback trong ChromeNativeApp.callbacks.");
            }
            delete ChromeNativeApp.callbacks[callbackId];
            if (typeof callback === 'function') {
                callback(resp_message);
                return;
            }

            if (resp_message) {
                return resp_message;
            }
        };

        parent.document.removeEventListener("BkaveGovChromeExtension_Response", handleResponse, false);
        parent.document.addEventListener("BkaveGovChromeExtension_Response", handleResponse);
        ChromeNativeApp.responseEventReady = true;
    }

    // Xu ly callback
    var callbackId = (new Date()).getTime();
    ChromeNativeApp.callbacks[callbackId] = callback;
    req_message.sender = req_message.sender || {};
    req_message.sender.hasAppendModeVersion = ChromeNativeApp.hasAppendMode || egov.hasPluginAppendMode;
    req_message.sender.callbackid = callbackId;

    // Gui message sang content.js
    /*
    * event.initCustomEvent(type, canBubble, cancelable, detail);
    *      Parameters
    *          type: Is a DOMString containing the name of the event.
    *          canBubble: Is a Boolean indicating whether the event bubbles up through the DOM or not.
    *          cancelable: Is a Boolean indicating whether the event is cancelable.
    *          detail: The data passed when initializing the event.
    */
    var request = document.createEvent("CustomEvent");

    // Gọi sang content.js thực hiện yêu cầu qua kích hoạt 1 event
    request.initCustomEvent("BkaveGovChromeExtension_Request", true, true, req_message);
    var evt = parent.document.dispatchEvent(request);
}

ChromeNativeApp.prototype.isReady = function (callback) {
    if (typeof callback === "function") {
        callback(true);
    } else {
        return true;
    }
};

ChromeNativeApp.prototype.isPluginExist = function (callback) {
    var data1 = { "message": "installed" };
    ChromeNativeApp.sendToContent(data1, function (result) {
        callback(result.isInstalled);
    });
};

ChromeNativeApp.prototype.hasAppendWriteMode = function (callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.HasAppendModeVersion, "fileName": "test.txt", "data": "MTIz", "mode": false };
    ChromeNativeApp.sendToContent(data1, function (result) {
        callback(result.hasAppendWriteMode);
    });
};

/************************************\
              ĐỌC/GHI FILE
\************************************/

/**
 * Mở file
 * @public
 * @name    openFile(pathfile, callback)
 * @param   {String}    pathfile    đường dẫn tương đối\\tên file. 
 *          VD: "1467366836294\\Vanban.doc". 
 *          Vị trí lưu file mặc định cua plugin: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov". 
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * @param   {function}  callback    hàm xử lý kết quả trả về từ plugin
 * @return  {Object}    result      kết quả là 1 đối tượng dạng:
 *          {"action":"Egov_OpenFile","returnCode":1,"returnMessage":"Thành công"}";
 *          |-------------|--------------------------------------------------------------------------|
 *          |returnCode   |returnMessage                                                             |
 *	        |-------------|--------------------------------------------------------------------------|
 *          |1            |nếu mở file thành công                                                    |
 *          |2            |nếu file đang được mở. Đồng thời thực hiện focus vào file đang mở này.    |
 *          |3            |nếu file không tồn tại (do chưa được js ghi tạm ra trước đó)              |
 *          |4            |nếu mở file không thành công do nội dung file không đúng định dạng        |
 *          |-1           |nếu mở file lỗi do các nguyên nhân chưa xác định                          |
 *	        |-------------|--------------------------------------------------------------------------|
 */
ChromeNativeApp.prototype.openFile = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.OpenFile, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};

/**
 * Đóng file
 * @public
 * @name    closeFile(pathfile, callback)
 * @param   {String}    pathfile    đường dẫn tương đối\\tên file. 
 *          VD: "1467366836294\\Vanban.doc". 
 *          Vị trí lưu file mặc định cua plugin: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov". 
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * @param   {function}  callback    hàm xử lý kết quả trả về từ plugin
 * @return  {Object}    result      kết quả là 1 đối tượng dạng:
 *          {"action": "Egov_CloseFile", "returnCode": 1, "returnMessage": "Thành công"}";
 *          |-------------|-------------------------------------------------------------------------------------------------------------|
 *          |returnCode   |returnMessage                                                                                                |
 *	        |-------------|-------------------------------------------------------------------------------------------------------------|
 *          |1            |nếu đóng file thành công (đã lưu nội dung file trước khi đóng)                                               |
 *          |2            |nếu file đó đang không được mở (có thể do người dùng đã chủ động lưu và đóng trước đó, hoặc chưa từng mở)    |
 *          |3            |nếu không thể lưu nội dung file trước khi đóng                                                               |
 *          |-1           |nếu mở file lỗi do các nguyên nhân chưa xác định                                                             |
 *	        |-------------|-------------------------------------------------------------------------------------------------------------|
 */
ChromeNativeApp.prototype.closeFile = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.CloseFile, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Ghi file ra ổ cứng, với nội dung file là Base64
 * Đầu vào:
 *       * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *         Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *         Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: "{\"action\":\"Egov_WriteFileBase64\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong đó: 
 *			* returnMessage: thông báo cụ thể các lỗi tương ứng với từng returnCode.
 *			* returnCode là
 *				* 1 nếu ghi file thành công.
 *          	* 2 nếu file đã tồn tại và mode ở trạng thái không yêu cầu ghi đè (mode = false)
 *          	* -1 nếu ghi file lỗi do các nguyên nhân khác (không có quyền ghi, tạo thư mục, tạo file...)
 */
ChromeNativeApp.prototype.writeFileBase64 = function (pathfile, data, mode, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.WriteFileBase64, "fileName": pathfile, "data": data, "mode": mode };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Đọc nội dung file, trả về dạng Base64
 *   Đầu vào:
 *        * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *          Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *   Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: "{\"action\":\"Egov_ReadFileBase64\",\"base64\":\"" + base64 +"\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong đó: 
 *		    * base64: Nội dung base64 của file (đã xử lý lưu nội dung file trước khi đọc nội dung)
 *			* returnMessage: thông báo cụ thể các lỗi tương ứng với từng returnCode.
 *		    * returnCode là
 *			    + 1 nếu lấy nội dung file thành công (sau khi đã lưu nội dung file)
 *			    + 2 nếu file không tồn tại
 *			    + 3 nếu file không thể lưu nội dung trước khi đọc
 *              + -1 nếu lấy nội dung file lỗi do các nguyên nhân khác
 */
ChromeNativeApp.prototype.readFileBase64 = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ReadFileBase64, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};

/* 
 * Trả về trạng thái file tồn tại hay không
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *	    Trả về 1 chuỗi json có dạng: "{\"action\":\"Egov_ExistsFile\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong đó: 
 *			* returnMessage: thông báo cụ thể các lỗi tương ứng với từng returnCode.
 *			* returnCode:
 *				+ 0 nếu file không tồn tại.
 *				+ 1 nếu file tồn tại.
 */
ChromeNativeApp.prototype.existsFile = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ExistsFile, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Trả về chuỗi Md5 của nội dung file, dùng để so sánh xác định nội dung file có thay đổi không
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *      * Trả về 1 chuỗi json có dạng: "{\"action\":\"Egov_GetMd5\",\"md5\":\"" + string(md5,32) +"\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *      * Trong đó: 
 * 	        * md5: là hashmac của file
 *			* returnMessage: thông báo cụ thể các lỗi tương ứng với từng returnCode.
 * 			* returnCode:
 * 				 + 1 nếu lấy hashmac thành công (sau khi đã lưu nội dung file thành công)
 * 				 + 2 nếu file yêu cầu không tồn tại
 * 				 + 3 nếu không thể lưu nội dung file trước khi lấy hashmac
 *               + -1 nếu lấy hashmac không thành công vì các lý do khác
 */
ChromeNativeApp.prototype.getMd5 = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.GetMd5, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Xóa file
 *   Đầu vào:
 *        * pathfile: Đường dẫn tương đối(+tên file) + mode (bắt xóa file). VD: "1467366836294\\Vanban.doc"
 *          Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *   Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: "{\"action\":\"Egov_DeleteFile\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong đó: 
 *			* mode: 
 *				+ true: bắt xóa file
 *				+ false: không bắt xóa file
 *			* returnMessage: thông báo cụ thể các lỗi tương ứng với từng returnCode.
 *		    * returnCode là
 *			    + 1 nếu xóa file thành công
 *						* File đang mở + mode (bắt xóa)
 *						* File xóa.
 *              + 2 nếu file đang mở + mode(không bắt xóa)
 *				+ -1 nếu không thể xóa file do các nguyên nhân khác.
 */
ChromeNativeApp.prototype.deleteFile = function (pathfile, mode, callback) {
    mode = mode || false;
    var data1 = { "action": ChromeNativeApp.Egov_Event.DeleteFile, "fileName": pathfile, "mode": mode };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Xóa folder
 *   Đầu vào:
 *        * pathfile: Đường dẫn tương đối(+tên folder) + mode (bắt xóa folder). VD: "1467366836294\\Thumuc"
 *          Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Thumuc"
 *   Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: "{\"action\":\"Egov_DeleteFolder\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong đó: 
 *			* mode: 
 *				+ true: bắt xóa folder
 *				+ false: không bắt xóa folder
 *			* returnMessage: thông báo cụ thể các lỗi tương ứng với từng returnCode.
 *		    * returnCode là
 *			    + 1 nếu xóa folder thành công
 *						* Folder có file + mode (bắt xóa)
 *						* Folder không có file.
 *              + 2 nếu Folder có file + mode(không bắt xóa)
 *				+ -1 nếu không thể xóa Folder do các nguyên nhân khác.
 */
ChromeNativeApp.prototype.deleteFolder = function (pathfile, mode, callback) {
    mode = mode || false;
    var data1 = { "action": ChromeNativeApp.Egov_Event.DeleteFolder, "fileName": pathfile, "mode": mode };
    return ChromeNativeApp.sendToContent(data1, callback);
};

/************************************\
              KÍ ĐIỆN TỬ
\************************************/

/* 
 * Ký điện tử File
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *		* config: {  			Ext: ".jpg",
								FindText: "DungHA",
								FindType: 1,	// 0: Tren xuong, 1: Duoi len
								ImagePath: "C:\\Users\\Bkav\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\ImageSignatureTemp_0.jpg",
								OffsetX: 0,		// Don vi: px; Can vi tri chen cks so voi vi tri FindText tim duoc
								OffsetY: 0,
								PosType: 1, 	// 0: trai, 1: phai, 2: tren, 3: duoi so voi vi tri FindText tim duoc
								SignType: 0,	// Loai ky: 0: Ky anh, 1: Ky text
								TextInfor: 0,	// 0, 1: Co lay thong tin trong CKS va hien thi ra cung voi anh trong chu ki khong
								Title: "ddd",
								SignAuthor: "ThangLVb",
								SignReason: "Test"
				 }
 *		* idxCert: Index chữ ký chọn để ký
 * Đầu ra: 
 *	    Trả về 1 chuỗi json có dạng: 
 *          "{
 *              \"action\":\"Egov_SignWord\",
 *				\"base64\":\"" + base64	+"\",
 *              \"returnCode\":\"" + returnCode +"\",
 *              \"returnMessage\":\"" + returnMessage +"\"
 *          }";
 *		Trong đó: 
 *			* returnCode:
 *				+ 1 nếu kí điện tử thành công.
 *              + 2 nếu file word cần kí không tồn tại
 *              + 3 nếu cert không tìm thấy (có thể do rút token ra)
 *              + -1 nếu không thể kí điện tử do các nguyên nhân khác
 */
ChromeNativeApp.prototype.signFile = function (filename, config, idxCert, callback) {
    var data1 = {
        "action": ChromeNativeApp.Egov_Event.SignFile,
        "idxCert": idxCert,
        "fileName": filename,
        "config": config
    };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.signFileByPoint = function (filename, config, idxCert, callback) {
    var data1 = {
        "action": ChromeNativeApp.Egov_Event.SignFileByPoint,
        "idxCert": idxCert,
        "fileName": filename,
        "config": config
    };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.getCertIndex = function (callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.GetCertIndex };

    return ChromeNativeApp.sendToContent(data1, callback);
};

/************************************\
            SCAN VAN BAN
\************************************/

ChromeNativeApp.prototype.getTempFolder = function (callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.GetTempFolder };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.transferPdf = function (url, format, callback) {

    var data1 = { "action": ChromeNativeApp.Egov_Event.TransferPdf, 'listpath': url };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.transferDoc = function (url, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.TransferDoc, 'listpath': url };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.cancelTransferImage = function (url) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.CancelTransferImage, "path": url };

    return ChromeNativeApp.sendToContent(data1);
};

ChromeNativeApp.prototype.isWordInstalled = function (callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.isWordInstalled };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.isChangeContent = function (filename, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.IsChangeContent, "filename": filename };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.getAllScanner = function (reload, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.GetAllScanner, "reload": reload };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.acquire = function (showui, sourceIndex, pixelType, resolution, enableDuplex, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.Acquire, "showui": showui, "sourceIndex": sourceIndex, "pixelType": pixelType, "resolution": resolution, "enableDuplex": enableDuplex };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.imageCrop = function (url, x, y, x2, y2, width, height) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ImageCrop, "path": url, "left": x, "top": y, "bottom": x2, "right": y2, "width": width, "height": height };

    return ChromeNativeApp.sendToContent(data1);
};

ChromeNativeApp.prototype.readFileScanBase64 = function (url, start, length, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ReadFileScanBase64, "start": start, "length": length, "path": url };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.imageRotate = function (url, angle, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ImageRotate, "path": url, "angle": angle };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.imageAdjustBrightness = function (url, value, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ImageAdjustBrightness, "path": url, "percentage": value };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.imageAdjustContrast = function (url, value, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ImageAdjustContrast, "path": url, "percentage": value };

    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.transferImage = function (url, format) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.TransferImage, "path": url, "imageformat": format };

    return ChromeNativeApp.sendToContent(data1);
};

ChromeNativeApp.prototype.openURL = function (url) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.OpenUrl, "url": url };
    return ChromeNativeApp.sendToContent(data1);
};

ChromeNativeApp.prototype.getMAC = function (callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.GetMAC };
    return ChromeNativeApp.sendToContent(data1, callback);
};

ChromeNativeApp.prototype.convertToPdf = function (files, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.Convert, "fileType": 35, "fileNameArr": files };
    return ChromeNativeApp.sendToContent(data1, callback);
};