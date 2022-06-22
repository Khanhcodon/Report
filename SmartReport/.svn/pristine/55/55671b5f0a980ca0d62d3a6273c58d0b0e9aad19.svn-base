/*
 * Utilities for web interface
 * Static Class
 * @author ducla
 * @since 24/04/2013
 */
function Util() {
}

//global varialbes
var noDuplicateNumber = 0;
//for render id
//prefix
var displayAddress = "addr_";
var autoComplete = "auto_";

Util.getRequest = function () {
    var request = null;
    if (window.XMLHttpRequest) {
        request = new XMLHttpRequest();
    } else {
        try {
            request = new ActiveXObject("MSXML2.XMLHTTP3.0");
        } catch (ex) {
            return null;
        }
    }

    return request;
};

Util.makeJsonHeader = function (json) {
    var header = {
        "context": {
            "_jsns": "urn:zimbra",
            "session": sessionId,
            "userAgent": {
                "name": navigator.userAgent,
                "version": "Bmail 7.0.2"
            },
            "account": {
                "_content": Util.readCookie("user"),
                "by": "name"
            },
            "authToken": Util.readCookie("authen")
        }
    };
    json["Header"] = header;
};

Date.prototype.getVNDay = function () {
    switch (this.getDay()) {
        case 1:
            return getResource("egov.resources.time.mon");
            break;
        case 2:
            return getResource("egov.resources.time.tue");
            break;
        case 3:
            return getResource("egov.resources.time.wed");
            break;
        case 4:
            return getResource("egov.resources.time.thi");
            break;
        case 5:
            return getResource("egov.resources.time.fri");
            break;
        case 6:
            return getResource("egov.resources.time.sat");
            break;
        default:
            return getResource("egov.resources.time.sun");
    }
}

Util.prettyDate = function (date) {
    /// <summary>Định dạng thời gian</summary>
    ///<param = "date"> thời gian truyền vào</param>
    date = new Date(date);
    var hours = date.getHours();
    var mins = date.getMinutes();
    if (hours < 10) {
        hours = "0" + hours;
    }
    if (mins < 10) {
        mins = "0" + mins;
    }

    var dateNow = new Date();
    var diff = ((dateNow.getTime() - date.getTime()) / 1000);
    var day_diff = Math.floor(diff / 86400);
    if (day_diff === 0 && date.getDate() === dateNow.getDate()) {
        if (diff < 120) {
            return "1" + egov.resources.time.minbefore;
        }
        else if (diff < 3600) {
            return Math.floor(diff / 60) + egov.resources.time.minbefore;
        }
        else {
            return hours + ":" + mins;
        }
    }
    //else if (date.weekOfYear() === dateNow.weekOfYear()) {
    //    return date.getVNDay();
    //}

    return date.getDate() + "/" + date.getMonth();
}
//will return "yesterday" if time is yesterday?
Util.convertTime = function (time, isDetailTime) {
    var vtime = new Date(time);
    var tmp = new Date();
    // if (tmp.getFullYear() == vtime.getFullYear()) {
    // if (tmp.getMonth() == vtime.getMonth()) {
    // if ((tmp.getDate() - vtime.getDate()) == 0) {
    // return "Today";
    // }
    // if ((tmp.getDate() - vtime.getDate()) < 2) {
    // return "Yesterday";
    // }
    // }
    // }
    var hour = vtime.getHours();
    if (hour < 10)
        hour = "0" + hour;
    var minutes = vtime.getMinutes();
    if (minutes < 10)
        minutes = "0" + minutes;

    var month = vtime.getMonth() + 1;
    if (month < 10)
        month = "0" + month;

    var date = vtime.getDate();
    if (date < 10)
        date = "0" + date;
    if (!isDetailTime && tmp.getFullYear() == vtime.getFullYear()) {
        if (tmp.getMonth() == vtime.getMonth()) {
            if ((tmp.getDate() - vtime.getDate()) == 0) {
                return hour + ":" + minutes// + ", " + bmail.resources.common.time.today.toLowerCase();
            }

        }
    }
    return date + "/" + month + "/" + vtime.getFullYear();
};

Util.convertDetailTime = function (time) {
    var vtime = new Date(time);
    var hour = vtime.getHours();
    if (hour < 10)
        hour = "0" + hour;
    var minutes = vtime.getMinutes();
    if (minutes < 10)
        minutes = "0" + minutes;
    return hour + ":" + minutes + " " + Util.convertTime(time, true);
};

Util.getDate = function (time) {
    var vtime = new Date(time);
    return vtime.getDate();
};
Util.getMonth = function (time) {
    var vtime = new Date(time);
    return (vtime.getMonth() + 1);
};
Util.getFullYear = function (time) {
    var vtime = new Date(time);
    return vtime.getFullYear();
};
Util.getHours = function (time) {
    var vtime = new Date(time);
    return vtime.getHours();
};
Util.getMinutes = function (time) {
    var vtime = new Date(time);
    return vtime.getMinutes();
};

Util.getTime = function (date, string_format) {
    /// <summary>
    /// Lấy thời gian theo định dạng
    /// </summary>
    /// <param name="date">Giá trị truyền vào</param>
    /// <param name="string_format">Định dạng muốn chuyển</param>
    /// <returns type=""></returns>

    if (typeof date == "number") {

    }

    return new Date();
}

/**
 * static method
 * check if variable is defined
 * @param object input object
 * @return true if variable is undefine and vice versa
 */
Util.isUndefined = function (object) {
    if (typeof object == "undefined") {
        return true;
    }
    return false;
};

/**
 * Get email addresses from an htmlTag
 * @param htmlTag input tag
 * @return a string with seperate is semi-colon
 */
Util.getAddress = function (htmlTag) {
    var addresses = new Array();
    $(htmlTag).children("#displayAddress").each(function () {
        addresses.push($(this).text());
    });
    return addresses.join(";");
};
Util.getAddressToSend = function (tagId) {

};
/**
 *Create bubble address
 *@param addresses an array that contains addresses
 *@return html string
 */
Util.displayBubbleAddress = function (addresses) {
    var html = new Array();
    for (var i = 0; i < addresses.length; i++) {
        noDuplicateNumber++;
        html.push("<div id='displayAddress'");
        html.push(" class='");
        html.push(displayAddress + noDuplicateNumber);
        //"addr_1265"
        html.push("'>");
        if (addresses[i].indexOf("gmail", 0) != -1) {
            html.push("<img src='/mail/cssmobile/images/google.png' id='logo-service'/>");
        } else if (addresses[i].indexOf("yahoo", 0) != -1) {
            html.push("<img src='/mail/cssmobile/images/yahoo.png' id='logo-service'/>");
        }
        html.push(addresses[i]);
        html.push("</div>");
    }
    return html.join("");
};

/**
 * Search contact with a specify input
 * @param contacts json, that contains contact data
 * @param amount the number of results we need to return
 * @param query what do we want?
 * @return number of results of contact
 */
Util.searchContact = function (contacts, amount, query) {
    console.log(query);
    var count = 0;
    var results = new Array();
    for (var i = 0; i < contacts.length; i++) {
        if (contacts[i].name.indexOf(query, 0) != -1 || contacts[i].email.indexOf(query, 0) != -1) {
            results.push({
                id: contacts[i].id,
                name: contacts[i].name == "" ? contacts[i].email : contacts[i].name,
                email: contacts[i].email
            });
            count++;
        }
        if (count == amount) {
            break;
        }
    }
    return results;
};
/**
 * kiem tra xem  một địa chỉ có trong mảng chưa đọc ko
 */
Util.checkNewChatArr = function (from) {
    if (newChatAddress && newChatAddress.length == 0)
        return -1;
    var index = -1;
    for (var i = 0; i < newChatAddress.length; i++) {
        var _from = newChatAddress[i].from;
        if (_from == from) {
            index = i;
            break;
        }
    }
    return index;
};
Util.updateNoti = function () {
    var _leg = newChatAddress.length;
    if (_leg != 0) {
        $(".chatNoti").css({
            "display": "block"
        });
        if ($(".chatNoti").length > 0)
            $(".chatNoti").html(_leg);

    } else
        $(".chatNoti").css({
            "display": "none"
        });
};
Util.updateFriendStatus = function (address, status) {
    if (friendListChat.length > 0) {
        for (var i = 0; i < friendListChat.lenght; i++) {
            var _address = friendListChat[i].address;
            if (_address == address) {
                friendListChat[i].show = status;
                break;
            }
        }
    }
};
Util.updateAvatar = function (address, avatar) {
    if (friendListChat.length > 0) {
        for (var i = 0; i < friendListChat.lenght; i++) {
            var _address = friendListChat[i].address;
            if (_address == address) {
                friendListChat[i].avatarHash = avatar;
                break;
            }
        }
    }
};
Util.sortRecentlyArray = function () {
    if (recentlyAddress.length > 1) {
        qsort(recentlyAddress, 0, recentlyAddress.length);
    }
};
function partition(array, begin, end, pivot) {
    var piv = recentlyAddress[pivot].count;
    array.swap(pivot, end - 1);
    var store = begin;
    var ix;
    for (ix = begin; ix < end - 1; ++ix) {
        if (array[ix].count <= piv) {
            array.swap(store, ix);
            ++store;
        }
    }
    array.swap(end - 1, store);

    return store;
}

Array.prototype.swap = function (a, b) {
    var tmp = this[a];
    this[a] = this[b];
    this[b] = tmp;
};
function qsort(array, begin, end) {
    if (end - 1 > begin) {
        var pivot = begin + Math.floor(Math.random() * (end - begin));

        pivot = partition(array, begin, end, pivot);

        qsort(array, begin, pivot);
        qsort(array, pivot + 1, end);
    }
}

Util.checkExistItem = function (arr, item) {
    if (arr.length == 0)
        return -1;
    else {
        var result = -1;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == item) {
                result = i;
                break;
            }
        }
    }
    return result;
};
Util.updateNewChatArr = function (from, msg) {
    if (newChatAddress && newChatAddress.length > 0) {
        var index = Util.checkNewChatArr(from);
        if (index != -1)
            newChatAddress[index].message = msg;
    }
};
Util.removeNewChatItem = function (from) {
    if (newChatAddress && newChatAddress.length > 0) {
        var index = Util.checkNewChatArr(from);
        if (index != -1) {
            var tmpArr = [];
            for (var i = 0; i < newChatAddress.length; i++) {
                var _from = newChatAddress[i].from;
                if (_from != from) {
                    tmpArr.push(newChatAddress[i]);
                }
            }
        }
        newChatAddress = tmpArr;
    }
};

Util.getCookie = function (responeData) {
    var result = "";
    if (responeData.AuthResponse && responeData.AuthResponse.authToken) {
        result = responeData.AuthResponse.authToken[0]._content;
    }
    return result;
}
Util.readCookie = function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length);
        }
        if (c.indexOf(nameEQ) == 0) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return null;
};

Util.createCookie = function (name, value, days, domain) {
    var expires;
    if (!days) {
        expires = "";
    } else {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 3600 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
};

Util.removeCookie = function (name, domain) {
    Util.createCookie(name, "", -1, domain);
}

Util.isValidEmail = function (email) {
    if (email == "") {
        //do not need to check if it is null
        return true;
    }
    var emails = email.split(";");
    var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
    for (var i = 0; i < emails.length; i++) {
        if (emails[i] == "") {
            emails.splice(i, 1);
        }
        else {
            if (!pattern.test(emails[i])) {
                alert(email[i] + ": not an valid email");
                return false;
            }
        }
    }
    return true;
};

/**
 * Need to send autocomplete request?
 * @param keycode key's code
 * @param input input text
 * @param charcode char's code, an asscii we received when user press a key, sometimes it's not like keycode
 */
Util.needToSendAutoComplete = function (keycode, input, charcode) {
    var tmp = new Array();
    if (keycode != 186 || keycode != 13) {
        if (charcode != ";" || charcode != "," || charcode != "[" || charcode != "]") {
            if (input.length > 0 && keycode != 8) {
                if (!Util.isUndefined(autoCompleteResult)) {
                    for (var i = 0; i < autoCompleteResult.length; i++) {
                        if (autoCompleteResult[i].email.indexOf(input, 0) != -1 || autoCompleteResult[i].name.indexOf(input, 0) != -1) {
                            tmp.push({
                                email: autoCompleteResult[i].email,
                                name: autoCompleteResult[i].name,
                                ranking: autoCompleteResult[i].ranking
                            });
                        }
                    }
                    if (tmp.length != 0) {
                        stillNeedToAutoComplete = false;
                        autoCompleteResult.length = 0;
                        autoCompleteResult = tmp;
                        //console.log("2");
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    console.log("6");
                    return false;
                }
                console.log("5");
                return false;
            } else {
                return true;
            }
            console.log("1");
            return false;
        } else {
            console.log("4");
            return true;
        }
        console.log("3");
        return false;
    } else {
        return true;
    }
};
/**
 * 	get folder by folderId from folderList
 * 	id : links[i].id,
 name : links[i].name,
 unread : Util.isUndefined(links[i].u) ? 0 : links[i].u,
 n : Util.isUndefined(links[i].n) ? 0 : links[i].n
 */
Util.getFolderById = function (_folderList, _folderId) {
    var folder = null;
    var l = _folderList.length;
    if (l > 0) {
        for (var i = 0; i < l; i++) {
            var id = _folderList[i].id;
            if (id == _folderId) {
                folder = _folderList[i];
                break;
            }
        }
    }
    return folder;
};
/**
 * update property của folder list với folderId
 *
 */
Util.updateFolderList = function (_folderList, _folderId, property, value) {
    var l = _folderList.length;
    if (l > 0) {
        for (var i = 0; i < l; i++) {
            var obj = _folderList[i];
            var id = obj.id;
            if (id == _folderId) {
                for (var j in obj) {
                    if (j == property)
                        obj[j] = value;
                }
                break;
            }
        }
    }
};
Util.pageLogout = function (reason) {
    var loc = window.location.host;
    Util.createCookie("authen", "", -1);
    //clear cookie
    //var url = "https://" + loc + "/mail/loginMobile.html?" + "reason=" + reason;
    //var url = "https://" + loc + "/loginMobile/";
    //document.title = "Login Mobile";
    //window.location = url;
};

Util.getUrl = function () {
    return window.location.protocol + "//" + window.location.host + "/mail/";
};

/**
 * Get the param's value from url string
 * @param name the name of the param
 * @return the value of param, ex: ?@k=v, we'll get v by give k.
 */
Util.getUrlParam = function (name) {
    var result = new RegExp(name + "=([^&]*)", "i").exec(window.location.search);
    return result && result[1] || "";
};

Util.sendPullRequest = function (parent) {
    var request1 = new SOAPRequest();
    var xml = new Array();
    request1.makeHeader(xml, "js");
    request1.makeNoOpRequest(xml, "NoOpRequest", "zimbraMail");
    request1.sendRequest("NoOpRequest", xml, parent);
};

Util.makeClassForLi = function (clazzName) {
    clazzName = clazzName.replace(/\@/g, "");
    clazzName = clazzName.replace(/\./g, "");
    clazzName = clazzName.replace(/\%/g, "");
    return clazzName;
};

/**
 * insert avatar into IM, Msg module
 */
Util.makeAvatar = function (html, size, friend, gateway, clazz) {
    var link = "";
    var i;
    if (gateway == "bmail") {
        for (i = 0; i < bads.length; i++) {
            if (friend == bads[i].addr) {
                link = bads[i].link;
                break;
            }
        }
    } else if (gateway == "yahoo") {
        for (i = 0; i < yads.length; i++) {
            if (friend == yads[i].addr) {
                link = yads[i].link;
                break;
            }
        }
    } else if (gateway == "facebook") {
        for (i = 0; i < fads.length; i++) {
            if (friend == fads[i].addr) {
                link = fads[i].link;
                break;
            }
        }
    } else if (gateway == "gtalk") {
        for (i = 0; i < gads.length; i++) {
            if (friend == gads[i].addr) {
                link = gads[i].link;
                break;
            }
        }
    }
    if (clazz != "") {
        html.push("<img class='");
        html.push(clazz);
        html.push("' ");
        html.push("src='");
    } else {
        html.push("<img src='");
    }
    if (link == "") {
        html.push("/mail/cssmobile/images/unknow.png");
    } else {
        html.push("https://" + window.location.host + "/mail/" + link);
    }
    html.push("' width='");
    html.push(size);
    html.push("' />");
};

Util.improveClickEvent = function () {
    //copy the current click events on document
    var clickEvents = $.hasData(document) && $._data(document).events.click;
    clickEvents = $.extend(true, {}, clickEvents);
    //remove these click events
    $(document).off("click");
    //reset them as vclick events
    for (var i in clickEvents) {
        console.log(clickEvents[i]);
        $(document).on("vclick", clickEvents[i].handler);
    }
};

/**
 * Set parameter on the link
 * See more http://stackoverflow.com/questions/486896/adding-a-parameter-to-the-url-with-javascript
 * @param params is a json object
 */
Util.makeUrlParameters = function (params) {
    var prms = "";
    var keys = Object.keys(params);
    for (var i = 0; i < keys.length; i++) {
        prms += "&" + keys[i] + "=" + params[keys[i]];
    }
    window.history.pushState("ducla", "Hop thu", "/mail/Tablet/index.html?" + prms);
};

/**
 * Update data on page if having
 * @param data new data
 * @param typeData it's message history fragment (msg) or avatar (avartar)
 */
Util.updateDataForPage = function (data, typeData) {
    var ilEls = $("#bmailChat > li").map(function () {
        return this.id;
    }).get();
    //get all id of child elements
    var i;
    var id;
    var name;
    var content;
    if (typeData == "msg") {
        for (i = 0; i < ilEls.length; i++) {
            id = "#" + ilEls[i];
            content = $("#bmailChat").children(id).html();
            name = $("#bmailChat").children(id).attr("name");
            if (Util.isUndefined(data[name])) {
                data[name] = "";
            }
            var index = content.indexOf("</h5>") + 5;
            var newContent = content.substring(0, index) + "<p style='white-space:nowrap'>" + data[name] + "</p>" + content.substring(index, content.length);
            $("#bmailChat").children(id).html(newContent);
        }

    } else if (typeData == "avatar") {
    }
};

/**
 * get part list cho phần save draft
 */
Util.getPartList = function () {
    var partList = [];

    return partList;
};
/**
 *	Lưu thư nháp
 *	@param {String} user
 *	@param {String} authen
 *	@param {String} emailTo
 *	@param {String} subject
 *	@param {String} multipart
 *	@param {Array} attachId
 *  @param {midPart} part của mail draft trước
 */
Util.saveDraft = function (user, authen, emailTo, subject, multipart, attachId, idMailDraft, idnt, irt, midPart, isAttaching) {
    var aid = '';
    if (attachId) {
        var l = attachId.length;
        if (l == 1) {
            var len = attachId[0].length;
            aid += attachId[0].substring(1, len - 1);
        } else {
            for (var i = 0; i < l; i++) {
                var len = attachId[i].length;
                if (i == 0)
                    attachId[i] = attachId[i].substring(1, len);
                if (i == l - 1)
                    attachId[i] = attachId[i].substring(0, len - 1);
                attachId[i] += ',';
                aid += attachId[i];
            }
        }
    }
    var url = egov.connections.BmailLink + "/service/soap";
    var soapCompose = "<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope'>";
    soapCompose += "<soap:Header>";
    soapCompose += "<context xmlns='urn:zimbra'>";
    soapCompose += "<format type='js'/>";
    soapCompose += "<account by='name'>" + user + "</account>";
    soapCompose += "<authToken>" + authen + "</authToken>";
    soapCompose += "</context>";
    soapCompose += "</soap:Header>";
    soapCompose += "<soap:Body>";
    soapCompose += "<SaveDraftRequest xmlns='urn:zimbraMail'>";
    if (idMailDraft && idMailDraft != "")
        if (idnt)
            soapCompose += "<m id='" + idMailDraft + "' idnt='" + idnt + "'>";
        else
            soapCompose += "<m id='" + idMailDraft + "' >";
    else
        soapCompose += "<m>";
    if (emailTo)
        soapCompose += emailTo;
    soapCompose += "<su>" + subject + "</su>";
    if (multipart)
        soapCompose += multipart;
    if (midPart && midPart.length > 0) {
        if (aid || isAttaching)
            soapCompose += "<attach aid='" + aid + "'>";
        else
            soapCompose += "<attach>";
        var _l = midPart.length;
        if (idMailDraft)
            for (var t = 0; t < _l; t++) {
                soapCompose += "<mp mid='" + idMailDraft + "' part='" + midPart[t] + "' />";
            }

        soapCompose += "</attach>";
    } else {
        if (aid)
            soapCompose += "<attach aid='" + aid + "'></attach>";
    }

    if (irt)
        soapCompose += "<irt>" + irt + "</irt>";

    soapCompose += "</m>";
    soapCompose += "</SaveDraftRequest>";
    soapCompose += "</soap:Body>";
    soapCompose += "</soap:Envelope>";

    var http = Util.getRequest();
    http.open('POST', url, false, "SaveDraftRequest");
    http.send(soapCompose);
    var responseJson = "error";
    responseJson = eval("(" + http.responseText + ")");
    return responseJson;
};
/**
 * get request
 */
Util.getRequest = function () {
    var http_request;
    if (window.ActiveXObject) {
        try {
            http_request = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                http_request = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {
            }
        }
    } else if (window.XMLHttpRequest) {
        http_request = new XMLHttpRequest();
        if (http_request.overrideMimeType) {
            http_request.overrideMimeType("text/xml");
        }
    }
    return http_request;
};

Util.toJson = function (text) {
    try {
        var json = JSON.parse(text);
        console.log("la json");
    } catch (e) {
        console.log("ko phai la json");
        var json = eval("(" + text + ")");
    }
    return json;
};

function insertImage(url) {
    var sel = document.selection;
    if (sel) {
        var textRange = sel.createRange();
        document.execCommand('insertImage', false, url);
        textRange.collapse(false);
        textRange.select();
    } else {
        document.execCommand('insertImage', false, url);
    }
}

function insertLink(link) {
    var sel = document.selection;
    if (sel) {
        var textRange = sel.createRange();
        document.execCommand('createLink', false, link);
        textRange.collapse(false);
        textRange.select();
    } else {
        document.execCommand('createLink', false, link);
    }
}

Util.getSubject = function (subject) {
    if (subject && subject.length > 0) {
        return subject;
    }
    else {
        return getResource("bmail.resources.detail.nosubject");
    }
}

/**
 * document.execCommand("insertHTML",false,"<a href='"+href+"'>"+selected+"</a>");
 */
function insertHtml(html) {
    var sel = document.selection;
    if (sel) {
        var textRange = sel.createRange();
        document.execCommand('insertHTML', false, html);
        textRange.collapse(false);
        textRange.select();
    } else {
        document.execCommand('insertHTML', false, html);
    }
}

function configMessage(chatAreaContent) {
    if (chatAreaContent.indexOf("http://www.youtube.com/watch") != -1) {
        var dd, text1;
        text1 = chatAreaContent.substring(chatAreaContent.indexOf("http://www.youtube.com/watch"));
        if (text1.indexOf(" ") != -1)
            dd = text1.substring(0, text1.indexOf(" "));
        else
            dd = text1;
        if (dd.indexOf("&") != -1)
            var str = dd.substring(dd.indexOf("=") + 1, dd.indexOf("&"));
        else
            var str = dd.substring(dd.indexOf("=") + 1);
        if (str.length > 6 && str.length < 15) {
            var src = "http://www.youtube.com/embed/" + str;
            chatAreaContent = chatAreaContent.replace(dd, src);
        }
    }
    chatAreaContent = chatAreaContent.replaceAll("<br>", "<br/>");
    //loại bỏ thẻ br.
    if (chatAreaContent.lastIndexOf("<br>") != -1 && chatAreaContent.lastIndexOf("<br>") == chatAreaContent.length - 4)
        chatAreaContent = chatAreaContent.substring(0, chatAreaContent.length - 4);
    if (chatAreaContent.lastIndexOf("<br/>") != -1 && chatAreaContent.lastIndexOf("<br/>") == chatAreaContent.length - 5)
        chatAreaContent = chatAreaContent.substring(0, chatAreaContent.length - 5);
    if (chatAreaContent.lastIndexOf("<br>") != -1 && chatAreaContent.lastIndexOf("<br>") == chatAreaContent.length - 4)
        chatAreaContent = chatAreaContent.substring(0, chatAreaContent.length - 4);
    if (chatAreaContent.lastIndexOf("<br/>") != -1 && chatAreaContent.lastIndexOf("<br/>") == chatAreaContent.length - 5)
        chatAreaContent = chatAreaContent.substring(0, chatAreaContent.length - 5);
    if (chatAreaContent.lastIndexOf("<br>") != -1 && chatAreaContent.lastIndexOf("<br>") == chatAreaContent.length - 4)
        chatAreaContent = chatAreaContent.substring(0, chatAreaContent.length - 4);
    if (chatAreaContent.lastIndexOf("<br/>") != -1 && chatAreaContent.lastIndexOf("<br/>") == chatAreaContent.length - 5)
        chatAreaContent = chatAreaContent.substring(0, chatAreaContent.length - 5);
    // chatAreaContent.replaceAll("<br>","<br/>");
    // alert(chatAreaContent);
    return chatAreaContent;
}

function reVertImg(str)// chuyển icon thành ký tự
{
    var arr = getIcon();
    var arr_img = getIconImg();
    str = str.replaceAll("<IMG", "<img");
    var arr_icon = str.split("<img");
    if (arr_icon.length > 1)// co icon cần đổi lại thành ký tự
    {
        var html = "";
        for (i = 0; i < arr_icon.length; i++) {
            if (arr_icon[i].indexOf("jsmobile/emoticon/") != -1)// có icon cần đổi
            {
                var vt = arr_icon[i].indexOf('>');
                if (vt != -1) {
                    var str = arr_icon[i].substr(0, vt + 1);
                    for (j = arr.length - 1; j >= 0; j--) {
                        if (arr_icon[i].indexOf(arr_img[j]) != -1) {
                            // thay đổi ký tự đặc biệt
                            var tmp = arr[j].replaceAll("<", "&lt;");
                            tmp = tmp.replaceAll(">", "&gt;");
                            arr_icon[i] = arr_icon[i].replace(str, tmp);
                            break;
                        }
                    }
                }
            }
            html += arr_icon[i];
        }
        str = html;
    }
    return str;
}

function convertImg(str) {
    if (typeof str != "string")
        if (Object.prototype.toString.call(str) === '[object Array]')
            str = str.join("");
    //return str;
    //B-)
    var arr = getIcon();
    var arr_img = getIconImg();
    for (var i = 0; i < arr.length; i++) {
        //':-*'
        str = str.replaceAll("&gt;", ">");
        str = str.replaceAll("&lt;", "<");
        //if (arr[i] == str) {
        //str = "<img src='https://" + readCookie('ipServer') + "/mail/jsmobile/emoticon/" + arr_img[i] + "' />";
        // break;
        // }
        str = str.replaceAll(arr[i], "<img src='https://" + readCookie('ipServer') + "/mail/jsmobile/emoticon/" + arr_img[i] + "' />");
        //str = str.replaceAll(arr[i], "<img src='https://" + readCookie('ipServer') + "/mail/jsmobile/emoticon/" + arr_img[i] + "' />");
        //str = str.replaceAll(arr[i].replaceAll("<", "&lt;").replaceAll(">", "&gt;"), "<img src='https://" + readCookie('ipServer') + "/mail/jsmobile/emoticon/" + arr_img[i] + "' />");
        //str = str.replaceAll(arr[i].replaceAll("<", "&lt;").replaceAll(">", "&gt;").toLowerCase(), "<img src='https://" + readCookie('ipServer') + "/mail/jsmobile/emoticon/" + arr_img[i] + "' />");
    }
    //alert(str);
    return str;
}

function getIconImg() {
    var arr = ["11.gif", "12.gif", "13.gif", "14.gif", "21.gif", "22.gif", "23.gif", "24.gif", "31.gif", "32.gif", "33.gif", "34.gif", "41.gif", "42.gif", "43.gif", "44.gif"];
    return arr;
}

/**
 *  sign[0] = [':-*', '=((', ':-O', "X("];
 sign[1] = [':))','#:-S', ':-S',  ":(("];
 sign[2] = ['/:)', ':(', ':P', "=))"];
 sign[3] = [':)', ':|', ':>', "B-)"];
 *
 */
function getIcon() {
    var arr = [':-*', '=((', ':-O', "X(", ':))', '#:-S', ':-S', ":((", '/:)', ':(', ':P', "=))", ':)', ':|', ':>', "B-)"];
    return arr;
}

function removeTag(text) {
    var arr = text.split("<");
    var rt = arr[0];
    for (var i = 1; i < arr.length; i++) {
        var txt = arr[i].substring(arr[i].lastIndexOf(">"));
        rt += txt;
    }
    rt = rt.replaceAll(">", "");
    return rt;
}

function regMess(text, ts) {
    text = convertImg(text);
    if (text == "")
        return "";
    //-----------------------------Youtube------------------------------
    if (typeof text != 'string')
        return text;
    text = text.replace("http://youtu.be/", "http://www.youtube.com/embed/");
    if (text.indexOf("http://www.youtube.com/watch") != -1) {
        var dd, text1;
        text1 = text.substring(text.indexOf("http://www.youtube.com/watch"));
        if (text1.indexOf(" ") != -1)
            dd = text1.substring(0, text1.indexOf(" "));
        else
            dd = text1;
        if (dd.indexOf("&") != -1)
            var str = dd.substring(dd.indexOf("=") + 1, dd.indexOf("&"));
        else
            var str = dd.substring(dd.indexOf("=") + 1);
        if (str.length > 6 && str.length < 15) {
            var src = "http://www.youtube.com/embed/" + str;
            text = text.replace(dd, '<iframe width="420" height="315" src="' + src + '" frameborder="0" allowfullscreen></iframe>');
        }
    } else if (text.indexOf("http://www.youtube.com/embed") != -1) {
        var dd, text1;
        text1 = text.substring(text.indexOf("http://www.youtube.com/"));
        if (text1.indexOf(" ") != -1)
            dd = text1.substring(0, text1.indexOf(" "));
        else
            dd = text1;
        var str = dd.substring(dd.indexOf("ed/") + 3);
        if (str.length > 6 && str.length < 15)
            text = text.replace(dd, '<iframe width="420" height="315" src="' + dd + '" frameborder="0" allowfullscreen></iframe>');
    }
    //---------------------------------------------end Youtube---------------------------------------------------
    //---------------------------------------------all link------------------------------------------------------
    arr = text.split("http");
    var isA = false;
    // ko có thẻ A
    for (i = 1; i < arr.length; i++) {
        arr[i] = "http" + arr[i];
        var isLink = true;
        //alert(arr[i-1]);
        if (arr[i - 1].lastIndexOf("<a ") != -1 && arr[i - 1].lastIndexOf("<a ") > arr[i - 1].lastIndexOf("</a"))// có thẻ a mở, không có đóng
            isA = true;
        if (arr[i - 1].length > 5) {
            var str = arr[i - 1].substring(arr[i - 1].length - 5);
            if (str.indexOf("=") != -1)// link chưa nằm trong thẻ nào
                isLink = false;
        }
        if (isLink) {
            // tìm link
            if (arr[i].indexOf(" ") != -1)
                var lk = arr[i].substring(0, arr[i].indexOf(" "));
            else
                var lk = arr[i];
            // loại dấu "." cuối cùng nếu có
            if (lk.lastIndexOf(".") == lk.length)
                lk = lk.substring(0, lk.length);
            // kiểm tra hợp lệ của link
            if (lk.indexOf(".") > 0 && lk.indexOf("://") != -1) {
                // kiểm tra link có phải là tên của liên kết hay không
                // trường hợp link là tên của liên kết.
                // fix lại link
                if (lk.lastIndexOf("<") != -1)
                    lk = lk.substring(0, lk.lastIndexOf("<"));
                // replace Link
                if (!isA)
                    arr[i] = arr[i].replace(lk, '<a target="_blank" href="' + lk + '">' + lk + '</a>');
            }
        }
        if (arr[i].indexOf("</a") != -1)// có thẻ a đóng
            isA = false;
    }
    text = arr[0];
    for (i = 1; i < arr.length; i++) {
        text += arr[i];
    }
    return text;
}

String.prototype.replaceAll = function (strTarget, strSubString) {
    var strText = this;
    var intIndexOfMatch = strText.indexOf(strTarget);
    while (intIndexOfMatch != -1) {
        strText = strText.replace(strTarget, strSubString);
        intIndexOfMatch = strText.indexOf(strTarget);
    }
    return (strText);
};

function setCaret(editable, caret) {
    //alert($("#Content").html());
    //alert(caret[0].caretPos);
    //alert(caret[0].anchorNode);
    var el = document.getElementById(editable);
    var range = document.createRange();
    var sel = window.getSelection();
    if (el.childNodes[0]) {
        var x = caret[0].caretPos;
        var anchorNode = caret[0].anchorNode;
        if (anchorNode && anchorNode.id == editable) {
            range.setStart(el, x);
            range.collapse(true);
            sel.removeAllRanges();
            sel.addRange(range);
            el.focus();
        } else {
            for (var i = 0; i < el.childNodes.length; i++) {
                if (el.childNodes[i] == anchorNode) {
                    range.setStart(el.childNodes[i], x);
                    range.collapse(true);
                    sel.removeAllRanges();
                    sel.addRange(range);
                    el.focus();
                }
            }
        }

        return true;
    }
    else {
        return false;
    }
}

function getCaretPosition(editableDiv) {
    var caretPos = 0, containerEl = null, sel, range;
    var anchorNode = null;

    var result = [];
    if (window.getSelection) {
        sel = window.getSelection();
        if (sel.rangeCount) {
            range = sel.getRangeAt(0);
            anchorNode = sel.anchorNode;
            if (range.commonAncestorContainer.parentNode.id == "mailBody") {
                caretPos = range.endOffset;
            } else if (range.commonAncestorContainer.parentNode.id == "MailContent") {
                caretPos = range.endOffset;
            } else if (range.commonAncestorContainer.parentNode == editableDiv) {
                caretPos = range.endOffset;
            }
        }
    } else if (document.selection && document.selection.createRange) {
        range = document.selection.createRange();
        if (range.parentElement() == editableDiv) {
            var tempEl = document.createElement("span");
            editableDiv.insertBefore(tempEl, editableDiv.firstChild);
            var tempRange = range.duplicate();
            tempRange.moveToElementText(tempEl);
            tempRange.setEndPoint("EndToEnd", range);
            caretPos = tempRange.text.length;
        }
    }

    result.push({
        caretPos: caretPos,
        anchorNode: anchorNode
    });
    return result;
}

function showReconnect(http) {
    var kt = false;
    if (http.readyState == 4 && http.status == 0)
        kt = true;

    if (kt) {

    }
}

