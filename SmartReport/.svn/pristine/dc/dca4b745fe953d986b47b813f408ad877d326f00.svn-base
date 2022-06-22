using Bkav.eGovCloud.Core.Caching;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.DataAccess;
using FluentScheduler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace Bkav.eGovCloud.Business.Tasks
{
    /// <summary>
    /// Job tự động xóa thư mục tạm của eGov
    /// </summary>
    public class EmptyTempFolderJob : IeGovJob
    {
        private readonly object _lock = new object();
        private bool _shuttingDown;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public EmptyTempFolderJob()
        {
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

                EmptyTempFolder();
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

        private void EmptyTempFolder()
        {
            try
            {
                DirectoryUtil.EmptyFolder(ResourceLocation.Default.FileUploadTemp);
                DirectoryUtil.EmptyFolder(ResourceLocation.Default.FileTemp);
            }
            catch
            {
            }

        }
    }
}
