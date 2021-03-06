using Bkav.eGovCloud.DataAccess;
using FluentScheduler;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Newtonsoft;
using Bkav.eGovCloud.SmsService;
using Bkav.eGovCloud.Core.Utils;
using System;
using System.IO;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Lịch tự động gửi mail
    /// </summary>
    public class SendEmailJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public SendEmailJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings"></param>
        public SendEmailJob(List<string> connectionStrings)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            _connections = connectionStrings.Select(c => new MySqlConnection(c));
			LogService(new List<string>() { "SendEmailJob" });
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Thực thi
        /// </summary>
        public void Execute()
        {
            try
            {
                lock (_lock)
                {
                    if (_shuttingDown)
                        return;
                }

                foreach (var connection in _connections)
                {
                    try
                    {
                        SendEmail(connection);
                    }
                    catch (Exception ex)
					{
						LogService(new List<string>() { ex.Message });
						continue;
                    }
                }
            }
            finally
            {
                // Always unregister the job when done.
                HostingEnvironment.UnregisterObject(this);
            }
        }

        /// <summary>
        /// Dừng job
        /// </summary>
        /// <param name="immediate">Dừng ngay lập tức khi có lệnh</param>
        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }

        private void SendEmail(MySqlConnection connection)
        {
            var mailSettings = GetEmailSettings(connection);
            var isActivated = mailSettings["emailsettings.isactivated"] == "True";
            if (!isActivated)
            {
                return;
            }

            var endpoint = GetSmtpClient(mailSettings);
            if (endpoint == null)
            {
                return;
            }

            var pendings = GetPendings(connection);
            if (pendings == null || !pendings.Any())
            {
                return;
            }

            foreach (var mail in pendings)
            {
                try
                {
                    var id = mail.MailId;
                    var failCount = ++mail.CountSentFail;
                    if (string.IsNullOrEmpty(mail.SendTo))
                    {
                        continue;
                    }

                    var isSent = Send(endpoint, mail, mailSettings);
                    if (isSent)
                    {
                        UpdateSuccess(id, connection);
                    }
                    else
                    {
                        UpdateFail(id, failCount, connection);
                    }
                }
                catch(Exception ex)
				{
					LogService(new List<string>() { ex.Message });
                }
            }
        }

        private List<System.Net.Mail.Attachment> GetAttachments(string attachmentIds)
        {
            var result = new List<System.Net.Mail.Attachment>();
            if (string.IsNullOrEmpty(attachmentIds))
            {
                return result;
            }

            var attachmentPaths = Json2.ParseAs<Dictionary<string, string>>(attachmentIds);
            if (attachmentPaths.Count == 0)
            {
                return result;
            }

            foreach (var att in attachmentPaths)
            {
                if (!File.Exists(att.Value))
                {
                    continue;
                }

                var stream = new FileStream(att.Value, FileMode.Open, FileAccess.Read);
                result.Add(new Attachment(stream, att.Key));
            }

            return result;
        }

        private bool Send(SmtpClient endpoint, dynamic mail, Dictionary<string, string> emaiSettings)
        {
            try
            {
                var mailto = EnsureReceivedMail((string)mail.SendTo);
                if (!mailto.Any())
                {
                    return false;
                }

                var attachment = GetAttachments((string)mail.AttachmentIdStr);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emaiSettings["emailsettings.smtpusername"], emaiSettings["emailsettings.displayname"], Encoding.UTF8),
                };

                mailMessage.Subject = mail.Subject;

                foreach (var mailAddr in mailto)
                {
                    mailMessage.To.Add(mailAddr);
                }

                mailMessage.Body = mail.Body;
                mailMessage.IsBodyHtml = true;

                foreach (var att in attachment)
                {
                    mailMessage.Attachments.Add(att);
                }

                endpoint.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private List<string> EnsureReceivedMail(string mails)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(mails))
            {
                return result;
            }

            var mailArr = mails.Split(',');
            if (!mailArr.Any())
            {
                return result;
            }

#if DEBUG
            // TienBV: Để hạn chế việc gửi email linh tinh khi fix lỗi cho khách hàng.
            // Việc gửi email khi code sẽ fix cứng địa chỉ mail ở đây.
            // Sửa lại thành địa chỉ mail muốn debug tương ứng

            mailArr = new string[] { "dungnvl@bkav.com", "vietdungit95@gmail.com" };
#endif

            result.AddRange(mailArr.Where(m => m.IsEmailAddress()).Select(m => m));
            return result;
        }

        private Dictionary<string, string> GetEmailSettings(MySqlConnection connection)
        {
            var result = new Dictionary<string, string>();

            using (var context = new EfContext(connection))
            {
                var smsSettings = context.RawQuery("SELECT SettingKey, SettingValue FROM `setting` WHERE SettingKey LIKE 'emailsettings%';");
                foreach (var setting in smsSettings)
                {
                    result.Add(setting.SettingKey, setting.SettingValue);
                }

                return result;
            }
        }

        private SmtpClient GetSmtpClient(Dictionary<string, string> emaiSettings)
        {
            var password = ((string)emaiSettings["emailsettings.smtppassword"]).Base64Decode();
            var result = new SmtpClient()
            {
                EnableSsl = emaiSettings["emailsettings.enablessl"] == "True",
                Host = emaiSettings["emailsettings.smtpserver"],
                Port = int.Parse(emaiSettings["emailsettings.smtpport"]),
                Credentials = new NetworkCredential(emaiSettings["emailsettings.smtpusername"], password)
            };

            return result;
        }

        private IEnumerable<dynamic> GetPendings(MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                return context.RawQuery("SELECT * from `mail` WHERE IsSent = 0 AND HasSentFail = 0;");
            }
        }

        private void UpdateSuccess(int id, MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@id", id));
                parameters.Add(new SqlParameter("@dateSend", DateTime.Now));

                var query = "UPDATE `mail` SET IsSent = 1, DateSend = @dateSend WHERE MailId = @id;";
                context.RawModify(query, parameters.ToArray());
            }
        }

        private void UpdateFail(int id, int countFail, MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@id", id));
                parameters.Add(new SqlParameter("@countFail", countFail));

                var query = countFail >= 5
                            ? "UPDATE `mail` SET HasSentFail = 1, CountSentFail = 5 WHERE MailId = @id;"
                            : "UPDATE `mail` SET CountSentFail = @countFail WHERE MailId = @id;";

                context.RawModify(query);
            }
        }
		
		private void LogService(List<string> message)
		{
			var logFolder = CommonHelper.MapPath("~/Logs");
			var logFile = Path.Combine(logFolder, "logservice_" + DateTime.Now.ToString("ddMMyyyy"));
			try
			{
				System.IO.File.AppendAllLines(logFile, message);
			}
			catch { }
		}
	}
}
