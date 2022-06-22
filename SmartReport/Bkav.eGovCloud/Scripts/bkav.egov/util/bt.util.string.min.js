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

