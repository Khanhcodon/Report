/*
* bkav.egate.compiler V2.1.0
*
* Copyright 2012: Bkav - Bso - Phòng 2 - eGate Team
* Created by: CuongNT@bkav.com.vn
* Edited by: 
*
* Mô tả về lớp bkav.egate.compiler.
* Public property
* Private property
* Convention
* Public method
*/

(function (f, fformModel, $, ko, JSON, undefined) {
    var msg = "Các số xóa bỏ, mất, hủy phải nằm trong khoảng {[10], [11]} và không được trùng nhau, giao nhau";
    var defaultDateTimeFormat = "dd/MM/yyyy";
    var dNow = new Date();
    window.now = {
        Year: dNow.getFullYear(),
        Month: dNow.getMonth() + 1,
        Day: dNow.getDate(),
        ToString: function (format) {
            return dNow.toString(format || defaultDateTimeFormat);
        }
    };
    f.validation = {
        ofType: function (data, type, format, errorCallback) {
            switch (type) {
                case "TaxTerm":
                    f.validation.isValidTaxTerm(data, errorCallback);
                    break;
                case "taxnumber":
                    if (!f.validation.checkTaxNumberFormat(data.toString())) {
                        errorCallback("Mã số thuế không đúng, hãy nhập lại");
                    }
                    break;
                case "numberrange":
                    if (!f.validation.checkNumberRange(data.toString())) {
                        errorCallback("Các số xóa bỏ, mất, hủy phải định dạng sau: 1-15 hoặc 1;3;5 hoặc 5, nằm trong khoảng {[10], [11]} và không được trùng nhau, giao nhau");
                    }
                    break;
                case "integer":
                    if (!/^-?\d+$/.test(data)) {
                        errorCallback("Chỉ được nhập số");
                    }
                    break;
                case "decimal":
                    if (!/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(data)) {
                        errorCallback("Chỉ được nhập số thập phân");
                    }
                    break;
                case "datetime":
                    if (!Date.parseExact(data, format || defaultDateTimeFormat)) {
                        errorCallback("Ngày tháng không đúng định dạng");
                    }
                    break;
                default:
                    return true;
            }
        },
        isValidTaxTerm: function (input, inputMsg) {
            var regexPattern = new RegExp('^Q[1-4]/\d{4}$');
            if (!regexPattern.test(input)) {
                inputMsg("Kỳ kê khai theo quý không dúng định dạng, hãy nhập lại");
                return false;
            }
            if (!/^\d{4}$/.test(input)) {
                inputMsg("Kỳ kê khai theo năm không dúng định dạng, hãy nhập lại");
                return false;
            }

            return true;
        },
        checkTaxNumberFormat: function (input) {
            if (input.length != 10 && input.length != 13) {
                return false;
            }
            if (input.length == 10) {
                if (f.utils.isNumber(input)) {
                    if (!f.validation.checkTaxNumber10Number(input)) {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            if (input.length == 13) {
                if (f.utils.isNumber(input)) {
                    var n1to10 = input.substr(0, 10);
                    var n11 = f.utils.toNumber(input.substring(10, 11));
                    var n12 = f.utils.toNumber(input.substring(11, 12));
                    var n13 = f.utils.toNumber(input.substring(12, 13));
                    if (!f.validation.checkTaxNumber10Number(n1to10)) {
                        return false;
                    }
                    if (!(n11 >= 0 || n11 <= 9)
                        || !(n12 >= 0 || n12 <= 9)
                        || !(n13 >= 0 || n13 <= 9)) {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            //            if (input.length == 14) {
            //                var n1to10 = input.substr(0, 10);
            //                var n11to14 = input.substr(11, 3);
            //                var n11 = input.substring(10, 11);


            //                if (n11.toString() != '-') {
            //                    return false;
            //                }
            //                else {
            //                    if (f.utils.isNumber(n1to10) && f.utils.isNumber(n11to14)) {

            //                        if (!f.validation.checkTaxNumber10Number(n1to10)) {
            //                            return false;
            //                        }
            //                        var n12 = f.utils.toNumber(input.substring(11, 12));
            //                        var n13 = f.utils.toNumber(input.substring(12, 13));
            //                        var n14 = f.utils.toNumber(input.substring(13, 14));
            //                        if (!(n12 >= 0 || n12 <= 9) || !(n13 >= 0 || n13 <= 9) || !(n14 >= 0 || n14 <= 9)) {
            //                            return false;
            //                        }
            //                    }
            //                    else {
            //                        return false;
            //                    }
            //                }
            //            }
            return true;
        },
        checkTaxNumber10Number: function (input) {
            var n1 = (input.substring(0, 1)) * 31;
            var n2 = (input.substring(1, 2)) * 29;
            var n3 = (input.substring(2, 3)) * 23;
            var n4 = (input.substring(3, 4)) * 19;
            var n5 = (input.substring(4, 5)) * 17;
            var n6 = (input.substring(5, 6)) * 13;
            var n7 = (input.substring(6, 7)) * 7;
            var n8 = (input.substring(7, 8)) * 5;
            var n9 = (input.substring(8, 9)) * 3;
            var n10 = input.substring(9, 10);
            var sodu = (n1 + n2 + n3 + n4 + n5 + n6 + n7 + n8 + n9) % 11;
            return 10 - sodu == n10;
        },
        checkNumberRange: function (input) {
            if (input.indexOf(';') != -1) {
                if (input.indexOf(';') == 0 || input.lastIndexOf(';') == input.toString().length - 1 || input.lastIndexOf('-') == input.toString().length - 1) {
                    return false;
                }
                var arrStr = input.split(';');
                for (var i = 0; i < arrStr.length; i++) {
                    if (arrStr[i].indexOf('-') != -1) {
                        var arrStr1 = arrStr[i].split("-");
                        if (!f.utils.isNumber(arrStr1[0]) || !f.utils.isNumber(arrStr1[1])) {
                            return false;
                        }
                        else {
                            if (f.utils.toNumber(arrStr1[0]) >= f.utils.toNumber(arrStr1[1])) {
                                return false;
                            }
                            else {
                                for (var j = i + 1; j < arrStr.length; j++) {
                                    if (!f.utils.checkTwoRangeConfluent(arrStr[i], arrStr[j]) || !f.utils.checkTwoRangeConfluent(f.utils.toNumber(arrStr[j]), arrStr[i])) {
                                        return false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (!f.utils.isNumber(arrStr[i]))
                        { return false; }
                        else {
                            if (arrStr[i] == arrStr[i + 1]) {
                                return false;
                            }
                        }
                    }
                }
            }
            else if (input.indexOf('-') != -1) {
                if (input.indexOf('-') == 0 || input.lastIndexOf('-') == input.length - 1) {

                    return false;
                }
                var arrStr2 = input.split("-");

                if (arrStr2.length > 2) {
                    return false;
                }
                if (!f.utils.isNumber(arrStr2[0]) || !f.utils.isNumber(arrStr2[1])) {
                    return false;
                }
                else {
                    if (f.utils.toNumber(arrStr2[0]) >= f.utils.toNumber(arrStr2[1])) {
                        return false;
                    }
                }
            }
            else {
                if (!f.utils.isNumber(input)) {
                    return false;
                }
            }
            return true;
        },

        require: function (data, errorCallback, errorText) {
            if (data == null || data == undefined || data == "") {
                errorCallback(errorText || "Trường này bắt buộc phải nhập");
            };
        },
        matchRegex: function (data, patternStr, errorCallback, errorText) {
            var regexPattern = new RegExp(patternStr);
            if (!regexPattern.test(data)) {
                errorCallback(errorText || "Dữ liệu nhập vào không đúng định dạng");
            }
        },
        withinLength: function (data, min, max, errorCallback, errorText) {
            if (data) {
                if (data.length < min || data.length > max) {
                    errorCallback(errorText || "Dữ liệu nhập vào quá lớn hoặc quá nhỏ");
                }
            }
        }
    };

    f.validate = function (val, type, format, rules) {
        var errors = [];
        var errorCallback = function (e) { errors.push(e); }
        f.validation.ofType(val, type, format, errorCallback);
        for (var i = 0; i < rules.length; i++) {
            var rule = rules[i];
            switch (rule.name) {
                case "required":
                    f.validation.require(val, errorCallback, rule.error)
                    break;
                case "regex":
                    f.validation.matchRegex(val, rule.pattern, errorCallback, rule.error);
                    break;
                case "withinLength":
                    f.validations.withinLength(val, rule.min, rule.max, errorCallback, rule.error);
                    break;
            }
        }
        return errors;
    };

    f.threadedLoop = function (array, name, order) {
        var self = this;
        var thread = {
            work: null,
            wait: null,
            index: 0,
            total: array.length,
            name: name,
            order: order,
            finished: false
        };
        //set the properties for the class
        this.collection = array;
        this.finish = function () { };
        this.action = function () {
            throw "You must provide the action to do for each element";
        };
        this.interval = 1;
        //set this to public so it can be changed
        var chunk = parseInt(thread.total * .005);
        this.chunk = (chunk == NaN || chunk == 0) ? thread.total : chunk;

        //end the thread interval
        thread.clear = function () {
            window.clearInterval(thread.work);
            window.clearTimeout(thread.wait);
            thread.work = null;
            thread.wait = null;
        };
        //checks to run the finish method
        thread.end = function () {
            if (thread.finished) { return; }
            self.finish(order);
            thread.finished = true;
        };
        //set the function that handles the work
        thread.process = function () {
            if (thread.index >= thread.total) { return false; }
            //thread, do a chunk of the work
            if (thread.work) {
                var part = Math.min((thread.index + self.chunk), thread.total);
                while (thread.index < part) {
                    self.action(self.collection[thread.index],
                    thread.index,
                    thread.total,
                    thread.name);
                    thread.index++;
                }
            }
            else {
                //no thread, just finish the work
                while (thread.index < thread.total) {
                    self.action(self.collection[thread.index],
                    thread.index,
                    thread.total,
                    thread.name);
                    thread.index++;
                }
            }
            //check for the end of the thread
            if (thread.index >= thread.total) {
                thread.clear();
                thread.end();
            }
            //return the process took place
            return true;
        };
        //set the working process
        self.start = function () {
            thread.finished = false;
            thread.index = 0;
            thread.work = window.setInterval(thread.process, self.interval);
        };
        //stop threading and finish the work
        self.wait = function (timeout) {
            //create the waiting function
            var complete = function () {
                thread.clear();
                thread.process();
                thread.end();
            };
            //if there is no time, just run it now
            if (!timeout) {
                complete();
            }
            else {
                thread.wait = window.setTimeout(complete, timeout);
            }
        };
    };

    f.utils = {
        parse: function (data, type, format) {
            switch (type) {
                case "string":
                    return (data || "") + "";
                case "integer":
                    return parseInt(data) || 0;
                case "decimal":
                    return parseFloat(data) || 0;
                case "datetime":
                    return (data || new Date().toString(format || defaultDateTimeFormat))
                default:
                    return data;
            }
        },
        resetErrorsToObservable: function (target) {
            for (var property in fformModel) {
                var val = target[property];
                if ($.isArray(val)) {
                    f.utils.resetErrorsToObservable(val)
                }
                else {
                    if (property.indexOf('Error') >= 0) {
                        target[property]('');
                    }
                }
            }
        },
        copyErrorsToObservable: function (source, target) {
            for (var property in source) {
                if (source.hasOwnProperty(property)) {
                    var val = source[property];
                    if ($.isArray(val)) {
                        if (target.hasOwnProperty(property) && ko.isWriteableObservable(target[property])) {
                            var targetArray = target[property]();
                            if ($.isArray(targetArray) && targetArray.length >= val.length) {
                                for (var i = 0; i < val.length; i++) {
                                    var errorRow = val[i];
                                    var targetRow = targetArray[i];
                                    if (targetRow) {
                                        f.utils.copyErrorsToObservable(errorRow, targetRow);
                                    }
                                }
                            }
                        }
                    }
                    else {
                        var errorProp = property + "Error";
                        if (target.hasOwnProperty(errorProp)
                            && ko.isWriteableObservable(target[errorProp])) {
                            target[errorProp](val);
                        }
                    }
                }
            }
            return target;
        },
        copyDataToObservable: function (source, target) {
            for (var property in source) {
                if (source.hasOwnProperty(property)
                    && target.hasOwnProperty(property)
                    && ko.isWriteableObservable(target[property])) {
                    var val = source[property];
                    if ($.isArray(val)) {
                        var addRowProp = property + "AddRow";
                        var addProp = "__add__";
                        if (target.hasOwnProperty(addRowProp) && $.isFunction(target[addRowProp])) {
                            for (var i = 0; i < val.length; i++) {
                                target[addRowProp](val[i]);
                            }
                        } else if (target[property].hasOwnProperty(addProp) && $.isFunction(target[property][addProp])) {
                            var adder = target[property][addProp];
                            for (var i = 0; i < val.length; i++) {
                                adder(val[i]);
                            }
                        }
                    } else {
                        target[property](val);
                    }

                }
            }
            return target;
        },
        copyDataToObservableSpecial: function (source, target) {
            var count = 0;
            var allThread = [];
            var order = 0;
            var progressbar = function (percent) {
                if (typeof window["step"] != 'undefined'
                    && typeof window["percent"] != 'undefined'
                    && $("#progressbar").length > 0) {
                    window["percent"] = percent ? percent : window["percent"] + window["step"];
                    $("#progressbar").progressbar({
                        value: window["percent"]
                    });
                }
            }
            for (var property in source) {
                if (source.hasOwnProperty(property)
                    && target.hasOwnProperty(property)
                    && ko.isWriteableObservable(target[property])) {
                    var val = source[property];
                    if ($.isArray(val)) {
                        if (val.length > 0) {
                            var addRowProp = property + "AddRow";
                            var addProp = "__add__";
                            if (target.hasOwnProperty(addRowProp) && $.isFunction(target[addRowProp])) {
                                for (var i = 0; i < val.length; i++) {
                                    count++
                                }
                            }
                            else if (target[property].hasOwnProperty(addProp) && $.isFunction(target[property][addProp])) {
                                for (var i = 0; i < val.length; i++) {
                                    count++
                                }
                            }
                        }
                    }
                    else {
                        count++;
                    }
                }
            }
            window["step"] = count > 0 ? 80 / count : 80;

            for (var property in source) {
                if (source.hasOwnProperty(property)
                    && target.hasOwnProperty(property)
                    && ko.isWriteableObservable(target[property])) {
                    var val = source[property];
                    if ($.isArray(val)) {
                        var addRowProp = property + "AddRow";
                        var addProp = "__add__";
                        if (target.hasOwnProperty(addRowProp) && $.isFunction(target[addRowProp])) {
                            if (val.length > 0) {
                                var thread = new f.threadedLoop(val, addRowProp, order);
                                thread.chunk = 3;
                                thread.action = function (item, index, total, name) {
                                    progressbar();
                                    target[name](item);
                                };
                                order++;
                                allThread.push(thread);
                            }
                        }
                        else if (target[property].hasOwnProperty(addProp) && $.isFunction(target[property][addProp])) {
                            if (val.length > 0) {
                                var thread = new f.threadedLoop(val, addProp, order);
                                thread.chunk = 3;
                                thread.action = function (item, index, total, name) {
                                    progressbar();
                                    adder(val[i]);
                                };
                                order++;
                                allThread.push(thread);
                            }
                        }
                    }
                    else {
                        progressbar()
                        target[property](val);
                    }
                }
            }
            if (allThread.length > 0) {
                for (var i = 0; i < allThread.length - 1; i++) {
                    allThread[i].finish = function (order) {
                        allThread[order + 1].start();
                    };
                }
                allThread[allThread.length - 1].finish = function (order) {
                    progressbar(100);
                    $.unblockUI();
                };
                allThread[0].start();
            } else {
                progressbar(100);
                $.unblockUI();
            }
        },
        detachForm: function (model, key) {
            fformModel.intern = fformModel.intern || {};
            for (var prop in model) {
                if (model.hasOwnProperty(prop)) {
                    if (fformModel.hasOwnProperty(prop)) {
                        delete fformModel[prop];
                    }
                    if (fformModel.extern.hasOwnProperty(prop)) {
                        delete fformModel.extern[prop];
                    }
                    if (fformModel.externDependencies.hasOwnProperty(prop)) {
                        delete fformModel.externDependencies[prop];
                    }

                    if (fformModel.intern.hasOwnProperty(prop)) {
                        fformModel[prop] = fformModel.intern[prop];
                        delete fformModel.intern[prop];
                    }
                }
            }
        },
        dumpForDotNET: function () {
            var data = f.utils.copyDataFromObservable(fformModel, {});

            var stripErrorProp = function (data) {
                for (var prop in data) {
                    var propName = prop.toString();
                    if (/Error$/.test(propName)) {
                        delete data[prop];
                    }
                }
            };

            stripErrorProp(data);
            for (var prop in data) {
                if ($.isArray(data[prop])) {
                    for (var i = 0; i < data[prop].length; i++) {
                        stripErrorProp(data[prop][i]);
                    }
                    data[prop] = {
                        $type: "System.Collections.Generic.List`1[[System.Collections.Generic.Dictionary`2[[System.String, mscorlib],[System.Object, mscorlib]], mscorlib]], mscorlib",
                        $values: data[prop]
                    }
                }
            }
            return JSON.stringify(data);
        },
        copyDataFromObservable: function (source, target) {
            if (typeof source == 'string') {
                target = source;
            }
            else {
                for (var property in source) {

                    if (source.hasOwnProperty(property)
                    && ko.isObservable(source[property])) {
                        var value = source[property]();
                        if ($.isArray(value)) {
                            var items = [];
                            for (var i = 0; i < value.length; i++) {
                                var item = f.utils.copyDataFromObservable(value[i], {});
                                items.push(item);
                            }
                            target[property] = items;
                        }
                        else {
                            target[property] = source[property]();
                        }
                    }
                }
            }
            return target;
        },
        loadDependency: function (dependency, context, dataType) {
            var result = "";
            var container = context.hasOwnProperty(dependency)
                            ? context
                            : fformModel.hasOwnProperty(dependency)
                            ? fformModel
                            : null;
            if (container) {
                result = ko.isObservable(container[dependency])
                        ? container[dependency]()
                        : container[dependency];
            }
            return f.utils.parse(result, dataType);
        },
        sum: function (tableName, colName, context) {
            var result = 0;
            var table;
            if (context.hasOwnProperty(tableName) && ko.isObservable(context[tableName])) {
                table = context[tableName]();
            }
            else if (context.container && context.container.hasOwnProperty(tableName) && ko.isObservable(context.container[tableName])) {
                table = context.container[tableName]();
            } else if (context.form && context.form.hasOwnProperty(tableName) && ko.isObservable(context.form[tableName])) {
                table = context.form[tableName]();
            }

            if (table && table.length) {
                for (var i = 0; i < table.length; i++) {
                    var row = table[i];
                    if (row.hasOwnProperty(colName) && ko.isObservable(row[colName])) {
                        var cell = row[colName];
                        var cellVal = cell();
                        var fParse = cell['fParse'];
                        if (fParse && typeof (fParse) === 'function') {
                            cellVal = fParse(cellVal);
                        }
                        result += cellVal || 0;
                    }
                }
            }

            return result;
        },
        checkNumberInRange: function (inputNumber, minNumber, maxNumber) {
            if (inputNumber == 0) {
                return;
            }
            return inputNumber >= minNumber && inputNumber <= maxNumber ? true : false;

        },
        isNumber: function (inputVal) {
            for (var i = 0; i < inputVal.length; i++) {
                var oneChar = inputVal.charAt(i)
                if (oneChar < "0" || oneChar > "9") {
                    return false
                }
            }
            return true;
        },
        toNumber: function (strNumber) {
            var numStr = strNumber.toString();
            if (null == strNumber || '' == strNumber) {
                return 0;
            }

            while (numStr.indexOf('.') != -1) {
                numStr = numStr.replace('.', '');
            }
            while (numStr.indexOf(',') != -1) {
                numStr = numStr.replace(',', '.');
            }
            if (numStr.indexOf('(') != -1) {
                numStr = numStr.replace('(', '');
                numStr = numStr.replace(')', '');
                numStr = '-' + numStr;
            }
            return parseFloat(numStr);
        },
        checkOneNumberInRange: function (soHoaDon, val2) {
            if (val2 != '' && soHoaDon != 0) {
                if (val2.indexOf(';') != -1) {
                    var soHDonArr = val2.split(";");
                    for (var i = 0; i < soHDonArr.length; i++) {
                        if (soHDonArr[i].indexOf('-') != -1) {
                            var soHDon = soHDonArr[i].split("-");
                            if ((f.utils.toNumber(soHoaDon) >= f.utils.toNumber(soHDon[0]) && f.utils.toNumber(soHoaDon) <= f.utils.toNumber(soHDon[1]))) {
                                //alert(msg);
                                //srcObj.focus();
                                return false;
                            }
                        } else if (soHoaDon == soHDonArr[i]) {
                            //alert(msg);
                            //srcObj.focus();
                            return false;
                        }
                    }
                } else if (val2.indexOf('-') != -1) {
                    var soHDon12 = val2.split("-");
                    if ((f.utils.toNumber(soHoaDon) >= f.utils.toNumber(soHDon12[0]) && f.utils.toNumber(soHoaDon) <= f.utils.toNumber(soHDon12[1]))) {
                        return false;
                    }

                } else if (f.utils.toNumber(soHoaDon) == f.utils.toNumber(val2)) {
                    //alert(msg);
                    //srcObj.focus();
                    return false;
                }

            }
            return true;
        },
        checkTwoRangeConfluent: function (val1, val2) {
            var soHoaDon;
            var soHoaDon2;
            if (val1 != '') {
                if (val1.toString().indexOf(';') != -1) {
                    soHoaDon = val1.split(";");
                    for (var i = 0; i < soHoaDon.length; i++) {
                        if (soHoaDon[i].indexOf('-') != -1) {
                            soHoaDon2 = soHoaDon[i].split("-");
                            for (var j = 0; j < soHoaDon2.length; j++) {
                                if (!f.utils.checkOneNumberInRange(soHoaDon2[j], val2)) {
                                    return false;
                                    break;
                                }
                            }
                        } else {
                            if (!f.utils.checkOneNumberInRange(soHoaDon[i], val2)) {
                                return false;
                                break;
                            }
                        }
                    }
                } else if (val1.toString().indexOf('-') != -1) {

                    var soHoaDon3 = val1.split("-");
                    for (var k = soHoaDon3[0]; k <= soHoaDon3[1]; k++) {
                        if (!f.utils.checkOneNumberInRange(k, val2)) {
                            return false;
                            break;
                        }
                    }
                } else {
                    if (!f.utils.checkOneNumberInRange(val1, val2)) {
                        return false;
                    }
                }
            }
            return true;
        },
        countNumberInRange: function (val1, minNumber, maxNumber) {

            var soHoaDon;
            var soHoaDon2;
            var soHoaDon3;
            var count = 0;
            if (val1 != '' && val1 != 0) {
                if (val1.indexOf(';') != -1) {
                    soHoaDon = val1.split(";");
                    for (var i = 0; i < soHoaDon.length; i++) {
                        if (soHoaDon[i].indexOf('-') != -1) {
                            soHoaDon2 = soHoaDon[i].split("-");
                            for (var j = soHoaDon2[0]; j <= soHoaDon2[1]; j++) {
                                if (f.utils.checkNumberInRange(j, minNumber, maxNumber)) {
                                    count++;
                                }
                            }
                        } else {
                            if (f.utils.checkNumberInRange(soHoaDon[i], minNumber, maxNumber)) {
                                count++;
                            }
                        }
                    }
                } else if (val1.toString().indexOf('-') != -1) {
                    soHoaDon3 = val1.split("-");
                    for (var k = soHoaDon3[0]; k <= soHoaDon3[1]; k++) {
                        if (f.utils.checkNumberInRange(k, minNumber, maxNumber)) {
                            count++;
                        }
                    }
                } else {
                    if (f.utils.checkNumberInRange(val1, minNumber, maxNumber)) {
                        count++;
                    }
                }
            }
            return count;
        },

        //Check signs bill
        checkSignsBill: function (inputStr, billCode) {
            var numberError = 0;
            if (billCode != '') {
                if (inputStr.length != 6 && inputStr.length != 8) {
                    numberError = 6; //Độ dài ký hiệu hóa đơn 6 ký tự đối với tổ chức, cá nhân kinh doanh đặt in, tự in và 8 ký tự đối với cơ quan thuế
                    if (billCode == "TT120") {
                        numberError = 6.1;
                    }
                }
                else {
                    if (inputStr.length == 6) {
                        numberError = f.utils.checkKhieuHoadon(inputStr);
                    }
                    if (inputStr.length == 8) {
                        if (billCode == "TT120") {
                            var arr = ["A", "B", "C", "D", "E", "G", "H", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y"];
                            var _0to1 = inputStr.substring(0, 1);
                            var _1to2 = inputStr.substring(1, 2);
                            var _2to3 = inputStr.substring(2, 3);
                            var _3to7 = inputStr.substring(3, 7);
                            var _7to8 = inputStr.substring(7, 8);
                            if (!f.utils.in_array(_0to1, arr) || !f.utils.in_array(_1to2, arr)) {
                                numberError = 1; // Hai ký tự nằm trong bảng chữ cái gồm: A, B, C, D, E, G, H, K, L, M, N, P, Q, R, S, T, U, V, X, Y
                            }
                            if (_2to3 != "/") {
                                numberError = 7; // 8 - Nếu mã hóa đơn là TT120 thì ký tự thứ 3 phải là "/"
                            }

                            var d = new Date();

                            if (_3to7 < 0 || _3to7 > d.getFullYear()) {
                                numberError = 8; // 8 - Năm in hóa đơn
                            }
                            if (_7to8 != "B" && _7to8 != "T" && _7to8 != "N") {
                                numberError = 9; // 9-Nếu mã hóa đơn là TT120 thì ký tự cuối cùng phải là 1 trong 3 ký tự B, N, T
                            }
                        }
                        else {
                            var _0to1 = inputStr.substring(0, 1);
                            var _1to2 = inputStr.substring(1, 2);
                            var _2to8 = inputStr.substring(2, 8);
                            if (_0to1 < "0" || _0to1 > "6") {
                                numberError = 5; //4 - Mã hóa đơn của cục thuế các tỉnh 01 -> 64 
                            }
                            if (_1to2 < "0" || _1to2 > "9") {
                                numberError = 5;
                            }
                            numberError = f.utils.checkKhieuHoadon(_2to8);
                            if (numberError == 1) {
                                numberError = 1.1;
                            }
                            if (numberError == 2) {
                                numberError = 2.1;
                            }
                            if (numberError == 3) {
                                numberError = 3.1;
                            }
                        }
                    }

                }
            }
            return numberError;
        },
        //Check signs pattern bill
        checkSignsPatternBill: function (inputStr, billCode) {

            var numberError = 0;
            if (billCode != '' && billCode != 'TT120') {
                if (billCode == '01/' || billCode == '02/') {
                    var n0to3 = inputStr.substring(0, 3);
                    if (n0to3 != billCode) {
                        numberError = 1;
                    }
                }
                else {
                    if (inputStr.length != 11 && inputStr.length != 13) {
                        numberError = 6;
                    }
                    else {
                        if (inputStr.length == 11) {
                            numberError = f.utils.checkKyMauHieu(inputStr, billCode);
                        }
                        if (inputStr.length == 13) {
                            var n0to11 = inputStr.substring(0, 11);
                            var n11to13 = inputStr.substring(11, 13);
                            numberError = f.utils.checkKyMauHieu(inputStr, billCode);
                            if (numberError == 0) {
                                if (n11to13.toString() != "BD" && n11to13.toString() != "IV") {
                                    numberError = 5;
                                }
                            }
                        }
                    }
                }
            }
            return numberError;
        },

        checkKyMauHieu: function (inputStr, billCode) {
            var numberError = 0;
            var _0to6 = inputStr.substring(0, 6);
            var _6to7 = inputStr.substring(6, 7);
            var _7to8 = inputStr.substring(7, 8);
            var _8to9 = inputStr.substring(8, 9);
            var _9to10 = inputStr.substring(9, 10);
            var _10to11 = inputStr.substring(10, 11);

            if (_0to6 != billCode) {
                numberError = 1;
            }
            if (_6to7 < "2" || _6to7 > "9") {
                numberError = 2;
            }
            if (_7to8.toString() != "/") {
                numberError = 3;
            }
            if (_8to9 < "0" || _8to9 > "9") {
                numberError = 4;
            }
            if (_9to10 < "0" || _9to10 > "9") {
                numberError = 4;
            }
            if (_10to11 < "0" || _10to11 > "9") {
                numberError = 4;
            }
            return numberError;
        },
        checkPartSelection: function (input) {
            var result = false;
            for (var i = 0; i < itemModel.selections.length; i++) {
                if (input != null) {
                    if ((itemModel.selections[i].primary == false) && (itemModel.selections[i].key == input)) {
                        if (itemModel.selections[i].selected) {
                            result = true;
                            break;
                        }
                        else { result = false; }
                    }
                }
                else {
                    if ((itemModel.selections[i].primary == false) && (itemModel.selections[i].selected == true)) {
                        result = true;
                    }
                }
            }
            return result;
        },
        checkPrimarySelection: function (input) {
            var result = false;
            for (var i = 0; i < itemModel.selections.length; i++) {
                if (input != null) {
                    if (itemModel.selections[i].key == input) {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        },
        checkKhieuHoadon: function (input) {
            var numberError = 0; // 0 - Không có lỗi
            var arr = ["A", "B", "C", "D", "E", "G", "H", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y"];
            var _0to1 = input.substring(0, 1);
            var _1to2 = input.substring(1, 2);
            var _2to3 = input.substring(2, 3);
            var _3to4 = input.substring(3, 4);
            var _4to5 = input.substring(4, 5);
            var _5to6 = input.substring(5, 6);
            if (!f.utils.in_array(_0to1, arr) && !f.utils.in_array(_1to2, arr)) {
                numberError = 1;
            }
            if (_2to3 != "/") {
                numberError = 2; // 2 - Ký tự thứ 3 phải là "/"
            }
            if (_5to6 != "E" && _5to6 != "T" && _5to6 != "P") {
                numberError = 4; //4 - Ký tự cuối là ký hiệu của hình thức in hóa đơn: E(hóa đơn điện tử), T(hóa đơn tự in), P(hóa đơn đặt in)
            }
            if (_3to4 < "0" || _3to4 > "9") {
                numberError = 3; //2 - Ký tự thứ 3 và thứ 4 là dạng số, chỉ năm thông báo phát hành hóa đơn
            }
            if (_4to5 < "0" || _4to5 > "9") {
                numberError = 3;
            }
            return numberError;
        },
        format_number: function (pnumber, decimals) {
            if (isNaN(pnumber)) { return 0 };
            if (pnumber == '') { return 0 };

            var snum = new String(pnumber);
            var sec = snum.split('.');
            var whole = parseFloat(sec[0]);
            var result = '';

            if (sec.length > 1) {
                var dec = new String(sec[1]);
                dec = String(parseFloat(sec[1]) / Math.pow(10, (dec.length - decimals)));
                dec = String(whole + Math.round(parseFloat(dec)) / Math.pow(10, decimals));
                var dot = dec.indexOf('.');
                if (dot == -1) {
                    dec += '.';
                    dot = dec.indexOf('.');
                }
                while (dec.length <= dot + decimals) { dec += '0'; }
                result = dec;
            } else {
                var dot;
                var dec = new String(whole);
                dec += '.';
                dot = dec.indexOf('.');
                while (dec.length <= dot + decimals) { dec += '0'; }
                result = dec;
            }
            return result
        },
        in_array: function (value, array) {
            for (i = 0; i < array.length / 2; i++)
                if (array[2 * i] === value || (i > 0 && array[2 * i - 1] === value))
                    return true;
            return false;
        },
        msgPatternBill: function (result) {
            var msg = '';
            if (result == 1) {
                msg = '6 (hoặc 3) ký tự đầu phải giống với mã hóa đơn';
            }
            if (result == 2) {
                msg = 'Số liên hóa đơn ít nhất là 2 và nhiều nhất là 9 (ký tự thứ 7)';
            }
            if (result == 3) {
                msg = 'Ký tự thứ 8 phải là "/"';
            }
            if (result == 4) {
                msg = '3 ký tự cuối là số thứ tự của mẫu trong một loại hóa đơn (khi độ dài mẫu là 11) ';
            }
            if (result == 5) {
                msg = 'Nếu mẫu hóa đơn dài 13 ký tự, thì 2 ký tự cuối phải là "BD" hoặc "IV"';
            }
            if (result == 6) {
                msg = 'Độ dài ký hiệu mẫu hóa đơn là 11 hoặc 13(tối đa) ký tự';
            }
            return msg;
        },
        msgSignsBill: function (result) {
            var msg = '';
            if (result == 1) {
                msg = 'Hai ký tự đầu nằm trong bảng chữ cái gồm: A, B, C, D, E, G, H, K, L, M, N, P, Q, R, S, T, U, V, X, Y';
            }
            if (result == 1.1) {
                msg = 'Hai ký tự thứ 3 và thứ 4 nằm trong bảng chữ cái gồm: A, B, C, D, E, G, H, K, L, M, N, P, Q, R, S, T, U, V, X, Y';
            }
            if (result == 2) {
                msg = 'Ký tự thứ 3 phải là "/"';
            }
            if (result == 2.1) {
                msg = 'Ký tự thứ 5 phải là "/"';
            }
            if (result == 3) {
                msg = 'Ký tự thứ 4 và thứ 5 là dạng số, chỉ năm thông báo phát hành hóa đơn';
            }
            if (result == 3.1) {
                msg = 'Ký tự thứ 6 và thứ 7 là dạng số, chỉ năm thông báo phát hành hóa đơn';
            }
            if (result == 4) {
                msg = 'Ký tự cuối là ký hiệu của hình thức in hóa đơn: E(hóa đơn điện tử), T(hóa đơn tự in), P(hóa đơn đặt in)';
            }
            if (result == 5) {
                msg = '2 ký tự đàu là mã hóa đơn của cục thuế các tỉnh từ: 01 -> 64';
            }
            if (result == 6) {
                msg = 'Độ dài ký hiệu hóa đơn 6 ký tự đối với tổ chức, cá nhân kinh doanh đặt in, tự in và 8 ký tự đối với cơ quan thuế và không để trắng';
            }
            if (result == 6.1) {
                msg = 'Nếu mã hóa đơn là TT120 thì ký hiệu gồm 8 ký tự và không để trắng';
            }
            if (result == 7) {
                msg = 'Nếu mã hóa đơn là TT120 thì ký tự thứ 3 phải là "/"';
            }
            if (result == 8) {
                msg = 'Nếu mã hóa đơn là TT120 thì ký tự thứ 4 đến thứ 7 là năm in hóa đơn';
            }
            if (result == 9) {
                msg = 'Nếu mã hóa đơn là TT120 thì ký tự cuối cùng phải là 1 trong 3 ký tự B, N, T';
            }
            return msg;
        }
    };
})(window.f = window.f || {}, window.fformModel = window.fformModel || {}, jQuery, window.ko, JSON)