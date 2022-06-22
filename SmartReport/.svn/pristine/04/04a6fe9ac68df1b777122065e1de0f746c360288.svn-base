$(function () {

    //#region Private Fields

    var mailType =
    {
        Bmail: 1,
        Gmail: 2,
        Yahoo: 3,
        MailExchange: 4,
        MDaemon: 5
    };

    //#endregion

    var LoginForm = {
        $el: $("body"),
        element: {
            password: $('#Password'),
            userName: $("#Username"),
            remember: $('#RememberMe'),
            forget: $(),
            notMe: $(".otherAccount"),
            submitForm: $('form:first'),
            processBar: $("#loginProcess")
        },

        loginUrl: {
            current: '/account/login',
            notAllowedDevice: '/account/notAllowedDevice'
        },

        // Login.cshtml
        loginOtherEmailSystemConfig: loginOtherEmailSystemConfig,
        returnUrl: returnUrl,
        defaultDomain: defaultDomain,
        maximumUserInput: maximumUserInput,
        limitByMac: limitByMac,

        initialize: function () {
            try {
                // Tự đăng nhập bản desktop
                this._loginDesktopVersion();
            }
            catch (ex) { }

            this._checkMAC();

            this._initEvents();

            this.inputedAccount = getInputAcc();

            var that = this;
            setTimeout(function () {
                // Timeout do đợi thời gian martial xử lý
                if (that.element.userName.is(":hidden")) {
                    that.element.password.focus();
                } else {
                    that.element.userName.focus();
                }
            }, 200);
        },

        reload: function () {
            if (this.returnUrl) {
                document.location.href = this.returnUrl;
                return;
            }

            window.document.location.reload();
        },

        _checkMAC: function () {
            if (!this.limitByMac) {
                return;
            }
            this.checkingMac = true;
            var that = this;

            if (window.Plugin) {
                window.Plugin.appendPlugin(function () {
                    Plugin.getMAC(function (mac) {
                        // that.checkingMac = false;
                        that.MAC = mac;
                    });
                });
            }
        },

        _initEvents: function () {
            var that = this;

            this.$el.keydown(function (e) {
                var code = e.which;
                if (code === 27) // ecs
                {
                    that._useOtherAccount();
                }
                return;
            });

            this.element.password.keypress(function (e) {
                that._checkCaplookIsOn(e);
            });

            this.element.notMe.click(function () {
                that._useOtherAccount();
            });

            this.element.submitForm.submit(function (e) {
                e.preventDefault();
                e.stopPropagation();

                var isValid = that._validForm();

                if (!isValid) {
                    return false;
                }

                that._login();

                return false;
            });
        },

        _validForm: function () {
            var result = true;
            this.element.userName.removeClass("input-validation-error valid");
            this.element.password.removeClass("input-validation-error valid");

            var spanUsername = this.element.userName.parent().find("span");
            var spanPassword = this.element.password.parent().find("span");
            spanUsername.html("");
            spanPassword.html("");

            var username = this.element.userName.val();
            var password = this.element.password.val();

            if (username == '') {
                spanUsername.html("<span>Bạn chưa nhập tài khoản.</span>");
                spanUsername.attr("class", "field-validation-error");
                this.element.userName.addClass("input-validation-error");
                result = false;
            }

            if (password == '') {
                spanPassword.html("<span>Bạn chưa nhập mật khẩu.</span>");
                spanPassword.attr("class", "field-validation-error");
                password.addClass("input-validation-error");
                result = false;
            }

            $('input.input-validation-error').first().focus();
            return result;
        },

        _login: function () {
            var username = this.element.userName.val();
            var password = this.element.password.val();
            var remember = this.element.remember.prop('checked');
            var that = this;

            var url = this.loginUrl.current;

            $.post({
                url: url,
                data: {
                    Username: username,
                    Password: password,
                    RememberMe: remember,
                    UseCaptcha: $("#captchaWrap").is(":visible"),
                    CaptchaDeText: $("#CaptchaDeText").val(),
                    CaptchaInputText: $("#CaptchaInputText").val(),
                    MAC: that.MAC
                },
                beforeSend: function () {
                    $(".mesage-login-error").addClass('mesage-login-error-hide');
                    $("#error").empty();
                    that._processing(true);
                },
                success: function (data) {
                    if (!data) {
                        return;
                    }

                    var isLoginSuccess = data.success;

                    if (isLoginSuccess) {
                        that._setDesktopInfo(username, password, remember);
                        removeInputAcc();

                        if (that.loginOtherEmailSystemConfig && that.loginOtherEmailSystemConfig.enable) {
                            that._loginOtherMailSystem(that.returnUrl, username, password);
                        } else {
                            that.reload();
                        }

                        return;
                    }

                    that._loginFail(data.error, data.deviceNotAllowed);
                },
                error: function () {
                    $(".mesage-login-error")
                        .removeClass('mesage-login-error-hide')
                        .addClass('mesage-login-error-show')
                        .text('Không thể đăng nhập lúc này, Vui lòng đăng nhập lại hoặc báo với quản trị để được xử lý!');
                },
                complete: function () {
                    that._processing(false);
                }
            });
        },

        _getSsoTokenSuccess: function (result) {
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
        },

        _loginFail: function (error, deviceNotAllowed) {
            if (deviceNotAllowed) {
                this._redirectNotAllowedDevice();
                return;
            }

            var enableCaptcha = false;
            if (this.maximumUserInput > 0) {
                // Xử lý đăng nhập sai tài khoản quá số lần cho phép.
                inputedAccount = inputedAccount.filter(function (v) {
                    return ((new Date()) - new Date(v.lastDate)) / 1000 < 30;
                });

                saveInputAcc(inputedAccount);
                enableCaptcha = inputedAccount.length > this.maximumUserInput;
            }

            if (error == "EnableCaptcha" || enableCaptcha) {
                $("#error").hide();
                $("#captchaWrap").show();
                $("#captcha a").click();
            }
            else {
                $("#error").text(error).show();
            }
        },

        _redirectNotAllowedDevice: function () {
            document.location.href = this.loginUrl.notAllowedDevice + "?u=" + this.element.userName.val();
        },

        _checkCaplookIsOn: function (e) {
            var s = String.fromCharCode(e.which);
            if (s.toUpperCase() === s && !e.shiftKey || s.toLowerCase() === s && e.shiftKey) {
                if (e.keyCode > 64 && e.keyCode < 91) {
                    $(".capslockalert").show();
                }
            }
            else {
                $(".capslockalert").hide();
            }
        },

        _useOtherAccount: function () {
            $.cookie("eGovUserInfo", "", { expires: -1 });
            $.cookie("eGovUserInfo", "", { domain: document.domain, path: "/", expires: -1 });

            $.cookie("bkavAuthen", "", { expires: -1 });
            $.cookie("bkavAuthen", "", { domain: document.domain, path: "/", expires: -1 });

            this.reload();
        },

        _processing: function (show) {
            if (show) {
                this.element.processBar.show();
            } else {
                this.element.processBar.hide();
            }
        },

        //#region Intergrated Other

        _loginDesktopVersion: function () {
            if (window.external == undefined || typeof window.external.CB_GetLoginInfo !== "function") {
                return;
            }

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
        },

        _setDesktopInfo: function (username, password, remember) {
            if (!window.external || typeof window.external.CB_LoginSuccess !== "function") {
                return;
            }

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
        },

        _loginOtherMailSystem: function (returnUrl, username, password) {
            //Delay time để submit form đăng nhập khác
            loginExternalSystem(username, password, function () {
                reloadPage(returnUrl);
            });
        },

        loginExternalSystem: function (username, password, callback) {
            var mailUrl = this.loginOtherEmailSystemConfig.mailUrl;
            if (this.loginOtherEmailSystemConfig.type != mailType.Bmail) {
                var $iframe = $("<iframe class='hidden' />");

                $iframe.load(function () {
                    if (this.loginOtherEmailSystemConfig.type == mailType.MailExchange) {
                        loginToMSExchange(this, mailUrl, username, password, callback);

                    } else if (this.loginOtherEmailSystemConfig.type == mailType.MDaemon) {
                        LoginToMdaemon(this, mailUrl, username, password, callback);

                    }
                });
                $("body").append($iframe);
            }
        }

        //#endregion
    };

    //#region Private Func

    /*
     * Trả  về số lần nhập sai tài khoản
     */
    function getInputAcc() {
        if (localStorage) {
            var result = localStorage.getItem("inputedAccount");
            if (result) {
                return JSON.parse(result);
            }
        }

        return [];
    }

    /*
     * Lưu số lần nhập sai tài khoản
     */
    function saveInputAcc(inputedAccount) {
        if (localStorage) {
            localStorage.setItem("inputedAccount", JSON.stringify(inputedAccount));
        }
    }

    /*
     * Xóa cache lưu nhập sai tài khoản nếu đăng nhập thành công
     */
    function removeInputAcc() {
        if (localStorage) {
            localStorage.removeItem("inputedAccount");
        }
    }

    //#endregion

    LoginForm.initialize();
});