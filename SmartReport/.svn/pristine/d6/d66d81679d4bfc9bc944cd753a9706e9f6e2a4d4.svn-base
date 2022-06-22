(function (bmail) {
    bmail.request = {
        preAuthen: function (callback) {
            /// <summary>
            /// Request check mail mới
            /// </summary>

            var request = new SOAPRequest();
            var req = new Array();

            request.preAuthen(req);
            request.sendRequest("AuthRequest", req, callback);
        },

        noOption: function (callback) {
            /// <summary>
            /// Request check mail mới
            /// </summary>
            var request = new SOAPRequest();
            var req = new Array();
            request.makeHeader(req, "js");
            request.makeNoOpRequest(req, "NoOpRequest", "zimbraMail");
            request.sendRequest("NoOpRequest", req, callback);
        },

        getUserInfo: function (callback) {
            /// <summary>
            /// Lấy thông tin người dùng
            /// </summary>
            var request = new SOAPRequest();
            var req = new Array();

            request.makeHeader(req, "js", true);
            request.makeGetInfoBody(req, "GetInfoRequest", "zimbraAccount");

            request.sendRequest("GetInfoRequest", req, callback);
        },

        getFolderMailList: function (skip, query, take, isSentBox, callback) {
            var request = new SOAPRequest();
            var req = new Array();
            request.makeHeader(req, "js");
            request.makeSearchBody(req, "SearchRequest", "zimbraMail", skip, query, take, false, isSentBox);
            request.sendRequest("SearchRequest", req, callback);
        },

        readMail: function (msgId, callback) {
            /// <summary>
            /// Trả về trạng thái đã đọc cho mail
            /// </summary>
            /// <param name="msgId"></param>
            /// <param name="callback"></param>          
            var request = new SOAPRequest();
            var req = new Array();
            request.makeHeader(req, "js");
            request.makeGetMsgBody(req, "GetMsgRequest", "zimbraMail", msgId);
            request.sendRequest("GetMsgRequest", req, callback);
        },

        msgAction: function (op, msgId, callback) {
            /// <summary>
            /// Action với mail
            /// </summary>
            /// <param name="op">
            /// Action name:
            ///  - spam vs !spam
            ///  - trash
            ///  - delete
            ///  - read
            /// </param>
            /// <param name="msgId"></param>
            var request = new SOAPRequest();
            var req = new Array();
            request.makeHeader(req, "js");
            request.makeMsgActionBody(req, "MsgActionRequest", "zimbraMail", msgId, op);
            request.sendRequest("MsgActionRequest", req, callback);
        },

        changeFolderStructure: function (folder, callback) {
            /// <summary>
            /// <folder path='inbox' id='2' visible='1' link='0' isOpen="1" parentid='1'></folder>
            /// </summary>
            /// <param name="folderStructure"></param>
            var folderStructure = [];
            _.each(folder.models, function (model) {
                var structure = "<folder path='" + model.get("path") + "' id='" + model.get("id") + "' visible='" + model.get("visible");
                if (model.get("isOpen") == 1) {
                    structure += "' isOpen='1";
                }
                structure += "' link='" + model.get("link") + "' parentid='" + model.get("parentid") + "'></folder>";
                folderStructure.push(structure);
            });
            var request = new SOAPRequest();
            var req = new Array();
            request.makeHeader(req, "js");
            request.makeFolderStructureBody(req, folderStructure.join(""), "zimbraMail");
            request.sendRequest("FolderStructureChangeRequest", req, callback);
        },

        setFolderRead: function (folderId, callback) {
            makeFolderActionRequest(folderId, "read", callback);
        },

        setEmptyFolder: function (folderId, callback) {
            makeFolderActionRequest(folderId, "empty", callback);
        },

        sendMail: function (msg, mailId, parts, callback) {
            var json = {};
            var request = new SOAPRequest();
            request.makeJsonHeader(json);
            request.makeSendMsgBody(json, msg[0], null, mailId, parts);
            request.sendJsonRequest("SendMsgRequest", json, $(this), undefined, undefined, callback);
        },

        sendPFMail: function (msg, mailId, folderId, attachs, callback) {
            var json = {};
            var request = new SOAPRequest();
            request.makeJsonHeader(json);
            request.makeSendMsgBody(json, msg[0], folderId, mailId, attachs);
            request.sendJsonRequest("SendMsgRequest", json, $(this), undefined, undefined, callback);
        },

        search: function (from, subject, fromDate, toDate, skip, take, success) {
            var request = new SOAPRequest();
            var req = new Array();
            request.makeHeader(req, "js");
            var query = ' is:anywhere ';
            if(from != ''){
                query += String.format(' from:({0}) ', from);
            }
            if(subject != ''){
                query += String.format(' subject:({0}) ', subject);
            }
            if (fromDate != '' && toDate != '') {
                query += String.format(' after:"{0}" before:"{1}" ', fromDate, toDate);
            }

            request.makeSearchBody(req, "SearchRequest", "zimbraMail", skip, query, take, true, false);
            request.sendRequest("SearchRequest", req, success);
        }
    }

    function makeFolderActionRequest(folderId, action, callback) {
        /// <summary>
        /// Send folder request 
        /// </summary>
        /// <param name="folderId">folderId</param>
        /// <param name="action">action:
        ///  - create, move, delete
        ///  - read, markallread
        ///  - rename
        ///  - empty
        /// </param>
        /// <param name="callback">callback</param>

        var request = new SOAPRequest();
        var req = new Array();
        request.makeHeader(req, "js");
        request.makeActionFolderBody(req, "FolderActionRequest", "zimbraMail", folderId, action);
        request.sendRequest("FolderActionRequest", req, callback);
    }

})
(window.bmail = window.bmail || {});

