using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;
using System.Linq;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Store - public - Entity
    /// Access Modifiers: 
    /// Create Date : 270612
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Store trong CSDL
    /// </summary>
    public class Store
    {
        private ICollection<StoreCode> _storeCodes;
    
        /// <summary>
        /// Lấy hoặc thiết lập Id tập hồ sơ, kho hồ sơ
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tập hồ sơ, kho hồ sơ
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người phụ trách
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id đơn vị, phòng ban phụ trách
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id người xem (json)
        /// </summary>
        public string UserViewIds { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id người xem
        /// </summary>
        public List<int> ListUserViewIds
        {
            get
            {
                List<int> result;
                try
                {
                    result = Json2.ParseAs<List<int>>(UserViewIds);
                }
                catch (Exception)
                {
                    result = new List<int>();
                    if (string.IsNullOrEmpty(UserViewIds))
                    {
                        return result;
                    }

                    var userIds = UserViewIds.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    result = userIds.Where(u => !string.IsNullOrEmpty(u)).Select(u => Int32.Parse(u)).ToList();
                }
                return result;
            }
            set { UserViewIds = value.StringifyJs(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id Docfield (json)
        /// </summary>
        public string DocFieldIds { get; set; }
        
        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>CuongNT@bkav.com - 120413</para>
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// <para>CuongNT@bkav.com - 120413</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        ///// <summary>
        ///// Lấy hoặc thiết lập Phòng ban phụ trách
        ///// </summary>
        //public virtual Department Department { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Người phụ trách
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id của mẫu sổ hồ sơ
        /// </summary>
        public string CodeIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<StoreCode> StoreCodes
        {
            get { return _storeCodes ?? (_storeCodes = new List<StoreCode>()); }
            set { _storeCodes = value; }
        }
    }
}
