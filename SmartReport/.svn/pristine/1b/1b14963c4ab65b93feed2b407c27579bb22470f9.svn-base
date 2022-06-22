using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(ReportRuleValidator))]
    public class ReportRuleModel
    {
        private DateTime _date = DateTime.Now;
        private bool _isActived = true;
        /// <summary>
        /// Id van ban quy dinh
        /// </summary>
        [LocalizationDisplayName("")]
        public int ReportRuleId { get; set; }
        /// <summary>
        /// Ma van ban quy dinh
        /// </summary>
        [LocalizationDisplayName("Mã văn bản quy định")]
        public string Code { get; set; }
        /// <summary>
        /// Ten van ban quy dinh
        /// </summary>
        [LocalizationDisplayName("Tên văn bản quy định")]
        public string Name { get; set; }
        /// <summary>
        /// Che do bao cao
        /// </summary>
        [LocalizationDisplayName("Chế độ báo cáo")]
        public string ReportMode { get; set; }
        /// <summary>
        /// Trang thai van ban quy dinh
        /// </summary>
        [LocalizationDisplayName("Trạng thái")]
        public bool IsActive
        {
            get { return _isActived; }
            set { _isActived = value; }
        }
        /// <summary>
        /// Ngay tao van ban quy dinh
        /// </summary>
        [LocalizationDisplayName("Thời gian tạo văn bản quy định")]
        public DateTime DateCreate
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}