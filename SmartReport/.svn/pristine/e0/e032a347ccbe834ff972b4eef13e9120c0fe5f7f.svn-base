/**
 * @author nobita
 *
 * @param {String} id
 * @param {String} html
 */
MenuButton = function(id, html) {
    this._controlId = id;
    this._id = id + "-menu";
    this._html = html;
    this._isOpen = false;
};
MenuButton.prototype.render = function() {
    $("#" + this._controlId).after('<div class="button-menu ui-widget-content" id="' + this._id + '">' + (this._html || this._getHtmlContent()) + '</div>');
    this._action();
};
MenuButton.prototype._getHtmlContent = function() {
    return "";
};

MenuButton.prototype._action = function() {
    var _this = this;
    $("#" + this._controlId).click(function(event) {
        $(document).trigger('click');
        if (!_this._isOpen) {

            _this._isOpen = true;
            $("#" + _this._id).show();

            if (navigator.appName == "Microsoft Internet Explorer") {
                $("#" + _this._id).position({
                    of : $("#" + _this._controlId),
                    my : "left top",
                    at : "left bottom"
                });
                if ($("#" + _this._id).css("left") == "0px")
                    $("#" + _this._id).css("left", "225px");
            }

        } else {
            _this._closeMenu();
        }
        return false;
    });
    $(document).bind("click blur", function() {
        _this._closeMenu();
    });
    $(window).bind("blur", function() {
        _this._closeMenu();
    });
};
MenuButton.prototype._closeMenu = function() {
    if (this._isOpen) {
        $("#" + this._id).hide();
        this._isOpen = false;
    }
};
MenuButton.prototype.click = function(handler) {
    var _this = this;
    $("#" + this._id + " a").click(function(e) {
        _this._closeMenu();
        var value = $(this).attr("value");
        handler(value);
        return false;
    });
};

/**
 * Emoticon Menu
 */
EmoticonMenu = function(id, html) {
    MenuButton.call(this, id);
};
EmoticonMenu.prototype = new MenuButton;
EmoticonMenu.constructor = EmoticonMenu;
EmoticonMenu.prototype._getHtmlContent = function() {
    //var ipServer = Session.getInstance().getIpServer();
    //var url = location.protocol + '//' + ipServer + "/mail/";
    var content = '<table style="z-index:99999;" cellpadding="2">';

    for (var j = 0; j <= 7; j++) {
        content += '<tr>';
        for (var i = 1; i <= 11; i++) {
            var value = 11 * j + i;
            var td = '<td align="center" class="tdEmoticon"><div class="aEmoticon" value="' + value + '" href="#" id="' + value + '"></div>' + '</td>';
            content += td;
        }
        content += '</tr>';
    }

    content += '</table>';
    return content;
};
EmoticonMenu.prototype.click = function(handler) {
    var _this = this;
    $("#" + this._id + " .aEmoticon").click(function(e) {
        _this._closeMenu();
        var value = $(this).attr("value");
        handler(value);
        return false;
    });
};
EmoticonMenu.prototype.renImage = function() {
    var l = $("#" + this._id).find(".aEmoticon").length;
    //var ipServer = Session.getInstance().getIpServer();
    //var url = location.protocol + '//' + ipServer + "/mail/";
    for (var i = 0; i < l; i++) {
        var tmp = $("#" + this._id).find(".aEmoticon")[i];
        var value = $(tmp).attr("value");
        var xPos = -1 * (21 * (value - 1 ));
        var yPos = 0;
        $(tmp).css("background-position", xPos + "px" + " 0px");
    }

    $(".aEmoticon").mouseover(function() {
        $(this).css("border", "1px solid red");
    });
    $(".aEmoticon").mouseout(function() {
        $(this).css("border", "1px solid #fff");
    });
};
EmoticonMenu.prototype._action = function() {
    var _this = this;
    $("#" + this._controlId).click(function(event) {

        if (!_this._isOpen) {

            _this._isOpen = true;
            $("#" + _this._id).show();
            _this.renImage();
            if (navigator.appName == "Microsoft Internet Explorer") {
                $("#" + _this._id).position({
                    of : $("#" + _this._controlId),
                    my : "left top",
                    at : "left bottom"
                });
                if ($("#" + _this._id).css("left") == "0px")
                    $("#" + _this._id).css("left", "225px");
            }

        } else {
            _this._closeMenu();
        }
    });
    $(document).bind("click blur ", function(e) {
        if (!$(e.target).attr("id") || $(e.target).attr("id") != _this._controlId)
            _this._closeMenu();
    });
    $(window).bind("blur", function() {
        _this._closeMenu();
    });
    if($("#divSignatureSetup").length > 0)
    {
    	$("#divSignatureSetup").scroll(function()
    	{
    		_this._closeMenu();
    	});
    }
};
/**
 * Emoticon Menu
 */
MailEmoticonMenu = function(id, html) {
    MenuButton.call(this, id);
};
MailEmoticonMenu.prototype = new MenuButton;
MailEmoticonMenu.constructor = EmoticonMenu;
MailEmoticonMenu.prototype._getHtmlContent = function() {

    var content = '<table style="z-index:99999;" cellpadding="2">';

    for (var j = 0; j <= 6; j++) {
        content += '<tr>';
        for (var i = 1; i <= 6; i++) {
            var value = 6 * j + i;
            var td = '<td align="center" class="tdMailEmoticon"><div class="aMailEmoticon" value="' + value + '" href="#" id="' + value + '"></div>' + '</td>';
            content += td;
        }
        content += '</tr>';
    }

    content += '</table>';
    return content;
};
MailEmoticonMenu.prototype.click = function(handler) {
    var _this = this;
    $("#" + this._id + " .aMailEmoticon").click(function(e) {
        _this._closeMenu();
        var value = $(this).attr("value");
        handler(value);
        return false;
    });
};
MailEmoticonMenu.prototype.renImage = function() {
    var l = $("#" + this._id).find(".aMailEmoticon").length;
    for (var i = 0; i < l; i++) {
        var tmp = $("#" + this._id).find(".aMailEmoticon")[i];
        var value = $(tmp).attr("value");
        var xPos = -1 * (30 * (value - 1 ));
        var yPos = 0;
        $(tmp).css("background-position", xPos + "px" + " 0px");
    }

    $(".aMailEmoticon").mouseover(function() {
        $(this).css("border", "1px solid red");
    });
    $(".aMailEmoticon").mouseout(function() {
        $(this).css("border", "1px solid #fff");
    });
};
MailEmoticonMenu.prototype._action = function() {
    var _this = this;
    $("#" + this._controlId).click(function(event) {
        if (!_this._isOpen) {

            _this._isOpen = true;
            $("#" + _this._id).show();
            _this.renImage();
            if (navigator.appName == "Microsoft Internet Explorer") {
                $("#" + _this._id).position({
                    of : $("#" + _this._controlId),
                    my : "left top",
                    at : "left bottom"
                });
                if ($("#" + _this._id).css("left") == "0px")
                    $("#" + _this._id).css("left", "225px");
            }

        } else {
            _this._closeMenu();
        }
    });
    $(document).bind("click blur ", function(e) {
        if (!$(e.target).attr("id") || $(e.target).attr("id") != _this._controlId)
            _this._closeMenu();
    });
    $(window).bind("blur", function() {
        _this._closeMenu();
    });
};
