define([],
function () {
    "use strict";
    var _resource, DocumentToolBar;

    _resource = egov.resources;

    var fileUploadSetting = egov.setting.fileUploadSettings;

    DocumentToolBar = Backbone.View.extend({

        printer: null,

        emptyDocTypeId: "00000000-0000-0000-0000-000000000000",

        totalSelectedFiles: 0,

        viewIcon: {
            viewIconBangiao: false,
            viewIconPhanloai: false,
            viewIconSua: false,
            viewIconXinykien: false,
            viewIconKetthuc: false,
            viewIconLuu: false,
            viewIconThongbao: false,
            viewIconTraloi: false,
            viewIconTraloiHSMC: false,
            viewIconGuiykien: false,
            viewIconKyDuyet: false,
            viewIconGiaHanXuLy: false,
            viewIconTraKetQua: false,
            viewIconDatLichHenTiep: false,
            viewIconYeuCauBoSung: false,
            viewIconTiepNhanBoSung: false,
            viewIconLuuvanban: false,
            viewIconHuyVanBan: false,
            viewIconXuLyTheoLo: false,
            viewIconGiayPhep: false,
            viewIconCBCL: false,
            viewIconXacNhanBanGiao: false,
            viewIconSendMail: false,
            viewIconSendSms: false,
            viewIconLuuDuThao: false,
            viewIconPrint: false,
            viewIconOther: false,
            viewIconAttach: false,
            viewIconEditDocInfo: false, //true khi đang xử lý chèn ảnh theo lô, muốn sửa công văn
            viewIconDuplicate: false,
            viewIconDaPhatHanhOrKetThuc: false, //=> văn bản đã phát hành hoặc kết thúc thì vẫn có thể đính kềm tệp đính kèm
            viewIconUndoKetThuc: false,
            viewIconPhatHanhLai: false,
            viewIconCollapseForm: false,// nút hiển thị thu gọn form
            viewIconPrintXLVB : false,
            viewIconInvoice: true,
            viewConfigKey: true
        },

        events: {
            'click .btnTransfer': '_show2',
            'click .btnInsertRelation': '_insertRelation',
            'click .btnInsertScan': '_insertScan',
            'click .btnInsertAnticipate': '_insertAnticipateTransfer',
            'click .btnReload': 'reload',
            'click .btnApprover': '_approver',
            'click .btnDestroy': 'destroy',
            'click .btnFinish': '_finish',
            'click .btnFinishNotice': '_finishNotice',
            'click .btnQuestion': '_sendQuestion',
            'click .btnAnnouncement': '_announcement',
            'click .btnSendAnswer': '_sendAnswer',
            'click .btnSaveStore': '_saveStore',
            'click .btnSupplementary': '_showSupplementary',
            'click .requiredSupplementaryItem': '_sendSupplementary',
            'click .btnAddRequiredSupplementary': "_addRequiredSupplementary",
            'click .btnReceiveSupplmentary': "_receiveSupplementary",
            'click .btnAnticipatePublish': '_configPublishPlan',
            'click .btnRenewals': '_renewals',
            'click .btnReturn': '_return',
            'click .btnAppoint': '_appointment',
            'click .btnCBCL': "_cbcl",
            'click .btnConfirmTransferOrProcess': '_confirmTransferOrProcess',
            'click .btnSaveDraft': "_saveDraft",
            'click .btnphatHanhSoLieu': "_phatHanhTHSoLieuVaKetThuc",
            'click .btnDinhChinhSoLieu': "_dinhChinhSoLieu",
            'click .btnSave': "_saveDocument",
            'click .btnEditDocInfo': "_editDocInfo",
            'click .btnDuplicate': "_duplicateDocument",
            'click .btnBusinessLicense': '_businessLicense',
            'click .btnUndoFinish': '_undoFinish',
            'click .btnRePublish': '_rePublish',
            'click .btnCodeManager': '_showCodeTemp',
            'click .btnUpdateLastResult': '_updateResult',
            'click .btnBookCalendar': '_bookCalendar',
            'click .btnFinishAndPublish': '_finishAndPublish',
            //'click .btnAnswerHSMC': '_answerHSMC',
            'mouseover .btnSendMail': '_renderMailTemplate',
            'hover .btnSendSms': '_renderSmsTemplate',
            'click .btnInvoiceImport': '_importInvoice',
            'click .btnExportExcel': '_exportExcel',
            'click .btnExportPDF': '_exportPdf',
            'click .btnExportWord': '_exportDoc',
            'click .btnImportExcel': '_importExcel',
            'click .btnViewLeaff': '_viewLeaf',
            'click .btnImportWord': '_importWord',
            'click .btnSurveyReleased': '_surveyReleased',
            'click .btnExportSurveyPDF': '_surveyPDF',
            'click .btnAddIndicator': '_addIndicator',
            'click #btnKey': '_keyClicking'
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            this.$el = options.el;

            // extend chổ này do khi mở nhiều văn bản thì viewIcon luôn lấy của văn bản mở sau cùng, ko hiểu tại sao.
            // Cần xem lại lý do tại sao lại vậy
            this.viewIcon = $.extend({}, this.viewIcon);
            this.document = options.document;
            //Gán giá trị docId để đẩy lên view cho ddl
            if (egov.mobile) {
                this.viewIcon.docId = options.document.id;
            }
            if (this.document.model.attributes.CategoryBusinessId != 8) {
                this.viewIcon.viewConfigKey = false;
                //document.getElementById("configKey").style.display = "block";
            }
            this.isQuickRender = options.isQuickRender;
            this.render();
        },

        /// <summary>Hiển thị toolbar</summary>
        render: function () {
            // Kiểm tra quyền sử dụng các chức năng đối với văn bản hiện tại
            var that = this,
                template;

            template = that._getTemplate();
            if (that.defaultToolbarType) {
                this._setDefaultViewIcon(that.defaultToolbarType);
            } else {
                this.checkPermissions();
            }
            var CategoryBusinessId = that.document.model.get("CategoryBusinessId");
            var Status = that.document.model.get("Status");
            var StatusReport = that.document.model.get("StatusReport");
            var UserCurrentId = that.document.model.get("UserCurrentId");
            var UserCreatedId = that.document.model.get("UserCreatedId");
            var uid = egov.userId;
            var isPhanLoai = that.document.transferType ===
                        egov.locache.get('documentTransferType').taoMoiThongThuong;
            that.viewIcon.viewLuongChuyen = true;
            if ( that.document.isCreate ) {
                that.viewIcon.viewLuongChuyen = false;
            }
            //check icon phat hanh va ket thuc
            if (CategoryBusinessId == 4 && that.document.isCreate && Status ==1) {
                that.viewIcon.phatHanhTHSoLieu = false;
            }

            if (Status == 4 && StatusReport == 4) {
                that.viewIcon.phatHanhTHSoLieu = false;
            }

            if (CategoryBusinessId == 4 && Status == 2 && StatusReport == 2 && UserCreatedId != uid) {
                that.viewIcon.phatHanhTHSoLieu = true;
            }
            //end check icon phat hanh va ket thuc
           
            require([template], function (Template) {
                if (that.document.model.get("CategoryBusinessId") == 32) {
                    var isPhanLoai = that.document.transferType ===
                        egov.locache.get('documentTransferType').banGiaoKhiPhanLoai;
                    if (that.document.isCreate || isPhanLoai) {
                        egov.request.getDocumentCreateAction({
                            data: {
                                documentTypeId: isPhanLoai
                                    ? that.document.newDoctype
                                    : that.document.model.get('DocTypeId'),
                                isPhanloai: that.document.transferType ===
                                    egov.locache.get('documentTransferType').banGiaoKhiPhanLoai
                            },
                            success: function (result) {
                                if (result) {
                                    that.actionSurvey = result.filter(function (x, y) {
                                        return `${x.id}`.includes("LuuSoVaPhatHanh");
                                    });
                                    var status = that.document.model.get("StatusReport") || 0;
                                    that.viewIcon.phatHanhSurvey = that.actionSurvey.length > 0 && status === 2;
                                    that.$el.html($.tmpl(Template, that.viewIcon));
                                }
                            }
                        });
                    } else {
                        egov.request.getDocumentEditAction({
                            data: { documentCopyId: that.document.model.get('DocumentCopyId') },
                            success: function (result) {
                                if (result) {
                                    if (result.error) {
                                        if (!egov.isMobile) {
                                            egov.pubsub.publish(egov.events.status.error, result.error);
                                        }
                                        that.$el.html($.tmpl(Template, that.viewIcon));
                                        return null;
                                    }
                                    that.actionSurvey = result.filter(function(x, y) {
                                        return `${x.id}`.includes("LuuSoVaPhatHanh");
                                    });
                                    var status = that.document.model.get("StatusReport") || 0;
                                    that.viewIcon.phatHanhSurvey = that.actionSurvey.length > 0 && status === 2;
                                    that.$el.html($.tmpl(Template, that.viewIcon));
                                }
                            }
                        });
                    }
                } else {
                    that.$el.html($.tmpl(Template, that.viewIcon));
                }

                if (that.defaultToolbarType) {
                    return;
                }
                
                ///Tạo tooltip cho các chức năng khi hiển thị ở quickview
                if (that.document.documentViewType !== egov.enum.documentViewType.default) {
                    ///Tìm các phần tử có class qtooltip
                    that.$el.find('.qtooltip').etip();
                }

                // Hiển thị danh sách loại văn bản đối với chức năng Phân loại và Trả lời.
                that.renderDoctypes();

                // Hiển thị danh sách nội dung văn bản cho phép sửa
                that.renderDocContents();

                if (that.viewIcon.viewIconDaPhatHanhOrKetThuc) {
                    that._insertAttachmentDaPhatHanhOrKetThuc();
                } else {
                    that._insertAttachment();
                }

                that._insertXMLAttachment();

                // that._insertPacket();

                that._insertImagePacket();

                that._renderPrints();
                //active tag file 
                var is_tag_file = egov.setting.transfer.isfiletag;
                if (is_tag_file === true) {
                    $("#tagfile").removeClass("hidden");
                }
                that.$el.bindResources();

                if (!that.viewIcon.viewIconGiaHanXuLy) {
                    that.document.$(".changeDateAppointed").hide();
                }
            });
        },

        _addIndicator: function () {
            var that = this;
            var doc = that.document.addIndicator();
        },

        /// <summary>Hiển thị danh sách các loại hồ sơ cho chức năng Trả lời và Phân loại</summary>
        renderDoctypes: function () {
            var that = this;
            var doctypes = egov.doctypes;
            if (doctypes && doctypes.length > 0) {
                var leng = doctypes.length;
                for (var i = 0; i < leng; i++) {
                    var doctype = doctypes[i];
                    var option = parseListItem(doctype.DocTypeName, doctype.DocTypeId, 'icon-text2', function (doctypeId, e) {
                        if ($(e.target).closest('ul.doctypeAnswer').length > 0) {
                            // Trả lời
                            that._answer(doctypeId);
                        }
                        else if ($(e.target).closest('ul.changeDoctypes').length > 0) {
                            // Phân loại
                            that._changeDoctype(doctypeId);
                        }
                    });

                    that.$('.ddlDoctypes').append(option);
                }
            }

            if (this.viewIcon.viewIconTraloiHSMC) {
                egov.request.getDocTypes({
                    success: function (doctypes) {
                        if (doctypes && doctypes.length > 0) {
                            var leng = doctypes.length;
                            for (var i = 0; i < leng; i++) {
                                var doctype = doctypes[i];
                                if (doctype.CategoryBusinessId == 4) {
                                    continue;
                                }
                                var option = parseListItem(doctype.DocTypeName, doctype.DocTypeId, 'icon-text2', function (doctypeId, e) {
                                    if ($(e.target).closest('ul.doctypeAnswerHSMC').length > 0) {
                                        // Trả lời
                                        that._answerHSMC(doctypeId);
                                    }
                                });

                                that.$('.ddlDoctypes').append(option);
                            }
                        }
                    }
                });
            }

            // Thêm trả lời kqklmr từ bwss cho phiên bản nội bộ
            if (egov.isPrivateVersion) {
                this.$kqkl = $("#kqklmrForm");
                var option = parseListItem("Trả lời bằng KQKLMR", "00000000", 'icon-text2', function () {
                    that._answerKQKLMR();
                });

                this.$('.doctypeAnswer').prepend(option);
                this._closeKQKLMRAndReloadRelation();
            }
        },

        /// <summary>Hiển thị danh sách các Nội dung văn bản cho chức năng Sửa</summary>
        renderDocContents: function () {
            if (this.viewIcon.viewIconBangiao) {
                var that = this,
                    contents = this.document.model.get('DocumentContents'),
                    leng = contents.length;
                if (leng == 1) {
                    this.$(".btnEdit").on("click", function (e) {
                        e.preventDefault();
                        that.document.editContent(contents[0].DocumentContentId);
                    });
                } else if (leng > 1) {
                    this.$(".btnEdit").addClass("dropdown-toggle").attr("data-toggle", "dropdown");
                    this.$(".btnEdit .caret").show();
                    var contentElement = this.$('.documentContents');
                    for (var i = 0; i < leng; i++) {
                        var content = contents[i];
                        var contentItem = parseListItem(content.ContentName, content.DocumentContentId, 'icon-history2', function (contentId) {
                            // Sửa nội dung văn bản hiện tại
                            that.document.editContent(contentId);
                        });
                        contentElement.append(contentItem);
                    }
                }
            }
        },

        ///<sumary>Kiểm tra quyền thao tao với văn bản</sumary>
        checkPermissions: function () {
            var egovPermission = egov.enum.permission,
                that = this;
            this._setDefaultViewIcon();

            if (this.document.model.get('DocumentCopyId') !== 0 && this.document.model.get('DocumentCopyId') !== "0") {
                this.viewIcon.viewIconBangiao = containFlags(this.model, egovPermission.bangiao);
                this.viewIcon.viewIconPhanloai = containFlags(this.model, egovPermission.phanloai);
                this.viewIcon.viewIconSua = containFlags(this.model, egovPermission.suavanban);
                this.viewIcon.viewIconXinykien = containFlags(this.model, egovPermission.xinykien);
                this.viewIcon.viewIconKetthuc = containFlags(this.model, egovPermission.ketthucxuly) || isLienThongError(that.document);
                this.viewIcon.viewIconLuu = containFlags(this.model, egovPermission.luuhosocanhan);
                this.viewIcon.viewIconThongbao = containFlags(this.model, egovPermission.thongbao);
                this.viewIcon.viewIconTraloi = containFlags(this.model, egovPermission.traloivanban);
                this.viewIcon.viewIconGuiykien = containFlags(this.model, egovPermission.guiykien);
                this.viewIcon.viewIconKyDuyet = containFlags(this.model, egovPermission.kyduyet);
                this.viewIcon.viewIconTraKetQua = containFlags(this.model, egovPermission.traketqua);
                this.viewIcon.viewIconYeuCauBoSung = containFlags(this.model, egovPermission.yeucaubosung);
                this.viewIcon.viewIconTiepNhanBoSung = containFlags(this.model, egovPermission.tiepnhanbosung);
                this.viewIcon.viewIconGiaHanXuLy = containFlags(this.model, egovPermission.giahanxuly);
                this.viewIcon.viewIconLuuvanban = containFlags(this.model, egovPermission.luuvanban);
                this.viewIcon.viewIconHuyVanBan = containFlags(this.model, egovPermission.huyvanban);
                this.viewIcon.viewIconCBCL = egov.isPrivateVersion;
                this.viewIcon.viewIconXacNhanBanGiao = containFlags(this.model, egovPermission.xacnhanbangiao);
                this.viewIcon.viewIconDatLichHenTiep = containFlags(this.model, egovPermission.hentiep);
                //Quyền sao chép văn bản, 
                this.viewIcon.viewIconDuplicate = true;
                // Hiển thị nút thu gọn form
                this.viewIcon.viewIconCollapseForm = true;

            }
            else {
                this.viewIcon.viewIconBangiao = true;
                this.viewIcon.viewIconLuuDuThao = true;
                this.viewIcon.viewIconKetthuc = this.document.newDoctype != this.emptyDocTypeId;
            }

            if (this.document.model.get("TransferTypeInEnum") === "PhanLoai") {
                this.viewIcon.viewIconBangiao = true;
                this.viewIcon.viewIconLuuDuThao = true;
                this.viewIcon.viewIconKetthuc = this.document.newDoctype != this.emptyDocTypeId;
            }

            if (this.document.model.get("CategoryBusinessId") != 1) {
                this.viewIcon.viewIconTraloi = false;
            }

            // Xét cho trường hợp văn bản đến tiếp nhận qua mạng
            if (this.document.model.get("DocTypeId") == this.emptyDocTypeId && this.document.newDoctype == null) {
                this.viewIcon.viewIconBangiao = false;
            }

            if (this.document.isInsertImagePacket) {
                this.viewIcon.viewIconOther = false;
            }

            this.viewIcon.viewIconTraloiHSMC = false;
            //if (this.document.model.get("CategoryBusinessId") == 4 && this.document.model.get('DocCopyStatus') == 2) {
            //    this.viewIcon.viewIconTraloiHSMC = true;
            //}

            if (egov.setting.isNotAllowFinishDocument) {
                this.viewIcon.viewIconHuyVanBan = false;
            }

            this.viewIcon.viewIconGiaHanXuLy = true;
            this.viewIcon.viewIconInvoice = this.viewIcon.viewIconYeuCauBoSung

            if (egov.setting.isNotAllowRenewal) {
                this.viewIcon.viewIconGiaHanXuLy = false;
            }
            // bao cao tuong minh
            if (this.document.model.get("CategoryBusinessId") == 8) {
                this.viewIcon.viewTuongMinh = true;
            }
            else
                this.viewIcon.viewTuongMinh = false;
            // survey
            this.viewIcon.phatHanhSurvey = false;
            this.viewIcon.viewSurvey = this.document.model.get("CategoryBusinessId") == 32;
        },
        _setIconSurvey: function (result) {
            this.viewIcon.phatHanhSurvey = result.filter(function (x, y) { return `${x.id}`.includes("LuuSoVaPhatHanh") }).length > 0;
        },
        checkDisable: function (event) {
            /// <summary>
            /// Kiểm tra xem target click có click được không.
            /// </summary>
            /// <returns type="bool"></returns>
            return $(event.target).hasClass("disabled") || $(event.target).parents().hasClass("disabled");
        },

        //#region Document Process methods

        _setDefaultViewIcon: function (defaultViewIcon) {
            /// <summary>
            /// Set lại defaut icon
            /// </summary>
            this.viewIcon.viewIconBangiao = false;
            this.viewIcon.viewIconPhanloai = false;
            this.viewIcon.viewIconSua = false;
            this.viewIcon.viewIconXinykien = false;
            this.viewIcon.viewIconKetthuc = false;
            this.viewIcon.viewIconLuu = false;
            this.viewIcon.viewIconThongbao = false;
            this.viewIcon.viewIconTraloi = false;
            this.viewIcon.viewIconGuiykien = false;
            this.viewIcon.viewIconKyDuyet = false;
            this.viewIcon.viewIconTraKetQua = false;
            this.viewIcon.viewIconYeuCauBoSung = false;
            this.viewIcon.viewIconTiepNhanBoSung = false;
            this.viewIcon.viewIconGiaHanXuLy = false;
            this.viewIcon.viewIconLuuvanban = false;
            this.viewIcon.viewIconHuyVanBan = false;
            this.viewIcon.viewIconXacNhanBanGiao = false;
            this.viewIcon.viewIconXuLyTheoLo = false;
            this.viewIcon.viewIconSendMail = false;
            this.viewIcon.viewIconSendSms = false;
            this.viewIcon.viewIconDatLichHenTiep = false;

            this.viewIcon.viewIconPrint = true;
            this.viewIcon.viewIconOther = true;
            this.viewIcon.viewIconAttach = true;

            this.viewIcon.viewIconLuuDuThao = false;
            this.viewIcon.viewIconDuplicate = false;

            this.viewIcon.viewIconDaPhatHanhOrKetThuc == false;

            //QuangP: bind trước 1 số icon trên toolbar để load lên đầu tiên, tránh việc lúc mở công văn, toolbar bị trắng
            //Hiện tại chỉ để mặc định mở lên là icon bàn giao, nếu muốn thêm icon mặc định khi mở công văn thì thêm vào
            if (defaultViewIcon === egov.enum.defaultToolbar.Create) {
                this.viewIcon.viewIconBangiao = true;
                return;
            }

            if (defaultViewIcon === egov.enum.defaultToolbar.Edit) {
                this.viewIcon.viewIconBangiao = true;
                return;
            }

            if (defaultViewIcon === egov.enum.defaultToolbar.InsertImagePacket) {
                this.viewIcon.viewIconPrint = false;
                this.viewIcon.viewIconOther = false;
                //this.viewIcon.viewIconEditDocInfo = true;

                return;
            }

            var categoryBussinessId = this.document.model.get("CategoryBusinessId");

            //Xử lý văn bản theo lô: Chỉ khi tạo mới văn bản đến mới có
            this.viewIcon.viewIconXuLyTheoLo = (this.document.model.get('DocumentCopyId') === 0
                && categoryBussinessId === egov.enum.categoryBusiness.vbden);
            //Giấy phép: tạo mới hồ sơ 1 cửa
            this.viewIcon.viewIconGiayPhep = (this.document.model.get('DocumentCopyId') != 0
                && categoryBussinessId === egov.enum.categoryBusiness.hsmc);

            //Gửi mail, sms cho người dân trên Hsmc
            var isHsmc = categoryBussinessId === egov.enum.categoryBusiness.hsmc;
            // this.document.model.get('DocumentCopyId') > 0 && categoryBussinessId === egov.enum.categoryBusiness.hsmc;

            this.viewIcon.viewIconSendMail = isHsmc;
            this.viewIcon.viewIconSendSms = isHsmc;
            this.viewIcon.viewIconPrint = isHsmc;

            this.viewIcon.viewIconPrintXLVB = !isHsmc;
            //if (this.document.model.get("CategoryBusinessId") == 4 && this.document.model.get('DocCopyStatus') == 2) {
            //    this.viewIcon.viewIconTraloiHSMC = true;
            //}

            //văn bản phát hành =>đã kêt thúc=> cho phép cập nhật các tệp đính kèm
            var checkDaPhatHanhOrKetThuc = this.document.model.get('DocCopyStatus') == 4
                && egov.userId == this.document.model.get('UserCurrentId');

            //egov.enum.permission
            this.viewIcon.viewIconDaPhatHanhOrKetThuc = checkDaPhatHanhOrKetThuc;//Văn bản kết thúc

            if (checkDaPhatHanhOrKetThuc) {
                this.viewIcon.viewIconAttach = true;
                this.viewIcon.dinhChinhSoLieu = true;
            }

            // Kiểm tra xem văn bản đã kết thúc thì thực hiện hiển thị việc lấy lại văn bản
            // Cần kiểm tra thêm là phải là người xử lý chính
            this.viewIcon.viewIconUndoKetThuc = false;
            this.viewIcon.viewIconPhatHanhLai = false;

            if (checkDaPhatHanhOrKetThuc || (this.document.model.get("CategoryBusinessId") === 2 && this.document.model.get("DocCode") && this.document.model.get('DocCopyStatus') == 0
                && egov.userId == this.document.model.get('UserCurrentId'))) {
                this.viewIcon.viewIconUndoKetThuc = true;
                if (this.document.model.get("CategoryBusinessId") === 2 && this.document.model.get("DocCode")) {
                    this.viewIcon.viewIconPhatHanhLai = true
                    this.viewIcon.viewIconUndoKetThuc = true;
                }
            }
        },

        _updateResult: function () {
            var that = this;
            that.document.updateLastResult("Cập nhật hồ sơ", function (result) {
                egov.pubsub.publish(egov.events.status.susscess, "Thêm dữ liệu thành công");
                that.document.tab.close(true);
                that.document._reloadDocumentTree();
            })
        },
        _show2: function (e) {
            /// <summary>
            /// Mở danh sách hướng chuyển
            /// </summary>
            /// <param name="e">Click event</param>
            var that = this,
                isPhanLoai;
            //Nếu e == undefined: Tải các hướng chuyển trước
            if (e) {
                var target = $(e.target).closest("a");
                if (!this.viewIcon.viewIconBangiao) {
                    return;
                }

                // Validate form
                if (!that.document.isValid()) {
                    // Hủy bỏ sự kiện click
                    egov.helper.destroyClickEvent(e);
                    return;
                }

                // Load hướng chuyển rồi thì hiển thị ra
                if (that.actionList !== undefined && that.document.transferType !== egov.locache.get('documentTransferType').banGiaoKhiPhanLoai) {
                    target.parent().toggleClass("open");
                    egov.helper.destroyClickEvent(e);
                    return;
                }
            }

            that.actionElement = that.$('.transferActions');

            if (that.actionListForShortKey !== undefined) {
                that.actionList = that.actionListForShortKey;
                that._showActionResult(that.actionList);
                return;
            }

            that.actionElement.append($('<li>').html(egov.helper.loading));
            isPhanLoai = that.document.transferType === egov.locache.get('documentTransferType').banGiaoKhiPhanLoai;

            if (that.document.isCreate || isPhanLoai) {
                egov.request.getDocumentCreateAction({
                    data: {
                        documentTypeId: isPhanLoai ? that.document.newDoctype : that.document.model.get('DocTypeId'),
                        isPhanloai: that.document.transferType === egov.locache.get('documentTransferType').banGiaoKhiPhanLoai
                    },
                    success: function (result) {
                        if (result) {
                            that._showActionResult(result);
                        }
                    },
                    complete: function () {
                        that.actionElement.find('.loading').parent().remove();
                    }
                });
            } else {
                this._getEditActions();
            }
        },
        _showActions: function (e) {
            /// <summary>
            /// Mở danh sách hướng chuyển
            /// </summary>
            /// <param name="e">Click event</param>
            var that = this,
                isPhanLoai;
            //Nếu e == undefined: Tải các hướng chuyển trước
            if (e) {
                var target = $(e.target).closest("a");
                if (!this.viewIcon.viewIconBangiao) {
                    return;
                }

                // Validate form
                if (!that.document.isValid()) {
                    // Hủy bỏ sự kiện click
                    egov.helper.destroyClickEvent(e);
                    return;
                }

                // Load hướng chuyển rồi thì hiển thị ra
                if (that.actionList !== undefined && that.document.transferType !== egov.locache.get('documentTransferType').banGiaoKhiPhanLoai) {
                    target.parent().toggleClass("open");
                    egov.helper.destroyClickEvent(e);
                    return;
                }
            }

            that.actionElement = that.$('.transferActions');

            if (that.actionListForShortKey !== undefined) {
                that.actionList = that.actionListForShortKey;
                that._showActionResult(that.actionList);
                return;
            }

            that.actionElement.append($('<li>').html(egov.helper.loading));
            isPhanLoai = that.document.transferType === egov.locache.get('documentTransferType').banGiaoKhiPhanLoai;

            if (that.document.isCreate || isPhanLoai) {
                egov.request.getDocumentCreateAction({
                    data: {
                        documentTypeId: isPhanLoai ? that.document.newDoctype : that.document.model.get('DocTypeId'),
                        isPhanloai: that.document.transferType === egov.locache.get('documentTransferType').banGiaoKhiPhanLoai
                    },
                    success: function (result) {
                        if (result) {
                            that._showActionResult(result);
                        }
                    },
                    complete: function () {
                        that.actionElement.find('.loading').parent().remove();
                    }
                });
            } else {
                this._getEditActions();
            }
        },
        _getEditActions: function () {
            var that = this;
            egov.request.getDocumentEditAction({
                data: { documentCopyId: that.document.model.get('DocumentCopyId') },
                success: function (result) {
                    if (result) {
                        if (result.error) {
                            if (!egov.isMobile) {
                                egov.pubsub.publish(egov.events.status.error, result.error);
                            }
                            return null;
                        }

                        that._showActionResult(result);
                    }
                },
                complete: function () {
                    that.actionElement.find('.loading').parent().remove();
                }
            });
        },

        _showActionResult: function (result) {
            // du lieu 1 ket qua
            //currentnodeid: 0, id: "ChuyenYKienDongGopVbXinYKien", isAllow: true, isspecial: true, name: "Chuyển ý kiến cho: Vũ Hoàng Trung", nextnodeid: 0, priority: 1, useridnext: 0, workflowid: 7

            // Không có hướng chuyển tiếp theo
            if (result.length === 0) {
                this.actionElement.html($('<li>').text(egov.resources.document.toolbar.noaction));
                return;
            }

            this.actionList = _.sortBy(result, function (action) { return action.priority; });
            this._insertTransferActions();
        },

        _getActionList: function () {
            /// <summary>
            ///Load trước ActionList
            /// </summary>
            // Load hướng chuyển rồi thì hiển thị ra
            if (this.actionListForShortKey !== undefined && this.document.transferType !== egov.locache.get('documentTransferType').banGiaoKhiPhanLoai) {
                return;
            }

            var that = this;
            var isPhanLoai = this.document.transferType === egov.locache.get('documentTransferType').banGiaoKhiPhanLoai;
            if (this.document.isCreate || isPhanLoai) {
                egov.request.getDocumentCreateAction({
                    data: {
                        documentTypeId: isPhanLoai ? this.document.newDoctype : this.document.model.get('DocTypeId'),
                        isPhanloai: this.document.transferType === egov.locache.get('documentTransferType').banGiaoKhiPhanLoai
                    },
                    success: function (result) {
                        if (result.error) {
                            // egov.message.error(result.error, true);
                            egov.pubsub.publish(egov.events.status.error, result.error);
                            return null;
                        }

                        // Không có hướng chuyển tiếp theo
                        if (result.length === 0) {
                            return;
                        }

                        that.actionListForShortKey = _.sortBy(result, function (action) { return action.priority; });
                    }
                });
            }
            else {
                var egovPermission = egov.enum.permission;
                if (containFlags(this.model, egovPermission.bangiao)) {
                    egov.request.getDocumentEditAction({
                        data: { documentCopyId: this.document.model.get('DocumentCopyId') },
                        success: function (result) {
                            if (result.error) {
                                // egov.message.error(result.error, true);
                                //egov.pubsub.publish(egov.events.status.error, result.error);
                                return null;
                            }

                            // Không có hướng chuyển tiếp theo
                            if (result.length === 0 && that.actionElement !== undefined) {
                                return;
                            }
                            that.actionListForShortKey = _.sortBy(result, function (action) { return action.priority; });
                        }
                    });
                }
            }
        },

        _insertTransferActions: function () {
            /// <summary>
            /// Hiển thị danh sách các hướng chuyển đối với văn bản hiện tại.
            /// </summary>
            var that = this,
                leng,
                action,
                transferIcon,
                actionEle,
                actionSpecials,
                maxAcitonsInList = 8,
                nextAction,
                transferPlan;

            this.actionElement.empty();
            if (this.secondActionElement) {
                this.secondActionElement.empty();
            }

            // Thêm hướng chuyển theo dự kiến (nếu có dự kiến chuyển cho mình)
            if (this.document.plan && this.document.plan.transfer) {
                this.actionElement.append(parseListItem(_resource.document.toolbar.transferByDk, 0, 'icon-paperplane', function () {
                    // Hàm bàn giao theo dự kiến
                    transferPlan = JSON.parse(that.document.plan.transfer);
                    that._transferPlan(transferPlan);
                }, false));
                this.actionElement.append($('<li>').addClass('divider'));
            }

            actionSpecials = egov.enum.actionSpecial;
            leng = this.actionList.length;

            //trường hợp danh sách hướng chuyển quá dài, tách list hướng chuyển thành 2
            //Tạm để nếu nhiều hơn 8 hướng chuyển thì tách
            if (leng > maxAcitonsInList) {
                this.secondActionElement = $("<ul class='dropdown-menu transferActions' role='menu'></ul>");
            }
            var survey = that.document.model.get("CategoryBusinessId");
            // Thêm các hướng chuyển của văn bản.
            for (var i = 0; i < leng; i++) {
                action = this.actionList[i];
                if (action.id == "LienThong") action.name = "Gửi phát hành báo cáo";
                transferIcon = _getTransferIcon(action.id);
                actionEle = i < maxAcitonsInList ? this.actionElement : this.secondActionElement;
                actionEle.append(parseListItem(action.name, action.id, transferIcon, function (key, e) {
                    if (key === actionSpecials.tiepTucXuLy.name
                        || key === actionSpecials.capNhatKetQuaDungXuLy.name
                        || key === actionSpecials.tiepNhanBoSung.name
                        || key === actionSpecials.lienThong.name) {
                        // Các hướng chuyển thuộc bổ sung hồ sơ, dừng xử lý
                        // Áp dụng cho văn bản, hồ sơ đã có
                        if (that.document.isCreate) {
                            return;
                        }

                        if (key === actionSpecials.lienThong.name) {
                            that._lienThongHs(key);
                        }

                        if (key === actionSpecials.capNhatKetQuaDungXuLy.name) {
                            that._continueProcess(key);
                        }

                        // Hàm bổ sung liên quan ở đây.
                    } else if (key === actionSpecials.tiepNhanHoSo.name
                        || key === actionSpecials.tiepNhanHoSoVaTiepTuc.name
                        || key === actionSpecials.chuyenNguoiCoQuyenDongGopYKien.name
                        || key === actionSpecials.chuyenNguoiGui.name
                        || key === actionSpecials.chuyenNguoiKhoiTao.name) {
                        // Hướng chuyển đặc biệt
                        if (key === actionSpecials.tiepNhanHoSo.name
                            || key === actionSpecials.tiepNhanHoSoVaTiepTuc.name) {
                            // Chuyển văn bản khởi tạo
                            that._transferSpecialCreate(key);
                        } else {
                            // Chuyển văn bản chỉnh sửa
                            that._transfer(key);
                        }
                    } else if (key === actionSpecials.luuSoNoiBo.name) {
                        // Lưu sổ nội bộ
                        // Hàm lưu sổ nội bộ ở đây
                    } else if (key === actionSpecials.luuSoVaPhatHanhNoiBo.name) {
                        // Phát hành nội bộ
                        that._privatePublish(true);
                    } else if (key === actionSpecials.luuSoVaPhatHanhRaNgoai.name) {
                        // Phát hành
                        that._publishment();
                    } else if (key === actionSpecials.chuyenYKienDongGopVbDxl.name
                              || key === actionSpecials.chuyenYKienDongGopVbXinYKien.name) {
                        // Chuyển ý kiến đóng góp
                        that._transferChuyenYKien(key);
                    } else {
                        // bàn giao văn bản theo hướng chuyển thông thường
                        if (survey != 32)
                            that._transfer(key);
                        else
                            that._transferSurvey(key);
                    }

                    egov.helper.destroyClickEvent(e);
                }, action.isAllow));
                // Phát hành báo cáo lên VP chính phủ
                var check = $(actionEle.children()).find('a').filter(function (x) { return $(this).attr('value') == 16; }).length > 0;
                if (survey !== 32 && !check && this.actionList.filter(function (x, y) { return `${x.id}`.includes("LuuSoVaPhatHanh") }).length > 0) {
                    transferIcon = _getTransferIcon(actionSpecials.luuSoVaPhatHanhRaNgoai.id);
                    actionEle.append(parseListItem('Phát hành báo cáo lên VP chính phủ', 16, transferIcon, function (key, e) {
                        that._publishmentGov();
                        egov.helper.destroyClickEvent(e);
                    }, action.isAllow));
                }
                if (i !== leng - 1 && i != maxAcitonsInList - 1) {
                    // Hiển thị phân cách nếu cuối danh sách.
                    nextAction = this.actionList[i + 1];
                    if (nextAction != undefined && action.priority != nextAction.priority) {
                        actionEle.append($('<li>').addClass('divider'));
                    }
                }
            }
            //append và css cho list hướng chuyển thứ 2
            if (this.secondActionElement) {
                if (this.actionElement.parent().find(this.secondActionElement).length == 0) {
                    if (egov.isMobile) {
                        this.actionElement.append($('<li>').addClass('divider'));
                        this.actionElement.append(this.secondActionElement.children());
                    }
                    else {
                        this.actionElement.parent().append(this.secondActionElement);
                    }
                }
                this.secondActionElement.css("left", this.actionElement.width() + 1);
            }

            // Nếu vừa tạo dự kiến chuyển, thêm hướng chuyển cho người nhận được dự kiến đó.
            if (this.document.newtransferplan) {
                this.actionElement.append(parseListItem(_resource.document.toolbar.transferUserDk, 0, 'icon-paperplane', function () {
                    // Hàm bàn giao cho người nhận dự kiến
                }, false));
            }
        },

        _transfer: function (actionId) {
            /// <summary>
            /// Bàn giao văn bản.
            /// </summary>
            /// <param name="actionId">Hướng chuyển</param>
            var action = _.find(this.actionList, function (action) {
                return action.id === actionId;
            });
            if (action) {
                this.document.transfer(action);
            }
        },
        //_publishAndFinish: function (actionId) {
        //    /// <summary>
        //    /// Phat hanh va ket thuc.
        //    /// </summary>
        //    /// <param name="actionId"></param>
        //    var action = _.find(this.actionList, function (action) {
        //        return action.id === actionId;
        //    });
        //    if (action) {
        //        this.document.publishAndFinish(action);
        //    }
        //},
        _transferSpecialCreate: function (actionId) {
            var action = _.find(this.actionList, function (action) {
                return action.id === actionId;
            });

            if (action === undefined) {
                action = _.find(this.actionListForShortKey, function (action) {
                    return action.id === actionId;
                });
            }

            if (action) {
                this.document.transferSpecialCreate(action);
            }
        },

        _transferChuyenYKien: function (id) {
            /// <summary>
            /// Chuyển ý kiến đóng góp
            /// </summary>
            /// <param name="id">id hướng chuyển</param>
            this.document.sendAnswer(id);
        },

        _transferPlan: function (plan) {
            if (plan) {
                var action = _.find(this.actionList, function (action) {
                    return action.id === plan.ActionId;
                });
                this.document.transferPlan(action, plan);
            }
        },

        _lienThongHs: function () {
            var that = this;

            require(["lienthong"], function (LienThong) {
                new LienThong({ document: that.document });
            });
        },

        _continueProcess: function () {
            /// <summary>
            /// Tiếp tục xử lý
            /// </summary>
            var that = this;

            require(["continueProcess"], function (ContinueProcess) {
                new ContinueProcess({ document: that.document });
            });
        },

        _publishment: function () {
            /// <summary>
            /// Lưu sổ và phát hành văn bản
            /// </summary>
            if (this.document && this.document.publishPlan) {
                this.document.publishmentPlan(JSON.parse(this.document.publishPlan));
            } else {
                this.document.publishment();
            }
        },
        _publishmentGov: function () {
            /// <summary>
            /// Lưu sổ và phát hành văn bản
            /// </summary>
            if (this.document && this.document.publishPlan) {
                this.document.publishmentPlan(JSON.parse(this.document.publishPlan));
            } else {
                this.document.publishmentGov();
            }
        },
        _transferSurvey: function (actionId) {
            /// <summary>
            /// Duyệt
            /// </summary>
            var action = _.find(this.actionList, function (action) {
                return action.id === actionId;
            });
            if (action) {
                var targetComments = egov.views.survey.getDestination();
                if (targetComments.length == 0) {
                    egov.pubsub.publish(egov.events.status.error, "Bạn chưa chọn đơn vị nhận . Vui lòng thử lại");
                    return;
                }
                this.document.transferSurvey(action);
            }
        },
        _publishmentSurvey: function () {
            /// <summary>
            /// phát hành khảo sát
            /// </summary>
            this.document.publishmentSurvey();
        },
        _privatePublish: function () {
            /// <summary>
            /// Lưu sổ phát hành nội bộ.
            /// </summary>
            this.document.privatePublishment();
        },

        _updateAttachment: function (newAttachment) {

        },

        _insertXMLAttachment: function () {
            /// <summary>
            /// Thêm tài liệu đính kèm
            /// </summary>
            var that = this;
            var newAttachment;
            var existFile;

            this.$('.fileuploadXML').fileupload({
                dataType: 'json',
                start: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                },
                stop: function () {
                    //egov.pubsub.publish(egov.events.status.destroy);
                },
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            $.ajax({
                                type: "POST",
                                url: '/Document/GetXMLFile2',
                                data: { 'filename': file.key, doctypeId: that.document.model.get("DocTypeId") },
                                success: function () {
                                    egov.pubsub.publish(egov.events.status.susscess, "Thêm dữ liệu thành công");
                                    that.document.tab.close(true);
                                    that.document._reloadDocumentTree();
                                },
                                error: function () {
                                    egov.pubsub.publish(egov.events.status.error, "Không đọc được file");
                                }
                            });
                        }
                    });
                },
                fail: function (e, data) {
                    egov.pubsub.publish(egov.events.status.error, _resource.file.errorUpload);
                    $(data.id).remove();
                }
            });
        },

        _insertAttachment: function () {
            /// <summary>
            /// Thêm tài liệu đính kèm
            /// </summary>
            var that = this;
            var newAttachment;
            var existFile;

            this.$('.fileupload').fileupload({
                dataType: 'json',
                dropZone: that.$(".divFiles"),
                pasteZone: null,
                add: function (e, data) {
                    var file = data.files[0];
                    var msg = "";
                    if (fileUploadSetting.maxFileSize && file.size > fileUploadSetting.maxFileSize) {
                        msg += file.name + ": " + _resource.file.lenghtIsNotAllow + _resource.file.maxLength + fileUploadSetting.maxFileSize / 1048576 + "Mb";
                    } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test('.' + file.name.split('.').pop()))) {
                        msg += file.name + ": " + _resource.file.typeIsNotAllow;
                    } else {
                        newAttachment = new egov.models.attachment({
                            Id: 0,
                            Name: file.name,
                            Size: file.size,
                            Extension: '.' + file.name.split('.').pop(),
                            Versions: [{
                                Version: 1,
                                User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                            }],
                            isNew: true,
                            isUploading: true
                        });

                        data.attachment = newAttachment;
                        that.document.attachments.model.add(newAttachment);
                    }
                    if (msg !== "") {
                        egov.pubsub.publish(egov.events.status.error, msg);
                    } else {
                        data.submit();
                    }
                },
                start: function () {
                    // egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                },
                progress: function (e, data) {
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    if (data.attachment && data.attachment.view) {
                        data.attachment.view.$(".progress-bar").css("width", progress + "%");
                    }
                },
                stop: function (e, data) {
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            that.document.attachments.model.remove(data.attachment);
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            var newFile = that.document.attachments.model.detect(function (f) {
                                return f.get('isNew') && f.get('Name') === file.name;
                            });
                            if (newFile) {
                                newFile.set('Id', file.key);
                                newFile.set('isUploading', false);
                            }
                        }
                    });
                },
                fail: function (e, data) {
                    egov.pubsub.publish(egov.events.status.error, _resource.file.errorUpload);

                    $(data.id).remove();
                }
            });
        },

        _insertAttachmentDaPhatHanhOrKetThuc: function () {
            /// <summary>
            /// Thêm tài liệu đính kèm
            /// </summary>
            var that = this;
            var newAttachment;
            this.$('.fileupload').fileupload({
                formData: { documentCopyId: that.document.model.get('DocumentCopyId') },
                dataType: 'json',
                dropZone: that.$(".divFiles"),
                pasteZone: null,
                add: function (e, data) {
                    var file = data.files[0];
                    var msg = "";
                    if (fileUploadSetting.maxFileSize && file.size > fileUploadSetting.maxFileSize) {
                        msg += file.name + ": " + _resource.file.lenghtIsNotAllow + _resource.file.maxLength + fileUploadSetting.maxFileSize / 1048576 + "Mb";
                    } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test('.' + file.name.split('.').pop()))) {
                        msg += file.name + ": " + _resource.file.typeIsNotAllow;
                    } else {
                        newAttachment = new egov.models.attachment({
                            Id: file.key ? file.key : 0,
                            Name: file.name,
                            Size: file.size,
                            Extension: '.' + file.name.split('.').pop(),
                            Versions: [{
                                Version: 1,
                                User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                            }],
                            isNew: true,
                            isPhatHanhOrKetThuc: true
                        });
                        that.document.attachments.model.add(newAttachment);
                    }
                    if (msg !== "") {
                        egov.pubsub.publish(egov.events.status.error, msg);
                    } else {
                        data.submit();
                    }
                },
                start: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing)
                },
                stop: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();

                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            var newFile = that.document.attachments.model.detect(function (f) {
                                return f.get('isNew') && f.get('Name') === file.name;
                            });
                            if (newFile) {
                                newFile.set('Id', file.key);
                            }
                        }
                    });
                },
                fail: function (e, data) {
                    egov.pubsub.publish(egov.events.status.error, _resource.file.errorUpload);

                    $(data.id).remove();
                }
            });
        },

        _insertRelation: function () {
            /// <summary>
            /// Thêm văn bản liên quan
            /// </summary>
            var that = this;

            require(['egovSearch'], function (Search) {
                //var search = new Search;
                var search = new Search({
                    currentDoc: that.document,
                    isRelationDoc: true
                });

                search.openDialog({
                    height: '400px',
                    width: '850px',
                    draggable: true,
                    title: _resource.document.relation.titleDialog,
                    buttons: [
                        {
                            text: _resource.common.addButton,
                            className: 'btn-primary',
                            click: function () {
                                var selected = search.getSelected();
                                if (!selected || selected.length <= 0) {
                                    return;
                                }
                                var $isHoiBao = search.$el.parents(".modal-dialog").find("#isHoiBao");
                                var isHoiBao = $isHoiBao.is(":checked");

                                var leng = selected.length;
                                search.$el.dialog('hide');
                                search.$el.empty();//tiennvg them vao vi khi close dialog still exist data-content 
                                for (var i = 0; i < leng; i++) {
                                    var relation = selected[i];
                                    that.document.relations.model.add(new egov.models.relation({
                                        RelationCopyId: relation.get('DocumentCopyId'),
                                        RelationId: relation.get('DocumentId'),
                                        RelationType: isHoiBao & isHoiBao == true?  3 : 1,
                                        IsAddNext: false,
                                        Compendium: relation.get('DocumentCompendium'),
                                        CitizenName: relation.get('DocumentCompendium'),
                                        DocCode: relation.get('ExtendInfo').DocCode,
                                        DateCreated: relation.get('ExtendInfo').DateCreate,
                                        CategoryName: relation.get('ExtendInfo').CategoryName
                                    }));
                                }
                            }
                        },
                        {
                            text: _resource.common.closeButton,
                            className: 'btn-close',
                            click: function () {
                                search.$el.dialog('hide');
                                search.$el.empty();//tiennvg them vao vi khi close dialog still exist data-content 
                            }
                        }
                    ],
                    addCallBack: function ($selector) {
                        $selector.parent().next().prepend('<div class="pull-left"><label class="checkbox document-color"><input id="isHoiBao" name="checkbox[]" value="2378" type="checkbox" ><span class="document-color-1"><i class="icon-check"></i></span></label><span for="isHoiBao">Là văn bản hồi báo</span></div>');
                    }
                });
            });
        },

        _insertScan: function () {
            var that = this;
            require(['scanner'], function (Scanner) {
                if (!that.isLoadedScanner) {
                    var scan = new Scanner({
                        isLoaded: false,
                        parent: that,
                    });
                    that.isLoadedScanner = true;
                } else {
                    var scan = new Scanner({
                        parent: that,
                        isLoaded: true
                    });
                }
            });
        },

        _insertPacket: function () {
            var that = this,
                newAttachment,
                createdInCurrentDoc = false,
                file,
                msg,
                fileExt;

            if (!egov.waitingPacket) {
                egov.waitingPacket = [];
            }

            var fileCount = 0;
            var uploaded = 0;
            that.$('.packetFileupload').fileupload({
                dataType: 'json',
                dropZone: that.$(".divFiles"),
                pasteZone: null,
                add: function (e, data) {
                    fileCount++;
                    file = data.files[0];
                    msg = "";
                    fileExt = '.' + file.name.split('.').pop();
                    if (fileUploadSetting.maxFileSize && file.size > fileUploadSetting.maxFileSize) {
                        msg += file.name + ": " + _resource.file.lenghtIsNotAllow;
                    } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test(fileExt) || fileExt === ".pdf" || fileExt === ".PDF")) {
                        msg += file.name + ": " + _resource.file.typeIsNotAllow;
                    }
                    if (msg !== "") {
                        egov.pubsub.publish(egov.events.status.error, msg);
                    } else {
                        data.submit();
                    }
                },
                start: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                },
                stop: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                done: function (e, data) {
                    uploaded++;
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            newAttachment = new egov.models.attachment({
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: '.' + file.name.split('.').pop(),
                                Versions: [{
                                    Version: 1,
                                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                                }],
                                isNew: true
                            });

                            egov.waitingPacket.push({
                                docTypeId: that.document.model.get("DocTypeId"),
                                title: that.document.model.get("DocTypeName"),
                                attachment: newAttachment,
                                opened: false,
                                fileName: file.name
                            });

                            egov.waitingPacket = _.sortBy(egov.waitingPacket, function (i) {
                                return i.fileName;
                            });

                            if (!createdInCurrentDoc && uploaded === fileCount) {
                                that.document.isPacket = true;
                                egov.views.home.tab.createDocInWaitingPacket(that.document);
                                createdInCurrentDoc = true;
                            }
                        }
                    });
                },
                fail: function (e, data) {
                    egov.pubsub.publish(egov.events.status.error, _resource.file.errorUpload);
                }
            });
        },

        // Chuyển pdf thành ảnh để tạo văn bản theo lô
        _insertImagePacket: function () {
            var that = this,
                newAttachment,
                createdInCurrentDoc = false,
                file,
                msg,
                fileExt;

            if (!egov.waitingPacket) {
                egov.waitingPacket = [];
            }

            var fileCount = 0;
            var uploaded = 0;
            that.$('.imagePacketFileupload').fileupload({
                dataType: 'json',
                dropZone: that.$(".divFiles"),
                pasteZone: null,
                singleFileUploads: false,
                add: function (e, data) {
                    file = data.files[0];
                    msg = "";
                    fileExt = '.' + file.name.split('.').pop();
                    fileExt = fileExt.toLowerCase();

                    if (fileUploadSetting.maxFileSize && file.size > fileUploadSetting.maxFileSize) {
                        msg += file.name + ": " + _resource.file.lenghtIsNotAllow;
                    } else if (!(egov.setting.acceptFileTypes.test(file.type) || egov.setting.acceptFileTypes.test(fileExt) || fileExt === ".pdf")) {
                        msg += file.name + ": " + _resource.file.typeIsNotAllow;
                    }

                    fileCount++;
                    if (msg !== "") {
                        egov.pubsub.publish(egov.events.status.error, msg);
                    } else {
                        data.submit();
                    }
                },
                start: function () {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);
                },
                stop: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                },
                done: function (e, data) {
                    uploaded++;
                    var fileIds = [];
                    $.each(data.result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                        } else {
                            newAttachment = new egov.models.attachment({
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: '.' + file.name.split('.').pop(),
                                Versions: [{
                                    Version: 1,
                                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                                }],
                                isNew: true
                            });

                            fileIds.push(file.key);

                            egov.waitingPacket.push({
                                docTypeId: that.document.model.get("DocTypeId"),
                                title: that.document.model.get("DocTypeName"),
                                attachment: newAttachment,
                                opened: false,
                                fileName: file.name
                            });

                            egov.waitingPacket = _.sortBy(egov.waitingPacket, function (i) {
                                return i.fileName;
                            });

                            // Trạng thái set về false trong tab.js phần mở packet
                            egov.hasImagePacket = true;
                            if (!createdInCurrentDoc) {
                                that.document.isPacket = true;
                                egov.views.home.tab.createDocInWaitingPacket(that.document);
                                createdInCurrentDoc = true;
                            }
                        }
                    });
                },
                fail: function (e, data) {
                    egov.pubsub.publish(egov.events.status.error, _resource.file.errorUpload);
                }
            });
        },

        _insertAnticipateTransfer: function () {
            /// <summary>
            /// Thêm dự kiến chuyển
            /// </summary>
            this.document.anticipate();
        },

        _finishAndPublish: function () {
            this.document.finishAndPublishData();
        },

        _changeDoctype: function (doctypeId) {
            /// <summary>
            /// Phân loại văn bản
            /// </summary>
            /// <param name="doctypeId">Loại văn bản phân loại đến.</param>
            var that = this,
                egovPermission = egov.enum.permission,// Hiển thị thông báo thay đổi hạn xử lý
                doctype,
                contents;

            this.document.newDoctype = doctypeId;
            this.document.hasChangeInfo = true;
            this.document.transferType = egov.locache.get('documentTransferType').banGiaoKhiPhanLoai;

            if (containFlags(this.model, egovPermission.doihanxulykhiphanloai)) {
                egov.message.show(egov.resources.document.ChangeDoctype.hasChangeDateAppoint, egov.resources.common.alert, egov.message.messageButtons.YesNo, function () {
                    that.document.hasChangeDateAppoint = true;
                });
            }

            if (this.document.model.get("DocTypeId") && this.document.model.get("DocTypeId") != this.emptyDocTypeId) {
                egov.message.show("Bạn có muốn đánh lại số đến cho văn bản này không?", egov.resources.common.alert, egov.message.messageButtons.YesNo, function () {
                    that.document.model.set("HasChangeInoutCode", true);
                    that.document.renderStoreWhenChangeDocType(doctypeId);

                });
            } else {
                that.document.model.set("HasChangeInoutCode", true);
                that.document.renderStoreWhenChangeDocType(doctypeId);
            }

            //egov.dataManager.getCurrentDoctypes({
            //    success: function (doctypes) {
            //        doctype = _.find(doctypes, function (dt) {
            //            return dt.DocTypeId === doctypeId;
            //        });

            //        egov.pubsub.publish(egov.events.status.success, String.format(egov.resources.document.ChangeDoctype.success, doctype.DocTypeName));
            //    }
            //});

            this.document.model.set("TransferTypeInEnum", "PhanLoai");
            if (!this.viewIcon.viewIconBangiao) {

                //this.document.model.set("DocumentCopyId", 0);

                //Set lại cho nội dung là đã sửa để khi bàn giao lấy lại nội dung
                contents = this.document.model.get("DocumentContents");
                contents[0].IsEdited = true;
                this.document.model.set("DocumentContents", contents);
                this.model = 0;
                this.render();
            }
        },

        _sendQuestion: function () {
            /// <summary>
            /// Xin ý kiến
            /// </summary>
            this.document.sendQuestion();
        },

        _announcement: function () {
            /// <summary>
            /// Thông báo
            /// </summary>
            this.document.announcement();
        },

        _sendAnswer: function () {
            /// <summary>
            /// Gửi ý kiến (Gửi ý kiến ở bất cứ thời điểm nào - đây không phải là chức năng trả lời của Xin ý kiến).
            /// </summary>
            this.document.sendComment();
        },

        _saveStore: function () {
            this.document.saveStore();
        },

        _answerHSMC: function (doctypeId) {
            var attachment = this.document.model.toJSON().Attachments;
            var attachments = _.each(attachment, function (attach) {
                attach.isNew = true;
            })
            this.document.answerHSMC(doctypeId, attachments);
        },

        _answer: function (doctypeId) {
            /// <summary>
            /// Trả lời
            /// </summary>
            /// <param name="doctypeId" type="Guid">Loại văn bản</param>
            this.document.answer(doctypeId);
        },

        _finish: function (e) {
            /// <summary>
            /// Kết thúc xử lý
            /// </summary>
            if (this.checkDisable(e)) {
                return;
            }

            this.document.$("#Comment").next(".error").remove();
            var comment = this.document.$("#Comment").val();

            if (egov.setting.transfer.requireCommentWhenFinish && comment === "") {
                this.document.$("#Comment").after('<label class="error">Bạn cần nhập lý do kết thúc văn bản.</span>');
                return;
            }

            // Nếu là văn bản đồng xử lý thì thực hiện gửi ý kiến và kết thúc.
            if (this.document.model.get("DocumentCopyType") == 2) {
                var actions = egov.enum.actionSpecial.chuyenYKienDongGopVbDxl.name;
                this._transferChuyenYKien(actions);
                return;
            }

            if (!this.document.model.get("DocumentCopyId") == 0) {
                this.document.finish();
                return;
            }

            // Kết thúc khi tạo mới
            var doc = this.document;
            require(['docStoreView'], function (DocStore) {
                if (!egov.views.home.documents.storePrivate) {
                    egov.views.home.documents.storePrivate = new DocStore;
                }

                var hasHideLuuSo = egov.setting.hasHideLuuSo;
                egov.pubsub.publish(egov.events.status.processing, egov.resources.document.finish.processing);
                doc.saveDocument(function (result) {
                    egov.request.finish(
                    {
                        data: {
                            documentCopyId: result.documentCopyId,
                            storePrivateId: storeId,
                            comment: comment,
                            isThongBao: false
                        },
                        success: function (finishResult) {
                            if (finishResult.error) {
                                egov.pubsub.publish(egov.events.status.error, egov.resources.document.finish.error);
                                doc.tab.close(true);
                            } else {
                                egov.pubsub.publish(egov.events.status.success, "Kết thúc văn bản thành công.");
                            }
                        },
                        error: function () {

                        }
                    });
                });
                //egov.views.home.documents.storePrivate.renderDialog(function (storeId) {
                    
                //});
            });
        },

        _finishNotice: function (e) {
            /// <summary>
            /// Kết thúc xử lý
            /// </summary>
            if (this.checkDisable(e)) {
                return;
            }

            this.document.finishNotification();
        },

        _approver: function (e) {
            /// <summary>
            /// Ký duyệt
            /// </summary>
            /// <param name="e">Click event</param>
            if (this.checkDisable(e)) {
                return;
            }

            var target = $(e.target).closest('.btnApprover'),
                approve = target.hasClass("yes");

            this.document._approve(approve);
            this._showActions(e);
        },

        _showSupplementary: function (e) {
            /// <summary>
            /// Hiển thị yêu cầu bổ sung
            /// </summary>
            var that = this,
                suppElement;

            if (this.checkDisable(e)) {
                return;
            }

            this.document.receiveSupplementary();

            //// Load hướng chuyển rồi thì hiển thị ra
            //if (this.requiredSupplementary !== undefined) {
            //    return;
            //}

            //this.suppElement = this.$('.supplementaryList');
            //this.suppElement.append($('<li>').html(egov.helper.loading));
            //egov.request.getRequiredSupplementary({
            //    data: { doctypeId: this.document.model.get('DocTypeId') },
            //    success: function (result) {
            //        if (result) {
            //            that.requiredSupplementary = result;
            //            _.each(result, function (supp) {
            //                var suppElement = $("<a  href='#' class='requiredSupplementaryItem'>").text(supp.Name);
            //                that.suppElement.prepend($("<li>").append(suppElement));
            //            });
            //        }
            //    },
            //    complete: function () {
            //        that.suppElement.find('.loading').parent().remove();
            //    }
            //});
        },

        _sendSupplementary: function (e) {
            var suppElement = $(e.target).closest('.requiredSupplementaryItem');
            this.document.model.get("Supplementary").add({ UserSendName: egov.userName, Comment: suppElement.text() })
            // this.document.insertSupplementary(suppElement.text());
        },

        _addRequiredSupplementary: function (e) {
            var that = this,
                suppElement;

            egov.message.prompt(egov.resources.requiredSupplementary.addRequiredTitle,
                egov.resources.common.addButton,
                function (content) {
                    if (content == null || content === '') {
                        return;
                    }
                    egov.request.createRequiredSupplementary({
                        data: {
                            content: content
                        },
                        success: function (result) {
                            if (result) {
                                that.requiredSupplementary.push(result);
                                suppElement = $("<a  href='#' class='requiredSupplementaryItem'>").text(result.Name);
                                that.suppElement.prepend($("<li>").append(suppElement));
                                that.$(".btnSupplementary").click();
                            }
                        }
                    });
                }, true
            );
        },

        _receiveSupplementary: function (e) {
            /// <summary>
            /// Tiếp nhận bổ sung
            /// </summary>
            var that = this;
            this.document.receiveSupplementary();
        },

        reload: function (event) {
            /// <summary>
            /// Reload lại nội dung chưa tab văn bản
            /// </summary>
            egov.helper.destroyClickEvent(event);
            this.document.reload();
        },

        _configPublishPlan: function () {
            //Check nếu có dự kiến phát hành rồi thì hiển thị lại theo dự kiến phát hành trước để sửa
            //không thì hiển thị form cấu hình dự kiến chuyển
            if (this.document && this.document.publishPlan) {
                var plan = JSON.parse(this.document.publishPlan);
                this.document.publishmentPlan(plan, true);
            } else {
                this.document.comfigPublishmentPlan();
            }
        },

        _getTemplate: function () {
            var template = egov.template.toolbar;
            if (egov.isMobile) {
                //Template cho giao diên màn hình cảm ứng
                return template.mobile;
            }

            //Template cho giao diên bình thường
            //Hiển thị khi mở tab văn bản/hồ sơ
            if (this.document.documentViewType === egov.enum.documentViewType.default) {
                return template.desktop;
            }

            //Hiển thị tabl bên phải
            if (egov.setting.userSetting.quickView === egov.enum.quickViewType.right) {
                return template.quickViewRight;
            }
            //Hiển thị khi mở tab dưới
            if (egov.setting.userSetting.quickView === egov.enum.quickViewType.below) {
                return template.quickViewBelow;
            }

            return '';
        },

        _renewals: function (e) {
            /// <summary>
            /// Gia hạn
            /// </summary>
            if (this.checkDisable(e)) {
                return;
            }

            this.document.renewals();
        },

        _return: function (e) {
            /// <summary>
            /// Trả kết quả
            /// </summary>
            if (this.checkDisable(e)) {
                return;
            }
            if (this.document.isHsmc) {
                this.document.returnResult();
            }
            //else if (this.document.isKNTC) {
            //    this.document.publicResult();
            //}
        },

        _appointment: function (e) {
            if (this.checkDisable(e)) {
                return;
            }
            if (this.document.isHsmc) {
                this.document.createNewAppoint();
            }
        },

        destroy: function (e) {
            /// <summary>
            /// Hủy văn bản
            /// </summary>
            if (this.checkDisable(e)) {
                return;
            }

            if (confirm(_resource.document.confirmDestroy)) {
                this.document.RemoveDocument();
            }
        },

        _renderPrints: function () {
            /// <summary>
            /// Hiển thị phiếu in
            /// </summary>
            var that = this,
                printItem,
                leng;
            if (egov.isMobile) {
                return;
            }

            if (that.document.model.get("CategoryBusinessId") != 4) {
                that.printList = that.$(".ddl-printListXlvb");
            } else {
                that.printList = that.$(".ddl-printList");
            }

            egov.request.getPrintByDocCopys({
                data: {
                    docCopyIds: JSON.stringify([that.document.model.get("DocumentCopyId")]),
                    docTypeId: that.document.model.get("DocTypeId")
                },
                success: function (result) {
                    leng = result.length;
                    if (leng > 0) {
                        _.each(result, function (print) {
                            printItem = parseListItem(print.Name, print.Id, 'icon-print', function (printId) {
                                that._openPrint(printId);
                            });
                            that.printList.append(printItem);
                        });
                    }
                }
            });
        },

        _openPrint: function (printId) {
            var that,
                printView,
                documentCopyId;

            that = this;
            documentCopyId = that.document.model.get("DocumentCopyId");
            if (documentCopyId == 0) {
                that.document.printReceiptShortKey(true, printId);
                return;
            }

            require(["print"], function (printView) {
                printView = new printView({
                    docCopyId: [parseInt(documentCopyId)],
                    docId: that.document.model.get("DocumentId"),
                    paperAddIds: "",
                    feeAddIds: "",
                    displayPreview: true,
                    printId: printId
                });
            });
        },

        _confirmTransferOrProcess: function (e) {
            var that = this;
            require(['confirmTransferOrProcess'], function (ConfirmTransferOrProcessView) {
                new ConfirmTransferOrProcessView({
                    docCopyId: that.document.model.get("DocumentCopyId"),
                    isTransfer: true
                });
            });
        },

        _renderMailTemplate: function () {
            /// <summary>
            /// Hiển thị  các mẫu gửi mail tới người dân
            /// </summary>
            var that = this;
            if (!this.viewIcon.viewIconSendMail || this.isLoadedMailTmp)
                return;

            this.$mailTmpList = this.$(".sendMails");
            this.$mailTmpList.append($('<li>').html(egov.helper.loading));
            egov.request.getMailTemplates({
                data: { documentCopyId: that.document.model.get("DocumentCopyId") },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }

                    that.isLoadedMailTmp = true;
                    if (result.length > 0) {
                        that.$mailTmpList.addClass('active');
                        _.each(result, function (mail) {
                            var mailItem = parseListItem(mail.Name, mail.TemplateId, 'icon-mail2', function () {
                                that._getTempMail(mail.TemplateId, mail.Name);
                            }, true);

                            that.$mailTmpList.append(mailItem);
                        });
                    }
                },
                complete: function () {
                    that.$mailTmpList.find('.loading').parent().remove();
                }
            });
        },

        _getTempMail: function (templateId, templateName) {
            /// <summary>
            /// Hiển thị  các mẫu gửi mail tới người dân
            /// </summary>
            ///<param name="templateId">Id mẫu gửi sms<param>
            ///<param name="templateName">Tên mẫu gửi sms<param>
            if (!this.viewIcon.viewIconSendMail)
                return;

            this.document.getTempMailOrSms(templateId, templateName, true);
        },

        _renderSmsTemplate: function () {
            /// <summary>
            /// Hiển thị  các mẫu gửi mail tới người dân
            /// </summary>
            if (!this.viewIcon.viewIconSendSms || this.isLoadedSmsTmp)
                return;

            var that = this;
            this.$sendSmsList = this.$(".sendSmss");
            this.$sendSmsList.append($('<li>').html(egov.helper.loading));
            egov.request.getSmsTemplates({
                data: { documentCopyId: that.document.model.get("DocumentCopyId") },
                success: function (result) {
                    if (result.error) {
                        egov.pubsub.publish(egov.events.status.error, result.error);
                        return;
                    }

                    that.isLoadedSmsTmp = true;
                    if (result.length > 0) {
                        that.$sendSmsList.addClass('active');
                        _.each(result, function (sms) {
                            var smsItem = parseListItem(sms.Name, sms.TemplateId, 'icon-phone', function () {
                                that._getTempSms(sms.TemplateId, sms.Name);
                            }, true);

                            that.$sendSmsList.append(smsItem);
                        });
                    }
                },
                complete: function () {
                    that.$sendSmsList.find('.loading').parent().remove();
                }
            });
        },

        _getTempSms: function (templateId, templateName) {
            /// <summary>
            /// Hiển thị  các mẫu gửi sms tới người dân
            /// </summary>
            ///<param name="templateId">Id mẫu gửi sms<param>
            ///<param name="templateName">Tên mẫu gửi sms<param>
            if (!this.viewIcon.viewIconSendSms)
                return;

            this.document.getTempMailOrSms(templateId, templateName, false);
        },

        _saveDraft: function () {
            var doc = this.document;
            doc.saveDocument(function (result) {

                egov.pubsub.publish(egov.events.status.success, egov.resources.document.saveSuccess);

                var docInWaitingPacket = _.find(egov.waitingPacket, function (item) {
                    return item.attachment.get("Id") == doc.attachmentIdInWaitingPacket;
                });

                //Chuyển văn bản đã lưu xuống cuối danh sách chờ xử lý theo lô
                if (docInWaitingPacket) {
                    doc.tab.close(true);
                    docInWaitingPacket.saved = true;
                    docInWaitingPacket.documentCopyId = result.documentCopyId;
                    docInWaitingPacket.title = doc.model.get("Compendium");
                    egov.waitingPacket.remove(docInWaitingPacket);
                    egov.waitingPacket.push(docInWaitingPacket);
                    return;
                }

                doc.tab.close(true);

                if (doc.model.get("CategoryBusinessId") === egov.enum.categoryBusiness.vbden) {
                    egov.views.home.tab.addDocument(doc.model.get("DocTypeId"), doc.model.get("DocTypeName"));
                }
                location.reload();
            });
        },
        _keyClicking: function () {
            $('#modalKey').show();
            var instanceKey = this.document.cid
            if (CKEDITOR.instances[instanceKey + "_explicit_template"] != undefined) {
                var ckVal = CKEDITOR.instances[instanceKey + "_explicit_template"].getData();
                $('#lsKey tbody').html('');
                // var datatable = $('#lsKey');
                var lsKey = ckVal.match(/\$\$(.*?)\$\$/gm);
                var html = "";
                if (lsKey != null) {
                    lsKey.forEach((e, i) => {
                        html += `<tr class="row" id="rowDelete${i + 1}"><td class="col-md-2" >${i + 1}</td><td class="col-md-10" >${e}</td><td class="col-md-4" style="text-align:center"><button type="button" class="btn btn-danger deleteKey" >Xóa</button></td></tr>`
                    });
                    if (html != "") {
                        $('#lsKey tbody').append(html);
                        $('#docCid').remove()  
                        $('#lsKey').append("<input type='hidden' value='" + instanceKey + "_explicit_template' id='docCid'>");
                    }
                }
            }
        },
        _phatHanhTHSoLieuVaKetThuc: function (e) {
            var _actionId,
                    that = this;
            var _docCopyId = that.document.model.get('DocumentCopyId');
            var _workflowId = that.document.model.get('WorkflowId');

            var dinhChinhShow = "<strong>Bạn có muốn phát hành không?</strong>"

            egov.pubsub.publish(egov.events.status.processing, "Đang xử lý");
            //var bmmDialog = "<div style='width: 100%; height: 100%;'><iframe style='width: 100%; height: 100%; border:none;' src='http://quanlynhiemvu.langson.gov.vn/Views/CreateMission.aspx?callBackSuccessful=GetListMissions'></iframe><div>";
            var $el = $(dinhChinhShow);
            $el.dialog({
                title: "Phát hành",
                height: 'auto',
                width: 600,
                buttons: [
                        {
                            text: "Có",
                            className: 'btn-success',
                            click: function () {
                                that._sendPublish(_docCopyId, _workflowId);
                                $el.dialog('destroy');
                            }
                        },
                        {
                            text: egov.resources.common.closeButton,
                            className: 'btn-close',
                            click: function () {
                                $el.dialog('destroy');
                            }
                        }
                ]
            });         
        },
        _sendPublish: function (_docCopyId, _workflowId) {

            var that = this,
                 doc = that.document.serialize();
            egov.request.fiAndPub({
                data: {
                    documentCopyId: _docCopyId,
                    storePrivateId: doc.StoreId,
                    comment: "Đã phát hành và kết thúc",
                    isThongBao: false
                },
                success: function (finishResult) {
                    if ( finishResult.success ) {                
                        that._closeTab(_docCopyId);
                        that.document._reloadDocumentTree(_docCopyId);
                        egov.pubsub.publish(egov.events.status.success, "Phát hành thành công.");
                    } else  {
                       
                        egov.pubsub.publish(egov.events.status.error, finishResult.message);
                    }
                },
                error: function () {
                }
            }); 
        },
        _dinhchinh: function () {
            var destination = {},
                transferType = egov.enum.transferType,
               targetComments = [],
                that = this;
            egov.request.getAction({
                data: { documentCopyId: _docCopyId },
                success: function (result) {
                    var leng = result.length;
                    //var _actionId = result[leng - 1];
                    var actionArr = $.grep(result, function (v) {
                        return v.id === "ChuyenNguoiKhoiTao";
                    });
                    egov.request.getUserByAction({
                        data: {
                            actionId: actionArr.id,
                            workflowId: _workflowId,
                            documentCopyId: _docCopyId,
                            userId: egov.userId
                        },
                        success: function (result) {
                            targetComments.push({
                                label: result[0].fullname,
                                department: result[0].department,
                                value: '',
                                type: transferType.xulychinh
                            });
                            //alert("oke" + JSON.stringify(result));
                            destination.UserIdXlc = result[0].value;
                            destination.UserIdsDxl = [];
                            destination.IsThongbao = false;
                            destination.IsDxl = true;
                            destination.IsAttachYk = false;
                            destination.UserIdsDg = []; // this._getUserConsults();
                            destination.UserTb = [];
                            destination.NextNodeId = that.document.documentInfo.get("NodeCurrentId")  + 1;
                            destination.CurrentNodeId = that.document.documentInfo.get("NodeCurrentId");
                            destination.WorkflowId = that.document.model.get('WorkflowId');
                            destination.NewDocTypeId = that.document ? that.document.newDoctype : undefined;
                            destination.TargetComment = JSON.stringify(targetComments);
                            destination.ActionId = _actionId.id;
                            destination.ExtInfo = "";
                            destination.PublishPlanId = that.document ? that.document.publishPlanId : 0;

                            //publishQD
                            that._publishQD(destination, _docCopyId);

                        },
                        error: function (err) {
                            console.log("Lỗi khi lấy wordflowId "+ err);
                        }
                    });
                },
                error: function (err) {
                    egov.pubsub.publish(egov.events.status.error, "Lỗi khi lấy actionId " + err);
                }
            });
        },
        _publishQD: function (_destination, _docCopyId) {
            var that = this;
            var doc = this.document.serialize();
            if (this.storeId && this.code) {
                doc.StoreId = this.storeId;
                doc.DocCode = this.code;
                doc.CodeId = this.codeId;
            }
            var selectedFiles = {};

            // File mới
            this.document.attachments.model.each(function (file) {
                if (file.get('isNew') && !file.get('isRemoved')) {
                    selectedFiles[file.get('Id')] = { name: file.get('Name') }
                }
            });

            var removeFiles = this.document.attachments.model.select(function (file) {
                return file.get('isRemoved');
            });
            removeFiles = _.map(removeFiles, function (f) { return f.get('Id'); });

            var modifiedFiles = this.document.attachments.modifiedFiles;

            // Cập nhật nội dung file đã sửa với những file vừa upload lên.
            // Đồng thời xóa file đó trong danh sách file đang chỉnh sửa.
            $.each(selectedFiles, function (keyname, value) {
                if (modifiedFiles[keyname]) {
                    value.content = modifiedFiles[keyname];
                    delete modifiedFiles[keyname];
                }
            });

            //Dự kiến chuyển
           var destinationPlan = this.document.getDestinationPlan();
            if (destinationPlan) {
                destinationPlan = JSON.stringify(destinationPlan);
            }
            egov.request.publishAndFinish({
                data: {
                    "doc": JSON.stringify(doc),
                    "destination": _destination ? JSON.stringify(_destination) : "",
                    "files": JSON.stringify(selectedFiles),
                    "modifiedFiles": JSON.stringify(modifiedFiles),
                    "removeAttachmentIds": removeFiles,
                    "storePrivateId": doc.StoreId == null ? 0 : doc.StoreId,
                    "destinationPlan": destinationPlan
                },
                success: function (data) {
                    egov.request.fiAndPub({
                      data: {
                          documentCopyId: _docCopyId,
                          storePrivateId: doc.StoreId,
                          comment: "Đã phát hành và kết thúc",
                          isThongBao: false
                      },
                      success: function (finishResult) {
                          if (finishResult.error || finishResult == null) {
                              egov.pubsub.publish(egov.events.status.error, egov.resources.document.finish.error);
                          } else {
                              egov.pubsub.publish(egov.events.status.success, "Phát hành thành công.");
                              that._closeTab(_docCopyId);
                              that.document._reloadDocumentTree(_docCopyId);
                          }
                      },
                      error: function () {
                      }
                  });                 
                },
                error: function (err) {
                    egov.pubsub.publish(egov.events.status.error, "Lỗi khi phát hành " + err);
                }
            });
        },
        _closeTab: function (documentCopyIds) {
            var that = this,
              key = "Normal",
              doc,
              destinationPlan,
              selectedFiles,
              docCopyId = that.document.model.get("DocumentCopyId"),
              isOnlyDxls,
              newContent,
              newCompendium;

            var closeTab = function () {

                that.document.isSaveChanged = true;
                if (key == 'TiepNhanHoSoVaTiepTuc') {
                    egov.pubsub.publish(egov.events.status.success, 'Tiếp nhận hồ sơ thành công.');
                } else {
                    if (that.document.isPopUp) {
                        return;
                    }
                    // close tab
                    that.document.$el.removeClass("display");
                    if (!egov.isMobile) {
                        that.document.tab.display(false);
                        egov.views.home.tab.activeTab(0);
                    }
                    //that._closeTab([docCopyId]);
                    var relationId;
                    if (doc && doc.TransferTypeInEnum == "TraLoi") {
                        relationId = parseInt(doc.relationAnswerId);
                        egov.views.home.tab.closeTab([relationId]);
                    }

                    // reload lại cây văn bản
                    that._reloadDocumentList( (relationId && isNaN(relationId)) ? [that.document.model.get("DocumentCopyId"), relationId] : [that.document.model.get("DocumentCopyId")]);

                    egov.callback(that.callback);
                }
            };
            ///<summay>
            /// Đóng tab văn bản khi bàn giao thành công
            ///<para name="documentCopyIds"> Có thể là mảng chứa danh sách id văn bản hoặc id văn bản</para>
            ///</summay>
            if (this.document) {
                this.document.isSaveChanged = true;
                if (this.document.tab) {
                    this.document.tab.close2(false);
                }
            }
            else if (egov.views.home && egov.views.home.tab) {
                if (!documentCopyIds) {
                    return;
                }

                if (!(documentCopyIds instanceof Array)) {
                    documentCopyIds = [documentCopyIds];
                }

                egov.views.home.tab.closeTab(documentCopyIds);
            }
        },
        showDinhChinh: function () {

            
        },
        _dinhChinhSoLieu: function() {
            alert("oke");
        },

        _saveDocument: function () {
            var doc = this.document;
            doc.updateDocument(function () {
                // doc.tab.close(true);
            });
        },

        _editDocInfo: function () {
            egov.views.home.tab.openDocument(this.document.model.get("DocumentCopyId"), this.document.model.get("Compendium"));
        },

        _duplicateDocument: function () {
            var that,
                doc;

            that = this;
            doc = this.document;

            if (doc.model.get("DocTypeId")) {
                egov.views.home.tab.duplicateDocument(doc.model);
            }
        },

        _businessLicense: function (e) {
            if (this.checkDisable(e)) {
                return;
            }

            this.document._businessLicense();
        },

        _importInvoice: function (e) {
            if (this.checkDisable(e)) {
                return;
            }

            this.document._importInvoice();
        },

        _exportExcel: function (e) {
            if (this.checkDisable(e)) {
                return;
            }

            this.document._exportExcel();
        },
        _exportPdf: function (e) {
            this.document._exportPdf();
        },
        _exportDoc: function (e) {
            this.document._exportDoc();
        },
        _importExcel: function (e) {
            if (this.checkDisable(e)) {
                return;
            }

            this.document._importExcel();
        },
        _viewLeaf: function () {

            this.document.showLeaf();
        },
        _importWord: function (e) {
            if (this.checkDisable(e)) {
                return;
            }

            this.document._importWord();
        },
            _surveyReleased: function (e) {
                var that = this;
                egov.views.survey._onReleased(that.document, that.actionSurvey.length > 0 ? that.actionSurvey[0] : {});
            },
            _surveyPDF: function (e) {
                var that = this;
                var fileName = that.document.model.get("Compendium") + ".pdf";
                var data = JSON.parse(that.document.model.get("Note") || '{}');
                egov.views.survey._saveSurveyToPdf(fileName, data);
            },

        _undoFinish: function (e) {
            if (this.checkDisable(e)) {
                return;
            }
            var r = confirm("Bạn có muốn lấy lại văn bản?");
            if (r == false) {
                return;
            }
            var doc = this.document;
            this.document.undoFinish(doc.model.get("DocumentCopyId"));
        },

        _rePublish: function (e) {
            if (this.checkDisable(e)) {
                return;
            }

            var doc = this.document;
            this.document.rePublish(doc.model.get("DocumentCopyId"));
        },

        //#endregion

        //#region Calendar

        _bookCalendar: function () {
            var title = this.document.model.get("Compendium");
            var organ = this.document.model.get("Organization");
            var dialog = $("<div>");
            dialog.dialog({
                height: '350px',
                width: '900px',
                draggable: true,
                title: "Tạo lịch họp",
                buttons: [
                    {
                        text: "Tạo lịch",
                        className: 'btn-primary',
                        click: function () {
                            dialog.find("#createCalendar")[0].contentWindow.createFromDocument(function () {
                                egov.pubsub.publish(egov.events.status.success, "Tạo lịch thành công.");
                                dialog.dialog("hide");
                                dialog.remove();
                            });
                        }
                    },
                    {
                        text: "Bỏ qua",
                        className: 'btn-default',
                        click: function () {
                            dialog.dialog("hide");
                            dialog.remove();
                        }
                    },
                ]
            });
            dialog.html('<iframe src="/Calendar/IndexFromDocument" id="createCalendar" style="width: 870px; height: 344px; border:none"></iframe>');
            var calendarIframe = dialog.find('#createCalendar')[0];
            calendarIframe.onload = function () {
                $(calendarIframe.contentDocument).find(".title").val(title);
                $(calendarIframe.contentDocument).find(".department").val(organ);
            };
        }

        //#endregion
    });

    //#region Private Methods

    function parseListItem(name, value, icon, callback, isAllow) {
        var result;
        if (egov.isMobile) {
            if (typeof value == "string" && "luusonoiboluusovaphathanhnoiboluusovaphathanhrangoai".indexOf(value.toString().toLowerCase()) > -1) {
                return;
            }
            result = $.tmpl('<li class="mdl-menu__item"><a href="#" value="${value}"><span class="icon ${icon}"></span>${name}</a>', { value: value, name: name, icon: icon });
        }
        else {
            result = $.tmpl('<li><a href="#" value="${value}"><span class="icon ${icon}"></span>${name}</a>', { value: value, name: name, icon: icon });
        }

        if (!isAllow) {
            result.attr('disabled', 'disabled');
        }

        result.on("click", function (e) {
            if (typeof callback === "function") {
                callback(value, e);
            }
        });

        return result;
    }

    function containFlags(combined, checkagainst) {
        ///<summary> Kiểm tra xem phần tử combined có chứa checkagainst hay không</summary>
        return ((combined & checkagainst) === checkagainst);
    }

    function isLienThongError(document) {
        var model = document.model.toJSON();
        ///<summary> kiểm tra xem văn bản có phải là văn bản liên thông bị gửi nhầm hay không</summary>
        if (model.CategoryBusinessId = 1 && model.DocTypeId == "00000000-0000-0000-0000-000000000000" && model.Original == 2) {
            return true;
        } else {
            return false;
        }
    }

    function _getTransferIcon(actionId) {
        var actionSpecials = egov.enum.actionSpecial;
        switch (actionId) {
            case actionSpecials.luuSoVaPhatHanhNoiBo.name:
            case actionSpecials.luuSoVaPhatHanhRaNgoai.name:
                return "icon-archive";
            case actionSpecials.luuSoNoiBo.name:
                return "icon-disk2";
            case actionSpecials.chuyenYKienDongGopVbDxl.name:
            case actionSpecials.chuyenYKienDongGopVbXinYKien.name:
                return "icon-comment";
            case actionSpecials.chuyenNguoiGui.name:
            case actionSpecials.chuyenNguoiKhoiTao.name:
            case actionSpecials.chuyenNguoiCoQuyenDongGopYKien.name:
                return "icon-back";
            default:
                return "icon-forward4";
        }
    }

    //#endregion

    //Các hàm xử lý sự kiện trên toolbar

    //Loại bỏ văn bản
    function removeDocument(docCopyIds) {
        //todo:xem lại hủy văn bản

        //Todo:xem lại ham đóng tab và reload lại trang
        window.parent.egov.cshtml.home.tab.closeActiveTab();
        window.parent.egov.cshtml.home.reloadData(true);
    }

    //Hàm lưu văn bản khi click nút lưu văn bản
    function openSaveDocument(documentCopyId) {
        var dialog = window.parent.egov.views.base.dialog,
            token = $("input[name='__RequestVerificationToken']", "#DocumentSaveDocumentToStorePrivate").val();

        $.get('/StorePrivate/GetStoreActive',
            {},
            function (result) {
                if (result) {
                    if (result.storePrivate.length > 0 || result.storeShare.length > 0) {
                        //to do: xem lại hàm lấy hồ sơ
                        window.parent.egov.cshtml.home.tree.storeprivate.openDialogSave(result, function (selectedId) {
                            $.post('/Document/SaveDocumentToStorePrivate',
                                {
                                    storePrivateId: selectedId,
                                    documentCopyId: documentCopyId,
                                    __RequestVerificationToken: token
                                }, function (data) {
                                    if (data) {
                                        if (data.error) {
                                            // egov.message.notification(data.message, egov.message.messageTypes.error);
                                            egov.pubsub.publish(egov.events.status.error, data.message);
                                        } else {
                                            // egov.message.notification('Lưu hồ sơ thành công', egov.message.messageTypes.success);
                                            egov.pubsub.publish(egov.events.status.success, 'Lưu hồ sơ thành công');

                                            dialog.close();
                                        }
                                    }
                                });
                        }, function () {
                            dialog.close();
                        });
                    } else {
                    }
                }
            }
        ).fail(function () {
            // egov.message.notification('Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân', egov.message.messageTypes.error);
            egov.pubsub.publish(egov.events.status.error, 'Có lỗi xảy ra khi tải danh sách hồ sơ cá nhân');
        });
    }

    //Hàm xử lý upload nhiều file ảnh
    function addMultiDocument(allowTransferMultiDocument, frame) {
        if (allowTransferMultiDocument) {
            egov.cshtml.home.transferMultiDocument.add(frame, egov.cshtml.document.doctypeId, egov.cshtml.document.isHoso, acceptFileTypes, fullname, username);
        }
    }

    return DocumentToolBar;
});