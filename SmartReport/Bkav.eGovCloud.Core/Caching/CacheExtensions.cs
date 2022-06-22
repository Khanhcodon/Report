using System;

namespace Bkav.eGovCloud.Core.Caching
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : CacheExtensions - public - Core
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH@bkav.com
    /// </author>
    /// <summary>
    /// <para>Class cung cấp các hàm extention cho <see cref="CacheExtensions"/> </para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Lấy ra giá trị theo key
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Key</param>
        /// <param name="acquire">1 delegate sẽ chạy để lấy ra kết quả khi mà key không tồn tại trong cache</param>
        /// <typeparam name="T">Kiểu</typeparam>
        /// <returns>Giá trị với kiểu dữ liệu tương ứng</returns>
        public static T Get<T>(this MemoryCacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Lấy ra giá trị theo key
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Key</param>
        /// <param name="cacheTime">Thời gian lưu cache</param>
        /// <param name="acquire">1 delegate sẽ chạy để lấy ra kết quả khi mà key không tồn tại trong cache</param>
        /// <typeparam name="T">Kiểu</typeparam>
        /// <returns>Giá trị với kiểu dữ liệu tương ứng</returns>
        public static T Get<T>(this MemoryCacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.Contains(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = acquire();
            cacheManager.Set(key, result, cacheTime);
            return result;
        }
    }
}
