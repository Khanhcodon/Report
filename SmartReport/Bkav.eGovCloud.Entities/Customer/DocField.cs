using Bkav.eGovCloud.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DocField - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng DocField trong CSDL
    /// </summary>
    public class DocField
    {
        // private ICollection<DocType> _docTypes;
        // private ICollection<ListSetting> _listSettings;

        /// <summary>
        /// Lấy hoặc thiết lập Id lĩnh vực 
        /// </summary>
        public int DocFieldId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên lĩnh vực
        /// </summary>
        public string DocFieldName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra lĩnh vực này đã được kích hoạt
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người tạo lĩnh vực
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày tạo lĩnh vực
        /// </summary>
        public DateTime? CreatedOnDate { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người cập nhật lĩnh vực cuối cùng
        /// </summary>
        public int? LastModifiedByUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Ngày cập nhật lĩnh vực cuối cùng
        /// </summary>
        public DateTime? LastModifiedOnDate { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>GiangPN@bkav.com - 290513</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// 
        /// </summary>
        public byte[] VersionByte { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VersionDateTime { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Các loại văn bản, loại hồ sơ liên quan
        ///// </summary>
        //public virtual ICollection<DocType> DocTypes
        //{
        //    get { return _docTypes ?? (_docTypes = new List<DocType>()); }
        //    set { _docTypes = value; }
        //}

        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///Tên file icon 
        /// </summary>
        public string IconFileName { get; set; }

        /// <summary>
        ///Tên file icon hiển thị
        /// </summary>
        public string IconFileDisplayName { get; set; }

        ///// <summary>
        ///// Lấy hoặc thiết lập Các cấu hình danh sách
        ///// </summary>
        //public virtual ICollection<ListSetting> ListSettings
        //{
        //    get { return _listSettings ?? (_listSettings = new List<ListSetting>()); }
        //    set { _listSettings = value; }
        //}

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id StoreId (json)
        /// </summary>
        public string StoreIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id Docfield 
        /// </summary>
        public List<int> ListStoreIds
        {
            get
            {
                List<int> result;
                try
                {
                    result = Json2.ParseAs<List<int>>(StoreIds);
                }
                catch
                {
                    result = new List<int>();
                }
                return result;
            }
            set { StoreIds = value.StringifyJs(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<DocfieldDoctypeWorkflow> DocfieldDoctypeWorkflows { get; set; }
    }
}
