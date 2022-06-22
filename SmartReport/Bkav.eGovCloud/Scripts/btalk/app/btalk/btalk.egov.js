// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function ($, btalk, _) {
    'use strict';

    if (btalk.egov) {
        return;
    }

    btalk.egov = {
        options: {
            url: "",
            userDeptUrl: "",
            allUsersUrl: "",
            allDeptUrl: "",
            allJobTitlesUrl: "",
            allPositionsUrl: "",

            // Cache json lay tu eGov
            users: egov.setting.allUsers,
            depts: egov.setting.allDeps,
            userDeptPoses: egov.setting.allUserDeps,
            jobtitles: egov.setting.allJobtitles,
            poses: egov.setting.allPositions,
            acs: []
        },

        init: function (options) {
            this.options = $.extend(true, {}, this.options, options);
        },

        getUserDeptTree: function (options) {
            if (this.options.users.length > 0
                && this.options.depts.length > 0
                && this.options.userDeptPoses.length > 0
                && this.options.jobtitles.length > 0
                && this.options.poses.length > 0) {
                if (typeof this.options.success == 'function') {
                    this.options.success({
                        users: this.options.users,
                        depts: this.options.depts,
                        userDeptPoses: this.options.userDeptPoses,
                        jobtitles: this.options.jobtitles,
                        poses: this.options.poses
                    });
                }
                return;
            }

            this.options = $.extend({}, this.options, options);
            // TODO: Sửa về dùng when.then.then... https://datgs.wordpress.com/2011/07/19/ma-thuat-voi-jquery-defferreds/
            //$.when(
            //    this.getAllUsers.bind(this),
            //    this.getAllDept.bind(this),
            //    this.getAllUserDeptPosition.bind(this),
            //    this.getAllJobTitles.bind(this),
            //    this.getAllPositions.bind(this))
            //.then(function () {
            //    var newDept = [];
            //    for (var i = 0; i < this.depts.length; i++) {
            //        newDept.push({
            //            'id': this.depts[i].value,
            //            'parentid': this.depts[i].parentid, // ? "#" : depts[i].parentid,
            //            'text': this.depts[i].data.toString(),
            //            'idext': this.depts[i].idext,
            //            'icon': "",
            //            'order': this.depts[i].order,
            //            'level': this.depts[i].level,
            //            'label': this.depts[i].label,
            //            'state': {
            //                "opened": false
            //            },
            //            'a_attr': {
            //                'rel': "dept",
            //                'idext': this.depts[i].idext,
            //                'label': this.depts[i].label
            //            },
            //            'li_attr': {
            //                'id': this.depts[i].value
            //            },
            //            'children': ['_']
            //        });
            //    }
            //    if (typeof this.options.success == 'function') {
            //        this.options.success({
            //            users: this.users,
            //            depts: newDept,
            //            userDeptPoses: this.userDeptPoses,
            //            jobtitles: this.jobtitles,
            //            poses: this.poses
            //        });
            //    }
            //}.bind(this));

            this.getAllUsers({
                success: function (users) {
                    this.getAllDept({
                        success: function (depts) {
                            this.getAllUserDeptPosition({
                                success: function (userDeptPoses) {
                                    this.getAllJobTitles({
                                        success: function (jobtitles) {
                                            this.getAllPositions({
                                                success: function (poses) {
                                                    var newDept = [];
                                                    for (var i = 0; i < depts.length; i++) {
                                                        newDept.push({
                                                            'id': depts[i].value,
                                                            'parentid': depts[i].parentid, // ? "#" : depts[i].parentid,
                                                            'text': depts[i].data.toString(),
                                                            'idext': depts[i].idext,
                                                            'icon': "",
                                                            'order': depts[i].order,
                                                            'level': depts[i].level,
                                                            'label': depts[i].label,
                                                            'state': {
                                                                "opened": false
                                                            },
                                                            'a_attr': {
                                                                'rel': "dept",
                                                                'idext': depts[i].idext,
                                                                'label': depts[i].label
                                                            },
                                                            'li_attr': {
                                                                'id': depts[i].value
                                                            },
                                                            'children': ['_']
                                                        });
                                                    }
                                                    if (typeof this.options.success == 'function') {
                                                        this.options.success({
                                                            users: users,
                                                            depts: newDept,
                                                            userDeptPoses: userDeptPoses,
                                                            jobtitles: jobtitles,
                                                            poses: poses
                                                        });
                                                    }
                                                    /*
                                                     * TamDN - 23/12/2016
                                                     * Sap xep lai roster theo phong ban
                                                     */
                                                    btalk.ROSTER.sort(btalk.ROSTER.users);
                                                }.bind(this)
                                            });
                                        }.bind(this)
                                    });
                                }.bind(this)
                            });
                        }.bind(this)
                    });
                }.bind(this)
            });
        },

        getAllUsers: function (callback) {
            if (this.options.users.length > 0) {
                return this.options.users;
            }

            if (this.options.allUsersUr == "") {
                console.log("Chua cau hinh url lay toan bo user egov!");
                return [];
            }

            $.ajax({
                url: this.options.allUsersUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.users = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllDept: function (callback) {
            if (this.options.depts.length > 0) {
                return this.options.depts;
            }

            $.ajax({
                url: this.options.allDeptUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.depts = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllUserDeptPosition: function (callback) {
            if (this.options.userDeptPoses.length > 0) {
                return this.options.userDeptPoses;
            }
            if (this.options.userDeptUrl == "") {
                console.log("Chua cau hinh url lay danh sach user thuoc phong ban egov!");
                return [];
            }
            $.ajax({
                url: this.options.userDeptUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.userDeptPoses = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllJobTitles: function (callback) {
            if (this.options.jobtitles.length > 0) {
                return this.options.jobtitles;
            }
            if (this.options.allJobTitlesUrl == "") {
                console.log("Chua cau hinh url lay toan bo chuc vu egov!");
                return [];
            }
            $.ajax({
                url: this.options.allJobTitlesUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.jobtitles = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getAllPositions: function (callback) {
            if (this.options.poses.length > 0) {
                return this.options.poses;
            }
            if (this.options.allPositionsUrl == "") {
                console.log("Chua cau hinh url lay toan bo chuc danh egov!");
                return [];
            }
            $.ajax({
                url: this.options.allPositionsUrl,
                type: "GET",
                success: function (data) {
                    btalk.egov.options.poses = data;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(data);
                    }
                }
            });
        },

        getACSInfo: function (callback) {
            if (this.options.acs.length > 0) {
                return this.options.acs;
            }
            if (this.options.acsUrl == "") {
                console.log("Chua cau hinh url lay thong tin nghi phep tu acs!");
                return [];
            }

            var d = new Date().toISOString();
            $.ajax({
                type: "POST",
                url: this.options.acsUrl,
                contentType: "application/x-www-form-urlencoded",
                data: { day: d },
                dataType: "xml",
                success: function (data) {
                    // array [{FinishDate: "1/18/2016 11:45:00 PM",
                    // Reason: "",
                    // RegionName: "",
                    // RegulationId: "101",
                    // RegulationName: "Không phải quẹt thẻ",
                    // StartDate: "1/18/2016 8:00:00 AM",
                    // UserName: "Andy"}, ...]
                    var result = [];
                    try {
                        result = JSON.parse($(data.children[0]).text());
                    } catch (err) {
                        console.log("btalk.egov.getACSInfo: loi parse thong tin nghi phep!")
                        result = [];
                    }

                    btalk.egov.options.acs = result;
                    if (callback && typeof callback.success == 'function') {
                        callback.success(result);
                    }
                }.bind(this),
                error: function (e) {
                    console.log("btalk.egov.getACSInfo: loi service lay thong tin nghi phep!")
                },
                async: false
            });
            return btalk.egov.options.acs;
        }
    };
})(window.jQuery, window.btalk = window.btalk || {}, window._);