using System.ComponentModel;
namespace Bkav.eGovCloud.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum ProcessFilterExpression
    {
        /// <summary>
        /// Nhóm theo một trường dữ liệu nào đó.
        /// <para>
        /// Sử dụng biểu thức này sẽ tự sinh các node con theo trường được nhóm và chỉ những nhóm có trong danh sách dữ liệu mới được hiển thị.
        /// </para>
        /// </summary>
        [Description("egovcloud.enum.processfilterexpression.groupby")]
        GroupBy = 1,

        /// <summary>
        /// Lọc theo một giá trị.
        /// <para>
        /// Trường hợp giá trị là dữ liệu lấy từ Sql: khác với GroupBy, danh sách các node con sẽ luôn cố định.
        /// </para>
        /// </summary>
        [Description("egovcloud.enum.processfilterexpression.equal")]
        Equal = 2,

        /// <summary>
        /// Lọc theo biểu thức javascript được cấu hình.
        /// <para>
        /// Khi xuống client sẽ eval biểu thức này;
        /// </para>
        /// </summary>
        [Description("egovcloud.enum.processfilterexpression.custom")]
        Custom = 3
    }
}
