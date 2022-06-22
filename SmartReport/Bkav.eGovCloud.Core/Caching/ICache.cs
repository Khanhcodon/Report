using System;
using System.Collections.Generic;

namespace Bkav.eGovCloud.Core.Caching
{
    /// <summary>
    /// Interface loại cache
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Trả về tất cả entry của cache
        /// </summary>
        IEnumerable<KeyValuePair<string, object>> Entries { get; }

        /// <summary>
        /// Trả về giá trị key theo key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Trả về giá trị cache theo key truyền vào hoặc truyền hàm gán giá trị cho cache nếu cache không tồn tại.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu cần lấy hoặc gán cho cache</typeparam>
        /// <param name="key">Key</param>
        /// <param name="acquirer">Hàm thực thi lấy giá trị gán cho cache</param>
        /// <param name="cacheTime">Hạn lưu cache</param>
        /// <returns>Giá trị cache đã lưu</returns>
        T Get<T>(string key, Func<T> acquirer, int? cacheTime);

        /// <summary>
        /// Thêm một đối tượng vào cache
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="cacheTime">Thời gian lưu cache tính theo phút</param>
        void Set(string key, object value, int? cacheTime);

        /// <summary>
        /// Trả về giá trị xác định key đã tồn tại trong danh sách cache chưa
        /// </summary>
        /// <param name="key">Tên key</param>
        /// <returns>Result</returns>
        bool Contains(string key);

        /// <summary>
        /// Xóa giá trị cache theo key
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// Xóa toàn bộ cache
        /// </summary>
        void Clear();

        /// <summary>
        /// Trả về dung lượng bộ nhớ cache
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetSizeOfMemories();
    }
}
