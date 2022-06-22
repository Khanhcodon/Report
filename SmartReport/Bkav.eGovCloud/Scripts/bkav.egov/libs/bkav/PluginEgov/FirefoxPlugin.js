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
// TODO: Thêm cảnh báo chức năng kí chưa chạy được trên windows 10 với Bkav token manager V 2.8.0.8 trở về trươc
function FirefoxPlugin() {

}

FirefoxPlugin.version = "1.0.2016.0801";
// ChromeNativeApp.prototype.version = ChromeNativeApp.version;

FirefoxPlugin.prototype = new Base();

FirefoxPlugin.prototype.constructor = FirefoxPlugin;

var firefoxPluginObject = null;

FirefoxPlugin.getInstance = function () {
    if (!this._instance)
        this._instance = new FirefoxPlugin();
    return this._instance;
};

/*
 * Trả về trạng thái đã sẵn sàng để sử dụng của plugin firefox
 * Plugin sẵn sàng khi:
 * - Đã cài đặt plugin vào trình duyệt firefox
 * - Đã injectPlugin vào web eGov để js gọi được tới plugin và sử dụng
 */
FirefoxPlugin.prototype.isReady = function (callback) {
    var isReady = this.isPluginExist() && firefoxPluginObject != undefined;
    if (typeof callback === "function") {
        callback(isReady);
    } else {
        return isReady;
    }
};

/*
 * Trả về trạng thái plugin firefox đã được cài đặt hay chưa
 * - Nếu đã cài đặt: dùng "injectPlugin" để chèn thẻ <object... để sử dụng plugin trong eGov:
 *      <object id="egovPlugin" width="0px" height="0px" type="application/x-eofficeplus">
 *          <param value="__pluginCB" name="onload">
 *      </object>
 * - Nếu chưa cài đặt: thực hiện quy trình yêu cầu cài đặt
 */
FirefoxPlugin.prototype.isPluginExist = function (callback) {
    try {
        var result = FireBreath.isPluginInstalled("eOfficePlus");
        //console.log(result)
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "isPluginExist", "returnCode": "-1" }); }
    }
};

/*
 * Chèn <object... trỏ tới plugin để javascript có thể sử dụng trong eGov
 */
FirefoxPlugin.prototype.injectPlugin = function (id, callback, params) {
    // Tạo Dom trung gian div để add object plugin
    /* var parser = new DOMParser();
     var pluginObj = parser.parseFromString("<div id='egov_plugin_id'></div>", "text/xml");
     var obj = pluginObj.firstChild;    
     document.body.appendChild(obj);
     */

    var divPlugin = document.createElement('div');
    divPlugin.setAttribute("id", "egov_plugin_id");
    document.body.appendChild(divPlugin);

    var pluginHtml = FireBreath.injectPlugin(document.getElementById("egov_plugin_id"), "eOfficePlus", id, function () {
        firefoxPluginObject = document.getElementById(id);
        callback();
    }, params);
    // Add object plugin
    //obj.innerHTML = pluginHtml;
};

/************************************\
              ĐỌC/GHI FILE
\************************************/

// TODO: Bổ sung tính năng đẩy các file đang sửa mà chưa bàn giao đi ngay lên server lưu tạm. Giúp lần sau có thể tiếp tục sửa và chuyển đi sau.
//      Kịch bản sơ bộ: 
//      + Đóng văn bản => Hỏi lưu tạm bản đang sửa lần sau sửa tiếp?
//      + Lần mở sau, danh sách file báo trạng thái có file đang sửa tạm. 
//        => Click đúp vào mở file thì báo sửa tiếp bản đang sửa trước? 
//        => Đồng ý thì mở file lưu tạm này, ngược lại thì mở file gốc.
//        => Lúc bàn giao thì xử lý file tạm đó như với mở file gốc thông thường...

// TODO: Bổ sung tính năng xóa toàn bộ dữ liệu trong thư mục tạm đi mỗi khi lúc truy cập/thoát eGov
//       hoặc xóa các file tạm liên quan khi đóng tab văn bản thì 


/* 
 * ThangLVb - Fix lại các hàm đóng mở file
 * pathfile là đường dẫn file. Các file sẽ được lưu vào thư mục "C:\\Users\\Bkav\\AppData\\Local\\Temp\\BkavEgov"
 * Ví dụ: pathfile = "C:\\Users\\Bkav\\AppData\\Local\\Temp\\BkavEgov\\vidu.doc";
 */

/* 
 * Trả về chuỗi Md5 của nội dung file, dùng để so sánh xác định nội dung file có thay đổi không
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *      * Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_GetMd5,
 *              md5: string(md5,32),
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
 *      * Trong đó: 
 * 	        * md5: là hashmac của file
 * 			* returnCode:
 * 				 + 1 nếu lấy hashmac thành công (sau khi đã lưu nội dung file thành công)
 * 				 + 2 nếu file yêu cầu không tồn tại
 * 				 + 3 nếu không thể lưu nội dung file trước khi lấy hashmac
 *               + -1 nếu lấy hashmac không thành công vì các lý do khác
 */
FirefoxPlugin.prototype.getMd5 = function (pathfile, callback) {
    try {
        var result = firefoxPluginObject.getMd5(pathfile);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_OpenFile", "returnCode": "-1" }); }
    }
};

/* 
 * Mở file yêu cầu
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *      Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_OpenFile,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
 *	    Trong đó: 
 *          * returnCode:
 *	            + 1 nếu mở file thành công
 *              + 2 nếu file đang được mở. Đồng thời thực hiện focus vào file đang mở này.
 *              + 3 nếu file không tồn tại (do chưa được js ghi tạm ra trước đó)
 *              + 4 nếu mở file không thành công do nội dung file không đúng định dạng
 *              + -1 nếu mở file lỗi do các nguyên nhân chưa xác định
 */
FirefoxPlugin.prototype.openFile = function (pathfile, callback) {
    try {
        var result = firefoxPluginObject.openFile(pathfile);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_OpenFile", "returnCode": "-1" }); }
    }
};

/* 
 * Đóng file yêu cầu
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_CloseFile,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
 *		Trong đó: 
 *          * returnCode:
 *		        + 1 nếu đóng file thành công (đã lưu nội dung file trước khi đóng)
 *              + 2 nếu file đó đang không được mở (có thể do người dùng đã chủ động lưu và đóng trước đó, hoặc chưa từng mở)
 *              + 3 nếu không thể lưu nội dung file trước khi đóng
 *              + -1 nếu đóng file lỗi do các nguyên nhân khác
 */
FirefoxPlugin.prototype.closeFile = function (pathfile, callback) {
    try {
        var result = firefoxPluginObject.closeFile(pathfile);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_CloseFile", "returnCode": "-1" }); }
    }
};

/* 
 * Ghi file ra ổ cứng, với nội dung file là Base64
 * Đầu vào:
 *       * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *         Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *         Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_WriteFileBase64,,
 *              md5: string(md5,32),
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
 *		Trong đó: returnCode là
 *			* 1 nếu ghi file thành công.
 *          * 2 nếu file đã tồn tại và mode ở trạng thái không yêu cầu ghi đè (mode = false)
 *          * -1 nếu ghi file lỗi do các nguyên nhân khác (không có quyền ghi, tạo thư mục, tạo file...)
 */
FirefoxPlugin.prototype.writeFileBase64 = function (pathfile, data, mode, callback) {
    try {
        var result = firefoxPluginObject.writeFileBase64(pathfile, data, mode);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_WriteFileBase64", "returnCode": "-1" }); }
    }
};

/* 
 * Đọc nội dung file, trả về dạng Base64
 *   Đầu vào:
 *        * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *          Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *   Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_ReadFileBase64,
 *              base64: base64,,
 *              md5: string(md5,32),
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
 *		Trong đó: 
 *		    * base64: Nội dung base64 của file (đã xử lý lưu nội dung file trước khi đọc nội dung)
 *		    * returnCode là
 *			    + 1 nếu lấy nội dung file thành công (sau khi đã lưu nội dung file)
 *			    + 2 nếu file không tồn tại
 *			    + 3 nếu file không thể lưu nội dung trước khi đọc
 *              + -1 nếu lấy nội dung file lỗi do các nguyên nhân khác
 */
FirefoxPlugin.prototype.readFileBase64 = function (pathfile, callback) {
    try {
        var result = firefoxPluginObject.readFileBase64(pathfile);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_ReadFileBase64", "returnCode": "-1" }); }
    }
};

/* 
 * Xóa file
 *   Đầu vào:
 *        * pathfile: Đường dẫn tương đối(+tên file) + mode (bắt xóa file). VD: "1467366836294\\Vanban.doc"
 *          Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *   Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_DeleteFile,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
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
FirefoxPlugin.prototype.deleteFile = function (pathfile, mode, callback) {
    try {
        mode = mode || false;
        var result = firefoxPluginObject.deleteFile(pathfile, mode);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_DeleteFile", "returnCode": "-1" }); }
    }
};

/* 
 * Xóa folder
 *   Đầu vào:
 *        * pathfile: Đường dẫn tương đối(+tên folder) + mode (bắt xóa folder). VD: "1467366836294\\Thumuc"
 *          Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Thumuc"
 *   Đầu ra: 
 *		Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_DeleteFolder,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
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
FirefoxPlugin.prototype.deleteFolder = function (pathfile, mode, callback) {
    try {
        mode = mode || false;
        var result = firefoxPluginObject.deleteFolder(pathfile, mode);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_DeleteFolder", "returnCode": "-1" }); }
    }
};

/* 
 * Trả về trạng thái file tồn tại hay không
 * Đầu vào:
 *      * pathfile: Đường dẫn tương đối + tên file. VD: "1467366836294\\Vanban.doc"
 *        Vị trí lưu file mặc định: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi đó vị trí file chính xác là: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * Đầu ra: 
 *	    Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_ExistsFile,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage
 *          }
 *		Trong đó: 
 *			* returnCode:
 *				+ 0 nếu file không tồn tại.
 *				+ 1 nếu file tồn tại.
 */
FirefoxPlugin.prototype.existsFile = function (pathfile, callback) {
    try {
        var result = firefoxPluginObject.existsFile(pathfile);
        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_OpenFile", "returnCode": "-1" }); }
    }
};

/************************************\
              KÍ ĐIỆN TỬ
\************************************/

/* 
 * Ký điện tử File Word
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
 *          {
 *              action:Egov_SignWord,
 *				base64: base64,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage
 *          }
 *		Trong đó: 
 *			* returnCode:
 *				+ 1 nếu kí điện tử thành công.
 *              + 2 nếu file word cần kí không tồn tại
 *              + 3 nếu cert không tìm thấy (có thể do rút token ra)
 *              + -1 nếu không thể kí điện tử do các nguyên nhân khác
 */
FirefoxPlugin.prototype.signFile = function (pathfile, config, idxCert, callback) {
    if (!callback) {
        throw new Exception("singFile bắt buộc phải truyền hàm callback đầu vào");
    }
    try {
        config = config || {};
        var data = { "pathfile": pathfile, "config": config, "idxCert": idxCert };
        data = JSON.stringify(data);
        firefoxPluginObject.signFile(data, callback);
    } catch (err) {
        var error = { "action": "Egov_SignWord", "returnCode": "-1" };
        callback(error);
    }
};

/* 
 * Trả về index Cert chọn ký
 * Đầu vào:
 *      * không có đầu vào
 * Đầu ra: 
 *	    Trả về 1 chuỗi json có dạng: 
 *          {
 *              action:Egov_GetCertIndex,
 *				idxCert: idxCert,
 *              returnCode: returnCode,
 *              returnMessage: returnMessage 
 *          }
 *		Trong đó: 
 *			* returnCode:
 *				+ 1 nếu lấy thông tin cert index thành công
 *              + 2 nếu không tìm thấy cert nào trên máy hiện tại
 *              + 3 nếu không chọn cert nào trong danh sách (đóng cửa sổ chọn cert mà không chọn cái nào)
 *              + -1 nếu không thể lấy cert index do các nguyên nhân khác
 */
FirefoxPlugin.prototype.getCertIndex = function (callback) {
    try {
        firefoxPluginObject.getCertIndex(callback);
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_GetCertIndex", "returnCode": "-1" }); }
    }
};

/************************************\
            SCAN VĂN BẢN
\************************************/

FirefoxPlugin.prototype.getAllScanner = function (reload, callback) {
    try {
        var result = firefoxPluginObject.getAllScanner(reload);

        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_GetAllScanner", "returnCode": "-1" }); }
    }
};

FirefoxPlugin.prototype.acquire = function (showui, sourceIndex, pixelType, resolution, enableDuplex, showImage) {
    try {
        var result = firefoxPluginObject.acquire(showui, sourceIndex, pixelType, resolution, enableDuplex, showImage);

        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_Acquire", "returnCode": "-1" }); }
    }
};

FirefoxPlugin.prototype.imageCrop = function (url, x, y, x2, y2, width, height, callback) {
    try {
        var result = firefoxPluginObject.imageCrop(url, x, y, x2, y2, width, height);

        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_ImageCrop", "returnCode": "-1" }); }
    }
};

FirefoxPlugin.prototype.imageRotate = function (url, angle, callback) {
    try {
        var result = firefoxPluginObject.imageRotate(url, angle);

        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_ImageRotate", "returnCode": "-1" }); }
    }
};

FirefoxPlugin.prototype.imageAdjustBrightness = function (url, value, callback) {
    try {
        var result = firefoxPluginObject.imageAdjustBrightness(url, value);

        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_ImageAdjustBrightness", "returnCode": "-1" }); }
    }
};

FirefoxPlugin.prototype.imageAdjustContrast = function (url, value, callback) {
    try {
        var result = firefoxPluginObject.imageAdjustContrast(url, value);

        result = JSON.parse(result);
        if (typeof callback === "function") {
            callback(result);
        } else {
            return result;
        }
    } catch (err) {
        if (typeof callback === "function") { callback({ "action": "Egov_ImageAdjustContrast", "returnCode": "-1" }); }
    }
};

//FirefoxPlugin.prototype.transferImage = function (url, format, callback) {
//    var base64 = firefoxPluginObject.transferImage(url, format);
//    callback(base64);
//};

//FirefoxPlugin.prototype.getTempFolder = function (callback) {
//    callback(firefoxPluginObject.getTempFolder());
//};

// FirefoxPlugin.prototype.transferPdf = function (url, format) {
// var base64 = firefoxPluginObject.transferPdf(url, format);
// return base64;
// };

// FirefoxPlugin.prototype.transferDoc = function (url) {
// var base64 = firefoxPluginObject.transferDoc(url);
// return base64;
// };

//FirefoxPlugin.prototype.cancelTransferImage = function (url) {
//    firefoxPluginObject.cancelTransferImage(url);
//};

//FirefoxPlugin.prototype.isWordInstalled = function () {
//    return firefoxPluginObject.isWordInstalled();
//};

//FirefoxPlugin.prototype.isChangeContent = function (pathfile, callback) {
//    callback(firefoxPluginObject.isChangeContent(pathfile));
//};

//FirefoxPlugin.prototype.readFileScanBase64 = function (pathfile, start, length, callback) {
//    var data = firefoxPluginObject.readFileScanBase64(pathfile, start, length);
//};

//FirefoxPlugin.prototype.readFileScanBase64 = function (url, start, length, callback) {
//    var data = firefoxPluginObject.readFileScanBase64(url, start, length);
//    callback(data);
//};
