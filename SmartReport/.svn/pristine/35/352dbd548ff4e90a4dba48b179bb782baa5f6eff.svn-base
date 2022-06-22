$(function () {
    $('input[type="text"]').first().focus();

    $("#login").click(function () {
        login();
    });

    $("#Username").keyup(function (e) {
        if (e.keyCode == 13) {
            login();
        }
    });

    $("#Password").keyup(function (e) {
        if (e.keyCode == 13) {
            login();
        }
    });
});

function login() {
    if (validateField()) {
        $("#error").text('').hide();
        showLoading(true);
        var username = $("#Username").val();
        var password = $("#Password").val();
        $.post
        (
            '@Url.Action("LoginViaLdap")',
            {
                username: username,
                password: password
            },
            function (data) {
                if (data.success == true && data.token != '') {
                    var remember = false;
                    if ($(".login").find("input[type='checkbox']").length) {
                        remember = $("#RememberMe").attr("checked") == "checked";
                    }
                    $.get
                    (
                        '@ssoDomain/User/LoginToken?callback=?',
                        {
                            token: data.token,
                            domain: '@ViewBag.Domain',
                            remember: $("#RememberMe").attr("checked") == "checked"
                        },
                        function (ssodata) {
                            if (ssodata.LoginTokenResult.Status == 'DENIED') {
                                showLoading(false);
                                $("#error").text('@Html.Raw(Localizer("User.Login.Failed"))').show();
                            } else {
                                var returnUrl = '@(string.IsNullOrWhiteSpace(Request.QueryString["ReturnUrl"]) ? "/" : Request.QueryString["ReturnUrl"])';
                                var redirect = '@Url.Action("Login", "Account")?ReturnUrl=' + returnUrl + '&remember=' + remember;
                                document.location = redirect;
                            }
                        },
                        'jsonp'
                    )
                    .fail(function (xhr) {
                        console.log("Login token");
                        console.log(xhr);
                    });
                } else {
                    showLoading(false);
                    if (data.message != '') {
                        $("#error").text(data.message).show();
                    } else {
                        $("#error").text('@Html.Raw(Localizer("User.Login.Failed"))').show();
                    }
                }
            },
            'json'
        )
        .fail(function (xhr) {
            console.log("Login Ldap");
            console.log(xhr);
        });
    }
    return false;
}