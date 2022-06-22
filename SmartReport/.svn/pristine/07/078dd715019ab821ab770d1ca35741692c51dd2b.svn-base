(function (egov, $, _, undefined) {
    if (typeof ($) === 'undefined') {
        throw 'Thư viện này sử dụng jQuery, hãy tải thư viện jQuery trước khi sử dụng';
    }
    if (typeof (_) === 'undefined') {
        throw 'Thư viện này sử dụng Underscore, hãy tải thư viện Underscore trước khi sử dụng';
    }

    //Trạng thái văn bản
    //var documentStatus = {
    //    duthao: 1,
    //    dangxuly: 2,
    //    ketthuc: 4,
    //    loaibo: 8,
    //    dungxuly: 16
    //};

    //Vị trí của văn bản/hồ sơ
    //var documentPositions = {
    //    duthao: 1,
    //    choxuly: 2,
    //    ketthuc: 4,
    //    loaibo: 8,
    //    dungxuly: 16,
    //    theodoi: 32,
    //    thongbao: 64,
    //    uyquyen: 128,
    //    chophathanh: 256
    //};

    if (!egov.document) {
        egov.document = {};
    }

    egov.document.permission = {
        //Các nghiệp vụ được phép tác động lên văn bản/hồ sơ
        documentPermissions: {
            khoitaovanban: 1,
            xemvanban: 2,
            dinhkem: 4,
            suavanban: 8,
            guiykien: 16,
            bangiao: 32,
            thongbao: 64,
            xinykien: 128,
            phanloai: 256,
            traloivanban: 512,
            laylaivanban: 1024,
            xacnhanbangiao: 2048,
            xacnhanxuly: 4096,
            yeucaubosung: 8192,
            tiepnhanbosung: 16384,
            kyduyet: 32768,
            traketqua: 65536,
            giahanxuly: 131072,
            dungxuly: 262144,
            ketthucxuly: 524288,
            huyvanban: 1048576,
            luuhosocanhan: 2097152,
            luuso: 4194304,
            phathanh: 8388608,
            capnhatketquaxulycuoi: 16777216,
            luuvanban: 33554432,
            traloiykien: 67108864,
            capphep: 134217728,
            doihanxulykhiphanloai: 268435456
        },
        //Các hướng chuyển đặc biệt
        actionSpecial: {
            thongThuong: { name: 'ThongThuong', value: 0 },
            luuSoVaPhatHanhNoiBo: { name: 'LuuSoVaPhatHanhNoiBo', value: 1 },
            luuSoNoiBo: { name: 'LuuSoNoiBo', value: 2 },
            luuSoVaPhatHanhRaNgoai: { name: 'LuuSoVaPhatHanhRaNgoai', value: 3 },
            chuyenNguoiKhoiTao: { name: 'ChuyenNguoiKhoiTao', value: 4 },
            chuyenYKienDongGopVbDxl: { name: 'ChuyenYKienDongGopVbDxl', value: 5 },
            tiepTucXuLy: { name: 'TiepTucXuLy', value: 6 },
            chuyenNguoiCoQuyenDongGopYKien: { name: 'ChuyenNguoiCoQuyenDongGopYKien', value: 7 },
            tiepNhanHoSo: { name: 'TiepNhanHoSo', value: 8 },
            tiepNhanHoSoVaTiepTuc: { name: 'TiepNhanHoSoVaTiepTuc', value: 9 },
            capNhatKetQuaDungXuLy: { name: 'CapNhatKetQuaDungXuLy', value: 10 },
            chuyenYKienDongGopVbXinYKien: { name: 'ChuyenYKienDongGopVbXinYKien', value: 11 },
            chuyenNguoiGui: { name: 'ChuyenNguoiGui', value: 12 },
            tiepNhanBoSung: { name: 'TiepNhanBoSung', value: 13 }
        }
    };

    //Đối tượng Context menu
    var objContextmenu = {
        'xemvanban': {
            className: 'xemvanbanClass',
            name: 'Xem văn bản',
            icon: 'document.png',
            commandName: 'xemvanban'
        },
        'suavanban': {
            className: 'suavanbanClass',
            name: 'Sửa văn bản',
            icon: 'edit.png',
            commandName: 'suavanban'
        },
        'guiykien': {
            className: 'guiykienClass',
            name: 'Gửi ý kiến',
            icon: 'new_comment.png',
            commandName: 'guiykien'
        },
        'xinykien': {
            className: 'xinykienClass',
            name: 'Xin ý kiến',
            icon: 'Speechbubbles.png',
            commandName: 'xinykien'
        },
        'bangiao': {
            className: 'bangiaoClass',
            name: 'Bàn giao',
            icon: 'transfer.png',
            commandName: 'bangiao'
        },
        'thongbao': {
            className: 'thongbaoClass',
            name: 'Thông báo',
            icon: 'anouncement.png',
            commandName: 'thongbao'
        },
        'laylaivanban': {
            className: 'laylaivanbanClass',
            name: 'Lấy lại văn bản',
            icon: 'retransfer.png',
            commandName: 'laylaivanban'
        },
        'xacnhanbangiao': {
            className: 'xacnhanbangiaoClass',
            name: 'Xác nhận bàn giao',
            icon: 'transfer.png',
            commandName: 'xacnhanbangiao'
        },
        'xacnhanxuly': {
            className: 'xacnhanxulyClass',
            name: 'Xác nhận xử lý',
            icon: 'transfer.png',
            commandName: 'xacnhanxuly',
        },
        'yeucaubosung': {
            className: 'yeucaubosungClass',
            name: 'Yêu cầu bổ sung',
            icon: 'yeucaubosung',
            commandName: 'yeucaubosung'
        },
        'tiepnhanbosung': {
            className: 'tiepnhanbosungClass',
            name: 'Tiếp nhận bổ sung',
            icon: 'tiepnhanbosung',
            commandName: 'tiepnhanbosung'
        },
        'kyduyet': {
            className: 'kyduyetClass',
            name: 'Ký duyệt',
            icon: 'kyduyet',
            commandName: 'kyduyet'
        },
        'ketthucxuly': {
            className: 'ketthucxulyClass',
            name: 'Kết thúc xử lý',
            icon: 'ketthucxuly',
            commandName: 'ketthucxuly'
        },
        'huyvanban': {
            className: 'huyvanbanClass',
            name: 'Hủy văn bản',
            icon: 'cancel.png',
            commandName: 'huyvanban'
        },
        'capnhatketquaxulycuoi': {
            className: 'capnhatketquaxulycuoiClass',
            name: 'Cập nhật kết quả xử lý cuối',
            icon: 'capnhatketquaxulycuoi',
            commandName: 'capnhatketquaxulycuoi'
        },
        'inphieutrinh': {
            className: 'inphieutrinhClass',
            name: 'In phiếu trình lãnh đạo',
            icon: 'print.png',
            commandName: 'inphieutrinh'
        },
        'intomtat': {
            className: 'intomtatClass',
            name: 'In tóm tắt',
            icon: 'print.png',
            commandName: 'intomtat'
        },
        'capnhattiendo': {
            className: 'capnhattiendoClass',
            name: 'Cập nhật tiến độ',
            icon: 'capnhattiendo',
            commandName: 'capnhattiendo'
        },
        'xoakhoiduthao': {
            className: 'xoakhoiduthaoClass',
            name: 'Xóa văn bản dự thảo',
            icon: 'xoakhoiduthao',
            commandName: 'xoakhoiduthao'
        },
        'contextheodoi': {
            className: 'contextheodoiClass',
            name: 'Fix contextmenu theo dõi',
            icon: 'contextheodoi',
            commandName: 'contextheodoi'
        },
        'dungxuly': {
            className: 'dungxulyClass',
            name: 'Dừng xử lý',
            icon: 'dungxuly',
            commandName: 'dungxuly'
        },
        'giahanxuly': {
            className: 'giahanxulyClass',
            name: 'Gia hạn xử lý',
            icon: 'giahanxuly',
            commandName: 'giahanxuly'
        }
    };

    var getPermissions = function (permissionValue, docCopyIdSelected, docCopyIdClick) {
        var itemsContext = {};

        if (permissionValue & egov.document.permission.documentPermissions.suavanban) {
            $.extend(itemsContext, { 'suavanban': objContextmenu['suavanban'] });
        }

        if (permissionValue & egov.document.permission.documentPermissions.guiykien) {
            $.extend(itemsContext, { 'guiykien': objContextmenu['guiykien'] });
            objContextmenu.guiykien.callback = function () {
                openSendCommentDialog(docCopyIdClick);
            };
        }

        if (permissionValue & egov.document.permission.documentPermissions.bangiao) {
            $.extend(itemsContext, { 'bangiao': objContextmenu['bangiao'] });
        }

        if (permissionValue & egov.document.permission.documentPermissions.thongbao) {
            $.extend(itemsContext, { 'thongbao': objContextmenu['thongbao'] });
            objContextmenu.thongbao.callback = function () {
                openAnnouncement(docCopyIdClick);
            };
        }

        if (permissionValue & egov.document.permission.documentPermissions.xinykien) {
            $.extend(itemsContext, { 'xinykien': objContextmenu['xinykien'] });
            objContextmenu.xinykien.callback = function () {
                openConsultContext(docCopyIdClick);
            };
        }

        if (docCopyIdSelected.length == 1) {
            if (permissionValue & egov.document.permission.documentPermissions.xacnhanbangiao) {
                $.extend(itemsContext, { 'xacnhanbangiao': objContextmenu['xacnhanbangiao'] });
                objContextmenu.xacnhanbangiao.callback = function () {
                    openConfirmTransferContext(docCopyIdClick);
                };
            }
            if (permissionValue & egov.document.permission.documentPermissions.xacnhanxuly) {
                $.extend(itemsContext, { 'xacnhanxuly': objContextmenu['xacnhanxuly'] });
                objContextmenu.xacnhanxuly.callback = function () {
                    openConfirmProcessContext(docCopyIdClick);
                };
            }
        }
        if (permissionValue & egov.document.permission.documentPermissions.yeucaubosung) {
            $.extend(itemsContext, { 'yeucaubosung': objContextmenu['yeucaubosung'] });
        }

        if (permissionValue & egov.document.permission.documentPermissions.tiepnhanbosung) {
            $.extend(itemsContext, { 'tiepnhanbosung': objContextmenu['tiepnhanbosung'] });
        }

        if (permissionValue & egov.document.permission.documentPermissions.kyduyet) {
            $.extend(itemsContext, { 'kyduyet': objContextmenu['kyduyet'] });
            objContextmenu.kyduyet.callback = function () {
                openKyduyet(docCopyIdClick);
            };
        }

        if (permissionValue & egov.document.permission.documentPermissions.traketqua) {
            // right flag is set
        }

        if (permissionValue & egov.document.permission.documentPermissions.giahanxuly) {
            $.extend(itemsContext, { 'giahanxuly': objContextmenu['giahanxuly'] });
            objContextmenu.giahanxuly.callback = function () {
                openGiahanxuly(docCopyIdClick);
            };
        }

        if (permissionValue & egov.document.permission.documentPermissions.dungxuly) {
            $.extend(itemsContext, { 'dungxuly': objContextmenu['dungxuly'] });
        }

        if (permissionValue & egov.document.permission.documentPermissions.ketthucxuly) {
            $.extend(itemsContext, { 'ketthucxuly': objContextmenu['ketthucxuly'] });
            objContextmenu.ketthucxuly.callback = function () {
                openEndProcess(docCopyIdClick);
            };
        }

        if (permissionValue & egov.document.permission.documentPermissions.huyvanban) {
            $.extend(itemsContext, { 'huyvanban': objContextmenu['huyvanban'] });
            objContextmenu.huyvanban.callback = function () {
                openRemoveDocument(docCopyIdSelected, docCopyIdClick);
            };
        }

        if (permissionValue & egov.document.permission.documentPermissions.capnhatketquaxulycuoi) {
            $.extend(itemsContext, { 'capnhatketquaxulycuoi': objContextmenu['capnhatketquaxulycuoi'] });
        }

        if (permissionValue & egov.document.permission.documentPermissions.laylaivanban) {
            $.ajax({
                type: "GET",
                data: { documentCopyId: docCopyIdClick },
                url: "/Document/GetContextItemForUndoTransfering",
                context: $(this),
                async: false,
                success: function (data) {
                    if (data.error) {
                        eGovMessage.show(data.error);
                    }
                    else {
                        if (!$.isEmptyObject(itemsContext) && data.length > 0) {
                            $.extend(itemsContext, { separator_laylaivanban: "-----" });
                        }
                        for (var i = 0; i < data.length; i++) {
                            var dateCreated = data[i].DateCreated;
                            var contextItem = {
                                className: 'laylaivanbanClass',
                                name: data[i].Name,
                                dateCreated: dateCreated,
                                icon: 'laylaivanban',
                                commandName: 'laylaivanban' + i,
                                callback: function (key, opt) {
                                    var dateCreated1 = opt.commands[key].dateCreated;
                                    var dateCreatedStr = Globalize.format(dateCreated1, "F");
                                    openLaylaivanban(docCopyIdClick, dateCreatedStr);//dateCreated1.toUTCString()
                                }
                            };

                            var contextItemInsert = {};
                            contextItemInsert['laylaivanban' + i] = contextItem;
                            $.extend(itemsContext, contextItemInsert);
                        }
                    }
                },
                error: function () {
                    eGovMessage.notification('Có lỗi xảy ra', eGovMessage.messageTypes.error);
                }
            });
        }

        return itemsContext;
    };

    // ===================================================================================================================
    ///Các hàm thực hiện chức năng trên context menu
    ///<summary> Hàm xác nhận bàn giao trên Contextmenu </summary>
    var openConfirmTransferContext = function (docCopyIdClick) {
        var confirmTransferOrProcess = new egov.document.confirmTransferOrProcess(docCopyIdClick, undefined);
        confirmTransferOrProcess.open(true);
    };

    ///<summary>Hàm xác nhận xử lý trên Contextmenu</summary>
    var openConfirmProcessContext = function (docCopyIdClick) {
        var confirmTransferOrProcess = new egov.document.confirmTransferOrProcess(docCopyIdClick, undefined);
        confirmTransferOrProcess.open(false);
    };

    ///<summary>Hàm xử lý xin ý kiến trên Contextmenu</summary>
    var openConsultContext = function (docCopyIdClick) {
        var consult = new egov.document.consult(docCopyIdClick, undefined);
        consult.open(false);
    };

    ///<summary>Hàm xử lý phần loại bỏ văn bản - hủy văn bản</summary>
    var openRemoveDocument = function (docCopyIdSelected) {
        egov.document.removeDocument(docCopyIdSelected);
    };

    ///<summary>Hàm xử lý phần kết thúc xử lý</summary>
    var openEndProcess = function (docCopyIdClick) {
        var endProcess = new egov.document.endProcess(docCopyIdClick, undefined, undefined);
        endProcess.open();
    };

    ///<summary>Hàm xử lý chức năng thông báo</summary>
    var openAnnouncement = function (docCopyIdClick) {
        var announcement = new egov.document.announcement(docCopyIdClick, undefined);
        announcement.open(false);
    };

    ///<summary>Hàm xử lý chức năng gia hạn</summary>
    var openGiahanxuly = function (docCopyIdClick) {
        var addTime = new egov.document.addTime(docCopyIdClick, undefined);
        addTime.open();
    };

    ///<summary>Hàm xử lý chức năng ký duyệt</summary>
    var openKyduyet = function (docCopyIdClick) {
        var approver = new egov.document.approver(docCopyIdClick, undefined);
        approver.send(true);
    };

    ///<summary>Hàm xử lý chức năng lấy lại văn bản</summary>
    var openLaylaivanban = function (docCopyIdClick, dateCreated) {
        var retrieve = new egov.document.retrieve(docCopyIdClick, dateCreated);
        retrieve.open();
    };

    var openSendCommentDialog = function (docCopyIdClick) {
        var sendComment = new egov.document.sendComment(docCopyIdClick, undefined);
        sendComment.open(false);
    };


    //Các hàm xử lý giá trị DocumentPermission
    //Hàm lấy các Permission từ 1 giá trị có sẵn
    //<param name="docCopyIdSelected" type="List">Các DocumentCopyId được chọn để hiện thị contextmenu</param>
    //<param name="docCopyIdClick" type="int">DocumentCopyId được click để hiện thị contextmenu</param>
    egov.document.permission.getContextmenu = function (permissionArr, docCopyIdSelected, docCopyIdClick) {
        var result = [];
        for (var i = 0; i < permissionArr.length; i++) {
            $.each(getPermissions(permissionArr[i], docCopyIdSelected, docCopyIdClick), function (key, value) {
                //var insertItem = {};
                //insertItem[value.commandName] = value;
                result.push(value);
                //$.extend(result, insertItem);
            });
        }
        return result;
    };

    egov.document.permission.containFlags = function (combined, checkagainst) {
        return ((combined & checkagainst) === checkagainst);
    };

})(window.egov = window.egov || {}, window.jQuery, window._);