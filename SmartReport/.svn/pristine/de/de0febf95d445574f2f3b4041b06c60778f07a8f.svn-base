var sessionId = 0,
autoCompleteResult,
caret,
currentMsgIds = [],
idMailDraft = null,
idnt = null,
irt = null,
midPart = [],
fileNameAtt = [],
detailMailLen = 0,
firstLoadMailNumber = 50,
maxUnreadMailNotify = 99,
more,
currentMailListCount = 0,
currentIndex,
tabMailDetailId,
hasDeletedMail = false,
isLoadMore = false,
isSearch = false,
isrefresh = false,
enableScrollView = false,
bmailServer = Util.readCookie("serverAddress"),
chatServer = Util.readCookie("chatAddress"),
homeService = "",
cookie_user = username.toLowerCase();

bmail.models = {};
bmail.models.folderInfo = [];

function getBasicConnectionData(callback) {
    $.ajax({
        url: '/Home/GetConnectionSettings'
    }).success(function (setting) {

        //Set lại các link liên kết với bmail
        if (setting.BmailLink != null && setting.BmailLink != "") {
            // var mailUrl = new URL(setting.BmailLink);
            bmailServer = getUrlOrigin(setting.BmailLink);// mailUrl.origin;
            if (bmailServer) {
                Util.createCookie("serverAddress", bmailServer, 7);
                homeService = bmailServer + "/service/home/";
            }
        }
        if (setting.ChatLink != null && setting.ChatLink != "") {
            // var chatUrl = new URL(setting.ChatLink);
            chatServer = getUrlOrigin(setting.ChatLink); //chatUrl.origin;
            if (chatServer) {
                Util.createCookie("chatAddress", chatServer, 7);
            }

            // $("#chatFrame").append("<iframe class='full-wrap no-border' id='chat' src='" + chatServer + "' />");
        }

    }).error(function () {
        console.warn("unknown mail address");
    })
    .complete(function () {
        egov.callback(callback);
    });
}

var folderUtil = function () {
};

folderUtil.prototype.getFolder = function (id) {
    var result = bmail.models.folderInfo.find(function (folder) {
        return folder.id == id;
    });

    return result;
},

folderUtil.prototype.isSendToMyFolder = function (id) {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    try {
        var folder = this.getFolder(id);
        //Gửi đến địa chỉ linh tinh thì không nhận thông báo
        return folder != undefined;
    } catch (e) {
        console.log(e);
    }
    return false;
}

function readAuthenCookie() {
    return Util.readCookie("bkavAuthen");
}

function getFolderInfo(locationId) {
    /// <summary>
    /// Trả về thông tin của thư mục
    /// </summary>
    /// <param name="locationId">Id thư mục</param>

    var folderInfo = bmail.models.folderInfo.filter(function (that) {
        return that.id === locationId;
    });

    if (folderInfo && folderInfo.length > 0) {
        return folderInfo[0];
    }
}

function setBasicFolderInfo(folderId, parentId) {
    var exist = bmail.models.folderInfo.find(function (v) {
        return v.id == folderId;
    });

    if (!exist) {
        var idPath = "";
        var isPublicFolder = false;
        var parent = bmail.models.folderInfo.find(function (v) {
            return v.id == parentId
        });

        if (parent) {
            idPath = parent.idPath + "/" + folderId
            isPublicFolder = parent.isPublicFolder;
        } else {
            idPath = folderId;
            isPublicFolder = folderId > 6;
        }

        exist = {
            id: folderId,
            isPublicFolder: isPublicFolder,
            idPath: idPath,
            unread: 0,
            perm: readPermission("rwip")
        };

        bmail.models.folderInfo.push(exist);
    }

    return exist;
}

function readFolderInfo(folderHeader) {
    readFolder(folderHeader.folder);
    readFolder(folderHeader.link);
    if (bmail.views.folderlist) {
        bmail.views.folderlist.rebindTotalUnread();
    }
}

function readFolder(folderPer) {
    if (folderPer) {
        $.each(folderPer, function () {
            var folderInfo = setBasicFolderInfo(this.id, this.l);

            if (typeof this.perm === "undefined") {
                this.perm = "rwip";
            }

            if (typeof this.u === "undefined") {
                this.u = 0;
            }

            folderInfo.perm = readPermission(this.perm);
            folderInfo.unread = this.u;
            folderInfo.name = this.name;

            if (typeof this.folder === "object") {
                readFolder(this.folder);
            }
            else if (typeof this.link === "object") {
                readFolder(this.link);
            }
        });
    }
}

function readPermission(perm) {
    /// <summary>
    /// Đọc quyền trên thư mục
    /// </summary>
    /// <param name="perm"></param>
    return {
        read: perm.match("r") != null,
        write: perm.match("w") != null,
        insert: perm.match("i") != null,
        post: perm.match("p") != null
    };
}

function getUrlOrigin(url) {
    const a = $('<a />').attr('href', url)[0];
    var origin = a.protocol + '//' + a.hostname;
    if (a.port.length > 0) {
        origin += ":" + a.port;
    }

    return origin;
}

bmail.folderUtil = new folderUtil();
