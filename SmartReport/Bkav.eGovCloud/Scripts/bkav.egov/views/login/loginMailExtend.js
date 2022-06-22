localStorage.removeItem("mdaemonUrl");
localStorage.removeItem("exchangeUserContext");
var hadSubmit = false;
function loginToMSExchange(iframe, mailUrl, username, password, callback) {
    if (hadSubmit) {
        return readExchangeInfo(iframe, callback);
    }
    hadSubmit = true;
    if (username.indexOf("@") > 0) {
        username = username.split("@")[0];
    }
    var p = {
        //curl: "Z2F",
        destination: mailUrl,
        flags: 5, forcedownlevel: 0, trusted: 4, isutf8: 1,
        username: username,
        password: password
    };

    var myForm = document.createElement("form");
    myForm.method = "post";
    myForm.action = mailUrl + "/auth.owa";

    for (var k in p) {
        var myInput = document.createElement("input");
        myInput.setAttribute("name", k);
        myInput.setAttribute("value", p[k]);
        myForm.appendChild(myInput);
    }

    iframe.contentDocument.body.appendChild(myForm);

    myForm.submit();
}
var exchangeKey = "UserContext=";//length = 12
function readExchangeInfo(iframe, callback) {
    //if (iframe.contentDocument && iframe.contentDocument.cookie) {
    //    var cookies = iframe.contentDocument.cookie.split(";");
    //    var userContext = cookies.find(function (cookie) {
    //        return cookie.indexOf(exchangeKey) > 0;;
    //    });
    //    userContext = userContext.substring(userContext.indexOf("=") + 1);
    //    localStorage.setItem("exchangeUserContext", userContext);
    //}
    if (typeof callback == "function") {
        callback();
    }
}

function readMdaemonInfo(iframe, mailUrl, callback) {
    try {
        if (!iframe.contentWindow.wcLink) {
            mailUrl = iframe.contentDocument.getElementsByTagName("frame")[0].src;
            mailUrl = mailUrl.replace("&View=Menu", "&View=main");
        }
        else {
            mailUrl = mailUrl + iframe.contentWindow.wcLink + "&View=main";
        }
        localStorage.setItem("mdaemonUrl", mailUrl);
    } catch (e) {
        console.log(e);
    }

    if (typeof callback == "function") {
        callback();
    }
}

function LoginToMdaemon(iframe, mailUrl, username, password, callback) {
    if (hadSubmit) {
        return readMdaemonInfo(iframe, mailUrl, callback);
    }
    hadSubmit = true;
    if (username.indexOf("@") < 0) {
        username += "@" + defaultDomain;
    }
    var loginUrl = mailUrl + "/WorldClient.dll?View=Main";

    var p = {
        User: username,
        Password: password,
        Lang: "",
        Theme: "",
    };

    var myForm = document.createElement("form");
    myForm.style.display = "none";
    myForm.method = "post";
    myForm.action = loginUrl;

    for (var k in p) {
        var myInput = document.createElement("input");
        myInput.setAttribute("name", k);
        myInput.setAttribute("value", p[k]);
        myForm.appendChild(myInput);
    }

    iframe.contentDocument.body.appendChild(myForm);

    myForm.submit();
}

function loginExternalSystem(username, password, callback) {
    var mailUrl = loginOtherSystemWithForm.mailUrl;
    if (loginOtherSystemWithForm.type != mailType.Bmail) {
        var $iframe = $("<iframe class='hidden' />");

        $iframe.load(function () {
            if (loginOtherSystemWithForm.type == mailType.MailExchange) {
                loginToMSExchange(this, mailUrl, username, password, callback);

            } else if (loginOtherSystemWithForm.type == mailType.MDaemon) {
                LoginToMdaemon(this, mailUrl, username, password, callback);

            }
        });
        $("body").append($iframe);
    }
}

