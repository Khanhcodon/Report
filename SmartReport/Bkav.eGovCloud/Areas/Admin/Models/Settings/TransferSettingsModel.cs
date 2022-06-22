using System.Collections.Generic;
namespace Bkav.eGovCloud.Areas.Admin.Models
{
    public class TransferSettingsModel
    {
        public TransferSettingsModel()
        {
            UserReceiveHsmc = "[]";
            UserReceiveVbDen = "[]";
        }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ của service Edoc
        /// </summary>
        public string EdocServiceUrl { get; set; }

        /// <summary>
        /// Tài khoản edoc
        /// </summary>
        public string OrganId { get; set; }

        /// <summary>
        /// Mật khẩu edoc
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Thời gian lấy văn bản mới- tính bằng phút
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Thời gian kiểm tra notify, tính bằng phút
        /// </summary>
        public int TimeForCheckNotify { get; set; }

        /// <summary>
        /// Lấy và thiết lập danh sách người tiếp nhận hồ sơ một cửa liên thông
        /// </summary>
        public string UserReceiveHsmc { get; set; }

        /// <summary>
        /// Lấy và thiết lập danh sách người tiếp nhận văn bản đến liên thông
        /// </summary>
        public string UserReceiveVbDen { get; set; }
    }

    public class UserReceivesModel
    {
        public string EDocId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}