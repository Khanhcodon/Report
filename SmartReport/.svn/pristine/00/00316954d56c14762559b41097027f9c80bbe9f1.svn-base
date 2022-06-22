function browserDetect() {
}

/**
* Begin detect
*/
browserDetect.prototype.detect = function() {
    //alert(navigator.userAgent);
    this.browser	= this.searchString(this.dataBrowser) || "Unsupport browser";
    this.version	= this.searchVersion(navigator.userAgent)
    || this.searchVersion(navigator.appVersion)
    || "Unsupport version";
    this.OS	= this.searchString(this.dataOS) || "Unsupport OS";
};

/**
* return the browser
*/
browserDetect.prototype.getBrowser = function() {
    return this.browser;
};

/**
* return the version of browser
*/
browserDetect.prototype.getVersion = function() {
    return this.version;
};
/**
 * 32 bit or 64 bit
 */
browserDetect.prototype.getTypeOs = function() {
    var userAgent = navigator.userAgent;
    var type = "32bit";
    if(userAgent.indexOf("WOW64") != -1 || userAgent.indexOf("Linux x86_64") != -1 )
    {
    	type = "64bit";
    }
    return type;
};
/**
* return the OS's name
*/
browserDetect.prototype.getOS = function() {
    return this.OS;
};
browserDetect.prototype.searchString = function(data) {
    for (var i = 0; i < data.length; i++) {
        var dataString	= data[i].string;
        var dataProp	= data[i].prop;
        this.versionSearchString	= data[i].versionSearch || data[i].identity;
        if(dataString) {
            if (dataString.indexOf(data[i].subString) != -1)
                return data[i].identity;
        } else if (dataProp)
            return data[i].identity;
    }
};

browserDetect.prototype.searchVersion = function(dataString) {
    var index	= dataString.indexOf(this.versionSearchString);
    if (index == -1) return;
    return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
};

browserDetect.prototype.dataBrowser = [
{
    string: navigator.userAgent,
    subString: "Chrome",
    identity: "Chrome"
},
{
    string: navigator.userAgent,
    subString: "OmniWeb",
    versionSearch: "OmniWeb/",
    identity: "OmniWeb"
},
{
    string: navigator.vendor,
    subString: "Apple",
    identity: "Safari",
    versionSearch: "Version"
},
{
    prop: window.opera,
    identity: "Opera"
},
{
    string: navigator.vendor,
    subString: "iCab",
    identity: "iCab"
},
{
    string: navigator.vendor,
    subString: "KDE",
    identity: "Konqueror"
},
{
    string: navigator.userAgent,
    subString: "Firefox",
    identity: "Firefox"
},
{
    string: navigator.vendor,
    subString: "Camino",
    identity: "Camino"
},
{
    string: navigator.userAgent,
    subString: "Netscape",
    identity: "Netscape"
},
{
    string: navigator.userAgent,
    subString: "MSIE",
    identity: "Explorer",
    versionSearch: "MSIE"
},
{
    string: navigator.userAgent,
    subString: "Gecko",
    identity: "Mozilla",
    versionSearch: "rv"
},
{
    string: navigator.userAgent,
    subString: "Mozilla",
    identity: "Netscape",
    versionSearch: "Mozilla"
},
{
    prop: window.opera,
    identity: "Opera"
}
];
	
browserDetect.prototype.dataOS = [
{
    string: navigator.userAgent,
    subString: "Windows NT 6.1",
    identity: "Windows7"
},
{
    string: navigator.userAgent,
    subString: "Windows NT 6.2",
    identity: "Windows8"
},
{
    string: navigator.userAgent,
    subString: "Windows NT 6.3",
    identity: "Windows8"
},
{
    string: navigator.userAgent,
    subString: "Windows NT 6.0",
    identity: "Windows Vista"
},
{
    string: navigator.userAgent,
    subString: "Windows NT 5.1",
    identity: "WindowsXP"
},
{
    string: navigator.userAgent,
    subString: "Macintosh",
    identity: "Mac"
},
{
    string: navigator.userAgent,
    subString: "iPhone",
    identity: "iPhone"
},
{
    string: navigator.userAgent,
    subString: "iPad",
    identity: "iPad"
},
{
    string: navigator.userAgent,
    subString: "Android",
    identity: "Android"
},
{
    string: navigator.userAgent,
    subString: "Windows Phone",
    identity: "Windows Phone"
},
{
    string: navigator.userAgent,
    subString: "Ubuntu",
    identity: "Linux"
}
,
{
    string: navigator.userAgent,
    subString: "Linux",
    identity: "Linux"
}
];
