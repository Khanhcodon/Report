(function (tree, _, $) {
    tree.allUsers = [];
    tree.allJobtitle = [];
    tree.allDepts = [];
    tree.allUserDeptPosition = [];
    tree.allUserDepts = [];
    tree.selector = '#jstreeUserDepart';

    tree.initialize = function (checked) {
        var arrChecked=[]
        if (checked) {
            arrChecked = checked.split(",")
        } 
        if (!egov.locache) {
            return;
        }

        var that = this;
        var locacheData = egov.locache;
        this.allUsers = locacheData.get("allUsers");
        this.allJobtitle = locacheData.get("allJobtitle");
        this.allDepts = locacheData.get("allDept");
        this.allUserDeptPosition = locacheData.get("allUserDeptPosition");

        var allDepartment = [];
        var userDeparts = [];

        for (var i = 0; i < that.allDepts.length; i++) {
            var parentid = "#";
            var opened = true;
            
            if (that.allDepts[i].parentid != 0) {
                parentid = that.allDepts[i].parentid;
                opened = false;
            }
            allDepartment.push({ "id": that.allDepts[i].attr.id, "parent": parentid, "text": that.allDepts[i].data, "icon": "/Content/Images/dept.png", "state": { opened: opened} });
        }

        for (var i = 0; i < that.allUserDeptPosition.length; i++) {
            var userDepart = _.filter(that.allUsers, function (user) {
                return user.value == that.allUserDeptPosition[i].userid;
            });

            var check = false;
            var userCheck = _.filter(arrChecked, function (user) {
                return user == userDepart[0].username
            });
            if (userCheck.length>0) {
                check = true;
            }

            that.allUserDepts.push({ userid: userDepart[0].value, username: userDepart[0].username, departmentid: that.allUserDeptPosition[i].departmentid });
            allDepartment.push({ "id": i + "," + userDepart[0].username, "parent": that.allUserDeptPosition[i].departmentid, "text": userDepart[0].fullname, "icon": "/Content/Images/People.png", "state": { selected: check} });
        }

        this._bind(allDepartment)
    };

    tree._bind = function (allDepartment, checked) {
        $(this.selector).jstree("destroy").empty();

        $(this.selector).jstree({
            'core': {
                'data': allDepartment
            },
            "checkbox": {
                "keep_selected_style": false
            },
            "plugins": ["checkbox"]
        });
    };

    tree.getAccountChecked = function () {
        var userids = $(this.selector).jstree('get_selected');
        var strUser = [];
        for (var i = 0; i < userids.length; i++) {
            var user = userids[i].split(",");
            if (user.length == 2) {
                strUser.push(user[1]);
            }
        }

        return _.uniq(strUser).toString();
    };

    tree.getTreeIds = function () {
        var userids = $("#jstreeUserDepart").jstree('get_selected');
        var users = [];
        for (var i = 0; i < userids.length; i++) {
            users.push(userids[i]);
        }

        return users;
    };
})
(this.treeUser = this.treeUser || {}, window._, window.jQuery);