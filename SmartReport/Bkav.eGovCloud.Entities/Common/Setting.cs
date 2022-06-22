namespace Bkav.eGovCloud.Entities.Common
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : Setting - public - Entity
    /// Access Modifiers: 
    /// Create Date : 090812
    /// Author      : TrungVH
    /// Description : Entity tương ứng với bảng Setting trong CSDL
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id của cấu hình
        /// </summary>
        public int SettingId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Key của cấu hình
        /// </summary>
        public string SettingKey { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Giá trị của cấu hình
        /// </summary>
        public string SettingValue { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return SettingKey;
        }
    }
}
