using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ProcessFunctionFilterValidator))]
    public class ProcessFunctionFilterModel
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id bộ lọc
        /// </summary>
        public int ProcessFunctionFilterId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên bộ lọc
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trường dữ liệu cần lọc
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.DataField.Label")]
        public string DataField { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị để lọc.
        /// <para>
        /// Trường hợp Value là biểu thức Sql: trong câu sql cần select ra 2 trường có tên là DataField và TextField;
        /// </para>
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.Value.Label")]
        public string Value { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập giá trị xác định các giá trị lọc được lấy từ database</para>
        /// <para>Với kiểu giá trị này sẽ sinh ra một loạt các ProcessFunctionFilter tương ứng với mỗi giá trị</para>
        /// <para>(Nếu đưa lên node trên tree thì mỗi giá trị tương ứng với 1 node).</para>
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.IsSqlValue.Label")]
        public bool IsSqlValue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập biểu thức lọc
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.FilterExpression.Label")]
        public int FilterExpression { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định có tự động sinh tên node theo giá trị của câu SQL không?
        /// </summary>
        /// <remarks>
        /// Chỉ thiết lập giá trị này = true khi IsSqlValue = true.
        /// </remarks>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.IsAutoGenNodeName.Label")]
        public bool IsAutoGenNodeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu tên của node sẽ sinh ra. Thiết lập theo dạng biểu thức của String.Format;
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionFilter.CreateOrEdit.Fields.NodeNameTemp.Label")]
        public string NodeNameTemp { get; set; }
    }
}