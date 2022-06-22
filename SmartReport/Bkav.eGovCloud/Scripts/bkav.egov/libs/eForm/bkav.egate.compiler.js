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

(function (f, fformModel, $, JSON, eForm, undefined) {
    //TODO: need a BIG refactor
    var log = (console && console.log) ? console.log : window.alert;
    var formid = "";
   
    fformModel = fformModel || {};

    //#region <CONSTANTS>
    var dataTypes = {
        string: "string",
        text: "Text",
        object: "object",
        set: "set",
        "boolean": "boolean",
        integer: "integer",
        signedinteger: "signedinteger",
        decimal: "decimal",
        dateTime: "datetime",
        timeSpan: "timespan",
        uri: "uri",
        email: "email",
        cmtnd: "cmtnd",
        IDCard: "idcard", // bỏ đi
        mobile: "mobile",
        year: "year",
        monthyear: "monthyear",
        custom: "custom"
    };

    // p13, p22, p23, p27, p28, p29, p30: bắt buộc nhập
    var validatorTypes = {
        required: "p13",        //"required",
        maxLength: "p22",       //"maxLength",
        arrange: "p23",         //"arrange"
        validateCsharp: "p27",  //"validate c#"
        jsExpression: "p28",    //"jsExpression"
        regex: "p29",           //"regex",
        nonNegative: "p30"      //"nonNegative"
    };

    var falsyValues = ['false', '', ' ', 'no', 'off', 'n', 'undefined', 'null', 'nope', 'negative'];

    var defaultFormats = {
        integer: "n0",
        decimal: "n2",
        datetime: "dd/MM/yyyy"
    };

    var defaultValues = {
        string: "",
        integer: 0,
        decimal: "0,00",
        datetime: $.global.format(new Date(), defaultFormats.datetime, $.global.culture),
        "boolean": false
    };
    //#endregion </CONSTANTS>   

    // ###########################
    //#region <CUSTOM DATA TYPES>
    var customTypes = {
        patterns: {
            quarter: new RegExp("^Q[1-4]\/\\d{4}$"),
            period: new RegExp("^K[1-2]\/\\d{4}$"),
            month: new RegExp("^([1-9]|0[1-9]|1[0-2])\/(19\\d{2}|2[0-3]\\d{2})$"),
            year: new RegExp("^(19\\d{2}|2[0-3]\\d{2})$"),
            cmtnd: new RegExp("^\\d{9}$"),
            mobile: new RegExp("^0(9[0-9]{8}|1[0-9]{9})$")
        },
        parseEmail: function (inputStr) {
            if (!inputStr) return "";

            var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!emailPattern.test(inputStr)) {
                throw new Error("Địa chỉ email không đúng");
            }
            return inputStr;
        },
        parseCmtnd: function (inputStr) {
            // 012.575.658
            if (!inputStr) return "";
            if (!this.patterns.cmtnd.test(inputStr)) {
                throw new Error("Định dạng CMTND không hợp lệ");
            }
            return inputStr;
        },
        parseYear: function (inputStr) {
            if (!inputStr) return "";
            if (!this.patterns.year.test(inputStr)) {
                throw new Error("Định dạng Năm không hợp lệ");
            }
            var yearTmp = $.global.parseInt(inputStr.toString(), 10, $.global.culture);
            if (yearTmp < 1900 || yearTmp > 3000) {
                throw new Error("Năm không được nhỏ hơn 1900 hoặc lớn hơn 2300");
            }
            return yearTmp.toString();
        },
        parseMonthYear: function (inputStr) {
            if (!inputStr) return "";
            if (!this.patterns.month.test(inputStr)) {
                throw new Error("Tháng/Năm nhập không đúng");
            }
            var monthTmp = parseInt(inputStr.split('/')[0], 10);
            var yearTmp = parseInt(inputStr.split('/')[1], 10);
            if (yearTmp < 1900 || yearTmp > 3000) {
                throw new Error("Năm không được nhỏ hơn 1900 hoặc lớn hơn 2300");
            }
            if (monthTmp < 1 || monthTmp > 12) {
                throw new Error("Tháng không được nhỏ hơn 1 hoặc lớn hơn 12");
            }
            return monthTmp + "/" + yearTmp;
        },
        parseMobile: function (inputStr) {
            if (!inputStr) return "";
            if (!this.patterns.mobile.test(inputStr)) {
                throw new Error("Số Di động nhập không đúng");
            }
            return inputStr;
        }
    };
    //#endregion </CUSTOM DATA TYPES>

    //#region <TOPOLOGICAL SORTING>
    // Sắp xếp control theo thứ tự phụ không phụ thuộc --> có phụ thuộc
    // ctrl5 = ctrl3 + ctrl2; ctrl1 = ctrl5 + ctrl7; --> thứ tự sau sort là: [ctrl3;ctrl2;ctrl5;ctrl7;ctrl1]
    /* ctrl1|--> ctrl5|--> ctrl3
    |         |--> ctrl2
    |--> ctrl7
    */
    var topoSort = function (nodes, fnGetRefs) {
        var visited = {};
        var result = [];
        if (nodes) {
            for (var i = 0, l = nodes.length; i < l; i++) {
                visit(nodes[i], fnGetRefs, visited, result);
            }
        }
        return result;
    };
    var visit = function (node, fnGetRefs, visited, result) {
        if (visited.hasOwnProperty(node.getKey()) && visited[node.getKey()] === true) {
            return;
        }
        visited[node.getKey()] = true;
        var dependencies = fnGetRefs(node);
        for (var i = 0, l = dependencies.length; i < l; i++) {
            visit(dependencies[i], fnGetRefs, visited, result);
        }
        result.push(node);
    };
    //#endregion </TOPOLOGICAL SORTING>

    //#region <UTILS>
    var getElementDict = function (items) {
        var result = {};
        if (items) {
            for (var i = 0, len = items.length; i < len; i++) {
                var node = items[i];
                result[node.key] = node;
            }
        }
        return result;
    };
    // Lấy giá trị của field trong container sau khi parse
    var getFieldValueByKey = function (dependency, container) {
        var result;
        var dependencyProp;
        if (container.hasOwnProperty(dependency)) {
            dependencyProp = container[dependency];
        } else if (fformModel.hasOwnProperty(dependency)) {
            dependencyProp = fformModel[dependency];
        } else {
            return undefined;
        }
        if (ko.isObservable(dependencyProp)) {
            result = dependencyProp();
            if (result === null || result === undefined) {
                result = "";
            }
            var fParse = dependencyProp['fParse'];
            if (fParse && typeof (fParse) === 'function') {
                // Không được bắt exception ở đây
                result = fParse(result);
            }
        }
        else {
            result = dependencyProp;
        }
        return result;
    };
    //#endregion </UTILS>

    // ###########################
    //#region <PARSING>
    var parsers = {
        object: function (value) {
            var result;
            if (typeof value === 'string') {
                try {
                    result = JSON.parse(value);
                }
                catch (error) {
                    result = value;
                }
            }
            else {
                result = value;
            }
            return result;
        },
        "boolean": function (value) {
            var result = true;
            if (value === undefined || value === null || value.toString().toLowerCase() in falsyValues) {
                result = false;
            }
            return result;
        },
        string: function (value) {
            return value ? value.toString() : defaultValues.string;
        },
        integer: function (value) {
            // 100,000.10
            var result = value ? $.global.parseInt(value.toString(), 10, $.global.culture) : defaultValues.integer;
            if (isNaN(result)) {
                throw new Error("Dữ liệu không phải số");
            }
            return result;
        },
        signedinteger: function (value) {
            var result = value ? $.global.parseInt(value.toString(), 10, $.global.culture) : defaultValues.integer;
            if (isNaN(result)) {
                throw new Error("Dữ liệu không phải số");
            }
            if (result < 0) {
                throw new Error("Dữ liệu không phải là số > 0");
            }
            return result;
        },
        decimal: function (value) {
            // Sử dụng dấu , ngăn cách hàng thập phân và dùng dấu . để ngăn cách hàng nghìn
            var result = value ? $.global.parseFloat(value.toString(), 10, $.global.culture) : defaultValues.decimal;
            // result != defaultValues.decimal --> Loại bỏ trường hợp isNaN("0,00") là true
            if (result != defaultValues.decimal && isNaN(result)) {
                throw new Error("Dữ liệu không phải số");
            }
            return result;
        },
        dateTime: function (value, format) {
            // dd/MM/yyyy
            var result = value ? $.global.parseDate(value.toString(), format || defaultFormats.datetime, $.global.culture) : "";
            if (result == null) {
                throw new Error("Dữ liệu không phải ngày/tháng/năm");
            }
            return result;
        },
        monthyear: function (value) {
            return customTypes.parseMonthYear(value); // bỏ new
        },
        year: function (value) {
            return customTypes.parseYear(value); // bỏ new
        },
        mobile: function (value) {
            return customTypes.parseMobile(value); // bỏ new
        },
        cmtnd: function (value) {
            return customTypes.parseCmtnd(value); // bỏ new
        },
        email: function (value) {
            return customTypes.parseEmail(value); // bỏ new
        }
        //TODO: number range:
    };

    // ###########################
    var getParser = function (ffield) {
        var parser;
        var type = ffield.getControlMask();
        switch (type.toLowerCase()) {
            case dataTypes.string:
            case dataTypes.text:
                parser = parsers.string;
                break;
            case dataTypes.integer:
                parser = parsers.integer;
                break;
            case dataTypes.signedinteger:
                parser = parsers.signedinteger;
                break;
            case dataTypes.decimal:
                parser = parsers.decimal;
                break;
            case dataTypes.dateTime:
                parser = parsers.dateTime;
                break;
            case dataTypes.email:
                parser = parsers.email;
                break;
            case dataTypes.cmtnd:
                parser = parsers.cmtnd;
                break;
            case dataTypes.IDCard:
                parser = parsers.cmtnd;
                break;
            case dataTypes.mobile:
                parser = parsers.mobile;
                break;
            case dataTypes.monthyear:
                parser = parsers.monthyear;
                break;
            case dataTypes.year:
                parser = parsers.year;
                break;
            case dataTypes.object:
                parser = parsers.object;
                break;
            case dataTypes.custom:
                var customTypeName = ffield.customTypeName;
                if (customTypeName) {
                    parser = parsers[customTypeName.toLowerCase()];
                }
                else {
                    parser = parsers.string;
                }
                break;
            default:
                // Mặc định là parse selected của dropdownlist {catalogname:"Tên catalog", cataoglid: 3}
                parser = parsers.object;
                break;
        }
        return parser;
    };
    //#endregion </PARSING>

    //#region <FORMATTING>
    var formatters = {
        decimal: function (value) {
            return value ? $.global.format(value || defaultValues.decimal, defaultFormats.decimal, $.global.culture) : "";
        },
        integer: function (value) {
            return value ? $.global.format(value || defaultValues.integer, defaultFormats.integer, $.global.culture) : "";
        },
        signedinteger: function (value) {
            return value ? $.global.format(value || defaultValues.integer, defaultFormats.integer, $.global.culture) : "";
        },
        datetime: function (value) {
            return value ? $.global.format(value, defaultFormats.datetime, $.global.culture) : null;
        }
    };
    //#endregion </FORMATTING>

    //#region <VALIDATING>
    var getValidators = function (ffield, container) {
        var validators = [];
        var properties = ffield.getProperties();
        for (var i = 0, ruleCount = properties.length; i < ruleCount; i++) {
            var arg = "", msg = "", dependencies = "";
            var result = null;
            var rule = properties[i];
            switch (rule.getId()) {
                case validatorTypes.required:
                    if (rule.getValue().toLowerCase() == 'true') {
                        result = function (value) {
                            return value ? true : false;
                        };
                        msg = "Dữ liệu bắt buộc nhập";
                    }
                    break;
                case validatorTypes.nonNegative:
                    if (rule.getValue().toLowerCase() == 'true') {
                        result = function (value) {
                            return value >= 0 ? true : false;
                        };
                        msg = "Dữ liệu là Số không âm";
                    }
                    break;
                case validatorTypes.regex:
                    if (!!rule.getValue()) {
                        arg = rule.getValue().split("@")[0];
                        msg = rule.getValue().split("@")[1];

                        var regExp = new RegExp(arg);
                        result = function (value) {
                            return regExp.test(value);
                        };
                    }
                    break;
                case validatorTypes.maxLength:
                    var maxLen = parseInt(arg) || -1;
                    if (!!rule.getValue() && maxLen != -1) {
                        arg = rule.getValue();
                        msg = "Không nhập quá " + arg + " kí tự.";
                        result = function (value) {
                            return (value + "").length <= maxLen;
                        };
                    }
                    break;
                case validatorTypes.arrange:
                    if (!!rule.getValue() && rule.getValue().split("-").length == 2) {
                        var minV = rule.getValue().split("-")[0];
                        var maxV = rule.getValue().split("-")[1];
                        result = function (value) {
                            return value >= minV && value <= maxV;
                        };
                        msg = "Dữ liệu phải nằm trong khoảng: [" + minV + "," + maxV + "]";
                    }
                    break;
                case validatorTypes.jsExpression:
                    if (!!rule.getValue()) {
                        arg = rule.getValue().split("@")[0].replace(/\s+/g, '');
                        dependencies = rule.getValue().split("@")[1].replace(/\s+/g, '').split(",");
                        msg = rule.getValue().split("@")[2];
                        var expression = arg;
                        if (expression) {//&& !detectExternalDependencies(rule.dependencies, container)
                            var validator = new Function("with(this){ return " + expression + " }");
                            result = function (value) {
                                var scope = arrangeScope(dependencies, container, fformModel, value);
                                return validator.call(scope);
                            };
                        }
                    }
                    break;
            }

            if (result) {
                if (!msg) {
                    msg = "Dữ liệu không hợp lệ";
                }
                validators.push({
                    validate: result,
                    message: msg,
                    isWarning: false//rule.type === "Warning"
                });
            }
        }
        return validators;
    };
    //#endregion </VALIDATING>

    //#region <EVALUATING>
    var detectExternalDependencies = function (dependencies, container) {
        var hasExternalDependency = false;
        if (dependencies && dependencies.length) {
            for (var i = 0, count = dependencies.length; i < count; i++) {
                var dependency = dependencies[i];
                //we assume the form is only 2 level deep
                if (container.hasOwnProperty(dependency)) {
                    continue;
                } else if (fformModel.hasOwnProperty(dependency)) {
                    continue;
                } else {
                    hasExternalDependency = true;
                    break;
                }
            }
        }
        return hasExternalDependency;
    };
    // Lấy hàm tính giá trị tự động cho field trong model
    var getEvaluator = function (ffield, container) {
        var evaluator;
        var expr = ffield.getExpressionOfValue();
        var dependencies = ffield.getDependenciesOfValue();
        if (!expr) return false; //&& detectExternalDependencies(dependencies, container)
        try {
            evaluator = new Function("with(this){ return " + expr + " }");
        }
        catch (error) {
            return false;
        }
        return evaluator;
    };
    // Chuẩn bị phạm vi để thực thi công thức js (công thức validate, tính tự động...) cấu hình trong mô tả eForm. 
    var arrangeScope = function (dependencies, container, form, value) {
        var dtNow = new Date();
        dtNow.Year = dtNow.getFullYear();
        dtNow.Month = dtNow.getMonth() + 1;
        dtNow.Day = dtNow.getDate();
        dtNow.ToString = dtNow.toString;
        var scope = {
            form: form,
            container: container,
            now: dtNow,
            value: value
        };

        if (dependencies && dependencies.length) {
            for (var i = 0, count = dependencies.length; i < count; i++) {
                var dependency = dependencies[i];
                //TODO:check for naming violation
                try {
                    scope[dependency] = getFieldValueByKey(dependency, container);
                }
                catch (exception) {
                    scope[dependency] = null;
                }
            }
        }
        return scope;
    };
    //#endregion </EVALUATING>

    var compileField = function (ffield, container) {

        ffield.key = ffield.getKey();
        ffield.value = ffield.getControlValue();

        // get parsers, formatter & evaluator
        ffield.parser = getParser(ffield);
        // get formatter
        ffield.formatter = formatters[ffield.getControlMask().toLowerCase()];
        // evaluator: p26: jsExpress, p25: c# Express, p21: lấy dữ liệu từ id tương ứng
        ffield.evaluator = getEvaluator(ffield, container);

        //#region <VALUE>
        var result;
        if (ffield.evaluator) {
            // dependent observable
            result = ko.dependentObservable(function () {
                // lấy lại dependencies of ffield. Get "Tự tính JS": p26
                var dependencies = ffield.getDependenciesOfValue();
                var scope = arrangeScope(dependencies, container, fformModel);
                var val = ffield.evaluator.call(scope);
                if (ffield.formatter) {
                    val = ffield.formatter(val);
                }
                // Lưu giá trị và databasejs
                ffield.setControlValue(val);

                return val;
            }, fformModel);
        }
        else {
            // observable with default value
            var initialValue = ffield.value;
            if (ffield.formatter) {
                initialValue = ffield.formatter(initialValue);
            }
            result = ko.observable(initialValue);
        }

        if (ffield.parser) {
            result.fParse = ffield.parser;
        }

        container[ffield.key] = result;
        //#endregion <VALUE>

        //#region <VALIDATE>
        // Get all validator expression and convert to function
        // p27: , p28: , p29: , p30: , p22: , p23: , p13: bắt buộc nhập
        ffield.validators = getValidators(ffield, container);
        // Store selected: selected5c10
        if (ffield.getTypeId() == "c10") {
            //container["selected" + ffield.key] = ko.observable("");
            //ffield.catalogSelected = ffield.getCatalogSelected();
            container[ffield.key + "CatalogSelected"] =
                ko.dependentObservable(function () {
                    var value = container[ffield.key]();
                    //Hopcv:160814:đối tượng đầu tiên của hàm ko.utils.arrayFirst là 1 mảng
                    var key = "catalog" + ffield.key;
                    return ko.utils.arrayFirst(fformModel[key], function (city) {
                        return city.catalogid === value;
                    });

                    //return ko.utils.arrayFirst(fformModel["catalog" + ffield.key](), function (city) {
                    //    return city.catalogid === value;
                    //});
                }, fformModel);
        }
        // Store error message
        // container[ffield.key + "Error"] = ko.observable("");
        container[ffield.key + "Warning"] = ko.observable("");
        container[ffield.key + "Error"] = ko.dependentObservable(function () {
            var error;
            var i = 0;
            var value;
            /* I - Validate by type of field */
            try {
                // Lấy lại value của ffield thông qua key, tìm bên trong container. 
                // Thực hiện parse giá trị để xác định đúng kiểu dữ liệu rồi mới trả về.
                value = getFieldValueByKey(ffield.key, container);
                if (value === null || value === undefined) {
                    error = "Sai định dạng dữ liệu";
                }
                if (value == '') return "";
            }
            catch (exception) {
                error = exception;
            }
            /* II - Validate by other rules of field */
            // Nếu không lỗi và trường hiện tại được cấu hình công thức validate thì thực thi các hàm này
            while (!error && i < ffield.validators.length) {
                var valor = ffield.validators[i];
                try {
                    if (!valor.validate(value, ffield)) {
                        error = valor.message;
                        break;
                    }
                }
                catch (exception) {
                    error = exception;
                }
                i++;
            }
            return error;
        }, fformModel);

        // Do dismiss error message on click
        container[ffield.key + "DismissError"] = function () {
        };
        // Do dismiss warning message on click
        container[ffield.key + "DismissWarning"] = function () {
            container[ffield.key + "Warning"]("");
        };
        // Do validation onBlur
        container[ffield.key + "Validate"] = function () {
            // Lấy lại value của ffield thông qua key, tìm bên trong container. 
            // Thực hiện parse giá trị để xác định đúng kiểu dữ liệu rồi mới trả về.
            var value;
            try {
                value = getFieldValueByKey(ffield.key, container);
            } catch (e) {
                value = "";
            }

            if (value === null || value === undefined) {
                value = "";
            }
            // Nếu là kiểu datetime
            if (ffield.formatter) {
                value = ffield.formatter(value);
            }
            
            ffield.setControlValue(value);

            // Xử lý lưu thêm object selected of dropdownlist
            var valueSelected;
            try {
                valueSelected = getFieldValueByKey(ffield.key + "CatalogSelected", container);
            } catch (e) {
                valueSelected = "";
            }

            if (valueSelected === null || valueSelected === undefined) {
                valueSelected = "";
            }
            // Lưu giá trị và databasejs
            if (typeof valueSelected === 'object') {
                valueSelected = JSON.stringify(valueSelected);
                valueSelected = valueSelected.replace(/"/g, "\\\"");
            }
            ffield.setCatalogSelected(valueSelected);
        };
        //#endregion <VALIDATE>
    };

    //View model for the whole form
    var partModel = function (schema) {

        //#region <COMPILER TABLE>
        // View model for a table row ~~ Table.Row
        var frowModel = function (cols, table, form) {
            var othis = this;
            this.index = ko.dependentObservable(function () {
                return table ? table.indexOf(this) + 1 : -1;
            }, this);
            this.isLast = ko.dependentObservable(function () {
                var length = table ? table().length : 0;
                return othis.index() == length;
            }, this);
            this.rowClass = ko.dependentObservable(function () {
                return othis.isLast() ? "frow last" : "frow";
            }, this);

            var colDict = table.colDict = getElementDict(cols);

            var getColRefs = function (node) {
                var refs = [];
                if (node.dependencies && node.dependencies.length) {
                    var dependencies = node.dependencies;
                    for (var i = 0, len = dependencies.length; i < len; i++) {
                        var dependency = dependencies[i];
                        if (colDict.hasOwnProperty(dependency)) {
                            refs.push(colDict[dependency]);
                        }
                        else { //external dependency detected
                            return [];
                        }
                    }
                }
                return refs;
            };
            var sortedCols = topoSort(cols, getColRefs);

            for (var j = 0, colCount = sortedCols.length; j < colCount; j++) {
                //- - define each column
                var col = sortedCols[j];
                var colKey = col.key;
                compileField(col, this, form);
            }

            //define the remove function
            this.remove = function () {
                if (table) {
                    table.remove(this);
                }
                $(document).triggerHandler("rowRemoved", [table, this, table.indexOf(this)]);
            } .bind(this);
        }; //~~Table.NewRow()

        var compileAddRow = function (table, tableModel, partModel) {
            return function (data) {
                var row = new frowModel(table.cols, tableModel, partModel);
                if (data) {
                    f.utils.copyDataToObservable(data, row);
                }
                $(document).triggerHandler("rowLoaded", [table.key, row]);
                tableModel.push(row);
                $(document).triggerHandler("rowAdded", [table.key, row, tableModel.indexOf(row)]);
            };
        };
        //#endregion <COMPILER TABLE>

        //#region <TOPOSORT>
        // eForm.database.FindControlsByProp("p15", dependency); Need replace by other
        // Tìm danh sách các Field phụ thuộc vào field hiện tại
        var getRefFields = function (field) {
            var refs = [];
            var depend1 = field.getDependenciesOfValue();
            var depend2 = field.getDependenciesOfValidate();
            field.dependencies = $.merge(depend1, depend2);
            if (field && field.dependencies && field.dependencies.length) {
                var dependencies = field.dependencies;
                for (var i = 0, len = dependencies.length; i < len; i++) {
                    var dependency = dependencies[i];
                    var ctrls = eForm.database.FindControlsByProp("p15", dependency, formid);
                    if (ctrls.length == 1) {
                        refs.push(ctrls[0]);
                    }
                    else {
                        //external dependency detected
                        refs = [];
                        break;
                    }
                }
            }
            return refs;
        };

        var elements = topoSort(schema, getRefFields);
        //#endregion <TOPOSORT>

        // compile từng phần tử trong eForm
        for (var i = 0, elemCount = elements.length; i < elemCount; i++) {
            var element = elements[i];
            //            if (element.cols && element.cols.length) {
            //                //compile Table
            //                this[element.key] = ko.observableArray([]);
            //                this[element.key + "AddRow"] = compileAddRow(element, this[element.key], this).bind(this);
            //            }
            //            else {
            //compile Field
            compileField(element, this);
            //}
        }

        //detach this form
        this.detach = function () {
            f.utils.detachForm(this, schema.key);
            $(document).triggerHandler("partUnloaded", [schema.key, this]);
        } .bind(this);
    };

    //Finally, add only 02 public function to fformModel
    fformModel.fromSchema = function (schema, pFormId) {
        formid = pFormId;
        return new partModel(schema);
    };

    fformModel.fromCatalog = function (catalogs) {
        // CatalogItem Model
        var catalogModel = function (data) {
            this.catalogid = data.catalogid;
            this.catalogname = data.catalogname;
            // mới thêm
            this.globalcode = data.globalcode;
        } .bind(this);

        // Init CatalogModel for binding in view like fformModel.catalog_9c10.
        for (var i = 0, cataCount = catalogs.length; i < cataCount; i++) {
            var catalogList = [];//ko.observableArray([]); //[];
            var catalog = catalogs[i];
            for (var j = 0, itemCount = catalog.lstItem.length; j < itemCount; j++) {
                var item = catalog.lstItem[j];
                var cataModel = new catalogModel(item);
                catalogList.push(cataModel);
            }
            fformModel["catalog" + eForm.efTools._formId_ + "_" + catalog.Id + "c10"] = catalogList;
        }
    };

})(window.f = window.f || {}, window.fformModel = window.fformModel || {}, jQuery, JSON, eForm);