using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Quản lý các báo cáo
    /// </summary>
    public class Report
    {
        private List<IDictionary<string, int>> _listDepartmentPositionHasPermission;
        private List<int> _listPositionHasPermission;
        private List<int> _listUserHasPermission;

        /// <summary>
        /// Key
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mô tả báo cáo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết  lập tên báo cáo
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục cha của báo cáo
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cấu hình hiển thị báo cáo
        /// </summary>
        public int DocColumnId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập các báo cáo nhóm cho tree [id, id,...]
        /// </summary>
        public string GroupForTree { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn dữ liệu cho báo cáo
        /// </summary>
        public string QueryStatistics { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu query lấy ra tổng số record của báo cáo
        /// </summary>
        public string QueryTotal { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập file crystalreport tương ứng
        /// </summary>
        public string CrystalPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập file crystalreport theo nhóm tương ứng
        /// </summary>
        public string CrystalGroupPath { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên file được lưu trữ trên server
        /// </summary>
        public string FileLocationName { get; set; }

         /// <summary>
        /// Lấy hoặc thiết lập tên file được lưu trữ trên server của nhóm
        /// </summary>
        public string FileLocationNameGroup { get; set; }
        
        /// <summary>
        /// Lấy hoặc thiết lập người được xem báo cáo
        /// <para> Được lưu theo dạng: [id1,id2,...]</para>
        /// </summary>
        public string UserPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập phòng ban được xem báo cáo
        /// <para> Được lưu theo dạng: [{"DepartmentId" : id1, "PositionId" : id2}]</para>
        /// </summary>
        public string DeptPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập chức vụ được xem báo cáo
        /// <para> Được lưu theo dạng: [id1,id2,...]</para>
        /// </summary>
        public string PositionPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái sử dụng báo cáo.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái là label trên tree
        /// </summary>
        public bool IsLabel { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái là HSMC của cây report
        /// </summary>
        public bool IsHSMC { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái là hiển thị tổng số văn bản
        /// </summary>
        public bool IsShowTotal { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo báo cáo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo báo cáo
        /// </summary>
        public int IsFile { get; set; } 
        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ có quyền nhìn thấy báo cáo
        /// </summary>
        public List<int> ListPositionHasPermission
        {
            get
            {
                if (_listPositionHasPermission != null)
                {
                    return _listPositionHasPermission;
                }
                try
                {
                    _listPositionHasPermission = Json2.ParseAs<List<int>>(PositionPermission);
                }
                catch (Exception)
                {
                    _listPositionHasPermission = new List<int>();
                }
                return _listPositionHasPermission;
            }
            set
            {
                _listPositionHasPermission = value;
                PositionPermission = value.StringifyJs();
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ thuộc phòng ban có quyền nhìn thấy báo cáo (Key trong 1 Dictionary gồm DepartmentId và PositionId)
        /// </summary>
        public List<IDictionary<string, int>> ListDepartmentPositionHasPermission
        {
            get
            {
                if (_listDepartmentPositionHasPermission != null)
                {
                    return _listDepartmentPositionHasPermission;
                }
                try
                {
                    _listDepartmentPositionHasPermission = Json2.ParseAs<List<IDictionary<string, int>>>(DeptPermission);
                }
                catch (Exception)
                {
                    _listDepartmentPositionHasPermission = new List<IDictionary<string, int>>();
                }
                return _listDepartmentPositionHasPermission;
            }
            set
            {
                _listDepartmentPositionHasPermission = value;
                DeptPermission = value.StringifyJs();
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các người dùng có quyền nhìn thấy function
        /// </summary>
        public List<int> ListUserHasPermission
        {
            get
            {
                if (_listUserHasPermission != null)
                {
                    return _listUserHasPermission;
                }
                try
                {
                    _listUserHasPermission = Json2.ParseAs<List<int>>(UserPermission);
                }
                catch (Exception)
                {
                    _listUserHasPermission = new List<int>();
                }
                return _listUserHasPermission;
            }
            set
            {
                _listUserHasPermission = value;
                UserPermission = value.StringifyJs();
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các nhóm được đưa lên trêe
        /// </summary>
        public List<int> GroupForTrees
        {
            get
            {
                List<int> result;
                try
                {
                    result = Json2.ParseAs<List<int>>(GroupForTree);
                }
                catch
                {
                    result = new List<int>();
                }
                return result;
            }
            set { GroupForTree = value.StringifyJs(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các báo cáo con của báo cáo hiện tại (bao gồm cả các báo cáo nhóm trên tree)
        /// </summary>
        public List<Report> Childs { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy số văn bản quá hạn
        /// </summary>
        public string QueryTotalDocumentIsOverdue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy số văn bản quá hạn
        /// </summary>
        public string QueryTotalDocumentProcessed { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập GroupId (Xem theo group)
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TreeGroupValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TreeGroupName { get; set; }

        /// <summary>
        /// Cấu hình cột dữ liệu
        /// </summary>
        public string ColumnConfig { get; set; }
    }
}
