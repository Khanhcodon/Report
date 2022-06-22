namespace Bkav.eGovCloud.Entities.Customer
{
    /// <summary>
    /// Bkav Corp. - BSO - eGov - eOffice team
    /// Project: eGov Cloud v1.0
    /// Class : KeyWord - public - Entity
    /// Access Modifiers: 
    /// Create Date : 221113
    /// Author      : DungHV
    /// Description : Entity tương ứng với bảng KeyWord trong CSDL
    /// </summary>
    public class KeyWord
    {
        /// <summary>
        /// Lấy hoặc thiết lập Id từ khóa
        /// </summary>
        public int KeyWordId { get; set; }

        /// <summary>
        /// Lấy hoặc thiết lập Tên từ khóa
        /// </summary>
        public string KeyWordName { get; set; }
    }
}
