using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov - eOffice team</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : PermissionSetting - public - Entity</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 241115</para>
    /// <para>Author      : HopCV</para>
    /// <para>Description : Entity tương ứng với bảng PermissionSetting trong CSDL, dùng để cấu hình các node trong cây văn bản đang xử lý trên trang chủ</para>
    /// </summary>
    public class PermissionSetting
    {
        private List<IDictionary<string, int>> _listDepartmentPositionHasPermission;
        private List<int> _listPositionHasPermission;
        private List<int> _listUserHasPermission;

        /// <summary>
        /// Lấy hoặc thiết lập Id function
        /// </summary>
        public int PermissionSettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên function
        /// </summary>
        public string PermissionSettingName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ thuộc phòng ban có quyền nhìn thấy function (json)
        /// </summary>
        public string DepartmentPositionHasPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ thuộc phòng ban có quyền nhìn thấy function (Key trong 1 Dictionary gồm DepartmentId và PositionId)
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
                    _listDepartmentPositionHasPermission = Json2.ParseAs<List<IDictionary<string, int>>>(DepartmentPositionHasPermission);
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
                DepartmentPositionHasPermission = value.StringifyJs();
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ có quyền nhìn thấy function (json)
        /// </summary>
        public string PositionHasPermission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các chức vụ có quyền nhìn thấy function
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
                    _listPositionHasPermission = Json2.ParseAs<List<int>>(PositionHasPermission);
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
                PositionHasPermission = value.StringifyJs();
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách các người dùng có quyền nhìn thấy function (json)
        /// </summary>
        public string UserHasPermission { get; set; }

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
                    _listUserHasPermission = Json2.ParseAs<List<int>>(UserHasPermission);
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
                UserHasPermission = value.StringifyJs();
            }
        }
    }
}
