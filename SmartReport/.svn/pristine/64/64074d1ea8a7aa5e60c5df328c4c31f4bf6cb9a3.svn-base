/*
 * ChromeNativeApp V1.0.2016.0801
 * - Tương thích với BkaveGov_FFPlugin_ChromNativeApp-1.0.0+.exe
 * - Tương thích với BkaveGovExtension-4.6+
 *  
 * Lịch sử:
 * 
 * ChromeNativeApp V1.0.0
 * - Tương thích với BkaveGov_FFPlugin_ChromNativeApp-1.0.0.exe 
 * - Tương thích với BkaveGovExtension-4.6+
 * 
 * Sử dụng:
 *  var plugin = PluginFactory.getInstance();
 *  var isInstalled = plugin.isPluginExist();
 */
function PluginFactory() {

}

PluginFactory.getInstance = function () {
    if (navigator.isMobile) {
        return BChromeCAPlugin.getInstance();
    }
    else if (PluginFactory.isChrome())
        return ChromeNativeApp.getInstance();
    else
        return null; // FirefoxPlugin.getInstance();
};

PluginFactory.isChrome = function () {
    if (navigator.userAgent.indexOf("Chrome") != -1)
        return true;
    return false;
};