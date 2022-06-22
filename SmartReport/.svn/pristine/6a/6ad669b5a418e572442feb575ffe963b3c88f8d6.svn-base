/*
	Lưu ý: tất cả các thay đổi ở background.js đều phải đảm bảo chạy được với NativeApp bản cũ trước đó.
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

var port = null,
    // Trạng thái sẵn sàng giống $.ready();
    domReady = false,
    // Tên Native App
    hostName = "com.bkav.egovextension",
	maxLengthOfBase64 = 300000,
	nativeAppVersion = "10000"; // 01.00.00. 
	
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
    IsExistExtension: "IsExistExtension",

    OpenUrl: "Egov_OpenUrl"
};

/* 1. Gửi/Nhận với content.js */

/*
message.sender là object đặc biệt, dùng chứa các thông tin cố định trong quá trình trao đổi và xử lý message từ 
Page -> content -> background -> native app -> background -> content -> Page. Dùng cho các việc:
- Lưu lại id của tab đã gửi message yêu cầu để trả lại kết quả sau khi native app xử lý xong.
- Lưu lại gói message kí gốc dùng để trả về sau khi tự xử lý đọc base64 ở tầng content và backbround.
- Lưu lại eventname của sự kiện chờ handle trên page.
*/

// Nhận message từ content.js
function receiveFromContent(message, sender, sendResponse) {    
	// message.sender sẽ khác undefined khi là yêu cầu đọc nội dung file kí do content tự động gửi xuống.
	message.sender = message.sender || {};
	message.sender.tab = {id: sender.tab.id};
	
	if (message.action === Egov_Event.ReadFileBase64) {
		message.start = 0;
		message.length = maxLengthOfBase64;
	}

	if (message.action === Egov_Event.WriteFileBase64) {
	    message.start = 0;
	    message.length = 10000;
	}

    sendToNativeApp(message);
};

// Gửi message tới content.js
function sendToContent(message) {	
	if (message.sender && message.sender.tab && message.sender.tab.id) {
		chrome.tabs.get(message.sender.tab.id, function(tab) {
			if (tab) {
				chrome.tabs.sendMessage(tab.id, { message: message });	
			}
		});
	} else if (chrome.tabs.query) {	
		// Since Chrome 16.	
		chrome.tabs.query({active: true}, function(tabs){
			if (tabs && tabs.length > 0) {
				chrome.tabs.sendMessage(tabs[0].id, { message: message });	
			}
		});
	} else {
		// Deprecated since Chrome 33. Please use tabs.query {active: true}.
		chrome.tabs.getSelected(null, function (tab) {
			if (tab) {
				chrome.tabs.sendMessage(tab.id, { message: message });	
			}
		});	
	}
}

/* 2. Gửi/nhận với NativeApp */
function readFileBase64() {
	
}

var final_base64 = {};
// Nhận message từ NativeApp
function receiveFromNativeApp(message) {	
	// Nếu là trả về 1 phần nội dung file => Thực hiện lấy phần tiếp theo.
	/* 		{
				"action": "Egov_ReadFileBase64",
				"base64": base64,
				"returnCode": returnCode,
				"returnMessage": returnMessage,
				"length": length,
				"md5": md5
			}";
	*/
	if (message.action === Egov_Event.ReadFileBase64 && message.returnCode == 1 
		// Điều kiện để kiểm tra phiên bản NativeApp đang cài hỗ trợ xử lý lấy file theo từng phần nhỏ, tránh lỗi khi đcọ file lớn làm tràn dung lượng được phép của 1 gói tin trên chrome.
		&& message.length !== undefined) {
		var md5 = message.md5;
		final_base64[md5] = final_base64[md5] || "";
		final_base64[md5] = final_base64[md5] + message.base64;
		
		if (final_base64[message.md5].length < message.length) {
			// Nếu chưa đọc hết nội dung file => Tiếp tục đọc
			var message_read = { 
				"action":  Egov_Event.ReadFileBase64, 
				"fileName": message.fileName, 
				"start": final_base64[md5].length, 
				"length": maxLengthOfBase64,
				"sender": message.sender || {}
			};
			// Send lại sang NativeApp để lấy nội dung Base64
			sendToNativeApp(message_read);
		} else {
			// Nếu đã đọc hết nội dung file
			// Nếu là đang đọc file vừa kí 
			var message_result;
			if (message.sender && message.sender.originSignedMessage) {
				message_result = message.sender.originSignedMessage;
			} else {
				message_result = message;
			}
			message_result.base64 = final_base64[md5];
			message_result.md5 = message.md5;
			
			// Gửi về content file vừa kí kèm base64 hoặc file yêu cầu đọc base64.
			sendToContent(message_result);
			delete final_base64[md5];
		}
		return;
	}
	
	if (message.action === Egov_Event.WriteFileBase64 && message.returnCode == 1
		&& message.length !== undefined) {
	    var md5 = message.md5;
	    final_base64[md5] = final_base64[md5] || "";
	    final_base64[md5] = final_base64[md5] + message.base64;

	    if (final_base64[message.md5].length < message.length) {
	        // Nếu chưa đọc hết nội dung file => Tiếp tục đọc
	        var message_read = {
	            "action": Egov_Event.ReadFileBase64,
	            "fileName": message.fileName,
	            "start": final_base64[md5].length,
	            "length": maxLengthOfBase64,
	            "sender": message.sender || {}
	        };
	        // Send lại sang NativeApp để lấy nội dung Base64
	        sendToNativeApp(message_read);
	    } else {
	        // Nếu đã đọc hết nội dung file
	        // Nếu là đang đọc file vừa kí 
	        var message_result;
	        if (message.sender && message.sender.originSignedMessage) {
	            message_result = message.sender.originSignedMessage;
	        } else {
	            message_result = message;
	        }
	        message_result.base64 = final_base64[md5];
	        message_result.md5 = message.md5;

	        // Gửi về content file vừa kí kèm base64 hoặc file yêu cầu đọc base64.
	        sendToContent(message_result);
	        delete final_base64[md5];
	    }
	    return;
	}

	// Các trường hợp khác trả về content như bình thường.
    sendToContent(message);
}

// Gửi message tới NativeApp
function sendToNativeApp(message) {
    if (!port) {
		// Thử reconnect lại native app. Nếu vẫn không được thì trả về content.js trạng thái không connect được.
		connectNativeHost();
		if (!port) {
			sendToContent({returnCode: -1, returnMessage: "Không connect được tới Native Host. Có thể do chưa cài đặt Bkav eGov Plugin (Egov Extension.msi)!"});
		}
        return;
    }	
	// Nếu là yêu cầu đọc nội dung file => Thực hiện lấy theo từng phần.
	
    port.postMessage(message);
}

// Disconected với NativeApp
function onDisconnected() {
    console.log("Failed to connectNativeHost: " + chrome.runtime.lastError.message);
    port = null;
}

/* 4. Connect NativeApp */

// Connect
function connectNativeHost() {
    if (!domReady || port) {
        return;
    }

    port = chrome.runtime.connectNative(hostName);
    port.onMessage.addListener(receiveFromNativeApp);
    port.onDisconnect.addListener(onDisconnected);
}

/* 5. Register event handler */

// Nhận message từ content.js
// https://developer.chrome.com/extensions/runtime#event-onMessage
// Fired when a message is sent from either an extension process (by runtime.sendMessage) or a content script (by tabs.sendMessage).
chrome.runtime.onMessage.addListener(receiveFromContent);

/**
 *	load được DOM thì gọi connectNativeHost
 */
document.addEventListener('DOMContentLoaded', function () {
    // Trạng thái sẵn sàng giống $.ready();
    domReady = true;
    connectNativeHost();
});

