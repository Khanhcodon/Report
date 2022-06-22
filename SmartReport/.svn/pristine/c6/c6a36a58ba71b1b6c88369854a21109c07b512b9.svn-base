using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Core.Caching
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : MemoryCacheManager - public - Core
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH@bkav.com
    /// </author>
    /// <summary>
    /// <para>1 thư viện quản lý memory cache...</para>
    /// (TrungVH@bkav.com - 270612)
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        private ICache _cache;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public MemoryCacheManager(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Trả về giá trị cache theo key truyền vào hoặc truyền hàm gán giá trị cho cache nếu cache không tồn tại.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu cần lấy hoặc gán cho cache</typeparam>
        /// <param name="key">Key</param>
        /// <param name="acquirer">Hàm thực thi lấy giá trị gán cho cache</param>
        /// <param name="cacheTime">Hạn lưu cache</param>
        /// <returns>Giá trị cache đã lưu</returns>
        public T Get<T>(string key, Func<T> acquirer, int? cacheTime = null)
        {
            return _cache.Get<T>(key, acquirer, cacheTime);
        }

        /// <summary>
        /// Lấy hoặc gán giá trị cho cache với 1 key xác định
        /// </summary>
        /// <typeparam name="T">Kiểu</typeparam>
        /// <param name="key">Key của giá trị cần lấy.</param>
        /// <returns>Giá trị với 1 key xác định.</returns>
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        /// <summary>
        /// Thêm một key và đối tượng vào cache
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Giá trị</param>
        /// <param name="cacheTime">Thời gian cache</param>
        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            _cache.Set(key, data, cacheTime);
        }

        /// <summary>
        /// Lấy ra 1 giá trị chỉ ra rằng key này đã được lưu vào cache
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true nếu key đã tồn tại trong cache và ngược lại</returns>
        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        /// <summary>
        /// Xóa 1 giá trị trong cache từ 1 key xác định
        /// </summary>
        /// <param name="key">/key</param>
        public void Remove(string key)
        {
            _cache.Remove(key);
        }
        
        /// <summary>
        /// Xóa tất cả dữ liệu trong cache
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        /// <summary>
        /// Trả về dung lượng bộ nhơ scache
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetSizeOfMemories()
        {
            return _cache.GetSizeOfMemories();
        }
    }
}
