function ViewMail(message) {
	this.msg = message;
	if (isDebug) {
		console.log(this.msg.getNumberOfPart());
	}
}


ViewMail.prototype.makeMsgHeader = function(sender, subject, displayName, to) {

	$("#mailSenderValue").html(sender);
	if (!Util.isUndefined(to)) {
		var _to = [];
		var l = to.length;
		if (to.length > 3)
			l = 3;
		for (var i = 0; i < l; i++) {
			_to.push(to[i]);
		}
		_to = _to.join(";");
		$("#mailReceiverValue").html(_to);
	} else {
		$("#mailReceiverValue").html("Disclosed receipients");
	}
	$("#mailSubjectValue").html(subject);
};

ViewMail.prototype.makeMsgBody = function(msgContent) {
	//console.log(msgContent);
	for (var i = 0; i < this.msg.inlineImages.length; i++) {
		var newLink = 'src="https://' + window.location.host + '/service/home/~/' + this.msg.inlineImages[i]["filename"] + '?authToken=co' + '&id=' + this.msg.getMsgId() + '&part=' + this.msg.inlineImages[i]["part"] + '"';
		var oldLink = 'dfsrc="' + 'cid:' + this.msg.inlineImages[i]["ci"] + '"';
		msgContent = msgContent.replace(new RegExp(oldLink, "g"), newLink);
	}

	$("#mailBody").html(msgContent);
	
};

