var maximumUserInput = 4;
var inputedAccount = getInputAcc();

function getInputAcc() {
    if (localStorage) {
        var result = localStorage.getItem("inputedAccount");
        if (result) {
            return JSON.parse(result);
        }
    }

    return [];
}

function saveInputAcc(inputedAccount) {
    if (localStorage) {
        localStorage.setItem("inputedAccount", JSON.stringify(inputedAccount));
    }
}

function removeInputAcc() {
    if (localStorage) {
        localStorage.removeItem("inputedAccount");
    }
}

(function () {

    try {
        loginDesktop();
    }
    catch (ex) { }

    $("#Username").focus();
    $("#login").click(function () {
        login();
        return false;
    });

    $("form").submit(function () {

        return false;
    });
})
();

function loginDesktop() {

    if (typeof window.external.CB_GetLoginInfo === "function") {
        console.log("Login desktop.");
        //var isLogin = readCookie('isLogin');
        //if (isLogin) {
        var loginInfo = window.external.CB_GetLoginInfo();
        loginInfo = loginInfo.replace("&user=", "");
        loginInfo = loginInfo.replace("password=", "");
        var arrInfo = loginInfo.split("&");
        var user = arrInfo[0];
        var pass = arrInfo[1];

        $('#Username').val(user);
        if (!pass) {
            return;
        }
        $("#Password").val(pass);
        $('#RememberMe').prop('checked', true);
        var href = "https://" + location.host + "?egov=1&client=tool";

        var username = $('#Username').val();
        var password = $('#Password').val();
        var autoLogin = function () {
            loginNormal($('form:first').attr('action'),
                username,
                password,
                $('#RememberMe').prop('checked'),
                href
            );
        }

        var countTime = 5;
        var countDiv = $("#expireLoginTime"), loginWarning = $("#auto-login-desktop");
        loginWarning.removeClass("hidden");
        countDiv.text(countTime);

        var countInterval = window.setInterval(function () {
            countTime--;
            countDiv.text(countTime);
            if (countTime == 0) {
                autoLogin();
                loginWarning.addClass("hidden");
                clearInterval(countInterval);
            }
        }, 1000);

        $(document).one("keypress", function () {
            loginWarning.addClass("hidden");
            clearInterval(countInterval);
        });
    }
}

function validField() {
    var result = true;
    var username = $("#Username");
    var spanUsername = username.parents(".field").find(".field-validation-valid");
    var password = $("#Password");
    var spanPassword = password.parents(".field").find(".field-validation-valid");
    if (username.val() == '') {
        spanUsername.html("<span>Bạn chưa nhập tài khoản.</span>");
        spanUsername.add("class", "field-validation-error");
        username.addClass("input-validation-error");
        result = false;
    } else {
        spanUsername.html("");
        spanUsername.attr("class", "field-validation-valid");
        username.addClass("valid");
    }
    if (password.val() == '') {
        spanPassword.html("<span>Bạn chưa nhập mật khẩu.</span>");
        spanPassword.attr("class", "field-validation-error");
        password.addClass("input-validation-error");
        result = false;
    } else {
        spanPassword.html("");
        spanPassword.attr("class", "field-validation-valid");
        password.addClass("valid");
    }
    $('input.input-validation-error').first().focus();
    return result;
}

function showLoading(show) {
    if (show) {
        $("#loginProcess").show();
    } else {
        $("#loginProcess").hide();
    }
}

function login() {
    if (!validField()) {
        return false;
    }

    $("#error").empty();
    showLoading(true);

    var username = $("#Username").val();
    var password = $("#Password").val();

    var exist = inputedAccount.filter(function (v) {
        return v.account == username;
    })[0];

    if (!exist) {
        inputedAccount.push({ account: username, lastDate: new Date() });
    }
    else {
        exist.lastDate = new Date();
    }

    var getSsoTokenSuccess = function (result) {
        if (result == undefined || !result) {
            return;
        }

        // Đăng nhập không thành công
        if (!result.Success || result.Token == '') {
            showLoading(false);
            var enableCaptcha = false;
            if (maximumUserInput > 0) {
                inputedAccount = inputedAccount.filter(function (v) {
                    /// <summary>
                    /// Chỉ lưu tài khoản của người dùng trong 30s
                    /// </summary>
                    /// <param name="v"></param>
                    return ((new Date()) - new Date(v.lastDate)) / 1000 < 30;
                });
                saveInputAcc(inputedAccount);
                enableCaptcha = inputedAccount.length > maximumUserInput;
            }

            if (enableCaptcha) {
                $("#error").hide();
                $("#captchaWrap").show();
                $("#captcha a").click();
            }
            else {
                $("#error").text(result.error || result.Message).show();
            }
            return;
        }

        if (result.Success) {
            loginExteralMail(username, password);
            return;
        }

        var remember = false;
        if ($(".login").find("input[type='checkbox']").length) {
            remember = $("#RememberMe").attr("checked") == "checked";
        }

        // Token tạm SSO trả về, post token này lên SSO để lấy token chính
        var tempToken = result.Token;
        $.get
        (
            ssoDomain + '/User/LoginToken?callback=?',
            {
                token: tempToken,
                domain: '',
                remember: remember
            },
            function (ssodata) {
                var isLoginSuccess = ssodata.LoginTokenResult.Status == 'SUCCESS';

                if (!isLoginSuccess) {
                    showLoading(false);
                    $("#error").text('Đăng nhập không thành công!').show();
                    return;
                }
                setDesktopInfo(username, password, remember);
                loginExteralMail(username, password);
            },
            'jsonp'
        )
        .fail(function (xhr) {
            console.log("Login token");
            console.log(xhr);
        });
    }

    $.ajaxSetup({ async: false });
    $.post
    (
        '/Account/LoginSso', //'/Account/LoginNormal',
        {
            username: username,
            password: password,
            UseCaptcha: $("#captchaWrap").is(":visible"),
            CaptchaDeText: $("#CaptchaDeText").val(),
            CaptchaInputText: $("#CaptchaInputText").val()
        },
        getSsoTokenSuccess
    )
    .fail(function (xhr) {
        console.log("Login normal");
        console.log(xhr);
    });
    $.ajaxSetup({ async: true });

}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "/" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function loginExteralMail(username, password) {
    if (loginOtherSystemWithForm.enable) {
        //Delay time để submit form đăng nhập khác
        loginExternalSystem(username, password, function () {
            reloadPage();
        });
    }
    else {
        reloadPage();
    }
}

function reloadPage() {
    window.document.location.href = returnUrl;
}

function setDesktopInfo(username, password, remember) {
    if (typeof window.external.CB_LoginSuccess == "function") {
        var pass = !remember ? "" : password;
        var data = [];
        var loginObj = {
            user: username,
            password: !remember ? "" : password,
            remember: "true"
        }

        data.push(loginObj);

        var myJsonString = JSON.stringify(data);
        window.external.CB_LoginSuccess(myJsonString);
    }
}