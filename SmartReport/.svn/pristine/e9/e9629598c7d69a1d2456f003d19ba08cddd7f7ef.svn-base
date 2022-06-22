using Bkav.eGovCloud.Business.Objects.CacheObjects;
using Bkav.eGovCloud.Core.Document;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// Loại văn bản cache
    /// </summary>
    [Serializable]
    public class DocTypeCached
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid DocTypeId { get; set; }

        /// <summary>
        /// Tên loại văn bản
        /// </summary>
        public string DocTypeName { get; set; }

        /// <summary>
        /// Mã loại văn bản
        /// </summary>
        public string DocTypeCode { get; set; }

        /// <summary>
        /// Tên không dấu
        /// </summary>
        public string Unsigned { get; set; }

        /// <summary>
        /// Nghiệp vụ
        /// </summary>
        public int CategoryBusinessId { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập Id danh mục nghiệp vụ</para>
        /// </summary>
        public CategoryBusinessTypes CategoryBusinessIdInEnum { get { return (CategoryBusinessTypes)CategoryBusinessId; } }

        /// <summary>
        /// Thể loại văn bản
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Trích yếu mặc định
        /// </summary>
        public string CompendiumDefault { get; set; }

        /// <summary>
        /// Lĩnh vực
        /// </summary>
        public int? DocFieldId { get; set; }

        /// <summary>
        /// Tên lĩnh vực
        /// </summary>
        public string DocFieldName { get; set; }

        /// <summary>
        /// Các quyền của Loại văn bản 
        /// </summary>
        public int? DocTypePermission { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DocTypePermissions? DocTypePermissionInEnum
        {
            get
            {
                if (DocTypePermission.HasValue)
                {
                    return (DocTypePermissions)DocTypePermission.Value;
                }

                return null;
            }
        }

        /// <summary>
        /// Cho phép đăng ký trực tuyến
        /// </summary>
        public bool? IsAllowOnline { get; set; }

        /// <summary>
        /// Có được sử dụng không
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// QUy trình
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Mức
        /// </summary>
        public int? LevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ActionLevel { get; set; }

        /// <summary>
        /// Xét quá hạn trên node
        /// </summary>
        public bool HasOverdueInNode { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Tên cơ quan
        /// </summary>
        public int? OfficeId { get; set; }

        /// <summary>
        /// Danh sách Id sổ văn bản.
        /// </summary>
        public IEnumerable<int> StoreIds { get; set; }

        /// <summary>
        /// Danh sách sổ văn bản
        /// </summary>
        public IEnumerable<StoreCached> Stores { get; set; }

        /// <summary>
        /// Map sang DocType
        /// </summary>
        /// <returns></returns>
        public DocType ToEntity()
        {
            return AutoMapper.Mapper.Map<DocTypeCached, DocType>(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsHsmc()
        {
#if HoSoMotCuaEdition
            return CategoryBusinessIdInEnum == CategoryBusinessTypes.Hsmc;
#else
            return false;
#endif
        }
    }
}
