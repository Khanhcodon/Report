using Bkav.eGovCloud.DataAccess;
using Bkav.eGovCloud.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Hosting;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Đồng bộ văn bản liên thông, hồ sơ online
    /// </summary>
    public class SyncDocumentJob : JobBase, IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;
        private IEnumerable<MySqlConnection> _connections;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="connectionStrings">Danh sách chuỗi kết nối csdl</param>
        public SyncDocumentJob(IEnumerable<string> connectionStrings)
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
                    ThreadPool.QueueUserWorkItem((data) =>
                    {
                        try
                        {
                            SyncDocument((MySqlConnection)data);
                        }
                        catch (Exception ex)
                        {
                            Log(ex);
                        }
                    }, connection);
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
        /// <param name="immediate"></param>
        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }

        private void SyncDocument(MySqlConnection connection)
        {
            IEnumerable<dynamic> pendingDocuments;
            using (var context = new EfContext(connection))
            {
                var getPendingQuery = "SELECT * from doc_publish WHERE IsPending = 1 AND IsHsmc = 1 AND HasLienThong = 1 " +
                                        "AND HasSendFail = 0 AND AddressCode is not null AND AddressId is not null;";
                pendingDocuments = context.RawQuery(getPendingQuery);


            }
        }

    }
}
