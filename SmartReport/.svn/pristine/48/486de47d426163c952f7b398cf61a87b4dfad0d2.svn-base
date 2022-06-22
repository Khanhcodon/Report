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
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business.Tasks
{
    public class GetDocTypeScheduleJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public GetDocTypeScheduleJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings"></param>
        public GetDocTypeScheduleJob(List<string> connectionStrings)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            _connections = connectionStrings.Select(c => new MySqlConnection(c));

            LogService(new List<string>() { "DoctypeScheduleStart" });
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
                        var scheduler = new DocTypeScheduler();
                        var docTypeSchedules = GetDocTypeSchedule(connection);
                        var docTypeScheduleDueDate = GetDocTypeScheduleDueDate(connection);
                        var docTypeScheduleOutOfDate = GetDocTypeScheduleOutOfDate(connection);
                        scheduler.RunSchedule(docTypeSchedules);
                        scheduler.RunScheduleDueTime(docTypeScheduleDueDate);
                        scheduler.RunScheduleOutOfTime(docTypeScheduleOutOfDate);
                    }
                    catch (Exception ex)
                    {
                        //LogService(new List<string>() { ex.Message });
                        LogService(new List<string>() { string.Format("error connection doctypeschedule") });
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

        private List<DocTypeTimeJob> GetDocTypeSchedule(MySqlConnection connection)
        {
            var result = new List<DocTypeTimeJob>();
            using (var context = new EfContext(connection))
            {
                var docTypeSchedules = context.RawQuery("SELECT * FROM `doctype_timejob` WHERE IsActive = 1");
                result = docTypeSchedules.Select(d => new DocTypeTimeJob
                {
                    DocTypeId = d.DocTypeId,
                    DocTypeTimeJobId = d.DocTypeTimeJobId,
                    ScheduleType = d.ScheduleType,
                    ScheduleConfig = d.ScheduleConfig,
                    IsActive = true
                }).ToList();
            }

            return result;
        }

        public List<DocTypeTimeJob> GetDocTypeScheduleDueDate(MySqlConnection connection)
        {
            var result = new List<DocTypeTimeJob>();
            using(var context = new EfContext(connection))
            {
                var docTypeScheduleDueDates = context.RawQuery("SELECT * FROM `doctype_timejob` WHERE isActiveAlert = 1");
                result = docTypeScheduleDueDates.Select(d => new DocTypeTimeJob {
                    DocTypeId = d.DocTypeId,
                    DocTypeTimeJobId = d.DocTypeTimeJobId,
                    ScheduleTypeDueDate = d.ScheduleTypeDueDate,
                    ScheduleConfigDueDate = d.ScheduleConfigDueDate
                }).ToList();
            }
            return result;
        }

        public List<DocTypeTimeJob> GetDocTypeScheduleOutOfDate(MySqlConnection connection)
        {
            var result = new List<DocTypeTimeJob>();
            using (var context = new EfContext(connection))
            {
                var docTypeScheduleDueDates = context.RawQuery("SELECT * FROM `doctype_timejob` WHERE IsActiveAlertOut = 1");
                result = docTypeScheduleDueDates.Select(d => new DocTypeTimeJob
                {
                    DocTypeId = d.DocTypeId,
                    DocTypeTimeJobId = d.DocTypeTimeJobId,
                    ScheduleTypeOutOfDate = d.ScheduleTypeOutOfDate,
                    ScheduleConfigOutOfDate = d.ScheduleConfigOutOfDate
                }).ToList();
            }
            return result;
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
