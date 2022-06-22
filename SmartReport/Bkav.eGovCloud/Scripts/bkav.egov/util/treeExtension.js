
(function () {

    var TreeUtil = {
        bindJsTree: function (divTree, hasUser, hasCheckbox,
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
        }
    };

    egov.utils.treeUtil = TreeUtil;

    var _parseUserItem = function (value, name) {
        var template = '<li class="list-group-item">\
                                <div class="row">\
                                    <label class="checkbox document-color">\
                                       <input name="checkbox[]" value="{0}" type="checkbox" checked="checked">\
                                        <span class="document-color-1"><i class="icon-check"></i></span>\
                                    </label>\
                                    <span style="margin-left: 15px;">{1}</span>\
                                </div>\
                            </li>';
        return $(String.format(template, value, name));
    };

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

    var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" hasReceiveDocument="${attr.hasReceiveDocument}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewCheckboxTemplate += '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var plugins = ["themes", "json_data", "ui", "crrm"];

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
})(egov || {});