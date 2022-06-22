using System.ComponentModel;

namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// Những timer đang chạy của egov.
    /// Muốn thêm timer:
    /// - Tạo bussiness cho timer
    /// - Thêm 1 thuộc tính ở đây
    /// - Vào admin để set thời gian chạy cho timer
    /// </summary>
    public enum EgovJobEnum
    {
        /// <summary>
        /// IndexTimerElapsed
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.indextimerelapsed")]
        IndexTimerElapsed = 1,

        /// <summary>
        /// Kiểm tra những service không hoạt động
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.checkservices")]
        CheckServices = 2,

        /// <summary>
        /// Đồng bộ hồ sơ liên thông
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.getdocumentsfromedoctool")]
        GetDocumentsFromEdocTool = 3,

        /// <summary>
        /// Notify những văn bản chưa đọc
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.notifydocunread")]
        NotifyDocUnread = 4,

        /// <summary>
        /// Notify những văn bản chờ xử lý
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.notifydocinprocesses")]
        NotifyDocInProcesses = 5,

        /// <summary>
        /// Kiểm tra file bị thay đổi
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.checkchangedfile")]
        CheckChangedFile = 6,

        /// <summary>
        /// Đánh index tìm kiếm
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.addindex")]
        AddIndex = 7,

        /// <summary>
        /// Sao luu CSDL
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.backupdatabase")]
        BackupDatabase = 8,

        /// <summary>
        /// Sao luu file
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.backupfile")]
        BackupFile = 9,

        /// <summary>
        /// Gửi mail
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.sendmail")]
        SendMail = 10,

        /// <summary>
        /// Gửi tin nhắn
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.sendsms")]
        SendSms = 11,

        /// <summary>
        /// Reset lại nhảy số theo năm
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.resetIncrease")]
        ResetInCrease = 12,

#if HoSoMotCuaEdition
        /// <summary>
        /// HSMC: Kiểm tra hết hạn bổ sung giấy tờ của hồ sơ hay không
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.checksupplementary")]
        CheckSupplementary = 13,
#endif
        
        /// <summary>
        /// Kiểm tra mail mới
        /// </summary>
        [Description("egovcloud.enum.egovjobenum.CheckNewMail")]
        CheckNewMail = 14,

        /// <summary>
        /// Kiểm tra và gửi cảnh báo quá hạn
        /// </summary>
        [Description("Gửi mail cảnh báo quá hạn iSO")]
        SendWarning = 15,

        /// <summary>
        /// Gửi báo cáo tới dịch vụ công
        /// </summary>
        [Description("Gửi báo cáo tới dịch vụ công")]
        SendReportToDVCService
    }
}