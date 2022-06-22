(function () {

    var EgovPlugin = {

        extension: null,

        signatureConfig: [],

        appendPlugin: function (callback) {
            /// <summary>
            /// Chèn plugin
            /// </summary>
            /// <param name="callback"></param>
            //if (egov.isMobile) {
            //    return;
            //}

            if (!egov.extension) {
                egov.extension = PluginFactory.getInstance();
            }

            if (!egov.extension) {
                return;
            }

            // Kiem tra plugin/extension đã sẵn sàng sử dụng?
            egov.extension.isReady(function (isReady) {
                if (isReady) {
                    this.checkNativeAppVersion(callback);
                } else {
                    // Kiểm tra plugin/extension đã được cài đặt?
                    egov.extension.isPluginExist(function (isInstalled) {
                        if (isInstalled) {
                            egov.extension.injectPlugin("egovPlugin", function () {
                                this.appendPlugin(callback);
                            }.bind(this));
                        } else {
                            this.showDialogDownloadPlugin(function () {
                                this.appendPlugin(callback);
                            }.bind(this));
                        }
                    }.bind(this));
                }
            }.bind(this));
        },

        checkNativeAppVersion: function (callback) {
            if (egov.extension.hasAppendMode != null || egov.hasPluginAppendMode != null) {
                // 
            } else {
                setTimeout(function () {
                    egov.extension.hasAppendWriteMode(function (result) {
                        egov.extension.hasAppendMode = result;
                        egov.hasPluginAppendMode = result;
                        egov.callback(callback);
                    });
                }, 2000);
            }

            egov.callback(callback);
            return;
        },

        //#region Tải bộ cài plugin

        showDialogDownloadPlugin: function (callback) {
            /// <summary>
            /// Hiển thị dialog yêu cầu tải về plugin để mở file.
            /// </summary>
            /// <param name="callback">Hàm thực thi sau khi cài đặt thành công.</param>
            var that,
                _div;

            that = this;

            _div = $('<div><p style="font-size:16px;font-weight:bold;">'
                + egov.resources.plugin.noplugin + '</p><p>'
                + egov.resources.plugin.pluginrequire + '</p><p style="color:red">'
                + egov.resources.plugin.needrestartbrowser + '</p><center><div style="text-align:center;width:180px"><input type="button" value="'
                + egov.resources.plugin.downloadtosetup + '" />'
                //<div id="imgDownloadingPlugin" style="float:left;display:none"><img src="/Content/Images/ajax-loader.gif" width="24px" height="24px" /></div><div id="msgDowloadingPlugin" style="float:left;padding-top:5px;display:none">&nbsp;'
                //+ egov.resources.plugin.waitforsetup + '</div>
                + '</div></center></div>');

            _div.find('input').bind('click', function () {
                that._startDownload();
                that._dowloadingPlugin(_div, callback);
            });

            _div.dialog({
                width: '800px',
                resizable: false,
                title: egov.resources.common.alert,
                buttons: [
                    {
                        text: egov.resources.common.closeButton,
                        click: function () {
                            _div.dialog('destroy');
                        }
                    }
                ]
            });
        },

        _startDownload: function () {
            /// <summary>
            /// DownloadPlugin
            /// </summary>
            document.location = "/Download/EOfficePlusPlugin";
        },

        _dowloadingPlugin: function (div, callback) {
            /// <summary>
            /// Các event xảy ra khi đang download
            /// </summary>
            /// <param name="div"></param>
            /// <param name="callback"></param>
            var that,
                retryBtn;

            that = this;
            retryBtn = $('<a class="retry">' + egov.resources.main.installPlugin.reDownload + '</a>');
            retryBtn.on("click", function (e) {
                that._startDownload();
            });
            div.empty();
            div.append(retryBtn);
            //TODO: Chờ plugin thực tế ntn rồi thêm hình ảnh hướng dẫn cài đặt theo kịch bản
            //Phần này phải xem plugin mới cài đặt xong nó ntn thì mới xử lý theo kịch bản được
            //FireBreath.waitForInstall(that.pluginName, function () {
            //    dialog.close();
            //    if (callback && typeof (callback) === 'function') {
            //        callback();
            //    }
            //});
        },

        //#endregion

        //#region Ký

        callSignSuccess: function (result) {
            if (this.tempFolderLoc) {
                this.deleteFolder(this.tempFolderLoc);
            }
            this.currentSignOptions = { withChoosePoint: false };
            egov.callback(this.signSuccess, result);
        },

        sign: function (document, signSuccess) {
            /// <summary>
            /// Ký các file trong văn bản
            /// </summary>
            /// <param name="document">văn bản</param>
            /// <param name="callback">Hàm callback</param>
            var that,
                confirmSignFiles,
                docAttachments;

            that = this;
            that.currentSignOptions = { withChoosePoint: false };

            // Thư mục lưu file tạm để chuẩn bị ký.
            that.tempFolderLoc = (new Date()).getTime().toString() + "_forsign";

            that.hasDeleteOldFile = false;
            that.signatureConfig = egov.signatureConfig || [];
            that.signSuccess = signSuccess;

            var signerConfig = egov.setting.signerConfig;
            if (signerConfig.length === 0) {
                egov.pubsub.publish(egov.events.status.error, "Bạn chưa cấu hình chữ ký số, vui lòng vào Thiết lập cá nhân/Cấu hình chữ ký để tạo mới.");
                that.callSignSuccess(false);
                return;
            }

            // Ghi toàn bộ nội dung file ảnh chèn vào chữ kí ra thư mục tạm
            var writeSignatureConfig = $.isArray(signerConfig)
                                            ? signerConfig.slice(0, signerConfig.length)
                                            : $.extend(true, {}, signerConfig);

            that.document = document;
            that._writeImageSignature(writeSignatureConfig, function (signatureConfig) {
                // Bắt đầu quá trình kí

                confirmSignFiles = document.getAttachmentsForSign();
                if (confirmSignFiles.length > 0) {
                    that.confirmSignFiles = confirmSignFiles;
                    that._displaySignDialog(confirmSignFiles, signSuccess);
                } else {
                    that.callSignSuccess(false);
                }
            });
        },

        _displaySignDialog: function (confirmSignFiles, signSuccess) {
            /// <summary>
            /// Hiển thị dialog ký
            /// </summary>
            /// <param name="confirmSignFiles">file cần ký</param>
            /// <param name="callback"></param>
            var that,
                $confirmDialog,
                settings;

            that = this;
            that.isPreviewing = false;

            require([egov.template.document.signer], function (signTemplate) {
                $confirmDialog = $('<div></div>');
                $confirmDialog.append(signTemplate);

                $confirmDialog.find(".sign-list").append($.tmpl('<li class="list-group-item"><div><label><input type="checkbox" data-id="${Id}"> ${Title}</label></div></li>', that.signatureConfig));
                if (that.signatureConfig.length === 1) {
                    $confirmDialog.find(".sign-list :checkbox").attr("checked", "checked");
                }

                var confirmFiles = _.map(confirmSignFiles, function (f) {
                    return f.toJSON();
                });

                var groupByDocument = _.groupBy(confirmFiles, "Compendium");
                var $attachments = $confirmDialog.find(".att-list");
                _.each(groupByDocument, function (items, name) {
                    $attachments.append('<li class="list-group-item"><b>' + name + '</b></li>');
                    $attachments.append($.tmpl('<li class="list-group-item"><div><label><input type="checkbox" data-id="${Id}" checked> ${Name}</label></div></li>', items));
                });

                $confirmDialog.appendTo('body');
                that.$confirmDialog = $confirmDialog;

                settings = {};
                settings.width = 500;
                settings.height = "auto";
                settings.autoResize = true;
                settings.title = "Ký số điện tử";
                settings.buttons = [
                {
                    text: "Xem trước",
                    className: 'btn-warning',
                    click: function () {
                        that._signPreviewBtn();
                    }
                },
                {
                    text: "Chuyển",
                    className: 'btn-primary',
                    click: function () {
                        that._signAndSendBtn();
                    }
                },
                {
                    text: "Bỏ qua",
                    className: 'btn-default',
                    click: function () {
                        that.isPreviewing = false;
                        that.$confirmDialog.dialog("destroy");
                        that.callSignSuccess(false);
                    }
                }];

                settings.confirm = {
                    text: "Xóa tệp cũ sau khi ký",
                    id: "deleleOldFile",
                    style: { float: "left", "font-weight": "normal", "font-size": "13px" },
                    click: function (isChecked) {
                        that.hasDeleteOldFile = isChecked;
                    },
                    hasAutoClick: false,
                    disabled: false
                };

                that.$confirmDialog.dialog(settings);
            });
        },

        _getSelectedFiles: function (confirmSignFiles) {
            var that = this;
            var $selected = that.$confirmDialog.find(".att-list input:checked");
            if ($selected.length <= 0) {
                return [];
            }
            var selectedIds = [];
            $selected.each(function (i, item) {
                selectedIds.push($(item).attr('data-id'));
            });

            var selectedFiles = _.filter(confirmSignFiles, function (item) {
                return _.contains(selectedIds, item.get("Id").toString());
            });
            that.currentSignOptions.selectedFiles = selectedFiles;
            return selectedFiles;
        },

        _getSelectedSignerConfig: function (signerConfig) {
            var that = this;
            var hasShowAlertChooseToken = false;
            var $selected = that.$confirmDialog.find(".sign-list input:checked");
            if ($selected.length <= 0) {
                return [];
            }

            var selectedIds = [];
            $selected.each(function (i, item) {
                selectedIds.push($(item).attr('data-id'));
            });

            var selectedSigners = _.filter(signerConfig, function (item) {
                return _.contains(selectedIds, item.Id.toString());
            });

            var config = {
                sameToken: _.where(selectedSigners, { 'hasSameToken': true })
            };

            _.each(_.where(selectedSigners, { 'hasSameToken': false }), function (c) {
                hasShowAlertChooseToken = true;
                config[c.Id] = [c];
            });

            var signerConfigs = _.values(config);
            that.currentSignOptions.config = signerConfigs;
            that.currentSignOptions.hasShowAlertChooseToken = hasShowAlertChooseToken;

            return selectedSigners;
        },

        _writeFileBase64: function (base64OfAttachmentFiles, selectedFiles, callback) {
            /*
             * Ghi nội dung các file cần kí ra thư mục tạm, chuẩn bị cho quá trình kí
             */
            var that = this;

            if (!base64OfAttachmentFiles) {
                callback();
                return;
            }

            // Lấy ra các giá trị key của file
            var fileIds = Object.keys(base64OfAttachmentFiles);
            if (fileIds.length === 0) {
                callback();
                return;
            }

            // lấy ra file đâu tiên để thực hiện kí
            var attachmentId = fileIds[0];
            var attachmentBase64 = base64OfAttachmentFiles[attachmentId];
            var attachment = selectedFiles.filter(function (item) {
                return item.get("Id") == attachmentId;
            });
            attachment = attachment[0];

            var extension = attachment.get("Extension");
            if (extension.indexOf(".") != 0) {
                extension = "." + extension;
            }

            var filename = attachment.get("Name").toLowerCase(); // attachment.get("Id") + extension;
            var filePath = that.tempFolderLoc + "\\" + filename;

            egov.extension.writeFileBase64(filePath, attachmentBase64, true, function (jsonResult) {

                var isSaved = jsonResult.returnCode === 1;
                if (isSaved) {
                    attachment.set('fileData', filePath);
                    attachment.set('md5', jsonResult.md5);

                    delete base64OfAttachmentFiles[attachmentId];

                    that._writeFileBase64(base64OfAttachmentFiles, selectedFiles, callback);
                } else {
                    // Nếu có lỗi thì báo và return luôn mà k cần gọi callback để dừng tiến trình xử lý lại
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                    return;
                }
            });
        },

        _writeImageSignature: function (signatureConfig, callback) {
            /*
             * Ghi tạm các file ảnh trong config kí
             */
            var that = this;

            if (signatureConfig.length === 0 || egov.isWriteSigner) {
                egov.isWriteSigner = true;
                egov.signatureConfig = that.signatureConfig;
                _.each(egov.signatureConfig, function (config) {
                    if (config.SignType !== 0) {
                        var docCode = that.document.isTransferTheoLo ? "" : that.document.model.get("DocCode");
                        config.ReplaceText = docCode.split('/')[0];
                    }
                });
                callback(that.signatureConfig);
                return;
            }

            var image = signatureConfig.pop();
            if (image == undefined) {
                callback(that.signatureConfig);
                return;
            }

            var isImageSigner = image.ImagePath && image.SignType === 0;
            if (isImageSigner) {
                var filename = '' + (new Date()).getTime() + image.Ext;
                var tempFolderLoc = (new Date()).getTime().toString();
                var filePath = tempFolderLoc + "\\" + filename;

                var filesize = egov.extension.writeFileBase64(filePath, image.ImagePath, true, function (jsonResult) {
                    if (jsonResult.returnCode == 1) {
                        filePath = filePath.replace(/\//g, '\\');
                        image.ImagePath = filePath;
                        that.signatureConfig.push(image);
                        that._writeImageSignature(signatureConfig, callback);
                    } else {
                        // Nếu có lỗi thì báo và return luôn mà k cần gọi callback để dừng tiến trình xử lý lại
                        egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                        return;
                    }
                });
            } else {
                image.ImagePath = "";
                image.ReplaceText = that.document.isTransferTheoLo ? "" : that.document.model.get("DocCode");
                that.signatureConfig.push(image);
                that._writeImageSignature(signatureConfig, callback);
            }
        },

        _signAndSendBtn: function () {
            var that = this;
            var sendDocument = function (isSigned, filePdfAddNew) {
                if (!isSigned || filePdfAddNew.length === 0) {
                    that.$confirmDialog.dialog("destroy");
                    that.callSignSuccess(false);
                    return;
                }

                egov.pubsub.publish(egov.events.status.success, "Kí file thành công!");
                that._uploadAndSend(filePdfAddNew);
            };

            this._doSign(sendDocument);
        },

        _signPreviewBtn: function () {
            var that = this;
            if (that.isPreviewing) {
                return;
            }

            that.isPreviewing = true;
            var openPreview = function (isSigned, filePdfAddNew) {
                if (!isSigned || filePdfAddNew.length === 0) {
                    that.$confirmDialog.dialog("destroy");
                    that.callSignSuccess(false);
                    return;
                }

                that._openPreviewSinged(filePdfAddNew);
            };

            this._doSign(openPreview);
        },

        _signWithPositionAndPreview: function () {
            if (!this.currentSignOptions) {
                return;
            }

            var that = this;
            var filePdfAddNew = [];
            var withChoosePoint = true;
            var openPreview = function (isSigned, filePdfAddNew) {
                if (!isSigned || filePdfAddNew.length === 0) {
                    that.$confirmDialog.dialog("destroy");
                    that.callSignSuccess(false);
                    return;
                }

                that._openPreviewSinged(filePdfAddNew);
            };

            that.currentSignOptions.withChoosePoint = true;

            egov.pubsub.publish(egov.events.status.info, "Vui lòng quét chuột chọn vị trí cần hiển thị chữ ký.");
            that._writeFilesAndSign(openPreview);
        },

        _openPreviewSinged: function (filePdfAddNew) {
            var that = this;
            var previewDialog = $("<div>");
            for (var i = 0; i < filePdfAddNew.length ; i++) {
                var file = filePdfAddNew[i];

                var pdfObj = $("<object>");
                pdfObj.css({ width: "100%", height: "350px", border: "none" });
                pdfObj.attr("data", "data:application/pdf;base64," + file.value);

                previewDialog.append(pdfObj);
            }

            previewDialog.appendTo("body");

            var setting = {
                width: 1000,
                height: 400,
                autoResize: true,
                title: "Ký số điện tử",
                buttons: [
                    {
                        text: "Quét chọn vị trí khác",
                        className: 'btn-warning',
                        click: function () {
                            previewDialog.dialog("destroy");
                            // Xóa thư mục đã ký trước đó trước khi ký lại
                            that.deleteFolder(that.tempFolderLoc, function () {
                                that._signWithPositionAndPreview();
                            });
                        }
                    },
                    {
                        text: "Chuyển",
                        className: 'btn-primary',
                        click: function () {
                            previewDialog.dialog("destroy");
                            that._uploadAndSend(filePdfAddNew);
                        }
                    },
                    {
                        text: "Bỏ qua",
                        className: 'btn-default',
                        click: function () {
                            that.isPreviewing = false;
                            previewDialog.dialog("destroy");
                        }
                    }
                ]
            };

            previewDialog.dialog(setting);
        },

        _doSign: function (signSuccess) {
            var selectedFiles,
                selectedSigners,
                filePdfAddNew,
                fileTempIds = [],
                fileIds = [],
                that = this;

            // Nếu không có file nào được chọn để kí => gọn callback để tiếp tục bàn giao.
            selectedFiles = that._getSelectedFiles(that.confirmSignFiles);
            selectedSigners = that._getSelectedSignerConfig(that.signatureConfig);

            if (selectedFiles.length <= 0 || selectedSigners.length <= 0) {
                egov.pubsub.publish(egov.events.status.warning, "Bạn chưa chọn tệp hoặc chữ ký để ký số. Vui lòng thử lại.");
                that.$confirmDialog.dialog("destroy");
                signSuccess(false, []);
                return;
            }

            // Lấy danh sách file cần tải từ server về để kí => ghi ra thư mục tạm => kí
            var serverFiles = selectedFiles.filter(function (item) {
                return !item.get("fileData") || (!egov.setting.publish.detectPdfChangeContent && egov.fileExtension.isReadonly(item.get("Name")));
            });

            // Tải danh sách file về để ký
            $.each(serverFiles, function (i, item) {
                if (item.get("isNew")) {
                    if (egov.isMobile) {
                        fileTempIds.push(item.get("Id") + '.' + item.get("Extension"));
                    } else {
                        fileTempIds.push(item.get("Id"));
                    }
                } else {
                    fileIds.push(item.get("Id"));
                }
            });

            // PROCESSING: Báo trạng thái đang xử lý
            egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

            egov.request.downloadAttachmentForSignBase64({
                data: {
                    fileIds: fileIds,
                    fileTempIds: fileTempIds,
                    convertWordTopdf: false
                },
                success: function (result) {
                    // Ghi toàn bộ nội dung file cần kí ra thư mục tạm.
                    that.currentSignOptions.base64OfAttachmentFiles = $.extend(true, {}, result.files);
                    that._writeFilesAndSign(signSuccess);
                },
                error: function (error) {
                    that.isPreviewing = false;
                    egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage);
                }
            });
        },

        _writeFilesAndSign: function (signSuccess) {
            var that = this;
            var hasShowAlertChooseToken = that.currentSignOptions.hasShowAlertChooseToken;
            var config = _.clone(that.currentSignOptions.config);
            var selectedFiles = _.clone(that.currentSignOptions.selectedFiles);
            var base64Attachments = _.clone(that.currentSignOptions.base64OfAttachmentFiles);

            var writeSuccess = function () {
                // Bắt đầu quá trình kí
                var filePdfAddNew = [];
                var currentSign = config.pop(); // currentSign is array
                currentSign.length === 0 && (currentSign = config.pop());

                var signSuccessWithToken = function () {
                    if (config.length === 0) {
                        signSuccess(true, filePdfAddNew);
                        return;
                    }

                    currentSign = config.pop();
                    that._signFileWithCertIndex(selectedFiles, currentSign, filePdfAddNew, signSuccessWithToken, hasShowAlertChooseToken);
                };

                that._signFileWithCertIndex(selectedFiles, currentSign, filePdfAddNew, signSuccessWithToken, hasShowAlertChooseToken);
            };

            this._writeFileBase64(base64Attachments, selectedFiles, writeSuccess);
        },

        _signFileWithCertIndex: function (selectedFiles, config, filePdfAddNew, success, hasConfirm) {
            // Ký các file với 1 token.

            if (config.length === 0) {
                return success();
            }

            if (hasConfirm) {
                var signName = _.pluck(config, 'Title').join(",");
                var message = String.format("Đang ký số cho chữ ký {0}. /nVui lòng cắm token vào máy và nhấn Ok để chọn.", signName);
                alert("Chọn Token cho chữ ký " + signName);
            }

            var that = this;

            if (this.currentSignOptions && this.currentSignOptions.idxCert) {
                this._signFile(selectedFiles, filePdfAddNew, config, this.currentSignOptions.idxCert, function () {
                    success();
                }, this.currentSignOptions.withChoosePoint);
                return;
            }

            // Chạy sau khi chọn token
            var callbackCertIndex = function (jsonResult) {
                var idxCert = parseInt(jsonResult.idxCert);
                if (isNaN(idxCert) || idxCert <= -1) {
                    // Báo không có chữ kí nào được chọn.
                    // Không đóng cửa sổ chọn file kí hiện tại (không gọi $confirmDialog.dialog("destroy");)
                    // Không tiếp tục bàn giao (Không gọi egov.callback(callback);)
                    that.isPreviewing = false;
                    egov.pubsub.publish(egov.events.status.error, "Vui lòng kiểm tra lại chữ kí số đã sẵn sàng trước khi kí!");
                    return;
                }

                that.currentSignOptions.idxCert = idxCert;
                that._signFile(selectedFiles, filePdfAddNew, config, idxCert, function () {
                    success();
                }, that.currentSignOptions.withChoosePoint);
            };

            egov.extension.getCertIndex(callbackCertIndex);
        },

        _signFile: function (selectedFiles, filePdfAddNew, config, idxCert, callback, withChoosePoint) {
            /// <summary>
            /// ký file
            /// </summary>
            /// <param name="item">file cần ký</param>
            if (selectedFiles.length === 0) {
                callback();
                return;
            }
            withChoosePoint = withChoosePoint || false;
            var item = selectedFiles.pop();

            // Đường dẫn file lưu trong %TEMP%/BkavEgovChrome
            var fileName = item.get("fileData");
            var isForSign = egov.fileExtension.isForSign(fileName);

            // Clone danh sach chu ky
            var cfigForSign = config.slice(0);
            if (isForSign) {
                //config = { Ext: "", FindText: "quangp", FindType: 1, : "", OffsetX: 0, OffsetY: 0, PosType: 3, SignType: 1, TextInfor: 1, Title: "Sign" };

                var cfig = cfigForSign.pop();
                var configSign = $.extend(true, { SignAuthor: egov.setting.userName, SignReason: "Bkav eGov - Ký số điện tử" }, cfig);

                var signedComplete = function (jsonResult) {
                    // jsonResult = {action: "Egov_SignFile", base64: "", fileName: "1504834898526\{attachmentId}.pdf", returnCode: 1, returnMessage: ""}
                    cfig = cfigForSign.pop();

                    if (jsonResult.returnCode == 1) {
                        if (cfig != undefined) {
                            if (egov.fileExtension.isMsOfficeFile(fileName)) {
                                fileName = egov.fileExtension.getFileName(fileName) + ".pdf";
                            }

                            configSign = $.extend(true, { SignAuthor: egov.setting.userName, SignReason: "Bkav eGov - Ký số điện tử" }, cfig);

                            this._signSelectFile(fileName, configSign, idxCert, signedComplete);
                            return;
                        }

                        var signedFilename = egov.fileExtension.getFileName(item.get("Name"));
                        if (!signedFilename.match(/_Signed$/)) {
                            signedFilename = signedFilename + "_Signed";
                        }
                        signedFilename = signedFilename + ".pdf";

                        filePdfAddNew.push({
                            id: '' + item.get("Id"),
                            documentCopyId: item.get("DocumentCopyId"),
                            name: signedFilename,
                            originName: jsonResult.fileName,
                            value: jsonResult.base64
                        });

                        this._signFile(selectedFiles, filePdfAddNew, config, idxCert, callback, withChoosePoint);
                    } else {
                        egov.pubsub.publish(egov.events.status.error, egov.resources.common.errorMessage + ": " + jsonResult.returnMessage);
                        return;
                    }
                }.bind(this);

                withChoosePoint
                    ? this._signByPoint(fileName, configSign, idxCert, signedComplete)
                    : this._signSelectFile(fileName, configSign, idxCert, signedComplete);
            }
        },

        _signSelectFile: function (localName, configSign, idxCert, callback) {
            // configSign.TSAURL = "http://ca.gov.vn/tsa";
            egov.extension.signFile(localName, configSign, parseInt(idxCert), function (jsonResult) {
                callback(jsonResult);
            }.bind(this));
        },

        _signByPoint: function (localName, configSign, idxCert, callback) {
            egov.extension.signFileByPoint(localName, configSign, parseInt(idxCert), function (jsonResult) {
                callback(jsonResult);
            }.bind(this));
        },

        _uploadAndSend: function (filePdfAddNew) {
            var that = this;
            this._uploadSignedFile(filePdfAddNew, this.document, function () {
                that.$confirmDialog.dialog("destroy");
                that.callSignSuccess(true);
                egov.pubsub.publish(egov.events.status.success, "Đính kèm file đã kí vào văn bản thành công!");
            });
        },

        _uploadSignedFile: function (signedFiles, document, callback) {
            /// <summary>
            /// Upload file đã ký lên
            /// </summary>
            document.uploadSignFiles(signedFiles, callback);
        },

        //#endregion

        //#region File đính kèm

        // Kiểm soát danh sách các file đang trong quá trình mở (tải về, ghi ra thư mục tạm, và mở) để tránh lặp khi click đúp liên tiếp
        openAttachment: function (attachment, version) {
            /// <summary>
            /// Mở file đính kèm
            /// </summary>
            /// <param name="attachment"></param>
            /// <param name="version"></param>
            var id, storePrivateId;
            id = attachment.model.get('Id');
            storePrivateId = attachment.parent.storePrivateId;

            //// Neu file nay dang trong qua trinh mo thi return
            //if (this.opennings[id]) {
            //    console.log("Info: openAttachment - click đúp mở file liên tiếp! Bỏ qua do đang tải và ghi file ra thư mục tạm.");
            //    return;
            //}

            // Danh dau qua trinh mo file bat dau
            //this.opennings[id] = id;

            // Nếu là file vừa đính kèm
            if (attachment.model.get('isNew')) {
                // Nếu file vừa đính kèm này đã được mở trước đó (đã có file ghi tạm) 
                // => Gọi lại lệnh mở file để mở file đã ghi tạm này nếu đã đóng, hoặc forcus lại file đang mở.
                if (attachment.model.get("isOpen") && attachment.model.get("fileData")) {
                    egov.extension.openFile(attachment.model.get("fileData"));
                }
                else {
                    // Nếu file chưa từng được mở sau khi đính kèm (chưa có file ghi tạm)
                    egov.request.downloadAttachmentTemp({
                        data: { id: id },
                        success: function (data) {
                            var result = JSON.parse(data);
                            if (result) {
                                if (result.error) {
                                    egov.pubsub.publish(egov.events.status.error, result.error);
                                } else {
                                    // Luu noi dung file vao object js truoc khi xu ly ghi file
                                    //attachment.model.set('base64', result.content);

                                    var extension = attachment.model.get("Extension");
                                    if (extension.indexOf(".") != 0) {
                                        extension = "." + extension;
                                    }

                                    var filename = attachment.model.get("Id") + extension;

                                    var tempFolderLoc = (new Date()).getTime().toString();
                                    filePath = tempFolderLoc + "\\" + filename;
                                    egov.extension.writeFileBase64(filePath, result.content, true, function (jsonResult) {

                                        // Danh dau qua trinh mo file hoan tat
                                        //delete this.opennings[id];

                                        // Neu ghi file thanh cong
                                        if (jsonResult.returnCode == 1) {
                                            attachment.model.set('fileData', filePath);
                                            attachment.model.set('md5', jsonResult.md5);
                                            egov.extension.openFile(filePath, function () {
                                                attachment.model.set('isOpen', true);
                                            });
                                        } else {
                                            // TODO: eGov xu ly cac truong hop loi nay:
                                            // - Yeu cau nguoi dung thu lai...                                            
                                        }
                                    }.bind(this));
                                }
                            }
                        }.bind(this),
                        error: function () {
                            // Danh dau qua trinh mo file hoan tat
                            //delete this.opennings[id];

                            egov.pubsub.publish(egov.events.status.error, egov.resources.document.attachment.errorDownload);
                        },
                        complete: function () {
                            egov.pubsub.publish(egov.events.status.destroy);
                        }
                    });
                }
            } else {
                var isLastVersion = version == attachment.model.get('LastestVesion');
                if (attachment.model.get("isOpen") && (!version || isLastVersion)) {
                    egov.extension.openFile(attachment.model.get("fileData"));
                }
                else {
                    egov.pubsub.publish(egov.events.status.processing, egov.resources.common.processing);

                    egov.request.downloadAttachment({
                        data: { id: id, storePrivateId: storePrivateId, version: version ? isLastVersion ? null : version : null },
                        success: function (data) {
                            var result = JSON.parse(data);
                            if (result) {
                                if (result.error) {
                                    egov.pubsub.publish(egov.events.status.error, result.error);
                                }
                                else {
                                    // var filename = attachment.model.get("Name");

                                    var extension = attachment.model.get("Extension");
                                    if (extension.indexOf(".") != 0) {
                                        extension = "." + extension;
                                    }

                                    //var filename = attachment.model.get("Name").toLowerCase() + extension;

                                    var filename = attachment.model.get("Id") + extension; //.get("Name").toLowerCase()

                                    var tempFolderLoc = (new Date()).getTime().toString();
                                    filePath = tempFolderLoc + "\\" + filename;
                                    egov.extension.writeFileBase64(filePath, result.content, true, function (jsonResult) {
                                        // Danh dau qua trinh mo file hoan tat
                                        //delete this.opennings[id];

                                        // Neu ghi file thanh cong
                                        if (jsonResult.returnCode == 1) {
                                            if (isLastVersion || !version) {
                                                attachment.model.set('fileData', filePath);
                                            }
                                            attachment.model.set('md5', jsonResult.md5);
                                            egov.extension.openFile(filePath, function () {
                                                if (isLastVersion || !version) {
                                                    attachment.model.set('isOpen', true);
                                                }
                                            });
                                        } else {
                                            // TODO: eGov xu ly cac truong hop loi nay:
                                            // - Yeu cau nguoi dung thu lai...
                                        }
                                    }.bind(this));
                                }
                            }
                        }.bind(this),
                        error: function () {
                            // Danh dau qua trinh mo file hoan tat
                            //delete this.opennings[id];

                            egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
                        },
                        complete: function () {
                            egov.pubsub.publish(egov.events.status.destroy);
                        }
                    });
                }
            }
        },

        openStoreAttachment: function (id) {
            egov.request.storePrivateOpenFile({
                data: { id: id },
                success: function (data) {
                    var result = JSON.parse(data);
                    if (result) {
                        if (result.error) {
                            egov.pubsub.publish(egov.events.status.error, result.error);
                        } else {
                            var filename = result.fileName;
                            var tempFolderLoc = (new Date()).getTime().toString();
                            filePath = tempFolderLoc + "\\" + filename;
                            egov.extension.writeFileBase64(filePath, result.content, true, function (jsonResult) {
                                // Danh dau qua trinh mo file hoan tat
                                //delete this.opennings[id];

                                // Neu ghi file thanh cong
                                if (jsonResult.returnCode == 1) {
                                    egov.extension.openFile(filePath, function () {

                                    });
                                } else {
                                    // TODO: eGov xu ly cac truong hop loi nay:
                                    // - Yeu cau nguoi dung thu lai...
                                }
                            }.bind(this));
                        }
                    }
                },
                error: function () {
                    egov.pubsub.publish(egov.events.status.error, _resource.errorDownload);
                },
                complete: function () {
                    egov.pubsub.publish(egov.events.status.destroy);
                }
            });
        },

        confirmAttachments: function (attachments, callback) {
            /// <summary>
            /// Lưu lại các file đang được sửa.
            /// </summary>
            /// <param name="callback">Hàm thực thi sau khi hoàn thành.</param>
            var that = this;
            var openFiles = attachments.model.select(function (file) {
                var isOpenning = !file.get('isRemoved') && file.get('isOpen');
                if (!attachments.isPublishing || !egov.setting.publish.detectPdfChangeContent) {
                    isOpenning = isOpenning && !egov.fileExtension.isReadonly(file.get("Name"));
                }
                return isOpenning;
            });

            if (openFiles.length === 0) {
                egov.callback(callback); return;
            }

            $(document).on('checkModifySuccess', function (e) {
                $(document).off('checkModifySuccess');

                egov.callback(callback);
                egov.helper.destroyClickEvent(e);
            });

            this._confirmSaveAttachment(openFiles, attachments);
        },

        _confirmSaveAttachment: function (openFiles, attachments) {
            /// <summary>
            /// Lưu các file đính kèm đang mở
            /// </summary>
            /// <param name="openFiles">Danh sách các file đang mở.</param>

            if (openFiles.length === 0) {
                $(document).trigger('checkModifySuccess');
                return;
            }

            var that = this;
            var file = openFiles[0];
            var remainFiles = openFiles.splice(1, openFiles.length - 1);

            this._closeFile(file, attachments, remainFiles);
        },

        _closeFile: function (file, attachments, newArray) {
            var that = this;
            egov.extension.closeFile(file.get("fileData"), function (closeResult) {
                if (closeResult.returnCode == 1 || closeResult.returnCode == 2) {
                    if (closeResult.returnCode == "2") {
                        console.log("egov.extension.closeFile: " + file.get('Name') + " đã đóng trước đó");
                    }

                    egov.extension.readFileBase64(file.get('fileData'), function (jsonResult) {
                        if (jsonResult.returnCode == 1) {
                            // So sanh noi dung file truoc va sau
                            // Nếu nội dung file có thay đổi
                            if (file.get("md5") != jsonResult.md5) {
                                var fileName = file.get('Name');
                                egov.message.show(String.format(egov.resources.document.attachment.fileChanged, fileName)
                                    , null
                                    , egov.message.messageButtons.YesNo
                                    , function () {
                                        attachments.modifiedFiles[file.get('Id')] = jsonResult.base64;

                                        // Tiếp tục hỏi với file tiếp theo
                                        that._confirmSaveAttachment(newArray, attachments);
                                    }
                                    , function () {
                                        // No: không lưu nội dung chỉnh sửa
                                        that._confirmSaveAttachment(newArray, attachments);
                                    });
                            }
                                // Nếu nội dung file không thay đổi
                            else {
                                that._confirmSaveAttachment(newArray, attachments);
                            }
                        } else if (jsonResult.returnCode == 2) {
                            console.log("egov.plugin::_confirmSaveAttachment::warning: Đọc nội dung file không tồn tại!!! Cần check lại code.");
                        } else {
                            // TODO: Báo cáo lỗi khi xử lý đóng file, đề nghị save file và đóng file đang mở và thử lại.
                            console.log("egov.plugin::_confirmSaveAttachment::Error: lỗi không thể lưu nội dung file");
                        }
                    });
                } else {
                    egov.message.show(String.format("Vui lòng đóng phần mềm đang mở file {0} và thử lại.", file.get('Name'))
                        , "Thông báo", egov.message.messageButtons.Ok);
                }
            });
        },

        openFileUri: function (uri, name) {
            var tempFolderLoc = (new Date()).getTime().toString();
            var filePath = tempFolderLoc + "\\" + name;
            if (!egov.extension) {
                egov.extension = PluginFactory.getInstance();
            }

            var filedata = getBinary(uri);
            var base64 = base64Encode(filedata);
            egov.extension.writeFileBase64(filePath, base64, true, function (jsonResult) {
                // Neu ghi file thanh cong
                if (jsonResult.returnCode == 1) {
                    egov.extension.openFile(filePath);
                }
            });
        },

        //#endregion

        //#region MAC

        getMAC: function (success) {
            if (!egov.extension) {
                egov.extension = PluginFactory.getInstance();
            }

            if (!egov.extension) {
                egov.callback(success, "");
                return;
            }

            egov.extension.getMAC(function (result) {
                egov.callback(success, result.mac);
            });
        },

        //#endregion

        //#region Delete Folder

        deleteFolder: function (path, success) {
            egov.extension.deleteFolder(path, true, function (result) {
                egov.callback(success);
            });
        },

        //#endregion
    }

    function base64Encode(str) {
        var CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        var out = "", i = 0, len = str.length, c1, c2, c3;
        while (i < len) {
            c1 = str.charCodeAt(i++) & 0xff;
            if (i == len) {
                out += CHARS.charAt(c1 >> 2);
                out += CHARS.charAt((c1 & 0x3) << 4);
                out += "==";
                break;
            }
            c2 = str.charCodeAt(i++);
            if (i == len) {
                out += CHARS.charAt(c1 >> 2);
                out += CHARS.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                out += CHARS.charAt((c2 & 0xF) << 2);
                out += "=";
                break;
            }
            c3 = str.charCodeAt(i++);
            out += CHARS.charAt(c1 >> 2);
            out += CHARS.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
            out += CHARS.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
            out += CHARS.charAt(c3 & 0x3F);
        }
        return out;
    }

    function getBinary(file) {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", file, false);
        xhr.overrideMimeType("text/plain; charset=x-user-defined");
        xhr.send(null);
        return xhr.responseText;
    }

    window.Plugin = EgovPlugin;
    return EgovPlugin;
})();