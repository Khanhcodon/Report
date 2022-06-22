function BChromeCAPlugin() {
    this.objectCert = new ObjCertificate();
};

BChromeCAPlugin.version = "1.0.2016.1116";
BChromeCAPlugin.prototype.version = BChromeCAPlugin.version;

//#region Android
function AndroidPlugin() {
}

AndroidPlugin.prototype.SignXML = function (dataInput, objectCert, callback) {
    signAndroid(dataInput, objectCert, DocumentType.XML, callback);
}

AndroidPlugin.prototype.VerifyXML = function (dataInput, callback) {
    verifyAndroid(dataInput, DocumentType.XML, callback);
}

AndroidPlugin.prototype.SignOOXML = function (dataInput, objectCert, callback) {
    signAndroid(dataInput, objectCert, DocumentType.OFFICE, functionCallback);
}

AndroidPlugin.prototype.VerifyOOXML = function (dataInput, callback) {
    verifyAndroid(dataInput, DocumentType.OFFICE, functionCallback);
}

AndroidPlugin.prototype.SignCMS = function (dataInput, objectCert, callback) {
    signAndroid(dataInput, objectCert, DocumentType.CMS, functionCallback);
}

AndroidPlugin.prototype.VerifyCMS = function (dataInput, callback) {
    verifyAndroid(dataInput, DocumentType.CMS, functionCallback);
}

AndroidPlugin.prototype.SignPDF = function (dataInput, objectCert, callback) {
    var dataToSign = convertStringToArrayBufferView(dataInput);
    var pinToSign = convertStringToArrayBufferView(objectCert.PIN);
    var signedBytes = bkavcrypto.signPDF(dataToSign, pinToSign);
    //When sign complete
    var base64String = String.fromCharCode.apply(null, new Uint8Array(signedBytes));
    console.log(base64String);
    callback(base64String);
}

AndroidPlugin.prototype.VerifyPDF = function (dataInput, objectCert, callback) {
    verifyAndroid(dataInput, DocumentType.PDF, functionCallback);
}

AndroidPlugin.prototype.GetCertListByFilter = function (callback) {
    runCallBack(callback({}));
}
//#endregion

//#region IOS
function IOSPlugin() {
}

IOSPlugin.prototype.SignXML = function (dataInput, objectCert, callback) {
    var callInfo = {
        functionname: "SignXML",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.VerifyXML = function (dataInput, callback) {
    var callInfo = {
        functionname: "VerifyXML",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.SignOOXML = function (dataInput, objectCert, callback) {
    var callInfo = {
        functionname: "SignOOXML",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.VerifyOOXML = function (dataInput, callback) {
    var callInfo = {
        functionname: "VerifyOOXML",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);

}

IOSPlugin.prototype.SignCMS = function (dataInput, objectCert, callback) {
    var callInfo = {
        functionname: "SignCMS",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.VerifyCMS = function (dataInput, callback) {
    var callInfo = {
        functionname: "VerifyCMS",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.SignPDF = function (dataInput, objectCert, callback) {
    var callInfo = {
        functionname: "SignPDF",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.VerifyPDF = function (dataInput, objectCert, callback) {
    var callInfo = {
        functionname: "VerifyPDF",
        callback: callback.name,
        args: [dataInput]
    };
    return SendToChrome(callInfo);
}

IOSPlugin.prototype.GetCertListByFilter = function (callback) {
    var callInfo = {
        functionname: "GetCertListByFilter",
        callback: callback.name,
        args: []
    };
    return SendToChrome(callInfo);
}

//#endregion

BChromeCAPlugin.getInstance = function () {
    if (!this._instance) {
        this._instance = new BChromeCAPlugin();
        BChromeCAPlugin.prototype.currentOS = undefined;
        var type = getMobileOperatingSystem();
        switch (type) {
            case 1:
                break;
            case 2:
                BChromeCAPlugin.prototype.currentOS = new AndroidPlugin();
                break;
            case 3:
                BChromeCAPlugin.prototype.currentOS = new IOSPlugin();
                break;
            default:
                break;
        }
    }

    return this._instance;
};

BChromeCAPlugin.prototype.isReady = function (callback) {
    if (typeof callback === "function") {
        callback(true);
    } else {
        return true;
    }
};

BChromeCAPlugin.prototype.writeFileBase64 = function (pathfile, data, mode, callback) {
    runCallBack(callback({ returnCode: 1 }));
};

BChromeCAPlugin.prototype.signFile = function (fileName, dataInput, callback) {
    if (this.currentOS) {
        var extension = getExtension(fileName);
        switch (extension) {
            case "pdf":
                this.currentOS.SignPDF(dataInput, this.objectCert, callback);
                break;
            default:
                this.currentOS.SignOOXML(dataInput, this.objectCert, callback);
                break;
        }
    }
}

BChromeCAPlugin.prototype.verifyFile = function (fileName, dataInput, callback) {
    if (this.currentOS) {
        var extension = getExtension(fileName);
        switch (extension) {
            case "pdf":
                this.currentOS.VerifyPDF(dataInput, callback);
                break;
            default:
                this.currentOS.VerifyOOXML(dataInput, callback);
                break;
        }
    }
}

BChromeCAPlugin.prototype.getCertIndex = function (callback) {
    if (this.currentOS) {
        this.currentOS.GetCertListByFilter(callback);
    }
}

//Object certificate
function ObjCertificate() {
    this.CertificateBase64 = "";
    this.CertificateSerialNumber = "";
    this.OcspUrl = "";
    this.PIN = "";
}

function signAndroid(dataInput, objectCert, docType, callback) {
    if (iCheckPlatform != 2) {
        return;
    }
    var dataToSign = convertStringToArrayBufferView(dataInput);
    var pinToSign = convertStringToArrayBufferView(objectCert.PIN);

    var signedBytes;
    switch (docType) {
        case DocumentType.XML: {
            signedBytes = bkavcrypto.signXML(dataToSign, pinToSign);
            break;
        }
        case DocumentType.PDF: {
            signedBytes = bkavcrypto.signPDF(dataToSign, pinToSign);
            break;
        }
        case DocumentType.OFFICE: {
            signedBytes = bkavcrypto.signOffice(dataToSign, pinToSign);
            break;
        }
        case DocumentType.CMS: {
            signedBytes = bkavcrypto.signCMS(dataToSign, pinToSign);
            break;
        }
        default: {
            signedBytes = null;
        }
    }

    //When sign complete
    signedBytes.then(
		function (result_signature) {
		    var base64Sign = Utf8ArrayToStr(new Uint8Array(result_signature));
		    callback(base64Sign);
		},
		function (e) {
		    console.log("Error: " + e);
		}
	);
}
function verifyAndroid(dataInput, docType, callback) {
    if (iCheckPlatform != 2) {
        return;
    }
    var dataToVerify = convertStringToArrayBufferView(dataInput);

    var verifiedBytes;
    switch (docType) {
        case DocumentType.XML: {
            verifiedBytes = bkavcrypto.verifyXML(dataToVerify);
            break;
        }
        case DocumentType.PDF: {
            verifiedBytes = bkavcrypto.verifyPDF(dataToVerify);
            break;
        }
        case DocumentType.OFFICE: {
            verifiedBytes = bkavcrypto.verifyOffice(dataToVerify);
            break;
        }
        case DocumentType.CMS: {
            verifiedBytes = bkavcrypto.verifyCMS(dataToVerify);
            break;
        }
        default: {
            verifiedBytes = null;
        }
    }

    //When verify complete
    verifiedBytes.then(
		function (_result) {
		    callback(String.fromCharCode.apply(null, new Uint8Array(buf)));
		},
		function (e) {
		    console.log("Error: " + e);
		}
	);
}
// ios
function createIFrame(src) {
    var newFrameElm = document.createElement("IFRAME");
    newFrameElm.setAttribute("src", src);
    document.body.appendChild(newFrameElm);
    return newFrameElm;
}

function SendToChrome(dataToChrome) {
    debugger
    var url = "js-command:";
    url += JSON.stringify(dataToChrome)
    var iFrame = createIFrame(url);
    //remove the frame now
    iFrame.parentNode.removeChild(iFrame);
}

function arrayBufferToString(buf) {
    return String.fromCharCode.apply(null, new Uint8Array(buf));
}

function convertStringToArrayBufferView(str) {
    var bytes = new Uint8Array(str.length);
    for (var iii = 0; iii < str.length; iii++) {
        bytes[iii] = str.charCodeAt(iii);
    }

    return bytes;
}

function getMobileOperatingSystem() {
    var userAgent = navigator.userAgent || navigator.vendor || window.opera;
    // Windows Phone must come first because its UA also contains "Android"
    if (/windows phone/i.test(userAgent)) {
        //return "Windows Phone";
        return 1;
    }
    if (/android/i.test(userAgent)) {
        //return "Android";
        return 2;
    }
    // iOS detection from: http://stackoverflow.com/a/9039885/177710
    if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
        //return "iOS";
        return 3;
    }
    return 4; // unknown
}
function Utf8ArrayToStr(array) {
    var out, i, len, c;
    var char2, char3;

    out = "";
    len = array.length;
    i = 0;
    while (i < len) {
        c = array[i++];
        switch (c >> 4) {
            case 0: case 1: case 2: case 3: case 4: case 5: case 6: case 7:
                // 0xxxxxxx
                out += String.fromCharCode(c);
                break;
            case 12: case 13:
                // 110x xxxx   10xx xxxx
                char2 = array[i++];
                out += String.fromCharCode(((c & 0x1F) << 6) | (char2 & 0x3F));
                break;
            case 14:
                // 1110 xxxx  10xx xxxx  10xx xxxx
                char2 = array[i++];
                char3 = array[i++];
                out += String.fromCharCode(((c & 0x0F) << 12) |
                               ((char2 & 0x3F) << 6) |
                               ((char3 & 0x3F) << 0));
                break;
        }
    }

    return out;
}
function runCallBack(callback) {
    if (typeof callback === "function") {
        callback();
    }
}

function getExtension(fileName) {
    if (fileName) {
        return fileName.contains(".") ? fileName.substr(fileName.lastIndexOf(".") + 1).toLowerCase() : "";
    }
}
