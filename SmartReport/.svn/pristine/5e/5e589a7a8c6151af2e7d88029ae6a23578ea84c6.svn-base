(function (egov, $, _, undefined) {
    //if (typeof ($) === 'undefined') {
    //    throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    //}
    //if (typeof (_) === 'undefined') {
    //    throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    //}
    var strips =
            [
                "áàảãạăắằẳẵặâấầẩẫậ",
                "ÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬ",
                "đ",
                "Đ",
                "éèẻẽẹêếềểễệ",
                "ÉÈẺẼẸÊẾỀỂỄỆ",
                "íìỉĩị",
                "ÍÌỈĨỊ",
                "óòỏõọơớờởỡợôốồổỗộ",
                "ÓÒỎÕỌƠỚỜỞỠỢÔỐỒỔỖỘ",
                "ưứừửữựúùủũụ",
                "ƯỨỪỬỮỰÚÙỦŨỤ",
                "ýỳỷỹỵ",
                "ÝỲỶỸỴ"
            ];

    var replacements =
    [
        'a',
        'A',
        'd',
        'D',
        'e',
        'E',
        'i',
        'I',
        'o',
        'O',
        'u',
        'U',
        'y',
        'Y'
    ];

    var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
    itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}">'
                                       + '<ins class="jstree-icon">&nbsp;</ins><a href="#" class="">'
                                       + '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var itemTreeviewCheckboxTemplate2 = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state} jstree-unchecked">'
                                      + '<ins class="jstree-icon">&nbsp;</ins><a href="#" class="">'
                                      + '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
    var plugins = ["themes", "json_data", "ui", "crrm"];

    egov.utilities = {};

    egov.utilities.checkbox = {};

    egov.utilities.checkbox.checkAndUnCheckAll = function (allitems, item, callback) {
        var count = item.length;
        //Check all items
        allitems.bind("click", function () {
            var checkedAll = this.checked;
            for (var i = 0; i < count; i++) {
                item[i].checked = checkedAll;
                $(item[i]).trigger('change');
            }
            if (callback && typeof callback === 'function') {
                callback();
            }
        });
        //Item check
        item.bind("click", function () {
            var countCheck = 0;
            var countChecked = 0;
            item.each(function () {
                countCheck++;
                if (this.checked == true) {
                    countChecked++;
                }
                //   $(this).trigger('change');
            });
            var checked = countCheck == countChecked ? true : false;
            allitems.prop("checked", checked);

            if (callback && typeof callback === 'function') {
                callback();
            }
        });
    };

    //Các hàm liên quan đến treeview - jstree
    egov.utilities.jstree = {};

    egov.utilities.jstree.getChildrens = function (parentid, hasUser, arrDept, arrUsers, arrDeptUserJobtitles) {
        var children = _.filter(arrDept, function (num) { return num.parentid == parentid; });
        var deptUsers = _.filter(arrDeptUserJobtitles, function (dept) {
            return dept.departmentid === parentid;
        });
        if (children.length > 0) {
            for (var j = 0; j < children.length; j++) {
                if (egov.utilities.jstree.getChildrens(children[j].value, false, arrDept, [], []).length > 0 || (hasUser && deptUsers.length > 0)) {
                    children[j].state = "closed";
                }
            }
        }

        if (hasUser) {
            if (deptUsers.length > 0) {
                for (var i = 0; i < deptUsers.length; i++) {
                    var userindept = _.find(arrUsers, function (user) {
                        return user.value === deptUsers[i].userid;
                    });
                    if (userindept) {
                        var selected = { "value": "user_" + userindept.value, "data": userindept.fullname, "parentid": parentid, "state": "leaf", "metadata": { "id": "user_" + userindept.value }, "attr": { "id": "user_" + userindept.value, "rel": "people", "idext": deptUsers[i].idext } };
                        children.push(selected);
                    }
                }
            }
        }
        return children;
    };

    egov.utilities.jstree.bindJsTree = function (divTree, hasUser, hasCheckbox, hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind, hasCheckParent, hasCheckChildren) {
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
            divTree.jstree({
                "json_data": {
                    "data": [
                        {
                            "data": deptRoot.data.toString(),
                            "metadata": { id: deptRoot.value },
                            "state": "closed",
                            "attr": { "id": deptRoot.value, "rel": "dept", "idext": deptRoot.idext, "label": deptRoot.label },
                            "children": egov.utilities.jstree.getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles)
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
                        egov.utilities.jstree.appendChild(data.rslt.obj, parseInt(data.rslt.obj.attr("id")), hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles, hasCheckParent, hasCheckChildren);
                    }
                });

                if (typeof callBack === 'function') {
                    callBack(dataBind);
                }
            });
        }
    };

    egov.utilities.jstree.appendChild = function ($parent, parentId, hasUser, hasCheckbox, arrDept, arrUsers, arrDeptUserJobtitles, hasCheckParent, hasCheckChildren) {
        if (typeof hasCheckParent == undefined)
            hasCheckParent = true;
        if (typeof hasCheckChildren == undefined)
            hasCheckChildren = true;
        var child = egov.utilities.jstree.getChildrens(parentId, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);
        if (child.length > 0) {
            var $newChild = $('<ul></ul>');
            $newChild.appendTo($parent);
            if (hasCheckbox) {
                var tem = hasCheckChildren ? itemTreeviewCheckboxTemplate : itemTreeviewCheckboxTemplate2;
                $.template("checkboxTemplate", tem);
                $.tmpl("checkboxTemplate", child).appendTo($newChild);
                if (hasCheckParent) {
                    $($parent).find("li").each(function (idx, listItem) {
                        $(listItem).addClass($parent.hasClass("jstree-checked") ? "jstree-checked" : "jstree-unchecked");
                    });
                }
            } else {
                $.template("itemTreeviewTemplate", itemTreeviewTemplate);
                $.tmpl("itemTreeviewTemplate", child).appendTo($newChild);
            }
            $newChild.children("li:last").addClass("jstree-last");
        }
    };

    // Các hàm xử lý chuỗi
    egov.utilities.string = {};

    egov.utilities.string.stripVietnameseChars = function (input) {
        var stringBuilder = [];
        for (var k = 0; k < input.length; k++) {
            stringBuilder.push(input.charAt(k));
        }
        for (var i = 0; i < stringBuilder.length; i++) {
            for (var j = 0; j < strips.length; j++) {
                if (strips[j].indexOf(stringBuilder[i]) > -1) {
                    stringBuilder[i] = replacements[j];
                }
            }
        }
        return stringBuilder.join("");
    };

    egov.utilities.url = {};
    egov.utilities.url.getQueryStringValue = function (name, url) {
        if (!url) {
            url = location.search;
        }
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(url);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };
    egov.utilities.array = {};

    egov.utilities.array.distinct = function (array) {
        var arrayWithUniqueValues = [];
        var objectCounter = {};
        for (var i = 0; i < array.length; i++) {
            var currentMemboerOfArrayKey = JSON.stringify(array[i]);
            var currentMemboerOfArrayValue = array[i];
            if (objectCounter[currentMemboerOfArrayKey] === undefined) {
                arrayWithUniqueValues.push(currentMemboerOfArrayValue);
                objectCounter[currentMemboerOfArrayKey] = 1;
            } else {
                objectCounter[currentMemboerOfArrayKey]++;
            }
        }
        return arrayWithUniqueValues;
    };

    //egov.utilities.resource = {};
    //egov.utilities.resource.getResource = function (resourceKey) {
    //    /// <summary>
    //    /// Trả về ResourceValue theo Key, nếu không tồn tại, trả về ResourceKey
    //    /// </summary>
    //    /// <param name="resourceKey"></param>
    //    try {
    //        return eval(resourceKey);
    //    } catch (e) {
    //        return resourceKey;
    //    }
    //};

    //egov.utilities.resource.getResourceWithEnum = function (wrapperElement) {
    //    require(['staticResource'], function () {
    //        egov.staticResource.getStaticElement(wrapperElement);
    //    });
    //}

})(window.egov = window.egov || {}, window.jQuery, window._);

//(function ($) {
//    $.fn.serializeObject = function () {
//        var o = {};
//        var a = this.serializeArray();
//        $.each(a, function () {
//            if (o[this.name]) {
//                if (!o[this.name].push) {
//                    o[this.name] = [o[this.name]];
//                }
//                o[this.name].push(this.value || '');
//            } else {
//                o[this.name] = this.value || '';
//            }
//        });
//        return o;
//    };
//})(window.jQuery);
