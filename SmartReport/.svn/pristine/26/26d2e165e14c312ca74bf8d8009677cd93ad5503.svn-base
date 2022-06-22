

define([egov.template.contact.listItem], function (contactTmp) {

    var DepartmentList = Backbone.View.extend({
        el: '#user-department-list',

        filterDeptIdExt: undefined,

        events: {
            'click .mdl-chip': '_switchOnlineStatus',
            'click #department-list li': '_filterByDepartment',
            'click .removeFilter': '_removeFilter',
            'click .btnChatWith': '_openChatter'
        },

        initialize: function (option) {
            this.registerHandlers();
            this.render();
        },

        render: function () {
            this._bindAllUserCount();
            this._bindContacts(true);
            this._bindDepartments();

            btalk.cm.getOnline();

            this._fixLayout();
        },

        _fixLayout: function () {
            this.$el.height(egov.screenSize.contentHWithoutFooter - 48); // 48 height cuar tab
        },

        registerHandlers: function () {
            this.model.on('change status', this.handleUsersOnline.bind(this));
        },

        //#region Handler

        handleUsersOnline: function () {
            var onlines = this.model.getOnlines().length;

            this.$(".count-online").text(onlines);
            if (this.isOnlineUserBinding) {
                this._bindContacts(true);
                return;
            }
        },

        handleStatusIDLE: function (presence) {
            console.log(presence);
        },

        //#endregion

        //#region Bind Contacts

        _bindAllUserCount: function () {
            this.$(".count-all").text(this.model.length);
        },

        _bindDepartments: function () {
            this.$('#department-list').html($.tmpl('<li class="mdl-menu__item" data-id="${idext}">${data}</li>', btalk.egov.options.depts));

            egov.mobile.upgradeMaterial(this.$(".mdl-menu"));
        },

        _bindContacts: function (isOnline) {
            this.isOnlineUserBinding = isOnline;
            var contacts = isOnline ? this.model.getOnlines() : this.model.getChats();
            var model = _.map(contacts, function (c) {
                return c.toJSON();
            });
            this.$('.contacts').html($.tmpl(contactTmp, model));
        },

        //#endregion

        //#region Events

        _switchOnlineStatus: function (e) {
            var target = $(e.target).closest('.mdl-chip');
            if (target.is(".is-focused")) {
                return;
            }

            var isOnline = target.attr("data-online") === 'on';

            this.$(".mdl-chip").toggleClass("is-focused");
            this._bindContacts(isOnline);
            this._renderFilterred();
        },

        _filterByDepartment: function (e) {
            var target = $(e.target).closest("li");
            this.filterDeptIdExt = target.attr("data-id");
            this.filterName = target.text();

            this._renderFilterred();
        },

        _removeFilter: function () {
            this.filterDeptIdExt = undefined;
            this.$(".contacts li").removeClass("hidden");
            this.$('.filter-department-data').addClass("hidden");
        },

        _openChatter: function (e) {
            var target = $(e.target).closest("li");
            var jid = target.attr('jid');

            egov.pubsub.publish("message.open", {
                chatterid: jid
            });
        },

        //#endregion	

        //#region Private Methods

        _renderFilterred: function () {
            if (this.filterDeptIdExt === undefined) {
                return;
            }

            var userCount = 0;
            var userIdInDepts = _.pluck(_.where(btalk.egov.options.userDeptPoses, { idext: this.filterDeptIdExt }), 'userid');

            this.$(".contacts li").each(function () {
                var userElement = $(this);
                var userId = parseInt(userElement.attr("userid"));
                if (_.contains(userIdInDepts, userId)) {
                    userCount++;
                    userElement.removeClass("hidden");
                } else {
                    userElement.addClass("hidden");
                }
            });

            this.$('.filter-department-data').removeClass("hidden");
            this.$('.filter-department-data .filter-name').text(String.format("Xem: {0} - {1}", this.filterName, userCount));
        }

        //#endregion
    });

    return DepartmentList;
});