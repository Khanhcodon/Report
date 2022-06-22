using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Bkav.eGovCloud.Core.Utils;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project       : eGov Cloud v1.0
    /// Class         : ControlService - public - Core 
    /// Access Modifiers:  
    /// Create Date   : 061112 
    /// Author        : CuongNT 
    /// </author>
    /// <summary>
    /// <para>Lớp quản lý control mẫu sử dụng trong biểu mẫu động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class ControlService
    {
        /// <summary>
        /// Đánh dấu đoạn đầu dữ liệu thư viện form động. Dùng để ghi, loại bỏ để lấy lại duy nhất đối tượng thư viện.
        /// </summary>
        private const string StartJsData = "(function (eForm) { eForm.jsonLib =";

        /// <summary>
        /// Đánh dấu cuối đầu dữ liệu thư viện form động. Dùng để ghi, loại bỏ để lấy lại duy nhất đối tượng thư viện.
        /// </summary>
        private const string EndJsData = "; })(window.eForm = window.eForm || {});";

        /// <summary>
        /// Trả về control mẫu mới nhất ở phiên bản hiện tại.
        /// </summary>
        /// <param name="controlType">Id của Control</param>
        /// <returns>JsControl hoặc Null</returns>
        public Control GetControl(ControlType controlType)
        {
            var controlId = (int)controlType;
            string filePath;
            try
            {
                filePath = ConfigurationManager.AppSettings["eFromJsonData"];
            }
            catch (ConfigurationErrorsException)
            {
                throw new ApplicationException("Chưa cấu hình AppSettings[\"eFromJsonData\"] cho thư viện form động.");
            }

            // Kiểm tra file tồn tại
            if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
            {
                throw new FileNotFoundException(string.Format("Data form động không tìm thấy tại {0}.", filePath));
            }

            // Đọc thông tin từ js
            var jsContent = File.ReadAllText(HttpContext.Current.Server.MapPath(filePath));
            jsContent = jsContent.Replace(StartJsData, "");
            jsContent = jsContent.Replace(EndJsData, "");

            var strJson = jsContent;
            var dataTables = Json2.ParseAsJs<DataTables>(strJson);//(DataTables)JsonConvert.DeserializeObject(strJson, typeof(DataTables), new JsonSerializerSettings { Culture = CultureInfo.InvariantCulture });

            // Lấy control
            var ctrl = dataTables.ControlTable.SingleOrDefault(c => c.Id == controlId);
            if (ctrl == null) return null;

            var jsControl = new Control
                              {
                                  Id = ctrl.Id,
                                  Name = ctrl.Name
                              };

            // Lấy thuộc tính control
            var jsProperties = (from cp in dataTables.ControlPropertyTable
                                join p in dataTables.PropertyTable on cp.PropertyId equals p.Id
                                where cp.ControlId == controlId
                                orderby cp.Orders ascending
                                select new Property
                                           {
                                               Id = p.Id,
                                               Name = p.Name,
                                               IsMultivalue = p.IsMultivalue,
                                               UIDescription = p.UIDescription,
                                               UIName = p.UIName
                                           }).ToList();
            if (!jsProperties.Any()) return null;

            jsControl.Properties = jsProperties;

            // Lấy giá trị mặc định của thuộc tính của control
            for (var i = 0; i < jsControl.Properties.Count(); i++)
            {
                var propertyId = jsControl.Properties[i].Id;
                var propertyValues = (from v in dataTables.PropertyValueTable
                                      where v.PropertyId == propertyId
                                      orderby v.Orders ascending
                                      select new PropertyValue
                                                 {
                                                     Id = v.Id,
                                                     IsDefault = v.IsDefault,
                                                     Orders = v.Orders,
                                                     UIVal = v.UIVal,
                                                     Val = v.Val,
                                                     PropertyId = v.PropertyId
                                                 }).ToList();
                if (!propertyValues.Any()) continue;

                jsControl.Properties[i].DefaultValue = new PropertyValue();
                if (propertyValues.Count() == 1)
                {
                    jsControl.Properties[i].DefaultValue.Val = propertyValues.First().Val;
                }
                else
                {
                    foreach (var itm in propertyValues.Where(itm => itm.IsDefault))
                    {
                        jsControl.Properties[i].DefaultValue.Val = itm.Val;
                        break;
                    }
                }
            }
            return jsControl;
        }
    }
}