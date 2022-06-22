/// <summary>Khai báo đường dẫn các thư viện của hệ thống cho RequireJS.</summary>
requirejs.config({
    // Đường dẫn gốc, các đường dẫn khác trong paths sẽ xuất phát từ đường dẫn gốc này
    baseUrl: '../../Scripts/bkav.egov',

    // Tienbv: thay đổi chút về cách đánh phiên bản
    //     - file main.js luôn luôn tải về bản mới nhất.
    //     - các file khác phụ thuộc vào phiên bản được đánh này, khi sửa file js thì đánh lại phiên bản cho nó là dc. Như thế ko phải lúc nào f5 cũng load về cái mới nhất
    // Todos: về lâu dài sẽ tính cho từng file, file nào thay đổi thì đánh version lại cho file đó thôi
    urlArgs: "e",

    // Khai báo tên và các đường dẫn tới thư viện js tương ứng
    paths: {
        //===========================================
        // jquery and plugins
        jquery: 'libs/jquery/jquery-2.2.3.min',
        jqueryBrowser: 'libs/jquery/browser/jquery.browser.min',
        jqueryUI: 'libs/jquery/jquery-ui-1.8.22.modified.min',
        jcrop: 'libs/jquery/jcrop/js/jquery.Jcrop.min',
        cookie: 'libs/jquery/jquery.cookie.min',
        nicescroll: 'libs/jquery/jquery.nicescroll.min',
        tmpl: 'libs/jquery/jQuery-tmpl.modified.min',
        layout: 'libs/jquery/jquery.ui.layout/jquery.layout-latest.min',
        globalize: 'libs/jquery/jquery.globalize/globalize.min',
        globalculture: 'libs/jquery/jquery.globalize/cultures/globalize.culture.vi-VN',
        qtip: 'libs/jquery/jquery.qtip/jquery.qtip.min',
        fileupload: 'libs/jquery/jquery.fileupload/js/jquery.fileupload.min',
        'jquery.ui.widget': 'libs/jquery/jquery.fileupload/js/vendor/jquery.ui.widget',
        filedownload: 'libs/jquery/jquery.fileDownload.min',
        bootstrap: 'libs/bootstrap/bootstrap.min',
        validate: 'libs/jquery/jquery.validate.min',
        jstree: 'libs/jstree/jquery.jstree',
        validateDateTime: 'libs/jquery/jquery.ui.datepicker.validation.min',
        timepicker: 'libs/jquery/jquery.timepicker/jquery.timepicker.min',
        dotdotdot: 'libs/jquery/jQuery.dotdotdot/jquery.dotdotdot.min',
        spellChecker: 'libs/bkav/jquery.spell.checker.min',

        offline: 'libs/offline/offline',

        //===========================================
        //backbone vs underscore
        underscore: 'libs/underscore/underscore-1.8.3.min',
        backbone: 'libs/backbone/backbone-1.3.3.min',

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

        eFormBkavEgateApplet: 'libs/eForm/bkav.egate.applet',
        eFormBkavEgate: 'libs/eForm/bkav.egate',
        eFormBkavEgateCompiler: 'libs/eForm/bkav.egate.compiler',

        //===========================================
        // Bkav libs
        //Plugin
        //pluginBase: 'libs/bkav/PluginEgov/Base',
        //extension: 'libs/bkav/PluginEgov/Extension',
        //fireBreath: 'libs/bkav/PluginEgov/FireBreath',
        //pluginFactory: 'libs/bkav/PluginEgov/PluginFactory',

        pluginBase: 'libs/bkav/PluginEgov/Base',
        bChromeCAPlugin: 'libs/bkav/PluginEgov/BChromeCA_Plugin',
        extension: 'libs/bkav/PluginEgov/ChromeNativeApp',
        fireBreath: 'libs/bkav/PluginEgov/FirefoxPlugin',
        pluginFactory: 'libs/bkav/PluginEgov/pluginfactory',

        // #region base view

        dialog: 'views/base/egov.dialog.min',
        modal: 'views/base/egov.modal.min',
        treeBase: 'views/base/egov.tree.min',

        //#endregion

        grid: 'libs/bkav/bkav.grid.min',
        message: 'util/egov.message.min',
        tooltip: 'util/egov.tooltip.min',
        egovCookie: 'util/egov.cookie.min',
        locache: 'locache/egov.locache.min',
        eGovLocalStorage: 'util/egov.localStorage.min',
        formtemplate: 'util/egov.formtemplate.min',
        eGovTable: 'util/egov.table.min',
        homeShortkey: 'util/egov.shortkey.home.min',
        documentShortkey: 'util/egov.shortkey.document.min',
        plugin: 'util/egov.plugin',
        egovHelper: "views/main/egov.helper.min",
        egovNotify: "views/main/egov.notify.min",
        headerMain: "views/main/main.min",
        autocomplete: 'libs/bkav/bkav.autocomplete.selectfirst.min',
        egovSearch: 'views/search/egovSearch.min',
        egovPopup: 'util/egov.popup.min',
        retrieve: 'views/document/functions/retrieve.min',
        bkavUtilities: "util/bkav.utilities.min",
        bt_string: 'util/bt.util.string.min',
        bt_date: "util/bt.util.date.min",

        //collections
        docPermisstions: 'collections/docPermisstions',
        //mudim
        mudim: 'libs/mudim-minorfixed',
        livejs: 'libs/live',

        // Views -------------------------------
        templateList: "templates/template",
        homeView: 'views/home/home.min',
        tabView: 'views/home/tab.min',
        toolbarView: 'views/home/toolbar.min',
        treeView: 'views/home/tree',
        documentsView: 'views/home/documents.min',
        contextMenuView: 'views/home/contextmenu.min',
        storeTree: 'views/home/storePrivate.min',
        newStorePrivateView: 'views/home/createNewPrivateStore.min',
        addStoreAttachment: 'views/home/addStoreAttachment.min',
        reportViews: 'views/report/report.min',
        documentContextmenu: 'views/home/documentContextmenu.min',

        documentView: 'views/document/document.min',
        documentToolbar: 'views/document/toolbar.min',
        dynamicForm: 'views/document/form/dynamic.min',
        htmlForm: 'views/document/form/html.min',
        urlform: 'views/document/form/urlform.min',
        docRelation: 'views/document/relations.min',
        docAttachment: 'views/document/attachments',
        transfer: 'views/document/functions/transfer.min',
        transferExtend: 'views/document/functions/transferExtend',
        anticipate: 'views/document/functions/anticipate.min',
        appoint: 'views/document/functions/appoint.min',
        publishmentView: 'views/document/functions/publishment.min',
        privatePublishmentView: 'views/document/functions/privatePublishment',
        announcementView: 'views/document/functions/announcement.min',
        consultView: 'views/document/functions/consult.min',
        docStoreView: 'views/document/functions/storePrivate.min',
        scanner: 'views/document/scanner.min',
        templateComment: 'views/document/functions/templateComment.min',
        documentDetail: 'views/home/document-detail.min',
        supplementary: 'views/document/functions/supplementary.min',
        renewals: 'views/document/functions/renewals.min',
        updateLastResult: 'views/document/functions/updateLastResult.min',
        returnResult: 'views/document/functions/returnResult.min',
        publicResult: 'views/document/functions/publicResult.min',
        lienthong: 'views/document/functions/lienthong.min',
        continueProcess: 'views/document/functions/continueProcess.min',
        confirmTransferOrProcess: 'views/document/functions/ConfirmTransferOrProcess.min',
        supplementaryList: 'views/document/supplementaryList.min',
        insertImagePacket: 'views/document/insertImagePacket.min',
        question: 'views/document/question/question.min',
        questionForward: 'views/document/question/forward.min',

        //DocumentOnline
        documentQuestions: 'views/document/question/documentQuestions.min',
        checkCitizenInfo: 'views/document/online/checkCitizenInfo.min',
        supplementaryOnline: 'views/document/online/supplementary.min',

        tempMailOrSms: 'views/document/functions/editTemplateMailOrSms.min',

        // Doctype Paper Fee
        doctypePaperFee: 'views/doctype/doctype_paperfee',

        models: "dev/egov.models.min",
        enums: "dev/egov.enum.min",

        search: 'views/search/search.min',
        resource: 'resource/egov.resources.bindresource.min',

        //common
        text: 'libs/require/text',
        knockout: 'libs/knockout/knockout-3.3.0',

        hashBase64: 'libs/hashbase64',
        jprint: "libs/jPrint",
        print: "views/document/functions/print.min",

        ////Calendar
        //calendarDeptUser: 'calendar/egov.tree.departuser',
        //datetimeFunction: 'calendar/eGov.datetimefunction',
        //serverCalendar: 'calendar/egov.server',

        //Mobile
        jqmobile: '../mobile/libs/jquery.mobile-1.4.5.modified.min',
        commonFn: "../mobile/extends/function.min",
        jsmobile: '../mobile/main',
        mobileConfig: '../mobile/views/config.min',
        multiselection: '../mobile/egov/multiselection.min',
        questionMobile: 'views/document/question/questionMobile.min',

        notificationMobile: '../mobile/notify/notifications',
        notifyMobile: '../mobile/notify/notify',
        material: '../mobile/libs/mdl/material.min',
        customMaterial: '../mobile/extends/customMaterial.min',

        bmailTemplate: '../mobile/bmail/templates/templates.min',

        bmailView: '../mobile/bmail/views/main',
        bmailViewFolder: '../mobile/bmail/views/folder',
        bmailViewMailList: '../mobile/bmail/views/mail-list',
        bmailViewDetail: '../mobile/bmail/views/detail',
        bmailViewToolbar: '../mobile/bmail/views/toolbar.min',
        bmailViewMultiSelectionBar: '../mobile/bmail/views/multiselection.min',
        bmailDetailToolbar: '../mobile/bmail/views/toolbar.min',
        bmailDetail: '../mobile/bmail/views/ViewMail.min',
        bmailCreateOrReply: '../mobile/bmail/views/create-reply.min',

        bmailAttributesConstant: '../mobile/bmail/controller/AttributesConstant.min',
        bmailRequest: '../mobile/bmail/controller/SOAPRequest.min',
        bmailResponse: '../mobile/bmail/controller/SOAPResponse.min',
        bmailmakeRequest: '../mobile/bmail/controller/makeRequest.min',
        bmailEvent: '../mobile/bmail/controller/PageEvents.min',
        bmailUpclick: '../mobile/bmail/controller/upclick.min',
        bmailUtil: '../mobile/bmail/util/Util.min',
        bmailCache: '../mobile/bmail/util/Cache.min',
        bmailMsgUtil: '../mobile/bmail/util/MsgUtil',

        bmailDataInit: '../mobile/bmail/model/dataInit.min',
        bmailModel: '../mobile/bmail/model/model.min',
        bmailModelFolder: '../mobile/bmail/model/folder.min',
        bmailModelList: '../mobile/bmail/model/mail-list.min',
        bmailModelDetail: '../mobile/bmail/model/detail.min',
        bmailCustomToolbar: '../mobile/bmail/model/toolbar.min',

        bmailChatController: '../mobile/chat/controllers/chat.min',
        bmailChatAccount: '../mobile/chat/controllers/account.min',
        bmailChatMain: '../mobile/chat/views/main',

        bmailChatHangout: 'libs/bmailChat/ChatHangout.min',
        bmailChatPopup: 'libs/bmailChat/PopupChat.min',

        // egov new
        status: "dev/egov.status.min",
        modernizr: 'libs/modernizr',
        egovUtil: "dev/egov.utils.min",
        egovEnum: "dev/egov.enum.min",
        egovEvents: "dev/egov.events.min",
        egovPubSub: "dev/egov.pubsub.min",
        egovMessage: "dev/egov.message.min",
        egovModels: "dev/egov.models.min",

        egovEntities: "dev/egov.entities.min",
        egovEntitiesSync: "dev/egov.entities.sync.min",
        egovDataAccess: "dev/egov.data-access",
        egovCacheManager: "dev/egov.cache-manager",
        egovRequest: "dev/egov.request-manager.min",
        egovDataManager: "dev/egov.data-manager.min",
        "egovDataManager.tree": "dev/egov.data-manager.tree",
        "egovDataManager.documents": "dev/egov.data-manager.documents.min",
        "egovDataManager.document": "dev/egov.data-manager.document.min",

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
        'jquery': {
            exports: "$"
        },
        //jquery
        jqueryUI: ['jquery'],
        jcrop: ['jquery'],
        cookie: ['jquery'],
        //scrollview: ['jqmobile', 'iscroll'],
        nicescroll: ['jquery'],
        timepicker: ["jqueryUI"],
        tmpl: ['jquery'],
        bootstrap: ['jquery'],
        tooltip: ['jquery'],
        jqueryBrowser: ['jquery'],
        layout: ['jquery', 'jqueryUI', 'jqueryBrowser'],
        globalize: ['jquery'],
        globalculture: ['jquery', 'globalize'],
        qtip: ['jquery'],
        fileupload: ['jquery', 'jquery.ui.widget'],
        alohaConfig: ['jquery'],
        aloha: ['jquery', 'alohaConfig'],
        //backbone vs underscore
        underscore: ['jquery'],
        backbone: ['jquery', 'underscore'],

        //bkav
        grid: ['jquery', 'locache'],
        tooltip: ['jquery', 'qtip'],
        egovCookie: ['jquery', 'locache'],
        eGovLocalStorage: ['jquery', 'locache'],
        dialog: ['jquery'],
        eFormBkavEgate: ['jquery', 'knockout'],
        eFormBkavEgateCompiler: ['jquery', 'knockout', 'eFormBkavEgate', 'eFormTool'],
        print: ["jprint"],

        //util
        eFormJqueryGlobalViVN: ['jquery', 'eFormJqueryGlobal'],
        transferExtend: ['modal'],

        bChromeCAPlugin: ['pluginBase'],
        extension: ['pluginBase'],
        fireBreath: ['pluginBase'],
        pluginFactory: ['extension', 'fireBreath'],
        plugin: ['jquery', 'pluginFactory', 'libs/bkav/PluginEgov/fb_installer.min'], //, 'libs/bkav/PluginEgov/fb_installer.min'

        //mobile
        jqmobile: ['jquery'],
        bmailView: ['jqmobile'],
        bmailmakeRequest: ['bmailRequest'],
        bmailResponse: ['bmailAttributesConstant'],
        bmailDataInit: ['bmailUtil'],
        customMaterial: ['jquery'],
        notifyMobile: ['egovHelper'],

        headerMain: ['egovHelper'],

        //resource
        resource: ['jquery'],

        // Phần datamanager mới
        egovPubSub: ["egovEvents"],
        status: ["jquery", "egovEvents", "egovPubSub"],
        egovRequest: ["underscore"],
        egovEntities: ["egovRequest"],
        egovEntitiesSync: ["egovEntities"],
        egovMessage: ["jquery", "jqueryUI", "egovPubSub"],
        egovCacheManager: ["modernizr"],
        egovDataAccess: ["egovCacheManager", "egovEntities"],
        egovDataManager: ["egovDataAccess", "models"],
        "egovDataManager.tree": ["egovDataManager"],
        "egovDataManager.documents": ["egovDataManager"],
        "egovDataManager.document": ["egovDataManager"],
        egovUtil: ["jquery"],
        homeView: ["egovDataManager.tree", "egovDataManager.document", "egovDataManager.documents"],

        egov: ["egovEntities", "egovEntitiesSync", "egovPubSub", "egovMessage", "egovDataAccess", "models", "egovDataManager"],

        'commonFn': ['jquery', 'underscore'],
        bmailEditorControls: ['bmailEditorEditorControl'],
        bmailEditor: ['yui', 'bmailEditorlanguage', 'bmailEditorConfig', 'bmailEditorMenuButton', 'bmailEditorEditorControl', 'bmailEditorControls'],
        offline: ['jquery']
    },

    mobileIgnores: ['dotdotdot', 'spellChecker'],

    waitSeconds: 120,

    staticPath: [
        '/aloha/',
        '/libs/'
    ],
});