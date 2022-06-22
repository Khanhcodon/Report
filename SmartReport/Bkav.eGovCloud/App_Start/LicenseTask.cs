using System;
using System.Threading;
using Bkav.eGovCloud.Core.Domain.License;
using Bkav.eGovCloud.Core.FileSystem;
using Bkav.eGovCloud.LicenseService;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud
{
    internal class LicenseTask : IDisposable
    {
        private Timer _timer;
        private bool _disposed;
        private LicenseSettings _license;
        private const string LicenseFile = "License.egovkey";

        internal void InitTimer()
        {
            if (_timer == null)
            {
                _timer = new Timer(TimerHandler, null, Interval, Interval);
            }
        }

        private void TimerHandler(object state)
        {
            _timer.Change(-1, -1);
            CheckLicense();
            _timer.Change(Interval, Interval);
        }

        private void CheckLicense()
        {
            var fileName = FileManager.Default.Resolve(LicenseFile);
            if (System.IO.File.Exists(fileName))
            {
                var licenseInfo = LicenseHelper.ReadLicense(fileName);
                if (licenseInfo != null)
                {
                    var motherBoardSerial = WmiHelper.GetMotherBoardSerial();
                    var cpuProcessorId = WmiHelper.GetCpuProcessorId();
                    var diskDriveSerial = WmiHelper.GetDiskDriveSerial();
                    if (!motherBoardSerial.Equals(licenseInfo.MotherBoardSerial) ||
                        !cpuProcessorId.Equals(licenseInfo.CpuProcessorId) ||
                        !diskDriveSerial.Equals(licenseInfo.DiskDriveSerial))
                    {
                        FileManager.Default.Delete(fileName);
                        return;
                    }
                    var service = new LicenseServiceClient();
                    var isValid = service.CheckLicense(licenseInfo.CustomerName, licenseInfo.Phone, licenseInfo.Email,
                        licenseInfo.MotherBoardSerial, licenseInfo.CpuProcessorId, licenseInfo.DiskDriveSerial,
                        licenseInfo.ToDate);
                    if (!isValid)
                    {
                        FileManager.Default.Delete(fileName);
                    }
                }
            }
        }

        private static int Interval
        {
            get
            {
                return 24 * 3600 * 1000;
            }
        }

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose()
        {
            if ((_timer != null) && !_disposed)
            {
                lock (this)
                {
                    _timer.Dispose();
                    _timer = null;
                    _disposed = true;
                }
            }
        }
    }
}