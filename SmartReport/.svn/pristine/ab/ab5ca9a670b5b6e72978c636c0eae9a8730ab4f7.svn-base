
(function (btalk) {
    'use strict';

    if (btalk.roster) {
        return;
    }

    btalk.roster = {
        options: {
            CLIENT_TYPE: "egov"
        },
        init: function (_type) {
            this.options.CLIENT_TYPE = _type;
        }
    };

    var RosterModel = Backbone.Model.extend({
        defaults: {
            departments: [],
            messages: [],
            members: [],
            unread: 0,
            status: 'unavailable',
            lastMessage: '', // Tin nhắn gần nhất
            lastMessageTime: null, // thời gian gửi tin nhắn gần nhất
            lastTimeString: '', // thời gian hiển thị gửi tin nhắn gần nhất
            jid: '',
            isRecent: false,
            isGroup: false,
            userid: '',
            fullname: '',
            username: '',
            avatar: '',
        },
        initialize: function () {

        }
    });

    /*
        - Lưu danh sách tất cả các chatter trong hệ thống: bao gồm chat 1 - 1 và chát nhóm
    */
    var BtalkRoster = Backbone.Collection.extend({
        model: RosterModel,
        CLIENT_TYPE: 'egov',
        currentUser: null,
        currentJid: btalk.auth.getJID(),

        initialize: function () {
            this.CLIENT_TYPE === 'egov' ? this._initFromEgov() : this._initFromOther();
        },

        sync: function (rosterNodes) {
            var rosterNodeIds = _.pluck(rosterNodes, 'jid');

            var newRosters = [];

            this.each(function (u) {
                !_.contains(rosterNodeIds, u.get('id')) && newRosters.push(u);
            });

            newRosters.length > 0 && this._syncRosterToServer(newRosters);
        },

        getUserByJID: function (id) {
            return this.detect(function (u) {
                return u.get('id') === id;
            });
        },

        getByUsername: function (username) {
            return this.detect(function (u) {
                return u.get('username') === username;
            });
        },

        getChats: function () {
            return this.filter(function (u) {
                return u.get('type') === btalk.CHATTYPE.CHAT;
            });
        },

        getGroups: function () {
            return this.where({ isGroup: true });
        },

        getChatRecents: function () {
            return this.where({ isRecent: true, isGroup: false });
        },

        getGroupChats: function () {
            var result = this.collect(function (u) { return u.get("jid") });
            return result;
        },

        getOnlines: function () {
            return this.where({ status: 'available' });
        },

        updateOnlines: function (onlineJids) {
            if (!onlineJids || onlineJids.length === 0) return;

            _.each(onlineJids, function (onlinejId) {
                var user = this.getUserByJID(onlinejId);
                user && user.set('status', 'available');
            }.bind(this));
        },

        addGroup: function (groupJid, name) {
            name = name || "";
            var newGroup;

            newGroup = new RosterModel({
                jid: groupJid,
                id: groupJid,
                name: name,
                fullname: name,
                username: groupJid.split('@')[0],
                type: btalk.CHATTYPE.GROUPCHAT,
                isGroup: true,
                avatar: '../AvatarProfile/noavatar_group.png'
            });

            this.add(newGroup);

            return newGroup;
        },

        addMembers: function (groupJid, members) {
            var group = this.getUserByJID(groupJid), member;
            group
                && members && members.length > 0
                && _.each(members, function (jid) {
                    member = this.getUserByJID(jid);
                    member && (group.get('members').push(member));
                }.bind(this));
        },

        changeStatus: function (jid, status) {
            var user = this.getUserByJID(jid);
            user && user.set('status', status);
        },

        _syncRosterToServer: function (newRosters) {
            _.each(newRosters, function (r) {
                btalk.cm.addRosters(r);
            });
        },

        _initFromEgov: function () {
            var usersFromeGov = btalk.egov.options.users;
            var userDeptPoses = btalk.egov.options.userDeptPoses;
            var allDepts = btalk.egov.options.depts;
            var jobtitles = btalk.egov.options.jobtitles;
            var that = this;

            _.each(usersFromeGov, function (egovuser) {
                var username = egovuser.username.indexOf("@") > 0 ? egovuser.username.toLowerCase().split('@')[0] : egovuser.username.toLowerCase();
                var jid = String.format("{0}@{1}", username, egov.setting.user.defaultDomain);

                var newRoster = new RosterModel({
                    id: jid,
                    jid: jid,
                    userid: egovuser.value,
                    fullname: egovuser.fullname,
                    username: egovuser.username,
                    avatar: egovuser.avatar,
                    userStatus: egovuser.status || "",
                    type: btalk.CHATTYPE.CHAT,
                    departments: [],
                });

                _.each(userDeptPoses, function (userDeptPosition) {
                    if (userDeptPosition.userid !== egovuser.value) {
                        return;
                    }

                    var _dept = _.find(allDepts, function (dept) {
                        return dept.value == userDeptPosition.departmentid;
                    });

                    var _jobtitle = _.find(jobtitles, function (jobtitle) {
                        return jobtitle.value == userDeptPosition.jobtitleid;
                    });

                    newRoster.get('departments').push({
                        deptId: _dept ? _dept.value : undefined,
                        deptIdext: _dept ? _dept.idext : undefined,
                        deptName: _dept ? _dept.data : undefined,
                        deptFullName: _dept ? _dept.label : undefined,
                        jobTitlesId: _jobtitle ? _jobtitle.value : undefined,
                        jobTitlesName: _jobtitle ? _jobtitle.label : undefined,
                    });
                });

                this.add(newRoster);

                if (jid === this.currentJid) {
                    this.currentUser = newRoster;
                }

            }.bind(this));
        },

        _initFromOther: function () { }
    });

    btalk.BtalkRoster = BtalkRoster;

})(this.btalk || {});