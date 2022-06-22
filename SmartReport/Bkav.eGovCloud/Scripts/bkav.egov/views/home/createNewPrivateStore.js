define([
    egov.template.tree.createPrivateStore,
    'jstree'
],
 function (Template) {

     "use strict";

     var NewPrivateStoreView = Backbone.View.extend({
         el: '#newPrivateStore',

         events: {
             'click .del-joined': 'deleteJoined'
         },

         initialize: function (option) {
             if ($("body > #newPrivateStore").length == 0) {
                 this.$el.appendTo('body');
             }
             this.$el.empty();
             if ($('#StorePrivateFilterDepartment').length > 0) {
                 $('#StorePrivateFilterDepartment').remove();
             }

             this.template = Template;
             var that = this;
             var dataManager = egov.dataManager;

             dataManager.getAllUsers({
                 success: function (allUsers) {
                     that.allUsers = allUsers;

                     dataManager.getAllDept({
                         success: function (allDepts) {
                             that.allDepts = allDepts;

                             dataManager.getAllUserDeptPosition({
                                 success: function (allUserDeptPosition) {
                                     that.allUserDeptPosition = allUserDeptPosition;

                                     that.render();
                                 }
                             });
                         }
                     });
                 }
             });
         },

         render: function () {
             var that = this;
             var userCreatedId = that.model.get("userCreated") == undefined ? egov.userId : that.model.get("userCreated");
             var userCreated = _.find(that.allUsers, function (u) {
                 return u.value == userCreatedId;
             });

             that.model.set("userCreatedName", userCreated.fullname);

             var modelObj = this.model.toJSON();
             this.$el.html($.tmpl(this.template, modelObj));

             this.$joinedUsers = this.$('#userJoined');
             this.$joinedPanel = this.$('#tblUserJoined');
             this.$joinedPanel.empty();
             this.$name = this.$('#storePrivateName');
             this.$description = this.$('#descStorePrivate');

             that.$('#dgUsers').customDropdown({
                 css: {
                     width: 300,
                     height: 'auto'
                 },
                 target: that.$el
             });

             var rebindUserJoined = function () {
                 that.rebindUserJoined();
             };

             if (!this.loadedJsTree) {
                 _bindJsTree($('#StorePrivateFilterDepartment'), true, true, false,
                                that.allDepts, that.allUsers,
                                that.allUserDeptPosition, null, rebindUserJoined, that);
                 this.loadedJsTree = true;
             }

             that.$joinedUsers.autocomplete({
                 minLength: 1,
                 source: _.reject(that.allUsers, function (user) {
                     return user.username === egov.setting.userName
                 }),
                 focus: function () {
                     return false;
                 },
                 selectFirst: true,
                 select: function (event, ui) {
                     that.$joinedUsers.val('');
                     var userId = ui.item.value;
                     var exist = _.find(userJoined, function (id) {
                         return id === userId;
                     });
                     if (!exist) {

                         // Check node 
                         var userItem = 'li#user_' + userId;
                         var userItems = $('#StorePrivateFilterDepartment ' + userItem);
                         if (typeof userItems !== 'undefined' && userItems.length > 0) {
                             // Nếu tồn tại thì check cho node đó
                             $('#StorePrivateFilterDepartment').jstree("check_node", userItems);
                         }
                     }
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

             $('body').unbind('keydown');
         },

         rebindUserJoined: function () {
             var modelObj = this.model.toJSON();
             if (modelObj.userIdJoined && modelObj.userIdJoined.length > 0) {
                 _.each(modelObj.userIdJoined, function (targetId) {
                     var nodeId = "user_" + targetId;
                     var nodeItems = $('#StorePrivateFilterDepartment #' + nodeId);
                     if (typeof nodeItems !== 'undefined' && nodeItems.length > 0) {
                         // Nếu tồn tại thì check cho node đó
                         $('#StorePrivateFilterDepartment').jstree("check_node", nodeItems);
                     }
                 });
             }

             if (modelObj.deptIdJoined && modelObj.deptIdJoined.length > 0) {
                 _.each(modelObj.deptIdJoined, function (targetId) {
                     var nodeId = targetId;
                     var nodeItems = $('#StorePrivateFilterDepartment #' + nodeId);
                     if (typeof nodeItems !== 'undefined' && nodeItems.length > 0) {
                         // Nếu tồn tại thì check cho node đó
                         $('#StorePrivateFilterDepartment').jstree("check_node", nodeItems);
                     }
                 });
             }
         },

         addJoined: function (data) {
             ///<summary>
             /// Thêm mới người xem sổ hồ sơs
             ///</summary>
             ///<param name="item"> Người được chọn thêm vào có thể xem sổ hồ sơ</param>
             var htmlTmpl = "<tr><td>${label}<a href='#' value=${value} {{if idext}} isDept='true' {{/if}} class='del-joined' style = 'float:right'>Xóa</a></td></tr>";
             this.$joinedPanel.empty();
             this.$joinedPanel.append($.tmpl(htmlTmpl, data.users));
             this.$joinedPanel.append($.tmpl(htmlTmpl, data.depts));
         },

         deleteJoined: function (e) {
             ///<summary>
             /// Xóa người dùng có thể xem sổ hồ sơ
             ///</summary>
             if (!e) {
                 return;
             }

             $(e.target).parents('tr:first').remove();
             var targetId = $(e.target).attr('value');
             var isdept = $(e.target).attr('isdept');

             var nodeId = isdept ? targetId : "user_" + targetId;
             var nodeItems = $('#StorePrivateFilterDepartment #' + nodeId);
             if (typeof nodeItems !== 'undefined' && nodeItems.length > 0) {
                 // Nếu tồn tại thì check cho node đó
                 $('#StorePrivateFilterDepartment').jstree("uncheck_node", nodeItems);
             }
         },

         setFocus: function () {
             ///<summary>
             /// Thiết lập con trỏ chuột focus
             ///</summary>
             var setTextFocus = this.$('input[type=text]').first();
             if (setTextFocus && setTextFocus.length > 0) {
                 var val = setTextFocus.val();
                 setTextFocus.focus();
                 if (val !== null && val !== '') {
                     setTextFocus[0].setSelectionRange(val.length, val.length);
                 }
             }
         },

         serialize: function () {
             ///<summary>
             /// Lấy đối tượng
             ///</summary>
             var userIdJoineds, deptIdJoined, destination;

             if (this.$name.val().trim() === '') {
                 this.$name.addClass('input-validation-error').focus().siblings('span').show();
                 return null;
             } else {
                 this.$name.removeClass('input-validation-error').siblings('span').hide();
             }

             destination = this.getDestination();

             this.model.set({
                 storePrivateName: this.$name.val(),
                 descStorePrivate: this.$description.val(),
                 userIdJoined: _.pluck(destination.users, "value"),
                 deptIdJoined: _.pluck(destination.depts, "value")
             });

             return this.model;
         },

         selectNode: function () {
             var destination = this.getDestination();
             this.addJoined(destination);
         },

         getDestination: function () {
             var result = {
                 users: [],
                 depts: []
             };
             var that = this;

             $("#StorePrivateFilterDepartment").jstree("get_checked", null, false).each(function () {
                 var node = $(this);
                 var nodeId = this.id;

                 // Xác định node đang chọn là node phòng ban hay node user
                 var isDeptNode = node.attr('rel') === 'dept';
                 if (isDeptNode) {
                     var dept = _.find(that.allDepts, function (i) {
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
                     // nodeId = nodeId.replace('user_', '');
                     var user = _.find(that.allUsers, function (i) {
                         return i.value == nodeId.replace('user_', '');
                     });

                     if (user) {
                         ///Kiểm tra xem tồn tai hay chưa,không có thì add
                         if (!_.contains(result.users, user))
                             result.users.push(user);
                     }
                 }
             });

             return result;
         }
     });

     //#region Private
     var itemTreeviewTemplate = '<li id="${value}" label="${attr.label}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
     itemTreeviewTemplate += '<ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
     var itemTreeviewCheckboxTemplate = '<li id="${value}" rel="${attr.rel}" idext="${attr.idext}" class="jstree-${state}"><ins class="jstree-icon">&nbsp;</ins><a href="#" class="">';
     itemTreeviewCheckboxTemplate += '<ins class="jstree-checkbox">&nbsp;</ins><ins class="jstree-icon">&nbsp;</ins>${data}</a></li>';
     var plugins = ["themes", "json_data", "ui", "crrm"];

     var _bindJsTree = function (divTree, hasUser, hasCheckbox,
         hasDnD, arrDept, arrUsers, arrDeptUserJobtitles, callBack, dataBind, that) {
         var deptRoot = _.find(arrDept, function (node) {
             return node.parentid === 0;
         });

         if (deptRoot) {
             var children = _getChildrens(deptRoot.value, hasUser, arrDept, arrUsers, arrDeptUserJobtitles);

             if (hasCheckbox) {
                 plugins.push("checkbox");
             }
             if (hasDnD) {
                 plugins.push("dnd");
             }

             divTree.jstree({
                 "json_data": {
                     "data": [
                         {
                             "data": deptRoot.data.toString(),
                             "metadata": { id: deptRoot.value },
                             "state": "closed",
                             "attr": { "id": deptRoot.value, "rel": "dept", "idext": deptRoot.idext, "label": deptRoot.label },
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

                 egov.callback(dataBind);

             }).on("check_node.jstree", function (evt, data) {
                 that.selectNode();
             });
         }
     };

     var _getChildrens = function (parentid, hasUser,
         arrDept, arrUsers, arrDeptUserJobtitles) {
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
                 $.tmpl(itemTreeviewTemplate, child).appendTo($newChild);
             }
             $newChild.children("li:last").addClass("jstree-last");
         }
     };

     //#endregion

     return NewPrivateStoreView;
 });