// var folderList;
// var activeFolder;
// var currentFolderId = '2';
// var changeFolder = false;

function folder() {
	this.folderList = null;
	this.activeFolder = null;
	this.currentFolderId = '2';
	this.changeFolder = false;

	// cau truc folder
	this.folderId = null;
	this.folderPath = null;
	this.folderName = null;
	this.folderParentId = null;
	this.valueLi = 0;
};

// Kiem tra thu muc hien tai co phai parent ko??
folder.prototype.checkIsParent = function(folderid) {
	for (var i = 0; i < folderList.length; i++) {
		var pId = folderList[i].parentId;
		if (pId == folderid) {
			return true;
		}
	}
	return false;
};
folder.prototype.checkRootParent = function(folderId) {
	for (var i = 0; i < folderList.length; i++) {
		var _folderId = parseInt(folderList[i].id);
		var pId = folderList[i].parentId;
		if (folderId == _folderId)
			if (pId == 1)
				return true;
	}
	return false;
};
folder.prototype.checkNthParent = function(idfolder, dem, rootId) {
	for (var i = 0; i < folderList.length; i++) {
		var pId = parseInt(folderList[i].parentId);
		var fName = folderList[i].name;
		var fId = parseInt(folderList[i].id);
		if (idfolder == fId) {
			for (var j = 0; j < folderList.length; j++) {
				var _folderId = parseInt(folderList[j].id);

				if (_folderId == pId && _folderId != fId && pId != 1) {
					dem++;
					//console.log(dem);
					this.checkNthParent(pId, dem, tmp);
				}
			}
		}
	}
	if (idfolder == rootId) {
		//console.log(idfolder,dem);
		return dem;
	}
};
// Tao ma html cho 1 folder
var valueLi = 0;
folder.prototype.createLi = function(idfolder, folderPath, folderName, cssClass, isSub, addclassChid, isParent, idParent) {
	var li = "";
	var check = 0;
	var tmp = idfolder;

	if (isSub == false)
		li += "<ul class='parentFolder' value='" + valueLi + "'>";
	else
		li += "<ul class='childentFolder childentFolder" + this.checkNthParent(idfolder, 0, idfolder) + "' value='" + valueLi + "'>";
	if (isSub) {
		li += '<li  name="' + folderPath + '" id="' + folderName + '" class="' + addclassChid + '">';
		valueLi++;
	} else {
		li += '<li name="' + folderPath + '" id="' + folderName + '" class="rootFolder">';
		valueLi++;
	}
	if (isParent == true) {
		li += '<span class="collapsablePlus" value="' + (valueLi + 50) + '"></span>';
		li += '<span class="collapsableMinus" value="' + -(valueLi + 50) + '"></span>';
	}
	//li += '<ul class="activeFolder">';
	li += '<span class="' + cssClass + '"></span>';
	li += '<span class="wpf-left">';
	li += '<a  class="folderName">';
	li += folderName;
	li += '</a>';
	li += '</span>';
	//li += '</ul>';
	li += '<div class="wp-space">';
	li += '<span class="li-space-left">';
	li += '</span>';
	li += '<span class="li-space-right">';
	li += '</span></div>';
	// Tam thoi an chuc nan xoa sua
	/*
	 if (isSub) {
	 li += "<span class='wpf-right'><span class='rename-fd'>";
	 li += "<span class='rename-icon'  name='" + idfolder + "'  folderName='" + folderName + "'></span>";
	 li += "<span class='rename-text'>Sửa</span></span><span class='folder-space'></span><span class='delete-fd'></span>";
	 li += "<span class='delete-icon'  name='" + idfolder + "'  folderName='" + folderName + "'></span>";
	 li += "<span class='delete-text'>Xóa</span></span>";
	 }
	 */
	li += "<div class='wp-space'><span class='li-space-left'></span><span class='li-space-right'></span></div></li>";
	return li;
};
// Tao 1 li moi li tuong ung voi 1 thu muc
folder.prototype.createNodeLi = function(folderId, cout, tmp, html) {
	for (var k = 0; k < folderList.length; k++) {
		var _parentId = folderList[k].parentId;
		var _folderName = folderList[k].name;
		var _folderId = parseInt(folderList[k].id);
		var _folderPath = folderList[k].folderPath;
		if (folderId == _parentId && folderId != _folderId) {
			//html.push('<ul class="parent' + k + '">');
			html.push(this.createLi(_folderId, _folderPath, _folderName, 'fli-icon', true, 'addclass' + cout + '', folder.prototype.checkIsParent(_folderId), _parentId));
			if (tmp != _parentId && tmp != undefined)
				cout++;
			tmp = _parentId;
			folder.prototype.createNodeLi(_folderId, cout, _parentId, html);
			html.push("</ul>");
		}
	}
};

folder.prototype.loadFolderList = function() {
	var html = new Array();

	//if(folderList.length > 0)
	for (var i = 0; i < folderList.length; i++) {
		var folderId = parseInt(folderList[i].id);
		var parentId = folderList[i].parentId;
		var folderName = folderList[i].name;
		var folderPath = folderList[i].folderPath;
		var isParent = false;
		//createNodeLi(folderId);

		if (folderId != 14) {
			if (folderId == 2) {
				html.push(folder.prototype.createLi(folderId, folderPath, folderName, 'fli-icon-inbox', false, "", folder.prototype.checkIsParent(folderId), parentId));
				var cout = 1;
				for (var i = 0; i < folderList.length; i++) {
					var _parentId = folderList[i].parentId;
					var _folderName = folderList[i].name;
					var _folderId = parseInt(folderList[i].id);
					var _folderPath = folderList[i].folderPath;
					if (folderId == _parentId && folderId != _folderId) {
						//html.push('<ul class="parent' + k + '">');
						html.push(this.createLi(_folderId, _folderPath, _folderName, 'fli-icon', true, 'addclass' + cout + '', folder.prototype.checkIsParent(_folderId), _parentId));
						if (tmp != _parentId && tmp != undefined)
							cout++;
						tmp = _parentId;
						folder.prototype.createNodeLi(_folderId, cout, _parentId, html);
						html.push("</ul>");
					}
				}
				//this.createNodeLi(folderId, 1, undefined, html);
				html.push("</ul>");
			} else if (folderId == 3) {
				html.push(folder.prototype.createLi(folderId, folderPath, folderName, 'fli-icon-trash', false, "", folder.prototype.checkIsParent(folderId), parentId));

				for (var f = 0; f < folderList.length; f++) {
					var pId = folderList[f].parentId;
					var fName = folderList[f].name;
					var fId = parseInt(folderList[f].id);
					var fPath = folderList[f].folderPath;
					if (pId == 3) {
						html.push(folder.prototype.createLi(fId, fPath, fName, 'fli-icon', true, "", folder.prototype.checkIsParent(fId), pId));
						folder.prototype.createNodeLi(fId, 1, undefined, html);
						html.push("</ul>");
					}
				}
				html.push("</ul>");
			} else if (3 < folderId && folderId <= 6) {
				html.push(folder.prototype.createLi(folderId, folderPath, folderName, 'fli-icon', false, "", folder.prototype.checkIsParent(folderId), parentId));
				folder.prototype.createNodeLi(folderId, 1, parentId, html);
				html.push("</ul>");
			} else if (folderId < 2 || folderId > 6) {
				if (folder.prototype.checkRootParent(folderId) == true) {
					html.push(folder.prototype.createLi(folderId, folderPath, folderName, 'fli-icon', false, "", folder.prototype.checkIsParent(folderId), parentId));
					folder.prototype.createNodeLi(folderId, 1, undefined, html);
					html.push("</ul>");
				}
			}
			//} while(isParent);

			//chuyển folder khi click vào 1 folder trong danh sách folder
			$("#folderlist").append(html.join(""));
			//$("#folderlist:visible").listview("refresh");
			html.length = 0;

		}

	}
	$("#FolderPage").on("click", ".wpf-left", function() {
		//$("#folderlist").children('li').live('click', function() {
		//open new folder
		//alert($(this).parent().attr('id'));
		if (!$(this).parent().attr('id')) {
		} else {
			if ($("#Mainpage").find(".bmailSettingDiv").length > 0) {
				$("#Mainpage").find(".bmailSettingDiv").remove();
				$("#Mainpage").find(".mask").remove();
				$("#bmailSetting").find(".ui-icon-bmail-setting").css("background-image", 'url("/mail/jsmobile/emoticon/Menu.png")');
			}
			var id = "#" + $(this).parent().attr('id');
			var a = $(this).parent().attr('id');
			//alert(id);
			activeFolder = $(this).parent().attr("name");
			//alert(activeFolder);
			//update active folder
			//currentFolderId =
			mailIndex = 0;
			//restore mail index count
			//$("#logoHeader").html(logoBackLable('#Mainpage', $(this).attr('id')));
			$("#ulHeader").removeAttr('class');
			var href = "";
			$("#logoHeader").html("<a href='#Mainpage'   class='back'   data-transition='slide' data-direction: 'reverse'><span class='logo'></span><span class='pageLabelHeaderB'> " + a + "</span></a>");
			$("#searchDiv").html(searchDiv);
			$("a[href='#Mainpage']").bind("click", function() {
				activeFolder = "inbox";
				$("#ulHeader").removeAttr('class');
				$("#logoHeader").html(logoLable('Inbox'));
				folder.prototype.getFolderMailList(0, activeFolder, 20);
			});

			folder.prototype.getFolderMailList(0, activeFolder, 20);
			if (activeFolder == "inbox")
				$.mobile.changePage("#Mainpage");
		}
	});
};

/**
 * Get list mail in inbox folder
 * @param offset where to begin in list mail
 * @param query
 * @param limit how many msg we need to get
 * @return true if we can continue get more mail and vice versa
 */
folder.prototype.getFolderMailList = function(offset, query, limit, isSearch) {
	//open new folder need to update list mail
	if (offset == 0) {
		$("#maillist").children().remove('li');
		$("#maillist").append("<li data-role=\"list-divider\">" + "</li>");
	}
	if (activeFolder != "inbox")
		changeFolder = true;

	var request = new SOAPRequest();
	var req = new Array();
	request.makeHeader(req, "js");
	request.makeSearchBody(req, "SearchRequest", "zimbraMail", offset, query, limit, isSearch);
	request.sendRequest("SearchRequest", req);
};
