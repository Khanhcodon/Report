// Lấy keycode
(function (window) {
    var UserInfo = Backbone.Model.extend({
        defaults: function () {
            return {
                id: 0,
                departmentid: 0,
                jobtitleName: "",
                phone: "",
                birthday: "",
                avatar: "",
                fullname: "",
                username: "",
                isShow: true
            };
        }
    });

    var UserList = Backbone.Collection.extend({
        model: UserInfo,
        remaining: function (deptid) {
            var list = this.where({ departmentid: deptid });
            list = _.sortBy(list, 'order');
            return list;
        },
        count: function (deptid) {
            var users = this.where({ departmentid: deptid, isShow: true });
            return users ? users.length : 0
        }, 
        search: function (key) {
            var pattern = new RegExp(key, "gi");
            _(this.each(function (user) {
                if (key == "") {
                    user.set("isShow", true);
                    return;
                };
                if (pattern.test(user.get("fullname")) || pattern.test(user.get("jobtitleName")) || pattern.test(user.get("username"))) {
                    user.set("isShow", true);
                } else {
                    user.set("isShow", false);
                };
            }));
        },
    });
    var userCollection = new UserList();

    var DepartmentModel = Backbone.Model.extend({
        defaults: function () {
            return {
                value: "",
                parentid: 0,
                data: "",
                idext: "",
                isShow: true
            };
        }
    });
    var DepartmentList = Backbone.Collection.extend({
        model: DepartmentModel,
        remaining: function (id) {
            return this.where({ ParentId: id });
        }
    });
   
    var addressBookMobileView = Backbone.View.extend({
        //$el: "#eGovAddressBookMobile",
        events: {
        },

        initialize: function () {
            var that = this;
            this.inputSearch = $("#searchMobile");
            this.btnBackList = $("#backList");
            ajaxFunc("/AddressBook/GetAllUserInfos", {}, function (allUsers) {
                that.allUsers = allUsers;
                ajaxFunc("/Common/getAllJobTitlies", {}, function (allJobTitles) {
                    that.allJobtitle = allJobTitles;
                    ajaxFunc("/Common/GetAllDepartment", {}, function (allDepts) {
                        that.allDepts = allDepts;
                        ajaxFunc("/Common/GetAllUserDepartmentJobTitlesPosition", {}, function (allUserDeptPosition) {
                            that.allUserDeptPosition = allUserDeptPosition;
                            that.render();
                        })
                    })
                })
            });
            this.inputSearch.on("keyup", function (e) {
                that.search(e)
            })
            this.btnBackList.on("click", function (e) {
                that.backList(e)
            })

        },

        search: function (e) {
                var value = this.inputSearch.val();
                userCollection.search(value);
        },

        render: function (options) {
            var that = this;
            var users = getAllUserDeptJob(that.allUserDeptPosition, that.allUsers, that.allJobtitle);
            userCollection = new UserList(users);
            userCollection.each( function (userCurrent) {
                var view = new userItemView({ model: userCurrent, tagName: "li" });
                $("#listUser").append(view.render().el);
            });
        },

        backList: function (e) {
            $("#detailMobile").css("height", "0px");
            $("#detailMobile").animate({ left: "-500px"}, 10);
        }
    });

    var addressBookDestopView = Backbone.View.extend({
        el: "#eGovAddressBook",
        template: "#treeTemplate",
        events: {
            "keypress #inputSearch": "searchKey",
            "click #btnSearch": "searchClick",
        },

        initialize: function () {
            this.inputSearch = $("#inputSearch");
            $.jstree.defaults.core.dblclick_toggle = true;
            this.render();
        },

        _layout: function () {
          this.$("#container").layout({
                resizable: true,
                closable: false,
                west__size: 260,
                west__minSize: 220,
                west__maxSize: 500,
                west__spacing_open: 1,
                spacing_closed: 2,
                west__resizable: true,
                west__paneSelector: "#left",
                center__paneSelector: "#right"
            });
        },

        render: function () {
            var that = this;
            that._layout()
            ajaxFunc("/AddressBook/GetAllUserInfos", {}, function (allUsers) {
                that.allUsers = allUsers;
                ajaxFunc("/AddressBook/getAllJobTitlies", {}, function (allJobTitles) {
                    that.allJobtitle = allJobTitles;
                    ajaxFunc("/Common/GetAllDepartment", {}, function (allDepts) {
                        that.allDepts = allDepts;
                        ajaxFunc("/Common/GetAllUserDepartmentJobTitlesPosition", {}, function (allUserDeptPosition) {
                            that.allUserDeptPosition = allUserDeptPosition;
                            that.autocompleteUser();
                            that._bind();
                        })
                    })
                })
            })
        },

        renderContentRight: function (allDepts, allUsers) {
            this.departments = new departmentView;
            this.departments.render({ allDept: allDepts, allUser: allUsers });
        },

        _bind: function () {
            var that = this;
            //var users = that.transferDataUser();
            var users = getAllUserDeptJob(that.allUserDeptPosition, that.allUsers, that.allJobtitle);
            that.renderContentRight(that.allDepts, users);

            _bindJsTree(that.$('#tree'), false, false, false,
                                     that.allDepts, [], [], null, []);
        },

        autocompleteUser: function () {
            var that = this;
            var sourceSearch = _.pluck(that.allUsers, "username");
            this.inputSearch.autocomplete({
                minLength: 1,
                source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(sourceSearch, function (value) {
                        value = value.username || value.value || value;
                        return matcher.test(value) || matcher.test(value);
                    }));
                },
                focus: function () {
                    return false;
                },
                selectFirst: true,
                select: function (event, ui) {
                    var userName = ui.item.value;
                    
                    that.inputSearch.val(userName);
                    that.search();
                    return false;
                }
            });
        },

        searchKey: function (e) {
            if (e.keyCode == 13) {
                this.search()
            }
        },

        searchClick: function (e) {
            this.search()
        },

        search: function () {
            var value = this.inputSearch.val();
            userCollection.search(value);
            this.departments.toggleView();
        },

        transferDataUser: function () {
            var that = this;
            var allUserDepts = innerJoin(that.allUsers, that.allUserDeptPosition, function (_ref, _ref2) {
                var uid = _ref.value,
                    name = _ref.label;
                var did = _ref2.departmentid,
                    userid = _ref2.userid,
                    jgid = _ref2.jobtitleid;
                var object = _ref2;
                object["avatar"] = _ref.avatar;
                object["fullname"] = _ref.fullname;
                object["username"] = _ref.username;
                if (uid === userid) {
                    return object;
                }
            })
            var allUserDeptJobs = innerJoin(that.allJobtitle, allUserDepts, function (_ref, _ref2) {
                var jid = _ref.value,
                    name = _ref.label;
                var did = _ref2.departmentid,
                    userid = _ref2.userid,
                    jgid = _ref2.jobtitleid,
                    uname = _ref2.userName;
                var object = _ref2;
                object["jobtitleName"] = _ref.name;
                return jid === jgid && object;
            });

            return allUserDeptJobs;
        }

    });

    var departmentView = Backbone.View.extend({
       // $el: "#contentRight",
        listDept : new DepartmentList(),
        events: {

        },

        initialize: function () {
            this.listenTo(this.listDept, 'add', this.addDepartment);
            this.listenTo(this.listDept, 'reset', this.resetDepartment);
        },

        render: function (options) {
            var that = this;
            userCollection = new UserList(options.allUser);

            _.each(options.allDept, function (dept) {
                that.listDept.add(dept);
            })
        },

        addDepartment: function (dept) {
            var userDepts = userCollection.remaining(dept.get("value"));
            var view = new departmentItemView({ model: dept, users: userDepts });
            $("#addressBookViewer").append(view.render().el);
        },

        toggleView: function () {
            var that = this;
            that.listDept.each(function (dept) {
                var countUser = userCollection.count(dept.get("value"));
                if (countUser > 0) {
                    dept.set("isShow", true);
                } else {
                    dept.set("isShow", false);
                }
            })
        }
    });

    var departmentItemView = Backbone.View.extend({
        tagName: "div",
        template: "#DepartmentTemplate",
        events: {

        },

        initialize: function (options) {
            this.model = options.model;
            var id = "department" + this.model.get("value");
            this.$el.attr("id", id);
            this.userDepts = options.users;
            this.listenTo(this.model, 'change:isShow', this.toggle);
            this.listenTo(this.model, 'destroy', this.remove);
            this.listenTo(this.model, 'add', this.render);
        },

        render: function () {
            var that = this;
            if (this.userDepts.length > 0) {
                this.$el.html($(this.template).tmpl(this.model.toJSON()));
                _.each(this.userDepts, function (userDept) {
                    var view = new userItemView({ model: userDept, tagName: "tr" });
                    that.$el.find(".user-depart-job").append(view.render().el);
                });
                that.renderRecordNumber(true);
            }
            return this;
        },

        toggle: function () {
            var that = this;
            if (that.model.get("isShow")) {
                that.$el.show();
            } else {
                that.$el.hide();
            }
            that.renderRecordNumber();
        },

        renderRecordNumber: function (isInit) {
            var that = this;
            var $records = isInit ? that.$el.find("tr") : that.$el.find("tr:visible");
           
            _.each($records, function ($record, index) {
                $($record).find(".record-number").text(index);
            })
        }
    });

    var userItemView = Backbone.View.extend({
        template: "#UserTemplate",
        events: {
            "click img": "showDetail",
            "click": "showDetail",
        },

        initialize: function (options) {
            this.model = options.model;
            this.template = isMobileDevice() ? "#UserTemplateMobile" : "#UserTemplate";
            if (isMobileDevice()) {
                this.$el.attr("data-group", "friends")
            }
            this.listenTo(this.model, 'destroy', this.remove);
            this.listenTo(this.model, 'change:isShow', this.toggle);
            this.listenTo(this.model, 'add', this.render);
        },

        render: function () {
            
            this.$el.html($(this.template).tmpl(this.model.toJSON()));
            return this;
        },

        showDetail: function () {
            if (isMobileDevice()) {
                $("#detailMobile").css("height", "");
                $("#detailMobile").animate({ left: "0px" },100);
                return;
            }
            $("#formDetail").html($("#DetailTemplate").tmpl(this.model.toJSON()))
            $("#detailModal").modal("show");
        },

        toggle: function () {
            if (this.model.get("isShow")) {
                this.$el.show();
            } else {
                this.$el.hide();
            }
        }
    });

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
                "themes" : {
                                "theme" : "default",
                                "dots" : true,
                                "icons" : false
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
            }).bind('select_node.jstree', function (event, data) {
                destroyEvent(event);

                var target = "#department" + data.rslt.obj.attr("id");

                // perform animated scrolling by getting top-position of target-element and set it as scroll target
                $('html, body').animate({
                    scrollTop: $(target).offset().top
                }, 100, function () {
                    location.hash = target; //attach the hash (#jumptarget) to the pageurl
                });
            }).bind("dblclick.jstree", function (event) {
                destroyEvent(event);
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

    var ajaxFunc = function (url, data, callback) {
        $.ajax({
            url: url,
            data: data,
            beforeSend: function () {
            },
            success: function (result) {
                if (!result) {
                    return;
                }
                callback(result)
            },
            error: function (xhr) { },
            complete: function () { }
        });
    }

    function destroyEvent(e) {
        if (e) {
            if (e.preventDefault) {
                e.preventDefault();
            } else {
                e.returnValue = false;
            }

            if (e.stopPropagation) {
                e.stopPropagation();
            } else {
                e.cancelBubble = true;
            }
        }
    }

    var innerJoin = function (xs, ys, sel) {
        return xs.reduce(function (zs, x) {
            return ys.reduce(function (zs, y) {
                return (// cartesian product - all combinations
                    zs.concat(sel(x, y) || [])
                );
            }, // filter out the rows and columns you want
            zs);
        }, []);
    };
    
    var getAllUserDeptJob = function (allUserDepts, allUser, allJobtitle) {
        var users = _.map(allUserDepts, function (userDept) {
            var user = _.findWhere(allUser, { value: userDept.userid });
            var jobtitle = _.findWhere(allJobtitle, { value: userDept.jobtitleid });
            userDept["avatar"] = user.avatar;
            userDept["phone"] = user.phone;
            userDept["fullname"] = user.fullname;
            userDept["username"] = user.username;
            userDept["jobtitleName"] = jobtitle.label;
            userDept["order"] = jobtitle.order;

            return userDept;
        });

        return users;
    }
    var isMobileDevice = function () {
        var isMobile = $("#eGovAddressBook").css('display') == 'none' ? true : false;

        return isMobile;
    }
    if (isMobileDevice()) {
        new addressBookMobileView;
    } else {
        var address = new addressBookDestopView;
    }
    
})(window);

