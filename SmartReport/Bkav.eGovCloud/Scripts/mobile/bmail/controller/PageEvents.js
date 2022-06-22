autoCompleteResult = [];
function PageEvents() {
}

/**
 * When user clicks on a autocomplete element
 * 
 * @ObjEvent where fire the event
 * @param output
 *            where we need to display bubble.
 * @param inputField
 *            where we get the data
 */
PageEvents.ContactAutoCompleteClickEvent = function(ObjEvent, output,
		inputField) {
	$(ObjEvent).children('li').bind('click', function() {
		var email = $(this).attr('id');
		$(ObjEvent).children().remove();
		var text = $(inputField).val();
		var value = "";
		if (text.indexOf(";") != -1) {
			var lastIndex = text.lastIndexOf(";");
			if (lastIndex < text.length - 1) {
				value = text.substring(0, lastIndex + 1);
			}
		}
		value = value + email + ";";
		$(inputField).val(value);
		$(ObjEvent).html("");
	});
};

/**
 * Event is fired when user clicks on a bubble address;
 */
PageEvents.addressClickEvent = function(ObjEvent) {

	$(ObjEvent).children("#displayAddress").bind("click", function() {

		$(this).remove();
	});
	// this function only for demo, will use it in the future
	$(ObjEvent).children("#displayAddress").bind("taphold", function() {
		$(this).css("background", "green");
	});
};

/**
 * Event fired when user press keyboard in address field input
 * 
 * @param input
 *            address input
 * @param event
 *            bind event
 * @param autocomp
 *            autocomplete list
 * @param output
 *            output field
 */
PageEvents.AddressInputKeyEvent = function(input, event, autocomp, output) {
	$(input)
			.bind(
					event,
					function (event) {
						var sugList = $(autocomp);
						var text = $(this).val();
						// we dont want add when user press backspace. NOTICE: I
						// dont know keycode if we use WP or Android
						if (!Util.needToSendAutoComplete(event.keyCode, text,
								event.charCode)) {
							if (text.length > 0) {
								sugList.html("");
								// if user press semi-colon key, we create new
								// bubble
								var tmp = $(output).html();
								var addresses = new Array();
								if (event.charCode == ";"
										|| event.charCode == ",") {
									addresses.push($(this).val().substr(0,
											$(this).val().length - 1));
								}
								if (addresses.length != 0) {
									tmp += Util.displayBubbleAddress(addresses);
									$(output).html(tmp);
									PageEvents.addressClickEvent(output);
									// them clear To input field
									$(input).val("");
									return;
								}
								if (autoCompleteResult.length != 0) {
									var html1 = new Array();
									for ( var j = 0; j < autoCompleteResult.length; j++) {
										html1.push("<li id='");
										html1.push(autoCompleteResult[j].email);
										html1.push("'>");
										html1.push(autoCompleteResult[j].name);
										html1.push("</li>");
									}
									sugList.html(html1.join(""));
								} else {
									sugList.html("");
								}
								PageEvents.ContactAutoCompleteClickEvent(
										sugList, output, input);
							}
						} else {

							if (text.length > 0) {
								if (text.indexOf(";") != -1) {
									var lastIndex = text.lastIndexOf(";");
									if (lastIndex < text.length - 1) {
										var query = text
												.substring(lastIndex + 1);
										var json = {};
										var request = new SOAPRequest();
										request.initParams();
										request.makeJsonHeader(json);
										request.makeAutoCompleteJsonBody(json,
												query);
										request.sendJsonRequest(
												"AutoCompleteRequest", json,
												sugList, output, input);
									}
								} else if (text.indexOf(",") != -1) {
									var lastIndex = text.lastIndexOf(",");
									if (lastIndex < text.length - 1) {
										var query = text
												.substring(lastIndex + 1);
										var json = {};
										var request = new SOAPRequest();
										request.initParams();
										request.makeJsonHeader(json);
										request.makeAutoCompleteJsonBody(json,
												query);
										request.sendJsonRequest(
												"AutoCompleteRequest", json,
												sugList, output, input);
									}
								} else {
									var query = text;
									var json = {};
									var request = new SOAPRequest();
									request.initParams();
									request.makeJsonHeader(json);
									request.makeAutoCompleteJsonBody(json,
											query);
									request.sendJsonRequest(
											"AutoCompleteRequest", json,
											sugList, output, input);
								}
							} else {
								sugList.html("");
								//PageEvents.ContactAutoCompleteClickEvent(ObjEvent, output, autocomp);
							}
						}

					});
};
/**
 * pull up to load older message
 */
PageEvents.pullUpToLoadMoreMsgs = function() {
	if (more) {
		var request = new SOAPRequest();
		var req = new Array();
		request.makeHeader(req, "js");
		if (!checkSearch)
			request.makeSearchBody(req, "SearchRequest", "zimbraMail",
					mailIndex, folder.ACTIVE_FOLDER, 20);
		else
			request.makeSearchBody(req, "SearchRequest", "zimbraMail",
					mailIndex, queryStringSearch, 20, true);
		request.sendRequest("SearchRequest", req);
	}
};

PageEvents.pullDownToLoadNewMsgs = function() {
	// alert(activeFolder);
	// alert(mailIndex);
	var request = new SOAPRequest();
	var req = new Array();
	request.makeHeader(req, "js");
	var limit = mailIndex || 20;
	if (!checkSearch)
		request.makeSearchBody(req, "SearchRequest", "zimbraMail", 0,
				folder.ACTIVE_FOLDER, limit);
	else
		request.makeSearchBody(req, "SearchRequest", "zimbraMail", 0,
				queryStringSearch, 0, true);
	request.sendRequest("SearchRequest", req);
};

/**
 * load tin nhắn chat cũ khi chat
 */
PageEvents.pullDownToLoadOlderChat = function(event, data) {
	isLoadOlderChat = true;
	var request = new SOAPRequest();
	var req = new Array();
	request.makeHeader(req, "js");
	if (!messageIndex[currentFriend])
		messageIndex[currentFriend] = 0;
	request.makeIMGetMessageHistoryRequest(req, "IMGetMessageHistoryRequest",
			"zimbraIM", currentFriend, messageIndex[currentFriend], 4);
	request.sendRequest("IMGetMessageHistoryRequest", req);
	// data.iscrollview.refresh();
};
/**
 * send typing khi chat
 */
PageEvents.sendTyping = function(object) {
	var request = new SOAPRequest();
	var req = new Array();
	request.makeHeader(req, "js");
	request.makeIMSendTypingBody(req, "IMSendMessageRequest", "zimbraIM",
			currentFriend);
	request.sendRequest("IMSendTypingRequest", req, object);
};
/**
 * send chat
 * 
 * @param {Object}
 *            object
 */
PageEvents.sendChatMsg = function(object) {
	var newChat = $("#chatInput").html();
	if (newChat != "") {
		$("#chatInput").html("");
		var img = "";
		// then append new content with id is ownerChat
		if (oAvatar != "") {
			img = "https://" + window.location.host + "/mail/" + oAvatar;
		}

		newChat = reVertImg(newChat);
		var txt = removeTag(newChat);
		txt = txt.replace(/&nbsp;/gi, ' ');
		newChat = newChat.replace(/(<br>)*$/gi, '');
		newChat = newChat.replace(/(<div><br><\/div>)*$/gi, '');
		newChat = newChat.replace(/&/gi, "&amp;");
		newChat = newChat.replace(/</gi, "&lt;");
		newChat = newChat.replace(/>/gi, "&gt;");
		if (newChat.indexOf('&nbsp') != -1) {
			newChat = newChat.replaceAll("&nbsp;&nbsp;", ' &nbsp;');
			newChat = newChat.replaceAll("&nbsp;", '&amp;nbsp;');
		}

		var request = new SOAPRequest();
		var req = new Array();
		request.makeHeader(req, "js");
		request.makeIMSendMessageBody(req, "IMSendMessageRequest", "zimbraIM",
				currentFriend, newChat);
		request.sendRequest("IMSendMessageRequest", req, object);
		$("#chatInput").focus();
	}
};

PageEvents.pageEvents = function() {
	$("#backButton").bind("click", function() {
		// refresh friend list if we have new friend online while we're not
		// focus in friedn list
		// $("#bmailChat").listview("refresh");
	});
	$("#mailBack").bind("click", function() {
		// bind event for list mail
		// we get active folder, if it's exist we load this folder's content
		// vice versa, we load inbox's data
		if (folder.ACTIVE_FOLDER == null || folder.ACTIVE_FOLDER == "") {
			// load inbox
			getFolderMailList(0, folder.ACTIVE_FOLDER, 20);
		} else {
			// load active folder
			folder.ACTIVE_FOLDER = "inbox";
			getFolderMailList(0, "inbox", 20);
		}
	});
	$("#chatBack").bind("click", function() {
		$("#bmailChat:visible").listview("refresh");
		$("#bmailChat").listview("refresh");
	});
	$("#chatGo").bind("click", function() {
		$("#bmailChat:visible").listview("refresh");
		$("#bmailChat").listview("refresh");
	});
};
// Bind scroll
PageEvents.bindScroll = function(selector, fnUp, fnDown) {
	var isBottom = false;
	var pageY1 = 0;
	var pageY2 = 0;
	var top = 0;
	var heightScroll;
	var totalHeight;
	var startDate;
	var isShow = false;
	var topzero = function() {
		$(selector).animate({
			top : "0"
		});
		$(selector).find(".iconLoadding").css("background-image",
				"url('/mail/cssmobile/images/imagesforall/loading.gif')");
		$(selector).find(".textcontent").html("Đang lấy thêm thư...");
		$(selector).animate({
			top : "0"
		}, 1500, function() {
			// console.log('do--');
		});
	};

	var tophide = function() {
		$(selector).animate({
			top : "0"
		}, 200, function() {
			$(selector).find('.headscroll').remove();
			isShow = false;
		});
	};
	// Bắt sự kiện chạm vào màn hình
	$(selector).bind("touchstart", function(event) {
		pageY1 = event.originalEvent.touches[0].pageY;
		startDate = new Date().getTime();
	});

	// Sự kiện chạm rồi di chuyển trên màn hình
	$(selector)
			.bind(
					"touchmove",
					function(event) {
						var scrollIsExist = $(selector).find('.headscroll').length;
						pageY2 = event.originalEvent.touches[0].pageY;
						var heightMove = pageY2 - pageY1;
						// console.log("heightMove"+ heightMove);
						if (!scrollIsExist && heightMove >= 40
								&& heightMove < 250) {
							$(selector).css("top", (81) + "px");
							if ($(selector).find('.iconLoadding').length == 0) {
								// console.log("loading");
								$(selector)
										.prepend(
												'<div class="headscroll"><span class="iconLoadding" ></span><span class="textcontent">Kéo thả để cập nhật...</span></div>');
							}
						}
						top = parseInt($(selector).css("top"));
						// console.log("top " + top);
						if (top > 80) {
							$(selector).find(".iconLoadding").css("transform",
									"rotate(180deg)");
							$(selector).find(".textcontent").html(
									"Thả để cập nhật..");
						}
					});

	// Sự kiện kết thúc sự kiện chạm
	$(selector).bind('touchend', function(event) {
		// console.log("touchend");
		if (top > 80) {
			topzero();
			fnDown();
			pageY1 = 0;
			pageY2 = 0;
			top = 0;
		}
		tophide();
	});
};
PageEvents.unbindScroll = function(selector) {
	$(selector).unbind('touchend');
	$(selector).unbind('touchstart');
	$(selector).unbind('touchmove');
	// console.log('unbindtop');
	// console.log($("#Mainpage").find(".headscroll").length);
	if ($("#Mainpage").find(".headscroll").length == 1) {
		$("#Mainpage").animate({
			top : "0"
		}, 200, function() {
			$("#Mainpage").find('.headscroll').remove();
			isShow = false;
		});
		$("#Mainpage").find("#mailListWrapper").css("top", "10px");
	}
};
PageEvents.updatecontent = function(selector, fnUp, fnDown) {
	// PageEvents.bindScroll(selector, fnUp, fnDown);
	// console.log('bind');
	// Sự kiện cuộn trang
	var kt = false;
	$(window)
			.scroll(
					function() {
						if ($.mobile.activePage.attr('id') != "Mainpage")
							return;
						// console.log($(window).scrollTop());
						// Neu o dau trang thi bind su kien lay thu moi
						var heightMailBottom = $(document).height()
								- $(window).height() - $(window).scrollTop();
						if (heightMailBottom > 120)
							kt = false;
						if (heightMailBottom == $(document).height()
								- $(window).height()) {
							PageEvents.bindScroll(selector, fnUp, fnDown);
						} else if (heightMailBottom < 120) {// Neu o cuoi trang
															// thi lay them mail
							// console.log(heightMailBottom);
							if ($(document).height() > $(window).height()) {
								var pageY1 = 0;
								var pageY2 = 0;
								var heightMove = 0;
								var scrolldownid = $(selector).find(
										'#loadding_Mail');
								// Nếu cuộn tới cuối trang thì insert thông báo
								// và chạy hàm cập nhật
								if (heightMailBottom < 120) {
									if (!scrolldownid.length && kt == false) {
										$('#mailListWrapper')
												.append(
														'<div id="loadding_Mail">Đang lấy thêm thư...</div>');
										$(window).scrollTop(
												$(document).height()
														- $(window).height());
										setTimeout(function() {
											fnUp();
											$('#mailListWrapper').find(
													'#loadding_Mail').remove();
											kt = true;
										}, 2000);
									}
								}
								if ($(window).scrollTop == $(document).height()
										- $(window).height()) {
									$(selector)
											.bind(
													"touchstart",
													function(event) {
														pageY1 = event.originalEvent.touches[0].pageY;
													});

									// Sự kiện chạm rồi di chuyển trên màn hình
									$(selector)
											.bind(
													"touchmove",
													function(event) {
														var scrollIsExist = $(
																selector).find(
																'.headscroll').length;
														pageY2 = event.originalEvent.touches[0].pageY;
														heightMove = pageY2
																- pageY1;
													});
									$(selector)
											.bind(
													'touchend',
													function(event) {
														$(selector).unbind(
																'touchstart');
														$(selector).unbind(
																'touchmove');
														$(selector).unbind(
																'touchend');
														if (!scrolldownid.length
																&& kt == false) {
															kt = true;
															$(
																	'#mailListWrapper')
																	.append(
																			'<div id="loadding_Mail">Đang lấy thêm thư...</div>');
															$(window)
																	.scrollTop(
																			$(
																					document)
																					.height()
																					- $(
																							window)
																							.height());
														}
														setTimeout(
																function() {
																	fnUp();
																	$(
																			'#mailListWrapper')
																			.find(
																					'#loadding_Mail')
																			.remove();
																}, 2000);

													});
								}
							}
						} else {
							PageEvents.unbindScroll(selector);
						}
					});
	kt = false;
};

PageEvents.updatecontentChat = function(selector, fnUp) {
	var activePage = $.mobile.activePage.attr("id");
	if (activePage == 'chatForm') {
		var isBottom = false;
		var selector = selector;
		var pageY1 = 0;
		var pageY2 = 0;
		var top = 0;
		var heightScroll;
		var totalHeight;
		var isShow = false;
		var topzero = function() {
			$(selector).animate({
				top : "0"
			});
			$(selector).find(".iconLoadding").css("background-image",
					"url('/mail/cssmobile/images/imagesforall/loading.gif')");
			$(selector).find(".textcontent").html("Đang lấy thêm tin nhắn");
			$(selector).animate({
				top : "0"
			}, 1000, function() {
				PageEvents.pullDownToLoadOlderChat();
				console.log('ok');
			});
		};
		var tophide = function() {
			$(selector).animate({
				top : 0
			}, 10, function() {
				$(selector).find('.headscroll').remove();
				isShow = false;
			});
		};

		// Bắt sự kiện chạm vào màn hình
		$(selector).bind("touchstart  ", function(event) {
			pageY1 = event.originalEvent.touches[0].pageY;
		});

		// Sự kiện chạm rồi di chuyển trên màn hình
		$(selector)
				.bind(
						"touchmove",
						function(event) {
							var scrollIsExist = $(selector).find('.headscroll').length;
							pageY2 = event.originalEvent.touches[0].pageY;
							var heightMove = pageY2 - pageY1;
							top = parseInt($(selector).css("top"), "10");
							var top2 = pageY2 - pageY1;
							if ($(window).scrollTop() == 0 && heightMove >= 40
									&& heightMove < 250 && !isShow) {
								$(selector).css("top", top2);
							}
							if (!scrollIsExist && top > 44) {
								$(selector)
										.prepend(
												'<div class="headscroll"><span class="iconLoadding" ></span><span class="textcontent">Kéo thả để lấy thêm tin nhắn...</span></div>');
								heightScroll = $(selector).find(".headscroll")
										.height();
							}
							if (top > 50) {
								$(selector).find(".iconLoadding").css(
										"transform", "rotate(180deg)");
								$(selector).find(".textcontent").html(
										"Thả để lấy tin nhắn..");
							}
						});
		// Sự kiện kết thúc sự kiện chạm
		$(selector).bind('touchend', function(event) {
			if (top > 0) {
				topzero();
				isShow = true;
				tophide();
			}
		});
	}
};
