using System;
using System.Threading;

namespace Bkav.eGovCloud.Core.Utils.Threadings
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WriteLockDisposable : IDisposable
    {
        // Fields
        private readonly ReaderWriterLockSlim _rwLock;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rwLock"></param>
        public WriteLockDisposable(ReaderWriterLockSlim rwLock)
        {
            this._rwLock = rwLock;
        }

        void IDisposable.Dispose()
        {
            this._rwLock.ExitWriteLock();
        }
    }
}