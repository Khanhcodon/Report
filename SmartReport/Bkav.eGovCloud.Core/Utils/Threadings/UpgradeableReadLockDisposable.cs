using System;
using System.Threading;

namespace Bkav.eGovCloud.Core.Utils.Threadings
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UpgradeableReadLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rwLock"></param>
        public UpgradeableReadLockDisposable(ReaderWriterLockSlim rwLock)
        {
            this._rwLock = rwLock;
        }

        void IDisposable.Dispose()
        {
            this._rwLock.ExitUpgradeableReadLock();
        }
    }
}