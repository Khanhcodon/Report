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

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class SendSmsJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public SendSmsJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings"></param>
        public SendSmsJob(List<string> connectionStrings)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            _connections = connectionStrings.Select(c => new MySqlConnection(c));
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
                        SendSms(connection);
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

        private void SendSms(MySqlConnection connection)
        {
            var smsSettings = GetSmsSettings(connection);
            var isActivated = smsSettings["smssettings.isactivated"] == "True";
            if (!isActivated)
            {
                return;
            }

            var pendings = GetPendings(connection);
            if (pendings == null || !pendings.Any())
            {
                return;
            }

            var smsService = GetServiceSMS(smsSettings);

            var serviceLogs = new List<string>();
            foreach (var sms in pendings)
            {
                var id = sms.SmsId;
                var failCount = ++sms.CountSendFail;
                var phone = sms.PhoneNumber.ToString();

#if DEBUG
                phone = "0912342342"; //"0914252584"; // "0973909395"; // "0919287970"; // "0914252584";
#endif

                if (!StringExtension.IsMobilePhoneNumber(phone))
                {
                    UpdateFail(id, 5, connection);
                    continue;
                }

                var isSent = Send(smsService, phone, sms.Message);
                if (isSent)
                {
                    UpdateSuccess(id, connection);
                }
                else
                {
                    UpdateFail(id, failCount, connection);
                }

                // Log gọi service ra bên ngoài
                serviceLogs.Add(string.Format("Sms {0}: {1} - {2}", DateTime.Now.ToString("hh:mm:ss ddMMyyyy"), sms.PhoneNumber, sms.Message));
            }

            if (serviceLogs.Any())
            {
                LogService(serviceLogs);
            }
        }

        private bool Send(ISmsServiceHelper endpoint, string phone, string message)
        {
            try
            {
                return endpoint.SendSms(phone, message);
            }
            catch
            {
                return false;
            }
        }

        private Dictionary<string, string> GetSmsSettings(MySqlConnection connection)
        {
            var result = new Dictionary<string, string>();

            using (var context = new EfContext(connection))
            {
                var smsSettings = context.RawQuery("SELECT SettingKey, SettingValue FROM `setting` WHERE SettingKey LIKE 'smssettings%';");
                foreach (var setting in smsSettings)
                {
                    result.Add(setting.SettingKey, setting.SettingValue);
                }

                return result;
            }
        }

        private ISmsServiceHelper GetServiceSMS(Dictionary<string, string> smsSettings)
        {
            ISmsServiceHelper smsClient = null;
            var smsVendor = smsSettings["smssettings.smsvendor"];
            var serviceUrl = smsSettings["smssettings.serviceurl"];
            var servicePass = EncryptionHelper.Decrypt(smsSettings["smssettings.servicepass"]);
            var serviceUser = smsSettings["smssettings.serviceuser"];
            var alias = smsSettings["smssettings.alias"];
            var code = smsSettings["smssettings.servicecode"];

            switch (smsVendor)
            {
                case "VTDD":
                    smsClient = new VTDDSmsService(serviceUser, servicePass, code, alias);
                    break;
                case "Bkav":
                    break;
                case "Vnpt":
                    smsClient = new VnptSmsService(serviceUser, servicePass, code, alias);
                    break;
                case "Modem":
                    break;
                case "Viettel":
                    smsClient = new ViettelSmsService(serviceUrl, serviceUser, servicePass, code, alias);
                    break;
                default:
                    break;
            }

            if (smsClient == null)
            {
                smsClient = new ViettelSmsService(serviceUrl, serviceUser, servicePass, code, alias);
            }

            return smsClient;
        }

        private IEnumerable<dynamic> GetPendings(MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                return context.RawQuery("SELECT * FROM sms WHERE IsSent = 0 AND HasSendFail = 0;");
            }
        }

        private void UpdateSuccess(int id, MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                var parameters = new List<SqlParameter>(); 
                parameters.Add(new SqlParameter("@id", id));
                parameters.Add(new SqlParameter("@date", DateTime.Now));

                var query = "UPDATE sms SET IsSent = 1, DateSend = @date WHERE SmsId = @id;";
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
                            ? "UPDATE sms SET HasSendFail = 1, CountSendFail = 5 WHERE SmsId = @id;"
                            : "UPDATE sms SET CountSendFail = @countFail WHERE SmsId =  @id;";

                context.RawModify(query, parameters.ToArray());
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
