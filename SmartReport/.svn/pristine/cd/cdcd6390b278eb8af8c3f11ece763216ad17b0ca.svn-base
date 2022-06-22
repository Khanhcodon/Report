/* 
   Phuc vu cho viec: tao nhom chat, them thanh vien vao nhom chat.
   1. tao nhom thi chon vao tao ra list usse goi ham tao nhom ben view.app
   2. them thanh vien thi can goi viewChatter info.
*/
// Read it:
// https://www.devbridge.com/articles/understanding-amd-requirejs/

(function (btalk, viewFriendT) {
    'use strict';

    if (btalk.formGroup) {
        return;
    }

    // View hien thi thanh vien trong dialog khi them thanh vien vao nhom.
    var FriendsView = Backbone.View.extend({
        tagName: "li",
        template: viewFriendT,
        events: {
        },

        initialize: function () {
            this.listenTo(this.model, 'destroy', this.clear);
        },

        render: function () {
            this.$el.html($.tmpl(this.template, this.model.toJSON()));
            this.$el = this.$el.children();
            this.setElement(this.$el);
            return this;
        },

        clear: function () {
            this.remove();
        }
    });

    /* change detail view chat group, addtional click for change chat group name */
    var ViewFormGroup = Backbone.View.extend({
        collectionSuggestFriend: null,  // bien danh sach goi y thanh vien co the them cao nhom        
        selectedMembers: [],        // bien luu danh sach thanh vien da chon de them vao nhom.   
        jidChatter: '',
        el: "#modalGroup",

        initialize: function () {
            this.collectionSuggestFriend = new btalk.model.FriendCollection();
            this.listenTo(this.collectionSuggestFriend, "add", this.showFriends);
        },

        events: {
            'click #btnGroupCancel': 'invisibleForm',
            'click #btnGroupActive': 'handleActive',

            'click .dialog-addpeople-header .list-member-chooser': 'focusInputForm',
            'click .dialog-addpeople-body .list-people li.item-list': 'selectedUser',

            'keyup #inputSearch': 'renderUserSuggest',
            'keydown .dialog-addpeople .dialog-addpeople-header .list-member-chooser input': 'removeUserSelected',
        },

        refreshForm: function () {
            this.$el.find('.list-member-chooser')
                .find('.account-selected')
                .not('#inputSearch')
                .remove();
            $('#inputSearch').val('');            
        },

        invisibleForm: function () {
            $('#modalGroup').modal('hide');
            this.refreshForm();            
        },

        visibleForm: function () {
            this.refreshForm();
            var modal = $('#modalGroup');
            var that = this;
            modal.modal('show');
            this.renderUserSuggest();
            modal.on('shown.bs.modal', function () {
                $('#inputSearch').focus().attr("placeholder", "Tìm kiếm bạn bè");
            });
            modal.on('hidden.bs.modal', function () {
                that.$el.find('.list-member-chooser').find('.account-selected').not('#inputSearch').remove();
                $('#inputSearch').val('');
                that.$el.unbind();
            });
        },

        renderUserSuggest: function (event) {
            var key = "";
            //TH la text truyen vao
            if (typeof event != "undefined") {
                key = event;
            }
            // TH la su kien xoa khi con chua.
            if (event && event.type == "keyup") {
                key = $('.dialog-addpeople .dialog-addpeople-header .list-member-chooser input').val();
            }
            var rosters = btalk.ROSTER.search(key);
            this.collectionSuggestFriend.reset(null);
            // lam moi giao dien cho nay.
            $('.dialog-addpeople .dialog-addpeople-body .list-people ul').empty();

            var collectionMemberInGroup = [];
            if (this.jidChatter != '') {
                collectionMemberInGroup = btalk.ROSTER.getUserByJID(this.jidChatter).members;
            }
            for (var i = 0; i < rosters.length; i++) {
                var chatter = btalk.CHATTERS.findWhere({ jid: rosters[i].jid });
                var newchatter = new btalk.model.ChatterSearch({
                    label: "",
                    type: "chat",
                    jid: rosters[i].jid,
                    account: rosters[i].name,
                    online: rosters[i].status == 'available',
                    messagecount: 0,
                    chatter: chatter,
                    rosterUser: rosters[i],
                    linkavatar: btalk.view.getAvatar([rosters[i].name]),
                    ismember: false
                });

                if (collectionMemberInGroup.indexOf(rosters[i].jid) > -1) {
                    newchatter.set('ismember', true);
                }
                // Khong xuat hien cac nhom va thanh vien da chon.
                if (newchatter.get('jid').indexOf("@conference") < 0 &&
                    this.selectedMembers.indexOf(rosters[i].name) < 0) {
                    this.collectionSuggestFriend.add(newchatter);
                }
            }
        },

        focusInputForm: function () {
            $(this.$el.find('#inputSearch')).val('').focus().attr("placeholder", "");
        },

        selectedUser: function (e) {            
            e.preventDefault();
            var li = $(e.target).parents('li.item-list');
            var icon_chooser = li.find('span.member-chooser');
            var selected = li.find('span.member-in-group');
            var data_value = selected.attr('data-value');
            var txt_area = $(".list-member-chooser");
            var valueAccount = li.find('span.name-member').attr('data');
            if (valueAccount != "") {
                if (this.selectedMembers.indexOf(valueAccount) == -1 && selected.html() == "") {
                    this.selectedMembers[this.selectedMembers.length] = valueAccount;
                    icon_chooser.show();

                    var _nameaccount;
                    if (valueAccount.indexOf(document.domain) > -1) {
                        _nameaccount = valueAccount.substring(0, valueAccount.indexOf('@'));
                    } else {
                        _nameaccount = valueAccount;
                    }
                    txt_area.find('input.text-input').before("<span class='account-selected' data='" + valueAccount + "'>" + _nameaccount + "</span>");
                    selected.attr('data-value', 'in-group');
                } else {
                    this.deleteUserSelected(valueAccount);
                    icon_chooser.hide();
                    selected.attr('data-value', '');
                    txt_area.find('span.account-selected[data="' + valueAccount + '"]').remove();
                }
                // Co thay doi  height cho dialog
                this.autoChangeHeightDialog();
                this.focusInputForm();
                e.stopPropagation();
            }
        },

        // Lang nghe su kien phim backspace tren phan input, xoa cai con lai.
        removeUserSelected: function (event) {
            var input = $('.dialog-addpeople .dialog-addpeople-header .list-member-chooser input');
            var textContent = input.val();
            var KeyID = event.keyCode;
            if (KeyID == 8 && textContent == "") {
                //Xoa account đã chọn
                var prev = $(this.$el.find('#inputSearch')).prev('.account-selected');
                var prev_Account = prev.attr('data');
                if (!prev_Account) {
                    return;
                }
                this.deleteUserSelected(prev_Account);
                var icon_chooser = $(this.$el.find('.dialog-addpeople .dialog-addpeople-body .list-people .item-list span.member-chooser[data-account="' + prev_Account + '"]')[0]);
                icon_chooser.hide();
                prev.remove();
                this.autoChangeHeightDialog();
            } else {
                //Load lại dữ liệu
                this.renderUserSuggest(textContent);
            }
            event.stopPropagation();
        },

        // Xoa account trong danh sach member da chon.
        deleteUserSelected: function (account) {
            var index_acc = this.selectedMembers.indexOf(account);
            if (index_acc != -1) {
                this.selectedMembers.splice(index_acc, 1);
            }
        },

        showFriends: function (roster) {
            var item = new FriendsView({ model: roster });
            $('.dialog-addpeople .dialog-addpeople-body .list-people ul').append(item.render().el);
        },

        autoChangeHeightDialog: function () {
            var dialog = $('.dialog-addpeople');
            var dialog_head = dialog.find('.dialog-addpeople-header');
            var dialog_body = $('.dialog-addpeople-body');
            var height_head = dialog_head.height();
            var height_body = dialog_body.height();
            var height_dialog = dialog.height();
            height_dialog = height_body + height_head + 30;
            dialog.height(height_dialog);
        },

        handleActive: function () {
            // neu gia tri jidChatter khong co thi coi nhu tao moi cuoc hoi thoai( tao nhom chat, tao chat 1-1)           ;
            // nguoc lai thi them nguoi vao nhom chat.
            if (this.jidChatter === "") {
                this.selectedMembers.push(btalk.auth.getJID());
                console.log(this.selectedMembers);
                this.createGrouChat(this.selectedMembers);
            } else {
                this.addUserToGroup(this.selectedMembers);
            }
            this.invisibleForm();
        },

        createGrouChat: function (members) {
            if (members.length < 1) {
                btalk.APPVIEW.showNotifyFormText("Không có thành viên nào!");
                return false;
            }
            var group_jid = btalk.cm.getGroupJID();
            var rosterUser = btalk.ROSTER.addUser(group_jid);
            var newchatter = new btalk.model.Chatter({
                jid: group_jid,
                account: group_jid.split("@")[0],
                type: btalk.CHATTYPE.GROUPCHAT,
                lasttimestamp: new Date(),
                lastmessage: '',
                isme: false,
                unread: false,
                unreadcount: 0,
                isselected: false,
                fullname: 'New Group Chat',
                rosterUser: rosterUser,
                roster: btalk.ROSTER,
                isResultOfSearch: true,
                statusHeightChatter: btalk.config.statuschatter,
            });
            rosterUser.members = members;
            rosterUser.chatterM = newchatter;
            btalk.CHATTERS.add(newchatter);
            newchatter.set({ isselected: true, unread: true });
            btalk.cm.createGroup(group_jid, members);
        },

        addUserToGroup: function (selectedMembers) {
            if (selectedMembers.length == 0) return;

            for (var i = 0; i < selectedMembers.length; i++) {
                var item = this.selectedMembers[i].toLowerCase();
                if (item.indexOf('@') < 0) {
                    item = item + "@" + document.domain;
                }
                selectedMembers[i] = item;
            }
            btalk.cm.addGroupMembers(this.jidChatter, selectedMembers);

            var newMemberList = "";
            for (var i = 0; i < selectedMembers.length; i++) {
                if (i > 0)
                    newMemberList += "; ";
                newMemberList += selectedMembers[i];
            }

            /*
             * TamDN - 13/12/2016
             * Gui goi tin messgae thong bao them thanh vien vao nhom
             */
            var configs = [{ "key": "edit_by", "value": btalk.ROSTER.currentuser.jid },
                           { "key": "new_members", "value": newMemberList },
                           { "key": "room_name", "value": btalk.ROSTER.getUserByJID(this.jidChatter).fullname }];
            btalk.cm.sendConfigs(configs, this.jidChatter, btalk.CHATTYPE.GROUPCHAT,
                    btalk.APPVIEW.printAndSaveMessage.bind(btalk.APPVIEW, btalk.APPVIEW.CURRENTCHATTER));
        },
    });

    btalk.view = $.extend(btalk.view, {
        ViewFormGroup: ViewFormGroup,
        //ChatterNextView: ChatterNextView
    });
})(btalk);