/* 
* eForm.Control V2.1.0
*
* Copyright 2012: Bkav - Bso - Phòng 2 - eGate Team
* Created by: AnhNGTB
* Edited by: CuongNT, AnhNVA, TienBV 
*
* Mô tả về lớp eForm.efTools.
* Public property
* Private property
* Convention
* Public method
*/

(function (eForm, $, undefined) {
    // private
    function EControl() {
        //var rawProperies;
        var id; // id: là id tự sinh tăng dần từ 1... không phải id database control
        var typeId;
        var posRow = 0;
        var posOrder = 0;
        // Sẽ được khởi tạo qua hàm AddProperty
        var properties = new Array();
        this.init = function (ID, TPID) {//LSTProp
            id = ID < 0 ? 0 : ID;
            typeId = TPID < 0 ? 0 : TPID;
            posRow = 0;
            //properties = LSTProp instanceof Array ? LSTProp : new Array();
            properties = new Array();
        }.bind(this);
        this.getId = function () { return id; }.bind(this);
        this.getTypeId = function () { return typeId; }.bind(this);
        this.getPosRow = function () { return posRow; }.bind(this);
        this.setPosRow = function (val) { posRow = val; }.bind(this);
        this.getPosOrder = function () { return posOrder; }.bind(this);
        this.setPosOrder = function (val) { posOrder = val; }.bind(this);
        this.getProperties = function () { return properties; }.bind(this);
        this.setProperties = function (val) { properties = val; }.bind(this);
        this.getProperty = function (val) {
            var result = false;
            for (var itm in properties) {
                if (typeof properties[itm].getId === 'function' && properties[itm].getId() == val) {
                    result = properties[itm];
                    break;
                }
            }
            return result;
        }.bind(this);
        this.setProperty = function (val) {
            if (!val.getId())
                return false;
            for (var itm in properties) {
                if (properties[itm].getId() == val.getId()) {
                    properties[itm] = val;
                    return true;
                }
            }
            return false;
        }.bind(this);

        var othis = this;
        // ControlId: là ExFieldID hoặc MetaId trong database tương ứng. Lấy Id control trên form động sử dụng getKey().
        this.getControlId = function () {
            var result = othis.getProperty("p15");
            if (result == false)
                throw "ControlId không thể rỗng.";
            else
                return result.getValue();
        }.bind(this);
        this.getControlValue = function () {
            var result = othis.getProperty("p1");
            result = !result ? "" : result.getValue();
            return result;
        }.bind(this);
        this.getCatalogSelected = function () {
            var result = othis.getProperty("p31");
            if (result == false) return "";
            else {
                return result.getValue();
            }
        }.bind(this);
        this.getControlMask = function () {
            var result = othis.getProperty("p19");
            return result == false ? "" : result.getValue();
        }.bind(this);
        this.getControlGlobalCode = function () {
            var result = othis.getProperty("p24");
            return result == false ? "" : result.getValue();
        }.bind(this);
        this.getControlReadOnly = function () {
            var result = othis.getProperty("p16");
            return result == false ? "" : result.getValue();
        }.bind(this);
        this.getRequirementControl = function () {
            var result = othis.getProperty("p13");
            return result == false ? false : result.getValue();
        }.bind(this);
        this.getControlTitle = function () {
            var result = othis.getProperty("p17");
            return result == false ? "" : result.getValue();
        }.bind(this);
        // Dùng cho <EVALUATING> bên bkav.egate.compiler.js
        this.getDependenciesOfValue = function () {
            var result = othis.getProperty("p26");
            if (result == false) return "";
            else {
                var values = result.getValue().replace(/\s+/g, '');
                //return [values];
                if (values.split("@").length >= 2) {
                    return values.split("@")[1].split(",");
                }
            }
            return "";
        }.bind(this);
        this.getExpressionOfValue = function () {
            var result = othis.getProperty("p26");
            if (result == false) return "";
            else {
                var values = result.getValue().replace(/\s+/g, '');
                //return values;
                if (values.split("@").length >= 2) {
                    return values.split("@")[0];
                }
            }
            return "";
        }.bind(this);
        // Dùng cho <VALIDATING> bên bkav.egate.compiler.js
        this.getDependenciesOfValidate = function () {
            var result = othis.getProperty("p28");
            if (result == false) return "";
            else {
                var values = result.getValue().replace(/\s+/g, '');
                if (values.split("@").length >= 2) {
                    return values.split("@")[1].split(",");
                }
            }
            return "";
        }.bind(this);
        this.getExpressionOfValidate = function () {
            var result = othis.getProperty("p28");
            if (result == false) return "";
            else {
                var values = result.getValue().replace(/\s+/g, '');
                if (values.split("@").length >= 2) {
                    return values.split("@")[0];
                }
            }
            return "";
        }.bind(this);
        /* Lấy Id control trên form động. getKey() return: _15c10, _14c9. Sử dụng để sinh id control động không trùng lắp */
        this.getKey = function () { return eForm.efTools._formId_ + "_" + othis.getControlId() + othis.getTypeId(); }.bind(this);
        // Quy ước đặt tên id cho các control động
        this.getTitleId = function () { return eForm.cfgPrefixId.divLabelId + othis.getKey(); }.bind(this);
        // Lấy Id của div chứa label + control trong form động.
        this.getPanelId = function () { return eForm.efTools._formId_ + eForm.cfgPrefixId.divControlId + othis.getId(); }.bind(this);

        this.setControlValue = function (value) {
            var result = othis.getProperty("p1");
            if (result != false) result.setValue(value);
        }.bind(this);
        this.setControlId = function (value) {
            var result = othis.getProperty("p15");
            if (result != false) result.setValue(value);
        }.bind(this);
        this.setCatalogSelected = function (value) {
            var result = othis.getProperty("p31");
            if (result != false) result.setValue(value);
        }.bind(this);
        this.setPropertyValue = function (proId, value) {
            var result = othis.getProperty(proId);
            if (result != false) result.setValue(value);
        }.bind(this);
        this.setControlTitle = function (value) {
            var result = othis.getProperty("p17");
            if (result != false) result.setValue(value);
        }.bind(this);
        this.setControlGlobalCode = function (value) {
            var result = othis.getProperty("p24");
            if (result != false) result.setValue(value);
        }.bind(this);
        this.AddProperty = function (val) {
            if (!val.getId()) return;
            if (properties.length == 0)
                properties[0] = val;
            else
                properties[properties.length] = val;
        }.bind(this);
    }
    var EProperty = function () {
        var id;
        var value;
        var isDefault;
        var isMultiline;
        var uiName = "";
        var uiValue = "";
        var uiDescription = "";
        this.init = function (ID, VALUE, ISDEFAULT, ISMULTILINE, UNAME, UVALUE, UDESC) {
            id = ID < 0 ? 0 : ID;
            value = VALUE == undefined ? "" : VALUE;
            isDefault = ISDEFAULT == undefined ? false : ISDEFAULT;
            isMultiline = ISMULTILINE == undefined ? false : ISMULTILINE;
            uiName = UNAME;
            uiValue = UVALUE;
            uiDescription = UDESC;
        }.bind(this);
        this.getId = function () { return id; }.bind(this);
        this.getValue = function () {
            var val = value;
            if (typeof val == "string") {
                val = val.replace(/&quot;/g, "\"");
            }
            //return unescape(value);
            return val;
        }.bind(this);
        this.setValue = function (val) {
            if (typeof val == "string") {
                val = val.replace(/"/g, "&quot;");
            }
            //value = escape(val);
            value = val;
        }.bind(this);
        this.IsDefault = function () { return isDefault; }.bind(this);
        this.IsMultiLine = function () { return isMultiline; }.bind(this);
        this.getUIName = function () { return uiName; }.bind(this);
        this.getUIValue = function () { return uiValue; }.bind(this);
        this.getUIDescription = function () { return uiDescription; }.bind(this);
    };
    function listItem(text, value, selected) {
        this.Text = text;
        this.Value = value;
        this.Selected = selected;
    };
    var parseListItem = function (strLstItems, sepString) {
        var result = new Array();
        if (sepString == undefined || sepString == "")
            sepString = "\n";
        var lstItms = strLstItems.split(sepString);

        for (var i = 0; i < lstItms.length; i++) {
            var txt = lstItms[i].trim();
            var sel = false;
            if (txt.indexOf("#") == 0) {
                sel = true;
                txt = txt.substring(1);
            }
            result[i] = new listItem(txt, txt, sel);
        }
        return result;
    };
    var controlClass = {
        constructor: function (options) {
            jQuery.extend(true, this, controlClass);
        },
        // Sử dụng khi tạo mới control trên form
        init: function (ctrlId, tpId) {
            if (!this.IsExistControl(tpId)) return false;
            var result = new EControl();
            result.init(ctrlId, tpId);
            for (var itm = 0, l = eForm.controllib[tpId].length; itm < l; itm++) {
                var lstPro = eForm.propertylib[eForm.controllib[tpId][itm]];
                if (lstPro == null) continue;
                if (lstPro.length == 0) continue;
                // Lấy index của property chứa giá mặc định
                var defIdx = 0;
                if (lstPro.length > 1) {
                    for (var i = 0; i < lstPro.length; i++) {
                        if (lstPro[i].IsDefault()) {
                            defIdx = i;
                            break;
                        }
                    }
                }
                /* Luôn luôn thêm mới, không dùng hàm result.AddProperty(lstPro[defIdx]); sẽ chỉ copy con trỏ mà thôi
                * Gây ra lỗi control thêm sau sẽ lấy toàn bộ property control tạo trước nó vì cùng trỏ về 1 biến duy nhất  
                */
                result.AddProperty(new eForm.PropertyEntity(lstPro[defIdx].getId(), lstPro[defIdx].getValue()));
            }
            return result;
        },
        IsExistControl: function (ctrlId) {
            if (eForm.controllib[ctrlId] instanceof Array)
                return true;
            return false;
        },
        SetPropertieOfControl: function (pid, pval, ctrlId, isMultiCtrl) {
            var ctrl = $("#" + ctrlId);
            var ctrlLbl = $("#" + (isMultiCtrl == true ? eForm.cfgPrefixId.divLabelId : "") + ctrlId);
            //var isRequired = false;
            switch (pid) {
                case "p1":  // Giá trị nhập
                    if (pval != "")
                        ctrl.html(pval);
                    break;
                case "p2":  // In đậm
                    ctrlLbl.css("font-weight", pval);
                    break;
                case "p3":  // In nghiêng
                    ctrlLbl.css("font-style", pval);
                    break;
                case "p4":  // Cỡ chữ
                    ctrlLbl.css("font-size", pval);
                    break;
                case "p5":  // Màu chữa
                    ctrlLbl.css("color", pval);
                    break;
                case "p6":  // Chiều rộng
                    if (pval != "")
                        ctrl.parent().css("width", pval);
                    break;
                case "p7":  // Chiều cao
                    if (pval != "")
                        ctrl.parent().css("height", pval);
                    break;
                case "p8":  // Gạch chân
                    ctrlLbl.css("text-decoration", pval);
                    break;
                case "p9":  // Font chữ
                    ctrlLbl.css("font-family", pval);
                    break;
                case "p10": // Màu nền
                    ctrlLbl.css("background-color", pval);
                    break;
                case "11":  // Danh sách (ListItem)
                    break;
                case "12":  // Số cột
                    break;
                case "13":  // Bắt buộc nhập --> Chuyên ra Textbox
                    //if (pval == "true") {
                    //    isRequired = true;
                    //}
                    break;
                case "14":  // Thông báo lỗi bắt buộc nhập --> Chuyên ra Textbox
                    //if (!isRequired) break;
                    //var tmpRequiredId = ctrlId + "_required";
                    //$("<span></span>")
                    //    .attr("id", tmpRequiredId)
                    //    .attr("controlValidate", ctrlId)
                    //    .html(isRequired ? (pval == "" ? "*" : pval) : "")
                    //    .css({ color: "red", display: "none", "font-style": "italic" })
                    //    .addClass("cssErr")
                    //    .insertAfter("#" + ctrlId);
                    //$("#" + ctrlId)
                    //    .blur(function () {
                    //        var tmpval = $(this).val().replace(/_/g, "");
                    //        tmpval = tmpval.replace("//", "");

                    //        if (tmpval == "") {
                    //            $("#" + tmpRequiredId).css({ display: "inline" });
                    //        } else {
                    //            $("#" + tmpRequiredId).css({ display: "none" });
                    //        }
                    //    });
                    break;
                case "15":  // ControlId
                    break;
                case "p16": // Chỉ đọc
                    if (pval == "true")
                        ctrl.attr("readonly", pval);
                    else
                        ctrl.removeAttr("readonly");
                    break;
                case "17":  // Tiêu đề (nhãn) --> Chuyên ra Textbox, Dropdownlist
                    break;
                case "p18": // Căn lề
                    ctrlLbl.css("text-align", pval);
                    break;
                case "19":  // Định dạng (Mask) --> Chuyên ra Textbox
                    break;
                case "p20": // Chữ hoa
                    ctrl.css("text-transform", pval);
                    break;
                case "p21": // Phụ thuộc
                    break;
                case "p22": //Độ dài tối đa
                    break;
                case "p23": //:	Khoảng cho phép
                    break;
                case "p24": //:	GlobalCode
                    break;
                case "p25": //:	Tự tính C#
                    break;
                case "p26": //:	Tự tính JS
                    break;
                case "P27": //:	Validate C#
                    break;
                case "P28": //:	Validate JS
                    break;
                case "p29": //:	Regex
                    break;
                case "p30": //:	Số không âm
                    break;
                case "p31": //:	CatalogSelected
                    break;
            }
        },
        LoadProperty: function (ctrl, propIdx, pnlPropertyId) {
            this._base.LoadProperties(ctrl, propIdx, pnlPropertyId);
        },
        LoadProperties: function (ctrl, currPro, pnlPropertyId, isMultiCtrl) {
            // ControlId
            var ctrlId = ctrl.getKey();
            var tmpLblPrefix = isMultiCtrl == true ? eForm.cfgPrefixId.divLabelId : "";
            // ControlPanelId
            var ctrlPnlId = eForm.cfgPrefixId.divControlId + ctrl.getId();
            var property = currPro;
            // PropertyPanelId
            var propId = eForm.cfgPrefixId.divPropertyId + property.getId();

            // Danh sách giá trị của 1 thuộc tính: [Time new roman, Arial,...]
            var lstPropLibTmp = eForm.propertylib[property.getId()];

            // inner class xử lý load & quản lý thay đổi giá trị (event change) của các properties
            var icLoadProp = {
                inputCssText: function (cssName) {
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .change(function () {
                            if ($(this).val() == "") {
                                //$("#" + ctrlPnlId).css(cssName, ""); //AnhNVa: Lý do bỏ: Không phải css cho panel mà chỉ css cho text
                                $("#" + tmpLblPrefix + ctrlId).css(cssName, "");
                            } else {
                                //$("#" + ctrlPnlId).css(cssName, $(this).val()); //AnhNVa: Lý do bỏ: Không phải css cho panel mà chỉ css cho text
                                $("#" + tmpLblPrefix + ctrlId).css(cssName, $(this).val());
                            }
                            if ($(this).val() != null) {
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                            }
                        })
                        .trigger('change');
                }
                ,
                inputCssSize: function (cssName, isPanel) {
                    if (isPanel != true) isPanel = false;
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .change(function (e) {
                            $(this).val(trim($(this).val()));
                            // Neu giá trị là chữ hoặc nhỏ hơn 0, thì để trống ( <=> lấy mặc định)
                            if (parseInt($(this).val()) > 0)
                                var changlamgi = true;
                            else
                                $(this).val("");

                            if ($(this).val() == "") {
                                if (!isPanel) $("#" + ctrlId).css(cssName, "");
                                $("#" + ctrlPnlId).css(cssName, "");
                            } else {
                                var tmpV = $(this).val().substring($(this).val().length - 1);
                                if (parseInt(tmpV) > -1) $(this).val($(this).val() + "px");
                                if (!isPanel) $("#" + ctrlId).css(cssName, $(this).val());
                                $("#" + ctrlPnlId).css(cssName, $(this).val());
                            }
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        })
                        .trigger('change');
                }
                ,
                selectCssOption: function (cssName, isControl) {
                    $("<select></select>")
                        .attr("id", propId)
                        .attr("name", propId)
                        .appendTo("#" + pnlPropertyId)
                        .change(function (e) {
                            if ($(this).val() == "") {
                                if (isControl == true)
                                    $("#" + ctrlId).css(cssName, "");
                                else
                                    $("#" + tmpLblPrefix + ctrlId).css(cssName, "");
                            } else {
                                if (isControl == true)
                                    $("#" + ctrlId).css(cssName, $(this).val());
                                else
                                    $("#" + tmpLblPrefix + ctrlId).css(cssName, $(this).val());
                            }
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        })
                        .trigger('change');

                    for (var i = 0, l = lstPropLibTmp.length; i < l; i++) {
                        if (lstPropLibTmp[i].getValue() == property.getValue()) {
                            $("<option />").attr("selected", "selected")
                                .attr("value", lstPropLibTmp[i].getValue())
                                .html(lstPropLibTmp[i].getUIValue())
                                .appendTo("#" + propId);
                        }
                        else {
                            $("<option />").attr("value", lstPropLibTmp[i].getValue())
                                .html(lstPropLibTmp[i].getUIValue())
                                .appendTo("#" + propId);
                        }
                    }
                }
                ,
                selectOption: function () {
                    $("<select></select>")
                        .attr("id", propId)
                        .attr("name", propId)
                        .appendTo("#" + pnlPropertyId)
                        .change(function (e) {
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        })
                        .trigger('change');

                    for (var i = 0, l = lstPropLibTmp.length; i < l; i++) {
                        if (lstPropLibTmp[i].getValue() == property.getValue())
                            $("<option />").attr("selected", "selected")
                                .attr("value", lstPropLibTmp[i].getValue())
                                .html(lstPropLibTmp[i].getUIValue())
                                .appendTo("#" + propId);
                        else
                            $("<option />").attr("value", lstPropLibTmp[i].getValue())
                                .html(lstPropLibTmp[i].getUIValue())
                                .appendTo("#" + propId);
                    }
                }
                ,
                checkOption: function () {
                    $("<div style='clear:both;'></div>")
                        .attr("id", propId)
                        .attr("name", propId)
                        .appendTo("#" + pnlPropertyId)
                        .change(function (e) {
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        })
                        .trigger('change');

                    for (var i = 0, l = lstPropLibTmp.length; i < l; i++) {
                        $("<div></div>")
                            .attr("id", "divCbx" + propId + i)
                            .attr("name", "divCbx" + propId + i)
                            .appendTo("#" + propId);
                        $("<input/>")
                            .attr("type", "checkbox")
                            .attr("id", lstPropLibTmp[i].getId() + "cbx" + i)
                            .attr("name", lstPropLibTmp[i].getId() + "cbx" + i)
                            .val(lstPropLibTmp[i].getValue())
                            .appendTo("#divCbx" + propId + i);
                        $("<span/>")
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#divCbx" + propId + i);
                    }
                }
            };
            var values;
            var valueArr;
            var express, msg, phuthuoc;
            switch (property.getId()) {
                case "p1":  // giá trị control
                    $("<input />")
                    .attr("id", propId)
                    .attr("name", propId)
                    .val(property.getValue())
                    .appendTo("#" + pnlPropertyId)
                    .keyup(function () {
                        $("#" + ctrl.getId() + ctrl.getTypeId()).val($(this).val());
                        if ($(this).val() != null)
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                    })
                    .trigger('keyup');
                    break;
                case "p2":  //font bold
                    icLoadProp.selectCssOption("font-weight");
                    break;
                case "p3":  // font italic
                    icLoadProp.selectCssOption("font-style");
                    break;
                case "p4":  // font size -- xử lý đơn vị mặc định là px nếu ko xác định đơn vị tính kích thước
                    icLoadProp.inputCssSize("font-size");
                    break;
                case "p5":  // font color
                    icLoadProp.inputCssText("color");
                    break;
                case "p6":  // width -- xử lý đơn vị mặc định là px nếu ko xác định đơn vị tính kích thước
                    icLoadProp.inputCssSize("width", true);
                    break;
                case "p7":  // height -- xử lý đơn vị mặc định là px nếu ko xác định đơn vị tính kích thước
                    icLoadProp.inputCssSize("height", true);
                    break;
                case "p8":  // font underline
                    icLoadProp.selectCssOption("text-decoration");
                    break;
                case "p9":  // font name - font-family
                    icLoadProp.selectCssOption("font-family");
                    break;
                case "p10": // background color
                    icLoadProp.inputCssText("background-color");
                    break;
                case "p11": // Danh sách dữ liệu của dropdownlis, checkboxlis
                case "p12": // Số cột
                case "p13": // Bắt buộc nhật
                case "p14": // Thông báo lỗi của bắt buộc nhập
                    break;
                case "p15":  //control Id
                    $("<span></span>")
                        .html(property.getValue() == "" ? ctrlId : property.getValue())
                        .css({ "font-weight": "bold", "color": "blue" })
                        .appendTo("#" + pnlPropertyId);
                    break;
                case "p16": // is ReadOnly
                    icLoadProp.selectOption();
                    break;
                case "p17":  //text/label
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .keyup(function () {
                            $("#" + eForm.cfgPrefixId.divLabelId + ctrl.getKey()).html($(this).val()); //ctrl.getId() + ctrl.getTypeId() + "_lbl"
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        });
                    //.trigger('keyup');
                    break;
                case "p18":  // text-align
                    icLoadProp.selectCssOption("text-align");
                    break;
                case "p19": // Kiểu dữ liệu
                    break;
                case "p20":  // text transform
                    icLoadProp.selectCssOption("text-transform", true);
                    break;
                case "p21":  // Lấy dữ liệu từ  id
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .keyup(function () {
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        });
                    break;
                case "p22": // Chiều dài tối đa
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .keyup(function () {
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        });
                    break;
                case "p23": // Khoảng cho phép
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .keyup(function () {
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        });
                    break;
                case "p24": // GlobalCode
                    $("<span />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .html(property.getValue())
                        .appendTo("#" + pnlPropertyId);
                    break;
                case "p25": // Tự tính C#
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .keyup(function () {
                            if ($(this).val() != null)
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                        })
                        .focus(function () {
                            $(".controlId").css('visibility', 'visible');
                        })
                        .focusout(function () {
                            $(".controlId").css('visibility', 'hidden');
                        });
                    break;
                case "p26": // Tự tính JS
                    values = property.getValue();
                    valueArr = values.split('@');
                    // express, msg;
                    if (valueArr.length != 2) {
                        express = values;
                        msg = "";
                    }
                    else {
                        express = $.trim(valueArr[0]);
                        msg = $.trim(valueArr[1]);
                    }
                    $("<div style='clear:both;'></div>")
                        .attr("id", "divjsEx" + propId)
                        .attr("name", "divjsEx" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Biểu thức:")
                        .appendTo("#divjsEx" + propId);
                    $("<input />")
                        .attr("id", "txtEx" + propId)
                        .attr("name", "txtEx" + propId)
                        .val(express)
                        .appendTo("#divjsEx" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                express = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + msg);
                            }
                        })
                        .trigger('keyup')
                        .focus(function () {
                            $(".controlId").css('visibility', 'visible');
                        })
                        .focusout(function () {
                            $(".controlId").css('visibility', 'hidden');
                        });
                    $("<div></div>")
                        .attr("id", "divjsMsg" + propId)
                        .attr("name", "divjsMsg" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Phụ thuộc:")
                        .appendTo("#divjsMsg" + propId);
                    $("<input />")
                        .attr("id", "txtMsg" + propId)
                        .attr("name", "txtMsg" + propId)
                        .val(msg)
                        .appendTo("#divjsMsg" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                msg = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + msg);
                            }
                        })
                        .trigger('keyup')
                        .focus(function () {
                            $(".controlId").css('visibility', 'visible');
                        })
                        .focusout(function () {
                            $(".controlId").css('visibility', 'hidden');
                        });
                    break;
                case "p27": // Validate C#
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .val(property.getValue())
                        .appendTo("#" + pnlPropertyId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                //ctrl.setPropertyValue(property.getId(), $(this).val());
                                ctrl.setPropertyValue(property.getId(), $(this).val());
                            }
                        })
                        .focus(function () {
                            $(".controlId").css('visibility', 'visible');
                        })
                        .focusout(function () {
                            $(".controlId").css('visibility', 'hidden');
                        });
                    break;
                case "p28": // Validate JS
                    values = property.getValue();
                    valueArr = values.split('@');
                    //express, msg;
                    if (valueArr.length != 3) {
                        express = values;
                        msg = "";
                        phuthuoc = "";
                    }
                    else {
                        express = $.trim(valueArr[0]);
                        phuthuoc = $.trim(valueArr[1]);
                        msg = $.trim(valueArr[2]);
                    }
                    $("<div style='clear:both;'></div>")
                        .attr("id", "divjsEx" + propId)
                        .attr("name", "divjsEx" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Biểu thức:")
                        .appendTo("#divjsEx" + propId);
                    $("<input />")
                        .attr("id", "txtEx" + propId)
                        .attr("name", "txtEx" + propId)
                        .val(express)
                        .appendTo("#divjsEx" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                express = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + phuthuoc + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + phuthuoc + "@" + msg);
                            }
                        })
                        .trigger('keyup')
                        .focus(function () {
                            $(".controlId").css('visibility', 'visible');
                        })
                        .focusout(function () {
                            $(".controlId").css('visibility', 'hidden');
                        });
                    $("<div></div>")
                        .attr("id", "divjsMsg" + propId)
                        .attr("name", "divjsMsg" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Thông báo lỗi:")
                        .appendTo("#divjsMsg" + propId);
                    $("<input />")
                        .attr("id", "txtMsg" + propId)
                        .attr("name", "txtMsg" + propId)
                        .val(msg)
                        .appendTo("#divjsMsg" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                msg = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + phuthuoc + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + phuthuoc + "@" + msg);
                            }
                        })
                        .trigger('keyup');
                    $("<div></div>")
                        .attr("id", "divjsPthuoc" + propId)
                        .attr("name", "divjsPthuoc" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Phụ thuộc:")
                        .appendTo("#divjsPthuoc" + propId);
                    $("<input />")
                        .attr("id", "txtPthuoc" + propId)
                        .attr("name", "txtPthuoc" + propId)
                        .val(phuthuoc)
                        .appendTo("#divjsPthuoc" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                phuthuoc = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + phuthuoc + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + phuthuoc + "@" + msg);
                            }
                        })
                        .trigger('keyup')
                        .focus(function () {
                            $(".controlId").css('visibility', 'visible');
                        })
                        .focusout(function () {
                            $(".controlId").css('visibility', 'hidden');
                        });
                    break;
                case "p29": // Regex
                    values = property.getValue();
                    valueArr = values.split('@');
                    //express, msg;
                    if (valueArr.length != 2) {
                        express = values;
                        msg = "";
                    }
                    else {
                        express = $.trim(valueArr[0]);
                        msg = $.trim(valueArr[1]);
                    }
                    $("<div style='clear:both;'></div>")
                        .attr("id", "divjsEx" + propId)
                        .attr("name", "divjsEx" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Biểu thức:")
                        .appendTo("#divjsEx" + propId);
                    $("<input />")
                        .attr("id", "txt" + propId)
                        .attr("name", "txt" + propId)
                        .val(express)
                        .appendTo("#divjsEx" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                express = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + msg);
                            }
                        })
                        .trigger('keyup');
                    $("<div></div>")
                        .attr("id", "divjsMsg" + propId)
                        .attr("name", "divjsMsg" + propId)
                        .appendTo("#" + pnlPropertyId);
                    $("<span  style='float: left; min-width: 100px;'/>")
                        .html("Thông báo lỗi:")
                        .appendTo("#divjsMsg" + propId);
                    $("<input />")
                        .attr("id", "txt" + propId)
                        .attr("name", "txt" + propId)
                        .val(msg)
                        .appendTo("#divjsMsg" + propId)
                        .keyup(function () {
                            if ($(this).val() != null) {
                                msg = $.trim($(this).val());
                                //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), express + "@" + msg);
                                ctrl.setPropertyValue(property.getId(), express + "@" + msg);
                            }
                        })
                        .trigger('keyup');

                    break;
                case "p30": // Không âm
                    $("<input />")
                        .attr("id", propId)
                        .attr("name", propId)
                        .attr("type", "checkbox")
                        .attr("checked", property.getValue() == 'true' ? true : false)
                        .appendTo("#" + pnlPropertyId)
                        .click(function () {
                            //eForm.database.SetControlProperty(ctrl.getId(), property.getId(), $(this).is(':checked'));
                            ctrl.setPropertyValue(property.getId(), $(this).is(':checked'));
                        })
                        .trigger('keyup');
                    break;
            }
        },
        Create: null,
        Add: null,
        Load: null,
        View: null
    };
    var control = controlClass.constructor;
    //------------------------------------------------------------
    var checklistClass = {
        constructor: function (options) {
            this._base = jQuery.extend(true, this, new control());
            jQuery.extend(true, this, checklistClass);
            //jQuery.extend(true, this._base, options);
        },
        Create: function (ctrlId, typeId, panelId) {
            var ctrl = this._base.init(ctrlId, typeId);
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();

            $("<div></div>")
                .attr("id", objId)
                .css({ width: "99%", "float": "left" })
                .appendTo("#" + panelId);

            var lstProperties = ctrl.getProperties();
            var lstItms = new Array();
            var numCols = 1;

            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                switch (pid) {
                    case "p11": // Lấy danh sách các Items (nếu có)
                        if (lstProperties[itm].getValue() != "")
                            lstItms = parseListItem(lstProperties[itm].getValue());
                        break;
                    case "p12": // Lấy số cột để xếp các Items
                        if (lstProperties[itm].getValue() != "")
                            numCols = parseInt(lstProperties[itm].getValue());
                        break;
                    default:
                        this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId);
                        break;
                }
            }
            // Bind các Item vào các cột
            //this.Bind(objId, lstItms, numCols);
            return ctrl;
        },
        Add: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();

            $("<div></div>")
                .attr("id", objId)
                .css({ width: "99%", "float": "left" })
                .appendTo("#" + panelId);

            // <select multiple="multiple" data-bind="options: optionValues, selectedOptions: multipleSelectedOptionValues"></select>
            // $("<select></select>")
            //      .attr("id", objId)
            //      .attr("name", objId)// optionsValue:'catalogid', 
            //      .attr("multiple", "multiple")
            //      .attr("data-bind", "options:fformModel.catalog" + objId + ", optionsText:'catalogname',optionsValue:'catalogid', optionsCaption:' ', selectedOptions:" + objId + ", event:{ change:" + objId + "Validate }")
            //      .css("float", "left")
            //      .appendTo("#" + panelId);

            // Hoặc sử dụng template: http://jsfiddle.net/Jm2Mh/55/
            // <ul data-bind="template: { name: 'choiceTmpl', foreach: choices, templateOptions: { selections: selectedChoices } }"></ul>
            $("<ul></ul>")
                .attr("id", objId)
                .attr("name", objId)
                .attr("data-bind", "template: { name: 'choiceTmpl', foreach: fformModel.catalog" + objId + ", templateOptions: { selections: " + objId + " } }, event:{ change:" + objId + "Validate }")
                .css("float", "left")
                .appendTo("#" + panelId);

            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + eForm.cfgPrefixId.divLabelId + objId).css('width')) - 15) + "px" });

            /*var lstProperties = ctrl.getProperties();
            var lstItms = new Array();
            var numCols = 1;
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
            var pid = lstProperties[itm].getId();
            switch (pid) {
            case "p11": // Lấy danh sách các Items (nếu có)
            if (lstProperties[itm].getValue() != "")
            lstItms = parseListItem(lstProperties[itm].getValue());
            break;
            case "p12": // Lấy số cột để xếp các Items
            if (lstProperties[itm].getValue() != "")
            numCols = parseInt(lstProperties[itm].getValue());
            break;
            default:
            this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId);
            break;
            }
            }
            // Bind các Item vào các cột
            this.Bind(objId, lstItms, numCols);*/
            return true;
        },
        Load: function (ctrl, panelId) {
            return true;
        },
        LoadProperty: function (ctrl, currPro, pnlPropertyId) {
            var ctrlId = ctrl.getKey();
            var property = currPro;
            var propId = eForm.cfgPrefixId.divPropertyId + property.getId();
            switch (property.getId()) {
                case "p11":
                    $("<br />").appendTo("#" + pnlPropertyId);
                    $("<textarea></textarea>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .attr("rows", "7")
                    .css({ width: "350px" })
                    .val(property.getValue())
                    .appendTo("#" + pnlPropertyId)
                    .keyup(function () {
                        var lstItms = parseListItem($(this).val());
                        var numCols = parseInt($("#p12prop").val());
                        var objId = ctrl.getKey();
                        var mchkLst = new eForm.Checklist();
                        mchkLst.ReBind(objId, lstItms, numCols);
                        if ($(this).val() != null) {
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                        }
                    })
                    .trigger('keyup');
                    break;
                case "p12":
                    $("<input />")
                    .attr("id", propId)
                    .attr("name", propId)
                    .attr("type", "text")
                    .val(property.getValue())
                    .appendTo("#" + pnlPropertyId)
                    .keyup(function () {
                        var inum = parseInt($(this).val());
                        if (inum > 0) $(this).val(inum);
                        else return;
                        var lstItms = parseListItem($("#p11prop").val());
                        var numCols = parseInt($(this).val());
                        var objId = ctrl.getKey();
                        var mchkLst = new eForm.Checklist();
                        mchkLst.ReBind(objId, lstItms, numCols);
                        if ($(this).val() != null) {
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                        }
                    })
                    .trigger('keyup');
                    break;
                default:
                    this._base.LoadProperties(ctrl, property.getId(), pnlPropertyId);
                    break;
            }
        }
    };
    var checklist = checklistClass.constructor;
    //------------------------------------------------------------
    var dropdown2Class = {
        constructor: function (options) {
            this._base = jQuery.extend(true, this, new control());
            jQuery.extend(true, this, dropdown2Class);
        },
        Create: function (ctrlId, typeId, panelId) {
            var ctrl = this._base.init(ctrlId, typeId);
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            $("<div></div>")
                .attr("id", eForm.cfgPrefixId.divLabelId + objId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId);
            $("<select></select>")
                .attr("id", objId)
                .attr("name", objId)
                .css("float", "left")
                .appendTo("#" + panelId);
            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                switch (pid) {
                    default:
                        this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
                        break;
                }
            }
            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + eForm.cfgPrefixId.divLabelId + objId).css('width')) - 15) + "px" });
            return ctrl;
        },
        CreateCat: function (ctrlId, panelId, controlId, controlTitle, globalCode) {
            var ctrl = this._base.init(ctrlId, 'c10');
            if (ctrl == undefined || ctrl == false) return false;
            ctrl.setControlId(controlId);
            //ctrl.setControlGlobalCode(globalCode);
            var objId = ctrl.getKey();

            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
            }

            ctrl.setControlTitle(controlTitle);

            this.Add(ctrl, panelId);
            return ctrl;
        },
        Add: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            var titleId = ctrl.getTitleId();
            // Label: set label for control
            var isRequirement = ctrl.getRequirementControl();
            var ctrlTitle = ctrl.getControlTitle();
            if (isRequirement == "true") {
                ctrlTitle += '<span style="color: red;"> (*)</span>';
            }
            $("<div></div>")
                .attr("id", titleId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId)
                .html(ctrlTitle);

            $("<select></select>")
                .attr("id", objId)
                .attr("name", objId)
                .css("float", "left")
                .appendTo("#" + panelId);

            // Load property of control
            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, false);
            }

            $('#' + objId).attr("disabled", "disabled");
            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + titleId).css('width')) - 15) + "px" });

            return true;
        },
        Load: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            var titleId = ctrl.getTitleId();

            // Label: set label for control
            var isRequirement = ctrl.getRequirementControl();
            var ctrlTitle = ctrl.getControlTitle();
            if (isRequirement == "true") {
                ctrlTitle += '<span style="color: red;"> (*)</span>';
            }
            $("<div></div>")
                .attr("id", titleId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId)
                .html(ctrlTitle);

            /*<select data-bind="options:itemModel.collections.categoriesBill, optionsText:'billcode', optionsCaption:' ', value:selection"
            style="width: 100px;">
            </select>*/
            $("<select></select>")
                .attr("id", objId)
                .attr("name", objId)// optionsValue:'catalogid', 
                .attr("data-bind", "options:fformModel.catalog" + objId + ", optionsText:'catalogname',optionsValue:'catalogid', optionsCaption:' ', value:" + objId + ", event:{ change:" + objId + "Validate }")
                .css("float", "left")
                .appendTo("#" + panelId);

            // Load property of control. Xử lý bắt buộc nhập cho Dropdownlist tại đây.
            var isRequired = false;
            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();

                switch (pid) {
                    case "p13": // Bắt buộc nhập
                        if (lstProperties[itm].getValue() == "true") {
                            isRequired = true;
                        }
                        break;
                    case "p14": // Thông báo lỗi của bắt buộc nhập
                        if (!isRequired) break;

                        var tmpRequiredId = objId + "_required";
                        $("<span></span>")
                            .attr("id", tmpRequiredId)
                            .attr("controlValidate", objId)
                            .html(isRequired ? (lstProperties[itm].getValue() == "" ? "*" : lstProperties[itm].getValue()) : "")
                            .css({ color: "red", display: "none", "font-style": "italic" })
                            .addClass("cssErr")
                            .insertAfter("#" + objId);
                        $("#" + objId)
                            .change(function () {
                                var tmpval = jQuery.trim($(this).find(":selected").text());
                                tmpval = tmpval.replace("//", "");
                                if (tmpval == "") {
                                    $("#" + tmpRequiredId).css({ display: "inline" });
                                } else {
                                    $("#" + tmpRequiredId).css({ display: "none" });
                                }
                            });
                        break;
                    default:
                        this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, false);
                }

            }

            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + titleId).css('width')) - 15) + "px" });
            return true;
        },
        View: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            var titleId = ctrl.getTitleId();

            $("<div></div>")
                .attr("id", titleId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId);

            $("<div></div>")
                .attr("id", objId)
                .css({ "float": "left", "padding-top": "3px", "border-bottom": "dotted 1px gray", "min-height": "15px" })
                .appendTo("#" + panelId);

            // Set property cho control
            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                switch (pid) {
                    case "p1":
                        break;
                    case "p17":
                        if (lstProperties[itm].getValue() != "")
                            $("#" + titleId).html(lstProperties[itm].getValue());
                        break;
                    case "p31":
                        $("#" + objId).html(lstProperties[itm].getValue());
                        break;
                    default:
                        this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
                }
            }

            // Tính lại kích thức label và control
            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + titleId).css('width')) - 15) + "px" });
            return true;
        },
        LoadProperty: function (ctrl, currPro, pnlPropertyId) {
            var ctrlId = ctrl.getKey();
            var property = currPro;
            var propId = eForm.cfgPrefixId.divPropertyId + property.getId();
            var lstPropLibTmp = eForm.propertylib[property.getId()];

            switch (property.getId()) {
                case "p1": // Để đây để bỏ qua xử lý p1 ở lớp base
                    break;
                case "p13": // Bắt buộc nhập
                    $("<select></select>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .appendTo("#" + pnlPropertyId)
                    .change(function (e) {

                        var titleId = ctrl.getTitleId();
                        var ctrlTitle = ctrl.getControlTitle();

                        if ($(this).val() == "true") {
                            $("#prop_p14").removeAttr("readonly");

                            // Label: set label for control
                            ctrlTitle += '<span style="color: red;"> (*)</span>';
                            $("#" + titleId)
                                .html(ctrlTitle);

                        } else {
                            $("#" + ctrlId + "_required").html("");
                            $("#prop_p14")
                                .val("")
                                .attr("readonly", "true");

                            // Label: set label for control
                            ctrlTitle = ctrlTitle.replace('<span style="color: red;"> (*)</span>', '');
                            $("#" + titleId)
                                .html(ctrlTitle);
                        }
                        if ($(this).val() != null) {
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                        }
                    })
                    .trigger('change');

                    for (var i = 0, l = lstPropLibTmp.length; i < l; i++) {
                        if (lstPropLibTmp[i].getValue() == property.getValue())
                            $("<option />").attr("selected", "selected")
                            .attr("value", lstPropLibTmp[i].getValue())
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#" + propId);
                        else
                            $("<option />").attr("value", lstPropLibTmp[i].getValue())
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#" + propId);
                    }

                    break;
                case "p14": // Thông báo lỗi bắt buộc nhập
                    var isRequired = false;
                    if ($("#prop_p13").val() == "true") {
                        isRequired = true;
                    }
                    var titleId = ctrl.getTitleId();
                    var ctrlTitle = ctrl.getControlTitle();
                    if (isRequired) {
                        // Label: set label for control
                        ctrlTitle += '<span style="color: red;"> (*)</span>';
                        $("#" + titleId)
                            .html(ctrlTitle);
                    }
                    else {
                        // Label: set label for control
                        ctrlTitle = ctrlTitle.replace('<span style="color: red;"> (*)</span>', '');
                        $("#" + titleId)
                            .html(ctrlTitle);
                    }
                    $("<input/>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .attr("type", "text")
                    .attr(isRequired ? "txt" : "readonly", isRequired ? "" : "true")
                    .val(isRequired ? property.getValue() : "")
                    .appendTo("#" + pnlPropertyId)
                    .keyup(function () {
                        $("#" + ctrlId + "_required").html($(this).val());
                        if ($(this).val() != null)
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                    })
                    .trigger('keyup');
                    break;
                case "p11": // Danh sách (ListItem)
                    $("<br />").appendTo("#" + pnlPropertyId);
                    $("<textarea></textarea>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .attr("readonly", "true")
                    .attr("rows", "5")
                    .css({ width: "350px" })
                    .val(property.getValue())
                    .appendTo("#" + pnlPropertyId);
                    break;
                default:
                    this._base.LoadProperties(ctrl, currPro, pnlPropertyId, true);
                    break;
            }
        }
    };
    var dropdown2 = dropdown2Class.constructor;
    //------------------------------------------------------------
    var textbox2Class = {
        constructor: function (options) {
            this._base = jQuery.extend(true, this, new control());
            jQuery.extend(true, this, textbox2Class);
        },
        Create: function (ctrlId, ctrlTypeId, panelId) {
            var ctrl = this._base.init(ctrlId, ctrlTypeId);
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrlId + "_new_";

            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
            }
            ctrl.setControlId(objId);
            this.Add(ctrl, panelId);
            return ctrl;
        },
        CreateExtend: function (ctrlId, panelId, controlId, controlTitle, globalCode) {
            var ctrl = this._base.init(ctrlId, 'c9');
            if (ctrl == undefined || ctrl == false) return false;
            ctrl.setControlId(controlId);
            //ctrl.setControlGlobalCode(globalCode);
            var objId = ctrl.getKey();

            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
            }

            ctrl.setControlTitle(controlTitle);
            this.Add(ctrl, panelId);
            return ctrl;
        },
        Add: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            var titleId = ctrl.getTitleId();

            // Label: set label for control
            var isRequirement = ctrl.getRequirementControl();
            var ctrlTitle = ctrl.getControlTitle();
            if (isRequirement == "true") {
                ctrlTitle += '<span style="color: red;"> (*)</span>';
            }
            $("<div></div>")
                .attr("id", titleId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId)
                .html(ctrlTitle);

            $("<input />")
                .attr("id", objId)
                .attr("name", objId)
                .attr("type", "text")
                .attr("readonly", true)
                .css("float", "left")
                .appendTo("#" + panelId);

            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
            }

            $('#' + objId).attr("readonly", true);
            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + titleId).css('width')) - 15) + "px" });
            return true;
        },
        Load: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            var titleId = ctrl.getTitleId();

            // Label: set label for control
            var isRequirement = ctrl.getRequirementControl();
            var ctrlTitle = ctrl.getControlTitle();
            if (isRequirement == "true") {
                ctrlTitle += '<span style="color: red;"> (*)</span>';
            }
            $("<div></div>")
                .attr("id", titleId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId)
                .html(ctrlTitle);

            // control: <input name="mst" data-bind="value:mst, event:{ change:mstValidate }" type="text" value="" class="ffield textbox string" title="Mã số thuế" />
            var maskType = ctrl.getControlMask();
            $("<input />")
                .attr("id", objId)
                .attr("name", objId)
                .attr("type", "text")
                .attr("data-bind", "value:" + objId + ", event:{ change:" + objId + "Validate }")
                .attr("data-mask", maskType)
                .addClass("ffield")
                .css("float", "left")
                .appendTo("#" + panelId);

            var readonly = ctrl.getControlReadOnly();
            if (readonly == "true") {
                $("#" + objId).attr("readonly", "readonly");
            }

            // Error: <label data-bind="visible:mstError, attr:{ title: mstError }, click:mstDismissError" class="error">!</label>
            $("<span />")
                .attr("id", objId + "Error")
                .attr("name", objId + "Error")
                .attr("type", "text")
                .attr("class", "cssErr")
                .attr("controlValidate", objId)
                .attr("data-bind", "visible:" + objId + "Error, text: " + objId + "Error, click:" + objId + "DismissError")
                .css("float", "left")
                .appendTo("#" + panelId);

            // Set property cho control
            var isRequired = false;
            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                switch (pid) {
                    case "p13": // Bắt buộc nhập
                        if (lstProperties[itm].getValue() == "true") {
                            isRequired = true;
                        }
                        break;
                    case "p14": // Thông báo lỗi của bắt buộc nhập
                        if (!isRequired) break;

                        var tmpRequiredId = objId + "_required";
                        $("<span></span>")
                            .attr("id", tmpRequiredId)
                            .attr("controlValidate", objId)
                            .html(isRequired ? (lstProperties[itm].getValue() == "" ? "*" : lstProperties[itm].getValue()) : "")
                            .css({ color: "red", display: "none", "font-style": "italic" })
                            .addClass("cssErr")
                            .insertAfter("#" + objId);
                        $("#" + objId)
                            .blur(function () {
                                var tmpval = $(this).val().replace(/_/g, "");
                                tmpval = tmpval.replace("//", "");

                                if (tmpval == "") {
                                    $("#" + tmpRequiredId).css({ display: "inline" });
                                } else {
                                    $("#" + tmpRequiredId).css({ display: "none" });
                                }
                            });
                        break;
                    default:
                        this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
                }
            }

            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + titleId).css('width')) - 15) + "px" });
            return true;
        },
        View: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();
            var propIdx = -1;
            for (var i = 0, m = ctrl.getProperties().length; i < m; i++) {
                if (ctrl.getProperties()[i].getId() == "p1") {
                    propIdx = i;
                    break;
                }
            }

            $("<div></div>")
                .attr("id", eForm.cfgPrefixId.divLabelId + objId)
                .addClass("cssNestedLabel")
                .appendTo("#" + panelId);

            $("<div></div>")
                .attr("id", objId)
                .css({ "float": "left", "padding-top": "3px", "border-bottom": "dotted 1px gray", "min-height": "15px" })
                .appendTo("#" + panelId);

            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                switch (pid) {
                    case "p17":
                        if (lstProperties[itm].getValue() != "")
                            $("#" + eForm.cfgPrefixId.divLabelId + objId).html(lstProperties[itm].getValue());
                        break;
                    default:
                        this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, true);
                }
            }
            $('#' + objId).css({ width: (parseInt($("#" + panelId).css('width')) - parseInt($('#' + eForm.cfgPrefixId.divLabelId + objId).css('width')) - 15) + "px" });
            return true;
        },
        LoadProperty: function (ctrl, currPro, pnlPropertyId) {
            var ctrlId = ctrl.getKey();
            var property = currPro;
            var propId = eForm.cfgPrefixId.divPropertyId + property.getId();
            var lstPropLibTmp = eForm.propertylib[property.getId()];

            switch (property.getId()) {
                case "p13": // Bắt buộc nhập
                    $("<select></select>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .appendTo("#" + pnlPropertyId)
                    .change(function (e) {
                        var titleId = ctrl.getTitleId();
                        var ctrlTitle = ctrl.getControlTitle();

                        if ($(this).val() == "true") {
                            $("#prop_p14").removeAttr("readonly");

                            // Label: set label for control
                            ctrlTitle += '<span style="color: red;"> (*)</span>';
                            $("#" + titleId)
                                .html(ctrlTitle);

                        } else {
                            $("#" + ctrlId + "_required").html("");
                            $("#prop_p14")
                                .val("")
                                .attr("readonly", "true");

                            // Label: set label for control
                            ctrlTitle = ctrlTitle.replace('<span style="color: red;"> (*)</span>', '');
                            $("#" + titleId)
                                .html(ctrlTitle);
                        }
                        if ($(this).val() != null) {
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                        }
                    })
                    .trigger('change');

                    for (var i = 0, l = lstPropLibTmp.length; i < l; i++) {
                        if (lstPropLibTmp[i].getValue().toString() == property.getValue())
                            //? "true" != true
                            //if (lstPropLibTmp[i].getValue() == property.getValue())
                            $("<option />").attr("selected", "selected")
                            .attr("value", lstPropLibTmp[i].getValue())
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#" + propId);
                        else
                            $("<option />").attr("value", lstPropLibTmp[i].getValue())
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#" + propId);
                    }

                    break;
                case "p14": // Thông báo lỗi bắt buộc nhập
                    var isRequired = false;
                    if ($("#prop_p13").val() == "true") {
                        isRequired = true;
                    }
                    var titleId = ctrl.getTitleId();
                    var ctrlTitle = ctrl.getControlTitle();
                    if (isRequired) {
                        // Label: set label for control
                        ctrlTitle += '<span style="color: red;"> (*)</span>';
                        $("#" + titleId)
                            .html(ctrlTitle);
                    }
                    else {
                        // Label: set label for control
                        ctrlTitle = ctrlTitle.replace('<span style="color: red;"> (*)</span>', '');
                        $("#" + titleId)
                            .html(ctrlTitle);
                    }
                    $("<input/>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .attr("type", "text")
                    .attr(isRequired ? "txt" : "readonly", isRequired ? "" : "true")
                    .val(isRequired ? property.getValue() : "")
                    .appendTo("#" + pnlPropertyId)
                    .keyup(function () {
                        $("#" + ctrlId + "_required").html($(this).val());
                        if ($(this).val() != null)
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                    })
                    .trigger('keyup');
                    break;
                case "p19": // Định dạng (Mask)
                    $("<select></select>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .appendTo("#" + pnlPropertyId)
                    .change(function (e) {
                        if ($(this).val() != null) {
                            $("#p1prop").val('');
                            $("#" + ctrlId).val('');
                        }
                        if ($(this).val() != null) {
                            //alert("die");
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                        }
                    })
                    .trigger('change');

                    for (var i = 0, l = lstPropLibTmp.length; i < l; i++) {
                        if (lstPropLibTmp[i].getValue() == property.getValue())
                            $("<option />").attr("selected", "selected")
                            .attr("value", lstPropLibTmp[i].getValue())
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#" + propId);
                        else
                            $("<option />").attr("value", lstPropLibTmp[i].getValue())
                            .html(lstPropLibTmp[i].getUIValue())
                            .appendTo("#" + propId);
                    }
                    break;
                default:
                    this._base.LoadProperties(ctrl, currPro, pnlPropertyId, true);
                    break;
            }
        }
    };
    var textbox2 = textbox2Class.constructor;
    //------------------------------------------------------------
    var labelClass = {
        constructor: function (options) {
            this._base = jQuery.extend(true, this, new control());
            jQuery.extend(true, this, labelClass);
        },
        Create: function (ctrlId, ctrlTypeId, panelId) {
            var ctrl = this._base.init(ctrlId, ctrlTypeId);
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrlId;
            ctrl.setControlId(objId);
            this.Add(ctrl, panelId);
            return ctrl;
        },
        Add: function (ctrl, panelId) {
            if (ctrl == undefined || ctrl == false) return false;
            var objId = ctrl.getKey();

            $("<div></div>")
                .attr("id", objId)
                .css({ "width": "100%", "padding-top": "3px" })
                .appendTo("#" + panelId);

            var lstProperties = ctrl.getProperties();
            for (var itm = 0, l = lstProperties.length; itm < l; itm++) {
                var pid = lstProperties[itm].getId();
                this._base.SetPropertieOfControl(pid, lstProperties[itm].getValue(), objId, false);
            }

            $('#' + objId).css({ width: "100%" });
            return true;
        },
        Load: function (ctrl, panelId) {
            return this.Add(ctrl, panelId);
        },
        View: function (ctrl, panelId) {
            return this.Add(ctrl, panelId);
        },
        LoadProperty: function (ctrl, currPro, pnlPropertyId) {
            var ctrlId = ctrl.getKey();
            var property = currPro;
            var propId = eForm.cfgPrefixId.divPropertyId + property.getId();

            switch (property.getId()) {
                case "p1":  //text
                    $("<br />").appendTo("#" + pnlPropertyId);
                    $("<textarea></textarea>")
                    .attr("id", propId)
                    .attr("name", propId)
                    .val(property.getValue())
                    .attr("rows", "7")
                    .css({ width: "280px" })
                    .appendTo("#" + pnlPropertyId)
                    .keyup(function () {
                        $("#" + ctrlId).html($(this).val().replace(/\n/g, "<br>"));
                        if ($(this).val() != null) {
                            ctrl.setPropertyValue(property.getId(), $(this).val());
                        }
                    })
                    .trigger('keyup');
                    break;
                default:
                    this._base.LoadProperties(ctrl, currPro, pnlPropertyId, false);
                    break;
            }
        }
    };
    var label = labelClass.constructor;

    // public
    // Construction of EProperty 
    eForm.PropertyEntity = function (ID, VALUE, ISDEFAULT, ISMULTILINE, UNAME, UVALUE, UDESC) {
        var result = new EProperty();
        result.init(ID, VALUE, ISDEFAULT, ISMULTILINE, UNAME, UVALUE, UDESC);
        return result;
    };
    // Construction of EControl
    eForm.ControlEntity = function (ID, TYPEID, POSROW, POSORDER, PROPERTIES) {
        var result = new EControl();
        result.init(ID, TYPEID);
        result.setPosRow(POSROW);
        result.setPosOrder(POSORDER);
        // result.rawProperies = PROPERTIES;
        // result.properies = PROPERTIES;
        // Lấy lại danh sách property trong cấu hình
        for (var i = 0, l = PROPERTIES.length; i < l; i++) {
            result.AddProperty(new eForm.PropertyEntity("p" + PROPERTIES[i].Id, PROPERTIES[i].Value));
        }
        // Lấy lại danh sách property mặc định của loại control tương ứng
        var proConfig = eForm.getPropertyByControlType(TYPEID);
        for (var j = 0, t = proConfig.length; j < t; j++) {
            var currPro = result.getProperty(proConfig[j].getId());
            if (!currPro) {
                result.AddProperty(new eForm.PropertyEntity(proConfig[j].getId(), proConfig[j].getValue()));
            }
        }
        return result;
    };
    // Trả về cỗ máy tạo control tương ứng với typeId
    eForm.CreateFormObject = function (typeId) {//ctrlLibs
        var dft;
        switch (typeId) {
            case "c1":
                dft = new label();
                break;
            case "c9":
                dft = new textbox2();
                break;
            case "c10":
                dft = new dropdown2();
                break;
            default:
                dft = new control();
                break;
        }
        return dft;
    }.bind(this);
    // Thiết lập mask cho control
    eForm.setMask = function () {
        $.datepicker.setDefaults($.datepicker.regional["vi"]);
        $('input.ffield[data-mask="integer"]').setMask();
        $('input.ffield[data-mask="decimal"]').setMask();
        $('input.ffield[data-mask="monthyear"]').mask("99/9999");
        $('input.ffield[data-mask="year"]').mask("9999");
        $('input.ffield[data-mask="datetime"]').mask("99/99/9999");
        $('input.ffield[data-mask="datetime"]').datepicker({
            changeMonth: true,
            changeYear: true
        });
    }.bind(this);

})(window.eForm = window.eForm || {}, jQuery);