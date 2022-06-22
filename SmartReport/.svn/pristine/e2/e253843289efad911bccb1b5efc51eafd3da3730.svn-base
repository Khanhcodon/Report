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
/**
 * Copyright 2012 Tsvetan Tsvetkov
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Author: Tsvetan Tsvetkov (tsekach@gmail.com)
 */
(function (win) {
    var PERMISSION_DEFAULT = "default",
    PERMISSION_GRANTED = "granted",
    PERMISSION_DENIED = "denied",
    PERMISSION = [PERMISSION_GRANTED, PERMISSION_DEFAULT, PERMISSION_DENIED],
    defaultSetting = {
        pageVisibility: false,
        autoClose: 0
    },

    empty = {},
    emptyString = "",
    isSupported = (function () {
        var isSupported = false;
        try {
            isSupported = !!(/* Safari, Chrome */win.Notification || /* Chrome & ff-html5notifications plugin */win.webkitNotifications || /* Firefox Mobile */navigator.mozNotification || /* IE9+ */(win.external && win.external.msIsSiteMode() !== undefined));
        } catch (e) { }
        return isSupported;
    }()),
    ieVerification = Math.floor((Math.random() * 10) + 1),
    isFunction = function (value) { return (value && (value).constructor === Function); },
    isString = function (value) { return (value && (value).constructor === String); },
    isObject = function (value) { return (value && (value).constructor === Object); },
    mixin = function (target, source) {
        var name, s;
        for (name in source) {
            s = source[name];
            if (!(name in target) || (target[name] !== s && (!(name in empty) || empty[name] !== s))) {
                target[name] = s;
            }
        }
        return target; // Object
    },
    noop = function (PERMISSION_GRANTED) {
        if (Notification) {
            Notification.permission = PERMISSION_GRANTED;
        }
    },
    settings = defaultSetting;

    function getNotification(options) {
        var notification;
        if (win.Notification) {
            notification = new win.Notification(options.title, {
                icon: isString(options.icon) ? options.icon : options.icon.x32,
                body: options.body || emptyString,
                tag: options.tag || emptyString
            });

        } else if (win.webkitNotifications) { /* FF with html5Notifications plugin installed */
            notification = win.webkitNotifications.createNotification(options.icon, options.title, options.body);
            notification.show();
        } else if (navigator.mozNotification) { /* Firefox Mobile */
            notification = navigator.mozNotification.createNotification(options.title, options.body, options.icon);
            notification.show();
        } else if (win.external && win.external.msIsSiteMode()) { /* IE9+ */
            //Clear any previous notifications
            win.external.msSiteModeClearIconOverlay();
            win.external.msSiteModeSetIconOverlay((isString(options.icon) ? options.icon : options.icon.x16), options.title);
            win.external.msSiteModeActivate();
            notification = { "ieVerification": ieVerification + 1 };
        }
        return notification;
    }

    function getWrapper(notification) {
        return {
            close: function () {
                if (notification) {
                    if (notification.close) {
                        notification.close();
                        //notification.onshow(function () { });
                    } else if (win.external && win.external.msIsSiteMode()) {
                        if (notification.ieVerification === ieVerification) {
                            win.external.msSiteModeClearIconOverlay();
                        }
                    }
                }
            }
        };
    }

    function requestPermission() {
        if (!isSupported) { return; }
        var callbackFunction = noop;
        if (win.webkitNotifications && win.webkitNotifications.checkPermission) {
            win.webkitNotifications.requestPermission(callbackFunction);
        } else if (win.Notification && win.Notification.requestPermission) {
            win.Notification.requestPermission(callbackFunction);
        }
    }

    function permissionLevel() {
        var permission;
        if (!isSupported) { return; }
        if (win.Notification && win.Notification.permissionLevel) {
            //Safari 6
            permission = win.Notification.permissionLevel();
        } else if (win.webkitNotifications && win.webkitNotifications.checkPermission) {
            //Chrome & Firefox with html5-notifications plugin installed
            permission = PERMISSION[win.webkitNotifications.checkPermission()];
        } else if (navigator.mozNotification) {
            //Firefox Mobile
            permission = PERMISSION_GRANTED;
        } else if (win.Notification && win.Notification.permission) {
            // Firefox 23+
            permission = win.Notification.permission;
        } else if (win.external && (win.external.msIsSiteMode() !== undefined)) { /* keep last  && (win.external.msIsSiteMode() !== undefined)*/
            //IE9+
            permission = win.external.msIsSiteMode() !== undefined ? PERMISSION_GRANTED : PERMISSION_DEFAULT;
        }
        return permission;
    }

    /**
     *  
     */
    function config(params) {
        if (params && isObject(params)) {
            mixin(settings, params);
        }
        return settings;
    }

    function isDocumentHidden() {
        return settings.pageVisibility ? (document.hidden || document.msHidden || document.mozHidden || document.webkitHidden) : true;
    }

    //Tạo và hiển thị notification
    function createNotification(options) {
        var notification, notificationWrapper;
        if (isString(options.title) && (isString(options.icon) || isobject(options.icon))) {
            notification = getNotification(options);
            var clickFunc = isFunction(options.click) ? options.click : function () { };
            notification.addEventListener("click", clickFunc);
            var closeFunc = isFunction(options.close) ? options.close : function () { };
            notification.addEventListener("close", closeFunc);
            notificationWrapper = getWrapper(notification);
        }

        return notificationWrapper;
    }

    //Kiểm tra trình duyệt: Có hỗ trợ notification hay không?.
    //Khác Ie => ie chưa hỗ trợ notification nhưng các hàm kiểm tra vẫn đúng lên dùng hàm isIE() để loại bỏ ie.
    function hasNotify() {
        return ((!isIE() && isSupported) && (isDocumentHidden() && (permissionLevel() === PERMISSION_GRANTED)))
    }

    //Kiểm tra trình duyệt đang chạy có phải là ie hay không
    function isIE() {
        return navigator.userAgent.indexOf('MSIE') > -1;
    }

    win.notify = {
        PERMISSION_DEFAULT: PERMISSION_DEFAULT,
        PERMISSION_GRANTED: PERMISSION_GRANTED,
        PERMISSION_DENIED: PERMISSION_DENIED,
        isSupported: isSupported,
        config: config,
        createNotification: createNotification,
        permissionLevel: permissionLevel,
        requestPermission: requestPermission,
        hasNotify: hasNotify
    };

    if (isFunction(Object.seal)) {
        //Object.seal(win.notify);
        win.PERMISSION_DEFAULT = PERMISSION_DEFAULT;
        win.PERMISSION_GRANTED = PERMISSION_GRANTED;
        win.PERMISSION_DENIED = PERMISSION_DENIED;
        win.isSupported = isSupported;
        win.config = config;
        win.createNotification = createNotification;
        win.permissionLevel = permissionLevel;
        win.requestPermission = requestPermission;
        win.hasNotify = hasNotify;
    }
}(window));

(function ($) {
    if (typeof $ === undefined)
        throw getResource("egov.resources.notJqueryAlert");

    var mailType =
    {
        Bmail: 1,
        Gmail: 2,
        Yahoo: 3,
        MailExchange: 4,
        MDaemon: 5
    };

    window.helper = {
        timeShowSoundNotify: 60000,//Thiết lập thời gian để bật âm thanh khi hiển thị notify, tránh khi nhận notify liên tục hiện thị âm thanh liên tục

        hasHideAfterDisplayNotify: false,//Thiết lập có ẩn notify sau khi hiển thị hay không

        timeOutHideNotify: null,// Thiết lập timout khi ẩn notify

        timeHideAfterDisplayNotify: 0,//Thiết lập thời gian ẩn notify khi cấu hình ẩn notify.

        change: true,// Thay đổi title

        isLoad: false,//Thiết lập trạng thái load lấy những notify cũ hơn của người dùng

        isAllData: false,//Thiết lập trạng thái đã load toàn bộ notify của người dùng ra

        isPlayAudio: false,//Thiết lập trạng thái để bật nhạc khi hiển thị notify tránh khi nhận nhiều notify cung lúc thì hiển thị nhiều.

        clearChangeTitle: null,

        connection: {
            isLoaded: false,
            BmailLink: '?egov=1',
            KNTCLink: '/TalkingPeople',
            CalendarLink: '?egov=1&calendar=1',
            ChatLink: '',
            CssLinks: [], // '/mail/Chat/css/ChatEgov.css'
            ContactMobile: '/mobile/#ContactPage',
            JsLinks: [],
            MailType: undefined,
            //[
            //    '/mail/ChatHangout/PopupChat.js',
            //    '/mail/ChatHangout/ChatHangout.js',
            //    '/mail/ChatHangout/MenuButton.js',
            //    '/mail/ChatHangout/WindowChat.js',
            //]
        },

        // TienBV: tạm chuyển hết về eGov Theme
        bkavElements: {
            bmail: { divContainId: "#div-bmail", iframeName: "bmail", tab: "#tab-bmail", name: getResource("egov.resources.bmail"), color: "egov-theme", hasShowChat: true },
            documents: { divContainId: "#div-document", iframeName: "documents", tab: "#tab-document", name: getResource("egov.resources.documentslabel"), color: "egov-theme", hasShowChat: true },
            chat: { divContainId: "#div-chat", iframeName: "chat", tab: "#tab-chat", name: getResource("egov.resources.chat"), color: "egov-theme", hasShowChat: false },
            calendar: { divContainId: "#div-calendar", iframeName: "calendar", tab: "#tab-calendar", name: getResource("egov.resources.calendar"), color: "egov-theme", hasShowChat: true },
            report: { divContainId: "#div-report", iframeName: "report", tab: "#tab-report", name: getResource("egov.resources.reportLabel"), color: "egov-theme", hasShowChat: false },
            kntc: { divContainId: "#div-kntc", iframeName: "kntc", tab: "#tab-kntc", name: getResource("egov.resources.kntc"), color: "egov-theme" },
            links: { divContainId: "#div-links", tab: "#tab-links", name: getResource("egov.resources.links"), color: "egov-theme" },
            statistics: { divContainId: "#div-statistics", iframeName: "statistics", tab: "#tab-statistics", name: getResource("egov.resources.statictisLabel"), color: "egov-theme", hasShowChat: false },
            overall: { divContainId: "#div-overall", iframeName: "overall", tab: "#tab-overall", name: getResource("egov.resources.overall"), color: "egov-theme", hasShowChat: false },
            bwss: { divContainId: "#div-bwss", iframeName: "bwss", tab: "#tab-bwss", name: "bwss", color: "egov-theme" },
            cbcl: { divContainId: "#div-cbcl", iframeName: "cbcl", tab: "#tab-cbcl", name: getResource("egov.resources.overall"), color: "egov-theme" },
            calendarregiter: { divContainId: "#div-calendarregiter", iframeName: "calendarregiter", tab: "#tab-calendarregiter", name: getResource("egov.resources.overall"), color: "egov-theme" }
        },

        isTool: isTool(),//Thiết lập xem có dùng bản desktop hay không

        getContentWindow: function (frameName) {
            if (frameName === undefined) {
                return null;
            }
            var _el = document.getElementsByName(frameName);
            if (_el === undefined || _el.length <= 0) {
                return null;
            }

            return _el[0].contentWindow;
        },

        compareObject: function (aObj, bObj, checkLengthPropery) {
            ///<summary> So sánh 2 giá trị với nhau
            /// Todo: mục đích viết là cho thêm trường checkLengthPropery để kiểm tra vào
            /// Vì dữ liệu model trả về từ server nhiều lúc không có các thuộc tính như  model ở client
            /// nên việc so sánh nhiều khi không đúng lên viết thêm hàm này.
            /// Nếu bỏ qua checkLengthPropery có thể dung _.isEquals của underscore  hoặc, compare của jQuery...
            ///</summary>
            ///<param name="aObj">Giá trị thứ 1</param>
            ///<param name="aObj">Giá trị thứ 2</param>
            ///<param name="checkLengthPropery" type="bool">Có kiểm tra độ dài, số lượng giá trị con hay không nếu là kiểu Object hay Array</param>
            if (typeof aObj !== "object" && typeof bObj !== "object") {
                if (aObj != bObj) {
                    return false;
                }

                return true;
            }
            else {
                if (aObj instanceof Array && bObj instanceof Array) {
                    if (aObj.length !== bObj.length) {
                        return false;
                    }

                    if (aObj.length > 0) {
                        for (var i = 0; i < aObj.length; i++) {
                            if (!this.compareObject(aObj[i], bObj[i], checkLengthPropery, true)) {
                                return false;
                            }
                        }
                    }

                    return true;
                }
                else {
                    try {
                        // Lấy các thuộc tính của các đối tượng
                        var aProps = Object.getOwnPropertyNames(aObj);
                        var bProps = Object.getOwnPropertyNames(bObj);

                        ///Kiểm tra số lượng thuộc tính của object
                        if (checkLengthPropery) {
                            if (aProps.length !== bProps.length) {
                                return false;
                            }
                        }

                        if (aProps.length > 0) {
                            for (var i = 0; i < aProps.length; i++) {
                                var propName = aProps[i];
                                if (!this.compareObject(aObj[propName], bObj[propName], true)) {
                                    return false;
                                }
                            }
                        }

                        return true;

                    } catch (ex) {
                        return false
                    }
                }
            }
        },

        checkExistInArray: function (item, objArr, checkLengthPropery) {
            ///<summary> So sánh 2 giá trị với nhau</summary>
            ///<param name="aObj">Giá trị thứ 1</param>
            ///<param name="objArr" type="Array">Mảng chứa giá trị so sánh</param>
            ///<param name="checkLengthPropery" type="bool">Có kiểm tra độ dài, số lượng giá trị con hay không nếu là kiểu Object hay Array</param>
            if (objArr instanceof Array && objArr.length > 0 && item) {
                for (var i = 0; i < objArr.length; i++) {
                    if (this.compareObject(item, objArr[i], checkLengthPropery)) {
                        return true;
                    }
                }
            }

            return false;
        },

        playAudio: function (url) {
            try {
                var _this = this;
                if (!_this.isPlayAudio) {
                    if (!url) {
                        url = '../Content/Sound/notify.wav';
                    }

                    var audio = new Audio(url);
                    audio.play();

                    _this.isPlayAudio = true;
                    window.setTimeout(function () {
                        _this.isPlayAudio = false;
                    }, _this.timeShowSoundNotify);
                }
            }
            catch (ex) { }
        },

        loadFrame: function (name, onLoadedSuccess) {
            var app = this.bkavElements[name];
            if (!app) {
                return;
            }

            // Load app
            var appFrame = document.getElementsByName(name);

            if (appFrame.length === 0) {
                // Chưa load frame lần nào
                var frame = $('<iframe>').attr('name', name).attr('id', name);
                var that = this;
                if (!that.connection.isLoaded) {
                    $.ajax({
                        url: '/Home/GetConnectionSettings'
                    }).success(function (result) {
                        //Set lại các link liên kết với bmail

                        that.connection.ParentDomain = result.ParentDomain;
                        that.connection.KNTCLink = result.KNTCLink + that.connection.KNTCLink;
                        that.connection.ChatLink = result.ChatLink;
                        that.connection.MailType = result.MailType;

                        if (result.MailType == mailType.Bmail) {
                            that.connection.BmailLink = result.BmailLink + that.connection.BmailLink;
                            that.connection.BmailLink += "&domain" + that.connection.ParentDomain;

                        }
                        else {
                            var mailUrl = localStorage.getItem("mdaemonUrl");
                            if (mailUrl != undefined) {
                                that.connection.BmailLink = mailUrl;
                                that.connection.CalendarLink = mailUrl;
                            }
                            else {
                                that.connection.BmailLink = result.BmailLink;
                                that.connection.CalendarLink = result.BmailLink;
                            }
                        }


                        //for (var i = 0; i < that.connection.JsLinks.length; i++) {
                        that.connection.isLoaded = true;
                        //    that.connection.JsLinks[i] = result.BmailLink + that.connection.JsLinks[i];
                        //}

                        document.domain = that.connection.ParentDomain;

                        var appUrl = that.getAppUrl(name);

                        frame.attr("src", appUrl);

                        $(app.divContainId).append(frame);
                        $(frame).ready(function () {
                            if (typeof onLoadedSuccess === 'function') {
                                onLoadedSuccess();
                            }
                        });

                        that.showHideApp(result);
                    });
                } else {
                    var appUrl = that.getAppUrl(name);
                    $(app.divContainId).append(frame);
                    frame.attr("src", appUrl);
                    $(frame).ready(function () {
                        if (typeof onLoadedSuccess === 'function') {
                            onLoadedSuccess();
                        }
                    });
                }
            }
            else {
                //Refresh trang
                if (name === "kntc") {
                    appFrame[0].src = this.connection.KNTCLink;
                }

                if (typeof onLoadedSuccess === 'function') {
                    onLoadedSuccess();
                }
            }
        },

        displayApp: function (name, onLoadedSuccess) {
            /// <summary>
            /// Hiển thị ứng dụng tương ứng theo tên.
            /// </summary>
            /// <param name="name">Tên ứng dụng: bmail, egov, chat, calendar</param>
            var app,
                menuItem;

            app = this.bkavElements[name];
            if (!app) {
                return;
            }

            menuItem = $('.menu-items a[data-ng-app = "' + name + '"]');

            // Active menu tương ứng
            if (!menuItem.hasClass('active')) {
                $('.menu-items .active').removeClass('active');
                menuItem.addClass('active');
            }
            
            // Show container
            $(app.divContainId).siblings().hide();
            $(app.divContainId).show();

            if (app.iframeName) {
                $('#colorTheme').attr("class", app.color);

                // Load app
                this.loadFrame(name, onLoadedSuccess);
                this.changeSearchApp(name);
            }

            if (name === "links" && $(app.divContainId).is(":empty")) {
                $(app.divContainId).load("/Home/Linkeds");
            }

        },

        isCurrentFrame: function (name) {
            var app,
                menuItem;

            app = this.bkavElements[name];

            if (!app) {
                return false;
            }

            menuItem = $('.menu-items a[data-ng-app = "' + name + '"]');
            return menuItem.hasClass('active');
        },

        getAppUrl: function (name) {
            var result;

            switch (name) {
                case "documents":
                    return "/Home/Index";
                case "chat":
                    return this.connection.ChatLink;
                case "calendar":
                    return this.connection.CalendarLink;
                case "contact":
                    return this.connection.ContactMobile;
                case "report":
                    return "/ReportViewer/Index";
                case "statistics":
                    return "/statistics/Index";
                case "overall":
                    return "/Statistics/Index";
                case "kntc":
                    return this.connection.KNTCLink;
                case "cbcl":
                    return "/EvaluationCriteria/Index";
                case "calendarregiter":
                    return "/Calendar/Index";
                default:
                    result = this.connection.BmailLink;
                    if (this.isTool) {
                        result += '&client=tool'
                    }
                    return result;
            }

        },

        changeCreateButtonName: function (name) {
            switch (name) {
                case "documents":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblDocument"));
                    break;
                case "chat":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewConversion"));
                    break;
                case "calendar":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewWorkTime"));
                    break;
                case "bmail":
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewMail"));
                    break;
                default:
                    $('.li-create-new .create-new-label').text(getResource("egov.resources.lblNewMail"));
                    break;
            }
        },

        changeSearchApp: function (name) {
            switch (name) {
                case "documents":
                    $('.form-search').show();
                    $(".search-type-btn").show();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchDocument"));
                    break;
                case "chat":
                    $('.form-search').hide();
                    break;
                case "calendar":
                    $('.form-search').hide();
                    break;
                default:
                    $('.form-search').show();
                    $(".search-type-btn").hide();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchMail"));
                    break;
            }
        },

        destroyEvent: function (e) {
            if (!e) {
                return;
            }
            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                // fix cho ie 8
                e.returnValue = false;
            }
            e.stopPropagation();
        },

        changeTitle: function (newTitle, oldTile) {
            ///<summary>
            /// Set thời gian thay đổi title
            ///</summary>
            ///<param name="newTitle" type="string"> Title mới</param>
            ///<param name="oldTile" type="string"> Title cũ</param>
            window.document.title = this.change ? newTitle : oldTile;
            this.change = !this.change;
        },

        timer: function (number) {
            var defaultTitle = "eGovCloud";
            var _this = this;

            if (helper.clearChangeTitle) {
                window.clearInterval(helper.clearChangeTitle);
                window.document.title = defaultTitle;
            }

            if (number > 0) {
                helper.clearChangeTitle = window.setInterval(function () {
                    _this.changeTitle(getResource("egov.resources.youHave") + " " + number + " " + getResource("egov.resources.unreadDocuments"), defaultTitle);
                }, 1000);
            }
        },

        deleteAllLocalStorage: function () {
            ///<summary>
            /// Xóa toàn bộ localstorage
            ///</summary>
            if ('localStorage' in window && window['localStorage'] !== null) {
                window['localStorage'].clear();
            }
        },

        deleteAllCookies: function () {
            ///<summary>
            /// Xóa toàn bộ cookie của trang 
            ///</summary>
            var cookies = document.cookie.split(";");
            for (var i = 0; i < cookies.length; i++) {
                var cookie = cookies[i];
                var eqPos = cookie.indexOf("=");
                var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
            }
        },

        setCookie: function (cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        },

        getCookie: function (cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1);
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        },

        loadCss: function (href) {
            var link = $("<link>");
            $("head").append(link);
            link.attr({
                rel: "stylesheet",
                type: "text/css",
                href: href
            });
        },

        loadJs: function (href) {
            $.getScript(href);
        },

        loadBmailResources: function () {
            var that = this;
            $.each(this.connection.CssLinks, function (index, value) {
                that.loadCss(value);
            });
            $.each(this.connection.JsLinks, function (index, value) {
                that.loadJs(value);
            });
        },

        showHideApp: function (connection) {
            if (connection.BmailLink == undefined || connection.BmailLink == "") {
                $(".bmail, .calendar").remove();
            }
            if (connection.ChatLink == undefined || connection.ChatLink == "") {
                $(".chat").remove();
            }
        }
    };

    function getResource(resourceKey) {
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

    function isTool() {
        if (!egov.utilities) {
            require(["util/bkav.utilities"], function () {
                return egov.utilities.url.getQueryStringValue('client') === 'tool';
            });
        }
        else {
            return egov.utilities.url.getQueryStringValue('client') === 'tool'
        }
    }

})(window.jQuery);
/*
 * TienBV@bkav.com
 * 
 * Thư viện xử lý notification eGov
 * Bao gồm quản lý việc hiển thị notify trên header, notify browser, notify desktop.
 * 
 * === PUBLISH APIS ================
 * 
 * - window.registerNotificationApp(notifyConfig): đăng ký app mới vào hệ thống notification
                    notifyConfig = defaultConfig ở dưới

 * - window.pushNotifyFromClient(notifyModel): đẩy notify vào trung tâm thông báo, trung tâm thông báo tự động hiển thị lên header và hiển thị notify theo app.
 *                      notifyModel = theo NotifyModel ở dưới
 * 
 * 
 * === REMARKS =====================
 * 
 * Hiển thị desktop notification:
 * - Chỉ hiển thị các notificaion chưa đọc.
 * 
 * Các sự kiện click notify:
 * - Click Icon trên header: Cập nhật trạng thái tất cả  notify về đã xem, ẩn hết các notify đang hiển thị trên browser hoặc desktop.
 * - Click Notify trên Browser, Desktop: ẩn notify, Xóa notify trên danh sách, gọi hàm callback.
 * - Click notify trên danh sách: Ẩn notify desktop, Xóa notify trên danh sách,  gọi hàm callback.
 * - Click CloseButton notify trên Browser, Desktop: ẩn notify, các thứ khác giữ nguyên.
 * - Click CloseButton trên danh sách: Ẩn notify desktop, xóa notify khỏi danh sách.
 * - Click xóa tất cả: xóa tất cả notify trên danh sách, ẩn tất cả notify desktop tương ứng.
 * 
 * Update lên server:
 * - Khi xảy ra việc xóa notify khỏi danh sách.
 */

/*
 * Danh sách cần tối ưu:
 * - Xử lý lại mỗi lần cập nhật sẽ cập nhật tất cả danh sách từ server:
 *   Sửa hàm fetch: bỏ lastId đi, mỗi lần get là get toàn bộ danh sách.
 * - Xử lý hiển thị lại thời gian, hiện đang hiện số âm: utils/bt.util.date.js.
 * - Xử lý request remove về cập nhật trạng thái delete = true thay vì xóa hẳn.
 * - Mỗi lần click mở danh sách notifications ra cần cập nhật lại toàn bộ thời gian.
 * - Cuộn xem tiếp.
 * - Avatar lỗi (chưa có ảnh đại diện thì hiển thị avatar mặc định).
 * - Tham khảo facebook xem nó làm thế nào áp dụng vào.
 */

(function ($, _, Backbone, desktopNotify) {
    "use strict";


    var urlSettings = {
        getConfig: "/Notification/GetConfig",
        // fetch: "/Notification/GetNotSents",
        fetch: "/Parallel/Notification_GetNotSents",
        getData: "/Notification/GetData",
        pulling: "/Notification/GetLastestMails",
        update: "/Notification/Update",
        removeAll: "/Notification/RemoveAll",
        remove: "/Notification/Remove",
        oldAll: "/Notification/OldAll",
        readAll: "/Notification/ReadAll",
        read: "/Notification/Read",
        setBmailFolder: '/Notification/SetMailFolderNotify'
    };

    /*
     * Notify trên web client
     * Nếu browser có hỗ trợ notification
     */
    var webClientNotify = function () {
        this._hasWebNotification = false;
        this._instance = null;
        this._notification = null;

        this._chromeNotificationTemplateType = {
            basic: "basic",     // Kiểu mặc định bao gồm title, message, icon, 2 nút bấm
            image: "image",     // Bao gồm kiểu basic và thêm preview 1 ảnh phía dưới
            list: "list",       // Bao gồm basic và hiển thị một danh sách notify thông qua danh sách items.
            progress: "progress"    // Hiển thị tiến độ xử lý: icon, title, message, progress, 2 nút bấm
        },
        this._chromeNotificationOptions = {
            type: "basic",      // templatetype mặc định
            iconUrl: "",        // link hình ảnh lớn của notify, có thể là: avatar người gửi, app icon, thumbnail image
            appIconMaskUrl: "", // Link icon của app
            title: "",          // Tiêu đề của thông báo
            message: "",        // Nội dung tin nhắn
            contextMessage: "",
            priority: 0,        // Độ ưu tiên từ -2 -> 2; -2 là thấp nhất; 0 là mặc định. Một số nền tảng không hỗ trợ -2, -1 do đó cần lưu ý khi sử dụng.
            eventTime: 0,       // Hẹn giờ hiển thị; ví dụ (Date.now() + n)
            buttons: [],
            imageUrl: "",       // Link preview một ảnh.
            items: [],          // Danh sách notify với templatetype là list: bao gồm các thông tin title và message
            // [{title: "", message: ""}, ...]
            progress: 0,        // Tiến độ xử lý từ 0 => 100 với kiểu templatetype là progress.
            isClickable: false, // Cho phép click vào body của notify
            requireInteraction: false   // Từ Chrome 50: chỉ ra rằng notify vẫn hiển thị trên màn hình đến khi người dùng click vào hoặc tắt nó đi. Mặc định là false
        },

        this.Instance = function () {
            if (!this._instance) {
                this._instance = new webClientNotify();
                this._checkBrowserNotification();
            }
            return this;
        };

        this.show = function (notify) {
            if (!this._hasWebNotification) {
                return;
            }
            var notify = this._create(notify);
            return notify;
        };

        this.showList = function (notifies) {

        },

        this._create = function (notify) {
            var title = notify.get("Title");
            var messsage = notify.get("Body");
            var icon = notify.get("Avatar");
            var notifyCallback = notify.Callback;
            var alternateContent = notify.get("alternateContent");

            if (alternateContent != undefined && alternateContent != "") {
                messsage = alternateContent;
            }

            var result = new this._notification(title, {
                body: messsage,
                icon: icon,
                silent: false,
                sound: '../Content/Sound/notify.wav',
                requireInteraction: true
            });

            result.onclick = function (e) {
                result.close();
                if (isFunction(notifyCallback)) {
                    notifyCallback(notify);
                }
            }

            return result;
        };

        this._checkBrowserNotification = function () {
            var that = this;

            this._notification = chrome.notifications || window.Notification || window.mozNotification || window.webkitNotification;

            if (!this._notification) {
                this._hasWebNotification = false;
            }

            var permission = this._notification.permission;

            if (permission == "default") {
                //Người dùng cho phép site hiển thị thông báo hay không
                this._notification.requestPermission(function (status) {
                    that._hasWebNotification = that.notificationManager.permission !== status;
                });
            } else {
                this._hasWebNotification = permission == "granted";
            }
        };
    };

    /*
     * Notify trên bản desktop
     * Nếu đang chạy app trên bản desktop
     */
    var desktopClientNotify = function () {
        this._isDesktop = false;

        this._instance = null;

        this.type = 3;

        this.Instance = function () {
            if (!this._instance) {
                this._instance = new desktopClientNotify();
                this._isDesktop = this.checkDesktopApp();
            }

            return this._instance;
        };

        this.show = function (notify) {
            var that = this;
            var title = notify.get("Title");
            var messsage = notify.get("Body");
            var icon = window.location.origin + "/logo.png";
            var notifyCallback = notify.Callback;
            var data = notify.get("JsonData");
            var senderName = data.SenderUserName;
            var alternateContent = notify.get("alternateContent");

            if (alternateContent != undefined && alternateContent != "") {
                messsage = alternateContent;
            }

            try {
                var jsonData = [
                {
                    "info": {
                        "title": title,
                        "content": messsage,
                        "account": "egov",
                        "avatar": icon,
                        "fullname": title,
                        "duration": that.type == 3 ? 5 : 0,
                        "type": that.type,
                    },
                    "callback": notifyCallback,
                    "callbackdata":
                    {
                        "notificationId": notify.get("NotificationId"),
                        "type": that.type
                    }
                }];

                window.external.CB_ShowNotify(JSON.stringify(jsonData));
            } catch (e) {
                this._isDesktop = false;
                console.log(e);
            }
        };

        this.checkDesktopApp = function () {
            return isDesktopApp();
        };
    };

    // #region Default Apps

    var defaultConfig = {
        "mail": {
            name: "bmail",                       // Tên app
            isActive: false,
            displayName: "Thông báo mail",
            el: ".div-bmail-notify",
            icon: '../Content/bkav.egov/icon egov/egov-11.png', // icon hiển thị lên header
            announIcon: '../Content/bkav.egov/icon egov/egov-16.png',  // Icon thông báo khi notify
            total: 0,                           // mặc định
            model: [],                          // mặc định
            callback: {                         // Xử lý khi click vào notify
                frameName: "",                  // Tên frame
                api: "readNewMail"              // Tên hàm gọi callback trong frame
            },
            isValidData: function (mailData) {
                var title = mailData.title,
                    folder = mailData.folderLocation,
                    result = false,
                    followFolders = this.followFolders;

                if (title.indexOf('[Spam]') >= 0) {
                    return result;
                }

                if (folder.indexOf("/") != 0) {
                    folder = "/" + folder;
                }

                if (_.indexOf(followFolders, folder) < 0) {
                    return result;
                }

                return true;
            },
            isFocus: function () {
                return $(".menu-items .bmail").is(".active");
            }
        },
        "egov": {
            name: "documents",
            isActive: false,
            displayName: "Thông báo xử lý văn bản",
            el: ".div-eGov-notify",
            icon: '../Content/bkav.egov/icon egov/egov-01.png',
            announIcon: '../Content/bkav.egov/icon egov/egov-14.png',
            total: 0,
            model: [],
            callback: {
                frameName: "documents",
                api: "openNotify"
            },
            isFocus: function () {
                return $(".menu-items .egov").is(".active");
            }
        },
        "chat": {
            name: "chat",
            frameName: "",
            isActive: false,
            displayName: "Thông báo chat",
            el: ".div-chat-notify",
            icon: '../Content/bkav.egov/icon egov/egov-10.png',
            announIcon: '../Content/bkav.egov/icon egov/egov-10.png',
            total: 0,
            model: [],
            callback: {
                frameName: "",
                api: "openChat"
            },
            isFocus: function () {
                return $(".menu-items .chat").is(".active");
            }
        }
    };

    //#endregion

    // #region Model

    var NotifyModel = Backbone.Model.extend({
        defaults: {
            NotificationId: 0,
            UserId: 0,
            Title: "",      // Tiêu đề thông báo
            Body: "",       // Nội dung thông báo
            alternateContent: "", // Nội dung thay thế để hiển thị notification desktop khi mà nội dung chính là html
            Avatar: "",     // Ảnh đại diện
            DateCreated: "",    // Ngày tháng
            AppName: "",    // Tên app
            IsNew: true,    // Trạng thái mới
            IsReaded: false, // Trạng thái đã xem hay chưa
            OnlyAnnoun: false,  // Trạng thái xác định notify chỉ để thông báo, ko xử lý.
            IsCreatedByClient: false,  // Trạng thái xác định notify được thêm mới ở client
            JsonData: {}    // Dữ liệu notify: dùng để truyền vào hàm callback
        }
    });

    var NotifyList = Backbone.Collection.extend({
        model: NotifyModel
    });

    //#endregion

    //#region Templates

    var notifyTemplate = ' <a href="#" class="ripple-ef dropdown-toggle" data-toggle="dropdown">'
                        + '     <img src="${icon}" />'
                        + '     <span class="notify-count" data-count="0"></span>'
                        + '</a>'
                        + '<div class="dropdown-menu" role="menu">'
                        + '     <div class="headerrow text-right">'
                        + '         <a href="#" class="btnReadAll">Đánh dấu tất cả là đã đọc</a>'
                        + '         <a href="#" class="btnCloseAll">Xóa tất cả</a>'
                        + '     </div>'
                        + '     <ul class="notification list-group"></ul>'
                        + '     <div class="text-center loading-image"></div>'
                        + '</div>';

    var templateInBell = '<li id="${NotificationId}" app="${AppName}" class="list-group-item {{if IsReaded == false}} unview {{/if}}">'
                     + '  <a href="#" class="open-notify row">'
                     + '    <div class="col-md-3">'
                     + '        <img src="${Avatar}" class="avatar"/>'
                     + '    </div>'
                     + '    <div class="col-md-13">'
                     + '         <div>'
                     + '            <div class="notify-usersend">${Title}</div>'
                     + '            <div class="notify-content">{{html Body}}</div>'
                     + '            <div class="notify-date">${DateCreated}</div>'
                     + '         </div>'
                     + '    </div>'
                     + '    <span class="close-notify" title="Xóa thông báo">x</span>'
                     + '    </a></li>';

    var loadingTemplate = '<div id="progress" style="display:none">'
                          + '<h4>Loading...</h4>'
                          + '</div>';
    var pageIndex = 0;
    var pageSize = 10;

    //#endregion

    var EgovNotificationView = Backbone.View.extend({
        el: "#notificationCenter",

        events: {
            "click .notifyBell": "_setOlds"
        },

        _isDesktop: false,


        // Danh sách các app được đăng ký hiển thị notify
        _registedApps: {},

        template: notifyTemplate,
        model: new NotifyList,

        initialize: function (options) {
            var that = this;
            that.lastId = 0;
            that.settings = options.settings;
            that._getNotificationConfig(function (config) {
                that._registedApps = config;
                that._isDesktop = isDesktopApp();
            });
            that.render();
        },

        render: function () {
            var that = this;
            that.$(".notifyBell").remove();

            for (var appName in that._registedApps) {
                var app = that._registedApps[appName];
                app.$el = $(app.el);
                if (app.$el.length === 0) {
                    app.$el = $("<li>").addClass("pull-left hidden notifyBell").addClass(app.el).attr("app", appName);
                    that.$el.prepend(app.$el);
                }

                if (app.isActive) {
                    app.$el.removeClass("hidden");
                }

                app.$el.html($.tmpl(that.template, { icon: app.icon }))
                app.$count = app.$el.find(".notify-count");
                app.$notificationList = app.$el.find(".notification");
                app.model = new NotifyList;

                that._bindNotificationItems(app);
            };

            this.fetch();

            //this._pulling();//QuiBQ:Tam thoi comment ham nay thay chua hop ly

            this._bindEvents();
            this._registerApiToGlobal();
            this._registerTriggers();
        },

        //#region Initialize

        fetch: function () {
            var that = this;
            $.ajax({
                url: urlSettings.fetch,
                success: function (data) {
                    success(data);
                }
            });
            var success = function (data) {
                if (data && data.length > 0) {
                    that.lastId = _.last(data).NotificationId;
                    ////_.each(data, function (nNotify) {
                    ////    that._removeSimilarBeforeAdd(nNotify);
                    //});

                    that.model.add(data);
                }
            };
        },

        _pulling: function () {
            var that = this;

            // Chổ này đang để tạm vậy
            // Cần đưa vào service
            setInterval(function () {
                $.ajax({
                    url: urlSettings.pulling
                });
            }, 1 * 60 * 1000);

            // 1ph sẽ query lên server lấy notify mới một lần.
            setInterval(function () {
                that.fetch();
            }, 1 * 60 * 1000);
        },

        _getNotificationConfig: function (success) {
            var that = this;
            var config = that.settings;
            if (!config) {
                success(null);
                return;
            }

            var result = [];

            if (config.hasshowdocumentnotify) {
                var egovApp = defaultConfig.egov;
                egovApp.isActive = true;
                result[egovApp.name] = egovApp;
            }

            if (config.hasshowmailnotify) {
                var mailApp = defaultConfig.mail;
                mailApp.isActive = true;
                mailApp.followFolders = config.mailfoldernotify === undefined ? [] : config.mailfoldernotify.toLowerCase().split(',');
                result[mailApp.name] = mailApp;
            }

            if (config.hasshowchatnotify) {
                var chatApp = defaultConfig.chat;
                chatApp.isActive = true;
                result[chatApp.name] = chatApp;
            }

            that.RemoveRead = config.removeread;
            that.HasShowDesktop = config.hasshowdesktop;
            that.HasPlaySound = config.hasplaysound;

            success(result);
            return;
        },

        //#endregion

        //#region Notification List

        _bindNotificationItems: function (currentApp) {
            var that = this, notifyItems = [];
            currentApp.$notificationList.empty();

            this._changeNotifyTotal(currentApp);

            this.model.each(function (n) {
                if (n.get("AppName") == currentApp.name) {
                    notifyItems.push(n.toJSON());
                }
            });

            notifyItems = notifyItems.reverse();
            currentApp.$notificationList.html(that._parseNotifyItems(notifyItems));
        },

        _showNotificationLists: function (currentApp) {
            currentApp.$el.click();
            currentApp.$el.addClass("open");
        },

        _parseNotifyItems: function (notifyItems) {
            var that = this;
            notifyItems.defaultAvatar = "/AvatarProfile/noavatar.jpg";
            var result = $.tmpl(templateInBell, notifyItems);

            result.click(function (e) {
                that._openNotifyInBell(e);
            });

            result.find(".close-notify").click(function (e) {
                that._remove(e);
            });
            result.find(".avatar").error(function (e) {
                that.imageError(this);
            });

            return result;
        },

        _bindListAndShowDesktop: function (newNotify, hasShowNotify) {
            var currentApp;
            currentApp = this._getApp(newNotify.get("AppName"));
            if (!currentApp) {
                throw "App name không đúng.";
            }

            this._bindNotificationItems(currentApp);
            if (hasShowNotify && newNotify.get("IsNew")) {
                this._showDesktop(newNotify);
            }
        },

        _changeNotifyTotal: function (currentApp) {
            // currentApp.total = currentApp.model.length;

            var unread = this.model.where({ IsNew: true, AppName: currentApp.name }).length;

            currentApp.$count.text(unread > 99 ? "99+" : unread);

            if (unread > 0) {
                currentApp.$count.show();
            } else {
                currentApp.$count.hide();
            }
        },

        _removeNotify: function (notifyModel) {
            var appName = notifyModel.get("AppName");
            var currentApp = this._getApp(appName);

            if (notifyModel.view) {
                notifyModel.view.close();
            }

            this._bindNotificationItems(currentApp);
        },

        //#endregion

        _registerApiToGlobal: function () {
            /*
             * Đăng ký các api ra global cho các ứng dụng khác sử dụng.
             */

            var that = this;

            /*
             * Hàm callback khi click vào notify ở các phần mềm ngoài như eGov Desktop
             */
            window.openNotifyFromExternal = function (data) {
                if (typeof data === "string") {
                    data = JSON.parse(data);
                }

                var notifyModel = that.model.detect(function (n) {
                    return n.get("NotificationId") == data.notificationId;
                });

                if (notifyModel.length == 0) {
                    return;
                }

                var currentApp = that._getApp(notifyModel.get("AppName"));

                that._openNotify(currentApp, notifyModel);
            }

            /*
             * API: Cho phép các app đăng ký thêm 1 loại notification app của mình.
             */
            window.registerNotificationApp = function (notifyConfig) {
                var appName = notifyConfig.AppName;
                var app = that._getApp(appName);
                if (app != undefined) {
                    throw "AppName đã có trong hệ thống";
                }

                that._registedApps[appName] = notifyConfig;
                that.render();
            };

            /*
             * API: cho phép các app đẩy 1 notify lên.
             * Cần gọi window.registerNotificationApp trước khi gọi hàm này.
             */
            window.pushNotifyFromClient = function (notifyModel) {
                var newNotifications = [];
                newNotifications.push(notifyModel);

                that._postToServer(newNotifications);

                if (notifyModel.IsReaded) {
                    // Hiển thị luôn xuống client

                    that._removeSimilarBeforeAdd(notifyModel);

                    if (typeof notifyModel !== "NotifyModel") {
                        notifyModel = new NotifyModel(notifyModel);
                    }
                    that.model.add(notifyModel);
                }
            };

            /*
             * API: cho phép nhận notify từ server qua các thư viện realtime: SignalR,...
             */
            window.pushNotifyFromServer = function (notifyModel) {

                that._removeSimilarBeforeAdd(notifyModel);
                if (typeof notifyModel !== "NotifyModel") {
                    notifyModel = new NotifyModel(notifyModel);
                }
                that.model.add(notifyModel);
            };

            /*
             * API: cho phép nhận notify từ bmail client
             */
            window.addBmailNotify = function (bmailData) {
                var mailApp = that._getApp("bmail");
                if (mailApp == undefined) {
                    return;
                }

                if (!mailApp.isValidData(bmailData)) {
                    return;
                }

                var mailData = {
                    NotificationType: 2,
                    Title: bmailData.fullName,
                    Content: bmailData.title,
                    SenderAvatar: bmailData.avatar,
                    SenderUserName: bmailData.userName,
                    SenderFullName: bmailData.fullName,
                    Date: bmailData.date,
                    ReceiveDate: new Date(),
                    MailId: bmailData.mailId,
                    FolderId: bmailData.folderId,
                    FolderLocation: bmailData.folderLocation
                };

                var notify = {
                    NotificationId: 0,
                    Title: parseMailTitle(bmailData.fullName, bmailData.folderLocation),
                    GroupId: bmailData.mailId,
                    Body: bmailData.title,
                    Avatar: bmailData.avatar,
                    DateCreated: new Date(),
                    IsNew: true,
                    AppName: "bmail",
                    JsonData: JSON.stringify(mailData)
                };

                var newNotifications = [];
                newNotifications.push(notify);

                that._postToServer(newNotifications);
            }

            /*
             * Sự kiện khi click đọc mail ở bmail, gọi ra hàm này để set notify đã đọc.
             */
            window.setMailRead = function (mailId) {
                var mailNotifyModel = that.model.detect(function (n) {
                    return n.get("GroupId") === mailId;
                });

                if (mailNotifyModel != undefined) {
                    mailNotifyModel.set("IsReaded", true);
                }

                // Xem xét có clear luôn những mail có cùng folder không?
                // Clear luôn thì có vẻ không đúng vì người ta chưa đọc.
                // Còn nếu click đọc thì sẽ nhảy vào hàm này để xét trạng thái luôn.
            }

            /*
             * API: Nhận chat notify từ chat client
             * Xử lý tạm: nhẽ ra chat phải tự registerNotificationApp và gọi pushNotifyFromClient mới đúng.
             */
            window.addChatNotify = function (chatData) {
                // chatdata.originalContent: nội dung chat gốc.
                // chatData.content: nội dung chat sau khi parse emoticon

                if (!chatData) return;

                var sender = chatData.sender;
                var receiver = chatData.chatterJid;
                if (!sender || !receiver) return;
                sender = sender.split('@')[0];
                receiver = receiver.split('@')[0];

                var userSend = _.find(egov.setting.allUsers, function (u) { return u.username === sender; });
                var userReceive = _.find(egov.setting.allUsers, function (u) { return u.username === receiver; });

                var notifyData = {
                    NotificationType: 3,
                    Title: userSend.label,
                    Content: chatData.content.split("\r\n")[0],
                    SenderAvatar: userSend.avatar,
                    SenderUserName: userSend.username,
                    SenderFullName: userSend.fullname,
                    Date: chatData.date,
                    ReceiveDate: new Date(),

                    //chat
                    ChatId: chatData.messageId, //
                    ChatterJid: chatData.chatterJid,
                    messageId: chatData.messageId,
                    IsNewChat: true
                };

                //Reply message
                if (notifyData.Content.indexOf("&amp;message=") >= 0) {
                    notifyData.Content = notifyData.Content.split("&amp;message=")[1];
                }

                notifyData.Content = escape(notifyData.Content);

                var isRead = false;
                var isNew = !isRead;
                var content = notifyData.Content;

                var notify = {
                    NotificationId: 0,
                    Title: notifyData.Title,
                    Body: content,
                    alternateContent: notifyData.originalContent,
                    Avatar: userSend.avatar,
                    DateCreated: chatData.date,
                    AppName: "chat",
                    GroupId: chatData.chatterJid,
                    IsViewed: false,
                    UserId: userReceive.value,
                    IsReaded: isRead,
                    IsNew: isNew,
                    JsonData: JSON.stringify(notifyData)
                };

                console.log(notify);

                pushNotifyFromClient(notify);
            }

            /*
             * Sự kiện khi click xem chat
             */
            window.eGovReadChat = function (chatterjId) {
                that.model.each(function (n) {
                    if (n.get("AppName") == "chat" && n.get("GroupId") == chatterjId) {
                        n.set("IsReaded", true);
                    }
                });
            }

            // Dùng để kiểm tra tab eGov trên browser có đang được active không.
            if (/*@cc_on!@*/false) { // hack IE
                document.onfocusin = onFocus;
                document.onfocusout = onBlur;
            } else {
                window.onfocus = onFocus;
                window.onblur = onBlur;
            }
        },

        _registerTriggers: function () {
            var that = this;

            this.model.on("update", function (collection, changed) {
                // Added
                var addedNotifications = changed.changes.added;
                if (addedNotifications.length > 0) {
                    var hasShowDesktop = that.HasShowDesktop;

                    var notifyNews = _.filter(addedNotifications, function (n) {
                        return n.get("IsNew");
                    });

                    if (notifyNews.length > 2) {
                        var appName = notifyNews[0].get("AppName");
                        that._showAnnounMessage(appName, "Bạn có " + notifyNews.length + " thông báo mới.");
                        hasShowDesktop = false;
                    }

                    addedNotifications.forEach(function (nNotify) {
                        nNotify.set("DateCreated", parseServerDate(nNotify.get("DateCreated")).relativeDate());
                        var newId = nNotify.get("NotificationId");

                        if (that.lastId < newId) {
                            that.lastId = newId;
                        }

                        nNotify.set("Body", unescape(nNotify.get("Body")));

                        that._bindListAndShowDesktop(nNotify, hasShowDesktop);
                    });

                    // Kiểm tra nếu notify là documents thì reload lại cây văn bản
                    var hasDocument = _.find(addedNotifications, function (n) {
                        return n.get("AppName") === defaultConfig.egov.name;
                    });

                    if (hasDocument) {
                        that._reloadDocumentTree();
                    }
                }

                //// Removed
                var removedNotifications = changed.changes.removed;
                if (removedNotifications.length > 0) {
                    removedNotifications.forEach(function (nNotify) {
                        that._removeNotify(nNotify);
                        var nId = nNotify.get("NotificationId");
                        if (nId > 0) {
                            $.ajax({
                                url: urlSettings.remove,
                                type: "Post",
                                data: { id: nNotify.get("NotificationId") }
                            });
                        }
                    });
                }
            });

            this.model.on("change:IsReaded", function (notification, changed) {
                if (that.RemoveRead) {
                    that.model.remove(notification);

                    $.ajax({
                        url: urlSettings.remove,
                        type: "Post",
                        data: { id: notification.get("NotificationId") }
                    });
                    return;
                }

                var element = that._findNotificationElement(notification);
                if (element.length > 0) {
                    element.removeClass("unview");
                }

                if (notification.view) {
                    notification.view.close();
                }

                if (notification.get("IsNew")) {
                    notification.set("IsNew", false);

                    $.ajax({
                        url: urlSettings.read,
                        type: "Post",
                        data: { id: notification.get("NotificationId") }
                    });
                }
            });

            this.model.on("change:IsNew", function (notification, changed) {
                var currentApp = that._getApp(notification.get("AppName"));
                that._changeNotifyTotal(currentApp);
            });
        },

        _reloadDocumentTree: function () {
            var frameWindow;
            var documentsApp = this._getApp(defaultConfig.egov.name);
            if (!documentsApp || documentsApp.isActive === false) {
                throw "App name không đúng.";
            }

            var appFrame = window.document.getElementsByName(documentsApp.callback.frameName);
            if (appFrame == null || appFrame.length == 0) {
                return;
            }

            frameWindow = appFrame[0].contentWindow;
            if (frameWindow && frameWindow.egov && frameWindow.egov.pubsub) {
                var event = "tree.reload"; // egov.events.tree.reload;
                frameWindow.egov.pubsub.publish(event);
                frameWindow.egov.pubsub.publish("tree.reloadSelected");
            }
        },

        //#region Events

        _bindEvents: function () {
            var that = this;

            that.$(".btnCloseAll").click(function (e) {
                that._removeAll(e);
            });

            that.$(".btnReadAll").click(function (e) {
                that._readAll(e);
            });
            that.$("ul").scroll(function (e) {
                that._scrollToGetData(e);
            });
        },

        _scrollToGetData: function (e) {
            var that = this;
            var element = $(e.target);
            if (element.scrollTop() + element.innerHeight() >= e.target.scrollHeight) {
                that.getData();
            }
        },

        getData: function () {
            var that = this;
            var load_img = $('<img/>').attr('src', '/Content/Images/ajax-loader-small.gif');
            $.ajax({
                url: urlSettings.getData,
                data: {
                    "pageIndex": pageIndex,
                    "pageSize": pageSize,
                },
                success: function (data) {
                    success(data);
                },
                beforeSend: function () {
                    that.$(".loading-image").append(load_img);
                },
                complete: function () {
                    load_img.remove();
                }
            });
            var success = function (data) {
                if (data.length > 0) {
                    that.model.add(data);
                    pageIndex++;
                }
            };
        },

        _setOlds: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");

            this.model.each(function (notify) {
                if (notify.get("AppName") === appName && notify.get("IsNew") == true) {
                    notify.set("IsNew", false);
                }
            });

            var currentApp = this._getApp(appName);
            this._changeNotifyTotal(currentApp);

            $.ajax({
                url: urlSettings.oldAll,
                type: 'Post',
                data: {
                    appName: appName,
                    lastId: this.lastId
                }
            });
        },

        _removeAll: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");
            var models = this.model.where({ AppName: appName });

            this.model.remove(models);

            $.ajax({
                url: urlSettings.removeAll,
                type: 'Post',
                data: {
                    appName: appName,
                    lastId: this.lastId
                }
            });
        },

        _readAll: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");

            this.model.each(function (notify) {
                if (notify.get("AppName") === appName && notify.get("IsReaded") == false) {
                    notify.set("IsReaded", true);
                }
            });

            $.ajax({
                url: urlSettings.readAll,
                type: 'Post',
                data: {
                    appName: appName,
                    lastId: this.lastId
                }
            });
        },

        _remove: function (e) {
            var target = $(e.target).closest("li");
            var notificationId = target.attr("id");
            var notifyModel = this.model.detect(function (n) {
                return n.get("NotificationId") == notificationId;
            });

            this.model.remove(notifyModel);

            $.ajax({
                url: urlSettings.remove,
                type: "Post",
                data: { id: notificationId }
            });
        },

        //#endregion

        //#region Hiển thị notify desktop

        /*
         * Hiển thị notify thông báo
         */
        _showAnnounMessage: function (appName, body) {
            //Todo: Xem có trường hợp thông báo không thuộc app nào thì thế nào?
            // Có thể chỉ cần truyền Icon chổ này cũng dc.
            var app = this._getApp(appName);

            var notify = new NotifyModel({
                NotificationId: 0,
                Title: "Thông báo",
                Body: body,
                AppName: appName,
                Avatar: app.AnnounIcon,
                OnlyAnnoun: true,
                JsonData: {}
            });

            this._showDesktop(notify);
        },

        /*
         * Xử lý hiển thị notify
         */
        _showDesktop: function (notify) {
            // Chỉ hiển thị notify desktop với notify mới
            if (notify.get("IsNew") == false) {
                return;
            }

            var that = this;
            if (this._isDesktop) {
                notify.Callback = "openNotifyFromExternal";
                notify.view = this._showNotifyInDesktop(notify);
                return;
            }

            if (this._egovTabIsActive(notify.get("AppName"))) {
                notify.view = this._showNotifyInBrowserTab(notify);
                return;
            }

            notify.Callback = function (notify) {
                that._openNotifyDesktop(notify);
            }

            notify.view = new webClientNotify().Instance().show(notify);
        },

        _showNotifyInBrowserTab: function (notify) {
            if (this.HasPlaySound) {
                playAudio();
            }

            // Xử lý hiển thị thông báo inbrowser ở góc ứng dụng.

        },

        _showNotifyInDesktop: function (notify) {
            return new desktopClientNotify().show(notify);
        },

        //#endregion

        //#region Notification Click Callback

        /*
         * Sự kiện click vào notify
         */
        _openNotifyDesktop: function (notify) {
            if (notify == null) {
                return;
            }

            var currentApp = this._getApp(notify.get("AppName"));
            if (!currentApp || currentApp.isActive === false) {
                throw "App name không đúng.";
            }

            if (notify.get("OnlyAnnoun")) {
                this._activeTab();
                // this._showNotificationLists(currentApp);
                return;
            }

            var currentApp = this._getApp(notify.get("AppName"));
            if (!currentApp || currentApp.isActive === false) {
                throw "App name không đúng.";
            }

            this._openNotify(currentApp, notify);
        },

        _openNotifyInBell: function (e) {
            var target = $(e.target).closest("li");
            var appName = target.attr("app");
            var notificationId = target.attr("id");

            var currentApp = this._getApp(appName);
            var notifyModel = this.model.detect(function (n) {
                return n.get("NotificationId") == notificationId;
            });

            this._openNotify(currentApp, notifyModel);
        },

        _openNotify: function (currentApp, notifyModel) {
            if (!notifyModel) {
                return;
            }

            this._activeTab();
            notifyModel.set("IsReaded", true);

            this._notifyCallback(currentApp, notifyModel);
        },

        _notifyCallback: function (currentApp, notify) {
            var frameWindow, callback, frameLoadedCallback;

            if (!currentApp.callback || !currentApp.callback.api || currentApp.callback.api === "") {
                return;
            }

            frameLoadedCallback = function (frameWindow) {
                if (frameWindow === undefined) {
                    return;
                }

                callback = frameWindow[currentApp.callback.api];
                if (isFunction(callback)) {
                    callback(notify.get("JsonData"));
                }
            };

            if (currentApp.callback.frameName == "") {
                frameWindow = window;
                frameLoadedCallback(frameWindow);
            } else {
                mainApps.openApp(currentApp.name, function () {
                    var appFrame = window.document.getElementsByName(currentApp.callback.frameName);
                    if (appFrame == null || appFrame.length == 0) {
                        return;
                    }

                    frameWindow = appFrame[0].contentWindow;

                    frameLoadedCallback(frameWindow);
                });
            }
        },

        //#endregion

        //#region Update Server

        _postToServer: function (addeds) {
            if (addeds.length === 0) {
                return;
            }



            $.ajax({
                url: urlSettings.update,
                type: "POST",
                data: { addeds: JSON.stringify(addeds) },
                success: function (result) { },
                error: function () {

                }
            });
        },

        //#endregion

        //#region Hàm khác

        /*
         * Trả về giá trị xác định đang active eGov tab
         */
        _egovTabIsActive: function (appName) {
            var chromeTabIsFocus = window.document.isFocused === true;

            // Nếu không focus vào tab eGov thì hiển thị notify desktop
            if (!chromeTabIsFocus) {
                return false;
            }

            // nếu đang focus vào eGov mà không focus vào app, thì hiển thị notify déktop
            var currentApp = this._getApp(appName);
            if (currentApp && !currentApp.isFocus()) {
                return false;
            }

            // Không hiển thị notify desktop
            return true;
        },

        _activeTab: function () {
            window.focus();
        },

        _getApp: function (appName) {
            return this._registedApps[appName];
        },

        _findNotificationElement: function (notifyModel) {
            var appName = notifyModel.get("AppName");
            var notificationId = notifyModel.get("NotificationId");
            var currentApp = this._getApp(appName);
            var element = currentApp.$notificationList.find("#" + notificationId);

            return element;
        },

        _removeSimilarBeforeAdd: function (nNotify) {
            var similars = this.model.select(function (n) {
                return n.get("AppName") == nNotify.AppName &&
                        n.get("GroupId") == nNotify.GroupId;
            });

            if (similars.length > 0) {
                this.model.remove(similars);
            }
        },

        imageError: function (evt) {
            $(evt).attr("src", "/AvatarProfile/noavatar.jpg");
        }

        //#endregion

        //#region Test

        , test: function () {
            this.testInterval;
            var that = this;
            var groups = ["tienbv", "dambv", "tamdn", "dunghv", "dungnvl", "phucnhb", "cuongnt", "taimv"];
            var contents = ["Nghẹn ngào giây phút ta chấp nhận sống không cần nhau",
                            "Chẳng khác chi trái đất này làm sao tồn tại không có mặt trời",
                            "Chỉ biết lặng nhìn em quay lưng bước đi lòng anh thắt lại",
                            "Nghĩ đến mình sẽ không gặp lại.",
                            "Tình yêu đâu phải ai cũng may mắn tìm được nhau",
                            "Chẳng giống như chúng ta tìm được nhau rồi lại hoang phí duyên trời",
                            "Lắng Nghe Nước Mắt lyrics on ChiaSeNhac.vn",
                            "Tại sao phải rời xa nhau mãi mãi, biết đến khi nào chúng ta",
                            "Nhận ra chẳng thể quên được nhau."]

            this.testInterval = setInterval(function () {
                var groupId = _.sample(groups);
                var content = _.sample(contents);

                window.pushNotifyFromClient({
                    NotificationId: 0,
                    Title: groupId,
                    GroupId: groupId,
                    Body: content,
                    Avatar: "https://danhba.bkav.com/avatars/" + groupId + ".bmp",
                    DateCreated: new Date,
                    AppName: "chat",
                    IsNew: true,
                    JsonData: '{DocumentCopyId: 1778,Compendium: "Phạm Quang Ba (Phòng Khám Nha khoa Sài Gòn Colgate 2) "}'
                });
            }, 1000 * 5 * 1);
        },

        stopTest: function () {
            clearInterval(this.testInterval);
        }

        //#endregion
    });

    function onBlur() {
        window.document.isFocused = false;
    }
    function onFocus() {
        window.document.isFocused = true;
    }
    function isDesktopApp() {
        var result = false;
        try {
            result = typeof window.external.CB_ShowNotify == "function";
        } catch (e) {
            console.log(e);
        }

        return result;
    }
    function isFunction(func) {
        return typeof func === "function";
    }
    function playAudio(url) {
        try {
            var that = this;
            if (!url) {
                url = '../Content/Sound/notify.wav';
            }

            var audio = new Audio(url);
            audio.play();
        }
        catch (ex) { }
    }
    function parseServerDate(date) {
        if (typeof date === "string") {
            return new Date(date.replace(/T/i, ' '));
        }

        return new Date(date);
    }
    function parseMailTitle(title, folderPath) {
        if (folderPath == undefined || folderPath === "") {
            return title;
        }

        var folder = _.last(folderPath.split('/'));

        return title + " đã gửi vào " + parseMailFolderName(folder);
    }
    function parseMailFolderName(name) {
        name = name.toLowerCase();
        if (name === "inbox") {
            return "Hộp thư đến";
        }

        if (name === "sent") {
            return "Hộp thư đi";
        }

        if (name === "junk") {
            return "Thư đã xóa";
        }

        if (name === "drafts") {
            return "Thư rác";
        }

        return name;
    }


    window.eGovNotification = EgovNotificationView;

})(window.jQuery, window._, window.Backbone, window.notify);
(function ($, eGovNotify, helper) {
    "use strict";

    // Tên class có kế thừa từ : Hub tương ứng trên server.
    // var hubs = $.connection.notificationHubs;
    var hubs = $.connection.hubs;

    var frameName = "documents";

    $.connection.hub.logging = false;

    $.connection.hub.start(function () {

    });

    $(window).off('unload').on('unload', function (e) {
        $.connection.hub.disconnected(function () {
            console.log('disconnected!');
        });

        if (helper)
            helper.destroyEvent(e);
    });

    $.connection.hub.reconnected(function (e) {
        try {
            console.log('reconnected!');
        }
        catch (ex) {
            console.log("Reconnected error!");
        }
    });

    $.connection.hub.error(function (error) {
        ///<summary>
        /// Hàm gọi khi hub xảy ra lỗi
        ///</summary>
        if (error) {
            try {
                console.log(error);
               // helper.getContentWindow(frameName).location.reload(true);
            }
            catch (ex) {
                console.log(ex.message);
            }
        }
    });

    //Nhận thông báo từ server
    hubs.client.notification = function (data) {
        ///<summary>
        /// Truyền đối tượng notify từ server xuống cho client
        ///</summary>
        ///<param name = "data">Đối tượng notify trả về cho client</param>
        try {
            if (!egov.isMobile && currentApp && currentApp.name == "mail" && document.hasFocus()) {
                return;
            }

            if (typeof window.pushNotifyFromServer === "function") {
                window.pushNotifyFromServer(data);
            }
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    hubs.client.mailNotification = function (data) {
        ///<summary>
        /// Truyền đối tượng notify từ server xuống cho client
        ///</summary>
        ///<param name = "data">Đối tượng notify trả về cho client</param>

        try {
            window.bmailNotify.addToModel(JSON.parse(data.JsonData));
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    //Cập nhật trạng thái notify của các connections khác của người dung
    hubs.client.updateViewAllNotify = function () {
        ///<summary>
        ///Cập nhật trạng thái xem tất cả văn bản
        ///</summary>
        try {
            window.eGovNotify.setViewAll();
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    //Cập nhật trạng thái notify và các notify của connections khác
    hubs.client.updateViewNotify = function (notifyId) {
        ///<summary>
        ///Cập nhật trạng thái xem của văn bản
        ///</summary>
        try {
            eGovNotify.reBindNotify(notifyId);
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

    hubs.client.reloadPage = function () {
        ///<summary>
        /// Reload lại trang
        ///</summary>
        try {
            document.location.reload(true);
        }
        catch (ex) {
            console.log(ex.message);
        }
    }

})(window.jQuery, window.eGovNotify, window.helper);
(function ($) {

    var $menuItems = $(".menu-items");
    var $divApps = $(".site-content");
    var qtip = function ($selector) {
        $selector.qtip({
            position: {
                at: "center right",
                my: "left center"
            }
        });
    }

    var runCallback = function (callback) {
        if (typeof callback == "function") {
            callback();
        }
    }

    function isTool() {
        if (!egov.utilities) {
            require(["util/bkav.utilities"], function () {
                return egov.utilities.url.getQueryStringValue('client') === 'tool';
            });
        }
        else {
            return egov.utilities.url.getQueryStringValue('client') === 'tool'
        }
    }

    qtip($("#btnNewApp"));

    var menuAppTemplate = '<a href="#" class="list-group-item{{if isCustomApp}} customapp{{/if}}" id="${name}" data-ng-app="${name}" title="${title}"><div class="btnDelete" appid="${id}">x</div><img src="${icon}" /></a>';

    window.mainApps = {
        init: function (config) {
            if (config instanceof Array) {
                for (var i = 0; i < config.length; i++) {
                    mainApps.initApp(config[i]);
                }
            }
            else {
                mainApps.initApp(config);
            }
        },

        isTool: isTool(),

        initApp: function (config) {
            var defaultConfig = {
                id: "",
                name: "",
                title: "",
                appUrl: "",
                icon: "",
                isActive: true,
                isMainApp: false,
                isBackgroundApp: false,
                isCustomApp: false
            };

            var thisConfig = $.extend(defaultConfig, config);

            var $thisApp;
            if (thisConfig.isActive) {
                $thisApp = $menuItems.children("[data-ng-app='" + thisConfig.name + "']");
                if ($thisApp.length == 0) {
                    $thisApp = $.tmpl(menuAppTemplate, thisConfig);
                    $menuItems.children().last().before($thisApp);
                } else {
                    $thisApp.removeClass("hidden");
                }
                thisConfig.$elItem = $thisApp;
                qtip($thisApp);
                mainApps.setConfig(thisConfig);

                if (thisConfig.isMainApp) {
                    mainApps.openApp(thisConfig);
                }

                if (thisConfig.isBackgroundApp) {
                    setTimeout(function () {
                        mainApps.addDivApp(thisConfig.name, thisConfig.appUrl);
                    }, 5 * 1000);
                }

                var that = mainApps;
                $thisApp.on("click", function (e) {
                    that.openApp(thisConfig);
                });
            }
            return $thisApp;
        },

        getConfig: function (name) {
            return mainApps[name];
        },

        setConfig: function (config) {
            mainApps[config.name] = config
        },

        deleteAllLocalStorage: function () {
            ///<summary>
            /// Xóa toàn bộ localstorage
            ///</summary>
            if ('localStorage' in window && window['localStorage'] !== null) {
                window['localStorage'].clear();
            }
        },

        destroyEvent: function (e) {
            if (!e) {
                return;
            }
            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                // fix cho ie 8
                e.returnValue = false;
            }
            e.stopPropagation();
        },

        openApp: function (config, callback) {
            var that = this, divApp, openAppSuccess;
            if (typeof config == "string") {
                config = mainApps.getConfig(config);
                if (!config) {
                    return;
                }
            }

            openAppSuccess = function () {
                try {
                    window.currentApp = that.getContentWindow(config.name);
                    window.currentApp.name = config.name;
                    mainApps.changeSearchApp(config.name);
                    localStorage.setItem("appCurrentOpen", config.name);
                } catch (ex) {
                    // log(ex);
                }
                runCallback(callback);
            };

            $menuItems.children().removeClass("active");
            $menuItems.children("." + config.name).addClass("active");

            if (config.$el != undefined && config.$el.length > 0) {
                divApp = config.$el;
                openAppSuccess();
            } else {
                divApp = mainApps.addDivApp(config.name, config.appUrl, openAppSuccess);
            }

            // Ẩn hiện chat
            if (egov.chatDesktop) {
                egov.pubsub && egov.pubsub.publish('chatdesktop.active', config.hasShowChat);
            }

            divApp.siblings().hide();
            divApp.show();
        },

        getContentWindow: function (name) {
            try {
                return $divApps.find("iframe#" + name)[0].contentWindow;
            } catch (e) {

            }
        },

        addDivApp: function (name, appUrl, callback) {
            var divApp = $divApps.children("." + name);
            if (divApp.length == 0) {
                divApp = $("<div class='" + name + "' style='display: none;' id='div-" + name + "' />");
                var frame = $('<iframe>').attr('name', name).attr('id', name);
                frame.attr("src", appUrl);
                divApp.append(frame);
                $divApps.append(divApp);
                $(frame).ready(function () {
                    runCallback(callback);
                });
            } else {
                runCallback(callback);
            }

            mainApps[name].$el = divApp;

            return divApp;
        },

        changeSearchApp: function (name) {
            switch (name) {
                case "documents":
                    $('.form-search').show();
                    $(".search-type-btn").show();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchDocument"));
                    break;
                case "bmail":
                    $('.form-search').show();
                    $(".search-type-btn").hide();
                    $('#MainSearchQuery').attr('placeholder', getResource("egov.resources.searchMail"));
                    break;
                default:
                    $('.form-search').hide();
                    break;
            }
        },
    };

    var mailType =
    {
        Bmail: 1,
        Gmail: 2,
        Yahoo: 3,
        MailExchange: 4,
        MDaemon: 5
    };

    var getSettingFromServer = function (callback) {
        $.ajax({
            url: '/Home/GetConnectionSettings'
        }).success(function (result) {
            if (localStorage) {
                localStorage.setItem("connectionSettings", JSON.stringify(result));
            }
            callback(result);
        });
    };

    var loadMenu = function (connection) {
        if (mailType.Bmail == connection.MailType) {
            connection.BmailLink = connection.BmailLink + "?egov=1" + "&domain=" + connection.ParentDomain;
            connection.CalendarLink = connection.BmailLink + "?egov=1&calendar=1" + "&domain=" + connection.ParentDomain;
        }
        else {
            var mailUrl = localStorage.getItem("mdaemonUrl");
            if (mailUrl != undefined) {
                connection.BmailLink = mailUrl;
                connection.CalendarLink = mailUrl;
            }
        }

        systemApps = systemApps.sort(function (a, b) { return a.Order - b.Order; });
        for (var i in systemApps) {
            var app = systemApps[i];
            var appName = app.Name;
            if (appName === "bmail" || appName === "chat") {
                if (app.AppUrl) {
                    if (app.AppUrl.indexOf("?") > 0) {
                        app.AppUrl += "&domain=" + connection.ParentDomain;
                    } else {
                        app.AppUrl += "?domain=" + connection.ParentDomain;
                    }
                }
            }

            new mainApps.init({
                id: app.Id,
                name: app.Name,
                title: app.Title,
                icon: app.IconUrl,
                appUrl: app.AppUrl,
                isBackgroundApp: app.IsBackgroundApp,
                isMainApp: app.IsDefaultApp,
                isActive: app.IsActived,
                hasShowChat: (appName === "documents" || appName === "bmail")
            });
        }

        //document.domain = connection.ParentDomain;
    }

    if (localStorage) {
        egov = egov || {};
        var localConnection = localStorage.getItem("connectionSettings");
        if (!localConnection) {
            getSettingFromServer(function (connection) {
                egov.connections = connection;
                loadMenu(connection);
            })
        } else {
            loadMenu(JSON.parse(localConnection));
            egov.connections = JSON.parse(localConnection);
        }
    }

})(window.jQuery);


/// <reference path="../chat/chat_desktop.js" />

(function () {
    var documentFrame,
        isLoadingDoctypes = false,
        _bmailAppName = "bmail",
        _chatAppName = "chat",
        _documentAppName = "documents",
        _mainApp = window.mainApps,
        _keyCode = {
            enter: 13,
            f5: 116
        };

    var pinedDoctypeTemplate = '<li class="newDocument" id="${DocTypeId}" name="${DocTypeName}"><a href="#" class="create-new-document" id="${DocTypeId}" name="${DocTypeName}"><img src="${iconName}" style="width: 25px;"> ${DocTypeName} <img class="pinDocType isPined" src="/Content/Images/Icons/unpin.png" title="Bỏ gắn lên đầu danh sách" id="${DocTypeId}"></a></li>';
    var unpinDoctypeTemplate = '<li class="newDocument" id="${DocTypeId}" name="${DocTypeName}"><a href="#" class="create-new-document" id="${DocTypeId}" name="${DocTypeName}"><img src="${iconName}" style="width: 25px;"> ${DocTypeName} <img class="pinDocType" src="/Content/Images/Icons/pin.png" title="Gắn lên trên đầu danh sách" id="${DocTypeId}"></a></li>';
    var docfieldTemplate = '<li class="docfield-groups"><a href="#"><span class="icon-arrow icon-arrow-right7"></span><span>Lĩnh vực ${DocFieldName}</span><span class="docfieldCount"> (${Count})</span></a><ul class="subDoctype" data-docfieldid="${DocFieldName}" style=""></ul></li>';

    var egovMain = {
        eGovSso: window.eGovSso,
        currentUserName: window.currentUserName,

        init: function () {
            _layoutMain();

            // Đăng xuất
            $('#logout, .btnlogout').on("click", function (e) {
                _mainApp.destroyEvent(e);
                _logout();
            });

            $(".avatar").error(function () {
                $(this).attr("src", "/AvatarProfile/noavatar.jpg");
            });

            $('.app-size').click(function () {
                var appSize = $(this).attr("data-value");
                var appSizeValue = appSize === "small-size" ? 0 : (appSize === "medium-size" ? 1 : 2);
                _changeAppSize(appSizeValue);
            });

            // Thay đổi xem preview
            $('li > a.preview').click(function () {
                var preview = $(this).attr("data-value");
                var documentFrame = mainApps.getContentWindow(_documentAppName);
                if (documentFrame) {
                    documentFrame.egov.views.home.showPreview(preview, true);
                }
            });

            $('.create-new').click(function () {
                _createNewMail();
            });

            $("#resetSystem").on("click", function (e) {
                //_saveChangeBeforeUnload(e, function () {
                //    _resetCache(e);
                //});
                _resetCache(e);
            });

            $("#MainSearchQuery").on("click", function () {
                // Bôi đen chuỗi tìm kiếm khi click vào để tiện xóa.
                $(this)[0].setSelectionRange(0, $(this).val().length);
            });

            $('.form-group .search-btn').click(function () {
                _doSearch();
            });

            $("#installPlugin").on("click", function () {
                _showInstallPlugin();
            });

            $(".search-type-btn a[role=menuitem]").click(function () {
                $("#MainSearchType").val(parseInt($(this).data("value")));
                $("#MainSearchQuery").focus();
            });

            $('#absentDialog .datetime').datepicker({
                dateFormat: "dd/mm",
                onSelect: function (dateText, inst) {
                    $(inst.input).val(new Date().format('HH:mm ') + dateText);
                }
            });

            $("#saveAbsent").click(function () {
                _saveAbsent();
            });

            $("#cancelAbsent").click(function () {
                $('#absentDialog .datetime').val('');
            });
        },

        initKeyPress: function () {
            $(document).keyup(function (e) {
                /*
                 * Lọc danh sách văn bản khi gõ vào ô tìm kiếm.
                 */
                var TextBoxSearchIsFocusing = $("#MainSearchQuery").is(':focus');
                if (TextBoxSearchIsFocusing) {
                    if (e.keyCode === _keyCode.enter) {
                        _doSearch();
                    }
                    _filterDocumentInClient($("#MainSearchQuery").val());
                }
            });

            $(window.document).keydown(function (e) {
                var keycode = e.keyCode;
                if (keycode === _keyCode.f5) {
                    _saveChangeBeforeUnload(e, function () {
                        window.location.reload();
                    });
                }
            });
        },

        initDoctype: function (doctypes) {
            _renderDoctypes(doctypes);
            _bindDoctypesEvent();
        },

        initChat: function (settings) {
            return;
            egov = egov || {};
            egov.setting = settings;

            require(['/scripts/bkav.egov/views/chat/chat_desktop.js'], function (DesktopChat) {
                new DesktopChat();
            });

            $('#startAbsent').val(settings.userSetting.StartAbsent);
            $('#endAbsent').val(settings.userSetting.EndAbsent);
        },

        initNotifications: function (settings) {
            if (!eGovNotification) {
                return;
            }

            egov.eGovNotification = new eGovNotification({ settings: settings });
        },

        registerToGlobal: function () {

            window.readNewMail = function (mailData) {
                /*
                 * Hàm mở mail khi notify sử dụng cho Bmail. Lưu ý, ko đổi tên hàm này.
                 */

                var mailId, folderId;
                if (typeof mailData === "string") {
                    mailData = JSON.parse(mailData);
                }

                mailId = mailData.MailId;
                folderId = mailData.FolderId;

                _mainApp.openApp(_bmailAppName, function () {
                    var frameApp = $("iframe#bmail")[0].contentWindow;
                    if (_.isFunction(frameApp.readNewMail)) {
                        frameApp.readNewMail(mailId, folderId);
                    }
                });
            }

            window.createNewMail = function () {
                _createNewMail();
            }

            window.openChat = function (chatData) {
                /*
                 * Hàm mở notify chat dùng cho chat bmail. Lưu ý, không đổi tên.
                 */
                if (egov.chatDesktop) {
                    // nếu có kích hoạt chat desktop thì bỏ qua xử lý mở menu chat
                    return;
                }

                var chatterJid;
                if (typeof chatData === "string") {
                    chatData = JSON.parse(chatData);
                }

                chatterJid = chatData.ChatterJid;

                _mainApp.openApp(_chatAppName, function () {
                    var frameApp = $("iframe#chat")[0].contentWindow;
                    if (frameApp.btalk && frameApp.btalk.APPVIEW) {
                        frameApp.btalk.APPVIEW.notificationClick({ chatterJid: chatterJid });
                    }
                });
            }

            window.addDocument = function (id, name, contentHTML, attachmentLinks) {
                _createNewDocument(id, name, contentHTML, attachmentLinks);
            }

            window.openDocument = function (id, name) {
                mainApps.openApp(_documentAppName, function () {
                    setTimeout(function () {
                        var egov = currentApp.egov;
                        egov.views.home.tab.openDocument(id, name, true);
                    }, 400)
                });
            }

            window.getDeptAndUsers = function (success) {
                /*
                 * Trả về danh sách cấu trúc phòng ban - người dùng, sử dụng cho các bên bmail, chat, calendar.
                 */
                if (!_.isFunction(success)) {
                    return;
                }

                var egovFrame = mainApps.getContentWindow(_documentAppName);
                if (!egovFrame || !egovFrame.egov || !egovFrame.egov.dataManager) {
                    success([]);
                    return;
                }

                var parseResult = function (users, depts, userDeptPoses) {
                    var results = [];
                    _.each(depts, function (dept) {
                        var userDepts = _.filter(userDeptPoses, function (userDeptPos) {
                            return userDeptPos.departmentid === dept.value;
                        });
                        var usersInDept = _.filter(users, function (user) {
                            return _.pluck(userDepts, "userid").indexOf(user.value) > -1;
                        });
                        var usersForm = [];
                        _.each(usersInDept, function (u) {
                            usersForm.push(
                                {
                                    Username: u.username,
                                    FullName: u.fullname,
                                });
                        });
                        results.push({
                            DepartmentId: dept.value,
                            ParentId: dept.parentid,
                            DepartmentName: dept.data,
                            DepartmentIdExt: dept.idext,
                            DepartmentPath: dept.label,
                            Order: dept.order,
                            Level: dept.level,
                            Users: usersForm
                        });
                    });

                    success(JSON.stringify(results));
                };

                egovFrame.egov.dataManager.getAllUsers({
                    success: function (users) {
                        egovFrame.egov.dataManager.getAllDept({
                            success: function (depts) {
                                egovFrame.egov.dataManager.getAllUserDeptPosition({
                                    success: function (userDeptPoses) {
                                        parseResult(users, depts, userDeptPoses);
                                    }
                                })
                            }
                        });
                    }
                });
            }

            window.logoutDesktop = function () {
                if (!window.external || !_.isFunction(window.external.CB_LoginSuccess)) {
                    return;
                }

                var data = [];
                var loginObj = {
                    user: window.currentUserName,
                    password: "",
                    remember: "true"
                }

                data.push(loginObj);

                var myJsonString = JSON.stringify(data);
                window.external.CB_LoginSuccess(myJsonString);
            }

            window.onSearchDocumentBegin = function () {
                _doSearch();
            }

            window.saveChangeBeforeUnload = function (e) {
                _saveChangeBeforeUnload(e, function () {
                    window.location.reload();
                });
            }

            window.changeBmailResource = function () { }

            window.resetApplication = function () {
                _resetCache();
            }
        },

        showTooltip: function () {
            var qtip = function ($selector) {
                $selector.qtip({
                    position: {
                        at: "center right",
                        my: "left center"
                    }
                });
            }

            var $settingItems = $(".setting-items > a");
            $settingItems.each(function () {
                qtip($(this));
            });

        }
    };

    $(function () {
        egovMain.init();

        egovMain.registerToGlobal();

        egovMain.initKeyPress();

        egovMain.showTooltip();

        window.egovMain = egovMain;
    });

    // #region _private methods

    function _logout() {
        var cookies = document.cookie.split(";");

        for (var i = 0; i < cookies.length; i++) {
            var eqPos = cookies[i].indexOf("=");
            var name = eqPos > -1 ? cookies[i].substr(0, eqPos) : cookies[i];
            name = name.trim();
            if (name === "bkavAuthen") {
                $.cookie(name, "", { domain: document.domain, path: "/", expires: -1 });
                $.cookie(name, "", { expires: -1 });
            }
        }

        logoutDesktop();

        // Tạm bỏ vào để demo trên nhiều tài khoản
        $(window).on('unload', function (e) {
            var egovFrame = _mainApp.getContentWindow(_documentAppName);
            if (egovFrame && egovFrame.egov && egovFrame.egov.dataManager) {
                egovFrame.egov.dataManager.reset({
                    success: function () {
                        window.location.reload(true);
                    }
                });
            }

            _mainApp.deleteAllLocalStorage();
        });

        if (egovMain.currentUserName != undefined) {
            $.get(egovMain.eGovSso + '/User/Logout?userName=' + egovMain.currentUserName, {});
        }

        window.document.location.href = "/account/logout";
    }

    function _layoutMain() {
        var contentLayout = $('.panel-container').layout({
            resizable: false,
            closable: true,
            spacing_closed: 0,
            spacing_open: 0,
            west__spacing_open: 0,
            west__size: 40,
            west__zIndex: 1,
            west__paneSelector: ".west-panel",
            center__paneSelector: ".center-panel"
        });

        //$(".west-panel").hide();

        $('.center-panel').layout({
            resizable: false,
            closable: true,
            spacing_closed: 0,
            spacing_open: 0,
            north__spacing_open: 0,
            north__size: 45,
            north__zIndex: 1,
            north__paneSelector: ".site-header",
            center__paneSelector: ".site-center"
        });
    }

    function _createNewMail() {
        _mainApp.openApp(_bmailAppName, function () {
            window.currentApp.$("#composeMailTab").click();
        });
    }

    function _createNewDocument(id, name, contentHTML, attachmentLinks) {
        /// <summary>
        /// Tạo mới công văn
        /// </summary>
        /// <param name="id">Id công văn</param>
        /// <param name="name">Tên công văn</param>
        /// <param name="contentHTML">nội dung HTML muốn truyền</param>
        _mainApp.openApp(_documentAppName, function () {
            var egovFrame = currentApp,
                userCurrent = egovFrame.egov.setting.userName,
                att = [];

            if (attachmentLinks == undefined || attachmentLinks.length === 0) {
                egovFrame.egov.views.home.tab.addDocument(id, name, null, false, null, contentHTML);
                return;
            }

            $.each(attachmentLinks, function (index, item) {
                att.push({ FileName: item.filename, Url: item.filepart });
            });

            $.ajax({
                type: "POST",
                url: "Attachment/UploadAttachmentInLink",
                data: {
                    urls: JSON.stringify(att)
                },
                success: function (result) {
                    var attachments = [];
                    $.each(result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.message.error(file.name + ": " + file.error);
                        } else {
                            var newAttachment = {
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: file.extension,
                                Versions: [{
                                    Version: 1,
                                    User: userCurrent
                                }],
                                isNew: true
                            };
                            attachments.push(newAttachment);
                        }
                    });
                    egovFrame.egov.views.home.tab.addDocument(id, name, null, false, attachments, contentHTML);
                },
                error: function () {
                }
            });
        });
    }

    function _resetCache(e) {
        e && _mainApp.destroyEvent(e);
        var egovFrame = _mainApp.getContentWindow(_documentAppName);
        $(window).on('unload', function (e) {
            _mainApp.deleteAllLocalStorage();
        });

        if (egovFrame && egovFrame.egov && egovFrame.egov.dataManager) {
            egovFrame.egov.dataManager.reset({
                success: function () {
                    window.location.reload(true);
                }
            });
        } else {
            window.location.reload(true);
        }
    }

    function _filterDocumentInClient(filterValue) {
        ///<summary>
        /// Lọc danh sách văn bản hiện tại khi gõ vào ô tìm kiếm.
        ///</summary>

        var $appSelected = $('.menu-items a.active');
        if ($appSelected.length === 0 || $appSelected.attr('data-ng-app') !== _documentAppName) {
            return;
        }

        if (!currentApp || !currentApp.egov) {
            return;
        }

        currentApp.egov.views.home.documents.clientQuickSearch(filterValue);
    }

    function _doSearch() {
        var searchQuery = $("#MainSearchQuery").val();
        var searchType = parseInt($("#MainSearchType").val());
        var $appSelected = $('.menu-items a.active');
        if ($appSelected.length === 0) {
            return;
        }

        var ngApp = $appSelected.attr('data-ng-app');
        if (ngApp === _documentAppName) {
            if (!currentApp || !currentApp.egov) {
                return;
            }

            currentApp.egov.views.home.tab.addSearch(searchQuery, searchType);
        }
        else if (ngApp.indexOf(_bmailAppName) >= 0) {
            currentApp.EgovLibrary.doSearch(searchQuery);
        }
    }

    function _saveChangeBeforeUnload(e, callback) {
        var hasChange = false;
        var documentframe = mainApps.getContentWindow(_documentAppName);
        if (documentframe) {
            hasChange = documentframe.egov.views.home.tab.hasChangeContent();
            if (hasChange) {
                documentframe.egov.views.home.tab.closeAll(function () {
                    callback();
                });
                e.preventDefault();
                return;
            } else {
                callback();
            }
        } else {
            callback();
        }
    }

    function _showInstallPlugin() {
        var eGdocument = document.getElementById(_documentAppName).contentWindow;
        eGdocument.showInstallPlugin();
    }

    function _saveAbsent() {
        var hasAbsent = false;
        var start = $("#startAbsent").val();
        var end = $("#endAbsent").val();

        if (start != '' && end != '') {
            hasAbsent = true;
        }

        $.ajax({
            url: 'Account/SaveAbsent',
            type: "post",
            data: {
                hasAbsent: hasAbsent,
                startAbsent: start,
                endAbsent: end
            },
            success: function (result) {
                var status = result.hasAbsent ? String.format("Tôi sẽ vắng mặt từ: {0} đến {1}", result.data.start, result.data.end)
                                : "";
                egov.pubsub.publish("chat.changestatus", status);
            }
        })
    }

    //#region Hiển thị loại văn bản cho khởi tạo

    function _renderDoctypes(doctypes) {
        var $doctypeList = $('.new-doctypes'),
           pinnedDocTypes,
           commonDoctypes,
           $pinnedDocTypes,
           $commonDoctypes,
           docFieldIds;

        $doctypeList.find(".newDocument").remove();
        $pinnedDocTypes = $(".pinnedDocTypes");
        $commonDoctypes = $(".commonDocTypes");

        if (doctypes === null || doctypes.length === 0) {
            return;
        }

        pinnedDocTypes = _.filter(doctypes, function (item) {
            return item.Pinned;
        });
        _renderDoctypeGroup(pinnedDocTypes, $pinnedDocTypes, true);
        $pinnedDocTypes.append("<li class='divider'></li>");

        // TienBV: pin những cái được đánh dấu lên lên, còn lại vẫn xử lý bt
        commonDoctypes = doctypes;

        //Hiển thị theo lĩnh vực
        docFieldIds = _.uniq(_.pluck(commonDoctypes, "DocFieldId"));
        
        var docFieldGroups = _.groupBy(commonDoctypes, function (doctype) {
            return doctype.DocFieldName;
        });
        _.each(docFieldGroups, function (doctypes, docfieldName) {
            var docFieldElement = $.tmpl(docfieldTemplate, { DocFieldName: docfieldName, Count: doctypes.length });
            $commonDoctypes.append(docFieldElement);

            var $subDoctype = docFieldElement.find(".subDoctype");
            _renderDoctypeGroup(doctypes, $subDoctype, false);
            $subDoctype.append("<li class='divider'></li>");
            $subDoctype.hide();
        });

        _displayDivider();
    }

    function _renderDoctypeGroup(doctypes, groupElement, isPined) {
        groupElement.empty();
        if (doctypes.length === 0) {
            return;
        }

        doctypes = _.sortBy(doctypes, function (dt) {
            return dt.DocTypeName;
        });

        groupElement.html(_parseDoctypeElement(doctypes, isPined));
    }

    function _bindDoctypesEvent() {
        var $commonDoctypes = $(".commonDocTypes");

        $commonDoctypes.find(".docfield-groups > a").click(function () {
            $(this).siblings("ul").toggle();
            if ($(this).find(".icon-arrow-down7").length > 0) {
                $(this).find(".icon-arrow").removeClass("icon-arrow-down7").addClass("icon-arrow-right7");
            } else {
                $(this).find(".icon-arrow").removeClass("icon-arrow-right7").addClass("icon-arrow-down7");
            }

            return false;
        });

        $('.li-create-new').hover(function () {
            $('.li-create-new').removeClass('unactive');
        }, function () {
            $('.li-create-new').addClass('unactive');
        });

        $(".doctypesFilter").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            return false;
        });

        $("#doctypesSearch").keyup(function () {
            var key = $(this).val();
            if (key.length > 0) {
                $(".newDocument[name *= '" + key + "']").removeClass("hidden").show();
                $(".newDocument").not("[name *= '" + key + "']").hide().addClass("hidden");
            } else {
                $(".newDocument").show().removeClass("hidden");
            }

            $(".docfieldCount").each(function () {
                var $docfield = $(this).parents("li.docfield-groups");
                var doctypesLength = $docfield.find("ul.subDoctype li.newDocument:not('.hidden')").length;
                $(this).text(" (" + doctypesLength + ")");
            });
        });
    }

    function _parseDoctypeElement(doctypes, isPined) {
        doctypes = _.each(doctypes, function (doctype) {
            var iconName = setIconDoctype(doctype.ActionLevel)
            doctype["iconName"] = iconName;
            return doctype;
        })
        var template = isPined ? pinedDoctypeTemplate : unpinDoctypeTemplate;
        var result = $.tmpl(template, doctypes);

        result.find(".create-new-document").click(function () {
            var doctypeId = $(this).attr('id');
            var doctypeName = $(this).attr('name');
            _createNewDocument(doctypeId, doctypeName);

            // ẩn tạo mới
            $('.li-create-new').addClass("unactive");
        });

        result.find(".pinDocType").click(function (e) {
            e.stopPropagation();
            var doctypeId = $(this).attr('id');
            var doctype = _.find(egovMain.doctypes, function (dt) {
                return dt.DocTypeId == doctypeId;
            });
            if (doctype == undefined) {
                return;
            }

            if (!$(this).is(".isPined") && doctype.Pinned) {
                // Chọn Pin nhưng đã tồn tại trên danh sách đã pin.
                return;
            }

            _setDoctypePinedToUser(doctypeId);

            doctype.Pinned = !doctype.Pinned;
            if (doctype.Pinned) {
                var pined = _parseDoctypeElement(doctype, true);
                $(".pinnedDocTypes").prepend(pined);
            } else {
                $(this).parents(".newDocument").remove();
            }
        });

        return result;
    }

    function _setDoctypePinedToUser(doctypeId) {
        /// <summary>
        /// Pin/bỏ pin loại văn bản
        /// </summary>
        if (doctypeId == undefined || doctypeId === "") {
            return;
        }

        $.ajax({
            url: '/Account/PinDocType',
            type: "Post",
            data: {
                docTypeId: doctypeId
            }
        });
    }

    function _displayDivider() {
        /// <summary>
        /// Hiển thị pinned divider
        /// </summary>
        var dividers = $('.new-doctypes').find(".divider");
        dividers.removeClass("hidden");
        _.each(dividers, function (item) {
            if ($(item).siblings("li").length === 0) {
                $(item).addClass("hidden");
            } else {
                $(item).removeClass("hidden");
            }
        });
        dividers.last().addClass("hidden");
    }

    // #endregion

    //#region Thay đổi font

    function _changeAppSize(appSizeValue) {
        var documentFrame = _mainApp.getContentWindow(_documentAppName);
        if (documentFrame) {
            documentFrame.egov.views.home.changeSize(appSizeValue);
        }

        _setCookie("ViewSize", appSizeValue, 365);

        var bmailFrame = _mainApp.getContentWindow(_bmailAppName);
        if (bmailFrame) {
            if (appSizeValue === 0) {
                $('#layout-doc', bmailFrame.document).removeClass("medium-size large-size");
            }
            else {
                $('#layout-doc', bmailFrame.document).removeClass("medium-size large-size");
                $('#layout-doc', bmailFrame.document).addClass(appSizeValue == 1 ? "medium-size" : "large-size");
            }
        }
    }

    function _setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires;
    }

    function setIconDoctype(level) {
        switch (level) {
            case 1:
                return "/Content/bkav.egov/times/ico-nam.png";
            case 2:
                return "/Content/bkav.egov/times/ico-nuanam.png";
            case 3:
                return "/Content/bkav.egov/times/ico-quy.png";
            case 4:
                return "/Content/bkav.egov/times/ico-month.png";
            case 5:
                return "/Content/bkav.egov/times/ico-week.png";
            case 6:
                return "/Content/bkav.egov/times/ico-day.png";
            default:
                return "/Content/bkav.egov/times/ico-khancap.png";
        }
    }

    //#endregion

    // #endregion

    jQuery.fn['editor'] = function (appendTo, callback) {
        /// <summary>
        /// Bind editor
        /// TODO: Hiện tại đang dùng editor ở frame bên dưới, nếu vừa clear cache + thao tác sửa trước khi load xong editor ở frame bên dưới thì sẽ bị lỗi => Nghiên cứu cách tối ưu hơn
        /// </summary>
        /// <param name="appendTo"></param>
        /// <param name="callback"></param>
        if (this) {
            var that = this,
                egovFrame = mainApps.getContentWindow("documents"),
                isLoaded = false;

            if (appendTo == "destroy") {
                return;
            }

            //Gán toolbar đã load trước vào toolbar của document mới
            if (egovFrame.Aloha && egovFrame.Aloha.$toolbar) {
                isLoaded = true;
                if (appendTo.find(egovFrame.Aloha.$toolbar).length == 0) {
                    appendTo.html(egovFrame.Aloha.$toolbar);
                }
            }

            egovFrame.Aloha.ready(function () {
                egovFrame.Aloha.jQuery(that).aloha();
                $(that).attr("spellcheck", false);
                if (!isLoaded) {
                    egovFrame.Aloha.trigger('aloha-editable-activated', {
                        'oldactive': undefined,
                        'editable': egovFrame.Aloha.editables[0]
                    });
                    appendTo.html(egovFrame.Aloha.$toolbar);
                }
                if (typeof callback === "function") {
                    callback();
                }
            });
        }
    }

    //function openLink(url) {
    //    $('.linked').parents('.qtip').hide();
    //    window.open(url, '_blank');
    //}

    //function _getContentWindow(frameName) {
    //    if (frameName === undefined) {
    //        return null;
    //    }
    //    var _el = $(".site-content iframe#" + frameName);
    //    if (_el === undefined || _el.length <= 0) {
    //        return null;
    //    }
    //    return _el[0].contentWindow;
    //}

})();

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