using System;
using System.Threading;
using System.Diagnostics;
using Bkav.eGovCloud.Core.Utils.Threadings;

namespace Bkav.eGovCloud.Core.Utils
{
    /// <summary>
    /// <para>Lớp mở rộng cho ReaderWriterLockSlim</para>
    /// ReaderWriterLockSlim: đại diện cho một khóa được sử dụng để truy cập tài nguyên (ví dụ như đọc ghi file), cho phép nhiều Threads có thể truy cập đọc hoặc độc quyền ghi.
    /// </summary>
    /// <remarks>
    /// Sử dụng ReaderWriterLockSlim để bảo vệ tài nguyên được Reading bởi nhiều Threads hoặc được Written với một thread ở một thời điểm.
    /// Tức là cho phép nhiều Thread có thể truy cập ở chế độ "Đọc" cùng lúc và chỉ một thread được truy cập ở chế độ "Ghi",
    /// và cho phép một thread có thể nâng cấp chế độ (Upgradable) từ Đọc sang Ghi mà không làm mất quyền đọc của mình.
    /// </remarks>
    public static class LockExtensions
    {
        /// <summary>
        /// Lấy một khóa Đọc và sử dụng với từ khóa using cho phép tự hủy
        /// </summary>
        [DebuggerStepThrough]
        public static IDisposable GetReadLock(this ReaderWriterLockSlim rwLock)
        {
            return rwLock.GetReadLock(-1);
        }

        /// <summary>
        /// Lấy một khóa Đọc và sử dụng với từ khóa using cho phép tự hủy
        /// </summary>
        /// <param name="rwLock"></param>
        /// <param name="millisecondsTimeout">
        /// Số mini giây chờ, đặt bằng -1 cho phép chờ vô hạn.
        /// </param>
        [DebuggerStepThrough]
        public static IDisposable GetReadLock(this ReaderWriterLockSlim rwLock, int millisecondsTimeout)
        {
            bool acquire = rwLock.IsReadLockHeld == false ||
                           rwLock.RecursionPolicy == LockRecursionPolicy.SupportsRecursion;

            if (acquire)
            {
                if (rwLock.TryEnterReadLock(millisecondsTimeout))
                {
                    return new ReadLockDisposable(rwLock);
                }
            }

            return ActionDisposable.Empty;
        }

        /// <summary>
        /// Lấy một khóa Đọc cho phép nâng cấp (Upgradeable) sang chế độ Ghi và sử dụng với từ khóa using để tự hủy
        /// </summary>
        [DebuggerStepThrough]
        public static IDisposable GetUpgradeableReadLock(this ReaderWriterLockSlim rwLock)
        {
            return rwLock.GetUpgradeableReadLock(-1);
        }

        /// <summary>
        /// Lấy một khóa Đọc cho phép nâng cấp (Upgradeable) sang chế độ Ghi và sử dụng với từ khóa using để tự hủy
        /// </summary>
        /// <param name="rwLock"></param>
        /// <param name="millisecondsTimeout">
        /// Số mini giây chờ, đặt bằng -1 cho phép chờ vô hạn.
        /// </param>
        [DebuggerStepThrough]
        public static IDisposable GetUpgradeableReadLock(this ReaderWriterLockSlim rwLock, int millisecondsTimeout)
        {
            bool acquire = rwLock.IsUpgradeableReadLockHeld == false ||
                           rwLock.RecursionPolicy == LockRecursionPolicy.SupportsRecursion;

            if (acquire)
            {
                if (rwLock.TryEnterUpgradeableReadLock(millisecondsTimeout))
                {
                    return new UpgradeableReadLockDisposable(rwLock);
                }
            }

            return ActionDisposable.Empty;
        }

        /// <summary>
        /// Lấy một khóa Ghi và sử dụng với từ khóa using cho phép tự hủy
        /// </summary>
        [DebuggerStepThrough]
        public static IDisposable GetWriteLock(this ReaderWriterLockSlim rwLock)
        {
            return rwLock.GetWriteLock(-1);
        }

        /// <summary>
        /// Lấy một khóa Ghi và sử dụng với từ khóa using cho phép tự hủy
        /// </summary>
        /// <param name="rwLock">this</param>
        /// <param name="millisecondsTimeout">
        /// Số mini giây chờ, đặt bằng -1 cho phép chờ vô hạn.
        /// </param>
        [DebuggerStepThrough]
        public static IDisposable GetWriteLock(this ReaderWriterLockSlim rwLock, int millisecondsTimeout)
        {
            bool acquire = rwLock.IsWriteLockHeld == false ||
                           rwLock.RecursionPolicy == LockRecursionPolicy.SupportsRecursion;

            if (acquire)
            {
                if (rwLock.TryEnterWriteLock(millisecondsTimeout))
                {
                    return new WriteLockDisposable(rwLock);
                }
            }

            return ActionDisposable.Empty;
        }

    }

}
