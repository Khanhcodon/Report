(function (egov) {

    egov.template = {
        // ======================================================
        tree: {
            documentTree: "text!templates/tree/tree.html",
            storeTree: "text!templates/tree/treeStorePrivate.html",
            storeTreeMobile: "text!templates/tree/treeStorePrivate-mobile.html",
            mobile: "text!templates/tree/tree-mobile.html",
            createPrivateStore: "text!templates/tree/createStorePrivate.html",
            createStoreAttachment: "text!templates/tree/addStoreAttachment.html",
            question: "text!templates/tree/treeQuestion.html",
            questionMobile: "text!templates/tree/treeQuestion-mobile.html",
        },

        // ======================================================
        question: {
            quickView: "text!templates/question/quickView.html",
            detail: "text!templates/question/detail.html",
            forward: "text!templates/question/forward.html",
            reject: "text!templates/question/reject.html",
            documentQuestions: "text!templates/question/documentQuestions.html",
            mobileQuestion: "text!templates/question/tablet-mobile.html",
            mobileDetail: "text!templates/question/detailMobile.html",
        },

        // ======================================================
        tab: {
            desktop: "text!templates/tab.html"
        },

        // ======================================================
        doctype: {
            paperFees: "text!templates/doctype/doctypePaperFee.html",
        },

        doctypeCreate: "text!templates/doctype/doctypeCreate.html",
        doctypeConfigSurvey: "text!templates/doctype/doctypeConfigSurvey.html",

        // ======================================================
        documentList: {
            desktop: "text!templates/documents.html",
            mobileDocumentItem: "text!templates/document-tablet-mobile.html",
            quickViewBelow: "text!templates/document-quickview-below.html",
            quickViewRight: "text!templates/document-quickview-right.html",
            quickViewDocumentOnline: "text!templates/documentOnlineQuickView.html",
            paging: "text!templates/documentsPaging.html",
            desktopToolbar: "text!templates/toolbar.html",
            mobileToolbar: "text!templates/toolbar-tablet-mobile.html",
            quickViewOnlineRegistration: "text!templates/documentOnlineRegistrationQuickView.html",
        },

        // ======================================================
        document: {
            desktop: "text!templates/document/document.html",
            desktopSurvey: "text!templates/document/documentSurvey.html",
            surveyContent: "text!templates/document/survey/surveyContent.html",
            detail: "text!templates/document/document-detail.html",
            mobile: "text!templates/document/document-Tablet-Mobile-Template.html",
            mobileError: "text!templates/document/document-tablet-mobile-error-template.html",
            mobileInfo: "text!templates/document/mobile/document-info.html",
            mobileCreate: "text!templates/document/mobile/document-create.html",
            mobileCategoryBusiness: "text!templates/document/document-tablet-mobile-categorybusiness.html",
            attachment: "text!templates/document/attachment.html",
            attachmentTheoLo: "text!templates/document/attachment-multi.html",
            attachmentPreview: "text!templates/document/attachmentPreview.html",
            comment: "text!templates/document/documentCommentNew.html",
            commentMulti: "text!templates/document/documentComment-multi.html",
            commentNavbarMobile: "text!templates/document/document-tablet-mobile-navbar-comment-template.html",
            documentPreviewComment: "text!templates/document/documentPreviewComment.html",
            documentOnline: "text!templates/DocumentOnlineView-template.html",
            checkCitizenInfo: "text!templates/document/documentOnline-check-citizen-info.html",
            checkCitizenInfoItem: "text!templates/document/documentOnline-check-citizen-info-document-item.html",
            relation: "text!templates/document/relation.html",
            confirmRelation: "text!templates/document/confirmRelation.html",
            scan: "text!templates/document/scan.html",
            mobileToolbar: "text!templates/document/toolbar-tablet-mobile.html",
            desktopToolbar: "text!templates/document/toolbar.html",
            quickViewToolBar: "text!templates/document/toolbar-quickview.html",
            quickView: 'text!templates/document/document-quickView-template.html',
            quickViewRight: 'text!templates/document/document-quickView-right-template.html',
            templateComment: 'text!templates/document/templateComment.html',
            templateCommentItem: 'text!templates/document/templateCommentItem.html',
            storePrivate: 'text!templates/document/storePrivate.html',
            publishment: 'text!templates/document/publishment.html',
            PublishAndFinishView: 'text!templates/document/publishAndFinish.html',
            publishmentGov: 'text!templates/document/publishmentGov.html',
            addressItem: 'text!templates/document/addressItem.html',
            consult: 'text!templates/tranfer/consult.html',
            anticipate: 'text!templates/document/anticipate.html',
            appoint: 'text!templates/document/appoint.html',
            announcement: 'text!templates/tranfer/announcement.html',
            //onlineRegistration: "text!templates/documentOnlineRegistrationView-template.html",
            onlineRegistration: "text!templates//document/documentOnline.html",
            multiselectionbar: "text!templates/multi-selection-bar-mobile.html",
            changeDateCreated: "text!templates/document/changeDateCreated.html",
            supplementary: "text!templates/document/supplementaryList.html",
            supplementaryOnline: "text!templates/document/supplementaryOnline.html",
            changeworkflowTypes: "text!templates/document/changeworkflowTypes.html",
            printLeftPanel: "text!templates/document/printLeftPanel.html",
            lienthong: "text!templates/document/lienthong.html",
            editTemplateMailOrSms: "text!templates/document/editTemplateMailOrSms.html",
            insertImagePacket: "text!templates/document/insertImagePacket.html",
            continueProcess: "text!templates/document/continueProcess.html",
            codetemp: "text!templates/document/codetemp.html",
            dateResponseAddresses: "text!templates/document/dateResponseAddresses.html",
            dateResponseAddressesItem: "text!templates/document/dateResponseAddressesItem.html",
            signer: "text!templates/document/signer.html",
            multi: "text!templates/document/multi-document.html",
            warningCompilation: "text!templates/document/warningCompilation.html",
            surveyWarningCompilation: "text!templates/document/survey/warningCompilation.html",
            addIndicator: "text!templates/document/addIndicator.html",
            thresHold: "text!templates/document/thresHold.html"
        },

        // ======================================================
        transfer: {
            donggui: 'text!templates/tranfer/dgview.html',
            jobTitle: 'text!templates/tranfer/jobTitles.html',
            jobTitleDept: 'text!templates/tranfer/jobTitlesDept.html',
            viewTarget: 'text!templates/tranfer/viewTarget.html',
            transfer: 'text!templates/document/transfer.html',
            transferMobile: 'text!templates/document/transfer-mobile.html',
            transferItem: 'text!templates/document/transferItem.html',
            transferItemMobile: 'text!templates/document/transferItem-mobile.html',
            transferExtend: 'text!templates/tranfer/transferExtend.html',
            transferExtendMobile: 'text!templates/tranfer/transferExtend-mobile.html',
            plan: 'text!templates/tranfer/plan-template.html',
        },
        survey: 'text!templates/document/survey.html',
        toolbar: {
            desktop: "text!templates/document/toolbar.html",
            mobile: "text!templates/document/toolbar-tablet-mobile.html",
            quickViewRight: 'text!templates/document/toolbar-quickview.html',
            quickViewBelow: 'text!templates/document/toolbar-quickview-below.html'
        },

        supplementary: {
            receiveSupplement: "text!templates/document/supplementary.html",
            receivedSupplementary: "text!templates/document/receivedSupplementary.html"
        },

        renewals: "text!templates/document/renewals.html",

        updateLastResult: "text!templates/document/updateLastResult.html",

        returnResult: "text!templates/document/returnResult.html",
        updateResult: "text!templates/document/updateResult.html",

        deptUser: "text!templates/referendum/deptUser.html",
        deptUserView: "text!templates/referendum/deptUserView.html",
        referendum: "text!templates/referendum/referendum.html",
        createReferendum: "text!templates/referendum/create-referendum.html",
        voteReferendum: "text!templates/referendum/vote-referendum.html",
        voteItemReferendum: "text!templates/referendum/vote-item-referendum.html",
        voteOnlyItemReferendum: "text!templates/referendum/voteonly-item-referendum.html",
        viewOnlyItemReferendum: "text!templates/referendum/viewonly-item-referendum.html",

        businessLicense: "text!templates/document/businessLicense.html",
        business: "text!templates/document/business.html",
        businessLicenseView: "text!templates/document/businessLicenseView.html",

        ImportExcelView: "text!templates/document/import-excel.html",
        ImportWordView: "text!templates/document/import-word.html",
        ViewTreeHtml: "text!templates/document/viewTree.html",
        ShowUploadImageHtml: "text!templates/document/ShowUploadImage.html",

        publicResult: "text!templates/document/publicResult.html",
        search: {
            main: 'text!templates/search/search.html',
            result: 'text!templates/search/searchResult.html'
        },

        contextmenu: "text!templates/contextmenuItem.html",

        mobile: {
            userConfig: "text!templates/mobile/user-config.html",
            notifyItem: "text!templates/mobile/notify-item.html",
        },

        calendar: {
            edit: "text!templates/calendar/calendar.html"
        },

        chat: {
            contactList: "text!templates/chat/contact-list.html",
            contactItem: "text!templates/chat/contact-list-item.html",
            chatTabItem: 'text!templates/chat/chat-tab-item.html',

            messageView: 'text!templates/chat/chat-message.html',
            messageGroup: 'text!templates/chat/chat-message-chattergroup.html',
            messageItem: 'text!templates/chat/chat-message-item.html',
            messageFileItem: 'text!templates/chat/chat-message-fileitem.html',
            messageImgItem: 'text!templates/chat/chat-message-imgitem.html',
            config: 'text!templates/chat/chat-message-config.html'
        },
        invoice: {
            importinvoice: "text!templates/document/importInvoice.html",
            invoiceview: "text!templates/document/invoiceView.html",
        }
    }
})
(this.egov = this.egov || {})