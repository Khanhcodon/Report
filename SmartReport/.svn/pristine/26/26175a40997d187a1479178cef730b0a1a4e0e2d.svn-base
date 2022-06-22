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

    egov.resources = {
        document: {
            Compendium: "ສ່ວນຫຍໍ້",
            Comment: "ຄວາມເຫັນແກ້ໄຂ",
            DocType: "ປະເພດເອກະສານ",
            Category: "ຮູບແບບ",
            InOutPlace: "ຫົວໜ່ວຍ",
            DateAppointed: "ເວລາແກ້ໄຂ",
            Organization: "ໜ່ວຍງານສົ່ງ",
            DocCode: "ເລກທີ/ເຄື່ອງໝາຍ*",
            DocCode2: "ເລກລຳດັບ*",
            DateArrived: "ວັນທີມາ",
            DateResponse: "ຄືນໜັງສືພິມ",
            DatePublished: "ວັນທີຈຳໜ່າຍ",
            StoreId: "ປື້ມເອກະສານ",
            InOutCode: "ເລກທີ່ຂາເຂົ້າຂາອອກ",
            TotalPage: "ຈຳນວນໜ້າ",
            ChooseTotalPage: "ເລືອກຈຳນວນໜ້າ",
            DocField: "ຂົງເຂດ",
            Keyword: "ສັບກະເຈ",
            SendType: "ຮູບແບບສົ່ງ",
            DocCode1: "ລະຫັດສຳນວນ",
            CitizenName: "ຊື່ປະຊາກອນ",
            Address: "ທີ່ຢູ່",
            Phone: "ເບີໂທລະສັບ",
            DocPapers: "ເອກະສານ",
            IdentityCard: "ເລກ ບປຕ.",
            Email: "ອິເມລ",
            Commune: "ຕາແສງ/ເຂດ",
            AttachmentList: "File ຄັດມາພ້ອມ",
            RelationList: "ເອກະສານກ່ຽວຂ້ອງ",
            cbDetail: "ສະແດງລາຍລະອຽດເອກະສານ",
            AllComment: "ເນື້ອໃນແກ້ໄຂ",
            titleContent: "ເນື້ອໃນເອກະສານ",
            Urgent: {
                name: "ລະດັບດ່ວນ",
                normal: "ທຳມະດາ",
                fast: "ດ່ານ",
                important: "ດ່ວນທີ່ສຸດ"
            },
            SecurityId: {
                name: "ລະດັບລັບ",
                normal: "ທຳມະດາ",
                high: "ລັບ",
                important: "ລັບພິເສດ",
            },
            CompendiumTitle: "ປ້ອນຄັດເຟັ້ນ.",
            NoComment: "ບໍ່ທັນໃຫ້ຄັດເຟັ້ນ",
            DisplayForm: "ສະແດງແບບຟອມ",
            StorePrivate: "ສຳນວນສ່ວນຕົວ",
            StoreShare: "ສຳນວນແບ່ງປັນ",
            nextPage: "ໜ້າຕໍ່",
            prePage: "ໜ້າກ່ອນ",
            currentPage: "ໜ້າ 1",
            print: "ພິມ",
            btnFinish: "ສິ້ນສຸດ",
            viewIconTraKetQua: "ສົ່ງຄືນຜົນ",
            viewIconTiepNhanBoSung: "ຮັບເອົາເພີ່ມເຕີມ",
            viewIconHuyVanBan: "ຍົກເລິກ",
            viewIconLuu: "ສຳເນົາປື້ມ",
            viewIconGuiykien: "ສົ່ງຄວາມເຫັນ",
            viewIconThongbao: "ແຈ້ງການ",
            viewIconXinykien: "ຂໍຄຳເຫັນ",
            viewIconYeuCauBoSung: "ຮຽກຮ້ອງເພີ່ມເຕີມ",
            viewIconGiaHanXuLy: "ຕໍ່ອາຍຸ",
            no: "ປະຕິເສດ",
            yes: "ເຫັນດີ",
            btnInsertRelation: "ເອກະສານກ່ຽວຂ້ອງ...",
            btnInsertAttachment: "ໄຟລຄັດພ້ອມ",
            btnInsertScan: "ໄຟລ scan...",
            btnPaper: "ໃບອະນຸຍາດ...",
            btnInsertAnticipate: "ຄາດຄະຈະສົ່ງ...",
            btnTransfer: "ສົ່ງເອກະສານ/ສຳນວນ",
            btnEdit: "ແປງເນື້ອໃນເອກະສານ/ສຳນວນ",
            btnInsertFile: "ຄັດຕິດພ້ອມ",
            btnApproverYes: "ເຫັນດີຜ່ານອະນຸມັດ",
            btnApproverNo: "ປະຕິເສດຜ່ານອະນຸມັດ",
            btnDestroy: "ຍົກເລິກເອກະສານ/ສຳນວນ",
            viewIconKetthuc: "",
            btnFinishtt: "ສິ້ນສຸດ",
            btnAnswer: "ຕອບ",
            btnChangeDoctype: "ແບ່ງປະເພດ",
            concurrency: "Vnd",
            UserComment: "ຜູ້ແກ້ໄຂ",
            filename: "ຊື່ໄຟລ",
            filesize: "ຂະໜາດ",
            fileversion: "ສະບັບທີ",
            lastUpdateFile: "ອັບເດດທ້າຍ",
            FinalComment: "ຄຳເຫັນແກ້ໄຂ",
            backtolist: "ກັບຄືນລາຍການ",
            "delete": "ລຶບ",
            MainProcess: "ແກ້ໄຂຫຼັກ:",
            CoProcess: "ຮ່ວມແກ້ໄຂ:",
            sendTo: "ສົງໄປຍັງ",
            thongbao: "ແຈ້ງການ:",
            xinykien: "ຂໍຄຳເຫັນ:",
            view: "ເບິ່ງ",
            download: "ໂຫຼດມາ"
        },
        documentQuickView: {
            belowDocumentSum: "ສະຫຼຸບຫຍໍ້ຂໍ້ມູນເອກະສານ",
            Comment: "ຄຳເຫັນແກ້ໄຂ:",
            timeComment: "ຂະນະ",
            Category: "ປະເພດເອກະສານ:",
            Docfield: "ຂົງເຂດ:",
            DocCode: "ເລກເຄື່ອງໝາຍ:",
            Result: "ຜົນການແກ້ໄຂ",
            LastUserComment: "ຜູ້ແກ້ໄຂທ້າຍ:",
            Place: "ບ່ອນຮັບເອກະສານ:",
            Sign: "ຜູ້ເຊັນ:",
            TotalPage: "ຈຳນວນໜ້າ:",
        },
        transfer: {
            ChoseOtherUser: "ເລືອກພະນັກງານຮັບຜູ້ອື່ນ",
            MainProcessUser: "ຮັບເອກະສານຫຼັກ",
            MainProcessUserComment: "(ແກ້ໄຂຫຼັກ)",
            CoProcessUser: "ຮັບສະບັນສຳເນົາ",
            CoProcessUserComment: "(ປະສານແກ້ໄຂ)",
            AnnouceUser: "ຮັບແຈ້ງການ",
            AnnouceUserComment: "(ເພື່ອເບິ່ງ)",
            QuickSearchAccount: "ຊອກຫາໄວບັນຊີຂອງທິດສົ່ງ ",
            AnnouncementPlace: "ບ່ອນຮັບແຈ້ງການ",
            PrivateAnoun: "ຮັບແຈ້ງການ",
            ConsultContent: "ເນື້ອໃນຂໍຄຳເຫັນ",
            ConsultUser: "ຜູ້ຂໍຄຳເຫັນ",
            MainProcess: "ແກ້ໄຂຫຼັກ",
            CoProcess: "ຮ່ວມແກ້ໄຂ",
            dgUserLabel: "(ເລືອກບຸກຄົນ, ໜ່ວຍງານຮັບສະບັບສໍາເນົາ)",
            dgUser: "ບຸກຄົນ, ໜ່ວຍງານຮັບສະບັບສໍາເນົາ",
            dgJobtitleLabel: "(ເລືອກຕຳແໜ່ງ ແລະພະແນກຮັບສະບັບສໍາເນົາ)",
            dgJobtitle: "ຕຳແໜ່ງ",
            dgDeptJob: "ພະແນກ",
            allJobs: "ຕຳແໜ່ງທັງໝົດ",
            sameDept: "ໜ່ວຍງານດ່ຽວ",
            isDg1: "ແຈ້ງການ",
            isDg2: "ສົ່ງຮ່ວມ",
            searchDgLabel: " ບຸກຄົນຮັບສະບັບສໍາເນົາ ",
            allJobTitlesForDept: "ບັນດາຕຳແໜ່ງທັງໝົດ",
            jobtitlesDeptPopup: "ຕຳແໜ່ງທີ່ຂຶ້ນກັບພະແນກ (ໜ່ວຍງານ)",
            jobtitleForAll: "ເບີກຮັບສະບັບສໍາເນົາ(ເພື່ອຊາບ)",
            allJobTitles: "ບັນດາຕຳແໜ່ງທັງໝົດ",
            IsThongbao: "ແຈ້ງການ|",
            IsDxl: "ຮ່ວມແກ້ໄຂ|",
            IsAttachYk: "ສົ່ງພ້ອມຄຳເຫັນແກ້ໄຂ",
        },
        attachment: {
            view: "ເບິ່ງ",
            open: "ເປີດ file ຄັດພ້ອມ",
            del: "ລຶບ file ຄັດພ້ອມ"
        },
        storePrivate: {
            attachmentName: "ເອກະສານ:",
            descStorePrivateAttachment: "ພັນລະນາ:",
            storePrivateName: "ຊື່ສຳນວນ:",
            storePrivateNameWarning: "ປ້ອນຊື່ສຳນວນ",
            userJoined: "ຜູ້ເຂົ້າຮ່ວມ:",
            delJoined: "ລຶບ",
            descStorePrivate: "ໝາຍເຫດ:",
            storePrivateName: "",
            storePrivateName: "",
        },
        relation: {
            open: "ເປີດເອກະສານ",
            del: "ລຶບເອກະສານກ່ຽວຂ້ອງ",
            view: "ຂໍ້ມູນລະອຽດເອກະສານ"
        },
        toolbar: {
            transferBtn: "ສົ່ງ",
            editBtn: "ແປງ",
            attachBtn: "ຄັດພ້ອມ",
            relation: "ເອກະສານກ່ຽວຂ້ອງ...",
            attachment: "ໄຟລຄັດພ້ອມ",
            scan: "ໄຟລ scan...",
            packet: "ແກ້ໄຂຕາມຊຸດ",
            paper: "ໃບອະນຸຍາດ...",
            DuKienChuyen: "ຄາດຄະສົ່ງ...",
            reloadBtn: "ໂຫຼດໃໝ່",
            allow: "ເຫັນດີ",
            deny: "ປະຕິເສດ",
            destroy: " ເຫັນດີ",
            TiepNhanBoSung: "ຮັບເອົາເພີ່ມເຕີມ",
            TraKetQua: "ສົ່ງຄືນຜົນ",
            finish: "ສຶ້ນສຸດ",
            reply: "ຕອບ",
            PhanLoai: "ແບ່ງປະເພດ",
            print: "ພິມ",
            other: "ອື່ນ",
            GiaHan: "ຕໍ່ອາຍຸ",
            YeuCauBoSung: "ຮຽກຮ້ອງເພີ່ມເຕີມ...",
            XinYKien: "ຂໍຄຳເຫັນ...",
            btnAnnouncement: "ແຈ້ງການ...",
            btnSendAnswer: "ສົ່ງຄຳເຫັນ...",
            btnSaveStore: "ສຳເນົາປື້ມ.."
        },
        main: {
            news: "ຂ່າວບັນຊາ",
            newEmail: "ຂຽນຈົດໝາຍ",
            config: "ຕັ້ງຄ່າ",
            reply: "ສົ່ງຕອບຄືນ",
            smallSize: "ເບິ່ງຂະໜາດນ້ອຍ",
            mediumSize: "ເບິ່ງຂະໜາດກາງ",
            largeSize: "ເບິ່ງຂະໜາດໃຫຍ່",
            underPreview: "ເບິ່ງກ່ອນດ້ານລຸ່ມ",
            rightPreview: "ເບິ່ງກ່ອນເບື້ອງຂາວ",
            hidePreview: "ຫຼົບເບິ່ງກ່ອນ",
            reload: "ອຸ່ນເຄື່ອງໃໝ່",
            logout: "ອອກຈາກ",
            searchDocument: "ຊອກຫາເອກະສານ",
            searchFile: "ຊອກຫາ file ຄັດພ້ອມ",
            reloadMessage: "ອັບເດດສຳເລັດ. ເຈົ້າຢາກໂຫຼດໜ້າບ?",
            closeBtn: "ປິດ",
            submitBtn: "ອັບເດດ",
            titleMessage: "ແຈ້ງການ!",
            closeAll: "ປິດທັງໝົດ",
            report: "ສະຖິຕິ",
            contacts: "ປື້ມຕິຕໍ່",
            calendar: "ປະຕິທິນ",
            chat: "Chat",
            documents: "ແກ້ໄຂໜັງສືລັດຖະການ",
            bmail: "Email",
            notifications: "ແຈ້ງການ",
            gtv: "ພິມພາສາຫວຽດ"
        },
        index: {
            storePrivate: "ສຳນວນວຽກງານ",
            plugin: "ປະຍຸກໃຊ້",
            reportNode: "ລາຍງານສະຖິຕິ",
            printNode: "ພິມໄວ",
            //reportNode: "ລາຍງານສະຖິຕິ",
            reload: "ຄົບຊຸດ"
        },
        setting: {
            title: "ຕັ້ງຄ່າບຸກຄົນ",
            ProfileConfig: "ຂໍ້ມູນສ່ວນບຸກຄົນ",
            Changepassword: "ປ່ຽນລະຫັດຜ່ານ",
            UserSetting: "ກຳນົດຄ່າປຸ່ມລັດ",
            GeneralSettings: "ກຳນົດຄ່າອື່ນ",
            Signature: "ກຳນົດຄ່າລາຍເຊັນ",
            btnUpdateSetting: "ອັບເດດ",
            btnCloseSetting: "ປິດ",
        },
        scan: {
            rotateLeft: "ປິ່ນຊ້າຍ",
            rotateRight: "ປິ່ວຂາວ",
            zoomIn: "ຂະຫຍາຍໃຫຍ່",
            zoomOut: "ຫຍໍ້ນ້ອຍ",
            setActualSize: "ຮູບເດີມ",
            crop: "ຕັດຮູບ",
            setBrightnessUp: "ເພີ່ມຄວາມແຈ້ງ",
            setBrightnessDown: "ຫຼຸດຄວາມແຈ້ງ",
            setContrastUp: "ເພີ່ມຄວາມກົງກັນຂ້າມ",
            setContrastDown: "ຫຼຸດຄວາມກົງກັນຂ້າມ",
            addToContent: "ນຳໃສ່ເນື້ອໃນ",
            pagePosition: "ໜ້າ: 0/0",
            preImage: "ຮູບກ່ອນ",
            nextImage: "ຮູບຫຼັງ",
            removeImageScan: "ລຶບ",
            removeAllImageScan: "ລຶບທັງໝົດ",
            listScannerLabel: "ເລືອກເຄື່ອງ scan:",
            reloadListScanner: "ເຮັດໃໝ່ລາຍການເຄື່ອງ scan",
            colortype: "ແບບສີ",
            pixeltype2: "ສີ",
            pixeltype0: "ສີຂີ້ເຖົ່າ",
            pixeltype1: "ສີດຳຂາວ",
            resolution75: "75 dpi",
            resolution100: "100 dpi",
            resolution150: "150 dpi",
            resolution200: "200 dpi",
            resolution300: "300 dpi",
            duplex: "ສະແກນ 2 ໜ້າ",
            showui: "ໃຊ້ໜ້າຈໍຂອງເຄື່ອງ scan",
            filename: "ຊື່ໄຟລ",
            imageformatLabel: "ບັນທຶກໄຟລແບບ",
            imageformatJPEG: "JPEG",
            imageformatPNG: "PNG",
            imageformatGIF: "GIF",
            imageformatTIFF: "TIFF",
            imageformatBMP: "BMP",
            imageformatPDF: "PDF",
            imageformatDOC: "DOC",
            acquire: "ສະແກນຮູບ",
            refresh: " ເຮັດໃໝ່ລາຍການເຄື່ອງ ",
            colortype: "ແບບສີ",
            color: {
                color: "ສີ",
                gray: "ສີຂີ້ເຖົ່າ",
                whiteblack: "ສີດຳຂາວ"
            },
        },
        tab: {
            close: "ປິດ tab"
        },
        search: {
            //search.html
            compendium: "ຄັດເຟັ້ນ",
            doccode: "ເລກເຄື່ອງໝາຍ",
            inoutcode: "ເລກຂາເຂົ້າ",
            content: "ເນື້ອໃນ",
            category: "ຮູບແບບ",
            keyword: "ຄຳຫຼັກ",
            urgent: "ລະດັບດ່ວນ",
            categorybusiness: {
                name: "ວິຊາສະເພາະ",
                all: "ທັງໝົດ",
                "in": "ເອກະສານຂາເຂົ້າ",
                out: "ເອກະສານຂາອອກ",
                one: "ສຳນວນປະຕູດຽວ"
            },
            FromDateStr: "ແຕ່ວັນທີ",
            InOutPlace: "ໜ່ວຍງານສົ່ງ",
            OrganizationCreate: "ໜ່ວຍງານປະກາດ",
            DocField: "ຂົງເຂດ",
            UserSuccess: "ຜູ້ເຊັນ",
            CurrentUser: "ຜູ້ກຳລັງຮັກສາ",
            ToDateStr: "ເຖິງວັນທີ",
            showsearch: "ສະແດງການຊອກຫາຍົກສູງ",
            createdate: "ວັນເປີດສ້າງ",
            createdate1: "ວັນສ້າງ",
            status: "ສະພາບ",
            //searchDocResult.html, searchResult.html
            status1: "ກຳລັງສ່າງຮ່າງ",
            status2: "ກຳລັງແກ້ໄຂ",
            status4: "ໄດ້ສິ້ນສຸດ",
            status8: "ໄດ້ຍົກເລິກ",
            status16: "ຢຸດແກ້ໄຂ",
            //searchTab.html
            search: "ຊອກຫາ",
            order: "ລ/ດ",
            searchnotfound: "ບໍ່ຄົ້ນພົບຜົນທີ່ເໝາະສົມ",
            view: "ເບິ່ງ",
            download: "ໂຫຼດມາ",
            DidYouMean: "ເຈົ້າຢາກຊອກຫາແມ່ນບໍ",
            all: "ທັງໝົດ",
            //search.html
            doccodePh: "ປ້ອນເລກເຄື່ອງໝາຍ",
            inoutcodePh: "ປ້ອນເລກຂາເຂົ້າ",
            contentPh: "ປ້ອນເນື້ອໃນ",
            keywordPh: "ປ້ອນຄຳຫຼັກ",
            error: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອຊອກຫາ. ກະລຸນາຕິດຕໍ່ບໍລິຫານເຄືອຂ່າຍ.",
            noresult: "ບໍ່ຊອກຫາພົບຜົນ",
            Compendiumph: "ປ້ອນຄັດເຟັ້ນເອກະສານ",
            openattachmentfile: "ເປີດ file ຄັດພ້ອມ",
            downloadattachmentfile: "ໂຫຼດ file ຄັດພ້ອມ",
        }
    }

    //#region Version 1.1 - ບໍ່ທັນແປ

    egov.resources.common = {
        processing: "ກຳລັງໂຫຼດ...",
        loading: "ກຳລັງໂຫຼດ...",
        error: "ມີຜິດພາດເກີດຂຶ້ນ",
        searching: "ກຳລັງຊອກຄົ້ນ",
        closeButton: "ປິດ",
        addButton: "ເພີ່ມ",
        editButton: "ແປງ",
        updateButton: "ອັບເດດ",
        cancelButton: "ຍົກເລິກ",
        deleleButton: "ລຶບ",
        confirmButton: "ຢືນຢັນ",
        alert: "ປະກາດ",
        transfering: 'ກຳລັງຍ້າຍ',
        currencyUnit: "Vnd"
    }
    egov.resources.tab = extend(egov.resources.tab, {
        home: { title: 'ເອກະສານ' },
        report: { title: 'ລາຍງານສະຖິຕິ' },
        print: { title: 'ພິມໄວ' },
        search: { title: 'ຊອກຄົ້ນ' },
        setting: { title: 'ສ້າງຕັ້ງ' },
        saveChange: "ເອກະສານມີການປ່ຽນແປງ, ເຈົ້າຢາກບັນທຶກໄວ້ ຫຼືບໍ?"
    });
    egov.resources.file = {
        lenghtIsNotAllow: "File ໂຫຼດຂຶ້ນເກີນບໍລິມາດບັນຈຸທີ່ກຳນົດ.",
        typeIsNotAllow: "File ບໍ່ຖືກຕ້ອງຮູບແບບທີ່ກຳນົດ.",
        errorUpload: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດໄຟລຂຶ້ນ.",
        errorDownload: " ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດໄຟລລົງ."
    };
    egov.resources.home = {
        syncDataError: 'ມີຜິດພາດເມື່ອປັບໃຫ້ເຂົ້າກັນລາຍການເອກະສານ',
        documentPreView: {
            tooltip: {
                open: "ສະແດງສະຫຼຸບຫຍໍ້ເອກະສານ/ສຳນວນ",
                close: "ຊ້ອນສະຫຼຸບຫຍໍ້ເອກະສານ/ສຳນວນ"
            },
            control: {
                close: "X",
                open: "open"
            }
        },
        docType: {
            message: {
                error: { loading: "ໂຫຼດລາຍການປະເພດເອກະສານບໍ່ໄດ້!" }
            }
        }
    };
    egov.resources.treeDocument = {
        message: {
            confirm: {},
            success: {},
            error: {
                syncData: "ຜິດພາດເມື່ອປັບໃຫ້ເຂົ້າກັນຂໍ້ມູນ!",
            }
        }
    };
    egov.resources.treeStore = {
        nameStorePrivateRoot: 'ສຳນວນສ່ວນບຸກຄົນ',
        nameStoreShareRoot: 'ສຳນວນແບ່ງປັນ',
        title: {
            createStore: 'ສ້າງປື້ມສຳນວນ',
            detailSotore: 'ເບິ່ງລາຍລະອຽດ',
            addStorePrivateAttachment: 'ເພີ່ມເອກະສານ'
        },
        message: {
            confirm: {
                openStore: 'ເຈົ້າແນ່ນ່ອນຢາກເປີດສຳນວນນີ້ ຫຼືບໍ?',
                closeStore: 'ເຈົ້າແນ່ນ່ອນຢາກປິດສຳນວນນີ້ ຫຼືບໍ?',
                deleteStore: 'ເຈົ້າແນ່ນ່ອນຢາກລຶບສຳນວນນີ້ ຫຼືບໍ?',
            },
            success: {
                openStore: 'ເປີດສຳນວນສຳເລັດ!',
                closeStore: 'ປິດສຳນວນສຳເລັດ!',
                deleteStore: 'ລຶບສຳນວນສຳເລັດ!',
            },
            error: {
                createStore: 'ມີຜິດພາດໃນເວລາສ້າງໃໝ່ປື້ມສຳນວນ',
                updateStore: 'ມີຜິດພາດໃນເວລາອັບເດດປື້ມສຳນວນ',
                selectStore: 'ມີຜິດພາດເກີດຂຶ້ນເມື່ອເອົາຂໍ້ມູນ',
                openStore: 'ມີຜິດພາດເມື່ອເປີດສຳນວນ!',
                closeStore: 'ມີຜິດພາດເມື່ອປິດສຳນວນ!',
                deleteStore: 'ມີຜິດພາດເມື່ອລຶບສຳນວນ!',
            }
        },
        contextmenu: {
            createStore: 'ສ້າງໃໝ່ສຳນວນ',
            updateStore: 'ອັບເດດສຳນວນ',
            deleteStore: 'ລຶບສຳນວນ',
            openStore: 'ເປີດສຳນວນ',
            closeStore: 'ປິດສຳນວນ',
            addStorePrivateAttachment: 'ເພີ່ມເອກະສານ',
            messageCloseStore: 'ເຈົ້າແນ່ນອນຢາກປິດສຳນວນນີ້?.',
            messageOpenStore: 'ເຈົ້າແນ່ນອນຢາກເປີດສຳນວນນີ້?.'
        }
    };

    egov.resources.documents = {
        title: {
            documentImportant: "ປົດຕິດສຳຄັນເອກະສານນີ້",
            documentNotImportant: "ຕິດສຳຄັນໃຫ້ເອກະສານນີ້",
            vanBanDongXuLy: "ເອກະສານແກ້ໄຂຮ່ວມກັນ",
            vanBanSapHetHan: "ເອກະສານເກືອບໝົດເວລາກຳນົດ (ຍັງ 1 ວັນ)",
            vanBanKhanHoacQuaHanXuLy: "ເອກະສານດ່ວນ ຫຼືກາຍກຳນົດແກ້ໄຂ",
            vanBanQuaHanHoiBao: "ເອກະສານກາຍກຳນົດແຈ້ງຄືນ",
            vanBanHoaToc: "ເອກະສານດ່ວນທີ່ສຸດ",
            vanBanThuong: "ເອກະສານປົກກະຕິ",
            documentDetail: "ລາຍລະອຽດເອກະສານ/ສຳນວນ",
        },
        toolbar: {
            controlName: {
                all: "ເບິ່ງທັງໝົດ",
                day: "ວັນທີ",
                page: "ໜ້າ",
                dateAppointed: "ວັນທີ່ໝົດກຳນົດເວລາ",
                docTypeName: "ປະເພດສຳນວນ",
                documentImportant: "ເບິ່ງເອກະສານສຳຄັນ",
                documentUnread: "ເບິ່ງເອກະສານບໍ່ທັນອ່ານ",
                refresh: "ໂຫຼດຄືນ",
                dateReceived: "ວັນທີຮັບ",
                sortBy: "ສັບຊ້ອນຕາມ",
                setting: "ຕິດຕັ້ງລາຍການ",
                preview: "ເບິ່ງກ່ອນ",
                menu: "Menu"
            }
        },
        contextmenu: {
            name: {
                xemvanban: 'ເບິ່ງເອກະສານ...',
                suavanban: 'ແປ່ງເອກະສານ...',
                guiykien: 'ສົ່ງຄຳເຫັນ...',
                xinykien: 'ຂໍຄຳເຫັນ...',
                bangiao: 'ມອບຮັບ...',
                thongbao: 'ແຈ້ງການ...',
                laylaivanban: 'ເອົາຄືນເອກະສານ',
                xacnhanbangiao: 'ຢືນຢັນມອບຮັບ...',
                xacnhanxuly: 'ຢືນຢັນແກ້ໄຂ...',
                yeucaubosung: 'ຮຽກຮ້ອງປະກອບເພີ່ມເຕີມ...',
                tiepnhanbosung: 'ຮັບເອົາປະກອບເພີ່ມເຕີມ...',
                kyduyet: 'ເຊັນຜ່ານອະນຸມັດ...',
                ketthucxuly: 'ສິ້ນສຸດການແກ້ໄຂ',
                huyvanban: 'ຍົກເລິກເອກະສານ',
                capnhatketquaxulycuoi: 'ອັບເດດຜົນການແກ້ໄຂທ້າຍ...',
                inphieutrinh: 'ພິມບັດຍື່ນການນຳ...',
                intomtat: 'ພິມສະຫຼຸບຫຍໍ້',
                capnhattiendo: 'ອັບເດດຄວາມຄືບໜ້າ...',
                xoakhoiduthao: 'ລຶບຮ່າງເອກະສານ',
                contextheodoi: 'Fix contextmenu ຕິດຕາມ',
                dungxuly: 'ຢຸດແກ້ໄຂ...',
                giahanxuly: 'ຕໍ່ການແກ້ໄຂ...',
                chitietvanban: 'ລາຍລະອຽດເອກະສານ/ສຳນວນ',
                danhdaudadoc: 'ໝາຍໄດ້ອ່ານແລ້ວ',
                danhdauchuadoc: 'ໝາຍບໍ່ທັນອ່ານ',
                movanban: 'ເປີດເອກະສານ',
            }
        },
        page: {
            text: "ໜ້າ",
            document: "ເອກະສານ"
        },
        message: {
            error: {
                quickView: "ຜິດພາດເມື່ອເອົາຂໍ້ມູນເອກະສານ!",
                documentNotExist: "ເອກະສານບໍ່ຄົງຕົວ!.",
                documentNotSelectDelete: "ບໍ່ທັນເລືອກເອກະສານເພື່ອລຶບ!.",
                pagging: "ມີຜິດພາດໃນເວລາຍົກຍ້າຍໄປໜ້າໃໝ່",
                loadNewerDocuments: "ມີຜິດພາດໃນເວລາໂຫຼດຂໍ້ມູນ!",
                getDocumentDetail: "ເອກະສານບໍ່ຄົງຕົວ"
            }
        }
    };
    egov.resources.document = extend(egov.resources.document, {
        toolbar: {
            noaction: "ບໍ່ມີທິດສົ່ງຕໍ່.",
            transferByDk: "ສົ່ງຕາມຄາດຄະ",
            transferUserDk: "ສົ່ງຜູ້ຮັບຄາດຄະ",
            controlName: {
                transferDoc: {
                    name: "ສົ່ງ",
                    message: { error: 'ມີຜິດພາດເມື່ອໂຫຼດລາຍການທິດສົ່ງ' },
                    item: {
                        cancel: { name: "ບໍ່ຊອກເຫັນທິດສົ່ງຕໍ່" },
                        transferplan: { name: 'ສົ່ງຕາມຄາດຄະ' },
                        newtransferplan: { name: " ສົ່ງຜູ້ຮັບທີ່ຄາດຄະ" }
                    }
                },
                edit: { name: "ແປ່ງ", },
                insert: {
                    name: "ເພີ່ມເຂົ້າ",
                    message: { error: 'ມີຜິດພາດເກີດຂຶ້ນ' }
                },
                reload: { name: "ໂຫຼດຄືນ" },
                approverYes: { name: "ຕົກລົງ" },
                approverNo: { name: 'ປະຕິເສດ' },
                remove: { name: 'ຍົກເລິກ' },
                tiepNhanBoSung: { name: 'ຮັບເອົາປະກອບເພີ່ມເຕີມ' },
                "return": { name: 'ຈ່າຍຜົນ' },
                finish: { name: 'ສິ້ນສຸດ' },
                traloi: {
                    name: "ຕອບ",
                    hoso: "ສຳນວນໃໝ່",
                    document: "ເອກະສານໃໝ່",
                    message: { error: 'ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດລາຍການຈັດປະເພດ' }
                },
                phanloai: {
                    name: "ຈັດປະເພດ",
                    callBackTitle: "ເລືອກປະເພດເອກະສານ/ສຳນວນ",
                    message: { error: 'ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດລາຍການຈັດປະເພດ' }
                },
                print: {
                    name: "ພິມ",
                    message: { error: 'ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດລາຍການພິມ!.' }
                },
                giahan: { name: "ພິມ", },
                tiepNhanBoSung: { name: 'ຮຽກຮ້ອງປະກອບເພີ່ມເຕີມ' },
                xinykien: { name: 'ຂໍຄຳເຫັນ' },
                thongbao: { name: 'ແຈ້ງການ' },
                guiykien: { name: 'ສົ່ງຄຳເຫັນ' },
                savePrivateStore: { name: 'ສຳເນົາປື້ມ' },
                others: { name: 'ອື່ນໆ' }
            }
        },
        content: {
            version: "ເບິ່ງສະບັບຂອງ {0} ອັບເດດເມື່ອ {1}"
        },
        relation: {
            titleDialog: "ເພີ່ມເອກະສານກ່ຽວຂ້ອງ",
            confirmRelationTitle: "ຢືນຢັນສົ່ງພ້ອມເອກະສານທີ່ກ່ຽວຂ້ອງ",
            ignoreConfirm: "ສົ່ງ, ບໍ່ສະແດງຄືນແຈ້ງການນີ້ຄັ້ງໜ້າເປັນປະຈຳ.",
            contextmenu: {
                open: "ເປີດເອກະສານ",
                "delete": "ລຶບເອກະສານ"
            },
            documentNotExist: "ເອກະສານບໍ່ຄົງຕົວ!",
        },
        attachment: {
            uploading: "ກຳລັງໂຫຼດໄຟລຂຶ້ນ",
            uploadSuccess: "ໂຫຼດໄຟລຂຶ້ນສຳເລັດ!.",
            uploadError: "ມີຜິດພາດເມື່ອໂຫຼດໄຟລຂຶ້ນ",
            fileChanged: "<strong>ໄຟລ {0} ມີການປ່ຽນແປງ</strong><br/>ເຈົ້າຢາກບັນທຶກໄວ້ຫຼືບໍ?",
            errorDownload: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດໄຟລ.",
            openFile: "ເປີດ file",
            deleteFile: "ລຶບ file",
            restoreFile: "ຟື້ນຟູ file",
            download: "ໂຫຼດມາ",
            removed: "(ໄດ້ລົບລ້າງ)",
            using: "ກຳລັງໃຊ້",
            version: "ສະບັບ {0} ອັບເດດເວລາ {1}",
            closeProgramBeforeSave: "ເຈົ້າຕ້ອງປິດບັນດາໂປຼແກຼມກຳລັງເປີດໄຟລຕິດພ້ອມກ່ອນຈະບັນທຶນ.",
            fileIsRemoved: "File ຖືກລຶບແລ້ວ"
        },
        transfer: {
            transferButton: "ສົ່ງ",
            dialogTitle: "ມອບຮັບເອກະສານ",
            noUser: "ບໍ່ທັນເລືອກຜູ້ຮັບແກ້ໄຂ",
            transferSuccess: "ສົ່ງເອກະສານສຳເລັດ.",
            transferError: "ມີຜິດພາດໃນເວລາມອບຮັບ.",
            noUserByAction: "ທິດສົ່ງບໍ່ມີຜູ້ຮັບ",
            sendAll: "ທຸກຄົນ",
            answerSuccess: "ຕອບສຳເລັດ.",
            answerFail: "ມີຜິດພາດໃນເວລາຕອບຄຳເຫັນ.",
            showDgTitle: "ສະແດງໜ້າຈໍພົວພັນໃຫ້ພະນັກງານຜູ້ອື່ນ",
            noXlc: "ບໍ່ທັນເລືອກພະນັກງານແກ້ໄຂ",
            userList: "ລາຍງານຮັບເອກະສານ"
        },
        publishment: {
            dialogTitle: "ຈຳໜ່າຍເອກະສານ",
            privateDialogTitle: "ບັນທຶກປື້ມຈຳໜ່າຍພາຍໃນ",
            publishButton: "ບັນທຶກ ແລະຈຳໜ່າຍ",
            noAddressSelected: "ເຈົ້າບໍ່ທັນເລືອກໜ່ວຍງານຮັບເອກະສານ.",
            success: "ຈຳໜ່າຍເອກະສານສຳເລັດ.",
            error: "ມີຜິດພາດເມື່ອຈຳໜ່າຍ. ກະລຸນາເຮັດຄືນ."
        },
        ChangeDoctype: {
            hasChangeDateAppoint: "ເອກະສານ/ສຳນວນໄດ້ຈັດປະເພດຕາມປະເພດສຳນວນໃໝ່.</br>ເຈົ້າຢາກປ່ຽນແປງກຳນົດເວລາແກ້ໄຂຂອງເອກະສານ/ສຳນວນ ຕາມກຳນົດເວລາແກ້ໄຂຂອງປະເພດສຳນວນໃໝ່ ຫຼືບໍ?",
            success: "ເອກະສານ/ສຳນວນ ໄດ້ຍ້າຍໄປປະເພດເອກະສານ {0}."
        },
        sendComment: {
            dialogButton: "ສົ່ງຄຳເຫັນ",
            dialogTitle: "ປ້ອນຄຳເຫັນ",
            enterComment: "ເຈົ້າບໍ່ທັນປ້ອນຄຳເຫັນແກ້ໄຂ",
            sendFail: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອໃຫ້ຄຳເຫັນ, ກະລຸນາເຮັດຄືນ.",
            sendSuccess: "ສົ່ງຄຳເຫັນສຳເລັດ",
            requireMessage: "ເຈົ້າບໍ່ທັນປ້ອນຄຳເຫັນ!"
        },
        announcement: {
            dialogTitle: "ແຈ້ງການ",
            announcementButton: "ສົ່ງແຈ້ງການ",
            sendSuccess: "ສົ່ງແຈ້ງການສຳເລັດ.",
            sendFail: "ສົ່ງແຈ້ງການຜິດພາດ, ກະລຸນາເຮັດຄືນ.",
            noReceiver: "ເຈົ້າບໍ່ທັນເລືອກຜູ້ຮັບແຈ້ງການ."
        },
        consult: {
            dialogTitle: "ຂໍຄຳເຫັນ",
            consultButton: "ສົ່ງຂໍຄຳເຫັນ",
            sendSuccess: "ສົ່ງຂໍຄຳເຫັນສຳເລັດ.",
            sendFail: "ສົ່ງຂໍຄຳເຫັນຜິດພາດ, ກະລຸນາເຮັດຄືນ.",
            noReceiver: "ເຈົ້າບໍ່ທັນເລືອກຜູ້ຮັບຂໍຄຳເຫັນ.",
            noComment: "ເຈົ້າບໍ່ທັນປ້ອນຄຳເຫັນແກ້ໄຂ."
        },
        finish: {
            error: "ບໍ່ສິ້ນສຸດເອກະສານໄດ້, ກະລຸນາເຮັດຄືນ.",
            success: "ສິ້ນສຸດ ເອກະສານ ສຳເລັດ",
            processing: "ກຳລັງແກ້ໄຂ"
        },
        docStore: {
            dialogTitle: "ບັນທຶກປື້ມສ່ວນຕົວ",
            createNew: "ສ້າງໃໝ່",
            saveButton: "ບັນທຶກ",
            notSaveButton: "ບໍ່ບັນທຶກ",
            noChooseStore: "ເຈົ້າບໍ່ທັນເບືອກປື້ມສ່ວນຕົວ",
            processing: "ກຳລັງບັນທຶກ",
            success: "ບັນທຶກສຳເລັດ",
            error: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອບັນທຶກ, ກະລຸນາເຮັດຄືນ"
        },
        hsmc: {
            documentResult: "ຜົນການແກ້ໄຂ: ",
            noResult: "ບໍ່ທັນຜ່ານອະນຸມັດ",
            resultOk: "ໄດ້ຜ່ານອະນຸມັດ",
            resultDeny: "ບໍ່ຜ່ານອະນຸມັດ",
            removeResult: "ຍົກເລິກ"
        },
        supplementary: {
            title: "ຮຽກຮ້ອງປະກອບເພີ່ມເຕີມ",
            requiredTitle: "ຂໍ້ມູນປະກອບເພີ່ມເຕີມ",
            paper: "ເອກະສານປະກອບເພີ່ມເຕີມ",
            fee: "ຄ່າທຳນຽມປະກອບເພີ່ມເຕີມ",
            noAdditional: "ປະຊາຊົນບໍ່ປະກອບເພີ່ມເຕີມ",
            addPaper: "ເພີ່ມເອກະສານ",
            addFee: "ເພີ່ມຄ່າທຳນຽມ",
            newDateAppointed: "ຄິດໄລ່ວັນທີນັດສົ່ງຄືນ",
            addDay: "ຈຳນວນວັນ",
            dateAppointed: "ວັນທີນັດສົ່ງຄືນ: ",
            supplementType: {
                renew: "ຄິດໄລ່ຄືນແຕ່ຕົ້ນ",
                "continue": "ສືບຕໍ່ຄິດໄລ່",
                add: "ບວກເພີ່ມວັນ"
            },
            success: "ຮັບເອົາປະກອບເພີ່ມເຕີມສຳເລັດ",
            updateAndPrintButton: "ອັບເດດ ແລະພິມໃບຮັບ"
        },
        print: { text: "ພິມ" },
        renewals: {
            renewalsButton: "ຕໍ່ອາຍຸ",
            renewalsAndPrintButton: "ຕໍ່ອາຍຸ ແລະພິມບັດ",
            dialogTitle: "ຕໍ່ອາຍຸແກ້ໄຂ",
            renewalsReason: "ສາເຫດຕໍ່ອາຍຸ",
            newDateAppoint: "ກຳນົດເວລາແກ້ໄຂໃໝ່",
            printTemplate: "ແບບພິມ",
            noPrintTemplate: "ບໍ່ມີແບບພິມຕໍ່ອາຍຸ"
        },
        updateLastResult: {
            ok: "ຜ່ານອະນຸມັດ",
            deny: "ບໍ່ຜ່ານອະນຸມັດ",
            comment: "ຄຳເຫັນແກ້ໄຂ:",
            dialogTitle: "ອັບເດດຜົນການແກ້ໄຂ"
        },
        returnResult: {
            dialogTitle: "ສົ່ງຄືນຜົນ",
            updateAndPrintButton: "ພິມ ແລະສົ່ງຄືນຜົນ",
            updateButton: "ສົ່ງຄືນຜົນ",
            needToUpdateSupplementary: "ສຳນວນກຳລັງມີຮຽກຮ້ອງປະກອບເພີ່ມເຕີມ, ເຈົ້າຕ້ອງອັບເດດຜົນການ ປະກອບເພີ່ມເຕີມກ່ອນ:",
            needToUpdateLastResult: "ສຳນວນບໍ່ທັນມີຜົນການແກ້ໄຂສຸດທ້າຍ, ເຈົ້າຕ້ອງອັບເດດຜົນການແກ້ໄຂທ້າຍ:",
            resultOk: "ຕົກລົງ",
            resultDeny: "ປະຕິເສດ",
            result: "ຜົນການແກ້ໄຂ: ",
            personGive: "ຂໍ້ມູນປະຊາກອນຮັບຜົນ",
            finish: "ສິ້ນສຸດແກ້ໄຂສຳນວນຫຼັງຈາກການສົ່ງຄືນຜົນ",
            printTemplate: "ແບບພິມ"
        },
        confirmDestroy: "ເຈົ້າແນ່ນອນຢາກຍົກເລິກເອກະສານນີ້?",
        xlcLabel: "ແກ້ໄຂຕົ້ນຕໍ: ",
        dxlLabel: "ສົ່ງຮ່ວມ: ",
        xykLabel: "ຂໍຄຳເຫັນ: "
    });

    egov.resources.documentQuickView.noDocumentSelected = "ເລືອກເອກະສານເພື່ອສະແດງຂໍ້ມູນສະຫຼຸບຫຍໍ້ທີ່ນີ້.";

    // y kien thuong dung
    egov.resources.templateComment = {
        titleDialog: "ແບບຄຳເຫັນມັກໃຊ້",
        btnAddTemplateComment: "ເພີ່ມແບບ",
        btnSelected: "ເລືອກ",
        table: {
            header: {
                content: "ເນື້ອໃນ",
                edit: "ແປງ",
                "delete": "ລຶບ"
            }
        },
        addDialog: {
            title: "ເພີ່ມແບບຄຳເຫັນມັດໃຊ້",
            btnCreate: "ສ້າງໃໝ່",
            errorMessage: "ເຈົ້າບໍ່ທັນປ້ອນແບບຄຳເຫັນ!"
        },
        editDialog: {
            title: "ອັບເດດແບບຄຳເຫັນມັກໃຊ້",
            btnEdit: "ອັບເດດ",
            errorMessage: "ເຈົ້າບໍ່ທັນປ້ອນແບບຄຳເຫັນ!"
        },
        contextmenu: {
            selected: "ເລືອກ",
            edit: "ແປ່ງ/ເບິ່ງຂໍ້ມູນ",
            "delete": "ລຶບ"
        }
    };
    egov.resources.setting.AuthorizesSetting = "ຕັ້ງຄ່າມອບສິດ";

    //ສ່ວນລາຍເຊັນ
    egov.resources.setting.signature = {
        titleCreate: "ເພີ່ມໃໝ່ລາຍເຊັນ",
        titleEdit: "ອັບເດດລາຍເຊັນ",
        configPossition: "ຕັ້ງຄ່າທີ່ຕັ້ງຕິດຕັ້ງລາຍເຊັນ",
        configOther: "ຕັ້ງຄ່າອື່ນ",
        deleteMessage: 'ເຈົ້າແນ່ນອນຢາກລຶບຕັ້ງຄ່ານີ້',
        labelCreate: "ເພີ່ມໃໝ່",
        table: {
            header: {
                stt: "ລຳດັບ",
                configNameSignature: "ຊື່ຕັ້ງຄ່າ",
                wordsNeedFind: "ຄຳຕ້ອງຊອກຄົ້ນ",
                findTypes: "ປະເພດຊອກຄົ້ນ",
                signTypes: "ປະເພດເຊັນ",
                position: "ທີ່ຕັ້ງ",
                edit: "ແປງ",
                "delete": "ລຶບ"
            },
            body: {
                findTypeBottomToTop: "ລຸ່ມຂຶ້ນ",
                findTypeTopToBottom: "ເທິງລົງ",
                imageSignature: "ລາຍເຊັນຮູບ",
                textSignature: "ລາຍເຊັນແບບອັກສອນ",
                leftPosition: "ເບື້ອງຊ້າຍ",
                abovePosition: "ດ້ານເທິງ",
                rightPosition: "ເບື້ອງຂວາ",
                belowPosition: "ດ້ານລຸ່ມ",
                noData: "ບໍ່ມີຂໍ້ມູນ"
            },
        }
    };

    //ສ່ວນມອບສິດ
    egov.resources.setting.authorize = {
        titleCreate: "ເພີ່ມໃໝ່ຜູ້ຮັບມອບສິດ",
        titleEdit: "ອັບເດດລາຍເຊັນ",
        labelCreate: "ເພີ່ມໃໝ່",
        titleDialogDelete: "ແຈ້ງການ!",
        confirmDelete: "'ເຈົ້າແນ່ນອນຢາກລຶບຕັ້ງຄ່ານີ້ ",
        btnSubmitDelete: "ຕົກລົງ",
        btnCancelDelete: "ຍົກເລິກ",
        table: {
            header: {
                stt: "ລຳດັບ",
                nameDocType: "ຊື່ປະເພດສຳນວນ",
                userReceive: "ຜູ້ຮັບມອບສິດ",
                startDate: "ມື້ເລີ່ມຕົ້ໜ",
                endDate: "ມື້ໝົດອາຍຸ",
                state: "ສະພາບ",
                edit: "ແປງ",
                "delete": "ລຶບ"
            },
            body: {
                noData: "ບໍ່ມີຂໍ້ມູນ"
            }
        }
    };

    //cấu hình chung
    egov.resources.setting.general = {
        page: "Phần trang",
        scrollLoadData: "ມ້ວນເມົາເພື່ອໂຫຼດຂໍ້ມູນ",
        pagingLoadData: "ແບ່ງໜ້າໂຫຼດຂໍ້ມູນ",
        showDetailDocument: "ສະແດງລາຍລະອຽດເອກະສານ",
        showQuickView: "ສະແດງຫຍໍ້ເອກະສານ"
    };
    egov.resources.setting.profile = {
        avatar: "ຮູບສະແດງ",
        choseAvatar: "ເລືອກ"
    };
    egov.resources.setting.login = {
        account: "ບັນຊີ:",
        password: "ລະຫັດຜ່ານ:",
        keepingLogin: "ຮັກສາການເຂົ້າສູ່ລະບົບ!",
        loginType: "ຮູບການເຂົ້າສູ່ລະບົບ",
        forgetPassword: "ລືມບະຫັດຜ່ານ",
        choseServicer: "ຈົ່ງເລືອກ 1 ຜູ້ໃຫ້ບໍລິການ OpenID:",
        loading: "ກຳລັງແກ້ໄຂ...",
        btnLogin: "ເຂົ້າສູ່ລະບົບ",
        title: " ເຂົ້າສູ່ລະບົບ "
    };
    egov.resources.requiredSupplementary = {
        addRequiredTitle: "ເພີ່ມແບບຮຽກຮ້ອງປະກອບເພີ່ມເຕີມ"
    };
    egov.resources.main.links = "ເຊື່ອງຕໍ່";
    egov.resources.tree = {
        displayUnRead: "Có {0} ເອກະສານບໍ່ທັນອ່ານ",
        displayUnReadOnAll: "{0} ບໍ່ທັນອ່ານ / ລວມທັງໝົດ {1} ເອກະສານ",
        displayAll: "ມີທັງໝົດ {0} ເອກະສານ",
    };
    //#endregion

    //#region Version 1.2 - chưa dịch

    egov.resources.searching = {
        result: "ຜົນການຊອກຫາ"
    };

    egov.resources.common = extend(egov.resources.common, {
        messageYesBtn: "ມີ",
        messageNoBtn: "ບໍ່",
        messageCancelBtn: "ຜ່ານໄປ",
        messageOkBtn: "ຕົກລົງ",
        errorMessage: "ມີຜິດພາດເກີດຂຶ້ນ, ກະລຸນາເຮັດຄືນ ຫຼືແຈ້ງໃຫ້ຜູ້ບໍລິຫານ"
    });

    egov.resources.transfer = extend(egov.resources.transfer, {
        HasNoneDocument: "ເຈົ້າບໍ່ທັນເລືອກເອກະສານ!",
        messageNoBtn: "ບໍ່",
        messageCancelBtn: "ຜ່ານໄປ",
        messageOkBtn: "ຕົກລົງ",
    });

    egov.resources.document = extend(egov.resources.document, {
        errorLoadPrivateStore: "ມີຜິດພາດເກີດຂຶ້ນ ເມື່ອໂຫຼດບັນຊີລາຍຊື່ສຳນວນເອກະສານບຸກຄົນ",
        saveSuccess: "ບັນທຶກສຳນວນເອກະສານສຳເລັດ",
        ignoreConfirmRelation: "ບໍ່ຖາມຄືນອີກຄັ້ງ",
        ignoreConfirmRelationWarning: 'ສາມາດແປ່ງຄືນ config ນີ້ດ້ວຍວິທີເຂົ້າສູ່ການຕັ້ງຄ່າ->ອົງປະກອບອື່ນ->ເລິກ ເລືອກ "ສົ່ງເອກະສານຄັດຕິດຢ່າງເລື້ອຍໆ"',
        checkAll: "ເລືອກທັງໝົດ",
        displayAllComment: "ເບິ່ງຄຳເຫັດທັງໝົດ",
        displayOnly3Coment: "ຊ້ອນສ່ວນໃດໜຶ່ງຄຳເຫັນແກ້ໄຂ",
        anticipate: "ຄາດຄະເນສະສົ່ງ",
        addAnticipate: "ເພີ່ມຄາດຄະເນ",
        require: "ຮຽກຮ້ອງ",
        hasSpellError: 'ປະກົດເຫັນຄວາມຜິດຂຽນທວາຍ. ກົດ "' + egov.resources.common.messageYesBtn + '" ຖ້າຢາກສືບຕໍ່, ກົດ"' + egov.resources.common.messageNoBtn + '"ຖ້າຢາກແປ່ງຄືນຄວາມຜິດຂຽນທວາຍ.',
        errorSpell: {
            add: "ເພີ່ມເຂົ້າໃນສະໝຸດຂຽນທວາຍ",
            addSuccess: "ເພີ່ມສຳເລັດ",
            addError: "ມີຜິດພາດເກີດຂຶ້ນ"
        },
        notpermission: "ເຈົ້າບໍ່ມີສິດເບິ່ງເອກະສານນີ້!",
    });

    egov.resources.documentQuickView.noDocumentSelected = "ເລືອກເອກະສານ ເພື່ອສະແດງຂໍ້ມູນຫຍໍ້ ຢູ່ທີ່ນີ້.";
    egov.resources.documents.noDocumentCopyItem = "ດີເລິດ! ເຈົ້າບໍ່ມີເອກະສານໃດໃນຂໍ້ນີ້.";

    egov.resources.documents.notFound = "ລາຍການເອກະສານປັດຈຸບັນ ບໍ່ມີຜົນທີ່ເໝາະສົມ. ກົດ Enter ເພື່ອຊອກຫາ ໃນທົ່ວລະບົບ";

    egov.resources.documents.documentNumberDayOverdue = "QH {0} ກາຍວັນ"; //quá hạn
    egov.resources.documents.validDocuments = "ຍັງ {0} ວັນ";
    egov.resources.documents.validDocumentsInToday = "ມື້ນີ້";

    egov.resources.file = extend(egov.resources.file, {
        maxLength: "ຂະໜາດເກັບກຳຂໍ້ມູນສູງສຸດ: ",
        notAcceptFileTypes: "ໄຟລປະເພດນີ້ບໍ່ອະນຸຍາດໃຫ້ໂຫຼດຂຶ້ນ"
    });

    egov.resources.main = extend(egov.resources.main, {
        administrator: "ບໍລິຫານລະບົບ",
        reloadMessage: "ບາງການຕັ້ງຄ່າຮຽກຮ້ອງຕ້ອງ reload ຄືນລະບົບ. ເຈົ້າຢາກ reload ຄືນບໍ?",
        messageNoBtn: "ບໍ່",

        emptyMailNotifications: "ເຈົ້າບໍ່ມີແຈ້ງ mail ໃດ",
        openAllMail: "ເປີດ mail ທັງໝົດທີ່ໄດ້ຮັບ",

        emptyChatNotifications: "ເຈົ້າບໍ່ມີຂໍ້ຄວາມໃດ",
        openAllChat: "ເປີດໄຟລເອກະສານທັງໝົດທີ່ໄດ້ຮັບ",

        emptyDocumentNotifications: "ເຈົ້າບໍ່ມີແຈ້ງການເອກະສານໃດ!",
        openAllDocument: "ເປີດເອກະສານທັງໝົດທີ່ໄດ້ຮັບແຈ້ງ",
        downloaddesktopversion: "ໂຫຼດສະບັບ desktop",
        conversion: "ສົນທະນາ",

        bmail: "ຂ່າວຈັດການ",
        documents: "ແກ້ໄຂເອກະສານ",
        chat: "Chat",
        calendar: "ຕາຕະລາງ",
        links: "ເຊື່ອມຕໍ່",
        report: "ລາຍງານສະຖິຕິ",
        notJqueryAlert: "ບໍ່ທັນມີ file jquery. ກະລຸນາໂຫຼດເພີ່ມ file jquery!",
        lblDocument: "ເອກະສານ",
        lblNewConversion: "ສົນທະນາ",
        lblNewWorkTime: "ສ້າງຕາຕະລາງ",
        lblNewMail: "ສ້າງຈົດໝາຍ",
        searchDocument: "ຊອກຫາຂໍ້ມູນສຳນວນເອກະສານ, ເອກະສານ, ໄຟລຄັດຕິດ",
        searchMail: "ຊອກຫາ mail",
        youHave: "ເຈົ້າມີ",
        unreadDocuments: "ເອກະສານບໍ່ທັນເບິ່ງ",
    });

    egov.resources.toolbar = extend(egov.resources.toolbar, {
        DuKienPhatHanh: "ຄາດຄະເນຈະຈັດຈຳໜ່າຍ",
        addnewtemplate: "ເພີ່ມແບບໃໝ່",
    });

    egov.resources.attachment = extend(egov.resources.attachment, {
        download: "ດາວໂຫຼດມາ",
        notPermision: "ເຈົ້າບໍ່ມີສິດປະຕິບັດຈັ່ງຫວະນີ້"
    });
    egov.resources.setting.usersetting = {
        document: "ເອກະສານ, ສຳນວນເອກະສານ",
        shortkey: "ປຸ່ມລັດ",
        documentdefaultname: "ຊື່ເອກະສານ, ສຳນວນເອກະສານ ກຳນົດຕາຍຕົວ",
        supportkey: "ປຸ່ມສະໜັບສະໜູນ",
        fnname: "ຊື່ບົດບາດ",
        generalconfig: "ອົງປະກອບລວມ",
        selectdocument: "ເລືອກເອກະສານ, ສຳນວນເອກະສານ",

    };
    egov.resources.document = extend(egov.resources.document, {
        openError: "ເປີດເອກະສານທີ່ກ່ຽວຂ້ອງບໍ່ໄດ້",
        configError: "ອົງປະກອບບໍ່ຖືກ, ກະລຸນາລອງອີກຄັ້ງ",
        saveViolateSuccess: "ຮັບຮອງ CBLC ສຳເລັດ",
        table: {
            ລຳດັບ: "ລຳດັບ",
            creater: "ຜູ້ສ້າງ",
            datecreate: "ວັນທີສ້າງ",
            exprisedate: "ວັນໝົດອາຍຸ",
            lastcomment: "ຄຳເຫັນແກ້ໄຂສຸດທ້າຍ",

        },
        require: "ຮຽກຮ້ອງ"
    });

    egov.resources.time = {
        date: "ວັນທີ",
        _date: "ວັນທີ",
        minute: "ນາທີ",
        _minute: "ນາທີ",
        mon: "ວັນຈັນ",
        tue: "ວັນອັງຄານ",
        wed: "ວັນພຸດ",
        thi: "ວັນພະຫັດ",
        fri: "ວັນສຸກ",
        sat: "ວັນເສົາ",
        sun: "ວັນທິດ",
        morning: "ຕອນເຊົ້າ",
        affternoon: "ຕອນບ່າຍ",

    },

    egov.resources.enumResource = {
        columntable: {
            doccode: "ລະຫັດສຳນວນເອກະສານ",
            datereceived: "ວັນທີຮັບເອົາ",
            compendium: "ຄັດເອົາ",
            datecreate: "ວັນທີສ້າງ",
            email: "E-mail",
            creator: "ຜູ້ສ້າງ",
            dateapoint: "ວັນທີໝົດອາຍຸ",
            lastcomment: "ຄຳເຫັນແກ້ໄຂສຸດທ້າຍ",
            docnumber: "ເລກລະຫັດ",
        },
        actionlevel: {
            levelone: "ລະດັບ 1",
            leveltwo: "ລະດັບ 2",
            levelthree: "ລະດັບ 3",
            levelfour: "ລະດັບ 5",
        },
        activitylogtype: {
            dangnhap: "ເຂົ້າສູ່ລະບົບ",
            dangxuat: "ອອກຈາກລະບົບ",
            bangiao: "ມອບ - ຮັບເອກະສານ",
            thongbao: "ແຈ້ງເອກະສານ",
            huyvanban: "ຍົກເລິກເອກະສານ",
            ketthucvanban: "ວິ້ນສຸດເອກະສານ",
            phanloai: "ຈັດປະເພດເອກະສານ",
            phathanh: "ຈັດຈຳໜ່າຍເອກະສານ",
            kyduyet: "ເຊັນຜ່ານອະນຸມັດເອກະສານ",
            xinykien: "ຂໍຄຳເຫັນ",
            guiykien: "ສົ່ງຄຳເຫັນ",
            tiepnhan: "ຮັບເອົາ",
            xingiahan: "ຂໍຕໍ່ອາຍຸ",
            chuyenykiendonggop: "ສົ່ງຄຳເຫັນທີ່ປະກອບ",
        },
        categorybusinesstypes: {
            vbden: "ເອກະສານຂາເຂົ້າ",
            vbdi: "ເອກະສານຂາອອກ",
            hsmc: "ສຳນວນເອກະສານປະຕູດຽວ",
        },
        dailyprocesstimeforsearch: {
            allday: "ຕະຫຼອດວັນ",
            thirtyminutes: "30 ນາທີກ່ານ",
            onehour: "1 ຊົ່ວໂມງກ່ອນ",
            twohour: "2 ຊົ່ວໂມງກ່ອນ",
            am: "ຕອນເຊົ້າ",
            pm: "ຕອນບ່າຍ",
        },
        datetimereport: {
            trongngay: "ພາຍໃນວັນ",
            trongtuan: "ພາຍໃນອາທິດ",
            tuantruoc: "ອາທິດກ່ອນ",
            trongthang: "ພາຍໃນເດືອນ",
            thangtruoc: "ເດືອນກ່ອນ",
            quy1: "ໄຕມາດ 1",
            quy2: "ໄຕມາດ 2",
            quy3: "ໄຕມາດ 3",
            quy4: "ໄຕມາດ 4",
            trongnam: "ພາຍໃນປີ",
            namtruoc: "ປີກ່ອນ",
            tatca: "ທັ້ງໝົດ",
            tuychon: "ແລ້ວແຕ່ເລືອກ",
        },
        displaynotify: {
            hide: "ບໍ່ປະກົດ notify",
            shownotifyinprocess: "ປະກົດພຽງແຕ່ notify ເອກະສານລໍຖ້າແກ້ໄຂ",
            all: "ປະກົດທັງໝົດ notify ເອກະສານທີ່ກ່ຽວຂ້ອງ",
        },
        displaytreetype: {
            none: "ບໍ່ປະກົດ",
            unread: "ເອກະສານບໍ່ທັນອ່ານ",
            unreadonall: "ບໍ່ທນອ່ານ / ທັງໝົດ",
            all: "ທັງໝົດ",
        },
        documentprocesstype: {
            tiepnhan: "ຮັບເອົາ",
            bangiao: "ມອບ - ຮັບ",
            kyduyet: "ເຊັ່ນອະນຸມັດ",
            traketqua: "ສົ່ງຄືນຜົນ",
            tiepnhanbosung: "ຮັບເອົາເພີ່ມເຕີມ",
            giahan: "ຕໍ່ອາຍຸ",
        },
        documenttype: {
            thongbao: "ແຈ້ງການ",
            congvan: "ໜັງສືທາງລັດຖະການ",
        },
        egovjobenum: {
            indextimerelapsed: "IndexTimerElapsed",
            checkservices: "ກວດກາບັນດາ service ບໍ່ເຄື່ອນໄຫວ",
            getdocumentsfromedoctool: "ກວດກາເບິ່ງວ່າ ມີເອກະສານ ໃໝ່ສົ່ງມາບໍ",
            notifydocunread: "Notify ບັນດາເອກະສານບໍ່ທັນອ່ານ",
            notifydocinprocesses: "Notify ບັນດາເອກະສານລໍຖ້າແກ້ໄຂ",
            checkchangedfile: "ກວດກາ file ຖືກປ່ຽນແປງ",
            addindex: "ໝາຍ index ຊອກຫາ",
        },
        feetype: {
            indextimerelapsed: "ຮັບເອົາ",
            thuongbosung: "ມັກເພີ່ມເຕີມ",
            tracongdan: "ສົ່ງຄືນປະຊາຊົນ",
        },
        leveltype: {
            sobannganh: "ພະແນກ, ຂະແໜງການ",
            quanhuyen: "ເມືອງ",
            phuongxa: "ບ້ານ",
        },
        licensestatus: {
            capmoi: "ອອກໃໝ່",
            capdoi: "ອອກປ່ຽນອັນໃໝ່, ເພີ່ມເຕີມ",
            thuhoi: "ເກັບຄືນ",
        },
        option: {
            documentonlinereg: "ລົງທະບຽນອອນໄລມີບັນຊີແລ້ວ",
            documentonlineregnoaccount: "ລົງທະບຽນອອນໄລບໍ່ທັນມີບັນຊີ",
            acceptdoconline: "ຍອມຮັບເມື່ອລົງທະບຽນອອນໄລ",
            implementdoconline: "ຮຽກຮ້ອງເພີ່ມເຕີມເມື່ອລົງທະບຽນອອນໄລ",
            rejectdoconline: "ປະຕິເສດເມື່ອລົງທະບຽນອອນໄລ",
        },
        papertype: {
            tiepnhan: "ຮັບເອົາ",
            thuongbosung: "ມັກເພີ່ມເຕີມ",
            tracongdan: "ສົ່ງຄືນປະຊາຊົນ",
        },
        permissiontypes: {
            ktao: "ສ້າງເອກະສານ",
            xly: "ແກ້ໄຂເອກະສານ",
        },
        processfilterexpression: {
            groupby: "ກຣຸບຕາມ",
            equal: "ເທົ່າ",
            custom: "ອື່ນໆ",
        },
        scheduletype: {
            hangphut: "ທຸກນາທີ",
            hanggio: "ທຸກຊົ່ວໂມງ",
            hangngay: "ທຸກວັນ",
            hangtuan: "ທຸກອາທິດ",
            hangthang: "ທຸກເດືອນ",
        },
        searchtype: {
            document: "ຊອກຫາ ເອກະສານ",
            file: "ຊອກຫາໃນ file",
        },
        securitytype: {
            thuong: "ທຳມະດາ",
            mat: "ລັບ",
            toimat: "ລັບທີ່ສຸດ",
        },
        sendtype: {
            buudien: "ໄປສະນີ",
            email: "Email",
            fax: "Fax",
            traotay: "ປ່ຽນມື",
        },
        servicestatus: {
            running: "ກຳລັງເຄື່ອນໄຫວ",
            stoped: "ກຳລັງຢຸດ",
            paused: "ກຳລັງຢຸດຊົ່ວຄາວ",
            accessdenied: "ບໍ່ມີສິດເຂົ້າສູ່ service",
            notfound: "Service ບໍ່ທັນຕິດຕັ້ງໃນລະບົບ",
        },
        specialkeyenum: {
            nguoidangnhap: "ຜູ້ພິມບັດ",
            ngaythanghientai: "ວັນທີເດືອນປັດຈຸບັນ",
            meetingtitle: "ຫົວຂໍ້ກອງປະຊຸມ",
            meetingresource: "ສະຖານທີ່ກອງປະຊຸມ",
            meetingdate: "ເວລາດຳເນີນກອງປະຊຸມ",
            meetingtodate: "ເວລາສິ້ນສຸດ",
            meetingcreator: "ຜູ້ຈັດກອງປະຊຸມ",
            meetingbody: "ເນື້ອໃນກອງປະຊຸມ",
            meetingusers: "ຜູ້ເຂົ້າຮ່ວມກອງປະຊຸມ",
            meetinglastupdate: "ຜູ້ອັບເດດກອງປະຊຸມ",
        },
        supplementtype: {
            reset: "ຄິດໄລ່ຄືນເວລາ",
            'continue': "ສືບຕໍ່ແກ້ໄຂ",
            fixeddays: "ເພີ່ມວັນທີຕາຍຕົວ",
        },
        templatetype: {
            phieuin: "ບັດພິມ",
            email: "ຈົດໝາຍເອເລັກໂຕຼນິກ",
            sms: "ຂໍ້ຄວາມ sms",
        },
        timerjobtype: {
            warning: "ປ່າວເຕືອນ",
            searchindex: "ໝາຍຂໍ້ຊອກຫາ",
            deletetempfile: "ລຶບລ້າງ file ຊົ່ວຄາວ",
        },
        urgent: {
            thuong: "ທຳມະດາ",
            khan: "ດ່ວນ",
            hoatoc: "ດ່ວນທີ່ສຸດ",
        },
    }

    egov.resources.setting.SignatureSetting = "ສັນຍານຕັ້ງຄ່າ";


    //#endregion

    //#region Layout
    egov.resources = extend(egov.resources, {
        documentNotifications: "ແຈ້ງເອກະສານ",
        emptyDocumentNotifications: "ເຈົ້າບໍ່ມີແຈ້ງເອກະສານໃດ!",
        openAllDocument: "ເປີດເອກະສານທັງໝົດທີ່ໄດ້ຮັບແຈ້ງ",
        downloaddesktopversion: "ໂຫຼດສະບັບ desktop",
        gtv: "ແບບພິມ",
        notifications: "ແຈ້ງການ",
        news: "ຂ່າວຈັດການ",
        newEmail: "ຂຽນຈົດໝາຍ",
        config: "ຕັ້ງຄ່າ",
        reply: "ສົ່ງຄຳຕອບ",
        smallSize: "ເບິ່ງຂະໜາດນ້ອຍ",
        mediumSize: "ເບິ່ງຂະໜາດກາງ",
        largeSize: "ເບິ່ງຂະໜາດໃຫຍ່",
        underPreview: "ເບິ່ງກ່ອນດ້ານລຸ່ມ",
        rightPreview: "ເບິ່ງກ່ອນເບື້ອງຂວາ",
        hidePreview: "ຊ້ອນເບິ່ງກ່ອນ",
        reload: "ເປີດຄືນໃໝ່",
        logout: "ອອກຈາກລະບົບ",
        searchDocument: "ຊອກຫາ ເອກະສານ",
        searchFile: "ຊອກຫາ file ຄັດຕິດ",
        reloadMessage: "ອັບເດດສຳເລັດ. ເຈົ້າຢາກດາວໂຫຼດຄືນບໍ?",
        closeBtn: "ປິດ",
        submitBtn: "ອັບເດດ",
        titleMessage: "ແຈ້ງການ!",
        closeAll: "ປິດທັງໝົດ",
        report: "ສະຖິຕິ",
        contacts: "ປື້ມຕິດຕໍ່",
        calendar: "ຕາຕະລາງ",
        chat: "Chat",
        documentslabel: "ແກ້ໄຈໜັງສືທາງລັດຖະການ",
        placeholderSearch: "ຊອກຫາຂໍ້ມູນສຳນວນເອກະສານ, ເອກະສານ, ໄຟລຄັດຕິດ",

        administrator: "ບໍລິຫານລະບົບ",
        links: "ເຊື່ອມຕໍ່",
        conversion: "ສົນທະນາ",
        reloadMessage: "ບາງການຕັ້ງຄ່າຮຽກຮ້ອງໃຫ້ reload ຄືນລະບົບ. ເຈົ້າຢາກ reload ຄືນບໍ?",
        messageNoBtn: "ບໍ່",

        mailNotifications: "ແຈ້ງ mail",
        emptyMailNotifications: "ເຈົ້າບໍ່ມີການແຈ້ງ mail ໃດ",
        openAllMail: "ເປີດ mail ທັງໝົດທີ່ໄດ້ຮັບ",

        chatNotifications: "ແຈ້ງການ chat",
        emptyChatNotifications: "ເຈົ້າບໍ່ມີຂໍ້ຄວາມໃດ",
        openAllChat: "ເປີດຂໍ້ຄວາມທັງໝົດທີ່ໄດ້ຮັບ",

        bmail: "ຂ່າວຈັດການ",
        notJqueryAlert: "ບໍ່ທັນມີ file jquery. ກະລຸນາດາວໂຫຼດເພີ່ມ file jquery!",
        lblDocument: "ເອກະສານ",
        lblNewConversion: "ສົນທະນາ",
        lblNewWorkTime: "ສ້າງຕາຕະລາງ",
        lblNewMail: "ຂຽນຈົດໝາຍ",
        searchMail: "ຊອກຫາ mail",
        youHave: "ເຈົ້າມີ",
        unreadDocuments: "ເອກະສານບໍ່ທັນເບິ່ງ",
        "delete": "ລຶບ",

        setting: {
            title: "ສ້າງຕັ້ງສ່ວນບຸກຄົນ",
            ProfileConfig: "ຂໍ້ມູນສ່ວນບຸກຄົນ",
            Changepassword: "ປ່ຽນລະຫັດຜ່ານ",
            UserSetting: "ອົງປະກອບປຸ່ມລັດ",
            GeneralSettings: "ອົງປະກອບອື່ນໆ",
            SignatureSetting: "ອົງປະກອບລາຍເຊັນ",
            btnUpdateSetting: "ອັບເດດ",
            btnCloseSetting: "ປິດ",
            AuthorizesSetting: "ອົງປະກອບມອບສິດ",
            signature: {
                titleCreate: "ເພີ່ມໃໝ່ລາຍເຊັນ",
                titleEdit: "ອັບເດດລາຍເຊັນ",
                configPossition: "ອົງປະກອບທີ່ຕັ້ງຕິດຕັ້ງລາຍເຊັນ",
                configOther: "ອົງປະກອບອື່ນໆ",
                deleteMessage: 'ເຈົ້າແນ່ໃຈຢາກລຶບອົງປະກອບນີ້',
                labelCreate: "ເພີ່ມໃໝ່",
                table: {
                    header: {
                        ລຳດັບ: "ລຳດັບ",
                        configNameSignature: "ຊື່ອົງປະກອບ",
                        wordsNeedFind: "ຄຳສັບຕ້ອງການຊອກຫາ",
                        findTypes: "ປະເພດຊອກຫາ",
                        signTypes: "ປະເພດເຊັນ",
                        position: "ທີ່ຕັ້ງ",
                        edit: "ແປ່ງ",
                        "delete": "ລຶບ"
                    },
                    body: {
                        findTypeBottomToTop: "ລຸ່ມຂຶ້ນ",
                        findTypeTopToBottom: "ເທິງລົງ",
                        imageSignature: "ລາຍເຊັນຮູບພາບ",
                        textSignature: "ລາຍເຊັນແບບຕົວອັກສອນ",
                        leftPosition: "ເບື້ອງຊ້າຍ",
                        abovePosition: "ດ້ານເທິງ",
                        rightPosition: "ເບື້ອງຂວາ",
                        belowPosition: "ດ້ານລຸ່ມ",
                        noData: "ບໍ່ມີຂໍ້ມູນ"
                    },
                }
            },
            authorize: {
                titleCreate: "ເພີ່ມໃໝ່ຜູ້ຮັບມອບສິດ",
                titleEdit: "ອັບເດດ ລາຍເຊັນ",
                labelCreate: "ເພີ່ມໃໝ່",
                titleDialogDelete: "ແຈ້ງການ!",
                confirmDelete: "ເຂົ້າແນ່ໃຈຢາກລຶບອົງປະກອບນີ້",
                btnSubmitDelete: "ຕົກລົງ",
                btnCancelDelete: "ຍົກເລິກ",

                table: {
                    header: {
                        ລຳດັບ: "ລຳດັບ",
                        nameDocType: "ຊື່ປະເພດສຳນວນເອກະສານ",
                        userReceive: "ຜູ້ຮັບມອບສິດ",
                        startDate: "ວັນທີເລີ່ມຕົ້ນ",
                        endDate: "ວັນທີໝົດອາຍຸ",
                        state: "ສະພາບ",
                        edit: "ແປ່ງ",
                        "delete": "ລຶບ"
                    },
                    body: {
                        noData: "ບໍ່ມີຂໍ້ມູນ"
                    }
                }
            },
            general: {
                page: "ພາກສ່ວນໜ້າ",
                scrollLoadData: "ມ້ວນເມົາເພື່ອດາວໂຫຼດຂໍ້ມູນ",
                pagingLoadData: "ແບ່ງໜ້າໂຫຼດຂໍ້ມູນ",
                showDetailDocument: "ສະແດງລາຍລະອຽດເອກະສານ",
                showQuickView: "ສະແດງຫຍໍ້ເອກະສານ"
            },
            profile: {
                avatar: "ຮູບສະແດງ",
                choseAvatar: "ເລືອກ"
            },
            login: {
                account: "ບັນຊີ:",
                username: "ຊື່ເຂົ້າສູ່ລະບົບ",
                password: "ລະຫັດຜ່ານ:",
                keepingLogin: "ຮັກສາການເຂົ້າສູ່ລະບົບ!",
                loginType: "ຮູບແບບເຂົ້າສູ່ລະບົບ",
                forgetPassword: "ລືມລະຫັດຜ່ານ",
                choseServicer: "ຈົ່ງເລືອກໜຶ່ງຜູ້ສະໜອງການບໍລິການ OpenID:",
                loading: "ກຳລັງແກ້ໄຂ...",
                btnLogin: "ເຂົ້າສູ່ລະບົບ",
                title: "ເຂົ້າສູ່ລະບົບ"
            },
            usersetting: {
                document: "ເອກະສານ, ສຳນວນເອກະສານ",
                shortkey: "ປຸ່ມລັດ",
                documentdefaultname: "ຊື່ເອກະສານ, ສຳນວນເອກະສານກຳນົດຕາຍຕົວ",
                supportkey: "ປຸ່ສະໜັບສະໜູນ",
                fnname: "ຊື່ບົດບາດ",
                generalconfig: "ອົງປະກອບລວມ",
                selectdocument: "ເລືອກເອກະສານ, ສຳນວນເອກະສານ",
            }
        },
        common: {
            saveBtn: "ບັນທຶກໄວ້",
            cancelBtn: "ຜ່ານໄປ"
        }
    });

    //#endregion

    //#reigon v1.0 - đã dịch, không thêm mới vào đây
    egov.resources = extend(egov.resources, {
        activityLog: {
            questionDelete: "ເຈົ້າຢາກລຶບບັນດາບັນທຶກນີ້ບໍ?",
            notChoice: "'ເຈົ້າບໍ່ທັນເລືອກບັນທຶກຢາກລຶບ'"
        },
        crystalReport: {

        },
        level: {
            nodata: "ບໍ່ມີຂັ້ນການປົກຄອງໃດ",

        },
        license: {
            AddLicense: "ລົງທະບຽນ",
            RegisterLicense: "ລົງທະບຽນລິຂະສິດ",
            customername: "ລູກຄ້າ",
            Phone: "ເບີໂທລະສັບ",
            Email: "Email",
            ToDate: "ວັນໝົດອາຍຸ",
            TotalUser: "ບັນຊີເລກທີ",
            key: "ລະຫັດເປີດໃຊ້",
            customername: "",

        },
        log: {
            logNotSelect: "ເຈົ້າບໍ່ທັນເລືອກບັນທຶກຢາກລຶບ",
            deleteSelection: "ລຶບບັນທຶກໄດ້ເລືອກ",
            detail: "ລາຍລະອຽດບັນທຶກ",
            //view ບໍ່ທັນເຮັດ

        },
        notify: {
            noform: "ບໍ່ທັນມີຟອມໃດ",
            nouse: "ບໍ່ນຳໃຊ້",
            urgent: {
                name: "ລະດັບດ່ວນ",
                level1: "ທຳມະດາ",
                level2: "ດ່ວນ",
                level3: "ດ່ວນທີ່ສຸດ",
            },
            hasRead: "ໄດ້ຜ່ານອະນຸມັດ",
            alerttime: " ນາທີ (ຕັ້ງ = 0 ເພື່ອສົ່ງ sms ຫຼັງຈາກມີກອງປະຊຸມໃໝ່)",

        },
        office: {
            nooffice: "ບໍ່ມີອົງການໃດ",

        },
        paper: {
            nopaper: "ບໍ່ມີເອກະສານໃດ",
            other: "ອື່ນໆ",
            list: "ລາຍການເອກະສານ",
            action: "ວິຊາສະເພາະ",
            docfield: "ຂົງເຂດ",
            doctype: "ປະເພດສຳນວນ",
            addpaper: "ເພີ່ມໃໝ່ເອກະສານ",
            updatepaper: "ອັບເດດເອກະສານ",

        },
        people: {
            nopeople: "ບໍ່ມີຜູ້ໃຊ້ໃດ",
            peoplesearch: "ຊອກຫາບັນຊີ",
        },
        position: {
            sorterror: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອສັບຊ້ອນລະດັບບູລິມະສິດຂອງຕຳແໜ່ງ.",
            npposition: "ບໍ່ມີຕຳແໜ່ງໃດ",
        },
        //aaaaaaa
        printer: {
            addprinter: "ເພີ່ມໃໝ່ປິນເຕີ",
            editprinter: "ກຳນົດຄ່າໃຫ້ປິນເຕີ",
            nodata: "ບໍ່ມີປິນເຕີໃດ",
            notconnect: "ບໍ່ກວດກາການເຊື່ອມຕໍ່ເຖິງປິນເຕີໄດ້!",
            nameisrequired: "ຊື່ປິນເຕີບໍ່ໃຫ້ປະຫວ່າງ!",
        },
        processfunction: {
            selectcolor: "ເລືອກສີ",
            user: "ຜູ້ໃຊ້",
            position: "ຕຳແໜ່ງ",
            position1: "ພະແນກ/ຕຳແໜ່ງ",
            all: "ທັງໝົດ",
            failure: "ເຈົ້າປ້ອນຂໍ້ມູນຍັງບໍ່ທັນຄົບ ໃນສ່ວນກຳນົດຄ່າລາຍການ",
            setupforfilterlist: "ກຳນົດຄ່າລາຍການຊຸດຕອງໃຫ້ແກ່ node",
            setupforsamelist: "ກຳນົດຄ່າລາຍການເທົ່າກັບ node",
            enternote: "(ໃຊ້ຄີບອດ enter ເພື່ອລົງແຖວ (ຖ້າຢູ່ແຖວສຸດທ້າຍແມ່ນເພີ່ມແຖວໃໝ່), ໃຊ້ປຸ່ມຂຶ້ນລົງເພື່ອສັບ ຊ້ອນຖັນ)",
            divrole: "ແບ່ງສິດ",
            addnodefilter: "ເພີ່ມຊຸດຕອງໃໝ່ໃຫ້ node",
            parameterlisthaverequirement: "ເຈົ້າປ້ອນບໍ່ທັນຄົບຂໍ້ມູນໃນສ່ວນລາຍການໃລມູນ",
            docfieldnote1: "ຖ້າໃລມູນມີຜົນໄດ້ຮັບເປັນ DocFieldId ລະບົບຈະ",
            docfieldnote2: "ເຂົ້າໃຈໃນຕົວວ່າເອກະສານນັ້ນຈະຕອງຕາມຂົງເຂດ ແລະປະເພດສຳນວນ",
            columnname: "ຊື່ຖັນຜົນໄດ້ຮັບ",
            paraname: "ຊື່ໃລມູນ",
            updatenodetype: "ອັບເດດປະເພດ node",
            document: "ສຳນວນວຽກ",
            addNode: "ເພີ່ມ node ໃໝ່",
            copyNode: "Copy node ນີ້",
            paste: "ຕິດ node",
            "delete": "ລຶບ node ນີ້",
            confirmdeletenode: "ລຶບ 1 node ຈະລຶບທັງ node ຍ່ອຍ, ເຈົ້າແນ່ໃຈຢາກບໍ?",
            deletenodesuccessfull: "ລຶບ node ສຳເລັດ!",
            nodata: "ບໍ່ມີປະເພດ node ໃດ",
            nofilter: "ບໍ່ທີຊຸດຕອງໃດ",
            nogroup: "ບໍ່ມີກຸ່ມໃດ",
            alldocument: "---ເອກະສານ, ສຳນວນທັງໝົດ---",
            someinfoisrequired: "ເຈົ້າປ້ອນບໍ່ທັນຄົບຂໍ້ມູນໃນສ່ວນກຳນົດຄ່າລາຍການ",
        },
        question: {
            nodata: "ບໍ່ມີຄຳຖາມໃດ",
        },
        report: {
            //ບໍ່ທັນເຮັດ
            fileuploadrpt: "ອະນຸຍາດອັບຂຶ້ນພຽງແຕ່ໄຟລ *.rpt",
            showguidewhenwritingsqlquery: "ສະແດງແນະນຳການຂຽນ sql",
            showdatabygroup: "ກຳນົດຄ່າເບິ່ງຂໍ້ມູນຕາມກຸ່ມ",
            showgroupinreporttree: "ກຳນົດຄ່າສະແດງກຸ່ມຂຶ້ນຟອນລາຍງານ",
            _setting: "[ກຳນົດຄ່າ]",
            statisticSetting: "ກຳນົດຄ່າສະຖິຕິ",
            permissionreadreport: "ແບ່ງສິດໄດ້ພິດຈາລະນາລາຍງານ",
            privatesetting: "ກຳນົດຄ່າສະເພາະ",
            settingreport: "ນຳໃຊ້ກຳນົດຄ່າລາຍງານ",
            nodata: "ບໍ່ມີກຸ່ມລາຍງານໃດ",
            deletesuccessfull: "ລຶບລາຍງານສຳເລັດ",
            confirmdeletereport: "ເຈົ້າຢາກລຶບລາຍງານນີ້ ພ້ອມກັບລາຍງານຍ່ອຍທັງໝົດຂອງມັນ?",
        },
        notifications: {
            loading: "ກຳລັງໂຫຼດ...",
            haveError: "ມີຜິດພາດເກີດຂຶ້ນ",
            nolog: "ບໍ່ມີບັນທຶກໃດ",
            processing: "ກຳລັງແກ້ໄຂ...",
            emailSuccess: "ສົ່ງ email ສຳເລັດ!",
            emailError: " ສົ່ງ email ບໍ່ສຳເລັດ!",
            sendEmailError: "ມີຜິດພາດເມື່ອສົ່ງ mail",
            queryEmbryonicForm: "ປະໂຫຍກສອບຖາມບໍ່ໃຫ້ປະຫວ່າງ!",
            changeStatusEmbryonicForm: "ລະບົບບໍ່ປ່ຽນແປງສະພາບໄດ້",

        },
        resource: {
            nodata: "ບໍ່ມີ resource ໃດ",
            choosefileimport: "ເລືອກໄຟລ import",
        },
        role: {
            nodata: "ບໍ່ທັນມີຜູ້ໃຊ້ໃດ",
            isallow: "ອະນຸຍາດ",
            rolename: "ຊື່ສິດທິ",
            nodatagroup: "ບໍ່ມີກຸ່ມຜູ້ໃຊ້ໃດ",

        },
        scopearea: {
            nodata: "ບໍ່ມີ ScopeArea ໃດ",
        },
        setting: {
            sendemailto: "ສົ່ງ email ກວດກາເຖິງ",
            sendemailsuccess: "ສຳເລັດຜົນ!",
            sendemailfailure: "ບໍ່ສຳເລັດຜົນ!",
            smtpsetting: "ກຳນົດຄ່າເຄື່ອງເຊີເວີ SMTP",
            othersetting: "ກຳນົດຄ່າອື່ນໆ",
            location: {
                addlocation: "ເພີ່ມທີ່ຕັ້ງ",
                editlocation: "ແປ່ງທີ່ຕັ້ງ",
                confirmdeletefilelocation: "ເຈົ້າແນ່ໃຈຢາກລຶບທີ່ຕັ້ງບັນທຶກ file ນີ້ບໍ?",
                canotdelete: "ທີ່ຕັ້ງບັນທຶກ file ນີ້ໄດ້ນໍາໃຊ້ແລ້ວ, ເຈົ້າບໍ່ໄດ້ລຶບ.",
                listfilelocation: "ລາຍການບ່ອນບັນທຶກ file",
                nodata: "ບໍ່ທັນມີກຳນົດຄ່າທີ່ຕັ້ງບັນທຶກ file",
                canotdelete: "",

            },
            general: {
                finishdocument: "ສິ້ນສຸດສຳນວນ/ເອກະສານ",
                setting: "ກຳນົດຄ່າ Page ໜ້າຫຼັກ",
                nofifysetting: " ກຳນົດຄ່າ notify",
            },
            passwordpolicy: {
                checkpassword: "ກວດກາກະຈີວິພາດລະຫັດຜ່ານ",
                lookaccount: "ປິດບັນຊີ",
                passworddeadtime: "ໝົດອາຍຸລະຫັດຜ່ານ",
                passwordchangehistory: "ປະຫວັດການປ່ຽນລະຫັດຜ່ານ",
            },

        },
        shared: {
            productname: "Bkav eGovernment - ບໍລິຫານລູກຄ້າ",
            systemtree: "ຟອນລະບົບ",
            home: "ໜ້າຫຼັກ",
            admincustomer: " ບໍລິຫານລູກຄ້າ ",
        },
        store: {
            pts: "(ຮັບຜິດຊອບປື້ມ)",
            nouser: "ບໍ່ທັນມີຜູ້ໃຊ້ໃດ",
            tempforstore: "ລາຍການແບບຟອມສຳລັບປື້ມ",
            alltempname: "ຊື່ແບບຟອມທັງໝົດ",
            notemp: "ບໍ່ຄົງຕົວແບບຟອມໃດ!",
            _all: "[ທັງໝົດ]",
            nodocumentstore: "ບໍ່ມີປື້ມສຳນວນໃດ",

        },
        template: {
            nodata: "ບໍ່ມີແບບຟອມໃດ",
            key: "Key ໃຊ້ຮ່ວມ",
            systemerror: "ລະບົບຜິດພາດ, ບໍ່ປ່ຽນສະພາບແບບຟອມບັດໄດ້",

        },
        templatekey: {
            onoffguide: "ເປີດ/ປິດການແນະນຳ",
            showguide: "ສະແດງແນະນຳເມື່ອຂຽນລະຫັດ key, sql, template",
            ex: "ຕຢ:",
            needparameter: "ປະໂຫຍກສອບຖາມຕ້ອງມີໃລຜົນ",
            parametercanuseinquery: "ບັນດາໃລຜົນອາດຈະໃຊ້ໃນປະໂຫຍກສອບຖາມ ",
            keyformat: "ກຳນົດ key ໄດ້ຮັບອະນຸຍາດ",
            speccharacter: "{[a-zA-Z0-9_]+}",
            keyformat2: "ລວມທັງບັນດາໂຕໜັງສື (ລວດລາຍ, ທໍາມະດາ), ໂຕເລກ ແລະຂີດກ້ອງ (_).",
            getvalueintempdoc: "ເອົາຜົນໃນແບບຟອມໃດຂອງສຳນວນ.",
            currentuserid: "Id ຜູ້ເຂົ້າສູ່ປັດຈຸບັນ.",
            doctype: "ປະເພດເອກະສານ.",
            costtype: "ປະເພດຄ່າທຳນຽມ",
            additiondoc: "ລາຍການບັນດາເອກະສານປະກອບເພີ່ມ.",
            formatofresulequery: "ຜົນປະໂຫຍກສອບຖາມໄດ້ເອົາຕາມແບບ",
            fieldname: "ten_truong",
            sqlresult: "ເປັນຜົນຂອງປະໂຫຍກ sql ແບບ",
            dataprocessfunctions: "ບັນດາຟົງຊີອົງແກ້ໄຂຂໍ້ມູນ",
            exdataconvertfunctions: "ທັງໝົດບັນດາຟົງຊີອົງ convert ຂໍ້ມູນ:",
            stringprocessingfuntions: "ທັງໝົດຟົງຊີອົງແກ້ໄຂພວງ:",
            datefunctions: "ບັນດາຟົງຊີອົງແກ້ໄຂທັງໝົດປະຈຳເດືອນ:",
            stringformats: "ສ້າງແບບແບບພວງທັງໝົດ:",
            viewdetail: "ເບິ່ງລາຍລະອຽດທີ່:",
            selectdocument: "ເລືອກແບບສຳນວນ",
            selecttemplate: "ເລືອກແບບຟອມ",
            selectdocfield: "ເລືອກຂົງເຂດ",
            keycode: "ລະຫັດ key",
            nokey: "ບໍ່ມີ key ໃດ",
        },

        time: {
            timenotcheck: "ບໍ່ກວດກາເວລາໄດ້",
            checkdate: "ກວດກາວັນທີ",
            caculateextendtime: "ຄິດໄລ່ຕາຕະລາງພັກຊົດເຊີຍ",
            nodata: "ບໍ່ມີວັນພັກໃດ",
            repeat: "ຊ້ຳຄືນ",
            repeatbyyear: "ຊ້ຳຕາມເດືອນ",
            freeday: "ຊື່ວັນພັກ",
            DL: "ວັນເດືອນຈັນ",
            AL: "ວັນປະຕິທິນ",
            day: "ວັນ",
            listofrestday: "ລາຍການວັນພັກໃນປີ",
            weekworktime: "ເວລາເຮັດວຽກໃນອາທິດ",

        },
        transfertype: {
            nodata: "ບໍ່ມີຮູບການຍົກຍ້າຍໃດ",
        },
        user: {
            username: "ຊື່ເຂົ້າສູ່ລະບົບ",
            fullname: "ຊື່ ແລະນາມສະກຸນ",
            phone: "ເບີໂທລະສັບ",
            usernameexist: "ຊື່ເຂົ້າສູ່ລະບົບມີຢູ່ແລ້ວ",
            notindepartment: "ບໍ່ມັນຂຶ້ນກັບພະແນກໃດ ",
            rolename: "ຊື່ສິດທິ",
            groupname: "ຊື່ກຸ່ມ",
            isadministrator: "ເປັນບໍລິຫານ",
            ismaindepartment: "ເປັນພະແນກຫຼັກ",
            position: "ນາມຕຳແໜ່ງ",
            position1: "ຕຳແໜ່ງ",
            departmentname: "ຊື່ພະແນກ",
            nodata: "ບໍ່ມີຜູ້ໃຊ້ໃດ",
            all: "---ທັງໝົດ---",
            active: "ເຄື່ອນໄຫວ",
            unactive: "ບໍ່ເຄື່ອນໄຫວ",
            confirmtoresetpassword: "ເຈົ້າແນ່ນອນຢາກ reset ລະຫັດຜ່ານໃຫ້ແກ່ບັນຊີນີ້ບໍ?",
            resetpasswordsuccess: "Reset ລະຫັດຜ່ານສຳເລັດ!",
            selectusertoimport: "ເຈົ້າຕ້ອງເລືອກຜູ້ໃຊ້ເພື່ອ import",
            importusersuccessfull: "Import ຜູ້ໃຊ້ສຳເລັດ",
        },
        ward: {
            city: "ແຂວງ/ນະຄອນ:",
            district: "ເມືອງ:",
            nodata: "ບໍ່ມີຕາແສງ/ຄຸ້ມໃດ",
            updatedata: "ອັບເດດຕາແສງ/ຄຸ້ມ",
            datalist: "ລາຍການຕາແສງ/ຄຸ້ມ",
        },
        welcome: {},

        notifications: {
            searching: "ກຳລັງຊອກຫາ...",
            loading: "ກຳລັງໂຫຼດ...",
            haveError: "ມີຜິດພາດເກີດຂຶ້ນ",
            nolog: "ບໍ່ມີບັນທຶກໃດ",
            processing: "ກຳລັງແກ້ໄຂ...",
            destroythisfilter: "ເຈົ້າຢາກລຶບຊຸດຕອງນີ້",
            post: "ແຈ້ງການ",
            confirmdelete: "ເຈົ້າແນ່ນອນຢາກລຶບບໍ?",
            updatesuccessful: "ອັບເດດສຳເລັດ",
            createsuccessful: "ເພີ່ມໃໝ່ສຳເລັດ",
            downloaderror: "ໂຫຼດ file ຜິດພາດ",
            choosefileimport: "ເຈົ້າຕ້ອງເລືອກໄຟລ import",
            importing: "ກຳລັງ import...",
            dowloadfileerror: "ມີຜິດພາດເກີດຂຶ້ນເມື່ອໂຫຼດ file",

        },
        buttons: {
            select: "ເລືອກ",
            selectAll: "ເລືອກໝົດ",
            edit: "ແປງ",
            "delete": "ລຶບ",
            orderedsort: "ສັບຊ້ອນໃໝ່ລຳດັບ",
            orderedsave: "ບັນທຶກລຳດັບ",
            addfilter: "ເພີ່ມຊຸດຕອງໃໝ່",
            addparameter: "ເພີ່ມໃລຜົນ",
            save: "ບັນທຶກ",
            confirm: "ຮັບຮອງ",
            back: "ກັບຄືນ",
        },
        tableheader: {
            stt: "ລ/ດ",
            "function": "ບົດບາດ",
            description: "ອະພິບາຍ",
            form: "ແບບ",
            edit: "ແປງ",
            select: "ເລືອກ",
            "delete": "ລຶບ",
            type: "ປະເພດ",
            formname: "ຊື່ແບບ",
            filtername: "ຊື່ຊຸດຕອງ",
            columnname: "ຊື່ຖັນ",
            displayname: "ຊື່ສະແດງ",
            width: "ລວງກວ້າງ",
            allowsort: "ອະນຸຍາດໃຫ້ສັບຊ້ອນ",
            sortcolumn: "ຖັນສັບຊ້ອນ",
            sort: "ສັບຊ້ອນ",
            type: "ແບບ",
            value: "ຄຸນຄ່າ",
            name: "Name",
            domain: "Domain",
            ip: "ip",
            zone: "ເຂດ",
            isallow: "ອະນຸຍາດ",
            addordelete: "ເພີ່ມ/ເລິກ",
        },
        commonlabel: {
            list: "ລາຍການ",
            select: "ເລືອກ",
            is: "ເປັນ",
            or: "ຫຼື...",
            select: "ເລືອກ",
            addcolumn: "ເພີ່ມຖັນ",
            add: "ເພີ່ມ",
            addnew: "ເພີ່ມໃໝ່ ",
            cancel: "ຍົກເລິກ",
            note: "*ເອົາໃຈໃສ່:",
            time: {
                date: "ວັນທີ",
                _date: "ວັນທີ",
                minute: "ນາທີ",
                _minute: "ນາທີ ",
                mon: "ວັນຈັນ",
                tue: "ວັນອັງຄານ",
                wed: "ວັນພຸດ",
                thi: "ວັນພະຫັດ",
                fri: "ວັນສຸກ",
                sat: "ວັນເສົາ",
                sun: "ວັນອາທິດ",
                morning: "ຕອນເຊົ້າ",
                affternoon: "ຕອນແລງ",

            },
            contact: "ຕິດຕໍ່",
            errorpage: "Sorry, an error occurred while processing your request.",
            all: "ທັງໝົດ",
            email: "Email",
            sms: "SMS",
            printcard: "ບັດພິທມ",
            vnconcurency: "vnd",
            reject: "ເລິກ",
            yes: "ມີ",
            no: "ບໍ່",
            search: "ຊອກຫາ",
            allow: "ອະນຸຍາດ",
            notallow: "ບໍ່ອະນຸຍາດ",
            "delete": "ລຶບ",
        }
    })

    //#endregion

    //#region v1.1 - đã dịch, không thêm mới vào đây
    egov.resources = extend(egov.resources, {
        crystalreport: {
            copyfromstatisticform: "ສຳເນົາຈາກແບບຟອມສະຖິຕິ",
            copyfromreportform: "ສຳເນົາຈາກແບບຟອມລາຍງານ",
            reconfig: "Config ຄືນແຕ່ຕົ້ນ",
        },
        doctype: {
            othernodes: "ບັນດາປຸ່ມອື່ນ",
            addcontrol: "ເພີ່ມ control",
            downloadworkflowerror: "ມີຂໍ້ຜິດພາດເມື່ອດາວໂຫຼດຂັນຕອນ",
            newworkflow: "ເພີ່ມຂັນຕອນໃໝ່",
            workflownameisrequired: "ເຈົ້າຕ້ອງປ້ອນຊື່ຂັ້ນຕອນ",
            pasteworkflow: "ຕິດຂັ້ນຕອນ",
            usethisworkflow: "ໃຊ້ຂັ້ນຕອນນີ້",
            comfirmtocancel1: "ເຈົ້າແນ່ໃຈບໍ",
            comfirmtocancel2: "ເລິກ",
            comfirmtocancel3: "ໃຊ້ຂັ້ນຕອນນີ້ບໍ?",
            update: "ອັບເດດລາຍການ",
            editTemplateWorkflow: "ອື່ນໆ",
            updatenode: "ອັບເດດ node",
            interfaceconfig: "ອົງປະກອບໜ້າຈໍພົວພັນ",
            editthisworkflow: "ແປ່ງຂັ້ນຕອນນີ້",
            copythisworkflow: "Copy ຂັ້ນຕອນນີ້",
            deletethisworkflow: "ລຶບຂັ້ນຕອນນີ້",
            confirmtodeltethisworkflow: "ເຈົ້າແນ່ໃຈຢາກລຶບຂັ້ນຕອນນີ້ບໍ?",
            notyouthisworkflow: "ເລິກການໃຊ້ຂັ້ນຕອນນີ້",
            workflowname: "ຊື່ຂັ້ນຕອນ",
            exprisedate: "ກຳນົດເວລາການແກ້ໄຂ",
            date: "ວັນທີ",
            addtemplateform: "ເພີ່ມແບບຟອມ",
            noformdata: "ບໍ່ມີແບບຟອມໃດ",
            doctypename: "ຊື່ສຳນວນເອກະສານ: ",
            docfield: "ຂົງເຂດ: ",
            status: "ສະພາບ:",
            active: "ເປີດນຳໃຊ້",
            notactive: "ບໍ່ເປີດນຳໃຊ້",
            noconfiguration: "ບໍ່ມີອົງປະກອບ",
            confirmtodeletereceivenode: "ເຈົ້າມີຕົກລົງລຶບ node ເຖິງນີ້ບໍ?",
            objectype: "ແບບເປົ້າໝາຍ",
            value: "ຄຸນຄ່າ",
            'delete': "ລຶບ",
            receivepostlist: "ລາຍການຮັບແຈ້ງການ",
            removeReceiveList: "ເຈົ້າຕົກລົງລຶບລາຍການແຈ້ງການນີ້ບ?",
            of: "ຂອງ",
            allpeople: "ທຸກຄົນ",
            level: "ອອກ",
            user: "ຜູ້ໃຊ້",
            alljobtitle: "ທັງໝົດນາມຕຳແໜ່ງ",
            alljobtitleof: "ທັງໝົດນາມຕຳແໜ່ງຂອງ",
            alljobtitleof1: "ທັງໝົດ",
            alljobtitleof2: "ຂອງ",
            jobtitledepartment: "ນາມຕຳແໜ່ງ-ຫ້ອງ, ພະແນກ",
            alluserof: "ທັງໝົດ user ຂອງ",
            fieldisrequired: "ເຈົ້າຕ້ອງປ່ອນຄຸນຄ່າ",
        },
        form: {
            updateform: "ອັບເດດປະເພດສຳນວນເອກະສານ, ເອກະສານ",
            formname: "ຊື່ຕົວຢ່າງ",
            description: "ພັນລະນາ",
            tempkey: "ແບບຟອມ",
            status: "ສະພາບ",
            config: "ອົງປະກອບ",
            status1: "ກຳລັງນຳໃຊ້",
            status1: "ບໍ່ນຳໃຊ້",
            status3: "ຕົວຢ່າງບັນທຶກຊົ່ວຄາວ",
            configformtitle: "Title",
            addtitle: "ເພີ່ມຫົວຂໍ້ (ຍີ່ຫໍ້)",
            showgrid: "ສະແດງເສັ້ນຂີດ",
            shownumber: "ສະແດງຕົວເລກ",
            addrow: "ເພີ່ມແຖວ",
            chooseextendfield: "- - - ເລືອກຫ້ອງຂະຫຍາຍ - - - ",
            choosedocumenttype: "- - - ເລືອກປະເພດເອກະສານ - - - ",
            title: "ເພີ່ມຫົວຂໍ້ (ຍີ່ຫໍ້)",
            brand: "ຫ້ອງປ້ອນຂໍ້ມູນ (ຍີ່ຫໍ້)",
            references: "ພົວພັນ",
        },
        guide: {
            nodata: "ບໍ່ມີບົດແນະນຳໃດ"
        },
        increase: {
        },
        infomation: {
            chageinfo: "ປ່ຽນແປງຂໍ້ມູນ",
        },
        jobtitles: {
            nodata: "ບໍ່ມີນາມຕຳແໜ່ງໃດ",
            dragordroptosort: "ອະນຸຍາດໃຫ້ຈັດລຳດັບດ້ວຍວິທີແກ່ປ່ອຍ",
            sorterror: "ມີຜິດພາດເມື່ອຈັດລຳດັບການໃຫ້ບູລິມະສິດຂອງນາມຕຳແໜ່ງ.",
        },
        editor: {
            deletecol: "ລຶບຖັນ",
            deleterow: "ລຶບແຖວ",
            insertabove: "ຈີມເທິງ",
            insertbelow: "ຈີມລຸ່ມ",
            insertleft: "ຈີມຊ້າຍ",
            insertright: "ຈີມຂວາ",
            merge: "ໂຮມເຂົ້າເປັນໜຶ່ງ",
            splitcolumn: "ແບ່ງຖັນ",
            splitrow: "ແບ່ງແຖວ",
            update: "ອັບເດດ",
            all: "[ທັງໝົດ]"

        },
        buttons: {
            deleteall: "ລຶບໝົດ",
            search: "ຊອກຫາ",
            agree: "ຕົກລົງ",
            ignore: "ຜ່ານໄປ",
            add: "ເພີ່ມ",
            view: "ເບິ່ງ"
        },
        commonlabel: {
            other: "ອື່ນໆ",
            haveerrortryagain: "ມີຜິດພາດເກີດຂຶ້ນ, ກະລຸນາລອງອີກຄັ້ງ!"
        }
    });
    //#endregion

    //#region V1.2 đã dịch, không thêm mới vào đây
    egov.resources = extend(egov.resources, {
        setting: {
            general: {
                loadpagescroll: "ມ້ວນໜ້າ",
                loadpagesize: "ແບ່ງໜ້າ",
                language: "ພາສາ: ",
                useVietNameseTyping: "ໃຊ້ຊຸດພິມພາສາຫວຽດນາມ"
            },
        },
        processfunction: {
            parent: "Cha",
            normal: "ທຳມະດາ",
            field: "ຫ້ອງຂໍ້ມູນ",
            type: "ແບບ",
            displayname: "ຊື່ສະແດງ",
            filterByOverdueDate: "ຕອງຕາມກຳນົດເວລາແກ້ໄຂ:",
            display: "ສະແດງ",
            defaultdocumentsortconfig: "ອົງປະກອບຈັດລຳດັບເອກະສານແບບຕາຍຕົວ",
            entertobreakpage: "(ໃຊ້ປຸ່ມ enter ເພື່ອລົງແຖວ (ຖ້າແຖວສຸດທ້າຍແມ່ນເພີ່ມແຖວໃໝ່), ໃຊ້ປຸ່ມຂຶ້ນລົງເພື່ອສັບ ຊ້ອນຖັນ)",
            configlistbynode: "ອົງປະກອບລາຍການທຽບເທົ່າກັບ node",

        },
        template: {
            printorder: "ບັດພິມ",
            per1: "ຮັບເອົາ",
            per2: "ມອບ - ຮັບ",
            per4: "ເຊັນຜ່ານອະນຸມັດ",
            per8: "ຕອບຜົນ",
            per16: "ຮັບເອົາເພີ່ມເຕີມ",
            per32: "ຕໍ່ອາຍຸ",
        },
        form: {
            title: "ອົງປະກອບຕົວຢ່າງໃຫ້ແກ່ປະເພດສຳນວນເອກະສານ",
            currentusername: "ຊື່ຜູ້ເຂົ້າສູ່ລະບົບປັດຈຸບັນ",
            docfieldname: "ຊື່ຂົງເຂດ",
            doctypename: "ຊື່ປະເພດສຳນວນເອກະສານ",
            doccode: "ລະຫັດສຳນວນເອກະສານ",
            receivedate: "ວັນທີຮັບເອົາ",
            appointdate: "ວັນທີນັດສົ່ງຄືນຜົນ",
            template: "ຕົວຢ່າງ:",
            insertspecialvalue: "ຈີມບັນດາຄຸນຄ່າພິເສດ:",

        },
        report: {
            config: "ອົງປະກອບລາຍງານ",
            configsetup: "[ອົງປະກອບ]",
            showguide: "ເປີດ/ປິດແນະນຳ",
        },
        law: {
            choosedocument: "ເລືອກເອກະສານ",
            lawnumbercode: "ເລກເຄື່ອງໝາຍ",
        },
        code: {
            choosedepartment: "ເລືອກຫ້ອງ, ພະແນກ",
        },
        store: {
            choosecategory: "ເລືອກວິຊາການ",
            addstoreviewer: "ເພີ່ມຜູ້ເບິ່ງປື້ມ",
        },
        time: {
            worktime: "ໂມງລັດຖະການ",
            listoffsetday: "ລາຍການວັນເຮັດວຽກຊົດເຊີຍໃນປີ",
        },
        deparment: {
            choosejobtitle: "ເລືອກນາມຕຳແໜ່ງ",
            chooseposition: "ເລືອກຕຳແໜ່ງ",
            listuser: "ລາຍການພະນັກງານທີ່ສັງກັດຫ້ອງ, ພະແນກ",
            nouser: "ບໍ່ທັນມີຜູ້ໃຊ້ໃດ",
            nodata: "ບໍ່ທັນມີຫ້ອງ, ພະແນກໃດ",
            addsubdeparment: "ເພີ່ມໃໝ່ຫ້ອງ, ພະແນກທີ່ຂຶ້ນກັບ",
            deparmentinfo: "ຂໍ້ມູນຫ້ອງ, ພະແນກ",
            deparmentname: "ຊື່ຫ້ອງ, ພະແນກ",
            updateinfo: "ອັບເດດຂໍ້ມູນຫ້ອງ, ພະແນກ",
            adduser: "ເພີ່ມຜ້ໃຊ້ເຂົ້າໃນຫ້ອງ/ພະແນກ",
            fullname: "ຊື່ ແລະນາມສະກຸນ",
            isadmin: "ເປັນບໍລິຫານ",
            jobtitle: "ນາມຕຳແໜ່ງ",
            position: "ຕຳແໜ່ງ",
            list: "ລາຍການຫ້ອງ, ພະແນກ"
        },
        position: {
            orderedsort: "ອະນຸຍາດໃຫ້ສັບຊ້ອນລຳດັບດ້ວຍວິທີແກ່ປ່ອຍ",
        },
        bkavmessagebox: {
            useshowtoreplacealert: "ໃຊ້ eGovMessage.show(message, title) ເພື່ອປ່ຽນແທນໃຫ້ messageBoxAlert().",
            useshowtoreplaceconfirm: "ໃຊ້ eGovMessage.show(message, title, messageButtons.OkCancel) ເພື່ອປ່ຽນແທນໃຫ້ messageBoxConfirm().",
            usenotificationtoreplacetemp: "ໃຊ້ eGovMessage.notification() ເພື່ອປ່ຽນແທນໃຫ້ messageTemp().",
            closebutton: "ປິດ",
            yes: "ຕົກລົງ",
            no: "ປາສະຈາກ",
            ok: "ຢືນຢັນ",
            cancel: "ຍົກເລິກ",
            notify: "ແຈ້ງການ",

        },
        tableheader: {
            sortcolumnname: "ຊື່ຖັນທີ່ສັບຊ້ອນ",
            sorttype: "ແບບສັບຊ້ອນ (ເພີ່ມ ຫຼືຫຼຸດ)",
            order: "ລຳດັບ",
            sortcolumn: "ຖັນທີ່ສັບຊ້ອນ",
            isallowsort: "ອະນຸຍາດໃຫ້ສັບຊ້ອນ",
            displayname: "ຊື່ສະແດງ",
            width: "ລວງກວ້າງ",
        },
        commonlabel: {
            deincrease: "ຫຼຸດລົງ",
        },
    });

    //#endregion

    //#region v1.3 - chưa dịch
    egov.resources.sitemap = {
        config: "Cấu hình - cd",
        general: "Thiết lập hệ thống - cd",
        processfunction: "Cây văn bản - cd",
        resource: "Tài nguyên - cd",
        activitylog: "Nhật ký hành động - cd",
        egovjob: "Timmer - cd",
        config4: "Biểu mẫu - cd",
        template: "Quản lý mẫu phiếu - cd",
        templatekey: "Quản lý key - cd",
        formgroup: "Quản lý nhóm biểu mẫu - cd",
        form: "Quản lý biểu mẫu - cd",
        embryonicform: "Quản lý mẫu phôi - cd",
        config5: "Báo cáo - cd",
        report: "Báo cáo động - cd",
        notify: "Thông báo - cd",
        config2: "Danh mục - cd",
        categorybusiness: "Nghiệp vụ - cd",
        docfield: "Lĩnh vực - cd",
        doctype: "Loại văn bản - cd",
        configworkflow: "Quy trình - cd",
        category: "Loại văn bả - cd",
        increase: "Nhảy số - cd",
        code: "Bảng mã - cd",
        store: "Sổ hồ sơ - cd",
        catalog: "Danh mục tùy chọn - cd",
        keyword: "Từ khóa - cd",
        transfertype: "Hình thức gửi - cd",
        time: "Thời gian làm việc - cd",
        address: "Địa chỉ - cd",
        config3: "Cơ cấu tổ chức - cd",
        imfomation: "Cơ quan - cd",
        department: "Phòng ban - cd",
        jobtitles: "Chức danh - cd",
        position: "Chức vụ - cd",
        user: "Cán bộ - cd",
        role: "Vai trò và quyền hạn - cd",
        authorize: "Ủy quyền - cd",
        client: "Client - cd",
        people: "Người dùng - cd",
        scopearea: "Vùng truy cập - cd",
        config6: "eGovOnline - cd",
        office: "Cơ quan - cd",
        law: "Văn bản quy phạm - cd",
        guide: "Hướng dẫn - cd",
        question: "Câu hỏi - cd",
    }

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
    egov.resources = extend(egov.resources, {
        template: {
            specialkey: "Key đặc biệt - cd",
            keyfromform: "Key từ biểu mẫu"
        },
        doctype: {
            embryonicformname: "Tên mẫu phôi: - cd",
            embryonicformlist: "Danh sách các mẫu phôi: - cd",
            addnewform: "Thêm mẫu mới - cd",
        },
        notify: {
            noJquery: "Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng - cd",
            noUnderscore: "Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng - cd",
        },
        workflow: {
            user: "Cán bộ - cd",
            position: "Vị trí - cd",
            relation: "Quan hệ - cd",
            belowoffice: "Đơn vị cấp dưới - cd",
            underoffice: "Đơn vị trực thuộc - cd",
            currentoffice: "Đơn vị hiện tại - cd",
            sameoffice: "Cùng đơn vị - cd",
            peernode: "Node ngang hàng - cd",
            sameparentnode: "Cùng Node cha - cd",
            underuser: "Cấp dưới - cd",
            overuser: "Cấp trên - cd",
            addnotifyfouser: "Add User receive notification",
            nodename: "Node name",
            choosedepartment: 'Choose Department',
            listuser: 'User list'

        },
        formtemplate: {
            columnwidth: "Chiều rộng cột - cd",
            brandwidth: "Chiều rộng nhãn - cd",
            height: "Chiều cao - cd",
            disable: "Vô hiệu hóa - cd",
            verifydata: "Có kiểm tra dữ liệu - cd",
            compendium: "Trích yếu - cd",
            comment: "Ý kiến xử lý - cd",
            doctype: "Loại văn bản - cd",
            category: "Hình thức - cd",
            inoutplace: "Đơn vị - cd",
            dateappointed: "Thời hạn xử lý - cd",
            organization: "Cơ quan gửi - cd",
            doccode: "Số/ký hiệu * - cd",
            doccode2: "Số hiệu * - cd",
            datearrived: "Ngày đến - cd",
            dateresponse: "Hồi báo - cd",
            datepublished: "Ngày phát hành - cd",
            store: "Sổ hồ sơ - cd",
            storeid: "Sổ văn bản - cd",
            inoutcode: "Số đến đi - cd",
            totalpage: "Số trang - cd",
            choosetotalpage: "Chọn số trang - cd",
            docfield: "Lĩnh vực - cd",
            keyword: "Từ khóa - cd",
            sendtype: "Hình thức gửi - cd",
            doccode1: "Mã hồ sơ - cd",
            citizenname: "Tên công dân - cd",
            address: "Địa chỉ - cd",
            phone: "Số điện thoại - cd",
            docpapers: "Giấy tờ - cd",
            identitycard: "Số CMT - cd",
            email: "Thư điện tử - cd",
            commune: "Xã phường - cd",
            attachmentlist: "File đính kèm - cd",
            relationlist: "Văn bản liên quan - cd",
            cbdetail: "Hiển thị chi tiết văn bản đến - cd",
            allcomment: "Nội dung xử lý - cd",
            titlecontent: "Nội dung văn bản - cd",
            urgent: {
                name: "Độ khẩn - cd",
                normal: "Thường - cd",
                fast: "Khẩn - cd",
                important: "Hỏa tốc"
            },
            securityid: {
                name: "Độ mật - cd",
                normal: "Thường - cd",
                high: "Cao - cd",
                important: "Tối mật - cd",
            },
            compendiumtitle: "Nhập trích yếu. - cd",
            nocomment: "Chưa cho ý kiến - cd",
            displayform: "Hiển thị biểu mẫu - cd",
            storeprivate: "Hồ sơ cá nhân - cd",
            storeshare: "Hồ sơ chia sẻ - cd",
            nextpage: "Trang tiếp - cd",
            prepage: "Trang trước - cd",
            currentpage: "Trang - cd",
            print: "In - cd",
            btnfinish: "Kết thúc - cd",
            viewicontraketqua: "Trả kết quả - cd",
            viewicontiepnhanbosung: "Tiếp nhận bổ sung - cd",
            viewiconhuyvanban: "Hủy - cd",
            viewiconluu: "Lưu sổ - cd",
            viewiconguiykien: "Gửi ý kiến - cd",
            viewiconthongbao: "Thông báo - cd",
            viewiconxinykien: "Xin ý kiến - cd",
            viewiconyeucaubosung: "Yêu cầu bổ sung - cd",
            viewicongiahanxuly: "Gia hạn - cd",
            no: "Từ chối - cd",
            yes: "Đồng ý - cd",
            btninsertrelation: "Văn bản liên quan... - cd",
            btninsertattachment: "Tệp đính kèm - cd",
            btninsertscan: "Tệp scan... - cd",
            btnpaper: "Giấy phép... - cd",
            btninsertanticipate: "Dự kiến chuyển... - cd",
            btntransfer: "Chuyển văn bản/hồ sơ - cd",
            btnedit: "Sửa nội dung văn bản/hồ sơ - cd",
            btninsertfile: "Đính kèm - cd",
            btnapproveryes: "Đông ý phê duyệt - cd",
            btnapproverno: "Từ chối phê duyệt - cd",
            btndestroy: "Hủy văn bản/hồ sơ - cd",
            viewiconketthuc: " - cd",
            btnfinishtt: "Kết thúc - cd",
            btnanswer: "Trả lời - cd",
            btnchangedoctype: "Phân loại - cd",
            concurrency: "Vnd - cd",
            usercomment: "Người xử lý - cd",
            filename: "Tên tệp - cd",
            filesize: "Kích thước - cd",
            fileversion: "Phiên bản - cd",
            lastupdatefile: "Cập nhật cuối - cd",
            finalcomment: "Ý kiến giải quyết - cd",
            backtolist: "Quay lại danh sách - cd",
            "delete": "Xóa - cd",
            mainprocess: "Xử lý chính: - cd",
            coprocess: "Đồng xử lý: - cd",
            sendto: "Chuyển tới - cd",
            thongbao: "Thông báo: - cd",
            xinykien: "Xin ý kiến: - cd",
            view: "Xem - cd",
            download: "Tải về - cd",
            placelabel: "Nơi nhận - cd",
            officename: "Tên cơ quan - cd",
            placeinoffice: "Nơi nhận trong đơn vị - cd",
            approvers: "Người ký - cd",
            datepublished: "Ngày ban hành - cd",
            docinpage: "S.bản / s.trang - cd",
            inplace: "Nơi lưu bản gốc - cd",
            publishreceive: "Danh sách nhận văn bản - cd",
            createdate: "Ngày khởi tạo - cd",
            createdate1: "Ngày tạo - cd",
            panelselectorrequire: "Bạn phải truyền tham số panelSelector - cd",
            publicoffice: "Cơ quan ban hành - cd",
            insertanticipate: "Dự kiến chuyển - cd",
            commoncomment: "Ý kiến thường dùng - cd",
            receivedays: "Số ngày thụ lý - cd",
            requirereport: "Yêu cầu hồi báo - cd",
            fees: "Lệ phí - cd",
            documentrelation: "Văn bản liên quan - cd",
            attachment: "Tệp đính kèm - cd",
        },
        form: {
            formgroup: "Nhóm mẫu: - cd",
            formtype: "Loại mẫu:"
        },
        code: {
            name: "Nhảy số - cd",
            config1: "Lấy ngày hiện tại, nếu nhỏ hơn 10 thì thêm số 0 đằng trước - cd",
            config2: "Lấy ngày hiện tại - cd",
            config3: "Lấy tháng hiện tại, nếu nhỏ hơn 10 thì thêm số 0 đằng trước - cd",
            config4: "Lấy tháng hiện tại - cd",
            config5: "Lấy năm hiện tại - cd",
            config6: "Lấy 2 số cuối của năm hiện tại - cd",

        },
        catalog: {
            addbewobject: "Thêm đối tượng - cd",
        }

    });
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