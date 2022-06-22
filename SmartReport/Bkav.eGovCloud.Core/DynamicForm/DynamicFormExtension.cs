using System;
using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : DynamicFormExtension - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Các hàm mở rộng xử lý biểu mẫu động</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public static class DynamicFormExtension
    {
        /// <summary>
        /// Trả về chuỗi json của biểu mẫu động.<br/>
        /// CuongNT - 061112
        /// </summary>
        /// <param name="jsControls">Cotrol trong biểu mẫu động <see cref="JsFormModel"/></param>
        /// <returns>Chuỗi Json đại diện cho Control</returns>
        public static string ToJsonString(this List<JsControl> jsControls)
        {
            return jsControls.StringifyJs();//JsonConvert.SerializeObject(jsControls);
        }

        /// <summary>
        /// Trả về control có đầy đủ các thuộc tính mới nhất của phiên bản eGov hiện tại.
        /// </summary>
        /// <param name="this">Control trên form động như Dropdownlist, Textbox...</param>
        /// <returns></returns>
        public static JsControl ToNewestJsControl(this JsControl @this)
        {
            // Lấy lại cấu hình control mới nhất từ phiên bản eGov hiện tại.
            var service = new ControlService();
            var control = service.GetControl(@this.Type);
            if (control == null)
            {
                throw new ApplicationException(string.Format("Không tìm thấy cấu hình control có Type = {0}", @this.TypeId));
            }
            if (control.Properties == null)
            {
                throw new ApplicationException(string.Format("Không tìm thấy cấu hình Property của control có Type = {0}", @this.TypeId));
            }
            var newestProperties = control.Properties.Select(p => new JsProperty
                                                                            {
                                                                                Id = p.Id,
                                                                                Value = p.DefaultValue == null ? "" : p.DefaultValue.Val
                                                                            }).ToList();


            foreach (var newestProperty in newestProperties)
            {
                foreach (var property in @this.Properties)
                {
                    if (newestProperty.Id != property.Id) continue;
                    // Kiểm tra trường hợp GlobalCode property mới thêm nếu chưa có thì cần get từ GlobalCode của CatalogMeta, ExField
                    newestProperty.Value = property.Value;
                    break;
                }
            }

            @this.Properties = newestProperties;
            return @this;
        }

        /// <summary>
        ///  Trả về biểu mẫu có các control chứa đầy đủ các thuộc tính mới nhất của phiên bản eGov hiện tại.
        /// </summary>
        /// <param name="this">Đối tượng JsControl: một control trên form động như Dropdownlist, Textbox...</param>
        /// <returns></returns>
        public static List<JsControl> ToNewestJsControl(this List<JsControl> @this)
        {
            foreach (var control in @this)
            {
                control.ToNewestJsControl();
            }
            return @this;
        }

        /// <summary>
        /// <para>So sánh biểu mẫu động hiện tại với biểu mẫu động khác, xác định số lượng các control Dropdownlist (10), Textbox (9) trùng nhau không.</para>
        /// <para>Các control Label (1) có thể khác nhau. </para>
        /// True: giống, False: ngược lại.
        /// </summary>
        /// <param name="lstControls">Danh sách Control biểu mẫu động 1.</param>
        /// <param name="lstControls2">Danh sách Control biểu mẫu động 2.</param>
        /// <returns></returns>
        public static bool CompareTo(this List<JsControl> lstControls, List<JsControl> lstControls2)
        {
            // Nếu form mới chứa control form cũ không có
            foreach (var newCtr in lstControls)
            {
                // Bỏ qua các control không phải Textbox và Dropdownlist khi so sánh.
                if (!newCtr.TypeId.Equals((int)ControlType.Textbox) && !newCtr.TypeId.Equals((int)ControlType.DropDownList)
                    && !newCtr.TypeId.Equals((int)ControlType.CheckBoxList))
                {
                    continue;
                }

                var control = lstControls2.SingleOrDefault(oldCtr => newCtr.ControlId.Equals(oldCtr.ControlId));
                if (control == null)
                {
                    return false;
                }
                if (control.MaskType != newCtr.MaskType)
                {
                    return false;
                }
            }

            // Nếu form mới không chứa control form cũ có
            foreach (var oldCtr in lstControls2)
            {
                // Bỏ qua các control không phải Textbox và Dropdownlist khi so sánh.
                if (!oldCtr.TypeId.Equals((int)ControlType.Textbox) && !oldCtr.TypeId.Equals((int)ControlType.DropDownList)
                     && !oldCtr.TypeId.Equals((int)ControlType.CheckBoxList))
                {
                    continue;
                }
                var control = lstControls.SingleOrDefault(newCtr => newCtr.ControlId.Equals(oldCtr.ControlId));
                if (control == null)
                {
                    return false;
                }
                if (control.MaskType != oldCtr.MaskType)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
