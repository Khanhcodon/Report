(function (egov, $, _) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    if (typeof (_) === 'undefined') {
        throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    }

    egov.common.dgview = {};

    egov.common.dgview.itemsDg = {
        jobtitlesItem: "jobtitlesItem",//chức vụ
        jobtitlesDeptItem: "jobtitlesDeptItem",//phòng ban - chức vụ
        allUsers: "allUsers", //tất cả người dùng
        deptItem: "deptItem",//phong ban
        userItem: 'userItem', // Cán bộ
    };

    //object hiển thị
    egov.common.dgview.objViewType = {
        thongbao: 'thongbao',
        xinykien: 'xinykien'
    };

    ///<summary> Hàm lấy danh sách user đồng gửi - xin ý kiến</summary>
    egov.common.dgview.getUsersConsult = function (listItemsDg, allUserDept) {
        var result = [];
        if (listItemsDg != undefined) {
            if (listItemsDg.length > 0) {
                for (var i = 0; i < listItemsDg.length; i++) {
                    var objDg = listItemsDg[i];
                    var listUser;
                    if (objDg.value == egov.common.dgview.itemsDg.allUsers) {
                        return _.pluck(allUserDept, 'userid');
                    }
                    else if (objDg.value == egov.common.dgview.itemsDg.userItem) {
                        result.push(objDg.item.userid);
                    }
                    else if (objDg.value == egov.common.dgview.itemsDg.jobtitlesItem) {
                        listUser = _.filter(allUserDept, function (num) { return num.jobtitleid == objDg.item.jobtitleid; });
                        if (listUser.length > 0) {
                            listUser = _.pluck(listUser, 'userid');
                            $.extend(result, listUser);
                        }
                    }
                    else if (objDg.value == egov.common.dgview.itemsDg.deptItem) {
                        // Tim phong ban hien tai va phong ban con
                        listUser = _.filter(allUserDept, function (num) { return num.departmentid == objDg.item.deptid || num.idext.indexOf('.' + objDg.item.deptid + '.') > 0; }); //num.departmentid == objDg.item;
                        if (listUser.length > 0) {
                            listUser = _.pluck(listUser, 'userid');
                            $.extend(result, listUser);
                        }
                    }
                    else if (objDg.value == egov.common.dgview.itemsDg.jobtitlesDeptItem) {
                        // Tim phong ban hien tai va phong ban con
                        listUser = _.filter(allUserDept, function (num) { return num.jobtitleid == objDg.item.jobtitleid && (num.departmentid == objDg.item.deptid || num.idext.indexOf('.' + objDg.item.deptid + '.') > 0); }); //num.departmentid == objDg.item;
                        if (listUser.length > 0) {
                            listUser = _.pluck(listUser, 'userid');
                            $.extend(result, listUser);
                        }
                    }
                }
            }
        }
        // Distinct array.
        result = _.uniq(result, false);
        return result;
    };

    egov.common.dgview.getTargetForComments = function (objType) {
        var result = [], value, type;
        egov.common.targetForComments = egov.common.targetForComments || [];
        switch (objType) {
            case objType === egov.common.dgview.objViewType.thongbao: {
                value = 'viewThongbao';
                type = 'thongbao';
            }
            case objType === egov.common.dgview.objViewType.xinykien: {
                value = 'viewXinykien';
                type = 'xinykien';
            }
            default: {
                value = 'viewThongbao';
                type = 'thongbao';
            }
        }
        for (var j = 0; j < egov.common.targetForComments.length; j++) {
            result.push({ label: egov.common.targetForComments[j].label, value: value, type: type });
        }
        return JSON.stringify(result);
    };

})(window.egov = window.egov || {}, window.jQuery, window._);