/**
 * @author nobita 
 *
 * là một điều khiển trên toolbar của editor
 * 
 * @param {BmailEditor}
 *            editor
 */

EditorControl = function(editor) {
	this._editor = editor;
	this.id = null;
};

EditorControl.prototype.genId = function() {
	BmailEditor.INDEX++;
	this._id = "editor-control-" + BmailEditor.INDEX;
	this.index = BmailEditor.INDEX;
	return this._id;
};

EditorControl.prototype.getIndex = function() {
	return this.index;
};

EditorControl.prototype.getEditorId = function ()
{
    return this._editor._editorId;
};
EditorControl.prototype.getId = function() {
	return this.id;
};

/**
 * Gắn editor với control
 * 
 * @param {BmailEditor}
 *            editor
 */
EditorControl.prototype.attachEditor = function(editor) {
	this._editor = editor;
};

EditorControl.prototype.getClearContent = function() {
};

/**
 * sinh ra nội dung html của control
 */
EditorControl.prototype.genHtmlContent = function() {
	return "";
};

/**
 * Hàm vẽ giao diện cho control
 */
EditorControl.prototype.render = function() {

};

/**
 * Hàm thực hiện hành động của control khi được click
 */
EditorControl.prototype.action = function() {

};

EditorControl.prototype.selectAction = function(selectname) {
	var _this = this;
	$("#" + this._id)
			.change(
					function() {
						_this._editor.focus();
						var cursel = document.getElementById(_this._id).selectedIndex;
						var selected = document.getElementById(_this._id).options[cursel].value;
						_this._editor.execCommand(selectname, false, selected);
						_this._editor.focus();
					});
};

/**
 * thay đổi trạng thái khi hover, out
 */
EditorControl.prototype.handleMouseEvent = function() {

	// trường hợp img
	if ($("#" + this._id).find("img").length > 0) {

		$("#" + this._id).find("img").mouseover(function() {

			var src = $(this).attr("src");
			if (src && src.indexOf("_hover") == -1) {
				var srcArr = src.split("/");
				var imgName = srcArr[srcArr.length - 1];
				var imgNameArr = imgName.split(".");
				imgName = imgNameArr[0] + "_hover." + imgNameArr[1];
				var _imgName = "";
				for ( var i = 0; i < srcArr.length - 1; i++) {
					_imgName += srcArr[i] + "/";

				}
				imgName = _imgName + imgName;

				$(this).attr("src", imgName);
			}

		});

		$("#" + this._id).find("img").mouseout(function() {

			var src = $(this).attr("src");
			if (src && src.indexOf("_hover") != -1) {
				var imgName = src.replace("_hover", "");
				$(this).attr("src", imgName);
			}

		});
	}
	// trường họp input
	else {
	    $("#" + this._id).mouseover(function () {
			var src = $(this).css("background-image");
			if (src && src.indexOf("_hover") == -1 && src!=="none") {
				var srcArr = src.split("/");
				var imgName = srcArr[srcArr.length - 1];
				var imgNameArr = imgName.split(".");
				imgName = imgNameArr[0] + "_hover." + imgNameArr[1];
				var _imgName = "";
				for ( var i = 0; i < srcArr.length - 1; i++) {
					_imgName += srcArr[i] + "/";

				}
				imgName = _imgName + imgName;

				var _this = this;
				var img = new Image();
				img.onload = function() {
					$(_this).css("background-image", imgName);
				};
				var imgSrc = imgName.replace("url(", "");
				imgSrc = imgSrc.replace(")", "");
				img.src = imgSrc;
			}
		});

		$("#" + this._id).mouseout(function() {
			var src = $(this).css("background-image");

			if (src && src.indexOf("_hover") != -1) {

				var imgName = src.replace("_hover", "");
				$(this).css("background-image", imgName);
			}
		});
	}
};

/**
 * thực thi command
 */
EditorControl.execCommand = function(editor, command, value) {
	if (navigator.userAgent.indexOf('Chrome') == -1) {
		editor.execCommand(command, false, value);
		editor.focus();
	} else
		window.setTimeout(function() {
			editor.execCommand(command, false, value);
		}, 10);
};
