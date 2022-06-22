using System.ComponentModel;
namespace Bkav.eGovCloud.Entities.Enum
{
    /// <summary>
    /// Các nhà cung cấp dịch vụ nhắn tin SMS
    /// </summary>
    public enum SmsVendor
    {
        /// <summary>
        /// Dịch vụ nhắn tin của Bkav
        /// </summary>
        [Description("Dịch vụ nhắn tin của Bkav")]
        Bkav,

        /// <summary>
        /// Dịch vụ nhắn tin của Viettel
        /// </summary>
        [Description("Dịch vụ nhắn tin của Viettel")]
        Viettel,

        /// <summary>
        /// Dịch vụ nhắn tin của VNPT
        /// </summary>
        [Description("Dịch vụ nhắn tin của VNPT")]
        Vnpt,

        /// <summary>
        /// Dịch vụ nhắn tin qua modem của Bkav eGov
        /// </summary>
        [Description("Dịch vụ nhắn tin qua modem của Bkav eGov")]
        Modem,

        /// <summary>
        /// Dịch vụ nhắn tin qua modem của Bkav eGov
        /// </summary>
        [Description("Dịch vụ nhắn tin qua modem của Viễn thông di động")]
        VTDD
    }
}
