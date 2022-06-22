(function (egov, $, _) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    if (typeof (_) === 'undefined') {
        throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
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
                $.each(node.USER_QUERIES, function(index, item) {
                    var userSelected = _.find(arrUsers, function (user) {
                        return user.value == item.UserId;
                    });
                    if (userSelected) {
                        itemSetting.push({ "queryType": queryType.TheoCanBo, "label": userSelected.username + " - " + userSelected.fullname, "objecttype": "Cán bộ", "value": JSON.stringify(item) });
                    }
                });
            }
            if (node.LEVEL_QUERIES && node.LEVEL_QUERIES.length > 0) {
                $.each(node.LEVEL_QUERIES, function (index, item) {
                    var pos = "Tất cả mọi người";
                    var label;
                    if (item.PositionId !== '*') {
                        var position = _.find(arrPosition, function (j) {
                            return j.value === parseInt(item.PositionId);
                        });
                        if (position) {
                            pos = position.label;
                        }
                        label = "Cấp " + item.Level + "\\" + pos;
                        itemSetting.push({ "queryType": queryType.TheoCap, "label": label, "objecttype": "Cấp", "value": JSON.stringify(item) });
                    }
                });
            }
            if (node.POSITION_QUERIES && node.POSITION_QUERIES.length > 0) {
                $.each(node.POSITION_QUERIES, function (index, item) {
                    var pos = "Tất cả mọi người";
                    var label = "";
                    if (item.PositionId !== '*') {
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
                        itemSetting.push({ "queryType": queryType.TheoViTri, "label": label, "objecttype": "Vị trí", "value": JSON.stringify(item) });
                    }
                });
            }
            if (node.RELATION_QUERIES && node.RELATION_QUERIES.length > 0) {
                $.each(node.RELATION_QUERIES, function (index, item) {
                    var pos = "Tất cả mọi người";
                    var label;
                    if (item.PositionId !== '*') {
                        var position = _.find(arrPosition, function (j) {
                            return j.value === parseInt(item.PositionId);
                        });
                        if (position) {
                            pos = position.label;
                        }
                        label = "Node " + item.NodeId + "\\" + getRelationUser(item.Relation) + "\\" + pos;
                        itemSetting.push({ "queryType": queryType.TheoQuanHe, "label": label, "objecttype": "Quan hệ", "value": JSON.stringify(item) });
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
            users = _.filter(arrDeptUserPosition, function(num) {
                return num.idext === deptIdExt && (positionid == "*" ? true : num.positionid === parseInt(positionid));
            });
        } else if (relation === positionType.DonViCapDuoi) {
            users = _.filter(arrDeptUserPosition, function(num) {
                return (num.idext.indexOf(deptIdExt + '.') > -1)
                    && (getLevelDepartment(num.idext) === getLevelDepartment(deptIdExt) + 1)
                        && (positionid == "*" ? true : num.positionid === parseInt(positionid));
            });
        } else if (relation === positionType.DonViTrucThuoc) {
            users = _.filter(arrDeptUserPosition, function(num) {
                return (num.idext.indexOf(deptIdExt + '.') > -1)
                    && (positionid == "*" ? true : num.positionid === parseInt(positionid));
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
                    && (positionid == "*" ? true : num.positionid === parseInt(positionid));
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
                result = 'Đơn vị cấp dưới';
                break;
            case positionType.DonViTrucThuoc:
                result = 'Đơn vị trực thuộc';
                break;
            case positionType.DonViHienTai:
                result = 'Đơn vị hiện tại';
                break;

            case relationType.CanBoCungDonVi:
                result = 'Cùng đơn vị';
                break;
            case relationType.CanBoCungCap:
                result = 'Node ngang hàng';
                break;
            case relationType.CanBoCungNutCha:
                result = 'Cùng Node cha';
                break;
            case relationType.CanBoCapDuoi:
                result = 'Cấp dưới';
                break;
            case relationType.CanBoCapTren:
                result = 'Cấp trên';
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