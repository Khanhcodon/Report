using System;
namespace Bkav.eGovCloud.Core.Caching
{
    /// <summary>
    /// Interface quản lý cache
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Trả về giá trị cache theo key truyền vào hoặc truyền hàm gán giá trị cho cache nếu cache không tồn tại.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu cần lấy hoặc gán cho cache</typeparam>
        /// <param name="key">Key</param>
        /// <param name="acquirer">Hàm thực thi lấy giá trị gán cho cache</param>
        /// <param name="cacheTime">Hạn lưu cache</param>
        /// <returns>Giá trị cache đã lưu</returns>
        /// <remark>
        /// Dữ liệu lấy ra từ cache, nếu được set lại sẽ tự động gán lại vào cache luôn. Nên cần lưu ý khi sử dụng.
        /// </remark>
        T Get<T>(string key, Func<T> acquirer, int? cacheTime = null);

        /// <summary>
        /// Thêm một key và đối tượng vào cache
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Giá trị</param>
        /// <param name="cacheTime">Thời gian cache</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Trả về giá trị xác định key đã tồn tại trong danh sách cache chưa
        /// </summary>
        /// <param name="key">Tên key</param>
        /// <returns>Result</returns>
        bool Contains(string key);

        /// <summary>
        /// Xóa giá trị cache theo key
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// Xóa tất cả cache
        /// </summary>
        void Clear();
    }
}
