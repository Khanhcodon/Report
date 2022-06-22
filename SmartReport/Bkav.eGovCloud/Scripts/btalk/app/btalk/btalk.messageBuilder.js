(function ($, btalk) {
    /*
        Đọc thêm các thông tin về gói tin xmpp ở đây
        https://xmpp.org/rfcs/rfc3921.html
    */

    if (typeof window.JSJaCMessage === "undefined") {
        throw "Chưa tải thư viện jsjac.js";
    }

    var NS_RECEIVED = "urn:xmpp:receipts";

    var MessageBuilder = {
        _message: null,
        init: function (id) {
            this._message = new window.JSJaCMessage(id);

            this._message.JSON = function () {
                return $.xml2json(this.doc);
            }

            return this;
        },

        setId: function (jid) {
            this._message.setID(jid);
            return this;
        },

        setType: function (type) {
            this._message.setType(type);
            return this;
        },

        setFrom: function (from) {
            this._message.setFrom(from);
            return this;
        },

        setTo: function (to) {
            this._message.setTo(to);
            return this;
        },

        setBody: function (body) {
            this._message.setBody(body);
            return this;
        },

        setAttr: function (name, value) {
            this._message.getNode().setAttribute(name, value);
            return this;
        },

        appendChild: function (name, attributes, chilren) {
            if (String.isNullOrEmpty(name)) {
                return this;
            }

            var childNode = this._message.getDoc().createElement(name);
            if (attributes && typeof attributes === 'object') {
                _.each(_.keys(attributes), function (name) {
                    childNode.setAttribute(name, attributes[name]);
                });
            }

            if (chilren && typeof chilren === 'object') {

            }

            this._message.getNode().appendChild(childNode);
            return this;
        },

        get: function () {
            this.setAttr('clienttime', new Date().getTime().toString())
            return this._message;
        }
    };

    var IQMessageBuilder = {
        _iqmessage: null,
        _newElement: null,

        init: function (id, type, to) {
            this._iqmessage = new window.JSJaCIQ();
            this.setIq(id, type, to);

            this._iqmessage.JSON = function () {
                return $.xml2json(this.doc);
            }

            return this;
        },

        setIq: function (id, type, to) {
            type = type || 'get';
            to = to || null;

            this._iqmessage.setIQ(to, type, id);
            return this;
        },

        appendChild: function (name, attributes, value) {
            this._newElement = this._iqmessage.buildNode(name, attributes, value);
            this._iqmessage.getNode().appendChild(this._newElement);

            return this;
        },

        appendNodeChild: function (parentName, name, attributes, value) {
            if (String.isNullOrEmpty(parentName)) return this;

            var parentNode = this._iqmessage.getNode().getElementsByTagName(parentName).item(0);
            if (!parentName) return this;

            this._newElement = this._iqmessage.buildNode(name, attributes, value);
            parentNode.appendChild(this._newElement);

            return this;
        },

        setQuery: function (query) {
            this._iqmessage.setQuery(query);
            return this;
        },

        setType: function (type) {
            this._iqmessage.setType(type);
            return this;
        },

        get: function () {

            return this._iqmessage;
        }
    };

    btalk.messageFactory = {
        _messageBase: function (id, from, to, type) {
            var result = MessageBuilder.init(id);
            result.setId(id)
                .setFrom(from)
                .setTo(to)
                .setType(type);

            return result;
        },

        _iqMessage: function (id, type, to) {
            return IQMessageBuilder.init(id, type, to);
        },

        //#region Chat Message Request

        sendMessage: function (id, from, to, type, body, quote) {
            /*
             * Tạo gói tin gửi tin nhắn đi, bao gồm các tin nhắn chat thông thường
                <message xmlns="jabber:client" id="1534740038052" from="bkav.tienbv@sotttt.tayninh.gov.vn" to="bkav.cuongnt@sotttt.tayninh.gov.vn" type="chat" clienttime="1534740038052">
                    <body>i</body>
                    <quote></quote>
                </message>
             */
            var message = new this._messageBase(id, from, to, type);
            message.setBody(body);

            if (quote && typeof quote === 'object') {
                message.appendChild('quote', quote);
            }

            return message.get();
        },

        sendStatusMessage: function (id, from, to, type, status, secs) {
            /*
             * Tạo gói tin gửi trạng thái gửi nhận tin nhắn

             */

            var message = new this._messageBase(id, from, to, type);
            message.appendChild("received", { xmlns: NS_RECEIVED, id: id, status: status, secs: secs });

            return message.get();
        },

        sendComposingMessage: function (id, from, to, type) {
            /*
             * Tạo gói tin gửi trạng thái đang gõ.
             <message xmlns="jabber:client" id="1534740667603" from="bkav.tienbv@sotttt.tayninh.gov.vn" to="bkav.dambv@sotttt.tayninh.gov.vn" type="chat" clienttime="1534740667603">
                <composing xmlns="http://jabber.org/protocol/chatstates" jid="bkav.tienbv@sotttt.tayninh.gov.vn"/>
             </message>
             */

            var message = new this._messageBase(id, from, to, type);
            message.appendChild('composing', { 'xmlns': 'http://jabber.org/protocol/chatstates', 'jid': from });

            return message.get();
        },

        sendConfigMessage: function (id, from, to, type, configs) {
            /*
             * Gói tin gửi trạng thái chung của room chat: thay đổi biệt hiệu, thời điểm bắt đầu một cuộc hội thoại, thay đổi tên nhóm, ...
                <message xmlns="jabber:client" id="1534740304930" from="bkav.tienbv@sotttt.tayninh.gov.vn" to="bkav.cuongnt@sotttt.tayninh.gov.vn" type="chat" clienttime="1534740304931">
                    <body>Room Config</body>
                    <config xmlns="" key="edit_by" value="bkav.tienbv@sotttt.tayninh.gov.vn"/>
                    <config xmlns="" key="start_conversion" value="1534740304930"/>
                </message>
             */

            if (!configs) return null;

            var message = new this._messageBase(id, from, to, type);
            message.setBody("Room Config");

            for (var i in configs) {
                var config = {};
                config[configs[i].key] = configs[i].value;
                message.appendChild('config', config);
            }

            return message.get();
        },

        sendAttachmentMessage: function (id, from, to, type, attachments) {
            /*
             * Tạo gói tin gửi file đính kèm
             */
            var msgBody = "";
            var fileType = "file";

            if (!attachments) return;

            var message = new this._messageBase(id, from, to, type);

            for (var i in attachments) {
                msgBody = msgBody + attachments[i].name;

                if (i < _.keys(attachments).length - 1) {
                    msgBody = msgBody + this.options.file.seperatekey;
                }
            }
            message.setBody(msgBody);

            if (attachments[_.keys(attachments)[0]].type.indexOf('image') > -1) {
                fileType = "image";
            }
            message.setAttr('msgContentType', fileType);

            _.each(attachments, function (file) {
                // attachment
                var attrs = {
                    'id': file.id, 'name': file.name, 'object': file.object, 'type': file.type, 'percentage': 101,
                    'messageid': file.messageid, 'sentDate': file.sentDate, 'fileServerType': file.fileServerType
                };

                file.size && parseInt(file.size) && (attrs['size'] = file.size);
                file.lastModified && (attrs['lastModified'] = file.lastModified);
                file.lastModifiedDate && (attrs['lastModifiedDate'] = file.lastModifiedDate);
                file.tenantid && (attrs['tenantid'] = file.tenantid);
                file.dimension && (attrs = _.extend(attrs, file.dimension));

                message.appendChild('attachment', attrs);
            });

            return message.get();
        },

        //#endregion

        //#region Roster, history, online Request

        getRosterIqRequest: function () {
            // <iq xmlns="jabber:client" type="get" id="roster_1"><query xmlns="jabber:iq:roster"/></iq>

            var id = 'roster_1', type = 'get';
            var result = new this._iqMessage(id, type);
            result.setQuery('jabber:iq:roster');

            return result.get();
        },

        getOnlineIqRequest: function () {
            // <iq xmlns="jabber:client" type="set" id="enable_auto_send_presence1532505128399"><mobile xmlns="http://btalk.vn/protocol/btalk_mobile#v2" enable="false"/></iq>

            var id = 'enable_auto_send_presence' + new Date().getTime().toString();
            var type = 'set';
            var result = new this._iqMessage(id, type);

            var xmlns = 'http://btalk.vn/protocol/btalk_mobile#v2';
            result.appendChild('mobile', { enable: 'false', 'xmlns': xmlns });

            return result.get();
        },

        getHistoryIqRequest: function (start, skip, take) {
            // <iq xmlns="jabber:client" type="get" id="jwchat_history"><list xmlns="urn:xmpp:archive" start="2015-01-01T00:00:00Z"><set xmlns="http://jabber.org/protocol/rsm"><max xmlns="">15</max><after xmlns="">15</max></set></list></iq>

            var id = 'jwchat_history', type = 'get';
            var result = new this._iqMessage(id, type);
            var xmlns = 'urn:xmpp:archive';
            var nodeName = 'list';
            skip = skip || 0;
            take = take || 10;

            result.appendChild(nodeName, { 'start': start, 'xmlns': xmlns })
                    .appendNodeChild(nodeName, 'set', { xmlns: 'http://jabber.org/protocol/rsm' })
                    .appendNodeChild('set', 'max', null, take)
                    .appendNodeChild('set', 'after', null, skip);

            return result.get();
        },

        GetMembersIqRequest: function (groupId) {
            // <iq id='member3' type='get' to='dambv1479888134772@conference.bkav.com'> <query xmlns='http://jabber.org/protocol/muc#admin'><item affiliation='owner'/></query></iq>

            var id = "get_members" + new Date().getTime().toString();
            var type = 'get';
            var to = btalk.cutResource(groupId);

            var result = new this._iqMessage(id, type, to);
            result.appendChild('query', { xmlns: "http://jabber.org/protocol/muc#admin" })
                    .appendNodeChild('query', 'item', { affiliation: 'owner' });

            return result.get();
        },

        GetMessageHistoriesIqRequest: function (jid, type, start, skip, take) {
            /*
                Build và trả về gói tin lấy danh sách tin nhắn có phân trang
                <iq xmlns="jabber:client" type="get" id="jwchat_get_history">
                    <retrieve xmlns="urn:xmpp:archive" start="2015-01-01T00:00:00Z" with="bkav.dambv@sotttt.tayninh.gov.vn">
                        <set xmlns="http://jabber.org/protocol/rsm">
                            <max xmlns="">30</max>
                            <after xmlns="">60</after>
                        </set>
                    </retrieve>
                </iq>
            */
            var id = 'jwchat_get_history';
            var xmlns = 'urn:xmpp:archive';
            skip = skip || 0;
            take = take || 30;

            var result = new this._iqMessage(id, 'get');
            var nodeName = type === btalk.CHATTYPE.GROUPCHAT ? 'groupchat' : 'retrieve';

            result.appendChild(nodeName, { xmlns: 'urn:xmpp:archive', start: start, 'with': jid })
                    .appendNodeChild(nodeName, 'set', { xmlns: 'http://jabber.org/protocol/rsm' })
                    .appendNodeChild('set', 'max', null, take)
                    .appendNodeChild('set', 'after', null, (skip - 1) + ''); // -1 do index bắt đầu từ 0

            return result.get();
        },

        //#endregion
    };
})
($, window.btalk = window.btalk || {});