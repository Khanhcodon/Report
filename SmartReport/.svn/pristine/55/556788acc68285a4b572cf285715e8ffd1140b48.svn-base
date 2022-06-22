(function (window, egov) {
    var extend = function (destination, source) {
        /// <summary>
        /// Hàm extend để thêm resource
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        for (var property in source) {
            if (source[property] && source[property].constructor &&
             source[property].constructor === Object) {
                destination[property] = destination[property] || {};
                arguments.callee(destination[property], source[property]);
            } else {
                destination[property] = source[property];
            }
        }
        return destination;
    };
    egov.resources = {
        document: {
            Compendium: "Compendium",
            Comment: "Comment",
            DocType: "Type of document",
            Category: "Form",
            InOutPlace: "Unit",
            DateAppointed: "Deadline",
            Organization: "Sender's Organization ",
            DocCode: "Document number/code*",
            DocCode2: "Document assigned number*",
            DateArrived: "Arrival date",
            DateResponse: "Response",
            DatePublished: "Publication date",
            StoreId: "Document store",
            InOutCode: "Incoming and outgoing document number ",
            TotalPage: "Total page",
            ChooseTotalPage: "Choose number of page ",
            DocField: "Field",
            Keyword: "Keyword",
            SendType: "Sending type",
            DocCode1: "Document code",
            CitizenName: "Citizen name",
            Address: "Address",
            Phone: "Phone number",
            DocPapers: "Document paper",
            IdentityCard: "Identity card",
            Email: "Email",
            Commune: "Commune",
            AttachmentList: "Attachments",
            RelationList: "Related document",
            cbDetail: "Details of incoming document",
            AllComment: "All comments",
            titleContent: "Document content",
            Urgent: {
                name: "Urgency level",
                normal: "Normal",
                fast: "Fast",
                important: "Express"
            },
            SecurityId: {
                name: "Security level",
                normal: "Normal ",
                high: "High",
                important: "Top secret",
            },
            CompendiumTitle: "Enter compendium",
            NoComment: "No comment",
            DisplayForm: "Display forms",
            StorePrivate: "Private store",
            StoreShare: "Shared store",
            nextPage: "Next page",
            prePage: "Previous page",
            currentPage: "Page 1",
            print: "Print",
            btnFinish: "Finish",
            viewIconTraKetQua: "Results",
            viewIconTiepNhanBoSung: "Additional reception",
            viewIconHuyVanBan: "Cancel",
            viewIconLuu: "Save store",
            viewIconGuiykien: "Send comment",
            viewIconThongbao: "Notify",
            viewIconXinykien: "Ask for comment ",
            viewIconYeuCauBoSung: "Additional request",
            viewIconGiaHanXuLy: "Extend",
            no: "Deny",
            yes: "Agree",
            btnInsertRelation: "Related document...",
            btnInsertAttachment: "File attached ",
            btnInsertScan: "Scan file...",
            btnPaper: "License...",
            btnInsertAnticipate: "Expected transfer ...",
            btnTransfer: "Transfer document/store",
            btnEdit: "Edit document/store ",
            btnInsertFile: "Attach file",
            btnApproverYes: "Agree to approve",
            btnApproverNo: "Deny to approve ",
            btnDestroy: "Destroy document/store C",
            viewIconKetthuc: "",
            btnFinishtt: "Finish",
            btnAnswer: "Answer",
            btnChangeDoctype: "Classify",
            concurrency: "Vnd",
            UserComment: "Processor",
            filename: "File name",
            filesize: "Size",
            fileversion: "Version",
            lastUpdateFile: "Last update",
            FinalComment: "Final comment",
            backtolist: "Back to list",
            "delete": "Delete",
            MainProcess: "Main processor:",
            CoProcess: "Coprocessor:",
            sendTo: "Send to",
            thongbao: "Notify:",
            xinykien: "Ask for comment:",
            view: "View",
            download: "Download"
        },
        documentQuickView: {
            belowDocumentSum: "Summarize",
            Comment: "Comment:",
            timeComment: "at",
            Category: "Category:",
            Docfield: "Field:",
            DocCode: "Document code:",
            Result: "Processing result",
            LastUserComment: "Last processor: ",
            Place: "Place:",
            Sign: "Signer:",
            TotalPage: "Quantity of page:",
        },
        transfer: {
            ChoseOtherUser: "Choose other user",
            MainProcessUser: "Receive original document",
            MainProcessUserComment: "(main processor) ",
            CoProcessUser: "Receive copy",
            CoProcessUserComment: "(co-process)",
            AnnouceUser: "Receive announcement",
            AnnouceUserComment: "( to reference)",
            QuickSearchAccount: "Quick search",
            AnnouncementPlace: "Announcement place",
            PrivateAnoun: "Receive announcement",
            ConsultContent: "Content ",
            ConsultUser: "Consultation user ",
            MainProcess: "Main processor",
            CoProcess: "Coprocessor",
            dgUserLabel: "(Select recipient)",
            dgUser: "Recipient",
            dgJobtitleLabel: "(Select job title and department receiving a copy)",
            dgJobtitle: "Title",
            dgDeptJob: "Department",
            allJobs: "All job titles",
            sameDept: "Same department ",
            isDg1: "Notify",
            isDg2: "CC",
            searchDgLabel: "User receiving a copy",
            allJobTitlesForDept: "All job titles",
            jobtitlesDeptPopup: "Job titles under department (unit)",
            jobtitleForAll: "Update a copy (for reference)",
            allJobTitles: "All job titles",
            IsThongbao: "Notify|",
            IsDxl: "Coprocesssor|",
            IsAttachYk: "Attach consultation comment",
        },
        attachment: {
            view: "View",
            open: "Open",
            del: "Delete"
        },
        storePrivate: {
            attachmentName: "Document:",
            descStorePrivateAttachment: "Description:",
            storePrivateName: "File name:",
            storePrivateNameWarning: "Enter file name",
            userJoined: "Participants:",
            delJoined: "Delete",
            descStorePrivate: "Note:",
            storePrivateName: "",
            storePrivateName: "",
        },
        relation: {
            open: "Open",
            del: "Delete",
            view: "Information"
        },
        toolbar: {
            transferBtn: "Transfer",
            editBtn: "Edit",
            attachBtn: "Attach",
            relation: "Related document...",
            attachment: "File attached",
            scan: "Scan file...",
            packet: "Packet process ",
            paper: "License...",
            DuKienChuyen: "Expected transfer...",
            reloadBtn: "Reload",
            allow: "Agree",
            deny: "Deny",
            destroy: "Agree",
            TiepNhanBoSung: "Additional reception",
            TraKetQua: "Return result ",
            finish: "Finish",
            reply: "Reply",
            PhanLoai: "Classify",
            print: "Print",
            other: "Other",
            GiaHan: "Extend",
            YeuCauBoSung: "Additional request...",
            XinYKien: "Ask for comment...",
            btnAnnouncement: "Notify...",
            btnSendAnswer: "Send answer...",
            btnSaveStore: "Save store.."
        },
        main: {
            news: "Operation news",
            newEmail: "Compose",
            config: "Configuration",
            reply: "Reply",
            smallSize: "View at small size",
            mediumSize: "View at medium size",
            largeSize: "View at large size",
            underPreview: "Below preview",
            rightPreview: "Preview on the right",
            hidePreview: "Hide preview",
            reload: "Reload",
            logout: "Log out",
            searchDocument: "Search document",
            searchFile: "Search attached file",
            reloadMessage: "Update successfully. Do you want to reload page?",
            closeBtn: "Close",
            submitBtn: "Update",
            titleMessage: "Notify!",
            closeAll: "Close all",
            report: "Statistics",
            contacts: "Contacts",
            calendar: "Calendar",
            chat: "Chat",
            documents: "Process document",
            bmail: "Email",
            notifications: "Notifications",
            gtv: "Type in Vietnamese "
        },
        index: {
            storePrivate: "Work profile",
            plugin: "Application",
            reportNode: "Statistic report",
            printNode: "Quick print",
            //reportNode: "Statistic report",
            reload: "Synchronize"
        },
        setting: {
            title: "Private settings",
            ProfileConfig: "Private information",
            Changepassword: "Change password",
            UserSetting: "Keyboard shortcut config",
            GeneralSettings: "Other config",
            Signature: "Signature config",
            btnUpdateSetting: "Update",
            btnCloseSetting: "Close",
        },
        scan: {
            rotateLeft: "Rotate left",
            rotateRight: "Rotate right",
            zoomIn: "Zoom in",
            zoomOut: "Zoom out",
            setActualSize: "Original image",
            crop: "Crop image ",
            setBrightnessUp: "Brightness up",
            setBrightnessDown: "Brightness down",
            setContrastUp: "Contrast up",
            setContrastDown: "Contrast down",
            addToContent: "Add to content",
            pagePosition: "Page: 0/0",
            preImage: "Previous image",
            nextImage: "Next image",
            removeImageScan: "Remove",
            removeAllImageScan: "Remove all",
            listScannerLabel: "Select scanner:",
            reloadListScanner: "Reload list of scanner",
            colortype: "Color type",
            pixeltype2: "Color",
            pixeltype0: "Gray",
            pixeltype1: "White black",
            resolution75: "75 dpi",
            resolution100: "100 dpi",
            resolution150: "150 dpi",
            resolution200: "200 dpi",
            resolution300: "300 dpi",
            duplex: "Duplex scan ",
            showui: "Use scanner interface",
            filename: "File name",
            imageformatLabel: "Save file as ",
            imageformatJPEG: "JPEG",
            imageformatPNG: "PNG",
            imageformatGIF: "GIF",
            imageformatTIFF: "TIFF",
            imageformatBMP: "BMP",
            imageformatPDF: "PDF",
            imageformatDOC: "DOC",
            acquire: "Scan image",
            refresh: "Refresh list of scanners ",
            colortype: "Color type",
            color: {
                color: "Color",
                gray: "Gray",
                whiteblack: "Black&white"
            },
        },
        tab: {
            close: "Close tab"
        },
        search: {
            //search.html
            compendium: "Compendium",
            doccode: "Document code",
            inoutcode: "Incoming number",
            content: "Content",
            category: "Category",
            keyword: "Keyword",
            urgent: "Urgency level",
            categorybusiness: {
                name: "Profession",
                all: "All",
                "in": "Incoming document",
                out: "Outgoing document",
                one: "One-door document"
            },
            FromDateStr: "From date",
            InOutPlace: "Sender's organization",
            OrganizationCreate: "Publisher's organization",
            DocField: "Field",
            UserSuccess: "Signer ",
            CurrentUser: "Current holder",
            ToDateStr: "To date",
            showsearch: "Display advanced search",
            createdate: "Creation date",
            createdate1: "Creation date",
            status: "Status",
            //searchDocResult.html, searchResult.html
            status1: "Drafting",
            status2: "Processing",
            status4: "Finished",
            status8: "Cancelled",
            status16: "Stop processing",
            //searchTab.html
            search: "Search",
            order: "No.",
            searchnotfound: "No result",
            view: "View",
            download: "Download",
            DidYouMean: "Did you mean",
            all: "All",
            //search.html
            doccodePh: "Enter document code",
            inoutcodePh: "Enter incoming code",
            contentPh: "Enter content",
            keywordPh: "Enter key word",
            error: "An error occurred while searching. Please contact your network administrator.",
            noresult: "No result found",
            Compendiumph: "Enter compendium",
            openattachmentfile: "Open attached file",
            downloadattachmentfile: "Download attached file",
        }
    }

    //#region version 1.1

    egov.resources.common = {
        processing: "Processing…",
        loading: "Loading...",
        error: "Error",
        searching: "Searching",
        closeButton: "Close",
        addButton: "Add ",
        editButton: "Edit",
        updateButton: "Update",
        cancelButton: "Cancel",
        deleleButton: "Delete",
        confirmButton: "Confirm",
        alert: "Alert",
        transfering: "Transfering",
        currencyUnit: "Vnd"
    }
    egov.resources.tab = extend(egov.resources.tab, {
        home: {
            title: 'Document'
        },
        report: {
            title: 'Statistics'
        },
        print: {
            title: 'Quick print'
        },
        search: {
            title: 'Search'
        },
        setting: {
            title: 'Setting'
        },
        saveChange: 'Do you want to save the changes to the document?'
    });
    egov.resources.file = {
        lenghtIsNotAllow: "Maximum upload size exceeded.",
        typeIsNotAllow: "The file is not in standard format.",
        errorUpload: "There was an error when uploading this file.",
        errorDownload: "There was an error when downloading this file."
    };
    egov.resources.home = {
        syncDataError: 'There was an error when synchronizing list of document list',
        documentPreView: {
            tooltip: {
                open: "View document/file properties",
                close: "Hide document/file properties"
            },
            control: {
                close: "X",
                open: "open"
            }
        },
        docType: {
            message: {
                error: {
                    loading: "Fail to download list of document!"
                }
            }
        }
    };
    egov.resources.treeDocument = {
        message: {
            confirm: {},
            success: {},
            error: {
                syncData: "Fail to synchronize data!",
            }
        }
    };
    egov.resources.treeStore = {
        nameStorePrivateRoot: 'Private store',
        nameStoreShareRoot: 'Share Storage',
        title: {
            createStore: 'Create storage',
            detailSotore: 'View details',
            addStorePrivateAttachment: 'Add attachment'
        },
        message: {
            confirm: {
                openStore: 'Are you sure you want to open this store?',
                closeStore: 'Are you sure you want to close this store?',
                deleteStore: 'Are you sure you want to delete this store?',
            },
            success: {
                openStore: 'Open the store successfully!',
                closeStore: 'Close the store successfully!',
                deleteStore: 'Delete the store successfully!',
            },
            error: {
                createStore: 'Fail to create store',
                updateStore: 'Fail to update store',
                selectStore: 'Fail to select store',

                openStore: 'Fail to open store!',
                closeStore: 'Fail to close store!',
                deleteStore: 'Fail to delete store!',
            }
        },
        contextmenu: {
            createStore: 'Create store',
            updateStore: 'Update store',
            deleteStore: 'Delete store',
            openStore: 'Open store',
            closeStore: 'Close store',
            addStorePrivateAttachment: 'Add new attachments',
            messageCloseStore: 'Are you sure you want to close this store?',
            messageOpenStore: 'Are you sure you want to open this store?'
        }
    };

    egov.resources.documents = {
        title: {
            documentImportant: "Unmark this document as important",
            documentNotImportant: "Mark this document as important",
            vanBanDongXuLy: "Coprocessor document",
            vanBanSapHetHan: "The document will expire (in one day)",
            vanBanKhanHoacQuaHanXuLy: "The document is urgent or exceeds deadline",
            vanBanQuaHanHoiBao: "The document exceeded response deadline",
            vanBanHoaToc: "Express document",
            vanBanThuong: "Normal document",
            documentDetail: "Document/Store details",
        },
        toolbar: {
            controlName: {
                all: "View all",
                day: "Date",
                page: "Page",
                dateAppointed: "Expiration date",
                docTypeName: "Store type",
                documentImportant: "View important documents",
                documentUnread: "View unread documents",
                refresh: "Reload",
                dateReceived: "Receipt date",
                sortBy: "Sort by",
                setting: "Setting list",
                preview: "Preview",
                menu: "Menu",
            }
        },
        contextmenu: {
            name: {
                xemvanban: 'View document...',
                suavanban: ' Edit document...',
                guiykien: 'Send comments...',
                xinykien: 'Ask for comments...',
                bangiao: 'Hand over...',
                thongbao: 'Notify…',
                laylaivanban: 'Retrieve document',
                xacnhanbangiao: 'Confirm handover…',
                xacnhanxuly: 'Confirm process…',
                yeucaubosung: 'Request for addition…',
                tiepnhanbosung: 'Receive addition…',
                kyduyet: 'Approve…',
                ketthucxuly: 'Finish process',
                huyvanban: 'Remove document',
                capnhatketquaxulycuoi: 'Update the last process result…',
                inphieutrinh: 'Print to ask for leader comment…',
                intomtat: 'Print summary',
                capnhattiendo: 'Update progress…',
                xoakhoiduthao: ' Delete draft document',
                contextheodoi: 'Fix track context menu',
                dungxuly: ' Stop process…',
                giahanxuly: 'Extend...',
                chitietvanban: 'Document/Store details',
                danhdaudadoc: 'Mark as read',
                danhdauchuadoc: 'Mark as unread',
                movanban: 'Open document',
            }
        },
        page: {
            text: "Page",
            document: "'Document"
        },
        message: {
            error: {
                quickView: "Fail to retrieve document information!",
                documentNotExist: "The document does not exist!",
                documentNotSelectDelete: "You have not selected document to delete!",
                pagging: "There was an error when moving to the next page!",
                loadNewerDocuments: "There was an error when downloading data!",
                getDocumentDetail: "The document does not exist!",
            }
        }
    };
    egov.resources.document = extend(egov.resources.document, {
        toolbar: {
            noaction: "There is no next transfer.",
            transferByDk: "Transfer as planned",
            transferUserDk: "Transfer to user as planned",
            controlName: {
                transferDoc: {
                    name: "Transfer",
                    message: {
                        error: ' Fail to download list of transfer '
                    },
                    item: {
                        cancel: {
                            name: "Cannot find the next transfer "
                        },
                        transferplan: {
                            name: 'Transfer as planned '
                        },
                        newtransferplan: {
                            name: "Transfer to user as planned"
                        }
                    }
                },
                edit: {
                    name: "Edit",
                },
                insert: {
                    name: "Insert",
                    message: {
                        error: 'Error'
                    }
                },
                reload: {
                    name: "Reload"
                },
                approverYes: {
                    name: "Agree"
                },
                approverNo: {
                    name: "Decline"
                },
                remove: {
                    name: "Remove"
                },
                tiepNhanBoSung: {
                    name: 'Receive addition'
                },
                "return": {
                    name: ' Return'
                },
                finish: {
                    name: 'Finish'
                },
                traloi: {
                    name: "Reply",
                    hoso: "New store",
                    document: "New document",
                    message: {
                        error: ' Failed to load list of classification '
                    }
                },
                phanloai: {
                    name: "Classification",
                    callBackTitle: "Select type of document/store",
                    message: {
                        error: 'Failed to load list of classification'
                    }
                },
                print: {
                    name: "Print",
                    message: {
                        error: 'Failed to load list of print!.'
                    }
                },
                giahan: {
                    name: "Print",
                },
                tiepNhanBoSung: {
                    name: ' Request for addition '
                },
                xinykien: {
                    name: ' Ask for comment '
                },
                thongbao: {
                    name: 'Notify'
                },
                guiykien: {
                    name: ' Send comments '
                },
                savePrivateStore: {
                    name: ' Save '
                },
                others: {
                    name: 'Others'
                }
            }
        },
        content: {
            version: "View the version of {0} updated at {1}"
        },
        relation: {
            titleDialog: "Add related documents",
            confirmRelationTitle: "Confirm to attach related documents",
            ignoreConfirm: "Always send, do not display this notification in next time.",
            contextmenu: {
                open: "Open",
                "delete": "Delete"
            },
            documentNotExist: "The document does not exist!",
        },
        attachment: {
            uploading: "Uploading file",
            uploadSuccess: "Upload file successfully!.",
            uploadError: "Failed to upload file",
            fileChanged: "<strong>File {0} has changed</strong><br/>Do want to save changes to {0}?",
            errorDownload: "Failed to download file",
            openFile: "Open file",
            deleteFile: "Delete file",
            restoreFile: "Restore file",
            download: "Download file",
            removed: "(Removed)",
            using: "Using",
            version: "The version {0} was updated at {1}",
            closeProgramBeforeSave: " You must close the running programs with attachments before saving",
            fileIsRemoved: " File was removed",
        },
        transfer: {
            transferButton: "Transfer",
            dialogTitle: "Hand over document",
            noUser: "You have not selected users to process.",
            transferSuccess: "Transferred successfully.",
            transferError: "Handover failure",
            noUserByAction: "There is no user by that name",
            sendAll: "Send to all",
            answerSuccess: "Answer successfully",
            answerFail: "Failed to answer",
            showDgTitle: "Display other user selection",
            noXlc: "You have not selected users to process",
            userList: " List of users"
        },
        publishment: {
            dialogTitle: "Publish document",
            privateDialogTitle: "Save to the internal publishing store",
            publishButton: "Save and publish",
            noAddressSelected: " Please select the address to receive the document",
            success: "Publish the document successfully",
            error: "Fail to publish! Please try again"
        },
        ChangeDoctype: {
            hasChangeDateAppoint: "Document/store has been classified to new type. </br> Do you want to change the deadline of document/store for the new type?",
            success: "Document/ store has moved to {0} document type?"
        },
        sendComment: {
            dialogButton: "Send comment",
            dialogTitle: "Enter comment",
            enterComment: "Please enter your comment to process",
            sendFail: "Failed to send comments, please try again!",
            sendSuccess: "Send comments successfully",
            requireMessage: "You have not entered comment!",
        },
        announcement: {
            dialogTitle: "Notify",
            announcementButton: "Send notifications",
            sendSuccess: "Send notifications successfully.",
            sendFail: "Fail to send notification, please try again.",
            noReceiver: "Please select the user to receive the notification.",
        },
        consult: {
            dialogTitle: "Ask for comment",
            consultButton: "Send comment",
            sendSuccess: "Send comments successfully",
            sendFail: "Fail to send comments,, please try again",
            noReceiver: "Please select user to receive comments",
            noComment: "There is no comment",
        },
        finish: {
            error: "Fail to finish document, please try again.",
            success: "Finish document successfully",
            processing: "Processing",
        },
        docStore: {
            dialogTitle: "Save to private store",
            createNew: "Create",
            saveButton: "Save",
            notSaveButton: "Do not save",
            noChooseStore: "You have not selected the private store",
            processing: "Saving",
            success: "Save successfully",
            error: "Fail to save, please try again",
        },
        hsmc: {
            documentResult: "Process result:",
            noResult: "Have not approved",
            resultOk: "Approved",
            resultDeny: "Do not approve",
            removeResult: "Remove"
        },
        supplementary: {
            title: "Ask for addition",
            requiredTitle: "Additional information",
            paper: "Additional document",
            fee: "Additional fee",
            noAdditional: "People do not add document",
            addPaper: "Add document",
            addFee: "Add fee",
            newDateAppointed: "Appointed date to return",
            addDay: "Date number",
            dateAppointed: "Return date:",
            supplementType: {
                renew: "Renew",
                "continue": "Continue",
                add: "Add days",
            },
            success: "Receive and add successfully",
            updateAndPrintButton: "Update and print receipt"
        },
        print: {
            text: "Print"
        },
        renewals: {
            renewalsButton: "Renew",
            renewalsAndPrintButton: "Renew and print",
            dialogTitle: "Renew process",
            renewalsReason: "Reason for renew",
            newDateAppoint: "New deadline",
            printTemplate: "Print template",
            noPrintTemplate: "No renewal template"
        },
        updateLastResult: {
            ok: "Approve",
            deny: "Do not approve",
            comment: "Comment:",
            dialogTitle: "Update result"
        },
        returnResult: {
            dialogTitle: "Return result",
            updateAndPrintButton: "Print and return result",
            updateButton: "Return result",
            needToUpdateSupplementary: "The store needs to update additional result:",
            needToUpdateLastResult: "The store needs to update last result:",
            resultOk: "Agree",
            resultDeny: "Deny",
            result: "Result: ",
            personGive: "Person information to receive result",
            finish: "Finish store after returning result",
            printTemplate: "Print form",
        },
        confirmDestroy: "Are you sure you want to destroy this document?",
        xlcLabel: "Main processor:",
        dxlLabel: "CC: ",
        xykLabel: "Ask for comment: "
    });

    egov.resources.documentQuickView.noDocumentSelected = "Select document to display the summary information";

    // y kien thuong dung
    egov.resources.templateComment = {
        titleDialog: "The usual template",
        btnAddTemplateComment: "Add template",
        btnSelected: "Select",
        table: {
            header: {
                content: "Content",
                edit: "Edit",
                "delete": "Delete"
            }
        },
        addDialog: {
            title: "Add more templates",
            btnCreate: "Create",
            errorMessage: "Please enter the comment template!"
        },
        editDialog: {
            title: "Update the usual template",
            btnEdit: "Update",
            errorMessage: "Please enter the comment template!"
        },
        contextmenu: {
            selected: "Select",
            edit: "Edit/ View",
            "delete": "Delete"
        }
    };
    egov.resources.setting.AuthorizesSetting = "Authorization configuration";

    // Signature
    egov.resources.setting.signature = {
        titleCreate: "Create new signature",
        titleEdit: "Update signature",
        configPossition: "Configure signature position",
        configOther: "Other configurations",
        deleteMessage: "Are you sure you want to delete this configuration",
        labelCreate: "Create",
        table: {
            header: {
                stt: "STT",
                configNameSignature: "Configuration name",
                wordsNeedFind: "Find word",
                findTypes: "Find type",
                signTypes: "Find signature",
                position: "Position",
                edit: "Edit",
                "delete": "Delete",
            },
            body: {
                findTypeBottomToTop: "Bottom up",
                findTypeTopToBottom: "Top – down",
                imageSignature: "Image signature",
                textSignature: "Text signature",
                leftPosition: "Left",
                abovePosition: "Above",
                rightPosition: "Right",
                belowPosition: "Below",
                noData: "No such data"
            },
        }
    };

    //Authorization
    egov.resources.setting.authorize = {
        titleCreate: "Create new authorized user",
        titleEdit: "Update signature",
        labelCreate: "Create",
        titleDialogDelete: "Report",
        confirmDelete: "Are you sure you want to delete this configuration",
        btnSubmitDelete: "Yes",
        btnCancelDelete: "Cancel",
        table: {
            header: {
                stt: "STT",
                nameDocType: "Document type",
                userReceive: "Authorized user",
                startDate: "Starting date",
                endDate: "Ending date",
                state: "Status",
                edit: "Edit",
                "delete": "Delete"
            },
            body: {
                noData: "No such data"
            }
        }
    };

    //cấu hình chung
    egov.resources.setting.general = {
        page: "Page section",
        scrollLoadData: "Scroll to load data",
        pagingLoadData: "Page section to load data",
        showDetailDocument: "View document details",
        showQuickView: "View document summary",
    };
    egov.resources.setting.profile = {
        avatar: "Avatar",
        choseAvatar: "Select"
    };
    egov.resources.setting.login = {
        account: "Account:",
        password: "Password:",
        keepingLogin: "Keep login!",
        loginType: "Login type",
        forgetPassword: "Forget password",
        choseServicer: "Please select one OpenID provider:",
        loading: "Processing…",
        btnLogin: "Login",
        title: "LOGIN"
    };
    egov.resources.requiredSupplementary = {
        addRequiredTitle: "Add required template"
    };
    egov.resources.main.links = "Link",
    egov.resources.tree = {
        displayUnRead: "You have {0} unread documents",
        displayUnReadOnAll: "{0} unread / total {1} documents",
        displayAll: "There are {0} documents"
    };
    //#endregion

    //#region Version 1.2 - đã dịch, không thêm mới vào đây

    egov.resources.searching = {
        result: "Search Results"
    };

    egov.resources.common = extend(egov.resources.common, {
        messageYesBtn: "Yes",
        messageNoBtn: "No",
        messageCancelBtn: "Skip",
        messageOkBtn: "Accept",
        errorMessage: "Errors occurred, Please try again or inform your administrator"
    });

    egov.resources.transfer = extend(egov.resources.transfer, {
        HasNoneDocument: "You have not choose your document yet!",
        messageNoBtn: "No",
        messageCancelBtn: "Skip",
        messageOkBtn: "Accept",
    });

    egov.resources.document = extend(egov.resources.document, {
        errorLoadPrivateStore: "Error occurred when loading personal profile list",
        saveSuccess: "Profile saved successfully",
        ignoreConfirmRelation: "Do not ask again",
        ignoreConfirmRelationWarning: 'You can adjust this configuration in Setting->Other Configuration->Uncheck “Alway send attached documents”',
        checkAll: "Check all",
        displayAllComment: "Display all comments",
        displayOnly3Coment: "Display less comments",
        anticipate: "Transfer Anticipate",
        addAnticipate: "Add Anticipate",
        require: "Requirement",
        hasSpellError: 'Spell Errors Detected. Select "' + egov.resources.common.messageYesBtn + '" if you want to continue, select "' + egov.resources.common.messageNoBtn + '" if you want to correct spell errors.',
        errorSpell: {
            add: "add to spell library",
            addSuccess: "add successfully",
            addError: "error occurred"
        },
        notpermission: "You are not permitted to view this document!",
    });

    egov.resources.documentQuickView.noDocumentSelected = "Select document to view summary information.";
    egov.resources.documents.noDocumentCopyItem = "Wonderful! You do not have any document in this folder.";

    egov.resources.documents.notFound = "There is no matching result in the current document list. Press Enter to search the whole system";

    egov.resources.documents.documentNumberDayOverdue = "Over {0} day(s)"; //Over due
    egov.resources.documents.validDocuments = "{0} days left";
    egov.resources.documents.validDocumentsInToday = "Today";

    egov.resources.file = extend(egov.resources.file, {
        maxLength: "Maximum storage: ",
        notAcceptFileTypes: "This file type is not accepted"
    });

    egov.resources.main = extend(egov.resources.main, {
        administrator: "Administrator",
        reloadMessage: "Some settings required system to reload. Do you want to reload?",
        messageNoBtn: "No",

        emptyMailNotifications: "You do not have any mail notfication",
        openAllMail: "Open all received mail",

        emptyChatNotifications: "You do not have any chat notification",
        openAllChat: "Open all received chat",

        emptyDocumentNotifications: "You do not have any document notification!",
        openAllDocument: "Open all received document",
        downloaddesktopversion: "Download desktop version",
        conversion: "Conversation",

        bmail: "Management mail",
        documents: "Process documents",
        chat: "Chat",
        calendar: "Calendar",
        links: "Links",
        report: "Statistic Report",
        notJqueryAlert: "There is no jquery file. Please download jquery file!",
        lblDocument: "Document",
        lblNewConversion: "New Conversation",
        lblNewWorkTime: "Create New shcedule",
        lblNewMail: "Compose",
        searchDocument: "Search more profile information, document. attached file",
        searchMail: "Search Mail",
        youHave: "You Have",
        unreadDocuments: "Unread Documents",
    });

    egov.resources.toolbar = extend(egov.resources.toolbar, {
        DuKienPhatHanh: "Expectation Publish",
        addnewtemplate: "Add New Template",
    });

    egov.resources.attachment = extend(egov.resources.attachment, {
        download: "Download",
        notPermision: "You do not have permission to do this action"
    });
    egov.resources.setting.usersetting = {
        document: "Document, Profile",
        shortkey: "Shortkey",
        documentdefaultname: "Document default names, Profile default name",
        supportkey: "Support Key",
        fnname: "Function Name",
        generalconfig: "Genaral Configuration",
        selectdocument: "Select Document, Profile",

    };
    egov.resources.document = extend(egov.resources.document, {
        openError: "Cannot open related document",
        configError: "Configuration error, please try again",
        saveViolateSuccess: "CBLC saved successfully",
        table: {
            stt: "No.",
            creater: "Creator",
            datecreate: "Date created",
            exprisedate: "Date expired",
            lastcomment: "Last comment",

        },
        require: "Requirement"
    });

    egov.resources.time = {
        date: "Date",
        _date: "date",
        minute: "minute",
        _minute: "minute",
        mon: "Monday",
        tue: "Tuesday",
        wed: "Wednesday",
        thi: "Thursday",
        fri: "Friday",
        sat: "Saturday",
        sun: "Sunday",
        morning: "Morning",
        affternoon: "Afternoon",
        minbefore: " minute(s) before",
        yesterday: "Yesterday",
    },

    egov.resources.enumResource = {
        //columntable: {
        //    doccode: "Document code",
        //    datereceived: "Date received",
        //    compendium: "Compendium",
        //    datecreate: "Date created",
        //    email: "E-mail",
        //    creator: "Creator",
        //    dateapoint: "Date expired",
        //    lastcomment: "Last comment",
        //    docnumber: "Document number",
        //},
        actionlevel: {
            levelone: "Level 1",
            leveltwo: "Level 2",
            levelthree: "Level 3",
            levelfour: "Level 4",
        },
        activitylogtype: {
            dangnhap: "Log in",
            dangxuat: "Log out",
            bangiao: "Transfer document",
            thongbao: "Inform document",
            huyvanban: "Delete document",
            ketthucvanban: "Finish document",
            phanloai: "Sort document",
            phathanh: "Publish document",
            kyduyet: "Sign document",
            xinykien: "Ask for advice",
            guiykien: "Send advice",
            tiepnhan: "Accept",
            xingiahan: "Ask for extension",
            chuyenykiendonggop: "Send Comments",
        },
        categorybusinesstypes: {
            vbden: "Receive document",
            vbdi: "Send document",
            hsmc: "One-door profile",
        },
        dailyprocesstimeforsearch: {
            allday: "All day",
            thirtyminutes: "30 minutes ago",
            onehour: "1 hour ago",
            twohour: "2 hours ago",
            am: "Morning",
            pm: "Afternoon",
        },
        datetimereport: {
            trongngay: "Today",
            trongtuan: "This week",
            tuantruoc: "last week",
            trongthang: "This month",
            thangtruoc: "last month",
            quy1: "First Quater",
            quy2: "Second Quarter",
            quy3: "Third Quarter",
            quy4: "Fourth Quarter",
            trongnam: "This year",
            namtruoc: "Last year",
            tatca: "All",
            tuychon: "Options",
        },
        displaynotifytype: {
            hide: "Hide notifications",
            shownotifyinprocess: "Show only document notifications in process",
            all: "Show all relared document notifications",
        },
        bmailnotifytype: {
            hide: "Hide notifications",
            shownotifyinprocess: "Show only my mail notifications",
            all: "Show all mail notifications",
        },
        displaytreetype: {
            none: "None",
            unread: "Unread",
            unreadonall: "Unread / All",
            all: "All",
        },
        documentprocesstype: {
            tiepnhan: "Accept",
            bangiao: "Transfer",
            kyduyet: "Sign",
            traketqua: "Return result",
            tiepnhanbosung: "Accept addition",
            giahan: "Extension",
        },
        documenttype: {
            thongbao: "Notification",
            congvan: "Official document",
        },
        egovjobenum: {
            indextimerelapsed: "IndexTimerElapsed",
            checkservices: "Check services not working",
            getdocumentsfromedoctool: "Check new documents",
            notifydocunread: "Notify unread documents",
            notifydocinprocesses: "Notify documents in process",
            checkchangedfile: "Check changed file",
            addindex: "Add search index",
        },
        feetype: {
            indextimerelapsed: "Accept",
            thuongbosung: "Bonus",
            tracongdan: "Pay to citizens",
        },
        leveltype: {
            sobannganh: "Departments",
            quanhuyen: "District",
            phuongxa: "Ward",
        },
        licensestatus: {
            capmoi: "New issue",
            capdoi: "Implementation",
            thuhoi: "Retrieve",
        },
        option: {
            documentonlinereg: "Online register ưith an account",
            documentonlineregnoaccount: "Online register ưithout an account",
            acceptdoconline: "Accept when registering online",
            implementdoconline: "Implementation required when registering online",
            rejectdoconline: "Reject when registering online",
        },
        papertype: {
            tiepnhan: "Accept",
            thuongbosung: "Bonus",
            tracongdan: "Pay to citizens",
        },
        permissiontypes: {
            ktao: "Create document",
            xly: "Adjust document",
        },
        processfilterexpression: {
            groupby: "Group by",
            equal: "Equal",
            custom: "Custom",
        },
        scheduletype: {
            hangphut: "Every minutes",
            hanggio: "Every hours",
            hangngay: "Every days",
            hangtuan: "Every weeks",
            hangthang: "Every months",
        },
        searchtype: {
            document: "Document",
            file: "File",
        },
        securitytype: {
            thuong: "Regular",
            mat: "Secret",
            toimat: "Top secret",
        },
        sendtype: {
            buudien: "Post",
            email: "Email",
            fax: "Fax",
            traotay: "hand to hand",
        },
        servicestatus: {
            running: "Running",
            stoped: "Stopped",
            paused: "Paused",
            accessdenied: "Access denied",
            notfound: "Service not found",
        },
        specialkeyenum: {
            nguoidangnhap: "Logged in users",
            ngaythanghientai: "Current date/time",
            meetingtitle: "Meeting title",
            meetingresource: "Meeting revenue",
            meetingdate: "Meeting start time",
            meetingtodate: "Meeting end time",
            meetingcreator: "Meeting creator",
            meetingbody: "Meeting content",
            meetingusers: "Meeting attendants",
            meetinglastupdate: "Meeting updater",
        },
        supplementtype: {
            reset: "Reset",
            'continue': "Continue",
            fixeddays: "Add fized days",
        },
        templatetype: {
            phieuin: "Receipt",
            email: "Email",
            sms: "Sms",
        },
        timerjobtype: {
            warning: "Warning",
            searchindex: "Search index",
            deletetempfile: "Delete temporary file",
        },
        urgent: {
            thuong: "Regular",
            khan: "Urgent",
            hoatoc: "Express",
        },
        quickview: {
            hide: "Hide",
            below: "Below",
            right: "Right",

        },
        fontsize: {
            nho: "Small",
            vua: "Medium",
            lon: "Large",
        }
    }

    //#region Layout
    egov.resources = extend(egov.resources, {
        documentNotifications: "Document notifications",
        emptyDocumentNotifications: "You do not have any document notifications!",
        openAllDocument: "Open all documents",
        downloaddesktopversion: "Download desktop version",
        gtv: "Input method",
        notifications: "Notification",
        news: "News",
        newEmail: "New email",
        config: "Configurations",
        reply: "Reply",
        smallSize: "Small size",
        mediumSize: "Medium size",
        largeSize: "Large size",
        underPreview: "Under preview",
        rightPreview: "Right preview",
        hidePreview: "Hide preview",
        reload: "Reload",
        logout: "Log out",
        searchDocument: "Search document",
        searchFile: "Search attached file",
        reloadMessage: "Update successfully. Do you want to reload",
        closeBtn: "Close",
        submitBtn: "Update",
        titleMessage: "Notice!",
        closeAll: "Close all",
        report: "Report",
        contacts: "Contact",
        calendar: "Calendar",
        chat: "Chat",
        documentslabel: "Document",
        placeholderSearch: "Search place holder",

        administrator: "Administrator",
        links: "Links",
        conversion: "Conversation",
        reloadMessage: "Some settings requires system to reload. Do you want to reload??",
        messageNoBtn: "No",

        mailNotifications: "Mail notifications",
        emptyMailNotifications: "You do not have any mail notifications",
        openAllMail: "Open all received mail",

        chatNotifications: "Chat notification",
        emptyChatNotifications: "You have to chat notifications",
        openAllChat: "Open all chat",

        bmail: "Administration news",
        notJqueryAlert: "No jquery file found. Please load more jquery file!",
        lblDocument: "Document",
        lblNewConversion: "New conversation",
        lblNewWorkTime: "New work time",
        lblNewMail: "New mail",
        searchMail: "Search mail",
        youHave: "You have",
        unreadDocuments: "Unread document",
        "delete": "Delete",

        setting: {
            title: "Personal configuration",
            ProfileConfig: "Personal information",
            Changepassword: "Change password",
            UserSetting: "User setting",
            GeneralSettings: "General setting",
            SignatureSetting: "Signature setting",
            btnUpdateSetting: "Update setting",
            btnCloseSetting: "Close setting",
            AuthorizesSetting: "Authorize setting",
            signature: {
                titleCreate: "Create signature",
                titleEdit: "Edit signature",
                configPossition: "Configure signature position",
                configOther: "Other configurations",
                deleteMessage: 'Are you sure you want to delete this configuration',
                labelCreate: "create new",
                table: {
                    header: {
                        stt: "No.",
                        configNameSignature: "Configuration name",
                        wordsNeedFind: "Word to find",
                        findTypes: "Search type",
                        signTypes: "Sign type",
                        position: "Position",
                        edit: "Edit",
                        "delete": "Delete"
                    },
                    body: {
                        findTypeBottomToTop: "Bottom to top",
                        findTypeTopToBottom: "Top to bottom",
                        imageSignature: "Image signature",
                        textSignature: "Text signature",
                        leftPosition: "Left position",
                        abovePosition: "Above position",
                        rightPosition: "Right position",
                        belowPosition: "Below position",
                        noData: "No data"
                    },
                }
            },
            authorize: {
                titleCreate: "Create new",
                titleEdit: "Update signature",
                labelCreate: "Create",
                titleDialogDelete: "Notice!",
                confirmDelete: "Are you sure you want to delete this configuration",
                btnSubmitDelete: "OK",
                btnCancelDelete: "Cancel",

                table: {
                    header: {
                        stt: "No.",
                        nameDocType: "Name of document type",
                        userReceive: "Receiver",
                        startDate: "Start date",
                        endDate: "End date",
                        state: "Status",
                        edit: "Edit",
                        "delete": "Delete"
                    },
                    body: {
                        noData: "No data"
                    }
                }
            },
            general: {
                page: "page",
                scrollLoadData: "Scroll to load data",
                pagingLoadData: "Data loading page",
                showDetailDocument: "Show detail document",
                showQuickView: "Show quick view"
            },
            profile: {
                avatar: "Avatar",
                choseAvatar: "Choose avatar"
            },
            login: {
                account: "Account:",
                username: "User name",
                password: "Password:",
                keepingLogin: "Keep your account logged in!",
                loginType: "Login type",
                forgetPassword: "Forget password",
                choseServicer: "Choose an OpenID service provider:",
                loading: "Loading...",
                btnLogin: "Login",
                title: "LOGIN"
            },
            usersetting: {
                document: "Document, Profile",
                shortkey: "Shortkey",
                documentdefaultname: "Document, Profile Default Name",
                supportkey: "Support Key",
                fnname: "Function name",
                generalconfig: "General Configuration",
                selectdocument: "Select Document, Profile",
            }
        },
        common: {
            saveBtn: "Save",
            cancelBtn: "Cancel"
        }
    });

    //#endregion

    egov.resources = extend(egov.resources, {
        activityLog: {
            questionDelete: "Do you want to delete this log?",
            notChoice: "You have not selected the log you want to delete. '"
        },
        crystalReport: {

        },

        level: {
            nodata: "No administration level was found",

        },
        license: {
            AddLicense: "Register",
            RegisterLicense: "Register license",
            customername: "Customer",
            Phone: "Phone number",
            Email: "Email",
            ToDate: "Expiration date",
            TotalUser: "Account number",
            key: "Activation key",
            customername: "",

        },
        log: {
            logNotSelect: "You have not selected the log you want to delete.",
            deleteSelection: "Delete selected log",
            detail: "Log detail",
            //view chưa làm

        },
        notify: {
            noform: "No form was found",
            nouse: "Not use",
            urgent: {
                name: "Urgency level",
                level1: "Normal",
                level2: "Urgent",
                level3: "Extremely urgent",
            },
            hasRead: "Approved",
            alerttime: "minutes (Set =0 to send sms as soon as there is a new meeting",

        },
        office: {
            nooffice: "No office was found",

        },
        paper: {
            nopaper: "No papers was found",
            other: "Other",
            list: "List of papers",
            action: "Profession",
            docfield: "Field",
            doctype: "Document type",
            addpaper: "Add new papers",
            updatepaper: "Update papers",

        },
        people: {
            nopeople: "No user was found",
            peoplesearch: "Search accounts",
        },
        position: {
            sorterror: "Error occurred when set up title priorities.",
            npposition: "No title was found",
        },
        //aaaaaaa
        printer: {
            addprinter: "Add new printers",
            editprinter: "Set up printers",
            nodata: "No printer was found",
            notconnect: "Fail to check connection to printer!",
            nameisrequired: "Printer name is required!",
        },
        processfunction: {
            selectcolor: "Select color",
            user: "User",
            position: "Title",
            position1: "Department/title",
            all: "All",
            failure: "Not enough information in list setting",
            setupforfilterlist: "Set up filter list for node",
            setupforsamelist: "Set up corresponding list with node",
            enternote: "Use Enter key to begin a newline (if in the last line, add a new line), use up and down arrow keys to sort columns",
            divrole: "Authorize",
            addnodefilter: "Add new filter for node",
            parameterlisthaverequirement: "Not enough information in parameter list",
            docfieldnote1: "If parameter has value column of  DocFieldId, the system",
            docfieldnote2: "understands that the document tree is filtered by field and document type",
            columnname: "Name of value column",
            paraname: "Parameter name",
            updatenodetype: "Update node type",
            document: "Working document",
            addNode: "Add new node",
            copyNode: "Copy this node",
            paste: "Paste node",
            "delete": "Delete this node",
            confirmdeletenode: "Deletion of 1 node means deletion of its child nodes. Are you sure you want to delete",
            deletenodesuccessfull: "Delete node successfully!",
            nodata: "No node type was found",
            nofilter: "No filter was found",
            nogroup: "No group was found",
            alldocument: "---All types of documents, files---",
            someinfoisrequired: "Not enough information in list setting",
        },
        question: {
            nodata: "No question was found",
        },
        report: {
            //Chưa làm
            fileuploadrpt: "Only *.rpt file is allowed to upload",
            showguidewhenwritingsqlquery: "Display guide of writing sql",
            showdatabygroup: "Set up to view data by group",
            showgroupinreporttree: "Set up to show groups in report tree",
            _setting: "[setting]",
            statisticSetting: "Statistics setting",
            permissionreadreport: "Authorize to read reports",
            privatesetting: "Private setting",
            settingreport: "Use report setting",
            nodata: "No report group was found",
            deletesuccessfull: "Delete the report successfully",
            confirmdeletereport: "Are you sure you want to delete this report with all its child reports?",
        },
        notifications: {
            loading: "Loading...",
            haveError: "Error occurred",
            nolog: "No log was found",
            processing: "Processing...",
            emailSuccess: "Email was sent successfully!",
            emailError: "Failed to send email!",
            sendEmailError: "Error occurred when sending email",
            queryEmbryonicForm: "Query must not be null!",
            changeStatusEmbryonicForm: "System cannot change status",

        },
        resource: {
            nodata: "No resource was found",
            choosefileimport: "Choose import file",
        },
        role: {
            nodata: "No user was found",
            isallow: "Allow",
            rolename: "Role name",
            nodatagroup: "No user group was found",

        },
        scopearea: {
            nodata: "No ScopeArea was found",
        },
        setting: {
            sendemailto: "Sending test email to",
            sendemailsuccess: "success!",
            sendemailfailure: "failure!",
            smtpsetting: "SMTP server setting",
            othersetting: "Other setting",
            location: {
                addlocation: "Add locations",
                editlocation: "Edit locations",
                confirmdeletefilelocation: "Are you sure you want to delete the file store location?",
                canotdelete: "The file location has already been used, you are not allowed to delete.",
                listfilelocation: "List of file locations",
                nodata: "Setting of file location was not found",
                canotdelete: "",

            },
            general: {
                finishdocument: "Finish files/documents",
                setting: "Home page setting",
                nofifysetting: "Notify setting",
            },
            passwordpolicy: {
                checkpassword: "Check password syntax",
                lookaccount: "Lock accounts",
                passworddeadtime: "Password expired",
                passwordchangehistory: "Password changes",
                defatultresetpassword: "Reset password"
            },

        },
        shared: {
            productname: "Customer administration",
            systemtree: "System tree",
            home: "Home page",
            admincustomer: "Customer administration",
        },
        store: {
            pts: "(Document store keeper)",
            nouser: "No user was found",
            tempforstore: "Template list for document store",
            alltempname: "All template names",
            notemp: "No template was found!",
            _all: "[All]",
            nodocumentstore: "No document store was found",

        },
        template: {
            nodata: "No template was found",
            key: "Shared key",
            systemerror: "System error occurred, template status cannot be changed",

        },
        templatekey: {
            onoffguide: "Turn on/off guide",
            showguide: "Show guide when writing key code, sql, template",
            ex: "For example:",
            needparameter: "Query must include parameter",
            parametercanuseinquery: "Parameters can be used in query",
            keyformat: "Valid key format",
            speccharacter: "{[a-zA-Z0-9_]+}",
            keyformat2: "Include letters (uppercase, lowercase), digit and underscore (_).",
            getvalueintempdoc: "Get value of template document.",
            currentuserid: "Current user id.",
            doctype: "Papers type.",
            costtype: "Fee type.",
            additiondoc: "List of additional docs.",
            formatofresulequery: "Query result by",
            fieldname: "field name",
            sqlresult: "is the result of sql",
            dataprocessfunctions: "Data processing functions",
            exdataconvertfunctions: "All data convert functions:",
            stringprocessingfuntions: "All string processing functions:",
            datefunctions: "All date processing functions:",
            stringformats: "All string formats:",
            viewdetail: "View details at:",
            selectdocument: "Select document type",
            selecttemplate: "Select templates",
            selectdocfield: "Select field",
            keycode: "Key code",
            nokey: "No key was found",
        },

        time: {
            timenotcheck: "Time cannot be checked",
            checkdate: "Check date",
            caculateextendtime: "Calculate extended vacation schedule",
            nodata: "No holiday/day-off was found",
            repeat: "Repeat",
            repeatbyyear: "Annual repeat ",
            freeday: "Name of day-off",
            DL: "Lunar calendar",
            AL: "Christian calendar",
            day: "Date",
            listofrestday: "List of annual vacation",
            weekworktime: "Weekly working hours",

        },
        transfertype: {
            nodata: "No transfer type was found",
        },
        user: {
            username: "Username",
            fullname: "Full name",
            phone: "Phone number",
            usernameexist: "Username already exists",
            notindepartment: "Not in any department",
            rolename: "Role name",
            groupname: "Group name",
            isadministrator: "Is an administrator",
            ismaindepartment: "Is main department",
            position: "Title",
            position1: "Job position",
            departmentname: "Department name",
            nodata: "No user was found",
            all: "---All---",
            active: "Active",
            unactive: "Unactive",
            confirmtoresetpassword: "Are you sure you want to reset password of this account?",
            resetpasswordsuccess: "Reset password successfully!",
            selectusertoimport: "Select user to import",
            importusersuccessfull: "Import successfully",
            defaultPasswordRest: "Default reset password",
            clearToRandomData: "Make empty to create random password",
        },
        ward: {
            city: "Province/city:",
            district: "District:",
            nodata: "No commune/ward was found",
            updatedata: "Update commune/ward",
            datalist: "Commune/ward list",
        },
        welcome: {},

        notifications: {
            searching: "Searching...",
            loading: "Loading...",
            haveError: "Error occurred",
            nolog: "No log was found",
            processing: "Processing...",
            destroythisfilter: "You want to delete this filter",
            post: "Notify",
            confirmdelete: "Are you sure you want to delete?",
            updatesuccessful: "Update successfully",
            createsuccessful: "Create successfully",
            downloaderror: "Failed to load file",
            choosefileimport: "Selection of import file is required",
            importing: "Importing...",
            dowloadfileerror: "Error occurred when loading file",
            aaaa: "",
            aaaa: "",
            aaaa: "",
            aaaa: "",

        },
        buttons: {
            select: "Select",
            selectAll: "Select all",
            edit: "Edit",
            "delete": "Delete",
            orderedsort: "Sort order",
            orderedsave: "Save order",
            addfilter: "Create filters",
            addparameter: "Add parameters",
            save: "Save",
            confirm: "Confirm",
            back: "Back",
        },
        tableheader: {
            stt: "No.",
            "function": "Function",
            description: "Description",
            form: "Form",
            edit: "Edit",
            select: "Select",
            "delete": "Delete",
            type: "Type",
            formname: "Form name",
            filtername: "Filter name",
            columnname: "Column name",
            displayname: "Display name",
            width: "Width",
            allowsort: "Allow sorting",
            sortcolumn: "Sorting column",
            sort: "Sort",
            type: "Type",
            value: "Value",
            name: "Name",
            domain: "Domain",
            ip: "ip",
            zone: "Zone",
            isallow: "Allow",
            addordelete: "Add/delete",
        },
        commonlabel: {
            list: "List",
            select: "Select",
            is: "Is",
            or: "Or...",
            select: "Select",
            addcolumn: "Add columns",
            add: "Add",
            addnew: "Create",
            cancel: "Cancel",
            note: "*Note:",
            time: {
                date: "Date",
                _date: "date",
                minute: "Minute",
                _minute: "minute",
                mon: "Monday",
                tue: "Tuesday",
                wed: "Wednesday",
                thi: "Thursday",
                fri: "Friday",
                sat: "Saturday",
                sun: "Sunday",
                morning: "Morning",
                affternoon: "Afternoon",

            },
            contact: "Contact",
            errorpage: "Sorry, error occurred while processing your request.",
            all: "All",
            email: "Email",
            sms: "SMS",
            printcard: "Printed card",
            vnconcurency: "vnd",
            reject: "Reject",
            yes: "Yes",
            no: "No",
            search: "Search",
            allow: "Allow",
            notallow: "Not allow",
            "delete": "Delete",
        }
    })

    //#endregion

    //#region v1.1 đã dịch không thêm mới vào đây

    egov.resources = extend(egov.resources, {
        crystalreport: {
            copyfromstatisticform: "Copy from statistic form",
            copyfromreportform: "Copy from report form",
            reconfig: "Reconfigure",
        },
        doctype: {
            othernodes: "Other nodes",
            addcontrol: "Add control",
            downloadworkflowerror: "Error occurred when download work flow",
            newworkflow: "Add new work flow",
            workflownameisrequired: "Work flow name required",
            pasteworkflow: "Paste work flow",
            usethisworkflow: "Use this work flow",
            comfirmtocancel1: "Confirm to cancel ",
            comfirmtocancel2: "Cancel ",
            comfirmtocancel3: "Use this work flow?",
            update: "Update list",
            editTemplateWorkflow: "Others",
            updatenode: "Update node",
            interfaceconfig: "Configure interface",
            editthisworkflow: "Edit this work flow",
            copythisworkflow: "Copy this work flow",
            deletethisworkflow: "Delete this work flow",
            confirmtodeltethisworkflow: "Are you sure want to delete this work flow?",
            notyouthisworkflow: "Do not use this work flow",
            workflowname: "Work flow name",
            exprisedate: "Expired date",
            date: "Date",
            addtemplateform: "Add template",
            noformdata: "No template",
            doctypename: "Document name: ",
            docfield: "Field: ",
            status: "Status:",
            active: "Active",
            notactive: "Not Active",
            noconfiguration: "No configuration",
            confirmtodeletereceivenode: "Are you want to delete the received node?",
            objectype: "Objective Types",
            value: "Value",
            'delete': "Delete",
            receivepostlist: "Receiving post list",
            removeReceiveList: "Are you sure want to delete receiving list?",
            of: "Of",
            allpeople: "Everyone",
            level: "Level",
            user: "User",
            alljobtitle: "All Job title ",
            alljobtitleof: "All job title of ",
            alljobtitleof1: "All ",
            alljobtitleof2: " Of ",
            jobtitledepartment: "Job title-Department",
            alluserof: "All user of ",
            fieldisrequired: "Input value",
        },
        form: {
            updateform: "Update form",
            formname: "Form name",
            description: "Description",
            tempkey: "Template",
            status: "Status",
            config: "Configuration",
            status1: " In use",
            status1: " Not in use",
            status3: " Temporary save form",
            configformtitle: "Title",
            addtitle: "Add title (Label)",
            showgrid: "Show grid",
            shownumber: "Show number",
            addrow: "Add row",
            chooseextendfield: "- - - Choose extended field - - - ",
            choosedocumenttype: "- - - Choose document type - - - ",
            title: "Title (label)",
            brand: "Brand (label)",
            references: "Reference",
            account: "account"
        },
        guide: {
            nodata: "No guide"
        },
        increase: {
        },
        infomation: {
            chageinfo: "change information",
        },
        jobtitles: {
            nodata: "No job title",
            dragordroptosort: "drag or drop to sort",
            sorterror: "Error occurred while sorting priority level of job titles.",
        },
        editor: {
            deletecol: "Delete column",
            deleterow: "Delete row",
            insertabove: "Insert above",
            insertbelow: "Insert below",
            insertleft: "Insert left",
            insertright: "Insert right",
            merge: "Merge",
            splitcolumn: "Split column",
            splitrow: "Split row",
            update: "update",
            all: "[All]"

        },
        buttons: {
            deleteall: "Delete all",
            search: "Search",
            agree: "Agree",
            ignore: "Ignore",
            add: "Add",
            view: "View"
        },
        commonlabel: {
            other: "Other",
            haveerrortryagain: "Error occurred, please try again!"
        }
    });

    //#endregion

    //#region V1.2 đã dịch không thêm mới vào đây

    egov.resources = extend(egov.resources, {
        setting: {
            general: {
                loadpagescroll: "Page scroll",
                loadpagesize: "Page size",
                language: "language: ",
                useVietNameseTyping: "Use Vietnameese typing"
            },
        },
        processfunction: {
            parent: "Parent",
            normal: "Normal",
            field: "Field",
            type: "Type",
            displayname: "Display name",
            filterByOverdueDate: "Filter by overdue date:",
            display: "Display",
            defaultdocumentsortconfig: "Default document sorting configuration ",
            entertobreakpage: "(Enter to break page (Add new line if at the end of the line),use up or down node to arrange column)",
            configlistbynode: "Config list by node",

        },
        template: {
            printorder: "Print order",
            per1: "Receive",
            per2: "Transfer",
            per4: "Sign",
            per8: "Return result",
            per16: "Receive implementation",
            per32: "Extend",
        },
        form: {
            title: "Template for document type",
            currentusername: "Current user name",
            docfieldname: "Document field",
            doctypename: "Document type",
            doccode: "Document code",
            receivedate: "Receive date",
            appointdate: "Appoint date",
            template: "Template:",
            insertspecialvalue: "Insert special value:",

        },
        report: {
            config: "Configuration",
            configsetup: "[Configuration setup]",
            showguide: "Show/hide guide",
        },
        law: {
            choosedocument: "Choose document",
            lawnumbercode: "Law number code",
        },
        code: {
            choosedepartment: "Choose departmnet code",
        },
        store: {
            choosecategory: "Choose category",
            addstoreviewer: "Add store viewer",
        },
        time: {
            worktime: "Work time",
            listoffsetday: "List off set day",
        },
        deparment: {
            choosejobtitle: "Choose job title",
            chooseposition: "Choose position",
            listuser: "List user",
            nouser: "No user",
            nodata: "No data",
            addsubdeparment: "Add sub-department",
            deparmentinfo: "Department information",
            deparmentname: "Department name",
            updateinfo: "Update information",
            adduser: "Add user",
            fullname: "Full name",
            isadmin: "Is admin",
            jobtitle: "Job title",
            position: "Position",
            list: "List"
        },
        position: {
            orderedsort: "Drag or drop to sort",
        },
        bkavmessagebox: {
            useshowtoreplacealert: "Use eGovMessage.show(message, title) to replace messageBoxAlert().",
            useshowtoreplaceconfirm: "Use eGovMessage.show(message, title, messageButtons.OkCancel) to replace messageBoxConfirm().",
            usenotificationtoreplacetemp: "Use eGovMessage.notification() to replace messageTemp().",
            closebutton: "Close",
            yes: "Yes",
            no: "No",
            ok: "OK",
            cancel: "Cancel",
            notify: "Notify",
        },
        tableheader: {
            sortcolumnname: "Sort column name",
            sorttype: "Sort type(ascending or descending)",
            order: "Order",
            sortcolumn: "Sort column",
            isallowsort: "Is allowed to sort",
            displayname: "Display name",
            width: "Width",
        },
        commonlabel: {
            deincrease: "Descending",
        },
    });


    //#endregion


    //#region V1.3 - Chua dich
    egov.resources = extend(egov.resources, {
        template: {
            specialkey: "Special Key",
            keyfromform: "Key From Form"
        },
        doctype: {
            embryonicformname: "embryonic form Name:",
            embryonicformlist: "The list of embryo form:",
            addnewform: "Add a new form",
        },
        notify: {
            noJquery: "The library is using jQuery, download the jQuery library before use",
            noUnderscore: "This library use underscore, download library underscore before use",
        },
        workflow: {
            user: "User",
            position: "Position",
            relation: "Relationships",
            belowoffice: "Subordinate units",
            underoffice: "The attached units",
            currentoffice: "Current Unit",
            sameoffice: "The same unit",
            peernode: "Peer Node",
            sameparentnode: "Together Parent Node",
            underuser: "Subordinates",
            overuser: "Superior",
            addnotifyfouser: "Add User receive notification",
            nodename: "Node Name",
            choosedepartment: 'Choose Department',
            listuser: 'User list',

        },
        formtemplate: {
            columnwidth: "The width of the column",
            brandwidth: "width labels",
            height: "Height",
            disabled: "Disabled",
            verifydata: "Having examined data",
            compendium: "Epitome",
            commented: "Italy is handled",
            doctype: "Document Type",
            category: "Style",
            inoutplace: "Unit",
            dateappointed: "The duration of treatment",
            organization: "The agency sent",
            doccode: "The number / symbol *",
            doccode2: "Number *",
            datearrived: "On arrival",
            dateresponse: "Complementary newspapers",
            datepublished: "Issued",
            store: "Book record",
            storeid: "text book",
            inoutcode: "No to go",
            totalpage: "The site",
            choosetotalpage: "Select site",
            docfield: "Field",
            keyword: "Tag",
            sendtype: "Forms sent",
            doccode1: "Ma record",
            citizenname: "The citizen",
            address: "Address",
            phone: "phone number",
            docpapers: "Papers",
            identitycard: "IdentityCard Number",
            email: "Email",
            commune: "Social wards",
            attachmentlist: "Attachment",
            relationlist: "Related Documents",
            cbdetail: "Show Details text to",
            allcomment: "Content processing",
            titlecontent: "Content writing",
            urgent: {
                name: "Urgency level",
                level1: "Normal",
                level2: "Urgent",
                level3: "Extremely urgent",
            },
            securityid: {
                name: "Security level",
                normal: "Normal ",
                high: "High",
                important: "Top secret",
            },
            compendiumtitle: "Enter compendium.",
            nocomment: "Never give opinions",
            displayform: "Show form",
            storeprivate: "Personal Profile",
            storeshare: "Profile share",
            nextpage: "Next",
            prepage: "Previous",
            currentpage: "Home",
            print: "In",
            btnfinish: "End",
            viewicontraketqua: "Check the results",
            viewicontiepnhanbosung: "To receive additional",
            viewiconhuyvanban: "Cancel",
            viewiconluu: "Save window",
            viewiconguiykien: "Send comments",
            viewiconthongbao: "Announcement",
            viewiconxinykien: "Consult",
            viewiconyeucaubosung: "Additional requirements",
            viewicongiahanxuly: "Renew",
            no: "Refused",
            yes: "Agree",
            btninsertrelation: "The documents related ...",
            btninsertattachment: "Attachment",
            btninsertscan: "File Scan ...",
            btnpaper: "License ...",
            btninsertanticipate: "It is expected that transfer ...",
            btntransfer: "Text / profile",
            btnedit: "Edit text / file",
            btninsertfile: "Attach",
            btnapproveryes: "Eastern standard approval",
            btnapproverno: "Refusal to approve",
            btndestroy: "Remove documents / records",
            viewiconketthuc: "",
            btnfinishtt: "End",
            btnanswer: "Answer",
            btnchangedoctype: "Classification",
            concurrency: "VND",
            UserComment: "The treatment",
            filename: "Filename",
            filesize: "Dimension",
            FileVersion: "version",
            lastupdatefile: "Update Post",
            finalcomment: "Italy is resolved",
            backtolist: "Honor",
            "delete": "Delete",
            mainprocess: "Handling key:",
            coprocess: "At the handle:",
            sendto: "Go to",
            thongbao: "Notice:",
            xinykien: "Consult:",
            view: "See",
            download: "Download",
            placelabel: "Recipients",
            officename: "The agency",
            placeinoffice: "Recipients of the unit",
            approvers: "Signer",
            datepublished: "Date issued",
            docinpage: "S.ban / s.trang",
            inplace: "Where the original note",
            publishreceive: "List of recognized text",
            createdate: "On initialization",
            createdate1: "On creation",
            panelselectorrequire: "You must pass parameters panelSelector",
            publicoffice: "The agency issued",
            insertanticipate: "It is expected that transfer",
            commoncomment: "Italy is used",
            receivedays: "The date of acceptance",
            requirereport: "Request a return",
            fees: "Fees",
            documentrelation: "Related Documents",
            attachment: "attachment",
        },
        form: {
            formgroup: "sample group:",
            formtype: "type specimen"
        },
        code: {
            name: "Dance number",
            config1: "Get the current day, if less than 10, add 0 in front",
            config2: "Get the current day",
            config3: "Get the current month, if less than 10, add 0 in front",
            config4: "Get the current month",
            config5: "Get the current year",
            config6: "Get two last digits of the current year",
        },
        catalogs: {
            addbewobject: "Add object",
        }
    });

    egov.resources = extend(egov.resources, {
        notify: {
            openall: "View all",
            closeall: "Close all",
            nomailnotify: "You do not have any new mail in notify",
            nodocumentnotify: "You do not have any new document in notify",
            nochatnotify: "You do not have any new conversation in notify",
            valuemodelnotnull: "This model value is not null",
        },
        menu: "Menu",
        processdoc: "Processing Document",
        documents: {
            documentNumberWeekOverdue: "Over {0} weeks",
            documentNumberMonthOverdue: "Over {0} month(s)",
            documentNumberYearOverdue: "Over {0} year(s)",
            unlimitedTime: "Unlimted",
            multiselect: "{0}",
            contextmenu: {
                reOpenDocument: {
                    text: "ReOpen Document",
                }
            }

        },
        formtemplate: {
            content: "Nội dung",
            hasauthentication: {
                name: "Thẩm quyền",
                'true': "Thuộc thẩm quyền giải quyết",
                'false': "Không thuộc thẩm quyền giải quyết"
            },
            original: {
                name: "Nguồn đơn",
                direct: "Trực tiếp",
                topdown: "Từ trên chuyển xuống",
                other: "Từ nơi khác chuyển đến"
            },
            iscomplain: {
                name: "Phân loại đơn",
                'true': "Khiếu nại",
                'false': "Tố cáo"
            }
        },
        userConfig: {
            saveSuccess: "Save config successfull",
            saveError: "Save config unsuccessfull",
        },
        setting: {
            profile: {
                male: "Male",
                female: "Female",
                lastname: "Last and middle name *",
                firstname: "First name *",
                gender: "Gender *",
                phone: "Number phone",
                fax: "Fax",
                address: "Address",
            },
            changepassword: {
                currentpassword: "Current password *",
                newpassword: "New password *",
                confirmpassword: "Confirm password",
            },
            general: {
                isFullQuickView: "Set up mode to view document summary",
                IsSaveOpenTab: "Allow re-opening document, file when loading page again",
                DisplayPopupTransferTheoLo: "Display popup when transfering document with ",
                ViewDocInPopUp: "Display document in popup",
                IgnoreConfirmRelation: "Ignore Confirm Relation",
                HasPopupChat: "Chat in popup",
                DisplayNotifyType: "Document notify type",
                BmailNotifyType: "Mail notify type",
                QuickView: "The display location of document summary ",
                MudimMethod: "Typing type",
                FontSize: "Font size",
                DefaultPageSizeHome: "Number of records on 1 page by default",
                ListPageSizeHome: "List of page size",
                PrinterName: "Printer",
                Language: "Language"
            },

            kntc: {
                enable: "Active",
            }

        }
    })

    egov.resources.document = extend(egov.resources.document, {
        addtime: {
            numberonly: "This is number only",
        },
        anticipate: {
            name: "Expectant forward",
            receive: "Receive trend",
            choosereceive: "Choose receive trend",
            receiver: "Receiver",
            choosereceiver: "Choose Receiver",
            anticipate: "Expectant trend",
            chooseanticipate: "Choose expectant trend",
        },
        DateCreated: "Date created",
        PlaceLabel: "Place",
        OfficeName: "Office name",
        PlaceInOffice: "Place In Office",
        Approvers: "Signer",
        DatePublished: "Published Date",
        DocInPage: "Page / Total page",
        InPlace: "Origin document location",
        publishReceive: "Receive list",
        UserNameReturned: "Payer",
        DateReturned: "Finish date",
        DateSuccess: "Approval Date",
        DateReceived: "Received Date",
    })
    egov.resources.document.publishment = extend(egov.resources.document.publishment, {
        addpublishment: "New publish document",

    })
    egov.resources.report = {
        exprort: "Export",
        group: "Group",
        print: "Print",
        view: "View",
        totalDocuments: "Documents:",
        totalDocumentNotProcessed: "Processing documents:",
        totalDocumentProcessed: "Procesed documents:",
        totalDocumentOverdue: "Overdue documents:",
    }

    egov.resources.plugin = {
        noplugin: "No plugin was found",
        pluginrequire: 'You need download and install our plugin to open attachment files and scan photo',
        needrestartbrowser: 'Restart your browser is recommend after our plugin installed',
        downloadtosetup: 'Download and Install',
        waitforsetup: 'Downloading plugin, take a wait...'
    }

    egov.resources.avatar = {
        nodata: "../../../AvatarProfile/noavatar.jpg",
        errorUrl: "../../../AvatarProfile/noavatar.jpg",
        icon: "../../../AvatarProfile/icon/i ({0}).png",
        troll: "../../../AvatarProfile/troll/t ({0}).png",
    }
    egov.resources.mobile = {
        usersetting: {
            loadavatar: "Use real avatar",
            fullscreen: "Full screen",
            fullscreennode: "(scrolling only)",
            appstart: "Start application",
            fontsize: "Font size",
            fontsizeType: {
                small: "Small",
                normal: "Normal",
                large: "Large",
            },
            pagesize: "Page size",
            pagesizenode: "(mail only)",
            language: "Language",
            languagenode: "(restart require)",
        },
        main: {
            logout: "Logout",
            reload: "Reload",
            config: "Config",
            createPersonMail: "Send personal mail",
            createBoxMail: "Send to this box",
            documentSearch: "Document search ...",
            mailSearch: "Enter search key ...",
        },
    }
    egov.resources.tree = extend(egov.resources.tree, {
        question: {
            label: "Frequently Asked Question",
            general: "General Questions",
            document: "Document Questions",
            QuestionName: "Question Name",
            citizenname: "Citizen Name",
            date: "Date",
            doccode: "Document Code"
        }
    })
    //#endregion

    //#region bmail
    bmail.resources = {
        mailbox: {
            inbox: "Inbox",
            sent: "Sent",
            drafts: "Drafts",
            junk: "Spam",
            trash: "Trash",
        },
        toolbar: {
            "delete": "Delete",
            deletePer: "Permanenty Delete",
            reply: "Reply",
            replyall: "Reply All",
            forward: "Forward",
            edit: "Edit",
            spam: "Mark as spam",
            unspam: "Unmark spam",
            unread: "Mark unread",
            read: "Mark read",
            restore: "Restore",
        },
        button: {
            ok: "Ok",
            cancel: "Cancel",
            "delete": "Delete",
            send: "Send",
            close: "Close",
        },
        notify: {
            success: "Success",
            error: "Error",
        },
        common: {
            processing: "Processing",
            label: {
                time: "Time",
            },
            time: {
                today: "Today",
                yesterday: "Yesterday",
                date: "Date",
                _date: "date",
                minute: "Minute",
                _minute: "minute",
                mon: "Mon",
                tue: "Tue",
                wed: "Web",
                thu: "Thu",
                fri: "Fri",
                sat: "Sat",
                sun: "Sun",
            },
            table: {
                stt: "Order",
                _stt: "Order",
            },
            searchresult: "Search Result",

        },
        main: {
            newmail: "Add new mail",
            sendto: "To",
            cc: "Cc",
            bcc: "Bcc",
            subject: "Subject",
            originmessage: "----- Origin : ----- ",
            send: "Send",
            close: "Close",
            savedraftsuccess: "Save draft successfull",
            savedrafterror: "Save draft unsuccessfull, try again later",
        },
        detail: {
            time: "Time",
            sendto: "To",
            sendfrom: "From",
            nosubject: "No subject",
            file: {
                toolarge: "File too large",
                tryagain: "Error. Try again",
            },
            fieldsrequire: "Some field are require",
            uploaderror: "Upload Error",
            confirmdelete: "Do you want to delete this message?",
            subject: "Subject",
            content: "Write something...",
            messageorigin: " ----- Original Message: ----- ",
        },
        savedraft: {
            success: "Saved draft",
            error: "Save draft error"
        },
        sendmail: {
            success: "Sent",
            sendding: "Sendding...",
            error: "Sendding mail error, try again"
        },

    }
    //#endregion
})
(window, window.egov = window.egov || {}, window.bmail = window.bmail || {})