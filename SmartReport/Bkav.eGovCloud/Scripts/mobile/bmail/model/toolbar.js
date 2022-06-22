(function (bmail) {
    bmail.toolbarList = [];
    bmail.customToolbar = {};

    bmail.customToolbar.toolbarItem = {
        reply: {//Trả lời
            name: "shopPopup",
            text: bmail.resources.toolbar.reply,
            href: "editorpage",
            hasPopup: true,
            id: "ReplyBT",
            className: "toolbar_reply",
            icon: "fa fa-mail-reply",
            isImportant: true,
            order: 1,
        },
        replyall: {//Trả lời tất cả
            name: "replyall",
            text: bmail.resources.toolbar.replyall,
            href: "editorpage",
            hasPopup: true,
            id: "ReplyMore",
            className: "toolbar_replyall",
            icon: "fa fa-mail-reply-all",
            isImportant: true,
            order: 2,
        },
        "delete": {//Xóa
            name: "trash",
            text: bmail.resources.toolbar.delete,
            href: "deletePopup",
            hasPopup: true,
            id: "btndelete",
            className: "toolbar_delete",
            icon: "fa fa-trash",
            isImportant: true,
            order: 3,
        },
        deletePer: {//Xóa vĩnh viễn
            name: "delete",
            text: bmail.resources.toolbar.deletePer,
            href: "deletePopup",
            hasPopup: true,
            id: "btndelete",
            className: "toolbar_deletePer",
            icon: "fa fa-trash",
            isImportant: true,
            order: 4,
        },
        edit: {//Sửa
            name: "edit",
            text: bmail.resources.toolbar.edit,
            href: "editorpage",
            hasPopup: true,
            id: "btnedit",
            className: "toolbar_edit",
            icon: "fa fa-edit",
            isImportant: false,
            order: 5,
        },
        forward: {//Chuyển tiếp
            name: "forward",
            text: bmail.resources.toolbar.forward,
            href: "editorpage",
            hasPopup: true,
            id: "btnforward",
            className: "toolbar_forward",
            icon: "fa fa-mail-forward",
            isImportant: false,
            order: 6,
        },
        spam: {//Đánh dấu là thư rác
            name: "spam",
            text: bmail.resources.toolbar.spam,
            href: "",
            hasPopup: false,
            id: "btnspam",
            className: "toolbar_spam",
            icon: "fa fa-flag",
            isImportant: false,
            order: 7,
        },
        unspam: {//Bỏ đánh dấu là thư rác
            name: "!spam",
            text: bmail.resources.toolbar.unspam,
            href: "",
            hasPopup: false,
            id: "btnunspam",
            className: "toolbar_unspam",
            icon: "fa fa-flag-o",
            isImportant: false,
            order: 8,
        },
        unread: {//Đánh dấu là chưa đọc
            name: "!read",
            text: bmail.resources.toolbar.unread,
            href: "",
            hasPopup: false,
            id: "btnunread",
            className: "toolbar_unread",
            icon: "fa fa-eye-slash",
            isImportant: false,
            order: 9,
        },
        restore: {//Khôi phục mail đã xóa
            name: "restore",
            text: bmail.resources.toolbar.restore,
            href: "",
            hasPopup: false,
            id: "btnrestore",
            className: "toolbar_restore",
            icon: "fa fa-repeat",
            isImportant: false,
            order: 3,
        },
    }

    bmail.customToolbar.getToolbar = function (path) {
        switch (path) {
            case "inbox": //Hộp thư đến 
                return "reply, replyall, forward, spam, delete, unread";
                break;
            case "sent": //Thư đã gửi
                return "forward, unread, delete";
                break;
            case "drafts": //Thư nháp
                return "edit, unread, delete";
                break;
            case "junk": //Thư rác
                return "forward, unspam, unread, delete";
                break;
            case "trash": //Thư đã xóa
                return "forward, restore, unread, deletePer";
                break;
            default: //Khác
                return "reply, replyall, forward, unread, delete";
                break;
        }
    }
})
(window.bmail = window.bmail || {})
