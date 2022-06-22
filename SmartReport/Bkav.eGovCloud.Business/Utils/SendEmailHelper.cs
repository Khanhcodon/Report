using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities.Common;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovCloud.Entities.Enum;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;
using Bkav.eGovCloud.Business.Common;
using System.Security.Cryptography;
using System.IO;
using Bkav.eGovCloud.Core.Mail;

namespace Bkav.eGovCloud.Business.Utils
{
	/// <summary>
	///Lớp quản lý việc gửi email tự động
	/// </summary>
	public class SendEmailHelper : IDisposable
	{
		#region Private Fields

		private EmailSettings _emailSettings;
		private NotifyConfigBll _notifyConfigService;
		private TemplateBll _templateService;
		private AttachmentBll _attachmentService;
		private TemplateHelper _templateHelper;
		private MailBll _mailService;

		private SmtpClient _smtpClient;

        private readonly ResourceBll _resourceService;

        /// <summary>
        /// Mail mặc định
        /// </summary>
        private MailMessage _defaultMailMessage
		{
			get
			{
				var mailMessage = new MailMessage
				{
					From = new MailAddress(_emailSettings.SmtpUsername, _emailSettings.DisplayName, Encoding.UTF8),
				};
				return mailMessage;
			}
		}

		#endregion

		/// <summary>
		/// Smtp mặc định
		/// </summary>
		public SmtpClient SmtpClient
		{
			get
			{
				if (_smtpClient != null)
				{
					return _smtpClient;
				}
				var smtp = new SmtpClient()
				{
					EnableSsl = _emailSettings.EnableSsl,
					Host = _emailSettings.SmtpServer,
					Port = _emailSettings.SmtpPort,
					Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword.Base64Decode())
				};
				return smtp;
			}
			set
			{
				_smtpClient = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="emailSettings"></param>
		/// <param name="notifyConfigService"></param>
		/// <param name="templateService"></param>
		/// <param name="templateHelper"></param>
		/// <param name="mailService"></param>
		/// <param name="attachmentService"></param>
		public SendEmailHelper(EmailSettings emailSettings, NotifyConfigBll notifyConfigService,
					TemplateBll templateService, TemplateHelper templateHelper, MailBll mailService, AttachmentBll attachmentService)
		{
			_emailSettings = emailSettings;
			_notifyConfigService = notifyConfigService;
			_templateService = templateService;
			_templateHelper = templateHelper;
			_mailService = mailService;
			_attachmentService = attachmentService;
		}

		#region Send Common Mail

		/// <summary>
		/// Gửi mail
		/// </summary>
		/// <param name="subject">Tiêu đề mail</param>
		/// <param name="mailto">Địa chỉ email nhận (sử dụng dấu , để gửi tới nhiều email)</param>
		/// <param name="content">Nội dung mail</param>
		/// <param name="isBodyHtml">Nội dung mail là html</param>
		/// <param name="smtp">Smtp Client ( null: sử dụng smtp mặc định trong cấu hình)</param>
		public void Send(string subject, string mailto, string content, bool isBodyHtml, SmtpClient smtp = null)
		{
			if (string.IsNullOrEmpty(mailto))
			{
				throw new ArgumentNullException("mailto");
			}

			mailto = EnsureReceivedMail(mailto);

			var mail = _defaultMailMessage;
			mail.Subject = subject;
			mail.To.Add(mailto);
			mail.Body = content;
			mail.IsBodyHtml = isBodyHtml;

			if (smtp == null)
			{
				smtp = SmtpClient;
			}

			smtp.Send(mail);
		}

		/// <summary>
		/// Gửi mail
		/// </summary>
		/// <param name="mailMessage">Mail message</param>
		/// <param name="smtp">Smtp Client ( null: sử dụng smtp mặc định trong cấu hình)</param>
		public void Send(MailMessage mailMessage, SmtpClient smtp = null)
		{
			if (smtp == null)
			{
				smtp = SmtpClient;
			}
			smtp.Send(mailMessage);
		}

		/// <summary>
		/// Gửi email
		/// </summary>
		/// <param name="sender">Địa chỉ email gửi(null: Mặc định như trong cấu hình)</param>
		/// <param name="senderDisplayName">Tên người gửi(null: Mặc định như trong cấu hình)</param>
		/// <param name="subject">Tiêu đề mail</param>
		/// <param name="body">Nội dung mail</param>
		/// <param name="sendTo">Địa chỉ email nhận (sử dụng dấu , để gửi tới nhiều email)</param>
		/// <param name="carbonCopys">Địa chỉ email gửi CC (null: mặc định là ko gửi CC)</param>
		/// <param name="isBodyHtml">Nội dung mail là html</param>
		/// <param name="attachments">File đính kèm trong email(null: mặc định là không gửi đính kèm)</param>
		/// <param name="smtp">Smtp Client ( null: sử dụng smtp mặc định trong cấu hình)</param>
		/// <param name="header">email header ( null: sử dụng header mặc định trong cấu hình)</param>
		/// <param name="signature">chữ ký ( null: sử dụng chữ ký mặc định trong cấu hình)</param>
		public void Send(string subject, string body, string sendTo,
			string signature = null, string header = null,
			string sender = null, string senderDisplayName = null,
			bool isBodyHtml = true, List<string> carbonCopys = null,
			List<System.Net.Mail.Attachment> attachments = null, SmtpClient smtp = null)
		{
			if (!_emailSettings.IsActivated)
			{
				return;
			}

			var mailMessage = new MailMessage();
			if (sender == null)
			{
				if (senderDisplayName == null)
				{
					mailMessage = _defaultMailMessage;
				}
				else
				{
					mailMessage.From = new MailAddress(_emailSettings.SmtpUsername, senderDisplayName, Encoding.UTF8);
				}
			}
			else
			{
				mailMessage.From = new MailAddress(sender, senderDisplayName, Encoding.UTF8);
			}

			if (signature == null)
			{
				signature = _emailSettings.EmailSignature;
			}

			if (header == null)
			{
				header = _emailSettings.EmailHeader;
			}

			if (isBodyHtml)
			{
				body = header + "<br/><br/>" + body + "<br/><br/>" + signature;
			}
			else
			{
				body = header + "\n" + body + "\n\n" + signature;
			}

			mailMessage.IsBodyHtml = isBodyHtml;
			mailMessage.Body = body;
			mailMessage.Subject = subject;


			sendTo = EnsureReceivedMail(sendTo);
			mailMessage.To.Add(new MailAddress(sendTo));

			if (carbonCopys != null)
			{
				foreach (var carbonCopy in carbonCopys)
				{
					mailMessage.CC.Add(carbonCopy);
				}
			}

			if (attachments != null)
			{
				foreach (var attachment in attachments)
				{
					mailMessage.Attachments.Add(attachment);
				}
			}

			Send(mailMessage, smtp);
		}

		/// <summary>
		/// Gửi mail tự động
		/// </summary>
		/// <param name="doc">Hồ sơ, văn bản</param>
		/// <param name="userSend">Người gửi</param>
		/// <param name="receivedMails">Danh sách mail nhận</param>
		/// <param name="type">Loại mail</param>
		/// <param name="documentCopyId">DocumentCopyId</param>
		/// <param name="supplementary">Yêu cầu bổ sung (nếu có)</param>
		/// <returns>Gửi mail khi tiếp nhận hồ sơ và trả về trạng thái gửi thành công hay không</returns>
		public bool SendAutoMail(Document doc, User userSend, List<string> receivedMails, NotifyConfigType type, int? documentCopyId, Supplementary supplementary)
		{
			if (!_emailSettings.IsActivated)
			{
				return false;
			}

			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}

			if (userSend == null)
			{
				throw new ArgumentNullException("userSend");
			}

			if (receivedMails == null || !receivedMails.Any())
			{
				throw new ArgumentNullException("receivedMails");
			}

			string subject;

			var content = GetMailContent(type, userSend.UserId, doc, supplementary, out subject);
			if (string.IsNullOrEmpty(content))
			{
				throw new Exception("content");
			}

			CreateQueueMail(subject, receivedMails, content, userSend, doc.DocumentId, documentCopyId);

			return true;
		}

		/// <summary>
		/// Thêm mail chờ gửi vào hàng đợi
		/// </summary>
		/// <param name="subject">Tiêu đề mail</param>
		/// <param name="receivedMails">Danh sách người nhận</param>
		/// <param name="content">Nội dung mail</param>
		/// <param name="userSend">Người gửi</param>
		/// <param name="documentId">ID Hồ sơ, văn bản</param>
		/// <param name="documentCopyId">DocumentCopyId</param>
		/// <param name="attachmentIds">Danh sách các file đính kèm</param>
		/// <param name="fileReport">file báo cáo</param>
		public void CreateQueueMail(string subject, List<string> receivedMails, string content, User userSend, Guid? documentId = null, int? documentCopyId = null, List<int> attachmentIds = null, string fileReport = "")
		{
			if (!_emailSettings.IsActivated)
			{
				return;
			}

			//receivedMails = EnsureReceivedMails(receivedMails);

            if (!receivedMails.Any())
			{
				throw new ArgumentNullException("receivedMails");
			}

			// TienBV: Đối list Id thành danh sách các đường dẫn File đính kèm để xử lý gửi mail cho tiện hơn, tránh query lại db nhiều lần
			var attachmentPaths = new Dictionary<string, string>();
            /*
			foreach (var attachmentId in attachmentIds)
			{
				string fileName;
				var filePath = _attachmentService.GetAttachmentPath(out fileName, attachmentId);
				attachmentPaths.Add(fileName, filePath);
			}
            */
			if (!string.IsNullOrEmpty(fileReport))
			{
				attachmentPaths.Add("InBienNhan.pdf", fileReport);
			}

			var newQueueMail = new Mail()
			{
				Body = content,
				IsBodyHtml = true,
				IsSent = false,
				DateCreated = DateTime.Now,
				Subject = subject,
				SendTo = string.Join(",", receivedMails),
				AttachmentIdStr = attachmentIds != null ? attachmentPaths.Stringify() : "{}"
			};

			if (userSend != null)
			{
				newQueueMail.UserName = userSend.FullName;
				newQueueMail.UserSendId = userSend.UserId;
			}

			_mailService.Create(newQueueMail);

            //foreach(var strList in receivedMails)
            //{
            //    var mail = strList;
            //    var body = content; 
            //    var enableSsl = _emailSettings.EnableSsl;
            //    var host = _emailSettings.SmtpServer;
            //    var port = _emailSettings.SmtpPort;
            //    var username = _emailSettings.SmtpUsername;
            //    var password = Base64Decode(_emailSettings.SmtpPassword);

            //    SendDocumentEmail(subject, mail, body, enableSsl, host, port, username, password);
            //}      
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// hiepns
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="body"></param>
        /// <param name="enableSsl"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SendDocumentEmail(string subject, string mail, string body, bool enableSsl, string host, int port, string username, string password)
        {
            if (!mail.IsEmailAddress())
            {
                return false;
            }

            if (string.IsNullOrEmpty(body))
            {
                return false;
            }

            try
            {
                var fromAddrees = new MailAddress(_emailSettings.SmtpUsername, "NguyenSyHiep");
                var toAddress = new MailAddress(mail, "Hiepns");

                SmtpClient smtpClient = new SmtpClient
                {   
                    Host = host,
                    Port = port,
                    EnableSsl = enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,            
                    Credentials = new NetworkCredential(fromAddrees.Address, password)
                };
                var message = new MailMessage(fromAddrees, toAddress);
                message.Body = body;
                message.IsBodyHtml = true;
                message.Subject = subject;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


		#endregion

		#region Send Document Mail

		/// <summary>
		/// Gửi mail khi tiếp nhận hồ sơ và trả về trạng thái gửi thành công hay không
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="userSend">Người gửi</param>
		/// <param name="documentCopyId">DocumentCopyId</param>
		/// <returns>Trạng thái gửi mail thành công hay không, true - thành công; còn lại - false</returns>
		public bool SendCreatedDocumentMail(Document doc, User userSend, int? documentCopyId = null)
		{
			return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenCreated, documentCopyId, supplementary: null);
		}

        /// <summary>
        /// khi tạo báo cáo tự động cho chuyên viên
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="userSend"></param>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public bool SendCreatedDocumentMailExpert(Document doc, User userSend, int? documentCopyId = null)
        {
            return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenCreated, documentCopyId, supplementary: null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="userSend"></param>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public bool SendCreateDocumentMailOutOfDate(Document doc, User userSend, int? documentCopyId = null)
        {
            return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenOverdue, documentCopyId, supplementary: null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="userSend"></param>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public bool SendCreateDocumentMailDueDate(Document doc, User userSend, int? documentCopyId = null)
        {
            return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenHasResult, documentCopyId, supplementary: null);
        }
        /// <summary>
        /// Gửi mail khi chuyển văn bản, hồ sơ và trả về trạng thái gửi thành công hay không
        /// </summary>
        /// <param name="doc">Hồ sơ</param>
        /// <param name="userSend">Người gửi</param>
        /// <param name="userReceiveds">Danh sách email nhận</param>
        /// <param name="documentCopyId">DocumentCopyId</param>
        /// <returns>Trạng thái gửi mail thành công hay không, true - thành công; còn lại - false</returns>
        public bool SendTranferDocumentMail(Document doc, User userSend, List<string> userReceiveds, int? documentCopyId = null)
		{
			// TienBV: tạm bỏ chức năng này do chưa có đơn vị nào trùng
			return SendAutoMail(doc, userSend, userReceiveds, NotifyConfigType.Document_WhenReceived, documentCopyId, supplementary: null);
			//return true;
		}

        /// <summary>
        /// gửi mail khi chuyển văn bản
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="userSend"></param>
        /// <param name="documentCopyId"></param>
        /// <returns></returns>
        public bool SendTranferDocumentMail_(Document doc, User userSend, int? documentCopyId = null)
        {
            return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenReceived, documentCopyId, supplementary: null);
        }
        /// <summary>
        /// Gửi mail thông báo khi phát hành cho các đơn vị bên ngoài 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="receivedMails"></param>
        /// <param name="attachmentIds"></param>
        /// <param name="documentCopyId"></param>
        /// <param name="attachNames"></param>
        /// <returns></returns>
        public bool SendTranferPublishDocument(Document doc, List<string> receivedMails, List<int> attachmentIds, int documentCopyId, List<string> attachNames)
		{
			if (string.IsNullOrEmpty(_emailSettings.SmtpUsername))
			{
				return false;
			}

			if (!receivedMails.Any())
			{
				return false;
			}

			Dictionary<string, string> model = new Dictionary<string, string>
			{
				{ "${CategoryName}", doc.CategoryName },
				{ "${DocCode}", doc.DocCode },
				{ "${InOutPlace}", doc.InOutPlace },
				{ "${Compendium}", doc.Compendium },
				{ "${DatePublished}", doc.DatePublished.Value.ToString("hh:mm dd/MM/yyyy") }
			};

			var path = "~/MailTemplate/PublishTemplate.cshtml";
			var subject = "";
			var content = GetMailContentTemplate(model, attachNames, path, out subject);
			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(subject, receivedMails, content, null, doc.DocumentId, documentCopyId, attachmentIds);

			return true;
		}

		/// <summary>
		/// Gửi mail trả kết quả cho công dân
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="userReceiveds"></param>
		/// <param name="attachmentIds"></param>
		/// <param name="documentCopyId"></param>
		/// <param name="attachNames"></param>
		/// <returns></returns>
		public bool SendReturnResult(Document doc, List<string> userReceiveds, List<int> attachmentIds, int documentCopyId, List<string> attachNames)
		{
			if (string.IsNullOrEmpty(_emailSettings.SmtpUsername))
			{
				return false;
			}

			if (!userReceiveds.Any())
			{
				return false;
			}

			Dictionary<string, string> model = new Dictionary<string, string>
			{
				{ "${UserSuccessName}", doc.UserSuccessName },
				{ "${CurrentUserName}", doc.UserCreatedName },
				{ "${DateSuccess}", doc.DateSuccess.HasValue? doc.DateSuccess.Value.ToString("hh:mm dd/MM/yyyy"): "" },
				{ "${DateReturn}", doc.DateResult.HasValue? doc.DateResult.Value.ToString("hh:mm dd/MM/yyyy"): ""},
				{ "${CitizenName}", doc.CitizenName },
				{ "${Email}", doc.Email },
				{ "${Phone}", doc.Phone },
				{ "${CategoryName}", doc.CategoryName },
				{ "${DocCode}", doc.DocCode },
				{ "${DoctypeName}", doc.DocTypeName },
				{ "${InOutPlace}", doc.InOutPlace },
				{ "${Compendium}", doc.Compendium },
			};
			var path = "~/MailTemplate/ReturnResultTemplate.cshtml";
			var subject = "";
			var content = GetMailContentTemplate(model, attachNames, path, out subject);
			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(subject, userReceiveds, content, null, doc.DocumentId, documentCopyId, attachmentIds);

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="docCopy"></param>
		/// <param name="userReceiveds"></param>
		/// <param name="currentUserId"></param>
		/// <returns></returns>
		public bool SendTiepNhan(DocumentCopy docCopy, List<string> userReceiveds, int currentUserId)
		{
			if (string.IsNullOrEmpty(_emailSettings.SmtpUsername))
			{
				return false;
			}

			if (!userReceiveds.Any())
			{
				return false;
			}
			var doc = docCopy.Document;
			Dictionary<string, string> model = new Dictionary<string, string>
			{
				{ "${UserSuccessName}", doc.UserSuccessName },
				{ "${CurrentUserName}", doc.UserCreatedName },
				{ "${DateSuccess}", doc.DateSuccess.HasValue? doc.DateSuccess.Value.ToString("hh:mm dd/MM/yyyy"): "" },
				{ "${DateReturn}", doc.DateResult.HasValue? doc.DateResult.Value.ToString("hh:mm dd/MM/yyyy"): ""},
				{ "${CitizenName}", doc.CitizenName },
				{ "${Email}", doc.Email },
				{ "${Phone}", doc.Phone },
				{ "${CategoryName}", doc.CategoryName },
				{ "${DocCode}", doc.DocCode },
				{ "${DoctypeName}", doc.DocTypeName },
				{ "${InOutPlace}", doc.InOutPlace },
				{ "${Compendium}", doc.Compendium },
			};
			var pathFile = "";
			_templateHelper.CrystalToPDF(docCopy, currentUserId, out pathFile, 32);
			var path = "~/MailTemplate/TiepNhanTemplate.cshtml";
			var subject = "";
			var content = GetMailContentTemplate(model, new List<string>() { "Phiếu in biên nhận và hẹn trả kết quả" }, path, out subject);
			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(subject, userReceiveds, content, null, doc.DocumentId, docCopy.DocumentCopyId, new List<int>(), pathFile);

			return true;
		}

		/// <summary>
		/// Gửi mail khi có yêu cầu bổ sung hồ sơ và trả về trạng thái gửi thành công hay không.
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="supp">Yêu cầu bổ sung</param>
		/// <param name="userSend">Người xử lý</param>
		/// <param name="documentCopyId">Document Copy Id</param>
		/// <returns>Trạng thái gửi mail thành công hay không, true - thành công; còn lại - false</returns>
		public bool SendSupplementaryMail(Document doc, Supplementary supp, User userSend, int documentCopyId)
		{
			return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenRequireSupplementary, documentCopyId, supp);
		}

		/// <summary>
		/// Gửi mail khi cập nhật yêu cầu bổ sung hồ sơ và trả về trạng thái gửi thành công hay không.
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="supp">Yêu cầu bổ sung</param>
		/// <param name="userSend">Người xử lý</param>
		/// <param name="documentCopyId">Document Copy Id</param>
		/// <returns>Trạng thái gửi mail thành công hay không, true - thành công; còn lại - false</returns>
		public bool SendCompleteSupplementary(Document doc, Supplementary supp, User userSend, int documentCopyId)
		{
			return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenCompleteSupplementary, documentCopyId, supp);
		}

		/// <summary>
		/// Gửi mail thông báo trả kết quả và trả về trạng thái gửi thành công hay chưa
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="userSend">Người gửi</param>
		/// <param name="documentCopyId">Document Copy Id</param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendReturnResultMail(Document doc, User userSend, int documentCopyId)
		{
			return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenHasResult, documentCopyId, supplementary: null);
		}

		/// <summary>
		/// Gửi mail thông báo hồ sơ trễ hẹn và trả về trạng thái gửi thành công hay chưa
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="userSend">Người gửi</param>
		/// <param name="documentCopyId">Document Copy Id</param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendDocumentOverdue(Document doc, User userSend, int documentCopyId)
		{
			return SendAutoMail(doc, userSend, new List<string>() { doc.Email }, NotifyConfigType.Document_WhenOverdue, documentCopyId, supplementary: null);
		}

		#endregion

		#region Send Document Online Mail

		/// <summary>
		/// Gửi mail thông báo Tiếp nhận hồ sơ đăng ký qua mạng và trả về giá trị xác định gửi mail thành công hay không.
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="attachmentIds">Danh sách file đính kèm</param>
		/// <param name="documentCopyId">DocumentCopyId</param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendAcceptedDocumentOnline(Document doc, List<int> attachmentIds, int documentCopyId)
		{
			if (doc == null || !doc.Email.IsEmailAddress())
			{
				return false;
			}

			var receivedMails = new List<string>() { doc.Email };
			if (!receivedMails.Any())
			{
				return false;
			}

			string subject;
			var content = GetMailContent(NotifyConfigType.DocumentOnline_WhenAccepted, 0, doc, null, out subject);
			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(subject, receivedMails, content, null, doc.DocumentId, documentCopyId, attachmentIds);

			return true;
		}

		/// <summary>
		/// Gửi mail thông báo Từ chối tiếp nhận hồ sơ đăng ký qua mạng và trả về giá trị xác định gửi mail thành công hay không.
		/// </summary>
		/// <param name="doc">Hồ sơ</param>
		/// <param name="userSend">Người xử lý</param>
		/// <param name="comment">Nội dung từ chối hoặc nội dung yêu cầu bổ sung.</param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendRejectedDocumentOnline(DocumentOnline doc, User userSend, string comment)
		{
			if (doc == null || !doc.Email.IsEmailAddress())
			{
				return false;
			}

			var receivedMails = new List<string>() { doc.Email };
			if (!receivedMails.Any())
			{
				return false;
			}

			var type = NotifyConfigType.DocumentOnline_WhenRejected.ToString();
			var notifyConfig = _notifyConfigService.Get(p => p.Key == type);
			if (notifyConfig == null || notifyConfig.MailTemplateId == 0)
			{
				return false;
			}

			var template = _templateService.Get(notifyConfig.MailTemplateId);
			if (template == null)
			{
				return false;
			}

			var content = _templateHelper.ParseDocumentOnlineTemplate(
						template,
						docFieldName: "",
						docCode: doc.DocCode,
						docTypeName: doc.DoctypeName,
						compendium: doc.Compendium,
						accountCommand: userSend.Username,
						dateCommand: DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
						fullNameCommand: userSend.FullName,
						officeName: "",
						citizenName: doc.PersonInfo,
						comment: comment,
						dateRegister: doc.DateReceived.ToString("dd/MM/yyyy hh:mm:ss"),
						token: doc.Token);

			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(template.TitleMail, receivedMails, content, userSend: null);

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
			if (supplementary.Mail == null || !supplementary.Mail.IsEmailAddress())
			{
				return false;
			}

			var receivedMails = new List<string>() { supplementary.Mail };
			if (!receivedMails.Any())
			{
				return false;
			}

			var type = NotifyConfigType.DocumentOnline_WhenRequireSupplementary.ToString();
			var notifyConfig = _notifyConfigService.Get(p => p.Key == type);
			if (notifyConfig == null || notifyConfig.MailTemplateId == 0)
			{
				return false;
			}

			var template = _templateService.Get(notifyConfig.MailTemplateId);
			if (template == null)
			{
				return false;
			}

			var details = string.Join("<br />+", supplementary.PaperDetails.Select(x => x.PaperName));

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
				return false;
			}

			CreateQueueMail(template.TitleMail, receivedMails, content, userSend: null);

			return true;
		}

		#endregion

		#region Send Question Mail

		/// <summary>
		/// Gửi mail thông báo trả lời câu hỏi và trả về giá trị xác định gửi thành công hay không.
		/// </summary>
		/// <param name="question">Câu hỏi</param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendAnswerQuestion(Question question)
		{
			if (!_emailSettings.IsActivated)
			{
				return false;
			}

			var receivedMails = new List<string>() { question.Email };
			if (!receivedMails.Any())
			{
				return false;
			}

			var type = NotifyConfigType.Question_WhenAnswered.ToString();
			var mailConfig = _notifyConfigService.Get(p => p.Key == type);
			if (mailConfig == null || mailConfig.MailTemplateId == 0)
			{
				return false;
			}

			var template = _templateService.Get(mailConfig.MailTemplateId);
			if (template == null)
			{
				return false;
			}

			var content = _templateHelper.ParseQuestionTemplate(template, question);
			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(template.TitleMail, receivedMails, content, userSend: null);

			return true;
		}

		/// <summary>
		/// Gửi mail thông báo Từ chối trả lời câu hỏi và trả về trạng thái gửi mail thành công hay không.
		/// </summary>
		/// <param name="question">Câu hỏi</param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendRejectQuestion(Question question)
		{
			if (!_emailSettings.IsActivated)
			{
				return false;
			}

			var receivedMails = new List<string>() { question.Email };
			if (!receivedMails.Any())
			{
				return false;
			}

			var type = NotifyConfigType.Question_WhenRejected.ToString();
			var mailConfig = _notifyConfigService.Get(p => p.Key == type);
			if (mailConfig == null || mailConfig.MailTemplateId == 0)
			{
				return false;
			}

			var template = _templateService.Get(mailConfig.MailTemplateId);
			if (template == null)
			{
				return false;
			}

			var content = _templateHelper.ParseQuestionTemplate(template, question);
			if (string.IsNullOrEmpty(content))
			{
				return false;
			}

			CreateQueueMail(template.TitleMail, receivedMails, content, userSend: null);

			return true;
		}

		#endregion

		#region Send Calendar Mail

		/// <summary>
		/// Gửi mail thông báo cuộc họp và trả về giá trị xác định gửi mail thành công hay không
		/// </summary>
		/// <param name="calendar"></param>
		/// <param name="userCreate"></param>
		/// <param name="userReceiveds"></param>
		/// <param name="type">Trạng thái của lịch làm việc
		/// 1: gửi mail khi lịch mới được tạo
		/// 2: gửi mail khi lịch thay đổi
		/// 3: gửi mail khi lịch bị xóa
		/// </param>
		/// <returns>Trạng thái trả về thành công hay không, true - gửi mail thành công; còn lại - false</returns>
		public bool SendCalendarMail(Calendar calendar, User userCreate, List<string> userReceiveds, int type)
		{
			// Xử lý nội dung mail ở đây
			var templateType = "";

			if (type == 1)
			{
				templateType = NotifyConfigType.Calendar_WhenCreated.ToString();
			}
			else if (type == 2)
			{
				templateType = NotifyConfigType.Calendar_WhenChanged.ToString();
			}
			else
			{
				templateType = NotifyConfigType.Calendar_WhenDestroyed.ToString();
			}

			var notifyConfig = _notifyConfigService.Get(n => n.Key.Equals(templateType));
			if (notifyConfig == null || notifyConfig.MailTemplateId == 0)
			{
				return false;
			}

			var template = _templateService.Get(notifyConfig.MailTemplateId);
			if (template == null)
			{
				return false;
			}

			var specialKeys = new Dictionary<string, string>();
			specialKeys.Add("{meeting_title}", calendar.Title);
			specialKeys.Add("{meeting_date}", calendar.BeginTime.HasValue ? calendar.BeginTime.Value.ToString("hh:mm dd/MM/yyyy") : "");
			specialKeys.Add("{meeting_creator}", userCreate.FullName);
			specialKeys.Add("{meeting_body}", "");
			specialKeys.Add("{meeting_resource}", calendar.Place);
			specialKeys.Add("{meeting_users}", "");
			specialKeys.Add("{ngay_thang_hien_tai}", DateTime.Now.ToShortTimeString() + " " + DateTime.Now.ToShortDateString());
			// ... 

			foreach (var key in specialKeys.Keys)
			{
				template.Content = template.Content.Replace(key, specialKeys[key]);
			}

			//var content = _templateHelper.ParseContentNew(template, 0, null, null, null, null, specialKeys);
			var content = template.Content;
			var subject = template.TitleMail;
			var receiveds = userReceiveds;// người gửi mail

			CreateQueueMail(subject, receiveds, content, userSend: null);

			return true;
		}

		#endregion

		#region Private Methods

		private List<string> EnsureReceivedMails(List<string> mails)
		{
			var result = new List<string>();
			foreach (var mail in mails)
			{
				var ensureMail = EnsureReceivedMail(mail);
				if (string.IsNullOrEmpty(ensureMail))
				{
					continue;
				}

				result.Add(ensureMail);
			}

			return result;
		}

		private string EnsureReceivedMail(string mail)
		{
			if (string.IsNullOrEmpty(mail) || !mail.IsEmailAddress())
			{
				return string.Empty;
			}

#if DEBUG
            // TienBV: Để hạn chế việc gửi email linh tinh khi fix lỗi cho khách hàng.
            // Việc gửi email khi code sẽ fix cứng địa chỉ mail ở đây.
            // Sửa lại thành địa chỉ mail muốn debug tương ứng

            //mail = "congquoc412@gmail.com";
            //mail = "vietdungit95@gmail.com";
            //mail = "tienbv@bkav.com";
            mail = "hiepn4759@gmail.com";
#endif
            return mail;
		}

		private string GetMailContent(NotifyConfigType type, int userId, Document doc, Supplementary supp, out string subject)
		{
			var result = string.Empty;
			subject = string.Empty;

			var templateType = type.ToString();
			var notifyConfig = _notifyConfigService.Get(n => n.Key.Equals(templateType));
			if (notifyConfig == null || !notifyConfig.HasAutoSendMail || notifyConfig.MailTemplateId == 0)
			{
				return result;
			}

			var template = _templateService.Get(notifyConfig.MailTemplateId);
			if (template == null)
			{
				return result;
			}

			string paperAddIds = "";
			string feeAddIds = "";

			if (supp != null)
			{
				paperAddIds = supp.PaperIds;
				feeAddIds = supp.FeeIds;
			}

			result = _templateHelper.ParseContentNew(template, userId, doc, null, paperAddIds, feeAddIds);

			subject = template.TitleMail;

			return result;
		}

		private string GetMailContentTemplate(Dictionary<string, string> keys, List<string> attachs, string path, out string subject)
		{
			var fileContents = System.IO.File.ReadAllText(CommonHelper.MapPath(path));
			Regex regexSubject = new Regex(@"<subject>(.*?)<\/subject>", RegexOptions.Singleline);
			MatchCollection collectionSubject = regexSubject.Matches(fileContents);
			subject = collectionSubject[0].Groups[1].Value;
			// xóa chủ đề
			fileContents = fileContents.Replace(collectionSubject[0].Groups[0].Value, "");

			string regularExpressionPatternLoop = @"<each(.*?)>(.*?)<\/each>";
			Regex regex = new Regex(regularExpressionPatternLoop, RegexOptions.Singleline);
			MatchCollection collection = regex.Matches(fileContents);
			for (int i = 0; i < collection.Count; i++)
			{
				var macth = collection[i];
				var value = macth.Groups[0].Value;
				var data = macth.Groups[1].Value.Trim();
				var template = macth.Groups[2].Value;
				var result = "";
				foreach (var att in attachs)
				{
					var tmp = template.Replace("${AttachmentName}", att);
					result += tmp;
				}
				fileContents = fileContents.Replace(value, result);
			}

			foreach (var key in keys)
			{
				fileContents = fileContents.Replace(key.Key, key.Value);
				subject = subject.Replace(key.Key, key.Value);
			}

			return fileContents;
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
				SmtpClient = null;
				_emailSettings = null;
				_notifyConfigService = null;
				_templateService = null;
				_templateHelper = null;
				_mailService = null;
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
