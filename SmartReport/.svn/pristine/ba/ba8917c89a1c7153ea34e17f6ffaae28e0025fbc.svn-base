(function (egov, $, _) {
    if (typeof ($) === 'undefined') {
        throw egov.resources.notify.noJquery;
    }
    if (typeof (_) === 'undefined') {
        throw egov.resources.notify.noUnderscore;
    }
    var relationType = {
        CanBoCungDonVi: 'CanBoCungDonVi',
        CanBoCungCap: 'CanBoCungCap',
        CanBoCungNutCha: 'CanBoCungNutCha',
        CanBoCapDuoi: 'CanBoCapDuoi',
        CanBoCapTren: 'CanBoCapTren'
    };

    var positionType = {
        DonViHienTai: 'DonViHienTai',
        DonViTrucThuoc: 'DonViTrucThuoc',
        DonViCapDuoi: 'DonViCapDuoi'
    };

    var queryType = {
        TheoCanBo: 'TheoCanBo',
        TheoCap: 'TheoCap',
        TheoViTri: 'TheoViTri',
        TheoQuanHe: 'TheoQuanHe'
    };

    var positionAll = "-1";

    egov.workflow = {};

    // Hàm lấy ra thông tin cấu hình cho 1 node đến
    // addstr: Địa chỉ lưu không gian user của 1 action(hướng chuyển)
    // arrDepartment: Danh sách phòng ban
    // arrPosition: Danh sách chức vụ
    // arrUsers: Danh sách cán bộ
    egov.workflow.getAddressByNodeFrom = function (node, arrDepartment, arrPosition, arrUsers) {
        var itemSetting = [];
        if (node) {
            if (node.USER_QUERIES && node.USER_QUERIES.length > 0) {
                $.each(node.USER_QUERIES, function (index, item) {
                    var userSelected = _.find(arrUsers, function (user) {
                        return user.value == item.UserId;
                    });
                    if (userSelected) {
                        itemSetting.push({
                            "queryType": queryType.TheoCanBo,
                            "label": userSelected.username + " - " + userSelected.fullname,
                            "objecttype": egov.resources.workflow.user,
                            "value": JSON.stringify(item)
                        });
                    }
                });
            }

            if (node.LEVEL_QUERIES && node.LEVEL_QUERIES.length > 0) {
                $.each(node.LEVEL_QUERIES, function (index, item) {
                    var pos = egov.resources.doctype.allpeople;
                    var label;
                    //if (item.PositionId !== '*') {
                    if (item.PositionId !== positionAll) {
                        var position = _.find(arrPosition, function (j) {
                            return j.value === parseInt(item.PositionId);
                        });
                        if (position) {
                            pos = position.label;
                        }
                        label = egov.resources.doctype.level + " " + item.Level + "\\" + pos;
                        itemSetting.push({
                            "queryType": queryType.TheoCap,
                            "label": label,
                            "objecttype": egov.resources.doctype.level,
                            "value": JSON.stringify(item)
                        });
                    }
                });
            }
            if (node.POSITION_QUERIES && node.POSITION_QUERIES.length > 0) {
                $.each(node.POSITION_QUERIES, function (index, item) {
                    var pos = egov.resources.doctype.allpeople;
                    var label = "";
                    //if (item.PositionId !== '*') {
                    if (item.PositionId !== positionAll) {
                        var position = _.find(arrPosition, function (j) {
                            return j.value === parseInt(item.PositionId);
                        });
                        if (position) {
                            pos = position.label;
                        }
                        var relation = "\\" + getRelationUser(item.Position);
                        var department = _.find(arrDepartment, function (dept) {
                            return dept.value == item.DepId;
                        });
                        if (department) {
                            label = department.pathname + relation + "\\" + pos;
                        }
                        itemSetting.push({
                            "queryType": queryType.TheoViTri,
                            "label": label,
                            "objecttype": egov.resources.workflow.position,
                            "value": JSON.stringify(item)
                        });
                    }
                });
            }
            if (node.RELATION_QUERIES && node.RELATION_QUERIES.length > 0) {
                $.each(node.RELATION_QUERIES, function (index, item) {
                    var pos = egov.resources.doctype.allpeople;
                    var label;
                    //if (item.PositionId !== '*') {
                    if (item.PositionId !== positionAll) {
                        var position = _.find(arrPosition, function (j) {
                            return j.value === parseInt(item.PositionId);
                        });
                        if (position) {
                            pos = position.label;
                        }
                        label = "Node " + item.NodeId + "\\" + getRelationUser(item.Relation) + "\\" + pos;
                        itemSetting.push({
                            "queryType": queryType.TheoQuanHe,
                            "label": label,
                            "objecttype": egov.resources.workflow.relation,
                            "value": JSON.stringify(item)
                        });
                    }
                });
            }
        }
        return itemSetting;
    };

    // Lấy ra danh sách cán bộ dựa vào vị trí
    // deptIdExt: Chuỗi id mở rộng (1.2.3)
    // relation: Kiểu quan hệ
    // positionid: id chức vụ
    // arrDeptUserPosition: Danh sách phòng ban người dùng chức vụ
    // arrUsers: Danh sách người dùng
    egov.workflow.getUserByPosition = function (deptIdExt, relation, positionid, arrDeptUserPosition, arrUsers) {
        var result = [];
        var users = [];
        if (relation === "" || relation === positionType.DonViHienTai) {
            users = _.filter(arrDeptUserPosition, function (num) {
                return num.idext === deptIdExt && (positionid == positionAll ? true : num.positionid === parseInt(positionid));
            });
        } else if (relation === positionType.DonViCapDuoi) {
            users = _.filter(arrDeptUserPosition, function (num) {
                return (num.idext.indexOf(deptIdExt + '.') > -1)
                    && (getLevelDepartment(num.idext) === getLevelDepartment(deptIdExt) + 1)
                        && (positionid == positionAll ? true : num.positionid === parseInt(positionid));
            });
        } else if (relation === positionType.DonViTrucThuoc) {
            users = _.filter(arrDeptUserPosition, function (num) {
                return (num.idext.indexOf(deptIdExt + '.') > -1)
                    && (positionid == positionAll ? true : num.positionid === parseInt(positionid));
            });
        }
        if (users.length > 0) {
            result = _.filter(arrUsers, function (user) { return _.contains(_.pluck(users, 'userid'), user.value); });
        }
        return result;
    };

    // Lấy ra danh sách cán bộ dựa vào cấp
    // level: Cấp
    // positionid: id chức danh
    // arrDeptUserPosition: Danh sách phòng ban người dùng chức vụ
    // arrUsers: Danh sách người dùng
    egov.workflow.getUserByLevel = function (level, positionid, arrDeptUserPosition, arrUsers) {
        var result = [];
        var users = _.filter(arrDeptUserPosition, function (num) {
            return getLevelDepartment(num.idext) == level
                    && (positionid == positionAll ? true : num.positionid === parseInt(positionid));
        });
        if (users.length > 0) {
            result = _.filter(arrUsers, function (user) { return _.contains(_.pluck(users, 'userid'), user.value); });
        }
        return result;
    };

    egov.workflow.getDepIdFromDepExt = function (departmentIdExt) {
        if (departmentIdExt.indexOf('.') > -1) {
            var arr = departmentIdExt.split('.');
            return parseInt(arr[arr.length - 1]);
        } else {
            //todo: Hopcv
            //Cần xem lại chỗ này
            //nếu trong trường hợp người dùng cấu hình như (deptId:123, deptext:1)=> cái này sai
            return parseInt(departmentIdExt);
        }
    };

    var getRelationUser = function (key) {
        var result = "";
        switch (key) {
            case positionType.DonViCapDuoi:
                result = egov.resources.workflow.belowoffice;
                break;
            case positionType.DonViTrucThuoc:
                result = egov.resources.workflow.underoffice;
                break;
            case positionType.DonViHienTai:
                result = egov.resources.workflow.currentoffice;
                break;

            case relationType.CanBoCungDonVi:
                result = egov.resources.workflow.sameoffice;
                break;
            case relationType.CanBoCungCap:
                result = egov.resources.workflow.peernode;
                break;
            case relationType.CanBoCungNutCha:
                result = egov.resources.workflow.sameparentnode;
                break;
            case relationType.CanBoCapDuoi:
                result = egov.resources.workflow.underuser;
                break;
            case relationType.CanBoCapTren:
                result = egov.resources.workflow.overuser;
                break;
        }
        return result;
    };

    var getLevelDepartment = function (departmentIdExt) {
        if (departmentIdExt.indexOf('.') > -1) {
            var arr = departmentIdExt.split('.');
            return arr.length;
        } else {
            return 1;
        }
    };

})(window.egov = window.egov || {}, window.jQuery, window._)