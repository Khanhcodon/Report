/**
 * @author nobita
 * @param {type}
 *            editor
 * @returns {BoldControl} Lớp vẽ insert image
 */

InsertImageControl = function (editor) {
    EditorControl.call(this, editor);
    this.dialog = null;
    this.isDialogShow = false;
    this.currentImg = null;
    this.ext = "";
};
InsertImageControl.prototype = new EditorControl;
InsertImageControl.constructor = InsertImageControl;

InsertImageControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div id="'
			+ this.id
			+ '" tabIndex="-1" class="bmeditor-inserimagebutton">'
			+ '<img class="image" id="insertImage_'
			+ this.id
			+ '" src="' + Config.Image.pic + '" alt="Chèn ảnh" title="Chèn ảnh" style="margin-top : 2px">'
			+ '</div>'
            + '<input type="file" id="insertBtn' + this.id + '" class="insertImage hidden" data-url="/Attachment/UploadTemp" multiple name="files"/>';
    return html;
};
InsertImageControl.prototype.action = function () {
};

/**
 * insert image
 */
InsertImageControl.prototype.insertImage = function (img) {
    var iframe = document.getElementById(this._editor._editorId);

    var doc = iframe.contentDocument || iframe.contentWindow.document;
    var id = "uploadingImage";
    img = "<img src='" + img + "' id='" + id + "' >";
    if (window.getSelection) {
        this._editor.execCommand("insertHTML", false, img);
    } else {
        var e = document.getElementById(this._editor.getId()).contentWindow.document;
        var range = e.selection.createRange();
        range.pasteHTML(img);
        range.collapse(false);
        range.select();
    }
    return doc.getElementById(id);
};

InsertImageControl.prototype.initDialog = function (x, y) {
    var _this = this;
    this.dialog = new YAHOO.widget.SimpleDialog("yui-picker-panel", {
        width: "800px",
        close: false,
        visible: false,
        draggable: true,
        constraintoviewport: true,
        modal: true,
        x: x - 20,
        y: y + 10,
        autofillheight: "footer",
        fixedcenter: true,
        buttons: [
				{
				    text: "Chọn",
				    handler: function () {
				        _this.isDialogShow = false;
				        if ($('.imageUploadedSelected').length > 0) {
				            var activeImage = $('.imageUploadedSelected')[0];
				            var src = $(activeImage).attr('src');
				            $(_this._editor.curentElementToInsert).prepend(
									'<img src="' + src + '">');
				        }
				        this.cancel();
				    },
				    isDefault: true
				}, {
				    text: "Bỏ qua",
				    handler: function () {
				        _this.isDialogShow = false;
				        this.cancel();
				    }
				}]
    });
    this.dialog.renderEvent.subscribe(function () {
        _this.isDialogShow = true;
        _this.initUpload();
    });

    this.dialog.setHeader('<div class = "rhd"></div><div class = "rft"></div>'
			+ "Thêm hình ảnh");
    var body = '<div>';
    body += '<table cellspacing="10" cellpadding="10"><tr>';
    body += '<td>';
    body += '<div><input type="radio" name="rdupload" id="rduploadMycomputer" checked="true" > '
			+ loadFromComputerMsg + '<div>';
    body += '<div><input type="radio" name="rdupload" id="rduploadUrl" > '
			+ linkFromWebMsg + '<div>';
    body += '</td>';
    body += '<td>';
    body += '<div id="imageInternalDialog">';
    body += '<div><input type="file" id="uploadInternalInput"></div>';
    body += '</div>';
    body += '<div id="imageExternalDialog">';
    body += '<div><input type="text" id="uploadExtenalInput"></div>';
    body += '<div>Nếu URL của bạn đúng, bạn sẽ nhìn thấy một hình ảnh xem trước tại đây.<p> Các hình ảnh lớn sẽ cần vài phút để hiển thị.</div>';
    body += '</div>';
    body += '</td>';
    body += '</tr></table></div>';
    this.dialog.setBody(body);
    this.dialog.render(document.body);
};
InsertImageControl.prototype.setSelectedStyle = function (type, value) {
};

/**
 *
 */
InsertImageControl.prototype.initUpload = function () {
    var _this = this,
        src = Config.URL_IMAGE + "images/progress.gif";

    if (!egov) {
        console.log("Egov undefined");
        return;
    }
    $("#" + _this.id).on("click", function (e) {
        $("#insertBtn" + _this.id).click();
    });

    $("#insertBtn" + _this.id).fileupload({
        dataType: 'json',
        dropZone: $("#divFiles"),
        add: function (e, data) {
            _this.insertImage(src);
            data.submit();
        },
        start: function () {
        },
        stop: function () {
        },
        done: function (e, data) {
            $.each(data.result, function (index, file) {
                if (file.error !== "") {
                    $(data.id).remove();
                    egov.pubsub.publish(egov.events.status.error, file.name + ": " + file.error);
                } else {
                    var content = _this._editor.getContent();
                    egov.request.downloadAttachmentTemp({
                        data: { id: file.key },
                        success: function (data) {
                            var img = JSON.parse(data);
                            if (img) {
                                if (!img.error) {
                                    var newId = _.random(10000, 99999);
                                    $img = '<img src="data:image/.' + file.extension + ';base64,' + img.content + '" id="' + newId + '"/>';
                                    content = content.replace('<img src="' + src + '" id="uploadingImage">', $img);
                                    _this._editor.setContent(content);
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
                }
            });
        },
        fail: function (e, data) {
        }
    });
};

/**
 * Lớp vẽ điều khiển làm chữ béo Bold
 */
BoldControl = function (editor) {
    EditorControl.call(this, editor);
};
BoldControl.prototype = new EditorControl;
BoldControl.constructor = BoldControl;
BoldControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div id="'
			+ this.id
			+ '" tabIndex="-1" class="bmeditor-boldbutton">'
			+ '<img class="image" src="' + Config.Image.B + '" alt="Bold" title="Bold">'
			+ '</div>';
    return html;
};
BoldControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id)
			.click(
					function () {
					    if (navigator.userAgent.indexOf('Chrome') != -1) {
					        // _this._editor.focus();
					        if ($("#" + this.id + " img").attr("src") == Config.Image.Bp)// bỏ
					            // bold
					        {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor.execCommand("bold", false, true);
					            $("#" + this.id + " img").attr("src", Config.Image.B);
					            if (_this._editor.isCache) {
					                createCookie('Chat_bold', false);
					            }
					        } else// bold chữ
					        {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor.execCommand("bold", false, null);
					            $("#" + this.id + " img").attr("src", Config.Image.Bp);
					            if (_this._editor.isCache) {
					                createCookie('Chat_bold', true);
					            }
					        }
					    } else {
					        if ($("#" + this.id + " img").attr("src") == Config.Image.Bp)// bỏ
					            // bold
					        {
					            $("#" + this.id + " img").attr("src", Config.Image.B);
					            if (_this._editor.isCache) {
					                createCookie('Chat_bold', false);
					            }

					            EditorControl.execCommand(_this._editor,
										"bold", true);
					        } else// bold chữ
					        {
					            $("#" + this.id + " img").attr("src", Config.Image.Bp);
					            if (_this._editor.isCache) {
					                createCookie('Chat_bold', true);
					            }

					            EditorControl.execCommand(_this._editor,
										"bold", null);
					        }
					    }
					});
};
BoldControl.prototype.setSelectedStyle = function (type, value) {
    if (type.toLowerCase() == "bold") {
        if (value)
            $("#" + this.id + " img").attr("src", Config.Image.Bp);
        else
            $("#" + this.id + " img").attr("src", Config.Image.B);
    }
};
BoldControl.prototype.getClearContent = function () {
    if ($("#" + this.id + " img").attr("src") == Config.Image.Bp)
        this._editor.execCommand("bold", false, true);
};

/**
 *
 * Lớp vẽ điều khiển làm chữ gạch
 */
StrikeControl = function (editor) {
    EditorControl.call(this, editor);
};
StrikeControl.prototype = new EditorControl;
StrikeControl.constructor = BoldControl;
StrikeControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div id="'
			+ this.id
			+ '" tabIndex="-1" class="bmeditor-strikebutton">'
			+ '<img class="image" src="' + Config.Image.strikeThrough + '" alt="strikeThrough" title="strikeThrough">'
			+ '</div>';
    return html;
};
StrikeControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id)
			.click(
					function () {
					    if (navigator.userAgent.indexOf('Chrome') != -1) {
					        if ($("#" + this.id + " img").attr("src") == Config.Image.strikeThrough) {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor.execCommand("strikeThrough",
										false, true);

					            $("#" + this.id + " img").attr("src", Config.Image.strikeThrough);
					        } else {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor.execCommand("strikeThrough",
										false, null);

					            $("#" + this.id + " img").attr("src", Config.Image.strikeThrough);
					        }
					    } else {
					        _this._editor.focus();
					        if ($("#" + this.id + " img").attr("src") == Config.Image.strikeThrough) {
					            $("#" + this.id + " img").attr("src", Config.Image.strikeThrough);
					            EditorControl.execCommand(_this._editor,
										"strikeThrough", true);
					        } else {
					            $("#" + this.id + " img").attr("src", Config.Image.strikeThrough);
					            EditorControl.execCommand(_this._editor,
										"strikeThrough", null);
					        }
					    }
					});
};
StrikeControl.prototype.setSelectedStyle = function (type, value) {
    if (type.toLowerCase() == "strike") {
        if (value)
            $("#" + this.id + " img").attr("src", Config.Image.strikeThrough);
        else
            $("#" + this.id + " img").attr("src", Config.Image.strikeThrough);
    }
};
StrikeControl.prototype.getClearContent = function () {
};

/**
 * Separator
 */

/**
 * Lớp vẽ điều khiển clean
 */
SeparatorControl = function (editor) {
    EditorControl.call(this, editor);
};
SeparatorControl.prototype = new EditorControl;
SeparatorControl.constructor = SeparatorControl;
SeparatorControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '"  tabIndex="-1" class="bmeditor-separator">' + '</div>';
    return html;
};
SeparatorControl.prototype.action = function () {
};
SeparatorControl.prototype.setSelectedStyle = function (type, value) {
};

SeparatorControl.prototype.getClearContent = function () {
};

/**
 * Lớp vẽ điều khiển clean
 */
CleanControl = function (editor) {
    EditorControl.call(this, editor);
};
CleanControl.prototype = new EditorControl;
CleanControl.constructor = CleanControl;
CleanControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '"  tabIndex="-1" class="bmeditor-imagebutton">'
			+ '<img src="' + Config.URL_IMAGE + 'icons/clean.gif" title="Clean">' + '</div>';
    return html;
};
CleanControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        _this._editor.clear();
        _this._editor.focus();
    });
};
CleanControl.prototype.setSelectedStyle = function (type, value) {
};

CleanControl.prototype.getClearContent = function () {
};

/**
 * Lớp vẽ điều khiển Attach File
 */
MoreControl = function (editor) {
    EditorControl.call(this, editor);
    this.editor = editor;
};
MoreControl.prototype = new EditorControl;
MoreControl.constructor = MoreControl;
MoreControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '"  tabIndex="-1" class="bmeditor-morebutton" style="width : 15px;">'
			+ '<img src="' + Config.Image.More + '" title="More">' + '</div>';
    return html;
};
MoreControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(
			function () {
			    if ($(_this.editor._selector).find('.bmeditor-imagebutton')
						.parent('td').css('display') === "inline-block") {
			        $(_this.editor._selector).find('.editor_font_select')
							.parent('td').css('display', 'none');
			        $(_this.editor._selector).find('.editor_fontsize_select')
							.parent('td').css('display', 'none');
			        $(_this.editor._selector).find('.editor-bgcolor').parent(
							'td').css('display', 'none');
			        $(_this.editor._selector).find('.bmeditor-imagebutton')
							.parent('td').css('display', 'none');
			        $(_this.editor._selector)
							.find('.editor_formatblock_select').parent('td')
							.css('display', 'none');
			        $(_this.editor._selector).find(
							'.bmeditor-insertTableButton').parent('td').css(
							'display', 'none');
			        $("#" + _this._id).find('img').attr('src', Config.Image.More);
			        //if (navigator.userAgent.indexOf('Chrome') != -1)
			        _this._editor.focus();
			    } else {
			        $(_this.editor._selector).find('.editor_font_select')
							.parent('td').css('display', 'inline-block');
			        $(_this.editor._selector).find('.editor_fontsize_select')
							.parent('td').css('display', 'inline-block');
			        $(_this.editor._selector).find('.editor-bgcolor').parent(
							'td').css('display', 'inline-block');
			        $(_this.editor._selector).find('.bmeditor-imagebutton')
							.parent('td').css('display', 'inline-block');
			        $(_this.editor._selector)
							.find('.editor_formatblock_select').parent('td')
							.css('display', 'inline-block');
			        $(_this.editor._selector).find(
							'.bmeditor-insertTableButton').parent('td').css(
							'display', 'inline-block');
			        $("#" + _this._id).find('img').attr('src', Config.Image.MoreP);
			        //if (navigator.userAgent.indexOf('Chrome') != -1)
			        _this._editor.focus();
			    }
			});

};
MoreControl.prototype.setSelectedStyle = function (type, value) {
};

MoreControl.prototype.getClearContent = function () {
};

/**
 * Lớp vẽ điều khiển Attach File
 */
AttachControl = function (editor) {
    EditorControl.call(this, editor);
    this._editor = editor;
};
AttachControl.prototype = new EditorControl;
AttachControl.constructor = AttachControl;
AttachControl.prototype.genHtmlContent = function () {
    var html = '<div  id="' + this.genId()
			+ '"  tabIndex="-1" class="bmeditor-attachbutton">'
			+ '<img id="tb_attach' +
			+ '" src="' + Config.Image.attachIcon + '"  >' + '</div>';
    return html;
};
AttachControl.prototype.action = function () {
};
AttachControl.prototype.setSelectedStyle = function (type, value) {
};

AttachControl.prototype.getClearContent = function () {
};

/**
 * Lớp in nội dung
 */
PrintControl = function (editor) {
    EditorControl.call(this, editor);
};
PrintControl.prototype = new EditorControl;
PrintControl.constructor = CleanControl;
PrintControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.print + '" title="Print">' + '</div>';
    return html;
};
PrintControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id)
			.click(
					function () {
					    // alert(_this._editor.getContent());
					    var oPrntWin = window
								.open(
										"",
										"_blank",
										"width=450,height=470,left=400,top=100,menubar=yes,toolbar=no,location=no,scrollbars=yes");
					    oPrntWin.document.open();
					    oPrntWin.document
								.write("<!doctype html><html><head><title>Print<\/title><\/head><body onload=\"print();\">"
										+ _this._editor.getContent()
										+ "<\/body><\/html>");
					    oPrntWin.document.close();
					});
};
PrintControl.prototype.setSelectedStyle = function (type, value) {
};
PrintControl.prototype.getClearContent = function () {
};
/**
 * Lớp Undo
 */
UndoControl = function (editor) {
    EditorControl.call(this, editor);
};
UndoControl.prototype = new EditorControl;
UndoControl.constructor = UndoControl;
UndoControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '"  tabIndex="-1" class="bmeditor-imagebutton">'
			+ '<img src="' + Config.URL_IMAGE + 'js/editor/emo/107.gif" title="Undo">' + '</div>';
    return html;
};
UndoControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        _this._editor.focus();
        _this._editor.execCommand("undo", false, null);
    });
};
UndoControl.prototype.setSelectedStyle = function (type, value) {
};
UndoControl.prototype.getClearContent = function () {
};
/**
 * Lớp Redo
 */
RedoControl = function (editor) {
    EditorControl.call(this, editor);
};
RedoControl.prototype = new EditorControl;
RedoControl.constructor = RedoControl;
RedoControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.URL_IMAGE + 'icons/redo.gif" title="Redo">' + '</div>';
    return html;
};
RedoControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        _this._editor.focus();
        _this._editor.execCommand("redo", false, null);
    });
};
RedoControl.prototype.setSelectedStyle = function (type, value) {
};
RedoControl.prototype.getClearContent = function () {
};
/**
 * Lớp loại bỏ định dạng
 */
RemoveFormatControl = function (editor) {
    EditorControl.call(this, editor);
};
RemoveFormatControl.prototype = new EditorControl;
RemoveFormatControl.constructor = RemoveFormatControl;
RemoveFormatControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.format + '" title="Remove formatting">'
			+ '</div>';
    return html;
};
RemoveFormatControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (BmailEditor.getSelection(_this.getEditorId()) == "")
            _this._editor.focus();
        EditorControl.execCommand(_this._editor, "removeFormat", null);
    });
};
RemoveFormatControl.prototype.setSelectedStyle = function (type, value) {
};
RemoveFormatControl.prototype.getClearContent = function () {
};

/**
 * Xử lý bộ gõ
 */
VietkeyControl = function (editor) {
    EditorControl.call(this, editor);
    this.id = "";
};
VietkeyControl.prototype = new EditorControl;
VietkeyControl.constructor = VietkeyControl;
VietkeyControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div id="' + this.id
			+ '"  tabIndex="-1" class="bmeditor-vietkeybutton">'
			+ '<img src="' + Config.Image.vietkey + '" title="Bộ gõ">'
			+ '</div>';
    return html;
};
VietkeyControl.prototype.action = function () {
    var _this = this;
    $('#' + this.id).click(
			function () {
			    if (Config.viEnabled) {
			        Config.viEnabled = false;
			        $(".bmeditor-vietkeybutton img").attr('src', Config.Image.engkey);
			        _this._editor.focus();
			        if (Config.useVietKey)
			            Mudim.method = 0;
			    } else {
			        Config.viEnabled = true;
			        $(".bmeditor-vietkeybutton img").attr('src', Config.Image.vietkey);
			        _this._editor.focus();
			        if (Config.useVietKey)
			            Mudim.method = 2;
			    }
			});
};
VietkeyControl.prototype.setSelectedStyle = function (type, value) {
};
VietkeyControl.prototype.init = function () {
    if (Config.viEnabled) {
        $('#' + this.id + ' img').attr('src', Config.Image.vietkey);
        Mudim.method = 2;
    } else {
        $('#' + this.id + ' img').attr('src', Config.Image.engkey);
        Mudim.method = 0;
    }
};
VietkeyControl.prototype.getClearContent = function () {
};

/**
 * Tạo chữ in nghiêng
 */
ItalicControl = function (editor) {
    EditorControl.call(this, editor);
};
ItalicControl.prototype = new EditorControl;
ItalicControl.constructor = ItalicControl;
ItalicControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div id="' + this.id
			+ '"  tabIndex="-1" class="bmeditor-italicbutton">'
			+ '<img src="' + Config.Image.I + '" title="Italic">' + '</div>';
    return html;
};
ItalicControl.prototype.action = function () {
    var _this = this;

    $("#" + this._id)
			.click(
					function () {
					    if (navigator.userAgent.indexOf('Chrome') != -1) {
					        if ($("#" + this.id + " img").attr("src") == Config.Image.Ip) {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor
										.execCommand("italic", false, true);

					            $("#" + this.id + " img").attr("src",
										Config.Image.I);
					            if (_this._editor.isCache) {
					                createCookie('Chat_italic', false);
					                setPrivateGroup();
					            }
					        } else {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor
										.execCommand("italic", false, null);

					            $("#" + this.id + " img").attr("src", Config.Image.Ip);
					            if (_this._editor.isCache) {
					                createCookie('Chat_italic', true);
					                setPrivateGroup();
					            }
					        }
					    } else {
					        _this._editor.focus();

					        if ($("#" + this.id + " img").attr("src") == Config.Image.Ip) {
					            $("#" + this.id + " img").attr("src",
										Config.Image.I);
					            if (_this._editor.isCache) {
					                createCookie('Chat_italic', false);
					                setPrivateGroup();
					            }
					            EditorControl.execCommand(_this._editor,
										"italic", true);
					        } else {
					            $("#" + this.id + " img").attr("src", Config.Image.Ip);
					            if (_this._editor.isCache) {
					                createCookie('Chat_italic', true);
					                setPrivateGroup();
					            }
					            EditorControl.execCommand(_this._editor,
										"italic", null);
					        }
					    }
					});
};
ItalicControl.prototype.setSelectedStyle = function (type, value) {
    if (type.toLowerCase() == "italic") {
        if (value)
            $("#" + this.id + " img").attr("src", Config.Image.Ip);
        else
            $("#" + this.id + " img").attr("src", Config.Image.I);
    }
};

ItalicControl.prototype.getClearContent = function () {
    if ($("#" + this.id + " img").attr("src") == Config.Image.Ip)
        this._editor.execCommand("italic", false, true);
};

/**
 * Tạo chữ gạch chân
 */
UnderlineControl = function (editor) {
    EditorControl.call(this, editor);
};
UnderlineControl.prototype = new EditorControl;
UnderlineControl.constructor = UnderlineControl;
UnderlineControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div id="' + this.id
			+ '" tabIndex="-1"  class="bmeditor-underlinebutton">'
			+ '<img src="' + Config.Image.U + '" title="Underline">' + '</div>';
    return html;
};
UnderlineControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id)
			.click(
					function () {
					    if (navigator.userAgent.indexOf('Chrome') != -1) {
					        if ($("#" + this.id + " img").attr("src") == Config.Image.Up) {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor.execCommand("underline", false,
										true);

					            $("#" + this.id + " img").attr("src", Config.Image.U);
					            if (_this._editor.isCache) {
					                createCookie('Chat_underline', false);
					                setPrivateGroup();
					            }
					        } else {
					            if (BmailEditor.getSelection(_this.getEditorId()) == "")
					                _this._editor.focus();
					            _this._editor.execCommand("underline", false,
										null);

					            $("#" + this.id + " img").attr("src", Config.Image.Up);
					            if (_this._editor.isCache) {
					                createCookie('Chat_underline', true);
					                setPrivateGroup();
					            }
					        }
					    } else {
					        _this._editor.focus();
					        if ($("#" + this.id + " img").attr("src") == Config.Image.Up) {
					            $("#" + this.id + " img").attr("src", Config.Image.U);
					            if (_this._editor.isCache) {
					                createCookie('Chat_underline', false);
					                setPrivateGroup();
					            }
					            EditorControl.execCommand(_this._editor,
										"underline", true);
					        } else {
					            $("#" + this.id + " img").attr("src", Config.Image.Up);
					            if (_this._editor.isCache) {
					                createCookie('Chat_underline', true);
					                setPrivateGroup();
					            }

					            EditorControl.execCommand(_this._editor,
										"underline", null);
					        }
					    }
					});
};
UnderlineControl.prototype.setSelectedStyle = function (type, value) {
    if (type.toLowerCase() == "underline") {
        if (value)
            $("#" + this.id + " img").attr("src", Config.Image.Up);
        else
            $("#" + this.id + " img").attr("src", Config.Image.U);
    }
};
UnderlineControl.prototype.getClearContent = function () {
    if ($("#" + this.id + " img").attr("src") == Config.Image.Up)
        this._editor.execCommand("underline", false, true);
};
/**
 * Tạo Căn trái
 */
JustifyLeftControl = function (editor) {
    EditorControl.call(this, editor);
};
JustifyLeftControl.prototype = new EditorControl;
JustifyLeftControl.constructor = JustifyLeftControl;
JustifyLeftControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.justifyleft + '" title="Left align" style="margin-left:3px">'
			+ '</div>';
    return html;
};
JustifyLeftControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("justifyLeft", false, null);
        EditorControl.execCommand(_this._editor, "justifyLeft", null);
    });
};
JustifyLeftControl.prototype.setSelectedStyle = function (type, value) {
};
JustifyLeftControl.prototype.getClearContent = function () {
};
/**
 * Tạo Căn giữa
 */
JustifyCenterControl = function (editor) {
    EditorControl.call(this, editor);
};
JustifyCenterControl.prototype = new EditorControl;
JustifyCenterControl.constructor = JustifyCenterControl;
JustifyCenterControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '"  tabIndex="-1" class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.justifycenter + '" title="Center align" style="margin-left:5px">'
			+ '</div>';
    return html;
};
JustifyCenterControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("justifyCenter", false, null);
        EditorControl.execCommand(_this._editor, "justifyCenter", null);
    });
};
JustifyCenterControl.prototype.setSelectedStyle = function (type, value) {
};
JustifyCenterControl.prototype.getClearContent = function () {
};
/**
 * Tạo căn phải
 */
JustifyRightControl = function (editor) {
    EditorControl.call(this, editor);
};
JustifyRightControl.prototype = new EditorControl;
JustifyRightControl.constructor = JustifyRightControl;
JustifyRightControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.justifyright + '" title="Right align" style="margin-left:7px">'
			+ '</div>';
    return html;
};
JustifyRightControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("justifyRight", false, null);
        EditorControl.execCommand(_this._editor, "justifyRight", null);
    });
};
JustifyRightControl.prototype.setSelectedStyle = function (type, value) {
};
JustifyRightControl.prototype.getClearContent = function () {
};
/**
 * Tạo căn đều
 */
JustifyFullControl = function (editor) {
    EditorControl.call(this, editor);
};
JustifyFullControl.prototype = new EditorControl;
JustifyFullControl.constructor = JustifyFullControl;
JustifyFullControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.justifyfull + '" title="Right align" style="margin-left:9px">'
			+ '</div>';
    return html;
};
JustifyFullControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("justifyFull", false, null);
        EditorControl.execCommand(_this._editor, "justifyFull", null);
    });
};
JustifyFullControl.prototype.setSelectedStyle = function (type, value) {
};
JustifyFullControl.prototype.getClearContent = function () {
};
/**
 * Tạo order list
 */
OrderListControl = function (editor) {
    EditorControl.call(this, editor);
};
OrderListControl.prototype = new EditorControl;
OrderListControl.constructor = OrderListControl;
OrderListControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.numberedlist + '" title="Numbered list" style="margin-top:-1px">'
			+ '</div>';
    return html;
};
OrderListControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("insertOrderedList", false, null);
        EditorControl.execCommand(_this._editor, "insertOrderedList", null);
    });
};
OrderListControl.prototype.setSelectedStyle = function (type, value) {
};
OrderListControl.prototype.getClearContent = function () {
};
/**
 * Tạo unorder list
 */
UnorderListControl = function (editor) {
    EditorControl.call(this, editor);
};
UnorderListControl.prototype = new EditorControl;
UnorderListControl.constructor = UnorderListControl;
UnorderListControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.dottedlist + '" title="Dotted list">'
			+ '</div>';
    return html;
};
UnorderListControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("insertUnorderedList", false, null);
        EditorControl.execCommand(_this._editor, "insertUnorderedList", null);
    });
};
UnorderListControl.prototype.setSelectedStyle = function (type, value) {
};
UnorderListControl.prototype.getClearContent = function () {
};
/**
 * Tạo đẩy trái
 */
OutdentControl = function (editor) {
    EditorControl.call(this, editor);
};

OutdentControl.prototype = new EditorControl;
OutdentControl.constructor = OutdentControl;
OutdentControl.prototype.genHtmlContent = function () {
    var html = '<div id="'
			+ this.genId()
			+ '"  tabIndex="-1" class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.outdent + '" title="Delete indentation">'
			+ '</div>';
    return html;
};
OutdentControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("outdent", false, null);
        EditorControl.execCommand(_this._editor, "outdent", null);
    });
};
OutdentControl.prototype.setSelectedStyle = function (type, value) {
};
OutdentControl.prototype.getClearContent = function () {
};
/**
 * Tạo đẩy phải
 */
IntdentControl = function (editor) {
    EditorControl.call(this, editor);
};
IntdentControl.prototype = new EditorControl;
IntdentControl.constructor = IntdentControl;
IntdentControl.prototype.genHtmlContent = function () {
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">'
			+ '<img src="' + Config.Image.indent + '" title="Add indentation">'
			+ '</div>';
    return html;
};
IntdentControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id).click(function () {
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(_this.getEditorId()) == "")
                _this._editor.focus();
        // _this._editor.execCommand("indent", false, null);
        EditorControl.execCommand(_this._editor, "indent", null);
    });
};
IntdentControl.prototype.setSelectedStyle = function (type, value) {
};
IntdentControl.prototype.getClearContent = function () {
};
/**
 * Chèn link
 */
AttachLinkControl = function (editor) {
    EditorControl.call(this, editor);
};
AttachLinkControl.prototype = new EditorControl;
AttachLinkControl.constructor = AttachLinkControl;
AttachLinkControl.prototype.genHtmlContent = function () {
    var html = '<div style="position:relative;" id="'
			+ this.genId()
			+ '" tabIndex="-1"  class="bmeditor-insertlinkbutton">'
			+ '<img src="' + Config.Image.insertLink + '" style="top:6px; left:2px; " title="Hyperlink">'
			+ '</div>';
    return html;
};
AttachLinkControl.prototype.action = function () {
    var _this = this;
    $("#" + this._id)
			.click(
					function () {
					    var sLnk = prompt("Nhập link: ", "http://");
					    if (sLnk != null && sLnk !== "http://") {
					        if (navigator.userAgent.indexOf('Chrome') != -1) {
					            //_this._editor.focus();
					            _this._editor.execCommand("createLink",
                                        false, sLnk);
					        } else {
					            _this._editor.focus();
					            if (navigator.appName == "Microsoft Internet Explorer")
					                _this._editor.setSelected(sl.start,
                                            sl.end);
					            window.setTimeout(function () {
					                _this._editor.focus();
					                _this._editor.execCommand("createLink",
                                            false, sLnk);
					            }, 100);
					        }
					        var _editorId = _this._editor._editorId;
					        var cssText = "cursor: pointer !important";
					        $("#" + _editorId).contents().find("a").css(
                                    "cursor", "pointer");
					    }
					});
};
AttachLinkControl.prototype.setSelectedStyle = function (type, value) {
};
AttachLinkControl.prototype.getClearContent = function () {
};

/**
 * Tạo bảng
 */
InsertTableControl = function (editor) {
    EditorControl.call(this, editor);
    this.dialog = null;
    this.isDialogShow = false;
    this.parentEl = null;
    this.button = null;
};
InsertTableControl.prototype = new EditorControl;
InsertTableControl.constructor = AttachLinkControl;
InsertTableControl.prototype.genHtmlContent = function () {
    var _this = this;
    this.id = this.genId();

    var html = '<div id="' + this.id
			+ '" tabIndex="-1"  class="bmeditor-insertTableButton">'
			+ '</div>';

    YAHOO.util.Event
			.onContentReady(
					this.id,
					function () {
					    debugger
					    var inputTable = function (ev) {
					    };
					    var optionButtonData = [{
					        text: insertTableMsg,
					        value: 1,
					        onclick: {
					            fn: inputTable
					        }
					    }];

					    _this.button = new YAHOO.widget.Button(
								{
								    type: "split",
								    label: '<img src="' + Config.Image.table + '" title='
											+ insertTableMsg
											+ ' style = "margin-right: 1px">',
								    name: "optionbutton",
								    tabindex: -1,
								    menu: [],
								    onclick: {
								        fn: function (ev) {
								            if (_this.button)
								                _this.button._hideMenu();

								            if ($("#yui-table-panel").css("display") != "block") {
								                if ($("#yui-table-panel").length == 0)
								                    _this.createTable(5, 5);
								                $("#yui-table-panel").css("display", "block");
								            }
								            else
								                $("#yui-table-panel").css("display", "none");
								        }
								    },
								    container: this
								});

					    $('.yui-split-button').attr("tabindex", '-1');
					});

    var _this = this;

    /**
	 * click ra ngoài document
	 */
    YAHOO.util.Event
			.on(
					document,
					"click",
					function (e) {
					    if (_this.dialog && _this.dialog.element) {
					        var el = YAHOO.util.Event.getTarget(e);
					        var dialogEl = _this.dialog.element;
					        var button = document.getElementById(_this.id);
					        if ((el != dialogEl
									&& !YAHOO.util.Dom.isAncestor(dialogEl, el)
									&& !YAHOO.util.Dom.isAncestor(button, el) && el != button)
									|| ($(el).html() && $(el)
											.html()
											.indexOf(
													'<img src="' + Config.Image.table + '" title=') != -1)) {
					            $("#yui-table-panel").css("display", "none");
					        }
					    }
					});

    $(window).bind("blur", function () {
        $("#yui-table-panel").css("display", "none");
    });

    return html;
};

InsertTableControl._insertNodeAtSelection = function (win, insertNode) {
    if (window.getSelection) {
        var sel = win.getSelection();
        var range = sel.getRangeAt(0);
    } else if (document.selection) {// IE
        var sel = win.selection.createRange();
        var range = sel;
    }
    sel.removeAllRanges();

    range.deleteContents();

    var container = range.startContainer;
    var pos = range.startOffset;

    range = document.createRange();

    if (container.nodeType == 3 && insertNode.nodeType == 3) {
        container.insertData(pos, insertNode.nodeValue);

        range.setEnd(container, pos + insertNode.length);
        range.setStart(container, pos + insertNode.length);
    } else {
        var afterNode;
        if (container.nodeType == 3) {
            var textNode = container;
            container = textNode.parentNode;
            var text = textNode.nodeValue;

            var textBefore = text.substr(0, pos);
            var textAfter = text.substr(pos);

            var beforeNode = document.createTextNode(textBefore);
            afterNode = document.createTextNode(textAfter);

            container.insertBefore(afterNode, textNode);
            container.insertBefore(insertNode, afterNode);
            container.insertBefore(beforeNode, insertNode);

            container.removeChild(textNode);
        } else {
            afterNode = container.childNodes[pos];
            container.insertBefore(insertNode, afterNode);
        }

        range.setEnd(afterNode, 0);
        range.setStart(afterNode, 0);
    }

    sel.addRange(range);
};
// /**
//  * tạo dialog chưa bảng để chọn cột hàng
//  */
// InsertTableControl.prototype.initDialogSelectTable = function(x, y) {
// 	if($("#yui-table-panel" ).length == 0)
// 		this.createTable(5,5);
//     $("#yui-table-panel").css("display","block");

// };
/**
 * tạo bảng để chọn số dòng số cột
 */
InsertTableControl.prototype.createTable = function (m, n, id) {
    var html = '<table height="100%" width="100%" border="1" cellspacing="0" cellpadding="0">';
    var _this = this;
    for (var i = 1; i <= m; i++) {
        html += "<tr>";
        for (var j = 1; j <= n; j++) {
            html += '<td style="background-color:#FFFFFF;" dong="' + i
					+ '" cot="' + j + '">&nbsp;</td>';
        }
        html += "</tr>";
    }
    html += "</table>";
    //$("#" + id).html(html);
    $("#" + this.getId()).after('<div class="button-menu ui-widget-content" id="' + "yui-table-panel" + '">' + html + '</div>');
    $("#yui-table-panel" + " table tr td").each(function () {
        $(this).bind("mouseover", function () {
            _this.selectTable(this);
        });
        $(this).bind("click", function () {
            _this.chooseTable(this);
        });
    });
};
/**
 * chọn bảng khi nhập số dòng và số cột
 */
InsertTableControl.prototype.chooseTable = function (self) {
    $("#yui-table-panel").css("display", "none");
    var _this = this;
    // tìm id
    var rows = parseInt($(self).attr("dong"));
    var cols = parseInt($(self).attr("cot"));
    var id = $(self).parent().parent().parent().parent().attr("id");
    e = document.getElementById(_this._editor.getId());
    if ((rows > 0) && (cols > 0)) {
        table = document.createElement("table");
        table.setAttribute("border", "1");
        table.setAttribute("cellpadding", "2");
        table.setAttribute("cellspacing", "2");
        table.setAttribute("width", $(e).width() - 25);
        tbody = document.createElement("tbody");
        for (var i = 0; i < rows; i++) {
            tr = document.createElement("tr");
            for (var j = 0; j < cols; j++) {
                td = document.createElement("td");
                br = document.createElement("br");
                td.appendChild(br);
                tr.appendChild(td);
            }
            tbody.appendChild(tr);
        }
        table.appendChild(tbody);
        if (navigator.appName == "Microsoft Internet Explorer") {
            $(_this._editor.curentElementToInsert).prepend(table);
        } else
            InsertTableControl._insertNodeAtSelection(e.contentWindow, table);
    }

    //_this.isDialogShow = false;
    //_this.dialog.hide();
};
/**
 * chọn bảng
 */
InsertTableControl.prototype.selectTable = function (_this) {
    // tìm id
    var m = parseInt($(_this).attr("dong"));
    var n = parseInt($(_this).attr("cot"));

    var id = $(_this).parent().parent().parent().parent().attr("id");

    // xóa màu nền
    $("#" + id + " table tr td").each(function () {
        $(this).css("background-color", "#FFFFFF");
    });
    // bôi màu phần được chọn
    for (var i = 1; i <= m; i++) {
        for (var j = 1; j <= n; j++) {
            $("td[dong='" + i + "'][cot='" + j + "']").css("background-color",
					"#ccc");
        }
    }
    // hiển thị thông tin
    $("#statustable_" + this.id).html(
			tableMsg + " " + m + " " + rowMsg + " " + n + " " + columnMsg);
};
InsertTableControl.prototype.action = function () {
};
InsertTableControl.prototype.setSelectedStyle = function (type, value) {
};
InsertTableControl.prototype.getClearContent = function () {
};

/**
 * kế thừa EditorControl, là điều khiển chịu trách nhiệm định dạng text
 *
 * @param {Object}
 *            editor
 */
EditorFormatControl = function (editor) {
    EditorControl.call(this, editor);
};
EditorFormatControl.prototype = new EditorControl;
EditorFormatControl.constructor = EditorFormatControl;
EditorFormatControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<input type="button" class="editor_formatblock_select" tabIndex="-1" value="Normal" id="'
			+ this.id + '" />';
    return html;
};
EditorFormatControl.prototype.action = function () {
    $('#' + this.id).formatblockSelector(this._editor, this.id);
   // this._editor.focus();
};
EditorFormatControl.prototype.setSelectedStyle = function (type, value) {
    if (type == "formatblock") {
        $('#' + this.id).val(value);
    }
};
EditorFormatControl.prototype.getClearContent = function () {
};

/**
 * Điều khiển chọn font chữ cho editor
 *
 * @param {BmailEditor}
 *            editor editor được gắn với control này
 */
EditorFontControl = function (editor) {
    EditorControl.call(this, editor);
    this.editor = editor;
    // this.dfFont = defaultFont;
};
EditorFontControl.prototype = new EditorControl;
EditorFontControl.constructor = EditorFontControl;
EditorFontControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<input type="button" class="editor_font_select" tabIndex="-1" value="Times New Roman" id="'
			+ this.id + '" />';
    return html;
};
EditorFontControl.prototype.action = function () {
    $('#' + this.id).fontSelector(this._editor, this.id);
};
EditorFontControl.prototype.render = function () {
};
EditorFontControl.prototype.setSelectedStyle = function (type, value) {
    if (type == "font") {
        $('#' + this.id).val(value);
        $('#' + this.id).css("font-family", value);
        var ulId = "fontselector_" + this.id;
        $("#" + ulId).find('a.selected').removeClass("selected");
        $("#" + ulId).find('a[font="' + value + '"]').addClass("selected");
    }
};
EditorFontControl.prototype.getClearContent = function () {
    var value = $('#' + this.id).val();
    this._editor.execCommand("fontname", false, value);
    if (value == 'Times New Roman')
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            var _this = this;
            window.setTimeout(function () {
                $("#" + _this._editor._editorId).contents().find("body").css(
						"font-family", "Times New Roman");
                _this._editor.execCommand("fontname", false, value);
            }, 500);
        }
};

/**
 * Điều khiển chọn size chữ cho editor
 */
EditorSizeControl = function (editor) {
    EditorControl.call(this, editor);
    this.editor = editor;
};
EditorSizeControl.prototype = new EditorControl;
EditorSizeControl.constructor = EditorSizeControl;
EditorSizeControl.prototype.genHtmlContent = function () {
    var html = '<div  class="styled-select" tabIndex="-1" >'
			+ '<select tabIndex="-1"  id="' + this.genId() + '">'
			+ '<option value="1">8</option>' + '<option value="2">10</option>'
			+ '<option value="3" selected="selected">12</option>'
			+ '<option value="4">14</option>'
			+ ' <option value="5">18</option>'
			+ '<option value="6">24</option>' + '<option value="7">36</option>'
			+ '</select></div>';
    return html;
};
EditorSizeControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<input type="button" class="editor_fontsize_select" tabIndex="-1" value="12" id="'
			+ this.id + '" />';
    return html;
};
EditorSizeControl.prototype.action = function () {
    var sel = this;
    $('#' + this.id).fontsizeSelector(this._editor, this.id);

    $('#' + this.id).parent().css("position", "relative");

    // sự kiện
    var _this = this;
    $('#sizecong_' + this.id).bind("click", function () {
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            sel.range = sel._editor.getSelected();
        }
        var value = parseInt($('#' + _this.id).val());

        var fontsizes = new Array(8, 10, 12, 14, 18, 24, 36);
        var size = 2;
        var val = value;
        jQuery.each(fontsizes, function (i, item) {
            if (item == value) {
                size = i + 1;
                val = fontsizes[size];
            }
        });
        size++;
        if (size > 7)
            size = 7;
        $('#' + _this.id).val(val);
        var ulId = "fontsizeSelector_" + _this.id;
        $("#" + ulId).find('a.selected').removeClass("selected");
        $("#" + ulId).find('a[showsize="' + val + '"]').addClass("selected");
        // _this.editor.execCommand("fontsize", false, size);
        if (navigator.userAgent.indexOf('Chrome') == -1) {
            _this.editor.execCommand("fontsize", false, size);
            _this.editor.focus();
        } else
            window.setTimeout(function () {
                _this.editor.execCommand("fontsize", false, size);
                _this.editor.focus();
                //
            }, 10);
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            _this.editor.focus();
            _this.editor.removeAllRanges();
            _this.editor.addRange(sel.range);
        }

        if (_this._editor.isCache) {
            createCookie('Chat_fontsize', size);
        }
    });
    $('#sizetru_' + this.id).bind("click", function () {
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            sel.range = sel._editor.getSelected();
        }
        var value = parseInt($('#' + _this.id).val());
        var fontsizes = new Array(8, 10, 12, 14, 18, 24, 36);
        var size = 2;
        var val = value;
        jQuery.each(fontsizes, function (i, item) {
            if (item == value) {
                size = i;
                val = fontsizes[size - 1];
            }
        });
        if (size < 0)
            size = 0;
        $('#' + _this.id).val(val);
        var ulId = "fontsizeSelector_" + _this.id;
        $("#" + ulId).find('a.selected').removeClass("selected");
        $("#" + ulId).find('a[showsize="' + val + '"]').addClass("selected");

        // _this.editor.execCommand("fontsize", false, size);
        if (navigator.userAgent.indexOf('Chrome') == -1) {
            _this.editor.execCommand("fontsize", false, size);
            _this.editor.focus();
        } else
            window.setTimeout(function () {
                _this.editor.execCommand("fontsize", false, size);
                // editor.focus();
            }, 10);
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            _this.editor.removeAllRanges();
            _this.editor.addRange(sel.range);
        }
        if (_this._editor.isCache) {
            createCookie('Chat_fontsize', size);
            // setPrivateGroup();
        }
    });
};
EditorSizeControl.prototype.setSelectedStyle = function (type, value) {
    if (type == "fontsize") {
        var fontsizes = new Array(8, 10, 12, 14, 18, 24, 36);
        var size = 12;
        jQuery.each(fontsizes, function (i, item) {
            if (i == value)
                size = item;
        });

        $('#' + this.id).val(size);
        var ulId = "fontsizeSelector_" + this.id;
        $("#" + ulId).find('a.selected').removeClass("selected");
        $("#" + ulId).find('a[showsize="' + size + '"]').addClass("selected");
    }
};
EditorSizeControl.prototype.getClearContent = function () {
};

/**
 * Điều khiển chọn mầu chữ cho editor
 */
EditorColorControl = function (editor) {
    EditorControl.call(this, editor);
    this.id = "";
    this.dialog = null;
    this.isDialogShow = false;
};
EditorColorControl.prototype = new EditorControl;
EditorColorControl.constructor = EditorColorControl;
EditorColorControl.prototype.getId = function () {
    return this.id;
};
EditorColorControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<div style ="position:relative;width:27px; height:22px;" id="'
			+ this.id
			+ '" class="editor-forcecolor"><input type="button" class="forcecolor" style="border:0px; background:url(' + Config.Image.fontColor + ') no-repeat; margin-top:6px; width: 27px;" tabIndex="-1" value="" id="'
			+ this.id + '_editor-forcecolor'
			+ '" /><div class="color_select" id = "' + this.id
			+ '_color" ></div><div class="muiten" id = "' + this.id
			+ '_muiten"></div></div>';

    return html;
};
EditorColorControl.prototype.action = function () {
    var _this = this;

    $("#" + this.id).colorSelector("color", this.id, this);
};
EditorColorControl.prototype.initDialogTextColor = function (x, y) {
    var _this = this;
    var textColor = '#000';
    this.dialog = new YAHOO.widget.SimpleDialog(
			"yui-picker-panel",
			{
			    width: "400px",
			    height: "300px",
			    close: false,
			    visible: false,
			    draggable: false,
			    constraintoviewport: true,
			    x: x - 20,
			    y: y + 10,
			    zIndex: 1000,
			    buttons: [
						{
						    text: chooseButtonMsg,
						    handler: function () {
						        if (navigator.appName == "Microsoft Internet Explorer") {
						            _this._editor.focus();
						            _this._editor.setSelected(_this.sl.start,
											_this.sl.end);
						        }
						        $('#' + this.id + "_color").css(
										'background-color', textColor);

						        if (navigator.userAgent.indexOf('Chrome') != -1) {
						            window.setTimeout(function () {
						                _this._editor.execCommand("foreColor",
												false, textColor);
						                // if
						                // (!Util.getSelection(editor._editorId))
						                _this._editor
												.getEditorWhenClearContent();
						            }, 100);
						        } else {
						            _this._editor.execCommand("foreColor",
											false, textColor);
						            _this._editor.focus();
						        }

						        this.cancel();
						        if (navigator.userAgent.indexOf('Chrome') != -1) {
						            // _this._editor.removeAllRanges();
						            // _this._editor.addRange(_this.range);
						        }
						        _this.isDialogShow = false;
						    },
						    isDefault: true
						}, {
						    text: cancelButtonMsg,
						    handler: function () {
						        this.cancel();
						        _this.isDialogShow = false;
						    }
						}]
			});
    this.dialog.renderEvent.subscribe(function () {
        if (!this.picker) {
            this.picker = new YAHOO.widget.ColorPicker("forcecolor-picker", {
                container: this.dialog,
                images: {
                    PICKER_THUMB: "giaodien/picker_thumb.png",
                    HUE_THUMB: "giaodien/hue_thumb.png"
                },
                showhexcontrols: true,
                showhsvcontrols: true
            });

            // thay doi
            this.picker.on("rgbChange", function (o) {
                textColor = '#' + this.get("hex");
            });
            YAHOO.util.Event.on('forcecolor-picker', 'click', function () {
            });
        }
    });

    this.dialog.setHeader('<div class = "rhd"></div><div class = "rft"></div>'
			+ forceColorMsg);
    this.dialog.setBody('<div id="forcecolor-picker"></div>');
    this.dialog.render(document.body);
    var _this = this;
    YAHOO.util.Event.on(document, "click", function (e) {
        if (_this.dialog.element) {
            var el = YAHOO.util.Event.getTarget(e);
            var dialogEl = _this.dialog.element;
            var elName = el.name;
            if (el != dialogEl && !YAHOO.util.Dom.isAncestor(dialogEl, el)
					&& (!elName || elName.indexOf("More") == -1)) {
                _this.dialog.cancel();
                _this.isDialogShow = false;
            }
        }
    });
};
EditorColorControl.prototype.setSelectedStyle = function (type, value) {
};

EditorColorControl.prototype.getClearContent = function () {
};
/**
 * Điều khiển chọn mầu nền chữ cho editor
 */
EditorBgColorControl = function (editor) {
    EditorControl.call(this, editor);
    this.curPos = 0;
};
EditorBgColorControl.prototype = new EditorControl;
EditorBgColorControl.constructor = EditorBgColorControl;
EditorBgColorControl.prototype.genHtmlContent = function () {
    this.id = this.genId();
    var html = '<input type="button" class="bgcolor" style="border:0px; background:url(' + Config.Image.bgcolor + ') no-repeat; width:22px;  margin-top:0px; height:22px;" value="" id="'
			+ this.id + '" /><div class="bg_color_select"></div>';
    return html;
};
EditorBgColorControl.prototype.RGBtoHex = function (value) {
    value = value.substring(4, value.length - 1);
    var arr = value.split(',');
    var R = arr[0];
    var G = arr[1];
    var B = arr[2];
    return this.toHex(R) + this.toHex(G) + this.toHex(B);
};
EditorBgColorControl.prototype.toHex = function (n) {
    n = parseInt(n, 10);
    if (isNaN(n))
        return "00";
    n = Math.max(0, Math.min(n, 255));
    return "0123456789ABCDEF".charAt((n - n % 16) / 16)
			+ "0123456789ABCDEF".charAt(n % 16);
};
/**
 * dialog chọn backgrond color
 */
EditorBgColorControl.prototype.initDialogBgColor = function (x, y) {
    var _this = this;
    var bgColor = '#fff';
    this.dialog = new YAHOO.widget.SimpleDialog("yui-picker-panel", {
        width: "400px",
        height: "300px",
        close: false,
        visible: false,
        draggable: false,
        constraintoviewport: true,
        x: x - 20,
        y: y + 10,
        zIndex: 1000,
        buttons: [{
            text: chooseButtonMsg,
            handler: function () {
                if (navigator.appName == "Microsoft Internet Explorer") {
                    _this._editor.focus();
                    _this._editor.setSelected(_this.sl.start, _this.sl.end);
                }

                if (navigator.userAgent.indexOf('Chrome') != -1) {
                    window.setTimeout(function () {
                        _this._editor.execCommand("backColor", false, bgColor);
                        _this._editor.getEditorWhenClearContent();
                    }, 100);
                } else {
                    _this._editor.execCommand("backColor", false, bgColor);
                    _this._editor.focus();
                }

                this.cancel();

                _this.isDialogShow = false;
            },
            isDefault: true
        }, {
            text: cancelButtonMsg,
            handler: function () {
                this.cancel();
                _this.isDialogShow = false;
            }
        }]
    });
    this.dialog.renderEvent.subscribe(function () {
        if (!this.picker) {
            this.picker = new YAHOO.widget.ColorPicker("background-picker", {
                container: this.dialog,
                images: {
                    PICKER_THUMB: "giaodien/picker_thumb.png",
                    HUE_THUMB: "giaodien/hue_thumb.png"
                },
                showhexcontrols: true,
                showhsvcontrols: true
            });

            // thay doi
            this.picker.on("rgbChange", function (o) {
                bgColor = '#' + this.get("hex");
                return false;
            });
            this.picker.on("beforeRgbChange", function (o) {
                // alert(1);
            });
            $('#yui-picker-panel_c').click(function (event) {
                event.preventDefault();
                return false;
            });
        }
    });

    this.dialog.setHeader("<div class = 'rhd'></div><div class = 'rft'></div>"
			+ backgroundColorMsg);
    this.dialog.setBody('<div id="background-picker"></div>');
    this.dialog.render(document.body);
    var _this = this;
    YAHOO.util.Event.on(document, "click", function (e) {
        if (_this.dialog.element) {
            var el = YAHOO.util.Event.getTarget(e);
            var dialogEl = _this.dialog.element;
            // var button = document.getElementById(_this.id);
            var elName = el.name;

            if (el != dialogEl && !YAHOO.util.Dom.isAncestor(dialogEl, el)
					&& (!elName || elName.indexOf("More") == -1)) {
                _this.dialog.cancel();
                _this.isDialogShow = false;
            }
        }
    });
};
EditorBgColorControl.prototype.genHtmlContent = function () {
    this.id = this.genId();

    var html = '<div style ="position:relative;*margin-top:3px; height:22px;" id="'
			+ this.id
			+ '" class="editor-bgcolor"><input type="button"  style="border:0px; background:url(' + Config.Image.bgcolor + ') no-repeat; width:22px;  margin-top:3px; height:22px;" value="" id="'
			+ this.id
			+ '_editor-bgcolor'
			+ '" /><div class="bg_color_select" id = "'
			+ this.id
			+ '_bg_color" ></div> <div class="muiten" id = "'
			+ this.id
			+ '_muiten"></div> </div>';

    return html;
};
EditorBgColorControl.prototype.action = function () {
    var _this = this;

    $("#" + this.id).colorSelector("background", this.id, this);
};
EditorBgColorControl.prototype.setSelectedStyle = function (type, value) {
    if (type == "backColor") {
        $('#' + this.id + "_bg_color").css('background-color', value);
    }
};
EditorBgColorControl.prototype.getClearContent = function () {
};
/**
 * Emoticon button
 */
EditorEmoControl = function (editor) {
    EditorControl.call(this, editor);
};
EditorEmoControl.prototype = new EditorControl;
EditorEmoControl.constructor = EditorEmoControl;
EditorEmoControl.prototype.genHtmlContent = function () {
    //var ipServer = Session.getInstance().getIpServer();
    this.id = this.genId();
    //var url = "https://" + ipServer + "/mail/";
    var html = '<div id="' + this.id
			+ '"  tabIndex="-1" class="bmeditor-emobutton">'
			+ '<div class="emoticonFace" value="1"></div>'
			+ '<div class="muiten" id="' + this.id + '_muiten"></div></div>';
    return html;
};
EditorEmoControl.prototype.action = function () {
};
EditorEmoControl.prototype.render = function () {
    $("#" + this.id + " .emoticonFace").css("background-position", "0px 0px");
    var _this = this;
    this._menu = new EmoticonMenu(this.id + "_muiten");
    this._menu.render();

    this._menu.click(function (a) {
        //var ipServer = Session.getInstance().getIpServer();
        //var url = "https://" + ipServer
        //+ "/mail/js/editor/js/emoticon/Emoticons/" + a + ".gif";
        var url = Config.URL_IMAGE + "emoticon/Emoticons/" + a + ".gif";
        var xPos = -1 * (a - 1) * 21;
        $("#" + _this.id + " .emoticonFace").css("background-position",
				xPos + "px 0px");
        $("#" + _this.id + " .emoticonFace").attr("value", a);
        _this._editor.focus();
        _this._editor.execCommand("insertImage", false, url);
    });

    $("#" + _this.id + " .emoticonFace").click(
			function () {
			    var value = $(this).attr("value");
			    //var ipServer = Session.getInstance().getIpServer();
			    //var url = "https://" + ipServer
			    //	+ "/mail/js/editor/js/emoticon/Emoticons/" + value
			    //	+ ".gif";
			    var url = Config.URL_IMAGE + "emoticon/Emoticons/" + value + ".gif";
			    _this._editor.focus();
			    _this._editor.execCommand("insertImage", false, url);
			});
};
EditorEmoControl.prototype.setSelectedStyle = function (type, value) {
};
EditorEmoControl.prototype.getClearContent = function () {
};

/**
 * Emoticon button
 */
EditorMailEmoControl = function (editor) {
    EditorControl.call(this, editor);
};
EditorMailEmoControl.prototype = new EditorControl;
EditorMailEmoControl.constructor = EditorMailEmoControl;
EditorMailEmoControl.prototype.genHtmlContent = function () {
    //var ipServer = Session.getInstance().getIpServer();
    this.id = this.genId();
    //var url = "https://" + ipServer + "/mail/";
    var html = '<div id="' + this.id
			+ '"  tabIndex="-1" class="bmeditor-emobutton">'
			+ '<div class="mailEmoticonFace" value="1"></div>'// '<img src="'
			// + url +
			// 'icons/emoticon.png">'
			+ '<div class="muiten" id="' + this.id + '_muiten"></div>'
			+ '</div>';
    return html;
};
EditorMailEmoControl.prototype.action = function () {
};
EditorMailEmoControl.prototype.render = function () {
    $("#" + this.id + " .mailEmoticonFace").css("background-position",
			"-2px 0px");
    var _this = this;
    this._menu = new MailEmoticonMenu(this.id + "_muiten");
    this._menu.render();
    $("#" + this.id + "_muiten").css("left", "-7px");
    this._menu
			.click(function (a) {
			    var value = a + "_mail";
			    var url = Config.URL_IMAGE + "emoticon/MailEmoticons/" + value + ".gif";
			    var xPos = -1 * (a - 1) * 30;
			    $("#" + _this.id + " .mailEmoticonFace").css(
						"background-position", xPos + "px 0px");
			    $("#" + _this.id + " .mailEmoticonFace").attr("value", a);
			    _this._editor.focus();
			    _this._editor.execCommand("insertImage", false, url);
			});

    $("#" + _this.id + " .mailEmoticonFace").click(
			function () {
			    var value = $(this).attr("value");
			    var url = Config.URL_IMAGE + "emoticon/MailEmoticons/" + value + "_mail.gif";
			    _this._editor.focus();
			    _this._editor.execCommand("insertImage", false, url);
			});
};
EditorMailEmoControl.prototype.setSelectedStyle = function (type, value) {
};
EditorMailEmoControl.prototype.getClearContent = function () {
};
/**
 * chọn mầu nền
 */
BgColorControl = function (editor) {
    EditorControl.call(this, editor);
};
BgColorControl.prototype = new EditorControl;
BgColorControl.constructor = BgColorControl;
BgColorControl.prototype.genHtmlContent = function () {
    //var ipServer = Session.getInstance().getIpServer();
    //var url = "https://" + ipServer + "/mail/";
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-bgcolor">' + '<img src="'
			+ Config.Image.bgcolor + '" title="Mầu nền">' + '</div>';
    return html;
};
BgColorControl.prototype.render = function () {
    var _this = this;
    this._menu = new BgColorMenu(_this._id);
    this._menu.render();
    this._menu.click(function (value) {
        _this._editor.focus();
        _this._editor.execCommand('backColor', null, value);
    });
};
BgColorControl.prototype.setSelectedStyle = function (type, value) {
};
BgColorControl.prototype.getClearContent = function () {
};
/**
 * chọn mầu chữ
 */
ForeColorControl = function (editor) {
    EditorControl.call(this, editor);
};
ForeColorControl.prototype = new EditorBgColorControl;
ForeColorControl.constructor = ForeColorControl;
ForeColorControl.prototype.genHtmlContent = function () {
    //var ipServer = Session.getInstance().getIpServer();
    //var url = "https://" + ipServer + "/mail/";
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-bgcolor">' + '<img src="'
			+ Config.Image.textColor + '" title="Mầu chữ">' + '</div>';
    return html;
};
ForeColorControl.prototype.render = function () {
    var _this = this;
    this._menu = new BgColorMenu(_this._id);
    this._menu.render();
    this._menu.click(function (value) {
        _this._editor.focus();
        _this._editor.execCommand('foreColor', null, value);
    });
};
ForeColorControl.prototype.setSelectedStyle = function (type, value) {
    value = this.RGBtoHex(value);
    value = '#' + value;
    $('#' + this.id).val(value);
    // $('#'+this.id).css("background",value);
};
ForeColorControl.prototype.getClearContent = function () {
};
/**
 * Emoticon button Chat
 */
EditorEmoChat = function (editor) {
    EditorControl.call(this, editor);
};
EditorEmoChat.prototype = new EditorControl;
EditorEmoChat.constructor = EditorEmoChat;
EditorEmoChat.prototype.genHtmlContent = function () {
    //var ipServer = Session.getInstance().getIpServer();
    //var url = "https://" + ipServer + "/mail/";
    var url = "/mail/";
    var html = '<div id="' + this.genId()
			+ '" tabIndex="-1"  class="bmeditor-imagebutton">' + '<img src="'
			+ Config.Image.emoticon + '" title="Add indentation">'
			+ '</div>';
    return html;
};
EditorEmoChat.prototype.action = function () {
};

EditorEmoChat.prototype.render = function () {
    var _this = this;
    this._menu = new EmoticonChat(_this._id);
    this._menu.render();
    this._menu.click(function (a) {
        _this._editor.focus();
        _this._editor.execCommand("insertImage", false, a);
    });
};
EditorEmoChat.prototype.setSelectedStyle = function (type, value) {
};
EditorEmoChat.prototype.getClearContent = function () {
};

jQuery.fn.fontsizeSelector = function (editor, id) {
    var fontsizes = new Array(8, 10, 12, 14, 18, 24, 36);

    var sel = this;
    var ulId = "fontsizeSelector_" + id;
    var ul = $('<ul class="fontsizeSelector" id="' + ulId + '"></ul>');
    $('body').prepend(ul);
    $(ul).hide();
    jQuery.each(fontsizes, function (i, item) {
        var cl = "";
        if ($(sel).val() == item)
            cl = "selected";
        var size = i;
        $(ul).append(
				'<li><a href="#" class="' + cl + '" showsize ="' + item
						+ '" fontsize=" ' + size + '">' + item + '</a></li>');
    });

    $(sel).click(function (ev) {
        $(".colorselector").css("display", "none");
        $(".formatblockselector").css("display", "none");
        // $(".fontsizeSelector").css("display","none");
        $(".fontselector").css("display", "none");
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            sel.range = editor.getSelected();
        }
        ev.preventDefault();
        if ($(ul).css("display") != "none")
            $(ul).css("display", "none");
        else
            $(ul).show();

        $(ul).css({
            top: $(sel).offset().top + $(sel).height() + 4,
            left: $(sel).offset().left,
            "z-index": "9999999999"
        });

        $(this).blur();
        return false;
    });

    $(ul).find('a').click(function () {
        var size = $(this).attr('fontsize');

        $(sel).val($(this).attr('showsize'));

        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(editor.getId()) == "")
                editor.focus();

        EditorControl.execCommand(editor, "fontsize", size);

        // tạo màu cho thẻ được chọn
        $(ul).find('a.selected').removeClass("selected");
        $(this).addClass("selected");

        $(ul).hide();

        if (navigator.userAgent.indexOf('Chrome') != -1) {
            editor.removeAllRanges();
            editor.addRange(sel.range);
        }

        // editor.focus();
        if (editor.isCache) {
            createCookie('Chat_fontsize', size);
            setPrivateGroup();
        }
        // editor.getEditorWhenClearContent();
        return false;
    });

    $(document)
			.bind(
					"click blur ",
					function (e) {
					    if ($("#" + ulId).css("display") != "none"
								&& $(e.target).attr("class") != "editor_fontsize_select")
					        $("#" + ulId).css("display", "none");
					});
    $(window).bind("blur", function () {
        $("#" + ulId).css("display", "none");
    });
};

jQuery.fn.formatblockSelector = function (editor, id) {
    var fonts = new Array('&lt;p&gt;,Normal', '&lt;p&gt;,Paragraph',
			'&lt;h1&gt;,Heading 1', '&lt;h2&gt;,Heading 2',
			'&lt;h3&gt;,Heading 3', '&lt;h4&gt;,Heading 4',
			'&lt;h5&gt;,Heading 5', '&lt;h6&gt;,Heading 6',
			'&lt;address&gt;,Address', '&lt;pre&gt;,Formatted');
    var ulId = "formatblockselector_" + id;
    // return this.each(function(){
    // Get input field
    var sel = this;
    // Add a ul to hold fonts
    var ul = $('<ul class="formatblockselector" id="' + ulId + '"></ul>');
    $('body').prepend(ul);
    $(ul).hide();
    jQuery.each(fonts, function (i, item) {
        var cl = "";
        if ($(sel).val() == item.split(',')[1])
            cl = "selected";

        $(ul).append(
				'<li><a href="#" class="' + cl + '" val="' + item.split(',')[0]
						+ '" name="' + item.split(',')[1] + '">'
						+ item.split(',')[1] + '</a></li>');
    });
    $(sel).bind("click", function (ev) {
        $(".colorselector").css("display", "none");
        // $(".formatblockselector").css("display","none");
        $(".fontsizeSelector").css("display", "none");
        $(".fontselector").css("display", "none");
        ev.preventDefault();
        // Show font list
        if ($(ul).css("display") != "none")
            $(ul).css("display", "none");
        else
            $(ul).show();

        $(ul).css({
            top: $(sel).offset().top + $(sel).height() + 4,
            left: $(sel).offset().left
        });

        $(this).blur();
        return false;
    });
    $(ul).find('a').click(function () {
        var val = $(this).attr('val');
        var name = $(this).attr('name');

        $(sel).val(name);
        if (navigator.userAgent.indexOf('Chrome') != -1)
            if (BmailEditor.getSelection(editor.getId()) == "")
                editor.focus();
        EditorControl.execCommand(editor, "formatblock", val);

        // tạo màu cho thẻ được chọn
        $(ul).find('a.selected').removeClass("selected");
        $(this).addClass("selected");

        $(ul).hide();
        return false;
    });

    $(document)
			.bind(
					"click blur ",
					function (e) {
					    if ($("#" + ulId).css("display") != "none"
								&& $(e.target).attr("class") != "editor_formatblock_select")
					        $("#" + ulId).css("display", "none");
					});
    $(window).bind("blur", function () {
        $("#" + ulId).css("display", "none");
    });
};

jQuery.fn.colorSelector = function (type, id, parentSelector) {
    var editor = parentSelector._editor;
    var colors = new Array('#FF0000,Red', '#0000FF,Blue', '#008000,Green',
			'#FFFFFF,White', '#000000,Black', '#00FFFF,Cyan',
			'#0000A0,DarkBlue', '#800080,Purple', '#00FF00,Lime',
			'#808000,Olive');

    var ulId = "colorselector_" + id;

    var sel = this;

    var ul = $('<ul class="colorselector" id="' + ulId + '"></ul>');
    $('body').prepend(ul);
    $(ul).hide();

    jQuery.each(colors, function (i, item) {
        var cl = "";
        if ($(sel).val() == item.split(',')[1])
            cl = "selected";

        $(ul).append(
				'<li><a href="#" class="' + cl + '" name="'
						+ item.split(",")[1] + '" color="' + item.split(",")[0]
						+ '" style="background: ' + item.split(",")[0]
						+ '"></a></li>');
    });

    // $(ul).append(
    // 		'<li><a href="#" class="moreColor" name="' + 'More' + type + '" >'
    // 				+ 'More' + '</a></li>');

    $(sel).click(function (ev) {
        // $(document).trigger("click");

        if (navigator.appName == "Microsoft Internet Explorer") {
            sel.sl = editor.getSelected();
        }
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            sel.range = editor.getSelected();
        }

        $(".formatblockselector").css("display", "none");
        $(".fontsizeSelector").css("display", "none");
        $(".fontselector").css("display", "none");

        ev.preventDefault();

        // Show font list
        if ($(ul).css("display") != "none")
            $(ul).css("display", "none");
        else
            $(ul).css("display", "block");

        $(ul).css({
            top: $(sel).offset().top + $(sel).height() + 4,
            left: $(sel).offset().left - 20,
            "z-index": "9999999999"
        });

        // return false;
    });

    // click để chọn màu
    $(ul)
			.find('a')
			.click(
					function (ev) {
					    ev.preventDefault();
					    var color = $(this).attr('color');

					    var name = $(this).attr('name');

					    if (name.indexOf("More") == -1) {
					        if (navigator.appName == "Microsoft Internet Explorer") {
					            editor.focus();
					            editor.setSelected(sel.sl.start, sel.sl.end);
					        }
					        if (navigator.userAgent.indexOf('Chrome') != -1) {
					            if (BmailEditor.getSelection(editor._editorId) == "") {
					                editor.removeAllRanges();
					                editor.addRange(sel.range);
					            }
					        }
					        var typeCm = "foreColor";
					        if (type == "background")
					            typeCm = "backColor";

					        if (navigator.userAgent.indexOf('Chrome') != -1)
					            if (BmailEditor.getSelection(editor.getId()) == "")
					                editor.focus();

					        EditorControl.execCommand(editor, typeCm, color);

					        // tạo màu cho thẻ được chọn
					        $(ul).find('a.selected').removeClass("selected");
					        $(this).addClass("selected");
					    } else {
					        // if (type == "background") {
					        // 	var x = ev.pageX;
					        // 	var y = ev.pageY - 100;

					        // 	if (!parentSelector.isDialogShow) {
					        // 		parentSelector.initDialogBgColor(x, y);
					        // 		parentSelector.dialog.show();
					        // 		parentSelector.isDialogShow = true;

					        // 		if (navigator.appName == "Microsoft Internet Explorer") {
					        // 			parentSelector.sl = parentSelector._editor
					        // 					.getSelected();
					        // 		}
					        // 		if (navigator.userAgent.indexOf('Chrome') != -1)
					        // 			parentSelector.range = parentSelector._editor
					        // 					.getSelected();
					        // 	}
					        // } else {
					        // 	if (!parentSelector.isDialogShow) {
					        // 		parentSelector.initDialogTextColor(x, y);
					        // 		parentSelector.dialog.show();
					        // 		parentSelector.isDialogShow = true;
					        // 	}
					        // 	if (navigator.appName == "Microsoft Internet Explorer") {
					        // 		parentSelector.sl = parentSelector._editor
					        // 				.getSelected();
					        // 	}
					        // 	if (navigator.userAgent.indexOf('Chrome') != -1)
					        // 		parentSelector.range = parentSelector._editor
					        // 				.getSelected();
					        // }
					    }
					    $(ul).hide();
					    if (!BmailEditor.getSelection(editor._editorId))
					        editor.getEditorWhenClearContent();
					    return false;
					});
    $(document).bind(
			"click",
			function (e) {
			    if ($("#" + ulId).css("display") != "none"
						&& $(e.target).attr("id").indexOf(id) == -1)
			        $("#" + ulId).css("display", "none");
			});

    $(window).bind("blur", function () {
        if ($("#" + ulId).css("display") != "none")
            $("#" + ulId).css("display", "none");
    });
};

jQuery.fn.fontSelector = function (editor, id) {
    // alert(1);

    var fonts = new Array('Arial', 'Arial Black', 'Comic Sans MS',
			'Courier New', 'Georgia', 'Impact', 'Lucida Console',
			'Lucida Sans Unicode', 'Palatino Linotype', 'Tahoma',
			'Times New Roman', 'Trebuchet MS', 'Verdana');
    var ulId = "fontselector_" + id;
    var sel = this;
    var ul = $('<ul class="fontselector" id="' + ulId + '"></ul>');

    $('body').prepend(ul);
    ul = "#" + ulId;
    $(ul).hide();

    jQuery.each(fonts, function (i, item) {
        var cl = "";
        if ($(sel).val() == item)
            cl = "selected";

        $(ul).append(
				'<li><a href="#" font = "' + item + '"  class="' + cl
						+ '" style="font-family: ' + item + '">'
						+ item.split(',')[0] + '</a></li>');
    });
    $(sel).click(function (ev) {
        $(".colorselector").css("display", "none");
        $(".formatblockselector").css("display", "none");
        $(".fontsizeSelector").css("display", "none");
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            sel.range = editor.getSelected();
        }
        ev.preventDefault();

        if ($(ul).css("display") != "none")
            $(ul).css("display", "none");
        else
            $(ul).show();

        $(ul).css({
            top: $(sel).offset().top + $(sel).height() + 4,
            left: $(sel).offset().left,
            "z-index":"9999999999"
        });

        $(this).blur();
        return false;
    });

    $(ul).find('a').click(function () {
        var font = $(this).attr('font');
        $(sel).val(font);
        $(sel).css("font-family", font);

        if (navigator.userAgent.indexOf('Chrome') != -1)
            editor.focus();
        EditorControl.execCommand(editor, "fontname", font);

        // tạo màu cho thẻ được chọn
        $(ul).find('a.selected').removeClass("selected");
        $(this).addClass("selected");

        $(ul).hide();
        // thiết lập lại font
        if (navigator.userAgent.indexOf('Chrome') != -1) {
            editor.removeAllRanges();
            editor.addRange(sel.range);
        }

        // lưu font
        if (editor.isCache) {
            createCookie('Chat_font', font);
            setPrivateGroup();
        }

        editor.getEditorWhenClearContent();
        return false;
    });
    $(document).bind(
			"click blur ",
			function (e) {
			    if ($("#" + ulId).css("display") != "none"
						&& $(e.target).attr("class") != "editor_font_select")
			        $("#" + ulId).css("display", "none");
			});
    $(window).bind("blur", function () {
        $("#" + ulId).css("display", "none");
    });
};