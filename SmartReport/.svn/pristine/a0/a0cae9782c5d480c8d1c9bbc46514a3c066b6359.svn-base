using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Areas.Admin.Validator;
using Bkav.eGovCloud.Web.Framework;

namespace Bkav.eGovCloud.Areas.Admin.Models
{
    [FluentValidation.Attributes.Validator(typeof(PrinterValidator))]
    public class PrinterModel
    {
        /// <summary>
        /// Id của máy in
        /// </summary>
        public int PrinterId { get; set; }

        /// <summary>
        ///Lấy hoặc thiết lập tên của máy in
        /// </summary>
        [LocalizationDisplayName("Printer.CreateOrEdit.Fields.PrinterName.Label")]
        public string PrinterName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập máy in có share hay không
        /// </summary>
        [LocalizationDisplayName("Printer.CreateOrEdit.Fields.IsShared.Label")]
        public bool IsShared { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập  tên máy in  khi share qua mạng
        /// </summary>
        [LocalizationDisplayName("Printer.CreateOrEdit.Fields.ShareName.Label")]
        public string ShareName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập cho phép hiển thị hoặc ẩn máy in 
        /// </summary>
        [LocalizationDisplayName("Printer.CreateOrEdit.Fields.IsActivated.Label")]
        public bool IsActivated { get; set; }

        /// <summary>
        /// Danh sách user mặc định dùng máy in
        /// </summary>
        public string UserIds { get; set; }

        /// <summary>
        /// Danh sách vị trí mặc định dùng máy in
        /// </summary>
        public string DepartmentPositions { get; set; }
    }
}