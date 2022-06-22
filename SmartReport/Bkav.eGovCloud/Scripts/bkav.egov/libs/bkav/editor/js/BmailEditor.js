//var curentElementToInsert;
/**
 * @author nobita Lớp vẽ editor
 * @param {String ||
 *            object} selector là selector của phần tử để gắn editor lên nó
 * @param {Array}
 *            controls là mảng các EditorControl của editor
 */
BmailEditor = function (selector) {
    this._selector = selector;
    this._insertImage = new InsertImageControl(this);

    this._controls = [new EditorEmoControl(this),
            new EditorMailEmoControl(this), new BoldControl(this),
            new ItalicControl(this), new UnderlineControl(this),
            new StrikeControl(this), new EditorColorControl(this),
            // more
            new EditorFontControl(this), new EditorSizeControl(this),
            new EditorBgColorControl(this), new UnorderListControl(this),
            new OrderListControl(this), new OutdentControl(this),
            new IntdentControl(this), new JustifyLeftControl(this),
            new JustifyCenterControl(this), new JustifyRightControl(this),
            new JustifyFullControl(this), new EditorFormatControl(this),
            new RemoveFormatControl(this), new InsertTableControl(this),
            // hết more
            //new SeparatorControl(this),
            //new AttachControl(this),
            this._insertImage,
            new AttachLinkControl(this),
            //new VietkeyControl(this),
        //new MoreControl(this)
    ];

    this._noFocusContent = false;
    BmailEditor.INDEX++;
    this._index = BmailEditor.INDEX;
    this._toolbar = "bmail-editor-toolbar" + this._index;
    this._editorId = "bmail-editor-" + this._index;
    this._toolbarShowed = true;
    this.isNeedChangeWhenClick = true;
    this.curentElementToInsert = null;

    this.currentStyle = null;
    //$(this._selector).click(function (e) {
    //    debugger
    //});
};
BmailEditor.INDEX = 0;
BmailEditor.prototype.setCurrentStyle = function (property, value) {
    this.currentStyle[property] = value;
};

BmailEditor.prototype.getCurrentStyle = function (property) {
    return this.currentStyle[property];
};

// bộ đếm của lớp để tự động sinh ra id khi tạo ra nhiều editor
// BmailEditor.share.INDEX = 0;
/**
 * gắn toolbar cho editor
 *
 * @param {Array}
 *            controls mảng các EditorControl của editor
 */
BmailEditor.prototype.setToolbar = function (controls) {
    this._controls = controls;
    for (var i = 0; i < this._controls.length; i++) {
        this._controls[i].attachEditor(this);
    }
};

BmailEditor.prototype.setNoFocusContent = function (bool) {
    this._noFocusContent = bool;
};

BmailEditor.prototype.clear = function () {
    this.execCommand("clean", false, null);
    // this.setContent("");
};

/**
 * Thiết lập nội dung có sẵn cho editor
 *
 * @param {String}
 *            content nội dung sẵn có
 * @param {Number}
 *            delay thời gian chờ
 */
BmailEditor.prototype.setContent = function (content, callback) {
    var _this = this;
    if (navigator.appName == "Microsoft Internet Explorer"
			|| navigator.appName == "Opera") {
        $("#" + _this._editorId).contents().find("body").html(content);
        if (typeof callback == "function") {
            callback();
        }
        // _this.turnOnDesignMode();
        //if (!_this._noFocusContent)
        //    _this.focus();
    } else {
        setTimeout(function () {
            $("#" + _this._editorId).contents().find("body").html(content);
            _this.turnOnDesignMode();
            if (typeof callback == "function") {
                callback();
            }
            //if (!_this._noFocusContent)
            //    _this.focus();
            //_this.getEditorWhenClearContent();
        }, 500);
    }
};

/**
 * get iframe
 */
BmailEditor.prototype.getIFrameDocument = function () {
    if (!document.getElementById(this._editorId)) {

    }

    if (document.getElementById(this._editorId).contentDocument) {
        return document.getElementById(this._editorId).contentDocument;
    } else {
        // IE
        return document.frames[this._editorId].document;
    }
};

/**
 *
 */
BmailEditor.prototype.startDesignMode = function () {
    this.getIFrameDocument().designMode = "On";
    $("#" + this._editorId).contents().find("body").attr("spellcheck", false);
    $("#" + this._editorId).contents().find("body").css("font-family",
			"Times New Roman");
};

/**
 * bật chế độ soạn thảo cho iframe
 */
BmailEditor.prototype.turnOnDesignMode = function () {
    var _this = this;
    /**
	 * bật chế độ designMode setTimeout khắc phục lỗi bị tắt bởi firefox ngay
	 * sau khi bật
	 */
    window.setTimeout(function () {
        _this.startDesignMode();
    }, 100);
};

/**
 * Thiết lập thay đổi font khi kick vào editor
 */
BmailEditor.prototype.setNeedChangeWhenClick = function (bool) {
    this.isNeedChangeWhenClick = bool;
};

/**
 * Thiết lập lại editor khi set content
 */
BmailEditor.prototype.getEditorWhenClearContent = function () {
    for (var i = 0; i < this._controls.length; i++) {
        this._controls[i].getClearContent();
    }
};

/**
 * Hàm vẽ editor, tạo content html, gắn các button lên toolbar khởi tạo hành
 * động attach các sự kiện phím tắt
 *
 */
BmailEditor.prototype.render = function (contentMail) {
    var _this = this;
    $(this._selector).empty();
    var content = this._generateHtmlContent();
    $(this._selector).html(content);

    /* vẽ toolbar */
    for (var i = 0; i < this._controls.length; i++) {
        this._controls[i].render();
        this._controls[i].handleMouseEvent();
    }

    this._initAction();
    this._insertImage.initUpload();
    $(".bmeditor-insertTableButton").parent().css("width", "40px");
    $(".bmeditor-insertTableButton").parent().css("z-index", -1);

    if (navigator.userAgent.indexOf('Chrome') != -1) {
        _this._setDefaultSetting();
        //if (contentMail)
            //_this.setContent(contentMail);

        //else
            _this.turnOnDesignMode();

        //khoi tao phim tat
        // var attachTo =
        // document.getElementById(_this._editorId).contentWindow.document;
        // mailbox.getInstance().disableKeyPress(attachTo);
        // mailbox.getInstance().hotkey.handleKeyEvent1(attachTo);
    } else {
        $("#" + this._editorId).load(function () {
            _this._setDefaultSetting();
            _this.turnOnDesignMode();
            if (contentMail)
                _this.setContent(contentMail);

            // var attachTo =
            // document.getElementById(_this._editorId).contentWindow.document;
            // mailbox.getInstance().disableKeyPress(attachTo);
            // mailbox.getInstance().hotkey.handleKeyEvent1(attachTo);
        });
    }

    //this.focus();
    for (var i = 0; i < this._controls.length; i++) {
        this._controls[i].handleMouseEvent();
    }
};

/**
 * set giá tri cho cac control
 *
 * @param {String}
 *            type kieu style font, size, background
 * @param {String}
 *            value gia tri tuong ung cua style
 */
BmailEditor.prototype.setSelectedStyle = function (type, value) {
    for (var i = 0; i < this._controls.length; i++) {
        this._controls[i].setSelectedStyle(type, value);
    }
};

/**
 * hàm thiết lập các chỉ số mặc định cho editor
 */
BmailEditor.prototype._setDefaultSetting = function () {
    var _this = this;

    var iframe = document.getElementById(this._editorId);
    this.curentElementToInsert = iframe.contentWindow.document.body;
    var iframeDoc = $('#' + this._editorId).contents().get(0);
    if (navigator.appName == "Microsoft Internet Explorer") {
        $(iframeDoc)
				.keydown(
						function (event) {
						    if (event.keyCode == 13) {
						        var e = document.getElementById(_this.getId()).contentWindow.document;
						        var id = "marker_"
										+ ("" + Math.random()).slice(2);
						        var html = '<span id="' + id + '"></span>';

						        if (window.getSelection) {// FF và Chrome
						            var idoc = iframe.contentDocument
											|| iframe.contentWindow.document;
						            var textRange = idoc.getSelection();
						        } else if (document.selection) {// IE
						            textRange = e.selection.createRange();
						        }

						        textRange.collapse(false);
						        textRange.pasteHTML('<br/>');
						        textRange.pasteHTML(html);
						        var markerSpan = e.getElementById(id);
						        textRange.moveToElementText(markerSpan);
						        textRange.select();
						        markerSpan.parentNode.removeChild(markerSpan);
						        return false;
						    }
						});
    }
    $("#" + _this._editorId).contents().find("body").css({
        "height": "95%"
    });
    $("#" + _this._editorId).contents().find("body").mouseover(function (event) {
        $(this).css('cursor', 'text');
    });
    $("#" + _this._editorId).contents().find("body").mouseout(function (event) {
        $(this).css('cursor', 'default');
    });

    /**
	 * xử lý khi click vào editor
	 */
    if (this.isNeedChangeWhenClick) {
        var iframe = document.getElementById(this._editorId);
        var iframeDoc = $('#' + this._editorId).contents().get(0);

        YAHOO.util.Event.on(iframeDoc, "paste", function (ev) {
            //$(iframeDoc).on("paste",function(ev)
            //{
            var html = ev.clipboardData.getData('text/html');
            if (html && BmailEditor.isExcel(html)) {
                ev.preventDefault();
                var div = document.createElement("div");
                div.innerHTML = html;
                var tmp = div.innerText;
                var index = tmp.indexOf("-->");
                tmp = tmp.substring(0, index);

                var index1 = tmp.indexOf('.xl65');
                var xl = tmp.substring(index1, tmp.length - 1);
                var xlArr = xl.split(".x");
                var l = xlArr.length;
                for (var i = 0 ; i < l ; i++) {
                    var tmp = xlArr[i];
                    if (tmp.indexOf("{") != -1 && tmp.indexOf("}") != -1) {
                        var index = tmp.indexOf("{");
                        var className = "x" + tmp.substring(0, index);
                        className = className.replace(/\s+/g, '');
                        var content = tmp.substring(index, tmp.length - 1);
                        content = content.replace("{", '');
                        content = content.replace("}", '');
                        var contentArr = content.split(";");
                        var l1 = contentArr.length;
                        var content1 = "";
                        for (var j = 0 ; j < l1; j++) {
                            var _content = contentArr[j];
                            _content2 = _content.replace(/\s+/g, '');
                            if (_content2 && _content2.indexOf("font-family") == -1)
                                content1 += $.trim(_content) + ";";

                        }
                        //content1 = content1.replace(/"/gi, '\\"');
                        //content = content.replace(/\s+/g ,'');
                        //content = content.replace(':.5pt',':1pt solid windowtext;');
                        var style = 'style="' + content1 + '"';
                        var cl = 'class=' + className;
                        html = html.replace(new RegExp(cl, 'g'), style);
                    }
                }



                // xl65 = xl65.replace("{" ,'');
                // xl65 = xl65.replace("}" ,'');
                // xl65 = xl65.replace(".xl65" ,'');
                // xl65 = xl65.replace(/\s+/g ,'');
                // xl65 = xl65.replace('border:.5ptsolidwindowtext;','border:1pt solid windowtext;');
                // var style = 'style="' + xl65 + '"';

                //var text = ev.clipboardData.getData('text/plain');
                html = BmailEditor.filter_msexcel(html);
                //html = html.replace(/class=xl65/g,style);
                html = html.substring(0, html.indexOf("</html>"));
                //html = html.replace(/<\/html>(.)*/g,'</html>');
                console.log(style);

                console.log(html);
                if (navigator.userAgent.indexOf('Chrome') != -1)
                    if (BmailEditor.getSelection(_this._editorId) == "")
                        _this.focus();

                EditorControl.execCommand(_this, "insertHTML", html);
            }

        });

        if (navigator.userAgent.indexOf('Chrome') != -1) {
            var checkAltKey = false;
            var checkCtrlKey = false;
            YAHOO.util.Event.on(iframeDoc, "keydown", function (e) {
                if (e.which == 18) {
                    checkAltKey = true;
                }
                else if (e.which == 17) {
                    checkCtrlKey = true;
                }
                else if (checkAltKey && e.which == 88) {
                    YAHOO.util.Event.preventDefault(e);
                    _this.execCommand("foreColor", false, "#0000FF");
                    // _this.focus();
                } else if (checkAltKey && e.which == 69) {
                    YAHOO.util.Event.preventDefault(e);
                    _this.execCommand("foreColor", false, "#FF0000");
                    _this.execCommand("strikeThrough", false, true);

                    // _this.focus();
                } else if (checkAltKey && e.which == 68) {
                    YAHOO.util.Event.preventDefault(e);
                    _this.execCommand("foreColor", false, "#FF0000");
                    // _this.focus();
                } else if (checkAltKey && e.which == 66) {
                    YAHOO.util.Event.preventDefault(e);
                    _this.execCommand("foreColor", false, "#000000");
                    // _this.focus();
                }
            });
        }
        YAHOO.util.Event.on(iframeDoc, "keyup", function (e) {
            if (e.which == 18) {
                checkAltKey = false;
            }
            if (e.which == 17) {
                checkCtrlKey = false;
            }
        });

        //khi chon mot van ban thi hien thi font family, font style, font size cua vung duoc chon
        $(iframeDoc)
				.click(
						function (e) {
						    if ($(e.target).is("img")) {
						        var target = $(e.target);
						        var currentWidth = target.width();
						        var currentHeight = target.height();
						        var sLnk = prompt("Nhập kích thước(Cao/Rộng): ", currentHeight + "/" + currentWidth);
						        if (sLnk !== null) {
						            var pos = sLnk.indexOf("/");
						            if (pos < 0) {
						                alert("Giá trị không đúng");
						                return;
						            }
						            var newHeight = sLnk.substring(0, pos);
						            var newWidth = sLnk.substring(pos + 1);
						            newHeight = parseInt(newHeight);
						            newWidth = parseInt(newWidth);
						            if (!newHeight || !newWidth) {
						                alert("Giá trị không đúng");
						                return;
						            }
						            target.css({
						                "width": newWidth,
						                "height": newHeight,
						            });
						            return;
						        }
						        return;
						    }
						    var isB = false;
						    var isU = false;
						    var isI = false;
						    var dfFont = "Tahoma";
						    var isFont = false;
						    var dfFontSize = 2;
						    var isFontSize = false;
						    var dfBgColor = '#fff';
						    var isBgColor = false;
						    var dfForceColor = '#000';
						    var isForceColor = false;
						    if (window.getSelection) {// FF và Chrome
						        var idoc = iframe.contentDocument
										|| iframe.contentWindow.document;
						        userSelection = idoc.getSelection();
						        var parentEl = userSelection.anchorNode.parentNode;
						        var content = '<br><div></div>';
						        var content1 = "";
						    } else if (document.selection) {// IE
						        userSelection = document.selection
										.createRange();
						        var parentEl = userSelection.parentElement();
						        var content = '<FONT face=Tahoma></FONT><BR>';
						        var content1 = '<FONT face=Tahoma></FONT>';
						    }
						    if (navigator.appName != "Microsoft Internet Explorer") {
						        if (parentEl instanceof HTMLHtmlElement)
						            _this.curentElementToInsert = iframe.contentWindow.document.body;
						        else
						            _this.curentElementToInsert = parentEl;
						        if (!parentEl)
						            return;
						    } else {
						        _this.curentElementToInsert = parentEl;
						        if (!parentEl)
						            return;
						    }

						    if (_this.curentElementToInsert == iframe.contentWindow.document.documentElement)
						        _this.curentElementToInsert = iframe.contentWindow.document.body;

						    if (_this.getContent() != content
									&& _this.getContent() != content1
									&& _this.getContent() != "") {// khi có
						        // nội dung
						        while ($(parentEl).attr('class') != "editor-iframe") {// duyệt
						            // đến
						            // khi
						            // gặp
						            // class
						            // ngoài
						            // cùng
						            if (!parentEl
											|| typeof parentEl == 'undefined')
						                break;
						            if (typeof parentEl.tagName == 'undefined')
						                break;
						            if (!isB)// tìm style chữ Bold
						            {
						                if (parseInt($(parentEl).css(
												"font-weight")) >= 700
												|| $(parentEl).css(
														"font-weight")
														.toLowerCase() == "bold")
						                    isB = true;
						                if ((typeof parentEl.tagName != 'undefined')
												&& (parentEl.tagName
														.toLowerCase() == "b" || parentEl.tagName
														.toLowerCase() == "strong"))
						                    isB = true;
						            }

						            if (!isU)// tìm style chữ underline
						            {
						                if ($(parentEl).css("text-decoration")
												.toLowerCase() == "underline")
						                    isU = true;
						                if ((typeof parentEl.tagName != 'undefined')
												&& parentEl.tagName
														.toLowerCase() == "u")
						                    isU = true;
						            }
						            if (!isI)// tìm style chữ italic
						            {
						                if ($(parentEl).css("font-style")
												.toLowerCase() == "italic")
						                    isI = true;
						                if ((typeof parentEl.tagName != 'undefined')
												&& (parentEl.tagName
														.toLowerCase() == "i" || parentEl.tagName
														.toLowerCase() == "em"))
						                    isI = true;
						            }
						            if (!isFont)// tìm font
						            {
						                if ($(parentEl).attr("style")
												&& $(parentEl).attr("style")
														.toLowerCase().indexOf(
																"font-family") != -1) {
						                    isFont = true;
						                    dfFont = $(parentEl).css(
													"font-family");
						                }
						                if ((typeof parentEl.tagName != 'undefined')
												&& (parentEl.tagName
														.toLowerCase() == "font" && $(
														parentEl).attr('face') != "")) {
						                    isFont = true;
						                    dfFont = $(parentEl).attr('face');
						                }
						            }
						            if (!isForceColor)// màu chữ
						            {
						                if ((typeof parentEl.tagName != 'undefined')
												&& parentEl.tagName
														.toLowerCase() == "font"
												&& $(parentEl).attr("color")
												&& $(parentEl).attr('color') != "") {
						                    isForceColor = true;
						                    dfForceColor = $(parentEl).attr(
													'color');
						                }
						                if ((typeof parentEl.tagName != 'undefined')
												&& parentEl.tagName
														.toLowerCase() == "span"
												&& $(parentEl).attr("style")
												&& $(parentEl).attr("style")
														.toLowerCase().indexOf(
																'color') != -1) {
						                    isForceColor = true;
						                    dfForceColor = $(parentEl).css(
													'color');
						                }
						            }
						            if (!isBgColor)// màu nền
						            {
						                if ((typeof parentEl.tagName != 'undefined')
												&& parentEl.tagName
														.toLowerCase() == "span"
												&& $(parentEl).attr("style")
												&& $(parentEl)
														.attr("style")
														.toLowerCase()
														.indexOf(
																'background-color') != -1) {
						                    isBgColor = true;
						                    dfBgColor = $(parentEl).css(
													'background-color');
						                }
						            }
						            if (!isFontSize)// tìm font
						            {
						                if ((typeof parentEl.tagName != 'undefined')
												&& $(parentEl).attr("style")
												&& $(parentEl).attr("style")
														.toLowerCase().indexOf(
																"font-size") != -1) {
						                    isFontSize = true;
						                    var size = parseInt($(parentEl)
													.css("font-size"));
						                    if ($(parentEl).css("font-size")
													.indexOf("pt") != -1)
						                        size = size * 1.3;
						                    if (size < 10) {
						                        dfFontSize = 0;
						                    } else if (size < 12) {
						                        dfFontSize = 1;
						                    } else if (size < 14) {
						                        dfFontSize = 2;
						                    } else if (size < 18) {
						                        dfFontSize = 3;
						                    } else if (size < 24) {
						                        dfFontSize = 4;
						                    } else if (size < 30) {
						                        dfFontSize = 5;
						                    } else {
						                        dfFontSize = 6;
						                    }
						                }
						                if ((typeof parentEl.tagName != 'undefined')
												&& parentEl.tagName
														.toLowerCase() == "font"
												&& $(parentEl).attr('size')) {
						                    var size = parseInt($(parentEl)
													.attr('size'));
						                    if (size > 6) {
						                        size = 6;
						                    }
						                    isFontSize = true;
						                    dfFontSize = size;
						                }
						            }

						            parentEl = parentEl.parentNode;
						            if (!parentEl
											|| typeof parentEl == 'undefined')
						                break;
						        }
						        if (!isFont)
						            _this.setSelectedStyle('font',
											"Times New Roman");
						        else
						            _this.setSelectedStyle('font', dfFont);
						        _this.setSelectedStyle('fontsize', dfFontSize);
						        _this.setSelectedStyle('bold', isB);
						        _this.setSelectedStyle('underline', isU);
						        _this.setSelectedStyle('italic', isI);
						        _this.setSelectedStyle('fontcolor',
										dfForceColor);
						        _this.setSelectedStyle('backColor', dfBgColor);
						    }
						    // set default
						});
    }
};

/**
 * disable check spelling
 */
BmailEditor.prototype.disableCheckSpelling = function () {
};

/**
 * Khởi tạo hành động của editor, bao gồm hành động của các control điểu khiển
 * nó
 */
BmailEditor.prototype._initAction = function () {
    for (var i = 0; i < this._controls.length; i++) {
        this._controls[i].action();
    }
};

/**
 * lấy về id của editor
 */
BmailEditor.prototype.getId = function () {
    return this._editorId;
};

BmailEditor.prototype.setSize = function (width, height) {
    $("#" + this._editorId).width(width);
    $("#" + this._editorId).height(height);
};

BmailEditor.prototype.setHeight = function (height) {
    $("#" + this._editorId).height(height);
};

/**
 * get content
 */
BmailEditor.prototype.getContent = function () {
    var iFrameBody;
    var iFrame = document.getElementById(this._editorId);
    if (iFrame && iFrame.contentWindow) {// IE
        iFrameBody = iFrame.contentWindow.document.getElementsByTagName('body')[0];
    } else if (iFrame && iFrame.contentDocument) {// FF
        iFrameBody = iFrame.contentDocument.getElementsByTagName('body')[0];
    }
    return iFrameBody.innerHTML;
};

/**
*	get plain text content
*/
BmailEditor.prototype.getPlainTextContent = function () {
    var iFrameBody;
    var iFrame = document.getElementById(this._editorId);
    if (iFrame && iFrame.contentWindow) {// IE
        iFrameBody = iFrame.contentWindow.document.getElementsByTagName('body')[0];
    } else if (iFrame && iFrame.contentDocument) {// FF
        iFrameBody = iFrame.contentDocument.getElementsByTagName('body')[0];
    }
    var text = "";
    if (iFrameBody.textContent)
        return iFrameBody.textContent;

    return iFrameBody.innerText;
};

/**
 *
 */
BmailEditor.prototype.getContentSpan = function () {
    var iFrameBody;
    var iFrame = document.getElementById(this._editorId);
    if (iFrame && iFrame.contentDocument) {// FF
        iFrameBody = iFrame.contentDocument.getElementsByTagName('body')[0];
    } else if (iFrame && iFrame.contentWindow) {// IE
        iFrameBody = iFrame.contentWindow.document.getElementsByTagName('body')[0];
    }
    $(iFrameBody).find("font").each(
			function () {
			    var font = $(this).attr("face");
			    var size = parseInt($(this).attr("size"));
			    var style = "";
			    if (font != "") {
			        style += " font-family:" + font + ";";
			        $(this).removeAttr("face");
			    }
			    if (size > 0) {
			        var fontsizes = new Array(8, 10, 12, 14, 18, 24, 36);
			        value = 12;
			        jQuery.each(fontsizes, function (i, item) {
			            if (i == size)
			                value = item;
			        });
			        style += " font-size:" + value + "px;";
			        $(this).removeAttr("size");
			    }
			    $(this).attr("style", style);
			    $(this).replaceWith(
						'<span style="' + style + '">' + $(this).html()
								+ '</span>');
			});
    return iFrameBody.innerHTML;
};

BmailEditor.prototype.getInnerText = function () {
    return $("#" + this._editorId).text();
};
/**
 * focus
 */
BmailEditor.prototype.focus = function () {
    if (navigator.userAgent.indexOf('Chrome') != -1) {
        var frame = $('#' + this._editorId).get(0).contentWindow;
        $(frame).click();
        frame.focus();

        if (frame.getSelection) {
            if (frame.document.body.innerHTML == '')
                frame.getSelection().extend(frame.document.body, 0);
            else
                frame.getSelection().collapseToEnd();
        } else if (frame.document.body.createTextRange) {
            var range = frame.document.body.createTextRange();
            range.moveEnd('character', frame.document.body.innerHTML.length);
            range.collapse(false);
            range.select();
        }
        //this.focusChrome();
        /*var _this = this;
		window.setTimeout(function() {
			_this.focusChrome();
		}, 100);*/
    }
    if (document.getElementById(this._editorId)
			&& document.getElementById(this._editorId).contentWindow)
        document.getElementById(this._editorId).contentWindow.focus();
    // var iskeypress = 0;

    // window.setTimeout(function() {
    // if (!mailbox.getInstance().viEnabled) {
    // Mudim.method = 0;
    // $(".bmeditor-vietkeybutton img").attr('src',
    // 'js/editor/icons/engkey.png');
    // } else {
    // Mudim.method = 2;
    // $(".bmeditor-vietkeybutton img").attr('src',
    // 'js/editor/icons/vietkey.png');
    // }
};
/**
 * focus in Chrome
 */
BmailEditor.prototype.focusChrome = function () {
    var iframe = document.getElementById(this._editorId);
    if (window.getSelection) {// FF và Chrome
        var idoc = iframe.contentDocument || iframe.contentWindow.document;
        var userSelection = idoc.getSelection();
        var range = userSelection.getRangeAt(0);

        userSelection.removeAllRanges();
        userSelection.addRange(range);
    }
};

/**
 *
 */
BmailEditor.prototype.execCommand = function (aCommandName, aShowDefaultUI,
		aValueArgument) {
    if (document.getElementById(this._editorId)
			&& document.getElementById(this._editorId).contentWindow
			&& document.getElementById(this._editorId).contentWindow.document)
        document.getElementById(this._editorId).contentWindow.document
				.execCommand(aCommandName, aShowDefaultUI, aValueArgument);

    // this.setCurrentStyle(aCommandName, aValueArgument);
};

/**
 * Sinh ra nội dung html của toolbar, góp nhặt từ html của tất cả các điều khiển
 * để tạo thành toolbar của editor
 */
BmailEditor.prototype._getToolbarContent = function () {
    var toolbar = '<div  class="rte-toolbar"><table id="' + this._toolbar
			+ '">' + '<tbody>' + ' <tr>';
    for (var i = 0; i < this._controls.length; i++) {
        toolbar += '<td>' + this._controls[i].genHtmlContent() + '</td>';
    }
    toolbar += '</tr>' + '</tbody>' + '</table></div>';
    return toolbar;
};

/**
 * Sinh ra nội dung html cho editor
 */
BmailEditor.prototype._generateHtmlContent = function () {
    var html = this._getToolbarContent()
			+ '<iframe type="content" id="'
			+ this._editorId
			+ '"  class="editor-iframe" style="background-color: #fff; overflow-y:auto;" width="100%" frameBorder="0"></iframe>';
    return html;
};
/**
 * Attach listener sử lý sự kiện cho editor
 *
 * @param {String}
 *            eventName tên sự kiện
 * @param {Function}
 *            hanlder hàm callback xử lý sự kiện
 */
BmailEditor.prototype.bind = function (eventName, handler) {
    var _this = this;
    if (document.getElementById(_this._editorId))
        $(document.getElementById(_this._editorId)).bind(eventName, handler);
};

/**
 * ẩn/hiện toolbar
 */
BmailEditor.prototype.toggleToolbar = function () {
    var _this = this;
    if (this._toolbarShowed) {
        $("#" + _this._toolbar).hide("blind", 100);
        this._toolbarShowed = false;
    } else {
        $("#" + _this._toolbar).show("blind", 100);
        this._toolbarShowed = true;
    }
};

BmailEditor.prototype.getSelected = function () {
    var iframe = document.getElementById(this._editorId);
    var iframeDoc = $('#' + this._editorId).contents().get(0);
    if (window.getSelection) {// FF và Chrome
        var idoc = iframe.contentDocument || iframe.contentWindow.document;
        userSelection = idoc.getSelection();
        var parentEl = userSelection.anchorNode.parentNode;
        iFrameBody = iframe.contentWindow.document.getElementsByTagName('body')[0];
        // userSelection.collapseToEnd();
        // var oRng2 = userSelection.duplicate();

        var range = userSelection.getRangeAt(0);

        return range;
    } else if (document.selection) {// IE
        var idoc = iframe.contentDocument || iframe.contentWindow.document;
        userSelection = idoc.selection.createRange();
        var parentEl = userSelection.parentElement();
        iFrameBody = iframe.contentWindow.document.getElementsByTagName('body')[0];

        var oRng2 = userSelection.duplicate();

        if (userSelection.text.length > 0) {
            oRng2.moveToElementText(iFrameBody);
            oRng2.setEndPoint("StartToEnd", userSelection);
            // console.log(oRng2.text.replace(/\r\n/g,'')
            // +"-"+oRng2.text.replace(/\r\n/g,'').length);
            iEnd = iFrameBody.innerText.length - oRng2.text.length;

            iEnd2 = iFrameBody.innerText.replace(/\r\n/g, '').length
					- oRng2.text.replace(/\r\n/g, '').length;

            oRng2.setEndPoint("StartToStart", userSelection);
            // console.log(oRng2.text.replace(/\r\n/g,'')
            // +"-"+oRng2.text.replace(/\r\n/g,'').length);
            // console.log(oRng2.text+"-"+oRng2.text.length);
            // console.log("----------------------------------------");
            iStart = iFrameBody.innerText.length - oRng2.text.length;
            iStart2 = iFrameBody.innerText.replace(/\r\n/g, '').length
					- oRng2.text.replace(/\r\n/g, '').length;

            // console.log(iFrameBody.innerText.replace(/\r\n/g,'') +"-"
            // +iFrameBody.innerText.replace(/\r\n/g,'').length);
            // console.log(iFrameBody.innerText +"-"
            // +iFrameBody.innerText.length);
            // console.log(userSelection.text);
        } else {
            iStart = iFrameBody.innerText.length;
            iEnd = iFrameBody.innerText.length;
        }

        // console.log(userSelection.text);
        // console.log(iStart +"-" + iEnd);
        // console.log(iStart2 +"-" + iEnd2);
        // console.log(iFrameBody.innerText);
        return {
            start: (iStart + iStart2) / 2,
            end: (iEnd + iEnd2) / 2,
            text: userSelection.text
        };
    }
};
BmailEditor.prototype.removeAllRanges = function () {
    return;
    var iframe = document.getElementById(this._editorId);
    if (window.getSelection) {// FF và Chrome
        var idoc = iframe.contentDocument || iframe.contentWindow.document;
        userSelection = idoc.getSelection();
        userSelection.removeAllRanges();
    }
};
BmailEditor.prototype.addRange = function (range) {
    this.focus();
    var iframe = document.getElementById(this._editorId);
    if (window.getSelection) {// FF và Chrome
        var idoc = iframe.contentDocument || iframe.contentWindow.document;
        userSelection = idoc.getSelection();
        userSelection.removeAllRanges();
        userSelection.addRange(range);
    }
};

BmailEditor.prototype.setSelected = function (start, end) {
    var iframe = document.getElementById(this._editorId);
    if (window.getSelection) {// FF và Chrome
    } else if (document.selection) {// IE
        var iFrameBody = iframe.contentWindow.document
				.getElementsByTagName('body')[0];
        var idoc = iframe.contentDocument || iframe.contentWindow.document;
        // idoc.selection.empty();
        var sel = idoc.selection.createRange();

        sel.moveStart("character", -iFrameBody.innerText.length);

        sel.collapse(true);
        sel.moveStart('character', start);
        sel.collapse(true);
        sel.moveEnd('character', end - start);
        sel.select();
        // this.focus();
    }
    // console.log($(iFrameBody).html());
    // console.log(start +'-'+end);
};
BmailEditor.getSelection = function (divID) {
    var html = "";

    var iframe = document.getElementById(divID);
    var idoc = iframe.contentDocument || iframe.contentWindow.document;

    var selection = idoc.getSelection();
    html = selection.toString();
    return html;
};

BmailEditor.isExcel = function(html)
{
    if(html.indexOf('urn:schemas-microsoft-com:office:office') !=-1 
    && html.indexOf('urn:schemas-microsoft-com:office:excel') !=-1
    && html.indexOf('urn:schemas-microsoft-com:office:word') ==-1)
        return true;

    return false;
};

BmailEditor.filter_msexcel = function (html) {
    html = html.replace(/border:.5pt/g, 'border:1pt');
    //html = html.replace(/<o:p>\s*<\/o:p>/g, '');
    //html = html.replace(/<o:p>[\s\S]*?<\/o:p>/g, '&nbsp;');


    // //Remove mso-? styles.
    // html = html.replace( /\s*mso-[^:]+:[^;"]+;?/gi, '');

    // //Remove more bogus MS styles.
    // html = html.replace( /\s*MARGIN: 0cm 0cm 0pt\s*;/gi, '');
    // html = html.replace( /\s*MARGIN: 0cm 0cm 0pt\s*"/gi, "\"");
    // html = html.replace( /\s*TEXT-INDENT: 0cm\s*;/gi, '');
    // html = html.replace( /\s*TEXT-INDENT: 0cm\s*"/gi, "\"");
    // html = html.replace( /\s*PAGE-BREAK-BEFORE: [^\s;]+;?"/gi, "\"");
    // html = html.replace( /\s*FONT-VARIANT: [^\s;]+;?"/gi, "\"" );
    // html = html.replace( /\s*tab-stops:[^;"]*;?/gi, '');
    // html = html.replace( /\s*tab-stops:[^"]*/gi, '');

    // //Remove XML declarations
    // html = html.replace(/<\\?\?xml[^>]*>/gi, '');

    // //Remove lang
    // html = html.replace(/<(\w[^>]*) lang=([^ |>]*)([^>]*)/gi, "<$1$3");

    // //Remove language tags
    // html = html.replace( /<(\w[^>]*) language=([^ |>]*)([^>]*)/gi, "<$1$3");

    // //Remove onmouseover and onmouseout events (from MS Word comments effect)
    // html = html.replace( /<(\w[^>]*) onmouseover="([^\"]*)"([^>]*)/gi, "<$1$3");
    // html = html.replace( /<(\w[^>]*) onmouseout="([^\"]*)"([^>]*)/gi, "<$1$3");

    return html;
};