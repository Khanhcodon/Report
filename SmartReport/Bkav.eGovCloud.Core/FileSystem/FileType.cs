namespace Bkav.eGovCloud.Core.FileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// File log
        /// </summary>
        Log,
        /// <summary>
        /// File đính kèm
        /// </summary>
        Attach,

        /// <summary>
        /// Báo cáo
        /// </summary>
        Report,

        /// <summary>
        /// File đính kèm cho giấy phép
        /// </summary>
        LicenseAttach,

        /// <summary>
        /// Tài liệu của hồ sơ cá nhân
        /// </summary>
        StorePrivateAttach,

        /// <summary>
        /// File văn bản quy phạm
        /// </summary>
        Law,

        /// <summary>
        /// File hướng dẫn
        /// </summary>
        Guide,

        /// <summary>
        /// Kho file chung 
        /// </summary>
        FileRepository,

        /// <summary>
        /// Góp ý
        /// </summary>
        Issue,

        /// <summary>
        /// Các loại file khác
        /// </summary>
        Others
    }
}
