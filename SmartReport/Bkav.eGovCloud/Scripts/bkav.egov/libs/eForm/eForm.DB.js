/* 
* eForm.DB V2.1.0
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

    eForm.database = new function () {
        var jsonDb = [];
        var self = this;

        /// <summary> CuongNT - 120727
        /// Tạo lstControls từ chuỗi json (json từ server gửi về
        /// <editors>
        /// 120418 - cuongnt - thêm param formid: tạo database cho các form khác nhau.
        /// </editors>
        /// </summary>
        this.JsonDeserialize = function (json, formid) {
            if (json == null || !json.hasOwnProperty || json.length == 0) return false;
            jsonDb = json;
            //var tmpLst = json;
            //lstControls = new Array();
            eForm.database["data" + formid] = new Array();
            var lstControls = eForm.database["data" + formid];
            for (var k = 0; k < jsonDb.length; k++) {
                var tmpId = k + 1;
                lstControls[k] = new eForm.ControlEntity(tmpId, "c" + jsonDb[k].TypeId, jsonDb[k].PosRow, jsonDb[k].PosOrder, jsonDb[k].Properties);
            }
            eForm.database["data" + formid] = lstControls;
            return true;
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Lấy chuỗi json lưu thông tin form động
        /// </summary>
        this.JsonSerialize = function (formid) {
            var lstControls = eForm.database["data" + formid];
            var json = "[";
            for (var i = 0, l = lstControls.length; i < l; i++) {
                var ctrl = lstControls[i];
                // start controls
                json += "{ \"Id\": " + ctrl.getId() + ",";
                json += "\"TypeId\": " + ctrl.getTypeId().substr(1) + ",";
                json += "\"PosRow\": " + ctrl.getPosRow() + ",";
                json += "\"PosOrder\": " + ctrl.getPosOrder() + ",";
                json += "\"Properties\": ["; // start properties
                //
                for (var j = 0, m = ctrl.getProperties().length; j < m; j++) {
                    var prop = ctrl.getProperties()[j];
                    json += "{\"Id\": " + prop.getId().substr(1) + ",";
                    json += "\"Value\": \"" + prop.getValue() + "\"}"; //+ escape(prop.getValue()) + "\"}";
                    if (j < m - 1) json += ",";
                } //end for

                json += "]}"; // end properties & control
                if (i < l - 1) json += ",";
            } //end for
            json += "]";
            return json;
        }.bind(this);
        
        /// <summary> CuongNT - 120727
        /// Lấy chuỗi json lưu thông tin đã nhập của form động để lưu lên ctzDocument trên server
        /// </summary>
        //this.JsonSerialize2 = function (formid) {
        //    var lstControls = eForm.database["data" + formid];
        //    var json = "[";
        //    for (var i = 0, l = lstControls.length; i < l; i++) {
        //        var ctrl = lstControls[i];
        //        // start controls
        //        json += "{ \"TypeId\": " + ctrl.getTypeId().substr(1) + ",";
        //        json += "\"DisplayName\": \"" + ctrl.getControlValue() + "\",";
        //        json += "\"ControlId\": " + ctrl.getControlId() + ",";
        //        json += "\"InputValue\": \"" + ctrl.getControlValue() + "\"";
        //        json += "}";
        //        if (i < l - 1) json += ",";
        //    } //end for
        //    json += "]";
        //    return json;
        //}.bind(this);
        
        /// <summary> CuongNT - 120727
        /// Lấy chuỗi json lưu thông tin đã nhập của form động để lưu lên ctzDocument trên server
        /// </summary>
        this.JsonSerialize3 = function (formid) {
            var lstControls = eForm.database["data" + formid];
            var json = "[";
            for (var i = 0, l = lstControls.length; i < l; i++) {
                var ctrl = lstControls[i];
                var type = ctrl.getTypeId().substr(1);
                // Do đang loại trừ contronl type=1 nên if (i < l - 1) json += ","; đang bị sai, cần sửa lại.
                if (type == '9' || type == '10') {
                    // start controls
                    json += "{ \"TypeId\": " + ctrl.getTypeId().substr(1) + ",";
                    json += "\"CatalogSelected\": \"" + ctrl.getCatalogSelected() + "\",";
                    json += "\"ControlId\": \"" + ctrl.getControlId() + "\",";
                    json += "\"GlobalCode\": \"" + ctrl.getControlGlobalCode() + "\",";
                    json += "\"DisplayName\": \"" + ctrl.getControlTitle() + "\",";
                    json += "\"MaskType\": \"" + ctrl.getControlMask() + "\",";
                    json += "\"IsRequired\": \"" + ctrl.getRequirementControl() + "\",";
                    json += "\"Value\": \"" + ctrl.getControlValue() + "\"";
                    json += "}";
                    //if (i < l - 1) json += ",";// cuongnt - 120731: fix lỗi thừa dấu , ở cuối json nếu có control type = 1 ở cuối form động.
                    json += ",";
                }
            } //end for
            if (json[json.length-1]==',') {
                 json = json.substring(0, json.length - 1);
            }
            json += "]";
            return json;
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Lấy tất cả các đối tượng control đang có
        /// </summary>
        this.GetAll = function (formid) {
            return eForm.database["data" + formid];
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// set lại giá trị mới của property thuộc list control đang quản lý --> Bỏ
        /// </summary>
        this.SetControlProperty = function (ctrlId, propName, propVal) {//propIdx
            var ctrl = self.Get(ctrlId);
            if (ctrl == null) return;
            //ctrl.getProperties()[propIdx].setValue(propVal);
            ctrl.getProperty(propName).setValue(propVal);
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Thêm 1 control mới vào DB
        /// input: Control object 
        /// output: boolean
        /// </summary>
        this.Add = function (control, formid) {
            var lstControls = eForm.database["data" + formid];
            lstControls.push(control);
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Xoá 1 control trong DB
        /// input: control ID
        ///output: boolean
        /// </summary>
        this.RemoveControl = function (ctrlId, formid) {
            var lstControls = eForm.database["data" + formid];
            var ctrlIdx = self.GetControlIndex(ctrlId, formid);
            lstControls.splice(ctrlIdx, 1);
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Lấy lại control theo Id tự tăng khi add vào eForm.database
        /// </summary>
        this.Get = function (id, formid) {
            var lstControls = eForm.database["data" + formid];
            for (var i = 0, l = lstControls.length; i < l; i++) {
                if (lstControls[i].getId() == id)
                    return lstControls[i];
            } //end for
            return null;
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Lấy lại control theo ControlId (Property = 15), TypeId
        /// </summary>
        this.GetByControlId = function (controlId, controlType, formid) {
            var lstControls = eForm.database["data" + formid];
            for (var i = 0, l = lstControls.length; i < l; i++) {
                if (lstControls[i].getControlId() == controlId && lstControls[i].getTypeId() == controlType)
                    return lstControls[i];
            } //end for
            return null;
        }.bind(this);

        // Không dùng
        /// <summary> CuongNT - 120727
        ///  Lấy index của control trong lstControls
        /// input: control Id
        /// output: control index (int)
        /// </summary>
        this.GetControlIndex = function (ctrlId, formid) {
            var lstControls = eForm.database["data" + formid];
            for (var i = 0, l = lstControls.length; i < l; i++) {
                if (lstControls[i].getId() == ctrlId)
                    return i;
            } //end for
            return -1;
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Tìm kiếm các control (trong lstControls) có 1 giá trị của property chỉ đinh 
        /// (Ex: p1 = 'abc')
        /// </summary>
        this.FindControlsByProp = function (propid, propval, formid) {
            var lstControls = eForm.database["data" + formid];
            var result = new Array();
            for (var i = 0, l = lstControls.length; i < l; i++) {
                for (var j = 0, m = lstControls[i].getProperties().length; j < m; j++) {
                    if (lstControls[i].getProperties()[j].getId() == propid) {
                        if (lstControls[i].getProperties()[j].getValue() == propval)
                            result[result.length] = lstControls[i];
                        break;
                    }
                }
            }
            return result;
        }.bind(this);

        /// <summary> CuongNT - 120727
        /// Đếm số lượng controls đang có trong DB
        /// output: int
        /// </summary>
        this.Count = function (formid) {
            var lstControls = eForm.database["data" + formid];
            if (!lstControls.hasOwnProperty) return 0;
            return lstControls.length;
        }.bind(this);

        this.hasControlC9C10 = function (formid) {
            var lstControls = eForm.database["data" + formid];
            var result = false;
            for (var i = 0, l = lstControls.length; i < l; i++) {
                var control = lstControls[i];
                if (control.getTypeId() == 'c9' || control.getTypeId() == 'c10') {
                    result = true;
                    break;
                }
            }
            return result;
        }.bind(this);
    };

})(window.eForm = window.eForm || {}, jQuery);