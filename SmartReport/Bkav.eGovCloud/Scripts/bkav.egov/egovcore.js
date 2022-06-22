//$(function () {
//    if (window.isCamResource) {
//        $("body, html").css("fontFamily", '"Khmer UI", sans-serif');
//        //$("body, html", window.parent.document).css("fontFamily", '"Khmer UI", sans-serif');
//    }
//})

jQuery.fn['bindResources'] = function (callback, isNotRemoveDataRes) {
    isNotRemoveDataRes = true;
    var labels = this.find("*[data-res]");
    var titles = this.find("*[data-restitle]");
    var placeHolders = this.find("*[data-respholder]");

    if (labels.length > 0 || titles.length > 0 || placeHolders.length > 0) {
        $.each(labels, function (i, ele) {
            var res = $(ele).attr("data-res");
            try {
                if (eval(res)) {
                    if (!isNotRemoveDataRes) {
                        $(ele).removeAttr("data-res");
                    }
                    if ($(ele).prop("tagName").toLowerCase() === "input") {
                        var eleVal = $(ele).val().trim();
                        $(ele).val(eval(res));
                    }
                    else {
                        var eleVal = $(ele).text().trim();
                        $(ele).text(eval(res));
                    }
                }
                else {
                    // console.log(res);
                }
            }
            catch (e) {
                var texts = JSON.stringify(res).split(".");
                texts = texts[texts.length - 2] + "." + texts[texts.length - 1];
                if (!isNotRemoveDataRes) {
                    $(ele).removeAttr("data-res");
                }
                if ($(ele).prop("tagName").toLowerCase() === "input") {
                    $(ele).val(texts);
                }
                else {
                    $(ele).text(texts);
                }
                // console.log(res);
            }

        });
        $.each(titles, function (i, ele) {
            var res = $(ele).attr("data-restitle");
            try {
                if (eval(res)) {
                    if (!isNotRemoveDataRes) {
                        $(ele).removeAttr("data-restitle");
                    }
                    $(ele).attr("title", eval(res));
                }
                else {
                    //console.log(res);
                }
            }
            catch (e) {
                var texts = JSON.stringify(res).split(".");
                texts = texts[texts.length - 2] + "." + texts[texts.length - 1];
                if (!isNotRemoveDataRes) {
                    $(ele).removeAttr("data-restitle");
                }
                $(ele).attr("title", texts);
                //console.log(res);
            }

        });
        $.each(placeHolders, function (i, ele) {
            var res = $(ele).attr("data-respholder");
            try {
                if (eval(res)) {
                    if (!isNotRemoveDataRes) {
                        $(ele).removeAttr("data-respholder");
                    }
                    $(ele).attr("placeholder", eval(res));
                }
                else {
                   // console.log(res);
                }
            }
            catch (e) {
                var texts = JSON.stringify(res).split(".");
                texts = texts[texts.length - 2] + "." + texts[texts.length - 1];
                if (!isNotRemoveDataRes) {
                    $(ele).removeAttr("data-respholder");
                }
                $(ele).attr("placeholder", texts);
               // console.log(res);
            }
        });
    }

    if (typeof callback === "function") {
        callback();
    }

    return this;
};

window.getResource = function (resourceKey) {
    /// <summary>
    /// Trả về ResourceValue theo Key, nếu không tồn tại, trả về ResourceKey
    /// </summary>
    /// <param name="resourceKey"></param>
    try {
        return eval(resourceKey);
    }
    catch (e) {
        return resourceKey;
    }
}

var extend = function (destination, source) {
    /// <summary>
    /// Hàm extend để thêm resource
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="source"></param>
    for (var property in source) {
        if (source[property] && source[property].constructor &&
         source[property].constructor === Object) {
            destination[property] = destination[property] || {};
            arguments.callee(destination[property], source[property]);
        } else {
            destination[property] = source[property];
        }
    }
    return destination;
};

// random 1 giá trị để ngẫu nhiên cho colorcode, mỗi lần tải file
// nó sẽ mang 1 giá trị, đảm bảo tính ngẫu nhiên nhưng vẫn đồng nhất
var randomInt = Math.floor(Math.random() * 10);

function getColorCode(char) {
    if (char) {
        return (char.toUpperCase().charCodeAt(0) + randomInt) % 10;
    }
    else {
        return randomInt % 10;
    }
}

function getDefaultAvatar() {
    if (egov.mobile) {
        title = egov.mobile.avatarTheme;
    }
    switch (title) {
        case "troll":
            return String.format(getResource("egov.resources.avatar.troll"), Math.floor(Math.random() * 6) + 1);
        case "icon":
            return String.format(getResource("egov.resources.avatar.icon"), Math.floor(Math.random() * 2) + 1);
        case "alphabet":
            return String.format(getResource("egov.resources.avatar.alphabet"), account[0].toLowerCase());
        default:
            return getResource("egov.resources.avatar.noData");
    }
}

function getUserAvatar(useRealAvatar, username) {
    if (useRealAvatar) {
        return String.format(egov.resources.avatar.path, username);
    }
    return getDefaultAvatar();
}
function getErrorAvatar() {
    return getResource("egov.resources.avatar.errorUrl");
}

(function (egov) {

    "use strict";

    //#region private fields

    var strips =
            [
                "áàảãạăắằẳẵặâấầẩẫậ",
                "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ",
                "đ",
                "Đ",
                "éèẻẽẹêếềểễệ",
                "ÉÈẺẼẸÊẾỀỂỄỆ",
                "íìỉĩị",
                "ÍÌỈĨỊ",
                "óòỏõọơớờởỡợôốồổỗộ",
                "ÓÒỎÕỌƠỚỜỞỠỢÔỐỒỔỖỘ",
                "ưứừửữựúùủũụ",
                "ƯỨỪỬỮỰÚÙỦŨỤ",
                "ýỳỷỹỵ",
                "ÝỲỶỸỴ"
            ];

    var replacements =
    [
        'a',
        'A',
        'd',
        'D',
        'e',
        'E',
        'i',
        'I',
        'o',
        'O',
        'u',
        'U',
        'y',
        'Y'
    ];

    //#endregion

    egov.utils = egov.utils || {};

    egov.log = function (logObject) {
        /// <summary>
        /// Hiển thị log ra dev tool.
        /// </summary>
        /// <param name="logObject">Tham số log</param>
        if (typeof console !== 'undefined' && typeof console.log !== 'undefined') {
            console.log(logObject);
        }
    };

    egov.toJSON = function (jsonStr) {
        /// <summary>
        /// Trả về json object từ chuỗi json
        /// </summary>
        /// <param name="jsonStr">Chuỗi json</param>
        if (typeof jsonStr === "string") {
            //try {
            return JSON.parse(jsonStr);
            //} catch (e) {
            //jsonStr = jsonStr.replaceAll("\\", "");
            return JSON.parse(jsonStr);
            //}
        }
        return jsonStr;
    }

    egov.getKeyName = function (entity) {
        /// <summary>
        /// Trả về tên đối tượng được lưu trong cache.
        /// </summary>
        /// <param name="entity">Entity của đối tượng.</param>

        var result;
        if (entity["id"] === undefined || entity["id"] === null) {
            result = entity["name"];
        } else {
            result = entity["name"] + "_" + entity["id"];
        }

        return result += "_" + egov.userid;
    }

    egov.callback = function (callbackFunction, param) {
        /// <summary>
        /// Thực thi hàm callback.
        /// </summary>
        /// <param name="callbackFunction">Hàm callback</param>
        /// <param name=" param">Giá trị truyền vào hàm</param>
        if (callbackFunction && typeof callbackFunction === 'function') {
            callbackFunction(param);
        }
    }

    egov.getRelativeEndpointUrl = function (url) {
        var i,
            splitString = function (string) {
                if ((string === null) || (string === undefined)) {
                    return '';
                }
                return string.split('/');
            },
            createUrl = function (newUrl, stringArray) {
                for (i = 0; i < stringArray.length; i += 1) {
                    if (stringArray[i].length > 0) {
                        newUrl += '/' + stringArray[i];
                    }
                }
                return newUrl;
            },
            splitRoot = splitString(egov.rootUrl),
            splitUrl = splitString(url),
            result = '';

        if (!url) {
            return '';
        }

        if (url.indexOf(egov.rootUrl || '') === 0) {
            return url;
        }

        result = createUrl(result, splitRoot);
        result = createUrl(result, splitUrl);

        return result;
    };

    egov.toPrettyDate = function (date) {
        /// <summary>Định dạng thời gian</summary>
        ///<param = "date"> thời gian truyền vào</param>

        var dateNow,
            diff,
            day_diff;

        if (typeof date !== "object") {
            date = new Date(date);
        }

        dateNow = new Date();
        diff = ((dateNow.getTime() - date.getTime()) / 1000);
        day_diff = Math.floor(diff / 86400);

        if (day_diff === 0) {
            if (diff < 60) {
                return "Mới gửi";
            }
            else if (diff < 120) {
                return "1 phút trước.";
            }
            else if (diff < 3600) {
                return Math.floor(diff / 60) + " phút trước.";
            }
            else if (diff < 7200) {
                return "1 giờ trước.";
            }
            else if (diff < 86400) {
                return Math.floor(diff / 3600) + " giờ trước.";
            }
        } else if (day_diff === 1) {
            return Globalize.format(date, "hh:mm tt") + " hôm qua.";
        } else if (day_diff > 1 && day_diff <= 7) {
            return Globalize.format(date, "hh:mm tt") + " " + Globalize.format(date, "dd") + " ngày trước.";
        }

        return Globalize.format(date, "dd/MM/yyyy hh:mm tt");
    };

    egov.toServerDate = function (date) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        return date.toISOString();
    };

    egov.mergeArraysByProperty = function mergeByProperty(arr1, arr2, prop) {
        /// <summary>
        /// Merge 2 mảng theo property chỉ định
        /// </summary>
        /// <param name="arr1">Mảng 1</param>
        /// <param name="arr2">Mảng 2</param>
        /// <param name="prop">Tên property muốn merge theo</param>
        _.each(arr2, function (arr2obj) {
            var arr1obj = _.find(arr1, function (arr1obj) {
                return arr1obj[prop] === arr2obj[prop];
            });

            arr1obj ? _.extend(arr1obj, arr2obj) : arr1.push(arr2obj);
        });

        return arr1;
    };

    egov.containFlags = function (combined, checkagainst) {
        ///<summary> 
        /// Kiểm tra xem phần tử combined có chứa checkagainst hay không
        /// </summary>
        return ((combined & checkagainst) === checkagainst);
    };

    egov.contain = function (str, target) {
        /// <summary>
        /// Trả về giá trị xác định chuỗi cần kiểm tra có thuộc một chuỗi khác không.
        /// </summary>
        /// <param name="str">Chuôi</param>
        /// <param name="target">Chuỗi cần kiểm tra</param>
        if (typeof str !== "string" || typeof target !== "string") {
            egov.log("Tham số đầu vào không hợp lệ.");
            return false;
        }
        return str.indexOf(target) > -1;
    }

    egov.getWordCount = function (str) {
        /// <summary>
        /// Trả về số từ trong chuỗi.
        /// </summary>
        /// <param name="str">Chuỗi cần kiểm tra</param>
        str = str.trim();
        return str.split(" ").length;
    };

    egov.stripVietnameseChars = function (input) {
        /// <summary>
        /// Trả về chuỗi tiếng việt không dấu
        /// </summary>
        /// <param name="input">chuỗi đầu vào</param>
        var stringBuilder;

        stringBuilder = input.split('');
        _.each(stringBuilder, function (str) {
            _.each(strips, function (strip, index) {
                if (egov.contain(strip, str)) {
                    str = replacements[index];
                }
            });
        });

        return stringBuilder.join("");
    };

    egov.sortByValue = function (objs, sortBy, value, otherSortBy) {
        /// <summary>
        /// Trả về danh sách sau khi sắp xếp đưa một giá trị theo property lên đầu
        /// </summary>
        /// <param name="objs">Danh sách các đối tượng cần sắp xếp</param>
        /// <param name="sortBy">Thuộc tính cần kiểm tra</param>
        /// <param name="value">Giá trị</param>
        var result,
            obj,
            remainObjects;

        if (typeof objs !== "object") {
            return;
        }

        result = [];
        obj = _.find(objs, function (item) {
            return item[sortBy] === value;
        });

        if (obj) {
            result.push(obj);
            remainObjects = _.reject(objs, function (item) {
                return item[sortBy] === value;
            });

            remainObjects = _.sortBy(remainObjects, function (item) {
                return item[otherSortBy];
            });

            result = _.union(result, remainObjects);
            return result;
        }

        return;
    };

    egov.getLevelOfDept = function (extDeptIds) {
        /// <summary>
        /// Trả về cấp độ phòng ban
        /// </summary>
        /// <param name="extDeptIds">Chuỗi mở rộng cấp phòng ban</param>

        return extDeptIds.split(".").length;
    }

})
(this.egov = this.egov || {});
(function (egov) {

    egov.enum = {

        categoryBusiness: {
            vbden: 1,
            vbdi: 2,
            hsmc: 4,
            kntc: 8,
        },

        urgents: {
            thuong: 1,
            khan: 2,
            hoatoc: 3
        },

        securityLevel: {
            thuong: 1,
            mat: 2,
            toimat: 3
        },

        transferType: {
            xulychinh: 1,
            dongxuly: 2,
            thongbao: 3,
            xyk: 4,
            giamsat: 5
        },

        documentTransferType: {
            taoMoiThongThuong: 1,
            banGiaoThongThuong: 2,
            banGiaoKhiTraLoi: 4,
            banGiaoKhiPhanLoai: 8
        },

        documentListSize: {
            small: 0,
            medium: 1,
            large: 2
        },

        documentViewType: {
            'default': 0,
            preView: 1,     //HIển thị văn bản hồ sơ ở khung preview (Người dung thiết lập hiển  thị toàn bộ thông tin văn bản hồ sơ ở khung xem trước văn bản)
            dialog: 2        //Hiển thị trên văn bản hồ sơ khi hiện dialog khi click 'Chi tiết văn bản ' ở contextmenu
        },

        quickViewType: {
            hide: 0,   ///Không hiển thị tóm tắt văn bản
            right: 1,   //Hiển thị tóm tắt văn bản bên phải
            below: 2   //Hiển thị tóm tắt văn bản bên dưới
        },

        documentOriginal: {
            egov: 0,
            egovOnline: 1,
            other: 2
        },

        fontSizeType: {
            nho: 0,  //Chữ nhỏ
            vua: 1,  //Chữ vừa
            lon: 2   //Chữ lớn
        },

        searchType: {
            document: 1, //Tìm văn bản.
            file: 2       //Tìm trong file.
        },

        processFilterType: {
            group: 1,
            equal: 2,
            custom: 3
        },

        documentStatus: {
            DuThao: 1,
            DangXuLy: 2,
            KetThuc: 4,
            LoaiBo: 8,
            DungXuLy: 16
        },

        permission: {
            khoitaovanban: 1,
            xemvanban: 2,
            dinhkem: 4,
            suavanban: 8,
            guiykien: 16,
            bangiao: 32,
            thongbao: 64,
            xinykien: 128,
            phanloai: 256,
            traloivanban: 512,
            laylaivanban: 1024,
            xacnhanbangiao: 2048,
            xacnhanxuly: 4096,
            yeucaubosung: 8192,
            tiepnhanbosung: 16384,
            kyduyet: 32768,
            traketqua: 65536,
            giahanxuly: 131072,
            dungxuly: 262144,
            ketthucxuly: 524288,
            huyvanban: 1048576,
            luuhosocanhan: 2097152,
            luuso: 4194304,
            phathanh: 8388608,
            capnhatketquaxulycuoi: 16777216,
            luuvanban: 33554432,
            traloiykien: 67108864,
            capphep: 134217728,
            doihanxulykhiphanloai: 268435456,
            molaivanban: 536870912,
            danhlaisoden: 1073741824,
            xoavanbankhoihoso: 2147483648
        },

        actionSpecial: {
            thongThuong: { name: 'ThongThuong', value: 0 },
            luuSoVaPhatHanhNoiBo: { name: 'LuuSoVaPhatHanhNoiBo', value: 1 },
            luuSoNoiBo: { name: 'LuuSoNoiBo', value: 2 },
            luuSoVaPhatHanhRaNgoai: { name: 'LuuSoVaPhatHanhRaNgoai', value: 3 },
            chuyenNguoiKhoiTao: { name: 'ChuyenNguoiKhoiTao', value: 4 },
            chuyenYKienDongGopVbDxl: { name: 'ChuyenYKienDongGopVbDxl', value: 5 },
            tiepTucXuLy: { name: 'TiepTucXuLy', value: 6 },
            chuyenNguoiCoQuyenDongGopYKien: { name: 'ChuyenNguoiCoQuyenDongGopYKien', value: 7 },
            tiepNhanHoSo: { name: 'TiepNhanHoSo', value: 8 },
            tiepNhanHoSoVaTiepTuc: { name: 'TiepNhanHoSoVaTiepTuc', value: 9 },
            capNhatKetQuaDungXuLy: { name: 'CapNhatKetQuaDungXuLy', value: 10 },
            chuyenYKienDongGopVbXinYKien: { name: 'ChuyenYKienDongGopVbXinYKien', value: 11 },
            chuyenNguoiGui: { name: 'ChuyenNguoiGui', value: 12 },
            tiepNhanBoSung: { name: 'TiepNhanBoSung', value: 13 },
            lienThong: { name: 'LienThong', value: 14 },
            transferMultiple: { name: 'TransferMultiple', value: 15 }
        },

        commentType: {
            Common: 1,
            Consulted: 2,
            Contribution: 3,
            Supplementary: 4,
            Signed: 5,
            Success: 6,
            Finished: 7
        },

        formType: {
            html: 1,
            dynamic: 2,
            fromUrl: 3
        },

        language: {
            VietNam: 1,
            Laos: 2
        },

        defaultToolbar: {
            Create: 1,
            Edit: 2,
            InsertImagePacket: 3
        },

        commonTemplate: {
            InBienNhanBanGiao: 1
        }
    };

    //#region HSMC

    egov.enum.feeType = {
        TiepNhan: 1,
        ThuongBosung: 2,
        TraCongDan: 3
    };

    egov.enum.paperType = {
        TiepNhan: 1,
        ThuongBosung: 2,
        TraCongDan: 3
    };

    egov.enum.supplementaryType = {
        renew: 1,
        "continue": 2,
        add: 3
    };

    egov.enum.printProcessType = {
        TiepNhan: 1,

        BanGiao: 2,

        KyDuyet: 4,

        TraKetQua: 8,

        TiepNhanBoSung: 16,

        GiaHan: 32
    };

    //#endregion


})(this.egov = this.egov || {});

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
(function (egov) {

    // Quản lý các đối tượng dữ liệu lưu ở client
    // {
    //    name: "userConfig",  => Tên đối tượng và là tên key lưu trong cache.
    //    hasCache: false,     => Giá trị xác định đối tượng dữ liệu có lưu cache offline dưới client hay không.
    //    hasSessionCache: true, => Giá trị xác định đối tượng dữ liệu có lưu lại theo phiên hay không.
    //    request: egov.request.getCommonConfigs, => trong egov.request-manager.js; set null để tạo đối tượng thuần dưới client
    //    id: 0,                                  => truyền vào Id để phân biệt các đối tượng lưu có cùng request: 
    //                                                  userConfig_1, userConfig_2,...
    //    isReplaceWhenSync: true,                => Giá trị xác định replace lại kết khi đồng bộ kết quả về hay merge với 
    //                                                  giá trị cũ
    //    isInsertToCurrentCache:true => Trong trường hợp không query lên server lấy toàn bộ 1 lượt dữ liệu (như allUsers) mà lấy lần lượt 
    //                                          dữ liệu (như template của văn bản, lúc nào mở ra mới lấy), giá trị này xác định 
    //                                          mỗi khi lấy 1 lần thì có insert thêm vào dữ liệu dưới client hay replace toàn bộ
    //                                    => Dữ liệu sẽ lưu dưới dạng 1 mảng, mỗi khi lấy dữ liệu mới sẽ insert   
    //    userId:egov.setting.userId => Trường hợp dữ liệu chỉ của 1 người dùng, người khác đăng nhập vào cùng 1 máy sẽ xóa dữ liệu của entity này, lấy dữ liệu mới về
    //                                  =>Khi đó dữ liệu sẽ lưu bằng 1 object : {userId: Id người dùng hiện tại, data: dữ liệu của riêng người dùng này}
    //    option: {                               => jquery ajax option mặc định.
    //        beforeSent: function () {
    //            egov.pubsub.public(egov.events.status, {
    //                type: "processing",
    //                message: "Đang tải"
    //            });
    //        }
    //    }
    // }

    egov.entities = {

        //#region common

        currentDoctypes: {
            name: "currentDoctypes",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocTypes,
            option: {
            }
        },

        categories: {
            name: "categories",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getCategories,
            option: {
            }
        },

        currentDepartments: {
            name: "currentDepartments",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDepartmentsByUser,
            option: {
            }
        },

        getDepartmentsCurrent: {
            name: "getDepartmentsCurrent",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDepartmentsCurrent,
            option: {
            }
        },

        allDept: {
            name: "allDept",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllDepartment,
            option: {
            }
        },

        allJobtitle: {
            name: "allJobtitle",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllJobTitlies,
            option: {
            }
        },
        allPosition: {
            name: "allPosition",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllPosition,
            option: {
            }
        },
        allUsers: {
            name: "allUsers",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllUsers,
            option: {
            }
        },

        allUserDeptPosition: {
            name: "allUserDeptPosition",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllUserDepartmentJobTitlesPosition,
            option: {
            }
        },

        getDeptAndUsers: {
            name: "getDeptAndUsers",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDeptAndUsers,
            option: {
            }
        },

        allAddress: {
            name: "allAddress",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllAddress,
            option: {
            }
        },

        allKeyword: {
            name: "allKeyword",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getKeywords,
            option: {
            }
        },

        allDocField: {
            name: "allDocField",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocField,
            option: {
            }
        },

        allSendType: {
            name: "allSendType",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getSendTypes,
            option: {
            }
        },

        userConfig: {
            name: "userConfig",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getCommonConfigs,
            option: {
                beforeSent: function () {
                    egov.pubsub.public(egov.events.status, {
                        type: "processing",
                        message: "Đang tải"
                    });
                }
            }
        },

        allCommonComments: {
            name: "allCommonComments",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getCommonComments,
            option: {
            }
        },

        allTemplateComments: {
            name: "allTemplateComments",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getTemplateComments,
            option: {
            }
        },

        currentUserId: {
            name: "currentUserId",
            hasCache: true,
            hasSessionCache: true,
            option: {
            }
        },

        getCommonConfigs: {
            name: "getCommonConfigs",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getCommonConfigs,
            option: {
            }
        },

        //#endregion

        //#region tree

        documentTree: {
            name: "documentTree",
            hasCache: true,
            request: egov.request.getDocumentTree,
            hasSessionCache: true,
            option: {
                beforeSent: function () {
                    egov.pubsub.public(egov.events.status, {
                        type: "processing",
                        message: "Đang tải"
                    });
                }
            }
        },

        //#endregion

        //#region documents

        documentStore: {
            name: "documentStore",
            id: null,
            hasCache: true,
            hasSessionCache: true,
            isReplaceWhenSync: false,
            request: egov.request.getDocumentStore,
            option: {}
        },

        getDocumentPermission: {
            name: "getDocumentPermission",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentPermission,
            option: {}
        },

        document_Store: {
            name: "document-functionStore",
            hasCache: false,
            hasSessionCache: true,
            request: null,
            option: {}
        },

        documents: {
            name: "documents",
            hasCache: true,
            hasSessionCache: true,
            isReplaceWhenSync: false,
            request: egov.request.getDocuments,
            option: {}
        },

        documentsReport: {
            name: "documents",
            hasCache: true,
            hasSessionCache: true,
            isReplaceWhenSync: false,
            request: egov.request.getReports,
            option: {}
        },

        tempDocuments: {
            name: "tempDocuments",
            hasCache: false,
            hasSessionCache: false,
            isReplaceWhenSync: true,
            request: egov.request.getDocuments,
            option: {}
        },

        //#endregion

        //#region Function Group

        functionGroups: {
            name: "functionGroups",
            id: null,
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getFunctionGroups,
            option: {}
        },

        //#endregion

        //#region document

        documentTemplate: {
            name: "documentTemplate",
            id: null,
            hasCache: true,
            hasSessionCache: true,
            isInsertToCurrentCache: true,
            request: egov.request.getDocumentTemplate,
            option: {
            }
        },

        documentEdit: {
            name: "documentEdit",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentInfoForEdit,
            option: {}
        },

        documentCreate: {
            name: "documentCreate",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentInfoForCreate,
            option: {}
        },

        documentCreateAction: {
            name: "documentCreateAction",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentCreateAction,
            option: {}
        },

        documentEditAction: {
            name: "documentEditAction",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentEditAction,
            option: {}
        },

        getUserByAction: {
            name: "getUserByAction",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getUserByAction,
            option: {}
        },

        transfer: {
            name: "transfer",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.transfer,
            option: {}
        },

        setPopUpSize: {
            name: "setPopUpSize",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.setPopUpSize,
            option: {}
        },

        getDocumentDetail: {
            name: "getDocumentDetail",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentDetail,
            option: {}
        },

        filterCitizen: {
            name: "filterCitizen",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.filterCitizen,
            option: {}
        },

        //#endregion

        //#region user store

        getStorePrivate: {
            name: "getStorePrivate",
            id: null,
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getStorePrivate,
            option: {}
        },

        //#endregion


        // #region cây văn bản (trees).

        //Lấy danh sách node con theo id node cha
        trees: {//Lấy danh sách node con ở node gốc
            name: "trees",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocumentTree,
            option: {},
            expriedDay: 7,
            isPrivate: true
        },

        //#endregion

        //#region Câu hỏi

        getNodeQuestion: {
            name: "getNodeQuestion",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getNodeQuestion,
            option: {}
        },

        //#endregion

        //region sổ hồ sơ (storeTrees)

        getStorePrivateDocuments: {
            name: "getStorePrivateDocuments",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getStorePrivateDocuments,
            option: {}
        },

        storeTrees: {//Lấy danh sách node con ở node gốc
            name: "storeTrees",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getStorePrivate,
            option: {}
        },

        //#endregion 

        //region version khi reset
        versionValue: {
            name: "version",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getVersionValue,
            option: {}
        },
        //#endregion 
    };

})
(this.egov = this.egov || {});
(function (egov) {

    egov.models = egov.models || {};
    egov.viewModels = egov.viewModels || {};

    //#region Document

    egov.models.document = Backbone.Model.extend({
        defaults: {
            Address: '',
            CategoryBusinessId: 0,
            CategoryId: 0,
            CitizenInfo: null,
            CitizenName: null,
            Color: 0,
            Comment: "",
            Comments: {},
            Compendium: "",
            Compendium2: "",
            DateAppointed: "",
            DateArrived: null,
            DateCreated: new Date(),
            DateFinished: null,
            DateModified: "",
            DateOfIssueCode: null,
            DatePublished: null,
            DateReceived: "",
            DateReceivedFormat: "",
            DateResponse: null,
            DateResponsed: null,
            DateResponsedOverdue: null,
            DateResult: "",
            DateReturned: null,
            DateSuccess: null,
            DocCode: null,
            DocFieldId: null,
            DocFieldIds: null,
            DocType: {},
            DocTypeId: "",
            DocTypePermission: 0,
            DocumentCopyId: 0,
            DocumentId: "00000000-0000-0000-0000-000000000000",
            Email: null,
            IdentityCard: null,
            InOutCode: null,
            InOutPlace: "",
            IsAcknowledged: 0,
            IsConverted: 0,
            IsDocumentImportant: 0,
            IsGettingOut: 0,
            IsReturned: null,
            IsSuccess: null,
            IsSupplemented: null,
            IsViewed: 0,
            Keyword: null,
            LastComment: "",
            LastUserIdComment: 0,
            Organization: null,
            Original: 1,
            Phone: null,
            ProcessedMinutes: null,
            ResultStatus: null,
            ReturnNote: null,
            SecurityId: null,
            SendTypeId: null,
            Status: 0,
            StoreId: null,
            SuccessNote: null,
            TotalPage: null,
            UrgentId: 1,
            UserCurrentId: 0,
            UserCreatedId: 0,
            UserCurrentFirstName: "",
            UserCurrentFullName: "",
            UserReturnedId: null,
            UserSuccessId: null,
            Selected: false,
            DocumentCopyType: 0,
            DocCopyStatus: 0,
            DateOverdue: '',
            NumberDayOverdue: '0',    //hạn giữ
            NumberDayAppointed: '0',  //Hạn tổng
            IsFile: 0,                 //Có phải là file hay không(dùng trong sổ hồ sơ):Mặc định là 0(không phải file)
            WorkflowId: 0,            //Quy trình của văn bản
            NodeCurrentId: 0,          //Node hiện tại của văn bản trên quy trình
            DocCopyDateModified: null,
            WorkflowTypes: "",
            WorkflowTypeId: "",
            WorkflowTypeName: "",
            Note: ""
        },

        initialize: function () {
            this.set('id', this.get('DocumentCopyId'));
        }
    });

    egov.models.documentList = Backbone.Collection.extend({
        model: egov.models.document
    });

    //#endregion

    //#region Document Permission

    egov.models.documentPermission = Backbone.Model.extend({
        defaults: { value: 0 }
    });

    egov.models.documentPermissionList = Backbone.Collection.extend({
        model: egov.models.documentPermission
    });

    //#endregion

    //#region Documents Toolbar

    egov.models.toolbar = Backbone.Model.extend({
        defaults: {
            text: '',
            className: '',
            disable: false,
            icon: '',
            dataUrl: '',
            shortKey: '',
            data: null,
            callback: null,
            dropdownWidth: 90,
            dropdownHeight: 0,
            position: {
                at: 'right bottom',
                my: 'right top'
            },
            contentId: '',
            isDatePicker: false,
            isDropdownMenu: false,
            hasShortKey: false,
            showSelected: false,
            defaultSelectedText: 'Tất cả'
        }
    });

    egov.models.toolbarList = Backbone.Collection.extend({
        model: egov.models.toolbar
    });

    //#endregion

    //#region Context Menu

    egov.models.contextMenu = Backbone.Model.extend({
        defaults: {
            selector: null,
            trigger: 'right',   // 'left'
            dataUrl: '',        // Url lấy dữ liệu (nếu có)
            param: '',          // Param cho url
            data: null,         // Danh sách các item là thể hiện của collection egov.models.ContextMenuItemModel
            callback: null,     // Hàm thực thi khi select trước thi thực thi hàm callback trong data
            style: {},
            position: {},
            isDatePicker: false, // Đặt true nếu muốn thể hiện nội dung xổ ra là datatimepicker
            // Hiển thị loading trước rồi bind dữ liệu sau
            // ví dụ: var context = selector.contextmenu({isShowLoading: true});
            // context.model.set('data', data);
            // context.render();
            isShowLoading: false,
            key: null
        },

        initialize: function () {
            var style = this.get('style');
            if (style.height === 0 || style.height === undefined) {
                style.height = 'auto';
            }

            this.set('style', $.extend({}, {
                display: 'none'
            }, style));

            this.set('position', $.extend({}, {
                at: 'right bottom',
                my: 'right top'
            }, this.get('position')));
        }
    });

    egov.models.contextMenuItem = Backbone.Model.extend({
        defaults: {
            text: '',
            value: '',
            callback: '',
            icon: '',
            selected: false
        },

        initialize: function () {
            var rootIconFolder = '/Content/Images/Toolbar/';
            if (this.get('icon') !== '') {
                this.set('icon', rootIconFolder + this.get('icon'));
            }
        }
    });

    egov.models.contextMenuList = Backbone.Collection.extend({
        model: egov.models.contextMenuItem
    });

    //#endregion

    //#region Tree

    var showTotalInTreeType = {
        none: 0,         //Không hiển thị
        unread: 1,       //Văn bản chưa đọc
        unreadOnAll: 2,  //Chưa đọc / Tất cả
        all: 3           //Tất cả
    };

    egov.models.TreeModel = Backbone.Model.extend({
        defaults: {
            functionId: 0,
            parentId: 0,
            name: "",
            params: "",
            paramId: 0,
            icon: "",
            state: "closed",
            order: 0,
            totalDocumentUnread: 0,
            totalDocument: 0,
            children: [],
            url: "",
            pagingUrl: "",
            isLoadChildren: false,
            showTotalInTreeType: showTotalInTreeType.unread,
            defaultSort: null,
            hasUyQuyen: false,
            userUyQuyen: null,
            isOpen: false,
            isSelected: false,
            hasTransferTheoLo: false,
            isOnlineRegistration: false,
            treeGroupId: null,
            treeGroupOrder: 0,
            hasExportFile: false
        },

        initialize: function () {
            this.set('id', this.get('functionId'));
            var url = '/Home/GetDocuments/' + this.get('functionId');
            var pagingUrl = '/Home/GetDocumentPaging/' + this.get('functionId');
            if (typeof this.get('params') === 'object') {
                this.set('params', JSON.stringify(this.get('params')));
            }
            if (this.get('params') !== '') {
                url += '?params=' + this.get('params');
                pagingUrl += '?params=' + this.get('params');
            }
            this.set('url', url);
            this.set('pagingUrl', pagingUrl);
            this.set('isLoadChildren', false);

            //  this.set("children", new egov.models.TreeList(this.get("children") ? this.get("children") : []));

            if (typeof this.get('defaultSort') === 'object') {
                this.set('defaultSort', JSON.stringify(this.get('defaultSort')));
            }

            if (typeof this.get('userUyQuyen') === 'string') {
                this.set('userUyQuyen', JSON.parse(this.get('userUyQuyen')));
            }
        }
    });

    egov.models.TreeList = Backbone.Collection.extend({
        model: egov.models.TreeModel
    });

    egov.models.StorePrivateModel = Backbone.Model.extend({
        defaults: {
            level: 0,
            parentId: 0,
            status: 0,
            storePrivateId: 0,
            storePrivateName: "",
            name: "",
            children: [],
            state: "closed",
            url: "",
            pagingUrl: "",
            root: false,
            isStoreShared: false,
            descStorePrivate: "",
            userIdJoined: [],
            deptExtJoined: []
        },

        initialize: function () {
            var pagingUrl = '/StorePrivate/GetDocuments/' + this.get('storePrivateId');
            this.set('id', this.get('storePrivateId'));
            this.set('pagingUrl', pagingUrl);

            this.set("children", new egov.models.StorePrivateList(this.get("children") ? this.get("children") : []));
        }
    });

    egov.models.StorePrivateList = Backbone.Collection.extend({
        model: egov.models.StorePrivateModel
    });

    //#endregion

    //#region Question

    egov.models.question = Backbone.Model.extend({
        defaults: {
            QuestionId: 0,
            Date: "",
            AskPeople: "",
            Detail: "",
            Name: "",
            Email: "",
            Phone: "",
            DocCode: "",
            QuestionType: 0,
            IsGeneralQuestion: true,
            UserComments: [],
            AnswerHolder: null,
            isMe: false,
            Compendium: "",
            DocumentHolderName: "",
            DocumentHolderAccount: "",
            DocumentHolderFullAccount: "",
        },

        initialize: function () {
        }
    });

    egov.models.questionList = Backbone.Collection.extend({
        model: egov.models.question
    });

    egov.models.QuestionTreeModel = Backbone.Model.extend({
        defaults: {
            level: 0,
            status: 0,
            name: "",
            children: [],
            state: "closed",
            root: false,
            isGeneral: false
        },

        initialize: function () {
            this.set("children", new egov.models.QuestionTreeList(this.get("children") ? this.get("children") : []));
        }
    });

    egov.models.QuestionTreeList = Backbone.Collection.extend({
        model: egov.models.QuestionTreeModel
    });

    //#endregion

    //#region Tabs

    egov.models.TabModel = Backbone.Model.extend({
        defaults: {
            id: 0,
            name: '',
            title: '',
            href: '',
            hasTooltip: true,
            hasCloseButton: false,
            isRoot: false,
            privateId: 0,
            //  isCookie: false,
            attributes: {},
            isCreateDocument: false,
            type: 0,
            cateBusId: 0,
            hasLoadContent: true
        }
    });

    egov.models.TabList = Backbone.Collection.extend({
        model: egov.models.TabModel
    });

    //#endregion

    //#region Transfer

    egov.models.action = Backbone.Model.extend({
        defaults: {
            currentNodeId: 0,
            id: "",
            isAllow: true,
            isAllowSign: false,
            isSpecial: false,
            name: "",
            nextNodeId: 0,
            priority: 0,
            userIdNext: 0,
            workflowId: 0
        }
    });

    egov.models.userAction = Backbone.Model.extend({
        defaults: {
            id: 0,
            value: 0,
            label: '',
            fullname: '',
            department: '',
            username: '',
            position: '',
            isMainProcess: false,
            isCoProcess: false
        },

        initialize: function () {
            this.set('id', this.get('value'));
        }
    });

    egov.models.actionUserList = Backbone.Collection.extend({
        model: egov.models.userAction
    });

    //#endregion

    //#region Attachment

    egov.models.attachment = Backbone.Model.extend({
        defaults: {
            Id: 0,
            Name: '',
            Extension: '',
            Size: 0,
            Versions: [],
            fileData: undefined,
            isRemoved: false,
            isNew: false,
            isMofified: false,
            isOpen: false,
            icon: ''
        },

        initialize: function () {
            var extension,
                icon;

            extension = this.get('Extension');
            if (extension.indexOf('.') !== 0) {
                extension = "." + extension;
            }

            switch (extension) {
                case '.doc':
                case '.docx':
                    icon = 'icon-file-word';
                    break;
                case '.xls':
                case '.xlsx':
                    icon = 'icon-file-excel';
                    break;
                case '.pdf':
                    icon = 'icon-file-pdf';
                    break;
                case '.txt':
                    icon = 'icon-text';
                    break;
                case '.zip':
                case '.rar':
                case '.7z':
                    icon = 'icon-file-zip';
                    break;
                case '.ppt':
                case '.pptx':
                    icon = 'icon-file-powerpoint';
                    break;
                case '.html':
                    icon = 'icon-chrome';
                    break;
                case '.jpg':
                case '.jpeg':
                case '.bmp':
                case '.png':
                case '.ico':
                case '.gif':
                    icon = 'icon-image2';
                    break;
                default:
                    icon = 'icon-file4';
                    break;
            }

            this.set('icon', icon);
        }
    });

    egov.models.attachmentList = Backbone.Collection.extend({
        model: egov.models.attachment
    });

    //#endregion

    //#region Relation

    egov.models.relation = Backbone.Model.extend({
        defaults: {
            id: 0,
            RelationCopyId: 0,
            RelationId: '',
            RelationType: 0,
            IsAddNext: false,
            Compendium: '',
            CitizenName: '',
            DocCode: '',
            DateCreated: '',
            CategoryName: '',
            IsRemoved: false,
            IsNew: false
        },

        initialize: function () {
            // Thiết lập id để tránh gán trùng relation id
            this.set('id', this.get('RelationCopyId'));
        }
    });

    egov.models.relationList = Backbone.Collection.extend({
        model: egov.models.relation
    });

    //#endregion

    //#region QuickView

    egov.models.quickView = Backbone.Model.extend({
        defaults: {
            id: 0,
            type: 1,
            compendium: null,
            lastComment: null,
            category: null,
            department: null,
            dateCreate: null,
            lastUser: null,
            docField: null,
            urgent: null,
            docCode: null,
            totalPage: null,

            ///online
            docType: null,
            dateReceived: null,
            DateReceivedFormat: "",
            dateAppoint: null,
            personInfo: null,
            email: null,
            phone: null,
            address: null,
        },
        initialize: function () {

        }
    });

    egov.models.quickViewList = Backbone.Collection.extend({

        model: egov.models.quickView,

        initialize: function () {
        }
    });

    //#endregion

    //#region Publish

    egov.models.address = Backbone.Model.extend({
        defaults: {
            IsShow: false,
            ParentId: null
        },
        initialize: function () {
            // Set các trường cho autocomplete
            this.set('id', this.get('AddressId'));
            this.set('value', this.get('AddressId'));
            this.set('label', this.get('Name'));
            this.set('isSelected', false);
        }
    });

    egov.models.addressCollection = Backbone.Collection.extend({
        model: egov.models.address
    });

    egov.models.publish = Backbone.Model.extend({
        defaults: {
            StoreId: 0,
            CodeId: 0,
            Code: '',
            DatePublished: new Date(),
            DateResponse: null,
            TotalPage: 1,
            SecurityId: 1,
            TotalCopy: 1,
            Approvers: '',
            KeyWordId: 0,
            InPlace: 0,
            IsCustomCode: false,
            Address: []
        }
    });

    //#endregion

    //#region Supplementary

    egov.models.supplementary = Backbone.Model.extend({
        SupplementaryId: 0,
        Comment: "",
        DateSend: "",
        IsDeleted: false,
        UserSendId: 0,
        UserSendName: ""
    });

    egov.models.supplementaryList = Backbone.Collection.extend({
        model: egov.models.supplementary
    });

    //#endregion

    //#region Search

    egov.models.search = Backbone.Model.extend({
        defaults: {
            Compendium: '',
            CategoryId: null,
            KeyWord: '',
            Content: '',
            DocCode: '',
            InOutCode: '',
            UrgentId: null,
            CategoryBusinessId: null,
            StorePrivateId: null,
            CurrentUserId: null,
            InOutPlace: '',
            FromDateStr: '',
            ToDateStr: '',
            BeforeDate: '',
            AfterDate: '',
            OrganizationCreate: '',
            DocFieldId: null,
            UserSuccessId: null
        }
    });

    egov.models.searchResultItem = Backbone.Model.extend({
        defaults: {
            Address: '',
            CategoryName: '',
            CitizenName: '',
            Compendium: '',
            DateAppointed: '',
            DateArrived: '',
            DateCreated: '',
            DocCode: '',
            DocumentCopyId: 0,
            DocumentId: '',
            InOutCode: 0,
            LastUserComment: '',
            UserSuccess: '',
            DateReceived: '',
            IsSelected: false
        }
    });

    egov.models.searchResult = Backbone.Collection.extend({
        model: egov.models.searchResultItem
    });

    //#endregion

    //#region Modal

    egov.models.modal = Backbone.Model.extend({
        defaults: {
            title: '',  // Tiêu đề modal
            keyboard: false, // Ẩn modal khi nhân esc
            resizable: false, // cho phép thay đổi kích thước
            draggable: false, // cho phép kéo thả vị trí
            animation: true, // hiển thị dạng fadein
            remote: '', // url nội dung modal - dùng để load nội dung modal sau theo url
            content: '', // nội dung modal - html có sẵn
            height: 'auto', // chiều cao
            width: 'auto', // chiều rộng,
            ignoreText: '',
            buttons: [], // các nút chức năng
            hide: null,   // callback sau khi ẩn modal
            close: null,   // callback trước khi ẩn modal 
            loaded: null,   // callback sau khi load xong nội dung = remote url.
            backdrop: "static",
            confirm: null
        }
    });

    //#endregion

    //#region Document Permission

    egov.models.totalNotifications = Backbone.Model.extend({
        defaults: {
            totalNotify: 0,
            total: 0
        }
    });

    egov.models.notification = Backbone.Model.extend({
        defaults: {
            NotificationId: 0,
            NotificationType: 0,
            Title: "",
            Content: "",
            SenderAvatar: "",
            SenderUserName: "",
            SenderFullName: "",
            Date: "",
            DateFormat: "",
            ReceiveDate: "",
            ViewdDate: "",
            IsViewed: false,

            //egov
            DocumentCopyId: 0,

            //mail
            MailId: 0,
            folderId: 0,

            //chat
            ChatId: "",
            chatterJid: "",
            messageId: ""
        }
    });

    egov.models.notificationList = Backbone.Collection.extend({
        model: egov.models.notification
    });

    egov.models.layoutNotify = Backbone.Model.extend({
        defaults: {
            total: 0,
            unreadTotal: 0,
            model: []
        }
    });

    //#endregion

    //#region Gán ra model cho các view. Mỗi view chỉ sử dụng một model tương ứng.

    // Cây văn bản
    egov.viewModels.tree = new egov.models.TreeList();

    // Danh sách văn bản
    egov.viewModels.documentList = new egov.models.documentList();

    // Danh sách tab
    egov.viewModels.tabList = new egov.models.TabList();

    // Danh sách người nhận trên form bàn giao
    egov.viewModels.actionUserList = new egov.models.actionUserList();

    //#endregion

})
(this.egov = this.egov || {});

(function (window, egov, Modernizr) {

    'use strict';

    //if (typeof Modernizr === 'undefined') {
    //    throw 'Chưa có thư viện Modernizr';
    //}

    //Biến tạm, dùng trong trường hợp trình duyệt không hỗ trợ localStorage lẫn indexDb
    var TempStorage = function () {
        return this._initialize();
    };

    TempStorage.prototype._initialize = function () {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        this._storage = {};
        this.hasSupport = true;
        return this;
    }

    TempStorage.prototype.get = function (key, callback) {
        var that = this,
            result;
        result = this._storage[key];
        if (result) {
            try {
                egov.callback(callback, JSON.parse(result));
            } catch (e) {
                var resultNew = JSON.parse(window.hashBase64.fromBase64(result));
                if (resultNew.value == null || resultNew.value == undefined) {
                    egov.locache.get(key, function (value) {
                        resultNew.value = value;
                        egov.locache.update(key, resultNew);
                        egov.callback(callback, resultNew);
                    });
                }
                else {
                    egov.callback(callback, resultNew);
                }
            }
        } else {
            egov.callback(callback);
        }
    }

    TempStorage.prototype.insert = function (key, value, callback) {
        this._storage[key] = JSON.stringify({ key: key, value: value, dateModified: new Date() });
        egov.callback(callback, value);
    }

    TempStorage.prototype.update = function (key, value, callback) {
        this._storage[key] = JSON.stringify({ key: key, value: value, dateModified: new Date() });

        egov.callback(callback, value);
    }

    TempStorage.prototype.delete = function (key, callback) {
        if (this._storage[key]) {
            this._storage[key] = null;
        }

        egov.callback(callback, key);
    }

    TempStorage.prototype.reset = function (callback) {
        this._storage = {};

        egov.callback(callback);
    }
    
    //#region LocalStorage

    var LocalStorage = function () {
        /// <summary>
        /// Lớp quản lý dữ liệu trong localStorage.
        /// localStorage: 
        ///     5MB
        ///     No Expire Date
        ///     Firefox 3.5+
        ///     Chrome 4+
        ///     IE 8+
        ///     Safari 4+
        ///     Opera 10.5+
        ///     Android 2.1+
        /// </summary>

        if (!Modernizr.localstorage) {
            return;
        }

        this._storage;
        return this._initialize();
    };

    LocalStorage.prototype._initialize = function () {
        /// <summary>
        /// Khởi tạo
        /// </summary>
        this.hasSupport = this._hasSupport();
        this._storage = window.localStorage;

        return this;
    }

    LocalStorage.prototype.get = function (key, callback) {
        var that = this,
            result;
        result = this._storage[key];
        if (result) {
            try {
                egov.callback(callback, JSON.parse(result));
            } catch (e) {
                var resultNew = JSON.parse(window.hashBase64.fromBase64(result));
                if (resultNew.value == null || resultNew.value == undefined) {
                    egov.locache.get(key, function (value) {
                        resultNew.value = value;
                        egov.locache.update(key, resultNew);
                        egov.callback(callback, resultNew);
                    });
                }
                else {
                    egov.callback(callback, resultNew);
                }
            }
        } else {
            egov.callback(callback);
        }
    }

    LocalStorage.prototype.insert = function (key, value, callback) {
        this._storage.setItem(key, JSON.stringify({ key: key, value: value, dateModified: new Date() }));
        egov.callback(callback, value);
    }

    LocalStorage.prototype.update = function (key, value, callback) {
        this._storage.setItem(key, JSON.stringify({ key: key, value: value, dateModified: new Date() }));

        egov.callback(callback, value);
    }

    LocalStorage.prototype.delete = function (key, callback) {
        if (this._storage[key]) {
            this._storage.removeItem(key);
        }

        egov.callback(callback, key);
    }

    LocalStorage.prototype.reset = function (callback) {
        this._storage.clear();

        egov.callback(callback);
    }

    LocalStorage.prototype._hasSupport = function () {
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

    //#endregion

    //#region IndexedDb

    //Các kiểu giao tiếp với indexedDb
    var indexedDbMode = {
        READ_WRITE: "readwrite",
        READ_ONLY: "readonly",
    };

    var IndexedDb = function () {
        /// <summary>
        /// Lớp quản lý dữ liệu trong localStorage.
        /// indexedDb: 
        ///     At least 20MB
        ///     No Expire Date
        ///     Firefox 10+
        ///     Chrome 23+
        ///     Opera 15+
        ///     Android 4.4+
        ///     IE 10+
        /// </summary>
        var request,
            that;

        if (!Modernizr.indexeddb) {
            return;
        }

        that = this;
        that._storage;
        that._storageName = "eGovStore";
        that._tableName = "eGovObjectStore";
        that.version = 1;
        that.hasSupport = true;    //Biến kiểm tra có hỗ trợ hay không

        return that._initialize();

        // HopCV:
        // todo: Chỗ này quá dư thừa, đã mở db lên rồi trong hàm init lại mở tiếp
        //       mà mục đích chỗ này check xem trình duyệt có hỗ trợ hay không khi mở kết db lên
        ////Open trước indexDb, nếu gặp lỗi sẽ xác định trình duyệt không support(như private mode in firefox)
        //request = window.indexedDB.open(that._storageName, 1);
        //request.onsuccess = function (event) {
        //    return that._initialize();
        //};

        //request.onerror = function () {
        //    that.isNotSupportIndexedDb = true;
        //}
    };

    IndexedDb.prototype._initialize = function () {
        /// <summary>
        /// Khởi tạo
        /// </summary>

        var request,
            db,
            that = this;

        request = window.indexedDB.open(that._storageName, that.version);

        request.onerror = function (event) {
            that.hasSupport = false;
            console.log("Database error: " + event.target.errorCode);
        };

        request.onupgradeneeded = function (event) {
            db = event.target.result;
            db.createObjectStore(that._tableName, { keyPath: "key" });
        }

        request.onsuccess = function (event) {
            that._storage = request.result;
        };

        return this;
    }

    IndexedDb.prototype.get = function (key, callback) {
        var objectStore,
            request,
            that = this;

        objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.get(key);

        request.onerror = function () {
            console.log("Database error: " + this.error.message);
        };

        request.onsuccess = function (e) {
            if (!request.result) {
                //console.log("Key không tồn tại: " + key);
            }
            egov.callback(callback, request.result);
        };
    }

    IndexedDb.prototype.insert = function (key, value, callback) {
        var objectStore,
            request,
            that = this;

        objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.add({ "key": key, "value": value, "dateModified": new Date() });

        request.onsuccess = function () {
            console.log("Add success: " + key);
            egov.callback(callback, value);
        };

        request.onerror = function () {
            console.log("Add error! " + this.error.message);
        }
    }

    IndexedDb.prototype.update = function (key, value, callback) {
        var objectStore,
            request,
            that = this;

        objectStore = that._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.get(key);

        request.onsuccess = function () {
            if (request.result) {
                request.result.value = value;
                request.result.dateModified = new Date();
                objectStore.put(request.result);
            }

            console.log("Update success." + key);
            egov.callback(callback, value);
        };

        request.onerror = function () {
            console.log("Update error! " + this.error.message);
        }
    }

    IndexedDb.prototype.delete = function (key, callback) {
        var objectStore,
            request;

        objectStore = this._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.delete(key);

        request.onerror = function () {
            console.log("delete error! " + this.error.message);
        }

        request.onsuccess = function () {
            console.log("delete success.");
            egov.callback(callback);
        };
    }

    IndexedDb.prototype.reset = function (callback) {
        var objectStore,
            request;

        objectStore = this._getObjectStore(indexedDbMode.READ_WRITE);
        request = objectStore.clear();

        request.onerror = function () {
            console.log("reset error! " + this.error.message);
        }

        request.onsuccess = function () {
            console.log("Reset success.");
            egov.callback(callback);
        };
    }

    IndexedDb.prototype._getObjectStore = function (mode) {
        /// <summary>
        /// Trả về store object trong indexeddb
        /// </summary>
        /// <param name="mode">Chế độ đọc "readwrite" hoặc "readonly"</param>
        /// <returns type="objectStore">objectStore</returns>
        mode = mode ? mode : indexedDbMode.READ_WRITE;
        var transaction, storage;

        if (this._storage == null) {
            console.log("Cache not loaded.");
            window.location.reload();
            return;
        }

        transaction = this._storage.transaction([this._tableName], mode);
        return transaction.objectStore(this._tableName);
    }

    //#endregion

    //#region File System

    //#endregion

    //#region Locache

    var Locache = function (cacheName) {
        /// <summary>
        /// Local Cache: Xử lý cache của hệ thống.
        /// Chỉ lưu những thông tin không quan trọng, được dùng thường xuyên dưới client để giảm tải cho server.
        /// </summary>
        /// <param name="cacheName" type="String">
        ///     - Giá trị chỉ định sử dụng cache nào: "storage","indexedDb", "fileApi".
        ///     - Để null: tự động chỉ định cache theo trình duyệt.
        /// </param>
        this._localStorage = Modernizr.localstorage;
        this._indexeddb = Modernizr.indexeddb;
        this._fileReader = Modernizr.filereader;

        // giá trị xác định có hỗ trợ Cache hay không.
        this._hasSupportCache;

        // cache data
        this._storage;

        // Giá trị xác định dung lượng cache client.
        this._cacheSize;
        this._cacheName = cacheName;

        return this._initialize();
    };

    Locache.prototype._initialize = function () {

        /// <summary>
        /// Thiết lập chế độ cache client
        /// </summary>

        // Kiểm tra trình duyệt hỗ trợ lưu cache
        this._hasSupportCache = this._localStorage || this._indexeddb || this._fileReader;
        if (!this._hasSupportCache) {
            return new TempStorage();
            return;
        }
        if (!this._fileReader && this._cacheName == "fileApi") {
            this._cacheName = "indexedDb";
        }
        else if (!this._indexeddb && this._cacheName == "indexedDb") {
            this._cacheName = "storage";
        }
        if (this._cacheName) {
            switch (this._cacheName) {
                case "storage":
                    return new LocalStorage();
                case "indexedDb":
                    return new IndexedDb();
                case "fileApi":
                    return; // undefined
                default:
                    console.log("Giá trị khởi tạo không đúng");
                    return; // undefined
            }
        }

        if (this._indexeddb) {
            return new IndexedDb();
        }
        else if (this._localStorage) {
            return new LocalStorage();
        }
        else if (this._fileReader) {
            return;  // undefined
        }

        return this;
    }

    Locache.prototype.get = function (key, callback) {
        /// <summary>
        /// Trả về giá trị theo key
        /// </summary>
        /// <param name="key">Key cần lấy giá trị</param>
        /// <param name="callback">Hàm thực thi sau khi lấy giá trị thành công</param>
        /// <returns type="Object">Json object: {key: "", value: "", dateModified: ""}</returns>
        egov.callback(callback);
    }

    Locache.prototype.insert = function (key, value, callback) {
        /// <summary>
        /// Thêm mới giá trị vào cache
        /// </summary>
        /// <param name="key">Tên giá trị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="callback">Hàm thực thi sau thêm giá trị thành công</param>

        console.log('Locache chưa được khởi tạo');
    }

    Locache.prototype.update = function (key, value, callback) {
        /// <summary>
        /// Cập nhật giá trị vào cache
        /// </summary>
        /// <param name="key">Tên giá trị</param>
        /// <param name="value">Giá trị</param>
        /// <param name="callback">Hàm thực thi sau thêm giá trị thành công</param>
        console.log('Locache chưa được khởi tạo');
    }

    Locache.prototype.delete = function (key, callback) {
        /// <summary>
        /// Xóa giá trị khỏi cache theo key
        /// </summary>
        /// <param name="key">Tên key</param>
        /// <param name="callback">Hàm thực thi sau xóa giá trị thành công</param>

        console.log('Locache chưa được khởi tạo');
    }

    Locache.prototype.reset = function (callback) {
        /// <summary>
        /// Xóa tất cả dữ liệu trong cache
        /// </summary>
        /// <param name="callback">Hàm thực thi sau xóa dữ liệu thành công</param>

        console.log('Locache chưa được khởi tạo');
    }

    //#endregion

    egov.CacheManager = Locache;

})(this, this.egov = this.egov || {}, this.Modernizr);
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

(function (egov, $) {
    /**
    * PubSub như là một hệ thống EventEmitter.
    * Các Widget đăng ký các sự kiện publish và sự kiện được sử dụng chung cho tất cả các subscriber khác.
    * 
    * Notes: sử dụng egov.events.js để quản lý tên các event được đăng ký.
    */
    egov.pubsub = (function () {
        var queue = [],
            that = {};

        that.publish = function (eventName, data, position) {
            /// <summary>
            /// Thực thi các hàm callback được liên kết với eventName
            /// </summary>
            /// <param name="eventName">Tên event cần thực thi</param>
            /// <param name="data">Dữ liệu truyền cho hàm callback</param>
            var context, intervalId, idx = 0;
            var events = queue[eventName];
            if (!events) return;

            intervalId = setInterval(function () {
                if (events[idx]) {
                    try {
                        context = events[idx].context || this;
                        events[idx].callback.call(context, data, position);
                    } catch (e) {
                        // log the message for developers
                        console.log('Có lỗi xảy ra khi thực thi một trong những hàm callback cho sự kiện "' + eventName + '"');
                        console.log('Lỗi đó là: "' + e + '"');
                    }

                    idx += 1;
                } else {
                    clearInterval(intervalId);
                }
            }, 0);
        };

        that.subscribe = function (eventName, callback, context) {
            /// <summary>
            /// Đăng ký một sự kiện. Cá Sự kiện đăng ký tiếp theo sẽ luôn được thêm vào (chứ không overwrite).
            /// Để hủy bỏ đăng ký một sự kiện, sử dụng hàm unsubscribe.
            /// </summary>
            /// <param name="eventName">Tên sự kiện đăng ký, nên sử dụng dấu . để phân biệt các event</param>
            /// <param name="callback">Hàm thực thi.</param>
            /// <param name="context">Context để thực thi hàm callback</param>
            if (!queue[eventName]) {
                queue[eventName] = [];
            }

            queue[eventName].push({
                callback: callback,
                context: context
            });
        };

        that.unsubscribe = function (eventName, callback, context) {
            /// <summary>
            /// Hủy bỏ đăng ký sự kiện.
            /// </summary>
            /// <param name="eventName">Tên sự kiện</param>
            /// <param name="callback">Hàm callback sau khi hủy bỏ. Sử dụng để chắc chắn rằng sự kiện đã được hủy bỏ.</param>
            /// <param name="context">Context thực thi hàm callback.</param>
            if (queue[eventName]) {
                queue[eventName].pop({
                    callback: callback,
                    context: context
                });
            }
        };

        return that;
    }());

}(this.egov = this.egov || {}, jQuery));

(function (egov, $) {

    // Quản lý danh sách các sự kiện được subscribe
    egov.events = {
        status: {
            status: "status",
            success: "status.success",
            error: "status.error",
            processing: "status.processing",
            warning: "status.warning",
            destroy: "status.destroy",
            importantWarning: "status.importantWarning"
        },

        destroyClickEvent: "destroyClickEvent",
        hideAllContext: "hideAllContext",

        tree: {
            reload: "tree.reload"
        },

        tab: {
            close: "tab.close",
            closeAll: "tab.closeAll",
            addDocument: "tab.addDocument",
            openDocument: "tab.openDocument",
            addSearch: "tab.addSearch",
            openDocumentOnline: "tab.openDocumentOnline"
        },

        documents: {
            showQuickView: "documents.showQuickView"
        },

        document: {
            open: "document.open",
            create: "document.create"
        }
    };

})
(this.egov = this.egov || {});
(function (egov, $) {

    var priorities = {
        importantWarning: 0,
        error: 1,
        success: 2,
        processing: 3,
        warning: 4
    };

    $.fn.extend({
        animateCss: function (animationName) {
            var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
            this.addClass('animated ' + animationName).one(animationEnd, function () {
                $(this).removeClass('animated ' + animationName);
            });
        }
    });

    // jQuery UI Widget
    $.widget('egov.status', {
        options: {
            duration: 5000,
            importantWarningDuration: 10000,
            subscribe: function () {
                egov.log('Chưa set thuộc tính subscribe cho option của status.');
            }
        },

        _create: function () {
            /// <summary>
            /// Khởi tạo
            /// </summary>

            this.element.addClass("egov-status");

            // Thiết lập vị trí hiển thị của status
            this.element.css({
                left: "10px",
                bottom: "0"
            });

            this._subscribeGlobalEvents();
        },

        currentStatus: null,

        _subscribeGlobalEvents: function () {
            /// <summary>
            /// Đăng ký các events ra global
            /// </summary>
            this.options.subscribe(egov.events.status.status, this._statusSubscription, this);
            this.options.subscribe(egov.events.status.success, this._statusSuccess, this);
            this.options.subscribe(egov.events.status.error, this._statusError, this);
            this.options.subscribe(egov.events.status.processing, this._statusProcessing, this);
            this.options.subscribe(egov.events.status.warning, this._statusWarning, this);
            this.options.subscribe(egov.events.status.destroy, this.destroy, this);
            this.options.subscribe("status.hide", this.destroy, this);
            this.options.subscribe(egov.events.status.importantWarning, this._statusImportantWarning, this);
        },

        _statusSubscription: function (status, position) {
            /// <summary>
            /// Hiển thị một status
            /// </summary>
            /// <param name="status">Status: {message: text, type: priority,duration: 3, undo}</param>
            /// <param name="position">Vị trí hiển thị thông báo (mobile)</param>
            var that = this,
                current = this.currentStatus,
                statusContent, statusClass;
            
            // that.element.show();

            status.priority = this._getPriority(status);

            // Hủy message hiện tại nếu message mới có priority nhỏ hơn (nhỏ hơn thì ưu tiên hơn).
            if (current && (status.priority < current.priority)) {
                clearTimeout(current.timer);
            }

            current = status;

            statusContent = this._getStatusContent(status);
            statusClass = this._getStatusColorClass(status.type);

            if (egov.mobile && status.priority == priorities.processing) {
                if (egov.mobile.isTablet) {
                    egov.mobile.$loading.attr("class", "");
                    egov.mobile.$loading.addClass(this._getStatusPositionClass(position));
                }

                egov.mobile.$loading.fadeIn();
                that.element.hide();
            }
            else {
                this.element.html(statusContent).attr("class", "alert egov-status " + statusClass).show();
            }

            that.element.animateCss("fadeInUp");

            // Trường hợp set duration = 0 hiển thị message đến khi gọi hàm destroy hoặc có notify khác ưu tiên hơn
            if (status.duration !== 0) {
                current.timer = setTimeout(function () {
                    that.element.fadeOut();
                    that.currentStatus = null;
                }, status.duration || this.options.duration);
            }
        },

        _importantWarningStatusSubscription: function (status) {
            /// <summary>
            /// Hiển thị cảnh báo quan trọng
            /// </summary>
            /// <param name="status"></param>
            var that = this,
                statusContent,
                statusClass;

            statusClass = this._getStatusColorClass(status.type);
            statusContent = this._getStatusContent(status);

            this.importantWaringElement.html(statusContent)
                .attr("class", statusClass)
                .fadeIn();

            // Trường hợp set duration = 0 hiển thị message đến khi gọi hàm destroy hoặc có notify khác ưu tiên hơn
            if (status.duration !== 0) {
                status.timer = setTimeout(function () {
                    that.importantWaringElement.fadeOut();
                    that.currentStatus = null;
                }, status.duration || this.options.importantWarningDuration);
            }
        },

        _statusSuccess: function (status) {
            /// <summary>
            /// Hiển thị thông báo xử lý thành công.
            /// </summary>
            /// <param name="status">{message: text, undo: object}</param>
            var status;
            if (typeof status == "string") {
                status = {
                    type: "success",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "success",
                    message: status.message,
                    undo: status.undo
                };
            }

            this._statusSubscription(status);
        },

        _statusError: function (status) {
            /// <summary>
            /// Hiển thị thông báo xử lý lỗi.
            /// </summary>
            /// <param name="status">{message: text, undo: object}</param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "error",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "error",
                    message: status.message,
                    undo: status.undo
                };
            }

            this._statusSubscription(status);
        },

        _statusProcessing: function (message, position) {
            /// <summary>
            /// Hiển thị thông báo đang xử lý
            /// </summary>
            /// <param name="message">text thông báo</param>
            /// <param name="position">Vị trí hiển thị thông báo</param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "processing",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "processing",
                    message: message,
                    duration: 0
                };
            }

            this._statusSubscription(status, position);
        },

        _statusWarning: function (message) {
            /// <summary>
            /// Hiển thị thông báo cảnh báo
            /// </summary>
            /// <param name="message">Text thông báo</param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "warning",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "warning",
                    message: message
                };
            }

            this._statusSubscription(status);
        },

        _statusImportantWarning: function (message) {
            /// <summary>
            /// Các cảnh báo quan trọng được hiển thị trên header của trang web
            /// </summary>
            /// <param name="message"></param>
            var status;

            if (typeof status == "string") {
                status = {
                    type: "importantWarning",
                    message: status,
                    undo: undefined
                };
            }
            else {
                status = {
                    type: "importantWarning",
                    message: message
                };
            }

            this.importantWaringElement = parent.$("#importantWarning");
            this._importantWarningStatusSubscription(status);
        },

        _getPriority: function (status) {
            return priorities[status.type];
        },

        _getStatusContent: function (status) {
            var result, undo, statusIcon;
            result = $("<div>").addClass("status-content");

            switch (status.type) {
                case "success":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/check.svg' >";
                    break;
                case "error":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/error.svg' >";
                    break;
                case "processing":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/processing.svg' >";
                    break;
                case "warning":
                case "importantWarning":
                    statusIcon = "<img src='../../content/bkav.egov/status/images/warning.svg' >";
                    break;
                default:
                    statusIcon = "<img src='../../content/bkav.egov/status/images/info.svg' >";
                    break;
            }
            result.append(statusIcon);

            result.append($("<span>").text(status.message));

            if (status.undo) {
                undo = $("<a href='#' class='alert-link'>").text(status.undo.message);
                undo.click(function (e) {
                    if (typeof status.undo.callback === "function") {
                        status.undo.callback();
                    }
                });

                result.append(undo);
            }

            return result;
        },

        _getStatusColorClass: function (type) {
            switch (type) {
                case "success":
                    return "alert-success";
                case "error":
                    return "alert-danger";
                case "processing":
                    return "alert-info";
                case "warning":
                case "importantWarning":
                    return "alert-warning";
                default:
                    return "alarm-info";
            }
        },

        _getStatusPositionClass: function (position) {
            // position: 
            //  - 1: Hiển thị trên danh sách văn bản
            //  - 2: Hiển thị trên chi tiết văn bản
            switch (position) {
                case 2:
                    return "showInDetail";
                default:
                    return "showInList";
            }
        },

        destroy: function () {
            if (this.currentStatus) {
                clearTimeout(this.currentStatus.timer);
            }
            // $.Widget.prototype.destroy.call(this);
            if (egov.mobile && egov.mobile.$loading) {
                egov.mobile.$loading.fadeOut();
            }
            this.element.hide();
        },
    });

}(this.egov = this.egov || {}, jQuery));
(function (egov, $, _, undefined) {
    //if (typeof ($) === 'undefined') {
    //    throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    //}
    //if (typeof (_) === 'undefined') {
    //    throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    //}
    var strips =
            [
                "áàảãạăắằẳẵặâấầẩẫậ",
                "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ",
                "đ",
                "Đ",
                "éèẻẽẹêếềểễệ",
                "ÉÈẺẼẸÊẾỀỂỄỆ",
                "íìỉĩị",
                "ÍÌỈĨỊ",
                "óòỏõọơớờởỡợôốồổỗộ",
                "ÓÒỎÕỌƠỚỜỞỠỢÔỐỒỔỖỘ",
                "ưứừửữựúùủũụ",
                "ƯỨỪỬỮỰÚÙỦŨỤ",
                "ýỳỷỹỵ",
                "ÝỲỶỸỴ"
            ];

    var replacements =
    [
        'a',
        'A',
        'd',
        'D',
        'e',
        'E',
        'i',
        'I',
        'o',
        'O',
        'u',
        'U',
        'y',
        'Y'
    ];

    var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}">'
                                       + '<ins class="jstree-icon">&nbsp;</ins><a href="#" class="">'
                                       + '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate2 = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state} jstree-unchecked">'
                                      + '<ins class="jstree-icon">&nbsp;</ins><a href="#" class="">'
                                      + '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var plugins = ["themes", "json_data", "ui", "crrm"];

    egov.utilities = {};

    egov.utilities.checkbox = {};

    egov.utilities.checkbox.checkAndUnCheckAll = function (allitems, item, callback) {
        var count = item.length;
        //Check all items
        allitems.bind("click", function () {
            var checkedAll = this.checked;
            for (var i = 0; i < count; i++) {
                item[i].checked = checkedAll;
                $(item[i]).trigger('change');
            }
            if (callback && typeof callback === 'function') {
                callback();
            }
        });
        //Item check
        item.bind("click", function () {
            var countCheck = 0;
            var countChecked = 0;
            item.each(function () {
                countCheck++;
                if (this.checked == true) {
                    countChecked++;
                }
                //   $(this).trigger('change');
            });
            var checked = countCheck == countChecked ? true : false;
            allitems.prop("checked", checked);

            if (callback && typeof callback === 'function') {
                callback();
            }
        });
    };

    //Các hàm liên quan đến treeview - jstree
    egov.utilities.jstree = {};

    egov.utilities.jstree.getChildrens = function (parentid, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
        var children = _.filter(arrDept, function (num) { return num.parentid == parentid; });
        var deptUsers = _.filter(arrDeptUserJobtitles, function (dept) {
            return dept.departmentid === parentid;
        });
        if (children.length > 0) {
            for (var j = 0; j < children.length; j++) {
                if (egov.utilities.jstree.getChildrens(children[j].value, false, arrDept, [], []).length > 0 || (hasUser && deptUsers.length > 0)) {
                    children[j].state = "closed";
                }
            }
        }

        if (hasUser) {
            if (deptUsers.length > 0) {
                for (var i = 0; i < deptUsers.length; i++) {
                    var userindept = _.find(arrUsers, function (user) {
                        return user.value === deptUsers[i].userid;
                    });
                    if (userindept) {
                        var selected = { "value": "user_" + userindept.value, "data": userindept.fullname, "parentid": parentid, "state": "leaf", "metadata": { "id": "user_" + userindept.value }, "attr": { "id": "user_" + userindept.value, "rel": "people", "idext": deptUsers[i].idext } };
                        children.push(selected);
                    }
                }
            }
        }
        return children;
    };

    egov.utilities.jstree.bindJsTree = function (divTree, hasUser, hasCheckbox, hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind, hasCheckParent, hasCheckChildren) {
        var deptRoot = _.find(arrDept, function (node) {
            return node.parentid === 0;
        });
        if (hasCheckbox) {
            plugins.push("checkbox");
        }
        if (hasDnD) {
            plugins.push("dnd");
        }
        if (deptRoot) {
            divTree.jstree({
                "json_data": {
                    "data": [
                        {
                            "data": deptRoot.data.toString(),
                            "metadata": { id: deptRoot.value },
                            "state": "closed",
                            "attr": { "id": deptRoot.value, "rel": "dept", "idext": deptRoot.idext, "label": deptRoot.label },
                            "children": egov.utilities.jstree.getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles)
                        }
                    ]
                },
                "crrm": hasDnD == false ? {} : {
                    "move": {
                        "check_move": function (m) {
                            var dept = _.find(arrDept, function (de) {
                                return de.value === parseInt(m.o.attr('id'));
                            });
                            if (!dept) return false;
                            if (dept.level != 1) return false;
                            var p = this._get_parent(m.o);
                            if (!p) return false;
                            p = p == -1 ? this.get_container() : p;
                            if (p === m.np) return true;
                            if (p[0] && m.np[0] && p[0] === m.np[0]) return true;
                            return false;
                        }
                    }
                },
                "dnd": hasDnD == false ? {} : {
                    "drop_target": false,
                    "drag_target": false
                },
                "plugins": plugins
            }).bind("loaded.jstree", function (e, dataLoad) {
                var depth = 1;
                dataLoad.inst.get_container().find('li').each(function () {
                    if (dataLoad.inst.get_path($(this)).length <= depth) {
                        dataLoad.inst.open_node($(this));
                    }
                });
                divTree.bind("open_node.jstree", function (event, data) {
                    if (data.inst._get_children(data.rslt.obj).length == 0) {
                        egov.utilities.jstree.appendChild(data.rslt.obj, parseInt(data.rslt.obj.attr("id")), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles, hasCheckParent, hasCheckChildren);
                    }
                });

                if (typeof callBack === 'function') {
                    callBack(dataBind);
                }
            });
        }
    };

    egov.utilities.jstree.appendChild = function ($parent, parentId, hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles, hasCheckParent, hasCheckChildren) {
        if (typeof hasCheckParent == undefined)
            hasCheckParent = true;
        if (typeof hasCheckChildren == undefined)
            hasCheckChildren = true;
        var child = egov.utilities.jstree.getChildrens(parentId, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
        if (child.length > 0) {
            var $newChild = $('<ul></ul>');
            $newChild.appendTo($parent);
            if (hasCheckbox) {
                var tem = hasCheckChildren ? itemTreeviewCheckboxTemplate : itemTreeviewCheckboxTemplate2;
                $.template("checkboxTemplate", tem);
                $.tmpl("checkboxTemplate", child).appendTo($newChild);
                if (hasCheckParent) {
                    $($parent).find("li").each(function (idx, listItem) {
                        $(listItem).addClass($parent.hasClass("jstree-checked") ? "jstree-checked" : "jstree-unchecked");
                    });
                }
            } else {
                $.template("itemTreeviewTemplate", itemTreeviewTemplate);
                $.tmpl("itemTreeviewTemplate", child).appendTo($newChild);
            }
            $newChild.children("li:last").addClass("jstree-last");
        }
    };

    // Các hàm xử lý chuỗi
    egov.utilities.string = {};

    egov.utilities.string.stripVietnameseChars = function (input) {
        var stringBuilder = [];
        for (var k = 0; k < input.length; k++) {
            stringBuilder.push(input.charAt(k));
        }
        for (var i = 0; i < stringBuilder.length; i++) {
            for (var j = 0; j < strips.length; j++) {
                if (strips[j].indexOf(stringBuilder[i]) > -1) {
                    stringBuilder[i] = replacements[j];
                }
            }
        }
        return stringBuilder.join("");
    };

    egov.utilities.url = {};
    egov.utilities.url.getQueryStringValue = function (name, url) {
        if (!url) {
            url = location.search;
        }
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(url);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };
    egov.utilities.array = {};

    egov.utilities.array.distinct = function (array) {
        var arrayWithUniqueValues = [];
        var objectCounter = {};
        for (var i = 0; i < array.length; i++) {
            var currentMemboerOfArrayKey = JSON.stringify(array[i]);
            var currentMemboerOfArrayValue = array[i];
            if (objectCounter[currentMemboerOfArrayKey] === undefined) {
                arrayWithUniqueValues.push(currentMemboerOfArrayValue);
                objectCounter[currentMemboerOfArrayKey] = 1;
            } else {
                objectCounter[currentMemboerOfArrayKey]++;
            }
        }
        return arrayWithUniqueValues;
    };

    //egov.utilities.resource = {};
    //egov.utilities.resource.getResource = function (resourceKey) {
    //    /// <summary>
    //    /// Trả về ResourceValue theo Key, nếu không tồn tại, trả về ResourceKey
    //    /// </summary>
    //    /// <param name="resourceKey"></param>
    //    try {
    //        return eval(resourceKey);
    //    } catch (e) {
    //        return resourceKey;
    //    }
    //};

    //egov.utilities.resource.getResourceWithEnum = function (wrapperElement) {
    //    require(['staticResource'], function () {
    //        egov.staticResource.getStaticElement(wrapperElement);
    //    });
    //}

})(window.egov = window.egov || {}, window.jQuery, window._);

//(function ($) {
//    $.fn.serializeObject = function () {
//        var o = {};
//        var a = this.serializeArray();
//        $.each(a, function () {
//            if (o[this.name]) {
//                if (!o[this.name].push) {
//                    o[this.name] = [o[this.name]];
//                }
//                o[this.name].push(this.value || '');
//            } else {
//                o[this.name] = this.value || '';
//            }
//        });
//        return o;
//    };
//})(window.jQuery);

// bt.util.date.js

/*
    Author: TienBV
    DateCreated: 25/06/2015
    Version: 1.0

    Description:

    - Các đơn vị thời gian:
        year: "year",
        quarter: "quarter",
        month: "month",
        week: "week",
        date: "date",
        day: "day",
        hour: "hours",
        minute: "minutes",
        second: "seconds",
        miniSecond: "miniSecond"

    - Các định dạng format
        yy: "yy",                   // Năm: 96, 97, ... 14, 15
        yyyy: "yyyy",               // Năm đầy đủ: 1996, 1997, ... 2014, 2015
        q: "q",                     // Quý: 1, 2, 3, 4
        M: "M",                     // Tháng: 1, 2, ... 12
        MM: "MM",                   // Tháng đầy đủ: 01, 02, ... 12
        MMM: "MMM",                 // Tháng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Tháng: Tháng một, Tháng hai, ... Tháng mười hai
        w: "w",                     // Tuần: 1, 2, ... 52
        ww: "ww",                   // Tuần: 01, 02, ... 52
        d: "d",                     // Ngày trong tháng: 1, 2, ... 31
        dd: "dd",                   // Ngày trong tháng: 01, 02, ... 31
        ddd: "ddd",                 // Ngày trong tuần: Thứ 2, thứ 3, ... 
        h: "h",                     // Giờ (12): 1, 2, ... 12    
        hh: "hh",                   // Giờ (12): 01, 02, ... 12
        H: "H",                     // Giờ (24): 1, 2, ... 23
        HH: "HH",                   // Giờ (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Phút: 1, 2, ... 59
        mm: "mm",                   // Phút: 01, 02, ... 59
        s: "s",                     // Giây: 1, 2, ... 59
        ss: "ss",                   // Giây: 01, 02, ... 59
        sss: "sss",                 // Tích tắc: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST

    - Các method cho kiểu dữ liệu Date

        + parse(value, format): Chuyển đổi string sang datetime theo format chỉ định. Mặc định hệ thống parse theo ISODate format.
            Date.parse("2015-07-15T16:20:04.021Z")  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            Date.parse("2015-07-15", "yyyy-MM-dd)  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            
            Notes: sẽ có trong phiên bản 1.1

        + daysInMonth(month, year): Trả về số ngày trong tháng.
            Date.daysInMonth(7, 2015)  = 31

        + compare(date1, date2): Trả về kết quả so sánh giữa 2 đối tượng ngày tháng.
            Notes: sẽ có trong phiên bản 1.1

        + max([date1, date2, ...]): Trả về đối tượng ngày tháng lớn nhất.
            Notes: sẽ có trong phiên bản 1.1

        + min([date1, date2, ...]): Trả về đối tượng ngày tháng nhỏ nhất.
            Notes: sẽ có trong phiên bản 1.1

    - Các method cho đối tượng dữ liệu String

        + format(partern): Convert ngày tháng theo chuỗi theo định dạng yêu cầu và trả về kết quả; partern mặc định hh:mm dd/MM/yyyy (Vn_vi)
            new Date().format("dd/MM/yy") = "15/07/15"
          
        + subtract(value, unit): Trừ đi một khoảng thời gian theo đơn vị thời gian (ở trên) của đối tượng ban đầu và trả về đối tượng mới.
            new Date().subtract(1, "month").format("dd/MM/yy") = "15/06/15"

        + add(value, unit): Trừ đi một khoảng thời gian theo đơn vị thời gian (ở trên) của đối tượng ban đầu và trả về đối tượng mới.
            new Date().add(1, "month").format("dd/MM/yy") = "15/06/15"

        + month(value): Lấy hoặc thiết lập tháng của đối tượng thời gian hiện tại: 1, 2, ... 12
            new Date().month() = 07
            new Date().month(12).format("dd/MM/yy") = "15/12/15";

        + year(value): Tương tự hàm month ở trên.
            new Date().year() = 2015
            new Date().year(2017).format("dd/MM/yy") = "15/07/17";

        + date(value): Lấy hoặc thiết lập ngày trong tháng của đối tượng thời gian hiện tại: 1, 2, ... 31
            new Date().date() = 15
            new Date().date(24).format("dd/MM/yy") = "24/07/17";

        + day(): Trả về thứ tự ngày trong tuần của đối tượng thời gian hiện tại tính từ chủ nhật: 0, 6
            new Date().day() = 03

        + weekOfYear():  Trả về thứ tự tuần trong năm của đối tượng thời gian hiện tại: 1, 2, ... 52
            new Date().weekOfYear() = 29

        + dayOfYear(): Trả về thứ tự ngày trong năm của đối tượng thời gian hiện tại: 1, 2, ... 365 (366)
            new Date().dayOfYear() = 196

        + hours(value): Lấy hoặc thiết lập giờ của đối tượng thời gian hiện tại: 1, 2, ... 23
            var d = new Date(); d.setHours(15); d.format("hh:mm")    = "15:33"

        + minutes(value): Tương tự hours

        + seconds(value): Tương tự hours

        + miniSeconds(value): Tương tự hours

        + quarter(): Trả về quý của đối tượng thời gian hiện tại
            new Date().quarter() = 3

        + endOf(): Thiết lập đối tượng thời gian hiện tại thành thời điểm cuối cùng theo đơn vị thời gian truyền vào và trả về ngày mới tương ứng.
            new Date().endOf("year").format("dd/MM/yyyy")   = "31/12/2015"          ( Ngày cuối cùng trong năm )
            new Date().endOf("month").format("dd/MM/yyyy")  = "31/07/2015"          ( Ngày cuối cùng trong tháng )
            new Date().endOf("day").format()                = "23:59 15/07/2015"    ( Thời điểm cuối cùng trong ngày)
            ...

        + startOf(): Tương tự endOf
            new Date().startOf("year").format("dd/MM/yyyy")   = "01/01/2015"          ( Ngày đầu tiên trong năm )
            new Date().startOf("month").format("dd/MM/yyyy")  = "01/07/2015"          ( Ngày đầu tiên trong tháng )
            new Date().startOf("day").format()                = "00:00 15/07/2015"    ( Thời điểm đầu tiên trong ngày)
            ...

        + isLeapYear(): Trả về giá trị xác định năm hiện tại có phải là năm nhuận không; true = năm nhuận; false = không.
            new Date().subtract(1, "year").isLeapYear() = true;
*/


bt_util_date_resource = {
    vi: {
        months: ["tháng 1", "tháng 2", "tháng 3", "tháng 4", "tháng 5", "tháng 6", "tháng 7", "tháng 8", "tháng 9", "tháng 10", "tháng 11", "tháng 12"],
        shortMonths: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
        weeks: ["chủ nhật", "thứ hai", "thứ ba", "thứ tư", "thứ năm", "thứ sáu", "thứ bảy"],
        shortWeeks: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
        times: {
            future: '%s tới',
            ago: '%s trước',
            s: 'vài giây',
            m: 'một phút',
            mm: '%d phút',
            h: 'một giờ',
            hh: '%d giờ',
            d: 'một ngày',
            dd: '%d ngày',
            M: 'một tháng',
            MM: '%d tháng',
            y: 'một năm',
            yy: '%d năm'
        },
        calendar: {
            sameDay: 'Hôm nay',
            nextDay: 'Ngày mai',
            lastDay: 'Hôm qua',
            sameWeek: 'Tuần này',
            nextWeek: 'Tuần tới',
            lastWeek: 'Tuần trước',
            sameMonth: 'Tháng này',
            lastMonth: 'Tháng trước'
        }
    }
};

(function (window) {
    "use strict";

    var _date, _prototype, _unitType, _formatObject, _formatTokens, _defaultFormat, _resource, _defaultFarseFormat;
    _date = Date;
    _prototype = _date.prototype;
    _unitType = {
        year: "year",
        quarter: "quarter",
        month: "month",
        week: "week",
        date: "date",
        day: "day",
        hour: "hours",
        minute: "minutes",
        second: "seconds",
        miniSecond: "miniSecond"
    };
    _formatTokens = /(\[[^\[]*\])|(\\)?(MM?M?M?|dd?d?|ww?|q|yy?yy?|a|A|hh?|HH?|mm?|ss?s?|z|.)/g;
    _defaultFormat = "hh:mm dd/MM/yyyy";
    _defaultFarseFormat = "yyyy-MM-ddTHH:mm:ss";
    _formatObject = {
        yy: "yy",                   // Năm: 96, 97, ... 14, 15
        yyyy: "yyyy",               // Năm: 1996, 1997, ... 2014, 2015
        q: "q",                     // Quý: 1, 2, 3, 4
        M: "M",                     // Tháng: 1, 2, ... 12
        MM: "MM",                   // Tháng: 01, 02, ... 12
        MMM: "MMM",                 // Tháng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Tháng: Tháng một, Tháng hai, ... Tháng mười hai
        w: "w",                     // Tuần: 1, 2, ... 52
        ww: "ww",                   // Tuần: 01, 02, ... 52
        d: "d",                     // Ngày trong tháng: 1, 2, ... 31
        dd: "dd",                   // Ngày trong tháng: 01, 02, ... 31
        ddd: "ddd",                 // Ngày trong tuần: Thứ 2, thứ 3, ... 
        h: "h",                     // Giờ (12): 1, 2, ... 12    
        hh: "hh",                   // Giờ (12): 01, 02, ... 12
        H: "H",                     // Giờ (24): 1, 2, ... 23
        HH: "HH",                   // Giờ (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Phút: 1, 2, ... 59
        mm: "mm",                   // Phút: 01, 02, ... 59
        s: "s",                     // Giây: 1, 2, ... 59
        ss: "ss",                   // Giây: 01, 02, ... 59
        sss: "sss",                 // Tích tắc: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST 
    };
    _resource = bt_util_date_resource["vi"];

    //#region Prototype Methods

    _prototype.format = function Date$format(partern) {
        /// <summary>
        /// Convert ngày tháng thành chuỗi theo định dạng yêu cầu
        /// </summary>
        /// <param name="partern">Định dạng chuỗi cần xuất ra.</param>
        var formats, leng, match, i, formatResult;
        var result = "";

        partern = partern || _defaultFormat;
        formats = partern.match(_formatTokens);
        leng = formats.length;

        for (i = 0; i < leng; i++) {
            match = formats[i].toString();
            match = match.replace("[", "").replace("]", "");
            result += isFormatting(match) ? this._getFormattingValue(match) : match;
        }

        return result;
    }

    _prototype.subtract = function Date$subtract(value, unit) {
        /// <summary>
        /// Trừ đi một khoảng thời gian và trả về khoảng thời gian mới từ thời gian ban đầu.
        /// </summary>
        /// <param name="value">Giá trị trừ</param>
        /// <param name="unit">Trường thời gian cần trừ</param>
        switch (unit) {
            case _unitType.year:
                this.year(this.year() - value);
                break;
            case _unitType.month:
                this.month(this.month() - value);
                break;
            case _unitType.date:
                this.date(this.date() - value);
                return this;
            case _unitType.hour:
                this.hours(this.hours() - value);
                break;
            case _unitType.minute:
                this.minutes(this.minutes() - value);
                break;
            case _unitType.second:
                this.seconds(this.seconds() - value);
                break;
            case _unitType.miniSecond:
                this.miniSeconds(this.miniSeconds() - value);
                break;
            default:
                break;
        }

        return this;
    }

    _prototype.add = function Date$add(value, unit) {
        /// <summary>
        /// Thêm một khoảng thời gian vào thời gian ban đầu và trả về thời gian mới
        /// </summary>
        /// <param name="value">Khoảng thời gian cần thêm</param>
        /// <param name="unit">Trường thời gian cần thêm</param>
        switch (unit) {
            case _unitType.year:
                this.year(this.year() + value);
                break;
            case _unitType.month:
                this.month(this.month() + value);
                break;
            case _unitType.date:
                this.date(this.date() + value);
                return this;
            case _unitType.hour:
                this.hours(this.hours() + value);
                break;
            case _unitType.minute:
                this.minutes(this.minutes() + value);
                break;
            case _unitType.second:
                this.seconds(this.seconds() + value);
                break;
            case _unitType.miniSecond:
                this.miniSeconds(this.miniSeconds() + value);
                break;
            default:
                break;
        }

        return this;
    }

    _prototype.month = function Date$month(value) {
        /// <summary>
        /// Lấy hoặc thiết lập tháng của ngày này: 1,2 ... 12
        /// </summary>

        if (value == 2 && this.date() > 28) {
            this.date(28);
        }

        if (hasValue(value)) {
            this.setMonth(value - 1);
            return this;
        }

        return this.getMonth() + 1;
    }

    _prototype.year = function Date$year(value) {
        /// <summary>
        /// Lấy hoặc thiết lập năm của ngày này
        /// </summary>
        if (hasValue(value)) {
            this.setFullYear(value);
            return this;
        }

        return this.getFullYear();
    }

    _prototype.date = function Date$date(value) {
        /// <summary>
        /// Lấy hoặc thiết lập ngày trong tháng của ngày này
        /// </summary>
        /// <param name="value" type="int">Ngày cần set, gán null để lấy giá trị.</param>

        if (hasValue(value)) {
            this.setDate(value);
            return this;
        }

        return this.getDate();
    }

    _prototype.day = function Date$day() {
        /// <summary>
        /// Trả về ngày trong tuần của ngày này
        /// </summary>

        return this.getDay();
    }

    _prototype.weekOfYear = function Date$week() {
        /// <summary>
        /// Trả về tuần trong năm của ngày này
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - firstDay) / 86400000) + firstDay.day() - 1) / 7);
    }

    _prototype.dayOfYear = function Date$dayOfYear() {
        /// <summary>
        /// Trả về ngày trong năm của ngày này, ví dụ ngày thứ 234 của 365
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((this - firstDay) / 86400000);
    }

    _prototype.hours = function Date$hours(value) {
        /// <summary>
        /// Lấy hoặc thiết lập giờ của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setHours(value);
            return this;
        }

        return this.getHours();
    }

    _prototype.minutes = function Date$minutes(value) {
        /// <summary>
        /// Lấy hoặc thiết lập phút của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setMinutes(value);
            return this;
        }

        return this.getMinutes();
    }

    _prototype.seconds = function Date$seconds(value) {
        /// <summary>
        /// Lấy hoặc thiết lập giây của ngày này
        /// </summary>
        if (hasValue(value)) {
            this.setSeconds(value);
            return this;
        }

        return this.getSeconds();
    }

    _prototype.miniSeconds = function Date$miniSeconds(value) {
        /// <summary>
        /// Lấy hoặc thiết lập mini giây của ngày này
        /// </summary>

        if (hasValue(value)) {
            this.setMilliseconds(value);
            return this;
        }

        return this.getMilliseconds();
    }

    _prototype.quarter = function Date$quarter() {
        /// <summary>
        /// Trả về quý của ngày này
        /// </summary>
        return Math.floor(((this.month() - 1) / 3) + 1);
    }

    _prototype.endOf = function Date$endOf(unit) {
        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm cuối cùng của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit">
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày cuối cùng trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày cuối cùng của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày cuối cùng trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày cuối cùng trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 23:59:59 của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 59:59 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 59s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 999ms của giây hiện tại.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "Đơn vị truyền vào không hợp lệ.";
        }

        var year, quarter, month, days, date;
        year = this.year();
        quarter = this.quarter();
        month = this.month();
        date = this.date();

        switch (unit) {
            case _unitType.year:
                this.month(12).date(31).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.quarter:
                month = quarter * 3;
                days = _date.daysInMonth(month, year);
                this.month(month).date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.month:
                days = _date.daysInMonth(month, year);
                this.date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.week:
                days = date - this.day() + 6;
                this.date(days).hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.day:
                this.hours(23).minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.hour:
                this.minutes(59).seconds(59).miniSeconds(999);
                break;
            case _unitType.minute:
                this.seconds(59).miniSeconds(999);
                break;
            case _unitType.second:
                this.miniSeconds(999);
                break;
        }

        return this;
    }

    _prototype.startOf = function Date$startOf(unit) {
        /// <summary>
        /// Thiết lập ngày hiện tại thành thời điểm đầu tiên của đơn vị truyền vào và trả về ngày mới tương ứng.
        /// </summary>
        /// <param name="unit">
        /// Đơn vị:
        /// - "year": thiết lập ngày hiện tại về ngày đầu tiên trong năm với ngày hiện tại.
        /// - "quarter": thiết lập ngày hiện tại về ngày đầu tiên của quý với ngày hiện tại
        /// - "month": thiết lập ngày hiện tại về ngày đầu tiên trong tháng với ngày hiện tại.
        /// - "week": thiết lập ngày hiện tại về ngày đầu tiên trong tuần với ngày hiện tại.
        /// - "day": thiết lập ngày hiện tại về thời điểm 12:00:00 am của ngày hiện tại.
        /// - "hour": thiết lập ngày hiện tại về thời điểm 00:00 của giờ hiện tại.
        /// - "minute": thiết lập ngày hiện tại về thời điểm 00s của phút hiện tại.
        /// - "second": thiết lập ngày hiện tại về thời điểm 000ms của giây hiện tại.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "Đơn vị truyền vào không hợp lệ.";
        }

        var quarter, month, days, date;
        quarter = this.quarter();
        date = this.date();

        switch (unit) {
            case _unitType.year:
                this.month(1).date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.quarter:
                month = quarter * 3 - 2;
                this.month(month).date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.month:
                this.date(1).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.week:
                days = date - this.day();
                this.date(days).hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.day:
                this.hours(0).minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.hour:
                this.minutes(0).seconds(0).miniSeconds(0);
                break;
            case _unitType.minute:
                this.seconds(0).miniSeconds(0);
                break;
            case _unitType.second:
                this.miniSeconds(0);
                break;
        }

        return this;
    }

    _prototype.isLeapYear = function Date$isLeapYear() {
        /// <summary>
        /// Trả về giá trị xác định năm hiện tại có phải là năm nhuận không.
        /// </summary>
        return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
    }

    _prototype._getFormattingValue = function Date$_getFormattingValue(partern) {
        var result;
        switch (partern) {
            case _formatObject.yy:
                result = this.year().toString().substr(2, 2);
                break;
            case _formatObject.yyyy:
                result = this.year();
                break;
            case _formatObject.q:
                result = this.quarter();
                break;
            case _formatObject.M:
                result = this.month();
                break;
            case _formatObject.MM:
                result = toDigit(this.month());
                break;
            case _formatObject.MMM:
                result = _resource.shortMonths[this.month() - 1];
                break;
            case _formatObject.MMMM:
                result = _resource.months[this.month() - 1];
                break;
            case _formatObject.d:
                result = this.date();
                break;
            case _formatObject.dd:
                result = toDigit(this.date());
                break;
            case _formatObject.ddd:
                result = _resource.weeks[this.day()];
                break;
            case _formatObject.h:
                result = to12Hour(this.hours());
                break;
            case _formatObject.hh:
                result = toDigit(to12Hour(this.hours()));
                break;
            case _formatObject.H:
                result = this.hours();
                break;
            case _formatObject.HH:
                result = toDigit(this.hours());
                break;
            case _formatObject.m:
                result = this.minutes();
                break;
            case _formatObject.mm:
                result = toDigit(this.minutes());
                break;
            case _formatObject.s:
                result = this.seconds();
                break;
            case _formatObject.ss:
                result = toDigit(this.seconds());
                break;
            case _formatObject.S:
                result = this.miniSeconds();
                break;
            case _formatObject.Z:
                result = this.year();
                break;
            default:
                result = "";
                break;
        }
        return result;
    }

    _prototype.relativeDate = function () {
        /// <summary>
        /// Time ago
        /// </summary>

        var seconds = Math.floor((new Date() - this) / 1000);
        var interval = Math.floor(seconds / 31536000);

        if (interval > 1) {
            return interval + " năm";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " Thg";
        }
        interval = Math.floor(seconds / 604800);
        if (interval > 1) {
            return interval + " tuần";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " ngày";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " giờ";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " phút";
        }
        return "Vừa mới";
    }

    _prototype.toServerString = function Date$toServerString() {
        //2017-10-25T11:04:23
        return this.format(_defaultFarseFormat);
    }

    _prototype.getVNDay = function () {
        var day = this.getDay();
        return bt_util_date_resource.vi.weeks[day];
    }

    _prototype.timeInWord = function () {
        /// <summary>
        ///
        /// </summary>

        var date = this; var distanceMillis;
        var now = Date.now();

        var today = now.format("yyyyMMwwdd");
        var lastDay = now.subtract(1, _unitType.date).format("yyyyMMwwdd");
        var week = now.format("yyyyMMww");
        var lastWeek = now.subtract(1, _unitType.week).format("yyyyMMww");
        var month = now.format("yyyyMM");
        var lastMonth = now.subtract(1, _unitType.month).format("yyyyMM");

        var year = now.format("yyyy");

        var dateFormat = date.format("yyyyMMwwddhh");
        if (dateFormat.indexOf(today) === 0) {
            return _resource.calendar.sameDay;
        }
        if (dateFormat.indexOf(lastDay) === 0) {
            return _resource.calendar.lastDay;
        }

        if (dateFormat.indexOf(week) === 0) {
            return _resource.calendar.sameWeek;
        }
        if (dateFormat.indexOf(lastWeek) === 0) {
            return _resource.calendar.lastWeek;
        }

        if (dateFormat.indexOf(month) === 0) {
            return _resource.calendar.sameMonth;
        }
        if (dateFormat.indexOf(lastMonth) === 0) {
            return _resource.calendar.lastMonth;
        }

        if (dateFormat.indexOf(year) === 0)
            return date.format("[Tháng] MM");

        return date.format("[Tháng] MM - yyyy");
    }

    _prototype.isToday = function () {
        var today = Date.now().format("yyyyMMdd");
        var date = this.format("yyyyMMdd");
        return date === today;
    }

    _prototype.isYesterday = function () {
        var yesterday = new Date().add(-1, _unitType.date).format("yyyyMMdd");
        var date = this.format("yyyyMMdd");
        return date === yesterday;
    },

    //#endregion

    //#region Statis Methods

    _date.parse = function Date$parse(value, format) {
        /// <summary>
        /// Chuyển đổi string thành định dạng ngày tháng theo format chỉ định
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        format = format || _defaultFarseFormat;

        if (!value)
            return null;

        if (Globalize) {
            return Globalize.parseDate(value, format)
        }

        return null;
    }

    _date.parseFromIsoString = function Date$parseFromIsoString(input) {
        var arr = input.split(/[^0-9]/);
        return new Date(arr[0], arr[1] - 1, arr[2], arr[3], arr[4], arr[5]);
    }

    _date.daysInMonth = function Date$daysInMonth(month, year) {
        /// <summary>
        /// Trả về số ngày trong tháng
        /// </summary>
        /// <param name="month">Tháng</param>
        /// <param name="year">Năm</param>
        return new Date(year, month, 0).date();
    }

    _date.compare = function Date$compare(date1, date2) {
        /// <summary>
        /// So sánh 2 ngày tháng và trả về kết quả so sánh nhỏ hơn, bằng, lớn hơn.
        /// </summary>
        /// <param name="date1">Ngày 1</param>
        /// <param name="date2">Ngày 2</param>
        /// <returns type="int">1-ngày 1 lớn hơn ngày 2; 0-ngày 1 bằng ngày 2; -1-ngày 1 nhỏ hơn ngày 2</returns>
    }

    _date.max = function () {
        /// <summary>
        /// Trả về ngày lớn nhất của kiểu dữ liệu Date
        /// </summary>
    }

    _date.min = function () {
        /// <summary>
        /// Trả về ngày nhỏ nhất của kiểu dữ liệu Date
        /// </summary>
    }

    _date.now = function () {
        return new Date;
    }

    //#endregion

    //#region Private Methods

    var hasValue = function (value) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị phải là null hay không.
        /// </summary>
        /// <param name="value">Giá trị</param>
        return value !== undefined && value !== null;
    };

    var _isObject = function (obj) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị có phải là một object không
        /// </summary>
        /// <param name="obj"></param>
        return typeof obj === "object";
    };

    var _isNumber = function (val) {
        /// <summary>
        /// Trả về kết quả kiểm tra giá trị hiện tải có phải là 1 number hay không.
        /// </summary>
        /// <param name="val"></param>

    };

    var toDigit = function (number) {
        return number > 9 ? number : ("0" + number);
    };

    var to12Hour = function (value) {
        return value > 12 ? (value - 12) : value;
    };

    var isFormatting = function (value) {
        return _formatObject[value] !== undefined;
    };

    //#endregion
})(this);
// bt.util.string.js

/*
    Author: TienBV
    DateCreated: 25/06/2015
    Version: 1.0

    Description:

    - Các method cho kiểu dữ liệu String

        + format:               String.format("Tôi là {0}", "TienBV")   => return: "Tôi là TienBV"

        + isNullOrEmpty:        String.isNullOrEmpty("")                => return: true

        + isNullOrWhiteSpace:   String.isNullOrWhiteSpace("   ")        => return: true

        + isString:             String.isString("TienBV")               => return: true

    - Các method cho đối tượng dữ liệu String

        + trim, trimStart, trimEnd: cắt các ký tự trống ở 2 đầu chuỗi, hoặc đầu chuỗi, hoặc cuối chuỗi
            "   Bkav vô đối   ".trim()        = "Bkav vô đối"; 
            "   Bkav vô đối   ".trimStart()   = "Bkav vô đối   "; 
            "   Bkav vô đối   ".trimEnd()     = "   Bkav vô đối";

        + startWith(prefix, ignoreCase): Trả về giá trị xác định chuỗi cần kiểm tra có phải nằm ở đầu chuỗi hiện tại không.
            "Bkav vô đối".startWith("Bkav")         = true;  
            "Bkav vô đối".startWith("bKaV", true)   = true;

        + endWith(prefix, ignoreCase)      => tương tự startWith

        + equals(value, ignoreCase): Trả về giá trị xác định chuỗi cung cấp có bằng với chuỗi hiện tại không.
            "Bkav vô đối".equals("Bkav vô đối")         = true; 
            "Bkav vô đối".equals("Bkav Vô Đối", true)   = true

        + remove(startIndex, count): Trả về chuỗi sau khi đã xóa những ký tự ở các vị trí truyền vào.
            "Bkav vô đối".remove(4)             = "Bkav";
            "Bkav vô đối".remove(0, 5)          = "vô đối";

        + replaceAll(searchValue, replaceValue): Trả về chuỗi sau khi thay thế các chuỗi cũ được chỉ định bằng chuỗi mới.
            "Bkav vô đối".replaceAll("v", "i")  = "Bkai iô đối";

        + contains(value): Trả về giá trị xác định chuỗi cần kiểm tra có thuộc chuỗi hiện tại không.
            "Bkav vô đối".contains("vô")        = true;

        + toCharArray():  Trả về một mảng tất cả các ký tự trong chuỗi
            "Bkav".toCharArray()        = ["B", "k", "a", "v"]

        + reverse(): đảo chuỗi
            "Bkav".reverse()            = "vakB";

        + removeVietnamChars(): loại bỏ dấu tiếng việt
            "Bkav vô đối".removeVietnamChars()      = "Bkav vo doi";

*/

(function () { var t = String, n = t.prototype, i = ["aáàảãạăắằẳẵặâấầẩẫậ", "AÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ", "dđ", "DĐ", "eéèẻẽẹêếềểễệ", "EÉÈẺẼẸÊẾỀỂỄỆ", "iíìỉĩị", "IÍÌỈĨỊ", "oóòỏõọơớờởỡợôốồổỗộ", "OÓÒỎÕỌƠỚỜỞỠỢÔỐỒỔỖỘ", "uưứừửữựúùủũụ", "UƯỨỪỬỮỰÚÙỦŨỤ", "yýỳỷỹỵ", "YÝỲỶỸỴ"]; n.trim = function () { return this.replace(/^\s+|\s+$/g, "") }; n.trimStart = function () { return this.replace(/^\s+/, "") }; n.trimEnd = function () { return this.replace(/\s+$/, "") }; n.startWith = function (n, t) { if (!String.isString(n)) throw "Giá trị truyền vào không hợp lệ"; return t = t || !1, n.equals(this.substr(0, n.length), t) }; n.endWith = function (n, t) { if (!String.isString(n)) throw "Giá trị truyền vào không hợp lệ"; return t = t || !1, n.equals(this.substr(this.length - n.length), t) }; n.equals = function (n, t) { if (!String.isString(n)) throw "Giá trị truyền vào không hợp lệ"; return t = t || !1, t ? this.toLowerCase() === n.toLowerCase() : this.toString() === n }; n.remove = function (n, t) { var i; if (typeof n != "number") throw "Start Index phải là một chữ số"; return i = this.slice(0, n), t != undefined && typeof t == "number" && (i += this.slice(n + t)), i }; n.replaceAll = function (n, t) { if (!String.isString(n) || !String.isString(t)) throw "Các tham số truyền vào phải là string"; var i = new RegExp(n.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&"), "g"); return this.replace(i, t) }; n.contains = function (n) { if (!String.isString(n)) throw "Tham số truyền vào phải là chuỗi"; return this.indexOf(n) > -1 }; n.toCharArray = function () { return this.split("") }; n.reverse = function () { return this.split("").reverse().join("") }; n.forEach = function (n) { if (typeof n == "function") for (var i = this.length, t = 0; t < i; t++) n(this.charAt(t), t) }; n.removeVietnamChars = function () { var t = [], r = this, n; return r.forEach(function (r, u) { for (t[u] = r, n = 0; n < i.length; n++) if (i[n].contains(r)) { t[u] = i[n][0]; break } }), t.join("") }; t.format = function () { return String._toFormattedString(!1, arguments) }; t.isNullOrEmpty = function (n) { return n === null || n === undefined || n === "" }; t.isNUllOrWhiteSpace = function (n) { return n = n.trim(), String.isNullOrEmpty(n) }; t.isString = function (n) { return typeof n == "string" }; t._toFormattedString = function (n, t) { for (var o, u, c, r, e = "", f = t[0], i = 0; ;) { if (o = f.indexOf("{", i), u = f.indexOf("}", i), o < 0 && u < 0) { e += f.slice(i); break } if (u > 0 && (u < o || o < 0)) { if (f.charAt(u + 1) !== "}") throw new Error("format stringFormatBraceMismatch"); e += f.slice(i, u + 1); i = u + 2; continue } if (e += f.slice(i, o), i = o + 1, f.charAt(i) === "{") { e += "{"; i++; continue } if (u < 0) throw new Error("format stringFormatBraceMismatch"); var s = f.substring(i, u), h = s.indexOf(":"), l = parseInt(h < 0 ? s : s.substring(0, h), 10) + 1; if (isNaN(l)) throw new Error("format stringFormatInvalid"); c = h < 0 ? "" : s.substring(h + 1); r = t[l]; (typeof r == "undefined" || r === null) && (r = ""); e += r.toFormattedString ? r.toFormattedString(c) : n && r.localeFormat ? r.localeFormat(c) : r.format ? r.format(c) : r.toString(); i = u + 1 } return e } })(this);


(function (egov) {
    var _readonlyExtensions = ['pdf', 'png', 'gif', 'jpg', 'jpeg', 'bmp'];
    var _signRegex = /(.doc|.docx|.pdf|.xls|.xlsx)$/i;

    var FileExtension = {
        getFileName: function (fileName) {
            var _dotIndex = fileName.lastIndexOf(".");
            if (_dotIndex === -1) return fileName;

            return fileName.substring(0, _dotIndex);
        },

        getExtension: function (fileName) {
            var _dotIndex = fileName.lastIndexOf(".");
            if (_dotIndex === -1) return "";

            var ext = fileName.substring(_dotIndex + 1, fileName.length);
            return ext.toLowerCase();
        },

        getExtensionWithDot: function (fileName) {
            var ext = this.getExtension(fileName);
            return ext === "" ? "" : ("." + ext);
        },

        isPdf: function (fileName) {
            var ext = this.getExtensionWithDot(fileName);
            return ext === ".pdf";
        },

        isMsOfficeFile: function (fileName) {
            var ext = this.getExtensionWithDot(fileName);
            return ext === ".doc" || ext === ".docx";
        },

        isReadonly: function (fileName) {
            var extension = this.getExtension(fileName);
            return _readonlyExtensions.indexOf(extension) >= 0;
        },

        isForSign: function (fileName) {
            var ext = this.getExtensionWithDot(fileName);
            return _signRegex.test(ext);
        },

        getSizeText: function (filesize) {
            var oneKiloByte = 1024;
            var oneMegaByte = 1048576;
            var oneGigaByte = 1073741824;

            if (filesize >= oneGigaByte) {
                return Math.round(filesize / oneGigaByte) + " GB";
            }
            if (filesize >= oneMegaByte) {
                return Math.round(filesize / oneMegaByte) + " MB";
            }
            if (filesize >= oneKiloByte) {
                return Math.round(filesize / oneKiloByte) + " KB";
            }

            return filesize + " bytes";
        },

        downloadUri: function (uri, name) {
            var link = document.createElement("a");
            link.download = name;
            link.href = uri;
            link.style.display = 'none';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
            delete link;
        }
    };

    egov.fileExtension = FileExtension;
})(window.egov || {});
(function () {
    "use strict";

    // Danh sách các cookie name
    var cookies = {
        RecentTabs: 'RecentTabs',
        DocumentHeaderWidth: 'DocumentHeaderWidth_',
        PageSize: 'PageSize',
        IgnoreConfirmRelations: "ConfirmRelation",
        viewSize: "ViewSize",
        quickView: "QuickView",
        mudimMethod: "MudimMethod",
        useVietKey: "UseVietKey",
        displayPopupTransferTheoLo: "displayPopupTransferTheoLo",
        lastApp: "lastApp",
    };

    /// <summary>Lớp quản lý tất cả các cookie</summary>
    var Cookie = {

        // Lưu tab đang mở vào cookie
        addRecentTab: function (user, tabModel) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (!recentTab) {
                recentTab = {};
            }
            var recentUserTab = recentTab[user];
            if (recentUserTab === undefined) {
                recentUserTab = [];
            }
            recentUserTab.push(tabModel);
            recentTab[user] = recentUserTab;
            $.cookie(cookies.RecentTabs, JSON.stringify(recentTab), { secure: true });
        },

        // Trả về danh sách tất cả các tab đã lưu vào cookie
        getRecentTab: function (user) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (recentTab) {
                return recentTab[user];
            }
            return [];
        },

        // Xóa một tab khỏi cookie
        deleteRecentTab: function (user, tabModel) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (recentTab) {
                recentTab[user] = _.reject(recentTab[user], function (itm) {
                    return itm.id === tabModel.id;
                });

                $.cookie(cookies.RecentTabs, JSON.stringify(recentTab), { secure: true });
            }
        },

        // Lưu thông tin các cột và độ rộng các cột vào cookie
        addDocumentHeaderWidth: function (functionId, value) {
            $.cookie(cookies.DocumentHeaderWidth + functionId, JSON.stringify(value), { expires: 7, path: "/", secure: true });
        },

        // Lấy ra danh sách các cột và độ rộng đã được lưu cookie
        getDocumentHeaderWidth: function (functionId) {
            return this.getCookie(cookies.DocumentHeaderWidth + functionId);
        },

        // Thêm cookie pagesize
        /// <param name="pageSize" type="int">PageSize</param>
        addPageSize: function (pageSize) {
            if (pageSize && typeof pageSize === 'number') {
                $.cookie(cookies.PageSize, pageSize, { expires: 7, path: "/", secure: true });
            }
        },

        // Trả về cookie pageSize
        getPageSize: function () {
            var value = $.cookie(cookies.PageSize);
            return parseInt(value);
        },

        // private: Lấy ra cookie theo tên
        getCookie: function (name) {
            var value = $.cookie(name);
            return JSON.parse(value);
        },

        // Thiết lập bỏ qua confirm văn bản liên quan khi bàn giao
        setIgnoreConfirmRelation: function (value) {
            $.cookie(cookies.IgnoreConfirmRelations, value, { secure: true });
        },

        // Trả về giá trị thiết lập confirm văn bản liên quan
        getIgnoreConfirmRelation: function () {
            var value = $.cookie(cookies.IgnoreConfirmRelations);
            return value;
        },

        // Kích thước trang chủ
        viewSize: function (value) {
            if (value === undefined) {
                return parseInt($.cookie(cookies.viewSize));
            } else {
                $.cookie(cookies.viewSize, value, { expires: 30, secure: true });
            }
        },

        ///Thiết lập hiển thị tóm tắt văn bản
        setQuickView: function (value) {
            $.cookie(cookies.quickView, value, { secure: true });
        },

        ///Lấy thiết lập hiển thị tóm tắt văn bản
        getQuickView: function () {
            return parseInt($.cookie(cookies.quickView));
        },

        ///Thiết lập ứng dụng chạy sau cùng
        setLastApp: function (value) {
            $.cookie(cookies.lastApp, value, { secure: true });
        },

        ///Lấy thiết lập ứng dụng chạy sau cùng
        getLastApp: function () {
            return $.cookie(cookies.lastApp);
        },

        ///Thiết lập gõ tiếng việt
        setMudimMethod: function (value) {
            $.cookie(cookies.mudimMethod, value, { path: '/', secure: true });
        },

        //Lấy thiết lập gõ tiếng việt
        getMudimMethod: function () {
            return parseInt($.cookie(cookies.mudimMethod));
        },

        ///Lấy thiết lập gõ tiếng việt
        getUseVietKey: function () {
            return parseInt($.cookie(cookies.useVietKey));
        },

        //lấy hoặc thiết lập trạng thái có hiển thị popup cho ý kiến khi chuyển theo lô
        displayPopupTransferTheoLo: function (value) {
            if (value === undefined) {
                var val = $.cookie(cookies.displayPopupTransferTheoLo);
                return val === "true" || val == 1;
            } else {
                $.cookie(cookies.displayPopupTransferTheoLo, value, { path: '/', secure: true });
            }
        },


    };

    window.eGovCookie = Cookie;
})();
(function () {
    "use strict";

    // Danh sách các key trong localStorage
    var localStorageKey = {
        RecentTabs: 'RecentTabs',
        DocumentHeaderWidth: 'DocumentHeaderWidth_',
        PageSize: 'PageSize',
        IgnoreConfirmRelations: "ConfirmRelation",
        viewSize: "ViewSize",
        quickView: "QuickView",
        mudimMethod: "MudimMethod",
        useVietKey: "UseVietKey",
        displayPopupTransferTheoLo: "displayPopupTransferTheoLo",
        lastApp: "lastApp",
    };

    /// <summary>Lớp quản lý tất cả các localStorage</summary>
    var Storage = {

        // Lưu tab đang mở vào localStorage
        addRecentTab: function (user, tabModel) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (!recentTab) {
                recentTab = {};
            }

            var recentUserTab = recentTab[user];
            if (recentUserTab === undefined) {
                recentUserTab = [];
            }

            recentUserTab.push(tabModel);
            recentTab[user] = recentUserTab;

            egov.locache.setDefault(localStorageKey.RecentTabs, JSON.stringify(recentTab));
        },

        // Trả về danh sách tất cả các tab đã lưu vào localStorage
        getRecentTab: function (user) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (recentTab) {
                return recentTab[user];
            }

            return [];
        },

        // Xóa một tab khỏi localStorage
        deleteRecentTab: function (user, tabModel) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (recentTab) {
                recentTab[user] = _.reject(recentTab[user], function (itm) {
                    return itm.id === tabModel.id;
                });

                egov.locache.setDefault(localStorageKey.RecentTabs, JSON.stringify(recentTab));
            }
        },

        // Lưu thông tin các cột và độ rộng các cột vào localStorage
        addDocumentHeaderWidth: function (functionId, value) {
            egov.locache.setDefault(localStorageKey.DocumentHeaderWidth + functionId, JSON.stringify(value));
        },

        // Lấy ra danh sách các cột và độ rộng đã được lưu localStorage
        getDocumentHeaderWidth: function (functionId) {
            return this.getLocalStorage(localStorageKey.DocumentHeaderWidth + functionId);
        },

        // Thêm localStorage pagesize
        /// <param name="pageSize" type="int">PageSize</param>
        addPageSize: function (pageSize) {
            if (pageSize && typeof pageSize === 'number') {
                egov.locache.setDefault(localStorageKey.PageSize, pageSize);
            }
        },

        // Trả về localStorage pageSize
        getPageSize: function () {
            var value = egov.locache.getDefault(localStorageKey.PageSize);
            return parseInt(value);
        },

        // private: Lấy ra localStorage theo tên
        getLocalStorage: function (name) {
            var value = egov.locache.getDefault(name);
            return JSON.parse(value);
        },

        // Thiết lập bỏ qua confirm văn bản liên quan khi bàn giao
        setIgnoreConfirmRelation: function (value) {
            egov.locache.setDefault(localStorageKey.IgnoreConfirmRelations, value);
        },

        // Trả về giá trị thiết lập confirm văn bản liên quan
        getIgnoreConfirmRelation: function () {
            var value = egov.locache.getDefault(localStorageKey.IgnoreConfirmRelations);
            return value;
        },

        // Kích thước trang chủ
        viewSize: function (value) {
            if (value === undefined) {
                return parseInt(egov.locache.getDefault(localStorageKey.viewSize));
            } else {
                egov.locache.setDefault(localStorageKey.viewSize, value);
            }
        },

        ///Thiết lập hiển thị tóm tắt văn bản
        setQuickView: function (value) {
            egov.locache.setDefault(localStorageKey.quickView, value);
        },

        ///Lấy thiết lập hiển thị tóm tắt văn bản
        getQuickView: function () {
            var value = parseInt(egov.locache.getDefault(localStorageKey.quickView));
            return value;
        },

        ///Thiết lập ứng dụng chạy sau cùng
        setLastApp: function (value) {
            egov.locache.setDefault(localStorageKey.lastApp, value);
        },

        ///Lấy thiết lập ứng dụng chạy sau cùng
        getLastApp: function () {
            return egov.locache.getDefault(localStorageKey.lastApp);
        },

        ///Thiết lập gõ tiếng việt
        setMudimMethod: function (value) {
            egov.locache.setDefault(localStorageKey.mudimMethod, value);
        },

        //Lấy thiết lập gõ tiếng việt
        getMudimMethod: function () {
            return parseInt(egov.locache.getDefault(localStorageKey.mudimMethod));
        },

        ///Lấy thiết lập gõ tiếng việt
        getUseVietKey: function () {
            return parseInt(egov.locache.getDefault(localStorageKey.useVietKey));
        },

        //lấy hoặc thiết lập trạng thái có hiển thị popup cho ý kiến khi chuyển theo lô
        displayPopupTransferTheoLo: function (value) {
            if (value === undefined) {
                var val = egov.locache.getDefault(localStorageKey.displayPopupTransferTheoLo);
                return val === "true" || val == 1;
            } else {
                egov.locache.setDefault(localStorageKey.displayPopupTransferTheoLo, value);
            }
        },
    };

    window.eGovLocalStorage = Storage;
})();
(function () {
    "use strict";

    //#region Private fields

    var messageButtons = {
        Ok: 1,
        YesNo: 2,
        OkCancel: 3
    };

    var messageResult = {
        Ok: 1,
        Yes: 2,
        No: 3,
        Cancel: 4
    };

    var messageTypes = {
        success: 1,
        error: 2,
        warning: 3,
        processing: 4
    };

    //#endregion

    /// <summary>Đối tượng View quản lý thông báo của eGov</summary>
    var eGovMessage = Backbone.View.extend({

        el: '.alert',

        // Khởi tạo
        initialize: function () {
            this.messageButtons = messageButtons;
            this.messageResult = messageResult;
            this.messageTypes = messageTypes;
            this.autoHide = false;
        },

        // Hiển thị thông báo dạng alert
        show: function (message, title, messageButton, okCallBackFunction, cancelCallBackFunction, settings) {
            this.hide();
            require(['dialog'], function () {
                var defaultSettings = _getDialogSetting();
                if (settings) {
                    defaultSettings = $.extend(defaultSettings, settings);
                }

                var dialogObj = _getDialogObj();
                if (typeof message !== 'string') {
                    message = '';
                }
                dialogObj.html(message);

                if (typeof title === 'string' && title !== '') {
                    defaultSettings.title = title;
                }

                defaultSettings.buttons = _getDialogButtons(messageButton, okCallBackFunction, cancelCallBackFunction, dialogObj);
                dialogObj.dialog(defaultSettings);
            });
        },

        prompt: function (title, confirmButtonName, callback, isCheckNullOrEmpty, valueDefault) {
            /// <summary>
            /// Hiển thị Prompt giống như javascript
            /// </summary>
            /// <param name="title">Tiêu đề</param>
            /// <param name="callback">Hàm thực thi sau khi comfirm.</param>
            /// <param name="isCheckNullOrEmpty">Có kiểm tra nếu là null hoặc rỗng thì validate</param>
            /// <param name="valueDefault">Giá trị mặc định khi bật dialog</param>
            var $el = $('<div><textarea class="form-control" row="4" style="height: 80px"></textarea></div>');
            $el.dialog({
                title: title,
                width: '400px',
                height: '120px',
                draggable: true,
                keyboard: true,
                buttons: [
                     {
                         text: confirmButtonName,
                         className: 'btn-primary',
                         click: function () {
                             var value = $el.find("textarea").val();
                             if (isCheckNullOrEmpty && (value === '' || value == null)) {
                                 $el.find("textarea").addClass('input-validation-error').first().focus();
                                 return;
                             }

                             if (typeof callback === 'function') {
                                 callback(value);
                             }

                             $el.dialog('destroy');
                         }
                     }
                    , {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            $el.dialog('destroy');
                        }
                    }
                ]
            });

            var $textarea = $el.find('textarea');
            if (valueDefault) {
                $textarea.focus().val(valueDefault);
            }
            else {
                $textarea.focus();
            }
        },

        promptHasConfirmDisplay: function (options) {
            //title, confirmButtonName, callback,
            //isCheckNullOrEmpty, callbackConfirm,
            //hasAutoComplete, addTemplateButtonName,
            //callbackCloseForm) {
            /// <summary>
            /// Hiển thị Prompt giống như javascript
            /// </summary>
            /// <param name="title">Tiêu đề</param>
            /// <param name="callback">Hàm thực thi sau khi comfirm.</param>
            /// <param name="isCheckNullOrEmpty">Có kiểm tra nếu là null hoặc rỗng thì validate</param>

            var that = this;
            var $el = $('<div><textarea class="form-control" row="5" style="height: 120px"></textarea></div>');
            var textArea = $el.find('textarea');
            $el.dialog({
                title: options.title,
                width: '500px',
                height: '150px',
                draggable: true,
                keyboard: true,
                confirm: {
                    id: "hasDisplay",
                    text: options.confirmTitleName,
                    style: {
                        'float': 'left',
                        'display': 'inline-block',
                        'font-size': '13px',
                        'font-weight': 'normal'
                    },
                    callback: function (value) {
                        if (typeof options.callbackConfirm === 'function') {
                            options.callbackConfirm(value);
                        }
                    }
                },
                buttons: [
                            {
                                text: options.confirmButtonName,
                                className: 'btn-primary',
                                click: function () {
                                    var value = $el.find("textarea").val();
                                    if (options.isCheckNullOrEmpty && (value === '' || value == null)) {
                                        $el.find("textarea").addClass('input-validation-error').first().focus();
                                        return;
                                    }

                                    if (typeof options.callback === 'function') {
                                        options.callback(value);
                                    }

                                    $el.dialog('destroy');
                                }
                            }
                            , {
                                text: egov.resources.common.closeButton,
                                className: 'btn-close',
                                click: function () {
                                    if (typeof options.callbackCloseForm === 'function') {
                                        options.callbackCloseForm();
                                    }
                                    $el.dialog('destroy');
                                }
                            }
                ]
            });

            textArea.focus();
        },

        // Hiển thị thông báo dạng notify
        notification: function (message, messageType, autoHide) {
            if (message === "hide") {
                this.hide();
                return;
            }

            if (autoHide == undefined) {
                autoHide = true;
            }
            if (messageType == undefined) {
                messageType = this.messageTypes.success;
            }
            this.$el.removeClass();
            this.$el.addClass(_getAlertTypeClass(messageType));
            this.$el.html(message);

            if (autoHide) {
                var that = this;
                this.$el.fadeIn();
                setTimeout(function () {
                    that.hide();
                }, 10000);
            }
            else {
                this.$el.fadeIn();
            }
        },

        /// <summary>Hiển thị message đang xử lý</summary>
        processing: function (message, autoHide) {
            this.notification(message, this.messageTypes.processing, autoHide);
        },

        /// <summary>Hiển thị message thông báo lỗi</summary>
        error: function (message, autoHide) {
            this.notification(message, this.messageTypes.error, autoHide);
        },

        /// <summary>Hiển thị message thông báo thành công</summary>
        success: function (message, autoHide) {
            this.notification(message, this.messageTypes.success, autoHide);
        },

        /// <summary>Hiển thị message cảnh báo</summary>
        warning: function (message, autoHide) {
            this.notification(message, this.messageTypes.warning, autoHide);
        },

        hide: function () {
            this.$el.fadeOut('slow');
        }
    });

    var _getAlertTypeClass = function (type) {
        switch (type) {
            case messageTypes.success:
                return "alert alert-success";
            case messageTypes.error:
                return "alert alert-danger";
            case messageTypes.warning:
                return "alert alert-warning";
            case messageTypes.processing:
                return "alert alert-info";
            default:
                return "";
        }
    };

    var _getDialogSetting = function () {
        return {
            height: 'auto',
            width: '600px',
            title: 'Thông báo',
            draggable: true,
            buttons: [],
            close: function () {
                //$(this).remove();
            }
        };
    };

    var _getDialogObj = function () {
        var result = $("<div>");
        result.css("min-width", "200px");
        return result;
    };

    var _getDialogButtons = function (button, okCallBackFunction, cancelCallBackFunction, that) {
        if (typeof button !== 'number') {
            return [];
        }
        var result = [];
        var btnText = {
            Yes: egov.resources.common.messageYesBtn ? egov.resources.common.messageYesBtn : "Yes",
            No: egov.resources.common.messageNoBtn ? egov.resources.common.messageNoBtn : "No",
            Cancel: egov.resources.common.messageCancelBtn ? egov.resources.common.messageCancelBtn : "Cancel",
            Ok: egov.resources.common.messageOkBtn ? egov.resources.common.messageOkBtn : "Ok"
        }
        switch (button) {
            case messageButtons.YesNo:
                result.push({
                    text: btnText.Yes,
                    className: 'btn-primary',
                    click: function () {
                        that.dialog('destroy');
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        return messageResult.Yes;
                    }
                });
                result.push({
                    text: btnText.No,
                    click: function () {
                        that.dialog('destroy');
                        if (typeof cancelCallBackFunction === "function") {
                            cancelCallBackFunction();
                        }
                        return messageResult.No;
                    }
                });

                break;
            case messageButtons.OkCancel:
                result.push({
                    text: btnText.Ok,
                    className: 'btn-primary',
                    click: function () {
                        that.dialog('destroy');
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        return messageResult.OK;
                    }
                });
                result.push({
                    text: btnText.Cancel,
                    click: function () {
                        that.dialog('destroy');
                        if (typeof cancelCallBackFunction === "function") {
                            cancelCallBackFunction();
                        }
                        return messageResult.Cancel;
                    }
                });

                break;
            default:
                result.push({
                    text: btnText.Ok,
                    className: 'btn-primary',
                    click: function () {
                        that.dialog('destroy');
                        if (typeof okCallBackFunction === "function") {
                            okCallBackFunction();
                        }
                        return messageResult.OK;
                    }
                });
                break;
        }
        return result;
    };

    window.eGovMessage = eGovMessage;

    return eGovMessage;
})();
(function ($) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }

    var refreshGrid = function ($gridContent, isAddHoverRow) {
        $gridContent.find(".trodd").removeClass("trodd");
        $gridContent.find("tr:odd").addClass("trodd");
        if (isAddHoverRow) {
            var tr = $gridContent.find("tr");
            if (tr.length > 0) {
                tr.hover(function () {
                    $(this).addClass("hover-row");
                }, function () {
                    $(this).removeClass("hover-row");
                });
                tr.click(function () {
                    tr.removeClass("highlight-row");
                    $(this).addClass("highlight-row");
                });
                tr.mousedown(function (e) {
                    if (e.which === 3) {
                        tr.removeClass("highlight-row");
                        $(this).addClass("highlight-row");
                    }
                });
                $('.grid-content table tbody tr input, .grid-content table tbody tr a, .grid-content table tbody tr button').unbind('click').click(function (e) {
                    e.stopPropagation();
                    return true;
                });
            }
        }
    };

    var _hasSupportLocalStorage = function () {
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
    };

    jQuery.fn['grid'] = function (settings) {
        if (typeof settings === 'string') {
            if (settings.toLowerCase() === 'refresh') {
                this.each(function (i, el) {
                    refreshGrid($(el).parent(), $(el).data()['bkav.grid'].isAddHoverRow);
                });
                return;
            }
        }

        settings = $.extend({
            isUsingCustomScroll: false,
            isAutoHideScroll: true,
            isResizeColumn: true,
            isFixHeightContent: true,
            isAddHoverRow: true,
            isUseCookie: true,
            isRenderPanelGrid: true,
            onresizefinish: function () { }
        }, settings);

        this.each(function (i, el) {
            var isResizing = null;
            var element = null;
            var index = null;
            var clientX = null;
            var width = null;
            var widthTable = null;
            var $elQ = $(el);
            if ($elQ.length === 0) {
                return false;
            }
            if ($elQ[0].tagName !== 'TABLE') {
                return false;
            }

            $elQ.data('bkav.grid', settings);
            var $grid;
            if (settings.isRenderPanelGrid) {
                $grid = $('<div></div>');
                $elQ.after($grid);
            } else {
                $grid = $elQ.parent();
            }
            $grid.attr('data-key', $elQ.attr('id'));
            if (!$grid.hasClass('grid')) {
                $grid.addClass('grid');
            }

            if (settings.width) {
                if (typeof settings.width === 'number') {
                    $grid.width(settings.width + 'px');
                } else if (typeof settings.width === 'string') {
                    $grid.width(settings.width);
                }
            }
            if (!$elQ.hasClass('table-main')) {
                $elQ.addClass('table-main');
            }
            //Build header
            var $gridHeader = $('<div class="grid-header"></div>');
            var $gridHeaderWrap = $('<div class="grid-header-wrap"></div>');
            var $tableHeader = $('<table class="table table-hover table-main"></table>');
            var countColumn = $elQ.find('thead tr:first th').length;
            var $colgroupHeader = $('<colgroup></colgroup>');

            //Tạo header cho table header
            $tableHeader.append($colgroupHeader);
            if ($elQ.find('col').length === 0) {
                var $colgroupContent = $('<colgroup></colgroup>');
                $elQ.children('tbody').before($colgroupContent);
                for (var j = 0; j < countColumn; j++) {
                    $colgroupHeader.append('<col />');
                    $colgroupContent.append('<col />');
                }
            } else {
                $colgroupHeader.append($elQ.find('colgroup').html());
            }

            $elQ.find('thead tr:first th').each(function (idx, item) {
                if (!$(item).hasClass('header')) {
                    $(item).addClass('header');
                }
            });

            $tableHeader.append($elQ.find('thead'));
            $gridHeaderWrap.append($tableHeader);
            $gridHeader.append($gridHeaderWrap);
            $grid.append($gridHeader);
            // $gridHeaderWrap.width($tableHeader.width());

            //Build pager
            if ($elQ.find('tfoot tr td').length > 0) {
                var $gridPager = $('<div class="grid-pager grid-pager-wrap grid-pager-bottom"></div>');
                $gridPager.append($elQ.find('tfoot tr td:first').html());
                $grid.append($gridPager);
                $elQ.find('tfoot').remove();
            }

            //Build content
            var $gridContent = $('<div class="grid-content"></div>');
            $gridContent.append($elQ);
            $gridHeader.after($gridContent);
            refreshGrid($gridContent, settings.isAddHoverRow);

            if (settings.isFixHeightContent) {
                //$gridContent.css({ 'overflow-y': 'auto', 'min-height': '300px' });
                $gridContent.css({ 'overflow-y': 'auto' });//bỏ min-height do bản desktop khi rezisie không nhận scroll ngang
                if (settings.height) {
                    if (typeof settings.height === 'number') {
                        $gridContent.height(settings.height + 'px');
                    } else if (typeof settings.height === 'string') {
                        $gridContent.height(settings.height);
                    }
                }
            } else {
                $gridContent.css({ 'overflow-y': 'visible', 'height': '' });
                if (settings.height) {
                    if (typeof settings.height === 'number') {
                        $grid.height(settings.height + 'px');
                    } else if (typeof settings.height === 'string') {
                        $grid.height(settings.height);
                    }
                }
            }

            // resize chiều rộng các cột
            if (settings.isResizeColumn) {

                // $gridContent.css({ 'overflow-x': 'auto' });
                var uniqueKey = i + "_" + $elQ.attr('id') + "_" + window.location.pathname;
                var columns = $tableHeader.find("thead tr:first th");

                //Bắt các sự kiện để khi scroll lesft thì các cột trên table header cũng thay đổi vị trí theo
                $gridContent.bind('mousedown, mouseup, mousemove', function () {
                    var value = $(this).scrollLeft();
                    $gridHeaderWrap.css({ "margin-left": -value })
                });

                //Lấy thiết lập các cột
                var widthColumn = null;
                if (settings.isUseCookie) {
                    if (_hasSupportLocalStorage()) {
                        widthColumn = window.localStorage.getItem(uniqueKey);
                    }
                    else {
                        widthColumn = $.cookie(uniqueKey);
                    }
                }

                //Bind lại các thiết lập trước đấy cho table
                var $tableHeaderCol = $gridHeaderWrap.find('col');
                var $tableContent = $gridContent.children('table');
                var $tableContentCol = $gridContent.find('col');

                $tableHeader.css({ minWidth: '1024px' });
                $tableContent.css({ minWidth: '100%' });
                // $gridContent.width($tableContent.width());

                if (settings.isFixHeightContent && !settings.isUsingCustomScroll) {
                    if ($gridContent.height() < $tableContent.height()) {
                        $gridHeader.css('padding-right', $.layout.scrollbarWidth() + 'px');
                        $tableContent.css('padding-right', $.layout.scrollbarWidth() + 'px');
                    }
                }

                if (widthColumn) {
                    var arrayWidthColumn = widthColumn.split(',');
                    if (arrayWidthColumn.length == $tableHeaderCol.length) {
                        widthTable = 0;
                        for (var l = 0; l < arrayWidthColumn.length; l++) {
                            if ($tableHeaderCol.eq(l)) {
                                $tableHeaderCol.eq(l).width(arrayWidthColumn[l] + 'px');
                                $tableContentCol.eq(l).width(arrayWidthColumn[l] + 'px');
                                widthTable += parseInt(arrayWidthColumn[l]);
                            }
                        }

                        $tableHeader.width(widthTable + 'px');
                        $tableContent.width(widthTable + 'px');
                    }
                } else {
                    var columnNoWidth = [];
                    var totalWidth = 0;
                    $tableHeaderCol.each(function (idx) {
                        if ($(this).width() == 0 || $(this).inlineStyle('width') == null) {
                            columnNoWidth.push(idx);
                        } else {
                            totalWidth += $(this).width();
                        }
                    });

                    if (columnNoWidth.length > 0) {
                        widthTable = $grid.width();
                        if (settings.isFixHeightContent && !settings.isUsingCustomScroll) {
                            widthTable = widthTable - $.layout.scrollbarWidth();
                        }
                        var allWidth = widthTable - totalWidth;
                        var widthCol = allWidth / columnNoWidth.length;
                        for (var g = 0; g < columnNoWidth.length; g++) {
                            if (g == columnNoWidth.length - 1) {
                                widthCol = widthCol - $.layout.scrollbarWidth();
                            }

                            $tableHeaderCol.eq(columnNoWidth[g]).width(widthCol);
                            $tableContentCol.eq(columnNoWidth[g]).width(widthCol);
                        }
                    } else {
                        widthTable = 0;
                        $tableHeaderCol.each(function (idx, item) {
                            widthTable += $(item).width();
                        });
                    }
                }

                if (settings.isUsingCustomScroll) {
                    $gridContent.niceScroll({ autohidemode: settings.isAutoHideScroll });
                }

                columns.each(function (k, column) {
                    $(column).unbind('mousedown')
                        .mousedown(function (e) {
                            if ($(e.target).css('cursor') == 'w-resize') {//'col-resize'
                                e.preventDefault();
                                $('body').addClass('unselectable');
                                isResizing = true;
                                element = e.target;
                                index = $(element).index();
                                clientX = e.clientX;
                                width = $(element).outerWidth();
                                widthTable = $tableContent.width();

                                var layer = $('<div id="resize-column-layer" style="position:fixed; z-index:10000; top:0; left:0; opacity: 0; filter: alpha(opacity=0); background-color: #000;cursor: w-resize !important;border-right:1px solid gray"></div>')
                                    .css({ height: $(document).height(), width: $(document).width(), display: 'block' })
                                    .mousemove(function (ev) {
                                        if (isResizing) {
                                            var difference = clientX - ev.clientX;
                                            if ((width - difference) > 10) {
                                                var newWidth = width - difference;

                                                $tableHeaderCol.eq(index).width(newWidth);
                                                $tableContentCol.eq(index).width(newWidth);

                                                $tableHeader.width(widthTable - difference);
                                                $gridHeaderWrap.width(widthTable - difference);

                                                $tableContent.width(widthTable - difference);
                                                $gridContent.width(widthTable - difference);
                                            }
                                        }

                                        $gridHeaderWrap.css({ "margin-left": -$gridContent.scrollLeft() })

                                    })
                                    .mouseup(function (ev) {
                                        if (isResizing) {
                                            isResizing = false;
                                            element = null;
                                            $('body').css({ cursor: '' });
                                            $('body').removeClass('unselectable');

                                            if (settings.isUseCookie) {
                                                var str = [];
                                                var columnNoWidth1 = [];
                                                var totalWidth1 = 0;
                                                $tableHeaderCol.each(function (idx) {
                                                    if ($(this).width() == 0 || $(this).inlineStyle('width') == null) {
                                                        columnNoWidth1.push(idx);
                                                    } else {
                                                        totalWidth1 += $(this).width();
                                                    }
                                                    str.push($(this).width());
                                                });

                                                if (columnNoWidth1.length > 0) {
                                                    var allWidth1 = $tableContent.width() - totalWidth1;
                                                    var widthCol1 = allWidth1 / columnNoWidth1.length;
                                                    for (var h = 0; h < columnNoWidth1.length; h++) {
                                                        str[columnNoWidth1[h]] = widthCol1;
                                                    }
                                                }

                                                if (_hasSupportLocalStorage()) {
                                                    window.localStorage.setItem(uniqueKey, str.join(','));
                                                }
                                                else {
                                                    $.cookie(uniqueKey, str.join(','), { expires: 7, path: "/", secure: true });
                                                }
                                            }

                                            if (settings.onresizefinish && $.isFunction(settings.onresizefinish)) {
                                                settings.onresizefinish();
                                            }

                                            if (settings.isUsingCustomScroll) {
                                                $gridContent.getNiceScroll().resize();
                                            }

                                            $gridHeaderWrap.css({ "margin-left": -$gridContent.scrollLeft() });

                                            $(ev.target).css({ cursor: 'w-resize', "border-right": "" });
                                            $(this).remove();
                                        }
                                    }).appendTo('body');

                                if (!$.support.fixedPosition) {
                                    layer.css({
                                        'position': 'absolute',
                                        'height': $(document).height()
                                    });
                                }
                            }
                        });
                    $(column).unbind('mousemove')
                        .mousemove(function (e) {
                            if ($(e.target).attr('class') != 'grid-header-wrap' && $(e.target).attr('class') == 'header') {
                                var bounds = $(e.target).bounds();
                                $gridHeaderWrap.css({ "margin-left": -$gridContent.scrollLeft() });
                                if (Math.abs((bounds.left + bounds.width + 10) - (e.clientX)) <= 10) {
                                    $(e.target).css({ cursor: 'w-resize' });
                                }
                                else {
                                    $(e.target).css({ cursor: '' });
                                }
                            }
                        });
                });

            } else {
                $gridContent.css({ 'overflow-x': 'visible' });
                $gridHeaderWrap.css('width', $grid.width() - 1).children('table').css('width', $grid.width() - 1);
                $gridContent.css('width', $grid.width() - 2).children('table').css('width', $grid.width() - 2);
            }
        });
    };
})(window.jQuery);

(function ($) {
    jQuery.fn['bounds'] = function () {
        var bounds = {
            left: Number.POSITIVE_INFINITY,
            top: Number.POSITIVE_INFINITY,
            right: Number.NEGATIVE_INFINITY,
            bottom: Number.NEGATIVE_INFINITY,
            width: Number.NaN,
            height: Number.NaN
        };

        this.each(function (i, el) {
            var elQ = $(el);
            var off = elQ.offset();
            off.right = off.left + $(elQ).width();
            off.bottom = off.top + $(elQ).height();

            if (off.left < bounds.left)
                bounds.left = off.left;

            if (off.top < bounds.top)
                bounds.top = off.top;

            if (off.right > bounds.right)
                bounds.right = off.right;

            if (off.bottom > bounds.bottom)
                bounds.bottom = off.bottom;
        });

        bounds.width = bounds.right - bounds.left;
        bounds.height = bounds.bottom - bounds.top;
        return bounds;
    };
})(window.jQuery);

(function ($) {
    $.fn['inlineStyle'] = function (prop) {
        var styles = $(this).attr("style");
        var value;
        if (styles == null || styles == undefined)
            return null;

        var stylies = styles.split(";");
        for (var i = 0; i < stylies.length; i++) {
            var style = stylies[i].split(":");
            if ($.trim(style[0]) === prop) {
                value = style[1];
            }
        }

        //styles && styles.split(";").forEach(function (e) {
        //	var style = e.split(":");
        //	if ($.trim(style[0]) === prop) {
        //		value = style[1];
        //	}
        //});
        return value;
    };
})(window.jQuery);

(function ($) {
    $.fn.tableSelect = function (options) {
        var defaultOptions = {
            clickedClass: "selected",
            clickCallback: null,
            contextCallback: null,
            tableCallback: null
        };

        defaultOptions = $.extend(defaultOptions, options);

        var table = $(this),
            lastSelected,
            originalRow,
            currentRow;

        if (table[0].tagName !== 'TABLE') {
            return false;
        }
        var tableRows = $(table).find('tr');

        //Xóa toàn bộ các hàng được chọn
        var tableRemoveAllSelected = function () {
            $.each(tableRows, function () {
                $(this).removeClass(defaultOptions.clickedClass);
            });
        };

        $.each(tableRows, function () {
            var _this = $(this);
            _this.children('td').attr('class', '');
            _this.children('td').attr('unselectable', 'on');

            //Bắt sự kiên click chuột trái
            _this.on('click', function (ev) {
                var that = $(this);
                //phím shift
                if (ev.shiftKey) {
                    var last = tableRows.index(lastSelected);
                    var first = tableRows.index(this);
                    var start = Math.min(first, last);
                    var end = Math.max(first, last) + 1;
                    tableRemoveAllSelected();

                    for (var i = start; i < end; i++) {
                        if (!$(tableRows[i]).hasClass(defaultOptions.clickedClass)) {
                            $(tableRows[i]).addClass(defaultOptions.clickedClass);
                        }
                    }

                    originalRow = lastSelected;
                    //  currentRow = this;
                }
                    //phím ctrl
                else if (ev.ctrlKey) {
                    if (this.className.search(defaultOptions.clickedClass) > -1) {
                        that.removeClass(defaultOptions.clickedClass);
                    } else {
                        if (!that.hasClass(defaultOptions.clickedClass)) {
                            that.addClass(defaultOptions.clickedClass);
                        }
                    }

                    lastSelected = this;
                    //  currentRow = this;
                    originalRow = this;
                }
                else {
                    tableRemoveAllSelected();
                    if (!that.hasClass(defaultOptions.clickedClass)) {
                        that.addClass(defaultOptions.clickedClass);
                        lastSelected = this;
                        originalRow = this;
                        // currentRow = this;
                    }
                }

                if (typeof defaultOptions.clickCallback === 'function') {
                    defaultOptions.clickCallback();
                }
            });

            //Bắt sự kiên click chuột phải
            if (!_this.hasClass(defaultOptions.clickedClass)) {
                //_this.on('contextmenu', function (ev) {
                //    if (lastSelected === currentRow) {
                //        tableRemoveAllSelected();
                //    }
                //    _this.addClass(defaultOptions.clickedClass);
                //    lastSelected = this;
                //    originalRow = this;
                //    currentRow = this;
                //    if (typeof defaultOptions.contextCallback === 'function') {
                //        defaultOptions.contextCallback();
                //    }
                //});
            }

            if (defaultOptions.tableCallback && typeof defaultOptions.tableCallback === 'function') {
                defaultOptions.tableCallback();
            }
        });
    };

    $.fn.openLink = function (options) {
        var defaultOptions = {
            event: 'dblclick',
            urlOpen: ''
        }
        , table = $(this);

        defaultOptions = $.extend(defaultOptions, options);

        if (table[0].tagName !== 'TABLE'
            || ((defaultOptions.urlOpen == null || defaultOptions.urlOpen == '')
            || (defaultOptions.event == null || defaultOptions.event == ''))) {
            return false;
        }

        var tableRows = $(table).find('tr');

        $.each(tableRows, function () {
            var _this = $(this);
            _this.on(defaultOptions.event, function (ev) {
                var that = $(this);
                var id = that.attr("id") || that.attr("data-id");
                urlOpen = getUrl(defaultOptions.urlOpen);

                if (id && urlOpen != null && urlOpen != '') {
                    document.location.href = urlOpen + '/' + id;
                }
            });
        });

        if (!String.prototype.endsWith) {
            String.prototype.endsWith = function (searchString, position) {
                var subjectString = this.toString();
                if (typeof position !== 'number' || !isFinite(position) || Math.floor(position) !== position || position > subjectString.length) {
                    position = subjectString.length;
                }
                position -= searchString.length;
                var lastIndex = subjectString.indexOf(searchString, position);
                return lastIndex !== -1 && lastIndex === position;
            };
        }

        var getUrl = function (inUrl) {
            if (inUrl == '' || inUrl == null)
                return null;

            if (inUrl.endsWith("/")) {
                inUrl = inUrl.substring(0, inUrl.lastIndexOf("/"));
            }
            return inUrl;
        }
    };
})(jQuery);
(function () {

    egov = egov || {};

    egov.popup = {
        open: function (url, title, inputWidth, inputHeight) {
            /// <summary>
            /// Mở cửa sổ popup
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="title">title</param>
            /// <param name="inputWidth">chiều rộng popup, nếu không truyền sẽ lấy theo cấu hình của user</param>
            /// <param name="inputHeight">chiều cao popup, nếu không truyền sẽ lấy theo cấu hình của user</param>
            var width,
                height;

            width = inputWidth ? inputWidth : egov.setting && egov.setting.userSetting && egov.setting.userSetting.PopUpWidth ? egov.setting.userSetting.PopUpWidth : 900;
            height = inputHeight ? inputWidth : egov.setting && egov.setting.userSetting && egov.setting.userSetting.PopUpHeight ? egov.setting.userSetting.PopUpHeight : 575;
            window.open(url, title, "width=" + width + ",height=" + height);
        },

        autoSaveSize: function () {
            /// <summary>
            /// Tự động lưu kích cỡ popup khi người dùng resize
            /// </summary>
            var delay = (function () {
                var timer = 0;
                return function (callback, ms) {
                    clearTimeout(timer);
                    timer = setTimeout(callback, ms);
                };
            })();
            $(window).resize(function () {
                var that = this;
                delay(function () {
                    egov.request.setPopUpSize({
                        data:{
                            width: that.innerWidth,
                            height:that.innerHeight
                        }
                    });
                }, 500);
            });
        }
    }
})();

(function () {
        "use strict";

        /// <summary>Lớp xử lý table</summary>
        var Table = Backbone.View.extend({
            selectedClass: 'rowSelected',
            events: {
                'click tr': 'selected',
                'click .checkAll': 'selectMany',
                "mousedown th": "startResize",
                "mousemove": "onResize",
                "mouseup": "endResize",
            },

            pressed: false,
            startWidth: undefined,

            /// <summary>Khởi tạo</summary>
            initialize: function (option) {
                this.$el = option.el;
                this.resizable = option.resizable;
                this.scrollable = option.scrollable;
                this.render();
            },

            /// <summary>Xử lý các sự kiện</summary>
            render: function () {
                if (this.resizable) {
                    this.renderResizable();
                }
                if (this.scrollable) {
                    this.renderScollable();
                }
            },

            startResize: function () {
                this.pressed = true;
            },

            onResize: function () {
                if (this.pressed) {
                    this.$el.find("colgroup").remove();
                }
            },

            endResize: function () {
                if (this.pressed) {
                    this.pressed = false;
                }
            },

            /// <summary>Selected</summary>
            selected: function (e) {
                var target = $(e.target);
                var row = target.is("tr") ? target : target.parents('tr');
                if (e) {
                    e.preventDefault();
                }
                e = e || {};

                var isMultiSelect = e.ctrlKey || $(e.target).closest('.checkbox, [type="checkbox"]').length > 0;

                // Nếu nhấn nút Shift
                if (e.shiftKey) {
                    // Lấy row có index nhỏ nhất trong số các row đã selected
                    var fromIdx = this.$el.find('.' + this.selectedClass).first().index();

                    // Lấy index của row hiện tại
                    var toIdx = row.index();
                    var allRow = this.$el.find('tr');

                    // Xóa hết các selected
                    this.removeAllSelected();

                    // Gán lại selected cho các row trong vùng được chọn
                    for (var i = fromIdx; i <= toIdx; i++) {
                        var rowItm = allRow.eq(i);
                        rowItm.addClass(this.selectedClass);
                        this.select(rowItm);
                    }
                }
                else if (isMultiSelect) { // Nếu nhấn Ctrl hoặc click vào checkbox
                    if (row.hasClass(this.selectedClass)) {
                        row.find('.checkbox, [type="checkbox"]').removeClass('checked').prop('checked', false);
                        row.removeClass(this.selectedClass);
                    }
                    else {
                        this.select(row);
                    }
                }
                else {
                    this.removeAllSelected();
                    this.select(row);
                }

                if (isMultiSelect) {
                    e.stopPropagation();
                }
            },

            /// <summary>Select row hiện tại</summary>
            select: function (row) {
                row.addClass(this.selectedClass);
                row.find('.checkbox, [type="checkbox"]').addClass('checked').prop('checked', true);
            },

            /// <summary>Select tất cả các row trong table</summary>
            selectAll: function () {
                this.$el.find('tr').addClass(this.selectedClass);
                this.$('tr .checkbox, tr [type="checkbox"]').addClass('checked').prop('checked', true);
            },

            /// <summary>Select, unselect tất cả</summary>
            selectMany: function (e) {
                var checkAll = $(e.target).closest('.checkAll');
                if (checkAll.hasClass("checked") || checkAll.is(":checked")) {
                    this.removeAllSelected();
                    checkAll.removeClass('checked').removeAttr('checked');
                }
                else {
                    this.selectAll();
                    checkAll.addClass('checked').prop('checked', true);
                }
                if (e) {
                    e.stopPropagation();
                    e.preventDefault();
                }
            },

            /// <summary>Remove tất cả các selected</summary>
            removeAllSelected: function () {
                this.$('tr').removeClass(this.selectedClass);
                this.$('tr .checkbox, tr [type="checkbox"]').removeClass('checked').prop('checked', false);
            },

            /// <summary>Cho phép kéo thả các cột trong table</summary>
            renderResizable: function () {
                this.$('th').resizable();
            },

            /// <summary>Cho phép scoll phần body của table</summary>
            renderScollable: function () {

            }
        });

        egov.utils.table = Table;

    })();


/// <summary>Thư viện tự động xử lý tooltip, các alert thông báo, menu xổ xuống</summary>
/// Author: TienBV@bkav.com
/// DateCreated: 24/12/2013
/// Requires:
///     - jquery 1.6 + download tại http://jquery.com/download/
///     - qtip2 download tại http://qtip2.com/

(function () {

        "use strict";

        //#region Variables

        var _tooltipTarget = ".qtooltip",
            _notifierTarget = ".edropdown",
            _shortKeyHelper = ".shortkey";

        //#endregion

        //#region Functions

        var eGovTip = function () {
        };

        eGovTip.prototype.initShortKeyHelper = function () {
            _initShorkeyHelper();
        }

        eGovTip.prototype.bind = function (objs) {
            if (!(objs instanceof jQuery)) {
                objs = $(objs);
            }
            objs.each(function (idx, obj) {
                obj = $(obj);
                if (obj.is(_tooltipTarget)) {
                    _initTooltip(obj);
                }
                else if (obj.is(_notifierTarget)) {
                    _initNotifier(obj);
                }
                else {
                    var option = _getDefaultSetting();
                    option.position = _getTooltipPosition($(obj));
                    obj.qtip(option);
                }
            });
        }

        eGovTip.prototype.destroy = function (obj) {
            obj.qtip('destroy');
        }

        //#endregion

        //#region Private Functions

        /// <summary>Hiển thị tooltips.</summary>
        var _initTooltip = function (obj) {
            var $this = $(obj);
            var option = _getDefaultSetting();
            option.events = {
                show: function (event, api) {
                    var $element = $this.data('qtip').elements.target;
                    var position = _getTooltipPosition($this);
                    $element.qtip('option', { 'position.at': position.at, 'position.my': position.my });
                }
            };
            $this.qtip(option);
        };

        /// <summary>Trả về tooltip option tương ứng với vị trí của target</summary>
        /// <param name="$this" type="object">target</param>
        var _getTooltipPosition = function ($this) {
            var lof = $this.offset().left;
            var tof = $this.offset().top;
            var h = $this.height();
            var w = $this.width();
            var tipWidth = $this.attr("etip-width");

            var myT = "top", myL = "center", atT = "bottom", atL = "center"; // mặc định hiển phía dưới căn giữa.

            if (lof < 20) // bên trái
            {
                myL = "left";
                atL = "left";
            }
            if ((lof + w) > window.innerWidth - tipWidth / 2 || (lof + w) > window.innerWidth - 50) { //bên phải
                myL = "right";
                atL = "right";
            }
            if (tof < 20) { // góc trên
                atT = "bottom";
                myT = "top";
            }
            if ((tof + h) > window.innerHeight - 20) { // góc dưới
                myT = "bottom";
                atT = "top";
            }
            var result = {
                my: myT + myL,
                at: atT + atL
            };
            return result;
        };

        var _initNotifier = function (obj) {
            var $this = $(obj);
            var contentId = $this.attr("content-id");
            var contentUrl = $this.attr("data-url");
            var isSmallTip = $this.attr("show-tip") == "true";
            var position = _getTooltipPosition($this);
            position.adjust = {
                y: (isSmallTip ? 5 : 13)
            }
            var tipWidth = $this.attr("etip-width");
            var tipHeight = $this.attr("etip-height");
            $this.qtip({
                content:
                {
                    text: function (event, api) {
                        if (contentId != undefined && contentId !== '') {
                            if (contentId.indexOf("#") < 0) {
                                contentId = "#" + contentId;
                            }
                            return $(contentId);
                        }
                        else {
                            $.ajax({
                                url: contentUrl,
                                type: 'POST'
                            })
                            .then(function (content) {
                                api.set('content.text', content);
                            }, function (xhr, status, error) {
                                api.set('content.text', status + ":" + error);
                            });
                        }
                    }
                },
                //position: position,
                events: {
                    show: function (event, api) {
                        var $element = $this.data('qtip').elements.target;
                        var position = _getTooltipPosition($this);
                        position.adjust = {
                            y: (isSmallTip ? 5 : 13)
                        }
                        $element.qtip('option', { 'position.at': position.at, 'position.my': position.my, 'position.adjust.y': position.adjust.y });
                    }
                },
                show: { event: 'click', fixed: true, effect: false, delay: 0 },
                hide: { event: 'click unfocus', fixed: true, effect: false, delay: 0 },
                style: { classes: 'qtip-light', tip: isSmallTip, width: tipWidth, height: tipHeight }
            });
        };

        var _initShorkeyHelper = function () {
            var option = _getDefaultSetting();
            option.style = {
                tip: false,
                classes: 'shortkey-helper'
            };
            option.position = {
                my: "bottom left",
                at: "center right"
            };
            option.show = false;
            $(_shortKeyHelper).qtip(option);
        };

        var _getDefaultSetting = function () {
            return {
                style: {
                    tip: false,
                    classes: 'qtip-dark tooltip'    // qtip-light, qtip-cream, qtip-red, qtip-green, qtip-blue, qtip-rounded, qtip-bootstrap, qtip-tipped,  ...
                },
                position: {                 // Hiển thị ngay phía dưới và căn giữa target
                    my: "top center",
                    at: "bottom center",
                    adjust: {
                        y: 10
                    }
                }
            };
        };

        //#endregion

        return eGovTip;

    })();
function Base() {

};

Base.prototype.isReady = function () {

};

Base.prototype.isPluginExist = function () {

};

Base.prototype.openURL = function () {

};
Base.prototype.openFile = function () {

};

Base.prototype.signWord = function () {

};

Base.prototype.signPdf = function () {

};

Base.prototype.getCertIndex = function () {

};

Base.prototype.writeFileBase64 = function () {

};
Base.prototype.readFileBase64 = function () {

};
Base.prototype.getTempFolder = function () {

};
Base.prototype.transferPdf = function () {

};
Base.prototype.transferDoc = function () {

};
Base.prototype.closeAllOfficeDocuments = function () {

};
Base.prototype.cancelTransferImage = function () {

};
Base.prototype.isWordInstalled = function () {

};
Base.prototype.isChangeContent = function () {

};
Base.prototype.getAllScanner = function () {

};
Base.prototype.imageCrop = function () {

};
Base.prototype.readFileScanBase64 = function () {

};
Base.prototype.imageRotate = function () {

};
Base.prototype.imageAdjustBrightness = function () {

};
Base.prototype.imageAdjustContrast = function () {

};
Base.prototype.transferImage = function () {

};



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
 * Sử dụng:
 *  var plugin = PluginFactory.getInstance();
 *  var isInstalled = plugin.isPluginExist();
 */
function PluginFactory() {

}

PluginFactory.getInstance = function () {
    if (navigator.isMobile) {
        return BChromeCAPlugin.getInstance();
    }
    else if (PluginFactory.isChrome())
        return ChromeNativeApp.getInstance();
    else
        return null; // FirefoxPlugin.getInstance();
};

PluginFactory.isChrome = function () {
    if (navigator.userAgent.indexOf("Chrome") != -1)
        return true;
    return false;
};
(function () {

    var EgovPlugin = {

        extension: null,

        signatureConfig: [],

        appendPlugin: function (callback) {
            /// <summary>
            /// Chèn plugin
            /// </summary>
            /// <param name="callback"></param>
            //if (egov.isMobile) {
            //    return;
            //}

            if (!egov.extension) {
                egov.extension = PluginFactory.getInstance();
            }

            if (!egov.extension) {
                return;
            }

            // Kiem tra plugin/extension đã sẵn sàng sử dụng?
            egov.extension.isReady(function (isReady) {
                if (isReady) {
                    this.checkNativeAppVersion(callback);
                } else {
                    // Kiểm tra plugin/extension đã được cài đặt?
                    egov.extension.isPluginExist(function (isInstalled) {
                        if (isInstalled) {
                            egov.extension.injectPlugin("egovPlugin", function () {
                                this.appendPlugin(callback);
                            }.bind(this));
                        } else {
                            this.showDialogDownloadPlugin(function () {
                                this.appendPlugin(callback);
                            }.bind(this));
                        }
                    }.bind(this));
                }
            }.bind(this));
        },

        checkNativeAppVersion: function (callback) {
            if (egov.extension.hasAppendMode != null || egov.hasPluginAppendMode != null) {
                // 
            } else {
                setTimeout(function () {
                    egov.extension.hasAppendWriteMode(function (result) {
                        egov.extension.hasAppendMode = result;
                        egov.hasPluginAppendMode = result;
                        egov.callback(callback);
                    });
                }, 2000);
            }

            egov.callback(callback);
            return;
        },

        //#region Tải bộ cài plugin

        showDialogDownloadPlugin: function (callback) {
            /// <summary>
            /// Hiển thị dialog yêu cầu tải về plugin để mở file.
            /// </summary>
            /// <param name="callback">Hàm thực thi sau khi cài đặt thành công.</param>
            var that,
                _div;

            that = this;

            _div = $('<div><p style="font-size:16px;font-weight:bold;">'
                + egov.resources.plugin.noplugin + '</p><p>'
                + egov.resources.plugin.pluginrequire + '</p><p style="color:red">'
                + egov.resources.plugin.needrestartbrowser + '</p><center><div style="text-align:center;width:180px"><input type="button" value="'
                + egov.resources.plugin.downloadtosetup + '" />'
                //<div id="imgDownloadingPlugin" style="float:left;display:none"><img src="/Content/Images/ajax-loader.gif" width="24px" height="24px" /></div><div id="msgDowloadingPlugin" style="float:left;padding-top:5px;display:none">&nbsp;'
                //+ egov.resources.plugin.waitforsetup + '</div>
                + '</div></center></div>');

            _div.find('input').bind('click', function () {
                that._startDownload();
                that._dowloadingPlugin(_div, callback);
            });

            _div.dialog({
                width: '800px',
                resizable: false,
                title: egov.resources.common.alert,
                buttons: [
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            _div.dialog('destroy');
                        }
                    }
                ]
            });
        },

        _startDownload: function () {
            /// <summary>
            /// DownloadPlugin
            /// </summary>
            document.location = "/Download/EOfficePlusPlugin";
        },

        _dowloadingPlugin: function (div, callback) {
            /// <summary>
            /// Các event xảy ra khi đang download
            /// </summary>
            /// <param name="div"></param>
            /// <param name="callback"></param>
            var that,
                retryBtn;

            that = this;
            retryBtn = $('<a class="retry">' + egov.resources.main.installPlugin.reDownload + '</a>');
            retryBtn.on("click", function (e) {
                that._startDownload();
            });
            div.empty();
            div.append(retryBtn);
            //TODO: Chờ plugin thực tế ntn rồi thêm hình ảnh hướng dẫn cài đặt theo kịch bản
            //Phần này phải xem plugin mới cài đặt xong nó ntn thì mới xử lý theo kịch bản được
            //FireBreath.waitForInstall(that.pluginName, function () {
            //    dialog.close();
            //    if (callback && typeof (callback) === 'function') {
            //        callback();
            //    }
            //});
        },

        //#endregion

        //#region Ký

        callSignSuccess: function (result) {
            if (this.tempFolderLoc) {
                this.deleteFolder(this.tempFolderLoc);
            }
            this.currentSignOptions = { withChoosePoint: false };
            egov.callback(this.signSuccess, result);
        },

        sign: function (document, signSuccess) {
            /// <summary>
            /// Ký các file trong văn bản
            /// </summary>
            /// <param name="document">văn bản</param>
            /// <param name="callback">Hàm callback</param>
            var that,
                confirmSignFiles,
                docAttachments;

            that = this;
            that.currentSignOptions = { withChoosePoint: false };

            // Thư mục lưu file tạm để chuẩn bị ký.
            that.tempFolderLoc = (new Date()).getTime().toString() + "_forsign";

            that.hasDeleteOldFile = false;
            that.signatureConfig = egov.signatureConfig || [];
            that.signSuccess = signSuccess;

            var signerConfig = egov.setting.signerConfig;
            if (signerConfig.length === 0) {
                egov.pubsub.publish(egov.events.status.error, "Bạn chưa cấu hình chữ ký số, vui lòng vào Thiết lập cá nhân/Cấu hình chữ ký để tạo mới.");
                that.callSignSuccess(false);
                return;
            }

            // Ghi toàn bộ nội dung file ảnh chèn vào chữ kí ra thư mục tạm
            var writeSignatureConfig = $.isArray(signerConfig)
                                            ? signerConfig.slice(0, signerConfig.length)
                                            : $.extend(true, {}, signerConfig);

            that.document = document;
            that._writeImageSignature(writeSignatureConfig, function (signatureConfig) {
                // Bắt đầu quá trình kí

                confirmSignFiles = document.getAttachmentsForSign();
                if (confirmSignFiles.length > 0) {
                    that.confirmSignFiles = confirmSignFiles;
                    that._displaySignDialog(confirmSignFiles, signSuccess);
                } else {
                    that.callSignSuccess(false);
                }
            });
        },

        _displaySignDialog: function (confirmSignFiles, signSuccess) {
            /// <summary>
            /// Hiển thị dialog ký
            /// </summary>
            /// <param name="confirmSignFiles">file cần ký</param>
            /// <param name="callback"></param>
            var that,
                $confirmDialog,
                settings;

            that = this;
            that.isPreviewing = false;

            require([egov.template.document.signer], function (signTemplate) {
                $confirmDialog = $('<div></div>');
                $confirmDialog.append(signTemplate);

                $confirmDialog.find(".sign-list").append($.tmpl('<li class="list-group-item"><div><label><input type="checkbox" data-id="${Id}"> ${Title}</label></div></li>', that.signatureConfig));
                if (that.signatureConfig.length === 1) {
                    $confirmDialog.find(".sign-list :checkbox").attr("checked", "checked");
                }

                var confirmFiles = _.map(confirmSignFiles, function (f) {
                    return f.toJSON();
                });

                var groupByDocument = _.groupBy(confirmFiles, "Compendium");
                var $attachments = $confirmDialog.find(".att-list");
                _.each(groupByDocument, function (items, name) {
                    $attachments.append('<li class="list-group-item"><b>' + name + '</b></li>');
                    $attachments.append($.tmpl('<li class="list-group-item"><div><label><input type="checkbox" data-id="${Id}" checked> ${Name}</label></div></li>', items));
                });

                $confirmDialog.appendTo('body');
                that.$confirmDialog = $confirmDialog;

                settings = {};
                settings.width = 500;
                settings.height = "auto";
                settings.autoResize = true;
                settings.title = "Ký số điện tử";
                settings.buttons = [
                {
                    text: "Xem trước",
                    className: 'btn-warning',
                    click: function () {
                        that._signPreviewBtn();
                    }
                },
                {
                    text: "Chuyển",
                    className: 'btn-primary',
                    click: function () {
                        that._signAndSendBtn();
                    }
                },
                {
                    text: "Bỏ qua",
                    className: 'btn-default',
                    click: function () {
                        that.isPreviewing = false;
                        that.$confirmDialog.dialog("destroy");
                        that.callSignSuccess(false);
                    }
                }];

                settings.confirm = {
                    text: "Xóa tệp cũ sau khi ký",
                    id: "deleleOldFile",
                    style: { float: "left", "font-weight": "normal", "font-size": "13px" },
                    click: function (isChecked) {
                        that.hasDeleteOldFile = isChecked;
                    },
                    hasAutoClick: false,
                    disabled: false
                };

                that.$confirmDialog.dialog(settings);
            });
        },

        _getSelectedFiles: function (confirmSignFiles) {
            var that = this;
            var $selected = that.$confirmDialog.find(".att-list input:checked");
            if ($selected.length <= 0) {
                return [];
            }
            var selectedIds = [];
            $selected.each(function (i, item) {
                selectedIds.push($(item).attr('data-id'));
            });

            var selectedFiles = _.filter(confirmSignFiles, function (item) {
                return _.contains(selectedIds, item.get("Id").toString());
            });
            that.currentSignOptions.selectedFiles = selectedFiles;
            return selectedFiles;
        },

        _getSelectedSignerConfig: function (signerConfig) {
            var that = this;
            var hasShowAlertChooseToken = false;
            var $selected = that.$confirmDialog.find(".sign-list input:checked");
            if ($selected.length <= 0) {
                return [];
            }

            var selectedIds = [];
            $selected.each(function (i, item) {
                selectedIds.push($(item).attr('data-id'));
            });

            var selectedSigners = _.filter(signerConfig, function (item) {
                return _.contains(selectedIds, item.Id.toString());
            });

            var config = {
                sameToken: _.where(selectedSigners, { 'hasSameToken': true })
            };

            _.each(_.where(selectedSigners, { 'hasSameToken': false }), function (c) {
                hasShowAlertChooseToken = true;
                config[c.Id] = [c];
            });

            var signerConfigs = _.values(config);
            that.currentSignOptions.config = signerConfigs;
            that.currentSignOptions.hasShowAlertChooseToken = hasShowAlertChooseToken;

            return selectedSigners;
        },

        _writeFileBase64: function (base64OfAttachmentFiles, selectedFiles, callback) {
            /*
             * Ghi nội dung các file cần kí ra thư mục tạm, chuẩn bị cho quá trình kí
             */
            var that = this;

            if (!base64OfAttachmentFiles) {
                callback();
                return;
            }

            // Lấy ra các giá trị key của file
            var fileIds = Object.keys(base64OfAttachmentFiles);
            if (fileIds.length === 0) {
                callback();
                return;
            }

            // lấy ra file đâu tiên để thực hiện kí
            var attachmentId = fileIds[0];
            var attachmentBase64 = base64OfAttachmentFiles[attachmentId];
            var attachment = selectedFiles.filter(function (item) {
                return item.get("Id") == attachmentId;
            });
            attachment = attachment[0];

            var extension = attachment.get("Extension");
            if (extension.indexOf(".") != 0) {
                extension = "." + extension;
            }

            var filename = attachment.get("Name").toLowerCase(); // attachment.get("Id") + extension;
            var filePath = that.tempFolderLoc + "\\" + filename;

            egov.extension.writeFileBase64(filePath, attachmentBase64, true, function (jsonResult) {

                var isSaved = jsonResult.returnCode === 1;
                if (isSaved) {
                    attachment.set('fileData', filePath);
                    attachment.set('md5', jsonResult.md5);

                    delete base64OfAttachmentFiles[attachmentId];

                    that._writeFileBase64(base64OfAttachmentFiles, selectedFiles, callback);
                } else {
                    // Nếu có lỗi thì báo và return luôn mà k cần gọi callback để dừng tiến trình xử lý lại
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                    return;
                }
            });
        },

        _writeImageSignature: function (signatureConfig, callback) {
            /*
             * Ghi tạm các file ảnh trong config kí
             */
            var that = this;

            if (signatureConfig.length === 0 || egov.isWriteSigner) {
                egov.isWriteSigner = true;
                egov.signatureConfig = that.signatureConfig;
                _.each(egov.signatureConfig, function (config) {
                    if (config.SignType !== 0) {
                        var docCode = that.document.isTransferTheoLo ? "" : that.document.model.get("DocCode");
                        config.ReplaceText = docCode.split('/')[0];
                    }
                });
                callback(that.signatureConfig);
                return;
            }

            var image = signatureConfig.pop();
            if (image == undefined) {
                callback(that.signatureConfig);
                return;
            }

            var isImageSigner = image.ImagePath && image.SignType === 0;
            if (isImageSigner) {
                var filename = '' + (new Date()).getTime() + image.Ext;
                var tempFolderLoc = (new Date()).getTime().toString();
                var filePath = tempFolderLoc + "\\" + filename;

                var filesize = egov.extension.writeFileBase64(filePath, image.ImagePath, true, function (jsonResult) {
                    if (jsonResult.returnCode == 1) {
                        filePath = filePath.replace(/\//g, '\\');
                        image.ImagePath = filePath;
                        that.signatureConfig.push(image);
                        that._writeImageSignature(signatureConfig, callback);
                    } else {
                        // Nếu có lỗi thì báo và return luôn mà k cần gọi callback để dừng tiến trình xử lý lại
                        egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                        return;
                    }
                });
            } else {
                image.ImagePath = "";
                image.ReplaceText = that.document.isTransferTheoLo ? "" : that.document.model.get("DocCode");
                that.signatureConfig.push(image);
                that._writeImageSignature(signatureConfig, callback);
            }
        },

        _signAndSendBtn: function () {
            var that = this;
            var sendDocument = function (isSigned, filePdfAddNew) {
                if (!isSigned || filePdfAddNew.length === 0) {
                    that.$confirmDialog.dialog("destroy");
                    that.callSignSuccess(false);
                    return;
                }

                egov.pubsub.publish(egov.events.status.success, "Kí file thành công!");
                that._uploadAndSend(filePdfAddNew);
            };

            this._doSign(sendDocument);
        },

        _signPreviewBtn: function () {
            var that = this;
            if (that.isPreviewing) {
                return;
            }

            that.isPreviewing = true;
            var openPreview = function (isSigned, filePdfAddNew) {
                if (!isSigned || filePdfAddNew.length === 0) {
                    that.$confirmDialog.dialog("destroy");
                    that.callSignSuccess(false);
                    return;
                }

                that._openPreviewSinged(filePdfAddNew);
            };

            this._doSign(openPreview);
        },

        _signWithPositionAndPreview: function () {
            if (!this.currentSignOptions) {
                return;
            }

            var that = this;
            var filePdfAddNew = [];
            var withChoosePoint = true;
            var openPreview = function (isSigned, filePdfAddNew) {
                if (!isSigned || filePdfAddNew.length === 0) {
                    that.$confirmDialog.dialog("destroy");
                    that.callSignSuccess(false);
                    return;
                }

                that._openPreviewSinged(filePdfAddNew);
            };

            that.currentSignOptions.withChoosePoint = true;

            egov.pubsub.publish(egov.events.status.info, "Vui lòng quét chuột chọn vị trí cần hiển thị chữ ký.");
            that._writeFilesAndSign(openPreview);
        },

        _openPreviewSinged: function (filePdfAddNew) {
            var that = this;
            var previewDialog = $("<div>");
            for (var i = 0; i < filePdfAddNew.length ; i++) {
                var file = filePdfAddNew[i];

                var pdfObj = $("<object>");
                pdfObj.css({ width: "100%", height: "350px", border: "none" });
                pdfObj.attr("data", "data:application/pdf;base64," + file.value);

                previewDialog.append(pdfObj);
            }

            previewDialog.appendTo("body");

            var setting = {
                width: 1000,
                height: 400,
                autoResize: true,
                title: "Ký số điện tử",
                buttons: [
                    {
                        text: "Quét chọn vị trí khác",
                        className: 'btn-warning',
                        click: function () {
                            previewDialog.dialog("destroy");
                            // Xóa thư mục đã ký trước đó trước khi ký lại
                            that.deleteFolder(that.tempFolderLoc, function () {
                                that._signWithPositionAndPreview();
                            });
                        }
                    },
                    {
                        text: "Chuyển",
                        className: 'btn-primary',
                        click: function () {
                            previewDialog.dialog("destroy");
                            that._uploadAndSend(filePdfAddNew);
                        }
                    },
                    {
                        text: "Bỏ qua",
                        className: 'btn-default',
                        click: function () {
                            that.isPreviewing = false;
                            previewDialog.dialog("destroy");
                        }
                    }
                ]
            };

            previewDialog.dialog(setting);
        },

        _doSign: function (signSuccess) {
            var selectedFiles,
                selectedSigners,
                filePdfAddNew,
                fileTempIds = [],
                fileIds = [],
                that = this;

            // Nếu không có file nào được chọn để kí => gọn callback để tiếp tục bàn giao.
            selectedFiles = that._getSelectedFiles(that.confirmSignFiles);
            selectedSigners = that._getSelectedSignerConfig(that.signatureConfig);

            if (selectedFiles.length <= 0 || selectedSigners.length <= 0) {
                egov.pubsub.publish(egov.events.status.warning, "Bạn chưa chọn tệp hoặc chữ ký để ký số. Vui lòng thử lại.");
                that.$confirmDialog.dialog("destroy");
                signSuccess(false, []);
                return;
            }

            // Lấy danh sách file cần tải từ server về để kí => ghi ra thư mục tạm => kí
            var serverFiles = selectedFiles.filter(function (item) {
                return !item.get("fileData") || (!egov.setting.publish.detectPdfChangeContent && egov.fileExtension.isReadonly(item.get("Name")));
            });

            // Tải danh sách file về để ký
            $.each(serverFiles, function (i, item) {
                if (item.get("isNew")) {
                    if (egov.isMobile) {
                        fileTempIds.push(item.get("Id") + '.' + item.get("Extension"));
                    } else {
                        fileTempIds.push(item.get("Id"));
                    }
                } else {
                    fileIds.push(item.get("Id"));
                }
            });

            // PROCESSING: Báo trạng thái đang xử lý
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

            egov.request.downloadAttachmentForSignBase64({
                data: {
                    fileIds: fileIds,
                    fileTempIds: fileTempIds,
                    convertWordTopdf: false
                },
                success: function (result) {
                    // Ghi toàn bộ nội dung file cần kí ra thư mục tạm.
                    that.currentSignOptions.base64OfAttachmentFiles = $.extend(true, {}, result.files);
                    that._writeFilesAndSign(signSuccess);
                },
                error: function (error) {
                    that.isPreviewing = false;
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                }
            });
        },

        _writeFilesAndSign: function (signSuccess) {
            var that = this;
            var hasShowAlertChooseToken = that.currentSignOptions.hasShowAlertChooseToken;
            var config = _.clone(that.currentSignOptions.config);
            var selectedFiles = _.clone(that.currentSignOptions.selectedFiles);
            var base64Attachments = _.clone(that.currentSignOptions.base64OfAttachmentFiles);

            var writeSuccess = function () {
                // Bắt đầu quá trình kí
                var filePdfAddNew = [];
                var currentSign = config.pop(); // currentSign is array
                currentSign.length === 0 && (currentSign = config.pop());

                var signSuccessWithToken = function () {
                    if (config.length === 0) {
                        signSuccess(true, filePdfAddNew);
                        return;
                    }

                    currentSign = config.pop();
                    that._signFileWithCertIndex(selectedFiles, currentSign, filePdfAddNew, signSuccessWithToken, hasShowAlertChooseToken);
                };

                that._signFileWithCertIndex(selectedFiles, currentSign, filePdfAddNew, signSuccessWithToken, hasShowAlertChooseToken);
            };

            this._writeFileBase64(base64Attachments, selectedFiles, writeSuccess);
        },

        _signFileWithCertIndex: function (selectedFiles, config, filePdfAddNew, success, hasConfirm) {
            // Ký các file với 1 token.

            if (config.length === 0) {
                return success();
            }

            if (hasConfirm) {
                var signName = _.pluck(config, 'Title').join(",");
                var message = String.format("Đang ký số cho chữ ký {0}. /nVui lòng cắm token vào máy và nhấn Ok để chọn.", signName);
                alert("Chọn Token cho chữ ký " + signName);
            }

            var that = this;

            if (this.currentSignOptions && this.currentSignOptions.idxCert) {
                this._signFile(selectedFiles, filePdfAddNew, config, this.currentSignOptions.idxCert, function () {
                    success();
                }, this.currentSignOptions.withChoosePoint);
                return;
            }

            // Chạy sau khi chọn token
            var callbackCertIndex = function (jsonResult) {
                var idxCert = parseInt(jsonResult.idxCert);
                if (isNaN(idxCert) || idxCert <= -1) {
                    // Báo không có chữ kí nào được chọn.
                    // Không đóng cửa sổ chọn file kí hiện tại (không gọi $confirmDialog.dialog("destroy");)
                    // Không tiếp tục bàn giao (Không gọi egov.callback(callback);)
                    that.isPreviewing = false;
                    egov.pubsub.publish(egov.events.status.error, "Vui lòng kiểm tra lại chữ kí số đã sẵn sàng trước khi kí!");
                    return;
                }

                that.currentSignOptions.idxCert = idxCert;
                that._signFile(selectedFiles, filePdfAddNew, config, idxCert, function () {
                    success();
                }, that.currentSignOptions.withChoosePoint);
            };

            egov.extension.getCertIndex(callbackCertIndex);
        },

        _signFile: function (selectedFiles, filePdfAddNew, config, idxCert, callback, withChoosePoint) {
            /// <summary>
            /// ký file
            /// </summary>
            /// <param name="item">file cần ký</param>
            if (selectedFiles.length === 0) {
                callback();
                return;
            }
            withChoosePoint = withChoosePoint || false;
            var item = selectedFiles.pop();

            // Đường dẫn file lưu trong %TEMP%/BkavEgovChrome
            var fileName = item.get("fileData");
            var isForSign = egov.fileExtension.isForSign(fileName);

            // Clone danh sach chu ky
            var cfigForSign = config.slice(0);
            if (isForSign) {
                //config = { Ext: "", FindText: "quangp", FindType: 1, : "", OffsetX: 0, OffsetY: 0, PosType: 3, SignType: 1, TextInfor: 1, Title: "Sign" };

                var cfig = cfigForSign.pop();
                var configSign = $.extend(true, { SignAuthor: egov.setting.userName, SignReason: "Bkav eGov - Ký số điện tử" }, cfig);

                var signedComplete = function (jsonResult) {
                    // jsonResult = {action: "Egov_SignFile", base64: "", fileName: "1504834898526\{attachmentId}.pdf", returnCode: 1, returnMessage: ""}
                    cfig = cfigForSign.pop();

                    if (jsonResult.returnCode == 1) {
                        if (cfig != undefined) {
                            if (egov.fileExtension.isMsOfficeFile(fileName)) {
                                fileName = egov.fileExtension.getFileName(fileName) + ".pdf";
                            }

                            configSign = $.extend(true, { SignAuthor: egov.setting.userName, SignReason: "Bkav eGov - Ký số điện tử" }, cfig);

                            this._signSelectFile(fileName, configSign, idxCert, signedComplete);
                            return;
                        }

                        var signedFilename = egov.fileExtension.getFileName(item.get("Name"));
                        if (!signedFilename.match(/_Signed$/)) {
                            signedFilename = signedFilename + "_Signed";
                        }
                        signedFilename = signedFilename + ".pdf";

                        filePdfAddNew.push({
                            id: '' + item.get("Id"),
                            documentCopyId: item.get("DocumentCopyId"),
                            name: signedFilename,
                            originName: jsonResult.fileName,
                            value: jsonResult.base64
                        });

                        this._signFile(selectedFiles, filePdfAddNew, config, idxCert, callback, withChoosePoint);
                    } else {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage + ": " + jsonResult.returnMessage);
                        return;
                    }
                }.bind(this);

                withChoosePoint
                    ? this._signByPoint(fileName, configSign, idxCert, signedComplete)
                    : this._signSelectFile(fileName, configSign, idxCert, signedComplete);
            }
        },

        _signSelectFile: function (localName, configSign, idxCert, callback) {
            // configSign.TSAURL = "http://ca.gov.vn/tsa";
            egov.extension.signFile(localName, configSign, parseInt(idxCert), function (jsonResult) {
                callback(jsonResult);
            }.bind(this));
        },

        _signByPoint: function (localName, configSign, idxCert, callback) {
            egov.extension.signFileByPoint(localName, configSign, parseInt(idxCert), function (jsonResult) {
                callback(jsonResult);
            }.bind(this));
        },

        _uploadAndSend: function (filePdfAddNew) {
            var that = this;
            this._uploadSignedFile(filePdfAddNew, this.document, function () {
                that.$confirmDialog.dialog("destroy");
                that.callSignSuccess(true);
                egov.pubsub.publish(egov.events.status.success, "Đính kèm file đã kí vào văn bản thành công!");
            });
        },

        _uploadSignedFile: function (signedFiles, document, callback) {
            /// <summary>
            /// Upload file đã ký lên
            /// </summary>
            document.uploadSignFiles(signedFiles, callback);
        },

        //#endregion

        //#region File đính kèm

        // Kiểm soát danh sách các file đang trong quá trình mở (tải về, ghi ra thư mục tạm, và mở) để tránh lặp khi click đúp liên tiếp
        openAttachment: function (attachment, version) {
            /// <summary>
            /// Mở file đính kèm
            /// </summary>
            /// <param name="attachment"></param>
            /// <param name="version"></param>
            var id, storePrivateId;
            id = attachment.model.get('Id');
            storePrivateId = attachment.parent.storePrivateId;

            //// Neu file nay dang trong qua trinh mo thi return
            //if (this.opennings[id]) {
            //    console.log("Info: openAttachment - click đúp mở file liên tiếp! Bỏ qua do đang tải và ghi file ra thư mục tạm.");
            //    return;
            //}

            // Danh dau qua trinh mo file bat dau
            //this.opennings[id] = id;

            // Nếu là file vừa đính kèm
            if (attachment.model.get('isNew')) {
                // Nếu file vừa đính kèm này đã được mở trước đó (đã có file ghi tạm) 
                // => Gọi lại lệnh mở file để mở file đã ghi tạm này nếu đã đóng, hoặc forcus lại file đang mở.
                if (attachment.model.get("isOpen") && attachment.model.get("fileData")) {
                    egov.extension.openFile(attachment.model.get("fileData"));
                }
                else {
                    // Nếu file chưa từng được mở sau khi đính kèm (chưa có file ghi tạm)
                    egov.request.downloadAttachmentTemp({
                        data: { id: id },
                        success: function (data) {
                            var result = JSON.parse(data);
                            if (result) {
                                if (result.error) {
                                    egov.pubsub.publish(egov.events.status.error, result.error);
                                } else {
                                    // Luu noi dung file vao object js truoc khi xu ly ghi file
                                    //attachment.model.set('base64', result.content);

                                    var extension = attachment.model.get("Extension");
                                    if (extension.indexOf(".") != 0) {
                                        extension = "." + extension;
                                    }

                                    var filename = attachment.model.get("Id") + extension;

                                    var tempFolderLoc = (new Date()).getTime().toString();
                                    filePath = tempFolderLoc + "\\" + filename;
                                    egov.extension.writeFileBase64(filePath, result.content, true, function (jsonResult) {

                                        // Danh dau qua trinh mo file hoan tat
                                        //delete this.opennings[id];

                                        // Neu ghi file thanh cong
                                        if (jsonResult.returnCode == 1) {
                                            attachment.model.set('fileData', filePath);
                                            attachment.model.set('md5', jsonResult.md5);
                                            egov.extension.openFile(filePath, function () {
                                                attachment.model.set('isOpen', true);
                                            });
                                        } else {
                                            // TODO: eGov xu ly cac truong hop loi nay:
                                            // - Yeu cau nguoi dung thu lai...                                            
                                        }
                                    }.bind(this));
                                }
                            }
                        }.bind(this),
                        error: function () {
                            // Danh dau qua trinh mo file hoan tat
                            //delete this.opennings[id];

                            egov.pubsub.publish(egov.events.status.error, egov.resources.document.attachment.errorDownload);
                        },
                        complete: function () {
                            egov.pubsub.publish(egov.events.status.destroy);
                        }
                    });
                }
            } else {
                var isLastVersion = version == attachment.model.get('LastestVesion');
                if (attachment.model.get("isOpen") && (!version || isLastVersion)) {
                    egov.extension.openFile(attachment.model.get("fileData"));
                }
                else {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

                    egov.request.downloadAttachment({
                        data: { id: id, storePrivateId: storePrivateId, version: version ? isLastVersion ? null : version : null },
                        success: function (data) {
                            var result = JSON.parse(data);
                            if (result) {
                                if (result.error) {
                                    egov.pubsub.publish(egov.events.status.error, result.error);
                                }
                                else {
                                    // var filename = attachment.model.get("Name");

                                    var extension = attachment.model.get("Extension");
                                    if (extension.indexOf(".") != 0) {
                                        extension = "." + extension;
                                    }

                                    //var filename = attachment.model.get("Name").toLowerCase() + extension;

                                    var filename = attachment.model.get("Id") + extension; //.get("Name").toLowerCase()

                                    var tempFolderLoc = (new Date()).getTime().toString();
                                    filePath = tempFolderLoc + "\\" + filename;
                                    egov.extension.writeFileBase64(filePath, result.content, true, function (jsonResult) {
                                        // Danh dau qua trinh mo file hoan tat
                                        //delete this.opennings[id];

                                        // Neu ghi file thanh cong
                                        if (jsonResult.returnCode == 1) {
                                            if (isLastVersion || !version) {
                                                attachment.model.set('fileData', filePath);
                                            }
                                            attachment.model.set('md5', jsonResult.md5);
                                            egov.extension.openFile(filePath, function () {
                                                if (isLastVersion || !version) {
                                                    attachment.model.set('isOpen', true);
                                                }
                                            });
                                        } else {
                                            // TODO: eGov xu ly cac truong hop loi nay:
                                            // - Yeu cau nguoi dung thu lai...
                                        }
                                    }.bind(this));
                                }
                            }
                        }.bind(this),
                        error: function () {
                            // Danh dau qua trinh mo file hoan tat
                            //delete this.opennings[id];

                            egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
                        },
                        complete: function () {
                            egov.pubsub.publish(egov.events.status.destroy);
                        }
                    });
                }
            }
        },

        openStoreAttachment: function (id) {
            egov.request.storePrivateOpenFile({
                data: { id: id },
                success: function (data) {
                    var result = JSON.parse(data);
                    if (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.error);
                        } else {
                            var filename = result.fileName;
                            var tempFolderLoc = (new Date()).getTime().toString();
                            filePath = tempFolderLoc + "\\" + filename;
                            egov.extension.writeFileBase64(filePath, result.content, true, function (jsonResult) {
                                // Danh dau qua trinh mo file hoan tat
                                //delete this.opennings[id];

                                // Neu ghi file thanh cong
                                if (jsonResult.returnCode == 1) {
                                    egov.extension.openFile(filePath, function () {

                                    });
                                } else {
                                    // TODO: eGov xu ly cac truong hop loi nay:
                                    // - Yeu cau nguoi dung thu lai...
                                }
                            }.bind(this));
                        }
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
                },
                complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },

        confirmAttachments: function (attachments, callback) {
            /// <summary>
            /// Lưu lại các file đang được sửa.
            /// </summary>
            /// <param name="callback">Hàm thực thi sau khi hoàn thành.</param>
            var that = this;
            var openFiles = attachments.model.select(function (file) {
                var isOpenning = !file.get('isRemoved') && file.get('isOpen');
                if (!attachments.isPublishing || !egov.setting.publish.detectPdfChangeContent) {
                    isOpenning = isOpenning && !egov.fileExtension.isReadonly(file.get("Name"));
                }
                return isOpenning;
            });

            if (openFiles.length === 0) {
                egov.callback(callback); return;
            }

            $(document).on('checkModifySuccess', function (e) {
                $(document).off('checkModifySuccess');

                egov.callback(callback);
                egov.helper.destroyClickEvent(e);
            });

            this._confirmSaveAttachment(openFiles, attachments);
        },

        _confirmSaveAttachment: function (openFiles, attachments) {
            /// <summary>
            /// Lưu các file đính kèm đang mở
            /// </summary>
            /// <param name="openFiles">Danh sách các file đang mở.</param>

            if (openFiles.length === 0) {
                $(document).trigger('checkModifySuccess');
                return;
            }

            var that = this;
            var file = openFiles[0];
            var remainFiles = openFiles.splice(1, openFiles.length - 1);

            this._closeFile(file, attachments, remainFiles);
        },

        _closeFile: function (file, attachments, newArray) {
            var that = this;
            egov.extension.closeFile(file.get("fileData"), function (closeResult) {
                if (closeResult.returnCode == 1 || closeResult.returnCode == 2) {
                    if (closeResult.returnCode == "2") {
                        console.log("egov.extension.closeFile: " + file.get('Name') + " đã đóng trước đó");
                    }

                    egov.extension.readFileBase64(file.get('fileData'), function (jsonResult) {
                        if (jsonResult.returnCode == 1) {
                            // So sanh noi dung file truoc va sau
                            // Nếu nội dung file có thay đổi
                            if (file.get("md5") != jsonResult.md5) {
                                var fileName = file.get('Name');
                                egov.message.show(String.format(egov.resources.document.attachment.fileChanged, fileName)
                                    , null
                                    , egov.message.messageButtons.YesNo
                                    , function () {
                                        attachments.modifiedFiles[file.get('Id')] = jsonResult.base64;

                                        // Tiếp tục hỏi với file tiếp theo
                                        that._confirmSaveAttachment(newArray, attachments);
                                    }
                                    , function () {
                                        // No: không lưu nội dung chỉnh sửa
                                        that._confirmSaveAttachment(newArray, attachments);
                                    });
                            }
                                // Nếu nội dung file không thay đổi
                            else {
                                that._confirmSaveAttachment(newArray, attachments);
                            }
                        } else if (jsonResult.returnCode == 2) {
                            console.log("egov.plugin::_confirmSaveAttachment::warning: Đọc nội dung file không tồn tại!!! Cần check lại code.");
                        } else {
                            // TODO: Báo cáo lỗi khi xử lý đóng file, đề nghị save file và đóng file đang mở và thử lại.
                            console.log("egov.plugin::_confirmSaveAttachment::Error: lỗi không thể lưu nội dung file");
                        }
                    });
                } else {
                    egov.message.show(String.format("Vui lòng đóng phần mềm đang mở file {0} và thử lại.", file.get('Name'))
                        , "Thông báo", egov.message.messageButtons.Ok);
                }
            });
        },

        openFileUri: function (uri, name) {
            var tempFolderLoc = (new Date()).getTime().toString();
            var filePath = tempFolderLoc + "\\" + name;
            if (!egov.extension) {
                egov.extension = PluginFactory.getInstance();
            }

            var filedata = getBinary(uri);
            var base64 = base64Encode(filedata);
            egov.extension.writeFileBase64(filePath, base64, true, function (jsonResult) {
                // Neu ghi file thanh cong
                if (jsonResult.returnCode == 1) {
                    egov.extension.openFile(filePath);
                }
            });
        },

        //#endregion

        //#region MAC

        getMAC: function (success) {
            if (!egov.extension) {
                egov.extension = PluginFactory.getInstance();
            }

            if (!egov.extension) {
                egov.callback(success, "");
                return;
            }

            egov.extension.getMAC(function (result) {
                egov.callback(success, result.mac);
            });
        },

        //#endregion

        //#region Delete Folder

        deleteFolder: function (path, success) {
            egov.extension.deleteFolder(path, true, function (result) {
                egov.callback(success);
            });
        },

        //#endregion
    }

    function base64Encode(str) {
        var CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        var out = "", i = 0, len = str.length, c1, c2, c3;
        while (i < len) {
            c1 = str.charCodeAt(i++) & 0xff;
            if (i == len) {
                out += CHARS.charAt(c1 >> 2);
                out += CHARS.charAt((c1 & 0x3) << 4);
                out += "==";
                break;
            }
            c2 = str.charCodeAt(i++);
            if (i == len) {
                out += CHARS.charAt(c1 >> 2);
                out += CHARS.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                out += CHARS.charAt((c2 & 0xF) << 2);
                out += "=";
                break;
            }
            c3 = str.charCodeAt(i++);
            out += CHARS.charAt(c1 >> 2);
            out += CHARS.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
            out += CHARS.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
            out += CHARS.charAt(c3 & 0x3F);
        }
        return out;
    }

    function getBinary(file) {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", file, false);
        xhr.overrideMimeType("text/plain; charset=x-user-defined");
        xhr.send(null);
        return xhr.responseText;
    }

    window.Plugin = EgovPlugin;
    return EgovPlugin;
})();

(function () {

    var TreeUtil = {
        bindJsTree: function (divTree, hasUser, hasCheckbox,
            hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind) {
            var deptRoot = _.find(arrDept, function (node) {
                return node.parentid === 0;
            });

            if (hasCheckbox) {
                plugins.push("checkbox");
            }
            if (hasDnD) {
                plugins.push("dnd");
            }
            if (deptRoot) {
                var children = _getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
                divTree.jstree({
                    "json_data": {
                        "data": [
                            {
                                "data": deptRoot.data.toString(),
                                "metadata": { id: deptRoot.value },
                                "state": "closed",
                                "attr": {
                                    "id": deptRoot.value, "rel": "dept",
                                    "idext": deptRoot.idext, "label": deptRoot.label
                                },
                                "children": children
                            }
                        ]
                    },
                    "crrm": hasDnD == false ? {} : {
                        "move": {
                            "check_move": function (m) {
                                var dept = _.find(arrDept, function (de) {
                                    return de.value === parseInt(m.o.attr('id'));
                                });
                                if (!dept) return false;
                                if (dept.level != 1) return false;
                                var p = this._get_parent(m.o);
                                if (!p) return false;
                                p = p == -1 ? this.get_container() : p;
                                if (p === m.np) return true;
                                if (p[0] && m.np[0] && p[0] === m.np[0]) return true;
                                return false;
                            }
                        }
                    },
                    "dnd": hasDnD == false ? {} : {
                        "drop_target": false,
                        "drag_target": false
                    },
                    "plugins": plugins
                }).bind("loaded.jstree", function (e, dataLoad) {
                    var depth = 1;
                    dataLoad.inst.get_container().find('li').each(function () {
                        if (dataLoad.inst.get_path($(this)).length <= depth) {
                            dataLoad.inst.open_node($(this));
                        }
                    });
                    divTree.bind("open_node.jstree", function (event, data) {
                        if (data.inst._get_children(data.rslt.obj).length == 0) {
                            _appendChild(data.rslt.obj, parseInt(data.rslt.obj.attr("id")), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles);
                        }
                    });
                });
            }
        }
    };

    egov.utils.treeUtil = TreeUtil;

    var _parseUserItem = function (value, name) {
        var template = '<li class="list-group-item">\
                                <div class="row">\
                                    <label class="checkbox document-color">\
                                       <input name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                        <span class="document-color-1"><i class="icon-check"></i></span>\
                                    </label>\
                                    <span style="margin-left: 15px;">{1}</span>\
                                </div>\
                            </li>';
        return $(String.format(template, value, name));
    };

    var _getChildrens = function (parentid, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
        var children = _.filter(arrDept, function (num) { return num.parentid == parentid; });
        var deptUsers = _.filter(arrDeptUserJobtitles, function (dept) {
            return dept.departmentid === parentid;
        });

        if (children.length > 0) {
            for (var j = 0; j < children.length; j++) {
                if (_getChildrens(children[j].value, false, arrDept, [], []).length > 0 || (hasUser && deptUsers.length > 0)) {
                    children[j].state = "closed";
                }
            }
        }

        if (hasUser && deptUsers.length > 0) {
            for (var i = 0; i < deptUsers.length; i++) {
                var userindept = _.find(arrUsers, function (user) {
                    return user.value === deptUsers[i].userid;
                });

                if (userindept) {
                    var selected = {
                        "value": "user_" + userindept.value,
                        "data": userindept.fullname,
                        "parentid": parentid,
                        "state": "leaf",
                        "metadata": { "id": "user_" + userindept.value },
                        "attr": {
                            "id": "user_" + userindept.value,
                            "rel": "people",
                            "idext": deptUsers[i].idext,
                            hasReceiveDocument: deptUsers[i].hasReceiveDocument
                        }
                    };
                    children.push(selected);
                }
            }
        }

        return children;
    };

    var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" hasReceiveDocument="${attr.hasReceiveDocument}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewCheckboxTemplate += '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var plugins = ["themes", "json_data", "ui", "crrm"];

    var _bindJsTree = function (divTree, hasUser, hasCheckbox,
        hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind) {
        var deptRoot = _.find(arrDept, function (node) {
            return node.parentid === 0;
        });
        if (hasCheckbox) {
            plugins.push("checkbox");
        }
        if (hasDnD) {
            plugins.push("dnd");
        }
        if (deptRoot) {
            var children = _getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
            divTree.jstree({
                "json_data": {
                    "data": [
                        {
                            "data": deptRoot.data.toString(),
                            "metadata": { id: deptRoot.value },
                            "state": "closed",
                            "attr": {
                                "id": deptRoot.value, "rel": "dept",
                                "idext": deptRoot.idext, "label": deptRoot.label
                            },
                            "children": children
                        }
                    ]
                },
                "crrm": hasDnD == false ? {} : {
                    "move": {
                        "check_move": function (m) {
                            var dept = _.find(arrDept, function (de) {
                                return de.value === parseInt(m.o.attr('id'));
                            });
                            if (!dept) return false;
                            if (dept.level != 1) return false;
                            var p = this._get_parent(m.o);
                            if (!p) return false;
                            p = p == -1 ? this.get_container() : p;
                            if (p === m.np) return true;
                            if (p[0] && m.np[0] && p[0] === m.np[0]) return true;
                            return false;
                        }
                    }
                },
                "dnd": hasDnD == false ? {} : {
                    "drop_target": false,
                    "drag_target": false
                },
                "plugins": plugins
            }).bind("loaded.jstree", function (e, dataLoad) {
                var depth = 1;
                dataLoad.inst.get_container().find('li').each(function () {
                    if (dataLoad.inst.get_path($(this)).length <= depth) {
                        dataLoad.inst.open_node($(this));
                    }
                });
                divTree.bind("open_node.jstree", function (event, data) {
                    if (data.inst._get_children(data.rslt.obj).length == 0) {
                        _appendChild(data.rslt.obj, parseInt(data.rslt.obj.attr("id")), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles);
                    }
                });
            });
        }
    };

    var _appendChild = function ($parent, parentId, hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles) {
        var child = _getChildrens(parentId, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
        if (child.length > 0) {
            var $newChild = $('<ul></ul>');
            $newChild.appendTo($parent);
            if (hasCheckbox) {
                // $.template("checkboxTemplate", itemTreeviewCheckboxTemplate);
                $.tmpl(itemTreeviewCheckboxTemplate, child).appendTo($newChild);
                $($parent).find("li").each(function (idx, listItem) {
                    $(listItem).addClass($parent.hasClass("jstree-checked") ? "jstree-checked" : "jstree-unchecked");
                });
            } else {
                // $.template("itemTreeviewTemplate", itemTreeviewTemplate);
                $.tmpl(itemTreeviewTemplate, child).appendTo($newChild);
            }
            $newChild.children("li:last").addClass("jstree-last");
        }
    };
})(egov || {});