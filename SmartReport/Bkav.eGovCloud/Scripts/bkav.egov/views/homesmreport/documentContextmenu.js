define(function () {

    "use strict";

    //Đối tượng Context menu (fix cứng)
    var objContextmenu = {
        'xemvanban': {
            className: 'xemvanbanClass',
            name: egov.resources.documents.contextmenu.name.xemvanban,
            iconClass: 'icon-xemvanban',
            commandName: 'xemvanban'
        },
        'suavanban': {
            className: 'suavanbanClass',
            name: egov.resources.documents.contextmenu.name.suavanban,
            iconClass: 'icon-pencil3',
            commandName: 'suavanban'
        },
        'guiykien': {
            className: 'guiykienClass',
            name: egov.resources.documents.contextmenu.name.guiykien,
            iconClass: 'icon-comment',
            commandName: 'guiykien'
        },
        //'xinykien': {
        //    className: 'xinykienClass',
        //    name: egov.resources.documents.contextmenu.name.xinykien,
        //    iconClass: 'icon-chat',
        //    commandName: 'xinykien'
        //},
        'bangiao': {
            className: 'bangiaoClass',
            name: egov.resources.documents.contextmenu.name.bangiao,
            iconClass: 'icon-forward4',
            commandName: 'bangiao'
        },
        'thongbao': {
            className: 'thongbaoClass',
            name: egov.resources.documents.contextmenu.name.thongbao,
            iconClass: 'icon-users3',
            commandName: 'thongbao'
        },
        'laylaivanban': {
            className: 'laylaivanbanClass',
            name: egov.resources.documents.contextmenu.name.laylaivanban,
            iconClass: 'icon-back',
            commandName: 'laylaivanban'
        },
        'xacnhanbangiao': {
            className: 'xacnhanbangiaoClass',
            name: egov.resources.documents.contextmenu.name.xacnhanbangiao,
            iconClass: 'icon-checkmark',
            commandName: 'xacnhanbangiao'
        },
        'xacnhanxuly': {
            className: 'xacnhanxulyClass',
            name: egov.resources.documents.contextmenu.name.xacnhanxuly,
            iconClass: 'icon-checkmark3',
            commandName: 'xacnhanxuly',
        },
        'yeucaubosung': {
            className: 'yeucaubosungClass',
            name: egov.resources.documents.contextmenu.name.yeucaubosung,
            iconClass: 'icon-yeucaubosung',
            commandName: 'yeucaubosung'
        },
        'tiepnhanbosung': {
            className: 'tiepnhanbosungClass',
            name: egov.resources.documents.contextmenu.name.tiepnhanbosung,
            iconClass: 'icon-tiepnhanbosung',
            commandName: 'tiepnhanbosung'
        },
        'kyduyet': {
            className: 'kyduyetClass',
            name: egov.resources.documents.contextmenu.name.kyduyet,
            iconClass: 'icon-signup',
            commandName: 'kyduyet'
        },
        'ketthucxuly': {
            className: 'ketthucxulyClass',
            name: egov.resources.documents.contextmenu.name.ketthucxuly,
            iconClass: 'icon-checkmark3',
            commandName: 'ketthucxuly'
        },
        'huyvanban': {
            className: 'huyvanbanClass',
            name: egov.resources.documents.contextmenu.name.huyvanban,
            iconClass: 'icon-remove',
            commandName: 'huyvanban'
        },
        'capnhatketquaxulycuoi': {
            className: 'capnhatketquaxulycuoiClass',
            name: egov.resources.documents.contextmenu.name.capnhatketquaxulycuoi,
            iconClass: 'icon-capnhatketquaxulycuoi',
            commandName: 'capnhatketquaxulycuoi'
        },
        'inphieutrinh': {
            className: 'inphieutrinhClass',
            name: egov.resources.documents.contextmenu.name.inphieutrinh,
            iconClass: 'icon-inphieutrinh',
            commandName: 'inphieutrinh'
        },
        'intomtat': {
            className: 'intomtatClass',
            name: egov.resources.documents.contextmenu.name.intomtat,
            iconClass: 'icon-intomtat',
            commandName: 'intomtat'
        },
        'capnhattiendo': {
            className: 'capnhattiendoClass',
            name: egov.resources.documents.contextmenu.name.capnhattiendo,
            iconClass: 'icon-capnhattiendo',
            commandName: 'capnhattiendo'
        },
        'xoakhoiduthao': {
            className: 'xoakhoiduthaoClass',
            name: egov.resources.documents.contextmenu.name.xoakhoiduthao,
            iconClass: 'icon-remove',
            commandName: 'xoakhoiduthao'
        },
        'contextheodoi': {
            className: 'contextheodoiClass',
            name: egov.resources.documents.contextmenu.name.contextheodoi,
            iconClass: 'icon-contextheodoi',
            commandName: 'contextheodoi'
        },
        'dungxuly': {
            className: 'dungxulyClass',
            name: egov.resources.documents.contextmenu.name.dungxuly,
            iconClass: 'icon-dungxuly',
            commandName: 'dungxuly'
        },
        'giahanxuly': {
            className: 'giahanxulyClass',
            name: egov.resources.documents.contextmenu.name.giahanxuly,
            iconClass: 'icon-giahanxuly',
            commandName: 'giahanxuly'
        },
        'chitietvanban': {
            className: 'chitietvanbanClass',
            name: egov.resources.documents.contextmenu.name.chitietvanban,
            iconClass: 'icon-info4',
            commandName: 'chitietvanban'
        },
        'danhdauchuadoc': {
            className: 'danhdauchuadocClass',
            name: egov.resources.documents.contextmenu.name.danhdauchuadoc,
            iconClass: 'icon-eye-blocked',
            commandName: 'danhdauchuadoc'
        },
        'danhdaudadoc': {
            className: 'danhdaudadocClass',
            name: egov.resources.documents.contextmenu.name.danhdaudadoc,
            iconClass: 'icon-eye',
            commandName: 'danhdaudadoc'
        },
        'movanban': {
            className: 'movanbanClass',
            name: egov.resources.documents.contextmenu.name.movanban,
            iconClass: 'icon-eye2',
            commandName: 'movanban'
        },
        'molaivanban': {
            className: 'molaivanbanClass',
            name: egov.resources.documents.contextmenu.reOpenDocument.text,
            iconClass: 'icon-eye2',
            commandName: 'molaivanban'
        },

        'exportToExcell': {
            className: 'movanbanClass',
            name: egov.resources.documents.contextmenu.name.exportToExcell,
            iconClass: 'icon-file-excel',
            commandName: 'exportToExcell'
        },
        'exportToWord': {
            className: 'molaivanbanClass',
            name: egov.resources.documents.contextmenu.name.exportToWord,
            iconClass: 'icon-file-word',
            commandName: 'exportToWord'
        },
        'removefromstoreprivate': {
            className: 'removefromstoreprivate',
            name: egov.resources.documents.contextmenu.name.removefromstoreprivate,
            iconClass: 'icon-close',
            commandName: 'removefromstoreprivate'
        },
        'exportToXML': {
            className: 'exportToXML',
            name: 'Xuất danh sách ra tệp XML',
            iconClass: 'icon-file-xml',
            commandName: 'exportToXML'
        }
    };

    //<sumary>================ </sumary>
    //<sumary>Lấy quyền hạn xử lý văn bản hồ sơ của người dùng </sumary>
    //<sumary>Lấy quyền hạn xử lý văn bản hồ sơ của người dùng </sumary>
    var DocumentContextmenu = Backbone.View.extend({
        //Khởi tạo
        initialize: function (options) {
            this.parent = options.parent;
            return this;
        },

        getContextmenu: function (docCopyIdSelected, selectedModels, clickModel, success) {
            ///<summary>
            ///Trả về danh sách contextmenu của người dung theo van bản
            ///<para name="docCopyIdSelected">Danh sách id văn bản mà người dùng chọn</para>
            ///<para name="selectedModels">Danh sách đối tượng văn bản</para>
            ///<para name="clickModel">Văn bản mà người dùng chọn đầu tiên</para>
            ///</summary>
            this.selectedModels = selectedModels;
            this.clickModel = clickModel;
            this.docCopyIdSelected = docCopyIdSelected;
            this.docCopyIdClick = this.clickModel.model.get('DocumentCopyId');
            var _this = this,
                result = [],
                leng = this.collection.length;

            this.collection.each(function (permission, idx) {
                _this._getItemContext(permission.get('value'), function (contextMenu) {
                    $.each(contextMenu, function (key, value) {
                        //for (var i = 0; i < 6; i++) {
                        //    result.push(value);
                        //}
                        result.push(value);
                    });
                    if (idx === leng - 1) {
                        if (typeof success === 'function') {
                            success(result);
                        }
                    }
                });
            });
        },

        //Lây quyền của người dùng xem có các chức năng nào hiển thị trên contextmenu
        _getItemContext: function (permissionValue, success) {
            var _this = this,
                docCopyIdSelectedLeng = this.docCopyIdSelected.length;
            var hasGetContext = false;

            var itemsContext = {};
            // mở văn bản
            $.extend(itemsContext, { 'movanban': objContextmenu['movanban'] });

            $.extend(itemsContext, { 'exportToXML': objContextmenu['exportToXML'] });
            objContextmenu.exportToXML.callback = function () {
                _this.parent.exportToXML("XML");
            };

            objContextmenu.movanban.callback = function () {
                _this.parent.openDocuments(_this.selectedModels);
            };

            itemsContext['---'] = null;


            if (permissionValue & egov.enum.permission.xoavanbankhoihoso) {
                $.extend(itemsContext, { 'removefromstoreprivate': objContextmenu['removefromstoreprivate'] });
                objContextmenu.removefromstoreprivate.callback = function () {
                    egov.message.show(
                    'Bạn có đồng ý xóa văn bản/hồ sơ này không?',
                    null,
                    egov.message.messageButtons.YesNo,
                    function () {
                        egov.request.removeStorePrivateDocument({
                            data: { documentCopyIds: _this.docCopyIdSelected, storeId: _this.parent.node.model.get("id") },
                            success: function (data) {
                                if (data.success) {
                                    _this.parent.removeDocumentsAndSetSelected(_this.selectedModels);
                                    egov.pubsub.publish(egov.events.status.success, data.success);

                                } else {
                                    egov.pubsub.publish(egov.events.status.error, data.error);
                                }
                            },
                            error: function () {
                                egov.pubsub.publish(egov.events.status.error, "Có lỗi trong quá trình loại bỏ văn bản/hồ sơ.");
                            }
                        });
                    });
                };
            }

            if (permissionValue & egov.enum.permission.suavanban) {
                $.extend(itemsContext, { 'suavanban': objContextmenu['suavanban'] });
            }

            

            if (permissionValue & egov.enum.permission.molaivanban) {
                $.extend(itemsContext, { 'molaivanban': objContextmenu['molaivanban'] });
                objContextmenu.molaivanban.callback = function () {
                    egov.request.reOpenDocument({
                        data: {
                            "docIds": JSON.stringify(_this.docCopyIdSelected)
                        },
                        success: function (data) {
                            if (data.success) {
                                egov.pubsub.publish(egov.events.status.success, egov.resources.documents.contextmenu.reOpenDocument.success);
                                return;
                            }
                            egov.pubsub.publish(egov.events.status.error, egov.resources.documents.contextmenu.reOpenDocument.error);

                        }
                    })
                };
            }

            //HopCV: Tạm bỏ không hiển thị contextmenu
            // Gửi ý kiến
            if (hasGetContext && docCopyIdSelectedLeng === 1) {

                if (permissionValue & egov.enum.permission.guiykien) {
                    $.extend(itemsContext, { 'guiykien': objContextmenu['guiykien'] });
                    objContextmenu.guiykien.callback = function () {
                        egov.message.prompt(egov.resources.document.sendComment.dialogTitle,
                             egov.resources.document.sendComment.dialogButton,
                             function (comment) {
                                 if (comment == null || comment === '') {
                                     egov.pubsub.publish(egov.events.status.error, egov.resources.document.sendComment.requireMessage);
                                     return;
                                 }

                                 egov.pubsub.publish(egov.events.status.processing, egov.resources.common.transfering);
                                 egov.request.sendComment({
                                     data: {
                                         documentCopyId: _this.docCopyIdClick,
                                         comment: comment
                                     },
                                     success: function (result) {
                                         if (result.error) {
                                             egov.pubsub.publish(egov.events.status.error, result.error);
                                         } else {
                                             egov.pubsub.publish(egov.events.status.success, egov.resources.document.sendComment.sendSuccess);
                                         }
                                     },
                                     error: function () {
                                         egov.pubsub.publish(egov.events.status.error, egov.resources.document.sendComment.sendFail);
                                     }
                                 });
                             }, true
                        );
                    };
                }
            }

            // Bàn giao
            if (permissionValue & egov.enum.permission.bangiao) {
                $.extend(itemsContext, { 'bangiao': objContextmenu['bangiao'] });
            }

            //HopCV: Tạm bỏ không hiển thị contextmenu
            // Thông báo
            if (hasGetContext && docCopyIdSelectedLeng === 1) {
                if (permissionValue & egov.enum.permission.thongbao) {
                    $.extend(itemsContext, { 'thongbao': objContextmenu['thongbao'] });
                    objContextmenu.thongbao.callback = function () {
                        require(['announcementView'], function (announcementView) {
                            if (egov.announcementForm === undefined) {
                                egov.announcementForm = new announcementView;
                            }
                            egov.announcementForm.render(_this.docCopyIdClick);
                        });
                    };
                }
            }

            ////HopCV: Tạm bỏ không hiển thị contextmenu
            //// Xin ý kiến
            //if (hasGetContext && permissionValue & egov.enum.permission.xinykien) {
            //    $.extend(itemsContext, { 'xinykien': objContextmenu['xinykien'] });
            //    objContextmenu.xinykien.callback = function () {
            //        require(['consultView'], function (consultView) {
            //            if (egov.consultView === undefined) {
            //                egov.consultView = new consultView;
            //            }
            //            egov.consultView.renderForContext(_this.docCopyIdClick);
            //        });
            //    };
            //}

            // Xác nhận bàn giao
            if (docCopyIdSelectedLeng === 1) {
                if (permissionValue & egov.enum.permission.xacnhanbangiao) {
                    $.extend(itemsContext, { 'xacnhanbangiao': objContextmenu['xacnhanbangiao'] });
                    objContextmenu.xacnhanbangiao.callback = function () {
                        require(['confirmTransferOrProcess'], function (ConfirmTransferOrProcessView) {
                            new ConfirmTransferOrProcessView({
                                docCopyId: _this.docCopyIdClick,
                                isTransfer: true
                            });
                        });
                    };
                }
                if (permissionValue & egov.enum.permission.xacnhanxuly) {
                    $.extend(itemsContext, { 'xacnhanxuly': objContextmenu['xacnhanxuly'] });
                    objContextmenu.xacnhanxuly.callback = function () {
                        require(['confirmTransferOrProcess'], function (ConfirmTransferOrProcessView) {
                            new ConfirmTransferOrProcessView({
                                docCopyId: _this.docCopyIdClick,
                                isTransfer: false
                            });
                        });
                    };
                }
            }

            // Yêu cầu bổ sung
            if (permissionValue & egov.enum.permission.yeucaubosung) {
                $.extend(itemsContext, { 'yeucaubosung': objContextmenu['yeucaubosung'] });
            }

            //HopCV: Tạm bỏ không hiển thị contextmenu
            // Tiếp nhận bổ sung
            if (hasGetContext && permissionValue & egov.enum.permission.tiepnhanbosung) {
                $.extend(itemsContext, { 'tiepnhanbosung': objContextmenu['tiepnhanbosung'] });
            }

            // Ký duyệt
            //if (docCopyIdSelectedLeng === 1) {
            if (permissionValue & egov.enum.permission.kyduyet) {
                $.extend(itemsContext, { 'kyduyet': objContextmenu['kyduyet'] });
                objContextmenu.kyduyet.callback = function () {
                    require(['views/document/functions/approver'], function (ApproverView) {
                        new ApproverView({
                            docCopyIds: _this.docCopyIdSelected
                        });
                    });
                };
            }

            //}
            // Trả kết quả
            if (permissionValue & egov.enum.permission.traketqua) {
                // right flag is set
            }

            // Gia hạn xử lý
            if (docCopyIdSelectedLeng === 1) {
                if (permissionValue & egov.enum.permission.giahanxuly) {
                    $.extend(itemsContext, { 'giahanxuly': objContextmenu['giahanxuly'] });
                    objContextmenu.giahanxuly.callback = function () {
                        require(['jquery', 'libs/jquery/jquery.validate.min'],
                            function () {
                                require(['libs/jquery/jquery.validate.unobtrusive.min'],
                                    function () {
                                        require(['views/document/functions/addtime'],
                                            function (AddTimeView) {
                                                new AddTimeView({
                                                    docCopyId: _this.docCopyIdClick
                                                });
                                            }
                                        );
                                    }
                                );
                            }
                        );
                    };
                }
            }

            // Dừng xử lý
            if (hasGetContext && permissionValue & egov.enum.permission.dungxuly) {
                $.extend(itemsContext, { 'dungxuly': objContextmenu['dungxuly'] });
            }

            // Kết thúc xử lý
            if (permissionValue & egov.enum.permission.ketthucxuly) {
                $.extend(itemsContext, { 'ketthucxuly': objContextmenu['ketthucxuly'] });
                objContextmenu.ketthucxuly.callback = function () {
                    _this.parent.finishs(_this.selectedModels, function () {
                        //_this.parent.reloadDocuments();
                    });
                };
            }

            // Hủy văn bản
            if (permissionValue & egov.enum.permission.huyvanban) {
                $.extend(itemsContext, { 'huyvanban': objContextmenu['huyvanban'] });
                objContextmenu.huyvanban.callback = function () {
                    egov.message.show(
                    'Bạn có đồng ý loại bỏ văn bản/hồ sơ này không?',
                    null,
                    egov.message.messageButtons.YesNo,
                    function () {
                        egov.request.removeDocument({
                            data: { documentCopyIds: _this.docCopyIdSelected },
                            success: function (data) {
                                if (data.success) {
                                    _this.parent.removeDocumentsAndSetSelected(_this.selectedModels);
                                    egov.pubsub.publish(egov.events.status.success, data.success);

                                } else {
                                    egov.pubsub.publish(egov.events.status.error, data.error);
                                }
                            },
                            error: function () {
                                egov.pubsub.publish(egov.events.status.error, "Có lỗi trong quá trình loại bỏ văn bản/hồ sơ.");
                            }
                        });
                    });
                };
            }

            //HopCV: Tạm bỏ không hiển thị contextmenu
            // Cập nhật kết quả xử lý cuối
            if (hasGetContext && permissionValue & egov.enum.permission.capnhatketquaxulycuoi) {
                $.extend(itemsContext, { 'capnhatketquaxulycuoi': objContextmenu['capnhatketquaxulycuoi'] });
            }

            ///Thêm xem chi tiết văn bản/hồ sơ
            if (docCopyIdSelectedLeng === 1) {
                $.extend(itemsContext, { 'chitietvanban': objContextmenu['chitietvanban'] });
                objContextmenu.chitietvanban.callback = function () {
                    getDocumentDetail(_this.clickModel.model.toJSON());
                };
            }


            //Note: Chức năng đánh dấu văn băn chưa đọc và văn bản đã đọc thì chỉ có ở mục văn bản chờ xử lý
            var document = _.filter(this.selectedModels, function (item) {
                return item.get('Status') === 2
                    && (item.get('UserCurrentId') === egov.setting.userId || _this.parent.node.model.get('hasUyQuyen'))
                    && _.contains([1, 2, 4, 32, 64], parseInt(item.get('DocumentCopyType')));
            });
            if (document.length > 0) {
                var read = [], unread = [],
                    leng = _this.selectedModels.length;
                for (var i = 0; i < leng; i++) {
                    if (_this.selectedModels[i].get('IsViewed') == true) {
                        read.push(_this.selectedModels[i]);
                    } else {
                        unread.push(_this.selectedModels[i]);
                    }
                }

                // Note: Nếu số lượng văn bản chọn mà chưa đọc >= đã đọc thì ưu tiên chưa đọc
                if (unread.length >= read.length) {
                    //Đánh dấu văn bản đã đọc
                    $.extend(itemsContext, { 'danhdaudadoc': objContextmenu['danhdaudadoc'] });
                    objContextmenu.danhdaudadoc.callback = function () {
                        _this.parent.setViewed(unread, true);
                    };
                }
                else {
                    //Đánh dấu văn bản chưa đọc
                    $.extend(itemsContext, { 'danhdauchuadoc': objContextmenu['danhdauchuadoc'] });
                    objContextmenu.danhdauchuadoc.callback = function () {
                        _this.parent.setViewed(read, false);
                    };
                }
            }

            if (this.parent && this.parent.hasExportFile == true) {
                //Xuat danh sach ra file word, excell
                $.extend(itemsContext, { 'exportToExcell': objContextmenu['exportToExcell'] });
                objContextmenu.exportToExcell.callback = function () {
                    _this.parent.exportToExcel("EXCELL");
                };

                //Xuat danh sach ra file word, excell
                $.extend(itemsContext, { 'exportToWord': objContextmenu['exportToWord'] });
                objContextmenu.exportToWord.callback = function () {
                    _this.parent.exportToWord("WORD");
                };
                
            }

            if (docCopyIdSelectedLeng > 1) {
                success(itemsContext);
            } else {
                //Lấy lại văn bản
                //Note: Chức năng lấy lại văn bản chỉ có ở mục văn bản theoo dõi và có trạng thái chưa đọc
                //  trạng thái văn bản chưa đọc => chứa chắc vì notify trạng thái văn bản xuống thì chưa set IsViewed  của model ngay được
                var checkUndo = //this.clickModel.model.get("IsViewed") == false &&
                    this.clickModel.model.get("UserCurrentId") !== egov.setting.userId
                    && (this.clickModel.model.get("Status") === 2
                    || this.clickModel.model.get("Status") === 16);

                if (!checkUndo) {
                    success(itemsContext);
                    return;
                }

                if (permissionValue & egov.enum.permission.laylaivanban) {
                    egov.request.getContextItemForUndoTransfering({
                        data: { documentCopyId: _this.docCopyIdClick },
                        success: function (data) {
                            if (data.error) {
                                return;
                            }

                            if (data.length == 0) {
                                success(itemsContext);
                                return;
                            }

                            if (!$.isEmptyObject(itemsContext) && data.length > 0) {
                                $.extend(itemsContext, { name: "---" });
                            }

                            for (var i = 0; i < data.length; i++) {
                                var dateCreated = data[i].DateCreated;
                                var contextItem = {
                                    className: 'laylaivanbanClass',
                                    name: data[i].Name,
                                    dateCreated: dateCreated,
                                    iconClass: 'icon-undo2',
                                    commandName: 'laylaivanban' + i,
                                    callback: function (key) {
                                        var dateCreated1 = itemsContext[key.get('key')].dateCreated;
                                        var dateCreatedStr = Globalize.format(dateCreated1, "F");
                                        require(['retrieve'], function (RetrieveView) {
                                            new RetrieveView({
                                                docCopyId: _this.docCopyIdClick,
                                                dateCreated: dateCreatedStr,
                                                callback: function (value) {
                                                    _this.clickModel.$el.remove();
                                                    var model = _this.clickModel.model.toJSON();
                                                    _this.parent.removeDocumentsAndSetSelected([_this.clickModel.model]);
                                                }
                                            });
                                        });
                                    }
                                };
                                var contextItemInsert = {};
                                contextItemInsert['laylaivanban' + i] = contextItem;
                                $.extend(itemsContext, contextItemInsert);
                            }

                            success(itemsContext);
                        }
                    });
                }

            }
            
        }
    });

    ///Parse thông tin văn bản và show lên dialog
    var getDocumentDetail = function (document) {
        require(['documentDetail'], function (DocumentDetail) {
            //render thông tin chi tiết văn bản
            var documentDetail = new DocumentDetail({ model: document });

            //Thiết lập dialog
            var settings = {
                width: 800,
                height: 310,
                title: egov.resources.documents.title.documentDetail,
                buttons: [{
                    text: egov.resources.common.closeButton,
                    className: 'btn-default',
                    click: function () {
                        documentDetail.$el.dialog('destroy');
                    }
                }],
                draggable: true,
                keyboard: true,
            };

            ///show dialog
            documentDetail.$el.dialog(settings);
        });
    }

    return DocumentContextmenu;
});