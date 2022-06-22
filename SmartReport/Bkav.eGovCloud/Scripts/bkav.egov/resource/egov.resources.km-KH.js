(function (window, egov, bmail) {
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

    window.isCamResource = true;

    egov.resources = {
        document: {
            Compendium: "ដកស្រង់",
            Comment: "យោបល់ដោះស្រាយ",
            DocType: "ប្រភេទឯកសារ",
            Category: "រូបភាព",
            InOutPlace: "អង្គភាព",
            DateAppointed: "កាលកំណត់ដោះស្រាយ",
            Organization: "ស្ថាប័នផ្ញើជូន",
            DocCode: "លេខ/សញ្ញាសម្គាល់*",
            DocCode2: "អត្តលេខ*",
            DateArrived: "ថ្ងៃមក",
            DateResponse: "ឆ្លើយតប",
            DatePublished: "ថ្ងៃចេញផ្សាយ",
            StoreId: "សៀវភៅឯកសារ",
            InOutCode: "លេខមក-ទៅ",
            TotalPage: "ចំនួនទំព័រ",
            ChooseTotalPage: "ជ្រើសចំនួនទំព័រ",
            DocField: "វិស័យ",
            Keyword: "ពាក្យគន្លឹះ",
            SendType: "រូបភាពផ្ញើជូន",
            DocCode1: "លេខកូដសំណុំរឿង",
            CitizenName: "ឈ្មោះប្រជាពលរដ្ឋ",
            Address: "អាស័យដ្ឋាន",
            Phone: "លេខទូរស័ព្ទ",
            DocPapers: "លិខិតស្នាម",
            IdentityCard: "លេខអត្តសញ្ញាណប័ណ្ណ",
            Email: "សំបូត្រអេឡិចទ្រនិច",
            Commune: "ឃុំ សង្កាត់",
            AttachmentList: "ឯកសារភ្ជាប់តាម",
            RelationList: "ឯកសារទាក់ទង",
            cbDetail: "បង្ហាញលិខិតឯកសារទទួលបានដោយលំអិត",
            AllComment: "ខ្លឹមសារដោះស្រាយ",
            titleContent: "ខ្លឹមសារលិខិតឯកសារ",
            Urgent: {
                name: "កំរិតប្រញាប់",
                normal: "ធម្មតា",
                fast: "ប្រញាប់",
                important: "យ៉ាងប្រញាប់"
            },
            SecurityId: {
                name: "កំរិតសម្ងាត់",
                normal: "ធម្មតា",
                high: "ខ្ពស់",
                important: "ដ៏សម្ងាត់",
            },
            CompendiumTitle: "បញ្ចូលការដកស្រង់",
            NoComment: "មិនទាន់មានយោបល់",
            DisplayForm: "បង្ហាញសំណុំបែបបទ",
            StorePrivate: "សំណុំរឿងឯកជន",
            StoreShare: "សំណុំរឿងចែករំលែក",
            nextPage: "ទំព័របន្ទាប់",
            prePage: "ទំព័រមុខ",
            currentPage: "ទំព័រទី1",
            print: "បោះពុម្ភ",
            btnFinish: "បញ្ចប់",
            viewIconTraKetQua: "ស្នងលទ្ធផលឱ្យវិញ",
            viewIconTiepNhanBoSung: "ទទួលបន្ថែម",
            viewIconHuyVanBan: "ចោល",
            viewIconLuu: "រក្សាទុកចូលសៀវភៅ",
            viewIconGuiykien: "ផ្ញើយោបល់",
            viewIconThongbao: "សេចក្តីប្រកាស",
            viewIconXinykien: "សូមយោបល់",
            viewIconYeuCauBoSung: "សំណូមពរបំពេញបន្ថែម",
            viewIconGiaHanXuLy: "ពន្យាពេល",
            no: "មិនយល់ព្រម",
            yes: "យល់ព្រម",
            btnInsertRelation: "ឯកសារទាក់ទង ...",
            btnInsertAttachment: "ឯកសារភ្ជាប់តាម",
            btnInsertScan: "ឯកសារ scan...",
            btnPaper: "អជ្ញាប័ណ្ណ ...",
            btnInsertAnticipate: "បំរុងបញ្ជូន ...",
            btnTransfer: "បញ្ជូនឯកសារ/សំណុំរឿង",
            btnEdit: "កែប្រែឯកសារ/សំណុំរឿង",
            btnInsertFile: "ភ្ជាប់តាម",
            btnApproverYes: "យល់ព្រមចុះសច្ចាប័ន",
            btnApproverNo: "មិនយល់ព្រមចុះសច្ចាប័ន",
            btnDestroy: "លុបចោលឯកសារ/សំណុំរឿង",
            viewIconKetthuc: "",
            btnFinishtt: "បញ្ចប់",
            btnAnswer: "ឆ្លើយតប",
            btnChangeDoctype: "បែងចែកតាមថ្នាក់",
            concurrency: "Vnd",
            UserComment: "អ្នកដោះស្រាយ",
            filename: "ឈ្មោះ file ",
            filesize: "ទំហំ file",
            fileversion: "កំណែ",
            lastUpdateFile: "ធ្វើទាន់សម័យចុងក្រោយ",
            FinalComment: "យោបល់ដោះស្រាយ",
            backtolist: "ត្រឡប់បញ្ជីឈ្មោះ",
            "delete": "លុបចោល",
            MainProcess: "ដោះស្រាយសំខាន់៖",
            CoProcess: "ដោះស្រាយទន្ទឹមនឹង៖",
            sendTo: "បញ្ជូនមក៖",
            thongbao: "សេចក្តីប្រកាស៖",
            xinykien: "សូមយោបល់៖",
            view: "មើល",
            download: "ទាញយក"
        },
        documentQuickView: {
            belowDocumentSum: "សង្ខេបព័ត៌មានឯកសារ",
            Comment: "យោបល់ដោះស្រាយ៖",
            timeComment: "នាពេល",
            Category: "ប្រភេទឯកសារ៖",
            Docfield: "វិស័យ៖",
            DocCode: "លេខសញ្ញាសម្គាល់៖",
            Result: "លទ្ធផលដោះស្រាយ",
            LastUserComment: "អ្នកដោះស្រាយចុងក្រោយ៖",
            Place: "ចម្លងជូនឯកសារ៖",
            Sign: "អ្នកចុះហត្ថលេខា៖",
            TotalPage: "ចំនួនទំព័រ៖",
        },
        transfer: {
            ChoseOtherUser: "ជ្រើសមន្រ្តីទទួលផ្សេង",
            MainProcessUser: "ទទួលច្បាប់ដើម",
            MainProcessUserComment: "(ដោះស្រាយសំខាន់ៗ)",
            CoProcessUser: "ទទួលច្បាប់ថតចម្លង",
            CoProcessUserComment: "(គួបផ្សំដោះស្រាយ)",
            AnnouceUser: "ទទួលសេចក្តីប្រកាស",
            AnnouceUserComment: "(ដើម្បីត្រួតមើល)",
            QuickSearchAccount: "ស្វែងរករហ័សគណនីរបស់ទិសបញ្ជូន",
            AnnouncementPlace: "ចម្លងជូនសេចក្តីប្រកាស",
            PrivateAnoun: "ទទួលសេចក្តីប្រកាស",
            ConsultContent: "ខ្លឹមសារសូមយោបល់",
            ConsultUser: "អ្នកសូមយោបល់",
            MainProcess: "ដោះស្រាយសំខាន់ៗ",
            CoProcess: "ដោះស្រាយទទឹមនឹង",
            dgUserLabel: "(ជ្រើសយកឯកជន អង្គភាពទទួលច្បាប់ថតចម្លង)",
            dgUser: "ឯកជន អង្គភាពទទួលច្បាប់ថតចម្លង",
            dgJobtitleLabel: "(ជ្រើសយកមុខងារ និងស្ថាប័នទទួលច្បាប់ថតចម្លង)",
            dgJobtitle: "មុខងារ",
            dgDeptJob: "ស្ថាប័ន",
            allJobs: "មុខងារទាំងឡាយ",
            sameDept: "អង្គភាពជាមួយគ្នា",
            isDg1: "សេចក្តីប្រកាស",
            isDg2: "ផ្ញើតាមរយៈព្រមជាមួយ",
            searchDgLabel: "ឯកជនទទួលច្បាប់ថតចម្លង",
            allJobTitlesForDept: "មុខនាទីទាំងឡាយ",
            jobtitlesDeptPopup: "មុខនាទីចំណុះស្ថាប័ន (អង្គភាព)",
            jobtitleForAll: "ថ្នាក់ទទួលច្បាប់ថតចម្លង (ដើម្បីជ្រាប)",
            allJobTitles: "មុខនាទីទាំងឡាយ",
            IsThongbao: "សេចក្តីប្រកាស",
            IsDxl: "ដោះស្រាយព្រមជាមួយគ្នា",
            IsAttachYk: "ភ្ជាប់តាមយោបល់ដោះស្រាយ",
        },
        attachment: {
            view: "មើល",
            open: "បើក file ភ្ជាប់តាម",
            del: "លុបចោល file ភ្ជាប់តាម"
        },
        storePrivate: {
            attachmentName: "ឯកសារ៖",
            descStorePrivateAttachment: "ពិពណ៌នា៖",
            storePrivateName: "ឈ្មោះសំណុំរឿង៖",
            storePrivateNameWarning: "បញ្ចូលឈ្មោះសំណុំរឿង",
            userJoined: "អ្នកចូលរួម៖",
            delJoined: "លុបចោល",
            descStorePrivate: "ផ្សេង៖",
            storePrivateName: "",
            storePrivateName: "",
        },
        relation: {
            open: "បើកឯកសារ",
            del: "លុបចោលឯកសារទាក់ទង",
            view: "ព័ត៌មានឯកសារលំអិត"
        },
        toolbar: {
            transferBtn: "បញ្ជូន",
            editBtn: "កែប្រែ",
            attachBtn: "ភ្ជាប់តាម",
            relation: "ឯកសារទាក់ទង ...",
            attachment: "ឯកសារភ្ជាប់តាម",
            scan: "ឯកសារ scan...",
            packet: "ដោះស្រាយតាមឡូ",
            paper: "អជ្ញាប័ណ្ណ ...",
            DuKienChuyen: "បំរុងបញ្ជូនទៅ ...",
            reloadBtn: "ទាញយកឡើងវិញ",
            allow: "យល់ព្រម",
            deny: "មិនយល់ព្រម",
            destroy: "យល់ព្រម",
            TiepNhanBoSung: "ទទួលបំពេញបន្ថែម",
            TraKetQua: "ស្នងលទ្ធផលឱ្យវិញ",
            finish: "បញ្ចប់",
            reply: "ឆ្លើយតប",
            PhanLoai: "បែងចែកតាមថ្នាក់",
            print: "បោះពុម្ភ",
            other: "ផ្សេងៗ",
            GiaHan: "ពន្យាពេល",
            YeuCauBoSung: "សំណូមពរបំពេញបន្ថែម ...",
            XinYKien: "សូំយោបល់ ...",
            btnAnnouncement: "សេចក្តីប្រកាស ...",
            btnSendAnswer: "ផ្ញើជូនយោបល់ ...",
            btnSaveStore: "រក្សាទុកក្នុងសៀវភៅ"
        },
        main: {
            news: "ព័ត៌មានបញ្ជាគ្រប់គ្រង",
            newEmail: "រៀបចំសំបូត្រ",
            config: "បង្កបង្កើត",
            reply: "ផ្ញើមតិត្រឡប់",
            smallSize: "មើលតាមទំហំតូច",
            mediumSize: "មើលតាមទំហំល្មម",
            largeSize: "មើលតាមទំហំធំ",
            underPreview: "មើលមុនខាងក្រោម",
            rightPreview: "មើលមុនខាងស្តាំ",
            hidePreview: "លាក់ទុកមើលមុន",
            reload: "ធ្វើដំណើរការឡើងវិញ",
            logout: "ចាកចេញ",
            searchDocument: "ស្វែងរកឯកសារ",
            searchFile: "ស្វែងរកឯកសារភ្ជាប់តាម",
            reloadMessage: "សម្រេចធ្វើទាន់សម័យ។ តើអ្នកចង់ទាញយកទំព័រឡើងវិញឬទេ?",
            closeBtn: "បិទ",
            submitBtn: "ធ្វើទាន់សម័យ",
            titleMessage: "សេចក្តីប្រកាស!",
            closeAll: "បិទទាំងអស់",
            report: "ធ្វើរបាយការណ៍ស្ថិតិ",
            contacts: "សៀវភៅទាក់ទង",
            calendar: "ប្រតិទិន",
            chat: "ជជែកកំសាន្ត",
            documents: "ដោះស្រាយលិខិត",
            bmail: "អ៊ីម៉ែល",
            notifications: "សេចក្តីប្រកាស",
            gtv: "វាយអក្សរវៀតណាម"
        },
        index: {
            storePrivate: "សំណុំរឿងការងារ",
            plugin: "ប្រើប្រាស់ជាក់ស្តែង",
            reportNode: "របាយការណ៍ស្ថិតិ",
            printNode: "បោះពុម្ភរហ័ស",
            //reportNode: "របាយការណ៍ស្ថិតិ",
            reload: "ធ្វើឱ្យស្របនឹងគ្នា"
        },
        setting: {
            title: "បង្កបង្កើតឯកជន",
            ProfileConfig: "ព័ត៌មានឯកជន",
            Changepassword: "ផ្លាស់ប្តូរពាក្យសម្ងាត់",
            UserSetting: "កំណត់រចនាសម្ព័ន្ធគ្រាប់ចុចកាត់",
            GeneralSettings: "កំណត់រចនាសម្ព័ន្ធផ្សេង",
            Signature: "កំណត់រចនាសម្ព័ន្ធហត្ថលេខា",
            btnUpdateSetting: "ធ្វើទាន់សម័យ",
            btnCloseSetting: "បិទ",
        },
        scan: {
            rotateLeft: "បង្វិលខាងឆ្វេង",
            rotateRight: "បង្វិលខាងស្តាំ",
            zoomIn: "ពង្រីកធំ",
            zoomOut: "ធ្វើឱ្យតូច",
            setActualSize: "រូបថតដើម",
            crop: "កាត់រូបថត",
            setBrightnessUp: "បង្កើនពន្លឺ",
            setBrightnessDown: "បន្ថយពន្លឺ",
            setContrastUp: "បង្កើនកំរិតពណ៌ផ្ទុយគ្នា",
            setContrastDown: "បន្ថយកំរិតពណ៌ផ្ទុយគ្នា",
            addToContent: "បញ្ចូលទៅក្នុងខ្លឹមសារ",
            pagePosition: "ទំព័រ៖ 0/0",
            preImage: "រូបថតមុខ",
            nextImage: "រូបថតបន្ទាប់",
            removeImageScan: "លុបចោល",
            removeAllImageScan: "លុបចោលទាំងអស់",
            listScannerLabel: "ជ្រើសរើសម៉ាស៊ីនស្កេន៖",
            reloadListScanner: "បើកឡើងវិញបញ្ជីឈ្មោះម៉ាស៊ីនស្កេន",
            colortype: "ប្រភេទពណ៌",
            pixeltype2: "ពណ៌",
            pixeltype0: "ពណ៌ប្រផេះ",
            pixeltype1: "ពណ៌ខ្មៅ-ស",
            resolution75: "75 dpi",
            resolution100: "100 dpi",
            resolution150: "150 dpi",
            resolution200: "200 dpi",
            resolution300: "300 dpi",
            duplex: "ស្កេនផ្ទៃមុខទាំងសងខាង",
            showui: "ប្រើប្រាស់ចំណុចប្រទាក់របស់ម៉ាស៊ីន scan",
            filename: "ឈ្មោះ file",
            imageformatLabel: "តំកល់ទុក file តាមប្រភេទ",
            imageformatJPEG: "JPEG",
            imageformatPNG: "PNG",
            imageformatGIF: "GIF",
            imageformatTIFF: "TIFF",
            imageformatBMP: "BMP",
            imageformatPDF: "PDF",
            imageformatDOC: "DOC",
            acquire: "ស្កេនរូប",
            refresh: "បើកឡើងវិញបញ្ជីឈ្មោះម៉ាស៊ីនស្កេន",
            colortype: "ប្រភេទពណ៌",
            color: {
                color: "ពណ៌",
                gray: "ពណ៌ប្រផេះ",
                whiteblack: "ពណ៌ខ្មៅ-ស"
            },
        },
        tab: {
            close: "បិទ tab"
        },
        search: {
            //search.html
            compendium: "ដកស្រង់",
            doccode: "លេខសញ្ញាសម្គាល់",
            inoutcode: "លេខមក",
            content: "ខ្លឹមសារ",
            category: "ប្រភេទ",
            keyword: "ពាក្យគន្លឹះ",
            urgent: "កំរិតប្រញាប់",
            categorybusiness: {
                name: "ជំនាញឯកទេស",
                all: "ទាំងអស់",
                "in": "ឯកសារមក",
                out: "ឯកសារទៅ",
                one: "សំណុំរឿងច្រកចេញចូលតែមួយ"
            },
            FromDateStr: "ពីថ្ងៃ",
            InOutPlace: "ស្ថាប័នផ្ញើ",
            OrganizationCreate: "ស្ថាប័នចុះផ្សាយ",
            DocField: "វិស័យ",
            UserSuccess: "អ្នកចុះហត្ថលេខា",
            CurrentUser: "អ្នកកំពុងរក្សាទុក",
            ToDateStr: "មកដល់ថ្ងៃ",
            showsearch: "បង្ហាញការស្វែងរកលំអិត",
            createdate: "ថ្ងៃបង្កបង្កើតថ្មី",
            createdate1: "ថ្ងៃបង្កបង្កើត",
            status: "ស្ថានភាព",
            //searchDocResult.html, searchResult.html
            status1: "កំពុងគ្រោងទុក",
            status2: "កំពុងដោះស្រាយ",
            status4: "បានបញ្ចប់",
            status8: "បានលុបចោល",
            status16: "ផ្អាកដោះស្រាយ",
            //searchTab.html
            search: "ស្វែងរក",
            order: "ល.រ",
            searchnotfound: "ពុំស្វែងរកបានលទ្ធផលសមស្របទេ",
            view: "មើល",
            download: "ទាញយក",
            DidYouMean: "អ្នកចង់ស្វែងរក",
            all: "ទាំងអស់",
            //search.html
            doccodePh: "បញ្ចូលលេខសញ្ញាសម្គាល់",
            inoutcodePh: "បញ្ចូលលេខមក",
            contentPh: "បញ្ចូលខ្លឹមសារ",
            keywordPh: "បញ្ចូលពាក្យគន្លឹះ",
            error: "មានកំហុសកើតឡើងពេលស្វែងរក។ សូមមេត្តាទាក់ទងនឹងអ្នកគ្រប់គ្រងបណ្តាញ",
            noresult: "មិនអាចរកបានលទ្ធផល",
            Compendiumph: "បញ្ចូលដកស្រង់របស់ឯកសារ",
            openattachmentfile: "បើក file ភ្ជាប់តាម",
            downloadattachmentfile: "ទាញយក file ភ្ជាប់តាម",
        }
    };


    //#region Version 1.1 - cần tìm lại bản dịch này

    egov.resources.common = {
        processing: "Đang xử lý...",
        loading: "Đang tải...",
        error: "Có lỗi xảy ra",
        searching: "Đang tìm kiếm",
        closeButton: "Đóng",
        addButton: "Thêm",
        editButton: "Sửa",
        updateButton: "Cập nhật",
        cancelButton: "Bỏ qua",
        deleleButton: "Xóa",
        confirmButton: "Xác nhận",
        alert: "Thông báo",
        transfering: 'Đang chuyển',
        currencyUnit: "Vnd"
    }

    egov.resources.tab = extend(egov.resources.tab, {
        home: {
            title: 'Văn bản'
        },
        report: {
            title: 'Báo cáo thống kê'
        },
        print: {
            title: 'In nhanh'
        },
        search: {
            title: 'Tìm kiếm'
        },
        setting: {
            title: 'Thiết lập'
        },

        saveChange: "Văn bản có thay đổi, bạn có muốn lưu lại không?"
    });

    egov.resources.file = {
        lenghtIsNotAllow: "File tải lên quá dung lượng quy định.",
        typeIsNotAllow: "File không đúng định dạng quy định.",
        errorUpload: "Có lỗi xảy ra khi tải tệp lên.",
        errorDownload: "Có lỗi xảy ra khi tải tệp xuống.",
    };

    egov.resources.home = {
        syncDataError: 'Có lỗi khi đồng bộ danh sách văn bản',
        documentPreView: {
            tooltip: {
                open: "Hiển thị tóm tăt văn bản/hồ sơ",
                close: "Ẩn tóm tăt văn bản/hồ sơ"
            },
            control: {
                close: "X",
                open: "open"
            }
        },
        docType: {
            message: {
                error: {
                    loading: "Không tải được danh sách loại văn bản!"
                }
            }
        }
    };

    egov.resources.treeDocument = {
        message: {
            confirm: {},
            success: {},
            error: {
                syncData: "Lỗi khi đồng bộ dữ liệu!",
            }
        }
    };

    egov.resources.treeStore = {
        nameStorePrivateRoot: 'Hồ sơ cá nhân',
        nameStoreShareRoot: 'Hồ sơ chia sẻ',
        title: {
            createStore: 'Tạo sổ hồ sơ',
            detailSotore: 'Xem chi tiết',
            addStorePrivateAttachment: 'Thêm tài liệu'
        },
        message: {
            confirm: {
                openStore: 'Bạn có chắc muốn mở hồ sơ này không?',
                closeStore: 'Bạn có chắc muốn đóng hồ sơ này không?',
                deleteStore: 'Bạn có chắc muốn xóa hồ sơ này không?',
            },
            success: {
                openStore: 'Mở hồ sơ thành công!',
                closeStore: 'Đóng hồ sơ thành công!',
                deleteStore: 'Xóa hồ sơ thành công!',
            },
            error: {
                createStore: 'Có lỗi trong quá trình tạo mới sổ hồ sơ',
                updateStore: 'Có lỗi trong quá trình cập nhật sổ hồ sơ',
                selectStore: ' Có lỗi xảy ra khi lấy dữ liệu',
                openStore: 'Có lỗi khi mở hồ sơ!',
                closeStore: 'Có lỗi khi đóng hồ sơ!',
                deleteStore: 'Có lỗi khi xóa hồ sơ!',
            }
        },
        contextmenu: {
            createStore: 'Tạo mới hồ sơ',
            updateStore: 'Cập nhật hồ sơ',
            deleteStore: 'Xóa hồ sơ',
            openStore: 'Mở hồ sơ',
            closeStore: 'Đóng hồ sơ',
            addStorePrivateAttachment: 'Thêm tài liệu',
            messageCloseStore: 'Bạn có chắc muộn đóng hồ sơ này?.',
            messageOpenStore: 'Bạn có chắc muốn mở hồ sơ này?.'
        }
    };

    egov.resources.documents = {
        title: {
            documentImportant: "Bỏ gắn quan trọng văn bản này",
            documentNotImportant: "Gắn quan trọng cho văn bản này",
            vanBanDongXuLy: "Văn bản đồng xử lý",
            vanBanSapHetHan: "Văn bản sắp hết hạn (còn 1 ngày)",
            vanBanKhanHoacQuaHanXuLy: "Văn bản khẩn hoặc quá hạn xử lý",
            vanBanQuaHanHoiBao: "Văn bản quá hạn hồi báo",
            vanBanHoaToc: "Văn bản hỏa tốc",
            vanBanThuong: "Văn bản bình thường",
            documentDetail: "Chi tiết văn bản/hồ sơ",
        },
        toolbar: {
            controlName: {
                all: "Xem tất cả",
                day: "ngày",
                page: "Trang",
                dateAppointed: "Ngày hết hạn",
                docTypeName: "Loại hồ sơ",
                documentImportant: "Xem văn bản quan trọng",
                documentUnread: "Xem văn bản chưa đọc",
                refresh: "Tải lại",
                dateReceived: "Ngày nhận",
                sortBy: "Sắp xếp theo",
                setting: "Cài đặt danh sách",
                preview: "Xem trước",
                menu: "Menu"
            }
        },
        contextmenu: {
            name: {
                xemvanban: 'Xem văn bản...',
                suavanban: 'Sửa văn bản...',
                guiykien: 'Gửi ý kiến...',
                xinykien: 'Xin ý kiến...',
                bangiao: 'Bàn giao...',
                thongbao: 'Thông báo...',
                laylaivanban: 'Lấy lại văn bản',
                xacnhanbangiao: 'Xác nhận bàn giao...',
                xacnhanxuly: 'Xác nhận xử lý...',
                yeucaubosung: 'Yêu cầu bổ sung...',
                tiepnhanbosung: 'Tiếp nhận bổ sung...',
                kyduyet: 'Ký duyệt...',
                ketthucxuly: 'Kết thúc xử lý',
                huyvanban: 'Hủy văn bản',
                capnhatketquaxulycuoi: 'Cập nhật kết quả xử lý cuối...',
                inphieutrinh: 'In phiếu trình lãnh đạo...',
                intomtat: 'In tóm tắt',
                capnhattiendo: 'Cập nhật tiến độ...',
                xoakhoiduthao: 'Xóa văn bản dự thảo',
                contextheodoi: 'Fix contextmenu theo dõi',
                dungxuly: 'Dừng xử lý...',
                giahanxuly: 'Gia hạn xử lý...',
                chitietvanban: 'Chi tiết văn bản/hồ sơ',
                danhdaudadoc: 'Đánh dấu đã đọc',
                danhdauchuadoc: 'Đánh dấu chưa đọc',
                movanban: 'Mở văn bản',
            }
        },
        page: {
            text: "Trang",
            document: "văn bản"
        },
        message: {
            error: {
                quickView: "Lỗi khi lấy thông tin văn bản!",
                documentNotExist: "Văn bản không tồn tại!.",
                documentNotSelectDelete: "Chưa chọn văn bản để xóa!.",
                pagging: "Có lỗi trong quá trình chuyển sang trang mới",
                loadNewerDocuments: "Có lỗi trong qua trình tải dữ liệu!",
                getDocumentDetail: "Văn bản không tồn tại"
            }
        }
    };

    egov.resources.document = extend(egov.resources.document, {
        toolbar: {
            noaction: "Không có hướng chuyển tiếp theo.",
            transferByDk: "Chuyển theo dự kiến",
            transferUserDk: "Chuyển người nhận dự kiến",
            controlName: {
                transferDoc: {
                    name: "Chuyển",
                    message: {
                        error: 'Có lỗi xảy ra khi tải danh sách hướng chuyển'
                    },
                    item: {
                        cancel: {
                            name: "Không tìm thấy hướng chuyển tiếp theo"
                        },
                        transferplan: {
                            name: 'Chuyển theo dự kiến'
                        },
                        newtransferplan: {
                            name: "Chuyển người nhận dự kiến"
                        }
                    }
                },
                edit: {
                    name: "Sửa",
                },
                insert: {
                    name: "Chèn",
                    message: {
                        error: 'Có lỗi xảy ra'
                    }
                },
                reload: {
                    name: "Tải lại"
                },
                approverYes: {
                    name: "Đồng ý"
                },
                approverNo: {
                    name: 'Từ chối'
                },
                remove: {
                    name: 'Hủy'
                },
                tiepNhanBoSung: {
                    name: 'Tiếp nhận bổ sung'
                },
                "return": {
                    name: 'Trả kết quả'
                },
                finish: {
                    name: 'Kết thúc'
                },
                traloi: {
                    name: "Trả lời",
                    hoso: "Hồ sơ mới",
                    document: "Văn bản mới",
                    message: {
                        error: 'Có lỗi xảy ra khi tải danh sách phân loại'
                    }
                },
                phanloai: {
                    name: "Phân loại",
                    callBackTitle: "Chọn loại văn bản/hồ sơ",
                    message: {
                        error: 'Có lỗi xảy ra khi tải danh sách phân loại'
                    }
                },
                print: {
                    name: "In",
                    message: {
                        error: 'Có lỗi xảy ra khi tải danh sách in!.'
                    }
                },
                giahan: {
                    name: "In",
                },
                tiepNhanBoSung: {
                    name: 'Yêu cầu bổ sung'
                },
                xinykien: {
                    name: 'Xin ý kiến'
                },
                thongbao: {
                    name: 'Thông báo'
                },
                guiykien: {
                    name: 'Gửi ý kiến'
                },
                savePrivateStore: {
                    name: 'Lưu sổ'
                },
                others: {
                    name: 'Khác'
                }
            }
        },
        content: {
            version: "Xem phiên bản của {0} cập nhật lúc {1}"
        },
        relation: {
            titleDialog: "Thêm văn bản liên quan",
            confirmRelationTitle: "Xác nhận gửi kèm văn bản liên quan",
            ignoreConfirm: "Luôn gửi, không hiển thị lại thông báo này lần sau.",
            contextmenu: {
                open: "Mở văn bản",
                "delete": "Xóa văn bản"
            },
            documentNotExist: "Văn bản không tồn tại!",
        },
        attachment: {
            uploading: "Đang tải tệp lên",
            uploadSuccess: "Tải tệp lên thành công!.",
            uploadError: "Có lỗi xảy ra khi tải tệp lên",
            fileChanged: "<strong>Tệp {0} có sự thay đổi</strong><br/>Bạn có muốn lưu lại không?",
            errorDownload: "Có lỗi xảy ra khi tải tệp.",
            openFile: "Mở file",
            deleteFile: "Xóa file",
            restoreFile: "Phục hồi file",
            download: "Tải về",
            removed: "Đã loại bỏ",
            using: "Đang sử dụng",
            version: "Phiên bản {0} cập nhật lúc {1}",
            closeProgramBeforeSave: "Bạn phải đóng các chương trình đang mở tệp đính kèm trước khi lưu.",
            fileIsRemoved: "File đã bị xóa"
        },
        transfer: {
            transferButton: "Chuyển",
            dialogTitle: "Bàn giao văn bản",
            noUser: "Chưa chọn người nhận xử lý",
            transferSuccess: "Chuyển văn bản thành công.",
            transferError: "Có lỗi trong quá trình bàn giao.",
            noUserByAction: "Hướng chuyển không có người nhận",
            sendAll: "Tất cả mọi người",
            answerSuccess: "Trả lời thành công.",
            answerFail: "Có lỗi trong quá trình trả lời ý kiến.",
            showDgTitle: "Hiển thị giao diện chọn cán bộ khác",
            noXlc: "Chưa chọn cán bộ xử lý",
            userList: "Danh sách nhận văn bản"
        },
        publishment: {
            dialogTitle: "Phát hành văn bản",
            privateDialogTitle: "Lưu sổ phát hành nội bộ",
            publishButton: "Lưu và Phát hành",
            noAddressSelected: "Bạn chưa chọn đơn vị nhận văn bản.",
            success: "Phát hành văn bản thành công.",
            error: "Có lỗi xảy ra khi phát hành. Vui lòng thử lại."
        },
        ChangeDoctype: {
            hasChangeDateAppoint: "Văn bản/hồ sơ đã được phân loại theo loại hồ sơ mới.</br>Bạn có muốn thay đổi hạn xử lý của văn bản/hồ sơ theo hạn xử lý của loại hồ sơ mới hay không?",
            success: "Văn bản/hồ sơ đã được chuyển sang loại văn bản {0}."
        },
        sendComment: {
            dialogButton: "Gửi ý kiến",
            dialogTitle: "Nhập ý kiến",
            enterComment: "Bạn chưa nhập ý kiến xử lý",
            sendFail: "Có lỗi xảy khi cho ý kiến, vui lòng thử lại.",
            sendSuccess: "Gửi ý kiến thành công",
            requireMessage: "Bạn chưa nhập ý kiến!"
        },
        announcement: {
            dialogTitle: "Thông báo",
            announcementButton: "Gửi thông báo",
            sendSuccess: "Gửi thông báo thành công.",
            sendFail: "Gửi thông báo lỗi, vui lòng thử lại.",
            noReceiver: "Bạn chưa chọn người nhận thông báo."
        },
        consult: {
            dialogTitle: "Xin ý kiến",
            consultButton: "Gửi xin ý kiến",
            sendSuccess: "Gửi xin ý kiến thành công.",
            sendFail: "Gửi xin ý kiến lỗi, vui lòng thử lại.",
            noReceiver: "Bạn chưa chọn người nhận xin ý kiến.",
            noComment: "Bạn chưa nhập ý kiến xử lý."
        },
        finish: {
            error: "Không kết thúc được văn bản, vui lòng thử lại.",
            success: "Kết thúc văn bản thành công",
            processing: "Đang xử lý"
        },
        docStore: {
            dialogTitle: "Lưu sổ cá nhân",
            createNew: "Tạo mới",
            saveButton: "Lưu",
            notSaveButton: "Không lưu",
            noChooseStore: "Bạn chưa chọn Sổ cá nhân",
            processing: "Đang lưu",
            success: "Lưu thành công",
            error: "Có lỗi xảy ra khi lưu, vui lòng thử lại"
        },

        hsmc: {
            documentResult: "Kết quả xử lý: ",
            noResult: "Chưa duyệt",
            resultOk: "Đã duyệt",
            resultDeny: "Không duyệt",
            removeResult: "Hủy"
        },
        supplementary: {
            title: "Yêu cầu bổ sung",
            requiredTitle: "Thông tin bổ sung",
            paper: "Giấy tờ bổ sung",
            fee: "Lệ phí bổ sung",
            noAdditional: "Dân không bổ sung",
            addPaper: "Thêm giấy tờ",
            addFee: "Thêm lệ phí",
            newDateAppointed: "Tính lại ngày hẹn trả",
            addDay: "Số ngày ",
            dateAppointed: "Ngày hẹn trả: ",
            supplementType: {
                renew: "Tính lại từ đầu",
                "continue": "Tiếp tục tính",
                add: "Cộng thêm ngày"
            },
            success: "Tiếp nhận bổ sung thành công",
            updateAndPrintButton: "Cập nhật và In biên nhận"
        },

        print: {
            text: "In"
        },

        renewals: {
            renewalsButton: "Gia hạn",
            renewalsAndPrintButton: "Gia hạn và In phiếu",
            dialogTitle: "Gia hạn xử lý",
            renewalsReason: "Lý do gia hạn",
            newDateAppoint: "Hạn xử lý mới",
            printTemplate: "Mẫu in",
            noPrintTemplate: "Không có mẫu in gia hạn"
        },

        updateLastResult: {
            ok: "Duyệt",
            deny: "Không duyệt",
            comment: "Ý kiến xử lý:",
            dialogTitle: "Cập nhật kết quả xử lý"
        },

        returnResult: {
            dialogTitle: "Trả kết quả",
            updateAndPrintButton: "In và trả kết quả",
            updateButton: "Trả kết quả",
            needToUpdateSupplementary: "Hồ sơ đang có yêu cầu bổ sung, Bạn cần cập nhật kết quả bổ sung trước:",
            needToUpdateLastResult: "Hồ sơ chưa có kết quả xử lý cuối cùng, Bạn cần cập nhật kết quả xử lý cuối:",
            resultOk: "Đồng ý",
            resultDeny: "Từ chối",
            result: "Kết quả xử lý: ",
            personGive: "Thông tin công dân nhận kết quả",
            finish: " Kết thúc xử lý hồ sơ sau khi trả kết quả",
            printTemplate: "Mẫu in"
        },

        documentOnline: {
            acceptSuccess: "Tiếp nhận văn bản đăng ký qua mạng thành công"
        },

        confirmDestroy: "Bạn có chắc muốn hủy văn bản này?",
        xlcLabel: "Xử lý chính: ",
        dxlLabel: "Đồng gửi: ",
        xykLabel: "Xin ý kiến: "
    });

    // y kien thuong dung
    egov.resources.templateComment = {
        titleDialog: "Mẫu ý kiến thường dùng",
        btnAddTemplateComment: "Thêm mẫu",
        btnSelected: "Chọn",
        table: {
            header: {
                content: "Nội dung",
                edit: "Sửa",
                "delete": "Xóa"
            }
        },
        addDialog: {
            title: "Thêm mẫu ý kiến thường dùng",
            btnCreate: "Tạo mới",
            errorMessage: "Bạn chưa mẫu nhập ý kiến!"
        },
        editDialog: {
            title: "Cập nhật mẫu ý kiến thường dùng",
            btnEdit: "Cập nhật",
            errorMessage: "Bạn chưa mẫu nhập ý kiến!"
        },
        contextmenu: {
            selected: "Chọn",
            edit: "Sửa/Xem thông tin",
            "delete": "Xóa"
        }
    };
    egov.resources.setting.AuthorizesSetting = "Cấu hình ủy quyền";


    //Phần chữ ký
    egov.resources.setting.signature = {
        titleCreate: "Thêm mới chữ ký",
        titleEdit: "Cập nhật chữ ký",
        configPossition: "Cấu hình vị trí đặt chữ ký",
        configOther: "Cấu hình khác",
        deleteMessage: 'Bạn có chắc muốn xóa cấu hình này',
        labelCreate: "Thêm mới",
        table: {
            header: {
                stt: "STT",
                configNameSignature: "Tên cấu hình",
                wordsNeedFind: "Từ cần tìm",
                findTypes: "Loại tìm kiếm",
                signTypes: "Loại ký",
                position: "Vị trí",
                edit: "Sửa",
                "delete": "Xóa"
            },
            body: {
                findTypeBottomToTop: "Dưới lên",
                findTypeTopToBottom: "Trên xuống",
                imageSignature: "Chữ ký ảnh",
                textSignature: "Chữ ký dạng ký tự",
                leftPosition: "Bên trái",
                abovePosition: "Bên trên",
                rightPosition: "Bên phải",
                belowPosition: "Bên dưới",
                noData: "Không có dữ liệu"
            },
        }
    };

    //Phần ủy quyền
    egov.resources.setting.authorize = {
        titleCreate: "Thêm mới người nhận ủy quyền",
        titleEdit: "Cập nhật chữ ký",
        labelCreate: "Thêm mới",
        titleDialogDelete: "Thông báo!",
        confirmDelete: "Bạn có chắc muốn xóa cấu hình này",
        btnSubmitDelete: "Đồng ý",
        btnCancelDelete: "Hủy",

        table: {
            header: {
                stt: "STT",
                nameDocType: "Tên loại hồ sơ",
                userReceive: "Người nhận ủy quyền",
                startDate: "Ngày bắt đầu",
                endDate: "Ngày hết hạn",
                state: "Trạng thái",
                edit: "Sửa",
                "delete": "Xóa"

            },
            body: {
                noData: "Không có dữ liệu"
            }
        }
    };

    //cấu hình chung
    egov.resources.setting.general = {
        page: "Phần trang",
        scrollLoadData: "Cuộn chuột để tải dữ liệu",
        pagingLoadData: "Phân trang tải dữ liệu",
        showDetailDocument: "Hiển thị chi tiết văn bản",
        showQuickView: "Hiển thị tóm tắt văn bản"
    };

    egov.resources.setting.profile = {
        avatar: "Ảnh đại diện",
        choseAvatar: "Chọn"
    };

    egov.resources.setting.login = {
        account: "Tài khoản:",
        password: "Mật khẩu:",
        keepingLogin: "Duy trì đăng nhập!",
        loginType: "Hình thức đăng nhập",
        forgetPassword: "Quên mật khẩu",
        choseServicer: "Hãy chọn 1 nhà cung cấp dịch vụ OpenID:",
        loading: "Đang xử lý...",
        btnLogin: "Đăng nhập",
        title: "ĐĂNG NHẬP"
    };

    egov.resources.requiredSupplementary = {
        addRequiredTitle: "Thêm mẫu yêu cầu bổ sung"
    };

    egov.resources.main.links = "Liên kết";

    egov.resources.tree = {
        displayUnRead: "Có {0} văn bản chưa đọc",
        displayUnReadOnAll: "{0} chưa đọc / tổng số {1} văn bản",
        displayAll: "Có tất cả {0} văn bản",
    };
    //#endregion

    //#region Version 1.2 - Đã dịch không thêm resource mới vào đây

    egov.resources.searching = {
        result: "លទ្ធផលស្វែងរក"
    };

    egov.resources.common = extend(egov.resources.common, {
        messageYesBtn: "មាន",
        messageNoBtn: "គ្មាន",
        messageCancelBtn: "ចោល",
        messageOkBtn: "យល់ព្រម",
        errorMessage: "មានកំហុសកើតឡើង សូមមេត្តាសាកឡើងវិញ ឬ រាយការណ៍ជូនអ្នកគ្រប់គ្រង"
    });

    egov.resources.transfer = extend(egov.resources.transfer, {
        HasNoneDocument: "អ្នកមិនទាន់ជ្រើសរើសឯកសារ!",
        messageNoBtn: "គ្មាន",
        messageCancelBtn: "ចោល",
        messageOkBtn: "យល់ព្រម",
    });

    egov.resources.document = extend(egov.resources.document, {
        errorLoadPrivateStore: "មានកំហុសកើតឡើង នៅពេលទាញយកបញ្ជីឈ្មោះសំណុំរឿងបុគ្គល",
        saveSuccess: "សម្រេចការរក្សាទុកសំណុំរឿង!",
        ignoreConfirmRelation: "មិនសួរឡើងវិញទៀត",
        ignoreConfirmRelationWarning: 'អាចកែសម្រួល config នេះឡើងវិញ ដោយរបៀបចូល-បង្កើត -> ចំណុចប្រទាក់ផ្សេង -> ចោល "ផ្ញើឯកសារភ្ជាប់តាមភ្លាម"',
        checkAll: "ជ្រើសរើសទាំងអស់",
        displayAllComment: "ត្រួតមើលយោបល់ទាំងឡាយ",
        displayOnly3Coment: "លាក់ទុកយោបល់ដោះស្រាយខ្លះ",
        anticipate: "គ្រងទុកបញ្ជូនទៅ",
        addAnticipate: "បន្ថែមគ្រងទុក",
        require: "សំណូមពរ",
        hasSpellError: 'រកឃើញកំហុសពេលសរសេរអក្សរ។ ជ្រើសរើស "' + egov.resources.common.messageYesBtn + '" បើសិនចង់បន្តទៀត, ជ្រើសរើស "' + egov.resources.common.messageNoBtn + '" បើសិនចង់កែប្រែកំហុសសរសេរអក្សរ។ ',
        errorSpell: {
            add: "បន្ថែមចូលបណ្ណាល័យសរសេរអក្សរ",
            addSuccess: "សម្រេចការបន្ថែម",
            addError: "មានកំហុសកើតឡើង"
        },
        notpermission: "អ្នកឥតមានសិទ្ធិមើលឯកសារនេះទេ!",
    });

    egov.resources.documentQuickView.noDocumentSelected = "ជ្រើសរើសឯកសារ ដើម្បីបង្ហាញឡើង ពត៌មានសង្ខេបនៅទីនេះ។";
    egov.resources.documents.noDocumentCopyItem = "ល្អអស្ចា្យ! អ្នកគ្មានឯកសារណាមួយក្នុងផ្នែកនេះ។";

    egov.resources.documents.notFound = "បញ្ជីឈ្មោះឯកសារបច្ចុប្បន្ន គ្មានលទ្ធផលសមស្រប។ ចុច Enter ដើម្បីស្វែងរកលើទូទាំងប្រព័ន្ធ";

    egov.resources.documents.documentNumberDayOverdue = "QH {0} ថ្ងៃ"; //លើសពេលកំណត់
    egov.resources.documents.validDocuments = "នៅ {0} ថ្ងៃ";
    egov.resources.documents.validDocumentsInToday = "ថ្ងៃនេះ";

    egov.resources.file = extend(egov.resources.file, {
        maxLength: "ចំណុះបន្ទុកអតិបរមា៖ ",
        notAcceptFileTypes: "ប្រភេទឯកសារនេះមិនអនុញ្ញាតឱ្យចេញផ្សាយ"
    });

    egov.resources.main = extend(egov.resources.main, {
        administrator: "គ្រប់គ្រងប្រព័ន្ធ",
        reloadMessage: "ការបង្កើតថ្មីមួយចំនួន ចាំបាច់ត្រូវតែ reload ប្រព័ន្ធឡើងវិញ។ បើអ្នកចង់ reload ឡើងវិញទេ?",
        messageNoBtn: "អតទេ!",

        emptyMailNotifications: "អ្នកគ្មានប្រកាសអ៊ីម៉ែលណាមួយទេ",
        openAllMail: "បើកគ្រប់អ៊ីម៉ែលដែលបានទទួល",

        emptyChatNotifications: "អ្នកគ្មានសារចរចារណាមួយទេ",
        openAllChat: "បើកគ្រប់សារចរចារដែលបានទទួល",

        emptyDocumentNotifications: "អ្នកគ្មានការប្រកាសស្តីពីឯកសារណាមួយទេ!",
        openAllDocument: "បើកគ្រប់ឯកសារដែលបានប្រកាស",
        downloaddesktopversion: "ទាញយកកំណែ desktop",
        conversion: "សន្ទនា",

        bmail: "ពត៌មានបញ្ជាណែនាំ",
        documents: "ដោះស្រាយឯកសារ",
        chat: "Chat",
        calendar: "ប្រតិទិន",
        links: "សម្ព័ន្ធ",
        report: "របាយការណ៍ស្ថិតិ",
        notJqueryAlert: "មិនទាន់មាន file jquery។ សូមមេត្តាទាញយកបន្ថែម file jquery!",
        lblDocument: "ឯកសារ",
        lblNewConversion: "សន្ទនា",
        lblNewWorkTime: "បង្កើតប្រតិទិន",
        lblNewMail: "រៀបចំសំបុត្រ",
        searchDocument: "ស្វែងរកពត៌មានសំណុំរឿង ទិន្នន័យ ឯកសារភ្ជាប់តាម",
        searchMail: "ស្វែងរក mail",
        youHave: "អ្នកទទួលបាន",
        unreadDocuments: "ឯកសារមិនទាន់ត្រួតមើល",
    });

    egov.resources.toolbar = extend(egov.resources.toolbar, {
        DuKienPhatHanh: "គ្រងទុកផ្សព្វផ្សាយ",
        addnewtemplate: "បន្ថែមគំរូថ្មី",
    });

    egov.resources.attachment = extend(egov.resources.attachment, {
        download: "ទាញយក",
        notPermision: "អ្នកឥតមានសិទ្ធិអនុវត្តន៍សកម្មភាពនេះ"
    });
    egov.resources.setting.usersetting = {
        document: "ឯកសារ សំណុំរឿង",
        shortkey: "ប៊ូតុងចុចកាត់",
        documentdefaultname: "ឈ្មោះឯកសារ សំណុំរឿងកំណត់ពីមុន",
        supportkey: "ប៊ូតុងចុចគាំទ្រ",
        fnname: "ឈ្មោះតួនាទី",
        generalconfig: "ចំណុចប្រទាក់ទូទៅ",
        selectdocument: "ជ្រើសយកឯកសារ សំណុំរឿង",

    };
    egov.resources.document = extend(egov.resources.document, {
        openError: "មិនអាចបើកឯកសាទាក់ទិនបានទេ",
        configError: "ចំណុចប្រទាក់មិនត្រឹមត្រូវ សូមមេត្តាសាកឡើងវិញ",
        saveViolateSuccess: "ទទួលស្គាល់ CBLC បានសម្រេច",
        table: {
            sttរ: "ល.រ",
            creater: "អ្នករៀបចំ",
            datecreate: "ថ្ងៃធ្វើឡើង",
            exprisedate: "ថ្ងៃផុតកំណត់កាល",
            lastcomment: "យោបល់ដោះស្រាយចុងក្រោយ",

        },
        require: "សំណូមពរ"
    });

    egov.resources.time = {
        date: "ថ្ងៃ",
        _date: "ថ្ងៃ",
        minute: "នាទី",
        _minute: "នាទី",
        mon: "ថ្ងៃច័ន្ទ",
        tue: "ថ្ងៃអង្គារ",
        wed: "ថ្ងៃពុធ",
        thi: "ថ្ងៃព្រហស្បតិ៍",
        fri: "ថ្ងៃសុក្រ",
        sat: "ថ្ងៃសៅរិ៍",
        sun: "ថ្ងៃអាទិត្យ",
        morning: "ពេលព្រឹក",
        affternoon: "ពេលរសៀល",

    },

    egov.resources.enumResource = {
        columntable: {
            doccode: "លេខកូដសំណុំរឿង",
            datereceived: "ថ្ងៃទទួល",
            compendium: "ដកស្រង់",
            datecreate: "ថ្ងៃធ្វើឡើង",
            email: "E-mail",
            creator: "អ្នករៀបចំឡើង",
            dateapoint: "ថ្ងៃផុតកំណត់កាល",
            lastcomment: "យោបល់ដោះស្រាយចុងក្រោយ",
            docnumber: "លេខសញ្ញាសម្គាល់",
        },
        actionlevel: {
            levelone: "កំរិតទី 1",
            leveltwo: "កំរិតទី 2",
            levelthree: "កំរិតទី 3",
            levelfour: "កំរិតទី 5",
        },
        activitylogtype: {
            dangnhap: "ចុះឈ្មោះជ្រាតចូល",
            dangxuat: "ចាកចេញក្រៅ",
            bangiao: "ប្រគល់-ទទួលឯកសារ",
            thongbao: "ប្រកាសឯកសារ",
            huyvanban: "លុបចោលឯកសារ",
            ketthucvanban: "បញ្ចប់ឯកសារ",
            phanloai: "បែងចែកឯកសារតាមថ្នាក់នីមួយៗ",
            phathanh: "ចេញផ្សាយឯកសារ",
            kyduyet: "ចុះសច្ចាប័នឯកសារ",
            xinykien: "សូមយោបល់",
            guiykien: "ផ្ញើយោបល់",
            tiepnhan: "ទទួល",
            xingiahan: "ស្នើព្យាពេល",
            chuyenykiendonggop: "បញ្ជូនយោបល់រួមចំណែក",
        },
        categorybusinesstypes: {
            vbden: "ឯកសារមក",
            vbdi: "ឯកសារបញ្ជូនទៅ",
            hsmc: "សំណុំរឿងច្រកចេញចូលតែមួយ",
        },
        dailyprocesstimeforsearch: {
            allday: "ទាំងម្ងៃ",
            thirtyminutes: "30 នាទី មុននេះ",
            onehour: "1 ម៉ោង មុននេះ",
            twohour: "2 ម៉ោង មុននេះ",
            am: "ពេលព្រឹក",
            pm: "ពេលរសៀល",
        },
        datetimereport: {
            trongngay: "ប្រចាំថ្ងៃ",
            trongtuan: "ប្រចាំសប្តាហ៍",
            tuantruoc: "សប្តាហ៍មុន",
            trongthang: "ប្រចាំខែ",
            thangtruoc: "ខែមុន",
            quy1: "ត្រីមាសទី 1",
            quy2: "ត្រីមាសទី 2",
            quy3: "ត្រីមាសទី 3",
            quy4: "ត្រីមាសទី 4",
            trongnam: "ប្រចាំឆ្នាំ",
            namtruoc: "ឆ្នាំមុន",
            tatca: "ទាំងអស់",
            tuychon: "តាមជម្រើស",
        },
        displaynotify: {
            hide: "មិនបង្ហាញ notify",
            shownotifyinprocess: "បង្ហាញ notify ឯកសាររងចាំដោះស្រាយ",
            all: "បង្ហាញគ្រប់ notify ឯកសារទាក់ទិន",
        },
        displaytreetype: {
            none: "មិនបង្ហាញ",
            unread: "ឯកសារមិនទាន់អំណាន",
            unreadonall: "មិនទាន់អំណាន / ទាំងអស់",
            all: "ទាំងអស់",
        },
        documentprocesstype: {
            tiepnhan: "ទទួលយក",
            bangiao: "ប្រគល់",
            kyduyet: "ចុះសច្ចាប័ន",
            traketqua: "សងលទ្ធផលឱ្យវិញ",
            tiepnhanbosung: "ទទួលយកបន្ថែម",
            giahan: "ពន្យាពេល",
        },
        documenttype: {
            thongbao: "សេចក្តីប្រកាស",
            congvan: "សារលិខិត",
        },
        egovjobenum: {
            indextimerelapsed: "IndexTimerElapsed",
            checkservices: "ត្រួតពិនិត្យឡើងវិញបណ្តា service ដែលមិនធ្វើសកម្មភាព",
            getdocumentsfromedoctool: "ត្រួតពិនិត្យឡើងវិញ តើមានឯកសារថ្មីៗទេ",
            notifydocunread: "Notify បណ្តាឯកសារមិនទាន់អំណាន",
            notifydocinprocesses: "Notify បណ្តាឯកសាររងចាំដោះស្រាយ",
            checkchangedfile: "ត្រួតពិនិត្យ file ត្រូវផ្លាស់ប្តូរយោបល់",
            addindex: "បញ្ចូល index ស្វែងរក",
        },
        feetype: {
            indextimerelapsed: "ទទួលយក",
            thuongbosung: "តែងតែបំពេញបំបន្ថែម",
            tracongdan: "សងឱ្យប្រជាពករដ្ឋ",
        },
        leveltype: {
            sobannganh: "មន្ទី, ស្ថាប័ន",
            quanhuyen: "ខណ្ឌ, ស្រុក",
            phuongxa: "ឃុំ, សង្កាត់",
        },
        licensestatus: {
            capmoi: "ផ្តល់ថ្មី",
            capdoi: "ប្តូរ ផ្តល់បន្ថែម",
            thuhoi: "រឹបអូស",
        },
        option: {
            documentonlinereg: "ចុះឈ្មោះផ្ទាល់លើបណ្តាញអ៊ីនថើណែត ដែលធ្លាប់មានគណនី",
            documentonlineregnoaccount: "ចុះឈ្មោះផ្ទាល់លើបណ្តាញអ៊ីនថើណែត ដែលមិនទាន់មានគណនី",
            acceptdoconline: "យល់ព្រមនៅពេលចុះឈ្មោះផ្ទាល់លើបណ្តាញអ៊ីនថើណែត",
            implementdoconline: "សំណូមពរបន្ថែមនៅពេលចុះឈ្មោះផ្ទាល់លើបណ្តាញអ៊ីនថើណែត",
            rejectdoconline: "មិនយល់ព្រមនៅពេលចុះឈ្មោះផ្ទាល់លើបណ្តាញអ៊ីនថើណែត",
        },
        papertype: {
            tiepnhan: "ទទួលយក",
            thuongbosung: "តែងតែបំពេញបន្ថែម",
            tracongdan: "សងឱ្យប្រជាពលរដ្ឋ",
        },
        permissiontypes: {
            ktao: "បង្កើតឯកសារថ្មី",
            xly: "ដោះស្រាយឯកសារ",
        },
        processfilterexpression: {
            groupby: "ប្រមូលផ្សំតាម",
            equal: "ស្មើ",
            custom: "ខុសគ្នា",
        },
        scheduletype: {
            hangphut: "រាល់នាទី",
            hanggio: "រាល់ម៉ោង",
            hangngay: "រាល់ថ្ងៃ",
            hangtuan: "រាល់សប្តាហ៍",
            hangthang: "រាល់ខែ",
        },
        searchtype: {
            document: "ស្វែងរកឯកសារ",
            file: "ស្វែងរកក្នុង file",
        },
        securitytype: {
            thuong: "ធម្មតា",
            mat: "សម្ងាត់",
            toimat: "យ៉ាងសម្ងាត់",
        },
        sendtype: {
            buudien: "ប្រៃសណីយ៍",
            email: "Email",
            fax: "Fax",
            traotay: "ប្រគល់ផ្ទាល់ដៃ",
        },
        servicestatus: {
            running: "កំពុងធ្វើដំណើរ",
            stoped: "កំពុងឈប់ផ្អាក",
            paused: "កំពុងឈប់ផ្អាកបណ្តោះអាសន្ន",
            accessdenied: "ឥតមានសិទ្ធិជ្រាតចូល service",
            notfound: "Service មិនទាន់តំឡើងក្នុងប្រព័ន្ធ",
        },
        specialkeyenum: {
            nguoidangnhap: "អ្នកបោះពុម្ពឆ្នោត",
            ngaythanghientai: "ថ្ងៃ ខែបច្ចុប្បន្ន",
            meetingtitle: "ប្រធានបទកិច្ចប្រជុំ",
            meetingresource: "ទីកន្លែងប្រជុំ",
            meetingdate: "រយៈពេលប្រជុំ",
            meetingtodate: "ពេលវាលាបញ្ចប់",
            meetingcreator: "អ្នករៀបចំកិច្ចប្រជុំ",
            meetingbody: "ខ្លឹមសារនៃកិច្ចប្រជុំ",
            meetingusers: "អ្នកចូលរួមក្នុងកិច្ចប្រជុំ",
            meetinglastupdate: "អ្នកធ្វើទាន់សម័យកិច្ចប្រជុំ",
        },
        supplementtype: {
            reset: "គិតគូពេលវាលាឡើងវិញ",
            'continue': "បន្តការដោះស្រាយ",
            fixeddays: "បន្ថែមថ្ងៃកំណត់",
        },
        templatetype: {
            phieuin: "ឆ្នោតបោះពុម្ព",
            email: "សំបុត្រអេឡិចទ្រនិច",
            sms: "សារ sms",
        },
        timerjobtype: {
            warning: "ព្រមាន",
            searchindex: "បញ្ចូលអាស័យដ្ឋានស្វែងរក",
            deletetempfile: "លុបចោលបណ្តា file បណ្តោះអាសន្ន",
        },
        urgent: {
            thuong: "ធម្មតា",
            khan: "ប្រញាប់",
            hoatoc: "យ៉ាងប្រញាប់",
        },
    }

    //#endregion

    //#region Layout
    egov.resources = extend(egov.resources, {
        documentNotifications: "ប្រកាសឯកសារ",
        emptyDocumentNotifications: "អ្នកគ្មានប្រកាសឯកសារណាមួយទេ!",
        openAllDocument: "បើកគ្រប់ឯកសារដែលបានប្រកាស",
        downloaddesktopversion: "ទាញយកកំណែ desktop",
        gtv: "របៀបសរសេរអក្សរ",
        notifications: "សេចក្តីប្រកាស",
        news: "ពត៌មានគ្រប់គ្រង",
        newEmail: "រៀបចំសំបុត្រ",
        config: "បងើ្កតថ្មី",
        reply: "បញ្ជូនយោបល់ឆ្លើយតប",
        smallSize: "ត្រួតមើលទំហំតូច",
        mediumSize: "ត្រួតមើលទំហំល្មម",
        largeSize: "ត្រួតមើលទំហំធំ",
        underPreview: "ត្រួតមើលពីមុនខាងក្រោម",
        rightPreview: "ត្រួតមើលពីមុនខាងស្តាំ",
        hidePreview: "លាក់ទុកត្រួតមើលពីមុន",
        reload: "ធ្វើចលនាឡើងវិញ",
        logout: "ចាកចេញក្រៅ",
        searchDocument: "ស្វែងរកឯកសារ",
        searchFile: "ស្វែងរក file ភ្ជាប់តាម",
        reloadMessage: "សម្រេចធ្វើទាន់សម័យ។ តើអ្នកចង់ទាញយកទំព័រឡើងវិញទេ?",
        closeBtn: "បិទ",
        submitBtn: "ធ្វើទាន់សម័យ",
        titleMessage: "សេចក្តីប្រកាស!",
        closeAll: "បិទទាំងអស់",
        report: "ស្ថិតិ",
        contacts: "សៀវភៅទាក់ទង",
        calendar: "ប្រតិទិន",
        chat: "Chat",
        documentslabel: "ដោះស្រាយសារលិខិត",
        placeholderSearch: "ស្វែងរកពត៌មាសំណុំរឿង ទិន្នន័យ ឯកសារភ្ជាប់តាម",

        administrator: "គ្រប់គ្រងប្រព័ន្ធ",
        links: "តភ្ជាប់",
        conversion: "សន្ទនា",
        reloadMessage: "ការតំឡើងមួយចំនួនត្រូវការ reload ប្រព័ន្ធឡើងវិញ។ តើអ្នកចង់ reload ឡើងវិញទេ?",
        messageNoBtn: "អត់ទេ",

        mailNotifications: "ប្រកាស mail",
        emptyMailNotifications: "ឥតមានប្រកាស mail ណាមួយទេ",
        openAllMail: "បើកគ្រប់ mail ដែលបានទទួល",

        chatNotifications: "ប្រកាស chat",
        emptyChatNotifications: "គ្មានប្រកាសសារណាមួយទេ",
        openAllChat: "បើកគ្រប់សារដែលបានទទួល",

        bmail: "ពត៌មានបញ្ជាណែនាំ",
        notJqueryAlert: "មិនទាន់មាន file jquery។ សូមមេត្តាទាញយកបន្ថែម file jquery!",
        lblDocument: "ឯកសារ",
        lblNewConversion: "សន្ទនា",
        lblNewWorkTime: "បង្កើតប្រតិទិន",
        lblNewMail: "រៀបចំសំបូត្រ",
        searchMail: "ស្វែងរក mail",
        youHave: "អ្នកទទួលបាន",
        unreadDocuments: "ឯកសារមិនទាន់ត្រួតមើល",
        "delete": "លុបចោល",

        setting: {
            title: "តំឡើងបុគ្គល",
            ProfileConfig: "ពត៌មានបុគ្គល",
            Changepassword: "ប្តូរពាក្យសម្ងាត់",
            UserSetting: "ចំណុចប្រទាក់ប៊ូតុងចុចកាត់",
            GeneralSettings: "ចំណុចប្រទាក់ផ្សេង",
            SignatureSetting: "ចំណុចប្រទាក់ហត្ថលេខា",
            btnUpdateSetting: "ធ្វើទាន់សម័យ",
            btnCloseSetting: "បិទ",
            AuthorizesSetting: "ចំណុចប្រទាក់ផ្ទេរសិទ្ធិ",
            signature: {
                titleCreate: "បន្ថែមហត្ថលេខាថ្មី",
                titleEdit: "ធ្វើទាន់សម័យហត្ថលេខា",
                configPossition: "ចំណុចប្រទាក់កន្លែងដាក់ហត្ថលេខា",
                configOther: "ចំណុចប្រទាក់ផ្សេង",
                deleteMessage: 'តើអ្នកចង់លុបចោលចំណុចប្រទាក់នេះមែនឬទេ?',
                labelCreate: "បន្ថែមថ្មី",
                table: {
                    header: {
                        stt: "ល.រ",
                        configNameSignature: "ឈ្មោះចំណុចប្រទាក់",
                        wordsNeedFind: "ពាក្យចង់ស្វែងរក",
                        findTypes: "ប្រភេទស្វែងរក",
                        signTypes: "ប្រភេទហត្ថលេខា",
                        position: "ទីកន្លែង",
                        edit: "កែប្រែ",
                        "delete": "លុបចោល"
                    },
                    body: {
                        findTypeBottomToTop: "ពីក្រោមឡើងលើ",
                        findTypeTopToBottom: "ពីលើចុះក្រោម",
                        imageSignature: "ហត្ថលេខាដោយរូបភាព",
                        textSignature: "ហត្ថលេខាប្រភេទតួអក្សរ",
                        leftPosition: "ខាងឆ្វេង",
                        abovePosition: "ខាងលើ",
                        rightPosition: "ខាងស្តាំ",
                        belowPosition: "ខាងក្រោម",
                        noData: "ឥតមានទិន្នន័យ"
                    },
                }
            },
            authorize: {
                titleCreate: "បន្ថែមថ្មីអ្នកទទួលផ្ទេរសិទ្ធិ ",
                titleEdit: "ធ្វើទាន់សម័យហត្ថលេខា",
                labelCreate: "បន្ថែមថ្មី",
                titleDialogDelete: "សេចក្តីប្រកាស!",
                confirmDelete: "តើអ្នកចង់លុបចោលចំណុចប្រទាក់នេះមែនឬទេ?",
                btnSubmitDelete: "យល់ព្រម",
                btnCancelDelete: "លុបចោល",

                table: {
                    header: {
                        stt: "ល.រ",
                        nameDocType: "ឈ្មោះប្រភេទសំណុំរឿង",
                        userReceive: "អ្នកទទួលផ្ទេរសិទ្ធិ",
                        startDate: "ថ្ងៃចាប់ផ្តើម",
                        endDate: "ថ្ងៃផុតកំណត់កាល",
                        state: "ស្ថានភាព",
                        edit: "កែប្រែ",
                        "delete": "លុបចោល"
                    },
                    body: {
                        noData: "ឥតមានទិន្នន័យ"
                    }
                }
            },
            general: {
                page: "ចែកទំព័រ",
                scrollLoadData: "រមូលកណ្តុរកុំព្យួទ័រដើម្បីទាញយកទិន្នន័យ",
                pagingLoadData: "ចែកទំព័រទាញយកទិន្នន័យ",
                showDetailDocument: "បង្ហាញឯកសារដោយលំអិត",
                showQuickView: "បង្ហាញឯកសារដោយសង្ខេប"
            },
            profile: {
                avatar: "រូបភាពតំណាង",
                choseAvatar: "ជ្រើសយក"
            },
            login: {
                account: "គណនី៖",
                username: "ឈ្មោះជ្រាតចូល",
                password: "ពាក្យសម្ងាត់៖",
                keepingLogin: "រក្សាការចុះឈ្មោះជ្រាតចូល!",
                loginType: "របៀបចុះឈ្មោះជ្រាតចូល",
                forgetPassword: "ភ្លេចពាក្យសម្ងាត់",
                choseServicer: "ចូរជ្រើសយកអ្នកផ្គត់ផ្គង់សេវ៉ា OpenID៖",
                loading: "កំពុងដោះស្រាយ...",
                btnLogin: "ចុះឈ្មោះជ្រាតចូល",
                title: "ចុះឈ្មោះជ្រាតចូល"
            },
            usersetting: {
                document: "ឯកសារ សំណុំរឿង",
                shortkey: "ប៊ូតុងចុចកាត់",
                documentdefaultname: "ឈ្មោះឯកសារ សំណុំរឿងកំណត់មុន",
                supportkey: "ប៊ូតុងចុចគាំទ្រ",
                fnname: "ឈ្មោះតួនាទី",
                generalconfig: "ចំណុចប្រទាក់ទូទៅ",
                selectdocument: "ជ្រើសយកឯកសារ សំណុំរឿង",
            }
        },
        common: {
            saveBtn: "រក្សាទុក",
            cancelBtn: "ផុតចោល"
        }
    });

    //#endregion

    //#region v1.0 cần tìm lại bản dịch này

    egov.resources = extend(egov.resources, {
        activityLog: {
            questionDelete: "Bạn có muốn xóa các nhật ký này không?",
            notChoice: "'Bạn chưa chọn nhật ký muốn xóa'"
        },
        level: {
            nodata: "Không có cấp hành chính nào",
        },
        license: {
            AddLicense: "Đăng ký",
            RegisterLicense: "Đăng ký bản quyền",
            customername: "Khách hàng",
            Phone: "Số điện thoại",
            Email: "Email",
            ToDate: "Ngày hết hạn",
            TotalUser: "Số tài khoản",
            key: "Mã kích hoạt",
            customername: "",

        },
        log: {
            logNotSelect: "Bạn chưa chọn nhật ký muốn xóa",
            deleteSelection: "Xóa nhật ký được chọn",
            detail: "Chi tiết nhật ký",
            //view chưa làm

        },
        notify: {
            noform: "Chưa có mẫu nào",
            nouse: "Không sử dụng",
            urgent: {
                name: "Độ khẩn",
                level1: "Bình thường",
                level2: "Khẩn",
                level3: "Thượng khẩn",
            },
            hasRead: "Được duyệt",
            alerttime: " phút (Đặt = 0 để gửi sms ngay khi có cuộc họp mới)",

        },
        office: {
            nooffice: "Không có cơ quan nào",

        },
        paper: {
            nopaper: "Không có giấy tờ nào",
            other: "Khác",
            list: "Danh sách giấy tờ",
            action: "Nghiệp vụ",
            docfield: "Lĩnh vực",
            doctype: "Loại hồ sơ",
            addpaper: "Thêm mới giấy tờ",
            updatepaper: "Cập nhật giấy tờ",

        },
        people: {
            nopeople: "Không có người dùng nào",
            peoplesearch: "Tìm kiếm tài khoản ",
        },
        position: {
            sorterror: "Có lỗi khi sắp xếp mức ưu tiên của chức vụ.",
            npposition: "Không có chức vụ nào",
        },
        //aaaaaaa
        printer: {
            addprinter: "Thêm mới máy in",
            editprinter: "Thiết lập cho máy in",
            nodata: "Không có máy in nào",
            notconnect: "Không kiểm tra kết nối được tới máy in!",
            nameisrequired: "Tên máy in không được để trống!",
        },
        processfunction: {
            selectcolor: "Chọn màu",
            user: "Người dùng",
            position: "Chức vụ",
            position1: "Phòng ban/Chức vụ",
            all: "Tất cả",
            failure: "Bạn chưa nhập đầy đủ thông tin trong phần cấu hình danh sách",
            setupforfilterlist: "Cấu hình danh sách bộ lọc cho node",
            setupforsamelist: "Cấu hình danh sách tương ứng với node",
            enternote: "(Dùng phím enter để xuống dòng (nếu ở dòng cuối cùng thì thêm dòng mới), dùng phím lên xuống để sắp xếp cột)",
            divrole: "Phân quyền",
            addnodefilter: "Thêm bộ lọc mới cho node",
            parameterlisthaverequirement: "Bạn chưa nhập đầy đủ thông tin trong phần danh sách tham số",
            docfieldnote1: " Nếu tham số có cột giá trị là DocFieldId thì hệ thống sẽ ",
            docfieldnote2: "ngầm hiểu là cây văn bản đó sẽ lọc theo lĩnh vực và loại hồ sơ",
            columnname: "Tên cột giá trị",
            paraname: "Tên tham số",
            updatenodetype: "Cập nhật loại node",
            document: "Hồ sơ công việc",
            addNode: "Thêm node mới",
            copyNode: "Copy node này",
            paste: "Dán node",
            "delete": "Xóa node này",
            confirmdeletenode: "Xóa 1 node sẽ xóa cả node con, bạn chắc chắn muốn xóa chứ?",
            deletenodesuccessfull: "Xóa node thành công!",
            nodata: "Không có loại node nào",
            nofilter: "Không có bộ lọc nào",
            nogroup: "Không có nhóm nào",
            alldocument: "---Tất cả loại văn bản, hồ sơ---",
            someinfoisrequired: "Bạn chưa nhập đầy đủ thông tin trong phần cấu hình danh sách",
        },
        question: {
            nodata: "Không có câu hỏi nào",
        },
        report: {
            //Chưa làm
            fileuploadrpt: "Chỉ cho phép tải lên tệp *.rpt",
            showguidewhenwritingsqlquery: "Hiển thị hướng dẫn soạn sql",
            showdatabygroup: "Cấu hình xem dữ liệu theo nhóm",
            showgroupinreporttree: "Cấu hình hiển thị nhóm lên cây báo cáo",
            _setting: "[cấu hình]",
            statisticSetting: "Cấu hình thống kê",
            permissionreadreport: "Phân quyền được xem báo cáo",
            privatesetting: "Cấu hình riêng",
            settingreport: "Sử dụng cấu hình báo cáo",
            nodata: "Không có nhóm báo cáo nào",
            deletesuccessfull: "Xóa báo cáo thành công",
            confirmdeletereport: "Bạn có chắc muốn xóa báo cáo này cùng với tất cả các báo cáo con của nó?",
        },
        notifications: {
            loading: "Đang tải...",
            haveError: "Có lỗi xảy ra",
            nolog: "Không có nhật ký nào",
            processing: "Đang xử lý...",
            emailSuccess: "Gửi email thành công!",
            emailError: "Gửi email không thành công!",
            sendEmailError: "Có lỗi khi gửi mail",
            queryEmbryonicForm: "Câu truy vấn không được để trống!",
            changeStatusEmbryonicForm: "Hệ thống không thay đổi được trạng thái",

        },
        resource: {
            nodata: "Không có resource nào",
            choosefileimport: "Chọn tệp import",
        },
        role: {
            nodata: "Chưa có người dùng nào",
            isallow: "Cho phép",
            rolename: "Tên quyền",
            nodatagroup: "Không có nhóm người dùng nào",

        },
        scopearea: {
            nodata: "Không có ScopeArea nào",
        },
        setting: {
            sendemailto: "Gửi email kiểm tra tới ",
            sendemailsuccess: "thành công!",
            sendemailfailure: "không thành công!",
            smtpsetting: "Cấu hình máy chủ SMTP",
            othersetting: "Cấu hình khác",
            location: {
                addlocation: "Thêm vị trí",
                editlocation: "Sửa vị trí",
                confirmdeletefilelocation: "Bạn chắc chắn muốn xóa vị trí lưu file này chứ?",
                canotdelete: "Vị trí lưu file này đã được sử dụng, bạn không được phép xóa.",
                listfilelocation: "Danh sác các nơi lưu file",
                nodata: "Chưa có cấu hình vị trí lưu file",
                canotdelete: "",

            },
            general: {
                finishdocument: "Kết thúc hồ sơ/văn bản",
                setting: "Cấu hình Page trang chủ",
                nofifysetting: "Cấu hình notify",
            },
            passwordpolicy: {
                checkpassword: "Kiểm tra cú pháp mật khẩu",
                lookaccount: "Khóa tài khoản",
                passworddeadtime: "Hết hạn mật khẩu",
                passwordchangehistory: "Lịch sử thay đổi mật khẩu",
            },

        },
        shared: {
            productname: "Bkav eGovernment - Quản trị khách hàng",
            systemtree: "Cây hệ thống",
            home: "Trang chủ",
            admincustomer: "Quản trị khách hàng",
        },
        store: {
            pts: "(Phụ trách sổ)",
            nouser: "Chưa có người dùng nào",
            tempforstore: "Danh sách mẫu cho sổ",
            alltempname: "Tất cả tên mẫu",
            notemp: "Không tồn tại mẫu nào!",
            _all: "[Tất cả]",
            nodocumentstore: "Không có sổ hồ sơ nào",

        },
        template: {
            nodata: "Không có mẫu nào",
            key: "Key dùng chung",
            systemerror: "Hệ thống lỗi, không thay đổi được trạng thái mẫu phiếu",

        },
        templatekey: {
            onoffguide: "Bật/tắt hướng dẫn",
            showguide: "Hiển thị hướng dẫn khi soạn mã key, sql, template",
            ex: "Vd:",
            needparameter: "Câu truy vấn phải có tham số",
            parametercanuseinquery: "Các tham số có thể sử dụng trong câu truy vấn",
            keyformat: "Định dạng key được phép",
            speccharacter: "{[a-zA-Z0-9_]+}",
            keyformat2: "bao gồm các chữ cái (hoa, thường), chữ số và dấu gạch chân (_).",
            getvalueintempdoc: "Lấy giá trị trong biểu mẫu nào của hồ sơ.",
            currentuserid: "Id người đăng nhập hiện tại.",
            doctype: "Loại giấy tờ.",
            costtype: "Loại lệ phí.",
            additiondoc: "Danh sách các giấy tờ bổ sung.",
            formatofresulequery: "Kết quả câu truy vấn được lấy theo dạng",
            fieldname: "ten_truong",
            sqlresult: "là kết quả của câu sql dạng",
            dataprocessfunctions: "Các hàm xử lý kiểu dữ liệu",
            exdataconvertfunctions: "Tất cả các hàm convert dữ liệu:",
            stringprocessingfuntions: "Tất cả các hàm xử lý chuỗi:",
            datefunctions: "Tất cả các hàm xử lý ngày tháng:",
            stringformats: "Tất cả các định dạng chuỗi:",
            viewdetail: "Xem chi tiết tại:",
            selectdocument: "Chọn loại hồ sơ",
            selecttemplate: "Chọn biểu mẫu",
            selectdocfield: "Chọn lĩnh vực",
            keycode: "Mã key",
            nokey: "Không có key nào",
        },

        time: {
            timenotcheck: "Không kiểm tra được thời gian",
            checkdate: "Kiểm tra ngày",
            caculateextendtime: "Tính lịch nghỉ bù",
            nodata: "Không có ngày nghỉ nào",
            repeat: "Lặp lại",
            repeatbyyear: "Lặp theo năm",
            freeday: "Tên ngày nghỉ",
            DL: "Ngày âm",
            AL: "Ngày dương",
            day: "Thứ",
            listofrestday: "Danh sách ngày nghỉ năm",
            weekworktime: "Thời gian làm việc trong tuần",

        },
        transfertype: {
            nodata: "Không có hình thức chuyển nào",
        },
        user: {
            username: "Tên đăng nhập",
            fullname: "Họ và tên",
            phone: "Số điện thoại",
            usernameexist: "Tên đăng nhập đã tồn tại",
            notindepartment: "Chưa thuộc phòng ban nào",
            rolename: "Tên quyền",
            groupname: "Tên nhóm",
            isadministrator: "Là quản trị",
            ismaindepartment: "Là phòng ban chính",
            position: "Chức danh",
            position1: "Chức vụ",
            departmentname: "Tên phòng",
            nodata: "Không có người dùng nào",
            all: "---Tất cả---",
            active: "Hoạt động",
            unactive: "Không hoạt động",
            confirmtoresetpassword: "Bạn có chắc chắn muốn reset mật khẩu cho tài khoản này không?",
            resetpasswordsuccess: "Reset mật khẩu thành công!",
            selectusertoimport: "Bạn phải chọn người dùng để import",
            importusersuccessfull: "Import người dùng thành công",
        },
        ward: {
            city: "Tỉnh/Thành phố:",
            district: "Quận/huyện:",
            nodata: "Không có xã/phường nào",
            updatedata: "Cập nhật xã/phường",
            datalist: "Danh sách xã/phường",
        },
        welcome: {},

        notifications: {
            searching: "Đang tìm ...",
            loading: "Đang tải ...",
            haveError: "Có lỗi xảy ra",
            nolog: "Không có nhật ký nào",
            processing: "Đang xử lý...",
            destroythisfilter: "Bạn muốn xóa bộ lọc này",
            post: "Thông báo",
            confirmdelete: "Bạn có chắc chắn muốn xóa không?",
            updatesuccessful: "Cập nhật thành công",
            createsuccessful: "Thêm mới thành công",
            downloaderror: "Tải file lỗi",
            choosefileimport: "Bạn phải chọn tệp import",
            importing: "Đang import...",
            dowloadfileerror: "Có lỗi khi tải file",
        },
        buttons: {
            select: "Chọn",
            selectAll: "Chọn tất",
            edit: "Sửa",
            "delete": "Xóa",
            orderedsort: "Sắp xếp lại thứ tự",
            orderedsave: "Lưu thứ tự",
            addfilter: "Thêm bộ lọc mới",
            addparameter: "Thêm tham số",
            save: "Lưu",
            confirm: "Xác nhận",
            back: "Quay lại",
        },
        tableheader: {
            stt: "STT",
            "function": "Chức năng",
            description: "Mô tả",
            form: "Mẫu",
            edit: "Sửa",
            select: "Chọn",
            "delete": "Xóa",
            type: "Loại",
            formname: "Tên mẫu",
            filtername: "Tên bộ lọc",
            columnname: "Tên cột",
            displayname: "Tên hiển thị",
            width: "Chiều rộng",
            allowsort: "Cho phép sắp xếp",
            sortcolumn: "Cột sắp xếp",
            sort: "Sắp xếp",
            type: "Kiểu",
            value: "Giá trị",
            name: "Name",
            domain: "Domain",
            ip: "ip",
            zone: "Vùng",
            isallow: "Cho phép",
            addordelete: "Thêm/Bỏ",
        },
        commonlabel: {
            list: "Danh sách",
            select: "Chọn",
            is: "Là",
            or: "Hoặc...",
            select: "Chọn",
            addcolumn: "Thêm cột",
            add: "Thêm",
            addnew: "Thêm mới",
            cancel: "Hủy bỏ",
            note: "*Lưu ý:",
            time: {
                date: "Ngày",
                _date: "ngày",
                minute: "Phút",
                _minute: "phút",
                mon: "Thứ 2",
                tue: "Thứ 3",
                wed: "Thứ 4",
                thi: "Thứ 5",
                fri: "Thứ 6",
                sat: "Thứ 7",
                sun: "Chủ nhật",
                morning: "Buổi sáng",
                affternoon: "Buổi chiều",

            },
            contact: "Liên hệ",
            errorpage: "Sorry, an error occurred while processing your request.",
            all: "Tất cả",
            email: "Email",
            sms: "SMS",
            printcard: "Phiếu in",
            vnconcurency: "vnd",
            reject: "Bỏ",
            yes: "Có",
            no: "Không",
            search: "Tìm",
            allow: "Cho phép",
            notallow: "Không cho phép",
            "delete": "Xóa",
        },
        sitemap: {
            config: "Cấu hình",
            general: "Thiết lập hệ thống",
            processfunction: "Cây văn bản",
            resource: "Tài nguyên",
            activitylog: "Nhật ký hành động",
            egovjob: "Timmer",
            config4: "Biểu mẫu",
            template: "Quản lý mẫu phiếu",
            templatekey: "Quản lý key",
            formgroup: "Quản lý nhóm biểu mẫu",
            form: "Quản lý biểu mẫu",
            embryonicform: "Quản lý mẫu phôi",
            config5: "Báo cáo",
            report: "Báo cáo động",
            notify: "Thông báo",
            config2: "Danh mục",
            categorybusiness: "Nghiệp vụ",
            docfield: "Lĩnh vực",
            doctype: "Loại văn bản",
            configworkflow: "Quy trình",
            category: "Loại văn bả",
            increase: "Nhảy số",
            code: "Bảng mã",
            store: "Sổ hồ sơ",
            catalog: "Danh mục tùy chọn",
            keyword: "Từ khóa",
            transfertype: "Hình thức gửi",
            time: "Thời gian làm việc",
            address: "Địa chỉ",
            config3: "Cơ cấu tổ chức",
            imfomation: "Cơ quan",
            department: "Phòng ban",
            jobtitles: "Chức danh",
            position: "Chức vụ",
            user: "Cán bộ",
            role: "Vai trò và quyền hạn",
            authorize: "Ủy quyền",
            client: "Client",
            people: "Người dùng",
            scopearea: "Vùng truy cập",
            config6: "eGovOnline",
            office: "Cơ quan",
            law: "Văn bản quy phạm",
            guide: "Hướng dẫn",
            question: "Câu hỏi",
        }
    })
    //#endregion

    //#region v1.1 đã dịch không thêm mới vào đây

    egov.resources = extend(egov.resources, {
        crystalreport: {
            copyfromstatisticform: "ចម្លងពីគំរូស្ថិតិ",
            copyfromreportform: "ចម្លងពីគំរូរបាយការណ៍",
            reconfig: "ធ្វើ Config ឡើងវិញពីដើម",
        },
        doctype: {
            othernodes: "បណ្តាចំណុចផ្សេង",
            addcontrol: "បន្ថែម control",
            downloadworkflowerror: "មានកំហុសនៅពេលទាញយកដំណើរការ",
            newworkflow: "បន្ថែមដំណើរការថ្មី",
            workflownameisrequired: "អ្នកត្រូវតែដាក់បញ្ចូលឈ្មោះដំណើរការ",
            pasteworkflow: "ដាក់បន្ថែមដំណើរការ",
            usethisworkflow: "ប្រើប្រាស់ដំណើរការនេះ",
            comfirmtocancel1: "តើអ្នកពិតជាចង់",
            comfirmtocancel2: "លុបចោល ",
            comfirmtocancel3: "ប្រើប្រាស់ដំណើរការនេះឬទេ?",
            update: "ធ្វើទាន់សម័យបញ្ជី",
            editTemplateWorkflow: "ផ្សេង",
            updatenode: "ធ្វើទាន់សម័យ node",
            interfaceconfig: "ចំណុចប្រទាក់ផ្ទាំងរូបភាព",
            editthisworkflow: "កែប្រែដំណើរការនេះ",
            copythisworkflow: "Copy ដំណើរការនេះ",
            deletethisworkflow: "លុបចោលដំណើរការនេះ",
            confirmtodeltethisworkflow: "តើអ្នកចង់លុបចោលដំណើរការនេះមែនឬទេ?",
            notyouthisworkflow: "លុបចោលការប្រើប្រាស់ដំណើរការនេះ",
            workflowname: "ឈ្មោះដំណើរ",
            exprisedate: "កំណត់ពេលដោះស្រាយ",
            date: "ថ្ងៃ",
            addtemplateform: "បន្ថែមគំរូដំបូង",
            noformdata: "ឥតមានគំរូណាមួយទេ",
            doctypename: "ឈ្មោះសំណុំរឿង៖ ",
            docfield: "វិស័យ៖",
            status: "ស្ថានភាព៖ ",
            active: "បានធ្វើឱ្យសកម្ម",
            notactive: "មិនបានធ្វើឱ្យសកម្ម",
            noconfiguration: "មិនទាន់មានចំណុចប្រទាក់",
            confirmtodeletereceivenode: "តើអ្នកយល់ព្រមលុបចោល node បានផ្ញើមក នេះទេ?",
            objectype: "ប្រភេទមុខសញ្ញា",
            value: "តម្លៃ",
            'delete': "លុបចោល",
            receivepostlist: "បញ្ជីឈ្មោះទទួលប្រកាស",
            removeReceiveList: "តើអ្នកយល់ព្រមលុបចោលបញ្ជីឈ្មោះប្រកាសនេះឬទេ?",
            of: "របស់",
            allpeople: "មនុស្សគ្រប់រូប",
            level: "ថ្នាក់",
            user: "អ្នកប្រើ",
            alljobtitle: "គ្រប់មុខងារ",
            alljobtitleof: "គ្រប់មុខងាររបស់ ",
            alljobtitleof1: "គ្រប់ ",
            alljobtitleof2: " របស់ ",
            jobtitledepartment: "មុខងារ-ស្ថាប័ន",
            alluserof: "គ្រប់ user របស់ ",
            fieldisrequired: "អ្នកត្រូវតែដាក់បញ្ចូលតម្លៃ",
        },
        form: {
            updateform: "ធ្វើទាន់សម័យប្រភេទសំណុំរឿង ឯកសារ",
            formname: "ឈ្មោះគំរូ",
            description: "ពិពណ៌នា",
            tempkey: "គំរូអំប្រ៊ីយ៉ុង",
            status: "ស្ថានភាព",
            config: "ចំណុចប្រទាក់",
            status1: " កំពុងប្រើប្រាស់",
            status1: " មិនប្រើប្រាស់",
            status3: " គំរូរក្សាទុកបណ្តោះអាសន្ន",
            configformtitle: "Title",
            addtitle: "បន្ថែមចំណងជើង (ស្លាកសញ្ញា)",
            showgrid: "បង្ហាញឡើងបន្ទាត់",
            shownumber: "បង្ហាញឡើងលេខ",
            addrow: "បន្ថែមបន្ទាត់",
            chooseextendfield: "- - - ជ្រើសយកទីវាលបន្ថែម - - - ",
            choosedocumenttype: "- - - ជ្រើសយកប្រភេទឯកសារ - - - ",
            title: "ចំណងជើង (ស្លាកសញ្ញា)",
            brand: "ក្រឡាដាក់បញ្ចូលទិន្នន័យ (ស្លាកសញ្ញា)",
            references: "ទំនាក់ទំនង",
        },
        guide: {
            nodata: "ឥតមានសេចក្តីណែនាំណាមួយ"
        },
        increase: {
        },
        infomation: {
            chageinfo: "ផ្លាស់ប្តូរពត៌មាន",
        },
        jobtitles: {
            nodata: "ឥតមានមុសងារណាមួយទេ",
            dragordroptosort: "អនុញ្ញាតឱ្យរៀបចំលេខរៀងតាមរបៀបទាញហើយដាក់ចូល",
            sorterror: "មានកំហុសនៅពេលរៀបចំកំរិតអទិភាពរបស់មុសងារ។",
        },
        editor: {
            deletecol: "លុបចោលក្រឡា",
            deleterow: "លុបចោលបន្ទាត់",
            insertabove: "ដាក់បន្ថែមខាងលើ",
            insertbelow: "ដាក់បន្ថែមខាងក្រោម",
            insertleft: "ដាក់បន្ថែមខាងឆ្វេង",
            insertright: "ដាក់បន្ថែមខាងស្តាំ",
            merge: "រួមផ្សំតែមួយ",
            splitcolumn: "ចែកក្រឡា",
            splitrow: "ចែកបន្ទាត់",
            update: "ធ្វើទាន់សម័យ",
            all: "[ទាំងអស់]"

        },
        buttons: {
            deleteall: "លុបចោលទាំងអស់",
            search: "ស្វែងរក",
            agree: "យល់ព្រម",
            ignore: "មើលរំលង",
            add: "បន្ថែម",
            view: "ត្រួតមើល"
        },
        commonlabel: {
            other: "ផ្សេង",
            haveerrortryagain: "មានកំហុសកើតឡើង សូមមេត្តាសាកមើលឡើងវិញ!"
        }
    });

    //#endregion

    //#region V1.2 đã dịch không thêm mới vào đây

    egov.resources = extend(egov.resources, {
        setting: {
            general: {
                loadpagescroll: "រមូលទំព័រ",
                loadpagesize: "ចែកទំព័រ",
                language: "ភាសា៖ ",
                useVietNameseTyping: "ប្រើប្រាស់កម្មវិធីវាយអក្សរវៀតណាម"
            },
        },
        processfunction: {
            parent: "ឳពុក",
            normal: "ធម្មតា",
            field: "ទីវាលទិន្នន័យ",
            type: "ប្រភេទ",
            displayname: "ឈ្មោះបង្ហាញ",
            filterByOverdueDate: "ជ្រើសយកតាមកាលកំណត់ដោះស្រាយ៖",
            display: "បង្ហាញ",
            defaultdocumentsortconfig: "ចំណុចប្រទាក់រៀបចំឯកសារកំណត់",
            entertobreakpage: "(ប្រើប៊ូតុង enter ដើម្បីចុះបន្ទាត់ (បើសិននៅបន្ទាត់ចុងក្រោយ គឺបន្ថែមបន្ទាត់ថ្មី) ប្រើប៊ូតុង ឡើងចុះ ដើម្បីរៀបចំក្រឡា)",
            configlistbynode: "ចំណុចប្រទាក់បញ្ជីឈ្មោះប្រហាក់ប្រហែលនឹង node",

        },
        template: {
            printorder: "ឆ្នោតបោះពុម្ព",
            per1: "ទទួលយក",
            per2: "ប្រគល់",
            per4: "ចុះសច្ចាប័ន",
            per8: "សងលទ្ធផល",
            per16: "ទទួលយកបន្ថែម",
            per32: "ពន្យាពេល",
        },
        form: {
            title: "ចំណុចប្រទាក់គំរូសម្រាប់ឱ្យប្រភេទសំណុំរឿង",
            currentusername: "ឈ្មោះរបស់អ្នកចុះឈ្មោះជ្រាតចូលបច្ចុប្បន្ន",
            docfieldname: "ឈ្មោះវិស័យ",
            doctypename: "ឈ្មោះប្រភេទសំណុំរឿង",
            doccode: "លេខកូដសំណុំរឿង",
            receivedate: "ថ្ងៃទទួលយក",
            appointdate: "ថ្ងៃសងលទ្ធផល",
            template: "គំរូ៖",
            insertspecialvalue: "ដាក់បន្ថែមបណ្តាតម្លៃពិសេស៖",

        },
        report: {
            config: "ទ្រង់ទ្រយរបាយការណ៍",
            configsetup: "[ចំណុចប្រទាក់]",
            showguide: "បិទ/បើកការណែនាំ",
        },
        law: {
            choosedocument: "ជ្រើសយកឯកសារ",
            lawnumbercode: "លេខសញ្ញាណ",
        },
        code: {
            choosedepartment: "ជ្រើសយកស្ថាប័ន",
        },
        store: {
            choosecategory: "ជ្រើសយកមុខជំនាញ",
            addstoreviewer: "បន្ថែមអ្នកត្រួតមើលសៀវភៅ",
        },
        time: {
            worktime: "ម៉ោងរដ្ឋបាល",
            listoffsetday: "បញ្ជីឈ្មោះថ្ងៃធ្វើការសំណងប្រចាំឆ្នាំ",
        },
        deparment: {
            choosejobtitle: "ជ្រើសយកមុខនាទី",
            chooseposition: "ជ្រើសយកមុខងារ",
            listuser: "បញ្ជីឈ្មោះបុគ្គលិកចំណុះស្ថាប័ន",
            nouser: "មិនទាន់មានអ្នកប្រើណាមួយទេ",
            nodata: "មិនទាន់មានស្ថាប័នណាមួយទេ",
            addsubdeparment: "បន្ថែមថ្មីស្ថាប័នចំណុះ",
            deparmentinfo: "ពត៌មានស្ថាប័ន",
            deparmentname: "ឈ្មោះស្ថាប័ន",
            updateinfo: "ធ្វើទាន់សម័យពត៌មានស្ថាប័ន",
            adduser: "បន្ថែមអ្នកប្រើចូលស្ថាប័ន",
            fullname: "នាម និងកោត្តនាម",
            isadmin: "ជាអ្នកគ្រប់គ្រង",
            jobtitle: "មុខនាទី",
            position: "មុខងារ",
            list: "បញ្ជីឈ្មោះស្ថាប័ន"
        },
        position: {
            orderedsort: "អនុញ្ញាតឱ្យរៀបចំលេខរៀងដោយរបៀបទាញហើយដាក់ចូល",
        },
        bkavmessagebox: {
            useshowtoreplacealert: "ប្រើប្រាស់ eGovMessage.show(message, title) ដើម្បីជំនួសឱ្យ messageBoxAlert().",
            useshowtoreplaceconfirm: "ប្រើប្រាស់ eGovMessage.show(message, title, messageButtons.OkCancel) ដើម្បីជំនួសឱ្យ messageBoxConfirm().",
            usenotificationtoreplacetemp: "ប្រើប្រាស់ eGovMessage.notification()ដើម្បីជំនួសឱ្យ messageTemp().",
            closebutton: "បិទ",
            yes: "យល់ព្រម",
            no: "បំបាត់ចោល",
            ok: "ទទួលស្គាល់",
            cancel: "លុបចោល",
            notify: "សេចក្តីប្រកាស",
        },
        tableheader: {
            sortcolumnname: "ឈ្មោះក្រឡារៀបចំ",
            sorttype: "របៀបរៀបចំ (កើនឡើង ឬ បន្ថយចុះ)",
            order: "របៀបរៀបរយ",
            sortcolumn: "ក្រឡារៀបចំ",
            isallowsort: "អនុញ្ញាតឱ្យរៀបចំ",
            displayname: "ឈ្មោះបង្ហាញ",
            width: "ទទឹង",
        },
        commonlabel: {
            deincrease: "បន្ថយចុះជាបណ្តើៗ",
        },
    });


    //#region V1.1 - chua dich
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
    });
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
            restore: "Khôi phục thư đã xóa",
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
            searchresult: "Kết quả tìm kiếm",

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

        },
        sendmail: {
            error: "Sendding mail error, try again"
        }
    }
    //#endregion
})
(window, window.egov = window.egov || {}, window.bmail = window.bmail || {})