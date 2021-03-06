/// <reference path="views/document/util/export.js" />
/// <summary>Khai báo đường dẫn các thư viện của hệ thống cho RequireJS.</summary>
requirejs.config({
    // Đường dẫn gốc, các đường dẫn khác trong paths sẽ xuất phát từ đường dẫn gốc này
    baseUrl: '../../Scripts/bkav.egov',

    // Tienbv: thay đổi chút về cách đánh phiên bản
    //     - file1main.js luôn luôn tải về bản mới nhất.
    //     - các file khác phụ thuộc vào phiên bản được đánh này, khi sửa file js thì đánh lại phiên bản cho nó là dc. Như thế ko phải lúc nào f5 cũng load về cái mới nhất
    // Todos: về lâu dài sẽ tính cho từng file, file nào thay đổi thì đánh version lại cho file đó thôi

    
    urlArgs: "v=1111202sssssssss120a",


    // Khai báo tên và các đường dẫn tới thư viện js tương ứng
    paths: {
        //===========================================
        // jquery and plugins
        jcrop: 'libs/jquery/jcrop/js/jquery.Jcrop.min',
        // nicescroll: 'libs/jquery/jquery.nicescroll.min',
        filedownload: 'libs/jquery/jquery.fileDownload.min',
        jstree: 'libs/jstree/jquery.jstree',
        validateDateTime: 'libs/jquery/jquery.ui.datepicker.validation.min',
        // timepicker: 'libs/jquery/jquery.timepicker/jquery.timepicker.min',
        // spellChecker: 'libs/bkav/jquery.spell.checker.min',

        //eForm
        eFormJqueryGlobal: 'libs/eForm/jquery/jquery.global',
        eFormJqueryGlobalViVN: 'libs/eForm/jquery/jquery.glob.vi-VN',
        eFormJqueryMaskedinput: 'libs/eForm/jquery/jquery.maskedinput-1.3.min',
        eFormJqueryMeioMask: 'libs/eForm/jquery/jquery.meio.mask.min',

        eFormJsutilt: 'libs/eForm/jsutilt',
        eFormLibdata: 'libs/eForm/eForm.Libdata',
        eFormControls: 'libs/eForm/eForm.Controls',
        eFormLib: 'libs/eForm/eForm.Lib',
        eFormDB: 'libs/eForm/eForm.DB',
        eFormTool: 'libs/eForm/eForm.Tool',
        eFormResize: 'libs/eForm/Resize',

        select2: 'libs/select2/select2.min',

        eFormBkavEgateApplet: 'libs/eForm/bkav.egate.applet',
        eFormBkavEgate: 'libs/eForm/bkav.egate',
        eFormBkavEgateCompiler: 'libs/eForm/bkav.egate.compiler',

        vendors: 'vendor.min',
        egovCore: 'egovcore',

        linq: '/Scripts/bkav.egov/libs/linq/linq',

        ereportth: '/Scripts/generalreport/eReport',

        // #region base view

        dialog: 'views/base/egov.dialog.min',
        modal: 'views/base/egov.modal',
        treeBase: 'views/base/egov.tree.min',
        paging: 'views/base/egov.paging.min',

        //#endregion

        //locache: 'locache/egov.locache.min',
        formtemplate: 'util/egov.formtemplate.min',
        egovHelper: "views/main/egov.helper.min",
        egovNotify: "views/main/egov.notify.min",
        headerMain: "views/main/main",
        autocomplete: 'libs/bkav/bkav.autocomplete.selectfirst.min',
        egovSearch: 'views/search/egovSearch.min',

        //collections
        docPermisstions: 'collections/docPermisstions',
        //mudim
        mudim: 'libs/mudim-minorfixed',

        // Vote
        referendumModel: 'referendum/referendumModel',
        referendum: 'referendum/referendum',
        referendumCreate: 'referendum/referendumCreate',
        referendumVote: 'referendum/referendumVote',
        referendumDropdownUser: 'referendum/dropdownUser',
        referendumDropdownUserView: 'referendum/dropdownUserView',

        // Views -------------------------------
        templateList: "templates/template",
        homeView: 'views/home/home.min',
        tabView: 'views/home/tab.min',
        toolbarView: 'views/home/toolbar.min',
        treeView: 'views/home/tree',
        documentsView: 'views/home/documents',
        contextMenuView: 'views/home/contextmenu.min',
        storeTree: 'views/home/storePrivate.min',
        newStorePrivateView: 'views/home/createNewPrivateStore.min',
        addStoreAttachment: 'views/home/addStoreAttachment.min',
        reportViews: 'views/report/report',
        documentContextmenu: 'views/home/documentContextmenu',
        documentShortkey: 'util/egov.shortkey.document',

        // Report View
        homeReportView: 'views/homesmreport/home',
        tabReportView: 'views/homesmreport/tab',
        toolbarReportView: 'views/homesmreport/toolbar.min',
        treeReportView: 'views/homesmreport/tree',
        documentsReportView: 'views/homesmreport/documents',
        contextMenuReportView: 'views/homesmreport/contextmenu.min',
        storeReportTree: 'views/homesmreport/storePrivate.min',
        newStoreReportPrivateView: 'views/homesmreport/createNewPrivateStore.min',
        addStoreReportAttachment: 'views/homesmreport/addStoreAttachment.min',
        reportReportViews: 'views/report/report.min',
        documentReportContextmenu: 'views/homesmreport/documentContextmenu.min',

        BusinessLicenseView: "views/document/functions/BusinessLicense",
        BusinessLicenseShowView: "views/document/functions/BusinessLicenseShowView",

        ImportExcelView: "views/document/functions/importExcel",
        ImportWordView: "views/document/functions/importWord",
        TreeView: "views/document/functions/viewTree",
        ShowUploadImage: "views/document/functions/showUploadImage",


        InvoiceView: "views/document/functions/invoice",
        InvoiceShowView: "views/document/functions/invoiceShowView",

        // Document
        documentView: 'views/document/document',
        surveyContentView: 'views/document/survey',
        documentMultipleView: 'views/document/multi-document',
        documentToolbar: 'views/document/toolbar',
        dynamicForm: 'views/document/form/dynamic.min',
        htmlForm: 'views/document/form/html.min',
        urlform: 'views/document/form/urlform.min',
        docRelation: 'views/document/relations.min',
        docAttachment: 'views/document/attachments',
        transfer: 'views/document/functions/transfer',
        PublishAndFinishView: 'views/document/functions/publishAndFinish',
        surveyView: 'views/document/functions/surveyDoc',
        transferExtend: 'views/document/functions/transferExtend',
        anticipate: 'views/document/functions/anticipate.min',
        appoint: 'views/document/functions/appoint.min',
        publishmentGovView: 'views/document/functions/publishmentGov',
        publishmentView: 'views/document/functions/publishment',
        privatePublishmentView: 'views/document/functions/privatePublishment.min',
        announcementView: 'views/document/functions/announcement.min',
        consultView: 'views/document/functions/consult.min',
        docStoreView: 'views/document/functions/storePrivate.min',
        scanner: 'views/document/scanner',
        templateComment: 'views/document/functions/templateComment.min',
        documentDetail: 'views/home/document-detail.min',
        supplementary: 'views/document/functions/supplementary.min',
        renewals: 'views/document/functions/renewals.min',
        updateLastResult: 'views/document/functions/updateLastResult',
        returnResult: 'views/document/functions/returnResult.min',
        publicResult: 'views/document/functions/publicResult.min',
        lienthong: 'views/document/functions/lienthong',
        continueProcess: 'views/document/functions/continueProcess.min',
        confirmTransferOrProcess: 'views/document/functions/ConfirmTransferOrProcess.min',
        supplementaryList: 'views/document/supplementaryList.min',
        insertImagePacket: 'views/document/insertImagePacket.min',
        question: 'views/document/question/question.min',
        questionForward: 'views/document/question/forward.min',
        retrieve: 'views/document/functions/retrieve.min',
        exportdata: 'views/document/util/export',
        addIndicator: 'views/document/util/addIndicator',
        thresHold: 'views/document/util/thresHold',
        buildQuery: 'views/document/util/buildQuery',
        editDataRow: 'views/document/util/editDataRow',

        warningCompilation: 'views/document/functions/warningCompilation',
        surveyWarningCompilation: 'views/document/functions/surveyWarningCompilation',
        followVPCP: 'views/document/functions/followVPCP',

        DateResponseAddresses: 'views/document/functions/dateResponseAddresses.min',
        savePrivateStore: 'views/document/futions/savePrivateStore.min',

        //DocumentOnline
        documentQuestions: 'views/document/question/documentQuestions.min',
        checkCitizenInfo: 'views/document/online/checkCitizenInfo.min',
        supplementaryOnline: 'views/document/online/supplementary.min',

        tempMailOrSms: 'views/document/functions/editTemplateMailOrSms.min',

        // Doctype Paper Fee
        doctypePaperFee: 'views/doctype/doctype_paperfee',
        doctypecreate: 'views/doctype/doctype_create',
        doctypeconfigsurvey: 'views/doctype/doctype_config_survey',

        search: 'views/search/search.min',
        resource: 'resource/egov.resources.bindresource.min',

        //common
        text: 'libs/require/text.min',
        knockout: 'libs/knockout/knockout-3.3.0',

        hashBase64: 'libs/hashbase64',
        jprint: "libs/jPrint",
        print: "views/document/functions/print",

        //bmailChatHangout: 'libs/bmailChat/ChatHangout.min',
        //bmailChatPopup: 'libs/bmailChat/PopupChat.min',

        // egov new
        //modernizr: 'libs/modernizr',

        // Chat        
        chatViewMain: "../mobile/chat/chat_mobile",
        charViewManager: "../btalk/app/views/btalk.home-desktop",
        chatContactView: "../btalk/app/views/btalk.contact_desktop",
        chatRoster: '../btalk/app/views/btalk.roster',
        chatDepartmentView: '../btalk/app/views/btalk.view.departments',
        chatHistoryView: '../btalk/app/views/btalk.view.histories',
        chatSearch: '../btalk/app/views/btalk.view.search',
        ChatDock: 'views/chat/chat_dock',

        chatterView: '../btalk/app/views/btalk.view.chatter',
        messageView: '../btalk/app/views/btalk.view.messages-desktop',

        chatterModel: '../btalk/app/models/btalk.model.chatter',
        chatterMessageModel: '../btalk/app/models/btalk.model.message',
        
        btalkCore: "../btalk/btalk-core",
        chatConfig: "../btalk/app/egov.config",
        jsjac: "../btalk/jsjac",
        
        //Bmail editor
        yui: "libs/bkav/editor/js/yui",
        bmailEditorlanguage: "libs/bkav/editor/js/language",
        bmailEditorConfig: "libs/bkav/editor/js/Config",
        bmailEditorMenuButton: "libs/bkav/editor/js/MenuButton",
        bmailEditorEditorControl: "libs/bkav/editor/js/EditorControl",
        bmailEditorControls: "libs/bkav/editor/js/Controls",
        bmailEditor: "libs/bkav/editor/js/BmailEditor"
    },

    // Khai báo sự phụ thuộc của các thư viện với nhau.
    // 'layout': ['jquery'] => 'layout' là tên thư viện được khai báo trong paths ở trên.
    //                      => ['jquery'] là thư viện cần load và excuted trước khi tải 'layout'
    shim: {
        //vendors: ['jquery'],
        egovCore: ['vendors', 'jstree'],
        'jstree': ['vendors'],
        fileupload: ['vendors'],
        alohaConfig: ['vendors'],
        contextMenuView: ['vendors'],
        modal: ['vendors'],
        dialog: ['modal'],

        dynamicForm: ['knockout'],
        eFormBkavEgate: ['knockout'],
        eFormBkavEgateCompiler: ['knockout', 'eFormBkavEgate', 'eFormTool'],
        print: ["jprint"],

        eFormJqueryGlobalViVN: ['eFormJqueryGlobal'],
        plugin: ['pluginFactory', 'libs/bkav/PluginEgov/fb_installer.min'], //, 'libs/bkav/PluginEgov/fb_installer.min'

        //mobile
        charViewManager: ['chatterModel', 'chatterMessageModel'],
        btalkCore: ['jsjac'],
        chatRoster: ['btalkCore'],
        chatView: ['btalkCore'],
        chatterView: ['btalkCore', 'chatterModel'],

        headerMain: ['egovHelper'],

        bmailEditorControls: ['bmailEditorEditorControl'],
        bmailEditor: ['yui', 'bmailEditorlanguage', 'bmailEditorConfig', 'bmailEditorMenuButton', 'bmailEditorEditorControl', 'bmailEditorControls']
    },

    mobileIgnores: ['dotdotdot', 'spellChecker'],

    waitSeconds: 120,

    staticPath: [
        '/libs/'
    ],
});