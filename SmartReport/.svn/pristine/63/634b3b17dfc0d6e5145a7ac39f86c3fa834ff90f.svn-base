using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ProcessFunctionTypeValidator))]
    public class ProcessFunctionTypeModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id loại function
        /// </summary>
        public int ProcessFunctionTypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên loại function
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Câu query để lấy ra loại
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Query.Label")]
        public string Query { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Cột hiển thị
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.TextField.Label")]
        public string TextField { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên tham số điều kiện
        /// </summary>
        [LocalizationDisplayName("ProcessFunction.ProcessFunctionType.CreateOrEdit.Fields.Param.Label")]
        public string Param { get; set; }
    }
}