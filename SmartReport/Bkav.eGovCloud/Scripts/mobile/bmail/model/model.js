
define(function () {
    bmail.models = bmail.models || {};

    bmail.models.folder = Backbone.Model.extend({
        defaults: {
            path: "",
            pathName: "",
            id: "",
            visible: "",//0 || 1
            link: "",
            parentid: "",
            totalUnread: 0,
            hasChildren: false,
            isOpen: 0,
            isPublicFolder: false
        },

        initialize: function () {

        }
    });

    bmail.models.folderList = Backbone.Collection.extend({
        model: bmail.models.folder
    });

    bmail.models.mail = Backbone.Model.extend({
        defaults: {
            date: "",
            location: "",
            conversationId: "",
            id: "",
            trimId: "",
            domId: "",
            mailDetailId: "",
            sender: {
                address: "",
                name: "",
                fullname: "",
            },
            alphabet: "",
            hasAttach: false,
            subject: "",
            unread: 1,
            perm: {},
            selected: false,
            isBinded: false,
        },

        initialize: function () {

        }
    });

    bmail.models.mailList = Backbone.Collection.extend({
        model: bmail.models.mail
    });

    bmail.models.attachment = Backbone.Model.extend({
        defaults: {
            part: "",
            size: 0,
            contentType: "",
            fileName: "",
            isCreate: false,
            downloadUrl: ""
        },

        initialize: function () {

        }
    });

    bmail.models.attachmentList == Backbone.Collection.extend({
        model: bmail.models.attachment
    });

    bmail.models.maildetail = Backbone.Model.extend({
        defaults: {
            id: "",
            mailId: "",
            bcc: "",
            cc: "",
            receivers: "",
            receiversLabel: "",
            sender: "",
            subject: "",
            content: "",
            date: "",
            location: "",
            perm: {},
        },

        initialize: function () {

        }
    });
});

