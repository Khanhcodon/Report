/*
	Lưu ý: tất cả các thay đổi ở content.js đều phải đảm bảo chạy được với NativeApp bản cũ trước đó.
	Tránh việc update Extension lên GGStore xong thì không chạy được ở các máy chưa kịp cài lại NativeApp tương ứng.
*/
/*
 * Quy trình trao đổi message giữa js và native app
 * js   -> content.js           call    chrome.extension.sendMessage 
 *      -> background.js        hanlde  chrome.extension.onMessage
 *      -> background.js        call    port.sendMessage                
 *      -> NativeApp            handle  nhận message
 * 
 * nativeApp 
 *      -> NativeApp            call    gửi message
 *      -> background.js        handle  port.onMessage
 *      -> background.js        call    chrome.tabs.sendMessage
 *      -> content.js           handle  chrome.extension.onMessage 
 */

var Egov_Event = {
    GetMd5: "Egov_GetMd5",
    OpenFile: "Egov_OpenFile",
    CloseFile: "Egov_CloseFile",
    WriteFileBase64: "Egov_WriteFileBase64",
    ReadFileBase64: "Egov_ReadFileBase64",
    DeleteFile: "Egov_DeleteFile",
    DeleteFolder: "Egov_DeleteFolder",
	
	GetCertIndex: "Egov_GetCertIndex",
    SignFile: "Egov_SignFile",
	
    ScanFile: "Egov_ScanFile",
    TransferPdf: "Egov_TransferPdf",
    TransferDoc: "Egov_TransferDoc",
    CancelTransferImage: "Egov_CancelTransferImage",
    IsWordInstalled: "Egov_IsWordInstalled",
    IsChangeContent: "Egov_isChangeContent",
    GetAllScanner: "Egov_GetAllScanner",
    ImageCrop: "Egov_ImageCrop",
    ReadFileScanBase64: "Egov_ReadFileScanBase64",
    ImageRotate: "Egov_ImageRotate",
    ImageAdjustBrightness: "Egov_ImageAdjustBrightness",
    ImageAdjustContrast: "Egov_ImageAdjustContrast",
    TransferImage: "Egov_TransferImage",
    Acquire: "Egov_Acquire",
    GetTempFolder: "Egov_GetTempFolder",
    IsExistExtension: "IsExistExtension",
    OpenUrl: "Egov_OpenUrl"
};

/*for (event in Egov_Event) {
	document.addEventListener(Egov_Event[event], function (e) {
		var data = e.detail;
		postToBackground(data);
	}); 
}*/

document.addEventListener("BkaveGovChromeExtension_Request", function (e) {
	var data = e.detail;
	postToBackground(data);
}); 

/* 1. Gửi message sang background.js */
function postToBackground(message) {
    //chrome.extension.sendMessage(message);
	chrome.runtime.sendMessage(message);
}

/* 2. Nhận message từ background.js */
function receiveFromBackground(message) {
	// Sau khi ký file thành công, yêu cầu đọc file vừa ký
	if ( message.action === Egov_Event.SignFile && message.returnCode == 1 && message.base64  === undefined ) {
		var sender = {};
		sender.originSignedMessage = message;	
		var message_read = { 
							"action": Egov_Event.ReadFileBase64, 
							"fileName": message.fileName,
							"sender": sender
						};		
		postToBackground(message_read);
		return;
	}
	
    sendToPage(message);
}

//chrome.extension.onMessage.addListener(function (message, sender, sendResponse) {
chrome.runtime.onMessage.addListener(function (message, sender, sendResponse) {
    receiveFromBackground(message.message);
});


/* 3. Gửi message sang page */
function sendToPage(data) {
    var evt = document.createEvent("CustomEvent");
    //evt.initCustomEvent("Resp_" + data.action, true, true, data);
	evt.initCustomEvent("BkaveGovChromeExtension_Response", true, true, data);
    document.dispatchEvent(evt);
}