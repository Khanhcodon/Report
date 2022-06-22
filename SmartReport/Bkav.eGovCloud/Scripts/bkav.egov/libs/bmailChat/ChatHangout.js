/**
 * Giao diện chat hangout
 * 
 * @author nobita
 */

function ChatHangout() {
	this.username = readCookie("bkavUsername");
	this.authToken = readCookie("bkavAuthen");
	this.accountList = [];

	this.popupChatManager = [];

	this.getAccountList();

};

ChatHangout.CHAT_FRAME_ID = "chat";
ChatHangout.CHAT_WINDOW = document.getElementById(ChatHangout.CHAT_FRAME_ID).contentWindow;

ChatHangout.ChAT_FORM_ID = "chat-form";
ChatHangout.ChAT_LIST_ID = "chat-list";

ChatHangout.getInstance = function() {
	if (!this._instance)
		this._instance = new ChatHangout();

	return this._instance;
};

/**
 * lấy danh sách chat
 */
ChatHangout.prototype.getAccountList = function() {
	this.accountList = ChatHangout.CHAT_WINDOW.AccountListController
			.getInstance()._model.list;
};

ChatHangout.prototype.handleEvent = function() {

};

ChatHangout.prototype.getPopupChat = function(account) {

};

ChatHangout.protoype.createPopupChat = function(account) {

};

ChatHangout.protoype.addPopupChat = function(account, popup) {
	this.popupChatManager.push({
		account : account,
		popup : popup
	});

	$("#" + ChatHangout.ChAT_FORM_ID).append(popup.initPopup());
};

ChatHangout.protoype.removePopupChat = function(account) {

	var l = this.popupChatManager.length;

	for ( var i = 0; i < l; i++) {
		var _account = this.popupChatManager[i].account;

	}
};