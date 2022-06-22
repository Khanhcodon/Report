using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(FileLocationValidator))]
    public class FileLocationModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id vị trí lưu file
        /// </summary>
        public int FileLocationId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí lưu file
        /// </summary>
        [LocalizationDisplayName("FileLocation.CreateOrEdit.Fields.FileLocationAddress.Label")]
        public string FileLocationAddress { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu đọc ghi file, qua service (true), tại local (false)
        /// </summary>
        [LocalizationDisplayName("FileLocation.CreateOrEdit.Fields.FileLocationType.Label")]
        public bool FileLocationType { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập 1 giá trị chỉ ra vị trí lưu file đang được sử dụng
        /// </summary>
        [LocalizationDisplayName("FileLocation.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }
    }
}
