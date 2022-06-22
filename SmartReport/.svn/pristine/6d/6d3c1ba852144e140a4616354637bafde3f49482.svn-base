using System;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(TreeGroupValidator))]
    public class TreeGroupModel : PacketModel
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeGroupModel()
            : base()
        {
            IsActived = true;
            IsShowUserFullName = false;
            DateCreated = DateTime.Now;
            IsShowTreeName = true;
            HasChildrenContextMenuAdmin = true;
            IsDocumentTree = true;
            IsOtherSystems = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TreeGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên của nhóm cây
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.TreeGroupName.Label")]
        public string TreeGroupName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có lấy tên người dùng hiện tại là tên hiển thị
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.IsShowUserFullName.Label")]
        public bool IsShowUserFullName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái kích hoạt
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.IsActived.Label")]
        public bool IsActived { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái có hiển thị tên trên cây hay không
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.IsShowTreeName.Label")]
        public bool IsShowTreeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  có phải là cây văn bản hay không
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.IsDocumentTree.Label")]
        public bool IsDocumentTree { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập node liên kết với hệ thống khác
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.IsOtherSystems.Label")]
        public bool IsOtherSystems { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập có hiển thị context menu(thêm, sửa, xóa, copy, paste) trong node con hay không
        /// </summary>
        [LocalizationDisplayName("Customer.TreeGroup.CreateOrEdit.Fields.HasChildrenContextMenuAdmin.Label")]
        public bool HasChildrenContextMenuAdmin { get; set; }


        /// <summary>
        /// Lấy hoặc thiết lập ngày tạo
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người tạo
        /// </summary>
        public string UserNameCreated { get; set; }

        /// <summary>
        /// Lấy hoặc thiết ngày có sụ thay đổi giá trên đối tượng
        /// </summary>
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập người thay đổi giá trị trên đối tượng
        /// </summary>
        public string UserNameModified { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trạng thái node là hsmc
        /// </summary>
        public bool IsHsmc { get; set; }
    }
}