using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using Bkav.eGovCloud.Entities;
using System.Text;
using Bkav.eGovCloud.Business.Objects;

namespace Bkav.eGovCloud.Business.Utils
{
    /// <summary>
    /// Lớp quản lý việc Gửi tin nhắn
    /// </summary>
    public class SendSmsHelper : IDisposable
    {
        private SmsSettings _smsSettings;
        private TemplateHelper _templateHelper;
        private TemplateBll _templateService;
        private UserBll _userService;
        private SmsBll _smsService;
        private NotifyConfigBll _notifyConfigService;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="smsSettings"></param>
        /// <param name="templateHelper"></param>
        /// <param name="templateService"></param>
        /// <param name="userService"></param>
        /// <param name="smsService"></param>
        /// <param name="notifyConfigService"></param>
        public SendSmsHelper(SmsSettings smsSettings,
            TemplateHelper templateHelper,
            TemplateBll templateService,
            UserBll userService,
            SmsBll smsService,
            NotifyConfigBll notifyConfigService)
        {
            _smsSettings = smsSettings;
            _templateHelper = templateHelper;
            _templateService = templateService;
            _userService = userService;
            _smsService = smsService;
            _notifyConfigService = notifyConfigService;
        }

        #region Commom Sms

        /// <summary>
        /// Gửi tin nhắn
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại người nhận</param>
        /// <param name="message">Nội dung tin nhắn</param>
        public void SendSms(string phoneNumber, string message)
        {
            CreateQueueSms(phoneNumber, message, userSend: null, documentId: null, documentCopyId: null, type: null);
        }

        /// <summary>
        /// Gửi sms tự động
        /// </summary>
        /// <param name="doc">Hồ sơ, văn bản</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="type">Loại sms</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <param name="supplementary">Yêu cầu bổ sung (nếu có)</param>
        /// <returns>Gửi sms khi tiếp nhận hồ sơ và trả về trạng thái gửi thành công hay không</returns>
        public bool SendAutoSms(Document doc, User userSend, NotifyConfigType type, int? documentCopyId, Supplementary supplementary)
        {
            if (!_smsSettings.IsActivated)
            {
                return true;
            }

            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }

            var userId = 0;
            if (userSend != null)
            {
                userId = userSend.UserId;
            }

            var content = GetSmsContent(type, userId, doc, supplementary);
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("content");
            }

            //CreateQueueSms(doc.Phone, content, userSend, doc.DocumentId, documentCopyId, type);
            CreateQueueSmsNew(doc.Phone, content, userSend, doc.DocumentId, documentCopyId, type, doc.DocTypeId);
            return true;
        }

        /// <summary>
        /// Lưu tin nhắn mới vào hàng đợi
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại nhận</param>
        /// <param name="message">Nội dung tin nhắn</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="documentId">DocumentId</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <param name="type">Loại tin nhắn</param>
        public void CreateQueueSmsNew(string phoneNumber, string message, User userSend, Guid? documentId, int? documentCopyId, NotifyConfigType? type,  Guid? docTypeId)
        {
            if (!_smsSettings.IsActivated)
            {
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("Message is null or empty.");
            }

            if (!ValidPhoneNumber(phoneNumber))
            {
                throw new ArgumentNullException("PhoneNumber must phone number.");
            }

            //Todo: cần sửa lại quản lý mẫu phiếu tránh sử dụng editor khi cấu hình mẫu tin nhắn
            message = System.Net.WebUtility.HtmlDecode(message);
            message = message.StripHtml().StripVietnameseChars();

            var smsType = type.HasValue ? type.Value.ToString() : "";

#if DEBUG
            // TienBV: Để hạn chế việc gửi sms linh tinh khi fix lỗi cho khách hàng.
            // Việc gửi sms khi code sẽ fix cứng số điện thoại ở đây.
            // Sửa lại thành số điện thoại muốn debug tương ứng

            //phoneNumber = "0964681635";
#endif

            bool hasExist = type.HasValue && (type.Value == NotifyConfigType.Document_WhenOverdue);  // smsType != "";
            if (hasExist && documentId.HasValue)
            {
                hasExist = _smsService.Exist(s => s.PhoneNumber == phoneNumber && s.NotifyConfigType == smsType
                                    && s.DocumentId.HasValue && s.DocumentId.Value.Equals(documentId.Value));
            }

            if (hasExist)
            {
                return;
            }

            var newSms = new Sms()
            {
                DateCreated = DateTime.Now,
                IsSent = false,
                Message = message,
                PhoneNumber = phoneNumber,
                DocumentId = documentId,
                DocumentCopyId = documentCopyId,
                NotifyConfigType = smsType,
                LinkApi = docTypeId
            };

            if (userSend != null)
            {
                newSms.UserName = userSend.FullName;
                newSms.UserSendId = userSend.UserId;
            }

            _smsService.Create(newSms);

            //sent email
        }

        /// <summary>
        /// Lưu tin nhắn mới vào hàng đợi
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại nhận</param>
        /// <param name="message">Nội dung tin nhắn</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="documentId">DocumentId</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <param name="type">Loại tin nhắn</param>
        public void CreateQueueSms(string phoneNumber, string message, User userSend, Guid? documentId, int? documentCopyId, NotifyConfigType? type)
        {
            if (!_smsSettings.IsActivated)
            {
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("Message is null or empty.");
            }

            if (!ValidPhoneNumber(phoneNumber))
            {
                throw new ArgumentNullException("PhoneNumber must phone number.");
            }

            //Todo: cần sửa lại quản lý mẫu phiếu tránh sử dụng editor khi cấu hình mẫu tin nhắn
            message = System.Net.WebUtility.HtmlDecode(message);
            message = message.StripHtml().StripVietnameseChars();

            var smsType = type.HasValue ? type.Value.ToString() : "";

#if DEBUG
            // TienBV: Để hạn chế việc gửi sms linh tinh khi fix lỗi cho khách hàng.
            // Việc gửi sms khi code sẽ fix cứng số điện thoại ở đây.
            // Sửa lại thành số điện thoại muốn debug tương ứng
             
            //phoneNumber = "0964681635";
#endif

            bool hasExist = type.HasValue && (type.Value == NotifyConfigType.Document_WhenOverdue);  // smsType != "";
            if (hasExist && documentId.HasValue)
            {
                hasExist = _smsService.Exist(s => s.PhoneNumber == phoneNumber && s.NotifyConfigType == smsType
                                    && s.DocumentId.HasValue && s.DocumentId.Value.Equals(documentId.Value));
            }

            if (hasExist)
            {
                return;
            }

            var newSms = new Sms()
            {
                DateCreated = DateTime.Now,
                IsSent = false,
                Message = message,
                PhoneNumber = phoneNumber,
                DocumentId = documentId,
                DocumentCopyId = documentCopyId,
                NotifyConfigType = smsType
            };

            if (userSend != null)
            {
                newSms.UserName = userSend.FullName;
                newSms.UserSendId = userSend.UserId;
            }

            _smsService.Create(newSms);

            //sent email
        }

        //Todo: đưa hàm này vào core
        private string ConvertToUnicode(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            return Encoding.Unicode.GetString(bytes);
        }

        #endregion

        #region Document Sms

        /// <summary>
        /// Gửi Sms khi tiếp nhận hồ sơ và trả về trạng thái gửi thành công hay không
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <returns>Trạng thái gửi Sms thành công hay không, true - thành công; còn lại - false</returns>
        public void SendCreatedDocumentSms(Document doc, User userSend, int? documentCopyId = null)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenCreated, documentCopyId, supplementary: null);
        }

        /// <summary>
        /// Gửi Sms khi chuyển văn bản, hồ sơ và trả về trạng thái gửi thành công hay không
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <returns>Trạng thái gửi Sms thành công hay không, true - thành công; còn lại - false</returns>
        public void SendTranferDocumentSms(Document doc, User userSend, int documentCopyId)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenReceived, documentCopyId, supplementary: null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="userSend"></param>
        /// <param name="documentCopyId"></param>
        public void SendCreateDocumentSmsExpert(Document doc, User userSend, int? documentCopyId = null)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenCreated, documentCopyId, supplementary: null);
        }

        public void SendCreateDocumentSmsOutOfDate(Document doc, User userSend, int? documentCopyId = null)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenOverdue, documentCopyId, supplementary: null);
        }

        public void SendCreateDocumentSmsDueDate(Document doc, User userSend, int documentCopyId)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenHasResult, documentCopyId, supplementary: null);
        }
        /// <summary>
        /// Gửi Sms khi có yêu cầu bổ sung hồ sơ và trả về trạng thái gửi thành công hay không.
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="supp">Yêu cầu bổ sung</param>
        /// <param name="userSend">Người xử lý</param>
        /// <param name="documentCopyId">Document Copy Id</param>
        /// <returns>Trạng thái gửi Sms thành công hay không, true - thành công; còn lại - false</returns>
        public void SendSupplementarySms(Document doc, Supplementary supp, User userSend, int documentCopyId)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenRequireSupplementary, documentCopyId, supp);
        }

        /// <summary>
        /// Gửi Sms khi cập nhật yêu cầu bổ sung hồ sơ và trả về trạng thái gửi thành công hay không.
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="supp">Yêu cầu bổ sung</param>
        /// <param name="userSend">Người xử lý</param>
        /// <param name="documentCopyId">Document Copy Id</param>
        /// <returns>Trạng thái gửi Sms thành công hay không, true - thành công; còn lại - false</returns>
        public void SendCompleteSupplementary(Document doc, Supplementary supp, User userSend, int documentCopyId)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenCompleteSupplementary, documentCopyId, supp);
        }

        /// <summary>
        /// Gửi Sms thông báo trả kết quả và trả về trạng thái gửi thành công hay chưa
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="documentCopyId">Document Copy Id</param>
        /// <returns>Trạng thái trả về thành công hay không, true - gửi Sms thành công; còn lại - false</returns>
        public void SendReturnResultSms(Document doc, User userSend, int documentCopyId)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenHasResult, documentCopyId, supplementary: null);
        }

        /// <summary>
        /// Gửi Sms thông báo hồ sơ trễ hẹn và trả về trạng thái gửi thành công hay chưa
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="documentCopyId">Document Copy Id</param>
        /// <returns>Trạng thái trả về thành công hay không, true - gửi Sms thành công; còn lại - false</returns>
        public void SendDocumentOverdue(Document doc, User userSend, int documentCopyId)
        {
            SendAutoSms(doc, userSend, NotifyConfigType.Document_WhenOverdue, documentCopyId, supplementary: null);
        }

        #endregion

        #region Document Online Sms

        /// <summary>
        /// Gửi Sms thông báo Tiếp nhận hồ sơ đăng ký qua mạng và trả về giá trị xác định gửi Sms thành công hay không.
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <returns>Trạng thái trả về thành công hay không, true - gửi Sms thành công; còn lại - false</returns>
        public bool SendAcceptedDocumentOnline(Document doc, int documentCopyId)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }

            var content = GetSmsContent(NotifyConfigType.DocumentOnline_WhenAccepted, 0, doc, null);
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("SendSms: content is null");
            }

            CreateQueueSms(doc.Phone, content, null, doc.DocumentId, documentCopyId, type: null);

            return true;
        }

        /// <summary>
        /// Gửi Sms thông báo Từ chối tiếp nhận hồ sơ đăng ký qua mạng và trả về giá trị xác định gửi Sms thành công hay không.
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="userSendName">Account Người xử lý</param>
        /// <param name="userSendFullname">Tên người xử lý </param>
        /// <param name="comment">Nội dung từ chối hoặc nội dung yêu cầu bổ sung.</param>
        /// <returns>Trạng thái trả về thành công hay không, true - gửi Sms thành công; còn lại - false</returns>
        public bool SendRejectedDocumentOnline(DocumentOnline doc, string userSendName, string userSendFullname, string comment)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }

            var type = NotifyConfigType.DocumentOnline_WhenRejected.ToString();
            var notifyConfig = _notifyConfigService.Get(p => p.Key == type);
            if (notifyConfig == null || notifyConfig.SmsTemplateId == 0)
            {
                throw new Exception("SendSms: notifyConfig is null");
            }

            var template = _templateService.Get(notifyConfig.SmsTemplateId);
            if (template == null)
            {
                throw new Exception("SendSms: template is null");
            }

            var content = _templateHelper.ParseDocumentOnlineTemplate(
                        template,
                        docFieldName: "",
                        docCode: doc.DocCode,
                        docTypeName: doc.DoctypeName,
                        compendium: doc.Compendium,
                        accountCommand: userSendName,
                        dateCommand: DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                        fullNameCommand: userSendFullname,
                        officeName: "",
                        citizenName: doc.PersonInfo,
                        comment: comment,
                        dateRegister: doc.DateReceived.ToString("dd/MM/yyyy hh:mm:ss"),
                        token: doc.Token);

            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("SendSms: content is null");
            }

            CreateQueueSms(doc.Phone, content, userSend: null, documentId: null, documentCopyId: null, type: null);

            return true;
        }

        /// <summary>
        /// Gửi yêu cầu tiếp nhận bổ sung
        /// </summary>
        /// <param name="supplementary"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool SendRequireSupplementary(SupplementaryOnline supplementary, string token)
        {
            if (string.IsNullOrWhiteSpace(supplementary.Phone))
            {
                throw new ArgumentNullException("supplementary.Phone");
            }

            var type = NotifyConfigType.DocumentOnline_WhenRequireSupplementary.ToString();
            var notifyConfig = _notifyConfigService.Get(p => p.Key == type);
            if (notifyConfig == null || notifyConfig.SmsTemplateId == 0)
            {
                throw new Exception("SendSms: notifyConfig is null");
            }

            var template = _templateService.Get(notifyConfig.SmsTemplateId);
            if (template == null)
            {
                throw new Exception("SendSms: template is null");
            }

            var details = string.Join(" - ", supplementary.PaperDetails.Select(x => x.PaperName));

            var content = _templateHelper.ParseDocumentOnlineTemplate(
                        template,
                        docFieldName: "",
                        docCode: supplementary.DocCode,
                        docTypeName: supplementary.DoctypeName,
                        compendium: supplementary.Compendium,
                        accountCommand: supplementary.User.Username,
                        dateCommand: supplementary.DateCommand,
                        fullNameCommand: supplementary.User.FullName,
                        officeName: supplementary.OfficeName,
                        citizenName: supplementary.CitizenName,
                        comment: supplementary.CommentSend,
                        dateRegister: supplementary.DateReceived,
                        supplementary: details,
                        supplementaryDate: supplementary.ExpireDate,
                        token: token);

            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("SendSms: content is null");
            }

            CreateQueueSms(supplementary.Phone, content, userSend: null, documentId: null, documentCopyId: null, type: null);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool ValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            var pattern = @"^\+?[0-9]{0,14}$";
            return (Regex.IsMatch(phoneNumber, pattern));
        }

        #endregion

        #region Tin nhắn tra cứu tiến độ

        /// <summary>
        /// Nhận tin nhắn tra cứu tiến độ xử lý hồ sơ
        /// </summary>
        /// <param name="phone">Số điện thoại</param>
        /// <param name="doc">Hồ sơ</param>
        /// <returns></returns>
        public bool MOReceiver(string phone, Document doc)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentNullException("phone");
            }

            string content;
            NotifyConfigType? type = null;
            if (doc == null)
            {
                content = "Ma ho so khong ton tai. Vui long nhan dung ma ho so duoc cap. Tran trong!";
            }
            else
            {
                type = ((doc.IsReturned.HasValue && doc.IsReturned.Value) || doc.Status == (int)DocumentStatus.KetThuc)
                                        ? NotifyConfigType.Lookup_DocumentHasResult
                                        : NotifyConfigType.Lookup_DocumentProcessing;

                content = GetSmsContent(type.Value, 0, doc, null);
            }
            
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("SendSms: content is null");
            }

            CreateQueueSms(phone, content, null, null, null, type: type);

            return true;
        }

        #endregion

        #region  Calendar

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calendar"></param>
        /// <param name="userCreate"></param>
        /// <param name="phones"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SendCreateCalendarSms(Calendar calendar, User userCreate, List<string> phones, int type)
        {
            //Hệ thống không kích hoạt cho gửi sms
            if (!_smsSettings.IsActivated)
            {
                return false;
            }

            // var templateType = "";

            // CreateQueueSms(phone, content, userSend: null, documentId: null, documentCopyId: null, type: null);

            return true;
        }

        #endregion

        #region private method

        private string GetSmsContent(NotifyConfigType type, int userId, Document doc, Supplementary supplementary)
        {
            var result = string.Empty;

            var templateType = type.ToString();
            var notifyConfig = _notifyConfigService.Get(n => n.Key.Equals(templateType));
            if (notifyConfig == null || !notifyConfig.HasAutoSendSms || notifyConfig.SmsTemplateId == 0)
            {
                return result;
            }

            var template = _templateService.Get(notifyConfig.SmsTemplateId);
            if (template == null)
            {
                return result;
            }

            var paperAddIds = "";
            var feeAddIds = "";

            if (supplementary != null)
            {
                paperAddIds = supplementary.PaperIds;
                feeAddIds = supplementary.FeeIds;
            }

            result = _templateHelper.ParseContentNew(template, userId, doc, null, paperAddIds, feeAddIds);

            return result;
        }


        #endregion
        
        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _smsSettings = null;
                _notifyConfigService = null;
                _templateService = null;
                _templateHelper = null;
                _userService = null;
                _smsService = null;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
