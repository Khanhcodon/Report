(function (egov) {

    // Quản lý các đối tượng dữ liệu lưu ở client
    // {
    //    name: "userConfig",  => Tên đối tượng và là tên key lưu trong cache.
    //    hasCache: false,     => Giá trị xác định đối tượng dữ liệu có lưu cache offline dưới client hay không.
    //    hasSessionCache: true, => Giá trị xác định đối tượng dữ liệu có lưu lại theo phiên hay không.
    //    request: egov.request.getCommonConfigs, => trong egov.request-manager.js; set null để tạo đối tượng thuần dưới client
    //    id: 0,                                  => truyền vào Id để phân biệt các đối tượng lưu có cùng request: 
    //                                                  userConfig_1, userConfig_2,...
    //    isReplaceWhenSync: true,                => Giá trị xác định replace lại kết khi đồng bộ kết quả về hay merge với 
    //                                                  giá trị cũ
    //    isInsertToCurrentCache:true => Trong trường hợp không query lên server lấy toàn bộ 1 lượt dữ liệu (như allUsers) mà lấy lần lượt 
    //                                          dữ liệu (như template của văn bản, lúc nào mở ra mới lấy), giá trị này xác định 
    //                                          mỗi khi lấy 1 lần thì có insert thêm vào dữ liệu dưới client hay replace toàn bộ
    //                                    => Dữ liệu sẽ lưu dưới dạng 1 mảng, mỗi khi lấy dữ liệu mới sẽ insert   
    //    userId:egov.setting.userId => Trường hợp dữ liệu chỉ của 1 người dùng, người khác đăng nhập vào cùng 1 máy sẽ xóa dữ liệu của entity này, lấy dữ liệu mới về
    //                                  =>Khi đó dữ liệu sẽ lưu bằng 1 object : {userId: Id người dùng hiện tại, data: dữ liệu của riêng người dùng này}
    //    option: {                               => jquery ajax option mặc định.
    //        beforeSent: function () {
    //            egov.pubsub.public(egov.events.status, {
    //                type: "processing",
    //                message: "Đang tải"
    //            });
    //        }
    //    }
    // }

    egov.entities = {

        //#region common

        currentDoctypes: {
            name: "currentDoctypes",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocTypes,
            option: {
            }
        },

        categories: {
            name: "categories",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getCategories,
            option: {
            }
        },

        currentDepartments: {
            name: "currentDepartments",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDepartmentsByUser,
            option: {
            }
        },

        getDepartmentsCurrent: {
            name: "getDepartmentsCurrent",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDepartmentsCurrent,
            option: {
            }
        },

        allDept: {
            name: "allDept",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllDepartment,
            option: {
            }
        },

        allJobtitle: {
            name: "allJobtitle",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllJobTitlies,
            option: {
            }
        },
        allPosition: {
            name: "allPosition",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllPosition,
            option: {
            }
        },
        allUsers: {
            name: "allUsers",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllUsers,
            option: {
            }
        },

        allUserDeptPosition: {
            name: "allUserDeptPosition",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllUserDepartmentJobTitlesPosition,
            option: {
            }
        },

        getDeptAndUsers: {
            name: "getDeptAndUsers",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDeptAndUsers,
            option: {
            }
        },

        allAddress: {
            name: "allAddress",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getAllAddress,
            option: {
            }
        },

        allKeyword: {
            name: "allKeyword",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getKeywords,
            option: {
            }
        },

        allDocField: {
            name: "allDocField",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocField,
            option: {
            }
        },

        allSendType: {
            name: "allSendType",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getSendTypes,
            option: {
            }
        },

        userConfig: {
            name: "userConfig",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getCommonConfigs,
            option: {
                beforeSent: function () {
                    egov.pubsub.public(egov.events.status, {
                        type: "processing",
                        message: "Đang tải"
                    });
                }
            }
        },

        allCommonComments: {
            name: "allCommonComments",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getCommonComments,
            option: {
            }
        },

        allTemplateComments: {
            name: "allTemplateComments",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getTemplateComments,
            option: {
            }
        },

        currentUserId: {
            name: "currentUserId",
            hasCache: true,
            hasSessionCache: true,
            option: {
            }
        },

        getCommonConfigs: {
            name: "getCommonConfigs",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getCommonConfigs,
            option: {
            }
        },

        //#endregion

        //#region tree

        documentTree: {
            name: "documentTree",
            hasCache: true,
            request: egov.request.getDocumentTree,
            hasSessionCache: true,
            option: {
                beforeSent: function () {
                    egov.pubsub.public(egov.events.status, {
                        type: "processing",
                        message: "Đang tải"
                    });
                }
            }
        },

        //#endregion

        //#region documents

        documentStore: {
            name: "documentStore",
            id: null,
            hasCache: true,
            hasSessionCache: true,
            isReplaceWhenSync: false,
            request: egov.request.getDocumentStore,
            option: {}
        },

        getDocumentPermission: {
            name: "getDocumentPermission",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentPermission,
            option: {}
        },

        document_Store: {
            name: "document-functionStore",
            hasCache: false,
            hasSessionCache: true,
            request: null,
            option: {}
        },

        documents: {
            name: "documents",
            hasCache: true,
            hasSessionCache: true,
            isReplaceWhenSync: false,
            request: egov.request.getDocuments,
            option: {}
        },

        documentsReport: {
            name: "documents",
            hasCache: true,
            hasSessionCache: true,
            isReplaceWhenSync: false,
            request: egov.request.getReports,
            option: {}
        },

        tempDocuments: {
            name: "tempDocuments",
            hasCache: false,
            hasSessionCache: false,
            isReplaceWhenSync: true,
            request: egov.request.getDocuments,
            option: {}
        },

        //#endregion

        //#region Function Group

        functionGroups: {
            name: "functionGroups",
            id: null,
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getFunctionGroups,
            option: {}
        },

        //#endregion

        //#region document

        documentTemplate: {
            name: "documentTemplate",
            id: null,
            hasCache: true,
            hasSessionCache: true,
            isInsertToCurrentCache: true,
            request: egov.request.getDocumentTemplate,
            option: {
            }
        },

        documentEdit: {
            name: "documentEdit",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentInfoForEdit,
            option: {}
        },

        documentCreate: {
            name: "documentCreate",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentInfoForCreate,
            option: {}
        },

        documentCreateAction: {
            name: "documentCreateAction",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentCreateAction,
            option: {}
        },

        documentEditAction: {
            name: "documentEditAction",
            id: null,
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentEditAction,
            option: {}
        },

        getUserByAction: {
            name: "getUserByAction",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getUserByAction,
            option: {}
        },

        transfer: {
            name: "transfer",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.transfer,
            option: {}
        },

        setPopUpSize: {
            name: "setPopUpSize",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.setPopUpSize,
            option: {}
        },

        getDocumentDetail: {
            name: "getDocumentDetail",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getDocumentDetail,
            option: {}
        },

        filterCitizen: {
            name: "filterCitizen",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.filterCitizen,
            option: {}
        },

        //#endregion

        //#region user store

        getStorePrivate: {
            name: "getStorePrivate",
            id: null,
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getStorePrivate,
            option: {}
        },

        //#endregion


        // #region cây văn bản (trees).

        //Lấy danh sách node con theo id node cha
        trees: {//Lấy danh sách node con ở node gốc
            name: "trees",
            hasCache: true,
            hasSessionCache: true,
            request: egov.request.getDocumentTree,
            option: {},
            expriedDay: 7,
            isPrivate: true
        },

        //#endregion

        //#region Câu hỏi

        getNodeQuestion: {
            name: "getNodeQuestion",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getNodeQuestion,
            option: {}
        },

        //#endregion

        //region sổ hồ sơ (storeTrees)

        getStorePrivateDocuments: {
            name: "getStorePrivateDocuments",
            hasCache: false,
            hasSessionCache: false,
            request: egov.request.getStorePrivateDocuments,
            option: {}
        },

        storeTrees: {//Lấy danh sách node con ở node gốc
            name: "storeTrees",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getStorePrivate,
            option: {}
        },

        //#endregion 

        //region version khi reset
        versionValue: {
            name: "version",
            hasCache: false,
            hasSessionCache: true,
            request: egov.request.getVersionValue,
            option: {}
        },
        //#endregion 
    };

})
(this.egov = this.egov || {});