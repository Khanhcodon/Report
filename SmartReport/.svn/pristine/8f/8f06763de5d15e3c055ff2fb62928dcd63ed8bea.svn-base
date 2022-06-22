using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Bkav.eGovCloud.Core.Template
{
    /// <author>
    /// <para> Bkav Corp. - BSO - eGov - eGate Team </para>
    /// <para> Project: eGov Cloud v1.0 </para>
    /// <para> Class : TemplateUtil - public - Core </para>
    /// <para> Access Modifiers: </para>
    /// <para> Create Date : 210313</para>
    /// <para> Author : TienBV@bkav.com </para>
    /// </author>
    /// <summary>
    /// <para> Các hàm mở rộng xử lý biểu mẫu động </para>
    /// <para> ( TienBV@bkav.com - 210313) </para>
    /// </summary>
    public static class TemplateUtil
    {
        private const string Partern = "{[\\w]+}";
        private const string KeyValueForView = "<span id='{1}' class='field-edit'>{0}</span>";

        /// <summary>
        /// Trả về danh sách các key có trong nội dung mẫu.
        /// </summary>
        /// <param name="content">Nội dung mẫu</param>
        /// <returns></returns>
        public static IEnumerable<string> GetKeysInContent(string content)
        {
            var regex = new Regex(Partern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var matches = regex.Matches(content);
            var result = new List<string>();
            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }
            return result.Distinct();
        }

        /// <summary>
        /// Trả về giá trị của key trong hồ sơ.
        /// <para>(Tienbv@bkav.com 280313)</para>
        /// </summary>
        /// <param name="keyCode"> Mã key</param>
        /// <param name="contents"> Nội dung hồ sơ</param>
        /// <returns></returns>
        public static string GetValueInForm(string keyCode, List<DynamicForm.JsDocument> contents)
        {
            var result = KeyValueForView;
            if (!contents.Any())
            {
                return string.Format(result, "", keyCode);
            }
            var keyElements = keyCode.Split('_');
            if (keyElements.Length < 3)
            {
                return string.Format(result, "", keyCode);
            }
            Guid controlId;
            if (!Guid.TryParse(keyElements[keyElements.Length - 2], out controlId))
            {
                return string.Format(result, "", keyCode);
            }
            Guid formId;
            if (!Guid.TryParse(keyElements[keyElements.Length - 1], out formId))
            {
                return string.Format(result, "", keyCode);
            }
            foreach (var content in contents)
            {
                if (!content.FormId.Equals(formId.ToString("N")))
                {
                    continue;
                }
                var control = content.DocFieldJson.SingleOrDefault(f => f.ControlId == controlId);
                if (control == null)
                {
                    continue;
                }
                result = control.Value;
                if (control.TypeId == (int)DynamicForm.ControlType.Textbox)
                {
                    result = string.Format(KeyValueForView, result, keyCode);  // đánh dấu key cho phép sửa
                }
                break;
            }
            return string.Format(result, "", keyCode);
        }
    }
}
