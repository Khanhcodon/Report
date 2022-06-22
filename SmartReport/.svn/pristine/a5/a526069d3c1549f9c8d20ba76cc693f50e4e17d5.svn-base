
(function () {

    function ParsePrivateFolderName(path) {
        path = path.toLowerCase();
        if (path === "inbox") {
            return "Thư cá nhân";
        }

        if (path === "drafts") {
            return "Thư nháp";
        }

        if (path === "junk") {
            return "Thư rác";
        }

        if (path === "sent") {
            return "Thư đã gửi";
        }

        if (path === "trash") {
            return "Thư đã xóa";
        }

        return path;
    }

    function getPrivateFolderIcon(path) {
        path = path.toLowerCase();
        if (path === "inbox") {
            return "inbox";
        }

        if (path === "drafts") {
            return "drafts";
        }

        if (path === "junk") {
            return "error";
        }

        if (path === "sent") {
            return "send";
        }

        if (path === "trash") {
            return "delete";
        }

        return "label";
    }

    function SOAPResponse(resp) {
        //public variables
        try {
            this.response = eval("(" + resp + ")");
            this.returnCode = 0;
            if (!Util.isUndefined(this.response)) {
                if (!Util.isUndefined(this.response.Header.Fault)) {
                    this.returnCode = this.getResponseCode(this.response.Header.Fault);
                }
            }
            if (!Util.isUndefined(this.response.Header.context.session)) {
                sessionId = this.response.Header.context.session.id;
                bmail.mailSessionId = sessionId;
                egov.mobile.setAppInfo();
            }
            this.type = "json";

        } catch (err) {
            try {
                this.response = this.xmlProcessor(resp);
                var code = this.response.find("Code").text();
                if (code.indexOf("service.AUTH_EXPIRED") != -1) {
                    Util.pageLogout("exprired");
                }
                this.type = "xml";
            } catch (err2) {
                // if (!isDebug) {
                console.log(err2);
                //}
            }
        }
    };

    function GetUser(userName) {
        var user = _.find(egov.setting.allUsers, function (u) {
            return u.username === userName;
        });

        return user;
    }

    SOAPResponse.prototype.getResponseName = function () {
        //get array of key of json object
        var keys = Object.keys(this.response.Body);
        return keys[0];
    };

    /**
     * For JSON response
     */
    SOAPResponse.prototype.jsonProcessor = function (resp) {
        return $.parseJSON(resp);
    };

    /**
     * For XML response
     */
    SOAPResponse.prototype.xmlProcessor = function (resp) {
        var xmlDoc = $.parseXML(resp);
        return $(xmlDoc);
    };

    SOAPResponse.prototype.getSessionId = function () {
        return this.sessionId = this.response.Header.context.session.id;
    };

    /**
     *@return 0 if ok
     */
    SOAPResponse.prototype.getResponseCode = function (resp) {
        if (resp.Detail.Error.Code == "mail.SEND_ABORTED_ADDRESS_FAILURE") {
            return 1;
        }
        return 0;
    };

    SOAPResponse.prototype.getResponseReturnCode = function () {
        return this.returnCode;
    };

    // -------------for GetInfoRequest------- //
    /**
     * Get user's folders
     */
    // -- New Code 08/02 -- isNewFolderList : cấu hình lúc nào dùng code folder cũ lúc nào dùng code folder mới -- //
    var isNewFolderList = true;
    if (isNewFolderList == false) {
        SOAPResponse.prototype.getFolderList = function () {
            var folderStructure = [];
            var responseJson = this.response;

            if (responseJson &&
                responseJson.Body &&
                responseJson.Body.GetInfoResponse &&
                responseJson.Body.GetInfoResponse.attrs &&
                responseJson.Body.GetInfoResponse.attrs._attrs &&
                responseJson.Body.GetInfoResponse.attrs._attrs.zimbraFolderStructure) {
                var response = responseJson.Body.GetInfoResponse.attrs._attrs.zimbraFolderStructure;
                var arrFolder = response.split('==========');
                var l = arrFolder.length;
                for (var i = 0; i < l - 1; i++) {
                    var folder = arrFolder[i];
                    var attrs = folder.split('::');
                    var tempArr = attrs[1].split('#');
                    var isOpen = "0";
                    if (tempArr.length == 5)
                        isOpen = tempArr[4];

                    var hasChildren = arrFolder.filter(function (obj) {
                        return obj != "" && obj.split('::')[1].split("#")[3] == tempArr[1];
                    }).length > 0;
                    //Để tạm
                    var totalUnread = 0;
                    var folderMailNode = new bmail.models.folder({
                        path: attrs[0], //path,
                        id: tempArr[1], //id,
                        visible: tempArr[0], //visible,
                        link: tempArr[2], //link,
                        parentid: tempArr[3], //parentId,
                        totalUnread: totalUnread,
                        hasChildren: hasChildren,
                        isOpen: isOpen
                    });

                    setBasicFolderInfo(tempArr[1], tempArr[3]);
                    folderStructure.push(folderMailNode);
                }
            }
            else {

            }
            return folderStructure;
        };
    } else {
        SOAPResponse.prototype.getFolderList = function () {
            var result = [];
            var responseJson = this.response;

            if (!responseJson ||
                !responseJson.Header ||
                !responseJson.Header.context.refresh ||
                !responseJson.Header.context.refresh.folder) {
                return result;
            }

            var rootFolder = responseJson.Header.context.refresh.folder[0];
            if (rootFolder == null || rootFolder.length === 0) {
                return result;
            }

            var privateFolders = rootFolder.folder;
            var publicFolders = rootFolder.link;

            privateFolders && _.each(privateFolders, function (privateFolder) {
                var newFolder = parseFolderItem(privateFolder, false, rootFolder);
                newFolder && result.push(newFolder);
            });

            publicFolders && _.each(publicFolders, function (pf) {
                var newFolder = parseFolderItem(pf, true, rootFolder);
                newFolder && result.push(newFolder);
            });

            console.log(result);
            return result;
        };
    }

    function parseFolderItem(bmailFolder, isPublic, parent) {
        if (bmailFolder == undefined) {
            return;
        }

        var visible = true;

        if (bmailFolder.view !== undefined && (bmailFolder.view != "message" || bmailFolder.name == "Chats")) {
            visible = false;
        }

        if (visible == false) {
            return;
        }

        var isChildFolder = parent.absFolderPath !== "/";
        var hasChildren = bmailFolder.folder && bmailFolder.folder.length > 0;

        var path = _.last(bmailFolder.absFolderPath.split("/")); // === 0 ? bmailFolder.absFolderPath.substring(1, bmailFolder.absFolderPath.length) : bmailFolder.absFolderPath;

        var folderName = _.last(((isPublic || isChildFolder) ? path : ParsePrivateFolderName(path)).split("/"));
        path = isChildFolder ? (parent.absFolderPath + "/" + path) : path;
        var result = {
            absFolderPath: path,
            path: path, //path,
            pathName: folderName,
            name: folderName,
            icon: getPrivateFolderIcon(path),
            id: isChildFolder ? bmailFolder.id : parseInt(bmailFolder.id),
            visible: visible,
            parentid: parent.id,
            totalUnread: bmailFolder.uuid,
            hasChildren: hasChildren,
            children: [],
            isPublicFolder: isPublic
        };

        if (result.hasChildren) {
            _.each(bmailFolder.folder, function (c) {
                var child = parseFolderItem(c, isPublic, result, true);

                child && result.children.push(child);
            });
        }

        return result;
    }

    SOAPResponse.prototype.getUserInfo = function () {
        var responseJson = this.response;
        var body = this.response.Body.GetInfoResponse;
        //Để tạm do dữ liệu Bmail trả về không ổn định
        //if (responseJson.Header.context && responseJson.Header.context && responseJson.Header.context.refresh) {
        //    readFolderInfo(responseJson.Header.context.refresh.folder[0]);
        //}
        var userInfo = new AttributesConstant();
        if (body.prefs
            && body.prefs._attrs
            && body.prefs._attrs.zimbraPrefFromDisplay) {
            userInfo.displayName = body.prefs._attrs.zimbraPrefFromDisplay;
        }
        userInfo.email = body.name;
        var signature = "";
        if (body.signatures
            && body.signatures.signature && body.signatures.signature[0]
            && body.signatures.signature[0].content[0]
            && body.signatures.signature[0].content[0]._content) {
            signature = body.signatures.signature[0].content[0]._content;
        }
        userInfo.signature = {
            useSignature: body.attrs._attrs.bmailPrefUseSignature == "TRUE",
            content: signature
        };
        return userInfo;
    };

    SOAPResponse.prototype.getContact = function () {
        var responseJson = this.response;
        var body = responseJson.Body.GetContactsResponse;

        var contacts = new Array();
        for (var i = 0; i < body.cn.length; i++) {
            contacts.push({
                id: body.cn[i].id,
                name: Util.isUndefined(body.cn[i]._attrs.firstName) ? "" : body.cn[i]._attrs.firstName,
                email: Util.isUndefined(body.cn[i]._attrs.email) ? "" : body.cn[i]._attrs.email
            });
        }
        return contacts;
    };

    SOAPResponse.prototype.getAutocompleteContact = function () {
        var body = this.response.Body.AutoCompleteResponse;
        var contacts = new Array();
        if (!Util.isUndefined(body)) {
            if (!Util.isUndefined(body.match)) {
                for (var i = 0; i < body.match.length; i++) {
                    var email = body.match[i].email.substring(body.match[i].email.indexOf("<", 0) + 1, body.match[i].email.indexOf(">", 0));
                    var name = body.match[i].email.replace("<", "(");
                    name = name.replace(">", ")");
                    contacts.push({
                        email: email,
                        ranking: body.match[i].ranking,
                        name: name
                    });
                }
            }
        }
        return contacts;
    };

    //-------------for SearchRequest-------------
    /**
     * Get data list from SearchResponse
     * Data can be: msg, ....
     */
    SOAPResponse.prototype.getDataList = function () {
        var body = this.response.Body.SearchResponse;
        var result = new Array();
        if (!Util.isUndefined(body.m)) {
            for (var i = 0; i < body.m.length; i++) {
                var mail = body.m[i];
                var trimId = mail.id.replace(/:/gi, "");

                var userSender = mail.e == null ? null : mail.e[0].a;
                var userName = userSender == null ? ""
                                    : userSender.split('@')[0];
                var user = GetUser(userName);
                var date = new Date(mail.d);
                var newMail = {
                    d: mail.d,
                    groupDate: date.timeInWord(),
                    date: date.format("d [thg] M"),
                    detailDate: date.format("HH:mm, dd [thg] MM, yyyy"),
                    location: mail.l,
                    conversationId: mail.cid,
                    id: mail.id,
                    trimId: trimId,
                    domId: "maillist_" + trimId,
                    avatar: user == null ? egov.setting.noavatar : user.avatar,
                    sender: {
                        fulladdress: userSender,
                        address: userName,
                        name: user == null ? userName : user.fullname,
                        fullname: user == null ? mail.a : user.fullname
                    },
                    hasAttach: mail.f == undefined ? false : mail.f.indexOf("a") != -1,
                    subject: Util.getSubject(mail.su),
                    subcontent: mail.fr,
                    unread: mail.f && mail.f == "u",
                    isSelected: false
                };

                result.push(newMail);
            }
        }
        return { mails: result, more: body.more };
    };

    SOAPResponse.prototype.getMsgData = function () {

        return this.response.Body.GetMsgResponse;
    };

    //--------------end SearchRequest--------------
    //--------for IM's requests----------------
    SOAPResponse.prototype.numberFriendsOnline = 0;
    /**
     * Get list of friends online
     */
    SOAPResponse.prototype.getFriendsOnline = function () {
        var body = this.response.Body.IMGetUserOnlineResponse;


        var friends = {};
        var yahoo = new Array();
        var facebook = new Array();
        var gtalk = new Array();
        var bmail = new Array();
        if (!Util.isUndefined(body.user)) {
            for (var i = 0; i < body.user.length; i++) {

                Util.updateFriendStatus(body.user[i].addr, "online");

                if (body.user[i].addr.indexOf("@yahoo", 0) != -1) {
                    yahoo.push({
                        "addr": body.user[i].addr,
                        "name": body.user[i].name
                    });
                } else if (body.user[i].addr.indexOf("@facebook", 0) != -1) {
                    facebook.push({
                        "addr": body.user[i].addr,
                        "name": body.user[i].name
                    });
                } else if (body.user[i].addr.indexOf("@gtalk", 0) != -1) {
                    gtalk.push({
                        "addr": body.user[i].addr,
                        "name": body.user[i].name
                    });
                } else {
                    bmail.push({
                        "addr": body.user[i].addr,
                        "name": body.user[i].name
                    });
                }
            }
            friends["yahoo"] = yahoo;
            friends["gtalk"] = gtalk;
            friends["facebook"] = facebook;
            friends["bmail"] = bmail;
            this.numberFriendsOnline = body.user.length;
        }
        return friends;

    };
    /**
     * If this full history request
     */
    SOAPResponse.prototype.isFullHistory = function () {
        if (Util.isUndefined(this.response.Body.IMGetMessageHistoryResponse.list)) {
            return false;
        }
        return true;
    };

    /**
     * return an array if is not a full history and json vice versa
     */
    SOAPResponse.prototype.getMsgHistories = function () {
        /*if (!this.isFullHistory()) {
            var msgs = new Array();
            if (!Util.isUndefined(this.response.Body.IMGetMessageHistoryResponse.chat)) {
                msgs = this.response.Body.IMGetMessageHistoryResponse.chat[0].message;
            }
            return msgs;
            //an array of message
        } else {
            return this._getMsgHistories(this.response.Body.IMGetMessageHistoryResponse.list[0]["chat"]);
        }*/
        var msgs = [];
        if (this.response.Body.IMGetMessageHistoryResponse.chat) {
            for (var i = 0 ; i < this.response.Body.IMGetMessageHistoryResponse.chat.length ; i++)
                msgs.push(this.response.Body.IMGetMessageHistoryResponse.chat[i].message);
        }
        return msgs;
    };
    SOAPResponse.prototype.getLastMessage = function () {
        var msgs = [];
        if (this.response.Body.IMGetMessageHistoryResponse.chat && this.response.Body.IMGetMessageHistoryResponse.chat[0] && this.response.Body.IMGetMessageHistoryResponse.chat[0].message) {
            for (var i = 0 ; i < this.response.Body.IMGetMessageHistoryResponse.chat[0].message.length ; i++)
                msgs.push(this.response.Body.IMGetMessageHistoryResponse.chat[0].message[i]);
        }
        return msgs;
    };
    SOAPResponse.prototype._getMsgHistories = function (data) {
        var msgs = {};
        for (var i = 0; i < data.length; i++) {
            //if (Util.isUndefined(msgs[data[i]["jid"]])) {
            var msg = data[i];
            if (msg)
                msgs[msg[0]["jid"]] = msg[0]["body"][0]._content;
            //}
        }
        return msgs;
    };
    //------------end IM's requests

    SOAPResponse.prototype.getRawData = function () {
        return this.response;
    };

    SOAPResponse.prototype.getRawHeaderData = function () {
        return this.response.Header;
    };

    window.SOAPResponse = SOAPResponse;
})();