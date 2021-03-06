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
    /// Tr??? v??? ResourceValue theo Key, n???u kh??ng t???n t???i, tr??? v??? ResourceKey
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
    /// H??m extend ????? th??m resource
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

// random 1 gi?? tr??? ????? ng???u nhi??n cho colorcode, m???i l???n t???i file
// n?? s??? mang 1 gi?? tr???, ?????m b???o t??nh ng???u nhi??n nh??ng v???n ?????ng nh???t
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
                "??????????????????????????????????????????????",
                "??????????????????????????????????????????????",
                "??",
                "??",
                "??????????????????????????????",
                "??????????????????????????????",
                "????????????",
                "????????????",
                "??????????????????????????????????????????????",
                "??????????????????????????????????????????????",
                "?????????????????????????????",
                "?????????????????????????????",
                "??????????????",
                "??????????????"
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
        /// Hi???n th??? log ra dev tool.
        /// </summary>
        /// <param name="logObject">Tham s??? log</param>
        if (typeof console !== 'undefined' && typeof console.log !== 'undefined') {
            console.log(logObject);
        }
    };

    egov.toJSON = function (jsonStr) {
        /// <summary>
        /// Tr??? v??? json object t??? chu???i json
        /// </summary>
        /// <param name="jsonStr">Chu???i json</param>
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
        /// Tr??? v??? t??n ?????i t?????ng ???????c l??u trong cache.
        /// </summary>
        /// <param name="entity">Entity c???a ?????i t?????ng.</param>

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
        /// Th???c thi h??m callback.
        /// </summary>
        /// <param name="callbackFunction">H??m callback</param>
        /// <param name=" param">Gi?? tr??? truy???n v??o h??m</param>
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
        /// <summary>?????nh d???ng th???i gian</summary>
        ///<param = "date"> th???i gian truy???n v??o</param>

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
                return "M???i g???i";
            }
            else if (diff < 120) {
                return "1 ph??t tr?????c.";
            }
            else if (diff < 3600) {
                return Math.floor(diff / 60) + " ph??t tr?????c.";
            }
            else if (diff < 7200) {
                return "1 gi??? tr?????c.";
            }
            else if (diff < 86400) {
                return Math.floor(diff / 3600) + " gi??? tr?????c.";
            }
        } else if (day_diff === 1) {
            return Globalize.format(date, "hh:mm tt") + " h??m qua.";
        } else if (day_diff > 1 && day_diff <= 7) {
            return Globalize.format(date, "hh:mm tt") + " " + Globalize.format(date, "dd") + " ng??y tr?????c.";
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
        /// Merge 2 m???ng theo property ch??? ?????nh
        /// </summary>
        /// <param name="arr1">M???ng 1</param>
        /// <param name="arr2">M???ng 2</param>
        /// <param name="prop">T??n property mu???n merge theo</param>
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
        /// Ki???m tra xem ph???n t??? combined c?? ch???a checkagainst hay kh??ng
        /// </summary>
        return ((combined & checkagainst) === checkagainst);
    };

    egov.contain = function (str, target) {
        /// <summary>
        /// Tr??? v??? gi?? tr??? x??c ?????nh chu???i c???n ki???m tra c?? thu???c m???t chu???i kh??c kh??ng.
        /// </summary>
        /// <param name="str">Chu??i</param>
        /// <param name="target">Chu???i c???n ki???m tra</param>
        if (typeof str !== "string" || typeof target !== "string") {
            egov.log("Tham s??? ?????u v??o kh??ng h???p l???.");
            return false;
        }
        return str.indexOf(target) > -1;
    }

    egov.getWordCount = function (str) {
        /// <summary>
        /// Tr??? v??? s??? t??? trong chu???i.
        /// </summary>
        /// <param name="str">Chu???i c???n ki???m tra</param>
        str = str.trim();
        return str.split(" ").length;
    };

    egov.stripVietnameseChars = function (input) {
        /// <summary>
        /// Tr??? v??? chu???i ti???ng vi???t kh??ng d???u
        /// </summary>
        /// <param name="input">chu???i ?????u v??o</param>
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
        /// Tr??? v??? danh s??ch sau khi s???p x???p ????a m???t gi?? tr??? theo property l??n ?????u
        /// </summary>
        /// <param name="objs">Danh s??ch c??c ?????i t?????ng c???n s???p x???p</param>
        /// <param name="sortBy">Thu???c t??nh c???n ki???m tra</param>
        /// <param name="value">Gi?? tr???</param>
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
        /// Tr??? v??? c???p ????? ph??ng ban
        /// </summary>
        /// <param name="extDeptIds">Chu???i m??? r???ng c???p ph??ng ban</param>

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
            preView: 1,     //HI???n th??? v??n b???n h??? s?? ??? khung preview (Ng?????i dung thi???t l???p hi???n  th??? to??n b??? th??ng tin v??n b???n h??? s?? ??? khung xem tr?????c v??n b???n)
            dialog: 2        //Hi???n th??? tr??n v??n b???n h??? s?? khi hi???n dialog khi click 'Chi ti???t v??n b???n ' ??? contextmenu
        },

        quickViewType: {
            hide: 0,   ///Kh??ng hi???n th??? t??m t???t v??n b???n
            right: 1,   //Hi???n th??? t??m t???t v??n b???n b??n ph???i
            below: 2   //Hi???n th??? t??m t???t v??n b???n b??n d?????i
        },

        documentOriginal: {
            egov: 0,
            egovOnline: 1,
            other: 2
        },

        fontSizeType: {
            nho: 0,  //Ch??? nh???
            vua: 1,  //Ch??? v???a
            lon: 2   //Ch??? l???n
        },

        searchType: {
            document: 1, //T??m v??n b???n.
            file: 2       //T??m trong file.
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
        /// ?????i t?????ng qu???n l?? c??c ajax http request t??? client l??n server.
        /// </summary>
        var check;

        this.profilers = [];

        this.dataDefaults = {
            type: 'GET',
            async: true,
            traditional: false,
            //global: false
        }

        // Ki???m tra c?? url tr??ng nhau
        // Khi th??m m???i 1 query n???u tr??ng s??? b??? b??o ngay.
        check = _.find(_.groupBy(_queries, function (q) { return q.url; }), function (g) {
            return g.length > 1;
        });

        if (check != null) {
            console.log("L???p url, ki???m tra l???i ????? ?????m b???o kh??ng b??? l???p: ");
            console.log(check);
        }

        // Ki???m tra t??n tr??ng nhau
        check = _.find(_.groupBy(_queries, function (q) { return q.name; }), function (g) {
            return g.length > 1;
        });

        if (check != null) {
            console.log("L???p t??n query, ki???m tra l???i ????? ?????m b???o kh??ng b??? l???p");
            console.log(check);
        }

        this.model = _queries;

        this.setDefaultAjaxSetting();
        this.init();

        this._profilers();
    }

    RequestManager.prototype.setDefaultAjaxSetting = function () {
        /// <summary>
        /// X??? l?? c??c thi???t l???p chung cho m???i ajax http request
        /// </summary>

        // X??? l?? l???i m???c ?????nh
        //$(document).ajaxError(function (e, jqXHR) {
        //    //if (jqXHR.status === 200) {

        //    //    // X??? l?? c??c m?? l???i ??? ????y
        //    //    // window.location.replace(egov.getRelativeEndpointUrl('/Error.html'));
        //    //    egov.log(jqXHR);
        //    //}
        //});
    }

    RequestManager.prototype.init = function () {
        /// <summary>
        /// Render c??c function theo c???u h??nh
        /// </summary>
        /// <returns type="this"></returns>
        var that = this, name;

        //ch??a danh s??ch ajax property khi run ajax call d??? li??u => ????? c?? th??? ph????ng th???c ajax
        this.aborts = {};

        // Render c??c function theo t??n c??c query trong danh s??ch
        _.each(this.model, function (query) {
            name = query['name'];
            that[name] = function (ajaxOption) {
                /// <summary>
                /// T??? ?????ng t???o h??m theo t??n c???a query.
                /// </summary>
                /// <param name="ajaxOption" type="object">jQuery ajax option</param>

                // closure: query
                var processName = query.name + JSON.stringify(ajaxOption.data) + "processing";
                that._exeQuery(query, ajaxOption);
                //if (!that[processName]) {
                //    that._exeQuery(query, ajaxOption);
                //} else {
                //    egov.log("G???i l???p ch???c n??ng " + query.name);
                //}
            }
        });

        return this;
    }

    RequestManager.prototype._exeQuery = function (query, ajaxOption) {
        /// <summary>Private: h??m th???c thi m???t query</summary>
        /// <param name="query" type="QueryModel">Query c???n th???c thi</param>
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

        //Overide l???i XmlhttpRequest ????? l???y tr???ng th??i k???t n???i c???a request ?????y ???????c tr??? v??? ????? ????nh tr???ng th??i k???t n???i
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

    // Danh s??ch t???t c??? c??c query t??? client l??n server
    var _queries = [

        //#region Common
        
        { name: 'getDataCompile', url: '/Document/GetDataCompile' },

        // L???y danh s??ch t???t c??? user trong h??? th???ng
        { name: 'getAllUsers', url: '/Common/GetAllUsers' },

        // L???y danh s??ch t???t c??? category trong h??? th???ng
        { name: 'getCategories', url: '/Common/GetCategories' },

        // L???y danh s??ch t???t c??? department theo user trong h??? th???ng
        { name: 'getDepartmentsByUser', url: '/Common/GetDepartmentsByUser' },

        // L???y danh s??ch t???t c??? department theo user trong h??? th???ng
        { name: 'getDepartmentsCurrent', url: '/Common/GetCurrentDepartments' },

        // L???y danh s??ch t???t c??? department trong h??? th???ng
        { name: 'getAllDepartment', url: '/Common/GetAllDepartment' },

        // L???y danh s??ch t???t c??? jobtitle trong h??? th???ng
        { name: 'getAllJobTitlies', url: '/Common/getAllJobTitlies' },

        { name: 'getAllUserDepartmentJobTitlesPosition', url: '/Common/GetAllUserDepartmentJobTitlesPosition' },

        // L???y ra danh s??ch t???t c??? c??c ch???c v??? trong h??? th???ng
        { name: 'getAllPosition', url: '/Common/GetAllPosition' },

        { name: 'getAllAddress', url: '/Common/GetAllAddress' },

        // L???y danh s??ch t???t c??? user trong h??? th???ng
        { name: 'getKeywords', url: '/Common/GetKeywords' },

        // L???y danh s??ch t???t c??? user trong h??? th???ng
        { name: 'getDocField', url: '/Common/GetDocField' },

        { name: 'getSendTypes', url: '/Common/GetSendTypes' },

        { name: 'getDeptAndUsers', url: '/Common/GetDeptAndUsers' },

        // L???y danh s??ch b???ng m??.
        { name: 'GetCodes', url: '/Common/GetCodes', type: 'Get' },

        // L???y danh s??ch c?? quan ban h??nh c???a ????n v??? hi???n t???i.
        { name: 'getOrganizations', url: '/Common/GetOrganizations', type: 'Get' },

        //#endregion

        //#region V??n b???n

        //S???a v??n b???n
        { name: 'editNew', url: '/Document/EditNew/', type: 'Get' },

        // L???y n???i dung form
        { name: 'getFormContent', url: '/Document/GetFormContent' },

        // L???y n???i dung form
        { name: 'getFormUrl', url: '/Document/GetFormUrl' },

        // X??c nh???n b??n giao
        { name: 'confirmTransfer', url: '/Document/ConfirmTransfer', type: 'Post', hasToken: true },

        // X??c nh???n x??? l??
        { name: 'confirmProcess', url: '/Document/ConfirmProcess', type: 'Post', hasToken: true },

        // L???y danh s??ch v??n b???n li??n quan
        { name: 'getDocumentRelations', url: '/document/GetDocumentRelations', type: 'Get' },

        // Gia h???n x??? l?? h??? s??
        { name: 'renewals', url: '/Document/Renewals', type: 'Post' },

        // L???y quy???n thao t??c x??? l?? v??n b???n h??? s??
        { name: 'getDocumentPermission', url: '/Document/GetDocumentPermission', traditional: true, async: true },

        // S???a v??n b???n
        { name: 'getDocumentInfoForEdit', url: '/Document/GetDocumentDetail' },

        // S???a v??n b???n
        { name: 'getMultiDocument', url: '/Document/GetDocumentDetails', traditional: true, },

        // M??? v??n b???n mobile
        { name: 'getDocumentInfoForMobile', url: '/Document/getDocumentInfoForMobile' },

         // T???o v??n b???n mobile
        { name: 'getDocumentInfoForCreateMobile', url: '/Document/GetDocumentInfoForCreateMobile' },

        // T???o v??n b???n
        { name: 'getDocumentInfoForCreate', url: '/Document/GetDocumentInfoForCreate' },

        // L???y l???i v??n b???n ??? c??c node
        { name: 'getContextItemForUndoTransfering', url: '/Document/GetContextItemForUndoTransfering/' },

        // X??a v??n b???n/h??? s??
        { name: 'removeDocument', url: '/Document/RemoveDocument', type: 'Post', hasToken: true, traditional: true, },

        // L???y l???i v??n b???n
        { name: 'undoTransfering', url: '/Document/UndoTransfering', type: 'Post', hasToken: true },

        // L???y nh???ng ng?????i nh???n v??n b???n ????? undo l???i
        { name: 'getUsersForUndoTransfering', url: '/Document/GetUsersForUndoTransfering' },

        // Set trang th??i ?????c v??n b???n
        { name: 'setDocumentViewed', url: '/Document/SetViewed/', type: 'Post' },

        // S???a h??? s?? ????ng k?? tr???c tuy???n
        { name: 'editDocumentOnline', url: '/Document/EditDocumentOnline/' },

        // S???a n???i dung h??? s??
        { name: 'editDocumentContent', url: '/Document/EditContent/', type: 'Post' },

        // Tr??? v??? c??c phi??n b???n c???a n???i dung v??n b???n
        { name: 'getDocumentContentVersion', url: '/Document/getDocumentContentVersion', type: 'Get' },

        // Lay y kien thuong dung cua nguoi dung
        { name: 'getCommonComments', url: '/Document/GetCommonComments/' },

        // Lay y kien thuong dung cua nguoi dung
        { name: 'getTemplateComments', url: '/Document/GetTemplateComments/' },

        { name: 'createTemplateComments', url: '/Document/CreateTemplateComments/', type: 'Post' },

        //Cap nhat m???u template ?? ki???n m?? ng?????i d??ng ???? so???n m???u tr?????c
        { name: 'updateTemplateComments', url: '/Document/UpdateTemplateComments', type: 'post' },

        //Tao moi m???u template ?? ki???n m?? ng?????i d??ng ???? so???n m???u tr?????c
        { name: 'deleteTemplateComments', url: '/Document/DeleteTemplateComments', type: 'post' },

        // Tim kiem van ban
        { name: 'searchDocuments', url: '/Document/SearchDocuments', type: 'Post', traditional: true },

        // Thi???t l???p v??n b???n quan trong hay kh??ng quan tr???ng
        { name: 'setDocumentImportant', url: '/Document/SetDocumentImportant', type: 'Post' },

        // Xem tr?????c th??ng tin v??n b???n h??? s??
        { name: 'quickViewDocument', url: '/Home/QuickViewDocument', type: 'Get' },

        // L???y file c???u h??nh form v??n b???n
        { name: 'getDocumentTemplate', url: '/Document/GetDocumentTemplate', type: 'Get' },

        
        // L???y file c???u h??nh form v??n b???n
        { name: 'getCatalog', url: '/Document/GetCatalog', type: 'Get' },

        // C???p nh???t k???t qu??? x??? l?? cu???i c??ng
        { name: 'updateLastResult', url: '/Document/UpdateLastResult', type: 'Post' },

        // T???o ???nh t??? file PDf, tr??? v??? url ???nh
        { name: 'createImagesFromBeginAndLastPdfPages', url: '/Document/CreateImagesFromBeginAndLastPdfPages', type: 'Get' },

        { name: 'GetDocumentInfoFromScan', url: '/Parallel/GetDocumentInfoFromScan', type: 'Get', traditional: true },

         // tr??? v??? url ???nh
        { name: 'getImageTemp', url: '/Document/GetImageTemp', type: 'Get' },

        // H???y s??? ???? c???p
        { name: 'cancelCode', url: '/Document/CancelCode', type: 'Get' },

        // L???y danh s??ch s??? k?? hi??u, m?? h??? s??
        { name: 'GetDocCodes', url: '/Document/GetDocCodes', type: 'Get' },

        // L???y danh s??ch s??? ?????n ??i
        { name: 'GetInOutCode', url: '/Document/GetInOutCode', type: 'Get' },

        // L???y tr???ng th??i ph??t h??nh
        { name: 'GetIsTransferPublish', url: '/Document/GetIsTransferPublish', type: 'Get' },

        // X??a doc paper
        { name: 'deleteDocPaper', url: '/Document/DeleteDocPaper', type: 'Post' },

        // X??a doc fee
        { name: 'deleteDocFee', url: '/Document/DeleteDocFee', type: 'Post' },

        // Thu h???i v??n b???n
        { name: 'acceptThuHoi', url: '/Document/AcceptThuHoi', type: 'Post' },

        // L???y lo???i h??? s??, v??n b???n
        { name: 'getDoctype', url: '/Doctype/GetDocType' },

        //Lay loai van ban
        { name: 'getDocTypes', url: '/Doctype/GetDocTypes' },

        // Tr??? v??? gi???y t??? v?? l??? ph?? c???a doctype
        { name: 'getDoctypePaperAndFees', url: '/Doctype/GetPaperAndFees' },

        // C???p nh???t gi???y t??? v?? l??? ph?? c???a lo???i h??? s??
        { name: 'updateDoctypePaperAndFees', url: '/Doctype/UpdatePaperAndFees', type: 'Post' },

        // X??a doctype paper
        { name: 'deleteDoctypePaper', url: '/Document/DeleteDoctypePaper', type: 'Post' },

        // X??a doctype paper
        { name: 'deleteDoctypeFee', url: '/Document/DeleteDoctypeFee', type: 'Post' },

        // Ki???m tra s??? k?? hi???u ???? ???????c d??ng
        { name: 'checkDocCodeIsUsed', url: '/Document/CheckDocCodeIsUsed' },

        //#endregion

         //#region Mission (T???o nhi???m v???)

        // L???y th??ng tin user
        { name: 'GetListUser', url: '/Mission/GetListUser', type: 'Get' },

        // T???o nhi???m v???
        { name: 'CreateMission', url: '/Mission/CreateMission', type: 'post', traditional: true, async: true },

        // L???y th??ng tin user
        { name: 'LinkDetailMission', url: '/Mission/LinkDetailMission', type: 'post', traditional: true, async: true },

         //#endregion Mission (T???o nhi???m v???)

        //#region ????nh k??m

        // T???i v??? t???p ????nh k??m m???i t???i l??n.
        { name: 'downloadAttachmentTemp', url: '/Attachment/DownloadAttachmentTempBase64', type: 'Get' },

        // T???i v??? t???p ????nh k??m c?? s???n
        { name: 'downloadAttachment', url: '/Attachment/DownloadAttachmentBase64', type: 'Get' },

        // T???i v??? t???p ????nh k??m ????? k??
        { name: 'downloadAttachmentForSignBase64', url: '/Attachment/DownloadAttachmentForSignBase64', type: 'Get', traditional: true },

        //Upload file scan
        { name: 'uploadTempScan', url: '/Attachment/UploadTempScan', type: 'Post' },

        //#endregion

        //#region Workflow

         // L???y danh s??ch c??c h?????ng chuy???n v???i v??n b???n ??ang edit
        { name: 'getAction', url: '/Workflow/GetActions', type: 'Get' },
        // L???y danh s??ch c??c h?????ng chuy???n v???i v??n b???n ??ang edit
        { name: 'getDocumentEditAction', url: '/Workflow/GetActionsEdit', type: 'Get' },

        // L???y danh s??ch c??c h?????ng chuy???n v???i v??n b???n t???o m???i
        { name: 'getDocumentCreateAction', url: '/Workflow/GetActionsCreate', type: 'Get' },

        // L???y danh s??ch ng?????i nh???n theo h?????ng chuy???n
        { name: 'getUserByAction', url: '/Workflow/GetUserByAction', type: 'Get' },

        // L???y c??c h?????ng chuy???n c???a ng?????i d??ng theo duwj kieen
        { name: 'getActionsTransferPlan', url: '/Workflow/GetActionsTransferPlan', type: 'Get' },

         // L???y danh s??ch ng?????i nh???n theo h?????ng chuy???n theo lo
        { name: 'getUserByActionTheoLo', url: '/Workflow/GetUserByActionTheoLo', type: 'post', traditional: true },

        // L???y danh s??ch h?????ng chuy???n theo lo
        { name: 'getActionTheoLoVanBan', url: '/Workflow/GetActionTheoLoVanBan', type: 'post', traditional: true, async: true },

        //#endregion

        //#region X??? l?? v??n b???n

        // Phat hang va ket thuc
        { name: 'publishAndFinish', url: '/Transfer/PublishAndFinish', type: 'Post', traditional: true, hasToken: true },
        // Chuy???n v??n b???n
        { name: 'transfer', url: '/Transfer/TransferDocument', type: 'Post', traditional: true, hasToken: true },
        //ph??t h??nh phi???u kh???o s??t
        { name: 'surveyRelease', url: '/Transfer/SurveyReleased', type: 'Post', traditional: true, hasToken: true },
        // hoan th??nh phi???u kh???o s??t
        { name: 'surveyComplete', url: '/Transfer/SurveyComplete', type: 'Post', traditional: true, hasToken: true },
        // Chi??nh s????a c????u hi??nh ba??o ca??o
        { name: 'surveySaveReport', url: '/Transfer/SurveySaveReport', type: 'Post', traditional: true, hasToken: true },
        // Chuyen theo lo
        { name: 'transferTheoLo', url: '/Transfer/TransferMultiple', type: 'Post', traditional: true, hasToken: true },

        // Chuy???n v??n b???n
        { name: 'lightTransfer', url: '/Transfer/LightTransfer', type: 'Post' },

        // Chuy???n ?? ki???n ????ng g??p: cho v??n b???n xin ?? ki???n, ?????ng x??? l??.
        { name: 'TransferYKienDongGop', url: '/Transfer/TransferAnswer', type: 'Post', traditional: true, hasToken: true },

        // Ti???p nh???n h??? s??.
        { name: 'TransferTiepNhan', url: '/Transfer/TransferTiepNhan', type: 'Post', traditional: true, hasToken: true },

        // Th??ng b??o
        { name: 'TransferAnnouncement', url: '/Transfer/TransferAnnouncement', type: 'Post', traditional: true, hasToken: true },

        // Xin ?? ki???n
        { name: 'TransferConsult', url: '/Transfer/TransferConsult', type: 'Post', traditional: true, hasToken: true },

        // Ph??t h??nh b??o c??o l??n VP ch??nh ph???
        { name: 'publishGov', url: '/Publish/TransferPublishGov', type: 'Post', traditional: true, hasToken: true },

        // L??u s??? v?? ph??t h??nh v??n b???n
        { name: 'publish', url: '/Publish/TransferPublish', type: 'Post', traditional: true, hasToken: true },

        // L??u s??? v?? ph??t h??nh n???i b???
        { name: 'privatePublish', url: '/Publish/TransferPrivatePublish', type: 'Post', traditional: true, hasToken: true },

        // L??u s??? v?? ph??t h??nh n???i b??? theo l??
        { name: 'privatePublishTheoLo', url: '/Publish/TransferPrivatePublishTheoLo', type: 'Post', traditional: true, },

        // L??u s??? v?? ph??t h??nh v??n b???n theo l??
        { name: 'publishTheoLo', url: '/Publish/TransferPublishTheoLo', type: 'Post', traditional: true, hasToken: true },

        // D??? ki???n ph??t h??nh
        { name: 'publishmentPlan', url: '/Publish/PublishmentPlan', type: 'Post' },

        // Ph??t h??nh ti???p
        { name: 'rePublish', url: '/Publish/RePublish', type: 'Post', traditional: true },

        // C???p nh???t v??n b???n
        { name: 'saveDoc', url: '/Transfer/SaveDoc', type: 'Post', hasToken: true, traditional: true },

        // L??u v??n b???n d??? th???o
        { name: 'saveDocDraft', url: '/Transfer/SaveDocDraft', type: 'Post', hasToken: true },

        // L??u v??n b???n d??? th???o
        { name: 'transferLienThong', url: '/Publish/transferLienThong', type: 'Post', hasToken: true, traditional: true },

        // Thu h???i v??n b???n li??n th??ng
        { name: 'recalledLienThong', url: '/Publish/RecalledLienThong', type: 'Post', hasToken: true, traditional: true },

        // G???i li??n th??ng l???i
        { name: 'resendLienThong', url: '/Transfer/ResendLienThong', type: 'Post', hasToken: true, traditional: true },

        // G???i ?? ki???n
        { name: 'sendComment', url: '/Document/SendComment', type: 'Post', hasToken: true },

         // ????nh ch??nh v??n b???n
        { name: 'dinhchinh', url: '/Finish/DinhChinh', type: 'Post' },
         // K???t th??c x??? l?? v??n b???n
        { name: 'fiAndPub', url: '/Finish/FinishAndPublish', type: 'Post', hasToken: true },

        // K???t th??c x??? l?? v??n b???n
        { name: 'finish', url: '/Finish/UpdateFinish', type: 'Post', hasToken: true },

         // L???y l???i v??n b???n ???? k???t th??c
        { name: 'undoFinish', url: '/Finish/UndoFinish', type: 'Post' },

        // K?? duy???t
        { name: 'approverSend', url: '/Approver/Send', type: 'Post', hasToken: true },

        { name: 'deleteApprover', url: '/Approver/deleteApprover', type: 'Post', hasToken: true },

        { name: 'deleteResult', url: '/Document/DeleteResult', type: 'Post', hasToken: true },

        //#endregion

        //#region h???i ????p

        { name: 'getNodeQuestion', url: '/Question/GetNode', type: 'Get' },

        { name: 'getsQuestion', url: '/Question/GetQuestions', type: 'Get' },

        { name: 'answerQuestion', url: '/Question/Answer', type: 'POST' },

        { name: 'rejectQuestion', url: '/Question/Reject', type: 'POST' },

        { name: 'forwardQuestion', url: '/Question/ForwardQuestion', type: 'POST' },

        { name: 'rejectAnswer', url: '/Question/RejectAnswer', type: 'POST' },

        { name: 'getForwardList', url: '/Question/GetForwardList', type: 'Get' },

        { name: 'getsHolderList', url: '/Question/GetsHolderList', type: 'Get' },

        //#endregion

        //#region H??? s?? c?? nh??n

        // L???y danh s??ch S??? v??n b???n
        { name: 'GetStores', url: '/Common/GetStores', type: 'Get' },

        // L???y danh s??ch h??? s?? c?? nh??n, h??? s?? chia s???
        { name: 'getStorePrivate', url: '/StorePrivate/Gets', type: 'Get' },

        // T???o m???i h??? s?? c?? nh??n,h??? s?? chia s???
        { name: 'createStorePrivate', url: '/StorePrivate/Create', type: 'Post', hasToken: true, traditional: true },

        // C???p nh???t h??? s?? c?? nh??n. h??? s?? chia s???
        { name: 'updateStorePrivate', url: '/StorePrivate/Update', type: 'Post', hasToken: true, traditional: true },

        { name: 'anycStoreShare', url: '/StorePrivate/AnycStoreShare', type: 'Get' },

        // M??? h??? s?? c?? nh??n. h??? s?? chia s???
        { name: 'openStorePrivate', url: '/StorePrivate/Open', type: 'Post', hasToken: true },

        // ????ng h??? s?? c?? nh??n. h??? s?? chia s???
        { name: 'closeStorePrivate', url: '/StorePrivate/Close', type: 'Post', hasToken: true },

        // X??a h??? s?? c?? nh??n. h??? s?? chia s???
        { name: 'deleteStorePrivate', url: '/StorePrivate/Delete', type: 'Post', hasToken: true },

        // L???y danh s??ch v??n b???n h??? s?? trong h??? s?? c?? nh??n, h??? s?? chia s???
        { name: 'getStorePrivateDocuments', url: '/StorePrivate/GetDocuments', type: 'Post' },

        // X??a v??n b???n ra kh???i h??? s??
        { name: 'removeStorePrivateDocument', url: '/StorePrivate/RemoveDocuments', type: 'Post', traditional: true },

        //L???y danh s??ch ng?????i d??ng trong h??? s?? c?? nh??n. h??? s?? chia s???
        { name: 'getUserJoined', url: '/StorePrivate/GetUserJoined', type: 'Get' },

        //Th??m v??n b???n v??o h??? s?? c?? nh??n/chia s???
        { name: 'SaveDocumentToStorePrivate', url: '/Document/SaveDocumentToStorePrivate', type: 'Post', hasToken: true },

        // M??? file t??? h??? s?? c?? nh??n, h??? s?? chia s???
        { name: 'storePrivateOpenFile', url: '/StorePrivate/DownloadAttachmentBase64', type: 'Get' },

        // Xo?? file trong h??? s??
        { name: 'storePrivateRemoveFile', url: '/StorePrivate/RemoveAttachment', type: 'post', hasToken: true },

        // Lo???i h??? s?? kh???i h??? s?? th??ng b??o
        { name: 'removeDocumentAnnouncement', url: '/Document/RemoveDocumentAnnouncement', type: 'post' },

        //#endregion

        //#region Danh s??ch v??n b???n

        // L???y danh s??ch v??n b???n theo kho
        { name: 'getDocumentStore', url: '/Home/GetDocumentStore' },

        //#endregion

        //#region C??y v??n b???n

        //L???y danh s??ch c??y v??n b???n
        { name: 'getDocumentTree', url: '/Home/GetFunctionByParentId', type: 'get' },

        //L???y danh s??ch c??y v??n b???n c?? h?????ng chuy???n theo l??
        { name: 'getDocumentTreeHasTransferTheoLo', url: '/Home/GetFunctionHasTransferTheoLoByParentId', type: 'get' },

        // L???y danh s??ch c??y v??n b???n
        { name: 'syncDocumentStore', url: '/Home/SyncDocumentStore' },

        // L???y danh s??ch c??c kho v??n b???n
        { name: 'getFunctionGroups', url: '/Home/GetFunctionGroups' },

        //#endregion

        //#region Home

        // L???y c??c c???u h??nh c???a ng?????i d??ng
        { name: 'getCommonConfigs', url: '/Home/GetCommonConfigs', type: 'Get' },

        // L???y c??c c???u h??nh b??? l??u s???
        { name: 'hasHideSaveStore', url: '/Home/HasHideSaveStore', type: 'Post' },

        // L???y ng??y h???t h???n
        { name: 'getDateAppointed', url: '/Document/GetDateAppointed', type: 'Post' },

        // Thi???t l???p c??c config c???a ng?????i d??ng
        { name: 'setUserConfig', url: '/Account/SetUserConfig/', type: 'Post' },

        // Thi???t l???p c??c config c???a ng?????i d??ng
        { name: 'setPopUpSize', url: '/Account/setPopUpSize/', type: 'Post' },

        { name: 'filterCitizen', url: '/Document/FilterCitizen/', type: 'Get' },

        // Trang in
        { name: 'print', url: '/Print/Index', type: 'Get' },

        { name: 'previewPrint', url: '/Print/PreviewPrint', type: 'Get' },

        // Tr??? v??? danh s??ch c??c phi???u in c???a h??? s?? 
        { name: 'getPrints', url: '/Print/GetPrints', type: 'Get' },

        // Tr??? v??? danh s??ch c??c m???u phi???u in theo nghi???p v???
        { name: 'getPrintTemplates', url: '/Print/GetPrintTemplates', type: 'Get' },

        // Tr??? v??? danh s??ch c??c m???u phi???u in theo danh s??ch h??? s??
        { name: 'getPrintByDocCopys', url: '/Print/GetPrintByDocCopys', type: 'Get' },

        // In phi???u bi??n nh???n
        { name: 'quickPrint', url: '/Print/QuickPrint', type: 'Get' },

        { name: 'getPrinters', url: '/Print/GetActivePrinters', type: 'Get' },

        { name: 'printTransferHistory', url: '/Print/PrintTransferHistory', type: 'Get' },

        // Tr??? v??? d??? li???u cho form tr??? k???t qu???
        { name: 'getReturnResult', url: '/Return/GetReturnResult', type: 'Get' },

        // Tr??? v??? d??? li???u cho form tr??? k???t qu???
        { name: 'updateReturn', url: '/Return/UpdateReturn', type: 'Post' },

        // Form y??u c???u b??? sung m???i
        { name: 'createSupplementary', url: '/Supplementary/CreateSupplementary', type: 'Get' },

        // Ti???p nh???n b??? sung
        { name: 'receiveSupplementary', url: '/Supplementary/GetDetails', type: 'Get' },

        // Ti???p nh???n b??? sung - Posts
        { name: 'supplementaryReceive', url: '/Supplementary/Receive', type: 'Post' },

        // Tr??? v??? ng??y h???n tr??? m???i khi ti???p nh???n b??? sung
        { name: 'getNewDateAppointed', url: '/Supplementary/GetDateAppointed', type: 'Get' },

        // T???o y??u c???u b??? sung h??? s??
        { name: 'sendRequiredSupplementary', url: '/Supplementary/SendRequire', type: 'Post', hasToken: true },

        // H???y y??u c???u b??? sung
        { name: 'cancelReceiveSupplementary', url: '/Supplementary/CancelReceive', type: 'Post', hasToken: true },

        // Ti???p t???c x??? l??
        { name: 'continueProcess', url: '/Supplementary/continueProcess', type: 'Post' },

        //T??m ki???m nhanh
        { name: 'quickSearch', url: '/Search/QuickSearch', type: 'Get', traditional: true },

        { name: 'getMailTemplates', url: '/Document/GetMailTemplates', type: 'Get', },

        { name: 'getSmsTemplates', url: '/Document/GetSmsTemplates', type: 'Get', },

        { name: 'sendMailToPeople', url: '/Document/SendMailToPeople', type: 'Post', },

        { name: 'sendSmsToPeople', url: '/Document/SendSmsToPeople', type: 'Post', },

        { name: 'getVersionValue', url: '/Home/GetVersionValue', type: 'Get' },


        //#endregion

        //#region Kh??c - ki???m tra l???i n???u kh??ng d??ng th?? b???

        // L???y ra t???ng s??? c??c v??n b???n h??? s?? ch??a ?????c
        { name: 'getTotalDocumentUnreadMultiFunction', url: '/Parallel/GetTotalDocumentUnreadMultiFunction', type: 'Post' },

        { name: 'getTotalDocumentUnread', url: '/Home/GetTotalDocumentUnread', type: 'Post' },

        // L???y danh s??ch h??? s??, v??n b???n
        { name: 'getDocuments', url: '/home/GetDocuments', type: 'Post' },

        // L???y to??n b??? danh s??ch v??n b???n c???a node hi???n t???i
        { name: 'getAllDocument', url: '/Home/GetAllDocument', type: 'Post' },

        // L???y danh s??ch h??? s?? v??n b???n theo ph??n trang
        { name: 'getDocumentPaging', url: '/Home/GetDocumentPaging', type: 'Post' },

        // L???y danh s??ch h??? s??, v??n b???n m???i ???????c th??m v??o node v?? nh???ng v??n b???n x??a kh???i node
        { name: 'getLastestDocuments', url: '/Home/GetLastestDocuments', type: 'Post', traditional: true },

          // L???y danh s??ch h??? s??, v??n b???n m???i ???????c th??m v??o node v?? nh???ng v??n b???n x??a kh???i node
        { name: 'getLastestReports', url: '/HomeSMReport/GetLastestDocuments', type: 'Post', traditional: true },

         { name: 'getReports', url: '/HomeSMReport/GetDocuments', type: 'Post', traditional: true },

        //T??m ki???m n??ng cao
        { name: 'searchAdvance', url: '/Search/SearchAdvance', type: 'Get', traditional: true },

        //l???y form t??m ki???m n??ng cao
        { name: 'getSearchAdvanceForm', url: '/Search/GetSearchAdvanceForm', type: 'Get', },

        //l???y form t??m ki???m n??ng cao
        { name: 'getDiffVersionTrees', url: '/Home/DiffVersionTree', type: 'Get', },

        //#endregion

        //#region ????ng k?? qua m???ng

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

        //Tong so van ban dang ky qua mang b??? h???y b???
        { name: 'getTotalOnlineCancel', url: '/DocumentOnline/GetTotalOnlineCancel', type: 'Get' },

         //Danh sach van ban dang ky qua mang b??? h???y b???
        { name: 'getDocumentOnlineCancel', url: '/DocumentOnline/GetDocumentOnlineCancel', type: 'Get' },

        //chi tiet hos dang ky qua mang
        { name: 'getDocumentDetailOnlineRegistration', url: '/DocumentOnline/GetDocumentDetailOnlineRegistration', type: 'Get' },

        //Ki???m tra h??? s?? c??ng d??n ??ang ????ng k?? tr???c tuy???n
        { name: 'checkDocumentOnline', url: '/DocumentOnline/CheckDocumentOnline/', type: 'Get' },

        //Ki???m tra danh s??ch h??? s?? ??ang c?? c???a c??ng d??n tr??n h??? th???ng
        { name: 'checkDocument', url: '/DocumentOnline/CheckDocument/', type: 'Get' },

        //M??? l???i c??ng v??n k???t th??c nh???m
        { name: 'reOpenDocument', url: '/Document/ReOpenDocument', type: 'Get' },

        //Xuat danh sach ra file
        { name: 'exportToFile', url: '/Home/ExportToFile', type: 'Post' },

          //L???y m???u ph??i c???a mail, sms
        { name: 'editTemplate', url: '/Document/EditTemplate', type: 'Get' },

        //#endregion

        //#region Mobile

        // L???y t???ng s??? v??n b???n th??ng b??o
        { name: 'notificationsCount', url: '/Mobile/GetNotificationsCount', type: 'Get' },

        // Thi???t l???p c??c config c???a ng?????i d??ng cho Mobile
        { name: 'setMobileUserConfig', url: '/Account/SetMobileUserConfig/', type: 'Post', hasToken: true },

        //#endregion

        //L???y X??a t???p ????nh k??m
        { name: 'removeAttachment', url: '/Attachment/RemoveAttachment', type: 'Post' },

        // l???y t???ng s?? c??u h???i chung
        { name: 'getTotalGeneralQuestion', url: '/Question/GetTotal?isGetGeneral=true', type: 'Get' },

        // l???y t???ng s?? c??u h???i theo h??? s??
        { name: 'getTotalDocumentQuestion', url: '/Question/GetTotal?isGetGeneral=false', type: 'Get' },

         //#region Gi???y ph??p doanh nghi???p
        { name: 'getBusinessLicense', url: '/BusinessLicense/BusinessLicenses', type: 'Get' },

         //#region Gi???y ph??p doanh nghi???p
        { name: 'removeBusinessLicense', url: '/BusinessLicense/RemoveLicenses', type: 'Post', traditional: true },

           //#region Gi???y ph??p doanh nghi???p
        { name: 'createCitizen', url: '/BusinessLicense/CreateCitizen', type: 'Post', traditional: true },

            //#region Gi???y ph??p doanh nghi???p
        { name: 'createLicense', url: '/BusinessLicense/CreateLicense', type: 'Post', traditional: true },
        //#endregion

        // T???o m???i h??a ????n
        { name: 'createInvoice', url: '/DocumentInvoice/ImportInvoice', type: 'Post' },

        // l???y h??a ????n
        { name: 'getInvoice', url: '/DocumentInvoice/LookupInvoice', type: 'Get' },

        // chi ti???t h??a ????n
        { name: 'getDetailInvoice', url: '/DocumentInvoice/DetailInvoice', type: 'Get' },

        // chi ti???t h??a ????n
        { name: 'removeInvoice', url: '/DocumentInvoice/RemoveInvoice', type: 'Post' },

        //#region Mobile

        // L???y t???ng s??? v??n b???n th??ng b??o
      { name: 'createVote', url: '/Referendum/Vote', type: 'Post', traditional: true },
      { name: 'updateVote', url: '/Referendum/VoteUpdate', type: 'Post', traditional: true },
      { name: 'deleteVote', url: '/Referendum/DeleteVote', type: 'Post', traditional: true },
      // L???y t???ng s??? c??c vote c???u ng?????i d??ng hi???n t???i
      { name: 'getVotes', url: '/Referendum/GetVotes', type: 'Get' },
      // L???y t???ng s??? c??c vote c???u ng?????i d??ng hi???n t???i
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

    // Qu???n l?? c??c ?????i t?????ng d??? li???u l??u ??? client
    // {
    //    name: "userConfig",  => T??n ?????i t?????ng v?? l?? t??n key l??u trong cache.
    //    hasCache: false,     => Gi?? tr??? x??c ?????nh ?????i t?????ng d??? li???u c?? l??u cache offline d?????i client hay kh??ng.
    //    hasSessionCache: true, => Gi?? tr??? x??c ?????nh ?????i t?????ng d??? li???u c?? l??u l???i theo phi??n hay kh??ng.
    //    request: egov.request.getCommonConfigs, => trong egov.request-manager.js; set null ????? t???o ?????i t?????ng thu???n d?????i client
    //    id: 0,                                  => truy???n v??o Id ????? ph??n bi???t c??c ?????i t?????ng l??u c?? c??ng request: 
    //                                                  userConfig_1, userConfig_2,...
    //    isReplaceWhenSync: true,                => Gi?? tr??? x??c ?????nh replace l???i k???t khi ?????ng b??? k???t qu??? v??? hay merge v???i 
    //                                                  gi?? tr??? c??
    //    isInsertToCurrentCache:true => Trong tr?????ng h???p kh??ng query l??n server l???y to??n b??? 1 l?????t d??? li???u (nh?? allUsers) m?? l???y l???n l?????t 
    //                                          d??? li???u (nh?? template c???a v??n b???n, l??c n??o m??? ra m???i l???y), gi?? tr??? n??y x??c ?????nh 
    //                                          m???i khi l???y 1 l???n th?? c?? insert th??m v??o d??? li???u d?????i client hay replace to??n b???
    //                                    => D??? li???u s??? l??u d?????i d???ng 1 m???ng, m???i khi l???y d??? li???u m???i s??? insert   
    //    userId:egov.setting.userId => Tr?????ng h???p d??? li???u ch??? c???a 1 ng?????i d??ng, ng?????i kh??c ????ng nh???p v??o c??ng 1 m??y s??? x??a d??? li???u c???a entity n??y, l???y d??? li???u m???i v???
    //                                  =>Khi ???? d??? li???u s??? l??u b???ng 1 object : {userId: Id ng?????i d??ng hi???n t???i, data: d??? li???u c???a ri??ng ng?????i d??ng n??y}
    //    option: {                               => jquery ajax option m???c ?????nh.
    //        beforeSent: function () {
    //            egov.pubsub.public(egov.events.status, {
    //                type: "processing",
    //                message: "??ang t???i"
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
                        message: "??ang t???i"
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
                        message: "??ang t???i"
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


        // #region c??y v??n b???n (trees).

        //L???y danh s??ch node con theo id node cha
        trees: {//L???y danh s??ch node con ??? node g???c
            name: "trees",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocumentTree,
            option: {},
            expriedDay: 7,
            isPrivate: true
        },

        //#endregion

        //#region C??u h???i

        getNodeQuestion: {
            name: "getNodeQuestion",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getNodeQuestion,
            option: {}
        },

        //#endregion

        //region s??? h??? s?? (storeTrees)

        getStorePrivateDocuments: {
            name: "getStorePrivateDocuments",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getStorePrivateDocuments,
            option: {}
        },

        storeTrees: {//L???y danh s??ch node con ??? node g???c
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
            NumberDayOverdue: '0',    //h???n gi???
            NumberDayAppointed: '0',  //H???n t???ng
            IsFile: 0,                 //C?? ph???i l?? file hay kh??ng(d??ng trong s??? h??? s??):M???c ?????nh l?? 0(kh??ng ph???i file)
            WorkflowId: 0,            //Quy tr??nh c???a v??n b???n
            NodeCurrentId: 0,          //Node hi???n t???i c???a v??n b???n tr??n quy tr??nh
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
            defaultSelectedText: 'T???t c???'
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
            dataUrl: '',        // Url l???y d??? li???u (n???u c??)
            param: '',          // Param cho url
            data: null,         // Danh s??ch c??c item l?? th??? hi???n c???a collection egov.models.ContextMenuItemModel
            callback: null,     // H??m th???c thi khi select tr?????c thi th???c thi h??m callback trong data
            style: {},
            position: {},
            isDatePicker: false, // ?????t true n???u mu???n th??? hi???n n???i dung x??? ra l?? datatimepicker
            // Hi???n th??? loading tr?????c r???i bind d??? li???u sau
            // v?? d???: var context = selector.contextmenu({isShowLoading: true});
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
        none: 0,         //Kh??ng hi???n th???
        unread: 1,       //V??n b???n ch??a ?????c
        unreadOnAll: 2,  //Ch??a ?????c / T???t c???
        all: 3           //T???t c???
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
            // Thi???t l???p id ????? tr??nh g??n tr??ng relation id
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
            // Set c??c tr?????ng cho autocomplete
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
            title: '',  // Ti??u ????? modal
            keyboard: false, // ???n modal khi nh??n esc
            resizable: false, // cho ph??p thay ?????i k??ch th?????c
            draggable: false, // cho ph??p k??o th??? v??? tr??
            animation: true, // hi???n th??? d???ng fadein
            remote: '', // url n???i dung modal - d??ng ????? load n???i dung modal sau theo url
            content: '', // n???i dung modal - html c?? s???n
            height: 'auto', // chi???u cao
            width: 'auto', // chi???u r???ng,
            ignoreText: '',
            buttons: [], // c??c n??t ch???c n??ng
            hide: null,   // callback sau khi ???n modal
            close: null,   // callback tr?????c khi ???n modal 
            loaded: null,   // callback sau khi load xong n???i dung = remote url.
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

    //#region G??n ra model cho c??c view. M???i view ch??? s??? d???ng m???t model t????ng ???ng.

    // C??y v??n b???n
    egov.viewModels.tree = new egov.models.TreeList();

    // Danh s??ch v??n b???n
    egov.viewModels.documentList = new egov.models.documentList();

    // Danh s??ch tab
    egov.viewModels.tabList = new egov.models.TabList();

    // Danh s??ch ng?????i nh???n tr??n form b??n giao
    egov.viewModels.actionUserList = new egov.models.actionUserList();

    //#endregion

})
(this.egov = this.egov || {});

(function (window, egov, Modernizr) {

    'use strict';

    //if (typeof Modernizr === 'undefined') {
    //    throw 'Ch??a c?? th?? vi???n Modernizr';
    //}

    //Bi???n t???m, d??ng trong tr?????ng h???p tr??nh duy???t kh??ng h??? tr??? localStorage l???n indexDb
    var TempStorage = function () {
        return this._initialize();
    };

    TempStorage.prototype._initialize = function () {
        /// <summary>
        /// Kh???i t???o
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
        /// L???p qu???n l?? d??? li???u trong localStorage.
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
        /// Kh???i t???o
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
            //Ki???m tra tr??nh duy??t c?? cho ph??p thao t??c v???i localStorage
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

    //C??c ki???u giao ti???p v???i indexedDb
    var indexedDbMode = {
        READ_WRITE: "readwrite",
        READ_ONLY: "readonly",
    };

    var IndexedDb = function () {
        /// <summary>
        /// L???p qu???n l?? d??? li???u trong localStorage.
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
        that.hasSupport = true;    //Bi???n ki???m tra c?? h??? tr??? hay kh??ng

        return that._initialize();

        // HopCV:
        // todo: Ch??? n??y qu?? d?? th???a, ???? m??? db l??n r???i trong h??m init l???i m??? ti???p
        //       m?? m???c ????ch ch??? n??y check xem tr??nh duy???t c?? h??? tr??? hay kh??ng khi m??? k???t db l??n
        ////Open tr?????c indexDb, n???u g???p l???i s??? x??c ?????nh tr??nh duy???t kh??ng support(nh?? private mode in firefox)
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
        /// Kh???i t???o
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
                //console.log("Key kh??ng t???n t???i: " + key);
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
        /// Tr??? v??? store object trong indexeddb
        /// </summary>
        /// <param name="mode">Ch??? ????? ?????c "readwrite" ho???c "readonly"</param>
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
        /// Local Cache: X??? l?? cache c???a h??? th???ng.
        /// Ch??? l??u nh???ng th??ng tin kh??ng quan tr???ng, ???????c d??ng th?????ng xuy??n d?????i client ????? gi???m t???i cho server.
        /// </summary>
        /// <param name="cacheName" type="String">
        ///     - Gi?? tr??? ch??? ?????nh s??? d???ng cache n??o: "storage","indexedDb", "fileApi".
        ///     - ????? null: t??? ?????ng ch??? ?????nh cache theo tr??nh duy???t.
        /// </param>
        this._localStorage = Modernizr.localstorage;
        this._indexeddb = Modernizr.indexeddb;
        this._fileReader = Modernizr.filereader;

        // gi?? tr??? x??c ?????nh c?? h??? tr??? Cache hay kh??ng.
        this._hasSupportCache;

        // cache data
        this._storage;

        // Gi?? tr??? x??c ?????nh dung l?????ng cache client.
        this._cacheSize;
        this._cacheName = cacheName;

        return this._initialize();
    };

    Locache.prototype._initialize = function () {

        /// <summary>
        /// Thi???t l???p ch??? ????? cache client
        /// </summary>

        // Ki???m tra tr??nh duy???t h??? tr??? l??u cache
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
                    console.log("Gi?? tr??? kh???i t???o kh??ng ????ng");
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
        /// Tr??? v??? gi?? tr??? theo key
        /// </summary>
        /// <param name="key">Key c???n l???y gi?? tr???</param>
        /// <param name="callback">H??m th???c thi sau khi l???y gi?? tr??? th??nh c??ng</param>
        /// <returns type="Object">Json object: {key: "", value: "", dateModified: ""}</returns>
        egov.callback(callback);
    }

    Locache.prototype.insert = function (key, value, callback) {
        /// <summary>
        /// Th??m m???i gi?? tr??? v??o cache
        /// </summary>
        /// <param name="key">T??n gi?? tr???</param>
        /// <param name="value">Gi?? tr???</param>
        /// <param name="callback">H??m th???c thi sau th??m gi?? tr??? th??nh c??ng</param>

        console.log('Locache ch??a ???????c kh???i t???o');
    }

    Locache.prototype.update = function (key, value, callback) {
        /// <summary>
        /// C???p nh???t gi?? tr??? v??o cache
        /// </summary>
        /// <param name="key">T??n gi?? tr???</param>
        /// <param name="value">Gi?? tr???</param>
        /// <param name="callback">H??m th???c thi sau th??m gi?? tr??? th??nh c??ng</param>
        console.log('Locache ch??a ???????c kh???i t???o');
    }

    Locache.prototype.delete = function (key, callback) {
        /// <summary>
        /// X??a gi?? tr??? kh???i cache theo key
        /// </summary>
        /// <param name="key">T??n key</param>
        /// <param name="callback">H??m th???c thi sau x??a gi?? tr??? th??nh c??ng</param>

        console.log('Locache ch??a ???????c kh???i t???o');
    }

    Locache.prototype.reset = function (callback) {
        /// <summary>
        /// X??a t???t c??? d??? li???u trong cache
        /// </summary>
        /// <param name="callback">H??m th???c thi sau x??a d??? li???u th??nh c??ng</param>

        console.log('Locache ch??a ???????c kh???i t???o');
    }

    //#endregion

    egov.CacheManager = Locache;

})(this, this.egov = this.egov || {}, this.Modernizr);
/// <reference path="../views/home/contextmenu.js" />
(function (egov, _, undefined) {
    "use strict";

    var cacheType = {
        indexedDb: "indexedDb",  //L??u tr??? v??o indexDB
        storage: "storage",     // L??u tr??? localStorage
        fileApi: "fileApi"      //L??u tr??? v??o file
    }

    var DataAccess = function () {
        /// <summary>
        /// L???p cung c???p d??? li???u cho Business:
        ///    - Qu???n l?? l??u cache d??? li???u.
        ///    - Qu???n l?? d??? li???u ??? client.
        ///    - Qu???n l?? ?????ng b??? d??? li???u v???i server;
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
        /// Tr??? v??? gi?? tr??? theo key
        /// </summary>
        /// <param name="entity">egov.entity c???n l???y gi?? tr???</param>
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
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
        //that._checkBrowsersNotSupport();

        // Tr?????ng h???p ko l??u cache l???i d?????i client th?? query l??n l???y d??? li???u tr??n server
        //Tr?????ng h???p tr??nh duy???t kh??ng m??? ???????c indexedDb(private mode firefox) th?? g???i server lu??n
        if (!entity.hasSessionCache) {
            that._sendRequestToServer(entity);
            return;
        }
        //G??n success ????? ki???m tra d??? li???u outDate ch??a
        entity.option.success = function (hasReset) {
            //Sau khi ki???m tra, g??n l???i success ban ?????u
            entity.option.success = callback;

            //N???u reset ho???c ch??a c?? d??? li???u => g???i l??n server lu??n
            if (hasReset) {
                that._sendRequestToServer(entity);
                return;
            }

            key = egov.getKeyName(entity)

            // N???u kh??ng reset
            // L???y trong database client tr?????c
            result = that._dataBase[key];
            if (result) {
                //N???u entity ch??? d??nh cho currentUser, ng?????i kh??c ????ng nh???p tr??n m??y ???? s??? b??? x??a ??i
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

            // D??? li???u ch??a t???n t???i ??? client
            // Ki???m tra n???u c?? cache th?? get t??? cache
            that._getFromCache(entity, function (data) {
                if (data) {
                    result = data.value;
                    //N???u entity ch??? d??nh cho currentUser, ng?????i kh??c ????ng nh???p tr??n m??y ???? data s??? b??? x??a ??i
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

                // N???u d??? li???u v???n ch??a c?? trong cache (ho???c kh??ng h??? tr??? Cache) th?? query l??n server ????? l???y v???
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
        /// Todo: H??m n??y m???c ????ch ch??? l???y d??? li???u ??? client ra th??i, kh??ng giao ti???p j v???i server
        /// Tr??? v??? gi?? tr??? theo key
        /// </summary>
        /// <param name="entity">egov.entity c???n l???y gi?? tr???</param>
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
                //N???u entity ch??? d??nh cho currentUser, ng?????i kh??c ????ng nh???p tr??n m??y ???? data s??? b??? x??a ??i
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
        /// Ki???m tra n???u entity ???? l??u kh??ng c???p nh???t th?? x??a ??i, kho???ng th???i gian ???????c b??o b???i bi???n this._expriedDay
        /// </summary>
        /// <param name="entity"></param>
        var that, key, callback;

        that = this;
        key = egov.getKeyName(entity);
        callback = entity.option.success;

        //HopCV
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
        //that._checkBrowsersNotSupport();

        entity.option.success = function (lastUpdate) {
            //Ki???m tra lastUpdate ???? qu?? experiedDay => X??a h???t d??? li???u trong cache
            if (lastUpdate && (Date.now() - lastUpdate > that._expriedDay * 86400000)) {
                that._dataBase[key] = null;
                that.delete(key, function () {
                    egov.callback(callback, true);
                });
                return;
            }

            //N???u lastUpdate == undefined => ch??a t???ng c?? cache => tr??? v??? true coi nh?? ???? reset ????? g???i l??n server lu??n
            egov.callback(callback, lastUpdate ? false : true);
        };

        this.getLastUpdate(entity);
    };

    DataAccess.prototype.getLastUpdate = function (entity) {
        /// <summary>
        /// Tr??? v??? th???i ??i???m ?????ng b??? g???n nh???t v???i server.
        /// </summary>
        var result,
            callback;

        if (entity == undefined) {
            egov.log("DataAccess.getLastUpdate: Entity is null.");
            return;
        }

        callback = entity.option.success;

        // Tr?????ng h???p ko l??u cache l???i d?????i client
        if (!entity.hasSessionCache) {
            return;
        }

        // Ki???m tra n???u c?? cache th?? get t??? cache
        this._getFromCache(entity, function (data) {
            egov.callback(callback, data ? data.dateModified : undefined);
        });
    }

    DataAccess.prototype.insert = function (entity, value, property) {
        /// <summary>
        /// Th??m gi?? tr??? v??o cache
        /// </summary>
        /// <param name="key">T??n gi?? tr???</param>
        /// <param name="value">Gi?? tr???</param>

        var key,
            callback,
            cacheValue,
            that;

        that = this;
        key = egov.getKeyName(entity);
        callback = entity.option.success;

        //HopCV
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
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
        /// C???p gi?? tr??? v??o cache
        /// </summary>
        /// <param name="key">T??n gi?? tr???</param>
        /// <param name="isReplace">Gi?? tr??? x??c ?????nh c?? replace to??n b??? d??? li???u c?? hay kh??ng</param>
        /// <param name="value">Gi?? tr???</param>

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
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
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
        /// X??a gi?? tr??? kh???i cache theo key
        /// </summary>
        /// <param name="key">T??n key</param>
        /// <param name="callback">H??m th???c thi sau x??a gi?? tr??? th??nh c??ng</param>

        var entity,
            that = this;

        //HopCV
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
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
        /// X??a t???t c??? d??? li???u trong cache
        /// </summary>
        /// <param name="callback">H??m th???c thi sau x??a d??? li???u th??nh c??ng</param>
        this._dataBase = {};

        //HopCV
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
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
        /// L??u cache client
        /// </summary>
        /// <param name="key" type="String">Key</param>
        /// <param name="value" type="dynamic">Gi?? tr???</param>

        //HopCV
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
        //that._checkBrowsersNotSupport();

        this._cacheManager.insert(key, value);
    }

    DataAccess.prototype._getFromCache = function (entity, callback) {
        /// <summary>
        /// Tr??? v??? gi?? tr??? t??? cache
        /// </summary>
        /// <param name="key">T??n key c???n l???y gi?? tr???</param>

        //HopCV
        //toddo; Ch??? n??y check l??m j nhi?
        // C??i set c?? h??? tr??? tr??n tr??nh duy???t hay kh??ng th?? ch??? c???n g???i tr??n th???ng kh???i t???o l?? ???????c??
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
        /// G???i request l??n server
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
            //Ki???m tra xem entity query c?? option insert v??o current cache hay kh??ng
            //True: insert b???n ghi m???i query v??o db d?????i client
            //False: th??m m???i
            if (entity.isInsertToCurrentCache) {
                that._getFromCache(entity, function (dataInDb) {
                    if (dataInDb) {
                        existDb = dataInDb.value;
                        flag = true;
                    }
                    keyInDb = JSON.stringify(entity.option.data);

                    if (!existDb) {
                        //n???u ch??a t???n t???i dataBase, th??m m???i
                        existDb = [{ key: keyInDb, value: result }];
                    } else {
                        existDb.push({ key: keyInDb, value: result });
                    }

                    that._dataBase[key] = existDb;

                    // Save to cache
                    if (entity.hasCache) {
                        //override l???i h??m success c???a entity ????? khi g???i insert ho???c update s??? t??? ?????ng callback
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

                //override l???i h??m success c???a entity ????? khi g???i insert s??? t??? ?????ng callback
                //success m???i s??? = h??m c?? k??m th??m gi?? tr??? tr??? v???.
                entity.option.success = function () {
                    egov.callback(callback, result);
                }

                // Save to cache
                if (entity.hasCache) {
                    that.insert(entity, result);
                } else {
                    //N???u entity ko l??u v??o cache, callback tr??? v??? gi?? tr???
                    egov.callback(entity.option.success);
                }
            }
        }
        requestOptions.error = function (xhr) {
            //N???u response error c?? header IsAuthenticated=false th?? logout
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
        /// L???p qu???n l?? truy c???p d??? li???u:
        ///     - X??? l?? logic d??? li???u cung c???p cho c??c Widget;
        /// </summary>

        this._dataAccess = egov.DataAccess;

        // Thi???t l???p ajax option m???c ?????nh cho to??n h??? th???ng
        this._requestDefault = {
            // B??? comment ????? thi???t l???p m???c ?????nh cho c??c option
            // beforeSent: function () { },
            // success: function (result) { },
            // error: function (xhr) {},
            // complete: function () { }
        };

        // Thi???t l???p ajax option m???c ?????nh cho to??n h??? th???ng
        this._syncDefault = {
            success: function (entity, result) {

            }

            // B??? comment ????? thi???t l???p m???c ?????nh cho c??c option
            // beforeSent: function () { },
            // error: function (xhr) { },
            // complete: function () { }
        };

        return this;
    }

    DataManager.prototype.sendRequest = function (request, options) {
        /// <summary>
        /// G???i request
        /// </summary>
        /// <param name="request">egov.entities request</param>
        /// <param name="options" type="object">jQuery ajax option</param>

        // Override ajax option theo th??? t???: m???c ?????nh cho to??n h??? th???ng <= m???c ?????nh cho t???ng request <= t??y ch???nh cua nguoi su dung.
        request.option = $.extend({}, this._requestDefault, request.option, options);
        this._dataAccess.get(request);
    }

    DataManager.prototype.getCache = function (request, options) {
        /// <summary>
        ///  HopCV 
        /// L???y d??? li???u t??? cache ra (kh??ng call l??n server)
        /// </summary>
        /// <param name="request">egov.entities request</param>
        /// <param name="options" type="object">jQuery ajax option</param>

        // Override ajax option theo th??? t???: m???c ?????nh cho to??n h??? th???ng <= m???c ?????nh cho t???ng request <= t??y ch???nh cua nguoi su dung.
        request.option = $.extend({}, this._requestDefault, request.option, options);
        this._dataAccess.getCache(request);
    }

    DataManager.prototype.getLastUpdate = function (entity, options) {
        /// <summary>
        /// Tr??? v??? th???i ??i???m ?????ng b??? g???n 
        /// </summary>
        /// <param name="entity" type="egov.entities">Entity c???n l???y lastupdate t????ng ???ng.</param>
        entity.option = $.extend({}, this._requestDefault, entity.option, options);
        this._dataAccess.getLastUpdate(entity);
    }

    DataManager.prototype.reset = function (options) {
        /// <summary>
        /// X??a h???t t???t c??? cache h??? th???ng.
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        this._dataAccess.reset(options ? options.success : undefined);
    }

    DataManager.prototype.deletePrivateCache = function (options) {
        /// <summary>
        /// X??a h???t c??c cache ch??? ng?????i d??ng hi???n t???i m???i thao tac ???????c
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

    //#region S??? d???ng chung cho to??n h??? th???ng

    DataManager.prototype.getCurrentDoctypes = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch c??c Lo???i v??n b???n ng?????i d??ng hi???n t???i ???????c ph??p kh???i t???o
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
        /// Tr??? v??? danh s??ch c??c th??? lo???i v??n b???n.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request = _entities.categories;
        this.sendRequest(request, options);
    }

    DataManager.prototype.getCurrentDepartments = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch c??c ph??ng ban hi???n t???i ng?????i d??ng thu???c v??o.
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
        /// Tr??? v??? danh s??ch c??c ph??ng ban hi???n t???i ng?????i d??ng thu???c v??o.
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
        /// Tr??? v??? danh s??ch c??c t???t c??? ph??ng ban c???a h??? th???ng.
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
        /// Tr??? v??? danh s??ch c??c t???t c??? ch???c danh c???a h??? th???ng.
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
        /// Tr??? v??? danh s??ch c??c t???t c??? ch???c v??? c???a h??? th???ng.
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
        /// Tr??? v??? danh s??ch c??c t???t c??? ng?????i d??ng trong h??? th???ng.
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
        /// Tr??? v??? danh s??ch c??c t???t c??? quan h??? ng?????i d??ng - ph??ng ban - ch???c v???.
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
        /// Tr??? v??? danh s??ch c??c ?????a ch???.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request = _entities.allAddress;
        this.sendRequest(request, options);
    }

    DataManager.prototype.clearAddress = function () {
        /// <summary>
        /// Clear danh s??ch ?????ac h???
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>
        this._dataAccess.delete("allAddress", function () {

        });
    }

    DataManager.prototype.getSendTypes = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch allSendType
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
            // X??? l?? d??? li???u tr?????c khi tr??? v??? cho Widget
            callback(result);
        }
        this.sendRequest(request, options);
    },

    DataManager.prototype.deletePrivateData = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch thi???t l???p c???a user.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request, callback;

        request = _entities.currentUserId;
        callback = options.success;
        options.success = function (result) {
            // X??? l?? d??? li???u tr?????c khi tr??? v??? cho Widget
            callback(result);
        }
        this.sendRequest(request, options);
    },

    DataManager.prototype.getUserConfig = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch thi???t l???p c???a user.
        /// </summary>
        /// <param name="options" type="Object">jQuery ajax option</param>

        var request, callback;

        request = _entities.userConfig;
        callback = options.success;
        options.success = function (result) {
            // X??? l?? d??? li???u tr?????c khi tr??? v??? cho Widget

            //L??u l???i gi?? tr??? quickview v??o cookie
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
        /// Tr??? v??? danh s??ch thi???t l???p c???a user.
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
        egov.log("Ch??a kh???i t???o data manager.");
        return;
    }

    if (egov.viewModels.tree === undefined) {
        egov.log("Ch??a kh???i t???o models.");
        return;
    }

    viewModel = egov.viewModels.tree;
    _entities = egov.entities;
    DataManager = egov.dataManager;

    DataManager.getTree = function (id, options) {
        /// <summary>
        /// Tr??? v??? danh s??ch c??y v??n b???n
        /// </summary>
        /// /// <param name="id" type="int">Id node cha. ?????t = 0 ????? l???y danh s??ch c??c node root.</param>
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
            /// X??? l?? danh s??ch c??c node l???y ra t??? database (server, cache);
            /// </summary>
            /// <param name="data">Danh s??ch t???t c??c node.</param>

            // Tr??? v??? danh s??ch node theo node cha. Tr?????ng h???p id == 0 l???y ra danh s??ch c??c node root.
            result = _.filter(data, function (node) {
                return (id === 0 && node.parentid == null) || (id !== 0 && node.parentid == id);
            });

            // Sort theo th??? t??? hi???n th???
            result = _.sortBy(result, function (node) {
                return node.order;
            });

            // Parse chu???i json config hi???n th??? c??c c???t cho danh s??ch v??n b???n ra json object.
            _.each(result, function (node) {
                node.columnSetting = egov.toJSON(node.columnSetting);
            });

            // X??? l?? sinh node n???u b??? l???c l?? k???t qu??? c??u sql.
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
        /// Tr??? v??? node tr??n c??y v??n b???n theo kho v??n b???n
        /// </summary>
        /// <param name="storeId">Id kho v??n b???n</param>
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
        /// Tr??? v??? danh s??ch c??u h???i
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var entity = _entities.getNodeQuestion;
        this.sendRequest(entity, options);
    }

    //#endregion

    //#store private

    DataManager.getStorePrivate = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch c??c h??? s?? c?? nh??n
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
        /// Tr??? v??? danh s??ch c??y v??n b???n
        /// </summary>
        /// <param name="parentId" type="int">Id node cha. ?????t = 0 ????? l???y danh s??ch c??c node root.</param>
        /// <param name="options">jQuery ajax option</param>

        var entity = $.extend({}, _entities.trees);
        parentId = parentId || 0;
        entity.option.data = { id: parentId };

        this.sendRequest(entity, options);
    }

    DataManager.updateTree = function (treeData) {
        /// <summary>
        /// C???p nh???t c??y v??n b???n ??? tr???ng th??i hi???n t???i v??o cache
        /// </summary>
        /// <param name="treeData">D??? li???u json c???a c??y v??n b???n</param>

        var entity = $.extend({}, _entities.trees);
        this._dataAccess.update(entity, treeData, true);
    }

    DataManager.updatePropInTreeModel = function (nodeId, propName, propValue) {
        /// <summary>
        /// C???p nh???t l???i gi?? tr??? theo thu???c t??nh theo id node truy???n v??o
        /// </summary>
        /// <param name="nodeId" type="int">node id truy???n v??o ????? c???p nh???t</param>
        /// <param name="propName" type="string">T??n thu???c t??nh mu???n c???p nh???t</param>
        /// <param name="propValue">Gi?? tr??? g??n v??o cho thu???c t??nh</param>

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
        /// X??? l?? sinh c??c node n???u node hi???n t???i c?? b??? l???c cho ph??p t??? sinh c??c node theo sql.
        /// </summary>
        /// <param name="nodes">Danh s??ch node</param>

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
                // Tr?????ng h???p c??u SQL kh??ng c?? k???t qu???.
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

                // S???p x???p l???i danh s??ch c??c node t??? sinh theo t??n
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
        egov.log("Ch??a kh???i t???o data manager.");
        return;
    }

    if (egov.viewModels.documentList === undefined) {
        egov.log("Ch??a kh???i t???o models.");
        return;
    }
    viewModel = egov.viewModels.documentList;
    _entities = egov.entities;
    DataManager = egov.dataManager;
    DataManager._defaultSortBy = "DateReceived";

    DataManager.getDocuments2 = function (id, filters, options) {
        /// <summary>
        /// Tr??? v??? danh s??ch v??n b???n theo kho v?? c??c b??? l???c
        /// </summary>
        /// <param name="id">Id kho v??n b???n.</param>
        /// <param name=" filters">C??c b??? l???c c???a node t????ng ???ng.
        ///         DataField: "UserCurrentId"
        ///         FilterExpression: 2
        ///         IsAutoGenNodeName: false
        ///         IsSqlValue: false
        ///         Name: "V??n b???n ch??? x??? l??"
        ///         ProcessFunctionFilterId: 1
        ///         Value: "2551"
        /// </param>
        /// <remarks>
        ///     Tr?????c khi g???i v??o h??m n??y c???n x??? l?? c??c b??? l???c c???a node c?? FilterExpression == 1 v??? h???t FilterExpression = 2.
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
        /// ?????ng b??? danh s??ch v??n b???n.
        /// </summary>
        /// <param name="storeId" type="int">Id kho v??n b???n t????ng ???ng</param>
        /// <param name=" filters">C??c b??? l???c c???a node t????ng ???ng.
        ///         DataField: "UserCurrentId"
        ///         FilterExpression: 2
        ///         IsAutoGenNodeName: false
        ///         IsSqlValue: false
        ///         Name: "V??n b???n ch??? x??? l??"
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
        /// Tr??? v??? danh s??ch v??n b???n theo kho
        /// </summary>
        /// <param name="id">Id kho v??n b???n</param>
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
            // X??? l?? nghi???p v??? business ??? ????y.

            // X??? l?? l??u mapping gi???a document copy id v???i function Store id
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
        /// Tr??? v??? danh s??ch c??c permission c???a danh s??ch v??n b???n ???????c ch???n.
        /// </summary>
        /// <param name="documentCopyIds">Danh s??ch v??n b???n c???n l???y permission</param>
        /// <param name="options">jQuery ajax option</param>

        var request,
            callback;

        request = $.extend({}, _entities.getDocumentPermission);

        request.option.data = { documentCopyIds: documentCopyIds };
        //callback = options.success;
        //options.success = function (data, lastUpdate) {
        //    // X??? l?? kho v??n b???n tr?????c khi tr??? v???.

        //    egov.callback(callback, data);
        //};

        this.sendRequest(request, options);
    }

    DataManager.getFunctionStores = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch t???t c??? FunctionStore trong h??? th???ng
        /// </summary>
        /// <param name="options" type="object">Jquery ajax option</param>

        var request,
            callback;

        // egov.entities.functionGroups
        request = _entities.functionGroups;
        callback = options.success;

        options.success = function (data) {
            // X??? l?? nghi???p v??? business ??? ????y

            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    },

    DataManager.getFunctionStoreFromDocument = function (document, options) {
        /// <summary>
        /// Tr??? v??? kho v??n b???n ch???a v??n b???n t????ng ???ng
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="document">V??n b???n</param>
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
                            // Tr??? v??? kho v??n b???n ?????u ti??n t??m ???????c
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
        /// C???p nh???t document v??o kho t????ng ???ng d???a tr??n c??c b??? l???c c???a kho.
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
        /// L???y danh s??ch v??n b???n ??? trong cache 
        /// </summary>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.documents);
        entity.option.success = options.success;
        this.getCache(entity, options);
    }

    DataManager.getDocumentsReportCache = function (options) {
        /// <summary>
        /// L???y danh s??ch v??n b???n ??? trong cache 
        /// </summary>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.documents);
        entity.option.success = options.success;
        this.getCache(entity, options);
    }

    DataManager.getDocuments = function (data, options) {
        /// <summary>
        /// L???y danh s??ch v??n b???n t??? server v??? (kh??ng l??u cache)
        /// </summary>
        /// <param name="data">C??c tham s??? khi post l??n l???y d??? li???u</param>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.tempDocuments);
        entity.option.data = data;

        this.sendRequest(entity, options);
    }

    DataManager.updateDocuments = function (documents, callback) {
        /// <summary>
        /// C???p nh???t c??y v??n b???n ??? tr???ng th??i hi???n t???i v??o cache
        /// </summary>
        /// <param name="documents">Danh s??ch v??n b???n</param>
        /// <param name="callback">H??m th???c thi g???i l???i khi th??nh c??ng</param>
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
        /// L???y danh s??ch v??n b???n t??? server v??? (kh??ng l??u cache)
        /// </summary>
        /// <param name="data">C??c tham s??? khi post l??n l???y d??? li???u</param>
        /// <param name="options">jQuery ajax option</param>
        var entity = $.extend({}, _entities.getStorePrivateDocuments);
        entity.option.data = data;

        this.sendRequest(entity, options);
    }

    //#region Private Methods

    DataManager._syncDocumentStore = function (id, lastUpdate, options) {
        /// <summary>
        /// ?????ng b??? kho v??n b???n v???i server
        /// </summary>
        /// <param name="id">Id kho v??n b???n</param>
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
            // X??? l?? cache kho v??n b???n tr?????c khi tr??? v???.
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
        /// L???c danh s??ch v??n b???n t??? kho qua c??c b??? l???c c???a node t????ng ???ng
        /// </summary>
        /// <param name="data">Danh s??ch v??n b???n t??? kho</param>
        /// <param name="filters">C??c b??? l???c t????ng ???ng</param>
        /// <param name="defaultSortBy">S???p x???p</param>
        var expressionStr,
            result,
            expression = [];
        // L???c danh s??ch v??n b???n theo c??c b??? l???c
        _.each(filters, function (filter) {
            if (filter.FilterExpression === egov.enum.processFilterType.equal) {
                expression.push("doc['" + filter.DataField + "'] == " + filter.Value);
            }
            if (filter.FilterExpression === egov.enum.processFilterType.custom) {
                expression.push(filter.Value);
            }
        });

        // Join c??c b??? l???c l???i
        expressionStr = expression.join(" && ");
        result = _.filter(data, function (doc) {
            return eval(expressionStr);
        });

        // S???p x???p m???c ?????nh theo ng??y thay ?????i
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
        egov.log("Ch??a kh???i t???o data manager.");
        return;
    }

    _entities = egov.entities;
    DataManager = egov.dataManager;

    DataManager.getDocumentTemplate = function (isCreate, docTypeId, categoryBusinessId, currentNodeId, options) {
        /// <summary>
        /// Tr??? v??? template c???u h??nh form v??n b???n
        /// </summary>
        /// <param name="isCreate">Gi?? tr??? x??c ?????nh v??n b???n l?? ??ang t???o m???i hay m???</param>
        /// <param name="docTypeId">Id lo???i v??n b???n</param>
        /// <param name="categoryBusinessId">categoryBusinessId (truy???n v??o khi m??? v??n b???n)</param>
        /// <param name="currentNodeId">Node hi???n t???i (truy???n v??o khi m??? v??n b???n)</param>

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
            // X??? l?? d??? li???u tr?????c khi tr??? v???.

            egov.callback(callback, data);
        };

        this.sendRequest(request, options);
    }

    DataManager.getDocument = function (documentCopyId, options) {
        /// <summary>
        /// Tr??? v??? v??n b???n theo id
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

        // X??c ?????nh kho v??n b???n m?? document thu???c v??o
        document_StoreEntity.option.success = function (data) {
            storeId = _.find(data, function (document_store) {
                return document_store["documentCopyId"] === documentCopyId;
            });

            if (storeId) {
                // L???y document t??? kho v??n b???n
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
                // L???y document tr??n server

            }
        };

        that._dataAccess.get(_entities.document_Store);
    }

    DataManager.getTemplateComments = function (options) {
        /// <summary>
        /// Tr??? v??? danh s??ch c??c ?? ki???n th?????ng d??ng
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
        /// Tr??? v??? danh s??ch c??c ?? ki???n th?????ng d??ng
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

                //Set l???i success
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
        /// Tr??? v??? danh s??ch c??c ?? ki???n th?????ng d??ng
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
        /// Tr??? v??? danh s??ch c??c ?? ki???n th?????ng d??ng
        /// </summary>
        /// <param name="options">jquery ajax option</param>
        var that,
            entity,
            exist,
            hasChange;

        that = this;
        entity = _entities.allCommonComments;
        //L???y danh s??ch comment ???? c??, ?????y comment m???i v??o v?? c???p nh???t l???i
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
                    //Set l???i success
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
        /// L???y c??c th??ng tin v??n b???n khi edit
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
        /// Tr??? v??? th??ng tin v??n b???n khi t???o m???i
        /// </summary>
        /// <param name="doctypeId">Lo???i v??n b???n</param>
        /// <param name="relationId">V??n b???n ????nh k??m, d??ng trong ch???c n??ng tr??? l???i</param>
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
        /// Tr??? v??? danh s??ch c??c h?????ng chuy???n khi ti???p nh???n ho???c ph??n lo???i v??n b???n
        /// </summary>
        /// <param name="doctypeId">Lo???i v??n b???n t???o m???i ho???c lo???i v??n b???n ???????c ph??n lo???i</param>
        /// <param name="hasChangingDoctype">Gi?? tr??? x??c ?????nh v??n b???n hi???n t???i ??ang ph??n lo???i hay kh??ng</param>
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
        /// Tr??? v??? danh s??ch c??c h?????ng chuy???n khi b??n giao v??n b???n
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
        /// Tr??? v??? danh s??ch ng?????i nh???n theo h?????ng chuy???n
        /// </summary>
        /// <param name="actionId">Id h?????ng chuy???n</param>
        /// <param name="workflowId">Id quy tr??nh</param>
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
        /// B??n giao v??n b???n
        /// </summary>
        /// <param name="doc">Document</param>
        /// <param name="destination">Danh s??ch ng?????i nh???n</param>
        /// <param name="files">Danh s??ch c??c file ????nh k??m l??n</param>
        /// <param name="modifiedFiles">Danh s??ch c??c file ch???nh s???a</param>
        /// <param name="removeAttachmentIds">Danh s??ch c??c file ???? x??a</param>
        /// <param name="storePrivateId">Id h??? s?? c?? nh??n</param>
        /// <param name="destinationPlan">D??? ki???n chuy???n</param>
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
        /// Tr??? v??? template c???u h??nh form v??n b???n
        /// </summary>
        /// <param name="isCreate">Gi?? tr??? x??c ?????nh v??n b???n l?? ??ang t???o m???i hay m???</param>
        /// <param name="id">DocumentCopy id khi m??? v??n b???n ho???c doctype id khi t???o m???i.</param>
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
        /// Tr??? v??? template c???u h??nh form v??n b???n
        /// </summary>
        /// <param name="isCreate">Gi?? tr??? x??c ?????nh v??n b???n l?? ??ang t???o m???i hay m???</param>
        /// <param name="id">DocumentCopy id khi m??? v??n b???n ho???c doctype id khi t???o m???i.</param>
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
    * PubSub nh?? l?? m???t h??? th???ng EventEmitter.
    * C??c Widget ????ng k?? c??c s??? ki???n publish v?? s??? ki???n ???????c s??? d???ng chung cho t???t c??? c??c subscriber kh??c.
    * 
    * Notes: s??? d???ng egov.events.js ????? qu???n l?? t??n c??c event ???????c ????ng k??.
    */
    egov.pubsub = (function () {
        var queue = [],
            that = {};

        that.publish = function (eventName, data, position) {
            /// <summary>
            /// Th???c thi c??c h??m callback ???????c li??n k???t v???i eventName
            /// </summary>
            /// <param name="eventName">T??n event c???n th???c thi</param>
            /// <param name="data">D??? li???u truy???n cho h??m callback</param>
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
                        console.log('C?? l???i x???y ra khi th???c thi m???t trong nh???ng h??m callback cho s??? ki???n "' + eventName + '"');
                        console.log('L???i ???? l??: "' + e + '"');
                    }

                    idx += 1;
                } else {
                    clearInterval(intervalId);
                }
            }, 0);
        };

        that.subscribe = function (eventName, callback, context) {
            /// <summary>
            /// ????ng k?? m???t s??? ki???n. C?? S??? ki???n ????ng k?? ti???p theo s??? lu??n ???????c th??m v??o (ch??? kh??ng overwrite).
            /// ????? h???y b??? ????ng k?? m???t s??? ki???n, s??? d???ng h??m unsubscribe.
            /// </summary>
            /// <param name="eventName">T??n s??? ki???n ????ng k??, n??n s??? d???ng d???u . ????? ph??n bi???t c??c event</param>
            /// <param name="callback">H??m th???c thi.</param>
            /// <param name="context">Context ????? th???c thi h??m callback</param>
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
            /// H???y b??? ????ng k?? s??? ki???n.
            /// </summary>
            /// <param name="eventName">T??n s??? ki???n</param>
            /// <param name="callback">H??m callback sau khi h???y b???. S??? d???ng ????? ch???c ch???n r???ng s??? ki???n ???? ???????c h???y b???.</param>
            /// <param name="context">Context th???c thi h??m callback.</param>
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

    // Qu???n l?? danh s??ch c??c s??? ki???n ???????c subscribe
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
                egov.log('Ch??a set thu???c t??nh subscribe cho option c???a status.');
            }
        },

        _create: function () {
            /// <summary>
            /// Kh???i t???o
            /// </summary>

            this.element.addClass("egov-status");

            // Thi???t l???p v??? tr?? hi???n th??? c???a status
            this.element.css({
                left: "10px",
                bottom: "0"
            });

            this._subscribeGlobalEvents();
        },

        currentStatus: null,

        _subscribeGlobalEvents: function () {
            /// <summary>
            /// ????ng k?? c??c events ra global
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
            /// Hi???n th??? m???t status
            /// </summary>
            /// <param name="status">Status: {message: text, type: priority,duration: 3, undo}</param>
            /// <param name="position">V??? tr?? hi???n th??? th??ng b??o (mobile)</param>
            var that = this,
                current = this.currentStatus,
                statusContent, statusClass;
            
            // that.element.show();

            status.priority = this._getPriority(status);

            // H???y message hi???n t???i n???u message m???i c?? priority nh??? h??n (nh??? h??n th?? ??u ti??n h??n).
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

            // Tr?????ng h???p set duration = 0 hi???n th??? message ?????n khi g???i h??m destroy ho???c c?? notify kh??c ??u ti??n h??n
            if (status.duration !== 0) {
                current.timer = setTimeout(function () {
                    that.element.fadeOut();
                    that.currentStatus = null;
                }, status.duration || this.options.duration);
            }
        },

        _importantWarningStatusSubscription: function (status) {
            /// <summary>
            /// Hi???n th??? c???nh b??o quan tr???ng
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

            // Tr?????ng h???p set duration = 0 hi???n th??? message ?????n khi g???i h??m destroy ho???c c?? notify kh??c ??u ti??n h??n
            if (status.duration !== 0) {
                status.timer = setTimeout(function () {
                    that.importantWaringElement.fadeOut();
                    that.currentStatus = null;
                }, status.duration || this.options.importantWarningDuration);
            }
        },

        _statusSuccess: function (status) {
            /// <summary>
            /// Hi???n th??? th??ng b??o x??? l?? th??nh c??ng.
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
            /// Hi???n th??? th??ng b??o x??? l?? l???i.
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
            /// Hi???n th??? th??ng b??o ??ang x??? l??
            /// </summary>
            /// <param name="message">text th??ng b??o</param>
            /// <param name="position">V??? tr?? hi???n th??? th??ng b??o</param>
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
            /// Hi???n th??? th??ng b??o c???nh b??o
            /// </summary>
            /// <param name="message">Text th??ng b??o</param>
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
            /// C??c c???nh b??o quan tr???ng ???????c hi???n th??? tr??n header c???a trang web
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
            //  - 1: Hi???n th??? tr??n danh s??ch v??n b???n
            //  - 2: Hi???n th??? tr??n chi ti???t v??n b???n
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
    //    throw 'Th?? vi???n n??y s??? d???ng jQuery, h??y t???i th?? vi???n jQuery tr?????c khi s??? d???ng';
    //}
    //if (typeof (_) === 'undefined') {
    //    throw 'Th?? vi???n n??y s??? d???ng Underscore, h??y t???i th?? vi???n Underscore tr?????c khi s??? d???ng';
    //}
    var strips =
            [
                "??????????????????????????????????????????????",
                "??????????????????????????????????????????????",
                "??",
                "??",
                "??????????????????????????????",
                "??????????????????????????????",
                "????????????",
                "????????????",
                "??????????????????????????????????????????????",
                "??????????????????????????????????????????????",
                "?????????????????????????????",
                "?????????????????????????????",
                "??????????????",
                "??????????????"
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

    //C??c h??m li??n quan ?????n treeview - jstree
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

    // C??c h??m x??? l?? chu???i
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
    //    /// Tr??? v??? ResourceValue theo Key, n???u kh??ng t???n t???i, tr??? v??? ResourceKey
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

    - C??c ????n v??? th???i gian:
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

    - C??c ?????nh d???ng format
        yy: "yy",                   // N??m: 96, 97, ... 14, 15
        yyyy: "yyyy",               // N??m ?????y ?????: 1996, 1997, ... 2014, 2015
        q: "q",                     // Qu??: 1, 2, 3, 4
        M: "M",                     // Th??ng: 1, 2, ... 12
        MM: "MM",                   // Th??ng ?????y ?????: 01, 02, ... 12
        MMM: "MMM",                 // Th??ng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Th??ng: Th??ng m???t, Th??ng hai, ... Th??ng m?????i hai
        w: "w",                     // Tu???n: 1, 2, ... 52
        ww: "ww",                   // Tu???n: 01, 02, ... 52
        d: "d",                     // Ng??y trong th??ng: 1, 2, ... 31
        dd: "dd",                   // Ng??y trong th??ng: 01, 02, ... 31
        ddd: "ddd",                 // Ng??y trong tu???n: Th??? 2, th??? 3, ... 
        h: "h",                     // Gi??? (12): 1, 2, ... 12    
        hh: "hh",                   // Gi??? (12): 01, 02, ... 12
        H: "H",                     // Gi??? (24): 1, 2, ... 23
        HH: "HH",                   // Gi??? (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Ph??t: 1, 2, ... 59
        mm: "mm",                   // Ph??t: 01, 02, ... 59
        s: "s",                     // Gi??y: 1, 2, ... 59
        ss: "ss",                   // Gi??y: 01, 02, ... 59
        sss: "sss",                 // T??ch t???c: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST

    - C??c method cho ki???u d??? li???u Date

        + parse(value, format): Chuy???n ?????i string sang datetime theo format ch??? ?????nh. M???c ?????nh h??? th???ng parse theo ISODate format.
            Date.parse("2015-07-15T16:20:04.021Z")  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            Date.parse("2015-07-15", "yyyy-MM-dd)  = Wed Jul 15 2015 09:20:04 GMT-0700 (Pacific Daylight Time)
            
            Notes: s??? c?? trong phi??n b???n 1.1

        + daysInMonth(month, year): Tr??? v??? s??? ng??y trong th??ng.
            Date.daysInMonth(7, 2015)  = 31

        + compare(date1, date2): Tr??? v??? k???t qu??? so s??nh gi???a 2 ?????i t?????ng ng??y th??ng.
            Notes: s??? c?? trong phi??n b???n 1.1

        + max([date1, date2, ...]): Tr??? v??? ?????i t?????ng ng??y th??ng l???n nh???t.
            Notes: s??? c?? trong phi??n b???n 1.1

        + min([date1, date2, ...]): Tr??? v??? ?????i t?????ng ng??y th??ng nh??? nh???t.
            Notes: s??? c?? trong phi??n b???n 1.1

    - C??c method cho ?????i t?????ng d??? li???u String

        + format(partern): Convert ng??y th??ng theo chu???i theo ?????nh d???ng y??u c???u v?? tr??? v??? k???t qu???; partern m???c ?????nh hh:mm dd/MM/yyyy (Vn_vi)
            new Date().format("dd/MM/yy") = "15/07/15"
          
        + subtract(value, unit): Tr??? ??i m???t kho???ng th???i gian theo ????n v??? th???i gian (??? tr??n) c???a ?????i t?????ng ban ?????u v?? tr??? v??? ?????i t?????ng m???i.
            new Date().subtract(1, "month").format("dd/MM/yy") = "15/06/15"

        + add(value, unit): Tr??? ??i m???t kho???ng th???i gian theo ????n v??? th???i gian (??? tr??n) c???a ?????i t?????ng ban ?????u v?? tr??? v??? ?????i t?????ng m???i.
            new Date().add(1, "month").format("dd/MM/yy") = "15/06/15"

        + month(value): L???y ho???c thi???t l???p th??ng c???a ?????i t?????ng th???i gian hi???n t???i: 1, 2, ... 12
            new Date().month() = 07
            new Date().month(12).format("dd/MM/yy") = "15/12/15";

        + year(value): T????ng t??? h??m month ??? tr??n.
            new Date().year() = 2015
            new Date().year(2017).format("dd/MM/yy") = "15/07/17";

        + date(value): L???y ho???c thi???t l???p ng??y trong th??ng c???a ?????i t?????ng th???i gian hi???n t???i: 1, 2, ... 31
            new Date().date() = 15
            new Date().date(24).format("dd/MM/yy") = "24/07/17";

        + day(): Tr??? v??? th??? t??? ng??y trong tu???n c???a ?????i t?????ng th???i gian hi???n t???i t??nh t??? ch??? nh???t: 0, 6
            new Date().day() = 03

        + weekOfYear():  Tr??? v??? th??? t??? tu???n trong n??m c???a ?????i t?????ng th???i gian hi???n t???i: 1, 2, ... 52
            new Date().weekOfYear() = 29

        + dayOfYear(): Tr??? v??? th??? t??? ng??y trong n??m c???a ?????i t?????ng th???i gian hi???n t???i: 1, 2, ... 365 (366)
            new Date().dayOfYear() = 196

        + hours(value): L???y ho???c thi???t l???p gi??? c???a ?????i t?????ng th???i gian hi???n t???i: 1, 2, ... 23
            var d = new Date(); d.setHours(15); d.format("hh:mm")    = "15:33"

        + minutes(value): T????ng t??? hours

        + seconds(value): T????ng t??? hours

        + miniSeconds(value): T????ng t??? hours

        + quarter(): Tr??? v??? qu?? c???a ?????i t?????ng th???i gian hi???n t???i
            new Date().quarter() = 3

        + endOf(): Thi???t l???p ?????i t?????ng th???i gian hi???n t???i th??nh th???i ??i???m cu???i c??ng theo ????n v??? th???i gian truy???n v??o v?? tr??? v??? ng??y m???i t????ng ???ng.
            new Date().endOf("year").format("dd/MM/yyyy")   = "31/12/2015"          ( Ng??y cu???i c??ng trong n??m )
            new Date().endOf("month").format("dd/MM/yyyy")  = "31/07/2015"          ( Ng??y cu???i c??ng trong th??ng )
            new Date().endOf("day").format()                = "23:59 15/07/2015"    ( Th???i ??i???m cu???i c??ng trong ng??y)
            ...

        + startOf(): T????ng t??? endOf
            new Date().startOf("year").format("dd/MM/yyyy")   = "01/01/2015"          ( Ng??y ?????u ti??n trong n??m )
            new Date().startOf("month").format("dd/MM/yyyy")  = "01/07/2015"          ( Ng??y ?????u ti??n trong th??ng )
            new Date().startOf("day").format()                = "00:00 15/07/2015"    ( Th???i ??i???m ?????u ti??n trong ng??y)
            ...

        + isLeapYear(): Tr??? v??? gi?? tr??? x??c ?????nh n??m hi???n t???i c?? ph???i l?? n??m nhu???n kh??ng; true = n??m nhu???n; false = kh??ng.
            new Date().subtract(1, "year").isLeapYear() = true;
*/


bt_util_date_resource = {
    vi: {
        months: ["th??ng 1", "th??ng 2", "th??ng 3", "th??ng 4", "th??ng 5", "th??ng 6", "th??ng 7", "th??ng 8", "th??ng 9", "th??ng 10", "th??ng 11", "th??ng 12"],
        shortMonths: ["Th1", "Th2", "Th3", "Th4", "Th5", "Th6", "Th7", "Th8", "Th9", "Th10", "Th11", "Th12"],
        weeks: ["ch??? nh???t", "th??? hai", "th??? ba", "th??? t??", "th??? n??m", "th??? s??u", "th??? b???y"],
        shortWeeks: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
        times: {
            future: '%s t???i',
            ago: '%s tr?????c',
            s: 'v??i gi??y',
            m: 'm???t ph??t',
            mm: '%d ph??t',
            h: 'm???t gi???',
            hh: '%d gi???',
            d: 'm???t ng??y',
            dd: '%d ng??y',
            M: 'm???t th??ng',
            MM: '%d th??ng',
            y: 'm???t n??m',
            yy: '%d n??m'
        },
        calendar: {
            sameDay: 'H??m nay',
            nextDay: 'Ng??y mai',
            lastDay: 'H??m qua',
            sameWeek: 'Tu???n n??y',
            nextWeek: 'Tu???n t???i',
            lastWeek: 'Tu???n tr?????c',
            sameMonth: 'Th??ng n??y',
            lastMonth: 'Th??ng tr?????c'
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
        yy: "yy",                   // N??m: 96, 97, ... 14, 15
        yyyy: "yyyy",               // N??m: 1996, 1997, ... 2014, 2015
        q: "q",                     // Qu??: 1, 2, 3, 4
        M: "M",                     // Th??ng: 1, 2, ... 12
        MM: "MM",                   // Th??ng: 01, 02, ... 12
        MMM: "MMM",                 // Th??ng: Th1, Th2, ... Th12
        MMMM: "MMMM",               // Th??ng: Th??ng m???t, Th??ng hai, ... Th??ng m?????i hai
        w: "w",                     // Tu???n: 1, 2, ... 52
        ww: "ww",                   // Tu???n: 01, 02, ... 52
        d: "d",                     // Ng??y trong th??ng: 1, 2, ... 31
        dd: "dd",                   // Ng??y trong th??ng: 01, 02, ... 31
        ddd: "ddd",                 // Ng??y trong tu???n: Th??? 2, th??? 3, ... 
        h: "h",                     // Gi??? (12): 1, 2, ... 12    
        hh: "hh",                   // Gi??? (12): 01, 02, ... 12
        H: "H",                     // Gi??? (24): 1, 2, ... 23
        HH: "HH",                   // Gi??? (24): 01, 02, ... 23
        a: "a",                     // Am/pm: am/pm
        A: "A",                     // Am/pm: AM/PM
        m: "m",                     // Ph??t: 1, 2, ... 59
        mm: "mm",                   // Ph??t: 01, 02, ... 59
        s: "s",                     // Gi??y: 1, 2, ... 59
        ss: "ss",                   // Gi??y: 01, 02, ... 59
        sss: "sss",                 // T??ch t???c: 1, 2, ... 999
        z: "z",                     // Timezone: EST CST ... MST PST 
    };
    _resource = bt_util_date_resource["vi"];

    //#region Prototype Methods

    _prototype.format = function Date$format(partern) {
        /// <summary>
        /// Convert ng??y th??ng th??nh chu???i theo ?????nh d???ng y??u c???u
        /// </summary>
        /// <param name="partern">?????nh d???ng chu???i c???n xu???t ra.</param>
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
        /// Tr??? ??i m???t kho???ng th???i gian v?? tr??? v??? kho???ng th???i gian m???i t??? th???i gian ban ?????u.
        /// </summary>
        /// <param name="value">Gi?? tr??? tr???</param>
        /// <param name="unit">Tr?????ng th???i gian c???n tr???</param>
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
        /// Th??m m???t kho???ng th???i gian v??o th???i gian ban ?????u v?? tr??? v??? th???i gian m???i
        /// </summary>
        /// <param name="value">Kho???ng th???i gian c???n th??m</param>
        /// <param name="unit">Tr?????ng th???i gian c???n th??m</param>
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
        /// L???y ho???c thi???t l???p th??ng c???a ng??y n??y: 1,2 ... 12
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
        /// L???y ho???c thi???t l???p n??m c???a ng??y n??y
        /// </summary>
        if (hasValue(value)) {
            this.setFullYear(value);
            return this;
        }

        return this.getFullYear();
    }

    _prototype.date = function Date$date(value) {
        /// <summary>
        /// L???y ho???c thi???t l???p ng??y trong th??ng c???a ng??y n??y
        /// </summary>
        /// <param name="value" type="int">Ng??y c???n set, g??n null ????? l???y gi?? tr???.</param>

        if (hasValue(value)) {
            this.setDate(value);
            return this;
        }

        return this.getDate();
    }

    _prototype.day = function Date$day() {
        /// <summary>
        /// Tr??? v??? ng??y trong tu???n c???a ng??y n??y
        /// </summary>

        return this.getDay();
    }

    _prototype.weekOfYear = function Date$week() {
        /// <summary>
        /// Tr??? v??? tu???n trong n??m c???a ng??y n??y
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - firstDay) / 86400000) + firstDay.day() - 1) / 7);
    }

    _prototype.dayOfYear = function Date$dayOfYear() {
        /// <summary>
        /// Tr??? v??? ng??y trong n??m c???a ng??y n??y, v?? d??? ng??y th??? 234 c???a 365
        /// </summary>
        var firstDay = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((this - firstDay) / 86400000);
    }

    _prototype.hours = function Date$hours(value) {
        /// <summary>
        /// L???y ho???c thi???t l???p gi??? c???a ng??y n??y
        /// </summary>

        if (hasValue(value)) {
            this.setHours(value);
            return this;
        }

        return this.getHours();
    }

    _prototype.minutes = function Date$minutes(value) {
        /// <summary>
        /// L???y ho???c thi???t l???p ph??t c???a ng??y n??y
        /// </summary>

        if (hasValue(value)) {
            this.setMinutes(value);
            return this;
        }

        return this.getMinutes();
    }

    _prototype.seconds = function Date$seconds(value) {
        /// <summary>
        /// L???y ho???c thi???t l???p gi??y c???a ng??y n??y
        /// </summary>
        if (hasValue(value)) {
            this.setSeconds(value);
            return this;
        }

        return this.getSeconds();
    }

    _prototype.miniSeconds = function Date$miniSeconds(value) {
        /// <summary>
        /// L???y ho???c thi???t l???p mini gi??y c???a ng??y n??y
        /// </summary>

        if (hasValue(value)) {
            this.setMilliseconds(value);
            return this;
        }

        return this.getMilliseconds();
    }

    _prototype.quarter = function Date$quarter() {
        /// <summary>
        /// Tr??? v??? qu?? c???a ng??y n??y
        /// </summary>
        return Math.floor(((this.month() - 1) / 3) + 1);
    }

    _prototype.endOf = function Date$endOf(unit) {
        /// <summary>
        /// Thi???t l???p ng??y hi???n t???i th??nh th???i ??i???m cu???i c??ng c???a ????n v??? truy???n v??o v?? tr??? v??? ng??y m???i t????ng ???ng.
        /// </summary>
        /// <param name="unit">
        /// ????n v???:
        /// - "year": thi???t l???p ng??y hi???n t???i v??? ng??y cu???i c??ng trong n??m v???i ng??y hi???n t???i.
        /// - "quarter": thi???t l???p ng??y hi???n t???i v??? ng??y cu???i c??ng c???a qu?? v???i ng??y hi???n t???i
        /// - "month": thi???t l???p ng??y hi???n t???i v??? ng??y cu???i c??ng trong th??ng v???i ng??y hi???n t???i.
        /// - "week": thi???t l???p ng??y hi???n t???i v??? ng??y cu???i c??ng trong tu???n v???i ng??y hi???n t???i.
        /// - "day": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 23:59:59 c???a ng??y hi???n t???i.
        /// - "hour": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 59:59 c???a gi??? hi???n t???i.
        /// - "minute": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 59s c???a ph??t hi???n t???i.
        /// - "second": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 999ms c???a gi??y hi???n t???i.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "????n v??? truy???n v??o kh??ng h???p l???.";
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
        /// Thi???t l???p ng??y hi???n t???i th??nh th???i ??i???m ?????u ti??n c???a ????n v??? truy???n v??o v?? tr??? v??? ng??y m???i t????ng ???ng.
        /// </summary>
        /// <param name="unit">
        /// ????n v???:
        /// - "year": thi???t l???p ng??y hi???n t???i v??? ng??y ?????u ti??n trong n??m v???i ng??y hi???n t???i.
        /// - "quarter": thi???t l???p ng??y hi???n t???i v??? ng??y ?????u ti??n c???a qu?? v???i ng??y hi???n t???i
        /// - "month": thi???t l???p ng??y hi???n t???i v??? ng??y ?????u ti??n trong th??ng v???i ng??y hi???n t???i.
        /// - "week": thi???t l???p ng??y hi???n t???i v??? ng??y ?????u ti??n trong tu???n v???i ng??y hi???n t???i.
        /// - "day": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 12:00:00 am c???a ng??y hi???n t???i.
        /// - "hour": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 00:00 c???a gi??? hi???n t???i.
        /// - "minute": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 00s c???a ph??t hi???n t???i.
        /// - "second": thi???t l???p ng??y hi???n t???i v??? th???i ??i???m 000ms c???a gi??y hi???n t???i.
        /// </param>
        if (_unitType[unit] === undefined) {
            throw "????n v??? truy???n v??o kh??ng h???p l???.";
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
        /// Tr??? v??? gi?? tr??? x??c ?????nh n??m hi???n t???i c?? ph???i l?? n??m nhu???n kh??ng.
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
            return interval + " n??m";
        }
        interval = Math.floor(seconds / 2592000);
        if (interval > 1) {
            return interval + " Thg";
        }
        interval = Math.floor(seconds / 604800);
        if (interval > 1) {
            return interval + " tu???n";
        }
        interval = Math.floor(seconds / 86400);
        if (interval > 1) {
            return interval + " ng??y";
        }
        interval = Math.floor(seconds / 3600);
        if (interval > 1) {
            return interval + " gi???";
        }
        interval = Math.floor(seconds / 60);
        if (interval > 1) {
            return interval + " ph??t";
        }
        return "V???a m???i";
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
            return date.format("[Th??ng] MM");

        return date.format("[Th??ng] MM - yyyy");
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
        /// Chuy???n ?????i string th??nh ?????nh d???ng ng??y th??ng theo format ch??? ?????nh
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
        /// Tr??? v??? s??? ng??y trong th??ng
        /// </summary>
        /// <param name="month">Th??ng</param>
        /// <param name="year">N??m</param>
        return new Date(year, month, 0).date();
    }

    _date.compare = function Date$compare(date1, date2) {
        /// <summary>
        /// So s??nh 2 ng??y th??ng v?? tr??? v??? k???t qu??? so s??nh nh??? h??n, b???ng, l???n h??n.
        /// </summary>
        /// <param name="date1">Ng??y 1</param>
        /// <param name="date2">Ng??y 2</param>
        /// <returns type="int">1-ng??y 1 l???n h??n ng??y 2; 0-ng??y 1 b???ng ng??y 2; -1-ng??y 1 nh??? h??n ng??y 2</returns>
    }

    _date.max = function () {
        /// <summary>
        /// Tr??? v??? ng??y l???n nh???t c???a ki???u d??? li???u Date
        /// </summary>
    }

    _date.min = function () {
        /// <summary>
        /// Tr??? v??? ng??y nh??? nh???t c???a ki???u d??? li???u Date
        /// </summary>
    }

    _date.now = function () {
        return new Date;
    }

    //#endregion

    //#region Private Methods

    var hasValue = function (value) {
        /// <summary>
        /// Tr??? v??? k???t qu??? ki???m tra gi?? tr??? ph???i l?? null hay kh??ng.
        /// </summary>
        /// <param name="value">Gi?? tr???</param>
        return value !== undefined && value !== null;
    };

    var _isObject = function (obj) {
        /// <summary>
        /// Tr??? v??? k???t qu??? ki???m tra gi?? tr??? c?? ph???i l?? m???t object kh??ng
        /// </summary>
        /// <param name="obj"></param>
        return typeof obj === "object";
    };

    var _isNumber = function (val) {
        /// <summary>
        /// Tr??? v??? k???t qu??? ki???m tra gi?? tr??? hi???n t???i c?? ph???i l?? 1 number hay kh??ng.
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

    - C??c method cho ki???u d??? li???u String

        + format:               String.format("T??i l?? {0}", "TienBV")   => return: "T??i l?? TienBV"

        + isNullOrEmpty:        String.isNullOrEmpty("")                => return: true

        + isNullOrWhiteSpace:   String.isNullOrWhiteSpace("   ")        => return: true

        + isString:             String.isString("TienBV")               => return: true

    - C??c method cho ?????i t?????ng d??? li???u String

        + trim, trimStart, trimEnd: c???t c??c k?? t??? tr???ng ??? 2 ?????u chu???i, ho???c ?????u chu???i, ho???c cu???i chu???i
            "   Bkav v?? ?????i   ".trim()        = "Bkav v?? ?????i"; 
            "   Bkav v?? ?????i   ".trimStart()   = "Bkav v?? ?????i   "; 
            "   Bkav v?? ?????i   ".trimEnd()     = "   Bkav v?? ?????i";

        + startWith(prefix, ignoreCase): Tr??? v??? gi?? tr??? x??c ?????nh chu???i c???n ki???m tra c?? ph???i n???m ??? ?????u chu???i hi???n t???i kh??ng.
            "Bkav v?? ?????i".startWith("Bkav")         = true;  
            "Bkav v?? ?????i".startWith("bKaV", true)   = true;

        + endWith(prefix, ignoreCase)      => t????ng t??? startWith

        + equals(value, ignoreCase): Tr??? v??? gi?? tr??? x??c ?????nh chu???i cung c???p c?? b???ng v???i chu???i hi???n t???i kh??ng.
            "Bkav v?? ?????i".equals("Bkav v?? ?????i")         = true; 
            "Bkav v?? ?????i".equals("Bkav V?? ?????i", true)   = true

        + remove(startIndex, count): Tr??? v??? chu???i sau khi ???? x??a nh???ng k?? t??? ??? c??c v??? tr?? truy???n v??o.
            "Bkav v?? ?????i".remove(4)             = "Bkav";
            "Bkav v?? ?????i".remove(0, 5)          = "v?? ?????i";

        + replaceAll(searchValue, replaceValue): Tr??? v??? chu???i sau khi thay th??? c??c chu???i c?? ???????c ch??? ?????nh b???ng chu???i m???i.
            "Bkav v?? ?????i".replaceAll("v", "i")  = "Bkai i?? ?????i";

        + contains(value): Tr??? v??? gi?? tr??? x??c ?????nh chu???i c???n ki???m tra c?? thu???c chu???i hi???n t???i kh??ng.
            "Bkav v?? ?????i".contains("v??")        = true;

        + toCharArray():  Tr??? v??? m???t m???ng t???t c??? c??c k?? t??? trong chu???i
            "Bkav".toCharArray()        = ["B", "k", "a", "v"]

        + reverse(): ?????o chu???i
            "Bkav".reverse()            = "vakB";

        + removeVietnamChars(): lo???i b??? d???u ti???ng vi???t
            "Bkav v?? ?????i".removeVietnamChars()      = "Bkav vo doi";

*/

(function () { var t = String, n = t.prototype, i = ["a??????????????????????????????????????????????", "A??????????????????????????????????????????????", "d??", "D??", "e??????????????????????????????", "E??????????????????????????????", "i????????????", "I????????????", "o??????????????????????????????????????????????", "O??????????????????????????????????????????????", "u?????????????????????????????", "U?????????????????????????????", "y??????????????", "Y??????????????"]; n.trim = function () { return this.replace(/^\s+|\s+$/g, "") }; n.trimStart = function () { return this.replace(/^\s+/, "") }; n.trimEnd = function () { return this.replace(/\s+$/, "") }; n.startWith = function (n, t) { if (!String.isString(n)) throw "Gi?? tr??? truy???n v??o kh??ng h???p l???"; return t = t || !1, n.equals(this.substr(0, n.length), t) }; n.endWith = function (n, t) { if (!String.isString(n)) throw "Gi?? tr??? truy???n v??o kh??ng h???p l???"; return t = t || !1, n.equals(this.substr(this.length - n.length), t) }; n.equals = function (n, t) { if (!String.isString(n)) throw "Gi?? tr??? truy???n v??o kh??ng h???p l???"; return t = t || !1, t ? this.toLowerCase() === n.toLowerCase() : this.toString() === n }; n.remove = function (n, t) { var i; if (typeof n != "number") throw "Start Index ph???i l?? m???t ch??? s???"; return i = this.slice(0, n), t != undefined && typeof t == "number" && (i += this.slice(n + t)), i }; n.replaceAll = function (n, t) { if (!String.isString(n) || !String.isString(t)) throw "C??c tham s??? truy???n v??o ph???i l?? string"; var i = new RegExp(n.replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&"), "g"); return this.replace(i, t) }; n.contains = function (n) { if (!String.isString(n)) throw "Tham s??? truy???n v??o ph???i l?? chu???i"; return this.indexOf(n) > -1 }; n.toCharArray = function () { return this.split("") }; n.reverse = function () { return this.split("").reverse().join("") }; n.forEach = function (n) { if (typeof n == "function") for (var i = this.length, t = 0; t < i; t++) n(this.charAt(t), t) }; n.removeVietnamChars = function () { var t = [], r = this, n; return r.forEach(function (r, u) { for (t[u] = r, n = 0; n < i.length; n++) if (i[n].contains(r)) { t[u] = i[n][0]; break } }), t.join("") }; t.format = function () { return String._toFormattedString(!1, arguments) }; t.isNullOrEmpty = function (n) { return n === null || n === undefined || n === "" }; t.isNUllOrWhiteSpace = function (n) { return n = n.trim(), String.isNullOrEmpty(n) }; t.isString = function (n) { return typeof n == "string" }; t._toFormattedString = function (n, t) { for (var o, u, c, r, e = "", f = t[0], i = 0; ;) { if (o = f.indexOf("{", i), u = f.indexOf("}", i), o < 0 && u < 0) { e += f.slice(i); break } if (u > 0 && (u < o || o < 0)) { if (f.charAt(u + 1) !== "}") throw new Error("format stringFormatBraceMismatch"); e += f.slice(i, u + 1); i = u + 2; continue } if (e += f.slice(i, o), i = o + 1, f.charAt(i) === "{") { e += "{"; i++; continue } if (u < 0) throw new Error("format stringFormatBraceMismatch"); var s = f.substring(i, u), h = s.indexOf(":"), l = parseInt(h < 0 ? s : s.substring(0, h), 10) + 1; if (isNaN(l)) throw new Error("format stringFormatInvalid"); c = h < 0 ? "" : s.substring(h + 1); r = t[l]; (typeof r == "undefined" || r === null) && (r = ""); e += r.toFormattedString ? r.toFormattedString(c) : n && r.localeFormat ? r.localeFormat(c) : r.format ? r.format(c) : r.toString(); i = u + 1 } return e } })(this);


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

    // Danh s??ch c??c cookie name
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

    /// <summary>L???p qu???n l?? t???t c??? c??c cookie</summary>
    var Cookie = {

        // L??u tab ??ang m??? v??o cookie
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

        // Tr??? v??? danh s??ch t???t c??? c??c tab ???? l??u v??o cookie
        getRecentTab: function (user) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (recentTab) {
                return recentTab[user];
            }
            return [];
        },

        // X??a m???t tab kh???i cookie
        deleteRecentTab: function (user, tabModel) {
            var recentTab = this.getCookie(cookies.RecentTabs);
            if (recentTab) {
                recentTab[user] = _.reject(recentTab[user], function (itm) {
                    return itm.id === tabModel.id;
                });

                $.cookie(cookies.RecentTabs, JSON.stringify(recentTab), { secure: true });
            }
        },

        // L??u th??ng tin c??c c???t v?? ????? r???ng c??c c???t v??o cookie
        addDocumentHeaderWidth: function (functionId, value) {
            $.cookie(cookies.DocumentHeaderWidth + functionId, JSON.stringify(value), { expires: 7, path: "/", secure: true });
        },

        // L???y ra danh s??ch c??c c???t v?? ????? r???ng ???? ???????c l??u cookie
        getDocumentHeaderWidth: function (functionId) {
            return this.getCookie(cookies.DocumentHeaderWidth + functionId);
        },

        // Th??m cookie pagesize
        /// <param name="pageSize" type="int">PageSize</param>
        addPageSize: function (pageSize) {
            if (pageSize && typeof pageSize === 'number') {
                $.cookie(cookies.PageSize, pageSize, { expires: 7, path: "/", secure: true });
            }
        },

        // Tr??? v??? cookie pageSize
        getPageSize: function () {
            var value = $.cookie(cookies.PageSize);
            return parseInt(value);
        },

        // private: L???y ra cookie theo t??n
        getCookie: function (name) {
            var value = $.cookie(name);
            return JSON.parse(value);
        },

        // Thi???t l???p b??? qua confirm v??n b???n li??n quan khi b??n giao
        setIgnoreConfirmRelation: function (value) {
            $.cookie(cookies.IgnoreConfirmRelations, value, { secure: true });
        },

        // Tr??? v??? gi?? tr??? thi???t l???p confirm v??n b???n li??n quan
        getIgnoreConfirmRelation: function () {
            var value = $.cookie(cookies.IgnoreConfirmRelations);
            return value;
        },

        // K??ch th?????c trang ch???
        viewSize: function (value) {
            if (value === undefined) {
                return parseInt($.cookie(cookies.viewSize));
            } else {
                $.cookie(cookies.viewSize, value, { expires: 30, secure: true });
            }
        },

        ///Thi???t l???p hi???n th??? t??m t???t v??n b???n
        setQuickView: function (value) {
            $.cookie(cookies.quickView, value, { secure: true });
        },

        ///L???y thi???t l???p hi???n th??? t??m t???t v??n b???n
        getQuickView: function () {
            return parseInt($.cookie(cookies.quickView));
        },

        ///Thi???t l???p ???ng d???ng ch???y sau c??ng
        setLastApp: function (value) {
            $.cookie(cookies.lastApp, value, { secure: true });
        },

        ///L???y thi???t l???p ???ng d???ng ch???y sau c??ng
        getLastApp: function () {
            return $.cookie(cookies.lastApp);
        },

        ///Thi???t l???p g?? ti???ng vi???t
        setMudimMethod: function (value) {
            $.cookie(cookies.mudimMethod, value, { path: '/', secure: true });
        },

        //L???y thi???t l???p g?? ti???ng vi???t
        getMudimMethod: function () {
            return parseInt($.cookie(cookies.mudimMethod));
        },

        ///L???y thi???t l???p g?? ti???ng vi???t
        getUseVietKey: function () {
            return parseInt($.cookie(cookies.useVietKey));
        },

        //l???y ho???c thi???t l???p tr???ng th??i c?? hi???n th??? popup cho ?? ki???n khi chuy???n theo l??
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

    // Danh s??ch c??c key trong localStorage
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

    /// <summary>L???p qu???n l?? t???t c??? c??c localStorage</summary>
    var Storage = {

        // L??u tab ??ang m??? v??o localStorage
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

        // Tr??? v??? danh s??ch t???t c??? c??c tab ???? l??u v??o localStorage
        getRecentTab: function (user) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (recentTab) {
                return recentTab[user];
            }

            return [];
        },

        // X??a m???t tab kh???i localStorage
        deleteRecentTab: function (user, tabModel) {
            var recentTab = this.getLocalStorage(localStorageKey.RecentTabs);
            if (recentTab) {
                recentTab[user] = _.reject(recentTab[user], function (itm) {
                    return itm.id === tabModel.id;
                });

                egov.locache.setDefault(localStorageKey.RecentTabs, JSON.stringify(recentTab));
            }
        },

        // L??u th??ng tin c??c c???t v?? ????? r???ng c??c c???t v??o localStorage
        addDocumentHeaderWidth: function (functionId, value) {
            egov.locache.setDefault(localStorageKey.DocumentHeaderWidth + functionId, JSON.stringify(value));
        },

        // L???y ra danh s??ch c??c c???t v?? ????? r???ng ???? ???????c l??u localStorage
        getDocumentHeaderWidth: function (functionId) {
            return this.getLocalStorage(localStorageKey.DocumentHeaderWidth + functionId);
        },

        // Th??m localStorage pagesize
        /// <param name="pageSize" type="int">PageSize</param>
        addPageSize: function (pageSize) {
            if (pageSize && typeof pageSize === 'number') {
                egov.locache.setDefault(localStorageKey.PageSize, pageSize);
            }
        },

        // Tr??? v??? localStorage pageSize
        getPageSize: function () {
            var value = egov.locache.getDefault(localStorageKey.PageSize);
            return parseInt(value);
        },

        // private: L???y ra localStorage theo t??n
        getLocalStorage: function (name) {
            var value = egov.locache.getDefault(name);
            return JSON.parse(value);
        },

        // Thi???t l???p b??? qua confirm v??n b???n li??n quan khi b??n giao
        setIgnoreConfirmRelation: function (value) {
            egov.locache.setDefault(localStorageKey.IgnoreConfirmRelations, value);
        },

        // Tr??? v??? gi?? tr??? thi???t l???p confirm v??n b???n li??n quan
        getIgnoreConfirmRelation: function () {
            var value = egov.locache.getDefault(localStorageKey.IgnoreConfirmRelations);
            return value;
        },

        // K??ch th?????c trang ch???
        viewSize: function (value) {
            if (value === undefined) {
                return parseInt(egov.locache.getDefault(localStorageKey.viewSize));
            } else {
                egov.locache.setDefault(localStorageKey.viewSize, value);
            }
        },

        ///Thi???t l???p hi???n th??? t??m t???t v??n b???n
        setQuickView: function (value) {
            egov.locache.setDefault(localStorageKey.quickView, value);
        },

        ///L???y thi???t l???p hi???n th??? t??m t???t v??n b???n
        getQuickView: function () {
            var value = parseInt(egov.locache.getDefault(localStorageKey.quickView));
            return value;
        },

        ///Thi???t l???p ???ng d???ng ch???y sau c??ng
        setLastApp: function (value) {
            egov.locache.setDefault(localStorageKey.lastApp, value);
        },

        ///L???y thi???t l???p ???ng d???ng ch???y sau c??ng
        getLastApp: function () {
            return egov.locache.getDefault(localStorageKey.lastApp);
        },

        ///Thi???t l???p g?? ti???ng vi???t
        setMudimMethod: function (value) {
            egov.locache.setDefault(localStorageKey.mudimMethod, value);
        },

        //L???y thi???t l???p g?? ti???ng vi???t
        getMudimMethod: function () {
            return parseInt(egov.locache.getDefault(localStorageKey.mudimMethod));
        },

        ///L???y thi???t l???p g?? ti???ng vi???t
        getUseVietKey: function () {
            return parseInt(egov.locache.getDefault(localStorageKey.useVietKey));
        },

        //l???y ho???c thi???t l???p tr???ng th??i c?? hi???n th??? popup cho ?? ki???n khi chuy???n theo l??
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

    /// <summary>?????i t?????ng View qu???n l?? th??ng b??o c???a eGov</summary>
    var eGovMessage = Backbone.View.extend({

        el: '.alert',

        // Kh???i t???o
        initialize: function () {
            this.messageButtons = messageButtons;
            this.messageResult = messageResult;
            this.messageTypes = messageTypes;
            this.autoHide = false;
        },

        // Hi???n th??? th??ng b??o d???ng alert
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
            /// Hi???n th??? Prompt gi???ng nh?? javascript
            /// </summary>
            /// <param name="title">Ti??u ?????</param>
            /// <param name="callback">H??m th???c thi sau khi comfirm.</param>
            /// <param name="isCheckNullOrEmpty">C?? ki???m tra n???u l?? null ho???c r???ng th?? validate</param>
            /// <param name="valueDefault">Gi?? tr??? m???c ?????nh khi b???t dialog</param>
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
            /// Hi???n th??? Prompt gi???ng nh?? javascript
            /// </summary>
            /// <param name="title">Ti??u ?????</param>
            /// <param name="callback">H??m th???c thi sau khi comfirm.</param>
            /// <param name="isCheckNullOrEmpty">C?? ki???m tra n???u l?? null ho???c r???ng th?? validate</param>

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

        // Hi???n th??? th??ng b??o d???ng notify
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

        /// <summary>Hi???n th??? message ??ang x??? l??</summary>
        processing: function (message, autoHide) {
            this.notification(message, this.messageTypes.processing, autoHide);
        },

        /// <summary>Hi???n th??? message th??ng b??o l???i</summary>
        error: function (message, autoHide) {
            this.notification(message, this.messageTypes.error, autoHide);
        },

        /// <summary>Hi???n th??? message th??ng b??o th??nh c??ng</summary>
        success: function (message, autoHide) {
            this.notification(message, this.messageTypes.success, autoHide);
        },

        /// <summary>Hi???n th??? message c???nh b??o</summary>
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
            title: 'Th??ng b??o',
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
        throw 'Th?? vi???n n??y s??? d???ng jQuery, h??y t???i th?? vi???n jQuery tr?????c khi s??? d???ng';
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
        /// Ki???m tra tr??nh duy???t c?? h??? tr??? localStorage hay kh??ng?
        /// </summary>
        if ('localStorage' in window && window['localStorage'] !== null) {
            //Ki???m tra tr??nh duy??t c?? cho ph??p thao t??c v???i localStorage
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

            //T???o header cho table header
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
                $gridContent.css({ 'overflow-y': 'auto' });//b??? min-height do b???n desktop khi rezisie kh??ng nh???n scroll ngang
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

            // resize chi???u r???ng c??c c???t
            if (settings.isResizeColumn) {

                // $gridContent.css({ 'overflow-x': 'auto' });
                var uniqueKey = i + "_" + $elQ.attr('id') + "_" + window.location.pathname;
                var columns = $tableHeader.find("thead tr:first th");

                //B???t c??c s??? ki???n ????? khi scroll lesft th?? c??c c???t tr??n table header c??ng thay ?????i v??? tr?? theo
                $gridContent.bind('mousedown, mouseup, mousemove', function () {
                    var value = $(this).scrollLeft();
                    $gridHeaderWrap.css({ "margin-left": -value })
                });

                //L???y thi???t l???p c??c c???t
                var widthColumn = null;
                if (settings.isUseCookie) {
                    if (_hasSupportLocalStorage()) {
                        widthColumn = window.localStorage.getItem(uniqueKey);
                    }
                    else {
                        widthColumn = $.cookie(uniqueKey);
                    }
                }

                //Bind l???i c??c thi???t l???p tr?????c ?????y cho table
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

        //X??a to??n b??? c??c h??ng ???????c ch???n
        var tableRemoveAllSelected = function () {
            $.each(tableRows, function () {
                $(this).removeClass(defaultOptions.clickedClass);
            });
        };

        $.each(tableRows, function () {
            var _this = $(this);
            _this.children('td').attr('class', '');
            _this.children('td').attr('unselectable', 'on');

            //B???t s??? ki??n click chu???t tr??i
            _this.on('click', function (ev) {
                var that = $(this);
                //ph??m shift
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
                    //ph??m ctrl
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

            //B???t s??? ki??n click chu???t ph???i
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
            /// M??? c???a s??? popup
            /// </summary>
            /// <param name="url">url</param>
            /// <param name="title">title</param>
            /// <param name="inputWidth">chi???u r???ng popup, n???u kh??ng truy???n s??? l???y theo c???u h??nh c???a user</param>
            /// <param name="inputHeight">chi???u cao popup, n???u kh??ng truy???n s??? l???y theo c???u h??nh c???a user</param>
            var width,
                height;

            width = inputWidth ? inputWidth : egov.setting && egov.setting.userSetting && egov.setting.userSetting.PopUpWidth ? egov.setting.userSetting.PopUpWidth : 900;
            height = inputHeight ? inputWidth : egov.setting && egov.setting.userSetting && egov.setting.userSetting.PopUpHeight ? egov.setting.userSetting.PopUpHeight : 575;
            window.open(url, title, "width=" + width + ",height=" + height);
        },

        autoSaveSize: function () {
            /// <summary>
            /// T??? ?????ng l??u k??ch c??? popup khi ng?????i d??ng resize
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

        /// <summary>L???p x??? l?? table</summary>
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

            /// <summary>Kh???i t???o</summary>
            initialize: function (option) {
                this.$el = option.el;
                this.resizable = option.resizable;
                this.scrollable = option.scrollable;
                this.render();
            },

            /// <summary>X??? l?? c??c s??? ki???n</summary>
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

                // N???u nh???n n??t Shift
                if (e.shiftKey) {
                    // L???y row c?? index nh??? nh???t trong s??? c??c row ???? selected
                    var fromIdx = this.$el.find('.' + this.selectedClass).first().index();

                    // L???y index c???a row hi???n t???i
                    var toIdx = row.index();
                    var allRow = this.$el.find('tr');

                    // X??a h???t c??c selected
                    this.removeAllSelected();

                    // G??n l???i selected cho c??c row trong v??ng ???????c ch???n
                    for (var i = fromIdx; i <= toIdx; i++) {
                        var rowItm = allRow.eq(i);
                        rowItm.addClass(this.selectedClass);
                        this.select(rowItm);
                    }
                }
                else if (isMultiSelect) { // N???u nh???n Ctrl ho???c click v??o checkbox
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

            /// <summary>Select row hi???n t???i</summary>
            select: function (row) {
                row.addClass(this.selectedClass);
                row.find('.checkbox, [type="checkbox"]').addClass('checked').prop('checked', true);
            },

            /// <summary>Select t???t c??? c??c row trong table</summary>
            selectAll: function () {
                this.$el.find('tr').addClass(this.selectedClass);
                this.$('tr .checkbox, tr [type="checkbox"]').addClass('checked').prop('checked', true);
            },

            /// <summary>Select, unselect t???t c???</summary>
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

            /// <summary>Remove t???t c??? c??c selected</summary>
            removeAllSelected: function () {
                this.$('tr').removeClass(this.selectedClass);
                this.$('tr .checkbox, tr [type="checkbox"]').removeClass('checked').prop('checked', false);
            },

            /// <summary>Cho ph??p k??o th??? c??c c???t trong table</summary>
            renderResizable: function () {
                this.$('th').resizable();
            },

            /// <summary>Cho ph??p scoll ph???n body c???a table</summary>
            renderScollable: function () {

            }
        });

        egov.utils.table = Table;

    })();


/// <summary>Th?? vi???n t??? ?????ng x??? l?? tooltip, c??c alert th??ng b??o, menu x??? xu???ng</summary>
/// Author: TienBV@bkav.com
/// DateCreated: 24/12/2013
/// Requires:
///     - jquery 1.6 + download t???i http://jquery.com/download/
///     - qtip2 download t???i http://qtip2.com/

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

        /// <summary>Hi???n th??? tooltips.</summary>
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

        /// <summary>Tr??? v??? tooltip option t????ng ???ng v???i v??? tr?? c???a target</summary>
        /// <param name="$this" type="object">target</param>
        var _getTooltipPosition = function ($this) {
            var lof = $this.offset().left;
            var tof = $this.offset().top;
            var h = $this.height();
            var w = $this.width();
            var tipWidth = $this.attr("etip-width");

            var myT = "top", myL = "center", atT = "bottom", atL = "center"; // m???c ?????nh hi???n ph??a d?????i c??n gi???a.

            if (lof < 20) // b??n tr??i
            {
                myL = "left";
                atL = "left";
            }
            if ((lof + w) > window.innerWidth - tipWidth / 2 || (lof + w) > window.innerWidth - 50) { //b??n ph???i
                myL = "right";
                atL = "right";
            }
            if (tof < 20) { // g??c tr??n
                atT = "bottom";
                myT = "top";
            }
            if ((tof + h) > window.innerHeight - 20) { // g??c d?????i
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
                position: {                 // Hi???n th??? ngay ph??a d?????i v?? c??n gi???a target
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
 * - T????ng th??ch v???i BkaveGov_FFPlugin_ChromNativeApp-1.0.0+.exe
 * - T????ng th??ch v???i BkaveGovExtension-4.6+
 *  
 * L???ch s???:
 * 
 * ChromeNativeApp V1.0.0
 * - T????ng th??ch v???i BkaveGov_FFPlugin_ChromNativeApp-1.0.0.exe 
 * - T????ng th??ch v???i BkaveGovExtension-4.6+
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
    // X??? l?? file: ?????c, ghi, m???, ????ng, l??u file. X??? l?? folder: x??a folder...
    GetMd5: "Egov_GetMd5",
    OpenFile: "Egov_OpenFile",
    CloseFile: "Egov_CloseFile",
    WriteFileBase64: "Egov_WriteFileBase64",
    ReadFileBase64: "Egov_ReadFileBase64",

    DeleteFile: "Egov_DeleteFile",
    DeleteFolder: "Egov_DeleteFolder",

    // K?? ??i???n t???
    GetCertIndex: "Egov_GetCertIndex",
    SignFile: "Egov_SignFile",
    SignFileByPoint: "Egov_SignFileByPoint",

    HasAppendModeVersion: "HasAppendModeVersion",

    // Scan v??n b???n
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
                // throw new Exception("Kh??ng t??m ???????c callbackid trong th??ng tin response.");
            }
            var callback = ChromeNativeApp.callbacks[callbackId];
            if (!callback) {
                return;
                // throw new Exception("Kh??ng t??m ???????c callback trong ChromeNativeApp.callbacks.");
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

    // G???i sang content.js th???c hi???n y??u c???u qua k??ch ho???t 1 event
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
              ?????C/GHI FILE
\************************************/

/**
 * M??? file
 * @public
 * @name    openFile(pathfile, callback)
 * @param   {String}    pathfile    ???????ng d???n t????ng ?????i\\t??n file. 
 *          VD: "1467366836294\\Vanban.doc". 
 *          V??? tr?? l??u file m???c ?????nh cua plugin: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov". 
 *          Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * @param   {function}  callback    h??m x??? l?? k???t qu??? tr??? v??? t??? plugin
 * @return  {Object}    result      k???t qu??? l?? 1 ?????i t?????ng d???ng:
 *          {"action":"Egov_OpenFile","returnCode":1,"returnMessage":"Th??nh c??ng"}";
 *          |-------------|--------------------------------------------------------------------------|
 *          |returnCode   |returnMessage                                                             |
 *	        |-------------|--------------------------------------------------------------------------|
 *          |1            |n???u m??? file th??nh c??ng                                                    |
 *          |2            |n???u file ??ang ???????c m???. ?????ng th???i th???c hi???n focus v??o file ??ang m??? n??y.    |
 *          |3            |n???u file kh??ng t???n t???i (do ch??a ???????c js ghi t???m ra tr?????c ????)              |
 *          |4            |n???u m??? file kh??ng th??nh c??ng do n???i dung file kh??ng ????ng ?????nh d???ng        |
 *          |-1           |n???u m??? file l???i do c??c nguy??n nh??n ch??a x??c ?????nh                          |
 *	        |-------------|--------------------------------------------------------------------------|
 */
ChromeNativeApp.prototype.openFile = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.OpenFile, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};

/**
 * ????ng file
 * @public
 * @name    closeFile(pathfile, callback)
 * @param   {String}    pathfile    ???????ng d???n t????ng ?????i\\t??n file. 
 *          VD: "1467366836294\\Vanban.doc". 
 *          V??? tr?? l??u file m???c ?????nh cua plugin: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov". 
 *          Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * @param   {function}  callback    h??m x??? l?? k???t qu??? tr??? v??? t??? plugin
 * @return  {Object}    result      k???t qu??? l?? 1 ?????i t?????ng d???ng:
 *          {"action": "Egov_CloseFile", "returnCode": 1, "returnMessage": "Th??nh c??ng"}";
 *          |-------------|-------------------------------------------------------------------------------------------------------------|
 *          |returnCode   |returnMessage                                                                                                |
 *	        |-------------|-------------------------------------------------------------------------------------------------------------|
 *          |1            |n???u ????ng file th??nh c??ng (???? l??u n???i dung file tr?????c khi ????ng)                                               |
 *          |2            |n???u file ???? ??ang kh??ng ???????c m??? (c?? th??? do ng?????i d??ng ???? ch??? ?????ng l??u v?? ????ng tr?????c ????, ho???c ch??a t???ng m???)    |
 *          |3            |n???u kh??ng th??? l??u n???i dung file tr?????c khi ????ng                                                               |
 *          |-1           |n???u m??? file l???i do c??c nguy??n nh??n ch??a x??c ?????nh                                                             |
 *	        |-------------|-------------------------------------------------------------------------------------------------------------|
 */
ChromeNativeApp.prototype.closeFile = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.CloseFile, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Ghi file ra ??? c???ng, v???i n???i dung file l?? Base64
 * ?????u v??o:
 *       * pathfile: ???????ng d???n t????ng ?????i + t??n file. VD: "1467366836294\\Vanban.doc"
 *         V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *         Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * ?????u ra: 
 *		Tr??? v??? 1 chu???i json c?? d???ng: "{\"action\":\"Egov_WriteFileBase64\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong ????: 
 *			* returnMessage: th??ng b??o c??? th??? c??c l???i t????ng ???ng v???i t???ng returnCode.
 *			* returnCode l??
 *				* 1 n???u ghi file th??nh c??ng.
 *          	* 2 n???u file ???? t???n t???i v?? mode ??? tr???ng th??i kh??ng y??u c???u ghi ???? (mode = false)
 *          	* -1 n???u ghi file l???i do c??c nguy??n nh??n kh??c (kh??ng c?? quy???n ghi, t???o th?? m???c, t???o file...)
 */
ChromeNativeApp.prototype.writeFileBase64 = function (pathfile, data, mode, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.WriteFileBase64, "fileName": pathfile, "data": data, "mode": mode };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * ?????c n???i dung file, tr??? v??? d???ng Base64
 *   ?????u v??o:
 *        * pathfile: ???????ng d???n t????ng ?????i + t??n file. VD: "1467366836294\\Vanban.doc"
 *          V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *   ?????u ra: 
 *		Tr??? v??? 1 chu???i json c?? d???ng: "{\"action\":\"Egov_ReadFileBase64\",\"base64\":\"" + base64 +"\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong ????: 
 *		    * base64: N???i dung base64 c???a file (???? x??? l?? l??u n???i dung file tr?????c khi ?????c n???i dung)
 *			* returnMessage: th??ng b??o c??? th??? c??c l???i t????ng ???ng v???i t???ng returnCode.
 *		    * returnCode l??
 *			    + 1 n???u l???y n???i dung file th??nh c??ng (sau khi ???? l??u n???i dung file)
 *			    + 2 n???u file kh??ng t???n t???i
 *			    + 3 n???u file kh??ng th??? l??u n???i dung tr?????c khi ?????c
 *              + -1 n???u l???y n???i dung file l???i do c??c nguy??n nh??n kh??c
 */
ChromeNativeApp.prototype.readFileBase64 = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ReadFileBase64, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};

/* 
 * Tr??? v??? tr???ng th??i file t???n t???i hay kh??ng
 * ?????u v??o:
 *      * pathfile: ???????ng d???n t????ng ?????i + t??n file. VD: "1467366836294\\Vanban.doc"
 *        V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * ?????u ra: 
 *	    Tr??? v??? 1 chu???i json c?? d???ng: "{\"action\":\"Egov_ExistsFile\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong ????: 
 *			* returnMessage: th??ng b??o c??? th??? c??c l???i t????ng ???ng v???i t???ng returnCode.
 *			* returnCode:
 *				+ 0 n???u file kh??ng t???n t???i.
 *				+ 1 n???u file t???n t???i.
 */
ChromeNativeApp.prototype.existsFile = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.ExistsFile, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * Tr??? v??? chu???i Md5 c???a n???i dung file, d??ng ????? so s??nh x??c ?????nh n???i dung file c?? thay ?????i kh??ng
 * ?????u v??o:
 *      * pathfile: ???????ng d???n t????ng ?????i + t??n file. VD: "1467366836294\\Vanban.doc"
 *        V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 * ?????u ra: 
 *      * Tr??? v??? 1 chu???i json c?? d???ng: "{\"action\":\"Egov_GetMd5\",\"md5\":\"" + string(md5,32) +"\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *      * Trong ????: 
 * 	        * md5: l?? hashmac c???a file
 *			* returnMessage: th??ng b??o c??? th??? c??c l???i t????ng ???ng v???i t???ng returnCode.
 * 			* returnCode:
 * 				 + 1 n???u l???y hashmac th??nh c??ng (sau khi ???? l??u n???i dung file th??nh c??ng)
 * 				 + 2 n???u file y??u c???u kh??ng t???n t???i
 * 				 + 3 n???u kh??ng th??? l??u n???i dung file tr?????c khi l???y hashmac
 *               + -1 n???u l???y hashmac kh??ng th??nh c??ng v?? c??c l?? do kh??c
 */
ChromeNativeApp.prototype.getMd5 = function (pathfile, callback) {
    var data1 = { "action": ChromeNativeApp.Egov_Event.GetMd5, "fileName": pathfile };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * X??a file
 *   ?????u v??o:
 *        * pathfile: ???????ng d???n t????ng ?????i(+t??n file) + mode (b???t x??a file). VD: "1467366836294\\Vanban.doc"
 *          V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
 *   ?????u ra: 
 *		Tr??? v??? 1 chu???i json c?? d???ng: "{\"action\":\"Egov_DeleteFile\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong ????: 
 *			* mode: 
 *				+ true: b???t x??a file
 *				+ false: kh??ng b???t x??a file
 *			* returnMessage: th??ng b??o c??? th??? c??c l???i t????ng ???ng v???i t???ng returnCode.
 *		    * returnCode l??
 *			    + 1 n???u x??a file th??nh c??ng
 *						* File ??ang m??? + mode (b???t x??a)
 *						* File x??a.
 *              + 2 n???u file ??ang m??? + mode(kh??ng b???t x??a)
 *				+ -1 n???u kh??ng th??? x??a file do c??c nguy??n nh??n kh??c.
 */
ChromeNativeApp.prototype.deleteFile = function (pathfile, mode, callback) {
    mode = mode || false;
    var data1 = { "action": ChromeNativeApp.Egov_Event.DeleteFile, "fileName": pathfile, "mode": mode };
    return ChromeNativeApp.sendToContent(data1, callback);
};


/* 
 * X??a folder
 *   ?????u v??o:
 *        * pathfile: ???????ng d???n t????ng ?????i(+t??n folder) + mode (b???t x??a folder). VD: "1467366836294\\Thumuc"
 *          V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *          Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Thumuc"
 *   ?????u ra: 
 *		Tr??? v??? 1 chu???i json c?? d???ng: "{\"action\":\"Egov_DeleteFolder\",\"returnCode\":\"" + returnCode +"\",\"returnMessage\":\"" + returnMessage +"\"}";
 *		Trong ????: 
 *			* mode: 
 *				+ true: b???t x??a folder
 *				+ false: kh??ng b???t x??a folder
 *			* returnMessage: th??ng b??o c??? th??? c??c l???i t????ng ???ng v???i t???ng returnCode.
 *		    * returnCode l??
 *			    + 1 n???u x??a folder th??nh c??ng
 *						* Folder c?? file + mode (b???t x??a)
 *						* Folder kh??ng c?? file.
 *              + 2 n???u Folder c?? file + mode(kh??ng b???t x??a)
 *				+ -1 n???u kh??ng th??? x??a Folder do c??c nguy??n nh??n kh??c.
 */
ChromeNativeApp.prototype.deleteFolder = function (pathfile, mode, callback) {
    mode = mode || false;
    var data1 = { "action": ChromeNativeApp.Egov_Event.DeleteFolder, "fileName": pathfile, "mode": mode };
    return ChromeNativeApp.sendToContent(data1, callback);
};

/************************************\
              K?? ??I???N T???
\************************************/

/* 
 * K?? ??i???n t??? File
 * ?????u v??o:
 *      * pathfile: ???????ng d???n t????ng ?????i + t??n file. VD: "1467366836294\\Vanban.doc"
 *        V??? tr?? l??u file m???c ?????nh: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov"
 *        Khi ???? v??? tr?? file ch??nh x??c l??: "C:\\Users\\Account\\AppData\\Local\\Temp\\BkavEgov\\1467366836294\\Vanban.doc"
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
 *		* idxCert: Index ch??? k?? ch???n ????? k??
 * ?????u ra: 
 *	    Tr??? v??? 1 chu???i json c?? d???ng: 
 *          "{
 *              \"action\":\"Egov_SignWord\",
 *				\"base64\":\"" + base64	+"\",
 *              \"returnCode\":\"" + returnCode +"\",
 *              \"returnMessage\":\"" + returnMessage +"\"
 *          }";
 *		Trong ????: 
 *			* returnCode:
 *				+ 1 n???u k?? ??i???n t??? th??nh c??ng.
 *              + 2 n???u file word c???n k?? kh??ng t???n t???i
 *              + 3 n???u cert kh??ng t??m th???y (c?? th??? do r??t token ra)
 *              + -1 n???u kh??ng th??? k?? ??i???n t??? do c??c nguy??n nh??n kh??c
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
 * - T????ng th??ch v???i BkaveGov_FFPlugin_ChromNativeApp-1.0.0+.exe
 * - T????ng th??ch v???i BkaveGovExtension-4.6+
 *  
 * L???ch s???:
 * 
 * ChromeNativeApp V1.0.0
 * - T????ng th??ch v???i BkaveGov_FFPlugin_ChromNativeApp-1.0.0.exe 
 * - T????ng th??ch v???i BkaveGovExtension-4.6+
 * 
 * S??? d???ng:
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
            /// Ch??n plugin
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

            // Kiem tra plugin/extension ???? s???n s??ng s??? d???ng?
            egov.extension.isReady(function (isReady) {
                if (isReady) {
                    this.checkNativeAppVersion(callback);
                } else {
                    // Ki???m tra plugin/extension ???? ???????c c??i ?????t?
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

        //#region T???i b??? c??i plugin

        showDialogDownloadPlugin: function (callback) {
            /// <summary>
            /// Hi???n th??? dialog y??u c???u t???i v??? plugin ????? m??? file.
            /// </summary>
            /// <param name="callback">H??m th???c thi sau khi c??i ?????t th??nh c??ng.</param>
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
            /// C??c event x???y ra khi ??ang download
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
            //TODO: Ch??? plugin th???c t??? ntn r???i th??m h??nh ???nh h?????ng d???n c??i ?????t theo k???ch b???n
            //Ph???n n??y ph???i xem plugin m???i c??i ?????t xong n?? ntn th?? m???i x??? l?? theo k???ch b???n ???????c
            //FireBreath.waitForInstall(that.pluginName, function () {
            //    dialog.close();
            //    if (callback && typeof (callback) === 'function') {
            //        callback();
            //    }
            //});
        },

        //#endregion

        //#region K??

        callSignSuccess: function (result) {
            if (this.tempFolderLoc) {
                this.deleteFolder(this.tempFolderLoc);
            }
            this.currentSignOptions = { withChoosePoint: false };
            egov.callback(this.signSuccess, result);
        },

        sign: function (document, signSuccess) {
            /// <summary>
            /// K?? c??c file trong v??n b???n
            /// </summary>
            /// <param name="document">v??n b???n</param>
            /// <param name="callback">H??m callback</param>
            var that,
                confirmSignFiles,
                docAttachments;

            that = this;
            that.currentSignOptions = { withChoosePoint: false };

            // Th?? m???c l??u file t???m ????? chu???n b??? k??.
            that.tempFolderLoc = (new Date()).getTime().toString() + "_forsign";

            that.hasDeleteOldFile = false;
            that.signatureConfig = egov.signatureConfig || [];
            that.signSuccess = signSuccess;

            var signerConfig = egov.setting.signerConfig;
            if (signerConfig.length === 0) {
                egov.pubsub.publish(egov.events.status.error, "B???n ch??a c???u h??nh ch??? k?? s???, vui l??ng v??o Thi???t l???p c?? nh??n/C???u h??nh ch??? k?? ????? t???o m???i.");
                that.callSignSuccess(false);
                return;
            }

            // Ghi to??n b??? n???i dung file ???nh ch??n v??o ch??? k?? ra th?? m???c t???m
            var writeSignatureConfig = $.isArray(signerConfig)
                                            ? signerConfig.slice(0, signerConfig.length)
                                            : $.extend(true, {}, signerConfig);

            that.document = document;
            that._writeImageSignature(writeSignatureConfig, function (signatureConfig) {
                // B???t ?????u qu?? tr??nh k??

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
            /// Hi???n th??? dialog k??
            /// </summary>
            /// <param name="confirmSignFiles">file c???n k??</param>
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
                settings.title = "K?? s??? ??i???n t???";
                settings.buttons = [
                {
                    text: "Xem tr?????c",
                    className: 'btn-warning',
                    click: function () {
                        that._signPreviewBtn();
                    }
                },
                {
                    text: "Chuy???n",
                    className: 'btn-primary',
                    click: function () {
                        that._signAndSendBtn();
                    }
                },
                {
                    text: "B??? qua",
                    className: 'btn-default',
                    click: function () {
                        that.isPreviewing = false;
                        that.$confirmDialog.dialog("destroy");
                        that.callSignSuccess(false);
                    }
                }];

                settings.confirm = {
                    text: "X??a t???p c?? sau khi k??",
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
             * Ghi n???i dung c??c file c???n k?? ra th?? m???c t???m, chu???n b??? cho qu?? tr??nh k??
             */
            var that = this;

            if (!base64OfAttachmentFiles) {
                callback();
                return;
            }

            // L???y ra c??c gi?? tr??? key c???a file
            var fileIds = Object.keys(base64OfAttachmentFiles);
            if (fileIds.length === 0) {
                callback();
                return;
            }

            // l???y ra file ????u ti??n ????? th???c hi???n k??
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
                    // N???u c?? l???i th?? b??o v?? return lu??n m?? k c???n g???i callback ????? d???ng ti???n tr??nh x??? l?? l???i
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                    return;
                }
            });
        },

        _writeImageSignature: function (signatureConfig, callback) {
            /*
             * Ghi t???m c??c file ???nh trong config k??
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
                        // N???u c?? l???i th?? b??o v?? return lu??n m?? k c???n g???i callback ????? d???ng ti???n tr??nh x??? l?? l???i
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

                egov.pubsub.publish(egov.events.status.success, "K?? file th??nh c??ng!");
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

            egov.pubsub.publish(egov.events.status.info, "Vui l??ng qu??t chu???t ch???n v??? tr?? c???n hi???n th??? ch??? k??.");
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
                title: "K?? s??? ??i???n t???",
                buttons: [
                    {
                        text: "Qu??t ch???n v??? tr?? kh??c",
                        className: 'btn-warning',
                        click: function () {
                            previewDialog.dialog("destroy");
                            // X??a th?? m???c ???? k?? tr?????c ???? tr?????c khi k?? l???i
                            that.deleteFolder(that.tempFolderLoc, function () {
                                that._signWithPositionAndPreview();
                            });
                        }
                    },
                    {
                        text: "Chuy???n",
                        className: 'btn-primary',
                        click: function () {
                            previewDialog.dialog("destroy");
                            that._uploadAndSend(filePdfAddNew);
                        }
                    },
                    {
                        text: "B??? qua",
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

            // N???u kh??ng c?? file n??o ???????c ch???n ????? k?? => g???n callback ????? ti???p t???c b??n giao.
            selectedFiles = that._getSelectedFiles(that.confirmSignFiles);
            selectedSigners = that._getSelectedSignerConfig(that.signatureConfig);

            if (selectedFiles.length <= 0 || selectedSigners.length <= 0) {
                egov.pubsub.publish(egov.events.status.warning, "B???n ch??a ch???n t???p ho???c ch??? k?? ????? k?? s???. Vui l??ng th??? l???i.");
                that.$confirmDialog.dialog("destroy");
                signSuccess(false, []);
                return;
            }

            // L???y danh s??ch file c???n t???i t??? server v??? ????? k?? => ghi ra th?? m???c t???m => k??
            var serverFiles = selectedFiles.filter(function (item) {
                return !item.get("fileData") || (!egov.setting.publish.detectPdfChangeContent && egov.fileExtension.isReadonly(item.get("Name")));
            });

            // T???i danh s??ch file v??? ????? k??
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

            // PROCESSING: B??o tr???ng th??i ??ang x??? l??
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

            egov.request.downloadAttachmentForSignBase64({
                data: {
                    fileIds: fileIds,
                    fileTempIds: fileTempIds,
                    convertWordTopdf: false
                },
                success: function (result) {
                    // Ghi to??n b??? n???i dung file c???n k?? ra th?? m???c t???m.
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
                // B???t ?????u qu?? tr??nh k??
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
            // K?? c??c file v???i 1 token.

            if (config.length === 0) {
                return success();
            }

            if (hasConfirm) {
                var signName = _.pluck(config, 'Title').join(",");
                var message = String.format("??ang k?? s??? cho ch??? k?? {0}. /nVui l??ng c???m token v??o m??y v?? nh???n Ok ????? ch???n.", signName);
                alert("Ch???n Token cho ch??? k?? " + signName);
            }

            var that = this;

            if (this.currentSignOptions && this.currentSignOptions.idxCert) {
                this._signFile(selectedFiles, filePdfAddNew, config, this.currentSignOptions.idxCert, function () {
                    success();
                }, this.currentSignOptions.withChoosePoint);
                return;
            }

            // Ch???y sau khi ch???n token
            var callbackCertIndex = function (jsonResult) {
                var idxCert = parseInt(jsonResult.idxCert);
                if (isNaN(idxCert) || idxCert <= -1) {
                    // B??o kh??ng c?? ch??? k?? n??o ???????c ch???n.
                    // Kh??ng ????ng c???a s??? ch???n file k?? hi???n t???i (kh??ng g???i $confirmDialog.dialog("destroy");)
                    // Kh??ng ti???p t???c b??n giao (Kh??ng g???i egov.callback(callback);)
                    that.isPreviewing = false;
                    egov.pubsub.publish(egov.events.status.error, "Vui l??ng ki???m tra l???i ch??? k?? s??? ???? s???n s??ng tr?????c khi k??!");
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
            /// k?? file
            /// </summary>
            /// <param name="item">file c???n k??</param>
            if (selectedFiles.length === 0) {
                callback();
                return;
            }
            withChoosePoint = withChoosePoint || false;
            var item = selectedFiles.pop();

            // ???????ng d???n file l??u trong %TEMP%/BkavEgovChrome
            var fileName = item.get("fileData");
            var isForSign = egov.fileExtension.isForSign(fileName);

            // Clone danh sach chu ky
            var cfigForSign = config.slice(0);
            if (isForSign) {
                //config = { Ext: "", FindText: "quangp", FindType: 1, : "", OffsetX: 0, OffsetY: 0, PosType: 3, SignType: 1, TextInfor: 1, Title: "Sign" };

                var cfig = cfigForSign.pop();
                var configSign = $.extend(true, { SignAuthor: egov.setting.userName, SignReason: "Bkav eGov - K?? s??? ??i???n t???" }, cfig);

                var signedComplete = function (jsonResult) {
                    // jsonResult = {action: "Egov_SignFile", base64: "", fileName: "1504834898526\{attachmentId}.pdf", returnCode: 1, returnMessage: ""}
                    cfig = cfigForSign.pop();

                    if (jsonResult.returnCode == 1) {
                        if (cfig != undefined) {
                            if (egov.fileExtension.isMsOfficeFile(fileName)) {
                                fileName = egov.fileExtension.getFileName(fileName) + ".pdf";
                            }

                            configSign = $.extend(true, { SignAuthor: egov.setting.userName, SignReason: "Bkav eGov - K?? s??? ??i???n t???" }, cfig);

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
                egov.pubsub.publish(egov.events.status.success, "????nh k??m file ???? k?? v??o v??n b???n th??nh c??ng!");
            });
        },

        _uploadSignedFile: function (signedFiles, document, callback) {
            /// <summary>
            /// Upload file ???? k?? l??n
            /// </summary>
            document.uploadSignFiles(signedFiles, callback);
        },

        //#endregion

        //#region File ????nh k??m

        // Ki???m so??t danh s??ch c??c file ??ang trong qu?? tr??nh m??? (t???i v???, ghi ra th?? m???c t???m, v?? m???) ????? tr??nh l???p khi click ????p li??n ti???p
        openAttachment: function (attachment, version) {
            /// <summary>
            /// M??? file ????nh k??m
            /// </summary>
            /// <param name="attachment"></param>
            /// <param name="version"></param>
            var id, storePrivateId;
            id = attachment.model.get('Id');
            storePrivateId = attachment.parent.storePrivateId;

            //// Neu file nay dang trong qua trinh mo thi return
            //if (this.opennings[id]) {
            //    console.log("Info: openAttachment - click ????p m??? file li??n ti???p! B??? qua do ??ang t???i v?? ghi file ra th?? m???c t???m.");
            //    return;
            //}

            // Danh dau qua trinh mo file bat dau
            //this.opennings[id] = id;

            // N???u l?? file v???a ????nh k??m
            if (attachment.model.get('isNew')) {
                // N???u file v???a ????nh k??m n??y ???? ???????c m??? tr?????c ???? (???? c?? file ghi t???m) 
                // => G???i l???i l???nh m??? file ????? m??? file ???? ghi t???m n??y n???u ???? ????ng, ho???c forcus l???i file ??ang m???.
                if (attachment.model.get("isOpen") && attachment.model.get("fileData")) {
                    egov.extension.openFile(attachment.model.get("fileData"));
                }
                else {
                    // N???u file ch??a t???ng ???????c m??? sau khi ????nh k??m (ch??a c?? file ghi t???m)
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
            /// L??u l???i c??c file ??ang ???????c s???a.
            /// </summary>
            /// <param name="callback">H??m th???c thi sau khi ho??n th??nh.</param>
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
            /// L??u c??c file ????nh k??m ??ang m???
            /// </summary>
            /// <param name="openFiles">Danh s??ch c??c file ??ang m???.</param>

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
                        console.log("egov.extension.closeFile: " + file.get('Name') + " ???? ????ng tr?????c ????");
                    }

                    egov.extension.readFileBase64(file.get('fileData'), function (jsonResult) {
                        if (jsonResult.returnCode == 1) {
                            // So sanh noi dung file truoc va sau
                            // N???u n???i dung file c?? thay ?????i
                            if (file.get("md5") != jsonResult.md5) {
                                var fileName = file.get('Name');
                                egov.message.show(String.format(egov.resources.document.attachment.fileChanged, fileName)
                                    , null
                                    , egov.message.messageButtons.YesNo
                                    , function () {
                                        attachments.modifiedFiles[file.get('Id')] = jsonResult.base64;

                                        // Ti???p t???c h???i v???i file ti???p theo
                                        that._confirmSaveAttachment(newArray, attachments);
                                    }
                                    , function () {
                                        // No: kh??ng l??u n???i dung ch???nh s???a
                                        that._confirmSaveAttachment(newArray, attachments);
                                    });
                            }
                                // N???u n???i dung file kh??ng thay ?????i
                            else {
                                that._confirmSaveAttachment(newArray, attachments);
                            }
                        } else if (jsonResult.returnCode == 2) {
                            console.log("egov.plugin::_confirmSaveAttachment::warning: ?????c n???i dung file kh??ng t???n t???i!!! C???n check l???i code.");
                        } else {
                            // TODO: B??o c??o l???i khi x??? l?? ????ng file, ????? ngh??? save file v?? ????ng file ??ang m??? v?? th??? l???i.
                            console.log("egov.plugin::_confirmSaveAttachment::Error: l???i kh??ng th??? l??u n???i dung file");
                        }
                    });
                } else {
                    egov.message.show(String.format("Vui l??ng ????ng ph???n m???m ??ang m??? file {0} v?? th??? l???i.", file.get('Name'))
                        , "Th??ng b??o", egov.message.messageButtons.Ok);
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