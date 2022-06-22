using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using FluentScheduler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Job tự động xóa bảng log
    /// </summary>
    public class EmptyLogTableJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public EmptyLogTableJob()
        {
            // Register this job with the hosting environment.
            // Allows for a more graceful stop of the job, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings"></param>
        public EmptyLogTableJob(List<string> connectionStrings)
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
                    TruncateTableLog(connection);
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

        private void TruncateTableLog(MySqlConnection connection)
        {
            using (var context = new EfContext(connection))
            {
                context.RawModify("TRUNCATE TABLE log;");
            }
        }
    }
}
