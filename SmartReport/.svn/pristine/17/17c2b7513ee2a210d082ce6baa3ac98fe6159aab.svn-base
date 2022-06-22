using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Bkav.eGovCloud.Core.DynamicForm
{
    /// <author>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : JsControl - public - Core
    /// Access Modifiers: 
    /// Create Date : 06112012
    /// Author      : CuongNT
    /// </author>
    /// <summary>
    /// <para>Đối tượng một control trên biểu mẫu động. Có thông tin về dòng, vị trí trong dòng của control trên form động.</para>
    /// (CuongNT@bkav.com - 061112)
    /// </summary>
    public class JsControl
    {
        /// <summary>
        /// Khởi tạo <see cref="JsControl"/>
        /// </summary>
        public JsControl()
        {
            Id = 0;
            TypeId = 0;
            PosRow = 0;
            PosOrder = 0;
            Properties = new List<JsProperty>();
        }

        /// <summary>
        /// Lấy hoặc thiết lập Id của control.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập kiểu control.<br/>
        /// Type: 10 - Dropdownlist, 9 - Textbox.
        /// </summary>
        /// <value>Kiểu control.</value>
        public int TypeId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí hàng mà control thuộc vào.
        /// </summary>
        /// <value>Vị trí hàng trong bảng.</value>
        public Int16 PosRow { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập vị trí trong hàng so với các control khác.
        /// </summary>
        /// <value>Vị trí nằm trong hàng so với control khác.</value>
        public Int16 PosOrder { get; set; }

        /// <summary>
        /// Danh sách các thuộc tính (Align, Color...).<br/>
        /// </summary>
        /// <value>Danh sách các thuộc tính.</value>
        /// <remarks>
        /// 
        /// </remarks>
        public List<JsProperty> Properties { get; set; }

        #region Json Ignore Properties

        /// <summary>
        /// Lấy hoặc thiết lập Độ dài tối da cho phép nhập
        /// </summary>
        [JsonIgnore]
        public bool ValidationLength { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Khoảng cho phép nhập
        /// </summary>
        [JsonIgnore]
        public bool ValidationRange { get; set; }

        /// <summary>
        /// Là thuộc tính số 15. Trả về Id của control trên biểu mẫu.<br/>
        /// Ví dụ: Dropdownlist(10): MetaId, Textbox(9): ExFieldId, Label(1): _new_1...
        /// </summary>
        /// <value>The control id.</value>
        [JsonIgnore]
        public Guid ControlId
        {
            get
            {
                // 15: ControlId
                var property = Properties.SingleOrDefault(p => p.Id == 15);
                Guid guid;
                return property == null ? new Guid() : Guid.TryParse(property.Value, out guid) ? new Guid(property.Value) : new Guid();
            }
            set
            {
                var property = Properties.SingleOrDefault(p => p.Id == 15);
                if (property != null)
                {
                    property.Value = value.ToString(CultureInfo.InvariantCulture.ToString());
                }
            }
        }

        /// <summary>
        /// Là thuộc tính số 17. Trả về Label đứng trước control trên biểu mẫu động.<br/>
        /// Dropdownlist: CatalogName, Textbox: ExtendFieldName.
        /// </summary>
        /// <value>CatalogName, ExtendFieldName.</value>
        [JsonIgnore]
        public string ControlName
        {
            get
            {
                // 17: LabelId
                JsProperty property = Properties.SingleOrDefault(p => p.Id == 17);
                return property == null ? "" : property.Value;
            }
            set
            {
                JsProperty property = Properties.SingleOrDefault(p => p.Id == 17);
                if (property != null)
                {
                    property.Value = value;
                }
            }
        }

        /// <summary>
        /// Là thuộc tính số 1. Trả về giá trị nhập, chọn của control.<br/>
        /// Dropdownlist: Id của CatalogItem được chọn, Textbox: nội dung được nhập.
        /// </summary>
        /// <value>The control value.</value>
        [JsonIgnore]
        public string ControlValue
        {
            get
            {
                // 1: Value
                JsProperty property = Properties.SingleOrDefault(p => p.Id == 1);
                return property == null ? "" : property.Value;
            }
            set
            {
                JsProperty property = Properties.SingleOrDefault(p => p.Id == 1);
                if (property != null)
                {
                    property.Value = value;
                }
            }
        }

        /// <summary>
        /// Là thuộc tính số 19. Trả về kiểu mask nhập liệu cho control Textbox.<br/>
        /// Mask của Textbox: text, datetime, monthyear, year, nature, integer, decimal, email, cmtnd, mobile.
        /// </summary>
        /// <value>Giá trị: text, datetime, monthyear, year, nature, integer, decimal, email, cmtnd, mobile...</value>
        [JsonIgnore]
        public string MaskType
        {
            get
            {
                var property = Properties.SingleOrDefault(p => p.Id == 19);
                return property == null ? "text" : property.Value;
            }

            set
            {
                var property = Properties.SingleOrDefault(p => p.Id == 19);
                if (property != null)
                {
                    property.Value = value;
                }
            }
        }

        /// <summary>
        /// Là thuộc tính số 13. Trả về giá trị control có bắt buộc phải nhập không.<br/>
        /// True: bắt buộc nhập, False: không bắt buộc nhập.
        /// </summary>
        /// <value>True/False.</value>
        [JsonIgnore]
        public bool ValidationRequired
        {
            get
            {
                var property = Properties.SingleOrDefault(p => p.Id == 13);
                bool tmp;
                return property != null && bool.TryParse(property.Value, out tmp) && bool.Parse(property.Value);
            }
        }

        /// <summary>
        /// Là thuộc tính số 24. Trả về Id dạng Guid của control.<br/>
        /// </summary>
        /// <value>Guid value.</value>
        [JsonIgnore]
        public Guid GlobalCode
        {
            get
            {
                var property = Properties.SingleOrDefault(p => p.Id == 24);
                if (property == null)
                {
                    throw new Exception("Không có cấu hình property 24: GlobalCode");
                }
                if (string.IsNullOrEmpty(property.Value) || property.Value.Equals(new Guid().ToString()))
                {
                    throw new Exception(string.Format("Không có cấu hình GlobalCode cho {0}", ControlName));
                }
                return new Guid(property.Value);
            }
            set
            {
                var property = Properties.SingleOrDefault(p => p.Id == 24);
                if (property == null)
                {
                    throw new Exception("Không có cấu hình property 24: GlobalCode");
                }
                property.Value = value.ToString();
            }
        }

        /// <summary>
        /// Trả về kiểu control. Là dạng ControlType của TypeId.
        /// </summary>
        [JsonIgnore]
        public ControlType Type
        {
            get { return (ControlType)TypeId; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị được chọn từ control Dropdownlist, Checkboxlist
        /// </summary>
        [JsonIgnore]
        public JsCatalogSelected CatalogSelecteds { get; set; }
        #endregion
    }
}