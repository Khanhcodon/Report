//$(function () {
//    if (window.isCamResource) {
//        $("body, html").css("fontFamily", '"Khmer UI", sans-serif');
//        //$("body, html", window.parent.document).css("fontFamily", '"Khmer UI", sans-serif');
//    }
//})

jQuery.fn['bindResources'] = function (callback, isNotRemoveDataRes) {
    isNotRemoveDataRes = true;
    var labels = this.find("*[data-res]");
    var titles = this.find("*[data-restitle]");
    var placeHolders = this.find("*[data-respholder]");

    if (labels.length > 0 || titles.length > 0 || placeHolders.length > 0) {
        $.each(labels, function (i, ele) {
            var res = $(ele).attr("data-res");
            try {
                if (eval(res)) {
                    if (!isNotRemoveDataRes) {
                        $(ele).removeAttr("data-res");
                    }
                    if ($(ele).prop("tagName").toLowerCase() === "input") {
                        var eleVal = $(ele).val().trim();
                        $(ele).val(eval(res));
                    }
                    else {
                        var eleVal = $(ele).text().trim();
                        $(ele).text(eval(res));
                    }
                }
                else {
                    // console.log(res);
                }
            }
            catch (e) {
                var texts = JSON.stringify(res).split(".");
                texts = texts[texts.length - 2] + "." + texts[texts.length - 1];
                if (!isNotRemoveDataRes) {
                    $(ele).removeAttr("data-res");
                }
                if ($(ele).prop("tagName").toLowerCase() === "input") {
                    $(ele).val(texts);
                }
                else {
                    $(ele).text(texts);
                }
                // console.log(res);
            }

        });
        $.each(titles, function (i, ele) {
            var res = $(ele).attr("data-restitle");
            try {
                if (eval(res)) {
                    if (!isNotRemoveDataRes) {
                        $(ele).removeAttr("data-restitle");
                    }
                    $(ele).attr("title", eval(res));
                }
                else {
                    //console.log(res);
                }
            }
            catch (e) {
                var texts = JSON.stringify(res).split(".");
                texts = texts[texts.length - 2] + "." + texts[texts.length - 1];
                if (!isNotRemoveDataRes) {
                    $(ele).removeAttr("data-restitle");
                }
                $(ele).attr("title", texts);
                //console.log(res);
            }

        });
        $.each(placeHolders, function (i, ele) {
            var res = $(ele).attr("data-respholder");
            try {
                if (eval(res)) {
                    if (!isNotRemoveDataRes) {
                        $(ele).removeAttr("data-respholder");
                    }
                    $(ele).attr("placeholder", eval(res));
                }
                else {
                   // console.log(res);
                }
            }
            catch (e) {
                var texts = JSON.stringify(res).split(".");
                texts = texts[texts.length - 2] + "." + texts[texts.length - 1];
                if (!isNotRemoveDataRes) {
                    $(ele).removeAttr("data-respholder");
                }
                $(ele).attr("placeholder", texts);
               // console.log(res);
            }
        });
    }

    if (typeof callback === "function") {
        callback();
    }

    return this;
};

window.getResource = function (resourceKey) {
    /// <summary>
    /// Trả về ResourceValue theo Key, nếu không tồn tại, trả về ResourceKey
    /// </summary>
    /// <param name="resourceKey"></param>
    try {
        return eval(resourceKey);
    }
    catch (e) {
        return resourceKey;
    }
}

var extend = function (destination, source) {
    /// <summary>
    /// Hàm extend để thêm resource
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="source"></param>
    for (var property in source) {
        if (source[property] && source[property].constructor &&
         source[property].constructor === Object) {
            destination[property] = destination[property] || {};
            arguments.callee(destination[property], source[property]);
        } else {
            destination[property] = source[property];
        }
    }
    return destination;
};

// random 1 giá trị để ngẫu nhiên cho colorcode, mỗi lần tải file
// nó sẽ mang 1 giá trị, đảm bảo tính ngẫu nhiên nhưng vẫn đồng nhất
var randomInt = Math.floor(Math.random() * 10);

function getColorCode(char) {
    if (char) {
        return (char.toUpperCase().charCodeAt(0) + randomInt) % 10;
    }
    else {
        return randomInt % 10;
    }
}

function getDefaultAvatar() {
    if (egov.mobile) {
        title = egov.mobile.avatarTheme;
    }
    switch (title) {
        case "troll":
            return String.format(getResource("egov.resources.avatar.troll"), Math.floor(Math.random() * 6) + 1);
        case "icon":
            return String.format(getResource("egov.resources.avatar.icon"), Math.floor(Math.random() * 2) + 1);
        case "alphabet":
            return String.format(getResource("egov.resources.avatar.alphabet"), account[0].toLowerCase());
        default:
            return getResource("egov.resources.avatar.noData");
    }
}

function getUserAvatar(useRealAvatar, username) {
    if (useRealAvatar) {
        return String.format(egov.resources.avatar.path, username);
    }
    return getDefaultAvatar();
}
function getErrorAvatar() {
    return getResource("egov.resources.avatar.errorUrl");
}
