namespace Bkav.eGovCloud.Core.Worktime
{
    /// <summary>
    /// Các kiểu tính ngày hẹn trả
    /// </summary>
    public enum WorktimeType
    {
        /// <summary>
        /// Tính bình thường: Tính thời gian xử lý đến 24h
        /// </summary>
        Normar = 1,
        
        /// <summary>
        /// Tính thời gian xử lý theo hồ sơ một cửa
        /// </summary>
        HsmcNormal = 2,

        /// <summary>
        /// Tính thời gian xử lý theo Hồ sơ một cửa: bỏ qua ngày tiếp nhận
        /// </summary>
        IgnoreCreatedDate = 3,

        /// <summary>
        /// Tính thời gian xử lý theo Hồ sơ một cửa: vẫn tính ngày hiện tại nếu tiếp nhận sau 15h
        /// </summary>
        UsingCreatedDateAfter3PM = 4,

        /// <summary>
        /// Tính thời gian xử lý theo Hồ sơ một cửa: tính đến 24h
        /// </summary>
        Hsmc24H = 5,

    }
}
