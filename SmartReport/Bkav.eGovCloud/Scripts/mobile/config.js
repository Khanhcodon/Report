/// <summary>Khai báo đường dẫn các thư viện của hệ thống cho RequireJS.</summary>
requirejs.config({
    // Đường dẫn gốc, các đường dẫn khác trong paths sẽ xuất phát từ đường dẫn gốc này
    baseUrl: '../../Scripts/mobile/',

    // Tienbv: thay đổi chút về cách đánh phiên bản
    //     - file1main.js luôn luôn tải về bản mới nhất.
    //     - các file khác phụ thuộc vào phiên bản được đánh này, khi sửa file js thì đánh lại phiên bản cho nó là dc. Như thế ko phải lúc nào f5 cũng load về cái mới nhất
    // Todos: về lâu dài sẽ tính cho từng file, file nào thay đổi thì đánh version lại cho file đó thôi
    urlArgs: "v=3efscsdfssws",

    // Khai báo tên và các đường dẫn tới thư viện js tương ứng
    paths: {
        //===========================================

        vendors: 'mobile.min',
        timeago: '../bkav.egov/libs/jquery/jquery.timeago',

        templateList: "../mobile/templates/templates.min",
        resource: '../bkav.egov/resource/egov.resources.bindresource.min',

        //common
        text: '../bkav.egov/libs/require/text.min',

        // jsonform 
        jsonform: "../bkav.egov/libs/jsonform/jsonform",
        formTemplate: 'views/template_form',

        tabView: '../bkav.egov/views/home/tab',
        mobiScroll: '../bkav.egov/libs/mobiscroll/mobiscroll.jquery.lite.min',

        //Mobile
        //jqueryMobile: 'libs/jquery.mobile-1.4.5.modified.min',
        main: '../mobile/main',
        mobileConfig: '../mobile/views/config.min',
        multiselection: '../mobile/egov/multiselection.min',

        documentsView: 'views/documents',
        documentView: 'views/document_mobi',
        transferMobile: '../mobile/views/transfer_mobi',
        docAttachment: 'views/attachments',
        searchDocument: 'views/search',

        notifyMobile: '../mobile/notify/notification_mobile',
        calendarMobile: '../mobile/calendar/calendar',

        material: '../mobile/libs/mdl/material',
        customMaterial: '../mobile/extends/customMaterial.min',

        bmailView: '../mobile/bmail/bmail',
        bmailViewDetail: '../mobile/bmail/views/detail',
        bmailViewToolbar: '../mobile/bmail/views/toolbar',
        bmailViewMultiSelectionBar: '../mobile/bmail/views/multiselection.min',
        bmailDetailToolbar: '../mobile/bmail/views/toolbar.min',
        bmailDetail: '../mobile/bmail/views/ViewMail.min',
        bmailCreateOrReply: '../mobile/bmail/views/create-reply',
        searchBmail: '../mobile/bmail/views/search',

        bmailAttributesConstant: '../mobile/bmail/controller/AttributesConstant.min',
        bmailRequest: '../mobile/bmail/controller/SOAPRequest',
        bmailResponse: '../mobile/bmail/controller/SOAPResponse',
        bmailmakeRequest: '../mobile/bmail/controller/request-manager',
        bmailEvent: '../mobile/bmail/controller/PageEvents.min',
        bmailUpclick: '../mobile/bmail/controller/upclick.min',
        bmailUtil: '../mobile/bmail/util/Util.min',
        bmailCache: '../mobile/bmail/util/Cache.min',
        bmailMsgUtil: '../mobile/bmail/util/MsgUtil',

        bmailDataInit: '../mobile/bmail/model/dataInit',
        bmailModel: '../mobile/bmail/model/model.min',
        bmailModelFolder: '../mobile/bmail/model/folder.min',
        bmailModelList: '../mobile/bmail/model/mail-list.min',
        bmailModelDetail: '../mobile/bmail/model/detail.min',
        bmailCustomToolbar: '../mobile/bmail/model/toolbar.min',

        chatViewMain: "../mobile/chat/chat_mobile",
        charViewManager: "../btalk/app/views/btalk.home-mobile",
        chatRoster: '../btalk/app/views/btalk.roster',
        chatDepartmentView: '../btalk/app/views/btalk.view.departments',
        chatHistoryView: '../btalk/app/views/btalk.view.histories',
        chatSearch: '../btalk/app/views/btalk.view.search',

        chatterView: '../btalk/app/views/btalk.view.chatter',
        messageView: '../btalk/app/views/btalk.view.messages',

        chatterModel: '../btalk/app/models/btalk.model.chatter',
        chatterMessageModel: '../btalk/app/models/btalk.model.message',
        
        btalkCore: "../btalk/btalk-core",
        chatConfig: "../btalk/app/egov.config",
        jsjac: "../btalk/jsjac"
    },  

    shim: {
        timeago: ['vendors'],
        mobiScroll: ['vendors'],     
        //mobile
        bmailmakeRequest: ['bmailRequest', 'bmailResponse'],
        bmailResponse: ['bmailAttributesConstant'],
        bmailDataInit: ['bmailUtil'],

        bmailEditorControls: ['bmailEditorEditorControl'],
        bmailEditor: ['yui', 'bmailEditorlanguage', 'bmailEditorConfig', 'bmailEditorMenuButton', 'bmailEditorEditorControl', 'bmailEditorControls'],


        charViewManager: ['chatterModel', 'chatterMessageModel'],
        btalkCore: ['jsjac'],
        chatRoster: ['btalkCore'],
        chatView: ['btalkCore'],
        chatterView: ['btalkCore', 'chatterModel'],
    },

    mobileIgnores: ['dotdotdot', 'spellChecker'],

    waitSeconds: 120,

    staticPath: [
        '/aloha/',
        '/libs/'
    ],
});