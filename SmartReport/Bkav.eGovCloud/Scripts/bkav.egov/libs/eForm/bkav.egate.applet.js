(function (f, $, JSON, undefined) {
    var composeAppletIE = function (config) {
        var tmpl = ''
            + '<object id="' + config.id + '" name="' + config.id + '" '
            + 'width="1px" height="1px" tabIndex="-1" '
            + 'classid="clsid:8AD9C840-044E-11D1-B3E9-00805F499D93" '
            + 'codebase="https://java.sun.com/update/1.6.0/jinstall-6u25-windows-i586.cab#Version=1,6,0" '
            + 'type="application/x-java-applet">'
            + '<param name="codebase" value="' + config.codeBase + '" />'
            + '<param name="archive" value="' + config.archive + '" />'
            + '<param name="code" value="' + config.code + '" />'
            + '<param name="cache_archive" value="' + config.archive + '" />'
            + '<param name="mayscript" value="true"/>'
            + '<param name="scriptable" value="true"/>'
            //+ '<param name="doEvil" value="true"/>'
            + '</object>'
        return tmpl;
    };

    var composeAppletNonIE = function (config) {
        var applet = $("<object>", {
            id: config.id,
            name: config.id,
            width: "1",
            height: "1",
            tabindex: "-1",
            classid: "java:" + config.code,
            type: "application/x-java-applet",
            archive: config.archive
        })
        .append($("<param/>", { name: "codeBase", value: config.codeBase }))
        .append($("<param/>", { name: "archive", value: config.archive }))
        .append($("<param/>", { name: "code", value: config.code }))
        .append($("<param/>", { name: "cache_archive", value: config.archive }))
        .append($("<param/>", { name: "scriptable", value: "true" }))
        .append($("<param/>", { name: "mayscript", value: "true" }));
        //.append($("<param/>", { name: "doEvil", value: "true" }));
        return applet;
    };

    if (typeof f.utils == 'undefined') {
        f.utils = {};
    };

    f.utils.loadApplet = function (usrConfig) {
        var cfg = {
            variableName: "$docSigner",
            placeHolderId: "appletPlaceHolder",
            eventName: "appletLoaded",
            id: "BkavTvanSigner",
            code: "bkav.tvan.signing.applet.DocumentSignerApplet.class",
            codeBase: "/Content/applet/",
            archives: ["docSigner3.jar", "lib/bcprov-jdk16-146.jar", "lib/itextpdf-5.1.2.jar", "lib/openxml4j_beta_v538.jar", "lib/log4j-1.2.16.jar", "lib/jaxen-1.1.1.jar", "lib/dom4j-1.6.1.jar"]
        };
        $.extend(cfg, usrConfig);

        if (typeof window[cfg.variableName] != 'undefined') {
            $(document).triggerHandler(cfg.eventName, [window[cfg.variableName]]);
        }
        else {
            if (typeof cfg.archive == 'undefined') {
                cfg.archive = cfg.archives.join(",");
            }

            window.onAppletLoaded = function (applet) {
                window[cfg.variableName] = applet;
                $(document).triggerHandler(cfg.eventName, [applet]);
            };
            if ($.browser.msie) {
                document.getElementById(cfg.placeHolderId).innerHTML = composeAppletIE(cfg);
            }
            else {
                $("#" + cfg.placeHolderId).append(composeAppletNonIE(cfg));
            }
        }
    }
})(window.f = window.f || {}, jQuery, JSON)