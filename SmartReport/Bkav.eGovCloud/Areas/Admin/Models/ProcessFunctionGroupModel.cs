using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ProcessFunctionGroupModelValidator))]
    public class ProcessFunctionGroupModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public int ProcessFunctionGroupId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên kho
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionGroup.CreateOrEdit.Fields.Name.Label")]
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu của kho
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionGroup.CreateOrEdit.Fields.DataQuery.Label")]
        public string DataQuery { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy dữ liệu của kho
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionGroup.CreateOrEdit.Fields.ClientExpression.Label")]
        public string ClientExpression { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập câu truy vấn lấy danh sách các cột (là select top 1 của DataQuery)
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionGroup.CreateOrEdit.Fields.ColumnQuery.Label")]
        public string ColumnQuery { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lậpl
        /// </summary>
        [LocalizationDisplayName("ProcessFunctionGroup.CreateOrEdit.Fields.LimitQuery.Label")]
        public int LimitQuery { get; set; }
    }
}