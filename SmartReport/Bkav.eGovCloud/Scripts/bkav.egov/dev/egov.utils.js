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