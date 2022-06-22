using Bkav.eGovCloud.DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Chạy xử lý các tác vụ trong hàng đợi
    /// </summary>
    public class ResetIncreateJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public ResetIncreateJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings"></param>
        public ResetIncreateJob(List<string> connectionStrings)
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
            var firstOfYear = new DateTime(DateTime.Now.Year, 1, 1, 0, 5, 59);
            if (DateTime.Now > firstOfYear)
            {
                // Nếu ko phải đang là thời điểm đầu năm
                // Tránh bị build nhầm bản Debug.
                return;
            }

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
                        ResetIncreate(connection);
                    }
                    catch
                    {
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

        private void ResetIncreate(MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                context.RawModify("UPDATE increase SET `Value` = 0;");
            }
        }
    }
}
