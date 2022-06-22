using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Dữ liệu thống kê
    /// </summary>
    public class ProgressStatisticXlvb
    {
        /// <summary>
        /// C'tor
        /// </summary>
        public ProgressStatisticXlvb()
        {
            Children = new List<ProgressStatisticXlvb>();
            UserId = 0;
        }

        /// <summary>
        /// Tên phòng ban, người dùng, hình thức văn bản
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tồn kỳ trước
        /// </summary>
        public int PreExtisting { get; set; }
        
        /// <summary>
        /// Tồn kỳ trước - quá hạn
        /// </summary>
        public int PreExtisting_Overdue { get; set; }

        /// <summary>
        /// Nhận trong kỳ
        /// </summary>
        public int NewReception { get; set; }

        /// <summary>
        /// Tổng xử lý
        /// </summary>
        public int Total { get; set; }

        #region Đã xử lý

        /// <summary>
        /// Đã xử lý
        /// </summary>
        public int TotalSolved { get; set; }

        /// <summary>
        /// Đã xử lý đúng hạn
        /// </summary>
        public int SolvedInTime { get; set; }
        
        /// <summary>
        /// Đã xử lý đúng hạn - để biết
        /// </summary>
        public int SolvedInTime_DeBiet { get; set; }

        /// <summary>
        /// Đã xử lý đúng hạn - Xử lý
        /// </summary>
        public int SolvedInTime_XuLy { get; set; }

        /// <summary>
        /// Đã xử lý quá hạn
        /// </summary>
        public int SolvedLate { get; set; }

        /// <summary>
        /// Đã xử lý quá hạn_DeBiet
        /// </summary>
        public int SolvedLate_DeBiet { get; set; }

        /// <summary>
        /// Đã xử lý quá hạn_XuLy
        /// </summary>
        public int SolvedLate_XuLy { get; set; }

        #endregion

        #region Chưa xử lý

        /// <summary>
        /// Chưa xử lý
        /// </summary>
        public int TotalPending { get; set; }

        /// <summary>
        /// Chưa xử lý đúng hạn
        /// </summary>
        public int Pending { get; set; }

        /// <summary>
        /// Chưa xử lý quá hạn
        /// </summary>
        public int PendingLate { get; set; }

        #endregion

        /// <summary>
        /// Laf nhom cha
        /// </summary>
        public bool IsParent { get; set; }

        public bool IsShowAll { get; set; }

        /// <summary>
        /// Laf nhom cha
        /// 1. là nhóm phòng ban
        /// 2. là nhóm người dùng
        /// </summary>
        public int TypeStatistics { get; set; }

        /// <summary>
        /// Là UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Laf nhom cha
        /// NHóm phòng ban người dùng
        /// </summary>
        public string ParrentName { get; set; }

        /// <summary>
        /// Chi tiết
        /// </summary>
        public List<ProgressStatisticXlvb> Children { get; set; }
    }
}
