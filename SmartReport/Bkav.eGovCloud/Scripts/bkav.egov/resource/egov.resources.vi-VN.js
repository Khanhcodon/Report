
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
    //#region Version 1.0 - Đã dịch không thêm resource mới vào đây

    egov.resources = {
        document: {
            Compendium: "Trích yếu",
            Comment: "Ý kiến xử lý",
            DocType: "Loại báo cáo",
            Category: "Hình thức",
            InOutPlace: "Đơn vị",
            DateAppointed: "Thời hạn XL",
            Organization: "Cơ quan gửi",
            DocCode: "Số/ký hiệu *",
            DocCode2: "Số hiệu *",
            DateArrived: "Ngày đến",
            DateResponse: "Hạn hồi báo",
            DatePublished: "Ngày ban hành",
            Docfield: "Lĩnh vực",
            StoreId: "Sổ báo cáo",
            InOutCode: "Số đến",
            TotalPage: "Số trang",
            ChooseTotalPage: "Chọn số trang",
            DocField: "Lĩnh vực",
            Keyword: "Từ khóa",
            SendType: "Hình thức gửi",
            DocCode1: "Mã hồ sơ",
            CitizenName: "Tên công dân",
            Address: "Địa chỉ",
            Phone: "Số điện thoại",
            DocPapers: "Giấy tờ thu",
            IdentityCard: "Số CMT",
            Email: "Thư điện tử",
            Commune: "Xã phường",
            AttachmentList: "File đính kèm",
            RelationList: "báo cáo liên quan",
            BusinessLicense: "Giấy phép đăng ký",
            cbDetail: "Hiển thị chi tiết báo cáo đến",
            AllComment: "Nội dung xử lý",
            titleContent: "Nội dung báo cáo",
            Urgent: {
                name: "Độ khẩn",
                normal: "Thường",
                fast: "Khẩn",
                important: "Hỏa tốc"
            },
            SecurityId: {
                name: "Độ mật",
                normal: "Thường",
                high: "Mật",
                important: "Tối mật",
                highest: "Tuyệt mật",
            },
            CompendiumTitle: "Nhập trích yếu.",
            NoComment: "Chưa cho ý kiến",
            DisplayForm: "Hiển thị biểu mẫu",
            StorePrivate: "Hồ sơ cá nhân",
            StoreShare: "Hồ sơ chia sẻ",
            Note: "Ghi chú",
            nextPage: "Trang tiếp",
            prePage: "Trang trước",
            currentPage: "Trang 1",
            btnFinish: "Kết thúc",
            viewIconTraKetQua: "Trả kết quả",
            viewIconTiepNhanBoSung: "Tiếp nhận bổ sung",
            viewIconHuyVanBan: "Hủy",
            viewIconLuu: "Lưu sổ",
            viewIconGuiykien: "Gửi ý kiến",
            viewIconThongbao: "Thông báo",
            viewIconXinykien: "Xin ý kiến",
            viewIconYeuCauBoSung: "Yêu cầu bổ sung",
            viewIconGiaHanXuLy: "Gia hạn",
            no: "Từ chối",
            yes: "Đồng ý",
            btnInsertRelation: "báo cáo liên quan...",
            btnInsertAttachment: "Tệp đính kèm",
            btnInsertScan: "Tệp scan...",
            btnPaper: "Giấy phép...",
            btnInsertAnticipate: "Dự kiến chuyển...",
            btnTransfer: "Chuyển báo cáo/hồ sơ",
            btnEdit: "Sửa nội dung báo cáo/hồ sơ",
            btnInsertFile: "Đính kèm",
            btnApproverYes: "Đông ý phê duyệt",
            btnApproverNo: "Từ chối phê duyệt",
            btnDestroy: "Hủy báo cáo/hồ sơ",
            viewIconKetthuc: "",
            btnFinishtt: "Kết thúc",
            btnAnswer: "Trả lời",
            btnChangeDoctype: "Phân loại",
            concurrency: "Vnd",
            UserComment: "Người xử lý",
            filename: "Tên tệp",
            filesize: "Kích thước",
            fileversion: "Phiên bản",
            lastUpdateFile: "Cập nhật cuối",
            FinalComment: "Ý kiến giải quyết",
            backtolist: "Quay lại danh sách",
            'delete': "Xóa",
            MainProcess: "Xử lý chính:",
            CoProcess: "Đồng xử lý:",
            sendTo: "Chuyển tới",
            thongbao: "Thông báo:",
            xinykien: "Xin ý kiến:",
            view: "Xem",
            download: "Tải về",
            PersonInfo: "Tên CD/DN:",
            toolbar: {
                noaction: "Không có hướng chuyển tiếp theo.",
                transferByDk: "Chuyển theo dự kiến",
                transferUserDk: "Chuyển người nhận dự kiến",
                controlName: {
                    transferDoc: {
                        name: "Chuyển",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách hướng chuyển"
                        },
                        item: {
                            cancel: {
                                name: "Không tìm thấy hướng chuyển tiếp theo"
                            },
                            transferplan: {
                                name: "Chuyển theo dự kiến"
                            },
                            newtransferplan: {
                                name: "Chuyển người nhận dự kiến"
                            }
                        }
                    },
                    edit: {
                        name: "Sửa"
                    },
                    insert: {
                        name: "Chèn",
                        message: {
                            error: "Có lỗi xảy ra"
                        }
                    },
                    reload: {
                        name: "Tải lại"
                    },
                    approverYes: {
                        name: "Đồng ý"
                    },
                    approverNo: {
                        name: "Từ chối"
                    },
                    remove: {
                        name: "Hủy"
                    },
                    tiepNhanBoSung: {
                        name: "Yêu cầu bổ sung"
                    },
                    'return': {
                        name: "Trả kết quả"
                    },
                    finish: {
                        name: "Kết thúc"
                    },
                    traloi: {
                        name: "Trả lời",
                        hoso: "Hồ sơ mới",
                        document: "báo cáo mới",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách phân loại"
                        }
                    },
                    phanloai: {
                        name: "Phân loại",
                        callBackTitle: "Chọn loại báo cáo/hồ sơ",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách phân loại"
                        }
                    },
                    print: {
                        name: "In",
                        message: {
                            error: "Có lỗi xảy ra khi tải danh sách in!."
                        }
                    },
                    giahan: {
                        name: "In"
                    },
                    xinykien: {
                        name: "Xin ý kiến"
                    },
                    thongbao: {
                        name: "Thông báo"
                    },
                    guiykien: {
                        name: "Gửi ý kiến"
                    },
                    savePrivateStore: {
                        name: "Lưu sổ"
                    },
                    others: {
                        name: "Khác"
                    }
                }
            },
            content: {
                version: "Xem phiên bản của {0} cập nhật lúc {1}"
            },
            relation: {
                titleDialog: "Thêm báo cáo liên quan",
                confirmRelationTitle: "Xác nhận gửi kèm báo cáo liên quan",
                ignoreConfirm: "Luôn gửi, không hiển thị lại thông báo này lần sau.",
                contextmenu: {
                    open: "Mở báo cáo",
                    'delete': "Xóa báo cáo"
                },
                documentNotExist: "báo cáo không tồn tại!"
            },
            attachment: {
                uploading: "Đang tải tệp lên",
                uploadSuccess: "Tải tệp lên thành công!.",
                uploadError: "Có lỗi xảy ra khi tải tệp lên",
                fileChanged: "<strong>Tệp {0} có sự thay đổi</strong><br/>Bạn có muốn lưu lại khô…",
                errorDownload: "Có lỗi xảy ra khi tải tệp.",
                openFile: "Mở",
                deleteFile: "Xóa",
                restoreFile: "Phục hồi tệp đã xóa",
                download: "Tải về",
                removed: "(Đã loại bỏ)",
                using: "Đang sử dụng",
                version: "Phiên bản {0} cập nhật lúc {1}",
                closeProgramBeforeSave: "Bạn phải đóng các chương trình đang mở tệp đính kèm trước khi lưu.",
                fileIsRemoved: "Tệp đã bị xóa",
                existFile: "Trùng tên tập đính kèm",
                replaceOrNo: "Bạn có muốn lưu phiên bản mới cho tệp <span style='color: #7A3807;'…",
                'new': "mới",
                notEqualName: "Tập tải lên không khớp với tập tin hiện tại",
                confirmToUploadWithOtherName: "Tập tin <span style='color: #7A3807;'>'{0}'</span> tải lên không ph…"
            },
            transfer: {
                transferButton: "Chuyển",
                dialogTitle: "Bàn giao báo cáo",
                noUser: "Chưa chọn người nhận xử lý",
                transferSuccess: "Chuyển báo cáo thành công.",
                transferError: "Có lỗi trong quá trình bàn giao.",
                noUserByAction: "Hướng chuyển không có người nhận",
                sendAll: "Tất cả mọi người",
                answerSuccess: "Trả lời thành công.",
                answerFail: "Có lỗi trong quá trình trả lời ý kiến.",
                showDgTitle: "Hiển thị giao diện chọn cán bộ khác",
                noXlc: "Chưa chọn cán bộ xử lý",
                userList: "Danh sách nhận báo cáo",
                quicktransfer: "Chuyển nhanh",
                detail: "Chi tiết",
                'extends': "Nâng cao"
            },
            publishment: {
                dialogTitle: "Phát hành báo cáo",
                privateDialogTitle: "Lưu sổ phát hành nội bộ",
                publishButton: "Lưu và Phát hành",
                noAddressSelected: "Bạn chưa chọn đơn vị nhận báo cáo.",
                success: "Phát hành báo cáo thành công.",
                error: "Có lỗi xảy ra khi phát hành. Vui lòng thử lại.",
                addpublishment: "Thêm dự kiến phát hành"
            },
            ChangeDoctype: {
                hasChangeDateAppoint: "báo cáo/hồ sơ đã được phân loại theo loại hồ sơ mới.</br>Bạn có muố…",
                success: "báo cáo/hồ sơ đã được chuyển sang loại báo cáo {0}."
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
                error: "Không kết thúc được báo cáo, vui lòng thử lại.",
                success: "Kết thúc báo cáo thành công",
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
                noAdditional: "Dân không tới bổ sung",
                addPaper: "Thêm giấy tờ",
                addFee: "Thêm lệ phí",
                newDateAppointed: "Tính lại ngày hẹn trả",
                addDay: "Số ngày ",
                dateAppointed: "Hẹn trả: ",
                supplementType: {
                    renew: "Tính lại từ đầu",
                    'continue': "Tiếp tục tính",
                    add: "Cộng thêm ngày"
                },
                success: "Đã bổ sung",
                updateAndPrintButton: "Cập nhật và In biên nhận",
                name: "Yêu cầu bổ sung",
                undel: "Khôi phục",
                del: "Hủy",
                receivedRequire: "Yêu cầu bổ sung lần ",
                printTemplate: "In biên nhận",
                print: "In biên nhận",
                comment: "Ý kiến bổ sung",
                receivedTitle: "Tiếp nhận bổ sung",
                unsuccess: "Chưa bổ sung",
                cancelReveiced: "Hủy bổ sung",
                noComment: "Chú ý: xóa trắng ô nhập liệu này để hủy yêu cầu bổ sung của chính m…",
                removeRequired: "Hủy yêu cầu bổ sung này",
                add: "Thêm yêu cầu bổ sung",
                defaultComment: "Yêu cầu ông/bà bổ sung các loại giấy tờ còn thiếu",
                sendSuplementRequire: "Gửi yêu cầu bổ sung",
                expireDate: "Hạn bổ sung (ngày)"
            },
            print: {
                text: "In",
                quickPrint: "In nhanh",
                success: "In thành công ở: ",
                error: "Có lỗi xảy ra khi in",
                inDoc: "Khổ dọc",
                inNgang: "Khổ ngang",
                printer: "Chọn máy in",
                copies: "Số bản in",
                landscape: "Bố cục",
                isNotCreated: "Phiếu tiếp nhận chỉ in được khi tiếp nhận báo cáo",
                processing: "Đang in"
            },
            renewals: {
                renewalsButton: "Gia hạn",
                renewalsAndPrintButton: "Gia hạn và In phiếu",
                dialogTitle: "Gia hạn xử lý",
                renewalsReason: "Lý do gia hạn",
                newDateAppoint: "Hạn xử lý mới",
                printTemplate: "Mẫu in",
                noPrintTemplate: "Không có mẫu in gia hạn",
                renewalsType: "Chọn hình thức gia hạn",
                renewalsStaffOverdue: "Gia hạn xử lý cá nhân",
                renewalsDocOverdue: "Gia hạn hẹn trả hồ sơ",
                error: "Gia hạn không thành công, vui lòng thử lại!"
            },
            updateLastResult: {
                ok: "Duyệt",
                deny: "Không duyệt",
                comment: "Ý kiến xử lý:",
                code: "Số đến:",
                datePublish: "Ngày phát hành:",
                dialogTitle: "Cập nhật kết quả xử lý"
            },
            returnResult: {
                dialogTitle: "Trả kết quả",
                updateAndPrintButton: "In và trả kết quả",
                updateButton: "Trả kết quả",
                needToUpdateSupplementary: "Hồ sơ đang có yêu cầu bổ sung, Bạn cần cập nhật kết quả bổ sung trư…",
                needToUpdateLastResult: "Hồ sơ chưa có kết quả xử lý cuối cùng, Bạn cần cập nhật kết quả xử …",
                resultOk: "Đồng ý",
                resultDeny: "Từ chối",
                result: "Kết quả xử lý: ",
                personGive: "Thông tin công dân nhận kết quả",
                finish: " Kết thúc xử lý hồ sơ",
                printTemplate: "Mẫu in",
                finishAfterReturn: "Kết thúc xử lý hồ sơ sau khi trả kết quả."
            },
            documentOnline: {
                acceptSuccess: "Tiếp nhận báo cáo đăng ký qua mạng thành công",
                checkCitizenInfo: "Kiểm tra thông tin công dân",
                noData: "Không có dữ liệu liên quan",
                getExistDocumentError: "Có lỗi trong quá trình tìm kiếm dữ liệu",
                checkRefDocument: "Xem hồ sơ liên quan",
                insertRelations: "Thêm báo cáo liên quan",
                insertRejectComment: "Nhập lý do từ chối tiếp nhận hồ sơ"
            },
            confirmDestroy: "Bạn có chắc muốn hủy báo cáo này?",
            xlcLabel: "Xử lý chính: ",
            dxlLabel: "Đồng gửi: ",
            xykLabel: "Xin ý kiến: ",
            gsLabel: "Giám sát: ",
            thongbaoLabel: "Thông báo: ",
            errorLoadPrivateStore: "Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân",
            saveSuccess: "Lưu hồ sơ thành công",
            ignoreConfirmRelation: "Không hỏi lại",
            ignoreConfirmRelationWarning: "Có thể chỉnh lại config này bằng cách vào Thiết lập->Cấu hình khác-…",
            checkAll: "Chọn tất cả",
            displayAllComment: "Xem các ý kiến khác...",
            displayOnly3Coment: "Ẩn bớt ý kiến xử lý",
            addAnticipate: "Thêm dự kiến",
            require: "Yêu cầu",
            hasSpellError: "Phát hiện lỗi chính tả. Chọn \"Có\" nếu muốn tiếp tục, chọn \"Không…",
            errorSpell: {
                add: "Thêm vào thư viện chính tả",
                addSuccess: "Thêm thành công",
                addError: "Có lỗi xảy ra"
            },
            notpermission: "Bạn không có quyền xem báo cáo này!",
            openError: "Không mở được báo cáo liên quan",
            configError: "Cấu hình không đúng, vui lòng thử lại",
            saveViolateSuccess: "Ghi nhận CBLC thành công",
            table: {
                stt: "STT",
                creater: "Người soạn thảo",
                datecreate: "Ngày tạo",
                exprisedate: "Ngày hết hạn",
                lastcomment: "Ý kiến xử lý cuối",
                docCode: "Số ký hiệu",
                dateRecieved: "Ngày nhận",
                idCard: "Số CMND",
                citizenName: "Tên cơ quan/doanh nghiệp",
                Phone: "Điện thoại",
                Email: "Email",
                address: "Địa chỉ",
                relationDocsNumber: "{0} báo cáo liên quan"
            },
            transferError: "Công văn có lỗi khi chuyển",
            addtime: {
                numberonly: "Thời gian gia hạn phải là số."
            },
            report: {
                exprort: "Xuất ra file",
                group: "Nhóm"
            },
            anticipate: {
                name: "Dự kiến chuyển",
                receive: "Hướng nhận",
                choosereceive: "Chọn hướng nhận",
                receiver: "Người nhận",
                choosereceiver: "Chọn người nhận",
                anticipate: "Hướng dự kiến",
                chooseanticipate: "Chọn hướng dự kiến"
            },
            PlaceLabel: "Nơi nhận",
            PublishMail: "Email nhận",
            OfficeName: "Tên cơ quan",
            PlaceInOffice: "Nơi nhận trong đơn vị",
            Approvers: "Người ký",
            DocInPage: "S.bản / s.trang",
            InPlace: "Nơi lưu bản gốc",
            publishReceive: "Danh sách nhận báo cáo",
            UserNameReturned: "Người trả kết quả",
            DateReturned: "Ngày kết thúc xử lý",
            DateSuccess: "Ngày ký duyệt",
            DateReceived: "Ngày tiếp nhận",
            contextmenu: {
                copyText: "Copy",
                selectAll: "Chọn tất cả"
            },
            documentInfo: "Thông tin hồ sơ",
            citizenInfo: "Thông tin công dân",
            noCitizenIdCardNumber: "Chưa có CMT",
            noCitizenFullName: "Chưa có họ tên",
            noCitizenEmail: "Chưa có Email",
            addcomment: "Thêm ý kiến",
            Content: "Nội dung",
            Original: "Nguồn đơn",
            hasauthentication: "Thẩm quyền",
            iscomplain: "Phân loại đơn",
            publicResult: {
                titleDialog: "Cập nhật kết quả giải quyết khiếu nại, tố cáo",
                updateButton: "Xác nhận",
                finish: "Kết thúc xử lý hồ sơ",
                finalResult: "Kết quả giải quyết",
                dateAppoint: "Ngày hẹn tiếp",
                isAllowPublic: "Cho phép công bố kết quả",
                hasresult: "Đã cập nhật kết quả xử lý lần cuối ngày:"
            },
            dateCreated: "Ngày tiếp nhận",
            changeDateCreated: "Thay đổi ngày tiếp nhận",
            changeDateCreatedDialog: {
                title: "Thay đổi ngày tiếp nhận",
                submitBtn: "Thay đổi",
                closeBtn: "Bỏ qua",
                delayReason: "Lý do thay đổi",
                dateCreated: "Ngày tiếp nhận",
                applyAll: "Lưu tại cho lần tiếp nhận tiếp theo(khi nhấn hướng chuyển Tiếp nhận…"
            },
            docPaper: {
                receivedCount: "giấy tờ",
                viewPapers: "xem"
            },
            docFee: {
                viewFees: "xem",
                title: "Lệ phí"
            },
            changeWorkflowTypesDialog: {
                title: "Thay đổi loại hồ sơ",
                submitBtn: "Thay đổi",
                closeBtn: "Bỏ qua",
                stt: "STT",
                name: "Loại hồ sơ",
                expireProcess: "Hạn giữ",
                day: "Ngày",
                select: "Chọn"
            },
            documentOnlineStatus: {
                label: "Trạng thái",
                choDuyet: "Chờ duyệt",
                dangXuLy: "Đang xử lý",
                choBoSung: "Chờ bổ sung",
                choThanhToan: "Chờ thanh toán",
                choTraKetQua: "Chờ trả kết quả",
                daTraKetQua: "Đã trả kết quả",
                biTuChoi: "Bị từ chối"
            },
            delayReason: "Lý do muộn",
            deleteDelayReason: "Xóa",
            approver: {
                label: "Ký duyệt",
                accept: "Đồng ý",
                denied: "Từ chối",
                delApprover: "Xóa"
            },
            lienthong: {
                dialogTitle: "Liên thông báo cáo",
                sendButton: "Gửi liên thông",
                noAddressChoised: "Chưa chọn cơ quan nhận",
                sendSuccess: "Gửi báo cáo liên thông thành công",
                sendFail: "Gửi báo cáo liên thông lỗi, vui lòng thử lại sau."
            },
            changeWorkflowType: "Thay đổi hạn xử lý theo loại",
            addCommonComment: "Thêm ý kiến thường dùng",
            selectCommonComment: "Chọn từ mẫu",
            create: {
                unpin: "Bỏ gắn",
                pin: "Gắn lên trên đầu"
            },
            hasReceived: "Hồ sơ đã tiếp nhận",
            isWaitting: "Hồ sơ đang đăng ký",
            appoint: {
                titleDialog: "Tạo lịch tiếp công dân",
                updateButton: "Cập nhật",
                dateAppoint: "Ngày hẹn tiếp",
                appointExist: "Đã có lịch hẹn",
                remind: "Ghi chú/Nhắc nhở",
                createAppointSuccess: "Đặt lịch thành công",
                updateAppointSuccess: "Đặt lại lịch thành công",
                number: "Lần gặp thứ"
            },
            validateEmail: "Mail không đúng định dạng.",
            validatePhone: "Số điện thoại không đúng định dạng.",
            notExist: "báo cáo/hồ sơ không tồn tại. Vui lòng xem lại.",
            deleteDocPaperError: "Có lỗi xảy ra khi xóa giấy tờ của hồ sơ, vui lòng thử lại."
        },
        documentQuickView: {
            belowDocumentSum: "Tóm tắt thông tin báo cáo",
            Comment: "Ý kiến xử lý:",
            timeComment: "lúc",
            Category: "Loại báo cáo:",
            Docfield: "Lĩnh vực:",
            DocCode: "Số kí hiệu:",
            Result: "Kết quả xử lý",
            LastUserComment: "Người xử lý cuối:",
            Place: "Nơi nhận báo cáo:",
            Sign: "Người ký:",
            TotalPage: "Số trang:",
            noDocumentSelected: "Chọn báo cáo để hiển thị thông tin tóm tắt ở đây."
        },
        transfer: {
            ChoseOtherUser: "Chọn cán bộ nhận khác",
            MainProcessUser: "Nhận bản chính",
            MainProcessUserComment: "(xử lý chính)",
            CoProcessUser: "Nhận bản sao",
            CoProcessUserComment: "(phối hợp xử lý)",
            AnnouceUser: "Nhận thông báo",
            AnnouceUserComment: "(để xem)",
            GiamsatUser: "Giám sát",
            QuickSearchAccount: "Tìm nhanh tài khoản của hướng chuyển",
            AnnouncementPlace: "Nơi nhận thông báo",
            PrivateAnoun: "Nhận thông báo",
            ConsultContent: "Nội dung xin ý kiến",
            ConsultUser: "Người xin ý kiến",
            MainProcess: "Xử lý chính",
            CoProcess: "Đồng xử lý",
            dgUserLabel: "(Chọn cá nhân, đơn vị nhận bản sao)",
            dgUser: "Cá nhân, đơn vị nhận bản sao",
            dgJobtitleLabel: "(Chọn chức vụ và phòng ban nhận bản sao)",
            dgJobtitle: "Chức vụ",
            dgDeptJob: "Phòng ban",
            dgUserGiamsat: "(Chọn cán bộ giám sát)",
            allJobs: "Tất cả chức vụ",
            sameDept: "Cùng đơn vị",
            isDg1: "Thông báo",
            isDg2: "Đồng gửi",
            searchDgLabel: "Cá nhân nhận bản sao",
            allJobTitlesForDept: "Tất cả các chức danh",
            jobtitlesDeptPopup: "Chức danh thuộc phòng ban(đơn vị)",
            jobtitleForAll: "Cấp nhận bản sao(để biết)",
            allJobTitles: "Tất cả các chức danh",
            IsThongbao: "Thông báo|",
            IsDxl: "Đồng xử lý |",
            IsAttachYk: "Gửi kèm ý kiến giải quyết",
            TransferDocument: "Bàn giao báo cáo",
            userList: "Danh sách nhận báo cáo",
            transferButton: "Chuyển",
            dialogTitle: "Bàn giao báo cáo",
            noUser: "Chưa chọn người nhận xử lý",
            transferSuccess: "Chuyển báo cáo thành công.",
            transferError: "Có lỗi trong quá trình bàn giao.",
            noUserByAction: "Hướng chuyển không có người nhận",
            sendAll: "Tất cả mọi người",
            answerSuccess: "Trả lời thành công.",
            answerFail: "Có lỗi trong quá trình trả lời ý kiến.",
            showDgTitle: "Hiển thị giao diện chọn cán bộ khác",
            noXlc: "Chưa chọn cán bộ xử lý",
            hsmsNoXlc: "Trên hồ sơ một cửa phải có người xử lý chính. Vui lòng xem lại",
            HasNoneDocument: "Bạn chưa chọn báo cáo!",
            messageNoBtn: "Không",
            messageCancelBtn: "Bỏ qua",
            messageOkBtn: "Đồng ý",
            dgUserLabelM: "Cá nhân, đơn vị nhận bản sao",
            dgJobtitleLabelM: "Chức vụ nhận bản sao",
            dgDeptLabelM: "Phòng ban nhận bản sao",
            dgUserGiamsatM: "Cán bộ giám sát"
        },
        attachment: {
            view: "Xem",
            open: "Sửa",
            del: "Xóa",
            download: "Tải về",
            notPermision: "Bạn không có quyền thực hiện thao tác này",
            downloadAll: "Tải tất cả"
        },
        storePrivate: {
            attachmentName: "Tài liệu:",
            descStorePrivateAttachment: "Mô tả:",
            storePrivateName: "Tên hồ sơ:",
            storePrivateNameWarning: "Nhập tên hồ sơ",
            userJoined: "Người tham gia:",
            delJoined: "Xóa",
            descStorePrivate: "Ghi chú:"
        },
        relation: {
            open: "Mở",
            del: "Xóa",
            view: "Xem chi tiết"
        },
        toolbar: {
            XMLAttachment: "Đính kèm file XML",
            codeManager: "Quản lý mã",
            DuKienPhatHanh: "Dự kiến phát hành",
            transferBtn: "Chuyển",
            editBtn: "Sửa",
            attachBtn: "Đính kèm",
            relation: "báo cáo liên quan...",
            attachment: "Tệp đính kèm",
            scan: "Tệp scan...",
            packet: "Xử lý theo lô",
            imagePacket: "Chèn ảnh theo lô",
            paper: "Giấy phép...",
            DuKienChuyen: "Dự kiến chuyển...",
            reloadBtn: "Tải lại",
            allow: "Đồng ý",
            deny: "Từ chối",
            destroy: "Hủy",
            TiepNhanBoSung: "Tiếp nhận bổ sung",
            TraKetQua: "Trả kết quả",
            CapNhatKetQua: "Cập nhật kết quả",
            finish: "Kết thúc",
            reply: "Trả lời",
            PhanLoai: "Phân loại",
            print: "In",
            other: "Khác",
            GiaHan: "Gia hạn",
            YeuCauBoSung: "Yêu cầu bổ sung",
            XinYKien: "Xin ý kiến...",
            btnAnnouncement: "Thông báo...",
            btnSendAnswer: "Gửi ý kiến...",
            btnSaveStore: "Lưu sổ..",
            sendMail: "Gửi mail",
            sendSms: "Gửi tin nhắn",
            accept: "Tiếp nhận",
            reject: "Từ chối",
            additionalRequirements: "Yêu cầu bổ sung",
            checkCitizenInfo: "Kiểm tra thông tin công dân",
            addnewtemplate: "Thêm mẫu mới",
            btnSaveDraft: "Lưu nháp",
            btnSave: "Cập nhật",
            confirmTransferOrProcess: "Xác nhận bàn giao",
            btnEditDocInfo: "Sửa thông tin",
            appoint: "Hẹn tiếp",
            pdfPacket: "Chèn pdf theo lô",
            btnUndoFinish: "Lấy lại báo cáo",
            btnRePublish: "Phát hành tiếp",
            invoice: "Biên lai",
            importinvoice: "Thêm mới biên lai"
        },
        main: {
            gtv: "Kiểu gõ",
            notifications: "Thông báo",
            news: "Tin điều hành",
            newEmail: "Soạn thư",
            config: "Thiết lập",
            reply: "Gửi phản hồi",
            smallSize: "Xem cỡ nhỏ",
            mediumSize: "Xem cỡ vừa",
            largeSize: "Xem cỡ lớn",
            underPreview: "Xem trước bên dưới",
            rightPreview: "Xem trước bên phải",
            hidePreview: "Ẩn xem trước",
            reload: "Khởi động lại",
            logout: "Đăng xuất",
            searchDocument: "Tìm kiếm thông tin hồ sơ, báo cáo, tệp đính kèm",
            searchFile: "Tìm kiếm file đính kèm",
            reloadMessage: "Một số thiết lập yêu câu phải reload lại hệ thống. Bạn có muốn relo…",
            closeBtn: "Đóng",
            submitBtn: "Cập nhật",
            titleMessage: "Thông báo!",
            closeAll: "Đóng tất cả lại",
            report: "Báo cáo thống kê",
            contacts: "Sổ liên lạc",
            calendar: "Lịch",
            chat: "Chat",
            documents: "Xử lý báo cáo",
            bmail: "Tin điều hành",
            placeholderSearch: "Tìm kiếm thông tin hồ sơ, báo cáo, tệp đính kèm",
            links: "Liên kết",
            administrator: "Quản trị hệ thống",
            messageNoBtn: "Không",
            emptyMailNotifications: "Bạn không có thông báo mail nào",
            openAllMail: "Mở tất cả mail nhận được",
            emptyChatNotifications: "Bạn không có tin nhắn nào",
            openAllChat: "Mở tất cả tin nhắn nhận được",
            emptyDocumentNotifications: "Bạn không có thông báo báo cáo nào!",
            openAllDocument: "Mở tất cả báo cáo được thông báo",
            haveNewDocument: "Bạn có báo cáo mới",
            haveNewMail: "Bạn có thư mới",
            haveNewChat: "Bạn có tin nhắn mới",
            downloaddesktopversion: "Tải bản desktop",
            conversion: "Hội thoại",
            notJqueryAlert: "Chưa có file jquery. Vui lòng tải thêm file jquery!",
            lblDocument: "báo cáo",
            lblNewConversion: "Hội thoại",
            lblNewWorkTime: "Tạo lịch",
            lblNewMail: "Soạn thư",
            searchMail: "Tìm kiếm mail",
            youHave: "Bạn có",
            unreadDocuments: "báo cáo chưa xem",
            installPlugin: {
                message: "Bạn cần cài đặt eGov Plugin để sử dụng đầy đủ các chức năng của eGo…",
                link: "Tải Plugin.",
                reDownload: "Tải lại."
            }
        },
        index: {
            storePrivate: "Hồ sơ công việc",
            plugin: "Ứng dụng",
            reportNode: "Báo cáo thống kê",
            printNode: "In nhanh",
            reload: "Đồng bộ"
        },
        setting: {
            title: "Thiết lập cá nhân",
            ProfileConfig: "Thông tin cá nhân",
            EnterCode: "Nhập mã xác thực",
            Changepassword: "Đổi mật khẩu",
            UserSetting: "Cấu hình phím tắt",
            GeneralSettings: "Cấu hình khác",
            NotifySettings: "Trung tâm thông báo",
            SignatureSetting: "Cấu hình chữ ký",
            btnUpdateSetting: "Cập nhật",
            btnCloseSetting: "Đóng",
            AuthorizesSetting: "Cấu hình ủy quyền",
            notify: {
                documentNotify: "Thông báo báo cáo",
                BmailNotifyType: "Thông báo thư điện tử",
                chat: "Thông báo hội thoại",
                mail: {
                    MailFolderNotify: "Danh sách các thư mục được nhận thông báo",
                },
                mobileconfig: "Cấu hình cho app eGov",
            },
            signature: {
                titleCreate: "Thêm mới chữ ký",
                titleEdit: "Cập nhật chữ ký",
                configPossition: "Cấu hình vị trí đặt chữ ký",
                configOther: "Cấu hình khác",
                deleteMessage: "Bạn có chắc muốn xóa cấu hình này",
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
                        'delete': "Xóa"
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
                    }
                }
            },
            authorize: {
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
                        'delete': "Xóa"
                    },
                    body: {
                        noData: "Không có dữ liệu"
                    }
                }
            },
            general: {
                page: "Phần trang",
                scrollLoadData: "Cuộn chuột để tải dữ liệu",
                pagingLoadData: "Phân trang tải dữ liệu",
                showDetailDocument: "Hiển thị chi tiết báo cáo",
                showQuickView: "Hiển thị tóm tắt báo cáo",
                finishdocument: "Thiết lập chung xử lý hồ sơ, báo cáo",
                setting: "Cấu hình Page trang chủ",
                nofifysetting: "Cấu hình notify",
                displayAccount: "Hiển thị tên người dùng",
                loadpagescroll: "Cuộn trang",
                loadpagesize: "Phân trang",
                language: "Ngôn ngữ: ",
                useVietNameseTyping: "Sử dụng bộ gõ Tiếng Việt",
                isFullQuickView: "Thiết lập chế độ xem tóm tắt báo cáo/hồ sơ",
                IsSaveOpenTab: "Cho phép mở lại hồ sơ, báo cáo khi load lại trang",
                HasHideLuuSo: "Cho phép bỏ qua lưu sổ khi kết thúc báo cáo",
                DisplayPopupTransferTheoLo: "Hiển thị popup cho ý kiến khi bàn giao báo cáo theo lô",
                ViewDocInPopUp: "Xem báo cáo ở cửa sổ mới",
                IgnoreConfirmRelation: "Luôn gửi tất cả báo cáo đính kèm",
                HasPopupChat: "Chat bằng cửa sổ popup (dạng phóng to).",
                DocumentNotifyType: "Thông báo báo cáo",
                QuickView: "Vị trí hiển thị tóm tắt báo cáo",
                MudimMethod: "Kiểu gõ",
                FontSize: "Cấu hình hiển thị cỡ chữ",
                DefaultPageSizeHome: "Số bản ghi trên 1 trang mặc định",
                ListPageSizeHome: "Danh sách phân trang",
                PrinterName: "Máy in",
                Language: "Ngôn ngữ",
                TypeChucVuChucDanh: "Hiển thị theo chức vụ hoặc chức danh trong phát hành báo cáo"
            },
            profile: {
                avatar: "Ảnh đại diện",
                choseAvatar: "Chọn",
                male: "Nam",
                female: "Nữ",
                lastname: "Họ và tên đệm *",
                firstname: "Tên *",
                gender: "Giới tính *",
                phone: "Số điện thoại",
                fax: "Fax",
                address: "Địa chỉ",
                entercode: "Nhập mã"
            },
            login: {
                account: "Tài khoản:",
                password: "Mật khẩu:",
                keepingLogin: "Duy trì đăng nhập!",
                loginType: "Hình thức đăng nhập",
                forgetPassword: "Quên mật khẩu",
                choseServicer: "Hãy chọn 1 nhà cung cấp dịch vụ OpenID:",
                loading: "Đang xử lý...",
                btnLogin: "Đăng nhập",
                title: "ĐĂNG NHẬP",
                username: "Tên đăng nhập",
                capslockison: "Capslock đang bật",
                entercaptcha: "Đăng nhập sai quá số lần cho phép, hãy chứng minh bạn không phải ro…"
            },
            usersetting: {
                document: "báo cáo, hồ sơ",
                shortkey: "Phím tắt",
                documentdefaultname: "Tên báo cáo, hồ sơ mặc định",
                supportkey: "Phím hỗ trợ",
                fnname: "Tên chức năng",
                generalconfig: "Cấu hình chung",
                selectdocument: "Chọn báo cáo, hồ sơ"
            },
            sendemailto: "Gửi email kiểm tra tới ",
            sendemailsuccess: "thành công!",
            sendemailfailure: "không thành công!",
            smtpsetting: "Cấu hình máy chủ SMTP",
            othersetting: "Cấu hình khác",
            location: {
                addlocation: "Thêm vị trí",
                editlocation: "Sửa vị trí",
                confirmdeletefilelocation: "Bạn chắc chắn muốn xóa vị trí lưu file này chứ?",
                canotdelete: "",
                listfilelocation: "Danh sác các nơi lưu file",
                nodata: "Chưa có cấu hình vị trí lưu file"
            },
            passwordpolicy: {
                checkpassword: "Kiểm tra cú pháp mật khẩu",
                lookaccount: "Khóa tài khoản",
                passworddeadtime: "Hết hạn mật khẩu",
                passwordchangehistory: "Lịch sử thay đổi mật khẩu",
                defaultpassword: "Mật khẩu mặc định",
                captchatext: "Sử dụng chữ: (Ví dụ: 'MADQES', 'JOMCOC', ...)",
                captchamath: "Sử dụng biểu thức toán học: (Ví dụ: '4 + 5 =', '64 - 12 =', ...)",
                captchanote: "(bỏ chọn sẽ dùng thời gian khóa)"
            },
            mail: {
                active: "Kích hoạt chế độ gửi mail"
            },
            changepassword: {
                currentpassword: "Mật khẩu hiện tại *",
                newpassword: "Mật khẩu mới *",
                confirmpassword: "Xác nhận mật khẩu"
            },
            kntc: {
                enable: "Kích hoạt"
            }
        },
        scan: {
            rotateLeft: "Quay trái",
            rotateRight: "Quay phải",
            zoomIn: "Phóng to",
            zoomOut: "Thu nhỏ",
            setActualSize: "Ảnh gốc",
            crop: "Cắt ảnh",
            setBrightnessUp: "Tăng độ sáng",
            setBrightnessDown: "Giảm độ sáng",
            setContrastUp: "Tăng độ tương phản",
            setContrastDown: "Giảm độ tương phản",
            addToContent: "Đưa vào nội dung",
            pagePosition: "Trang: 0/0",
            preImage: "Ảnh trước",
            nextImage: "Ảnh sau",
            removeImageScan: "Xóa",
            removeAllImageScan: "Xóa tất cả",
            listScannerLabel: "Chọn máy scan:",
            reloadListScanner: "Làm mới danh sách máy scan",
            pixeltype: "Kiểu màu",
            pixeltype2: "Màu",
            pixeltype0: "Màu xám",
            pixeltype1: "Màu đen trắng",
            resolution: "Độ phân giải",
            resolution75: "75 dpi",
            resolution100: "100 dpi",
            resolution150: "150 dpi",
            resolution200: "200 dpi",
            resolution300: "300 dpi",
            duplex: "Quét 2 mặt",
            showui: "Dùng giao diện của máy scan",
            filename: "Tên tệp",
            imageformatLabel: "Lưu tệp dạng",
            imageformatJPEG: "JPEG",
            imageformatPNG: "PNG",
            imageformatGIF: "GIF",
            imageformatTIFF: "TIFF",
            imageformatBMP: "BMP",
            imageformatPDF: "PDF",
            imageformatDOC: "DOC",
            acquire: "Quét ảnh",
            refresh: "Làm mới danh sách máy scan",
            SaveFileAs: "Lưu file dưới dạng",
            selectScanMachine: "Chọn máy Scan",
            FileName: "Tên file",
            removeAllImageScantt: "Xóa toàn bộ"
        },
        tab: {
            close: "Đóng tab",
            home: {
                title: "báo cáo"
            },
            report: {
                title: "Báo cáo thống kê"
            },
            print: {
                title: "In nhanh"
            },
            search: {
                title: "Tìm kiếm"
            },
            setting: {
                title: "Thiết lập"
            },
            saveDraft: "Bạn có muốn lưu nháp lại báo cáo này?",
            saveChange: "báo cáo có thay đổi, bạn có muốn lưu lại không?",
            newDocument: "báo cáo mới"
        },
        search: {
            compendium: "Trích yếu",
            doccode: "Số ký hiệu",
            inoutcode: "Số đến",
            content: "Nội dung",
            category: "Thể loại vb",
            keyword: "Từ khóa",
            urgent: "Độ khẩn",
            storeprivate: "Hồ sơ cá nhân",
            store: "Sổ báo cáo",
            categorybusiness: {
                name: "Nghiệp vụ",
                all: "Tất cả",
                in: "báo cáo đến",
                out: "báo cáo đi",
                one: "Hồ sơ một cửa"
            },
            InOutPlace: "Đơn vị xử lý",
            OrganizationCreate: "C/Q ban hành",
            DocField: "Lĩnh vực",
            UserSuccess: "Người ký",
            UserCreate: "Người khởi tạo",
            CurrentUser: "Người giữ",
            CurrentDepartment: "Phòng ban giữ",
            FromDateStr: "Ngày tạo",
            ToDateStr: "Đến ngày",
            FromPubDateStr: "Ngày ban hành",
            showsearch: "Tìm kiếm nâng cao",
            createdate: "Ngày khởi tạo",
            createdate1: "Ngày tạo",
            status: "Trạng thái",
            status1: "Đang dự thảo",
            status2: "Đang xử lý",
            status4: "Đã kết thúc",
            status8: "Đã hủy",
            status16: "Dừng xử lý",
            search: "Tìm kiếm",
            searchnew: "Tìm kiếm mới",
            order: "STT",
            searchnotfound: "Không tìm thấy kết quả phù hợp",
            view: "Xem",
            download: "Tải về",
            DidYouMean: "Có phải bạn muốn tìm",
            all: "Tất cả",
            doccodePh: "Nhập số ký hiệu",
            inoutcodePh: "Nhập số đến",
            contentPh: "Nhập nội dung",
            keywordPh: "Nhập từ khóa",
            error: "Có lỗi xảy ra khi tìm kiếm. Vui lòng liên hệ quản trị mạng.",
            noresult: "Không tìm thấy kết quả",
            Compendiumph: "Nhập trích yếu báo cáo",
            openattachmentfile: "Mở file đính kèm",
            downloadattachmentfile: "Tải file đính kèm"
        },
        common: {
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
            transfering: "Đang chuyển",
            currencyUnit: "Vnd",
            save: "Lưu",
            messageYesBtn: "Có",
            messageNoBtn: "Không",
            messageCancelBtn: "Bỏ qua",
            messageOkBtn: "Đồng ý",
            errorMessage: "Có lỗi xảy ra, vui lòng thử lại hoặc báo cho quản trị",
            saveBtn: "Lưu",
            cancelBtn: "Bỏ qua",
            view: "Xem",
            updating: "Đang cập nhật ...",
            showPreviewPrint: "Hiển thị xem trước khi In"
        },
        file: {
            lenghtIsNotAllow: "File tải lên quá dung lượng quy định.",
            typeIsNotAllow: "File không đúng định dạng quy định.",
            errorUpload: "Có lỗi xảy ra khi tải tệp lên.",
            errorDownload: "Có lỗi xảy ra khi tải tệp xuống.",
            maxLength: "Dung lượng tối đa: ",
            notAcceptFileTypes: "Loại tệp này không cho phép tải lên"
        },
        home: {
            syncDataError: "Có lỗi khi đồng bộ danh sách báo cáo",
            documentPreView: {
                tooltip: {
                    open: "Hiển thị tóm tăt báo cáo/hồ sơ",
                    close: "Ẩn tóm tăt báo cáo/hồ sơ"
                },
                control: {
                    close: "X",
                    open: "open"
                }
            },
            docType: {
                message: {
                    error: {
                        loading: "Không tải được danh sách loại báo cáo!"
                    }
                }
            }
        },
        treeDocument: {
            message: {
                confirm: {},
                success: {},
                error: {
                    syncData: "Lỗi khi đồng bộ dữ liệu!"
                }
            }
        },
        treeStore: {
            nameStorePrivateRoot: "Hồ sơ cá nhân",
            nameStoreShareRoot: "Hồ sơ chia sẻ",
            title: {
                createStore: "Tạo sổ hồ sơ",
                detailSotore: "Xem chi tiết",
                addStorePrivateAttachment: "Thêm tài liệu"
            },
            message: {
                confirm: {
                    openStore: "Bạn có chắc muốn mở hồ sơ này không?",
                    closeStore: "Bạn có chắc muốn đóng hồ sơ này không?",
                    deleteStore: "Bạn có chắc muốn xóa hồ sơ này không?"
                },
                success: {
                    openStore: "Mở hồ sơ thành công!",
                    closeStore: "Đóng hồ sơ thành công!",
                    deleteStore: "Xóa hồ sơ thành công!"
                },
                error: {
                    createStore: "Có lỗi trong quá trình tạo mới sổ hồ sơ",
                    updateStore: "Có lỗi trong quá trình cập nhật sổ hồ sơ",
                    selectStore: " Có lỗi xảy ra khi lấy dữ liệu",
                    openStore: "Có lỗi khi mở hồ sơ!",
                    closeStore: "Có lỗi khi đóng hồ sơ!",
                    deleteStore: "Có lỗi khi xóa hồ sơ!"
                }
            },
            contextmenu: {
                createStore: "Tạo mới hồ sơ",
                updateStore: "Cập nhật hồ sơ",
                deleteStore: "Xóa hồ sơ",
                openStore: "Mở hồ sơ",
                closeStore: "Đóng hồ sơ",
                addStorePrivateAttachment: "Thêm tài liệu",
                messageCloseStore: "Bạn có chắc muộn đóng hồ sơ này?.",
                messageOpenStore: "Bạn có chắc muốn mở hồ sơ này?."
            }
        },
        documents: {
            title: {
                documentImportant: "Bỏ gắn quan trọng báo cáo này",
                documentNotImportant: "Gắn quan trọng cho báo cáo này",
                vanBanDongXuLy: "báo cáo đồng xử lý",
                vanBanSapHetHan: "báo cáo sắp hết hạn (còn 1 ngày)",
                vanBanKhanHoacQuaHanXuLy: "báo cáo khẩn hoặc quá hạn xử lý",
                vanBanQuaHanHoiBao: "báo cáo quá hạn hồi báo",
                vanBanHoaToc: "báo cáo hỏa tốc",
                vanBanThuong: "báo cáo bình thường",
                documentDetail: "Chi tiết báo cáo/hồ sơ"
            },
            toolbar: {
                controlName: {
                    all: "Xem tất cả",
                    day: "ngày",
                    page: "Trang",
                    dateAppointed: "Ngày hết hạn",
                    docTypeName: "Loại hồ sơ",
                    documentImportant: "Xem báo cáo quan trọng",
                    documentUnread: "Xem báo cáo chưa đọc",
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
                    xemvanban: "Xem báo cáo...",
                    suavanban: "Sửa báo cáo...",
                    guiykien: "Gửi ý kiến...",
                    xinykien: "Xin ý kiến...",
                    bangiao: "Bàn giao...",
                    thongbao: "Thông báo...",
                    laylaivanban: "Lấy lại báo cáo",
                    xacnhanbangiao: "Xác nhận bàn giao...",
                    xacnhanxuly: "Xác nhận xử lý...",
                    yeucaubosung: "Yêu cầu bổ sung...",
                    tiepnhanbosung: "Tiếp nhận bổ sung...",
                    kyduyet: "Ký duyệt...",
                    ketthucxuly: "Kết thúc xử lý",
                    huyvanban: "Hủy báo cáo",
                    capnhatketquaxulycuoi: "Cập nhật kết quả xử lý cuối...",
                    inphieutrinh: "In phiếu trình lãnh đạo...",
                    intomtat: "In tóm tắt",
                    capnhattiendo: "Cập nhật tiến độ...",
                    xoakhoiduthao: "Xóa báo cáo dự thảo",
                    contextheodoi: "Fix contextmenu theo dõi",
                    dungxuly: "Dừng xử lý...",
                    giahanxuly: "Gia hạn xử lý...",
                    chitietvanban: "Chi tiết báo cáo/hồ sơ",
                    danhdaudadoc: "Đánh dấu đã đọc",
                    danhdauchuadoc: "Đánh dấu chưa đọc",
                    movanban: "Mở báo cáo",
                    exportToExcell: "Xuất danh sách ra tệp Excell",
                    exportToWord: "Xuất danh sách ra tệp Word",
                    removefromstoreprivate: "Xóa khỏi hồ sơ",
                },
                printTransferHistory: "In lịch sử bàn giao",

                reOpenDocument: {
                    text: "Mở lại hồ sơ",
                    success: "Mở lại thành công",
                    error: "Có lỗi xảy ra"
                },
                duplicateDocument: "Sao chép"
            },
            page: {
                text: "Trang",
                document: "báo cáo"
            },
            message: {
                error: {
                    quickView: "Lỗi khi lấy thông tin báo cáo!",
                    documentNotExist: "báo cáo không tồn tại!.",
                    documentNotSelectDelete: "Chưa chọn báo cáo để xóa!.",
                    pagging: "Có lỗi trong quá trình chuyển sang trang mới",
                    loadNewerDocuments: "Có lỗi trong qua trình tải dữ liệu!",
                    getDocumentDetail: "báo cáo không tồn tại"
                }
            },
            noDocumentCopyItem: "Không có báo cáo cần xử lý.",
            notFound: "Danh sách báo cáo hiện tại không có kết quả phù hợp. Nhấn Enter để …",
            documentNumberDayOverdue: "-{0} ngày",
            validDocuments: "Còn {0} ngày",
            validDocumentsInToday: "Hôm nay",
            validDocumentsInTodayMorning: "Sáng nay",
            validDocumentsInTodayAfternoon: "Chiều nay",
            validDocumentsTomorrow: "Ngày mai",
            validDocumentsAfterTomorrow: "Ngày kia",
            documentNumberWeekOverdue: "-{0} tuần",
            documentNumberMonthOverdue: "-{0} thg",
            documentNumberYearOverdue: "-{0} năm",
            unlimitedTime: "Vô hạn",
            multiselect: "{0}",
            print: {
                success: "In thành công",
                error: "Có lỗi xảy ra"
            },
            transfer: {
                notSelectedDocument: "Bạn chưa chọn báo cáo nào.",
                confirmTitle: "Cho ý kiến trước khi bàn giao.",
                confirmCheckName: "Không hiển thị lại lần sau.",
                primaryButtonName: "Tiếp tục",
                addTemplateButtonName: "Chọn từ mẫu",
                noAction: "Không có hướng chuyển."
            }
        },
        templateComment: {
            titleDialog: "Mẫu ý kiến thường dùng",
            btnAddTemplateComment: "Thêm mẫu",
            btnSelected: "Chọn",
            table: {
                header: {
                    content: "Nội dung",
                    edit: "Sửa",
                    'delete': "Xóa"
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
                'delete': "Xóa"
            }
        },
        requiredSupplementary: {
            addRequiredTitle: "Thêm mẫu yêu cầu bổ sung",
            noRequired: "Không có mẫu"
        },
        tree: {
            displayUnRead: "Có {0} báo cáo chưa đọc",
            displayUnReadOnAll: "{0} chưa đọc / tổng số {1} báo cáo",
            displayAll: "Có tất cả {0} báo cáo",
            question: {
                label: "Hỏi đáp",
                general: "Hỏi đáp chung",
                document: "Câu hỏi hồ sơ",
                QuestionName: "Tên câu hỏi",
                citizenname: "Tên công dân",
                date: "Ngày hỏi",
                doccode: "Mã hồ sơ"
            }
        },
        searching: {
            result: "Kết quả tìm kiếm"
        },
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
            timenotcheck: "Không kiểm tra được thời gian",
            checkdate: "Kiểm tra ngày",
            caculateextendtime: "Tính lịch nghỉ bù",
            nodata: "Không có ngày nghỉ nào",
            repeat: "Lặp lại",
            repeatbyyear: "Lặp theo năm",
            freeday: "Tên ngày nghỉ",
            AL: "Ngày âm",
            DL: "Ngày dương",
            day: "Thứ",
            listofrestday: "Danh sách ngày nghỉ năm",
            weekworktime: "Thời gian làm việc trong tuần",
            state: "Trạng thái",
            nghibu: "Nghỉ bù",
            nghile: "Nghỉ lễ",
            tatca: "Tất cả",
            worktime: "Giờ hành chính",
            listoffsetday: "Danh sách ngày làm bù trong năm",
            yesterday: "H.qua",
            minbefore: "{0} phút trước",
            justnow: "Vừa xong"
        },
        enumResource: {
            actionlevel: {
                levelone: "Mức độ 1",
                leveltwo: "Mức độ 2",
                levelthree: "Mức độ 3",
                levelfour: "Mức độ 5"
            },
            activitylogtype: {
                dangnhap: "Đăng nhập",
                dangxuat: "Đăng xuất",
                bangiao: "Bàn giao báo cáo",
                thongbao: "Thông báo báo cáo",
                huyvanban: "Hủy báo cáo",
                ketthucvanban: "Kết thúc báo cáo",
                phanloai: "Phân loại báo cáo",
                phathanh: "Phát hành báo cáo",
                kyduyet: "Ký duyệt báo cáo",
                xinykien: "Xin ý kiến",
                guiykien: "Gửi ý kiến",
                tiepnhan: "Tiếp nhận",
                xingiahan: "Xin gia hạn",
                chuyenykiendonggop: "Chuyển ý kiến đóng góp"
            },
            categorybusinesstypes: {
                vbden: "báo cáo đến",
                vbdi: "báo cáo đi",
                hsmc: "Hồ sơ một cửa"
            },
            dailyprocesstimeforsearch: {
                allday: "Cả ngày",
                thirtyminutes: "30 phút trước",
                onehour: "1 tiếng trước",
                twohour: "2 tiếng trước",
                am: "Buổi sáng",
                pm: "Buổi chiều"
            },
            datetimereport: {
                trongngay: "Trong ngày",
                trongtuan: "Trong tuần",
                tuantruoc: "Tuần trước",
                trongthang: "Trong tháng",
                thangtruoc: "Tháng trước",
                quy1: "Quý 1",
                quy2: "Quý 2",
                quy3: "Quý 3",
                quy4: "Quý 4",
                trongnam: "Trong năm",
                namtruoc: "Năm trước",
                tatca: "Tất cả",
                tuychon: "Tùy chọn"
            },
            displaytreetype: {
                none: "Không hiển thị",
                unread: "báo cáo chưa đọc",
                unreadonall: "Chưa đọc / Tất cả",
                all: "Tất cả"
            },
            documentprocesstype: {
                tiepnhan: "Tiếp nhận",
                bangiao: "Bàn giao",
                kyduyet: "Ký duyệt",
                traketqua: "Trả kết quả",
                tiepnhanbosung: "Tiếp nhận bổ sung",
                giahan: "Gia hạn"
            },
            documenttype: {
                thongbao: "Thông báo",
                congvan: "Công văn"
            },
            egovjobenum: {
                indextimerelapsed: "IndexTimerElapsed",
                checkservices: "Kiểm tra những service không hoạt động",
                getdocumentsfromedoctool: "Kiểm tra xem có báo cáo mới tới không",
                notifydocunread: "Notify những báo cáo chưa đọc",
                notifydocinprocesses: "Notify những báo cáo chờ xử lý",
                checkchangedfile: "Kiểm tra file bị thay đổi",
                addindex: "Đánh index tìm kiếm"
            },
            feetype: {
                indextimerelapsed: "Tiếp nhận",
                thuongbosung: "Thường bổ sung",
                tracongdan: "Trả công dân"
            },
            leveltype: {
                sobannganh: "Sở, Ban ngành",
                quanhuyen: "Quận, Huyện",
                phuongxa: "Xã, Phường"
            },
            licensestatus: {
                capmoi: "Cấp mới",
                capdoi: "Cấp đổi, bổ sung",
                thuhoi: "Thu hổi"
            },
            option: {
                documentonlinereg: "Đăng ký trực tuyến đã có tài khoản",
                documentonlineregnoaccount: "Đăng ký trực tuyến mà chưa có tài khoản",
                acceptdoconline: "Chấp nhận khi đăng ký trực tuyến",
                implementdoconline: "Yêu cầu bổ sung khi đăng ký trực tuyến",
                rejectdoconline: "Từ chối khi đăng ký trực tuyến"
            },
            papertype: {
                tiepnhan: "Tiếp nhận",
                thuongbosung: "Thường bổ sung",
                tracongdan: "Trả công dân"
            },
            permissiontypes: {
                ktao: "Khởi tạo báo cáo",
                xly: "Xử lý báo cáo"
            },
            processfilterexpression: {
                groupby: "Nhóm theo",
                equal: "Bằng",
                custom: "Khác"
            },
            scheduletype: {
                hangphut: "Hàng phút",
                hanggio: "Hàng giờ",
                hangngay: "Hàng ngày",
                hangtuan: "Hàng tuần",
                hangthang: "Hàng tháng"
            },
            searchtype: {
                document: "Tìm kiếm báo cáo",
                file: "Tìm kiếm trong file"
            },
            securitytype: {
                thuong: "Thường",
                mat: "Mật",
                toimat: "Tối mật"
            },
            sendtype: {
                buudien: "Bưu điện",
                email: "Email",
                fax: "Fax",
                traotay: "Trao tay"
            },
            servicestatus: {
                running: "Đang chạy",
                stoped: "Đang dừng",
                paused: "Đang tạm dừng",
                accessdenied: "Không có quyền truy cập service",
                notfound: "Service chưa được cài đặt trên hệ thống"
            },
            specialkeyenum: {
                nguoidangnhap: "Người in phiếu",
                ngaythanghientai: "Ngày tháng hiện tại",
                meetingtitle: "Tiêu đề cuộc họp",
                meetingresource: "Địa điểm họp",
                meetingdate: "Thời điểm họp",
                meetingtodate: "Thời điểm kết thúc",
                meetingcreator: "Người tạo cuộc họp",
                meetingbody: "Nội dung cuộc họp",
                meetingusers: "Người tham gia cuộc họp",
                meetinglastupdate: "Người cập nhật cuộc họp"
            },
            supplementtype: {
                reset: "Tính lại thời gian",
                'continue': "Tiếp tục xử lý",
                fixeddays: "Thêm ngày cố định"
            },
            templatetype: {
                phieuin: "Phiếu in",
                email: "Thư điện tử",
                sms: "Tin nhắn sms"
            },
            timerjobtype: {
                warning: "Cảnh báo",
                searchindex: "Đánh chỉ mục tìm kiếm",
                deletetempfile: "Xóa bỏ các file tạm"
            },
            urgent: {
                thuong: "Thường",
                khan: "Khẩn",
                hoatoc: "Hỏa tốc"
            },
            quickview: {
                hide: "Không hiển thị",
                below: "Ở bên dưới",
                right: "Ở bên phải"
            },
            fontsize: {
                nho: "Chữ nhỏ",
                vua: "Chữ vừa",
                lon: "Chữ lớn"
            },
            notify: {
                documentnotifytype: {
                    hide: "Không hiển thị",
                    shownotifyinprocess: "Chỉ hiển thị thông báo báo cáo chờ xử lý",
                    all: "Hiển thị tất cả thông báo báo cáo có liên quan"
                },
                bmailnotifytype: {
                    hide: "Không hiển thị",
                    inbox: "Chỉ hiển thị thư cá nhân",
                    option: "Hiển thị trên các thư mục đã xem",
                    all: "Hiển thị tất cả thư nhận được"
                },
            }
        },
        documentNotifications: "Thông báo báo cáo",
        emptyDocumentNotifications: "Bạn không có thông báo báo cáo nào!",
        openAllDocument: "Mở tất cả báo cáo được thông báo",
        downloaddesktopversion: "Tải bản desktop",
        gtv: "Kiểu gõ",
        notifications: "Thông báo",
        news: "Tin điều hành",
        newEmail: "Soạn thư",
        config: "Thiết lập",
        reply: "Gửi phản hồi",
        smallSize: "Xem cỡ nhỏ",
        mediumSize: "Xem cỡ vừa",
        largeSize: "Xem cỡ lớn",
        underPreview: "Xem trước bên dưới",
        rightPreview: "Xem trước bên phải",
        hidePreview: "Ẩn xem trước",
        reload: "Khởi động lại",
        logout: "Đăng xuất",
        searchDocument: "Tìm kiếm báo cáo",
        searchFile: "Tìm kiếm file đính kèm",
        reloadMessage: "Một số thiết lập yêu câu phải reload lại hệ thống. Bạn có muốn relo…",
        closeBtn: "Đóng",
        submitBtn: "Cập nhật",
        titleMessage: "Thông báo!",
        closeAll: "Đóng tất cả lại",
        reportL: "Thống kê",
        contacts: "Sổ liên lạc",
        calendar: "Lịch",
        statictisLabel: "Giám sát, Thống kê",
        cbcl: "Chaas",
        reportLabel: "Báo cáo",
        chat: "Chat",
        documentslabel: "Xử lý công văn",
        placeholderSearch: "Tìm kiếm thông tin hồ sơ, báo cáo, tệp đính kèm",
        administrator: "Quản trị hệ thống",
        links: "Liên kết",
        conversion: "Hội thoại",
        messageNoBtn: "Không",
        mailNotifications: "Thông báo mail",
        emptyMailNotifications: "Bạn không có thông báo mail nào",
        openAllMail: "Mở tất cả mail nhận được",
        chatNotifications: "Thông báo chat",
        emptyChatNotifications: "Bạn không có tin nhắn nào",
        openAllChat: "Mở tất cả tin nhắn nhận được",
        bmail: "Tin điều hành",
        notJqueryAlert: "Chưa có file jquery. Vui lòng tải thêm file jquery!",
        lblDocument: "báo cáo",
        lblNewConversion: "Hội thoại",
        lblNewWorkTime: "Tạo lịch",
        lblNewMail: "Soạn thư",
        searchMail: "Tìm kiếm mail",
        youHave: "Bạn có",
        unreadDocuments: "báo cáo chưa xem",
        'delete': "Xóa",
        activityLog: {
            questionDelete: "Bạn có muốn xóa các nhật ký này không?",
            notChoice: "'Bạn chưa chọn nhật ký muốn xóa'"
        },
        level: {
            nodata: "Không có cấp hành chính nào"
        },
        license: {
            AddLicense: "Đăng ký",
            RegisterLicense: "Đăng ký bản quyền",
            customername: "",
            Phone: "Số điện thoại",
            Email: "Email",
            ToDate: "Ngày hết hạn",
            TotalUser: "Số tài khoản",
            key: "Mã kích hoạt"
        },
        log: {
            logNotSelect: "Bạn chưa chọn nhật ký muốn xóa",
            deleteSelection: "Xóa nhật ký được chọn",
            detail: "Chi tiết nhật ký"
        },
        notify: {
            noform: "Chưa có mẫu nào",
            nouse: "Không sử dụng",
            urgent: {
                name: "Độ khẩn",
                level1: "Bình thường",
                level2: "Khẩn",
                level3: "Thượng khẩn"
            },
            hasRead: "Được duyệt",
            alerttime: " phút (Đặt = 0 để gửi sms ngay khi có cuộc họp mới)",
            noJquery: "Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử d…",
            noUnderscore: "Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước …",
            openall: "Mở tất cả",
            closeall: "Đóng tất cả",
            nomailnotify: "Bạn không có thông báo mail nào.",
            nodocumentnotify: "Bạn không có thông báo báo cáo nào",
            nochatnotify: "Bạn không có tin nhắn nào",
            valuemodelnotnull: "Giá trị model không được để null"
        },
        office: {
            nooffice: "Không có cơ quan nào"
        },
        paper: {
            nopaper: "Không có giấy tờ nào",
            other: "Khác",
            list: "Danh sách giấy tờ",
            action: "Nghiệp vụ",
            docfield: "Lĩnh vực",
            doctype: "Loại hồ sơ",
            addpaper: "Thêm mới giấy tờ",
            updatepaper: "Cập nhật giấy tờ"
        },
        people: {
            nopeople: "Không có người dùng nào",
            peoplesearch: "Tìm kiếm tài khoản "
        },
        position: {
            sorterror: "Có lỗi khi sắp xếp mức ưu tiên của chức vụ.",
            npposition: "Không có chức vụ nào",
            orderedsort: "Cho phép sắp xếp thứ tự bằng cách kéo thả"
        },
        printer: {
            addprinter: "Thêm mới máy in",
            editprinter: "Thiết lập cho máy in",
            nodata: "Không có máy in nào",
            notconnect: "Không kiểm tra kết nối được tới máy in!",
            nameisrequired: "Tên máy in không được để trống!"
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
            enternote: "(Dùng phím enter để xuống dòng (nếu ở dòng cuối cùng thì thêm dòng …",
            divrole: "Phân quyền",
            addnodefilter: "Thêm bộ lọc mới cho node",
            parameterlisthaverequirement: "Bạn chưa nhập đầy đủ thông tin trong phần danh sách tham số",
            docfieldnote1: " Nếu tham số có cột giá trị là DocFieldId thì hệ thống sẽ ",
            docfieldnote2: "ngầm hiểu là cây báo cáo đó sẽ lọc theo lĩnh vực và loại hồ sơ",
            columnname: "Tên cột giá trị",
            paraname: "Tên tham số",
            updatenodetype: "Cập nhật loại node",
            document: "Hồ sơ công việc",
            addNode: "Thêm node mới",
            copyNode: "Copy node này",
            paste: "Dán node",
            'delete': "Xóa node này",
            confirmdeletenode: "Xóa 1 node sẽ xóa cả node con, bạn chắc chắn muốn xóa chứ?",
            deletenodesuccessfull: "Xóa node thành công!",
            nodata: "Không có loại node nào",
            nofilter: "Không có bộ lọc nào",
            nogroup: "Không có nhóm nào",
            alldocument: "---Tất cả loại báo cáo, hồ sơ---",
            someinfoisrequired: "Bạn chưa nhập đầy đủ thông tin trong phần cấu hình danh sách",
            parent: "Cha",
            normal: "Bình thường",
            field: "Trường dữ liệu",
            type: "Kiểu",
            displayname: "Tên hiển thị",
            filterByOverdueDate: "Lọc theo hạn xử lý:",
            display: "Hiển thị",
            defaultdocumentsortconfig: "Cấu hình sắp xếp báo cáo mặc định",
            entertobreakpage: "(Dùng phím enter để xuống dòng (nếu ở dòng cuối cùng thì thêm dòng …",
            configlistbynode: "Cấu hình danh sách tương ứng với node"
        },
        question: {
            nodata: "Không có câu hỏi nào",
            name: "Tên câu hỏi",
            content: "Nội dung câu hỏi",
            citizenname: "Tên công dân",
            email: "Email",
            phone: "Số điện thoại",
            uptohome: "Cho lên trang hành chính công",
            quickanswer: "Trả lời nhanh",
            answer: "Câu trả lời",
            btnAnswer: "Trả lời",
            transfer: "Chuyển cán bộ trả lời",
            date: "Thời gian hỏi",
            Compendium: "Trích yếu hồ sơ",
            reject: "Từ chối trả lời",
            rejectClause: "Lý do từ chối",
            rejectHolder: "Nhập lý do từ chối trả lời",
            rejectConfirm: "Xác nhận",
            searchUser: "Tìm tài khoản cán bộ",
            chooseuser: "Chọn cán bộ xử lý",
            forwarding: "Đang chuyển câu hỏi",
            forwardsuccess: "Chuyển câu hỏi thành công",
            btnShowDocumentDetail: "Xem chi tiết",
            listquestiontitle: "Danh sách câu hỏi",
            detail: "Chi tiết câu hỏi",
            showadvancededit: "Nâng cao",
            commentList: "Ý kiến cán bộ xử lý",
            compendium: "Trích yếu hồ sơ",
            transferComment: "Nhập ý kiến chuyển",
            defaultTransferComment: "Trả lời hộ nhe cưng",
            holding: "đang giữ câu hỏi",
            noquestion: "Không có câu hỏi nào"
        },
        report: {
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
            confirmdeletereport: "Bạn có chắc muốn xóa báo cáo này cùng với tất cả các báo cáo con củ…",
            config: "Cấu hình báo cáo",
            configsetup: "[cấu hình]",
            showguide: "Bật/tắt hướng dẫn",
            exprort: "Xuất ra file",
            group: "Nhóm",
            print: "In",
            view: "Xem",
            totalDocuments: "Tổng số báo cáo:",
            totalDocumentNotProcessed: "Số báo cáo chưa được xử lý:",
            totalDocumentProcessed: "Số báo cáo đã được xử lý:",
            totalDocumentOverdue: "Số báo cáo quá hạn:"
        },
        resource: {
            nodata: "Không có resource nào",
            choosefileimport: "Chọn tệp import"
        },
        role: {
            nodata: "Chưa có người dùng nào",
            isallow: "Cho phép",
            rolename: "Tên quyền",
            nodatagroup: "Không có nhóm người dùng nào"
        },
        scopearea: {
            nodata: "Không có ScopeArea nào"
        },
        shared: {
            productname: "Bkav eGov",
            systemtree: "Cây hệ thống",
            home: "Trang chủ",
            admincustomer: "Quản trị khách hàng"
        },
        store: {
            pts: "(Phụ trách sổ)",
            nouser: "Chưa có người dùng nào",
            tempforstore: "Danh sách mẫu cho sổ",
            alltempname: "Tất cả tên mẫu",
            notemp: "Không tồn tại mẫu nào!",
            _all: "[Tất cả]",
            nodocumentstore: "Không có sổ hồ sơ nào",
            choosecategory: "Chọn nghiệp vụ",
            addstoreviewer: "Thêm người xem sổ",
            addDocFields: "Thêm lĩnh vực",
            docField: "Lĩnh vực"
        },
        template: {
            nodata: "Không có mẫu nào",
            key: "Key dùng chung",
            systemerror: "Hệ thống lỗi, không thay đổi được trạng thái mẫu phiếu",
            printorder: "Phiếu in",
            per1: "Tiếp nhận",
            per2: "Bàn giao",
            per4: "Ký duyệt",
            per8: "Trả kết quả",
            per16: "Tiếp nhận bổ sung",
            per32: "Gia hạn",
            specialkey: "Key từ cuộc họp",
            keyfromform: "Key từ biểu mẫu",
            questionkey: "Key hỏi đáp",
            documentOnlineKey: "Key đăng ký qua mạng",
            dbkey: "Key lấy từ CSDL",
            commonKey: "Các key dung chung"
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
            nokey: "Không có key nào"
        },
        transfertype: {
            nodata: "Không có hình thức chuyển nào"
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
            defaultPasswordRest: "Mật khẩu reset mặc định",
            clearToRandomData: "Bỏ trống để tạo mật khẩu ngẫu nhiên"
        },
        ward: {
            city: "Tỉnh/Thành phố:",
            district: "Quận/huyện:",
            nodata: "Không có xã/phường nào",
            updatedata: "Cập nhật xã/phường",
            datalist: "Danh sách xã/phường"
        },
        welcome: {},
        buttons: {
            select: "Chọn",
            selectAll: "Chọn tất",
            edit: "Sửa",
            'delete': "Xóa",
            orderedsort: "Lưu thứ tự sắp xếp",
            orderedSortHint: "Cho phép sắp xếp thứ tự bằng cách kéo thả",
            orderedsave: "Lưu thứ tự",
            addfilter: "Thêm bộ lọc mới",
            addparameter: "Thêm tham số",
            save: "Lưu",
            confirm: "Xác nhận",
            back: "Quay lại danh sách",
            config: "Cấu hình",
            close: "Đóng",
            deleteAll: "Xóa toàn bộ",
            deleteall: "Xóa hết",
            search: "Tìm kiếm",
            agree: "Đồng ý",
            ignore: "Bỏ qua",
            add: "Tạo mới",
            view: "Xem",
            copy: "Sao chép",
            rerunAll: "Chạy lại toàn bộ",
            testDVC: "Test dịch vụ công"
        },
        tableheader: {
            stt: "STT",
            'function': "Chức năng",
            description: "Mô tả",
            form: "Mẫu",
            edit: "Sửa",
            select: "Chọn",
            'delete': "Xóa",
            type: "Kiểu",
            formname: "Tên mẫu",
            filtername: "Tên bộ lọc",
            columnname: "Tên cột",
            justify: "Căn lề",
            displayname: "Tên hiển thị",
            width: "Chiều rộng",
            allowsort: "Cho phép sắp xếp",
            sortcolumn: "Cột sắp xếp",
            sort: "Sắp xếp",
            value: "Giá trị",
            name: "Name",
            domain: "Domain",
            ip: "ip",
            zone: "Vùng",
            isallow: "Cho phép",
            addordelete: "Thêm/Bỏ",
            sortcolumnname: "Tên cột sắp xếp",
            sorttype: "Kiểu sắp xếp(tăng hoặc giảm)",
            order: "Thứ tự",
            isallowsort: "Cho phép sắp xếp"
        },
        commonlabel: {
            list: "Danh sách",
            select: "Chọn",
            is: "Là",
            or: "Hoặc...",
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
                affternoon: "Buổi chiều"
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
            'delete': "Xóa",
            other: "Khác",
            haveerrortryagain: "Có lỗi xảy ra, vui lòng thử lại!",
            deincrease: "Giảm dần"
        },
        sitemap: {
            config: "Cấu hình",
            general: "Thiết lập hệ thống",
            processfunction: "Cây báo cáo",
            resource: "Tài nguyên",
            activitylog: "Nhật ký hành động",
            egovjob: "Quản lý tiến trình của hệ thống ",
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
            doctype: "Loại báo cáo",
            configworkflow: "Quy trình",
            category: "Hình thức báo cáo",
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
            law: "báo cáo quy phạm",
            guide: "Hướng dẫn",
            question: "Câu hỏi",
            onlinetemplate: "Biểu mẫu hành chính",
            treeGroup: "Nhóm cây báo cáo",
            permissionSetting: "Cấu hình quyền trên node",
            config7: "Sao lưu",
            docColumnSetting: "Cấu hình hiển thị danh sách",
            reportgroup: "Nhóm báo cáo",
            paper: "Giấy tờ",
            fee: "Lệ phí",
            requiredSupplementary: "Mẫu yêu cầu bổ sung",
            sharefolder: "Địa chỉ lưu trữ file sao lưu",
            backuprestoreconfig: "Sao lưu cơ sở dữ liệu",
            backuprestorehistory: "Lịch sử sao lưu cơ sở dữ liệu",
            backuprestorefileconfig: "Sao lưu thư mục",
            backupRestoreManager: "Quản lý tệp tin sao lưu",
            printer: "Quản lý máy in",
            interfaceConfig: "Giao diện",
            actionlevel: "Kỳ báo cáo"
        },
        docfield: {
            store: "Sổ hồ sơ",
            nodocumentstore: "Không có sổ hồ sơ nào"
        },
        crystalreport: {
            copyfromstatisticform: "Sao chép từ mẫu thống kê",
            copyfromreportform: "Sao chép từ mẫu báo cáo",
            reconfig: "Config lại từ đầu",
            save: "Lưu lại"
        },
        doctype: {
            othernodes: "Các nốt khác",
            addcontrol: "Thêm control",
            downloadworkflowerror: "Cõ lỗi khi tải quy trình",
            newworkflow: "Thêm quy trình mới",
            workflownameisrequired: "Bạn phải nhập tên quy trình",
            pasteworkflow: "Dán quy trình",
            usethisworkflow: "Dùng quy trình này",
            comfirmtocancel1: "Bạn có chắc muốn ",
            comfirmtocancel2: "bỏ ",
            comfirmtocancel3: "dùng quy trình này không?",
            update: "Cập nhật danh mục",
            editTemplateWorkflow: "Cấu hình giao diện",
            updatenode: "Câp nhật node",
            interfaceconfig: "Cấu hình giao diện",
            editthisworkflow: "Sửa quy trình này",
            copythisworkflow: "Copy quy trình này",
            deletethisworkflow: "Xóa quy trình này",
            confirmtodeltethisworkflow: "Bạn có chắc muốn xóa quy trình này không?",
            notyouthisworkflow: "Bỏ dùng quy trình này",
            workflowname: "Tên quy trình",
            exprisedate: "Hạn xử lý",
            date: "Ngày",
            addtemplateform: "Thêm mẫu phôi",
            noformdata: "Không có mẫu nào",
            doctypename: "Tên hồ sơ: ",
            docfield: "Lĩnh vực: ",
            status: "Trạng thái:",
            active: "Kích hoạt",
            notactive: "Không kích hoạt",
            noconfiguration: "Chưa có cấu hình",
            confirmtodeletereceivenode: "Bạn có đồng ý xóa node đến này không?",
            objectype: "Kiểu đối tượng",
            value: "Giá trị",
            'delete': "Xóa",
            receivepostlist: "Danh sách nhận thông báo",
            removeReceiveList: "Bạn có đồng ý xóa danh sách thông báo này không?",
            of: "Của",
            allpeople: "Tất cả mọi người",
            level: "Cấp",
            user: "Người sử dụng",
            alljobtitle: "Tất cả chức danh",
            alljobtitleof: "Tất cả chức danh của ",
            alljobtitleof1: "Tất cả ",
            alljobtitleof2: " của ",
            jobtitledepartment: "Chức danh-phòng ban",
            alluserof: "Tất cả user của ",
            fieldisrequired: "Bạn phải nhập giá trị",
            embryonicformname: "Tên mẫu phôi:",
            embryonicformlist: "Danh sách các mẫu phôi:",
            addnewform: "Thêm mẫu mới"
        },
        form: {
            updateform: "Cập nhật loại hồ sơ, báo cáo",
            formname: "Tên mẫu",
            description: "Mô tả",
            tempkey: "Mẫu phôi",
            status: "Trạng thái",
            config: "Cấu hình",
            status1: " Đang sử dụng",
            status2: " Không sử dụng",
            status3: " Mẫu lưu tạm",
            configformtitle: "Title",
            addtitle: "Thêm Tiêu đề (nhãn)",
            showgrid: "Hiển thị đường kẻ",
            shownumber: "Hiển thị số",
            addrow: "Thêm dòng",
            chooseextendfield: "- - - Chọn trường mở rộng - - - ",
            choosedocumenttype: "- - - Chọn loại báo cáo - - - ",
            title: "Cấu hình mẫu cho loại hồ sơ",
            brand: "Ô nhập liệu (nhãn)",
            references: "Quan hệ",
            account: "Tên đăng nhập",
            currentusername: "Tên người đăng nhập hiện tại",
            currentdepartment: "Tên đơn vị/bộ phận/phòng ban",
            docfieldname: "Tên lĩnh vực",
            doctypename: "Tên loại hồ sơ",
            doccode: "Mã hồ sơ",
            receivedate: "Ngày tiếp nhận",
            appointdate: "Ngày hẹn trả",
            template: "Mẫu:",
            insertspecialvalue: "Chèn các giá trị đặc biệt:",
            formgroup: "Nhóm mẫu:",
            formtype: "Loại mẫu:",
            searchText: "Từ khóa",
            submit: "Tìm kiếm"
        },
        guide: {
            nodata: "Không có bản hướng dẫn nào"
        },
        increase: {},
        infomation: {
            chageinfo: "Thay đổi thông tin"
        },
        jobtitles: {
            nodata: "Không có chức danh nào",
            dragordroptosort: "Cho phép sắp xếp thứ tự bằng cách kéo thả",
            sorterror: "Có lỗi khi sắp xếp mức ưu tiên của chức danh."
        },
        editor: {
            deletecol: "Xóa cột",
            deleterow: "Xóa dòng",
            insertabove: "Chèn trên",
            insertbelow: "Chèn dưới",
            insertleft: "Chèn trái",
            insertright: "Chèn phải",
            merge: "Hợp nhất",
            splitcolumn: "Chia cột",
            splitrow: "Chia dòng",
            update: "Cập nhật",
            all: "[Tất cả]"
        },
        law: {
            choosedocument: "Chọn báo cáo",
            lawnumbercode: "Số kí hiệu",
            subContent: "Tóm tắt"
        },
        code: {
            choosedepartment: "Chọn phòng ban",
            name: "Nhảy số",
            config1: "Lấy ngày hiện tại, nếu nhỏ hơn 10 thì thêm số 0 đằng trước",
            config2: "Lấy ngày hiện tại",
            config3: "Lấy tháng hiện tại, nếu nhỏ hơn 10 thì thêm số 0 đằng trước",
            config4: "Lấy tháng hiện tại",
            config5: "Lấy năm hiện tại",
            config6: "Lấy 2 số cuối của năm hiện tại"
        },
        deparment: {
            choosejobtitle: "Chọn chức danh",
            chooseposition: "Chọn chức vụ",
            listuser: "Danh sách cán bộ thuộc phòng ban",
            nouser: "Chưa có người dùng nào",
            nodata: "Chưa có phòng ban",
            addsubdeparment: "Thêm mới phòng ban trực thuộc",
            deparmentinfo: "Thông tin phòng ban",
            deparmentname: "Tên phòng ban",
            updateinfo: "Cập nhật thông tin phòng ban",
            adduser: "Thêm người dùng vào phòng/ban",
            fullname: "Họ tên",
            isadmin: "Quản trị",
            jobtitle: "Chức danh",
            position: "Chức vụ",
            list: "Danh sách phòng ban",
            isprimary: "Phòng chính"
        },
        bkavmessagebox: {
            useshowtoreplacealert: "Sử dụng eGovMessage.show(message, title) để thay thế cho messageBox…",
            useshowtoreplaceconfirm: "Sử dụng eGovMessage.show(message, title, messageButtons.OkCancel) đ…",
            usenotificationtoreplacetemp: "Sử dụng eGovMessage.notification() để thay thế cho messageTemp().",
            closebutton: "X",
            yes: "Đồng ý",
            no: "Từ bỏ",
            ok: "Xác nhận",
            cancel: "Hủy bỏ",
            notify: "Thông báo"
        },
        workflow: {
            user: "Cán bộ",
            position: "Vị trí",
            relation: "Quan hệ",
            belowoffice: "Đơn vị cấp dưới",
            underoffice: "Đơn vị trực thuộc",
            currentoffice: "Đơn vị hiện tại",
            sameoffice: "Cùng đơn vị",
            peernode: "Node ngang hàng",
            sameparentnode: "Cùng Node cha",
            underuser: "Cấp dưới",
            overuser: "Cấp trên",
            addnotifyfouser: "Thêm user nhận thông báo",
            nodename: "Tên node",
            choosedepartment: "Chọn phòng ban",
            listuser: "Danh sách user",
            workflowTypes: "Loại con",
            addWorkflowType: "Thêm"
        },
        formtemplate: {
            columnwidth: "Chiều rộng cột",
            brandwidth: "Chiều rộng nhãn",
            height: "Chiều cao",
            disable: "Vô hiệu hóa",
            verifydata: "Có kiểm tra dữ liệu",
            compendium: "Trích yếu",
            comment: "Ý kiến xử lý",
            doctype: "Loại báo cáo",
            category: "Hình thức",
            inoutplace: "Đơn vị",
            dateappointed: "Thời hạn xử lý",
            organization: "Cơ quan gửi",
            doccode: "Số/ký hiệu *",
            doccode2: "Số hiệu *",
            datearrived: "Ngày đến",
            dateresponse: "Hồi báo",
            datepublished: "Ngày ban hành",
            store: "Sổ hồ sơ",
            storeid: "Sổ báo cáo",
            inoutcode: "Số đến đi",
            totalPage: "Số trang",
            choosetotalpage: "Chọn số trang",
            docfield: "Lĩnh vực",
            keyword: "Từ khóa",
            sendtype: "Hình thức gửi",
            doccode1: "Mã hồ sơ",
            citizenname: "Tên công dân",
            address: "Địa chỉ",
            phone: "Số điện thoại",
            docpapers: "Giấy tờ",
            identitycard: "Số CMT",
            email: "Thư điện tử",
            commune: "Xã phường",
            attachmentlist: "File đính kèm",
            relationlist: "báo cáo liên quan",
            cbdetail: "Hiển thị chi tiết báo cáo đến",
            allcomment: "Nội dung xử lý",
            titlecontent: "Nội dung báo cáo",
            urgent: {
                name: "Độ khẩn",
                normal: "Thường",
                fast: "Khẩn",
                important: "Hỏa tốc"
            },
            securityid: {
                name: "Độ mật",
                normal: "Thường",
                high: "Mật",
                important: "Tối mật",
                highest: "Tuyệt mật",

            },
            compendiumtitle: "Nhập trích yếu.",
            nocomment: "Chưa cho ý kiến",
            displayform: "Hiển thị biểu mẫu",
            storeprivate: "Hồ sơ cá nhân",
            storeshare: "Hồ sơ chia sẻ",
            nextpage: "Trang tiếp",
            prepage: "Trang trước",
            currentpage: "Trang",
            print: "In",
            btnfinish: "Kết thúc",
            viewicontraketqua: "Trả kết quả",
            viewicontiepnhanbosung: "Tiếp nhận bổ sung",
            viewiconhuyvanban: "Hủy",
            viewiconluu: "Lưu sổ",
            viewiconguiykien: "Gửi ý kiến",
            viewiconthongbao: "Thông báo",
            viewiconxinykien: "Xin ý kiến",
            viewiconyeucaubosung: "Yêu cầu bổ sung",
            viewicongiahanxuly: "Gia hạn",
            no: "Từ chối",
            yes: "Đồng ý",
            btninsertrelation: "báo cáo liên quan...",
            btninsertattachment: "Tệp đính kèm",
            btninsertscan: "Tệp scan...",
            btnpaper: "Giấy phép...",
            btninsertanticipate: "Dự kiến chuyển...",
            btntransfer: "Chuyển báo cáo/hồ sơ",
            btnedit: "Sửa nội dung báo cáo/hồ sơ",
            btninsertfile: "Đính kèm",
            btnapproveryes: "Đông ý phê duyệt",
            btnapproverno: "Từ chối phê duyệt",
            btndestroy: "Hủy báo cáo/hồ sơ",
            viewiconketthuc: "",
            btnfinishtt: "Kết thúc",
            btnanswer: "Trả lời",
            btnchangedoctype: "Phân loại",
            concurrency: "Vnd",
            usercomment: "Người xử lý",
            filename: "Tên tệp",
            filesize: "Kích thước",
            fileversion: "Phiên bản",
            lastupdatefile: "Cập nhật cuối",
            finalcomment: "Ý kiến giải quyết",
            backtolist: "Quay lại danh sách",
            'delete': "Xóa",
            mainprocess: "Xử lý chính:",
            coprocess: "Đồng xử lý:",
            sendto: "Chuyển tới",
            thongbao: "Thông báo:",
            xinykien: "Xin ý kiến:",
            view: "Xem",
            download: "Tải về",
            placelabel: "Nơi nhận",
            officename: "Tên cơ quan",
            placeinoffice: "Nơi nhận trong đơn vị",
            approvers: "Người ký",
            docinpage: "S.bản / s.trang",
            inplace: "Nơi lưu bản gốc",
            publishreceive: "Danh sách nhận báo cáo",
            createdate: "Ngày khởi tạo",
            createdate1: "Ngày tạo",
            panelselectorrequire: "Bạn phải truyền tham số panelSelector",
            publicoffice: "Cơ quan ban hành",
            insertanticipate: "Dự kiến chuyển",
            commoncomment: "Ý kiến thường dùng",
            receivedays: "Số ngày thụ lý",
            requirereport: "Yêu cầu hồi báo",
            fees: "Lệ phí",
            documentrelation: "báo cáo liên quan",
            attachment: "Tệp đính kèm",
            dateOverdue: "Hạn xử lý",
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
            },
            dateCreated: "Ngày tiếp nhận",
            delayReason: "Lý do muộn",
            note: "Ghi chú khác",
            typeReturn: "Nơi trả hồ sơ"
        },
        catalog: {
            addbewobject: "Thêm đối tượng"
        },
        menu: "Danh mục",
        processdoc: "báo cáo chờ xử lý",
        userConfig: {
            saveSuccess: "Lưu thiết lập thành công",
            saveError: "Lưu thiết lập không thành công"
        },
        imagePacket: "Đính kèm ảnh theo lô",
        plugin: {
            noplugin: "Bạn chưa cài đặt plugin",
            pluginrequire: "Bạn cần tải về và cài đặt plugin này để sử dụng chức năng mở tệp đí…",
            needrestartbrowser: "Nếu bạn vẫn thấy thông báo này sau khi cài đặt plugin, hãy khởi độn…",
            downloadtosetup: "Tải về và cài đặt",
            waitforsetup: "Đang chờ cài đặt plugin..."
        },
        avatar: {
            nodata: "../../../AvatarProfile/noavatar.jpg",
            errorUrl: "../../../AvatarProfile/noavatar.jpg",
            icon: "../../../AvatarProfile/icon/i ({0}).png",
            troll: "../../../AvatarProfile/troll/t ({0}).png",
            alphabet: "../../../AvatarProfile/alphabet/{0}.png",
            path: "https://danhba.bkav.com/avatars/{0}.bmp"
        },
        mobile: {
            main: {
                documentSearch: "Nhập số ký hiệu, trích yếu,...",
                mailSearch: "Nhập từ khóa tìm kiếm",
                logout: "Đăng xuất",
                exit: "Thoát",
                reload: "Khởi động lại",
                config: "Thiết lập",
                createPersonMail: "Gửi mail cá nhân",
                createBoxMail: "Gửi tin vào đây",
                folderempty: "Làm rỗng mục tin",
                foldermarkread: "Đánh dấu tất cả là đã đọc",
                clearNotifies: "Xóa tất cả thông báo",
                nonotify: "Bạn không có thông báo nào"
            },
            usersetting: {
                loadavatar: "Tải ảnh đại diện",
                fullscreen: "Toàn màn hình",
                fullscreennode: "(khi cuộn trang)",
                appstart: "Ứng dụng khởi động",
                fontsize: "Cỡ chữ",
                fontsizeType: {
                    small: "Nhỏ",
                    normal: "Vừa",
                    large: "Lớn"
                },
                pagesizenode: "(chỉ cho mail)",
                pagesize: "Phân trang",
                language: "Ngôn ngữ",
                languagenode: "(cần khởi động lại)",
                fontfamily: "Font chữ",
                systemfont: "Mặc định",
                savelastapp: "Chạy sau cùng",
                appType: {
                    documents: "báo cáo",
                    bmail: "Thư",
                    chat: "Trò truyện",
                    calendar: "Lịch",
                    contacts: "Danh bạ"
                },
                notify: {
                    config: "Thông báo",
                    noNotify: "Tắt thông báo",
                    oneNotify: "Chỉ hiện thông báo cuối cùng",
                    allNotify: "Hiện tất cả thông báo"
                }
            },
            notify: {
                mailNotify: "Bạn có {0} thư mới.",
                chatNotify: "Bạn có {0} tin nhắn mới.",
                documentNotify: "Bạn có {0} báo cáo thông báo.",
                calendarNotify: "Bạn có {0} lịch làm việc chưa xem.",
                documentNotFound: "báo cáo thông báo không tồn tại"
            },
            connectionError: "Máy chủ mail không thể kết nối"
        },
        kntc: "Khiếu nại tố cáo",
        syncDocType: "Đồng bộ công văn liên thông"
    };

    //egov.resources.transfer = extend(egov.resources.transfer, {
    //    dgUserLabelM: "Cá nhân, đơn vị nhận bản sao",
    //    dgJobtitleLabelM: "Chức vụ nhận bản sao",
    //    dgDeptLabelM: "Phòng ban nhận bản sao",
    //    dgUserGiamsatM: "Cán bộ giám sát",
    //});
    //#region bmail
    bmail.resources = {
        mailbox: {
            inbox: "Hộp thư đến",
            sent: "Thư đã gửi",
            drafts: "Thư nháp",
            junk: "Thư rác",
            trash: "Thư đã xóa"
        },
        toolbar: {
            'delete': "Xóa",
            deletePer: "Xóa vĩnh viễn",
            reply: "Trả lời",
            replyall: "Trả lời tất cả",
            forward: "Chuyển tiếp",
            edit: "Sửa",
            spam: "Đánh dấu là spam",
            unspam: "Bỏ đánh dấu là spam",
            read: "Đánh dấu là đã đọc",
            unread: "Đánh dấu là chưa đọc",
            restore: "Khôi phục thư đã xóa"
        },
        button: {
            ok: "Xác nhận",
            cancel: "Hủy bỏ",
            'delete': "Xóa",
            send: "Gửi",
            close: "Đóng"
        },
        notify: {
            success: "Thành công",
            error: "Lỗi"
        },
        common: {
            processing: "Đang tải",
            label: {
                time: "Thời gian"
            },
            time: {
                today: "Hôm nay",
                yesterday: "Hôm qua",
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
                sun: "Chủ nhật"
            },
            table: {
                stt: "STT",
                _stt: "Số thứ tự"
            },
            searchresult: "Kết quả tìm kiếm"
        },
        main: {
            newmail: "Thêm mới thư",
            sendto: "Gửi tới",
            cc: "Cc",
            bcc: "Bcc",
            subject: "Chủ đề",
            originmessage: "----- Thông điệp gốc : ----- ",
            send: "Gửi",
            close: "Đóng",
            confirmSave: "Lưu",
            notSave: "Không lưu",
            mailContentChange: "Nội dung thư thay đổi",
            savedraft: "Bạn có muốn lưu nháp thư đang soạn?",
            savedraftsuccess: "Lưu nháp thành công",
            savedrafterror: "Lưu nháp không thành công, vui lòng thử lại sau."
        },
        detail: {
            time: "Thời gian",
            sendto: "Gửi tới",
            sendfrom: "Gửi từ",
            subject: "Chủ đề",
            nosubject: "Không có chủ đề",
            file: {
                toolarge: "Không đính kèm được file có kích thước quá lớn",
                tryagain: "Lỗi không đính kèm được file. Mời bạn thử lại."
            },
            fieldsrequire: "Nhiều thông tin không được để trống.",
            uploaderror: "Lỗi tải tập tin lên, vui lòng thử lại.",
            confirmdelete: "Bạn có muốn xóa thư này?",
            content: "Nội dung thư",
            messageorigin: " ----- Thông điệp gốc: ----- ",
            downnloadAll: "Tải xuống tất cả",
            attachList: "Danh sách tệp đính kèm"
        },
        savedraft: {
            success: "Lưu nháp thành công",
            error: "Lưu nháp thất bại, vui lòng thử lại sau."
        },
        sendmail: {
            success: "Đã gửi",
            sendding: "Đang gửi",
            error: "Không thể gửi tin, vui lòng thử lại sau."
        },
        error: {
            hasError: "Có lỗi xảy ra, vui lòng thử lại sau.",
            connectionError: "Kết nối đến máy chủ thất bại, vui lòng thử lại sau."
        }
    }
    //#endregion
})
(window, window.egov = window.egov || {}, window.bmail = window.bmail || {});