/**
 * @author nobita
 * 
 * popup chat với từng người khi chat hangout
 */
function PopupChat(args) {

	this.friend = args.friend;
	this.thread = args.thread || "";
	this.avatar = "";
	this.thread = "";
};

PopupChat.templateSentMsg = '<li class="clearfix list-group-item">'
		+ '<div class="chat-body right pull-right">'
		+ '<div class="tooltip-arrow"></div>'
		+ '<div class="tooltip-inner">';

PopupChat.templateCloseMsg = '</div>' + '</div>' + '</li>';

PopupChat.templateRecievedMsg = '<li class="clearfix list-group-item">'
		+ '<div class="chat-body left">'
		+ '<div class="tooltip-arrow"></div>'
		+ '<div class="tooltip-inner">';

PopupChat.templateFriendInformation = '<div class="panel-body">'
+'<div class="row">'
+'<div class="col-md-3">'
+'<img class="img-circle chat-image">'
+'</div>'
+'<div class="col-md-13">'
+'<div class="bold-text">'
+'</div>'
+'<div class="second-color">'
+'<span></span>'
+'</div>'
+'</div>'
+'</div>'
+'</div>';

PopupChat.templateHeader = '<div class="panel-heading primary-color white-color">'
+'<span class="icon-record online-color qtooltip" data-hasqtip="36" oldtitle="Online" title=""></span>'
+'<span class="icon-cross pull-right qtooltip close" data-hasqtip="40" oldtitle="Đóng" title=""></span>'
+'<span class="icon-minus4 pull-right qtooltip minimize" data-hasqtip="41" oldtitle="Thu gọn" title=""></span>'
+'</div></div>';

PopupChat.templateDivHeader = '<div class="panel full-height z-depth-3">';

PopupChat.templateBody = '<ul class="chat list-group" style="overflow-y: hidden; outline: none;" tabindex="5001"></ul>';
PopupChat.templateFooter = '<div class="panel-footer">'
+'<div class="input-group input-group-sm">'
+'<input type="text" class="form-control chat-editor" placeholder="Nhập nội dung chat...">'
+'<span class="input-group-btn">'
+'<button class="btn btn-default" type="button">'
+'<span class="icon-cool"></span>'
+'</button>'
+'</span>'
+'</div></div>';


PopupChat.prototype.getBodyChatContainer = function() {
	return $("#" + this.getPopupId() + " .chat list-group");
};

PopupChat.prototype.getPopupId = function() {
	return this.friend.account;
};

/**
 * khởi tạo popup
 */
PopupChat.prototype.initPopup = function() {
	
	var html = PopupChat.templateDivHeader + PopupChat.templateHeader +PopupChat.templateFriendInformation + PopupChat.templateBody +PopupChat.templateFooter;
	html += "</div>";
	
	return html;
};

/**
 * Tạo thông tin chat
 */
PopupChat.protoype.initBodyChat = function()
{
	return PopupChat.templateBody;
	};
	
/**
 * tạo header chứa button
 */	
PopupChat.prototype.initHeader = function(){
	return PopupChat.templateHeader;
};

/**
 * thông tin người chat
 */
PopupChat.prototype.initInformation = function()
{
	return PopupChat.templateFriendInformation;
};

/**
 * editor chat
 */
PopupChat.prototype.initFooter = function()
{
	return PopupChat.templateFooter;
};
PopupChat.prototype.getAvatar = function()
{
	return 'https://danhba.bkav.com/avatars/' + this.getPopupId() + '.bmp';
};
/**
 * minimize poup
 */
PopupChat.prototype.minimizePopup = function(that) {
	$(that).parents('li').hide();
    if ($('#chatList #btnNewChat').is(':hidden')) {
        showNewChatButton();
    }
    $('#chatList').prepend($('#ChatMinimize').tmpl({ avatar: this.getAvatar(), name: this.friend.fullName, target: $(that).parents('li').attr('id') }));
};

/**
 * maximize popup
 */
PopupChat.prototype.maximizePopup = function() {

};

/**
 * close tab và đóng thread
 */
PopupChat.prototype.closeChat = function(that) {
	$(that).qtip('hide');
    $(that).parents('li').remove();
    if ($('#chatList li').length === 2 && $('.chat-form li').length === 0) {
        $('#chatList #btnNewChat').hide();
    }
};

/**
 * gửi tin chat
 */
PopupChat.prototype.sendMsg = function(msg) {
	ChatHangout.CHAT_WINDOW.sendIMMessage(null, null, null,
			null, this.friend.fullAccount, this.thread,
			null, msg,
			null);
};

/**
 * update msg khi nhận được tin chat mới hoặc là khi gửi một tin chat mới đi
 */
PopupChat.prototype.updateMsg = function(msg, isSent) {
	var msgLi = "";
	
	if(isSent)
		msgLi = PopupChat.templateSentMsg + msg + PopupChat.templateCloseMsg;
	else
		msgLi = PopupChat.templateRecievedMsg + msg + PopupChat.templateCloseMsg;
	
	var container = this.getBodyChatContainer();
	$(container).append(msgLi);
};

PopupChat.prototype.sendTyping = function() {
	var _tabChat = ChatHangout.CHAT_WINDOW.tabChat();
	_tabChat.sendTyping(this.friend.account);
};

PopupChat.prototype.sendEndTyping = function() {
	var _tabChat = ChatHangout.CHAT_WINDOW.tabChat();
	_tabChat.endTyping(this.friend.account);
};

/**
 * reset lại khung chat
 */
PopupChat.prototype.resetChat = function(msg) {
	$("#" + popupId + " .chat-editor").val("");
};

/**
 * bắt các sự kiện trên popup chat
 */
PopupChat.prototype.handleEvent = function() {

	var _this = this;
	var popupId = this.getPopupId();

	$("#" + popupId + " .chat-editor").keypress(function(ev) {
		if (ev.keyCode == 13) {
			var msg = $("#" + popupId + " .chat-editor").val();

			_this.sendMsg(msg);
		} else if (ev.keyCode == 8) {
			if ($("#" + popupId + " .chat-editor").val() == "") {
				_this.sendEndTyping();
			}
		} else {
			_this.sendTyping();
		}
	});
	
	$("#" + popupId + " .close").click(function(ev) 
			{
			_this.closeChat(this);
			});
	$("#" + popupId + " .minimize").click(function(ev) 
			{
			_this.minimizePopup(this);
			});
};