define([
    egov.template.document.scan
],
function (ScanTemplate) {
    "use strict";

    var _resource = egov.resources;

    var currentUrl = "";
    var currentExt = "";
    var currentWidth = 0;
    var currentHeight = 0;
    var currentPage = 0;
    var allImages = [];
    var fitWidth = 250;
    var jcropApi;


    var x, y, x2, y2, w, h;
    var scanDialogTemplate;
    var mapImageFormat = { "2": '.jpg', "13": '.png', "25": '.gif', "18": '.tif', "0": '.bmp', "PDF": '.pdf', "DOC": '.doc' };
    var waitingScanPacket = [];

    var setJcrop = function () {
        require(['jcrop'], function () {
            jcropApi = $.Jcrop('#imageScan', {
                onChange: setPosition,
                onSelect: setPosition,
                bgColor: 'black',
                bgOpacity: .9
            });
        })
    };

    var setPosition = function (c) {
        x = c.x;
        y = c.y;
        x2 = c.x2;
        y2 = c.y2;
        w = c.w;
        h = c.h;
    };

    var Scanner = Backbone.View.extend({

        $scan: $.tmpl(ScanTemplate),

        el: 'body',

        events: {
            "click #acquire": "acquire",
            "click #removeAllImageScan": "removeAllImageScan",
            "click #removeImageScan": "removeImageScan",
            "click #preImage": "preImage",
            "click #nextImage": "nextImage",
            "click #rotateLeft": "rotateLeft",
            "click #rotateRight": "rotateRight",
            "click #zoomIn": "zoomIn",
            "click #zoomOut": "zoomOut",
            "click #setActualSize": "setActualSize",
            "click #crop": "crop",
            "click #setBrightnessUp": "setBrightnessUp",
            "click #setBrightnessDown": "setBrightnessDown",
            "click #reloadListScanner": "reloadListScannerEvent",
            "click #setContrastUp": "setContrastUp",
            "click #setContrastDown": "setContrastDown",
            "change #addToContent": "addToContent"
        },

        /// <summary>Khởi tạo</summary>
        initialize: function (options) {
            var that = this;
            this.isPacket = options.isPacket;
            this.toolbar = options.parent;
            this.doc = options.parent.document;
            this.docModel = options.parent.document.model;
            this.isLoadedScanner = options.isLoaded;
            Plugin.appendPlugin(function () {
                that.render(that.docModel.get("DocumentCopyId"));
            });
            egov.views.scanner = this;
        },

        render: function (imageName) {
            /// <summary>
            /// Render dialog scan
            /// </summary>
            /// <param name="imageName">Tên ảnh, nếu văn bản đã có ID thì lấy theo ID, không thì mặc định là "image"</param>
            var that = this;
            if (that.isLoadedScanner) {
                that.reloadListScanner(false);
            } else {
                that.reloadListScanner(true);
                that.isLoadedScanner = true;
            }
            currentUrl = "";
            currentExt = "";
            currentWidth = 0;
            currentHeight = 0;
            currentPage = 0;
            allImages = [];

            $("#imagePreviewPanel").html("");
            that.toggeScanDialog({
                height: '450px',
                width: '800px',
                draggable: true,
                modal: true,
                resizable: true,
                title: "eGovCloud - Quét tài liệu",
                buttons: [
                    {
                        text: _resource.common.addButton,
                        className: 'btn-primary',
                        click: function () {
                            // Tesst
                            // allImages = [{ fileName: "Scan\\Image_020181019145345.jpg" }, { fileName: "Scan\\Image_020181019145537.jpg" }];

                            if (allImages && allImages.length > 0) {
                                if (that.isPacket) {
                                    that.uploadImageScanPacket()
                                }
                                else {
                                    var images = _.filter(allImages, function (item) {
                                        return item.addToContent;
                                    });

                                    if (images.length > 0) {
                                        that.insertImageToContent(images);
                                    }

                                    that.uploadImageScan();
                                }
                            }

                            that.toggeScanDialog("destroy");
                        }
                    },
                    {
                        text: _resource.common.closeButton,
                        className: 'btn-close',
                        click: function () {
                            that.toggeScanDialog("destroy");
                        }
                    }
                ]
            });
            that.$("#filename").val(imageName ? imageName : 'vanbanden');
            that.$("#pagePosition").text('Trang: 0/0');
            that.$("#addToContent").prop('checked', false);
            that.$("#preImage, #nextImage, #addToContent, #removeImageScan, #removeAllImageScan").attr("disabled", "disabled");
            if (jcropApi) {
                jcropApi.destroy();
                that.$("#imageScan").remove();
            }
        },

        uploadImageScanPacket: function () {
            var allImagesTransfer = [],
                that = this;
            $.each(allImages, function (index, item) {
                var filename = that.$("#filename").val() + '_' + (that.doc.attachments.model.length + index) + mapImageFormat[that.$("#imageformat").val()];
                egov.extension.transferImage(item.url, that.$("#imageformat").val(), function (base64) {
                    if (base64 !== '') {
                        allImagesTransfer.push({ name: filename, value: base64 });
                    }
                });
            });
            if (allImagesTransfer.length > 0) {
                that.uploadImageScanPacketBus(allImagesTransfer);
            }
            allImages = [];
        },

        uploadImageScanPacketBus: function (allImagesTransfer) {
            var doc = this.docModel, that = this;
            var totalSelectedFiles = 0;
            $.each(allImagesTransfer, function (index, value) {
                if (!egov.setting.acceptFileTypes.test(value.name)) {
                    egov.message.warning(value.name + ": " + egov.resources.file.notAcceptFileTypes);
                } else {
                    value.id = "#temp" + totalSelectedFiles;
                    totalSelectedFiles++;
                }
            });
            egov.request.uploadTempScan({
                data: {
                    files: JSON.stringify(allImagesTransfer)
                },
                success: function (result) {
                    $.each(result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.message.error(file.name + ": " + file.error);
                        } else {
                            var newAttachment = new egov.models.attachment({
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: file.extension,
                                Versions: [{
                                    Version: 1,
                                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                                }],
                                isNew: true,
                                fileData: file.data
                            });
                            waitingScanPacket.push(newAttachment);
                            //x.attachments.model.add(newAttachment);
                        }
                    });
                    if (waitingScanPacket.length > 0) {
                        that.createDocForScanPacket();
                    }
                }
            });
        },

        createDocForScanPacket: function () {
            if (waitingScanPacket.length > 0) {
                var doc = this.docModel;
                var newAttachment = waitingScanPacket[0];
                egov.views.home.tab.addDocument(doc.get("DocTypeId"), doc.get("Compendium"), undefined, true, newAttachment);
                waitingScanPacket.splice(0, 1);
            }
        },

        uploadImageScan: function () {
            /// <summary>
            /// Kiểm tra xem người dùng lưu file dạng gì, xử lý và đẩy cho BUS upload
            /// </summary>
            var allImagesTransfer = [],
                that = this;
            var isPdf = that.$("#imageformat").val() === 'PDF';

            if (isPdf) {
                if (allImages.length > 0) {
                    var filenamePdf = that.$("#filename").val() + ".pdf";
                    var fileNames = _.map(_.pluck(allImages, 'fileName'), function (f) { return f.replace(".bmp", ".jpg"); });

                    egov.extension.convertToPdf(fileNames, function (result) {
                        var pdfPath = result.fileArr.fileName;
                        egov.extension.readFileBase64(pdfPath, function (readResult) {
                            var base64 = readResult.base64;
                            that.uploadImageScanBus([{ name: filenamePdf, value: base64 }]);
                        });
                    });
                }
            } else {
                $.each(allImages, function (index, item) {
                    var filename = that.$("#filename").val() + '_' + (that.doc.attachments.model.length + index) + mapImageFormat[that.$("#imageformat").val()];

                    egov.extension.transferImage(item.fileName, that.$("#imageformat").val(), function (base64) {
                        if (base64 !== '') {
                            allImagesTransfer.push({ name: filename, value: base64 });
                        }
                    });
                });
            }

            if (allImagesTransfer.length > 0) {
                that.uploadImageScanBus(allImagesTransfer);
            }

            allImages = [];
        },

        uploadImageScanBus: function (allImagesTransfer) {
            /// <summary>
            /// Upload file scan lên server, bind danh sách file theo từng văn bản.
            /// </summary>
            /// <param name="allImagesTransfer">Danh sách file</param>
            var that = this.toolbar;
            var totalSelectedFiles = 0;
            egov.message.processing(egov.common.processing);
            $.each(allImagesTransfer, function (index, value) {
                if (!egov.setting.acceptFileTypes.test(value.name)) {
                    // egov.message.warning(value.name + ": " + egov.resources.file.notAcceptFileTypes);
                } else {
                    value.id = "#temp" + totalSelectedFiles;
                    totalSelectedFiles++;
                }
            });

            egov.request.uploadTempScan({
                data: {
                    files: JSON.stringify(allImagesTransfer)
                },
                success: function (result) {
                    $.each(result, function (index, file) {
                        if (file.error !== "") {
                            $(data.id).remove();
                            egov.message.error(file.name + ": " + file.error);
                        } else {
                            var newAttachment = new egov.models.attachment({
                                Id: file.key,
                                Name: file.name,
                                Size: file.size,
                                Extension: file.extension,
                                Versions: [{
                                    Version: 1,
                                    User: egov.setting.fullName + " (" + egov.setting.userName + ")"
                                }],
                                isNew: true,
                            });
                            that.document.attachments.model.add(newAttachment);
                        }
                    });
                }
            });
        },

        insertImageToContent: function (images, type) {
            /// <summary>
            /// Thêm ảnh scan vào nội dung văn bản
            /// </summary>
            /// <param name="images">danh sách ảnh</param>
            /// <param name="type"></param>
            var that = this;
            var $form = that.doc.$("#divContent>div");
            var scanContent = "";
            $.each(images, function (index, item) {
                var w = item.width > 800 ? 800 : item.width;
                if (item.addToContent) {
                    var $img = '<img width="' + w + '" src="data:image/' + item.ext + ';base64,' + item.base64 + '" />';
                    $form.prepend('<div><p>' + $img + '</p></div>');
                    scanContent += '<div><p>' + $img + '</p></div>';
                }
            });

            that.doc.isInsertedImage = true;
            that.docModel.get("DocumentContents")[0].Content += scanContent;
            that.doc._renderForm();
        },

        reloadListScannerEvent: function (e) {
            /// <summary>
            /// Event click nút reload danh sách máy scan
            /// </summary>
            /// <param name="e"></param>
            this.reloadListScanner(true);
        },

        reloadListScanner: function (reload) {
            /// <summary>
            /// Lấy danh sách máy scan kết nối
            /// </summary>
            /// <param name="reload"></param>
            var that = this;
            that.$scan.find("#listScanner").empty();
            that.$scan.find("#acquire").attr('disabled', 'disabled');

            reload = egov.extension.isChrome ? '' + reload : reload;
            egov.extension.getAllScanner(false, function (result) {
                var listScanner = JSON.parse(result.scanners)
                if (listScanner && listScanner.length > 0) {
                    that.$scan.find("#acquire").removeAttr('disabled');
                    $.each(listScanner, function (index, item) {
                        that.$scan.find("#listScanner").append("<option value='" + index + "'>" + item + "</option>");
                    });
                }
            });

        },

        toggeScanDialog: function (settings) {
            /// <summary>
            /// Toggle dialog scan theo settings
            /// </summary>
            /// <param name="settings"></param>
            this.$scan.dialog(settings);
        },

        acquire: function () {
            /// <summary>
            /// Scan ảnh
            /// </summary>
            var that = this;
            var showImage = function (result) {
                if (result.returnCode === -1) {
                    that._showMessage("Không thế scan: vui lòng kiểm tra lại thiết bị của bạn.");
                    return;
                }

                that._parseScanFiles(result.items, function (result) {
                    that._hideMessage();
                    allImages = result;
                    var currentImage = allImages[0];
                    var $img = $('<img style="width: 100%;" id="imageScan" src="data:image/' + currentImage.ext + ';base64,' + currentImage.base64 + '" />');
                    $("#imagePreviewPanel").html($img);

                    currentPage = 0;

                    $("#pagePosition").text('Trang: ' + (currentPage + 1) + '/' + allImages.length);

                    if (allImages.length > 1) {
                        $("#nextImage").removeAttr("disabled");
                        $("#preImage").attr("disabled", "disabled");
                    }
                    $("#addToContent, #removeImageScan, #removeAllImageScan").removeAttr("disabled");
                });
            }

            var hasUseScannerUi = $("#showui").prop("checked");
            var scanner = $("#listScanner").val();
            var scanType = $("#pixeltype").val();
            var dpi = $("#resolution").val();
            var twoSideScan = $("#duplex").prop("checked");

            this._showMessage("Đang chờ máy scan.");
            egov.extension.acquire(hasUseScannerUi, scanner, scanType, dpi, twoSideScan, showImage);
        },

        _parseScanFiles: function (images, success) {
            this._showMessage("Đang lấy ảnh.");
            var imageCount = images.length;
            var idx = 0;
            _.each(images, function (img) {
                img.ext = img.fileName.indexOf(".bmp", currentUrl.length - 4) !== -1 ? "bmp" : "jpg";
                egov.extension.readFileBase64(img.fileName, function (result) {
                    img.base64 = result.base64;
                    if (idx === imageCount - 1) {
                        success(images);
                    }
                    idx++;
                });
            });
        },

        removeImageScan: function () {
            /// <summary>
            /// Xóa ảnh ở trang hiện tại
            /// </summary>
            if (allImages.length > 0) {
                var oldLength = allImages.length;
                egov.extension.cancelTransferImage(allImages[currentPage].url);
                allImages.splice(currentPage, 1);
                if (currentPage === oldLength - 1) {
                    if (currentPage > 0) {
                        currentPage--;
                        this.showImageByCurrentPage();
                    } else {
                        jcropApi.destroy();
                        this.$("#imageScan").remove();
                        this.$("#pagePosition").text('Trang: 0/0');
                    }
                } else {
                    this.showImageByCurrentPage();
                }
                if (currentPage === 0) {
                    this.$("#preImage").attr("disabled", "disabled");
                    if (allImages.length <= 1) {
                        this.$("#nextImage").attr("disabled", "disabled");
                    }
                } else if (currentPage === allImages.length - 1) {
                    this.$("#nextImage").attr("disabled", "disabled");
                }
                if (allImages.length === 0) {
                    currentUrl = "";
                    currentExt = "";
                    currentWidth = 0;
                    currentHeight = 0;
                    currentPage = 0;
                    this.$("#removeImageScan, #removeAllImageScan, #addToContent").attr("disabled", "disabled");
                }
            }
        },

        removeAllImageScan: function () {
            /// <summary>
            /// Xóa tất cả ảnh đã scan được
            /// </summary>
            if (allImages.length > 0) {
                jcropApi.destroy();
                this.$("#imageScan").remove();
                this.$("#pagePosition").text('Trang: 0/0');
                currentUrl = "";
                currentExt = "";
                currentWidth = 0;
                currentHeight = 0;
                currentPage = 0;
                $.each(allImages, function (index, item) {
                    egov.extension.cancelTransferImage(item.url);
                });
                allImages = [];
                this.$("#removeImageScan, #removeAllImageScan, #nextImage, #preImage, #addToContent").attr("disabled", "disabled");
            }
        },

        showImageByCurrentPage: function () {
            /// <summary>
            /// Hiển thị ảnh theo trang hiện tại
            /// </summary>
            var image = allImages[currentPage];
            currentUrl = image.url;
            currentExt = image.ext;
            currentWidth = image.width;
            currentHeight = image.height;
            var $img = $('<img  id="imageScan" style="width: 100%;" src="data:image/' + image.ext + ';base64,' + image.base64 + '" />');
            this.$("#imagePreviewPanel").html($img);

            this.$("#addToContent").prop('checked', image.addToContent);
            this.$("#pagePosition").text('Trang: ' + (currentPage + 1) + '/' + allImages.length);
        },

        preImage: function () {
            /// <summary>
            /// Ảnh trước
            /// </summary>
            if (currentPage >= 1) {
                currentPage--;
                this.showImageByCurrentPage();
                this.$("#nextImage").removeAttr("disabled");
                if (currentPage === 0) {
                    this.$("#preImage").attr("disabled", "disabled");
                }
            }
        },

        nextImage: function () {
            /// <summary>
            /// Ảnh sau
            /// </summary>
            if (currentPage <= allImages.length - 2) {
                currentPage++;
                this.showImageByCurrentPage();
                this.$("#preImage").removeAttr("disabled");
                if (currentPage === allImages.length - 1) {
                    this.$("#nextImage").attr("disabled", "disabled");
                }
            }
        },

        rotateLeft: function () {
            /// <summary>
            /// Quay trái
            /// </summary>
            this._setRotate(90);
        },

        rotateRight: function () {
            /// <summary>
            /// Quay phải
            /// </summary>
            this._setRotate(-90);
        },

        zoomIn: function () {
            /// <summary>
            /// Phóng to
            /// </summary>
            this._setZoom(100);
        },

        zoomOut: function () {
            /// <summary>
            /// Thu nhỏ
            /// </summary>
            this._setZoom(-100);
        },

        setActualSize: function () {
            /// <summary>
            /// Đưa ảnh về kích thước ban đầu
            /// </summary>
            if (currentUrl !== '') {
                jcropApi.destroy();
                this.$("#imageScan").css({
                    'width': currentWidth + "px",
                    'height': currentHeight + "px"
                });
                this.$("#imageScan").attr("width", currentWidth + "px").attr("height", currentHeight + "px");
                setJcrop();
            }
        },

        crop: function () {
            /// <summary>
            /// Cắt ảnh
            /// </summary>
            if (currentUrl !== '') {
                var width = this.$("#imageScan").width();
                var height = this.$("#imageScan").height();

                var that = this;
                egov.extension.imageCrop(currentUrl, x, y, x2, y2, width, height, function (data) {
                    if (data) {
                        jcropApi.destroy();
                        egov.extension.readFileBase64(currentUrl, 0, -1, function (base64) {
                            var $img = $('<img width="' + fitWidth + 'px" height="' + ((fitWidth / w) * h) + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                            that.$("#imagePreviewPanel").html($img);
                            setJcrop();
                            currentWidth = (w / width) * currentWidth;
                            currentHeight = (h / height) * currentHeight;
                            var image = allImages[currentPage];
                            image.width = currentWidth;
                            image.height = currentHeight;
                            image.data = base64;
                        }, true);
                    }
                });
            }
        },

        setBrightnessUp: function () {
            /// <summary>
            /// Tăng độ sáng
            /// </summary>
            this._setBrightness(10);
        },

        setBrightnessDown: function () {
            /// <summary>
            /// Giảm độ sáng
            /// </summary>
            this._setBrightness(-10);
        },

        setContrastUp: function () {
            /// <summary>
            /// Tăng độ tương phản
            /// </summary>
            this._setContrast(10);
        },

        setContrastDown: function () {
            /// <summary>
            /// Giảm độ tương phản
            /// </summary>
            this._setContrast(-10);
        },

        addToContent: function () {
            /// <summary>
            /// Đưa ảnh vào nội dung văn bản
            /// </summary>
            var image = allImages[currentPage];
            image.addToContent = this.$("#addToContent").prop('checked');
        },

        _showMessage: function (message) {
            this.$('.scan-info').text(message);
        },

        _hideMessage: function () {
            this.$('.scan-info').text('');
        },

        //#region private

        _setContrast: function (value) {
            if (currentUrl !== '') {
                var that = this;
                egov.extension.imageAdjustContrast(currentUrl, value, function (data) {
                    if (data) {
                        var width = that.$("#imageScan").width();
                        var height = that.$("#imageScan").height();
                        jcropApi.destroy();
                        egov.extension.readFileBase64(currentUrl, 0, -1, function (base64) {
                            var $img = $('<img width="' + width + '" height="' + height + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                            that.$("#imagePreviewPanel").html($img);
                            setJcrop();
                            allImages[currentPage].data = base64;
                        }, true);
                    }

                })
            }
        },

        _setZoom: function (value) {
            if (currentUrl !== '') {
                var width = $("#imageScan").width();
                var height = $("#imageScan").height();
                var zoomInWidth = width + value;
                var zoomInHeight = zoomInWidth / (width / height);
                if (zoomInWidth > 0 && zoomInHeight > 0) {
                    jcropApi.destroy();
                    this.$("#imageScan").css({
                        'width': zoomInWidth + "px",
                        'height': zoomInHeight + "px"
                    });
                    this.$("#imageScan").attr("width", zoomInWidth + "px").attr("height", zoomInHeight + "px");
                    setJcrop();
                }
            }
        },

        _setRotate: function (angle) {

            if (currentUrl !== '') {
                var that = this;
                egov.extension.imageRotate(currentUrl, angle, function (data) {
                    if (data) {
                        jcropApi.destroy();
                        var width = that.$("#imageScan").width();
                        var height = that.$("#imageScan").height();

                        egov.extension.readFileBase64(currentUrl, 0, -1, function (base64) {
                            var $img = $('<img width="' + height + 'px" height="' + width + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                            that.$("#imageScan").remove();
                            that.$("#imagePreviewPanel").html($img);
                            setJcrop();
                            var newCurrentWidth = currentHeight;
                            currentHeight = currentWidth;
                            currentWidth = newCurrentWidth;
                            var image = allImages[currentPage];
                            image.width = currentWidth;
                            image.height = currentHeight;
                            image.data = base64;
                        }, true);
                    }
                })
            }
        },

        _setBrightness: function (value) {
            if (currentUrl !== '') {
                var that = this;
                egov.extension.imageAdjustBrightness(currentUrl, value, function (data) {
                    var width = that.$("#imageScan").width();
                    var height = that.$("#imageScan").height();
                    jcropApi.destroy();
                    egov.extension.readFileBase64(currentUrl, 0, -1, function (base64) {
                        var $img = $('<img width="' + width + '" height="' + height + 'px" id="imageScan" src="data:image/' + currentExt + ';base64,' + base64 + '" />');
                        that.$("#imagePreviewPanel").html($img);
                        setJcrop();
                        allImages[currentPage].data = base64;
                    }, true);
                })
            }
        },

        //#endregion
    });

    var resizeImageBase64ForInsertToContent = function (base64, type) {
        var tmpImg = new Image();
        tmpImg.src = "data:image/" + type + ";base64," + base64;
        var fitWidthResize = 768;
        var newWidth = tmpImg.width;
        var newHeight = tmpImg.height;
        if (tmpImg.width >= fitWidthResize) {
            newWidth = fitWidthResize;
            newHeight = (fitWidthResize / tmpImg.width) * tmpImg.height;
        }
        return '<img width="' + newWidth + 'px" height="' + newHeight + 'px" src="data:image/' + type + ';base64,' + base64 + '" />';
    }

    return Scanner;
});