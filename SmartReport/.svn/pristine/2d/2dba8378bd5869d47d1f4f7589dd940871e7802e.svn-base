using System.Collections.Generic;
using System.Linq;
using Bkav.eGovCloud.Core.DynamicForm;
using Bkav.eGovCloud.Entities.Customer;

namespace Bkav.eGovCloud.Business
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Enum : FormExtension - public - BLL
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary> 
    /// <para>Các hàm mở rộng cho Form</para>
    /// <para>(CuongNT@bkav.com - 061112)</para>
    /// </summary>
    public static class FormExtension
    {
        /// <summary>
        /// <para>Chuyển đối tượng Danh mục catalog thành đối tượng control trên biểu mẫu động.</para>
        /// <para>(CuongNT - 161112)</para>
        /// </summary>
        /// <param name="catalogs"></param>
        /// <returns></returns>
        public static List<JsControl> ToJsControl(this List<Catalog> catalogs)
        {
            if (catalogs == null || catalogs.Count <= 0) return new List<JsControl>();

            var jsControls = new List<JsControl>();
            var service = new ControlService();
            var ddl = service.GetControl(ControlType.DropDownList);

            for (var i = 0; i < catalogs.Count(); i++)
            {
                var frmCtrl = new JsControl
                {
                    TypeId = ddl.Id,
                    PosRow = 0,//catalogs[i].PosRow.Equals(0) ? dNum.ToShort(currRow++) : catalogs[i].PosRow,
                    PosOrder = 0,//catalogs[i].PosOrder.Equals(0) ? (Int16)1 : catalogs[i].PosOrder,
                    Properties = ddl.Properties.Select(p => new JsProperty
                    {
                        Id = p.Id,
                        Value = p.DefaultValue == null ? "" : p.DefaultValue.Val
                    }).ToList(),
                    ControlId = catalogs[i].CatalogId,
                    ControlName = catalogs[i].CatalogName
                };
                jsControls.Add(frmCtrl);
            }
            return jsControls;
        }

        /// <summary>
        /// <para>Chuyển đối tượng Danh mục extendField thành đối tượng control trên biểu mẫu động.</para>
        /// <para>(CuongNT - 161112)</para>
        /// </summary>
        /// <param name="extendFields"></param>
        /// <returns></returns>
        public static List<JsControl> ToJsControl(this List<ExtendField> extendFields)
        {
            if (extendFields == null || extendFields.Count <= 0) return new List<JsControl>();

            var jsControls = new List<JsControl>();
            var service = new ControlService();
            var tbx = service.GetControl(ControlType.Textbox);

            for (var i = 0; i < extendFields.Count(); i++)
            {
                var frmCtrl = new JsControl
                {
                    TypeId = tbx.Id,
                    PosRow = 0,
                    PosOrder = 0,
                    Properties = tbx.Properties.Select(p => new JsProperty
                    {
                        Id = p.Id,
                        Value = p.DefaultValue == null ? "" : p.DefaultValue.Val
                    }).ToList(),
                    ControlId = extendFields[i].ExtendFieldId,
                    ControlName = extendFields[i].ExtendFieldName,
                    MaskType = extendFields[i].Mask
                };
                jsControls.Add(frmCtrl);
            }

            return jsControls;
        }

        /// <summary>
        /// <para>Convert list catalog to catalog js.</para>
        /// <para>TienBV 161112</para>
        /// </summary>
        /// <param name="catalogs"></param>
        /// <returns></returns>
        public static List<JsCatalog> ToListJsCatalog(this List<Catalog> catalogs)
        {
            var result = new List<JsCatalog>();
            foreach (var catalog in catalogs)
            {
                var itm = new JsCatalog
                              {
                                  CatalogId = catalog.CatalogId.ToString("N"),
                                  CatalogName = catalog.CatalogName,
                                  CatalogValues = catalog.CatalogValues.ToListJsCatalogItem()
                              };
                result.Add(itm);
            }
            return result;
        }

        /// <summary>
        /// <para>Convert list CatalogValue to JsCatalogItem.</para>
        /// <para>TienBV 161112</para>
        /// </summary>
        /// <param name="catalogValues"></param>
        /// <returns></returns>
        public static List<JsCatalogItem> ToListJsCatalogItem(this ICollection<CatalogValue> catalogValues)
        {
            var result = new List<JsCatalogItem>();
            foreach (var catalogValue in catalogValues)
            {
                var itm = new JsCatalogItem
                              {
                                  CatalogValueId = catalogValue.CatalogValueId.ToString("N"),
                                  Value = catalogValue.Value
                              };
                result.Add(itm);
            }
            return result;
        }
    }
}
