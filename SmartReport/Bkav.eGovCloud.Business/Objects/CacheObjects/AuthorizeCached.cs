using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bkav.eGovCloud.Business.Objects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AuthorizeCached
    {
        /// <summary>
        /// Constructer
        /// </summary>
        public AuthorizeCached()
        {
            this.Permission = (int)PermissionTypes.XLy;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của ủy quyền
        /// </summary>
        public int AuthorizeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người ủy quyền
        /// </summary>
        public int AuthorizeUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người được ủy quyền
        /// </summary>
        public int AuthorizedUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày bắt đầu hiệu lực ủy quyền
        /// </summary>
        public DateTime DateBegin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hết hiệu lực ủy quyền
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra ủy quyền này còn hiệu lực
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu ủy quyền còn hiệu lực; ngược lại, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại hồ sơ
        /// </summary>
        public string DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập danh sách Id người xem
        /// </summary>
        public List<Guid> DocTypes
        {
            get
            {
                List<Guid> result = null;
                try
                {
                    if (!string.IsNullOrEmpty(DocTypeId))
                    {
                        result = Json2.ParseAs<List<Guid>>(DocTypeId);
                    }
                }
                catch (Exception)
                {
                    result = new List<Guid>();
                }

                return result;
            }
            set { DocTypeId = value.StringifyJs(); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập quyền đối với văn bản
        /// </summary>
        public int Permission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người ủy quyền
        /// </summary>
        public string AuthorizeUserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người được ủy quyền
        /// </summary>
        public string AuthorizedUserName { get; set; }
    }
}
