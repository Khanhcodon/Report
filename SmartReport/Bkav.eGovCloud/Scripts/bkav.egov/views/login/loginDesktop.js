window.isDesktopApp = false;
try {
    isDesktopApp = typeof window.external.CB_GetLoginInfo == "function";
} catch (e) {
    console.log(e);
}
if (isDesktopApp) {

    console.log(window.external.CB_GetLoginInfo);
    window.external.CB_GetLoginInfo(function (user, password) {
        console.log(window.external.CB_GetLoginInfo);
        console.log(password);
        window.isDesktopLogin = true;

    });
}
