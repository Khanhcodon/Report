using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Business.Common;
using Bkav.eGovCloud.Business.Customer;
using Bkav.eGovCloud.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Models
{
    [FluentValidation.Attributes.Validator(typeof(AuthorizeValidator))]
    public class AuthorizeModel
    {
        private readonly ResourceBll _resourceService;
        private readonly UserBll _userService;

        public AuthorizeModel()
        {
            _resourceService = DependencyResolver.Current.GetService<ResourceBll>();
            _userService = DependencyResolver.Current.GetService<UserBll>();
            DateBegin = DateTime.Now;
            DateEnd = DateTime.Now.AddDays(1);
            Active = true;
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của ủy quyền
        /// </summary>
        public int AuthorizeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.AuthorizeUser.Label")]
        public int AuthorizeUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Id người được ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.AuthorizedUser.Label")]
        public int AuthorizedUserId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên người được ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.AuthorizedUserName.Label")]
        public string AuthorizedUserName
        {
            get
            {
                string userName = string.Empty;
                if (AuthorizedUserId > 0)
                {
                    userName = _userService.GetFromCache(AuthorizedUserId).Username;
                }
                return userName;
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập ngày bắt đầu hiệu lực ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.DateBegin.Label")]
        public DateTime DateBegin { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ngày hết hiệu lực ủy quyền
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Areas.CreateOrEdit.Fields.DateEnd.Label")]
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập ghi chú
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.Note.Label")]
        public string Note { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra ủy quyền này còn hiệu lực
        /// </summary>
        /// <value>
        /// 	<c>true</c> nếu ủy quyền còn hiệu lực; ngược lại, <c>false</c>.
        /// </value>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.Active.Label")]
        public bool Active { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập loại văn bản
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.DocTypeId.Label")]
        public string DocTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập quyền đối với văn bản
        /// </summary>
        [LocalizationDisplayName("Bkav.eGovCloud.Authorize.CreateOrEdit.Fields.Permision.Label")]
        public int Permission { get; set; }

        /// <summary>
        /// Xem còn hạn hay không
        /// </summary>
        public string HasDateLine
        {
            get
            {
                var date = DateTime.Now;
                if (Active && (date <= DateEnd && date >= DateBegin))
                {
                    return _resourceService.GetResource("Customer.Authorize.ExistDateLine");
                }

                return _resourceService.GetResource("Customer.Authorize.NotExistDateLine");
            }
        }

        /// <summary>
        /// Lấy hoặc thiết lập ngày bắt đầu
        /// </summary>
        public string DateBeginShort
        {
            get { return DateBegin.ToString("dd/MM/yyyy"); }
        }

        /// <summary>
        /// Lấy hoặc thiết lập tên loại hồ sơ
        /// </summary>
        public string DateEndShort
        {
            get { return DateEnd.ToString("dd/MM/yyyy"); }
        }

        /// <summary>
        /// <para>Lấy hoặc thiết lập danh mục quyền đối với văn bản</para>
        /// <para>DungHV@bkav.com - 060913</para>
        /// </summary>
        public List<int> Permissions { get; set; }
    }

}