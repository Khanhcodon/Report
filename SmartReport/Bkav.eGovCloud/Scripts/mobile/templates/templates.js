(function (egov) {

    egov.template = {

        // ======================================================
        documentList: {
            DocumentItem: "text!templates/document_list_item.html"
        },

        // ======================================================
        document: {
            mobileInfo: "text!templates/document-info.html",
            mobileCreate: "text!templates/document-create.html",
            commentMobile: "text!templates/document-comment.html",
            attachment: "text!templates/document-attachment.html",
        },

        bmail: {
            folderItem: "text!templates/bmail-folder-item.html",
            listItem: "text!templates/bmail-list-item.html",
            detail: "text!templates/bmail-detail.html",
            toolbar: "text!templates/bmail-toolbar.html",
            attachmentItem: "text!templates/bmail-attachment-item.html",
            createOrReply: 'text!templates/bmail-createorreply.html',
        },

        // ======================================================
        transfer: {
            transferMobile: 'text!templates/transfer-mobile.html',
            transferItemMobile: 'text!templates/transferItem-mobile.html',
            transferExtendMobile: 'text!templates/transferExtend-mobile.html'
        },

        calendar: {
            detail: 'text!templates/calendar-detail.html',
            item: 'text!templates/calendar-list-item.html'
        },

        toolbar: {
            mobile: "text!templates/document/toolbar-tablet-mobile.html"
        },

        notifyItem: "text!templates/notification-list-item.html",

        contact: {
            listItem: 'text!/scripts/mobile/templates/contact-list-item.html',
        },
        history: {
            listItem: 'text!/scripts/mobile/templates/history_list_item.html',
        },
        chat: {
            messageView: 'text!templates/chat-message.html',
            messageGroup: 'text!templates/chat-message-chattergroup.html',
            messageItem: 'text!templates/chat-message-item.html',
            messageFileItem: 'text!templates/chat-message-fileitem.html',
            messageImgItem: 'text!templates/chat-message-imgitem.html',
            messageInfo: 'text!templates/chat-message-info.html',
            imagePreview: 'text!templates/chat-message-preview.html',
            config: 'text!templates/chat-message-config.html'
        },

        search: {
            document: 'text!templates/search-document.html',
            documentResult: 'text!templates/search-document-item.html',
            bmail: 'text!templates/search-bmail.html',
            bmailResult: 'text!templates/search-bmail-item.html'
        }
    }
})
(this.egov = this.egov || {});