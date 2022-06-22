using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ShareFolderValidator))]
    public class ShareFolderModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ShareFolderId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục chia sẻ
        /// </summary>
        [LocalizationDisplayName("ShareFolder.CreateOrEdit.Fields.Directory.Label")]
        public string Directory { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tài khoàn đăng nhập vào thư mục chia sẻ
        /// </summary>
        [LocalizationDisplayName("ShareFolder.CreateOrEdit.Fields.UserName.Label")]
        public string UserName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mật khẩu đăng nhập vào thư mục chia sẻ
        /// </summary>
        [LocalizationDisplayName("ShareFolder.CreateOrEdit.Fields.Password.Label")]
        public string Password { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập thư mục được chia sẻ qua mạng
        /// </summary>
        [LocalizationDisplayName("ShareFolder.CreateOrEdit.Fields.IsNetwork.Label")]
        public bool IsNetwork { get; set; }
    }
}