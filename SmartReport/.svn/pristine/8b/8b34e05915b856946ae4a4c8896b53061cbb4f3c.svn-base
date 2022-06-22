using Bkav.eGovCloud.Core.Caching;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Chạy xử lý các tác vụ trong hàng đợi
    /// </summary>
    public class eGovQueueJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;
        private MemoryCacheManager _cache;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public eGovQueueJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="cache"></param>
        public eGovQueueJob(List<string> connectionStrings, MemoryCacheManager cache)
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            _connections = connectionStrings.Select(c => new MySqlConnection(c));
            _cache = cache;

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
