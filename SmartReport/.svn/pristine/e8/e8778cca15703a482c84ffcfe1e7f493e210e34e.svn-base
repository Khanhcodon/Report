using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DynamicFormHelper - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Các hàm chức năng hỗ trợ xử lý biểu mẫu động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class DynamicFormHelper
    {
        /// <summary>
        /// Kiểm tra chuỗi json có đúng định dạng của biểu mẫu động.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="jsControls"></param>
        /// <returns></returns>
        public static bool TryParse(string json, out List<JsControl> jsControls)
        {
            jsControls = new List<JsControl>();

            if (string.IsNullOrEmpty(json))
            {
                return false;
            }

            try
            {
                jsControls = Json2.ParseAsJs<List<JsControl>>(json);//JsonConvert.DeserializeObject<List<JsControl>>(json, new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });
            }
            catch (Exception)
            {
                jsControls = new List<JsControl>();
                return false;
            }
            foreach (var jsControl in jsControls)
            {
                var control = jsControl.Properties.SingleOrDefault(c => c.Id == 15);
                if (control != null)
                {
                    Guid value;
                    control.Value = Guid.TryParse(control.Value, out value) ? value.ToString("N") : control.Value; // control.value sử dụng khi lưu form có label
                }
            }
            return true;
        }

        /// <summary>
        /// Chuyển chuỗi json thành biểu mẫu động.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<JsControl> Parse(string json)
        {
            return Json2.ParseAsJs<IEnumerable<JsControl>>(json);//JsonConvert.DeserializeObject<IEnumerable<JsControl>>(json, new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });
        }

        /// <summary>
        /// Chuyển chuỗi json document thành biểu mẫu động
        /// </summary>
        /// <param name="json">The document json.</param>
        /// <param name="jsDocumentItems">out</param>
        /// <returns>
        /// <para>
        ///     - Return true: nếu parse thành công chuỗi json của document về List(JsDocumentItem).
        /// </para>
        /// <para>
        ///     - Return false: nếu parse không thành công và out = new List(JsDocumentItems).
        /// </para>
        /// </returns>
        public static bool TryParse(string json, out List<JsDocumentItem> jsDocumentItems)
        {
            jsDocumentItems = new List<JsDocumentItem>();
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }
            try
            {
                jsDocumentItems = Json2.ParseAsJs<List<JsDocumentItem>>(json);//JsonConvert.DeserializeObject<List<JsDocumentItem>>(json, new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });
                return true;
            }
            catch (Exception)
            {
                jsDocumentItems = new List<JsDocumentItem>();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<JsDocumentItem> ParseDocJson(string json)
        {
            return Json2.ParseAsJs<IEnumerable<JsDocumentItem>>(json);//JsonConvert.DeserializeObject<IEnumerable<JsDocumentItem>>(json, new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="jsDocuments"></param>
        /// <returns></returns>
        public static bool TryParse(string json, out JsDocument jsDocuments)
        {
            jsDocuments = new JsDocument();
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }
            try
            {
                jsDocuments = Json2.ParseAsJs<JsDocument>(json);//JsonConvert.DeserializeObject<JsDocument>(json, new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Trả về danh sách Id các catalog sử dụng trong biểu mẫu động.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<Guid> GetCatalogIds(string json)
        {
            List<JsControl> jsControls;
            if (!TryParse(json, out jsControls))
            {
                throw new ApplicationException("Chuỗi json không đúng định dạng biểu mẫu động.");
            }
            return jsControls.Where(c => c.TypeId.Equals((int)ControlType.DropDownList) ||
                c.TypeId.Equals((int)ControlType.CheckBoxList)).Select(c => c.ControlId);
        }

        /// <summary>
        /// Trả về danh sách Id các catalog sử dụng trong biểu mẫu động.
        /// </summary>
        /// <param name="jsControls"></param>
        /// <returns></returns>
        public static IEnumerable<Guid> GetCatalogIds(List<JsControl> jsControls)
        {
            if (jsControls == null || jsControls.Count <= 0)
            {
                return new List<Guid>();
            }
            return jsControls.Where(c => c.TypeId.Equals((int)ControlType.DropDownList) ||
                c.TypeId.Equals((int)ControlType.CheckBoxList)).Select(c => c.ControlId);
        }

        /// <summary>
        /// Trả về danh sách Id các extendfield sử dụng trong biểu mẫu động.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<Guid> GetExtendFieldIds(string json)
        {
            List<JsControl> jsControls;
            if (!TryParse(json, out jsControls))
            {
                throw new ApplicationException("Chuỗi json không đúng định dạng biểu mẫu động.");
            }
            return jsControls.Where(c => c.TypeId.Equals((int)ControlType.Textbox)).Select(c => c.ControlId);
        }

        /// <summary>
        /// Trả về danh sách Id các extendfield sử dụng trong biểu mẫu động.
        /// </summary>
        /// <param name="jsControls"></param>
        /// <returns></returns>
        public static IEnumerable<Guid> GetExtendFieldIds(List<JsControl> jsControls)
        {
            if (jsControls == null || jsControls.Count <= 0)
            {
                return new List<Guid>();
            }
            return jsControls.Where(c => c.TypeId.Equals((int)ControlType.Textbox)).Select(c => c.ControlId);
        }

        /// <summary>
        /// Trả về danh sách Id các catalog sử dụng trong biểu mẫu động của document.
        /// </summary>
        /// <param name="jsControls"></param>
        /// <returns></returns>
        public static IEnumerable<Guid> GetCatalogIds(List<JsDocumentItem> jsControls)
        {
            if (jsControls == null || jsControls.Count <= 0)
            {
                return new List<Guid>();
            }
            return jsControls.Where(c => c.TypeId.Equals((int)ControlType.DropDownList) ||
                c.TypeId.Equals((int)ControlType.CheckBoxList)).Select(c => c.ControlId);
        }
    }
}