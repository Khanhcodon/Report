define([egov.isMobile ? egov.template.transfer.transferExtendMobile : egov.template.transfer.transferExtend],
    function (Template) {
        "use strict";

        //#region Models

        //#endregion

        var _resource = egov.resources.document.transfer;
        var max, max1;

        //#region Views

        var TransferExtend = Backbone.View.extend({
            className: 'transfer-extend',
            template: Template,

            initialize: function () {
                /// <summary>
                /// Khởi tạo
                /// </summary>

                return this;
            },

            render: function (hasDg, showRelation, onselect, loadedFunction) {
                /// <summary>
                /// Hiển thị form đồng gửi
                /// </summary>
                /// <returns type=""></returns>
                if ($("body .transfer-extend").length > 0) {
                    $("body .transfer-extend").remove();
                }

                this.$el.html(this.template);
                this.$el.bindResources();
                this.$el.appendTo("body");
                this.$dgOption = this.$('.dg-option');
                this.$relationOption = this.$('.relation-option');

                this.onselect = onselect;
                this.onloaded = loadedFunction;
                this.forTransfer = hasDg;
                this.showRelation = showRelation;
                var that = this;

                // Tải các dữ liệu cần thiết trước khi bind form
                if (!egov.dataManager) {
                    return;
                }
                var type_position = egov.setting.typePositionTitleJob;
                if (type_position === undefined) {
                    type_position = 0;
                }

                var dataManager = egov.dataManager;
                dataManager.getAllUsers({
                    success: function (allUsers) {
                        that.allUsers = allUsers;
                        if (type_position === 0) {
                            dataManager.getAllJobtitle({
                                success: function (allPosition) {
                                    that.allPosition = allPosition;

                                    dataManager.getAllDept({
                                        success: function (allDepts) {
                                            that.allDepts = allDepts;

                                            dataManager.getAllUserDeptPosition({
                                                success: function (allUserDeptPosition) {
                                                    that.allUserDeptPosition = allUserDeptPosition;
                                                    that._bind(that.onloaded);
                                                }
                                            });
                                        }
                                    });
                                }
                            });
                        } else {
                            dataManager.getAllPosition({
                                success: function (allPosition) {
                                    that.allPosition = allPosition;

                                    dataManager.getAllDept({
                                        success: function (allDepts) {
                                            that.allDepts = allDepts;

                                            dataManager.getAllUserDeptPosition({
                                                success: function (allUserDeptPosition) {
                                                    that.allUserDeptPosition = allUserDeptPosition;
                                                    that._bind(that.onloaded);
                                                }
                                            });
                                        }
                                    });
                                }
                            });
                        }
                    }
                });

                return this;
            },

            reRender: function (hasDg, showRelation, onselect) {
                /// <summary>
                /// Render lại form đồng gửi
                /// </summary>
                /// <param name="hasDg">Cho phép gửi Đồng xử lý</param>
                /// <param name="showRelation">Hiển thị chọn theo quan hệ</param>
                /// <param name="callback">Hàm callback khi thay đổi lựa chọn</param>
                this.onselect = onselect;
                this.hasDg = hasDg;
                this.showRelation = showRelation;
                this._show();
                this._bindCallback();
                that.$('#dgUser').focus();
            },

            destroy: function () {
                /// <summary>
                /// Hủy bỏ các cây văn người dung- phòng ban- chức danh
                /// </summary>
                this.$('#dgUsers, #dgJobtitle, #dgDeptJob, #dgUserGiamsats').each(function () {
                    $(this).customDropdown("destroy");
                });

                this.$el.remove();
            },

            callback: function () {
                /// <summary>
                /// Gọi hàm callback sau sự kiện chọn hoặc bỏ chọn người dùng, phòng ban, chức vụ
                /// </summary>
                egov.callback(this.onselect);
            },

            serialize: function () {
                /// <summary>
                /// Trả về không gian user được chọn
                /// </summary>
                this.result = {
                    isDg: true, // this.$('[name="isDg"]:checked').val() === '1',
                    isAllUser: false,
                    isAllDept: false,
                    isAllJobtitle: false,
                    users: [],
                    depts: [],
                    jobDepts: [],
                    jobtitlies: [],
                    userGs: [],
                    deptGs: []
                };
                this._getUsersDept();
                this._getJobtitlesDept();
                this._getUserGs();
                return this._processResult();
            },

            getDestination: function () {
                /// <summary>
                /// Trả về các không gian user nhận đồng gửi.
                /// </summary>
                /// <returns type=""></returns>
                var selected = this.serialize();
                var result = [];
                var transferType = egov.enum.transferType;
                var type = selected.isDg ? transferType.dongxuly : transferType.thongbao;

                //#region Xử lý giám sát

                if (selected.isAllUserGs) {
                    result.push({
                        label: _resource.sendAll,
                        value: 'all',
                        type: transferType.giamsat
                    });
                } else {

                    // Hiển thị các lựa chọn ở chọn user - phòng ban
                    selected.deptGs.forEach(function (dept) {
                        result.push({
                            label: dept.data,
                            value: "FilterDepartment-" + dept.value,
                            type: transferType.giamsat
                        });
                        //containner.append(parseUserItem(0, dept.data, false));
                    });

                    // Hiển thị các lựa chọn ở chọn user - phòng ban
                    ///Loại bỏ các user trùng nhau
                    //  selected.users = _.uniq(selected.users);
                    selected.userGs.forEach(function (user) {
                        result.push({
                            label: user.fullname + " - " + user.username,
                            value: "FilterDepartment-user_" + user.value,
                            type: transferType.giamsat
                        });
                        //containner.append(parseUserItem(0, user.fullname, false));
                    });
                }

                //#endregion

                // Trường hợp chọn tất cả người dùng
                if (selected.isAllUser || (selected.isAllDept && selected.isAllJobtitle)) {
                    result.push({
                        label: _resource.sendAll,
                        value: 'all',
                        type: type
                    });

                    return result;
                }

                // Hiển thị các lựa chọn ở chọn user - phòng ban: chọn phòng ban
                selected.depts.forEach(function (dept) {
                    result.push({
                        label: dept.data,
                        value: "FilterDepartment-" + dept.value,
                        type: type
                    });
                });

                // Hiển thị các lựa chọn ở chọn user - phòng ban: chọn người dùng
                selected.users.forEach(function (user) {
                    result.push({
                        label: user.fullname + " - " + user.username,
                        value: "FilterDepartment-user_" + user.value,
                        type: type
                    });
                });

                // Chọn tất cả phòng ban
                if (selected.isAllDept) {
                    // Trường hợp chọn tất cả chức vụ đã set ở trên
                    if (selected.jobtitlies.length > 0) {
                        // Không chọn tất cả chức vụ thì hiển thị danh sách các chức vụ thuộc phòng ban root
                        var dept = " thuộc " + selected.jobDepts[0].data;
                        var text = _.pluck(selected.jobtitlies, 'label').join(', ') + dept;
                        var jobtitleIds = _.pluck(selected.jobtitlies, 'value').join(',');
                        result.push({
                            label: text,
                            value: String.format('JobtitleForDept-{0}_{1}', selected.jobDepts[0].idext, jobtitleIds),
                            type: type
                        });
                    }
                }
                else if (selected.isAllJobtitle) {// Chọn tất cả chức vụ
                    // Trường hợp chọn tất cả phòng ban đã set ở trên
                    if (!selected.isAllDept) {
                        selected.jobDepts.forEach(function (dept) {
                            result.push({
                                label: dept.data,
                                value: String.format('JobtitleForDept-{0}_{1}', dept.idext, 0),
                                type: type
                            });
                        });
                    }
                }
                else if (selected.jobtitlies.length > 0) {
                    var jobtitleIds = _.pluck(selected.jobtitlies, 'value').join(',');
                    selected.jobDepts.forEach(function (dept) {
                        var text = _.pluck(selected.jobtitlies, 'label').join(', ') + " thuộc " + dept.data;
                        result.push({
                            label: text,
                            value: String.format('DeptForJobtitle-{0}_{1}', dept.idext, jobtitleIds),
                            type: type
                        });
                    });
                }

                return result;
            },

            getUserConsults: function () {
                /// <summary>
                /// Trả về danh sách các user nhận đồng gửi.
                /// </summary>
                /// <remarks>
                /// Hàm này gọi khi lấy danh sách user từ không gian user được chọn nên:
                /// Cần gọi hàm this.serialize() hoặc this.getDestination() trước khi gọi hàm này.
                /// </remarks>
                /// <returns type="object">Mảng các userid thuộc không gian user được chọn.</returns>
                var selected = this.result;
                var result = [];
                if (selected === undefined) {
                    return result;
                }
                var type_position = egov.setting.typePositionTitleJob;
                if (type_position === undefined) {
                    type_position = 0;
                }
                var allUserDept = this.allUserDeptPosition;
                var allUsers = this.allUsers;

                var listUser;
                if (selected.isAllUser || (selected.isAllDept && selected.isAllJobtitle)) {
                    return _.pluck(allUsers, 'value');
                }

                // Thêm các user được chọn
                if (selected.users.length > 0) {
                    result = _.union(result, _.pluck(selected.users, "value"));
                }

                // Thêm các phòng ban được chọn
                if (selected.depts.length > 0) {
                    result = _.union(result, _getUserFromDepts(_.pluck(selected.depts, 'value'), allUserDept));
                }

                // Lọc từ chọn chức vụ - phòng ban
                // Chọn tất cả phòng ban
                if (selected.isAllDept) {
                    if (!selected.isAllJobtitle && selected.jobtitlies.length > 0) {
                        var depts;
                        if (type_position !== 0) {
                            // Không chọn tất cả chức vụ thì lấy user thuộc danh sách các chức vụ thuộc phòng ban root
                            depts = _.filter(allUserDept, function (i) {
                                return _.find(selected.jobtitlies, function (jobtitle) {
                                    return jobtitle.value === i.positionid;
                                });
                            });
                        } else {
                            // Không chọn tất cả chức vụ thì lấy user thuộc danh sách các chức vụ thuộc phòng ban root
                            depts = _.filter(allUserDept, function (i) {
                                return _.find(selected.jobtitlies, function (jobtitle) {
                                    return jobtitle.value === i.jobtitleid;
                                });
                            });
                        }
                        if (depts.length > 0) {
                            result = _.union(result, _getUserFromDepts(_.pluck(depts, 'departmentid'), allUserDept));
                        }
                    }
                }
                else if (selected.isAllJobtitle) {// Chọn tất cả chức vụ
                    // Trường hợp chọn tất cả phòng ban đã set ở trên
                    if (!selected.isAllDept && selected.jobDepts.length > 0) {
                        var depts = _.filter(allUserDept, function (i) {
                            return _.find(selected.jobDepts, function (dept) {
                                return dept.departmentid === i.departmentid;
                            });
                        });

                        if (depts.length > 0) {
                            result = _.union(result, _getUserFromDepts(_.pluck(depts, 'departmentid'), allUserDept));
                        }
                    }
                }
                else if (selected.jobtitlies.length > 0) {
                    // trường hợp chọn các chức vụ thuộc một vài phòng ban
                    var depts;
                    if (type_position !== 0) {
                        depts = _.filter(allUserDept, function (i) {
                            var checkDept = _.find(selected.jobDepts, function (dept) {
                                //return dept.departmentid === i.departmentid;
                                return dept.value === i.departmentid;
                            });
                            var checkJob = _.find(selected.jobtitlies, function (jobtitle) {
                                return jobtitle.value === i.positionid;
                            });

                            return checkDept && checkJob;
                        });
                    } else {
                        depts = _.filter(allUserDept, function (i) {
                            var checkDept = _.find(selected.jobDepts, function (dept) {
                                //return dept.departmentid === i.departmentid;
                                return dept.value === i.departmentid;
                            });
                            var checkJob = _.find(selected.jobtitlies, function (jobtitle) {
                                return jobtitle.value === i.jobtitleid;
                            });

                            return checkDept && checkJob;
                        });
                    }
                    if (depts.length > 0) {
                        result = _.union(result, _getUserFromDepts(_.pluck(depts, 'departmentid'), allUserDept));
                    }
                }

                result = _.uniq(result, false);
                return result;
            },

            getUserThongBao: function () {
                /// <summary>
                /// Trả về danh sách các user giám sát.
                /// </summary>
                /// <remarks>
                /// Hàm này gọi khi lấy danh sách user từ không gian user được chọn nên:
                /// Cần gọi hàm this.serialize() hoặc this.getDestination() trước khi gọi hàm này.
                /// </remarks>
                /// <returns type="object">Mảng các userid thuộc không gian user được chọn.</returns>

                var selected = this.result;
                var result = [];
                if (selected === undefined) {
                    return result;
                }

                var allUserDept = this.allUserDeptPosition;
                var allUsers = this.allUsers;

                var listUser;

                if (selected.isAllUserGs) {
                    result = _.union(result, _.pluck(allUsers, "value"));
                    return result;
                }

                // Thêm các user được chọn
                if (selected.userGs.length > 0) {
                    result = _.union(result, _.pluck(selected.userGs, "value"));
                }

                // Thêm các phòng ban được chọn
                if (selected.deptGs.length > 0) {
                    result = _.union(result, _getUserFromDepts(_.pluck(selected.deptGs, 'value'), allUserDept));
                }

                result = _.uniq(result, false);
                return result;
            },

            getUserReceiveDocument: function () {
                /// <summary>
                /// Trả về danh sách các user nhận đồng gửi.
                /// </summary>
                /// <remarks>
                /// Hàm này gọi khi lấy danh sách user từ không gian user được chọn nên:
                /// Cần gọi hàm this.serialize() hoặc this.getDestination() trước khi gọi hàm này.
                /// </remarks>
                /// <returns type="object">Mảng các userid thuộc không gian user được chọn.</returns>
                var result = [];

                var allUserDept = this.allUserDeptPosition;
                var allUsers = this.allUsers;
                var allDepts = this.allDepts;

                // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức vụ
                $("#FilterDepartment").jstree("get_checked", null, false).each(function () {
                    var node = $(this);
                    var nodeId = this.id;
                    var hasReceiveDocument = node.attr("hasreceivedocument") === "true";

                    // Xác định node đang chọn là node phòng ban hay node user
                    var isDeptNode = node.attr('rel') === 'dept';
                    if (isDeptNode) {
                        var dept = _.find(allDepts, function (i) {
                            return i.value == nodeId;
                        });
                        if (dept) {
                            // Xác định node root: là node không có extend .
                            if (dept.idext.indexOf('.') < 0) {
                                var users = _.filter(allUserDept, function (u) {
                                    return u.hasReceiveDocument == true;
                                });

                                result = _.union(result, _.pluck(users, "userid"));

                            } else {
                                var userInDepts = _.filter(allUserDept, function (u) {
                                    return u.departmentid == dept.value && u.hasReceiveDocument == true;
                                });
                                result = _.union(result, _.pluck(userInDepts, "userid"));
                            }
                        }
                    }
                    else {
                        // Node là user
                        if (hasReceiveDocument) {
                            nodeId = nodeId.replace('user_', '');
                            var user = _.find(allUsers, function (i) {
                                return i.value == nodeId;
                            });

                            if (user) {
                                ///Kiểm tra xem tồn tai hay chưa,không có thì add
                                if (!_.contains(result, user))
                                    result.push(user);
                            }
                        }
                    }
                });

                return result;
            },

            getExtInfo: function () {
                /// <summary>
                /// Trả thông tin chung chưa lọc lấy người dùng=> lưu trạng thái hiển thị lại
                /// </summary>
                if (!this.result) {
                    return null;
                }

                var extInfo = {
                    isAllUser: false,
                    isAllDept: false,
                    isAllJobtitle: false,
                    users: [],//Người được chọn
                    jobtitlies: [],//chức vụ được chọn
                    jobDepts: [],//Chức vụ phòng ban
                    depts: []//Phòng ban
                };

                //Nếu chọn toàn bộ người dùng
                if (this.result.isAllUser) {
                    extInfo.isAllUser = true;
                    return extInfo;
                }

                // Nếu chọn toàn bộ chức danh và phòng ban
                if (this.result.isAllJobtitle) {
                    extInfo.isAllJobtitle = true;
                    if (this.result.isAllDept) {
                        extInfo.isAllDept = true;
                    }
                    return extInfo;
                }

                //Lấy danh sách người dùng
                if (this.result.users && this.result.users.length > 0) {
                    extInfo.users = _.pluck(this.result.users, 'value');
                }

                //Lấy danh sách chức danh
                if (this.result.jobtitlies && this.result.jobtitlies.length > 0) {
                    extInfo.jobtitlies = _.pluck(this.result.jobtitlies, 'value');
                }

                //Lấy danh sách chức danh - phòng ban
                if (this.result.jobDepts && this.result.jobDepts.length > 0) {
                    extInfo.jobDepts = _.pluck(this.result.jobDepts, 'value');
                }

                //Lấy danh sách phòng ban
                if (this.result.depts && this.result.depts.length > 0) {
                    extInfo.depts = _.pluck(this.result.depts, 'value');
                }

                return extInfo;
            },

            checkIsDongGui: function () {
                /// <summary>
                /// Check xem là đồng gửi hay thông báo
                /// </summary>

                return this.forTransfer;
            },

            selectDg: function (node, gsNode) {
                /// <summary>
                /// Hiển thị danh sách người dùng, phòng ban, chức danh- phòng ban được chọn lên
                ///<param name="node">Node chứa dach sách hiển thị</param>
                /// </summary>
                var destination = this.getDestination();
                if (!destination || destination.length <= 0) {
                    return;
                }

                for (var i = 0; i < destination.length; i++) {
                    var value = destination[i].value;
                    var type = destination[i].type;

                    if (gsNode && type == egov.enum.transferType.giamsat) {
                        gsNode.append(_parseUserItem(value, destination[i].label, "user"));
                        continue;
                    }

                    var cssClass = "user";
                    if (value === 'all') {
                        cssClass = 'all';
                    }
                    else if (value === '') {
                        cssClass = 'allJobtitle';
                    }
                    else if (value.indexOf('FilterDepartment-user_') !== -1) {
                        cssClass = 'dept-user';
                    }
                    else if (value.indexOf('FilterDepartment-') !== -1) {
                        cssClass = 'dept';
                    }
                    else if (value === 'JobtitleForDept') {
                        cssClass = 'jobtitleForDept';
                    }
                    else if (value.indexOf('DeptForJobtitle-') !== -1) {
                        cssClass = 'deptForJobtitle';
                    }

                    node.append(_parseUserItem(value, destination[i].label, cssClass));
                }
            },

            uncheckPrivateAnoun: function (selectedValue) {            
                var value = selectedValue.split("-");
                var isAllUserSelected = value[0] === "all";
                var isUserOrDeptSelected = value[0] === "FilterDepartment";
                var isPosDeptSelected = value[0] === "DeptForJobtitle" || value[0] === "JobtitleForDept";

                if (isAllUserSelected) {
                    $("#JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
                    $("#DeptForJobtitle").jstree("uncheck_node", $("#DeptForJobtitle").find("li"));
                    $("#FilterDepartment").jstree("uncheck_node", $("#FilterDepartment").find("li"));
                    return;
                }

                if (isUserOrDeptSelected) {
                    $("#" + value[0]).jstree("uncheck_node", $("#" + value[0]).find("li#" + value[1]));
                    return;
                }

                if (isPosDeptSelected) {
                    var positionIds = value[1].split("_")[1].split(',');
                    var deptIdExt = value[1].split("_")[0];

                    $("#DeptForJobtitle").jstree("uncheck_node", $("#DeptForJobtitle").find("li[idext='" + deptIdExt + "']"));
                    var selecteds = $("#DeptForJobtitle").jstree("get_checked", null, false);
                    if (selecteds.length === 0) {
                        _.each(positionIds, function (positionId) {
                            positionId == 0
                                ? $("#JobtitleForDept").find("input:checkbox").removeAttr("checked")
                                : $("#JobtitleForDept").find("input:checkbox[value='" + positionId + "']").removeAttr("checked");
                        });
                    }
                }
            },

            uncheckThongBao: function (e) {
                var target = $(e.target);
                if (!target.is(':checkbox')) return;
                var value = target.val().split("-");

                if (value[0] === "FilterDepartment") {
                    $("#FilterDepartmentGiamSat").jstree("uncheck_node", $("#FilterDepartmentGiamSat").find("li#" + value[1]));
                }
            },

            //#region Private Methods

            _bind: function () {
                var that = this;

                var $userDg = this.$('#dgUser');
                var $filterDept = this.$('.filterDeparment#FilterDepartment');
                that._bindUsers($userDg, $filterDept);

                if (that.forTransfer) {
                    // Nếu cho form chuyển
                    // Hiển thị thêm phần user nhận thông báo và user nhận đồng xử lý riêng.
                    var $userGs = this.$('#dgUserGiamsat');
                    var $filterDeptGs = this.$('.filterDeparment#FilterDepartmentGiamSat');
                    $filterDeptGs.empty();
                    that._bindUsers($userGs, $filterDeptGs);
                } else {

                }

                that._bindDepartments(function () {
                    that._bindJobtitlies(function () {
                        if (!that.forTransfer) {
                            that.$dgOption.hide();
                            that.$(".thongbao").hide();
                            that.$(".dgText").hide();
                        }

                        if (!that.showRelation) {
                            that.$relationOption.hide();
                        }

                        that._show();
                        that._bindCallback();
                        egov.callback(that.onloaded);
                        that.$el.bindResources();
                        // that.$('#dgUser').focus();
                    });
                });
            },

            _bindUsers: function ($userDg, $filterDept) {
                /// <summary>
                /// Hiển thị dữ liệu user để tìm kiếm và tree dept
                /// </summary>
                var $user = $userDg;
                var that = this;
                var $filterDept = $filterDept;
                var allUserDeps = that.allUserDeptPosition;
                $user.autocomplete({
                    source: that.allUsers,
                    focus: function (event, ui) {
                        $user.val(ui.item.username);
                        return false;
                    },
                    select: function (event, ui) {
                        // xác định các phòng ban user thuộc vào
                        var departments = _.filter(allUserDeps, function (u) {
                            return u.userid === ui.item.value;
                        });
                        if (departments) {
                            for (var i = 0; max = departments.length, i < max; i++) {
                                var parents = departments[i].idext.split('.');
                                if (parents) {
                                    // Tìm đến node có id là node đang được chọn.
                                    var userItem = 'li#user_' + ui.item.value;
                                    var userItems = $filterDept.find(userItem);
                                    if (typeof userItems !== 'undefined' && userItems.length > 0) {
                                        // Nếu tồn tại thì check cho node đó
                                        $filterDept.jstree("check_node", userItems);
                                        // return;
                                    }

                                    for (var j = 0; max1 = parents.length, j < max1; j++) {
                                        // Xác định node cha
                                        var parent = parents[j];
                                        var parentItem = $filterDept.find('li[id=' + parent + ']');

                                        // Nếu node cha tồn tại.
                                        if (parentItem.length > 0) {
                                            // và chưa mở thì thêm các node con của node cha tương ứng.
                                            if (parentItem.hasClass('jstree-closed') && parentItem.children('ul').length === 0) {
                                                _appendChild(parentItem, parseInt(parent),
                                                                true, true, that.allDepts, that.allUsers, allUserDeps);
                                            }
                                            continue;
                                        }
                                        // Không thì thêm vào
                                        var $parent = $('');
                                        var parentId = 0;
                                        if (j > 0) {
                                            $parent = $filterDept.find('li[id=' + parents[j - 1] + ']');
                                            parentId = parseInt(parents[j - 1]);
                                        }
                                        _appendChild($parent, parentId, true, true,
                                                        that.allDepts, that.allUsers,
                                                        allUserDeps);

                                        if (j === max1 - 1) {
                                            _appendChild($filterDept.find('li[id=' + parent + ']'),
                                                            parseInt(parent), true, true,
                                                             that.allDepts, that.allUsers,
                                                             allUserDeps);
                                        }
                                    }
                                }
                            }

                            $filterDept.jstree("check_node", $filterDept
                                                .jstree("get_unchecked", null, true)
                                                .find('li[rel!=dept][id=user_' + ui.item.value + ']'));
                        }
                        $user.val('');
                        return false;
                    }
                })
                .data("autocomplete")._renderItem = function (ul, item) {
                    ul.addClass('dropdown-menu').css("zIndex", "1060");
                    return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append("<a>" + item.label + "</a>")
                        .appendTo(ul);
                };
            },

            _bindDepartments: function (callback) {
                var that = this;
                var allDepts = that.allDepts;
                require(['jstree'], function () {
                    // Bind search user
                    _bindJsTree(that.$el.find('#FilterDepartment'), true, true, false,
                                    allDepts, that.allUsers,
                                    that.allUserDeptPosition, null, []);

                    _bindJsTree(that.$el.find('#FilterDepartmentGiamSat'), true, true, false,
                                    allDepts, that.allUsers,
                                    that.allUserDeptPosition, null, []);

                    // Phòng ban, chức danh
                    _bindJsTree(that.$el.find('#DeptForJobtitle'), false, true, false,
                                      allDepts, [], [], null, []);

                    if (typeof callback === 'function') {
                        callback();
                    }
                });
            },

            _bindJobtitlies: function (callback) {
                /// <summary>
                /// Bind danh sách các chức danh
                /// </summary>
                this.$jobtitlies = this.$('.dgJobtitlies');
                var that = this;
                var allPosition = that.allPosition;

                var max;
                for (var i = 0; max = allPosition.length, i < max; i++) {
                    var jobtitle = allPosition[i];
                    var jobItem = $('<li>').addClass('list-group-item');
                    jobItem.append(String.format('<label><input type="checkbox" value="{0}"> {1}</label>', jobtitle.value, jobtitle.label));
                    that.$jobtitlies.append(jobItem);
                }
                if (egov.isMobile) {
                    that.$jobtitlies.find(".mdl-checkbox").materialCheckbox();
                }
                $('#JobtitleForDept #allJobsForDept').checkAll($('#JobtitleForDept :checkbox').not(":first"));
                that.$('.jobtitlies #allJobs').checkAll(that.$('.jobtitlies :checkbox').not(":first"));

                if (typeof callback === 'function') {
                    callback();
                }
            },

            _bindCallback: function () {
                // Bind các sự kiện change tree và change jobtitle
                // Gọi đến hàm callback cho các sự kiện này
                var that = this;

                $('#FilterDepartment, #DeptForJobtitle, #FilterDepartmentGiamSat').bind('change_state.jstree', function () {
                    that.callback();
                });

                $('#JobtitleForDept :checkbox').on("click", function () {
                    that.callback();
                });

                that.$('[name="isDg"]').on("click", function () {
                    that.callback();
                });
            },

            _getUsersDept: function () {
                ///<summary>
                /// Trả về kết quả chọn user - phòng ban
                /// </summary>
                var result = this.result,
                    that = this,
                    allDepts = that.allDepts,
                    allUsers = that.allUsers;

                // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức danh
                $("#FilterDepartment").jstree("get_checked", null, false).each(function () {
                    var node = $(this);
                    var nodeId = this.id;
                    // Xác định node đang chọn là node phòng ban hay node user
                    var isDeptNode = node.attr('rel') === 'dept';
                    if (isDeptNode) {
                        var dept = _.find(allDepts, function (i) {
                            return i.value == nodeId;
                        });
                        if (dept) {
                            // Xác định node root: là node không có extend .
                            if (dept.idext.indexOf('.') < 0) {
                                result.isAllUser = true;
                                return;
                            }
                            // Không thì push dept hiện tại vào danh sách được chọn
                            result.depts.push(dept);
                        }
                    }
                    else {
                        // Node là user
                        nodeId = nodeId.replace('user_', '');
                        var user = _.find(allUsers, function (i) {
                            return i.value == nodeId;
                        });

                        if (user) {
                            ///Kiểm tra xem tồn tai hay chưa,không có thì add
                            if (!_.contains(result.users, user))
                                result.users.push(user);
                        }
                    }
                });
            },

            _getUserGs: function () {
                ///<summary>
                /// Trả về kết quả chọn user - phòng ban
                /// </summary>
                var result = this.result,
                    that = this,
                    allDepts = that.allDepts,
                    allUsers = that.allUsers;

                // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức danh
                $("#FilterDepartmentGiamSat").jstree("get_checked", null, false).each(function () {
                    var node = $(this);
                    var nodeId = this.id;
                    // Xác định node đang chọn là node phòng ban hay node user
                    var isDeptNode = node.attr('rel') === 'dept';
                    if (isDeptNode) {
                        var dept = _.find(allDepts, function (i) {
                            return i.value == nodeId;
                        });
                        if (dept) {
                            // Xác định node root: là node không có extend .
                            if (dept.idext.indexOf('.') < 0) {
                                result.isAllUserGs = true;
                                return;
                            }
                            // Không thì push dept hiện tại vào danh sách được chọn
                            result.deptGs.push(dept);
                        }
                    }
                    else {
                        // Node là user
                        nodeId = nodeId.replace('user_', '');
                        var user = _.find(allUsers, function (i) {
                            return i.value == nodeId;
                        });

                        if (user) {
                            ///Kiểm tra xem tồn tai hay chưa,không có thì add
                            if (!_.contains(result.userGs, user))
                                result.userGs.push(user);
                        }
                    }
                });
            },

            _getJobtitlesDept: function () {
                ///<summary>
                /// Trả về kết quả chọn Jobtitle - phòng ban
                /// </summary>
                var result = this.result;
                // Lấy ra danh sách các chức vụ đang được chọn.
                var isAllJobtitle = false;
                var jobtitleSelected = [];
                var that = this;
                $('#JobtitleForDept :checked').each(function (checkbox) {
                    var jobId = $(this).val();
                    if (jobId === '0') {
                        isAllJobtitle = true;
                        return;
                    }
                    var jobtitle = _.find(that.allPosition, function (i) {
                        return i.value == jobId;
                    });
                    if (jobtitle) {
                        jobtitleSelected.push(jobtitle);
                    }
                });

                var that = this;
                var allDepts = that.allDepts;
                var selecteds = $("#DeptForJobtitle").jstree("get_checked", null, false);
                if (selecteds.length === 0) {
                    // Trường hợp chưa chọn phòng ban nào mặc định là gửi cho tất cả
                    result.isAllDept = true;
                    result.isAllJobtitle = isAllJobtitle;
                    result.jobtitlies = jobtitleSelected;
                    var rootDept = _.find(allDepts, function (i) {
                        return i.idext.indexOf('.') < 0;
                    });
                    result.jobDepts.push(rootDept);
                    return;
                }
                selecteds.each(function () {
                    var node = $(this);
                    var nodeId = this.id;

                    var dept = _.find(allDepts, function (i) {
                        return i.value == nodeId;
                    });
                    if (dept) {
                        // Xác định node root: là node không có extend .
                        var isAllDept = dept.idext.indexOf('.') < 0;
                        // Chọn tất cả phòng ban
                        if (isAllDept) {
                            // Trường hợp chọn tất cả phòng ban và chức danh = chọn tất cả user
                            if (isAllJobtitle) {
                                result.isAllDept = true;
                                result.isAllJobtitle = true;
                                return;
                            }
                            else {
                                result.isAllDept = true;
                                result.jobDepts.push(dept);
                                result.jobtitlies = jobtitleSelected;
                                return;
                            }
                        }
                        else {
                            // Chọn tất cả chức danh và không chọn tất cả phòng ban
                            if (isAllJobtitle) {
                                result.isAllJobtitle = true;
                                result.jobDepts.push(dept);
                            }
                            else {
                                result.jobtitlies = jobtitleSelected;
                                result.jobDepts.push(dept);
                            }
                        }
                    }
                });
            },

            _processResult: function () {
                /// <summary>
                /// Xử lý danh sách nhận đồng gửi.
                /// </summary>
                /// <returns type=""></returns>

                var that = this;
                // Loại bỏ phòng ban hiện tại và các phòng ban của nó ở những lựa chọn khác.
                // Ví dụ ở lựa chọn User - Phòng ban: chọn gửi cho BDA,
                // Xong lại chọn ở Chức vụ - phòng ban: gửi Manager của BDA, Gửi tất cả nhân viên BDA/Phòng 4
                // => Lựa chọn ở trên bao gồm lựa chọn ở dưới => cần remove cái thừa đi
                this.result.depts.forEach(function (d) {
                    that.result.jobDepts = _.reject(that.result.jobDepts, function (i) {
                        return i.idext.split('.').indexOf('' + d.value) >= 0; // điều kiện này tính cả trường hợp =
                    });
                });

                return this.result;
            },

            _resetForm: function () {
                $("#FilterDepartment").jstree("uncheck_node", $("#FilterDepartment").find("li"));
                $("#DeptForJobtitle").jstree("uncheck_node", $("#DeptForJobtitle").find("li"));
                $("#JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
            },

            _show: function () {
                var that = this;
                that.$('#dgUsers, #dgJobtitle, #dgDeptJob, #dgUserGiamsats').each(function () {
                    $(this).customDropdown({
                        css: {
                            width: 300,
                            height: 'auto'
                        }
                    });
                });
                that._resetForm();
            },

            bindDataInTransferView: function (isDg, data) {
                /// <summary>
                /// Bind lại dữ liệu được chọn trên  form bàn giao văn theo dự kiến chuyển
                ///<param name="isDg" type="bool">Có phải là đồng gửi(true), thống báo(false) <param>
                ///<param name="userIds" type="Array">Danh sách userid<param>
                /// </summary>
                if (isDg) {
                    this.$('input[name="isDg"][value="1"]').prop('checked', true);
                } else {
                    this.$('input[name="isDg"][value="0"]').prop('checked', true);
                }
                this.bindDataView(data);
            },

            bindDataView: function (data) {
                /// <summary>
                /// Bind lại dữ liệu được chọn trên form phát hành văn bản theo dự kiến phát hành
                ///<param name="data" type="object">Có phải là đồng gửi <param>
                /// </summary>
                if (!data || typeof data !== 'object') {
                    return;
                }
                //hiển thị tất cả người dùng phòng ban
                if (data.isAllUser) {
                    //check chọn tắt cả ở cây người dùng phòng ban
                    this._showAllUser();
                    return;
                }

                //Hiển thị chức danh
                if (data.isAllJobtitle) {
                    this._showAllJobtitle();
                    if (data.isAllDept) {
                        this._showAllJobDepts();
                        return;
                    }
                } else {
                    this._showJobtitle(data.jobtitlies);
                }

                //Hiển thị chức danh phòng ban
                if (data.isAllDept) {
                    this._showAllJobDepts();
                } else {
                    this._showJobDepts(data.jobDepts);
                }

                //Hiển thị phòng ban
                this._showDepts(data.depts);

                //Hiển thị ngưởi dùng
                this._showUsers(data.users);
            },

            // #region Hiển thị lại các thông tin được chọn trên form bàn giao, phát hành như đã cấu hình dự kiến đã làm

            _showUsers: function (userIds) {
                ///<summary>
                ///Bind lại danh sách người dùng được chọn trên cây người dùng phòng ban
                ///<param name="userIds" type="Array">Danh sách id người dùng</param>
                ///</summary>
                if (!userIds || userIds.length <= 0) {
                    return;
                }

                var that = this;
                this.$filterDept = this.$('.filterDeparment');
                var allUserDeps = that.allUserDeptPosition;
                // xác định các phòng ban user thuộc vào
                var departments = _.filter(allUserDeps, function (u) {
                    return _.contains(userIds, u.userid);
                });

                if (departments) {
                    for (var u = 0; u < userIds.length; u++) {
                        for (var i = 0; max = departments.length, i < max; i++) {
                            var parents = departments[i].idext.split('.');
                            if (parents) {
                                // Tìm đến node có id là node đang được chọn.
                                var userItem = 'li#user_' + userIds[u];
                                var userItems = that.$filterDept.find(userItem);
                                if (typeof userItems !== 'undefined' && userItems.length > 0) {
                                    // Nếu tồn tại thì check cho node đó
                                    that.$filterDept.jstree("check_node", userItems);
                                    // return;
                                }

                                for (var j = 0; max1 = parents.length, j < max1; j++) {
                                    // Xác định node cha
                                    var parent = parents[j];
                                    var parentItem = that.$filterDept.find('li[id=' + parent + ']');

                                    // Nếu node cha tồn tại.
                                    if (parentItem.length > 0) {
                                        // và chưa mở thì thêm các node con của node cha tương ứng.
                                        if (parentItem.hasClass('jstree-closed') && parentItem.children('ul').length === 0) {
                                            _appendChild(parentItem, parseInt(parent),
                                                            true, true, that.allDepts,
                                                            that.allUsers, allUserDeps);
                                        }
                                        continue;
                                    }
                                    // Không thì thêm vào
                                    var $parent = $('');
                                    var parentId = 0;
                                    if (j > 0) {
                                        $parent = that.$filterDept.find('li[id=' + parents[j - 1] + ']');
                                        parentId = parseInt(parents[j - 1]);
                                    }
                                    _appendChild($parent, parentId, true, true,
                                                    that.allDepts, that.allUsers,
                                                    allUserDeps);

                                    if (j === max1 - 1) {
                                        _appendChild(that.$filterDept.find('li[id=' + parent + ']'),
                                                        parseInt(parent), true, true,
                                                         that.allDepts, that.allUsers,
                                                         allUserDeps);
                                    }
                                }
                            }
                        }

                        that.$filterDept.jstree("check_node", that.$filterDept
                                            .jstree("get_unchecked", null, true)
                                            .find('li[rel!=dept][id=user_' + userIds[u] + ']'));
                    }
                }
            },

            _showAllUser: function () {
                ///<summary>
                ///Check chọn tất cả trên cây người dùng- phòng ban
                ///</summary>
                this.$('.filterDeparment').jstree("check_all");
            },

            _showAllJobtitle: function () {
                ///<summary>
                ///Check chọn tất cả trên cây chức danh
                ///</summary>
                this.$('.jobtitleForDept :checkbox').prop('checked', true);
            },

            _showJobtitle: function (jobs) {
                ///<summary>
                ///Bind lại dữ liệu lên cây chức danh
                ///<param name= "jobs" type="Array"> Danh sách id phòng ban</param>
                ///</summary>
                if (jobs) {
                    for (var i = 0; i < jobs.length; i++) {
                        this.$('.jobtitleForDept :checkbox[value="' + jobs[i] + '"]').prop('checked', true);
                    }
                }
            },

            _showAllJobDepts: function () {
                ///<summary>
                ///Chọn tất cả trên cây chức danh - phòng ban
                ///</summary>
                this.$('.deptForJobtitle').jstree("check_all");
            },

            _showJobDepts: function (deptIds) {
                ///<summary>
                ///Bind lại dữ liệu lên chức danh phong ban
                ///<param name= "deptIds" type="Array"> Danh sách id phòng ban</param>
                ///</summary>
                if (!deptIds || deptIds.length <= 0) {
                    return;
                }
                var that = this;
                this.$deptForJobtitle = this.$('.deptForJobtitle');
                var allDept = that.allDepts;
                // xác định các phòng ban user thuộc vào
                var departments = _.filter(allDept, function (u) {
                    return _.contains(deptIds, u.value);
                });
                if (departments) {
                    for (var u = 0; u < deptIds.length; u++) {
                        for (var i = 0; max = departments.length, i < max; i++) {
                            var parents = departments[i].idext.split('.');
                            if (parents) {
                                // Tìm đến node có id là node đang được chọn.
                                var deptItem = 'li#' + deptIds[u];
                                var deptItems = that.$deptForJobtitle.find(deptItem);
                                if (typeof deptItems !== 'undefined' && deptItems.length > 0) {
                                    // Nếu tồn tại thì check cho node đó
                                    that.$deptForJobtitle.jstree("check_node", deptItems);
                                    // return;
                                }

                                for (var j = 0; max1 = parents.length, j < max1; j++) {
                                    // Xác định node cha
                                    var parent = parents[j];
                                    var parentItem = that.$deptForJobtitle.find('li[id=' + parent + ']');

                                    // Nếu node cha tồn tại.
                                    if (parentItem.length > 0) {
                                        // và chưa mở thì thêm các node con của node cha tương ứng.
                                        if (parentItem.hasClass('jstree-closed') && parentItem.children('ul').length === 0) {
                                            _appendChild(parentItem, parseInt(parent),
                                                            true, true, allDept, [], []);
                                        }
                                        continue;
                                    }
                                    // Không thì thêm vào
                                    var $parent = $('');
                                    var parentId = 0;
                                    if (j > 0) {
                                        $parent = that.$deptForJobtitle.find('li[id=' + parents[j - 1] + ']');
                                        parentId = parseInt(parents[j - 1]);
                                    }
                                    _appendChild($parent, parentId, true, true,
                                                    allDept, [], []);

                                    if (j === max1 - 1) {
                                        _appendChild(that.$deptForJobtitle.find('li[id=' + parent + ']'),
                                                        parseInt(parent), true, true,
                                                        allDept, [], []);
                                    }
                                }
                            }
                        }

                        that.$deptForJobtitle.jstree("check_node", that.$deptForJobtitle
                                            .jstree("get_unchecked", null, true)
                                            .find('li[rel=dept][id=' + deptIds[u] + ']'));
                    }
                }
            },

            _showDepts: function (deptIds) {
                ///<summary>
                ///Bind lại dữ liệu lên cây phòng ban
                ///<param name= "deptIds" type="Array"> Danh sách id phòng ban</param>
                ///</summary>
                if (!deptIds || deptIds.length <= 0) {
                    return;
                }
                var that = this;
                this.$filterDept = this.$('.filterDeparment');
                var allUserDeps = that.allUserDeptPosition;

                // xác định các phòng ban user thuộc vào
                var departments = _.filter(allUserDeps, function (u) {
                    return _.contains(deptIds, u.departmentid);
                });

                if (departments) {
                    for (var u = 0; u < deptIds.length; u++) {
                        for (var i = 0; max = departments.length, i < max; i++) {
                            var parents = departments[i].idext.split('.');
                            if (parents) {
                                // Tìm đến node có id là node đang được chọn.
                                var deptItem = 'li#' + deptIds[u];
                                var deptItems = that.$filterDept.find(deptItem);
                                if (typeof deptItems !== 'undefined' && deptItems.length > 0) {
                                    // Nếu tồn tại thì check cho node đó
                                    that.$filterDept.jstree("check_node", deptItems);
                                    // return;
                                }

                                for (var j = 0; max1 = parents.length, j < max1; j++) {
                                    // Xác định node cha
                                    var parent = parents[j];
                                    var parentItem = that.$filterDept.find('li[id=' + parent + ']');

                                    // Nếu node cha tồn tại.
                                    if (parentItem.length > 0) {
                                        // và chưa mở thì thêm các node con của node cha tương ứng.
                                        if (parentItem.hasClass('jstree-closed') && parentItem.children('ul').length === 0) {
                                            _appendChild(parentItem, parseInt(parent),
                                                            true, true, that.allDepts,
                                                            that.allUsers, allUserDeps);
                                        }
                                        continue;
                                    }
                                    // Không thì thêm vào
                                    var $parent = $('');
                                    var parentId = 0;
                                    if (j > 0) {
                                        $parent = that.$filterDept.find('li[id=' + parents[j - 1] + ']');
                                        parentId = parseInt(parents[j - 1]);
                                    }
                                    _appendChild($parent, parentId, true, true,
                                                    that.allDepts, that.allUsers,
                                                    allUserDeps);

                                    if (j === max1 - 1) {
                                        _appendChild(that.$filterDept.find('li[id=' + parent + ']'),
                                                        parseInt(parent), true, true,
                                                         that.allDepts, that.allUsers,
                                                         allUserDeps);
                                    }
                                }
                            }
                        }

                        that.$filterDept.jstree("check_node", that.$filterDept
                                            .jstree("get_unchecked", null, true)
                                            .find('li[rel=dept][id=' + deptIds[u] + ']'));
                    }
                }
            },

            //#endregion
        });

        //#endregion

        //#region Private Methods

        var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
        itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
        var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" hasReceiveDocument="${attr.hasReceiveDocument}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
        itemTreeviewCheckboxTemplate += '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
        var plugins = ["themes", "json_data", "ui", "crrm"];

        var _getChildrens = function (parentid, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
            var children = _.filter(arrDept, function (num) { return num.parentid == parentid; });
            var deptUsers = _.filter(arrDeptUserJobtitles, function (dept) {
                return dept.departmentid === parentid;
            });

            if (children.length > 0) {
                for (var j = 0; j < children.length; j++) {
                    if (_getChildrens(children[j].value, false, arrDept, [], []).length > 0 || (hasUser && deptUsers.length > 0)) {
                        children[j].state = "closed";
                    }
                }
            }

            if (hasUser && deptUsers.length > 0) {
                for (var i = 0; i < deptUsers.length; i++) {
                    var userindept = _.find(arrUsers, function (user) {
                        return user.value === deptUsers[i].userid;
                    });

                    if (userindept) {
                        var selected = {
                            "value": "user_" + userindept.value,
                            "data": userindept.fullname,
                            "parentid": parentid,
                            "state": "leaf",
                            "metadata": { "id": "user_" + userindept.value },
                            "attr": {
                                "id": "user_" + userindept.value,
                                "rel": "people",
                                "idext": deptUsers[i].idext,
                                hasReceiveDocument: deptUsers[i].hasReceiveDocument
                            }
                        };
                        children.push(selected);
                    }
                }
            }

            return children;
        };

        var _bindJsTree = function (divTree, hasUser, hasCheckbox,
            hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind) {
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
                var children = _getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
                divTree.jstree({
                    "json_data": {
                        "data": [
                            {
                                "data": deptRoot.data.toString(),
                                "metadata": { id: deptRoot.value },
                                "state": "closed",
                                "attr": {
                                    "id": deptRoot.value, "rel": "dept",
                                    "idext": deptRoot.idext, "label": deptRoot.label
                                },
                                "children": children
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
                            _appendChild(data.rslt.obj, parseInt(data.rslt.obj.attr("id")), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles);
                        }
                    });
                });
            }
        };

        var _appendChild = function ($parent, parentId, hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles) {
            var child = _getChildrens(parentId, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
            if (child.length > 0) {
                var $newChild = $('<ul></ul>');
                $newChild.appendTo($parent);
                if (hasCheckbox) {
                    // $.template("checkboxTemplate", itemTreeviewCheckboxTemplate);
                    $.tmpl(itemTreeviewCheckboxTemplate, child).appendTo($newChild);
                    $($parent).find("li").each(function (idx, listItem) {
                        $(listItem).addClass($parent.hasClass("jstree-checked") ? "jstree-checked" : "jstree-unchecked");
                    });
                } else {
                    // $.template("itemTreeviewTemplate", itemTreeviewTemplate);
                    $.tmpl(itemTreeviewTemplate, child).appendTo($newChild);
                }
                $newChild.children("li:last").addClass("jstree-last");
            }
        };

        var _getUserFromDepts = function (depts, allUserDept) {
            /// <summary>
            /// Trả về danh sách các user thuộc các phòng ban cần lấy
            /// </summary>
            /// <param name="depts">Mảng id các phòng ban</param>
            /// <returns type="">Mảng id các user đã được uniq</returns>
            var result = [];
            depts.forEach(function (dept) {
                // lấy ra phòng ban và các phòng ban con của nó.
                var depts = _.filter(allUserDept, function (num) {
                    return num.departmentid === dept || num.idext.indexOf('.' + dept + '.') > 0;
                });

                if (depts.length > 0) {
                    result = _.union(result, _.pluck(depts, 'userid'));
                }
            });
            result = _.uniq(result, false);
            return result;
        }

        var _parseUserItem = function (value, name, cssClass) {
            var result;
            var template = '<li class="list-group-item {2}">\
                                <div class="row">\
                                    <label class="mdl-checkbox mdl-js-checkbox checkbox document-color">\
                                       <input class="mdl-checkbox__input" name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                        <span class="document-color-1"><i class="icon-check"></i></span>\
                                    </label>\
                                    <span style="margin-left: 15px;">{1}</span>\
                                </div>\
                            </li>';
            result = $(String.format(template, value, name, cssClass));
            if (egov.isMobile) {
                $(result).find(".mdl-checkbox").materialCheckbox();
            }
            return result;
        };

        //#endregion

        return TransferExtend;
    });