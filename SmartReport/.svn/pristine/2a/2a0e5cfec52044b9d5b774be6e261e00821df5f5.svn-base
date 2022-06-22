/* 
* eForm.Lib V2.1.0
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

(function (eForm) {
    eForm.propertylib = {};
    eForm.controllib = {};
    eForm.Lib = {};

    // eForm.jsonLib in eForm.Libdata.js
    //var jsonDb = eForm.jsonLib;

    function countProp(frmControlId, jsonDb) {
        var count = 0;
        for (var i = 0; i < jsonDb.ControlPropertyTable.length; i++) {
            if (jsonDb.ControlPropertyTable[i].ControlId == frmControlId) {
                count++;
            }
        };
        return count;
    };

    eForm.Lib.Init = function () {
        var jsonDb = eForm.jsonLib;
        // countProp
        //#region eForm.controllib
        //{ "c1": ['p15', 'p2'], "c2": ['p3, 'p4'] }
        var strControlDb = "{";
        for (var i = 0; i < jsonDb.ControlTable.length; i++) {
            var frmControlId = jsonDb.ControlTable[i].Id;
            var count = countProp(frmControlId, jsonDb);
            var count2 = 0;
            strControlDb += '"c' + frmControlId + '" : [';
            for (var j = 0; j < jsonDb.ControlPropertyTable.length; j++) {
                if (jsonDb.ControlPropertyTable[j].ControlId == frmControlId) {
                    count2++;
                    strControlDb += '"';
                    strControlDb += 'p' + jsonDb.ControlPropertyTable[j].PropertyId;
                    strControlDb += '"';
                    if (count2 < count) {
                        strControlDb += ',';
                    }
                }
            };
            strControlDb += ']';
            if (i.toString() != (jsonDb.ControlTable.length - 1)) {
                strControlDb += ',';
            }
        };
        strControlDb += "}";
        eForm.controllib = jQuery.parseJSON(strControlDb);
        //#endregion

        //#region eForm.propertylib
        //{
        //"p1": [eForm.PropertyEntity("p1", "", true, false, "Nội dung", "", "")],
        //"p2":[
        //        eForm.PropertyEntity("p2", "normal", true, true, "In đậm", "Không", ""),
        //        eForm.PropertyEntity("p2", "bold", false, true, "In đậm", "Có", "")
        //     ]
        //}
        for (var i = 0; i < jsonDb.PropertyTable.length; i++) {
            //cuongnt
            var property;
            var frmPropertyId = jsonDb.PropertyTable[i].Id;
            eForm.propertylib['p' + frmPropertyId] = [];

            var count = 0;
            for (var j = 0; j < jsonDb.PropertyValueTable.length; j++) {
                //ID
                var ID = 'p' + frmPropertyId;

                //ISMULTILINE
                var ISMULTILINE = jsonDb.PropertyTable[i].IsMultivalue;

                //UNAME
                if (jsonDb.PropertyTable[i].UIName != null) {
                    var UNAME = jsonDb.PropertyTable[i].UIName;
                }
                else {
                    var UNAME = '';
                }

                //UDESC
                if (jsonDb.PropertyTable[i].UIDescription != null) {
                    var UDESC = jsonDb.PropertyTable[i].UIDescription;
                }
                else {
                    var UDESC = '';
                }

                if (parseInt(jsonDb.PropertyValueTable[j].PropertyId) == frmPropertyId) {
                    count++;
                    //VALUE
                    if (jsonDb.PropertyValueTable[j].Val != null) {
                        var VALUE = jsonDb.PropertyValueTable[j].Val;
                    }
                    else {
                        var VALUE = '';
                    }

                    //ISDEFAULT
                    var ISDEFAULT = jsonDb.PropertyValueTable[j].IsDefault;

                    //UVALUE
                    if (jsonDb.PropertyValueTable[j].UIVal != null) {
                        var UVALUE = jsonDb.PropertyValueTable[j].UIVal;
                    }
                    else {
                        var UVALUE = '';
                    }

                    //cuongnt
                    property = eForm.PropertyEntity(ID, VALUE, ISDEFAULT, ISMULTILINE, UNAME, UVALUE, UDESC);
                    eForm.propertylib['p' + frmPropertyId].push(property);
                }
            }
        }
        //#endregion
    };

    eForm.getPropertyByControlType = function (type) {
        var result = [];
        var pros = eForm.controllib[type];
        var length = pros.length;
        for (var i = 0; i < length; i++) {
            var lst = eForm.propertylib[pros[i]];
            for (var j = 0, t = lst.length; j < t; j++) {
                if (lst[j].IsDefault()) {
                    result.push(lst[j]);
                    break;
                }
            }
        }
        return result;
    };

})(window.eForm = window.eForm || {});