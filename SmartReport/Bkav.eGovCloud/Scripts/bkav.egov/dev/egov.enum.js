(function (egov) {

    egov.enum = {

        categoryBusiness: {
            vbden: 1,
            vbdi: 2,
            hsmc: 4,
            kntc: 8,
        },

        urgents: {
            thuong: 1,
            khan: 2,
            hoatoc: 3
        },

        securityLevel: {
            thuong: 1,
            mat: 2,
            toimat: 3
        },

        transferType: {
            xulychinh: 1,
            dongxuly: 2,
            thongbao: 3,
            xyk: 4,
            giamsat: 5
        },

        documentTransferType: {
            taoMoiThongThuong: 1,
            banGiaoThongThuong: 2,
            banGiaoKhiTraLoi: 4,
            banGiaoKhiPhanLoai: 8
        },

        documentListSize: {
            small: 0,
            medium: 1,
            large: 2
        },

        documentViewType: {
            'default': 0,
            preView: 1,     //HIển thị văn bản hồ sơ ở khung preview (Người dung thiết lập hiển  thị toàn bộ thông tin văn bản hồ sơ ở khung xem trước văn bản)
            dialog: 2        //Hiển thị trên văn bản hồ sơ khi hiện dialog khi click 'Chi tiết văn bản ' ở contextmenu
        },

        quickViewType: {
            hide: 0,   ///Không hiển thị tóm tắt văn bản
            right: 1,   //Hiển thị tóm tắt văn bản bên phải
            below: 2   //Hiển thị tóm tắt văn bản bên dưới
        },

        documentOriginal: {
            egov: 0,
            egovOnline: 1,
            other: 2
        },

        fontSizeType: {
            nho: 0,  //Chữ nhỏ
            vua: 1,  //Chữ vừa
            lon: 2   //Chữ lớn
        },

        searchType: {
            document: 1, //Tìm văn bản.
            file: 2       //Tìm trong file.
        },

        processFilterType: {
            group: 1,
            equal: 2,
            custom: 3
        },

        documentStatus: {
            DuThao: 1,
            DangXuLy: 2,
            KetThuc: 4,
            LoaiBo: 8,
            DungXuLy: 16
        },

        permission: {
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
            doihanxulykhiphanloai: 268435456,
            molaivanban: 536870912,
            danhlaisoden: 1073741824,
            xoavanbankhoihoso: 2147483648
        },

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
            tiepNhanBoSung: { name: 'TiepNhanBoSung', value: 13 },
            lienThong: { name: 'LienThong', value: 14 },
            transferMultiple: { name: 'TransferMultiple', value: 15 }
        },

        commentType: {
            Common: 1,
            Consulted: 2,
            Contribution: 3,
            Supplementary: 4,
            Signed: 5,
            Success: 6,
            Finished: 7
        },

        formType: {
            html: 1,
            dynamic: 2,
            fromUrl: 3
        },

        language: {
            VietNam: 1,
            Laos: 2
        },

        defaultToolbar: {
            Create: 1,
            Edit: 2,
            InsertImagePacket: 3
        },

        commonTemplate: {
            InBienNhanBanGiao: 1
        }
    };

    //#region HSMC

    egov.enum.feeType = {
        TiepNhan: 1,
        ThuongBosung: 2,
        TraCongDan: 3
    };

    egov.enum.paperType = {
        TiepNhan: 1,
        ThuongBosung: 2,
        TraCongDan: 3
    };

    egov.enum.supplementaryType = {
        renew: 1,
        "continue": 2,
        add: 3
    };

    egov.enum.printProcessType = {
        TiepNhan: 1,

        BanGiao: 2,

        KyDuyet: 4,

        TraKetQua: 8,

        TiepNhanBoSung: 16,

        GiaHan: 32
    };

    //#endregion


})(this.egov = this.egov || {});