using System;
using System.Collections.Generic;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// <para>Bkav Corp. - BSO - eGov</para>
    /// <para>Project: eGov Cloud v1.0</para>
    /// <para>Class : ProcessFunctionFilter - public - Entity</para>
    /// <para>Access Modifiers: </para>
    /// <para>Create Date : 05/01/2015</para>
    /// <para>Author      : TienBV</para>
    /// <para>Description : Entity tương ứng với bảng ProcessFunctionFilter trong CSDL</para>
    /// </summary>
    public class ProcessFunctionFilter
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id bộ lọc
        /// </summary>
        public int ProcessFunctionFilterId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập tên bộ lọc
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập trường dữ liệu cần lọc
        /// </summary>
        public string DataField { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị để lọc.
        /// <para>
        /// Trường hợp Value là biểu thức Sql: trong câu sql cần select ra 2 trường có tên là DataField và TextField;
        /// </para>
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// <para>Lấy hoặc thiết lập giá trị xác định các giá trị lọc được lấy từ database</para>
        /// <para>Với kiểu giá trị này sẽ sinh ra một loạt các ProcessFunctionFilter tương ứng với mỗi giá trị</para>
        /// <para>(Nếu đưa lên node trên tree thì mỗi giá trị tương ứng với 1 node).</para>
        /// </summary>
        public bool IsSqlValue { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập biểu thức lọc
        /// </summary>
        public int FilterExpression { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập giá trị xác định có tự động sinh tên node theo giá trị của câu SQL không?
        /// </summary>
        /// <remarks>
        /// Chỉ thiết lập giá trị này = true khi IsSqlValue = true.
        /// </remarks>
        public bool IsAutoGenNodeName { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập mẫu tên của node sẽ sinh ra. Thiết lập theo dạng biểu thức của String.Format;
        /// </summary>
        public string NodeNameTemp { get; set; }
    }
}
