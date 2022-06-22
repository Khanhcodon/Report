using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Core.Utils;
using Bkav.eGovCloud.Entities;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(AuthorizeValidator))]
    public class AuthorizeModel
    {
        public AuthorizeModel()
        {
            DateBegin = DateTime.Now;
            DateEnd = DateTime.Now;
            Active = true;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của ủy quyền
        /// </summary>
        public int AuthorizeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizeUser.Label")]
        public int AuthorizeUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người được ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.AuthorizedUser.Label")]
        public int AuthorizedUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày bắt đầu hiệu lực ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.DateBegin.Label")]
        public DateTime DateBegin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hết hiệu lực ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.DateEnd.Label")]
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ghi chú
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.Note.Label")]
        public string Note { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra ủy quyền này còn hiệu lực
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu ủy quyền còn hiệu lực; ngược lại, <c>false</c>.
        /// </value>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.Active.Label")]
        public bool Active { get; set; }

        public string DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id loại hồ sơ
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.DocTypeId.Label")]
        public List<Guid> DocTypes
        {
            get
            {
                List<Guid> result;
                try
                {
                    result = Json2.ParseAs<List<Guid>>(DocTypeId);
                }
                catch (Exception)
                {
                    result = new List<Guid>();
                }
                return result;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập quyền đối với văn bản
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.Permision.Label")]
        public int Permission { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người ủy quyền
        /// </summary>
        public string AuthorizeUserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người được ủy quyền
        /// </summary>
        public string AuthorizedUserName { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập danh mục quyền đối với văn bản</para>
        /// <para>DungHV@bkav.com - 060913</para>
        /// </summary>
        public PermissionTypes PermissionTypesInEnum { get { return (PermissionTypes)Permission; } }

        /// <summary>
        /// <para>Lấy hoặc thiết lập danh mục quyền đối với văn bản</para>
        /// <para>DungHV@bkav.com - 060913</para>
        /// </summary>
        public List<int> Permissions { get; set; }

        public string DoctypeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập xóa toàn bộ cấu hình trước nếu tồn tại
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.Admin.Authorize.CreateOrEdit.Fields.HasDeleteExist.Label")]
        public bool HasDeleteExist { get; set; }
    }
}