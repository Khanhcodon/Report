$(function () {
    var hasExtensionInstalling = false;
    var extensionId = "cngndmpehahbhkdhihanbpobckmkdkkk";
    var step1Element = $(".ins-step1");
    var step2Element = $(".ins-step2");
    var step3Element = $(".ins-step3");

    isPluginExist(function (result) {
        if (result) {
            nextStep(2);
        }
    });

    $("#downloadPlugin").click(function () {
        var pluginLink = "/Content/Plugin/BkaveGov_FFPlugin_ChromNativeApp-1.0.2.zip";
        window.open(pluginLink, "_blank");
        nextStep(2);
    });

    var waitInstallExtensionInterval = setInterval(function () {
        if (hasExtensionInstalling) {
            isPluginExist(function (result) {
                if (result) {
                    nextStep(3);
                    clearInterval(waitInstallExtensionInterval);
                }
            });
        }
    }, 500);

    $("#addExtension").click(function () {
        var extensionLink = "https://chrome.google.com/webstore/detail/" + extensionId;
        isPluginExist(function (result) {
            if (result) {
                nextStep(3);
            } else {
                window.open(extensionLink, "_blank");
                hasExtensionInstalling = true;
            }
        });
    });

    $("#finish").click(function () {
        var r = confirm("Bạn cần kiểm tra các nội dung nhập liệu trược ki tải lại trang!");
        if (r == true) {
            document.location.reload();
        }
    });

    function nextStep(numb) {
        if (numb == 2) {
            showDownloadPlugin();
        } else if (numb == 3) {
            showInstallPlugin();
        }
    }

    function showDownloadPlugin() {
        step2Element.siblings().hide();
        step2Element.show();
    }

    function showInstallPlugin() {
        step3Element.siblings().hide();
        step3Element.show();
    }

    function isPluginExist(callback) {
        
        chrome.runtime.sendMessage(extensionId, { message: "installed" }, function (reply) {
            if (typeof callback == "function") {
                callback(reply);
            }
        });
    };
});