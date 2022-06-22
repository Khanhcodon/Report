(function () {

    //#region Private Method

    function _isFunc(functionName) {
        return typeof functionName === "function";
    }

    //#endregion


    /**
     * Create SOAPRequest object and init some params
     * - Step 1: new Object
     * - Step 2: makeHeader
     * - Step 3: makeBody
     * - Step 4: send
     * - And wait for callback function
     */

    //#region Contructor

    function SOAPRequest() {
        this.request = null;
        if (window.XMLHttpRequest) {
            this.request = new XMLHttpRequest();
        } else {
            try {
                this.request = new ActiveXObject("MSXML2.XMLHTTP3.0");
            } catch (ex) {
                return null;
            }
        }

        this.needAllReload = false;
        this._initParams();
    }

    SOAPRequest.prototype._initParams = function () {
        var bmailServer = new URL(egov.connections.BmailLink).origin;
        if (bmailServer) {
            this.vituralHost = bmailServer.replace("http://", "").replace("https://", "");
            var arr = bmailServer.split('.');
            if (arr.length > 2 && arr[0] == "mail") {
                var domain = '';
                var l = arr.length;
                for (var i = 1; i < l - 1; i++)
                    domain += arr[i] + '.';
                domain += arr[l - 1];
            }

            // -- New Code --  // 
            this.username = egov.usernameEmailDomain;
            this.url = bmailServer + "/service/soap";
        }
        else {
            console.log("bmailServer is undefined");
        }
    };

    //#endregion

    //#region Send Request And receive Response

    SOAPRequest.prototype.sendRequest = function (reqName, req, callback) {
        var that = this;

        // Khi có phản rồi
        this.request.onreadystatechange = function () {
            try {
                var isDone = that.request.readyState === 4; // 1 - UNSENT; 2 - OPENED; 3 - LOADING; 4 - DONE
                var isLoaded = that.request.status == 200; // UNSENT 0 * OPENED 0 * LOADING 200 * DONE 200
                var isUnsent = that.request.status === 0;

                if (reqName == "NoOpRequest") { // Request check mail mới                    
                    if (isDone) {
                        if (isUnsent) {
                            //Gửi lại request đầy đủ mà vẫn lỗi thì không request nữa
                            if (typeof callback == "function") {
                                Util.sendPullRequest(that);
                            }
                            else {
                            }
                        }
                        else if (isLoaded) {
                            SOAPRequest.readyData(this, parent, this.output, this.input, SOAPRequest.needAllReload, callback);
                        }
                    }
                }
                else if (isDone) {
                    if (isLoaded) {
                        SOAPRequest.readyData(this, parent, this.output, this.input, SOAPRequest.needAllReload, callback);
                    }
                }
            } catch (e) {
                console.log(e);
            }
        };

        // Khi có lỗi
        this.request.onerror = function () {
            $.cookie("serverAddress", "", { expires: -1, secure: true });
        };

        // Thiết lập request
        this.request.open("POST", this.url, true, reqName);
        this.request.timeout = 0.5 * 60 * 1000; // 30s
        this.request.ontimeout = function (e) {
            console.log("TimeOut Expired!");
        }

        try {
            // Gửi request lên server mail
            this.request.send(req.join(""));
        } catch (e) {
            console.log(e);
        }
    };

    SOAPRequest.readyData = function (request, parent, output, input, needAllReload, callback) {
        var isDone = request.readyState === 4;
        var isLoaded = request.status == 200;
        var isServerError = request.status == 500;

        if (isServerError) {
            response = new SOAPResponse(request.responseText);
            if (isDone && response.response.Body) {
                callback(response.response.Body.Fault.Reason.Text, "error");
            }

            return;
        }

        if (!isDone || !isLoaded) {
            return;
        }

        var response = new SOAPResponse(request.responseText);

        var respName = response.getResponseName();
        switch (respName) {
            case "AuthResponse":
                // Lưu cookie đăng nhập
                var newBkavAuthenCookie = response.response.Body.AuthResponse.authToken[0]._content;
                // Util.getCookie(response.response.Body);
                bmail.bmailNewAuthenCookie = newBkavAuthenCookie;
                bmail.authenCookie = newBkavAuthenCookie;
                egov.createCookie("bkavAuthen", newBkavAuthenCookie);
                egov.createCookie("newBkavAuthen", newBkavAuthenCookie);
                callback(callback);
                break;
            case "GetMsgResponse":
                // Lấy nội dung thư
                callback(response.getMsgData());
                break;
            case "GetInfoResponse":
                // Lấy danh sách thư mục và thông tin người dùng
                callback(response.getFolderList(), response.getUserInfo());
                break;
            case "SearchResponse":
                // Lấy danh sách mail lần đầu
                callback(response.getDataList());
                break;
            case "SendMsgResponse":
                // Lấy thư đầu tiên
                callback(response.getResponseReturnCode());
                break;
            case "NoOpResponse":
                // Kiểm tra thư mới
                if (response.response.Header && response.response.Header.context) {
                    var data = response.response.Header.context;
                    if (data.refresh && data.refresh.folder) {
                        //Request lấy thông tin folder, ...
                        readFolderInfo(data.refresh.folder[0]);
                    }
                    else
                        if (data.notify && data.notify[0] && data.notify[0].created) {
                            //Có notify
                            if (egov.mobile.notification) {
                                var info = data.notify[0].created;
                                egov.mobile.notification.notifyMail(info.m);
                            }
                        }
                }
                //console.log(response.response);
                egov.callback(callback);
                bmail.makeRequest.noOption();
                break;
            default:
                callback(response.returnCode);
                break;
        }
    };

    //#endregion

    //#region Authenticate

    SOAPRequest.prototype.preAuthen = function (req) {
        req.push("<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'>");
        req.push("<soap:Header>");
        req.push("<context xmlns='urn:zimbra'><session /><notify />");
        req.push("<format type='js'/>");
        req.push("</context>");
        req.push("</soap:Header>");
        req.push("<soap:Body>");
        req.push("<AuthRequest xmlns='urn:zimbraAccount'>");
        req.push("<virtualHost>");
        req.push(this.vituralHost);
        req.push("</virtualHost>");
        req.push("<bkavAuthen>");
        req.push(bmail.authenCookie);
        req.push("</bkavAuthen>");
        req.push("<type>simple</type>");
        req.push("</AuthRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    //#endregion

    SOAPRequest.prototype.makeHeader = function (req, reqType, isGetUserInfo) {
        req.push("<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'>");
        req.push("<soap:Header>");
        req.push("<context xmlns='urn:zimbra'>");
        if (sessionId != 0 && !isGetUserInfo) {
            req.push("<session id='");
            req.push(sessionId);
            req.push("'>");
            req.push(sessionId);
            req.push("</session>");
        } else {
            req.push("<session/>");
        }
        req.push("<notify/>");
        req.push("<format type='");
        req.push(reqType);
        req.push("'/>");
        req.push("<account by='name'>");
        req.push(this.username);
        req.push("</account>");

        if (bmail.authenCookie == null) {
            Util.pageLogout();
        }
        req.push("<authToken>");
        req.push(bmail.authenCookie);
        req.push("</authToken>");
        req.push("<virtualHost>");
        req.push(this.vituralHost);
        req.push("</virtualHost>");
        req.push("</context>");
        req.push("</soap:Header>");
    };

    SOAPRequest.prototype.makeSearchBody = function (req, reqName, reqNamespace, offset, query, limit, _isSearch, isSentBox) {
        isSearch = false;
        if (_isSearch)
            isSearch = _isSearch;
        if (offset == 0) {
            SOAPRequest.needAllReload = true;
        } else {
            SOAPRequest.needAllReload = false;
        }
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'");

        //add attributes
        req.push(" sortBy='dateDesc' locale='en_US' ");
        req.push("offset='");
        req.push(offset);
        req.push("' limit='");
        req.push(limit);
        if (isSentBox) {
            req.push("' recip='1");
        }
        req.push("' types='message'");

        if (!isSearch) {
            req.push(">");
            req.push("<query>in:\"");
            req.push(query);
            req.push("\"</query>");
        } else {
            req.push("  query='" + query + "'");
            req.push(">");
        }
        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeGetInfoBody = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'");

        //add attributes
        req.push(">");
        req.push("<account by='name'>");
        req.push(this.username);
        req.push("</account>");
        req.push("<authToken>");
        req.push(bmail.authenCookie);
        req.push("</authToken>");
        //req.push("<type>simple</type>");
        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeGetMsgBody = function (req, reqName, reqNamespace, msgId) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<m id='");
        req.push(msgId);
        req.push("'");
        req.push(" html='1' read='1' />");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makePrivateRequestBody = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        //req.push("<IMGetPrivateDataRequest xmlns='urn:zimbraIM' />");
        req.push("</IMGetPrivateDataRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeSetPrivateRequestBody = function (req, reqName, reqNamespace, privateData) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");
        req.push("<privateData>");
        req.push(privateData);
        req.push("</privateData>");
        req.push("</IMSetPrivateDataRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeFolderStructureBody = function (req, folderStructure, reqNamespace) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderStructure">List Folder: <folder path='path' id='id' visible='visible' link='link' parentid='parentid'></folder></param>
        /// <param name="reqNamespace"></param>

        req.push("<soap:Body>");
        req.push("<FolderStructureChangeRequest xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");
        req.push("<account by='name'>");
        req.push(this.username);
        req.push("</account><authtoken>");
        req.push(bmail.authenCookie);
        req.push("</authtoken>");
        req.push(folderStructure);
        req.push("</FolderStructureChangeRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeRenameFolderBody = function (req, reqName, reqNamespace, folderId, folderName) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");
        //<action op='rename' id='450872' name='cccc' />
        req.push("<action ");
        req.push("op='rename' ");
        req.push("id='" + folderId + "' ");
        req.push("name='" + folderName + "' ");
        req.push(" />");
        req.push("</FolderActionRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeActionFolderBody = function (req, reqName, reqNamespace, folderId, action) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");
        req.push("<action ");
        req.push("op='" + action + "' ");
        req.push("id='" + folderId + "' ");
        req.push("/><type >simple</type>");
        req.push("</FolderActionRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeDeleteFolderBody = function (req, reqName, reqNamespace, folderId) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");
        req.push("<action ");
        req.push("op='delete' ");
        req.push("id='" + folderId + "' ");
        req.push(" />");
        req.push("</FolderActionRequest>");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeMsgActionBody = function (req, reqName, reqNamespace, msgId, op) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<action id='");
        req.push(msgId);
        req.push("'");
        req.push(" op='");
        req.push(op);
        req.push("' tcon='-ts' /><type >simple</type>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeGetContactsBody = function (req, reqName, reqNamespace, msgId, op) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeJsonHeader = function (json) {
        var header = {
            "context": {
                "_jsns": "urn:zimbra",
                "session": sessionId,
                "userAgent": {
                    "name": navigator.userAgent,
                    "version": "Bmail 7.0.2"
                },
                "account": {
                    "_content": this.username,
                    "by": "name"
                },
                "authToken": bmail.authenCookie
            }
        };
        json["Header"] = header;
    };

    SOAPRequest.prototype.makeAutoCompleteJsonBody = function (json, key) {
        var body = {
            "AutoCompleteRequest": {
                "_jsns": "urn:zimbraMail",
                "name": {
                    "_content": key
                }
            }
        };
        json["Body"] = body;
        //add it to json as attribute
    };

    /**
     * @params msg's params: from, to, cc, bcc, msg's content,...(json object, ex: params{from: "user@test.com}
     */
    SOAPRequest.prototype.makeSendMsgBody = function (json, params, folderId, mid, part) {
        var e = new Array();
        var isSendToPublishFolder = folderId != null;
        var requestName = isSendToPublishFolder ? "SendMailHereRequest" : "SendMsgRequest";
        var body = {};

        if (!Util.isUndefined(params.from)) {
            e.push({
                "t": "f",
                "a": params.from,
                "p": params.from_displayName
            });
        }
        if (!Util.isUndefined(params.to)) {
            if (params.to.indexOf(";") != -1) {
                var arr = params.to.split(";");
                for (var i = 0; i < arr.length; i++) {
                    e.push({
                        "t": "t",
                        "a": arr[i]
                    });
                }
            } else
                e.push({
                    "t": "t",
                    "a": params.to
                });
        }
        if (!Util.isUndefined(params.cc) && params.cc != "") {
            e.push({
                "t": "c",
                "a": params.cc
            });
        }
        if (!Util.isUndefined(params.bcc) && params.bcc != "") {
            e.push({
                "t": "b",
                "a": params.bcc
            });
        }
        if (mid && part) {
            var attach = "";
            var mp = [];
            //"attach":{"mp":[{"mid":"6163","part":"2"},{"mid":"6163","part":"3"},{"mid":"6163","part":"4"}]}
            for (var i = 0; i < part.length; i++) {
                mp.push({
                    "mid": mid,
                    "part": part[i]
                });
            }

            body[requestName] = {
                "_jsns": "urn:zimbraMail",
                "m": {
                    "e": e,
                    "su": {
                        "_content": params.subject
                    },
                    "mp": this.makeMPPart(params.mp),
                    "attach": {
                        "mp": mp
                    }
                }
            };
        } else {
            body[requestName] = {
                "_jsns": "urn:zimbraMail",
                "m": {
                    "e": e,
                    "su": {
                        "_content": params.subject
                    },
                    "mp": this.makeMPPart(params.mp)
                }
            };
        }

        if (isSendToPublishFolder) {
            body[requestName].l = { "_content": folderId + "" };
        }

        json["Body"] = body;
    };

    SOAPRequest.prototype.makeMPPart = function (mpart) {
        var mp = new Array();
        for (var i = 0; i < mpart.length; i++) {
            mp.push({});
            mp[i]["ct"] = mpart[i].ct;
            if (!Util.isUndefined(mpart[i].mp)) {
                mp[i]["mp"] = this.makeMPPart(mpart[i].mp);
            }
            if (!Util.isUndefined(mpart[i].content)) {
                mp[i]["content"] = {
                    "_content": mpart[i].content
                };
            }
        }
        return mp;

    };

    SOAPRequest.prototype.reqName
    SOAPRequest.prototype.requireUpdate

    /**
     * Send JSON request to server
     * json request does not need envelope element
     * @param reqName request's name
     * @param json json data
     * @param parent which fired event
     * @param output where we need to display data
     * @param input where we get the data
     */
    SOAPRequest.prototype.sendJsonRequest = function (reqName, json, parent, output, input, callback) {
        this.request.onreadystatechange = function () {
            SOAPRequest.readyData(this, parent, output, input, false, callback);
        };
        this.request.open("POST", this.url, true, reqName);
        this.request.send(JSON.stringify(json));
    };

    //----IM requests------//

    SOAPRequest.prototype.makeIMSendMessageBody = function (req, reqName, reqNamespace, to, msg) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<message");
        req.push(" xmlns='' addr='");
        req.push(to);
        req.push("'>");
        if (msg != "") {
            req.push("<body>");
            req.push(msg);
            req.push("</body>");
        }
        req.push("</message>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeIMSendTypingBody = function (req, reqName, reqNamespace, to) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<message");
        req.push(" xmlns='' addr='");
        req.push(to);
        req.push("'>");
        req.push("<typing/>");
        req.push("</message>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeIMGetUserOnlineRequest = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<IMGetUserOnlineRequest xmlns='urn:zimbraIM'/>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeIMGatewayListRequest = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<IMGatewayListRequest xmlns='urn:zimbraIM'/>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeIMGetRosterRequest = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<IMGetRosterRequest xmlns='urn:zimbraIM'/>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };

    SOAPRequest.prototype.makeIMGetMessageHistoryRequest = function (req, reqName, reqNamespace, friend, startIndex, maxMsg) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        //req.push("<list startIndex="5" maxMsg='4' addr='");
        req.push("<chat startIndex='" + startIndex + "' maxMsg='" + maxMsg + "' addr='");
        req.push(friend);
        req.push("' type='lastChatMessages'/>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };
    /**
     * <IMGetMessageHistoryRequest xmlns='urn:zimbraIM' ><list type='countConversations' addr='thuhien@bmail.vn' /></IMGetMessageHistoryRequest>
     */
    SOAPRequest.prototype.makeIMCountHistoryRequest = function (req, reqName, reqNamespace, friend) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        req.push("<list  addr='");
        req.push(friend);
        req.push("' type='countChatMessages'/>");

        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };
    /**
     * <list maxMsg='4' addr='dungha@bmail.vn' type='lastConversation'/>
     * @param {Object} req
     * @param {Object} reqName
     * @param {Object} reqNamespace
     */
    SOAPRequest.prototype.makeIMGetListHistoryRequest = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        req.push("'>");

        //add attributes, child elements
        //req.push("<list maxMsg ='1' start='0' getFirstMessageContent='true' type='chat'/>");
        req.push("<list maxMsg ='1' type='lastConversation' />");
        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
    };
    //---end IM requests----//

    //--Pull NoOp request--//
    SOAPRequest.prototype.makeNoOpRequest = function (req, reqName, reqNamespace) {
        req.push("<soap:Body>");
        req.push("<");
        req.push(reqName);
        req.push(" xmlns='urn:");
        req.push(reqNamespace);
        //<NoOpRequest xmlns='urn:zimbraMail></NoOpRequest>
        req.push("'");
        req.push(" wait='1'");
        //req.push(" limitToOneBlocked='1'");
        req.push(" >");
        req.push("</");
        req.push(reqName);
        req.push(">");
        req.push("</soap:Body>");
        req.push("</soap:Envelope>");
        //alert(req.join(""));
    };

    window.SOAPRequest = SOAPRequest;
})();