/// <summary>Khai báo đường dẫn các thư viện của hệ thống cho RequireJS.</summary>
requirejs.config({
    // Đường dẫn gốc, các đường dẫn khác trong paths sẽ xuất phát từ đường dẫn gốc này
    baseUrl: '../../Scripts/bkav.egov',

    //RequireJS option: tự động chèn vào url khi request.
    urlArgs: "v=" + (new Date()).getTime(),

    // Khai báo tên và các đường dẫn tới thư viện js tương ứng
    paths: {
        //=================
        //jquery
        jquery: 'libs/jquery/jquery-1.7.2.min',
        jqueryUI: 'libs/jquery/jquery-ui-1.8.22.min',
        cookie: 'libs/jquery/jquery.cookie',
        //nicescroll: 'libs/jquery/jquery.nicescroll.min',
        tmpl: 'libs/jquery/jQuery-tmpl',
        layout: 'libs/jquery/jquery.ui.layout/jquery.layout-latest',
        globalize: 'libs/jquery/jquery.globalize/globalize',
        globalculture: 'libs/jquery/jquery.globalize/cultures/globalize.culture.vi-VN',
        qtip: 'libs/jquery/jquery.qtip/jquery.qtip.min',
        fileupload: 'libs/jquery/jquery.fileupload/js/jquery.fileupload',
        'jquery.ui.widget': 'libs/jquery/jquery.fileupload/js/vendor/jquery.ui.widget',
        filedownload: 'libs/jquery/jquery.fileDownload',
        bootstrap: 'libs/bootstrap/bootstrap.min',
        validate: 'libs/jquery/jquery.validate',
        jstree: 'libs/jstree/jquery.jstree',
        validateDateTime: 'libs/jquery/jquery.ui.datepicker.validation',
        jcrop: 'libs/jquery/jcrop/js/jquery.Jcrop.min',

        //================
        //backbone vs underscore
        underscore: 'libs/underscore/underscore-1.8.3.min',
        backbone: 'libs/backbone/backbone-min',

        //bkav
        grid: 'libs/bkav/bkav.grid',
        message: 'util/egov.message',
        tooltip: 'util/egov.tooltip',
        egovCookie: 'util/egov.cookie',
        dialog: 'views/base/egov.dialog',
        //locache: 'locache/egov.locache',
        formtemplate: 'util/egov.formtemplate',
        modal: 'views/base/egov.modal',
        table: 'util/egov.table',
        query: 'util/egov.query',
        homeShortkey: 'util/egov.home.shortkey',
        documentShortkey: 'util/egov.document.shortkey',
        plugin: 'util/egov.plugin',

        //eForm
        eFormJqueryGlobal: 'libs/eForm/jquery/jquery.global',
        eFormJqueryGlobalViVN: 'libs/eForm/jquery/jquery.glob.vi-VN',
        eFormJqueryMaskedinput: 'libs/eForm/jquery/jquery.maskedinput-1.3.min',
        eFormJqueryMeioMask: 'libs/eForm/jquery/jquery.meio.mask',

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

        //mudim
        mudim: 'libs/mudim-minorfixed',
        livejs: 'libs/live',

        // Views -------------------------------
        homeView: 'dev/egov.home',

        tabView: 'dev/egov.tab',
        toolbarView: 'views/home/toolbar',
        treeView: 'dev/egov.tree',
        // documentsView: 'views/home/documents',
        documentsView: 'dev/egov.documents',
        documentContextMenu: "dev/egov.documents.contextmenu",
        contextMenuView: 'views/home/contextmenu',
        newStorePrivateView: 'views/home/createNewPrivateStore',
        addStoreAttachment: 'views/home/addStoreAttachment',

        documentView: 'dev/egov.document',
        documentToolbar: 'dev/egov.document.toolbar',
        dynamicForm: 'views/document/form/dynamic',
        htmlForm: 'views/document/form/html',
        docRelation: 'dev/egov.document.relation',
        docAttachment: 'dev/egov.document.attachment',
        transfer: 'dev/egov.document.transfer',
        transferExtend: 'views/document/functions/transferExtend',
        scanner: 'views/document/scanner',
        publishmentView: 'views/document/functions/publishment',
        privatePublishmentView: 'views/document/functions/privatePublishment',
        announcementView: 'views/document/functions/announcement',
        consultView: 'views/document/functions/consult',
        docStoreView: 'views/document/functions/storePrivate',

        documentDetail: 'views/home/document-detail',

        search: 'views/search/search',

        //common
        text: 'libs/require/text',
        editor: 'libs/ckeditor/ckeditor',
        //alohaConfig: '../aloha/demo/boilerplate/js/aloha-config',
        alohaConfig: '../aloha/lib/aloha-config',
        aloha: '../aloha/lib/aloha-full',//'../aloha/lib/aloha-modify-min'
        knockout: 'libs/knockout/knockout-2.2.0',
        resource: 'resource/egov.resources',
        newResourcesVN: 'resource/egov.newResources.VN',
        newResourcesLaos: 'resource/egov.newResources.Laos',
        string: 'libs/string',
        hashBase64: 'libs/hashbase64',
        modernizr: 'libs/modernizr',

        // egov new
        egovUtil: "dev/egov.utils",
        egovEnum: "dev/egov.enum",
        egovEvents: "dev/egov.events",
        egovPubSub: "dev/egov.pubsub",
        egovMessage: "dev/egov.message",
        egovCacheManager: "dev/egov.cache-manager",
        egovRequest: "dev/egov.request-manager",
        egovEntities: "dev/egov.entities",
        egovEntitiesSync: "dev/egov.entities.sync",
        egovDataAccess: "dev/egov.data-access",
        egovModels: "dev/egov.models",

        egovDataManager: "dev/egov.data-manager",
        "egovDataManager.tree": "dev/egov.data-manager.tree",
        "egovDataManager.documents": "dev/egov.data-manager.documents",
        "egovDataManager.document": "dev/egov.data-manager.document",

        egov: "dev/egov"
    },

    // Khai báo sự phụ thuộc của các thư viện với nhau.
    // 'layout': ['jquery'] => 'layout' là tên thư viện được khai báo trong paths ở trên.
    //                      => ['jquery'] là thư viện cần load và excuted trước khi tải 'layout'
    shim: {
        //jquery
        jqueryUI: ['jquery'],
        cookie: ['jquery'],
        nicescroll: ['jquery'],
        tmpl: ['jquery'],
        tooltip: ['jquery'],
        layout: ['jquery', 'jqueryUI'],
        globalize: ['jquery'],
        globalculture: ['jquery', 'globalize'],
        qtip: ['jquery'],
        fileupload: ['jquery', 'jquery.ui.widget'],
        alohaConfig: ['jquery'],
        aloha: ['jquery'],
        //backbone vs underscore
        underscore: ['jquery'],
        backbone: ['jquery', 'underscore'],
        bootstrap: ['jquery'],

        //bkav
        grid: ['jquery'],
        tooltip: ['jquery', 'qtip'],
        egovCookie: ['jquery'],
        dialog: ['jquery'],
        eFormBkavEgate: ['jquery', 'knockout'],
        eFormBkavEgateCompiler: ['jquery', 'knockout', 'eFormBkavEgate'],

        //util
        eFormJqueryGlobalViVN: ['jquery', 'eFormJqueryGlobal'],
        transferExtend: ['modal'],

        // eGov
        egovPubSub: ["egovEvents"],
        egovRequest: ["underscore"],
        egovEntities: ["egovRequest"],
        egovEntitiesSync: ["egovEntities"],
        egovMessage: ["jquery", "jqueryUI", "egovPubSub"],
        egovCacheManager: ["modernizr"],
        egovDataAccess: ["egovCacheManager", "egovEntities"],
        egovDataManager: ["egovDataAccess", "egovModels"],
        "egovDataManager.tree": ["egovDataManager"],
        "egovDataManager.documents": ["egovDataManager"],
        "egovDataManager.document": ["egovDataManager"],
        egovUtil:["jquery"],
        homeView: ["egovDataManager.tree", "egovDataManager.document", "egovDataManager.documents"],

        egov: ["egovEntities", "egovEntitiesSync", "egovPubSub", "egovMessage", "egovDataAccess", "egovModels", "egovDataManager"]
    }
});