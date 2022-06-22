define([egov.template.deptUser],
    function (Template) {
        "use strict";

        //#region Models

        //#endregion

        var _resource = egov.resources.document.transfer;
        var max, max1;

        //#region Views

        var DeptUserView = Backbone.View.extend({
            className: 'dropdown-userDeptVote',
            template: Template,

            initialize: function () {
                /// <summary>
                /// Khởi tạo
                /// </summary>
                return this;
            },

            render: function (hasDg, showRelation, callback, callback2) {
                /// <summary>
                /// Hiển thị form đồng gửi
                /// </summary>
                /// <returns type=""></returns>
                if ($("body .dropdown-userDeptVote").length > 0) {
                    $("body .dropdown-userDeptVote").remove();
                }

                this.$el.html(this.template);
                this.$el.bindResources();
                this.$el.appendTo("body");
                this.$dgOption = this.$('.dg-option');
                this.$relationOption = this.$('.relation-option');

                this.callbackFunc = callback;
                this.hasDg = hasDg;
                this.showRelation = showRelation;
                var that = this;

                // Tải các dữ liệu cần thiết trước khi bind form
                if (!egov.dataManager) {
                    return;
                }

                var dataManager = egov.dataManager;
                dataManager.getAllUsers({
                    success: function (allUsers) {
                        that.allUsers = allUsers;

                        dataManager.getAllJobtitle({
                            success: function (allJobTitles) {
                                that.allJobtitle = allJobTitles;

                                dataManager.getAllDept({
                                    success: function (allDepts) {
                                        that.allDepts = allDepts;

                                        dataManager.getAllUserDeptPosition({
                                            success: function (allUserDeptPosition) {
                                                that.allUserDeptPosition = allUserDeptPosition;
                                                that._bind(callback2);
                                            }
                                        });
                                    }
                                });
                            }
                        });
                    }
                });

                if (egov.isMobile) {
                    this.$(".mdl-checkbox").materialCheckbox();
                    this.$(".mdl-button").materialButton();
                    this.$(".mdl-radio").materialRadio();
                    this.$(".mdl-textfield").materialTextfield();
                }
                return this;
            },

            reRender: function (hasDg, showRelation, callback) {
                /// <summary>
                /// Render lại form đồng gửi
                /// </summary>
                /// <param name="hasDg">Cho phép gửi Đồng xử lý</param>
                /// <param name="showRelation">Hiển thị chọn theo quan hệ</param>
                /// <param name="callback">Hàm callback khi thay đổi lựa chọn</param>
                this.callbackFunc = callback;
                this.hasDg = hasDg;
                this.showRelation = showRelation;
                this._show();
                this._bindCallback();
                that.$('#dgUserVote').focus();
            },

            destroy: function () {
                /// <summary>
                /// Hủy bỏ các cây văn người dung- phòng ban- chức danh
                /// </summary>
                this.$('#dgUsersVote').each(function () {
                    $(this).customDropdown("destroy");
                });
                this.$el.remove();
            },

            callback: function () {
                /// <summary>
                /// Gọi hàm callback sau sự kiện chọn hoặc bỏ chọn người dùng, phòng ban, chức vụ
                /// </summary>
                if (typeof this.callbackFunc === 'function') {
                    this.callbackFunc();
                }
            },

            serialize: function () {
                /// <summary>
                /// Trả về không gian user được chọn
                /// </summary>
                this.result = {
                    isDg: this.$('[name="isDg"]:checked').val() === '1',
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

                // Trường hợp chọn tất cả
                if (selected.isAllUser || (selected.isAllDept && selected.isAllJobtitle)) {
                    result.push({
                        label: _resource.sendAll,
                        value: 'all',
                        type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                    });

                    return result;
                    //containner.append(parseUserItem(0, _resource.sendAll, false));
                }

                // Hiển thị các lựa chọn ở chọn user - phòng ban
                selected.depts.forEach(function (dept) {
                    result.push({
                        label: dept.data,
                        value: "FilterDepartmentVote-" + dept.value,
                        type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                    });
                    //containner.append(parseUserItem(0, dept.data, false));
                });
                // Hiển thị các lựa chọn ở chọn user - phòng ban
                ///Loại bỏ các user trùng nhau
                //  selected.users = _.uniq(selected.users);
                selected.users.forEach(function (user) {
                    result.push({
                        label: user.fullname + " - " + user.username,
                        value: "FilterDepartmentVote-user_" + user.value,
                        type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                    });
                    //containner.append(parseUserItem(0, user.fullname, false));
                });

                // Chọn tất cả phòng ban
                if (selected.isAllDept) {
                    if (!selected.isAllJobtitle && selected.jobtitlies.length > 0) {
                        // Không chọn tất cả chức vụ thì hiển thị danh sách các chức vụ thuộc phòng ban root
                        var dept = " thuộc " + selected.jobDepts[0].data;
                        var text = _.pluck(selected.jobtitlies, 'label').join(', ') + dept;
                        result.push({
                            label: text,
                            value: 'JobtitleForDept',
                            type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                        });

                        //containner.append(parseUserItem(0, text, false));
                    }
                }
                else if (selected.isAllJobtitle) {// Chọn tất cả chức vụ
                    // Trường hợp chọn tất cả phòng ban đã set ở trên
                    if (!selected.isAllDept) {
                        selected.jobDepts.forEach(function (dept) {
                            result.push({
                                label: dept.data,
                                value: '',
                                type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                            });
                            // containner.append(parseUserItem(0, dept.data, false));
                        });
                    }
                }
                else if (selected.jobtitlies.length > 0) {
                    selected.jobDepts.forEach(function (dept) {
                        var text = _.pluck(selected.jobtitlies, 'label').join(', ') + " thuộc " + dept.data;
                        result.push({
                            label: text,
                            value: 'DeptForJobtitle-' + dept.value,
                            type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                        });
                        //containner.append(parseUserItem(0, text, false));
                    });
                }

                //#region Xử lý giám sát

                // Hiển thị các lựa chọn ở chọn user - phòng ban
                selected.deptGs.forEach(function (dept) {
                    result.push({
                        label: dept.data,
                        value: "FilterDepartmentVote-" + dept.value,
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
                        value: "FilterDepartmentVote-user_" + user.value,
                        type: transferType.giamsat
                    });
                    //containner.append(parseUserItem(0, user.fullname, false));
                });

                //#endregion

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
                        // Không chọn tất cả chức vụ thì lấy user thuộc danh sách các chức vụ thuộc phòng ban root
                        var depts = _.filter(allUserDept, function (i) {
                            return _.find(selected.jobtitlies, function (jobtitle) {
                                return jobtitle.value === i.jobtitleid;
                            });
                        });

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
                    var depts = _.filter(allUserDept, function (i) {
                        var checkDept = _.find(selected.jobDepts, function (dept) {
                            //return dept.departmentid === i.departmentid;
                            return dept.value === i.departmentid;
                        });
                        var checkJob = _.find(selected.jobtitlies, function (jobtitle) {
                            return jobtitle.value === i.jobtitleid;
                        });

                        return checkDept && checkJob;
                    });

                    if (depts.length > 0) {
                        result = _.union(result, _getUserFromDepts(_.pluck(depts, 'departmentid'), allUserDept));
                    }
                }

                result = _.uniq(result, false);
                return result;
            },

            getUserGiamSat: function () {
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

                // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức danh
                $("#FilterDepartmentVote").jstree("get_checked", null, false).each(function () {
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
                return this.$('[name="isDg"]:checked').val() === '1';
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
                    else if (value.indexOf('FilterDepartmentVote-user_') !== -1) {
                        cssClass = 'dept-user';
                    }
                    else if (value.indexOf('FilterDepartmentVote-') !== -1) {
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

            uncheckPrivateAnoun: function (e) {
                var target = $(e.target);
                if (target.is(':checkbox')) {
                    var value = target.val().split("-");
                    if (value[0] === "all") {
                        //$("#JobtitleForDept").find("input[type=checkbox]").click();
                        var jobTitles = $("#JobtitleForDept").find("input[type=checkbox]");
                        for (var i = 0; i < jobTitles.length; i++) {
                            if ($(jobTitles[i]).attr("checked") !== undefined) {
                                $(jobTitles[i]).removeAttr("checked");
                            }
                        }
                        //this._dongGui();
                        $("#FilterDepartmentVote").jstree("uncheck_node", $("#FilterDepartmentVote").find("li"));
                    }
                    else if (value[0] === "FilterDepartmentVote" || value[0] === "DeptForJobtitle") {
                        $("#" + value[0]).jstree("uncheck_node", $("#" + value[0]).find("li#" + value[1]));
                        if (value[0] === "DeptForJobtitle") {
                            var destination = egov.views.dg.getDestination();
                            for (var i = 0; i < destination.length; i++) {
                                if (destination[i].value === "JobtitleForDept") {
                                    $("#JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
                                    //this._dongGui();
                                }
                            }
                        }
                    }
                    else if (value[0] === "JobtitleForDept") {
                        $("#" + value[0]).find("input[type=checkbox]").removeAttr("checked");
                        //this._dongGui();
                    }
                }
            },

            uncheckGiamsat: function (e) {
                var target = $(e.target);
                if (target.is(':checkbox')) {
                    var value = target.val().split("-");
                    if (value[0] === "FilterDepartmentVote") {
                        $("#FilterDepartmentVoteGiamSat").jstree("uncheck_node", $("#FilterDepartmentVoteGiamSat").find("li#" + value[1]));
                    }
                }
            },

            //#region Private Methods

            _bind: function (callback) {
                var that = this;

                var $userDg = this.$('#dgUserVote');
                var $filterDept = this.$('.filterDeparmentVote#FilterDepartmentVote');
                that._bindUsers($userDg, $filterDept);

                //var $userGs = this.$('#dgUserGiamsat');
                //var $filterDeptGs = this.$('.filterDeparmentVote#FilterDepartmentVoteGiamSat');
                //that._bindUsers($userGs, $filterDeptGs);

                that._bindDepartments(function () {
                    that._bindJobtitlies(function () {
                        that._show();
                        that._bindCallback();
                        egov.callback(callback);
                        that.$el.bindResources();
                        that.$('#dgUserVote').focus();
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
                    ul.addClass('dropdown-menu');
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
                    _bindJsTree(that.$el.find('#FilterDepartmentVote'), true, true, false,
                                    allDepts, that.allUsers,
                                    that.allUserDeptPosition, null, []);

                    //_bindJsTree(that.$el.find('#FilterDepartmentVoteGiamSat'), true, true, false,
                    //                allDepts, that.allUsers,
                    //                that.allUserDeptPosition, null, []);

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
                var allJobtitle = that.allJobtitle;

                var max;
                for (var i = 0; max = allJobtitle.length, i < max; i++) {
                    var jobtitle = allJobtitle[i];
                    var jobItem = $('<li>').addClass('list-group-item');
                    jobItem.append('<label class="mdl-checkbox mdl-js-checkbox checkbox"><input class="mdl-checkbox__input" type="checkbox" value="' + jobtitle.value + '"/>' + ' ' + jobtitle.label + '</label>');
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

                $('#FilterDepartmentVote, #DeptForJobtitle, #FilterDepartmentVoteGiamSat').bind('change_state.jstree', function () {
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
                $("#FilterDepartmentVote").jstree("get_checked", null, false).each(function () {
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
                $("#FilterDepartmentVote").jstree("uncheck_node", $("#FilterDepartmentVote").find("li"));
            },

            _show: function () {
                var that = this;
                that.$('#dgUsersVote').each(function () {
                    $(this).customDropdown({
                        css: {
                            width: 300,
                            height: 'auto'
                        }
                    })
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
                this.$filterDept = this.$('.filterDeparmentVote');
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
                this.$('.filterDeparmentVote').jstree("check_all");
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
                this.$filterDept = this.$('.filterDeparmentVote');
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

        return DeptUserView;
    });
define([
   egov.template.referendum, "referendumModel"
],
function (template) {
    var Referendum = Backbone.View.extend({

        template: template,

        listVote: new ListVote(),

        events: {

        },

        initialize: function (options) {
            this.listenTo(this.listVote, 'add', this.addVote);
            this.listenTo(this.listVote, 'reset', this.resetVote);
            this.render();
            this.getVotes();
            return this;
        },

        render: function () {
            var that = this;
            $("#ListVoteBody").remove();
            installElement = $("<div id='ListVoteBody'>");
            installElement.appendTo("body");
            this.$el = $("#ListVoteBody");
            this.$el.html($.tmpl(this.template));

            var that = this;
            // that.$("#listRefendum").html(that.$el.ht)
            var dialogSetting = {
                width: 900,
                height: "auto",
                draggable: true,
                title: "Danh sách các cuộc thăm dò ý kiến",
                buttons: [{
                    text: "Tạo mới",
                    className: "btn-success",
                    click: function () {
                        that._create();
                    }
                },
                             {
                                 text: "Đóng",
                                 click: function () {
                                     that.$el.dialog("hide");
                                     that.$el.remove()
                                 }
                             }]
            };

            that.$el.dialog(dialogSetting);

            return that;
        },

        addVote: function (vote) {
            if (this.empty) {
                this.listVote.reset();
                this.empty = false;
            }
            var view = new ReferendumItem({ model: vote });
            this.$(".listVote").append(view.render().el);
        },

        getVotes: function () {
            var that = this;
            that.listVote.reset()
            egov.request.getVotes({
                data: {},
                success: function (result) {
                    if (result.length == 0) {
                        that.empty = true;
                        this.$(".listVote").html("<tr><td colspan='4'>Không có cuộc trưng cầu nào</td></tr>");
                        return;
                    }
                    for (var i = 0; i < result.length; i++) {
                        var vote = new VoteModel(result[i]);
                        that.listVote.add(vote);
                    }

                    that.listVote.comparator = function (model) {
                        return model.get('IsNow');
                    }
                    that.listVote.sort();
                },
                error: function (error) { }
            });
        },

        resetVote: function () {
            this.$(".listVote").html("");
        },

        _create: function () {
            that = this;
            require(['referendumCreate'], function (referendumCreate) {
                var create = new referendumCreate({ vote: that });
            });
        }
    });

    var ReferendumItem = Backbone.View.extend({
        tagName: "tr",

        events: {
            "dblclick td": "getShowView",
            "click .editVote": "editVote",
            "click .deleteVote": "deleteVote",
            "click .viewVote": "getShowView"
        },

        template:
            '<td class="wraptext">' +
                        '${Title}' +
                       '</td>' +
                       '<td class="second-color time-begin-vote" style="text-align:right;width: 150px"> ${TimeBeginFormat} </td>' +
                       '<td class="second-color wraptext" style="width: 100px">' +
                       '${UsernameCreate}' + '</td>' +
            '<td style="text-align:center;width: 100px">      <label class="checkbox document-color">          <input name="checkbox[]" value="2378" type="checkbox"{{if IsNow}}checked{{/if}} disabled>          <span class="document-color-1"><i class="icon-check"></i></span>      </label>  </td>'
            + '<td class="second-color wraptext" style="width: 100px">' +
                       '{{if IsCreate}}<a class="editVote"href="#" style="color: blue">Chi tiết</a> <a href="#" class="deleteVote" style="color: red">Xóa</a>{{else}}<a class="viewVote"href="#">Xem</a>{{/if}}' + '</td>',

        model: VoteModel,

        initialize: function (options) {
            var setTime = function (timeStr) {
                if (timeStr instanceof Date) { } else {
                    timeStr = timeStr.match(/\d+/)[0];
                }
                var time = new Date(Number(timeStr));
                return time.getHours() + ":" + ("0" + time.getMinutes()).slice(-2) + "   " + time.getDate() + "-" + Number(time.getMonth() + 1) + "-" + time.getFullYear()
            }
            var convertTime = function (timeStr) {
                if (timeStr instanceof Date) { } else {
                    timeStr = timeStr.match(/\d+/)[0];
                }
                var time = new Date(Number(timeStr));
                return time;
            }
            this.listenTo(this.model, 'add', this.render);
            this.listenTo(this.model, 'change', this.render);
            this.model.set({ "TimeBeginFormat": setTime(this.model.get("TimeBegin")) });
            if (new Date().getTime() > convertTime(this.model.get("TimeBegin")).getTime() && new Date().getTime() < convertTime(this.model.get("TimeEnd")).getTime()) {
                this.model.set({ "IsNow": true })
            }

        },

        render: function () {
            var that = this;
            that.$el.html($.tmpl(that.template, that.model.toJSON()));

            return that;
        },
        getShowView: function () {
            this.showView(4);
        },

        editVote: function () {
            var that = this;
            egov.request.getVoteDetail({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    require(['referendumCreate'], function (referendumCreate) {
                        var create = new referendumCreate({ vote: result, isEdit: true });
                    });
                },
                error: function (error) { }
            });
        },

        deleteVote: function () {
            var that = this;
            var r = confirm("Bạn có muốn xóa cuộc trưng cầu này");
            if (r == false) {
                return false;
            }
            egov.request.deleteVote({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    that.model.destroy();
                    that.$el.remove();
                },
                error: function (error) { }
            });
        },

        showView: function (view) {
            var that = this;
            egov.request.getVoteDetail({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    require(['referendumVote'], function (referendumVote) {
                        var model = new VoteModel(result);
                        var vote = new referendumVote({ model: model, view: view });
                        vote.render();
                    });
                },
                error: function (error) { }
            });
        },

        _create: function () {
        }
    });

    return Referendum;
});
define([egov.template.deptUserView],
    function (Template) {
        "use strict";

        //#region Models

        //#endregion

        var _resource = egov.resources.document.transfer;
        var max, max1;

        //#region Views

        var DeptUserView = Backbone.View.extend({
            className: 'dropdown-userDept',
            template: Template,

            initialize: function () {
                /// <summary>
                /// Khởi tạo
                /// </summary>
                return this;
            },

            render: function (hasDg, showRelation, callback, callback2) {
                /// <summary>
                /// Hiển thị form đồng gửi
                /// </summary>
                /// <returns type=""></returns>
                if ($("body .dropdown-userDept").length > 0) {
                    $("body .dropdown-userDept").remove();
                }

                this.$el.html(this.template);
                this.$el.bindResources();
                this.$el.appendTo("body");

                this.callbackFunc = callback;
                this.hasDg = hasDg;
                this.showRelation = showRelation;
                var that = this;

                // Tải các dữ liệu cần thiết trước khi bind form
                if (!egov.dataManager) {
                    return;
                }

                var dataManager = egov.dataManager;
                dataManager.getAllUsers({
                    success: function (allUsers) {
                        that.allUsers = allUsers;

                        dataManager.getAllJobtitle({
                            success: function (allJobTitles) {
                                that.allJobtitle = allJobTitles;

                                dataManager.getAllDept({
                                    success: function (allDepts) {
                                        that.allDepts = allDepts;

                                        dataManager.getAllUserDeptPosition({
                                            success: function (allUserDeptPosition) {
                                                that.allUserDeptPosition = allUserDeptPosition;
                                                that._bind(callback2);
                                            }
                                        });
                                    }
                                });
                            }
                        });
                    }
                });

                if (egov.isMobile) {
                    this.$(".mdl-checkbox").materialCheckbox();
                    this.$(".mdl-button").materialButton();
                    this.$(".mdl-radio").materialRadio();
                    this.$(".mdl-textfield").materialTextfield();
                }
                $(".filterDeparmentView").hide();
                return this;
            },

            reRender: function (hasDg, showRelation, callback) {
                /// <summary>
                /// Render lại form đồng gửi
                /// </summary>
                /// <param name="hasDg">Cho phép gửi Đồng xử lý</param>
                /// <param name="showRelation">Hiển thị chọn theo quan hệ</param>
                /// <param name="callback">Hàm callback khi thay đổi lựa chọn</param>
                this.callbackFunc = callback;
                this.hasDg = hasDg;
                this.showRelation = showRelation;
                this._show();
                this._bindCallback();
                $(".filterDeparmentView").hide();
                that.$('#dgUserView').focus();
            },

            destroy: function () {
                /// <summary>
                /// Hủy bỏ các cây văn người dung- phòng ban- chức danh
                /// </summary>
                this.$('#dgUsersView').each(function () {
                    $(this).customDropdown("destroy");
                });
                this.$el.remove();
            },

            callback: function () {
                /// <summary>
                /// Gọi hàm callback sau sự kiện chọn hoặc bỏ chọn người dùng, phòng ban, chức vụ
                /// </summary>
                if (typeof this.callbackFunc === 'function') {
                    this.callbackFunc();
                }
            },

            serialize: function () {
                /// <summary>
                /// Trả về không gian user được chọn
                /// </summary>
                this.result = {
                    isDg: this.$('[name="isDg"]:checked').val() === '1',
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

                // Trường hợp chọn tất cả
                if (selected.isAllUser || (selected.isAllDept && selected.isAllJobtitle)) {
                    result.push({
                        label: _resource.sendAll,
                        value: 'all',
                        type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                    });

                    return result;
                    //containner.append(parseUserItem(0, _resource.sendAll, false));
                }

                // Hiển thị các lựa chọn ở chọn user - phòng ban
                selected.depts.forEach(function (dept) {
                    result.push({
                        label: dept.data,
                        value: "FilterDepartmentView-" + dept.value,
                        type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                    });
                    //containner.append(parseUserItem(0, dept.data, false));
                });
                // Hiển thị các lựa chọn ở chọn user - phòng ban
                ///Loại bỏ các user trùng nhau
                //  selected.users = _.uniq(selected.users);
                selected.users.forEach(function (user) {
                    result.push({
                        label: user.fullname + " - " + user.username,
                        value: "FilterDepartmentView-user_" + user.value,
                        type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                    });
                    //containner.append(parseUserItem(0, user.fullname, false));
                });

                // Chọn tất cả phòng ban
                if (selected.isAllDept) {
                    if (!selected.isAllJobtitle && selected.jobtitlies.length > 0) {
                        // Không chọn tất cả chức vụ thì hiển thị danh sách các chức vụ thuộc phòng ban root
                        var dept = " thuộc " + selected.jobDepts[0].data;
                        var text = _.pluck(selected.jobtitlies, 'label').join(', ') + dept;
                        result.push({
                            label: text,
                            value: 'JobtitleForDept',
                            type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                        });

                        //containner.append(parseUserItem(0, text, false));
                    }
                }
                else if (selected.isAllJobtitle) {// Chọn tất cả chức vụ
                    // Trường hợp chọn tất cả phòng ban đã set ở trên
                    if (!selected.isAllDept) {
                        selected.jobDepts.forEach(function (dept) {
                            result.push({
                                label: dept.data,
                                value: '',
                                type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                            });
                            // containner.append(parseUserItem(0, dept.data, false));
                        });
                    }
                }
                else if (selected.jobtitlies.length > 0) {
                    selected.jobDepts.forEach(function (dept) {
                        var text = _.pluck(selected.jobtitlies, 'label').join(', ') + " thuộc " + dept.data;
                        result.push({
                            label: text,
                            value: 'DeptForJobtitle-' + dept.value,
                            type: selected.isDg ? transferType.dongxuly : transferType.thongbao
                        });
                        //containner.append(parseUserItem(0, text, false));
                    });
                }

                //#region Xử lý giám sát

                // Hiển thị các lựa chọn ở chọn user - phòng ban
                selected.deptGs.forEach(function (dept) {
                    result.push({
                        label: dept.data,
                        value: "FilterDepartmentView-" + dept.value,
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
                        value: "FilterDepartmentView-user_" + user.value,
                        type: transferType.giamsat
                    });
                    //containner.append(parseUserItem(0, user.fullname, false));
                });

                //#endregion

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
                        // Không chọn tất cả chức vụ thì lấy user thuộc danh sách các chức vụ thuộc phòng ban root
                        var depts = _.filter(allUserDept, function (i) {
                            return _.find(selected.jobtitlies, function (jobtitle) {
                                return jobtitle.value === i.jobtitleid;
                            });
                        });

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
                    var depts = _.filter(allUserDept, function (i) {
                        var checkDept = _.find(selected.jobDepts, function (dept) {
                            //return dept.departmentid === i.departmentid;
                            return dept.value === i.departmentid;
                        });
                        var checkJob = _.find(selected.jobtitlies, function (jobtitle) {
                            return jobtitle.value === i.jobtitleid;
                        });

                        return checkDept && checkJob;
                    });

                    if (depts.length > 0) {
                        result = _.union(result, _getUserFromDepts(_.pluck(depts, 'departmentid'), allUserDept));
                    }
                }

                result = _.uniq(result, false);
                return result;
            },

            getUserGiamSat: function () {
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

                // Loại bỏ các lựa chọn liên quan tới Phòng ban - Chức danh
                $("#FilterDepartmentView").jstree("get_checked", null, false).each(function () {
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
                return this.$('[name="isDg"]:checked').val() === '1';
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
                    else if (value.indexOf('FilterDepartmentView-user_') !== -1) {
                        cssClass = 'dept-user';
                    }
                    else if (value.indexOf('FilterDepartmentView-') !== -1) {
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

            uncheckPrivateAnoun: function (e) {
                var that = this
                var target = $(e.target);
                if (target.is(':checkbox')) {
                    var value = target.val().split("-");
                    if (value[0] === "all") {
                        //$("#JobtitleForDept").find("input[type=checkbox]").click();
                        var jobTitles = $("#JobtitleForDept").find("input[type=checkbox]");
                        for (var i = 0; i < jobTitles.length; i++) {
                            if ($(jobTitles[i]).attr("checked") !== undefined) {
                                $(jobTitles[i]).removeAttr("checked");
                            }
                        }
                        //this._dongGui();
                        $("#FilterDepartmentView").jstree("uncheck_node", $("#FilterDepartmentView").find("li"));
                    }
                    else if (value[0] === "FilterDepartmentView" || value[0] === "DeptForJobtitle") {
                        $("#" + value[0]).jstree("uncheck_node", $("#" + value[0]).find("li#" + value[1]));
                        if (value[0] === "DeptForJobtitle") {
                            var destination = egov.views.dg.getDestination();
                            for (var i = 0; i < destination.length; i++) {
                                if (destination[i].value === "JobtitleForDept") {
                                    $("#JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
                                    //this._dongGui();
                                }
                            }
                        }
                    }
                    else if (value[0] === "JobtitleForDept") {
                        $("#" + value[0]).find("input[type=checkbox]").removeAttr("checked");
                        //this._dongGui();
                    }
                }
            },

            uncheckGiamsat: function (e) {
                var target = $(e.target);
                if (target.is(':checkbox')) {
                    var value = target.val().split("-");
                    if (value[0] === "FilterDepartmentView") {
                        $("#FilterDepartmentGiamSat").jstree("uncheck_node", $("#FilterDepartmentGiamSat").find("li#" + value[1]));
                    }
                }
            },

            //#region Private Methods

            _bind: function (callback) {
                var that = this;
                var $userDg = this.$('#dgUserView');
                var $filterDept = this.$('.filterDeparmentView#FilterDepartmentView');
                that._bindUsers($userDg, $filterDept);

                //var $userGs = this.$('#dgUserGiamsat');
                //var $filterDeptGs = this.$('.filterDeparment#FilterDepartmentGiamSat');
                //that._bindUsers($userGs, $filterDeptGs);

                that._bindDepartments(function () {
                    that._bindJobtitlies(function () {
                        that._show();
                        that._bindCallback();
                        egov.callback(callback);
                        that.$el.bindResources();
                        that.$('#dgUserView').focus();
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
                    ul.addClass('dropdown-menu');
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
                    _bindJsTree(that.$el.find('#FilterDepartmentView'), true, true, false,
                                    allDepts, that.allUsers,
                                    that.allUserDeptPosition, null, []);
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
                var allJobtitle = that.allJobtitle;

                var max;
                for (var i = 0; max = allJobtitle.length, i < max; i++) {
                    var jobtitle = allJobtitle[i];
                    var jobItem = $('<li>').addClass('list-group-item');
                    jobItem.append('<label class="mdl-checkbox mdl-js-checkbox checkbox"><input class="mdl-checkbox__input" type="checkbox" value="' + jobtitle.value + '"/>' + ' ' + jobtitle.label + '</label>');
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

                $('#FilterDepartmentView, #DeptForJobtitle, #FilterDepartmentGiamSat').bind('change_state.jstree', function () {
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
                $("#FilterDepartmentView").jstree("get_checked", null, false).each(function () {
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
                $("#FilterDepartmentView").jstree("uncheck_node", $("#FilterDepartmentView").find("li"));
                $("#DeptForJobtitle").jstree("uncheck_node", $("#DeptForJobtitle").find("li"));
                $("#JobtitleForDept").find("input[type=checkbox]").removeAttr("checked");
            },

            _show: function () {
                var that = this;
                that.$('#dgUsersView').each(function () {
                    $(this).customDropdown({
                        css: {
                            width: 300,
                            height: 'auto'
                        }
                    })
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
                this.$filterDept = this.$('.filterDeparmentView');
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
                this.$('.filterDeparmentView').jstree("check_all");
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
                this.$filterDept = this.$('.filterDeparmentView');
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

        return DeptUserView;
    });
define([
   egov.template.createReferendum, "referendumModel"
],
function (template) {

    var ReferendumCreate = Backbone.View.extend({

        template: template,

        model: VoteModel,

        initialize: function (options) {
            /// <summary>
            /// Contructor
            /// </summary>
            /// <param name="options">{document, callback}</param>
            this.IsEdit = options.isEdit ? true : false;
            this.vote = options.vote;
            this.model = new VoteModel();
            var that = this
            if (this.IsEdit) {
                that.listDetail = JSON.parse(that.vote.ListOpinion);
                that.model = new VoteModel(that.vote);
            }
            this.templateVoteDetail = '<tr><td> <input name="text" value="${TitleDetail}" type="text" class="form-control vote-detail-title" style="border:none" placeholder="+ Thêm ý kiến"> </td></tr>';
            this.render();
            return this;
        },

        render: function () {
            /// <summary>
            /// Page Load
            /// </summary>
            if ($("#Referendum").length === 0) {
                var installElementReferendum = $("<div id='Referendum'>");
                installElementReferendum.appendTo("body");
            }
            this.$el = $("#Referendum");
            this.$el.html($.tmpl(this.template, this.model.toJSON()));
            this.$dg = this.$('.dg-view');
            this.$dg1 = this.$('.dg-view1');
            this.$privateAnounc = this.$('.private-anoun ul:first');
            this.$privateAnounc1 = this.$('.private-anoun1 ul:first');
            var buttons = [];
            var title = "Thêm cuộc trưng cầu ý kiến";
            var titleButton = "Tạo mới";


            if (this.IsEdit) {
                title = "Sửa cuộc trưng cầu";
                titleButton = "Sửa";
                var timeBegin = convertTime(this.model.get("TimeBegin"));
                if (timeBegin.getTime() < new Date().getTime()) {
                    $("#validateError").html("Đã hết thời gian sửa");
                } else {
                    var btn = {
                        text: titleButton,
                        className: "btn-success",
                        click: function () {
                            that._create();
                        }
                    }
                    buttons.push(btn);
                }
            } else {
                var btn = {
                    text: titleButton,
                    className: "btn-success",
                    click: function () {
                        that._create();
                    }
                }
                buttons.push(btn);
            }
            buttons.push({
                text: "Đóng",
                click: function () {
                    that.$el.dialog("hide");
                }
            })
            var that = this;
            var dialogSetting = {
                width: 900,
                height: "auto",
                draggable: true,
                title: title,
                buttons: buttons
            };

            that.$el.dialog(dialogSetting);
            that._destroyDg();
            that._destroyDg1();
            that.renderTitleDetail(that.listDetail);
            that.renderDatePicker();
            that._showDg1();
            that.renderEdit();
            $("#IsSyncUser").click(function () {
                var IsSyncUser = that.$el.find("#IsSyncUser").is(":checked");
                
                if (IsSyncUser) {
                    that._destroyDg();
                    that.$el.find(".private-anoun > ul").html("");
                    that.$el.find(".private-anoun").parent().hide();
                    that.$el.find("#titleVote").text("Danh sách tham gia bỏ phiếu và xem kết quả");
                } else {
                    that.$el.find("#titleVote").text("Danh sách tham gia bỏ phiếu");
                    that.$el.find(".private-anoun").parent().show();
                    that._showDg();
                }
            })
            return that;
        },

        renderEdit: function () {
            var that = this;
            if (that.IsEdit) {
                that._showDg();
                setTimeout(function () {
                        var listUserVote = that.vote.UsersVote.split(";");
                        var listUserView = that.vote.UsersView.split(";");
                        listUserVote = cleanArray(listUserVote);
                        listUserView = cleanArray(listUserView);
                        var list = listUserVote.map(function (item) {
                            return parseInt(item, 10);
                        });
                        var listView = listUserView.map(function (item) {
                            return parseInt(item, 10);
                        });
                        if (list.length == listView.length) {
                            egov.views.dg1._showUsers(list);
                            return;
                        }
                        $("#IsSyncUser").click();
                        that.$el.find("#titleVote").text("Danh sách tham gia bỏ phiếu");
                        that.$el.find(".private-anoun").parent().show();
                        egov.views.dg1._showUsers(list);
                        egov.views.dg._showUsers(listView);
                }, 1000)
            }
            
        },

        renderTitleDetail: function (data) {
            var that = this;
            var listVoteDetail = [];
            if (data == null) {
                for (var i = 0; i < 5; i++) {
                    var voteDetail = new VoteDetailModel();
                    listVoteDetail.push(voteDetail.toJSON())
                }
            } else {
                for (var i = 0; i < data.length; i++) {
                    var voteDetail = new VoteDetailModel(data[i]);
                    listVoteDetail.push(voteDetail.toJSON())
                }
                var voteDetail = new VoteDetailModel();
                listVoteDetail.push(voteDetail.toJSON())
            }
            that.$el.find(".listVoteDetail").html($.tmpl(that.templateVoteDetail, listVoteDetail))
            $(document).on("blur", ".vote-detail-title", function () {
                var count = 0;
                $(".vote-detail-title").each(function () {
                    var value = $(this).val();
                    if (value == "") {
                        count++;
                    }
                });
                if (count < 2) {
                    var voteDetail = new VoteDetailModel();
                    that.$el.find(".listVoteDetail").append($.tmpl(that.templateVoteDetail, voteDetail.toJSON()));
                }
            })
            $(document).on("keyup", ".vote-detail-title", function (e) {
                var target = $(e.target).closest("tr");
                if (e.keyCode == 40) {
                    var trNext = target.next();
                    trNext.find(".vote-detail-title").focus();
                }
                if (e.keyCode == 38) {
                    var trPreview = target.prev();
                    trPreview.find(".vote-detail-title").focus();
                }
            })

        },

        renderDatePicker: function () {
            var that = this;
            $.datepicker.regional["vi-VN"] =
                     {
                         closeText: "Đóng",
                         prevText: "Trước",
                         nextText: "Sau",
                         currentText: "Hôm nay",
                         monthNames: ["Tháng một", "Tháng hai", "Tháng ba", "Tháng tư", "Tháng năm", "Tháng sáu", "Tháng bảy", "Tháng tám", "Tháng chín", "Tháng mười", "Tháng mười một", "Tháng mười hai"],
                         monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
                         dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
                         dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
                         dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
                         weekHeader: "Tuần",
                         dateFormat: "dd/mm/yy",
                         firstDay: 1,
                         isRTL: false,
                         showMonthAfterYear: false,
                         yearSuffix: ""
                     };

            $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);
            var dates = that.$el.find("#beginDate,#endDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                defaultDate: new Date(),
                onSelect: function (selectedDate) {
                    var dateObject = $(this).datepicker('getDate');
                    if ($(this).attr("id") == "beginDate") {
                        that.model.set({ TimeBegin: dateObject })
                    }
                    if ($(this).attr("id") == "endDate") {
                        that.model.set({ TimeEnd: dateObject })
                    }
                }
            });

            var formatTime = function (time) {
                return time.getHours() + ":" + ("0" + time.getMinutes()).slice(-2);
            }
            if (that.IsEdit) {
                var timeBegin = convertTime(that.model.get("TimeBegin"));
                var timeEnd = convertTime(that.model.get("TimeEnd"));

                $("#beginDate").datepicker("setDate", timeBegin);
                $("#endDate").datepicker("setDate", timeEnd);
                $("#timeBegin").val(formatTime(timeBegin))
                $("#timeEnd").val(formatTime(timeEnd))
            } else {
                $("#beginDate,#endDate").datepicker("setDate", new Date());
            }
            var times = that.$el.find("#timeBegin,#timeEnd").timepicker({
                timeFormat: 'H:i',
                durationTime: new Date()
            });
        },

        uncheckPrivateAnoun: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            egov.views.dg.uncheckPrivateAnoun(e);
            this._selectDg();
        },
        uncheckPrivateAnoun1: function (e) {
            /// <summary>
            /// Bỏ check đồng gửi
            /// </summary>
            /// <param name="e">event</param>
            egov.views.dg1.uncheckPrivateAnoun(e);
            this._selectDg1();
        },
        _showDg: function () {
            /// <summary>
            /// Hiển thị form đồng gửi
            /// </summary>
            var that = this;
            if (!that.$dg.is(':not(:empty)')) {
                if (!egov.views.dg) {
                    require(['referendumDropdownUserView'], function (deptUser) {
                        egov.views.dg = new deptUser;
                        egov.views.dg.render(false, false, function () {
                            that._selectDg();
                        }, function () {
                            that.$dg.html(egov.views.dg.$el);
                        });
                    });
                }
                else {
                    egov.views.dg.render(false, false, function () {
                        that._selectDg();
                    });
                    that.$dg.html(egov.views.dg.$el);
                };
            }
        },

        _showDg1: function () {
            var that = this;
            if (!that.$dg1.is(':not(:empty)')) {
                if (!egov.views.dg1) {
                    require(['referendumDropdownUser'], function (deptUser) {
                        egov.views.dg1 = new deptUser;
                        egov.views.dg1.render(false, false, function () {
                            that._selectDg1();
                        }, function () {
                            that.$dg1.html(egov.views.dg1.$el);
                        }, true);
                    });
                }
                else {
                    egov.views.dg1.render(false, false, function () {
                        that._selectDg1();
                    });
                    that.$dg1.html(egov.views.dg1.$el);
                };

            }
        },

        _selectDg: function () {
            /// <summary>
            /// Hiển thị select đồng gửi
            /// </summary>
            var that = this;
            that.$privateAnounc.empty();
            egov.views.dg.selectDg(this.$privateAnounc);
            that.$privateAnounc.find(".checkbox").on("click", function (e) {
                that.uncheckPrivateAnoun(e);
            })
        },

        _selectDg1: function () {
            /// <summary>
            /// Hiển thị select đồng gửi
            /// </summary>
            var that = this;
            that.$privateAnounc1.empty();
            egov.views.dg1.selectDg(this.$privateAnounc1);
            that.$privateAnounc1.find(".checkbox").on("click", function (e) {
                that.uncheckPrivateAnoun1(e);
            })
        },

        _destroyDg: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg) {
                egov.views.dg.destroy();
            }
        },
        _destroyDg1: function () {
            ///<summay>
            /// Hủy bỏ hiển thị cây người dùng phòng ban, cây phong ban, cây chức danh phòng ban
            ///</summay>
            if (egov.views.dg1) {
                egov.views.dg1.destroy();
            }
        },

        setTime: function (id, attrModel) {
            var that = this;
            var time = $(id).val();
            var nameId = $(id).attr("id");
            if (time) {
                var hour = time.split(":")[0];
                var minute = time.split(":")[1];
                var dateBegin = that.model.get(attrModel);
                if (dateBegin instanceof Date) {
                } else {
                    dateBegin = convertTime(dateBegin)
                }
                dateBegin.setHours(Number(hour));
                dateBegin.setMinutes(Number(hour));
                var dateObj = {};
                dateObj[attrModel] = dateBegin
                that.model.set(dateObj);
            }
        },

        _validate: function () {
            var that = this;
            var IsSyncUser = that.$el.find("#IsSyncUser").is(":checked");
            var title = that.$el.find(".comment").val();
            var listDetail = [];
            $(".vote-detail-title").each(function () {
                var value = $(this).val();
                if (value != "") {
                    listDetail.push(value)
                }
            });
            if (title == "") {
                return "Chưa có tiêu đề";
            }
            if (listDetail.length == 0) {
                return "Chưa có ý kiến nào được ghi";
            }
            if (egov.views.dg1.getUserConsults().length == 0) {
                return "Chưa có người nào được tham gia trưng cầu";
            }

            if ((egov.views.dg && egov.views.dg.getUserConsults().length) == 0 && !IsSyncUser) {
                return "Chưa có người nào được xem kết quả";
            }

            if (that.model.get("TimeEnd").getTime() < that.model.get("TimeBegin").getTime()) {
                return "Thời gian bắt đầu không được lơn hơn thời gian kết thúc";
            }
            return false;
        },

        _create: function () {
            var that = this;
            that.setTime("#timeBegin", "TimeBegin");
            that.setTime("#timeEnd", "TimeEnd");
            var validate = that._validate();
            if (validate) {
                $("#validateError").html(validate);
                return false;
            }
            var IsSyncUser = that.$el.find("#IsSyncUser").is(":checked");

            var userVote = egov.views.dg1.getUserConsults();
            var userView = [];
            if (IsSyncUser) {
                userView = userVote;
            } else {
                userView = userView = egov.views.dg.getUserConsults();
            }

            var title = that.$el.find(".comment").val();
            var isMultiSelect = that.$el.find("#IsMultiSelect").is(":checked");
            var isPublic = that.$el.find("#IsPublic").is(":checked");
            var isCommentDiff = that.$el.find("#IsCommentDiff").is(":checked");
            var isViewResultImmediately = that.$el.find("#IsViewResultImmediately").is(":checked");
            var isNotify = that.$el.find("#IsNotify").is(":checked");
            var listDetail = [];
            $(".vote-detail-title").each(function () {
                var value = $(this).val();
                if (value != "") {
                    var voteDetail = new VoteDetailModel({
                        VoteId: 0,
                        VoteDetailId: 0,
                        UserIdCreate: 0,
                        UserIdVote: "",
                        TitleDetail: value,
                    });
                    listDetail.push(voteDetail.toJSON())
                }
            });
            var strUserVote = JSON.stringify(userVote).replace(/[^a-zA-Z0-9]/g, ';');
            var strUserView = JSON.stringify(userView).replace(/[^a-zA-Z0-9]/g, ';');
            that.model.set({
                Title: title,
                UsersView: strUserView,
                UsersVote: strUserVote,
                IsMultiSelect: isMultiSelect,
                IsPublic: isPublic,
                IsCommentDiff: isCommentDiff,
                IsViewResultImmediately: isViewResultImmediately,
                IsNotify: isNotify,
                VoteDetailId: JSON.stringify(listDetail)
            });
            if (that.IsEdit) {
                egov.request.updateVote({
                    data: {
                        voteStr: JSON.stringify(that.model.toJSON())
                    },
                    success: function (result) {
                        if (result.error) {
                            $("#validateError").html(result.content);
                            return;
                        }
                        var vote = new VoteModel(result.content);
                        that.$el.dialog("hide");
                    },
                    error: function (error) {
                        $("#validateError").html("Có lỗi khi gửi lên server");
                    }
                });
                return;
            }
            egov.request.createVote({
                data: {
                    vote: JSON.stringify(that.model.toJSON())
                },
                success: function (result) {
                    if (result.error) {
                        $("#validateError").html(result.content);
                        return;
                    }
                    var vote = new VoteModel(result.content);
                    vote.set("IsCreate", true);
                    that.vote.listVote.add(vote)
                    that.$el.dialog("hide");
                },
                error: function (error) {
                    $("#validateError").html("Có lỗi khi gửi lên server");
                }
            });
        }
    });

    function cleanArray(actual) {
        var newArray = new Array();
        for (var i = 0; i < actual.length; i++) {
            if (actual[i]) {
                newArray.push(actual[i]);
            }
        }
        return newArray;
    }

    var convertTime = function (timeStr) {
        if (timeStr instanceof Date) { } else {
            timeStr = timeStr.match(/\d+/)[0];
        }
        var time = new Date(Number(timeStr));
        return time;
    }

    return ReferendumCreate;
    //if ($("#Referendum").length === 0) {
    //    var installElementReferendum = $("<div id='Referendum'>");
    //    installElementReferendum.appendTo("body");
    //}

    //installElementReferendum = $("#Referendum");

    //var html = template;
    //installElementReferendum.html(html);

    //installElementReferendum.dialog({
    //    width: 900,
    //    height: "auto",
    //    draggable: true,
    //    title: "Thêm cuộc trưng cầu ý kiến",
    //    buttons: [
    //                 {
    //                     text: "Tạo mới",
    //                     className: "btn-success",
    //                     click: function () {
    //                     }
    //                 },
    //                 {
    //                      text: egov.resources.common.closeButton,
    //                      click: function () {

    //                      }
    //                 },
    //    ]
    //});
});
define([
   egov.template.voteReferendum,
   egov.template.voteItemReferendum,
   egov.template.voteOnlyItemReferendum,
   egov.template.viewOnlyItemReferendum,
   "referendumModel"
],
function (template, templateItem, templateOnlyItem, templateViewOnlyItem) {
    window.intervalVote = function () {

    }
    var ReferendumVote = Backbone.View.extend({

        template: template,
        // view : 2 giao diện xem và vote
        // view : 3 giao diện xem
        // view : 4 giao diện vote
        view: 3,
        events: {
            "keydown .oppinionDiff": "commentDiff"
        },

        initialize: function (options) {
            this.listenTo(listVoteDetail, 'add', this.addVote);
            this.listenTo(listVoteDetail, 'reset', this.resetVote);
            //this.listenTo(listVoteDetail, 'all', this.renderData);
            var listUserVote = this.model.get("UsersVote").split(";");
            var listUserVoted =[]
            if (this.model.get("UsersVoted") != null) {
                listUserVoted = this.model.get("UsersVoted").split(";");
            }
            listUserVote = cleanArray(listUserVote);
            listUserVoted = cleanArray(listUserVoted);
            this.model.set("TimeBeginFormat", setTime(this.model.get("TimeBegin")))
            this.model.set("CountUserVote", listUserVote.length)
            this.model.set("CountUserVoted", listUserVoted.length)

            this.model = options.model;
            this.view = options.view
            this.checkView();

            return this;
        },

        checkView: function () {
            var now = new Date();
            var that = this;
            var timeEndStr = that.model.get("TimeEnd");
            var timeBeginStr = that.model.get("TimeBegin");
            if (timeEndStr) {
                timeEndStr = timeEndStr.match(/\d+/)[0]
            }
            if (timeBeginStr) {
                timeBeginStr = timeBeginStr.match(/\d+/)[0]
            }

            if (that.model.get("IsView") && !that.model.get("IsVote") && that.view == 4) {
                that.view = 3 // nếu chỉ được xem ko được vote thì chuyển sang giao diện xem
                return;
            }

            if (Number(timeEndStr) < now.getTime() || Number(timeBeginStr) > now.getTime()) {
                that.view = 3;// hết hạn hoặc chưa đến hạn trưng cầu thì sẽ cho vào dao diện xem
                return;
            } else {
                //that.view = 4;
            }
        },

        renderData: function () {
            listVoteDetail.reset();
            var that = this;
            egov.request.getVoteDetail({
                data: { voteId: that.model.get("VoteId") },
                success: function (result) {
                    this.model = new VoteModel(result);
                    var listUserVote = result.UsersVote.split(";");
                    listUserVote = cleanArray(listUserVote);
                    var opinion = JSON.parse(this.model.get("ListOpinion"));
                    currentUserId = this.model.get("CurrentUserId")
                    opinion = that.voteDetailCount(opinion, currentUserId, listUserVote.length)
                    opinion["IsMultiSelect"] = that.model.get("IsMultiSelect");
                    for (var i = 0; i < opinion.length; i++) {
                        opinion[i]["Option"] = {
                            IsMultiSelect: that.model.get("IsMultiSelect"),
                            IsPublic: that.model.get("IsPublic"),
                            IsCommentDiff: that.model.get("IsCommentDiff"),
                            IsViewResultImmediately: that.model.get("IsViewResultImmediately"),
                            IsNotify: that.model.get("IsNotify")
                        }
                        var vote = new VoteDetailModel(opinion[i]);
                        listVoteDetail.add(vote);
                    }
                },
                error: function (error) {}
            });
        },

        voteDetailCount: function (opinion, currentUserId, maxVote) {
            for (var i = 0; i < opinion.length; i++) {
                if (opinion[i].UserIdsVote) {
                    var listUser = opinion[i].UserIdsVote.split(";");
                    listUser = cleanArray(listUser);
                    opinion[i]["TotalVote"] = listUser.length;
                    if (listUser.includes(currentUserId.toString())) {
                        opinion[i]["IsChecked"] = true;
                    }
                } else {
                    opinion[i]["TotalVote"] = 0;
                }
                
            }
            //var maxVote = _.max(opinion, function (opi) { return opi.TotalVote; });
            for (var i = 0; i < opinion.length; i++) {
                opinion[i]["MaxVote"] = maxVote;
            }
            return opinion;
        },

        addVote: function (vote) {
            var view = new ReferendumVoteItem({ model: vote, view: this.view });
            this.$(".list-vote-detail").append(view.render().el);
            if (this.view != 4) {
                this.$el.find('.progress .progress-bar').css("width",
                       function () {
                           return $(this).attr("aria-valuenow") + "%";
                       }
               )
            }
        },

        resetVote: function (vote) {
            this.$(".list-vote-detail").html("");
        },

        render: function () {
            var that = this;
            this.$el.html($.tmpl(this.template, that.model.toJSON()));
            var height = 700;
            if (that.view == 4) {
                height = 600;
            }
            if (that.view == 3) {
                if (that.$el.find(".notifyContent").length > 0) {
                    that.$el.find(".notifyContent").hide();
                }
            }
            var buttons = [];
            if (that.model.get("IsView") && that.view == 4) {
                var textTitle = "Gửi kết quả"
                var voted = that.model.get("IsVoted");
                if (voted) {
                    textTitle = "Xem kết quả";
                }
                buttons.push({
                    text: textTitle,
                    className: "btn-success",
                    click: function () {
                        var list = listVoteDetail.where({ IsChecked: true });
                        var voteDetailIds = [];
                        list.forEach(function (model, index) {
                            voteDetailIds.push(model.get("VoteDetailId"))
                        });
                        
                        if (that.model.get("IsVoted")) {
                            that.view = 3;
                            that.$el.closest(".modal-content").find("h4.modal-title").text("Xem kết quả trưng cầu");
                            that._viewResult();
                            return;
                        }
                        egov.request.checkVoteResult({
                            data: { voteId: that.model.get("VoteId"), voteDetailIds: JSON.stringify(voteDetailIds) },
                            success: function (resultData) {
                                if (resultData == true) {
                                    that.view = 3;
                                    that.$el.closest(".modal-content").find("h4.modal-title").text("Xem kết quả trưng cầu");
                                    that._viewResult();
                                } else {
                                    that.$el.find(".errorContent").text(resultData);
                                }
                            },
                            error: function (error) {

                            }
                        });
                    }
                });
            }

            buttons.push({
                text: "Đóng",
                click: function () {
                   // clearInterval(window.intervalVote);
                    that.$el.dialog("hide");
                }
            });
            var btnView = {
                text: "Xem kết quả",
                className: "btn-success",
                click: function () {
                    that.view = 3;
                    that._viewResult()
                }
            }
            var that = this;
            var dialogSetting = {
                width: height,
                height: "auto",
                draggable: true,
                title: "Thực hiện trưng cầu",
                buttons: buttons
            };
            if (that.view == 3) {
                that.$el.find(".oppinionDiff").hide();
            }
            that.$el.dialog(dialogSetting);
            that.renderData();
            if (that.view == 3) {
                that.$el.find("h4.modal-title").text("Xem kết quả trưng cầu");
                //window.intervalVote = setInterval(function () {
                //    that.reloadData();
                //}, 3000)
            }

            return that;
        },

        reloadData: function () {
            var that = this;
            var count = listVoteDetail.length;
            egov.request.getVoteDetailReload({
                data: { voteId: that.model.get("VoteId"), voteDetailCount: count },
                success: function (resultData) {
                    if (resultData.IsDiff) {
                        that.renderData();
                        return;
                    }
                    var result = resultData.List;
                    listVoteDetail.forEach(function (model, index) {
                        for (var i = 0; i < result.length; i++) {
                            if (model.get("VoteDetailId") == result[i].VoteDetailId) {
                                if (model.get("TotalVote") != result[i].TotalVote) {
                                    model.set("TotalVote", result[i].TotalVote);
                                }
                            }
                        }
                    });
                },
                error: function (error) {

                }
            });
            
        },

        commentDiff: function (e) {
            if (!this.model.get("IsCommentDiff")) {
                return false;
            }

            if (e.keyCode == 13 && commentDiff != "") {
                var that = this;
                var commentDiff = that.$el.find(".oppinionDiff").val();
                egov.request.createCommentDiff({
                    data: { voteId: that.model.get("VoteId"), commentDiff: commentDiff },
                    success: function (result) {
                        if (result.error) {
                            $(".errorContent").text(result.content)
                            return;
                        }

                        listVoteDetail.reset();
                        that.renderData();
                        that.$el.find(".oppinionDiff").val("");
                    },
                    error: function (error) {
                        $(".errorContent").text("Lỗi kết nối đến server");
                    }
                });
            }
        },

        _viewResult: function () {
            this.render();
        }
    });

    var ReferendumVoteItem = Backbone.View.extend({
        tagName: "div",

        template: templateItem,

        events: {
            "click .checkbox": "chooseOpinion"
        },

        model: VoteDetailModel,

        initialize: function (options) {
            this.listenTo(this.model, 'add', this.render);
            this.listenTo(this.model, 'change:IsChecked', this.renderAfterChange);
            this.listenTo(this.model, 'change:TotalVote', this.renderAfterChange);
            this.listenTo(this.model, 'change:Percent', this.renderAfterChange);
            this.listenTo(this.model, 'change:UserIdsVote', this.renderAfterChange);
            var view = options.view;
            if (view == 4) {
                this.template = templateOnlyItem;
            }
            if (view == 3) {
                this.template = templateViewOnlyItem;
            }
        },

        chooseOpinion: function (e) {
            e.preventDefault();
            e.stopPropagation();
            var that = this;
            var isCheck = that.model.get("IsChecked");
            if (that.model.get("Option").IsMultiSelect) {
                if (isCheck) {
                    that.model.set("IsChecked", false);
                } else {
                    that.model.set("IsChecked", true);
                }
            } else {
                if (isCheck) {
                    return;
                }
                listVoteDetail.forEach(function (model, index) {
                    if (model.get("IsChecked") == true) {
                        model.set("IsChecked", false);
                    }
                });
                that.model.set("IsChecked", true);
            }
        },

        render: function () {
            var that = this;
            if (that.model.get("MaxVote") == 0) {
                that.model.set("Percent", 0);
            } else {
                var percent = parseInt(parseFloat(that.model.get("TotalVote") / that.model.get("MaxVote")).toFixed(2) * 100);
                that.model.set("Percent", percent);
            }
            that.$el.addClass("row");
            that.$el.html($.tmpl(that.template, that.model.toJSON()));
            that.renderUserInfo();
            
            return that;
        },

        renderAfterChange: function () {
            var width = this.model.get("Percent");
            this.render();
            this.$el.find('.progress .progress-bar').css("width",
                        function () {
                            return width + "%";
                        });
        },

        renderUserInfo: function () {
            var that = this;
            that.$el.find(".btnUserVote").customDropdown({
                css: {
                    width: 300,
                    height: 300
                },
                callback: function () {
                    if (that.model.get("UserIdsVote") != ";;" && that.model.get("UserIdsVote")) {
                        egov.request.getUserInfos({
                            data: {
                                userIds: that.model.get("UserIdsVote")
                            },
                            success: function (result) {
                                var template = '<li><a href="#">${UserName}</a></li>';
                                $("#ListUserVote").html($.tmpl(template, result))
                            },
                            error: function (error) {
                            }
                        });
                    } else {
                        $("#ListUserVote").html("");
                    }
                }
            });
           
        },

        getTotalVote: function (userVotes) {
            var listUser = userVotes.split(";");
            listUser = cleanArray(listUser);
            return listUser.length;
        }
    });

    function cleanArray(actual) {
        var newArray = new Array();
        for (var i = 0; i < actual.length; i++) {
            if (actual[i]) {
                newArray.push(actual[i]);
            }
        }
        return newArray;
    }
    var setTime = function (timeStr) {
        if (timeStr instanceof Date) { } else {
            timeStr = timeStr.match(/\d+/)[0];
        }
        var time = new Date(Number(timeStr));
        var day = "";
        switch (time.getDay()) {
            case 0:
                day = "Chủ nhật";
                break;
            case 1:
                day = "Thứ hai";
                break;
            case 2:
                day = "Thứ ba";
                break;
            case 3:
                day = "Thứ tư";
                break;
            case 4:
                day = "Thứ năm";
                break;
            case 5:
                day = "Thứ sáu";
                break;
            case 6:
                day = "Thứ bảy";
        }
        return day + "," + ("0" + time.getDate()).slice(-2) + "-" + ("0" + Number(time.getMonth() + 1)).slice(-2) + "-" + time.getFullYear() + " " + time.getHours() + ":" + ("0" + time.getMinutes()).slice(-2)
    }
    return ReferendumVote;
});

var VoteModel = Backbone.Model.extend({
    defaults: function () {
        var end = new Date
        end.setHours(23);
        return {
            VoteId: 0,
            TimeBegin: new Date(),
            TimeEnd: end,
            TimeBeginFormat: "",
            VoteDetailId: 1,
            Title: "",
            IsMultiSelect: true,
            IsPublic: true,
            IsCommentDiff: true,
            IsViewResultImmediately: true,
            IsNotify: true,
            DepartmentsView: "",
            UsersView: "",
            DepartmentsVote: "",
            UsersVote: "",
            UsersVoted: "",
            UserIdCreate: 1,
            CurrentUserId: 0,
            UsernameCreate: '',
            CountUserVote:0,
            IsNow: false,
            IsVoted: false,
            IsCreate: false
        };
    },
});

var VoteDetailModel = Backbone.Model.extend({
    defaults: function () {
        return {
            VoteId: 0,
            VoteDetailId: 0,
            UserIdCreate: 0,
            UserIdsVote: "",
            TitleDetail: "",
            IsChecked: false,
            TotalVote: 0,
            Percent: 0,
            MaxVote: 0,
            IsMultiSelect: true,
            Option: {
                IsMultiSelect: true,
                IsPublic: true,
                IsCommentDiff: true,
                IsViewResultImmediately: true,
                IsNotify: true
            },
        };
    },
    toggle: function () {
        this.save({ IsChecked: !(this.get("IsChecked")) });
    },
    setNotCheck: function () {
        this.save({ IsChecked: false });
    }
});

var ListVote = Backbone.Collection.extend({
    model: VoteModel
});

var ListVoteDetail = Backbone.Collection.extend({
    model: VoteDetailModel,

    //setMaxVote: function () {
    //    debugger
    //    var max = this.max(function (player) {
    //        return player.get('TotalVote');
    //    });
    //    this.forEach(function (model, index) {
    //        model.set("MaxVote", max.get("TotalVote"));
    //    });
    //},
    
    comparator: function (item) {
        return item.get('TotalVote');
    },

    remaining: function () {
        return this.where({ IsChecked: false });
    },
});

var listVoteDetail = new ListVoteDetail();

