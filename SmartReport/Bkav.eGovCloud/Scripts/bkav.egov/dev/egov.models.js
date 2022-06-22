(function (egov) {

    egov.models = egov.models || {};
    egov.viewModels = egov.viewModels || {};

    //#region Document

    egov.models.document = Backbone.Model.extend({
        defaults: {
            Address: '',
            CategoryBusinessId: 0,
            CategoryId: 0,
            CitizenInfo: null,
            CitizenName: null,
            Color: 0,
            Comment: "",
            Comments: {},
            Compendium: "",
            Compendium2: "",
            DateAppointed: "",
            DateArrived: null,
            DateCreated: new Date(),
            DateFinished: null,
            DateModified: "",
            DateOfIssueCode: null,
            DatePublished: null,
            DateReceived: "",
            DateReceivedFormat: "",
            DateResponse: null,
            DateResponsed: null,
            DateResponsedOverdue: null,
            DateResult: "",
            DateReturned: null,
            DateSuccess: null,
            DocCode: null,
            DocFieldId: null,
            DocFieldIds: null,
            DocType: {},
            DocTypeId: "",
            DocTypePermission: 0,
            DocumentCopyId: 0,
            DocumentId: "00000000-0000-0000-0000-000000000000",
            Email: null,
            IdentityCard: null,
            InOutCode: null,
            InOutPlace: "",
            IsAcknowledged: 0,
            IsConverted: 0,
            IsDocumentImportant: 0,
            IsGettingOut: 0,
            IsReturned: null,
            IsSuccess: null,
            IsSupplemented: null,
            IsViewed: 0,
            Keyword: null,
            LastComment: "",
            LastUserIdComment: 0,
            Organization: null,
            Original: 1,
            Phone: null,
            ProcessedMinutes: null,
            ResultStatus: null,
            ReturnNote: null,
            SecurityId: null,
            SendTypeId: null,
            Status: 0,
            StoreId: null,
            SuccessNote: null,
            TotalPage: null,
            UrgentId: 1,
            UserCurrentId: 0,
            UserCreatedId: 0,
            UserCurrentFirstName: "",
            UserCurrentFullName: "",
            UserReturnedId: null,
            UserSuccessId: null,
            Selected: false,
            DocumentCopyType: 0,
            DocCopyStatus: 0,
            DateOverdue: '',
            NumberDayOverdue: '0',    //hạn giữ
            NumberDayAppointed: '0',  //Hạn tổng
            IsFile: 0,                 //Có phải là file hay không(dùng trong sổ hồ sơ):Mặc định là 0(không phải file)
            WorkflowId: 0,            //Quy trình của văn bản
            NodeCurrentId: 0,          //Node hiện tại của văn bản trên quy trình
            DocCopyDateModified: null,
            WorkflowTypes: "",
            WorkflowTypeId: "",
            WorkflowTypeName: "",
            Note: ""
        },

        initialize: function () {
            this.set('id', this.get('DocumentCopyId'));
        }
    });

    egov.models.documentList = Backbone.Collection.extend({
        model: egov.models.document
    });

    //#endregion

    //#region Document Permission

    egov.models.documentPermission = Backbone.Model.extend({
        defaults: { value: 0 }
    });

    egov.models.documentPermissionList = Backbone.Collection.extend({
        model: egov.models.documentPermission
    });

    //#endregion

    //#region Documents Toolbar

    egov.models.toolbar = Backbone.Model.extend({
        defaults: {
            text: '',
            className: '',
            disable: false,
            icon: '',
            dataUrl: '',
            shortKey: '',
            data: null,
            callback: null,
            dropdownWidth: 90,
            dropdownHeight: 0,
            position: {
                at: 'right bottom',
                my: 'right top'
            },
            contentId: '',
            isDatePicker: false,
            isDropdownMenu: false,
            hasShortKey: false,
            showSelected: false,
            defaultSelectedText: 'Tất cả'
        }
    });

    egov.models.toolbarList = Backbone.Collection.extend({
        model: egov.models.toolbar
    });

    //#endregion

    //#region Context Menu

    egov.models.contextMenu = Backbone.Model.extend({
        defaults: {
            selector: null,
            trigger: 'right',   // 'left'
            dataUrl: '',        // Url lấy dữ liệu (nếu có)
            param: '',          // Param cho url
            data: null,         // Danh sách các item là thể hiện của collection egov.models.ContextMenuItemModel
            callback: null,     // Hàm thực thi khi select trước thi thực thi hàm callback trong data
            style: {},
            position: {},
            isDatePicker: false, // Đặt true nếu muốn thể hiện nội dung xổ ra là datatimepicker
            // Hiển thị loading trước rồi bind dữ liệu sau
            // ví dụ: var context = selector.contextmenu({isShowLoading: true});
            // context.model.set('data', data);
            // context.render();
            isShowLoading: false,
            key: null
        },

        initialize: function () {
            var style = this.get('style');
            if (style.height === 0 || style.height === undefined) {
                style.height = 'auto';
            }

            this.set('style', $.extend({}, {
                display: 'none'
            }, style));

            this.set('position', $.extend({}, {
                at: 'right bottom',
                my: 'right top'
            }, this.get('position')));
        }
    });

    egov.models.contextMenuItem = Backbone.Model.extend({
        defaults: {
            text: '',
            value: '',
            callback: '',
            icon: '',
            selected: false
        },

        initialize: function () {
            var rootIconFolder = '/Content/Images/Toolbar/';
            if (this.get('icon') !== '') {
                this.set('icon', rootIconFolder + this.get('icon'));
            }
        }
    });

    egov.models.contextMenuList = Backbone.Collection.extend({
        model: egov.models.contextMenuItem
    });

    //#endregion

    //#region Tree

    var showTotalInTreeType = {
        none: 0,         //Không hiển thị
        unread: 1,       //Văn bản chưa đọc
        unreadOnAll: 2,  //Chưa đọc / Tất cả
        all: 3           //Tất cả
    };

    egov.models.TreeModel = Backbone.Model.extend({
        defaults: {
            functionId: 0,
            parentId: 0,
            name: "",
            params: "",
            paramId: 0,
            icon: "",
            state: "closed",
            order: 0,
            totalDocumentUnread: 0,
            totalDocument: 0,
            children: [],
            url: "",
            pagingUrl: "",
            isLoadChildren: false,
            showTotalInTreeType: showTotalInTreeType.unread,
            defaultSort: null,
            hasUyQuyen: false,
            userUyQuyen: null,
            isOpen: false,
            isSelected: false,
            hasTransferTheoLo: false,
            isOnlineRegistration: false,
            treeGroupId: null,
            treeGroupOrder: 0,
            hasExportFile: false
        },

        initialize: function () {
            this.set('id', this.get('functionId'));
            var url = '/Home/GetDocuments/' + this.get('functionId');
            var pagingUrl = '/Home/GetDocumentPaging/' + this.get('functionId');
            if (typeof this.get('params') === 'object') {
                this.set('params', JSON.stringify(this.get('params')));
            }
            if (this.get('params') !== '') {
                url += '?params=' + this.get('params');
                pagingUrl += '?params=' + this.get('params');
            }
            this.set('url', url);
            this.set('pagingUrl', pagingUrl);
            this.set('isLoadChildren', false);

            //  this.set("children", new egov.models.TreeList(this.get("children") ? this.get("children") : []));

            if (typeof this.get('defaultSort') === 'object') {
                this.set('defaultSort', JSON.stringify(this.get('defaultSort')));
            }

            if (typeof this.get('userUyQuyen') === 'string') {
                this.set('userUyQuyen', JSON.parse(this.get('userUyQuyen')));
            }
        }
    });

    egov.models.TreeList = Backbone.Collection.extend({
        model: egov.models.TreeModel
    });

    egov.models.StorePrivateModel = Backbone.Model.extend({
        defaults: {
            level: 0,
            parentId: 0,
            status: 0,
            storePrivateId: 0,
            storePrivateName: "",
            name: "",
            children: [],
            state: "closed",
            url: "",
            pagingUrl: "",
            root: false,
            isStoreShared: false,
            descStorePrivate: "",
            userIdJoined: [],
            deptExtJoined: []
        },

        initialize: function () {
            var pagingUrl = '/StorePrivate/GetDocuments/' + this.get('storePrivateId');
            this.set('id', this.get('storePrivateId'));
            this.set('pagingUrl', pagingUrl);

            this.set("children", new egov.models.StorePrivateList(this.get("children") ? this.get("children") : []));
        }
    });

    egov.models.StorePrivateList = Backbone.Collection.extend({
        model: egov.models.StorePrivateModel
    });

    //#endregion

    //#region Question

    egov.models.question = Backbone.Model.extend({
        defaults: {
            QuestionId: 0,
            Date: "",
            AskPeople: "",
            Detail: "",
            Name: "",
            Email: "",
            Phone: "",
            DocCode: "",
            QuestionType: 0,
            IsGeneralQuestion: true,
            UserComments: [],
            AnswerHolder: null,
            isMe: false,
            Compendium: "",
            DocumentHolderName: "",
            DocumentHolderAccount: "",
            DocumentHolderFullAccount: "",
        },

        initialize: function () {
        }
    });

    egov.models.questionList = Backbone.Collection.extend({
        model: egov.models.question
    });

    egov.models.QuestionTreeModel = Backbone.Model.extend({
        defaults: {
            level: 0,
            status: 0,
            name: "",
            children: [],
            state: "closed",
            root: false,
            isGeneral: false
        },

        initialize: function () {
            this.set("children", new egov.models.QuestionTreeList(this.get("children") ? this.get("children") : []));
        }
    });

    egov.models.QuestionTreeList = Backbone.Collection.extend({
        model: egov.models.QuestionTreeModel
    });

    //#endregion

    //#region Tabs

    egov.models.TabModel = Backbone.Model.extend({
        defaults: {
            id: 0,
            name: '',
            title: '',
            href: '',
            hasTooltip: true,
            hasCloseButton: false,
            isRoot: false,
            privateId: 0,
            //  isCookie: false,
            attributes: {},
            isCreateDocument: false,
            type: 0,
            cateBusId: 0,
            hasLoadContent: true
        }
    });

    egov.models.TabList = Backbone.Collection.extend({
        model: egov.models.TabModel
    });

    //#endregion

    //#region Transfer

    egov.models.action = Backbone.Model.extend({
        defaults: {
            currentNodeId: 0,
            id: "",
            isAllow: true,
            isAllowSign: false,
            isSpecial: false,
            name: "",
            nextNodeId: 0,
            priority: 0,
            userIdNext: 0,
            workflowId: 0
        }
    });

    egov.models.userAction = Backbone.Model.extend({
        defaults: {
            id: 0,
            value: 0,
            label: '',
            fullname: '',
            department: '',
            username: '',
            position: '',
            isMainProcess: false,
            isCoProcess: false
        },

        initialize: function () {
            this.set('id', this.get('value'));
        }
    });

    egov.models.actionUserList = Backbone.Collection.extend({
        model: egov.models.userAction
    });

    //#endregion

    //#region Attachment

    egov.models.attachment = Backbone.Model.extend({
        defaults: {
            Id: 0,
            Name: '',
            Extension: '',
            Size: 0,
            Versions: [],
            fileData: undefined,
            isRemoved: false,
            isNew: false,
            isMofified: false,
            isOpen: false,
            icon: ''
        },

        initialize: function () {
            var extension,
                icon;

            extension = this.get('Extension');
            if (extension.indexOf('.') !== 0) {
                extension = "." + extension;
            }

            switch (extension) {
                case '.doc':
                case '.docx':
                    icon = 'icon-file-word';
                    break;
                case '.xls':
                case '.xlsx':
                    icon = 'icon-file-excel';
                    break;
                case '.pdf':
                    icon = 'icon-file-pdf';
                    break;
                case '.txt':
                    icon = 'icon-text';
                    break;
                case '.zip':
                case '.rar':
                case '.7z':
                    icon = 'icon-file-zip';
                    break;
                case '.ppt':
                case '.pptx':
                    icon = 'icon-file-powerpoint';
                    break;
                case '.html':
                    icon = 'icon-chrome';
                    break;
                case '.jpg':
                case '.jpeg':
                case '.bmp':
                case '.png':
                case '.ico':
                case '.gif':
                    icon = 'icon-image2';
                    break;
                default:
                    icon = 'icon-file4';
                    break;
            }

            this.set('icon', icon);
        }
    });

    egov.models.attachmentList = Backbone.Collection.extend({
        model: egov.models.attachment
    });

    //#endregion

    //#region Relation

    egov.models.relation = Backbone.Model.extend({
        defaults: {
            id: 0,
            RelationCopyId: 0,
            RelationId: '',
            RelationType: 0,
            IsAddNext: false,
            Compendium: '',
            CitizenName: '',
            DocCode: '',
            DateCreated: '',
            CategoryName: '',
            IsRemoved: false,
            IsNew: false
        },

        initialize: function () {
            // Thiết lập id để tránh gán trùng relation id
            this.set('id', this.get('RelationCopyId'));
        }
    });

    egov.models.relationList = Backbone.Collection.extend({
        model: egov.models.relation
    });

    //#endregion

    //#region QuickView

    egov.models.quickView = Backbone.Model.extend({
        defaults: {
            id: 0,
            type: 1,
            compendium: null,
            lastComment: null,
            category: null,
            department: null,
            dateCreate: null,
            lastUser: null,
            docField: null,
            urgent: null,
            docCode: null,
            totalPage: null,

            ///online
            docType: null,
            dateReceived: null,
            DateReceivedFormat: "",
            dateAppoint: null,
            personInfo: null,
            email: null,
            phone: null,
            address: null,
        },
        initialize: function () {

        }
    });

    egov.models.quickViewList = Backbone.Collection.extend({

        model: egov.models.quickView,

        initialize: function () {
        }
    });

    //#endregion

    //#region Publish

    egov.models.address = Backbone.Model.extend({
        defaults: {
            IsShow: false,
            ParentId: null
        },
        initialize: function () {
            // Set các trường cho autocomplete
            this.set('id', this.get('AddressId'));
            this.set('value', this.get('AddressId'));
            this.set('label', this.get('Name'));
            this.set('isSelected', false);
        }
    });

    egov.models.addressCollection = Backbone.Collection.extend({
        model: egov.models.address
    });

    egov.models.publish = Backbone.Model.extend({
        defaults: {
            StoreId: 0,
            CodeId: 0,
            Code: '',
            DatePublished: new Date(),
            DateResponse: null,
            TotalPage: 1,
            SecurityId: 1,
            TotalCopy: 1,
            Approvers: '',
            KeyWordId: 0,
            InPlace: 0,
            IsCustomCode: false,
            Address: []
        }
    });

    //#endregion

    //#region Supplementary

    egov.models.supplementary = Backbone.Model.extend({
        SupplementaryId: 0,
        Comment: "",
        DateSend: "",
        IsDeleted: false,
        UserSendId: 0,
        UserSendName: ""
    });

    egov.models.supplementaryList = Backbone.Collection.extend({
        model: egov.models.supplementary
    });

    //#endregion

    //#region Search

    egov.models.search = Backbone.Model.extend({
        defaults: {
            Compendium: '',
            CategoryId: null,
            KeyWord: '',
            Content: '',
            DocCode: '',
            InOutCode: '',
            UrgentId: null,
            CategoryBusinessId: null,
            StorePrivateId: null,
            CurrentUserId: null,
            InOutPlace: '',
            FromDateStr: '',
            ToDateStr: '',
            BeforeDate: '',
            AfterDate: '',
            OrganizationCreate: '',
            DocFieldId: null,
            UserSuccessId: null
        }
    });

    egov.models.searchResultItem = Backbone.Model.extend({
        defaults: {
            Address: '',
            CategoryName: '',
            CitizenName: '',
            Compendium: '',
            DateAppointed: '',
            DateArrived: '',
            DateCreated: '',
            DocCode: '',
            DocumentCopyId: 0,
            DocumentId: '',
            InOutCode: 0,
            LastUserComment: '',
            UserSuccess: '',
            DateReceived: '',
            IsSelected: false
        }
    });

    egov.models.searchResult = Backbone.Collection.extend({
        model: egov.models.searchResultItem
    });

    //#endregion

    //#region Modal

    egov.models.modal = Backbone.Model.extend({
        defaults: {
            title: '',  // Tiêu đề modal
            keyboard: false, // Ẩn modal khi nhân esc
            resizable: false, // cho phép thay đổi kích thước
            draggable: false, // cho phép kéo thả vị trí
            animation: true, // hiển thị dạng fadein
            remote: '', // url nội dung modal - dùng để load nội dung modal sau theo url
            content: '', // nội dung modal - html có sẵn
            height: 'auto', // chiều cao
            width: 'auto', // chiều rộng,
            ignoreText: '',
            buttons: [], // các nút chức năng
            hide: null,   // callback sau khi ẩn modal
            close: null,   // callback trước khi ẩn modal 
            loaded: null,   // callback sau khi load xong nội dung = remote url.
            backdrop: "static",
            confirm: null
        }
    });

    //#endregion

    //#region Document Permission

    egov.models.totalNotifications = Backbone.Model.extend({
        defaults: {
            totalNotify: 0,
            total: 0
        }
    });

    egov.models.notification = Backbone.Model.extend({
        defaults: {
            NotificationId: 0,
            NotificationType: 0,
            Title: "",
            Content: "",
            SenderAvatar: "",
            SenderUserName: "",
            SenderFullName: "",
            Date: "",
            DateFormat: "",
            ReceiveDate: "",
            ViewdDate: "",
            IsViewed: false,

            //egov
            DocumentCopyId: 0,

            //mail
            MailId: 0,
            folderId: 0,

            //chat
            ChatId: "",
            chatterJid: "",
            messageId: ""
        }
    });

    egov.models.notificationList = Backbone.Collection.extend({
        model: egov.models.notification
    });

    egov.models.layoutNotify = Backbone.Model.extend({
        defaults: {
            total: 0,
            unreadTotal: 0,
            model: []
        }
    });

    //#endregion

    //#region Gán ra model cho các view. Mỗi view chỉ sử dụng một model tương ứng.

    // Cây văn bản
    egov.viewModels.tree = new egov.models.TreeList();

    // Danh sách văn bản
    egov.viewModels.documentList = new egov.models.documentList();

    // Danh sách tab
    egov.viewModels.tabList = new egov.models.TabList();

    // Danh sách người nhận trên form bàn giao
    egov.viewModels.actionUserList = new egov.models.actionUserList();

    //#endregion

})
(this.egov = this.egov || {});
